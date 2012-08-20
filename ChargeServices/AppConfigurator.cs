using ChargeServices.Repositories;
using StructureMap.Configuration.DSL;

namespace ChargeServices
{
    public class AppConfigurator : Registry
    {
        public AppConfigurator()
        {
            For<IServiceChargeRepository>().Singleton();
 
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<IServiceChargeSearch>();
                scanner.WithDefaultConventions();
            });
        }
    }
}
