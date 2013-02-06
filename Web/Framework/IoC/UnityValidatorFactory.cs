using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Framework.IoC
{
    public class UnityValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return DependencyResolver.Current.GetService(validatorType) as IValidator;
        }
    }
}