namespace Ilyfairy.Robot.Sdk.Api.MessageChunks
{
    public class ImageMessageChunk : MessageChunk
    {
        public string Url { get; set; }
        public string File { get; set; }
        public int? SubType { get; set; }
    }
}