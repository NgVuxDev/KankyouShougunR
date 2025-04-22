// $Id: BankHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using BankHoshu.Logic;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace BankHoshu.APP
{
    /// <summary>
    /// 銀行保守画面
    /// </summary>
    [Implementation]
    public partial class BankHoshuForm : SuperForm
    {
        /// <summary>
        /// 銀行保守画面ロジック
        /// </summary>
        private BankHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        //Bao 2016/04/26 #17462 オンラインバンク マージ -Start
        private string PreviousValue;
        bool bRenkeiChk = true;
        bool bChangeBackColor = false;
        bool bMove = false;
        int errorCell;
        int errorRow;
        //Bao 2016/04/26 #17462 オンラインバンク マージ -End
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BankHoshuForm()
            : base(WINDOW_ID.M_BANK, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new BankHoshuLogic(this);

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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

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
                    if (this.RegistErrorFlag)
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
        /// 銀行CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            // Bao 2016/04/22 #17435 M198 銀行入力 -Add
            bChangeBackColor = false;

            if (e.CellName.Equals(Const.BankHoshuConstans.BANK_CD))
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
                // Bao 2016/04/22 #17435 M198 銀行入力 -Start
                string sValue = string.Empty;
                string sRenkeiCd = string.Empty;
                string sBankCd = string.Empty;

                if (e.FormattedValue != null)
                {
                    sValue = e.FormattedValue.ToString().PadLeft(Const.BankHoshuConstans.MAX_LENT_RENKEI_CD, '0');
                }
                if (this.Ichiran.Rows[e.RowIndex].Cells[Const.BankHoshuConstans.RENKEI_CD].Value != null)
                {
                    sRenkeiCd = this.Ichiran.Rows[e.RowIndex].Cells[Const.BankHoshuConstans.RENKEI_CD].Value.ToString();
                }

                if (this.Ichiran.Rows[e.RowIndex].Cells[Const.BankHoshuConstans.BANK_CD].Value != null)
                {
                    sBankCd = this.Ichiran.Rows[e.RowIndex].Cells[Const.BankHoshuConstans.BANK_CD].Value.ToString();
                }

                if (!PreviousValue.Equals(sValue))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells[Const.BankHoshuConstans.RENKEI_CD].Value = e.FormattedValue;
                }
                // Bao 2016/04/22 #17435 M198 銀行入力 -End
            }
            // Bao 2016/04/22 #17435 M198 銀行入力 -Start
            if (!bMove)
            {
                if (e.CellName.Equals(Const.BankHoshuConstans.BANK_CD))
                {
                    bool isRenkeiNoErr = this.logic.RenkeiDuplicationCheck(e);
                    if (!isRenkeiNoErr)
                    {

                        errorCell = this.Ichiran.CurrentCell.CellIndex;
                        errorRow = this.Ichiran.CurrentCell.RowIndex;

                        bMove = true;
                        bRenkeiChk = false;

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E263", "連携用CD");

                        this.Ichiran.CurrentCell = this.Ichiran[errorRow, Const.BankHoshuConstans.RENKEI_CD];

                        e.Cancel = true;
                        bChangeBackColor = true;
                        GcMultiRow gc = sender as GcMultiRow;
                        if (gc != null && gc.EditingControl != null)
                        {
                            ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                        }

                        return;
                    }
                    else
                    {
                        bRenkeiChk = true;
                    }
                }
            }

            if (e.CellName.Equals(Const.BankHoshuConstans.RENKEI_CD))
            {
                bool isNoErr = this.logic.RenkeiDuplicationCheck(e);
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E022", "入力された連携用CD");
                    return;
                }
            }
            //Bao 2016/03/30 #16768 M198 銀行入力 -End
        }

        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = true;
            }
            //Bao 2016/03/30 #16768 M198 銀行入力 -Start
            PreviousValue = string.Empty;
            if (this.Ichiran[e.RowIndex, Const.BankHoshuConstans.BANK_CD].Value != null)
            {
                PreviousValue = this.Ichiran[e.RowIndex, Const.BankHoshuConstans.BANK_CD].Value.ToString();
            }

            if (!bRenkeiChk)
            {
                this.Ichiran.CurrentCell = this.Ichiran[errorRow, Const.BankHoshuConstans.RENKEI_CD];
                bRenkeiChk = true;
                if (e.CellName.Equals(Const.BankHoshuConstans.RENKEI_CD)
                    || e.CellName.Equals(Const.BankHoshuConstans.BANK_CD))
                {
                    bMove = false;
                }
            }
            if (bChangeBackColor)
            {
                this.Ichiran[e.RowIndex, Const.BankHoshuConstans.BANK_CD].UpdateBackColor(false);
            }
            //Bao 2016/03/30 #16768 M198 銀行入力 -End
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BankHoshuForm_Shown(object sender, EventArgs e)
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
