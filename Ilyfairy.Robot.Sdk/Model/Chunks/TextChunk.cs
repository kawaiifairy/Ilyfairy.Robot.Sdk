using Ilyfairy.Robot.Sdk.Api;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class TextChunk : MessageChunk
    {
        public TextChunk()
        {

        }
        public TextChunk(string text)
        {
            this.Text = text;
            this.OriginText = text.CQEscape();
            this.Type = CQCode.none;
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}