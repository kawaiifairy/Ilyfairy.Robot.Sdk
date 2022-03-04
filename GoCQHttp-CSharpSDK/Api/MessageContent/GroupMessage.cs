using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.CSharpSdk.Api.MessageContent
{
    public class GroupMessage : MessageBase
    {
        public MessageSender Sender { get; set; }
        public long GroupId { get; set; }
    }
}
