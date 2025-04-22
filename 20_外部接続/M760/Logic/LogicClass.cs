using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
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
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Logic
{
    /// <summary>
    /// 電子文書詳細入力のビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.DenshiBunshoHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 電子文書詳細入力Form
        /// </summary>
        internal UIForm form;
        internal UIHeader headerForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// WANSIGN文書詳細情報エンティティ
        /// </summary>
        internal M_WANSIGN_KEIYAKU_INFO wanSignKeiyakuInfoEntity;

        /// <summary>
        /// WANSIGN文書詳細情報エンティティ
        /// </summary>
        internal M_WANSIGN_KEIYAKU_INFO[] listWansignKeiyakuInfoEntity = null;

        /// <summary>
        /// WANSIGN文書詳細情報Dao
        /// </summary>
        internal IM_WANSIGN_KEIYAKU_INFODAO wanSignKeiyakuInfoDao;

        /// <summary>
        /// 委託契約WANSIGN連携Dao
        /// </summary>
        private IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO linkWanSignKeiyakuDao;

        /// <summary>
        /// 委託契約基本エンティティ
        /// </summary>
        private M_ITAKU_KEIYAKU_KIHON itakuKeiyakuEntity;

        /// <summary>
        /// 委託契約基本Dao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao itakuKeiyakuDao;

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        internal M_SYS_INFO systemInfoEntity;

        /// <summary>
        /// システム設定Dao
        /// </summary>
        private IM_SYS_INFODao systemInfoDao;
        #endregion

        #region プロパティ
        /// <summary>
        /// 委託契約システムID
        /// </summary>
        internal string KeiyakuSystemID { get; set; }

        /// <summary>
        /// WANSIGNシステムID
        /// </summary>
        internal string WansignSystemID { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        internal bool isRegist { get; set; }

        /// <summary>
        /// エラーフラグ
        /// </summary>
        internal bool isError = false;

        /// <summary>
        /// 検索結果(現場一覧)
        /// </summary>
        private DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 電子契約APIロジッククラス
        /// </summary>
        private DenshiWanSignLogic denshiWanSignLogic;

        internal string preValue { get; set; }
        internal DataTable RenewalPeriodUnitData = null;
        internal DataTable CancelPeriodUnitData = null;
        internal DataTable ReminderPeriodUnitData = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.wanSignKeiyakuInfoEntity = new M_WANSIGN_KEIYAKU_INFO();
            this.systemInfoEntity = new M_SYS_INFO();
            this.itakuKeiyakuEntity = new M_ITAKU_KEIYAKU_KIHON();
            this.wanSignKeiyakuInfoDao = DaoInitUtility.GetComponent<IM_WANSIGN_KEIYAKU_INFODAO>();
            this.linkWanSignKeiyakuDao = DaoInitUtility.GetComponent<IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO>();
            this.systemInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.itakuKeiyakuDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.denshiWanSignLogic = new DenshiWanSignLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)parentForm.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 処理モード別画面初期化
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    this.Search();

                    // 検索結果を画面に設定
                    this.WindowInitCtrl();
                    this.SetDataForWindow();

                    // functionボタン
                    parentForm.bt_func3.Enabled = true;     // 電子契約修正
                    parentForm.bt_func5.Enabled = true;     // 契約参照
                    parentForm.bt_func9.Enabled = true;     // 登録
                    parentForm.bt_func12.Enabled = true;    // 閉じる
                    parentForm.bt_process2.Enabled = true;  // 契約書ﾀﾞｳﾝﾛｰﾄﾞ
                }
                this.allControl = this.form.allControl;

                this.RenewalPeriodUnitPopUpDataInit();
                this.CancelPeriodUnitPopUpDataInit();
                this.ReminderPeriodUnitPopUpDataInit();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.wanSignKeiyakuInfoEntity = new M_WANSIGN_KEIYAKU_INFO();
            this.itakuKeiyakuEntity = new M_ITAKU_KEIYAKU_KIHON();
            if (!string.IsNullOrEmpty(this.WansignSystemID))
            {
                this.wanSignKeiyakuInfoEntity = wanSignKeiyakuInfoDao.GetDataBySystemId(this.WansignSystemID);
            }
            if (!string.IsNullOrEmpty(this.KeiyakuSystemID))
            {
                var linkWanSign = this.linkWanSignKeiyakuDao.GetDataBySystemId(long.Parse(this.WansignSystemID), long.Parse(this.KeiyakuSystemID));
                if (linkWanSign != null)
                {
                    M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                    cond.SYSTEM_ID = this.KeiyakuSystemID.PadLeft(9, '0');
                    this.itakuKeiyakuEntity = itakuKeiyakuDao.GetDataBySystemId(cond);
                }
            }

            return 1;
        }

        /// <summary>
        /// 画面項目初期化
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitCtrl()
        {
            try
            {
                // 最新のSYS_INFOを取得
                M_SYS_INFO[] sysInfo = this.systemInfoDao.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.systemInfoEntity = sysInfo[0];
                }
                else
                {
                    this.systemInfoEntity = null;
                }

                this.headerForm.CONTROL_NUMBER.Text = string.Empty;
                this.headerForm.REGISTERED_USER_NAME.Text = string.Empty;
                this.headerForm.DOCUMENT_ID.Text = string.Empty;
                this.form.ORIGINAL_CONTROL_NUMBER.Text = string.Empty;
                this.form.DOCUMENT_NAME.Text = string.Empty;
                this.form.KEIYAKU_NUMBER.Text = string.Empty;
                this.form.HIMOZUKE_SYSTEM_ID.Text = string.Empty;
                this.form.SYSTEM_ID.Text = string.Empty;
                this.form.POST_NM.Text = string.Empty;
                this.form.NAME_NM.Text = string.Empty;

                #region 委託契約情報タブ
                this.form.HAISHUTSU_JIGYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.BIKOU1.Text = string.Empty;
                this.form.BIKOU2.Text = string.Empty;
                this.form.ITAKU_KEIYAKU_ICHIRAN.DataSource = null;
                this.form.ITAKU_KEIYAKU_ICHIRAN.Rows.Clear();
                #endregion

                #region 電子情報タブ
                this.form.IS_VALID.Text = "0";
                this.form.CONTRACT_DATE.Text = string.Empty;
                this.form.CONTRACT_EXPIRATION_DATE.Text = string.Empty;

                this.form.IS_AUTO_UPDATING.Text = "0";
                this.ChangeAutoUpdating();
                this.form.RENEWWAL_PERIOD.Text = string.Empty;
                this.form.RENEWWAL_PERIOD_UNIT.Text = DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_1;
                this.form.RENEWWAL_PERIOD_UNIT_NAME.Text = DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_1;
                this.form.CANCEL_PERIOD.Text = string.Empty;
                this.form.CANCEL_PERIOD_UNIT.Text = DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_1;
                this.form.CANCEL_PERIOD_UNIT_NAME.Text = DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_1;

                this.form.IS_REMINDER.Text = "0";
                this.ChangeReminder();
                this.form.REMINDER_PERIOD.Text = string.Empty;
                this.form.REMINDER_PERIOD_UNIT.Text = DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_1;
                this.form.REMINDER_PERIOD_UNIT_NAME.Text = DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_1;

                this.form.CONTRACT_DECIMAL.Text = string.Empty;
                this.form.STORAGE_LOCATION.Text = string.Empty;
                #endregion

                #region 電子文書情報タブ
                if (this.systemInfoEntity != null)
                {
                    if (!string.IsNullOrEmpty(this.systemInfoEntity.WAN_SIGN_BIKOU_1))
                    {
                        this.form.lbBIKOU_1.Text = this.systemInfoEntity.WAN_SIGN_BIKOU_1;
                    }
                    else
                    {
                        this.form.lbBIKOU_1.Text = "備考1";
                    }

                    if (!string.IsNullOrEmpty(this.systemInfoEntity.WAN_SIGN_BIKOU_2))
                    {
                        this.form.lbBIKOU_2.Text = this.systemInfoEntity.WAN_SIGN_BIKOU_2;
                    }
                    else
                    {
                        this.form.lbBIKOU_2.Text = "備考2";
                    }

                    if (!string.IsNullOrEmpty(this.systemInfoEntity.WAN_SIGN_BIKOU_3))
                    {
                        this.form.lbBIKOU_3.Text = this.systemInfoEntity.WAN_SIGN_BIKOU_3;
                    }
                    else
                    {
                        this.form.lbBIKOU_3.Text = "備考3";
                    }

                    this.form.lbFIELD_1.Text = this.systemInfoEntity.WAN_SIGN_FIELD_NAME_1;
                    this.form.lbFIELD_2.Text = this.systemInfoEntity.WAN_SIGN_FIELD_NAME_2;
                    this.form.lbFIELD_3.Text = this.systemInfoEntity.WAN_SIGN_FIELD_NAME_3;
                    this.form.lbFIELD_4.Text = this.systemInfoEntity.WAN_SIGN_FIELD_NAME_4;
                    this.form.lbFIELD_5.Text = this.systemInfoEntity.WAN_SIGN_FIELD_NAME_5;

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value)
                        {
                            case 1:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_1;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_1;
                                break;
                            case 2:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_2;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_2;
                                break;
                            case 3:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_3;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_1.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_3;
                                break;
                        }
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value)
                        {
                            case 1:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_1;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_1;
                                break;
                            case 2:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_2;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_2;
                                break;
                            case 3:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_3;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_2.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_3;
                                break;
                        }
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value)
                        {
                            case 1:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_1;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_1;
                                break;
                            case 2:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_2;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_2;
                                break;
                            case 3:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_3;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_3.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_3;
                                break;
                        }
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value)
                        {
                            case 1:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_1;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_1;
                                break;
                            case 2:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_2;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_2;
                                break;
                            case 3:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_3;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_4.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_3;
                                break;
                        }
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value)
                        {
                            case 1:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_1;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_1;
                                break;
                            case 2:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_2;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_2;
                                break;
                            case 3:
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_CD_3;
                                this.form.WAN_SIGN_FIELD_ATTRIBUTE_NAME_5.Text = DenshiBunshoHoshuConstans.INPUT_TYPE_NAME_3;
                                break;
                        }
                    }
                }

                this.FieldControlSetting(1);
                #endregion

				//PhuocLoc 2022/03/14 #161423 -Start
                #region 相手方タブ
                this.form.PARTNER_ORGANIZE_NM_ICHIRAN.DataSource = null;
                this.form.PARTNER_ORGANIZE_NM_ICHIRAN.Rows.Clear();
                #endregion
                //PhuocLoc 2022/03/14 #161423 -End
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitCtrl", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitCtrl", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// データをDBから取得
        /// </summary>
        public void SetDataForWindow()
        {
            if (this.wanSignKeiyakuInfoEntity != null)
            {
                // ヘッダー項目
                this.headerForm.CONTROL_NUMBER.Text = this.wanSignKeiyakuInfoEntity.CONTROL_NUMBER;
                this.headerForm.REGISTERED_USER_NAME.Text = this.wanSignKeiyakuInfoEntity.REGISTERED_USER_NAME;
                this.headerForm.DOCUMENT_ID.Text = this.wanSignKeiyakuInfoEntity.DOCUMENT_ID;
                this.form.ORIGINAL_CONTROL_NUMBER.Text = this.wanSignKeiyakuInfoEntity.ORIGINAL_CONTROL_NUMBER;
                this.form.DOCUMENT_NAME.Text = this.wanSignKeiyakuInfoEntity.DOCUMENT_NAME;
                this.form.POST_NM.Text = this.wanSignKeiyakuInfoEntity.POST_NM;
                this.form.NAME_NM.Text = this.wanSignKeiyakuInfoEntity.NAME_NM;

                #region 電子情報タブ
                if (!this.wanSignKeiyakuInfoEntity.IS_VALID.IsNull)
                {
                    this.form.IS_VALID.Text = this.wanSignKeiyakuInfoEntity.IS_VALID.Value.ToString();
                }
                if (!this.wanSignKeiyakuInfoEntity.CONTRACT_DATE.IsNull)
                {
                    this.form.CONTRACT_DATE.Text = this.wanSignKeiyakuInfoEntity.CONTRACT_DATE.Value.ToString();
                }
                if (!this.wanSignKeiyakuInfoEntity.CONTRACT_EXPIRATION_DATE.IsNull)
                {
                    this.form.CONTRACT_EXPIRATION_DATE.Text = this.wanSignKeiyakuInfoEntity.CONTRACT_EXPIRATION_DATE.Value.ToString();
                }

                if (!this.wanSignKeiyakuInfoEntity.IS_AUTO_UPDATING.IsNull)
                {
                    this.form.IS_AUTO_UPDATING.Text = this.wanSignKeiyakuInfoEntity.IS_AUTO_UPDATING.Value.ToString();
                }
                this.form.RENEWWAL_PERIOD.Text = this.wanSignKeiyakuInfoEntity.RENEWWAL_PERIOD;
                this.form.RENEWWAL_PERIOD_UNIT.Text = this.wanSignKeiyakuInfoEntity.RENEWWAL_PERIOD_UNIT;
                this.SetRenewalPeriodUnitName();

                this.form.CANCEL_PERIOD.Text = this.wanSignKeiyakuInfoEntity.CANCEL_PERIOD;
                this.form.CANCEL_PERIOD_UNIT.Text = this.wanSignKeiyakuInfoEntity.CANCEL_PERIOD_UNIT;
                this.SetCancelPeriodUnitName();

                if (!this.wanSignKeiyakuInfoEntity.IS_REMINDER.IsNull)
                {
                    this.form.IS_REMINDER.Text = this.wanSignKeiyakuInfoEntity.IS_REMINDER.Value.ToString();
                }
                this.form.REMINDER_PERIOD.Text = this.wanSignKeiyakuInfoEntity.REMINDER_PERIOD;
                this.form.REMINDER_PERIOD_UNIT.Text = this.wanSignKeiyakuInfoEntity.REMINDER_PERIOD_UNIT;
                this.SetReminderPeriodUnitName();

                if (!this.wanSignKeiyakuInfoEntity.CONTRACT_DECIMAL.IsNull)
                {
                    this.form.CONTRACT_DECIMAL.Text = this.wanSignKeiyakuInfoEntity.CONTRACT_DECIMAL.Value.ToString();
                }
                this.form.STORAGE_LOCATION.Text = this.wanSignKeiyakuInfoEntity.STORAGE_LOCATION;
                #endregion

                #region 電子文書情報タブ
                this.form.COMMENT_1.Text = this.wanSignKeiyakuInfoEntity.COMMENT_1;
                this.form.COMMENT_2.Text = this.wanSignKeiyakuInfoEntity.COMMENT_2;
                this.form.COMMENT_3.Text = this.wanSignKeiyakuInfoEntity.COMMENT_3;
                this.form.FIELD_STR_1.Text = this.wanSignKeiyakuInfoEntity.FIELD_1;
                this.form.FIELD_STR_2.Text = this.wanSignKeiyakuInfoEntity.FIELD_2;
                this.form.FIELD_STR_3.Text = this.wanSignKeiyakuInfoEntity.FIELD_3;
                this.form.FIELD_STR_4.Text = this.wanSignKeiyakuInfoEntity.FIELD_4;
                this.form.FIELD_STR_5.Text = this.wanSignKeiyakuInfoEntity.FIELD_5;

                if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull
                    && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 2)
                {
                    this.ChangeNumberFormat(this.form.FIELD_STR_1);
                }
                if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull
                    && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 2)
                {
                    this.ChangeNumberFormat(this.form.FIELD_STR_2);
                }
                if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull
                    && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 2)
                {
                    this.ChangeNumberFormat(this.form.FIELD_STR_3);
                }
                if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull
                    && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 2)
                {
                    this.ChangeNumberFormat(this.form.FIELD_STR_4);
                }
                if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull
                    && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 2)
                {
                    this.ChangeNumberFormat(this.form.FIELD_STR_5);
                }
                #endregion

				//PhuocLoc 2022/03/14 #161423 -Start
                #region 相手方
                this.SetPartnerOrganize();
                #endregion
                //PhuocLoc 2022/03/14 #161423 -End
            }

            #region 委託契約情報
            if (this.itakuKeiyakuEntity != null && !string.IsNullOrEmpty(this.itakuKeiyakuEntity.SYSTEM_ID))
            {
                this.form.KEIYAKU_NUMBER.Text = this.itakuKeiyakuEntity.ITAKU_KEIYAKU_NO;
                this.form.HIMOZUKE_SYSTEM_ID.Text = this.itakuKeiyakuEntity.SYSTEM_ID.PadLeft(9, '0');
                this.form.SYSTEM_ID.Text = this.itakuKeiyakuEntity.SYSTEM_ID.PadLeft(9, '0'); ;
                this.SearchKeiyakuInfo();
            }
            #endregion
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();
                foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.listWansignKeiyakuInfoEntity)
                {
                    wansignInfoEntity.ORIGINAL_CONTROL_NUMBER = this.form.ORIGINAL_CONTROL_NUMBER.Text;
                    wansignInfoEntity.DOCUMENT_NAME = this.form.DOCUMENT_NAME.Text;
                    wansignInfoEntity.POST_NM = this.form.POST_NM.Text;
                    wansignInfoEntity.NAME_NM = this.form.NAME_NM.Text;

                    if (!string.IsNullOrEmpty(this.form.IS_VALID.Text))
                    {
                        wansignInfoEntity.IS_VALID = SqlInt16.Parse(this.form.IS_VALID.Text);
                    }
                    wansignInfoEntity.CONTRACT_DATE = SqlDateTime.Null;
                    if (!string.IsNullOrEmpty(this.form.CONTRACT_DATE.Text))
                    {
                        wansignInfoEntity.CONTRACT_DATE = DateTime.Parse(this.form.CONTRACT_DATE.Text);
                    }
                    wansignInfoEntity.CONTRACT_EXPIRATION_DATE = SqlDateTime.Null;
                    if (!string.IsNullOrEmpty(this.form.CONTRACT_EXPIRATION_DATE.Text))
                    {
                        wansignInfoEntity.CONTRACT_EXPIRATION_DATE = DateTime.Parse(this.form.CONTRACT_EXPIRATION_DATE.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.IS_AUTO_UPDATING.Text))
                    {
                        wansignInfoEntity.IS_AUTO_UPDATING = SqlInt16.Parse(this.form.IS_AUTO_UPDATING.Text);
                    }
                    wansignInfoEntity.RENEWWAL_PERIOD = this.form.RENEWWAL_PERIOD.Text;
                    wansignInfoEntity.RENEWWAL_PERIOD_UNIT = this.form.RENEWWAL_PERIOD_UNIT.Text;
                    wansignInfoEntity.CANCEL_PERIOD = this.form.CANCEL_PERIOD.Text;
                    wansignInfoEntity.CANCEL_PERIOD_UNIT = this.form.CANCEL_PERIOD_UNIT.Text;

                    if (!string.IsNullOrEmpty(this.form.IS_REMINDER.Text))
                    {
                        wansignInfoEntity.IS_REMINDER = SqlInt16.Parse(this.form.IS_REMINDER.Text);
                    }
                    wansignInfoEntity.REMINDER_PERIOD = this.form.REMINDER_PERIOD.Text;
                    wansignInfoEntity.REMINDER_PERIOD_UNIT = this.form.REMINDER_PERIOD_UNIT.Text;
                    decimal contractDecimal = 0;
                    wansignInfoEntity.CONTRACT_DECIMAL = SqlDecimal.Null;
                    if (decimal.TryParse(this.form.CONTRACT_DECIMAL.Text, out contractDecimal))
                    {
                        wansignInfoEntity.CONTRACT_DECIMAL = contractDecimal;
                    }
                    wansignInfoEntity.STORAGE_LOCATION = this.form.STORAGE_LOCATION.Text;

                    wansignInfoEntity.COMMENT_1 = this.form.COMMENT_1.Text;
                    wansignInfoEntity.COMMENT_2 = this.form.COMMENT_2.Text;
                    wansignInfoEntity.COMMENT_3 = this.form.COMMENT_3.Text;

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull
                        && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value == 2)
                    {
                        wansignInfoEntity.FIELD_1 = GetResultText(this.form.FIELD_STR_1);
                    }
                    else
                    {
                        wansignInfoEntity.FIELD_1 = this.form.FIELD_STR_1.Text;
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull
                        && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value == 2)
                    {
                        wansignInfoEntity.FIELD_2 = GetResultText(this.form.FIELD_STR_2);
                    }
                    else
                    {
                        wansignInfoEntity.FIELD_2 = this.form.FIELD_STR_2.Text;
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull
                        && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value == 2)
                    {
                        wansignInfoEntity.FIELD_3 = GetResultText(this.form.FIELD_STR_3);
                    }
                    else
                    {
                        wansignInfoEntity.FIELD_3 = this.form.FIELD_STR_3.Text;
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull
                        && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value == 2)
                    {
                        wansignInfoEntity.FIELD_4 = GetResultText(this.form.FIELD_STR_4);
                    }
                    else
                    {
                        wansignInfoEntity.FIELD_4 = this.form.FIELD_STR_4.Text;
                    }

                    if (!this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull
                        && this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value == 2)
                    {
                        wansignInfoEntity.FIELD_5 = GetResultText(this.form.FIELD_STR_5);
                    }
                    else
                    {
                        wansignInfoEntity.FIELD_5 = this.form.FIELD_STR_5.Text;
                    }

                    // 更新者情報設定
                    var dataBinderLogic = new DataBinderLogic<M_WANSIGN_KEIYAKU_INFO>(wansignInfoEntity);
                    dataBinderLogic.SetSystemProperty(wansignInfoEntity, false);
                }
                
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateEntity", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (errorFlag)
                {
                    this.isRegist = false;
                    return;
                }

                foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.listWansignKeiyakuInfoEntity)
                {
                    wansignInfoEntity.ORIGINAL_CONTROL_NUMBER = this.form.ORIGINAL_CONTROL_NUMBER.Text;
                    wansignInfoEntity.DELETE_FLG = false;
                    this.wanSignKeiyakuInfoDao.Update(wansignInfoEntity);
                }

                //委託契約書、電子契約書 自動紐付処理
                //委託契約書
                if (this.KeiyakuSystemID != this.form.HIMOZUKE_SYSTEM_ID.Text)
                {
                    if (!string.IsNullOrEmpty(this.form.HIMOZUKE_SYSTEM_ID.Text))
                    {
                        M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                        cond.SYSTEM_ID = this.form.HIMOZUKE_SYSTEM_ID.Text.PadLeft(9, '0');

                        var itakuKeiyakuKihon = this.itakuKeiyakuDao.GetDataBySystemId(cond);
                        //　未紐付状態のWANSIGN文書詳細情報（新規テーブル）と委託契約基本情報（M_ITAKU_KEIYAKU_KIHON）紐付　
                        if (itakuKeiyakuKihon != null)
                        {
                            //委託契約WANSIGN連携
                            foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.listWansignKeiyakuInfoEntity)
                            {
                                M_ITAKU_LINK_WANSIGN_KEIYAKU[] linkWanSignList = this.linkWanSignKeiyakuDao.GetDataByWanSignSystemId(long.Parse(wansignInfoEntity.WANSIGN_SYSTEM_ID.ToString()));
                                if (linkWanSignList != null && linkWanSignList.Length > 0)
                                {
                                    this.linkWanSignKeiyakuDao.DeleteByWanSignSystemId(long.Parse(wansignInfoEntity.WANSIGN_SYSTEM_ID.ToString()));
                                }

                                var linkWanSign = new M_ITAKU_LINK_WANSIGN_KEIYAKU();
                                linkWanSign.WANSIGN_SYSTEM_ID = long.Parse(wansignInfoEntity.WANSIGN_SYSTEM_ID.ToString());
                                linkWanSign.SYSTEM_ID = long.Parse(this.form.HIMOZUKE_SYSTEM_ID.Text);
                                linkWanSign.DOCUMENT_ID = wansignInfoEntity.DOCUMENT_ID;
                                this.linkWanSignKeiyakuDao.Insert(linkWanSign);
                            }
                            this.KeiyakuSystemID = this.form.HIMOZUKE_SYSTEM_ID.Text;
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.KeiyakuSystemID) && string.IsNullOrEmpty(this.form.HIMOZUKE_SYSTEM_ID.Text))
                    {
                        //委託契約WANSIGN連携
                        foreach (M_WANSIGN_KEIYAKU_INFO wansignInfoEntity in this.listWansignKeiyakuInfoEntity)
                        {
                            M_ITAKU_LINK_WANSIGN_KEIYAKU[] linkWanSignList = this.linkWanSignKeiyakuDao.GetDataByWanSignSystemId(long.Parse(wansignInfoEntity.WANSIGN_SYSTEM_ID.ToString()));
                            if (linkWanSignList != null && linkWanSignList.Length > 0)
                            {
                                this.linkWanSignKeiyakuDao.DeleteByWanSignSystemId(long.Parse(wansignInfoEntity.WANSIGN_SYSTEM_ID.ToString()));
                                this.KeiyakuSystemID = this.form.HIMOZUKE_SYSTEM_ID.Text;
                            }
                        }
                    }
                }

                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Equals/GetHashCode/ToString

        //<summary>
        //クラスが等しいかどうか判定
        //</summary>
        //<param name="other"></param>
        //<returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        //<summary>
        //ハッシュコード取得
        //</summary>
        //<returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // <summary>
        //該当するオブジェクトを文字列形式で取得
        //</summary>
        //<returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;


            //電子契約修正(F3)イベント生成
            this.form.C_Regist(parentForm.bt_func3);
            parentForm.bt_func3.Click += new EventHandler(this.form.Update);
            parentForm.bt_func3.ProcessKbn = PROCESS_KBN.NEW;

            //契約参照(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.SetKeiyakuFrom);

            //登録(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //[2]契約書ﾀﾞｳﾝﾛｰﾄﾞ
            parentForm.bt_process2.Click += new EventHandler(this.form.KeiyakushoDownload);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        //<summary>
        //ボタン設定の読込
        //</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// 登録データをチェックする。
        /// </summary>
        internal bool CheckRegistData()
        {
            try
            {
                //必須チェック
                if(string.IsNullOrEmpty(this.form.DOCUMENT_NAME.Text))
                {
                    this.form.DOCUMENT_NAME.Focus();
                    this.form.errmessage.MessageBoxShow("E001", "文書名");
                    return true;
                }
                //PhuocLoc 2022/03/14 #161401 -Start
                if (!string.IsNullOrEmpty(this.form.IS_AUTO_UPDATING.Text) && 
                    this.form.IS_AUTO_UPDATING.Text == "1" && 
                    string.IsNullOrEmpty(this.form.RENEWWAL_PERIOD.Text))
                {
                    this.form.RENEWWAL_PERIOD.Focus();
                    this.form.errmessage.MessageBoxShow("E341");
                    return true;
                }
                //PhuocLoc 2022/03/14 #161401 -End
                if(!string.IsNullOrEmpty(this.form.RENEWWAL_PERIOD.Text) && string.IsNullOrEmpty(this.form.RENEWWAL_PERIOD_UNIT.Text))
                {
                    this.form.RENEWWAL_PERIOD_UNIT.Focus();
                    this.form.errmessage.MessageBoxShow("E332");
                    return true;
                }
                if(!string.IsNullOrEmpty(this.form.CANCEL_PERIOD.Text) && string.IsNullOrEmpty(this.form.CANCEL_PERIOD_UNIT.Text))
                {
                    this.form.CANCEL_PERIOD_UNIT.Focus();
                    this.form.errmessage.MessageBoxShow("E333");
                    return true;
                }
                //PhuocLoc 2022/03/14 #161402 -Start
                if (!string.IsNullOrEmpty(this.form.IS_REMINDER.Text) &&
                    this.form.IS_REMINDER.Text == "1" &&
                    string.IsNullOrEmpty(this.form.REMINDER_PERIOD.Text))
                {
                    this.form.REMINDER_PERIOD.Focus();
                    this.form.errmessage.MessageBoxShow("E342");
                    return true;
                }
                if (!string.IsNullOrEmpty(this.form.REMINDER_PERIOD.Text) && string.IsNullOrEmpty(this.form.REMINDER_PERIOD_UNIT.Text))
                {
                    this.form.REMINDER_PERIOD_UNIT.Focus();
                    this.form.errmessage.MessageBoxShow("E334");
                    return true;
                }
                //PhuocLoc 2022/03/14 #161402 -End

                //文字種チェック　(電子文書情報タブーフィールド1　～　フィールド5)
                if (this.systemInfoEntity != null)
                {
                    //電子文書情報タブーフィールド1
                    if (!this.form.FIELD_STR_1.ReadOnly &&  
                        !string.IsNullOrEmpty(this.form.FIELD_STR_1.Text) && 
                        !this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_1.Value)
                        {
                            case 2:
                                if (!this.CheckValidNumber(this.form.FIELD_STR_1.Text, this.form.lbFIELD_1.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E335", this.form.lbFIELD_1.Text);
                                    return true;
                                }
                                break;
                            case 3:
                                if (!this.CheckValidDate(this.form.FIELD_STR_1.Text, this.form.lbFIELD_1.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E336", this.form.lbFIELD_1.Text);
                                    return true;
                                }
                                break;
                        }
                    }
                    //電子文書情報タブーフィールド2
                    if (!this.form.FIELD_STR_2.ReadOnly &&
                        !string.IsNullOrEmpty(this.form.FIELD_STR_2.Text) &&
                        !this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_2.Value)
                        {
                            case 2:
                                if (!this.CheckValidNumber(this.form.FIELD_STR_2.Text, this.form.lbFIELD_2.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E335", this.form.lbFIELD_2.Text);
                                    return true;
                                }
                                break;
                            case 3:
                                if (!this.CheckValidDate(this.form.FIELD_STR_2.Text, this.form.lbFIELD_2.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E336", this.form.lbFIELD_2.Text);
                                    return true;
                                }
                                break;
                        }
                    }
                    //電子文書情報タブーフィールド3
                    if (!this.form.FIELD_STR_3.ReadOnly &&
                        !string.IsNullOrEmpty(this.form.FIELD_STR_3.Text) &&
                        !this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_3.Value)
                        {
                            case 2:
                                if (!this.CheckValidNumber(this.form.FIELD_STR_3.Text, this.form.lbFIELD_3.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E335", this.form.lbFIELD_3.Text);
                                    return true;
                                }
                                break;
                            case 3:
                                if (!this.CheckValidDate(this.form.FIELD_STR_3.Text, this.form.lbFIELD_3.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E336", this.form.lbFIELD_3.Text);
                                    return true;
                                }
                                break;
                        }
                    }
                    //電子文書情報タブーフィールド4
                    if (!this.form.FIELD_STR_4.ReadOnly &&
                        !string.IsNullOrEmpty(this.form.FIELD_STR_4.Text) &&
                        !this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_4.Value)
                        {
                            case 2:
                                if (!this.CheckValidNumber(this.form.FIELD_STR_4.Text, this.form.lbFIELD_4.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E335", this.form.lbFIELD_4.Text);
                                    return true;
                                }
                                break;
                            case 3:
                                if (!this.CheckValidDate(this.form.FIELD_STR_4.Text, this.form.lbFIELD_4.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E336", this.form.lbFIELD_4.Text);
                                    return true;
                                }
                                break;
                        }
                    }
                    //電子文書情報タブーフィールド5
                    if (!this.form.FIELD_STR_5.ReadOnly &&
                        !string.IsNullOrEmpty(this.form.FIELD_STR_5.Text) &&
                        !this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.IsNull)
                    {
                        switch (this.systemInfoEntity.WAN_SIGN_FIELD_ATTRIBUTE_5.Value)
                        {
                            case 2:
                                if (!this.CheckValidNumber(this.form.FIELD_STR_5.Text, this.form.lbFIELD_5.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E335", this.form.lbFIELD_5.Text);
                                    return true;
                                }
                                break;
                            case 3:
                                if (!this.CheckValidDate(this.form.FIELD_STR_5.Text, this.form.lbFIELD_5.Text))
                                {
                                    this.form.errmessage.MessageBoxShow("E336", this.form.lbFIELD_5.Text);
                                    return true;
                                }
                                break;
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegistData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 数字フォーマットと範囲のチェック
        /// </summary>
        /// <param name="FieldStr"></param>
        /// <param name="CtrlName"></param>
        /// <returns></returns>
        internal bool CheckValidNumber(string FieldStr, string CtrlName)
        {
            bool result = true;
            long value ;
            bool isNumeric = long.TryParse(FieldStr.Replace(",", ""), out value);
            if (!isNumeric)
            {
                result = false;
            }
            else if (value < DenshiBunshoHoshuConstans.MIN_NUMBERIC
                || value > DenshiBunshoHoshuConstans.MAX_NUMBERIC)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 日付形式チェック
        /// </summary>
        /// <param name="FieldStr"></param>
        /// <param name="CtrlName"></param>
        /// <returns></returns>
        internal bool CheckValidDate(string FieldStr, string CtrlName)
        {
            bool result = true;
            string[] formats = { "yyyy/MM/dd" };
            DateTime dateValue;

            if (!DateTime.TryParseExact(FieldStr, formats,
                                           new CultureInfo("en-US"),
                                           DateTimeStyles.None,
                                           out dateValue))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 契約処理
        /// </summary>
        internal virtual void SetKeiyakuFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 委託契約情報検索(G475)をモーダル表示
                var PopUpHeadForm = new Shougun.Core.Common.ItakuKeiyakuSearch.HeaderForm();
                var PopUpForm = new Shougun.Core.Common.ItakuKeiyakuSearch.ItakuKeiyakuSearchForm(PopUpHeadForm);
                BasePopForm PopForm = new BasePopForm(PopUpForm, PopUpHeadForm);
                PopForm.ShowDialog();

                // 実行結果
                switch (PopUpForm.DialogResult)
                {
                    case DialogResult.OK:

                        //現在のスクロール位置を保持
                        Point scrollPos = this.form.AutoScrollPosition;
                        this.form.AutoScroll = false;

                        //反映したコントロール全てロストフォーカスの処理を実行する
                        // 契約番号
                        this.form.KEIYAKU_NUMBER.Text = !string.IsNullOrEmpty(PopUpForm.retKeiyakuNumber) ? PopUpForm.retKeiyakuNumber : string.Empty;
                        // 契約番号SystemID
                        this.form.HIMOZUKE_SYSTEM_ID.Text = !string.IsNullOrEmpty(PopUpForm.retSystemId) ? PopUpForm.retSystemId.PadLeft(9,'0') : string.Empty;
                        // 契約番号SystemID
                        this.form.SYSTEM_ID.Text = !string.IsNullOrEmpty(PopUpForm.retSystemId) ? PopUpForm.retSystemId.PadLeft(9, '0') : string.Empty;
                        // 委託契約情報タブ
                        this.SearchKeiyakuInfo();

                        this.form.AutoScroll = true;
                        //保持したスクロール位置に戻す
                        this.form.AutoScrollPosition = new Point(-scrollPos.X, -scrollPos.Y);
                        break;

                    case DialogResult.Cancel:
                        // 何もしない
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeiyakuFrom", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 委託契約情報の取得
        /// </summary>
        internal void SearchKeiyakuInfo()
        {
            this.ClearKeiyakuInfo();
            if (!string.IsNullOrEmpty(this.form.HIMOZUKE_SYSTEM_ID.Text))
            {
                M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                cond.SYSTEM_ID = this.form.HIMOZUKE_SYSTEM_ID.Text.PadLeft(9, '0');

                M_ITAKU_KEIYAKU_KIHON result = itakuKeiyakuDao.GetDataBySystemId(cond);
                if (result != null)
                {
                    this.form.HAISHUTSU_JIGYOUSHA_CD.Text = result.HAISHUTSU_JIGYOUSHA_CD;
                    this.form.GYOUSHA_NAME_RYAKU.Text = this.GetGyoushaName();
                    this.form.BIKOU1.Text = result.BIKOU1;
                    this.form.BIKOU2.Text = result.BIKOU2;
                    this.SearchGenba();
                    this.SetIchiranGenba();
                }
                else
                {
                    // メッセージ表示
                    this.form.errmessage.MessageBoxShow("E045");

                    //フォーカスを紐付SystemIDにする
                    this.form.HIMOZUKE_SYSTEM_ID.Focus();
                }
            }
        }

        /// <summary>
        /// 委託契約情報のチェック
        /// </summary>
        internal bool CheckKeiyakuInfo()
        {
            if (!string.IsNullOrEmpty(this.form.HIMOZUKE_SYSTEM_ID.Text))
            {
                M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                cond.SYSTEM_ID = this.form.HIMOZUKE_SYSTEM_ID.Text.PadLeft(9, '0');

                M_ITAKU_KEIYAKU_KIHON result = itakuKeiyakuDao.GetDataBySystemId(cond);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 排出事業者名の取得
        /// </summary>
        /// <returns></returns>
        private string GetGyoushaName()
        {
            string gyoushaName = string.Empty;

            if (!string.IsNullOrEmpty(this.form.HAISHUTSU_JIGYOUSHA_CD.Text))
            {
                M_GYOUSHA result = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDataByCd(this.form.HAISHUTSU_JIGYOUSHA_CD.Text);
                if (result != null)
                {
                    gyoushaName = result.GYOUSHA_NAME_RYAKU;
                }
            }
            return gyoushaName;
        }

        /// <summary>
        /// データ取得処理(排出事業場)
        /// </summary>
        /// <returns></returns>
        internal void SearchGenba()
        {
            LogUtility.DebugMethodStart();

            this.SearchResultGenba = new DataTable();
            var sql = new StringBuilder();
            sql.Append(" SELECT ");
            sql.Append("        MITAKU.HAISHUTSU_JIGYOUJOU_CD AS GENBA_CD, ");
            sql.Append("        MGEN.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            sql.Append("        MTO.TODOUFUKEN_NAME_RYAKU AS GENBA_TODOUFUKEN_CD, ");
            sql.Append("        MGEN.GENBA_ADDRESS1 AS GENBA_ADDRESS1, ");
            sql.Append("        MGEN.GENBA_ADDRESS2 AS GENBA_ADDRESS2 ");
            sql.Append(" FROM M_ITAKU_KEIYAKU_KIHON_HST_GENBA AS MITAKU ");
            sql.Append(" LEFT JOIN M_GENBA MGEN  ");
            sql.Append("        ON MITAKU.HAISHUTSU_JIGYOUSHA_CD = MGEN.GYOUSHA_CD AND MITAKU.HAISHUTSU_JIGYOUJOU_CD = MGEN.GENBA_CD ");
            sql.Append(" LEFT JOIN M_TODOUFUKEN MTO ");
            sql.Append("        ON MGEN.GENBA_TODOUFUKEN_CD = MTO.TODOUFUKEN_CD ");
            sql.AppendFormat(" WHERE MITAKU.SYSTEM_ID = {0} ", this.form.HIMOZUKE_SYSTEM_ID.Text.PadLeft(9, '0'));
            sql.AppendFormat("       AND MITAKU.HAISHUTSU_JIGYOUSHA_CD = '{0}' ", this.form.HAISHUTSU_JIGYOUSHA_CD.Text);
            string mcreateSql = sql.ToString();

            this.SearchResultGenba = itakuKeiyakuDao.GetDateForStringSql(mcreateSql);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索結果を排出事業場一覧に設定
        /// </summary>
        internal void SetIchiranGenba()
        {
            this.form.ITAKU_KEIYAKU_ICHIRAN.IsBrowsePurpose = false;
            var table = this.SearchResultGenba;
            table.BeginLoadData();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }
            this.form.ITAKU_KEIYAKU_ICHIRAN.DataSource = table;
            this.form.ITAKU_KEIYAKU_ICHIRAN.IsBrowsePurpose = true;
        }

        /// <summary>
        /// 委託契約情報のクリア
        /// </summary>
        internal void ClearKeiyakuInfo()
        {
            this.form.HAISHUTSU_JIGYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.BIKOU1.Text = string.Empty;
            this.form.BIKOU2.Text = string.Empty;
            this.form.ITAKU_KEIYAKU_ICHIRAN.DataSource = null;
        }

		//PhuocLoc 2022/03/14 #161423 -Start
        /// <summary>
        /// 相手方一覧に設定
        /// </summary>
        internal void SetPartnerOrganize()
        {
            DataTable table = new DataTable();
            table.Columns.Add("NO");
            table.Columns.Add("PARTNER_ORGANIZE_NM");

            int NoCol = 1;
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM2))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM2;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM3))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM3;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM4))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM4;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM5))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM5;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM6))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM6;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM7))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM7;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM8))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM8;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM9))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM9;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM10))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM10;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM11))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM11;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM12))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM12;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM13))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM13;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM14))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM14;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM15))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM15;
                table.Rows.Add(row);
                NoCol++;
            }
            if (!string.IsNullOrEmpty(this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM16))
            {
                DataRow row = table.NewRow();
                row["NO"] = NoCol;
                row["PARTNER_ORGANIZE_NM"] = this.wanSignKeiyakuInfoEntity.PARTNER_ORGANIZE_NM16;
                table.Rows.Add(row);
                NoCol++;
            }

            this.form.PARTNER_ORGANIZE_NM_ICHIRAN.IsBrowsePurpose = false;
            table.BeginLoadData();
            this.form.PARTNER_ORGANIZE_NM_ICHIRAN.DataSource = table;
            this.form.PARTNER_ORGANIZE_NM_ICHIRAN.IsBrowsePurpose = true;
        }
        //PhuocLoc 2022/03/14 #161423 -End

        /// <summary>
        /// 更新期間単位 ポップアップ初期化
        /// </summary>
        internal void RenewalPeriodUnitPopUpDataInit()
        {
            LogUtility.DebugMethodStart();

            // 表示用データ設定
            this.RenewalPeriodUnitData = new DataTable();

            // タイトル
            this.RenewalPeriodUnitData.TableName = DenshiBunshoHoshuConstans.POPUP_TITLE_RENEWWAL_PERIOD_UNIT;

            // 列定義
            this.RenewalPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_RENEWWAL_PERIOD_UNIT_CD, typeof(String));
            this.RenewalPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_RENEWWAL_PERIOD_UNIT_NAME, typeof(String));

            // データ
            this.RenewalPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_1.ToString(), DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_1.ToString());
            this.RenewalPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_2.ToString(), DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_2.ToString());
            this.RenewalPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_3.ToString(), DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_3.ToString());

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 更新期間単位名称の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        internal bool SetRenewalPeriodUnitName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string RenewalPeriodUnitCD = string.Empty;
                if (!string.IsNullOrEmpty(this.form.RENEWWAL_PERIOD_UNIT.Text))
                {
                    RenewalPeriodUnitCD = this.form.RENEWWAL_PERIOD_UNIT.Text;
                }

                string RenewalPeriodUnitName = this.GetRenewalPeriodUnitName(RenewalPeriodUnitCD);

                this.form.RENEWWAL_PERIOD_UNIT_NAME.Text = RenewalPeriodUnitName;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetRenewalPeriodUnitName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 更新期間単位名称取得処理
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        internal string GetRenewalPeriodUnitName(object RenewalPeriodUnitCd)
        {
            LogUtility.DebugMethodStart(RenewalPeriodUnitCd);

            string cd = string.Empty;
            if (RenewalPeriodUnitCd != null && !string.IsNullOrWhiteSpace(RenewalPeriodUnitCd.ToString()))
            {
                cd = RenewalPeriodUnitCd.ToString().PadLeft(2, '0');
            }

            string RenewalPeriodUnitName = string.Empty;
            switch (cd)
            {
                case DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_1:
                    RenewalPeriodUnitName = DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_1;
                    break;

                case DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_2:
                    RenewalPeriodUnitName = DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_2;
                    break;

                case DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_CD_3:
                    RenewalPeriodUnitName = DenshiBunshoHoshuConstans.RENEWWAL_PERIOD_UNIT_TEXT_3;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(RenewalPeriodUnitName);
            return RenewalPeriodUnitName;
        }

        /// <summary>
        /// 解約通知期限単位 ポップアップ初期化
        /// </summary>
        internal void CancelPeriodUnitPopUpDataInit()
        {
            LogUtility.DebugMethodStart();

            // 表示用データ設定
            this.CancelPeriodUnitData = new DataTable();

            // タイトル
            this.CancelPeriodUnitData.TableName = DenshiBunshoHoshuConstans.POPUP_TITLE_CANCEL_PERIOD_UNIT;

            // 列定義
            this.CancelPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_CANCEL_PERIOD_UNIT_CD, typeof(String));
            this.CancelPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_CANCEL_PERIOD_UNIT_NAME, typeof(String));

            // データ
            this.CancelPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_1.ToString(), DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_1.ToString());
            this.CancelPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_2.ToString(), DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_2.ToString());
            this.CancelPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_3.ToString(), DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_3.ToString());

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 解約通知期限単位名称の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        internal bool SetCancelPeriodUnitName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string CancelPeriodUnitCD = string.Empty;
                if (!string.IsNullOrEmpty(this.form.CANCEL_PERIOD_UNIT.Text))
                {
                    CancelPeriodUnitCD = this.form.CANCEL_PERIOD_UNIT.Text;
                }

                string CancelPeriodUnitName = this.GetCancelPeriodUnitName(CancelPeriodUnitCD);

                this.form.CANCEL_PERIOD_UNIT_NAME.Text = CancelPeriodUnitName;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetCancelPeriodUnitName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 解約通知期限単位名称取得処理
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        internal string GetCancelPeriodUnitName(object CancelPeriodUnitCd)
        {
            LogUtility.DebugMethodStart(CancelPeriodUnitCd);

            string cd = string.Empty;
            if (CancelPeriodUnitCd != null && !string.IsNullOrWhiteSpace(CancelPeriodUnitCd.ToString()))
            {
                cd = CancelPeriodUnitCd.ToString().PadLeft(2, '0');
            }

            string CancelPeriodUnitName = string.Empty;
            switch (cd)
            {
                case DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_1:
                    CancelPeriodUnitName = DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_1;
                    break;

                case DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_2:
                    CancelPeriodUnitName = DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_2;
                    break;

                case DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_CD_3:
                    CancelPeriodUnitName = DenshiBunshoHoshuConstans.CANCEL_PERIOD_UNIT_TEXT_3;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(CancelPeriodUnitName);
            return CancelPeriodUnitName;
        }

        /// <summary>
        /// リマインダー期限単位 ポップアップ初期化
        /// </summary>
        internal void ReminderPeriodUnitPopUpDataInit()
        {
            LogUtility.DebugMethodStart();

            // 表示用データ設定
            this.ReminderPeriodUnitData = new DataTable();

            // タイトル
            this.ReminderPeriodUnitData.TableName = DenshiBunshoHoshuConstans.POPUP_TITLE_REMINDER_PERIOD_UNIT;

            // 列定義
            this.ReminderPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_REMINDER_PERIOD_UNIT_CD, typeof(String));
            this.ReminderPeriodUnitData.Columns.Add(DenshiBunshoHoshuConstans.COLUMN_REMINDER_PERIOD_UNIT_NAME, typeof(String));

            // データ
            this.ReminderPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_1.ToString(), DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_1.ToString());
            this.ReminderPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_2.ToString(), DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_2.ToString());
            this.ReminderPeriodUnitData.Rows.Add(DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_3.ToString(), DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_3.ToString());

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// リマインダー期限単位名称の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        internal bool SetReminderPeriodUnitName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string ReminderPeriodUnitCD = string.Empty;
                if (!string.IsNullOrEmpty(this.form.REMINDER_PERIOD_UNIT.Text))
                {
                    ReminderPeriodUnitCD = this.form.REMINDER_PERIOD_UNIT.Text;
                }

                string ReminderPeriodUnitName = this.GetReminderPeriodUnitName(ReminderPeriodUnitCD);

                this.form.REMINDER_PERIOD_UNIT_NAME.Text = ReminderPeriodUnitName;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetReminderPeriodUnitName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// リマインダー期限単位名称取得処理
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        internal string GetReminderPeriodUnitName(object ReminderPeriodUnitCd)
        {
            LogUtility.DebugMethodStart(ReminderPeriodUnitCd);

            string cd = string.Empty;
            if (ReminderPeriodUnitCd != null && !string.IsNullOrWhiteSpace(ReminderPeriodUnitCd.ToString()))
            {
                cd = ReminderPeriodUnitCd.ToString().PadLeft(2, '0');
            }

            string ReminderPeriodUnitName = string.Empty;
            switch (cd)
            {
                case DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_1:
                    ReminderPeriodUnitName = DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_1;
                    break;

                case DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_2:
                    ReminderPeriodUnitName = DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_2;
                    break;

                case DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_CD_3:
                    ReminderPeriodUnitName = DenshiBunshoHoshuConstans.REMINDER_PERIOD_UNIT_TEXT_3;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(ReminderPeriodUnitName);
            return ReminderPeriodUnitName;
        }

        /// <summary>
        /// 自動更新（WAN）の変更のイベント
        /// </summary>
        internal void ChangeAutoUpdating()
        {
            if (this.form.IS_AUTO_UPDATING.Text == DenshiBunshoHoshuConstans.IS_USING)
            {
                this.form.RENEWWAL_PERIOD.ReadOnly = false;
                this.form.RENEWWAL_PERIOD_UNIT.ReadOnly = false;
                this.form.RENEWWAL_PERIOD.TabStop = true;
                this.form.RENEWWAL_PERIOD_UNIT.TabStop = true;
            }
            else
            {
                this.form.RENEWWAL_PERIOD.Text = string.Empty;
                this.form.RENEWWAL_PERIOD_UNIT.Text = string.Empty;
                this.form.RENEWWAL_PERIOD_UNIT_NAME.Text = string.Empty;
                this.form.RENEWWAL_PERIOD.ReadOnly = true;
                this.form.RENEWWAL_PERIOD_UNIT.ReadOnly = true;
                this.form.RENEWWAL_PERIOD.TabStop = false;
                this.form.RENEWWAL_PERIOD_UNIT.TabStop = false;
            }
        }

        /// <summary>
        /// ﾘﾏｲﾝﾄﾞ通知（WAN）の変更のイベント
        /// </summary>
        internal void ChangeReminder()
        {
            if (this.form.IS_REMINDER.Text == DenshiBunshoHoshuConstans.IS_USING)
            {
                this.form.REMINDER_PERIOD.ReadOnly = false;
                this.form.REMINDER_PERIOD_UNIT.ReadOnly = false;
                this.form.REMINDER_PERIOD.TabStop = true;
                this.form.REMINDER_PERIOD_UNIT.TabStop = true;
            }
            else
            {
                this.form.REMINDER_PERIOD.Text = string.Empty;
                this.form.REMINDER_PERIOD_UNIT.Text = string.Empty;
                this.form.REMINDER_PERIOD_UNIT_NAME.Text = string.Empty;
                this.form.REMINDER_PERIOD.ReadOnly = true;
                this.form.REMINDER_PERIOD_UNIT.ReadOnly = true;
                this.form.REMINDER_PERIOD.TabStop = false;
                this.form.REMINDER_PERIOD_UNIT.TabStop = false;
            }
        }

        /// <summary>
        /// 日付のフォーマット変換を行う
        /// </summary>
        public void ChangeDateTimeFormat(CustomTextBox ctrl)
        {
            //ReadOnly対応
            if (ctrl.ReadOnly)
            {
                return;
            }

            string value = ctrl.Text;
            if (string.IsNullOrEmpty(value))
            {
                return; //空の場合は処理不要
            }

            if (ctrl.Text.Length == 8)
            {
                DateTime dt;
                if (DateTime.TryParseExact(ctrl.Text, "yyyyMMdd", null, DateTimeStyles.None, out dt))
                {
                    ctrl.Text = dt.ToString("yyyy/MM/dd");
                    ctrl.SelectionStart = ctrl.TextLength;
                }
            }
        }

        /// <summary>
        /// 数字のフォーマット変換を行う
        /// </summary>
        public void ChangeNumberFormat(CustomTextBox ctrl)
        {
            //ReadOnly対応
            if (ctrl.ReadOnly)
            {
                return;
            }

            string value = ctrl.Text;
            if (string.IsNullOrEmpty(value))
            {
                return; //空の場合は処理不要
            }

            long dt;
            if (long.TryParse(ctrl.Text, out dt))
            {
                ctrl.Text = dt.ToString("#,##0");
                ctrl.SelectionStart = ctrl.TextLength;
            }
        }

        /// <summary>
        /// 数字のフォーマット変換を行う
        /// </summary>
        public string GetResultText(CustomTextBox ctrl)
        {
            //ReadOnly対応
            if (ctrl.ReadOnly)
            {
                return string.Empty;
            }

            string value = ctrl.Text;
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty; //空の場合は処理不要
            }

            // SuperEntity取得する時、「,」削除対応。
            return ctrl.Text.Replace(",", "");
        }

        /// <summary>
        /// 文書詳細情報編集API
        /// </summary>
        internal bool WanSignDocumentDetailUpdate()
        {
            #region 1．アクセストークン生成API
            //アクセストークン取得
            var accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();
            if (accessToken == null)
            {
                return false;
            }

            var dateTimeOut = DateTime.Now.AddMinutes(30);
            #endregion

            #region 2．文書詳細情報編集API
            M_WANSIGN_KEIYAKU_INFO wansignInfoEntity = this.listWansignKeiyakuInfoEntity[0];
            wansignInfoEntity.ORIGINAL_CONTROL_NUMBER = null;
            if (string.IsNullOrEmpty(this.form.ORIGINAL_CONTROL_NUMBER.Text))
            {
                wansignInfoEntity.ORIGINAL_CONTROL_NUMBER = this.form.ORIGINAL_CONTROL_NUMBER.Text;
            }
            else
            {
                M_WANSIGN_KEIYAKU_INFO[] listWansignInfo = this.wanSignKeiyakuInfoDao.GetDataByKanriBango(this.form.ORIGINAL_CONTROL_NUMBER.Text);
                if (listWansignInfo == null || listWansignInfo.Length == 0)
                {
                    wansignInfoEntity.ORIGINAL_CONTROL_NUMBER = this.form.ORIGINAL_CONTROL_NUMBER.Text;
                }
            }

            var dataResult = this.denshiWanSignLogic.WanSignDocumentDetailUpdate(accessToken.Result.Access_Token, wansignInfoEntity);
            if (dataResult == null || dataResult.Result == null)
            {
                return false;
            }

            //APIレスポンス-satus≠０（0以外）
            if (!"0".Equals(dataResult.Status))
            {
                this.form.errmessage.MessageBoxShow("E337", dataResult.Status);
                return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// フィールド項目の設定
        /// </summary>
        /// <param name="levelKbn"></param>
        internal void FieldControlSetting(int levelKbn)
        {
            switch (levelKbn)
            {
                case 1:
                    if (this.systemInfoEntity.WAN_SIGN_FIELD_1 == DenshiBunshoHoshuConstans.IS_USING_FIELD)
                    {
                        this.form.FIELD_STR_1.ReadOnly = false;
                        this.form.FIELD_STR_1.TabStop = true;
                        FieldControlSetting(2);
                    }
                    else
                    {
                        this.form.FIELD_STR_1.ReadOnly = true;
                        this.form.FIELD_STR_1.TabStop = false;
                        this.form.FIELD_STR_2.ReadOnly = true;
                        this.form.FIELD_STR_2.TabStop = false;
                        this.form.FIELD_STR_3.ReadOnly = true;
                        this.form.FIELD_STR_3.TabStop = false;
                        this.form.FIELD_STR_4.ReadOnly = true;
                        this.form.FIELD_STR_4.TabStop = false;
                        this.form.FIELD_STR_5.ReadOnly = true;
                        this.form.FIELD_STR_5.TabStop = false;
                    }
                    break;
                case 2:
                    if (this.systemInfoEntity.WAN_SIGN_FIELD_2 == DenshiBunshoHoshuConstans.IS_USING_FIELD)
                    {
                        this.form.FIELD_STR_2.ReadOnly = false;
                        this.form.FIELD_STR_2.TabStop = true;
                        FieldControlSetting(3);
                    }
                    else
                    {
                        this.form.FIELD_STR_2.ReadOnly = true;
                        this.form.FIELD_STR_2.TabStop = false;
                        this.form.FIELD_STR_3.ReadOnly = true;
                        this.form.FIELD_STR_3.TabStop = false;
                        this.form.FIELD_STR_4.ReadOnly = true;
                        this.form.FIELD_STR_4.TabStop = false;
                        this.form.FIELD_STR_5.ReadOnly = true;
                        this.form.FIELD_STR_5.TabStop = false;
                    }
                    break;
                case 3:
                    if (this.systemInfoEntity.WAN_SIGN_FIELD_3 == DenshiBunshoHoshuConstans.IS_USING_FIELD)
                    {
                        this.form.FIELD_STR_3.ReadOnly = false;
                        this.form.FIELD_STR_3.TabStop = true;
                        FieldControlSetting(4);
                    }
                    else
                    {
                        this.form.FIELD_STR_3.ReadOnly = true;
                        this.form.FIELD_STR_3.TabStop = false;
                        this.form.FIELD_STR_4.ReadOnly = true;
                        this.form.FIELD_STR_4.TabStop = false;
                        this.form.FIELD_STR_5.ReadOnly = true;
                        this.form.FIELD_STR_5.TabStop = false;
                    }
                    break;
                case 4:
                    if (this.systemInfoEntity.WAN_SIGN_FIELD_4 == DenshiBunshoHoshuConstans.IS_USING_FIELD)
                    {
                        this.form.FIELD_STR_4.ReadOnly = false;
                        this.form.FIELD_STR_4.TabStop = true;
                        FieldControlSetting(5);
                    }
                    else
                    {
                        this.form.FIELD_STR_4.ReadOnly = true;
                        this.form.FIELD_STR_4.TabStop = false;
                        this.form.FIELD_STR_5.ReadOnly = true;
                        this.form.FIELD_STR_5.TabStop = false;
                    }
                    break;
                case 5:
                    if (this.systemInfoEntity.WAN_SIGN_FIELD_5 == DenshiBunshoHoshuConstans.IS_USING_FIELD)
                    {
                        this.form.FIELD_STR_5.ReadOnly = false;
                        this.form.FIELD_STR_5.TabStop = true;
                    }
                    else
                    {
                        this.form.FIELD_STR_5.ReadOnly = true;
                        this.form.FIELD_STR_5.TabStop = false;
                    }
                    break;
            }
        }

        #region subF2 契約書ダウンロード
        /// <summary>
        /// 契約書関連ファイルの出力ダイアログ処理
        /// </summary>
        internal string SetOutputFolder()
        {
            var directoryName = string.Empty;
            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "ファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            if (string.IsNullOrWhiteSpace(directoryName))
            {
                return string.Empty;
            }

            return directoryName;
        }

        /// <summary>
        /// 契約書ダウンロード
        /// </summary>
        /// <param name="dir">フォルダパス</param>
        /// <returns></returns>
        internal bool KeiyakuDownLoad(string dir)
        {
            bool ret = false;

            #region アクセストークン生成API
            //アクセストークン取得
            var accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();
            if (accessToken == null)
            {
                return ret;
            }

            var dateTimeOut = DateTime.Now.AddMinutes(30);
            #endregion

            //関連コードリスト
            var listControlNumber = new List<string>();

            //アクセストークンの有効期限は発行された時間を起点として30分です。
            //各APIで有効期限が切れていると判定された場合は当APIを実行し新たなアクセストークンを取得してください。
            var dateNow = DateTime.Now;
            if (dateNow > dateTimeOut)
            {
                accessToken = this.denshiWanSignLogic.GetAccessTokenWanSign();
                if (accessToken == null)
                {
                    ret = false;
                    return ret;
                }
            }

            //関連コード
            var controlNumber = this.wanSignKeiyakuInfoEntity.CONTROL_NUMBER;
            if (!string.IsNullOrEmpty(controlNumber))
            {
                if (!listControlNumber.Contains(controlNumber))
                {
                    //関連コードリスト
                    listControlNumber.Add(controlNumber);

                    #region ２．２文書取得API
                    ret = this.denshiWanSignLogic.DownLoadKeyakuWanSign(dir, accessToken.Result.Access_Token, controlNumber);
                    #endregion
                }
            }

            return ret;
        }
        #endregion
    }
}