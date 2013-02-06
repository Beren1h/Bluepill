using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Framework.IoC
{
    public class IoCInitializer
    {
        public static IUnityContainer InitializeContainer()
        {
            var container = new UnityContainer();

            RegisterApplicationTypes(container);

            return container;
        }

        private static void RegisterApplicationTypes(UnityContainer container)
        {
            var allTypes = GetAllTypes();

            Type interfaceType = typeof(IIoCRegistry);

            IEnumerable<Type> registries = allTypes.Where(x => interfaceType.IsAssignableFrom(x) && x.IsClass);

            foreach (Type registry in registries)
            {
                var reg = (IIoCRegistry)Activator.CreateInstance(registry);
                reg.Register(container);
            }
        }

        private static IEnumerable<Type> GetAllTypes()
        {
            var types = new List<Type>();

            ICollection assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInAssembly;

                try
                {
                    typesInAssembly = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    typesInAssembly = ex.Types;
                }

                types.AddRange(typesInAssembly);
            }

            return types;
        }
    }
}
