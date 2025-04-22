using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Shougun.Core.Adjustment.Shiharaiichiran.CustomControls_Ex;
using Shougun.Core.Common.IchiranCommon.APP;


namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    /// <summary>
    /// Formクラス
    /// </summary>
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジッククラス
        /// </summary>
        private LogicClass logicShiharaiichiran;

        /// <summary>
        /// 起動状態
        /// </summary>
        private Boolean isLoaded = false;

        /// <summary>
        /// 検索後のアラートを表示するかのフラグを取得・設定します
        /// </summary>
        public Boolean IsNoneAlert { get; set; }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        internal string transactionId;
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        /// <param name="header"></param>
        public UIForm(DENSHU_KBN denshuKbn, UIHeader header)
            : base(denshuKbn, false)
        {
            this.InitializeComponent();

            this.ShainCd = SystemProperty.Shain.CD;    //社員コード

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logicShiharaiichiran = new LogicClass(this);

            logicShiharaiichiran.SetHeader(header);

            isLoaded = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <param name="header">ヘッダフォーム</param>
        /// <param name="dto">初期表示情報</param>
        public UIForm(DENSHU_KBN denshuKbn, UIHeader header, DTOClass dto)
            : base(denshuKbn, false)
        {
            this.InitializeComponent();

            this.ShainCd = SystemProperty.Shain.CD;    //社員コード

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logicShiharaiichiran = new LogicClass(this);

            logicShiharaiichiran.SetHeader(header);

            this.logicShiharaiichiran.InitDto = dto;

            isLoaded = false;

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

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.transactionId = Guid.NewGuid().ToString();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            this.customDataGridView1.IsBrowsePurpose = false;//160020
            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.logicShiharaiichiran.WindowInit())
                {
                    return;
                }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 91);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 113);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 140);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 290);
            }

            this.PatternReload();

            this.setLogicSelect();
            isLoaded = true;
            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.logicShiharaiichiran.HeaderCheckBoxSupport();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            //PhuocLoc 2021/05/14 #148575 -Start
            this.customSearchHeader1.TabStop = false;
            this.logicShiharaiichiran.HeaderCheckBoxSupportMod();
            //PhuocLoc 2021/05/14 #148575 -End

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定            
            if (this.customDataGridView1 != null)
            {
                this.logicShiharaiichiran.headform.txtReadDataCnt.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.logicShiharaiichiran.headform.txtReadDataCnt.Text = "0";
            }
            //thongh 2015/10/16 #13526 end

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
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        #region メソッド

        /// <summary>
        /// IchiranSuperFormで取得されたパターン一覧のSelectQeuryをセット
        /// </summary>
        public void setLogicSelect()
        {
            this.logicShiharaiichiran.selectQuery = this.logic.SelectQeury;
            this.logicShiharaiichiran.orderByQuery = this.logic.OrderByQuery;
            this.logicShiharaiichiran.joinQuery = this.logic.JoinQuery;
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        public void UpdateDataGridView()
        {
            this.UpdateDataGridView(false);
        }

        /// <summary>
        /// フォーム更新処理
        /// </summary>
        /// <param name="isNoneAlert">アラートを表示させない場合は、true</param>
        public void UpdateDataGridView(bool isNoneAlert)
        {
            this.IsNoneAlert = isNoneAlert;
            this.logicShiharaiichiran.bt_func8_Click(null, EventArgs.Empty);
        }

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            this.Table = this.logicShiharaiichiran.SearchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);

                bool isExistColumnCheckBox = false;

                foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
                {
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    //if (column.Name.Equals("必須精算番号") || column.Name.Equals("必須取引先CD"))
                    if (column.Name.Equals("必須精算番号")
                        || column.Name.Equals("必須取引先CD")
                        || column.Name.Equals("UPLOAD_STATUS")
                        || column.Name.Equals("DOWNLOAD_STATUS")
                        //160020 S
                        || column.Name.Equals("SHUKKIN_KOMI_GAKU")
                        || column.Name.Equals("TIMESTAMP")
                        || column.Name.Equals("SHIHARAI_DATE"))
                        //160020 E
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
                    {
                        column.Visible = false;
                    }

                    if (column.Name.Equals("CHECKBOX_DEL"))
                    {
                        isExistColumnCheckBox = true;
                    }
                }

                if (isExistColumnCheckBox)
                {
                    foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                    {
                        this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX_DEL"].ReadOnly = false;
                    }
                }

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                if (logicShiharaiichiran.isInxsShiharaiUpload)
                {
                    var col = this.customDataGridView1.Columns["CHECKBOX"];
                    DataGridViewCheckBoxHeaderCell headerCell = (DataGridViewCheckBoxHeaderCell)col.HeaderCell;
                    if (headerCell != null)
                    {
                        headerCell._checked = false;
                    }
                    foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                    {
                        string seisanNumber = dgvRow.Cells["必須精算番号"].Value.ToString();
                        string inxsShiharaiKbn = dgvRow.Cells["INXS_SHIHARAI_KBN"].Value.ToString();
                        string uploadStatus = dgvRow.Cells["UPLOAD_STATUS"].Value.ToString();
                        if (inxsShiharaiKbn == "2")
                        {
                            this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX"].ReadOnly = true;
                        }
                        else
                        {
                            if (uploadStatus.Equals("2"))
                            {
                                this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX"].ReadOnly = false;
                            }
                            else
                            {
                                this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX"].ReadOnly = true;
                            }
                        }
                        if (this.logicShiharaiichiran.selectedSeisanNumber != null)
                        {
                            if (this.logicShiharaiichiran.selectedSeisanNumber.Contains(seisanNumber))
                            {
                                this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX"].Value = true;
                            }
                            else
                            {
                                this.customDataGridView1.Rows[dgvRow.Index].Cells["CHECKBOX"].Value = false;
                            }
                        }
                    }
                }
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            }
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

        #endregion

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        private void dtpDateFrom_Leave(object sender, EventArgs e)
        {
            this.dtpDateTo.IsInputErrorOccured = false;
            this.dtpDateTo.BackColor = Constans.NOMAL_COLOR;
        }

        private void dtpDateTo_Leave(object sender, EventArgs e)
        {
            this.dtpDateFrom.IsInputErrorOccured = false;
            this.dtpDateFrom.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        //PhuocLoc 2021/05/14 #148575 -Start
        public override void AdjustColumnSizeComplete()
        {
            DataGridViewColumn col = this.customDataGridView1.Columns["CHECKBOX_DEL"];
            col.Width = 42;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //160020 S
            if (this.customDataGridView1.Columns.Contains("出金予定日(変更後)"))
            {
                col = this.customDataGridView1.Columns["出金予定日(変更後)"];
                col.Width = 120;
            }
            //160020 E
        }
        //PhuocLoc 2021/05/14 #148575 -End
    }
}
