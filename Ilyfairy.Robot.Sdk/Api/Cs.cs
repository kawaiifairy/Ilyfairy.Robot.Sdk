using System.Diagnostics;

namespace Ilyfairy.Robot.Sdk.Api.Debug
{
    public class Cs
    {
        private List<(string, ConsoleColor, ConsoleColor, bool)> list = new();

        public static Cs Crate(string text, ConsoleColor force = ConsoleColor.Gray, ConsoleColor back = ConsoleColor.Black)
        {
            Cs co = new Cs();
            co.list.Add((text, force, back, false));
            return co;
        }

        public Cs Write(string text, ConsoleColor force = ConsoleColor.Gray, ConsoleColor back = ConsoleColor.Black)
        {
            list.Add((text, force, back, false));
            return this;
        }
        public Cs WriteLine(string text, ConsoleColor force = ConsoleColor.Gray, ConsoleColor back = ConsoleColor.Black)
        {
            list.Add((text, force, back, true));
            return this;
        }

        [Conditional("DEBUG")]
        public void Show()
        {
            foreach (var item in list)
            {
                var old_fore = Console.ForegroundColor;
                var old_back = Console.BackgroundColor;
                Console.ForegroundColor = item.Item2;
                Console.BackgroundColor = item.Item3;
                Console.Write(item.Item1);
                if (item.Item4) Console.Write('\n');
                Console.ForegroundColor = old_fore;
                Console.BackgroundColor = old_back;
            }
            list.Clear();
        }
    }
}