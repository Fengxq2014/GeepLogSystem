using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
{
    [Serializable]
    public class LogRequestEntity
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string DevNo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        //[MongoDB.Bson.Serialization.Attributes.BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string AccNo { get; set; }

        /// <summary>
        /// 服务器ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 自定义标志
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 附加描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 异常日志标志
        /// </summary>
        public bool IsErrorLog { get { return false; } set { } }

        /// <summary>
        /// 日志分类
        /// </summary>
        public string LogName { get; set; }
    }
}
