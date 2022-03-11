using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Events
{
    public class EventBase : System.EventArgs
    {
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public long RobotQQ { get; set; }
    }
}
