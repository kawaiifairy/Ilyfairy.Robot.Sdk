using Ilyfairy.Robot.Sdk.Api.MessageChunks;
using System.Text;

namespace Ilyfairy.Robot.Sdk.Api
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
