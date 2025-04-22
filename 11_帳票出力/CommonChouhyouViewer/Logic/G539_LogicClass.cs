// $Id: G539_LogicClass.cs 15601 2014-02-04 06:08:35Z ogawa@takumi-sys.co.jp $

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
    internal class G539_LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIFormG539 form;

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
        /// <summary>
        /// 画面でMultirowのCell名一覧
        /// </summary>
        private string[] MultirowCellNames = { "ITEM_1", "ITEM_2", "ITEM_3", "ITEM_4", "ITEM_5", "ITEM_6", "ITEM_7", "ITEM_8", "ITEM_9" };

        #region Detail-Header部cell名
        //[SK_LABEL + OUTPUT_LABEL]は共通からDetail-Header部全部cell名

        // 集計項目ラベル
        private string[] SK_LABEL = { "FILL_COND_1_CD_LABEL", "FILL_COND_1_NAME_LABEL", "FILL_COND_2_CD_LABEL", "FILL_COND_2_NAME_LABEL", "FILL_COND_3_CD_LABEL", "FILL_COND_3_NAME_LABEL" };

        // 出力項目ラベル
        private string[] OUTPUT_LABEL = { "OUTPUT_DENPYOU_1_LABEL","OUTPUT_DENPYOU_2_LABEL","OUTPUT_DENPYOU_3_LABEL","OUTPUT_DENPYOU_4_LABEL"
                                         ,"OUTPUT_DENPYOU_5_LABEL","OUTPUT_DENPYOU_6_LABEL","OUTPUT_DENPYOU_7_LABEL","OUTPUT_DENPYOU_8_LABEL"
                                         ,"OUTPUT_MEISAI_1_LABEL","OUTPUT_MEISAI_2_LABEL","OUTPUT_MEISAI_3_LABEL","OUTPUT_MEISAI_4_LABEL"
                                         ,"OUTPUT_MEISAI_5_LABEL","OUTPUT_MEISAI_6_LABEL","OUTPUT_MEISAI_7_LABEL","OUTPUT_MEISAI_8_LABEL" };
        // 画面Detail-Header部処理後の出力項目ラベル
        private string[] newOUTPUT_LABEL = new string[8];

        #endregion

        #region Detail-Header部cell名
        /* 明細部の集計項目:rowType1
         * 明細部の出力項目:rowType2
         * 明細部のグループ3合計:rowType3
         * 明細部のグループ2合計:rowType4
         * 明細部のグループ1合計:rowType5
         * 明細部の総合計:rowType6
         */

        // datatable項目DetialのrowType1のCell名一覧:集計の明細
        private string[] rowType1Names = { "FILL_COND_1_CD", "FILL_COND_1_NAME", "FILL_COND_2_CD", "FILL_COND_2_NAME", "FILL_COND_3_CD", "FILL_COND_3_NAME" };

        // datatable項目DetialのrowType2のCell名一覧:出力の明細
        //private string[] rowType2Names = { "ITEM_1", "ITEM_2", "ITEM_3", "ITEM_4", "ITEM_5", "ITEM_6", "ITEM_7", "ITEM_8" };
        private string[] rowType2Names = new string[8];
        // datatable項目DetialのrowType2の配置区分一覧:出力の明細
        private string[] rowType2Align = new string[8];

        // datatable項目DetialのrowType3のCell名一覧:グループ3合計
        private string[] rowType3Names = { "FILL_COND_3_TOTAL_1", "FILL_COND_3_TOTAL_2", "FILL_COND_3_TOTAL_3", "FILL_COND_3_TOTAL_4", "FILL_COND_3_TOTAL_5", "FILL_COND_3_TOTAL_6", "FILL_COND_3_TOTAL_7", "FILL_COND_3_TOTAL_8" };

        // datatable項目DetialのrowType4のCell名一覧:グループ2合計
        private string[] rowType4Names = { "FILL_COND_2_TOTAL_1", "FILL_COND_2_TOTAL_2", "FILL_COND_2_TOTAL_3", "FILL_COND_2_TOTAL_4", "FILL_COND_2_TOTAL_5", "FILL_COND_2_TOTAL_6", "FILL_COND_2_TOTAL_7", "FILL_COND_2_TOTAL_8" };

        // datatable項目DetialのrowType5のCell名一覧:グループ1合計
        private string[] rowType5Names = { "FILL_COND_1_TOTAL_1", "FILL_COND_1_TOTAL_2", "FILL_COND_1_TOTAL_3", "FILL_COND_1_TOTAL_4", "FILL_COND_1_TOTAL_5", "FILL_COND_1_TOTAL_6", "FILL_COND_1_TOTAL_7", "FILL_COND_1_TOTAL_8" };

        //datatable項目 DetialのrowType6のCell名一覧:総合計
        private string[] rowType6Names = { "ALL_TOTAL_1", "ALL_TOTAL_2", "ALL_TOTAL_3", "ALL_TOTAL_4", "ALL_TOTAL_5", "ALL_TOTAL_6", "ALL_TOTAL_7", "ALL_TOTAL_8" };

        #endregion

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal G539_LogicClass(UIFormG539 targetForm)
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

                ClearControls();
                SetData();

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

        #region 初期データ設定
        /// <summary>
        /// データ取得処理
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

        #region Header初期表示
        /// <summary>
        /// Header初期表示
        /// </summary>
        public void Init_Header()
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
                // 明細のヘッダ値設定
                // ****************************************
                // 明細ヘッダループ
                //集計項目CDラベル設定
                GrapeCity.Win.MultiRow.CellCollection headerRowCells = this.form.gcCustomMultiRow1.ColumnHeaders[0].Cells;
                //ヘッダ部クリア
                foreach (var cell in headerRowCells)
                {
                    cell.Value = string.Empty;
                }

                int headerRowCell1 = 0;
                foreach (string cellName in SK_LABEL)
                {
                    // 設定された場合
                    if (dt.Columns.Contains(cellName))
                    {
                        // ヘッダ設定
                        headerRowCells[headerRowCell1].Value = row[cellName].ToString();
                        headerRowCell1++;
                    }
                }
                //出力（伝票・明細）項目ラベル設定
                int headerRowCell2 = 10;
                foreach (string cellName in OUTPUT_LABEL)
                {
                    if (headerRowCell2 > 17)
                    {
                        //共通からのDatatable不正,画面表示可の列は８列です。
                        break;
                    }
                    if (dt.Columns.Contains(cellName) && !string.IsNullOrEmpty(dt.Rows[0][cellName].ToString()))
                    {
                        headerRowCells[headerRowCell2].Value = dt.Rows[0][cellName].ToString();
                        //出力（伝票・明細）項目ラベル取得
                        newOUTPUT_LABEL[headerRowCell2 - 10] = cellName;
                        headerRowCell2++;
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

        #region Detail初期表示
        /// <summary>
        /// Detail初期表示
        /// </summary>
        public void Init_Detail()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // Headerテーブル
                var dtHeader = this.form.ReportInfo.DataTableList["Header"];

                // Row取得（1行しかないはず）
                DataRow headerRow = dtHeader.Rows[0];
                string FILL_COND_ID_1_TOTAL_LABEL = headerRow["FILL_COND_ID_1_TOTAL_LABEL"].ToString(); // 集計項目合計ラベル1
                string FILL_COND_ID_2_TOTAL_LABEL = headerRow["FILL_COND_ID_2_TOTAL_LABEL"].ToString(); // 集計項目合計ラベル2
                string FILL_COND_ID_3_TOTAL_LABEL = headerRow["FILL_COND_ID_3_TOTAL_LABEL"].ToString(); // 集計項目合計ラベル3
                string ALL_TOTAL_LABEL = headerRow["ALL_TOTAL_LABEL"].ToString(); // 集計項目総合計ラベル

                //string[] SK_ITEM_ALIGN = {dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_1"].ToString() //集計項目CD1配置区分
                //                         ,dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_1"].ToString()//集計項目名1配置区分
                //                         ,dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_2"].ToString()//集計項目CD2配置区分
                //                         ,dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_2"].ToString()//集計項目名2配置区分
                //                         ,dtHeader.Rows[0]["SK_ITEM_CD_ALIGN_3"].ToString()//集計項目CD3配置区分
                //                         ,dtHeader.Rows[0]["SK_ITEM_MEI_ALIGN_3"].ToString()//集計項目名3配置区分
                //                         };

                //出力（伝票・明細）項目値と配置区分のcolumn設定
                int strValueCount = 0;
                foreach (string outValue in newOUTPUT_LABEL)
                {
                    if (!string.IsNullOrEmpty(outValue))
                    {
                        rowType2Names[strValueCount] = outValue.Replace("LABEL", "VALUE");
                        rowType2Align[strValueCount] = headerRow[outValue.Replace("LABEL", "ALIGN")].ToString();
                        strValueCount++;
                    }

                }

                // Detailテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // ****************************************
                // 明細部設定
                // ****************************************
                int MultiRowCount = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        // ****************************************
                        // 明細のrowType1出力
                        // ****************************************
                        // Row取得（1行しかないはず）
                        this.form.gcCustomMultiRow1.Rows.Add();

                        for (int iTyp1 = 0; iTyp1 < rowType1Names.Length; iTyp1++)
                        {
                            //値設定
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp1]].Value =
                                dt.Rows[i][rowType1Names[iTyp1]].ToString();
                            //Align配置設定
                            //this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp1]],
                            //                  SK_ITEM_ALIGN[iTyp1]);
                        }
                        MultiRowCount++;
                    }
                    else
                    {

                        //if (dt.Rows[i]["FILL_COND_1_CD"] != dt.Rows[i - 1]["FILL_COND_1_CD"]
                        //  || dt.Rows[i]["FILL_COND_2_CD"] != dt.Rows[i - 1]["FILL_COND_2_CD"]
                        //  || dt.Rows[i]["FILL_COND_3_CD"] != dt.Rows[i - 1]["FILL_COND_3_CD"])
                        if (!dt.Rows[i]["FILL_COND_1_CD"].ToString().Equals(dt.Rows[i - 1]["FILL_COND_1_CD"].ToString())
                            || !dt.Rows[i]["FILL_COND_2_CD"].ToString().Equals(dt.Rows[i - 1]["FILL_COND_2_CD"].ToString())
                            || !dt.Rows[i]["FILL_COND_3_CD"].ToString().Equals(dt.Rows[i - 1]["FILL_COND_3_CD"].ToString()))
                        {
                            // ****************************************
                            // 明細のrowType1出力
                            // ****************************************
                            // Row取得（1行しかないはず）
                            this.form.gcCustomMultiRow1.Rows.Add();

                            for (int iTyp1 = 0; iTyp1 < rowType1Names.Length; iTyp1++)
                            {
                                //値設定
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp1]].Value =
                                    dt.Rows[i][rowType1Names[iTyp1]].ToString();
                                //Align配置設定
                                //this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp1]],
                                //                  SK_ITEM_ALIGN[iTyp1]);
                            }
                            MultiRowCount++;
                        }
                    }

                    // ****************************************
                    // 明細のrowType2出力
                    // ****************************************
                    // Row取得（1行しかないはず）
                    this.form.gcCustomMultiRow1.Rows.Add();

                    //for (int iTyp2 = 0; iTyp2 < rowType2Names.Length; iTyp2++)
                    //{
                    //    //値設定
                    //    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp2 + 1]].Value =
                    //        dt.Rows[i][rowType2Names[iTyp2]].ToString();
                    //    //Align配置設定
                    //    //this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp2 + 1]],
                    //    //                  ITEM_ALIGN[iTyp2]);
                    //}
                    //MultiRowCount++;

                    for (int iTyp2 = 0; iTyp2 < rowType2Names.Length; iTyp2++)
                    {
                        if (!string.IsNullOrEmpty(rowType2Names[iTyp2]))
                        {
                            //値設定
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp2 + 1]].Value =
                                dt.Rows[i][rowType2Names[iTyp2]].ToString();
                            //Align配置設定
                            this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp2 + 1]],
                                              rowType2Align[iTyp2]);
                        }
                    }
                    MultiRowCount++;

                    if (i == (dt.Rows.Count - 1))
                    {
                        // ****************************************
                        // 明細のrowType3出力
                        // ****************************************
                        // Row取得（1行しかないはず）
                        if (!string.IsNullOrEmpty(FILL_COND_ID_3_TOTAL_LABEL))
                        {
                            this.form.gcCustomMultiRow1.Rows.Add();
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_3_TOTAL_LABEL;
                            // ヘッダスタイル設定
                            this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                            for (int iTyp3 = 0; iTyp3 < rowType3Names.Length; iTyp3++)
                            {
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]].Value =
                                    dt.Rows[i][rowType3Names[iTyp3]].ToString();
                                //Align配置設定
                                this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]],
                                                  rowType2Align[iTyp3]);
                            }
                            MultiRowCount++;
                        }
                        // ****************************************
                        // 明細のrowType4出力
                        // ****************************************
                        // Row取得（1行しかないはず）
                        if (!string.IsNullOrEmpty(FILL_COND_ID_2_TOTAL_LABEL))
                        {
                            this.form.gcCustomMultiRow1.Rows.Add();
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_2_TOTAL_LABEL;
                            // ヘッダスタイル設定
                            this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);
                            for (int iTyp4 = 0; iTyp4 < rowType4Names.Length; iTyp4++)
                            {
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]].Value =
                                    dt.Rows[i][rowType4Names[iTyp4]].ToString();
                                //Align配置設定
                                this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]],
                                                  rowType2Align[iTyp4]);
                            }
                            MultiRowCount++;
                        }
                        // ****************************************
                        // 明細のrowType5出力
                        // ****************************************
                        // Row取得（1行しかないはず）
                        if (!string.IsNullOrEmpty(FILL_COND_ID_1_TOTAL_LABEL))
                        {
                            this.form.gcCustomMultiRow1.Rows.Add();
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_1_TOTAL_LABEL;
                            // ヘッダスタイル設定
                            this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                            for (int iTyp5 = 0; iTyp5 < rowType5Names.Length; iTyp5++)
                            {
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp5 + 1]].Value =
                                    dt.Rows[i][rowType5Names[iTyp5]].ToString();
                                //Align配置設定
                                this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp5 + 1]],
                                                  rowType2Align[iTyp5]);
                            }
                            MultiRowCount++;
                        }
                        // ****************************************
                        // 明細のrowType6出力
                        // ****************************************
                        // Row取得（1行しかないはず）
                        if (!string.IsNullOrEmpty(ALL_TOTAL_LABEL))
                        {
                            this.form.gcCustomMultiRow1.Rows.Add();
                            this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = ALL_TOTAL_LABEL;
                            // ヘッダスタイル設定
                            this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                            for (int iTyp6 = 0; iTyp6 < rowType6Names.Length; iTyp6++)
                            {
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp6 + 1]].Value =
                                    dt.Rows[i][rowType6Names[iTyp6]].ToString();
                                //Align配置設定
                                this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp6 + 1]],
                                                  rowType2Align[iTyp6]);
                            }
                            MultiRowCount++;
                        }
                    }
                    else
                    {
                        if (!dt.Rows[i]["FILL_COND_1_CD"].ToString().Equals(dt.Rows[i + 1]["FILL_COND_1_CD"].ToString()))
                        {
                            // ****************************************
                            // 明細のrowType3出力
                            // ****************************************
                            if (!string.IsNullOrEmpty((FILL_COND_ID_3_TOTAL_LABEL)))
                            {
                                // Row取得（1行しかないはず）
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_3_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                                for (int iTyp3 = 0; iTyp3 < rowType3Names.Length; iTyp3++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]].Value =
                                        dt.Rows[i][rowType3Names[iTyp3]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]],
                                                      rowType2Align[iTyp3]);
                                }
                                MultiRowCount++;
                            }
                            // ****************************************
                            // 明細のrowType4出力
                            // ****************************************
                            if (!string.IsNullOrEmpty((FILL_COND_ID_2_TOTAL_LABEL)))
                            {
                                // Row取得（1行しかないはず）
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_2_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                                for (int iTyp4 = 0; iTyp4 < rowType4Names.Length; iTyp4++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]].Value =
                                        dt.Rows[i][rowType4Names[iTyp4]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]],
                                                      rowType2Align[iTyp4]);
                                }
                                MultiRowCount++;
                            }
                            // ****************************************
                            // 明細のrowType5出力
                            // ****************************************
                            // Row取得（1行しかないはず）
                            if (!string.IsNullOrEmpty(FILL_COND_ID_1_TOTAL_LABEL))
                            {
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_1_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                                for (int iTyp5 = 0; iTyp5 < rowType5Names.Length; iTyp5++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp5 + 1]].Value =
                                        dt.Rows[i][rowType5Names[iTyp5]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp5 + 1]],
                                                      rowType2Align[iTyp5]);
                                }
                                MultiRowCount++;
                            }
                        }
                        else if (!dt.Rows[i]["FILL_COND_2_CD"].ToString().Equals(dt.Rows[i + 1]["FILL_COND_2_CD"].ToString()))
                        {
                            // ****************************************
                            // 明細のrowType3出力
                            // ****************************************
                            // Row取得（1行しかないはず）
                            if (!string.IsNullOrEmpty(FILL_COND_ID_3_TOTAL_LABEL))
                            {
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_3_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);
                                for (int iTyp3 = 0; iTyp3 < rowType3Names.Length; iTyp3++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]].Value =
                                        dt.Rows[i][rowType3Names[iTyp3]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]],
                                                      rowType2Align[iTyp3]);
                                }
                                MultiRowCount++;
                            }

                            // ****************************************
                            // 明細のrowType4出力
                            // ****************************************
                            // Row取得（1行しかないはず） 
                            if (!string.IsNullOrEmpty(FILL_COND_ID_2_TOTAL_LABEL))
                            {
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_2_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                                for (int iTyp4 = 0; iTyp4 < rowType4Names.Length; iTyp4++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]].Value =
                                        dt.Rows[i][rowType4Names[iTyp4]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp4 + 1]],
                                                      rowType2Align[iTyp4]);
                                }
                                MultiRowCount++;
                            }
                        }
                        else if (!dt.Rows[i]["FILL_COND_3_CD"].ToString().Equals(dt.Rows[i + 1]["FILL_COND_3_CD"].ToString()))
                        {
                            // ****************************************
                            // 明細のrowType3出力
                            // ****************************************
                            // Row取得（1行しかないはず）
                            if (!string.IsNullOrEmpty(FILL_COND_ID_3_TOTAL_LABEL))
                            {
                                this.form.gcCustomMultiRow1.Rows.Add();
                                this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]].Value = FILL_COND_ID_3_TOTAL_LABEL;
                                // ヘッダスタイル設定
                                this.SetTitleCellStyle(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[0]]);

                                for (int iTyp3 = 0; iTyp3 < rowType3Names.Length; iTyp3++)
                                {
                                    this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]].Value =
                                        dt.Rows[i][rowType3Names[iTyp3]].ToString();
                                    //Align配置設定
                                    this.SetCellAlign(this.form.gcCustomMultiRow1.Rows[MultiRowCount][MultirowCellNames[iTyp3 + 1]],
                                                      rowType2Align[iTyp3]);
                                }
                                MultiRowCount++;
                            }
                        }
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
                this.headerForm.KYOTEN_CD.Text = string.Empty;
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                this.headerForm.ALERT_NUMBER.Text = string.Empty;
                this.headerForm.READ_DATA_NUMBER.Text = string.Empty;

                //PageHeader部
                this.form.DENPYOU_DATE_BEGIN.Text = string.Empty;
                this.form.DENPYOU_DATE_END.Text = string.Empty;
                this.form.FILL_COND_1_NAME.Text = string.Empty;
                this.form.FILL_COND_1_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_1_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_1_CD_END.Text = string.Empty;
                this.form.FILL_COND_1_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_2_NAME.Text = string.Empty;
                this.form.FILL_COND_2_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_2_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_2_CD_END.Text = string.Empty;
                this.form.FILL_COND_2_VALUE_END.Text = string.Empty;
                this.form.FILL_COND_3_NAME.Text = string.Empty;
                this.form.FILL_COND_3_CD_BEGIN.Text = string.Empty;
                this.form.FILL_COND_3_VALUE_BEGIN.Text = string.Empty;
                this.form.FILL_COND_3_CD_END.Text = string.Empty;
                this.form.FILL_COND_3_VALUE_END.Text = string.Empty;

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

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return string.Empty;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return string.Empty;
                    else
                        return obj;
                }
                else if (obj.Equals(null))
                {
                    return string.Empty;
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
                //this.form.ReportInfo.Create(@".\Template\R352-Form.xml", "LAYOUT1", new DataTable());
                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.form.ReportInfo, WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU))
                {
                    formReportPrintPopup.ShowDialog();
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
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(-0)))
                    {
                        // 1) ヘッダ部
                        DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                        DataRow row = dtHeader.Rows[0];

                        // 削除文字
                        char[] trimChar = { '別' };

                        List<string> lstHeader = new List<string>();
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
                        lstHeader.Add("[" + row["FILL_COND_1_NAME"].ToString().TrimEnd(trimChar) + "]");
                        if (string.IsNullOrEmpty(row["FILL_COND_1_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_1_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_1_CD_BEGIN"].ToString() + "～" + row["FILL_COND_1_CD_END"].ToString());
                        }
                        lstHeader.Add("[" + row["FILL_COND_2_NAME"].ToString().TrimEnd(trimChar) + "]");
                        if (string.IsNullOrEmpty(row["FILL_COND_2_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_2_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_2_CD_BEGIN"].ToString() + "～" + row["FILL_COND_2_CD_END"].ToString());
                        }
                        lstHeader.Add("[" + row["FILL_COND_3_NAME"].ToString().TrimEnd(trimChar) + "]");
                        if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                        {
                            lstHeader.Add(string.Empty);
                        }
                        else
                        {
                            lstHeader.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + "～" + row["FILL_COND_3_CD_END"].ToString());
                        }

                        // ヘッダ書く
                        sw.WriteLine(string.Join(",", lstHeader));

                        // 2) 見出し部
                        //集計項目ラベル
                        List<string> lstPageHeader = new List<string>();
                        foreach (string lable in SK_LABEL)
                        {
                            lstPageHeader.Add(row[lable].ToString());
                        }

                        //出力項目ラベル
                        foreach (string lable in newOUTPUT_LABEL)
                        {
                            if (!string.IsNullOrEmpty(lable))
                            {
                                lstPageHeader.Add(row[lable].ToString());

                            }
                        }

                        // 見出し部書く
                        sw.WriteLine(string.Join(",", lstPageHeader));


                        //3) 明細部
                        DataTable dtDetail = this.form.ReportInfo.DataTableList["Detail"];
                        foreach (DataRow rowDetail in dtDetail.Rows)
                        {
                            List<string> lstdtDetail = new List<string>();
                            //集計項目
                            foreach (string name in rowType1Names)
                            {
                                lstdtDetail.Add(rowDetail[name].ToString());
                            }

                            //出力項目
                            foreach (string name in rowType2Names)
                            {
                                if (!string.IsNullOrEmpty(name))
                                {
                                    // 「,」がある場合
                                    if (rowDetail[name].ToString().IndexOf(',') >= 0)
                                    {
                                        lstdtDetail.Add("\"" + rowDetail[name].ToString() + "\"");
                                    }
                                    else
                                    {
                                        lstdtDetail.Add(rowDetail[name].ToString());
                                    }
                                }
                            }

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
