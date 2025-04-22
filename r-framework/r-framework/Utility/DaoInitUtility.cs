using r_framework.Const;
using r_framework.Dao;
using Seasar.Framework.Container.Factory;
namespace r_framework.Utility
{
    /// <summary>
    /// Dao初期化クラス
    /// </summary>
    public static class DaoInitUtility
    {
        /// <summary>
        /// 指定したDaoをS2Containerから取得し返却
        /// </summary>
        public static T GetComponent<T>(string connectionID = Constans.DAO) where T : IS2Dao
        {
            return (T)SingletonS2ContainerFactory.Container.GetComponent(typeof(T));
        }
    }
}
