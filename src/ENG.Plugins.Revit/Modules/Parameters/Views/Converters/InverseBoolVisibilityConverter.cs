using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Visibility = System.Windows.Visibility;

namespace ENG.Plugins.Revit.Modules.Parameters.Views.Converters;

[MarkupExtensionReturnType(typeof(InverseBoolVisibilityConverter))]
[ValueConversion(typeof(bool), typeof(Visibility))]
public class InverseBoolVisibilityConverter : MarkupExtension, IValueConverter
{
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is false ? Visibility.Visible : Visibility.Hidden;
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is not Visibility.Visible;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
