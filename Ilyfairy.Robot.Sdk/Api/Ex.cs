using Flurl.Http;
using Ilyfairy.Robot.Sdk.Model.Chunks;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Ilyfairy.Robot.Sdk.Api
{
    public static class Ex
    {
        internal static string ToOriginText(this IEnumerable<MessageChunk> chunks)
        {
            StringBuilder s = new();
            foreach (var chunk in chunks)
            {
                s.Append(chunk.OriginText);
            }
            return s.ToString();
        }

        internal static string ToText(this IEnumerable<MessageChunk> chunks)
        {
            StringBuilder s = new();
            foreach (var chunk in chunks)
            {
                s.Append(chunk.ToString());
            }
            return s.ToString();
        }

        internal static JObject? GetUrlJson(string url)
        {
            try
            {
                var r = url.GetStringAsync().Result;
                var json = JObject.Parse(r);
                return json;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
