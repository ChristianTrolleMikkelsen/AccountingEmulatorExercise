using CallCentral.Repositories;
using StructureMap;
using SubscriptionService;

namespace CallCentral
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
                                             x.For<ICallRepository>().Singleton();
 
                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<ICallCentral>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
