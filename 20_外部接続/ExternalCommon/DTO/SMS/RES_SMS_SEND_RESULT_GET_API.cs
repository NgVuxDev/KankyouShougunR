using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS
{
    /// <summary>
    /// SMS送信結果取得APIのレスポンス
    /// </summary>
    [DataContract]
    public class RES_SMS_SEND_RESULT_GET_API : IApiDto
    {
        /// <summary>ステータスコード</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>メッセージ状態コード</summary>
        [DataMember(Name = "messagestatus")]
        public string Messagestatus { get; set; }

        /// <summary>送達結果コード</summary>
        [DataMember(Name = "resultstatus")]
        public string Resultstatus { get; set; }

        /// <summary>メッセージ送信日時</summary>
        [DataMember(Name = "senddate")]
        public string Senddate { get; set; }

        /// <summary>携帯キャリア</summary>
        [DataMember(Name = "carrier")]
        public string Carrier { get; set; }

        /// <summary>送信時メッセージ（ドコモ）</summary>
        [DataMember(Name = "docomoMessage")]
        public string DocomoMessage { get; set; }

        /// <summary>送信時メッセージ（ソフトバンク）</summary>
        [DataMember(Name = "softbankMessage")]
        public string SoftbankMessage { get; set; }

        /// <summary>送信時メッセージ（au）</summary>
        [DataMember(Name = "auMessage")]
        public string AuMessage { get; set; }

        /// <summary>送信時メッセージ（楽天）</summary>
        [DataMember(Name = "rakutenMessage")]
        public string RakutenMessage { get; set; }

        /// <summary>送信時メッセージ（オプション携帯キャリア）</summary>
        [DataMember(Name = "optionMessage")]
        public string OptionMessage { get; set; }

        /// <summary>短縮URLのクリック数</summary>
        [DataMember(Name = "click")]
        public string click { get; set; }
    }
}
