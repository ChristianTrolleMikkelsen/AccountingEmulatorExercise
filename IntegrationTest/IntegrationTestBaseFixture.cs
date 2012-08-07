using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PhoneSubscriptionCalculator;

namespace IntegrationTest
{
    [TestFixture]
    internal abstract class IntegrationTestBaseFixture
    {
        [SetUp]
        public void InitializeApplication()
        {
            new AppConfigurator().Initialize();
        }
    }
}
