﻿using System;
using System.Collections.Generic;
using AccountingMachine.Models;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using TestHelpers;

namespace UnitTest
{
    [TestFixture]
    class BillTest
    {
        [Test]
        public void Must_Be_Able_To_Print_Bill()
        {
            var sub = SubscriptionHelper.CreateSubscriptionWithDefaultCustomer("55555555");
            var records = new List<Record>
                              {
                                  new Record("55555555", DateTime.Now, "Standard", "Call from 55555555 to 66666666", 12.5M)
                              };
            var discount = new Discount(CustomerStatus.VIP, 0.5M);

            var bill = new Bill(sub,records,discount);

            bill.ToString().Should().NotBeNullOrEmpty();
        }
    }
}
