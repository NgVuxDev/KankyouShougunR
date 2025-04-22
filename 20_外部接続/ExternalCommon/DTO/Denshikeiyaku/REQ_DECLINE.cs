using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// 書類却下(PUT)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_DECLINE_PUT : REQ_COMMON
    {
        /// <summary>却下理由を表す文字列。1000文字未満まで入力可能</summary>
        [DataMember(Name = "comment ")]
        public string comment { get; set; }
    }
}
