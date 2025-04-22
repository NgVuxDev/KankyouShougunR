using r_framework.Const;
using r_framework.Dao;
using Seasar.Framework.Container.Factory;
namespace r_framework.Utility
{
    /// <summary>
    /// Dao初期化クラス
    /// </summary>
    public class DaoInitUtilityLOG
    {
        /// <summary>
        /// ファイルアップロード用Daoの生成
        /// </summary>
        public static T GetComponent<T>() where T : IS2Dao
        {
            return DaoInitUtility.GetComponent<T>(Constans.DAO_LOG);
        }
    }
}