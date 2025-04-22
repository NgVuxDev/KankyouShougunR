

namespace r_framework.Logic
{
    /// <summary>
    /// Logicクラスのインタフェース
    /// </summary>
    public interface IBuisinessLogic
    {
        /// <summary>
        /// 検索処理
        /// </summary>
        int Search();

        /// <summary>
        /// 登録処理
        /// </summary>
        void Regist(bool errorFlag);

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update(bool errorFlag);

        /// <summary>
        /// 論理削除処理
        /// </summary>
        void LogicalDelete();

        /// <summary>
        /// 物理削除処理
        /// </summary>
        void PhysicalDelete();
    }
}
