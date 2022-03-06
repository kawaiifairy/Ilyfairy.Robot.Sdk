using Newtonsoft.Json.Linq;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public abstract class MessageChunk
    {
        public string OriginText { get; set; }
        /// <summary>
        /// 消息块类型 (none为文本消息)
        /// </summary>
        public CQCode Type { get; set; }

        public override string ToString()
        {
            return OriginText;
        }
    }
}