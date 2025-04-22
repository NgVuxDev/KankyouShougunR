// $Id: ConstCls.cs 5894 2013-11-05 00:22:07Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>抽出設定</summary>
        public const string Chushutsu = "1 または 2 ";

        /// <summary>新規のみ表示</summary>
        public const string Chushutsu_Shinki = "1";

        /// <summary>登録済みも表示</summary>
        public const string Chushutsu_Zumi = "2";

        //マスタ204
        /// <summary>M_CONTENA_SHURUIのCONTENA_SHURUI_CD</summary>
        public static readonly string CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";
        //マスタ204
        /// <summary>M_CONTENA_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";
        //マスタ204 登録?
        /// <summary>M_CONTENA_SHURUIのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";
        
        /// <summary>抽出設定</summary>
        public const string ChushutsuSettei = "0";

        /// <summary>新規のみ表示Code</summary>
        public const string ChushutsuCD_Shinkinomi = "1";
        /// <summary>新規のみ表示Name</summary>
        public const string ChushutsuName_Shinkinomi = "新規のみ表示";

        /// <summary>登録済み表示Code</summary>
        public const string ChushutsuCD_TourokuZumi = "2";
        /// <summary>登録済み表示Name</summary>
        public const string ChushutsuName_TourokuZumi = "登録済み表示";


        /// <summary>
        /// 定期配車明細のカラム名
        /// </summary>
        public class DetailColName
        {
            public const string TOUROKU_FLG = "TOUROKU_FLG";
            public const string NO = "NO";
            public const string JUNNBANN = "JUNNBANN";
            public const string NIOROSHI_NUMBER_DETAIL = "NIOROSHI_NUMBER_DETAIL";
            public const string GYOUSHA_CD = "GYOUSHA_CD";
            public const string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
            public const string GENBA_CD = "GENBA_CD";
            public const string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
            public const string HINMEI_INFO = "HINMEI_INFO";
            public const string MEISAI_BIKOU = "MEISAI_BIKOU";
            public const string UKETSUKE_NUMBER = "UKETSUKE_NUMBER";
            public const string DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";
            public const string TIME_STAMP_DETAIL = "TIME_STAMP_DETAIL";
            public const string SHOUSAI = "SHOUSAI";

            //追加分
            public const string KYOTEN_CD = "KYOTEN_CD";
            public const string SAGYOU_DATE = "SAGYOU_DATE";
            public const string COURSE_NAME_CD = "COURSE_NAME_CD";
            public const string SHARYOU_CD = "SHARYOU_CD";
            public const string SHASHU_CD = "SHASHU_CD";
            public const string UNTENSHA_CD = "UNTENSHA_CD";
            public const string UNPAN_GYOUSHA_CD = "UNPAN_GYOUSHA_CD";
            public const string HOJOIN_CD = "HOJOIN_CD";
            public const string DAY_CD = "DAY_CD";
            public const string COURSE_BIKOU = "COURSE_BIKOU";
            public const string TIME_STAMP_ENTRY = "TIME_STAMP_ENTRY";
            public const string TEIKI_HAISHA_NUMBER = "TEIKI_HAISHA_NUMBER";
            public const string SYSTEM_ID = "SYSTEM_ID";
            public const string SEQ = "SEQ";
            public const string FURIKAE_HAISHA_KBN = "FURIKAE_HAISHA_KBN";
            

        }

        /// <summary>
        /// 定期配車荷卸のカラム名
        /// </summary>
        public class NioroshiColName
        {
            public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
            public const string NIOROSHI_GYOUSHA_CD = "NIOROSHI_GYOUSHA_CD";
            public const string NIOROSHI_GYOUSHA_NAME_RYAKU = "NIOROSHI_GYOUSHA_NAME_RYAKU";
            public const string NIOROSHI_GENBA_CD = "NIOROSHI_GENBA_CD";
            public const string NIOROSHI_GENBA_NAME_RYAKU = "NIOROSHI_GENBA_NAME_RYAKU";
            public const string TIME_STAMP_NIOROSHI = "TIME_STAMP_NIOROSHI";
        }

        /// <summary>
        /// 定期配車詳細のカラム名
        /// </summary>
        public class ShousaiColName
        {
            public const string HINMEI_CD = "HINMEI_CD";
            public const string HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";
            public const string UNIT_CD = "UNIT_CD";
            public const string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";
            public const string TSUKIGIME_KBN = "TSUKIGIME_KBN";
            public const string MONDAY = "MONDAY";
            public const string TUESDAY = "TUESDAY";
            public const string WEDNESDAY = "WEDNESDAY";
            public const string THURSDAY = "THURSDAY";
            public const string FRIDAY = "FRIDAY";
            public const string SATURDAY = "SATURDAY";
            public const string SUNDAY = "SUNDAY";
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
