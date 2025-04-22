using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.ManifestPattern.DBAccesser
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

        ///// <summary>
        ///// IT_UKEIRE_ENTRYDao
        ///// </summary>
        //Shougun.Core.Common.KensakuKekkaIchiran.DAO.DAOClass myDao;

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            //myDao = DaoInitUtility.GetComponent<Shougun.Core.Common.KensakuKekkaIchiran.DAO.DAOClass>();

        }
        #endregion

        //#region DBアクセッサ

        ///// <summary>
        ///// SYS_INFOを取得する
        ///// </summary>
        ///// <returns></returns>
        //public M_SYS_INFO GetSysInfo()
        //{
        //    // TODO: ログイン時に共通メンバでSYS_INFOの情報を保持する可能性があるため、
        //    //       その場合、このメソッドは必要なくなる。
        //    M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
        //    return returnEntity[0];
        //}
        //#endregion
    }
}
