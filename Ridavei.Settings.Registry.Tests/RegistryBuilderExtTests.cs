using System;
using Microsoft.Win32;
using NUnit.Framework;
using Ridavei.Settings.Registry.Enums;
using Shouldly;

namespace Ridavei.Settings.Registry.Tests
{
    [TestFixture]
    public class RegistryBuilderExtTests
    {
        private const string SubKeyName = "Software\\Ridavei";

        [SetUp]
        public void SetUp()
        {
            RemoveSubKeyTrees();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveSubKeyTrees();
        }

        private void RemoveSubKeyTrees()
        {
            RemoveSubKeyTree(Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SubKeyName, true));
            RemoveSubKeyTree(Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SubKeyName, true));
        }

        private void RemoveSubKeyTree(RegistryKey registryKey)
        {
            if (registryKey == null)
                return;
            using (registryKey)
            {
                registryKey.DeleteSubKeyTree(string.Empty, false);
                registryKey.Flush();
            }
        }

        [Test]
        public static void UseRegistryManager_NonSupportedType__RaiseException()
        {
            Should.Throw<NotSupportedException>(() =>
            {
                SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(RegistryKeyType.LocalMachine | RegistryKeyType.CurrentUser);
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public static void UseRegistryManager__RetrieveSettings(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(registryKeyType)
                    .GetSettings("Test")
                    .ShouldNotBeNull();
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public static void UseRegistryManager_Set__NoException(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(registryKeyType)
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                settings.Set("T1", "T2");
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public static void UseRegistryManager_Get__GetValue(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(registryKeyType)
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                settings.Set(key, value);
                settings.Get(key).ShouldBe(value);
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public static void UseRegistryManager_GetAll_Empty__GetEmptyDictionary(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(registryKeyType)
                    .GetSettings("Test");
                settings.ShouldNotBeNull();
                var dict = settings.GetAll();
                dict.ShouldNotBeNull();
                dict.Count.ShouldBe(0);
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public static void UseRegistryManager_GetAll__GetDictionary(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                var settings = SettingsBuilder
                    .CreateBuilder()
                    .UseRegistryManager(registryKeyType)
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
