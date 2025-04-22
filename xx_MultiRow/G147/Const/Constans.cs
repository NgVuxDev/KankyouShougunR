// $Id: Constans.cs 33102 2014-10-23 00:41:23Z takeda $

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class ConstCls
    {
		/// <summary>
		/// Button設定用XMLファイルパス
		/// </summary>
        public static readonly string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.MihimodukeIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 排出事業者
        /// </summary>
        public static readonly string HAISHUTSU_JIGYOUSHA = "排出事業者";

        /// <summary>
        /// 収集運搬業者N
        /// </summary>
        public static readonly string SHUSHU_UNPAN_GYOUSHAN = "収集運搬業者N";

        /// <summary>
        /// 運搬先業者N
        /// </summary>
        public static readonly string UNPANSAKI_GYOUSHAN = "運搬先業者N";

        /// <summary>
        /// 処分業者
        /// </summary>
        public static readonly string SHOBUN_GYOUSHA = "処分業者";

        /// <summary>
        /// 排出事業場
        /// </summary>
        public static readonly string HAISHUTSU_JIGYOUJOU = "排出事業場";

        /// <summary>
        /// 運搬先事業場N
        /// </summary>
        public static readonly string UNPANSAKI_JIGYOUJOUN = "運搬先事業場N";

        /// <summary>
        /// 処分事業場
        /// </summary>
        public static readonly string SHOBUN_JIGYOUJOU = "処分事業場";
        
        /// <summary>チェックボックス</summary>
        public static readonly string CELL_NAME_CHECK = "CHECK";

        /// <summary>処分事業者の加入者番号(頭文字)</summary>
        public static readonly string EDI_MEMBER_ID_SHOBUN_3 = "3";
        public static readonly string EDI_MEMBER_ID_SHOBUN_D3 = "D3";
    }
}
