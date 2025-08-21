using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCheck.Models
{
    /// <summary>
    /// 历史数据模型
    /// </summary>
    public class HistoricalData
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 探头ID
        /// </summary>
        public int ProbeId { get; set; }

        /// <summary>
        /// 剂量率 (mSV/H)
        /// </summary>
        public double DoseRate { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
