﻿using Microsoft.Practices.Unity;
using Bluepill.Picture;
using Bluepill.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bluepill.Web.Gateways;
using Bluepill.Storage;
using Bluepill.Dropbox;

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
            container.RegisterType<IConfigurationReader, ConfigurationReader>();
            container.RegisterType<IQueryBuilder, QueryBuilder>();
            container.RegisterType<IPacker, Packer>();
            container.RegisterType<IAttic, Attic>();
            container.RegisterType<ICookieGateway, CookieGateway>();
            container.RegisterType<IFacetReader, FacetReader>();
            container.RegisterType<IApiRequestParameters, ApiRequestParameters>();
            container.RegisterType<IApiRequest, ApiRequest>();
            container.RegisterType<ISignature, Signature>();

            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<Web.Areas.Application.Models.Validators.AuthenticationModelValidator>()
                .ForEach(result => container.RegisterType(result.InterfaceType, result.ValidatorType));
        }
    }
}