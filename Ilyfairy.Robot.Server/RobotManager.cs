using Ilyfairy.Robot.Sdk.Api;
using Newtonsoft.Json.Linq;
using System.Text;
using Websocket.Client;
using Ilyfairy.Robot.Sdk.Model.Chunks;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Server;
using Ilyfairy.Robot.Sdk.Model.Units;
using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk;

namespace Ilyfairy.Robot.Manager
{
    public class RobotManager
    {
        public Uri WsAddress { get; }
        public Uri HttpAddress { get; }
        public WebsocketClient WsClient { get; set; }
        public RobotApi Api { get; private set; }
        public MessageUtils Utils { get; private set; }

        public RobotManager(string wsAddress, string httpAddress)
        {
            WsAddress = new Uri(wsAddress);
            HttpAddress = new Uri(httpAddress);

            Api = new RobotApi(HttpAddress.AbsoluteUri);
            Utils = new MessageUtils(HttpAddress.AbsoluteUri);
        }

        private void Init()
        {
            WsClient?.Dispose();
            WsClient = new(WsAddress);

            WsClient.ErrorReconnectTimeout = TimeSpan.FromSeconds(10);
            // 断开连接发生
            WsClient.DisconnectionHappened.Subscribe(msg =>
            {
                //Cs.Crate($"[{DateTime.Now:G}] [WARNING] Disconnection: TYPE is {msg.Type}\n", ConsoleColor.DarkYellow).Show();
                if (msg.Type == DisconnectionType.Lost)
                {
                    ConnectEvent?.Invoke(this, new()
                    {
                        Type = ConnectType.Lost
                    });
                    //Console.WriteLine("连接断开");
                }
                else
                {
                    ConnectEvent?.Invoke(this, new()
                    {
                        Type = ConnectType.Error
                    });
                }
                //ConnectEvent?.Invoke(this, false);
            });
            // 重新连接发生
            WsClient.ReconnectionHappened.Subscribe(msg =>
            {
                if (msg.Type == ReconnectionType.Initial)
                {
                    //IsConnect = true;
                }
                //Cs.Crate($"[{DateTime.Now:G}] [WARNING] Reconnection: TYPE is {msg.Type}\n", ConsoleColor.DarkYellow).Show();
            });
            // 接收到消息发生
            WsClient.MessageReceived.Subscribe((message) =>
            {
                //Cs.Crate($"MESSAGE: {message.Text}\n").Show();
                JObject obj = JObject.Parse(message.Text);
                WsSocketProc(obj);
            });
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            if (WsClient == null)
            {
                Init();
            }
            await WsClient.Start();
            //return IsConnect;
        }

        /// <summary>
        /// WebSocket处理
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="Exception"></exception>
        private void WsSocketProc(JObject json)
        {
            var post_type = json.Value<string>("post_type");
            if (json.Value<string>("meta_event_type") == "heartbeat") return;

            switch (post_type)
            {
                case "meta_event":
                    MetaEventProc(json);
                    break;
                case "message":
                    MessageProc(json);
                    break;
                case "notice": //特殊事件
                    NoticeProc(json);
                    break;
                case "request":
                    break;
                default:
                    return;
#if DEBUG
                    throw new Exception($"未知类型: {post_type}");
#endif
                    break;
            }
        }

