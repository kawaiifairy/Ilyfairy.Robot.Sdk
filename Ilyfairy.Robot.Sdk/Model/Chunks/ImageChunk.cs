using Ilyfairy.Robot.Sdk.Api;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    /// <summary>
    /// 图片
    /// </summary>
    public class ImageChunk : MessageChunk
    {
        public string Url { get; set; }
        public string File { get; set; }
        public int? SubType { get; set; }

        public ImageChunk()
        {

        }
        public ImageChunk(string url)
        {
            var file = new string(Enumerable.Range(0, 32).Select(v => (char)('0' + Random.Shared.Next(0, 10))).ToArray());
            OriginText = $"[CQ:image,file={file},url={url.CQEscape()},subType=1]";
            Type = CQCode.image;
            File = file;
            Url = url;
            SubType = null;
        }
    }
}