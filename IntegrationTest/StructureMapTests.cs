﻿using NUnit.Framework;
using StructureMap;

namespace IntegrationTest
{
    [TestFixture]
    class StructureMapTests
    {
        [Test]
        public void Must_Always_Have_A_Valid_Configuration()
        {
            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}
