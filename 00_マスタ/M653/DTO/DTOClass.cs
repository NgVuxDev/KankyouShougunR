// $Id: DTOClass.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using r_framework.Entity;

namespace Shougun.Core.Master.DenManiKansanHoshu
{
    /// <summary>
    /// 電マニ換算値入力検索条件DTO
    /// </summary>
    internal class findConditionDTO
    {
        #region - Field -
        /// <summary>加入者番号</summary>
        public M_DENSHI_MANIFEST_KANSAN entity { get; set; }
        /// <summary>電子廃棄物細分類名</summary>
        public string HAIKI_SHURUI_NAME { get; set; }
        /// <summary>単位名</summary>
        public string UNIT_NAME_RYAKU { get; set; }
        /// <summary>削除フラグ</summary>
        public bool SHOW_CONDITION_DELETED { get; set; }

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal findConditionDTO()
        {
            // フィールドの初期化
            this.entity = new M_DENSHI_MANIFEST_KANSAN();
        }

        #endregion - Constructor -
    }
}
