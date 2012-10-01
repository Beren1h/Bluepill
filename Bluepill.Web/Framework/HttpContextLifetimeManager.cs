using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bluepill.Web.Framework
{
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly Type _typeBeingResolved;
        private string _itemKey;
        private HttpContextBase _httpContextCurrent;

        /// <summary>
        /// Injects HttpContext.Current when the base is not set during testing
        /// </summary>
        public HttpContextBase CurrentHttpContext
        {
            get { return _httpContextCurrent ?? new HttpContextWrapper(HttpContext.Current); }
            set { _httpContextCurrent = value; }
        }

        /// <summary>
        /// Constructor that takes an HttpContext abstraction for testing
        /// </summary>
        /// <param name="httpContextBase">The http context wrapper</param>
        /// <param name="typeBeingResolved"></param>
        public HttpContextLifetimeManager(Type typeBeingResolved)
        {
            _typeBeingResolved = typeBeingResolved;
        }

        /// <summary>
        /// Retrieve a request-level singleton from HttpContext
        /// </summary>
        /// <returns>The object</returns>
        public override object GetValue()
        {
            return CurrentHttpContext.Items[_typeBeingResolved.AssemblyQualifiedName];
        }

        /// <summary>
        /// Remove a request-level singleton from HttpContext
        /// </summary>
        public override void RemoveValue()
        {
            CurrentHttpContext.Items.Remove(_typeBeingResolved.AssemblyQualifiedName);
        }

        /// <summary>
        /// Override a value in HttpContext's items
        /// </summary>
        /// <param name="newValue"></param>
        public override void SetValue(object newValue)
        {
            CurrentHttpContext.Items[_typeBeingResolved.AssemblyQualifiedName] = newValue;
        }

        /// <summary>
        /// Remove the instance from HttpContext
        /// </summary>
        public void Dispose()
        {
            RemoveValue();
        }
    }

    /// <summary>
    /// Limits the lifetime of an object to a single request
    /// </summary>
    /// <typeparam name="T">The type being resolved</typeparam>
    public class HttpContextLifetimeManager<T> : HttpContextLifetimeManager
    {
        /// <summary>
        /// Constructor that takes an HttpContext abstraction for testing
        /// </summary>
        /// <param name="httpContextBase">The http context wrapper</param>
        public HttpContextLifetimeManager() :
            base(typeof(T))
        {
        }
    }
}