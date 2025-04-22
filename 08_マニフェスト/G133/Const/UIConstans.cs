using System.Data.SqlTypes;
namespace Shougun.Core.PaperManifest.HaikibutuTyoubo.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.HaikibutuTyoubo.Setting.ButtonSetting.xml";

        /// <summary>
        /// 廃棄区分
        /// </summary>
        internal static readonly string HaikibutsuKbn = "廃棄物区分";

        /// <summary>
        /// 廃棄区分:産廃(直行)
        /// </summary>
        internal static readonly SqlInt16 Chokkou = 1;
        
        /// <summary>
        /// 廃棄区分:産廃(積替)
        /// </summary>
        internal static readonly SqlInt16 Tsumikae = 3;

        /// <summary>
        /// 廃棄区分:建廃
        /// </summary>
        internal static readonly SqlInt16 Kenpai = 2;

        /// <summary>
        /// 拠点:全社コード
        /// </summary>
        internal static readonly string Zensha = "99";

        /// <summary>
        /// 運搬受託者
        /// </summary>
        internal static readonly string UnpanJutakusha = "運搬受託者";

        /// <summary>
        /// 処分受託者
        /// </summary>
        internal static readonly string ShobunJutakusha = "処分受託者";

        /// <summary>
        /// 処分事業場
        /// </summary>
        internal static readonly string ShobunJigyojo = "処分事業場";

        /// <summary>
        /// 排出事業者
        /// </summary>
        internal static readonly string HaishutsuJutakusha = "排出事業者";

        /// <summary>
        /// 排出事業場
        /// </summary>
        internal static readonly string HaishutsuJigyojo = "排出事業場";

        /// <summary>
        /// 最大印刷データ数
        /// </summary>
        internal static readonly int PRINTMAX = 12;
    }
}
