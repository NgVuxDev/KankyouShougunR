
using System.Data.SqlTypes;
using System;
namespace Shougun.Core.Allocation.ContenaRirekiIchiranHyou
{
    /// <summary>
    /// 検索結果格納用DTO
    /// </summary>
    public class SearchResultDTO
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
        /// 設置日
        /// </summary>
        public string SECCHI_DATE { get; set; }

        /// <summary>
        /// 設置引揚区分
        /// </summary>
        public Int16 CONTENA_SET_KBN { get; set; }

        /// <summary>
        /// 台数
        /// </summary>
        public Int16 DAISUU_CNT { get; set; }

        /// <summary>
        /// 伝票更新日
        /// </summary>
        public SqlDateTime UPDATE_DATE { get; set; }
    }
}
