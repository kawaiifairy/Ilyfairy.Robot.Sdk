using Ilyfairy.Robot.CSharpSdk.Api;
using Ilyfairy.Robot.Sdk;
using Ilyfairy.Robot.Sdk.Api;
using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk.Model.Messages;

namespace Ilyfairy.Robot.Sdk.Plugin
{
    /// <summary>
    /// 插件基类
    /// </summary>
    public abstract class PluginBase : IDisposable
    {
        private RobotManager Robot;
        public RobotApi Api { get; private set; }
        public MessageUtils Utils { get; private set; }

        public PluginBase()
        {
            Robot = new RobotManager(WsUri, HttpUri);
            Robot.ConnectEvent += OnConnect;
            Robot.PrivateMessageReceivedEvent += OnPrivateMessage;
            Robot.GroupMessageReceivedEvent += OnGroupMessage;
            Api = Robot.Api;
            Utils = Robot.Utils;
        }

        public void Start()
        {
            Robot.Connect();
        }

        public void Close()
        {
            Robot.WsClient?.Dispose();
        }

        public abstract PluginInfo Info { get; }
        public abstract string WsUri { get; }
        public abstract string HttpUri { get; }

        public void Dispose()
        {
            Robot.WsClient?.Dispose();
        }

        /// <summary>
        /// 连接状态发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnConnect(object? sender, ConnectEventArgs e)
        {
            
        }
        /// <summary>
        /// 接收到私聊消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPrivateMessage(object? sender, PrivateMessage e)
        {
            
        }
        /// <summary>
        /// 接收到群消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnGroupMessage(object? sender, GroupMessage e)
        {

        }

    }
}