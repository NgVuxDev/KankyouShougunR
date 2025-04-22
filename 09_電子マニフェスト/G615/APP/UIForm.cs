// $Id: UIForm.cs 48020 2015-04-22 08:58:27Z saitou $
using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using Seasar.Quill;
using Shougun.Core.Inspection.KongouHaikibutsuJoukyouIchiran;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran
{
    /// <summary>
    /// 混合廃棄物状況一覧画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region - Field -

        /// <summary>画面Logic</summary>
        private LogicClass logic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion - Field -

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // 混合廃棄物状況一覧ロジック
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
            switch (ctrl.Name)
            {
                case "bt_func1":
                    // 振分登録画面遷移
                    this.logic.showDistributeDisp();
                    break;

                case "bt_func2":
                    // 新規登録画面遷移
                    this.logic.showNewDisp();
                    break;

                case "bt_func3":
                    // 修正登録画面遷移
                    this.logic.showModifyDisp();
                    break;

                case "bt_func4":
                    // 削除登録画面遷移
                    this.logic.showDeleteDisp();
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
                    if (!this.logic.SearchIchiran())
                    {
                        return;
                    }
                    break;

                case "bt_func10":
                    // 並び替え
                    this.logic.sort();
                    break;

                case "bt_func12":
                    // 画面Close
                    this.logic.closeDisp();
                    break;

                default:
                    // DO NOTHING
                    break;
            }
        }

        #endregion - FunctionKeyEvent -

        #region - ControlEvent -

        /// <summary>
        /// 抽出日付区分TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DATE_KBN_TextChanged(object sender, EventArgs e)
        {
            // 抽出日付ラベルTitle設定
            this.logic.setDateLabelTitle();
        }

        /// <summary>
        /// 抽出条件CtrlValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void searchConditionCtrlValidating(object sender, CancelEventArgs e)
        {
            var bRet = true;
            bool catchErr = false;
            // イベント振分
            var ctrl = (CustomTextBox)sender;
            var type = this.logic.getInputType(ctrl.Name);
            switch (type)
            {
                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GYOUSHA:
                    // 排出事業者CDセット
                    bRet = this.logic.setHstGyoushaCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HST_GENBA:
                    // 排出事業場CDセット
                    bRet = this.logic.setHstGenbaCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_SBN_GYOUSHA:
                    // 処分受託者CDセット
                    bRet = this.logic.setSbnGyoushaCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_SAKI_GENBA:
                    // 運搬先の事業場CDセット
                    bRet = this.logic.setUpnSakiGenbaCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_UPN_JYUTAKUSHA:
                    // 運搬受託者CDセット
                    bRet = this.logic.setUpnJyutakushaCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_HOUKOKUSHO_BUNRUI:
                    // 報告書分類CDセット
                    bRet = this.logic.setHoukokushoBunruiCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                case ConstClass.INPUT_TYPE.INPUT_TYPE_DENSHI_HAIKI_SHURUI:
                    // 電子廃棄物種類CDセット
                    bRet = this.logic.setDenshiHaikiShuruiCD(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    break;

                default:
                    // DO NOTHING
                    break;
            }

            if (bRet == false)
            {
                // 入力エラーが発生した場合は入力キャンセル
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 一覧CellDoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 修正登録画面遷移
                this.logic.showModifyDisp();
            }
        }

        /// <summary>
        /// FromTo抽出条件DoubleClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void fromToConditionCtrlDoubleClick(object sender, EventArgs e)
        {
            // FROMをTOにCopy
            var ctrl = (TextBox)sender;
            var type = this.logic.getInputType(ctrl.Name);
            this.logic.fromToCopy(type);
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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // 画面情報の初期化
            this.logic.WindowInit();
        }

        /// <summary>
        /// CD検索ポップアップ表示後イベント
        /// </summary>
        /// <param name="sender"></param>
        public void cdSearchPopupAfterEvent(object sender)
        {
            // ポップアップ表示後処理
            var btn = (CustomPopupOpenButton)sender;
            var type = this.logic.getInputType(btn.Name);
            if (!this.logic.cdSearchPopupAfterProc(type))
            {
                return;
            }
        }

        /// <summary>
        /// CD検索ポップアップ表示後イベント
        /// </summary>
        /// <param name="sender"></param>
        public void txtCdSearchPopupAfterEvent(object sender)
        {
            // ポップアップ表示後処理
            var txtBox = (CustomAlphaNumTextBox)sender;
            var type = this.logic.getInputType(txtBox.Name);
            if (!this.logic.cdSearchPopupAfterProc(type))
            {
                return;
            }
        }

        #endregion - OtherEvent -
    }
}