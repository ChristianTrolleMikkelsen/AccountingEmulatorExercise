using System.Linq;
using Core.Models;
using Core.Repositories;

using Core.ServiceCharges.Voice;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;
using TestHelpers;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class LocalServiceChargeRepositoryTests : IntegrationTestBaseFixture
    {
        private ISubscription _subscription;

        [SetUp]
        public void CreateSubscription()
        {
            var phoneNumber = "44556677";

            _subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);
        }

        [Test]
        public void Should_Add_A_ServiceCharge_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<ILocalServiceChargeRepository>();

            repo.SaveServiceCharge(new SecondCharge(_subscription.PhoneNumber, 1.1M));

            repo.GetServiceChargesForPhoneNumber(_subscription.PhoneNumber)
                    .Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_ServiceCharge_For_Specific_Phone_Number_And_Service()
        {
            var repo = ObjectFactory.GetInstance<ILocalServiceChargeRepository>();

            repo.SaveServiceCharge(new SecondCharge(_subscription.PhoneNumber, 1.1M));
            repo.SaveServiceCharge(new SecondCharge("11111111", 1.1M));
            repo.SaveServiceCharge(new SecondCharge(_subscription.PhoneNumber, 1.1M));
            repo.SaveServiceCharge(new SecondCharge("22222222", 1.1M));

            repo.GetServiceChargesForPhoneNumber(_subscription.PhoneNumber)
                    .Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_No_ServiceCharges_Could_Be_Found_With_Specified_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<ILocalServiceChargeRepository>();

            repo.GetServiceChargesForPhoneNumber(_subscription.PhoneNumber)
                    .Count().Should().Be(0);
        }

        [Test]
        public void ServiceChargeRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<ILocalServiceChargeRepository>();
            var secondRepo = ObjectFactory.GetInstance<ILocalServiceChargeRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
