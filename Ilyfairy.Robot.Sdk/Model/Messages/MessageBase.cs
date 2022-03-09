using Ilyfairy.Robot.Sdk.Api;
using Ilyfairy.Robot.Sdk.Model.Chunks;
using Ilyfairy.Robot.Sdk.Model.Units;

namespace Ilyfairy.Robot.Sdk.Model.Messages
{
    /// <summary>
    /// QQ消息基类
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageId { get; set; }
        /// <summary>
        /// 消息块
        /// </summary>
        public IEnumerable<MessageChunk> MessageChunks { get; set; }
        /// <summary>
        /// 将消息块转换为文本字符串
        /// </summary>
        public string Text { get => MessageChunks.ToText(); }
        /// <summary>
        /// 将消息块转换为原始字符串
        /// </summary>
        public string OriginText { get => MessageChunks.ToOriginText(); }
    }
}
