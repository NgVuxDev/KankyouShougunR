using System;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 関連コードのレスポンス
    /// </summary>
    [DataContract]
    public class RESULT_CONTROL_NUMBER_WAN_SIGN : RESPONSE_WAN
    {
        /// <summary>
        /// リクエスト結果
        /// </summary>
        [DataMember(Name = "result")]
        public LIST_CONTROL_NUMBER_WAN_SIGN Result { get; set; }
    }
}