using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LEA_Nermin_Alajlani_Stanislav_Kharchenko
{
    public class AbsenderZuFarbeKonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string absender = value as string;
            string aktuellerBenutzer = parameter as string;

            if (string.IsNullOrEmpty(absender) || string.IsNullOrEmpty(aktuellerBenutzer))
                return Brushes.Gray;

            return absender == aktuellerBenutzer ? Brushes.LightBlue : Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
