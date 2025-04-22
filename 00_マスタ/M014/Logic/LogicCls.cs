using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.OboegakiIkkatuHoshu.APP;
using Shougun.Core.Master.OboegakiIkkatuHoshu.Dao;
using Shougun.Core.Master.OboegakiIkkatuHoshu.DTO;
using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.OboegakiIkkatuHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region 変数

        /// <summary>
        /// DTO
        /// </summary>
        private DTOCls dto;

        /// <summary>
        /// Header
        /// </summary>
        private UIHeader header;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 業者のDao
        /// </summary>
        public IM_GYOUSHADao gyousyaDao;

        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        public IM_GENBADao genbaDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 委託契約基本のDao
        /// </summary>
        private IMITAKUKEIYAKUKIHONDao kihonDao;

        /// <summary>
        ///覚書一括明細のDao
        /// </summary>
        private ItakuMemoIkkatsuDetailDAO mDetailDao;

        /// <summary>
        /// 覚書一括Dao
        /// </summary>
        private ItakuMemoIkkatsuEntryDAO mEntryDao;

        /// <summary>
        /// 委託契約別表覚書Dao
        /// </summary>
        private ItakuKeiyakuOboeDao mOboeDao;

        /// <summary>
        /// 委託契約別表3_処分Dao
        /// </summary>
        private ItakuKeiyakuBetsu3Dao mBetsu3Dao;

        /// <summary>
        /// 委託契約別表4_最終処分Dao
        /// </summary>
        private ItakuKeiyakuBetsu4Dao mBetsu4Dao;

        /// <summary>
        /// IS_NUMBER_SYSTEMDao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// IS_NUMBER_DENSHUDao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 伝票番号
        /// </summary>
        public String mDenpyouNumber;

        /// <summary>
        /// ButtonSetting.xmlファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.OboegakiIkkatuHoshu.Setting.ButtonSetting.xml";

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal BusinessBaseForm parentForm;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        #endregion 変数

        #region プロパティ

        /// <summary>
        /// 伝票番号検索結果
        /// </summary>
        public DataTable SearchItakuMemoIkkatsuEntryResult { get; set; }

        /// <summary>
        /// 検索条件の結果
        /// </summary>
        public DataTable SearchItakuMemoIkkatsuDetailResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<T_ITAKU_MEMO_IKKATSU_ENTRY> ItakuMemoIkkatsuEntryList { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        #endregion プロパティ

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;
            this.SearchString = new DTOCls();
            this.dto = new DTOCls();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mEntryDao = DaoInitUtility.GetComponent<ItakuMemoIkkatsuEntryDAO>();
            this.mDetailDao = DaoInitUtility.GetComponent<ItakuMemoIkkatsuDetailDAO>();
            this.kihonDao = DaoInitUtility.GetComponent<IMITAKUKEIYAKUKIHONDao>();
            this.mOboeDao = DaoInitUtility.GetComponent<ItakuKeiyakuOboeDao>();
            this.mBetsu3Dao = DaoInitUtility.GetComponent<ItakuKeiyakuBetsu3Dao>();
            this.mBetsu4Dao = DaoInitUtility.GetComponent<ItakuKeiyakuBetsu4Dao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();                 // 現場マスタのDao
            this.gyousyaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();                 // 業者マスタのDao
            LogUtility.DebugMethodEnd();
        }

        #endregion コンストラクタ

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                var parentForm = (BusinessBaseForm)this.form.Parent;

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ヘッダー項目
                this.header = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                // 処理モード別画面初期化
                if (!this.ModeInit(windowType, parentForm))
                {
                    return false;
                }
                //システム情報を取得し、初期値をセットする
                this.HearerSysInfoInit();
                // this.allControl = this.form.allControl;
                parentForm.txb_process.Enabled = false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType);
            }
            return true;
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">画面種別</param>
        /// <param name="parentForm">親フォーム</param>
        public bool ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            bool ret = true;
            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNewMode(parentForm);
                    // 初期フォーカス設定
                    this.form.txtDenpyouNumber.Focus();
                    ret = true;
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    ret = this.WindowInitUpdate(parentForm);
                    // 初期フォーカス設定
                    this.form.dtpMemoUpdateDate.Focus();
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    ret = this.WindowInitDelete(parentForm);
                    // 初期フォーカス設定
                    this.form.dtpMemoUpdateDate.Focus();
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    ret = this.WindowInitReference(parentForm);
                    // 初期フォーカス設定
                    this.form.dtpMemoUpdateDate.Focus();
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNewMode(parentForm);
                    // 初期フォーカス設定
                    this.form.txtDenpyouNumber.Focus();
                    ret = true;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNewMode(BusinessBaseForm parentForm)
        {
            // 全コントロール操作可能とする
            this.AllControlLock(false);

            // 処理モードを修正に設定
            this.form.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
            // ヘッダー項目
            this.header.CreateDate.Text = string.Empty;
            this.header.CreateUser.Text = string.Empty;
            this.header.LastUpdateDate.Text = string.Empty;
            this.header.LastUpdateUser.Text = string.Empty;
            this.header.ReadDataNumber.Text = "0";

            // 入力項目
            this.form.txtDenpyouNumber.Text = string.Empty;
            this.form.txtSeq.Text = string.Empty;
            this.form.txtSystemId.Text = string.Empty;

            // 20150922 katen #12048 「システム日付」の基準作成、適用 start
            //this.form.dtpMemoUpdateDate.Value = DateTime.Today;
            this.form.dtpMemoUpdateDate.Value = this.parentForm.sysDate.Date;
            // 20150922 katen #12048 「システム日付」の基準作成、適用 end
            this.form.txtMemo.Text = string.Empty;
            this.form.txtHaishutsuJigyoushaCd.Text = string.Empty;
            this.form.txtHaishutsuJigyoushaNm.Text = string.Empty;
            this.form.txtGenbaCd.Text = string.Empty;
            this.form.txtGenbaNm.Text = string.Empty;
            this.form.txtUnbanGyoushaCd.Text = string.Empty;
            this.form.txtUnbanGyoushaNm.Text = string.Empty;
            this.form.txtShobunPatternNm.Text = string.Empty;
            this.form.txtShobunPatternSysId.Text = string.Empty;
            this.form.txtShobunPatternSeq.Text = string.Empty;
            this.form.txtLastShobunPatternNm.Text = string.Empty;
            this.form.txtLastShobunPatternSysId.Text = string.Empty;
            this.form.txtLastShobunPatternSeq.Text = string.Empty;
            this.form.dtpKeiyakuBegin.Value = null;
            this.form.dtpKeiyakuBeginTo.Value = null;
            this.form.dtpKeiyakuEnd.Value = null;
            this.form.dtpKeiyakuEndTo.Value = null;
            this.form.UPDATE_SHUBETSU.Text = "0";
            this.form.rbtShubetsu0.Checked = true;
            this.form.KEIYAKUSHO_SHURUI.Text = string.Empty;
            this.form.rdtShurui0.Checked = true;
            this.form.txtShobun.Text = "2";
            this.form.rdbShobun2.Checked = true;
            this.form.txtShobunPatternNm2.Text = string.Empty;
            this.form.txtShobunPatternSysId2.Text = string.Empty;
            this.form.txtShobunPatternSeq2.Text = string.Empty;
            this.form.txtLastShobun.Text = "2";
            this.form.rdbLastShobun2.Checked = true;
            this.form.txtLastShobunPatternNm2.Text = string.Empty;
            this.form.txtLastShobunPatternSysId2.Text = string.Empty;
            this.form.txtLastShobunPatternSeq2.Text = string.Empty;

            // 一覧のデータをセットする
            this.form.grdIchiran.Rows.Clear();
            this.form.grdIchiran.ColumnHeaders["columnHeaderSection1"].Cells["gcCustomColumnHeader1"].Value = false;

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func8.Enabled = true;    // 対象検索
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func12.Enabled = true;    // 閉じる

            // 処理モードを新規に設定
            //this.header.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
            //this.header.windowTypeLabel.Text = "新規";

            // 初期フォーカス設定
            this.form.txtDenpyouNumber.Focus();
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.WindowInitNewMode(parentForm);
                // 全コントロール操作可能とする
                this.AllControlLock(false);

                // 処理モードを修正に設定
                this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                int count = this.Search(this.mDenpyouNumber);
                if (count == -1)
                {
                    return false;
                }
                this.form.txtDenpyouNumber.Text = this.mDenpyouNumber;
                this.SetEntry();
                count = this.Search();
                if (count == -1)
                {
                    return false;
                }
                this.SetIchiran();

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = true;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;     // 対象検索
                parentForm.bt_func12.Enabled = true;    // 閉じる
                // 初期フォーカス設定
                this.form.dtpMemoUpdateDate.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitUpdate", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        public void InitDenpyouNumberUpdate(BusinessBaseForm parentForm)
        {
            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;     // 対象検索
            parentForm.bt_func12.Enabled = true;    // 閉じる

            // 処理モードを修正に設定
            this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            // 初期フォーカス設定
            this.form.txtDenpyouNumber.Focus();
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            this.WindowInitNewMode(parentForm);
            // 削除モード固有UI設定
            this.AllControlLock(true);
            // 処理モードを削除に設定
            this.form.SetWindowType(WINDOW_TYPE.DELETE_WINDOW_FLAG);
            this.form.txtDenpyouNumber.Text = this.mDenpyouNumber;
            int count = this.Search(this.mDenpyouNumber);
            if (count == -1)
            {
                return false;
            }
            this.SetEntry();
            count = this.Search();
            if (count == -1)
            {
                return false;
            }
            this.SetIchiran();

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func8.Enabled = false;   // 対象検索
            parentForm.bt_func12.Enabled = true;    // 閉じる

            //初期フォーカス設定
            this.form.dtpMemoUpdateDate.Focus();
            return true;
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.WindowInitNewMode(parentForm);
                // 参照モード固有UI設定
                this.AllControlLock(true);
                // 処理モードを参照に設定
                this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                this.form.txtDenpyouNumber.Text = this.mDenpyouNumber;
                int count = this.Search(this.mDenpyouNumber);
                if (count == -1)
                {
                    return false;
                }
                this.SetEntry();
                count = this.Search();
                if (count == -1)
                {
                    return false;
                }
                this.SetIchiran();

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func8.Enabled = false;    // 対象検索
                parentForm.bt_func12.Enabled = true;    // 閉じる

                //初期フォーカス設定
                this.form.dtpMemoUpdateDate.Focus();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInitReference", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
                this.header.alertNumber.Text = this.sysInfoEntity.ICHIRAN_ALERT_KENSUU.Value.ToString();
                // システム情報からアラート件数を取得
                this.alertCount = (int)this.sysInfoEntity.ICHIRAN_ALERT_KENSUU;
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            // 入力項目
            this.form.txtDenpyouNumber.ReadOnly = isBool;
            this.form.txtMemo.ReadOnly = isBool;
            this.form.txtHaishutsuJigyoushaCd.ReadOnly = isBool;
            this.form.txtGenbaCd.ReadOnly = isBool;
            this.form.txtUnbanGyoushaCd.ReadOnly = isBool;
            this.form.txtShobunPatternNm.ReadOnly = isBool;
            this.form.txtShobunPatternSysId.ReadOnly = isBool;
            this.form.txtShobunPatternSeq.ReadOnly = isBool;
            this.form.txtLastShobunPatternNm.ReadOnly = isBool;
            this.form.txtLastShobunPatternSysId.ReadOnly = isBool;
            this.form.txtLastShobunPatternSeq.ReadOnly = isBool;

            this.form.UPDATE_SHUBETSU.ReadOnly = isBool;
            this.form.KEIYAKUSHO_SHURUI.ReadOnly = isBool;

            this.form.rbtShubetsu0.Enabled = !isBool;
            this.form.rbtShubetsu1.Enabled = !isBool;
            this.form.rbtShubetsu2.Enabled = !isBool;
            this.form.rdtShurui0.Enabled = !isBool;
            this.form.rdtShurui1.Enabled = !isBool;
            this.form.rdtShurui2.Enabled = !isBool;
            this.form.rdtShurui3.Enabled = !isBool;
            this.form.rdbShobun1.Enabled = !isBool;
            this.form.rdbShobun2.Enabled = !isBool;
            this.form.rdbLastShobun1.Enabled = !isBool;
            this.form.rdbLastShobun2.Enabled = !isBool;

            this.form.dtpMemoUpdateDate.Enabled = !isBool;
            this.form.dtpKeiyakuBegin.Enabled = !isBool;
            this.form.dtpKeiyakuBeginTo.Enabled = !isBool;
            this.form.dtpKeiyakuEnd.Enabled = !isBool;
            this.form.dtpKeiyakuEndTo.Enabled = !isBool;

            this.form.btnSearchHaishutsuGyousha.Enabled = !isBool;
            this.form.btnSearchGenba.Enabled = !isBool;
            this.form.btnSearchUnbanGyousha.Enabled = !isBool;
            this.form.btnShobunPattern.Enabled = !isBool;

            this.form.btnLastShobunPattern.Enabled = !isBool;
            this.form.btnLastShobunPattern2.Enabled = !isBool;

            this.form.txtShobun.ReadOnly = isBool;
            //this.form.txtShobunPatternNm2.ReadOnly = isBool;
            //this.form.txtShobunPatternSysId2.ReadOnly = !isBool;
            //this.form.txtShobunPatternSeq2.ReadOnly = !isBool;
            //this.form.btnShobunPattern2.Enabled = !isBool;

            if ((this.form.txtShobun.Text == "2" && this.form.txtShobun.ReadOnly == false) || this.form.txtShobun.ReadOnly == true)
            {
                this.form.txtShobunPatternNm2.ReadOnly = true;
                this.form.txtShobunPatternSysId2.ReadOnly = true;
                this.form.txtShobunPatternSeq2.ReadOnly = true;
                this.form.btnShobunPattern2.Enabled = false;
            }
            else
            {
                this.form.txtShobunPatternNm2.ReadOnly = isBool;
                this.form.txtShobunPatternSysId2.ReadOnly = !isBool;
                this.form.txtShobunPatternSeq2.ReadOnly = !isBool;
                this.form.btnShobunPattern2.Enabled = !isBool;
            }
            this.form.txtLastShobun.ReadOnly = isBool;
            if ((this.form.txtLastShobun.Text == "2" && this.form.txtLastShobun.ReadOnly == false) || this.form.txtLastShobun.ReadOnly == true)
            {
                this.form.txtLastShobunPatternNm2.ReadOnly = true;
                this.form.txtLastShobunPatternSysId2.ReadOnly = true;
                this.form.txtLastShobunPatternSeq2.ReadOnly = true;
                this.form.btnLastShobunPattern2.Enabled = false;
            }
            else
            {
                this.form.txtLastShobunPatternNm2.ReadOnly = isBool;
                this.form.txtLastShobunPatternSysId2.ReadOnly = isBool;
                this.form.txtLastShobunPatternSeq2.ReadOnly = isBool;
                this.form.btnLastShobunPattern2.Enabled = !isBool;
            }

            this.form.grdIchiran.ReadOnly = isBool;
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 「契約書種類」項目を非表示

            this.form.label9.Visible = false;
            this.form.panel3.Visible = false;
        }

        #endregion 画面初期化処理

        #region ボタン初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart(parentForm);

            var buttonSetting = this.CreateButtonInfo();
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                switch (this.form.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        cont.Text = button.NewButtonName;
                        break;

                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        cont.Text = button.ReferButtonName;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        cont.Text = button.UpdateButtonName;
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        cont.Text = button.DeleteButtonName;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion ボタン初期化

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                //新規ボタン(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                //修正ボタン(F3)イベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                //一覧ボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                this.form.txtHaishutsuJigyoushaCd.Enter += new EventHandler(this.form.txtHaishutsuJigyoushaCd_Enter);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// パタンーン設定する
        /// </summary>
        public void SetPatternData(CustomTextBox itemName, CustomTextBox itemSysId, CustomTextBox itemSeq, int LastSbnKbn)
        {
            try
            {
                LogUtility.DebugMethodStart(itemName, itemSysId, itemSeq, LastSbnKbn);

                M_SBNB_PATTERN data = new M_SBNB_PATTERN();
                string name = itemName.Text.Trim();

                data.PATTERN_NAME = name;
                data.LAST_SBN_KBN = (Int16)LastSbnKbn;
                if (string.IsNullOrEmpty(data.PATTERN_NAME))
                {
                    itemSysId.Text = string.Empty;
                    itemSeq.Text = string.Empty;
                    return;
                }

                // マスタデータを取得する
                DataTable pattern = this.mEntryDao.GetPatternName(data);
                if (pattern == null || pattern.Rows.Count <= 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "処分場パターン");
                    itemName.Text = string.Empty;
                    itemSysId.Text = string.Empty;
                    itemSeq.Text = string.Empty;
                    itemName.Focus();
                    return;
                }

                if (pattern != null)
                {
                    // SystemIdをセットする
                    if (itemSysId != null)
                    {
                        itemSysId.Text = pattern.Rows[0]["SYSTEM_ID"].ToString();
                    }

                    // Seqをセットする
                    if (itemSeq != null)
                    {
                        itemSeq.Text = pattern.Rows[0]["SEQ"].ToString();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetPatternData", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetPatternData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion イベントの初期化処理

        #region データ処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search(string pDenpyouNumber)
        {
            LogUtility.DebugMethodStart(pDenpyouNumber);
            int count = 0;
            try
            {
                this.SearchString = new DTOCls();
                //伝票番号
                this.SearchString.Denpyou_Number = pDenpyouNumber;

                //データ取得
                this.SearchItakuMemoIkkatsuEntryResult = mEntryDao.GetDataForEntryEntity(this.SearchString);
                count = this.SearchItakuMemoIkkatsuEntryResult == null ? 0 : this.SearchItakuMemoIkkatsuEntryResult.Rows.Count;
                int cnt = 0;
                // 情報が存在する場合のみ明細情報の取得を行う
                if (count != 0)
                {
                    this.dto.ItakuMemoIkkatsuEntry.SYSTEM_ID = Int64.Parse(this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["SYSTEM_ID"].ToString());
                    this.dto.ItakuMemoIkkatsuEntry.SEQ = Int32.Parse(this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["SEQ"].ToString());
                    this.dto.ItakuMemoIkkatsuEntry.DENPYOU_NUMBER = Int64.Parse(this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["DENPYOU_NUMBER"].ToString());

                    this.SearchItakuMemoIkkatsuDetailResult = mEntryDao.GetDataForDetailByDenpyouNumberEntity(this.dto.ItakuMemoIkkatsuEntry);
                    cnt = this.SearchItakuMemoIkkatsuDetailResult.Rows.Count;
                }

                Properties.Settings.Default.Save();

                if (count == 0)
                {
                    //MessageBox.Show(ConstCls.SearchEmptInfo, ConstCls.DialogTitle);
                }
                // ヘッダー項目
                this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult == null ? "0" : this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString();
                //this.header.alertNumber.Text = this.alertCount.ToString();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int cnt = 0;
            try
            {
                this.SearchString = new DTOCls();
                //検索条件設定
                this.SearchString.Hst_Gyousha_Cd = this.form.txtHaishutsuJigyoushaCd.Text.Trim();
                this.SearchString.Hst_Genba_Cd = this.form.txtGenbaCd.Text.Trim();
                this.SearchString.Unpan_Gyousha_Cd = this.form.txtUnbanGyoushaCd.Text.Trim();
                this.SearchString.Shobun_Pattern_Name = this.form.txtShobunPatternNm.Text.Trim();
                this.SearchString.Last_Shobun_Pattern_Name = this.form.txtLastShobunPatternNm.Text.Trim();
                this.SearchString.Keiyaku_Begin = string.IsNullOrEmpty(this.form.dtpKeiyakuBegin.Text.Trim()) ? null : this.form.dtpKeiyakuBegin.Text.Substring(0, 10).Trim();
                this.SearchString.Keiyaku_Begin_To = string.IsNullOrEmpty(this.form.dtpKeiyakuBeginTo.Text.Trim()) ? null : this.form.dtpKeiyakuBeginTo.Text.Substring(0, 10).Trim();
                this.SearchString.Keiyaku_End = string.IsNullOrEmpty(this.form.dtpKeiyakuEnd.Text.Trim()) ? null : this.form.dtpKeiyakuEnd.Text.Substring(0, 10).Trim();
                this.SearchString.Keiyaku_End_To = string.IsNullOrEmpty(this.form.dtpKeiyakuEndTo.Text.Trim()) ? null : this.form.dtpKeiyakuEndTo.Text.Substring(0, 10).Trim();
                this.SearchString.Update_Shubetsu = this.form.UPDATE_SHUBETSU.Text.Trim() == "0" ? string.Empty : this.form.UPDATE_SHUBETSU.Text.Trim();
                this.SearchString.Keiyakusho_Shurui = this.form.KEIYAKUSHO_SHURUI.Text.Trim() == "0" ? string.Empty : this.form.KEIYAKUSHO_SHURUI.Text.Trim();

                // 情報が存在する場合のみ明細情報の取得を行う
                DataTable result = mEntryDao.GetDataForDetailByJyokenEntity(this.SearchString);
                this.SearchItakuMemoIkkatsuDetailResult = result.Clone();
                string strtemp = string.Empty;
                foreach (DataRow row in result.Rows)
                {
                    if (string.IsNullOrEmpty(strtemp))
                    {
                        DataRow newRow = SearchItakuMemoIkkatsuDetailResult.NewRow();
                        strtemp = row["ITAKU_KEIYAKU_SYSTEM_ID"].ToString();
                        for (int i = 0; i < SearchItakuMemoIkkatsuDetailResult.Columns.Count; i++)
                        {
                            newRow[i] = row[i];
                        }
                        SearchItakuMemoIkkatsuDetailResult.Rows.Add(newRow);
                    }
                    else if (!strtemp.Equals(row["ITAKU_KEIYAKU_SYSTEM_ID"].ToString()))
                    {
                        DataRow newRow = SearchItakuMemoIkkatsuDetailResult.NewRow();
                        strtemp = row["ITAKU_KEIYAKU_SYSTEM_ID"].ToString();
                        for (int i = 0; i < SearchItakuMemoIkkatsuDetailResult.Columns.Count; i++)
                        {
                            newRow[i] = row[i];
                        }
                        SearchItakuMemoIkkatsuDetailResult.Rows.Add(newRow);
                    }
                }
                this.SearchItakuMemoIkkatsuDetailResult.AcceptChanges();
                cnt = this.SearchItakuMemoIkkatsuDetailResult.Rows.Count;
                this.form.grdIchiran.Rows.Clear();
                // ヘッダー項目
                this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString();
                //this.header.alertNumber.Text = this.alertCount.ToString();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return cnt;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetEntry()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索結果を設定する
                DataTable table = this.SearchItakuMemoIkkatsuEntryResult;

                // ヘッダー項目
                //this.header.windowTypeLabel.Text = shoriMode;

                this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult == null ? "0" : (string.IsNullOrEmpty(this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString()) ? "0" : this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString());
                //this.header.alertNumber.Text = this.alertCount.ToString();
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    // ヘッダー項目
                    this.header.CreateDate.Text = table.Rows[i]["CREATE_DATE"].ToString();
                    this.header.CreateUser.Text = table.Rows[i]["CREATE_USER"].ToString();
                    this.header.LastUpdateDate.Text = table.Rows[i]["UPDATE_DATE"].ToString();
                    this.header.LastUpdateUser.Text = table.Rows[i]["UPDATE_USER"].ToString();

                    //詳細情報項目
                    //  this.form.txtDenpyouNumber.Text = table.Rows[i]["DENPYOU_NUMBER"].ToString();
                    this.form.txtSystemId.Text = table.Rows[i]["SYSTEM_ID"].ToString();
                    this.form.txtSeq.Text = table.Rows[i]["SEQ"].ToString();
                    this.form.TIME_STAMP.Text = ConvertStrByte.ByteToString((byte[])table.Rows[i]["TIME_STAMP"]);
                    this.form.txtMemo.Text = table.Rows[i]["MEMO"].ToString();
                    this.form.dtpMemoUpdateDate.Value = string.IsNullOrEmpty(table.Rows[i]["MEMO_UPDATE_DATE"].ToString()) ? string.Empty : DateTime.Parse(table.Rows[i]["MEMO_UPDATE_DATE"].ToString()).ToString();

                    // 検索条件項目
                    this.form.txtHaishutsuJigyoushaCd.Text = table.Rows[i]["HST_GYOUSHA_CD"].ToString();
                    this.form.txtHaishutsuJigyoushaNm.Text = table.Rows[i]["HST_GYOUSHA_NAME"].ToString();
                    this.form.txtGenbaCd.Text = table.Rows[i]["HST_GENBA_CD"].ToString();
                    this.form.txtGenbaNm.Text = table.Rows[i]["HST_GENBA_NAME"].ToString();
                    this.form.txtUnbanGyoushaCd.Text = table.Rows[i]["UNPAN_GYOUSHA_CD"].ToString();
                    this.form.txtUnbanGyoushaNm.Text = table.Rows[i]["UNPAN_GYOUSHA_NAME"].ToString();
                    this.form.txtShobunPatternNm.Text = table.Rows[i]["SHOBUN_PATTERN_NAME"].ToString();
                    this.form.txtShobunPatternSysId.Text = table.Rows[i]["SHOBUN_PATTERN_SYSTEM_ID"].ToString();
                    this.form.txtShobunPatternSeq.Text = table.Rows[i]["SHOBUN_PATTERN_SEQ"].ToString();
                    this.form.txtLastShobunPatternNm.Text = table.Rows[i]["LAST_SHOBUN_PATTERN_NAME"].ToString();
                    this.form.txtLastShobunPatternSysId.Text = table.Rows[i]["LAST_SHOBUN_PATTERN_SYSTEM_ID"].ToString();
                    this.form.txtLastShobunPatternSeq.Text = table.Rows[i]["LAST_SHOBUN_PATTERN_SEQ"].ToString();
                    if (!string.IsNullOrEmpty(table.Rows[i]["KEIYAKU_BEGIN"].ToString()))
                    {
                        this.form.dtpKeiyakuBegin.Value = DateTime.Parse(table.Rows[i]["KEIYAKU_BEGIN"].ToString());
                    }
                    else
                    {
                        this.form.dtpKeiyakuBegin.Value = null;
                    }
                    if (!string.IsNullOrEmpty(table.Rows[i]["KEIYAKU_BEGIN_TO"].ToString()))
                    {
                        this.form.dtpKeiyakuBeginTo.Value = DateTime.Parse(table.Rows[i]["KEIYAKU_BEGIN_TO"].ToString());
                    }
                    else
                    {
                        this.form.dtpKeiyakuBeginTo.Value = null;
                    }
                    if (!string.IsNullOrEmpty(table.Rows[i]["KEIYAKU_END"].ToString()))
                    {
                        this.form.dtpKeiyakuEnd.Value = DateTime.Parse(table.Rows[i]["KEIYAKU_END"].ToString());
                    }
                    else
                    {
                        this.form.dtpKeiyakuEnd.Value = null;
                    }
                    if (!string.IsNullOrEmpty(table.Rows[i]["KEIYAKU_END_TO"].ToString()))
                    {
                        this.form.dtpKeiyakuEndTo.Value = DateTime.Parse(table.Rows[i]["KEIYAKU_END_TO"].ToString());
                    }
                    else
                    {
                        this.form.dtpKeiyakuEndTo.Value = null;
                    }
                    this.form.UPDATE_SHUBETSU.Text = string.IsNullOrEmpty(table.Rows[i]["UPDATE_SHUBETSU"].ToString()) ? "0" : table.Rows[i]["UPDATE_SHUBETSU"].ToString();

                    this.form.KEIYAKUSHO_SHURUI.Text = string.IsNullOrEmpty(table.Rows[i]["KEIYAKUSHO_SHURUI"].ToString()) ? "0" : table.Rows[i]["KEIYAKUSHO_SHURUI"].ToString();

                    //処分場所の更新
                    this.form.txtShobun.Text = string.IsNullOrEmpty(table.Rows[i]["SHOBUN_UPDATE_KBN"].ToString()) ? "2" : table.Rows[i]["SHOBUN_UPDATE_KBN"].ToString();
                    //this.form.rdbShobun1.Checked = true;
                    this.form.txtShobunPatternNm2.Text = table.Rows[i]["UPD_SHOBUN_PATTERN_NAME"].ToString();
                    this.form.txtShobunPatternSysId2.Text = table.Rows[i]["UPD_SHOBUN_PATTERN_SYSTEM_ID"].ToString();
                    this.form.txtShobunPatternSeq2.Text = table.Rows[i]["UPD_SHOBUN_PATTERN_SEQ"].ToString();
                    this.form.txtLastShobun.Text = string.IsNullOrEmpty(table.Rows[i]["LAST_SHOBUN_UPDATE_KBN"].ToString()) ? "2" : table.Rows[i]["LAST_SHOBUN_UPDATE_KBN"].ToString();
                    //this.form.rdbLastShobun1.Checked = true;
                    this.form.txtLastShobunPatternNm2.Text = table.Rows[i]["UPD_LAST_SHOBUN_PATTERN_NAME"].ToString();
                    this.form.txtLastShobunPatternSysId2.Text = table.Rows[i]["UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID"].ToString();
                    this.form.txtLastShobunPatternSeq2.Text = table.Rows[i]["UPD_LAST_SHOBUN_PATTERN_SEQ"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetEntry", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                DataTable table = this.SearchItakuMemoIkkatsuDetailResult;
                this.form.grdIchiran.IsBrowsePurpose = false;
                this.form.grdIchiran.Rows.Clear();
                if (table != null)
                {
                    table.BeginLoadData();
                    //Headarのアラート件数を処理する。
                    DialogResult result = DialogResult.Yes;
                    string strAlertCount = this.header.alertNumber.Text.Replace(",", "");
                    if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount) < table.Rows.Count)
                    {
                        MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                        result = showLogic.MessageBoxShow("C025");
                    }
                    if (result != DialogResult.Yes)
                    {
                        return ret;
                    }
                    int j = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        this.form.grdIchiran.Rows.Add();
                        if (j == 0)
                        {
                            this.form.grdIchiran.CurrentCell = this.form.grdIchiran.Rows[j].Cells["SHORI_KBN"];
                            this.form.grdIchiran.Rows[j].Cells["SHORI_KBN"].Selected = true;
                        }
                        j = i;
                        //this.form.grdIchiran.Rows[j].Cells["ROW_NO"].Value = i+1;
                        if (!string.IsNullOrEmpty(table.Rows[i]["SHORI_KBN"].ToString()))
                        {
                            this.form.grdIchiran.Rows[j].Cells["SHORI_KBN"].Value = table.Rows[i]["SHORI_KBN"];
                        }
                        this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_NO"].Value = table.Rows[i]["ITAKU_KEIYAKU_NO"].ToString();
                        this.form.grdIchiran.Rows[j].Cells["GYOUSHA_CD"].Value = table.Rows[i]["GYOUSHA_CD"];
                        this.form.grdIchiran.Rows[j].Cells["GYOUSHA_NAME"].Value = table.Rows[i]["GYOUSHA_NAME"];
                        this.form.grdIchiran.Rows[j].Cells["GYOUSHA_ADDRESS"].Value = table.Rows[i]["GYOUSHA_ADDRESS"];
                        this.form.grdIchiran.Rows[j].Cells["KOUSHIN_SHUBETSU"].Value = table.Rows[i]["KOUSHIN_SHUBETSU"];
                        this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_DATE_BEGIN"].Value = table.Rows[i]["ITAKU_KEIYAKU_DATE_BEGIN"];
                        this.form.grdIchiran.Rows[j].Cells["SHOBUN_PATTERN_NAME"].Value = table.Rows[i]["SHOBUN_PATTERN_NAME"];
                        this.form.grdIchiran.Rows[j].Cells["SHOBUN_PATTERN_SYSYTEM_ID"].Value = table.Rows[i]["SHOBUN_PATTERN_SYSYTEM_ID"];
                        this.form.grdIchiran.Rows[j].Cells["SHOBUN_PATTERN_SEQ"].Value = table.Rows[i]["SHOBUN_PATTERN_SEQ"];

                        this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_SYSTEM_ID"].Value = table.Rows[i]["ITAKU_KEIYAKU_SYSTEM_ID"].ToString();
                        this.form.grdIchiran.Rows[j].Cells["GENBA_CD"].Value = table.Rows[i]["GENBA_CD"];
                        this.form.grdIchiran.Rows[j].Cells["GENBA_NAME"].Value = table.Rows[i]["GENBA_NAME"];
                        this.form.grdIchiran.Rows[j].Cells["GENBA_ADDRESS"].Value = table.Rows[i]["GENBA_ADDRESS"];
                        this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_SHURUI"].Value = table.Rows[i]["ITAKU_KEIYAKU_SHURUI"];
                        int itakuKeiyakuShurui = 0;
                        if (table.Rows[i]["ITAKU_KEIYAKU_SHURUI"] != null
                            && int.TryParse(table.Rows[i]["ITAKU_KEIYAKU_SHURUI"].ToString(), out itakuKeiyakuShurui))
                        {
                            this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_SHURUI_NAME"].Value = this.GetItakuKeiyakuShuruiName(itakuKeiyakuShurui);
                        }
                        this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_DATE_END"].Value = table.Rows[i]["ITAKU_KEIYAKU_DATE_END"];
                        this.form.grdIchiran.Rows[j].Cells["LAST_SHOBUN_PATTERN_NAME"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_NAME"];
                        this.form.grdIchiran.Rows[i].Cells["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_SYSYTEM_ID"];
                        this.form.grdIchiran.Rows[j].Cells["LAST_SHOBUN_PATTERN_SEQ"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_SEQ"];
                        this.form.grdIchiran.Rows[j].Cells["TIME_STAMP"].Value = table.Rows[i]["TIME_STAMP"];
                    }
                }
                this.form.grdIchiran.IsBrowsePurpose = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion データ処理

        #region 一覧画面表示

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        public void ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                FormManager.OpenFormWithAuth("M421", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
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

        ///// <summary>
        /////  同一画面存在時処理
        ///// </summary>
        ///// <param name="mainForm">起動する予定のForm</param>
        ///// <returns>Dispose否フラグ</returns>
        //public bool DisposeScreenPresenceForm(SuperForm mainForm)
        //{
        //    var exists = false;

        //    foreach (Form openForm in Application.OpenForms)
        //    {
        //        if (mainForm.GetType() == openForm.GetType())
        //        {
        //            var superForm = openForm as SuperForm;

        //            if (superForm != null)
        //            {
        //                if (superForm.WindowType == mainForm.WindowType)
        //                {
        //                    exists = true;
        //                    var parentForm = openForm.ParentForm;
        //                    if (parentForm != null)
        //                    {
        //                        parentForm.BringToFront();
        //                    }
        //                    openForm.Dispose();
        //                    parentForm.Dispose();
        //                    return exists;
        //                }
        //            }
        //        }
        //    }
        //    return exists;
        //}

        #endregion 一覧画面表示

        #region Entityを作成する

        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanSystemId()
        {
            try
            {
                LogUtility.DebugMethodStart();
                int returnInt = 1;
                // 覚書一括の最大値+1を取得
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.OBOEGAKI_IKKATSU;

                var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.OBOEGAKI_IKKATSU;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberSystemDao.Update(updateEntity);
                }

                return returnInt;
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
        /// 伝票番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanOboegaku()
        {
            try
            {
                LogUtility.DebugMethodStart();
                int returnInt = -1;
                // 伝票番号の最大値+1を取得
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.OBOEGAKI_IKKATSU;

                var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.OBOEGAKI_IKKATSU;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberDenshuDao.Update(updateEntity);
                }

                return returnInt;
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

        public void CreateItakuMemoIkkatsuEntryEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.dto.ItakuMemoIkkatsuEntry = new T_ITAKU_MEMO_IKKATSU_ENTRY();

                //覚書一括設定
                if (!string.IsNullOrWhiteSpace(this.form.txtSystemId.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.SYSTEM_ID = Int64.Parse(this.form.txtSystemId.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtSeq.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.SEQ = Int32.Parse(this.form.txtSeq.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtDenpyouNumber.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.DENPYOU_NUMBER = Int64.Parse(this.form.txtDenpyouNumber.Text.Trim());
                }
                if (!string.IsNullOrWhiteSpace(this.form.TIME_STAMP.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP.Text);
                }

                this.dto.ItakuMemoIkkatsuEntry.MEMO_UPDATE_DATE = string.IsNullOrEmpty(this.form.dtpMemoUpdateDate.Text) ? SqlDateTime.Null : (DateTime)this.form.dtpMemoUpdateDate.Value; //(DateTime)this.form.dtpMemoUpdateDate.Value;
                this.dto.ItakuMemoIkkatsuEntry.MEMO = this.form.txtMemo.Text;
                this.dto.ItakuMemoIkkatsuEntry.HST_GYOUSHA_CD = this.form.txtHaishutsuJigyoushaCd.Text;
                this.dto.ItakuMemoIkkatsuEntry.HST_GYOUSHA_NAME = this.form.txtHaishutsuJigyoushaNm.Text;

                this.dto.ItakuMemoIkkatsuEntry.HST_GENBA_CD = this.form.txtGenbaCd.Text;
                this.dto.ItakuMemoIkkatsuEntry.HST_GENBA_NAME = this.form.txtGenbaNm.Text;

                this.dto.ItakuMemoIkkatsuEntry.UNPAN_GYOUSHA_CD = this.form.txtUnbanGyoushaCd.Text;
                this.dto.ItakuMemoIkkatsuEntry.UNPAN_GYOUSHA_NAME = this.form.txtUnbanGyoushaNm.Text;

                this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm.Text;
                if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq.Text);
                }

                this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm.Text;
                if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSeq.Text);
                }
                if (this.form.dtpKeiyakuBegin.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuBegin.Value.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_BEGIN = this.form.dtpKeiyakuBegin.Value.ToString();
                }
                if (this.form.dtpKeiyakuBeginTo.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuBeginTo.Value.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_BEGIN_TO = this.form.dtpKeiyakuBeginTo.Value.ToString();
                }
                if (this.form.dtpKeiyakuEnd.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuEnd.Value.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_END = this.form.dtpKeiyakuEnd.Value.ToString();
                }
                if (this.form.dtpKeiyakuEndTo.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuEndTo.Value.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_END_TO = this.form.dtpKeiyakuEndTo.Value.ToString();
                }
                if (!string.IsNullOrWhiteSpace(this.form.UPDATE_SHUBETSU.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPDATE_SHUBETSU = Int16.Parse(this.form.UPDATE_SHUBETSU.Text);
                }
                if (!string.IsNullOrWhiteSpace(this.form.KEIYAKUSHO_SHURUI.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.KEIYAKUSHO_SHURUI = Int16.Parse(this.form.KEIYAKUSHO_SHURUI.Text);
                }
                this.dto.ItakuMemoIkkatsuEntry.SHOBUN_UPDATE_KBN = Int16.Parse(this.form.txtShobun.Text);
                if ("1".Equals(this.form.txtShobun.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text;
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text))
                    {
                        this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId2.Text);
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text))
                    {
                        this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq2.Text);
                    }
                }
                this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_UPDATE_KBN = Int16.Parse(this.form.txtLastShobun.Text);
                if ("1".Equals(this.form.txtLastShobun.Text))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text;
                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text))
                    {
                        this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text);
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text))
                    {
                        this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSeq2.Text);
                    }
                }
                this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = false;

                // 更新者情報設定
                var dataBinderLogicItakuMemoIkkatsuEntry = new DataBinderLogic<r_framework.Entity.T_ITAKU_MEMO_IKKATSU_ENTRY>(this.dto.ItakuMemoIkkatsuEntry);
                dataBinderLogicItakuMemoIkkatsuEntry.SetSystemProperty(this.dto.ItakuMemoIkkatsuEntry, false);
                // 修正モードの場合、入力Entityの作成情報を設定
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                {
                    // 作成者
                    this.dto.ItakuMemoIkkatsuEntry.CREATE_USER = this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["CREATE_USER"].ToString();
                    // 作成日
                    this.dto.ItakuMemoIkkatsuEntry.CREATE_DATE = DateTime.Parse(this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["CREATE_DATE"].ToString());
                    // 作成PC
                    this.dto.ItakuMemoIkkatsuEntry.CREATE_PC = this.SearchItakuMemoIkkatsuEntryResult.Rows[0]["CREATE_PC"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuMemoIkkatsuEntryEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void CreateItakuMemoIkkatsuDetailEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 一覧設定(委託契約基本情報)
                List<T_ITAKU_MEMO_IKKATSU_DETAIL> itakuMemoIkkatsuDetailList = new List<T_ITAKU_MEMO_IKKATSU_DETAIL>();
                for (int j = 0; j < this.form.grdIchiran.RowCount; j++)
                {
                    Row row = this.form.grdIchiran.Rows[j];
                    T_ITAKU_MEMO_IKKATSU_DETAIL temp = new T_ITAKU_MEMO_IKKATSU_DETAIL();
                    if (!string.IsNullOrWhiteSpace(this.form.txtSystemId.Text))
                    {
                        temp.SYSTEM_ID = Int64.Parse(this.form.txtSystemId.Text);
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtSeq.Text))
                    {
                        temp.SEQ = Int32.Parse(this.form.txtSeq.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(this.form.txtDenpyouNumber.Text))
                    {
                        temp.DENPYOU_NUMBER = Int32.Parse(this.form.txtDenpyouNumber.Text);
                    }
                    temp.ROW_NO = j + 1;
                    if (row["SHORI_KBN"].Value != null)
                    {
                        temp.SHORI_KBN = (bool)row["SHORI_KBN"].Value;
                    }
                    else
                    {
                        temp.SHORI_KBN = false;
                    }
                    temp.ITAKU_KEIYAKU_SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                    temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                    temp.GYOUSHA_CD = row["GYOUSHA_CD"].Value.ToString();
                    temp.GYOUSHA_NAME = row["GYOUSHA_NAME"].Value.ToString();
                    temp.GYOUSHA_ADDRESS = row["GYOUSHA_ADDRESS"].Value.ToString();
                    temp.GENBA_CD = row["GENBA_CD"].Value.ToString();
                    temp.GENBA_NAME = row["GENBA_NAME"].Value.ToString();
                    temp.GENBA_ADDRESS = row["GENBA_ADDRESS"].Value.ToString();
                    if (row["KOUSHIN_SHUBETSU"].Value != null && !string.IsNullOrEmpty(row["KOUSHIN_SHUBETSU"].Value.ToString()))
                    {
                        temp.KOUSHIN_SHUBETSU = Int16.Parse(row["KOUSHIN_SHUBETSU"].Value.ToString());
                    }
                    if (row["ITAKU_KEIYAKU_SHURUI"].Value != null && !string.IsNullOrEmpty(row["ITAKU_KEIYAKU_SHURUI"].Value.ToString()))
                    {
                        temp.ITAKU_KEIYAKU_SHURUI = Int16.Parse(row["ITAKU_KEIYAKU_SHURUI"].Value.ToString());
                    }
                    if (row["ITAKU_KEIYAKU_DATE_BEGIN"].Value != null && !string.IsNullOrEmpty(row["ITAKU_KEIYAKU_DATE_BEGIN"].Value.ToString()))
                    {
                        temp.ITAKU_KEIYAKU_DATE_BEGIN = DateTime.Parse(row["ITAKU_KEIYAKU_DATE_BEGIN"].Value.ToString());
                    }
                    if (row["ITAKU_KEIYAKU_DATE_END"].Value != null && !string.IsNullOrEmpty(row["ITAKU_KEIYAKU_DATE_END"].Value.ToString()))
                    {
                        temp.ITAKU_KEIYAKU_DATE_END = DateTime.Parse(row["ITAKU_KEIYAKU_DATE_END"].Value.ToString());
                    }
                    // TIME_STAMP
                    if (row["TIME_STAMP"].Value != null && !string.IsNullOrEmpty(row["TIME_STAMP"].Value.ToString()))
                    {
                        temp.TIME_STAMP = (byte[])row["TIME_STAMP"].Value;//ConvertStrByte.StringToByte(row["TIME_STAMP"].Value);//(byte[])row["TIME_STAMP"].Value;
                    }
                    //中間処分場所の更新取得

                    if (this.form.txtShobun.Text.Equals("1") && temp.SHORI_KBN)
                    {
                        temp.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text;
                        if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text))
                        {
                            temp.SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId2.Text);
                        }
                        if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text))
                        {
                            temp.SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq2.Text);
                        }
                    }
                    else
                    {
                        temp.SHOBUN_PATTERN_NAME = row["SHOBUN_PATTERN_NAME"].Value.ToString();
                        if (row["SHOBUN_PATTERN_SYSYTEM_ID"].Value != null && !string.IsNullOrEmpty(row["SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString()))
                        {
                            temp.SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(row["SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString());
                        }
                        if (row["SHOBUN_PATTERN_SEQ"].Value != null && !string.IsNullOrEmpty(row["SHOBUN_PATTERN_SEQ"].Value.ToString()))
                        {
                            temp.SHOBUN_PATTERN_SEQ = Int32.Parse(row["SHOBUN_PATTERN_SEQ"].Value.ToString());
                        }
                    }
                    //最終処分場所の更新取得
                    if (this.form.txtLastShobun.Text.Equals("1") && temp.SHORI_KBN)
                    {
                        temp.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text;

                        if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text))
                        {
                            temp.LAST_SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text);
                        }
                        if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text))
                        {
                            temp.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSeq2.Text);
                        }
                    }
                    else
                    {
                        temp.LAST_SHOBUN_PATTERN_NAME = row["LAST_SHOBUN_PATTERN_NAME"].Value.ToString();

                        if (row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value != null && !string.IsNullOrEmpty(row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString()))
                        {
                            temp.LAST_SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString());
                        }
                        if (row["LAST_SHOBUN_PATTERN_SEQ"].Value != null && !string.IsNullOrEmpty(row["LAST_SHOBUN_PATTERN_SEQ"].Value.ToString()))
                        {
                            temp.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(row["LAST_SHOBUN_PATTERN_SEQ"].Value.ToString());
                        }
                    }

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.T_ITAKU_MEMO_IKKATSU_DETAIL>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    itakuMemoIkkatsuDetailList.Add(temp);
                }
                this.dto.ItakuMemoIkkatsuDetailArry = new T_ITAKU_MEMO_IKKATSU_DETAIL[itakuMemoIkkatsuDetailList.Count];
                this.dto.ItakuMemoIkkatsuDetailArry = itakuMemoIkkatsuDetailList.ToArray<T_ITAKU_MEMO_IKKATSU_DETAIL>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuMemoIkkatsuDetailEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void CreateItakuKeiyakuKihonEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 一覧設定(委託契約基本情報)
                List<M_ITAKU_KEIYAKU_KIHON> itakuKeiyakuKihon = new List<M_ITAKU_KEIYAKU_KIHON>();
                // idx = 0;
                for (int j = 0; j < this.form.grdIchiran.RowCount; j++)
                {
                    Row row = this.form.grdIchiran.Rows[j];

                    if (row["SHORI_KBN"].Value == null || !(bool)row["SHORI_KBN"].Value)
                    {
                        continue;
                    }
                    if (!this.form.txtShobun.Text.Equals("1") && !this.form.txtLastShobun.Text.Equals("1"))
                    {
                        continue;
                    }
                    M_ITAKU_KEIYAKU_KIHON temp = new M_ITAKU_KEIYAKU_KIHON();
                    temp.SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                    temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                    temp = this.kihonDao.GetDataBySystemId(temp);

                    //中間処分場所の更新取得
                    if (this.form.txtShobun.Text.Equals("1"))
                    {
                        temp.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text;
                        if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text))
                        {
                            temp.SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId2.Text);
                        }
                        if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text))
                        {
                            temp.SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq2.Text);
                        }
                    }
                    else
                    {
                        temp.SHOBUN_PATTERN_NAME = row["SHOBUN_PATTERN_NAME"].Value.ToString();
                        if (row["SHOBUN_PATTERN_SYSYTEM_ID"].Value != null && !string.IsNullOrEmpty(row["SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString()))
                        {
                            temp.SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(row["SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString());
                        }
                        if (row["SHOBUN_PATTERN_SEQ"].Value != null && !string.IsNullOrEmpty(row["SHOBUN_PATTERN_SEQ"].Value.ToString()))
                        {
                            temp.SHOBUN_PATTERN_SEQ = Int32.Parse(row["SHOBUN_PATTERN_SEQ"].Value.ToString());
                        }
                    }
                    //最終処分場所の更新取得
                    if (this.form.txtLastShobun.Text.Equals("1"))
                    {
                        temp.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text;

                        if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text))
                        {
                            temp.LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text);
                        }
                        if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text))
                        {
                            temp.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSeq2.Text);
                        }
                    }
                    else
                    {
                        temp.LAST_SHOBUN_PATTERN_NAME = row["LAST_SHOBUN_PATTERN_NAME"].Value.ToString();

                        if (row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value != null && !string.IsNullOrEmpty(row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString()))
                        {
                            temp.LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(row["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value.ToString());
                        }
                        if (row["LAST_SHOBUN_PATTERN_SEQ"].Value != null && !string.IsNullOrEmpty(row["LAST_SHOBUN_PATTERN_SEQ"].Value.ToString()))
                        {
                            temp.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(row["LAST_SHOBUN_PATTERN_SEQ"].Value.ToString());
                        }
                    }

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_KIHON>(temp);
                    dbLogic.SetSystemProperty(temp, false);
                    itakuKeiyakuKihon.Add(temp);
                }
                this.dto.ItakuKeiyakuKihon = new M_ITAKU_KEIYAKU_KIHON[itakuKeiyakuKihon.Count];
                this.dto.ItakuKeiyakuKihon = itakuKeiyakuKihon.ToArray<M_ITAKU_KEIYAKU_KIHON>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuKeiyakuKihonEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void CreateItakuOboeEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 一覧設定(委託契約覚書)
                List<M_ITAKU_KEIYAKU_OBOE> itakuOboe = new List<M_ITAKU_KEIYAKU_OBOE>();

                //ItakuKeiyakuOboeDao oboe = DaoInitUtility.GetComponent<ItakuKeiyakuOboeDao>();
                Int32 intSeq = 0;

                for (int j = 0; j < this.form.grdIchiran.RowCount; j++)
                {
                    Row row = this.form.grdIchiran.Rows[j];
                    if (row["SHORI_KBN"].Value == null || !(bool)row["SHORI_KBN"].Value)
                    {
                        continue;
                    }
                    M_ITAKU_KEIYAKU_OBOE temp = new M_ITAKU_KEIYAKU_OBOE();
                    DataTable temp1; ;
                    temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                    temp.SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                    temp1 = this.mOboeDao.GetMaxSeq(temp);

                    if (temp1 != null && temp1.Rows.Count == 1 && !string.IsNullOrEmpty(temp1.Rows[0][0].ToString()))
                    {
                        intSeq = Int32.Parse(temp1.Rows[0][0].ToString()) + 1;
                    }
                    else
                    {
                        intSeq = 1;
                    }
                    temp.MEMO = this.form.txtMemo.Text;
                    temp.SEQ = intSeq;

                    // 更新者情報設定
                    var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_OBOE>(temp);
                    dbLogic.SetSystemProperty(temp, false);

                    // dbLogic.SetSystemPropertyの後に設定しないとシステム日付で更新される
                    temp.UPDATE_DATE = string.IsNullOrEmpty(this.form.dtpMemoUpdateDate.Text) ? SqlDateTime.Null : (DateTime)this.form.dtpMemoUpdateDate.Value;

                    itakuOboe.Add(temp);
                }
                this.dto.ItakuKeiyakuOboe = new M_ITAKU_KEIYAKU_OBOE[itakuOboe.Count];
                this.dto.ItakuKeiyakuOboe = itakuOboe.ToArray<M_ITAKU_KEIYAKU_OBOE>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuKeiyakuKihonEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 委託契約別表3_処分テーブルを更新します
        /// </summary>
        public void CreateItakuBetsu3Entity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 最終処理区分の更新が２．しないの場合
                if (string.IsNullOrEmpty(this.form.txtShobun.Text) || this.form.txtShobun.Text == "2"
                    || string.IsNullOrEmpty(this.form.txtShobunPatternSysId2.Text))
                {
                    return;
                }

                List<M_ITAKU_KEIYAKU_BETSU3> itakuBetsu3 = new List<M_ITAKU_KEIYAKU_BETSU3>();
                Int32 intSeq = 1;

                // パターンデータを取得する
                M_SBNB_PATTERN data = new M_SBNB_PATTERN();
                data.SYSTEM_ID = Convert.ToInt64(this.form.txtShobunPatternSysId2.Text.Trim());
                DataTable pattern = this.mEntryDao.GetPatternName(data);
                if (pattern == null || pattern.Rows.Count <= 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "処分場パターン");
                    this.form.txtShobunPatternNm2.Text = string.Empty;
                    this.form.txtShobunPatternSysId2.Text = string.Empty;
                    this.form.txtShobunPatternSeq2.Text = string.Empty;
                    this.form.txtShobunPatternNm2.Focus();
                    return;
                }

                foreach (Row row in this.form.grdIchiran.Rows.Where(r => r["SHORI_KBN"].Value != null && (bool)r["SHORI_KBN"].Value))
                {
                    // SEQ初期化
                    intSeq = 1;
                    foreach (DataRow r in pattern.Rows)
                    {
                        M_ITAKU_KEIYAKU_BETSU3 temp = new M_ITAKU_KEIYAKU_BETSU3();
                        temp.SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                        temp.SEQ = intSeq++;
                        temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                        temp.SHOBUN_GYOUSHA_CD = r["GYOUSHA_CD"].ToString();
                        temp.SHOBUN_GYOUSHA_NAME = r["GYOUSHA_NAME"].ToString();
                        temp.SHOBUN_GYOUSHA_ADDRESS = r["GYOUSHA_ADDRESS"].ToString();
                        temp.SHOBUN_GYOUSHA_ADDRESS1 = r["GYOUSHA_ADDRESS1"].ToString();
                        temp.SHOBUN_GYOUSHA_ADDRESS2 = r["GYOUSHA_ADDRESS2"].ToString();
                        temp.SHOBUN_JIGYOUJOU_CD = r["GENBA_CD"].ToString();
                        temp.SHOBUN_JIGYOUJOU_NAME = r["GENBA_NAME"].ToString();
                        temp.SHOBUN_JIGYOUJOU_ADDRESS = r["GENBA_ADDRESS"].ToString();
                        temp.SHOBUN_JIGYOUJOU_ADDRESS1 = r["GENBA_ADDRESS1"].ToString();
                        temp.SHOBUN_JIGYOUJOU_ADDRESS2 = r["GENBA_ADDRESS2"].ToString();
                        temp.SHOBUN_HOUHOU_CD = r["SHOBUN_HOUHOU_CD"].ToString();
                        if (r["HOKAN_JOGEN"] != DBNull.Value)
                        {
                            temp.HOKAN_JOGEN = Convert.ToDecimal(r["HOKAN_JOGEN"]);
                        }
                        if (r["HOKAN_JOGEN_UNIT_CD"] != DBNull.Value)
                        {
                            temp.HOKAN_JOGEN_UNIT_CD = Convert.ToInt16(r["HOKAN_JOGEN_UNIT_CD"].ToString());
                        }
                        temp.SHISETSU_CAPACITY = r["SHORI_SPEC"].ToString();
                        if (r["UNPAN_FROM"] != DBNull.Value)
                        {
                            temp.UNPAN_FROM = Convert.ToInt16(r["UNPAN_FROM"].ToString());
                        }
                        if (r["UNPAN_END"] != DBNull.Value)
                        {
                            temp.UNPAN_END = Convert.ToInt16(r["UNPAN_END"].ToString());
                        }
                        if (r["KONGOU"] != DBNull.Value)
                        {
                            temp.KONGOU = Convert.ToInt16(r["KONGOU"].ToString());
                        }
                        if (r["SHUSENBETU"] != DBNull.Value)
                        {
                            temp.SHUSENBETU = Convert.ToInt16(r["SHUSENBETU"].ToString());
                        }

                        // 更新者情報設定
                        var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU3>(temp);
                        dbLogic.SetSystemProperty(temp, false);

                        itakuBetsu3.Add(temp);
                    }
                }
                this.dto.ItakuKeiyakuBetsu3 = new M_ITAKU_KEIYAKU_BETSU3[itakuBetsu3.Count];
                this.dto.ItakuKeiyakuBetsu3 = itakuBetsu3.ToArray<M_ITAKU_KEIYAKU_BETSU3>();

            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuBetsu3Entity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 委託契約別表4_処分テーブルを更新します
        /// </summary>
        public void CreateItakuBetsu4Entity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 最終処理区分の更新が２．しないの場合
                if (string.IsNullOrEmpty(this.form.txtLastShobun.Text) || this.form.txtLastShobun.Text == "2"
                    || string.IsNullOrEmpty(this.form.txtLastShobunPatternSysId2.Text))
                {
                    return;
                }

                List<M_ITAKU_KEIYAKU_BETSU4> itakuBetsu4 = new List<M_ITAKU_KEIYAKU_BETSU4>();
                Int32 intSeq = 1;

                // パターンデータを取得する
                M_SBNB_PATTERN data = new M_SBNB_PATTERN();
                data.SYSTEM_ID = Convert.ToInt64(this.form.txtLastShobunPatternSysId2.Text.Trim());
                DataTable pattern = this.mEntryDao.GetPatternName(data);
                if (pattern == null || pattern.Rows.Count <= 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "処分場パターン");
                    this.form.txtShobunPatternNm2.Text = string.Empty;
                    this.form.txtShobunPatternSysId2.Text = string.Empty;
                    this.form.txtShobunPatternSeq2.Text = string.Empty;
                    this.form.txtShobunPatternNm2.Focus();
                    return;
                }

                foreach (Row row in this.form.grdIchiran.Rows.Where(r => r["SHORI_KBN"].Value != null && (bool)r["SHORI_KBN"].Value))
                {
                    // SEQ初期化
                    intSeq = 1;
                    foreach (DataRow r in pattern.Rows)
                    {
                        M_ITAKU_KEIYAKU_BETSU4 temp = new M_ITAKU_KEIYAKU_BETSU4();
                        temp.SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                        temp.SEQ = intSeq++;
                        temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                        temp.LAST_SHOBUN_GYOUSHA_CD = r["GYOUSHA_CD"].ToString();
                        temp.LAST_SHOBUN_GYOUSHA_NAME = r["GYOUSHA_NAME"].ToString();
                        temp.LAST_SHOBUN_GYOUSHA_ADDRESS = r["GYOUSHA_ADDRESS"].ToString();
                        temp.LAST_SHOBUN_GYOUSHA_ADDRESS1 = r["GYOUSHA_ADDRESS1"].ToString();
                        temp.LAST_SHOBUN_GYOUSHA_ADDRESS2 = r["GYOUSHA_ADDRESS2"].ToString();
                        temp.LAST_SHOBUN_JIGYOUJOU_CD = r["GENBA_CD"].ToString();
                        temp.LAST_SHOBUN_JIGYOUJOU_NAME = r["GENBA_NAME"].ToString();
                        temp.LAST_SHOBUN_JIGYOUJOU_ADDRESS = r["GENBA_ADDRESS"].ToString();
                        temp.LAST_SHOBUN_JIGYOUJOU_ADDRESS1 = r["GENBA_ADDRESS1"].ToString();
                        temp.LAST_SHOBUN_JIGYOUJOU_ADDRESS2 = r["GENBA_ADDRESS2"].ToString();
                        temp.SHOBUN_HOUHOU_CD = r["SHOBUN_HOUHOU_CD"].ToString();
                        temp.SHORI_SPEC = r["SHORI_SPEC"].ToString();
                        temp.OTHER = r["OTHER"].ToString();
                        if (r["BUNRUI"] != DBNull.Value)
                        {
                            temp.BUNRUI = Convert.ToInt16(r["BUNRUI"].ToString());
                        }
                        if (r["END_KUBUN"] != DBNull.Value)
                        {
                            temp.END_KUBUN = Convert.ToInt16(r["END_KUBUN"].ToString());
                        }
                        temp.HOUKOKUSHO_BUNRUI_CD = pattern.Rows[0]["HOUKOKUSHO_BUNRUI_CD"].ToString();
                        temp.HOUKOKUSHO_BUNRUI_NAME = pattern.Rows[0]["HOUKOKUSHO_BUNRUI_NAME"].ToString();
                        temp.SHOBUNSAKI_NO = pattern.Rows[0]["SHOBUNSAKI_NO"].ToString();

                        // 更新者情報設定
                        var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_BETSU4>(temp);
                        dbLogic.SetSystemProperty(temp, false);
                        itakuBetsu4.Add(temp);
                    }
                }
                this.dto.ItakuKeiyakuBetsu4 = new M_ITAKU_KEIYAKU_BETSU4[itakuBetsu4.Count];
                this.dto.ItakuKeiyakuBetsu4 = itakuBetsu4.ToArray<M_ITAKU_KEIYAKU_BETSU4>();

            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateItakuBetsu4Entity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="WindowType"></param>
        public bool CreateEntity(WINDOW_TYPE WindowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(WindowType);
                // 新規の場合のみ、システムIDを採番する
                // 登録データ作成前に行う必要あり
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == WindowType)
                {
                    // 新規採番
                    this.form.txtSystemId.Text = SaibanSystemId().ToString();
                    this.form.txtDenpyouNumber.Text = SaibanOboegaku().ToString();
                    this.form.txtSeq.Text = "1";
                    this.mDenpyouNumber = this.form.txtDenpyouNumber.Text;
                }
                CreateItakuMemoIkkatsuEntryEntity();
                CreateItakuMemoIkkatsuDetailEntity();
                CreateItakuKeiyakuKihonEntity();
                CreateItakuOboeEntity();
                CreateItakuBetsu3Entity();
                CreateItakuBetsu4Entity();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntity", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion Entityを作成する

        #region 登録/更新/削除

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                this.isRegist = false;
                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    using (Transaction tran = new Transaction())
                    {
                        this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = true;
                        this.mEntryDao.Update(this.dto.ItakuMemoIkkatsuEntry);

                        // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します start
                        // 覚書一括登録
                        this.dto.ItakuMemoIkkatsuEntry.SEQ = this.dto.ItakuMemoIkkatsuEntry.SEQ + 1;
                        this.mEntryDao.Insert(this.dto.ItakuMemoIkkatsuEntry);

                        // 覚書一括詳細登録
                        foreach (T_ITAKU_MEMO_IKKATSU_DETAIL data in this.dto.ItakuMemoIkkatsuDetailArry)
                        {
                            data.SEQ = this.dto.ItakuMemoIkkatsuEntry.SEQ;
                            this.mDetailDao.Insert(data);
                        }
                        // 20141204 ブン CREATE_XX、UPDATE_XX、DELETE_FLGを設定します end

                        tran.Commit();
                        // msgLogic.MessageBoxShow("I001", "削除");
                    }
                    this.isRegist = true;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                if (errorFlag)
                {
                    return;
                }
                this.isRegist = false;
                using (Transaction tran = new Transaction())
                {
                    //覚書一括登録
                    this.mEntryDao.Insert(this.dto.ItakuMemoIkkatsuEntry);

                    //覚書一括詳細登録
                    foreach (T_ITAKU_MEMO_IKKATSU_DETAIL data in this.dto.ItakuMemoIkkatsuDetailArry)
                    {
                        this.mDetailDao.Insert(data);
                    }
                    // 委託契約基本マスタ更新
                    foreach (M_ITAKU_KEIYAKU_KIHON data in this.dto.ItakuKeiyakuKihon)
                    {
                        this.kihonDao.Update(data);
                    }

                    // 委託契約覚書マスタ更新
                    for (int i = 0; i < this.dto.ItakuKeiyakuOboe.Length; i++)
                    {
                        this.mOboeDao.Insert(this.dto.ItakuKeiyakuOboe[i]);
                    }

                    // 委託契約別表3_処分マスタ更新
                    if (this.dto.ItakuKeiyakuBetsu3 != null)
                    {
                        this.dto.ItakuKeiyakuBetsu3.ToList().ForEach(f => this.mBetsu3Dao.Delete(f));
                        for (int i = 0; i < this.dto.ItakuKeiyakuBetsu3.Length; i++)
                        {
                            this.mBetsu3Dao.Insert(this.dto.ItakuKeiyakuBetsu3[i]);
                        }
                    }

                    // 委託契約別表4_処分マスタ更新
                    if (this.dto.ItakuKeiyakuBetsu4 != null)
                    {
                        this.dto.ItakuKeiyakuBetsu4.ToList().ForEach(f => this.mBetsu4Dao.Delete(f));
                        for (int i = 0; i < this.dto.ItakuKeiyakuBetsu4.Length; i++)
                        {
                            this.mBetsu4Dao.Insert(this.dto.ItakuKeiyakuBetsu4[i]);
                        }
                    }
                    tran.Commit();
                }
                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                if (errorFlag)
                {
                    return;
                }
                this.isRegist = false;
                using (Transaction tran = new Transaction())
                {
                    //覚書一括登録
                    this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = true;
                    this.mEntryDao.Update(this.dto.ItakuMemoIkkatsuEntry);
                    this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = false;
                    this.dto.ItakuMemoIkkatsuEntry.SEQ = this.dto.ItakuMemoIkkatsuEntry.SEQ + 1;
                    this.mEntryDao.Insert(this.dto.ItakuMemoIkkatsuEntry);

                    //覚書一括詳細登録
                    foreach (T_ITAKU_MEMO_IKKATSU_DETAIL data in this.dto.ItakuMemoIkkatsuDetailArry)
                    {
                        data.SEQ = this.dto.ItakuMemoIkkatsuEntry.SEQ;
                        this.mDetailDao.Insert(data);
                    }

                    // 委託契約基本マスタ更新
                    foreach (M_ITAKU_KEIYAKU_KIHON data in this.dto.ItakuKeiyakuKihon)
                    {
                        this.kihonDao.Update(data);
                    }

                    // 委託契約覚書マスタ更新
                    for (int i = 0; i < this.dto.ItakuKeiyakuOboe.Length; i++)
                    {
                        this.mOboeDao.Insert(this.dto.ItakuKeiyakuOboe[i]);
                    }

                    // 委託契約別表3_処分マスタ更新
                    if (this.dto.ItakuKeiyakuBetsu3 != null)
                    {
                        this.dto.ItakuKeiyakuBetsu3.ToList().ForEach(f => this.mBetsu3Dao.Delete(f));
                        for (int i = 0; i < this.dto.ItakuKeiyakuBetsu3.Length; i++)
                        {
                            this.mBetsu3Dao.Insert(this.dto.ItakuKeiyakuBetsu3[i]);
                        }
                    }

                    // 委託契約別表4_処分マスタ更新
                    if (this.dto.ItakuKeiyakuBetsu4 != null)
                    {
                        this.dto.ItakuKeiyakuBetsu4.ToList().ForEach(f => this.mBetsu4Dao.Delete(f));
                        for (int i = 0; i < this.dto.ItakuKeiyakuBetsu4.Length; i++)
                        {
                            this.mBetsu4Dao.Insert(this.dto.ItakuKeiyakuBetsu4[i]);
                        }
                    }

                    tran.Commit();
                }
                this.isRegist = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion 登録/更新/削除

        #region 業者と現場と運搬業者ロックフォーカス後処理

        /// <summary>
        /// 業者ロックフォーカス後処理
        /// </summary>
        /// <param name="strValue"></param>
        public bool GetHaishutsuGyoushaNm(string strValue)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(strValue);
                //削除モード時処理しない。
                if (this.form.txtHaishutsuJigyoushaCd.ReadOnly)
                {
                    return ret;
                }
                //空時は処理
                if (string.IsNullOrEmpty(strValue))
                {
                    this.form.txtHaishutsuJigyoushaNm.Text = string.Empty;
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaNm.Text = string.Empty;
                    return ret;
                }
                //値ある時は処理
                var messageShowLogic = new MessageBoxShowLogic();
                string Name = string.Empty;
                string strSql = " select M_GYOUSHA.GYOUSHA_CD AS CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU AS NAME FROM M_GYOUSHA  WHERE   GYOUSHA_CD = '" + strValue + "' and HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1 AND  M_GYOUSHA.DELETE_FLG = 0 ";
                var dt = this.gyousyaDao.GetDateForStringSql(strSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.form.txtHaishutsuJigyoushaNm.Text = dt.Rows[0]["NAME"].ToString();
                }
                else
                {
                    this.form.txtHaishutsuJigyoushaCd.IsInputErrorOccured = true;
                    this.form.txtHaishutsuJigyoushaNm.Text = string.Empty;
                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.form.txtHaishutsuJigyoushaCd.Focus();
                }

                if (this.form.befHaishutsuJigyoushaCd != this.form.txtHaishutsuJigyoushaCd.Text)
                {
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaNm.Text = string.Empty;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetHaishutsuGyoushaNm", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHaishutsuGyoushaNm", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 現場ロックフォーカス後処理
        /// </summary>
        /// <param name="gyosyaObj"></param>
        /// <param name="genbaObj"></param>
        /// <returns></returns>
        public int GetHaishutsuGenbaNm(CustomAlphaNumTextBox gyosyaObj, CustomAlphaNumTextBox genbaObj)
        {
            try
            {
                LogUtility.DebugMethodStart(gyosyaObj, genbaObj);

                int result = 0;
                var messageShowLogic = new MessageBoxShowLogic();

                string gyoushaCd = gyosyaObj.Text.Trim();
                string genbrCd = genbaObj.Text.Trim();
                this.form.txtGenbaNm.Text = "";
                if (string.IsNullOrEmpty(genbrCd))
                {
                    return result;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    messageShowLogic.MessageBoxShow("E051", "排出事業者");
                    genbaObj.Text = string.Empty;
                    genbaObj.Focus();
                    return result;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                string strSql = " SELECT ";
                strSql = strSql + " M_GYOUSHA.GYOUSHA_CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU,M_GENBA.GENBA_CD,M_GENBA.GENBA_NAME_RYAKU ";
                strSql = strSql + " FROM M_GYOUSHA INNER JOIN M_GENBA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD  ";
                strSql = strSql + " WHERE M_GYOUSHA.DELETE_FLG = 0 AND M_GENBA.DELETE_FLG = 0  AND  M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 1 ";
                if (!string.IsNullOrEmpty(gyoushaCd))
                {
                    strSql = strSql + " AND M_GYOUSHA.GYOUSHA_CD = '" + gyoushaCd + "'";
                }
                strSql = strSql + " AND M_GENBA.GENBA_CD = '" + genbrCd + "'";
                var dt = this.genbaDao.GetDateForStringSql(strSql);

                int cnt = dt.Rows.Count;
                var dtRow = cnt == 1 ? dt.Rows[0] : null;
                var kv = new KeyValuePair<int, DataRow>(cnt, dtRow);
                switch (kv.Key)
                {
                    case 0:
                        this.form.txtGenbaNm.Text = "";
                        this.form.txtGenbaCd.IsInputErrorOccured = true;
                        messageShowLogic.MessageBoxShow("E020", "現場");
                        this.form.txtGenbaCd.Focus();
                        break;

                    case 1:
                        var dr = kv.Value;
                        this.form.txtHaishutsuJigyoushaCd.Text = dr.Field<string>("GYOUSHA_CD");
                        this.form.txtHaishutsuJigyoushaNm.Text = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                        this.form.txtGenbaCd.Text = dr.Field<string>("GENBA_CD");
                        this.form.txtGenbaNm.Text = dr.Field<string>("GENBA_NAME_RYAKU");
                        break;

                    default:
                        result = kv.Key;
                        // SendKeys.Send(" ");
                        break;
                }
                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GetHaishutsuGenbaNm", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHaishutsuGenbaNm", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者ロックフォーカス後処理
        /// </summary>
        /// <param name="strValue"></param>
        public bool GetUnpanJutakusha(string strValue)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(strValue);
                this.form.txtUnbanGyoushaNm.Text = string.Empty;
                if (string.IsNullOrEmpty(strValue))
                {
                    return ret;
                }
                var messageShowLogic = new MessageBoxShowLogic();

                //M_GYOUSHA gyousha = this.gyousyaDao..GetUnpanJutakusha(strValue);

                var gyousha = this.gyousyaDao.GetDataByCd(strValue);
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN)
                {
                    // 運搬業者名を設定
                    this.form.txtUnbanGyoushaNm.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.form.txtUnbanGyoushaCd.IsInputErrorOccured = true;
                    this.form.txtUnbanGyoushaNm.Text = string.Empty;
                    messageShowLogic.MessageBoxShow("E020", "業者");
                    this.form.txtUnbanGyoushaCd.Focus();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetUnpanJutakusha", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetUnpanJutakusha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }

        #endregion 業者と現場と運搬業者ロックフォーカス後処理

        #region 最終処分場所パターン一覧ポップアップ

        /// <summary>
        /// 最終処分場所パターン一覧ポップアップ
        /// </summary>
        /// <returns></returns>
        public string OpenPattenPopUp(DENSHU_KBN kbn, out bool catchErr)
        {
            String returnValue = null;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(kbn);
                // 起動
                var callHeader = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIHeader();
                var callForm = new Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP.UIForm(kbn);
                var baseForm = new BusinessBaseForm(callForm, callHeader);

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    baseForm.ShowDialog();
                }

                // 返却値
                returnValue = callForm.OutSelectedPatternName;
            }
            catch (Exception ex)
            {
                LogUtility.Error("OpenPattenPopUp", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnValue, catchErr);
            }
            return returnValue;
        }

        #endregion 最終処分場所パターン一覧ポップアップ

        #region 契約書種類名取得メソッド

        /// <summary>
        /// 契約書種類名取得
        /// </summary>
        /// <param name="shurui">契約書種類CD</param>
        /// <returns>契約書種類名</returns>
        internal string GetItakuKeiyakuShuruiName(int shurui)
        {
            string retName = string.Empty;

            switch (shurui)
            {
                case 1:
                    retName = "収・運契約";
                    break;

                case 2:
                    retName = "処分契約";
                    break;

                case 3:
                    retName = "収・運/処分契約";
                    break;

                default:
                    retName = string.Empty;
                    break;
            }

            return retName;
        }

        #endregion 契約書種類名取得メソッド

        #region ヘッダーチェック変更処理(全選択)

        /// <summary>
        /// ヘッダーチェックボックス変更処理
        /// </summary>
        public void ChangeHeaderCheckBox()
        {
            // ヘッダーチェックボックスの値取得
            bool check = (bool)this.form.grdIchiran.CurrentCell.EditedFormattedValue;

            foreach (Row temp in this.form.grdIchiran.Rows)
            {
                if (temp.IsNewRow)
                {
                    continue;
                }

                temp["SHORI_KBN"].Value = check;
            }
        }

        #endregion ヘッダーチェック変更処理(全選択)

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

        #endregion Equals/GetHashCode/ToString

        /// <summary>
        /// 伝票番号の一個前か一個次の番号を取得する
        /// </summary>
        /// <param name="isPrevious">前=True, 次=False</param>
        /// <param name="number">基準になる伝票番号</param>
        /// <returns></returns>
        internal string GetPreviousNextNumber(bool isPrevious, long number)
        {
            string ren = string.Empty;

            // 基準になる伝票番号が最大or最小値だった場合
            if (this.mEntryDao.GetPreviousNextNumber(!isPrevious, 0) == number)
            {
                // 反転した方向のMAXorMINを取得
                ren = this.mEntryDao.GetPreviousNextNumber(isPrevious, 0).ToString();
            }
            else
            {
                // 前or次の伝票番号を取得
                ren = this.mEntryDao.GetPreviousNextNumber(isPrevious, number).ToString();
            }

            // 取得できなかった場合はEmptyで返す
            return ren == "0" ? string.Empty : ren;
        }

        #region 契約開始日、終了日チェック
        /// <summary>
        /// 契約開始日、終了日チェック
        /// </summary>
        /// <returns>true:正常, false:異常</returns>
        internal bool CheckSerchKeiyakuDate()
        {
            this.form.dtpKeiyakuBegin.IsInputErrorOccured = false;
            this.form.dtpKeiyakuBeginTo.IsInputErrorOccured = false;
            this.form.dtpKeiyakuEnd.IsInputErrorOccured = false;
            this.form.dtpKeiyakuEndTo.IsInputErrorOccured = false;

            if (!string.IsNullOrEmpty(this.form.dtpKeiyakuBegin.GetResultText())
                && !string.IsNullOrEmpty(this.form.dtpKeiyakuBeginTo.GetResultText()))
            {
                // 契約開始日チェック
                DateTime dtpFrom = DateTime.Parse(this.form.dtpKeiyakuBegin.GetResultText()).Date;
                DateTime dtpTo = DateTime.Parse(this.form.dtpKeiyakuBeginTo.GetResultText()).Date;

                int diff = dtpFrom.CompareTo(dtpTo);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.dtpKeiyakuBegin.IsInputErrorOccured = true;
                    this.form.dtpKeiyakuBeginTo.IsInputErrorOccured = true;
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    string[] errorMsg = { "契約開始日From", "契約開始日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);

                    this.form.dtpKeiyakuBegin.Select();
                    this.form.dtpKeiyakuBegin.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(this.form.dtpKeiyakuEnd.GetResultText())
                && !string.IsNullOrEmpty(this.form.dtpKeiyakuEndTo.GetResultText()))
            {
                // 契約終了日チェック
                DateTime dtpFrom = DateTime.Parse(this.form.dtpKeiyakuEnd.GetResultText()).Date;
                DateTime dtpTo = DateTime.Parse(this.form.dtpKeiyakuEndTo.GetResultText()).Date;

                int diff = dtpFrom.CompareTo(dtpTo);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.form.dtpKeiyakuEnd.IsInputErrorOccured = true;
                    this.form.dtpKeiyakuEndTo.IsInputErrorOccured = true;
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    string[] errorMsg = { "契約終了日From", "契約終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);

                    this.form.dtpKeiyakuEnd.Select();
                    this.form.dtpKeiyakuEnd.Focus();
                    return false;
                }

            }

            return true;
        }
        #endregion

        #region パターン名の必須を切り替えます
        /// <summary>
        /// パターン名の必須を切り替えます
        /// </summary>
        /// <param name="shoriKbn">1=中間、2=最終</param>
        /// <param name="requiredFlg">True=必須、False=非必須</param>
        internal void PatteenRequiredChange(int shoriKbn, bool requiredFlg)
        {
            if (requiredFlg)
            {
                // 必須チェック設定
                SelectCheckDto existCheck = new SelectCheckDto();
                existCheck.CheckMethodName = "必須チェック";
                Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
                excitChecks.Add(existCheck);
                if (shoriKbn == 1)
                {
                    // 中間パターン
                    this.form.txtShobunPatternNm2.RegistCheckMethod = excitChecks;
                }
                else if (shoriKbn == 2)
                {
                    // 最終パターン
                    this.form.txtLastShobunPatternNm2.RegistCheckMethod = excitChecks;
                }
            }
            else
            {
                if (shoriKbn == 1)
                {
                    // 中間パターン
                    this.form.txtShobunPatternNm2.RegistCheckMethod = null;
                }
                else if (shoriKbn == 2)
                {
                    // 最終パターン
                    this.form.txtLastShobunPatternNm2.RegistCheckMethod = null;
                }
            }


        }

        #endregion パターン入力チェック
    }
}