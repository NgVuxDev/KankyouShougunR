// $Id: MenuKengenHoshuForm.cs 52897 2015-06-19 06:12:48Z miya@e-mall.co.jp $
using System;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using MenuKengenHoshu.Const;
using MenuKengenHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Windows.Forms;
using System.Drawing;

namespace MenuKengenHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class MenuKengenHoshuForm : SuperForm
    {
        #region - Fields -

        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private MenuKengenHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        public static bool processingFlg = false;

        /// <summary>
        /// メニュー区分
        /// </summary>
        public int MenuKbn = MenuKengenHoshuConstans.MENU_KBN_SINGLE;

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MenuKengenHoshuForm()
            : base(WINDOW_ID.M_MENU_AUTH_EACH_SHAIN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new MenuKengenHoshuLogic(this);

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

            // 画面表示変更処理
            this.logic.ChangeDisplay();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        #endregion

        private void MenuKengenHoshuForm_Shown(object sender, EventArgs e)
        {
            this.logic.SetIchiranCheckBoxEnabled();
        }

        #region - Function Event -

        /// <summary>
        /// F1 (個別/複数)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ModeChange(object sender, EventArgs e)
        {
            // メニュー区分変更処理
            this.logic.ChangeMenuKbn();
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            if (!this.logic.SearchCheck(false))
            {
                return;
            }

            this.logic.CSV();
        }

        /// <summary>
        /// F8 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (MenuKengenHoshuForm.processingFlg)
            {
                return;
            }

            try
            {
                MenuKengenHoshuForm.processingFlg = true;

                if (!this.logic.SearchCheck(true))
                {
                    return;
                }

                int count = this.logic.Search();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    return;
                }

                if (count > 0)
                {
                    this.logic.SetIchiran();
                }
            }
            finally
            {
                MenuKengenHoshuForm.processingFlg = false;
            }
        }

        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                if (!base.RegistErrorFlag)
                {
                    // 待機カーソル
                    this.Parent.Cursor = Cursors.WaitCursor;

                    // 必須チェック
                    bool check = this.logic.RegistCheck();
                    if(!check)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    bool catchErr = this.logic.CreateEntity();
                    if (catchErr)
                    {
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    if (base.RegistErrorFlag)
                    {
                        return;
                    }

                    // 再表示
                    if (this.MenuKbn == MenuKengenHoshuConstans.MENU_KBN_SINGLE)
                    {
                        // 検索ボタンが使用出来るのは個人の場合のみ
                        this.Search(sender, e);
                    }
                    else
                    {
                        // CreateEntityで修正した際に、DataSorceを設定しなおしてる所為で
                        // チェックボックス制御が初期されるため暫定で制御処理を再度呼び出す。
                        // CreateEntityでDataSorceを設定しなおさないようにすればここは不要。
                        catchErr = this.logic.SetIchiranCheckBoxEnabled();
                        if (catchErr)
                        {
                            return;
                        }
                    }

                    // ログインユーザー権限再設定
                    r_framework.Dto.SystemProperty.Shain.UpdateAuth(r_framework.Utility.EntityUtility.DataTableToEntity<r_framework.Entity.M_MENU_AUTH>(this.logic.SearchResult));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                // プログレスバーをリセット
                this.logic.ResetProgBar();
                // デフォルトカーソル
                this.Parent.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// F11 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            //this.logic.Cancel();
            this.logic.ClearCondition();
        }

        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            // 前回設定値保存
            bool catchErr = this.logic.SetPrevData();

            this.Close();
            parentForm.Close();
        }

        #endregion

        #region Sub Function Event

        /// <summary>
        /// パターン登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternRegist(object sender, EventArgs e)
        {
            if (this.Ichiran == null || this.Ichiran.Rows.Count == 0)
            {
                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                msg.MessageBoxShow("E061");
                return;
            }

            this.logic.PatternRegist();
        }

        /// <summary>
        /// パターン呼出処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatternCall(object sender, EventArgs e)
        {
            this.logic.PatternCall();
        }

        #endregion

        #region - Control Event -

        /// <summary>
        /// 部署CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUSHO_CD_TextChanged(object sender, EventArgs e)
        {
            // 一覧の初期化
            bool catchErr = this.logic.ClearIchiran();
            if (catchErr)
            {
                return;
            }

            // 社員CDの初期化
            if (string.IsNullOrEmpty(this.BUSHO_CD.Text))
            {
                this.SHAIN_CD.Text = string.Empty;
            }

            // ファンクションボタンを非活性
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            this.BUSHO_NAME_RYAKU.Text = string.Empty;
            if (this.BUSHO_CD.Text.Length >= 3)
            {
                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    return;
                }
                else
                {
                    //  部署名称の取得
                    var bushoName = string.Empty;
                    catchErr = false;
                    if (this.logic.GetBushoName(this.BUSHO_CD.Text, out bushoName, out catchErr) && !catchErr)
                    {
                        this.BUSHO_NAME_RYAKU.Text = bushoName;
                    }
                }
            }
        }

        /// <summary>
        /// 部署CD検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUSHO_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 社員CDの初期化
            if (!this.BUSHO_CD.Text.Equals(base.PreviousValue))
            {
                this.SHAIN_CD.Text = string.Empty;
            }

            // 空の場合、何もしない
            if (string.IsNullOrEmpty(this.BUSHO_CD.Text))
            {
                this.BUSHO_CD_POPUP.Text = string.Empty;
                return;
            }
            else
            {
                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    e.Cancel = true;
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "部署");
                }
                else
                {
                    this.BUSHO_CD_POPUP.Text = this.BUSHO_CD.Text;
                }
            }

            //  部署名称の取得
            var bushoName = string.Empty;
            bool catchErr = false;
            if (this.logic.GetBushoName(this.BUSHO_CD.Text, out bushoName, out catchErr) && !catchErr)
            {
                if (!this.BUSHO_CD.Text.Equals("999"))
                {
                    this.BUSHO_NAME_RYAKU.Text = bushoName;
                }
            }
            else
            {
                e.Cancel = true;
                if (!catchErr)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "部署");
                }
            }
        }

        /// <summary>
        /// 社員CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_TextChanged(object sender, EventArgs e)
        {
            // 一覧の初期化
            bool catchErr = this.logic.ClearIchiran();
            if (catchErr)
            {
                return;
            }

            // ファンクションボタンを非活性
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            this.SHAIN_NAME_RYAKU.Text = string.Empty;
            if (this.SHAIN_CD.Text.Length >= 6)
            {
                //  社員名称の取得
                var strShainName = string.Empty;
                var strBushoCD = this.BUSHO_CD.Text;

                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    strBushoCD = null;
                }

                if (this.logic.GetShainName(this.SHAIN_CD.Text, ref strBushoCD, out strShainName, out catchErr) && !catchErr)
                {
                    // No2661-->
                    this.BUSHO_CD_POPUP.Text = strBushoCD;
                    // No2661<--
                    this.BUSHO_CD.Text = strBushoCD;
                    this.SHAIN_NAME_RYAKU.Text = strShainName;
                }
            }
        }

        /// <summary>
        /// 社員CD検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 空の場合、何もしない
            if (string.IsNullOrEmpty(this.SHAIN_CD.Text))
            {
                return;
            }

            //  社員名称の取得
            var strShainName = string.Empty;
            var strBushoCD = this.BUSHO_CD.Text;
            
            if (this.BUSHO_CD.Text.Equals("999"))
            {
                strBushoCD = null;
            }

            bool catchErr = false;
            if (this.logic.GetShainName(this.SHAIN_CD.Text, ref strBushoCD, out strShainName, out catchErr) && !catchErr)
            {
                // No2661-->
                this.BUSHO_CD_POPUP.Text = strBushoCD;
                // No2661<--
                this.BUSHO_CD.Text = strBushoCD;
                this.SHAIN_NAME_RYAKU.Text = strShainName;
            }
            else
            {
                e.Cancel = true;
                if (!catchErr)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "社員");
                }
            }
        }

        /// <summary>
        /// 検索用社員CD_Validated
        /// 社員一覧の該当行にフォーカスを当てます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEARCH_SHAIN_CD_Validated(object sender, EventArgs e)
        {
            this.logic.SearchIchiranShainRow();
        }

        #endregion

        #region - Ichiran Event -

        /// <summary>
        /// メニュー一覧 - セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 一覧セル編集開始時イベント処理
            bool catchErr = this.logic.IchiranCellEnter(e);
            if (catchErr)
            {
                return;
            }
            Ichiran.Refresh();
        }

        /// <summary>
        /// メニュー一覧 - セルが変更されたことを通知するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.Ichiran.CurrentCell.Name.Equals(MenuKengenHoshuConstans.BIKOU))
            {
                // 備考の場合はイベント回避
                return;
            }

            // チェックボックスが変更された場合
            // CellValueChangedイベントが発生するのはフォーカスアウト時。
            // このタイミングでコミットすることで変更時にイベントを発生させることが可能。
            if (this.Ichiran.IsCurrentCellDirty)
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// メニュー一覧 - セル変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (MenuKengenHoshuForm.processingFlg)
            {
                return;
            }

            try
            {
                MenuKengenHoshuForm.processingFlg = true;
                this.logic.Ichiran_CellValueChanged(e);
            }
            finally
            {
                MenuKengenHoshuForm.processingFlg = false;
            }
        }

        /// <summary>
        /// メニュー一覧 - CellContentClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellContentClick(object sender, CellEventArgs e)
        {
            if (MenuKengenHoshuForm.processingFlg)
            {
                return;
            }

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M188", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                return;
            }

            try
            {

                MenuKengenHoshuForm.processingFlg = true;
                this.logic.Ichiran_CellContentClick(e);
            }
            finally
            {
                MenuKengenHoshuForm.processingFlg = false;
            }
        }

        /// <summary>
        /// 社員一覧 - セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_Shain_CellContentClick(object sender, CellEventArgs e)
        {
            if (MenuKengenHoshuForm.processingFlg)
            {
                return;
            }

            try
            {
                MenuKengenHoshuForm.processingFlg = true;
                this.logic.IchiranShain_CellContentClick(e);
            }
            finally
            {
                MenuKengenHoshuForm.processingFlg = false;
            }
        }

        #endregion

        /// <summary>
        /// ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellContentDoubleClick(object sender, CellEventArgs e)
        {
            if (MenuKengenHoshuForm.processingFlg)
            {
                return;
            }

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M188", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                return;
            }

            try
            {

                MenuKengenHoshuForm.processingFlg = true;
                this.logic.Ichiran_CellContentClick(e);
            }
            finally
            {
                MenuKengenHoshuForm.processingFlg = false;
            }
        }
    }

}
