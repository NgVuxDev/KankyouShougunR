// $Id: UIFormG534.cs 11936 2013-12-19 02:35:13Z sys_dev_41 $

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
    /// 順位表/一覧画面
    /// </summary>
    public partial class UIFormG534 : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private G534_LogicClass logic;

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
        /// <param name="windowID">windowID</param>
        public UIFormG534(UIHeaderForm headerForm, WINDOW_ID windowID)
            : base(windowID)
        {
            try
            {
                LogUtility.DebugMethodStart(headerForm,windowID);

                this.InitializeComponent();

                // ヘッダフォームの項目を初期化
                this.header_new = headerForm;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new G534_LogicClass(this);
                //this.logic.SetHeader(header_new);

                this.logic.headerForm = this.header_new;
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
                
                this.Parent.Text += "／一覧";

                //// テストデータ作成
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

                // ".\Template\R434-Form.xml"　にデータを表示する
                //this.ReportInfo.Create(@".\Template\R433-Form.xml", "LAYOUT1", new DataTable());
                
                //// 印刷ポップ画面表示

                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.ReportInfo, this.WindowId))
                {
                    formReportPrintPopup.ShowDialog();
                }
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

                //var result = msgLogic.MessageBoxShow("C012");

                //if (result == DialogResult.Yes)
                //{
                //    CsvOutput();
                //}

                this.logic.CsvOutput();

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

        ///// <summary>
        ///// [F8]検索
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Search(object sender, EventArgs e)
        //{
        //}

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

        #region テストデータ作成方法
        /// <summary>
        /// テストデータ作成
        /// </summary>
        private void CreateTestData()
        {

            Dictionary<string, DataTable> dataTableList = new Dictionary<string, DataTable>();

            #region Header

            // 条件設定テーブル
            DataTable header = new DataTable();

            // カラム生成
            header.Columns.Add("CORP_RYAKU_NAME");
            header.Columns.Add("PRINT_DATE");
            header.Columns.Add("TITLE");
            header.Columns.Add("KYOTEN_CD");
            header.Columns.Add("KYOTEN_NAME_RYAKU");
            header.Columns.Add("ALERT_NUMBER");
            header.Columns.Add("READ_DATA_NUMBER");
            header.Columns.Add("DENPYOU_DATE_BEGIN");
            header.Columns.Add("DENPYOU_DATE_END");
            header.Columns.Add("DENPYOU_SHURUI");
            header.Columns.Add("DENPYOU_KUBUN");
            header.Columns.Add("FILL_COND_1_NAME");
            header.Columns.Add("FILL_COND_1_CD_BEGIN");
            header.Columns.Add("FILL_COND_1_VALUE_BEGIN");
            header.Columns.Add("FILL_COND_1_CD_END");
            header.Columns.Add("FILL_COND_1_VALUE_END");
            header.Columns.Add("FILL_COND_2_NAME");
            header.Columns.Add("FILL_COND_2_CD_BEGIN");
            header.Columns.Add("FILL_COND_2_VALUE_BEGIN");
            header.Columns.Add("FILL_COND_2_CD_END");
            header.Columns.Add("FILL_COND_2_VALUE_END");
            header.Columns.Add("FILL_COND_3_NAME");
            header.Columns.Add("FILL_COND_3_CD_BEGIN");
            header.Columns.Add("FILL_COND_3_VALUE_BEGIN");
            header.Columns.Add("FILL_COND_3_CD_END");
            header.Columns.Add("FILL_COND_3_VALUE_END");
            header.Columns.Add("FILL_COND_4_NAME");
            header.Columns.Add("FILL_COND_4_CD_BEGIN");
            header.Columns.Add("FILL_COND_4_VALUE_BEGIN");
            header.Columns.Add("FILL_COND_4_CD_END");
            header.Columns.Add("FILL_COND_4_VALUE_END");
            header.Columns.Add("ROW_NO_LABEL");
            //header.Columns.Add("SK_ITEM_CD_LABEL_1");
            //header.Columns.Add("SK_ITEM_CD_ALIGN_1");
            //header.Columns.Add("SK_ITEM_MEI_LABEL_1");
            //header.Columns.Add("SK_ITEM_MEI_ALIGN_1");
            //header.Columns.Add("SK_ITEM_CD_LABEL_2");
            //header.Columns.Add("SK_ITEM_CD_ALIGN_2");
            //header.Columns.Add("SK_ITEM_MEI_LABEL_2");
            //header.Columns.Add("SK_ITEM_MEI_ALIGN_2");
            header.Columns.Add("FILL_COND_1_CD_LABEL");
            header.Columns.Add("FILL_COND_1_NAME_LABEL");
            header.Columns.Add("FILL_COND_2_CD_LABEL");
            header.Columns.Add("FILL_COND_2_NAME_LABEL");
            header.Columns.Add("FILL_COND_3_CD_LABEL");
            header.Columns.Add("FILL_COND_3_NAME_LABEL");
            header.Columns.Add("FILL_COND_4_CD_LABEL");
            header.Columns.Add("FILL_COND_4_NAME_LABEL");
            header.Columns.Add("OUTPUT_ITEM_1_LABEL");


            // データ設定
            DataRow row = header.NewRow();
            row["CORP_RYAKU_NAME"] = "株式会社エジソン商事";
            row["PRINT_DATE"] = "2013/12/12";
            row["TITLE"] = "○○順位表";
            row["KYOTEN_CD"] = "01";
            row["KYOTEN_NAME_RYAKU"] = "拠点01";
            row["ALERT_NUMBER"] = "1000";
            row["READ_DATA_NUMBER"] = "2000";
            row["DENPYOU_DATE_BEGIN"] = "2013年12月01日 (日)";
            row["DENPYOU_DATE_END"] = "2013年12月31日 (火)";
            row["DENPYOU_SHURUI"] = "1.受入";
            row["DENPYOU_KUBUN"] = "1.売上";
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
            row["FILL_COND_4_NAME"] = "現場別";
            row["FILL_COND_4_CD_BEGIN"] = "000009";
            row["FILL_COND_4_VALUE_BEGIN"] = "現場別000009";
            row["FILL_COND_4_CD_END"] = "000009";
            row["FILL_COND_4_VALUE_END"] = "現場別000009";
            row["ROW_NO_LABEL"] = "No";
            row["FILL_COND_1_CD_LABEL"] = "業者CD";
            //row["SK_ITEM_CD_ALIGN_1"] = "1";
            row["FILL_COND_1_NAME_LABEL"] = "業者名";
            //row["SK_ITEM_MEI_ALIGN_1"] = "1";
            row["FILL_COND_2_CD_LABEL"] = "現場CD";
            //row["SK_ITEM_CD_ALIGN_2"] = "3";
            row["FILL_COND_2_NAME_LABEL"] = "現場名";
            //row["SK_ITEM_MEI_ALIGN_2"] = "1";
            row["FILL_COND_3_CD_LABEL"] = "品名CD";
            //row["SK_ITEM_CD_ALIGN_3"] = "3";
            row["FILL_COND_3_NAME_LABEL"] = "品名";
            //row["SK_ITEM_MEI_ALIGN_3"] = "1";
            row["FILL_COND_4_CD_LABEL"] = "伝票CD";
            //row["SK_ITEM_CD_ALIGN_4"] = "3";
            row["FILL_COND_4_NAME_LABEL"] = "伝票名";
            //row["SK_ITEM_MEI_ALIGN_4"] = "1";
            row["OUTPUT_ITEM_1_LABEL"] = "合計";  
          

            header.Rows.Add(row);
            dataTableList.Add("Header", header);

            #endregion

            #region Detail

            // 条件設定テーブル
            DataTable detail = new DataTable();

            // カラム生成
            detail.Columns.Add("GROUP_LABEL");
            detail.Columns.Add("ROW_NO");
            detail.Columns.Add("FILL_COND_1_CD");
            detail.Columns.Add("FILL_COND_1_NAME");
            detail.Columns.Add("FILL_COND_2_CD");
            detail.Columns.Add("FILL_COND_2_NAME");
            detail.Columns.Add("FILL_COND_3_CD");
            detail.Columns.Add("FILL_COND_3_NAME");
            detail.Columns.Add("FILL_COND_4_CD");
            detail.Columns.Add("FILL_COND_4_NAME");
            detail.Columns.Add("OUTPUT_ITEM_1");
            detail.Columns.Add("ALL_TOTAL_1");

            // データ設定
            row = detail.NewRow();
            row["GROUP_LABEL"] = "支払・受入";
            row["ROW_NO"] = "1";
            row["FILL_COND_1_CD"] = "000100";
            row["FILL_COND_1_NAME"] = "(株)エジソンエ業";
            row["FILL_COND_2_CD"] = "000002";
            row["FILL_COND_2_NAME"] = "つくば工場";
            row["FILL_COND_3_CD"] = "000002";
            row["FILL_COND_3_NAME"] = "鉄くず";
            row["FILL_COND_4_CD"] = "000001";
            row["FILL_COND_4_NAME"] = "test";
            row["OUTPUT_ITEM_1"] = "25";
            row["ALL_TOTAL_1"] = "";
            detail.Rows.Add(row);

            row = detail.NewRow();
            row["GROUP_LABEL"] = "支払・受入";
            row["ROW_NO"] = "2";
            row["FILL_COND_1_CD"] = "000100";
            row["FILL_COND_1_NAME"] = "(株)エジソンエ業";
            row["FILL_COND_2_CD"] = "000002";
            row["FILL_COND_2_NAME"] = "つくば工場";
            row["FILL_COND_3_CD"] = "000002";
            row["FILL_COND_3_NAME"] = "鉄くず";
            row["FILL_COND_4_CD"] = "000001";
            row["FILL_COND_4_NAME"] = "test";
            row["OUTPUT_ITEM_1"] = "1,000";
            row["ALL_TOTAL_1"] = "";
            detail.Rows.Add(row);


            row = detail.NewRow();
            row["GROUP_LABEL"] = "支払・出荷";
            row["ROW_NO"] = "1";
            row["FILL_COND_1_CD"] = "000100";
            row["FILL_COND_1_NAME"] = "(株)エジソンエ業";
            row["FILL_COND_2_CD"] = "000002";
            row["FILL_COND_2_NAME"] = "つくば工場";
            row["FILL_COND_3_CD"] = "000002";
            row["FILL_COND_3_NAME"] = "鉄くず";
            row["FILL_COND_4_CD"] = "000001";
            row["FILL_COND_4_NAME"] = "test";
            row["OUTPUT_ITEM_1"] = "2,000";
            row["ALL_TOTAL_1"] = "30,000";
            detail.Rows.Add(row);

            dataTableList.Add("Detail", detail);


            #endregion


            this.ReportInfo = new ReportInfoBase();
            this.ReportInfo.DataTableList = dataTableList;

        }
        #endregion

        //private void meisai_CellContentClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        //{

        //}

    }
}
