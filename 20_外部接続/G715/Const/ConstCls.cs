using System.Data.SqlTypes;
using System.Drawing;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>委託契約書書式名（産廃）</summary>
        public static readonly string SANPAI_SHOSHIKI_NAME = "全産連様式契約書";

        /// <summary>委託契約書書式名（建廃）</summary>
        public static readonly string KENPAI_SHOSHIKI_NAME = "建廃個別様式契約書";

        /// <summary>委託契約書種類名１</summary>
        public static readonly string KEIYAKUSHO_SHURUI_NAME_1 = "収集運搬契約";

        /// <summary>委託契約書種類名１</summary>
        public static readonly string KEIYAKUSHO_SHURUI_NAME_2 = "処分契約";

        /// <summary>委託契約書種類名１</summary>
        public static readonly string KEIYAKUSHO_SHURUI_NAME_3 = "収集運搬/処分契約";

        /// <summary>ファイル種類:11.産廃委託契約書（収運）</summary>
        public static readonly string FILE_SHURUI_NAME_11 = "産廃委託契約書（収運）";

        /// <summary>ファイル種類:12.産廃委託契約書（処分）</summary>
        public static readonly string FILE_SHURUI_NAME_12 = "産廃委託契約書（処分）";

        /// <summary>ファイル種類:13.産廃委託契約書（収運/処分）</summary>
        public static readonly string FILE_SHURUI_NAME_13 = "産廃委託契約書（収運/処分）";

        /// <summary>ファイル種類:21.建廃委託契約書（収運）</summary>
        public static readonly string FILE_SHURUI_NAME_21 = "建廃委託契約書（収運）";

        /// <summary>ファイル種類:22.建廃委託契約書（処分）</summary>
        public static readonly string FILE_SHURUI_NAME_22 = "建廃委託契約書（処分）";

        /// <summary>ファイル種類:23.建廃委託契約書（収運/処分）</summary>
        public static readonly string FILE_SHURUI_NAME_23 = "建廃委託契約書（収運/処分）";

        /// <summary>ファイル種類:31.運搬許可証</summary>
        public static readonly string FILE_SHURUI_NAME_31 = "運搬許可証";

        /// <summary>ファイル種類:32.処分許可証</summary>
        public static readonly string FILE_SHURUI_NAME_32 = "処分許可証";

        /// <summary>ファイル種類:33.最終処分許可証</summary>
        public static readonly string FILE_SHURUI_NAME_33 = "最終処分許可証";

        /// <summary>ファイル種類:99.覚書</summary>
        public static readonly string FILE_SHURUI_NAME_99 = "覚書";

        /// <summary>エラー背景色（赤）</summary>
        public static readonly Color ERROR_COLOR = Color.FromArgb(255, 100, 100);
    }
}
