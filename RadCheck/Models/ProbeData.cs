using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCheck.Models
{
    /// <summary>
    /// 探头数据模型
    /// </summary>
    public class ProbeData
    {
        /// <summary>
        /// 探头编号
        /// </summary>
        public int ProbeId { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 剂量率 (mSV/H)
        /// </summary>
        public double DoseRate { get; set; }

        /// <summary>
        /// 状态 (正常/警告/报警)
        /// </summary>
        public ProbeStatus Status { get; set; }

        /// <summary>
        /// 测量时间
        /// </summary>
        public DateTime MeasureTime { get; set; }
    }

    /// <summary>
    /// 探头状态枚举
    /// </summary>
    public enum ProbeStatus
    {
        Normal,
        Warning,
        Alarm
    }
}
