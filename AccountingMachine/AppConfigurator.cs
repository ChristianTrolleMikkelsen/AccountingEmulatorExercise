﻿using AccountingMachine.Repositories;
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
                                             x.For<ILocalServiceChargeRepository>().Singleton();
                                             x.For<IForeignServiceChargeRepository>().Singleton();
                                             x.For<IRecordRepository>().Singleton();

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
