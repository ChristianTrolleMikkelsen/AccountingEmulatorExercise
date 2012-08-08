using PhoneSubscriptionCalculator.Models;
using PhoneSubscriptionCalculator.Repositories;
using PhoneSubscriptionCalculator.Service_Calls;
using StructureMap;

namespace PhoneSubscriptionCalculator
{
    public class AppConfigurator
    {
        public void Initialize()
        {
            SetUpStructureMap();
        }

        private void SetUpStructureMap()
        {
            ObjectFactory.Initialize(x =>
                                         {
                                             x.For<ICallRepository>().Singleton();
                                             x.For<IServiceRepository>().Singleton();
                                             x.For<ISubscriptionRepository>().Singleton();

                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<ICall>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
