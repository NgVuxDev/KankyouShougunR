using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.UnchiHinmeiHoshu.Logic;

namespace Shougun.Core.Master.UnchiHinmeiHoshu.APP
{
    /// <summary>
    /// 運賃品名入力画面
    /// </summary>
    [Implementation]
    public partial class UnchiHinmeiHoshuForm : SuperForm
    {
        /// <summary>
        /// 運賃品名入力画面ロジック
        /// </summary>
        private UnchiHinmeiHoshuLogic logic;

        /// <summary>
        /// 単位CD前回値
        /// </summary>
        private string preUnitCd = string.Empty;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private bool nowControlOut = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchiHinmeiHoshuForm()
            : base(WINDOW_ID.M_UNCHIN_HINMEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new UnchiHinmeiHoshuLogic(this);

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
            if (!this.logic.WindowInit())
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
            this.Ichiran.CausesValidation = false;

            int count = this.logic.Search();
            if (count == -1)
            {
                return;
            }
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
            }
            else
            {
                this.logic.SetIchiran();
            }

            this.Ichiran.CausesValidation = true;

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
                if (this.logic.UnchinHinmeiCdCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "運賃品名", "運賃品名CD", "\n運賃単価マスタ");
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

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            if (this.logic.Cancel())
            {
                this.Search(sender, e);
            }
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
            this.logic.CancelCondition();

            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("M639", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, true);
            }
            else
            {
                this.logic.DispReferenceMode();
            }
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
            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("M639", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                if (!this.logic.IchiranValidating(sender, e))
                {
                    return;
                }
            }

            if (e.CellName.Equals(Const.UnchiHinmeiHoshuConstans.UNIT_CD))
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
                            this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Style.BackColor = Constans.ERROR_COLOR;
                            e.Cancel = true;
                            ((TextBox)this.Ichiran.EditingControl).SelectAll();
                            return;
                        }
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
            if (nowControlOut)
            {
                return;
            }

            nowControlOut = true;
            this.logic.IchiranCellSwitchCdName(e, Const.UnchiHinmeiHoshuConstans.FocusSwitch.OUT);
            nowControlOut = false;
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

            if (e.CellName.Equals(Const.UnchiHinmeiHoshuConstans.UNIT_CD))
            {
                this.preUnitCd = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["UNIT_NAME_RYAKU"].Value);
            }

            if (nowControlOut == false)
            {
                this.logic.IchiranCellSwitchCdName(e, Const.UnchiHinmeiHoshuConstans.FocusSwitch.IN);
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

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnchiHinmeiHoshuForm_Shown(object sender, EventArgs e)
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