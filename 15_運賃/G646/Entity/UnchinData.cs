using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 売上データエンティティ
    /// </summary>
    public class UnchinData
    {
        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public string KYOTEN_CD { get; set; }

        /// <summary>
        /// 拠点を取得・設定します
        /// </summary>
        public string KYOTEN_NAME { get; set; }

        /// <summary>
        /// 取引先CDを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 取引先を取得・設定します
        /// </summary>
        public string TORIHIKISAKI_NAME { get; set; }

        /// <summary>
        /// 業者CDを取得・設定します
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 業者を取得・設定します
        /// </summary>
        public string GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 現場CDを取得・設定します
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 現場を取得・設定します
        /// </summary>
        public string GENBA_NAME { get; set; }

        /// <summary>
        /// 荷降業者CDを取得・設定します
        /// </summary>
        public string NIOROSHI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷降業者を取得・設定します
        /// </summary>
        public string NIOROSHI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷降現場CDを取得・設定します
        /// </summary>
        public string NIOROSHI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷降現場を取得・設定します
        /// </summary>
        public string NIOROSHI_GENBA_NAME { get; set; }

        /// <summary>
        /// 荷積業者CDを取得・設定します
        /// </summary>
        public string NIZUMI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷積業者を取得・設定します
        /// </summary>
        public string NIZUMI_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 荷積現場CDを取得・設定します
        /// </summary>
        public string NIZUMI_GENBA_CD { get; set; }

        /// <summary>
        /// 荷積現場を取得・設定します
        /// </summary>
        public string NIZUMI_GENBA_NAME { get; set; }

        /// <summary>
        /// 営業担当者CDを取得・設定します
        /// </summary>
        public string EIGYOU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 営業担当者を取得・設定します
        /// </summary>
        public string EIGYOU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 入力担当者CDを取得・設定します
        /// </summary>
        public string NYUURYOKU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 入力担当者を取得・設定します
        /// </summary>
        public string NYUURYOKU_TANTOUSHA_NAME { get; set; }

        /// <summary>
        /// 車輌CDを取得・設定します
        /// </summary>
        public string SHARYOU_CD { get; set; }

        /// <summary>
        /// 車輌を取得・設定します
        /// </summary>
        public string SHARYOU_NAME { get; set; }

        /// <summary>
        /// 車種CDを取得・設定します
        /// </summary>
        public string SHASHU_CD { get; set; }

        /// <summary>
        /// 車種を取得・設定します
        /// </summary>
        public string SHASHU_NAME { get; set; }

        /// <summary>
        /// 運搬業者CDを取得・設定します
        /// </summary>
        public string UNPAN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 運搬業者を取得・設定します
        /// </summary>
        public string UNPAN_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 運転者CDを取得・設定します
        /// </summary>
        public string UNTENSHA_CD { get; set; }

        /// <summary>
        /// 運転者を取得・設定します
        /// </summary>
        public string UNTENSHA_NAME { get; set; }

        /// <summary>
        /// 形態区分CDを取得・設定します
        /// </summary>
        public string KEITAI_KBN_CD { get; set; }

        /// <summary>
        /// 形態区分を取得・設定します
        /// </summary>
        public string KEITAI_KBN_NAME { get; set; }

        /// <summary>
        /// 台貫区分CDを取得・設定します
        /// </summary>
        public string DAIKAN_KBN_CD { get; set; }

        /// <summary>
        /// 台貫区分を取得・設定します
        /// </summary>
        public string DAIKAN_KBN_NAME { get; set; }

        /// <summary>
        /// 品名CDを取得・設定します
        /// </summary>
        public string HINMEI_CD { get; set; }

        /// <summary>
        /// 品名を取得・設定します
        /// </summary>
        public string HINMEI_NAME { get; set; }

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
        public string UNIT_CD { get; set; }

        /// <summary>
        /// 単位を取得・設定します
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// 金額を取得・設定します
        /// </summary>
        public SqlDecimal KINGAKU { get; set; }

        /// <summary>
        /// 種類CDを取得・設定します
        /// </summary>
        public string SHURUI_CD { get; set; }

        /// <summary>
        /// 種類を取得・設定します
        /// </summary>
        public string SHURUI_NAME { get; set; }

        /// <summary>
        /// 分類CDを取得・設定します
        /// </summary>
        public string BUNRUI_CD { get; set; }

        /// <summary>
        /// 分類を取得・設定します
        /// </summary>
        public string BUNRUI_NAME { get; set; }
    }
}
