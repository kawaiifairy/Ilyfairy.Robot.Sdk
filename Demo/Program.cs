using Ilyfairy.Robot.PluginLoader;
using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Plugin;
using Ilyfairy.Robot.Sdk.Server;
using System.Text.RegularExpressions;

// 初始化插件
PluginBase plugin = new DemoPlugin();

// 初始化插件管理器
PluginManager manager = new("ws://127.0.0.1:6700", "http://127.0.0.1:5700");

// 加载并启用插件
manager.Load(plugin).Enabled = true;

// 开始连接
manager.Connect();

Thread.Sleep(-1);

class DemoPlugin : PluginBase
{
    public override PluginInfo Info => null;

    //连接事件
    public override bool OnConnect(object? sender, ConnectEventArgs e)
    {
        switch (e.Type)
        {
            case ConnectType.Success:
                Console.WriteLine("连接成功");
                break;
            case ConnectType.Lost:
                Console.WriteLine("连接断开");
                break;
            case ConnectType.Error:
                Console.WriteLine("连接错误");
                break;
            default:
                break;
        }
        return true;
    }

    // 群消息事件
    public override bool OnGroupMessage(object? sender, GroupMessage e)
    {
        //复读机
        var match = Regex.Match(e.Text, @"^echo\s*(?<msg>(?:.|\n)*)$");
        if (match.Success)
        {
            Api.SendGroupMessage(e.GroupId, match.Groups["msg"].Value, false);
        }
        return true;
    }
}