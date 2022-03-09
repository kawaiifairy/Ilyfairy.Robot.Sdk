using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    /// <summary>
    /// 艾特QQ
    /// </summary>
    public class AtChunk : MessageChunk
    {
        /// <summary>
        /// 需要艾特的QQ
        /// </summary>
        public long QQ { get; set; }

        internal AtChunk()
        {

        }

        public AtChunk(long qq)
        {
            if (qq < 0) qq = 0;
            QQ = qq;
            OriginText = $"[CQ:at,qq={qq}]";
            Type = CQCode.at;
        }
    }
}
