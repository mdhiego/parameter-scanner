using Autodesk.Revit.Attributes;
using ENG.Plugins.Revit.Modules.Parameters.ViewModels;
using ENG.Plugins.Revit.Modules.Parameters.Views;
using Nice3point.Revit.Toolkit.External;

namespace ENG.Plugins.Revit.Commands;

/// <inheritdoc />
/// <summary>
///     Opens a dialog that allows users to scan model elements for specific parameter values
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class ParameterScannerCommand : ExternalCommand
{
    private static ParameterScannerView? s_view;

    public override void Execute()
    {
        if (s_view is null)
        {
            var viewModel = new ParameterScannerViewModel(ExternalCommandData.Application.ActiveUIDocument);
            s_view = new ParameterScannerView(viewModel);
            s_view.Closed += static (_, _) => s_view = null;

            s_view?.Show();
        }
        else
        {
            s_view.Close();
        }
    }
}
