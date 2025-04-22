using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.Dto
{
    public class MeisaiSearchJoukenDto : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public SqlDateTime TANK_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TANK_TEKIYOU_END { get; set; }
        public SqlDateTime TANK_TEKIYOU_BEGIN_START { get; set; }
        public SqlDateTime TANK_TEKIYOU_BEGIN_END { get; set; }
        public SqlDateTime TANK_TEKIYOU_END_START { get; set; }
        public SqlDateTime TANK_TEKIYOU_END_END { get; set; }
        public SqlDecimal TANK_FROM { get; set; }
        public SqlDecimal TANK_TO { get; set; }
        public string SHURUI_CD { get; set; }
        public string BUNRUI_CD { get; set; }
    }   
}
