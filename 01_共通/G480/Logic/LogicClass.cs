using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
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
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranSyuDenpyou.DAO;

namespace Shougun.Core.Common.IchiranSyuDenpyou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Common.IchiranSyuDenpyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        public GetSOCSDaoCls dao_GetSOCS;
        public GetMOPDaoCls dao_GetMOP;
        public SetMOPDaoCls dao_SetMOP;
        public SetMOPCDaoCls dao_SetMOPC;


        /// <summary>
        /// DTO
        /// </summary>
        private SOCSDtoCls dto_SOCS;
        private MOPDtoCls dto_MOP;
        private MOPCDtoCls dto_MOPC;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;

        private DBAccessor CommonDBAccessor;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果(選択項目)
        /// </summary>
        public DataTable Search_SOCS { get; set; }

        /// <summary>
        /// 検索結果(出力項目)
        /// </summary>
        public DataTable Search_MOP { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<M_OUTPUT_PATTERN_HIMO> MopList { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<M_OUTPUT_PATTERN_COLUMN_HIMO> MopcList { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;

                //DTO
                this.dto_SOCS = new SOCSDtoCls();
                this.dto_MOP = new MOPDtoCls();
                this.dto_MOPC = new MOPCDtoCls();

                //DAO
                this.dao_GetSOCS = DaoInitUtility.GetComponent<DAO.GetSOCSDaoCls>();
                this.dao_GetMOP = DaoInitUtility.GetComponent<DAO.GetMOPDaoCls>();
                this.dao_SetMOP = DaoInitUtility.GetComponent<DAO.SetMOPDaoCls>();
                this.dao_SetMOPC = DaoInitUtility.GetComponent<DAO.SetMOPCDaoCls>();

                this.CommonDBAccessor = new DBAccessor();

            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;

                //ヘッダーの初期化
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;

            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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

                LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
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
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SOCSDtoCls SOCSDtoCls = new SOCSDtoCls();
                MOPDtoCls MOPDtoCls = new MOPDtoCls();
                MOPCDtoCls MOPCDtoCls = new MOPCDtoCls();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.C_ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU);
                this.form.cstTxtBoxSrTsKn.Text = "1";
                this.form.cstRdoBtn1.Checked = true;

                // 伝種区分設定
                DenshuKbnSet();

                this.form.OUTPUT_KBN_VALUE.Text = "1";
                this.form.OUTPUT_KBN_DENPYO.Checked = true;

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

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int RowCount_Select = 0;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 伝種区分設定
                DenshuKbnSet();

                //選択項目 データ取得
                RowCount_Select = this.Search_Select(form.denshuKB, form.outputKB, "", form.systemID, "", "false");
                switch (form.systemID)
                {
                    case ""://新規入力
                        if (RowCount_Select == 0)
                        {
                            msgLogic.MessageBoxShow("E020", "一覧項目選択");
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
                LogUtility.Error("Search", ex);
                throw;
            }

            LogUtility.DebugMethodEnd(RowCount_Select);
            return RowCount_Select;
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int SearchOutPut()
        {
            LogUtility.DebugMethodStart();

            // 変数定義
            int RowCount_Output = 0;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

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
                        this.form.TIME_STAMP_MOP.Text = "";

                        //出力項目の表示
                        //なし

                        break;

                    default://更新
                        //出力項目の取得
                        RowCount_Output = this.Search_Output(form.systemID, "", form.denshuKB, "", "", "false");
                        if (RowCount_Output == 0)
                        {
                            msgLogic.MessageBoxShow("E045");

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
                            this.form.TIME_STAMP_MOP.Text = this.Search_MOP.Rows[0]["TIME_STAMP_MOP"].ToString();

                            //出力項目の表示
                            if (string.IsNullOrEmpty(this.Search_MOP.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                            {
                                //明細が無いので、出力一覧の処理をしない。
                            }
                            else
                            {
                                this.SetResOutPut();
                                // 移動ボタン設定
                                RightMoveBtnSet();
                                LeftMoveBtnSet();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchOutPut", ex);
                throw;
            }

            LogUtility.DebugMethodEnd(RowCount_Output);
            return RowCount_Output;
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

                ////出力項目
                //for (int j = this.form.customDataGridView1.RowCount; j > 0; j--)
                //{
                //    this.form.customDataGridView1.Rows.RemoveAt(j - 1);
                //}

                ////パターン名
                //this.form.PATTERN_NAME.Text = "";
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void ClearScreenOutPut()
        {
            LogUtility.DebugMethodStart();

            try
            {
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
                LogUtility.Error("ClearScreenOutPut", ex);
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 選択項目　データ取得
        /// </summary>
        /// <param>伝種区分
        /// <param>出力区分
        /// <param>項目ID
        /// <param>システムID
        /// <param>枝番
        /// <param>削除フラグ
        [Transaction]
        public virtual int Search_Select(String DENSHU_KBN_CD, String OUTPUT_KBN, String KOUMOKU_ID, String SYSTEM_ID, String SEQ, String DELETE_FLG)
        {
            try
            {
                LogUtility.DebugMethodStart(DENSHU_KBN_CD, OUTPUT_KBN, KOUMOKU_ID, SYSTEM_ID, SEQ, DELETE_FLG);

                SOCSDtoCls data = new SOCSDtoCls();
                data.DENSHU_KBN_CD = DENSHU_KBN_CD;
                data.OUTPUT_KBN = OUTPUT_KBN;
                data.KOUMOKU_ID = KOUMOKU_ID;
                switch (SYSTEM_ID)
                {
                    case "":
                        data.SYSTEM_ID = "0";
                        break;

                    default:
                        data.SYSTEM_ID = SYSTEM_ID;
                        break;
                }
                data.SEQ = SEQ;
                data.DELETE_FLG = DELETE_FLG;

                Search_SOCS = new DataTable();

                this.Search_SOCS = dao_GetSOCS.GetDataForEntity(data);

                //取得件数
                int count = this.Search_SOCS.Rows.Count;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search_Select", ex);
                throw;
            }
        }

        /// <summary>
        /// 優先順の最大値
        /// </summary>
        public int GetMaxPriorityNo()
        {
            try
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

                LogUtility.DebugMethodEnd(MaxPriorityNo);
                return MaxPriorityNo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetMaxPriorityNos", ex);
                throw;
            }
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        public Boolean DuplicationCheck_OutPut(String Data_table, String Data_colmun)
        {
            try
            {
                LogUtility.DebugMethodStart(Data_table, Data_colmun);

                Boolean hantei = false;
                for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
                {
                    if (
                    Data_table == this.form.customDataGridView1.Rows[i].Cells["TABLE_NAME"].Value.ToString()
                    &&
                    Data_colmun == this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_BUTSURI_NAME"].Value.ToString()
                    )
                    {
                        hantei = true;
                        break;
                    }
                }

                LogUtility.DebugMethodEnd(hantei);
                return hantei;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck_OutPut", ex);
                throw;
            }
        }

        /// <summary>
        /// 選択項目　表示
        /// </summary>
        public void SetResSelect()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int j = 0;
                int k = 0;

                k = this.form.customDataGridView1.Rows.Count;

                //int MaxPriorityNo = 0;
                //選択項目
                for (int i = 0; i < this.Search_SOCS.Rows.Count; i++)
                {
                    //switch (Boolean.Parse(this.Search_SOCS.Rows[i]["HISSU_KBN"].ToString()))
                    //{
                    //    case true:

                    //        if (DuplicationCheck_OutPut(this.Search_SOCS.Rows[i]["TABLE_NAME"].ToString(), this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"].ToString()))
                    //        {
                    //        }
                    //        else
                    //        {
                    //            MaxPriorityNo = this.GetMaxPriorityNo() + 1;

                    //            this.form.customDataGridView1.Rows.Add();

                    //            //機能名
                    //            switch (form.cstTxtBoxSrTsKn.Text)
                    //            {
                    //                case "1"://受付
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受付";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 1;
                    //                    break;
                    //                case "2"://計量
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "計量";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 2;
                    //                    break;
                    //                case "3"://受入
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受入";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 3;
                    //                    break;
                    //                case "4"://出荷
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "出荷";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 4;
                    //                    break;
                    //                case "5"://売上/支払
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "売上/支払";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 5;
                    //                    break;
                    //                case "6"://マニ１次
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "マニ１次";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 6;
                    //                    break;
                    //                case "7"://マニ２次
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "マニ２次";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 7;
                    //                    break;
                    //                case "8"://電マニ
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "電マニ";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 8;
                    //                    break;
                    //                case "9"://運賃
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "運賃";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 9;
                    //                    break;
                    //                case "10"://代納
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "代納";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 10;
                    //                    break;
                    //                default://受付
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受付";
                    //                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 1;
                    //                    break;
                    //            }

                    //            //表示項目名（項目論理名）
                    //            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_RONRI_NAME"].Value = this.Search_SOCS.Rows[i]["DISP_KOUMOKU_NAME"];

                    //            //並び順
                    //            this.form.customDataGridView1.Rows[k + j].Cells["SORT_NO"].Value = 1;

                    //            //優先順
                    //            this.form.customDataGridView1.Rows[k + j].Cells["PRIORITY_NO"].Value = MaxPriorityNo;

                    //            //必須区分：非表示
                    //            this.form.customDataGridView1.Rows[k + j].Cells["HISSU_KBN"].Value = this.Search_SOCS.Rows[i]["HISSU_KBN"];

                    //            //テーブル名：非表示
                    //            this.form.customDataGridView1.Rows[k + j].Cells["TABLE_NAME"].Value = this.Search_SOCS.Rows[i]["TABLE_NAME"];

                    //            //項目物理名：非表示
                    //            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_BUTSURI_NAME"].Value = this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                    //            //タイムスタンプ（MOPC）：非表示
                    //            this.form.customDataGridView1.Rows[k + j].Cells["TIME_STAMP_MOPC"].Value = "";
                    //        }
                    //        j += 1;

                    //        break;

                    //    case false:
                    this.form.customDataGridView2.Rows.Add();

                    //表示項目名（項目論理名）
                    this.form.customDataGridView2.Rows[i - j].Cells["KOUMOKU_RONRI_NAME_SE"].Value = this.Search_SOCS.Rows[i]["DISP_KOUMOKU_NAME"];

                    ////機能名
                    //this.form.customDataGridView2.Rows[i - j].Cells["KINOU_NAME"].Value = this.Search_SOCS.Rows[i]["KINOU_NAME"];

                    ////機能番号：非表示
                    //this.form.customDataGridView2.Rows[i - j].Cells["KINOU_NO"].Value = this.Search_SOCS.Rows[i]["KINOU_NO"];

                    //必須区分：非表示
                    this.form.customDataGridView2.Rows[i - j].Cells["HISSU_KBN_SE"].Value = this.Search_SOCS.Rows[i]["HISSU_KBN"];

                    //テーブル名：非表示
                    this.form.customDataGridView2.Rows[i - j].Cells["TABLE_NAME_SE"].Value = this.Search_SOCS.Rows[i]["TABLE_NAME"];

                    //項目物理名：非表示
                    this.form.customDataGridView2.Rows[i - j].Cells["KOUMOKU_BUTSURI_NAME_SE"].Value = this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                    //タイムスタンプ（MOPC）：非表示
                    this.form.customDataGridView2.Rows[i - j].Cells["TIME_STAMP_MOPC_SE"].Value = "";
                    //}
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetResSelect", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            try
            {
                LogUtility.DebugMethodStart(SYSTEM_ID, SEQ, DENSHU_KBN_CD, OUTPUT_KBN, DETAIL_SYSTEM_ID, DELETE_FLG);

                MOPDtoCls data = new MOPDtoCls();
                data.SYSTEM_ID = SYSTEM_ID;
                data.SEQ = SEQ;
                data.DENSHU_KBN_CD = DENSHU_KBN_CD;
                data.OUTPUT_KBN = OUTPUT_KBN;
                data.DELETE_FLG = DELETE_FLG;

                this.Search_MOP = dao_GetMOP.GetDataForEntity(data);

                //取得件数
                int count = this.Search_MOP.Rows.Count;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search_Output", ex);
                throw;
            }
        }

        /// <summary>
        /// 出力項目　表示
        /// </summary>
        public void SetResOutPut()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //出力項目
                int SortNo = 0;
                int PriorityNo = 0;
                for (int i = 0; i < this.Search_MOP.Rows.Count; i++)
                {
                    this.form.customDataGridView1.Rows.Add();

                    //機能名
                    this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value = this.Search_MOP.Rows[i]["KINOU_NAME"];

                    //表示項目名（項目論理名）
                    this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_RONRI_NAME"].Value = this.Search_MOP.Rows[i]["KOUMOKU_RONRI_NAME"];

                    //並び順
                    SortNo = (Int16)this.Search_MOP.Rows[i]["SORT_NO"];
                    this.form.customDataGridView1.Rows[i].Cells["SORT_NO"].Value = SortNo;

                    //優先順
                    PriorityNo = (Int16)this.Search_MOP.Rows[i]["PRIORITY_NO"];
                    this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value = PriorityNo;

                    //機能番号：非表示
                    this.form.customDataGridView1.Rows[i].Cells["KINOU_NO"].Value = this.Search_MOP.Rows[i]["KINOU_NO"];

                    //必須区分：非表示
                    this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value = this.Search_MOP.Rows[i]["HISSU_KBN"];

                    //テーブル名：非表示
                    this.form.customDataGridView1.Rows[i].Cells["TABLE_NAME"].Value = this.Search_MOP.Rows[i]["TABLE_NAME"];

                    //項目物理名：非表示
                    this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_BUTSURI_NAME"].Value = this.Search_MOP.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                    //タイムスタンプ（MOPC）：非表示
                    this.form.customDataGridView1.Rows[i].Cells["TIME_STAMP_MOPC"].Value = this.Search_MOP.Rows[i]["TIME_STAMP_MOPC"];

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetResOutPut", ex);
                throw;
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
                //選択確認
                if (this.form.customDataGridView1.SelectedCells.Count > 0)
                {
                    int j = 0;
                    //複数項目　追加・削除
                    for (int i = this.form.customDataGridView1.SelectedCells.Count; i > 0; i--)
                    {
                        j = this.form.customDataGridView1.SelectedCells[i - 1].RowIndex;
                        string strHissuKinouName = this.form.customDataGridView1.Rows[j].Cells["KINOU_NAME"].Value.ToString();
                        int intNoHissuCnt = 0;

                        for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
                        {
                            if (!Boolean.Parse(this.form.customDataGridView1.Rows[k].Cells["HISSU_KBN"].Value.ToString()) &&
                                this.form.customDataGridView1.Rows[k].Cells["KINOU_NAME"].Value.ToString().Equals(strHissuKinouName))
                            {
                                intNoHissuCnt = intNoHissuCnt + 1;
                                break;
                            }
                        }

                        if (intNoHissuCnt > 0 && Boolean.Parse(this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value.ToString()))
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("I006");
                            break;
                        }

                        if (intNoHissuCnt == 0 && Boolean.Parse(this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value.ToString()))
                        {
                            for (int k = this.form.customDataGridView1.Rows.Count - 1; k >= 0 ; k--)
                            {
                                if (Boolean.Parse(this.form.customDataGridView1.Rows[k].Cells["HISSU_KBN"].Value.ToString()) &&
                                    this.form.customDataGridView1.Rows[k].Cells["KINOU_NAME"].Value.ToString().Equals(strHissuKinouName))
                                {
                                    //選択項目 追加
                                    this.form.customDataGridView2.Rows.Add
                                        (this.form.customDataGridView1.Rows[k].Cells["KOUMOKU_RONRI_NAME"].Value
                                        , this.form.customDataGridView1.Rows[k].Cells["HISSU_KBN"].Value
                                        , this.form.customDataGridView1.Rows[k].Cells["TABLE_NAME"].Value
                                        , this.form.customDataGridView1.Rows[k].Cells["KOUMOKU_BUTSURI_NAME"].Value
                                        , this.form.customDataGridView1.Rows[k].Cells["TIME_STAMP_MOPC"].Value
                                        );

                                    //出力項目 削除
                                    this.form.customDataGridView1.Rows.RemoveAt(k);
                                }
                            }

                        }
                        else
                        {
                            //選択項目 追加
                            this.form.customDataGridView2.Rows.Add
                                (this.form.customDataGridView1.Rows[j].Cells["KOUMOKU_RONRI_NAME"].Value
                                , this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value
                                , this.form.customDataGridView1.Rows[j].Cells["TABLE_NAME"].Value
                                , this.form.customDataGridView1.Rows[j].Cells["KOUMOKU_BUTSURI_NAME"].Value
                                , this.form.customDataGridView1.Rows[j].Cells["TIME_STAMP_MOPC"].Value
                                );

                            //出力項目 削除
                            this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.SelectedCells[i - 1].RowIndex);
                        }

                        //選択項目　画面表示処理
                        this.form.customDataGridView2.CurrentCell = this.form.customDataGridView2.Rows[0].Cells[0];
                        this.form.customDataGridView2.Focus();
                    }
                }

                // 移動ボタン設定
                RightMoveBtnSet();
                LeftMoveBtnSet();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
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
                    //優先順の最大値
                    int MaxPriorityNo = this.GetMaxPriorityNo();

                    //機能名
                    string strKnName = "受付";
                    int intKnNo = 1;
                    switch (form.cstTxtBoxSrTsKn.Text)
                    {
                        case "1"://受付
                            strKnName = "受付";
                            intKnNo = 1;
                            break;
                        case "2"://受付
                            strKnName = "計量";
                            intKnNo = 2;
                            break;
                        case "3"://受付
                            strKnName = "受入";
                            intKnNo = 3;
                            break;
                        case "4"://受付
                            strKnName = "出荷";
                            intKnNo = 4;
                            break;
                        case "5"://受付
                            strKnName = "売上/支払";
                            intKnNo = 5;
                            break;
                        case "6"://受付
                            strKnName = "マニ１次";
                            intKnNo = 6;
                            break;
                        case "7"://受付
                            strKnName = "マニ２次";
                            intKnNo = 7;
                            break;
                        case "8"://受付
                            strKnName = "電マニ";
                            intKnNo = 8;
                            break;
                        case "9"://受付
                            strKnName = "運賃";
                            intKnNo = 9;
                            break;
                        case "10"://受付
                            strKnName = "代納";
                            intKnNo = 10;
                            break;
                        default://受付
                            strKnName = "受付";
                            intKnNo = 1;
                            break;
                    }

                    //選択項目　追加・削除
                    for (int i = this.form.customDataGridView2.SelectedCells.Count; i > 0; i--)
                    {
                        MaxPriorityNo += 1;
                        //選択項目 追加
                        int j = this.form.customDataGridView2.SelectedCells[i - 1].RowIndex;

                        //出力項目 追加
                        this.form.customDataGridView1.Rows.Add
                            (strKnName
                            ,this.form.customDataGridView2.Rows[j].Cells["KOUMOKU_RONRI_NAME_SE"].Value
                            , 1
                            , MaxPriorityNo
                            , this.form.customDataGridView2.Rows[j].Cells["HISSU_KBN_SE"].Value
                            , intKnNo
                            , this.form.customDataGridView2.Rows[j].Cells["TABLE_NAME_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["KOUMOKU_BUTSURI_NAME_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["TIME_STAMP_MOPC_SE"].Value
                            );

                        //選択項目 削除
                        this.form.customDataGridView2.Rows.RemoveAt(this.form.customDataGridView2.SelectedCells[i - 1].RowIndex);
                    }
                }

                // 移動ボタン設定
                RightMoveBtnSet();
                LeftMoveBtnSet();

                // 必須項目移動
                HissuKoumokuMove();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 必須項目移動
        /// </summary>
        public void HissuKoumokuMove()
        {
            LogUtility.DebugMethodStart();

            int j = 0;
            int k = 0;

            try
            {
                k = this.form.customDataGridView1.Rows.Count;

                int MaxPriorityNo = 0;
                //選択項目
                for (int i = 0; i < this.form.customDataGridView2.Rows.Count; i++)
                {
                    if (Boolean.Parse(this.form.customDataGridView2.Rows[i].Cells["HISSU_KBN_SE"].Value.ToString()))
                    {
                        if (DuplicationCheck_OutPut(this.form.customDataGridView2.Rows[i].Cells["TABLE_NAME_SE"].ToString(), this.form.customDataGridView2.Rows[i].Cells["KOUMOKU_BUTSURI_NAME_SE"].ToString()))
                        {
                        }
                        else
                        {
                            MaxPriorityNo = this.GetMaxPriorityNo() + 1;

                            this.form.customDataGridView1.Rows.Add();

                            //機能名
                            switch (form.cstTxtBoxSrTsKn.Text)
                            {
                                case "1"://受付
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受付";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 1;
                                    break;
                                case "2"://計量
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "計量";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 2;
                                    break;
                                case "3"://受入
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受入";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 3;
                                    break;
                                case "4"://出荷
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "出荷";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 4;
                                    break;
                                case "5"://売上/支払
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "売上/支払";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 5;
                                    break;
                                case "6"://マニ１次
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "マニ１次";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 6;
                                    break;
                                case "7"://マニ２次
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "マニ２次";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 7;
                                    break;
                                case "8"://電マニ
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "電マニ";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 8;
                                    break;
                                case "9"://運賃
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "運賃";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 9;
                                    break;
                                case "10"://代納
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "代納";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 10;
                                    break;
                                default://受付
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NAME"].Value = "受付";
                                    this.form.customDataGridView1.Rows[k + j].Cells["KINOU_NO"].Value = 1;
                                    break;
                            }

                            //表示項目名（項目論理名）
                            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_RONRI_NAME"].Value = this.form.customDataGridView2.Rows[i].Cells["KOUMOKU_RONRI_NAME_SE"].Value;

                            //並び順
                            this.form.customDataGridView1.Rows[k + j].Cells["SORT_NO"].Value = 1;

                            //優先順
                            this.form.customDataGridView1.Rows[k + j].Cells["PRIORITY_NO"].Value = MaxPriorityNo;

                            //必須区分：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["HISSU_KBN"].Value = this.form.customDataGridView2.Rows[i].Cells["HISSU_KBN_SE"].Value;

                            //テーブル名：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["TABLE_NAME"].Value = this.form.customDataGridView2.Rows[i].Cells["TABLE_NAME_SE"].Value;

                            //項目物理名：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_BUTSURI_NAME"].Value = this.form.customDataGridView2.Rows[i].Cells["KOUMOKU_BUTSURI_NAME_SE"].Value;

                            //タイムスタンプ（MOPC）：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["TIME_STAMP_MOPC"].Value = "";
                        }
                        j += 1;

                        //選択項目 削除
                        this.form.customDataGridView2.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
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
                LogUtility.Debug(ex);
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
                LogUtility.Debug(ex);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///DataGridViewの行を複製する。
        /// </summary>
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            try
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
            catch (Exception ex)
            {
                LogUtility.Error("CloneWithValues", ex);
                throw;
            }
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

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("I001", "登録");

                    //検索処理
                    this.ClearScreen();
                    this.ClearScreenOutPut();
                    this.SearchOutPut();
                    this.Search();
                }
            }
            catch(Exception ex)
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
            try
            {
                LogUtility.DebugMethodStart();

                var messageShowLogic = new MessageBoxShowLogic();

                //登録データチェック
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    messageShowLogic.MessageBoxShow("E046", "項目が未選択", "一覧出力項目の");

                    //フォーカスを選択項目へ移動
                    this.form.customDataGridView2.Select();

                    return false;
                }

                //パターン名 入力チェック
                if (this.form.PATTERN_NAME.Text == "")
                {
                    messageShowLogic.MessageBoxShow("E012", "パターン名");
                    //フォーカスを出力区分へ移動
                    this.form.PATTERN_NAME.Select();
                    return false;
                }

                //優先順 入力チェック
                for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value.ToString() == "")
                    {
                        messageShowLogic.MessageBoxShow("E001", "優先順");
                        //フォーカスを出力区分へ移動
                        this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"];
                        return false;
                    }
                }

                //優先順 重複チェック
                int PriorityNo = 0;
                int TmpPriorityNo = 0;
                for (int j = 0; j < this.form.customDataGridView1.RowCount; j++)
                {
                    PriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[j].Cells["PRIORITY_NO"].Value.ToString());
                    for (int k = j + 1; k < this.form.customDataGridView1.RowCount; k++)
                    {
                        TmpPriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[k].Cells["PRIORITY_NO"].Value.ToString());

                        if (PriorityNo == TmpPriorityNo)
                        {
                            messageShowLogic.MessageBoxShow("E031", "優先順");
                            //フォーカスを出力区分へ移動
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["KINOU_NAME"];
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["KOUMOKU_RONRI_NAME"];
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["SORT_NO"];
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[k].Cells["PRIORITY_NO"];
                            return false;
                        }
                    }
                }

                //優先順 欠番チェック
                PriorityNo = 0;
                TmpPriorityNo = 0;
                for (int l = 0; l < this.form.customDataGridView1.RowCount; l++)
                {
                    PriorityNo = l + 1;

                    Boolean CheckFLG = true;
                    for (int m = 0; m < this.form.customDataGridView1.RowCount; m++)
                    {
                        TmpPriorityNo = Int16.Parse(this.form.customDataGridView1.Rows[m].Cells["PRIORITY_NO"].Value.ToString());
                        if (PriorityNo == TmpPriorityNo)
                        {
                            CheckFLG = false;
                            break;
                        }
                    }
                    if (CheckFLG)
                    {
                        messageShowLogic.MessageBoxShow("E052", PriorityNo.ToString());

                        return false;
                    }
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegister", ex);
                throw;
            }
        }

        /// <summary>
        /// 一覧明細情報を取得する
        /// </summary>
        public void GetSyutsuryokuKoumokuData(String kbn)
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<M_OUTPUT_PATTERN_HIMO> MpList = new List<M_OUTPUT_PATTERN_HIMO>();
                M_OUTPUT_PATTERN_HIMO mop;

                List<M_OUTPUT_PATTERN_COLUMN_HIMO> MpcList = new List<M_OUTPUT_PATTERN_COLUMN_HIMO>();
                M_OUTPUT_PATTERN_COLUMN_HIMO mopc;

                switch (kbn)
                {
                    case "InsMOP":
                        //一覧出力項目
                        mop = new M_OUTPUT_PATTERN_HIMO();
                        //発番
                        if (this.Search_MOP == null || this.Search_MOP.Rows.Count == 0)
                        {
                            var dba = new Common.BusinessCommon.DBAccessor();
                            form.systemID = dba.createSystemId((int)DENSHU_KBN.ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU).ToString();
                            mop.SYSTEM_ID = SqlInt64.Parse(form.systemID);
                            mop.SEQ = 1;
                        }
                        else
                        {
                            mop.SYSTEM_ID = SqlInt64.Parse(form.systemID);
                            mop.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                        }
                        //mop.OUTPUT_KBN = String.IsNullOrEmpty(this.form.OUTPUT_KBN_VALUE.Text.ToString()) ? SqlInt16.Null : SqlInt16.Parse(this.form.OUTPUT_KBN_VALUE.Text.ToString());
                        mop.OUTPUT_KBN = 1;
                        mop.PATTERN_NAME = this.form.PATTERN_NAME.Text.ToString();
                        mop.DELETE_FLG = false;

                        DataBinderLogic<M_OUTPUT_PATTERN_HIMO> WHO_InsMOP = new DataBinderLogic<M_OUTPUT_PATTERN_HIMO>(mop);
                        WHO_InsMOP.SetSystemProperty(mop, false);

                        //TODO:排他制御の修正
                        mop.TIME_STAMP = null;

                        MpList.Add(mop);
                        this.MopList = MpList;
                        break;

                    case "LogDelMOP":
                        //一覧出力項目
                        mop = new M_OUTPUT_PATTERN_HIMO();
                        mop.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                        mop.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString());
                        mop.OUTPUT_KBN = SqlInt16.Parse(this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString());
                        mop.PATTERN_NAME = this.Search_MOP.Rows[0]["PATTERN_NAME"].ToString();
                        mop.DELETE_FLG = true;

                        DataBinderLogic<M_OUTPUT_PATTERN_HIMO> WHO_LogDelMOP = new DataBinderLogic<M_OUTPUT_PATTERN_HIMO>(mop);
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
                            mopc = new M_OUTPUT_PATTERN_COLUMN_HIMO();
                            mopc.SYSTEM_ID = SqlInt64.Parse(this.form.systemID);
                            if (this.Search_MOP == null || this.Search_MOP.Rows.Count == 0)
                            {
                                mopc.SEQ = 1;
                            }
                            else
                            {
                                mopc.SEQ = SqlInt32.Parse(this.Search_MOP.Rows[0]["SEQ"].ToString()) + 1;
                            }
                            mopc.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(SqlInt16.Parse(this.form.denshuKB.ToString()));
                            mopc.KINOU_NO = SqlInt16.Parse(crtRow.Cells["KINOU_NO"].Value.ToString());
                            mopc.KINOU_NAME = crtRow.Cells["KINOU_NAME"].Value.ToString();
                            mopc.TABLE_NAME = crtRow.Cells["TABLE_NAME"].Value.ToString();
                            mopc.KOUMOKU_RONRI_NAME = crtRow.Cells["KOUMOKU_RONRI_NAME"].Value.ToString();
                            mopc.KOUMOKU_BUTSURI_NAME = crtRow.Cells["KOUMOKU_BUTSURI_NAME"].Value.ToString();
                            mopc.SORT_NO = SqlInt16.Parse(crtRow.Cells["SORT_NO"].Value.ToString());
                            mopc.PRIORITY_NO = SqlInt16.Parse(crtRow.Cells["PRIORITY_NO"].Value.ToString());
                            mopc.TIME_STAMP = null;

                            MpcList.Add(mopc);
                        }
                        this.MopcList = MpcList;
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSyutsuryokuKoumokuData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //論理削除：出力項目
        public void LogicalDelete_OutPut()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //一覧出力項目
                this.GetSyutsuryokuKoumokuData("LogDelMOP");
                if (MopList != null && MopList.Count() > 0)
                {
                    foreach (M_OUTPUT_PATTERN_HIMO mop in MopList)
                    {
                        int CntMocLogDel = dao_SetMOP.Update(mop);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete_OutPut", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //登録：出力項目
        public void Regist_OutPut()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.GetSyutsuryokuKoumokuData("InsMOP");
                if (MopList != null && MopList.Count() > 0)
                {
                    foreach (M_OUTPUT_PATTERN_HIMO mop in MopList)
                    {
                        int CntMopIns = dao_SetMOP.Insert(mop);
                    }
                }

                //一覧出力項目詳細
                this.GetSyutsuryokuKoumokuData("InsMOPC");
                if (MopcList != null && MopcList.Count() > 0)
                {
                    foreach (M_OUTPUT_PATTERN_COLUMN_HIMO mopc in MopcList)
                    {
                        int CntMopcIns = dao_SetMOPC.Insert(mopc);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist_OutPut", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 伝種区分設定DENSHU_KBN
        public void DenshuKbnSet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                switch (this.form.cstTxtBoxSrTsKn.Text)
                {
                    case "1":
                        this.form.denshuKB = ((int)DENSHU_KBN.UKETSUKE_HIMO).ToString();
                        break;
                    case "2":
                        this.form.denshuKB = ((int)DENSHU_KBN.KEIRYOU_HIMO).ToString();
                        break;
                    case "3":
                        this.form.denshuKB = ((int)DENSHU_KBN.UKEIRE_HIMO).ToString();
                        break;
                    case "4":
                        this.form.denshuKB = ((int)DENSHU_KBN.SHUKKA_HIMO).ToString();
                        break;
                    case "5":
                        this.form.denshuKB = ((int)DENSHU_KBN.URIAGE_SHIHARAI_HIMO).ToString();
                        break;
                    case "6":
                        this.form.denshuKB = ((int)DENSHU_KBN.MANI_ICHIJI_HIMO).ToString();
                        break;
                    case "7":
                        this.form.denshuKB = ((int)DENSHU_KBN.MANI_NIJI_HIMO).ToString();
                        break;
                    case "8":
                        this.form.denshuKB = ((int)DENSHU_KBN.DENMANI_HIMO).ToString();
                        break;
                    case "9":
                        this.form.denshuKB = ((int)DENSHU_KBN.UNCHIN_HIMO).ToString();
                        break;
                    case "10":
                        this.form.denshuKB = ((int)DENSHU_KBN.DAINOU_HIMO).ToString();
                        break;
                    default:
                        this.form.denshuKB = ((int)DENSHU_KBN.UKETSUKE_HIMO).ToString();
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("DenshuKbnSet", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 右へ移動ボタン設定
        public void RightMoveBtnSet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;
                int intCnt1 = 0;
                int intCnt2 = 0;
                int intCnt3 = 0;
                int intCnt4 = 0;
                int intCnt5 = 0;
                int intCnt6 = 0;
                int intCnt7 = 0;
                int intCnt8 = 0;
                int intCnt9 = 0;
                int intCnt10 = 0;

                for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value == null)
                    {
                        return;
                    }

                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("受付") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt1 = intCnt1 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("計量") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt2 = intCnt2 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("受入") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt3 = intCnt3 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("出荷") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt4 = intCnt4 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("売上/支払") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt5 = intCnt5 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("マニ１次") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt6 = intCnt6 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("マニ２次") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt7 = intCnt7 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("電マニ") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt8 = intCnt8 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("運賃") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt9 = intCnt9 + 1;
                    }
                    if (this.form.customDataGridView1.Rows[i].Cells["KINOU_NAME"].Value.Equals("代納") &&
                        !Boolean.Parse(this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value.ToString()))
                    {
                        intCnt10 = intCnt10 + 1;
                    }
                }

                switch (this.form.cstTxtBoxSrTsKn.Text)
                {
                    case "1":
                        if (intCnt1 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "2":
                        if (intCnt2 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "3":
                        if (intCnt3 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "4":
                        if (intCnt4 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "5":
                        if (intCnt5 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "6":
                        if (intCnt6 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "7":
                        if (intCnt7 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "8":
                        if (intCnt8 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "9":
                        if (intCnt9 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                    case "10":
                        if (intCnt10 > 4)
                        {
                            this.form.customButton2.Enabled = false;
                            parentForm.bt_func2.Enabled = false;
                        }
                        else
                        {
                            this.form.customButton2.Enabled = true;
                            parentForm.bt_func2.Enabled = true;
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("RightMoveBtnSet", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 左へ移動ボタン設定
        public void LeftMoveBtnSet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;

                this.form.customButton1.Enabled = false;
                parentForm.bt_func1.Enabled = false;

                string strKnName = "";
                string strKnNameSel = "";

                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;

                if (this.form.customDataGridView1.RowCount > 0 &&
                    this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[0].Value != null)
                {
                    // 機能名
                    strKnNameSel = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[0].Value.ToString();
                    switch (this.form.cstTxtBoxSrTsKn.Text)
                    {
                        case "1"://受付
                            strKnName = "受付";
                            break;
                        case "2"://受付
                            strKnName = "計量";
                            break;
                        case "3"://受付
                            strKnName = "受入";
                            break;
                        case "4"://受付
                            strKnName = "出荷";
                            break;
                        case "5"://受付
                            strKnName = "売上/支払";
                            break;
                        case "6"://受付
                            strKnName = "マニ１次";
                            break;
                        case "7"://受付
                            strKnName = "マニ２次";
                            break;
                        case "8"://受付
                            strKnName = "電マニ";
                            break;
                        case "9"://受付
                            strKnName = "運賃";
                            break;
                        case "10"://受付
                            strKnName = "代納";
                            break;
                    }

                    if (strKnNameSel == strKnName)
                    {
                        this.form.customButton1.Enabled = true;
                        parentForm.bt_func1.Enabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("LeftMoveBtnSet", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 終了処理

        /// <summary>
        /// 終了する。
        /// </summary>
        public void FormClose()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;

                this.ClearScreen();

                this.form.Close();
                parentForm.Close();

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

        #region 未使用処理
        
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
        #endregion
    }
}