// $Id: UIForm.cs 4357 2013-10-22 00:18:55Z sys_dev_12 $
namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
	/// <summary>
	/// 検収入力画面定義クラス
	/// </summary>
	public class ConstClass
    {
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        public const string ButtonInfoXmlPath = "Shougun.Core.Inspection.KenshuMeisaiNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 出荷複写時メッセージ
        /// </summary>
        public const string copyConfirmMsg = "既存の入力データを破棄して出荷明細行から複写を";

        /// <summary>
        /// 再読込時メッセージ
        /// </summary>
        public const string reLoadConfirmMsg = "既存の入力データを破棄して再読込を";

        /// <summary>
        /// 取り消し時メッセージ
        /// </summary>
        public const string cancelConfirmMsg = "追記したデータは破棄されますが宜しいですか？";

        internal const string CELL_HINMEI_CD = "HINMEI_CD";
        internal const string CELL_HINMEI_NAME = "HINMEI_NAME";
        internal const string CELL_BUBIKI = "BUBIKI";
        internal const string CELL_KENSHU_NET = "KENSHU_NET";
        internal const string CELL_SUURYOU = "SUURYOU";
        internal const string CELL_UNIT_CD = "UNIT_CD";
        internal const string CELL_TANKA = "TANKA";
        internal const string CELL_SHOW_KINGAKU = "SHOW_KINGAKU";

    }
}
