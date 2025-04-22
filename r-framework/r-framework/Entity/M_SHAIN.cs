using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_SHAIN : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string SHAIN_NAME { get; set; }
        public string SHAIN_NAME_RYAKU { get; set; }
        public string SHAIN_FURIGANA { get; set; }
        public string BUSHO_CD { get; set; }
        public SqlBoolean EIGYOU_TANTOU_KBN { get; set; }
        public SqlBoolean UNTEN_KBN { get; set; }
        public SqlBoolean SHOBUN_TANTOU_KBN { get; set; }
        public SqlBoolean TEGATA_HOKAN_KBN { get; set; }
        public SqlBoolean NYUURYOKU_TANTOU_KBN { get; set; }
        public SqlBoolean INXS_TANTOU_FLG { get; set; }
        public SqlBoolean MOBILE_USER_KBN { get; set; }
        public string LOGIN_ID { get; set; }
        public string PASSWORD { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string SHAIN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //20250310
        public string WARIATE_JUN { get; set; }

        //20250311
        public SqlBoolean NIN_I_TORIHIKISAKI_FUKA { get; set; }
    }
}