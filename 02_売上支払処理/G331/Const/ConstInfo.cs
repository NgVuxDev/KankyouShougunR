using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
{
    internal class ConstInfo
    {
        #region 定数
        //エラーメッセージ用引数
        internal const String ERR_ARGS_KYOTEN_MASTER = "拠点マスタ";
        internal const String ERR_ARGS_TORIHIKISAKI_MASTER = "取引先マスタ";
        internal const String ERR_ARGS_GYOSYA_MASTER = "業者マスタ";
        internal const String ERR_ARGS_GENBA_MASTER = "現場マスタ";
        internal const String ERR_ARGS_SHIMEBI = "締日";
        internal const String ERR_ARGS_TARGET_KIKAN = "対象期間";
        internal const String ERR_ARGS_TARGET_KIKAN_FROM = "対象期間FROM";
        internal const String ERR_ARGS_TARGET_KIKAN_TO = "対象期間TO";
        internal const String ERR_ARGS_SEIKYU_DATE = "売上日付";
        internal const String ERR_ARGS_CUREATE_DATA = "作成データ";

        //エラーメッセージID
        internal const String ERR_MSG_CD_E002 = "E002";
        internal const String ERR_MSG_CD_E011 = "E011";
        internal const String ERR_MSG_CD_E012 = "E012";
        internal const String ERR_MSG_CD_E027 = "E027";
        internal const String ERR_MSG_CD_C001 = "C001";
        internal const String C030 = "C030";
        internal const String EX_ERR = "E080";
        
        //確認メッセージID
        internal const String CHK_MSG_CD_I001 = "I001";

        #endregion 定数
    }
}
