using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Drawing;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    internal class ConstClass
    {
        internal static int TORIKOMI_DATA_LENGTH = 200;
        internal static TorikomiDataStruct TORIKOMI_DATA_KBN = new TorikomiDataStruct(1, 1);//データ区分
        internal static TorikomiDataStruct TORIKOMI_SUBETSU_CD = new TorikomiDataStruct(2, 2);//種別コード
        internal static TorikomiDataStruct TORIKOMI_BANK_RENKEI_CD = new TorikomiDataStruct(23, 4);//銀行コード
        internal static TorikomiDataStruct TORIKOMI_BANK_SHITEN_RENKEI_CD = new TorikomiDataStruct(42, 3);//支店コード
        internal static TorikomiDataStruct TORIKOMI_KOUZA_NO = new TorikomiDataStruct(64, 10);//口座番号
        internal static TorikomiDataStruct TORIKOMI_YONYUU_DATE = new TorikomiDataStruct(16, 6);//預入・払出日
        internal static TorikomiDataStruct TORIKOMI_HARAIDASHI_KBN = new TorikomiDataStruct(22, 1);//入払区分
        internal static TorikomiDataStruct TORIKOMI_TORIHIKI_KBN = new TorikomiDataStruct(23, 2);//取引区分
        internal static TorikomiDataStruct TORIKOMI_KINGAKU = new TorikomiDataStruct(25, 12);//取引金額
        internal static TorikomiDataStruct TORIKOMI_FURIKOMI_JINMEI = new TorikomiDataStruct(82, 48);//振込依頼人名または契約者番号
        internal static TorikomiDataStruct TORIKOMI_TEKIYOU_NAIYOU = new TorikomiDataStruct(160, 20);//摘要内容

        //-----------------------------------------------------------------------------------

        internal static string COL_SAKUSEI = "作成";
        internal static string COL_SAKUJO = "削除";
        internal static string COL_DENPYOU_SAKUSEI = "伝票作成";
        internal static string COL_YONYUU_DATE = "預入日";
        internal static string COL_TORIHIKISAKI_CD = "取引先CD";
        internal static string COL_TORIHIKISAKI_NAME = "取引先名";
        internal static string COL_NYUUKINSAKI_CD = "入金先CD";
        internal static string COL_NYUUKINSAKI_NAME = "入金先名";
        internal static string COL_KINGAKU = "入金額";
        internal static string COL_FURIKOMI_JINMEI = "振込人名";
        internal static string COL_BANK_CD = "銀行CD";
        internal static string COL_BANK_NAME = "銀行名";
        internal static string COL_BANK_SHITEN_CD = "銀行支店CD";
        internal static string COL_BANK_SHITEN_NAME = "銀行支店名";
        internal static string COL_KOUZA_SHURUI = "口座種類";
        internal static string COL_KOUZA_NO = "口座番号";
        internal static string COL_KOUZA_NAME = "口座名義";
        internal static string COL_TEKIYOU_NAIYOU = "摘要内容";
        internal static string COL_ERROR_NAME = "作成不可理由";
        internal static string COL_ERROR_CD = "EROR_CD";
        internal static string COL_ERROR_CD_ORIG = "ERROR_CD_ORIG";
        internal static string COL_ERROR_NAME_ORIG = "ERROR_NAME_ORIG";
        internal static string COL_TORIKOMI_NUMBER = "TORIKOMI_NUMBER";
        internal static string COL_ROW_NUMBER = "ROW_NUMBER";
        internal static string COL_TIME_STAMP = "TIME_STAMP";

        internal static Color YELLOW_COLOR = Color.FromArgb(255, 255, 0);
        internal static Color RED_COLOR = Color.FromArgb(255, 100, 100);
    }
}
