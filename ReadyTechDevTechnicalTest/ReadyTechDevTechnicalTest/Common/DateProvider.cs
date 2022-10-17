namespace ReadyTechDevTechnicalTest.Common
{
    public class DateProvider : IDateProvider
    {
        public DateTimeOffset GetNow() => DateTimeOffset.Now;
    }
}
