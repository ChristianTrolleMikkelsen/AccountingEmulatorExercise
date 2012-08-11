using StructureMap;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
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
                                             x.For<IServiceChargeRepository>().Singleton();
                                             x.For<IServiceRepository>().Singleton();
                                             x.For<ISubscriptionRepository>().Singleton();
 
                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<IService>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
