// $Id: UnchinTankaHoshuForm.cs 43108 2015-02-26 00:37:53Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.UnchinTankaHoshu.Logic;

namespace Shougun.Core.Master.UnchinTankaHoshu.APP
{
    /// <summary>
    /// 運賃単価入力画面
    /// </summary>
    [Implementation]
    public partial class UnchinTankaHoshuForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 運賃単価入力画面ロジック
        /// </summary>
        private UnchinTankaHoshuLogic logic;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal string beforeGyoushaCd;

        internal string preValue = string.Empty;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isError;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private bool nowControlOut = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchinTankaHoshuForm()
            : base(WINDOW_ID.M_UNCHIN_TANKA, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new UnchinTankaHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit())
            {
                return;
            }
            int count = this.logic.Search();
            if (count == -1)
            {
                return;
            }
            if (count == 0)
            {
                this.logic.SetIchiran();
                //FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            }
            else
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

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Ichiran.CausesValidation = false;

            this.Ichiran.AllowUserToAddRows = true;

            int count = this.logic.Search();
            if (count == -1)
            {
                return;
            }
            if (count == 0)
            {
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    // 明細部クリア
                    this.Ichiran.DataSource = null;
                    Ichiran.AllowUserToAddRows = false;
                }
                //else
                //{
                //var messageShowLogic = new MessageBoxShowLogic();
                //messageShowLogic.MessageBoxShow("C001");
                //}
            }
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "運搬業者");
                this.UNPAN_GYOUSHA_CD.Focus();
            }
            else
            {
                if (!this.logic.SetIchiran())
                {
                    return;
                }
            }

            this.Ichiran.CausesValidation = true;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (this.Ichiran == null || this.Ichiran.Rows.Count <= 0)
                return;

            if (base.RegistErrorFlag)
            {
                bool focued = false;
                foreach (Row row in this.Ichiran.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.Style.BackColor == Constans.ERROR_COLOR)
                        {
                            this.Ichiran.CellValidating -= this.Ichiran_CellValidating;
                            this.Ichiran.CurrentCell = cell;
                            this.Ichiran.Focus();
                            focued = true;
                            this.Ichiran.CellValidating += this.Ichiran_CellValidating;
                            break;
                        }
                    }
                    if (focued)
                    {
                        break;
                    }
                }
                return;
            }

            if (!this.logic.DuplicationCheck())
            {
                base.RegistErrorFlag = true;
                return;
            }

            if (!this.logic.CheckRegist(sender, e))
            {
                return;
            }

            if (!base.RegistErrorFlag)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                if ((string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text)) ||
                    (this.logic.SearchResultAll == null))
                {
                    this.UNPAN_GYOUSHA_CD.Text = string.Empty;
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
                    }
                }
                else if (!base.RegistErrorFlag)
                {
                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
                    }
                }
            }
        }

        #endregion

        #region 論理削除

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (this.Ichiran == null || this.Ichiran.Rows.Count <= 0)
                    return;

                if (!this.logic.DuplicationCheck())
                {
                    base.RegistErrorFlag = true;
                    return;
                }

                if (!this.logic.CreateEntity(true))
                {
                    return;
                }
                this.logic.LogicalDelete();
                if (this.logic.isRegist)
                {
                    this.Search(sender, e);
                }
            }
        }

        #endregion

        #region 取り消し

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            if (!this.logic.Cancel())
            {
                return;
            }

            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                Search(sender, e);
            }
        }

        #endregion

        #region プレビュー

        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    var messageShowLogic = new MessageBoxShowLogic();
        //    if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
        //    {
        //        messageShowLogic.MessageBoxShow("E001", "業者");
        //        this.GYOUSHA_CD.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        this.logic.Preview();
        //    }
        //}

        #endregion

        #region CSV

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.ParentForm;
            var bt4_Enable = parentForm.bt_func4.Enabled;
            var bt6_Enable = parentForm.bt_func6.Enabled;
            var bt9_Enable = parentForm.bt_func9.Enabled;

            this.logic.CancelCondition();

            parentForm.bt_func4.Enabled = bt4_Enable;
            parentForm.bt_func6.Enabled = bt6_Enable;
            parentForm.bt_func9.Enabled = bt9_Enable;
        }

        #endregion

        #region Formクローズ処理

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                Properties.Settings.Default.GyoushaValue_Text = string.Empty;
                Properties.Settings.Default.GyoushaName_Text = string.Empty;
            }
            else if (this.logic.CheckUnpanGyousha(this.UNPAN_GYOUSHA_CD.Text))
            {
                Properties.Settings.Default.GyoushaValue_Text = this.UNPAN_GYOUSHA_CD.Text;
                Properties.Settings.Default.GyoushaName_Text = this.UNPAN_GYOUSHA_NAME.Text;
            }

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        #endregion

        #region 一覧のCell

        /// <summary>
        /// 日付コントロールの初期設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
        {
            GcMultiRow gcMultiRow = (GcMultiRow)sender;
            if (e.EditCanceled == false)
            {
                if (gcMultiRow.CurrentCell is GcCustomDataTimePicker)
                {
                    if (gcMultiRow.CurrentCell.Value == null
                        || string.IsNullOrEmpty(gcMultiRow.CurrentCell.Value.ToString()))
                    {
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        //gcMultiRow.CurrentCell.Value = DateTime.Today;
                        gcMultiRow.CurrentCell.Value = this.logic.parentForm.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
        }

        /// <summary>
        /// 一覧のCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD))
            {
                this.logic.IchiranCellValidatingUnchinHinmei(e);
            }

            if (e.CellName.Equals(Const.UnchinTankaHoshuConstans.UNIT_CD))
            {
                this.logic.IchiranCellValidatingUnit(e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            if (nowControlOut)
            {
                return;
            }

            nowControlOut = true;
            this.logic.IchiranCellSwitchCdName(e, Const.UnchinTankaHoshuConstans.FocusSwitch.OUT);
            nowControlOut = false;
        }

        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 業者CDが空白の場合、明細入力ができないようにする
            if (this.logic.SearchResultAll == null)
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
            if (this.Ichiran.CurrentCell != null)
            {
                this.preValue = Convert.ToString(this.Ichiran.CurrentCell.Value);
            }

            if (nowControlOut == false)
            {
                this.logic.IchiranCellSwitchCdName(e, Const.UnchinTankaHoshuConstans.FocusSwitch.IN);
            }
        }

        /// <summary>
        /// セルフォーマット処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            switch (e.CellName)
            {
                case "TANKA":
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        e.Value = this.logic.FormatSystemTanka(decimal.Parse(e.Value.ToString()));
                    }
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 運搬業者コードをEnter

        /// <summary>
        /// 運搬業者コードをチェック
        /// 運搬業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                this.beforeGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            }
        }

        #endregion

        #region 運搬業者コードをチェック

        /// <summary>
        /// 運搬業者コードをチェック
        /// 運搬業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.UNPAN_GYOUSHA_CD_Validated();
        }

        #endregion

        /// <summary>
        /// 在庫品名CDポップアップを開け前に処理します
        /// </summary>
        public void PopupAfter()
        {
            this.logic.UNPAN_GYOUSHA_CD_Validated();
        }
    }
}