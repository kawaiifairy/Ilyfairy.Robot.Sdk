using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Api.MessageChunks
{
    public class JsonMessageChunk : MessageChunk
    {
        public JsonMessageChunk()
        {

        }
        public JsonMessageChunk(string json)
        {
            this.Json = json;
            this.OriginText = $"[CQ:json,data={json}]";
            this.Type = CQCode.none;
        }
        public string Json { get; set; }

        public override string ToString()
        {
            return Json;
        }
    }
}
