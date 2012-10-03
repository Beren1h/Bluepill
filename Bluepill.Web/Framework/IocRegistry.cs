using Microsoft.Practices.Unity;
using Bluepill.Picture;
using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bluepill.Web.Gateways;
using Bluepill.Storage;

namespace Bluepill.Web.Framework
{
    public class IocRegistry : IIocRegistry
    {
        public void Register(IUnityContainer container)
        {
            container.RegisterType<IPrincipalService, BluePillPrincipalService>(new HttpContextLifetimeManager<IPrincipalService>());
            container.RegisterType<IAuthenticationGateway, AuthenticationGateway>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBluePillUserStore, BluePillUserStore>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPictureGateway, PictureGateway>();
            container.RegisterType<IResize, Resize>();
            container.RegisterType<IFacetCollectionReader, FacetCollectionReader>();
            container.RegisterType<IConfigurationReader, ConfigurationReader>();
            container.RegisterType<IQueryBuilder, QueryBuilder>();

            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<Web.Areas.Application.Models.Validators.AuthenticationModelValidator>()
                .ForEach(result => container.RegisterType(result.InterfaceType, result.ValidatorType));
        }
    }
}