using System;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Onix.Api.Utils;

namespace Onix.Test.Api.Utils
{
    public class LibSettingTest
    {
        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable ("ONIX_CONNECTION_STR", "DUMMY1");
            Environment.SetEnvironmentVariable ("ONIX_EXTERNAL_APPLICATION_KEY", "DUMMY2");
            Environment.SetEnvironmentVariable ("ONIX_OAUTH_KEY", "DUMMY3");
        }

        private LibSetting getLibSettingInstance()
        {
            LibSetting instance = LibSetting.GetInstance();
            return instance;
        }

        private IConfigurationRoot getConfigurationRoot()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();

            IConfigurationRoot cfg = builder.Build();
            return cfg;
        }

        [TestCase]
        public void PassValuesToLibSettingTest()
        {
            LibSetting lib = getLibSettingInstance();
            IConfigurationRoot cfg = getConfigurationRoot();
            lib.Configuration = cfg;
        
            string connStr = lib.ConnectionString;
            Assert.AreEqual("DUMMY1", connStr, "ONIX_CONNECTION_STR string mismatch !!!"); 

            string extKey = lib.ExternalApplicationKey;
            Assert.AreEqual("DUMMY2", extKey, "ONIX_EXTERNAL_APPLICATION_KEY string mismatch !!!");  

            string oauthKey = lib.OAuthKey;
            Assert.AreEqual("DUMMY3", oauthKey, "ONIX_OAUTH_KEY string mismatch !!!");

            ILoggerFactory logFactory = lib.LogFactory;
            Assert.NotNull(logFactory, "Log factory is null !!!");

            IConfigurationRoot cfgOut = lib.Configuration;
            Assert.NotNull(cfgOut, "Configuratin is null !!!");
        }

        [TestCase]
        public void InjectValuesToLibSettingTest()
        {
            LibSetting lib = getLibSettingInstance();
            IConfigurationRoot cfg = getConfigurationRoot();
            lib.Configuration = cfg;
        
            lib.ConnectionString = "INJECT1";
            Assert.AreEqual("INJECT1", lib.ConnectionString, "ONIX_CONNECTION_STR string mismatch !!!"); 

            lib.ExternalApplicationKey = "INJECT2";
            Assert.AreEqual("INJECT2", lib.ExternalApplicationKey, "ONIX_EXTERNAL_APPLICATION_KEY string mismatch !!!");  

            lib.OAuthKey = "INJECT3";
            Assert.AreEqual("INJECT3", lib.OAuthKey, "ONIX_OAUTH_KEY string mismatch !!!");

            ILoggerFactory temp = lib.LogFactory; //Backup the old value
            lib.LogFactory = null;
            Assert.IsNull(lib.LogFactory, "Log factory should be null !!!");
            lib.LogFactory = temp; //Restore the old value back
        }                                                
    }    
}