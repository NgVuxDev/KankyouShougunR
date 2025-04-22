using System.Collections.Generic;
using System.Data.SqlTypes;
using r_framework.Entity;
using Shougun.Function.ShougunCSCommon.Const;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO
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

        public string eigyouTantoushaCd { get; set; }

        public string denshuKbnCd { get; set; }
    }

    public class ResultDto<S, T>
        where S : SuperEntity
        where T : SuperEntity
    {
        public long denpyouNo { get; set; }

        public int rowIndex { get; set; }

        public S entry { get; set; }

        public List<T> detailList { get; set; }

        public Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>> detailZaikoUkeireDetails { get; set; }

        public Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>> detailZaikoShukkaDetails { get; set; }

        public Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>> detailZaikoHinmeiHuriwakes { get; set; }

        public Dictionary<string, List<T_KENSHU_DETAIL>> detailKenshuDetails { get; set; }

        public List<T_CONTENA_RESULT> contenaResults { get; set; }

        public List<T_CONTENA_RESERVE> contenaReserveList;

        public List<M_CONTENA> contenaMasterList;

        public Dictionary<string, bool> tankaChanged { get; set; }

        public M_TORIHIKISAKI_SEIKYUU seikyuu { get; set; }

        public M_TORIHIKISAKI_SHIHARAI shiharai { get; set; }

        public ResultDto()
        {
            detailList = new List<T>();
            detailZaikoUkeireDetails = new Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>>();
            detailZaikoShukkaDetails = new Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>>();
            detailZaikoHinmeiHuriwakes = new Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>>();
            detailKenshuDetails = new Dictionary<string, List<T_KENSHU_DETAIL>>();
            contenaResults = new List<T_CONTENA_RESULT>();
            contenaReserveList = new List<T_CONTENA_RESERVE>();
            contenaMasterList = new List<M_CONTENA>();
            tankaChanged = new Dictionary<string, bool>();
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

    public class TabGoDto
    {
        public string ControlName { get; set; }
        public string NextControlName { get; set; }
        public string PreviousControlName { get; set; }
        public bool isLast { get; set; }
        public bool isFirst { get; set; }
    }
}
