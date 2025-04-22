// $Id: SearchResult.cs 31270 2014-09-30 08:28:42Z takeda $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.ContenaIchiran
{
    /// <summary>
    /// スクリプトの実行結果を格納するDTO
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// 伝種区分CD
        /// </summary>
        public short DENSHU_KBN_CD { get; set; }

        /// <summary>
        /// 種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 種類名
        /// </summary>
        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

        /// <summary>
        /// コンテナ名
        /// </summary>
        public string CONTENA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 業者名
        /// </summary>
        public string GYOUSHA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 現場名
        /// </summary>
        public string GENBA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 営業担当者CD
        /// </summary>
        public string EIGYOU_TANTOU_CD { get; set; }

        /// <summary>
        /// 社員名(営業担当者名)
        /// </summary>
        public string SHAIN_NAME_RYAKU { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE { get; set; }

        /// <summary>
        /// 経過日数
        /// </summary>
        public Int32 DAYSCOUNT { get; set; }

        /// <summary>
        /// グラフ
        /// </summary>
        public string GRAPH { get; set; }

        /// <summary>
        /// 設置引揚区分
        /// </summary>
        public Int16 CONTENA_SET_KBN { get; set; }

        /// <summary>
        /// 台数
        /// </summary>
        public Int16 DAISUU_CNT { get; set; }

        /// <summary>
        /// 重複
        /// </summary>
        public string SecchiChouhuku { get; set; }
    }
}
