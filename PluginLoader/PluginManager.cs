using Ilyfairy.Robot.Manager;
using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Model.Units;
using Ilyfairy.Robot.Sdk.Plugin;
using System.Reflection;

namespace Ilyfairy.Robot.PluginLoader
{
    public class PluginManager
    {
        public List<Plugin> Plugins { get; set; } = new();

        public RobotManager Robot { get; set; }
        public string WsAddress { get; }
        public string HttpAddress { get; }

        public PluginManager(string wsAddress, string httpAddress)
        {
            WsAddress = wsAddress;
            HttpAddress = httpAddress;

            Robot?.WsClient?.Dispose();
            Robot = new RobotManager(WsAddress, HttpAddress);

            Robot.ConnectEvent += Robot_ConnectEvent;
            Robot.PrivateMessageReceivedEvent += Robot_PrivateMessageReceivedEvent;
            Robot.GroupMessageReceivedEvent += Robot_GroupMessageReceivedEvent;
            Robot.GroupIncreaseEvent += Robot_GroupIncreaseEvent;
            Robot.GroupDecreaseEvent += Robot_GroupDecreaseEvent;
        }

        public async Task Connect()
        {
            await Robot.Connect();
        }

        public Plugin Load(PluginBase pluginBase)
        {
            Plugin plugin = new();
            plugin.Instance = pluginBase;
            plugin.Instance.Utils = Robot.Utils;
            plugin.Instance.Api = Robot.Api;
            plugin.File = null;
            plugin.Info = pluginBase.Info;

            Plugins.Add(plugin);
            return plugin;
        }

        public Plugin Load(string file)
        {
            file = Path.GetFullPath(file);
            if (!File.Exists(file))
            {
                throw new Exception($"找不到指定的文件: {file}");
            }
            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFile(file);
            }
            catch (Exception e)
            {
                throw new Exception($"dll加载异常: {file}\n{e.Message}");
            }
            TypeInfo? typeInfo = asm.DefinedTypes.FirstOrDefault(v => v.BaseType == typeof(PluginBase));
            if (typeInfo == null)
            {
                throw new Exception($"找不到 PluginBase 的派生类: {file}");
            }
            var obj = Activator.CreateInstance(typeInfo) as PluginBase;
            if (obj == null)
            {
                throw new Exception($"初始化 PluginBase 新实例异常");
            }

            Plugin plugin = new();
            plugin.Instance = obj;
            plugin.Instance.Utils = Robot.Utils;
            plugin.Instance.Api = Robot.Api;
            plugin.File = null;
            plugin.Info = obj.Info;

            Plugins.Add(plugin);
            return plugin;
        }

        private void Loop(Func<Plugin, bool> callback)
        {
            foreach (var plugin in Plugins)
            {
                if (plugin.Enabled)
                {
                    var next = callback(plugin);
                    if (!next) break;
                }
            }
        }

        private void Robot_ConnectEvent(object? sender, ConnectEventArgs e)
        {
            Loop(plugin =>
            {
                return plugin.Instance.OnConnect(sender, e);
            });
        }
        private void Robot_PrivateMessageReceivedEvent(object? sender, PrivateMessage e)
        {
            Loop(plugin =>
            {
                return plugin.Instance.OnPrivateMessage(sender, e);
            });
        }
        private void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
        {
            Loop(plugin =>
            {
                return plugin.Instance.OnGroupMessage(sender, e);
            });
        }

        private void Robot_GroupIncreaseEvent(object? sender, GroupMemberChange e)
        {
            Loop(plugin =>
            {
                return plugin.Instance.OnGroupIncrease(sender, e);
            });
        }

        private void Robot_GroupDecreaseEvent(object? sender, GroupMemberChange e)
        {
            Loop(plugin =>
            {
                return plugin.Instance.OnGroupDecrease(sender, e);
            });
        }
    }

}