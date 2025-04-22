// $Id: SharyouHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using SharyouHoshu.Logic;

namespace SharyouHoshu.APP
{
    /// <summary>
    /// 車輌保守画面
    /// </summary>
    [Implementation]
    public partial class SharyouHoshuForm : SuperForm
    {
        /// <summary>
        /// 車輌保守画面ロジック
        /// </summary>
        private SharyouHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SharyouHoshuForm()
            : base(WINDOW_ID.M_SHARYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new SharyouHoshuLogic(this);

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
            if (!string.IsNullOrWhiteSpace(this.GYOUSHA_CD.Text))
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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "業者CD");
                this.GYOUSHA_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true; 
                
                int count = this.logic.Search();
                if (count == 0)
                {
                    messageShowLogic.MessageBoxShow("C001");
                }
                else if (count > 0)
                {
                    bool catchErr = this.logic.SetIchiran();
                    if(catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
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
                bool catchErr = false;
                var messageShowLogic = new MessageBoxShowLogic();
                if ((string.IsNullOrEmpty(this.GYOUSHA_CD.Text)) ||
                    (this.logic.SearchResultAll == null))
                {
                    this.GYOUSHA_CD.Text = string.Empty;
                    this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                    catchErr = this.logic.CreateEntity(false);
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
                else if (!base.RegistErrorFlag)
                {
                    catchErr = this.logic.CreateEntity(false);
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
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag && this.logic.CheckDelete())
            {
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

            if (!string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                Search(sender, e);
            }
        }

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

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;
            Properties.Settings.Default.GyoushaValue_Text = this.GYOUSHA_CD.Text;
            Properties.Settings.Default.GyoushaName_Text = this.GYOUSHA_NAME_RYAKU.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

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
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            switch (e.CellName)
            {
                case "SHASYU_CD":
                    // 車種名が空の場合、自動でDBNullになってしまうためここで空白に戻す。
                    // （値が変更されると更新日・更新PCなどが変更されるのでその対策）
                    if (this.Ichiran.Rows[e.RowIndex]["SHASHU_NAME_RYAKU"].Value == DBNull.Value)
                    {
                        this.Ichiran.Rows[e.RowIndex]["SHASHU_NAME_RYAKU"].Value = string.Empty;
                    }
                    break;

                case "SHAIN_CD":
                    // 運転者チェック
                    bool catchErr = false;
                    if (!this.logic.CheckUntenshasha(out catchErr))
                    {
                        // フォーカスを動かさない
                        e.Cancel = true;
                    }
                    if (catchErr)
                    {
                        return;
                    }
                    // 運転者名が空の場合、自動でDBNullになってしまうためここで空白に戻す。
                    // （値が変更されると更新日・更新PCなどが変更されるのでその対策）
                    if (this.Ichiran.Rows[e.RowIndex]["SHAIN_NAME_RYAKU"].Value == DBNull.Value)
                    {
                        this.Ichiran.Rows[e.RowIndex]["SHAIN_NAME_RYAKU"].Value = string.Empty;
                    }

                    break;
            }



            if (e.CellName.Equals(Const.SharyouHoshuConstans.SHARYOU_CD))
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
            }

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
                case "SAIDAI_SEKISAI":
                case "KUUSHA_JYURYO":
                    if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                    {
                        bool catchErr = false;
                        e.Value = this.logic.FormatSystemJuuryou(decimal.Parse(e.Value.ToString()), out catchErr);
                    }
                    break;
            }
        }

        /// <summary>
        /// 業者コードをチェック
        /// 業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            //    Ichiran.DataSource = null;
            //    Ichiran.RowCount = 1;
            //    this.logic.SearchResult = null;
            //    this.logic.SearchResultAll = null;
            //    this.logic.SearchString = null;
            //    FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            if (this.GYOUSHA_CD.Text.Length >= 6)
            {
                if (!this.logic.SearchGyoushaName())
                {
                    Ichiran.DataSource = null;
                    Ichiran.AllowUserToAddRows = false;
                    this.logic.SearchResult = null;
                    this.logic.SearchResultAll = null;
                    this.logic.SearchString = null;
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// FormのShown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharyouHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}