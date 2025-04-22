// $Id: DBAccessor.cs 47466 2015-04-15 13:42:50Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Text;
using GrapeCity.Win.MultiRow;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dao;
using r_framework.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Function.ShougunCSCommon.Dto;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    /// <summary>
    /// DBAccessするためのクラス
    ///
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </summary>
    public class DBAccessor
    {
        #region フィールド

        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IM_HINMEIDao
        /// </summary>
        M_HINMEIDao hinmeiDao;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        r_framework.Dao.IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_SHOUHIZEIDao
        /// </summary>
        r_framework.Dao.IM_SHOUHIZEIDao shouhizeiDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        r_framework.Dao.IM_GENBADao genbaDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        r_framework.Dao.IM_SHAINDao shainDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SEIKYUUDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SHIHARAIDao
        /// </summary>
        r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        /// <summary>
        /// IM_SHARYOUDao
        /// </summary>
        r_framework.Dao.IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        r_framework.Dao.IM_UNITDao unitDao;

        /// <summary>
        /// IM_SHASHUDao
        /// </summary>
        r_framework.Dao.IM_SHASHUDao shashuDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// 日連番Dao
        /// </summary>
        IS_NUMBER_DAYDao numberDayDao;

        /// <summary>
        /// 年連番Dao
        /// </summary>
        IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IM_CORP_INFODao
        /// </summary>
        IM_CORP_INFODao mCorpInfoDao;

        /// <summary>
        /// IT_SEIKYUU_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao seikyuuDetail;

        /// <summary>
        /// IT_SEISAN_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao seisanDetail;

        private T_UR_SH_ENTRYDao entryDao;
        private T_UR_SH_DETAILDao detailDao;

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        r_framework.Dao.IM_KOBETSU_HINMEIDao kobetsuHinmei;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<M_HINMEIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
            this.entryDao = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();
            this.detailDao = DaoInitUtility.GetComponent<T_UR_SH_DETAILDao>();
            this.seikyuuDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao>();
            this.seisanDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao>();
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.numberDayDao = DaoInitUtility.GetComponent<IS_NUMBER_DAYDao>();
            this.numberYearDao = DaoInitUtility.GetComponent<IS_NUMBER_YEARDao>();
            this.mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<IM_SHOUHIZEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.kobetsuHinmei = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
        }
        #endregion

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        /// <summary>
        /// 個別品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_KOBETSU_HINMEI GetKobetsuHinmeiDataByCd(string gyoushaCd, string genbaCd, string hinmeiCd, SqlInt16 denpyou_kbn)
        {
            M_KOBETSU_HINMEI keyEntity = new M_KOBETSU_HINMEI();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.HINMEI_CD = hinmeiCd;

            M_HINMEI hinmei = new M_HINMEI();
            hinmei.HINMEI_CD = hinmeiCd;
            hinmei.DENPYOU_KBN_CD = denpyou_kbn;

            M_KOBETSU_HINMEI returnValue = this.kobetsuHinmei.GetDataByHinmei(keyEntity, hinmei);

            return returnValue;
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #region DBアクセッサ

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            // TODO: ログイン時に共通メンバでSYS_INFOの情報を保持する可能性があるため、
            //       その場合、このメソッドは必要なくなる。
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            if (returnEntity != null && returnEntity.Length > 0)
            {
                return returnEntity[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// M_CORP_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_CORP_INFO GetMCorpInfo()
        {
            M_CORP_INFO[] returnEntity = mCorpInfoDao.GetAllData();
            if (returnEntity != null && returnEntity.Length > 0)
            {
                return returnEntity[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 代納入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateSystemIdForDainou()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 代納入力用の伝票番号採番処理
        /// </summary>
        /// <returns></returns>
        public SqlInt64 CreateDainouNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDenshuDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDenshuDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 代納入力用のDATE_NUMBER採番処理
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <param name="numberedDay"></param>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        public SqlInt32 CreateDayNumberForDainou(DateTime numberedDay, SqlInt16 kyotenCd)
        {
            SqlInt32 returnInt = 1;

            var entity = new S_NUMBER_DAY();
            entity.NUMBERED_DAY = numberedDay.Date;
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
            entity.KYOTEN_CD = kyotenCd;

            var updateEntity = this.numberDayDao.GetNumberDayData(entity);
            returnInt = this.numberDayDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DAY();
                updateEntity.NUMBERED_DAY = numberedDay.Date;
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
                updateEntity.KYOTEN_CD = kyotenCd;
                updateEntity.CURRENT_NUMBER = 1;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_DAY>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberDayDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberDayDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 代納入力用のYEAR_NUMBER採番処理
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <param name="numberedYear"></param>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        public SqlInt32 CreateYearNumberForDainou(SqlInt32 numberedYear, SqlInt16 kyotenCd)
        {
            SqlInt32 returnInt = 1;

            var entity = new S_NUMBER_YEAR();
            entity.NUMBERED_YEAR = numberedYear;
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
            entity.KYOTEN_CD = kyotenCd;

            var updateEntity = this.numberYearDao.GetNumberYearData(entity);
            returnInt = this.numberYearDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_YEAR();
                updateEntity.NUMBERED_YEAR = numberedYear;
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
                updateEntity.KYOTEN_CD = kyotenCd;
                updateEntity.CURRENT_NUMBER = 1;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_YEAR>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberYearDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                this.numberYearDao.Update(updateEntity);
            }

            return returnInt;
        }

        /// <summary>
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="hinmeiCD">品名CD</param>
        /// <returns></returns>
        public DataTable GetHinmeiDataForUkeire(string hinmeiCD)
        {
            return this.hinmeiDao.GetHinmeiDataForUkeire(hinmeiCD);
        }

        /// <summary>
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="hinmeiCD">品名CD</param>
        /// <returns></returns>
        public DataTable GetHinmeiDataForShukka(string hinmeiCD)
        {
            return this.hinmeiDao.GetHinmeiDataForShukka(hinmeiCD);
        }

        /// <summary>
        /// 拠点を取得
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <param name="isCheckDelete">False：削除済みデータを含む</param>
        /// <returns></returns>
        public M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd, bool isCheckDelete = true)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = !isCheckDelete;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 入力担当者を取得
        /// </summary>
        /// <param name="shainCd"></param>
        /// <returns></returns>
        public M_SHAIN GetNyuuryokuTantousha(string shainCd)
        {
            M_SHAIN entity = new M_SHAIN();
            entity.SHAIN_CD = shainCd;
            entity.NYUURYOKU_TANTOU_KBN = true;
            var shainEntirys = this.shainDao.GetAllValidData(entity);
            if (shainEntirys == null || shainEntirys.Length < 1)
            {
                return null;
            }
            else
            {
                return shainEntirys[0];
            }
        }

        /// <summary>
        /// 指定されたSqlで業者情報を取得する
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable GetGyoushaDateForStringSql(string strSql)
        {
            return this.gyoushaDao.GetDateForStringSql(strSql);
        }

        /// <summary>
        /// 運搬業者を取得
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA GetUnpanGyousya(string gyoushaCd, object strDenpyouDate, object sysDate)
        {
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            var gyousha = this.gyoushaDao.GetDainouUnpanGyoushaData(entity);

            if (gyousha == null)
            {
                return null;
            }

            if (null != gyousha)
            {
                string strBegin = gyousha.TEKIYOU_BEGIN.ToString();
                string strEnd = gyousha.TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (gyousha.TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (gyousha.TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            return gyousha;
        }

        /// <summary>
        /// 運転者を取得
        /// </summary>
        /// <param name="untenshaCd"></param>
        /// <returns></returns>
        public M_SHAIN GetUntensha(string untenshaCd)
        {
            M_SHAIN entity = new M_SHAIN();
            entity.SHAIN_CD = untenshaCd;
            entity.UNTEN_KBN = true;
            var untenList = this.shainDao.GetAllValidData(entity);
            if (untenList == null || untenList.Length < 1)
            {
                return null;
            }
            else
            {
                return untenList[0];
            }
        }

        /// <summary>
        /// 営業車を取得
        /// </summary>
        /// <param name="eigyoushaCd"></param>
        /// <returns></returns>
        public M_SHAIN GetEigyousha(string eigyoushaCd)
        {
            M_SHAIN entity = new M_SHAIN();
            entity.SHAIN_CD = eigyoushaCd;
            entity.EIGYOU_TANTOU_KBN = true;
            var eigyoushaList = this.shainDao.GetAllValidData(entity);
            if (eigyoushaList == null || eigyoushaList.Length < 1)
            {
                return null;
            }
            else
            {
                return eigyoushaList[0];
            }
        }

        /// <summary>
        /// 社員取得
        /// </summary>
        /// <param name="shainCd"></param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd)
        {
            if (string.IsNullOrEmpty(shainCd))
            {
                return null;
            }

            M_SHAIN keyEntity = new M_SHAIN();
            keyEntity.SHAIN_CD = shainCd;
            var shain = this.shainDao.GetAllValidData(keyEntity);

            if (shain == null || shain.Length < 1)
            {
                return null;
            }
            else
            {
                return shain[0];
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            if (null != gyousha && gyousha.Length > 0)
            {
                string strBegin = gyousha[0].TEKIYOU_BEGIN.ToString();
                string strEnd = gyousha[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (gyousha[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (gyousha[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            return gyousha[0];

        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            List<M_GENBA> retlist = new List<M_GENBA>();
            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < genba.Length; i++)
            {
                string strBegin = genba[i].TEKIYOU_BEGIN.ToString();
                string strEnd = genba[i].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (genba[i].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (genba[i].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        continue;
                    }
                    else
                    {
                        retlist.Add(genba[i]);
                    }
                }
            }

            return retlist.ToArray();
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            if (null != genba && genba.Length > 0)
            {
                string strBegin = genba[0].TEKIYOU_BEGIN.ToString();
                string strEnd = genba[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (genba[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (genba[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            // PK指定のため1件
            return genba[0];
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }

            if (null != torihikisaki && torihikisaki.Length > 0)
            {
                string strBegin = torihikisaki[0].TEKIYOU_BEGIN.ToString();
                string strEnd = torihikisaki[0].TEKIYOU_END.ToString();
                string sagyobi = string.Empty;
                if (strDenpyouDate != null)
                {
                    sagyobi = strDenpyouDate.ToString();
                }
                else
                {
                    sagyobi = sysDate.ToString();
                }

                if (torihikisaki[0].TEKIYOU_BEGIN.IsNull)
                {
                    strBegin = "0001/01/01 00:00:01";
                }

                if (torihikisaki[0].TEKIYOU_END.IsNull)
                {
                    strEnd = "9999/12/31 23:59:59";
                }

                if (!string.IsNullOrEmpty(sagyobi))
                {
                    //作業日は適用期間より範囲外の場合
                    if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                    {
                        return null;
                    }
                }
            }

            return torihikisaki[0];
        }

        /// <summary>
        /// 取引先_請求情報取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuu(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI_SEIKYUU keyEntity = new M_TORIHIKISAKI_SEIKYUU();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisakiSeikyuu = this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
            if (torihikisakiSeikyuu == null)
            {
                return null;
            }

            return torihikisakiSeikyuu;
        }

        /// <summary>
        /// 取引先_支払情報取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharai(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_TORIHIKISAKI_SHIHARAI keyEntity = new M_TORIHIKISAKI_SHIHARAI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisakiShiharai = this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
            if (torihikisakiShiharai == null)
            {
                return null;
            }

            return torihikisakiShiharai;
        }

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd, SqlDateTime strDenpyouDate)
        {
            if (string.IsNullOrEmpty(sharyouCd))
            {
                return null;
            }

            M_SHARYOU keyEntity = new M_SHARYOU();
            keyEntity.SHARYOU_CD = sharyouCd;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                keyEntity.GYOUSHA_CD = gyoushaCd;
            }
            if (!string.IsNullOrEmpty(shasyuCd))
            {
                keyEntity.SHASYU_CD = shasyuCd;
            }
            if (!string.IsNullOrEmpty(shainCd))
            {
                keyEntity.SHAIN_CD = shainCd;
            }

            var sharyous = this.sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "3", strDenpyouDate, true, false, false);
            if (sharyous == null || sharyous.Length < 1)
            {
                return null;
            }

            return sharyous;
        }

        /// <summary>
        /// 車輌休動情報の取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="sharyouCd"></param>
        /// <param name="sagyouDate"></param>
        /// <returns></returns>
        public M_WORK_CLOSED_SHARYOU[] GetWorkClosedSharyou(string gyoushaCd, string sharyouCd, string sagyouDate)
        {
            M_WORK_CLOSED_SHARYOU workclosedsharyouEntry = new M_WORK_CLOSED_SHARYOU();
            // 運搬業者CD
            workclosedsharyouEntry.GYOUSHA_CD = gyoushaCd;
            // 車輌CD取得
            workclosedsharyouEntry.SHARYOU_CD = sharyouCd;
            // 作業日取得
            workclosedsharyouEntry.CLOSED_DATE = Convert.ToDateTime(sagyouDate);

            return this.workclosedsharyouDao.GetAllValidData(workclosedsharyouEntry);
        }

        /// <summary>
        /// 車輌休動情報の取得
        /// </summary>
        /// <param name="untenshaCd"></param>
        /// <param name="sagyouDate"></param>
        /// <returns></returns>
        public M_WORK_CLOSED_UNTENSHA[] GetWorkClosedUntensha(string untenshaCd, string sagyouDate)
        {
            M_WORK_CLOSED_UNTENSHA workcloseduntenshaEntry = new M_WORK_CLOSED_UNTENSHA();
            // 運転者CD取得
            workcloseduntenshaEntry.SHAIN_CD = untenshaCd;
            // 作業日取得
            workcloseduntenshaEntry.CLOSED_DATE = Convert.ToDateTime(sagyouDate);

            return workcloseduntenshaDao.GetAllValidData(workcloseduntenshaEntry);
        }

        /// <summary>
        /// 単位区分取得
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <param name="isCheckDelete">False：削除済みデータを含む</param>
        /// <returns></returns>
        public M_UNIT[] GetUnit(short unitCd, bool isCheckDelete = true)
        {
            if (unitCd < 0)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = !isCheckDelete;
            var units = this.unitDao.GetAllValidData(keyEntity);
            if (units == null || units.Length < 1)
            {
                return null;
            }

            return units;
        }

        /// <summary>
        /// 単位一覧取得
        /// </summary>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit()
        {
            M_UNIT keyEntity = new M_UNIT();
            return this.unitDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="targetEntity"></param>
        public M_SHASHU GetShashu(string strShashuCd)
        {
            if (string.IsNullOrEmpty(strShashuCd))
            {
                return null;
            }

            M_SHASHU keyEntity = new M_SHASHU();
            keyEntity.SHASHU_CD = strShashuCd;
            var shashu = this.shashuDao.GetAllValidData(keyEntity);

            if (shashu == null || shashu.Length < 1)
            {
                return null;
            }
            else
            {
                return shashu[0];
            }
        }

        /// <summary>
        /// 指定された受入番号の次に大きい番号を取得
        /// </summary>
        /// <param name="denpyouNumber"></param>
        /// <param name="KyotenCd"></param>
        /// <returns></returns>
        internal long GetNextUkeireNumber(long denpyouNumber, string kyotenCd)
        {
            long returnValue = 0;
            T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
            entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd);
            entity.UR_SH_NUMBER = denpyouNumber;
            var dainou = this.entryDao.GetDainoDataForNextDainounumber(entity);
            if (dainou == null)
            {
                returnValue = this.GetMinDainouNumber(kyotenCd);
            }
            else
            {
                if (!dainou.UR_SH_NUMBER.IsNull)
                {
                    returnValue = long.Parse(dainou.UR_SH_NUMBER.ToString());
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 指定された受入番号の次に小さい番号を取得
        /// </summary>
        /// <param name="denpyouNumber"></param>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        internal long GetPreUkeireNumber(long denpyouNumber, string kyotenCd)
        {
            long returnValue = 0;
            T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
            entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd);
            entity.UR_SH_NUMBER = denpyouNumber;
            var dainou = this.entryDao.GetDainoDataForFrontDainounumber(entity);
            if (dainou == null)
            {
                returnValue = this.GetMaxDainouNumber(kyotenCd);
            }
            else
            {
                if (!dainou.UR_SH_NUMBER.IsNull)
                {
                    returnValue = long.Parse(dainou.UR_SH_NUMBER.ToString());
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 最大代納番号の取得
        /// </summary>
        internal long GetMaxDainouNumber(string kyotenCd)
        {
            LogUtility.DebugMethodStart();
            long dainouNumber = 0;
            try
            {
                T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
                entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd);
                // 最大代納番号の取得
                dainouNumber = this.entryDao.GetMaxDainouNumber(entity);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
            return dainouNumber;
        }

        /// <summary>
        /// 最小代納番号の取得
        /// </summary>
        internal long GetMinDainouNumber(string kyotenCd)
        {
            LogUtility.DebugMethodStart();
            long dainouNumber = 0;
            try
            {
                T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
                entity.KYOTEN_CD = SqlInt16.Parse(kyotenCd);
                //最小代納番号の取得
                dainouNumber = this.entryDao.GetMinDainouNumber(entity);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
            return dainouNumber;
        }

        /// <summary>
        /// 「伝票番号」存在チェック
        /// </summary>
        /// <param name="denpyouNumber"></param>
        /// <returns></returns>
        internal DataTable GetDainouUkeireShukkaData(SqlInt64 denpyouNumber)
        {
            T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
            entity.UR_SH_NUMBER = denpyouNumber;
            return this.entryDao.GetDainoUkeireShukkaData(entity);
        }

        /// <summary>
        /// 「伝票番号」で売上支払データの存在チェック
        /// </summary>
        /// <param name="denpyouNumber"></param>
        /// <returns></returns>
        internal DataTable GetUrshData(SqlInt64 denpyouNumber)
        {
            T_UR_SH_ENTRY entity = new T_UR_SH_ENTRY();
            entity.UR_SH_NUMBER = denpyouNumber;
            return this.entryDao.GetUrshData(entity);
        }

        /// <summary>
        /// 指定された伝票番号で代納データを取得する
        /// </summary>
        /// <param name="denpyouNum"></param>
        /// <param name="flg">1:受入、2:出荷</param>
        /// <returns></returns>
        internal T_UR_SH_ENTRY GetDainouEntry(SqlInt64 denpyouNum, int flg)
        {
            DainoNyuryukuDTO entity = new DainoNyuryukuDTO();
            entity.UR_SH_NUMBER = denpyouNum;
            if (flg == 1)
            {
                entity.DENPYOU_KBN_CD = 2;
            }
            else if (flg == 2)
            {
                entity.DENPYOU_KBN_CD = 1;
            }

            return this.entryDao.GetDainouData(entity);
        }

        /// <summary>
        /// キーとして代納明細データを取得する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal List<T_UR_SH_DETAIL> GetDainouDetail(T_UR_SH_ENTRY entity)
        {
            return this.detailDao.GetDainouDetail(entity.SYSTEM_ID, entity.SEQ);
        }

        /// <summary>
        /// 代納Entry 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int InsertDainouEntry(T_UR_SH_ENTRY entity)
        {
            return this.entryDao.Insert(entity);
        }

        /// <summary>
        /// 代納Entry 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int UpdateDainouEntry(T_UR_SH_ENTRY entity)
        {
            return this.entryDao.Update(entity);
        }

        /// <summary>
        /// 代納明細 登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal int InsertDainouDetail(T_UR_SH_DETAIL entity)
        {
            return this.detailDao.Insert(entity);
        }

        /// <summary>
        /// 請求明細取得(出荷入力用)
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public DataTable GetSeikyuMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd)
        {
            T_SEIKYUU_DETAIL keyEntity = new T_SEIKYUU_DETAIL();
            // 伝種区分：代納
            keyEntity.DENPYOU_SHURUI_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
            keyEntity.DENPYOU_SYSTEM_ID = systemId;
            keyEntity.DENPYOU_SEQ = seq;
            if (0 <= detailSystemId)
            {
                keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
            }
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

            return this.seikyuuDetail.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 清算明細取得(受入入力用)
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public DataTable GetSeisanMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd)
        {
            T_SEISAN_DETAIL keyEntity = new T_SEISAN_DETAIL();
            // 伝種区分：受入
            keyEntity.DENPYOU_SHURUI_CD = (SqlInt16)DENSHU_KBN.URIAGE_SHIHARAI.GetHashCode();
            keyEntity.DENPYOU_SYSTEM_ID = systemId;
            keyEntity.DENPYOU_SEQ = seq;
            if (0 <= detailSystemId)
            {
                keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
            }
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

            return this.seisanDetail.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 消費税率を取得
        /// DELETE_FLGとTEKIYOU_BEGIN, TEKIYOU_ENDの絞込みはしない
        /// </summary>
        /// <returns></returns>
        public M_SHOUHIZEI[] GetAllShouhizeiRate()
        {
            return this.shouhizeiDao.GetAllData();
        }

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="tekiyouDate">適用日付</param>
        /// <returns>消費税率</returns>
        public M_SHOUHIZEI GetShouhizeiRate(DateTime tekiyouDate)
        {
            M_SHOUHIZEI returnEntity = null;

            if (tekiyouDate == null)
            {
                return returnEntity;
            }

            // SQL文作成(冗長にならないためsqlファイルで管理しない)
            DataTable dt = new DataTable();
            string selectStr = "SELECT * FROM M_SHOUHIZEI";
            string whereStr = " WHERE DELETE_FLG = 0";

            StringBuilder sb = new StringBuilder();
            sb.Append(" AND");
            sb.Append(" (");
            sb.Append("  (");
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)tekiyouDate + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + (DateTime)tekiyouDate + "', 111), 120) <= ISNULL(TEKIYOU_END,'9999/12/31')");
            sb.Append("  )");
            sb.Append(" )");

            whereStr = whereStr + sb.ToString();

            dt = this.GetGyoushaDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                return null;
            }

            var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

            var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
            returnEntity = shouhizeis[0];

            return returnEntity;
        }
        #endregion
    }
}