using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.SalesPayment.DenpyouHakou.Const
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

        //【1、2】のいずれかで入力してください
        public const string TOOL_TIP_TXT_1 = "【1、2】のいずれかで入力してください";
        //【2、3】のいずれかで入力してください
        public const string TOOL_TIP_TXT_2 = "【2、3】のいずれかで入力してください";
        //【1～3】のいずれかで入力してください
        public const string TOOL_TIP_TXT_3 = "【1～3】のいずれかで入力してください";
        //税区分を外税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_4 = "税区分を外税にする場合にはチェックを付けてください";
        //税区分を内税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_5 = "税区分を内税にする場合にはチェックを付けてください";
        //税区分を非課税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_6 = "税区分を非課税にする場合にはチェックを付けてください";
        //税区取引分を現金にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_7 = "取引区分を現金にする場合にはチェックを付けてください";
        //税区取引分を掛けにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_8 = "取引区分を掛けにする場合にはチェックを付けてください";
        //精算をするにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_9 = "精算をするにする場合にはチェックを付けてください";
        //精算をしないにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_10 = "精算をしないにする場合にはチェックを付けてください";
        //伝票発行を有りにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_11 = "伝票発行を有りにする場合にはチェックを付けてください";
        //伝票発行を無しにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_12 = "伝票発行を無しにする場合にはチェックを付けてください";
        //税区分を外税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_13 = "税区分を外税にする場合にはチェックを付けてください";
        //税区分を内税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_14 = "税区分を内税にする場合にはチェックを付けてください";
        //税区分を非課税にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_15 = "税区分を非課税にする場合にはチェックを付けてください";
        //税区取引分を現金にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_16 = "税区取引分を現金にする場合にはチェックを付けてください";
        //税区取引分を掛けにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_17 = "税区取引分を掛けにする場合にはチェックを付けてください";
        //精算をするにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_18 = "精算をするにする場合にはチェックを付けてください";
        //精算をしないにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_19 = "精算をしないにする場合にはチェックを付けてください";
        //伝票発行を有りにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_20 = "伝票発行を有りにする場合にはチェックを付けてください";
        //伝票発行を無しにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_21 = "伝票発行を無しにする場合にはチェックを付けてください";
        //相殺をするにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_22 = "相殺をするにする場合にはチェックを付けてください";
        //相殺をしないにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_23 = "相殺をしないにする場合にはチェックを付けてください";
        //発行区分を個別にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_24 = "発行区分を個別にする場合にはチェックを付けてください";
        //発行区分を相殺にする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_25 = "発行区分を相殺にする場合にはチェックを付けてください";
        //発行区分を全てにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_26 = "発行区分を全てにする場合にはチェックを付けてください";
        //領収証を有りにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_27 = "領収書を有りにする場合にはチェックを付けてください";
        //領収証を無しにする場合にはチェックを付けてください
        public const string TOOL_TIP_TXT_28 = "領収書を無しにする場合にはチェックを付けてください";
        //敬称1を全角5文字以内で入力してください
        public const string TOOL_TIP_TXT_29 = "敬称1を全角5文字以内で入力してください";
        //敬称2を全角5文字以内で入力してください
        public const string TOOL_TIP_TXT_30 = "敬称2を全角5文字以内で入力してください";
        //金額0
        public const string KIGAKU_0 = "0";
        //マイナス
        public const string MINUS_S = "-";
        //Empty 
        public const string Empty_S = "";
    }
}
