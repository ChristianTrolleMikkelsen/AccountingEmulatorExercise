using CallServices.Repositories;
using CallServices.Validator.Rules;
using StructureMap.Configuration.DSL;

namespace CallServices
{
    public class AppConfigurator : Registry
    {
        public AppConfigurator()
        {
            For<ICallRepository>().Singleton();

            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ICallRegistration>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf<IRule>();
            });
        }
    }
}
