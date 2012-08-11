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

            new CallServices.AppConfigurator().Initialize();

            new SubscriptionServices.AppConfigurator().Initialize();

            new ChargeServices.AppConfigurator().Initialize();
        }
    }
}
