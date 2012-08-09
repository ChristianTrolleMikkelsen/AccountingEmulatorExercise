using System.Linq;
using Core.Models;
using Core.Repositories;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;
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

            subscription = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer(phoneNumber);
            
        }

        [Test]
        public void Should_Add_A_Service_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new VoiceService(subscription.PhoneNumber));

            repo.GetServicesForPhoneNumber(subscription.PhoneNumber).Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_Calls_For_Specific_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new VoiceService(subscription.PhoneNumber));
            repo.SaveService(new VoiceService("99999999"));
            repo.SaveService(new VoiceService("88888888"));
            repo.SaveService(new VoiceService(subscription.PhoneNumber));

            repo.GetServicesForPhoneNumber(subscription.PhoneNumber).Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_Not_Calls_Where_Made_With_Specified_Phone_Number()
        {
            var repo = ObjectFactory.GetInstance<IServiceRepository>();

            repo.SaveService(new VoiceService("99999999"));
            repo.SaveService(new VoiceService("88888888"));

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
