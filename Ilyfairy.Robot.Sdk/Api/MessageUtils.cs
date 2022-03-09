using Ilyfairy.Robot.Sdk.Model.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.CSharpSdk.Api
{
    public class MessageUtils
    {
        /// <summary>
        /// Api的http地址
        /// </summary>
        public string BaseAddress { get; }

        public MessageUtils(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        /// <summary>
        /// 获取QQ头像链接
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public string GetAvatarUrl(long qq)
        {
            var url = $"http://q1.qlogo.cn/g?b=qq&nk={qq}&s=640";
            return url;
        }
        /// <summary>
        /// 获取QQ头像图片消息块
        /// </summary>
        /// <returns></returns>
        public ImageChunk GetQQAvatarChunk(long qq)
        {
            return new ImageChunk(GetAvatarUrl(qq));
        }
        /// <summary>
        /// 获取群头像消息块
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public ImageChunk GetGroupAvatarChunk(long group)
        {
            return new ImageChunk($"http://p.qlogo.cn/gh/{group}/{group}/640");
        }
    }
}
