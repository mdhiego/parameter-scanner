using Nice3point.Revit.Toolkit.External;

namespace ENG.Plugins.Revit.Commands.Availability;

/// <inheritdoc />
/// <summary>
///     Only Available if the active document is open
/// </summary>
public sealed class DocumentCommandAvailability : ExternalCommandAvailability
{
    public override bool SetCommandAvailability(UIApplication applicationData, CategorySet selectedCategories)
    {
        return applicationData.ActiveUIDocument?.Document is not null;
    }
}
