using Ilyfairy.Robot.Sdk.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.Sdk.Model.Events
{
    public class ConnectEventArgs : EventBase
    {
        public ConnectType Type { get; set; }
    }
}
