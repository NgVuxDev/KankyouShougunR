// $Id: ManifestKansanHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using ManifestKansanHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace ManifestKansanHoshu.APP
{
    /// <summary>
    /// マニフェスト換算保守画面
    /// </summary>
    [Implementation]
    public partial class ManifestKansanHoshuForm : SuperForm
    {
        /// <summary>
        /// マニフェスト換算保守画面ロジック
        /// </summary>
        private ManifestKansanHoshuLogic logic;

        /// <summary>
        /// 単位CD前回値
        /// </summary>
        private string preUnitCd = string.Empty;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private bool nowControlOut = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManifestKansanHoshuForm()
            : base(WINDOW_ID.M_MANIFEST_KANSAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ManifestKansanHoshuLogic(this);

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
            if (!string.IsNullOrWhiteSpace(this.HOUKOKUSHO_BUNRUI_CD.Text))
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

            this.Ichiran.Focus();

            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "報告書分類");
                this.HOUKOKUSHO_BUNRUI_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1979
                int count = this.logic.Search();
                if (count == 0)
                {
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
            if ((string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text)) ||
                (this.logic.SearchResultAll == null))
            {
                messageShowLogic.MessageBoxShow("E001", "報告書分類");
                this.HOUKOKUSHO_BUNRUI_CD.Focus();
                return;
            }
            else if (!base.RegistErrorFlag)
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
            var isNoError = true;
            if ((string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text)) ||
                (this.logic.SearchResultAll == null))
            {
                messageShowLogic.MessageBoxShow("E001", "伝票区分");
                this.HOUKOKUSHO_BUNRUI_CD.Focus();
                return;
            }
            else if (!base.RegistErrorFlag)
            {
                isNoError = this.logic.DuplicationCheck();
                if (isNoError)
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
            if (!string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                Search(sender, e);
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "伝票区分");
                this.HOUKOKUSHO_BUNRUI_CD.Focus();
                return;
            }
            else
            {
                this.logic.CSV();
            }
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
            Properties.Settings.Default.ManiHoukokushoCd_Text = this.HOUKOKUSHO_BUNRUI_CD.Text;

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
                        gcMultiRow.CurrentCell.Value = DateTime.Today;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
        }

        /// <summary>
        /// 画面Shown処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManifestKansanHoshuForm_Shown(object sender, EventArgs e)
        {
            this.logic.SearchKansanShiki();
        }

        /// <summary>
        /// 単位セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            //報告書分類ＣＤが空白の場合、明細入力ができないようにする
            if (this.HOUKOKUSHO_BUNRUI_CD.TextLength <= 0 ||
                this.logic.SearchResultAll == null)
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
                this.logic.SearchKansanShiki();
            }

            if (nowControlOut == false)
            {
                this.logic.IchiranSwitchCdName(e, Const.ManifestKansanHoshuConstans.FocusSwitch.IN);
            }
        }

        /// <summary>
        /// セル表示編集処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals(Const.ManifestKansanHoshuConstans.KANSANCHI))
            {
                e.Value = string.Format("{0:0.000}", e.Value);
            }
        }

        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.ManifestKansanHoshuConstans.UNIT_CD))
            {
                this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Style.BackColor = Constans.NOMAL_COLOR;
                if (e.FormattedValue == null || !e.FormattedValue.Equals(preUnitCd))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(e.FormattedValue)))
                    {
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_CD"].Value = DBNull.Value;
                        this.Ichiran.Rows[e.RowIndex].Cells["UNIT_NAME"].Value = string.Empty;
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
            this.logic.IchiranSwitchCdName(e, Const.ManifestKansanHoshuConstans.FocusSwitch.OUT);
            nowControlOut = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOUKOKUSHO_BUNRUI_CD_TextChanged(object sender, EventArgs e)
        {
            this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = string.Empty;
            Ichiran.DataSource = null;
            Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1979
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SearchString = null;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 条件値テキストボックスに入力があったときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if (this.CONDITION_ITEM.Text == "単位区分")
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
            if (this.CONDITION_ITEM.Text == "単位区分")
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
            }
        }

        public void BeforeRegist()
        {
            this.logic.SearchKansanShiki();
        }
    }
}