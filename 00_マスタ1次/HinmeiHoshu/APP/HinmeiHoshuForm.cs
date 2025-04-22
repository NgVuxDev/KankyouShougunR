// $Id: HinmeiHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using HinmeiHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

using r_framework.Dto;
using System.Collections.ObjectModel;

namespace HinmeiHoshu.APP
{
    /// <summary>
    /// 品名保守画面
    /// </summary>
    [Implementation]
    public partial class HinmeiHoshuForm : SuperForm
    {
        /// <summary>
        /// 品名保守画面ロジック
        /// </summary>
        private HinmeiHoshuLogic logic;

        /// <summary>
        /// 単位CD前回値
        /// </summary>
        private string preUnitCd = string.Empty;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private bool nowControlOut = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HinmeiHoshuForm()
            : base(WINDOW_ID.M_HINMEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new HinmeiHoshuLogic(this);

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

            this.Search(null, e);

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

            // 検索条件入力チェック
            if (!this.logic.CheckSearchString())
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E084", CONDITION_ITEM.Text);
                CONDITION_VALUE.Focus();
                return;
            }

            this.Ichiran.CausesValidation = false;

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else if (count > 0)
            {
                bool catchErr = this.logic.SetIchiran();
                if (catchErr)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            this.Ichiran.CausesValidation = true;

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
            if (!base.RegistErrorFlag)
            {
                if (this.logic.CheckDelete())
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
            Search(sender, e);
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

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
            bool catchErr = this.logic.CancelCondition();
            if (catchErr)
            {
                return;
            }
            //20150414 minhhoang edit #1748
            //do not reload search result when F7 press
            //Search(sender, e);
            this.logic.SetIchiran();
            //20150414 minhhoang end edit #1748
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

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
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
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            // 参照モード時、e.FormattedValueに""が設定されるため、必ずエラーとなるため権限チェックを行い回避
            bool catchErr = false;
            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("M230", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                catchErr = this.logic.IchiranValidating(sender, e);
                if (catchErr)
                {
                    return;
                }
            }

            // 伝種区分チェック&セッティング
            if (e.CellName.Equals("DENSHU_KBN_CD"))
            {
                this.logic.DenshuKbnCheckAndSetting(e);
            }

            if (e.CellName.Equals(Const.HinmeiHoshuConstans.UNIT_CD))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                if (e.FormattedValue == null || !e.FormattedValue.Equals(preUnitCd))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(e.FormattedValue)))
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Value = DBNull.Value;
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value = string.Empty;
                    }
                    else
                    {
                        if (this.logic.unitCheck(e.RowIndex))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "単位");
                            ((TextBox)this.Ichiran.EditingControl).SelectAll();
                            var cell = this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"] as GcCustomTextBoxCell;
                            cell.IsInputErrorOccured = true;
                            cell.UpdateBackColor();
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }

            //20250312
            if (e.CellName.Equals(Const.HinmeiHoshuConstans.TC_KOME_KANZANKEISU))
            {
                var row = this.Ichiran.Rows[e.RowIndex];
                object tc_kome = row.Cells[Const.HinmeiHoshuConstans.TC_KOME_KANZANKEISU].Value;

                if (tc_kome == null)
                {
                    return;
                }

                var cell = (GcCustomNumericTextBox2Cell)row.Cells[Const.HinmeiHoshuConstans.TC_KOME_KANZANKEISU];

                if (decimal.TryParse(tc_kome.ToString(), out decimal result))
                {
                    if (result <= 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShowError("立米換算係数に0以下の値は設定できません。\n0を超える値を設定してください。");
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("M230", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                if (nowControlOut)
                {
                    return;
                }

                nowControlOut = true;
                this.logic.IchiranSwitchCdName(e, Const.HinmeiHoshuConstans.FocusSwitch.OUT);
                nowControlOut = false;
            }
        }

        /// <summary>
        /// 単位セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }

            // 1行目が新行の場合設定
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                //システム設定から初期値取得
                this.logic.settingSysDataDisp(e.RowIndex);
            }

            if (nowControlOut == false)
            {
                this.logic.IchiranSwitchCdName(e, Const.HinmeiHoshuConstans.FocusSwitch.IN);
            }
        }

        /// <summary>
        /// セル値変化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (e.CellName.Equals(Const.HinmeiHoshuConstans.ZEI_KBN_CD))
            {
                this.logic.SetZeiKbnName(e.RowIndex);
            }
        }

        /// <summary>
        /// セル編集値変化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // 税区分セルで変化した瞬間に変更の確定を行う
            if (this.Ichiran.CurrentCell.Name.Equals(Const.HinmeiHoshuConstans.ZEI_KBN_CD) && this.Ichiran.IsCurrentCellDirty)
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 条件値テキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if (this.CONDITION_ITEM.Text == "単位")
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
            }
        }

        /// <summary>
        /// 条件項目テキストボックスのバリデーションが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void CONDITION_ITEM_Validated(object sender, EventArgs e)
        {
            if (this.CONDITION_ITEM.Text == "単位")
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
            }
        }

        private void HinmeiHoshuForm_Shown(object sender, EventArgs e)
        {
            this.logic.EditableToPrimaryKey();
            //if (!r_framework.Authority.Manager.CheckAuthority("M230", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShowInformation("修正権限がないため参照モードで表示します。");
            //}
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}