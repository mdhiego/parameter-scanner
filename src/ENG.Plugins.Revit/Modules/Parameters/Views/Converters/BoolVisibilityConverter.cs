using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Visibility = System.Windows.Visibility;

namespace ENG.Plugins.Revit.Modules.Parameters.Views.Converters;

[MarkupExtensionReturnType(typeof(BoolVisibilityConverter))]
[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolVisibilityConverter : MarkupExtension, IValueConverter
{
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is true ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is Visibility.Visible;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
