// $Id: MenuKengenPtTorokuForm.cs 36727 2014-12-08 07:16:52Z sanbongi $
using System;
using System.Collections.Generic;
using MenuKengenHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Entity;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace MenuKengenHoshu.APP
{
    /// <summary>
    /// メニュー権限パターン登録画面
    /// </summary>
    [Implementation]
    public partial class MenuKengenPtTorokuForm : SuperForm
    {
        #region - Fields -

        /// <summary>
        /// メニュー権限パターン登録処理
        /// </summary>
        private MenuKengenPtTorokuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loginUser">ログインユーザー名</param>
        /// <param name="menuAuthPtDetailList">メニュー権限パターン詳細リスト</param>
        /// <param name="patternID">パターンID</param>
        public MenuKengenPtTorokuForm(string loginUserName, List<M_MENU_AUTH_PT_DETAIL> menuAuthPtDetailList, long patternID)
        {
            InitializeComponent();

            this.logic = new MenuKengenPtTorokuLogic(this);
            this.logic.LoginUserName = loginUserName;
            this.logic.MenuAuthPtDetailList = menuAuthPtDetailList;
            this.logic.PatternID = patternID;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region - OnLoad -

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面初期化処理
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            this.ActiveControl = this.txt_PatternName;
        }

        #endregion

        #region - Function Event -

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Regist(object sender, System.EventArgs e)
        {
            this.logic.Regist(true);
        }

        /// <summary>
        /// フォーム閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
