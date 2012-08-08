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
                                             x.For<ILocalServiceChargeRepository>().Singleton();
                                             x.For<IForeignServiceChargeRepository>().Singleton();
                                             x.For<IRecordRepository>().Singleton();

                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<IServiceCall>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
