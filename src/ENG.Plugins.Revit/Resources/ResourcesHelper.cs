using System.Windows.Media.Imaging;

namespace ENG.Plugins.Revit.Resources;

public static class ResourcesHelper
{
    private static readonly string PackName = typeof(ResourcesHelper).Assembly.GetName().Name!;

    public static Uri GetIconUri(string iconName)
    {
        var iconUri = new Uri($"pack://application:,,,/{PackName};component/Resources/Icons/{iconName}");

        return iconUri;
    }

    public static BitmapImage GetIconFromAssets(string fileName)
    {
        var icon = new BitmapImage(GetIconUri(fileName));

        return icon;
    }

    public static BitmapImage GetIconFromAssets(string fileName, int width, int height)
    {
        var image = GetIconFromAssets(fileName);

        image.BeginInit();

        image.DecodePixelWidth = width;
        image.DecodePixelHeight = height;

        image.EndInit();

        return image;
    }
}
