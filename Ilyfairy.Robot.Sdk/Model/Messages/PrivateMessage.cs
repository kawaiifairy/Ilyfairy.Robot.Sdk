using Ilyfairy.Robot.Sdk.Model.Units;

namespace Ilyfairy.Robot.Sdk.Model.Messages
{
    /// <summary>
    /// 私聊消息
    /// </summary>
    public class PrivateMessage : MessageBase
    {
        public MessageSender Sender { get; set; }

    }
}
