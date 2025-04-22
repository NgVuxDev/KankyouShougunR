using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Drawing;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.ContenaShitei.Utility;
using Shougun.Core.Common.ContenaShitei.DTO;
using Shougun.Core.Common.ContenaShitei.DAO;
using System.Linq;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Common.ContenaShitei
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
        private string ButtonInfoXmlPath = "Shougun.Core.Common.ContenaShitei.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        public IM_CONTENA_SHURUIDao dao_Getcontenashurui;

        /// <summary>
        /// DAO
        /// </summary>
        public IM_CONTENADao dao_Getcontena;


        /// <summary>
        /// DTO
        /// </summary>
        private CNTSHIDtoCls dto_contana_shitei;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BasePopForm parentForm;

        private DBAccessor CommonDBAccessor;

        private bool DispInit;  // 表示時にデータがある場合はTrue

        /// <summary>
        /// M_SYS_INFO.CONTENA_KANRI_HOUHOU(true:数量管理)
        /// </summary>
        internal bool isSuuryouKanri;

        internal bool isInputError;
        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果(コンテナ種類)
        /// </summary>
        public DataTable Search_CNT_SRI { get; set; }

        /// <summary>
        /// 検索結果(コンテナ)
        /// </summary>
        public DataTable Search_CNT { get; set; }

        /// <summary>
        /// 受取コンテナ稼動実績リスト
        /// </summary>
        public List<T_CONTENA_RESULT> CntRetList { get; set; }

        /// <summary>
        /// 受取コンテナ稼動予定リスト
        /// </summary>
        public List<T_CONTENA_RESERVE> CntResList { get; set; }

        /// <summary>
        /// 処理モード
        /// </summary>
        public string ShoriMode = "";

        /// <summary>
        /// 呼出画面
        /// </summary>
        public string Yobidashi = "";

        /// <summary>
        /// 伝種区分
        /// </summary>
        public String denshuKB = "";

        /// <summary>
        /// システムID
        /// </summary>
        public String systemID = "";

        /// <summary>
        /// 枝番
        /// </summary>
        public String seq = "";

        /// <summary>
        /// 起動元画面の伝票日付(または作業日)
        /// 設置日、引揚日の比較に使用する
        /// </summary>
        internal string denpyouDate { get; set; }

        /// <summary>起動元画面の業者CD</summary>
        internal string GyoushaCd { get; set; }

        /// <summary>起動元画面の現場CD</summary>
        internal string GenbaCd { get; set; }

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
                this.dto_contana_shitei = new CNTSHIDtoCls();

                //DAO
                dao_Getcontenashurui = DaoInitUtility.GetComponent<IM_CONTENA_SHURUIDao>();
                dao_Getcontena = DaoInitUtility.GetComponent<IM_CONTENADao>();

                this.CommonDBAccessor = new DBAccessor();

                var sysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
                int contenaKanriHouhou = 1;
                if (sysInfo.Length > 0)
                {
                    contenaKanriHouhou = sysInfo[0].CONTENA_KANRI_HOUHOU.IsNull ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)sysInfo[0].CONTENA_KANRI_HOUHOU;
                }

                this.isSuuryouKanri = (contenaKanriHouhou == CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU);
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

        #region 初期処理

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BasePopForm)this.form.Parent;

                //ヘッダーの初期化
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;
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
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                //var parentForm = (MasterBaseForm)this.form.Parent;
                //var parentForm = (BusinessBaseForm)this.form.Parent;
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BasePopForm)this.form.Parent;
                //交換ボタン(F1)イベント生成
                if (this.isSuuryouKanri)
                {
                    parentForm.bt_func1.Click += new EventHandler(this.bt_func1_Click);
                }
                //実行ボタン(F9)イベント生成
                //this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

                ////閉じるボタン(×)イベント生成
                //parentForm.FormClosed += new FormClosedEventHandler(this.form.parentForm_FormClosed);
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

        #region コンテナ管理方法による表示の切り替え
        /// <summary>数量管理で表示するカラム</summary>
        private string[] suuryouKanriSecchiColumn = { "DAISUU_CNT"};
        private string[] suuryouKanriHikiageColumn = { "DAISUU_CNT_HIKIAGE" };

        /// <summary>個体管理</summary>
        private string[] kotaiKanriSecchiColumn = { "CONTENA_CD", "CONTENA_NAME_RYAKU"};
        private string[] kotaiKanriHikiageColumn = { "CONTENA_CD_HIKIAGE", "CONTENA_NAME_RYAKU_HIKIAGE"};

        /// <summary>
        /// コンテナ管理方法による表示の切り替え
        /// </summary>
        private void ChangeVisibleForContenaKanri()
        {
            /**
             * 明細欄
             */

            // 下記以外のカラムについては常に非表示
            // 数量管理用カラム(設置用)
            foreach (var suuryouCol in suuryouKanriSecchiColumn)
            {
                this.form.customDataGridViewSechi.Columns[suuryouCol].Visible = this.isSuuryouKanri;
            }
            // 数量管理用カラム(引揚用)
            foreach (var suuryouCol in suuryouKanriHikiageColumn)
            {
                this.form.customDataGridViewHikiage.Columns[suuryouCol].Visible = this.isSuuryouKanri;
            }

            // 個体管理用カラム(設置用)
            foreach (var kotaiCol in kotaiKanriSecchiColumn)
            {
                this.form.customDataGridViewSechi.Columns[kotaiCol].Visible = !this.isSuuryouKanri;
            }
            // 個体管理用カラム(引揚用)
            foreach (var kotaiCol in kotaiKanriHikiageColumn)
            {
                this.form.customDataGridViewHikiage.Columns[kotaiCol].Visible = !this.isSuuryouKanri;
            }

            if (!this.isSuuryouKanri)
            {
                /**
                 * フッダーボタン
                 */
                var parentForm = (BasePopForm)this.form.Parent;

                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func1.Text = string.Empty;
                parentForm.bt_func1.Tag = string.Empty;
            }

        }
        #endregion

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.C_CONTENA_SHITEI);

                // コンテナ管理方法によって画面の表示項目を切り替える
                this.ChangeVisibleForContenaKanri();
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

        #region 画面デートの検索とセット処理

        /// <summary>
        /// 設置と引揚GridViewのデータの検索とセット（1:受入、売上支払、2:出荷の場合）
        /// </summary>
        public void SearchContenaResult()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<CNTSHIDtoCls> SeichiList = new List<CNTSHIDtoCls>();
                List<CNTSHIDtoCls> HikiageList = new List<CNTSHIDtoCls>();

                if (CntRetList == null)
                    return;
                //設置と引揚区分のデートを分ける。
                foreach (T_CONTENA_RESULT conret in CntRetList)
                {
                    //設置区分
                    if (conret.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        //コンテナ種類CDによって、コンテナ種類名を取得
                        CNTSHIDtoCls seichi = new CNTSHIDtoCls();                                              //コンテナ指定一覧
                        M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();                                      //コンテナ種類
                        M_CONTENA con = new M_CONTENA();
                        seichi.CONTENA_SHURUI_CD = conret.CONTENA_SHURUI_CD;                                   //コンテナ種類CD
                        conshu = dao_Getcontenashurui.GetDataByCd(seichi.CONTENA_SHURUI_CD);
                        if (conshu != null)
                        {
                            seichi.CONTENA_SHURUI_NAME_RYAKU = conshu.CONTENA_SHURUI_NAME_RYAKU;               //コンテナ種類名
                        }

                        //コンテナCDによって、コンテナ名を取得
                        seichi.CONTENA_CD = conret.CONTENA_CD;                                                 //コンテナCD   
                        M_CONTENA data = new M_CONTENA();
                        data.CONTENA_CD = seichi.CONTENA_CD;
                        data.CONTENA_SHURUI_CD = seichi.CONTENA_SHURUI_CD;
                        con = dao_Getcontena.GetDataByCd(data);
                        if (con != null)
                        {
                            seichi.CONTENA_NAME_RYAKU = con.CONTENA_NAME_RYAKU;      　　　　　　　　　　　　　//コンテナ名
                        }
                        seichi.DAISUU_CNT = this.ChgDBNullToValue(conret.DAISUU_CNT, string.Empty).ToString(); //台数

                        if (conret.TIME_STAMP != null)
                        {
                            seichi.TIME_STAMP = System.Text.Encoding.Default.GetString(conret.TIME_STAMP);     //TIME_STAMP
                        }

                        //削除フラグ初期値設定
                        //コンテナ種類コードは空白または台数コントは0の場合は削除フラグを１に立てる
                        if (string.IsNullOrEmpty(conret.CONTENA_SHURUI_CD) ||
                            (conret.DAISUU_CNT.IsNull || conret.DAISUU_CNT == 0))
                            seichi.DELETE_FLG = "true";
                        else
                            seichi.DELETE_FLG = "false";

                        SeichiList.Add(seichi);

                    }
                    //引揚区分
                    else if (conret.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        //コンテナ種類CDによって、コンテナ種類名を取得
                        CNTSHIDtoCls hikiage = new CNTSHIDtoCls();
                        M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();　　　　　　　　　　　　　　　　　　　    //コンテナ指定一覧
                        M_CONTENA con = new M_CONTENA();　　　　　　　　　　　　　　　　　　　　　　　　　　       //コンテナ種類
                        hikiage.CONTENA_SHURUI_CD = conret.CONTENA_SHURUI_CD;                                      //コンテナ種類CD
                        conshu = dao_Getcontenashurui.GetDataByCd(hikiage.CONTENA_SHURUI_CD);
                        if (conshu != null)
                        {
                            hikiage.CONTENA_SHURUI_NAME_RYAKU =
                            dao_Getcontenashurui.GetDataByCd(hikiage.CONTENA_SHURUI_CD).CONTENA_SHURUI_NAME_RYAKU; //コンテナ種類名
                        }

                        //コンテナCDによって、コンテナ名を取得
                        hikiage.CONTENA_CD = conret.CONTENA_CD;                                                    //コンテナCD     
                        M_CONTENA data = new M_CONTENA();                                                          //コンテナ種類
                        data.CONTENA_CD = hikiage.CONTENA_CD;
                        data.CONTENA_SHURUI_CD = hikiage.CONTENA_SHURUI_CD;
                        con = dao_Getcontena.GetDataByCd(data);
                        if (con != null)
                        {
                            hikiage.CONTENA_NAME_RYAKU = con.CONTENA_NAME_RYAKU;                                   //コンテナ名
                        }
                        hikiage.DAISUU_CNT = this.ChgDBNullToValue(conret.DAISUU_CNT, string.Empty).ToString();    //台数

                        if (conret.TIME_STAMP != null)
                        {
                            hikiage.TIME_STAMP = System.Text.Encoding.Default.GetString(conret.TIME_STAMP);             //TIME_STAMP
                        }
                        //削除フラグ初期値設定
                        //コンテナ種類コードは空白または台数コントは0の場合は削除フラグを１に立てる
                        if (string.IsNullOrEmpty(conret.CONTENA_SHURUI_CD) ||
                            (conret.DAISUU_CNT.IsNull || conret.DAISUU_CNT == 0))
                            hikiage.DELETE_FLG = "true";
                        else
                            hikiage.DELETE_FLG = "false";
                        HikiageList.Add(hikiage);

                    }
                }

                //設置区分検索結果設定
                for (int i = 0; i < SeichiList.Count; i++)
                {
                    this.form.customDataGridViewSechi.Rows.Add();
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value = SeichiList[i].CONTENA_SHURUI_CD;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value = SeichiList[i].CONTENA_SHURUI_NAME_RYAKU;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value = SeichiList[i].CONTENA_CD;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_NAME_RYAKU"].Value = SeichiList[i].CONTENA_NAME_RYAKU;
                    this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value = SeichiList[i].DAISUU_CNT;
                    this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value = SeichiList[i].DELETE_FLG;
                    this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value = SeichiList[i].TIME_STAMP;

                }
                //引揚区分検索結果設定
                for (int i = 0; i < HikiageList.Count; i++)
                {
                    this.form.customDataGridViewHikiage.Rows.Add();
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value = HikiageList[i].CONTENA_SHURUI_CD;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value = HikiageList[i].CONTENA_SHURUI_NAME_RYAKU;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value = HikiageList[i].CONTENA_CD;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_NAME_RYAKU_HIKIAGE"].Value = HikiageList[i].CONTENA_NAME_RYAKU;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value = HikiageList[i].DAISUU_CNT;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value = HikiageList[i].DELETE_FLG;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value = HikiageList[i].TIME_STAMP;

                }
                // 画面表示時の表示データ有無を確認
                if (CntRetList.Count > 0)
                    DispInit = true;
                else
                    DispInit = false;
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
        /// 設置と引揚GridViewのデータの検索とセット（3:受付）
        /// </summary>
        public void SearchContenaReserve()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<CNTSHIDtoCls> SeichiList = new List<CNTSHIDtoCls>();
                List<CNTSHIDtoCls> HikiageList = new List<CNTSHIDtoCls>();

                if (CntResList == null)
                    return;
                //設置と引揚区分のデートを分ける。
                foreach (T_CONTENA_RESERVE conret in CntResList)
                {
                    //設置区分
                    if (conret.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI)
                    {
                        //コンテナ種類CDによって、コンテナ種類名を取得
                        CNTSHIDtoCls seichi = new CNTSHIDtoCls();                                              //コンテナ指定一覧
                        M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();                                      //コンテナ種類
                        M_CONTENA con = new M_CONTENA();
                        seichi.CONTENA_SHURUI_CD = conret.CONTENA_SHURUI_CD;                                   //コンテナ種類CD
                        conshu = dao_Getcontenashurui.GetDataByCd(seichi.CONTENA_SHURUI_CD);
                        if (conshu != null)
                        {
                            seichi.CONTENA_SHURUI_NAME_RYAKU = conshu.CONTENA_SHURUI_NAME_RYAKU;               //コンテナ種類名
                        }

                        //コンテナCDによって、コンテナ名を取得
                        seichi.CONTENA_CD = conret.CONTENA_CD;                                                 //コンテナCD   
                        M_CONTENA data = new M_CONTENA();
                        data.CONTENA_CD = seichi.CONTENA_CD;
                        data.CONTENA_SHURUI_CD = seichi.CONTENA_SHURUI_CD;
                        con = dao_Getcontena.GetDataByCd(data);
                        if (con != null)
                        {
                            seichi.CONTENA_NAME_RYAKU = con.CONTENA_NAME_RYAKU;      　　　　　　　　　　　　　//コンテナ名
                        }
                        seichi.DAISUU_CNT = this.ChgDBNullToValue(conret.DAISUU_CNT, string.Empty).ToString(); //台数
                        if (conret.TIME_STAMP != null)
                        {
                            seichi.TIME_STAMP = System.Text.Encoding.Default.GetString(conret.TIME_STAMP);     //TIME_STAMP
                        }
                        //削除フラグ初期値設定
                        //コンテナ種類コードは空白または台数コントは0の場合は削除フラグを１に立てる
                        if (string.IsNullOrEmpty(conret.CONTENA_SHURUI_CD) ||
                            (conret.DAISUU_CNT.IsNull || conret.DAISUU_CNT == 0))
                            seichi.DELETE_FLG = "true";
                        else
                            seichi.DELETE_FLG = "false";

                        SeichiList.Add(seichi);

                    }
                    //引揚区分
                    else if (conret.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE)
                    {
                        //コンテナ種類CDによって、コンテナ種類名を取得
                        CNTSHIDtoCls hikiage = new CNTSHIDtoCls();
                        M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();　　　　　　　　　　　　　　　　　　　    //コンテナ指定一覧
                        M_CONTENA con = new M_CONTENA();　　　　　　　　　　　　　　　　　　　　　　　　　　       //コンテナ種類
                        hikiage.CONTENA_SHURUI_CD = conret.CONTENA_SHURUI_CD;                                      //コンテナ種類CD
                        conshu = dao_Getcontenashurui.GetDataByCd(hikiage.CONTENA_SHURUI_CD);
                        if (conshu != null)
                        {
                            hikiage.CONTENA_SHURUI_NAME_RYAKU =
                            dao_Getcontenashurui.GetDataByCd(hikiage.CONTENA_SHURUI_CD).CONTENA_SHURUI_NAME_RYAKU; //コンテナ種類名
                        }

                        //コンテナCDによって、コンテナ名を取得
                        hikiage.CONTENA_CD = conret.CONTENA_CD;                                                    //コンテナCD     
                        M_CONTENA data = new M_CONTENA();                                                          //コンテナ種類
                        data.CONTENA_CD = hikiage.CONTENA_CD;
                        data.CONTENA_SHURUI_CD = hikiage.CONTENA_SHURUI_CD;
                        con = dao_Getcontena.GetDataByCd(data);
                        if (con != null)
                        {
                            hikiage.CONTENA_NAME_RYAKU = con.CONTENA_NAME_RYAKU;                                   //コンテナ名
                        }
                        hikiage.DAISUU_CNT = this.ChgDBNullToValue(conret.DAISUU_CNT, string.Empty).ToString();    //台数
                        if (conret.TIME_STAMP != null)
                        {
                            hikiage.TIME_STAMP = System.Text.Encoding.Default.GetString(conret.TIME_STAMP);        //TIME_STAMP
                        }
                        //削除フラグ初期値設定
                        //コンテナ種類コードは空白または台数コントは0の場合は削除フラグを１に立てる
                        if (string.IsNullOrEmpty(conret.CONTENA_SHURUI_CD) ||
                            (conret.DAISUU_CNT.IsNull || conret.DAISUU_CNT == 0))
                            hikiage.DELETE_FLG = "true";
                        else
                            hikiage.DELETE_FLG = "false";
                        HikiageList.Add(hikiage);

                    }
                }

                //設置区分検索結果設定
                for (int i = 0; i < SeichiList.Count; i++)
                {
                    this.form.customDataGridViewSechi.Rows.Add();
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value = SeichiList[i].CONTENA_SHURUI_CD;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value = SeichiList[i].CONTENA_SHURUI_NAME_RYAKU;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value = SeichiList[i].CONTENA_CD;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_NAME_RYAKU"].Value = SeichiList[i].CONTENA_NAME_RYAKU;
                    this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value = SeichiList[i].DAISUU_CNT;
                    this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value = SeichiList[i].DELETE_FLG;
                    this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value = SeichiList[i].TIME_STAMP;

                }
                //引揚区分検索結果設定
                for (int i = 0; i < HikiageList.Count; i++)
                {
                    this.form.customDataGridViewHikiage.Rows.Add();
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value = HikiageList[i].CONTENA_SHURUI_CD;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value = HikiageList[i].CONTENA_SHURUI_NAME_RYAKU;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value = HikiageList[i].CONTENA_CD;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_NAME_RYAKU_HIKIAGE"].Value = HikiageList[i].CONTENA_NAME_RYAKU;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value = HikiageList[i].DAISUU_CNT;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value = HikiageList[i].DELETE_FLG;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value = HikiageList[i].TIME_STAMP;
                }
                // 画面表示時の表示データ有無を確認
                if (CntResList.Count > 0)
                    DispInit = true;
                else
                    DispInit = false;
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

        #region 台数セル制御
        /// <summary>
        /// 台数セル制御
        /// </summary>
        /// <param name="row"></param>
        /// <param name="isSechi"></param>
        internal void DaisuuCntCellCtrl(DataGridViewRow row, bool isSechi)
        {
            if (row == null)
            {
                return;
            }

            string daisuuCnt = (isSechi) ? "DAISUU_CNT" : "DAISUU_CNT_HIKIAGE";

            if (IsFixedDaisuuCntCell(row, isSechi))
            {
                // 種類、名称入力時は台数は１固定
                row.Cells[daisuuCnt].ReadOnly = true;
                row.Cells[daisuuCnt].Value = "1";
            }
            else
            {
                row.Cells[daisuuCnt].ReadOnly = false;
            }

            // No.3342-->
            if (isSechi == true)
            {
                DeleteFlagSetei(row); 
            }
            else
            {
                DeleteFlagHikiage(row);
            }
            // No.3342<--
        }
        #endregion

        #region
        /// <summary>
        /// 台数セルが読取専用(台数1固定)か判定
        /// </summary>
        /// <param name="row"></param>
        /// <param name="isSechi"></param>
        /// <returns></returns>
        private bool IsFixedDaisuuCntCell(DataGridViewRow row, bool isSechi)
        {
            if (row == null)
            {
                return false;
            }

            // セル名称の設定
            string contenaShuruiCd = (isSechi) ? "CONTENA_SHURUI_CD" : "CONTENA_SHURUI_CD_HIKIAGE";
            string contenaShuruiNameRyaku = (isSechi) ? "CONTENA_SHURUI_NAME_RYAKU" : "CONTENA_SHURUI_NAME_RYAKU_HIKIAGE";
            string contenaCd = (isSechi) ? "CONTENA_CD" : "CONTENA_CD_HIKIAGE";
            string contenaNameRyaku = (isSechi) ? "CONTENA_NAME_RYAKU" : "CONTENA_NAME_RYAKU_HIKIAGE";

            if ((null != row.Cells[contenaShuruiCd].Value && !string.IsNullOrEmpty(row.Cells[contenaShuruiCd].Value.ToString()))
                && (null != row.Cells[contenaShuruiNameRyaku].Value && !string.IsNullOrEmpty(row.Cells[contenaShuruiNameRyaku].Value.ToString()))
                && (null != row.Cells[contenaCd].Value && !string.IsNullOrEmpty(row.Cells[contenaCd].Value.ToString()))
                && (null != row.Cells[contenaNameRyaku].Value && !string.IsNullOrEmpty(row.Cells[contenaNameRyaku].Value.ToString()))
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region F9使用可か不可か
        /// <summary>
        /// F9使用可か不可か
        /// </summary>
        internal void F9SiyouKaFuka()
        {
            LogUtility.DebugMethodStart();
            try
            {
                bool flg = false;
                bool nyuuryokuFlg = true;

                for (int i = 0; i < this.form.customDataGridViewSechi.RowCount - 1; i++)
                {
                    if (((null == this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                            || (null == this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value.ToString()))
                            || (null == this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString()))
                            || (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Visible && (null == this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value
                            || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString()))))
                        && (!((null == this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                            && (null == this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString()))))
                        )
                    {
                        nyuuryokuFlg = false;
                        break;
                    }

                }

                for (int i = 0; i < this.form.customDataGridViewHikiage.RowCount - 1; i++)
                {
                    if (((null == this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                            || (null == this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value.ToString()))
                            || (null == this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                            || (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Visible && (null == this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value
                            || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString()))))
                        && (!((null == this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                            && (null == this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString()))))
                        )
                    {
                        nyuuryokuFlg = false;
                        break;
                    }

                }

                if (nyuuryokuFlg && (this.form.customDataGridViewSechi.RowCount > 1 || this.form.customDataGridViewHikiage.RowCount > 1))
                    flg = true;

                //F9使用可か
                var parentForm = (BasePopForm)this.form.Parent;
                if (flg)
                {
                    parentForm.bt_func9.Enabled = true;
                }
                else
                {
                    parentForm.bt_func9.Enabled = false;
                }
                if (ShoriMode == "2")
                {
                    parentForm.bt_func9.Enabled = false;
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

        #region 画面クリア
        /// <summary>
        /// 画面クリア
        /// </summary>
        public void ClearScreen()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //設置
                for (int i = this.form.customDataGridViewSechi.RowCount - 1; i > 0; i--)
                {
                    this.form.customDataGridViewSechi.Rows.RemoveAt(i - 1);
                }

                //引揚
                for (int j = this.form.customDataGridViewHikiage.RowCount - 1; j > 0; j--)
                {
                    this.form.customDataGridViewHikiage.Rows.RemoveAt(j - 1);
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

        #region 処理モードを設定する。
        /// <summary>
        /// 処理モードを設定する。
        /// </summary>
        internal void SetMode()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //1:通常
                if (ShoriMode == "1")
                {
                    var parentForm = (BasePopForm)this.form.Parent;
                    parentForm.bt_func9.Enabled = true;
                    //this.form.customDataGridViewSechi.AllowUserToAddRows = true;
                    //this.form.customDataGridViewHikiage.AllowUserToAddRows = true;
                    this.form.customDataGridViewSechi.ReadOnly = false;
                    this.form.customDataGridViewHikiage.ReadOnly = false;
                }
                //2:入力不可
                if (ShoriMode == "2")
                {
                    var parentForm = (BasePopForm)this.form.Parent;
                    parentForm.bt_func9.Enabled = false;
                    //this.form.customDataGridViewSechi.AllowUserToAddRows = false;
                    //this.form.customDataGridViewHikiage.AllowUserToAddRows = false;
                    this.form.customDataGridViewSechi.ReadOnly = true;
                    this.form.customDataGridViewHikiage.ReadOnly = true;
                    this.form.customDataGridViewSechi.CellValidated -= this.form.customDataGridViewSechi_CellValidated;
                    this.form.customDataGridViewHikiage.CellValidated -= this.form.customDataGridViewHikiage_CellValidated;
                    this.form.customDataGridViewSechi.CellValidating -= this.form.customDataGridViewSechi_CellValidating;
                    this.form.customDataGridViewHikiage.CellValidating -= this.form.customDataGridViewHikiage_CellValidating;
                    this.form.customDataGridViewSechi.CellEnter -= this.form.customDataGridViewSechi_CellEnter;
                    this.form.customDataGridViewHikiage.CellEnter -= this.form.customDataGridViewHikiage_CellEnter;
                    this.form.customDataGridViewSechi.MouseClick -= this.form.customDataGridViewSechi_MouseClick;
                    this.form.customDataGridViewHikiage.MouseClick -= this.form.customDataGridViewHikiage_MouseClick;
                    parentForm.bt_func1.Enabled = false;
                    // 設定Detailのコントロールを制御
                    for (int k = 0; k < this.form.customDataGridViewSechi.Rows.Count; k++)
                    {
                        for (int i = 0; i < this.form.customDataGridViewSechi.Columns.Count; i++)
                        {
                            //コンテナ種類CDまたはコンテナCD
                            if (i == 0 || i == 2)
                            {
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewSechi.Columns[i]).FocusOutCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewSechi.Columns[i]).RegistCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewSechi.Columns[i]).FocusOutCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewSechi.Columns[i]).RegistCheckMethod = null;
                                this.form.customDataGridViewSechi.Rows[k].Cells[i].ReadOnly = true;
                            }

                        }

                    }

                    // 引揚Detailのコントロールを制御
                    for (int k = 0; k < this.form.customDataGridViewHikiage.Rows.Count; k++)
                    {
                        for (int i = 0; i < this.form.customDataGridViewHikiage.Columns.Count; i++)
                        {
                            //コンテナ種類CDまたはコンテナCD
                            if (i == 0 || i == 2)
                            {
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewHikiage.Columns[i]).FocusOutCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewHikiage.Columns[i]).RegistCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewHikiage.Columns[i]).FocusOutCheckMethod = null;
                                ((DgvCustomTextBoxColumn)this.form.customDataGridViewHikiage.Columns[i]).RegistCheckMethod = null;
                                this.form.customDataGridViewHikiage.Rows[k].Cells[i].ReadOnly = true;
                            }
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

        #endregion

        #region 業務処理

        #region 実行処理（F9）

        /// <summary>
        /// 実行処理
        /// </summary>
        [Transaction]
        public void Jikou()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //設置合計と引揚合計作成
                RetSechiHikiageGoukeiSakusei();
                if (this.Yobidashi.Equals("1")
                   || this.Yobidashi.Equals("2"))
                {
                    //1:受入、売上支払、2:出荷の場合
                    //戻りコンテナ稼働実績Entityリスト作成
                    RetContenaResultListSakusei();
                }
                else if (this.Yobidashi.Equals("3"))
                {

                    //3:受付の場合
                    //戻りコンテナ稼働予定Entityリスト作成
                    RetContenaReserveListSakusei();
                }
                //ダイアログClose処理
                var parentForm = (BasePopForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
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

        #region 終了処理（F12）

        /// <summary>
        /// 終了する。
        /// </summary>
        public void FormClose()
        {
            try
            {
                //var parentForm = (BusinessBaseForm)this.Parent;
                var parentForm = (BasePopForm)this.form.Parent;
                //クローズの場合、戻り値nullに設定する。
                if (this.Yobidashi.Equals("1")
                   || this.Yobidashi.Equals("2"))
                {
                    //1:受入、売上支払、2:出荷の場合
                    this.form.RetCntRetList = null;
                }
                else if (this.Yobidashi.Equals("3"))
                {

                    //3:受付の場合
                    this.form.RetCntResList = null;
                }
                this.form.SeichiGoukei = 0;
                this.form.HikiageGoukei = 0;
                this.ClearScreen();

                this.form.Close();
                parentForm.Close();
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

        #region 戻りコンテナ稼働実績Entityリスト作成
        /// <summary>
        /// 戻りコンテナ稼働実績Entityリスト作成
        /// </summary>
        public void RetContenaResultListSakusei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.RetCntRetList = new List<T_CONTENA_RESULT>();                               //返却コンテナ稼動実績リスト
                //設置区分検索結果設定
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    T_CONTENA_RESULT cntRet = new T_CONTENA_RESULT();
                    //伝種区分CD
                    if (!string.IsNullOrEmpty(denshuKB))
                    {
                        cntRet.DENSHU_KBN_CD = SqlInt16.Parse(denshuKB);
                    }
                    //cntRet.DENSHU_KBN_CD = 1;
                    //システムID
                    if (!string.IsNullOrEmpty(systemID))
                    {
                        cntRet.SYSTEM_ID = SqlInt64.Parse(systemID);
                    }
                    //枝番
                    if (!string.IsNullOrEmpty(seq))
                    {
                        cntRet.SEQ = SqlInt32.Parse(seq);
                    }
                    //コンテナセット区分
                    cntRet.CONTENA_SET_KBN = CommonConst.CONTENA_SET_KBN_SECCHI;
                    //コンテナ種類コード
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value != null)
                    {
                        cntRet.CONTENA_SHURUI_CD = this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString();
                    }
                    //コンテナコード
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value != null)
                    {
                        cntRet.CONTENA_CD = this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString();
                    }
                    //台数コント
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString()))
                        {
                            cntRet.DAISUU_CNT = SqlInt16.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString());
                        }

                    }
                    //削除フラグ
                    cntRet.DELETE_FLG = SqlBoolean.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value.ToString());

                    //TIME_STAMP
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value != null)
                    {
                        cntRet.TIME_STAMP = System.Text.Encoding.Default.GetBytes(this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value.ToString());
                    }
                    this.form.RetCntRetList.Add(cntRet);

                }
                //引揚区分検索結果設定
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    T_CONTENA_RESULT cntRet = new T_CONTENA_RESULT();
                    //伝種区分CD
                    if (!string.IsNullOrEmpty(denshuKB))
                    {
                        cntRet.DENSHU_KBN_CD = SqlInt16.Parse(denshuKB);
                    }
                    //cntRet.DENSHU_KBN_CD = 1;
                    //システムコード
                    if (!string.IsNullOrEmpty(systemID))
                    {
                        cntRet.SYSTEM_ID = SqlInt64.Parse(systemID);
                    }
                    //枝番
                    if (!string.IsNullOrEmpty(seq))
                    {
                        cntRet.SEQ = SqlInt32.Parse(seq);
                    }
                    //コンテナセット区分
                    cntRet.CONTENA_SET_KBN = CommonConst.CONTENA_SET_KBN_HIKIAGE;
                    //コンテナ種類コード
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value != null)
                    {
                        cntRet.CONTENA_SHURUI_CD = this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString();
                    }
                    //コンテナコード
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value != null)
                    {
                        cntRet.CONTENA_CD = this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString();
                    }
                    //台数コント
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                        {
                            cntRet.DAISUU_CNT = SqlInt16.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString());
                        }
                    }
                    //削除フラグ
                    cntRet.DELETE_FLG = SqlBoolean.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value.ToString());
                    //TIME_STAMP
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value != null)
                    {
                        cntRet.TIME_STAMP = System.Text.Encoding.Default.GetBytes(this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value.ToString());
                    }
                    this.form.RetCntRetList.Add(cntRet);
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

        #region 戻りコンテナ稼働予定Entityリスト作成
        /// <summary>
        /// 戻りコンテナ稼働予定Entityリスト作成
        /// </summary>
        public void RetContenaReserveListSakusei()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.RetCntResList = new List<T_CONTENA_RESERVE>();                              //返却コンテナ稼動予定リスト
                //設置区分検索結果設定
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    T_CONTENA_RESERVE cntRes = new T_CONTENA_RESERVE();
                    //システムコード
                    if (!string.IsNullOrEmpty(systemID))
                    {
                        cntRes.SYSTEM_ID = SqlInt64.Parse(systemID);
                    }
                    //枝番
                    if (!string.IsNullOrEmpty(seq))
                    {
                        cntRes.SEQ = SqlInt32.Parse(seq);
                    }
                    //コンテナセット区分
                    cntRes.CONTENA_SET_KBN = CommonConst.CONTENA_SET_KBN_SECCHI;
                    //コンテナ種類コード
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value != null)
                    {
                        cntRes.CONTENA_SHURUI_CD = this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString();
                    }
                    //コンテナコード
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value != null)
                    {
                        cntRes.CONTENA_CD = this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString();
                    }
                    //台数コント
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString()))
                        {
                            cntRes.DAISUU_CNT = SqlInt16.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value.ToString());
                        }

                    }
                    //削除フラグ
                    cntRes.DELETE_FLG = SqlBoolean.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value.ToString());
                    //TIME_STAMP
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value != null)
                    {
                        cntRes.TIME_STAMP = System.Text.Encoding.Default.GetBytes(this.form.customDataGridViewSechi.Rows[i].Cells["TIME_STAMP"].Value.ToString());
                    }
                    this.form.RetCntResList.Add(cntRes);

                }
                //引揚区分検索結果設定
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    T_CONTENA_RESERVE cntRes = new T_CONTENA_RESERVE();
                    //システムコード
                    if (!string.IsNullOrEmpty(systemID))
                    {
                        cntRes.SYSTEM_ID = SqlInt64.Parse(systemID);
                    }
                    //枝番
                    if (!string.IsNullOrEmpty(seq))
                    {
                        cntRes.SEQ = SqlInt32.Parse(seq);
                    }
                    //コンテナセット区分
                    cntRes.CONTENA_SET_KBN = CommonConst.CONTENA_SET_KBN_HIKIAGE;
                    //コンテナ種類コード
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value != null)
                    {
                        cntRes.CONTENA_SHURUI_CD = this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString();
                    }
                    //コンテナコード
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value != null)
                    {
                        cntRes.CONTENA_CD = this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString();
                    }
                    //台数コント
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value != null)
                    {
                        if (!string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                        {
                            cntRes.DAISUU_CNT = SqlInt16.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value.ToString());
                        }
                    }
                    //削除フラグ
                    cntRes.DELETE_FLG = SqlBoolean.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value.ToString());
                    //TIME_STAMP
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value != null)
                    {
                        cntRes.TIME_STAMP = System.Text.Encoding.Default.GetBytes(this.form.customDataGridViewHikiage.Rows[i].Cells["TIME_STAMP_HIKIAGE"].Value.ToString());
                    }
                    this.form.RetCntResList.Add(cntRes);
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

        #region 設置合計と引揚合計作成
        /// <summary>
        /// 設置合計と引揚合計作成
        /// </summary>
        public void RetSechiHikiageGoukeiSakusei()
        {
            try
            {
                LogUtility.DebugMethodStart();
                int sechiCount = 0;
                int hikiageCount = 0;

                //設置合計作成
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value != null)
                    {
                        if (!bool.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].
                                Cells["DAISUU_CNT"].Value.ToString()))
                            {
                                sechiCount += Convert.ToInt16(this.form.customDataGridViewSechi.Rows[i].
                                Cells["DAISUU_CNT"].Value.ToString());
                            }
                        }
                    }
                }
                //引揚合計作成
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value != null)
                    {
                        if (!bool.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].
                                Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                            {
                                hikiageCount += Convert.ToInt16(this.form.customDataGridViewHikiage.Rows[i].
                                Cells["DAISUU_CNT_HIKIAGE"].Value.ToString());
                            }
                        }
                    }
                }
                this.form.ctxt_SechiTotal.Text = sechiCount.ToString();
                this.form.ctxt_HikiageTotal.Text = hikiageCount.ToString();
                this.form.SeichiGoukei = sechiCount;      //設置合計
                this.form.HikiageGoukei = hikiageCount;   //引揚合計
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

        #region 台数カラムのチェック処理
        /// <summary>
        /// 台数カラムのチェック処理
        /// </summary>
        public bool DaishuCheck(DataGridViewCell currentcell)
        {
            bool retFlg = true;
            try
            {
                LogUtility.DebugMethodStart(currentcell);
                if (currentcell.Value != null)
                {
                    if (!string.IsNullOrEmpty(currentcell.Value.ToString()))
                    {
                        if (currentcell.ColumnIndex == (int)Shougun.Core.Common.ContenaShitei.UIForm.enumCols.Count)
                        {
                            int i;
                            if (currentcell.Value != null)
                            {
                                if (!int.TryParse(currentcell.Value.ToString(), out i))
                                {
                                    retFlg = false;
                                    return retFlg;
                                }
                                if (Convert.ToDecimal(currentcell.Value.ToString()) <= 0 ||
                                    Convert.ToDecimal(currentcell.Value.ToString()) > 999)
                                {
                                    retFlg = false;
                                    return retFlg;
                                }
                            }
                        }
                    }
                }
                return retFlg;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retFlg);
            }
        }
        #endregion

        #region 台数カラムの累加値チェック処理(設置)
        /// <summary>
        /// 台数カラムの累加値チェック処理(設置)
        /// </summary>
        public bool DaishuRuikaCheckSechi()
        {
            bool retFlg = true;
            try
            {
                int sechiCount = 0;
                //設置合計作成
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["DAISUU_CNT"].Value != null)
                    {
                        if (!bool.Parse(this.form.customDataGridViewSechi.Rows[i].Cells["DELETE_FLG"].Value.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].
                                Cells["DAISUU_CNT"].Value.ToString()))
                            {
                                sechiCount += Convert.ToInt16(this.form.customDataGridViewSechi.Rows[i].
                                Cells["DAISUU_CNT"].Value.ToString());
                                if (sechiCount > 999)
                                {
                                    retFlg = false;
                                    return retFlg;
                                }
                            }
                        }
                    }
                }
                return retFlg;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retFlg);
            }
        }
        #endregion

        #region 台数カラムの累加値チェック処理(引揚)
        /// <summary>
        /// 台数カラムの累加値チェック処理(引揚)
        /// </summary>
        public bool DaishuRuikaCheckHikiage()
        {
            bool retFlg = true;
            try
            {
                int hikiageCount = 0;
                //引揚合計作成
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["DAISUU_CNT_HIKIAGE"].Value != null)
                    {
                        if (!bool.Parse(this.form.customDataGridViewHikiage.Rows[i].Cells["DELETE_FLG_HIKIAGE"].Value.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].
                                Cells["DAISUU_CNT_HIKIAGE"].Value.ToString()))
                            {
                                hikiageCount += Convert.ToInt16(this.form.customDataGridViewHikiage.Rows[i].
                                Cells["DAISUU_CNT_HIKIAGE"].Value.ToString());
                                if (hikiageCount > 999)
                                {
                                    retFlg = false;
                                    return retFlg;
                                }
                            }
                        }
                    }
                }
                return retFlg;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retFlg);
            }
        }
        #endregion

        #region 複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）(設置)
        /// <summary>
        /// 複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）(設置)
        /// </summary>
        public bool MulKeyTanitsuCheckSechi(DataGridViewRow currentrow, int rowindex)
        {
            bool retFlg = true;
            try
            {
                string ContenaShuruiCD = "";
                string ContenaCD = "";
                if (currentrow.Cells[0].Value == null ||
                    currentrow.Cells[2].Value == null ||
                    string.IsNullOrEmpty(currentrow.Cells[0].Value.ToString()) ||
                    string.IsNullOrEmpty(currentrow.Cells[2].Value.ToString()))
                {
                    return retFlg;
                }
                ContenaShuruiCD = currentrow.Cells[0].Value.ToString();
                ContenaCD = currentrow.Cells[2].Value.ToString();


                //設置合計作成
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value != null &&
                       this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value != null)
                    {
                        if (ContenaShuruiCD.Equals(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString())
                            && ContenaCD.Equals(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value.ToString()))
                        {
                            if (rowindex != i)
                            {
                                retFlg = false;
                                return retFlg;
                            }
                        }
                    }
                }
                return retFlg;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retFlg);
            }
        }
        #endregion

        #region 複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）(引揚)
        /// <summary>
        /// 複数キーの一意制約チェック処理（コンテナ種類CD、コンテナCD）(引揚)
        /// </summary>
        public bool MulKeyTanitsuCheckHikiage(DataGridViewRow currentrow, int rowindex)
        {
            bool retFlg = true;
            try
            {
                string ContenaShuruiCD = "";
                string ContenaCD = "";

                if (currentrow.Cells[0].Value == null ||
                   currentrow.Cells[2].Value == null ||
                   string.IsNullOrEmpty(currentrow.Cells[0].Value.ToString()) ||
                   string.IsNullOrEmpty(currentrow.Cells[2].Value.ToString()))
                {
                    return retFlg;
                }
                ContenaShuruiCD = currentrow.Cells[0].Value.ToString();
                ContenaCD = currentrow.Cells[2].Value.ToString();

                ////引揚合計作成
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value != null &&
                        this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value != null)
                    {
                        if (ContenaShuruiCD.Equals(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString())
                            && ContenaCD.Equals(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value.ToString()))
                        {
                            if (rowindex != i)
                            {
                                retFlg = false;
                                return retFlg;
                            }
                        }
                    }
                }
                return retFlg;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retFlg);
            }
        }
        #endregion

        #region 削除フラグの設定処理
        /// <summary>
        /// 削除フラグの設定処理(設置GridView)
        /// </summary>
        /// <param name="targetRow"></param>
        internal void DeleteFlagSetei(DataGridViewRow targetRow)
        {
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return;
                }

                //コンテナ種類コードは空白または台数コントは空白の場合は削除フラグを１に立てる
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["CONTENA_SHURUI_CD"].Value)) ||
                    String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["DAISUU_CNT"].Value)))
                {
                    targetRow.Cells["DELETE_FLG"].Value = true;
                }
                else
                {
                    targetRow.Cells["DELETE_FLG"].Value = false;
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
        /// 削除フラグの設定処理(引揚GridView)
        /// </summary>
        /// <param name="targetRow"></param>
        internal void DeleteFlagHikiage(DataGridViewRow targetRow)
        {
            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return;
                }
                //コンテナ種類コードは空白または台数コントは空白の場合は削除フラグを１に立てる
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value)) ||
                    String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["DAISUU_CNT_HIKIAGE"].Value)))
                {
                    targetRow.Cells["DELETE_FLG_HIKIAGE"].Value = true;
                }
                else
                {
                    targetRow.Cells["DELETE_FLG_HIKIAGE"].Value = false;
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

        #endregion

        #region コンテナ種類チェック(設置)
        /// <summary>
        ///コンテナ種類チェック(設置)
        /// </summary>
        internal bool CheckContenaShuruiSechi(object CONTENA_SHURUI_CD, object CONTENA_CD, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = false;
            try
            {
                LogUtility.DebugMethodStart(CONTENA_SHURUI_CD, CONTENA_CD, e);

                // 入力されてない場合
                if (CONTENA_SHURUI_CD == null || String.IsNullOrEmpty(CONTENA_SHURUI_CD.ToString()) ||
                    CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                {
                    // 処理終了
                    this.form.customDataGridViewSechi["CONTENA_CD", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                    returnval = true;
                    return returnval;
                }

                M_CONTENA con = new M_CONTENA();
                M_CONTENA data = new M_CONTENA();
                data.CONTENA_CD = CONTENA_CD.ToString();
                data.CONTENA_SHURUI_CD = CONTENA_SHURUI_CD.ToString();
                con = dao_Getcontena.GetDataByCd(data);
                if (con == null)
                {
                    //this.form.customDataGridViewSechi["CONTENA_SHURUI_CD", e.RowIndex].Value = string.Empty;
                    //this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewSechi["CONTENA_CD", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                }

                returnval = true;
                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

        #region コンテナ種類チェック(引揚)
        /// <summary>
        ///コンテナ種類チェック(引揚)
        /// </summary>
        internal bool CheckContenaShuruiHikiage(object CONTENA_SHURUI_CD, object CONTENA_CD, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = false;
            try
            {
                LogUtility.DebugMethodStart(CONTENA_SHURUI_CD, CONTENA_CD, e);

                if (CONTENA_SHURUI_CD != null
                    && !string.IsNullOrEmpty(CONTENA_SHURUI_CD.ToString())
                    && !this.isSuuryouKanri)
                {
                    // ポップアップで表示している情報と同等のデータでチェック
                    SearchConditionDto condition = new SearchConditionDto();
                    condition.CONTENA_SHURUI_CD = CONTENA_SHURUI_CD.ToString();
                    condition.GYOUSHA_CD = this.GyoushaCd;
                    condition.GENBA_CD = this.GenbaCd;

                    // データ取得 + 設置データ絞込
                    var dataList = this.GetPutContenaList(condition);

                    // 設置可能なコンテナ種類かチェック
                    if (dataList == null
                        || dataList.Count() < 1)
                    {
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_CD_HIKIAGE", e.RowIndex].Value = string.Empty;
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                        MessageBoxUtility.MessageBoxShow("E057", "指定している現場に設置", "このコンテナ種類CDは指定");
                        returnval = false;
                        return returnval;
                    }
                }

                // 入力されてない場合
                if (CONTENA_SHURUI_CD == null || String.IsNullOrEmpty(CONTENA_SHURUI_CD.ToString()) ||
                    CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                {
                    // 処理終了
                    this.form.customDataGridViewHikiage["CONTENA_CD_HIKIAGE", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                    returnval = true;
                    return returnval;
                }

                M_CONTENA con = new M_CONTENA();
                M_CONTENA data = new M_CONTENA();
                data.CONTENA_CD = CONTENA_CD.ToString();
                data.CONTENA_SHURUI_CD = CONTENA_SHURUI_CD.ToString();
                con = dao_Getcontena.GetDataByCd(data);
                if (con == null)
                {
                    //this.form.customDataGridViewSechi["CONTENA_SHURUI_CD", e.RowIndex].Value = string.Empty;
                    //this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewHikiage["CONTENA_CD_HIKIAGE", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                }

                returnval = true;
                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

        #region コンテナチェック(設置)
        /// <summary>
        ///コンテナチェック(設置)
        /// </summary>
        internal bool CheckContenaSechi(object CONTENA_SHUEUI_CD, object CONTENA_CD, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = false;
            try
            {
                LogUtility.DebugMethodStart(CONTENA_SHUEUI_CD, CONTENA_CD, e);

                // 入力されてない場合
                if (CONTENA_SHUEUI_CD == null || String.IsNullOrEmpty(CONTENA_SHUEUI_CD.ToString()) ||
                    CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                {
                    // 処理終了
                    // this.form.customDataGridViewSechi["CONTENA_SHURUI_CD", e.RowIndex].Value = string.Empty;
                    if (CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                    {
                        this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                    }

                    returnval = true;
                    return returnval;
                }

                M_CONTENA con = new M_CONTENA();
                M_CONTENA data = new M_CONTENA();
                data.CONTENA_CD = CONTENA_CD.ToString();
                data.CONTENA_SHURUI_CD = CONTENA_SHUEUI_CD.ToString();
                con = dao_Getcontena.GetDataByCd(data);
                if (con == null)
                {
                    returnval = false;
                    return returnval;
                }
                else
                {
                    //削除済みか確認
                    if (con.DELETE_FLG)
                    {
                        returnval = false;
                        return returnval;
                    }

                    this.form.customDataGridViewSechi["CONTENA_SHURUI_CD", e.RowIndex].Value = con.CONTENA_SHURUI_CD;
                    //
                    this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = con.CONTENA_NAME_RYAKU;

                    //コンテナ種類CDによって、コンテナ種類名を取得
                    CNTSHIDtoCls seichi = new CNTSHIDtoCls();                                              //コンテナ指定一覧
                    M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();                                      //コンテナ種類
                    seichi.CONTENA_SHURUI_CD = con.CONTENA_SHURUI_CD;                                  //コンテナ種類CD
                    conshu = dao_Getcontenashurui.GetDataByCd(seichi.CONTENA_SHURUI_CD);
                    if (conshu != null)
                    {
                        this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = conshu.CONTENA_SHURUI_NAME_RYAKU;               //コンテナ種類名
                    }
                }

                LogUtility.DebugMethodEnd(true);

                returnval = true;
                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

        #region コンテナチェック(引揚)
        /// <summary>
        ///コンテナチェック(引揚)
        /// </summary>
        internal bool CheckContenaHikiage(object CONTENA_SHUEUI_CD, object CONTENA_CD, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = false;
            try
            {
                LogUtility.DebugMethodStart(CONTENA_SHUEUI_CD, CONTENA_CD, e);

                // 入力されてない場合
                if (CONTENA_SHUEUI_CD == null || String.IsNullOrEmpty(CONTENA_SHUEUI_CD.ToString()) ||
                    CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                {
                    // 処理終了
                    // this.form.customDataGridViewHikiage["CONTENA_SHURUI_CD_HIKIAGE", e.RowIndex].Value = string.Empty;
                    if (CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
                    {
                        this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                    }

                    returnval = true;
                    return returnval;
                }

                // ポップアップで表示している情報と同等のデータでチェック
                SearchConditionDto condition = new SearchConditionDto();
                condition.CONTENA_SHURUI_CD = CONTENA_SHUEUI_CD.ToString();
                condition.GYOUSHA_CD = this.GyoushaCd;
                condition.GENBA_CD = this.GenbaCd;
                condition.CONTENA_CD = CONTENA_CD.ToString();

                // データ取得 + 設置データ絞込
                var dataList = this.GetPutContenaList(condition);

                // 設置可能なコンテナかチェック
                if (dataList == null
                    || dataList.Count() < 1)
                {
                    returnval = false;
                    return returnval;
                }

                // コンテナマスタのチェック
                M_CONTENA con = new M_CONTENA();
                M_CONTENA data = new M_CONTENA();
                data.CONTENA_CD = CONTENA_CD.ToString();
                data.CONTENA_SHURUI_CD = CONTENA_SHUEUI_CD.ToString();
                con = dao_Getcontena.GetDataByCd(data);
                if (con == null)
                {
                    returnval = false;
                    return returnval;
                }
                else
                {
                    //削除済みか確認
                    if (con.DELETE_FLG)
                    {
                        returnval = false;
                        return returnval;
                    }

                    this.form.customDataGridViewHikiage["CONTENA_SHURUI_CD_HIKIAGE", e.RowIndex].Value = con.CONTENA_SHURUI_CD;
                    //
                    this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = con.CONTENA_NAME_RYAKU;
                    //コンテナ種類CDによって、コンテナ種類名を取得
                    CNTSHIDtoCls seichi = new CNTSHIDtoCls();                                              //コンテナ指定一覧
                    M_CONTENA_SHURUI conshu = new M_CONTENA_SHURUI();                                      //コンテナ種類
                    seichi.CONTENA_SHURUI_CD = con.CONTENA_SHURUI_CD;                                  //コンテナ種類CD
                    conshu = dao_Getcontenashurui.GetDataByCd(seichi.CONTENA_SHURUI_CD);
                    if (conshu != null)
                    {
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = conshu.CONTENA_SHURUI_NAME_RYAKU;               //コンテナ種類名
                    }
                }

                LogUtility.DebugMethodEnd(true);

                returnval = true;
                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

        /// <summary>
        ///（コンテナ種類CD+コンテナCD）重複チェック
        /// True：重複 False：重複なし
        /// </summary>
        internal bool ContenaShuruiCDJuufukuChk()
        {

            LogUtility.DebugMethodStart();
            try
            {
                int recCount = 0;
                List<string> secchiContenaList = new List<string>();
                List<string> hikiageContenaList = new List<string>();

                //設置
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    //色初期化
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Style.BackColor = Color.White;
                    this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Style.BackColor = Color.White;
                }
                for (int i = 0; i < this.form.customDataGridViewSechi.Rows.Count - 1; i++)
                {
                    // 空白行は無視
                    if (this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value == null
                        || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                    {
                        continue;
                    }

                    var checkStr = this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_SHURUI_CD"].Value + "," +
                                   this.form.customDataGridViewSechi.Rows[i].Cells["CONTENA_CD"].Value;
                    secchiContenaList.Add(checkStr);

                    //比較
                    for (int k = i + 1; k < this.form.customDataGridViewSechi.Rows.Count - 1; k++)
                    {
                        if (this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_SHURUI_CD"].Value == null
                            || string.IsNullOrEmpty(this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_SHURUI_CD"].Value.ToString()))
                        {
                            continue;
                        }

                        string hikakuStr = this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_SHURUI_CD"].Value + "," +
                                           this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_CD"].Value;

                        if (checkStr.Equals(hikakuStr))
                        {
                            this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_SHURUI_CD"].Style.BackColor = Color.FromArgb(255, 100, 100);
                            this.form.customDataGridViewSechi.Rows[k].Cells["CONTENA_CD"].Style.BackColor = Color.FromArgb(255, 100, 100);
                            recCount++;
                        }
                    }

                }

                //引揚
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    //色初期化
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Style.BackColor = Color.White;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Style.BackColor = Color.White;
                }
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    // 空白行は無視
                    if (this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value == null
                        || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                    {
                        continue;
                    }

                    var contenaShuruiCdVal = this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value + ","+
                                             this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Value;
                    hikiageContenaList.Add(contenaShuruiCdVal);
                    //比較
                    for (int k = i + 1; k < this.form.customDataGridViewHikiage.Rows.Count - 1; k++)
                    {
                        if (this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value == null
                            || string.IsNullOrEmpty(this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value.ToString()))
                        {
                            continue;
                        }

                        string hikakuStr = this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value + "," +
                                           this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_CD_HIKIAGE"].Value;
                        if (contenaShuruiCdVal.Equals(hikakuStr))
                        {
                            this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Style.BackColor = Color.FromArgb(255, 100, 100);
                            this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_CD_HIKIAGE"].Style.BackColor = Color.FromArgb(255, 100, 100);
                            recCount++;
                        }
                    }
                }

                // 個体管理の場合、設置と引揚で同じものを指定するのは無し
                if (!this.isSuuryouKanri)
                {
                    foreach (var secchiContena in secchiContenaList)
                    {
                        if (hikiageContenaList.Contains(secchiContena))
                        {
                            recCount++;
                        }
                    }
                }

                //重複なら
                if (recCount > 0)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }


        #region uitility

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
                }
                else
                {
                    return obj;
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

        #endregion

        #region ベースフォームのメッソドの実例化(ロジックは実装しない)

        /// <summary>
        ///データ検索処理
        /// </summary>
        public int Search()
        {
            return 0;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {

        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {

        }
        #endregion


        /// <summary>
        /// 有効期限内か確認
        /// </summary>
        /// <param name="startDate">適用開始日</param>
        /// <param name="endDate">適用終了日</param>
        /// <returns>True:有効範囲内 False：有効範囲外</returns>
        private bool IsYuukouKikannai(SqlDateTime startDate, SqlDateTime endDate)
        {
            // TODO 20141010 暫定対応として適用期間は現在日付を条件とする
            //               次回の修正で伝票日付を条件にする（GetContenaData.sqlも修正すること）

            var nowDate = new SqlDateTime(this.parentForm.sysDate);
            if (startDate.CompareTo(nowDate) <= 0)
            {
                if (endDate.IsNull)
                {
                    //開始日が範囲内で終了日なしの時
                    return true;
                }
                else
                {
                    if (endDate.CompareTo(nowDate) >= 0)
                    {
                        //開始日、終了日が範囲内の時
                        return true;
                    }
                }
            }
            return false;
        }

        //2014.05.23 引揚に設置で選択している行をコピーします by 胡　strat  
        /// <summary>
        /// 引揚に設置で選択している行をコピーします
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                int recCount = 0;
                int SechiCurrentCellRowsIndex = this.form.customDataGridViewSechi.CurrentCell.RowIndex;
                if (SechiCurrentCellRowsIndex >= this.form.customDataGridViewSechi.Rows.Count-1)
                {
                    return;
                }
                //設置データ取得
                var contenaShuruiCdVal = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_SHURUI_CD"].Value + "," +
                                         this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_CD"].Value;
                if (null == contenaShuruiCdVal || string.IsNullOrEmpty(contenaShuruiCdVal.ToString()))
                {
                    return;
                }               
                //引揚  
                for (int i = 0; i < this.form.customDataGridViewHikiage.Rows.Count - 1; i++)
                {
                    //色初期化
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Style.BackColor = Color.White;
                    this.form.customDataGridViewHikiage.Rows[i].Cells["CONTENA_CD_HIKIAGE"].Style.BackColor = Color.White;
                }
                //比較
                for (int k = 0; k < this.form.customDataGridViewHikiage.Rows.Count - 1; k++)
                {
                    string hikakuStr = this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value + "," +
                                       this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_CD_HIKIAGE"].Value;
                    if (contenaShuruiCdVal.Equals(hikakuStr))
                    {
                        this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Style.BackColor = Color.FromArgb(255, 100, 100);
                        this.form.customDataGridViewHikiage.Rows[k].Cells["CONTENA_CD_HIKIAGE"].Style.BackColor = Color.FromArgb(255, 100, 100);
                        recCount++;
                    }
                }

                //重複ないとき
                if (recCount <= 0)
                {
                    //引揚区分検索結果設定
                    this.form.customDataGridViewHikiage.Rows.Add();
                    int HikiageAddRowsIndex = this.form.customDataGridViewHikiage.Rows.Count - 2;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["CONTENA_SHURUI_CD_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_SHURUI_CD"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_SHURUI_NAME_RYAKU"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["CONTENA_CD_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_CD"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["CONTENA_NAME_RYAKU_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["CONTENA_NAME_RYAKU"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["DAISUU_CNT_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["DAISUU_CNT"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["DELETE_FLG_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["DELETE_FLG"].Value;
                    this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells["TIME_STAMP_HIKIAGE"].Value = this.form.customDataGridViewSechi.Rows[SechiCurrentCellRowsIndex].Cells["TIME_STAMP"].Value;
                    //フォーカス設定
                    this.form.customDataGridViewHikiage.CurrentCell = this.form.customDataGridViewHikiage.Rows[HikiageAddRowsIndex].Cells[0];
                    this.form.customDataGridViewHikiage.CurrentCell.Selected = true;
                }
                else 
                {
                    //選択しているコンテナは引揚対象で既に使用されているため、この操作は実行できません。
                    MessageBoxUtility.MessageBoxShow("E086", "選択しているコンテナ", "引揚対象", "この操作は実行");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func1_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        //2014.05.23 引揚に設置で選択している行をコピーします by 胡　end     

        /// <summary>
        /// 設置不可能なコンテナか判定
        /// </summary>
        /// <param name="CONTENA_SHUEUI_CD"></param>
        /// <param name="CONTENA_CD"></param>
        /// <param name="contenaSetKbn">コンテナの操作(1:設置、2:引揚)</param>
        /// <param name="e"></param>
        /// <returns>true:設置不可能、false:設置可能</returns>
        internal bool IsCannotPutAndTakeContena(object CONTENA_SHUEUI_CD, object CONTENA_CD, int contenaSetKbn, DataGridViewCellValidatingEventArgs e)
        {
            bool returnVal = false;

            // 入力されてない場合
            if (CONTENA_SHUEUI_CD == null || String.IsNullOrEmpty(CONTENA_SHUEUI_CD.ToString()) ||
                CONTENA_CD == null || String.IsNullOrEmpty(CONTENA_CD.ToString()))
            {
                // 処理終了
                returnVal = false;
                return returnVal;
            }

            if (string.IsNullOrEmpty(this.denpyouDate))
            {
                // 伝票日付が無い場合は、データ登録時のチェックに任せるため、ここではエラーとしない
                returnVal = false;
                return returnVal;
            }

            var contenaShiteiUtil = new ContenaShiteiUtility();
            DateTime denpyouDate = Convert.ToDateTime(this.denpyouDate);
            returnVal = contenaShiteiUtil.IsCannotPutAndTakeContena(CONTENA_SHUEUI_CD.ToString(), CONTENA_CD.ToString(), contenaSetKbn, denpyouDate.Date);

            return returnVal;
        }

        /// <summary>
        /// 設置済コンテナの情報を取得する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>dtoのGYOUSHA_CD, GENBA_CDが空の場合はnullを返す</returns>
        internal SearchResultDto[] GetPutContenaList(SearchConditionDto dto)
        {
            SearchResultDto[] returnVal = null;

            // 必須チェック
            if (string.IsNullOrEmpty(dto.GYOUSHA_CD)
                || string.IsNullOrEmpty(dto.GENBA_CD))
            {
                return returnVal;
            }

            var contenaDao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();

            var jissekiDataList = contenaDao.GetJissekiContenaDataSql(dto);

            if (jissekiDataList != null && jissekiDataList.Count > 0)
            {

                // 以前は「設置 -> 引揚の情報を打ち消して、設置のデータだけを抽出」という処理が
                // この後に存在したが、処理速度を改善するために削除し、
                // 「設置コンテナ一覧画面用(固体管理)の一覧データ取得」処理の中で同時に行うよう
                // 改修した。 #124463

                returnVal = jissekiDataList.ToArray();
            }

            return returnVal;
        }

        internal SearchResultDto[] GetContenaList(SearchConditionDto dto)
        {
            LogUtility.DebugMethodStart(dto);

            SearchResultDto[] ret = null;

            var dao = DaoInitUtility.GetComponent<CheckCONRETDaoCls>();
            ret = dao.GetContenaData(dto).ToArray();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region コンテナ種類チェック
        /// <summary>
        ///コンテナ種類チェック
        /// </summary>
        internal bool CheckContenaShuruiCd(int kbn, string contenaShuruiCd, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = true;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(kbn, contenaShuruiCd, e);

                this.isInputError = false;
                if (kbn == 1)
                {
                    string cellname = this.form.customDataGridViewSechi.Columns[e.ColumnIndex].Name;
                    this.form.customDataGridViewSechi["CONTENA_CD", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                    // 入力されてない場合
                    if (string.IsNullOrEmpty(contenaShuruiCd))
                    {
                        // 処理終了
                        this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                        return returnval;
                    }

                    var shurui = dao_Getcontenashurui.GetDataByCd(contenaShuruiCd);
                    if (shurui == null || shurui.DELETE_FLG.IsTrue)
                    {
                        var cell = this.form.customDataGridViewSechi[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewSechi.EditingControl).SelectAll();
                        this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                        msgLogic.MessageBoxShow("E020", "コンテナ種類");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                    }
                    else
                    {
                        this.form.customDataGridViewSechi["CONTENA_SHURUI_NAME_RYAKU", e.RowIndex].Value = shurui.CONTENA_SHURUI_NAME_RYAKU;
                    }
                }
                else
                {
                    string cellname = this.form.customDataGridViewHikiage.Columns[e.ColumnIndex].Name;
                    this.form.customDataGridViewHikiage["CONTENA_CD_HIKIAGE", e.RowIndex].Value = string.Empty;
                    this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                    // 入力されてない場合
                    if (string.IsNullOrEmpty(contenaShuruiCd))
                    {
                        // 処理終了
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                        return returnval;
                    }

                    var shurui = dao_Getcontenashurui.GetDataByCd(contenaShuruiCd);
                    if (shurui == null || shurui.DELETE_FLG.IsTrue)
                    {
                        var cell = this.form.customDataGridViewHikiage[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewHikiage.EditingControl).SelectAll();
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                        msgLogic.MessageBoxShow("E020", "コンテナ種類");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                    }
                    else
                    {
                        this.form.customDataGridViewHikiage["CONTENA_SHURUI_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = shurui.CONTENA_SHURUI_NAME_RYAKU;
                    }
                }
                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckContenaShuruiCd", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                this.isInputError = true;
                returnval = false;
                return returnval;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

        #region コンテナチェック
        /// <summary>
        ///コンテナチェック
        /// </summary>
        internal bool CheckContenaCd(int kbn, string contenaShuruiCd, string contenaCd, DataGridViewCellValidatingEventArgs e)
        {
            bool returnval = true;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(kbn, contenaShuruiCd, contenaCd, e);

                var before = string.Empty;
                string cellname = string.Empty;
                M_CONTENA keyEtity = new M_CONTENA();
                if (kbn == 1)
                {
                    cellname = this.form.customDataGridViewSechi.Columns[e.ColumnIndex].Name;
                    if (this.form.beforeValuesForDetailSechi.ContainsKey(cellname))
                    {
                        before = this.form.beforeValuesForDetailSechi[cellname];
                    }
                    if (contenaCd == before && !this.isInputError)
                    {
                        return returnval;
                    }

                    this.isInputError = false;
                    // 入力されてない場合
                    if (string.IsNullOrEmpty(contenaCd))
                    {
                        // 処理終了
                        this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                        return returnval;
                    }
                    if (string.IsNullOrEmpty(contenaShuruiCd))
                    {
                        // 処理終了
                        var cell = this.form.customDataGridViewSechi[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewSechi.EditingControl).SelectAll();
                        msgLogic.MessageBoxShow("E012", "コンテナ種類CD");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                        return returnval;
                    }
                    keyEtity.CONTENA_SHURUI_CD = contenaShuruiCd;
                    keyEtity.CONTENA_CD = contenaCd;
                    var contena = dao_Getcontena.GetDataByCd(keyEtity);
                    if (contena == null || contena.DELETE_FLG.IsTrue)
                    {
                        var cell = this.form.customDataGridViewSechi[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewSechi.EditingControl).SelectAll();
                        this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = string.Empty;
                        msgLogic.MessageBoxShow("E020", "コンテナ");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                    }
                    else
                    {
                        this.form.customDataGridViewSechi["CONTENA_NAME_RYAKU", e.RowIndex].Value = contena.CONTENA_NAME_RYAKU;
                    }
                }
                else
                {
                    cellname = this.form.customDataGridViewHikiage.Columns[e.ColumnIndex].Name;
                    if (this.form.beforeValuesForDetailHikiage.ContainsKey(cellname))
                    {
                        before = this.form.beforeValuesForDetailHikiage[cellname];
                    }
                    if (contenaCd == before && !this.isInputError)
                    {
                        return returnval;
                    }

                    this.isInputError = false;
                    // 入力されてない場合
                    if (string.IsNullOrEmpty(contenaShuruiCd))
                    {
                        // 処理終了
                        var cell = this.form.customDataGridViewHikiage[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewHikiage.EditingControl).SelectAll();
                        msgLogic.MessageBoxShow("E012", "コンテナ種類CD");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                        return returnval;
                    }
                    if (string.IsNullOrEmpty(contenaCd))
                    {
                        // 処理終了
                        this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                        return returnval;
                    }
                    keyEtity.CONTENA_SHURUI_CD = contenaShuruiCd;
                    keyEtity.CONTENA_CD = contenaCd;
                    var contena = dao_Getcontena.GetDataByCd(keyEtity);
                    if (contena == null)
                    {
                        var cell = this.form.customDataGridViewHikiage[cellname, e.RowIndex];
                        ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                        ((TextBox)this.form.customDataGridViewHikiage.EditingControl).SelectAll();
                        this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = string.Empty;
                        msgLogic.MessageBoxShow("E020", "コンテナ");
                        e.Cancel = true;
                        this.isInputError = true;
                        returnval = false;
                    }
                    else
                    {
                        this.form.customDataGridViewHikiage["CONTENA_NAME_RYAKU_HIKIAGE", e.RowIndex].Value = contena.CONTENA_NAME_RYAKU;
                    }
                }

                return returnval;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckContenaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                this.isInputError = true;
                returnval = false;
                return returnval;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnval);
            }
        }
        #endregion

    }
}