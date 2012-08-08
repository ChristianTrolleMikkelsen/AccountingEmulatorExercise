using NUnit.Framework;
using PhoneSubscriptionCalculator;
using PhoneSubscriptionCalculator.Factories;
using StructureMap;
using TechTalk.SpecFlow;

namespace AcceptanceTest
{
    [TestFixture]
    internal abstract class AcceptanceTestFixtureBase
    {
        protected IPhoneSubscriptionFactory _subscriptionFactory;

        [BeforeScenario]
        public void InitializeApplication()
        {
            new AppConfigurator().Initialize();
        }

        [BeforeScenario]
        public void CreateHelpers()
        {
            _subscriptionFactory = ObjectFactory.GetInstance<IPhoneSubscriptionFactory>();
        }
    }
}
