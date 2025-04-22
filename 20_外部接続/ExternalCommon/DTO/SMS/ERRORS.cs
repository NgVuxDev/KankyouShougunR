using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS
{
    /// <summary>
    /// 送信リスト設定変更APIのレスポンス
    /// </summary>
    [DataContract]
    public class ERRORS : IApiDto
    {
        /// <summary>登録エラーの対象行</summary>
        [DataMember(Name = "errorrow")]
        public string Errorrow { get; set; }

        /// <summary>登録エラーの項目</summary>
        [DataMember(Name = "erroritem")]
        public string Erroritem { get; set; }

        /// <summary>ステータスコード</summary>
        [DataMember(Name = "errorstatus")]
        public string Errorstatus { get; set; }
    }
}
