// $Id: DBAccessor.cs 55238 2015-07-09 09:21:12Z miya@e-mall.co.jp $
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
using Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku.Accessor
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

        // No.4578-->
        // 20150412 go 在庫品名DAO Start
        /// <summary>
        /// IM_ZAIKO_HINMEIDao
        /// </summary>
        r_framework.Dao.IM_ZAIKO_HINMEIDao zaikoHinmeiDao;
        // 20150412 go 在庫品名DAO End
        // No.4578<--

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IT_SHUKKA_ENTRYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKA_ENTRYDao shukkaEntryDao;

        /// <summary>
        /// IT_SHUKKA_DETAILDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKA_DETAILDao shukkaDetailDao;

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

        /// <summary>IT_NYUUKIN_SUM_ENTRYDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_ENTRYDao nyuukinSumEntryDao;

        /// <summary>IT_NYUUKIN_SUM_DETAILDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_DETAILDao nyuukinSumDetailDao;

        /// <summary>IT_NYUUKIN_ENTRYDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_ENTRYDao nyuukinEntryDao;

        /// <summary>IT_NYUUKIN_DETAILDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_DETAILDao nyuukinDetailDao;

        /// <summary>IT_SHUKKIN_ENTRYDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_ENTRYDao shukkinEntryDao;

        /// <summary>IT_SHUKKIN_DETAILDao</summary>
        Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_DETAILDao shukkinDetailDao;

        /// <summary>
        /// 検収明細DAO
        /// </summary>
        TKSDClass tksdDao;

        /// <summary>
        /// 取引先_請求情報DAO
        /// </summary>
        MTSeiClass mtSeiDao;

        /// <summary>
        /// 取引先_支払情報DAO
        /// </summary>
        MTShihaClass mtShihaDao;

        /// <summary>
        /// 在庫情報DAO
        /// </summary>
        TZSDClass tzsdDao;

        // No.4578-->
        // 20150415 在庫品名振分追加(G051と同様) Start
        /// <summary>
        /// 在庫品名振分
        /// </summary>
        TZHHClass tzhhDao;
        // 20150415 在庫品名振分追加 End
        // No.4578<--

        /// <summary>
        /// 受付（出荷）入力DAO
        /// </summary>
        TUSKEClass tuskeDao;

        /// <summary>
        /// 受付（出荷）明細DAO
        /// </summary>
        TUSKDClass tuskdDao;

        /// <summary>
        /// IM_SHASHUDao
        /// </summary>
        r_framework.Dao.IM_SHASHUDao shashuDao;

        TKEClass tkeDao;

        TKDClass tkdDao;

        // No.4578-->
        // G051と同様修正
        // 20150415 go 既存IM在庫品名DAOを利用するように改修 Start
        //MZHClass mzhDao;
        // 20150415 go 既存IM在庫品名DAOを利用するように改修 End

        // 20150415 go 在庫比率DAO追加 Start
        MZHIClass mzhiDao;
        // 20150415 go 在庫比率DAO追加 End
        // No.4578<--

        /// <summary>
        /// 形態区分DAO
        /// </summary>
        MKKClass mkkDao;

        /// <summary>
        /// 出荷番号DAO
        /// </summary>
        TSEClass tseDao;

        /// <summary>
        /// IM_SHOUHIZEIDao
        /// </summary>
        r_framework.Dao.IM_SHOUHIZEIDao shouhizeiDao;

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        r_framework.Dao.IM_KOBETSU_HINMEIDao kobetsuHinmei;
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTanka;

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
            // No.4578-->
            // 20150412 go 在庫品名DAO Start
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_ZAIKO_HINMEIDao>();
            // 20150412 go 在庫品名DAO End
            // No.4578<--
            this.shukkaEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKA_ENTRYDao>();
            this.shukkaDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKA_DETAILDao>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.manifestEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_MANIFEST_ENTRYDao>();
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
            this.nyuukinEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_ENTRYDao>();
            this.nyuukinDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_DETAILDao>();
            this.shukkinEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_ENTRYDao>();
            this.shukkinDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_SHUKKIN_DETAILDao>();
            this.mtSeiDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MTSeiClass>();
            this.mtShihaDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MTShihaClass>();
            this.tzsdDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TZSDClass>();
            // No.4578-->
            // 20150415 go 在庫品名振分処理追加 Start
            this.tzhhDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TZHHClass>();
            // 20150415 go 在庫品名振分処理追加 End
            // No.4578<--
            this.tuskeDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TUSKEClass>();
            this.tuskdDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TUSKDClass>();
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();
            this.tkeDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TKEClass>();
            this.tkdDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TKDClass>();
            //this.mzhDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MZHClass>();
            // No.4578-->
            // 20150415 go 在庫品名DAO削除 Start
            //this.mzhDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MZHClass>();
            // 20150415 go 在庫品名DAO削除 End
            // 20150415 go 在庫比率DAO追加 Start
            this.mzhiDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MZHIClass>();
            // 20150415 go 在庫比率DAO追加 End
            // No.4578<--
            this.tksdDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TKSDClass>();
            this.mkkDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.MKKClass>();
            this.tseDao = DaoInitUtility.GetComponent<Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO.TSEClass>();
            this.nyuukinSumEntryDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_ENTRYDao>();
            this.nyuukinSumDetailDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IT_NYUUKIN_SUM_DETAILDao>();
            this.shouhizeiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHOUHIZEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.kobetsuHinmei = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEIDao>();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.kobetsuHinmeiTanka = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();
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
        /// 出荷入力を取得
        /// </summary>
        /// <param name="shukkaNumber">出荷番号</param>
        /// <returns></returns>
        public T_SHUKKA_ENTRY[] GetShukkaEntry(SqlInt64 shukkaNumber, string seq)
        {
            T_SHUKKA_ENTRY entity = new T_SHUKKA_ENTRY();
            entity.SHUKKA_NUMBER = shukkaNumber;
            entity.SEQ = SqlInt32.Parse(seq);
            return this.shukkaEntryDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 出荷入力の登録
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int InsertShukkaEntry(T_SHUKKA_ENTRY entity)
        {
            return this.shukkaEntryDao.Insert(entity);
        }

        /// <summary>
        /// 出荷入力の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateShukkaEntry(T_SHUKKA_ENTRY entity)
        {
            return this.shukkaEntryDao.Update(entity);
        }

        /// <summary>
        /// 出荷明細を取得
        /// </summary>
        /// <param name="entrySysId"></param>
        /// <param name="entrySeq"></param>
        /// <returns></returns>
        public T_SHUKKA_DETAIL[] GetShukkaDetail(SqlInt64 entrySysId, SqlInt32 entrySeq)
        {
            T_SHUKKA_DETAIL entity = new T_SHUKKA_DETAIL();
            entity.SYSTEM_ID = entrySysId;
            entity.SEQ = entrySeq;
            return shukkaDetailDao.GetDataForEntity(entity);
        }

        /// <summary>
        /// 出荷明細を登録
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int InsertShukkaDetail(T_SHUKKA_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var shukkaDetail in entitys)
            {
                returnint = returnint + this.shukkaDetailDao.Insert(shukkaDetail);
            }

            return returnint;
        }

        /// <summary>
        /// 出荷明細を更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public int UpdateShukkaDetail(T_SHUKKA_DETAIL[] entitys)
        {
            int returnint = 0;

            foreach (var shukkaDetail in entitys)
            {
                returnint = returnint + this.shukkaDetailDao.Update(shukkaDetail);
            }

            return returnint;
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
        /// 出荷入力用のSYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateSystemIdForShukka()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
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

        public SqlInt64 CreateSukkaNumber()
        {
            SqlInt64 returnInt = -1;

            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
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
        /// 運搬業者を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA[] GetUnpanGyousya(string gyoushaCd)
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
        /// 取得後、ShukkaDetailのキーでさらに絞り込んで使用してもらうため、
        /// あえてDataTableで返しています。
        /// </summary>
        /// <param name="shukkaDetails">連携しているT_SHUKKA_DETAIL</param>
        /// <returns>必要なキーが設定されていない場合はNullを返します。</returns>
        public DataTable GetManifestEntry(T_SHUKKA_DETAIL[] shukkaDetails)
        {
            if (shukkaDetails == null || shukkaDetails.Length < 1)
            {
                return null;
            }

            if (shukkaDetails[0].SYSTEM_ID.IsNull)
            {
                // 念のため先頭のキー値チェック
                return null;
            }

            // SQL文作成
            string whereStr = " AND RENKEI_DENSHU_KBN_CD = '" + SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA + "' AND RENKEI_SYSTEM_ID = '" + shukkaDetails[0].SYSTEM_ID + "' AND RENKEI_MEISAI_SYSTEM_ID IN ( ";

            // RENKEI_MEISAI_SYSTEM_ID用の条件句作成
            bool existRenkeiMeisaiId = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < shukkaDetails.Length; i++)
            {
                if (shukkaDetails[i].DETAIL_SYSTEM_ID.IsNull)
                {
                    continue;
                }

                if (i == 0)
                {
                    existRenkeiMeisaiId = true;
                    sb.Append("'" + shukkaDetails[i].DETAIL_SYSTEM_ID + "'");
                }
                else
                {
                    existRenkeiMeisaiId = true;
                    sb.Append(", '" + shukkaDetails[i].DETAIL_SYSTEM_ID + "'");
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
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
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
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
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
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, object strDenpyouDate, object sysDate, out bool catchErr, bool isCheckDelete = true)
        {
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                return null;
            }
        }

        /// <summary>
        /// 現場取得（削除済、適用期間外も含む）
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
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
        public M_GENBA[] GetGenbaByGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
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
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
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
            catchErr = false;
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
                catchErr = true;
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
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
        // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd, SqlDateTime strDenpyouDate)
        // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
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
            // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
            var sharyous = this.sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "2", strDenpyouDate, true, false, false);
            // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END
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
        /// 車輌取得
        /// </summary>
        /// <param name="sharyouCd"></param>
        /// <returns></returns>
        public M_SHARYOU[] GetSharyou(string sharyouCd, string gyoushaCd, string shasyuCd, string shainCd)
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

            var sharyous = this.sharyouDao.GetAllValidData(keyEntity);
            if (sharyous == null || sharyous.Length < 1)
            {
                return null;
            }

            return sharyous;
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
            M_YOUKI keyEntity = new M_YOUKI() { ISNOT_NEED_DELETE_FLG = true };
            return this.youkiDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 単位一覧取得
        /// </summary>
        /// <returns></returns>
        public M_UNIT[] GetAllUnit()
        {
            M_UNIT keyEntity = new M_UNIT() { ISNOT_NEED_DELETE_FLG = true };
            return this.unitDao.GetAllValidData(keyEntity);
        }

        /// <summary>
        /// 形態区分取得
        /// </summary>
        /// <param name="keitaiCbnCd"></param>
        /// <param name="containsDelete"></param>
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
            // 伝種区分：出荷
            keyEntity.DENPYOU_SHURUI_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
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
        /// 清算明細取得(出荷入力用)
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public DataTable GetSeisanMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd)
        {
            T_SEISAN_DETAIL keyEntity = new T_SEISAN_DETAIL();
            // 伝種区分：出荷
            keyEntity.DENPYOU_SHURUI_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
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
        /// 入金一括入力追加
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
        /// 入金一括明細追加
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

        // No.4578-->
        // 20150412 go 在庫品名取得処理 Start
        // 既存IM_ZAIKO_HINMEIDaoを利用するように改修
        ///// <summary>
        ///// 在庫品名取得
        ///// </summary>
        ///// <param name="zaikoHinmeiCd"></param>
        ///// <returns></returns>
        //internal string GetZaikoHinmei(string zaikoHinmeiCd)
        //{
        //    //キーチェック
        //    if (zaikoHinmeiCd == null)
        //    {
        //        return null;
        //    }
        //    M_ZAIKO_HINMEI tmpEntity = new M_ZAIKO_HINMEI();
        //    tmpEntity.ZAIKO_HINMEI_CD = zaikoHinmeiCd;
        //    tmpEntity = this.mzhDao.GetDataForEntity(tmpEntity);
        //    if (tmpEntity == null)
        //    {
        //        return null;
        //    }
        //    return tmpEntity.ZAIKO_HINMEI_NAME_RYAKU;
        //}
        /// <summary>
        /// 在庫品名取得
        /// </summary>
        /// <param name="zaikoHinmeiCd"></param>
        /// <returns></returns>
        internal M_ZAIKO_HINMEI[] GetAllValidZaikoHinmeiData(string zaikoHinmeiCd = null)
        {
            //キーチェック
            M_ZAIKO_HINMEI keyData = new M_ZAIKO_HINMEI();
            if (!string.IsNullOrEmpty(zaikoHinmeiCd))
            {
                keyData.ZAIKO_HINMEI_CD = zaikoHinmeiCd;
            }

            return this.zaikoHinmeiDao.GetAllValidData(keyData);
        }
        // 20150412 go 在庫品名取得処理 End
        // No.4578<--

        // No.4578-->
        // 20150409 go 在庫品名振分処理追加 Start
        /// <summary>
        /// 在庫品名振分
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
        /// 在庫比率
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal DataTable GetZaikoHiritsus(string hinmeiCd)
        {
            //キーチェック
            if (string.IsNullOrEmpty(hinmeiCd))
            {
                return null;
            }

            M_ZAIKO_HIRITSU tmpEntity = new M_ZAIKO_HIRITSU();
            tmpEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            tmpEntity.HINMEI_CD = hinmeiCd;

            return this.mzhiDao.GetZaikoHiritsuInfo(tmpEntity);
        }
        // 20150409 go 在庫品名振分処理追加 End
        // No.4578<--

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
        /// 締処理状況（在庫）取得
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        internal T_ZAIKO_SHUKKA_DETAIL GetZaikoShukkaData(long systemId, int seq)
        {
            T_ZAIKO_SHUKKA_DETAIL keyEntity = new T_ZAIKO_SHUKKA_DETAIL();
            // 伝種区分：出荷
            keyEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            keyEntity.SYSTEM_ID = systemId;
            keyEntity.SEQ = seq;

            return this.tzsdDao.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 検収明細取得
        /// </summary>
        /// <param name="sysId"></paparam>
        /// <param name="seq"></param>]
        internal List<T_KENSHU_DETAIL> GetKenshuDetail(SqlInt64 sysId, SqlInt32 seq)
        {
            List<T_KENSHU_DETAIL> returnVal = new List<T_KENSHU_DETAIL>();

            if (sysId.IsNull || seq.IsNull)
            {
                return returnVal;
            }

            T_KENSHU_DETAIL targetEntity = new T_KENSHU_DETAIL();
            targetEntity.SYSTEM_ID = sysId;
            targetEntity.SEQ = seq;

            return this.tksdDao.GetKenshuDetail(targetEntity);
        }
        /// <summary>
        /// 検収明細を登録
        /// </summary>
        /// <param name="targetEntity"></param>
        internal void InsertKenshuDetail(List<T_KENSHU_DETAIL> targetEntity)
        {
            foreach (T_KENSHU_DETAIL entity in targetEntity)
            {
                // キーチェック
                if (entity == null
                    || entity.SYSTEM_ID.IsNull
                    || entity.SEQ.IsNull
                    || entity.DETAIL_SYSTEM_ID.IsNull
                    || entity.KENSHU_SYSTEM_ID.IsNull
                    )
                {
                    continue;
                }

                this.tksdDao.Insert(entity);
            }
        }

        /// <summary>
        /// 検収明細を更新
        /// </summary>
        /// <param name="targetEntity"></param>
        internal void UpdateKenshuDetail(List<T_KENSHU_DETAIL> targetEntity)
        {
            foreach (T_KENSHU_DETAIL entity in targetEntity)
            {
                // キーチェック
                if (entity == null
                    || entity.SYSTEM_ID.IsNull
                    || entity.SEQ.IsNull
                    || entity.DETAIL_SYSTEM_ID.IsNull
                    || entity.KENSHU_SYSTEM_ID.IsNull
                    )
                {
                    return;
                }

                this.tksdDao.Update(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertZaikoShukkaDetails(Dictionary<Row, List<T_ZAIKO_SHUKKA_DETAIL>> targetEntityLists)
        {
            //// 値を配列に変換する
            //List<T_ZAIKO_SHUKKA_DETAIL>[] vals = new List<T_ZAIKO_SHUKKA_DETAIL>[targetEntityLists.Values.Count];
            //targetEntityLists.Values.CopyTo(vals, 0);

            //foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityLst in vals)
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
        /// 在庫明細_出荷を更新
        /// </summary>
        /// <param name="targetEntityLists"></param>
        //internal void UpdateZaikoShukkaDetail(List<List<T_ZAIKO_SHUKKA_DETAIL>> targetEntity)
        internal void UpdateZaikoShukkaDetails(Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_SHUKKA_DETAIL>> targetEntityLists)
        {
            //foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityLst in targetEntity)
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

        // No.4578-->
        // 20150415 go 在庫品名振分処理追加(処理後のG051からコピー) Start
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void InsertZaikoHinmeiHuriwakes(Dictionary<Row, List<T_ZAIKO_HINMEI_HURIWAKE>> targetEntityLists)
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
        // 20150415 go 在庫品名振分処理追加 End

        // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する Start
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetEntityLists"></param>
        internal void UpdateZaikoHinmeiHuriwakes(Dictionary<T_SHUKKA_DETAIL, List<T_ZAIKO_HINMEI_HURIWAKE>> targetEntityLists)
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
        // 20150522 go 運賃不具合一覧No.57と伴に、在庫関連に横展開する End
        // No.4578<--

        /// <summary>
        /// 受付（出荷）テーブル取得
        /// </summary>
        /// <param name="uketsukeNum"></param>
        /// <returns></returns>
        internal DataTable GetUketsukeSK(string uketsukeNum)
        {
            //キーチェック
            if (string.IsNullOrEmpty(uketsukeNum))
            {
                return null;
            }

            return this.tuskeDao.GetUketsukeSKData(uketsukeNum);
        }

        /// <summary>
        /// 受付(出荷)の連携データを取得
        /// </summary>
        /// <param name="uketsukeNum">検索対象の受付番号</param>
        /// <param name="filteringNum">除外したい出荷番号</param>
        /// <returns></returns>
        internal DataTable GetUketsukeSKRenkei(string uketsukeNum, string filteringNum)
        {
            // キーチェック
            if (string.IsNullOrEmpty(uketsukeNum))
            {
                return null;
            }

            return this.tuskeDao.GetUketsukeSKRenkeiData(uketsukeNum, CommonConst.DENSHU_KBN_SHUKKA, filteringNum);
        }

        /// <summary>
        /// 出荷受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>出荷受付入力エンティティ</returns>
        internal T_UKETSUKE_SK_ENTRY GetUketsukeSkEntry(string systemId, string seq)
        {
            return this.tuskeDao.GetDataByKey(systemId, seq);
        }

        /// <summary>
        /// 有効な出荷受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <returns>出荷受付入力エンティティ</returns>
        internal T_UKETSUKE_SK_ENTRY GetUketsukeSkEntry(string systemId)
        {
            return this.tuskeDao.GetDataBySystemId(systemId);
        }

        /// <summary>
        /// 出荷受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSkEntry">追加する出荷受付入力エンティティ</param>
        /// <returns>追加した件数</returns>
        internal int InsertUketsukeSkEntry(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry)
        {
            return this.tuskeDao.Insert(tUketsukeSkEntry);
        }

        /// <summary>
        /// 出荷受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSkEntry">更新する出荷受付入力エンティティ</param>
        /// <returns>更新した件数</returns>
        internal int UpdateUketsukeSkEntry(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry)
        {
            return this.tuskeDao.Update(tUketsukeSkEntry);
        }

        /// <summary>
        /// 出荷受付詳細エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>取得した出荷受付詳細エンティティリスト</returns>
        internal List<T_UKETSUKE_SK_DETAIL> GetUketsukeSkDetail(string systemId, string seq)
        {
            return this.tuskdDao.GetDataByKey(systemId, seq);
        }

        /// <summary>
        /// 出荷受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加する出荷受付詳細エンティティ</param>
        /// <returns>追加した件数</returns>
        internal int InsertUketsukeSkDetail(T_UKETSUKE_SK_DETAIL tUketsukeSkDetail)
        {
            return this.tuskdDao.Insert(tUketsukeSkDetail);
        }

        /// <summary>
        /// 計量データ取得
        /// </summary>
        /// <param name="keiryouNum"></param>
        /// <returns></returns>
        internal DataTable GetKeiryou(string keiryouNum)
        {
            //キーチェック
            if (string.IsNullOrEmpty(keiryouNum))
            {
                return null;
            }

            return this.tkeDao.GetKeiryouData(keiryouNum);
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
            if (keitaiKbnCd == 0)
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
        /// 指定された出荷番号の次に大きい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber"></param>
        /// <param name="KyotenCD"></param>
        /// <returns></returns>
        internal long GetNextShukkaNumber(long ShukkaNumber, string KyotenCD)
        {
            // No.3341-->
            //キーチェック
            //if (ShukkaNumber == 0)
            //{
            //    return 0;
            //}
            // No.3341<--

            SqlInt64 returnValue = this.tseDao.GetNextShukkaNumber(SqlInt64.Parse(ShukkaNumber.ToString()), KyotenCD);
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
        /// 指定された出荷番号の次に小さい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber"></param>
        /// <param name="KyotenCD"></param>
        /// <returns></returns>
        internal long GetPreShukkaNumber(long ShukkaNumber, string KyotenCD)
        {
            //キーチェック
            if (ShukkaNumber == 0)
            {
                return 0;
            }

            SqlInt64 returnValue = this.tseDao.GetPreShukkaNumber(SqlInt64.Parse(ShukkaNumber.ToString()), KyotenCD);
            if (!returnValue.IsNull)
            {
                return long.Parse(returnValue.ToString());
            }
            else
            {
                return 0;
            }
        }

        // No.1767
        /// <summary>
        /// 登録された一番大きい出荷番号を取得
        /// </summary>
        /// <returns></returns>
        internal long GetMaxShukkaNumber()
        {
            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);

            SqlInt64 returnValue = this.numberDenshuDao.GetMaxPlusKey(entity) - 1;
            if (!returnValue.IsNull)
            {
                return long.Parse(returnValue.ToString());
            }
            else
            {
                return 0;
            }
        }

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

            if (returnValue == null)
            {
                keyEntity.GENBA_CD = "";
                returnValue = this.kobetsuHinmei.GetDataByHinmei(keyEntity, hinmei);
            }

            return returnValue;

        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end
        #endregion

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

        /// <summary>
        /// 個別品名テーブルの情報を取得
        /// </summary>
        /// <param name="key">品名CD</param>
        /// <returns></returns>
        public M_KOBETSU_HINMEI_TANKA GetKobetsuHinmeiTankaDataByCd(string torihikisakiCd, string gyoushaCd, string genbaCd, string hinmeiCd, string date)
        {
            M_KOBETSU_HINMEI_TANKA keyEntity = new M_KOBETSU_HINMEI_TANKA();
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.HINMEI_CD = hinmeiCd;
            DateTime Date = Convert.ToDateTime(date);

            M_KOBETSU_HINMEI_TANKA returnValue = this.kobetsuHinmeiTanka.GetDataForHinmei(keyEntity, Date);

            return returnValue;

        }
    }
}