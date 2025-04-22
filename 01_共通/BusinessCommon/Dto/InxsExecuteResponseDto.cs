using System;
using Shougun.Core.Common.BusinessCommon.Enums;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    public class InxsExecuteResponseDto
    {
        public EnumExecuteAction Action { get; set; }
        public EnumMessageType MessageType { get; set; }
        public string ResponseMessage { get; set; }
    }
}
