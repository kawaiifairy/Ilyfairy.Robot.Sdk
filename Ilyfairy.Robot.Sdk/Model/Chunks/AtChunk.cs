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

        /// <summary>
        /// 艾特QQ
        /// </summary>
        /// <param name="qq">艾特的QQ，-1表示艾特全体</param>
        public AtChunk(long qq)
        {
            QQ = qq;
            OriginText = $"[CQ:at,qq={(qq == -1 ? "all" : qq)}]";
            Type = CQCode.at;
        }
    }
}
