namespace Ridavei.Settings.Registry.Enums
{
    /// <summary>
    /// Enum to choose one of the registry base.
    /// </summary>
    public enum RegistryKeyType
    {
        /// <summary>
        /// Reads from the HKEY_CURRENT_USER registry base.
        /// </summary>
        CurrentUser = 1,
        /// <summary>
        /// Reads from the HKEY_CURRENT_USER registry base.
        /// </summary>
        LocalMachine = 2
    }
}
