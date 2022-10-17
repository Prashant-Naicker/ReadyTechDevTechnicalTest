namespace ReadyTechDevTechnicalTest.Domain
{
    public class CoffeeProvider : ICoffeeProvider
    {
        private int _count = 0;
        public bool CoffeeAvailable() => Interlocked.Increment(ref _count) % 5 == 0;
    }
}
