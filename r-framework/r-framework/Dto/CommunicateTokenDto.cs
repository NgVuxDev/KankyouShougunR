using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;

namespace r_framework.Dto
{
    public class CommunicateTokenDto : ITokenModel
    {
        public string TransactionId { get; set; }
        public object ReferenceID { get; set; }
    }
}
