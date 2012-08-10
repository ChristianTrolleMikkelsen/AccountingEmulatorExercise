﻿using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;
using SubscriptionService;
using SubscriptionService.Repositories;
using TestHelpers;

namespace IntegrationTest.Repositories
{
    [TestFixture]
    class SubscriptionRepositoryTests : IntegrationTestBaseFixture
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
        public void Should_Add_A_Subscription_To_The_Repository()
        {
            var repo = ObjectFactory.GetInstance<ISubscriptionRepository>();

            repo.SaveSubscription(subscription);

            repo.GetSubscriptionForPhoneNumber(subscription.PhoneNumber).Should().NotBeNull();
        }

        [Test]
        public void SubscriptionRepository_Must_Be_A_Singleton_To_Allow_Sharing_Of_Repository_Until_MVC_Application_Can_Be_Setup()
        {
            var firstRepo = ObjectFactory.GetInstance<ISubscriptionRepository>();
            var secondRepo = ObjectFactory.GetInstance<ISubscriptionRepository>();

            firstRepo.Should().Be(secondRepo);
        }
    }
}
