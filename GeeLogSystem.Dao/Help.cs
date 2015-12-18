using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeepLogSystem.Dao
{
    public class Help
    {
        public static string GetConfigValueForNull(string key)
        {
            string rtnString = string.Empty;

            if (ConfigurationManager.AppSettings != null
                && ConfigurationManager.AppSettings[key] != null
                && !String.IsNullOrEmpty(ConfigurationManager.AppSettings[key].ToString()))
            {
                rtnString = ConfigurationManager.AppSettings[key];
            }
            if (!string.IsNullOrWhiteSpace(rtnString))
            {
                return rtnString;
            }
            else
            {
                return null;
            }
        }
    }
}
