using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Common.IchiranCommon.APP;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukaiWanSign
{
    /// <summary>
    /// 電子契約最新照会（WAN-Sign）
    /// </summary>
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass shoukaiLogic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 排出事業者CD
        /// </summary>
        internal string oldGyoushaCd = string.Empty;

        /// <summary>
        /// 排出事業場CD
        /// </summary>
        internal string oldGenbaCd = string.Empty;

        internal int rowIndex = 0;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.DENSHI_KEIYAKU_SAISHIN_SHOUKAI_WAN_SIGN, false)
        {
            InitializeComponent();

            // 汎用検索機能が未実装の為、汎用検索は非表示
            this.searchString.Visible = false;
            this.customSearchHeader1.Visible = true;
            this.customDataGridView1.TabIndex = 120;
            this.customDataGridView1.Location = new System.Drawing.Point(5, 156);
            this.customDataGridView1.Size = new System.Drawing.Size(997, 285);

            // 行の追加オプション(false)
            this.customDataGridView1.AllowUserToAddRows = false;
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.shoukaiLogic == null)
            {
                this.shoukaiLogic = new LogicClass(this);

                //初期化、初期表示
                if (!this.shoukaiLogic.WindowInit())
                {
                    return;
                }
            }

            //パターン一覧
            this.PatternReload();

            // ボタン制御
            this.shoukaiLogic.ButtonEnabledControl();

            //一覧内のチェックボックスの設定
            this.shoukaiLogic.HeaderCheckBoxSupport();

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //項目名を隠れた
            this.HideKeyColumns();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
            //管理番号（WAN）
            this.ORIGINAL_CONTROL_NUMBER.Focus();
        }

        /// <summary>
        /// ProcessCmdKey
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if ((Keys)keyData == Keys.Space)
            {
                if (this.logic.currentPatternDto != null &&
                    this.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 1 &&
                    this.ActiveControl != null &&
                    this.customDataGridView1.CurrentCell != null &&
                    (this.ActiveControl.Name == this.customDataGridView1.Name ||
                    (this.ActiveControl.Parent != null &&
                    this.ActiveControl.Parent.Parent != null &&
                    this.ActiveControl.Parent.Parent.Name == this.customDataGridView1.Name)))
                {
                    string columnName = this.customDataGridView1.CurrentCell.OwningColumn.Name;
                    if (columnName == ConstCls.CELL_SYSTEM_ID)
                    {
                        var index = this.customDataGridView1.CurrentCell.RowIndex;

                        this.shoukaiLogic.SetItakuKeiyakuKensaku(index);
                        this.customDataGridView1.Focus();
                        this.customDataGridView1.CurrentCell = this.customDataGridView1[ConstCls.CELL_SYSTEM_ID, index];

                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// AdjustColumnSizeComplete
        /// </summary>
        public override void AdjustColumnSizeComplete()
        {
            base.AdjustColumnSizeComplete();

            if (this.customDataGridView1.Columns.Contains(ConstCls.CELL_HIMODZUKE_FUKA_RIYUU))
            {
                this.customDataGridView1.Columns[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].Width = 180;

            }
        }
        #endregion

        #region F1 電子契約照会（WAN-Sign）

        /// <summary>
        /// 電子契約照会(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //電子契約照会
                this.shoukaiLogic.DenshiKeiyakuShoukaiWanSign();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region F3 修正
        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //電子詳細情報入力画面を表示
                this.shoukaiLogic.OpenDenshiKeiyaku();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region F5 契約参照

        /// <summary>
        /// 契約参照(F5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //委託契約情報検索を表示
                this.shoukaiLogic.OpenItakuKeiyakuKensaku();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region F7 条件クリア

        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //条件クリア
                this.shoukaiLogic.Clear();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region F8 検索
        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //紐付の状態
                if (string.IsNullOrEmpty(this.HIMODZUKE_JOUTAI.Text))
                {
                    this.HIMODZUKE_JOUTAI.IsInputErrorOccured = true;
                    this.shoukaiLogic.msgLogic.MessageBoxShow("E001", "紐付の状態");
                    this.HIMODZUKE_JOUTAI.Focus();
                    return;
                }

                // 契約日（WAN）のチェック
                if (!this.shoukaiLogic.CheckDate())
                {
                    return;
                }

                //データを検索する
                if (this.shoukaiLogic.Search() == 0)
                {
                    //メッセージRを表示
                    this.shoukaiLogic.msgLogic.MessageBoxShow("C001");
                }
                else
                {
                    if (this.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                    {
                        DataGridViewCheckBoxHeaderCell header = this.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].HeaderCell as DataGridViewCheckBoxHeaderCell;
                        if (header != null)
                        {
                            header._checked = false;
                        }

                        foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                        {
                            row.Cells[ConstCls.CELL_CHECKBOX].Value = false;
                        }
                    }
                    if (this.customDataGridView1.RowCount > 0)
                    {
                        foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                        {
                            this.customDataGridView1[col.Name, 0].Style.BackColor = Constans.SELECT_COLOR;
                            this.customDataGridView1[col.Name, 0].Style.SelectionBackColor = Constans.SELECT_COLOR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region F9 登録

        /// <summary>
        /// 登録(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //チェックオンのチェックボックス＝0件（チェンクオンのチェックボックスが無い）
                bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox(9);

                //メッセージGを表示（処理中止）
                if (!checkBoxFlg)
                {
                    return;
                }

                // 登録処理
                if (this.shoukaiLogic.RegistData())
                {
                    this.bt_func8_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func9_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region F10 並び替え

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.Columns[ConstCls.CELL_SYSTEM_ID].Visible = false;
            this.customSortHeader1.ShowCustomSortSettingDialog();
            this.customDataGridView1.Columns[ConstCls.CELL_SYSTEM_ID].Visible = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F11 フィルタ

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.Columns[ConstCls.CELL_SYSTEM_ID].Visible = false;
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
            this.customDataGridView1.Columns[ConstCls.CELL_SYSTEM_ID].Visible = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F12 閉じる

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.shoukaiLogic.parentForm != null)
            {
                this.shoukaiLogic.parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region SubF1 パターン一覧

        /// <summary>
        /// パターン一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //パターン一覧
                this.shoukaiLogic.OpenPatternIchiran();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region SubF2 契約書ダウンロード

        /// <summary>
        /// 契約書ダウンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //明細行＝0件（電子契約データが明細に表示なし（未検索））
                bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox(14);

                //メッセージKを表示（処理中止）
                if (!checkBoxFlg)
                {
                    return;
                }

                var folderName = this.shoukaiLogic.SetOutputFolder();

                //出力先フォルダ＝入力無
                if (string.IsNullOrEmpty(folderName))
                {
                    return;
                }

                //出力先フォルダ＝入力有
                var res = this.shoukaiLogic.KeiyakuDownLoad(folderName);
                if (res)
                {
                    this.shoukaiLogic.msgLogic.MessageBoxShow("I001", "ダウンロード");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region SubF3 紐付補助
        /// <summary>
        /// 紐付補助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process3_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //チェックオンのチェックボックス＝0件（チェンクオンのチェックボックスが無い）
                bool checkBoxFlg = this.shoukaiLogic.CheckForCheckBox(15);

                //メッセージGを表示（処理中止）
                if (!checkBoxFlg)
                {
                    return;
                }

                //電子契約紐付補助画面
                this.shoukaiLogic.OpenHimodzukeHojo();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process4_Click", ex);
                this.shoukaiLogic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region その他処理

        /// <summary>
        /// 指定した明細列を非表示に
        /// </summary>
        private void HideKeyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {
                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    if (col.Name == ConstCls.HIDDEN_WANSIGN_SYSTEM_ID ||
                        col.Name == ConstCls.HIDDEN_SYSTEM_ID ||
                        col.Name == ConstCls.HIDDEN_DOCUMENT_ID ||
                        col.Name == ConstCls.HIDDEN_CONTROL_NUMBER ||
                        col.Name == ConstCls.HIDDEN_SIGNING_DATETIME ||
                        col.Name == ConstCls.HIDDEN_ORIGINAL_CONTROL_NUMBER)
                    {
                        col.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        internal void ShowData()
        {
            if (!this.DesignMode)
            {
                //アラート件数を設定する（カンマを除く）
                this.logic.AlertCount = 0;

                if (!string.IsNullOrEmpty(this.shoukaiLogic.headerForm.AlertNumber.Text))
                {
                    this.logic.AlertCount = int.Parse(this.shoukaiLogic.headerForm.AlertNumber.Text.Replace(",", ""));
                }

                if (this.Table != null && this.PatternNo != 0)
                {

                    if (this.Table.Columns.Contains(ConstCls.CELL_HIMODZUKE_FUKA_RIYUU))
                    {
                        this.Table.Columns[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].ReadOnly = false;
                        this.Table.Columns[ConstCls.CELL_HIMODZUKE_FUKA_RIYUU].MaxLength = 40;
                    }

                    this.Table.Columns[ConstCls.CELL_SYSTEM_ID].ReadOnly = false;
                    this.Table.Columns[ConstCls.CELL_SYSTEM_ID].MaxLength = 9;

                    // 明細に表示
                    this.logic.CreateDataGridView(this.Table);

                    // 指定した明細行を非表示にする。
                    this.HideKeyColumns();

                    // 明細のチェックボックスを編集可能にする。
                    this.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].ReadOnly = false;
                }
            }
        }

        #endregion

        #region Event Form

        /// <summary>
        ///  排出事業者 GYOUSHA_CD_Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.oldGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者 GYOUSHA_CD_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            // 番号が変更されていない場合、処理しない
            if (this.oldGyoushaCd == this.GYOUSHA_CD.Text)
            {
                return;
            }

            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;

            this.shoukaiLogic.CheckGyousha();
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.oldGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.oldGyoushaCd != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 排出事業場 GENBA_CD_Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.oldGenbaCd = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 排出事業場 GENBA_CD_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            // 番号が変更されていない場合、処理しない
            if (this.oldGenbaCd == this.GENBA_CD.Text)
            {
                return;
            }

            this.shoukaiLogic.CheckGenba();
        }

        /// <summary>
        /// 検索条件の契約日（WAN）範囲(To)のダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.CONTRACT_DATE_TO.Text = this.CONTRACT_DATE_FROM.Text;
        }

        /// <summary>
        /// customDataGridView1_CellValidating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var cellName = this.customDataGridView1.Columns[e.ColumnIndex].Name;
            this.rowIndex = e.RowIndex;

            if (cellName == ConstCls.CELL_SYSTEM_ID)
            {
                var sysId = Convert.ToString(this.customDataGridView1[ConstCls.CELL_SYSTEM_ID, e.RowIndex].EditedFormattedValue);
                var documentId = Convert.ToString(this.customDataGridView1[ConstCls.HIDDEN_DOCUMENT_ID, e.RowIndex].Value);
                var controlNumber = Convert.ToString(this.customDataGridView1[ConstCls.HIDDEN_CONTROL_NUMBER, e.RowIndex].Value);

                if (!string.IsNullOrEmpty(sysId))
                {
                    sysId = sysId.PadLeft(9, '0');
                }

                //SystemIDを手入力した場合、入力チェックを実施（カーソルアウト時）
                var res = this.shoukaiLogic.CheckExistsItakuKeiyaku(sysId, documentId, controlNumber);
                if (!res)
                {
                    e.Cancel = true;
                }
                else if (this.shoukaiLogic.isErr)
                {
                    this.customDataGridView1[ConstCls.CELL_SYSTEM_ID, e.RowIndex].Value = this.shoukaiLogic.oldSystemId;
                }
            }
        }

        /// <summary>
        /// customDataGridView1_CellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.rowIndex = e.RowIndex;

            var cellName = this.customDataGridView1.Columns[e.ColumnIndex].Name;

            if (cellName == ConstCls.CELL_SYSTEM_ID &&
                !this.shoukaiLogic.isErr)
            {
                this.shoukaiLogic.oldSystemId = Convert.ToString(this.customDataGridView1[ConstCls.CELL_SYSTEM_ID, e.RowIndex].Value);
            }
        }

        /// <summary>
        /// customDataGridView1_CellClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.rowIndex = e.RowIndex;
        }

        /// <summary>
        /// customDataGridView1_CellFormatting
        /// 明細行の背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
            {
                foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                {
                    if (this.rowIndex != row.Index)
                    {
                        var sysId = Convert.ToString(row.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value);

                        //明細行の背景色＝薄ピンク
                        if (row.Cells[col.Name].ReadOnly)
                        {
                            if (string.IsNullOrEmpty(sysId))
                            {
                                row.Cells[col.Name].Style.BackColor = ConstCls.USUPINKU;
                                row.Cells[col.Name].Style.SelectionBackColor = ConstCls.USUPINKU;
                            }
                            else
                            {
                                row.Cells[col.Name].Style.BackColor = Constans.READONLY_COLOR;
                                row.Cells[col.Name].Style.SelectionBackColor = Constans.READONLY_COLOR;
                            }
                        }
                        else
                        {
                            row.Cells[col.Name].Style.BackColor = Constans.NOMAL_COLOR;
                            row.Cells[col.Name].Style.SelectionBackColor = Constans.NOMAL_COLOR;
                        }
                    }
                    else
                    {
                        row.Cells[col.Name].Style.BackColor = Constans.SELECT_COLOR;
                        row.Cells[col.Name].Style.SelectionBackColor = Constans.SELECT_COLOR;
                    }
                }
            }
        }

        /// <summary>
        /// customDataGridView1_CellDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.bt_func3_Click(null, null);
        }
        #endregion
    }
}
