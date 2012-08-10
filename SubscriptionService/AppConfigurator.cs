using StructureMap;
using StructureMap.Pipeline;
using SubscriptionService.Repositories;

namespace SubscriptionService
{
    public class AppConfigurator
    {
        public void Initialize()
        {
            SetUpStructureMap();
        }

        private void SetUpStructureMap()
        {
            ObjectFactory.Configure(x =>
                                        {
                                             x.For<ISubscriptionService>().Singleton();
                                             x.For<IServiceChargeRepository>().Singleton();
                                             x.For<IServiceRepository>().Singleton();
                                             x.For<ISubscriptionRepository>().Singleton();
 
                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<ISubscriptionService>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
