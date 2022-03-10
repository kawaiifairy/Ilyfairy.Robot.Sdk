using Ilyfairy.Robot.Sdk.Model.Events;
using Ilyfairy.Robot.Sdk.Model.Messages;
using Ilyfairy.Robot.Sdk.Plugin;
using Ilyfairy.Robot.Sdk.Server;
using System.Text.RegularExpressions;

// 初始化Plugin新实例
PluginBase Plugin = new DemoPlugin();

//开始连接
Plugin.Start();

Thread.Sleep(-1);

class DemoPlugin : PluginBase
{
    public override PluginInfo Info => null;

    public override string WsUri => "ws://127.0.0.1:6700";

    public override string HttpUri => "http://127.0.0.1:5700";

    //连接事件
    protected override void OnConnect(object? sender, ConnectEventArgs e)
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
    }

    // 群消息事件
    protected override void OnGroupMessage(object? sender, GroupMessage e)
    {
        //复读机
        var match = Regex.Match(e.Text, @"^echo\s*(?<msg>(?:.|\n)*)$");
        if (match.Success)
        {
            Api.SendGroupMessage(e.GroupId, match.Groups["msg"].Value, false);
        }
    }
}