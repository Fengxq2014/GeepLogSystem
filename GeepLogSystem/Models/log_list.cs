using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeepLogSystem.Models
{
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]
    public class log_list : GeepLogSystem.Dao.GeepLogModel
    {
        /// <summary>
        /// 交易类型
        /// </summary>
        public string Trade_Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Trade_Time { get; set; }

        public string Message { get; set; }

        public string Resp_NO { get; set; }
        public string Ftag { get; set; }
        public string Pin_Code { get; set; }
        public bool Succeed { get; set; }
    }
}