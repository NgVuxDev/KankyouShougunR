using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;
using r_framework.Utility;
using Seasar.Quill;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private HaisouKeikakuNyuuryoku.LogicClass logic = null;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        internal bool isShown = false;

        /// <summary>
        /// システムID
        /// </summary>
        internal string SystemId { get; set; }

        // 配送名保持用
        private string haisouName { get; set; }

        /// <summary>
        /// 前回業者コード
        /// </summary>
        private string beforGyoushaCD = string.Empty;

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        // 前回値
        internal string beforHaishaKBN { get; set; }
        internal string beforSagyouBegin { get; set; }
        internal string beforSagyouEnd { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : this(WINDOW_TYPE.NEW_WINDOW_FLAG, null, false)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="systemId">システムID</param>
        /// <param name="f4BtnUnLoock">F4連携削除ボタン制御</param>
        public UIForm(WINDOW_TYPE windowType, string systemId, bool f4BtnUnLoock)
            : base(WINDOW_ID.T_HAISOU_KEIKAKU_NYUURYOKU, windowType)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.logic.SYSTEM_ID = systemId;
            this.logic.F4_BTN_UNLOCK = f4BtnUnLoock;
            this.SystemId = systemId;

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

            if (!this.isLoaded)
            {
                //初期化、初期表示
                if (!this.logic.WindowInit(base.WindowType))
                {
                    return;
                }
            }

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran1 != null)
            {
                this.Ichiran1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
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

            if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.logic.ColumnReadOnly();
            }

            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント
        #region F1 送信連携
        /// <summary>
        /// 送信連携(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.RegistCommon(1, HTTP_METHOD.PUT);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F2 新規
        /// <summary>
        /// 新規(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 前回値クリア
            this.beforHaishaKBN = string.Empty;
            this.beforSagyouBegin = string.Empty;
            this.beforSagyouEnd = string.Empty;
            this.beforGyoushaCD = string.Empty;

            // 画面初期化
            this.WinInit();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F4 削除連携
        /// <summary>
        /// 削除連携(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.RegistCommon(4, HTTP_METHOD.DELETE);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F6 一覧
        /// <summary>
        /// [F6]一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G696", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F7 ｸﾘｱ
        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 前回値クリア
            this.beforHaishaKBN = string.Empty;
            this.beforSagyouBegin = string.Empty;
            this.beforSagyouEnd = string.Empty;
            this.beforGyoushaCD = string.Empty;

            this.logic.SetInitialRenkeiCondition();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F8 検索
        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            int count = this.logic.Search();

            if (0 < this.Ichiran1.Rows.Count)
            {
                this.Ichiran1.DataSource = new List<DeliveryDataDTO>();
            }

            if (count < 0)
            {
                return;
            }
            else if (count == 0)
            {
                this.logic.msgLogic.MessageBoxShow("C001");
                return;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9 登録
        /// <summary>
        /// 登録(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.RegistCommon(9, 0);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            base.CloseTopForm();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SUBF1 組込み
        /// <summary>
        /// 組込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.SearchDTO == null)
            {
                this.logic.msgLogic.MessageBoxShowError("組込対象が存在しません。");
                return;
            }

            if (this.logic.HasErrorCheckKumikomi())
            {
                return;
            }

            this.logic.SetDeliveryData();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SUBF2 順番整列
        /// <summary>
        /// 順番整列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SortIchiran();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SUBF3 荷積挿入
        /// <summary>
        /// 荷積挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.HasErrorAddNizumiNiorosi())
            {
                return;
            }

            this.logic.AddNizumiGenba();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SUBF4 荷降挿入
        /// <summary>
        /// 荷降挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.HasErrorAddNizumiNiorosi())
            {
                return;
            }

            this.logic.AddNioroshiGenba();

            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion

        #region F1、F4、F9共通処理
        /// <summary>
        /// F1、F4、F9共通処理
        /// ほとんど内容が同じなのでまとめた
        /// </summary>
        /// <param name="FunctionNo">ファンクション番号</param>
        /// <param name="method">リクエストに飛ばすHTTPメソッド</param>
        private void RegistCommon(int FunctionNo, HTTP_METHOD method)
        {
            int count = 0;

            //まず配送名を保存
            this.haisouName = this.DELIVERY_NAME.Text;

            // 登録チェック
            if (this.logic.HasErrorRegist())
            {
                return;
            }

            // F9仮登録で「1.未送信」。それ以外は「2.送信済」で連携状態設定
            short linkStatus = 0;
            if (FunctionNo == 9)
            {
                linkStatus = 1;
            }
            else
            {
                linkStatus = 2;
            }
            // 登録用Entityの作成
            this.logic.CreateEntity(linkStatus);

            if (FunctionNo != 9)
            {
                // 送信
                count = this.logic.JsonConnection(method);
                if (count < 0)
                {
                    //エラー時配送名を元に戻す
                    this.DELIVERY_NAME.Text = this.haisouName;
                    return;
                }
                else if (count == 0)
                {
                    this.logic.msgLogic.MessageBoxShow("C001");
                    return;
                }
            }

            // モード別更新処理
            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    count = this.logic.RegistData();
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    count = this.logic.UpdateData();
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:     // 参照モードはF4削除連携時のみを想定
                    count = this.logic.LogicalDeleteData();
                    break;
            }

            // エラーしたら抜ける
            if (count == 0)
            {
                //エラー時配送名を元に戻す
                this.DELIVERY_NAME.Text = this.haisouName;
                return;
            }

            // 完了メッセージ
            if (FunctionNo == 9)
            {
                if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    this.logic.msgLogic.MessageBoxShow("I001", "削除");
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            else
            {
                this.logic.msgLogic.MessageBoxShow("I001", "連携");
            }

            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("G695", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
            {
                // 初期化
                this.WinInit();
            }
            else
            {
                // 新規権限がない場合は画面Close
                base.CloseTopForm();
            }
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面初期化処理
        /// 複数個所から呼ぶためまとめた
        /// </summary>
        private void WinInit()
        {
            // システムID初期化
            this.SystemId = "";

            // DTO初期化
            this.logic.RegistDTO = null;

            // DGV初期化
            this.customDataGridView1.DataSource = new List<DeliveryPlanDTO>();
            this.Ichiran1.DataSource = new List<DeliveryDataDTO>();

            // 画面タイプ初期化
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // ヘッダー情報
            base.HeaderFormInit();

            // 新規モードで表示する
            this.logic.ModeInit(WINDOW_TYPE.NEW_WINDOW_FLAG);
        }
        #endregion

        #region 抽出条件のイベント
        /// <summary>
        /// 配車区分が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void HAISHA_KBN_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 前回値保存
            this.beforHaishaKBN = this.HAISHA_KBN.Text;

            // 配車区分の値を変数にセット
            if (this.HAISHA_KBN.Text == string.Empty)
            {
                this.logic.haisha_kbn = 0;
            }
            else
            {
                this.logic.haisha_kbn = int.Parse(this.HAISHA_KBN.Text);
            }

            // 追加モードで配車区分を変更したらDGV初期化
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.customDataGridView1.DataSource = new List<DeliveryPlanDTO>();
                this.Ichiran1.DataSource = new List<DeliveryDataDTO>();

                // SearchByTeikiでエラーしないように読み込み
                this.logic.SearchDTO = this.logic.CreateSearchDto();

                // 表示切替
                this.logic.ColumnVisiblePlan();
                this.logic.ColumnVisibleData();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 配車区分未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HAISHA_KBN_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (string.IsNullOrEmpty(this.HAISHA_KBN.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.HAISHA_KBN.Focus();
                this.HAISHA_KBN.Text = "1";
                // 前回値保存
                this.beforHaishaKBN = this.HAISHA_KBN.Text;
                this.logic.msgLogic.MessageBoxShow("W001", "1", "2");
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 作業日Fromダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.SAGYOU_BEGIN.Text = this.SAGYOU_END.Text;
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 作業日Toダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.SAGYOU_END.Text = this.SAGYOU_BEGIN.Text;
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 作業日Fromチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_BEGIN_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.SAGYOU_BEGIN.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E001", "作業日");
                e.Cancel = true;
                return;
            }
            
            // 前回値保存
            this.beforSagyouBegin = this.SAGYOU_BEGIN.Text;

            DateTime date_from = DateTime.Parse(this.SAGYOU_BEGIN.Text);

            if (date_from < this.logic.date_today)
            {
                // 今日より前はアラート
                this.logic.msgLogic.MessageBoxShowWarn("本日より、過去の作業日の場合、システム連携が行えません。");
            }
        }

        /// <summary>
        /// 作業日Toチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_END_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.SAGYOU_END.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E001", "作業日");
                e.Cancel = true;
                return;
            }
            
            // 前回値保存
            this.beforSagyouEnd = this.SAGYOU_END.Text;

            DateTime date_to = DateTime.Parse(this.SAGYOU_END.Text);

            if (date_to < this.logic.date_today)
            {
                // 今日より前はアラート
                this.logic.msgLogic.MessageBoxShowWarn("本日より、過去の作業日の場合、システム連携が行えません。");
            }
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (String.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = String.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = string.Empty;
            }
            else if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_RNAME.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E051", "業者");
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_CD.Focus();
                return;
            }

            this.logic.CheckGenba();
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.ChechSharyouCd();
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckUnpanGyoushaCd();
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckUntenshaCd();
        }

        #endregion

        #region ポップアップの前後処理
        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            curGyoushaCd = this.GYOUSHA_CD.Text;
            if (preGyoushaCd != curGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 荷降現場検索ポップアップ前処理(スポット：荷降挿入ボタン押下時)
        /// </summary>
        public void NioroshiGenbaPopupBeforeMethod()
        {
            this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.NIOROSHI_GENBA_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 荷降現場検索ポップアップ後処理(スポット：荷降挿入ボタン押下時)
        /// </summary>
        public void NioroshiGenbaPopupMethod()
        {
            this.logic.NioroshiGenbaPopupBefore(true);
        }

        /// <summary>
        /// 荷積現場検索ポップアップ前処理(スポット：荷積挿入ボタン押下時)
        /// </summary>
        public void NizumiGenbaPopupBeforeMethod()
        {
            this.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.NIZUMI_GENBA_CD.Text = string.Empty;
            this.NIZUMI_GENBA_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 荷積現場検索ポップアップ後処理(スポット：荷積挿入ボタン押下時)
        /// </summary>
        public void NizumiGenbaPopupMethod()
        {
            this.logic.NizumiGenbaPopupBefore(true);
        }

        /// <summary>
        /// 現場検索ポップアップ前処理(定期：荷積 or 荷降挿入ボタン押下時)
        /// </summary>
        public void TeikiGenbaPopupBeforeMethod()
        {
            this.TEIKI_GYOUSHA_CD.Text = string.Empty;
            this.TEIKI_GYOUSHA_NAME.Text = string.Empty;
            this.TEIKI_GENBA_CD.Text = string.Empty;
            this.TEIKI_GENBA_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 現場検索ポップアップ後処理(定期：荷積 or 荷降挿入ボタン押下時)
        /// </summary>
        public void TeikiGenbaPopupMethod()
        {
            if (this.logic.AddNizumiGenbaFlg)
            {
                this.logic.NizumiGenbaPopupBefore(false);
            }
            else
            {
                this.logic.NioroshiGenbaPopupBefore(false);
            }
        }
        #endregion

    }
}
