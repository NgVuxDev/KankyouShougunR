using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku
{
    internal class DTOUketukeKeiryoClass
    {
        /// <summary>
        /// T_UKETSUKE_MK_ENTRY用のEntity
        /// </summary>
        internal T_UKETSUKE_MK_ENTRY entryEntity;

        /// <summary>
        /// T_UKETSUKE_MK_ENTRY用のEntity
        /// </summary>
        internal T_UKETSUKE_MK_DETAIL[] detailEntity;

        /// <summary>
        /// T_KEIRYOU_ENTRY用のEntity
        /// </summary>
        internal T_KEIRYOU_ENTRY keiryouEntryEntity;

        /// <summary>
        /// T_KEIRYOU_DETAIL用のEntity
        /// </summary>
        internal T_KEIRYOU_DETAIL[] keiryouDetailEntity;

        /// <summary>
        /// T_UKEIRE_JISSEKI_ENTRY用のEntity
        /// </summary>
        internal T_UKEIRE_JISSEKI_ENTRY ukeireJissekiEntryEntity;

        /// <summary>
        /// T_UKEIRE_JISSEKI_DETAIL用のEntity
        /// </summary>
        internal T_UKEIRE_JISSEKI_DETAIL[] ukeireJissekiDetailEntity;

        /// <summary>
        /// DETAIL数カウント
        /// </summary>
        internal int detailCount { get; set; }

        //入力
        internal string strKyotenName { get; set; }
        internal string strTorihikisakiName { get; set; }
        internal string strGyoshaName { get; set; }
        internal string strGenbaName { get; set; }
        internal string strNioroshiGyoshaName { get; set; }
        internal string strNioroshiGenbaName { get; set; }
        internal string strEigyoTantoshaName { get; set; }
        internal string strSharyoName { get; set; }
        internal string strShashuName { get; set; }
        internal string strUnpanGyoshaName { get; set; }
        internal string strNyuryokuTantoshaName { get; set; }
        internal string strUntenashaName { get; set; }
        internal string strKeitaiKbnName { get; set; }
        internal string strContenaName { get; set; }
        internal string strManiShuruiName { get; set; }
        internal string strManiTehaiName { get; set; }

        //明細
        internal Dictionary<int, string> strHinmeiName = new Dictionary<int,string>();
        internal Dictionary<int, string> strDenpyoKbnName = new Dictionary<int,string>();
        internal Dictionary<int, string> strTaniName = new Dictionary<int, string>();
        internal Dictionary<int, string> strYoukiName = new Dictionary<int, string>();
        internal Dictionary<int, string> strNisugataTaniName = new Dictionary<int, string>();  
    }

}
