using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.DTO
{
    /// <summary>
    /// 拡張テーブル用DTOクラス
    /// </summary>
    public class SearchDTOForDTExClass
    {
        /// <summary>検索条件  :電子廃棄物名称CD</summary>
        public string HAIKI_NAME_CD { get; set; }
        /// <summary>検索条件  :廃棄種類CD</summary>
        public string HAIKI_SHURUI_CD { get; set; }
        /// <summary>検索条件  :処分方法CD</summary>
        public string SHOBUN_HOUHOU_CD { get; set; }
    }
}
