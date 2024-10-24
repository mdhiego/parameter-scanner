namespace ENG.Plugins.Revit.Common.Comparers;

public sealed class ElementComparer : IEqualityComparer<Element>
{
    public static readonly ElementComparer Instance = new();

    public bool Equals(Element x, Element y) => Equals(x.Id, y.Id);

    public int GetHashCode(Element obj) => obj.Id.GetHashCode();
}
