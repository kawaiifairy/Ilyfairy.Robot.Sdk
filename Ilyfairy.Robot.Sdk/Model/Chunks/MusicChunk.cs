using Ilyfairy.Robot.Sdk.Model.Units;
using Ilyfairy.Robot.Sdk.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Chunks
{
    /// <summary>
    /// 音乐卡片
    /// </summary>
    public class MusicChunk : MessageChunk
    {
        public MusicCardType MusicType { get; set; }
        /// <summary>
        /// 非自定义卡片的音乐ID
        /// </summary>
        public long Id { get; set; }

        internal MusicChunk()
        {

        }

        /// <summary>
        /// 初始化自定义音乐卡片
        /// </summary>
        /// <param name="title"></param>
        /// <param name="clickUrl"></param>
        /// <param name="audioUrl"></param>
        /// <param name="imageUrl"></param>
        /// <param name="content"></param>
        public MusicChunk(string title, string clickUrl, string audioUrl, string? imageUrl = null, string? content = null)
        {
            title = title.CQEscape();
            clickUrl = clickUrl.CQEscape();
            audioUrl = audioUrl.CQEscape();
            imageUrl = imageUrl.CQEscape();
            content = content.CQEscape();

            OriginText = $"[CQ:music,type=custom,url={clickUrl},audio={audioUrl},title={title},image={imageUrl},content={content}]";
            MusicType = MusicCardType.custom;
            Type = CQCode.music;
        }

        /// <summary>
        /// 初始化新的音乐卡片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">如果需要使用MusicCardType.custom,则使用另外一个构造方法</param>
        public MusicChunk(long id, MusicCardType type)
        {
            string str;
            switch (type)
            {
                case MusicCardType.qq:
                    MusicType = MusicCardType.qq;
                    str = "qq";
                    break;
                case MusicCardType.xm:
                    MusicType = MusicCardType.xm;
                    str = "xm";
                    break;
                case MusicCardType._163:
                    MusicType = MusicCardType._163;
                    str = "163";
                    break;
                default:
                    MusicType = MusicCardType._163;
                    str = "163";
                    break;
            }
            OriginText = $"[CQ:music,type={str},id={id}]";
            Type = CQCode.music;
        }

    }
}
