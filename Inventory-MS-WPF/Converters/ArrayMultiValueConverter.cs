using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Inventory_MS_WPF.Converters
{
    class ArrayMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.ToArray();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private static ArrayMultiValueConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null) _converter = new ArrayMultiValueConverter();
            return _converter;
        }

        public ArrayMultiValueConverter()
            : base()
        {
        }
    }
}
