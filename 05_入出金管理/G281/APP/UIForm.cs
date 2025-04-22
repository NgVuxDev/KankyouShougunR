using System;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.APP;
using r_framework.Utility;
using r_framework.BrowseForFolder;
using r_framework.Logic;
using System.ComponentModel;
using r_framework.Entity;
using System.Linq;
using r_framework.CustomControl.DataGridCustomControl;
using DataGridViewCheckBoxColumnHeader;
using System.Windows.Forms;
using r_framework.CustomControl;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        private NyuukinDataTorikomi.LogicClass ichiranLogic;

        private UIHeader headerForm;

        internal string previousBankShitenCd = string.Empty;

        internal bool initFlag = true;

        internal int _indexRow = -1;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="strNyuukinNum">入金番号</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="bumonCd">部門CD</param>
        /// <param name="denpyouHidukeForm">伝票日付</param>
        public UIForm(UIHeader headerForm)
            : base(DENSHU_KBN.NYUUKIN_DATA_TORIKOMI_ICHIRAN, false)
        {
            this.InitializeComponent();

            ichiranLogic = new LogicClass(this);

            this.headerForm = headerForm;

            ichiranLogic.SetHeader(this.headerForm);

        }

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (initFlag)
            {
                this.ichiranLogic.WindowInit();
                initFlag = false;
            }
            this.headerForm.ReadDataNumber.Text = "0";

            this.customDataGridView1.IsBrowsePurpose = false;
            // パターンをロードする
            this.PatternReload();

            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            this.ShowHeader();

            this.ResizeColumns();

            this.FormatColumns();
        }
        /// <summary>
        /// 画面Shown処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            this.ResizeColumns();

            this.FormatColumns();

            this.SEARCH_TORIKOMI.Focus();

        }
        /// <summary>
        /// 
        /// </summary>
        public override void  AdjustColumnSizeComplete()
        {
            this.ResizeColumns();
 	         base.AdjustColumnSizeComplete();
        }
        /// <summary>
        /// OnKeyDown
        /// </summary>
        /// <param name="e"></param>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if ((Keys)keyData == Keys.Tab || keyData == Keys.Enter)
            {
                if (this.ActiveControl != null && (this.ActiveControl.Name == "customDataGridView1" || (this.ActiveControl.Parent != null && this.ActiveControl.Parent.Parent != null && this.ActiveControl.Parent.Parent.Name == "customDataGridView1")))
                {
                    if (this.customDataGridView1.RowCount > 0)
                    {
                        if (this.customDataGridView1.CurrentCell != null
                            && this.customDataGridView1.CurrentCell.RowIndex == this.customDataGridView1.RowCount - 1
                            && ((this.customDataGridView1.Columns[this.customDataGridView1.CurrentCell.ColumnIndex].Name == ConstClass.COL_SAKUJO && this.customDataGridView1[ConstClass.COL_TORIHIKISAKI_CD, this.customDataGridView1.CurrentCell.RowIndex].ReadOnly)
                            || (this.customDataGridView1.Columns[this.customDataGridView1.CurrentCell.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)))
                        {
                            if (this.customDataGridView1[ConstClass.COL_SAKUSEI, 0].ReadOnly == false)
                            {
                                this.customDataGridView1.CurrentCell = this.customDataGridView1[ConstClass.COL_SAKUSEI, 0];
                            }
                            else
                            {
                                this.customDataGridView1.CurrentCell = this.customDataGridView1[ConstClass.COL_SAKUJO, 0];
                            }
                            this.SEARCH_TORIKOMI.Focus();
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 検索

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
           
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;

                this.logic.CreateDataGridView(this.ichiranLogic.SearchResult); 

                this.HideSystemColumn();

                this.ResizeColumns();

                this.FormatColumns();

                this.SetStateGrid();
            }
        }



        /// <summary>
        /// ヘッダー表示
        /// </summary>
        public void ShowHeader()
        {
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
            }
        }

        #endregion

        #region Control GridView Event
        /// <summary>
        /// 
        /// </summary>
        public void TorihikisakiPopupAfter()
        {
            if (this.customDataGridView1.CurrentCell != null && this.customDataGridView1.Columns[customDataGridView1.CurrentCell.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)
            {
                if (!this.ichiranLogic.TorihikisakiValidating(this.customDataGridView1.CurrentCell.ColumnIndex, this.customDataGridView1.CurrentCell.RowIndex))
                {
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E012", "取引先");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TorihikisakiPopupBefore()
        {
            if (this.customDataGridView1.CurrentCell != null && this.customDataGridView1.Columns[customDataGridView1.CurrentCell.ColumnIndex].Name == ConstClass.COL_TORIHIKISAKI_CD)
            {
                DgvCustomAlphaNumTextBoxCell cell = this.customDataGridView1.CurrentCell as DgvCustomAlphaNumTextBoxCell;
                string errorCd = (this.customDataGridView1[ConstClass.COL_ERROR_CD_ORIG, this.customDataGridView1.CurrentCell.RowIndex].EditedFormattedValue == null ? string.Empty : this.customDataGridView1[ConstClass.COL_ERROR_CD_ORIG, this.customDataGridView1.CurrentCell.RowIndex].EditedFormattedValue.ToString());
                string furikomiJinmei = (this.customDataGridView1[ConstClass.COL_FURIKOMI_JINMEI, this.customDataGridView1.CurrentCell.RowIndex].EditedFormattedValue == null ? string.Empty : this.customDataGridView1[ConstClass.COL_FURIKOMI_JINMEI, this.customDataGridView1.CurrentCell.RowIndex].EditedFormattedValue.ToString());
                cell.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<PopupSearchSendParamDto>();
                PopupSearchSendParamDto send1 = new PopupSearchSendParamDto();
                send1.KeyName = "POPUP_TYPE";
                send1.Value = errorCd;
                cell.PopupSearchSendParams.Add(send1);
                PopupSearchSendParamDto send2 = new PopupSearchSendParamDto();
                send2.KeyName = "FURIKOMI_JINMEI";
                send2.Value = furikomiJinmei;
                cell.PopupSearchSendParams.Add(send2);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void headerSakusei_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {
            int currentRow = -1;
            int currentCell = -1;
            if (this.customDataGridView1.CurrentCell != null)
            {
                currentRow = this.customDataGridView1.CurrentCell.RowIndex;
                currentCell = this.customDataGridView1.CurrentCell.ColumnIndex;
                this.customDataGridView1.CurrentCell = null;
            }
            for (int i = 0; i < this.customDataGridView1.RowCount; i++)
            {
                if (customDataGridView1[ConstClass.COL_SAKUSEI, i].ReadOnly == false)
                {
                    customDataGridView1[ConstClass.COL_SAKUSEI, i].Value = e.CheckedState;
                    if (e.CheckedState == true)
                    {
                        customDataGridView1[ConstClass.COL_SAKUJO, i].Value = false;
                    }
                }
            }
            if (currentRow >= 0 && currentCell >= 0)
            {
                this.customDataGridView1.CurrentCell = this.customDataGridView1[currentCell, currentRow];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void headerSakujo_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {
            int currentRow = -1;
            int currentCell = -1;
            if (this.customDataGridView1.CurrentCell != null)
            {
                currentRow = this.customDataGridView1.CurrentCell.RowIndex;
                currentCell = this.customDataGridView1.CurrentCell.ColumnIndex;
                this.customDataGridView1.CurrentCell = null;
            }
            for (int i = 0; i < this.customDataGridView1.RowCount; i++)
            {
                if (customDataGridView1[ConstClass.COL_SAKUSEI, i].ReadOnly == false && e.CheckedState == true)
                {
                    customDataGridView1[ConstClass.COL_SAKUSEI, i].Value = false;
                    ((DataGridviewCheckboxHeaderCell)customDataGridView1.Columns[ConstClass.COL_SAKUSEI].HeaderCell).Checked = false;
                }
                customDataGridView1[ConstClass.COL_SAKUJO, i].Value = e.CheckedState;
            }
            if (currentRow >= 0 && currentCell >= 0)
            {
                this.customDataGridView1.CurrentCell = this.customDataGridView1[currentCell, currentRow];
            }
        }

        #endregion

        #region Format GridView
        /// <summary>
        /// 
        /// </summary>
        public void FormatColumns()
        {
            if (this.Table != null)
            {
                this.customDataGridView1.Columns[ConstClass.COL_SAKUSEI].Tag = "伝票を作成する明細をチェックしてください";
                this.customDataGridView1.Columns[ConstClass.COL_SAKUJO].ReadOnly = false;
                this.customDataGridView1.Columns[ConstClass.COL_SAKUJO].Tag = "削除する場合にチェックを付けてください";
                this.customDataGridView1.Columns[ConstClass.COL_KINGAKU].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                this.customDataGridView1.Columns[ConstClass.COL_KINGAKU].DefaultCellStyle.Format = "#,##0";
                this.customDataGridView1.Columns[ConstClass.COL_DENPYOU_SAKUSEI].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                DataGridviewCheckboxHeaderCell headerSakusei = new DataGridviewCheckboxHeaderCell();
                headerSakusei.Value = ConstClass.COL_SAKUSEI;
                this.customDataGridView1.Columns[ConstClass.COL_SAKUSEI].HeaderCell = headerSakusei;
                headerSakusei.OnCheckBoxClicked += new DataGridviewCheckboxHeaderCell.datagridviewcheckboxHeaderEventHander(headerSakusei_OnCheckBoxClicked);
                DataGridviewCheckboxHeaderCell headerSakujo = new DataGridviewCheckboxHeaderCell();
                headerSakujo.Value = ConstClass.COL_SAKUJO;
                headerSakujo.OnCheckBoxClicked += new DataGridviewCheckboxHeaderCell.datagridviewcheckboxHeaderEventHander(headerSakujo_OnCheckBoxClicked);
                this.customDataGridView1.Columns[ConstClass.COL_SAKUJO].HeaderCell = headerSakujo;

                if (this.customDataGridView1.Columns.Contains(ConstClass.COL_TORIHIKISAKI_CD))
                {
                    int idxTorihikiCol = this.customDataGridView1.Columns[ConstClass.COL_TORIHIKISAKI_CD].Index;
                    this.customDataGridView1.Columns.Remove(ConstClass.COL_TORIHIKISAKI_CD);
                    DgvCustomAlphaNumTextBoxColumn colTorihikisakiCd = new DgvCustomAlphaNumTextBoxColumn();
                    colTorihikisakiCd.HeaderText = ConstClass.COL_TORIHIKISAKI_CD;
                    colTorihikisakiCd.Name = ConstClass.COL_TORIHIKISAKI_CD;
                    colTorihikisakiCd.DBFieldsName = ConstClass.COL_TORIHIKISAKI_CD;
                    colTorihikisakiCd.DataPropertyName = ConstClass.COL_TORIHIKISAKI_CD;
                    colTorihikisakiCd.CharactersNumber = 6;
                    colTorihikisakiCd.ZeroPaddengFlag = true;
                    colTorihikisakiCd.Width = 80;
                    //colTorihikisakiCd.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
                    colTorihikisakiCd.PopupWindowName = "入金データ取込取引先検索";
                    colTorihikisakiCd.PopupWindowId = WINDOW_ID.M_TORIHIKISAKI;
                    colTorihikisakiCd.SetFormField = "取引先CD,取引先名";
                    colTorihikisakiCd.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
                    colTorihikisakiCd.ReadOnly = false;
                    colTorihikisakiCd.PopupAfterExecuteMethod = "TorihikisakiPopupAfter";
                    colTorihikisakiCd.PopupBeforeExecuteMethod = "TorihikisakiPopupBefore";
                    JoinMethodDto join1 = new JoinMethodDto();
                    join1.Join = JOIN_METHOD.INNER_JOIN;
                    join1.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
                    join1.RightTable = "M_TORIHIKISAKI";
                    join1.LeftKeyColumn = "TORIHIKISAKI_CD";
                    join1.RightKeyColumn = "TORIHIKISAKI_CD";

                    JoinMethodDto join2 = new JoinMethodDto();
                    join2.Join = JOIN_METHOD.WHERE;
                    join2.LeftTable = "M_TORIHIKISAKI_SEIKYUU";
                    join2.SearchCondition = new System.Collections.ObjectModel.Collection<SearchConditionsDto>();
                    SearchConditionsDto search2 = new SearchConditionsDto();
                    search2.And_Or = CONDITION_OPERATOR.AND;
                    search2.LeftColumn = "TORIHIKI_KBN_CD";
                    search2.ValueColumnType = DB_TYPE.SMALLINT;
                    search2.Value = "2";
                    join2.SearchCondition.Add(search2);

                    colTorihikisakiCd.popupWindowSetting = new System.Collections.ObjectModel.Collection<JoinMethodDto>();
                    colTorihikisakiCd.popupWindowSetting.Add(join1);
                    colTorihikisakiCd.popupWindowSetting.Add(join2);
                    colTorihikisakiCd.PopupSearchSendParams = new System.Collections.ObjectModel.Collection<PopupSearchSendParamDto>();

                    this.customDataGridView1.Columns.Insert(idxTorihikiCol, colTorihikisakiCd);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ResizeColumns()
        {
            if (this.Table != null)
            {
                this.customDataGridView1.Columns[ConstClass.COL_SAKUSEI].Width = 65;
                this.customDataGridView1.Columns[ConstClass.COL_SAKUJO].Width = 65;
                //this.customDataGridView1.Columns[ConstClass.COL_DENPYOU_SAKUSEI].Width = 70;
                //this.customDataGridView1.Columns[ConstClass.COL_YONYUU_DATE].Width = 110;
                //this.customDataGridView1.Columns[ConstClass.COL_TORIHIKISAKI_CD].Width = 80;
                //this.customDataGridView1.Columns[ConstClass.COL_TORIHIKISAKI_NAME].Width = 160;
                //this.customDataGridView1.Columns[ConstClass.COL_NYUUKINSAKI_CD].Width = 80;
                //this.customDataGridView1.Columns[ConstClass.COL_NYUUKINSAKI_NAME].Width = 160;
                //this.customDataGridView1.Columns[ConstClass.COL_KINGAKU].Width = 100;
                //this.customDataGridView1.Columns[ConstClass.COL_FURIKOMI_JINMEI].Width = 350;
                //this.customDataGridView1.Columns[ConstClass.COL_BANK_CD].Width = 60;
                //this.customDataGridView1.Columns[ConstClass.COL_BANK_NAME].Width = 160;
                //this.customDataGridView1.Columns[ConstClass.COL_BANK_SHITEN_CD].Width = 85;
                //this.customDataGridView1.Columns[ConstClass.COL_BANK_SHITEN_NAME].Width = 160;
                //this.customDataGridView1.Columns[ConstClass.COL_KOUZA_SHURUI].Width = 80;
                //this.customDataGridView1.Columns[ConstClass.COL_KOUZA_NO].Width = 80;
                //this.customDataGridView1.Columns[ConstClass.COL_ERROR_NAME].Width = 280;
            }
        }

        /// <summary>
        /// システム必須列を非表示にします。
        /// </summary>
        private void HideSystemColumn()
        {
            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_TORIKOMI_NUMBER))
            {
                this.customDataGridView1.Columns[ConstClass.COL_TORIKOMI_NUMBER].Visible = false;
            }

            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_ROW_NUMBER))
            {
                this.customDataGridView1.Columns[ConstClass.COL_ROW_NUMBER].Visible = false;
            }

            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_ERROR_CD))
            {
                this.customDataGridView1.Columns[ConstClass.COL_ERROR_CD].Visible = false;
            }

            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_ERROR_CD_ORIG))
            {
                this.customDataGridView1.Columns[ConstClass.COL_ERROR_CD_ORIG].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_ERROR_NAME_ORIG))
            {
                this.customDataGridView1.Columns[ConstClass.COL_ERROR_NAME_ORIG].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_KOUZA_NAME))
            {
                this.customDataGridView1.Columns[ConstClass.COL_KOUZA_NAME].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_TEKIYOU_NAIYOU))
            {
                this.customDataGridView1.Columns[ConstClass.COL_TEKIYOU_NAIYOU].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(ConstClass.COL_TIME_STAMP))
            {
                this.customDataGridView1.Columns[ConstClass.COL_TIME_STAMP].Visible = false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void SetStateGrid()
        {
              //背景色対応
            foreach (DataGridViewRow dgRow in this.customDataGridView1.Rows)
            {
                string errorCd = dgRow.Cells[ConstClass.COL_ERROR_CD].Value == null ? string.Empty : dgRow.Cells[ConstClass.COL_ERROR_CD].Value.ToString();
                if (String.IsNullOrEmpty(errorCd))
                {
                    dgRow.Cells[ConstClass.COL_TORIHIKISAKI_CD].ReadOnly = true;
                    dgRow.Cells[ConstClass.COL_SAKUSEI].ReadOnly = false;
                }
                else if (errorCd == "3" || errorCd == "4")
                {
                    dgRow.Cells[ConstClass.COL_TORIHIKISAKI_CD].ReadOnly = false;
                    dgRow.Cells[ConstClass.COL_SAKUSEI].ReadOnly = true;
                }
                else if (errorCd == "1" || errorCd == "2")
                {
                    dgRow.Cells[ConstClass.COL_TORIHIKISAKI_CD].ReadOnly = true;
                    dgRow.Cells[ConstClass.COL_SAKUSEI].ReadOnly = true;
                }
                else
                {
                    dgRow.Cells[ConstClass.COL_SAKUSEI].ReadOnly = true;
                }

            }
        }
        #endregion

        #region Control Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEARCH_TORIKOMI_Click(object sender, EventArgs e)
        {
            string forderPath = ichiranLogic.ForderSearch();
            if (!String.IsNullOrEmpty(forderPath))
            {
                this.TORIKOMI.Text = forderPath;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_SHITEN_CD_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHITEN_NAME_RYAKU.Text = string.Empty;
            this.KOUZA_SHURUI.Text = string.Empty;
            this.KOUZA_NO.Text = string.Empty;

            this.previousBankShitenCd = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_TextChanged(object sender, EventArgs e)
        {
            this.BANK_NAME_RYAKU.Text = String.Empty;
            this.BANK_SHITEN_CD.Text = String.Empty;
            this.BANK_SHITEN_NAME_RYAKU.Text = string.Empty;
        }

        /// <summary>
        /// 振込銀行支店CDのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //20151005 hoanghm start
            var bankCd = this.BANK_CD.Text;
            var bankShitenCd = this.BANK_SHITEN_CD.Text;
            //
            var kouzaShurui = this.KOUZA_SHURUI.Text;
            var kouzaNo = this.KOUZA_NO.Text;
            //

            if (string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.BANK_SHITEN_CD.Focus();
                return;
            }

            if (bankShitenCd.Equals(this.previousBankShitenCd))
            {
                return;
            }

            if (!string.IsNullOrEmpty(bankCd) && !string.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.ichiranLogic.GetBankShiten(bankCd, bankShitenCd);
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店");

                    this.BANK_SHITEN_CD.Text = String.Empty;
                    this.KOUZA_SHURUI.Text = String.Empty;
                    this.KOUZA_NO.Text = String.Empty;

                    this.previousBankShitenCd = string.Empty;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.BANK_SHITEN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;

                    this.previousBankShitenCd = bankShiten.BANK_SHITEN_CD;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (!string.IsNullOrEmpty(kouzaShurui) && !string.IsNullOrEmpty(kouzaNo))
                    {
                        var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd && b.KOUZA_SHURUI == kouzaShurui && b.KOUZA_NO == kouzaNo).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                        this.BANK_SHITEN_NAME_RYAKU.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                        this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                        this.KOUZA_NO.Text = bankShiten.KOUZA_NO;

                        this.previousBankShitenCd = bankShiten.BANK_SHITEN_CD;
                    }
                    else
                    {
                        //Show popup
                        System.Windows.Forms.DialogResult result = CustomControlExtLogic.PopUp(this.BANK_SHITEN_CD);

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            this.previousBankShitenCd = this.BANK_SHITEN_CD.Text;

                        }

                        e.Cancel = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAKUSEI_BI_TO_DoubleClick(object sender, EventArgs e)
        {
            SAKUSEI_BI_TO.Text = SAKUSEI_BI_FROM.Text;
        }

        #endregion

        internal string BeforeSelTorihikisaki = string.Empty;
        public void SEL_TORIHIKISAKI_CD_After(ICustomControl control, DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes) return;
            this.ichiranLogic.SetDataTorihikisaki(this.SEL_TORIHIKISAKI_CD.Text);
        }

        private void SEL_TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            BeforeSelTorihikisaki = this.SEL_TORIHIKISAKI_CD.Text;
        }
        private void SEL_TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            if (BeforeSelTorihikisaki == this.SEL_TORIHIKISAKI_CD.Text) return;
            this.ichiranLogic.SetDataTorihikisaki(this.SEL_TORIHIKISAKI_CD.Text);
        }
        private void SEL_SETTEI_BTN_1_Click(object sender, EventArgs e)
        {
            this.ichiranLogic.SetFurikomiBtn(this.SEL_SENTAKU_SAKI, this.SEL_FURIKOMI_JINMEI_1, "振込人名１");
        }
        private void SEL_SETTEI_BTN_2_Click(object sender, EventArgs e)
        {
            this.ichiranLogic.SetFurikomiBtn(this.SEL_SENTAKU_SAKI, this.SEL_FURIKOMI_JINMEI_2, "振込人名２");
        }
    }
}
