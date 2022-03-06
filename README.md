Ilyfairy.Robot.Sdk

# QQ机器人 C#SDK

采用.net6 C#10编写

### 安装

前往[go-cqhttp Release](https://github.com/Mrs4s/go-cqhttp/releases)下载go-cqhttp



### 如何使用

```c#
using Ilyfairy.Robot.Sdk.Connect;
using Ilyfairy.Robot.Sdk.Model.Content;
using System.Text.RegularExpressions;

class Program
{
    static RobotManager Robot;
    static void Main(string[] args)
    {
        // 初始化Robot新实例
        Robot = new("ws://127.0.0.1:6700", "http://127.0.0.1:5700");

        // 绑定事件
        Robot.GroupMessageReceivedEvent += Robot_GroupMessageReceivedEvent;
        Robot.ConnectEvent += Robot_ConnectEvent;
        // 开始连接!
        Robot.Connect();

        Thread.Sleep(-1);
    }

    //连接事件
    private static void Robot_ConnectEvent(object? sender, ConnectType e)
    {
        switch (e)
        {
            case ConnectType.Success:
                Console.WriteLine("连接成功");
                break;
            case ConnectType.Lost:
                break;
            case ConnectType.Error:
                break;
            default:
                break;
        }
    }

    // 群消息事件
    static void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
    {
        //复读机
        var match = Regex.Match(e.Text, @"^echo\s*(?<msg>(?:.|\n)*)$");
        if (match.Success)
        {
            Robot.Api.SendGroupMessage(e.GroupId, match.Groups["msg"].Value, false);
        }
    }

}
```





