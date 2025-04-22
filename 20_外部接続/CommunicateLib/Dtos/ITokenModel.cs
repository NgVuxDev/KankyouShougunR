using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Dtos
{
    public interface ITokenModel
    {
        string TransactionId { get; set; }
        object ReferenceID { get; set; }
    }
}
