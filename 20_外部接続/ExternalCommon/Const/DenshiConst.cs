using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// 電子契約(クラウドサイン)で使用する共通的な定数を定義
    /// </summary>
    public static class DenshiConst
    {
        /// <summary>REQ_COMMONで定義しているクライアントIDのプロパティ名</summary>
        public static readonly string CLIENT_ID = "client_id";

        #region S_LOGI_CONNECTテーブルのCONTENT_NAME
        public static readonly string CONTENT_NAME_TEST_BASE_URL = "TEST_BASE_URL";
        public static readonly string CONTENT_NAME_BASE_URL = "BASE_URL";
        public static readonly string CONTENT_NAME_TOKEN = "Token";
        public static readonly string CONTENT_NAME_DOCUMENTS = "Documents";
        public static readonly string CONTENT_NAME_DOCUMENTID = "DocumentID";
        public static readonly string CONTENT_NAME_DECLINE = "Decline";
        public static readonly string CONTENT_NAME_FILES = "Files";
        public static readonly string CONTENT_NAME_FILEID = "FileID";
        public static readonly string CONTENT_NAME_PARTICIPANTS = "Participants";
        public static readonly string CONTENT_NAME_PARTICIPANTID = "ParticipantID";
        public static readonly string CONTENT_NAME_WIDGETS = "Widgets";
        public static readonly string CONTENT_NAME_WIDGETID = "WidgetID";
        public static readonly string CONTENT_NAME_TEAM_DOCUMENTS = "Team_Documents";
        public static readonly string CONTENT_NAME_CERTIFICATE = "Certificate";
        public static readonly string CONTENT_NAME_ATTRIBUTE = "Attribute";
        public static readonly string CONTENT_NAME_REPORTEES = "Reportees";
        #endregion

        #region HTTPヘッダ付与
        /// <summary>HTTPヘッダー識別子</summary>
        public static readonly string HEADERS_X_APPID = "X-AppID";
        #endregion
    }
}
