﻿using System;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    class SMSServiceTests
    {
        [Test]
        public void Should_Support_SMSCall_Calls()
        {
            var service = new SMSService("");

            service.HasSupportForCall(new SMSServiceCall("", DateTime.Now, 128, "", "", ""))
                .Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls()
        {
            var service = new SMSService("");

            service.HasSupportForCall(new DummyServiceCall())
                .Should().BeFalse();
        }

        internal class DummyServiceCall : IServiceCall
        {
            public string PhoneNumber
            {
                get { return "I Am A Dummy"; }
            }

            public string FromCountry
            {
                get { throw new NotImplementedException(); }
            }

            public string ToCountry
            {
                get { throw new NotImplementedException(); }
            }

            public DateTime GetStartTime()
            {
                throw new NotImplementedException();
            }

            public decimal GetUnitSize()
            {
                throw new NotImplementedException();
            }

            public bool IsValid()
            {
                throw new NotImplementedException();
            }
        }
    }
}
