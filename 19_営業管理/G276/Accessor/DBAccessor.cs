using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;
using System.Data.SqlTypes;
using r_framework.Logic;
using r_framework.Const;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.IO;
//using Shougun.Core.Scale.Keiryou.Utility;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Accessor
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
        /// T_MITSUMORI_DETAILDao
        /// </summary>
        Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IT_MITSUMORI_DETAILDao mitsumoriDetailDao;

        /// <summary>
        /// IT_MITSUMORI_ENTRYDao
        /// </summary>
        Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IT_MITSUMORI_ENTRYDao mitsumoriEntryDao;

        /// <summary>
        /// IM_HIKIAI_TORIHIKISAKIDao
        /// </summary>
        Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_TORIHIKISAKIDao hikiaiTorihikisakiDao;

        /// <summary>
        /// IM_HIKIAI_GYOUSHADao
        /// </summary>
        Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GYOUSHADao hikiaiGyoushaDao;

        /// <summary>
        /// IM_HIKIAI_GENBADao
        /// </summary>
        Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GENBADao hikiaiGenbaDao;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        r_framework.Dao.IM_KYOTENDao kyotenDao;

        /// <summary>
        /// IM_BUMONDao
        /// </summary>
        r_framework.Dao.IM_BUMONDao bumonDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        r_framework.Dao.IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        r_framework.Dao.IM_GENBADao genbaDao;

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
        /// IM_KOBETSU_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;

        /// <summary>
        /// 
        /// </summary>
        r_framework.Dao.IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao;

        /// <summary>
        /// IM_CONTENA_JOUKYOUDao
        /// </summary>
        r_framework.Dao.IM_CONTENA_JOUKYOUDao contenaJoukyouDao;

        /// <summary>
        /// IM_DENPYOU_KBNDao
        /// </summary>
        r_framework.Dao.IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// IM_KEITAI_KBNDao
        /// </summary>
        r_framework.Dao.IM_KEITAI_KBNDao keitaiKbnDao;

        /// <summary>
        /// M_NISUGATADao
        /// </summary>
        r_framework.Dao.IM_NISUGATADao nisugataDao;

        /// <summary>
        /// M_CORP_INFODao
        /// </summary>
        r_framework.Dao.IM_CORP_INFODao corpInfoDao;

        /// <summary>
        /// 社員マスタ取得用DAO
        /// </summary>
        private MitsumoriTantousyaDao mitsumoriTantousyaDao;
        
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
            this.mitsumoriDetailDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IT_MITSUMORI_DETAILDao>();
            this.mitsumoriEntryDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IT_MITSUMORI_ENTRYDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.hikiaiTorihikisakiDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.hikiaiGyoushaDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.hikiaiGenbaDao = DaoInitUtility.GetComponent<Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.IM_HIKIAI_GENBADao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.bumonDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_BUMONDao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            this.youkiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_YOUKIDao>();
             this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SEIKYUUDao>();
            this.torihikisakiShiharaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao>();
            this.unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();
            this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();
            this.kihonHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KIHON_HINMEI_TANKADao>();
            this.contenaJoukyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CONTENA_JOUKYOUDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_DENPYOU_KBNDao>();
            this.corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();

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
        /// 取引先の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateTorihikisakiEntry(M_TORIHIKISAKI entity)
        {
            return this.torihikisakiDao.Update(entity);
        }

        /// <summary>
        /// 引合取引先の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertHikiaiTorihikisakiEntry(M_HIKIAI_TORIHIKISAKI entity)
        {
            return this.hikiaiTorihikisakiDao.Insert(entity);
        }

        /// <summary>
        /// 引合取引先の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHikiaiTorihikisakiEntry(M_HIKIAI_TORIHIKISAKI entity)
        {
            return this.hikiaiTorihikisakiDao.Update(entity);
        }

        /// <summary>
        /// 引合取引先の取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_HIKIAI_TORIHIKISAKI GetHikiaiTorihikisakiEntry(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_HIKIAI_TORIHIKISAKI keyEntity = new M_HIKIAI_TORIHIKISAKI();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisaki = this.hikiaiTorihikisakiDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }

            return torihikisaki[0];
        }

        /// <summary>
        /// 引合業者の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertHikiaiGyoushaEntry(M_HIKIAI_GYOUSHA entity)
        {
            return this.hikiaiGyoushaDao.Insert(entity);
        }

        /// <summary>
        /// 業者の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateGyoushaEntry(M_GYOUSHA entity)
        {
            return this.gyoushaDao.Update(entity);
        }

        /// <summary>
        /// 引合業者の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHikiaiGyoushaEntry(M_HIKIAI_GYOUSHA entity)
        {
            return this.hikiaiGyoushaDao.Update(entity);
        }

        /// <summary>
        /// 引合業者の取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_HIKIAI_GYOUSHA GetHikiaiGyoushaEntry(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_HIKIAI_GYOUSHA keyEntity = new M_HIKIAI_GYOUSHA();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisaki = this.hikiaiGyoushaDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }

            return torihikisaki[0];
        }

        /// <summary>
        /// 引合現場の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertHikiaiGenbaEntry(M_HIKIAI_GENBA entity)
        {
            return this.hikiaiGenbaDao.Insert(entity);
        }

        /// <summary>
        /// 現場の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateGenbaEntry(M_GENBA entity)
        {
            return this.gyoushaDao.Update(entity);
        }
        
        /// <summary>
        /// 引合現場の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHikiaiGenbaEntry(M_HIKIAI_GENBA entity)
        {
            return this.hikiaiGyoushaDao.Update(entity);
        }

        /// <summary>
        /// 見積入力の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertMitsumoriEntry(T_MITSUMORI_ENTRY entity)
        {
            return this.mitsumoriEntryDao.Insert(entity);
        }

        /// <summary>
        /// 見積入力の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateMitsumoriEntry(T_MITSUMORI_ENTRY entity)
        {
            return this.mitsumoriEntryDao.Update(entity);
        }

        /// <summary>
        /// 見積入力を取得
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T_MITSUMORI_ENTRY[] GetMitsumoriEntry(SqlInt64 mitsumoriNumber)
        {
            T_MITSUMORI_ENTRY entity = new T_MITSUMORI_ENTRY();
            entity.MITSUMORI_NUMBER = mitsumoriNumber;
            return mitsumoriEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 見積明細を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_MITSUMORI_DETAIL[] GetMitsumoriDetail(SqlInt64 entrySysId, SqlInt32 entrySeq)
        {
            T_MITSUMORI_DETAIL entity = new T_MITSUMORI_DETAIL();
            entity.SYSTEM_ID = entrySysId;
            entity.SEQ = entrySeq;
            return mitsumoriDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 見積明細を登録
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int InsertMitsumoriDetail(T_MITSUMORI_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var mitsumoriDetail in entitys)
            {
                returnint = returnint + this.mitsumoriDetailDao.Insert(mitsumoriDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 見積明細を更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int UpdateMitsumoriDetail(T_MITSUMORI_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var mitsumoriDetail in entitys)
            {
                returnint = returnint + this.mitsumoriDetailDao.Update(mitsumoriDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 見積入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt32 createSystemIdForMitsumori()
        {
            SqlInt32 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (short)DENSHU_KBN.MITSUMORI;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (short)DENSHU_KBN.MITSUMORI;
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
        /// 見積番号採番処理
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt32 createMitsumoriNumber()
        {
            SqlInt32 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = (short)DENSHU_KBN.MITSUMORI;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = (short)DENSHU_KBN.MITSUMORI;
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
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        public M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            if (kyotenCd < 1)
            {
                return null;
            }

            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 部門を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="bumonCd">BUMON_CD</param>
        /// <returns></returns>
        public M_BUMON[] GetDataByCodeForBumon(string bumonCd)
        {
            if (string.IsNullOrEmpty(bumonCd))
            {
                return null;
            }

            M_BUMON keyEntity = new M_BUMON();
            keyEntity.BUMON_CD = bumonCd;
            return this.bumonDao.GetAllValidData(keyEntity);
        }

        ///// <summary>
        ///// 消費税率を取得
        ///// </summary>
        ///// <param name="denpyouHiduke">適用期間条件</param>
        ///// <returns></returns>
        //public M_SHOUHIZEI GetShouhizeiRate(DateTime denpyouHiduke)
        //{
        //    M_SHOUHIZEI returnEntity = null;

        //    if (denpyouHiduke == null)
        //    {
        //        return returnEntity;
        //    }

        //    // SQL文作成(冗長にならないためsqlファイルで管理しない)
        //    DataTable dt = new DataTable();
        //    string selectStr = "SELECT * FROM M_SHOUHIZEI";
        //    string whereStr = " WHERE DELETE_FLG = 0";

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(" AND");
        //    sb.Append(" (");
        //    sb.Append("  (");
        //    sb.Append("  TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, '" + denpyouHiduke + "', 111), 120) <= TEKIYOU_END");
        //    sb.Append("  )");
        //    sb.Append(" )");

        //    whereStr = whereStr + sb.ToString();

        //    dt = this.gyoushaDao.GetDateForStringSql(selectStr + whereStr);

        //    if (dt == null || dt.Rows.Count < 1)
        //    {
        //        return returnEntity;
        //    }

        //    var dataBinderUtil = new DataBinderUtility<M_SHOUHIZEI>();

        //    var shouhizeis = dataBinderUtil.CreateEntityForDataTable(dt);
        //    returnEntity = shouhizeis[0];

        //    return returnEntity;
        //}

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
        /// 取引先取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd)
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

            return torihikisaki[0];
        }


        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
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
            else
            {
                return gyousha[0];
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }
            
            return genba;
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
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
            
            // PK指定のため1件
            return genba[0];
        }


        /// <summary>
        /// 引合現場の取得
        /// </summary>
        /// <param name="tirihikisakiCd"></param>
        /// <returns></returns>
        public M_HIKIAI_GENBA GetHikiaiGenbaEntry(string torihikisakiCd)
        {
            if (string.IsNullOrEmpty(torihikisakiCd))
            {
                return null;
            }

            M_HIKIAI_GENBA keyEntity = new M_HIKIAI_GENBA();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            var torihikisaki = this.hikiaiGenbaDao.GetAllValidData(keyEntity);
            if (torihikisaki == null || torihikisaki.Length < 1)
            {
                return null;
            }

            return torihikisaki[0];
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
        public M_SHARYOU[] GetSharyou(string sharyouCd)
        {
            if (string.IsNullOrEmpty(sharyouCd))
            {
                return null;
            }

            M_SHARYOU keyEntity = new M_SHARYOU();
            keyEntity.SHARYOU_CD = sharyouCd;
            var sharyous = this.sharyouDao.GetAllValidData(keyEntity);
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

        public M_KOBETSU_HINMEI_TANKA GetKobetsuhinmeiTanka(short denpyouKBNCd, string torihikisakiCd, string gyoushaCd,
            string genbaCd, string unpanGyoushaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd, short unitCd)
        {
            M_KOBETSU_HINMEI_TANKA keyEntity = new M_KOBETSU_HINMEI_TANKA();
            M_KOBETSU_HINMEI_TANKA[] kobetsuHinmeiTankas = null;

            // 伝票区分CDは動的に変更されないため最初に設定
            keyEntity.DENPYOU_KBN_CD = denpyouKBNCd;

            // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 6; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 5:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 現場, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && !string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞現場＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GENBA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 5; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 4:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && !string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 4; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 運搬業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 2; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷降現場，荷卸業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 3; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 荷卸業者, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                for (int i = 0; i < 2; i++)
                {
                    // 個別品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                    {
                        return kobetsuHinmeiTankas[0];
                    }
                }

            }

            // 値あり：品名, 単位, 取引先
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd)
                && string.IsNullOrEmpty(genbaCd)
                && string.IsNullOrEmpty(gyoushaCd)
                && !string.IsNullOrEmpty(torihikisakiCd))
            {

                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;

                kobetsuHinmeiTankas = this.kobetsuHinmeiTankaDao.GetAllValidData(keyEntity);
                if (kobetsuHinmeiTankas != null && 0 < kobetsuHinmeiTankas.Length)
                {
                    return kobetsuHinmeiTankas[0];
                }

            }

            // 何も取得できなかったとき
            return null;
        }

        /// <summary>
        /// 基本品名単価を検索する
        /// </summary>
        /// <param name="denpyouKBNCd">伝種区分CD</param>
        /// <param name="unpanGyoushaCd">運搬業者CD</param>
        /// <param name="nioroshiGyoushaCd">荷降業者CD</param>
        /// <param name="nioroshiGenbaCd">荷降現場CD</param>
        /// <param name="hinmeiCd">品名CD</param>
        /// <param name="unitCd">単位CD</param>
        /// <returns></returns>
        public M_KIHON_HINMEI_TANKA GetKihonHinmeitanka(short denpyouKBNCd, string unpanGyoushaCd, 
            string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd, short unitCd)
        {
            M_KIHON_HINMEI_TANKA keyEntity = new M_KIHON_HINMEI_TANKA();
            M_KIHON_HINMEI_TANKA[] kihonHinmeiTankas = null;

            // すべて値あり：品名, 単位, 荷卸現場, 荷卸業者, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for ( int i = 0; i < 4; i++ )
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 3:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸業者, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for ( int i = 0; i < 3; i++ )
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        case 2:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 運搬業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && string.IsNullOrEmpty(nioroshiGyoushaCd)
                && !string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.UNPAN_GYOUSHA_CD = unpanGyoushaCd;

                for ( int i = 0; i < 2; i++ )
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.UNPAN_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸現場, 荷卸業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && !string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GENBA_CD = nioroshiGenbaCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                for ( int i = 0; i < 3; i++ )
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GENBA_CD = null;
                            break;

                        case 2:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 値あり：品名, 単位, 荷卸業者
            if (!string.IsNullOrEmpty(hinmeiCd)
                && 0 < unitCd
                && string.IsNullOrEmpty(nioroshiGenbaCd)
                && !string.IsNullOrEmpty(nioroshiGyoushaCd)
                && string.IsNullOrEmpty(unpanGyoushaCd))
            {
                keyEntity.HINMEI_CD = hinmeiCd;
                keyEntity.UNIT_CD = unitCd;
                keyEntity.NIOROSHI_GYOUSHA_CD = nioroshiGyoushaCd;

                for ( int i = 0; i < 2; i++ )
                {
                    // 基本品名単価に検索結果がない場合＞下記の順に条件を外して再検索する
                    // 荷卸現場＞荷卸業者＞運搬業者
                    switch (i)
                    {
                        case 0:
                            break;

                        case 1:
                            keyEntity.NIOROSHI_GYOUSHA_CD = null;
                            break;

                        default:
                            break;

                    }

                    kihonHinmeiTankas = this.kihonHinmeiTankaDao.GetAllValidData(keyEntity);
                    if (kihonHinmeiTankas != null && 0 < kihonHinmeiTankas.Length)
                    {
                        return kihonHinmeiTankas[0];
                    }
                }
            }

            // 何も取得できなかった場合
            return null;
        }

        /// <summary>
        /// コンテナ状況取得
        /// </summary>
        /// <param name="contenaJoukyouCd"></param>
        /// <returns></returns>
        public M_CONTENA_JOUKYOU GetContenaJoukyou(short contenaJoukyouCd)
        {
            if (contenaJoukyouCd < 0)
            {
                return null;
            }

            M_CONTENA_JOUKYOU keyEntity = new M_CONTENA_JOUKYOU();
            keyEntity.CONTENA_JOUKYOU_CD = contenaJoukyouCd;
            var result = this.contenaJoukyouDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return new M_CONTENA_JOUKYOU();

            }

            return result[0];
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
        /// 伝票区分を取得
        /// </summary>
        /// <param name="kyotenCd">DENPYOU_KBN</param>
        /// <returns></returns>
        public M_DENPYOU_KBN GetDataByCodeForDenpyouKbn(short denpyouKbnCd)
        {
            if (denpyouKbnCd < 0)
            {
                return null;
            }

            M_DENPYOU_KBN keyEntity = new M_DENPYOU_KBN();
            keyEntity.DENPYOU_KBN_CD = (SqlInt16)denpyouKbnCd;
            var result = this.denpyouKbnDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return null;

            }

            return result[0];
        }
        /// <summary>
        /// 容器一覧取得
        /// </summary>
        /// <returns></returns>
        public M_YOUKI[] GetAllYouki()
        {
            M_YOUKI keyEntity = new M_YOUKI();
            return this.youkiDao.GetAllValidData(keyEntity);
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
                return new M_KEITAI_KBN();
            }

            return result[0];
        }
        /// <summary>
        /// 単位を取得
        /// </summary>
        /// <param name="kyotenCd">UNIT_CD</param>
        /// <returns></returns>
        public M_UNIT[] GetDataByCodeForUnit(short unitCd)
        {
            if (unitCd < 1)
            {
                return null;
            }

            M_UNIT keyEntity = new M_UNIT();
            keyEntity.UNIT_CD = unitCd;
            return this.unitDao.GetAllValidData(keyEntity);
        }
        ///// <summary>
        ///// 領収書管理取得
        ///// </summary>
        ///// <param name="hiduke">日付</param>
        ///// <param name="densyuKbnCd">伝種区分</param>
        ///// <param name="kyotenCd">拠点CD</param>
        ///// <returns></returns>
        //public S_NUMBER_RECEIPT GetNumberReceipt(DateTime hiduke, short densyuKbnCd, short kyotenCd)
        //{
        //    S_NUMBER_RECEIPT keyEntity = new S_NUMBER_RECEIPT();
        //    if (hiduke == null)
        //    {
        //        return null;
        //    }
        //    keyEntity.NUMBERED_DAY = hiduke.Date;
        //    keyEntity.DENSHU_KBN_CD = (SqlInt16)densyuKbnCd;
        //    keyEntity.KYOTEN_CD = (SqlInt16)kyotenCd;

        //    var numberReceips = this.numberReceiptDao.GetDataForEntity(keyEntity);
        //    if (numberReceips == null
        //        || numberReceips.Length < 1)
        //    {
        //        return null;
        //    }

        //    return numberReceips[0];
 
        //}

        ///// <summary>
        ///// 領収書管理追加
        ///// </summary>
        ///// <param name="taergetEntity"></param>
        //public void InsertNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        //{
        //    this.numberReceiptDao.Insert(targetEntity);
        //}

        ///// <summary>
        ///// 領収書管理更新
        ///// </summary>
        ///// <param name="targetEntity"></param>
        //public void UpdateNumberReceipt(S_NUMBER_RECEIPT targetEntity)
        //{
        //    this.numberReceiptDao.Update(targetEntity);
        //}

        /// <summary>
        /// 荷姿単位取得
        /// </summary>
        /// <returns></returns>
        public M_NISUGATA[] GetAllNisugata()
        {
            M_NISUGATA keyEntity = new M_NISUGATA();
            return this.nisugataDao.GetAllValidData(keyEntity);
        }


        /// <summary>
        /// 会社情報取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <returns></returns>
        public M_CORP_INFO GetCorpInfo(SqlInt16 SYS_ID)
        {
            if (SYS_ID < 0)
            {
                return null;
            }

            M_CORP_INFO keyEntity = new M_CORP_INFO();
            keyEntity.SYS_ID = SYS_ID;
            var result = this.corpInfoDao.GetAllValidData(keyEntity);
            if (result == null || result.Length < 1)
            {
                return new M_CORP_INFO();
            }

            return result[0];
        }
        #endregion

        #region Utility
        #endregion
    }
}
