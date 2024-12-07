using System.Globalization;

namespace EndPoint.WebSite.Units
{
    public static class Converts
    {
        public static string ToShamsi(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            return $"{year}/{month}/{day}";
        }
    }
}
