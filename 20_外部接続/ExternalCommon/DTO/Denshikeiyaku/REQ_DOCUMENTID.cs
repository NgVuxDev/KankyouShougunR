using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// 書類作成(PUT)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_DOCUMENTID_PUT : REQ_COMMON
    {
        /// <summary>書類のタイトル</summary>
        [DataMember(Name = "title")]
        public string title { get; set; }

        /// <summary>書類についてのメモ。受信者には表示されない</summary>
        [DataMember(Name = "note")]
        public string note { get; set; }

        /// <summary>確認依頼メールに追加されるメッセージ</summary>
        [DataMember(Name = "message")]
        public string message { get; set; }

        /// <summary>受信者に転送を許可するフラグ。指定できる値はtrue、またはfalse。trueの代わりに1、t、T、TRUE、True、falseの代わりに0, f, F, FALSE, Falseを指定可能。デフォルトはfalse。</summary>
        /// <remarks></remarks>
        [DataMember(Name = "can_transfer")]
        public bool can_transfer { get; set; }
    }
}
