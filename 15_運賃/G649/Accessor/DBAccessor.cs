// $Id: DBAccessor.cs 3143 2013-10-09 02:26:33Z takeda $
using System.Linq;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Accessor
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
        /// 取引先のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
        }
        #endregion

        #region DBアクセッサ
        /// <summary>
        /// 業者CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="gyoushaCd">略名を取得したい取引先のCD</param>
        /// <returns name="string">業者略名</returns>
        internal string GetName(string gyoushaCd)
        {
            string returnval = string.Empty;

            // 取得したentityより略名を取得
            M_GYOUSHA entity = this.gyoushaDao.GetDataByCd(gyoushaCd);
            if (entity != null)
            {
                returnval = entity.GYOUSHA_NAME_RYAKU;
            }

            return returnval;
        }

        /// <summary>
        /// 業者を取得
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousya(string gyoushaCd)
        {
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;
            return this.gyoushaDao.GetAllValidData(entity).FirstOrDefault();
        }

        /// <summary>
        /// 運搬業者を取得
        /// </summary>
        /// <param name="gyoushaCd">GYOSHA_CD</param>
        /// <returns></returns>
        public M_GYOUSHA GetUnpanGyousya(string gyoushaCd)
        {
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;
            return this.gyoushaDao.GetUnpanGyoushaData(entity);
        }
        #endregion
    }
}
