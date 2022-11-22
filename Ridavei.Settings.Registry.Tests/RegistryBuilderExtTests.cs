using System;

using Ridavei.Settings.Exceptions;

using Ridavei.Settings.Registry.Enums;

using Microsoft.Win32;
using NUnit.Framework;
using Shouldly;

namespace Ridavei.Settings.Registry.Tests
{
    [TestFixture]
    public class RegistryBuilderExtTests
    {
        private const string SubKeyName = "Software\\Ridavei";
        private const string DictionaryName = "Test";

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

        [Test]
        public void UseRegistryManager_NonSupportedType__RaiseException()
        {
            Should.Throw<NotSupportedException>(() =>
            {
                var builder = SettingsBuilder.CreateBuilder();
                builder.UseRegistryManager(RegistryKeyType.LocalMachine | RegistryKeyType.CurrentUser);
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void GetSettings_NonExistingDictionary__RaiseException(RegistryKeyType registryKeyType)
        {
            Should.Throw<DictionaryNotFoundException>(() =>
            {
                GetBuilder(registryKeyType).GetSettings(DictionaryName);
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void GetOrCreateSettings__RetrieveSettings(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                using (var settings = GetBuilder(registryKeyType).GetOrCreateSettings(DictionaryName))
                    settings.ShouldNotBeNull();
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void Set__NoException(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                using (var settings = GetBuilder(registryKeyType).GetOrCreateSettings(DictionaryName))
                {
                    settings.ShouldNotBeNull();
                    settings.Set("T1", "T2");
                }
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void Get__GetValue(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                using (var settings = GetBuilder(registryKeyType).GetOrCreateSettings(DictionaryName))
                {
                    settings.ShouldNotBeNull();
                    settings.Set(key, value);
                    settings.Get(key).ShouldBe(value);
                }
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void GetAll_Empty__GetEmptyDictionary(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                using (var settings = GetBuilder(registryKeyType).GetOrCreateSettings(DictionaryName))
                {
                    settings.ShouldNotBeNull();
                    var dict = settings.GetAll();
                    dict.ShouldNotBeNull();
                    dict.Count.ShouldBe(0);
                }
            });
        }

        [TestCase(RegistryKeyType.LocalMachine)]
        [TestCase(RegistryKeyType.CurrentUser)]
        public void GetAll__GetDictionary(RegistryKeyType registryKeyType)
        {
            Should.NotThrow(() =>
            {
                string key = "T1";
                string value = "T2";
                using (var settings = GetBuilder(registryKeyType).GetOrCreateSettings(DictionaryName))
                {
                    settings.ShouldNotBeNull();
                    settings.Set(key, value);
                    var dict = settings.GetAll();
                    dict.ShouldNotBeNull();
                    dict.Count.ShouldBe(1);
                    dict.ContainsKey(key).ShouldBeTrue();
                    dict[key].ShouldBe(value);
                }
            });
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

        private SettingsBuilder GetBuilder(RegistryKeyType registryKeyType)
        {
            return SettingsBuilder
                .CreateBuilder()
                .UseRegistryManager(registryKeyType);
        }
    }
}
