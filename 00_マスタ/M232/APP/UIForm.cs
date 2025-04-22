// $Id: UIForm.cs 56609 2015-07-24 01:54:30Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.KaisyuuHinmeShousai;
using Shougun.Core.Master.CourseNyuryoku.Logic;
using Seasar.Framework.Exceptions;
using r_framework.FormManager;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using r_framework.Configuration;

namespace Shougun.Core.Master.CourseNyuryoku.APP
{
    /// <summary>
    /// コンテナ状況画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// コンテナ状況画面ロジック
        /// </summary>
        private LogicCls logic;

        bool IsCdFlg = false;

        // public HeaderForm headerForm;

        int IchiranHeight;
        int customPanel3Height;
        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        // 前荷降業者
        string ZenNiorosiGyosyaCD = string.Empty;
        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        internal string dayCd;
        internal string courseNameCd;
        internal string courseName;
        internal string kyotenCd;
        internal string kyotenName;
        internal string dayCdF;
        internal string courseNameCdF;
        internal string courseNameF;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public UIForm(/*HeaderForm hf*/)
            : base(WINDOW_ID.M_COURSE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            // 時間コンボボックスのItemsをセット
            this.SAGYOU_BEGIN_HOUR.SetItems();
            this.SAGYOU_BEGIN_MINUTE.SetItems(5);
            this.SAGYOU_END_HOUR.SetItems();
            this.SAGYOU_END_MINUTE.SetItems(5);

            this.customDataGridView_M_GENBA_TEIKI_HINMEI.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //headerForm = hf;
            // headerForm = ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm);

            //IchiranHeight = Ichiran.Height;
            //customPanel3Height = customPanel3.Height;

            LogUtility.DebugMethodEnd();
        }

