using System;

using Ridavei.Settings.Registry.Enums;
using Ridavei.Settings.Registry.Manager;

namespace Ridavei.Settings.Registry
{
    /// <summary>
    /// Class used to extend <see cref="SettingsBuilder"/>.
    /// </summary>
    public static class RegistryBuilderExt
    {
        /// <summary>
        /// Allows to use <see cref="RegistryManager"/> as the manager class.
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <param name="registryKeyType">Registry base (default <see cref="RegistryKeyType.CurrentUser"/>)</param>
        /// <returns>Builder</returns>
        /// <exception cref="NotSupportedException">Throwed when the selected <see cref="RegistryKeyType"/> was not supported.</exception>
        public static SettingsBuilder UseRegistryManager(this SettingsBuilder builder, RegistryKeyType registryKeyType = RegistryKeyType.CurrentUser)
        {
            return builder.SetManager(new RegistryManager(registryKeyType));
        }
    }
}
