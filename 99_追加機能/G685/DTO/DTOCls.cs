using System.Collections.Generic;
using System.Data.SqlTypes;
using r_framework.Entity;
using Shougun.Function.ShougunCSCommon.Const;

namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.DTO
{
    public class SearchDto
    {
        public string HidukeFrom { get; set; }

        public string HidukeTo { get; set; }

        public SqlInt16 kyotenCd { get; set; }

        public SqlInt16 kakuteiKbn { get; set; }

        public string torihikisakiCd { get; set; }

        public string gyoushaCd { get; set; }

        public string genbaCd { get; set; }

        public string upnGyoushaCd { get; set; }

        public string nizumiGyoushaCd { get; set; }

        public string nizumiGenbaCd { get; set; }

        public string nioroshiGyoushaCd { get; set; }

        public string nioroshiGenbaCd { get; set; }

        public string hinmeiCd { get; set; }

        public string shuruiCd { get; set; }

        public string bunruiCd { get; set; }

        public SqlInt16 unitCd { get; set; }

        public string denshuKbnCd { get; set; }

        public SqlInt16 denpyouKbnCd { get; set; }

        public SqlInt16 torihikiKbnCd { get; set; }
    }

    public class ResultDto<S, T>
        where S : SuperEntity
        where T : SuperEntity
    {
        public long denpyouNo { get; set; }

        public int rowIndex { get; set; }

        public S entry { get; set; }

        public List<T> detailList { get; set; }

        public List<T> detailListHidden { get; set; }

        public bool check { get; set; }

        public S_NUMBER_DAY numberDay { get; set; }

        public S_NUMBER_YEAR numberYear { get; set; }

        public SalesPaymentConstans.REGIST_KBN hiRenbanRegistKbn { get; set; }

        public SalesPaymentConstans.REGIST_KBN nenRenbanRegistKbn { get; set; }

        public Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>> detailZaikoUkeireDetails { get; set; }

        public Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>> detailZaikoShukkaDetails { get; set; }

        public Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>> detailZaikoHinmeiHuriwakes { get; set; }

        public List<T_CONTENA_RESULT> contenaResults { get; set; }

        public List<T_CONTENA_RESERVE> contenaReserveList;

        public List<M_CONTENA> contenaMasterList;

        public byte[] numberDayTimeStamp { get; set; }

        public byte[] numberYearTimeStamp { get; set; }

        public M_TORIHIKISAKI_SEIKYUU seikyuu { get; set; }

        public M_TORIHIKISAKI_SHIHARAI shiharai { get; set; }

        public ResultDto()
        {
            detailList = new List<T>();
            detailListHidden = new List<T>();
            detailZaikoUkeireDetails = new Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>>();
            detailZaikoShukkaDetails = new Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>>();
            detailZaikoHinmeiHuriwakes = new Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            contenaResults = new List<T_CONTENA_RESULT>();
            contenaReserveList = new List<T_CONTENA_RESERVE>();
            contenaMasterList = new List<M_CONTENA>();
            check = false;
        }

        public List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoHinmeiHuriwakeListByDetail(SqlInt64 systemId, SqlInt64 detailSystemId, SqlInt32 seq)
        {
            string key = string.Format("{0}_{1}_{2}", systemId.Value.ToString(), detailSystemId.Value.ToString(), seq.Value.ToString());
            if (this.detailZaikoHinmeiHuriwakes.ContainsKey(key))
            {
                return this.detailZaikoHinmeiHuriwakes[key];
            }
            else
            {
                return null;
            }
        }     
    }

    public class SendPopupParam
    {
        public bool cbxHinmei = false;
        public bool cbxSuuryou = false;
        public bool cbxTanka = false;
        public bool cbxMeisaiBikou = false;
        public bool cbxUnit = false;
        public string denshuKbnCd = "";
    }

    public class TabGoDto
    {
        public string ControlName { get; set; }
        public string NextControlName { get; set; }
        public string PreviousControlName { get; set; }
        public bool isLast { get; set; }
        public bool isFirst { get; set; }
    }
}
