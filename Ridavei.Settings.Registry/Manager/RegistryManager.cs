using System;

using Ridavei.Settings.Registry.Enums;
using Ridavei.Settings.Registry.Settings;

using Ridavei.Settings.Base;

using Microsoft.Win32;

namespace Ridavei.Settings.Registry.Manager
{
    /// <summary>
    /// Windows Registry manager class used to retrieve settings using <see cref="RegistrySettings"/>.
    /// </summary>
    internal class RegistryManager : AManager
    {
        private readonly RegistryKey _registerKey;

        /// <summary>
        /// The default constructor for <see cref="RegistryManager"/> class.
        /// </summary>
        /// <param name="registryKeyType">Registry base</param>
        public RegistryManager(RegistryKeyType registryKeyType) : base()
        {
            _registerKey = GetRegistryBase(registryKeyType);
        }

        /// <inheritdoc/>
        protected override bool TryGetSettingsObject(string dictionaryName, out ASettings settings)
        {
            return RegistrySettings.TryGetSettings(dictionaryName, _registerKey, out settings);
        }

        /// <inheritdoc/>
        protected override ASettings CreateSettingsObject(string dictionaryName)
        {
            return RegistrySettings.CreateSettings(dictionaryName, _registerKey);
        }

        /// <summary>
        /// Return the <see cref="RegistryKey"/> for the selected <see cref="RegistryKeyType"/>.
        /// </summary>
        /// <param name="registerKeyType">Registry base</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Throwed when the selected <see cref="RegistryKeyType"/> was not supported.</exception>
        private RegistryKey GetRegistryBase(RegistryKeyType registerKeyType)
        {
            switch (registerKeyType)
            {
                case RegistryKeyType.LocalMachine:
                    return Microsoft.Win32.Registry.LocalMachine;
                case RegistryKeyType.CurrentUser:
                    return Microsoft.Win32.Registry.CurrentUser;
                default:
                    throw new NotSupportedException($"The provided value for enum RegisterKeyType is not supported (value: {registerKeyType}).");
            };
        }
    }
}
