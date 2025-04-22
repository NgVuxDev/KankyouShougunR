using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    // リクエストクラス作成時の注意点
    // プロパティを新規追加する場合、string以外のプリミティブ型は初期値(default)に注意
    // 初期値のままだとAPI通信から除外される
    // 詳細は「20_外部接続/ExternalCommon/Logic/DenshiLogic.cs」のCreateJsonStringメソッドを参照

    /// <summary>
    /// ファイル追加(POST)用のリクエストクラス
    /// </summary>
    [DataContract]
    public class REQ_FILES_POST : REQ_COMMON
    {
        /// <summary>ファイル名</summary>
        [DataMember(Name = "name")]
        public string name { get; set; }

        /// <summary>ファイルパス</summary>
        [DataMember(Name = "uploadfile")]
        public string uploadfile { get; set; }
    }
}
