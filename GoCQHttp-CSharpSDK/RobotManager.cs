using Ilyfairy.Robot.CSharpSdk.Api;
using Ilyfairy.Robot.CSharpSdk.Api.MessageChunks;
using Ilyfairy.Robot.CSharpSdk.Api.MessageContent;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using Websocket.Client;

namespace Ilyfairy.Robot.CSharpSdk
{
    public class RobotManager
    {
        public Uri WsAddress { get; }
        public Uri HttpAddress { get; }
        public WebsocketClient WsClient { get; set; }
        public RobotApi Api { get; private set; }

        public RobotManager(string wsUri, string httpUri)
        {
            WsAddress = new Uri(wsUri);
            HttpAddress = new Uri(httpUri);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            DisconnectionType? msg = null;
            WsClient?.Dispose();
            WsClient = new(WsAddress);

            WsClient.DisconnectionHappened.Subscribe(x =>
            {
                msg = x.Type;
            });
            WsClient.MessageReceived.Subscribe((message) =>
            {
                JObject obj = JObject.Parse(message.Text);
                WsSocketProc(obj);
            });

            WsClient.Start().Wait();

            if (msg != null)
            {
                return false;
            }
            else
            {
                Api = new RobotApi(HttpAddress.AbsoluteUri);
                return true;
            }
        }

        /// <summary>
        /// WebSocket处理
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="Exception"></exception>
        private void WsSocketProc(JObject json)
        {
            var post_type = json.Value<string>("post_type");

            switch (post_type)
            {
                case "meta_event":
                    MetaEventProc(json);
                    break;
                case "message":
                    MessageProc(json);
                    break;
                case "notice": //撤回触发

                    break;
                default:
                    throw new Exception($"未知类型: {post_type}");
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

            switch (type)
            {
                case "group": //群消息
                    GroupMessageProc(json, messageChunks);
                    break;
                case "private": //私聊消息
                    PrivateMessageProc(json, messageChunks);
                    break;
                default:
                    Console.WriteLine($"未知消息类型: {type}");
                    break;
            }
        }

        /// <summary>
        /// 群消息处理
        /// </summary>
        /// <param name="json"></param>
        /// <param name="messageChunks"></param>
        private void GroupMessageProc(JObject json,IEnumerable<MessageChunk> messageChunks)
        {
            GroupMessageReceivedEvent?.Invoke(this, new GroupMessage
            {
                MessageChunks = messageChunks
            });
        }
        /// <summary>
        /// 私聊消息处理
        /// </summary>
        /// <param name="json"></param>
        /// <param name="messageChunks"></param>
        private void PrivateMessageProc(JObject json, IEnumerable<MessageChunk> messageChunks)
        {
            PrivateMessageReceivedEvent?.Invoke(this, new PrivateMessage
            {
                MessageChunks = messageChunks
            });
        }

        /// <summary>
        /// 消息块处理
        /// </summary>
        /// <param name="originMessageJson"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<MessageChunk> MessageChunkProc(JObject originMessageJson)
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
                    yield return new TextMessageChunk()
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

                foreach (var key in chunk.msg.Split(','))
                {
                    if (key.StartsWith("CQ:"))
                    {
                        code = Enum.Parse<CQCode>(key[3..], true);
                    }
                    var keyValue = key.Split('=');
                    if (keyValue.Length == 2)
                    {
                        property.Add(keyValue[0], keyValue[1]);
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
                        obj = new FaceMessageChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            Face = int.Parse(property["id"]),
                        };
                        break;
                    case CQCode.record:
                        
                        break;
                    case CQCode.video:
                        break;
                    case CQCode.at:
                        obj = new AtMessageChunk()
                        {
                            OriginText = originText,
                            Type = code,
                            AtQQ = long.Parse(property["qq"]),
                        };
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
                        obj = new ImageMessageChunk()
                        {
                            OriginText = originText,
                            Url = property["file"],
                            SubType = subType != null ? int.Parse(subType) : null,
                            Type = code,
                            File = property["file"],
                        };
                        break;
                    case CQCode.reply:
                        obj = new ReplyMessageChunk()
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
                        obj = new JsonMessageChunk()
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
        /// 群消息事件
        /// </summary>
        public event EventHandler<GroupMessage> GroupMessageReceivedEvent;
        /// <summary>
        /// 私聊事件
        /// </summary>
        public event EventHandler<PrivateMessage> PrivateMessageReceivedEvent;
    }
}