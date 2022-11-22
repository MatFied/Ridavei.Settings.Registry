# Ridavei.Settings.Registry

### Latest release
[![NuGet Badge Ridavei.Settings.Registry](https://buildstats.info/nuget/Ridavei.Settings.Registry)](https://www.nuget.org/packages/Ridavei.Settings.Registry)

Builder extension to store settings keys and values in Windows Registry.\
To use the "HKEY_LOCAL_MACHINE" registry base the program needs to run under administration privilages.

## Examples of use

```csharp
using Ridavei.Settings;
using Ridavei.Settings.Base;

using Ridavei.Settings.Registry;

namespace TestProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            SettingsBuilder builder = SettingsBuilder
                .CreateBuilder()
                //You can call UseRegistryManager(RegistryKeyType.LocalMachine) if you need to use "HKEY_LOCAL_MACHINE"
                .UseRegistryManager();
            using (ASettings settings = builder.GetSettings("DictionaryName"))
            {
                //You can use settings.Get("ExampleKey", "DefaultValue") if you want to retrieve the default value if the key doesn't exists.
                string value = settings.Get("ExampleKey");
                settings.Set("AnotherKey", "NewValue");
            }
        }
    }
}
```