        /// <summary>
        /// 元消息处理
        /// </summary>
        /// <param name="json"></param>
        private void MetaEventProc(JObject json)
        {
            if (json.Value<string>("sub_type") == "connect")
            {
                ConnectEvent?.Invoke(this, new ConnectEventArgs
                {
                    Type = ConnectType.Success,
                });
            }

        }
        private void NoticeProc(JObject json)
        {
            string notice = json.Value<string>("notice_type");
            switch (notice)
            {
                case "group_increase": //加群
                    GroupIncreaseEvent?.Invoke(this, new GroupMemberChange()
                    {
                        GroupId = json.Value<long>("group_id"),
                        QQ = json.Value<long>("user_id"),
                    });
                    break;
                case "group_decrease": //退群
                    GroupDecreaseEvent?.Invoke(this, new GroupMemberChange()
                    {
                        GroupId = json.Value<long>("group_id"),
                        QQ = json.Value<long>("operator_id"),
                    });
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// qq消息处理
        /// </summary>
        /// <param name="json"></param>
        private void MessageProc(JObject json)
        {
            var type = json.Value<string>("message_type");
            var message = json.Value<string>("message");

            var messageChunks = MessageChunkProc(json);
            var sender = SenderProc(json);

            if (string.IsNullOrEmpty(sender.CardName))
            {
                sender.CardName = sender.Name;
            }

            switch (type)
            {
                case "group": //群消息
                    GroupMessageProc(json, messageChunks, sender);
                    break;
                case "private": //私聊消息
                    PrivateMessageProc(json, messageChunks, sender);
                    break;
                default:
#if DEBUG
                    throw new Exception($"未知消息类型: {type}");
#endif
                    break;
            }
        }

        /// <summary>
        /// 发送者处理
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private MessageSender SenderProc(JObject json)
        {
            var sender = json["sender"];
            if (sender == null) return null;
            var obj = new MessageSender();

            obj.QQ = sender.Value<long>("user_id");
            obj.Name = sender.Value<string>("nickname");
            obj.CardName = sender.Value<string>("card");
            if (string.IsNullOrEmpty(obj.CardName)) obj.Name = obj.CardName;
            obj.Age = sender.Value<int>("age");
            obj.Sex = sender.Value<string>("sex");

            return obj;
        }
        /// <summary>
        /// 消息块处理
        /// </summary>
        /// <param name="originMessageJson"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IEnumerable<MessageChunk> MessageChunkProc(JObject originMessageJson)
        {
            List<(string msg, bool isCQ)> originChunks = new();
            StringBuilder s = new();
            foreach (var c in originMessageJson.Value<string>("message"))
            {
                if (c == '[')
                {
                    if (s.Length > 0)
                    {
                        originChunks.Add((s.ToString(), false));
                        s.Clear();
                    }
                    continue;
                }
                else if (c == ']')
                {
                    originChunks.Add((s.ToString(), true));
                    s.Clear();
                }
                else
                {
                    s.Append(c);
                }
            }
            if (s.Length > 0)
            {
                originChunks.Add((s.ToString(), false));
                s.Clear();
            }

            foreach (var chunk in originChunks)
            {
                if (!chunk.isCQ)
                {
                    /*
                        & = &amp;
                        [ = &#91;
                        ] = &#93;
                    */
                    var text = chunk.msg.Replace("&amp;", "&").Replace("&#91;", "[").Replace("&#93;", "]");
                    yield return new TextChunk()
                    {
                        OriginText = chunk.msg,
                        Type = CQCode.none,
                        Text = text,
                    };
                    continue;
                }

                CQCode code = CQCode.none;
                MessageChunk obj = null;
                Dictionary<string, string> property = new();
                var originText = $"[{chunk.msg}]";
                var split = chunk.msg.Split(',');

                foreach (var key in split)
                {
                    if (key.StartsWith("CQ:"))
                    {
                        code = Enum.Parse<CQCode>(key[3..], true);
                    }
                    var keyValue = key.Split('=');
                    if (keyValue.Length >= 2)
                    {
                        property.Add(keyValue[0], string.Join("=", keyValue.Skip(1)));
                    }
                }

                switch (code)
                {
                    case CQCode.none:
#if DEBUG
                        throw new Exception("消息异常");
#endif
                        break;
                    case CQCode.face:
                        obj = new FaceChunk(int.Parse(property["id"]));
                        break;
                    case CQCode.record:

                        break;
                    case CQCode.video:
                        obj = new VideoChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            File = property["file"],
                            Url = property["url"],
                        };
                        break;
                    case CQCode.at:
                        obj = new AtChunk(long.Parse(property["qq"]));
                        break;
                    case CQCode.rps:
                        break;
                    case CQCode.dice:
                        break;
                    case CQCode.shake:
                        break;
                    case CQCode.anonymous:
                        break;
                    case CQCode.share:
                        break;
                    case CQCode.contact:
                        break;
                    case CQCode.location:
                        break;
                    case CQCode.music:
                        break;
                    case CQCode.image:
                        property.TryGetValue("subType", out var subType);
                        obj = new ImageChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            File = property["file"],
                            Url = property["url"],
                            SubType = subType != null ? int.Parse(subType) : null,
                        };
                        break;
                    case CQCode.reply:
                        obj = new ReplyChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            ReplyId = long.Parse(property["id"]),
                        };
                        break;
                    case CQCode.redbag:
                        break;
                    case CQCode.poke:
                        break;
                    case CQCode.gift:
                        break;
                    case CQCode.forward:
                        break;
                    case CQCode.node:
                        break;
                    case CQCode.xml:
                        break;
                    case CQCode.json:
                        obj = new JsonChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            Json = property["json"],
                        };
                        break;
                    case CQCode.cardimage:
                        break;
                    case CQCode.tts:
                        break;
                    default:
                        break;
                }
                if (obj == null)
                {
#if DEBUG
                    throw new Exception("未知消息类型");
#endif
                }
                yield return obj;
            }
        }

        /// <summary>
        /// 群消息处理
        /// </summary>
        /// <param name="json"></param>
        /// <param name="messageChunks"></param>
        private void GroupMessageProc(JObject json, IEnumerable<MessageChunk> messageChunks, MessageSender sender)
        {
            var groupId = json.Value<long>("group_id");
            GroupMessageReceivedEvent?.Invoke(this, new GroupMessage(new Lazy<GroupInfo>(() => Api.GetGroupInfo(groupId)))
            {
                MessageChunks = messageChunks,
                Sender = sender,
                GroupId = groupId,
            });
        }
        /// <summary>
        /// 私聊消息处理
        /// </summary>
        /// <param name="json"></param>
        /// <param name="messageChunks"></param>
        private void PrivateMessageProc(JObject json, IEnumerable<MessageChunk> messageChunks, MessageSender sender)
        {
            PrivateMessageReceivedEvent?.Invoke(this, new PrivateMessage
            {
                MessageChunks = messageChunks,
                Sender = sender,
            });
        }

        /// <summary>
        /// 群消息事件
        /// </summary>
        public event EventHandler<GroupMessage> GroupMessageReceivedEvent;
        /// <summary>
        /// 私聊事件
        /// </summary>
        public event EventHandler<PrivateMessage> PrivateMessageReceivedEvent;
        /// <summary>
        /// 连接发生改变时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> ConnectEvent;
        /// <summary>
        /// 群人数已增加事件
        /// </summary>
        public event EventHandler<GroupMemberChange> GroupIncreaseEvent;
        /// <summary>
        /// 群人数已减少事件
        /// </summary>
        public event EventHandler<GroupMemberChange> GroupDecreaseEvent;
    }
}