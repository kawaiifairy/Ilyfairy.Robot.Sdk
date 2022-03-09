class Program
{

    static void Main(string[] args)
    {
        PluginManager manager = new();

        string input;
        while (true)
        {
            input = Console.ReadLine();
            if (input.StartsWith("load "))
            {
                var file = input[5..].Trim();
                try
                {
                    var plugin = manager.Load(file);
                    Console.WriteLine($"插件加载成功: {plugin.File}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
