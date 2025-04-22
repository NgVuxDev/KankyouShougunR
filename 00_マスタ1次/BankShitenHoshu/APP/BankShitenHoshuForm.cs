// $Id: BankShitenHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using BankShitenHoshu.Logic;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Collections.Generic;
using System.Text;

namespace BankShitenHoshu.APP
{
    /// 銀行支店保守画面
    /// <summary>  
    /// </summary>
    [Implementation]
    public partial class BankShitenHoshuForm : SuperForm
    {
        /// <summary>
        /// 銀行支店保守画面ロジック
        /// </summary>
        private BankShitenHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        // thongh 2015/12/28 #1983 start
        /// <summary>
        /// 前回値チェック用変数(銀行CD用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForBank_Cd = new Dictionary<string, string>();
        // thongh 2015/12/28 #1983 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BankShitenHoshuForm()
            : base(WINDOW_ID.M_BANK_SHITEN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new BankShitenHoshuLogic(this);

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
            this.logic.WindowInit();
            // 初回検索
            if (!string.IsNullOrEmpty(this.BANK_CD.Text))
            {
                this.Search(null, e);
            }

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

            var messageShowLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.BANK_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "銀行名");
                this.BANK_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1983
                int count = this.logic.Search();
                if (count == 0)
                {
                    messageShowLogic.MessageBoxShow("C001");
                }
                else if (count == -1)
                {
                    return;
                }
                else
                {
                    if (this.logic.SetIchiran())
                    {
                        return;
                    }
                }
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

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
            var messageShowLogic = new MessageBoxShowLogic();

            if ((string.IsNullOrEmpty(this.BANK_CD.Text)) ||
                (this.logic.SearchResultAll == null))
            {
                messageShowLogic.MessageBoxShow("E001", "銀行名");
                this.BANK_CD.Focus();
                return;
            }
            else if (!base.RegistErrorFlag)
            {
                if (this.logic.CreateEntity(false))
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
            var messageShowLogic = new MessageBoxShowLogic();

            if ((string.IsNullOrEmpty(this.BANK_CD.Text)) ||
                (this.logic.SearchResultAll == null))
            {
                messageShowLogic.MessageBoxShow("E001", "銀行名");
                this.BANK_CD.Focus();
                return;
            }
            else if (!base.RegistErrorFlag && this.logic.CheckDelete())
            {
                if (this.logic.CreateEntity(true))
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

            if (!string.IsNullOrEmpty(this.BANK_CD.Text))
            {
                Search(sender, e);
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1983
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
            Properties.Settings.Default.BanksValue_Text = this.BANK_CD.Text;
            if (string.IsNullOrEmpty(this.BANK_CD.Text))
            {
                Properties.Settings.Default.BanksName_Text = string.Empty;
            }
            else
            {
                var bank = this.logic.getBank(this.BANK_CD.Text);
                if (bank != null && bank.Length == 1)
                {
                    Properties.Settings.Default.BanksName_Text = this.BANK_NAME_RYAKU.Text;
                }
                else
                {
                    Properties.Settings.Default.BanksName_Text = string.Empty;
                }
            }

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
        /// 銀行支店CDの入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.Ichiran_CellValidating(sender, e);
        }

        /// <summary>
        /// 銀行支店CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            this.logic.Ichiran_RowValidating(sender, e);
        }

        /// <summary>
        /// 銀行名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_Validated(object sender, EventArgs e)
        {
            ClearMesai();
        }

        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 銀行CDが空白の場合、明細入力ができないようにする
            if ((this.BANK_CD.TextLength <= 0) ||
                (this.logic.SearchResultAll == null))
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
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = true;
            }
            if (e.CellName.Equals(Const.BankShitenHoshuConstans.BANK_SHITEN_CD))
            {
                this.logic.beforeBankShitenCd = Convert.ToString(this.Ichiran.CurrentRow.Cells[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].Value);
            }

            if (this.logic.isRenkeiError)
            {
                this.Ichiran.CurrentCell = this.Ichiran.Rows[this.logic.errorRow].Cells[this.logic.errorCell];
                this.logic.isRenkeiError = false;
            }
        }

        /// <summary>
        /// 編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IchiranEditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            this.logic.Ichiran_CellValidated(sender, e);

            #region VANTRUONG
            switch (e.CellName)
            {
                case "BANK_SHITEN_NAME":
                case "KOUZA_NAME":
                    Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                    byte[] byteArray;
                    var mBank = this.logic.bankDao.GetDataByCd(this.BANK_CD.Text);
                    if (mBank != null)
                    {
                        string checkString = string.Format("{0}{1}{2}{3}{4}", mBank.BANK_NAME, this.Ichiran[e.RowIndex, "BANK_SHITEN_NAME"].Value, this.Ichiran[e.RowIndex, "KOUZA_SHURUI"].Value, this.Ichiran[e.RowIndex, "KOUZA_NO"].Value, this.Ichiran[e.RowIndex, "KOUZA_NAME"].Value);
                        byteArray = encoding.GetBytes(checkString);
                        if (byteArray.Length > 75)
                        {
                            errmessage.MessageBoxShow("W010");
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion
        }
        // thongh 2015/12/28 #1983 start
        /// <summary>
        /// 明細一覧をクリア
        /// </summary>
        public void ClearMesai()
        {
            if (beforeValuesForBank_Cd.ContainsKey(this.BANK_CD.Name) && !this.ChangeValue(this.BANK_CD.Text).Equals(this.beforeValuesForBank_Cd[this.BANK_CD.Name]))
            {
                this.Ichiran.CellValidated -= Ichiran_CellValidated;
                Ichiran.DataSource = null;
                Ichiran.AllowUserToAddRows = false;
                this.logic.SearchResult = null;
                this.logic.SearchResultAll = null;
                this.logic.SearchString = null;
                this.Ichiran.CellValidated += Ichiran_CellValidated;
            }
            else if (this.BANK_CD.Text.Length >= 3)
            {
                this.logic.SearchBankName();
            }
            // 前回値チェック用データをセット
            if (beforeValuesForBank_Cd.ContainsKey(this.BANK_CD.Name))
            {
                beforeValuesForBank_Cd[this.BANK_CD.Name] = this.ChangeValue(this.BANK_CD.Text);
            }
            else
            {
                beforeValuesForBank_Cd.Add(this.BANK_CD.Name, this.ChangeValue(this.BANK_CD.Text));
            }
        }

        /// <summary> 指定Valueに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <returns>指定Valueに変換後文字列</returns>
        private string ChangeValue(string value)
        {
            // フォーマット変換後文字列
            string ret = string.Empty;

            // 引数がブランクの場合はブランクを返す
            if (value.Trim() != string.Empty)
            {
                ret = Convert.ToInt16(value).ToString();
            }

            return ret;
        }
        // thongh 2015/12/28 #1983 end

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
