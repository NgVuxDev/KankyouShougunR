using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Entity
{
    /// <summary>
    /// 返送先情報
    /// </summary>
    public class MANIFESUTO_HENSOUSAKI
    {
        /// <summary>返送先区分</summary>
        public string MANI_HENSOUSAKI_KBN { get; set; }
        /// <summary>返送先使用区分</summary>
        public string MANI_HENSOUSAKI_USE { get; set; }
        /// <summary>返送先取引先CD</summary>
        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD { get; set; }
        /// <summary>返送先取引先名</summary>
        public string MANI_HENSOUSAKI_TORIHIKISAKI_NAME { get; set; }
        /// <summary>返送先業者CD</summary>
        public string MANI_HENSOUSAKI_GYOUSHA_CD { get; set; }
        /// <summary>返送先業者名</summary>
        public string MANI_HENSOUSAKI_GYOUSHA_NAME { get; set; }
        /// <summary>返送先現場CD</summary>
        public string MANI_HENSOUSAKI_GENBA_CD { get; set; }
        /// <summary>返送先現場名</summary>
        public string MANI_HENSOUSAKI_GENBA_NAME { get; set; }
        /// <summary>返送先取引先引合フラグ</summary>
        public string MANI_HENSOUSAKI_HIKIAI_TORIHIKISAKI_USE_FLG { get; set; }
        /// <summary>返送先業者引合フラグ</summary>
        public string MANI_HENSOUSAKI_HIKIAI_GYOUSHA_USE_FLG { get; set; }
        /// <summary>返送先現場引合フラグ</summary>
        public string MANI_HENSOUSAKI_GENBA_HIKIAI_FLG { get; set; }
        /// <summary>返送先名称1</summary>
        public string MANI_HENSOUSAKI_NAME1 { get; set; }
        /// <summary>返送先名称2</summary>
        public string MANI_HENSOUSAKI_NAME2 { get; set; }
        /// <summary>返送先敬称1</summary>
        public string MANI_HENSOUSAKI_KEISHOU1 { get; set; }
        /// <summary>返送先敬称2</summary>
        public string MANI_HENSOUSAKI_KEISHOU2 { get; set; }
        /// <summary>返送先郵便番号</summary>
        public string MANI_HENSOUSAKI_POST { get; set; }
        /// <summary>返送先住所1</summary>
        public string MANI_HENSOUSAKIDDRESS1 { get; set; }
        /// <summary>返送先住所2</summary>
        public string MANI_HENSOUSAKIDDRESS2 { get; set; }
        /// <summary>返送先部署</summary>
        public string MANI_HENSOUSAKI_BUSHO { get; set; }
        /// <summary>返送先担当者</summary>
        public string MANI_HENSOUSAKI_TANTOU { get; set; }
        /// <summary>返送先場所区分</summary>
        public string MANI_HENSOUSAKI_PLACE_KBN { get; set; }
    }
}
