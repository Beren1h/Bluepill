using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Framework.IoC
{
    public interface IIoCRegistry
    {
        void Register(IUnityContainer container);
    }
}