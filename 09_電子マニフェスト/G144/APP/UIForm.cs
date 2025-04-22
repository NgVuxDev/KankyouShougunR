using System;
using System.Collections;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.Message;
using System.Linq;
using System.Data;
using r_framework.CustomControl;
using System.Drawing;
using System.Data.SqlTypes;
using r_framework.Authority;
using System.Collections.Generic;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ElectronicManifest.UnpanShuryouHoukoku
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private ElectronicManifest.UnpanShuryouHoukoku.LogicClass MILogic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string befHstGyousha = string.Empty;
        private string befUpnGyousha = string.Empty;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        /// <summary>
        /// タイトルリスト
        /// </summary>
        public ArrayList TitleList { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.UNPAN_SYUURYOU_HOUKOKU, false)
        {
            this.InitializeComponent();

            // 社員CD
            this.ShainCd = SystemProperty.Shain.CD;
            // 全ユーザー固定の場合、コメントアウトを解除する
            //this.ShainCd = "000001";

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            if (isLoaded == false)
            {
                this.customDataGridView1.TabStop = false;
                this.customDataGridView1.Visible = false;
                IchiranDgv1.TabIndex = 100; //TODO:コントロール追加時には要注意
                IchiranDgv1.TabStop = true;
                IchiranDgv1.Parent = this;
                this.Controls.Add(IchiranDgv1);
                this.IchiranDgv1.LinkedDataPanelName = "customSortHeader1";
                this.customSortHeader1.LinkedDataGridViewName = "IchiranDgv1";

                //初期化、初期表示
                if (!this.MILogic.WindowInit())
                {
                    return;
                }

                this.MILogic.First_Flg = true;

                //キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;
            }

            // 権限チェック
            if (Manager.CheckAuthority("G144", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                this.ParentBaseForm.bt_func1.Enabled = true;    // 一括入力
                this.ParentBaseForm.bt_func9.Enabled = true;    // JWNET送信
                this.ParentBaseForm.bt_func10.Enabled = true;   // 保留保存
            }
            else if (Manager.CheckAuthority("G144", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                this.ParentBaseForm.bt_func1.Enabled = false;    // 一括入力
                this.ParentBaseForm.bt_func9.Enabled = false;    // JWNET送信
                this.ParentBaseForm.bt_func10.Enabled = false;   // 保留保存
            }

            if (!this.InitializeDataGridView())
            {
                return;
            }

            if (this.MILogic.First_Flg == true)
            {
                this.MILogic.First_Flg = false;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.IchiranDgv1 != null)
            {
                this.IchiranDgv1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            }

            this.isLoaded = true;
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
        /// データグリッドを初期化します
        /// </summary>
        internal bool InitializeDataGridView()
        {
            try
            {
                this.PatternReload(!this.isLoaded);

                //並び順ソートヘッダー
                this.customSortHeader1.ClearCustomSortSetting();

                //バターン一覧の設定を取得
                this.MILogic.selectQuery = this.SelectQuery;
                this.MILogic.orderByQuery = this.OrderByQuery;
                this.MILogic.joinQuery = this.JoinQuery;

                //初期表示の場合、headerを表示するため
                if (!this.DesignMode)
                {
                    this.customDataGridView1.DataSource = null;
                    if (this.Table != null)
                    {
                        this.logic.CreateDataGridView(this.Table);
                        this.MILogic.CreateFixedColumn();
                        this.MILogic.CreateVariableColumn(this.Table);
                        this.IchiranDgv1.DataSource = this.Table;
                    }
                }

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("InitializeDataGridView", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitializeDataGridView", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyoriKubun_CD_TextChanged(object sender, EventArgs e)
        {
            //footerのコントロール
            switch (this.SyoriKubun_CD.Text)
            {
                case "1":
                    this.SyoriKubun_Radio1.Checked = true;
                    this.SyoriKubun_Radio2.Checked = false;
                    this.SyoriKubun_Radio3.Checked = false;
                    break;
                case "2":
                    this.SyoriKubun_Radio1.Checked = false;
                    this.SyoriKubun_Radio2.Checked = true;
                    this.SyoriKubun_Radio3.Checked = false;
                    break;
                case "3":
                    this.SyoriKubun_Radio1.Checked = false;
                    this.SyoriKubun_Radio2.Checked = false;
                    this.SyoriKubun_Radio3.Checked = true;
                    break;
                default:
                    this.SyoriKubun_Radio1.Checked = false;
                    this.SyoriKubun_Radio2.Checked = false;
                    this.SyoriKubun_Radio3.Checked = false;
                    break;
            }
            this.MILogic.HaikiKbnCD = this.SyoriKubun_CD.Text;
            this.SyoriKubun_CD.SelectAll();
        }

        private void HAIKI_KBN_CD_LostFocus(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.MILogic.Haiki_Kbn_CD_Check(out catchErr);
        }

        /// <summary>
        /// パターン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 一括入力(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            var dtoList = new List<UnpanShuryouHoukokuIkkatuNyuuryoku.InputInfoDTOCls>();
            foreach (DataGridViewRow row in this.IchiranDgv1.Rows)
            {
                if (SqlBoolean.Parse(row.Cells["CHECKBOX"].Value.ToString()).IsTrue && row.Cells["操作CD"].Value != null && row.Cells["操作CD"].Value.ToString() != "3")
                {
                    var input = new UnpanShuryouHoukokuIkkatuNyuuryoku.InputInfoDTOCls()
                    {
                        KANRI_ID = row.Cells["管理番号"].Value.ToString(),
                        SEQ = row.Cells["枝番"].Value.ToString(),
                        UPN_ROUTE_NO = row.Cells["KUKAN"].Value.ToString(),
                        EDI_MEMBER_ID = row.Cells["UPN_SHA_EDI_MEMBER_ID"].Value.ToString(),
                        GYOUSHA_CD = row.Cells["GYOUSHA_CD"].Value.ToString()
                    };
                    dtoList.Add(input);
                }
            }
            if (dtoList.Count == 0)
            {
                //選択無
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E051", "一括入力する対象(※操作が「取消」以外)");
                return;
            }

            var callForm = new Shougun.Core.ElectronicManifest.UnpanShuryouHoukokuIkkatuNyuuryoku.UIForm();
            // 20151110 katen #12048 「システム日付」の基準作成、適用 start
            (callForm as SuperForm).sysDate = (this.Parent as BusinessBaseForm).sysDate;
            // 20151110 katen #12048 「システム日付」の基準作成、適用 end

            // 検索条件
            callForm.inputInfo = dtoList.ToArray();

            callForm.ShowDialog(); //表示

            this.MILogic.SetIchiranData(callForm.outputInfo); //反映（いいえで閉じたかどうかは 引数がnullかどうかで内部でチェック）
        }

        /// <summary>
        /// 保留削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            MILogic.HoryuDelete();
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            ////仕様不明なため、未実装。確認用
            CSVExport CSVExp = new CSVExport();
            //CSVExp.ConvertCustomDataGridViewToCsv(this.IchiranDgv1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_UNPAN_SHURYO));
            CSVExp.ConvertCustomDataGridViewToCsv(this.IchiranDgv1, true, true, "運搬終了報告", this);
        }

        /// <summary>
        /// [F7]並び替えをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            // チェックボックス、詳細ボタン、操作CD、操作名はソート条件に含めたくないので、一旦非表示にする
            this.IchiranDgv1.Columns["CHECKBOX"].Visible = false;
            this.IchiranDgv1.Columns["詳細ボタン"].Visible = false;
            this.IchiranDgv1.Columns["操作CD"].Visible = false;
            this.IchiranDgv1.Columns["操作"].Visible = false;

            this.customSortHeader1.ShowCustomSortSettingDialog();

            this.IchiranDgv1.Columns["CHECKBOX"].Visible = true;
            this.IchiranDgv1.Columns["詳細ボタン"].Visible = true;
            this.IchiranDgv1.Columns["操作CD"].Visible = true;
            this.IchiranDgv1.Columns["操作"].Visible = true;
        }

        /// <summary>
        /// 検索(F8)ボタンを押したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // パターンが登録されていない場合は検索しない
            if (this.PatternNo == 0)
            {
                new MessageBoxShowLogic().MessageBoxShow("E057", "パターンが登録", "検索");
            }
            else
            {
                bool catchErr = false;
                bool retCheck = this.MILogic.DateCheck(out catchErr);
                if (catchErr)
                {
                    return;
                }

                // 日付チェック
                if (!retCheck)
                {
                    this.MILogic.Search();
                }
            }
        }

        /// <summary>
        /// JWNET送信(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            this.MILogic.JWEInsert();
        }

        /// <summary>
        /// 保留保存(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            /// 20141022 Houkakou 「運搬終了報告」の日付チェックを追加する　start
            bool catchErr = false;
            bool retCheck = this.MILogic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retCheck)
            {
                return;
            }
            /// 20141022 Houkakou 「運搬終了報告」の日付チェックを追加する　end
            this.MILogic.HoryuInsert();

        }

        /// <summary>
        /// 検索条件クリア(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.MILogic.Clear();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            if (parentForm != null)
            {
                this.customDataGridView1.DataSource = "";

                this.Close();
                parentForm.Close();
            }
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {

            var sysID = this.OpenPatternIchiran();

            this.MILogic.selectQuery = this.logic.SelectQeury;
            this.MILogic.orderByQuery = this.logic.OrderByQuery;
            this.MILogic.joinQuery = this.logic.JoinQuery;

            this.InitializeDataGridView();
        }

        /// <summary>
        /// 検索条件設定(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            //仕様不明なため、未実装。確認用
            MessageBox.Show("検索条件設定画面", "画面遷移");
        }

        #endregion

        private void SyoriKubun_Radio1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SyoriKubun_Radio1.Checked)
            {
                this.SyoriKubun_CD.Text = "1";
            }
        }

        private void SyoriKubun_Radio2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SyoriKubun_Radio2.Checked)
            {
                this.SyoriKubun_CD.Text = "2";
            }
        }

        private void SyoriKubun_Radio3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SyoriKubun_Radio3.Checked)
            {
                this.SyoriKubun_CD.Text = "3";
            }
        }


        /// <summary>
        /// CellBeginEdit
        /// </summary>
        public void IchiranDgv1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string HeaderText =
                this.IchiranDgv1.Columns[this.IchiranDgv1.CurrentCell.ColumnIndex].HeaderText.ToString();
            if ("操作CD".Equals(HeaderText)
                || "運搬担当者CD".Equals(HeaderText)
                || "報告担当者CD".Equals(HeaderText)
                || "運搬量単位CD".Equals(HeaderText)
                || "有価物拾集量単位CD".Equals(HeaderText)
                || "運搬終了日".Equals(HeaderText)
                || "運搬量".Equals(HeaderText)
                || "車輌番号CD".Equals(HeaderText))
            {
                this.IchiranDgv1.ImeMode = ImeMode.Alpha;
                this.IchiranDgv1.ImeMode = ImeMode.Disable;
            }
            else if ("備考".Equals(HeaderText))
            {
                if (this.IchiranDgv1.ImeMode != ImeMode.Hiragana || this.IchiranDgv1.ImeMode != ImeMode.Katakana)
                {
                    this.IchiranDgv1.ImeMode = ImeMode.Hiragana;
                }
            }
            else if ("運搬担当者".Equals(HeaderText)
                || "報告担当者".Equals(HeaderText)
                || "車輌名".Equals(HeaderText))
            {
                this.IchiranDgv1.ImeMode = ImeMode.Hiragana;
                this.IchiranDgv1.ImeMode = ImeMode.Off;
            }
        }

        /// <summary>
        /// セルをダブルクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void IchiranDgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }

        /// 20141022 Houkakou 「運搬終了報告」の日付チェックを追加する　start
        private void DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_TO.Text))
            {
                this.DATE_TO.IsInputErrorOccured = false;
                this.DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_FROM.Text))
            {
                this.DATE_FROM.IsInputErrorOccured = false;
                this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141022 Houkakou 「運搬終了報告」の日付チェックを追加する　end

        /// <summary>
        /// データグリッドのセルのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void IchiranDgv1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var columnName = this.IchiranDgv1.Columns[e.ColumnIndex].Name;
                var cellValue = this.IchiranDgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (columnName == "操作CD")
                {
                    if (cellValue == null || String.IsNullOrEmpty(cellValue.ToString()))
                    {
                        this.IchiranDgv1.Rows[e.RowIndex].Cells["操作"].Value = String.Empty;
                    }
                    else if (cellValue.ToString() == "1")
                    {
                        this.IchiranDgv1.Rows[e.RowIndex].Cells["操作"].Value = "報告";
                    }
                    else if (cellValue.ToString() == "2")
                    {
                        this.IchiranDgv1.Rows[e.RowIndex].Cells["操作"].Value = "修正";
                    }
                    else if (cellValue.ToString() == "3")
                    {
                        this.IchiranDgv1.Rows[e.RowIndex].Cells["操作"].Value = "取消";
                    }

                    if (!this.SetRowReadOnly(e.RowIndex))
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 操作CDが3の行を読み取り専用にします
        /// </summary>
        internal void SetRowReadOnly()
        {
            foreach (DataGridViewRow row in this.IchiranDgv1.Rows)
            {
                if (!this.SetRowReadOnly(row.Index))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 操作CDが3の行を読み取り専用にします
        /// </summary>
        /// <param name="rowIndex">対象の行index</param>
        internal bool SetRowReadOnly(int rowIndex)
        {
            try
            {
                // ReadOnlyプロパティの変更時にCellValidatedイベントが発生するので一旦イベントを削除
                this.IchiranDgv1.CellValidated -= new DataGridViewCellEventHandler(this.IchiranDgv1_CellValidated);

                var row = this.IchiranDgv1.Rows[rowIndex];
                if (row.Cells["操作CD"].Value.ToString() == "3")
                {
                    row.ReadOnly = true;
                }
                else
                {
                    row.ReadOnly = false;
                }
                row.Cells["CHECKBOX"].ReadOnly = false;
                // 処理区分が3の場合は操作CDは変更可能
                if (this.SyoriKubun_Radio3.Checked)
                {
                    row.Cells["操作CD"].ReadOnly = false;
                }

                foreach (DataGridViewCell cell in row.Cells.OfType<ICustomAutoChangeBackColor>())
                {
                    CustomControlExtLogic.UpdateBackColor((ICustomAutoChangeBackColor)cell);
                }

                this.IchiranDgv1.CellValidated += new DataGridViewCellEventHandler(this.IchiranDgv1_CellValidated);

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetRowReadOnly", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRowReadOnly", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 全選択チェックボックス
        /// </summary>
        private CheckBox allSelectCheckBox = new CheckBox();

        /// <summary>
        /// データグリッドのセルが描画されるときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void IchiranDgv1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.Transparent);
                    }

                    allSelectCheckBox.Width = 13;
                    allSelectCheckBox.Height = 13;

                    // 描画領域の中央に配置
                    Point pt1 = new Point((bmp.Width - allSelectCheckBox.Width) / 2, (bmp.Height - allSelectCheckBox.Height) / 2);
                    if (pt1.X < 0) pt1.X = 0;
                    if (pt1.Y < 0) pt1.Y = 0;

                    // Bitmapに描画
                    allSelectCheckBox.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, pt2);
                    e.Handled = true;

                    allSelectCheckBox.CheckedChanged -= new EventHandler(this.allSelectCheckBox_CheckedChanged);
                    allSelectCheckBox.CheckedChanged += new EventHandler(this.allSelectCheckBox_CheckedChanged);
                }
            }
        }

        /// <summary>
        /// 全選択チェックボックスのチェック状態が変更されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void allSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var currentCell = this.IchiranDgv1.CurrentCell;
            if (currentCell != null)
            {
                this.IchiranDgv1.CurrentCell = this.IchiranDgv1.Rows[0].Cells["操作CD"];
            }
            foreach (DataGridViewRow row in this.IchiranDgv1.Rows)
            {
                if (row.Index != -1)
                {
                    row.Cells["CHECKBOX"].Value = allSelectCheckBox.Checked;
                }
            }
            this.IchiranDgv1.CurrentCell = currentCell;
        }

        /// <summary>
        /// 一覧のセルがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void IchiranDgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == 0)
                {
                    allSelectCheckBox.Checked = !allSelectCheckBox.Checked;
                    this.IchiranDgv1.Refresh();
                }
            }
            else
            {
                if (this.IchiranDgv1.CurrentCell is System.Windows.Forms.DataGridViewButtonCell)
                {
                    FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG,
                                                 this.IchiranDgv1.Rows[e.RowIndex].Cells["管理番号"].Value.ToString(),
                                                 this.IchiranDgv1.Rows[e.RowIndex].Cells["枝番"].Value.ToString());
                }
            }
        }

        /// <summary>
        /// 全選択チェックボックスのチェック状態を設定します
        /// </summary>
        /// <param name="check">チェック状態</param>
        internal void SetAllSelectChecked(bool check)
        {
            this.allSelectCheckBox.Checked = check;
        }

        /// <summary>
        /// 選択されている行があるかを表すフラグ
        /// </summary>
        /// <returns>True:選択されている行がある False:選択されている行がない</returns>
        internal bool IsRowSelected()
        {
            var ret = false;
            var selectedRowCount = this.IchiranDgv1.Rows.Cast<DataGridViewRow>().Where(r => SqlBoolean.Parse(r.Cells["CHECKBOX"].Value.ToString()).IsTrue).Count();
            if (selectedRowCount > 0)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void Jigyousya_PopupBeforeExecuteMethod()
        {
            this.befHstGyousha = this.Jigyousya_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void Jigyousya_PopupAfterExecuteMethod()
        {
            if (this.befHstGyousha != this.Jigyousya_CD.Text)
            {
                this.Jigyoujou_CD.Text = string.Empty;
                this.JIGYOUBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 運搬業者 PopupBeforeExecuteMethod
        /// </summary>
        public void Unpansha_PopupBeforeExecuteMethod()
        {
            this.befUpnGyousha = this.Unpansha_CD.Text;
        }

        /// <summary>
        /// 運搬業者 PopupAfterExecuteMethod
        /// </summary>
        public void Unpansha_PopupAfterExecuteMethod()
        {
            if (this.befUpnGyousha != this.Unpansha_CD.Text)
            {
                this.Unpanba_CD.Text = string.Empty;
                this.Unpanba_Name.Text = string.Empty;
            }
        }
    }
}
