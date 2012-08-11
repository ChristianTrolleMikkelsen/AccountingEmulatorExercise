using AccountingMachine.Repositories;
using Core.ServiceCalls;
using StructureMap;

namespace AccountingMachine
{
    public class AppConfigurator
    {
        public void Initialize()
        {
            ObjectFactory.Initialize(x =>
                                         {
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
