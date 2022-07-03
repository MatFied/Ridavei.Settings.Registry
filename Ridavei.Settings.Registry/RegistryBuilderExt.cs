using System;

using Ridavei.Settings.InMemory.Manager;

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
        /// <returns>Builder</returns>
        public static SettingsBuilder UseRegistryManager(this SettingsBuilder builder)
        {
            return builder.SetManager(new RegistryManager());
        }
    }
}
