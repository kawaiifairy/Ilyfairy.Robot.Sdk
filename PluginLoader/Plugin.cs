﻿using Ilyfairy.Robot.Sdk.Plugin;


namespace Ilyfairy.Robot.PluginLoader
{
    public class Plugin
    {
        public string File { get; set; }
        public PluginBase Instance { get; set; }
        public PluginInfo Info { get; set; }
        public bool Enabled { get; set; }
    }
}
