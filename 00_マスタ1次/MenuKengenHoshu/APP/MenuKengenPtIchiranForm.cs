// $Id: MenuKengenPtIchiranForm.cs 36342 2014-12-02 07:51:20Z sanbongi $
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MenuKengenHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Menu;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace MenuKengenHoshu.APP
{
    /// <summary>
    /// メニュー権限パターン一覧画面
    /// </summary>
    public partial class MenuKengenPtIchiranForm : SuperForm
    {
        // Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopFormで画面を呼び出すため
        // FWは「r_framework」を使用する(r_frameworkは使用しない)

        #region - Fields -

        /// <summary>
        /// メニュー権限パターン一覧画面ロジック
        /// </summary>
        private MenuKengenPtIchiranLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        #region - Properties -

        /// <summary>
        /// パターンID
        /// </summary>
        /// <remarks>
        /// 呼出元画面に選択結果を返すプロパティ
        /// </remarks>
        public long PATTERN_ID { get; private set; }

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="menuItems">メニューアイテムリスト</param>
        public MenuKengenPtIchiranForm(List<MenuItemComm> menuItems)
            : base(r_framework.Const.WINDOW_ID.M_MENU_AUTH_PATTERN_ICHIRAN, r_framework.Const.WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            this.logic = new MenuKengenPtIchiranLogic(this);
            this.logic.MenuItems = menuItems;
        }

        #endregion

        #region - OnLoad -

        /// <summary>
        /// 画面Loadイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic.WindowInit();
        }

        #endregion

        #region - Function Event -

        /// <summary>
        /// F3 適用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Tekiyo(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();

            var dialogResult = messageShowLogic.MessageBoxShow("C046", "パターンを適用");
            if (DialogResult.Yes == dialogResult)
            {
                var patternId = this.logic.GetPatternID();
                if (0 < patternId)
                {
                    // 取得結果を設定
                    this.PATTERN_ID = patternId;

                    var parentForm = (BasePopForm)this.Parent;
                    parentForm.DialogResult = DialogResult.OK;
                }

                this.FormClose(sender, e);
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Sakujo(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();

            var dialogResult = messageShowLogic.MessageBoxShow("C026");
            if (DialogResult.Yes == dialogResult)
            {
                bool result = this.logic.Delete();

                if (result)
                {
                    // 再検索
                    int count = this.logic.Search();
                    if (count < 0)
                    {
                        return;
                    }

                    // 完了メッセージ表示
                    messageShowLogic.MessageBoxShow("I001", "削除");
                }
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Clear(object sender, EventArgs e)
        {
            this.logic.Clear();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Search(object sender, EventArgs e)
        {
            this.logic.Search();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BasePopForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        #endregion

        #region - Ichiran Event -

        /// <summary>
        /// パターン一覧SelectionChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_PtEntry_SelectionChanged(object sender, EventArgs e)
        {
            bool catchErr = this.logic.SearchPtDetail();
            if (catchErr)
            {
                return;
            }

            this.logic.SetIchiranDetail();
        }

        /// <summary>
        /// パターン一覧CellDoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_PtEntry_CellDoubleClick(object sender, CellEventArgs e)
        {
            this.Tekiyo(sender, e);
        }

        private void Ichiran_PtDetail_RowEnter(object sender, CellEventArgs e)
        {
            Ichiran_PtDetail.Refresh();
        }

        #endregion
    }
}
