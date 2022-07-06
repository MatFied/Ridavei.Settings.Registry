# Ridavei.Settings.Registry

Builder extension to store settings keys and values in Windows Registry.\
To use the "HKEY_LOCAL_MACHINE" registry base the program needs to run under administration privilages.

## Examples of use

```csharp
using Ridavei.Settings;
using Ridavei.Settings.Interface;
using Ridavei.Settings.Registry;

namespace TestProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (SettingsBuilder settingsBuilder = SettingsBuilder.CreateBuilder())
            {
                ISettings settings = settingsBuilder
                    //You can call UseRegistryManager(RegistryKeyType.LocalMachine) if you need to use "HKEY_LOCAL_MACHINE"
                    .UseRegistryManager()
                    .GetSettings("DictionaryName");

                //You can use settings.Get("ExampleKey", "DefaultValue") if you want to retrieve the default value if the key doesn't exists.
                string value = settings.Get("ExampleKey");
                settings.Set("AnotherKey", "NewValue");
            }
        }
    }
}
```