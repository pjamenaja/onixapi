using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;

namespace Onix.Api.Utils
{    
    public class LibSetting
    {
        private static LibSetting instance = new LibSetting();

        private IConfigurationRoot config = null;
        private ILoggerFactory logFactory = null;
        private string connStr = "";
        private string secretKey = "";
        private string apiKey = "";

        private LibSetting()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder                
                .AddFilter(level => level >= LogLevel.Error));

            logFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }

        public static LibSetting GetInstance()
        {
            return(instance);
        }

        public IConfigurationRoot Configuration
        {
            set 
            {
                connStr = value["ONIX_CONNECTION_STR"];
                apiKey = value["ONIX_EXTERNAL_APPLICATION_KEY"];
                secretKey = value["ONIX_OAUTH_KEY"];

                instance.config = value;
            }

            get
            {
                return(instance.config);
            }
        }

        public ILoggerFactory LogFactory
        {
            set 
            {
                logFactory = value;
            }

            get
            {
                return(instance.logFactory);
            }
        }

        public string ConnectionString
        {
            set 
            {
                instance.connStr = value;
            }

            get
            {
                return(instance.connStr);
            }
        }  

        public string OAuthKey
        {
            set 
            {
                instance.secretKey = value;
            }

            get
            {
                return(instance.secretKey);
            }
        }  

        public string ExternalApplicationKey
        {
            set 
            {
                instance.apiKey = value;
            }

            get
            {
                return(instance.apiKey);
            }
        }                                
    }
}