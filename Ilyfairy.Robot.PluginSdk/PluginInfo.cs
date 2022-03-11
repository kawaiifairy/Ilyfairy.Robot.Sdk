namespace Ilyfairy.Robot.Sdk.Plugin
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// 插件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
    }
}