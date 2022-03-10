class Program
{
    static void Main(string[] args)
    {
        PluginManager manager = new();
        Directory.CreateDirectory("plugins");
        var files = Directory.GetFiles("plugins", "*.dll");
        Console.WriteLine($"plugins下一共检测到{files.Length}个插件");
        foreach (var file in files)
        {
            try
            {
                var plugin = manager.Load(file);
                plugin.Start();
                Console.WriteLine($"插件加载成功: {Path.GetFileName(plugin.File)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Thread.Sleep(-1);
    }
}
