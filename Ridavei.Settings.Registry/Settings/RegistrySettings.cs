using System;
using System.Collections.Generic;

using Ridavei.Settings.Base;

using Microsoft.Win32;

namespace Ridavei.Settings.Registry.Settings
{
    /// <summary>
    /// Windows Registry settings class that uses <see cref="Microsoft.Win32.Registry"/> for storing keys and values.
    /// </summary>
    internal class RegistrySettings : ASettings
    {
        private const string BaseSubKeyName = "Software\\Ridavei\\Settings\\";

        private RegistryKey _registryKey;

        /// <summary>
        /// Retrieves the <see cref="ASettings"/> object for the specifed dictionary name.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <param name="registryBase">Registry base</param>
        /// <param name="settings">Retrieved settings</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        public static bool TryGetSettings(string dictionaryName, RegistryKey registryBase, out ASettings settings)
        {
            settings = new RegistrySettings(dictionaryName);

            return ((RegistrySettings)settings).TryGetRegistry(dictionaryName, registryBase, out _);
        }

        /// <summary>
        /// Creates registry sub key for the specific dictionary name.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <param name="registryBase">Registry base</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        public static RegistrySettings CreateSettings(string dictionaryName, RegistryKey registryBase)
        {
            var res = new RegistrySettings(dictionaryName);

            if (!res.TryGetRegistry(dictionaryName, registryBase, out var subKeyName))
                res.CreateRegistry(registryBase, subKeyName);

            return res;
        }

        /// <summary>
        /// The default constructor for <see cref="RegistrySettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        internal RegistrySettings(string dictionaryName) : base(dictionaryName) { }

        /// <summary>
        /// Sets a new value for the specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected override void SetValue(string key, string value)
        {
            _registryKey.SetValue(key, value, RegistryValueKind.String);
            _registryKey.Flush();
        }

        /// <summary>
        /// Returns true and the value for the specific key if exists, else return false and null value.
        /// </summary>
        /// <param name="key">Settings key</param>
        /// <param name="value">Returned value</param>
        /// <returns>True if key exists, else false.</returns>
        protected override bool TryGetValue(string key, out string value)
        {
            value = (string)_registryKey.GetValue(key);
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Returns all keys with their values.
        /// </summary>
        protected override IReadOnlyDictionary<string, string> GetAllValues()
        {
            var res = new Dictionary<string, string>();
            foreach (var valueName in _registryKey.GetValueNames())
                res.Add(valueName, (string)_registryKey.GetValue(valueName));
            return res;
        }

        /// <summary>
        /// Tries to get the registry sub key for the specific dictionary name.
        /// </summary>
        /// <param name="registryBase">Registry base</param>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <param name="subKeyName">Retrieved sub key name</param>
        /// <returns>True if the registry exists or false if not</returns>
        private bool TryGetRegistry(string dictionaryName, RegistryKey registryBase, out string subKeyName)
        {
            subKeyName = string.Concat(BaseSubKeyName, dictionaryName);
            _registryKey = registryBase.OpenSubKey(subKeyName, true);
            return _registryKey != null;
        }

        /// <summary>
        /// Creates the registry sub key for the specific dictionary name.
        /// </summary>
        /// <param name="registryBase">Registry base</param>
        /// <param name="subKeyName">Sub key name</param>
        private void CreateRegistry(RegistryKey registryBase, string subKeyName)
        {
            _registryKey = registryBase.CreateSubKey(subKeyName, true);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (_registryKey != null)
                {
                    _registryKey.Dispose();
                    _registryKey = null;
                }
        }
    }
}
