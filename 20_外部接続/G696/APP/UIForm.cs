using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Const;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuIchiran
{
    /// <summary>
    /// 配送計画一覧
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private HaisouKeikakuIchiran.LogicClass logic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_HAISOU_KEIKAKU_ICHIRAN)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            //完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //初期化、初期表示
            if (!this.logic.WindowInit()) { return; }

            // 初期フォーカス位置を設定します
            this.txtNum_HaishaKBN.Focus();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran1 != null)
            {
                this.Ichiran1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント

        /// <summary>
        /// 追加(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.search_renkei_kbn == LogicClass.RENKEI_SOUSHIN)
            {
                this.logic.OpenWindow(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, f4BtnUnLock:true);
            }
            else
            {
                this.logic.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.OpenWindow(WINDOW_TYPE.DELETE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SetInitialRenkeiCondition();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            int count = this.logic.Search();
            this.logic.ButtonEnabledControl();
            this.Ichiran1.Columns[ConstCls.CELL_TARGET].ReadOnly = true;

            if (count == 0)
            {
                this.logic.msgLogic.MessageBoxShow("C001");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取込確認(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.CanImport())
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            // データ取込み可能に
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.bt_func10.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// データ取込(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.HasErrorImportData())
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            if (this.logic.ImportData())
            {
                //処理完了したら再抽出
                this.logic.Search();
                this.logic.ButtonEnabledControl();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //処理
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Ichiran1.DataSource = "";

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 各項目のイベント処理

        #region 配車区分変更時の処理
        /// <summary>
        /// 配車区分が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void txtNum_HaishaKBN_TextChanged(object sender, EventArgs e)
        {
            //配車区分の値を変数にセット
            if (this.txtNum_HaishaKBN.Text == string.Empty)
            {
                this.logic.haisha_kbn = 0;
            }
            else
            {
                this.logic.haisha_kbn = int.Parse(this.txtNum_HaishaKBN.Text);
            }
        }

        /// <summary>
        /// 配車区分未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtNum_HaishaKBN_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_HaishaKBN.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_HaishaKBN.Focus();
                this.txtNum_HaishaKBN.Text = "1";
                this.logic.msgLogic.MessageBoxShow("W001", "1", "2");

            }
        }
        #endregion

        #region 連携状況変更時の処理
        /// <summary>
        /// 連携状況が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void txtNum_RenkeiJyoukyou_TextChanged(object sender, EventArgs e)
        {
            //配車区分の値を変数にセット
            if (this.txtNum_RenkeiJyoukyou.Text == string.Empty)
            {
                this.logic.renkei_kbn = 0;
            }
            else
            {
                this.logic.renkei_kbn = int.Parse(this.txtNum_RenkeiJyoukyou.Text);
            }
        }

        /// <summary>
        /// 連携状況未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtNum_RenkeiJyoukyou_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_RenkeiJyoukyou.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_RenkeiJyoukyou.Focus();
                this.txtNum_RenkeiJyoukyou.Text = "1";
                this.logic.msgLogic.MessageBoxShow("W001", "1", "3");

            }
        }
        #endregion

        #region 作業日変更時の処理
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TEKIYOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var tekiyouBeginTextBox = this.TEKIYOU_BEGIN;
            var tekiyouEndTextBox = this.TEKIYOU_END;
            tekiyouEndTextBox.Text = tekiyouBeginTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TEKIYOU_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TEKIYOU_END.Text))
            {
                this.TEKIYOU_END.IsInputErrorOccured = false;
                this.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TEKIYOU_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TEKIYOU_BEGIN.Text))
            {
                this.TEKIYOU_BEGIN.IsInputErrorOccured = false;
                this.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region 明細ダブルクリック
        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (this.logic.search_renkei_kbn == LogicClass.RENKEI_JYUSHIN)
            {
                // 受信済の場合、参照モード
                this.logic.OpenWindow(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            else if (this.logic.search_renkei_kbn == LogicClass.RENKEI_SOUSHIN)
            {
                // 送信済の場合、参照モード。但し、F4連携削除ボタンは押下可能
                this.logic.OpenWindow(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, f4BtnUnLock:true);
            }
            else
            {
                this.logic.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }
        #endregion

        #endregion
    }
}
