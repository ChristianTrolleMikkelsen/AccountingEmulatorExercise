using AccountingMachine.Generators;
using AccountingMachine.Repositories;
using Core.Repositories;
using Core.ServiceCalls;
using StructureMap;

namespace AccountingMachine
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
                                             x.For<IServiceRepository>().Singleton();
                                             x.For<ISubscriptionRepository>().Singleton();
                                             x.For<IServiceChargeRepository>().Singleton();
                                             x.For<IRecordRepository>().Singleton();
                                             x.For<IDiscountRepository>().Singleton();

                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<IAccountingMachine>();
                                                            scan.AssemblyContainingType<IServiceCall>();
                                                            scan.WithDefaultConventions();
                                                        });
                                         });
        }
    }
}
