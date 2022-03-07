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
    public class AtChunk : MessageChunk
    {
        public long AtQQ { get; set; }

        internal AtChunk()
        {

        }

        public AtChunk(long qq)
        {
            if (qq < 0) qq = 0;
            AtQQ = qq;
            OriginText = $"[CQ:at,qq={qq}]";
            Type = CQCode.at;
        }
    }
}
