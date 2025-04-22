using System;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using r_framework.Utility;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    public partial class UIForm : SuperForm
    {
        /// <summary>画面ロジック</summary>
        //private ManifestCheckHyo.LogicClass logic = null;
        private LogicClass logic;

        public DataTable table = new DataTable();
        public string taisyo = string.Empty;

        public UIForm(WINDOW_ID windowID, WINDOW_TYPE window_type)
            : base(windowID, window_type)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        public UIForm(WINDOW_ID windowID, WINDOW_TYPE window_type, DataTable dt, string taisyo)
            : base(windowID, window_type)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.table = dt;            // データテーブル
            this.taisyo = taisyo;       // 対象データ　1:マニフェスト、2:マスタ

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }


        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary> 画面読み込み処理 </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!logic.WindowInit()) { return; }

            // 20140623 ria EV004852 一覧と抽出条件の変更 start
            // 一覧
            this.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);
            // 20140623 ria EV004852 一覧と抽出条件の変更 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

        /// <summary>Ｆ5キー（印刷）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            this.logic.Func5();
        }
        /// <summary>Ｆ6キー（CSV出力）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc6_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvExp = new CSVExport();
            csvExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, "マニフェストチェック表", this);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>Ｆ8キー（登録）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            //this.logic.Func8();
            // 範囲条件指定画面表示
            // 引数に条件を格納した変数を渡す想定
            this.logic.ShowPopUp(this.logic.JoukenParam);
        }

        /// <summary>Ｆ12キー（登録）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        // 20140623 ria EV004852 一覧と抽出条件の変更 start
        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        #endregion

        /// <summary>Ｆ3キー（修正）ボタンが押された場合の処理</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc3_Clicked(object sender, EventArgs e)
        {
            this.logic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
        }

        ///// <summary>Ｆ10キー（並び替え）ボタンが押された場合の処理</summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void ButtonFunc10_Clicked(object sender, EventArgs e)
        //{
        //}

        /// <summary>
        /// 一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.logic.FormChanges(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }
        // 20140623 ria EV004852 一覧と抽出条件の変更 end
        
        private void UIForm_Shown(object sender, EventArgs e)
        {
            this.Search(null, e);
        }

        /// <summary>
        /// 範囲条件指定
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Search(object sender, EventArgs e)
        {
            // 範囲条件指定画面表示
            // 引数に条件を格納した変数を渡す想定
            this.logic.ShowPopUp(this.logic.JoukenParam);
        }
    }
}
