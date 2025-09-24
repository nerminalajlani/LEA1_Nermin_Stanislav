﻿using System;
using System.Globalization;
using System.Windows.Data;

public class StringIstLeerOderNullKonverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = value as string;
        return string.IsNullOrEmpty(str);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
