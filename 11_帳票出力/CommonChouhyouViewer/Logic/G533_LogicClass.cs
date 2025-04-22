// $Id: G533_LogicClass.cs 19741 2014-04-23 01:08:32Z seven1@bh.mbn.or.jp $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlTypes;
using System.Xml;
using System.Collections.ObjectModel;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Quill.Attrs;
using CommonChouhyouPopup.App;
using System.Drawing;
using System.IO;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G533_LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIFormG533 form;

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

        private bool groupLabelFlg = false;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 明細
        /// </summary>
        private GcCustomMultiRow meisai;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal G533_LogicClass(UIFormG533 targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;

                // 明細
                this.meisai = targetForm.meisai;

                // ControlUtility
                this.controlUtil = new ControlUtility();

                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();

                // DTO
                this.dto = new DTOClass();

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
                this.parentForm.ProcessButtonPanel.Visible = false;

                // システム情報を取得
                this.GetSysInfoInit();

                // イベントの初期化処理
                this.EventInit();

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

                //// ﾎﾞﾀﾝEnabled制御
                //var controlUtil = new ControlUtility();
                //foreach (var button in buttonSetting)
                //{
                //    var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                //    if (cont != null && !string.IsNullOrEmpty(cont.Text))
                //    {
                //        cont.Enabled = true;
                //    }
                //}

                //// 処理No　Enabled制御
                //this.parentForm.txb_process.Enabled = true;
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
                returnVal = buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
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

                //CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);
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

                // ボタンのテキストを初期化
                this.ButtonInit();

                // ヘッダ設定
                this.SetHeader();

                // 明細設定
                this.SetDetail();

                // フッター設定
                this.SetFooter();

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
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ヘッダ値設定
        /// <summary>
        /// ヘッダ値設定
        /// </summary>
        private void SetHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string strYear = "";
                string strMonth = "";

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return;
                }

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Header"];
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
                        && ctrl.GetType() != typeof(Label)
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
                // ****************************************
                // 帳票情報データレコード定義のHeaderの値設定
                // ****************************************
                // 明細Headerループ
                foreach (var cell in this.form.meisai.ColumnHeaders[0].Cells)
                {
                    // 設定された場合
                    if (dt.Columns.Contains(cell.Name))
                    {
                        // ヘッダ設定
                        cell.Value = row[cell.Name].ToString();
                        continue;
                    }
                    else
                    {
                        // ヘッダクリア
                        cell.Value = string.Empty;
                    }

                    // 年設定
                    string cellNameYear = cell.Name.Replace("_YYYY", "");
                    if (dt.Columns.Contains(cellNameYear))
                    {
                        if (row[cellNameYear].ToString().Length == 7 &&
                            !strYear.Equals(row[cellNameYear].ToString().Substring(0, 4)))
                        {
                            strYear = row[cellNameYear].ToString().Substring(0, 4);
                            cell.Value = strYear + "年";
                        }
                    }

                    // 月設定
                    string cellNameMonth = cell.Name.Replace("_MM", "");
                    if (dt.Columns.Contains(cellNameMonth) && row[cellNameMonth].ToString().Length == 7)
                    {
                        strMonth = row[cellNameMonth].ToString().Substring(5, 2);
                        cell.Value = int.Parse(strMonth).ToString() + "月";
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

        #region 明細値設定
        /// <summary>
        /// 明細値設定
        /// </summary>
        private void SetDetail()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Detail"))
                {
                    // 処理終了
                    return;
                }

                // Detailテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // ヘッダ情報取得
                var dtHeader = this.form.ReportInfo.DataTableList["Header"];

                // ブレーク制御用番号
                int breakNo = -1;
                // MultiRowの行
                GrapeCity.Win.MultiRow.Row muRow;
                // 
                DataRow row = null;
                // 明細データループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Row取得
                    row = dt.Rows[i];

                    // 明細項目出力
                    this.meisai.Rows.Add();
                    muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                    // CellのAlignスタイル設定
                    //this.SetCellAlign(muRow.Cells["GROUP_LABEL"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_1_CD"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_1_NAME"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_2_CD"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_2_NAME"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_3_CD"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_3_NAME"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_4_CD"], "3");
                    //this.SetCellAlign(muRow.Cells["FILL_COND_4_NAME"], "3");

                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_1"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_2"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_3"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_4"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_5"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_6"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_7"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_8"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_9"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_10"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_11"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_12"], "2");
                    //this.SetCellAlign(muRow.Cells["OUTPUT_YEAR_MONTH_TOTAL"], "2");

                    // 値設定
                    if (i == 0 || breakNo > -1)
                    {
                        if (!System.DBNull.Value.Equals(row["GROUP_LABEL"]))
                        {
                            muRow.Cells["GROUP_LABEL"].Value = row["GROUP_LABEL"].ToString();

                            // groupLabelFlgを設定
                            groupLabelFlg = true;
                        }
                    }
                    muRow.Cells["FILL_COND_1_CD"].Value = row["FILL_COND_1_CD"].ToString();
                    muRow.Cells["FILL_COND_1_NAME"].Value = row["FILL_COND_1_NAME"].ToString();
                    muRow.Cells["FILL_COND_2_CD"].Value = row["FILL_COND_2_CD"].ToString();
                    muRow.Cells["FILL_COND_2_NAME"].Value = row["FILL_COND_2_NAME"].ToString();
                    muRow.Cells["FILL_COND_3_CD"].Value = row["FILL_COND_3_CD"].ToString();
                    muRow.Cells["FILL_COND_3_NAME"].Value = row["FILL_COND_3_NAME"].ToString();
                    muRow.Cells["FILL_COND_4_CD"].Value = row["FILL_COND_4_CD"].ToString();
                    muRow.Cells["FILL_COND_4_NAME"].Value = row["FILL_COND_4_NAME"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_1"].Value = row["OUTPUT_YEAR_MONTH_1"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_2"].Value = row["OUTPUT_YEAR_MONTH_2"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_3"].Value = row["OUTPUT_YEAR_MONTH_3"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_4"].Value = row["OUTPUT_YEAR_MONTH_4"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_5"].Value = row["OUTPUT_YEAR_MONTH_5"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_6"].Value = row["OUTPUT_YEAR_MONTH_6"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_7"].Value = row["OUTPUT_YEAR_MONTH_7"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_8"].Value = row["OUTPUT_YEAR_MONTH_8"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_9"].Value = row["OUTPUT_YEAR_MONTH_9"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_10"].Value = row["OUTPUT_YEAR_MONTH_10"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_11"].Value = row["OUTPUT_YEAR_MONTH_11"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_12"].Value = row["OUTPUT_YEAR_MONTH_12"].ToString();
                    muRow.Cells["OUTPUT_YEAR_MONTH_TOTAL"].Value = row["OUTPUT_YEAR_MONTH_TOTAL"].ToString();

                    // 最後行の場合
                    if (i == dt.Rows.Count - 1)
                    {
                        // ブレーク番号
                        breakNo = 1;
                    }
                    // 最後行目ではない場合
                    else
                    {
                        // NextRow取得
                        DataRow nextRow = dt.Rows[i + 1];
                        // グループブレークの場合
                        if (row["GROUP_LABEL"].ToString() != nextRow["GROUP_LABEL"].ToString())
                        {
                            breakNo = 0;
                        }
                        else
                        {
                            breakNo = -1;
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

        #region フッター設定
        /// <summary>
        /// フッター設定
        /// </summary>
        private void SetFooter()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Detail"))
                {
                    // 処理終了
                    return;
                }

                // Footerテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }
                // Row取得
                DataRow row = dt.Rows[dt.Rows.Count - 1];

                // ****************************************
                // ヘッダ値設定
                // ****************************************
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_1"].Value = row["ALL_YEAR_MONTH_TOTAL_1"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_2"].Value = row["ALL_YEAR_MONTH_TOTAL_2"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_3"].Value = row["ALL_YEAR_MONTH_TOTAL_3"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_4"].Value = row["ALL_YEAR_MONTH_TOTAL_4"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_5"].Value = row["ALL_YEAR_MONTH_TOTAL_5"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_6"].Value = row["ALL_YEAR_MONTH_TOTAL_6"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_7"].Value = row["ALL_YEAR_MONTH_TOTAL_7"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_8"].Value = row["ALL_YEAR_MONTH_TOTAL_8"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_9"].Value = row["ALL_YEAR_MONTH_TOTAL_9"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_10"].Value = row["ALL_YEAR_MONTH_TOTAL_10"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_11"].Value = row["ALL_YEAR_MONTH_TOTAL_11"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_YEAR_MONTH_TOTAL_12"].Value = row["ALL_YEAR_MONTH_TOTAL_12"].ToString();
                this.meisai.ColumnFooters[0].Cells["ALL_TOTAL_1"].Value = row["ALL_TOTAL_1"].ToString();

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

        #region 明細CellのAlign設定
        /// <summary>
        /// 明細CellのAlign設定
        /// </summary>
        /// <param name="cell">セール</param>
        private void SetCellAlign(GrapeCity.Win.MultiRow.Cell cell, string alignFlg)
        {
            try
            {
                //LogUtility.DebugMethodStart(cell, alignFlg);

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
                //LogUtility.DebugMethodStart(cell);

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
                //LogUtility.DebugMethodEnd(breakNo);
            }
        }
        #endregion

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

                return result;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
        #endregion

        #region [F6]CSV出力
        /// <summary>
        /// [F12]閉じる　処理
        /// </summary>
        internal void CSVOutput()
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
                    // No.4100-->
                    if (fileName.IndexOf('/') >= 0)
                    {
                        fileName = fileName.Replace('/', '_');
                    }
                    // No.4100<--

                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        // 1) ヘッダ部
                        DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                        DataRow row = dtHeader.Rows[0];
                        List<string> lstHeader = new List<string>();
                        // 削除文字
                        char[] trimChar = { '別' };
                        lstHeader.Add(row["CORP_RYAKU_NAME"].ToString());
                        lstHeader.Add(row["PRINT_DATE"].ToString());
                        lstHeader.Add(row["TITLE"].ToString());
                        lstHeader.Add(row["KYOTEN_NAME_RYAKU"].ToString());
                        if (string.IsNullOrEmpty(row["DENPYOU_DATE_BEGIN"].ToString()) && string.IsNullOrEmpty(row["DENPYOU_DATE_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["DENPYOU_DATE_BEGIN"].ToString() + "～" + row["DENPYOU_DATE_END"].ToString());
                        }
                        // 集計項目名1
                        if (!string.IsNullOrEmpty(row["FILL_COND_1_NAME"].ToString()))
                        {
                            lstHeader.Add("[" + row["FILL_COND_1_NAME"].ToString().TrimEnd(trimChar) + "]");
                        }
                        else
                        {
                            lstHeader.Add(string.Empty);
                        }
                        if (string.IsNullOrEmpty(row["FILL_COND_1_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_1_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_1_CD_BEGIN"].ToString() + "～" + row["FILL_COND_1_CD_END"].ToString());
                        }
                        // 集計項目名2
                        if (!string.IsNullOrEmpty(row["FILL_COND_2_NAME"].ToString()))
                        {
                            lstHeader.Add("[" + row["FILL_COND_2_NAME"].ToString().TrimEnd(trimChar) + "]");
                        }
                        else
                        {
                            lstHeader.Add(string.Empty);
                        }
                        if (string.IsNullOrEmpty(row["FILL_COND_2_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_2_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_2_CD_BEGIN"].ToString() + "～" + row["FILL_COND_2_CD_END"].ToString());
                        }
                        // 集計項目名3
                        if (!string.IsNullOrEmpty(row["FILL_COND_3_NAME"].ToString()))
                        {
                            lstHeader.Add("[" + row["FILL_COND_3_NAME"].ToString().TrimEnd(trimChar) + "]");
                        }
                        else
                        {
                            lstHeader.Add(string.Empty);
                        }
                        if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + "～" + row["FILL_COND_3_CD_END"].ToString());
                        }
                        // 集計項目名4
                        if (!string.IsNullOrEmpty(row["FILL_COND_4_NAME"].ToString()))
                        {
                            lstHeader.Add("[" + row["FILL_COND_4_NAME"].ToString().TrimEnd(trimChar) + "]");
                        }
                        else
                        {
                            lstHeader.Add(string.Empty);
                        }
                        if (string.IsNullOrEmpty(row["FILL_COND_4_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_4_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_4_CD_BEGIN"].ToString() + "～" + row["FILL_COND_4_CD_END"].ToString());
                        }
                        lstHeader.Add(row["UNIT_LABEL"].ToString());

                        // ヘッダ書く
                        sw.WriteLine(string.Join(",", lstHeader));

                        // 2) 見出し部
                        List<string> lstPageHeader = new List<string>();
                        if (groupLabelFlg)
                        {
                            lstPageHeader.Add("グループ区分");
                        }
                        lstPageHeader.Add(row["FILL_COND_1_CD_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_1_NAME_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_2_CD_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_2_NAME_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_3_CD_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_3_NAME_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_4_CD_LABEL"].ToString());
                        lstPageHeader.Add(row["FILL_COND_4_NAME_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_1_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_2_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_3_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_4_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_5_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_6_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_7_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_8_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_9_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_10_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_11_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_12_LABEL"].ToString());
                        lstPageHeader.Add(row["OUTPUT_YEAR_MONTH_TOTAL_LABEL"].ToString());

                        // 見出し部書く
                        sw.WriteLine(string.Join(",", lstPageHeader));


                        //3) 明細部
                        DataTable dtDetail = this.form.ReportInfo.DataTableList["Detail"];
                        foreach (DataRow rowDetail in dtDetail.Rows)
                        {
                            List<string> lstdtDetail = new List<string>();
                            if (groupLabelFlg)
                            {
                                lstdtDetail.Add(rowDetail["GROUP_LABEL"].ToString());
                            }
                            lstdtDetail.Add(rowDetail["FILL_COND_1_CD"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_1_NAME"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_2_CD"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_2_NAME"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_3_CD"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_3_NAME"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_4_CD"].ToString());
                            lstdtDetail.Add(rowDetail["FILL_COND_4_NAME"].ToString());

                            // No.4067-->
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_1"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_2"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_3"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_4"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_5"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_6"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_7"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_8"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_9"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_10"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_11"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_12"].ToString()));
                            lstdtDetail.Add(commaCheck(rowDetail["OUTPUT_YEAR_MONTH_TOTAL"].ToString()));
                            // No.4067<--

                            // 明細部書く
                            sw.WriteLine(string.Join(",", lstdtDetail));
                        }
                        msgLogic.MessageBoxShow("I000", "CSV出力");
                    }
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
                LogUtility.DebugMethodEnd();
            }
        }

        // No.4067-->
        /// <summary>
        /// CSV出力データのカンマチェック処理
        /// カンマがあるデータは""で括る
        /// </summary>
        private string commaCheck(string data)
        {
            string outdata;

            // 「,」がある場合
            if (data.IndexOf(',') >= 0)
            {
                outdata = "\"" + data + "\"";
            }
            else
            {
                outdata = data;
            }
            return outdata;
        }
        // No.4067<--

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

        #region その他(使わない)

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
    }
}
