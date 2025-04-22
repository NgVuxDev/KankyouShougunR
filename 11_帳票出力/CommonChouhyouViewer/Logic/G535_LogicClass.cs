// $Id: G535_LogicClass.cs 19741 2014-04-23 01:08:32Z seven1@bh.mbn.or.jp $

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
    internal class G535_LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIFormG535 form;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeaderForm headerForm;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 明細
        /// </summary>
        private GcCustomMultiRow meisai;

        private bool groupLabelFlg = false;

        ///// <summary>
        ///// ControlUtility
        ///// </summary>
        //internal ControlUtility controlUtil;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal G535_LogicClass(UIFormG535 targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;

                // 明細
                this.meisai = this.form.meisai;

                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();

                //// ControlUtility
                //this.controlUtil = new ControlUtility();

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
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.parentForm.ProcessButtonPanel.Visible = false;

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

        #region 初期表示処理
        /// <summary>
        /// 初期表示処理
        /// </summary>
        internal bool DisplayInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダ設定
                this.SetHeader();

                // 明細値設定
                this.SetDetail();

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

        #region ヘッダ値設定
        /// <summary>
        /// ヘッダ値設定
        /// </summary>
        private bool SetHeader()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return returnVal;
                }

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Header"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return returnVal;
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

                // ****************************************
                // 明細のヘッダ値設定
                // ****************************************
                // 明細ヘッダループ
                foreach (var cell in this.form.meisai.ColumnHeaders[0].Cells)
                {
                    // 設定された場合
                    if (dt.Columns.Contains(cell.Name))
                    {
                        // ヘッダ設定
                        cell.Value = row[cell.Name].ToString();
                    }
                    else
                    {
                        // ヘッダ設定
                        cell.Value = string.Empty;
                    }
                }

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
                LogUtility.DebugMethodEnd(returnVal);
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

                // MultiRowの行
                GrapeCity.Win.MultiRow.Row muRow;

                String GROUP_LABEL = "";



                // 明細データループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Row取得
                    DataRow row = dt.Rows[i];
                    // 一行目（集計項目行出力）
                    this.meisai.Rows.Add();
                    muRow = this.meisai.Rows[i];

                    if (!System.DBNull.Value.Equals(row["GROUP_LABEL"]))
                    {
                        // groupLabelFlgを設定
                        groupLabelFlg = true;
                    }

                    if (!GROUP_LABEL.Equals(row["GROUP_LABEL"].ToString()))
                    {
                        muRow.Cells["GROUP_LABEL"].Value = row["GROUP_LABEL"].ToString();
                    }
                    GROUP_LABEL = row["GROUP_LABEL"].ToString();
                    muRow.Cells["FILL_COND_1_CD"].Value = row["FILL_COND_1_CD"].ToString();
                    muRow.Cells["FILL_COND_1_NAME"].Value = row["FILL_COND_1_NAME"].ToString();
                    muRow.Cells["FILL_COND_2_CD"].Value = row["FILL_COND_2_CD"].ToString();
                    muRow.Cells["FILL_COND_2_NAME"].Value = row["FILL_COND_2_NAME"].ToString();
                    muRow.Cells["FILL_COND_3_CD"].Value = row["FILL_COND_3_CD"].ToString();
                    muRow.Cells["FILL_COND_3_NAME"].Value = row["FILL_COND_3_NAME"].ToString();
                    muRow.Cells["FILL_COND_4_CD"].Value = row["FILL_COND_4_CD"].ToString();
                    muRow.Cells["FILL_COND_4_NAME"].Value = row["FILL_COND_4_NAME"].ToString();
                    muRow.Cells["OUTPUT_ITEM_1"].Value = row["OUTPUT_ITEM_1"].ToString();
                    muRow.Cells["OUTPUT_ITEM_2"].Value = row["OUTPUT_ITEM_2"].ToString();
                    muRow.Cells["OUTPUT_ITEM_3"].Value = row["OUTPUT_ITEM_3"].ToString();
                    muRow.Cells["OUTPUT_ITEM_4"].Value = row["OUTPUT_ITEM_4"].ToString();
                    // CellのAlignスタイル設定
                    this.SetCellAlign(muRow.Cells["GROUP_LABEL"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_1_CD"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_1_NAME"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_2_CD"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_2_NAME"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_3_CD"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_3_NAME"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_4_CD"], "3");
                    this.SetCellAlign(muRow.Cells["FILL_COND_4_NAME"], "3");
                    this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_1"], "2");
                    this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_2"], "2");
                    this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_3"], "2");
                    this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_4"], "2");
                }

                // Row取得
                DataRow footerRow = dt.Rows[dt.Rows.Count - 1];
                // 一行目（集計項目行出力）
                this.meisai.Rows.Add();
                var footerMuRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                // 背景色を設定する
                this.SetTitleCellStyle(footerMuRow.Cells["GROUP_LABEL"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_1_CD"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_1_NAME"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_2_CD"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_2_NAME"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_3_CD"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_3_NAME"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_4_CD"]);
                this.SetTitleCellStyle(footerMuRow.Cells["FILL_COND_4_NAME"]);

                footerMuRow.Cells["GROUP_LABEL"].Value = "総合計";

                footerMuRow.Cells["OUTPUT_ITEM_1"].Value = footerRow["ALL_TOTAL_1"].ToString();
                footerMuRow.Cells["OUTPUT_ITEM_2"].Value = footerRow["ALL_TOTAL_2"].ToString();
                footerMuRow.Cells["OUTPUT_ITEM_3"].Value = footerRow["ALL_TOTAL_3"].ToString();
                footerMuRow.Cells["OUTPUT_ITEM_4"].Value = footerRow["ALL_TOTAL_4"].ToString();

                // CellのAlignスタイル設定
                this.SetCellAlign(footerMuRow.Cells["GROUP_LABEL"], "1");

                this.SetCellAlign(footerMuRow.Cells["OUTPUT_ITEM_1"], "2");
                this.SetCellAlign(footerMuRow.Cells["OUTPUT_ITEM_2"], "2");
                this.SetCellAlign(footerMuRow.Cells["OUTPUT_ITEM_3"], "2");
                this.SetCellAlign(footerMuRow.Cells["OUTPUT_ITEM_4"], "2");

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
                cell.Style.SelectionBackColor = cell.Style.BackColor;
                cell.Style.SelectionForeColor = cell.Style.ForeColor;
                ((GcCustomTextBoxCell)cell).AutoChangeBackColorEnabled = false;
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
                // LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region CSV出力処理

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
                    // No.4100-->
                    if (fileName.IndexOf('/') >= 0)
                    {
                        fileName = fileName.Replace('/', '_');
                    }
                    // No.4100<--

                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(0)))
                    {
                        string strCsv = string.Empty;
                        // タイトル文字列取得
                        strCsv = this.GetCsvTitleString();
                        // タイトル書く
                        sw.WriteLine(strCsv);

                        // 明細ヘッダ文字列取得
                        strCsv = this.GetCsvHeaderString(groupLabelFlg);
                        // 明細ヘッダ書く
                        sw.WriteLine(strCsv);

                        // 明細データ出力
                        strCsv = this.GetCsvMeisaiString(groupLabelFlg);
                        // 明細データ書く
                        sw.WriteLine(strCsv);
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
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return returnVal;
                }

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Header"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return returnVal;
                }

                // 削除文字
                char[] trimChar = { '別' };

                // ヘッダ値出力
                DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];

                List<string> csvItems = new List<string>();
                // 会社名
                csvItems.Add(row["CORP_RYAKU_NAME"].ToString());
                // 発行日時
                csvItems.Add(row["PRINT_DATE"].ToString());
                // 伝票種類名
                csvItems.Add(row["DENPYOU_SHURUI"].ToString());
                // タイトル
                csvItems.Add(row["TITLE"].ToString());
                // 拠点
                csvItems.Add(row["KYOTEN_NAME_RYAKU"].ToString());
                // 日付指定範囲
                if (string.IsNullOrEmpty(row["DENPYOU_DATE_BEGIN"].ToString()) && string.IsNullOrEmpty(row["DENPYOU_DATE_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["DENPYOU_DATE_BEGIN"].ToString() + " ～ " + row["DENPYOU_DATE_END"].ToString());
                }
                // 集計項目1
                if (!string.IsNullOrEmpty(row["FILL_COND_1_NAME"].ToString()))
                {
                    csvItems.Add("[" + row["FILL_COND_1_NAME"].ToString().TrimEnd(trimChar) + "]");
                }
                else
                {
                    csvItems.Add(string.Empty);
                }
                // 集計項目1CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_1_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_1_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_1_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_1_CD_END"].ToString());
                }
                // 集計項目2
                if (!string.IsNullOrEmpty(row["FILL_COND_2_NAME"].ToString()))
                {
                    csvItems.Add("[" + row["FILL_COND_2_NAME"].ToString().TrimEnd(trimChar) + "]");
                }
                else
                {
                    csvItems.Add(string.Empty);
                }
                // 集計項目2CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_2_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_2_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_2_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_2_CD_END"].ToString());
                }
                // 集計項目3
                if (!string.IsNullOrEmpty(row["FILL_COND_3_NAME"].ToString()))
                {
                    csvItems.Add("[" + row["FILL_COND_3_NAME"].ToString().TrimEnd(trimChar) + "]");
                }
                else
                {
                    csvItems.Add(string.Empty);
                }
                // 集計項目3CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + "～" + row["FILL_COND_3_CD_END"].ToString());
                }
                // 集計項目4
                if (!string.IsNullOrEmpty(row["FILL_COND_4_NAME"].ToString()))
                {
                    csvItems.Add("[" + row["FILL_COND_4_NAME"].ToString().TrimEnd(trimChar) + "]");
                }
                else
                {
                    csvItems.Add(string.Empty);
                }
                // 集計項目3CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_4_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_4_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_4_CD_BEGIN"].ToString() + "～" + row["FILL_COND_4_CD_END"].ToString());
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
        private string GetCsvHeaderString(bool groupLabelFlg)
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Header"))
                {
                    // 処理終了
                    return returnVal;
                }

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Header"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return returnVal;
                }

                List<string> outputItems = new List<string> { "FILL_COND_1_CD_LABEL"      // 集計項目CD1
                                                            ,"FILL_COND_1_NAME_LABEL"    // 集計項目名1
                                                            , "FILL_COND_2_CD_LABEL"      // 集計項目CD2
                                                            , "FILL_COND_2_NAME_LABEL"    // 集計項目名2
                                                            , "FILL_COND_3_CD_LABEL"      // 集計項目CD3
                                                            , "FILL_COND_3_NAME_LABEL"    // 集計項目名3
                                                            , "FILL_COND_4_CD_LABEL"      // 集計項目CD4
                                                            , "FILL_COND_4_NAME_LABEL"    // 集計項目名4
                                                            , "OUTPUT_ITEM_1_LABEL"       // 出力項目1
                                                            , "OUTPUT_ITEM_2_LABEL"       // 出力項目2
                                                            , "OUTPUT_ITEM_3_LABEL"       // 出力項目3
                                                            , "OUTPUT_ITEM_4_LABEL"       // 出力項目4
                                                            };

                // ヘッダ値出力
                DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];

                List<string> csvItems = new List<string>();

                if (groupLabelFlg)
                {
                    csvItems.Add("グループ区分");
                }

                foreach (var item in outputItems)
                {
                    csvItems.Add(row[item].ToString());
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

        #region Csv明細データ文字列を作成
        /// <summary>
        /// Csv明細データ文字列を作成
        /// </summary>
        /// <returns>明細データ文字列（カンマで区切済み）</returns>
        private string GetCsvMeisaiString(bool groupLabelFlg)
        {
            string returnVal = string.Empty;

            try
            {
                LogUtility.DebugMethodStart();

                // 対象テーブル存在しない場合
                if (!this.form.ReportInfo.DataTableList.ContainsKey("Detail"))
                {
                    // 処理終了
                    return returnVal;
                }

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return returnVal;
                }

                List<string> outputItems = new List<string> { "GROUP_LABEL"	        // グループラベル
                                                            , "FILL_COND_1_CD"	    // 集計項目CD1
                                                            , "FILL_COND_1_NAME"    // 集計項目名1
                                                            , "FILL_COND_2_CD"	    // 集計項目CD2
                                                            , "FILL_COND_2_NAME"	// 集計項目名2
                                                            , "FILL_COND_3_CD"	    // 集計項目CD3
                                                            , "FILL_COND_3_NAME"	// 集計項目名3
                                                            , "FILL_COND_4_CD"	    // 集計項目CD4
                                                            , "FILL_COND_4_NAME"	// 集計項目名4
                                                            , "OUTPUT_ITEM_1"	    // 出力項目1
                                                            , "OUTPUT_ITEM_2"	    // 出力項目2
                                                            , "OUTPUT_ITEM_3"	    // 出力項目3
                                                            , "OUTPUT_ITEM_4"	    // 出力項目4
                                                            };

                List<string> outputItemNoGroupLabel = new List<string> {"FILL_COND_1_CD"	    // 集計項目CD1
                                                            , "FILL_COND_1_NAME"    // 集計項目名1
                                                            , "FILL_COND_2_CD"	    // 集計項目CD2
                                                            , "FILL_COND_2_NAME"	// 集計項目名2
                                                            , "FILL_COND_3_CD"	    // 集計項目CD3
                                                            , "FILL_COND_3_NAME"	// 集計項目名3
                                                            , "FILL_COND_4_CD"	    // 集計項目CD4
                                                            , "FILL_COND_4_NAME"	// 集計項目名4
                                                            , "OUTPUT_ITEM_1"	    // 出力項目1
                                                            , "OUTPUT_ITEM_2"	    // 出力項目2
                                                            , "OUTPUT_ITEM_3"	    // 出力項目3
                                                            , "OUTPUT_ITEM_4"	    // 出力項目4
                                                            };

                // ヘッダ値出力
                DataTable dtDetail = this.form.ReportInfo.DataTableList["Detail"];
                foreach (DataRow row in dtDetail.Rows)
                {
                    List<string> csvItems = new List<string>();

                    if (groupLabelFlg)
                    {
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
                    }
                    else
                    {
                        foreach (var item in outputItemNoGroupLabel)
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
                    }

                    // カンマで区切り
                    returnVal += string.Join(",", csvItems) + "\r\n";
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

        #region その他(使わない)
        public int Search()
        {
            throw new NotImplementedException();
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
    }
}
