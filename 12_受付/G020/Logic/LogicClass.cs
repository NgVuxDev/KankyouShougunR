// $Id: LogicClass.cs 55238 2015-07-09 09:21:12Z miya@e-mall.co.jp $

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeaderForm headerForm;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 受付（クレーム）入力のDao
        /// </summary>
        private T_UKETSUKE_CM_ENTRYDao daoUketsukeEntry;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 受付（クレーム）入力検索結果
        /// </summary>
        private DataTable dtUketsukeEntry;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>
        /// EnterかTabボタンが押下されたかどうかの判定フラグ
        /// </summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// HeaderFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearHeaderControlNames = { "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" };
        //2014/01/28 修正 仕様変更 qiao start
        /// <summary>
        /// UIFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearUiFormControlNames = { "UKETSUKE_DATE", "UKETSUKE_DATE_HOUR", "UKETSUKE_DATE_MINUTE", "UKETSUKE_NUMBER", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "GYOUSHA_CD", "GYOUSHA_NAME", "GENBA_CD", "GENBA_NAME", "TAIOUKANRYOU_DETA", "TXT_HYOUDAI", "TXT_TOIAWASESYA", "TXT_NAIYOU1", "TXT_NAIYOU2", "TXT_NAIYOU3", "TXT_NAIYOU4", "TXT_NAIYOU5", "TXT_NAIYOU6", "TXT_NAIYOU7", "TXT_NAIYOU8" };
        //2014/01/28 修正 仕様変更 qiao end
        /// <summary>
        /// HeaderFormのreadonlyコントロール名一覧
        /// </summary>
        private string[] readonlyUiFormControlNames = { "KYOTEN_NAME_RYAKU", "CreateUser", "CreateDate", "LastUpdateUser", "LastUpdateDate" };
        /// <summary>
        /// UIFormのreadonlyコントロール名一覧
        /// </summary>
        private string[] readonlyHeaderControlNames = { "EIGYOU_TANTOUSHA_NAME", "TORIHIKISAKI_NAME", "GYOUSHA_NAME", "GENBA_NAME" };

        /// <summary>
        /// HeaderFormの入力コントロール名一覧
        /// </summary>
        private string[] inputHeaderControlNames = { "KYOTEN_CD" };

        //2014/01/28 修正 仕様変更 qiao start
        /// <summary>
        /// UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames = { "UKETSUKE_DATE", "UKETSUKE_DATE_HOUR", "UKETSUKE_DATE_MINUTE", "UKETSUKE_NUMBER", "EIGYOU_TANTOUSHA_CD", "TORIHIKISAKI_CD", "GYOUSHA_CD", "GENBA_CD", "TAIOUKANRYOU_DETA", "TXT_HYOUDAI", "TXT_TOIAWASESYA", "TXT_NAIYOU1", "TXT_NAIYOU2", "TXT_NAIYOU3", "TXT_NAIYOU4", "TXT_NAIYOU5", "TXT_NAIYOU6", "TXT_NAIYOU7", "TXT_NAIYOU8", "UKETSUKE_PREVIOUS_BUTTON", "UKETSUKE_NEXT_BUTTON", "TORIHIKISAKI_SEARCH_BUTTON", "GYOUSHA_SEARCH_BUTTON", "GENBA_SEARCH_BUTTON", };

        /// <summary>
        /// [参照モード用] UIFormの入力コントロール名一覧
        /// </summary>
        private string[] inputUiFormControlNames_Reference = { "UKETSUKE_DATE", "UKETSUKE_DATE_HOUR", "UKETSUKE_DATE_MINUTE", "UKETSUKE_NUMBER", "EIGYOU_TANTOUSHA_CD", "TORIHIKISAKI_CD", "GYOUSHA_CD", "GENBA_CD", "TAIOUKANRYOU_DETA", "TXT_HYOUDAI", "TXT_TOIAWASESYA", "TXT_NAIYOU1", "TXT_NAIYOU2", "TXT_NAIYOU3", "TXT_NAIYOU4", "TXT_NAIYOU5", "TXT_NAIYOU6", "TXT_NAIYOU7", "TXT_NAIYOU8", "TORIHIKISAKI_SEARCH_BUTTON", "GYOUSHA_SEARCH_BUTTON", "GENBA_SEARCH_BUTTON", };

        //2014/01/28 修正 仕様変更 qiao end
        /// <summary>
        /// 受付クレーム入力Entityを格納
        /// </summary>
        private List<T_UKETSUKE_CM_ENTRY> insEntryEntityList = new List<T_UKETSUKE_CM_ENTRY>();

        /// <summary>
        /// 受付クレーム入力を削除Entity
        /// </summary>
        private T_UKETSUKE_CM_ENTRY delEntryEntity = new T_UKETSUKE_CM_ENTRY();

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;
                // ControlUtility
                this.controlUtil = new ControlUtility();
                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();
                // DTO
                this.dto = new DTOClass();
                // 受付（クレーム）入力のDao
                this.daoUketsukeEntry = DaoInitUtility.GetComponent<T_UKETSUKE_CM_ENTRYDao>();
                // システム情報Dao
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
                this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 初期処理

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                headerForm = (UIHeaderForm)parentForm.headerForm;

                // システム情報を取得
                this.GetSysInfoInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面初期表示処理
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                string KYOTEN_CD = this.GetUserProfileValue(userProfile, "拠点CD");
                CheckKyotenCd(KYOTEN_CD);
                this.DisplayInit();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

                // ﾎﾞﾀﾝEnabled制御
                var controlUtil = new ControlUtility();
                foreach (var button in buttonSetting)
                {
                    var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                    if (cont != null && !string.IsNullOrEmpty(cont.Text))
                    {
                        cont.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 新規ボタン(F2)イベント
                parentForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);

                // 修正ボタン(F3)イベント
                parentForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);

                // 一覧ボタン(F7)イベント
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                //登録ボタン(F9)イベント生成
                //this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // コントロールのイベント(諸口設定関連)
                this.form.TORIHIKISAKI_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GYOUSHA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
                this.form.GENBA_CD.PreviewKeyDown += new PreviewKeyDownEventHandler(this.PreviewKeyDownForShokuchikbnCheck);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region モード別の初期表示処理
        /// <summary>
        /// モード別の初期表示処理
        /// </summary>
        internal bool DisplayInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // ボタンのテキストを初期化
                this.ButtonInit();
                // モードより必須項目設定
                this.SetRegistCheck();
                // 値クリア
                this.ClearControls();
                // フィールドクリア
                this.ClearFields();

                //連動ティクスボックスのインターイベントの初期化処理
                this.EnterEventInit();
                this.parentForm.txb_process.Enabled = false;

                int count = 0;
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 新規モードの場合
                        // 諸口関連項目のReadonly初期設定
                        this.SetShokuchiControlsReadonlyProperty();
                        // 20160121 chenzz 諸口関連修正 end

                        // 活性・非活性制御
                        this.ChangeEnabledForInputControl(false);
                        this.ChangeEnabledForReadonlyControl(false);
                        // 受付番号ありの場合
                        if (!string.IsNullOrEmpty(this.form.UketsukeNumber))
                        {
                            // データを検索
                            count = this.Search();
                            if (count < 0)
                            {
                                ret = false;
                                return ret;
                            }
                            else if (count > 0)
                            {
                                // データを表示
                                this.SetValueToForm();
                                // 受付番号をクリア
                                this.form.UKETSUKE_NUMBER.Text = string.Empty;
                            }

                            this.form.UketsukeNumber = string.Empty;
                        }
                        //2014/01/28 修正 仕様変更 qiao start
                        else
                        {
                            // 受付日初期値設定
                            this.form.UKETSUKE_DATE.Value = parentForm.sysDate.Date;
                            this.form.UKETSUKE_DATE_HOUR.Text = parentForm.sysDate.Hour.ToString();
                            this.form.UKETSUKE_DATE_MINUTE.Text = (parentForm.sysDate.Minute / 5 * 5).ToString();
                        }
                        //2014/01/28 修正 仕様変更 qiao end

                        // chenzz 20160121 複写すると諸口の場合、名称入力ができない　start
                        if (!this.CheckTorihikisakiShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        if (!this.CheckGyoushaShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        if (!this.CheckGenbaShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        // chenzz 20160121 複写すると諸口の場合、名称入力ができない　end

                        this.headerForm.windowTypeLabel.Text = "新規";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 更新モードの場合
                        // 諸口関連項目のReadonly初期設定
                        this.SetShokuchiControlsReadonlyProperty();
                        // 20160121 chenzz 諸口関連修正 end

                        // 活性・非活性制御
                        this.ChangeEnabledForInputControl(false);
                        this.ChangeEnabledForReadonlyControl(false);
                        // 受付番号がある場合
                        if (!string.IsNullOrEmpty(this.form.UketsukeNumber))
                        {
                            // データを検索
                            count = this.Search();
                            if (count < 0)
                            {
                                ret = false;
                                return ret;
                            }
                            else if (count == 0)
                            {
                                // 受付番号をセット
                                this.form.UKETSUKE_NUMBER.Text = this.form.UketsukeNumber;
                                // メッセージ表示
                                msgLogic.MessageBoxShow("E045");
                                ret = false;
                                // 処理終了
                                return ret;
                            }

                            // データを表示
                            this.SetValueToForm();
                        }
                        //20150928 hoanghm start
                        ////ThangNguyen [Add] 20150825 #10907 Start
                        //this.CheckTorihikisaki();
                        //this.CheckGyousha();
                        //this.CheckGenba();
                        ////ThangNguyen [Add] 20150825 #10907 End
                        if (!this.CheckTorihikisakiShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        if (!this.CheckGyoushaShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        if (!this.CheckGenbaShokuchi())
                        {
                            ret = false;
                            return ret;
                        }
                        //20150928 hoanghm end

                        this.headerForm.windowTypeLabel.Text = "修正";
                        this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                        this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 削除・参照モードの場合
                        // すべてをReadOnlyにしたいので初期化の最後に実施
                        // データを検索
                        count = this.Search();
                        if (count < 0)
                        {
                            ret = false;
                            return ret;
                        }
                        else if (count == 0)
                        {
                            // 受付番号をセット
                            this.form.UKETSUKE_NUMBER.Text = this.form.UketsukeNumber;
                            // メッセージ表示
                            msgLogic.MessageBoxShow("E045");
                            // フォーカス設定
                            this.form.UKETSUKE_NUMBER.Focus();
                            ret = false;
                            // 処理終了
                            return ret;
                        }
                        // データを表示
                        this.SetValueToForm();
                        this.ChangeEnabledForInputControl(true);
                        this.ChangeEnabledForReadonlyControl(true);

                        if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.headerForm.windowTypeLabel.Text = "削除";
                            this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                            this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            this.headerForm.windowTypeLabel.Text = "参照";
                            this.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                            this.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                        }

                        break;
                    default:
                        break;
                }

                // コントロールの表示・ReadOnly制御
                this.ControlInit();
                //2013/12/24 追加 start
                // フォーカス設定
                this.form.UKETSUKE_DATE.Focus();
                //2013/12/24 追加 end
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DisplayInit", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DisplayInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd(string KYOTEN_CD)
        {
            if (string.IsNullOrEmpty(KYOTEN_CD))
            {
                this.headerForm.KYOTEN_CD.Text = string.Empty;
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            var kyotens = this.kyotenDao.GetDataByCd(KYOTEN_CD);
            // 存在チェック
            if (kyotens != null)
            {
                this.headerForm.KYOTEN_CD.Text = KYOTEN_CD.PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyotens.KYOTEN_NAME_RYAKU.ToString();
            }
            else
            {
                //拠点CD、拠点 : ブランク
                this.headerForm.KYOTEN_CD.Text = string.Empty;
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
            }
        }
        #endregion

        #region モードより、必須項目設定
        /// <summary>
        /// モードより、必須項目設定
        /// </summary>
        internal void SetRegistCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                switch (this.form.WindowType)
                {
                    // 新規モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 削除モードの場合
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 受付番号
                        this.form.lblUKETSUKE_NUMBER.Text = "受付番号";
                        this.form.UKETSUKE_NUMBER.RegistCheckMethod = null;

                        break;

                    // 修正モードの場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 必須チェック設定
                        SelectCheckDto existCheck = new SelectCheckDto();
                        existCheck.CheckMethodName = "必須チェック";
                        Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                        excitChecks.Add(existCheck);

                        // 受付番号
                        this.form.lblUKETSUKE_NUMBER.Text = "受付番号※";
                        this.form.UKETSUKE_NUMBER.RegistCheckMethod = excitChecks;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 受付番号変わった後、連動ティクスボックスのイベントの初期化処理
        /// <summary>
        /// 受付番号変わった後、連動ティクスボックスのインターイベントの初期化処理
        /// </summary>
        private void EnterEventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 取引先
                this.form.TORIHIKISAKI_CD.Enter -= this.form.Control_Enter;
                this.form.TORIHIKISAKI_CD.Enter += this.form.Control_Enter;

                //業者
                this.form.GYOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.GYOUSHA_CD.Enter += this.form.Control_Enter;
                //現場
                this.form.GENBA_CD.Enter -= this.form.Control_Enter;
                this.form.GENBA_CD.Enter += this.form.Control_Enter;
                //営業担当者
                this.form.EIGYOU_TANTOUSHA_CD.Enter -= this.form.Control_Enter;
                this.form.EIGYOU_TANTOUSHA_CD.Enter += this.form.Control_Enter;

                //受付番号
                this.form.UKETSUKE_NUMBER.Enter -= this.form.Control_Enter;
                this.form.UKETSUKE_NUMBER.Enter += this.form.Control_Enter;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 入力コントロールの活性・非活性制御
        /// <summary>
        /// 入力コントロールの活性・非活性制御
        /// </summary>
        /// <param name="isLock">ロック状態に設定するbool</param>
        internal void ChangeEnabledForInputControl(bool isLock)
        {
            try
            {
                LogUtility.DebugMethodStart(isLock);

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();

                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 参照モード時は前・次ボタンは操作可能にするため分けている。
                    formControlNameList.AddRange(inputUiFormControlNames_Reference);
                }
                else
                {
                    formControlNameList.AddRange(inputUiFormControlNames);
                }

                formControlNameList.AddRange(inputHeaderControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.form, controlName);

                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    var property = control.GetType().GetProperty("ReadOnly");

                    if (property != null)
                    {
                        property.SetValue(control, isLock, null);
                    }

                    property = control.GetType().GetProperty("Enabled");

                    if (property != null)
                    {
                        property.SetValue(control, !isLock, null);
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Readonlyコントロールの活性・非活性制御
        /// </summary>
        /// <param name="isLock"></param>
        internal void ChangeEnabledForReadonlyControl(bool isLock)
        {

            try
            {
                LogUtility.DebugMethodStart(isLock);

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(readonlyUiFormControlNames);
                formControlNameList.AddRange(readonlyHeaderControlNames);
                foreach (var controlName in formControlNameList)
                {
                    Control control = controlUtil.FindControl(this.form, controlName);

                    if (control == null)
                    {
                        // headerを検索
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    var property = control.GetType().GetProperty("ReadOnly");
                    if ((property != null && property.GetValue(control, null).Equals(true)) || isLock)
                    {
                        property = control.GetType().GetProperty("Enabled");

                        if (property != null)
                        {
                            property.SetValue(control, !isLock, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region コントロールの表示・ReadOnly制御
        /// <summary>
        /// コントロールの表示・ReadOnly制御
        /// </summary>
        internal void ControlInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                switch (this.form.WindowType)
                {
                    // 新規モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = false;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;
                        break;

                    // 修正モードの場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;
                        break;

                    // 削除モードの場合
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = false;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = false;
                        break;

                    // 参照モードの場合
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        // 受付番号入力不可
                        this.form.UKETSUKE_NUMBER.ReadOnly = true;
                        // 受付番号 [前]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_PREVIOUS_BUTTON.Visible = true;
                        // 受付番号 [次]ﾎﾞﾀﾝ
                        this.form.UKETSUKE_NEXT_BUTTON.Visible = true;
                        // [F9]登録ボタン
                        this.parentForm.bt_func9.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コントロールの値をクリア
        /// <summary>
        /// コントロールの値をクリア
        /// </summary>
        internal void ClearControls()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(clearUiFormControlNames);
                formControlNameList.AddRange(clearHeaderControlNames);
                // 新規モード以外の場合
                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 拠点クリア
                    string[] headTmp = { "KYOTEN_CD", "KYOTEN_NAME_RYAKU" };
                    formControlNameList.AddRange(headTmp);
                }

                foreach (var controlName in formControlNameList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        // ヘッダフォームからコントロールを取得
                        control = controlUtil.FindControl(this.headerForm, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    PropertyInfo property;
                    // 日付コントロールの場合
                    if (control is CustomDateTimePicker)
                    {
                        // Valueをクリア
                        ((CustomDateTimePicker)control).Value = null;
                    }
                    else
                    {
                        // Textプロパティを取得
                        property = control.GetType().GetProperty("Text");
                        if (property != null)
                        {
                            // クリア
                            property.SetValue(control, string.Empty, null);
                        }
                    }

                    // IsInputErrorOccuredプロパティを取得
                    property = control.GetType().GetProperty("IsInputErrorOccured");
                    if (property != null)
                    {
                        // クリア
                        property.SetValue(control, false, null);
                    }
                }
                //受付日:システム日付
                this.form.UKETSUKE_DATE.Value = parentForm.sysDate;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region フィールド値をクリア
        /// <summary>
        /// フィールド値をクリア
        /// </summary>
        internal void ClearFields()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // Form関連
                this.form.dicControl.Clear();

                // 受付クレーム入力関連
                this.insEntryEntityList.Clear();
                this.delEntryEntity = null;
            }
            catch (Exception ex)
            {
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

        #region 指定した受付番号のデータが存在するか返す
        /// <summary>
        /// 指定した受付番号のデータが存在するか返す
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistData(string uketsukeNumber)
        {
            // 戻り値初期化
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                if (!string.IsNullOrEmpty(uketsukeNumber))
                {
                    // 検索条件設定
                    this.dto.UketsukeNumber = long.Parse(this.form.UketsukeNumber);
                    // 入力データを検索
                    this.dtUketsukeEntry = this.daoUketsukeEntry.GetDataToDataTable(this.dto);
                    // 0件の場合
                    if (this.dtUketsukeEntry.Rows.Count > 0)
                    {
                        // 戻り値
                        returnVal = true;
                    }
                }
                else if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 戻り値
                    returnVal = true;
                }

                LogUtility.DebugMethodEnd(returnVal);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region SYSTEM_IDを採番
        /// <summary>
        /// SYSTEM_ID採番処理
        /// Entry.SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createSystemIdForUketsuke()
        {
            SqlInt64 returnInt = 1;
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {

                    var entity = new S_NUMBER_SYSTEM();
                    entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();

                    // IS_NUMBER_SYSTEMDao(共通)
                    IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    var updateEntity = numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                    returnInt = numberSystemDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_SYSTEM();
                        updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        numberSystemDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        numberSystemDao.Update(updateEntity);
                    }

                    // コミット
                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnInt);
            }
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // ****************************
                // 受付（クレーム）入力データを検索
                // ****************************
                // 検索条件設定
                this.dto.UketsukeNumber = long.Parse(this.form.UketsukeNumber);
                // 入力データを検索
                this.dtUketsukeEntry = this.daoUketsukeEntry.GetDataToDataTable(this.dto);
                // 件数
                result = this.dtUketsukeEntry.Rows.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                result = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }
        #endregion

        #region 検索結果を画面に表示
        /// <summary>
        /// 検索結果を画面に表示
        /// </summary>
        internal void SetValueToForm()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var dt = this.dtUketsukeEntry;
                dt.BeginLoadData();

                // ヘッダフォーム設定
                // 拠点
                this.headerForm.KYOTEN_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["KYOTEN_CD"], string.Empty).ToString().PadLeft(2, '0');
                this.headerForm.KYOTEN_NAME_RYAKU.Text = this.ChgDBNullToValue(dt.Rows[0]["KYOTEN_NAME_RYAKU"], string.Empty).ToString();

                // 作成者
                this.headerForm.CreateUser.Text = dt.Rows[0]["CREATE_USER"].ToString();
                // 作成日
                this.headerForm.CreateDate.Text = dt.Rows[0]["CREATE_DATE"].ToString();
                // 更新者
                this.headerForm.LastUpdateUser.Text = dt.Rows[0]["UPDATE_USER"].ToString();
                // 更新日
                this.headerForm.LastUpdateDate.Text = dt.Rows[0]["UPDATE_DATE"].ToString();

                // 受付日
                string strDate = string.Empty;
                string strHour = string.Empty;
                string strMinute = string.Empty;
                if (WINDOW_TYPE.NEW_WINDOW_FLAG != this.form.WindowType)
                {
                    if (this.TryChgDateTimeToDHM(this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_DATE"], null), out strDate, out strHour, out strMinute))
                    {
                        this.form.UKETSUKE_DATE.Value = (DateTime)dt.Rows[0]["UKETSUKE_DATE"];
                        this.form.UKETSUKE_DATE_HOUR.Text = strHour;
                        this.form.UKETSUKE_DATE_MINUTE.Text = strMinute;
                    }
                }
                else
                {
                    this.form.UKETSUKE_DATE.Value = parentForm.sysDate.Date;
                    this.form.UKETSUKE_DATE_HOUR.Text = parentForm.sysDate.Hour.ToString();
                    this.form.UKETSUKE_DATE_MINUTE.Text = (parentForm.sysDate.Minute / 5 * 5).ToString();
                }

                // 受付番号
                this.form.UKETSUKE_NUMBER.Text = this.ChgDBNullToValue(dt.Rows[0]["UKETSUKE_NUMBER"], string.Empty).ToString();

                // 営業担当
                this.form.EIGYOU_TANTOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["EIGYOU_TANTOUSHA_CD"], string.Empty).ToString();
                this.form.EIGYOU_TANTOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["EIGYOU_TANTOUSHA_NAME"], string.Empty).ToString();

                // 取引先
                this.form.TORIHIKISAKI_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["TORIHIKISAKI_CD"], string.Empty).ToString();
                this.form.TORIHIKISAKI_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["TORIHIKISAKI_NAME"], string.Empty).ToString();

                // 業者
                this.form.GYOUSHA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["GYOUSHA_CD"], string.Empty).ToString();
                this.form.GYOUSHA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["GYOUSHA_NAME"], string.Empty).ToString();

                // 現場
                this.form.GENBA_CD.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_CD"], string.Empty).ToString();
                this.form.GENBA_NAME.Text = this.ChgDBNullToValue(dt.Rows[0]["GENBA_NAME"], string.Empty).ToString();

                // 対応完了日(ブランク||TAIOU_END__DATE)
                this.form.TAIOUKANRYOU_DETA.Value = dt.Rows[0]["TAIOU_END__DATE"];

                // 表題
                this.form.TXT_HYOUDAI.Text = this.ChgDBNullToValue(dt.Rows[0]["TITLE_NAME"], string.Empty).ToString();

                // 先方問合せ者
                this.form.TXT_TOIAWASESYA.Text = this.ChgDBNullToValue(dt.Rows[0]["SENPOU_TOIAWASE_USER"], string.Empty).ToString();

                // 内容
                this.form.TXT_NAIYOU1.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_1"], string.Empty).ToString();
                this.form.TXT_NAIYOU2.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_2"], string.Empty).ToString();
                this.form.TXT_NAIYOU3.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_3"], string.Empty).ToString();
                this.form.TXT_NAIYOU4.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_4"], string.Empty).ToString();
                this.form.TXT_NAIYOU5.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_5"], string.Empty).ToString();
                this.form.TXT_NAIYOU6.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_6"], string.Empty).ToString();
                this.form.TXT_NAIYOU7.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_7"], string.Empty).ToString();
                this.form.TXT_NAIYOU8.Text = this.ChgDBNullToValue(dt.Rows[0]["NAIYOU_8"], string.Empty).ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 前の受付番号を取得
        /// <summary>
        /// 前の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>前の受付番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            String kyoten = this.headerForm.KYOTEN_CD.Text;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPreviousNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }
        #endregion

        #region 次の受付番号を取得
        /// <summary>
        /// 次の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>次の受付番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            String kyoten = this.headerForm.KYOTEN_CD.Text;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;

                // データ取得
                dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 " + "AND KYOTEN_CD = " + kyoten;
                    // データ取得
                    dt = this.daoUketsukeEntry.GetDateForStringSql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }
        #endregion

        #region 受付番号を採番
        /// <summary>
        /// 受付番号を採番
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createUketsukeNumber()
        {
            SqlInt64 returnInt = -1;
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    var entity = new S_NUMBER_DENSHU();
                    entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
                    var updateEntity = numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                    returnInt = numberDenshuDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_DENSHU();
                        updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.UKETSUKE.GetHashCode();
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        numberDenshuDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        numberDenshuDao.Update(updateEntity);
                    }

                    // コミット
                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnInt);
            }
        }
        #endregion

        #region Entitys作成

        #region Entitysデータを作成
        /// <summary>
        /// Entitysデータを作成
        /// </summary>
        public bool CreateEntitys()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // Entityデータをクリア
                this.insEntryEntityList.Clear();

                // 画面モード
                switch (this.form.WindowType)
                {
                    // 登録処理
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // ***************
                        // 登録データ作成
                        // ***************
                        // FirstシステムIDを退避
                        string systemID = string.Empty;
                        // 受付クレーム入力Entityを作成
                        T_UKETSUKE_CM_ENTRY entryEntity = this.CreateEntryEntity(ref systemID);

                        // 受付クレーム入力Entityをリストに追加
                        this.insEntryEntityList.Add(entryEntity);
                        break;

                    // 更新処理
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // ***************
                        // 削除データ作成
                        // ***************
                        // 受付クレーム入力論理削除Entityを作成
                        this.CreateDelEntryEntity();

                        // ***************
                        // 登録データ作成
                        // ***************
                        // 受付クレーム入力Entityを作成
                        string updSystemID = string.Empty;
                        T_UKETSUKE_CM_ENTRY updEntryEntity = this.CreateEntryEntity(ref updSystemID);

                        // 受付クレーム入力Entityをリストに追加
                        this.insEntryEntityList.Add(updEntryEntity);

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // ***************
                        // 削除データ作成
                        // ***************
                        // 受付クレーム入力論理削除Entityを作成
                        this.CreateDelEntryEntity();

                        // ***************
                        // 登録データ作成
                        // ***************
                        // 受付クレーム入力Entityを作成
                        string delSystemID = string.Empty;
                        T_UKETSUKE_CM_ENTRY delEntryEntity = this.CreateEntryEntity(ref delSystemID);

                        // 受付クレーム入力Entityをリストに追加
                        this.insEntryEntityList.Add(delEntryEntity);

                        break;

                    default:
                        break;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntitys", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntitys", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 論理削除のEntryEntityを作成
        /// <summary>
        /// 論理削除のEntityを作成
        /// </summary>
        private void CreateDelEntryEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.delEntryEntity = new T_UKETSUKE_CM_ENTRY();

                // SYSTEM_ID(元データのシステムID)
                this.delEntryEntity.SYSTEM_ID = (long)dtUketsukeEntry.Rows[0]["SYSTEM_ID"];

                // SEQ(元データのSEQ)
                this.delEntryEntity.SEQ = (int)dtUketsukeEntry.Rows[0]["SEQ"];

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_CM_ENTRY>(this.delEntryEntity);
                dbLogic.SetSystemProperty(this.delEntryEntity, false);

                // 削除フラグ
                this.delEntryEntity.DELETE_FLG = true;

                // TIME_STAMP
                this.delEntryEntity.TIME_STAMP = (byte[])dtUketsukeEntry.Rows[0]["TIME_STAMP"];
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録用のEntryEntityを作成
        /// <summary>
        /// 登録用のEntityを作成
        /// </summary>
        /// <param name="systemID">システムID</param>
        /// <param name="no">車輌台数連番</param>
        /// <returns>T_UKETSUKE_CM_ENTRY</returns>
        private T_UKETSUKE_CM_ENTRY CreateEntryEntity(ref string systemID)
        {
            T_UKETSUKE_CM_ENTRY entryEntity = new T_UKETSUKE_CM_ENTRY();
            try
            {
                LogUtility.DebugMethodStart(systemID);
                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_CM_ENTRY>(entryEntity);
                dbLogic.SetSystemProperty(entryEntity, false);
                // 画面モード
                switch (this.form.WindowType)
                {
                    // 登録処理の場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // SYSTEM_IDの採番
                        entryEntity.SYSTEM_ID = this.createSystemIdForUketsuke();
                        if (string.IsNullOrEmpty(systemID))
                        {
                            systemID = entryEntity.SYSTEM_ID.ToString();
                        }

                        // 受入番号の採番
                        entryEntity.UKETSUKE_NUMBER = this.createUketsukeNumber();

                        // SEQ
                        entryEntity.SEQ = 1;
                        break;

                    // 更新処理の場合
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // SYSTEM_ID(元データのシステムID)
                        entryEntity.SYSTEM_ID = (long)dtUketsukeEntry.Rows[0]["SYSTEM_ID"];

                        // 受入番号(元データの受入番号)
                        entryEntity.UKETSUKE_NUMBER = (long)dtUketsukeEntry.Rows[0]["UKETSUKE_NUMBER"];

                        // SEQ（+1連番）
                        entryEntity.SEQ = ((int)dtUketsukeEntry.Rows[0]["SEQ"] + 1);

                        // 作成者
                        entryEntity.CREATE_USER = dtUketsukeEntry.Rows[0]["CREATE_USER"].ToString();
                        // 作成日
                        entryEntity.CREATE_DATE = (DateTime)dtUketsukeEntry.Rows[0]["CREATE_DATE"];
                        // 作成PC
                        entryEntity.CREATE_PC = dtUketsukeEntry.Rows[0]["CREATE_PC"].ToString();

                        break;
                }

                // 拠点
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    entryEntity.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text);
                }

                string strDatetime;
                //受付日
                if (this.TryChgDHMtoDateTime(this.form.UKETSUKE_DATE.Value, out strDatetime))
                {
                    entryEntity.UKETSUKE_DATE = DateTime.Parse(strDatetime);
                }

                // 取引先
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    entryEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    entryEntity.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME.Text;
                }

                // 業者
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    entryEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    entryEntity.GYOUSHA_NAME = this.form.GYOUSHA_NAME.Text;
                }

                // 現場
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    entryEntity.GENBA_CD = this.form.GENBA_CD.Text;
                    entryEntity.GENBA_NAME = this.form.GENBA_NAME.Text;
                }

                // 営業担当
                if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    entryEntity.EIGYOU_TANTOUSHA_CD = this.form.EIGYOU_TANTOUSHA_CD.Text;
                    entryEntity.EIGYOU_TANTOUSHA_NAME = this.form.EIGYOU_TANTOUSHA_NAME.Text;
                }

                //対応完了日
                string strDate = string.Empty;
                string strHour = string.Empty;
                string strMinute = string.Empty;
                if (this.TryChgDateTimeToDHM(this.form.TAIOUKANRYOU_DETA.Value, out strDate, out strHour, out strMinute))
                {
                    entryEntity.TAIOU_END__DATE = strDate;
                }

                //表題
                if (!string.IsNullOrEmpty(this.form.TXT_HYOUDAI.Text))
                {
                    entryEntity.TITLE_NAME = this.form.TXT_HYOUDAI.Text;
                }

                //先方問合せ者
                if (!string.IsNullOrEmpty(this.form.TXT_TOIAWASESYA.Text))
                {
                    entryEntity.SENPOU_TOIAWASE_USER = this.form.TXT_TOIAWASESYA.Text;
                }

                //内容1
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU1.Text))
                {
                    entryEntity.NAIYOU_1 = this.form.TXT_NAIYOU1.Text;
                }
                //内容2
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU2.Text))
                {
                    entryEntity.NAIYOU_2 = this.form.TXT_NAIYOU2.Text;
                }
                //内容3
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU3.Text))
                {
                    entryEntity.NAIYOU_3 = this.form.TXT_NAIYOU3.Text;
                }
                //内容4
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU4.Text))
                {
                    entryEntity.NAIYOU_4 = this.form.TXT_NAIYOU4.Text;
                }
                //内容5
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU5.Text))
                {
                    entryEntity.NAIYOU_5 = this.form.TXT_NAIYOU5.Text;
                }
                //内容6
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU6.Text))
                {
                    entryEntity.NAIYOU_6 = this.form.TXT_NAIYOU6.Text;
                }
                //内容7
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU7.Text))
                {
                    entryEntity.NAIYOU_7 = this.form.TXT_NAIYOU7.Text;
                }
                ////内容8
                if (!string.IsNullOrEmpty(this.form.TXT_NAIYOU8.Text))
                {
                    entryEntity.NAIYOU_8 = this.form.TXT_NAIYOU8.Text;
                }

                // 削除フラグ
                entryEntity.DELETE_FLG = false;

                return entryEntity;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(entryEntity, systemID);
            }
        }
        #endregion

        #endregion

        #region [F9]登録処理
        #region [F9]登録処理
        /// <summary>
        /// [F9]登録処理
        /// </summary>
        [Transaction]
        public bool RegistData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    // **********
                    // 新規登録
                    // **********
                    // 受付クレーム入力レコードをループ
                    foreach (T_UKETSUKE_CM_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeEntry.Insert(entity);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                ret = false;

            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region [F9]更新処理
        /// <summary>
        /// [F9]更新処理
        /// </summary>
        [Transaction]
        public bool UpdateData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    // **********
                    // 論理削除
                    // **********
                    // 受付クレーム入力の元レコードを論理削除
                    this.daoUketsukeEntry.Update(this.delEntryEntity);

                    // **********
                    // 新規登録
                    // **********
                    // 受付クレーム入力レコードをループ
                    foreach (T_UKETSUKE_CM_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUketsukeEntry.Insert(entity);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);

            }
            return ret;
        }
        #endregion

        #region [F9]論理削除処理
        /// <summary>
        /// [F9]論理削除処理
        /// </summary>
        [Transaction]
        public bool LogicalDeleteData()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {
                    // **********
                    // 論理削除
                    // **********
                    // 受付クレーム入力の元レコードを論理削除
                    this.daoUketsukeEntry.Update(this.delEntryEntity);

                    // **********
                    // 新規登録
                    // **********
                    // 受付クレーム入力レコードをループ
                    foreach (T_UKETSUKE_CM_ENTRY entity in this.insEntryEntityList)
                    {
                        // 登録処理を行う
                        entity.DELETE_FLG = true;
                        this.daoUketsukeEntry.Insert(entity);
                    }
                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E245");
                }
                ret = false;

            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        #endregion

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる　処理
        /// </summary>
        internal void CloseForm()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.Close();
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
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
        /// <returns></returns>
        internal string GetTextName(String buttonName)
        {
            string textName = "";
            try
            {
                LogUtility.DebugMethodStart(buttonName);

                switch (buttonName)
                {
                    case "TORIHIKISAKI_SEARCH_BUTTON":
                        textName = "TORIHIKISAKI_CD";
                        break;
                    case "GYOUSHA_SEARCH_BUTTON":
                        textName = "GYOUSHA_CD";
                        break;
                    case "GENBA_SEARCH_BUTTON":
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

        #region データ処理
        #region [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// <summary>
        /// [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// </summary>
        /// <param name="objDate">date</param>
        /// <param name="strDatetime">組合値</param>
        /// <returns>bool</returns>
        private bool TryChgDHMtoDateTime(object objDate, out string strDatetime)
        {
            // 戻り値初期化
            bool returnVal = false;
            //2014/01/28 修正 仕様変更 qiao start
            string date = "";
            string hour = "0";
            string minute = "00";
            string second = "00";
            strDatetime = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(objDate, strDatetime);

                // DateがNullの場合
                if (objDate == null)
                {
                    // 処理終了
                    return returnVal;
                }
                // DateTimeに変換
                DateTime dateTime = (DateTime)objDate;
                // Date
                date = dateTime.Date.ToShortDateString();

                if (!string.IsNullOrEmpty(this.form.UKETSUKE_DATE_HOUR.Text))
                {
                    hour = this.form.UKETSUKE_DATE_HOUR.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UKETSUKE_DATE_MINUTE.Text))
                {
                    minute = this.form.UKETSUKE_DATE_MINUTE.Text;
                }

                strDatetime = date + " " + hour + ":" + minute + ":" + second;
                //2014/01/28 修正 仕様変更 qiao end
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, strDatetime);
            }
        }
        #endregion

        #region DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// <summary>
        /// DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// </summary>
        /// <param name="objDateTime">分解対象</param>
        /// <param name="date">date</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <returns>bool</returns>
        private bool TryChgDateTimeToDHM(object objDateTime, out string date, out string hour, out string minute)
        {
            // 戻り値初期化
            bool returnVal = false;
            date = string.Empty;
            hour = string.Empty;
            minute = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(objDateTime, date, hour, minute);
                // nullの場合
                if (objDateTime == null)
                {
                    // 処理終了
                    return returnVal;
                }
                // DateTimeに変換
                DateTime dateTime = (DateTime)objDateTime;

                // Date
                date = dateTime.Date.ToShortDateString();
                // Hour
                hour = dateTime.Hour.ToString();
                // Minute
                minute = dateTime.Minute.ToString();
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, date, hour, minute);
            }
        }
        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            object returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(obj, value);

                if (obj is DBNull)
                {
                    returnVal = value;
                }
                else
                {
                    returnVal = obj;
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region Null値を指定値に変換
        /// <summary>
        /// Null値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>string</returns>
        private string ChgNullToValue(string obj, string value)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(obj, value);
                if (string.IsNullOrEmpty(obj))
                {
                    returnVal = value;
                }
                else
                {
                    returnVal = obj;
                }
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion
        #endregion

        #region 取引先、業者、現場関連チェック

        #region 取引先チェック
        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, true);

                // 入力されていない場合
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    // 関連項目クリア
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;

                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 取引先を取得
                bool catchErr = true;
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki == null)
                {
                    // 取引先名設定
                    this.form.TORIHIKISAKI_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "取引先");

                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                    return false;
                }

                // 取引先と拠点の関係をチェック
                if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                {
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;

                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                    return false;
                }

                // 取引先を設定
                if (!this.form.dicControl.ContainsKey("TORIHIKISAKI_CD") || !this.form.dicControl["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    // 取引先名設定
                    if (torihikisaki.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);

                // 諸口区分によってフォーカスを制御
                if ((bool)torihikisaki.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.TORIHIKISAKI_NAME);
                }
                else
                {
                    if (this.form.oldShokuchiKbn)
                    {
                        this.form.isNotMoveFocusFW = false;
                    }
                    return true;
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiKyoten(string headerKyotenCd, string torihikisakiCd)
        {
            try
            {
                LogUtility.DebugMethodStart(headerKyotenCd, torihikisakiCd);

                //取引先が空だったらReturn
                if (string.Empty == torihikisakiCd)
                {
                    this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                    return true;
                }

                // 取引先の拠点をチェック
                if (String.IsNullOrEmpty(headerKyotenCd))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E146");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                string oldTorihikisakiCd = string.Empty;
                string oldKyotenCd = string.Empty;
                if (this.dtUketsukeEntry != null && this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    oldTorihikisakiCd = this.ChgDBNullToValue(dtUketsukeEntry.Rows[0]["TORIHIKISAKI_CD"], string.Empty).ToString();
                    oldKyotenCd = this.ChgDBNullToValue(dtUketsukeEntry.Rows[0]["KYOTEN_CD"], string.Empty).ToString().PadLeft(2, '0');
                }
                if (torihikisakiCd == oldTorihikisakiCd && headerKyotenCd == oldKyotenCd)
                {
                    return true;
                }

                bool catchErr = true;
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null == torihikisaki)
                {
                    // 取引先名設定
                    this.form.TORIHIKISAKI_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "取引先");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                var kyotenCd = (int)torihikisaki.TORIHIKISAKI_KYOTEN_CD;
                if (99 != kyotenCd && Convert.ToInt16(headerKyotenCd) != kyotenCd)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E146");

                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var torihikisakiName = this.form.TORIHIKISAKI_NAME.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var gyoushaName = this.form.GYOUSHA_NAME.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                var genbaName = this.form.GENBA_NAME.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, true);
                this.SetCtrlReadonly(this.form.GENBA_NAME, true);

                // 現場項目クリア
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    // 関連項目クリア
                    this.form.GYOUSHA_NAME.Text = String.Empty;

                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 業者を取得
                // (入力エラーチェックは前回値と比較する前に行っているためここではチェックしない）
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                // 業者を設定
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    if ((bool)gyousha.SHOKUCHI_KBN)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end
                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);

                // 取引先を再設定
                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(gyousha.TORIHIKISAKI_CD, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = gyousha.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, gyousha.TORIHIKISAKI_CD))
                    {
                        this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.form.isInputError = true;
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);
                }

                // 諸口区分によってフォーカスを制御
                var zenShokuchi = (bool)gyousha.SHOKUCHI_KBN;
                if ((bool)gyousha.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.GYOUSHA_NAME);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        if (this.form.oldShokuchiKbn)
                        {
                            this.form.isNotMoveFocusFW = false;
                        }
                        return true;
                    }
                }

                // 現場を再設定
                // 現場を取得
                var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != genba)
                {
                    this.form.GENBA_CD.Text = genba.GENBA_CD;
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genba.SHOKUCHI_KBN);
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 業者CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGyousha()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            bool ren = true;

            if (!String.IsNullOrEmpty(gyoushaCd))
            {
                // 業者を取得
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null == gyousha)
                {
                    // 業者名設定
                    this.form.GYOUSHA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");

                    this.form.isInputError = true;
                    this.form.SetFocusControl(this.form.GYOUSHA_CD);
                    ren = false;
                }
            }
            return ren;
        }

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;
                var eigyouTantoushaCd = this.form.EIGYOU_TANTOUSHA_CD.Text;

                // Readonly初期化
                this.SetCtrlReadonly(this.form.GENBA_NAME, true);

                // 入力されてない場合
                if (String.IsNullOrEmpty(genbaCd))
                {
                    if (!this.form.oldShokuchiKbn || this.form.Key.Shift)
                    {
                        // フレームワーク側のフォーカス処理を行わない
                        this.form.isNotMoveFocusFW = true;
                    }
                    else
                    {
                        // フレームワーク側のフォーカス処理を行う
                        this.form.isNotMoveFocusFW = false;
                    }

                    return true;
                }

                // 現場情報を取得
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.form.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    return false;
                }

                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                //マスタに業者CDが存在しない場合
                //又は取引日外の業者CDが選択された場合

                if (null == genba)
                {
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E062", "業者");

                    this.form.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    return false;
                }
                else
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    // 現場が見つかったので現場名などをセット
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genba.SHOKUCHI_KBN);

                    torihikisakiCd = genba.TORIHIKISAKI_CD;
                }

                // 業者を設定
                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    // 業者名
                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end
                this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);

                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisaki)
                {
                    // 取引先設定
                    this.form.TORIHIKISAKI_CD.Text = torihikisaki.TORIHIKISAKI_CD;

                    // 取引先と拠点の関係をチェック
                    if (false == this.CheckTorihikisakiKyoten(this.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                    {
                        this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                        this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        this.form.isInputError = true;
                        return false;
                    }

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end

                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisaki.SHOKUCHI_KBN);
                }

                // 諸口区分によってフォーカスを制御
                if ((bool)genba.SHOKUCHI_KBN)
                {
                    this.form.SetFocusControl(this.form.GENBA_NAME);
                }
                else
                {
                    if (this.form.oldShokuchiKbn)
                    {
                        this.form.isNotMoveFocusFW = false;
                    }
                    return true;
                }

                //コントロールにフォーカスを設定します
                if (!this.form.boolMoveFocusControl())
                {
                    // フレームワーク側のフォーカス処理を行わない
                    this.form.isNotMoveFocusFW = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // 営業担当者を設定
                this.SetEigyouTantousha();

                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 現場CDエラーチェック
        /// </summary>
        /// <returns></returns>
        internal bool ErrorCheckGenba()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            var genbaCd = this.form.GENBA_CD.Text;

            bool ren = true;

            if (string.IsNullOrEmpty(genbaCd))
            {
                // 現場の関連情報をクリア
                this.form.GENBA_NAME.Text = String.Empty;
                return ren;
            }

            // 業者入力されてない場合
            if (String.IsNullOrEmpty(gyoushaCd))
            {
                this.msgLogic.MessageBoxShow("E051", "業者");
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;
                this.form.isInputError = true;
                this.form.SetFocusControl(this.form.GENBA_CD);
                ren = false;
                return ren;
            }

            // 現場情報を取得
            if (!string.IsNullOrEmpty(genbaCd))
            {
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");

                    this.form.isInputError = true;
                    this.form.SetFocusControl(this.form.GENBA_CD);
                    ren = false;
                    return ren;
                }
            }

            bool catchErr = true;
            var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return false;
            }
            if (null == gyousha)
            {
                // 業者及び現場の関連情報をクリア
                this.form.GENBA_CD.Text = String.Empty;
                this.form.GENBA_NAME.Text = String.Empty;
                this.form.GYOUSHA_NAME.Text = String.Empty;

                this.form.isInputError = true;
                this.form.SetFocusControl(this.form.GENBA_CD);
                ren = false;
            }
            return ren;
        }

        /// <summary>
        /// 営業担当者を設定します
        /// （現場の営業担当者 → 業者の営業担当者 → 取引先の営業担当者の優先順）
        /// </summary>
        private void SetEigyouTantousha()
        {
            var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            var genbaCd = this.form.GENBA_CD.Text;
            this.form.EIGYOU_TANTOUSHA_CD.Text = String.Empty;
            this.form.EIGYOU_TANTOUSHA_NAME.Text = String.Empty;

            // 取引先があればセット
            bool catchErr = true;
            var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != torihikisaki && !String.IsNullOrEmpty(torihikisaki.EIGYOU_TANTOU_CD))
            {
                // 取得した取引先の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = torihikisaki.EIGYOU_TANTOU_CD;
            }

            // 業者があればセット
            var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != gyousha && !String.IsNullOrEmpty(gyousha.EIGYOU_TANTOU_CD))
            {
                // 取得した業者の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = gyousha.EIGYOU_TANTOU_CD;
            }

            // 現場があればセット
            var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != genba && !String.IsNullOrEmpty(genba.EIGYOU_TANTOU_CD))
            {
                // 取得した現場の営業担当CDが設定されている場合
                this.form.EIGYOU_TANTOUSHA_CD.Text = genba.EIGYOU_TANTOU_CD;
            }

            // 営業担当者CDが設定されたら名称を設定
            var eigyouTantoushaCd = this.form.EIGYOU_TANTOUSHA_CD.Text;
            if (!String.IsNullOrEmpty(eigyouTantoushaCd))
            {
                var shain = this.GetShain(eigyouTantoushaCd);
                if (null != shain)
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shain.SHAIN_NAME_RYAKU;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_CD.Text = String.Empty;
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = String.Empty;
                }
            }
        }

        #region 各マスター情報を取得
        /// <summary>
        /// 取引先取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <returns></returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            M_TORIHIKISAKI returnVal = null;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return null;
                }

                M_TORIHIKISAKI keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity);
                if (torihikisaki != null && torihikisaki.Length > 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.UKETSUKE_DATE.Text) && DateTime.TryParse(this.form.UKETSUKE_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (torihikisaki[0].TEKIYOU_BEGIN.IsNull && torihikisaki[0].TEKIYOU_END.IsNull)
                    {
                        returnVal = torihikisaki[0];
                    }
                    else if (torihikisaki[0].TEKIYOU_BEGIN.IsNull && !torihikisaki[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(torihikisaki[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = torihikisaki[0];
                    }
                    else if (!torihikisaki[0].TEKIYOU_BEGIN.IsNull && torihikisaki[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(torihikisaki[0].TEKIYOU_BEGIN) >= 0)
                    {
                        returnVal = torihikisaki[0];
                    }
                    else if (!torihikisaki[0].TEKIYOU_BEGIN.IsNull && !torihikisaki[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(torihikisaki[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(torihikisaki[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = torihikisaki[0];
                    }
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr)
        {
            M_GYOUSHA returnVal = null;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.UKETSUKE_DATE.Text) && DateTime.TryParse(this.form.UKETSUKE_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = gyousha[0];
                    }
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return returnVal;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
        }

        /// <summary>
        /// 現場取得(複数)
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            var returnVal = new List<M_GENBA>();
            try
            {
                LogUtility.DebugMethodStart(genbaCd);

                if (string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal.ToArray();
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.UKETSUKE_DATE.Text) && DateTime.TryParse(this.form.UKETSUKE_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    foreach (M_GENBA entity in genba)
                    {
                        if (entity.TEKIYOU_BEGIN.IsNull && entity.TEKIYOU_END.IsNull)
                        {
                            returnVal.Add(entity);
                        }
                        else if (entity.TEKIYOU_BEGIN.IsNull && !entity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(entity.TEKIYOU_END) <= 0)
                        {
                            returnVal.Add(entity);
                        }
                        else if (!entity.TEKIYOU_BEGIN.IsNull && entity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(entity.TEKIYOU_BEGIN) >= 0)
                        {
                            returnVal.Add(entity);
                        }
                        else if (!entity.TEKIYOU_BEGIN.IsNull && !entity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(entity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(entity.TEKIYOU_END) <= 0)
                        {
                            returnVal.Add(entity);
                        }
                    }
                }

                return returnVal.ToArray();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string genbaCd, string gyoushaCd, out bool catchErr)
        {
            M_GENBA returnVal = null;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    // PK指定のため1件
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.UKETSUKE_DATE.Text) && DateTime.TryParse(this.form.UKETSUKE_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    
                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        returnVal = genba[0];
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = genba[0];
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        returnVal = genba[0];
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = genba[0];
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 業者CD、現場CDで現場リストを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティリスト</returns>
        public M_GENBA[] GetGenbaList(string gyoushaCd, string genbaCd)
        {
            var returnVal = new List<M_GENBA>();
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal.ToArray();
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.UKETSUKE_DATE.Text) && DateTime.TryParse(this.form.UKETSUKE_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    
                    foreach (M_GENBA entity in genba)
                    {
                        if (entity.TEKIYOU_BEGIN.IsNull && entity.TEKIYOU_END.IsNull)
                        {
                            returnVal.Add(entity);
                        }
                        else if (entity.TEKIYOU_BEGIN.IsNull && !entity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(entity.TEKIYOU_END) <= 0)
                        {
                            returnVal.Add(entity);
                        }
                        else if (!entity.TEKIYOU_BEGIN.IsNull && entity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(entity.TEKIYOU_BEGIN) >= 0)
                        {
                            returnVal.Add(entity);
                        }
                        else if (!entity.TEKIYOU_BEGIN.IsNull && !entity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(entity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(entity.TEKIYOU_END) <= 0)
                        {
                            returnVal.Add(entity);
                        }
                    }
                    returnVal = genba.ToList();
                }

                return returnVal.ToArray();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 諸口区分関連項目のReadonly設定(true)
        /// <summary>
        /// 諸口区分関連項目のReadonly設定(true)
        /// </summary>
        internal void SetShokuchiControlsReadonlyProperty()
        {
            try
            {
                //LogUtility.DebugMethodStart();

                List<string> shokuchiCtrlList = new List<string> { "TORIHIKISAKI_NAME"
                                                                , "GYOUSHA_NAME"
                                                                , "GENBA_NAME"
                                                                , "UNPAN_GYOUSHA_NAME"
                                                                , "NIOROSHI_GYOUSHA_NAME"
                                                                , "NIOROSHI_GENBA_NAME" };

                // UIFormのコントロールを制御
                foreach (var controlName in shokuchiCtrlList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);
                    // コントロール取得できない場合
                    if (control == null)
                    {
                        // 次へ
                        continue;
                    }

                    // Readonlyプロパティを取得
                    var property = control.GetType().GetProperty("Enabled");
                    if (property != null)
                    {
                        this.SetCtrlReadonly((CustomTextBox)control, true);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region コントロールのReadonlyプロパティ設定
        /// <summary>
        /// コントロールのReadonlyプロパティ設定
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="isReadOnly"></param>
        internal void SetCtrlReadonly(CustomTextBox ctrl, bool isReadOnly)
        {
            try
            {
                //LogUtility.DebugMethodStart(ctrl, isReadOnly);

                ctrl.ReadOnly = isReadOnly;
                ctrl.TabStop = !isReadOnly;
                if (isReadOnly)
                {
                    ctrl.Tag = string.Empty;
                    return;
                }
                string hintLenth = (ctrl.MaxLength/2).ToString();
                string ZENKAKU = "全角" + hintLenth + "桁以内で入力してください";
                switch (ctrl.Name)
                {
                    case "TORIHIKISAKI_NAME":
                    case "GYOUSHA_NAME":
                    case "GENBA_NAME":
                    case "UNPAN_GYOUSHA_NAME":
                    case "NIOROSHI_GYOUSHA_NAME":
                    case "NIOROSHI_GENBA_NAME":
                        ctrl.Tag = ZENKAKU;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 諸口区分用プレビューキーダウンイベント
        /// <summary>
        /// 諸口区分用プレビューキーダウンイベント
        /// 諸口区分が存在する取引先、業者、現場で使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewKeyDownForShokuchikbnCheck(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                    case Keys.Tab:
                        this.pressedEnterOrTab = true;
                        break;

                    default:
                        this.pressedEnterOrTab = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 諸口区分用フォーカス移動処理
        /// <summary>
        /// 諸口区分用フォーカス移動処理
        /// </summary>
        /// <param name="control"></param>
        internal void MoveToNextControlForShokuchikbnCheck(ICustomControl control)
        {
            try
            {
                LogUtility.DebugMethodStart(control);

                if (this.pressedEnterOrTab)
                {
                    var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    this.form.SelectNextControl((Control)control, !isPressShift, true, true, true);
                }

                // マウス操作を考慮するためpressedEnterOrTabを初期化
                pressedEnterOrTab = false;
            }
            catch (Exception ex)
            {
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

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        internal void SetErrorFocus()
        {
            //必須チェックフォーカス処理
            bool errorFlg = false;
            if (this.form.RegistErrorFlag)
            {
                var allControl = this.form.GetAllControl();
                Control[] sortControl = allControl.OfType<Control>().OrderBy(s => s.TabIndex).ToArray();


                var k = this.form.allControl.Where(c => c.Name == "KYOTEN_CD");

                foreach (Control ctr in sortControl)
                {
                    // TextBoxの場合
                    var textBox = ctr as ICustomTextBox;
                    if (textBox != null)
                    {
                        if (textBox.IsInputErrorOccured)
                        {
                            ctr.Focus();
                            errorFlg = true;
                        }
                    }

                    // CustomDateTimePickerの場合
                    var CustomDateTimePicker = ctr as CustomDateTimePicker;

                    if (CustomDateTimePicker != null)
                    {
                        if (CustomDateTimePicker.IsInputErrorOccured)
                        {
                            ctr.Focus();
                            errorFlg = true;
                        }
                    }

                    if (errorFlg)
                    {
                        break;
                    }
                }
            }
        }

        #endregion 必須チェックエラーフォーカス処理

        #region 営業担当者チェック
        /// <summary>
        /// 営業担当者チェック
        /// </summary>
        internal bool CheckEigyouTantousha()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                var before = this.form.GetBeforeText(this.form.EIGYOU_TANTOUSHA_CD.Name);
                if (before == this.form.EIGYOU_TANTOUSHA_CD.Text && !this.form.isInputError)
                {
                    return ret;
                }

                // 初期化
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                this.form.isInputError = false;

                if (string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD.Text))
                {
                    // 営業担当者CDがなければ既にエラーが表示されているので何もしない。
                    return ret;
                }

                var shainEntity = this.GetShain(this.form.EIGYOU_TANTOUSHA_CD.Text);
                if (shainEntity == null || shainEntity.DELETE_FLG.IsTrue)
                {
                    this.form.isInputError = true;
                    return ret;
                }
                else if (shainEntity.EIGYOU_TANTOU_KBN.Equals(SqlBoolean.False))
                {
                    this.form.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = true;
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E057", "営業担当の登録が", "入力");
                    this.form.EIGYOU_TANTOUSHA_CD.Focus();
                    this.form.isInputError = true;
                    return ret;
                }
                else
                {
                    this.form.EIGYOU_TANTOUSHA_NAME.Text = shainEntity.SHAIN_NAME_RYAKU;
                }

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckEigyouTantousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 社員取得
        /// </summary>
        /// <param name="shainCd"></param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd)
        {
            M_SHAIN returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);

                if (string.IsNullOrEmpty(shainCd))
                {
                    return returnVal;
                }

                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                var shain = this.shainDao.GetAllValidData(keyEntity);

                if (shain != null && shain.Length > 0 && shain[0].DELETE_FLG.IsFalse)
                {
                    returnVal = shain[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region 使用しない
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region データ移動処理
        /// <summary>
        /// データ移動処理
        /// </summary>
        internal bool SetMoveData()
        {
            if (this.form.moveData_flg)
            {
                this.form.TORIHIKISAKI_CD.Text = this.form.moveData_torihikisakiCd;
                if (!this.form.SetTorihikisaki())
                {
                    return false;
                }
                this.form.GYOUSHA_CD.Text = this.form.moveData_gyousyaCd;
                if (!this.form.SetGyousha())
                {
                    return false;
                }
                this.form.GENBA_CD.Text = this.form.moveData_genbaCd;
                if (!this.form.SetGenba())
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 20150928 hoanghm #10907

        private const string ZENKAKU = "全角{0}桁以内で入力してください";

        private bool CheckTorihikisakiShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                bool catchErr = true;
                var torihikisakiEntity = this.GetTorihikisaki(this.form.TORIHIKISAKI_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisakiEntity)
                {
                    //this.form.TORIHIKISAKI_NAME.ReadOnly = !(bool)torihikisakiEntity.SHOKUCHI_KBN;
                    //this.form.TORIHIKISAKI_NAME.Tag = (bool)torihikisakiEntity.SHOKUCHI_KBN ? string.Format(ZENKAKU, this.form.TORIHIKISAKI_NAME.MaxLength.ToString()) : string.Empty;
                    this.SetCtrlReadonly(this.form.TORIHIKISAKI_NAME, !(bool)torihikisakiEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        private bool CheckGyoushaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                bool catchErr = true;
                var gyoushaEntity = this.GetGyousha(this.form.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != gyoushaEntity)
                {
                    //this.form.GYOUSHA_NAME.ReadOnly = !(bool)gyoushaEntity.SHOKUCHI_KBN;
                    //this.form.GYOUSHA_NAME.Tag = (bool)gyoushaEntity.SHOKUCHI_KBN ? string.Format(ZENKAKU, this.form.GYOUSHA_NAME.MaxLength.ToString()) : string.Empty;
                    this.SetCtrlReadonly(this.form.GYOUSHA_NAME, !(bool)gyoushaEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        private bool CheckGenbaShokuchi()
        {
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                bool catchErr = true;
                var genbaEntity = this.GetGenba(this.form.GENBA_CD.Text, this.form.GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != genbaEntity)
                {
                    //this.form.GENBA_NAME.ReadOnly = !(bool)genbaEntity.SHOKUCHI_KBN;
                    //this.form.GENBA_NAME.Tag = (bool)genbaEntity.SHOKUCHI_KBN ? string.Format(ZENKAKU, this.form.GENBA_NAME.MaxLength.ToString()) : string.Empty;
                    this.SetCtrlReadonly(this.form.GENBA_NAME, !(bool)genbaEntity.SHOKUCHI_KBN);
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 現場CDが空の時の処理
        /// </summary>
        internal void GenbaCdEnptyProcess()
        {
            // 現場の関連情報をクリア
            this.form.GENBA_NAME.Text = String.Empty;
            this.form.GENBA_CD.IsInputErrorOccured = false;
            this.SetCtrlReadonly(this.form.GENBA_NAME, true);
            this.form.isInputError = false;
            this.form.GENBA_CD.UpdateBackColor(false);

            if (this.pressedEnterOrTab)
            {
                if (this.form.Key.Shift)
                {
                    this.form.SetFocusControl(this.form.GYOUSHA_CD);
                }
                else
                {
                    this.form.SetFocusControl(this.form.TORIHIKISAKI_CD);
                }
                //フォーカス設定処理
                this.form.boolMoveFocusControl();
            }

            // 営業担当者を設定
            this.SetEigyouTantousha();
        }
    }
}
