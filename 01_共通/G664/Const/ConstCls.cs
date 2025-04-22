// $Id: ConstCls.cs 25413 2014-07-11 10:45:11Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.NioroshiNoSettei
{
    /// <summary>
    /// 荷降のカラム名
    /// </summary>
    public class NioroshiColName
    {
        public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
        public const string NIOROSHI_GYOUSHA_CD = "NIOROSHI_GYOUSHA_CD";
        public const string NIOROSHI_GYOUSHA_NAME = "NIOROSHI_GYOUSHA_NAME";
        public const string NIOROSHI_GENBA_CD = "NIOROSHI_GENBA_CD";
        public const string NIOROSHI_GENBA_NAME = "NIOROSHI_GENBA_NAME";
    }

    /// <summary>
    /// 明細のカラム名
    /// </summary>
    public class DetailColName
    {
        public const string ROW_NO = "ROW_NO";
        public const string TABLE_ROW_NO = "TABLE_ROW_NO";
        public const string ROUND_NO = "ROUND_NO";
        public const string GYOUSHA_CD = "GYOUSHA_CD";
        public const string GYOUSHA_NAME = "GYOUSHA_NAME";
        public const string GENBA_CD = "GENBA_CD";
        public const string GENBA_NAME = "GENBA_NAME";
        public const string HINMEI_CD = "HINMEI_CD";
        public const string HINMEI_NAME = "HINMEI_NAME";
        public const string UNIT_CD = "UNIT_CD";
        public const string UNIT_NAME = "UNIT_NAME";
        public const string NIOROSHI_NUMBER_DETAIL = "NIOROSHI_NUMBER_DETAIL";
        public const string DELETE_FLG = "DELETE_FLG";
        public const string KEIYAKU_KBN = "KEIYAKU_KBN";
        public const string KEIJYOU_KBN = "KEIJYOU_KBN";
        public const string DENPYOU_KBN_CD = "DENPYOU_KBN_CD";
        public const string KANSAN_UNIT_MOBILE_OUTPUT_FLG = "KANSAN_UNIT_MOBILE_OUTPUT_FLG";
        public const string KANSANCHI = "KANSANCHI";
        public const string KANSAN_UNIT_CD = "KANSAN_UNIT_CD";
        public const string INPUT_KBN = "INPUT_KBN";
        public const string ANBUN_FLG = "ANBUN_FLG";
        public const string TABLE_NAME = "TABLE_NAME";
    }
}
