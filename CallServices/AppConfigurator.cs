using CallServices.Repositories;
using CallServices.Validator.Rules;
using StructureMap;

namespace CallServices
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
                                                            scan.AssemblyContainingType<ICallRegistration>();
                                                            scan.WithDefaultConventions();
                                                            scan.AddAllTypesOf<IRule>();
                                                        });
                                         });
        }
    }
}
