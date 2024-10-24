using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ENG.Plugins.Revit.Modules.Parameters.Views.Converters;

[MarkupExtensionReturnType(typeof(InverseBoolConverter))]
[ValueConversion(typeof(bool), typeof(bool))]
public class InverseBoolConverter : MarkupExtension, IValueConverter
{
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is false;
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is false;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
