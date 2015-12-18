using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
{
    public class ErrorLogRequestEntity : LogRequestEntity
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Exception { get; set; }

        public new bool IsErrorLog { get { return true; } set { } }
    }
}
