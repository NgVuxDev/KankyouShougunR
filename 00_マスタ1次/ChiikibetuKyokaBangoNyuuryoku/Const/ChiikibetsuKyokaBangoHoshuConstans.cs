// $Id: ChiikibetsuKyokaBangoHoshuConstans.cs 39265 2015-01-12 07:16:04Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace ChiikibetsuKyokaBangoHoshu.Const
{
    public class ChiikibetsuKyokaBangouNyuuryokuConstans
    {
        /// <summary>M_CHIIKIBETSU_KYOKAのKYOKA_KBN</summary>
        public static readonly string KYOKA_KBN = "KYOKA_KBN";

        /// <summary>M_CHIIKIBETSU_KYOKAのSHAIN_CD</summary>
        public static readonly string CHIIKI_CD = "CHIIKI_CD";

        /// <summary>M_CHIIKIのCHIIKI_NAME_RYAKU</summary>
        public static readonly string CHIIKI_NAME_RYAKU = "CHIIKI_NAME_RYAKU";

        /// <summary>M_CHIIKIBETSU_KYOKAのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CHIIKIBETSU_KYOKAのDELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_CHIIKIBETSU_KYOKAのGYOUSHA_CD</summary>
        public static readonly string GYOUSHA_CD = "GYOUSHA_CD";

        /// <summary>M_CHIIKIBETSU_KYOKAのGYOUSHA_NAME_RYAKU</summary>
        public static readonly string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";

        /// <summary>M_CHIIKIBETSU_KYOKAのGENBA_CD</summary>
        public static readonly string GENBA_CD = "GENBA_CD";

        /// <summary>M_CHIIKIBETSU_KYOKAのGENBA_NAME_RYAKU</summary>
        public static readonly string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";

        /// <summary>M_CHIIKIBETSU_KYOKAのTOKUBETSU_KANRI_KBN</summary>
        public static readonly string TOKUBETSU_KANRI_KBN = "TOKUBETSU_KANRI_KBN";

        ///// <summary>M_CHIIKIBETSU_KYOKAのHAIKI_KBN_CD</summary>
        //public static readonly string HAIKI_KBN_CD = "HAIKI_KBN_CD";

        /// <summary>M_CHIIKIBETSU_KYOKAのHAIKI_SHURUI_CD</summary>
        public static readonly string HAIKI_SHURUI_CD = "HOUKOKUSHO_BUNRUI_CD";

        /// <summary>M_CHIIKIBETSU_KYOKAのHAIKI_SHURUI_NAME_RYAKU</summary>
        public static readonly string HAIKI_SHURUI_NAME_RYAKU = "HOUKOKUSHO_BUNRUI_NAME_RYAKU";

        /// <summary>M_CHIIKIBETSU_KYOKA_MEIGARAのTSUMIKAE</summary>
        public static readonly string TSUMIKAE = "TSUMIKAE";

        /// <summary>M_CHIIKIBETSU_KYOKAのFUTSUU_KYOKA_BEGIN</summary>
        public static readonly string FUTSUU_KYOKA_BEGIN = "FUTSUU_KYOKA_BEGIN";

        /// <summary>M_CHIIKIBETSU_KYOKAのFUTSUU_KYOKA_END</summary>
        public static readonly string FUTSUU_KYOKA_END = "FUTSUU_KYOKA_END";

        /// <summary>M_CHIIKIBETSU_KYOKAのTOKUBETSU_KYOKA_BEGIN</summary>
        public static readonly string TOKUBETSU_KYOKA_BEGIN = "TOKUBETSU_KYOKA_BEGIN";

        /// <summary>M_CHIIKIBETSU_KYOKAのTOKUBETSU_KYOKA_END</summary>
        public static readonly string TOKUBETSU_KYOKA_END = "TOKUBETSU_KYOKA_END";

        /// <summary>廃棄物区分CD【普通】（カンマ区切り）</summary>
        //public static readonly string HAIKI_KBN_CD_FUTSUU = "HAIKI_KBN_CD_FUTSUU";

        /// <summary>廃棄物種類CD【普通】（カンマ区切り）</summary>
        public static readonly string HAIKI_SHURUI_CD_FUTSUU = "HOUKOKUSHO_BUNRUI_CD_FUTSUU";

        /// <summary>廃棄物種類名【普通】（カンマ区切り）</summary>
        public static readonly string HAIKI_SHURUI_NAME_FUTSUU = "HOUKOKUSHO_BUNRUI_NAME_RYAKU_FUTSUU";

        /// <summary>積替【普通】（カンマ区切り）</summary>
        public static readonly string TSUMIKAE_FUTSUU = "TSUMIKAE_FUTSUU";

        /// <summary>廃棄物区分CD【特別】（カンマ区切り）</summary>
        //public static readonly string HAIKI_KBN_CD_TOKUBETSU = "HAIKI_KBN_CD_TOKUBETSU";

        /// <summary>廃棄物種類CD【特別】（カンマ区切り）</summary>
        public static readonly string HAIKI_SHURUI_CD_TOKUBETSU = "HOUKOKUSHO_BUNRUI_CD_TOKUBETSU";

        /// <summary>廃棄物種類名【特別】（カンマ区切り）</summary>
        public static readonly string HAIKI_SHURUI_NAME_TOKUBETSU = "HOUKOKUSHO_BUNRUI_NAME_RYAKU_TOKUBETSU";

        /// <summary>積替【特別】（カンマ区切り）</summary>
        public static readonly string TSUMIKAE_TOKUBETSU = "TSUMIKAE_TOKUBETSU";

        /// <summary>必須チェック名</summary>
        public static readonly string REQUIRED_CHECK_NAME = "必須チェック";

        /// <summary>普通・特管有効期限がどちらもない場合のメッセージ</summary>
        public static readonly string NO_KYOKA_KIGEN_MESSAGE = "普通許可有効期限、特管許可有効期限のどちらかを入力してください。";

        /// <summary>M_CHIIKIBETSU_KYOKAのFILE_ID</summary>
        public static readonly string FILE_ID = "FILE_ID";

        /// <summary>
        /// セパレータ
        /// </summary>
        public static readonly char SEPARATOR = ',';

        /// <summary>
        /// 許可モード
        /// </summary>
        public enum KYOKA_MODE
        {
            /// <summary>
            /// 運搬
            /// </summary>
            UNPAN = 1,
            /// <summary>
            /// 処分
            /// </summary>
            SHOBUN = 2,
            /// <summary>
            /// 最終処分
            /// </summary>
            SAISHUSHOBUN = 3,
        }

    }
}
