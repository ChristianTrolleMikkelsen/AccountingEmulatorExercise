using System.Linq;
using Core;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;
using SubscriptionServices;
using SubscriptionServices.Repositories;
using SubscriptionServices.ServiceCharges;
using TestHelpers;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class ServiceChargeRepositoryTests : IntegrationTestBaseFixture
    {
        private ISubscription _subscription;

        [SetUp]
        public void CreateSubscription()
        {
            var phoneNumber = "44556677";

            var subscriptionRegistration = ObjectFactory.GetInstance<ISubscriptionRegistration>();
            var customerRegistration = ObjectFactory.GetInstance<ICustomerRegistration>();

            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(subscriptionRegistration, customerRegistration, phoneNumber, "DK", CustomerStatus.Normal);
        }

        [Test]
        public void Should_Add_A_ServiceCharge_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<IServiceChargeRepository>();

            repo.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice,1.1M, "Standard Call Fee", "DK"));

            repo.GetServiceChargesByPhoneNumber( _subscription.PhoneNumber)
                    .Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_ServiceCharge_For_Specific_Phone_Number_And_Service()
        {
            var repo = ObjectFactory.GetInstance<IServiceChargeRepository>();

            repo.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1.1M, "Standard Call Fee", "DK"));
            repo.SaveServiceCharge(new FixedCharge("11111111", ServiceType.Voice, 1.1M, "Standard Call Fee", "DK"));
            repo.SaveServiceCharge(new FixedCharge(_subscription.PhoneNumber, ServiceType.Voice, 1.1M, "Standard Call Fee", "DK"));
            repo.SaveServiceCharge(new FixedCharge("22222222", ServiceType.Voice, 1.1M, "Standard Call Fee", "DK"));

            repo.GetServiceChargesByPhoneNumber(_subscription.PhoneNumber)
                    .Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_No_ServiceCharges_Could_Be_Found_With_Specified_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<IServiceChargeRepository>();

            repo.GetServiceChargesByPhoneNumber(_subscription.PhoneNumber)
                    .Count().Should().Be(0);
        }

        [Test]
        public void ServiceChargeRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<IServiceChargeRepository>();
            var secondRepo = ObjectFactory.GetInstance<IServiceChargeRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
