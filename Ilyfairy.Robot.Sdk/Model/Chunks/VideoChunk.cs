using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    /// <summary>
    /// AtQQ
    /// </summary>
    public class VideoChunk : MessageChunk
    {
        public string File { get; set; }
        public string Url { get; set; }
    }
}
