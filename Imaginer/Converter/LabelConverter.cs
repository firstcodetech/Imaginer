using Imaginer.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Imaginer.Converter
{
    public class LabelConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int resourceKey = System.Convert.ToInt32(parameter);
            if (resourceKey != 0)
            {
                return ResourceMangagerService.GetResourceString(resourceKey);
            }
            return string.Empty;
        }

        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
