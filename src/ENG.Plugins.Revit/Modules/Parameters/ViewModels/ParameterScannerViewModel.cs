using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ENG.Plugins.Revit.Common.Comparers;
using Nice3point.Revit.Extensions;

namespace ENG.Plugins.Revit.Modules.Parameters.ViewModels;

/// <inheritdoc />
/// <summary>
///     View Model for the Parameter Scanner
/// </summary>
public sealed partial class ParameterScannerViewModel : ObservableObject
{
    private readonly UIDocument _activeDocument;

    [ObservableProperty]
    private string[] _parameterNames = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(IsolateInViewCommand))]
    [NotifyCanExecuteChangedFor(nameof(SelectCommand))]
    private string _parameterName;

    [ObservableProperty]
    private string? _parameterValue;

    public ParameterScannerViewModel(UIDocument activeDocument)
    {
        _activeDocument = activeDocument;
    }

    [RelayCommand]
    private void Initialize()
    {
        var viewInstances = _activeDocument.Document
            .EnumerateInstances(_activeDocument.ActiveView.Id)
            .Where(e =>
                e.Category is { Id: not null, CategoryType: CategoryType.Model or CategoryType.AnalyticalModel }
                && e is not Material and not ViewSheet and not ProjectInfo
            );

        // Collect all parameter names efficiently to avoid unnecessary data fetching
        var viewParameterNames = new List<string>();
        foreach (var category in viewInstances.GroupBy(e => e.Category.Id))
        {
            foreach (var element in category.Distinct(InstanceFamilyComparer.Instance))
            {
                string[] elementParameters = element.GetOrderedParameters().Select(p => p.Definition.Name).ToArray();
                viewParameterNames.AddRange(elementParameters);
            }
        }

        ParameterNames = viewParameterNames.Distinct().OrderBy(n => n).ToArray();
        if (ParameterNames.Length > 0)
        {
            ParameterName = ParameterNames[0];
        }
    }

    private bool IsFormValid => !string.IsNullOrEmpty(ParameterName);

    /// <summary>
    ///     Isolates all matching elements in the current view
    /// </summary>
    [RelayCommand(CanExecute = nameof(IsFormValid))]
    private void IsolateInView()
    {
        using var isolateTransaction = new Transaction(_activeDocument.Document, Localizations.IsolateInView_TransactionName);
        try
        {
            isolateTransaction.Start();

            var matchingElementIds = GetMatchingElementIds();
            _activeDocument.ActiveView.IsolateElementsTemporary(matchingElementIds);

            isolateTransaction.Commit();

            TaskDialog.Show(Localizations.ParameterScanner_Title, string.Format(Localizations.ElementsFoundMessage, matchingElementIds.Count));
        }
        catch (Exception ex)
        {
            isolateTransaction.RollBack();
            TaskDialog.Show($"{Localizations.ParameterScanner_Title} Error", ex.Message);
        }
    }

    /// <summary>
    ///     Selects all matching elements in the current view
    /// </summary>
    [RelayCommand(CanExecute = nameof(IsFormValid))]
    private void Select()
    {
        try
        {
            var matchingElementIds = GetMatchingElementIds();
            _activeDocument.Selection.SetElementIds(matchingElementIds);

            TaskDialog.Show(Localizations.ParameterScanner_Title, string.Format(Localizations.ElementsFoundMessage, matchingElementIds.Count));
        }
        catch (Exception ex)
        {
            TaskDialog.Show($"{Localizations.ParameterScanner_Title} Error", ex.Message);
        }
    }

    /// <summary>
    ///     Returns all matching element ids in the current view given the provided parameter name and value
    /// </summary>
    /// <returns>
    ///     List of matching element ids
    /// </returns>
    private List<ElementId> GetMatchingElementIds()
    {
        var matchingElementIds = new List<ElementId>();

        var viewElements = _activeDocument.Document.EnumerateInstances(_activeDocument.ActiveView.Id);
        foreach (var viewElement in viewElements)
        {
            var parameter = viewElement.LookupParameter(ParameterName);
            if (parameter is null) continue;

            if (string.IsNullOrEmpty(ParameterValue) || parameter.AsValueString() == ParameterValue)
            {
                matchingElementIds.Add(viewElement.Id);
            }
        }

        return matchingElementIds;
    }
}
