using Ilyfairy.Robot.Sdk;
using Ilyfairy.Robot.Sdk.Api.Debug;
using Ilyfairy.Robot.Sdk.Connect;
using Ilyfairy.Robot.Sdk.Model.Content;
using System.Diagnostics;
using System.Drawing;
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
        Robot.ConnectEvent += Robot_ConnectEvent;
        Robot.Connect();

        Thread.Sleep(-1);
    }

    private static async void Robot_ConnectEvent(object? sender, ConnectType e)
    {
        Console.WriteLine(e);
        switch (e)
        {
            case ConnectType.Success:
                break;
            case ConnectType.Lost:
                //Robot.Connect();
                break;
            case ConnectType.Error:
                break;
            default:
                break;
        }
    }

    static void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
    {
        //Console.WriteLine($"[{e.Sender.Name} ({e.Sender.QQ})]: {e.OriginText}");

        Console.WriteLine($"{e.Group.Name} {e.Sender.CardName}:{e.Text}");

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