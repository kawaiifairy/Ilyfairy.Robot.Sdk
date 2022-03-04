using Ilyfairy.Robot.CSharpSdk;
using Ilyfairy.Robot.CSharpSdk.Api.MessageContent;

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

        Thread.Sleep(1000000);
    }
    static void Robot_GroupMessageReceivedEvent(object? sender, GroupMessage e)
    {
        Robot.Api.SendGroupMessage(e.GroupId, e.OriginText, false);
    }
    static void Robot_PrivateMessageReceivedEvent(object? sender, PrivateMessage e)
    {
        Robot.Api.SendPrivateMessage(e.Sender.QQ, e.OriginText, false);
    }
}