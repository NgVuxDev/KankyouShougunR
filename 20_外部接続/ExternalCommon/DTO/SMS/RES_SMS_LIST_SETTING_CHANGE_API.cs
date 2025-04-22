using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS
{
    /// <summary>
    /// 送信リスト設定変更APIのエラー詳細取得
    /// </summary>
    [DataContract]
    public class RES_SMS_LIST_SETTING_CHANGE_API : IApiDto
    {
        /// <summary>ステータスコード</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>送信リストの登録受付数</summary>
        [DataMember(Name = "request")]
        public string Request { get; set; }

        /// <summary>登録成功数</summary>
        [DataMember(Name = "success")]
        public string Success { get; set; }

        /// <summary>登録エラー件数</summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }

        /// <summary>登録エラーの詳細</summary>
        [DataMember(Name = "errors")]
        public List<ERRORS> Errors { get; set; }
    }
}
