namespace Ilyfairy.Robot.Sdk.Api.MessageContent
{
    /// <summary>
    /// 私聊消息
    /// </summary>
    public class PrivateMessage : MessageBase
    {
        public MessageSender Sender { get; set; }

    }
}
