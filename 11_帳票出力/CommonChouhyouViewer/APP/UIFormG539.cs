// $Id: UIFormG539.cs 11252 2013-12-16 05:59:24Z sys_dev_22 $

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    /// <summary>
    /// 計量集計表/一覧画面
    /// </summary>
    public partial class UIFormG539 : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G539_LogicClass logic;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        UIHeaderForm header_new;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">headerForm</param>
        public UIFormG539(UIHeaderForm headerForm, WINDOW_ID windowID)
            : base(WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU, WINDOW_TYPE.NONE)
        {
            try
            {
                LogUtility.DebugMethodStart(headerForm, windowID);

                this.InitializeComponent();

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G539_LogicClass(this);
                this.logic.SetHeader(header_new);
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

        #region - Properties -

        /// <summary>帳票情報を保持するプロパティ</summary>
        public ReportInfoBase ReportInfo { get; set; }

        #endregion - Properties -

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                // タイトル設定
                this.header_new.lb_title.Text += "／一覧";
                // タイトル設定
                this.Parent.Text += "／一覧";

                // テストデータ作成
                //this.CreateTestData();

                this.logic.WindowInit();
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

        #region Functionボタン 押下処理
        /// <summary>
        /// [F5]印刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Print();

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

        /// <summary>
        /// [F6]CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CSVOutput();

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

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();

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

        #endregion

        #region 帳票データを設定

        /// <summary>
        /// テストデータ作成
        /// </summary>
        private void CreateTestData()
        {

            Dictionary<string, DataTable> dataTableList = new Dictionary<string, DataTable>();

            #region DataTableListキー：Header

            // 条件設定テーブル
            DataTable Header = new DataTable();
            // カラム生成
            Header.Columns.Add("CORP_RYAKU_NAME");
            Header.Columns.Add("PRINT_DATE");
            Header.Columns.Add("TITLE");
            Header.Columns.Add("KYOTEN_CD");
            Header.Columns.Add("KYOTEN_NAME_RYAKU");
            Header.Columns.Add("ALERT_NUMBER");
            Header.Columns.Add("READ_DATA_NUMBER");
            Header.Columns.Add("DENPYOU_DATE_BEGIN");
            Header.Columns.Add("DENPYOU_DATE_END");
            Header.Columns.Add("DENPYOU_SHURUI");
            Header.Columns.Add("DENPYOU_KUBUN");
            Header.Columns.Add("FILL_COND_1_NAME");
            Header.Columns.Add("FILL_COND_1_CD_BEGIN");
            Header.Columns.Add("FILL_COND_1_VALUE_BEGIN");
            Header.Columns.Add("FILL_COND_1_CD_END");
            Header.Columns.Add("FILL_COND_1_VALUE_END");
            Header.Columns.Add("FILL_COND_2_NAME");
            Header.Columns.Add("FILL_COND_2_CD_BEGIN");
            Header.Columns.Add("FILL_COND_2_VALUE_BEGIN");
            Header.Columns.Add("FILL_COND_2_CD_END");
            Header.Columns.Add("FILL_COND_2_VALUE_END");
            Header.Columns.Add("FILL_COND_3_NAME");
            Header.Columns.Add("FILL_COND_3_CD_BEGIN");
            Header.Columns.Add("FILL_COND_3_VALUE_BEGIN");
            Header.Columns.Add("FILL_COND_3_CD_END");
            Header.Columns.Add("FILL_COND_3_VALUE_END");
            Header.Columns.Add("FILL_COND_4_NAME");
            Header.Columns.Add("FILL_COND_4_CD_BEGIN");
            Header.Columns.Add("FILL_COND_4_VALUE_BEGIN");
            Header.Columns.Add("FILL_COND_4_CD_END");
            Header.Columns.Add("FILL_COND_4_VALUE_END");
            Header.Columns.Add("UNIT_LABEL");
            Header.Columns.Add("ROW_NO_LABEL");
            Header.Columns.Add("FILL_COND_1_CD_LABEL");
            Header.Columns.Add("FILL_COND_1_NAME_LABEL");
            Header.Columns.Add("FILL_COND_2_CD_LABEL");
            Header.Columns.Add("FILL_COND_2_NAME_LABEL");
            Header.Columns.Add("FILL_COND_3_CD_LABEL");
            Header.Columns.Add("FILL_COND_3_NAME_LABEL");
            Header.Columns.Add("FILL_COND_4_CD_LABEL");
            Header.Columns.Add("FILL_COND_4_NAME_LABEL");
            Header.Columns.Add("DENPYOU_NUMBER_LABEL");
            Header.Columns.Add("DENPYOU_DATE_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_1_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_1_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_2_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_2_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_3_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_3_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_4_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_4_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_5_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_5_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_6_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_6_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_7_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_7_ALIGN");
            Header.Columns.Add("OUTPUT_DENPYOU_8_LABEL");
            Header.Columns.Add("OUTPUT_DENPYOU_8_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_1_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_1_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_2_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_2_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_3_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_3_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_4_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_4_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_5_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_5_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_6_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_6_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_7_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_7_ALIGN");
            Header.Columns.Add("OUTPUT_MEISAI_8_LABEL");
            Header.Columns.Add("OUTPUT_MEISAI_8_ALIGN");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_1_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_2_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_3_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_4_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_5_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_6_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_7_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_8_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_9_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_10_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_11_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_12_LABEL");
            Header.Columns.Add("OUTPUT_YEAR_MONTH_TOTAL_LABEL");
            Header.Columns.Add("OUTPUT_ITEM_1_LABEL");
            Header.Columns.Add("OUTPUT_ITEM_2_LABEL");
            Header.Columns.Add("OUTPUT_ITEM_3_LABEL");
            Header.Columns.Add("OUTPUT_ITEM_4_LABEL");
            Header.Columns.Add("KINGAKU_TOTAL_LABEL");
            Header.Columns.Add("TAX_TOTAL_LABEL");
            Header.Columns.Add("TOTAL_LABEL");
            Header.Columns.Add("DENPYOU_TOTAL_LABEL");
            Header.Columns.Add("FILL_COND_ID_1_TOTAL_LABEL");
            Header.Columns.Add("FILL_COND_ID_2_TOTAL_LABEL");
            Header.Columns.Add("FILL_COND_ID_3_TOTAL_LABEL");
            Header.Columns.Add("FILL_COND_ID_4_TOTAL_LABEL");
            Header.Columns.Add("ALL_TOTAL_LABEL");


            // データ設定
            DataRow row = Header.NewRow();
            row["CORP_RYAKU_NAME"] = "テスト会社";
            row["PRINT_DATE"] = DateTime.Now.ToString();
            row["TITLE"] = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU);

            row["KYOTEN_CD"] = "01";
            row["KYOTEN_NAME_RYAKU"] = "拠点01";
            row["ALERT_NUMBER"] = "1000";
            row["READ_DATA_NUMBER"] = "2000";
            row["DENPYOU_DATE_BEGIN"] = "2013/12/01(日)";
            row["DENPYOU_DATE_END"] = "2013/12/31(火)";
            row["DENPYOU_SHURUI"] = "1.収集";//****************
            row["DENPYOU_KUBUN"] = "1.売上";//****************
            row["FILL_COND_1_NAME"] = "取引先別";
            row["FILL_COND_1_CD_BEGIN"] = "000003";
            row["FILL_COND_1_VALUE_BEGIN"] = "取引先000003";
            row["FILL_COND_1_CD_END"] = "000004";
            row["FILL_COND_1_VALUE_END"] = "取引先000004";
            row["FILL_COND_2_NAME"] = "業者別";
            row["FILL_COND_2_CD_BEGIN"] = "000005";
            row["FILL_COND_2_VALUE_BEGIN"] = "業者000005";
            row["FILL_COND_2_CD_END"] = "000006";
            row["FILL_COND_2_VALUE_END"] = "業者000006";
            row["FILL_COND_3_NAME"] = "営業者別";
            row["FILL_COND_3_CD_BEGIN"] = "000007";
            row["FILL_COND_3_VALUE_BEGIN"] = "営業者000007";
            row["FILL_COND_3_CD_END"] = "000008";
            row["FILL_COND_3_VALUE_END"] = "営業者000008";
            row["FILL_COND_4_NAME"] = "営業者別";//****************
            row["FILL_COND_4_CD_BEGIN"] = "000007";//****************
            row["FILL_COND_4_VALUE_BEGIN"] = "営業者000007";//****************
            row["FILL_COND_4_CD_END"] = "000008";//****************
            row["FILL_COND_4_VALUE_END"] = "営業者000008";//****************
            row["UNIT_LABEL"] = "KG";//****************
            row["ROW_NO_LABEL"] = "行No";//****************

            row["FILL_COND_1_CD_LABEL"] = "集計項目CD1ラベル";
            row["FILL_COND_1_NAME_LABEL"] = "集計項目名1ラベル";
            row["FILL_COND_2_CD_LABEL"] = "集計項目CD2ラベル";
            row["FILL_COND_2_NAME_LABEL"] = "集計項目名2ラベル";
            row["FILL_COND_3_CD_LABEL"] = "集計項目CD3ラベル";
            row["FILL_COND_3_NAME_LABEL"] = "集計項目名3ラベル";

            row["OUTPUT_DENPYOU_1_LABEL"] = "出力（伝票）項目1ラベル";
            row["OUTPUT_DENPYOU_1_ALIGN"] = "3";
            row["OUTPUT_DENPYOU_2_LABEL"] = "出力（伝票）項目2ラベル";
            row["OUTPUT_DENPYOU_2_ALIGN"] = "3";
            //row["OUTPUT_DENPYOU_3_LABEL"] = "出力（伝票）項目3ラベル";
            //row["OUTPUT_DENPYOU_3_ALIGN"] = "2";
            //row["OUTPUT_DENPYOU_4_LABEL"] = "出力（伝票）項目4ラベル";
            //row["OUTPUT_DENPYOU_4_ALIGN"] = "2";
            //row["OUTPUT_DENPYOU_5_LABEL"] = "出力（伝票）項目5ラベル";
            //row["OUTPUT_DENPYOU_5_ALIGN"] = "2";
            //row["OUTPUT_DENPYOU_6_LABEL"] = "出力（伝票）項目6ラベル";
            //row["OUTPUT_DENPYOU_6_ALIGN"] = "2";
            //row["OUTPUT_DENPYOU_7_LABEL"] = "出力（伝票）項目7ラベル";
            //row["OUTPUT_DENPYOU_7_ALIGN"] = "2";
            row["OUTPUT_DENPYOU_8_LABEL"] = "出力（伝票）項目8ラベル";
            row["OUTPUT_DENPYOU_8_ALIGN"] = "3";
            //row["OUTPUT_MEISAI_1_LABEL"] = "出力（明細）項目1ラベル";
            //row["OUTPUT_MEISAI_1_ALIGN"] = "3";
            //row["OUTPUT_MEISAI_2_LABEL"] = "出力（明細）項目2ラベル";
            //row["OUTPUT_MEISAI_2_ALIGN"] = "3";
            //row["OUTPUT_MEISAI_3_LABEL"] = "出力（明細）項目3ラベル";
            //row["OUTPUT_MEISAI_3_ALIGN"] = "2";
            row["OUTPUT_MEISAI_4_LABEL"] = "出力（明細）項目4ラベル";
            row["OUTPUT_MEISAI_4_ALIGN"] = "2";
            row["OUTPUT_MEISAI_5_LABEL"] = "出力（明細）項目5ラベル";
            row["OUTPUT_MEISAI_5_ALIGN"] = "2";
            row["OUTPUT_MEISAI_6_LABEL"] = "出力（明細）項目6ラベル";
            row["OUTPUT_MEISAI_6_ALIGN"] = "2";
            row["OUTPUT_MEISAI_7_LABEL"] = "出力（明細）項目7ラベル";
            row["OUTPUT_MEISAI_7_ALIGN"] = "2";
            row["OUTPUT_MEISAI_8_LABEL"] = "出力（明細）項目8ラベル";


            row["FILL_COND_ID_1_TOTAL_LABEL"] = "集計項目合計ラベル1";
            row["FILL_COND_ID_2_TOTAL_LABEL"] = "集計項目合計ラベル2";
            row["FILL_COND_ID_3_TOTAL_LABEL"] = "集計項目合計ラベル3";
            row["ALL_TOTAL_LABEL"] = "集計項目総合計ラベル";

            Header.Rows.Add(row);
            #endregion

            #region DataTableListキー：Detail

            // 条件設定テーブル
            DataTable Detail = new DataTable();

            //detailカラム生成
            Detail.Columns.Add("GROUP_LABEL");
            Detail.Columns.Add("FILL_COND_1_CD");
            Detail.Columns.Add("FILL_COND_1_NAME");
            Detail.Columns.Add("FILL_COND_2_CD");
            Detail.Columns.Add("FILL_COND_2_NAME");
            Detail.Columns.Add("FILL_COND_3_CD");
            Detail.Columns.Add("FILL_COND_3_NAME");
            Detail.Columns.Add("FILL_COND_4_CD");
            Detail.Columns.Add("FILL_COND_4_NAME");
            Detail.Columns.Add("DENPYOU_NUMBER");
            Detail.Columns.Add("DENPYOU_DATE");
            Detail.Columns.Add("OUTPUT_DENPYOU_1_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_1_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_2_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_2_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_3_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_3_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_4_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_4_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_5_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_5_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_6_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_6_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_7_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_7_VALUE");
            Detail.Columns.Add("OUTPUT_DENPYOU_8_CD");
            Detail.Columns.Add("OUTPUT_DENPYOU_8_VALUE");
            Detail.Columns.Add("ROW_NO");
            Detail.Columns.Add("OUTPUT_MEISAI_1_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_1_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_2_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_2_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_3_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_3_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_4_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_4_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_5_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_5_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_6_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_6_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_7_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_7_VALUE");
            Detail.Columns.Add("OUTPUT_MEISAI_8_CD");
            Detail.Columns.Add("OUTPUT_MEISAI_8_VALUE");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_1");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_2");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_3");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_4");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_5");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_6");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_7");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_8");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_9");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_10");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_11");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_12");
            Detail.Columns.Add("OUTPUT_YEAR_MONTH_TOTAL");
            Detail.Columns.Add("OUTPUT_ITEM_1");
            Detail.Columns.Add("OUTPUT_ITEM_2");
            Detail.Columns.Add("OUTPUT_ITEM_3");
            Detail.Columns.Add("OUTPUT_ITEM_4");
            Detail.Columns.Add("DENPYOU_KINGAKU_TOTAL");
            Detail.Columns.Add("DENPYOU_TAX_TOTAL");
            Detail.Columns.Add("DENPYOU_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_1_TOTAL_CD");
            Detail.Columns.Add("FILL_COND_ID_1_TOTAL_NAME");
            Detail.Columns.Add("FILL_COND_ID_1_KINGAKU_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_1_TAX_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_1_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_2_TOTAL_CD");
            Detail.Columns.Add("FILL_COND_ID_2_TOTAL_NAME");
            Detail.Columns.Add("FILL_COND_ID_2_KINGAKU_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_2_TAX_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_2_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_3_TOTAL_CD");
            Detail.Columns.Add("FILL_COND_ID_3_TOTAL_NAME");
            Detail.Columns.Add("FILL_COND_ID_3_KINGAKU_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_3_TAX_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_3_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_4_TOTAL_CD");
            Detail.Columns.Add("FILL_COND_ID_4_TOTAL_NAME");
            Detail.Columns.Add("FILL_COND_ID_4_KINGAKU_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_4_TAX_TOTAL");
            Detail.Columns.Add("FILL_COND_ID_4_TOTAL");
            Detail.Columns.Add("ALL_KINGAKU_TOTAL");
            Detail.Columns.Add("ALL_TAX_TOTAL");
            Detail.Columns.Add("ALL_TOTAL");
            Detail.Columns.Add("FILL_COND_1_TOTAL_1");
            Detail.Columns.Add("FILL_COND_1_TOTAL_2");
            Detail.Columns.Add("FILL_COND_1_TOTAL_3");
            Detail.Columns.Add("FILL_COND_1_TOTAL_4");
            Detail.Columns.Add("FILL_COND_1_TOTAL_5");
            Detail.Columns.Add("FILL_COND_1_TOTAL_6");
            Detail.Columns.Add("FILL_COND_1_TOTAL_7");
            Detail.Columns.Add("FILL_COND_1_TOTAL_8");
            Detail.Columns.Add("FILL_COND_2_TOTAL_1");
            Detail.Columns.Add("FILL_COND_2_TOTAL_2");
            Detail.Columns.Add("FILL_COND_2_TOTAL_3");
            Detail.Columns.Add("FILL_COND_2_TOTAL_4");
            Detail.Columns.Add("FILL_COND_2_TOTAL_5");
            Detail.Columns.Add("FILL_COND_2_TOTAL_6");
            Detail.Columns.Add("FILL_COND_2_TOTAL_7");
            Detail.Columns.Add("FILL_COND_2_TOTAL_8");
            Detail.Columns.Add("FILL_COND_3_TOTAL_1");
            Detail.Columns.Add("FILL_COND_3_TOTAL_2");
            Detail.Columns.Add("FILL_COND_3_TOTAL_3");
            Detail.Columns.Add("FILL_COND_3_TOTAL_4");
            Detail.Columns.Add("FILL_COND_3_TOTAL_5");
            Detail.Columns.Add("FILL_COND_3_TOTAL_6");
            Detail.Columns.Add("FILL_COND_3_TOTAL_7");
            Detail.Columns.Add("FILL_COND_3_TOTAL_8");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_1");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_2");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_3");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_4");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_5");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_6");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_7");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_8");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_9");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_10");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_11");
            Detail.Columns.Add("ALL_YEAR_MONTH_TOTAL_12");
            Detail.Columns.Add("ALL_TOTAL_1");
            Detail.Columns.Add("ALL_TOTAL_2");
            Detail.Columns.Add("ALL_TOTAL_3");
            Detail.Columns.Add("ALL_TOTAL_4");
            Detail.Columns.Add("ALL_TOTAL_5");
            Detail.Columns.Add("ALL_TOTAL_6");
            Detail.Columns.Add("ALL_TOTAL_7");
            Detail.Columns.Add("ALL_TOTAL_8");

            for (int i = 0; i < 9; i++)
            { 
            // データ設定
            DataRow rowDetail = Detail.NewRow();
            switch (i)
            {
                case 0:
                case 1:
                case 2:
                    rowDetail["FILL_COND_1_CD"] = "000003";
                    rowDetail["FILL_COND_1_NAME"] = "取引先000003";
                    rowDetail["FILL_COND_2_CD"] = "000005";
                    rowDetail["FILL_COND_2_NAME"] = "業者000005";
                    rowDetail["FILL_COND_3_CD"] = "000007";
                    rowDetail["FILL_COND_3_NAME"] = "営業者000007";
                    break;
                case 3:
                case 4:
                    rowDetail["FILL_COND_1_CD"] = "000003";
                    rowDetail["FILL_COND_1_NAME"] = "取引先000003";
                    rowDetail["FILL_COND_2_CD"] = "000005";
                    rowDetail["FILL_COND_2_NAME"] = "業者000005";
                    rowDetail["FILL_COND_3_CD"] = "000008";
                    rowDetail["FILL_COND_3_NAME"] = "営業者000008";
                    break;
                case 5:
                case 6:
                    rowDetail["FILL_COND_1_CD"] = "000003";
                    rowDetail["FILL_COND_1_NAME"] = "取引先000003";
                    rowDetail["FILL_COND_2_CD"] = "000006";
                    rowDetail["FILL_COND_2_NAME"] = "業者000006";
                    rowDetail["FILL_COND_3_CD"] = "000008";
                    rowDetail["FILL_COND_3_NAME"] = "営業者000008";
                    break;
                case 7:
                case 8:
                    rowDetail["FILL_COND_1_CD"] = "000004";
                    rowDetail["FILL_COND_1_NAME"] = "取引先000004";
                    rowDetail["FILL_COND_2_CD"] = "000006";
                    rowDetail["FILL_COND_2_NAME"] = "業者000006";
                    rowDetail["FILL_COND_3_CD"] = "000008";
                    rowDetail["FILL_COND_3_NAME"] = "営業者000008";
                    break;
            }
            rowDetail["OUTPUT_DENPYOU_1_VALUE"] = "出力項目１_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_2_VALUE"] = "出力項目２_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_3_VALUE"] = "01_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_4_VALUE"] = "01_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_5_VALUE"] = "01_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_6_VALUE"] = "01_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_7_VALUE"] = "01_DENPYOU" + i.ToString();
            rowDetail["OUTPUT_DENPYOU_8_VALUE"] = "出力項目８_DENPYOU" + i.ToString();

            rowDetail["OUTPUT_MEISAI_1_VALUE"] = "出力項目１_MEISAI" + i.ToString();
            //rowDetail["OUTPUT_MEISAI_1_VALUE"] = "999,999,999";
            rowDetail["OUTPUT_MEISAI_2_VALUE"] = "出力項目２_MEISAI" + i.ToString();
            rowDetail["OUTPUT_MEISAI_3_VALUE"] = "01_MEISAI" + i.ToString();
            //rowDetail["OUTPUT_MEISAI_4_VALUE"] = "01_MEISAI" + i.ToString();
            rowDetail["OUTPUT_MEISAI_4_VALUE"] = "999,999,999";
            rowDetail["OUTPUT_MEISAI_5_VALUE"] = "01_MEISAI" + i.ToString();
            rowDetail["OUTPUT_MEISAI_6_VALUE"] = "01_MEISAI" + i.ToString();
            rowDetail["OUTPUT_MEISAI_7_VALUE"] = "01_MEISAI" + i.ToString();
            rowDetail["OUTPUT_MEISAI_8_VALUE"] = "出力項目８_MEISAI" + i.ToString();

            rowDetail["FILL_COND_1_TOTAL_1"] = "集計1の合計";
            rowDetail["FILL_COND_1_TOTAL_2"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_3"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_4"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_5"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_6"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_7"] = "002_" + i.ToString();
            rowDetail["FILL_COND_1_TOTAL_8"] = "002_" + i.ToString();

            rowDetail["FILL_COND_2_TOTAL_1"] = "集計2の合計";
            rowDetail["FILL_COND_2_TOTAL_2"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_3"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_4"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_5"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_6"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_7"] = "003_" + i.ToString();
            rowDetail["FILL_COND_2_TOTAL_8"] = "003_" + i.ToString();

            rowDetail["FILL_COND_3_TOTAL_1"] = "集計3の合計";
            rowDetail["FILL_COND_3_TOTAL_2"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_3"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_4"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_5"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_6"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_7"] = "004_" + i.ToString();
            rowDetail["FILL_COND_3_TOTAL_8"] = "004_" + i.ToString();

            rowDetail["ALL_TOTAL_1"] = "総合計";
            rowDetail["ALL_TOTAL_2"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_3"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_4"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_5"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_6"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_7"] = "0005_" + i.ToString();
            rowDetail["ALL_TOTAL_8"] = "0005_" + i.ToString();

            Detail.Rows.Add(rowDetail);
            }
            #endregion

            dataTableList.Add("Header", Header);
            dataTableList.Add("Detail", Detail);

            this.ReportInfo = new ReportInfoBase(new DataTable());
            this.ReportInfo.DataTableList = dataTableList;
        }

        #endregion

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.header_new));
                //allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

                return allControl.ToArray();
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
    }
}
