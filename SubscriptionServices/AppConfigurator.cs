using StructureMap;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public class AppConfigurator
    {
        public void Initialize()
        {
            ObjectFactory.Configure(x =>
                                        {
                                             x.For<ISubscriptionRepository>().Singleton();
 
                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<ISubscriptionSearch>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
