using Ilyfairy.Robot.CSharpSdk.Api.MessageChunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.CSharpSdk.Api
{
    public static class Ex
    {
        public static string ToOriginText(this IEnumerable<MessageChunk> chunks)
        {
            StringBuilder s = new();
            foreach (var chunk in chunks)
            {
                s.Append(chunk.OriginText);
            }
            return s.ToString();
        }
        public static string ToText(this IEnumerable<MessageChunk> chunks)
        {
            StringBuilder s = new();
            foreach (var chunk in chunks)
            {
                s.Append(chunk.ToString());
            }
            return s.ToString();
        }
    }
}
