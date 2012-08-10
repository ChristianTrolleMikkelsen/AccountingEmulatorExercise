using CallCentral.Repositories;
using CallCentral.Validator.Rules;
using StructureMap;

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
                                                            scan.AddAllTypesOf<IRule>();
                                                        });
                                         });
        }
    }
}
