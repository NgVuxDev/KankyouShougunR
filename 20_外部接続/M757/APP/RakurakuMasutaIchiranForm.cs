// $Id$
using System;
using System.Reflection;
using System.Windows.Forms;
using Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Logic;
using Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Const;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.IchiranCommon.APP;
using r_framework.CustomControl;

namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP
{
    public partial class RakurakuMasutaIchiranForm : IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;

        /// <summary>
        /// イベントフラグ
        /// </summary>
        internal bool EventSetFlg = false;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyoushaCD = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public RakurakuMasutaIchiranForm(DENSHU_KBN denshuKbn) : base(denshuKbn, false)
        {
            InitializeComponent();
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // デフォルトパターンを設定する
            base.OnLoad(e);
            // 初回のみ
            if (this.businessLogic == null)
            {
                // ビジネスロジックの初期化
                this.businessLogic = new LogicClass(this);

                // 画面初期化
                this.businessLogic.WindowInit();

                this.businessLogic.InitFrom();

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;
            }

            this.PatternReload();

            // パターンヘッダのみ表示
            this.ShowData();
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            if (!this.DesignMode)
            {
                DialogResult dlgResult = DialogResult.Yes;

                if (dlgResult == DialogResult.Yes && this.Table != null && this.PatternNo != 0)
                {
                    // 明細に表示
                    if(!this.customDataGridView1.Columns.Contains("選択"))
                    {
                        GridViewCheckBoxColumn boxColumn = new GridViewCheckBoxColumn();
                        boxColumn.Name = "選択";
                        boxColumn.HeaderText = "";
                        boxColumn.ReadOnly = false;
                        this.customDataGridView1.Columns.Add(boxColumn);
                    }
                    else
                    {
                        GridViewCheckBoxColumn boxColumn = new GridViewCheckBoxColumn();
                        boxColumn.Name = "選択";
                        boxColumn.HeaderText = "          ";
                        boxColumn.ReadOnly = false;
                        this.customDataGridView1.Columns.Remove("選択");
                        this.customDataGridView1.Columns.Insert(0, boxColumn);
                    }

                    if (this.businessLogic.CheckDataTableColumnName(ConstansIC.EMAIL))
                    {
                        this.Table.Columns[ConstansIC.EMAIL].ReadOnly = false;
                        this.Table.Columns[ConstansIC.EMAIL].MaxLength = 100;
                    }
                    if (this.businessLogic.CheckDataTableColumnName(ConstansIC.EMAIL_ADDRESS1))
                    {
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS1].ReadOnly = false;
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS1].MaxLength = 100;
                    }
                    if (this.businessLogic.CheckDataTableColumnName(ConstansIC.EMAIL_ADDRESS2))
                    {
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS2].ReadOnly = false;
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS2].MaxLength = 100;
                    }
                    if (this.businessLogic.CheckDataTableColumnName(ConstansIC.EMAIL_ADDRESS3))
                    {
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS3].ReadOnly = false;
                        this.Table.Columns[ConstansIC.EMAIL_ADDRESS3].MaxLength = 100;
                    }

                    this.logic.CreateDataGridView(this.Table);
                    this.businessLogic.SetDataColumnsReadOnly();
                    foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var ia = cell as ICustomAutoChangeBackColor;
                            if (ia != null) ia.UpdateBackColor(); //色設定がない場合に対応させる
                            else cell.UpdateBackColor(false); //読み取り専用だと最初に色を付ける
                        }
                    }
                    this.HideKeyColumns();

                    this.customDataGridView1.Columns[ConstansIC.RENKEI].Width = 45;
                    if (this.businessLogic.CheckDataGridColumnName(ConstansIC.EMAIL))
                    {
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)this.customDataGridView1.Columns[ConstansIC.EMAIL];
                        col.MaxInputLength = 100;
                        col.ValueType = typeof(string);
                        this.customDataGridView1.Columns[ConstansIC.EMAIL].Width = 200; 
                    }

                    if (this.businessLogic.CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS1))
                    {
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS1];
                        col.MaxInputLength = 100;
                        col.ValueType = typeof(string);
                        this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS1].Width = 200;
                    }

                    if (this.businessLogic.CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS2))
                    {
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS2];
                        col.MaxInputLength = 100;
                        col.ValueType = typeof(string);
                        this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS2].Width = 200;
                    }

                    if (this.businessLogic.CheckDataGridColumnName(ConstansIC.EMAIL_ADDRESS3))
                    {
                        DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS3];
                        col.MaxInputLength = 100;
                        col.ValueType = typeof(string);
                        this.customDataGridView1.Columns[ConstansIC.EMAIL_ADDRESS3].Width = 200;
                    }

                    this.customDataGridView1.RefreshEdit();
                    this.customDataGridView1.Refresh();
                }
            }
        }

        /// <summary>
        /// 画面連携に使用するキー取得項目を隠す
        /// </summary>
        private void HideKeyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {
                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    if (col.Name == ConstansIC.KEY_ID0 || col.Name == ConstansIC.KEY_ID1 || col.Name == ConstansIC.KEY_ID2 || 
                        col.Name == ConstansIC.RAKU_ID || col.Name == ConstansIC.SHOSHIKI_KBN || col.Name == ConstansIC.RAKURAKU_CUSTOMER_CD)
                    {
                        col.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                // 表示条件保存
                this.businessLogic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                // 画面が閉じれなくなるのでログのみ
                LogUtility.Fatal("OnClosing", ex);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            base.OnSizeChanged(e);
        }

        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (String.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = String.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
            }
            else if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        public void PopupAfterGenba()
        {
            this.beforGyoushaCD = this.GYOUSHA_CD.Text;
        }

        public void PopupBeforeGenba()
        {
            this.businessLogic.CheckGenba();
        }

        /// <summary>
        /// 現場一覧のShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaIchiranForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を設定します
            this.businessLogic.InitHeaderShown();
        }

        private void RAKURAKU_CSV_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.businessLogic.newValue) { this.businessLogic.newValue = false; return; }

            this.businessLogic.msgLogic = new MessageBoxShowLogic();
            if (this.RAKURAKU_CSV_KBN.Text == "1")
            {
                if (this.customDataGridView1.Rows.Count > 0)
                {
                    if (this.businessLogic.msgLogic.MessageBoxShow("C123") == DialogResult.Yes)
                    {
                        this.Table.Rows.Clear();
                        this.customDataGridView1.Refresh();
                        this.businessLogic.parentForm.bt_func4.Enabled = true;
                        this.businessLogic.newValue = false;
                    }
                    else
                    {
                        this.businessLogic.newValue = true;
                        this.RAKURAKU_CSV_KBN.Text = "2";
                    }
                }
                else this.businessLogic.parentForm.bt_func4.Enabled = true;
            }
            else if (this.RAKURAKU_CSV_KBN.Text == "2")
            {
                if (this.customDataGridView1.Rows.Count > 0)
                {
                    if (this.businessLogic.msgLogic.MessageBoxShow("C123") == DialogResult.Yes)
                    {
                        this.Table.Rows.Clear();
                        this.customDataGridView1.Refresh();
                        this.businessLogic.parentForm.bt_func4.Enabled = false;
                        this.businessLogic.newValue = false;
                    }
                    else
                    {
                        this.businessLogic.newValue = true;
                        this.RAKURAKU_CSV_KBN.Text = "1";
                    }
                }
                else this.businessLogic.parentForm.bt_func4.Enabled = false;
            }
        }

        private void RAKURAKU_CSV_KBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (ActiveControl != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    if (forward) this.businessLogic.headerForm.RAKURAKU_MEISAI_RENKEI.Focus();
                }    
            }
        }

        private void SEIKYUU_SHO_SHOSHIKI_1_TextChanged(object sender, EventArgs e)
        {
            if (this.SEIKYUU_SHO_SHOSHIKI_1.Text == "1")
            {
                this.GYOUSHA_CD.Enabled = false;
                this.GYOUSHA_SEARCH_BUTTON.Enabled = false;
                this.GENBA_CD.Enabled = false;
                this.GENBA_SEARCH_BUTTON.Enabled = false;

                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }   
            else if (this.SEIKYUU_SHO_SHOSHIKI_1.Text == "2")
            {
                this.GYOUSHA_CD.Enabled = true;
                this.GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.GENBA_CD.Enabled = false;
                this.GENBA_SEARCH_BUTTON.Enabled = false;

                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }
            else if (this.SEIKYUU_SHO_SHOSHIKI_1.Text == "3" || this.SEIKYUU_SHO_SHOSHIKI_1.Text == "9")
            {
                this.GYOUSHA_CD.Enabled = true;
                this.GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.GENBA_CD.Enabled = true;
                this.GENBA_SEARCH_BUTTON.Enabled = true;
            }

            if (this.Table != null)
            {
                this.Table.Rows.Clear();
                this.customDataGridView1.Refresh();
            }    
        }

        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.businessLogic.CheckGenba();
        }
    }
}

