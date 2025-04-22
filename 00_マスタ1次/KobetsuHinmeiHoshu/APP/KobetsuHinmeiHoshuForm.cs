// $Id: KobetsuHinmeiHoshuForm.cs 52370 2015-06-16 02:14:23Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KobetsuHinmeiHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Utility;

namespace KobetsuHinmeiHoshu.APP
{
    /// <summary>
    /// 個別品名保守画面
    /// </summary>
    [Implementation]
    public partial class KobetsuHinmeiHoshuForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        internal string beforGenbaCD = string.Empty;

        /// <summary>
        /// 個別品名保守画面ロジック
        /// </summary>
        private KobetsuHinmeiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KobetsuHinmeiHoshuForm()
            : base(WINDOW_ID.M_KOBETSU_HINMEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KobetsuHinmeiHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            catchErr = Settitle();
            if (catchErr)
            {
                return;
            }

            if (codeCheck(0))
            {
                this.Search(null, e);
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        ///  title初期化
        /// </summary>
        private bool Settitle()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.Parent;

                //title
                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Settitle", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
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
            this.logic.EditableToPrimaryKey();
            this.GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {

            if (codeCheck(1))
            {
                this.Ichiran.AllowUserToAddRows = true;

                int count = this.logic.Search();
                bool catchErr = false;
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    catchErr = this.logic.SetIchiran();//空データをセットする。
                }
                else if (count > 0)
                {
                    catchErr = this.logic.SetIchiran();
                }
                if (count < 0 || catchErr)
                {
                    return;
                }
                this.logic.EditableToPrimaryKey();
            }

            this.Ichiran.Focus();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {

                // 検索部CODEチェック
                if (!codeCheck(1))
                {
                    return;
                }

                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {


            //通常削除
            if (!base.RegistErrorFlag)
            {

                if (!codeCheck(1))
                {
                    return;
                }

                bool catchErr = this.logic.CreateEntity(true);
                if (catchErr)
                {
                    return;
                }
                this.logic.LogicalDelete();
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            this.GYOUSHA_CD.Text = string.Empty;
            this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;
            if (this.Ichiran.DataSource != null)
            {
                this.Ichiran.DataSource = null;
                this.Ichiran.AllowUserToAddRows = false;
            }

            this.GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            if (!codeCheck(1))
            {
                return;
            }

            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            this.GYOUSHA_CD.Text = string.Empty;
            this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;
            if (this.Ichiran.DataSource != null)
            {
                this.Ichiran.DataSource = null;
                this.Ichiran.AllowUserToAddRows = false;
            }
            this.GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                Properties.Settings.Default.GYOUSHA_CD = "";
                Properties.Settings.Default.GYOUSHA_NAME_RYAKU = "";
                Properties.Settings.Default.GENBA_CD = "";
                Properties.Settings.Default.GENBA_NAME_RYAKU = "";
            }
            else if (this.logic.CheckGyousha(this.GYOUSHA_CD.Text))
            {
                if (Properties.Settings.Default.GYOUSHA_CD != this.GYOUSHA_CD.Text)
                {
                    Properties.Settings.Default.GENBA_CD = "";
                    Properties.Settings.Default.GENBA_NAME_RYAKU = "";
                }
                Properties.Settings.Default.GYOUSHA_CD = this.GYOUSHA_CD.Text;
                Properties.Settings.Default.GYOUSHA_NAME_RYAKU = this.GYOUSHA_NAME_RYAKU.Text;
                if (this.logic.CheckGenba(this.GYOUSHA_CD.Text, this.GENBA_CD.Text))
                {
                    Properties.Settings.Default.GENBA_CD = this.GENBA_CD.Text;
                    Properties.Settings.Default.GENBA_NAME_RYAKU = this.GENBA_NAME_RYAKU.Text;
                }
            }
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string gyoushaCd = this.GYOUSHA_CD.Text;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                gyoushaCd = this.GYOUSHA_CD.Text.PadLeft(this.GYOUSHA_CD.MaxLength, '0');
            }
            if (!this.beforGyousaCD.Equals(gyoushaCd))
            {
                Ichiran.CurrentCell = null;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;

                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                if (!this.logic.SearchGyoushaName())
                {
                    this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    e.Cancel = true;
                }
            }
            else
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 現場名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
            this.beforGenbaCD = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 現場CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string gyoushaCd = this.GYOUSHA_CD.Text;
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                gyoushaCd = this.GYOUSHA_CD.Text.PadLeft(this.GYOUSHA_CD.MaxLength, '0');
            }
            string genbaCd = this.GENBA_CD.Text;
            if (!string.IsNullOrEmpty(genbaCd))
            {
                genbaCd = this.GENBA_CD.Text.PadLeft(this.GENBA_CD.MaxLength, '0');
            }
            if (!this.beforGyousaCD.Equals(gyoushaCd) || !this.beforGenbaCD.Equals(genbaCd))
            {
                Ichiran.CurrentCell = null;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;
            }
            if (!string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_CD.Text = genbaCd;
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_CD.Focus();
                    e.Cancel = true;
                    return;
                }

                if (!this.logic.SearchGenbaName())
                {
                    e.Cancel = true;
                    this.GENBA_NAME_RYAKU.Text = string.Empty;
                }
            }
            else
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 検索部CODEのチェック処理
        /// </summary>
        /// <param name="syoriKbn">処理区分</param>
        /// <returns>チェック結果</returns>
        private bool codeCheck(int syoriKbn)
        {
            try
            {
                var messageShowLogic = new MessageBoxShowLogic();

                // 取引先を必須入力項目から任意入力とし、業者を必須入力項目とする（2014/4/30）
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    Ichiran.CurrentCell = null;
                    this.logic.SearchResult = null;
                    this.logic.SearchResultAll = null;
                    this.logic.SearchString = null;
                    Ichiran.DataSource = null;
                    Ichiran.AllowUserToAddRows = false;

                    if (syoriKbn == 1)
                    {
                        this.GYOUSHA_CD.Focus();
                        messageShowLogic.MessageBoxShow("E001", "業者");
                    }

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("codeCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 明細選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            if (this.GYOUSHA_CD.TextLength <= 0 || this.logic.SearchResultAll == null)
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }
        }

        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            GcCustomMultiRow list = this.Ichiran;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            //品名
            if (e.CellName.Equals(Const.KobetsuHinmeiHoshuConstans.HINMEI_CD))
            {
                bool isNoErr = this.logic.DuplicationCheck();
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }
                    return;
                }
                this.logic.SearchHinmei(e);
            }
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            string curGyoushaCd = this.GYOUSHA_CD.Text;
            if (this.beforGyousaCD != curGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        // 業者CDのTextChangedイベント
        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        // 現場CDのTextChangedイベント
        private void GENBA_CD_TextChanged(object sender, EventArgs e)
        {
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
