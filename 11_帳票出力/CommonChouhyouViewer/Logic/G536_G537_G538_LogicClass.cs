// $Id: G536_G537_G538_LogicClass.cs 17948 2014-03-25 06:44:59Z sc.h.hashimoto $

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
//using Shougun.Core.Common.BusinessCommon;
//using Seasar.Quill.Attrs;
using CommonChouhyouPopup.App;
using System.Drawing;
using System.IO;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class G536_G537_G538_LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// Form
        /// </summary>
        private UIFormG536_G537_G538 form;

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

        ///// <summary>
        ///// ControlUtility
        ///// </summary>
        //internal ControlUtility controlUtil;

        /// <summary>
        /// CSV出力の列の出力フラグ
        /// </summary>
        private List<bool> csvOutputFlag;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        internal G536_G537_G538_LogicClass(UIFormG536_G537_G538 targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                // メインフォーム
                this.form = targetForm;

                // 明細
                this.meisai = targetForm.meisai;

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

                ////検索ボタン(F8)イベント生成
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

        #region 初期表示処理
        /// <summary>
        /// 初期表示処理
        /// </summary>
        internal bool DisplayInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 表示項目制御
                switch (this.form.WindowId)
                {
                    // 受付明細表の場合
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:
                        this.form.DENPYOU_KUBUN_LABEL.Visible = false;
                        this.form.DENPYOU_KUBUN.Visible = false;
                        break;
                    // 運賃明細表の場合
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:
                        break;
                    // 計量明細表の場合
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:
                        this.form.DENPYOU_SHURUI_LABEL.Visible = false;
                        this.form.DENPYOU_SHURUI.Visible = false;
                        this.form.DENPYOU_KUBUN_LABEL.Visible = false;
                        this.form.DENPYOU_KUBUN.Visible = false;
                        break;
                }

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

        #region 検索条件を画面にセット
        ///// <summary>
        ///// 検索条件を画面にセット
        ///// </summary>
        //private void SetFormCondition()
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart();

        //        // 対象テーブル存在しない場合
        //        if (!this.form.RptInfoCls.DataTableList.ContainsKey("FillCondition"))
        //        {
        //            // 処理終了
        //            return;
        //        }

        //        // FillConditionテーブル
        //        var dt = this.form.RptInfoCls.DataTableList["FillCondition"];
        //        if (dt == null || dt.Rows.Count < 1)
        //        {
        //            return;
        //        }
        //        // Row取得（1行しかないはず）
        //        DataRow row = dt.Rows[0];

        //        // コントロール取得
        //        Control[] ctrls = this.form.GetAllControl();

        //        // UIFormのコントロールを制御
        //        foreach (var ctrl in ctrls)
        //        {
        //            // 下記コントロール以外の場合
        //            if (ctrl.GetType() != typeof(CustomTextBox)
        //                && ctrl.GetType() != typeof(CustomAlphaNumTextBox)
        //                && ctrl.GetType() != typeof(CustomNumericTextBox2)
        //                )
        //            {
        //                // 次へ
        //                continue;
        //            }

        //            // textプロパティを取得
        //            var property = ctrl.GetType().GetProperty("Text");
        //            if (property != null)
        //            {
        //                // DataTableに該当項目ある場合
        //                if (dt.Columns.Contains(ctrl.Name))
        //                {
        //                    // Text値を設定
        //                    property.SetValue(ctrl, row[ctrl.Name].ToString(), null);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
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
                        // 3行2列目の場合
                        if (cell.Name == "head3_02")
                        {
                            // 明細番号設定（固定）
                            cell.Value = "明細番号";
                        }
                        else
                        {
                            // ヘッダクリア
                            cell.Value = string.Empty;
                        }
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

                // ブレーク制御用番号
                int breakNo = -1;
                // MultiRowの行
                GrapeCity.Win.MultiRow.Row muRow;
                // 明細データループ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Row取得
                    DataRow row = dt.Rows[i];

                    // 最初行または伝票番号または集計項目が変わる場合
                    if (i == 0 || breakNo > -1)
                    {
                        // 一行目（集計項目行出力）
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];
                        muRow.Cells["KAHEN1_VALUE"].Value = row["FILL_COND_1_CD"].ToString();
                        muRow.Cells["KAHEN2_VALUE"].Value = row["FILL_COND_1_NAME"].ToString();
                        muRow.Cells["KAHEN3_VALUE"].Value = row["FILL_COND_2_CD"].ToString();
                        muRow.Cells["KAHEN4_VALUE"].Value = row["FILL_COND_2_NAME"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["FILL_COND_3_CD"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["FILL_COND_3_NAME"].ToString();
                    }

                    // 最初行または伝票番号または集計項目が変わる場合
                    if (i == 0 || breakNo > -1)
                    {
                        // 二行目（伝票項目出力）
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // CellのAlignスタイル設定
                        // CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN1_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_1_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN1_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_1_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN2_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_2_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN2_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_2_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN3_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_3_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_3_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN4_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_4_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN4_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_4_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN5_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_5_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN5_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_5_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN6_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_6_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_6_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN7_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_7_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_7_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN8_CD"], dtHeader.Rows[0]["OUTPUT_DENPYOU_8_ALIGN"].ToString());
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], dtHeader.Rows[0]["OUTPUT_DENPYOU_8_ALIGN"].ToString());

                        // 値設定
                        muRow.Cells["detail1_01_value"].Value = row["DENPYOU_NUMBER"].ToString();
                        muRow.Cells["detail1_02_value"].Value = row["DENPYOU_DATE"].ToString();
                        muRow.Cells["KAHEN1_CD"].Value = row["OUTPUT_DENPYOU_1_CD"].ToString();
                        muRow.Cells["KAHEN1_VALUE"].Value = row["OUTPUT_DENPYOU_1_VALUE"].ToString();
                        muRow.Cells["KAHEN2_CD"].Value = row["OUTPUT_DENPYOU_2_CD"].ToString();
                        muRow.Cells["KAHEN2_VALUE"].Value = row["OUTPUT_DENPYOU_2_VALUE"].ToString();
                        muRow.Cells["KAHEN3_CD"].Value = row["OUTPUT_DENPYOU_3_CD"].ToString();
                        muRow.Cells["KAHEN3_VALUE"].Value = row["OUTPUT_DENPYOU_3_VALUE"].ToString();
                        muRow.Cells["KAHEN4_CD"].Value = row["OUTPUT_DENPYOU_4_CD"].ToString();
                        muRow.Cells["KAHEN4_VALUE"].Value = row["OUTPUT_DENPYOU_4_VALUE"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["OUTPUT_DENPYOU_5_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["OUTPUT_DENPYOU_5_VALUE"].ToString();
                        muRow.Cells["KAHEN6_CD"].Value = row["OUTPUT_DENPYOU_6_CD"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["OUTPUT_DENPYOU_6_VALUE"].ToString();
                        muRow.Cells["KAHEN7_CD"].Value = row["OUTPUT_DENPYOU_7_CD"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["OUTPUT_DENPYOU_7_VALUE"].ToString();
                        muRow.Cells["KAHEN8_CD"].Value = row["OUTPUT_DENPYOU_8_CD"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["OUTPUT_DENPYOU_8_VALUE"].ToString();
                    }

                    // 三行目（明細項目出力）
                    this.meisai.Rows.Add();
                    muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                    // CellのAlignスタイル設定
                    this.SetCellAlign(muRow.Cells["detail1_02_value"], "2");
                    this.SetCellAlign(muRow.Cells["KAHEN1_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_1_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN1_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_1_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN2_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_2_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN2_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_2_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN3_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_3_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN3_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_3_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN4_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_4_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN4_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_4_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN5_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_5_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN5_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_5_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN6_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_6_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_6_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN7_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_7_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_7_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN8_CD"], dtHeader.Rows[0]["OUTPUT_MEISAI_8_ALIGN"].ToString());
                    this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], dtHeader.Rows[0]["OUTPUT_MEISAI_8_ALIGN"].ToString());

                    // 値設定
                    //muRow.Cells["detail1_01_value"].Value = row["DENPYOU_NUMBER"].ToString();
                    muRow.Cells["detail1_02_value"].Value = row["ROW_NO"].ToString();
                    muRow.Cells["KAHEN1_CD"].Value = row["OUTPUT_MEISAI_1_CD"].ToString();
                    muRow.Cells["KAHEN1_VALUE"].Value = row["OUTPUT_MEISAI_1_VALUE"].ToString();
                    muRow.Cells["KAHEN2_CD"].Value = row["OUTPUT_MEISAI_2_CD"].ToString();
                    muRow.Cells["KAHEN2_VALUE"].Value = row["OUTPUT_MEISAI_2_VALUE"].ToString();
                    muRow.Cells["KAHEN3_CD"].Value = row["OUTPUT_MEISAI_3_CD"].ToString();
                    muRow.Cells["KAHEN3_VALUE"].Value = row["OUTPUT_MEISAI_3_VALUE"].ToString();
                    muRow.Cells["KAHEN4_CD"].Value = row["OUTPUT_MEISAI_4_CD"].ToString();
                    muRow.Cells["KAHEN4_VALUE"].Value = row["OUTPUT_MEISAI_4_VALUE"].ToString();
                    muRow.Cells["KAHEN5_CD"].Value = row["OUTPUT_MEISAI_5_CD"].ToString();
                    muRow.Cells["KAHEN5_VALUE"].Value = row["OUTPUT_MEISAI_5_VALUE"].ToString();
                    muRow.Cells["KAHEN6_CD"].Value = row["OUTPUT_MEISAI_6_CD"].ToString();
                    muRow.Cells["KAHEN6_VALUE"].Value = row["OUTPUT_MEISAI_6_VALUE"].ToString();
                    muRow.Cells["KAHEN7_CD"].Value = row["OUTPUT_MEISAI_7_CD"].ToString();
                    muRow.Cells["KAHEN7_VALUE"].Value = row["OUTPUT_MEISAI_7_VALUE"].ToString();
                    muRow.Cells["KAHEN8_CD"].Value = row["OUTPUT_MEISAI_8_CD"].ToString();
                    muRow.Cells["KAHEN8_VALUE"].Value = row["OUTPUT_MEISAI_8_VALUE"].ToString();

                    // 最後行の場合
                    if (i == dt.Rows.Count - 1)
                    {
                        // ブレーク番号
                        breakNo = 4;
                    }
                    // 最後行目ではない場合
                    else
                    {
                        // NextRow取得
                        DataRow nextRow = dt.Rows[i + 1];
                        // ブレーク制御用番号
                        breakNo = this.GetBreakNO(row, nextRow);
                    }

                    // 伝票合計出力(計量明細表G538を除外)
                    if (breakNo > -1 && this.form.WindowId != WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                    {
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN4_VALUE"].Value = dtHeader.Rows[0]["DENPYOU_TOTAL_LABEL"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["DENPYOU_KINGAKU_TOTAL"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["DENPYOU_TAX_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["DENPYOU_TOTAL"].ToString();
                    }
                    // 集計項目３合計出力
                    if (breakNo > 0)
                    {
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN4_VALUE"].Value = dtHeader.Rows[0]["FILL_COND_ID_3_TOTAL_LABEL"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["FILL_COND_ID_3_TOTAL_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["FILL_COND_ID_3_TOTAL_NAME"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["FILL_COND_ID_3_KINGAKU_TOTAL"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["FILL_COND_ID_3_TAX_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["FILL_COND_ID_3_TOTAL"].ToString();
                    }
                    // 集計項目２合計出力
                    if (breakNo > 1)
                    {
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN4_VALUE"].Value = dtHeader.Rows[0]["FILL_COND_ID_2_TOTAL_LABEL"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["FILL_COND_ID_2_TOTAL_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["FILL_COND_ID_2_TOTAL_NAME"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["FILL_COND_ID_2_KINGAKU_TOTAL"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["FILL_COND_ID_2_TAX_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["FILL_COND_ID_2_TOTAL"].ToString();
                    }
                    // 集計項目１合計出力
                    if (breakNo > 2)
                    {
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN4_VALUE"].Value = dtHeader.Rows[0]["FILL_COND_ID_1_TOTAL_LABEL"].ToString();
                        muRow.Cells["KAHEN5_CD"].Value = row["FILL_COND_ID_1_TOTAL_CD"].ToString();
                        muRow.Cells["KAHEN5_VALUE"].Value = row["FILL_COND_ID_1_TOTAL_NAME"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["FILL_COND_ID_1_KINGAKU_TOTAL"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["FILL_COND_ID_1_TAX_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["FILL_COND_ID_1_TOTAL"].ToString();
                    }
                    // 総合計
                    if (breakNo > 3)
                    {
                        this.meisai.Rows.Add();
                        muRow = this.meisai.Rows[this.meisai.Rows.Count - 1];

                        // ヘッダスタイル設定
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_CD"]);
                        this.SetTitleCellStyle(muRow.Cells["KAHEN4_VALUE"]);
                        // 数値CellのAlignスタイル設定
                        this.SetCellAlign(muRow.Cells["KAHEN6_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN7_VALUE"], "2");
                        this.SetCellAlign(muRow.Cells["KAHEN8_VALUE"], "2");

                        // 値設定
                        muRow.Cells["KAHEN4_VALUE"].Value = dtHeader.Rows[0]["ALL_TOTAL_LABEL"].ToString();
                        muRow.Cells["KAHEN6_VALUE"].Value = row["ALL_KINGAKU_TOTAL"].ToString();
                        muRow.Cells["KAHEN7_VALUE"].Value = row["ALL_TAX_TOTAL"].ToString();
                        muRow.Cells["KAHEN8_VALUE"].Value = row["ALL_TOTAL_1"].ToString();
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

        #region ブレーク制御用番号を取得
        /// <summary>
        /// ブレーク制御用番号を取得
        /// </summary>
        /// <param name="row">現在の行</param>
        /// <param name="nextRow">次の行</param>
        /// <returns>ブレーク制御用番号</returns>
        private int GetBreakNO(DataRow row, DataRow nextRow)
        {
            // ブレーク制御用番号
            int breakNo = -1;

            try
            {
                //LogUtility.DebugMethodStart();

                // ブレークチェック
                // 集計項目1ブレークの場合
                if (row["FILL_COND_1_CD"].ToString() != nextRow["FILL_COND_1_CD"].ToString())
                {
                    breakNo = 3;
                }
                // 集計項目2ブレークの場合
                else if (row["FILL_COND_2_CD"].ToString() != nextRow["FILL_COND_2_CD"].ToString())
                {
                    breakNo = 2;
                }
                // 集計項目3ブレークの場合
                else if (row["FILL_COND_3_CD"].ToString() != nextRow["FILL_COND_3_CD"].ToString())
                {
                    breakNo = 1;
                }
                // 伝票番号ブレークの場合
                else if (row["DENPYOU_NUMBER"].ToString() != nextRow["DENPYOU_NUMBER"].ToString())
                {
                    breakNo = 0;
                }

                return breakNo;
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
                LogUtility.DebugMethodStart(cell);

                cell.Style.BackColor = Color.FromArgb(0, 105, 51);
                cell.Style.ForeColor = Color.FromArgb(255, 255, 255);
                cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                ((GcCustomTextBoxCell)cell).AutoChangeBackColorEnabled = false;
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
                    //ファイルを開く,追記しない(上書き）、エンコードはデフォルト（日本語WindowsではSJIS)
                    using (StreamWriter sw = new StreamWriter(filePath + "\\" + fileName, false, System.Text.Encoding.GetEncoding(-0)))
                    {
                        string strCsv = string.Empty;

                        // 全出力不要（2014/01/24）
                        //// 受付明細(G536)以外の場合
                        //if (this.form.WindowId != WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                        //{
                        //    // タイトル文字列取得
                        //    strCsv = this.GetCsvTitleString();
                        //    // タイトル書く
                        //    sw.WriteLine(strCsv);
                        //}

                        // 出力するカラムの判定
                        this.GetCsvOutputColumn();
                        
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
                csvItems.Add("[" + row["FILL_COND_1_NAME"].ToString().TrimEnd(trimChar) + "]");
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
                csvItems.Add("[" + row["FILL_COND_2_NAME"].ToString().TrimEnd(trimChar) + "]");
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
                csvItems.Add("[" + row["FILL_COND_3_NAME"].ToString().TrimEnd(trimChar) + "]");
                // 集計項目3CD範囲
                if (string.IsNullOrEmpty(row["FILL_COND_3_CD_BEGIN"].ToString()) && string.IsNullOrEmpty(row["FILL_COND_3_CD_END"].ToString()))
                {
                    csvItems.Add(string.Empty);
                }
                else
                {
                    csvItems.Add(row["FILL_COND_3_CD_BEGIN"].ToString() + "～" + row["FILL_COND_3_CD_END"].ToString());
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

        #region 出力するカラムの判定処理

        private void GetCsvOutputColumn()
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

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // 出力項目名
                List<string> outputItems = new List<string> { "FILL_COND_1_CD"	            //集計項目CD1
                                                            , "FILL_COND_1_NAME"	        //集計項目名1
                                                            , "FILL_COND_2_CD"	            //集計項目CD2
                                                            , "FILL_COND_2_NAME"	        //集計項目名2
                                                            , "FILL_COND_3_CD"	            //集計項目CD3
                                                            , "FILL_COND_3_NAME"	        //集計項目名3
                                                            , "DENPYOU_NUMBER"	            //伝票番号
                                                            , "DENPYOU_DATE"	            //伝票日付
                                                            , "OUTPUT_DENPYOU_1_CD"	        //出力（伝票）項目CD1
                                                            , "OUTPUT_DENPYOU_1_VALUE"	    //出力（伝票）項目値1
                                                            , "OUTPUT_DENPYOU_2_CD"	        //出力（伝票）項目CD2
                                                            , "OUTPUT_DENPYOU_2_VALUE"	    //出力（伝票）項目値2
                                                            , "OUTPUT_DENPYOU_3_CD"	        //出力（伝票）項目CD3
                                                            , "OUTPUT_DENPYOU_3_VALUE"	    //出力（伝票）項目値3
                                                            , "OUTPUT_DENPYOU_4_CD"	        //出力（伝票）項目CD4
                                                            , "OUTPUT_DENPYOU_4_VALUE"	    //出力（伝票）項目値4
                                                            , "OUTPUT_DENPYOU_5_CD"	        //出力（伝票）項目CD5
                                                            , "OUTPUT_DENPYOU_5_VALUE"	    //出力（伝票）項目値5
                                                            , "OUTPUT_DENPYOU_6_CD"	        //出力（伝票）項目CD6
                                                            , "OUTPUT_DENPYOU_6_VALUE"	    //出力（伝票）項目値6
                                                            , "OUTPUT_DENPYOU_7_CD"	        //出力（伝票）項目CD7
                                                            , "OUTPUT_DENPYOU_7_VALUE"	    //出力（伝票）項目値7
                                                            , "OUTPUT_DENPYOU_8_CD"	        //出力（伝票）項目CD8
                                                            , "OUTPUT_DENPYOU_8_VALUE"	    //出力（伝票）項目値8
                                                            , "ROW_NO"	                    //明細番号
                                                            , "OUTPUT_MEISAI_1_CD"	        //出力（明細）項目CD1
                                                            , "OUTPUT_MEISAI_1_VALUE"	    //出力（明細）項目値1
                                                            , "OUTPUT_MEISAI_2_CD"	        //出力（明細）項目CD2
                                                            , "OUTPUT_MEISAI_2_VALUE"	    //出力（明細）項目値2
                                                            , "OUTPUT_MEISAI_3_CD"	        //出力（明細）項目CD3
                                                            , "OUTPUT_MEISAI_3_VALUE"	    //出力（明細）項目値3
                                                            , "OUTPUT_MEISAI_4_CD"	        //出力（明細）項目CD4
                                                            , "OUTPUT_MEISAI_4_VALUE"	    //出力（明細）項目値4
                                                            , "OUTPUT_MEISAI_5_CD"	        //出力（明細）項目CD5
                                                            , "OUTPUT_MEISAI_5_VALUE"	    //出力（明細）項目値5
                                                            , "OUTPUT_MEISAI_6_CD"	        //出力（明細）項目CD6
                                                            , "OUTPUT_MEISAI_6_VALUE"	    //出力（明細）項目値6
                                                            , "OUTPUT_MEISAI_7_CD"	        //出力（明細）項目CD7
                                                            , "OUTPUT_MEISAI_7_VALUE"	    //出力（明細）項目値7
                                                            , "OUTPUT_MEISAI_8_CD"	        //出力（明細）項目CD8
                                                            , "OUTPUT_MEISAI_8_VALUE"	    //出力（明細）項目値8
                                                            //, "DENPYOU_KINGAKU_TOTAL"	    //伝票毎金額計
                                                            //, "DENPYOU_TAX_TOTAL"	        //伝票毎消費税
                                                            //, "DENPYOU_TOTAL"	            //伝票毎総合計
                                                            };

                // 計量明細(G538)以外の場合
                if (this.form.WindowId != WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                {
                    outputItems.Add("DENPYOU_KINGAKU_TOTAL");        // 伝票毎金額計 
                    outputItems.Add("DENPYOU_TAX_TOTAL");            // 伝票毎消費税 
                    outputItems.Add("DENPYOU_TOTAL");                // 伝票毎総合計 
                }

                csvOutputFlag = new List<bool>();
                foreach (var item in outputItems)
                {
                    csvOutputFlag.Add(false);
                }

                // ヘッダ値出力
                DataTable dtDetail = this.form.ReportInfo.DataTableList["Detail"];
                foreach (DataRow row in dtDetail.Rows)
                {
                    int idx = 0;
                    foreach (var item in outputItems)
                    {
                        if (!string.IsNullOrEmpty(row[item].ToString()))
                        {
                            csvOutputFlag[idx] = true;
                        }
                        idx++;
                    }
                }
                return;
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

                // 出力項目名１
                List<string> outputItems1 = new List<string> { "FILL_COND_1_CD_LABEL"           // 集計項目CD1
                                                            , "FILL_COND_1_NAME_LABEL"          // 集計項目名1
                                                            , "FILL_COND_2_CD_LABEL"            // 集計項目CD2
                                                            , "FILL_COND_2_NAME_LABEL"          // 集計項目名2
                                                            , "FILL_COND_3_CD_LABEL"            // 集計項目CD3
                                                            , "FILL_COND_3_NAME_LABEL"          // 集計項目名3
                                                            , "DENPYOU_NUMBER_LABEL"            // 伝票番号
                                                            , "DENPYOU_DATE_LABEL"              // 伝票日付
                                                            , "OUTPUT_DENPYOU_1_LABEL"          // 出力（伝票）項目1
                                                            , "OUTPUT_DENPYOU_2_LABEL"          // 出力（伝票）項目2
                                                            , "OUTPUT_DENPYOU_3_LABEL"          // 出力（伝票）項目3
                                                            , "OUTPUT_DENPYOU_4_LABEL"          // 出力（伝票）項目4
                                                            , "OUTPUT_DENPYOU_5_LABEL"          // 出力（伝票）項目5
                                                            , "OUTPUT_DENPYOU_6_LABEL"          // 出力（伝票）項目6
                                                            , "OUTPUT_DENPYOU_7_LABEL"          // 出力（伝票）項目7
                                                            , "OUTPUT_DENPYOU_8_LABEL"          // 出力（伝票）項目8
                                                            };

                // 出力項目名２
                List<string> outputItems2 = new List<string> { "OUTPUT_MEISAI_1_LABEL"          // 出力（明細）項目1
                                                            , "OUTPUT_MEISAI_2_LABEL"           // 出力（明細）項目2
                                                            , "OUTPUT_MEISAI_3_LABEL"           // 出力（明細）項目3
                                                            , "OUTPUT_MEISAI_4_LABEL"           // 出力（明細）項目4
                                                            , "OUTPUT_MEISAI_5_LABEL"           // 出力（明細）項目5
                                                            , "OUTPUT_MEISAI_6_LABEL"           // 出力（明細）項目6
                                                            , "OUTPUT_MEISAI_7_LABEL"           // 出力（明細）項目7
                                                            , "OUTPUT_MEISAI_8_LABEL"           // 出力（明細）項目8
                                                            //, "KINGAKU_TOTAL_LABEL"             // 金額計 
                                                            //, "TAX_TOTAL_LABEL"                 // 消費税
                                                            //, "TOTAL_LABEL"                     // 総合計
                                                            };
                // 計量明細(G538)以外の場合
                if (this.form.WindowId != WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                {
                    outputItems2.Add("KINGAKU_TOTAL_LABEL");        // 金額計 
                    outputItems2.Add("TAX_TOTAL_LABEL");            // 消費税 
                    outputItems2.Add("TOTAL_LABEL");                // 総合計 
                }

                // ヘッダ値出力
                DataTable dtHeader = this.form.ReportInfo.DataTableList["Header"];
                DataRow row = dtHeader.Rows[0];

                List<string> csvItems = new List<string>();
                int idx = 0;

                foreach (var item in outputItems1)
                {
                    if (item.Equals("OUTPUT_DENPYOU_1_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_2_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_3_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_4_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_5_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_6_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_7_LABEL") ||
                        item.Equals("OUTPUT_DENPYOU_8_LABEL"))
                    {
                        if (csvOutputFlag[idx++])
                        {
                            csvItems.Add(string.Empty);
                        }
                    }

                    if (csvOutputFlag[idx++])
                    {
                        csvItems.Add(row[item].ToString());
                    }
                }

                // 明細番号
                if (csvOutputFlag[idx++])
                {
                    csvItems.Add("明細番号");
                }

                foreach (var item in outputItems2)
                {
                    if (item.Equals("OUTPUT_MEISAI_1_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_2_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_3_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_4_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_5_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_6_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_7_LABEL") ||
                        item.Equals("OUTPUT_MEISAI_8_LABEL"))
                    {
                        if (csvOutputFlag[idx++])
                        {
                            csvItems.Add(string.Empty);
                        }
                    }

                    if (csvOutputFlag[idx++])
                    {
                        csvItems.Add(row[item].ToString());
                    }
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
        /// <param name="sw">StreamWriter</param>
        /// <returns>明細データ文字列（カンマで区切済み）</returns>
        private void OutPutCsvMeisaiData(StreamWriter sw)
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

                // Headerテーブル
                var dt = this.form.ReportInfo.DataTableList["Detail"];
                if (dt == null || dt.Rows.Count < 1)
                {
                    return;
                }

                // 出力項目名
                List<string> outputItems = new List<string> { "FILL_COND_1_CD"	            //集計項目CD1
                                                            , "FILL_COND_1_NAME"	        //集計項目名1
                                                            , "FILL_COND_2_CD"	            //集計項目CD2
                                                            , "FILL_COND_2_NAME"	        //集計項目名2
                                                            , "FILL_COND_3_CD"	            //集計項目CD3
                                                            , "FILL_COND_3_NAME"	        //集計項目名3
                                                            , "DENPYOU_NUMBER"	            //伝票番号
                                                            , "DENPYOU_DATE"	            //伝票日付
                                                            , "OUTPUT_DENPYOU_1_CD"	        //出力（伝票）項目CD1
                                                            , "OUTPUT_DENPYOU_1_VALUE"	    //出力（伝票）項目値1
                                                            , "OUTPUT_DENPYOU_2_CD"	        //出力（伝票）項目CD2
                                                            , "OUTPUT_DENPYOU_2_VALUE"	    //出力（伝票）項目値2
                                                            , "OUTPUT_DENPYOU_3_CD"	        //出力（伝票）項目CD3
                                                            , "OUTPUT_DENPYOU_3_VALUE"	    //出力（伝票）項目値3
                                                            , "OUTPUT_DENPYOU_4_CD"	        //出力（伝票）項目CD4
                                                            , "OUTPUT_DENPYOU_4_VALUE"	    //出力（伝票）項目値4
                                                            , "OUTPUT_DENPYOU_5_CD"	        //出力（伝票）項目CD5
                                                            , "OUTPUT_DENPYOU_5_VALUE"	    //出力（伝票）項目値5
                                                            , "OUTPUT_DENPYOU_6_CD"	        //出力（伝票）項目CD6
                                                            , "OUTPUT_DENPYOU_6_VALUE"	    //出力（伝票）項目値6
                                                            , "OUTPUT_DENPYOU_7_CD"	        //出力（伝票）項目CD7
                                                            , "OUTPUT_DENPYOU_7_VALUE"	    //出力（伝票）項目値7
                                                            , "OUTPUT_DENPYOU_8_CD"	        //出力（伝票）項目CD8
                                                            , "OUTPUT_DENPYOU_8_VALUE"	    //出力（伝票）項目値8
                                                            , "ROW_NO"	                    //明細番号
                                                            , "OUTPUT_MEISAI_1_CD"	        //出力（明細）項目CD1
                                                            , "OUTPUT_MEISAI_1_VALUE"	    //出力（明細）項目値1
                                                            , "OUTPUT_MEISAI_2_CD"	        //出力（明細）項目CD2
                                                            , "OUTPUT_MEISAI_2_VALUE"	    //出力（明細）項目値2
                                                            , "OUTPUT_MEISAI_3_CD"	        //出力（明細）項目CD3
                                                            , "OUTPUT_MEISAI_3_VALUE"	    //出力（明細）項目値3
                                                            , "OUTPUT_MEISAI_4_CD"	        //出力（明細）項目CD4
                                                            , "OUTPUT_MEISAI_4_VALUE"	    //出力（明細）項目値4
                                                            , "OUTPUT_MEISAI_5_CD"	        //出力（明細）項目CD5
                                                            , "OUTPUT_MEISAI_5_VALUE"	    //出力（明細）項目値5
                                                            , "OUTPUT_MEISAI_6_CD"	        //出力（明細）項目CD6
                                                            , "OUTPUT_MEISAI_6_VALUE"	    //出力（明細）項目値6
                                                            , "OUTPUT_MEISAI_7_CD"	        //出力（明細）項目CD7
                                                            , "OUTPUT_MEISAI_7_VALUE"	    //出力（明細）項目値7
                                                            , "OUTPUT_MEISAI_8_CD"	        //出力（明細）項目CD8
                                                            , "OUTPUT_MEISAI_8_VALUE"	    //出力（明細）項目値8
                                                            //, "DENPYOU_KINGAKU_TOTAL"	    //伝票毎金額計
                                                            //, "DENPYOU_TAX_TOTAL"	        //伝票毎消費税
                                                            //, "DENPYOU_TOTAL"	            //伝票毎総合計
                                                            };

                // 計量明細(G538)以外の場合
                if (this.form.WindowId != WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                {
                    outputItems.Add("DENPYOU_KINGAKU_TOTAL");        // 伝票毎金額計 
                    outputItems.Add("DENPYOU_TAX_TOTAL");            // 伝票毎消費税 
                    outputItems.Add("DENPYOU_TOTAL");                // 伝票毎総合計 
                }

                // ヘッダ値出力
                DataTable dtDetail = this.form.ReportInfo.DataTableList["Detail"];
                foreach (DataRow row in dtDetail.Rows)
                {
                    List<string> csvItems = new List<string>();
                    int idx = 0;

                    foreach (var item in outputItems)
                    {
                        if (csvOutputFlag[idx++])
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
