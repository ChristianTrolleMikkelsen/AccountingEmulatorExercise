using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator.Factories;
using StructureMap;

namespace IntegrationTest.Factories
{
    [TestFixture]
    class PhoneSubscriptionFactory : IntegrationTestBaseFixture
    {
        [Test]
        public void Should_Return_Working_Factory()
        {
            var factory = ObjectFactory.GetInstance<IPhoneSubscriptionFactory>();

            factory.CreateBlankSubscriptionWithPhoneNumberAndLocalCountry("11111111")
                        .Should().NotBeNull();
        }
    }
}
