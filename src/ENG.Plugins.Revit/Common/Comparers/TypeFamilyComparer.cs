namespace ENG.Plugins.Revit.Common.Comparers;

public sealed class TypeFamilyComparer : IEqualityComparer<ElementType>
{
    public static readonly TypeFamilyComparer Instance = new();

    public bool Equals(ElementType x, ElementType y) => Equals(x.FamilyName, y.FamilyName);

    public int GetHashCode(ElementType obj) => obj.FamilyName.GetHashCode();
}
