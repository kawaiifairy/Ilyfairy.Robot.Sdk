using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    public class JsonChunk : MessageChunk
    {
        public JsonChunk()
        {

        }
        public JsonChunk(string json)
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
