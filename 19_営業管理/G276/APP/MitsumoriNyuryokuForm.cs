// $Id: MitsumoriNyuryokuForm.cs 53608 2015-06-26 00:09:57Z miya@e-mall.co.jp $
using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.BusinessManagement.Const.Common;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DTO;
using Shougun.Core.BusinessManagement.MitsumoriNyuryoku;
using r_framework.FormManager;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Authority;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    /// <summary>
    /// 見積入力画面E:\SVN\trunk_1\19_営業管理\G276\APP\MitsumoriNyuryokuForm.cs
    /// </summary>
    [Implementation]
    public partial class MitsumoriNyuryokuForm : SuperForm
    {

        #region フィールド

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private Shougun.Core.BusinessManagement.MitsumoriNyuryoku.LogicClass logic;

        /// <summary>
        /// 見積入力のSYSTE_ID
        /// </summary>
        public long MitsumoriSysId = -1;

        /// <summary>
        /// 前回値チェック用変数(header用)
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        public Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 見積入力のSEQ
        /// </summary>
        public int MitsumoriSEQ = -1;

        /// <summary>
        /// 見積明細のSYSTE_ID
        /// </summary>
        public long MeisaiSysId = -1;

        /// <summary>
        /// 見積番号
        /// </summary>
        public long mitsumoriNumber = -1;

        /// <summary>
        /// 前回見積番号
        /// </summary>
        public string beforMitsumoriNumber = string.Empty;

        /// <summary>
        /// 前回取引先コード
        /// </summary>
        public string beforTorihikisakiCD = string.Empty;
        public string beforTorihikisakiHikiai = string.Empty;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        public string beforeGenbaCD = string.Empty;
        public string beforeGenbaHikiai = string.Empty;

        /// <summary>
        /// 前回取引先引合FLG
        /// </summary>
        public string beforGyoushaHikiai = string.Empty;

        public bool isInputError = false;
        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// FormのLeaveイベント実行中にMultiRowのデータを変更すると
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        public bool nowLoding = false;

        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// Formのcイベント実行中にMultiRowのデータを変更すると
        /// もう一回CellValueChangedイベントが発生してしまう問題の回避策
        /// </summary>
        public bool meisaiNowLoding = false;

        /// <summary>
        /// 新規モード処理中
        /// </summary>
        public bool changeNewModeNowLoding = false;


        /// <summary>
        /// 新規モードのフォーカス
        /// </summary>
        public bool gyouten_focus = false;

        /// <summary>
        /// CreateDenpyoNumbering trueの場合、CellValueChangedイベントを実行しない
        /// </summary>
        public bool CreateDenpyoNumbering = false;

        /// <summary>
        /// 画面のタイプ
        /// </summary>
        public WINDOW_TYPE WindowType_Back { get; set; }

        /// <summary>
        /// 現在イベント処理中かどうかフラグ
        /// FormのCellValueChangedイベント実行中にデータを変更すると
        /// もう一回CellValueChangedイベントが発生してしまう問題の回避策
        /// </summary>
        private bool cellValueChanging = false;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        HeaderForm headerForm;

        /// <summary>
        /// CustomDataGridView
        /// </summary>
        internal CustomDataGridView CustomDataGridView;

        /// <summary>
        /// TabPage
        /// </summary>
        private System.Windows.Forms.TabPage tbp_MitsumoriMeisai;

        /// <summary>
        /// 遷移元画面パラメータ
        /// </summary>
        public FormShowParamDao formParem;

        /// <summary>
        /// TabPage最大ページ
        /// </summary>
        private int maxTabPage = 10;

        /// <summary>
        /// 画面遷移が発生するコントロール名一覧
        /// </summary>
        private string[] controlNamesForChangeScreenEvents = new string[] { "bt_func7", "bt_func12" };


        /// <summary>
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、引数なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod();

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// </summary>
        public LastRunMethod closeMethod;


        /// <summary>
        /// CellValueChanged実行フラグ
        /// </summary>
        private bool bExecuteCalc = false;

        /// <summary>
        /// データ移動モード Flg
        /// True:データ移動モード
        /// </summary>
        internal bool moveData_flg = false;
        /// <summary>
        /// データ移動用 取引先
        /// </summary>
        internal string moveData_torihikisakiCd;
        /// <summary>
        /// データ移動用 業者
        /// </summary>
        internal string moveData_gyousyaCd;
        /// <summary>
        /// データ移動用 現場
        /// </summary>
        internal string moveData_genbaCd;

        /// <summary>
        /// 変更前の値保持用
        /// </summary>
        internal string beforeValue = string.Empty;

        /// <summary>
        /// 手入力か判断するフラグ
        /// true = 手入力  false = ポップアップ入力
        /// </summary>
        internal bool ManualInputFlg = true;

        /// <summary>
        /// 引合を表示するか判断するフラグです
        /// true = 引合表示   false = 既存表示
        /// 初期値を既存表示に設定
        /// </summary>
        internal bool hikiaiDisplaySwitchingFlg = false;

        /// <summary>
        /// true：複写
        /// </summary>
        public bool copyDataFlg = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        internal bool ManualInputGenbaFlg = true;

        //20250421
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();


        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType"></param>
        /// <param name="FormShowParamDao"></param>
        /// <param name="lastRunMethod">見積入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public MitsumoriNyuryokuForm(WINDOW_ID windowId, WINDOW_TYPE windowType, FormShowParamDao formParem, LastRunMethod lastRunMethod = null)
            : base(WINDOW_ID.T_MITSUMORI_NYUURYOKU, windowType)
        {

            LogUtility.DebugMethodStart(windowId, windowType, formParem.mitsumoriNumber, formParem.cyohyoType, lastRunMethod);
            try
            {
                CommonShogunData.Create(SystemProperty.Shain.CD);

                this.InitializeComponent();

                this.lb_TorihikisakiKeishou.AutoCompleteMode = AutoCompleteMode.None;
                this.lb_TorihikisakiKeishou.AutoCompleteSource = AutoCompleteSource.None;
                this.lb_GyoushaKeishou.AutoCompleteMode = AutoCompleteMode.None;
                this.lb_GyoushaKeishou.AutoCompleteSource = AutoCompleteSource.None;
                this.lb_GenbaKeishou.AutoCompleteMode = AutoCompleteMode.None;
                this.lb_GenbaKeishou.AutoCompleteSource = AutoCompleteSource.None;

                if (formParem.cyohyoType == 0)
                {
                    // 見積書種類のデフォルトに「１:単価見積書(横)」をセット
                    formParem.cyohyoType = MitumorisyoConst.MITUMOTISYO_TANKA_YOKO;
                }

                // 最初の拠点フォーカスへ移動
                gyouten_focus = true;

                // ページを追加
                this.PageAdd();
                this.components = new System.ComponentModel.Container();
                this.CustomDataGridView = new CustomDataGridView();
                this.WindowId = windowId;
                this.WindowType = windowType;
                this.formParem = formParem;
                // 見積番号設定
                if (!string.IsNullOrEmpty(formParem.mitsumoriNumber))
                {
                    this.mitsumoriNumber = long.Parse(formParem.mitsumoriNumber);
                }
                else
                {
                    this.mitsumoriNumber = -1;

                }
                this.closeMethod = lastRunMethod;

                // 現在のタブコントロールのサイズに明細欄のサイズを合わせる
                this.MeisaiAncher();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);

            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();

        }


        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public MitsumoriNyuryokuForm(WINDOW_ID windowId, WINDOW_TYPE windowType, FormShowParamDao formParem, LastRunMethod lastRunMethod,
                                                                    string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(windowId, windowType, formParem, lastRunMethod)
        {
            //データ移動用
            this.moveData_flg = true;
            this.moveData_torihikisakiCd = torihikisakiCd;
            this.moveData_gyousyaCd = gyousyaCd;
            this.moveData_genbaCd = genbaCd;
        }

        #endregion

        #region 画面OnShown処理
        /// <summary>
        /// 画面OnShown処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
            // 見積番号検索以外にフォーカス設定
            if (gyouten_focus)
            {
                this.dtp_MitsumoriDate.Focus();
                gyouten_focus = false;
            }

        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            LogUtility.DebugMethodStart(e);

            try
            {

                // 保存
                WindowType_Back = this.WindowType;

                if (!string.IsNullOrEmpty(formParem.mitsumoriNumber) && this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                base.OnLoad(e);
                bool catchErr = false;
                bool isOpenFormError = this.logic.GetAllEntityData(out catchErr);
                if (catchErr) { return; }
                ParentBaseForm = (BusinessBaseForm)this.Parent;
                this.headerForm = (HeaderForm)ParentBaseForm.headerForm;

                nowLoding = true;

                if (!this.logic.WindowInit()) { return; }

                nowLoding = false;



                if (!string.IsNullOrEmpty(formParem.mitsumoriNumber) && WindowType_Back.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 処理モードを新規にする
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.logic.setHeaderInfo(this.WindowType);


                    // 見積番号を空白にする
                    this.txt_MitsumoriNumber.Text = string.Empty;

                    // 見積日付
                    var parentForm = (BusinessBaseForm)this.Parent;
                    this.dtp_MitsumoriDate.Value = parentForm.sysDate;

                    // 登録者情報
                    this.logic.headerForm.CreateUser.Text = string.Empty;
                    this.logic.headerForm.CreateDate.Text = string.Empty;

                    // 更新者情報
                    this.logic.headerForm.LastUpdateUser.Text = string.Empty;
                    this.logic.headerForm.LastUpdateDate.Text = string.Empty;


                    // 活性制御
                    if (!this.logic.ChangeEnabledForInputControl(false)) { return; }

                    // 初期表示
                    //取引先、業者、現場ロストフォーカスイベント
                    this.logic.Torihikisaki_GyoushaCD_GenbaCD_LostFocus(txt_TorihikisakiCD.Text, this.txt_GyoushaCD.Text, this.txt_GenbaCD.Text, out catchErr);
                    if (catchErr) { return; }
                    //取引先、業者、現場タブ画面表示設定
                    if (!this.logic.Torihikisaki_GyoushaCD_GenbaCD_SetTab(false)) { return; }


                    // ボタンを初期化
                    this.logic.ButtonInit();
                }

                this.logic.ButtonInit();
                this.logic.EventInit();

                if (!isOpenFormError)
                {
                    this.FormClose(null, e);
                }
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 見積番号ロストフォーカスイベント
        /// <summary>
        /// 見積番号ロストフォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void txt_MitsumoriNumber_LostFocus(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);
            //try
            //{

            //    if (this.beforMitsumoriNumber != this.txt_MitsumoriNumber.Text)
            //    {
            //        // 修正権限チェック
            //        if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            //        {
            //            // 処理中二回呼び出し禁止
            //            if (!nowLoding)
            //            {
            //                nowLoding = true;

            //                WINDOW_TYPE dispWindowType = this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG) ? WINDOW_TYPE.DELETE_WINDOW_FLAG : WINDOW_TYPE.UPDATE_WINDOW_FLAG;

            //                // 見積検索処理
            //                this.MitsumoriNumber_Search(sender, e, dispWindowType);

            //                nowLoding = false;
            //            }

            //            // 前回検索取引先コードを設定
            //            this.beforMitsumoriNumber = this.txt_MitsumoriNumber.Text;
            //        }
            //        else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            //        {
            //            // 修正権限が無く、参照権限がある場合は参照モードで起動する
            //            this.ChangeRefWindow(sender, e);
            //        }
            //        else
            //        {
            //            // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
            //            var msgLogic = new MessageBoxShowLogic();
            //            msgLogic.MessageBoxShow("E158", "修正");
            //            this.txt_MitsumoriNumber.Focus();
            //        }
            //    }
            //}
            //catch
            //{
            //    throw;
            //}

            //LogUtility.DebugMethodEnd();
        }

        /*
         * 権限チェック時のアラート表示で、アラート表示中も見積番号のテキストボックスが水色表示になってほしいためイベントを変更。
         * ※LostForcusの場合、アラート前のフォーカスを見積番号に当てるとイベントの無限ループになるため使用出来ない。
         */

        /// <summary>
        /// 見積番号 - Validated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_MitsumoriNumber_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                bool catchErr = false;
                if (this.beforMitsumoriNumber != this.txt_MitsumoriNumber.Text)
                {
                    // 修正権限チェック
                    if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 処理中二回呼び出し禁止
                        if (!nowLoding)
                        {
                            nowLoding = true;
                            this.moveData_flg = false;

                            WINDOW_TYPE dispWindowType = this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG) ? WINDOW_TYPE.DELETE_WINDOW_FLAG : WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                            // 見積検索処理
                            this.MitsumoriNumber_Search(sender, e, dispWindowType, out catchErr);
                            if (catchErr) { return; }

                            nowLoding = false;
                        }

                        // 前回検索取引先コードを設定
                    }
                    else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.moveData_flg = false;
                        // 修正権限が無く、参照権限がある場合は参照モードで起動する
                        this.ChangeRefWindow(sender, e);
                    }
                    else
                    {
                        // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                        this.txt_MitsumoriNumber.Focus();
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 見積検索処理
        /// <summary>
        /// 見積検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MitsumoriNumber_Search(object sender, EventArgs e, WINDOW_TYPE dispWindowType, out bool catchErr)
        {
            LogUtility.DebugMethodStart(sender, e, dispWindowType);
            catchErr = false;
            try
            {
                // 保存
                WindowType_Back = this.WindowType;
                // 見積番号
                long mitsumoriNumber = 0;
                if (!string.IsNullOrEmpty(this.txt_MitsumoriNumber.Text)
                    && long.TryParse(this.txt_MitsumoriNumber.Text.ToString(), out mitsumoriNumber))
                {
                    this.WindowType = dispWindowType;
                    this.mitsumoriNumber = mitsumoriNumber;

                    bool isGetAllData = this.logic.GetAllEntityData(out catchErr);
                    if (catchErr) { return; }
                    if (!isGetAllData)
                    {
                        switch (WindowType_Back)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 処理モードを新規にする
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.logic.setHeaderInfo(this.WindowType);
                                break;
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                break;
                            case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                                break;
                            default:
                                break;

                        }

                        //base.OnLoad(e);


                        // エラー発生時には値をクリアする
                        this.txt_MitsumoriNumber.IsInputErrorOccured = true;
                        this.txt_MitsumoriNumber.Focus();


                        nowLoding = false;
                        return;
                    }
                    this.txt_MitsumoriNumber.IsInputErrorOccured = false;
                    this.logic.setHeaderInfo(this.WindowType);
                    // 画面表示
                    if (!this.logic.WindowInit())
                    {
                        catchErr = true;
                        return;
                    }

                    // ボタン制御
                    this.logic.ButtonInit();

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MitsumoriNumber_Search", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 前へボタン押下イベント
        /// <summary>
        /// 前へボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PreMitsumoriData(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                String previousNumber;
                String tableName = "T_MITSUMORI_ENTRY";
                String fieldName = "MITSUMORI_NUMBER";
                String mitsumoriNumber = this.txt_MitsumoriNumber.Text;
                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                // 前の見積番号を取得
                previousNumber = this.logic.GetPreviousNumber(tableName, fieldName, mitsumoriNumber, out catchErr);
                if (catchErr) { return; }
                if (previousNumber == "")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return;
                }
                // 修正権限チェック
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 見積番号を設定
                    this.txt_MitsumoriNumber.Text = previousNumber;
                    // イベント削除
                    this.txt_MitsumoriNumber.Enter -= this.Control_Enter;

                    nowLoding = true;
                    this.moveData_flg = false;

                    // 見積検索処理
                    this.MitsumoriNumber_Search(sender, e, WINDOW_TYPE.UPDATE_WINDOW_FLAG, out catchErr);
                    if (catchErr) { return; }

                    // 見積番号にフォーカスを設定
                    this.txt_MitsumoriNumber.Focus();

                    nowLoding = false;
                }
                else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 見積番号を設定
                    this.txt_MitsumoriNumber.Text = previousNumber;

                    // イベント削除
                    this.txt_MitsumoriNumber.Enter -= this.Control_Enter;

                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                    this.ChangeRefWindow(sender, e);
                }
                else
                {
                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 次へボタン押下イベント
        /// <summary>
        /// 次へボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NextMitsumoriData(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;

            try
            {
                String nextNumber;
                String tableName = "T_MITSUMORI_ENTRY";
                String fieldName = "MITSUMORI_NUMBER";
                String mitsumoriNumber = this.txt_MitsumoriNumber.Text;
                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                // 次の見積番号を取得
                nextNumber = this.logic.GetNextNumber(tableName, fieldName, mitsumoriNumber, out catchErr);
                if (catchErr) { return; }
                if (nextNumber == "")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return;
                }
                // 修正権限チェック
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 見積番号を設定
                    this.txt_MitsumoriNumber.Text = nextNumber;
                    // イベント削除
                    this.txt_MitsumoriNumber.Enter -= this.Control_Enter;
                    nowLoding = true;
                    this.moveData_flg = false;

                    // 見積検索処理
                    this.MitsumoriNumber_Search(sender, e, WINDOW_TYPE.UPDATE_WINDOW_FLAG, out catchErr);
                    if (catchErr) { return; }

                    // 見積番号にフォーカスを設定
                    this.txt_MitsumoriNumber.Focus();

                    nowLoding = false;
                }
                else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 見積番号を設定
                    this.txt_MitsumoriNumber.Text = nextNumber;

                    // イベント削除
                    this.txt_MitsumoriNumber.Enter -= this.Control_Enter;

                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                    this.ChangeRefWindow(sender, e);
                }
                else
                {
                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 引合登録ボタン押下イベント
        /// <summary>
        /// 引合登録ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RegistHikiaiData(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.HikiaiSetRequiredSetting();
            var HikiaiAutoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
            base.RegistErrorFlag = HikiaiAutoCheckLogic.AutoRegistCheck();
            this.logic.HIkiaiRequiredSettingInit();


            // 登録処理
            if (!base.RegistErrorFlag)
            {
                if (!this.logic.CreateHikiaiEntityAndUpdateTables(base.RegistErrorFlag))
                {
                    return;
                }

                // 初期表示
                //取引先、業者、現場ロストフォーカスイベント
                bool catchErr = false;
                this.logic.Torihikisaki_GyoushaCD_GenbaCD_LostFocus(txt_TorihikisakiCD.Text, this.txt_GyoushaCD.Text, this.txt_GenbaCD.Text, out catchErr);
                if (catchErr) { return; }
                //取引先、業者、現場タブ画面表示設定
                this.logic.Torihikisaki_GyoushaCD_GenbaCD_SetTab(true);

            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 新規ボタン押下イベント
        /// <summary>
        /// 新規ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 新規権限がある場合処理続行
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    changeNewModeNowLoding = true;
                    this.copyDataFlg = false;

                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.mitsumoriNumber = -1;

                    //base.OnLoad(e);
                    bool catchErr = false;
                    this.logic.GetAllEntityData(out catchErr);
                    if (catchErr) { return; }

                    if (!this.logic.WindowInit()) { return; }
                    this.logic.ButtonInit();
                    base.HeaderFormInit();

                    // 最初の拠点フォーカスへ移動
                    gyouten_focus = true;

                    changeNewModeNowLoding = false;

                    this.dtp_MitsumoriDate.Focus();
                }
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 修正ボタン押下イベント
        /// <summary>
        /// 修正ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeUpdateWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            bool catchErr = false;
            try
            {
                // 修正権限チェック
                if (Manager.CheckAuthority("G276", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    nowLoding = true;
                    this.copyDataFlg = false;

                    // 見積番号更新後処理
                    this.MitsumoriNumber_Search(sender, e, WINDOW_TYPE.UPDATE_WINDOW_FLAG, out catchErr);
                    if (catchErr) { return; }

                    // 最初の拠点フォーカスへ移動
                    gyouten_focus = true;

                    this.headerForm.Focus();
                    this.headerForm.KYOTEN_CD.Focus();

                    nowLoding = false;
                }
                else if (Manager.CheckAuthority("G276", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 修正権限が無く、参照権限がある場合は参照モードで起動する
                    this.ChangeRefWindow(sender, e);
                }
                else
                {
                    // 修正・参照権限が無い場合は、修正権限なしのエラーを表示する
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 参照画面遷移イベント
        /// <summary>
        /// 参照画面遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeRefWindow(object sender, EventArgs e)
        {
            // 参照モードで情報を展開
            bool catchErr = false;
            this.MitsumoriNumber_Search(sender, e, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, out catchErr);
            this.moveData_flg = false;
        }
        #endregion

        #region プレビューボタン押下イベント
        /// <summary>
        /// プレビューボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowPreview(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 見積書種類のみ必須チェック
            if (string.IsNullOrEmpty(this.MITSUMORISYURUI_CD.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "見積書種類");
                return;
            }

            // 帳票出力
            this.logic.Print();

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 一覧ボタン押下イベント
        /// <summary>
        /// 一覧ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowMitsumoriForm(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 参照権限がある場合一覧画面表示
            FormManager.OpenFormWithAuth("G277", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 複写ボタン押下イベント
        /// <summary>
        /// 複写ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CopyData(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 新規権限がある場合処理続行
            if (Manager.CheckAuthority("G276", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                // 処理モードを新規にする
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.logic.setHeaderInfo(this.WindowType);

                // 複写フラグをtrueにする
                this.copyDataFlg = true;

                // 見積番号を空白にする
                this.txt_MitsumoriNumber.Text = string.Empty;

                // 見積日付
                var parentForm = (BusinessBaseForm)this.Parent;
                this.dtp_MitsumoriDate.Value = parentForm.sysDate;

                // 登録者情報
                this.logic.headerForm.CreateUser.Text = string.Empty;
                this.logic.headerForm.CreateDate.Text = string.Empty;

                // 更新者情報
                this.logic.headerForm.LastUpdateUser.Text = string.Empty;
                this.logic.headerForm.LastUpdateDate.Text = string.Empty;

                // 活性制御
                if (!this.logic.ChangeEnabledForInputControl(false)) { return; }

                // 初期表示
                //取引先、業者、現場ロストフォーカスイベント
                bool catchErr = false;
                this.logic.Torihikisaki_GyoushaCD_GenbaCD_LostFocus(txt_TorihikisakiCD.Text, this.txt_GyoushaCD.Text, this.txt_GenbaCD.Text, out catchErr);
                if (catchErr) { return; }
                //取引先、業者、現場タブ画面表示設定
                if (!this.logic.Torihikisaki_GyoushaCD_GenbaCD_SetTab(true)) { return; }

                // 複写不要なデータをクリア
                this.dtp_JuchuDate.Value = null;
                this.dtp_SichuDate.Value = null;
                this.txt_JykyoFlg.Text = string.Empty;

                // ボタンを初期化
                this.logic.ButtonInit();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 登録ボタン押下イベント
        /// <summary>
        /// 登録ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RegistMistumori(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                bool catchErr = false;

                // 取引先CDと拠点CDの関連チェック
                if (this.txt_Torihikisaki_hikiai_flg.Text.Equals("0"))
                {
                    // 画面の取引先CDが「既存取引先」の場合
                    SqlDateTime date = SqlDateTime.Null;
                    if (this.dtp_MitsumoriDate.Value != null)
                    {
                        date = Convert.ToDateTime(this.dtp_MitsumoriDate.Value);
                    }
                    var torihikisakiEntity = this.logic.GetTorihikisaki(this.txt_TorihikisakiCD.Text, date, out catchErr);
                    if (catchErr) { return; }
                    bool toriCheck = this.logic.CheckTorihikisakiAndKyotenCd(null, torihikisakiEntity, this.txt_TorihikisakiCD.Text, out catchErr);
                    if (catchErr || !toriCheck)
                    {
                        return;
                    }
                }
                else if (this.txt_Torihikisaki_hikiai_flg.Text.Equals("1"))
                {
                    // 画面の取引先CDが「引合取引先」の場合
                    var torihikisakiEntity_hiki = this.logic.GetHikiaiTorihikisakiEntry(this.txt_TorihikisakiCD.Text, out catchErr);
                    if (catchErr) { return; }
                    bool hikiToriCheck = this.logic.CheckTorihikisakiAndKyotenCd(torihikisakiEntity_hiki, null, this.txt_TorihikisakiCD.Text, out catchErr);
                    if (catchErr || !hikiToriCheck)
                    {
                        return;
                    }
                }

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                        if (!this.logic.SetRequiredSetting()) { return; }
                        var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                        base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                        if (!this.logic.RequiredSettingInit()) { return; }

                        // 引合チェック関連チェック
                        if (!base.RegistErrorFlag)
                        {
                            bool errCheck = this.logic.HikiaiCheck(out catchErr);
                            if (catchErr) { return; }
                            if (!errCheck)
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        bool ret = true;
                        // 空ページはページ間に残せません。
                        if (!base.RegistErrorFlag)
                        {
                            ret = this.logic.CheckRequiredDataForDeital(out catchErr);
                            if (catchErr) { return; }
                            if (!ret)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                // 空ページはページ間に残せません。
                                msgLogic.MessageBoxShow("E070");
                                base.RegistErrorFlag = true;
                            }
                        }

                        // 入力された明細行が1行もありません
                        if (!base.RegistErrorFlag)
                        {
                            ret = this.logic.CheckRequiredDataForDeitalEmpty(out catchErr);
                            if (catchErr) { return; }
                            if (!ret)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                // 入力された明細行が1行もありません
                                msgLogic.MessageBoxShow("E067");
                                base.RegistErrorFlag = true;
                            }
                        }

                        // 税区分必須チェック
                        if (!base.RegistErrorFlag)
                        {
                            ret = this.logic.SetMeisaiRequiredSetting(out catchErr);
                            if (catchErr) { return; }
                            if (!ret)
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        // 20140711 ria No.947 営業管理機能改修 start
                        // 状況チェック
                        if (!base.RegistErrorFlag)
                        {
                            ret = this.logic.CheckJokyoCd(out catchErr);
                            if (catchErr) { return; }
                            if (!ret)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 start
                                // 入力された状況が1、2以外の場合
                                msgLogic.MessageBoxShow("E002", "状況", "1～2");
                                // 20140715 ria EV005247 状況CDにて「２．受注」「３．失注」となっている。 end
                                base.RegistErrorFlag = true;
                            }
                            // 20140711 ria No.947 営業管理機能改修 end
                        }

                        break;

                    default:
                        base.RegistErrorFlag = false;
                        break;
                }

                // 登録処理
                if (!base.RegistErrorFlag)
                {
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                            {
                                break;
                            }

                            this.copyDataFlg = false;

                            // 新規権限チェック
                            if (Manager.CheckAuthority("G276", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.mitsumoriNumber = -1;

                                bool isGetAllData = this.logic.GetAllEntityData(out catchErr);
                                if (catchErr) { return; }
                                if (!isGetAllData)
                                {
                                    // エラー発生時には値をクリアする
                                    this.ChangeNewWindow(sender, e);
                                    return;
                                }
                                if (!this.logic.WindowInit()) { return; }
                                this.logic.ButtonInit();
                            }
                            else
                            {
                                // 新規権限が無ければ、画面を閉じる
                                this.FormClose(sender, e);
                            }
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                            {
                                return;
                            }

                            this.copyDataFlg = false;

                            // 登録後表示制御
                            this.logic.GetAllEntityData(out catchErr);
                            if (catchErr) { return; }

                            // 新規権限がある場合処理続行
                            if (Manager.CheckAuthority("G276", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                // 新規モード
                                this.ChangeNewWindow(sender, e);
                            }
                            else
                            {
                                // 新規権限が無ければ、画面を閉じる
                                this.FormClose(sender, e);
                            }

                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            var result = msgLogic.MessageBoxShow("C026");
                            if (result == DialogResult.Yes)
                            {
                                if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                                {
                                    return;
                                }

                                this.copyDataFlg = false;

                                // 新規権限がある場合処理続行
                                if (Manager.CheckAuthority("G276", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // 新規モード
                                    this.ChangeNewWindow(sender, e);
                                }
                                else
                                {
                                    // 新規権限が無ければ、画面を閉じる
                                    this.FormClose(sender, e);
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行挿入イベント
        /// <summary>
        /// 行挿入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DataRowAdd(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.CustomDataGridView.Select();

            int rowIndex = 0;
            if (this.CustomDataGridView.Rows.Count > 1)
            {
                rowIndex = this.CustomDataGridView.SelectedCells[0].RowIndex;
            }
            // 新規行追加
            this.CustomDataGridView.Rows.Insert(rowIndex);

            // ROW_NOを採番
            if (!this.logic.CreateDenpyoNumber()) { return; }

            // 小計の計算
            this.logic.Calculate(string.Empty);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行挿入(自動)
        /// <summary>
        /// 行挿入(自動)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CustomDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ロード場合、処理しない
                if (nowLoding)
                {
                    return;
                }

                // 伝票番号を作成時にこのイベント処理しない
                if (CreateDenpyoNumbering)
                {
                    return;
                }

                if (e.RowIndex > 0)
                {
                    // ROW_NOを採番
                    this.logic.CreateDenpyoNumber();

                }

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行削除イベント
        /// <summary>
        /// 行削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DataRowDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.CustomDataGridView.Rows.Count > 1)
            {


                int rowIndex = this.CustomDataGridView.SelectedCells[0].RowIndex;
                if (this.CustomDataGridView.Rows.Count - 1 > rowIndex)
                {
                    this.CustomDataGridView.Rows.RemoveAt(rowIndex);


                    // ROW_NOを採番
                    if (!this.logic.CreateDenpyoNumber()) { return; }

                    // 小計の計算
                    this.logic.Calculate(string.Empty);

                }

            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行削除(自動)
        /// <summary>
        /// 行削除(自動)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CustomDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ロード場合、処理しない
                if (nowLoding)
                {
                    return;
                }

                // 伝票番号を作成時にこのイベント処理しない
                if (CreateDenpyoNumbering)
                {
                    return;
                }


                // 最初の一行は削除しない
                if (e.RowIndex == 0)
                {
                    return;
                }
                // ROW_NOを採番
                this.logic.CreateDenpyoNumber();

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {

                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
                if (closeMethod != null)
                {
                    this.closeMethod();
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行コピーイベント
        /// <summary>
        /// 行コピーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RowCopy(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 選択行番号取得
            int intRowNo = this.CustomDataGridView.CurrentRow.Index;
            int rowNo = 0;
            // 小計行
            var shoukeiFlg = this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];
            if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
            {
            }
            else
            {
                // 最後の行はコピーしない
                if (this.CustomDataGridView.Rows.Count - 1 == intRowNo)
                {
                    return;
                }
                rowNo = int.Parse(this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_ROW_NO].Value.ToString());

                // 行をコピー
                this.logic.RowCopy(rowNo);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 行貼付イベント
        /// <summary>
        /// 行貼付イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RowPast(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.logic.copyDetail != null)
            {

                // 選択行番号取得
                int intRowNo = this.CustomDataGridView.CurrentRow.Index;

                // 最後の行の場合、1行追加
                if (this.CustomDataGridView.RowCount == intRowNo + 1)
                {
                    // 新規行追加
                    this.CustomDataGridView.Rows.Add();
                    // ROW_NOを採番
                    if (!this.logic.CreateDenpyoNumber()) { return; }
                }
                // 行をコピー
                this.logic.RowPast(intRowNo);

            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 小計挿入イベント
        /// <summary>
        /// 小計挿入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SubTotal(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 最小行は挿入できません
            if (this.CustomDataGridView.CurrentRow != null && this.CustomDataGridView.CurrentRow.Index > 0)
            {
                // 選択行番号取得
                int intRowNo = this.CustomDataGridView.CurrentRow.Index;

                // 新規行追加
                this.CustomDataGridView.Rows.Insert(intRowNo);

                // 小計フラグにTrueを設定(判定用）
                this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG].Value = "True";

                // ROW_NOを採番
                this.logic.CreateDenpyoNumber();

                // 行をコピー
                this.logic.SubTotal(intRowNo);

                // 合計計算
            }


            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ページ挿入イベント
        /// <summary>
        /// ページ挿入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PageAdd(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 最大１０ページ挿入可能
            if (this.tbc_MitsumoriMeisai.TabPages.Count == this.maxTabPage)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // ページは挿入できません(最大{1}ページ)。
                msgLogic.MessageBoxShow("E065", this.maxTabPage.ToString());
                return;
            }

            // ページ追加
            this.PageAdd();

            // ROW_NOを採番
            if (!this.logic.CreateDenpyoNumber()) { return; }

            // 201400708 syunrei ＃947　№11　　start
            this.logic.SetMeisaiZeiKbnCtr(false);
            // 201400708 syunrei ＃947　№11　　end
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ページ挿入処理
        /// <summary>
        /// ページ挿入処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PageAdd()
        {
            LogUtility.DebugMethodStart();


            // 初期化
            this.tbp_MitsumoriMeisai = new System.Windows.Forms.TabPage();

            // 最初のページ
            if (this.tbc_MitsumoriMeisai.TabPages.Count == 0)
            {
                // ページ名設定
                this.tbp_MitsumoriMeisai.Text = "ページ1";

                // ページ追加
                this.tbc_MitsumoriMeisai.Controls.Add(this.tbp_MitsumoriMeisai);

                // ユーザコントロール追加
                UserControl1 control = new UserControl1();
                control.Name = "control";

                // イベント定義
                control.CustomDataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.CustomDataGridView_RowsAdded);
                control.CustomDataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.CustomDataGridView_RowsRemoved);
                control.CustomDataGridView.CellValueChanged += new DataGridViewCellEventHandler(this.CustomDataGridView_CellValueChanged);
                control.CustomDataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(this.CustomDataGridView_CellValidating);
                control.CustomDataGridView.CellEnter += new DataGridViewCellEventHandler(this.CustomDataGridView_CellEnter);
                tbc_MitsumoriMeisai.Click += new System.EventHandler(this.tbp_MitsumoriMeisai_Click);

                // tabコントロール追加
                tbc_MitsumoriMeisai.TabPages[this.tbc_MitsumoriMeisai.SelectedIndex].Controls.Add(control);

                // 新規追加したPageTabを選択
                this.tbc_MitsumoriMeisai.SelectedTab = tbc_MitsumoriMeisai.TabPages[0];

            }
            else
            {
                // ページ名設定
                this.tbp_MitsumoriMeisai.Text = "ページ" + (tbc_MitsumoriMeisai.TabPages.Count + 1).ToString();

                //ページ追加
                this.tbc_MitsumoriMeisai.Controls.Add(this.tbp_MitsumoriMeisai);

                // ユーザコントロール追加
                UserControl1 control = new UserControl1();
                control.Name = "control";

                // イベント定義
                control.CustomDataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(this.CustomDataGridView_RowsAdded);
                control.CustomDataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.CustomDataGridView_RowsRemoved);
                control.CustomDataGridView.CellValueChanged += new DataGridViewCellEventHandler(this.CustomDataGridView_CellValueChanged);
                control.CustomDataGridView.CellValidating += new DataGridViewCellValidatingEventHandler(this.CustomDataGridView_CellValidating);
                control.CustomDataGridView.CellEnter += new DataGridViewCellEventHandler(this.CustomDataGridView_CellEnter);
                tbc_MitsumoriMeisai.Click += new System.EventHandler(this.tbp_MitsumoriMeisai_Click);

                // tabコントロール追加
                tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.TabPages.Count - 1].Controls.Add(control);

                // 新規追加したPageTabを選択
                this.tbc_MitsumoriMeisai.SelectedTab = tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.TabPages.Count - 1];

            }

            // ページ名
            string pageName;
            // ページタイトル
            string pageTitle;

            // ページを再構成する
            for (int i = 0; i < tbc_MitsumoriMeisai.TabPages.Count; i++)
            {

                // ページ名の設定
                pageName = "tabPage" + (i + 1).ToString();
                // ページタイトルの設定
                pageTitle = "ページ" + (i + 1).ToString();
                // ページ名の設定
                tbc_MitsumoriMeisai.TabPages[i].Name = pageName;
                // ページ名(表示)の設定
                tbc_MitsumoriMeisai.TabPages[i].Text = pageTitle;
            }

            // 一覧を初期化する
            this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[this.tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];

            // 現在のタブコントロールのサイズに合わせる
            this.MeisaiAncher();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ページ削除イベント
        /// <summary>
        /// ページ削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PageDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool catchErr = false;
            this.PageDelete(out catchErr);
            if (catchErr) { return; }

            // ROW_NOを採番
            this.logic.CreateDenpyoNumber();

            // 小計の計算
            this.logic.Calculate(string.Empty);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ページ削除処理
        /// <summary>
        /// ページ削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PageDelete(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            try
            {
                // ページ名
                string pageName;
                // ページタイトル
                string pageTitle;


                // ページが1枚以上ある場合
                if (tbc_MitsumoriMeisai.TabPages.Count == 1)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    // ページは削除できません
                    msgLogic.MessageBoxShow("E066", "1");
                    base.RegistErrorFlag = true;

                    return;
                }

                // ページが2枚以上ある場合
                if (tbc_MitsumoriMeisai.TabPages.Count > 1)
                {
                    // ページ削除
                    tbc_MitsumoriMeisai.TabPages.Remove(tbc_MitsumoriMeisai.SelectedTab);
                }

                // ページ数分繰り返す
                for (int i = 0; i < tbc_MitsumoriMeisai.TabPages.Count; i++)
                {
                    // ページ名の設定
                    pageName = "tabPage" + (i + 1).ToString();
                    // ページタイトルの設定
                    pageTitle = "ページ" + (i + 1).ToString();
                    // ページ名の設定
                    tbc_MitsumoriMeisai.TabPages[i].Name = pageName;
                    //　ページ名(表示)の設定
                    tbc_MitsumoriMeisai.TabPages[i].Text = pageTitle;
                }
                this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];
            }
            catch (Exception ex)
            {
                LogUtility.Error("PageDelete", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                catchErr = true;
            }
            LogUtility.DebugMethodEnd(catchErr);
        }
        #endregion

        #region CellValueChangedイベント
        /// <summary>
        /// CellValueChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CustomDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // ロード場合、処理しない
                if (nowLoding)
                {
                    return;
                }
                // ロード場合、処理しない
                if (meisaiNowLoding)
                {
                    return;
                }
                // 伝票番号を作成時にこのイベント処理しない
                if (CreateDenpyoNumbering)
                {
                    return;
                }

                // 選択行番号取得
                int intRowNo = e.RowIndex;

                // 小計行は必須チェックしない。
                var shoukeiFlg = ((CustomDataGridView)sender).Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];
                if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
                {
                    ((DgvCustomAlphaNumTextBoxCell)this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).FocusOutCheckMethod = null;

                    return;
                }
                bExecuteCalc = false;

                // セル名称取得
                String cellname = this.CustomDataGridView.Columns[e.ColumnIndex].Name;
                bool catchErr = false;
                switch (cellname)
                {
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD:
                        bExecuteCalc = true;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_SUURYOU:
                        bExecuteCalc = true;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_KINGAKU:
                        bExecuteCalc = true;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA:
                        bExecuteCalc = true;
                        break;
                    case "clm_Shohizei1":
                        bExecuteCalc = true;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD:
                        bExecuteCalc = true;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD:
                        bExecuteCalc = true;
                        break;
                    default:
                        break;
                }


                //再計算対象の項目だった場合、金額の再計算を行う。
                if (bExecuteCalc && !cellValueChanging)
                {
                    // 処理開始(true)
                    cellValueChanging = true;

                    //再計算対象の項目だった場合、金額の再計算を行う。
                    this.logic.Calculate(cellname);

                    // 処理終了(false)
                    cellValueChanging = false;
                }


            }
            catch
            {
                // 処理終了(false)
                cellValueChanging = false;

                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region CellValidatingイベント
        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // ロード場合、処理しない
                if (nowLoding)
                {
                    return;
                }

                // ロード場合、処理しない
                if (meisaiNowLoding)
                {
                    return;
                }

                // 伝票番号を作成時にこのイベント処理しない
                if (CreateDenpyoNumbering)
                {
                    return;
                }


                // 選択行番号取得
                int intRowNo = e.RowIndex;

                // 小計行は必須チェックしない。
                var shoukeiFlg = this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];
                if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
                {
                    ((DgvCustomAlphaNumTextBoxCell)this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).FocusOutCheckMethod = null;
                    return;
                }

                // セル名称取得
                String cellname = this.CustomDataGridView.Columns[e.ColumnIndex].Name;

                switch (cellname)
                {
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD:
                        // 品名CDチェック
                        if (!this.logic.CheckHinmeiCd(intRowNo))
                        {
                            e.Cancel = true;
                            break;
                        }

                        // 品名CDの入力が無ければ処理中断
                        if (string.IsNullOrEmpty((string)this.CustomDataGridView.CurrentCell.Value))
                        {
                            break;
                        }

                        // 同一の品名コードが入力されてもポップアップを表示したいため伝票区分をクリア
                        var targetRow = this.CustomDataGridView.CurrentRow;
                        if (targetRow != null)
                        {
                            // 入力を変更せずにタブ遷移するだけの時にポップアップが上がらないようにするための対策
                            DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["clm_HinmeiCD1"];
                            if (string.IsNullOrEmpty(Convert.ToString(this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value)) || control.TextBoxChanged == true)
                            {
                                // 伝票区分をクリア
                                targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = string.Empty;

                                // 伝票区分選択ポップアップ表示
                                if (!this.logic.SetDenpyouKbn())
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    // 品名チェック
                                    bool catchErr = false;
                                    if (!this.logic.CheckHinmeiCd(intRowNo, 1, false, out catchErr))
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME:
                        // 品名チェック
                        if (!this.logic.CheckHinmeiName(intRowNo))
                        {
                            e.Cancel = true;
                        }
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD:
                        // 伝票区分チェック
                        if (!this.logic.checkDenpyouKbn(e))
                        {
                            e.Cancel = true;
                        }
                        break;
                    default:
                        break;
                }

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CellEnterイベント
        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // ロード場合、処理しない
                if (nowLoding)
                {
                    return;
                }

                // ロード場合、処理しない
                if (meisaiNowLoding)
                {
                    return;
                }

                // 伝票番号を作成時にこのイベント処理しない
                if (CreateDenpyoNumbering)
                {
                    return;
                }

                // 選択行情報取得
                int intRowNo = e.RowIndex;
                var CurrentRow = this.CustomDataGridView.CurrentRow;

                // 品名でPopup表示後処理追加
                if (this.CustomDataGridView.Columns[e.ColumnIndex].Name.Equals(MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD))
                {
                    // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                    DgvCustomAlphaNumTextBoxCell cell = (DgvCustomAlphaNumTextBoxCell)CurrentRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD];
                    cell.PopupAfterExecute = PopupAfter_HINMEI_CD;

                    if (CurrentRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value != null)
                    {
                        this.logic.befHinmeiCd = CurrentRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD].Value.ToString();
                    }
                    else
                    {
                        this.logic.befHinmeiCd = string.Empty;
                    }
                }

                // 20140714 syunrei EV002452_システムエラー　※ログ有り　start
                this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];
                // 20140714 syunrei EV002452_システムエラー　※ログ有り　end

                // 小計行は必須チェックしない。
                var shoukeiFlg = this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_SHOUKEI_FLG];
                if (shoukeiFlg.Value != null && shoukeiFlg.Value.ToString() == "True")
                {
                    ((DgvCustomAlphaNumTextBoxCell)this.CustomDataGridView.Rows[intRowNo].Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD]).FocusOutCheckMethod = null;
                    return;
                }

                // IME制御
                switch (this.CustomDataGridView.Columns[e.ColumnIndex].Name)
                {
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_CD:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_NAME:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_NAME:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_UNIT_CD:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_TANKA:
                        this.CustomDataGridView.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_NAME:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_TAX_SOTO:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_HINMEI_ZEI_KBN_CD:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_BIKOU:
                    case MitumorisyoConst.MITSUMORI_COLUMN_NAME_MEISAI_TEKIYOU:
                        this.CustomDataGridView.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region コンストラクタで渡された計量番号のデータ存在するかチェック
        /// <summary>
        /// コンストラクタで渡された計量番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistMitsumoriData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // 返却
                bool rtn;

                rtn = this.logic.IsExistMitsumoriData(this.mitsumoriNumber);

                LogUtility.DebugMethodEnd(rtn);

                return rtn;


            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region ポップアップ表示後イベント(取引先用)

        /// <summary>
        /// 取引先ポップアップから帰ってきた後のメソッド
        /// </summary>
        public void TorihikisakiPopUpAfter()
        {
            // ポップアップ入力なので手入力フラグをFalseに設定
            this.ManualInputFlg = false;

            bool catchErr = false;
            bool ret = this.txt_TorihikisakiCD_AfterUpdate(out catchErr);
            if (catchErr) { return; }
            if (!ret)
            {
                this.isInputError = true;
                this.txt_TorihikisakiCD.Focus();
            }
        }

        /// <summary>
        /// Validated処理(ポップアップ表示後でも使いたいのでメソッド化)
        /// </summary>
        public virtual bool txt_TorihikisakiCD_AfterUpdate(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                if (this.isInputError || this.beforTorihikisakiCD != this.txt_TorihikisakiCD.Text || this.beforTorihikisakiHikiai != this.txt_Torihikisaki_hikiai_flg.Text)
                {
                    this.isInputError = false;
                    ret = this.logic.CheckTorihikisaki(out catchErr);
                    if (catchErr) { return ret; }

                    //取引先マスタ検索
                    if (!this.logic.TorihikisakiLostFocus(txt_TorihikisakiCD.Text))
                    {
                        // 前回検索取引先コードを設定
                        this.beforTorihikisakiCD = this.txt_TorihikisakiCD.Text;

                        this.txt_TorihikisakiCD.Focus();
                    }
                    //取引先タブ画面表示設定
                    this.logic.setDataTorihikisakiTab(true);

                    // 品名チェック
                    this.logic.CheckAllHinmeiCd(out catchErr);
                    //データグリットの情報を選択しているデータに戻す
                    this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];
                    if (catchErr) { return ret; }

                    // 金額の再計算を行う。
                    if (!this.logic.Calculate(string.Empty))
                    {
                        catchErr = true;
                        return ret;
                    }
                }

                // 前回検索取引先コードを設定
                this.beforTorihikisakiCD = this.txt_TorihikisakiCD.Text;
                this.beforTorihikisakiHikiai = this.txt_Torihikisaki_hikiai_flg.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("txt_TorihikisakiCD_AfterUpdate", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }

            return ret;
        }
        #endregion

        #region 取引先のValidatingイベント
        /// <summary>
        /// 取引先のValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_TorihikisakiCD_Validating(object sender, CancelEventArgs e)
        {
            // 入力が変更された場合
            if (!this.ManualInputFlg &&
                this.beforTorihikisakiCD != this.txt_TorihikisakiCD.Text)
            {
                this.ManualInputFlg = true;
                txt_Torihikisaki_hikiai_flg.Text = "0";
            }

            // 取引先名称クリア
            //if (!this.logic.torihikisakiNameChangedFlg)
            //{
            //    this.txt_TorihikisakiMei.Text = string.Empty;
            //}

            bool catchErr = false;
            bool isSetTori = this.txt_TorihikisakiCD_AfterUpdate(out catchErr);
            if (catchErr) { return; }
            if (!isSetTori)
            {
                // 取引先拠点チェックがエラーだったらここのエラーは表示しない
                if (!this.logic.torihikisakiAndKyotenErrorFlg)
                {
                    // 取引先チェックで取引先ＣＤが正常と判断されない場合はValidatingイベントをキャンセル
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                }
                e.Cancel = true;
                this.isInputError = true;
            }

            this.logic.torihikisakiAndKyotenErrorFlg = false;
        }
        #endregion

        #region ポップアップ表示後イベント(業者用)
        /// <summary>
        /// 業者先ポップアップから帰ってきた後のメソッド
        /// </summary>
        public void GyoushaPopUpAfter()
        {
            // ポップアップ入力なので手入力フラグをFalseに設定
            this.ManualInputFlg = false;
            bool catchErr = false;
            bool ret = this.txt_GyoushaCD_AfterUpdate(out catchErr);
            if (catchErr) { return; }
            if (!ret)
            {
                this.isInputError = true;
                this.txt_GyoushaCD.Focus();
            }
        }

        /// <summary>
        /// 業者 PopupBefore
        /// </summary>
        public void GyoushaPopupBefore()
        {
            this.beforGyousaCD = this.txt_GyoushaCD.Text;
        }

        /// <summary>
        /// Validated処理(ポップアップ表示後でも使いたいのでメソッド化)
        /// </summary>
        public virtual bool txt_GyoushaCD_AfterUpdate(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                if (this.isInputError || this.beforGyousaCD != this.txt_GyoushaCD.Text || this.beforGyoushaHikiai != this.txt_Gyousha_hikiai_flg.Text)
                {
                    this.isInputError = false;
                    // 業者CDが前回値と変わったら、現場をクリア
                    if (this.beforGyousaCD != this.txt_GyoushaCD.Text)
                    {
                        this.txt_GenbaCD.Text = string.Empty;
                        this.txtGenbaName.Text = string.Empty;
                        this.txtGenbaName.ReadOnly = true;
                        //
                        this.txt_Genba_hikiai_flg.Text = string.Empty;
                        this.txt_Genba_shokuchi_kbn.Text = string.Empty;
                        //
                        //this.logic.dto.gyoushaEntry = null;
                        this.logic.dto.gyoushaEntry = new r_framework.Entity.M_GYOUSHA();
                        this.logic.dto.hikiaiGyoushaEntry = null;
                        this.logic.dto.genbaEntry = null;
                        this.logic.dto.hikiaiGenbaEntry = null;
                    }
                }
                else
                {
                    return true;
                }

                //20151103 hoanghm #2189 start
                //this.beforGyousaCD = string.Empty;
                //20151103 hoanghm #2189 end

                ret = this.logic.CheckGyousha(out catchErr);
                if (catchErr) { return ret; }

                // 業者マスタ検索
                if (!this.logic.GyoushaCD_LostFocus(this.txt_GyoushaCD.Text))
                {
                    // 前回検索業者コードを設定
                    this.beforGyousaCD = this.txt_GyoushaCD.Text;

                    this.txt_GyoushaCD.Focus();

                    if (!this.logic.gyoushaNameChangedFlg)
                    {
                        this.txt_GyoushaName.Text = string.Empty;
                    }
                    return false;
                }

                //業者タブ画面表示設定
                this.logic.setDataGyoushaTab(true);

                // 前回検索取引先コードを設定
                this.beforGyousaCD = this.txt_GyoushaCD.Text;
                this.beforGyoushaHikiai = this.txt_Gyousha_hikiai_flg.Text;

                if (this.txt_GyoushaCD.BackColor.Equals(System.Drawing.Color.Aqua))
                {
                    this.txt_GyoushaCD.BackColor = System.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txt_GyoushaCD_AfterUpdate", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }

            return ret;
        }
        #endregion

        #region 業者のValidatingイベント
        /// <summary>
        /// 業者のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_GyoushaCD_Validating(object sender, CancelEventArgs e)
        {
            // 取引先拠点チェックがエラーだったらここのエラーは表示しない
            if (!this.logic.torihikisakiAndKyotenErrorFlg)
            {
                //if (this.beforGyousaCD == this.txt_GyoushaCD.Text)
                //{
                //    return;
                //}

                // 入力が変更された場合
                if (!this.ManualInputFlg &&
                    this.beforGyousaCD != this.txt_GyoushaCD.Text)
                {
                    // 手入力フラグをTRUEに設定
                    this.ManualInputFlg = true;
                }

                // 業者情報クリア
                //if (!this.logic.gyoushaNameChangedFlg)
                //{
                //    this.txt_GyoushaName.Text = string.Empty;
                //    this.txt_Gyousha_hikiai_flg.Text = string.Empty;
                //    this.txt_Gyousha_shokuchi_kbn.Text = string.Empty;
                //}

                bool catchErr = false;
                bool isSetGyousha = this.txt_GyoushaCD_AfterUpdate(out catchErr);
                if (catchErr) { return;}
                if (!isSetGyousha)
                {
                    // 取引先拠点チェックがエラーだったらここのエラーは表示しない
                    if (!this.logic.torihikisakiAndKyotenErrorFlg)
                    {
                        // 業者チェックで業者ＣＤが正常と判断されない場合はValidatingイベントをキャンセル
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                    }
                    e.Cancel = true;
                    this.isInputError = true;
                }
            }
        }

        #endregion

        #region ポップアップ表示後イベント(現場用)

        /// <summary>
        /// 現場ポップアップから帰ってきた後のメソッド
        /// </summary>
        public void GenbaPopUpAfter()
        {
            // ポップアップ入力なので手入力フラグをFalseに設定
            this.ManualInputFlg = false;
            this.ManualInputGenbaFlg = false;

            bool catchErr = false;
            bool ret = this.txt_GenbaCD_AfterUpdate(out catchErr);
            if (catchErr) { return; }
            if (!ret)
            {
                this.isInputError = true;
                this.txt_GenbaCD.Focus();
            }
        }

        /// <summary>
        /// 現場 PopUpBefore
        /// </summary>
        public void GenbaPopUpBefore()
        {
            this.beforGyousaCD = this.txt_GyoushaCD.Text;
            this.beforeGenbaCD = this.txt_GenbaCD.Text;
        }

        /// <summary>
        /// Validated処理(ポップアップ表示後でも使いたいのでメソッド化)
        /// </summary>
        public virtual bool txt_GenbaCD_AfterUpdate(out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                if (this.isInputError || this.beforGyousaCD != this.txt_GyoushaCD.Text
                    || this.beforGyoushaHikiai != this.txt_Gyousha_hikiai_flg.Text
                    || this.beforeGenbaCD != this.txt_GenbaCD.Text
                    || this.beforeGenbaHikiai != this.txt_Genba_hikiai_flg.Text)
                {
                    this.isInputError = false;
                    ret = this.logic.CheckGenba(out catchErr);
                    if (catchErr) { return ret; }

                    // 現場マスタ検索
                    if (!this.logic.GenbaCD_LostFocus(this.txt_GyoushaCD.Text, this.txt_GenbaCD.Text))
                    {
                        // 前回検索取引先コードを設定
                        this.beforeGenbaCD = this.txt_GenbaCD.Text;

                        this.txt_GenbaCD.Focus();

                        // 前回検索取引先コードを設定
                        this.beforeGenbaCD = string.Empty;

                        this.txtGenbaName.Text = string.Empty;

                        this.isInputError = true;
                        return ret;
                    }

                    //現場タブ画面表示設定
                    this.logic.setDataGenbaTab(true);

                    // 業者マスタ検索を現場入力から呼び出す場合は、
                    // ポップアップから入力されたときと同じ動きにしたいため、
                    // 手入力フラグをFalseに設定。
                    this.ManualInputFlg = false;

                    // 業者マスタ検索
                    this.txt_GyoushaCD_AfterUpdate(out catchErr);
                    if (catchErr) { return ret; }

                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                    this.logic.SetTorihiksiakiInfoByGenba(out catchErr);
                    if (catchErr) { return ret; }
                    // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                }
                else
                {
                    return ret;
                }

                // 前回検索取引先コードを設定
                this.beforeGenbaCD = this.txt_GenbaCD.Text;
                this.beforeGenbaHikiai = this.txt_Genba_hikiai_flg.Text;
                if (this.txt_GenbaCD.BackColor.Equals(System.Drawing.Color.Aqua))
                {
                    this.txt_GenbaCD.BackColor = System.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txt_GenbaCD_AfterUpdate", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                catchErr = true;
            }
            return ret;
        }
        #endregion

        #region 現場のValidatingイベント
        /// <summary>
        /// 現場のValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txt_GenbaCD_Validating(object sender, CancelEventArgs e)
        {
            // 取引先拠点チェックがエラーだったらここのエラーは表示しない
            if (!this.logic.torihikisakiAndKyotenErrorFlg)
            {
                this.ManualInputGenbaFlg = true;

                // 入力が変更された場合
                if (!this.ManualInputFlg &&
                    this.beforeGenbaCD != this.txt_GenbaCD.Text)
                {
                    // 手入力フラグをTRUEに設定
                    this.ManualInputFlg = true;
                }

                // 現場名称クリア
                //if (!this.logic.genbaNameChangedFlg)
                //{
                //    this.txtGenbaName.Text = string.Empty;
                //}

                bool catchErr = false;
                bool isSetGenba = this.txt_GenbaCD_AfterUpdate(out catchErr);
                if (catchErr) { return;}
                if (!isSetGenba)
                {
                    // エラー区分によってエラーを切り替え
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (this.logic.gyousyaGenbaErrorKbn == 1)
                    {
                        this.txt_GenbaCD.Focus();
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        msgLogic.MessageBoxShow("E051", "業者");
                        // 20150915 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                        this.ManualInputFlg = true;
                        this.txt_GenbaCD.Text = string.Empty;
                        this.beforeGenbaCD = string.Empty;
                        this.isInputError = true;
                    }
                    else if (this.logic.gyousyaGenbaErrorKbn == 2)
                    {
                        this.txt_GenbaCD.Focus();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.ManualInputFlg = true;
                        this.beforeGenbaCD = string.Empty;
                        this.isInputError = true;
                    }

                    // 現場チェックで現場ＣＤが正常と判断されない場合はValidatingイベントをキャンセル
                    if (this.logic.gyousyaGenbaErrorKbn != 0)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }


        #endregion

        #region 見積日付のEnterイベント
        /// <summary>見積日付前回値チェック用</summary>
        private DateTime beforeMitsumoridate = DateTime.Now;

        /// <summary>
        /// 見積日付 Enter Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_MitsumoriDate_Enter(object sender, EventArgs e)
        {
            DateTime tempDate = DateTime.Now;
            if (this.dtp_MitsumoriDate.Value != null && DateTime.TryParse(this.dtp_MitsumoriDate.Value.ToString(), out tempDate))
            {
                this.beforeMitsumoridate = tempDate;
            }
        }
        #endregion

        #region 見積日付のロストフォーカスイベント
        /// <summary>
        /// 見積日付のロストフォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_MitsumoriDate_LostFocus(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.dtp_MitsumoriDate_LostFocus(sender, e);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 見積日付の変更イベント
        /// <summary>
        /// 見積日付の変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_MitsumoriDate_ValueChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 前回値チェックをしないと明細の再計算が実行されてしまう。
            DateTime tempDate = DateTime.Now;
            if (DateTime.TryParse(this.dtp_MitsumoriDate.Value.ToString(), out tempDate)
                && DateTime.Compare(tempDate, this.beforeMitsumoridate) != 0)
            {

                this.logic.dtp_MitsumoriDate_TextChanged(sender, e);
                // 金額の再計算を行う。
                this.logic.Calculate(string.Empty);

            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region tabPageクリックイベント
        /// <summary>
        /// tabPageクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbp_MitsumoriMeisai_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // CustomDataGridViewを毎回切り替える
            this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];

            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                Type type = sender.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)sender;
                    // テキスト名を取得
                    String textName = this.logic.GetTextName(ctrl.Name);
                    Control control = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = control.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, control.Text);
                    }

                    // イベント削除
                    control.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomDateTimePicker")
                {
                    CustomDateTimePicker ctrl = (CustomDateTimePicker)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region ユーティリティ
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            try
            {
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm));

                return allControl.ToArray();
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 画面遷移するイベントかどうかチェック
        /// 以下の操作の場合に画面遷移すると判断する。
        /// 　・一覧ボタンクリック(F7)
        /// 　・閉じるボタンクリック(F12)
        /// </summary>
        /// <returns></returns>
        private bool isChangeScreenEvent()
        {
            LogUtility.DebugMethodStart();

            try
            {
                bool returnVal = false;
                if (this.ActiveControl == null
                    && this.ParentBaseForm.ActiveControl != null
                    && -1 < Array.IndexOf(controlNamesForChangeScreenEvents, this.ParentBaseForm.ActiveControl.Name))
                {
                    returnVal = true;
                }

                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 税計算区分イベント
        /// <summary>
        /// 税計算区分イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ZeiKeisanKbnCD_TextChanged(object sender, EventArgs e)
        {
            if (changeNewModeNowLoding)
            {
                return;
            }
            // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　start
            if (this.txt_ZeiKeisanKbnCD.Text.Equals("1"))
            {
                if (!this.logic.SetZeiKbnCtr(true)) { return; }
            }
            else
            {
                if (!this.logic.SetZeiKbnCtr(false)) { return; }
            }

            if (!this.logic.SetGamenZeiKbn(this.txt_ZeiKeisanKbnCD.Text)) { return; }
            // 201400704 syunrei EV002427_見積入力の税計算区分がシステム設定の税区分締処理利用形態を参照していない　end
            // 金額の再計算を行う。
            this.logic.Calculate(string.Empty);
        }
        #endregion

        #region 税区分イベント
        /// <summary>
        /// 税区分イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ZeiKbnCD_TextChanged(object sender, EventArgs e)
        {
            if (changeNewModeNowLoding)
            {
                return;
            }
            // 20140718 katen No.5323 品名「000002.金属くず」をセットすると税区分を非課税にセットしても消費税を計上してしまう start‏
            // ページ
            for (int page = 0; page < this.tbc_MitsumoriMeisai.TabPages.Count; page++)
            {
                // CustomDataGridViewを取得する
                CustomDataGridView cdgv = ((CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"]);
                //データグリットの情報を処理中のpage情報でセットする
                this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[page].Controls["control"].Controls["CustomDataGridView"];

                // 明細に対して、計算を行う
                for (int i = 0; i < cdgv.Rows.Count - 1; i++)
                {
                    this.logic.SetZeiKbn(i);
                }
            }

            //データグリットの情報を選択中の情報でセットする
            this.CustomDataGridView = (CustomDataGridView)this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Controls["CustomDataGridView"];

            // 20140718 katen No.5323 品名「000002.金属くず」をセットすると税区分を非課税にセットしても消費税を計上してしまう end‏
            // 金額の再計算を行う。
            this.logic.Calculate(string.Empty);
        }
        #endregion

        #region 営業担当者更新後イベント
        /// <summary>
        /// 営業担当者更新後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void txt_ShainCD_OnValidated(object sender, EventArgs e)
        {
            this.logic.CheckNyuuryokuTantousha();
        }
        #endregion


        /// <summary>
        /// 取引先CDへフォーカス移動する
        /// 取引先CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToTorihikisakiCd()
        {
            this.txt_TorihikisakiCD.Focus();
        }

        /// <summary>
        /// 業者CDへフォーカス移動する
        /// 業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGyoushaCd()
        {
            this.txt_GyoushaCD.Focus();
        }

        /// <summary>
        /// 現場CDへフォーカス移動する
        /// 現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGenbaCd()
        {
            this.txt_GenbaCD.Focus();
        }

        private void MitsumoriNyuryokuForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.logic.SetMoveData();
        }

        /// <summary>
        /// 取引先CDエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TorihikisakiCD_Enter(object sender, EventArgs e)
        {
            this.beforTorihikisakiCD = this.txt_TorihikisakiCD.Text;
            this.beforTorihikisakiHikiai = this.txt_Torihikisaki_hikiai_flg.Text;
            this.beforeValue = this.txt_TorihikisakiCD.Text;
        }

        /// <summary>
        /// 業者CDエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GyoushaCD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = this.txt_GyoushaCD.Text;
            this.beforGyoushaHikiai = this.txt_Gyousha_hikiai_flg.Text;
            this.beforeValue = this.txt_GyoushaCD.Text;
        }

        /// <summary>
        /// 現場CDエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GenbaCD_Enter(object sender, EventArgs e)
        {
            this.beforeGenbaCD = this.txt_GenbaCD.Text;
            //this.beforGyousaCD = this.txt_GyoushaCD.Text;
            this.beforeGenbaHikiai = this.txt_Genba_hikiai_flg.Text;
            this.beforeValue = this.txt_GenbaCD.Text;
        }

        /// <summary>
        /// 取引先ポップアップの検索条件を拠点の有無によって切り替えます
        /// （「取引先CD」と「取引先虫眼鏡ボタン」のPopupBeforeExecuteMethodプロパティで使用しています）
        /// </summary>
        public void torihikisakiSearchConditionSwitching()
        {
            // 初期化
            this.txt_TorihikisakiCD.PopupSearchSendParams.Clear();
            this.btn_TorihikisakieSearch.PopupSearchSendParams.Clear();
            PopupSearchSendParamDto torihikisakiCdPopUpCondition = new PopupSearchSendParamDto();

            // 拠点CDで取引先ポップアップの検索結果を絞り込む
            var subCondition = new r_framework.Dto.PopupSearchSendParamDto();
            torihikisakiCdPopUpCondition.And_Or = CONDITION_OPERATOR.AND;

            // 拠点CDが空じゃない場合
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                // SubCondition[1]
                subCondition.And_Or = CONDITION_OPERATOR.AND;
                subCondition.Control = this.headerForm.KYOTEN_CD.Name;
                subCondition.KeyName = "TORIHIKISAKI_KYOTEN_CD";
                torihikisakiCdPopUpCondition.SubCondition.Add(subCondition);

                // SubCondition[2]
                subCondition = new r_framework.Dto.PopupSearchSendParamDto();
                subCondition.And_Or = CONDITION_OPERATOR.OR;
                subCondition.KeyName = "TORIHIKISAKI_KYOTEN_CD";
                subCondition.Value = "99";
                torihikisakiCdPopUpCondition.SubCondition.Add(subCondition);
            }

            // SubCondition
            subCondition = new r_framework.Dto.PopupSearchSendParamDto();
            subCondition.And_Or = CONDITION_OPERATOR.AND;
            subCondition.Control = "dtp_MitsumoriDate";
            subCondition.KeyName = "TEKIYOU_BEGIN";
            this.txt_TorihikisakiCD.PopupSearchSendParams.Add(subCondition);
            this.btn_TorihikisakieSearch.PopupSearchSendParams.Add(subCondition);

            // 取引先CDテキストボックスと取引先検索用虫眼鏡ボタンに検索条件を追加
            this.txt_TorihikisakiCD.PopupSearchSendParams.Add(torihikisakiCdPopUpCondition);
            this.btn_TorihikisakieSearch.PopupSearchSendParams.Add(torihikisakiCdPopUpCondition);
        }

        /// <summary>
        /// 品名設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_HINMEI_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var targetRow = this.CustomDataGridView.CurrentRow;
                if (targetRow != null)
                {
                    // 伝票区分をクリア
                    targetRow.Cells[MitumorisyoConst.MITSUMORI_COLUMN_NAME_DENPYOU_KBN_CD].Value = string.Empty;

                    // 値が変更されたフラグを立てる
                    ((DgvCustomTextBoxCell)targetRow.Cells["clm_HinmeiCD1"]).TextBoxChanged = true;
                }
            }
        }

        /// <summary>
        /// タブコントロールのサイズチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_MitsumoriMeisai_SizeChanged(object sender, EventArgs e)
        {
            this.MeisaiAncher();
        }

        /// <summary>
        /// タブコントロールのセレクトインデックスチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbc_MitsumoriMeisai_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MeisaiAncher();
        }

        /// <summary>
        /// 明細欄のサイズをタブコントロールのサイズに応じて変化させます
        /// </summary>
        internal bool MeisaiAncher()
        {
            bool ret = true;
            try
            {
                // 現在のタブコントロールのサイズに明細欄のサイズを合わせる
                if (tbc_MitsumoriMeisai.TabPages.Count >= 1)
                {
                    this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Controls["control"].Size = this.tbc_MitsumoriMeisai.TabPages[tbc_MitsumoriMeisai.SelectedIndex].Size;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MeisaiAncher", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 見積書種類CDのテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MITSUMORISYURUI_CD_TextChanged(object sender, EventArgs e)
        {
            // 見積書種類CDチェック
            this.logic.MitsumoriSyuruiCheck();
        }

        #region 取引先・業者・現場の名称エンターイベント
        /// <summary>
        /// 取引先名称のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TorihikisakiMei_Enter(object sender, EventArgs e)
        {
            // ReadOnly以外の場合
            if (!this.txt_TorihikisakiMei.ReadOnly)
            {
                // 取引先名称の前回値を格納
                this.logic.beforeTorihikisakiName = this.txt_TorihikisakiMei.Text;
            }
        }

        /// <summary>
        /// 業者名称のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GyoushaName_Enter(object sender, EventArgs e)
        {
            // ReadOnly以外の場合
            if (!this.txt_GyoushaName.ReadOnly)
            {
                // 業者名称の前回値を格納
                this.logic.beforeGyoushaName = this.txt_GyoushaName.Text;
            }
        }

        /// <summary>
        /// 現場名称のエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void txtGenbaName_Enter(object sender, EventArgs e)
        {
            // ReadOnly以外の場合
            if (!this.txtGenbaName.ReadOnly)
            {
                // 現場名称の前回値を格納
                this.logic.beforeGenbaName = this.txtGenbaName.Text;
            }
        }
        #endregion 取引先・業者・現場の名称エンターイベント

        #region 取引先・業者・現場の名称バリデーティングイベント
        /// <summary>
        /// 取引先名称のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TorihikisakiMei_Validating(object sender, CancelEventArgs e)
        {
            // 前回値（エンターイベント時）と比較して値変更があった場合
            if (this.logic.beforeTorihikisakiName != this.txt_TorihikisakiMei.Text)
            {
                // 取引先名称が変更されたのでフラグを立てる
                this.logic.torihikisakiNameChangedFlg = true;
            }
        }

        /// <summary>
        /// 業者名称のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GyoushaName_Validating(object sender, CancelEventArgs e)
        {
            // 前回値（エンターイベント時）と比較して値変更があった場合
            if (this.logic.beforeGyoushaName != this.txt_GyoushaName.Text)
            {
                // 業者名称が変更されたのでフラグを立てる
                this.logic.gyoushaNameChangedFlg = true;
            }
        }

        /// <summary>
        /// 現場名称のバリデーティングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGenbaName_Validating(object sender, CancelEventArgs e)
        {
            // 前回値（エンターイベント時）と比較して値変更があった場合
            if (this.logic.beforeGenbaName != this.txtGenbaName.Text)
            {
                // 業者名称が変更されたのでフラグを立てる
                this.logic.genbaNameChangedFlg = true;
            }
        }
        #endregion 取引先・業者・現場の名称バリデーティングイベント

        #region 取引先CD・業者CD・現場CDテキストチェンジイベント
        /// <summary>
        /// 取引先CDのテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TorihikisakiCD_TextChanged(object sender, EventArgs e)
        {
            // 取引先CDに変更があったらフラグを初期化する
            this.logic.torihikisakiNameChangedFlg = false;
        }

        /// <summary>
        /// 業者CDのテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GyoushaCD_TextChanged(object sender, EventArgs e)
        {
            // 業者CDに変更があったらフラグを初期化する
            this.logic.gyoushaNameChangedFlg = false;
        }

        /// <summary>
        /// 現場CDのテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GenbaCD_TextChanged(object sender, EventArgs e)
        {
            // 現場CDに変更があったらフラグを初期化する
            this.logic.genbaNameChangedFlg = false;
        }
        #endregion 取引先CD・業者CD・現場CDテキストチェンジイベント

        /// <summary>
        /// 受注チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chec_Jyuchu_CheckedChanged(object sender, EventArgs e)
        {
            // 受注にチェックがついた場合
            if (this.chk_Jyuchu.Checked)
            {
                this.txt_JykyoFlg.Text = "1";

                // 失注にもチェックがついている場合
                if (this.chk_sichu.Checked)
                {
                    // 失注のチェックを外す
                    this.chk_sichu.Checked = false;
                }
            }
            else
            {
                // チェックが外されたら日付をクリア
                this.dtp_JuchuDate.Text = string.Empty;
                this.dtp_JuchuDate.IsInputErrorOccured = false;
            }

            // 受注/失注 両方のチェックが外されている場合
            if (!this.chk_Jyuchu.Checked && !this.chk_sichu.Checked)
            {
                this.txt_JykyoFlg.Text = string.Empty;
            }
        }

        /// <summary>
        /// 失注チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chec_sichu_CheckedChanged(object sender, EventArgs e)
        {
            // 失注にチェックがついた場合
            if (this.chk_sichu.Checked)
            {
                this.txt_JykyoFlg.Text = "2";

                // 受注にもチェックがついている場合
                if (this.chk_Jyuchu.Checked)
                {
                    // 受注のチェックを外す
                    this.chk_Jyuchu.Checked = false;
                }
            }
            else
            {
                // チェックが外されたら日付をクリア
                this.dtp_SichuDate.Text = string.Empty;
                this.dtp_SichuDate.IsInputErrorOccured = false;
            }

            // 受注/失注 両方のチェックが外されている場合
            if (!this.chk_Jyuchu.Checked && !this.chk_sichu.Checked)
            {
                this.txt_JykyoFlg.Text = string.Empty;
            }
        }

        /// <summary>
        /// 状況CDのテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_JykyoFlg_TextChanged(object sender, EventArgs e)
        {
            // 状況CDが１の場合
            if (this.txt_JykyoFlg.Text == "1")
            {
                this.chk_Jyuchu.Checked = true;
                this.chk_sichu.Checked = false;
            }
            // 状況CDが２の場合
            else if (this.txt_JykyoFlg.Text == "2")
            {
                this.chk_Jyuchu.Checked = false;
                this.chk_sichu.Checked = true;
            }
            else
            {
                this.chk_Jyuchu.Checked = false;
                this.chk_sichu.Checked = false;
            }
        }

        //20250411
        private void BIKO_KBN_CD_Validating(object sender, CancelEventArgs e)
        {
            if (this.logic.BikoKbnCdValidating(sender, e))
            {
                return;
            }
        }

        //20250414
        private void BIKO_KBN_CD_Validated(object sender, EventArgs e)
        {
            this.logic.BikoKBNCdValidated();
        }

        private void btn_GyoSonyu_Click(object sender, EventArgs e)
        {
            if (Ichiran.CurrentRow == null)
            {
                return;
            }

            int index = Ichiran.CurrentRow.Index;
            Ichiran.Rows.Insert(index, 1);
            Ichiran.CurrentCell = Ichiran.Rows[index].Cells[0];
        }

        private void btn_GyoSakuyo_Click(object sender, EventArgs e)
        {
            if (Ichiran.CurrentRow == null)
            {
                return;
            }

            int index = Ichiran.CurrentRow.Index;
            if (!Ichiran.Rows[index].IsNewRow)
            {
                Ichiran.Rows.RemoveAt(index);
            }
        }

        private void btn_UeIdo_Click(object sender, EventArgs e)
        {
            if (Ichiran.CurrentRow == null || Ichiran.CurrentRow.IsNewRow)
            {
                return;
            }

            int index = Ichiran.CurrentRow.Index;
            if (index > 0)
            {
                SwapRows(index, index - 1);
                Ichiran.CurrentCell = Ichiran.Rows[index - 1].Cells[0];
            }
        }

        private void btn_ShimoIdo_Click(object sender, EventArgs e)
        {
            if (Ichiran.CurrentRow == null || Ichiran.CurrentRow.IsNewRow)
            {
                return;
            }

            int index = Ichiran.CurrentRow.Index;
            if (index < Ichiran.Rows.Count - 2)
            {
                SwapRows(index, index + 1);
                Ichiran.CurrentCell = Ichiran.Rows[index + 1].Cells[0];
            }
        }

        private void SwapRows(int i, int j)
        {
            for (int col = 0; col < Ichiran.Columns.Count; col++)
            {
                var tmp = Ichiran.Rows[i].Cells[col].Value;
                Ichiran.Rows[i].Cells[col].Value = Ichiran.Rows[j].Cells[col].Value;
                Ichiran.Rows[j].Cells[col].Value = tmp;
            }
        }

        //20250415
        public void dgvBiko_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //var cell = this.dgvBiko.CurrentCell;

            //if (cell != null && this.dgvBiko.Columns[cell.ColumnIndex].Name.Equals("BIKO_CD"))
            //{
            //    //if (this.logic.DuplicationCheck())
            //    //{
            //    //    this.dgvBiko.Rows[cell.RowIndex].Cells["BIKO_NOTE"].Value = string.Empty;
            //    //    e.Cancel = true;
            //    //    return;
            //    //}

            //    this.logic.DgvBikoCellValidating(sender, e);
            //}
        }

        private void Ichiran_CellValidating(object sender, GrapeCity.Win.MultiRow.CellValidatingEventArgs e)
        {
            this.logic.Ichiran_CellValidating(sender, e);
        }
    }
}
