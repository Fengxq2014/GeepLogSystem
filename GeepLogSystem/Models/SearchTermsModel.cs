using System;

namespace GeepLogSystem.Models
{
    /// <summary>
    /// 搜索条件模型
    /// </summary>
    public class SearchTermsModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// 当前控制器名
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public bool AccNo { get; set; }

        /// <summary>
        /// 终端编号
        /// </summary>
        public bool DevNo { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public bool ClassName { get; set; }

        /// <summary>
        /// 自定义标志
        /// </summary>
        public bool Label { get; set; }

        /// <summary>
        /// 错误日志
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// 正常日志
        /// </summary>
        public bool Normal { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 附加描述
        /// </summary>
        public bool Description { get; set; }
    }
}