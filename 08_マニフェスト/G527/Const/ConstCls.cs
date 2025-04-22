using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
       
        /// <summary>
        /// 交付番号毎のカラム名
        /// </summary>
        public class KohuBangoGRDColName
        {
            public const string ROW_NO = "K_ROWNO";
            public const string KOUFU_DATE = "K_KOUFU_DATE";
            public const string MANIFEST_ID = "K_MANIFEST_ID";
            public const string SEND_A = "K_SEND_A";
            public const string SEND_B1 = "K_SEND_B1";
            public const string SEND_B2 = "K_SEND_B2";
            public const string SEND_B4 = "K_SEND_B4";
            public const string SEND_B6 = "K_SEND_B6";
            public const string SEND_C1 = "K_SEND_C1";
            public const string SEND_C2 = "K_SEND_C2";
            public const string SEND_D = "K_SEND_D";
            public const string SEND_E = "K_SEND_E";
        }

        /// <summary>
        /// 現場毎のカラム名
        /// </summary>
        public class GenbaGRDColName
        {
            public const string ROW_NO = "G_ROWNO";
            public const string HST_GYOUSHA_CD1 = "G_HST_GYOUSHA_CD1";
            public const string HST_GENBA_CD1 = "G_HST_GENBA_CD1";
            public const string HST_GYOUSHA_CD2 = "G_HST_GYOUSHA_CD2";
            public const string HST_GENBA_CD2 = "G_HST_GENBA_CD2";
            public const string HAIKI_SHURUI_CD = "G_HAIKI_SHURUI_CD";
            public const string KOUFU_DATE = "G_KOUFU_DATE";
            public const string MANIFEST_ID = "G_MANIFEST_ID";
            public const string SEND_A = "G_SEND_A";
            public const string SEND_B1 = "G_SEND_B1";
            public const string SEND_B2 = "G_SEND_B2";
            public const string SEND_B4 = "G_SEND_B4";
            public const string SEND_B6 = "G_SEND_B6";
            public const string SEND_C1 = "G_SEND_C1";
            public const string SEND_C2 = "G_SEND_C2";
            public const string SEND_D = "G_SEND_D";
            public const string SEND_E = "G_SEND_E";
        }

        /// <summary>
        /// 返却先集計のカラム名
        /// </summary>
        public class HenkyakusakiShukeiGRDColName
        {
            public const string ROW_NO = "H_ROWNO";
            public const string REF_DATE = "H_REF_DATE";
            public const string HENSOUSAKI_NAME = "H_HENSOUSAKI_NAME";
            public const string SEND_A = "H_SEND_A";
            public const string SEND_B1 = "H_SEND_B1";
            public const string SEND_B2 = "H_SEND_B2";
            public const string SEND_B4 = "H_SEND_B4";
            public const string SEND_B6 = "H_SEND_B6";
            public const string SEND_C1 = "H_SEND_C1";
            public const string SEND_C2 = "H_SEND_C2";
            public const string SEND_D = "H_SEND_D";
            public const string SEND_E = "H_SEND_E";
        }

        /// <summary>
        /// 帳票のカラム名
        /// </summary>
        public class ReportColName
        {
            public const string ROW_NO = "ROW_NO";
            public const string HST_GYOUSHA_CD1 = "HST_GYOUSHA_CD1";
            public const string HST_GENBA_CD1 = "HST_GENBA_CD1";
            public const string HST_GYOUSHA_CD2 = "HST_GYOUSHA_CD2";
            public const string HST_GENBA_CD2 = "HST_GENBA_CD2";
            public const string HAIKI_SHURUI_CD = "HAIKI_SHURUI_CD";
            public const string KOUFU_DATE = "KOUFU_DATE";
            public const string MANIFEST_ID = "MANIFEST_ID";
            public const string REF_DATE = "REF_DATE";
            public const string HENSOUSAKI_NAME = "HENSOUSAKI_NAME";
            public const string SEND_A = "SEND_A";
            public const string SEND_B1 = "SEND_B1";
            public const string SEND_B2 = "SEND_B2";
            public const string SEND_B4 = "SEND_B4";
            public const string SEND_B6 = "SEND_B6";
            public const string SEND_C1 = "SEND_C1";
            public const string SEND_C2 = "SEND_C2";
            public const string SEND_D = "SEND_D";
            public const string SEND_E = "SEND_E";
            public const string SUM_NUM = "SUM_NUM";
        }

    }
}
