using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppSettingsEditor
{
    internal static class AppSettingsUtil
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Check the existence of the specified settings container
        /// </summary>
        /// <param name="path">The path of the container</param>
        /// <returns>Return ApplicationDataContainer if container exists</returns>
        public static ApplicationDataContainer ContainerExists(string containerPath)
        {
            ApplicationDataContainer container = string.IsNullOrEmpty(containerPath) ? localSettings : null;
            try
            {
                string[] names = containerPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                for (int index = 0; index < names.Length; ++index)
                {
                    container = container == null ? localSettings.CreateContainer(names[index], ApplicationDataCreateDisposition.Existing) :
                                                    container.CreateContainer(names[index], ApplicationDataCreateDisposition.Existing);

                }
            }
            catch
            {
                //Catch the exception if the specified container does not exist when disposition equal to "Exist"
                return null;
            }
            return container;
        }

        /// <summary>
        /// Create and opens the specified settings container
        /// </summary>
        public static ApplicationDataContainer CreateContainer(string containerPath)
        {
            // Containers can be nested up to 32 levels deep.
            ApplicationDataContainer container = string.IsNullOrEmpty(containerPath) ? localSettings : null;
            try
            {
                string[] names = containerPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                for (int index = 0; index < names.Length; ++index)
                {
                    container = container == null ? localSettings.CreateContainer(names[index], ApplicationDataCreateDisposition.Always) :
                                                    container.CreateContainer(names[index], ApplicationDataCreateDisposition.Always);

                }
            }
            catch
            {
                //Cache the exception if the specified container does not exist when disposition equal to "Exist"
                return null;
            }
            return container;
        }

        public static object StringToObject(string value)
        {
            object obj;
            if (string.IsNullOrEmpty(value))
            {
                obj = "";
            }
            else if (value.ToLowerInvariant() == "false")
            {
                obj = false;
            }
            else if (value.ToLowerInvariant() == "true")
            {
                obj = true;
            }
            else
            {
                int intValue;
                double dValue;
                if (int.TryParse(value, out intValue))
                {
                    obj = intValue;
                }
                else if (double.TryParse(value, out dValue))
                {
                    obj = dValue;
                }
                else
                {
                    if (value.StartsWith("\""))
                        value = value.Substring(1);
                    if (value.EndsWith("\""))
                        value = value.Substring(0, value.Length - 1);

                    obj = value;
                }
            }

            return obj;
        }
    }
}
