using System.Windows;
using ENG.Plugins.Revit.Common.Abstractions;
using ENG.Plugins.Revit.Modules.Parameters.ViewModels;

namespace ENG.Plugins.Revit.Modules.Parameters.Views;

/// <inheritdoc cref="System.Windows.Window" />
/// <summary>
///     View for the Parameter Scanner
/// </summary>
public sealed partial class ParameterScannerView : IClosableWindow
{
    public ParameterScannerView(ParameterScannerViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    private ParameterScannerViewModel ViewModel => (ParameterScannerViewModel)DataContext;

    private void Window_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.InitializeCommand.Execute(null);
    }
}
