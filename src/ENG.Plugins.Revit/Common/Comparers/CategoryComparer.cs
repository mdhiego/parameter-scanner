namespace ENG.Plugins.Revit.Common.Comparers;

public sealed class CategoryComparer : IEqualityComparer<Category>
{
    public static readonly CategoryComparer Instance = new();

    public bool Equals(Category x, Category y) => Equals(x.Id, y.Id);

    public int GetHashCode(Category obj) => obj.Id.GetHashCode();
}
