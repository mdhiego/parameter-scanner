using ENG.Plugins.Revit.Commands;
using ENG.Plugins.Revit.Commands.Availability;
using ENG.Plugins.Revit.Resources;
using Nice3point.Revit.Extensions;
using Nice3point.Revit.Toolkit.External;

namespace ENG.Plugins.Revit;

/// <inheritdoc />
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        CreateParametersRibbon();
    }

    /// <summary>
    ///     Creates the Parameters ribbon, containing the Parameter Scanner button
    /// </summary>
    private void CreateParametersRibbon()
    {
        var panel = Application.CreatePanel("Commands", "Parameters");

        panel
            .AddPushButton<ParameterScannerCommand>("Parameter Scanner")
            .SetAvailabilityController<ViewPlansAnd3DCommandAvailability>()
            .SetImage(ResourcesHelper.GetIconUri("ParameterScanner.ico").AbsoluteUri)
            .SetLargeImage(ResourcesHelper.GetIconUri("ParameterScanner.ico").AbsoluteUri);
    }
}
