using RobotPluginSdk;

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