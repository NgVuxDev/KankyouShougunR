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
using System.Reflection;
using System.Data;
using System.Windows.Forms;
using r_framework.CustomControl;
using System.Drawing;
using System.IO;
using CommonChouhyouPopup.App;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Data.SqlTypes;
using r_framework.Dto;

namespace Shougun.Core.Carriage.UnchinSyuukeihyou
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
        /// ヘッダフォーム
        /// </summary>
        internal UIHeaderForm headerForm;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        /// <summary>帳票情報を保持するプロパティ</summary>
        public ReportInfoBase ReportInfo { get; set; }

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
                //this.dto = new DTOClass();

                // システム情報Dao
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // システム情報を取得
                this.GetSysInfoInit();

                // イベントの初期化処理
                this.EventInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 画面初期表示処理
                this.DisplayInit();
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

                // 処理No　Enabled制御
                this.parentForm.txb_process.Enabled = true;
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

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {

            ButtonSetting[] returnVal = null;

            try
            {
                LogUtility.DebugMethodStart();

                var thisAssembly = Assembly.GetExecutingAssembly();
                var buttonSetting = new ButtonSetting();
                string BUTTON_SETTING_XML = "Shougun.Core.Carriage.UnchinSyuukeihyou.Setting.ButtonSetting.xml";
                returnVal = buttonSetting.LoadButtonSetting(thisAssembly, BUTTON_SETTING_XML);
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

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                // 印刷ボタン(F5)イベント
                parentForm.bt_func5.Click += new EventHandler(this.form.Print);

                // CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);
                parentForm.ProcessButtonPanel.Visible = false;
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

        #region header初期化処理
        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeaderForm headForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headForm);
                this.headerForm = headForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 初期表示処理
        /// <summary>
        /// 初期表示処理
        /// </summary>
        internal bool DisplayInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //ボタン活性/非活性設定
                this.SetEnabled();

                // コントロールの値をクリア
                this.ClearControls();

                return true;
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

        #region ボタン活性/非活性
        /// <summary>
        //ボタン活性/非活性
        /// </summary>
        /// <returns></returns>
        private void SetEnabled()
        {
            // 検索実行が行われている？
            if (this.ReportInfo != null)
            {
                // 印刷ボタン(F5)活性化
                this.parentForm.bt_func5.Enabled = true;
                // CSVボタン(F6)活性化
                this.parentForm.bt_func6.Enabled = true;

            }
            else
            {
                // 印刷ボタン(F5)非活性化
                this.parentForm.bt_func5.Enabled = false;
                // CSVボタン(F6)非活性化
                this.parentForm.bt_func6.Enabled = false;
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
                LogUtility.Error("例外エラーが発生しました。", ex);
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

                //HeaderForm部
                this.headerForm.CORP_RYAKU_NAME.Text = string.Empty;
                this.headerForm.KYOTEN_NAME.Text = string.Empty;
                this.headerForm.ALERT_NUMBER.Text = string.Empty;
                this.headerForm.READ_DATA_NUMBER.Text = string.Empty;

                //PageHeader部
                this.form.DENPYOU_DATE_BEGIN.Text = string.Empty;
                this.form.DENPYOU_DATE_END.Text = string.Empty;
                this.form.FILL_COND_1_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_1_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_1_CD_END.Text = string.Empty;
                this.form.FILL_COND_1_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_2_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_2_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_2_CD_END.Text = string.Empty;
                this.form.FILL_COND_2_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_3_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_3_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_3_CD_END.Text = string.Empty;
                this.form.FILL_COND_3_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_4_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_4_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_4_CD_END.Text = string.Empty;
                this.form.FILL_COND_4_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_5_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_5_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_5_CD_END.Text = string.Empty;
                this.form.FILL_COND_5_VALUE_END.Text = string.Empty;

                //Detail部
                // 明細 Start
                // テンプレートをいじる処理は、データ設定前に実行
                this.form.gcCustomMultiRow1.BeginEdit(false);
                this.form.gcCustomMultiRow1.Rows.Clear();
                this.form.gcCustomMultiRow1.EndEdit();
                this.form.gcCustomMultiRow1.NotifyCurrentCellDirty(false);
                // 明細 End

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

        #region [F5]印刷
        /// <summary>
        /// [F5]印刷　処理
        /// </summary>
        internal void Print()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.ReportInfo.Create(@".\Template\R483-Form.xml", "LAYOUT1", new DataTable());
                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.ReportInfo, WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU))
                {
                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
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

        #region [F6]CSV出力処理

        #region CSVFile出力
        /// <summary>
        /// CSVFile出力
        /// </summary>
        internal void CsvOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = this.form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;
                var fileName = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                browserForFolder = null;

                if (false == String.IsNullOrEmpty(filePath))
                {
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(-0)))
                    {
                        string strCsv = string.Empty;
                        // タイトル文字列取得
                        strCsv = this.GetCsvTitleString();
                        // タイトル書く
                        sw.WriteLine(strCsv);

                        // 明細ヘッダ文字列取得
                        strCsv = this.GetCsvHeaderString();
                        // 明細ヘッダ書く
                        sw.WriteLine(strCsv);

                        // 明細データ出力
                        this.OutPutCsvMeisaiData(sw);
                    }

                    // 出力完了メッセージ
                    msgLogic.MessageBoxShow("I000", "CSV出力");
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Csvタイトル文字列を作成
        /// <summary>
        /// Csvタイトル文字列を作成
        /// </summary>
        /// <returns>タイトル文字列（カンマで区切済み）</returns>
        private string GetCsvTitleString()
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return returnVal;
                }

                // Headerテーブル
                var dt = this.ReportInfo.DataTableList["Header"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return returnVal;
                }

                // 削除文字
                char[] trimChar = { '別' };

                // ヘッダ値出力
                DataTable dtHeader = this.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];

                List<string> csvItems = new List<string>();
                // 会社
                csvItems.Add(row["CORP_RYAKU_NAME"].ToString());
                // 拠点
                csvItems.Add(row["KYOTEN_NAME"].ToString());
                // 日付指定範囲
                if (string.IsNullOrEmpty(row["DENPYOU_DATE_BEGIN"].ToString()) && string.IsNullOrEmpty(row["DENPYOU_DATE_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["DENPYOU_DATE_BEGIN"].ToString() + " ～ " + row["DENPYOU_DATE_END"].ToString());
                }
                // 抽出条件1
                csvItems.Add("[運搬業者]");
                // 抽出条件1CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_1_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_1_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_1_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_1_CD_END"].ToString());
                }
                // 抽出条件2
                csvItems.Add("[荷積業者]");
                // 抽出条件2CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_2_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_2_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_2_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_2_CD_END"].ToString());
                }
                // 抽出条件3
                csvItems.Add("[荷積現場]");
                // 抽出条件3CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + "～" + row["FILL_COND_3_CD_END"].ToString());
                }

                // 抽出条件4
                csvItems.Add("[荷降業者]");
                // 抽出条件4CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_4_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_4_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_4_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_4_CD_END"].ToString());
                }
                // 抽出条件5
                csvItems.Add("[荷降現場]");
                // 抽出条件5CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_5_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_5_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_5_CD_BEGIN"].ToString() + "～" + row["FILL_COND_5_CD_END"].ToString());
                }

                // カンマで区切り
                returnVal = string.Join(",", csvItems);

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

        #region Csv明細ヘッダ文字列を作成
        /// <summary>
        /// Csv明細ヘッダ文字列を作成
        /// </summary>
        /// <returns>明細ヘッダ文字列（カンマで区切済み）</returns>
        private string GetCsvHeaderString()
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                List<string> csvItems = new List<string>();
                // 明細
                csvItems.Add("運搬業者CD");
                csvItems.Add("運搬業者名");
                csvItems.Add("荷積業者CD");
                csvItems.Add("荷積業者名");
                csvItems.Add("荷積現場CD");
                csvItems.Add("荷積現場名");
                csvItems.Add("荷降業者CD");
                csvItems.Add("荷降業者名");
                csvItems.Add("荷降現場CD");
                csvItems.Add("荷降現場名");
                csvItems.Add("伝票種類");
                csvItems.Add("車種CD");
                csvItems.Add("車種名");
                csvItems.Add("車輌CD");
                csvItems.Add("車輌名");
                csvItems.Add("運転者CD");
                csvItems.Add("運転者名");
                csvItems.Add("品名CD");
                csvItems.Add("品名");
                csvItems.Add("正味(kg)");
                csvItems.Add("数量");
                csvItems.Add("単位");
                csvItems.Add("運賃金額");
                csvItems.Add("運賃消費税");
                csvItems.Add("運賃合計額");

                // カンマで区切り
                returnVal = string.Join(",", csvItems);

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

        #region Csv明細データ文字列を作成
        /// <summary>
        /// Csv明細データ文字列を作成
        /// </summary>
        /// <param name="sw">StreamWriter</param>
        /// <returns>明細データ文字列（カンマで区切済み）</returns>
        private void OutPutCsvMeisaiData(StreamWriter sw)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.ReportInfo.DataTableList.ContainsKey("Detail"))
                {
                    // 処理終了
                    return;
                }

                // Headerテーブル
                var dt = this.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // 出力項目名
                List<string> outputItems = new List<string> { "UNPAN_GYOUSHA_CD"	    //運搬業者CD
                                                            , "UNPAN_GYOUSHA_NAME"	    //運搬業者名
                                                            , "NZM_GYOUSHA_CD"	        //荷積業者CD
                                                            , "NZM_GYOUSHA_NAME"	    //荷積業者名
                                                            , "NZM_GENBA_CD"	        //荷積現場CD
                                                            , "NZM_GENBA_NAME"	        //荷積現場名
                                                            , "NOS_GYOUSHA_CD"	        //荷降業者CD
                                                            , "NOS_GYOUSHA_NAME"	    //荷降業者名
                                                            , "NOS_GENBA_CD"	        //荷降現場CD
                                                            , "NOS_GENBA_NAME"	        //荷降現場名
                                                            , "DENPYOU_SHURUI"	        //伝票種類
                                                            , "SHASHU_CD"	            //車種CD
                                                            , "SHASHU_NAME"	            //車種名
                                                            , "SHARYOU_CD"	            //車輌CD
                                                            , "SHARYOU_NAME"	        //車輌名
                                                            , "UNTENSHA_CD"	            //運転者CD
                                                            , "UNTENSHA_NAME"	        //運転者名
                                                            , "HINMEI_CD"	            //品名CD
                                                            , "HINMEI_NAME"	            //品名
                                                            , "SYOUMI"	                //正味(kg)
                                                            , "SUURYOU"	                //数量
                                                            , "UNIT_NAME"	            //単位
                                                            , "UNCHIN"	                //運賃金額
                                                            , "TAX"	                    //運賃消費税
                                                            , "KINGAKU"	                //運賃合計額
                                                            };

                // ヘッダ値出力
                DataTable dtDetail = this.ReportInfo.DataTableList["Detail"];
                foreach (DataRow row in dtDetail.Rows)
                {
                    List<string> csvItems = new List<string>();

                    foreach (var item in outputItems)
                    {
                        // 「,」がある場合
                        if (row[item].ToString().IndexOf(',') >= 0)
                        {
                            csvItems.Add("\"" + row[item].ToString() + "\"");
                        }
                        else
                        {
                            csvItems.Add(row[item].ToString());
                        }
                    }

                    // カンマで区切り
                    sw.WriteLine(string.Join(",", csvItems));
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

        #region [F8]検索

        #region [F8]検索
        /// <summary>
        /// [F8]検索
        /// </summary>
        internal void ShowPopUp()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //LogUtility.DebugMethodStart();

                // 運賃範囲条件指定画面表示
                var callForm = new Shougun.Core.Carriage.UntinSyuusyuuhyoPopup.UIForm();
                var callHeader = new Shougun.Core.Carriage.UntinSyuusyuuhyoPopup.UIHeader();
                var popForm = new BasePopForm(callForm, callHeader);

                //// 画面表示位置を設定（ディスプレイ中央）
                //popForm.StartPosition = FormStartPosition.Manual;
                //int screenHeight, screenWidth;
                ////ディスプレイの高さ
                //screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                ////ディスプレイの幅
                //screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

                ////フォーム位置を中央に設定
                //popForm.Location = new Point((screenWidth - callForm.formWidth) / 2, (screenHeight - callForm.formHeight) / 2);

                // 画面表示位置を設定（親フォーム中央）
                popForm.StartPosition = FormStartPosition.Manual;
                int parentFormHeight, parentFormWidth;
                // 親フォームの高さ
                parentFormHeight = parentForm.Height;
                // 親フォームの幅
                parentFormWidth = parentForm.Width;

                //フォーム位置を中央に設定
                popForm.Location = new Point(parentForm.Left + (parentFormWidth - callForm.formWidth) / 2, parentForm.Top + (parentFormHeight - callForm.formHeight) / 2);

                // ポップアップ
                var dr = popForm.ShowDialog();


                // 結果を取得
                callForm = (Shougun.Core.Carriage.UntinSyuusyuuhyoPopup.UIForm)popForm.inForm;

                // キャンセルを押下する場合
                if (this.form.gcCustomMultiRow1.Rows.Count > 0 && callForm.getReportInfo() == null)
                {
                    return;
                }

                this.ReportInfo = callForm.getReportInfo();
                if (this.ReportInfo != null)
                {
                    // データを表示
                    SetData();
                }

                // ボタン活性/非活性設定
                this.SetEnabled();

            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region データ設定
        /// <summary>
        /// データ設定
        /// </summary>
        /// <returns></returns>
        public void SetData()
        {
            try
            {
                LogUtility.DebugMethodStart();
                Init_Header();
                Init_Detail();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Header表示
        /// <summary>
        /// Header表示
        /// </summary>
        public void Init_Header()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return;
                }

                // Headerテーブル
                var dt = this.ReportInfo.DataTableList["Header"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }
                // Row取得（1行しかないはず）
                DataRow row = dt.Rows[0];

                // ****************************************
                // ヘッダ値設定
                // ****************************************
                // コントロール取得
                Control[] ctrls = this.form.GetAllControl();
                // UIFormのコントロールを制御
                foreach (var ctrl in ctrls)
                {
                    // 下記コントロール以外の場合
                    if (ctrl.GetType() != typeof(CustomTextBox)
                        && ctrl.GetType() != typeof(CustomAlphaNumTextBox)
                        && ctrl.GetType() != typeof(CustomNumericTextBox2)
                        )
                    {
                        // 次へ
                        continue;
                    }

                    // textプロパティを取得
                    var property = ctrl.GetType().GetProperty("Text");
                    if (property != null)
                    {
                        // DataTableに該当項目ある場合
                        if (dt.Columns.Contains(ctrl.Name))
                        {
                            // Text値を設定
                            property.SetValue(ctrl, row[ctrl.Name].ToString(), null);
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

        #region Detail表示
        /// <summary>
        /// Detail表示
        /// </summary>
        public void Init_Detail()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.ReportInfo.DataTableList.ContainsKey("Detail"))
                {
                    // 処理終了
                    return;
                }

                // Detailテーブル
                var dt = this.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // 数量フォーマット
                String systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();

                // フォーマット
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 正味(kg)
                    dt.Rows[i]["SYOUMI"] = this.SuuryouAndTankFormat(this.ChgDBNullToValue(dt.Rows[i]["SYOUMI"], 0), systemSuuryouFormat);
                    // 数量
                    dt.Rows[i]["SUURYOU"] = this.SuuryouAndTankFormat(this.ChgDBNullToValue(dt.Rows[i]["SUURYOU"], 0), systemSuuryouFormat);
                    // 運賃金額
                    dt.Rows[i]["UNCHIN"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["UNCHIN"], 0));
                    // 運賃消費税
                    dt.Rows[i]["TAX"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["TAX"], 0));
                    // 運賃合計額
                    dt.Rows[i]["KINGAKU"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["KINGAKU"], 0));

                    // 正味合計
                    dt.Rows[i]["SHOUMI_KEI"] = this.SuuryouAndTankFormat(this.ChgDBNullToValue(dt.Rows[i]["SHOUMI_KEI"], 0), systemSuuryouFormat);
                    // 運賃金額計
                    dt.Rows[i]["KINGAKU_KEI"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["KINGAKU_KEI"], 0));
                    // 運賃消費税
                    dt.Rows[i]["SHOHIZEI_TOTAL"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["SHOHIZEI_TOTAL"], 0));
                    // 運賃合計金額
                    dt.Rows[i]["KINGAKU_TOTAL"] = this.DecimalFormat(this.ChgDBNullToValue(dt.Rows[i]["KINGAKU_TOTAL"], 0));
                }

                // 
                this.form.gcCustomMultiRow1.Rows.Clear();

                // ブレーク制御用番号
                int breakNo = 0;

                // MultiRowの行
                GrapeCity.Win.MultiRow.Row muRow;

                // 明細データループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Row取得
                    DataRow row = dt.Rows[i];
                    // 最初行または集計項目が変わる場合
                    if (i == 0 || breakNo > 0)
                    {
                        this.form.gcCustomMultiRow1.Rows.Add();
                        muRow = this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1];
                        // 一行目
                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN1_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN1_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN2_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN2_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN3_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN4_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN4_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN5_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN5_VALUE"], "3");
                        // 値設定
                        muRow.Cells["KAHEN1_CD"].Value = row["UNPAN_GYOUSHA_CD"].ToString();
                        muRow.Cells["KAHEN1_VALUE"].Value = row["UNPAN_GYOUSHA_NAME"].ToString();
                        muRow.Cells["KAHEN2_CD"].Value = row["NZM_GYOUSHA_CD"].ToString();
                        muRow.Cells["KAHEN2_VALUE"].Value = row["NZM_GYOUSHA_NAME"].ToString();
                        muRow.Cells["KAHEN3_CD"].Value = row["NZM_GENBA_CD"].ToString();
                        muRow.Cells["KAHEN3_VALUE"].Value = row["NZM_GENBA_NAME"].ToString();
                        muRow.Cells["KAHEN4_CD"].Value = row["NOS_GYOUSHA_CD"].ToString();
                        muRow.Cells["KAHEN4_VALUE"].Value = row["NOS_GYOUSHA_NAME"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["NOS_GENBA_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["NOS_GENBA_NAME"].ToString();
                    }

                    // 最初行または集計項目が変わらない場合
                    if (i == 0 || breakNo > -1)
                    {
                        // 二行目
                        this.form.gcCustomMultiRow1.Rows.Add();
                        muRow = this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1];
                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN2_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN2_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN3_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN4_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN4_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN5_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN5_VALUE"], "3");
                        // 値設定
                        muRow.Cells["KAHEN2_VALUE"].Value = row["DENPYOU_SHURUI"].ToString();
                        muRow.Cells["KAHEN3_CD"].Value = row["SHASHU_CD"].ToString();
                        muRow.Cells["KAHEN3_VALUE"].Value = row["SHASHU_NAME"].ToString();
                        muRow.Cells["KAHEN4_CD"].Value = row["SHARYOU_CD"].ToString();
                        muRow.Cells["KAHEN4_VALUE"].Value = row["SHARYOU_NAME"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["UNTENSHA_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["UNTENSHA_NAME"].ToString();

                        // 三行目
                        this.form.gcCustomMultiRow1.Rows.Add();
                        muRow = this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1];
                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN2_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN2_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN3_CD"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN4_CD"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN4_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN5_CD"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN5_VALUE"], "3");
                        this.SetCellAlign(muRow.Cells["KAHEN6_CD"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_CD"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_CD"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");
                        // 値設定
                        muRow.Cells["KAHEN2_CD"].Value = row["HINMEI_CD"].ToString();
                        muRow.Cells["KAHEN2_VALUE"].Value = row["HINMEI_NAME"].ToString();
                        muRow.Cells["KAHEN3_VALUE"].Value = row["SYOUMI"].ToString();
                        muRow.Cells["KAHEN4_VALUE"].Value = row["SUURYOU"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["UNIT_NAME"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["UNCHIN"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["TAX"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["KINGAKU"].ToString();
                    }

                    // 最後行目ではない場合
                    if (i == dt.Rows.Count - 1)
                    {
                        // ブレーク番号
                        breakNo = 3;

                    }
                    // 最後行目ではない場合
                    else
                    {
                        // NextRow取得
                        DataRow nextRow = dt.Rows[i + 1];

                        // ブレーク制御用番号初期化
                        breakNo = 0;

                        // 荷積業者CD、荷積現場CD、荷降業者CD、荷降現場CDの何れかがブレーク時、表示出力
                        if (row["NZM_GYOUSHA_CD"].ToString() != nextRow["NZM_GYOUSHA_CD"].ToString()
                            || row["NZM_GENBA_CD"].ToString() != nextRow["NZM_GENBA_CD"].ToString()
                            || row["NOS_GYOUSHA_CD"].ToString() != nextRow["NOS_GYOUSHA_CD"].ToString()
                            || row["NOS_GENBA_CD"].ToString() != nextRow["NOS_GENBA_CD"].ToString())
                        {
                            // ブレーク番号
                            breakNo = 1;
                        }

                        // 運搬業者CDがブレーク時表示出力
                        if (row["UNPAN_GYOUSHA_CD"].ToString() != nextRow["UNPAN_GYOUSHA_CD"].ToString())
                        {
                            // ブレーク番号
                            breakNo = 2;
                        }

                    }

                    // 運搬業者計出力
                    if (breakNo > 1)
                    {
                        // Row取得
                        DataRow Row = dt.Rows[i];
                        this.form.gcCustomMultiRow1.Rows.Add();
                        muRow = this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1];
                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN1_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN1_VALUE"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN2_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN2_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN1_VALUE"], "1");
                        // 値設定
                        muRow.Cells["KAHEN1_VALUE"].Value = "運搬業者合計";

                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN3_VALUE"].Value = Row["SHOUMI_KEI"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = Row["KINGAKU_KEI"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = Row["SHOHIZEI_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = Row["KINGAKU_TOTAL"].ToString();
                    }

                    // 総合計出力
                    if (breakNo > 2)
                    {
                        // Detailテーブル
                        var dtFooter = this.ReportInfo.DataTableList["Footer"];
                        if (dtFooter == null || dtFooter.Rows.Count < 1)
                        {
                            return;
                        }
                        // Row取得
                        DataRow rowFooter = dtFooter.Rows[0];
                        this.form.gcCustomMultiRow1.Rows.Add();
                        muRow = this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1];
                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN1_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN1_VALUE"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN2_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN2_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN1_VALUE"], "1");
                        // 値設定
                        muRow.Cells["KAHEN1_VALUE"].Value = "総合計";
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 正味合計
                        rowFooter["SHOUMI_SOU_KEI"] = this.SuuryouAndTankFormat(this.ChgDBNullToValue(rowFooter["SHOUMI_SOU_KEI"], 0), systemSuuryouFormat);
                        // 運賃金額計
                        rowFooter["KINGAKU_SOU_KEI"] = this.DecimalFormat(this.ChgDBNullToValue(rowFooter["KINGAKU_SOU_KEI"], 0));
                        // 運賃消費税
                        rowFooter["SHOHIZEI_SOU_TOTAL"] = this.DecimalFormat(this.ChgDBNullToValue(rowFooter["SHOHIZEI_SOU_TOTAL"], 0));
                        // 運賃合計金額
                        rowFooter["KINGAKU_SOU_TOTAL"] = this.DecimalFormat(this.ChgDBNullToValue(rowFooter["KINGAKU_SOU_TOTAL"], 0));

                        // 値設定
                        muRow.Cells["KAHEN3_VALUE"].Value = rowFooter["SHOUMI_SOU_KEI"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = rowFooter["KINGAKU_SOU_KEI"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = rowFooter["SHOHIZEI_SOU_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = rowFooter["KINGAKU_SOU_TOTAL"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_Detail", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 明細CellのAlign設定
        /// <summary>
        /// 明細CellのAlign設定
        /// </summary>
        /// <param name="cell">セール</param>
        private void SetCellAlign(GrapeCity.Win.MultiRow.Cell cell, string alignFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(cell, alignFlg);

                switch (alignFlg)
                {
                    case "1":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                        break;
                    case "2":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                        break;
                    case "3":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
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
                //LogUtility.DebugMethodEnd(breakNo);
            }
        }
        #endregion

        #region 明細中のヘッダスタイル設定
        /// <summary>
        /// 明細中のヘッダスタイル設定
        /// </summary>
        /// <param name="cell">セール</param>
        private void SetTitleCellStyle(GrapeCity.Win.MultiRow.Cell cell)
        {
            try
            {

                cell.Style.BackColor = Color.FromArgb(0, 105, 51);
                cell.Style.ForeColor = Color.FromArgb(255, 255, 255);
                cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
            }
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

        #region 単価、数量の共通フォーマット
        /// <summary>
        /// 単価、数量の共通フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string SuuryouAndTankFormat(object obj, String format)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(obj, format);

                decimal num = 0;
                decimal.TryParse(Convert.ToString(obj), out num);

                returnVal = string.Format("{0:" + format + "}", num);

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

        #region 金額の共通フォーマット
        /// <summary>
        /// 金額の共通フォーマットメソッド
        /// 単価などM_SYS_INFO等にフォーマットが設定されている
        /// ものについては使用しないでください
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string DecimalFormat(object obj)
        {
            string format = "#,##0";
            decimal num = 0;
            decimal.TryParse(Convert.ToString(obj), out num);
            return string.Format("{0:" + format + "}", num);
        }
        #endregion

        #region その他(使わない)
        public int Search()
        {
            return 0;
        }
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
    }
}
