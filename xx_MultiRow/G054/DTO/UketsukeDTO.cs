using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    /// <summary>
    /// 受付番号用DTO
    /// </summary>
    internal class UketsukeDTO
    {
        internal T_UKETSUKE_SS_ENTRY entrySSEntity;

        internal T_UKETSUKE_SS_DETAIL[] detailSSEntity;

        internal T_UKETSUKE_SK_ENTRY entrySKEntity;

        internal T_UKETSUKE_SK_DETAIL[] detailSKEntity;

        /// <summary>
        /// DETAIL数カウント
        /// </summary>
        internal int detailCount { get; set; }

        //入力
        internal string strKyotenName { get; set; }
        internal string strTorihikisakiName { get; set; }
        internal string strGyoshaName { get; set; }
        internal string strGenbaName { get; set; }
        internal string strUnpanGyoshaName { get; set; }
        internal string strEigyouTantoushaName { get; set; }
        internal string strSharyouName { get; set; }
        internal string strShashuName { get; set; }
        internal string strUntenashaName { get; set; }
        internal string strManiShuruiName { get; set; }
        internal string strManiTehaiName { get; set; }
        internal string strContenaName { get; set; }
        internal string strNizumiGyoshaName { get; set; }
        internal string strNizumiGenbaName { get; set; }

        //明細
        internal Dictionary<int, string> strHinmeiName = new Dictionary<int, string>();
        internal Dictionary<int, string> strDenpyoKbnName = new Dictionary<int, string>();
        internal Dictionary<int, string> strTaniName = new Dictionary<int, string>();
    }
}
