using Ilyfairy.Robot.Sdk;
using Ilyfairy.Robot.Sdk.Api.MessageContent;
using System.Net.Sockets;
using System.Text.RegularExpressions;

class Program
{
    static RobotManager Robot;
    static void Main(string[] args)
    {
        Robot = new("ws://127.0.0.1:6700", "http://127.0.0.1:5700");

        Robot.PrivateMessageReceivedEvent += Robot_PrivateMessageReceivedEvent;
        Robot.GroupMessageReceivedEvent += Robot_GroupMessageReceivedEvent;

        if (Robot.Connect())
        {
            Console.WriteLine("连接成功");
        }
        else
        {
            Console.WriteLine("连接失败");
            return;
        }

        Thread.Sleep(-1);
    }

    static void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
    {
        //Console.WriteLine($"[{e.Sender.Name} ({e.Sender.QQ})]: {e.OriginText}");
        Console.WriteLine(e.GroupId);
        Console.WriteLine(e.Group.Name);

        return;
        var match = Regex.Match(e.Text, @"^echo\s*(?<msg>.*)$");
        if (match.Success)
        {
            //Robot.Api.SendGroupMessage(e.GroupId, match.Groups["msg"].Value, false);
        }
    }

    static void Robot_PrivateMessageReceivedEvent(object? sender, PrivateMessage e)
    {

    }
}