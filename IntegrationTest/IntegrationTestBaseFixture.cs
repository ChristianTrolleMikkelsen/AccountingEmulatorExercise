using NUnit.Framework;
using StructureMap;

namespace IntegrationTest
{
    [TestFixture]
    internal abstract class IntegrationTestBaseFixture
    {
        [SetUp]
        public void InitializeApplication()
        {
            ObjectFactory.Initialize(x => x.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory();
                scanner.LookForRegistries();
            }));
        }
    }
}
