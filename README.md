# Ridavei.Settings.Registry

Builder extension to store settings keys and values in Windows Registry.

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
            ISettings settings = SettingsBuilder
                .CreateBuilder()
                .UseRegistryManager()
                .GetSettings("DictionaryName");

            //You can use settings.Get("ExampleKey", "DefaultValue") if you want to retrieve the default value if the key doesn't exists.
            string value = settings.Get("ExampleKey");
            settings.Set("AnotherKey", "NewValue");
        }
    }
}
```