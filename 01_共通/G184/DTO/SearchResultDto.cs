using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.ContenaShitei.DTO
{
    /// <summary>
    /// CheckCONRETDaoClsの戻り値用DTO
    /// </summary>
    public class SearchResultDto
    {
        public SqlInt64 SYSTEM_ID { get; set; }

        public SqlInt32 SEQ { get; set; }

        public SqlInt16 DENSHU_KBN_CD { get; set; }

        public string CONTENA_SHURUI_CD { get; set; }

        public string CONTENA_CD { get; set; }

        public string GYOUSHA_CD { get; set; }

        public string GENBA_CD { get; set; }

        public Int16 CONTENA_SET_KBN { get; set; }

        public string SECCHI_DATE { get; set; }

        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }

        public string CONTENA_NAME_RYAKU { get; set; }

        public SqlBoolean CONTENA_SHURUI_DELETE_FLG { get; set; }

        public SqlDateTime CONTENA_SHURUI_TEKIYOU_BEGIN { get; set; }

        public SqlDateTime CONTENA_SHURUI_TEKIYOU_END { get; set; }
    }
}
