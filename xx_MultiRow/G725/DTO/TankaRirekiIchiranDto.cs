using System;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.TankaRirekiIchiran
{
    /// <summary>
    /// G725 単価履歴一覧DTO
    /// </summary>
    public class TankaRirekiIchiranDto
    {
        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public string KYOTEN_CD { get; set; }

        /// <summary>
        /// 取引先CDを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 業者CDを取得・設定します
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場CDを取得・設定します
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 運搬業者CDを取得・設定します
        /// </summary>
        public string UNPAN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷積業者CDを取得・設定します
        /// </summary>
        public string NIZUMI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷積現場CDを取得・設定します
        /// </summary>
        public string NIZUMI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷降業者CDを取得・設定します
        /// </summary>
        public string NIOROSHI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷降現場CDを取得・設定します
        /// </summary>
        public string NIOROSHI_GENBA_CD { get; set; }

        /// <summary>
        /// 伝票日付Fromを取得・設定します
        /// </summary>
        public SqlDateTime HIDZUKE_FROM { get; set; }

        /// <summary>
        /// 伝票日付Toを取得・設定します
        /// </summary>
        public SqlDateTime HIDZUKE_TO { get; set; }

        /// <summary>
        /// 伝票区分を取得・設定します
        /// </summary>
        public string DENPYOU_KBN { get; set; }

        /// <summary>
        /// 確定区分を取得・設定します
        /// </summary>
        public string KAKUTEI_KBN { get; set; }

        public string HINMEI_CD { get; set; }
    }
}
