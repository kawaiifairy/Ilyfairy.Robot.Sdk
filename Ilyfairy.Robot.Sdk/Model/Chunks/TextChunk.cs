﻿namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class TextChunk : MessageChunk
    {
        public TextChunk()
        {

        }
        public TextChunk(string text)
        {
            this.Text = text;
            this.OriginText = text.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;");
            this.Type = CQCode.none;
        }

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}