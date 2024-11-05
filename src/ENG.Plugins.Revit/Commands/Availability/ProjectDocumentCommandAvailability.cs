using Nice3point.Revit.Toolkit.External;

namespace ENG.Plugins.Revit.Commands.Availability;

/// <inheritdoc />
/// <summary>
///     Only Available if the active document is open and it not a family document
/// </summary>
public sealed class ProjectDocumentCommandAvailability : ExternalCommandAvailability
{
    public override bool SetCommandAvailability(UIApplication applicationData, CategorySet selectedCategories)
    {
        return applicationData.ActiveUIDocument?.Document is not null
               && applicationData.ActiveUIDocument.Document.IsFamilyDocument == false;
    }
}
