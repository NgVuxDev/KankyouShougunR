using r_framework.Entity;

namespace r_framework.Dto
{
    /// <summary>
    /// メニュー権限情報Dto
    /// </summary>
    public class MenuAuthDto
    {
        /// <summary>
        /// コンストラクタ
        /// 全権限をtrue（許可）で初期化
        /// </summary>
        public MenuAuthDto()
        {
            this.CanRead = true;
            this.CanAdd = true;
            this.CanEdit = true;
            this.CanDelete = true;
        }
        
        /// <summary>
        /// 読込権限
        /// </summary>
        public bool CanRead { get; private set; }

        /// <summary>
        /// 追加権限
        /// </summary>
        public bool CanAdd { get; private set; }

        /// <summary>
        /// 編集権限
        /// </summary>
        public bool CanEdit { get; private set; }

        /// <summary>
        /// 削除権限
        /// </summary>
        public bool CanDelete { get; private set; }

        /// <summary>
        /// 権限情報設定
        /// </summary>
        /// <param name="mMenuAuth">メニュー権限エンティティ</param>
        public void SetAuth(M_MENU_AUTH mMenuAuth)
        {
            this.CanRead = mMenuAuth == null ? true : mMenuAuth.AUTH_READ.IsTrue;
            this.CanAdd = mMenuAuth == null ? true : mMenuAuth.AUTH_ADD.IsTrue;
            this.CanEdit = mMenuAuth == null ? true : mMenuAuth.AUTH_EDIT.IsTrue;
            this.CanDelete = mMenuAuth == null ? true : mMenuAuth.AUTH_DELETE.IsTrue;
        }
    }
}
