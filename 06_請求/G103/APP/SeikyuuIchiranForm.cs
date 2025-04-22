using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Shougun.Core.Billing.Seikyuichiran.CustomControls_Ex;
using Shougun.Core.Common.IchiranCommon.APP;


namespace Shougun.Core.Billing.Seikyuichiran
{
    public partial class SeikyuuIchiranForm : IchiranSuperForm
    {

        #region フィールド

        private Seikyuichiran.LogicClass Logic;

        UIHeader header_new;

        private Boolean isLoaded;

        /// <summary>
        /// 検索後のアラートを表示するかのフラグを取得・設定します
        /// </summary>
        public Boolean IsNoneAlert { get; set; }

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        internal string transactionId;
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        #endregion

        public SeikyuuIchiranForm(DENSHU_KBN SEIKYUU_ICHIRAN, UIHeader headerFor)
            : base(SEIKYUU_ICHIRAN, false)
        {
            this.InitializeComponent();

            this.header_new = headerFor;

            this.ShainCd = SystemProperty.Shain.CD;    //社員コード

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            Logic.SetHeader(header_new);

            isLoaded = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="SEIKYUU_ICHIRAN">伝種区分</param>
        /// <param name="headerFor">ヘッダフォーム</param>
        /// <param name="dto">初期表示情報</param>
        public SeikyuuIchiranForm(DENSHU_KBN SEIKYUU_ICHIRAN, UIHeader headerFor, DTOClass dto)
            : base(SEIKYUU_ICHIRAN, false)
        {
            this.InitializeComponent();

            this.header_new = headerFor;

            this.ShainCd = SystemProperty.Shain.CD;    //社員コード

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            Logic.SetHeader(header_new);

            this.Logic.InitDto = dto;

            isLoaded = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.transactionId = Guid.NewGuid().ToString();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            this.customDataGridView1.IsBrowsePurpose = false;//160015
            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.Logic.WindowInit())
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

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            this.Logic.HeaderCheckBoxSupport();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            //PhuocLoc 2021/05/14 #148574 -Start
            this.customSearchHeader1.TabStop = false;
            this.Logic.HeaderCheckBoxSupportMod();
            //PhuocLoc 2021/05/14 #148574 -End

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header_new.ReadDataNumber.Text = "0";
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

        public void setLogicSelect()
        {
            this.Logic.selectQuery = this.logic.SelectQeury;
            this.Logic.orderByQuery = this.logic.OrderByQuery;
            this.Logic.joinQuery = this.logic.JoinQuery;
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
            this.Logic.bt_func8_Click(null, EventArgs.Empty);
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

        #region 検索結果表示

        public virtual void ShowData()
        {
            this.Table = this.Logic.SearchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);

                bool isExistColumnCheckBox = false;

                foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
                {
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    //if (column.Name.Equals("必須請求番号") || column.Name.Equals("必須取引先CD"))
                    if (column.Name.Equals("必須請求番号")
                        || column.Name.Equals("必須取引先CD")
                        || column.Name.Equals("UPLOAD_STATUS")
                        || column.Name.Equals("DOWNLOAD_STATUS")
                        || column.Name.Equals("INXS_SEIKYUU_KBN")
                        //160015 S
                        || column.Name.Equals("NYUUKIN_KOMI_GAKU")
                        || column.Name.Equals("TIMESTAMP")
                        || column.Name.Equals("SEIKYUU_DATE"))
                        //160015 E
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
                    
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
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                if (Logic.isInxsSeikyuusho)
                {
                    var col = this.customDataGridView1.Columns["CHECKBOX"];
                    DataGridViewCheckBoxHeaderCell headerCell = (DataGridViewCheckBoxHeaderCell)col.HeaderCell;
                    if (headerCell != null)
                    {
                        headerCell._checked = false;
                    }
                    foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                    {
                        string seikyuuNumber = dgvRow.Cells["必須請求番号"].Value.ToString();
                        string inxsSeikyuuKbn = dgvRow.Cells["INXS_SEIKYUU_KBN"].Value.ToString();
                        string uploadStatus = dgvRow.Cells["UPLOAD_STATUS"].Value.ToString();
                        if (inxsSeikyuuKbn == "2")
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
                        if (this.Logic.selectedSeikyuuNumber != null)
                        {
                            if (this.Logic.selectedSeikyuuNumber.Contains(seikyuuNumber))
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
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            }
        }

        #endregion

        //PhuocLoc 2021/05/14 #148574 -Start
        public override void AdjustColumnSizeComplete()
        {
            DataGridViewColumn col = this.customDataGridView1.Columns["CHECKBOX_DEL"];
            col.Width = 42;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //160015 S
            if (this.customDataGridView1.Columns.Contains("入金予定日(変更後)"))
            {
                col = this.customDataGridView1.Columns["入金予定日(変更後)"];
                col.Width = 120;
            }
            //160015 E
        }
        //PhuocLoc 2021/05/14 #148574 -End
    }
}
