using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
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
                throw new Exception("没有获取到配置信息");
            }
        }
    }
}
