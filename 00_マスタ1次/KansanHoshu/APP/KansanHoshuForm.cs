// $Id: KansanHoshuForm.cs 37791 2014-12-19 08:22:08Z fangjk@oec-h.com $
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KansanHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Utility;

namespace KansanHoshu.APP
{
    /// <summary>
    /// 換算値保守画面
    /// </summary>
    [Implementation]
    public partial class KansanHoshuForm : SuperForm
    {
        /// <summary>
        /// 換算値保守画面ロジック
        /// </summary>
        private KansanHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KansanHoshuForm()
            : base(WINDOW_ID.M_KANSAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KansanHoshuLogic(this);

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
            if (!string.IsNullOrWhiteSpace(this.SHURUI_CD.Text))
            {
                this.Search(null, e);
            }

			// Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }
        private bool Settitle()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.Parent;

                //title
                var titleControl = (System.Windows.Forms.Label)controlUtil.FindControl(parentForm, "lb_title");

                bool catchErr = this.logic.TitleInit();
                if (catchErr)
                {
                    return true;
                }
                parentForm.txb_process.Enabled = true;
                parentForm.txb_process.ReadOnly = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Settitle", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        ///　Title処理
        /// </summary>
        public virtual void Change(object sender, EventArgs e)
        {
            bool catchErr = this.logic.TitleInit();
            if (catchErr)
            {
                return;
            }
            if (!string.IsNullOrEmpty(this.SHURUI_CD.Text))
            {
                this.Search(sender, e);
            }
            else
            {
                //this.SHURUI_NAME_RYAKU.Clear();
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
            var focus = (this.TopLevelControl as Form).ActiveControl;
            this.Ichiran.Focus();

            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            var messageShowLogic = new MessageBoxShowLogic();
            int count = this.logic.Search();
            if (count == 0)
            {
                messageShowLogic.MessageBoxShow("C001");
            }
            else if (count > 0)
            {
                if (this.logic.SetIchiran()) { return; }
            }
            else
            {
                return;
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            if (focus != null)
            {
                focus.Focus();
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            bool isNoErr = true;
            if (!base.RegistErrorFlag)
            {
                isNoErr = this.logic.DuplicationCheck();
                //重複データが登録不可
                if (isNoErr)
                {
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
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            if (!base.RegistErrorFlag)
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
            this.logic.Cancel();
            if (!string.IsNullOrEmpty(this.SHURUI_CD.Text))
            {
                Search(sender, e);
            }
        }

        //プレビュ機能削除
        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    var messageShowLogic = new MessageBoxShowLogic();
        //    if (string.IsNullOrEmpty(this.DENPYOU_KBN_CD.Text))
        //    {
        //        messageShowLogic.MessageBoxShow("E001", "伝票区分");
        //        this.DENPYOU_KBN_CD.Focus();
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

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            Properties.Settings.Default.ShuruiItem_Text = this.SHURUI_CD.Text;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 画面Shown処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KansanHoshuForm_Shown(object sender, EventArgs e)
        {
            this.logic.SearchKansanShiki();
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
                        gcMultiRow.CurrentCell.Value = DateTime.Today;
                    }
                }
            }
        }

        /// <summary>
        /// 項目のフォーカスアウトチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.KansanHoshuConstans.HINMEI_CD))
            {
                this.logic.SearchHinmei(e);
            }
        }

        /// <summary>
        /// 単位セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 未検索の場合、明細入力ができないようにする
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

            // 1行目が新行の場合、適用開始日に本日を設定
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                //システム設定から初期値取得
                this.logic.settingSysDataDisp(e.RowIndex);

                //計算式を×に設定
                this.Ichiran[e.RowIndex, "KANSANSHIKI"].Value = 0;
                bool catchErr = this.logic.SearchKansanShiki();
                if (catchErr)
                {
                    return;
                }
            }


            if (e.CellName.Equals(Const.KansanHoshuConstans.HINMEI_CD))
            {
                GcCustomAlphaNumTextBoxCell target = (GcCustomAlphaNumTextBoxCell)this.Ichiran[e.RowIndex, e.CellName];
                target.PopupSearchSendParams[1].SubCondition[1].Value = this.logic.TargetDenpyouKbn.ToString();
                object val = this.Ichiran[e.RowIndex, e.CellName].Value;
                if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                {
                    this.Ichiran[e.RowIndex, Const.KansanHoshuConstans.SHURUI_CD_TEMP].Value = null;
                }
            }
        }

        /// <summary>
        /// セル表示編集処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals(Const.KansanHoshuConstans.UNIT_CD))
            {
                if (this.Ichiran[e.RowIndex, Const.KansanHoshuConstans.UNIT_NAME_RYAKU].Value != null)
                {
                    e.Value = this.Ichiran[e.RowIndex, Const.KansanHoshuConstans.UNIT_NAME_RYAKU].Value.ToString();
                }
            }
            if (e.CellName.Equals(Const.KansanHoshuConstans.KANSANCHI))
            {
                e.Value = string.Format("{0:0.000}", e.Value);
            }
        }

        /// <summary>
        /// セルデータエラーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void Ichiran_DataError(object sender, DataErrorEventArgs e)
        {
            // 例外を無視する
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (e.CellName.Equals(Const.KansanHoshuConstans.UNIT_CD) && (e.Context & DataErrorContexts.CurrentCellChange) != DataErrorContexts.CurrentCellChange)
            {
                msgLogic.MessageBoxShow("E020", "単位");
                e.Cancel = true;
                ((TextBox)this.Ichiran.EditingControl).SelectAll();
            }
        }

        /// <summary>
        /// 種類略称名設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SHURUI_CD_TextChanged(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            this.SHURUI_NAME_RYAKU.Clear();
            Ichiran.DataSource = null;
            Ichiran.RowCount = 1;
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SearchString = null;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            if (this.SHURUI_CD.Text.Length >= 3)
            {
                this.logic.SearchDenPyouKbnName();
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }
    }
}