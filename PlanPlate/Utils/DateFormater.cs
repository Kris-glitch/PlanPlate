namespace PlanPlate.Utils
{
    public static class DateFormater
    {

        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMMM-yyyy");
        }

        public static List<string> GetWeekDates(DateTime date)
        {
            List<string> weekDates = new List<string>();

            DateTime today = date.Date;
            int difference = DayOfWeek.Monday - today.DayOfWeek;
            DateTime monday = today.AddDays(difference);

           
            weekDates.Add(DateTimeToString(monday));

            for (int i = 1; i < 7; i++)
            {
                weekDates.Add(DateTimeToString(monday.AddDays(i)));
            }
            return weekDates;
        }
    }
}
