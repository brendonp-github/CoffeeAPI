namespace CoffeeAPI.Util
{
    public class DateTimeProvider //based off https://dvoituron.com/2020/01/22/UnitTest-DateTime/
    {
        public static DateTime Now
            => DateTimeProviderContext.Current == null
                    ? DateTime.Now
                    : DateTimeProviderContext.Current.ContextDateTimeNow;

        public static DateTime UtcNow => Now.ToUniversalTime();

        public static DateTime Today => Now.Date;
    }
}
