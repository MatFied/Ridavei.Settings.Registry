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
        private const string BaseSubKeyName = "Software\\Ridavei\\Settings";
        private readonly RegistryKey _registryKey;

        /// <summary>
        /// The default constructor for <see cref="RegistrySettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <param name="registryKey">Registry base</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        public RegistrySettings(string dictionaryName, RegistryKey registryKey) : base(dictionaryName)
        {
            _registryKey = OpenSubKey(registryKey, dictionaryName);
        }

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
        /// Opens or creates registry sub key for the specific dictionary name.
        /// </summary>
        /// <param name="registryKey">Registry base</param>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <returns></returns>
        private RegistryKey OpenSubKey(RegistryKey registryKey, string dictionaryName)
        {
            string subKeyName = string.Concat(BaseSubKeyName, "\\", dictionaryName);
            var res = registryKey.OpenSubKey(subKeyName, true);
            if (res == null)
                res = registryKey.CreateSubKey(subKeyName, true);

            return res;
        }

        public override void Dispose()
        {
            _registryKey.Dispose();

            base.Dispose();
        }
    }
}
