using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// 共有先の追加(POST)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_REPORTEES_POST : REQ_COMMON
    {
        /// <summary>共有先のメールアドレス</summary>
        [DataMember(Name = "email")]
        public string email { get; set; }

        /// <summary>共有先の名前</summary>
        [DataMember(Name = "name")]
        public string name { get; set; }

        /// <summary>共有先の会社名など</summary>
        [DataMember(Name = "organization")]
        public string organization { get; set; }
    }
}
