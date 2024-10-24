using Nice3point.Revit.Toolkit.External;

namespace ENG.Plugins.Revit.Commands.Availability;

/// <inheritdoc />
/// <summary>
///     Only Available if the active document is open and the active view is a 3D view or floor/ceiling plan
/// </summary>
public sealed class ViewPlansAnd3DCommandAvailability : ExternalCommandAvailability
{
    public override bool SetCommandAvailability(UIApplication applicationData, CategorySet selectedCategories)
    {
        return applicationData.ActiveUIDocument?.Document is not null
               && applicationData.ActiveUIDocument.Document.ActiveView?.ViewType
                   is ViewType.ThreeD
                   or ViewType.FloorPlan
                   or ViewType.CeilingPlan;
    }
}
