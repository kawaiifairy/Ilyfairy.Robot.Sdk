using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Events.EventArgs
{
    public class FriendRequestArgs : EventBase
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
    }
}
