using Ilyfairy.Robot.Sdk.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    /// <summary>
    /// Json消息
    /// </summary>
    public class JsonChunk : MessageChunk
    {
        public JsonChunk()
        {

        }
        public JsonChunk(string json)
        {
            this.Json = json;
            this.OriginText = $"[CQ:json,data={this.Json.CQEscape()}]";
            this.Type = CQCode.none;
        }
        /// <summary>
        /// Json内容
        /// </summary>
        public string Json { get; set; }

        public override string ToString()
        {
            return Json;
        }
    }
}
