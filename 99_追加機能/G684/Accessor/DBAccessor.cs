using System.Data.SqlTypes;
using System.Linq;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DAO;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.DTO;
using System.Collections.Generic;
using System;
using System.Data;
using Shougun.Function.ShougunCSCommon.Const;
using System.Text;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// </summary>
    /// <remarks>
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </remarks>
    public class DBAccessor
    {
        #region フィールド

        /// <summary>
        ///
        /// </summary>
        private Common.BusinessCommon.DBAccessor commAccessor;

        /// <summary>
        /// 受入入力のDao
        /// </summary>
        private T_UKEIRE_ENTRYDao ukeireEntryDao;

        /// <summary>
        /// 受入明細のDao
        /// </summary>
        private T_UKEIRE_DETAILDao ukeireDetailDao;

        /// <summary>
        /// 出荷入力のDao
        /// </summary>
        private T_SHUKKA_ENTRYDao shukkaEntryDao;

        /// <summary>
        /// 出荷明細のDao
        /// </summary>
        private T_SHUKKA_DETAILDao shukkaDetailDao;

        /// <summary>
        /// 売上支払入力のDao
        /// </summary>
        private T_UR_SH_ENTRYDao urshEntryDao;

        /// <summary>
        /// 売上支払明細のDao
        /// </summary>
        private T_UR_SH_DETAILDao urshDetailDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SEIKYUUDao
        /// </summary>
        private IM_TORIHIKISAKI_SEIKYUUDao torihikisakiSeikyuuDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SHIHARAIDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao torihikisakiShiharaiDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_HINMEIDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// IM_SHASHUDao
        /// </summary>
        private IM_SHASHUDao shashuDao;

        /// <summary>
        /// IM_SHARYOUDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// 車輌休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workclosedsharyouDao;

        /// <summary>
        /// 運転者休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workcloseduntenshaDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workclosedhannyuusakiDao;

        /// <summary>
        /// IM_MANIFEST_SHURUIDao
        /// </summary>
        private IM_MANIFEST_SHURUIDao manishuruiDao;

        /// <summary>
        /// IM_MANIFEST_TEHAIDao
        /// </summary>
        private IM_MANIFEST_TEHAIDao manitehaiDao;

        /// <summary>
        /// IM_KEITAI_KBNDao
        /// </summary>
        private IM_KEITAI_KBNDao keitaiDao;

        /// <summary>
        /// IT_SEIKYUU_DETAILDao
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao seikyuuDetail;

        /// <summary>
        /// IT_SEISAN_DETAILDao
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao seisanDetail;

        /// <summary>
        /// 在庫情報DAO
        /// </summary>
        private TZUDClass tzudDao;

        /// <summary>
        /// 在庫情報DAO
        /// </summary>
        private TZSDClass tzsdDao;

        /// <summary>
        /// 在庫品名振分
        /// </summary>
        private TZHHClass tzhhDao;

        /// <summary>
        /// コンテナ情報DAO
        /// </summary>
        private TCRClass tcrDao;

        /// <summary>
        /// コンテナ稼動予定情報DAO
        /// </summary>
        private TCREClass tcreDao;

        /// <summary>
        /// コンテナマスタDAO
        /// </summary>
        private MCClass mcDao;

        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        private Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao numberDayDao;

        /// <summary>IT_NYUUKIN_SUM_ENTRYDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_ENTRYDao nyuukinSumEntryDao;

        /// <summary>IT_NYUUKIN_SUM_DETAILDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_DETAILDao nyuukinSumDetailDao;

        /// <summary>IT_NYUUKIN_ENTRYDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_ENTRYDao nyuukinEntryDao;

        /// <summary>IT_NYUUKIN_DETAILDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_DETAILDao nyuukinDetailDao;

        /// <summary>IT_SHUKKIN_ENTRYDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_ENTRYDao shukkinEntryDao;

        /// <summary>IT_SHUKKIN_DETAILDao</summary>
        private Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_DETAILDao shukkinDetailDao;

        private r_framework.Dao.GET_SYSDATEDao dateDao;

        /// <summary>
        /// 検収明細DAO
        /// </summary>
        private TKDClass tkdDao;

        public List<M_KYOTEN> kyotenList = new List<M_KYOTEN>();
        public List<M_MANIFEST_SHURUI> maniShuruiList = new List<M_MANIFEST_SHURUI>();
        public List<M_MANIFEST_TEHAI> maniTehaiList = new List<M_MANIFEST_TEHAI>();
        public List<M_KEITAI_KBN> keitaiList = new List<M_KEITAI_KBN>();
        #endregion

        #region 初期化

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.ukeireEntryDao = DaoInitUtility.GetComponent<T_UKEIRE_ENTRYDao>();
            this.ukeireDetailDao = DaoInitUtility.GetComponent<T_UKEIRE_DETAILDao>();
            this.shukkaEntryDao = DaoInitUtility.GetComponent<T_SHUKKA_ENTRYDao>();
            this.shukkaDetailDao = DaoInitUtility.GetComponent<T_SHUKKA_DETAILDao>();
            this.urshEntryDao = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();
            this.urshDetailDao = DaoInitUtility.GetComponent<T_UR_SH_DETAILDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.workclosedsharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();
            this.workcloseduntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();
            this.workclosedhannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();
            this.manishuruiDao = DaoInitUtility.GetComponent<IM_MANIFEST_SHURUIDao>();
            this.manitehaiDao = DaoInitUtility.GetComponent<IM_MANIFEST_TEHAIDao>();
            this.keitaiDao = DaoInitUtility.GetComponent<IM_KEITAI_KBNDao>();
            this.seikyuuDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao>();
            this.seisanDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao>();
            this.tzudDao = DaoInitUtility.GetComponent<TZUDClass>();
            this.tzsdDao = DaoInitUtility.GetComponent<TZSDClass>();
            this.tzhhDao = DaoInitUtility.GetComponent<TZHHClass>();
            this.tcrDao = DaoInitUtility.GetComponent<TCRClass>();
            this.tcreDao = DaoInitUtility.GetComponent<TCREClass>();
            this.mcDao = DaoInitUtility.GetComponent<MCClass>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.nyuukinEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_ENTRYDao>();
            this.nyuukinDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_DETAILDao>();
            this.shukkinEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_ENTRYDao>();
            this.shukkinDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_DETAILDao>();
            this.nyuukinSumEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_ENTRYDao>();
            this.nyuukinSumDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_DETAILDao>();
            this.dateDao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();
            this.tkdDao = DaoInitUtility.GetComponent<TKDClass>();

            this.commAccessor = new Shougun.Core.Common.BusinessCommon.DBAccessor();

            this.kyotenList = this.kyotenDao.GetAllData().ToList();
            this.maniShuruiList = this.manishuruiDao.GetAllData().ToList();
            this.maniTehaiList = this.manitehaiDao.GetAllData().ToList();
            this.keitaiList = this.keitaiDao.GetAllData().ToList();
        }

        #endregion

        #region DB関連メソッド

        internal T_UKEIRE_ENTRY[] SearchUkeireEntryData(SearchDto data)
        {
            return this.ukeireEntryDao.GetUkeireEntryData(data);
        }

        internal T_UKEIRE_DETAIL[] SearchUkeireDetailData(T_UKEIRE_ENTRY data)
        {
            return this.ukeireDetailDao.GetUkeireDetailData(data);
        }

        internal T_SHUKKA_ENTRY[] SearchShukkaEntryData(SearchDto data)
        {
            return this.shukkaEntryDao.GetShukkaEntryData(data);
        }

        internal T_SHUKKA_DETAIL[] SearchShukkaDetailData(T_SHUKKA_ENTRY data)
        {
            return this.shukkaDetailDao.GetShukkaDetailData(data);
        }

        internal T_UR_SH_ENTRY[] SearchUrshEntryData(SearchDto data)
        {
            return this.urshEntryDao.GetUrshEntryData(data);
        }

        internal T_UR_SH_DETAIL[] SearchUrshDetailData(T_UR_SH_ENTRY data)
        {
            return this.urshDetailDao.GetUrshDetailData(data);
        }

        internal int InsertUkeireEntryData(T_UKEIRE_ENTRY entryEntity)
        {
            return this.ukeireEntryDao.Insert(entryEntity);
        }

        internal int InsertUkeireDetailData(T_UKEIRE_DETAIL detailEntity)
        {
            return this.ukeireDetailDao.Insert(detailEntity);
        }

        internal int UpdateUkeireEntryData(T_UKEIRE_ENTRY entryEntity)
        {
            return this.ukeireEntryDao.Update(entryEntity);
        }

        internal int InsertShukkaEntryData(T_SHUKKA_ENTRY entryEntity)
        {
            return this.shukkaEntryDao.Insert(entryEntity);
        }

        internal int InsertShukkaDetailData(T_SHUKKA_DETAIL detailEntity)
        {
            return this.shukkaDetailDao.Insert(detailEntity);
        }

        internal int UpdateShukkaEntryData(T_SHUKKA_ENTRY entryEntity)
        {
            return this.shukkaEntryDao.Update(entryEntity);
        }

        internal int InsertUrshEntryData(T_UR_SH_ENTRY entryEntity)
        {
            return this.urshEntryDao.Insert(entryEntity);
        }

        internal int InsertUrshDetailData(T_UR_SH_DETAIL detailEntity)
        {
            return this.urshDetailDao.Insert(detailEntity);
        }

        internal int UpdateUrshEntryData(T_UR_SH_ENTRY entryEntity)
        {
            return this.urshEntryDao.Update(entryEntity);
        }

        /// <summary>
        /// 品名情報取得
        /// </summary>
        /// <param name="hinmeiCd">品名CD</param>
        /// <returns></returns>
        /// <remarks>
        /// 適用開始・終了日、削除フラグについては有効なものだけを検索します
        /// </remarks>
        internal M_HINMEI[] GetAllValidHinmeiData(string hinmeiCd = null)
        {
            var cond = new M_HINMEI()
            {
                HINMEI_CD = hinmeiCd,
            };
            return this.hinmeiDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 品名取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_HINMEI GetHinmeiDataByCode(string hinmeiCd)
        {
            if (string.IsNullOrWhiteSpace(hinmeiCd))
                return null;
            else
                return this.hinmeiDao.GetDataByCd(hinmeiCd);
        }

        /// <summary>
        /// 単位情報取得
        /// </summary>
        /// <param name="hinmeiCd">品名CD</param>
        /// <returns></returns>
        /// <remarks>
        /// 適用開始・終了日、削除フラグについては有効なものだけを検索します
        /// </remarks>
        internal M_UNIT[] GetAllValidUnitData(short unitCd)
        {
            var cond = new M_UNIT()
            {
                UNIT_CD = unitCd,
            };
            return this.unitDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 単位取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_UNIT GetUnitDataByCode(short unitCd)
        {
            return this.unitDao.GetDataByCd(unitCd);
        }

        /// <summary>
        /// 社員取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_SHAIN GetShainDataByCode(string shainCd)
        {
            return this.shainDao.GetDataByCd(shainCd);
        }

        /// <summary>
        /// 車種取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_SHASHU GetShashuDataByCode(string shashuCd)
        {
            return this.shashuDao.GetDataByCd(shashuCd);
        }

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_SHARYOU GetSharyouDataByCode(string upnGyoushaCd, string sharyouCd)
        {
            var cond = new M_SHARYOU()
            {
                GYOUSHA_CD = upnGyoushaCd,
                SHARYOU_CD = sharyouCd,
            };
            return this.sharyouDao.GetDataByCd(cond);
        }

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_SHARYOU[] GetAllValidSharyouData(string upnGyoushaCd, string sharyouCd)
        {
            var cond = new M_SHARYOU();
            if (!string.IsNullOrEmpty(upnGyoushaCd))
            {
                cond.GYOUSHA_CD = upnGyoushaCd;
            }
            cond.SHARYOU_CD = sharyouCd;
            return this.sharyouDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 車輌休動取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_WORK_CLOSED_SHARYOU[] GetAllValidSharyouClosedData(string inputUnpanGyoushaCd, string inputSharyouCd, string inputSagyouDate)
        {
            var cond = new M_WORK_CLOSED_SHARYOU();
            //運搬業者CD
            cond.GYOUSHA_CD = inputUnpanGyoushaCd;
            //車輌CD取得
            cond.SHARYOU_CD = inputSharyouCd;
            //伝票日付取得
            cond.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            return this.workclosedsharyouDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 運転者休動取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_WORK_CLOSED_UNTENSHA[] GetAllValidUntenshaClosedData(string inputUntenshaCd, string inputSagyouDate)
        {
            var cond = new M_WORK_CLOSED_UNTENSHA();
            //運転者CD取得
            cond.SHAIN_CD = inputUntenshaCd;
            //作業日取得
            cond.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            return this.workcloseduntenshaDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 運転者休動取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_WORK_CLOSED_HANNYUUSAKI[] GetAllValidHannyuuClosedData(string inputNioroshiGyoushaCd, string inputNioroshiGenbaCd, string inputSagyouDate)
        {
            var cond = new M_WORK_CLOSED_HANNYUUSAKI();
            //荷降業者CD取得
            cond.GYOUSHA_CD = inputNioroshiGyoushaCd;
            //荷降現場CD取得
            cond.GENBA_CD = inputNioroshiGenbaCd;
            //作業日取得
            cond.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);
            return this.workclosedhannyuusakiDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 拠点を取得
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <param name="isNotNeedDeleteFlg">ISNOT_NEED_DELETE_FLG</param>
        /// <returns></returns>
        /// <remarks>
        /// 適用開始・終了日、削除フラグを考慮せず指定されたCDのデータを取得する
        /// </remarks>
        internal M_KYOTEN GetKyotenDataByCode(short kyotenCd, bool isNotNeedDeleteFlg = true)
        {
            if (kyotenCd < 0)
                return null;

            var cond = new M_KYOTEN()
            {
                KYOTEN_CD = kyotenCd,
                ISNOT_NEED_DELETE_FLG = isNotNeedDeleteFlg,
            };
            var rst = this.kyotenDao.GetAllValidData(cond);
            // 削除フラグがFalseのデータが優先で、無いの場合は結果の一件目を戻す。
            return rst.FirstOrDefault(x => x.DELETE_FLG.IsFalse) ?? rst.FirstOrDefault();
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        internal M_TORIHIKISAKI GetTorihikisakiDataByCode(string torihikisakiCd)
        {
            if (string.IsNullOrWhiteSpace(torihikisakiCd))
                return null;

            return this.torihikisakiDao.GetDataByCd(torihikisakiCd);
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        internal M_TORIHIKISAKI GetTorihikisakiValidDataByCode(string torihikisakiCd, string teikiyouDate)
        {
            if (string.IsNullOrWhiteSpace(torihikisakiCd))
                return null;

            M_TORIHIKISAKI result = this.torihikisakiDao.GetDataByCd(torihikisakiCd);
            if (result != null  && result.DELETE_FLG == false && !string.IsNullOrEmpty(teikiyouDate))
            {
                DateTime date = Convert.ToDateTime(teikiyouDate);
                if (result.TEKIYOU_BEGIN.Value > date || (!result.TEKIYOU_END.IsNull && result.TEKIYOU_END.Value < date))
                {
                    return null;
                }
            }
            else if (result != null && result.DELETE_FLG != false)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// 取引先請求情報取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        internal M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuDataByCode(string torihikisakiCd)
        {
            if (string.IsNullOrWhiteSpace(torihikisakiCd))
                return null;

            return this.torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        internal M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharaiDataByCode(string torihikisakiCd)
        {
            if (string.IsNullOrWhiteSpace(torihikisakiCd))
                return null;

            return this.torihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_GYOUSHA GetGyoushaDataByCode(string gyoushaCd)
        {
            if (string.IsNullOrWhiteSpace(gyoushaCd))
                return null;

            return this.gyoushaDao.GetDataByCd(gyoushaCd);
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        internal M_GYOUSHA GetGyoushaValidDataByCode(string gyoushaCd, string teikiyouDate)
        {
            if (string.IsNullOrWhiteSpace(gyoushaCd))
                return null;

            M_GYOUSHA result = this.gyoushaDao.GetDataByCd(gyoushaCd);
            if (result != null && result.DELETE_FLG == false && !string.IsNullOrEmpty(teikiyouDate))
            {
                DateTime date = Convert.ToDateTime(teikiyouDate);
                if (result.TEKIYOU_BEGIN.Value > date || (!result.TEKIYOU_END.IsNull && result.TEKIYOU_END.Value < date))
                {
                    return null;
                }
            }
            else if (result != null && result.DELETE_FLG != false)
            {
                return null;
            }

            return this.gyoushaDao.GetDataByCd(gyoushaCd);
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_GENBA[] GetAllValidGenbaDataByCode(string genbaCd)
        {
            if (string.IsNullOrWhiteSpace(genbaCd))
                return null;

            var cond = new M_GENBA()
            {
                GENBA_CD = genbaCd,
            };
            return this.genbaDao.GetAllValidData(cond);
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_GENBA GetGenbaDataByCode(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrWhiteSpace(gyoushaCd) ||
                string.IsNullOrWhiteSpace(genbaCd))
                return null;

            var cond = new M_GENBA()
            {
                GYOUSHA_CD = gyoushaCd,
                GENBA_CD = genbaCd,
            };
            return this.genbaDao.GetDataByCd(cond);
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_GENBA GetGenbaValidDataByCode(string gyoushaCd, string genbaCd, string teikiyouDate)
        {
            if (string.IsNullOrWhiteSpace(gyoushaCd) ||
                string.IsNullOrWhiteSpace(genbaCd))
                return null;

            var cond = new M_GENBA()
            {
                GYOUSHA_CD = gyoushaCd,
                GENBA_CD = genbaCd,
            };
            M_GENBA result = this.genbaDao.GetDataByCd(cond);
            if (result != null && result.DELETE_FLG == false && !string.IsNullOrEmpty(teikiyouDate))
            {
                DateTime date = Convert.ToDateTime(teikiyouDate);
                if (result.TEKIYOU_BEGIN.Value > date || (!result.TEKIYOU_END.IsNull && result.TEKIYOU_END.Value < date))
                {
                    return null;
                }
            }
            else if (result != null && result.DELETE_FLG != false)
            {
                return null;
            }
            return this.genbaDao.GetDataByCd(cond);
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_KEITAI_KBN GetKeitaiDataByCode(string cd)
        {
            if (string.IsNullOrWhiteSpace(cd))
                return null;

            return this.keitaiDao.GetDataByCd(cd);
        }

        /// <summary>
        /// マニ種類取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_MANIFEST_SHURUI GetManiShuruiDataByCode(string cd)
        {
            if (string.IsNullOrWhiteSpace(cd))
                return null;

            return this.manishuruiDao.GetDataByCd(cd);
        }

        /// <summary>
        /// マニ手配取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        internal M_MANIFEST_TEHAI GetManiTehaiDataByCode(string cd)
        {
            if (string.IsNullOrWhiteSpace(cd))
                return null;

            return this.manitehaiDao.GetDataByCd(cd);
        }

        /// <summary>
        /// 拠点名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetKyotenNameFast(string cd)
        {
            if (string.IsNullOrEmpty(cd))
            {
                return "";
            }

            M_KYOTEN[] kyoten = this.kyotenList.Where(x => x.KYOTEN_CD.Value == Convert.ToInt16(cd)).ToArray();

            if (kyoten == null || kyoten.Length == 0)
            {
                return "";
            }
            return kyoten[0].KYOTEN_NAME_RYAKU;
        }

        /// <summary>
        /// マニ種類名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetManiShuruiNameFast(string cd)
        {
            if (string.IsNullOrEmpty(cd))
            {
                return "";
            }

            M_MANIFEST_SHURUI[] shurui = this.maniShuruiList.Where(x => x.MANIFEST_SHURUI_CD.Value == Convert.ToInt16(cd)).ToArray();

            if (shurui == null || shurui.Length == 0)
            {
                return "";
            }
            return shurui[0].MANIFEST_SHURUI_NAME_RYAKU;
        }

        /// <summary>
        /// マニ手配名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetManiTehaiNameFast(string cd)
        {
            if (string.IsNullOrEmpty(cd))
            {
                return "";
            }

            M_MANIFEST_TEHAI[] tehai = this.maniTehaiList.Where(x => x.MANIFEST_TEHAI_CD.Value == Convert.ToInt16(cd)).ToArray();

            if (tehai == null || tehai.Length == 0)
            {
                return "";
            }
            return tehai[0].MANIFEST_TEHAI_NAME_RYAKU;
        }

        /// <summary>
        /// 形態名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetKeitaiNameFast(string cd)
        {
            if (string.IsNullOrEmpty(cd))
            {
                return "";
            }

            M_KEITAI_KBN[] keitai = this.keitaiList.Where(x => x.KEITAI_KBN_CD.Value == Convert.ToInt16(cd)).ToArray();

            if (keitai == null || keitai.Length == 0)
            {
                return "";
            }
            return keitai[0].KEITAI_KBN_NAME_RYAKU;
        }

        /// <summary>
        /// 諸口チェック
        /// </summary>
        /// <returns></returns>
        internal bool IsShokuchi(M_TORIHIKISAKI entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.TORIHIKISAKI_CD))
            {
                return false;
            }
            M_TORIHIKISAKI result = this.torihikisakiDao.GetDataByCd(entity.TORIHIKISAKI_CD);
            if (result == null)
            {
                return false;
            }
            return result.SHOKUCHI_KBN.IsTrue;
        }

        /// <summary>
        /// 諸口チェック
        /// </summary>
        /// <returns></returns>
        internal bool IsShokuchi(M_GYOUSHA entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.GYOUSHA_CD))
            {
                return false;
            }
            M_GYOUSHA result = this.gyoushaDao.GetDataByCd(entity.GYOUSHA_CD);
            if (result == null)
            {
                return false;
            }
            return result.SHOKUCHI_KBN.IsTrue;
        }

        /// <summary>
        /// 諸口チェック
        /// </summary>
        /// <returns></returns>
        internal bool IsShokuchi(M_GENBA entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.GYOUSHA_CD) || string.IsNullOrEmpty(entity.GENBA_CD))
            {
                return false;
            }
            M_GENBA result = this.genbaDao.GetDataByCd(entity);
            if (result == null)
            {
                return false;
            }
            return result.SHOKUCHI_KBN.IsTrue;
        }

        /// <summary>
        /// 諸口チェック
        /// </summary>
        /// <returns></returns>
        internal bool IsShokuchi(M_SHARYOU entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.SHARYOU_CD))
            {
                return false;
            }
            M_SHARYOU result = this.sharyouDao.GetDataByCd(entity);
            return result == null;
        }

        /// <summary>
        /// 請求明細取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denshuKbn">伝種区分CD</param>
        /// <returns></returns>
        public DataTable GetSeikyuMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd, short denshuKbn)
        {
            T_SEIKYUU_DETAIL keyEntity = new T_SEIKYUU_DETAIL();
            keyEntity.DENPYOU_SHURUI_CD = denshuKbn;
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
        /// 清算明細取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denshuKbn">伝種区分CD</param>
        /// <returns></returns>
        public DataTable GetSeisanMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd, short denshuKbn)
        {
            T_SEISAN_DETAIL keyEntity = new T_SEISAN_DETAIL();
            keyEntity.DENPYOU_SHURUI_CD = denshuKbn;
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
        /// 締処理状況（在庫）取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        internal T_ZAIKO_UKEIRE_DETAIL GetZaikoUkeireData(long systemId, int seq)
        {
            T_ZAIKO_UKEIRE_DETAIL keyEntity = new T_ZAIKO_UKEIRE_DETAIL();

            // 伝種区分：受入
            keyEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
            keyEntity.SYSTEM_ID = systemId;
            keyEntity.SEQ = seq;

            return this.tzudDao.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 在庫情報取得
        /// </summary>
        /// <param name="data"></param>
        internal List<T_ZAIKO_UKEIRE_DETAIL> GetZaikoUkeireDetails(T_ZAIKO_UKEIRE_DETAIL data)
        {
            //キーチェック
            if (data == null
                || data.SYSTEM_ID.Equals(SqlInt64.Null)
                || data.DETAIL_SYSTEM_ID.Equals(SqlInt64.Null)
                || data.SEQ.Equals(SqlInt32.Null))
            {
                return null;
            }

            return this.tzudDao.GetZaikoInfo(data);
        }

        /// <summary>
        /// 締処理状況（在庫）取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        internal T_ZAIKO_SHUKKA_DETAIL GetZaikoShukkaData(long systemId, int seq)
        {
            T_ZAIKO_SHUKKA_DETAIL keyEntity = new T_ZAIKO_SHUKKA_DETAIL();

            // 伝種区分：受入
            keyEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            keyEntity.SYSTEM_ID = systemId;
            keyEntity.SEQ = seq;

            return this.tzsdDao.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 在庫情報取得
        /// </summary>
        /// <param name="data"></param>
        internal List<T_ZAIKO_SHUKKA_DETAIL> GetZaikoShukkaDetails(T_ZAIKO_SHUKKA_DETAIL data)
        {
            //キーチェック
            if (data == null
                || data.SYSTEM_ID.Equals(SqlInt64.Null)
                || data.DETAIL_SYSTEM_ID.Equals(SqlInt64.Null)
                || data.SEQ.Equals(SqlInt32.Null))
            {
                return null;
            }

            return this.tzsdDao.GetZaikoInfo(data);
        }

        internal List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoHinmeiHuriwakes(T_ZAIKO_HINMEI_HURIWAKE data)
        {
            //キーチェック
            if (data == null
                || data.DENSHU_KBN_CD.IsNull
                || data.SYSTEM_ID.IsNull
                || data.DETAIL_SYSTEM_ID.IsNull
                || data.SEQ.IsNull)
            {
                return null;
            }

            return this.tzhhDao.GetZaikoInfo(data);
        }

        /// <summary>
        /// コンテナ情報取得
        /// </summary>
        /// <param name="sysId"></param>
        internal T_CONTENA_RESULT[] GetContena(string sysId, int denshuKbn)
        {
            //キーチェック
            if (sysId == null || denshuKbn == null)
            {
                return null;
            }

            return this.tcrDao.GetContena(sysId, denshuKbn);
        }

        /// <summary>
        /// コンテナ稼動予定情報取得
        /// </summary>
        /// <param name="sysId"></param>
        /// <param name="SEQ"></param>
        /// <returns></returns>
        internal T_CONTENA_RESERVE[] GetContenaReserve(string sysId, string SEQ)
        {
            //キーチェック
            if (sysId == null || SEQ == null)
            {
                return null;
            }

            return this.tcreDao.GetContenaReserve(sysId, SEQ);
        }

        /// <summary>
        /// 年連番のデータを取得
        /// </summary>
        /// <param name="numberedYear">年度</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_YEAR[] GetNumberYear(SqlInt32 numberedYear, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            S_NUMBER_YEAR entity = new S_NUMBER_YEAR();
            entity.NUMBERED_YEAR = numberedYear;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return this.numberYearDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 年連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Insert(entity);
        }

        /// <summary>
        /// 年連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberYear(S_NUMBER_YEAR entity)
        {
            return this.numberYearDao.Update(entity);
        }

        /// <summary>
        /// 日連番のデータを取得
        /// </summary>
        /// <param name="numberedDay">日付</param>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_DAY[] GetNumberDay(DateTime numberedDay, SqlInt16 denshuKbnCd, SqlInt16 kyotenCd)
        {
            S_NUMBER_DAY entity = new S_NUMBER_DAY();
            entity.NUMBERED_DAY = numberedDay.Date;
            entity.DENSHU_KBN_CD = denshuKbnCd;
            entity.KYOTEN_CD = kyotenCd;
            return this.numberDayDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 日連番を登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Insert(entity);
        }

        /// <summary>
        /// 日連番を更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateNumberDay(S_NUMBER_DAY entity)
        {
            return this.numberDayDao.Update(entity);
        }

        /// <summary>
        /// 消費税率を取得
        /// </summary>
        /// <param name="denpyouHiduke">適用期間条件</param>
        /// <returns></returns>
        public M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        {
            M_SHOUHIZEI returnEntity = null;

            if (denpyouHiduke == null)
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
            sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= ISNULL(TEKIYOU_END,'9999/12/31')");
            sb.Append("  )");
            sb.Append(" )");

            whereStr = whereStr + sb.ToString();

            dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

            if (dt == null || dt.Rows.Count < 1)
            {
                return returnEntity;
            }

            var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

            var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
            returnEntity = shouhizeis[0];

            return returnEntity;
        }

        /// <summary>
        /// 入金入力追加
        /// </summary>
        /// <param name="targetEntity"></param>
        public void InsertNyuukinSumEntry(T_NYUUKIN_SUM_ENTRY targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.SYSTEM_ID.IsNull
                || targetEntity.SEQ.IsNull)
            {
                return;
            }

            this.nyuukinSumEntryDao.Insert(targetEntity);

        }

        /// <summary>
        /// 入金明細追加
        /// </summary>
        /// <param name="targetEntitys"></param>
        public void InsertNyuukinSumDetails(List<T_NYUUKIN_SUM_DETAIL> targetEntitys)
        {
            // 存在チェック
            if (targetEntitys == null
                || targetEntitys.Count < 1)
            {
                return;
            }

            foreach (var targetEntity in targetEntitys)
            {
                // キーチェック
                if (targetEntity == null
                    || targetEntity.SYSTEM_ID.IsNull
                    || targetEntity.SEQ.IsNull
                    || targetEntity.DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                this.nyuukinSumDetailDao.Insert(targetEntity);
            }
        }

        /// <summary>
        /// 入金入力追加
        /// </summary>
        /// <param name="targetEntity"></param>
        public void InsertNyuukinEntry(T_NYUUKIN_ENTRY targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.SYSTEM_ID.IsNull
                || targetEntity.SEQ.IsNull)
            {
                return;
            }

            this.nyuukinEntryDao.Insert(targetEntity);

        }

        /// <summary>
        /// 入金明細追加
        /// </summary>
        /// <param name="targetEntitys"></param>
        public void InsertNyuukinDetails(List<T_NYUUKIN_DETAIL> targetEntitys)
        {
            // 存在チェック
            if (targetEntitys == null
                || targetEntitys.Count < 1)
            {
                return;
            }

            foreach (var targetEntity in targetEntitys)
            {
                // キーチェック
                if (targetEntity == null
                    || targetEntity.SYSTEM_ID.IsNull
                    || targetEntity.SEQ.IsNull
                    || targetEntity.DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                this.nyuukinDetailDao.Insert(targetEntity);
            }
        }

        /// <summary>
        /// 出金入力追加
        /// </summary>
        /// <param name="targetEntity"></param>
        public void InsertShukkinEntry(T_SHUKKIN_ENTRY targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.SYSTEM_ID.IsNull
                || targetEntity.SEQ.IsNull)
            {
                return;
            }

            this.shukkinEntryDao.Insert(targetEntity);

        }

        /// <summary>
        /// 出金明細追加
        /// </summary>
        /// <param name="targetEntitys"></param>
        public void InsertShukkinDetails(List<T_SHUKKIN_DETAIL> targetEntitys)
        {
            // 存在チェック
            if (targetEntitys == null
                || targetEntitys.Count < 1)
            {
                return;
            }

            foreach (var targetEntity in targetEntitys)
            {
                // キーチェック
                if (targetEntity == null
                    || targetEntity.SYSTEM_ID.IsNull
                    || targetEntity.SEQ.IsNull
                    || targetEntity.DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                this.shukkinDetailDao.Insert(targetEntity);
            }
        }

        /// <summary>
        /// コンテナ稼動予定の更新
        /// CALC_DAISUU_FLGをFALSEに設定しUpdate
        /// </summary>
        /// <param name="targetEntitys">更新対象</param>
        internal void UpdateContenaReserve(List<T_CONTENA_RESERVE> targetEntitys)
        {
            foreach (T_CONTENA_RESERVE entity in targetEntitys)
            {
                // キーチェック
                if (entity == null
                    || entity.SYSTEM_ID.IsNull
                    || entity.SEQ.IsNull)
                {
                    continue;
                }

                entity.CALC_DAISUU_FLG = SqlBoolean.False;
                this.tcreDao.Update(entity);
            }
        }

        /// <summary>
        /// コンテナ稼働実績を登録
        /// </summary>
        /// <param name="targetEntity"></param>
        internal void InsertContenaResult(List<T_CONTENA_RESULT> targetEntity)
        {
            foreach (T_CONTENA_RESULT entity in targetEntity)
            {
                // キーチェック
                if (entity == null
                    || entity.DENSHU_KBN_CD.IsNull
                    || entity.SYSTEM_ID.IsNull
                    || entity.SEQ.IsNull
                    || entity.CONTENA_SET_KBN.IsNull
                    || entity.CONTENA_SHURUI_CD == null
                    || entity.CONTENA_CD == null)
                {
                    continue;
                }

                this.tcrDao.Insert(entity);
            }
        }

        /// <summary>
        /// コンテナ稼働実績を更新
        /// </summary>
        /// <param name="targetEntity"></param>
        internal void UpdateContenaResult(List<T_CONTENA_RESULT> targetEntity)
        {
            foreach (T_CONTENA_RESULT entity in targetEntity)
            {
                // キーチェック
                if (entity == null
                    || entity.DENSHU_KBN_CD.IsNull
                    || entity.SYSTEM_ID.IsNull
                    || entity.SEQ.IsNull
                    || entity.CONTENA_SET_KBN.IsNull
                    || entity.CONTENA_SHURUI_CD == null
                    || entity.CONTENA_CD == null)
                {
                    continue;
                }

                this.tcrDao.Update(entity);
            }
        }
        /// <summary>
        /// 在庫明細_受入を登録
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertZaikoUkeireDetails(Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>> targetEntityLists)
        {
            //// 値を配列に変換する
            foreach (List<T_ZAIKO_UKEIRE_DETAIL> entityList in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_UKEIRE_DETAIL entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull
                        || entity.ROW_NO.IsNull)
                    {
                        return;
                    }

                    this.tzudDao.Insert(entity);
                }
            }
        }

        /// <summary>
        /// 在庫明細_受入を更新
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void UpdateZaikoUkeireDetails(Dictionary<string, List<T_ZAIKO_UKEIRE_DETAIL>> targetEntityLists)
        {
            foreach (List<T_ZAIKO_UKEIRE_DETAIL> entityList in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_UKEIRE_DETAIL entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull
                        || entity.ROW_NO.IsNull)
                    {
                        return;
                    }

                    this.tzudDao.Update(entity);
                }
            }
        }

        /// <summary>
        /// 在庫明細_出荷を更新
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertZaikoShukkaDetails(Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>> targetEntityLists)
        {
            foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull
                        || entity.ROW_NO.IsNull)
                    {
                        return;
                    }

                    this.tzsdDao.Insert(entity);
                }
            }
        }

        /// <summary>
        /// 在庫明細_出荷を登録
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void UpdateZaikoShukkaDetails(Dictionary<string, List<T_ZAIKO_SHUKKA_DETAIL>> targetEntityLists)
        {
            foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull
                        || entity.ROW_NO.IsNull)
                    {
                        return;
                    }

                    this.tzsdDao.Update(entity);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertZaikoHinmeiHuriwakes(Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>> targetEntityLists)
        {
            foreach (List<T_ZAIKO_HINMEI_HURIWAKE> entityList in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_HINMEI_HURIWAKE entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull)
                    {
                        return;
                    }

                    this.tzhhDao.Insert(entity);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void UpdateZaikoHinmeiHuriwakes(Dictionary<string, List<T_ZAIKO_HINMEI_HURIWAKE>> targetEntityLists)
        {
            foreach (List<T_ZAIKO_HINMEI_HURIWAKE> entityLst in targetEntityLists.Values)
            {
                foreach (T_ZAIKO_HINMEI_HURIWAKE entity in entityLst)
                {
                    // キーチェック
                    if (entity == null
                        || entity.DENSHU_KBN_CD.IsNull
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull)
                    {
                        return;
                    }

                    this.tzhhDao.Update(entity);
                }
            }
        }

        /// <summary>
        /// コンテナマスタを更新
        /// </summary>
        /// <param name="targetEntity"></param>
        internal void UpdateContenaMaster(List<M_CONTENA> targetEntity)
        {
            foreach (M_CONTENA entity in targetEntity)
            {
                // キーチェック
                if (entity == null
                    || entity.CONTENA_SHURUI_CD == null
                    || entity.CONTENA_CD == null)
                {
                    return;
                }

                this.mcDao.Update(entity);
            }
        }

        /// <summary>
        /// コンテナマスタ取得
        /// </summary>
        /// <param name="ContenaShuruiCd"></param>
        /// <param name="ContenaCd"></param>
        /// <returns></returns>
        internal M_CONTENA GetContenaMaster(string ContenaShuruiCd, string ContenaCd)
        {
            if (string.IsNullOrEmpty(ContenaShuruiCd) || string.IsNullOrEmpty(ContenaCd))
            {
                return null;
            }

            var contena = this.mcDao.GetContenaMasterEntity(ContenaShuruiCd, ContenaCd);
            if (contena == null)
            {
                return null;
            }

            return contena;
        }

        internal DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }

        #region コンテナチェック

        internal T_UKEIRE_ENTRY getUkeireDataByNumber(long UkeireNumber)
        {
            return this.ukeireEntryDao.GetUkeireDataByNumber(UkeireNumber);
        }

        internal T_UR_SH_ENTRY getUrshDataByNumber(long UrshNumber)
        {
            return this.urshEntryDao.GetUrshDataByNumber(UrshNumber);
        }

        internal T_CONTENA_RESULT[] GetContenaData(SqlInt64 SystemId, SqlInt16 DenshuKbn)
        {
            return this.tcrDao.GetContenaData(SystemId, DenshuKbn);
        }

        #endregion

        #region 検収明細
        internal List<T_KENSHU_DETAIL> GetKenshuDetails(T_KENSHU_DETAIL data)
        {
            //キーチェック
            if (data == null
                || data.SYSTEM_ID.IsNull
                || data.DETAIL_SYSTEM_ID.IsNull
                || data.SEQ.IsNull)
            {
                return null;
            }

            return this.tkdDao.GetKenshuInfo(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void UpdateKenshuDetail(Dictionary<string, List<T_KENSHU_DETAIL>> targetEntityLists)
        {
            foreach (List<T_KENSHU_DETAIL> entityLst in targetEntityLists.Values)
            {
                foreach (T_KENSHU_DETAIL entity in entityLst)
                {
                    // キーチェック
                    if (entity == null
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull)
                    {
                        return;
                    }

                    this.tkdDao.Update(entity);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertKenshuDetail(Dictionary<string, List<T_KENSHU_DETAIL>> targetEntityLists)
        {
            foreach (List<T_KENSHU_DETAIL> entityList in targetEntityLists.Values)
            {
                foreach (T_KENSHU_DETAIL entity in entityList)
                {
                    // キーチェック
                    if (entity == null
                        || entity.SYSTEM_ID.IsNull
                        || entity.SEQ.IsNull
                        || entity.DETAIL_SYSTEM_ID.IsNull)
                    {
                        return;
                    }

                    this.tkdDao.Insert(entity);
                }
            }
        } 
        #endregion

        #endregion
    }
}