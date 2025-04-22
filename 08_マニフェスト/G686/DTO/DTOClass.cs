using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.ManifestIkkatsuKousin
{
    internal class DTOClass
    {
        //システムID(全般･マニ返却日)
        public SqlInt64 SYSTEM_ID { get; set; }
        //枝番(全般)
        public SqlInt32 SEQ { get; set; }
        //枝番(マニ返却日)
        public SqlInt32 SeqRD { get; set; }
        //廃棄物区分
        public String HAIKI_KBN_CD { get; set; }
        //一次二次区分
        public int maniFlag { get; set; }
        //タイムスタンプ
        public byte[] TIME_STAMP_ENTRY { get; set; }
        //マニ返却日タイムスタンプ
        public byte[] TIME_STAMP_RET_DATE { get; set; }
        //運搬終了日(区間1)
        public SqlDateTime UPN_END_DATE1 { get; set; }
        //運搬終了日(区間2)
        public SqlDateTime UPN_END_DATE2 { get; set; }
        //運搬終了日(区間3)
        public SqlDateTime UPN_END_DATE3 { get; set; }
        //区間1の運搬受託者 建廃の場合、処分の受託（受領）更新用
        public String UPN_JYUTAKUSHA_CD1 { get; set; }
        //区間2の運搬受託者 建廃の場合、処分の受託（受領）更新用
        public String UPN_JYUTAKUSHA_CD2 { get; set; }

        public List<DETAIL> detailList = new List<DETAIL>();
    }

    internal class DETAIL
    {
        //明細システムID
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        //処分終了日
        public SqlDateTime SBN_END_DATE { get; set; }
        //最終処分終了日
        public SqlDateTime LAST_SBN_END_DATE { get; set; }
    }
}
