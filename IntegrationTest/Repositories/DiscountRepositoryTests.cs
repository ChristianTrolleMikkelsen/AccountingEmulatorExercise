using System;
using System.Linq;
using AccountingMachine.Models;
using AccountingMachine.Repositories;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class DiscountRepositoryTests : IntegrationTestBaseFixture
    {
        [Test]
        public void Should_Add_A_Discount_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<IDiscountRepository>();

            repo.SaveDiscount(new Discount(CustomerStatus.Normal, 0.1M));

            repo.GetDiscountForCustomerStatus(CustomerStatus.Normal).Percentage.Should().Be(0.1M);
        }

        [Test]
        public void DiscountRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<IDiscountRepository>();
            var secondRepo = ObjectFactory.GetInstance<IDiscountRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
