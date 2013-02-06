using FluentValidation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Areas.Authentication.Models.Validators;

namespace Web.Framework.IoC
{
    public class IoCRegistry : IIoCRegistry
    {
        public void Register(IUnityContainer container)
        {
            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<Web.Areas.Authentication.Models.Validators.SignOnModelValidator>()
                .ForEach(result => container.RegisterType(result.InterfaceType, result.ValidatorType));


            //var x = FluentValidation.AssemblyScanner

            //    foreach(var item in x){
            //        container.RegisterType(item.InterfaceType, item.ValidatorType);
            //    }

            //container.RegisterType<IValidator, SignOnModelValidator>();
        }
    }
}