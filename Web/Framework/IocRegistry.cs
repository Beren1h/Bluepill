using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Gateways;

namespace Web.Framework
{
    public class IocRegistry : IIocRegistry
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IPrincipalService, BluePillPrincipalService>(new HttpContextLifetimeManager<IPrincipalService>());
            container.RegisterType<IAuthenticationGateway, AuthenticationGateway>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBluePillUserStore, BluePillUserStore>(new ContainerControlledLifetimeManager());

            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<Web.Areas.Application.Models.Validators.AuthenticationModelValidator>()
                .ForEach(result => container.RegisterType(result.InterfaceType, result.ValidatorType));
        }
    }
}