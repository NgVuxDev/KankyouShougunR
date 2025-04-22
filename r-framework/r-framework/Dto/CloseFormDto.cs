using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Enums;

namespace r_framework.Dto
{
    public class CloseFormDto : INotificationModel
    {
        public string FormID { get; set; }
        public string Token { get; set; }
        public NotificationType Type { get; set; }
        public object[] Args { get; set; }
    }
}
