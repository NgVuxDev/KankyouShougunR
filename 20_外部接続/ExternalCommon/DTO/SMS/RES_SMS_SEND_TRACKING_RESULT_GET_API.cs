using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS
{
    /// <summary>
    /// SMS送信結果取得（トラッキングコード）APIのリスト取得
    /// </summary>
    [DataContract]
    public class RES_SMS_SEND_TRACKING_RESULT_GET_API : IApiDto
    {
        /// <summary>ステータスコード</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>対象レコード数</summary>
        [DataMember(Name = "maxrecord")]
        public string Maxrecord { get; set; }

        /// <summary>リスト</summary>
        [DataMember(Name = "list")]
        public List<TRACKING_LIST> List { get; set; }
    }
}
