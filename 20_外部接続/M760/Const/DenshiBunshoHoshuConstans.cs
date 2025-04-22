using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Const
{
    public class DenshiBunshoHoshuConstans
    {
        /// <summary>区分の使用</summary>
        public static readonly string IS_USING = "1";

        /// <summary>区分の使用しない</summary>
        public static readonly string IS_NOT_USING = "0";

        /// <summary>区分の使用（フィールド）</summary>
        public static readonly SqlInt16 IS_USING_FIELD = 1;

        /// <summary>区分の使用（フィールド）</summary>
        public static readonly SqlInt16 IS_NOT_USING_FIELD = 2;

        public static readonly string INPUT_TYPE_CD_1 = "1";
        public static readonly string INPUT_TYPE_NAME_1 = "文字";
        public static readonly string INPUT_TYPE_CD_2 = "2";
        public static readonly string INPUT_TYPE_NAME_2 = "数字";
        public static readonly string INPUT_TYPE_CD_3 = "3";
        public static readonly string INPUT_TYPE_NAME_3 = "日付";

        public const string RENEWWAL_PERIOD_UNIT_CD_1 = "01";
        public const string RENEWWAL_PERIOD_UNIT_TEXT_1 = "日";
        public const string RENEWWAL_PERIOD_UNIT_CD_2 = "02";
        public const string RENEWWAL_PERIOD_UNIT_TEXT_2 = "ヶ月";
        public const string RENEWWAL_PERIOD_UNIT_CD_3 = "03";
        public const string RENEWWAL_PERIOD_UNIT_TEXT_3 = "年";

        public const string CANCEL_PERIOD_UNIT_CD_1 = "01";
        public const string CANCEL_PERIOD_UNIT_TEXT_1 = "日前";
        public const string CANCEL_PERIOD_UNIT_CD_2 = "02";
        public const string CANCEL_PERIOD_UNIT_TEXT_2 = "ヶ月前";
        public const string CANCEL_PERIOD_UNIT_CD_3 = "03";
        public const string CANCEL_PERIOD_UNIT_TEXT_3 = "年前";

        public const string REMINDER_PERIOD_UNIT_CD_1 = "01";
        public const string REMINDER_PERIOD_UNIT_TEXT_1 = "日前";
        public const string REMINDER_PERIOD_UNIT_CD_2 = "02";
        public const string REMINDER_PERIOD_UNIT_TEXT_2 = "ヶ月前";
        public const string REMINDER_PERIOD_UNIT_CD_3 = "03";
        public const string REMINDER_PERIOD_UNIT_TEXT_3 = "年前";

        public static readonly string HEADER_RENEWWAL_PERIOD_UNIT_CD = "更新期間単位CD";
        public static readonly string HEADER_RENEWWAL_PERIOD_UNIT_NAME = "更新期間単位名";
        public static readonly string POPUP_TITLE_RENEWWAL_PERIOD_UNIT = "更新期間単位検索";
        public static readonly string COLUMN_RENEWWAL_PERIOD_UNIT_CD = "RENEWWAL_PERIOD_UNIT_CD";
        public static readonly string COLUMN_RENEWWAL_PERIOD_UNIT_NAME = "RENEWWAL_PERIOD_UNIT_NAME";

        public static readonly string HEADER_CANCEL_PERIOD_UNIT_CD = "解約通知単位CD";
        public static readonly string HEADER_CANCEL_PERIOD_UNIT_NAME = "解約通知単位名";
        public static readonly string POPUP_TITLE_CANCEL_PERIOD_UNIT = "解約通知単位検索";
        public static readonly string COLUMN_CANCEL_PERIOD_UNIT_CD = "CANCEL_PERIOD_UNIT_CD";
        public static readonly string COLUMN_CANCEL_PERIOD_UNIT_NAME = "CANCEL_PERIOD_UNIT_NAME";

        public static readonly string HEADER_REMINDER_PERIOD_UNIT_CD = "ﾘﾏｲﾝﾄﾞ通知日単位CD";
        public static readonly string HEADER_REMINDER_PERIOD_UNIT_NAME = "ﾘﾏｲﾝﾄﾞ通知日単位名";
        public static readonly string POPUP_TITLE_REMINDER_PERIOD_UNIT = "ﾘﾏｲﾝﾄﾞ通知日単位検索";
        public static readonly string COLUMN_REMINDER_PERIOD_UNIT_CD = "REMINDER_PERIOD_UNIT_CD";
        public static readonly string COLUMN_REMINDER_PERIOD_UNIT_NAME = "REMINDER_PERIOD_UNIT_NAME";

        public static readonly long MIN_NUMBERIC = -2147483648;
        public static readonly long MAX_NUMBERIC = 2147483648;
    }
}
