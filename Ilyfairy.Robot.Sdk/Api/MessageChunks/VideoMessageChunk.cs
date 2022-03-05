using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Api.MessageChunks
{
    /// <summary>
    /// AtQQ
    /// </summary>
    public class VideoMessageChunk : MessageChunk
    {
        public string File { get; set; }
        public string Url { get; set; }
    }
}
