namespace ENG.Plugins.Revit.Commands.Availability;

/// <inheritdoc />
/// <summary>
///     Only Available if the active document is open
/// </summary>
public sealed class DocumentCommandAvailability : IExternalCommandAvailability
{
    public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
    {
        return applicationData.ActiveUIDocument?.Document is not null;
    }
}
