using Ilyfairy.Robot.Sdk.Api;
using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Model.Units;

namespace Ilyfairy.Robot.Sdk.Plugin
{
    /// <summary>
    /// 插件基类
    /// </summary>
    public abstract class PluginBase
    {
        //private RobotManager Robot;
        public RobotApi Api { get; set; }
        public MessageUtils Utils { get; set; }
        public abstract PluginInfo Info { get; }

        /// <summary>
        /// 连接状态发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>是否继续向下传递事件</returns>
        public virtual bool OnConnect(object? sender, ConnectEventArgs e)
        {
            return true;
        }
        /// <summary>
        /// 接收到私聊消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>是否继续向下传递事件</returns>
        public virtual bool OnPrivateMessage(object? sender, PrivateMessage e)
        {
            return true;
        }
        /// <summary>
        /// 接收到群消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>是否继续向下传递事件</returns>
        public virtual bool OnGroupMessage(object? sender, GroupMessage e)
        {
            return true;
        }
        /// <summary>
        /// 群员已增加
        /// </summary>
        /// <returns>是否继续向下传递事件</returns>
        public virtual bool OnGroupIncrease(object? sender,GroupMemberChange e)
        {
            return true;
        }
        /// <summary>
        /// 群员已减少
        /// </summary>
        /// <returns>是否继续向下传递事件</returns>
        public virtual bool OnGroupDecrease(object? sender, GroupMemberChange e)
        {
            return true;
        }

    }
}