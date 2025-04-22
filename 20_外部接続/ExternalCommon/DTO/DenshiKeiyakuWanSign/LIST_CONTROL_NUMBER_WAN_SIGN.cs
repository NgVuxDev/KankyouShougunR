using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 関連コードのレスポンス
    /// </summary>
    [DataContract]
    public class LIST_CONTROL_NUMBER_WAN_SIGN
    {
        /// <summary>
        /// 文書情報をリストで返却します
        /// </summary>
        [DataMember(Name = "document")]
        public List<CONTROL_NUMBER_WAN_SIGN> Document { get; set; }
    }
}