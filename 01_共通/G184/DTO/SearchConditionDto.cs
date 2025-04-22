
using System.Data.SqlTypes;
namespace Shougun.Core.Common.ContenaShitei.DTO
{
    /// <summary>
    /// CheckCONRETDaoClsの検索条件用DTO
    /// </summary>
    public class SearchConditionDto
    {
        public string CONTENA_SHURUI_CD { get; set; }

        public string CONTENA_CD { get; set; }

        public string GYOUSHA_CD { get; set; }

        public string GENBA_CD { get; set; }

        public string DENPYOU_DATE { get; set; }

        public SqlBoolean ISNOT_NEED_DELETE_FLG { get; set; }
    }
}
