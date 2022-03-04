using Flurl.Http;
using Ilyfairy.Robot.CSharpSdk.Api.MessageChunks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.CSharpSdk.Api
{
    public class RobotApi
    {
        public string BaseAddress { get; }

        public RobotApi(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        /// <summary>
        /// 发送群信息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="messageChunks">消息块</param>
        /// <returns></returns>
        public long SendGroupMessage(long group, IEnumerable<MessageChunk> messageChunks)
        {
            return SendGroupMessage(group, messageChunks.ToOriginText(), true);
        }
        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="message">消息</param>
        /// <param name="autoEscape">是否以文本方式发送(不进行转义)</param>
        /// <returns></returns>
        public long SendGroupMessage(long group, string message, bool autoEscape = false)
        {
            string url = $"{BaseAddress}send_group_msg?group_id={group}&message={WebUtility.UrlEncode(message)}&auto_escape={autoEscape}";
            var json = url.GetStringAsync().Result;
            return JObject.Parse(json)["data"]?.Value<long>("message_id") ?? 0;
        }

        /// <summary>
        /// 发送私聊信息
        /// </summary>
        /// <param name="qq">qq</param>
        /// <param name="messageChunks">消息块</param>
        /// <returns></returns>
        public long SendPrivateMessage(long qq, IEnumerable<MessageChunk> messageChunks)
        {
            return SendGroupMessage(qq, messageChunks.ToOriginText(), true);
        }
        /// <summary>
        /// 发送私聊信息
        /// </summary>
        /// <param name="qq">qq</param>
        /// <param name="message">消息</param>
        /// <param name="autoEscape">是否以文本方式发送(不进行转义)</param>
        /// <returns></returns>
        public long SendPrivateMessage(long qq, string message, bool autoEscape)
        {
            long? groupId = null;
            string url = $"{BaseAddress}send_private_msg?user_id={qq}&group_id={groupId}&message={message}&auto_escape={autoEscape}";
            var json = url.GetStringAsync().Result;
            return JObject.Parse(json)["data"]?.Value<long>("message_id") ?? 0;
        }
    }
}
