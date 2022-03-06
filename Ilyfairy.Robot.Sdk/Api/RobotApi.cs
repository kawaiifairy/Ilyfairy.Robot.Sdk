using Flurl.Http;
using Ilyfairy.Robot.Sdk.Model;
using Ilyfairy.Robot.Sdk.Model.Chunks;
using Ilyfairy.Robot.Sdk.Model.Content;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Api
{
    public class RobotApi
    {
        /// <summary>
        /// Api的http地址
        /// </summary>
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
        /// <param name="autoEscape">是否以纯文本方式发送(不进行转义)</param>
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
        /// <param name="autoEscape">是否以纯文本方式发送(不进行转义)</param>
        /// <returns></returns>
        public long SendPrivateMessage(long qq, string message, bool autoEscape)
        {
            long? groupId = null;
            string url = $"{BaseAddress}send_private_msg?user_id={qq}&group_id={groupId}&message={message}&auto_escape={autoEscape}";
            var json = url.GetStringAsync().Result;
            return JObject.Parse(json)["data"]?.Value<long>("message_id") ?? 0;
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="noCache">是否不使用缓存</param>
        /// <returns></returns>
        public GroupInfo? GetGroupInfo(long group,bool noCache = false)
        {
            var json = Ex.GetUrlJson($"{BaseAddress}get_group_info?group_id={group}&no_cache={noCache}");
            if (json == null) return null;

            var data = json["data"];
            GroupInfo info = new();
            info.Id = data.Value<long>("group_id");
            info.Name = data.Value<string>("group_name");
            info.Memo = data.Value<string>("group_memo");
            info.CreateTime = data.Value<uint>("group_create_time");
            info.Level = data.Value<uint>("group_level");
            info.MemberCount = data.Value<int>("member_count");
            info.MaxMemberCount = data.Value<int>("max_member_count");
            return info;
        }

        public void GetFriendList()
        {

        }
    }
}
 