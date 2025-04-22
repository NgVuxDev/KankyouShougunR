using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.Logic;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// </summary>
    public class DBAccessor
    {
        #region フィールド

        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        r_framework.Dao.IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IS_NUMBER_YEARDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao numberYearDao;

        /// <summary>
        /// IS_NUMBER_DAYDao
        /// </summary>
        Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao numberDayDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// IM_KOBETSU_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao kobetsuHinmeiTankaDao;

        /// <summary>
        /// IM_KIHON_HINMEI_TANKADao
        /// </summary>
        r_framework.Dao.IM_KIHON_HINMEI_TANKADao kihonHinmeiTankaDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            this.numberYearDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_YEARDao>();
            this.numberDayDao = DaoInitUtility.GetComponent<Shougun.Function.ShougunCSCommon.Dao.IS_NUMBER_DAYDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.kobetsuHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KOBETSU_HINMEI_TANKADao>();
            this.kihonHinmeiTankaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KIHON_HINMEI_TANKADao>();
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
            entity.NUMBERED_DAY = numberedDay;
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
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemId()
        {
            SqlInt64 returnInt = 1;

            using (Transaction tran = new Transaction())
            {
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;

                // テーブルロックをかけつつ、既存データがあるかを検索する。
                var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
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

                // コミット
                tran.Commit();
            }

            return returnInt;
        }

        /// <summary>
        /// 伝種区分別番号採番処理
        /// S_NUMBER_DENSHUから指定された伝種区分CDの最新の番号 + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createDenshuNumber()
        {
            SqlInt64 returnInt = -1;

            using (Transaction tran = new Transaction())
            {
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;

                // テーブルロックをかけつつ、既存データがあるかを検索する。
                var updateEntity = this.numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
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

                // コミット
                tran.Commit();
            }

            return returnInt;
        }

        #endregion
    }
}