using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Api.MessageContent
{
    /// <summary>
    /// 群消息
    /// </summary>
    public class GroupMessage : MessageBase
    {

        /// <summary>
        /// 发送者
        /// </summary>
        public MessageSender Sender { get; set; }
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupId { get; set; }
        private Lazy<GroupInfo> groupLazy;
        /// <summary>
        /// 群信息
        /// </summary>
        public GroupInfo Group { get => groupLazy.Value; }
        

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GroupMessage()
        {

        }
        public GroupMessage(Lazy<GroupInfo> group)
        {
            groupLazy = group;
        }


    }
}
