using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.Dao;
using r_framework.Logic;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
{
    /// <summary>
    /// 入金消込用DBアクセスクラス
    /// </summary>
    internal class DBAccessor
    {
        #region フィールド
        /// <summary>入金消込DAO</summary>
        internal DAOClass dao;

        /// <summary>入金入力DAO</summary>
        internal NyuukinEntryDAOClass nyuukinEntryDao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        r_framework.Dao.IS_NUMBER_SYSTEMDao numberSystemDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal DBAccessor()
        {
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.nyuukinEntryDao = DaoInitUtility.GetComponent<NyuukinEntryDAOClass>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
        }
        #endregion

        #region アクセッサ

        #region 入金消込データ取得(キー：入金番号)
        /// <summary>
        /// 入金消込を取得する。
        /// </summary>
        /// <param name="nyuukinNumber"></param>
        /// <returns>count</returns>
        internal T_NYUUKIN_KESHIKOMI[] GetNyuukinKeshikomi(string nyuukinNumber)
        {
            T_NYUUKIN_KESHIKOMI[] retval = null;

            T_NYUUKIN_KESHIKOMI entity = new T_NYUUKIN_KESHIKOMI();
            entity.NYUUKIN_NUMBER = long.Parse(nyuukinNumber);
            entity.DELETE_FLG = false;
            retval = this.dao.GetDataForEntity(entity);
            return retval;
        }
        #endregion

        #region 入金消込データ取得(キー：SYSTEM_ID, 入金番号)
        /// <summary>
        /// 入金消込テーブルの情報を取得
        /// </summary>
        /// <returns></returns>
        internal T_NYUUKIN_KESHIKOMI GetNyukinKeshikomi(int systemId, int SeikyuNumber)
        {
            T_NYUUKIN_KESHIKOMI[] retval = null;

            T_NYUUKIN_KESHIKOMI entity = new T_NYUUKIN_KESHIKOMI();
            entity.SYSTEM_ID = systemId;
            entity.SEIKYUU_NUMBER = SeikyuNumber;
            entity.DELETE_FLG = false;
            retval = this.dao.GetDataForEntity(entity);

            if (retval == null || retval.Length < 1)
            {
                return null;
            }
            else
            {
                return retval[0];
            }
        }
        #endregion

        #region 入金消込データ取得(キー：入金番号、取引先CD)
        /// <summary>
        /// 入金消込を取得する。
        /// </summary>
        /// <param name="nyuukinNumber"></param>
        /// <returns>count</returns>
        internal T_NYUUKIN_KESHIKOMI[] GetNyuukinKeshikomi(string nyuukinNumber, string TorihikisakiCD)
        {
            T_NYUUKIN_KESHIKOMI[] retval = null;

            T_NYUUKIN_KESHIKOMI entity = new T_NYUUKIN_KESHIKOMI();
            entity.NYUUKIN_NUMBER = long.Parse(nyuukinNumber);
            entity.TORIHIKISAKI_CD = TorihikisakiCD;
            entity.DELETE_FLG = false;
            retval = this.dao.GetDataForEntity(entity);
            return retval;
        }
        #endregion

        #region 入金消込データ取得(キー：システムID、請求番号、鑑番号　論理削除済含む)
        /// <summary>
        /// 入金消込を取得する。
        /// </summary>
        /// <param name="nyuukinNumber"></param>
        /// <returns>count</returns>
        internal T_NYUUKIN_KESHIKOMI[] GetNyuukinKeshikomi(SqlInt64 systemId, SqlInt64 seikyuuNumber, SqlInt32 kagamiNumber)
        {
            T_NYUUKIN_KESHIKOMI[] retval = null;

            T_NYUUKIN_KESHIKOMI entity = new T_NYUUKIN_KESHIKOMI();
            entity.SYSTEM_ID = systemId;
            entity.SEIKYUU_NUMBER = seikyuuNumber;
            entity.KAGAMI_NUMBER = kagamiNumber;
            retval = this.dao.GetDataForEntity(entity);
            return retval;
        }
        #endregion

        #region 消込明細取得(対象SYSTEM_ID以外のデータ)
        /// <summary>
        /// 消込明細を取得する
        /// </summary>
        /// <returns></returns>
        internal DataTable GetKeshikomi(string strTorihikisakiCd, string strDenpyouDate, string nyuukinNumber)
        {
            DataTable data = this.dao.GetKeshikomi(strTorihikisakiCd, strDenpyouDate, nyuukinNumber);
            return data;
        }
        #endregion

        //#region 消込明細取得(キー：入金先CD)
        ///// <summary>
        ///// 消込明細を入金先CDで取得する
        ///// </summary>
        ///// <returns></returns>
        //internal DataTable GetKeshikomiByNyuukinsakiCd(string strNyuukinsakiCd, string strDenpyouDate)
        //{
        //    DataTable data = this.dao.GetKeshikomiByNyuukinsakiCd(strNyuukinsakiCd, strDenpyouDate);
        //    return data;
        //}
        //#endregion

        #region 入金消込.KESHIKOMI_SEQのMAX値取得
        internal int GetNyuukinKeshikomiMaxKeshikomiSeq(SqlInt64 systemId, SqlInt64 seikyuuNumber)
        {
            if (systemId.IsNull || seikyuuNumber.IsNull)
            {
                return 1;
            }

            return this.dao.GetKeshikomiMaxSeq((long)systemId, (long)seikyuuNumber);
        }
        #endregion

        #region 入金入力データ取得
        internal List<T_NYUUKIN_ENTRY> GetNyuukinEntryList(string sumSystemId)
        {
            List<T_NYUUKIN_ENTRY> returnVal = new List<T_NYUUKIN_ENTRY>();

            long sumSysId = 0;
            if (long.TryParse(sumSystemId, out sumSysId))
            {
                returnVal = this.nyuukinEntryDao.GetNyuukinEntryList(sumSysId);
            }

            return returnVal;
        }
        #endregion

        #region 入金消込データ挿入
        /// <summary>
        /// 入金消込を追加する。
        /// </summary>
        /// <param name="data"></param>
        /// <returns>count</returns>
        internal int insertNyuukinKeshikomi(T_NYUUKIN_KESHIKOMI data)
        {
            int count = this.dao.Insert(data);
            return count;
        }
        #endregion

        #region 入金消込データ更新
        /// <summary>
        /// 入金消込を更新する。
        /// </summary>
        /// <param name="data"></param>
        /// <returns>count</returns>
        internal int UpdateNyuukinKeshikomi(T_NYUUKIN_KESHIKOMI data)
        {
            int count = this.dao.Update(data);
            return count;
        }
        #endregion

        //#region 一番大きいsystem_idを取得
        ///// <summary>
        ///// 一番大きいsystem_idを取得
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns>maxSystemId</returns>
        //internal long GetKeshikomiMaxSystemId()
        //{
        //    long maxSystemId = this.dao.GetKeshikomiMaxSystemId();
        //    return maxSystemId;
        //}
        //#endregion

        #region 指定データを削除
        /// <summary>
        /// 指定データを削除
        /// </summary>
        /// <param name="data"></param>
        internal void DeleteDataByCd(long systemId, long seikyuuNumber, int keshikomiSeq)
        {
            this.dao.DeleteDataByCd(systemId, seikyuuNumber, keshikomiSeq);
        }
        #endregion

        #region SYSTEM_ID採番処理
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// S_NUMBER_SYSTEMから指定された伝種区分CDの最新のID + 1の値を返す
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>採番した数値</returns>
        public SqlInt64 createSystemId(SqlInt16 denshuKbnCd)
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = denshuKbnCd;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = denshuKbnCd;
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                this.numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion

        #endregion

    }
}
