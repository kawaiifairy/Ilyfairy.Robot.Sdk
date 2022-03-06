namespace Ilyfairy.Robot.Sdk.Model.Content
{
    /// <summary>
    /// 消息发送者
    /// </summary>
    public class MessageSender
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// (群)昵称
        /// </summary>
        public string CardName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public long QQ { get; set; }
        /// <summary>
        /// QQ昵称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        Unknow,

    }
}
