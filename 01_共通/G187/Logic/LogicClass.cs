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
using Shougun.Core.Common.IchiranSyu.DAO;
using Shougun.Core.Common.IchiranSyu.Const;

namespace Shougun.Core.Common.IchiranSyu
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
        private string ButtonInfoXmlPath = "Shougun.Core.Common.IchiranSyu.Setting.ButtonSetting.xml";

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
        public List<M_OUTPUT_PATTERN> MopList { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<M_OUTPUT_PATTERN_COLUMN> MopcList { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
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
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            string denpyouShurui = string.Empty;

            // 伝票一覧→パターン一覧→一覧出力項目選択と呼ばれた場合、伝種区分CDが伝票一覧の伝種区分+伝票種類CD
            var denpyouIchiran = Convert.ToInt32(DENSHU_KBN.DENPYOU_ICHIRAN).ToString();
            if (this.form.denshuKB.StartsWith(denpyouIchiran))
            {
                string denpyouCd = this.form.denshuKB.Replace(denpyouIchiran, "");
                switch (denpyouCd)
                {
                    case "1":
                        denpyouShurui = "受入";
                        break;
                    case "2":
                        denpyouShurui = "出荷";
                        break;
                    case "3":
                        denpyouShurui = "売上/支払";
                        break;
                    case "4":
                        denpyouShurui = "計量";
                        break;
                    case "5":
                        denpyouShurui = "運賃";
                        break;
                    case "6":
                        denpyouShurui = "代納";
                        break;
                    default:
                        break;
                }
            }

            this.header.lb_title.Text = denpyouShurui + this.header.lb_title.Text;
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.header.lb_title.Text);

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

            SOCSDtoCls SOCSDtoCls = new SOCSDtoCls();
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

            if (outputKbn == "1")
            {
                this.form.OUTPUT_KBN_VALUE.Text = outputKbn;
                this.form.OUTPUT_KBN_VALUE.Enabled = false;
                this.form.OUTPUT_KBN_MEISAI.Enabled = false;
            }
            else if (outputKbn == "2")
            {
                this.form.OUTPUT_KBN_VALUE.Text = outputKbn;
                this.form.OUTPUT_KBN_VALUE.Enabled = false;
                this.form.OUTPUT_KBN_DENPYO.Enabled = false;
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
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

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
                LogUtility.Debug(ex);
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
                LogUtility.Debug(ex);
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

                default :
                    if (this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString() != OUTPUT_KBN)
                    {
                        data.SYSTEM_ID = "0";
                    }
                    else
                    { 
                        data.SYSTEM_ID = SYSTEM_ID;
                    }
                    break;
            }
            data.SEQ = SEQ;
            data.DELETE_FLG = DELETE_FLG;

            Search_SOCS = new DataTable();

            this.Search_SOCS = dao_GetSOCS.GetDataForEntity(data);

            LogUtility.DebugMethodEnd(DENSHU_KBN_CD, OUTPUT_KBN, KOUMOKU_ID, SYSTEM_ID, SEQ, DELETE_FLG);

            //取得件数
            int count = this.Search_SOCS.Rows.Count;
            return count;
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
        public Boolean DuplicationCheck_OutPut(String Data_table, String Data_colmun)
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

            LogUtility.DebugMethodEnd(Data_table, Data_colmun);
            return hantei;
        }

        /// <summary>
        /// 選択項目　表示
        /// </summary>
        public void SetResSelect()
        {
            LogUtility.DebugMethodStart();

            int j = 0;
            int k = 0;

            if (form.systemID == "") 
            {
                k = 0;
            }
            else if (form.outputKB != this.Search_MOP.Rows[0]["OUTPUT_KBN"].ToString())
            {
                k = 0;
            }
            else if (string.IsNullOrEmpty(this.Search_MOP.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
            {
                k = this.Search_MOP.Rows.Count - 1;
            }
            else
            {
                k = this.Search_MOP.Rows.Count;
            }
            
            int MaxPriorityNo = 0;
            //選択項目
            for (int i = 0; i < this.Search_SOCS.Rows.Count; i++)
            {
                switch (Boolean.Parse(this.Search_SOCS.Rows[i]["HISSU_KBN"].ToString()))
                {
                    case true:

                        if (DuplicationCheck_OutPut(this.Search_SOCS.Rows[i]["TABLE_NAME"].ToString(), this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"].ToString()))
                        {
                        }
                        else
                        {
                            MaxPriorityNo = this.GetMaxPriorityNo() + 1;

                            this.form.customDataGridView1.Rows.Add();

                            //表示項目名（項目論理名）
                            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_RONRI_NAME"].Value = this.Search_SOCS.Rows[i]["DISP_KOUMOKU_NAME"];

                            //並び順
                            this.form.customDataGridView1.Rows[k + j].Cells["SORT_NO"].Value = 1;

                            //優先順
                            this.form.customDataGridView1.Rows[k + j].Cells["PRIORITY_NO"].Value = MaxPriorityNo;

                            //必須区分：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["HISSU_KBN"].Value = this.Search_SOCS.Rows[i]["HISSU_KBN"];

                            //テーブル名：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["TABLE_NAME"].Value = this.Search_SOCS.Rows[i]["TABLE_NAME"];

                            //項目物理名：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["KOUMOKU_BUTSURI_NAME"].Value = this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                            //タイムスタンプ（MOPC）：非表示
                            this.form.customDataGridView1.Rows[k + j].Cells["TIME_STAMP_MOPC"].Value = "";
                        }
                        j += 1;

                        break;

                    case false:
                        this.form.customDataGridView2.Rows.Add();

                        //表示項目名（項目論理名）
                        this.form.customDataGridView2.Rows[i - j].Cells["KOUMOKU_RONRI_NAME_SE"].Value = this.Search_SOCS.Rows[i]["DISP_KOUMOKU_NAME"];

                        //必須区分：非表示
                        this.form.customDataGridView2.Rows[i - j].Cells["HISSU_KBN_SE"].Value = this.Search_SOCS.Rows[i]["HISSU_KBN"];

                        //テーブル名：非表示
                        this.form.customDataGridView2.Rows[i - j].Cells["TABLE_NAME_SE"].Value = this.Search_SOCS.Rows[i]["TABLE_NAME"];

                        //項目物理名：非表示
                        this.form.customDataGridView2.Rows[i - j].Cells["KOUMOKU_BUTSURI_NAME_SE"].Value = this.Search_SOCS.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                        //タイムスタンプ（MOPC）：非表示
                        this.form.customDataGridView2.Rows[i - j].Cells["TIME_STAMP_MOPC_SE"].Value = "";

                        break;
                }
            }

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

            DataTable Search_MOP = new DataTable();
            //Search_MOP.Columns.Add("SYSTEM_ID", Type.GetType("System.Int62"));
            //Search_MOP.Columns.Add("SEQ", Type.GetType("System.Int32"));
            //Search_MOP.Columns.Add("DENSHU_KBN_CD", Type.GetType("System.Int16"));
            //Search_MOP.Columns.Add("OUTPUT_KBN", Type.GetType("System.Int16"));
            //Search_MOP.Columns.Add("PATTERN_NAME");
            //Search_MOP.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.Int64"));
            //Search_MOP.Columns.Add("TABLE_NAME");
            //Search_MOP.Columns.Add("KOUMOKU_RONRI_NAME");
            //Search_MOP.Columns.Add("KOUMOKU_BUTSURI_NAME");
            //Search_MOP.Columns.Add("SORT_NO", Type.GetType("System.Int16"));
            //Search_MOP.Columns.Add("PRIORITY_NO", Type.GetType("System.Int16"));
            //Search_MOP.Columns.Add("HISSU_KBN", Type.GetType("System.Boolean"));
            //Search_MOP.Columns.Add("TIME_STAMP_MOP");
            //Search_MOP.Columns.Add("TIME_STAMP_MOPC");

            this.Search_MOP = dao_GetMOP.GetDataForEntity(data);

            LogUtility.DebugMethodEnd(SYSTEM_ID, SEQ, DENSHU_KBN_CD, OUTPUT_KBN, DETAIL_SYSTEM_ID, DELETE_FLG);

            //取得件数
            int count = this.Search_MOP.Rows.Count;
            return count;
        }

        /// <summary>
        /// 出力項目　表示
        /// </summary>
        public void SetResOutPut()
        {
            LogUtility.DebugMethodStart();
            
            //出力項目
            int SortNo = 0;
            int PriorityNo = 0;
            for (int i = 0; i < this.Search_MOP.Rows.Count; i++)
            {
                this.form.customDataGridView1.Rows.Add();

                //表示項目名（項目論理名）
                this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_RONRI_NAME"].Value = this.Search_MOP.Rows[i]["KOUMOKU_RONRI_NAME"];

                //並び順
                SortNo = (Int16)this.Search_MOP.Rows[i]["SORT_NO"];
                this.form.customDataGridView1.Rows[i].Cells["SORT_NO"].Value = SortNo != 1 && SortNo != 2 ? 1 : SortNo;

                //優先順
                PriorityNo = (Int16)this.Search_MOP.Rows[i]["PRIORITY_NO"];
                this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value = PriorityNo;

                //必須区分：非表示
                this.form.customDataGridView1.Rows[i].Cells["HISSU_KBN"].Value = this.Search_MOP.Rows[i]["HISSU_KBN"];

                //テーブル名：非表示
                this.form.customDataGridView1.Rows[i].Cells["TABLE_NAME"].Value = this.Search_MOP.Rows[i]["TABLE_NAME"];

                //項目物理名：非表示
                this.form.customDataGridView1.Rows[i].Cells["KOUMOKU_BUTSURI_NAME"].Value = this.Search_MOP.Rows[i]["KOUMOKU_BUTSURI_NAME"];

                //タイムスタンプ（MOPC）：非表示
                this.form.customDataGridView1.Rows[i].Cells["TIME_STAMP_MOPC"].Value = this.Search_MOP.Rows[i]["TIME_STAMP_MOPC"];

            }
            
            LogUtility.DebugMethodEnd();
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
                        switch (Boolean.Parse(this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value.ToString()))
                        {
                            case false:
                                //選択項目 追加
                                this.form.customDataGridView2.Rows.Add
                                    ( this.form.customDataGridView1.Rows[j].Cells["KOUMOKU_RONRI_NAME"].Value
                                    , this.form.customDataGridView1.Rows[j].Cells["HISSU_KBN"].Value
                                    , this.form.customDataGridView1.Rows[j].Cells["TABLE_NAME"].Value
                                    , this.form.customDataGridView1.Rows[j].Cells["KOUMOKU_BUTSURI_NAME"].Value
                                    , this.form.customDataGridView1.Rows[j].Cells["TIME_STAMP_MOPC"].Value
                                    );

                                //出力項目 削除
                                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.SelectedCells[i - 1].RowIndex);

                                //選択項目　画面表示処理
                                this.form.customDataGridView2.CurrentCell = this.form.customDataGridView2.Rows[0].Cells[0];
                                this.form.customDataGridView2.Focus();
                            
                                break;

                            case true:
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("I006");
                                break;
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
                            , this.form.customDataGridView2.Rows[j].Cells["TABLE_NAME_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["KOUMOKU_BUTSURI_NAME_SE"].Value
                            , this.form.customDataGridView2.Rows[j].Cells["TIME_STAMP_MOPC_SE"].Value
                            );

                        //選択項目 削除
                        this.form.customDataGridView2.Rows.RemoveAt(this.form.customDataGridView2.SelectedCells[i - 1].RowIndex);
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

                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("I001", "登録");

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

            var messageShowLogic = new MessageBoxShowLogic();

            //登録データチェック
            if (this.form.customDataGridView1.RowCount == 0)
            {
                messageShowLogic.MessageBoxShow("E046", "項目が未選択","一覧出力項目の");
                
                //PTバグトラブル管理表 No.790対応
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

            //優先度 入力チェック
            for (int i = 0; i < this.form.customDataGridView1.RowCount; i++)
            {
                if (this.form.customDataGridView1.Rows[i].Cells["PRIORITY_NO"].Value.ToString() == "")
                {
                    messageShowLogic.MessageBoxShow("E001", "優先度");
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
                        messageShowLogic.MessageBoxShow("E031", "優先度");
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
                        // DETAIL_STSTEM_IDは同じSYSTEM_ID, SEQ内で連番とする
                        //mopc.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(SqlInt16.Parse(this.form.denshuKB.ToString()));
                        mopc.DETAIL_SYSTEM_ID = i;
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
            }

            //一覧出力項目詳細
            this.GetSyutsuryokuKoumokuData("InsMOPC");
            if (MopcList != null && MopcList.Count() > 0)
            {
                foreach (M_OUTPUT_PATTERN_COLUMN mopc in MopcList)
                {
                    int CntMopcIns = dao_SetMOPC.Insert(mopc);
                }
            }

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

    }
}