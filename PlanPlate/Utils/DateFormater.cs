namespace PlanPlate.Utils
{
    public static class DateFormater
    {

        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMMM-yyyy");
        }
    }
}
