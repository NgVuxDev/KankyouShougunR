// $Id: UIForm.cs 56232 2015-07-21 06:20:31Z j-kikuchi $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Logic;

namespace Shougun.Core.Master.DenManiKansanHoshu
{
    /// <summary>
    /// 電マニ換算値入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region - Field -
        /// <summary>画面Logic</summary>
        private LogicClass logic = null;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #endregion - Field -

        #region - Constructor -
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // 電マニ換算値入力ロジック
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion - Constructor -

        #region - FunctionKeyEvent -
        /// <summary>
        /// FunctionKey押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void functionKeyClick(object sender, EventArgs e)
        {
            // イベント振分
            var ctrl = (CustomButton)sender;
            switch(ctrl.Name)
            {
                case "bt_func4":
                    // 削除
                    this.logic.delete();
                    break;
                case "bt_func6":
                    // CSV出力
                    this.logic.csvOutput();
                    break;
                case "bt_func7":
                    // 条件クリア
                    this.logic.clearCondition();
                    break;
                case "bt_func8":
                    // 検索
                    this.logic.search();
                    break;
                case "bt_func9":
                    // 登録
                    this.logic.regist();
                    break;
                case "bt_func11":
                    // 取消
                    this.logic.reLoad();
                    break;
                case "bt_func12":
                    // 画面Close
                    this.logic.closeDisp();
                    break;
                case "bt_process1":
                    // 細分類読込
                    this.logic.saibunruiLoad();
                    break;
                default:
                    // DO NOTHING
                    break;
            }
        }

        #endregion - FunctionKeyEvent -

        #region - DetailEvent -
        /// <summary>
        /// 一覧RowValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 一覧RowValidating処理
            this.logic.ichiranRowValidatingProc(e);
        }

        /// <summary>
        /// 一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 一覧CellEnter処理
            this.logic.ichiranCellEnterProc(e);
        }

        /// <summary>
        /// 一覧CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // 一覧CellValidating処理
            this.logic.ichiranCellValidatingProc(e);
        }

        /// <summary>
        /// 編集する場合、IMEモードをセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CustomDataGridView gc = sender as CustomDataGridView;
            DataGridViewCell cell = gc.CurrentCell;

            if (cell.OwningColumn.Name == "clmMANIFEST_KANSAN_BIKOU")
            {
                e.Control.ImeMode = ImeMode.Hiragana;
            }
            else
            {
                e.Control.ImeMode = ImeMode.Disable;
            }

        }

        /// <summary>
        /// 一覧CellFormattingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 一覧CellFormatting処理
            this.logic.ichiranCellFormattingProc(e);
        }

        /// <summary>
        /// 一覧CurrentCellDirtyStateChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // 一覧CurrentCellDirtyStateChanged処理
            this.logic.ichiranCurrentCellDirtyStateChanged();
        }

        #endregion - DetailEvent -

        #region - ControlEvent -
        /// <summary>
        /// 加入者番号TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EDI_MEMBER_ID_TextChanged(object sender, EventArgs e)
        {
            // 明細クリア
            this.logic.ichiranClear();
        }

        /// <summary>
        /// 電子廃棄物種類TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HAIKI_SHURUI_CD_TextChanged(object sender, EventArgs e)
        {
            // 明細クリア
            this.logic.ichiranClear();
        }

        /// <summary>
        /// 検索条件Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SEARCH_CONDITION_ITEM_Validated(object sender, EventArgs e)
        {
            // 検索条件ImeMode制御
            this.logic.changeIME();
        }

        #endregion - ControlEvent -

        #region - OtherEvent -
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面情報の初期化
            this.logic.WindowInit();

			// Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            base.OnShown(e);
        }

        #endregion - OtherEvent -
    }
}
