using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.Manifestsuiihyo
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary> 親フォーム </summary>
        //public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public UIForm(WINDOW_ID windowID, WINDOW_TYPE window_type)
            : base(windowID, window_type)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 初期化
            logic.WindowInit();
        }

        /// <summary>
        /// Ｆ5キー（印刷）ボタンが押された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc5_Clicked(object sender, EventArgs e)
        {
            if (this.customDataGridView1.RowCount == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // 出力する該当データがありません。
                msgLogic.MessageBoxShow("E044");

                return;
            }
            this.logic.Func5();
        }

        /// <summary>
        /// Ｆ6キー（CSV出力）ボタンが押された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc6_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.customDataGridView1.RowCount > 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // CSV出力しますか？
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExp = new CSVExport();
                    csvExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, "マニフェスト推移表", this);
                }
            }
            else
            {
                MessageBox.Show("対象データが無い為、出力を中止しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// Ｆ8キー（検索）ボタンが押された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            this.logic.ShowPopUp(null);
        }

        /// <summary>
        /// Ｆ12キー（閉じる）ボタンが押された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = null;
            this.Close();
            parentForm.Close();
        }

        /// <summary> UIForm_Shown </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 範囲条件指定画面表示
            // 引数に条件を格納した変数を渡す想定
            this.logic.ShowPopUp(this.logic.JoukenParam);
        }
    }
}
