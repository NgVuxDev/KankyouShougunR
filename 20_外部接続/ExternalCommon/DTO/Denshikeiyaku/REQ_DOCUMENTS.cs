using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// 書類作成(POST)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_DOCUMENTS_POST : REQ_COMMON
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public REQ_DOCUMENTS_POST()
        {
            this.can_transfer = false;
        }

        public System.Int32 A { get; set; }

        /// <summary>書類のタイトル</summary>
        [DataMember(Name = "title")]
        public string title { get; set; }

        /// <summary>書類についてのメモ。受信者には表示されない</summary>
        [DataMember(Name = "note")]
        public string note { get; set; }

        /// <summary>確認依頼メールに追加されるメッセージ</summary>
        [DataMember(Name = "message")]
        public string message { get; set; }

        /// <summary>既存のテンプレートから書類作成を行う場合の元となるテンプレートの書類 ID</summary>
        [DataMember(Name = "template_id")]
        public string template_id { get; set; }

        /// <summary>受信者に転送を許可するフラグ。指定できる値はtrue、またはfalse。trueの代わりに1、t、T、TRUE、True、falseの代わりに0, f, F, FALSE, Falseを指定可能。デフォルトはfalse。</summary>
        /// <remarks></remarks>
        [DataMember(Name = "can_transfer")]
        public bool can_transfer { get; set; }
    }

    /// <summary>
    /// 書類作成(GET)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_DOCUMENTS_GET : REQ_COMMON
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public REQ_DOCUMENTS_GET()
        {
            page = 1;
            status = -1;
            with_files = "y";
            with_participants = "y";
        }

        /// <summary>値を渡さない場合、または不適切な値を渡した場合は 1 を利用する。データ量に対して大きすぎる値を渡した場合は documents の配列は空となる。</summary>
        [DataMember(Name = "page")]
        public int page { get; set; }

        /// <summary>値を渡さない場合は、全ての状態を対象として書類を取得する。指定可能な値は -1/0/1/2/3/4 のいずれか。</summary>
        [DataMember(Name = "status")]
        public int status { get; set; }

        /// <summary>ファイルの情報が不要な場合は、 y 以外の値を指定する。</summary>
        [DataMember(Name = "with_files")]
        public string with_files { get; set; }

        /// <summary>参加者の情報が不要な場合は、 y 以外の値を指定する。</summary>
        [DataMember(Name = "with_participants")]
        public string with_participants { get; set; }
    }
}
