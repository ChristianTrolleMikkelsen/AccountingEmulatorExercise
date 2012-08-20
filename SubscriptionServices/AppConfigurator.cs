using StructureMap.Configuration.DSL;
using SubscriptionServices.Repositories;

namespace SubscriptionServices
{
    public class AppConfigurator : Registry
    {
        public AppConfigurator()
        {
            For<ISubscriptionRepository>().Singleton();
 
            Scan(scan =>
                    {
                        scan.AssemblyContainingType<ISubscriptionSearch>();
                        scan.WithDefaultConventions();
                    });
        }
    }
}
