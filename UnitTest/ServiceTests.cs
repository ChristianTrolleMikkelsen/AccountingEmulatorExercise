using System;
using Core;
using Core.ServiceCalls;
using Core.Services;
using FluentAssertions;
using NUnit.Framework;
using SubscriptionService.Services;

namespace UnitTest
{
    [TestFixture]
    class ServiceTests
    {
        [Test]
        public void Should_Support_VoiceCall_Calls()
        {
            var service = new Service("",ServiceType.Voice);

            service.HasSupportForCallType(ServiceCallType.Voice).Should().BeTrue();
        }
         
        [Test]
        public void Should_Not_Support_Other_Calls_Than_Voice_Calls()
        {
            var service = new Service("", ServiceType.Voice);

            service.HasSupportForCallType(ServiceCallType.SMS).Should().BeFalse();
        }

        [Test]
        public void Should_Support_SMSCall_Calls()
        {
            var service = new Service("", ServiceType.SMS);

            service.HasSupportForCallType(ServiceCallType.SMS).Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls_Then_SMS_Calls()
        {
            var service = new Service("", ServiceType.SMS);

            service.HasSupportForCallType(ServiceCallType.DataTransfer).Should().BeFalse();
        }

        [Test]
        public void Should_Support_DataTransfer_Calls()
        {
            var service = new Service("", ServiceType.DataTransfer);

            service.HasSupportForCallType(ServiceCallType.DataTransfer).Should().BeTrue();
        }

        [Test]
        public void Should_Not_Support_Other_Calls_Than_DataTransfer_Calls()
        {
            var service = new Service("", ServiceType.DataTransfer);

            service.HasSupportForCallType(ServiceCallType.Voice).Should().BeFalse();
        }
    }
}
