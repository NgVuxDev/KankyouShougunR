// $Id: UIForm.cs 54199 2015-07-01 05:01:14Z minhhoang@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Const;
using Shougun.Core.Master.HikiaiTorihikisakiHoshu.Logic;
using Seasar.Framework.Exceptions;
using System.Linq;
using r_framework.CustomControl;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.APP
{
    /// <summary>
    /// 引合取引先入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 引合取引先入力画面ロジック
        /// </summary>
        private LogicCls logic;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 銀行支店ポップアップ表示フラグ
        /// </summary>
        internal bool isBankShitenPopup;

        /// <summary>
        /// 銀行支店2ポップアップ表示フラグ
        /// </summary>
        internal bool isBankShitenPopup_2;

        /// <summary>
        /// 銀行支店3ポップアップ表示フラグ
        /// </summary>
        internal bool isBankShitenPopup_3;

        /// <summary>
        /// 銀行支店CD前回入力値
        /// </summary>
        internal string previousBankShitenCd;

        /// <summary>
        /// 銀行支店CD2前回入力値
        /// </summary>
        internal string previousBankShitenCd_2;

        /// <summary>
        /// 銀行支店CD3前回入力値
        /// </summary>
        internal string previousBankShitenCd_3;
        internal string previousBankShitenMotoCd;//160026
        private bool isBankShitenPopup_moto;//160026
        private string beforeFURIKOMI_BANK_MOTO_CD = string.Empty;//160026

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();

                InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public UIForm(WINDOW_TYPE type, string torihikisakiCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_HIKIAI_TORIHIKISAKI_NYUURYOKU, type)
        {
            try
            {
                LogUtility.DebugMethodStart(type, torihikisakiCd, denshiShinseiFlg);

                InitializeComponent();

                // この２つのプロパティは、デザイナー上でNoneに設定しても、
                // フレームワーク側で上書きされてしまうためここで再設定します。
                this.TORIHIKISAKI_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.TORIHIKISAKI_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.SEIKYUU_SOUFU_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.SEIKYUU_SOUFU_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.SHIHARAI_SOUFU_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.SHIHARAI_SOUFU_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.MANI_HENSOUSAKI_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.MANI_HENSOUSAKI_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.TORIHIKISAKI_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.TORIHIKISAKI_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.SEIKYUU_SOUFU_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.SEIKYUU_SOUFU_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.SHIHARAI_SOUFU_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.SHIHARAI_SOUFU_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.MANI_HENSOUSAKI_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.MANI_HENSOUSAKI_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
                this.logic.torihikisakiCD = torihikisakiCd;
                this.logic.denshiShinseiFlg = denshiShinseiFlg;

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                //※※※　何故か、タブが一度選択されないと修正時に敬称がうまくセットされないので
                //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
                TabPage now = this.tabData.SelectedTab;
                foreach (TabPage page in this.tabData.TabPages)
                {
                    this.tabData.SelectedTab = page;
                }
                this.tabData.SelectedTab = now;
                //※※※　強引な対応ここまで

                if (!this.logic.WindowInit(WindowType))
                {
                    return;
                }

                this.logic.Search();

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.GYOUSHA_ICHIRAN != null)
                {
                    this.GYOUSHA_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 20140708 ria No.947 営業管理機能改修 start
        private bool ikoflg = false;
        /// <summary>
        /// F1:移行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToTuujyou(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M213", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (msgLogic.MessageBoxShow("C074") == DialogResult.Yes)
                {
                    string bak_TORIHIKISAKI_CD = this.TORIHIKISAKI_CD.Text;

                    var parentForm = (BusinessBaseForm)this.Parent;
                    //登録ボタン(F9)イベント呼出
                    parentForm.bt_func9.PerformClick();

                    if (!base.RegistErrorFlag && this.ikoflg)
                    {
                        this.logic.MoveToTuujyou(bak_TORIHIKISAKI_CD);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        // 20140708 ria No.947 営業管理機能改修 end

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!Manager.CheckAuthority("M461", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                // 画面再描画
                base.HeaderFormInit();

                // 画面初期化
                this.logic.torihikisakiCD = string.Empty;
                this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (Manager.CheckAuthority("M461", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (Manager.CheckAuthority("M461", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    if (!Manager.CheckAuthority("M461", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");
                        return;
                    }

                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                // 画面再描画
                base.HeaderFormInit();

                // 画面初期化
                if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                }
                else if (base.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 本登録済みデータチェック
                bool catchErr = true;
                bool isOk = this.logic.ExistedTorihikisaki(this.TORIHIKISAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    && isOk)
                {
                    MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E209");
                    return;
                }

                this.logic.Cancel((BusinessBaseForm)this.Parent);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ShowIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　end
                    
                    #region 電子申請チェック
                    var messageShowLogic = new MessageBoxShowLogic();
                    bool catchErr = true;
                    bool isOk = this.logic.CheckDenshiShinseiData(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if ((base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                            || base.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                        && !isOk)
                    {
                        messageShowLogic.MessageBoxShow("E192", WINDOW_TYPEExt.ToTypeString(base.WindowType));
                        return;
                    }
                    #endregion

                    #region 本登録済みデータチェック
                    isOk = this.logic.ExistedTorihikisaki(this.TORIHIKISAKI_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        && isOk)
                    {
                        messageShowLogic.MessageBoxShow("E202", "既に");
                        return;
                    }
                    #endregion

                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }
                    string sTorihikisakiCd = this.logic.entitysTORIHIKISAKI.TORIHIKISAKI_CD;

                    switch (base.WindowType)
                    {
                        // 新規追加
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 重複チェック
                            bool result = this.DupliUpdateViewCheck(e, true);
                            if (result)
                            {
                                // 重複していなければ登録を行う
                                this.logic.Regist(base.RegistErrorFlag);
                            }
                            break;

                        // 更新
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            this.logic.Update(base.RegistErrorFlag);
                            break;

                        // 論理削除
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            this.logic.LogicalDelete();
                            break;

                        default:
                            break;
                    }

                    if (this.logic.isRegist)
                    {
                        if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            if (Manager.CheckAuthority("M461", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                // DB更新後、新規モードで表示
                                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.torihikisakiCD = string.Empty;
                                this.logic.isRegist = false;
                                if (!this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent))
                                {
                                    return;
                                }
                                // 20140711 ria No.947 営業管理機能改修 start
                                this.ikoflg = true;
                                // 20140711 ria No.947 営業管理機能改修 end
                            }
                            else
                            {
                                this.FormClose(sender, e);
                            }
                        }
                        else//open mode 参照 after regist
                        {
                            if (Manager.CheckAuthority("M461", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // DB更新後、新規モードで表示
                                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.torihikisakiCD = sTorihikisakiCd;
                                this.logic.isRegist = false;
                                if (!this.logic.WindowInitReference((BusinessBaseForm)this.Parent))
                                {
                                    return;
                                }
                                // 20140711 ria No.947 営業管理機能改修 start
                                this.ikoflg = true;
                                // 20140711 ria No.947 営業管理機能改修 end
                            }
                            else
                            {
                                this.FormClose(sender, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 電子申請登録
        /// <summary>
        /// 電子登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Shinsei(object sender, EventArgs e)
        {
            try
            {
                if (!base.RegistErrorFlag)
                {
                    /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合取引先入力」の日付チェックを追加する　end
                    
                    #region 電子申請入力起動
                    var initDto = this.logic.CreateDenshiShinseiInitDto();

                    // 以下の変数は画面で設定されたデータが設定されてくる
                    var denshiShinseiEntry = new T_DENSHI_SHINSEI_ENTRY();
                    var denshiShinseiDetailList = new List<T_DENSHI_SHINSEI_DETAIL>();

                    FormManager.OpenFormModal("G279", WINDOW_TYPE.NEW_WINDOW_FLAG, null, null
                                        , initDto, denshiShinseiEntry, denshiShinseiDetailList);

                    // 必須項目が入力されていないのであれば何もしない
                    if (denshiShinseiEntry.SHINSEI_DATE.IsNull)
                    {
                        return;
                    }
                    #endregion

                    #region 更新処理
                    #region 電子申請チェック
                    var messageShowLogic = new MessageBoxShowLogic();
                    bool catchErr = true;
                    bool isOk = this.logic.CheckDenshiShinseiData(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        messageShowLogic.MessageBoxShow("E189");
                        return;
                    }
                    #endregion

                    if (!this.logic.CreateEntity(false))
                    {
                        return;
                    }
                    this.logic.Shinsei(base.RegistErrorFlag, denshiShinseiEntry, denshiShinseiDetailList);
                    #endregion

                    if (this.logic.isRegist)
                    {
                        // 成功
                        this.FormClose(sender, e);
                        FormManager.UpdateForm("G560");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Shinsei", ex);
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 取引先CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister)
        {
            LogUtility.DebugMethodStart(e, isRegister);

            bool result = false;
            try
            {

                // 取引先CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.TORIHIKISAKI_CD.Text);

                // 重複チェック
                ConstCls.TorihikisakiCdLeaveResult isUpdate = this.logic.DupliCheckTorihikisakiCd(zeroPadCd, isRegister);

                if (isUpdate == ConstCls.TorihikisakiCdLeaveResult.FALSE_ON)
                {
                    if (Manager.CheckAuthority("M461", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                    else if (Manager.CheckAuthority("M461", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    else
                    {
                        // 権限無しの場合
                        this.TORIHIKISAKI_CD.Text = string.Empty;
                        this.TORIHIKISAKI_CD.Focus();

                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");

                        return result;
                    }

                    this.logic.torihikisakiCD = zeroPadCd;

                    base.HeaderFormInit();

                    this.NYUUKINSAKI_KBN.Focus();

                    if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        // 修正モードで画面初期化
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                    }
                    else if (base.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        // 参照モードで画面初期化
                        this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                    }

                    result = true;
                }
                else if (isUpdate != ConstCls.TorihikisakiCdLeaveResult.TURE_NONE)
                {
                    // 入力した取引先CDが重複した かつ 修正モード未起動の場合
                    this.TORIHIKISAKI_CD.Text = string.Empty;
                    this.TORIHIKISAKI_CD.Focus();
                }
                else
                {
                    // 重複しなければINSERT処理を行うフラグON
                    result = true;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DupliUpdateViewCheck", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.Parent;

                this.Close();
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TorihikisakiCdValidating(object sender, CancelEventArgs e)
        {
            // 【新規】モードの場合のみチェック処理を行う
            if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 本登録済みデータチェック
                bool catchErr = true;
                bool isOk = this.logic.ExistedTorihikisaki(this.TORIHIKISAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (isOk)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E202", "既に");

                    e.Cancel = true;

                    return;
                }
            }
        }

        /// <summary>
        /// 取引先CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TorihikisakiCdValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【新規】モードの場合のみチェック処理を行う
                if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 入力された取引先CD取得
                    string inputCd = this.TORIHIKISAKI_CD.Text;
                    if (!string.IsNullOrWhiteSpace(inputCd))
                    {
                        // 重複チェック
                        this.logic.torihikisakiCD = inputCd;
                        this.DupliUpdateViewCheck(e, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TorihikisakiCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 採番ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SaibanButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Saiban();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySeikyuButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CopyToSeikyu();
                this.logic.CheckTextBoxLength(this.SEIKYUU_SOUFU_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopySeikyuButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopySiharaiButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CopyToSiharai();
                this.logic.CheckTextBoxLength(this.SHIHARAI_SOUFU_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopySiharaiButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 分類取引先コピーボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void CopyManiButtonClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CopyToMani();
                this.logic.CheckTextBoxLength(this.MANI_HENSOUSAKI_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CopyManiButtonClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当部署CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void EigyouTantouBushoCdValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.EigyouTantouBushoCdValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouBushoCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当者CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void EigyouTantouCdValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.EigyouTantouCdValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EigyouTantouBushoCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【営業担当者CD】【営業担当者名】をクリアする。
                this.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.EIGYOU_TANTOU_NAME.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouBushoCode_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当者CDポップアップ後処理
        /// </summary>
        public virtual void EigyouTantouCdAfterPopup()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.logic.EigyouTantouCdValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("EigyouTantouCdAfterPopup", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 振込銀行CD変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCdValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.FurikomiBankCdValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCdValidated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 振込銀行CD2変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCd2Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.FurikomiBankCd2Validated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCd2Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 振込銀行CD3変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FurikomiBankCd3Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.FurikomiBankCd3Validated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FurikomiBankCd3Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 入金先CDポップアップ後処理
        /// </summary>
        public virtual void NyuukinsakiCdAfterPopup()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.logic.NyuukinsakiCdValidated();
            }
            catch (Exception ex)
            {
                LogUtility.Error("NyuukinsakiCdAfterPopup", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 開始売掛残高の入力終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAISHI_URIKAKE_ZANDAKA_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.SetZandakaFormat(this.KAISHI_URIKAKE_ZANDAKA.Text, this.KAISHI_URIKAKE_ZANDAKA);
            }
            catch (Exception ex)
            {
                LogUtility.Error("KAISHI_URIKAKE_ZANDAKA_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 開始買掛残高の入力終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAISHI_KAIKAKE_ZANDAKA_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.SetZandakaFormat(this.KAISHI_KAIKAKE_ZANDAKA.Text, this.KAISHI_KAIKAKE_ZANDAKA);
            }
            catch (Exception ex)
            {
                LogUtility.Error("KAISHI_KAIKAKE_ZANDAKA_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ウインドウタイプ設定処理
        /// </summary>
        /// <param name="type"></param>
        public void SetWindowType(WINDOW_TYPE type)
        {
            try
            {
                LogUtility.DebugMethodStart(type);

                base.WindowType = type;
                base.HeaderFormInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowType", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrWhiteSpace(this.SHIMEBI1.Text))
                {
                    this.SHIMEBI2.Enabled = false;
                    this.SHIMEBI2.Text = string.Empty;
                    this.SHIMEBI3.Enabled = false;
                }
                else
                {
                    this.SHIMEBI2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIMEBI1_SelectedIndexChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求先締日1変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIMEBI2.Text) && this.SHIMEBI1.Text.Equals(this.SHIMEBI2.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日2");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIMEBI3.Text) && this.SHIMEBI1.Text.Equals(this.SHIMEBI3.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日3");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIMEBI1_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrWhiteSpace(this.SHIMEBI2.Text))
                {
                    this.SHIMEBI3.Enabled = false;
                    this.SHIMEBI3.Text = string.Empty;
                }
                else
                {
                    this.SHIMEBI3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIMEBI2_SelectedIndexChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIMEBI1.Text) && this.SHIMEBI2.Text.Equals(this.SHIMEBI1.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日1");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIMEBI3.Text) && this.SHIMEBI2.Text.Equals(this.SHIMEBI3.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日3");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIMEBI2_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIMEBI1.Text) && this.SHIMEBI3.Text.Equals(this.SHIMEBI1.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日1");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIMEBI2.Text) && this.SHIMEBI3.Text.Equals(this.SHIMEBI2.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日2");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIMEBI3_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払先締日1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text))
                {
                    this.SHIHARAI_SHIMEBI2.Enabled = false;
                    this.SHIHARAI_SHIMEBI2.Text = string.Empty;
                    this.SHIHARAI_SHIMEBI3.Enabled = false;
                    this.SHIHARAI_SHIMEBI3.Text = string.Empty;
                }
                else
                {
                    this.SHIHARAI_SHIMEBI2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHIMEBI1_SelectedIndexChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払先締日1変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日2");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI1.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日3");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHIMEBI1_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払先締日2変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text))
                {
                    this.SHIHARAI_SHIMEBI3.Enabled = false;
                    this.SHIHARAI_SHIMEBI3.Text = string.Empty;
                }
                else
                {
                    this.SHIHARAI_SHIMEBI3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHIMEBI2_SelectedIndexChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払先締日2変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日1");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI3.Text) && this.SHIHARAI_SHIMEBI2.Text.Equals(this.SHIHARAI_SHIMEBI3.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日3");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHIMEBI2_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払先締日3変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHIMEBI3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI1.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI1.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日1");
                    e.Cancel = true;
                }
                if (!string.IsNullOrWhiteSpace(this.SHIHARAI_SHIMEBI2.Text) && this.SHIHARAI_SHIMEBI3.Text.Equals(this.SHIHARAI_SHIMEBI2.Text))
                {
                    msgLogic.MessageBoxShow("E031", "締日2");
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHIMEBI3_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private string beforeFURIKOMI_BANK_CD = string.Empty;
        private string beforeFURIKOMI_BANK_CD_2 = string.Empty;
        private string beforeFURIKOMI_BANK_CD_3 = string.Empty;


        /// <summary>
        /// 銀行検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_PopupAfterExecuteMethod()
        {
            if (!this.beforeFURIKOMI_BANK_CD.Equals(this.FURIKOMI_BANK_CD.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD.Text = string.Empty;
                this.previousBankShitenCd = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                this.KOUZA_SHURUI.Text = string.Empty;
                this.KOUZA_NO.Text = string.Empty;
                this.KOUZA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 銀行2検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_2_PopupAfterExecuteMethod()
        {
            if (!this.beforeFURIKOMI_BANK_CD_2.Equals(this.FURIKOMI_BANK_CD_2.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD_2.Text = string.Empty;
                this.previousBankShitenCd_2 = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME_2.Text = string.Empty;
                this.KOUZA_SHURUI_2.Text = string.Empty;
                this.KOUZA_NO_2.Text = string.Empty;
                this.KOUZA_NAME_2.Text = string.Empty;
            }
        }

        /// <summary>
        /// 銀行3検索ポップアップ実行後処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_3_PopupAfterExecuteMethod()
        {
            if (!this.beforeFURIKOMI_BANK_CD_3.Equals(this.FURIKOMI_BANK_CD_3.Text))
            {
                this.FURIKOMI_BANK_SHITEN_CD_3.Text = string.Empty;
                this.previousBankShitenCd_3 = string.Empty;
                this.FURIKOMI_BANK_SHITEN_NAME_3.Text = string.Empty;
                this.KOUZA_SHURUI_3.Text = string.Empty;
                this.KOUZA_NO_3.Text = string.Empty;
                this.KOUZA_NAME_3.Text = string.Empty;
            }
        }

        /// <summary>
        /// 銀行検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD = this.FURIKOMI_BANK_CD.Text;
        }

        /// <summary>
        /// 銀行2検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_2_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD_2 = this.FURIKOMI_BANK_CD_2.Text;
        }

        /// <summary>
        /// 銀行3検索ポップアップ実行前処理
        /// </summary>
        public void FURIKOMI_BANK_SEARCH_3_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_CD_3 = this.FURIKOMI_BANK_CD_3.Text;
        }

        /// <summary>
        /// 銀行支店CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FURIKOMI_BANK_SHITEN_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrWhiteSpace(this.FURIKOMI_BANK_SHITEN_CD.Text))
                {
                    this.FURIKOMI_BANK_SHITEN_NAME.Text = string.Empty;
                    this.KOUZA_SHURUI.Text = string.Empty;
                    this.KOUZA_NO.Text = string.Empty;
                    this.KOUZA_NAME.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("FURIKOMI_BANK_SHITEN_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入金先区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NYUUKINSAKI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.NYUUKINSAKI_KBN.Text.Equals("1"))
                {
                    this.NYUUKINSAKI_CD.Enabled = false;
                    this.NYUUKINSAKI_SEARCH.Enabled = false;
                    this.NYUUKINSAKI_CD.Text = string.Empty;
                    this.NYUUKINSAKI_NAME1.Text = string.Empty;
                    this.NYUUKINSAKI_NAME2.Text = string.Empty;
                }
                else
                {
                    this.NYUUKINSAKI_CD.Enabled = true;
                    this.NYUUKINSAKI_SEARCH.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("NYUUKINSAKI_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求情報1取引区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeTorihikiKbn(true);
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKI_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払情報1取引区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SIHARAI_TORIHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeSiharaiTorihikiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SIHARAI_TORIHIKI_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニ返送先区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManiHensousakiKbn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.MANI_HENSOUSAKI_KBN.Checked)
                {
                    this.MANI_HENSOUSAKI_THIS_ADDRESS_KBN.Text = "2";
                    this.MANI_HENSOUSAKI_NAME1.Text = string.Empty;
                    this.MANI_HENSOUSAKI_NAME2.Text = string.Empty;
                    this.MANI_HENSOUSAKI_KEISHOU1.Text = string.Empty;
                    this.MANI_HENSOUSAKI_KEISHOU2.Text = string.Empty;
                    this.MANI_HENSOUSAKI_POST.Text = string.Empty;
                    this.MANI_HENSOUSAKI_ADDRESS1.Text = string.Empty;
                    this.MANI_HENSOUSAKI_ADDRESS2.Text = string.Empty;
                    this.MANI_HENSOUSAKI_BUSHO.Text = string.Empty;
                    this.MANI_HENSOUSAKI_TANTOU.Text = string.Empty;
                }
                this.logic.ManiCheckOffCheck();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ManiHensousakiKbn_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 営業担当部署変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.EIGYOU_TANTOU_CD.Text.Equals(this.PreviousValue) || this.EIGYOU_TANTOU_CD.IsInputErrorOccured)
                {
                    if (!this.logic.EigyouTantouCdValidated())
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("EIGYOU_TANTOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 銀行支店CDフォーカス乖離処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FURIKOMI_BANK_SHITEN_CD_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // No2994-->
                //this.logic.SetBankInfo();
                // No2994<--
            }
            catch (Exception ex)
            {
                LogUtility.Error("FURIKOMI_BANK_SHITEN_CD_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引中止業者指定変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKI_STOP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    this.logic.TorihikiStopIchiran();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKI_STOP_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_CellDoubleClick(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex >= 0)
                {
                    this.logic.ShowGyoushaWindow();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ICHIRAN_CellDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    this.logic.ShowGyoushaWindow();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ICHIRAN_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧キー押下処理（KeyDownより先に動きます）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // Enterキーが押下された場合
                if (e.KeyCode == Keys.Enter)
                {
                    // 現場入力画面を呼び出します
                    this.logic.ShowGyoushaWindow();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ICHIRAN_PreviewKeyDown", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.KeyPreview = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ICHIRAN_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者一覧フォーカス乖離処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ICHIRAN_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ICHIRAN_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 請求書式明細区分の制限処理
                this.logic.LimitSeikyuuShoshikiMeisaiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHOSHIKI_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払書式1変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOSHIKI_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 支払書式明細区分の制限処理
                this.logic.LimitShiharaiShoshikiMeisaiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_SHOSHIKI_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SEIKYUU_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeSeikyuuKyotenPrintKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SEIKYUU_KYOTEN_PRINT_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払拠点印字区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_PRINT_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChangeShiharaiKyotenPrintKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_KYOTEN_PRINT_KBN_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 請求税区分の制限処理
                this.logic.LimitSeikyuuZeiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZEI_KEISAN_KBN_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払税計算区分変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 支払税区分の制限処理
                this.logic.LimitShiharaiZeiKbn();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.SeikyuuKyotenCdValidated())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SEIKYUU_KYOTEN_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払書拠点の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_KYOTEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.ShiharaiKyotenCdValidated())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHIHARAI_KYOTEN_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 部署の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOU_BUSHO_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            if (!this.logic.BushoCdValidated())
            {
                e.Cancel = true;
            }
        }

        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう start‏
        /// <summary>
        /// 振込銀行支店CDのバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURIKOMI_BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD.Text;

            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm start
            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenCd != bankShitenCd)
            {
                this.isBankShitenPopup = false;
            }
            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm end

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                    this.KOUZA_SHURUI.Text = String.Empty;
                    this.KOUZA_NO.Text = String.Empty;
                    this.KOUZA_NAME.Text = String.Empty;

                    this.previousBankShitenCd = String.Empty;

                    this.isBankShitenPopup = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd = bankShitenCd;

                    this.isBankShitenPopup = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup && this.previousBankShitenCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD);
                        this.isBankShitenPopup = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup = false;
                    }

                    this.previousBankShitenCd = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME.Text = String.Empty;
                this.KOUZA_SHURUI.Text = String.Empty;
                this.KOUZA_NO.Text = String.Empty;
                this.KOUZA_NAME.Text = String.Empty;

                this.previousBankShitenCd = String.Empty;

                this.isBankShitenPopup = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店CD2のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void FURIKOMI_BANK_SHITEN_CD_2_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD_2.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD_2.Text;

            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD_2.Focus();
                return;
            }
            if (this.previousBankShitenCd_2 != bankShitenCd)
            {
                this.isBankShitenPopup_2 = false;
            }

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME_2.Text = String.Empty;
                    this.KOUZA_SHURUI_2.Text = String.Empty;
                    this.KOUZA_NO_2.Text = String.Empty;
                    this.KOUZA_NAME_2.Text = String.Empty;

                    this.previousBankShitenCd_2 = String.Empty;

                    this.isBankShitenPopup_2 = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME_2.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_2.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_2.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_2.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_2 = bankShitenCd;

                    this.isBankShitenPopup_2 = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_2 && this.previousBankShitenCd_2 != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD_2);
                        this.isBankShitenPopup_2 = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_2 = false;
                    }

                    this.previousBankShitenCd_2 = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME_2.Text = String.Empty;
                this.KOUZA_SHURUI_2.Text = String.Empty;
                this.KOUZA_NO_2.Text = String.Empty;
                this.KOUZA_NAME_2.Text = String.Empty;

                this.previousBankShitenCd_2 = String.Empty;

                this.isBankShitenPopup_2 = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店CD3のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void FURIKOMI_BANK_SHITEN_CD_3_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURIKOMI_BANK_CD_3.Text;
            var bankShitenCd = this.FURIKOMI_BANK_SHITEN_CD_3.Text;

            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm start
            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURIKOMI_BANK_SHITEN_CD_3.Focus();
                return;
            }
            if (this.previousBankShitenCd_3 != bankShitenCd)
            {
                this.isBankShitenPopup_3 = false;
            }
            //20150706 #2124 銀行名がブランクの場合はアラートを表示させるようにする。hoanghm end

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURIKOMI_BANK_SHITEN_NAME_3.Text = String.Empty;
                    this.KOUZA_SHURUI_3.Text = String.Empty;
                    this.KOUZA_NO_3.Text = String.Empty;
                    this.KOUZA_NAME_3.Text = String.Empty;

                    this.previousBankShitenCd_3 = String.Empty;

                    this.isBankShitenPopup_3 = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURIKOMI_BANK_SHITEN_NAME_3.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.KOUZA_SHURUI_3.Text = bankShiten.KOUZA_SHURUI;
                    this.KOUZA_NO_3.Text = bankShiten.KOUZA_NO;
                    this.KOUZA_NAME_3.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenCd_3 = bankShitenCd;

                    this.isBankShitenPopup_3 = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_3 && this.previousBankShitenCd_3 != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURIKOMI_BANK_SHITEN_CD_3);
                        this.isBankShitenPopup_3 = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_3 = false;
                    }

                    this.previousBankShitenCd_3 = bankShitenCd;
                }
            }
            else
            {
                this.FURIKOMI_BANK_SHITEN_NAME_3.Text = String.Empty;
                this.KOUZA_SHURUI_3.Text = String.Empty;
                this.KOUZA_NO_3.Text = String.Empty;
                this.KOUZA_NAME_3.Text = String.Empty;

                this.previousBankShitenCd_3 = String.Empty;

                this.isBankShitenPopup_3 = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 振込銀行支店CD PopupAfterExecuteMethod
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_PopupAfterExecuteMethod()
        {
            this.previousBankShitenCd = this.FURIKOMI_BANK_SHITEN_CD.Text;
        }

        /// <summary>
        /// 振込銀行支店CD2 PopupAfterExecuteMethod
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_2_PopupAfterExecuteMethod()
        {
            this.previousBankShitenCd_2 = this.FURIKOMI_BANK_SHITEN_CD_2.Text;
        }

        /// <summary>
        /// 振込銀行支店CD3 PopupAfterExecuteMethod
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_3_PopupAfterExecuteMethod()
        {
            this.previousBankShitenCd_3 = this.FURIKOMI_BANK_SHITEN_CD_3.Text;
        }

        // 20140717 katen No.5264 CDが重複している銀行支店が複数登録されていた場合、フォーカスアウト時に同一CDの違う口座に切り替わってしまう end‏

        /// <summary>
        /// 都道府県チェック
        /// </summary>
        internal bool CheckTodoufuken()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ０埋め処理
                if (!string.IsNullOrEmpty(this.TORIHIKISAKI_TODOUFUKEN_CD.Text))
                {
                    var todoufukenCd = Convert.ToInt16(this.TORIHIKISAKI_TODOUFUKEN_CD.Text);
                    this.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Format("{0:D2}", todoufukenCd);
                }
                else
                {
                    this.TORIHIKISAKI_TODOUFUKEN_NAME.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTodoufuken", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 都道府県ポップアップから戻ってきた後に実行されます
        /// </summary>
        public void TodoufukenPopupAfter()
        {
            // 都道府県チェック
            this.CheckTodoufuken();
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

        private void MANI_HENSOUSAKI_THIS_ADDRESS_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeManiHensousakiAddKbn();
        }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 請求情報1出力区分CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OUTPUT_KBN_TextChanged(object sender, EventArgs e)
        {
            this.logic.ChangeOutputKbn();
        }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end
        /// <summary>
        /// 振込人名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FURIKOMI_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaCheck(textbox))
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる
            //スペース、数字、英小文字、英大文字、記号「()」「,」「.」「/」「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z()\\,./｢｣\-\uFF66-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }

        /// <summary>
        /// 取引先敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 取引先敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TORIHIKISAKI_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 請求敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_SOUFU_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SEIKYUU_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 支払敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SOUFU_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SHIHARAI_SOUFU_KEISHOU2.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 返送先敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.MANI_HENSOUSAKI_KEISHOU2.DroppedDown = false;
        }
        //160026 S
        internal void SHIHARAI_BETSU_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.SHIHARAI_BETSU_KBN.Text == "2")
            {
                this.SHIHARAI_MONTH.Text = string.Empty;
                this.SHIHARAI_MONTH.Enabled = false;
                this.SHIHARAI_MONTH_1.Enabled = false;
                this.SHIHARAI_MONTH_2.Enabled = false;
                this.SHIHARAI_MONTH_3.Enabled = false;
                this.SHIHARAI_MONTH_4.Enabled = false;
                this.SHIHARAI_MONTH_5.Enabled = false;
                this.SHIHARAI_MONTH_6.Enabled = false;
                this.SHIHARAI_MONTH_7.Enabled = false;
                this.SHIHARAI_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_BETSU_NICHIGO.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.SHIHARAI_BETSU_NICHIGO.Text = this.logic.entitysM_SYS_INFO.SHIHARAI_KAISHUU_BETSU_NICHIGO.ToString();
                }
            }
            if (this.SHIHARAI_BETSU_KBN.Text == "1")
            {
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SHIHARAI_MONTH.IsNull)
                {
                    this.SHIHARAI_MONTH.Text = this.logic.entitysM_SYS_INFO.SHIHARAI_MONTH.ToString();
                }
                this.SHIHARAI_MONTH.Enabled = true;
                this.SHIHARAI_MONTH_1.Enabled = true;
                this.SHIHARAI_MONTH_2.Enabled = true;
                this.SHIHARAI_MONTH_3.Enabled = true;
                this.SHIHARAI_MONTH_4.Enabled = true;
                this.SHIHARAI_MONTH_5.Enabled = true;
                this.SHIHARAI_MONTH_6.Enabled = true;
                this.SHIHARAI_MONTH_7.Enabled = true;
                this.SHIHARAI_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.SHIHARAI_BETSU_NICHIGO.Text = string.Empty;
                this.SHIHARAI_BETSU_NICHIGO.Enabled = false;
            }
        }

        internal void KAISHUU_BETSU_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.KAISHUU_BETSU_KBN.Text == "2")
            {
                this.KAISHUU_MONTH.Text = string.Empty;
                this.KAISHUU_MONTH.Enabled = false;
                this.KAISHUU_MONTH_1.Enabled = false;
                this.KAISHUU_MONTH_2.Enabled = false;
                this.KAISHUU_MONTH_3.Enabled = false;
                this.KAISHUU_MONTH_4.Enabled = false;
                this.KAISHUU_MONTH_5.Enabled = false;
                this.KAISHUU_MONTH_6.Enabled = false;
                this.KAISHUU_MONTH_7.Enabled = false;
                this.KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.KAISHUU_BETSU_NICHIGO.Enabled = true;
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.IsNull)
                {
                    this.KAISHUU_BETSU_NICHIGO.Text = this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_BETSU_NICHIGO.ToString();
                }
            }
            if (this.KAISHUU_BETSU_KBN.Text == "1")
            {
                if (this.logic.entitysM_SYS_INFO != null && !this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.IsNull)
                {
                    this.KAISHUU_MONTH.Text = this.logic.entitysM_SYS_INFO.SEIKYUU_KAISHUU_MONTH.ToString();
                }
                this.KAISHUU_MONTH.Enabled = true;
                this.KAISHUU_MONTH_1.Enabled = true;
                this.KAISHUU_MONTH_2.Enabled = true;
                this.KAISHUU_MONTH_3.Enabled = true;
                this.KAISHUU_MONTH_4.Enabled = true;
                this.KAISHUU_MONTH_5.Enabled = true;
                this.KAISHUU_MONTH_6.Enabled = true;
                this.KAISHUU_MONTH_7.Enabled = true;
                this.KAISHUU_BETSU_NICHIGO.BackColor = Constans.NOMAL_COLOR;
                this.KAISHUU_BETSU_NICHIGO.Text = string.Empty;
                this.KAISHUU_BETSU_NICHIGO.Enabled = false;
            }
        }
        /// <summary>
        /// 振込元銀行検索ポップアップ実行後処理
        /// </summary>
        public void FURI_KOMI_MOTO_BANK_CD_PopupAfterExecuteMethod()
        {
            // 検索ポップアップからCDを設定すると紐付く項目がクリアされないのでここで対策
            if (!this.beforeFURIKOMI_BANK_MOTO_CD.Equals(this.FURI_KOMI_MOTO_BANK_CD.Text))
            {
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                this.previousBankShitenMotoCd = string.Empty;
            }
        }
        /// <summary>
        /// 振込元銀行検索ポップアップ実行前処理
        /// </summary>
        public void FURI_KOMI_MOTO_BANK_CD_PopupBeforeExecuteMethod()
        {
            this.beforeFURIKOMI_BANK_MOTO_CD = this.FURI_KOMI_MOTO_BANK_CD.Text;
        }
        /// <summary>
        /// 振込元銀行のバリデートが開始されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void FURI_KOMI_MOTO_BANK_SHITTEN_CD_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var bankCd = this.FURI_KOMI_MOTO_BANK_CD.Text;
            var bankShitenCd = this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text;

            if (String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var message = new MessageBoxShowLogic();
                message.MessageBoxShow("E012", "銀行");
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Focus();
                return;
            }
            if (this.previousBankShitenMotoCd != bankShitenCd)
            {
                this.isBankShitenPopup_moto = false;
            }

            bool catchErr = false;
            if (!String.IsNullOrEmpty(bankCd) && !String.IsNullOrEmpty(bankShitenCd))
            {
                var bankShitenList = this.logic.GetBankShiten(bankCd, bankShitenCd, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (bankShitenList.Count == 0)
                {
                    // 該当なしなのでエラー
                    var message = new MessageBoxShowLogic();
                    message.MessageBoxShow("E011", "銀行支店マスタ");

                    this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                    this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                    this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                    this.FURI_KOMI_MOTO_NAME.Text = string.Empty;
                    this.previousBankShitenMotoCd = string.Empty;

                    this.isBankShitenPopup_moto = false;

                    e.Cancel = true;
                }
                else if (bankShitenList.Count == 1)
                {
                    // 1件該当なので値をセット
                    var bankShiten = bankShitenList.Where(b => b.BANK_CD == bankCd && b.BANK_SHITEN_CD == bankShitenCd).DefaultIfEmpty(new M_BANK_SHITEN()).FirstOrDefault();
                    this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = bankShiten.BANK_SHIETN_NAME_RYAKU;
                    this.FURI_KOMI_MOTO_SHURUI.Text = bankShiten.KOUZA_SHURUI;
                    this.FURI_KOMI_MOTO_NO.Text = bankShiten.KOUZA_NO;
                    this.FURI_KOMI_MOTO_NAME.Text = bankShiten.KOUZA_NAME;

                    this.previousBankShitenMotoCd = bankShitenCd;

                    this.isBankShitenPopup_moto = false;
                }
                else if (bankShitenList.Count > 1)
                {
                    if (false == this.isBankShitenPopup_moto && this.previousBankShitenMotoCd != bankShitenCd)
                    {
                        // 複数該当なのでポップアップ表示
                        CustomControlExtLogic.PopUp(this.FURI_KOMI_MOTO_BANK_SHITTEN_CD);
                        this.isBankShitenPopup_moto = true;

                        e.Cancel = true;
                    }
                    else
                    {
                        this.isBankShitenPopup_moto = false;
                    }

                    this.previousBankShitenMotoCd = bankShitenCd;
                }
            }
            else
            {
                this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
                this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
                this.FURI_KOMI_MOTO_NO.Text = string.Empty;
                this.FURI_KOMI_MOTO_NAME.Text = string.Empty;

                this.previousBankShitenMotoCd = String.Empty;

                this.isBankShitenPopup_moto = false;
            }

            LogUtility.DebugMethodEnd();
        }
        internal virtual void FurikomiMotoBankCdValidated(object sender, EventArgs e)
        {
            this.logic.FurikomiMotoBankCdValidated();
        }
        internal void FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            string shuruiCd = this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text;
            if (string.IsNullOrEmpty(shuruiCd))
            {
                this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
            }
            else
            {
                if (shuruiCd.Equals("1"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "普通預金";
                }
                else if (shuruiCd.Equals("2"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "当座預金";
                }
                else if (shuruiCd.Equals("9"))
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = "その他";
                }
                else
                {
                    this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
                }
            }
        }
        private void FURIKOMI_SAKI_BANK_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaFurisakiBankCheck(textbox))
            {
                e.Cancel = true;
            }
        }

        private void FURIKOMI_SAKI_BANK_SHITEN_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaFurisakiBankShitenCheck(textbox))
            {
                e.Cancel = true;
            }
        }

        private void FURIKOMI_SAKI_BANK_KOUZA_NAME_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox textbox = (CustomTextBox)sender;
            if (!this.HanKatakanaCheck(textbox))
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaFurisakiBankCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる
            //スペース、数字、英小文字、英大文字、記号「()」「.」「/」「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z()\./\-\uFF67-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }
        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public bool HanKatakanaFurisakiBankShitenCheck(CustomTextBox CheckControl)
        {
            bool IsKatakanaFlagr = true;

            if (string.IsNullOrEmpty(CheckControl.GetResultText()))
            {
                return IsKatakanaFlagr;
            }

            //半角カタカナか調べる
            //スペース、数字、英小文字、英大文字、記号「-」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z()\-\uFF67-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = CheckControl.DisplayItemName;
                MessageBoxShowLogic message = new MessageBoxShowLogic();
                message.MessageBoxShow("E014", itemName);
            }
            return IsKatakanaFlag;
        }
        internal void FURIKOMI_EXPORT_KBN_TextChanged(object sender, EventArgs e)
        {
            string furikomiKbn = this.FURIKOMI_EXPORT_KBN.Text;
            this.FURIKOMI_SAKI_BANK_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_SHITEN_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_NO.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Text = string.Empty;
            this.FURIKOMI_SAKI_BANK_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_NAME.Enabled = false;
            this.FURIKOMI_SAKI_BANK_SHITEN_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_NO.Enabled = false;
            this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Enabled = false;

            this.TEI_SUU_RYOU_KBN.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_CD.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_NAME.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Text = string.Empty;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_NAME.Text = string.Empty;
            this.FURI_KOMI_MOTO_SHURUI.Text = string.Empty;
            this.FURI_KOMI_MOTO_NO.Text = string.Empty;
            this.FURI_KOMI_MOTO_NAME.Text = string.Empty;

            this.TEI_SUU_RYOU_KBN.Enabled = false;
            this.TEI_SUU_RYOU_KBN_1.Enabled = false;
            this.TEI_SUU_RYOU_KBN_2.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_CD.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_POPUP.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = false;
            this.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = false;
            if (furikomiKbn.Equals("1"))
            {
                this.FURIKOMI_SAKI_BANK_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_NAME.Enabled = true;
                this.FURIKOMI_SAKI_BANK_SHITEN_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_SHITEN_NAME.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_SHURUI_CD.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_NO.Enabled = true;
                this.FURIKOMI_SAKI_BANK_KOUZA_NAME.Enabled = true;

                this.TEI_SUU_RYOU_KBN.Text = "1";
                this.TEI_SUU_RYOU_KBN.Enabled = true;
                this.TEI_SUU_RYOU_KBN_1.Enabled = true;
                this.TEI_SUU_RYOU_KBN_2.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_CD.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_POPUP.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_CD.Enabled = true;
                this.FURI_KOMI_MOTO_BANK_SHITTEN_POPUP.Enabled = true;
                this.logic.SetDefaultValueFromFURIKOMI_BANK_MOTO();
            }
        }
        //160026 E
    }
}
