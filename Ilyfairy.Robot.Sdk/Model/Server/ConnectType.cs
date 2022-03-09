namespace Ilyfairy.Robot.Sdk.Server
{
    /// <summary>
    /// 连接状态
    /// </summary>
    public enum ConnectType
    {
        /// <summary>
        /// 连接成功
        /// </summary>
        Success,
        /// <summary>
        /// 连接断开
        /// </summary>
        Lost,
        /// <summary>
        /// 连接错误/超时
        /// </summary>
        Error,
    }
}