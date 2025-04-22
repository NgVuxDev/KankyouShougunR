// $Id: G534_LogicClass.cs 19741 2014-04-23 01:08:32Z seven1@bh.mbn.or.jp $

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
    internal class G534_LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIFormG534 form;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeaderForm headerForm;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;


        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 明細
        /// </summary>
        private GcCustomMultiRow meisai;

        private bool groupLabelFlg = false;

        ///// <summary>
        ///// システム情報のDao
        ///// </summary>
        //private IM_SYS_INFODao sysInfoDao;

        ///// <summary>
        ///// システム情報のエンティティ
        ///// </summary>
        //private M_SYS_INFO sysInfoEntity;

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
        internal G534_LogicClass(UIFormG534 targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;

                // 明細
                this.meisai = targetForm.meisai;

                // ControlUtility
                //this.controlUtil = new ControlUtility();
                // メッセージ表示オブジェクト
                msgLogic = new MessageBoxShowLogic();
                // DTO
                this.dto = new DTOClass();

                // システム情報Dao
                //this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

                // システム情報を取得
                //this.GetSysInfoInit();

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
                //var controlUtil = new ControlUtility();
                //foreach (var button in buttonSetting)
                //{
                //    var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                //    if (cont != null && !string.IsNullOrEmpty(cont.Text))
                //    {
                //        cont.Enabled = true;
                //    }
                //}

                // 処理No　Enabled制御
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

                //検索ボタン(F8)イベント生成
                //parentForm.bt_func8.Click += new EventHandler(this.form.Search);
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

        //#region header初期化処理
        ///// <summary>
        ///// header設定
        ///// </summary>
        //public void SetHeader(UIHeaderForm headForm)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(headForm);

        //        this.headerForm = headForm;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("SetHeader", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        //#endregion

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
                //this.ButtonInit();

                // ヘッダと明細を設定
                this.SetHeader();

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
        private void SetHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();

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
                    }
                    else
                    {
                        // ヘッダクリア
                        cell.Value = string.Empty;
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
                // 明細データループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Row取得
                    DataRow row = dt.Rows[i];

                    // 最初行または集計項目が変わる場合
                    if (i == 0 || breakNo > -1)
                    {
                        if (!System.DBNull.Value.Equals(row["GROUP_LABEL"]))
                        {
                            // 一行目（グループ名）
                            this.meisai.Rows.Add();
                            muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                            // CellのAlignスタイル設定
                            this.SetCellAlign(muRow.Cells["ROW_NO"], "1");

                            // 値設定
                            muRow.Cells["ROW_NO"].Value = row["GROUP_LABEL"].ToString();

                            // groupLabelFlgを設定
                            groupLabelFlg = true;
                        }
                    }

                    // 明細項目出力
                    this.meisai.Rows.Add();
                    muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                    // CellのAlignスタイル設定
                    this.SetCellAlign(muRow.Cells["ROW_NO"], "2");
                    this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_1"], "2");
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_CD_1"], dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_1"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_MEI_1"], dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_1"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_CD_2"], dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_2"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_MEI_2"], dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_2"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_CD_3"], dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_3"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_MEI_3"], dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_3"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_CD_4"], dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_4"].ToString());
                    //this.SetCellAlign(muRow.Cells["SK_ITEM_MEI_4"], dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_4"].ToString());
                    //this.SetCellAlign(muRow.Cells["ITEM_1"], "2");

                    // 値設定
                    muRow.Cells["ROW_NO"].Value = row["ROW_NO"].ToString();
                    muRow.Cells["FILL_COND_1_CD"].Value = row["FILL_COND_1_CD"].ToString();
                    muRow.Cells["FILL_COND_1_NAME"].Value = row["FILL_COND_1_NAME"].ToString();
                    muRow.Cells["FILL_COND_2_CD"].Value = row["FILL_COND_2_CD"].ToString();
                    muRow.Cells["FILL_COND_2_NAME"].Value = row["FILL_COND_2_NAME"].ToString();
                    muRow.Cells["FILL_COND_3_CD"].Value = row["FILL_COND_3_CD"].ToString();
                    muRow.Cells["FILL_COND_3_NAME"].Value = row["FILL_COND_3_NAME"].ToString();
                    muRow.Cells["FILL_COND_4_CD"].Value = row["FILL_COND_4_CD"].ToString();
                    muRow.Cells["FILL_COND_4_NAME"].Value = row["FILL_COND_4_NAME"].ToString();
                    muRow.Cells["OUTPUT_ITEM_1"].Value = row["OUTPUT_ITEM_1"].ToString();

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

                    // 最後行の場合
                    if (breakNo > 0)
                    {
                        // 総合計項目出力
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["ROW_NO"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_1_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_1_NAME"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_2_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_2_NAME"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_3_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_3_NAME"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["FILL_COND_4_NAME"]);
                        //this.SetTitleCellStyle(muRow.Cells["OUTPUT_ITEM_1"]);

                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["FILL_COND_4_NAME"], "1");
                        // 値設定
                        muRow.Cells["FILL_COND_4_NAME"].Value = "総合計";
                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["OUTPUT_ITEM_1"], "2");
                        // 値設定
                        muRow.Cells["OUTPUT_ITEM_1"].Value = row["ALL_TOTAL_1"].ToString();
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
                //LogUtility.DebugMethodEnd(breakNo);
            }
        }
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

        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
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

                // ヘッダ値出力
                DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];
                List<string> csvItems = new List<string>();
                csvItems.Add(row["CORP_RYAKU_NAME"].ToString());
                csvItems.Add(row["PRINT_DATE"].ToString());
                csvItems.Add(row["DENPYOU_SHURUI"].ToString());
                csvItems.Add(row["TITLE"].ToString());
                csvItems.Add(row["KYOTEN_NAME_RYAKU"].ToString());
                if (string.IsNullOrEmpty(row["DENPYOU_DATE_BEGIN"].ToString()) && string.IsNullOrEmpty(row["DENPYOU_DATE_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["DENPYOU_DATE_BEGIN"].ToString() + " ～ " + row["DENPYOU_DATE_END"].ToString());
                }
                //末尾1文字"別"を削除
                char[] a = { '別' };
                csvItems.Add("[" + row["FILL_COND_1_NAME"].ToString().TrimEnd(a) + "]");
                if (string.IsNullOrEmpty(row["FILL_COND_1_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_1_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_1_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_1_CD_END"].ToString());
                }
                csvItems.Add("[" + row["FILL_COND_2_NAME"].ToString().TrimEnd(a) + "]");
                if (string.IsNullOrEmpty(row["FILL_COND_2_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_2_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_2_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_2_CD_END"].ToString());
                }
                csvItems.Add("[" + row["FILL_COND_3_NAME"].ToString().TrimEnd(a) + "]");
                if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_3_CD_END"].ToString());
                }
                csvItems.Add("[" + row["FILL_COND_4_NAME"].ToString().TrimEnd(a) + "]");
                if (string.IsNullOrEmpty(row["FILL_COND_4_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_4_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_4_CD_BEGIN"].ToString() + " ～ " + row["FILL_COND_4_CD_END"].ToString());
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

                List<string> outputItems1 = new List<string> { "ROW_NO_LABEL"             // No
                                                            , "FILL_COND_1_CD_LABEL"      // 集計項目CD1
                                                            , "FILL_COND_1_NAME_LABEL"    // 集計項目名1
                                                            , "FILL_COND_2_CD_LABEL"      // 集計項目CD2
                                                            , "FILL_COND_2_NAME_LABEL"    // 集計項目名2
                                                            , "FILL_COND_3_CD_LABEL"      // 集計項目CD3
                                                            , "FILL_COND_3_NAME_LABEL"    // 集計項目名3
                                                            , "FILL_COND_4_CD_LABEL"      // 集計項目CD4
                                                            , "FILL_COND_4_NAME_LABEL"     // 集計項目名4
                                                            , "OUTPUT_ITEM_1_LABEL"        // 出力項目１
                                                            };

                // ヘッダ値出力
                DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];

                List<string> csvItems = new List<string>();

                if (groupLabelFlg)
                {
                    csvItems.Add("グループ区分");
                }

                foreach (var item in outputItems1)
                {
                    csvItems.Add(row[item].ToString());
                }

                //// 明細番号
                //csvItems.Add("明細番号");

                //foreach (var item in outputItems2)
                //{
                //    csvItems.Add(row[item].ToString());
                //}

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

                List<string> outputItems = new List<string> { "GROUP_LABEL"	            //グループラベル
                                                            , "ROW_NO"	                //明細番号
                                                            , "FILL_COND_1_CD"	        //集計項目CD1
                                                            , "FILL_COND_1_NAME"	    //集計項目名1
                                                            , "FILL_COND_2_CD"	        //集計項目CD2
                                                            , "FILL_COND_2_NAME"	    //集計項目名2
                                                            , "FILL_COND_3_CD"	        //集計項目CD3
                                                            , "FILL_COND_3_NAME"	    //集計項目名4
                                                            , "FILL_COND_4_CD"	        //集計項目CD4
                                                            , "FILL_COND_4_NAME"	    //集計項目名4
                                                            , "OUTPUT_ITEM_1"	        //出力項目１
                                                            };

                List<string> outputItemsNoGroupLabel = new List<string> {"ROW_NO"	    //明細番号
                                                            , "FILL_COND_1_CD"	        //集計項目CD1
                                                            , "FILL_COND_1_NAME"	    //集計項目名1
                                                            , "FILL_COND_2_CD"	        //集計項目CD2
                                                            , "FILL_COND_2_NAME"	    //集計項目名2
                                                            , "FILL_COND_3_CD"	        //集計項目CD3
                                                            , "FILL_COND_3_NAME"	    //集計項目名4
                                                            , "FILL_COND_4_CD"	        //集計項目CD4
                                                            , "FILL_COND_4_NAME"	    //集計項目名4
                                                            , "OUTPUT_ITEM_1"	        //出力項目１
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
                        foreach (var item in outputItemsNoGroupLabel)
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
