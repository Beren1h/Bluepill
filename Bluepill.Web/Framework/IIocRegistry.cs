using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public interface IIocRegistry
    {
        void Register(IUnityContainer container);
    }
}