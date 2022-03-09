using Ilyfairy.Robot.Sdk.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilyfairy.Robot.CSharpSdk.Model.Events.EventHeader
{
    public class ConnectEventArgs : EventArgs
    {
        public ConnectType Type { get; set; }
    }
}
