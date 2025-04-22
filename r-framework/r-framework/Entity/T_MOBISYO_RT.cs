using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MOBISYO_RT : SuperEntity
    {
        public SqlInt64 SEQ_NO { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string UNTENSHA_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public SqlDateTime HAISHA_SAGYOU_DATE { get; set; }
        public SqlInt64 HAISHA_DENPYOU_NO { get; set; }
        public string HAISHA_COURSE_NAME_CD { get; set; }
        public string HAISHA_COURSE_NAME { get; set; }
        public SqlInt32 HAISHA_KBN { get; set; }
        public SqlDateTime HAISHA_TORIKOMI_DATE { get; set; }
        public SqlInt32 GENBA_NO { get; set; }
        public SqlDateTime GENBA_JISSEKI_CREATEDATE { get; set; }
        public SqlDateTime GENBA_JISSEKI_UPDATEDATE { get; set; }
        public SqlDateTime GENBA_JISSEKI_SHUUSHUUTIME { get; set; }
        public string GENBA_JISSEKI_GYOUSHACD { get; set; }
        public string GENBA_JISSEKI_GENBACD { get; set; }
        public SqlBoolean GENBA_JISSEKI_ADDGENBAFLG { get; set; }
        public SqlBoolean SHIJI_FLG { get; set; }
        public SqlBoolean GENBA_JISSEKI_JYOGAIFLG { get; set; }
        public SqlInt16 GENBA_DETAIL_MANIKBN { get; set; }
        public SqlInt64 GENBA_DETAIL_MANINO { get; set; }
        public string GENBA_STTS { get; set; }
        public SqlBoolean JISSEKI_REGIST_FLG { get; set; }
        public string GENBA_JISSEKI_UPNGYOSHACD { get; set; }
        public SqlInt32 HAISHA_ROW_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        // 20170601 wangjm モバイル将軍#105481 start
        public string GENCHAKU_TIME_NAME { get; set; }
        public string GENCHAKU_TIME { get; set; }
        public SqlInt32 KAISHU_NO { get; set; }
        public string KAISHU_BIKOU { get; set; }
        public string GENBA_TEL { get; set; }
        public string GENBA_TANTOU_NAME { get; set; }
        public string GENBA_TANTOU_TEL { get; set; }
        public string GENBA_EIGYOU_TANTOU { get; set; }
        public string UKETSUKE_BIKOU1 { get; set; }
        public string UKETSUKE_BIKOU2 { get; set; }
        public string UKETSUKE_BIKOU3 { get; set; }
        public string UKETSUKE_SIJIJIKOU1 { get; set; }
        public string UKETSUKE_SIJIJIKOU2 { get; set; }
        public string UKETSUKE_SIJIJIKOU3 { get; set; }
        public SqlInt16 UKETSUKE_KBN { get; set; }
        // 20170601 wangjm モバイル将軍#105481 end
    }
}