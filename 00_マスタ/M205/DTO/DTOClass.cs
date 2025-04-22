
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.ContenaHoshu
{
    public class DTOClass : SuperEntity
    {

        public string CONTENA_SHURUI_CD { get; set; }
        public string CONTENA_SHURUI_NAME { get; set; }
        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }
        public string CONTENA_CD { get; set; }
        public string CONTENA_NAME { get; set; }
        public string CONTENA_NAME_RYAKU { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME_RYAKU { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME_RYAKU { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string KYOTEN_NAME_RYAKU { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME_RYAKU { get; set; }
        public SqlDateTime SECCHI_DATE { get; set; }
        public string SEARCH_SECCHI_DATE { get; set; }
        public SqlDateTime HIKIAGE_DATE { get; set; }
        public string SEARCH_HIKIAGE_DATE { get; set; }
        public SqlInt16 JOUKYOU_KBN { get; set; }
        public string CONTENA_JOUKYOU_NAME_RYAKU { get; set; }
        public SqlDateTime KOUNYUU_DATE { get; set; }
        public string SEARCH_KOUNYUU_DATE { get; set; }
        public SqlDateTime LAST_SHUUFUKU_DATE { get; set; }
        public string SEARCH_LAST_SHUUFUKU_DATE { get; set; }
        public string CONTENA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

    }
}