        public UIForm(string dayCd, string courseNameCd, string courseName, string kyotenCd, string kyotenName, string dayCdF, string courseNameCdF, string courseNameF)
            : base(WINDOW_ID.M_COURSE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart(dayCd, courseNameCd, courseName, kyotenCd, kyotenName, dayCdF, courseNameCdF, courseNameF);

            this.InitializeComponent();

            this.dayCd = dayCd;
            this.courseNameCd = courseNameCd;
            this.courseName = courseName;
            this.kyotenCd = kyotenCd;
            this.kyotenName = kyotenName;

            // 時間コンボボックスのItemsをセット
            this.SAGYOU_BEGIN_HOUR.SetItems();
            this.SAGYOU_BEGIN_MINUTE.SetItems(5);
            this.SAGYOU_END_HOUR.SetItems();
            this.SAGYOU_END_MINUTE.SetItems(5);

            this.customDataGridView_M_GENBA_TEIKI_HINMEI.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
            if (!string.IsNullOrEmpty(dayCdF)
                && !string.IsNullOrEmpty(courseNameCdF)
                && !string.IsNullOrEmpty(courseNameF))
            {
                this.dayCdF = dayCdF;
                this.courseNameCdF = courseNameCdF;
                this.courseNameF = courseNameF;
                this.logic.FromCourseIchiran = true;
            }
            else
            {
                this.logic.FromCourseIchiran = false;
            }

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
                base.OnLoad(e);

                //VAN 20210921 #154848 S
                //列を自動作成が要らないため、falseにする
                this.Ichiran.AutoGenerateColumns = false;
                this.customDataGridView_M_COURSE_NIOROSHI.AutoGenerateColumns = false;
                this.customDataGridView_M_GENBA_TEIKI_HINMEI.AutoGenerateColumns = false;
                //VAN 20210921 #154849 E

                // headerForm = ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm);

                // ---20140123 oonaka add ヘッダコントロールを必須チェック対象化 start ---
                Control[] formControls = controlUtil.GetAllControls(this);
                Control[] headerControls = controlUtil.GetAllControls(((BusinessBaseForm)this.Parent).headerForm);
                List<Control> allCon = new List<Control>();
                allCon.AddRange(formControls);
                allCon.AddRange(headerControls);
                this.allControl = allCon.ToArray();
                // ---20140123 oonaka add ヘッダコントロールを必須チェック対象化 end ---

                this.logic.ReleaseReferenceMode();
                if (!this.logic.WindowInit())
                {
                    return;
                }
                // this.Search(null, e);
                // changeGenbaTeikiHinmei();
                gridUiLock(true);
                //((BusinessBaseForm)this.Parent).bt_func8.Enabled = true;
                //((BusinessBaseForm)this.Parent).bt_func12.Enabled = true;

                this.Ichiran.AllowUserToAddRows = false;

                if (this.logic.FromCourseIchiran)
                {
                    if (!string.IsNullOrEmpty(this.dayCdF) && !string.IsNullOrEmpty(this.courseNameCdF))
                    {
                        this.customTextBoxDayCd.Text = this.dayCdF;
                        this.customTextBoxCoureseNameCd.Text = this.courseNameCdF;
                        this.customTextBoxCoureseName.Text = this.courseNameF;
                        var headerForm = ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm);
                        headerForm.KYOTEN_CD.Text = this.kyotenCd;
                        headerForm.KYOTEN_NAME_RYAKU.Text = this.kyotenName;
                        this.customTextBoxCoureseNameCd_Validated(this.customTextBoxCoureseNameCd, new EventArgs());
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.dayCd) && !string.IsNullOrEmpty(this.courseNameCd))
                    {
                        this.customTextBoxDayCd.Text = this.dayCd;
                        this.customTextBoxCoureseNameCd.Text = this.courseNameCd;
                        this.customTextBoxCoureseName.Text = this.courseName;
                        var headerForm = ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm);
                        headerForm.KYOTEN_CD.Text = this.kyotenCd;
                        headerForm.KYOTEN_NAME_RYAKU.Text = this.kyotenName;
                        this.customTextBoxCoureseNameCd_Validated(this.customTextBoxCoureseNameCd, new EventArgs());
                    }
                }
                customTextBoxDayCd.Focus();


                this.customTextBoxCoureseNameCd.Enter += new EventHandler(customTextBoxCoureseNameCd_Enter);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
        /// Anchorが上手く効かないので親フォームのサイズ変更に合わせて手動で変更
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.tableLayoutPanel1 != null)
            {
                this.tableLayoutPanel1.SuspendLayout();
                this.tableLayoutPanel1.Size = this.Size;
                this.tableLayoutPanel1.Height -= 10;
                this.tableLayoutPanel1.ResumeLayout();
            }
            base.OnSizeChanged(e);
        }

        internal bool resetKaisyuuhinmei()
        {
            try
            {
                // コース_明細
                var table = this.logic.courseDetailSearchResult;
                if (table != null && table.Rows.Count > 0)
                {
                    table.BeginLoadData();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow dr = this.logic.courseDetailSearchResult.Rows[i];

                        DataRow[] drs = this.logic.courseDetailItemsSearchResult.Select("DAY_CD = " + dr["DAY_CD"] + " AND COURSE_NAME_CD = '" + dr["COURSE_NAME_CD"] + "' AND REC_ID = " + dr["REC_ID"] + " AND DELETE_FLG = 0");
                        drs = drs.OrderBy(d => d["HINMEI_CD"]).ToArray();

                        string s = "";
                        foreach (DataRow c in drs)
                        {
                            if (0 == (int)c["DELETE_FLG"])
                            {
                                if (s.Length > 0)
                                {
                                    s += "/ ";
                                }
                                s += c["HINMEI_NAME_RYAKU"];
                            }
                        }
                        int len = 200 < s.Length ? 200 : s.Length;
                        table.Columns["KAISYUUHIN_NAME"].ReadOnly = false;
                        dr["KAISYUUHIN_NAME"] = s.Substring(0, len);
                        if (this.Ichiran.Rows.Count > i && !this.Ichiran.Rows[i].IsNewRow)
                        {
                            DataGridViewRow row = this.Ichiran.Rows[i];
                            ((DataTable)this.Ichiran.DataSource).Columns["KAISYUUHIN_NAME"].ReadOnly = false;
                            this.Ichiran.Columns["KAISYUUHIN_NAME"].ReadOnly = false;
                            row.Cells["KAISYUUHIN_NAME"].Value = s.Substring(0, len);
                            this.Ichiran.Columns["KAISYUUHIN_NAME"].ReadOnly = true;
                            ((DataTable)this.Ichiran.DataSource).Columns["KAISYUUHIN_NAME"].ReadOnly = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("resetKaisyuuhinmei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void DataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if ("".Equals(e.Value)) //空文字を入力された場合
                {
                    e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                    e.ParsingApplied = true; //後続の解析不要
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD入力チェック
        /// </summary>
        private void Ichiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string columName = Ichiran.Columns[e.ColumnIndex].Name;

                switch (columName)
                {
                    case "GENBA_CD":
                        //業者CD入力チェック
                        if (!ChkGyoushaNyuryoku(Ichiran.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value))
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            MessageBox.Show("先に業者CDを入力してください", Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 業者入力チェック
        /// <summary>
        /// 業者入力チェック
        /// </summary>
        /// <param name="gyousha">業者CD</param>
        /// <returns>チェック結果</returns>
        public bool ChkGyoushaNyuryoku(object gyousha)
        {
            LogUtility.DebugMethodStart(gyousha);

            if (!string.IsNullOrEmpty(gyousha.ToString()))
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            else
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.Disposing == false)
            {
                // 編集名のセット
                var row = this.Ichiran.Rows[e.RowIndex];

                // 編集名のセット
                var colName = this.Ichiran.Columns[e.ColumnIndex].Name;

                var cell = this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (colName == "ROW_NO2")
                {
                    // 順番に同じ値は２件まで
                    int sameCount = this.Ichiran.Rows.Cast<DataGridViewRow>()
                        .Where(t => t.Cells[e.ColumnIndex].FormattedValue.Equals(e.FormattedValue)).Count();
                    //var cell = this.Ichiran[e.ColumnIndex, e.RowIndex];

                    if (sameCount > 2 && !string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                    {
                        msgLogic.MessageBoxShow("E031", "順番");
                        e.Cancel = true;

                        // 前回の値に戻す
                        this.Ichiran.CancelEdit();
                        return;
                    }
                }

                // 業者CD(ァベット大文字変化)
                if (colName == "GYOUSHA_CD")
                {
                    if (this.Ichiran.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value == null
                        || string.IsNullOrEmpty(this.Ichiran.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value.ToString()))
                    {
                        return;
                    }

                    this.Ichiran.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value =
                        this.Ichiran.Rows[e.RowIndex].Cells["GYOUSHA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }

                //現場チェック
                if (colName == "GENBA_CD")
                {
                    if (!this.logic.ChkGridGenba(e.RowIndex))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                // 希望時間
                if (colName == "KIBOU_TIME")
                {
                    if (!this.logic.IsTimeChkOK(cell))
                    {
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                    //this.logic.isInputError = false;
                }
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string columName = Ichiran.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "ROW_NO": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "ROW_NO2": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "ROUND_NO": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "GYOUSHA_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "GYOUSHA_NAME_RYAKU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "GENBA_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "GENBA_NAME_RYAKU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "DETAIL_BUTTON": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "KAISYUUHIN_NAME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "BIKOU": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "KIBOU_TIME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "SAGYOU_TIME_MINUTE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    //default: Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                }

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.Ichiran.Columns["KIBOU_TIME"].Index == e.ColumnIndex && !this.Ichiran[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // 入力可能な場合「:」を削除
                    this.Ichiran[e.ColumnIndex, e.RowIndex].Value = Convert.ToString(this.Ichiran[e.ColumnIndex, e.RowIndex].Value).Replace(":", string.Empty);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 行切替処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (this.Disposing == false)
            {
                // 編集名のセット
                var row = this.Ichiran.Rows[e.RowIndex];

                // 回数の重複チェック
                if (true == this.logic.roundNoOverlapCheck(row))
                {
                    // エラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E031", "回数・業者CD・現場CD");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView_M_COURSE_NIOROSHI_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string columName = customDataGridView_M_COURSE_NIOROSHI.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "M_COURSE_NIOROSHI_NIOROSHI_NO": customDataGridView_M_COURSE_NIOROSHI.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "M_COURSE_NIOROSHI_GYOUSHA_CD": customDataGridView_M_COURSE_NIOROSHI.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "M_COURSE_NIOROSHI_GYOUSHA_NAME_RYAKU": customDataGridView_M_COURSE_NIOROSHI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_COURSE_NIOROSHI_GENBA_CD": customDataGridView_M_COURSE_NIOROSHI.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "M_COURSE_NIOROSHI_GENBA_NAME_RYAKU": customDataGridView_M_COURSE_NIOROSHI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    //default: Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView_M_GENBA_TEIKI_HINMEI_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                string columName = customDataGridView_M_GENBA_TEIKI_HINMEI.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "M_GENBA_TEIKI_HINMEI_KUMIKOMI": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "M_GENBA_TEIKI_HINMEI_GYOUSHA_CD": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_GYOUSHA_NAME_RYAKU": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_GENBA_CD": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_GENBA_NAME_RYAKU": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_HINMEI_CD": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_HINMEI_NAME_RYAKU": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_KANSANCHI": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_KANSAN_UNIT_CD": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_MONDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_TUESDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_WEDNESDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_THURSDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_FRIDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_SATURDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "M_GENBA_TEIKI_HINMEI_SUNDAY": customDataGridView_M_GENBA_TEIKI_HINMEI.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;

                    //default: Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// Ichiran 業者CD・現場CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // GYOUSHA_CD
                // GENBA_CD
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["GYOUSHA_CD"].Index ||
                    Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["GENBA_CD"].Index)
                {
                    IsCdFlg = true;
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
                else
                {
                    IsCdFlg = false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// itemID_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (IsCdFlg &&
                    !char.IsControl(e.KeyChar) &&
                    !char.IsDigit(e.KeyChar) &&
                    !char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }



        /// <summary>
        /// customDataGridView_M_COURSE_NIOROSHI 荷降業者CD・荷降現場CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView_M_COURSE_NIOROSHI_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // M_COURSE_NIOROSHI_GYOUSHA_CD
                // M_COURSE_NIOROSHI_NIOROSHI_GENBA_CD
                if (customDataGridView_M_COURSE_NIOROSHI.CurrentCell.ColumnIndex == customDataGridView_M_COURSE_NIOROSHI.Columns["M_COURSE_NIOROSHI_GYOUSHA_CD"].Index ||
                    customDataGridView_M_COURSE_NIOROSHI.CurrentCell.ColumnIndex == customDataGridView_M_COURSE_NIOROSHI.Columns["M_COURSE_NIOROSHI_GENBA_CD"].Index)
                {
                    IsCdFlg = true;
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
                else
                {
                    IsCdFlg = false;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 明細一覧のcellを結合する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView_M_GENBA_TEIKI_HINMEI_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            try
            {
                // ガード句
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    //LogUtility.DebugMethodEnd();
                    return;
                }

                // 月曜から日曜を結合する処理
                var monIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_MONDAY"].Index;
                var tueIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_TUESDAY"].Index;
                var wedIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_WEDNESDAY"].Index;
                var thuIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_THURSDAY"].Index;
                var friIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_FRIDAY"].Index;
                var satIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_SATURDAY"].Index;
                var sunIndex = this.customDataGridView_M_GENBA_TEIKI_HINMEI.Columns["M_GENBA_TEIKI_HINMEI_SUNDAY"].Index;
                if (e.ColumnIndex == monIndex || e.ColumnIndex == sunIndex) //★★★
                {
                    // セルの矩形を取得
                    Rectangle rect = e.CellBounds;

                    DataGridView dgv = (DataGridView)sender;

                    //★★★火曜から日曜の場合
                    if (e.ColumnIndex == monIndex)
                    {
                        // 火曜の幅を取得して、月曜の幅に足す
                        rect.Width = rect.Width + dgv.Columns[tueIndex].Width + dgv.Columns[wedIndex].Width + dgv.Columns[thuIndex].Width + dgv.Columns[friIndex].Width + dgv.Columns[satIndex].Width + dgv.Columns[sunIndex].Width;
                        rect.Y = e.CellBounds.Y + 1;
                    }
                    else if (e.ColumnIndex == sunIndex)
                    {
                        // 月曜の幅を取得して、火曜の幅に足す
                        rect.Width = rect.Width + dgv.Columns[monIndex].Width + dgv.Columns[tueIndex].Width + dgv.Columns[wedIndex].Width + dgv.Columns[thuIndex].Width + dgv.Columns[friIndex].Width + dgv.Columns[satIndex].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        //★★★Leftを月曜に合わせる
                        rect.X = rect.X - dgv.Columns[monIndex].Width - dgv.Columns[tueIndex].Width - dgv.Columns[wedIndex].Width - dgv.Columns[thuIndex].Width - dgv.Columns[friIndex].Width - dgv.Columns[satIndex].Width;

                    }
                    // 背景、枠線、セルの値を描画
                    using (SolidBrush brush = new SolidBrush(this.Ichiran.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);

                        using (Pen pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                    }

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                    "曜日",
                                    e.CellStyle.Font,
                                    rect,
                                    e.CellStyle.ForeColor,
                                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                }

                // 結合セル以外は既定の描画を行う
                if (!(
                    e.ColumnIndex == monIndex ||
                    e.ColumnIndex == tueIndex ||
                    e.ColumnIndex == wedIndex ||
                    e.ColumnIndex == thuIndex ||
                    e.ColumnIndex == friIndex ||
                    e.ColumnIndex == satIndex ||
                    e.ColumnIndex == sunIndex
                    ))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;

            }
            catch
            {
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void customTextBoxHinmeiNameCD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 値に変更があったかを判断します
                if (this.IsChangedValue(sender))
                {
                    if (customTextBoxHinmeiNameCD.Enabled == false)
                    {
                        return;
                    }
                    changeGenbaTeikiHinmei();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        public bool changeGenbaTeikiHinmei()
        {
            // LogUtility.DebugMethodStart();

            try
            {
                this.logic.SearchGenbaTeikiHinmei();

                if (0 < this.logic.genbaTeikiHinmeiSearchResult.Rows.Count)
                {
                    if (this.logic.courseDetailSearchResult != null &&
                        this.logic.courseDetailItemsSearchResult != null)
                    {
                        // #21890 KAISYUUHIN_NAMEのMaxLength以上の文字数が設定された場合に、DefaultView.RowFilterでReadOnlyExceptionが発生するための回避策
                        this.logic.courseDetailSearchResult.Columns["KAISYUUHIN_NAME"].ReadOnly = false;

                        DataView dvCourseDetail = this.logic.courseDetailSearchResult.DefaultView;
                        DataView dvCourseDetailItems = this.logic.courseDetailItemsSearchResult.DefaultView;

                        for (int i = 0; i < this.logic.genbaTeikiHinmeiSearchResult.Rows.Count; i++)
                        {
                            this.logic.genbaTeikiHinmeiSearchResult.Columns["isShow"].ReadOnly = false;

                            DataRow dr = this.logic.genbaTeikiHinmeiSearchResult.Rows[i];
                            dvCourseDetail.RowFilter = "GYOUSHA_CD = '" + dr["GYOUSHA_CD"] + "' AND GENBA_CD = '" + dr["GENBA_CD"] + "'";

                            for (int j = 0; j < dvCourseDetail.Count; j++)
                            {
                                dvCourseDetailItems.RowFilter = "DAY_CD = '" + dvCourseDetail[j]["DAY_CD"] + "' AND COURSE_NAME_CD = '" + dvCourseDetail[j]["COURSE_NAME_CD"] + "' AND REC_ID = '" + dvCourseDetail[j]["REC_ID"] + "'";

                                for (int k = 0; k < dvCourseDetailItems.Count; k++)
                                {
                                    // 換算後単位CD=0のレコードを比較するために、nullと0を同じものとして扱う
                                    var teikiHinmeiKansanUnitCd = 0;
                                    var courseDetailItemsKansanUnitCd = 0;
                                    if (null != dr["KANSAN_UNIT_CD"] && !String.IsNullOrEmpty(dr["KANSAN_UNIT_CD"].ToString()))
                                    {
                                        teikiHinmeiKansanUnitCd = Int16.Parse(dr["KANSAN_UNIT_CD"].ToString());
                                    }
                                    if (null != dvCourseDetailItems[k]["KANSAN_UNIT_CD"] && !String.IsNullOrEmpty(dvCourseDetailItems[k]["KANSAN_UNIT_CD"].ToString()))
                                    {
                                        courseDetailItemsKansanUnitCd = Int16.Parse(dvCourseDetailItems[k]["KANSAN_UNIT_CD"].ToString());
                                    }

                                    if (dr["HINMEI_CD"].Equals(dvCourseDetailItems[k]["HINMEI_CD"])
                                        && dr["UNIT_CD"].Equals(dvCourseDetailItems[k]["UNIT_CD"])
                                        && teikiHinmeiKansanUnitCd == courseDetailItemsKansanUnitCd
                                        && dr["DENPYOU_KBN_CD"].Equals(dvCourseDetailItems[k]["DENPYOU_KBN_CD"]))
                                    {
                                        dr["isShow"] = 1;
                                    }
                                }
                            }
                        }

                        dvCourseDetail.RowFilter = "";
                        dvCourseDetailItems.RowFilter = "";

                        // 処理が完了したので、読取専用に戻す
                        this.logic.courseDetailSearchResult.Columns["KAISYUUHIN_NAME"].ReadOnly = true;
                    }
                }

                this.logic.genbaTeikiHinmeiSearchResult.Columns["KUMIKOMI"].ReadOnly = false;
                DataView dvGenbaTeikiHinmei = this.logic.genbaTeikiHinmeiSearchResult.DefaultView;
                var findStr = this.logic.roundChanged();
                if (false == string.IsNullOrEmpty(findStr))
                {
                    dvGenbaTeikiHinmei.RowFilter = findStr;
                }
                else
                {
                    dvGenbaTeikiHinmei.RowFilter = "isShow = 0";
                }

                // 現場_定期品名
                this.customDataGridView_M_GENBA_TEIKI_HINMEI.DataSource = dvGenbaTeikiHinmei;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("changeGenbaTeikiHinmei", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("changeGenbaTeikiHinmei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                // LogUtility.DebugMethodEnd();
            }
            return true;
        }

        /// <summary>
        /// [F1] 切替処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dispChange(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;

                this.panel2.Visible = !this.panel2.Visible;
                this.customDataGridView_M_GENBA_TEIKI_HINMEI.Visible = this.panel2.Visible;
                var rowSpan = this.panel2.Visible ? 1 : 3;
                this.tableLayoutPanel1.SetRowSpan(this.Ichiran, rowSpan);
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F2] 行挿入ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void RecAdd(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                DataTable datatable = GetDataSource();
                if (null == datatable)
                {
                    return;
                }
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    datatable.Columns[i].ReadOnly = false;
                }

                //********************20131212 by heyong**********修正開始**************************
                int rec_id = 0;
                M_COURSE_DETAIL courseDetailEntity = new M_COURSE_DETAIL();
                courseDetailEntity.DAY_CD = SqlInt16.Parse(customTextBoxDayCd.Text);
                courseDetailEntity.COURSE_NAME_CD = customTextBoxCoureseNameCd.Text;
                DataTable tbMaxNo = this.logic.courseDetailDao.GetMaxIdByCd(courseDetailEntity);

                if (null == tbMaxNo || 0 == tbMaxNo.Rows.Count)
                {
                    rec_id = 1;
                }
                else
                {
                    rec_id = (int)tbMaxNo.Rows[0]["MAX_REC_ID"] + 1;
                }

                if (datatable != null)
                {
                    rec_id = rec_id + datatable.Rows.Count;
                }

                DataRow dr = datatable.NewRow();

                dr["GYOUSHA_CD"] = DBNull.Value;
                dr["GYOUSHA_NAME_RYAKU"] = "";
                dr["GENBA_NAME_RYAKU"] = "";
                dr["DETAIL_BUTTON"] = "詳細";
                dr["KAISYUUHIN_NAME"] = "";
                dr["BIKOU"] = "";
                dr["DAY_CD"] = customTextBoxDayCd.Text;
                dr["COURSE_NAME_CD"] = customTextBoxCoureseNameCd.Text;
                dr["REC_ID"] = rec_id;
                dr["KIBOU_TIME"] = "";
                dr["SAGYOU_TIME_MINUTE"] = DBNull.Value;
                dr["NewFlag"] = "True";

                int rowIndex = 0;
                if (0 < this.Ichiran.RowCount)
                {
                    if (this.Ichiran.SelectedCells.Count <= 0)
                    {
                        rowIndex = 0;
                    }
                    else
                    {
                        rowIndex = this.Ichiran.SelectedCells[0].RowIndex;
                    }
                }

                datatable.Rows.InsertAt(dr, rowIndex);
                Ichiran.DataSource = datatable;

                resetREC_ID();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F3] 行削除ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void RecDel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.Ichiran.SelectedCells.Count <= 0)
                {
                    return;
                }
                int rowIndex = this.Ichiran.SelectedCells[0].RowIndex;
                if (rowIndex < 0 || rowIndex >= this.Ichiran.Rows.Count)
                {
                    return;
                }
                DataGridViewRow row = this.Ichiran.Rows[rowIndex];
                if (row != null && !row.IsNewRow)
                {
                    var dayCd = row.Cells["DAY_CD"].Value;
                    var courseNameCd = row.Cells["COURSE_NAME_CD"].Value;
                    var recId = row.Cells["REC_ID"].Value;

                    // 対象のDataRowを削除する
                    this.logic.courseDetailItemsSearchResult.Rows.Cast<DataRow>().Where(d => d.RowState != DataRowState.Deleted
                                                                                          && d["DAY_CD"].Equals(dayCd)
                                                                                          && d["COURSE_NAME_CD"].Equals(courseNameCd)
                                                                                          && d["REC_ID"].Equals(recId))
                                                                                 .ToList()
                                                                                 .ForEach(d => d.Delete());
                    this.logic.courseDetailItemsSearchResult.AcceptChanges();
                    this.Ichiran.Rows.Remove(row);
                    var table = (DataTable)this.Ichiran.DataSource;
                    table.AcceptChanges();
                    this.Ichiran.DataSource = table;
                }
                else
                {
                    this.Ichiran.CurrentCell = this.Ichiran.SelectedCells[0];
                }

                resetREC_ID();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F4] 上ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void RecUp(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                DataGridView dgv = Ichiran;
                DataTable datatable = (DataTable)dgv.DataSource;

                //if (dgv.CurrentCell == null) return;
                //if (dgv.CurrentCell.RowIndex == 0) return;

                if (this.Ichiran.SelectedCells.Count <= 0)
                {
                    return;
                }
                int rowIndex = this.Ichiran.SelectedCells[0].RowIndex;
                if (rowIndex <= 0 || rowIndex >= this.Ichiran.Rows.Count - 1)
                {
                    this.Ichiran.CurrentCell = this.Ichiran.SelectedCells[0];
                    return;
                }
                if (this.Ichiran.Rows[rowIndex] == null || this.Ichiran.Rows[rowIndex].IsNewRow)
                {
                    this.Ichiran.CurrentCell = this.Ichiran.SelectedCells[0];
                    return;
                }

                object[] obj = datatable.Rows[dgv.CurrentCell.RowIndex].ItemArray;
                object[] obj2 = datatable.Rows[dgv.CurrentCell.RowIndex - 1].ItemArray;

                List<string> list = new List<string>();
                foreach (DataColumn col in datatable.Columns)
                {
                    if (col.ReadOnly)
                    {
                        col.ReadOnly = false;
                        list.Add(col.ColumnName);
                    }
                }
                foreach (string col in list)
                {
                    this.Ichiran.Columns[col].ReadOnly = false;
                }

                datatable.Rows[dgv.CurrentCell.RowIndex].ItemArray = obj2;
                datatable.Rows[dgv.CurrentCell.RowIndex - 1].ItemArray = obj;

                foreach (string col in list)
                {
                    this.Ichiran.Columns[col].ReadOnly = true;
                }
                foreach (string col in list)
                {
                    datatable.Columns[col].ReadOnly = true;
                }

                if (!resetREC_ID())
                {
                    return;
                }

                dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex - 1].Cells[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F5] 下ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void RecDown(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                DataGridView dgv = Ichiran;
                DataTable datatable = (DataTable)dgv.DataSource;

                if (this.Ichiran.SelectedCells.Count <= 0)
                {
                    return;
                }
                int rowIndex = this.Ichiran.SelectedCells[0].RowIndex;
                if (rowIndex < 0 || rowIndex >= this.Ichiran.Rows.Count - 2)
                {
                    this.Ichiran.CurrentCell = this.Ichiran.SelectedCells[0];
                    return;
                }
                if (this.Ichiran.Rows[rowIndex] == null || this.Ichiran.Rows[rowIndex].IsNewRow)
                {
                    this.Ichiran.CurrentCell = this.Ichiran.SelectedCells[0];
                    return;
                }


                object[] obj = datatable.Rows[dgv.CurrentCell.RowIndex].ItemArray;

                List<string> list = new List<string>();
                foreach (DataColumn col in datatable.Columns)
                {
                    if (col.ReadOnly)
                    {
                        col.ReadOnly = false;
                        list.Add(col.ColumnName);
                    }
                }
                foreach (string col in list)
                {
                    this.Ichiran.Columns[col].ReadOnly = false;
                }

                datatable.Rows[dgv.CurrentCell.RowIndex].ItemArray = datatable.Rows[dgv.CurrentCell.RowIndex + 1].ItemArray;
                datatable.Rows[dgv.CurrentCell.RowIndex + 1].ItemArray = obj;

                foreach (string col in list)
                {
                    this.Ichiran.Columns[col].ReadOnly = true;
                }
                foreach (string col in list)
                {
                    datatable.Columns[col].ReadOnly = true;
                }

                if (!resetREC_ID())
                {
                    return;
                }

                if (!resetREC_ID())
                {
                    return;
                }

                dgv.CurrentCell = dgv.Rows[dgv.CurrentCell.RowIndex + 1].Cells[0];
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F6] CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.CheckSagyouTime())
                {
                    return;
                }

                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");

                    LogUtility.DebugMethodEnd();
                    return;
                }
                this.logic.CSVOutput();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F7] 一覧表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("M663", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F8] 順番整列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Sort(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //コース明細が表示されていない場合ソート処理を行わない。
                if (this.Ichiran.Rows.Count > 0)
                {
                    // 順番の必須チェック
                    bool isErrorFlag = false;
                    for (int i = 0; i < this.Ichiran.Rows.Count - 1; i++)
                    {
                        DataGridViewRow row = this.Ichiran.Rows[i];
                        if (string.IsNullOrEmpty(row.Cells["ROW_NO2"].FormattedValue.ToString()))
                        {
                            isErrorFlag = true;
                            break;
                        }
                    }

                    if (isErrorFlag)
                    {
                        this.resetREC_ID();
                    }
                    if (this.Ichiran.CurrentCell == null)
                    {
                        this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
                    }
                    this.Ichiran.BeginEdit(false);

                    // 明細リストを「順番」の昇順に並びかえる
                    var table = ((DataTable)this.Ichiran.DataSource).Clone();
                    table.Columns["KAISYUUHIN_NAME"].MaxLength = 200;
                    var sortRows = ((DataTable)this.Ichiran.DataSource).Select(null, "ROW_NO2, ROW_NO");
                    var rowNo = 1;
                    bool isNewRow = true;
                    foreach (var row in sortRows)
                    {
                        isNewRow = true;
                        foreach (object obj in row.ItemArray)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(obj)))
                            {
                                isNewRow = false;
                                break;
                            }
                        }
                        if (isNewRow)
                        {
                            continue;
                        }
                        row["ROW_NO"] = rowNo;
                        table.Rows.Add(row.ItemArray);
                        rowNo++;
                    }
                    this.logic.courseDetailSearchResult = table;
                    this.Ichiran.DataSource = this.logic.courseDetailSearchResult;

                    this.Ichiran.EndEdit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F9] 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    if (!this.logic.CheckSagyouTime())
                    {
                        return;
                    }

                    if (this.logic.ActionBeforeCheck())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                    if (!isCheckOK)
                    {
                        return;
                    }

                    bool catchErr = true;
                    Boolean isOK_cd = this.logic.CreateEntity_M_COURSE_DETAIL(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    Boolean isOK_c = this.logic.CreateEntity_M_COURSE(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    Boolean isOK_cn = this.logic.CreateEntity_M_COURSE_NIOROSHI(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    Boolean isOK_cdi = this.logic.CreateEntity_M_COURSE_DETAIL_ITEMS(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    catchErr = this.logic.CreateEntity_CHANGE_LOG(false);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (this.logic.FromCourseIchiran)
                    {
                        if (!this.logic.UpdateEntiry())
                        {
                            return;
                        }
                    }

                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F10] 地図表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MapOpen(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.mapCountChk())
                {
                    this.errmessage.MessageBoxShowError("地図表示対象の明細がありません。");
                    return;
                }

                if (this.errmessage.MessageBoxShowConfirm("地図を表示しますか？" +
                    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.logic.createMapboxDto();
                if (dtos.Count == 0)
                {
                    this.errmessage.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_COURSE);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private bool mapCountChk()
        {
            // 明細と、組込明細の出力対象が共に0件ならfalseを返す

            if (this.Ichiran.Rows.Count <= 1)
            {
                int j = 0;
                for (int i = 0; i < this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows.Count; i++)
                {
                    if (this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].IsNewRow)
                    {
                        continue;
                    }
                    // 組込がONの明細のみ対象
                    if (this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].Cells["M_GENBA_TEIKI_HINMEI_KUMIKOMI"].Value.Equals(1))
                    {
                        j++;
                    }
                }
                if (j <= 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// [F11] 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.Cancel())
                {
                    return;
                }

                this.logic.ReleaseReferenceMode();

                serchUiLock(false);

                gridUiLock(true);

                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func2.Enabled = false;
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func4.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func8.Enabled = false;
                parentForm.bt_func6.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_func10.Enabled = false;
                parentForm.bt_func11.Enabled = true;
                parentForm.bt_func12.Enabled = true;
                parentForm.bt_process1.Enabled = true;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = true;

                this.customTextBoxDayCd.Focus();
                this.Ichiran.AllowUserToAddRows = false;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [F12] Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.Ichiran.CellValidating -= Ichiran_CellValidating;
                this.customDataGridView_M_COURSE_NIOROSHI.CellValidating -= customDataGridView_M_COURSE_NIOROSHI_CellValidating;
                this.logic.FormClose();

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [1]一括選択(一括解除)ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void SelectedAll(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool selected = false;
            foreach (DataGridViewRow row in this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                if (Convert.ToString(row.Cells["M_GENBA_TEIKI_HINMEI_KUMIKOMI"].Value) == "0")
                {
                    selected = true;
                    break;
                }
            }
            if (selected)
            {
                this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows.Cast<DataGridViewRow>().ToList()
                                                                 .ForEach(r => r.Cells["M_GENBA_TEIKI_HINMEI_KUMIKOMI"].Value = 1);
            }
            else
            {
                this.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows.Cast<DataGridViewRow>().ToList()
                                                                 .ForEach(r => r.Cells["M_GENBA_TEIKI_HINMEI_KUMIKOMI"].Value = 0);
            }
            this.customDataGridView_M_GENBA_TEIKI_HINMEI.Refresh();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [2]組込ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void Kumikomi(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                GetDataSource();
                DataTable datatable = (DataTable)this.Ichiran.DataSource;
                if (null == datatable)
                {
                    return;
                }

                DataView dtGenbaTeikiHinmei = (DataView)customDataGridView_M_GENBA_TEIKI_HINMEI.DataSource;
                dtGenbaTeikiHinmei.Table.Columns["isShow"].ReadOnly = false;

                for (int j = 0; j < dtGenbaTeikiHinmei.Count; j++)
                {
                    // 組込のチェックがついていないものはスルー
                    if (!dtGenbaTeikiHinmei[j]["KUMIKOMI"].Equals(1))
                    {
                        continue;
                    }

                    int recSeq = 1;

                    // 組み込み先のコース詳細を取得
                    DataRow[] drs = datatable.Select("GYOUSHA_CD = '" + dtGenbaTeikiHinmei[j]["GYOUSHA_CD"] + "' AND GENBA_CD = '" + dtGenbaTeikiHinmei[j]["GENBA_CD"] + "' AND ROUND_NO = '" + this.KUMIKOMI_ROUND_NO.Text + "'");
                    DataRow dr;
                    string gyoushaCd = Convert.ToString(dtGenbaTeikiHinmei[j]["GYOUSHA_CD"]);
                    string genbaCd = Convert.ToString(dtGenbaTeikiHinmei[j]["GENBA_CD"]);

                    // M_COURSE_DETAIL
                    if (0 == drs.Length)
                    {
                        bool catchErr = true;
                        DataRow findDr = logic.getRecId(
                                customTextBoxDayCd.Text,
                                customTextBoxCoureseNameCd.Text,
                                (string)dtGenbaTeikiHinmei[j]["GYOUSHA_CD"],
                                (string)dtGenbaTeikiHinmei[j]["GENBA_CD"],
                                this.KUMIKOMI_ROUND_NO.Text,
                                out catchErr
                            );
                        if (!catchErr)
                        {
                            return;
                        }

                        if (null != findDr)
                        {
                            // セルの既定値処理
                            dr = datatable.NewRow();

                            //MCD.ROW_NO,
                            dr["ROW_NO"] = findDr["ROW_NO"];
                            //MCD.ROW// null_NO as ROW_NO2,
                            dr["ROW_NO2"] = findDr["ROW_NO"];
                            //MCD.ROUND_NO,
                            dr["ROUND_NO"] = findDr["ROUND_NO"];
                            //MCD.GYOUSHA_CD,
                            dr["GYOUSHA_CD"] = findDr["GYOUSHA_CD"];
                            //M_GYOUSHA.GYOUSHA_NAME_RYAKU,
                            dr["GYOUSHA_NAME_RYAKU"] = findDr["GYOUSHA_NAME_RYAKU"];
                            //MCD.GENBA_CD,
                            dr["GENBA_CD"] = findDr["GENBA_CD"];
                            //M_GENBA.GENBA_NAME_RYAKU,
                            dr["GENBA_NAME_RYAKU"] = findDr["GENBA_NAME_RYAKU"];
                            //'詳細' as DETAIL_BUTTON,
                            dr["DETAIL_BUTTON"] = "詳細";
                            //'' as KAISYUUHIN_NAME,
                            dr["KAISYUUHIN_NAME"] = "";
                            //MCD.BIKOU,
                            dr["BIKOU"] = "";
                            //MCD.DAY_CD,
                            dr["DAY_CD"] = findDr["DAY_CD"];
                            //MCD.COURSE_NAME_CD,
                            dr["COURSE_NAME_CD"] = findDr["COURSE_NAME_CD"];
                            //MCD.REC_ID,
                            dr["REC_ID"] = findDr["REC_ID"];
                            //MCD.KIBOU_TIME,
                            dr["KIBOU_TIME"] = findDr["KIBOU_TIME"];
                            //MCD.SAGYOU_TIME_MINUTE,
                            dr["SAGYOU_TIME_MINUTE"] = findDr["SAGYOU_TIME_MINUTE"];
                            //MCD.TIME_STAMP
                            dr["TIME_STAMP"] = findDr["TIME_STAMP"];
                            //新規フラグ      
                            dr["NewFlag"] = "True";
                        }
                        else
                        {
                            int rec_id = 0;

                            if (0 < datatable.Rows.Count)
                            {
                                for (int i = 0; i < datatable.Rows.Count; i++)
                                {
                                    if (rec_id < (int)datatable.Rows[i]["REC_ID"])
                                    {
                                        rec_id = (int)datatable.Rows[i]["REC_ID"];
                                    }
                                }
                                rec_id++;
                            }
                            else
                            {
                                rec_id = 1;
                            }

                            // セルの既定値処理
                            dr = datatable.NewRow();

                            //MCD.ROUND_NO,
                            dr["ROUND_NO"] = this.KUMIKOMI_ROUND_NO.Text;
                            //MCD.ROW_NO,
                            // 後処理で設定
                            //MCD.ROW// null_NO as ROW_NO2,
                            // 後処理で設定
                            //MCD.NIOROSHI_NO,
                            dr["GYOUSHA_CD"] = DBNull.Value;
                            //MCD.GYOUSHA_CD,
                            dr["GYOUSHA_CD"] = dtGenbaTeikiHinmei[j]["GYOUSHA_CD"];
                            //M_GYOUSHA.GYOUSHA_NAME_RYAKU,
                            dr["GYOUSHA_NAME_RYAKU"] = dtGenbaTeikiHinmei[j]["GYOUSHA_NAME_RYAKU"];
                            //MCD.GENBA_CD,
                            dr["GENBA_CD"] = dtGenbaTeikiHinmei[j]["GENBA_CD"];
                            //M_GENBA.GENBA_NAME_RYAKU,
                            dr["GENBA_NAME_RYAKU"] = dtGenbaTeikiHinmei[j]["GENBA_NAME_RYAKU"];
                            //'詳細' as DETAIL_BUTTON,
                            dr["DETAIL_BUTTON"] = "詳細";
                            //'' as KAISYUUHIN_NAME,
                            dr["KAISYUUHIN_NAME"] = "";
                            //MCD.BIKOU,
                            dr["BIKOU"] = "";
                            //MCD.DAY_CD,
                            dr["DAY_CD"] = customTextBoxDayCd.Text;
                            //MCD.COURSE_NAME_CD,
                            dr["COURSE_NAME_CD"] = customTextBoxCoureseNameCd.Text;
                            //MCD.REC_ID,
                            dr["REC_ID"] = rec_id;
                            //MCD.KIBOU_TIME,
                            dr["KIBOU_TIME"] = "";
                            //MCD.SAGYOU_TIME_MINUTE,
                            dr["SAGYOU_TIME_MINUTE"] = DBNull.Value;
                            //MCD.TIME_STAMP
                            //新規フラグ      
                            dr["NewFlag"] = "True";
                        }

                        int rowIndex = 0;
                        if (0 < this.Ichiran.SelectedRows.Count)
                        {
                            rowIndex = this.Ichiran.SelectedCells[0].RowIndex;
                        }
                        datatable.Rows.Add(dr);

                        if (!resetREC_ID())
                        {
                            return;
                        }
                    }
                    else
                    {
                        dr = drs[0];

                        DataTable dtDetailItems = (DataTable)logic.courseDetailItemsSearchResult;
                        DataRow[] drsDetailItems = dtDetailItems.Select("DAY_CD = " + dr["DAY_CD"] + " AND COURSE_NAME_CD = '" + dr["COURSE_NAME_CD"] + "' AND REC_ID = " + dr["REC_ID"]);

                        if (0 < drsDetailItems.Length)
                        {
                            for (int i = 0; i < drsDetailItems.Length; i++)
                            {
                                if (recSeq < (int)drsDetailItems[i]["REC_SEQ"])
                                {
                                    recSeq = (int)drsDetailItems[i]["REC_SEQ"];
                                }
                            }
                            recSeq++;
                        }
                    }
                    this.logic.courseDetailSearchResult = datatable;
                    this.Ichiran.DataSource = this.logic.courseDetailSearchResult;

                    // M_COURSE_DETAIL_ITEMS
                    {
                        DataTable dtDetailItems = (DataTable)logic.courseDetailItemsSearchResult;

                        DataRow drDetailItems = dtDetailItems.NewRow();

                        M_GENBA key = new M_GENBA();
                        key.GYOUSHA_CD = gyoushaCd;
                        key.GENBA_CD = genbaCd;
                        M_GENBA genba = this.logic.genbaDao.GetDataByCd(key);

                        //DAY_CD,
                        drDetailItems["DAY_CD"] = dr["DAY_CD"];
                        //COURSE_NAME_CD,
                        drDetailItems["COURSE_NAME_CD"] = dr["COURSE_NAME_CD"];
                        //REC_ID,
                        drDetailItems["REC_ID"] = dr["REC_ID"];
                        //REC_SEQ,
                        drDetailItems["REC_SEQ"] = recSeq;
                        //HINMEI_CD,
                        drDetailItems["HINMEI_CD"] = dtGenbaTeikiHinmei[j]["HINMEI_CD"];
                        //UNIT_CD,
                        drDetailItems["UNIT_CD"] = dtGenbaTeikiHinmei[j]["UNIT_CD"];

                        //HINMEI_NAME_RYAKU
                        drDetailItems["HINMEI_NAME_RYAKU"] = dtGenbaTeikiHinmei[j]["HINMEI_NAME_RYAKU"];

                        //UNIT_NAME
                        drDetailItems["UNIT_NAME"] = dtGenbaTeikiHinmei[j]["UNIT_NAME"];
                        //DELETE_FLG
                        drDetailItems["DELETE_FLG"] = 0;

                        // DENPYOU_KBN_CD
                        drDetailItems["DENPYOU_KBN_CD"] = dtGenbaTeikiHinmei[j]["DENPYOU_KBN_CD"];

                        // KEIJYOU_KBN
                        drDetailItems["KEIJYOU_KBN"] = dtGenbaTeikiHinmei[j]["KEIJYOU_KBN"];

                        // KANSANCHI
                        drDetailItems["KANSANCHI"] = dtGenbaTeikiHinmei[j]["KANSANCHI"];

                        // KANSAN_UNIT_CD
                        drDetailItems["KANSAN_UNIT_CD"] = dtGenbaTeikiHinmei[j]["KANSAN_UNIT_CD"];

                        // KANSAN_UNIT_MOBILE_OUTPUT_FLG
                        drDetailItems["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = dtGenbaTeikiHinmei[j]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"];

                        // ANBUN_FLG
                        drDetailItems["ANBUN_FLG"] = dtGenbaTeikiHinmei[j]["ANBUN_FLG"];

                        // KEIYAKU_KBN
                        drDetailItems["KEIYAKU_KBN"] = dtGenbaTeikiHinmei[j]["KEIYAKU_KBN"];

                        // INPUT_KBN
                        drDetailItems["INPUT_KBN"] = "2";

                        // TEKIYOU_BEGIN
                        drDetailItems["TEKIYOU_BEGIN"] = ((BusinessBaseForm)this.Parent).sysDate.Date;

                        // GENBA_TEKIYOU_BEGIN
                        if (genba != null && !genba.TEKIYOU_BEGIN.IsNull)
                        {
                            drDetailItems["GENBA_TEKIYOU_BEGIN"] = genba.TEKIYOU_BEGIN.Value;
                        }

                        // GENBA_TEKIYOU_END
                        if (genba != null && !genba.TEKIYOU_END.IsNull)
                        {
                            drDetailItems["GENBA_TEKIYOU_END"] = genba.TEKIYOU_END.Value;
                        }

                        dtDetailItems.Rows.Add(drDetailItems);
                    }
                }

                if (!resetKaisyuuhinmei())
                {
                    return;
                }

                if (!changeGenbaTeikiHinmei())
                {
                    return;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [3]荷降No一括入力ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void IkkatsuInput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.logic.ShowNioroshiIkkatsu();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// [4]荷降行削除ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DeleteNioroshiRow(object sender, EventArgs e)
        {
            // 荷降行削除
            this.logic.deleteNioroshiRow();
        }

        /// <summary>
        /// [5]再読込ボタンを押したときに処理されます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {

                if (!base.RegistErrorFlag)
                {
                    this.KUMIKOMI_ROUND_NO.Text = "1";
                    if (this.logic.Search() == -2)
                    {
                        return;
                    }

                    // コースマスタ
                    customTextBox_bikou.DataBindings.Clear();
                    if (1 == this.logic.courseSearchResult.Rows.Count)
                    {
                        customTextBox_bikou.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "COURSE_BIKOU"));
                    }
                    else
                    {
                        DataRow dr = this.logic.courseSearchResult.NewRow();

                        dr["DAY_CD"] = Convert.ToInt16(customTextBoxDayCd.Text);
                        dr["COURSE_NAME_CD"] = customTextBoxCoureseNameCd.Text;
                        dr["COURSE_BIKOU"] = "";

                        this.logic.courseSearchResult.Rows.Add(dr);

                        customTextBox_bikou.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "COURSE_BIKOU"));

                    }

                    // 取得結果をコントロールに格納
                    if (this.logic.courseSearchResult.Rows.Count > 0)
                    {
                        this.SHASHU_CD.Text = this.logic.courseSearchResult.Rows[0]["SHASHU_CD"].ToString();
                        this.SHASHU_NAME.Text = this.logic.courseSearchResult.Rows[0]["SHASHU_NAME"].ToString();
                        this.SHARYOU_CD.Text = this.logic.courseSearchResult.Rows[0]["SHARYOU_CD"].ToString();
                        this.SHARYOU_NAME.Text = this.logic.courseSearchResult.Rows[0]["SHARYOU_NAME"].ToString();
                        this.UNTENSHA_CD.Text = this.logic.courseSearchResult.Rows[0]["UNTENSHA_CD"].ToString();
                        this.UNTENSHA_NAME.Text = this.logic.courseSearchResult.Rows[0]["UNTENSHA_NAME"].ToString();
                        this.UNPAN_GYOUSHA_CD.Text = this.logic.courseSearchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                        this.UNPAN_GYOUSHA_NAME.Text = this.logic.courseSearchResult.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                        this.SHUPPATSU_GYOUSHA_CD.Text = this.logic.courseSearchResult.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                        this.SHUPPATSU_GYOUSHA_NAME.Text = this.logic.courseSearchResult.Rows[0]["SHUPPATSU_GYOUSHA_NAME"].ToString();
                        this.SHUPPATSU_GENBA_CD.Text = this.logic.courseSearchResult.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();
                        this.SHUPPATSU_GENBA_NAME.Text = this.logic.courseSearchResult.Rows[0]["SHUPPATSU_GENBA_NAME"].ToString();
                        this.SHASHU_CD.DataBindings.Clear();
                        this.SHASHU_NAME.DataBindings.Clear();
                        this.SHARYOU_CD.DataBindings.Clear();
                        this.SHARYOU_NAME.DataBindings.Clear();
                        this.UNTENSHA_CD.DataBindings.Clear();
                        this.UNTENSHA_NAME.DataBindings.Clear();
                        this.UNPAN_GYOUSHA_CD.DataBindings.Clear();
                        this.UNPAN_GYOUSHA_NAME.DataBindings.Clear();
                        this.SHUPPATSU_GYOUSHA_CD.DataBindings.Clear();
                        this.SHUPPATSU_GYOUSHA_NAME.DataBindings.Clear();
                        this.SHUPPATSU_GENBA_CD.DataBindings.Clear();
                        this.SHUPPATSU_GENBA_NAME.DataBindings.Clear();
                        this.SHASHU_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHASHU_CD"));
                        this.SHASHU_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHASHU_NAME"));
                        this.SHARYOU_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHARYOU_CD"));
                        this.SHARYOU_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHARYOU_NAME"));
                        this.UNTENSHA_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "UNTENSHA_CD"));
                        this.UNTENSHA_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "UNTENSHA_NAME"));
                        this.UNPAN_GYOUSHA_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "UNPAN_GYOUSHA_CD"));
                        this.UNPAN_GYOUSHA_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "UNPAN_GYOUSHA_NAME"));
                        this.SHUPPATSU_GYOUSHA_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHUPPATSU_GYOUSHA_CD"));
                        this.SHUPPATSU_GYOUSHA_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHUPPATSU_GYOUSHA_NAME"));
                        this.SHUPPATSU_GENBA_CD.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHUPPATSU_GENBA_CD"));
                        this.SHUPPATSU_GENBA_NAME.DataBindings.Add(new Binding("Text", this.logic.courseSearchResult, "SHUPPATSU_GENBA_NAME"));
                        this.SAGYOU_BEGIN_HOUR.Text = Convert.ToString(this.logic.courseSearchResult.Rows[0]["SAGYOU_BEGIN_HOUR"]);
                        this.SAGYOU_BEGIN_MINUTE.Text = Convert.ToString(this.logic.courseSearchResult.Rows[0]["SAGYOU_BEGIN_MINUTE"]);
                        this.SAGYOU_END_HOUR.Text = Convert.ToString(this.logic.courseSearchResult.Rows[0]["SAGYOU_END_HOUR"]);
                        this.SAGYOU_END_MINUTE.Text = Convert.ToString(this.logic.courseSearchResult.Rows[0]["SAGYOU_END_MINUTE"]);
                    }

                    // コース_荷降先
                    this.customDataGridView_M_COURSE_NIOROSHI.DataSource = this.logic.courseNioroshiSearchResult;

                    if (null != sender)
                    {
                        if (!changeGenbaTeikiHinmei())
                        {
                            return;
                        }
                    }

                    if (!resetKaisyuuhinmei())
                    {
                        return;
                    }

                    var table = this.logic.courseDetailSearchResult;
                    if (this.logic.FromCourseIchiran == true)
                    {
                        if (table != null && table.Rows.Count > 0)
                        {
                            for (int i = table.Rows.Count - 1; i >= 0; i--)
                            {
                                string gyoushaCd = string.Empty;
                                string genbacd = string.Empty;
                                string daycd = string.Empty;
                                string courseName = string.Empty;
                                string recId = string.Empty;
                                if (table.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(table.Rows[i]["GENBA_CD"].ToString()))
                                {
                                    genbacd = table.Rows[i]["GENBA_CD"].ToString();
                                }

                                if (table.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(table.Rows[i]["GYOUSHA_CD"].ToString()))
                                {
                                    gyoushaCd = table.Rows[i]["GYOUSHA_CD"].ToString();
                                }

                                if (table.Rows[i]["DAY_CD"] != null && !string.IsNullOrEmpty(table.Rows[i]["DAY_CD"].ToString()))
                                {
                                    daycd = table.Rows[i]["DAY_CD"].ToString();
                                }

                                if (table.Rows[i]["COURSE_NAME_CD"] != null && !string.IsNullOrEmpty(table.Rows[i]["COURSE_NAME_CD"].ToString()))
                                {
                                    courseName = table.Rows[i]["COURSE_NAME_CD"].ToString();
                                }

                                if (table.Rows[i]["REC_ID"] != null && !string.IsNullOrEmpty(table.Rows[i]["REC_ID"].ToString()))
                                {
                                    recId = table.Rows[i]["REC_ID"].ToString();
                                }
                                DataTable dr = this.logic.courseDetailDao.CheckIchiranDataSql(gyoushaCd, genbacd, daycd, courseName, recId, this.dayCdF);
                                if (dr == null || dr.Rows.Count < 1)
                                {
                                    table.Rows.RemoveAt(i);
                                }
                            }
                        }
                    }

                    this.Ichiran.DataSource = table;
                    if (!resetREC_ID())
                    {
                        return;
                    }

                    this.logic.courseDetail = table.Clone();
                    this.logic.courseDetail.Columns["KAISYUUHIN_NAME"].MaxLength = 200;
                    foreach (DataRow row in table.Rows)
                    {
                        this.logic.courseDetail.ImportRow(row);
                    }
                    serchUiLock(true);
                    gridUiLock(false);

                    var parentForm = (BusinessBaseForm)this.Parent;
                    parentForm.bt_func2.Enabled = true;
                    parentForm.bt_func3.Enabled = true;
                    parentForm.bt_func4.Enabled = true;
                    parentForm.bt_func5.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func8.Enabled = true;
                    parentForm.bt_func9.Enabled = true;
                    // オプション
                    if (AppConfig.AppOptions.IsMAPBOX())
                    {
                        parentForm.bt_func10.Enabled = true;
                    }
                    parentForm.bt_func11.Enabled = true;
                    parentForm.bt_func12.Enabled = true;
                    parentForm.bt_process1.Enabled = true;
                    parentForm.bt_process2.Enabled = true;
                    parentForm.bt_process3.Enabled = true;
                    parentForm.bt_process4.Enabled = true;
                    parentForm.bt_process5.Enabled = true;

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        this.logic.SetReferenceMode();
                    }

                    // 備考にフォーカス
                    this.customTextBox_bikou.Focus();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private bool resetREC_ID()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                DataGridViewRow row = null;
                for (int k = 0; k < this.Ichiran.Rows.Count; k++)
                {
                    row = this.Ichiran.Rows[k];
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    if (DBNull.Value.Equals(row.Cells["ROW_NO"].Value) ||
                        (int)row.Cells["ROW_NO"].Value != k + 1)
                    {
                        row.Cells["ROW_NO"].Value = k + 1;
                    }
                    if (DBNull.Value.Equals(row.Cells["ROW_NO2"].Value) ||
                        (int)row.Cells["ROW_NO2"].Value != k + 1)
                    {
                        row.Cells["ROW_NO2"].Value = k + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("resetREC_ID", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        private void customTextBoxDayCd_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (0 < customTextBoxDayCd.Text.Length)
                {
                    if (!getCourseName())
                    {
                        return;
                    }
                    if (!crearCoureseName())
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public bool getCourseName()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // コース名称ポップアップ設定
                if (false == string.IsNullOrEmpty(customTextBoxDayCd.Text))
                {
                    // 戻り先のコントロール名設定は以前のまま
                    // ポップアップの左上のタイトル
                    this.customTextBoxCoureseNameCd.PopupWindowId = WINDOW_ID.M_COURSE_NAME;
                    // ポップアップに表示するデータ列(物理名)
                    this.customTextBoxCoureseNameCd.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME";

                    // ---20140116 oonaka delete 曜日選択値不正対応 start ---
                    //// 表示用データ取得＆加工
                    //this.logic.getCourseName(
                    //    ((HeaderForm)((BusinessBaseForm)Parent).headerForm).KYOTEN_CD.Text,
                    //customRadioButton1.Checked,
                    //customRadioButton2.Checked,
                    //customRadioButton3.Checked,
                    //customRadioButton4.Checked,
                    //customRadioButton5.Checked,
                    //customRadioButton6.Checked,
                    //customRadioButton7.Checked
                    //);
                    // ---20140116 oonaka delete 曜日選択値不正対応 end ---

                    // ---20140116 oonaka add 曜日選択値不正対応 start ---
                    // 表示用データ取得＆加工
                    string youbi = this.customTextBoxDayCd.Text.Trim();
                    this.logic.getCourseName(
                        ((HeaderForm)((BusinessBaseForm)Parent).headerForm).KYOTEN_CD.Text,
                        youbi.Equals("1"),
                        youbi.Equals("2"),
                        youbi.Equals("3"),
                        youbi.Equals("4"),
                        youbi.Equals("5"),
                        youbi.Equals("6"),
                        youbi.Equals("7")
                    );
                    // ---20140116 oonaka add 曜日選択値不正対応 end ---

                    var ShainDataTable = this.logic.courseNameSearchResult;// = this.GetPopUpData(dayCd);courseNameSearchResult
                    // TableNameを設定すれば、ポップアップのタイトルになる
                    ShainDataTable.TableName = "コース名称検索";

                    // 列名とデータソース設定
                    this.customTextBoxCoureseNameCd.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称" };
                    this.customTextBoxCoureseNameCd.PopupDataSource = ShainDataTable;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getCourseName", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseName", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        public bool crearCoureseName()
        {
            LogUtility.DebugMethodStart();
            try
            {

                if (0 < customTextBoxCoureseNameCd.Text.Length)
                {
                    string filter = "COURSE_NAME_CD = " + "'" + customTextBoxCoureseNameCd.Text + "'";

                    if (0 < ((HeaderForm)((BusinessBaseForm)Parent).headerForm).KYOTEN_CD.Text.Length)
                    {
                        filter += " AND (KYOTEN_CD = 99 OR KYOTEN_CD = " + ((HeaderForm)((BusinessBaseForm)Parent).headerForm).KYOTEN_CD.Text + " ) ";
                    }

                    // ---20140115 oonaka add 曜日選択値不正対応 start ---
                    string youbi = this.customTextBoxDayCd.Text.Trim();
                    if (youbi.Equals("1"))
                    {
                        filter += " AND MONDAY = 'True'";
                    }
                    if (youbi.Equals("2"))
                    {
                        filter += " AND TUESDAY = 'True'";
                    }
                    if (youbi.Equals("3"))
                    {
                        filter += " AND WEDNESDAY = 'True'";
                    }
                    if (youbi.Equals("4"))
                    {
                        filter += " AND THURSDAY = 'True'";
                    }
                    if (youbi.Equals("5"))
                    {
                        filter += " AND FRIDAY = 'True'";
                    }
                    if (youbi.Equals("6"))
                    {
                        filter += " AND SATURDAY = 'True'";
                    }
                    if (youbi.Equals("7"))
                    {
                        filter += " AND SUNDAY = 'True'";
                    }
                    // ---20140115 oonaka add 曜日選択値不正対応 end ---

                    DataRow[] dr = this.customTextBoxCoureseNameCd.PopupDataSource.Select(filter);

                    if (0 == dr.Length)
                    {
                        customTextBoxCoureseNameCd.Text = "";
                        customTextBoxCoureseName.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("crearCoureseName", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        // ---20140125 oonaka delete 拠点CDによる業者、現場ﾎﾟｯﾌﾟｱｯﾌﾟの制限を外す start ---
        #region
        //// ---20140120 oonaka delete 拠点CD設定不正対応 start ---
        ////public void changeGyoushaCd(string kyotenCd)
        ////{
        ////    LogUtility.DebugMethodStart();

        ////    GYOUSHA_CD.PopupSearchSendParams[0].Value = kyotenCd;
        ////    GENBA_CD.PopupSearchSendParams[1].Value = kyotenCd;

        ////    M_COURSE_NIOROSHI_GYOUSHA_CD.PopupSearchSendParams[1].Value = kyotenCd;
        ////    M_COURSE_NIOROSHI_NIOROSHI_GENBA_CD.PopupSearchSendParams[1].Value = kyotenCd;

        ////    LogUtility.DebugMethodEnd();
        ////}
        //// ---20140120 oonaka delete 拠点CD設定不正対応 end ---

        //// ---20140120 oonaka add 拠点CD設定不正対応 start ---
        //public void changeGyoushaCd(string kyotenCd)
        //{
        //    LogUtility.DebugMethodStart();

        //    if (!string.IsNullOrWhiteSpace(kyotenCd))
        //    {
        //        kyotenCd = kyotenCd.PadLeft(2, '0');

        //        // 業者ポップアップの拠点CDによる制限追加
        //        this.SetPopupSearchSendParams(kyotenCd, this.GYOUSHA_CD.PopupSearchSendParams);

        //        // 現場ポップアップの拠点CDによる制限追加
        //        this.SetPopupSearchSendParams(kyotenCd, this.GENBA_CD.PopupSearchSendParams);

        //        // 荷降業者ポップアップの拠点CDによる制限追加
        //        this.SetPopupSearchSendParams(kyotenCd, this.M_COURSE_NIOROSHI_GYOUSHA_CD.PopupSearchSendParams);

        //        // 荷降現場ポップアップの拠点CDによる制限追加
        //        this.SetPopupSearchSendParams(kyotenCd, this.M_COURSE_NIOROSHI_GENBA_CD.PopupSearchSendParams);
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// 検索ポップアップの条件追加（設定）
        ///// </summary>
        ///// <param name="kyotenCd"></param>
        ///// <param name="popupSearchParams"></param>
        //private void SetPopupSearchSendParams(string kyotenCd, Collection<PopupSearchSendParamDto> popupSearchParams)
        //{
        //    bool addFlg = false;
        //    PopupSearchSendParamDto param;
        //    if (popupSearchParams != null)
        //    {
        //        // 既存設定の取得
        //        param = popupSearchParams.Cast<PopupSearchSendParamDto>().Where(t => t.KeyName.Equals("KYOTEN_CD")).FirstOrDefault();

        //        // 既存設定がなければ作成
        //        if (param == null)
        //        {
        //            param = new PopupSearchSendParamDto();
        //            addFlg = true;
        //        }

        //        // 設定
        //        param.And_Or = CONDITION_OPERATOR.AND;
        //        param.Value = kyotenCd;
        //        param.KeyName = "KYOTEN_CD";

        //        // 追加する場合
        //        if (addFlg)
        //        {
        //            popupSearchParams.Add(param);
        //        }
        //    }
        //}
        //// ---20140120 oonaka add 拠点CD設定不正対応 end ---
        #endregion
        // ---20140125 oonaka delete 拠点CDによる業者、現場ﾎﾟｯﾌﾟｱｯﾌﾟの制限を外す start ---

        public void gridUiLock(bool b)
        {
            LogUtility.DebugMethodStart();

            customTextBox_bikou.Enabled = !b;
            customDataGridView_M_COURSE_NIOROSHI.Enabled = !b;
            Ichiran.Enabled = !b;
            customDataGridView_M_GENBA_TEIKI_HINMEI.Enabled = !b;
            customTextBoxHinmeiNameCD.Enabled = !b;

            shikuChousonCdTextBox.Enabled = !b;
            gyoushaCdTextBox.Enabled = !b;
            genbaCdTextBox.Enabled = !b;

            SHIKUCHOUSON_SEARCH_BUTTON.Enabled = !b;
            HINMEI_SEARCH_BUTTON.Enabled = !b;
            GYOUSHA_SEARCH_BUTTON.Enabled = !b;
            GENBA_SEARCH_BUTTON.Enabled = !b;

            LogUtility.DebugMethodEnd();
        }

        public void serchUiLock(bool b)
        {
            LogUtility.DebugMethodStart(b);

            ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm).KYOTEN_CD.Enabled = !b;

            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Enabled = !b;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Enabled = !b;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Enabled = !b;

            this.customTextBoxDayCd.Enabled = !b;
            this.customRadioButton1.Enabled = !b;
            this.customRadioButton2.Enabled = !b;
            this.customRadioButton3.Enabled = !b;
            this.customRadioButton4.Enabled = !b;
            this.customRadioButton5.Enabled = !b;
            this.customRadioButton6.Enabled = !b;
            this.customRadioButton7.Enabled = !b;

            this.customTextBoxCoureseNameCd.Enabled = !b;

            LogUtility.DebugMethodEnd();
        }

        private void customDataGridView_M_COURSE_NIOROSHI_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                DataGridView dgv = (DataGridView)sender;

                if (DBNull.Value.Equals(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value) &&
                    !DBNull.Value.Equals(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value) &&
                    0 < ((string)dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value).Length &&
                    !DBNull.Value.Equals(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value) &&
                    0 < ((string)dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value).Length)
                {
                    int no = 0;
                    for (int i = 0; i < dgv.RowCount - 1; i++)
                    {
                        if (!DBNull.Value.Equals(dgv.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value) &&
                            no < (int)dgv.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value)
                        {
                            no = (int)dgv.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value;
                        }
                    }

                    dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value = no + 1;
                }

                //新規行の場合は、曜日とコース名ＣＤを設定する。
                //曜日
                if (dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_DAY_CD"].Value != null &&
                    e.RowIndex != customDataGridView_M_COURSE_NIOROSHI.RowCount - 1)
                {
                    if (string.IsNullOrEmpty(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_DAY_CD"].Value.ToString()))
                    {
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_DAY_CD"].Value = this.customTextBoxDayCd.Text;
                    }
                }
                //コース名ＣＤ
                if (dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value != null &&
                    e.RowIndex != customDataGridView_M_COURSE_NIOROSHI.RowCount - 1)
                {
                    if (dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value == null ||
                        string.IsNullOrEmpty(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value.ToString()))
                    {
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value = this.customTextBoxCoureseNameCd.Text;
                    }
                }

                // ---20140114 oonaka add 親(荷降業者CD)を削除、変更した場合、子(荷降現場CD)を削除する対応 start ---

                // 業者CDの変更があった場合
                if (e.ColumnIndex == 1)
                {
                    string oldValue = customDataGridView_M_COURSE_NIOROSHI.CellValidatingOldValue as string;
                    string newValue = customDataGridView_M_COURSE_NIOROSHI.CellValidatingNewValue as string;

                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    // 変更有り
                    if (oldValue != null && newValue != null && !newValue.PadLeft(6, '0').ToUpper().Equals(oldValue.PadLeft(6, '0').ToUpper()))
                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    {
                        // 現場CD、現場名を初期化する
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value = DBNull.Value;
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
                    }

                    // 業者CDがnullの場合
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        // 業者名、現場CD、現場名を初期化する
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value = DBNull.Value;
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
                    }
                }

                // 現場CDの変更があった場合
                if (e.ColumnIndex == 3)
                {
                    string oldValue = customDataGridView_M_COURSE_NIOROSHI.CellValidatingOldValue as string;
                    string newValue = customDataGridView_M_COURSE_NIOROSHI.CellValidatingNewValue as string;

                    // 現場CDがnullの場合
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
                    }
                }
                // ---20140114 oonaka add 親(荷降業者CD)を削除、変更した場合、子(荷降現場CD)を削除する対応 end ---

                // ---20140114 oonaka add 更新エラーの対処[荷降NOの有効性確認] start ---
                // 荷降Noの有効性確認
                if (string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value as string))
                {
                    // 荷降業者CD、荷降現場CDがない場合は
                    if (string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value as string)
                        || string.IsNullOrWhiteSpace(dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value as string)
                        )
                    {
                        // 荷降No削除
                        dgv.Rows[e.RowIndex].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value = DBNull.Value;
                    }
                }
                // ---20140114 oonaka add 更新エラーの対処[荷降NOの有効性確認] end ---

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        public void NiorosigyousyaPopBefore()
        {

            ZenNiorosiGyosyaCD = customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString();
        }

        public void NiorosigyousyaPopAfter()
        {
            if (customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value == null)
            {
                // 業者名、現場CD、現場名を初期化する
                customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value = DBNull.Value;
                customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
            }
            else if (ZenNiorosiGyosyaCD != customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString())
            {
                // 現場CD、現場名を初期化する
                customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value = DBNull.Value;
                customDataGridView_M_COURSE_NIOROSHI.CurrentRow.Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value = string.Empty;
            }
        }
        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        // ---20140114 oonaka add 親(業者CD)を削除、変更した場合、子(現場CD)を削除する対応 start ---
        private void Ichiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

            LogUtility.DebugMethodStart(sender, e);

            try
            {
                DgvCustom dgv = sender as DgvCustom;
                string colName = dgv.Columns[e.ColumnIndex].Name;
                var row = this.Ichiran.Rows[e.RowIndex];
                if (!row.IsNewRow && string.IsNullOrEmpty(Convert.ToString(row.Cells["DETAIL_BUTTON"].Value)))
                {
                    RowAdded(e.RowIndex);
                }

                GetDataSource();
                if (true == this.logic.createRoundNo(sender, e))
                {
                    // 業者CDの変更があった場合
                    if (colName.Equals("GYOUSHA_CD"))
                    {
                        var oldValue = String.Empty;
                        var newValue = String.Empty;
                        if (isGyoushaPopup)
                        {
                            oldValue = this.popupBeforeGyoushaCd;
                            this.isGyoushaPopup = false;
                            this.popupBeforeGyoushaCd = String.Empty;
                        }
                        else
                        {
                            oldValue = dgv.CellValidatingOldValue as string;
                        }
                        newValue = dgv.CellValidatingNewValue as string;
                        if (string.IsNullOrEmpty(newValue))
                        {
                            newValue = string.Empty;
                        }
                        else
                        {
                            newValue = newValue.PadLeft(6, '0').ToUpper();
                        }
                        if (string.IsNullOrEmpty(oldValue))
                        {
                            oldValue = string.Empty;
                        }
                        else
                        {
                            oldValue = oldValue.PadLeft(6, '0').ToUpper();
                        }

                        // 変更有り
                        if (!newValue.Equals(oldValue))
                        {
                            // 現場CD、現場名を初期化する
                            dgv.Rows[e.RowIndex].Cells["GENBA_CD"].Value = DBNull.Value;
                            dgv.Rows[e.RowIndex].Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                        }

                        // 業者CDがnullの場合
                        if (string.IsNullOrWhiteSpace(newValue))
                        {
                            // 業者名、現場CD、現場名を初期化する
                            dgv.Rows[e.RowIndex].Cells["GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                            dgv.Rows[e.RowIndex].Cells["GENBA_CD"].Value = DBNull.Value;
                            dgv.Rows[e.RowIndex].Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                        }

                        if (!newValue.Equals(oldValue))
                        {
                            // CDが変更された場合は、回収品名詳細をクリア
                            this.ClearKaishuHinmeiShousai(dgv.Rows[e.RowIndex]);
                        }
                    }

                    // 現場CDの変更があった場合
                    if (colName.Equals("GENBA_CD"))
                    {
                        var oldValue = String.Empty;
                        var newValue = String.Empty;
                        if (isGenbaPopup)
                        {
                            oldValue = this.popupBeforeGenbaCd;
                            this.isGenbaPopup = false;
                            this.popupBeforeGenbaCd = String.Empty;
                        }
                        else
                        {
                            oldValue = dgv.CellValidatingOldValue as string;
                        }
                        newValue = dgv.CellValidatingNewValue as string;

                        if (string.IsNullOrEmpty(newValue))
                        {
                            newValue = string.Empty;
                        }
                        else
                        {
                            newValue = newValue.PadLeft(6, '0').ToUpper();
                        }
                        if (string.IsNullOrEmpty(oldValue))
                        {
                            oldValue = string.Empty;
                        }
                        else
                        {
                            oldValue = oldValue.PadLeft(6, '0').ToUpper();
                        }

                        // 現場CDがnullの場合
                        if (string.IsNullOrWhiteSpace(newValue))
                        {
                            dgv.Rows[e.RowIndex].Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                        }

                        if (oldValue != newValue)
                        {
                            // CDが変更された場合は、回収品名詳細をクリア
                            this.ClearKaishuHinmeiShousai(dgv.Rows[e.RowIndex]);
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // ---20140114 oonaka add 親(業者CD)を削除、変更した場合、子(現場CD)を削除する対応 end ---

        /// <summary>
        /// 引数で与えられたコース詳細の回収品名詳細をクリアします
        /// </summary>
        /// <param name="dgvRow">コース詳細一覧の行</param>
        private bool ClearKaishuHinmeiShousai(DataGridViewRow dgvRow)
        {
            try
            {
                var dayCd = dgvRow.Cells["DAY_CD"].Value;
                var courseNameCd = dgvRow.Cells["COURSE_NAME_CD"].Value;
                var recId = dgvRow.Cells["REC_ID"].Value;

                // 対象のDataRowを削除する
                this.logic.courseDetailItemsSearchResult.Rows.Cast<DataRow>().Where(d => d.RowState != DataRowState.Deleted
                                                                                      && d["DAY_CD"].Equals(dayCd)
                                                                                      && d["COURSE_NAME_CD"].Equals(courseNameCd)
                                                                                      && d["REC_ID"].Equals(recId))
                                                                             .ToList()
                                                                             .ForEach(d => d.Delete());
                // 変更をコミット
                //((DataTable)this.Ichiran.DataSource).AcceptChanges();

                if (!this.resetKaisyuuhinmei())
                {
                    return false;
                }
                if (!this.changeGenbaTeikiHinmei())
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearKaishuHinmeiShousai", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        private void customTextBoxCoureseNameCd_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (0 == customTextBoxCoureseNameCd.Text.Length)
                {
                    customTextBoxCoureseName.Text = "";
                    return;
                }

                // 表示用データ取得＆加工
                this.logic.getCourseName(
                    ((HeaderForm)((BusinessBaseForm)Parent).headerForm).KYOTEN_CD.Text,
                    customRadioButton1.Checked,
                    customRadioButton2.Checked,
                    customRadioButton3.Checked,
                    customRadioButton4.Checked,
                    customRadioButton5.Checked,
                    customRadioButton6.Checked,
                    customRadioButton7.Checked
                    );

                DataRow[] drs = this.logic.courseNameSearchResult.Select("COURSE_NAME_CD = '" + customTextBoxCoureseNameCd.Text.PadLeft(6, '0') + "'");

                if (1 != drs.Length)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "コース名称");

                    customTextBoxCoureseName.Text = "";
                    customTextBoxCoureseNameCd.Focus();

                    return;
                }

                customTextBoxCoureseName.Text = drs[0]["COURSE_NAME"].ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コースCDValidated処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customTextBoxCoureseNameCd_Validated(object sender, EventArgs e)
        {
            if (false == string.IsNullOrEmpty(this.customTextBoxCoureseName.Text))
            {
                // コース入力変更時再検索を行う
                var autoCheckLogic = new AutoRegistCheckLogic(this.allControl);
                this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                this.Search(sender, e);
                this.Ichiran.AllowUserToAddRows = true;
            }
        }

        private void Ichiran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //ヘッダクリックは走らない。
                if (e.RowIndex < 0)
                    return;
                var msgLogic = new MessageBoxShowLogic();
                DataGridView dgv = (DataGridView)sender;

                //"Button"列ならば、ボタンがクリックされた
                if (dgv.Columns[e.ColumnIndex].Name == "DETAIL_BUTTON")
                {
                    if (DBNull.Value.Equals(dgv["GYOUSHA_CD", e.RowIndex].Value) ||
                        DBNull.Value.Equals(dgv["GENBA_CD", e.RowIndex].Value))
                    {
                        msgLogic.MessageBoxShow("E012", "業者CDと現場CD");
                        return;
                    }

                    int dayCd = (short)dgv["DAY_CD", e.RowIndex].Value;
                    string courseNameCd = (string)dgv["COURSE_NAME_CD", e.RowIndex].Value;
                    int recId = (int)dgv["REC_ID", e.RowIndex].Value;
                    string gyoushaCd = (string)dgv["GYOUSHA_CD", e.RowIndex].Value;
                    string genbaCd = (string)dgv["GENBA_CD", e.RowIndex].Value;

                    DataView dv = logic.courseDetailItemsSearchResult.DefaultView;
                    dv.RowFilter = "DAY_CD = " + dayCd.ToString() + " AND COURSE_NAME_CD = '" + courseNameCd + "' AND REC_ID = " + recId.ToString();
                    dv.Sort = "HINMEI_CD ASC";

                    List<DTOClass> list = new List<DTOClass>(dv.Count);
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DTOClass l = new DTOClass();

                        l.DELETE_FLG = (int)dv[i]["DELETE_FLG"];
                        l.REC_ID = (int)dv[i]["REC_ID"];
                        l.REC_SEQ = (int)dv[i]["REC_SEQ"];
                        l.HINMEI_CD = (DBNull.Value.Equals(dv[i]["HINMEI_CD"]) ? null : (string)dv[i]["HINMEI_CD"]);
                        l.HINMEI_NAME = null; // (DBNull.Value.Equals(dv[i]["HINMEI_NAME_RYAKU"]) ? null : (string)dv[i]["HINMEI_NAME_RYAKU"]);
                        l.UNIT_CD = (DBNull.Value.Equals(dv[i]["UNIT_CD"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["UNIT_CD"].ToString()));
                        l.UNIT_NAME = null; //(DBNull.Value.Equals(dv[i]["UNIT_NAME"]) ? null: (string)dv[i]["UNIT_NAME"]);
                        l.KANSANCHI = (DBNull.Value.Equals(dv[i]["KANSANCHI"]) ? SqlDecimal.Null : SqlDecimal.Parse(dv[i]["KANSANCHI"].ToString()));
                        l.KANSAN_UNIT_CD = (DBNull.Value.Equals(dv[i]["KANSAN_UNIT_CD"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["KANSAN_UNIT_CD"].ToString()));
                        l.KANSAN_UNIT_NAME = null; // (DBNull.Value.Equals(dv[i]["KANSAN_UNIT_NAME"]) ? null : (string)dv[i]["KANSAN_UNIT_NAME"]);

                        // ---20140130 oonaka add ポップアップ連携項目追加 start ---
                        l.DENPYOU_KBN_CD = (DBNull.Value.Equals(dv[i]["DENPYOU_KBN_CD"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["DENPYOU_KBN_CD"].ToString()));
                        // NULLの場合はデフォルト値の「0」として扱う
                        l.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (DBNull.Value.Equals(dv[i]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"]) ? SqlBoolean.True : (bool)dv[i]["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] ? SqlBoolean.True : SqlBoolean.False);
                        l.KEIYAKU_KBN = (DBNull.Value.Equals(dv[i]["KEIYAKU_KBN"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["KEIYAKU_KBN"].ToString()));
                        l.KEIJYOU_KBN = (DBNull.Value.Equals(dv[i]["KEIJYOU_KBN"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["KEIJYOU_KBN"].ToString()));
                        // ---20140130 oonaka add ポップアップ連携項目追加 end ---

                        l.INPUT_KBN = (DBNull.Value.Equals(dv[i]["INPUT_KBN"]) ? SqlInt16.Null : SqlInt16.Parse(dv[i]["INPUT_KBN"].ToString()));
                        l.NIOROSHI_NUMBER = (DBNull.Value.Equals(dv[i]["NIOROSHI_NUMBER"]) ? SqlInt32.Null : SqlInt32.Parse(dv[i]["NIOROSHI_NUMBER"].ToString()));
                        l.ANBUN_FLG = (DBNull.Value.Equals(dv[i]["ANBUN_FLG"]) ? SqlBoolean.Null : SqlBoolean.Parse(dv[i]["ANBUN_FLG"].ToString()));
                        l.TEKIYOU_BEGIN = (DBNull.Value.Equals(dv[i]["TEKIYOU_BEGIN"]) ? SqlDateTime.Null : SqlDateTime.Parse(dv[i]["TEKIYOU_BEGIN"].ToString()));
                        l.TEKIYOU_END = (DBNull.Value.Equals(dv[i]["TEKIYOU_END"]) ? SqlDateTime.Null : SqlDateTime.Parse(dv[i]["TEKIYOU_END"].ToString()));
                        list.Add(l);
                    }
                    dv.RowFilter = "";

                    List<string> nioroshiList = new List<string>();
                    string niorishiNum = string.Empty;
                    foreach (DataGridViewRow row in this.customDataGridView_M_COURSE_NIOROSHI.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        niorishiNum = Convert.ToString(row.Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value);
                        if (!string.IsNullOrEmpty(niorishiNum))
                        {
                            nioroshiList.Add(niorishiNum);
                        }
                    }
                    {
                        WINDOW_TYPE windowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        DateTime now = ((BusinessBaseForm)this.Parent).sysDate.Date;
                        Shougun.Core.Common.KaisyuuHinmeShousai.UIForm callForm =
                        new Shougun.Core.Common.KaisyuuHinmeShousai.UIForm(windowType, recId, 0, gyoushaCd, genbaCd, list, nioroshiList, "M232", now);

                        var headerForm = new Shougun.Core.Common.KaisyuuHinmeShousai.UIHeader();
                        var popForm = new BasePopForm(callForm, headerForm);
                        var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                        if (!isExistForm)
                        {
                            popForm.ShowDialog();
                            List<Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass> RetCntRetList = callForm.RetKaishuHinmeiSyousaiList;

                            foreach (DataColumn dc in dv.Table.Columns)
                            {
                                dc.ReadOnly = false;
                            }

                            if (RetCntRetList.Count > 0)
                            {
                                dv.RowFilter = "DAY_CD = " + dayCd.ToString() + " AND COURSE_NAME_CD = '" + courseNameCd + "' AND REC_ID = " + RetCntRetList[0].REC_ID;
                                var count = dv.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    // 古いデータを全削除
                                    dv[0]["DELETE_FLG"] = 1;
                                    dv[0].Delete();
                                }

                                foreach (Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass dto in RetCntRetList)
                                {
                                    // レコード追加
                                    DataRow dr = dv.Table.NewRow();

                                    dr["DAY_CD"] = dayCd;
                                    dr["COURSE_NAME_CD"] = courseNameCd;

                                    dr["DELETE_FLG"] = dto.DELETE_FLG;
                                    dr["REC_ID"] = dto.REC_ID;
                                    dr["REC_SEQ"] = dto.REC_SEQ;
                                    dr["HINMEI_CD"] = dto.HINMEI_CD;
                                    dr["HINMEI_NAME_RYAKU"] = dto.HINMEI_NAME;
                                    if (!dto.UNIT_CD.IsNull)
                                    {
                                        dr["UNIT_CD"] = dto.UNIT_CD.Value;
                                        dr["UNIT_NAME"] = dto.UNIT_NAME;
                                    }

                                    if (!dto.DENPYOU_KBN_CD.IsNull)
                                    {
                                        dr["DENPYOU_KBN_CD"] = dto.DENPYOU_KBN_CD.Value;
                                    }
                                    if (!dto.KANSANCHI.IsNull)
                                    {
                                        dr["KANSANCHI"] = dto.KANSANCHI.ToString();
                                    }
                                    if (!dto.KANSAN_UNIT_CD.IsNull)
                                    {
                                        dr["KANSAN_UNIT_CD"] = dto.KANSAN_UNIT_CD.Value;
                                    }
                                    // NULLの場合はデフォルト値の「0」として扱う
                                    dr["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = true;
                                    if (!dto.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
                                    {
                                        dr["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = dto.KANSAN_UNIT_MOBILE_OUTPUT_FLG.Value;
                                    }
                                    if (!dto.KEIYAKU_KBN.IsNull)
                                    {
                                        dr["KEIYAKU_KBN"] = dto.KEIYAKU_KBN.Value;
                                    }
                                    if (!dto.KEIJYOU_KBN.IsNull)
                                    {
                                        dr["KEIJYOU_KBN"] = dto.KEIJYOU_KBN.Value;
                                    }

                                    if (!dto.INPUT_KBN.IsNull)
                                    {
                                        dr["INPUT_KBN"] = dto.INPUT_KBN.Value;
                                    }

                                    if (!dto.NIOROSHI_NUMBER.IsNull)
                                    {
                                        dr["NIOROSHI_NUMBER"] = dto.NIOROSHI_NUMBER.Value;
                                    }

                                    if (!dto.ANBUN_FLG.IsTrue)
                                    {
                                        dr["ANBUN_FLG"] = false;
                                    }
                                    else
                                    {
                                        dr["ANBUN_FLG"] = true;
                                    }

                                    if (!dto.TEKIYOU_BEGIN.IsNull)
                                    {
                                        dr["TEKIYOU_BEGIN"] = dto.TEKIYOU_BEGIN.Value;
                                    }

                                    if (!dto.TEKIYOU_END.IsNull)
                                    {
                                        dr["TEKIYOU_END"] = dto.TEKIYOU_END.Value;
                                    }

                                    if (!dto.GENBA_TEKIYOU_BEGIN.IsNull)
                                    {
                                        dr["GENBA_TEKIYOU_BEGIN"] = dto.GENBA_TEKIYOU_BEGIN.Value;
                                    }

                                    if (!dto.GENBA_TEKIYOU_END.IsNull)
                                    {
                                        dr["GENBA_TEKIYOU_END"] = dto.GENBA_TEKIYOU_END.Value;
                                    }

                                    dv.Table.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    this.logic.courseDetailItemsSearchResult.AcceptChanges();
                    // 回収品名更新
                    if (!resetKaisyuuhinmei())
                    {
                        return;
                    }
                    this.GetDataSource();
                    this.logic.courseDetailSearchResult.AcceptChanges();
                    this.Ichiran.DataSource = this.logic.courseDetailSearchResult;
                    ((DataTable)this.Ichiran.DataSource).AcceptChanges();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //// カレントセルの取得
            //var cell = this.Ichiran.CurrentCell;

            //// 編集列名のセット
            //var colName = this.Ichiran.Columns[cell.ColumnIndex].Name;

            //if(colName == "chb_delete")
            //{
            //    // 値変更時
            //    if(this.Ichiran.IsCurrentCellDirty == true)
            //    {
            //        // 削除フラグチェックボックス変更時処理
            //        if(false == this.logic.deleteCheck(this.Ichiran.Rows[cell.RowIndex]))
            //        {
            //            // エラー発生のため、入力をキャンセル
            //            this.Ichiran.CancelEdit();
            //        }
            //        else
            //        {
            //            // 入力をコミット
            //            this.Ichiran.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// コース荷降GridViewのCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView_M_COURSE_NIOROSHI_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                int row = e.RowIndex;

                // --20140120 oonaka add 業者フォーカスアウトチェック追加 start ---

                // 業者チェック
                if (e.ColumnIndex == 1)
                {
                    if (!this.logic.ChkGridNioroshiGyousha(row))
                    {
                        e.Cancel = true;
                    }
                }
                // --20140120 oonaka add 業者フォーカスアウトチェック追加 end ---



                //現場チェック
                if (e.ColumnIndex == 3)
                {
                    if (!this.logic.ChkGridNioroshiGenba(row))
                    {
                        e.Cancel = true;
                        // ---20140116 oonaka delete 例外発生対応 start ---
                        //this.Ichiran.BeginEdit(false);
                        // ---20140116 oonaka delete 例外発生対応 end ---
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    Ichiran.Rows[e.Row.Index].Cells["DAY_CD"].Value = Convert.ToInt16(customTextBoxDayCd.Text);
                    Ichiran.Rows[e.Row.Index].Cells["COURSE_NAME_CD"].Value = customTextBoxCoureseNameCd.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DefaultValuesNeeded", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void customDataGridView_M_COURSE_NIOROSHI_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DataGridView dgv = (DataGridView)sender;
                if (dgv.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    dgv.Rows[e.Row.Index].Cells["M_COURSE_NIOROSHI_DAY_CD"].Value = Convert.ToInt16(customTextBoxDayCd.Text);
                    dgv.Rows[e.Row.Index].Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value = customTextBoxCoureseNameCd.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView_M_COURSE_NIOROSHI_DefaultValuesNeeded", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コース名CDテキストボックスが入力されるときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void customTextBoxCoureseNameCd_Enter(object sender, EventArgs e)
        {
            //var kyotenCdTextBox = ((HeaderForm)((BusinessBaseForm)this.Parent).headerForm).KYOTEN_CD;
            //var kyotenCd = kyotenCdTextBox.Text;
            //if (String.IsNullOrEmpty(kyotenCd))
            //{
            //    var messageShowLogic = new MessageBoxShowLogic();
            //    messageShowLogic.MessageBoxShow("E034", "拠点CD");
            //    kyotenCdTextBox.Focus();
            //}
        }

        /// <summary>
        /// 業者検索ポップアップを表示した？
        /// </summary>
        private bool isGyoushaPopup = false;

        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 業者検索ポップアップ表示前の業者CD
        /// </summary>
        internal string popupBeforeGyoushaCd = String.Empty;
        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        /// <summary>
        /// 業者検索ポップアップを表示した後に処理されます
        /// </summary>
        public void GyoushaPopupAfter()
        {
            this.logic.createRoundNo();
        }

        /// <summary>
        /// 業者検索ポップアップを表示する前に処理されます
        /// </summary>
        public void GyoushaPopupBefore()
        {
            this.isGyoushaPopup = true;
            var rowIndex = this.Ichiran.CurrentRow.Index;
            this.popupBeforeGyoushaCd = this.Ichiran.Rows[rowIndex].Cells["GYOUSHA_CD"].Value.ToString();
        }

        /// <summary>
        /// 現場検索ポップアップを表示した？
        /// </summary>
        private bool isGenbaPopup = false;

        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 現場検索ポップアップ表示前の現場CD
        /// </summary>
        internal string popupBeforeGenbaCd = String.Empty;
        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        /// <summary>
        /// 現場検索ポップアップを表示した後に処理されます
        /// </summary>
        public void GenbaPopupAfter()
        {
            this.logic.createRoundNo();
        }

        /// <summary>
        /// 現場検索ポップアップを表示する前に処理されます
        /// </summary>
        public void GenbaPopupBefore()
        {
            this.isGenbaPopup = true;
            var rowIndex = this.Ichiran.CurrentRow.Index;
            this.popupBeforeGenbaCd = this.Ichiran.Rows[rowIndex].Cells["GENBA_CD"].Value.ToString();
        }

        /// <summary>
        /// 市区町村テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void shikuChousonCdTextBox_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                this.changeGenbaTeikiHinmei();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void gyoushaCdTextBox_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                // 業者CDの前回値チェックをします
                this.logic.GyoushaCdBeforeCheck();

                if (!this.changeGenbaTeikiHinmei())
                {
                    return;
                }

                var gyoushaCd = this.gyoushaCdTextBox.Text;
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    this.genbaCdTextBox.Text = String.Empty;
                    this.genbaNameRyakuTextBox.Text = String.Empty;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場テキストボックスのバリデートが行われるときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void genbaCdTextBox_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 業者CDが未入力の場合は、入力できない
            var gyoushaCd = this.gyoushaCdTextBox.Text;
            var genbaCd = this.genbaCdTextBox.Text;
            if (!String.IsNullOrEmpty(genbaCd) && String.IsNullOrEmpty(gyoushaCd))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "業者");
                this.genbaCdTextBox.Text = string.Empty;
                e.Cancel = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(genbaCd))
                {
                    var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    var mGenba = genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();
                    if (null != mGenba)
                    {
                        this.genbaNameRyakuTextBox.Text = mGenba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "現場");

                        e.Cancel = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場テキストボックスのバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void genbaCdTextBox_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                var genbaCd = this.genbaCdTextBox.Text;
                if (String.IsNullOrEmpty(genbaCd))
                {
                    this.genbaNameRyakuTextBox.Text = String.Empty;
                }

                this.changeGenbaTeikiHinmei();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌Popup表示後処理
        /// </summary>
        public void sharyouPopupAfterExecuteMethod()
        {
            // データソースの更新
            this.logic.sharyouDataSourceUpdate();
        }

        /// <summary>
        /// 車輌ButtonPopup表示後処理
        /// </summary>
        public void sharyouButtonPopupAfterExecuteMethod()
        {
            this.logic.oldSharyouCD = this.SHARYOU_CD.Text;
            // データソースの更新
            this.logic.sharyouDataSourceUpdate();
        }

        /// <summary>
        /// 組み込み回数変更後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KUMIKOMI_ROUND_NO_Validated(object sender, EventArgs e)
        {
            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                // 現場定期品名
                this.changeGenbaTeikiHinmei();
            }
        }

        /// <summary>
        /// 業者CDのポップアップから戻ってきたときに動くメソッドです
        /// </summary>
        public void GyoushaPopupAfterMethod()
        {
            // 業者CDの前回値チェックをします
            this.logic.GyoushaCdBeforeCheck();

            if (!this.changeGenbaTeikiHinmei())
            {
                return;
            }
        }

        #region エンターイベント
        /// <summary>
        /// 業者CDのエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gyoushaCdTextBox_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 市区町村のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shikuChousonCdTextBox_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 現場のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genbaCdTextBox_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 品名のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customTextBoxHinmeiNameCD_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 回数のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KUMIKOMI_ROUND_NO_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }
        #endregion エンターイベント

        /// <summary>
        /// 値をoldValueDicにSetします
        /// </summary>
        /// <param name="sender"></param>
        internal void SetOldValue(object sender)
        {
            var ctl = (Control)sender;
            if (ctl != null)
            {
                // 既に同一Keyを持っていたらValueを上書き
                if (!this.logic.oldValueDic.ContainsKey(ctl.Name))
                {
                    this.logic.oldValueDic.Add(ctl.Name, ctl.Text);
                }
                else
                {
                    this.logic.oldValueDic[ctl.Name] = ctl.Text;
                }

            }
        }

        /// <summary>
        /// 値が変更されたかを判断します
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool IsChangedValue(object sender)
        {
            var ctl = (Control)sender;
            bool ren = false;
            switch (ctl.Name)
            {
                // 市区町村
                case ("shikuChousonCdTextBox"):
                    if (this.logic.oldValueDic["shikuChousonCdTextBox"] != ctl.Text) ren = true;
                    break;
                // 業者
                case ("gyoushaCdTextBox"):
                    if (this.logic.oldValueDic["gyoushaCdTextBox"] != ctl.Text) ren = true;
                    break;
                // 現場
                case ("genbaCdTextBox"):
                    if (this.logic.oldValueDic["genbaCdTextBox"] != ctl.Text) ren = true;
                    break;
                // 品名
                case ("customTextBoxHinmeiNameCD"):
                    if (this.logic.oldValueDic["customTextBoxHinmeiNameCD"] != ctl.Text) ren = true;
                    break;
                // 回数
                case ("KUMIKOMI_ROUND_NO"):
                    if (this.logic.oldValueDic["KUMIKOMI_ROUND_NO"] != ctl.Text) ren = true;
                    break;
                // 運搬業者業者
                case ("UNPAN_GYOUSHA_CD"):
                    if (this.logic.oldValueDic["UNPAN_GYOUSHA_CD"] != ctl.Text) ren = true;
                    break;
                // 出発業者
                case ("SHUPPATSU_GYOUSHA_CD"):
                    if (this.logic.oldValueDic["SHUPPATSU_GYOUSHA_CD"] != ctl.Text) ren = true;
                    break;
                // 出発現場
                case ("SHUPPATSU_GENBA_CD"):
                    if (this.logic.oldValueDic["SHUPPATSU_GENBA_CD"] != ctl.Text) ren = true;
                    break;
            }
            return ren;
        }

        #region 時間項目の入力後チェック

        /// <summary>
        /// 作業開始_時の入力後チェック
        /// 時が空でなく分が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SAGYOU_BEGIN_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_BEGIN_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業開始_分の入力後チェック
        /// 分が空でなく時が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SAGYOU_BEGIN_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_BEGIN_HOUR.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者をクリアしても、車輌の情報もクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.SHARYOU_CD.Text = string.Empty;
                this.SHARYOU_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 作業終了_時の入力後チェック
        /// 時が空でなく分が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SAGYOU_END_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_END_MINUTE.Text))
                {
                    this.SAGYOU_END_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業終了_分の入力後チェック
        /// 分が空でなく時が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SAGYOU_END_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_END_MINUTE.Text))
                {
                    this.SAGYOU_END_HOUR.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 時間項目の入力チェック

        public void RowAdded(int index)
        {
            try
            {
                int rec_id = 0;
                M_COURSE_DETAIL courseDetailEntity = new M_COURSE_DETAIL();
                courseDetailEntity.DAY_CD = SqlInt16.Parse(customTextBoxDayCd.Text);
                courseDetailEntity.COURSE_NAME_CD = customTextBoxCoureseNameCd.Text;
                DataTable tbMaxNo = this.logic.courseDetailDao.GetMaxIdByCd(courseDetailEntity);

                DataTable datatable = GetDataSource();

                if (null == tbMaxNo || 0 == tbMaxNo.Rows.Count)
                {
                    rec_id = 1;
                }
                else
                {
                    rec_id = (int)tbMaxNo.Rows[0]["MAX_REC_ID"] + 1;
                }

                if (datatable != null)
                {
                    rec_id = rec_id + datatable.Rows.Count - 1;
                }

                DataGridViewRow dr = this.Ichiran.Rows[index];

                dr.Cells["DETAIL_BUTTON"].Value = "詳細";
                dr.Cells["DAY_CD"].Value = customTextBoxDayCd.Text;
                dr.Cells["COURSE_NAME_CD"].Value = customTextBoxCoureseNameCd.Text;
                dr.Cells["REC_ID"].Value = rec_id;
                dr.Cells["NewFlag"].Value = "True";

                resetREC_ID();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public DataTable GetDataSource()
        {
            DataTable dt = ((DataTable)this.Ichiran.DataSource);
            dt.Columns["KAISYUUHIN_NAME"].MaxLength = 200;
            DataRow r;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                r = dt.Rows[i];
                if (string.IsNullOrEmpty(Convert.ToString(r["DETAIL_BUTTON"])))
                {
                    dt.Rows.Remove(r);
                }
            }

            DataTable table = this.logic.courseDetailSearchResult.Clone();

            if (this.Ichiran.Rows.Count <= 1)
            {
                return table;
            }
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            table.Columns["KAISYUUHIN_NAME"].MaxLength = 200;
            DataRow dr = table.NewRow();

            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                dr = table.NewRow();

                dr["ROW_NO"] = row.Cells["ROW_NO"].Value;
                dr["ROW_NO2"] = row.Cells["ROW_NO2"].Value;
                dr["ROUND_NO"] = row.Cells["ROUND_NO"].Value;
                dr["GYOUSHA_CD"] = row.Cells["GYOUSHA_CD"].Value;
                dr["GYOUSHA_NAME_RYAKU"] = row.Cells["GYOUSHA_NAME_RYAKU"].Value;
                dr["GENBA_CD"] = row.Cells["GENBA_CD"].Value;
                dr["GENBA_NAME_RYAKU"] = row.Cells["GENBA_NAME_RYAKU"].Value;
                dr["DETAIL_BUTTON"] = row.Cells["DETAIL_BUTTON"].Value;
                dr["KAISYUUHIN_NAME"] = row.Cells["KAISYUUHIN_NAME"].Value;
                dr["BIKOU"] = row.Cells["BIKOU"].Value;
                dr["DAY_CD"] = row.Cells["DAY_CD"].Value;
                dr["COURSE_NAME_CD"] = row.Cells["COURSE_NAME_CD"].Value;
                dr["REC_ID"] = row.Cells["REC_ID"].Value;
                dr["KIBOU_TIME"] = row.Cells["KIBOU_TIME"].Value;
                dr["SAGYOU_TIME_MINUTE"] = row.Cells["SAGYOU_TIME_MINUTE"].Value;
                dr["TIME_STAMP"] = row.Cells["TIME_STAMP"].Value;
                dr["NewFlag"] = row.Cells["NewFlag"].Value;
                table.Rows.Add(dr);
            }
            this.logic.courseDetailSearchResult = table;
            return table;
        }

        /// <summary>
        /// 運搬業者CD検索ポップアップ前の処理を実施
        /// </summary>
        public void PopupBeforeUnpanGyoushaCode()
        {
            // 既に同一Keyを持っていたらValueを上書き
            if (!this.logic.oldValueDic.ContainsKey("UNPAN_GYOUSHA_CD"))
            {
                this.logic.oldValueDic.Add("UNPAN_GYOUSHA_CD", this.UNPAN_GYOUSHA_CD.Text);
            }
            else
            {
                this.logic.oldValueDic["UNPAN_GYOUSHA_CD"] = this.UNPAN_GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 運搬業者CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterUnpanGyoushaCode()
        {
            this.logic.unpanGyoushaCDValidatedProc(this.UNPAN_GYOUSHA_CD.Text);
        }

        /// <summary>
        /// 業者CD検索ポップアップ前の処理を実施
        /// </summary>
        public void GyoushaPopupBeforeMethod()
        {
            // 既に同一Keyを持っていたらValueを上書き
            if (!this.logic.oldValueDic.ContainsKey("gyoushaCdTextBox"))
            {
                this.logic.oldValueDic.Add("gyoushaCdTextBox", this.gyoushaCdTextBox.Text);
            }
            else
            {
                this.logic.oldValueDic["gyoushaCdTextBox"] = this.gyoushaCdTextBox.Text;
            }
        }

        /// <summary>
        /// 市区町村のポップアップから戻ってきたときに動くメソッドです
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void ShikuchousonPopupAfterExecuteMethod()
        {
            this.changeGenbaTeikiHinmei();
        }

        /// <summary>
        /// 現場のポップアップから戻ってきたときに動くメソッドです
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void GenbaPopupAfterExecuteMethod()
        {
            this.changeGenbaTeikiHinmei();
        }

        #region NAVITIME追加項目

        /// <summary>
        /// 出発業者CDのポップアップから戻ってきたときに動くメソッドです
        /// </summary>
        public void ShuppatsuGyoushaPopupAfterMethod()
        {
            // 出発業者CDの前回値チェックをします
            this.logic.ShuppatsuGyoushaCdBeforeCheck();
        }

        /// <summary>
        /// 出発業者CDのエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 出発現場CDのエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.SetOldValue(sender);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                var genbaCd = this.SHUPPATSU_GENBA_CD.Text;
                if (String.IsNullOrEmpty(genbaCd))
                {
                    this.SHUPPATSU_GENBA_NAME.Text = String.Empty;
                }
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 業者CDが未入力の場合は、入力できない
            var gyoushaCd = this.SHUPPATSU_GYOUSHA_CD.Text;
            var genbaCd = this.SHUPPATSU_GENBA_CD.Text;
            if (!String.IsNullOrEmpty(genbaCd) && String.IsNullOrEmpty(gyoushaCd))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "出発業者");
                this.genbaCdTextBox.Text = string.Empty;
                e.Cancel = true;
            }
            else
            {
                if (!String.IsNullOrEmpty(genbaCd))
                {
                    var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                    var mGenba = genbaDao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();
                    if (null != mGenba)
                    {
                        this.SHUPPATSU_GENBA_NAME.Text = mGenba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "出発現場");

                        e.Cancel = true;
                    }
                }
                this.logic.shuppatsuDataSourceUpdate();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 値に変更があったかを判断します
            if (this.IsChangedValue(sender))
            {
                // 業者CDの前回値チェックをします
                this.logic.ShuppatsuGyoushaCdBeforeCheck();

                var gyoushaCd = this.SHUPPATSU_GYOUSHA_CD.Text;
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    this.SHUPPATSU_GENBA_CD.Text = String.Empty;
                    this.SHUPPATSU_GENBA_NAME.Text = String.Empty;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion
    }
}
