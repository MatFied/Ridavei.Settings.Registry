using Ridavei.Settings.Base;
using Ridavei.Settings.InMemory.Settings;
using Ridavei.Settings.Interface;

namespace Ridavei.Settings.InMemory.Manager
{
    /// <summary>
    /// In memory manager class used to retrieve settings using <see cref="RegistrySettings"/>.
    /// </summary>
    internal class RegistryManager : AManager
    {
        /// <summary>
        /// The default constructor for <see cref="RegistryManager"/> class.
        /// </summary>
        public RegistryManager() : base() { }

        /// <summary>
        /// Retrieves the <see cref="ISettings"/> object for the specifed dictionary name.
        /// </summary>
        /// <param name="dictionaryName">Name of the dictionary</param>
        /// <returns>Settings</returns>
        protected override ISettings GetSettingsObject(string dictionaryName)
        {
            return new RegistrySettings(dictionaryName);
        }
    }
}
