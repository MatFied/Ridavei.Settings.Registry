namespace Ridavei.Settings.Registry.Enums
{
    /// <summary>
    /// Enum to choose one of the registry bases.
    /// </summary>
    public enum RegistryKeyType
    {
        /// <summary>
        /// Reads from the HKEY_CURRENT_USER registry base.
        /// </summary>
        CurrentUser = 1,
        /// <summary>
        /// Reads from the HKEY_LOCAL_MACHINE registry base.
        /// </summary>
        LocalMachine = 2
    }
}
