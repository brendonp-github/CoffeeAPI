namespace CoffeeAPI.Util
{
    public class DateTimeUtil
    {
        /// <summary>
        /// ISO-8601 format: UTC Date and time + current timezone offset
        /// </summary>
        public static string GetISO8601Format(DateTime dateTime) => $"{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss}{DateTime.Now:zzz}";
    }
}
