using System;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Stock.ZaikoKanriHyo.Const;
using Shougun.Core.Stock.ZaikoKanriHyo.DAO;
using Shougun.Core.Stock.ZaikoKanriHyo.DTO;
using r_framework.APP.Base;

namespace Shougun.Core.Stock.ZaikoKanriHyo.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// 
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </summary>
    internal class DBAccessor
    {
        #region フィールド

        /// <summary>
        /// システム設定のDao
        /// </summary>
        internal IM_SYS_INFODao sysInfoDao;
        /// <summary>
        /// 業者のDao
        /// </summary>
        internal IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 現場のDao
        /// </summary>
        internal IM_GENBADao genbaDao;
        /// <summary>
        /// 在庫品名のDao
        /// </summary>
        internal IM_ZAIKO_HINMEIDao zaikoHinmeiDao;
        /// <summary>
        /// 開始在庫情報Dao
        /// </summary>
        internal IM_KAISHI_ZAIKO_INFODao kaishiZaikoDao;
        /// <summary>
        /// 在庫管理表のDao
        /// </summary>
        internal DAOClass zaikoKanriDao;
        /// <summary>
        /// 自社情報
        /// </summary>
        private IM_CORP_INFODao mCorpInfoDao;
        /// <summary>
        /// システム情報
        /// </summary>
        M_SYS_INFO sysInfo;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();
            this.kaishiZaikoDao = DaoInitUtility.GetComponent<IM_KAISHI_ZAIKO_INFODao>();
            this.zaikoKanriDao = DaoInitUtility.GetComponent<DAOClass>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.sysInfo = this.sysInfoDao.GetAllData().FirstOrDefault();
        }
        #endregion

        internal M_SYS_INFO GetSysInfoData()
        {
            M_SYS_INFO info = this.sysInfoDao.GetAllData().FirstOrDefault();
            return info;
        }

        internal M_ZAIKO_HINMEI GetZaikoHinmei(string zaikoHinmeiCd)
        {
            M_ZAIKO_HINMEI info = this.zaikoHinmeiDao.GetDataByCd(zaikoHinmeiCd);
            return info;
        }

        #region 明細用一覧データの取得
        /// <summary>
        /// 明細用一覧データの取得
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="form"></param>
        /// <param name="hadSearched"></param>
        internal DataTable GetIchiranData(UIConstans.ConditionInfo param, Form form, ref bool hadSearched)
        {
            // 共有情報をセット
            DataTable table = new DataTable();
            DTOClass dto = new DTOClass();
            dto.dateFrom = param.DateFrom.ToString();
            dto.dateTo = param.DateTo.ToString();
            dto.gyoushaFrom = param.GyoushaCdFrom;
            dto.gyoushaTo = param.GyoushaCdTo;
            dto.genbaFrom = param.GenbaCdFrom;
            dto.genbaTo = param.GenbaCdTo;
            dto.zaikoHinmeiFrom = param.ZaikoHinmeiCdFrom;
            dto.zaikoHinmeiTo = param.ZaikoHinmeiCdTo;

            if (param.OutPutKBN == 1)
            {
                table = this.zaikoKanriDao.GetIchiranData1(dto);
            }
            else
            {
                table = this.zaikoKanriDao.GetIchiranData2(dto);
            }

            // 検索完了用のフラグを立てる
            hadSearched = true;

            return table;
        }
        #endregion

        #region 在庫量を取得します
        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        internal ZaikoDTO GetZaiko1(string gyoushaCd, string genbaCd, string zaikoHInmeiCd, DateTime dateFrom, DateTime dateTo, SqlDateTime sysDate)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd, zaikoHInmeiCd, dateFrom, dateTo, sysDate);

            ZaikoDTO dto = new ZaikoDTO();
            dto.preZaikoRyou = 0;
            dto.preZaikoKingaku = 0;
            dto.ukeireRyou = 0;
            dto.shukkaRyou = 0;
            dto.chouseiIdouRyou = 0;
            dto.nowZaikoRyou = 0;
            dto.nowZaikoKingaku = 0;
            dto.totalZaikoRyou = 0;
            dto.totalZaikoKingaku = 0;
            decimal kaishiZaikoRyou = 0;
            decimal kaishiZaikoKingaku = 0;

            SqlDateTime DATE_FROM = SqlDateTime.Null;
            SqlDateTime DATE_TO = sysDate;
            // 繰越
            GetsujiDTO data = new GetsujiDTO();
            data.gyoushaCd = gyoushaCd;
            data.genbaCd = genbaCd;
            data.zaikoHinmeiCd = zaikoHInmeiCd;
            data.year = dateTo.Year;
            data.month = dateTo.Month;
            T_MONTHLY_LOCK_ZAIKO monthly = this.zaikoKanriDao.GetGetsujiData(data);

            if (monthly == null)
            {
                M_KAISHI_ZAIKO_INFO kaishiZaiko = new M_KAISHI_ZAIKO_INFO();
                kaishiZaiko.GYOUSHA_CD = gyoushaCd;
                kaishiZaiko.GENBA_CD = genbaCd;
                kaishiZaiko.ZAIKO_HINMEI_CD = zaikoHInmeiCd;
                kaishiZaiko = this.kaishiZaikoDao.GetAllValidData(kaishiZaiko).FirstOrDefault();

                if (kaishiZaiko != null)
                {
                    if (!kaishiZaiko.KAISHI_ZAIKO_RYOU.IsNull)
                    {
                        kaishiZaikoRyou += kaishiZaiko.KAISHI_ZAIKO_RYOU.Value;
                    }
                    if (!kaishiZaiko.KAISHI_ZAIKO_KINGAKU.IsNull)
                    {
                        kaishiZaikoKingaku += kaishiZaiko.KAISHI_ZAIKO_KINGAKU.Value;
                    }
                }
            }
            else
            {
                if (monthly.YEAR.Value == dateTo.Year && monthly.MONTH.Value == dateTo.Month)
                {
                    if (!monthly.PREVIOUS_MONTH_ZAIKO_RYOU.IsNull)
                    {
                        dto.preZaikoRyou = monthly.PREVIOUS_MONTH_ZAIKO_RYOU.Value;
                    }
                    if (!monthly.PREVIOUS_MONTH_KINGAKU.IsNull)
                    {
                        dto.preZaikoKingaku = monthly.PREVIOUS_MONTH_KINGAKU.Value;
                    }
                    if (!monthly.UKEIRE_RYOU.IsNull)
                    {
                        dto.ukeireRyou = monthly.UKEIRE_RYOU.Value;
                    }
                    if (!monthly.SHUKKA_RYOU.IsNull)
                    {
                        dto.shukkaRyou = monthly.SHUKKA_RYOU.Value;
                    }
                    if (!monthly.TYOUSEI_RYOU.IsNull)
                    {
                        dto.chouseiIdouRyou = monthly.TYOUSEI_RYOU.Value;
                    }
                    if (!monthly.IDOU_RYOU.IsNull)
                    {
                        dto.chouseiIdouRyou += monthly.IDOU_RYOU.Value;
                    }
                    if (!monthly.MONTH_ZAIKO_RYOU.IsNull)
                    {
                        dto.nowZaikoRyou = monthly.MONTH_ZAIKO_RYOU.Value;
                    }
                    if (!monthly.MONTH_KINGAKU.IsNull)
                    {
                        dto.nowZaikoKingaku = monthly.MONTH_KINGAKU.Value;
                    }
                    if (!monthly.GOUKEI_ZAIKO_RYOU.IsNull)
                    {
                        dto.totalZaikoRyou = monthly.GOUKEI_ZAIKO_RYOU.Value;
                    }
                    if (!monthly.GOUKEI_KINGAKU.IsNull)
                    {
                        dto.totalZaikoKingaku = monthly.GOUKEI_KINGAKU.Value;
                    }

                    if (this.sysInfo.ZAIKO_HYOUKA_HOUHOU.Value == 1)
                    {
                        M_ZAIKO_HINMEI hinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoHInmeiCd);
                        if (hinmei != null && !hinmei.ZAIKO_TANKA.IsNull)
                        {
                            dto.zaikoTanka = hinmei.ZAIKO_TANKA.Value;
                        }
                        else
                        {
                            dto.zaikoTanka = 0;
                        }
                    }
                    else
                    {
                        if (dto.totalZaikoRyou != 0)
                        {
                            dto.zaikoTanka = (dto.totalZaikoKingaku) / (dto.totalZaikoRyou);
                            switch (this.sysInfo.SYS_TANKA_FORMAT_CD.Value)
                            {
                                case 1:
                                case 2:
                                    dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                                    break;
                                case 3:
                                    dto.zaikoTanka = Math.Round(dto.zaikoTanka, 1, MidpointRounding.AwayFromZero);
                                    break;
                                case 4:
                                    dto.zaikoTanka = Math.Round(dto.zaikoTanka, 2, MidpointRounding.AwayFromZero);
                                    break;
                                case 5:
                                    dto.zaikoTanka = Math.Round(dto.zaikoTanka, 3, MidpointRounding.AwayFromZero);
                                    break;
                                default:
                                    dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                                    break;
                            }
                        }
                        else
                        {
                            dto.zaikoTanka = 0;
                        }
                    }

                    return dto;
                }
                else
                {
                    DATE_FROM = new DateTime(monthly.YEAR.Value, monthly.MONTH.Value, 1).AddMonths(1);
                    if (!monthly.GOUKEI_ZAIKO_RYOU.IsNull)
                    {
                        kaishiZaikoRyou += monthly.GOUKEI_ZAIKO_RYOU.Value;
                    }
                    if (!monthly.GOUKEI_KINGAKU.IsNull)
                    {
                        kaishiZaikoKingaku += monthly.GOUKEI_KINGAKU.Value;
                    }
                }
            }
            DATE_TO = dateFrom.AddDays(-1);
            DataTable dt = this.zaikoKanriDao.GetZaikoRyou1(gyoushaCd, genbaCd, zaikoHInmeiCd, DATE_FROM, DATE_TO);
            if (dt != null && dt.Rows.Count != 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_RYOU"])))
                {
                    dto.preZaikoRyou += Decimal.Parse(dt.Rows[0]["ZAIKO_RYOU"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_KINGAKU"])))
                {
                    dto.preZaikoKingaku += Decimal.Parse(dt.Rows[0]["ZAIKO_KINGAKU"].ToString());
                }
            }

            // 当月
            dt = this.zaikoKanriDao.GetZaikoRyou1(gyoushaCd, genbaCd, zaikoHInmeiCd, dateFrom, dateTo);
            if (dt != null && dt.Rows.Count != 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["UKEIRE_RYOU"])))
                {
                    dto.ukeireRyou += Decimal.Parse(dt.Rows[0]["UKEIRE_RYOU"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["SHUKKA_RYOU"])))
                {
                    dto.shukkaRyou += Decimal.Parse(dt.Rows[0]["SHUKKA_RYOU"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["IDOU_CHOUSEI_RYOU"])))
                {
                    dto.chouseiIdouRyou += Decimal.Parse(dt.Rows[0]["IDOU_CHOUSEI_RYOU"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_RYOU"])))
                {
                    dto.nowZaikoRyou += Decimal.Parse(dt.Rows[0]["ZAIKO_RYOU"].ToString());
                }
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_KINGAKU"])))
                {
                    dto.nowZaikoKingaku += Decimal.Parse(dt.Rows[0]["ZAIKO_KINGAKU"].ToString());
                }
            }

            if (this.sysInfo.ZAIKO_HYOUKA_HOUHOU.Value == 1)
            {
                decimal tanka = 0;
                M_ZAIKO_HINMEI hinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoHInmeiCd);
                if (hinmei != null && !hinmei.ZAIKO_TANKA.IsNull)
                {
                    tanka = hinmei.ZAIKO_TANKA.Value;
                }
                dto.preZaikoKingaku = dto.preZaikoRyou * tanka + kaishiZaikoKingaku;
                dto.preZaikoRyou += kaishiZaikoRyou;
                dto.nowZaikoKingaku = dto.nowZaikoRyou * tanka;
                dto.preZaikoKingaku = Math.Round(dto.preZaikoKingaku, MidpointRounding.AwayFromZero);
                dto.nowZaikoKingaku = Math.Round(dto.nowZaikoKingaku, MidpointRounding.AwayFromZero);
                dto.totalZaikoKingaku = dto.preZaikoKingaku + dto.nowZaikoKingaku;
                dto.totalZaikoRyou = dto.preZaikoRyou + dto.nowZaikoRyou;
                dto.zaikoTanka = tanka;
            }
            else
            {
                dto.preZaikoRyou += kaishiZaikoRyou;
                dto.preZaikoKingaku += kaishiZaikoKingaku;
                dto.preZaikoKingaku = Math.Round(dto.preZaikoKingaku, MidpointRounding.AwayFromZero);
                dto.nowZaikoKingaku = Math.Round(dto.nowZaikoKingaku, MidpointRounding.AwayFromZero);
                dto.totalZaikoKingaku = dto.preZaikoKingaku + dto.nowZaikoKingaku;
                dto.totalZaikoRyou = dto.preZaikoRyou + dto.nowZaikoRyou;
                if (dto.totalZaikoRyou != 0)
                {
                    dto.zaikoTanka = (dto.totalZaikoKingaku) / (dto.totalZaikoRyou);
                    switch (this.sysInfo.SYS_TANKA_FORMAT_CD.Value)
                    {
                        case 1:
                        case 2:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                            break;
                        case 3:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 1, MidpointRounding.AwayFromZero);
                            break;
                        case 4:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 2, MidpointRounding.AwayFromZero);
                            break;
                        case 5:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 3, MidpointRounding.AwayFromZero);
                            break;
                        default:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                            break;
                    }
                }
                else
                {
                    dto.zaikoTanka = 0;
                }
            }

            LogUtility.DebugMethodEnd(dto);

            return dto;
        }
        #endregion

        #region 在庫量を取得します
        /// <summary>
        /// 在庫量を取得します
        /// </summary>
        internal ZaikoDTO GetZaiko2(string zaikoHInmeiCd, DateTime dateFrom, DateTime dateTo, SqlDateTime sysDate)
        {
            LogUtility.DebugMethodStart(zaikoHInmeiCd, dateFrom, dateTo, sysDate);

            ZaikoDTO dto = new ZaikoDTO();
            dto.preZaikoRyou = 0;
            dto.preZaikoKingaku = 0;
            dto.nowZaikoRyou = 0;
            dto.nowZaikoKingaku = 0;
            decimal kaishiZaikoRyou = 0;
            decimal kaishiZaikoKingaku = 0;
            decimal preZaikoRyou = 0;
            decimal preZaikoKingaku = 0;
            decimal nowZaikoRyou = 0;
            decimal nowZaikoKingaku = 0;

            DataTable table = this.zaikoKanriDao.GetList(zaikoHInmeiCd, dateFrom.Year, dateFrom.Month);

            if (table == null || table.Rows.Count == 0)
            {
                return dto;
            }

            string gyoushaCd = "";
            string genbaCd = "";
            SqlDateTime DATE_FROM = SqlDateTime.Null;
            //var parentForm = (BusinessBaseForm)this.Parent;
            SqlDateTime DATE_TO = sysDate;

            decimal tanka = 0;
            M_ZAIKO_HINMEI hinmei = this.zaikoHinmeiDao.GetDataByCd(zaikoHInmeiCd);
            if (hinmei != null && !hinmei.ZAIKO_TANKA.IsNull)
            {
                tanka = hinmei.ZAIKO_TANKA.Value;
            }

            foreach (DataRow row in table.Rows)
            {
                gyoushaCd = Convert.ToString(row["GYOUSHA_CD"]);
                genbaCd = Convert.ToString(row["GENBA_CD"]);
                DATE_FROM = SqlDateTime.Null;
                DATE_TO = sysDate;

                kaishiZaikoRyou = 0;
                kaishiZaikoKingaku = 0;
                preZaikoRyou = 0;
                preZaikoKingaku = 0;
                nowZaikoRyou = 0;
                nowZaikoKingaku = 0;

                // 繰越
                GetsujiDTO data = new GetsujiDTO();
                data.gyoushaCd = gyoushaCd;
                data.genbaCd = genbaCd;
                data.zaikoHinmeiCd = zaikoHInmeiCd;
                data.year = dateTo.Year;
                data.month = dateTo.Month;
                T_MONTHLY_LOCK_ZAIKO monthly = this.zaikoKanriDao.GetGetsujiData(data);

                if (monthly == null)
                {
                    M_KAISHI_ZAIKO_INFO kaishiZaiko = new M_KAISHI_ZAIKO_INFO();
                    kaishiZaiko.GYOUSHA_CD = gyoushaCd;
                    kaishiZaiko.GENBA_CD = genbaCd;
                    kaishiZaiko.ZAIKO_HINMEI_CD = zaikoHInmeiCd;
                    kaishiZaiko = this.kaishiZaikoDao.GetAllValidData(kaishiZaiko).FirstOrDefault();

                    if (kaishiZaiko != null)
                    {
                        if (!kaishiZaiko.KAISHI_ZAIKO_RYOU.IsNull)
                        {
                            kaishiZaikoRyou = kaishiZaiko.KAISHI_ZAIKO_RYOU.Value;
                        }
                        if (!kaishiZaiko.KAISHI_ZAIKO_KINGAKU.IsNull)
                        {
                            kaishiZaikoKingaku = kaishiZaiko.KAISHI_ZAIKO_KINGAKU.Value;
                        }
                    }
                }
                else
                {
                    if (monthly.YEAR.Value == dateTo.Year && monthly.MONTH.Value == dateTo.Month)
                    {
                        if (!monthly.PREVIOUS_MONTH_ZAIKO_RYOU.IsNull)
                        {
                            dto.preZaikoRyou += monthly.PREVIOUS_MONTH_ZAIKO_RYOU.Value;
                        }
                        if (!monthly.PREVIOUS_MONTH_KINGAKU.IsNull)
                        {
                            dto.preZaikoKingaku += monthly.PREVIOUS_MONTH_KINGAKU.Value;
                        }
                        if (!monthly.UKEIRE_RYOU.IsNull)
                        {
                            dto.ukeireRyou += monthly.UKEIRE_RYOU.Value;
                        }
                        if (!monthly.SHUKKA_RYOU.IsNull)
                        {
                            dto.shukkaRyou += monthly.SHUKKA_RYOU.Value;
                        }
                        if (!monthly.TYOUSEI_RYOU.IsNull)
                        {
                            dto.chouseiIdouRyou += monthly.TYOUSEI_RYOU.Value;
                        }
                        if (!monthly.IDOU_RYOU.IsNull)
                        {
                            dto.chouseiIdouRyou += monthly.IDOU_RYOU.Value;
                        }
                        if (!monthly.MONTH_ZAIKO_RYOU.IsNull)
                        {
                            dto.nowZaikoRyou += monthly.MONTH_ZAIKO_RYOU.Value;
                        }
                        if (!monthly.MONTH_KINGAKU.IsNull)
                        {
                            dto.nowZaikoKingaku += monthly.MONTH_KINGAKU.Value;
                        }
                        if (!monthly.GOUKEI_ZAIKO_RYOU.IsNull)
                        {
                            dto.totalZaikoRyou += monthly.GOUKEI_ZAIKO_RYOU.Value;
                        }
                        if (!monthly.GOUKEI_KINGAKU.IsNull)
                        {
                            dto.totalZaikoKingaku += monthly.GOUKEI_KINGAKU.Value;
                        }
                        continue;
                    }
                    else
                    {
                        DATE_FROM = new DateTime(monthly.YEAR.Value, monthly.MONTH.Value, 1).AddMonths(1);
                        if (!monthly.GOUKEI_ZAIKO_RYOU.IsNull)
                        {
                            kaishiZaikoRyou = monthly.GOUKEI_ZAIKO_RYOU.Value;
                        }
                        if (!monthly.GOUKEI_KINGAKU.IsNull)
                        {
                            kaishiZaikoKingaku = monthly.GOUKEI_KINGAKU.Value;
                        }
                    }
                }
                DATE_TO = dateTo.AddMonths(-1);
                DataTable dt = this.zaikoKanriDao.GetZaikoRyou1(gyoushaCd, genbaCd, zaikoHInmeiCd, DATE_FROM, DATE_TO);
                if (dt != null && dt.Rows.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_RYOU"])))
                    {
                        preZaikoRyou += Decimal.Parse(dt.Rows[0]["ZAIKO_RYOU"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_KINGAKU"])))
                    {
                        preZaikoKingaku += Decimal.Parse(dt.Rows[0]["ZAIKO_KINGAKU"].ToString());
                    }
                }

                // 当月
                dt = this.zaikoKanriDao.GetZaikoRyou1(gyoushaCd, genbaCd, zaikoHInmeiCd, dateFrom, dateTo);
                if (dt != null && dt.Rows.Count != 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["UKEIRE_RYOU"])))
                    {
                        dto.ukeireRyou += Decimal.Parse(dt.Rows[0]["UKEIRE_RYOU"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["SHUKKA_RYOU"])))
                    {
                        dto.shukkaRyou += Decimal.Parse(dt.Rows[0]["SHUKKA_RYOU"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["IDOU_CHOUSEI_RYOU"])))
                    {
                        dto.chouseiIdouRyou += Decimal.Parse(dt.Rows[0]["IDOU_CHOUSEI_RYOU"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_RYOU"])))
                    {
                        nowZaikoRyou = Decimal.Parse(dt.Rows[0]["ZAIKO_RYOU"].ToString());
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ZAIKO_KINGAKU"])))
                    {
                        nowZaikoKingaku = Decimal.Parse(dt.Rows[0]["ZAIKO_KINGAKU"].ToString());
                    }
                }

                if (this.sysInfo.ZAIKO_HYOUKA_HOUHOU.Value == 1)
                {
                    preZaikoKingaku = preZaikoRyou * tanka + kaishiZaikoKingaku;
                    preZaikoRyou += kaishiZaikoRyou;
                    nowZaikoKingaku = nowZaikoRyou * tanka;
                    preZaikoKingaku = Math.Round(preZaikoKingaku, MidpointRounding.AwayFromZero);
                    nowZaikoKingaku = Math.Round(nowZaikoKingaku, MidpointRounding.AwayFromZero);
                }
                else
                {
                    preZaikoKingaku += kaishiZaikoKingaku;
                    preZaikoRyou += kaishiZaikoRyou;
                    preZaikoKingaku = Math.Round(preZaikoKingaku, MidpointRounding.AwayFromZero);
                    nowZaikoKingaku = Math.Round(nowZaikoKingaku, MidpointRounding.AwayFromZero);
                }
                dto.preZaikoKingaku += preZaikoKingaku;
                dto.preZaikoRyou += preZaikoRyou;
                dto.nowZaikoKingaku += nowZaikoKingaku;
                dto.nowZaikoRyou += nowZaikoRyou;
                dto.totalZaikoKingaku += preZaikoKingaku + nowZaikoKingaku;
                dto.totalZaikoRyou += preZaikoRyou + nowZaikoRyou;
            }

            if (this.sysInfo.ZAIKO_HYOUKA_HOUHOU.Value == 1)
            {
                dto.zaikoTanka = tanka;
            }
            else
            {
                if (dto.totalZaikoRyou != 0)
                {
                    dto.zaikoTanka = (dto.totalZaikoKingaku) / (dto.totalZaikoRyou);
                    switch (this.sysInfo.SYS_TANKA_FORMAT_CD.Value)
                    {
                        case 1:
                        case 2:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                            break;
                        case 3:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 1, MidpointRounding.AwayFromZero);
                            break;
                        case 4:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 2, MidpointRounding.AwayFromZero);
                            break;
                        case 5:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 3, MidpointRounding.AwayFromZero);
                            break;
                        default:
                            dto.zaikoTanka = Math.Round(dto.zaikoTanka, 0, MidpointRounding.AwayFromZero);
                            break;
                    }
                }
                else
                {
                    dto.zaikoTanka = 0;
                }
            }

            LogUtility.DebugMethodEnd(dto);

            return dto;
        }
        #endregion

        #region 自社情報マスタテーブル会社名SELECT
        /// <summary>
        /// 自社情報マスタテーブル会社名SELECT
        /// </summary>
        /// <returns></returns>
        internal String SelectCorpName()
        {
            M_CORP_INFO[] corpInfo;

            corpInfo = (M_CORP_INFO[])mCorpInfoDao.GetAllData();
            return corpInfo[0].CORP_RYAKU_NAME;
        }
        #endregion

        #region 業者CDに紐付けられた略名を返却する
        /// <summary>
        /// 業者CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="GyoushaCD">略名を取得したい業者CD</param>
        /// <returns name="string">業者略名</returns>
        internal string GetGyoushaName(string GyoushaCD)
        {
            // 取得したentityより略名を取得
            M_GYOUSHA entity = this.gyoushaDao.GetDataByCd(GyoushaCD);
            if (entity != null)
            {
                return entity.GYOUSHA_NAME_RYAKU;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 現場CDに紐付けられた略名を返却する
        /// <summary>
        /// 現場CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="GyoushaCD">略名を取得したい現場CD</param>
        /// <returns name="string">現場略名</returns>
        internal string GetGenbaName(string GyoushaCD, string GenbaCD)
        {
            // 取得したentityより略名を取得
            M_GENBA data = new M_GENBA();
            data.GYOUSHA_CD = GyoushaCD;
            data.GENBA_CD = GenbaCD;
            M_GENBA entity = this.genbaDao.GetDataByCd(data);
            if (entity != null)
            {
                return entity.GENBA_NAME_RYAKU;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 在庫品名CDに紐付けられた略名を返却する
        /// <summary>
        /// 在庫品名CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="GyoushaCD">略名を取得したい在庫品名CD</param>
        /// <returns name="string">在庫品名</returns>
        internal string GetZaikoHinmeiName(string ZaikoHinmeiCd)
        {
            // 取得したentityより略名を取得
            M_ZAIKO_HINMEI entity = this.zaikoHinmeiDao.GetDataByCd(ZaikoHinmeiCd);
            if (entity != null)
            {
                return entity.ZAIKO_HINMEI_NAME_RYAKU;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
