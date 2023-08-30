namespace Casino.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Get8AMUTCNextDay(this DateTime dt)
        {
            if (dt.Hour >= 8)
            {
                dt = dt.AddDays(1);
            }
            return dt.AddHours(8);
        }
    }
}
