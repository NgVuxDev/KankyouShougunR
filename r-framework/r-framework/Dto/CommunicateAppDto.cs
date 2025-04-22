using r_framework.Const;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Enums;

namespace r_framework.Dto
{
    public class CommunicateAppDto : INotificationModel
    {
        public string FormID { get; set; }
        public WINDOW_TYPE? WindowType { get; set; }
        public string ShainCD { get; set; }
        public string Token { get; set; }
        public NotificationType Type { get; set; }
        public object[] Args { get; set; }
    }
}
