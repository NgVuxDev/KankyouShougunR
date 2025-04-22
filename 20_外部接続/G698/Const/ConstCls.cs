using System.Drawing;

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>API連携用のCSVファイル名</summary>
        public static readonly string CSV_FILE_NAME = "配送計画一括削除";

        /// <summary>最適化対象：1.コースマスタ</summary>
        public static readonly short SAITEKIKA_TAISHO_1 = 1;
        /// <summary>最適化対象：2.定期配車伝票</summary>
        public static readonly short SAITEKIKA_TAISHO_2 = 2;

        /// <summary>時間取得：1.要</summary>
        public static readonly string NAVI_GET_TIME_1 = "1";
        /// <summary>時間取得：2.不要</summary>
        public static readonly string NAVI_GET_TIME_2 = "2";

        #region 一覧用
        /// <summary>希望時間</summary>
        public static readonly string BEFORE_KIBOU_TIME = "BEFORE_KIBOU_TIME";
        #endregion

        #region 実績明細一覧用
        /// <summary>連番</summary>
        public static readonly string AFTER_INDEX_NO = "AFTER_INDEX_NO";
        /// <summary>変更前</summary>
        public static readonly string AFTER_PRE_ROW_NO = "AFTER_PRE_ROW_NO";
        /// <summary>業者CD</summary>
        public static readonly string AFTER_GYOUSHA_CD = "AFTER_GYOUSHA_CD";
        /// <summary>現場CD</summary>
        public static readonly string AFTER_GENBA_CD = "AFTER_GENBA_CD";
        /// <summary>現場名称</summary>
        public static readonly string AFTER_GENBA_NAME_RYAKU = "AFTER_GENBA_NAME_RYAKU";
        /// <summary>順番</summary>
        public static readonly string AFTER_ROW_NO = "AFTER_ROW_NO";
        /// <summary>希望時間</summary>
        public static readonly string AFTER_KIBOU_TIME = "AFTER_KIBOU_TIME";
        /// <summary>到着予定時刻</summary>
        public static readonly string AFTER_ESTIMATED_ARRIVAL_TIME = "AFTER_ESTIMATED_ARRIVAL_TIME";
        /// <summary>備考</summary>
        public static readonly string AFTER_BIKOU = "AFTER_BIKOU";
        #endregion

        /// <summary>希望時間＜到着予定時間 のセル背景色</summary>
        public static readonly Color COLOR_RED = Color.Red;
        /// <summary>希望時間＞到着予定時間 のセル背景色</summary>
        public static readonly Color COLOR_YELLOW = Color.Yellow;
    }
}
