using System.Linq;
using Core;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;
using SubscriptionService;
using SubscriptionService.Repositories;
using SubscriptionService.Services;
using TestHelpers;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class ServiceRepositoryTests : IntegrationTestBaseFixture
    {
        private ISubscription subscription;

        [SetUp]
        public void CreateSubscription()
        {
            var phoneNumber = "44556677";

            var service = ObjectFactory.GetInstance<ISubscriptionService>();

            subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(service, phoneNumber, "DK", CustomerStatus.Normal);
        }

        [Test]
        public void Should_Add_A_Service_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new Service(subscription.PhoneNumber, ServiceType.Voice));

            repo.GetServicesForPhoneNumber(subscription.PhoneNumber).Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_Calls_For_Specific_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new Service(subscription.PhoneNumber, ServiceType.Voice));
            repo.SaveService(new Service("99999999", ServiceType.Voice));
            repo.SaveService(new Service("88888888", ServiceType.Voice));
            repo.SaveService(new Service(subscription.PhoneNumber, ServiceType.Voice));

            repo.GetServicesForPhoneNumber(subscription.PhoneNumber).Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_Not_Calls_Where_Made_With_Specified_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new Service("99999999", ServiceType.Voice));
            repo.SaveService(new Service("88888888",ServiceType.Voice));

            repo.GetServicesForPhoneNumber(subscription.PhoneNumber).Count().Should().Be(0);
        }

        [Test]
        public void ServiceRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Across_Subscriptions_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<IServiceRepository>();
            var secondRepo = ObjectFactory.GetInstance<IServiceRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
