using Shougun.Core.ExternalConnection.CommunicateLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Dtos
{
    public interface INotificationModel
    {
        string Token { get; set; }
        NotificationType Type { get; set; }
        object[] Args { get; set; }
    }
}
