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
    class BillRepositoryTests : IntegrationTestBaseFixture
    {
        [Test]
        public void Should_Add_A_Bill_To_The_Repository()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<IRecordRepository>();

            repo.SaveRecord(new Record(phoneNumber, "This is a test bill", 1.2M));

            repo.GetRecordsForPhoneNumber(phoneNumber).Count().Should().Be(1);
        }

        [Test]
        public void Should_Find_All_Bills_For_Specific_Phone_Number()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<IRecordRepository>();

            repo.SaveRecord(new Record(phoneNumber, "This is a test bill", 1.2M));
            repo.SaveRecord(new Record("11111111", "This is a test bill", 2.2M));
            repo.SaveRecord(new Record(phoneNumber, "This is a test bill", 3.2M));
            repo.SaveRecord(new Record("22222222", "This is a test bill", 4.2M));

            repo.GetRecordsForPhoneNumber(phoneNumber).Count().Should().Be(2);
        }

        [Test]
        public void Should_Return_An_Empty_List_When_No_Bills_Where_Found_With_Specified_Phone_Number()
        {
            var phoneNumber = "44556677";

            var repo = ObjectFactory.GetInstance<IRecordRepository>();

            repo.SaveRecord(new Record("11111111", "This is a test bill", 2.2M));
            repo.SaveRecord(new Record("22222222", "This is a test bill", 4.2M));

            repo.GetRecordsForPhoneNumber(phoneNumber).Count().Should().Be(0);
        }

        [Test]
        public void BillRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<IRecordRepository>();
            var secondRepo = ObjectFactory.GetInstance<IRecordRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
