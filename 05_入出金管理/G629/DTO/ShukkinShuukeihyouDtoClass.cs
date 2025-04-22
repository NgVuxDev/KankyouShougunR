using System.Collections.Generic;

namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou
{
    /// <summary>
    /// 入金集計表出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class ShukkinShuukeihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ShukkinShuukeihyouDtoClass()
        {
            Pattern = null;
            KyotenCd = 99;
            KyotenName = null;
            DateFrom = null;
            DateTo = null;
            TorihikisakiCdFrom = null;
            TorihikisakiFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiTo = null;
            ShukkinKbnCdFrom = null;
            ShukkinKbnFrom = null;
            ShukkinKbnCdTo = null;
            ShukkinKbnTo = null;
            IsGroupDenpyouNumber = true;
            IsGroupTorihikisaki = true;
            Jyouken1 = null;
            Jyouken2 = null;
            ShuukeiIsChecked = new Dictionary<string, string>();
        }

        /// <summary>
        /// パターン
        /// </summary>
        public PatternDto Pattern { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名称を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 日付種類CDを取得・設定します
        /// </summary>
        public int DateShuruiCd { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先Fromを取得・設定します
        /// </summary>
        public string TorihikisakiFrom { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先Toを取得・設定します
        /// </summary>
        public string TorihikisakiTo { get; set; }

        /// <summary>
        /// 出金区分CDFromを取得・設定します
        /// </summary>
        public string ShukkinKbnCdFrom { get; set; }

        /// <summary>
        /// 出金区分Fromを取得・設定します
        /// </summary>
        public string ShukkinKbnFrom { get; set; }

        /// <summary>
        /// 出金区分CDToを取得・設定します
        /// </summary>
        public string ShukkinKbnCdTo { get; set; }

        /// <summary>
        /// 出金区分Toを取得・設定します
        /// </summary>
        public string ShukkinKbnTo { get; set; }

        /// <summary>
        /// 伝票番号単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouNumber { get; set; }

        /// <summary>
        /// 取引先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupTorihikisaki { get; set; }

        /// <summary>
        /// 帳票に表示する条件1
        /// </summary>
        public string Jyouken1 { get; set; }

        /// <summary>
        /// 帳票に表示する条件1
        /// </summary>
        public string Jyouken2 { get; set; }

        /// <summary>
        /// 集計チェックありの項目を格納
        /// </summary>
        public Dictionary<string, string> ShuukeiIsChecked { get; set; }
    }
}
