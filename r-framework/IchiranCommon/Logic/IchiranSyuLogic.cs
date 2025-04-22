using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.APP;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Common.IchiranCommon.Dao;
using Shougun.Core.Common.IchiranCommon.Dto;
using Shougun.Core.Message;
using r_framework.Dto;

namespace Shougun.Core.Common.IchiranCommon.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class IchiranSyuLogic : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Common.IchiranCommon.Setting.IchiranSyuButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        public GetMOPDaoCls dao_GetMOP;
        public SetMOPDaoCls dao_SetMOP;
        public SetMOPCDaoCls dao_SetMOPC;
        //Communicate InxsSubApplication Start
        public SetMOPInxsDaoCls dao_SetMOPInxs;
        public SetMOPCInxsDaoCls dao_SetMOPCInxs;
        //Communicate InxsSubApplication End

        /// <summary>
        /// DTO
        /// </summary>
        private MOPDtoCls dto_MOP;
        private MOPCDtoCls dto_MOPC;

        /// <summary>
        /// Form
        /// </summary>
        private HeaderBaseForm header;
        private IchiranSyuForm form;

        private DBAccessor CommonDBAccessor;

        private PatternSetting patternSetting;

        internal bool ismobile_mode = false;

        internal bool isfile_upload = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果(出力項目)
        /// </summary>
        public DataTable Search_MOP { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<M_OUTPUT_PATTERN> MopList { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<M_OUTPUT_PATTERN_COLUMN> MopcList { get; set; }

        /// <summary>
        /// Inxs SubApplication pattern
        /// </summary>
        public List<M_OUTPUT_PATTERN_INXS> MopInxsList { get; set; }

        /// <summary>
        /// Inxs SubApplication pattern
        /// </summary>
        public List<M_OUTPUT_PATTERN_COLUMN_INXS> MopcInxsList { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranSyuLogic(IchiranSyuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            //DTO
            this.dto_MOP = new MOPDtoCls();
            this.dto_MOPC = new MOPCDtoCls();

            //DAO
            this.dao_GetMOP = DaoInitUtility.GetComponent<GetMOPDaoCls>();
            this.dao_SetMOP = DaoInitUtility.GetComponent<SetMOPDaoCls>();
            this.dao_SetMOPC = DaoInitUtility.GetComponent<SetMOPCDaoCls>();
            //Communicate InxsSubApplication Start
            this.dao_SetMOPInxs = DaoInitUtility.GetComponent<SetMOPInxsDaoCls>();
            this.dao_SetMOPCInxs = DaoInitUtility.GetComponent<SetMOPCInxsDaoCls>();
            //Communicate InxsSubApplication End

            this.CommonDBAccessor = new DBAccessor();

            this.ismobile_mode = r_framework.Configuration.AppConfig.AppOptions.IsMobile();

            this.isfile_upload = r_framework.Configuration.AppConfig.AppOptions.IsFileUpload();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BasePopForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BasePopForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            
            var parentForm = (BasePopForm)this.form.Parent;

            //<ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.customButton1_Click);

            //>ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.customButton2_Click);

            //↑ボタン(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.customButton3_Click);

            //↓ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.customButton4_Click);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            MOPDtoCls MOPDtoCls = new MOPDtoCls();
            MOPCDtoCls MOPCDtoCls = new MOPCDtoCls();

            // ヘッダー（フッター）を初期化
            this.HeaderInit();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            //出力一覧のプルダウン設定
            DataTable dt = new DataTable();
            dt.Columns.Add("Display", typeof(string));
            dt.Columns.Add("Value", typeof(int));
            dt.Rows.Add("昇順", 1);
            dt.Rows.Add("降順", 2);
            DataGridViewComboBoxColumn column = this.form.customDataGridView1.Columns["SORT_NO"] as DataGridViewComboBoxColumn;
            column.DataSource = dt;
            column.ValueMember = "Value";
            column.DisplayMember = "Display";

            var denshuKbn = (DENSHU_KBN)int.Parse(this.form.denshuKB);
            var outputKbn = denshuKbn.GetPatternOutputKbn().ToString();
            this.patternSetting = PatternManager.GetPatternSetting((int)denshuKbn);

            if (outputKbn == "1")
            {
                this.form.OUTPUT_KBN_VALUE.Text = outputKbn;
                this.form.OUTPUT_KBN_VALUE.Enabled = false;
                this.form.OUTPUT_KBN_MEISAI.Enabled = false;
                this.form.OUTPUT_KBN_JISSEKI.Enabled = false;
            }
            else if (outputKbn == "2")
            {
                this.form.OUTPUT_KBN_VALUE.Text = outputKbn;
                this.form.OUTPUT_KBN_VALUE.Enabled = false;
                this.form.OUTPUT_KBN_DENPYO.Enabled = false;
                this.form.OUTPUT_KBN_JISSEKI.Enabled = false;
            }

            if (this.ismobile_mode && (denshuKbn.Equals(DENSHU_KBN.UKEIRE_ICHIRAN) || denshuKbn.Equals(DENSHU_KBN.KEIRYOU)))
            {
                this.form.OUTPUT_KBN_JISSEKI.Visible = true;
                this.form.panel1.Width = 245;
                this.form.OUTPUT_KBN_JISSEKI.Left = 145;
                this.form.OUTPUT_KBN_VALUE.RangeSetting.Max = 3;
                if (outputKbn == "3")
                {
                    this.form.OUTPUT_KBN_VALUE.Text = outputKbn;
                    this.form.OUTPUT_KBN_VALUE.Enabled = false;
                    this.form.OUTPUT_KBN_MEISAI.Enabled = false;
                    this.form.OUTPUT_KBN_DENPYO.Enabled = false;
                }
            }
            else
            {
                this.form.OUTPUT_KBN_JISSEKI.Visible = false;
                this.form.OUTPUT_KBN_JISSEKI.Left = 4;
                this.form.panel1.Width = 146;
                this.form.OUTPUT_KBN_VALUE.RangeSetting.Max = 2;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int RowCount_Output = 0;
            int RowCount_Select = 0;
            try
            {
                switch (form.systemID)
                {
                    case ""://新規作成
                        //出力項目の取得
                        //なし

                        //出力区分
                        if (form.outputKB == "")
                        {
                            form.outputKB = "1";
                        }
                        this.form.OUTPUT_KBN_VALUE.Text = form.outputKB;

                        //パターン名
                        this.form.PATTERN_NAME.Text = "";

                        //タイムスタンプ（MOP）：非表示
                        //TODO:排他制御の修正
                        this.form.TIME_STAMP_MOP.Text = "";

                        //出力項目の表示
                        //なし

                        break;

                    default://更新
                        //出力項目の取得
                        RowCount_Output = this.Search_Output(form.systemID, "", form.denshuKB, "", "", "false");
                        if (RowCount_Output == 0)
                        {
                            MessageBoxUtility.MessageBoxShow("E045");

                            //初期化
                            form.systemID = "";

                            //出力区分
                            if (form.outputKB == "")
                            {
                                form.outputKB = "1";
                            }
                            this.form.OUTPUT_KBN_VALUE.Text = form.outputKB;

                            //パターン名
                            this.form.PATTERN_NAME.Text = "";

                            //タイムスタンプ（MOP）：非表示
                            //TODO:排他制御の修正
                            this.form.TIME_STAMP_MOP.Text = "";

                            //出力項目の表示
                            //なし
                        }
                        else
                        {
                            //出力区分
                            //初回（outputKB==""）時は、データ優先。それ以降は、画面優先。
                            if (form.outputKB == "")
                            {
                                form.outputKB = this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString();
                            }
                            this.form.OUTPUT_KBN_VALUE.Text = form.outputKB;

                            //パターン名
                            this.form.PATTERN_NAME.Text = this.Search_MOP.Rows[0]["PATTERN_NAME"].ToString();

                            //タイムスタンプ（MOP）：非表示
                            //TODO:排他制御の修正
                            this.form.TIME_STAMP_MOP.Text = this.Search_MOP.Rows[0]["TIME_STAMP_MOP"].ToString();

                            //出力項目の表示
                            if (form.outputKB != this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString())
                            {
                                //明細が無いので、出力一覧の処理をしない。
                            }
                            else if (string.IsNullOrEmpty(this.Search_MOP.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                            {
                                //明細が無いので、出力一覧の処理をしない。
                            }
                            else
                            {
                                this.SetResOutPut();
                            }
                        }
                        break;
                }

                //選択項目 データ取得
                RowCount_Select = this.patternSetting.OutputGroups.Count;
                switch (form.systemID)
                {
                    case ""://新規入力
                        if (RowCount_Select == 0)
                        {
                            MessageBoxUtility.MessageBoxShow("E020", "一覧項目選択");
                        }
                        else
                        {
                            //選択項目 表示
                            this.SetResSelect();
                        }
                        break;

                    default://更新
                        //選択項目 表示
                        this.SetResSelect();
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
            //取得件数
            int count = RowCount_Select + RowCount_Output;
            return count;
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void ClearScreen()
        {
            LogUtility.DebugMethodStart();
            
            try 
            {
                //選択項目
                for (int i = this.form.customDataGridView2.RowCount; i > 0; i--)
                {
                    this.form.customDataGridView2.Rows.RemoveAt(i - 1);
                }

                //出力項目
                for (int j = this.form.customDataGridView1.RowCount; j > 0; j--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(j - 1);
                }

                //パターン名
                this.form.PATTERN_NAME.Text = "";
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 優先順の最大値
        /// </summary>
        public int GetMaxPriorityNo()
        {
            LogUtility.DebugMethodStart();

            int PriorityNo = 0;
            int MaxPriorityNo = 0;
            if (this.form.customDataGridView1.RowCount > 0)
            {
                for (int j = 0; j < this.form.customDataGridView1.RowCount; j++)
                {
                    if (this.form.customDataGridView1.Rows[j].Cells["PRIORITY_NO"].Value.ToString() != "")
                    {
                        PriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[j].Cells["PRIORITY_NO"].Value.ToString());
                    }
                    if (MaxPriorityNo < PriorityNo)
                    {
                        MaxPriorityNo = PriorityNo;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
            return MaxPriorityNo;
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        public Boolean DuplicationCheck_OutPut(String outputKbn, String columnId)
        {
            LogUtility.DebugMethodStart(outputKbn, columnId);
            
            Boolean hantei = false;
            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (
                outputKbn == this.form.customDataGridView1.Rows[i].Cells["OUTPUT_KBN"].Value.ToString()
                &&
                columnId == this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_ID"].Value.ToString()
                )
                {
                    hantei = true;
                    break;
                }
            }

            LogUtility.DebugMethodEnd(hantei);
            return hantei;
        }

        /// <summary>
        /// 選択項目　表示
        /// </summary>
        public void SetResSelect()
        {
            LogUtility.DebugMethodStart();
   
            int MaxPriorityNo = 0;
            //選択項目
            List<OutputColumn> cols = new List<OutputColumn>();
            cols.AddRange(this.patternSetting.OutputGroups[(int)OUTPUT_KBN.DENPYOU].OutputColumns.Values);

            if (this.form.outputKB == ((int)OUTPUT_KBN.MEISAI).ToString())
            {
                cols.AddRange(this.patternSetting.OutputGroups[(int)OUTPUT_KBN.MEISAI].OutputColumns.Values);
            }
            if (this.form.outputKB == ((int)OUTPUT_KBN.JISSEKI).ToString())
            {
                for (int i = 1; i < (this.patternSetting.OutputGroups[(int)OUTPUT_KBN.JISSEKI].OutputColumns.Count + 1); i++)
                {
                    string dispName = this.patternSetting.OutputGroups[(int)OUTPUT_KBN.JISSEKI].OutputColumns[i].DispName;

                    if (!this.isfile_upload && dispName.Equals("添付ファイル"))
                    {
                        // ファイルアップロードオプションがOFFの場合、添付ファイルはリストから除外する。
                        continue;
                    }
                    else
                    {
                        cols.Add(this.patternSetting.OutputGroups[(int)OUTPUT_KBN.JISSEKI].OutputColumns[i]);
                    }
                }
            }

            bool mapboxVisible = r_framework.Configuration.AppConfig.AppOptions.IsMAPBOX();

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            // 画面起動時に電子申請で追加するコントロール・項目の表示/非表示を切り替える
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();

            //Begin: LANDUONG - 20220209 - refs#160051
            bool denshiRakuVisible = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();
            //End: LANDUONG - 20220209 - refs#160051

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            bool inxsSeikyuusho = r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            bool inxsShiharaisho = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            bool smsVisible = r_framework.Configuration.AppConfig.AppOptions.IsSMS();

            for (int i = 0; i < cols.Count; i++)
            {
                // mapboxオプションの表示切替
                if (!mapboxVisible)
                {
                    // 取引先一覧、業者一覧、現場一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "210" || this.form.denshuKB == "220")
                    {
                        if (cols[i].DispName.ToString() == "緯度" || cols[i].DispName.ToString() == "位置情報更新者" ||
                            cols[i].DispName.ToString() == "経度" || cols[i].DispName.ToString() == "位置情報更新日付")
                        {
                            continue;
                        }
                    }
                }

                // 20160429 koukoukon v2.1_電子請求書 #16612 start 
                if (!densiVisible && !denshiRakuVisible) //LANDUONG - 20220209 - refs#160051
                {
                    // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                        this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                        this.form.denshuKB == "220" || this.form.denshuKB == "928")
                    {                        
                        if (cols[i].DispName.ToString() == "出力区分" || cols[i].DispName.ToString() == "発行先コード" 
                            || cols[i].DispName.ToString() == "楽楽顧客コード") //LANDUONG - 20220209 - refs#160051
                        {
                            continue;
                        }
                    }
                }
                //Begin: LANDUONG - 20220209 - refs#160051
                else if (densiVisible && !denshiRakuVisible)
                {
                    // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                        this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                        this.form.denshuKB == "220" || this.form.denshuKB == "928")
                    {
                        if (cols[i].DispName.ToString() == "楽楽顧客コード")
                        {
                            continue;
                        }
                    }
                }
                else if (!densiVisible && denshiRakuVisible)
                {
                    // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                        this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                        this.form.denshuKB == "220" || this.form.denshuKB == "928")
                    {
                        if (cols[i].DispName.ToString() == "発行先コード")
                        {
                            continue;
                        }
                        if (!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && this.form.denshuKB == "210" && cols[i].DispName.ToString() == "楽楽顧客コード")
                        {
                            continue;
                        }
                        if ((!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && !this.form.OUTPUT_KBN_VALUE.Text.Equals("2")) && this.form.denshuKB == "220"
                            && cols[i].DispName.ToString() == "楽楽顧客コード")
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                        this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                        this.form.denshuKB == "220" || this.form.denshuKB == "928")
                    {
                        if (!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && this.form.denshuKB == "210" && cols[i].DispName.ToString() == "楽楽顧客コード")
                        {
                            continue;
                        }
                        if ((!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && !this.form.OUTPUT_KBN_VALUE.Text.Equals("2")) && this.form.denshuKB == "220"
                            && cols[i].DispName.ToString() == "楽楽顧客コード")
                        {
                            continue;
                        }
                    }
                }
                //End: LANDUONG - 20220209 - refs#160051

                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                if (!onlineBankVisible)
                {
                    // 取引先一覧、引合取引先一覧
                    if (this.form.denshuKB == "200" || this.form.denshuKB == "927")
                    {
                        if (cols[i].DispName.ToString() == "振込人名１" || cols[i].DispName.ToString() == "振込人名２")
                        {
                            continue;
                        }
                    }
                }

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                // 取引先一覧
                if (!inxsSeikyuusho && this.form.denshuKB == "200")
                {
                    if (cols[i].DispName.ToString() == "INXS請求区分")
                    {
                        continue;
                    }
                }

                // 請求一覧
                if (!inxsSeikyuusho && this.form.denshuKB == "903")
                {
                    if (cols[i].DispName.ToString() == "アップロード状況")
                    {
                        continue;
                    }

                    if (cols[i].DispName.ToString() == "ダウンロード状況")
                    {
                        continue;
                    }
                }
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                // 取引先一覧
                if (!inxsShiharaisho && this.form.denshuKB == "200")
                {
                    if (cols[i].DispName.ToString() == "INXS支払区分")
                    {
                        continue;
                    }
                }

                // 支払明細一覧
                if (!inxsShiharaisho && this.form.denshuKB == "904")
                {
                    if (cols[i].DispName.ToString() == "アップロード状況")
                    {
                        continue;
                    }

                    if (cols[i].DispName.ToString() == "ダウンロード状況")
                    {
                        continue;
                    }
                }
                // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                if (!DuplicationCheck_OutPut(cols[i].OutputKbn.ToString(), cols[i].ID.ToString()))
                {
                    if (cols[i].Needs)
                    {
                        MaxPriorityNo = this.GetMaxPriorityNo() + 1;

                        this.form.customDataGridView1.Rows.Add();
                        int index = this.form.customDataGridView1.Rows.Count - 1;
                        //表示項目名（項目論理名）
                        this.form.customDataGridView1.Rows[index].Cells["KOUMOKU_RONRI_NAME"].Value = cols[i].DispName; ;

                        //並び順
                        this.form.customDataGridView1.Rows[index].Cells["SORT_NO"].Value = 1;

                        //優先順
                        this.form.customDataGridView1.Rows[index].Cells["PRIORITY_NO"].Value = MaxPriorityNo;

                        //必須区分：非表示
                        this.form.customDataGridView1.Rows[index].Cells["HISSU_KBN"].Value = cols[i].Needs;

                        // 出力区分：非表示
                        this.form.customDataGridView1.Rows[index].Cells["OUTPUT_KBN"].Value = cols[i].OutputKbn;

                        // 項目ID：非表示
                        this.form.customDataGridView1.Rows[index].Cells["KOUMOKU_ID"].Value = cols[i].ID;

                        //タイムスタンプ（MOPC）：非表示
                        this.form.customDataGridView1.Rows[index].Cells["TIME_STAMP_MOPC"].Value = "";

                        //Communicate InxsSubApplication Start
                        this.form.customDataGridView1.Rows[index].Cells["IS_COLUMN_INXS"].Value = cols[i].IsColumnInxs;
                        //Communicate InxsSubApplication End
                    }
                    else
                    {
                        this.form.customDataGridView2.Rows.Add();
                        int indx = this.form.customDataGridView2.Rows.Count - 1;
                        // 表示項目名（項目論理名）
                        this.form.customDataGridView2.Rows[indx].Cells["KOUMOKU_RONRI_NAME_SE"].Value = cols[i].DispName;

                        // 必須区分：非表示
                        this.form.customDataGridView2.Rows[indx].Cells["HISSU_KBN_SE"].Value = cols[i].Needs;

                        // 出力区分：非表示
                        this.form.customDataGridView2.Rows[indx].Cells["OUTPUT_KBN_SE"].Value = cols[i].OutputKbn;

                        // 項目ID：非表示
                        this.form.customDataGridView2.Rows[indx].Cells["KOUMOKU_ID_SE"].Value = cols[i].ID;

                        // 出力順：非表示
                        this.form.customDataGridView2.Rows[indx].Cells["DISP_NUMBER_SE"].Value = cols[i].DispNum;

                        //タイムスタンプ（MOPC）：非表示
                        this.form.customDataGridView2.Rows[indx].Cells["TIME_STAMP_MOPC_SE"].Value = "";

                        //Communicate InxsSubApplication Start
                        this.form.customDataGridView2.Rows[indx].Cells["IS_COLUMN_INXS_SE"].Value = cols[i].IsColumnInxs;
                        //Communicate InxsSubApplication End
                    }
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞオプションの表示切替
                if (!smsVisible)
                {
                    // 受付一覧
                    if (this.form.denshuKB == "1001" || this.form.denshuKB == "1002" || this.form.denshuKB == "1003"
                        || this.form.denshuKB == "1007" || this.form.denshuKB == "1008")
                    {
                        if (cols[i].DispName.ToString() == "SMS送信")
                        {
                            for(int j = 0; j < this.form.customDataGridView2.Rows.Count; j++)
                            {
                                if (this.form.customDataGridView2.Rows[j].Cells[0].Value.ToString() == "SMS送信")
                                {
                                    // ｼｮｰﾄﾒｯｾｰｼﾞオプション=オフ時、指定列を非表示
                                    this.form.customDataGridView2.Rows[j].Visible = false;
                                }
                            }
                        }
                        continue;
                    }
                }
            }

            this.form.customDataGridView2.Sort(new CustomComparer(SortOrder.Ascending));

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力項目　データ取得
        /// </summary>
        /// <param>システムID
        /// <param>枝番
        /// <param>出力区分
        [Transaction]
        public virtual int Search_Output(String SYSTEM_ID, String SEQ, String DENSHU_KBN_CD, String OUTPUT_KBN, String DETAIL_SYSTEM_ID, String DELETE_FLG)
        {
            LogUtility.DebugMethodStart(SYSTEM_ID, SEQ, DENSHU_KBN_CD, OUTPUT_KBN, DETAIL_SYSTEM_ID, DELETE_FLG);
            
            MOPDtoCls data = new MOPDtoCls();
            data.SYSTEM_ID = SYSTEM_ID;
            data.SEQ = SEQ;
            data.DENSHU_KBN_CD = DENSHU_KBN_CD;
            data.OUTPUT_KBN = OUTPUT_KBN;
            data.DELETE_FLG = DELETE_FLG;
            //Communicate InxsSubApplication Start
            data.IS_USE_INXS = r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke();
            //Communicate InxsSubApplication End

            this.Search_MOP = dao_GetMOP.GetDataForEntity(data);

            //取得件数
            int count = this.Search_MOP.Rows.Count;

            LogUtility.DebugMethodEnd(count);

            return count;
        }

        /// <summary>
        /// 出力項目　表示
        /// </summary>
        public void SetResOutPut()
        {
            LogUtility.DebugMethodStart();

            try
            {
                //出力項目
                int SortNo = 0;
                int PriorityNo = 0;
                int i = 0;

                int outputKbn;
                int.TryParse(this.form.outputKB, out outputKbn);

                if (outputKbn <= 0)
                {
                    return;
                }

                bool mapboxVisible = r_framework.Configuration.AppConfig.AppOptions.IsMAPBOX();

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                // 画面起動時に電子申請で追加するコントロール・項目の表示/非表示を切り替える
                // densiVisible true場合表示false場合非表示
                bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();

                // Begin: LANDUONG - 20220209 - refs#160051
                bool denshiRakuVisible = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai();
                // End: LANDUONG - 20220209 - refs#160051

                foreach (DataRow row in this.Search_MOP.Rows)
                {
                    var kb = row.Field<Int16?>("OUTPUT_KBN_MOPC");
                    var id = row.Field<Int32?>("KOUMOKU_ID");

                    if (kb == null || id == null)
                    {
                        // Nullは次へ
                        continue;
                    }

                    var col = this.patternSetting.GetColumn((int)kb, (int)id);

                    if (col == null)
                    {
                        // Nullは次へ
                        continue;
                    }

                    // mapboxオプションの表示切替
                    if (!mapboxVisible)
                    {
                        // 取引先一覧、業者一覧、現場一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "210" || this.form.denshuKB == "220")
                        {
                            if (col.DispName.ToString() == "緯度" || col.DispName.ToString() == "位置情報更新者" ||
                                col.DispName.ToString() == "経度" || col.DispName.ToString() == "位置情報更新日付")
                            {
                                continue;
                            }
                        }
                    }

                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    if (!densiVisible && !denshiRakuVisible) // LANDUONG - 20220209 - refs#160051
                    {
                        // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                            this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                            this.form.denshuKB == "220" || this.form.denshuKB == "928")
                        {
                            if (col.DispName.ToString() == "出力区分" || col.DispName.ToString() == "発行先コード" 
                                || col.DispName.ToString() == "楽楽顧客コード") // LANDUONG - 20220209 - refs#160051
                            {
                                continue;
                            }
                        }
                    }
                    // Begin: LANDUONG - 20220209 - refs#160051
                    else if (densiVisible && !denshiRakuVisible)
                    {
                        // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                            this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                            this.form.denshuKB == "220" || this.form.denshuKB == "928")
                        {
                            if (col.DispName.ToString() == "楽楽顧客コード")
                            {
                                continue;
                            }
                        }
                    }
                    else if (!densiVisible && denshiRakuVisible)
                    {
                        // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                            this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                            this.form.denshuKB == "220" || this.form.denshuKB == "928")
                        {
                            if (col.DispName.ToString() == "発行先コード")
                            {
                                continue;
                            }
                            if (!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && this.form.denshuKB == "210" && col.DispName.ToString() == "楽楽顧客コード")
                            {
                                continue;
                            }
                            if ((!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && !this.form.OUTPUT_KBN_VALUE.Text.Equals("2")) && this.form.denshuKB == "220"
                                && col.DispName.ToString() == "楽楽顧客コード")
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        // 取引先一覧、引合取引先一覧、業者一覧、引合業者一覧、現場一覧、引合現場一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "927" ||
                            this.form.denshuKB == "210" || this.form.denshuKB == "913" ||
                            this.form.denshuKB == "220" || this.form.denshuKB == "928")
                        {
                            if (!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && this.form.denshuKB == "210" && col.DispName.ToString() == "楽楽顧客コード")
                            {
                                continue;
                            }
                            if ((!this.form.OUTPUT_KBN_VALUE.Text.Equals("1") && !this.form.OUTPUT_KBN_VALUE.Text.Equals("2")) && this.form.denshuKB == "220"
                                && col.DispName.ToString() == "楽楽顧客コード")
                            {
                                continue;
                            }
                        }
                    }
                    // End: LANDUONG - 20220209 - refs#160051

                    // 20160429 koukoukon v2.1_電子請求書 #16612 end

                    if (!onlineBankVisible)
                    {
                        // 取引先一覧、引合取引先一覧
                        if (this.form.denshuKB == "200" || this.form.denshuKB == "927")
                        {
                            if (col.DispName.ToString() == "振込人名１" || col.DispName.ToString() == "振込人名２")
                            {
                                continue;
                            }
                        }
                    }

                    this.form.customDataGridView1.Rows.Add();

                    // 表示項目名（項目論理名）
                    this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_RONRI_NAME"].Value = col.DispName;

                    //並び順
                    SortNo = (Int16)this.Search_MOP.Rows[i]["SORT_NO"];
                    this.form.customDataGridView1.Rows[i].Cells["SORT_NO"].Value = SortNo != 1 && SortNo != 2 ? 1 : SortNo;

                    //優先順
                    PriorityNo = (Int16)this.Search_MOP.Rows[i]["PRIORITY_NO"];
                    this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value = PriorityNo;

                    // 必須区分：非表示
                    this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value = col.Needs;

                    // 出力区分：非表示
                    this.form.customDataGridView1.Rows[i].Cells["OUTPUT_KBN"].Value = col.OutputKbn;

                    // 項目ID：非表示
                    this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_ID"].Value = col.ID;

                    // タイムスタンプ（MOPC）：非表示
                    this.form.customDataGridView1.Rows[i].Cells["TIME_STAMP_MOPC"].Value = this.Search_MOP.Rows[i]["TIME_STAMP_MOPC"];

                    //Communicate InxsSubApplication Start
                    this.form.customDataGridView1.Rows[i].Cells["IS_COLUMN_INXS"].Value = col.IsColumnInxs;
                    //Communicate InxsSubApplication End

                    i++;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 移動処理（F1～F4）

        /// <summary>
        /// 選択項目へ移動（←ボタン）
        /// </summary>
        public void MoveSelect()
        {
            LogUtility.DebugMethodStart();

            try
            {
                int outputKbn;
                int.TryParse(this.form.outputKB, out outputKbn);
                //選択確認
                if (this.form.customDataGridView1.SelectedCells.Count > 0 && outputKbn > 0)
                {
                    int j = 0;
                    //複数項目　追加・削除
                    for (int i = this.form.customDataGridView1.SelectedCells.Count; i > 0; i--)
                    {
                        j = this.form.customDataGridView1.SelectedCells[i - 1].RowIndex;
                        switch (Boolean.Parse(this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value.ToString()))
                        {
                            case false:
                                int kbn;
                                int id;
                                int.TryParse(this.form.customDataGridView1.Rows[j].Cells["OUTPUT_KBN"].Value.ToString(), out kbn);
                                int.TryParse(this.form.customDataGridView1.Rows[j].Cells["KOUMOKU_ID"].Value.ToString(), out id);
                                var col = this.patternSetting.GetColumn(kbn, id, outputKbn);

                                if (col != null)
                                {
                                    //選択項目 追加
                                    this.form.customDataGridView2.Rows.Add
                                        (col.DispName, col.Needs.ToString(), col.OutputKbn, col.ID, col.DispNum
                                        , this.form.customDataGridView1.Rows[j].Cells["TIME_STAMP_MOPC"].Value
                                        , col.IsColumnInxs
                                        );
                                }

                                //出力項目 削除
                                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.SelectedCells[i - 1].RowIndex);

                                //選択項目　画面表示処理
                                this.form.customDataGridView2.CurrentCell = this.form.customDataGridView2.Rows[0].Cells[0];
                                this.form.customDataGridView2.Focus();
                                break;

                            case true:
                                MessageBoxUtility.MessageBoxShow("I006");
                                break;
                        }
                    }

                    // 選択項目をソート
                    this.form.customDataGridView2.Sort(new CustomComparer(SortOrder.Ascending));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力項目へ移動（→ボタン）
        /// </summary>
        public void MoveOutPut()
        {
            LogUtility.DebugMethodStart();
            
            try
            {
                //選択確認
                if (this.form.customDataGridView2.SelectedCells.Count > 0)
                {
                    ////優先順の最大値
                    int MaxPriorityNo = this.GetMaxPriorityNo();

                    //選択項目　追加・削除
                    for (int i = this.form.customDataGridView2.SelectedCells.Count; i > 0; i--)
                    {
                        MaxPriorityNo += 1;
                        //選択項目 追加
                        int j = this.form.customDataGridView2.SelectedCells[i - 1].RowIndex;

                        //出力項目 追加
                        this.form.customDataGridView1.Rows.Add
                            ( this.form.customDataGridView2.Rows[j].Cells["KOUMOKU_RONRI_NAME_SE"].Value
                            , 1
                            , MaxPriorityNo
                            , this.form.customDataGridView2.Rows[j].Cells["HISSU_KBN_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["OUTPUT_KBN_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["KOUMOKU_ID_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["TIME_STAMP_MOPC_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["IS_COLUMN_INXS_SE"].Value
                            );

                        //選択項目 削除
                        this.form.customDataGridView2.Rows.RemoveAt(this.form.customDataGridView2.SelectedCells[i - 1].RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 1行上げる（↑ボタン）
        /// </summary>
        public void UpRow()
        {
            LogUtility.DebugMethodStart();
            
            try
            {
                //選択確認
                if (this.form.customDataGridView1.SelectedCells.Count > 0)
                {
                    //1行目でないこと
                    int i = this.form.customDataGridView1.CurrentCell.RowIndex;
                    if (i > 0)
                    {
                        //移動先行の挿入
                        this.form.customDataGridView1.Rows.Insert(i - 1, this.CloneWithValues(this.form.customDataGridView1.Rows[i]));

                        //移動元行の削除
                        this.form.customDataGridView1.Rows.RemoveAt(i + 1);

                        //フォーカス移動
                        this.form.customDataGridView1.Focus();
                        for (int j = 3; j > 0; j--)
                        {
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[i - 1].Cells[j - 1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 1行下げる（↓ボタン）
        /// </summary>
        public void DownRow()
        {
            LogUtility.DebugMethodStart();
            try
            {
                //選択確認
                if (this.form.customDataGridView1.SelectedCells.Count > 0)
                {
                    //最終行目でないこと
                    int i = this.form.customDataGridView1.CurrentCell.RowIndex;
                    if (i < this.form.customDataGridView1.RowCount - 1)
                    {
                        //移動先行の挿入
                        this.form.customDataGridView1.Rows.Insert(i + 2, this.CloneWithValues(this.form.customDataGridView1.Rows[i]));

                        //移動元行の削除
                        this.form.customDataGridView1.Rows.RemoveAt(i);

                        //フォーカス移動
                        this.form.customDataGridView1.Focus();
                        for (int j = 3; j > 0; j--)
                        {
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[i + 1].Cells[j - 1];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///DataGridViewの行を複製する。
        /// </summary>
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart();

            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 i = 0; i < row.Cells.Count; i++)
            {
                clonedRow.Cells[i].Value = row.Cells[i].Value;
            }

            LogUtility.DebugMethodEnd();
            return clonedRow;
        }

        #endregion

        #region 登録処理（F9）

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            try
            {
                //登録前チェック
                if (this.CheckRegister())
                {
                    using (Transaction tran = new Transaction())
                    {
                        switch (form.systemID)
                        {
                            case ""://新規作成
                                //一覧出力項目を登録
                                //一覧出力項目詳細を登録
                                this.Regist_OutPut();
                                break;

                            default://更新
                                this.Regist_OutPut();
                                this.LogicalDelete_OutPut();
                                break;
                        }

                        tran.Commit();
                    }

                    MessageBoxUtility.MessageBoxShow("I001", "登録");

                    //検索処理
                    this.ClearScreen();
                    this.Search();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録事前確認処理
        /// </summary>
        public Boolean CheckRegister()
        {
            LogUtility.DebugMethodStart();

            //登録データチェック
            if (this.form.customDataGridView1.RowCount == 0)
            {
                MessageBoxUtility.MessageBoxShow("E046", "項目が未選択", "一覧出力項目の");
                
                //PTバグトラブル管理表 No.790対応
                //フォーカスを選択項目へ移動
                this.form.customDataGridView2.Select();
                
                return false;
            }

            //パターン名 入力チェック
            if (this.form.PATTERN_NAME.Text == "")
            {
                MessageBoxUtility.MessageBoxShow("E012", "パターン名");
                //フォーカスを出力区分へ移動
                this.form.PATTERN_NAME.Select();
                return false;
            }

            //優先度 入力チェック
            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value.ToString() == "")
                {
                    MessageBoxUtility.MessageBoxShow("E001", "優先度");
                    //フォーカスを出力区分へ移動
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"];
                    return false;
                }
            }

            //優先度 重複チェック
            int PriorityNo = 0;
            int TmpPriorityNo = 0;
            for (int j = 0; j < this.form.customDataGridView1.RowCount; j++)
            {
                PriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[j].Cells["PRIORITY_NO"].Value.ToString());
                for (int k = j + 1; k < this.form.customDataGridView1.RowCount - (j + 1); k++)
                {
                    TmpPriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[k].Cells["PRIORITY_NO"].Value.ToString());

                    if (PriorityNo == TmpPriorityNo)
                    {
                        MessageBoxUtility.MessageBoxShow("E031", "優先度");
                        //フォーカスを出力区分へ移動
                        this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["KOUMOKU_RONRI_NAME"];
                        this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["SORT_NO"];
                        this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["PRIORITY_NO"];
                        return false;
                    }
                }
            }

            // 優先度 欠番は詰める
            var pNo = new Dictionary<int, int>();
            for (int l = 0; l < this.form.customDataGridView1.RowCount; l++)
            {
                pNo[l] = int.Parse(this.form.customDataGridView1.Rows[l].Cells["PRIORITY_NO"].Value.ToString());
            }

            // 優先度でソートしてから採番し直し
            var kvpList = pNo.OrderBy(n => n.Value);
            int m = 1; // 1からスタート
            foreach (var kvp in kvpList)
            {
                this.form.customDataGridView1.Rows[kvp.Key].Cells["PRIORITY_NO"].Value = m;
                m++;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// 一覧明細情報を取得する
        /// </summary>
        public void GetSyutsuryokuKoumokuData(String kbn)
        {
            LogUtility.DebugMethodStart();

            List<M_OUTPUT_PATTERN> MpList = new List<M_OUTPUT_PATTERN>();
            M_OUTPUT_PATTERN mop;

            List<M_OUTPUT_PATTERN_COLUMN> MpcList = new List<M_OUTPUT_PATTERN_COLUMN>();
            M_OUTPUT_PATTERN_COLUMN mopc;

            //Communicate InxsSubApplication Start
            List<M_OUTPUT_PATTERN_INXS> MpInxsList = new List<M_OUTPUT_PATTERN_INXS>();
            M_OUTPUT_PATTERN_INXS mopInxs;

            List<M_OUTPUT_PATTERN_COLUMN_INXS> MpcInxsList = new List<M_OUTPUT_PATTERN_COLUMN_INXS>();
            M_OUTPUT_PATTERN_COLUMN_INXS mopcInxs;
            //Communicate InxsSubApplication End

            switch (kbn)
            {
                case "InsMOP":
                    //一覧出力項目
                    mop = new M_OUTPUT_PATTERN();
                    //発番
                    if (this.Search_MOP == null)
                    {
                        //2013-11-06 Upd ogawamut
                        //form.systemID = this.CommonDBAccessor.createSystemId(SqlInt16.Parse(this.form.denshuKB.ToString())).ToString();
                        var dba = new Common.BusinessCommon.DBAccessor();

                        for (int i = 0; i < 2; i++)
                        {
                            form.systemID = dba.createSystemId((int)DENSHU_KBN.ICHIRANSYUTSURYOKU_KOUMOKU).ToString();
                        }
                        mop.SYSTEM_ID = SqlInt64.Parse(form.systemID);
                        mop.SEQ = 1;
                    }
                    else
                    {
                        mop.SYSTEM_ID = SqlInt64.Parse(form.systemID);
                        mop.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                    }
                    mop.DENSHU_KBN_CD = String.IsNullOrEmpty(this.form.denshuKB) ? SqlInt16.Null : SqlInt16.Parse(this.form.denshuKB);
                    mop.OUTPUT_KBN = String.IsNullOrEmpty(this.form.OUTPUT_KBN_VALUE.Text.ToString()) ? SqlInt16.Null : SqlInt16.Parse(this.form.OUTPUT_KBN_VALUE.Text.ToString());
                    mop.PATTERN_NAME = this.form.PATTERN_NAME.Text.ToString();
                    mop.DELETE_FLG = false;

                    DataBinderLogic<M_OUTPUT_PATTERN> WHO_InsMOP = new DataBinderLogic<M_OUTPUT_PATTERN>(mop);
                    WHO_InsMOP.SetSystemProperty(mop, false);
                
                    //TODO:排他制御の修正
                    mop.TIME_STAMP = null;

                    MpList.Add(mop);
                    this.MopList = MpList;

                    //Communicate InxsSubApplication Start
                    bool isExistsColumnInxs = false;
                    for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                    {
                        DataGridViewRow crtRow = this.form.customDataGridView1.Rows[i];
                        bool isInxs;
                        if (crtRow.Cells["IS_COLUMN_INXS"].Value != null && bool.TryParse(crtRow.Cells["IS_COLUMN_INXS"].Value.ToString(), out isInxs) && isInxs) //INXS
                        {
                            isExistsColumnInxs = true;
                            break;
                        }
                    }
                    if (isExistsColumnInxs)
                    {
                        mopInxs = new M_OUTPUT_PATTERN_INXS();
                        mopInxs.SYSTEM_ID = mop.SYSTEM_ID;
                        mopInxs.SEQ = mop.SEQ;
                        mopInxs.HYOUJUN_TEMPLATE_CD = SystemProperty.AppSettingInxs.BusinessType;
                        MpInxsList.Add(mopInxs);
                        this.MopInxsList = MpInxsList;
                    }
                    //Communicate InxsSubApplication End
                    break;

                case "LogDelMOP":
                    //一覧出力項目
                    mop = new M_OUTPUT_PATTERN();
                    mop.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                    mop.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString());
                    mop.DENSHU_KBN_CD = String.IsNullOrEmpty(this.form.denshuKB) ? SqlInt16.Null : SqlInt16.Parse(this.form.denshuKB);
                    mop.OUTPUT_KBN = SqlInt16.Parse(this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString());
                    mop.PATTERN_NAME = this.Search_MOP.Rows[0]["PATTERN_NAME"].ToString();
                    mop.DELETE_FLG = true;

                    DataBinderLogic<M_OUTPUT_PATTERN> WHO_LogDelMOP = new DataBinderLogic<M_OUTPUT_PATTERN>(mop);
                    WHO_LogDelMOP.SetSystemProperty(mop, false);

                    //TODO:排他制御の修正
                    Int32 data2 = Int32.Parse(this.form.TIME_STAMP_MOP.Text);
                    mop.TIME_STAMP = ConvertStrByte.In32ToByteArray(data2);

                    MpList.Add(mop);
                    this.MopList = MpList;
                    break;

                case "InsMOPC":
                    //一覧出力項目詳細
                    for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                    {
                        DataGridViewRow crtRow = this.form.customDataGridView1.Rows[i];
                        
                        //Communicate InxsSubApplication Start
                        //mopc = new M_OUTPUT_PATTERN_COLUMN();
                        //mopc.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                        //if (this.Search_MOP == null)
                        //{
                        //    mopc.SEQ = 1;
                        //}
                        //else
                        //{
                        //    mopc.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                        //}
                        
                        //mopc.DETAIL_SYSTEM_ID = i;
                        //mopc.OUTPUT_KBN = SqlInt16.Parse(crtRow.Cells["OUTPUT_KBN"].Value.ToString());
                        //mopc.KOUMOKU_ID = SqlInt32.Parse(crtRow.Cells["KOUMOKU_ID"].Value.ToString());
                        //mopc.SORT_NO = SqlInt16.Parse(crtRow.Cells["SORT_NO"].Value.ToString());
                        //mopc.PRIORITY_NO = SqlInt16.Parse(crtRow.Cells["PRIORITY_NO"].Value.ToString());
                        //mopc.TIME_STAMP = null;

                        //#region 後程不要に
                        //mopc.TABLE_NAME = string.Empty;                 // 何故かNull非許容…
                        //#endregion

                        //MpcList.Add(mopc);

                        bool isInxs;
                        if (crtRow.Cells["IS_COLUMN_INXS"].Value == null || !bool.TryParse(crtRow.Cells["IS_COLUMN_INXS"].Value.ToString(), out isInxs)) //INXS
                        {
                            isInxs = false;
                        }

                        if (isInxs) //INXS
                        {
                            mopcInxs = new M_OUTPUT_PATTERN_COLUMN_INXS();
                            mopcInxs.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                            if (this.Search_MOP == null)
                            {
                                mopcInxs.SEQ = 1;
                            }
                            else
                            {
                                mopcInxs.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                            }

                            mopcInxs.DETAIL_SYSTEM_ID = i;
                            mopcInxs.OUTPUT_KBN = SqlInt16.Parse(crtRow.Cells["OUTPUT_KBN"].Value.ToString());
                            mopcInxs.KOUMOKU_ID = SqlInt32.Parse(crtRow.Cells["KOUMOKU_ID"].Value.ToString());
                            mopcInxs.SORT_NO = SqlInt16.Parse(crtRow.Cells["SORT_NO"].Value.ToString());
                            mopcInxs.PRIORITY_NO = SqlInt16.Parse(crtRow.Cells["PRIORITY_NO"].Value.ToString());
                            mopcInxs.TIME_STAMP = null;

                            #region 後程不要に
                            mopcInxs.TABLE_NAME = string.Empty;                 // 何故かNull非許容…
                            #endregion

                            MpcInxsList.Add(mopcInxs);
                        }
                        else //SHOUGUN
                        {
                            mopc = new M_OUTPUT_PATTERN_COLUMN();
                            mopc.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                            if (this.Search_MOP == null)
                            {
                                mopc.SEQ = 1;
                            }
                            else
                            {
                                mopc.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                            }

                            mopc.DETAIL_SYSTEM_ID = i;
                            mopc.OUTPUT_KBN = SqlInt16.Parse(crtRow.Cells["OUTPUT_KBN"].Value.ToString());
                            mopc.KOUMOKU_ID = SqlInt32.Parse(crtRow.Cells["KOUMOKU_ID"].Value.ToString());
                            mopc.SORT_NO = SqlInt16.Parse(crtRow.Cells["SORT_NO"].Value.ToString());
                            mopc.PRIORITY_NO = SqlInt16.Parse(crtRow.Cells["PRIORITY_NO"].Value.ToString());
                            mopc.TIME_STAMP = null;

                            #region 後程不要に
                            mopc.TABLE_NAME = string.Empty;                 // 何故かNull非許容…
                            #endregion

                            MpcList.Add(mopc);
                        }
                        //Communicate InxsSubApplication End
                    }
                    //this.MopcList = MpcList;

                    //Communicate InxsSubApplication Start
                    if (MpcInxsList.Any()) //INXS
                    {
                        this.MopcInxsList = MpcInxsList;
                    }

                    if (MpcList.Any()) //SHOUGUN
                    {
                        this.MopcList = MpcList;
                    }
                    //Communicate InxsSubApplication End
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        //論理削除：出力項目
        public void LogicalDelete_OutPut()
        {
            LogUtility.DebugMethodStart();

            //一覧出力項目
            this.GetSyutsuryokuKoumokuData("LogDelMOP");
            if (MopList != null && MopList.Count() > 0)
            {
                foreach (M_OUTPUT_PATTERN mop in MopList)
                {
                    int CntMocLogDel = dao_SetMOP.Update(mop);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        //登録：出力項目
        public void Regist_OutPut()
        {
            LogUtility.DebugMethodStart();

            this.GetSyutsuryokuKoumokuData("InsMOP");
            if (MopList != null && MopList.Count() > 0)
            {
                foreach (M_OUTPUT_PATTERN mop in MopList)
                {
                    int CntMopIns = dao_SetMOP.Insert(mop);
                }

                //Communicate InxsSubApplication Start
                if (MopInxsList != null && MopInxsList.Any())
                {
                    foreach (M_OUTPUT_PATTERN_INXS mopInxs in MopInxsList)
                    {
                        int CntMopInxsIns = dao_SetMOPInxs.Insert(mopInxs);
                    }
                }
                //Communicate InxsSubApplication End
            }

            //一覧出力項目詳細
            this.GetSyutsuryokuKoumokuData("InsMOPC");
            if (MopcList != null && MopcList.Count() > 0)
            {
                foreach (M_OUTPUT_PATTERN_COLUMN mopc in MopcList)
                {
                    int CntMopcIns = dao_SetMOPC.Insert(mopc);
                }

                ////Communicate InxsSubApplication Start
                //if (MopcInxsList != null && MopcInxsList.Any())
                //{
                //    foreach (M_OUTPUT_PATTERN_COLUMN_INXS mopcInxs in MopcInxsList)
                //    {
                //        int CntMopcIns = dao_SetMOPCInxs.Insert(mopcInxs);
                //    }
                //}
                ////Communicate InxsSubApplication End
            }

            //Communicate InxsSubApplication Start
            if (MopcInxsList != null && MopcInxsList.Any())
            {
                foreach (M_OUTPUT_PATTERN_COLUMN_INXS mopcInxs in MopcInxsList)
                {
                    int CntMopcIns = dao_SetMOPCInxs.Insert(mopcInxs);
                }
            }
            //Communicate InxsSubApplication End

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 終了処理

        /// <summary>
        /// 終了する。
        /// </summary>
        public void FormClose()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BasePopForm)this.form.Parent;

            this.ClearScreen();
            
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        ///物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// customDataGridView2のソートオーダークラス
        /// </summary>
        public class CustomComparer : IComparer
        {
            private int sortOrder;
            private Comparer comparer;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="order"></param>
            public CustomComparer(SortOrder order)
            {
                this.sortOrder = (order == SortOrder.Descending ? -1 : 1);
                this.comparer = new Comparer(
                    System.Globalization.CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// 比較メソッド
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y)
            {
                int result = 0;

                DataGridViewRow rowx = (DataGridViewRow)x;
                DataGridViewRow rowy = (DataGridViewRow)y;

                // 出力区分でソートした後に項目IDでソート
                result = this.comparer.Compare(rowx.Cells["OUTPUT_KBN_SE"].Value, rowy.Cells["OUTPUT_KBN_SE"].Value);
                if (result == 0)
                {
                    result = this.comparer.Compare(rowx.Cells["DISP_NUMBER_SE"].Value, rowy.Cells["DISP_NUMBER_SE"].Value);
                }

                //結果を返す
                return result * this.sortOrder;
            }
        }
    }
}