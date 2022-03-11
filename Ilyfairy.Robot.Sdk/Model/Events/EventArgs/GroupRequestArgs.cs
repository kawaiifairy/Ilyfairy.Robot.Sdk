using Ilyfairy.Robot.Sdk.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Events.EventArgs
{
    public class GroupRequestArgs : EventBase
    {
        /// <summary>
        /// 验证信息
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 请求flag
        /// </summary>
        public long Flag { get; set; }
        /// <summary>
        /// 发送请求的qq
        /// </summary>
        public long QQ { get; set; }
        /// <summary>
        /// 群ID
        /// </summary>
        public long GroupId { get; set; }
        /// <summary>
        /// 加群类型
        /// </summary>
        public GroupRequestType Type { get; set; }
    }
}
