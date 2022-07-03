using System;

using NUnit.Framework;
using Shouldly;

namespace Ridavei.Settings.Registry.Tests
{
    [TestFixture]
    public class RegistryBuilderExtTests
    {
        [Test]
        public static void UseRegistryManager__RetrieveSettings()
        {
            Should.NotThrow(() =>
            {
                SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager()
                    .GetSettings("Test")
                    .ShouldNotBeNull();
            });
        }

        [Test]
        public static void UseRegistryManager_Set__NoException()
        {
            Should.NotThrow(() =>
            {
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager()
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                settings.Set("T1", "T2");
            });
        }

        [Test]
        public static void UseRegistryManager_Get__GetValue()
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager()
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                settings.Set(key, value);
                settings.Get(key).ShouldBe(value);
            });
        }

        [Test]
        public static void UseRegistryManager_GetAll_Empty__GetEmptyDictionary()
        {
            Should.NotThrow(() =>
            {
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager()
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                var dict = settings.GetAll();
                dict.ShouldNotBeNull();
                dict.Count.ShouldBe(0);
            });
        }

        [Test]
        public static void UseRegistryManager_GetAll__GetDictionary()
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager()
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                settings.Set(key, value);
                var dict = settings.GetAll();
                dict.ShouldNotBeNull();
                dict.Count.ShouldBe(1);
                dict.ContainsKey(key).ShouldBeTrue();
                dict[key].ShouldBe(value);
            });
        }
    }
}
