// $Id: ManifestKansanHoshuConstans.cs 16092 2014-02-17 15:13:00Z ishibashi $
namespace ManifestKansanHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ManifestKansanHoshuConstans
    {
        /// <summary>HOUKOKUSHO_BUNRUI_CD</summary>
        public static readonly string HOUKOKUSHO_BUNRUI_CD = "HOUKOKUSHO_BUNRUI_CD";

        /// <summary>HAIKI_NAME_CD</summary>
        public static readonly string HAIKI_NAME_CD = "HAIKI_NAME_CD";

        /// <summary>UNIT_CD</summary>
        public static readonly string UNIT_CD = "UNIT_CD";

        /// <summary>UNIT_NAME</summary>
        public static readonly string UNIT_NAME = "UNIT_NAME";

        /// <summary>NISUGATA_CD</summary>
        public static readonly string NISUGATA_CD = "NISUGATA_CD";

        /// <summary>TIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>DELETE_FLG</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>KANSANSHIKI表示変換（0:×,1:÷）</summary>
        public static readonly string KANSANSHIKI_0 = "0";
        public static readonly string KANSANSHIKI_0_shown = "×";
        public static readonly string KANSANSHIKI_1 = "1";
        public static readonly string KANSANSHIKI_1_shown = "÷";

        /// <summary>KANSANSHIKI</summary>
        public static readonly string KANSANSHIKI = "KANSANSHIKI";

        /// <summary>KANSANSHIKI_SHOW</summary>
        public static readonly string KANSANSHIKI_SHOW = "KANSANSHIKI_SHOW";

        /// <summary>KANSANCHI</summary>
        public static readonly string KANSANCHI = "KANSANCHI";

        /// <summary>MANIFEST_KANSAN_BIKOU</summary>
        public static readonly string MANIFEST_KANSAN_BIKOU = "MANIFEST_KANSAN_BIKOU";

        /// <summary>ヘッダ部の基本単位</summary>
        public static string MANI_KANSAN_UNIT_CD = null;

        /// <summary>UK_HOUKOKUSHO_BUNRUI_CD</summary>
        public static readonly string UK_HOUKOKUSHO_BUNRUI_CD = "UK_HOUKOKUSHO_BUNRUI_CD";

        /// <summary>UK_HAIKI_NAME_CD</summary>
        public static readonly string UK_HAIKI_NAME_CD = "UK_HAIKI_NAME_CD";

        /// <summary>UK_UNIT_CD</summary>
        public static readonly string UK_UNIT_CD = "UK_UNIT_CD";

        /// <summary>UK_NISUGATA_CD</summary>
        public static readonly string UK_NISUGATA_CD = "UK_NISUGATA_CD";

        public enum FocusSwitch
        {
            NONE,
            IN,
            OUT
        }
    }
}