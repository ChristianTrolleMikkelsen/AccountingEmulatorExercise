using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using StructureMap;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class CallRepositoryTests : IntegrationTestBaseFixture
    {
        [Test]
        public void Should_Add_A_VoiceCall_To_The_Repository()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<ICallRepository>();

            repo.RegisterACallForPhone(new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "11112222", "DK", "DK"));

            repo.GetCallsMadeByPhone(phoneNumber).Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_Calls_For_Specific_Phone_Number()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<ICallRepository>();

            repo.RegisterACallForPhone(new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "11111111", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceServiceCall("11111111", DateTime.Now, DateTime.Now.TimeOfDay, "22222222", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceServiceCall("44556671", DateTime.Now, DateTime.Now.TimeOfDay, "33333333", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceServiceCall(phoneNumber, DateTime.Now, DateTime.Now.TimeOfDay, "44444444", "DK", "DK"));

            repo.GetCallsMadeByPhone(phoneNumber).Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_Not_Calls_Where_Made_With_Specified_Phone_Number()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<ICallRepository>();

            repo.RegisterACallForPhone(new VoiceServiceCall("11111111", DateTime.Now, DateTime.Now.TimeOfDay, "22222222", "DK", "DK"));
            repo.RegisterACallForPhone(new VoiceServiceCall("44556671", DateTime.Now, DateTime.Now.TimeOfDay, "33333333", "DK", "DK"));

            repo.GetCallsMadeByPhone(phoneNumber).Count().Should().Be(0);
        }

        [Test]
        public void PhoneCallRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<ICallRepository>();
            var secondRepo = ObjectFactory.GetInstance<ICallRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
