using NUnit.Framework;

namespace IntegrationTest
{
    [TestFixture]
    internal abstract class IntegrationTestBaseFixture
    {
        [SetUp]
        public void InitializeApplication()
        {
            new AccountingMachine.AppConfigurator().Initialize();

            new CallCentral.AppConfigurator().Initialize();

            new SubscriptionService.AppConfigurator().Initialize();
        }
    }
}
