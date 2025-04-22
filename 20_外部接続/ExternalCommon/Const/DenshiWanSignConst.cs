
namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// 
    /// </summary>
    public static class DenshiWanSignConst
    {
        /// <summary>ベースURL</summary>
        public static readonly string URL = "https://service10-api.wanbishi.ne.jp/wansign-api/v0/api/";

        /// <summary>
        /// アクセストークン生成
        /// </summary>
        public static readonly string ROUTE_ACCESS_TOKEN = "accesstoken/generate";

        /// <summary>
        /// 関連コード取得API
        /// </summary>
        public static readonly string ROUTE_CONTROL_NUMBER = "document/controlNumber";

        /// <summary>
        /// 文書詳細情報取得API
        /// </summary>
        public static readonly string ROUTE_KEIYAKU_INFO = "document/detail/get";

        /// <summary>
        /// 文書状態取得API
        /// </summary>
        public static readonly string ROUTE_KEIYAKU_STATUS = "document/status";

        /// <summary>
        /// 文書取得API
        /// </summary>
        public static readonly string ROUTE_KEIYAKU_DOWNLOAD = "document/getAll";

        /// <summary>
        /// 文書詳細情報編集API
        /// </summary>
        public static readonly string ROUTE_DOCUMENT_DETAIL_UPDATE = "document/detail/edit";

        /// <summary>
        /// メッセージAを表示
        /// </summary>
        public static readonly string MsgA = "アクセストークン取得に失敗しました。システム管理者に問合せください。（status＝{0}）";

        /// <summary>
        /// メッセージLを表示
        /// </summary>
        public static readonly string MsgL = "文書のダウンロードに失敗しました。システム管理者に問合せください。（status＝{0}）";
    }
}
