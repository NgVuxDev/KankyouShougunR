
using System.Data.SqlTypes;
namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
{
    /// <summary>
    /// 検索結果格納用DTO
    /// </summary>
    public class SearchResultDTO
    {
        /// <summary>コンテナ種類CD</summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>設置引揚区分</summary>
        public SqlInt16 CONTENA_SET_KBN { get; set; }

        /// <summary>台数</summary>
        public SqlInt32 DAISUU_CNT { get; set; }

        /// <summary>コンテナ種類名</summary>
        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }

        /// <summary>コンテナCD</summary>
        public string CONTENA_CD { get; set; }

        /// <summary>コンテナ名</summary>
        public string CONTENA_NAME_RYAKU { get; set; }
    }
}
