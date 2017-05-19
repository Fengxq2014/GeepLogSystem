using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeepLogSystem.Models
{
    /// <summary>
    /// ZFT日志
    /// </summary>
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class log : GeepLogSystem.Dao.GeepLogModel
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string DevNo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [MongoDB.Bson.Serialization.Attributes.BsonElement("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonElement("class")]
        public string Class { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string AccNo { get; set; }

        /// <summary>
        /// 服务器ip
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonElement("ip")]
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
        [MongoDB.Bson.Serialization.Attributes.BsonElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 异常日志标志
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonElement("iserror")]
        public bool IsErrorLog { get; set; }

        /// <summary>
        /// 日志分类
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonElement("logname")]
        public string LogName { get; set; }
    }
}