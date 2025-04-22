using System;
using System.Data.SqlTypes;

namespace Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou
{
    /// <summary>
    /// 支払データエンティティ
    /// </summary>
    public class ShiharaiData
    {
        /// <summary>
        /// 取引区分CDを取得・設定します
        /// </summary>
        public String SHIHARAI_TORIHIKI_KBN_CD { get; set; }

        /// <summary>
        /// 取引区分を取得・設定します
        /// </summary>
        public String SHIHARAI_TORIHIKI_KBN_NAME { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public String KYOTEN_CD { get; set; }

        /// <summary>
        /// 拠点を取得・設定します
        /// </summary>
        public String KYOTEN_NAME { get; set; }

        /// <summary>
        /// 取引先CDを取得・設定します
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 取引先を取得・設定します
        /// </summary>
        public String TORIHIKISAKI_NAME { get; set; }

        /// <summary>
        /// 業者CDを取得・設定します
        /// </summary>
        public String GYOUSHA_CD { get; set; }

        /// <summary>
        /// 業者を取得・設定します
        /// </summary>
        public String GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 現場CDを取得・設定します
        /// </summary>
        public String GENBA_CD { get; set; }

        /// <summary>
        /// 現場を取得・設定します
        /// </summary>
        public String GENBA_NAME { get; set; }

        /// <summary>
        /// 荷降業者CDを取得・設定します
        /// </summary>
        public String NIOROSHI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷降業者を取得・設定します
        /// </summary>
        public String NIOROSHI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷降現場CDを取得・設定します
        /// </summary>
        public String NIOROSHI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷降現場を取得・設定します
        /// </summary>
        public String NIOROSHI_GENBA_NAME { get; set; }

        /// <summary>
        /// 荷積業者CDを取得・設定します
        /// </summary>
        public String NIZUMI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷積業者を取得・設定します
        /// </summary>
        public String NIZUMI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷積現場CDを取得・設定します
        /// </summary>
        public String NIZUMI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷積現場を取得・設定します
        /// </summary>
        public String NIZUMI_GENBA_NAME { get; set; }

        /// <summary>
        /// 営業担当者CDを取得・設定します
        /// </summary>
        public String EIGYOU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 営業担当者を取得・設定します
        /// </summary>
        public String EIGYOU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 入力担当者CDを取得・設定します
        /// </summary>
        public String NYUURYOKU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 入力担当者を取得・設定します
        /// </summary>
        public String NYUURYOKU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 車輌CDを取得・設定します
        /// </summary>
        public String SHARYOU_CD { get; set; }

        /// <summary>
        /// 車輌を取得・設定します
        /// </summary>
        public String SHARYOU_NAME { get; set; }

        /// <summary>
        /// 車種CDを取得・設定します
        /// </summary>
        public String SHASHU_CD { get; set; }

        /// <summary>
        /// 車種を取得・設定します
        /// </summary>
        public String SHASHU_NAME { get; set; }

        /// <summary>
        /// 運搬業者CDを取得・設定します
        /// </summary>
        public String UNPAN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 運搬業者を取得・設定します
        /// </summary>
        public String UNPAN_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 運転者CDを取得・設定します
        /// </summary>
        public String UNTENSHA_CD { get; set; }

        /// <summary>
        /// 運転者を取得・設定します
        /// </summary>
        public String UNTENSHA_NAME { get; set; }

        /// <summary>
        /// 形態区分CDを取得・設定します
        /// </summary>
        public String KEITAI_KBN_CD { get; set; }

        /// <summary>
        /// 形態区分を取得・設定します
        /// </summary>
        public String KEITAI_KBN_NAME { get; set; }

        /// <summary>
        /// 台貫区分CDを取得・設定します
        /// </summary>
        public String DAIKAN_KBN_CD { get; set; }

        /// <summary>
        /// 台貫区分を取得・設定します
        /// </summary>
        public String DAIKAN_KBN_NAME { get; set; }

        /// <summary>
        /// 品名CDを取得・設定します
        /// </summary>
        public String HINMEI_CD { get; set; }

        /// <summary>
        /// 品名を取得・設定します
        /// </summary>
        public String HINMEI_NAME { get; set; }

        /// <summary>
        /// 正味重量を取得・設定します
        /// </summary>
        public SqlDecimal NET_JYUURYOU { get; set; }

        /// <summary>
        /// 数量を取得・設定します
        /// </summary>
        public SqlDecimal SUURYOU { get; set; }

        /// <summary>
        /// 単位CDを取得・設定します
        /// </summary>
        public String UNIT_CD { get; set; }

        /// <summary>
        /// 単位を取得・設定します
        /// </summary>
        public String UNIT_NAME { get; set; }

        /// <summary>
        /// 金額を取得・設定します
        /// </summary>
        public SqlDecimal KINGAKU { get; set; }

        /// <summary>
        /// 昨年金額を取得・設定します
        /// </summary>
        public SqlDecimal PAST_KINGAKU { get; set; }

        /// <summary>
        /// 差金額を取得・設定します
        /// </summary>
        public SqlDecimal SAGAKU { get; set; }

        /// <summary>
        /// 増加率を取得・設定します
        /// </summary>
        public SqlDecimal ZOKA_RITSU { get; set; }

        /// <summary>
        /// 種類CDを取得・設定します
        /// </summary>
        public String SHURUI_CD { get; set; }

        /// <summary>
        /// 種類を取得・設定します
        /// </summary>
        public String SHURUI_NAME { get; set; }

        /// <summary>
        /// 分類CDを取得・設定します
        /// </summary>
        public String BUNRUI_CD { get; set; }

        /// <summary>
        /// 分類を取得・設定します
        /// </summary>
        public String BUNRUI_NAME { get; set; }
    }
}
