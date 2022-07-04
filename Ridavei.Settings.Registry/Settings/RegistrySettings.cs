using System;
using System.Collections.Generic;

using Ridavei.Settings.Registry.Enums;

using Ridavei.Settings.Base;

using Microsoft.Win32;

namespace Ridavei.Settings.Registry.Settings
{
    /// <summary>
    /// 
    /// </summary>
    internal class RegistrySettings : ASettings
    {
        private const string SubKeyName = "Software\\Ridavei\\Settings";
        private readonly RegistryKey _registryKey;

        /// <summary>
        /// The default constructor for <see cref="RegistrySettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <param name="registerKey">Registry base</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        /// <exception cref="NotSupportedException">Throwed when the selected <see cref="RegistryKeyType"/> was not supported.</exception>
        public RegistrySettings(string dictionaryName, RegistryKey registerKey) : base(dictionaryName)
        {
            _registryKey = OpenDesiredSubKey(registerKey, dictionaryName);
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
            value = _registryKey.GetValue(key) as string;
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Returns all keys with their values.
        /// </summary>
        protected override IReadOnlyDictionary<string, string> GetAllValues()
        {
            var res = new Dictionary<string, string>();
            foreach (var valueName in _registryKey.GetValueNames())
            {
                var value = _registryKey.GetValue(valueName) as string;
                res.Add(valueName, value);
            }
            return res;
        }

        private RegistryKey OpenDesiredSubKey(RegistryKey registryKey, string dictionaryName)
        {
            string subKeyName = string.Concat(SubKeyName, "\\", dictionaryName);
            var res = registryKey.OpenSubKey(subKeyName, true);
            if (res == null)
                res = registryKey.CreateSubKey(subKeyName, true);

            return res;
        }
    }
}
