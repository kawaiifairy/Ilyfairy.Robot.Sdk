using RobotPluginSdk;
using System.Reflection;

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

class PluginManager
{
    public List<Plugin> Plugins { get; set; } = new();

    public Plugin Load(string file)
    {
        file = Path.GetFullPath(file);
        if (!File.Exists(file))
        {
            throw new Exception($"找不到指定的文件: {file}");
        }
        Assembly asm = null;
        try
        {
            asm = Assembly.LoadFile(file);
        }
        catch (Exception e)
        {
            throw new Exception($"dll加载异常: {file}\n{e.Message}");
        }
        TypeInfo? typeInfo = asm.DefinedTypes.FirstOrDefault(v => v.BaseType == typeof(PluginBase));
        if (typeInfo == null)
        {
            throw new Exception($"找不到 PluginBase 的派生类: {file}");
        }
        var obj = Activator.CreateInstance(typeInfo) as PluginBase;
        if (obj == null)
        {
            throw new Exception($"初始化 PluginBase 新实例异常");
        }

        Plugin plugin = new();
        plugin.Instance = obj;
        plugin.File = file;
        plugin.Info = obj.Info;

        Plugins.Add(plugin);
        return plugin;
    }

}

class Plugin
{
    public string File { get; set; }
    public PluginBase Instance { get; set; }
    public PluginInfo Info { get; set; }

    public void Start()
    {
        Instance.Start();
    }

    public void Close()
    {
        Instance.Close();
    }

}