using Nice3point.Revit.Extensions;

namespace ENG.Plugins.Revit.Common.Comparers;

public sealed class InstanceFamilyComparer : IEqualityComparer<Element>
{
    public static readonly InstanceFamilyComparer Instance = new();

    public bool Equals(Element x, Element y) => Equals(
        (x.GetTypeId().ToElement<ElementType>(x.Document))?.FamilyName,
        (y.GetTypeId().ToElement<ElementType>(y.Document))?.FamilyName
    );

    public int GetHashCode(Element obj) => obj.GetTypeId().ToElement<ElementType>(obj.Document)?.FamilyName.GetHashCode() ?? -1;
}
