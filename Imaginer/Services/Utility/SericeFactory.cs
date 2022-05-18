using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;


namespace Imaginer.Services.Utility
{
    public class ServiceFactory
    {
        static IUnityContainer container;
        public static void LoadUnityConfig()
        {
            container = new UnityContainer().LoadConfiguration();
        }
        public static T GetInstance<T>()
        {
            if (container == null)
            {
                LoadUnityConfig();
            }
            var obj = container.Resolve<T>();

            return obj;
        }
    }
}
