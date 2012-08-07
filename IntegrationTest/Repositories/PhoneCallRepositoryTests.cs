using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using StructureMap;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class PhoneCallRepositoryTests : IntegrationTestBaseFixture
    {
        [Test]
        public void Should_Add_A_VoiceCall_To_The_Repository()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<IPhoneCallRepository>();

            repo.RegisterACallForPhone(new VoiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "11112222", "DK", "DK"));

            repo.GetCallsForPhone(phoneNumber).Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_Calls_For_Specific_Phone_Number()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<IPhoneCallRepository>();

            repo.RegisterACallForPhone(new VoiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "11111111", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceCall("11111111", DateTime.Now, DateTime.Now.TimeOfDay, "22222222", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceCall("44556671", DateTime.Now, DateTime.Now.TimeOfDay, "33333333", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "44444444", "DK", "DK"));

            repo.GetCallsForPhone(phoneNumber).Count().Should().Be(2);
        }

        [Test]
        public void PhoneCallRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Across_Subscriptions_In_Single_Threaded_Application()
        {
            var firstRepo = ObjectFactory.GetInstance<IPhoneCallRepository>();
            var secondRepo = ObjectFactory.GetInstance<IPhoneCallRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
