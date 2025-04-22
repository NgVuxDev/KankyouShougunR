using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using GrapeCity.Win.MultiRow;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Scale.KeiryouNyuuryoku;
using Shougun.Core.Scale.KeiryouNyuuryoku.DAO;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Const;
using r_framework.Dao;

namespace Shougun.Scale.KeiryouNyuuryoku.Accessor
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
        r_framework.Dao.IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IT_KEIRYOU_ENTRYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_KEIRYOU_ENTRYDao keiryouEntryDao;

        /// <summary>
        /// IT_KEIRYOU_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_KEIRYOU_DETAILDao keiryouDetailDao;

        /// IT_UKEIRE_JISSEKI_ENTRYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_JISSEKI_ENTRYDao ukeireJissekiEntryDao;

        /// IT_UKEIRE_JISSEKI_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_JISSEKI_DETAILDao ukeireJissekiDetailDao;

        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao numberDayDao;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        r_framework.Dao.IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        r_framework.Dao.IM_GENBADao genbaDao;

        /// <summary>
        /// IT_MANIFEST_ENTRYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao manifestEntryDao;

        /// <summary>
        /// IM_SHOUHIZEIDao
        /// </summary>
        r_framework.Dao.IM_SHOUHIZEIDao shouhizeiDao;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        r_framework.Dao.IM_SHAINDao shainDao;

        /// <summary>
        /// IM_YOUKIDao
        /// </summary>
        r_framework.Dao.IM_YOUKIDao youkiDao;

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
        /// IM_MANIFEST_SHURUIDao
        /// </summary>
        r_framework.Dao.IM_MANIFEST_SHURUIDao manifestShuruiDao;

        /// <summary>
        /// IM_MANIFEST_TEHAIDao
        /// </summary>
        r_framework.Dao.IM_MANIFEST_TEHAIDao manifestTehaiDao;

        /// <summary>
        /// IM_SHARYOUDao
        /// </summary>
        r_framework.Dao.IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// IM_UNITDao
        /// </summary>
        r_framework.Dao.IM_UNITDao unitDao;

        /// <summary>
        /// IM_DENPYOU_KBNDao
        /// </summary>
        r_framework.Dao.IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// IM_KEITAI_KBNDao
        /// </summary>
        r_framework.Dao.IM_KEITAI_KBNDao keitaiKbnDao;

        /// <summary>
        /// IT_SEIKYUU_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao seikyuuDetail;

        /// <summary>
        /// IT_SEISAN_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao seisanDetail;

        /// <summary>
        /// IS_NUMBER_RECEIPTDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPTDao numberReceiptDao;

        /// <summary>
        /// IS_NUMBER_RECEIPT_YEARDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPT_YEARDao numberReceiptYearDao;

        /// <summary>
        /// 取引先_請求情報DAO
        /// </summary>
        MTSeiClass mtSeiDao;

        /// <summary>
        /// 取引先_支払情報DAO
        /// </summary>
        MTShihaClass mtShihaDao;

        /// <summary>
        /// 車種Dao
        /// </summary>
        r_framework.Dao.IM_SHASHUDao shashuDao;

        /// <summary>
        /// 形態区分DAO
        /// </summary>
        MKKClass mkkDao;

        /// <summary>
        /// 計量番号DAO
        /// </summary>
        TKEClass tkeDao;

        r_framework.Dao.IM_KOBETSU_HINMEIDao kobetsuHinmei;
        GET_SYSDATEDao dao;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HINMEIDao>();
            this.keiryouEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_KEIRYOU_ENTRYDao>();
            this.keiryouDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_KEIRYOU_DETAILDao>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.manifestEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            this.youkiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_YOUKIDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao>();
            this.manifestShuruiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_SHURUIDao>();
            this.manifestTehaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_MANIFEST_TEHAIDao>();
            this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();
            this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_DENPYOU_KBNDao>();
            this.keitaiKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KEITAI_KBNDao>();
            this.seikyuuDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEIKYUU_DETAILDao>();
            this.seisanDetail = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SEISAN_DETAILDao>();
            this.numberReceiptDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPTDao>();
            this.numberReceiptYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_RECEIPT_YEARDao>();
            this.mtSeiDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.KeiryouNyuuryoku.DAO.MTSeiClass>();
            this.mtShihaDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.KeiryouNyuuryoku.DAO.MTShihaClass>();
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.tkeDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.KeiryouNyuuryoku.DAO.TKEClass>();
            this.mkkDao = DaoInitUtility.GetComponent<Shougun.Core.Scale.KeiryouNyuuryoku.DAO.MKKClass>();
            this.kobetsuHinmei = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEIDao>();
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            this.ukeireJissekiEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_JISSEKI_ENTRYDao>();
            this.ukeireJissekiDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_UKEIRE_JISSEKI_DETAILDao>();

        }
        #endregion

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
            return returnEntity[0];
        }

        /// <summary>
        /// 計量入力を取得
        /// </summary>
        /// <param name="keiryouNumber">計量番号</param>
        /// <returns></returns>
        public T_KEIRYOU_ENTRY[] GetKeiryouEntry(SqlInt64 keiryouNumber, string seq)
        {
            T_KEIRYOU_ENTRY entity = new T_KEIRYOU_ENTRY();
            entity.KEIRYOU_NUMBER = keiryouNumber;
            entity.SEQ = SqlInt32.Parse(seq);
            return this.keiryouEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 計量入力の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertKeiryouEntry(T_KEIRYOU_ENTRY entity)
        {
            return this.keiryouEntryDao.Insert(entity);
        }

        /// <summary>
        /// 計量入力の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateKeiryouEntry(T_KEIRYOU_ENTRY entity)
        {
            return this.keiryouEntryDao.Update(entity);
        }

        /// <summary>
        /// 計量明細を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_KEIRYOU_DETAIL[] GetKeiryouDetail(SqlInt64 entrySysId, SqlInt32 entrySeq)
        {
            T_KEIRYOU_DETAIL entity = new T_KEIRYOU_DETAIL();
            entity.SYSTEM_ID = entrySysId;
            entity.SEQ = entrySeq;
            return keiryouDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 計量明細を登録
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int InsertKeiryouDetail(T_KEIRYOU_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var keiryouDetail in entitys)
            {
                returnint = returnint + this.keiryouDetailDao.Insert(keiryouDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 計量明細を更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int UpdateKeiryouDetail(T_KEIRYOU_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var keiryouDetail in entitys)
            {
                returnint = returnint + this.keiryouDetailDao.Update(keiryouDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 受入実績を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_UKEIRE_JISSEKI_ENTRY[] GetUkeireJissekiEntry(SqlInt16 entryShurui, SqlInt64 entrySysId)
        {
            T_UKEIRE_JISSEKI_ENTRY entity = new T_UKEIRE_JISSEKI_ENTRY();
            entity.DENPYOU_SHURUI = entryShurui;
            entity.DENPYOU_SYSTEM_ID = entrySysId;
            return ukeireJissekiEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 受入実績の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertUkeireJissekiEntry(T_UKEIRE_JISSEKI_ENTRY entity)
        {
            return this.ukeireJissekiEntryDao.Insert(entity);
        }

        /// <summary>
        /// 受入実績の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateUkeireJissekiEntry(T_UKEIRE_JISSEKI_ENTRY entity)
        {
            return this.ukeireJissekiEntryDao.Update(entity);
        }

        /// <summary>
        /// 受入実績明細を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_UKEIRE_JISSEKI_DETAIL[] GetUkeireJissekiDetail(SqlInt16 entryShurui, SqlInt64 entrySysId, SqlInt32 entrySeq)
        {
            T_UKEIRE_JISSEKI_DETAIL entity = new T_UKEIRE_JISSEKI_DETAIL();
            entity.DENPYOU_SHURUI = entryShurui;
            entity.DENPYOU_SYSTEM_ID = entrySysId;
            entity.SEQ = entrySeq;
            return ukeireJissekiDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 受入実績明細を登録
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int InsertUkeireJissekiDetail(T_UKEIRE_JISSEKI_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var ukeireJissekiDetail in entitys)
            {
                returnint = returnint + this.ukeireJissekiDetailDao.Insert(ukeireJissekiDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 受入実績用のMAXSEQ取得処理（DELETE_FLGに関わらず）
        /// </summary>
        /// <param name="entryShurui"></param>
        /// <param name="entrySysId"></param>
        /// <returns></returns>
        public SqlInt32 OldSEQForUkeireJisseki(SqlInt16 entryShurui, SqlInt64 entrySysId)
        {
            SqlInt32 returnInt = 1;

            returnInt = this.ukeireJissekiEntryDao.getMaxSEQ(" WHERE DENPYOU_SHURUI = " + entryShurui + " AND DENPYOU_SYSTEM_ID = " + entrySysId);

            return returnInt;
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
        /// 計量入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateSystemIdForKeiryou()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;
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
        /// 受入実績用のDETAIL_SYSTEM_ID採番処理
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateSystemIdForUkeireJisseki()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE_JISSEKI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE_JISSEKI;
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

        public SqlInt64 CreateKeiryouNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;
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
        /// 品名テーブルの情報を取得
        /// 適用開始日、終了日、削除フラグについては
        /// 有効なものだけを検索します
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI[] GetAllValidHinmeiData(string key = null)
        {
            M_HINMEI keyEntity = new M_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.HINMEI_CD = key;
            }

            return this.hinmeiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_HINMEI GetHinmeiDataByCd(string key)
        {
            M_HINMEI keyEntity = new M_HINMEI();
            if (!string.IsNullOrEmpty(key))
            {
                keyEntity.HINMEI_CD = key;
            }

            M_HINMEI returnValue = new M_HINMEI();
            M_HINMEI[] hinmeis = this.hinmeiDao.GetAllValidData(keyEntity);

            if (hinmeis != null && hinmeis.Length > 0)
            {
                returnValue = hinmeis[0];
            }

            return returnValue;

        }

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

            if (returnValue == null)
            {
                keyEntity.GENBA_CD = "";
                returnValue = this.kobetsuHinmei.GetDataByHinmei(keyEntity, hinmei);
            }
            return returnValue;

        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグを指定しない
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetAllDataByCodeForKyoten(short kyotenCd)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 荷降業者を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA[] GetNizumiGyousha(string gyoushaCd)
        {

            M_GYOUSHA[] gyoushas = null;

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return gyoushas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.NizumiGyoushaCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return gyoushas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

            gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

            return gyoushas;
        }

        /// <summary>
        /// 荷降現場を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="genbaCd">GYOUSHA_CD</param>
        /// <param name="gyoushaCd">GENBA_CD</param>
        /// <returns></returns>
        public M_GENBA[] GetNizumiGenba(string gyoushaCd, string genbaCd)
        {

            M_GENBA[] genbas = null;

            if (string.IsNullOrEmpty(genbaCd))
            {
                return genbas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = "AND (GYOUSHA_CD = '" + gyoushaCd + "' AND GENBA_CD = '" + genbaCd + "')";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Genba.NizumiGenbaCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return genbas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GENBA>();

            genbas = dataBinderUtil.CreateEntityForDataTable(dt);

            return genbas;
        }

        /// <summary>
        /// 運搬業者を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA[] GetUnpanGyousha(string gyoushaCd)
        {

            M_GYOUSHA[] gyoushas = null;

            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return gyoushas;
            }

            // SQL文作成
            DataTable dt = new DataTable();
            string whereStr = " AND GYOUSHA_CD = '" + gyoushaCd + "'";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.Gyousha.UnpanGyoushaCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    dt = this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }

            if (dt == null || dt.Rows.Count < 1)
            {
                return gyoushas;
            }

            var dataBinderUtil = new DataBinderUtility<M_GYOUSHA>();

            gyoushas = dataBinderUtil.CreateEntityForDataTable(dt);

            return gyoushas;
        }

        /// <summary>
        /// マニフェスト取得
        /// 取得後、keiryouDetailのキーでさらに絞り込んで使用してもらうため、
        /// あえてDataTableで返しています。
        /// </summary>
        /// <param name="keiryouDetails">連携しているT_KEIRYOU_DETAIL</param>
        /// <returns>必要なキーが設定されていない場合はNullを返します。</returns>
        public DataTable GetManifestEntry(T_KEIRYOU_DETAIL[] keiryouDetails)
        {
            if (keiryouDetails == null || keiryouDetails.Length < 1)
            {
                return null;
            }

            if (keiryouDetails[0].SYSTEM_ID.IsNull)
            {
                // 念のため先頭のキー値チェック
                return null;
            }

            // SQL文作成
            string whereStr = " AND RENKEI_DENSHU_KBN_CD = '" + SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU + "' AND RENKEI_SYSTEM_ID = '" + keiryouDetails[0].SYSTEM_ID + "' AND RENKEI_MEISAI_SYSTEM_ID IN ( ";

            // RENKEI_MEISAI_SYSTEM_ID用の条件句作成
            bool existRenkeiMeisaiId = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < keiryouDetails.Length; i++)
            {
                if (keiryouDetails[i].DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                if (i == 0)
                {
                    existRenkeiMeisaiId = true;
                    sb.Append("'" + keiryouDetails[i].DETAIL_SYSTEM_ID + "'");
                }
                else
                {
                    existRenkeiMeisaiId = true;
                    sb.Append(", '" + keiryouDetails[i].DETAIL_SYSTEM_ID + "'");
                }
            }

            if (!existRenkeiMeisaiId)
            {
                return null;    // 明細IDが無いと一意に識別できないためreturn
            }

            whereStr = whereStr + sb.ToString() + ")";

            var thisAssembly = Assembly.LoadFrom("ShougunCSCommon.dll");
            using (var resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Function.ShougunCSCommon.Dao.SqlFile.ManifestEntry.RenkeiDataCondition.sql"))
            {
                using (var sqlStr = new StreamReader(resourceStream))
                {
                    return this.gyoushaDao.GetDateForStringSql(sqlStr.ReadToEnd().Replace(Environment.NewLine, "") + whereStr);

                }
            }
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
        /// 消費税率を取得
        /// DELETE_FLGとTEKIYOU_BEGIN, TEKIYOU_ENDの絞込みはしない
        /// </summary>
        /// <returns></returns>
        public M_SHOUHIZEI[] GetAllShouhizeiRate()
        {
            return this.shouhizeiDao.GetAllData();
        }

        /// <summary>
        /// 社員取得
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <param name="containsDelete">削除済みのデータ有無</param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd, bool containsDelete)
        {
            if (string.IsNullOrEmpty(shainCd))
            {
                return null;
            }

            if (containsDelete)
            {
                // 削除済みも含みで社員マスタ取得
                return this.shainDao.GetDataByCd(shainCd);
            }
            else
            {
                // 削除されてない社員を取得
                return GetShain(shainCd);
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
        public M_GYOUSHA GetGyousha(string gyoushaCd, object strDenpyouDate, object sysDate, out bool catchErr)
        {
            catchErr = true;
            try
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }

        }

        /// <summary>
        /// 業者取得（削除済、適用期間外も含め）
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return null;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha == null || gyousha.Length < 1)
                {
                    return null;
                }
                else
                {
                    return gyousha[0];
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }

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

            for(int i = 0 ; i<genba.Length; i++)
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
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate, out bool catchErr, bool isCheckDelete = true)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return null;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = !isCheckDelete;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    return null;
                }

                if (null != genba && genba.Length > 0 && isCheckDelete)
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }

        /// <summary>
        /// 現場取得（削除済、適用期間外も含め）
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return null;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    return null;
                }
                else
                {
                    // PK指定のため1件
                    return genba[0];
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenbaList(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            List<M_GENBA> retlist = new List<M_GENBA>();
            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
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
        /// 現場取得（業者コードによる）
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenbaByGyousha(string gyoushaCd, object strDenpyouDate, object sysDate)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            List<M_GENBA> retlist = new List<M_GENBA>();
            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
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
        /// 容器取得
        /// </summary>
        /// <param name="youkiCd"></param>
        /// <returns></returns>
        public M_YOUKI GetYouki(string youkiCd)
        {
            if (string.IsNullOrEmpty(youkiCd))
            {
                return null;
            }

            M_YOUKI keyEntity = new M_YOUKI();
            keyEntity.YOUKI_CD = youkiCd;
            var youki = this.youkiDao.GetAllValidData(keyEntity);
            if (youki == null || youki.Length < 1)
            {
                return null;
            }

            return youki[0];
        }

        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, object strDenpyouDate, object sysDate, out bool catchErr, bool isCheckTekiyoubi = true)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return null;
                }

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = !isCheckTekiyoubi;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
                if (torihikisaki == null || torihikisaki.Length < 1)
                {
                    return null;
                }

                if (null != torihikisaki && torihikisaki.Length > 0 && isCheckTekiyoubi)
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }

        /// <summary>
        /// 取引先取得（削除済、適用期間外も含め）
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return null;
                }

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
                if (torihikisaki == null || torihikisaki.Length < 1)
                {
                    return null;
                }
                else
                {
                    return torihikisaki[0];
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
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
        /// マニフェスト種類取得
        /// </summary>
        /// <param name="manifestShuruiCd"></param>
        /// <returns></returns>
        public M_MANIFEST_SHURUI GetManifestShurui(SqlInt16 manifestShuruiCd)
        {
            if (manifestShuruiCd.IsNull)
            {
                return null;
            }

            M_MANIFEST_SHURUI keyEntity = new M_MANIFEST_SHURUI();
            keyEntity.MANIFEST_SHURUI_CD = manifestShuruiCd;
            var manifestShuruiList = this.manifestShuruiDao.GetAllValidData(keyEntity);
            if (manifestShuruiList == null || manifestShuruiList.Length < 1)
            {
                return null;
            }

            // PK指定のためデータ1件
            return manifestShuruiList[0];
        }

        /// <summary>
        /// マニフェスト手配取得
        /// </summary>
        /// <param name="manifestTehaiCd"></param>
        /// <returns></returns>
        public M_MANIFEST_TEHAI GetManifestTehai(SqlInt16 manifestTehaiCd)
        {
            if (manifestTehaiCd.IsNull)
            {
                return null;
            }

            M_MANIFEST_TEHAI keyEntity = new M_MANIFEST_TEHAI();
            keyEntity.MANIFEST_TEHAI_CD = manifestTehaiCd;
            var manifestTehaiList = this.manifestTehaiDao.GetAllValidData(keyEntity);
            if (manifestTehaiList == null || manifestTehaiList.Length < 1)
            {
                return null;
            }

            // PK指定のためにデータ1件
            return manifestTehaiList[0];
        }

        /// <summary>
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd, SqlDateTime strDenpyouDate, string gyoushakbn)
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

            var sharyous = this.sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, gyoushakbn, strDenpyouDate, true, false, false);
            if (sharyous == null || sharyous.Length < 1)
            {
                return null;
            }

            return sharyous;
        }

        /// <summary>
        /// 単位区分取得
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <returns></returns>
        public M_UNIT[] GetUnit(short unitCd)
        {
            if (unitCd < 0)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            var units = this.unitDao.GetAllValidData(keyEntity);
            if (units == null || units.Length < 1)
            {
                return null;
            }

            return units;
        }

        /// <summary>
        /// 単位区分取得
        /// 削除済みデータも検索します。
        /// </summary>
        /// <param name="unitCd">単位区分CD</param>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit(short unitCd)
        {
            if (unitCd < 0)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var units = this.unitDao.GetAllValidData(keyEntity);
            if (units == null || units.Length < 1)
            {
                return null;
            }

            return units;
        }

        /// <summary>
        /// 伝票区分一覧取得
        /// </summary>
        /// <returns></returns>
        public M_DENPYOU_KBN[] GetAllDenpyouKbn()
        {
            M_DENPYOU_KBN keyEntity = new M_DENPYOU_KBN();
            return this.denpyouKbnDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 容器一覧取得
        /// </summary>
        /// <returns></returns>
        public M_YOUKI[] GetAllYouki()
        {
            M_YOUKI keyEntity = new M_YOUKI();
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            return this.youkiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 単位一覧取得
        /// </summary>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit()
        {
            M_UNIT keyEntity = new M_UNIT();
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            return this.unitDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 品名一覧取得
        /// </summary>
        /// <returns></returns>
        public M_HINMEI[] GetAllHinmei()
        {
            M_HINMEI keyEntity = new M_HINMEI();
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            return this.hinmeiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="keitaiCbnCd">形態区分CD</param>
        /// <param name="containsDelete">削除済みのデータ有無</param>
        /// <returns></returns>
        public M_KEITAI_KBN GetkeitaiKbn(short keitaiCbnCd, bool containsDelete)
        {
            if (containsDelete)
            {
                if (keitaiCbnCd < 0)
                {
                    return null;
                }
                // 削除済みも含みで形態区分マスタ取得
                return this.keitaiKbnDao.GetDataByCd(keitaiCbnCd.ToString());
            }
            else
            {
                // 削除されてない形態区分を取得
                return GetkeitaiKbn(keitaiCbnCd);
            }
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <returns></returns>
        public M_KEITAI_KBN GetkeitaiKbn(short keitaiCbnCd)
        {
            if (keitaiCbnCd < 0)
            {
                return null;
            }

            M_KEITAI_KBN keyEntity = new M_KEITAI_KBN();
            keyEntity.KEITAI_KBN_CD = keitaiCbnCd;
            var result = this.keitaiKbnDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return null;
            }

            return result[0];
        }


        /// <summary>
        /// 領収書管理取得
        /// </summary>
        /// <param name="hiduke">日付</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_RECEIPT GetNumberReceipt(DateTime hiduke, short kyotenCd)
        {
            int index = 0;
            S_NUMBER_RECEIPT keyEntity = new S_NUMBER_RECEIPT();
            if (hiduke == null)
            {
                return null;
            }
            keyEntity.NUMBERED_DAY = hiduke.Date;
            keyEntity.KYOTEN_CD = (SqlInt16)kyotenCd;

            var numberReceips = this.numberReceiptDao.GetDataForEntity(keyEntity);
            if (numberReceips == null
                || numberReceips.Length < 1)
            {
                return null;
            }
            //最大CURRENT_NUMBERを取得する
            else
            {
                SqlInt32 Maxnum = 0;
                for (int i = 0; i < numberReceips.Length; i++)
                {
                    S_NUMBER_RECEIPT sn = numberReceips[i];
                    if (sn != null)
                    {
                        if (Maxnum <= sn.CURRENT_NUMBER)
                        {
                            Maxnum = sn.CURRENT_NUMBER;
                            index = i;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return numberReceips[index];
        }

        /// <summary>
        /// 領収書管理更新
        /// </summary>
        /// <param name="taergetEntity"></param>
        public void InsertNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.NUMBERED_DAY.IsNull
                || targetEntity.KYOTEN_CD.IsNull)
            {
                return;
            }
            this.numberReceiptDao.Insert(targetEntity);
        }

        /// <summary>
        /// 領収書管理更新
        /// </summary>
        /// <param name="targetEntity"></param>
        public void UpdateNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.NUMBERED_DAY.IsNull
                || targetEntity.KYOTEN_CD.IsNull)
            {
                return;
            }
            this.numberReceiptDao.Update(targetEntity);
        }

        /// <summary>
        /// 領収書管理(年連番)取得
        /// </summary>
        /// <param name="hiduke">日付</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        public S_NUMBER_RECEIPT_YEAR GetNumberReceiptYear(DateTime hiduke, short kyotenCd)
        {
            int index = 0;
            S_NUMBER_RECEIPT_YEAR keyEntity = new S_NUMBER_RECEIPT_YEAR();
            if (hiduke == null)
            {
                return null;
            }
            keyEntity.NUMBERED_YEAR = (SqlInt16)hiduke.Year;
            keyEntity.KYOTEN_CD = (SqlInt16)kyotenCd;

            var data = this.numberReceiptYearDao.GetDataForEntity(keyEntity);
            if (data == null
                || data.Length < 1)
            {
                return null;
            }
            //最大CURRENT_NUMBERを取得する
            else
            {
                SqlInt32 Maxnum = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    S_NUMBER_RECEIPT_YEAR sn = data[i];
                    if (sn != null)
                    {
                        if (Maxnum <= sn.CURRENT_NUMBER)
                        {
                            Maxnum = sn.CURRENT_NUMBER;
                            index = i;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return data[index];
        }

        /// <summary>
        /// 領収書管理(年連番)更新
        /// </summary>
        /// <param name="taergetEntity"></param>
        public void InsertNumberReceiptYear(S_NUMBER_RECEIPT_YEAR targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.NUMBERED_YEAR.IsNull
                || targetEntity.KYOTEN_CD.IsNull)
            {
                return;
            }
            this.numberReceiptYearDao.Insert(targetEntity);
        }

        /// <summary>
        /// 領収書管理(年連番)更新
        /// </summary>
        /// <param name="targetEntity"></param>
        public void UpdateNumberReceiptYear(S_NUMBER_RECEIPT_YEAR targetEntity)
        {
            // キーチェック
            if (targetEntity == null
                || targetEntity.NUMBERED_YEAR.IsNull
                || targetEntity.KYOTEN_CD.IsNull)
            {
                return;
            }
            this.numberReceiptYearDao.Update(targetEntity);
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
        /// 取引先区分（請求情報）取得
        /// </summary>
        /// <param name="torihikisakiCD"></param>
        internal string GetTrihikisakiKBN_Seikyuu(string torihikisakiCD)
        {
            //キーチェック
            if (torihikisakiCD == null)
            {
                return null;
            }

            return this.mtSeiDao.GetTorihikisakiKBN_Seikyuu(torihikisakiCD);
        }

        /// <summary>
        /// 取引先区分（支払情報）取得
        /// </summary>
        /// <param name="torihikisakiCD"></param>
        internal string GetTrihikisakiKBN_Shiharai(string torihikisakiCD)
        {
            //キーチェック
            if (torihikisakiCD == null)
            {
                return null;
            }

            return this.mtShihaDao.GetTorihikisakiKBN_Shiharai(torihikisakiCD);
        }


        /// <summary>
        /// 単位CD取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal M_UNIT[] GetUnitCd(M_UNIT data)
        {
            //キーチェック
            if (data == null)
            {
                return null;
            }

            return this.unitDao.GetAllValidData(data);
        }

        /// <summary>
        /// 同一伝種区分コード中最も若い形態区分コードを取得
        /// </summary>
        /// <param name="denshuKbnCd"></param>
        /// <returns></returns>
        internal SqlInt16 GetKeitaiKbnCd(SqlInt16 denshuKbnCd)
        {
            //キーチェック
            if (denshuKbnCd == 0)
            {
                return 0;
            }

            SqlInt16 returnValue = this.mkkDao.GetKeitaiKbnCd(denshuKbnCd);
            if (!returnValue.IsNull)
            {
                return returnValue;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 形態区分名称略称を取得
        /// </summary>
        /// <param name="keitaiKbnCd"></param>
        /// <returns></returns>
        internal string GetKeitaiKbnNameRyaku(SqlInt16 keitaiKbnCd)
        {
            //キーチェック
            if (keitaiKbnCd == 0 || keitaiKbnCd.IsNull)
            {
                return null;
            }
            M_KEITAI_KBN tmpEntity = new M_KEITAI_KBN();
            tmpEntity = GetkeitaiKbn((short)keitaiKbnCd);
            if (tmpEntity == null)
            {
                return null;
            }

            return tmpEntity.KEITAI_KBN_NAME_RYAKU;
        }

        /// <summary>
        /// 指定された計量番号の次に大きい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber"></param>
        /// <param name="KyotenCD"></param>
        /// <returns></returns>
        internal long GetNextKeiryouNumber(long KeiryouNumber, string KyotenCD)
        {
            SqlInt64 returnValue = this.tkeDao.GetNextKeiryouNumber(SqlInt64.Parse(KeiryouNumber.ToString()), KyotenCD);
            if (!returnValue.IsNull)
            {
                return long.Parse(returnValue.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 指定された計量番号の次に小さい番号を取得
        /// </summary>
        /// <param name="KeiryouNumber"></param>
        /// <param name="KyotenCD"></param>
        /// <returns></returns>
        internal long GetPreKeiryouNumber(long KeiryouNumber, string KyotenCD)
        {
            //キーチェック
            if (KeiryouNumber == 0)
            {
                return 0;
            }

            SqlInt64 returnValue = this.tkeDao.GetPreKeiryouNumber(SqlInt64.Parse(KeiryouNumber.ToString()), KyotenCD);
            if (!returnValue.IsNull)
            {
                return long.Parse(returnValue.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 登録された一番大きい計量番号を取得
        /// </summary>
        /// <returns></returns>
        internal long GetMaxKeiryouNumber()
        {
            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);

            SqlInt64 returnValue = this.numberSystemDao.GetMaxPlusKey(entity) - 1;
            if (!returnValue.IsNull)
            {
                return long.Parse(returnValue.ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion


        /// <summary>
        /// 滞留データを取得
        /// </summary>
        /// <returns></returns>
        internal DataTable GetTairyuuData(TairyuuDTOClass data)
        {
            DataTable dt = this.tkeDao.GetTairyuuData(data);
            return dt;
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, object strDenpyouDate, object sysDate, bool hstFlg, bool sbnFlg, bool lastSbnFlg, out bool catchErr)
        {
            catchErr = true;
            try
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
                    if (hstFlg && !(gyousha[0].HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue && gyousha[0].GYOUSHAKBN_MANI))
                    {
                        return null;
                    }
                    if ((sbnFlg || lastSbnFlg) && !(gyousha[0].SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && gyousha[0].GYOUSHAKBN_MANI))
                    {
                        return null;
                    }
                }

                return gyousha[0];
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }

        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate, bool hstFlg, bool sbnFlg, bool lastSbnFlg, out bool catchErr, bool isCheckDelete = true)
        {
            catchErr = true;
            try
            {
                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return null;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = !isCheckDelete;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    return null;
                }

                if (null != genba && genba.Length > 0 && isCheckDelete)
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
                    if (hstFlg && !genba[0].HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue)
                    {
                        return null;
                    }
                    if (sbnFlg && !(genba[0].SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba[0].SAISHUU_SHOBUNJOU_KBN.IsTrue))
                    {
                        return null;
                    }
                    if (lastSbnFlg && !genba[0].SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    {
                        return null;
                    }
                }

                // PK指定のため1件
                return genba[0];
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = false;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = false;
                return null;
            }
        }

        /// <summary>
        /// システム日付取得
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            DateTime sysDate = DateTime.Now;
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return sysDate;
        }

        #region Utility
        #endregion

        public M_SHARYOU[] GetSharyouMod(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd)
        {
            if (string.IsNullOrEmpty(sharyouCd))
            {
                return null;
            }
            string tmpSharyou = sharyouCd;
            M_SHARYOU keyEntity = new M_SHARYOU();
            keyEntity.SHARYOU_CD = tmpSharyou;
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

            M_SHARYOU[] sharyous = this.sharyouDao.GetAllValidDataMod(keyEntity);
            if (sharyous == null || sharyous.Length < 1)
            {
                return null;
            }

            return sharyous;

        }
    }
}