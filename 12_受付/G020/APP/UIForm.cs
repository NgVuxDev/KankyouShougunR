// $Id: UIForm.cs 56005 2015-07-17 06:49:19Z sanbongi $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
{
    /// <summary>
    /// 受付（クレーム）入力画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 受付番号
        /// </summary>
        public string UketsukeNumber { get; set; }

        /// <summary>
        /// 前回値チェック用変数(header用)
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// フォーカス設定コントロール格納
        /// </summary>
        private Control focusControl;

        /// <summary>
        /// キーイベントを保持用
        /// </summary>
        internal KeyEventArgs Key;

        /// <summary>
        /// フレームワークのフォーカス処理をするかしないか判断するフラグ
        /// </summary>
        internal bool isNotMoveFocusFW = false;

        /// <summary>
        /// 諸口区分の前回値を保持する
        /// </summary>
        internal bool oldShokuchiKbn;

        internal string beforeGyousha = string.Empty;
        internal string beforeGenba = string.Empty;
        internal string beforeGenbaName = string.Empty;

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
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isInputError = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">モード(WINDOW_TYPE)</param>
        /// <param name="UketsukeNumber">受付番号</param>
        public UIForm(WINDOW_TYPE windowType, String UketsukeNumber)
            : base(WINDOW_ID.T_UKETSUKE_COMPLAIN, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, UketsukeNumber);

                this.InitializeComponent();

                // 時間コンボボックスのItemsをセット
                this.UKETSUKE_DATE_HOUR.SetItems();
                this.UKETSUKE_DATE_MINUTE.SetItems(1);

                this.UketsukeNumber = UketsukeNumber;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
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

        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public UIForm(WINDOW_TYPE windowType, string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(windowType, string.Empty)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, torihikisakiCd, gyousyaCd, genbaCd);

                //データ移動用
                this.moveData_flg = true;
                this.moveData_torihikisakiCd = torihikisakiCd;
                this.moveData_gyousyaCd = gyousyaCd;
                this.moveData_genbaCd = genbaCd;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType, torihikisakiCd, gyousyaCd, genbaCd);
            }
        }
        #endregion

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                if (!this.logic.WindowInit()) { return; }
                //2013/12/3 削除　start
                //ParentBaseForm = this.Parent as BusinessBaseForm;
                //this.allControl = controlUtil.GetAllControls(ParentBaseForm);
                //2013/12/3 削除　end

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コンストラクタで渡された受付番号のデータ存在するかチェック
        /// <summary>
        /// コンストラクタで渡された受付番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistData(out bool catchErr)
        {
            bool ret = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                ret = this.logic.IsExistData(this.UketsukeNumber);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistData", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistData", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }
        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.logic.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

                return allControl.ToArray();
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

        #region KeyDownイベントを発生させます

        /// <summary>
        /// KeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.Key = e;
            base.OnKeyDown(e);
        }
        #endregion KeyDownイベントを発生させます

        #region イベント処理

        #region [F2]新規
        /// <summary>
        /// [F2]新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 新規モードに変更
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                //base.OnLoad(e);

                // 受付番号クリア
                this.UketsukeNumber = string.Empty;

                // 表示初期化
                this.logic.DisplayInit();

                // フォーカス設定
                //this.UKETSUKE_DATE.Focus();
                this.GYOUSHA_CD.Focus();
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

        #region [F3]修正
        /// <summary>
        /// [F3]修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeUpdateWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                
                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正モードに変更
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 参照モードに変更
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                //base.OnLoad(e);
                // 表示初期化
                this.logic.DisplayInit();

                this.GYOUSHA_CD.Focus();

                LogUtility.DebugMethodEnd(sender, e);
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

        #region [F7]一覧
        /// <summary>
        /// [F7]一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool retResult;
                //2014/01/28 修正 仕様変更 qiao start
                //伝票種類（「4」（クレーム設定）
                string denPyouSyurui = "4";
                // 受付一覧を表示
                retResult = FormManager.OpenFormWithAuth("G021", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.logic.headerForm.KYOTEN_CD.Text, denPyouSyurui);
                //2014/01/28 修正 仕様変更 qiao end
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

        #region [F9]登録
        /// <summary>
        /// [F9]登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 取引先と拠点の関係をチェック
                if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    if (false == this.logic.CheckTorihikisakiKyoten(this.logic.headerForm.KYOTEN_CD.Text, this.TORIHIKISAKI_CD.Text))
                    {
                        this.TORIHIKISAKI_CD.Focus();
                        this.TORIHIKISAKI_NAME.Text = string.Empty;
                        return;
                    }
                }


                //var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                //var torihikisakiEntity = dao.GetDataByCd(this.TORIHIKISAKI_CD.Text);
                //var kyotenCd = (int)torihikisakiEntity.TORIHIKISAKI_KYOTEN_CD;
                //if (99 != kyotenCd && Convert.ToInt16(this.logic.headerForm.KYOTEN_CD.Text) != kyotenCd)
                //{
                //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //    msgLogic.MessageBoxShow("E146");

                //    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                //    this.TORIHIKISAKI_CD.Focus();

                //    return;
                //}

                //// エラーの場合
                //if (base.RegistErrorFlag)
                //{
                //    // 処理中止
                //    return;
                //}

                var messageShowLogic = new MessageBoxShowLogic();

                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // 登録処理
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:

                        // エラーの場合
                        if (base.RegistErrorFlag)
                        {
                            // 必須チェックエラーフォーカス処理
                            this.logic.SetErrorFocus();
                            // 処理中止
                            return;
                        }
                        // データ登録
                        if (!this.logic.CreateEntitys())
                        {
                            return;
                        }
                        if (!this.logic.RegistData())
                        {
                            return;
                        }

                        messageShowLogic.MessageBoxShow("I001", "登録");

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // ベースフォームロード
                            //base.OnLoad(e);
                            // 画面初期化
                            this.logic.DisplayInit();
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        // エラーの場合
                        if (base.RegistErrorFlag)
                        {
                            // 必須チェックエラーフォーカス処理
                            this.logic.SetErrorFocus();
                            // 処理中止
                            return;
                        }

                        // データ更新
                        if (!this.logic.CreateEntitys())
                        {
                            return;
                        }
                        if (!this.logic.UpdateData())
                        {
                            return;
                        }
                        messageShowLogic.MessageBoxShow("I001", "更新");

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 【モードなし】モードに変更
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                            // ベースフォームロード
                            //base.OnLoad(e);

                            //受付番号クリア
                            this.UketsukeNumber = string.Empty;

                            // 画面初期化
                            this.logic.DisplayInit();
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 確認メッセージ
                        var result = this.logic.msgLogic.MessageBoxShow("C026");
                        if (result != DialogResult.Yes)
                        {
                            return;
                        }

                        // データ削除
                        if (!this.logic.CreateEntitys())
                        {
                            return;
                        }
                        if (this.logic.LogicalDeleteData())
                        {
                            // メッセージ通知
                            messageShowLogic.MessageBoxShow("I001", "削除");
                        }
                        else
                        {
                            return;
                        }

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 【新規】モードに変更
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            // ベースフォームロード
                            //base.OnLoad(e);
                            // 画面初期化
                            this.logic.DisplayInit();
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    default:
                        break;
                }

                // フィールドクリア
                this.logic.ClearFields();

                this.GYOUSHA_CD.Focus();
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

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();
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

        #endregion

        #region 受付番号の処理
        #region 受付番号「前」
        /// <summary>
        /// 受付番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_PREVIOUS_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                    !r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                String previousUketsukeNumber;
                String tableName = "T_UKETSUKE_CM_ENTRY";
                String fieldName = "UKETSUKE_NUMBER";
                String UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                // 前の受付番号を取得
                bool catchErr = true;
                previousUketsukeNumber = this.logic.GetPreviousNumber(tableName, fieldName, UketsukeNumber, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                //ThangNguyen [Add] 20150814 #11409 Start
                if (previousUketsukeNumber == "")
                {
                    this.logic.msgLogic.MessageBoxShow("E045");
                    return;
                }
                //ThangNguyen [Add] 20150814 #11409 End
                // 受付番号を設定
                this.UKETSUKE_NUMBER.Text = previousUketsukeNumber;
                // イベント削除
                //this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                // 受付番号更新後処理
                UKETSUKE_NUMBER_Validated(sender, e);
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

        #region 受付番号「次」
        /// <summary>
        /// 受付番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NEXT_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                    !r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                String nextUketsukeNumber;
                String tableName = "T_UKETSUKE_CM_ENTRY";
                String fieldName = "UKETSUKE_NUMBER";
                String UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                // 次の受付番号を取得
                bool catchErr = true;
                nextUketsukeNumber = this.logic.GetNextNumber(tableName, fieldName, UketsukeNumber, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                //ThangNguyen [Add] 20150814 #11409 Start
                if (nextUketsukeNumber == "")
                {
                    this.logic.msgLogic.MessageBoxShow("E045");
                    return;
                }
                //ThangNguyen [Add] 20150814 #11409 End
                // 受付番号を設定
                this.UKETSUKE_NUMBER.Text = nextUketsukeNumber;
                // イベント削除
                //this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                // 受付番号更新後処理
                UKETSUKE_NUMBER_Validated(sender, e);
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

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

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
                    //ctrl.Enter -= this.Control_Enter;
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
                    //ctrl.Enter -= this.Control_Enter;
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
                    //control.Enter -= this.Control_Enter;
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
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受付番号更新後処理
        /// <summary>
        /// 受付番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // イベント追加
                //this.UKETSUKE_NUMBER.Enter += this.Control_Enter;

                // 変更なしの場合
                if (this.dicControl.ContainsKey("UKETSUKE_NUMBER") &&
                    this.dicControl["UKETSUKE_NUMBER"].Equals(this.UKETSUKE_NUMBER.Text))
                {
                    return;
                }

                bool result = true;
                // 新規モード and 未入力　の場合
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && string.IsNullOrEmpty(this.UKETSUKE_NUMBER.Text))
                {
                    return;
                }
                else if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 受付番号をセット
                    this.UketsukeNumber = this.UKETSUKE_NUMBER.Text;

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                        !r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.logic.msgLogic.MessageBoxShow("E158", "修正");

                        // 受付番号の初期化
                        this.UketsukeNumber = string.Empty;
                        this.UKETSUKE_NUMBER.Text = this.UketsukeNumber;

                        bool isInit = this.logic.DisplayInit();

                        if (!isInit)
                        {
                            // イベント削除
                            this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                            // フォーカス設定
                            this.UKETSUKE_NUMBER.Focus();
                        }

                        // 処理終了
                        return;
                    }

                    // 新規モード and 受付番号データが存在しない場合
                    int count = this.logic.Search();
                    if (count < 0)
                    {
                        return;
                    }
                    else if (count == 0)
                    {
                        // メッセージ表示
                        this.logic.msgLogic.MessageBoxShow("E045");

                        // 受付番号の初期化
                        this.UKETSUKE_NUMBER.Text = this.UketsukeNumber;
                        // イベント削除
                        this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                        this.UKETSUKE_NUMBER.Focus();
                        this.UKETSUKE_NUMBER.Enter += this.Control_Enter;

                        // 処理終了
                        return;
                    }
                    else
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("G020", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 編集モードに変更
                            this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        }
                        else
                        {
                            /* ここに到達する前に修正＆参照なしをチェックしているので参照権限チェックは行っていない */
                            // 参照モードに変更
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        //base.OnLoad(e);
                        result = this.logic.DisplayInit();
                        if (!result)
                        {
                            // イベント削除
                            this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                            // フォーカス設定
                            this.UKETSUKE_NUMBER.Focus();
                            return;
                        }
                    }
                }

                // 編集モード処理を行う
                this.UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                result = this.logic.DisplayInit();

                if (!result)
                {
                    // イベント削除
                    this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                    // フォーカス設定
                    this.UKETSUKE_NUMBER.Focus();
                    return;
                }

                // 前・次ﾎﾞﾀﾝクリック場合、値退避
                if (this.dicControl.ContainsKey("UKETSUKE_NUMBER"))
                {
                    this.dicControl["UKETSUKE_NUMBER"] = this.UketsukeNumber;
                }
                else
                {
                    this.dicControl.Add("UKETSUKE_NUMBER", this.UketsukeNumber);
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

        #endregion

        #region 取引先更新後処理
        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 初期化
            this.isNotMoveFocusFW = true;
            var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

            if (!this.SetTorihikisaki())
            {
                return;
            }

            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void TorihikisakiBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();
                
                var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

                if (this.TORIHIKISAKI_CD.IsInputErrorOccured || this.TORIHIKISAKI_CD.Text != before)
                {
                    this.SetTorihikisaki();
                }

                // フォーカスセット
                this.TORIHIKISAKI_CD.Focus();

                //this.logic.CheckTorihikisaki();

                //// ﾎﾞﾀﾝ押下の場合、セット必要ため
                //this.dicControl["TORIHIKISAKI_CD"] = this.TORIHIKISAKI_CD.Text;
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

        /// <summary>
        /// 取引先ポップアップ後の処理
        /// </summary>
        public void TorihikisakiPopupAfterExecute(object sender, DialogResult result)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, result);

                if (result != DialogResult.Yes && result != DialogResult.OK)
                    return;

                var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

                if (this.TORIHIKISAKI_CD.IsInputErrorOccured || this.TORIHIKISAKI_CD.Text != before)
                {
                    this.SetTorihikisaki();
                }

                // フォーカスセット
                this.TORIHIKISAKI_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, result);
            }
        }
        #endregion

        #region 業者更新後処理
        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGyousha())
            {
                return;
            }

            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }

            // 業者はバリデーション時も前回値をセット
            this.Control_Enter(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void GyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // フォーカスセット
                this.GYOUSHA_CD.Focus();

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

        #region 現場更新後処理
        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGenba())
            {
                return;
            }
            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void GenbaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!this.SetGenba())
                {
                    return;
                }

                var gyoushaCd = this.GYOUSHA_CD.Text;

                bool catchErr = true;
                var gyousha = this.logic.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (null != gyousha)
                {
                    this.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;

                    this.logic.SetCtrlReadonly(this.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                }

                this.GENBA_CD.Focus();
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

        #region 営業担当者更新後処理
        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CheckEigyouTantousha();
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
        //2014/01/28 修正 仕様変更 qiao start
        #region 受付日（時）Validatedイベント
        /// <summary>
        /// 受付日（時）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_DATE_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.UKETSUKE_DATE_HOUR.Text) && string.IsNullOrEmpty(this.UKETSUKE_DATE_MINUTE.Text))
                {
                    this.UKETSUKE_DATE_MINUTE.Text = "0";
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

        #region 受付日（分）Validatedイベント
        /// <summary>
        /// 受付日（分）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_DATE_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.UKETSUKE_DATE_HOUR.Text) && !string.IsNullOrEmpty(this.UKETSUKE_DATE_MINUTE.Text))
                {
                    this.UKETSUKE_DATE_HOUR.Text = "0";
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
        //2014/01/28 修正 仕様変更 qiao end

        #region UIForm_Shownイベント

        /// <summary>
        /// UIForm_Shownイベント
        /// </summary>
        /// <param name="sender">初期フォーカスをTabの先頭から変更</param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            if (!this.logic.SetMoveData())
            {
                return;
            }

            //初期フォーカスを業者CDに設定
            this.SetFocusControl(this.GYOUSHA_CD);
            this.boolMoveFocusControl();
        }
        #endregion UIForm_Shownイベント

        #region フォーカス設定処理
        /// <summary>
        /// フォーカスさせるコントロールを設定します
        /// </summary>
        /// <param name="control">フォーカスを設定したいコントロール</param>
        internal void SetFocusControl(Control control)
        {
            //引数にフォーカスを設定
            this.focusControl = control;
        }

        /// <summary>
        /// コントロールにフォーカスを設定します
        /// </summary>
        internal bool boolMoveFocusControl()
        {
            // 初期化
            this.isNotMoveFocusFW = false;
            bool ret = false;

            if (null != this.focusControl)
            {
                this.isNotMoveFocusFW = true;
                this.focusControl.Focus();
                ret = true;
            }

            return ret;

        }
        #endregion フォーカス設定処理

        /// <summary>
        /// コントロールに入力されていた値を取得します
        /// </summary>
        /// <param name="key">コントロール名</param>
        /// <returns>前回値</returns>
        internal String GetBeforeText(string key)
        {
            LogUtility.DebugMethodStart(key);

            string ret = this.dicControl.Where(r => r.Key == key).Select(r => r.Value).DefaultIfEmpty(String.Empty).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CDに関連する項目をセットする
        /// </summary>
        public bool SetTorihikisaki()
        {
            try
            {
                var ctrl = (TextBox)this.TORIHIKISAKI_CD;

                // 取引先を取得
                var torihikisakiCd = this.TORIHIKISAKI_CD.Text;
                bool catchErr = true;
                var torihikisaki = this.logic.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                // 取引先と拠点の関係をチェック
                if (false == this.logic.CheckTorihikisakiKyoten(this.logic.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                {
                    //フォーカス設定処理
                    this.SetFocusControl(this.TORIHIKISAKI_CD);
                    this.boolMoveFocusControl();


                    this.TORIHIKISAKI_NAME.Text = string.Empty;
                    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    return false;
                }

                var before = this.GetBeforeText(ctrl.Name);
                if (!this.TORIHIKISAKI_CD.IsInputErrorOccured)
                {
                    if (ctrl.Text != before)
                    {
                        if (this.logic.CheckTorihikisaki())
                        {
                            // 何もしない
                            //（受付系の画面でコードレイアウトを合わせています)
                        }
                        else
                        {
                            //フォーカス設定処理
                            //this.boolMoveFocusControl();

                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisaki", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisaki", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ前の処理
        /// </summary>
        public void GyoushaPopupBeforeMethod()
        {
            this.beforeGyousha = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void GyoushaPopupAfterMethod()
        {
            if (this.GYOUSHA_CD.Text != this.beforeGyousha)
            {
                this.SetGyousha();
            }
        }

        /// <summary>
        /// 業者CDに関連する項目をセットする
        /// </summary>
        public bool SetGyousha()
        {
            bool ret = true;
            try
            {
                var ctrl = (TextBox)this.GYOUSHA_CD;

                var before = this.GetBeforeText(ctrl.Name);
                if (this.isInputError || ctrl.Text != before)
                {
                    // 業者CDをチェック
                    if (this.logic.ErrorCheckGyousha())
                    {
                        this.GENBA_CD.Text = string.Empty;
                        this.GENBA_NAME.Text = String.Empty;

                        if (!this.logic.CheckGyousha())
                        {
                            //フォーカス設定処理
                            this.boolMoveFocusControl();
                        }
                    }
                    else
                    {
                        //フォーカス設定処理
                        this.boolMoveFocusControl();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyousha", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyousha", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場CDに関連する項目をセットする
        /// </summary>
        public bool SetGenba()
        {
            bool ret = true;
            try
            {
                var genbaCtrl = (TextBox)this.GENBA_CD;
                var beforeGenba = this.GetBeforeText(genbaCtrl.Name);

                // 入力されてない場合
                if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    this.logic.GenbaCdEnptyProcess();
                    return true;
                }

                if (this.logic.ErrorCheckGenba())
                {
                    if (this.logic.CheckGenba())
                    {
                        if (this.logic.pressedEnterOrTab && this.GENBA_NAME.ReadOnly)
                        {
                            if (this.Key.Shift)
                            {
                                this.SetFocusControl(this.GYOUSHA_CD);
                            }
                            else
                            {
                                this.SetFocusControl(this.TORIHIKISAKI_CD);
                            }
                            //フォーカス設定処理
                            this.boolMoveFocusControl();
                        }
                    }
                    else
                    {
                        //フォーカス設定処理
                        this.boolMoveFocusControl();
                    }
                }
                else
                {
                    //フォーカス設定処理
                    this.boolMoveFocusControl();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenba", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenba", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfter()
        {
            if (!this.isInputError && this.GENBA_CD.Text == this.beforeGenba)
            {
                this.GENBA_CD.Text = this.beforeGenba;
                this.GENBA_NAME.Text = this.beforeGenbaName;
            }
            else
            {
                this.SetGenba();
            }
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfterExecute(object sender, DialogResult result)
        {
            if (result != DialogResult.Yes && result != DialogResult.OK)
                return;

            if (!this.isInputError && this.GENBA_CD.Text == this.beforeGenba)
            {
                this.GENBA_CD.Text = this.beforeGenba;
                this.GENBA_NAME.Text = this.beforeGenbaName;
            }
            else
            {
                this.SetGenba();
            }
        }

        /// <summary>
        /// 現場ポップアップ表示前の処理
        /// </summary>
        public void GenbaPopupBefore()
        {
            // フォーカスアウトせずに再度検索ポップアップを表示した際、値が変更されないための対応
            Control_Enter(this.GENBA_CD, null);
            this.beforeGenba = this.GENBA_CD.Text;
            this.beforeGenbaName = this.GENBA_NAME.Text;
        }

        /// <summary>
        ///  取引先エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            // 取引先を取得
            bool catchErr = true;
            var torihikisaki = this.logic.GetTorihikisaki(this.TORIHIKISAKI_CD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (torihikisaki != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)torihikisaki.SHOKUCHI_KBN;
            }
        }

        /// <summary>
        /// 業者エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 業者を取得
            bool catchErr = true;
            var gyousha = this.logic.GetGyousha(this.GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != gyousha)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)gyousha.SHOKUCHI_KBN;
            }

            this.beforeGyousha = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 現場エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 取引先を取得
            bool catchErr = true;
            var genba = this.logic.GetGenba(this.GENBA_CD.Text, this.GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != genba)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)genba.SHOKUCHI_KBN;
            }

            this.beforeGyousha = this.GYOUSHA_CD.Text;
            this.beforeGenba = this.GENBA_CD.Text;
        }
    }
}