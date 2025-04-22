using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.HikiaiGyousha.Const;
using Shougun.Core.Master.HikiaiGyousha.Logic;
using r_framework.Authority;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.HikiaiGyousha.APP
{
    /// <summary>
    /// 引合業者入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 引合業者入力画面ロジック
        /// </summary>
        private LogicCls logic;

        // 201400709 syunrei #947 №19　start
        //メッセージ
        public MessageBoxShowLogic messBSL = new MessageBoxShowLogic();
        // 201400709 syunrei #947 №19　end
        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG)
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
        /// コンストラクタ(【修正】【削除】【複写】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="gyoushaCd">選択されたデータの入金先CD</param>
        /// <param name="denshiShinseiFlg">True：電子申請で使用 False：電子申請で使用せず</param>
        public UIForm(WINDOW_TYPE windowType, string gyoushaCd, bool denshiShinseiFlg)
            : base(WINDOW_ID.M_HIKIAI_GYOUSHA_NYUURYOKU, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, gyoushaCd, denshiShinseiFlg);

                InitializeComponent();

                // この２つのプロパティは、デザイナー上でNoneに設定しても、
                // フレームワーク側で上書きされてしまうためここで再設定します。
                this.GYOUSHA_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.GYOUSHA_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.SEIKYUU_SOUFU_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.SEIKYUU_SOUFU_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.SHIHARAI_SOUFU_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.SHIHARAI_SOUFU_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.MANI_HENSOUSAKI_KEISHOU1.AutoCompleteMode = AutoCompleteMode.None;
                this.MANI_HENSOUSAKI_KEISHOU2.AutoCompleteMode = AutoCompleteMode.None;
                this.GYOUSHA_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.GYOUSHA_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.SEIKYUU_SOUFU_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.SEIKYUU_SOUFU_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.SHIHARAI_SOUFU_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.SHIHARAI_SOUFU_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;
                this.MANI_HENSOUSAKI_KEISHOU1.AutoCompleteSource = AutoCompleteSource.None;
                this.MANI_HENSOUSAKI_KEISHOU2.AutoCompleteSource = AutoCompleteSource.None;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                this.logic.GyoushaCd = gyoushaCd;
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

                //※※※　何故か、現場分類タブが一度選択されないと修正時に敬称がうまくセットされないので
                //※※※　強制的に一度全タブを選択して戻すようにすることで一旦解決
                TabPage now = this.JOHOU.SelectedTab;
                foreach (TabPage page in this.JOHOU.TabPages)
                {
                    this.JOHOU.SelectedTab = page;
                }
                this.JOHOU.SelectedTab = now;
                //※※※　強引な対応ここまで

                this.logic.WindowInit(base.WindowType);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.JOHOU != null)
                {
                    this.JOHOU.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.GENBA_ICHIRAN != null)
                {
                    this.GENBA_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

        // 201400709 syunrei #947 №19　start
        /// <summary>
        /// 【移行】ボタンを押下する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Ikou(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M215", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                if (messBSL.MessageBoxShow("C074") == DialogResult.Yes)
                {
                    string gyoshaCd = this.GYOUSHA_CD.Text;
                    //登録処理を行う
                    var parentForm = (BusinessBaseForm)this.Parent;
                    //登録ボタン(F9)イベント呼出
                    parentForm.bt_func9.PerformClick();

                    // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする start
                    //移行前のデータ取得
                    M_HIKIAI_GYOUSHA ikouBeforeData = new M_HIKIAI_GYOUSHA();
                    //検索条件設定
                    M_HIKIAI_GYOUSHA gyoCd = new M_HIKIAI_GYOUSHA();
                    gyoCd.GYOUSHA_CD = gyoshaCd;
                    bool catchErr = true;
                    ikouBeforeData = this.logic.ikouBeforeData(gyoCd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    // 20140718 EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する start
                    if (ikouBeforeData.TORIHIKISAKI_UMU_KBN == 1)
                    {
                        if (ikouBeforeData.HIKIAI_TORIHIKISAKI_USE_FLG)
                        {
                            messBSL.MessageBoxShow("E185", "引合取引先");
                            return;
                        }
                        else
                        {
                            string torihikisaki = string.Empty;
                            torihikisaki = this.logic.getTorihikisakiAFTER(ikouBeforeData.TORIHIKISAKI_CD, out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                            if (torihikisaki == "" || string.IsNullOrEmpty(torihikisaki))
                            {
                                messBSL.MessageBoxShow("E185", "引合取引先");
                                return;
                            }
                        }
                    }
                    // 20140718 EV005237_引合取引先を既存取引先に本登録(移行)した時に、引合取引先を使用している引合業者・引合現場の取引先も本登録先に変更する end

                    // トランザクション開始
                    //using (Transaction tran = new Transaction())
                    //{
                    if (!base.RegistErrorFlag)
                    {

                        Transaction tran = new Transaction();
                        try
                        {
                            //引合先マスタから通常マスタへ移行する
                            M_HIKIAI_GYOUSHA ikouBefore = this.logic.CreateIkouData(ikouBeforeData, out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }

                            if (ikouBefore != null && !string.IsNullOrEmpty(ikouBefore.GYOUSHA_CD))
                            {
                                M_GYOUSHA ikouData = new M_GYOUSHA();
                                //引合マスタのデータを通常マスタに変更する
                                ikouData = this.logic.HikiToTsujou(ikouBefore);

                                //引合マスタのデータを通常マスタに移行する
                                if (!this.logic.InsertIkouData(ikouData))
                                {
                                    return;
                                }
                            }

                            // トランザクション終了
                            tran.Commit();

                            if (!this.logic.SaibanErrorFlg)
                            {
                                messBSL.MessageBoxShow("I001", "通常マスタへの移行");
                            }
                        }
                        catch
                        {
                        }
                    }
                    //}
                }
                // 20140718 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする end 
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ikou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        // 201400709 syunrei #947 №19　end

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
                if (!Manager.CheckAuthority("M462", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    return;
                }

                // 処理モード変更
                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                // 画面タイトル変更
                base.HeaderFormInit();

                // 画面初期化
                this.logic.GyoushaCd = string.Empty;
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
                if (Manager.CheckAuthority("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モード変更
                    base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (Manager.CheckAuthority("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    if (!Manager.CheckAuthority("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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

                // 画面タイトル変更
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
        /// 検索処理(現場)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SearchGenba(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TorihikiStopIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenba", ex);
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
                    /// 20141203 Houkakou 「引合業者入力」の日付チェックを追加する　start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合業者入力」の日付チェックを追加する　end
                    
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
                    bool isExit = this.logic.ExistedGyousha(this.GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        && isExit)
                    {
                        messageShowLogic.MessageBoxShow("E202", "既に");
                        return;
                    }
                    #endregion

                    if (!this.logic.CreateHikiaiGyoushaEntity(false))
                    {
                        return;
                    }

                    string sHikiaiGyoushaCd = this.logic.hikiaiGyoushaEntity.GYOUSHA_CD;
                    string sHikiaiGenbaCd = string.Empty;
                    bool HikiaiKihonFlg = false;

                    if (base.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        if (null == this.logic.hikiaiGenbaEntity || (null != this.logic.hikiaiGenbaEntity && SqlBoolean.False == this.logic.hikiaiGenbaEntity.DELETE_FLG))
                        {
                            // 現場マスタ登録確認
                            var messageLogic = new MessageBoxShowLogic();
                            var dialogResult = DialogResult.No;
                            if (null == this.logic.hikiaiGenbaEntity)
                            {
                                dialogResult = messageLogic.MessageBoxShow("C071");
                                this.logic.IsHikiaiGenbaAdd = true;
                            }
                            else
                            {
                                dialogResult = messageLogic.MessageBoxShow("C072");
                                this.logic.IsHikiaiGenbaAdd = false;
                            }
                            if (DialogResult.Yes == dialogResult)
                            {
                                this.logic.CreateHikiaiGenbaEntity();

                                sHikiaiGenbaCd = this.logic.hikiaiGenbaEntity.GENBA_CD;
                                if (!this.logic.hikiaiGenbaEntity.HIKIAI_GYOUSHA_USE_FLG.IsNull)
                                {
                                    HikiaiKihonFlg = this.logic.hikiaiGenbaEntity.HIKIAI_GYOUSHA_USE_FLG.Value;
                                }
                            }
                            else
                            {
                                this.logic.hikiaiGenbaEntity = null;
                            }
                        }
                    }
                    else if (base.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        // 他マスタで使用されているかのチェック
                        if (!this.logic.CheckDelete()) return;
                    }

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
                        //open hikiai genba master
                        if (!string.IsNullOrEmpty(sHikiaiGenbaCd))
                        {
                            switch (base.WindowType)
                            {
                                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                    this.logic.OpenHikiaiGenbaForm(WINDOW_TYPE.UPDATE_WINDOW_FLAG, HikiaiKihonFlg, sHikiaiGyoushaCd, sHikiaiGenbaCd);
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            if (Manager.CheckAuthority("M462", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                            {
                                // DB更新後、新規モードで表示
                                base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.GyoushaCd = string.Empty;
                                this.logic.isRegist = false;
                                this.logic.IsHikiaiGenbaAdd = false;
                                this.logic.hikiaiGenbaEntity = null;
                                this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                            }
                            else
                            {
                                this.FormClose(sender, e);
                            }
                        }
                        else//open mode 参照 after regist
                        {
                            if (Manager.CheckAuthority("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                base.HeaderFormInit();
                                this.logic.GyoushaCd = sHikiaiGyoushaCd;
                                this.logic.isRegist = false;
                                this.logic.IsHikiaiGenbaAdd = false;
                                this.logic.hikiaiGenbaEntity = null;
                                this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
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

        #region 電子申請
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Shinsei(object sender, EventArgs e)
        {
            try
            {
                if (!base.RegistErrorFlag)
                {
                    /// 20141203 Houkakou 「引合業者入力」の日付チェックを追加する　start
                    if (this.logic.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「引合業者入力」の日付チェックを追加する　end
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
                    if (!this.logic.CreateHikiaiGyoushaEntity(false))
                    {
                        return;
                    }
                    this.logic.Shinsei(base.RegistErrorFlag, denshiShinseiEntry, denshiShinseiDetailList);

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
        /// 業者CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns></returns>
        private bool DupliUpdateViewCheck(EventArgs e, bool isRegister)
        {
            bool result = false;

            try
            {
                LogUtility.DebugMethodStart(e, isRegister);

                // 業者CDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.GYOUSHA_CD.Text);

                // 重複チェック
                ConstCls.GyoushaCdLeaveResult isUpdate = this.logic.DupliCheckGyoushaCd(zeroPadCd, isRegister);

                if (isUpdate == ConstCls.GyoushaCdLeaveResult.FALSE_ON)
                {
                    if (Manager.CheckAuthority("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    }
                    else if (Manager.CheckAuthority("M462", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }
                    else
                    {
                        // 権限無しの場合
                        this.GYOUSHA_CD.Text = string.Empty;
                        this.GYOUSHA_CD.Focus();

                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");

                        return result;
                    }

                    // 修正モードで表示する
                    this.logic.GyoushaCd = zeroPadCd;

                    // 画面タイトル変更
                    base.HeaderFormInit();

                    this.GYOUSHA_NAME1.Focus();

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
                else if (isUpdate != ConstCls.GyoushaCdLeaveResult.TURE_NONE)
                {
                    // 入力した業者CDが重複した かつ 修正モード未起動の場合
                    this.GYOUSHA_CD.Text = string.Empty;
                    this.logic.hikiaiGenbaEntity = null;
                    this.GYOUSHA_CD.Focus();
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
                this.messBSL.MessageBoxShow("E093", "");
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.messBSL.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }


        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    this.logic.LogicalDelete();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引中止の表示を切り替えたら現場一覧を再検索
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
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                bool catchErr = true;
                bool isExist = this.logic.ExistedGyousha(this.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                // 本登録済みデータチェック
                if (base.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    && isExist)
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
        /// ユーザーコードチェック処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void FormUserRegistCheck(object source, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(source, e);

                this.logic.FormUserRegistCheck(source, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormUserRegistCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先有無区分変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_UMU_KBN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TabDispControl();
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_UMU_KBN_TextChanged", ex);
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
        private void EIGYOU_TANTOU_BUSHO_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【営業担当者CD】【営業担当者名】をクリアする。
                this.EIGYOU_TANTOU_CD.Text = string.Empty;
                this.SHAIN_NAME.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EIGYOU_TANTOU_BUSHO_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CDの変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【新規】モードの場合のみチェック処理を行う
                if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    bool catchErr = true;
                    bool isExist = this.logic.ExistedGyousha(this.GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 本登録済みデータチェック
                    if (isExist)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E202", "既に");

                        e.Cancel = true;

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validating", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CDフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 【新規】モードの場合のみチェック処理を行う
                if (base.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    return;
                }

                // 入力された業者CD取得
                string inputCd = this.GYOUSHA_CD.Text;
                if (string.IsNullOrWhiteSpace(inputCd))
                {
                    return;
                }

                // 重複チェック
                this.DupliUpdateViewCheck(e, false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.TorihikisakiCopy();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_torihikisaki_copy_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 自社区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JISHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.JISHA_KBN.Checked)
                {
                    // 現場マスタデータを取得する
                    M_HIKIAI_GENBA queryParam = new M_HIKIAI_GENBA();
                    queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                    queryParam.JISHA_KBN = true;
                    bool catchErr = true;
                    bool isOk = this.logic.ManiCheckMsg(queryParam, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        this.JISHA_KBN.Checked = true;
                        // 不整合エラー
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E188", "自社区分");
                        return;
                    }
                }

                this.logic.FlgJishaKbn = this.JISHA_KBN.Checked;
                this.logic.ManiCheckOffCheck();
            }
            catch (Exception ex)
            {
                LogUtility.Error("JISHA_KBN_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 排出事業者区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHUTSU_JIGYOUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked)
                {
                    // 現場マスタデータを取得する
                    M_HIKIAI_GENBA queryParam = new M_HIKIAI_GENBA();
                    queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                    queryParam.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                    bool catchErr = true;
                    bool isOk = this.logic.ManiCheckMsg(queryParam, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = true;
                        // 不整合エラー
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E098", "排出事業場/荷積現場");
                        return;
                    }
                }

                this.logic.FlgHaishutsuJigyoushaKbn = this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked;
                this.logic.ManiCheckOffCheck();
            }
            catch (Exception ex)
            {
                LogUtility.Error("HAISHUTSU_JIGYOUSHA_KBN_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬受託者区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_JUTAKUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked)
                {
                    // 現場マスタデータを取得する
                    M_HIKIAI_GENBA queryParam = new M_HIKIAI_GENBA();
                    queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                    queryParam.TSUMIKAEHOKAN_KBN = true;
                    bool catchErr = true;
                    bool isOk = this.logic.ManiCheckMsg(queryParam, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = true;
                        // 不整合エラー
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E098", "積み替え保管");
                        return;
                    }
                }

                this.logic.FlgUnpanJutakushaKbn = this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked;
                this.logic.ManiCheckOffCheck();
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_JUTAKUSHA_KBN_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 処分受託者区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHOBUN_JUTAKUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked)
                {
                    // 現場マスタデータを取得する
                    M_HIKIAI_GENBA queryParam = new M_HIKIAI_GENBA();
                    queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
                    queryParam.SHOBUN_NIOROSHI_GENBA_KBN = true;
                    bool catchErr = true;
                    bool isOk = this.logic.ManiCheckMsg(queryParam, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOk)
                    {
                        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = true;
                        // 不整合エラー
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E098", "処分事業場/荷降現場");
                        return;
                    }
                }

                this.logic.FlgShobunJutakushaKbn = this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked;
                this.logic.ManiCheckOffCheck();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHOBUN_JUTAKUSHA_KBN_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニ返送先区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANI_HENSOUSAKI_KBN_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.FlgManiHensousakiKbn = this.MANI_HENSOUSAKI_KBN.Checked;
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
                LogUtility.Error("MANI_HENSOUSAKI_KBN_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 荷降業者区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void NIOROSHI_GYOUSHA_KBN_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        if (!string.IsNullOrWhiteSpace(this.logic.GyoushaCd) && !this.NIOROSHI_GYOUSHA_KBN.Checked)
        //        {
        //            // 現場マスタデータを取得する
        //            M_HIKIAI_GENBA queryParam = new M_HIKIAI_GENBA();
        //            queryParam.GYOUSHA_CD = this.logic.GyoushaCd;
        //            queryParam.NIOROSHI_GENBA_KBN = true;
        //            bool catchErr = true;
        //            bool isOk = this.logic.ManiCheckMsg(queryParam, out catchErr);
        //            if (!catchErr)
        //            {
        //                return;
        //            }
        //            if (!isOk)
        //            {
        //                this.NIOROSHI_GYOUSHA_KBN.Checked = true;
        //                // 不整合エラー
        //                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //                msgLogic.MessageBoxShow("E098", "荷卸現場");
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("NIOROSHI_GYOUSHA_KBN_CheckedChanged", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        /// <summary>
        /// 採番ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_gyoushacd_saiban_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 採番値取得
                this.logic.Saiban();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_gyoushacd_saiban_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ポップアップ用営業担当部署取得メソッド
        /// </summary>
        public void SetBushoData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string cd = this.EIGYOU_TANTOU_CD.Text;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    this.logic.SetBushoData(cd);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBushoData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先入力画面を表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_torihikisaki_create_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 取引先画面を表示する
                this.logic.ShowTorihikisakiCreate();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_torihikisaki_create_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場入力画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_CellContentDoubleClick(object sender, CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                this.logic.ShowWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_ICHIRAN_CellContentDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場一覧キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    this.logic.ShowWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_ICHIRAN_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 請求情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_souhusaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.GyoushaInfoCopyFromSeikyuuInfo();
                this.logic.CheckTextBoxLength(this.SEIKYUU_SOUFU_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_souhusaki_torihikisaki_copy_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 支払情報タブ：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shiharai_souhusaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.GyoushaInfoCopyFromShiharaiInfo();
                this.logic.CheckTextBoxLength(this.SHIHARAI_SOUFU_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_shiharai_souhusaki_torihikisaki_copy_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者分類：業者情報コピーボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_hensousaki_torihikisaki_copy_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.GyoushaInfoCopyFromGyoushaBunrui();
                this.logic.CheckTextBoxLength(this.MANI_HENSOUSAKI_ADDRESS1);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_hensousaki_torihikisaki_copy_Click", ex);
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

                string cd = this.EIGYOU_TANTOU_CD.Text;
                if (!string.IsNullOrWhiteSpace(cd))
                {
                    if (!this.logic.SetBushoData(cd))
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.SHAIN_NAME.Text = string.Empty;
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
        /// 都道府県コードテキスト変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_TODOUFUKEN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 都道府県チェック
            this.CheckTodoufuken();
        }

        /// <summary>
        /// 都道府県チェック
        /// </summary>
        internal bool CheckTodoufuken()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ０埋め処理
                if (!string.IsNullOrEmpty(this.GYOUSHA_TODOUFUKEN_CD.Text))
                {
                    var todoufukenCd = Convert.ToInt16(this.GYOUSHA_TODOUFUKEN_CD.Text);
                    this.GYOUSHA_TODOUFUKEN_CD.Text = string.Format("{0:D2}", todoufukenCd);
                }

                if (!this.logic.ChechChiiki(true))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(this.GYOUSHA_TODOUFUKEN_CD.Text))
                {
                    this.TODOUFUKEN_NAME.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTodoufuken", ex1);
                this.messBSL.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTodoufuken", ex);
                this.messBSL.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 住所1フォーカス乖離イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_ADDRESS1_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.ChechChiiki(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_ADDRESS1_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        public void PopupAfterTorihikisakiCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //取引先拠点セット
                this.logic.IsSetDataFromPopup = true;
                if (this.logic.SearchsetTorihikisaki() == -1)
                {
                    return;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                if (!this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue))
                {
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterTorihikisakiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD検索ポップアップ後の処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="rlt"></param>
        public void PopupAfterExecuteTorihikisakiCd(object sender, DialogResult rlt)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, rlt);

                if (rlt != DialogResult.OK && rlt != DialogResult.Yes)
                    return;

                //取引先拠点セット
                this.logic.IsSetDataFromPopup = true;
                if (this.logic.SearchsetTorihikisaki() == -1)
                {
                    return;
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                if (!this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue))
                {
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (this.ActiveControl != null)
                {
                    base.PreviousValue = this.ActiveControl.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopupAfterExecuteTorihikisakiCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CD変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //取引先拠点セット
                this.logic.ChangeTorihikisai();
                this.logic.IsSetDataFromPopup = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_TextChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先CDValidatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.TORIHIKISAKI_CD.Text.Equals(this.PreviousValue) && !this.TORIHIKISAKI_CD.IsInputErrorOccured)
                {
                    if (this.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals(this.hikiaiTorihikisakiUseFlgPreviousValue))
                    {
                        return;
                    }

                }
                //取引先拠点セット
                this.logic.SearchsetTorihikisaki();
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 発行先チェック処理
                if (!this.logic.isError)
                {
                    this.logic.HakkousakuCheck();
                }
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場一覧フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.KeyPreview = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_ICHIRAN_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 現場一覧フォーカス乖離処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_ICHIRAN_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.KeyPreview = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_ICHIRAN_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニ記載業者チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Gyousha_KBN_3_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        //新規画面
        //        if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
        //        {
        //            this.GyousyaKbnEnableControl(this.Gyousha_KBN_3.Checked);
        //        }
        //        //修正画面
        //        else if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
        //        {
        //            if (!this.Gyousha_KBN_3.Checked)
        //            {
        //                if (!this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked
        //                    && !this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked
        //                    && !this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked
        //                    && !this.JISHA_KBN.Checked)
        //                {
        //                    this.GyousyaKbnEnableControl(false);
        //                }
        //                else
        //                {
        //                    this.Gyousha_KBN_3.Checked = true;
        //                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //                    msgLogic.MessageBoxShow("E105");

        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                this.GyousyaKbnEnableControl(true);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("Gyousha_KBN_3_CheckedChanged", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        /// <summary>
        /// マニ記載業者のチェックを判別し、
        /// [マニあり側] もしくは [マニなし側]のどちらか一方を有効にする。
        /// True＝[マニあり側]有効、False＝[マニなし側]有効
        /// </summary>
        /// <param name="gyoushaKbn3Check"></param>
        //private void GyousyaKbnEnableControl(bool gyoushaKbn3Check)
        //{
        //    if (gyoushaKbn3Check)
        //    {
        //        //マニあり側
        //        //this.JISHA_KBN.Enabled = true;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = true;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = true;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = true;

        //        //マニなし側
        //        this.UNPAN_KAISHA_KBN.Enabled = false;
        //        this.UNPAN_KAISHA_KBN.Checked = false;
        //        this.NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        this.NIOROSHI_GYOUSHA_KBN.Checked = false;
        //    }
        //    else
        //    {
        //        //マニあり側
        //        //this.JISHA_KBN.Enabled = false;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Enabled = false;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Enabled = false;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Enabled = false;
        //        //this.JISHA_KBN.Checked = false;
        //        this.HAISHUTSU_NIZUMI_GYOUSHA_KBN.Checked = false;
        //        this.UNPAN_JUTAKUSHA_KAISHA_KBN.Checked = false;
        //        this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Checked = false;

        //        //マニなし側
        //        this.UNPAN_KAISHA_KBN.Enabled = true;
        //        this.NIOROSHI_GYOUSHA_KBN.Enabled = true;
        //    }
        //}

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
                LogUtility.Error("SEIKYUU_KYOTEN_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        private string hikiaiTorihikisakiUseFlgPreviousValue;
        /// <summary>
        /// 取引先CDの前回値を保存する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            hikiaiTorihikisakiUseFlgPreviousValue = this.HIKIAI_TORIHIKISAKI_USE_FLG.Text;
        }

        /// <summary>
        /// 営業担当部署の変更時処理
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

        // 20141209 katen #2927 実績報告書　フィードバック対応 start
        internal void UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.isError = false;
            // 運搬報告書提出先拠点セット
            if (!this.logic.SearchsetUpanHoukokushoTeishutsu())
            {
                e.Cancel = true;
            }
        }
        // 20141209 katen #2927 実績報告書　フィードバック対応 end

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

        /// <summary>
        /// 業者敬称1変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU1_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 業者敬称2変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU2_TextChanged(object sender, EventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
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
        /// 業者敬称1キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU1.DroppedDown = false;
        }

        /// <summary>
        /// 業者敬称2キー押下時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_KEISHOU2_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.GYOUSHA_KEISHOU2.DroppedDown = false;
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

        /// <summary>
        /// BT_TORIHIKISAKI_REFERENCE_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_TORIHIKISAKI_REFERENCE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E290", "取引先CD");
                return;
            }
            this.logic.OpenHikiaiTorihikisakiFormReference(this.TORIHIKISAKI_CD.Text);
        }
    }
}