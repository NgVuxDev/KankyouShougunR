using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Dto;
using r_framework.Logic;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Master.CourseIchiran
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass courseLogic;

        private UIHeader header;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        public string beforeCd;

        public string popupCd;

        public bool isError;

        private bool isLoaded = false;

        #endregion

        #region コンストラクタ

        public UIForm()
            : base(DENSHU_KBN.M_COURSE_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.courseLogic = new LogicClass(this);

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

        }

        public UIForm(DTOClass dto)
            : base(DENSHU_KBN.M_COURSE_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.courseLogic = new LogicClass(this);

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            this.courseLogic.dto = dto;
        }

        #endregion

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            if (!isLoaded)
            {
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                this.courseLogic.parentForm = this.ParentBaseForm;

                //ヘッダーの初期化
                this.header = (UIHeader)this.ParentBaseForm.headerForm;

                // 一覧
                this.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_CellMouseDoubleClick);

                this.courseLogic.WindowInit();

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(0, 140);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(0, 163);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(0, 190);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 250);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();
            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.header.SEARCH_CNT.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header.SEARCH_CNT.Text = "0";
            }
        }

        /// <summary>
        /// 表出イベント（初回のみ）
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.courseLogic.dto != null)
            {
                this.courseLogic.Search();
            }
        }

        #endregion

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            this.courseLogic.Shuusei();
        }

        /// <summary>
        /// 複写(F5)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public virtual void bt_func5_Click(object sender, EventArgs e)
        {
            this.courseLogic.Fukusha();
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_COURSE_ICHIRAN), this);
        }

        /// <summary>
        /// 条件ｸﾘｱ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.courseLogic.ClearScreen();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            this.courseLogic.Search();
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
            //読込データ件数
            if (this.customDataGridView1 != null)
            {
                this.header.SEARCH_CNT.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header.SEARCH_CNT.Text = "0";
            }
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            Properties.Settings.Default.COURSE_NAME_CD = this.COURSE_NAME_CD.Text;
            Properties.Settings.Default.GYOUSHA_CD = this.GYOUSHA_CD.Text;
            Properties.Settings.Default.GENBA_CD = this.GENBA_CD.Text;
            Properties.Settings.Default.HINMEI_CD = this.HINMEI_CD.Text;
            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        public void bt_process1_Click(object sender, System.EventArgs e)
        {
            var sysID = this.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.SetPatternBySysId(sysID);
                this.courseLogic.searchResult = this.Table;
                this.ShowData();
            }
        }

        /// <summary>
        /// 地図を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process2_Click(object sender, EventArgs e)
        {
            try
            {
                // 件数チェック
                if (!this.courseLogic.CheckForCheckBox())
                {
                    return;
                }

                if (this.courseLogic.msgLogic.MessageBoxShowConfirm("地図を表示しますか？" +
                    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.courseLogic.createMapboxDto();
                if (dtos.Count == 0)
                {
                    this.courseLogic.msgLogic.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_COURSE_ICHIRAN);
            }
            catch
            {
                throw;
            }
        }


        #region 検索結果表示
        public virtual void ShowData()
        {
            this.Table = this.courseLogic.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                this.courseLogic.HideColumnHeader();
                this.courseLogic.notReadOnlyColumns();
                this.header.SEARCH_CNT.Text = this.Table.Rows.Count.ToString();
            }
        }
        #endregion

        #region 画面イベント

        internal void COURSE_NAME_CD_Validated(object sender, EventArgs e)
        {
            this.courseLogic.COURSE_NAME_CD_Validated();
        }

        internal void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.courseLogic.GYOUSHA_CD_Validated();
        }

        internal void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.courseLogic.GENBA_CD_Validated();
        }

        internal void HINMEI_CD_Validated(object sender, EventArgs e)
        {
            this.courseLogic.HINMEI_CD_Validated();
        }

        internal void COURSE_NAME_CD_Enter(object sender, EventArgs e)
        {
            this.beforeCd = this.COURSE_NAME_CD.Text;
        }

        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeCd = this.GYOUSHA_CD.Text;
        }

        internal void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeCd = this.GENBA_CD.Text;
        }

        internal void HINMEI_CD_Enter(object sender, EventArgs e)
        {
            this.beforeCd = this.HINMEI_CD.Text;
        }

        public void GyoushaPopupBefore()
        {
            this.popupCd = this.GYOUSHA_CD.Text;
            this.beforeCd = this.GYOUSHA_CD.Text;
        }

        public void GyoushaPopupAfter()
        {
            if (this.popupCd != this.GYOUSHA_CD.Text)
            {
                this.courseLogic.GYOUSHA_CD_Validated();
            }
        }

        public void customDataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCellMouseEventArgs datagridviewcell = (DataGridViewCellMouseEventArgs)e;
            if (datagridviewcell.RowIndex >= 0)
            {
                this.courseLogic.Shuusei();
            }
        }

        #endregion

        /// <summary>
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "表示現場");
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// 複写曜日変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAY_CD_FUKUSHA_TextChanged(object sender, EventArgs e)
        {
            this.courseLogic.SetFukushaDay();
        }

        /// <summary>
        /// 複写曜日チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COURSE_NAME_CD_FUKUSHA_Enter(object sender, EventArgs e)
        {
            this.courseLogic.CheckFukushaDay();
        }

        /// <summary>
        /// コースチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COURSE_NAME_CD_FUKUSHA_Validated(object sender, EventArgs e)
        {
            this.courseLogic.CheckCourseCd();
        }
    }
}