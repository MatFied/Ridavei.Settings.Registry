using System;
using System.Collections.Generic;

using Ridavei.Settings.Base;

namespace Ridavei.Settings.Registry.Settings
{
    /// <summary>
    /// 
    /// </summary>
    internal class RegistrySettings : ASettings
    {
        private readonly Dictionary<string, string> keyValues = new Dictionary<string, string>();

        /// <summary>
        /// The default constructor for <see cref="RegistrySettings"/> class.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <exception cref="ArgumentNullException">Throwed when the name of the dictionary is null, empty or whitespace.</exception>
        public RegistrySettings(string dictionaryName) : base(dictionaryName) { }

        /// <summary>
        /// Sets a new value for the specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected override void SetValue(string key, string value)
        {
        }

        /// <summary>
        /// Returns true and the value for the specific key if exists, else return false and null value.
        /// </summary>
        /// <param name="key">Settings key</param>
        /// <param name="value">Returned value</param>
        /// <returns>True if key exists, else false.</returns>
        protected override bool TryGetValue(string key, out string value)
        {
            value = null;
            return false;
        }

        /// <summary>
        /// Returns all keys with their values.
        /// </summary>
        protected override IReadOnlyDictionary<string, string> GetAllValues()
        {
            return null;
        }
    }
}
