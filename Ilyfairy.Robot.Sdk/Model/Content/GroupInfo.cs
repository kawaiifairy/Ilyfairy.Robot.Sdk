namespace Ilyfairy.Robot.Sdk.Model.Content
{
    public class GroupInfo
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 群名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 群备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 群创建时间
        /// </summary>
        public uint CreateTime { get; set; }
        /// <summary>
        /// 群等级
        /// </summary>
        public uint Level { get; set; }
        /// <summary>
        /// 当前群人数
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 最大成员数 (群容量)
        /// </summary>
        public int MaxMemberCount { get; set; }
    }
}
 