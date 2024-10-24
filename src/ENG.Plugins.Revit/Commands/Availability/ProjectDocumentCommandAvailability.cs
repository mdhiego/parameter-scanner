namespace ENG.Plugins.Revit.Commands.Availability;

/// <inheritdoc />
/// <summary>
///     Only Available if the active document is open and it not a family document
/// </summary>
public sealed class ProjectDocumentCommandAvailability : IExternalCommandAvailability
{
    public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
    {
        return applicationData.ActiveUIDocument?.Document is not null
               && applicationData.ActiveUIDocument.Document.IsFamilyDocument == false;
    }
}
