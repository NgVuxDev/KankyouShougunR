using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ElectronicManifest.UkewatashiKakuninHyou.Logic;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Data;
using r_framework.Utility;
namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran
{
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>

        private ManifestSyuuryoubiIchiran.LogicClass MILogic = null;
        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }
        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;
        public string fromKbn = "";

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        //初期時一覧の明細列
        private string[] strColumns = { "SYSTEM_ID", "SEQ", "HAIKI_KBN_CD", "DENMANI_KANRI_ID", "DENMANI_SEQ", "EDI_PASSWORD", "KIND", "廃棄物区分", "一次二次", "終了日警告区分", "経過日数", "交付番号", "排出事業者名", "排出事業場名", "運搬受託者名", "処分受託社名", "最終処分場所名", "廃棄物種類", "数量", "単位" };

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.T_MANIFEST_SHUURYOUBI_KEIKOKU_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        public UIHeader HeaderForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.isLoaded)
            {

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 0);
                this.customSearchHeader1.Size = new System.Drawing.Size(990, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 25);
                this.customSortHeader1.Size = new System.Drawing.Size(990, 26);

                //this.customDataGridView1.Location = new System.Drawing.Point(3, 52);
                this.customDataGridView1.Location = new System.Drawing.Point(3, 52);
                this.customDataGridView1.Size = new System.Drawing.Size(990, 400);

                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                // 汎用検索は一旦廃止
                this.searchString.Visible = false;

                // ソート条件の初期化
                this.customSortHeader1.ClearCustomSortSetting();

                // フィルタの初期化
                this.customSearchHeader1.ClearCustomSearchSetting();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    // この画面ではWindowInit内のSearchに入る前にAnchorを固定する必要がある
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }

                // 初期化、初期表示
                this.MILogic.WindowInit();
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
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MILogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 印刷ボタンを押下したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.MILogic.Print();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            string title = "マニフェスト終了日警告一覧";
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, title, this);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.MILogic.Search() == 0)
            {
                this.messageShowLogic.MessageBoxShow("C001");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MILogic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// 一覧初期列名
        /// </summary>
        public void SetInitCol()
        {
            // 20140715 katen start‏
            this.logic.AlertCount = this.MILogic.alertCount;
            // 20140715 katen end‏
            if (this.MILogic.SearchResult == null || this.MILogic.SearchResult.Rows.Count <= 0)
            {
                this.MILogic.SearchResult = new DataTable();

                foreach (string s in this.strColumns)
                {
                    this.MILogic.SearchResult.Columns.Add(s);
                }
                this.customSearchHeader1.SearchDataTable(this.MILogic.SearchResult);
                this.logic.CreateDataGridView(this.MILogic.SearchResult);
            }
            else
            {
                this.customSearchHeader1.SearchDataTable(this.MILogic.SearchResult);
                this.logic.CreateDataGridView(this.MILogic.SearchResult);
                if (this.customDataGridView1.RowCount > 0)
                {
                    this.customDataGridView1.CurrentCell = this.customDataGridView1[4, 0];
                }
                this.customDataGridView1.Refresh();

            }

            if (0 < this.customDataGridView1.Columns.Count)
            {
                //列を隠す
                this.customDataGridView1.Columns["SYSTEM_ID"].Visible = false;
                this.customDataGridView1.Columns["SEQ"].Visible = false;
                this.customDataGridView1.Columns["HAIKI_KBN_CD"].Visible = false;
                this.customDataGridView1.Columns["DENMANI_KANRI_ID"].Visible = false;
                this.customDataGridView1.Columns["DENMANI_SEQ"].Visible = false;
                this.customDataGridView1.Columns["EDI_PASSWORD"].Visible = false;
                this.customDataGridView1.Columns["KIND"].Visible = false;
            }
        }

        // 20140715 katen start‏
        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }
        // 20140715 katen end‏

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.customDataGridView1 != null)
            {
                this.HeaderForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
        }
    }
}
