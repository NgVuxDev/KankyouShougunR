using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using OboegakiIkkatuHoshu.APP;
using OboegakiIkkatuHoshu;
using System.Reflection;
using System.Data;
using System.Windows.Forms;
using OboegakiIkkatuHoshu.Dao;
using Seasar.Quill.Attrs;

using GrapeCity.Win.MultiRow;
using System.Data.SqlTypes;
using OboeGakiIkktuImputIchiran;
namespace OboegakiIkkatuHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class OboegakiIkkatuHoshuLogic : IBuisinessLogic
    {
        #region 
        /// <summary>
        /// DTO
        /// </summary>
        private OboegakiIkkatuHoshuDTO dto;
        /// <summary>
        /// Header
        /// </summary>
        private OboegakiIkkatuHoshuHeader header;      

        /// <summary>
        /// Form
        /// </summary>
        private OboegakiIkkatuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 委託契約基本のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao kihonDao;

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
        private IM_ITAKU_KEIYAKU_OBOEDao mOboeDao;

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
        /// 覚書一括 新規時のシステムID 
        /// </summary>
        public int newSystemId;
        /// <summary>
        /// 覚書一括 元システムID 
        /// </summary>
        public String oldSystemId;

        #endregion

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
        public OboegakiIkkatuHoshuDTO SearchString { get; set; }

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
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OboegakiIkkatuHoshuLogic(OboegakiIkkatuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            this.form = targetForm;          
            this.SearchString = new OboegakiIkkatuHoshuDTO();
            this.dto = new OboegakiIkkatuHoshuDTO();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mEntryDao = DaoInitUtility.GetComponent<ItakuMemoIkkatsuEntryDAO>();
            this.mDetailDao = DaoInitUtility.GetComponent<ItakuMemoIkkatsuDetailDAO>();
            this.kihonDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.mOboeDao = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_OBOEDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region  画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit(WINDOW_TYPE windowType)
        {
            LogUtility.DebugMethodStart(windowType);

            var parentForm = (BusinessBaseForm)this.form.Parent;
            // ヘッダー項目
            this.header = (OboegakiIkkatuHoshuHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;

            // ボタンのテキストを初期化
            this.ButtonInit(parentForm);

            // イベントの初期化
            this.EventInit(parentForm);

            // 処理モード別画面初期化
            this.ModeInit(windowType, parentForm);
            //システム情報を取得し、初期値をセットする
            this.HearerSysInfoInit();
           // this.allControl = this.form.allControl;

            LogUtility.DebugMethodEnd(windowType);

        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">画面種別</param>
        /// <param name="parentForm">親フォーム</param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNewMode(parentForm);
                    // 初期フォーカス設定
                    this.form.txtDenpyouNumber.Focus();
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.WindowInitUpdate(parentForm);
                    // 初期フォーカス設定
                    this.form.dtpMemoUpdateDate.Focus();
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    // 初期フォーカス設定
                    this.form.dtpMemoUpdateDate.Focus();
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNewMode(parentForm);
                    // 初期フォーカス設定
                    this.form.txtDenpyouNumber.Focus();
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNewMode(BusinessBaseForm parentForm)
        {
            // 全コントロール操作可能とする
            this.AllControlLock(false);

            // ヘッダー項目           
            this.header.CreateDate.Text = string.Empty;
            this.header.CreateUser.Text = string.Empty;
            this.header.LastUpdateDate.Text = string.Empty;
            this.header.LastUpdateUser.Text = string.Empty;
            this.header.ReadDataNumber.Text = "0";
            //this.header.alertNumber.Text = string.Empty;

            // 入力項目
            this.form.txtDenpyouNumber.Text = string.Empty;
            this.form.txtSeq.Text = string.Empty;
            this.form.txtSystemId.Text = string.Empty;

            this.form.dtpMemoUpdateDate.Value = DateTime.Today;
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
            this.form.dtpKeiyakuEnd.Value = null;
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

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func8.Enabled = true;    // 対象検索
            parentForm.bt_func9.Enabled = true;     // 登録          
            parentForm.bt_func12.Enabled = true;    // 閉じる

            // 処理モードを新規に設定
            this.header.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
            this.header.windowTypeLabel.Text = "新規";

            // 初期フォーカス設定
            this.form.txtDenpyouNumber.Focus();
        }
        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitUpdate(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            this.WindowInitNewMode(parentForm);
            // 全コントロール操作可能とする
            this.AllControlLock(false);

            this.Search(this.mDenpyouNumber);
             this.form.txtDenpyouNumber.Text=this.mDenpyouNumber ;
             this.SetEntry("修正");
             this.SetIchiran();

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;    // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;     // 対象検索
            parentForm.bt_func12.Enabled = true;    // 閉じる          

            //// 処理モードを修正に設定
            //this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            this.header.windowTypeLabel.BackColor = System.Drawing.Color.Orange;

            this.header.windowTypeLabel.Text = "修正";

            // 初期フォーカス設定
            this.form.dtpMemoUpdateDate.Focus();
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
            this.header.windowTypeLabel.BackColor = System.Drawing.Color.Orange;

            this.header.windowTypeLabel.Text = "修正";

            // 初期フォーカス設定
            this.form.txtDenpyouNumber.Focus();
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 検索結果を画面に設定
            this.WindowInitNewMode(parentForm);
            // 削除モード固有UI設定
            this.AllControlLock(true);

            this.form.txtDenpyouNumber.Text = this.mDenpyouNumber;
            this.Search(this.mDenpyouNumber);
            this.SetEntry("削除");
            this.SetIchiran();           

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func7.Enabled = true;     // 一覧
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func8.Enabled = false;   // 対象検索
            parentForm.bt_func12.Enabled = true;    // 閉じる

            //// 処理モードを削除に設定
            //this.form.SetWindowType(WINDOW_TYPE.DELETE_WINDOW_FLAG);

            this.header.windowTypeLabel.BackColor = System.Drawing.Color.Red;
             //初期フォーカス設定
           this.form.dtpMemoUpdateDate.Focus();
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
            this.form.txtShobun.ReadOnly = isBool;
            this.form.txtShobunPatternNm2.ReadOnly = isBool;
            this.form.txtShobunPatternSysId2.ReadOnly = !isBool;
            this.form.txtShobunPatternSeq2.ReadOnly = !isBool;
            this.form.txtLastShobun.ReadOnly = isBool;
            this.form.txtLastShobunPatternNm2.ReadOnly = isBool;
            this.form.txtLastShobunPatternSysId2.ReadOnly = isBool;
            this.form.txtLastShobunPatternSeq2.ReadOnly = isBool;

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
            this.form.dtpKeiyakuEnd.Enabled = !isBool;

            this.form.btnSearchHaishutsuGyousha.Enabled = !isBool;
            this.form.btnSearchGenba.Enabled = !isBool;           
            this.form.btnSearchUnbanGyousha.Enabled = !isBool;
            this.form.btnShobunPattern.Enabled = !isBool;
            this.form.btnShobunPattern2.Enabled = !isBool;
            this.form.btnLastShobunPattern.Enabled = !isBool;
            this.form.btnLastShobunPattern2.Enabled = !isBool;

            this.form.grdIchiran.ReadOnly = isBool;
        }

   #endregion 

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
            LogUtility.DebugMethodStart();

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
            return buttonSetting.LoadButtonSetting(thisAssembly, OboegakiIkkatuHoshuConst.ButtonInfoXmlPath);
        }
        #endregion        

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
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

            LogUtility.DebugMethodEnd(parentForm);
        }
        /// <summary>
        /// パタンーン設定する
        /// </summary>
        public void SetPatternData(r_framework.CustomControl.CustomTextBox itemName, object itemSysId, object itemSeq, int LastSbnKbn)
        {
            LogUtility.DebugMethodStart(itemName, itemSysId, itemSeq, LastSbnKbn);

            M_SBNB_PATTERN data = new M_SBNB_PATTERN();
            string name = itemName.Text.ToString();

            data.PATTERN_NAME = name;
            data.LAST_SBN_KBN = (Int16)LastSbnKbn;
            if (string.IsNullOrEmpty(data.PATTERN_NAME))
            {
                ((r_framework.CustomControl.CustomTextBox)itemSysId).Text = string.Empty;
                ((r_framework.CustomControl.CustomTextBox)itemSeq).Text = string.Empty;
                return;
            }

            // マスタデータを取得する
            DataTable pattern = this.mEntryDao.GetpatternName(data);
            if (pattern == null || pattern.Rows.Count <= 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E020", "処分場パターン");
                ((r_framework.CustomControl.CustomTextBox)itemName).Text = string.Empty;
                ((r_framework.CustomControl.CustomTextBox)itemSysId).Text = string.Empty;
                ((r_framework.CustomControl.CustomTextBox)itemSeq).Text = string.Empty;
                itemName.Focus();
                return;
            }

            // systemIdをセットする
            if (pattern != null && itemSysId != null)
            {
                ((r_framework.CustomControl.CustomTextBox)itemSysId).Text = pattern.Rows[0]["SYSTEM_ID"].ToString();

            }

            // Seqをセットする
            if (pattern != null && itemSeq != null)
            {
                ((r_framework.CustomControl.CustomTextBox)itemSeq).Text = pattern.Rows[0]["SEQ"].ToString();
            }

            LogUtility.DebugMethodEnd(itemName, itemSysId, itemSeq, LastSbnKbn);
        }

        #endregion

        #region  データ処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search(string pDenpyouNumber)
        {
            LogUtility.DebugMethodStart(pDenpyouNumber);

            //伝票番号
            this.SearchString.Denpyou_Number = pDenpyouNumber;
            
            //データ取得
            this.SearchItakuMemoIkkatsuEntryResult = mEntryDao.GetDataForEntryEntity(this.SearchString);
            int count = this.SearchItakuMemoIkkatsuEntryResult == null ? 0 : this.SearchItakuMemoIkkatsuEntryResult.Rows.Count;
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
            this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult==null ?"0": this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString();
            this.header.alertNumber.Text = this.alertCount.ToString();

            LogUtility.DebugMethodEnd(pDenpyouNumber);
            return count;
            
        }
         /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            //検索条件設定          
            this.SearchString.Hst_Gyousha_Cd = this.form.txtHaishutsuJigyoushaCd.Text.ToString().Trim();
            this.SearchString.Hst_Genba_Cd = this.form.txtGenbaCd.Text.ToString().Trim();
            this.SearchString.Unpan_Gyousha_Cd = this.form.txtUnbanGyoushaCd.Text.ToString().Trim();
            this.SearchString.Shobun_Pattern_Name = this.form.txtShobunPatternNm.Text.ToString().Trim();
            this.SearchString.Last_Shobun_Pattern_Name = this.form.txtLastShobunPatternNm.Text.ToString().Trim();
            this.SearchString.Keiyaku_Begin = string.IsNullOrEmpty(this.form.dtpKeiyakuBegin.Text.Trim()) ? null : this.form.dtpKeiyakuBegin.Text.Substring(0, 10).Trim();
            this.SearchString.Keiyaku_End = string.IsNullOrEmpty(this.form.dtpKeiyakuEnd.Text.Trim()) ? null : this.form.dtpKeiyakuEnd.Text.Substring(0, 10).Trim();
            this.SearchString.Update_Shubetsu = this.form.UPDATE_SHUBETSU.Text.ToString().Trim() == "0" ? string.Empty : this.form.UPDATE_SHUBETSU.Text.ToString().Trim();
            this.SearchString.Keiyakusho_Shurui = this.form.KEIYAKUSHO_SHURUI.Text.ToString().Trim() == "0" ? string.Empty : this.form.KEIYAKUSHO_SHURUI.Text.ToString().Trim();


            //// 情報が存在する場合のみ明細情報の取得を行う
            DataTable result= mEntryDao.GetDataForDetailByJyokenEntity(this.SearchString);
            this.SearchItakuMemoIkkatsuDetailResult =result.Clone();
            string strtemp = string.Empty;
            foreach (DataRow row in result.Rows)
            {
                if (string.IsNullOrEmpty(strtemp))
                {
                   DataRow newRow= SearchItakuMemoIkkatsuDetailResult.NewRow();
                    strtemp = row["ITAKU_KEIYAKU_NO"].ToString();
                    for (int i = 0; i < SearchItakuMemoIkkatsuDetailResult.Columns.Count - 1; i++)
                    {
                        newRow[i] = row[i];
                    }
                    SearchItakuMemoIkkatsuDetailResult.Rows.Add(newRow);
                }
                else if(!strtemp.Equals(row["ITAKU_KEIYAKU_NO"].ToString()))
                {
                    DataRow newRow = SearchItakuMemoIkkatsuDetailResult.NewRow();
                    strtemp = row["ITAKU_KEIYAKU_NO"].ToString();
                    for (int i = 0; i < SearchItakuMemoIkkatsuDetailResult.Columns.Count - 1; i++)
                    {
                        newRow[i] = row[i];
                    }
                    SearchItakuMemoIkkatsuDetailResult.Rows.Add(newRow);
                }
            }
            this.SearchItakuMemoIkkatsuDetailResult.AcceptChanges();
            int cnt = this.SearchItakuMemoIkkatsuDetailResult.Rows.Count;
            this.form.grdIchiran.Rows.Clear();
            // ヘッダー項目           
            this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString();
            this.header.alertNumber.Text = this.alertCount.ToString();
            LogUtility.DebugMethodEnd();
            return cnt;
        }
       
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetEntry(string shoriMode)
        {
            LogUtility.DebugMethodStart();
           

            //検索結果を設定する
            DataTable table = this.SearchItakuMemoIkkatsuEntryResult;

            // ヘッダー項目
            this.header.windowTypeLabel.Text = shoriMode;

            this.header.ReadDataNumber.Text = this.SearchItakuMemoIkkatsuDetailResult==null?"0":(string.IsNullOrEmpty(this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString()) ? "0" : this.SearchItakuMemoIkkatsuDetailResult.Rows.Count.ToString());
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
                this.form.txtMemo.Text = table.Rows[i]["MEMO"].ToString();
                this.form.dtpMemoUpdateDate.Value = DateTime.Parse(table.Rows[i]["MEMO_UPDATE_DATE"].ToString());

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
                if (!string.IsNullOrEmpty(table.Rows[i]["KEIYAKU_END"].ToString()))
                {
                    this.form.dtpKeiyakuEnd.Value = DateTime.Parse(table.Rows[i]["KEIYAKU_END"].ToString());
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
                this.form.txtLastShobunPatternSysId2.Text =  table.Rows[i]["UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID"].ToString();
                this.form.txtLastShobunPatternSeq2.Text =table.Rows[i]["UPD_LAST_SHOBUN_PATTERN_SEQ"].ToString();
              
            }

            LogUtility.DebugMethodEnd();
        }   

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();
            DataTable table = this.SearchItakuMemoIkkatsuDetailResult;
            this.form.grdIchiran.Rows.Clear();
            if (table != null)
            {
                table.BeginLoadData();              
                int j = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.grdIchiran.Rows.Add();
                    j = i;
                    //this.form.grdIchiran.Rows[j].Cells["ROW_NO"].Value = i+1;
                    if (!string.IsNullOrEmpty(table.Rows[i]["SHORI_KBN"].ToString ()))
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
                    this.form.grdIchiran.Rows[j].Cells["ITAKU_KEIYAKU_DATE_END"].Value = table.Rows[i]["ITAKU_KEIYAKU_DATE_END"];
                    this.form.grdIchiran.Rows[j].Cells["LAST_SHOBUN_PATTERN_NAME"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_NAME"];
                    this.form.grdIchiran.Rows[i].Cells["LAST_SHOBUN_PATTERN_SYSYTEM_ID"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_SYSYTEM_ID"];
                    this.form.grdIchiran.Rows[j].Cells["LAST_SHOBUN_PATTERN_SEQ"].Value = table.Rows[i]["LAST_SHOBUN_PATTERN_SEQ"];


                }             
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 一覧画面表示
        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        public void ShowIchiran()
        {
            LogUtility.DebugMethodStart();


            var callForm = new OboeGakiIkktuImputIchiran.M421Form(this.form.ItilanWindowInitUpdate);
            var callHeaderForm = new OboeGakiIkktuImputIchiran.M421HeaderForm();
            var businessForm = new BusinessBaseForm(callForm, callHeaderForm);

            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!this.form.mstartFlg)
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.Close();
                this.form.Close();
            }
            if (!isExistForm)
            {
                businessForm.Show();
            }
            else
            {
                SuperForm mainForm = callForm;
                if (this.DisposeScreenPresenceForm(callForm))
                {
                    businessForm.Show();
                }
            }

            #region XMLにて読み込み
           // LinkWindowSetting linkWindowDto = new LinkWindowSetting();

            //var thisAssembly = Assembly.GetExecutingAssembly();
            //LinkWindowSetting[] linkWindowDtoArray = LinkWindowSetting.LoadLinkWindowSetting(thisAssembly, OboegakiIkkatuHoshuConst.LinkWindowInfoXmlPath);

            ////各アセンブリの読み込みを同一メソッドで行えるように
            //// XMLにて読み込みを行うように
            //var assembly = Assembly.LoadFrom(linkWindowDtoArray[0].AssemblyName);
            //SuperForm superForm = (SuperForm)assembly.CreateInstance(
            //        linkWindowDtoArray[0].FormName, // 名前空間を含めたクラス名
            //        false, // 大文字小文字を無視するかどうか
            //        BindingFlags.CreateInstance, // インスタンスを生成
            //        null,
            //        new object[] { }, // コンストラクタの引数//this.form.ItilanWindowInitUpdate //"OboegakiIkkatuHoshuForm"
            //        null,
            //        null
            //      );
            //HeaderBaseForm hearForm = (HeaderBaseForm)assembly.CreateInstance(
            //      linkWindowDtoArray[1].FormName, // 名前空間を含めたクラス名
            //      false, // 大文字小文字を無視するかどうか
            //      BindingFlags.CreateInstance, // インスタンスを生成
            //      null,
            //      new object[] { }, // コンストラクタの引数//this.form.ItilanWindowInitUpdate 
            //      null,
            //      null
            //    );
            //if (superForm.IsDisposed)
            //{
            //    return;
            //}
            //if (hearForm.IsDisposed)
            //{
            //    return;
            //}
            //BusinessBaseForm baseForm = new BusinessBaseForm(superForm, hearForm);
            //FormControlLogic formLogic = new FormControlLogic();
            //var flag = formLogic.ScreenPresenceCheck(superForm);
            //if (!flag)
            //{
            //    baseForm.Show();
            //    var parentForm = (BusinessBaseForm)this.form.Parent;
            //    parentForm.Close();
            //    this.form.Close();
               

               
            //}
            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///  同一画面存在時処理
        /// </summary>
        /// <param name="mainForm">起動する予定のForm</param>
        /// <returns>Dispose否フラグ</returns>
        public bool DisposeScreenPresenceForm(SuperForm mainForm)
        {
            var exists = false;

            foreach (Form openForm in Application.OpenForms)
            {
                if (mainForm.GetType() == openForm.GetType())
                {

                    var superForm = openForm as SuperForm;

                    if (superForm != null)
                    {
                        if (superForm.WindowType == mainForm.WindowType)
                        {
                            exists = true;
                            var parentForm = openForm.ParentForm;
                            if (parentForm != null)
                            {
                                parentForm.BringToFront();
                            }
                            openForm.Dispose();
                            parentForm.Dispose();
                            return exists;
                        }
                    }
                }
            }
            return exists;
        }

        #endregion

        #region Entityを作成する
        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanSystemId()
        {
            int returnInt = 1;
            // 覚書一括の最大値+1を取得    
            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (Int16)OboegakiIkkatuHoshuConst.DENSHU_KBN_CD_OBOEGAKU;

            var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
            returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (Int16)OboegakiIkkatuHoshuConst.DENSHU_KBN_CD_OBOEGAKU;
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

        /// <summary>
        /// 伝票番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanOboegaku()
        {
            int returnInt = -1;
            // 伝票番号の最大値+1を取得  
            var entity = new S_NUMBER_DENSHU();
            entity.DENSHU_KBN_CD = (Int16)OboegakiIkkatuHoshuConst.DENSHU_KBN_CD_OBOEGAKU;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = (Int16)OboegakiIkkatuHoshuConst.DENSHU_KBN_CD_OBOEGAKU;
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

        public void CreateItakuMemoIkkatsuEntryEntity()
        {        

            this.dto.ItakuMemoIkkatsuEntry = new T_ITAKU_MEMO_IKKATSU_ENTRY();

            //覚書一括設定
            if (!string.IsNullOrWhiteSpace(this.form.txtSystemId.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.SYSTEM_ID = Int64.Parse(this.form.txtSystemId.Text.ToString());
            }
            if (!string.IsNullOrWhiteSpace(this.form.txtSeq.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.SEQ = Int32.Parse(this.form.txtSeq.Text.ToString());
            }
            if (!string.IsNullOrWhiteSpace(this.form.txtDenpyouNumber.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.DENPYOU_NUMBER = Int64.Parse(this.form.txtDenpyouNumber.Text.Trim());
            }

            this.dto.ItakuMemoIkkatsuEntry.MEMO_UPDATE_DATE = (DateTime)this.form.dtpMemoUpdateDate.Value;
            this.dto.ItakuMemoIkkatsuEntry.MEMO = this.form.txtMemo.Text.ToString();
            this.dto.ItakuMemoIkkatsuEntry.HST_GYOUSHA_CD = this.form.txtHaishutsuJigyoushaCd.Text.ToString();
            this.dto.ItakuMemoIkkatsuEntry.HST_GYOUSHA_NAME = this.form.txtHaishutsuJigyoushaNm.Text.ToString();

            this.dto.ItakuMemoIkkatsuEntry.HST_GENBA_CD = this.form.txtGenbaCd.Text.ToString();
            this.dto.ItakuMemoIkkatsuEntry.HST_GENBA_NAME = this.form.txtGenbaNm.Text.ToString();

            this.dto.ItakuMemoIkkatsuEntry.UNPAN_GYOUSHA_CD = this.form.txtUnbanGyoushaCd.Text.ToString();
            this.dto.ItakuMemoIkkatsuEntry.UNPAN_GYOUSHA_NAME = this.form.txtUnbanGyoushaNm.Text.ToString();

            this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm.Text.ToString();
            if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId.Text.ToString());
            }
            if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq.Text.ToString());
            }

            this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm.Text.ToString();
            if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId.Text.ToString());
            }
            if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_PATTERN_SEQ =  Int32.Parse(this.form.txtLastShobunPatternSeq.Text.ToString());

            }
            if (this.form.dtpKeiyakuBegin.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuBegin.Value.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_BEGIN = this.form.dtpKeiyakuBegin.Value.ToString();
            }
            if (this.form.dtpKeiyakuEnd.Value != null && !string.IsNullOrWhiteSpace(this.form.dtpKeiyakuEnd.Value.ToString()))
             {
                 this.dto.ItakuMemoIkkatsuEntry.KEIYAKU_END =this.form.dtpKeiyakuEnd.Value.ToString();
             }
            if (!string.IsNullOrWhiteSpace(this.form.UPDATE_SHUBETSU.Text.ToString()))
             {
                 this.dto.ItakuMemoIkkatsuEntry.UPDATE_SHUBETSU = Int16.Parse(this.form.UPDATE_SHUBETSU.Text.ToString());
             }
            if (!string.IsNullOrWhiteSpace(this.form.KEIYAKUSHO_SHURUI.Text.ToString()))
             {
                 this.dto.ItakuMemoIkkatsuEntry.KEIYAKUSHO_SHURUI = Int16.Parse(this.form.KEIYAKUSHO_SHURUI.Text.ToString());
             }
            this.dto.ItakuMemoIkkatsuEntry.SHOBUN_UPDATE_KBN = Int16.Parse(this.form.txtShobun.Text.ToString());
            if ("1".Equals(this.form.txtShobun.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text.ToString();
                if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_SYSTEM_ID =Int64.Parse(this.form.txtShobunPatternSysId2.Text.ToString());
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSeq2.Text.ToString());
                }
            }
            this.dto.ItakuMemoIkkatsuEntry.LAST_SHOBUN_UPDATE_KBN = Int16.Parse(this.form.txtLastShobun.Text.ToString());
            if ("1".Equals(this.form.txtLastShobun.Text.ToString()))
            {
                this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text.ToString();
                if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text.ToString());
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text.ToString()))
                {
                    this.dto.ItakuMemoIkkatsuEntry.UPD_LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSeq2.Text.ToString());
                }
            }
            this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = false;

            // 更新者情報設定
            var dataBinderLogicItakuMemoIkkatsuEntry = new DataBinderLogic<r_framework.Entity.T_ITAKU_MEMO_IKKATSU_ENTRY>(this.dto.ItakuMemoIkkatsuEntry);
            dataBinderLogicItakuMemoIkkatsuEntry.SetSystemProperty(this.dto.ItakuMemoIkkatsuEntry, false);
        }

        public void CreateItakuMemoIkkatsuDetailEntity()
        {
            // 一覧設定(委託契約基本情報)
            List<T_ITAKU_MEMO_IKKATSU_DETAIL> itakuMemoIkkatsuDetailList = new List<T_ITAKU_MEMO_IKKATSU_DETAIL>();
            for (int j = 0; j < this.form.grdIchiran.RowCount; j++)
            {
                Row row = this.form.grdIchiran.Rows[j];
                T_ITAKU_MEMO_IKKATSU_DETAIL temp = new T_ITAKU_MEMO_IKKATSU_DETAIL();
                if (!string.IsNullOrWhiteSpace(this.form.txtSystemId.Text.ToString()))
                {
                    temp.SYSTEM_ID = Int64.Parse(this.form.txtSystemId.Text.ToString());
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtSeq.Text.ToString()))
                {
                    temp.SEQ = Int32.Parse(this.form.txtSeq.Text.ToString());
                }
                if (!string.IsNullOrWhiteSpace(this.form.txtDenpyouNumber.Text.ToString()))
                {
                    temp.DENPYOU_NUMBER = Int32.Parse(this.form.txtDenpyouNumber.Text.ToString());
                }
                temp.ROW_NO = j + 1;
                if (row["SHORI_KBN"].Value != null)
                {
                    temp.SHORI_KBN = (bool)row["SHORI_KBN"].Value;
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
                    temp.KOUSHIN_SHUBETSU = Int16.Parse (row["KOUSHIN_SHUBETSU"].Value.ToString());
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
                //中間処分場所の更新取得
                if (this.form.txtShobun.Text.ToString().Equals("1"))
                {
                    temp.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text.ToString();
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text.ToString()))
                    {
                        temp.SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId2.Text.ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text.ToString()))
                    {
                        temp.SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtShobunPatternSysId2.Text.ToString());
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
                if (this.form.txtLastShobun.Text.ToString().Equals("1"))
                {
                    temp.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text.ToString();

                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text.ToString()))
                    {
                        temp.LAST_SHOBUN_PATTERN_SYSYTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text.ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text.ToString()))
                    {
                        temp.LAST_SHOBUN_PATTERN_SEQ = Int32.Parse(this.form.txtLastShobunPatternSysId2.Text.ToString());
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

        public void CreateItakuKeiyakuKihonEntity()
        {
            // 一覧設定(委託契約基本情報)
            List<M_ITAKU_KEIYAKU_KIHON> itakuKeiyakuKihon = new List<M_ITAKU_KEIYAKU_KIHON>();
            // idx = 0;
            for (int j = 0; j < this.form.grdIchiran.RowCount; j++)
            {
                Row row = this.form.grdIchiran.Rows[j];

                if (row["SHORI_KBN"].Value ==null || !(bool)row["SHORI_KBN"].Value)
                {
                    continue;
                }
                if (!this.form.txtShobun.Text.ToString().Equals("1") && !this.form.txtLastShobun.Text.ToString().Equals("1"))
                {
                    continue;
                }
                M_ITAKU_KEIYAKU_KIHON temp = new M_ITAKU_KEIYAKU_KIHON();
                temp.SYSTEM_ID = row["ITAKU_KEIYAKU_SYSTEM_ID"].Value.ToString();
                temp.ITAKU_KEIYAKU_NO = row["ITAKU_KEIYAKU_NO"].Value.ToString();
                temp=this.kihonDao.GetDataBySystemId(temp);
                
                //中間処分場所の更新取得
                if (this.form.txtShobun.Text.ToString().Equals("1"))
                {
                    temp.SHOBUN_PATTERN_NAME = this.form.txtShobunPatternNm2.Text.ToString();
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSysId2.Text.ToString()))
                    {
                        temp.SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtShobunPatternSysId2.Text.ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtShobunPatternSeq2.Text.ToString()))
                    {
                        temp.SHOBUN_PATTERN_SEQ = Int64.Parse(this.form.txtShobunPatternSysId2.Text.ToString());
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
                        temp.SHOBUN_PATTERN_SEQ = Int16.Parse(row["SHOBUN_PATTERN_SEQ"].Value.ToString());
                    }
                }
                //最終処分場所の更新取得
                if (this.form.txtLastShobun.Text.ToString().Equals("1"))
                {
                    temp.LAST_SHOBUN_PATTERN_NAME = this.form.txtLastShobunPatternNm2.Text.ToString();

                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSysId2.Text.ToString()))
                    {
                        temp.LAST_SHOBUN_PATTERN_SYSTEM_ID = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text.ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(this.form.txtLastShobunPatternSeq2.Text.ToString()))
                    {
                        temp.LAST_SHOBUN_PATTERN_SEQ = Int64.Parse(this.form.txtLastShobunPatternSysId2.Text.ToString());
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
                        temp.LAST_SHOBUN_PATTERN_SEQ = Int16.Parse(row["LAST_SHOBUN_PATTERN_SEQ"].Value.ToString());
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

        public void CreateItakuOboeEntity()
        {

            // 一覧設定(委託契約覚書)
            List<M_ITAKU_KEIYAKU_OBOE> itakuOboe = new List<M_ITAKU_KEIYAKU_OBOE>();

            ItakuKeiyakuOboeDao oboe = DaoInitUtility.GetComponent<ItakuKeiyakuOboeDao>();
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
                temp1 = oboe.GetMaxSeq(temp);


                if (temp1 != null && temp1.Rows.Count == 1 && !string.IsNullOrEmpty(temp1.Rows[0][0].ToString()))
                {
                    intSeq = Int32.Parse(temp1.Rows[0][0].ToString())+1;
                }
                else
                {
                    intSeq = 1;
                }
                temp.UPDATE_DATE = (DateTime)this.form.dtpMemoUpdateDate.Value;
                temp.MEMO = this.form.txtMemo.Text.ToString();               
                temp.SEQ = intSeq;

                // 更新者情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.M_ITAKU_KEIYAKU_OBOE>(temp);
                dbLogic.SetSystemProperty(temp, false);
                itakuOboe.Add(temp);
            }
            this.dto.ItakuKeiyakuOboe = new M_ITAKU_KEIYAKU_OBOE[itakuOboe.Count];
            this.dto.ItakuKeiyakuOboe = itakuOboe.ToArray<M_ITAKU_KEIYAKU_OBOE>();

        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public void CreateEntity(WINDOW_TYPE WindowType)
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

            LogUtility.DebugMethodEnd(WindowType);
        }

        #endregion  
    
        #region 登録/更新/削除

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var result = msgLogic.MessageBoxShow("C026");
            if (result == DialogResult.Yes)
            {
                this.dto.ItakuMemoIkkatsuEntry.DELETE_FLG = true;

                this.mEntryDao.Update(this.dto.ItakuMemoIkkatsuEntry);

                msgLogic.MessageBoxShow("I001", "削除");

                this.isRegist = true;
            }

            LogUtility.DebugMethodEnd();
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
            LogUtility.DebugMethodStart(errorFlag);

            if (errorFlag)
            {
                return;
            }
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
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("I001", "登録");
            this.isRegist = true;

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            if (errorFlag)
            {
                return;
            }
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
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("I001", "更新");
            this.isRegist = true;

            LogUtility.DebugMethodEnd();          
        }
        #endregion
    }
}
