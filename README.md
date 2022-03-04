Ilyfairy.Robot.Sdk

# QQ机器人 C#SDK

采用.net6 C#10编写

### 安装

前往[go-cqhttp Release](https://github.com/Mrs4s/go-cqhttp/releases)下载go-cqhttp



### Demo

```c#
// Main方法

// ws地址 http地址
Robot = new("ws://127.0.0.1:6700", "http://127.0.0.1:5700");

// 绑定事件
Robot.PrivateMessageReceivedEvent += Robot_PrivateMessageReceivedEvent;
Robot.GroupMessageReceivedEvent += Robot_GroupMessageReceivedEvent;

// 连接
if (Robot.Connect())
{
    Console.WriteLine("连接成功");
}
else
{
    Console.WriteLine("连接失败");
    return;
}

Console.ReadLine();

// 复读机
static void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
{
    Robot.Api.SendGroupMessage(e.GroupId, e.OriginText, false);
}
static void Robot_PrivateMessageReceivedEvent(object? sender, PrivateMessage e)
{
    Robot.Api.SendPrivateMessage(e.Sender.QQ, e.OriginText, false);
}
```





