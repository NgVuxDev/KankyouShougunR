using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Entity
{

    /// <summary>
    /// 伝票区分
    /// </summary>
    public enum DenpyouKbn
    {
        /// <summary>
        /// なし(0)
        /// </summary>
        None = 0,
        /// <summary>
        /// 受入入力(1)
        /// </summary>
        Ukeire = 1,
        /// <summary>
        /// 出荷入力(2)
        /// </summary>
        Syukka = 2,
        /// <summary>
        /// 売上／支払入力(3)
        /// </summary>
        UriageSiharai = 3,
    }

    /// <summary>
    /// 取引履歴一覧(グリッド用に集計した後の一行分)
    /// </summary>
    [Serializable()]
    public class T_DISPLAY_ROW
    {
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME_RYAKU { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME_RYAKU { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME_RYAKU { get; set; }
        public List<T_DENPYOU_DATA> DENPYOU_LIST { get; set; }
        public decimal DEAL_AVERAGE;
        public T_DISPLAY_ROW()
        {
            this.DENPYOU_LIST = new List<T_DENPYOU_DATA>();
        }
    }

    /// <summary>
    /// 伝票データ
    /// </summary>
    [Serializable()]
    public class T_DENPYOU_DATA
    {
        public DateTime DENPYOU_DATE { get; set; }
        public DenpyouKbn DENPYOU_KBN { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public bool DAINOU_FLG { get; set; }
    }
}
