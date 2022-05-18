using Imaginer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imaginer.Services
{
    public static class ResourceMangagerService
    {
        public static string GetResourceString(int resourceKey)
        {
            string resourceString = string.Empty;
            if (!string.IsNullOrEmpty(resourceKey.ToString()))
            {

                resourceString =  LabelConstants.labels[resourceKey];
            }
            else
            {
                throw new ArgumentException(string.Format("Resource {0} not found", resourceKey));
            }

            return resourceString;

        }
    }
}
