using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// 宛先追加(POST)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_PARTICIPANTS_POST : REQ_COMMON
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public REQ_PARTICIPANTS_POST()
        {
            language_code = "ja";
        }

        /// <summary>宛先のメールアドレス</summary>
        [DataMember(Name = "email")]
        public string email { get; set; }

        /// <summary>宛先の名前</summary>
        [DataMember(Name = "name")]
        public string name { get; set; }

        /// <summary>宛先の会社名など</summary>
        [DataMember(Name = "organization")]
        public string organization { get; set; }

        /// <summary>表示をする際に要求されるアクセスコード</summary>
        [DataMember(Name = "access_code")]
        public string access_code { get; set; }

        /// <summary>受信者の言語設定。指定できる値はja（日本語）、en（英語）、zh-CHS（簡体字）、zh-CHT（繁体字）の４つ。何も設定しない場合の値はja</summary>
        [DataMember(Name = "language_code")]
        public string language_code { get; set; }
    }
}
