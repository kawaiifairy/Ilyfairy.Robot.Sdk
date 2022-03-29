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
                if (chunk == null) continue;
                s.Append(chunk.OriginText);
            }
            return s.ToString();
        }

        internal static string ToText(this IEnumerable<MessageChunk> chunks)
        {
            StringBuilder s = new();
            foreach (var chunk in chunks)
            {
                if (chunk == null) continue;
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

        internal static string CQEscape(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            else
            {
                return str.Replace("&", "&amp;").Replace(",", "&#44;").Replace("[", "&#91;").Replace("]", "&#93;");
            }
        }
        internal static string CQTextEscape(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            else
            {
                return str.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;");
            }
        }
    }
}
