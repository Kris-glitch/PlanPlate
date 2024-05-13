using System.Globalization;


namespace PlanPlate.Converters
{
    public class ListElementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IList<string> list && parameter is string indexString)
            {
                if (int.TryParse(indexString, out int index) && index >= 0 && index < list.Count)
                {
                    return list[index];
                }
            }
            return null; // Or whatever default value you want to return
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
