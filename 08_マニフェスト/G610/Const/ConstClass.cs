using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv.Const
{
    public class ConstClass
    {
        //税計算区分（伝票毎）
        public const string ZEIKEISAN_KBN_1 = "1";
        //税計算区分（請求毎）
        public const string ZEIKEISAN_KBN_2 = "2";
        //税計算区分（明細毎）
        public const string ZEIKEISAN_KBN_3 = "3";
        //税区分（外税）
        public const string ZEI_KBN_1 = "1";
        //税区分（内税）
        public const string ZEI_KBN_2 = "2";
        //税区分（非課税）
        public const string ZEI_KBN_3 = "3";
        //伝種区分１（受入）
        public const string TENSYU_KBN_1 = "1";
        //伝種区分２（出荷）
        public const string TENSYU_KBN_2 = "2";
        //伝種区分３（売上支払）
        public const string TENSYU_KBN_3 = "3";
        //伝票モード１（追加）
        public const string TENPYO_MODEL_1 = "1";
        //伝票モード２（修正）
        public const string TENPYO_MODEL_2 = "2";
        //差引基準２（支払）
        public const string CALC_BASE_KBN_2 = "2";
        //請求取引1（現金）
        public const string TORIHIKI_KBN_1 = "1";
        //請求取引２（掛け）
        public const string TORIHIKI_KBN_2 = "2";
        //精算1（する）
        public const string SEISAN_KBN_1 = "1";
        //精算２（しない）
        public const string SEISAN_KBN_2 = "2";
        //領収書1（あり）
        public const string RYOSYUSYO_KBN_1 = "1";
        //領収書1（なし）
        public const string RYOSYUSYO_KBN_2 = "2";
        //相殺1（する）
        public const string SOUSATU_KBN_1 = "1";
        //相殺２（しない）
        public const string SOUSATU_KBN_2 = "2";
        //システム税計算区分利用形態(1.締毎税・伝票毎税)
        public const string SYS_ZEI_KEISAN_KBN_USE_KBN_1 = "1";
        //システム税計算区分利用形態(2.締毎税・明細毎税)
        public const string SYS_ZEI_KEISAN_KBN_USE_KBN_2 = "2";
        //伝票区分１（売上）
        public const string TENPYO_KBN_1 = "1";
        //伝票区分2（支払）
        public const string TENPYO_KBN_2 = "2";

        // 計量票発行区分：1
        public const string KEIRYOU_PRIRNT_KBN_1 = "1";

        // 計量票発行区分：2
        public const string KEIRYOU_PRIRNT_KBN_2 = "2";

        // No.1751
        //キャッシャ連動１（する）
        public const string KYASYA_RENDOU_1 = "1";
        //キャッシャ連動2（しない）
        public const string KYASYA_RENDOU_2 = "2";

        //金額0
        public const string KIGAKU_0 = "0";
        //マイナス
        public const string MINUS_S = "-";
        //Empty 
        public const string Empty_S = "";
    }
}
