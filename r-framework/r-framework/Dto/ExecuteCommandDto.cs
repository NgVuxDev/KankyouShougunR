using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Enums;

namespace r_framework.Dto
{
    public class ExecuteCommandDto : INotificationModel
    {
        public string Token { get; set; }
        public NotificationType Type { get; set; }
        public object[] Args { get; set; }
    }
}
