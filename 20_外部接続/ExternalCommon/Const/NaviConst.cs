using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.ExternalCommon.Const
{
    /// <summary>
    /// NAVI TIMEで使用する共通的な定数を定義
    /// </summary>
    public static class NaviConst
    {
        /// <summary>ナビタイムテスト環境のベースURL(企業コード含む)</summary>
        public static readonly string naviURL = "https://fleet-test.navitime.biz/api/ednavi7215/";
        /// <summary>訪問先マスタ一括登録</summary>
        public static readonly string naviUPLOAD_VISIT = "master/UploadVisit";
        /// <summary>訪問先マスタ一括登録の結果確認</summary>
        public static readonly string naviCHECK_UPLOAD_VISIT = "master/CheckUploadVisit";
        /// <summary>配送計画情報一括登録</summary>
        public static readonly string naviUPLOAD = "allocate/Upload";
        /// <summary>配送計画情報一括登録の結果確認</summary>
        public static readonly string naviCHECK_UPLOAD = "allocate/CheckUpload";
        /// <summary>ユーザーマスタ一括登録</summary>
        public static readonly string naviUPLOAD_USER = "master/UploadUser";
        /// <summary>案件の実績情報取得</summary>
        public static readonly string naviGET_EXPERIENCE = "results/GetExperience";
        /// <summary>案件の到着予定時刻取得</summary>
        public static readonly string naviGET_ARRIVAL_TIME = "results/GetArrivalTime";
        /// <summary>案件削除</summary>
        public static readonly string naviDELETE_MATTER = "acceptance/DeleteMatter";

        #region S_NAVI_CONNECTテーブルのCONTENT_NAME
        /// <summary>テスト用ベースURL</summary>
        public static readonly string CONTENT_NAME_TEST_BASE_URL = "TEST_BASE_URL";
        /// <summary>ベースURL</summary>
        public static readonly string CONTENT_NAME_BASE_URL = "BASE_URL";
        /// <summary>訪問先マスタ一括登録</summary>
        public static readonly string CONTENT_NAME_UPLOAD_VISIT = "UploadVisit";
        /// <summary>訪問先マスタ一括登録の結果確認</summary>
        public static readonly string CONTENT_NAME_CHECK_UPLOAD_VISIT = "CheckUploadVisit";
        /// <summary>配送計画情報一括登録</summary>
        public static readonly string CONTENT_NAME_UPLOAD = "Upload";
        /// <summary>配送計画情報一括登録の結果確認</summary>
        public static readonly string CONTENT_NAME_CHECK_UPLOAD = "CheckUpload";
        /// <summary>ユーザーマスタ一括登録</summary>
        public static readonly string CONTENT_NAME_UPLOAD_USER = "UploadUser";
        /// <summary>案件の実績情報取得</summary>
        public static readonly string CONTENT_NAME_GET_EXPERIENCE = "GetExperience";
        /// <summary>案件の到着予定時刻取得</summary>
        public static readonly string CONTENT_NAME_GET_ARRIVAL_TIME = "GetArrivalTime";
        #endregion
    }
}
