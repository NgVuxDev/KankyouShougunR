using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using System.ComponentModel;

namespace Shougun.Core.Stock.ZaikoTyouseiNyuuryoku
{
    /// <summary>
    /// G631 入金入力 画面クラス
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// ロジッククラス
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 業者CDの前回値
        /// </summary>
        internal string beforeGyoushaCd;

        /// <summary>
        /// 業者名の前回値
        /// </summary>
        internal string beforeGyoushaName;

        /// <summary>
        /// 現場CDの前回値
        /// </summary>
        internal string beforeGenbaCd;

        /// <summary>
        /// 現場名の前回値
        /// </summary>
        internal string beforeGenbaName;

        /// <summary>
        /// 在庫品名CDの前回値
        /// </summary>
        internal string beforeZaikoHinmeiCd;

        /// <summary>
        /// 在庫品名CDの前回値
        /// </summary>
        internal string beforeZaikoHinmeiName;

        /// <summary>
        /// 明細在庫品名CDの前回値
        /// </summary>
        internal string beforeMesaiEditZaikoHinmeiCd;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isError;

        /// <summary>
        /// 入力エラーの状態を保持
        /// </summary>
        private bool IsInputErrorOccured;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region プロパティ
        /// <summary>
        /// データ移動モードで指定された入金先CDを取得・設定します
        /// </summary>
        internal string tyouseiNumber { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType) : this(headerForm, windowType, string.Empty) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面区分</param>
        /// <param name="nyuukinNumber">入金番号</param>
        /// <param name="seq">入金一括入力SEQ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType, string tyouseNum)
            : base(WINDOW_ID.T_ZAIKO_TYOUSEI_NYUURYOKU, windowType)
        {
            this.InitializeComponent();

            this.headerForm = headerForm;
            this.tyouseiNumber = tyouseNum;
            this.logic = new LogicClass(this.headerForm, this);
        }

        #endregion

        #region ボタン押下時メソッド
        /// <summary>
        /// [F2]新規ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var formID = FormManager.GetFormID(Assembly.GetExecutingAssembly());
            if (Manager.CheckAuthority(formID, WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.tyouseiNumber = "";
                this.TYOUSEI_NUMBER.Text = "";
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                if (!this.logic.WindowInit()) { return; }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F7]一覧ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G168", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F9]登録ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Regist();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F11]行削除ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.RowRemove();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名読込ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.zaikoMeiYomikomi();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 前ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void PrevButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Prev();
            this.logic.SetTyouseiRyouGoukei();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 次ボタンをクリックしたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public void NextButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.Next();
            this.logic.SetTyouseiRyouGoukei();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント
        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            // オールコントロールにヘッダーのコントロールも含める
            this.allControl = this.allControl.Concat(this.headerForm.allControl).ToArray();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (!this.logic.WindowInit()) { return; }

            if (this.DETAIL_Ichiran != null)
            {
                this.DETAIL_Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }
            if (this.label4 != null)
            {
                this.label4.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }
            if (this.label5 != null)
            {
                this.label5.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }
            if (this.TYOUSEI_BEFORE_GOUKEI != null)
            {
                this.TYOUSEI_BEFORE_GOUKEI.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }
            if (this.TYOUSEI_AFTER_GOUKEI != null)
            {
                this.TYOUSEI_AFTER_GOUKEI.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            }

            LogUtility.DebugMethodEnd();
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
        /// 調整番号のValidated実行時に処理します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TYOUSEI_NUMBER_Validated(object sender, EventArgs e)
        {
            this.logic.TYOUSEI_NUMBER_Validated();
        }

        /// <summary>
        /// 業者CDのCellEnter実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.beforeGyoushaCd = this.GYOUSHA_CD.Text;
                this.beforeGyoushaName = this.GYOUSHA_NAME.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.GYOUSHA_CD_Validated();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのCellEnter実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.beforeGenbaCd = this.GENBA_CD.Text;
                this.beforeGenbaName = this.GENBA_NAME.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.GENBA_CD_Validated();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDのCellEnter実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ZAIKO_HINMEI_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.IsInputErrorOccured && !this.isError)
            {
                // 在庫品名CDが入力エラーで無い場合、現在の入力値を変更前在庫品名CDとする
                this.beforeZaikoHinmeiCd = this.ZAIKO_HINMEI_CD.Text;
                this.beforeZaikoHinmeiName = this.ZAIKO_HINMEI_NAME.Text;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDのCellValidated実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void ZAIKO_HINMEI_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.ZAIKO_HINMEI_CD_Validating() == -1)
            {
                e.Cancel = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 移動一覧のCellValidating実行時に処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void DETAIL_Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.DETAIL_Ichiran_CellValidating(sender, e);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 業者CDポップアップを開け前に処理します
        /// </summary>
        public void GyoushaPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeGyoushaCd = this.GYOUSHA_CD.Text;
            this.beforeGyoushaName = this.GYOUSHA_NAME.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者CDポップアップを閉じた後に処理します
        /// </summary>
        public void GyoushaPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.logic.GYOUSHA_CD_Validated();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDポップアップを開け前に処理します
        /// </summary>
        public void GenbaPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeGenbaCd = this.GENBA_CD.Text;
            this.beforeGenbaName = this.GENBA_NAME.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CDポップアップを閉じた後に処理します
        /// </summary>
        public void GenbaPopupAfter()
        {
            LogUtility.DebugMethodStart();
            this.logic.GENBA_CD_Validated();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDポップアップを開け前に処理します
        /// </summary>
        public void ZaikoHinmeiPopupBefore()
        {
            LogUtility.DebugMethodStart();
            this.beforeZaikoHinmeiCd = this.ZAIKO_HINMEI_CD.Text;
            this.beforeZaikoHinmeiName = this.ZAIKO_HINMEI_NAME.Text;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 在庫品名CDポップアップを閉じた後に処理します
        /// </summary>
        public void ZaikoHinmeiPopupAfter()
        {
            LogUtility.DebugMethodStart();
            if (this.logic.ZAIKO_HINMEI_CD_Validating() == -1)
            {
                return;
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// セルインテル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.DETAIL_Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.OwningColumn.Name.Equals("MEISAI_ZAIKO_HINMEI_CD"))
            {
                this.beforeMesaiEditZaikoHinmeiCd = Convert.ToString(cell.Value);
                this.DETAIL_Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
            if (cell.OwningColumn.Name.Equals("MEISAI_TYOUSEI_RYOU"))
            {
                this.DETAIL_Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
        }
    }
}
