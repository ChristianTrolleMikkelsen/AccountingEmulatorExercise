using AccountingMachine.Repositories;
using Core.ServiceCalls;
using StructureMap.Configuration.DSL;

namespace AccountingMachine
{
    public class AppConfigurator : Registry
    {
        public AppConfigurator()
        {
            For<IRecordRepository>().Singleton();
            For<IDiscountRepository>().Singleton();

            Scan(scan =>
                    {
                        scan.AssemblyContainingType<IAccountingMachine>();
                        scan.AssemblyContainingType<IServiceCall>();
                        scan.WithDefaultConventions();
                    });
        }
    }
}
