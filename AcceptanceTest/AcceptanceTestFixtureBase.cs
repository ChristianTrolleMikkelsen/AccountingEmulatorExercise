using NUnit.Framework;
using PhoneSubscriptionCalculator;

namespace AcceptanceTest
{
    [TestFixture]
    internal abstract class AcceptanceTestFixtureBase
    {
        [SetUp]
        public void InitializeApplication()
        {
            new AppConfigurator().Initialize();
        }
    }
}
