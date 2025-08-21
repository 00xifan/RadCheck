using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadCheck.Models
{
    /// <summary>
    /// 测量记录模型
    /// </summary>
    public class MeasurementRecord
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 桶号
        /// </summary>
        public string BucketNumber { get; set; }

        /// <summary>
        /// 测量日期
        /// </summary>
        public DateTime MeasureDate { get; set; }

        /// <summary>
        /// 测量人
        /// </summary>
        public string Measurer { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
