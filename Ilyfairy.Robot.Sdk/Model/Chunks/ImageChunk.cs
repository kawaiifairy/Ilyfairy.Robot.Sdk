namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class ImageChunk : MessageChunk
    {
        public string Url { get; set; }
        public string File { get; set; }
        public int? SubType { get; set; }
    }
}