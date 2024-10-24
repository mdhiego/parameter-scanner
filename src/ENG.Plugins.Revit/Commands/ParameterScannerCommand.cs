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
    public override void Execute()
    {
        var viewModel = new ParameterScannerViewModel(ExternalCommandData.Application.ActiveUIDocument);
        var view = new ParameterScannerView(viewModel);
        view.ShowDialog();
    }
}
