using System;
using System.Data.SqlTypes;
using r_framework.Entity;
using System.Collections.Generic;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO
{
    public class NyuuryokuParamDto
    {
        public SqlInt16 kyotenCd { get; set; }

        public string kyotenName { get; set; }

        public string denpyouDate { get; set; }

        public string uriageDate { get; set; }

        public string shiharaiDate { get; set; }

        public string torihikisakiCd { get; set; }

        public string torihikisakiName { get; set; }

        public string gyoushaCd { get; set; }

        public string gyoushaName { get; set; }

        public string genbaCd { get; set; }

        public string genbaName { get; set; }

        public string upnGyoushaCd { get; set; }

        public string upnGyoushaName { get; set; }

        public string untenshaCd { get; set; }

        public string untenshaName { get; set; }

        public string nizumiGyoushaCd { get; set; }

        public string nizumiGyoushaName { get; set; }

        public string nizumiGenbaCd { get; set; }

        public string nizumiGenbaName { get; set; }

        public string nioroshiGyoushaCd { get; set; }

        public string nioroshiGyoushaName { get; set; }

        public string nioroshiGenbaCd { get; set; }

        public string nioroshiGenbaName { get; set; }

        public string shashuCd { get; set; }

        public string shashuName { get; set; }

        public string sharyouCd { get; set; }

        public string sharyouName { get; set; }

        public string eigyouTantoushaCd { get; set; }

        public string eigyouTantoushaName { get; set; }

        public SqlInt16 keitaiKbnCd { get; set; }

        public string keitaiKbnName { get; set; }

        public SqlInt16 daikanKbn { get; set; }

        public string daikanKbnName { get; set; }

        public string manifestShuruiCd { get; set; }

        public string manifestShuruiName { get; set; }

        public string manifestTehaiCd { get; set; }

        public string manifestTehaiName { get; set; }

        public string denpyouBikou { get; set; }

        public string taipyuuBikou { get; set; }
    }   
}
