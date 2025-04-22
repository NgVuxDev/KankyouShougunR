using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.Logic;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.APP
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 前回値チェック用変数(header用)
        /// </summary>
        private Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// ポップアップ選択前の業者CD
        /// </summary>
        private string beforeGyousha { get; set; }

        /// <summary>
        /// ポップアップ選択前の現場CD
        /// </summary>
        private string beforeGenba { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal UIForm()
            : this(WINDOW_TYPE.NEW_WINDOW_FLAG, null, null)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        internal UIForm(WINDOW_TYPE windowType, string gyoushaCd, string genbaCd)
            : base(WINDOW_ID.M_GENBA_DIGI, windowType)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.logic.GYOUSHA_CD = gyoushaCd;
            this.logic.GENBA_CD = genbaCd;
        }
        #endregion

        #region 画面読み込み
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.logic.WindowInit(base.WindowType))
            {
                return;
            }
        }
        #endregion

        #region ボタンイベント
        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_SAIBAN_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 採番値取得
            this.logic.Saiban();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場情報コピーボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_GENBA_COPY_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.CopyGenbaInfo();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ファンクションボタンイベント
        #region F2 新規
        /// <summary>
        /// [F2]新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CreateMode(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 新規モードで表示する
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // ヘッダー情報
            base.HeaderFormInit();

            // 画面初期化
            this.logic.GYOUSHA_CD = null;
            this.logic.GENBA_CD = null;

            this.logic.ModeInit(base.WindowType, (BusinessBaseForm)this.Parent);
            this.POINT_ID.Focus();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F4 連携削除
        /// <summary>
        /// [F4]連携削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Delete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hasError = this.logic.HasErrorDeleteData();
            if (hasError)
            {
                return;
            }

            var result = this.logic.msgLogic.MessageBoxShow("C026");
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool reslut = false;
                bool renkeiToroku = true;
                this.logic.CreateEntity(renkeiToroku);

                using (Transaction tran = new Transaction())
                {
                    reslut = this.logic.DeleteDataDigi();
                    if (reslut)
                    {
                        this.logic.DeleteData();
                    }

                    if (!reslut)
                    {
                        return;
                    }

                    // コミット
                    tran.Commit();
                }
                this.logic.msgLogic.MessageBoxShow("I001", "削除");

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                {
                    // 新規モードで表示する
                    CreateMode(sender, e);
                }
                else
                {
                    // 新規権限がない場合は画面Close
                    this.FormClose(sender, e);
                }

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F7 一覧
        /// <summary>
        /// [F7]一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormSearch(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("M694", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9 連携登録
        /// <summary>
        /// [F9]連携登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!base.RegistErrorFlag)
            {
                try
                {
                    bool reslut = false;
                    bool renkeiToroku = true;
                    if (!this.logic.GaibuPointMapNameCheck(this.POINT_NAME.Text, this.MAP_NAME.Text))
                    {
                        return;
                    }

                    this.logic.CreateEntity(renkeiToroku);

                    using (Transaction tran = new Transaction())
                    {
                        switch (this.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                reslut = this.logic.RegistDataDigi();
                                if (reslut)
                                {
                                    this.logic.RegistData(renkeiToroku);
                                }
                                break;
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                reslut = this.logic.RegistDataDigi();
                                if (reslut)
                                {
                                    this.logic.UpdateData(renkeiToroku);
                                }
                                break;
                            default:
                                break;
                        }

                        if (!reslut)
                        {
                            return;
                        }

                        // コミット
                        tran.Commit();
                    }

                    // 正常完了メッセージ通知
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.msgLogic.MessageBoxShow("I001", "登録");
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.msgLogic.MessageBoxShow("I001", "更新");
                            break;

                        default:
                            break;
                    }

                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 新規モードで表示する
                        CreateMode(sender, e);
                    }
                    else
                    {
                        // 新規権限がない場合は画面Close
                        this.FormClose(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    // 例外エラー
                    LogUtility.Error(ex);
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F10 保留登録
        /// <summary>
        /// [F10]保留登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HoldRegist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!base.RegistErrorFlag)
            {
                try
                {
                    bool reslut = false;
                    bool notRenkeiToroku = false;
                    if (!this.logic.GaibuPointMapNameCheck(this.POINT_NAME.Text, this.MAP_NAME.Text))
                    {
                        return;
                    }

                    this.logic.CreateEntity(notRenkeiToroku);

                    using (Transaction tran = new Transaction())
                    {
                        switch (this.WindowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                reslut = this.logic.RegistData(notRenkeiToroku);
                                break;
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                reslut = this.logic.UpdateData(notRenkeiToroku);
                                break;
                            default:
                                break;
                        }

                        if (!reslut)
                        {
                            return;
                        }

                        // コミット
                        tran.Commit();
                    }

                    // 正常完了メッセージ通知
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            this.logic.msgLogic.MessageBoxShow("I001", "登録");
                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.msgLogic.MessageBoxShow("I001", "更新");
                            break;

                        default:
                            break;
                    }

                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("M693", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // 新規モードで表示する
                        CreateMode(sender, e);
                    }
                    else
                    {
                        // 新規権限がない場合は画面Close
                        this.FormClose(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    // 例外エラー
                    LogUtility.Error(ex);
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F11 取消
        /// <summary>
        /// [F11]取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 画面初期化
            this.logic.ModeInit(base.WindowType, (BusinessBaseForm)this.Parent);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            base.CloseTopForm();

            LogUtility.DebugMethodEnd();
        }
        #endregion
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
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)sender;
                    // テキスト名を取得
                    String textName = this.GetTextName(ctrl.Name);
                    Control control = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = control.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, control.Text);
                    }
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

        /// <summary>
        /// フォーカス取得処理(ポップアップの後処理用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ControlEnterForPopUpAfter(Control control)
        {
            try
            {
                LogUtility.DebugMethodStart(control);

                Type type = control.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)control;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)control;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)control;
                    // テキスト名を取得
                    String textName = this.GetTextName(ctrl.Name);
                    Control ctl = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = ctl.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, ctl.Text);
                    }
                }
                else if (type.Name == "CustomDateTimePicker")
                {
                    CustomDateTimePicker ctrl = (CustomDateTimePicker)control;
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

        #region ポップアップボタン名により、テキスト名を取得
        /// <summary>
        /// ポップアップボタン名により、テキスト名を取得
        /// </summary>
        /// <param name="buttonName">ボタン名称</param>
        /// <returns></returns>
        private String GetTextName(String buttonName)
        {
            string textName = "";

            try
            {
                LogUtility.DebugMethodStart(buttonName);

                switch (buttonName)
                {
                    case "BT_GYOUSHA_SEARCH":
                        textName = "GYOUSHA_CD";
                        break;
                    case "BT_GENBA_SEARCH":
                        textName = "GENBA_CD";
                        break;
                    default:
                        break;
                }
                return textName;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(textName);
            }
        }
        #endregion

        #region ポップアップ関連イベント
        /// <summary>
        /// 業者検索ポップアップの前処理
        /// </summary>
        public void PopupBeforeGyoushaCode()
        {
            this.beforeGyousha = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者検索ポップアップの後処理
        /// </summary>
        public void PopupAfterGyoushaCode()
        {
            if (this.GYOUSHA_CD.Text != this.beforeGyousha)
            {
                this.SetGyousha();

                // Popupから戻ってきたとき値が変わっていれば前回値を保存
                this.ControlEnterForPopUpAfter(this.GYOUSHA_CD);
            }
        }

        /// <summary>
        /// 業者CDに関連する項目をセットする
        /// </summary>
        private bool SetGyousha()
        {
            bool ret = true;
            try
            {
                this.logic.isInputError = false;
                // 業者CDをチェック
                if (this.logic.ErrorCheckGyousha())
                {
                    // 現場の関連情報をクリア
                    this.logic.ClearGenbaRelationInfo();

                    if (!this.logic.CheckGyousha())
                    {
                        this.logic.isInputError = true;
                        ret = false;
                    }
                }
                else
                {
                    this.logic.isInputError = true;
                    ret = false;
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
        /// 現場検索ポップアップの前処理
        /// </summary>
        public void PopupBeforeGenbaCode()
        {
            this.beforeGyousha = this.GYOUSHA_CD.Text;
            this.beforeGenba = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 現場検索ポップアップの後処理
        /// </summary>
        public void PopupAfterGenbaCode()
        {
            if ((this.GYOUSHA_CD.Text != this.beforeGyousha || this.GENBA_CD.Text != this.beforeGenba))
            {
                this.SetGenba();

                // Popupから戻ってきたとき値が変わっていれば前回値を保存
                this.ControlEnterForPopUpAfter(this.GYOUSHA_CD);
                // Popupから戻ってきたとき値が変わっていれば前回値を保存
                this.ControlEnterForPopUpAfter(this.GENBA_CD);
            }
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理(虫眼鏡ボタン用)
        /// </summary>
        public void MusimeganeGenbaPopupAfter()
        {
            this.PopupAfterGenbaCode();
            this.GENBA_CD_Validated(null, null);
        }

        /// <summary>
        /// 現場CDに関連する項目をセットする
        /// </summary>
        private bool SetGenba()
        {
            bool ret = true;
            try
            {
                // 入力されてない場合
                if (String.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    // 現場の関連情報をクリア
                    this.logic.ClearGenbaRelationInfo();
                }

                if (!this.logic.CheckGenba())
                {
                    this.logic.isInputError = true;
                    ret = false;
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
        #endregion

        #region 各コントロール毎のイベント
        /// <summary>
        /// 業者CD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (this.GYOUSHA_CD.ReadOnly)
            {
                return;
            }

            this.beforeGyousha = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.GYOUSHA_CD.ReadOnly)
            {
                return;
            }

            var befoer = string.Empty;
            if (this.dicControl.ContainsKey(this.GYOUSHA_CD.Name))
            {
                befoer = this.dicControl[this.GYOUSHA_CD.Name];
            }

            if (this.logic.isInputError || this.GYOUSHA_CD.Text != this.beforeGyousha
                || this.GYOUSHA_CD.Text != befoer)
            {
                if (!this.SetGyousha())
                {
                    return;
                }

                // 業者はバリデーション時も前回値をセット
                this.Control_Enter(sender, e);

                this.logic.isInputError = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場CD Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            if (this.GENBA_CD.ReadOnly)
            {
                return;
            }

            this.beforeGyousha = this.GYOUSHA_CD.Text;
            this.beforeGenba = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 現場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.GENBA_CD.ReadOnly)
            {
                return;
            }

            var befoerGyousha = string.Empty;
            var befoerGenba = string.Empty;
            if (this.dicControl.ContainsKey(this.GYOUSHA_CD.Name))
            {
                befoerGyousha = this.dicControl[this.GYOUSHA_CD.Name];
            }
            if (this.dicControl.ContainsKey(this.GENBA_CD.Name))
            {
                befoerGenba = this.dicControl[this.GENBA_CD.Name];
            }

            if ((this.logic.isInputError || this.GYOUSHA_CD.Text != this.beforeGyousha || this.GENBA_CD.Text != this.beforeGenba)
                || befoerGyousha != this.GYOUSHA_CD.Text || befoerGenba != this.GENBA_CD.Text)
            {
                if (!this.SetGenba())
                {
                    return;
                }

                // 登録済みデータかチェック
                this.logic.CheckRegisteredData();

                this.logic.isInputError = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 外部地点CD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void POINT_ID_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {

                // 変更なしの場合
                if (this.dicControl.ContainsKey("POINT_ID") &&
                    this.dicControl["POINT_ID"].Equals(this.POINT_ID.Text))
                {
                    return;
                }

                // 【新規】モードの場合のみチェック処理を行う
                if (!this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }
                // 入力された外部地点ID取得
                if (string.IsNullOrWhiteSpace(this.POINT_ID.Text))
                {
                    return;
                }

                M_GENBA_DIGI genbaDigi;
                // 地点ID
                if (!this.logic.GetGenbaDigiKeyCode(out genbaDigi))
                {
                    return;
                }

                OpenEditModeByKey(genbaDigi);
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

        #region 登録済み現場マスタ呼出し
        /// <summary>
        /// 既に登録されている現場マスタの呼出
        /// </summary>
        /// <param name="genbaDigi"></param>
        internal void OpenEditModeByKey(M_GENBA_DIGI genbaDigi)
        {
            if (genbaDigi == null || string.IsNullOrEmpty(genbaDigi.GYOUSHA_CD) || string.IsNullOrEmpty(genbaDigi.GENBA_CD))
            {
                return;
            }

            this.logic.GYOUSHA_CD = genbaDigi.GYOUSHA_CD;
            this.logic.GENBA_CD = genbaDigi.GENBA_CD;

            // 権限降格チェック
            if (r_framework.Authority.Manager.CheckAuthority("M693", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 修正モードで表示する
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                // ヘッダー情報
                base.HeaderFormInit();

                this.POINT_NAME.Focus();

                // 画面初期化
                this.logic.ModeInit(base.WindowType, (BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M693", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                // 参照モードで表示する
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;

                // ヘッダー情報
                base.HeaderFormInit();

                // 参照モードで画面初期化
                this.logic.ModeInit(base.WindowType, (BusinessBaseForm)this.Parent);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
                this.TORIHIKISAKI_CD.Text = string.Empty;
                this.TORIHIKISAKI_CD.Focus();
            }
        }
        #endregion
    }
}
