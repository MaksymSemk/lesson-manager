using System.Windows;
using System.Windows.Data;

namespace LessonManager.WPF.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        if (value is bool boolValue)
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        return value is Visibility vis && vis == Visibility.Visible;
    }
}

public class NullToBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        return value != null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}

public class EmptyStringToCollapsedConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        if (value is string str && !string.IsNullOrEmpty(str))
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}
