using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Drawing;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    class DenpyouInfo
    {
        public string FieldName;
        public Object DefaultValue;
        public Type type;   //ThangNguyen [Add] 20150721
    }

    class ConstClass
    {
        /// <summary>ボタン設定ファイル</summary>
        public static readonly string BUTTON_SETTING_XML = "Shougun.Core.Allocation.SagyoubiHenkou.Setting.ButtonSetting.xml";

        public const int DENPYOU_COUNT = 24;
        public static readonly Object[] EmptySharyou = new Object[]
        {
            "",
            "",
            "",
            "",
            DENPYOU_COUNT,
            "",
            "",
            "",
            "",
            "",
            false ,
            false ,
        };

        //ThangNguyen [Add] 20150721 End
        public static readonly DenpyouInfo[] DENPYOU_HEADER_INFO = 
        {
            new DenpyouInfo{ FieldName = "SHARYOU_CD",              DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SHARYOU_NAME_RYAKU",      DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SHASYU_CD",               DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SHASHU_NAME_RYAKU",       DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SHAIN_CD",                DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SHAIN_NAME_RYAKU",        DefaultValue =  ""                 ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "GYOUSHA_CD",              DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "GYOUSHA_NAME_RYAKU",      DefaultValue = ""                  ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SAIDAI_WAKU_SUU",         DefaultValue = DENPYOU_COUNT       ,type = typeof(int) },
            new DenpyouInfo{ FieldName = "WORK_CLOSED_SHARYOU",     DefaultValue = false               ,type = typeof(bool)},
            new DenpyouInfo{ FieldName = "WORK_CLOSED_UNTENSHA",    DefaultValue = false               ,type = typeof(bool)},
        };
        public static readonly DenpyouInfo[] CELL_INFO = 
        {
            new DenpyouInfo{ FieldName = "cellHaishaShurui",           },
            new DenpyouInfo{ FieldName = "cellSagyouDateKubun",              },
            new DenpyouInfo{ FieldName = "cellGenchakuJikan",            },
            new DenpyouInfo{ FieldName = "cellHaishaSijishoStatus",                     },
            new DenpyouInfo{ FieldName = "cellHaishaSijishoCheckBox",            },
            new DenpyouInfo{ FieldName = "cellEmpty",           },
            new DenpyouInfo{ FieldName = "cellDenpyouContent",        },
        };
        public static readonly string[] BlockHaishaCellName = new string[] {
                                                            "cellHaishaShurui",
                                                            "cellSagyouDateKubun",
                                                            "cellGenchakuJikan",
                                                            "cellHaishaSijishoStatus",
                                                            "cellHaishaSijishoCheckBox",
                                                            "cellEmpty",
                                                            "cellDenpyouContent" };

        public static readonly string[] BlockMihaishaCellName = new string[] {"cellHaishaShurui",
		                                                                    "cellSagyouDateKubun",
		                                                                    "cellGenchakuJikan",
		                                                                    "cellDenpyouContent" };
        public static readonly Color blockDetailColor = Color.Yellow;

        //public static readonly string FIELD_SELECTED_FLG = "SELECTED_FLG";
        public static readonly string NO_HAISHA_FLG = "1";
        //public static readonly System.Drawing.Color COLOR_READONLY = System.Drawing.Color.FromArgb(240, 250, 230);
        //ThangNguyen [Add] 20150721 End

        public const short SHUBETSU_KBN_EMPTY = 0;
        public const short SHUBETSU_KBN_UKETSUKE_SS = 1;
        public const short SHUBETSU_KBN_UKETSUKE_SK = 2;
        public const short SHUBETSU_KBN_TEIKI_HAISHA = 3;

        public const string HAISHA_SHURUI_KAKU = "確";
        public const string HAISHA_SHURUI_KARI = "仮";

        public const string MAIL_SEND_FALSE = "未送信";
        public const string MAIL_SEND_TRUE = "送信済";
        public const string HAISHA_SIJISHO_FALSE = "未印刷";
        public const string HAISHA_SIJISHO_TRUE = "印刷済";

        public static readonly DenpyouInfo[] DENPYOU_INFO = 
        {
            new DenpyouInfo{ FieldName = "SHUBETSU_KBN_",           DefaultValue = SHUBETSU_KBN_EMPTY   ,type = typeof(short) },
            new DenpyouInfo{ FieldName = "SYSTEM_ID_",              DefaultValue = (Int64)0             ,type = typeof(Int64) },
            new DenpyouInfo{ FieldName = "DENPYOU_NUM_",            DefaultValue = (Int64)0             ,type = typeof(Int64) },
            new DenpyouInfo{ FieldName = "SEQ",                     DefaultValue = 0                    ,type = typeof(int) },
            new DenpyouInfo{ FieldName = "HAISHA_JOKYO",            DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "HAISHA_SHURUI",           DefaultValue = ""                   ,type = typeof(string) },
            new DenpyouInfo{ FieldName = "SAGYOUDATE_KUBUN",        DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "GENCHAKU_JIKAN",          DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "GENCHAKU_BACK_COLOR",     DefaultValue = DBNull.Value         ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "HAISHA_SIJISHO_STATUS",   DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "HAISHA_SIJISHO_CHECKED",  DefaultValue = false                ,type = typeof(bool)},
            new DenpyouInfo{ FieldName = "MAIL_SEND_STATUS",        DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "MAIL_SEND_CHECKED",       DefaultValue = false                ,type = typeof(bool)},
            new DenpyouInfo{ FieldName = "DENPYOU_CONTENT",         DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "ROW_NUM",                 DefaultValue = 0                    ,type = typeof(long)},
            new DenpyouInfo{ FieldName = "KARADENPYOU_FLG_",        DefaultValue = false                ,type = typeof(bool)},
            new DenpyouInfo{ FieldName = "HAISHA_FLG",              DefaultValue = 1                    ,type = typeof(int)},
            new DenpyouInfo{ FieldName = "SORT_KEY1_",              DefaultValue = 0                    ,type = typeof(int)},
            new DenpyouInfo{ FieldName = "SORT_KEY2_",              DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "SORT_KEY3_",              DefaultValue = 0                    ,type = typeof(int)},
            new DenpyouInfo{ FieldName = "SORT_KEY4_",              DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "SORT_KEY5_",              DefaultValue = ""                   ,type = typeof(string)},
            new DenpyouInfo{ FieldName = "SORT_KEY6_",              DefaultValue = 0                    ,type = typeof(Int64)},
        };
        /// <summary>配車状況</summary>
        public const short HAISHA_JOKYO_CD_KEIJO = 3;
        public const short HAISHA_JOKYO_CD_NASHI = 5;
    }
}
