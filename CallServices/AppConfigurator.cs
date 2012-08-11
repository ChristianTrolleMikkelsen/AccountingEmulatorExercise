using CallServices.Repositories;
using CallServices.Validator.Rules;
using StructureMap;

namespace CallServices
{
    public class AppConfigurator
    {
        public void Initialize()
        {
            ObjectFactory.Configure(x =>
                                         {
                                             x.For<ICallRepository>().Singleton();
 
                                             x.Scan(scan =>
                                                        {
                                                            scan.AssemblyContainingType<ICallRegistration>();
                                                            scan.WithDefaultConventions();
                                                            scan.AddAllTypesOf<IRule>();
                                                        });
                                         });
        }
    }
}
