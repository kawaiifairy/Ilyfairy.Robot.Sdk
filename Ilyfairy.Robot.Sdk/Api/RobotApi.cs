using Flurl.Http;
using Ilyfairy.Robot.Sdk.Model;
using Ilyfairy.Robot.Sdk.Model.Chunks;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Model.Units;
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
        public long? SendGroupMessage(long group, params MessageChunk[] messageChunks)
        {
            return SendGroupMessage(group, messageChunks.ToOriginText(), false);
        }
        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="message">消息</param>
        /// <param name="autoEscape">是否以纯文本方式发送(不进行转义)</param>
        /// <returns></returns>
        public long? SendGroupMessage(long group, string message, bool autoEscape = false)
        {
            string url = $"{BaseAddress}send_group_msg?group_id={group}&message={WebUtility.UrlEncode(message)}&auto_escape={autoEscape}";
            var json = url.GetStringAsync().Result;
            var result = JObject.Parse(json);
            if (result.Value<string>("status") != "success")
            {
                return null;
            }
            return result["data"]?.Value<long>("message_id") ?? 0;
        }

        /// <summary>
        /// 发送私聊信息
        /// </summary>
        /// <param name="qq">qq</param>
        /// <param name="messageChunks">消息块</param>
        /// <returns></returns>
        public long? SendPrivateMessage(long qq, params MessageChunk[] messageChunks)
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
        public long? SendPrivateMessage(long qq, string message, bool autoEscape)
        {
            long? groupId = null;
            string url = $"{BaseAddress}send_private_msg?user_id={qq}&group_id={groupId}&message={message}&auto_escape={autoEscape}";
            var json = url.GetStringAsync().Result;
            var result = JObject.Parse(json);
            if (result.Value<string>("status") != "success") return null;

            return result["data"]?.Value<long>("message_id") ?? 0;
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="noCache">是否不使用缓存</param>
        /// <returns></returns>
        public GroupInfo? GetGroupInfo(long group, bool noCache = false)
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

        /// <summary>
        /// 获取好友列表
        /// </summary>
        public Friend[] GetFriendList()
        {
            var json = Ex.GetUrlJson($"{BaseAddress}get_friend_list");
            if (json == null) return null;

            if (json.Value<string>("status") != "ok" || json.Value<int>("retcode") != 0)
            {
                return null;
            }

            List<Friend> qs = new();
            var data = json["data"].ToArray();
            foreach (var item in data)
            {
                var nickname = item.Value<string>("nickname");
                var remark = item.Value<string>("remark");
                var user_id = item.Value<long>("user_id");

                Friend qq = new();
                qq.Name = nickname;
                qq.Remark = remark;
                qq.QQ = user_id;
                qs.Add(qq);
            }

            return qs.ToArray();
        }

        /// <summary>
        /// 群员禁言
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="qq">要禁言的QQ号</param>
        /// <param name="duration">时长 默认30分钟<br/>0 则取消禁言</param>
        /// <returns>禁言成功返回True, 否则False</returns>
        public bool SetGroupMemberMute(long group, long qq, int duration = 30 * 60)
        {
            var json = Ex.GetUrlJson($"{BaseAddress}set_group_ban?group_id={group}&user_id={qq}&duration={duration}");
            if (json == null) return false;

            if (json.Value<string>("status") != "ok" || json.Value<int>("retcode") != 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 群全员禁言
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="enable">是否要禁言</param>
        /// <returns>请求成功返回True, 否则False<br/>True不一定代表禁言成功</returns>
        public bool SetGroupAllMute(long group, bool enable)
        {
            var json = Ex.GetUrlJson($"{BaseAddress}set_group_whole_ban?group_id={group}&enable={enable}");
            if (json == null) return false;

            if (json.Value<string>("status") != "ok" || json.Value<int>("retcode") != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置群名
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="newName">要设置的新的群名</param>
        /// <returns>请求成功返回True, 否则False<br/>True不一定代表设置成功</returns>
        public bool SetGroupName(long group, string newName)
        {
            var json = Ex.GetUrlJson($"{BaseAddress}set_group_name?group_id={group}&group_name={WebUtility.UrlEncode(newName)}");
            if (json == null) return false;

            if (json.Value<string>("status") != "ok" || json.Value<int>("retcode") != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置群名片
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="qq">要指定的成员qq</param>
        /// <param name="newCardName">群名片内容</param>
        /// <returns>请求成功返回True, 否则False<br/>True不一定代表设置成功</returns>
        public bool SetGroupMemberCardName(long group, long qq, string newCardName)
        {
            var json = Ex.GetUrlJson($"{BaseAddress}set_group_card?group_id={group}&user_id={qq}&card={WebUtility.UrlEncode(newCardName)}");
            if (json == null) return false;

            if (json.Value<string>("status") != "ok" || json.Value<int>("retcode") != 0)
            {
                return false;
            }

            return true;
        }


    }

}
