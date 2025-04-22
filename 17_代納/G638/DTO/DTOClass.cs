using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PayByProxy.DainoMeisaihyoOutput
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SearchParameterDto
    {
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlDateTime DATE_FROM { get; set; }
        public SqlDateTime DATE_TO { get; set; }
        public string UKEIRE_TORI_CD_FROM { get; set; }
        public string UKEIRE_TORI_CD_TO { get; set; }
        public string UKEIRE_GYOUSHA_CD_FROM { get; set; }
        public string UKEIRE_GYOUSHA_CD_TO { get; set; }
        public string UKEIRE_GENBA_CD_FROM { get; set; }
        public string UKEIRE_GENBA_CD_TO { get; set; }
        public string SHUKKA_TORI_CD_FROM { get; set; }
        public string SHUKKA_TORI_CD_TO { get; set; }
        public string SHUKKA_GYOUSHA_CD_FROM { get; set; }
        public string SHUKKA_GYOUSHA_CD_TO { get; set; }
        public string SHUKKA_GENBA_CD_FROM { get; set; }
        public string SHUKKA_GENBA_CD_TO { get; set; }
    }
}
