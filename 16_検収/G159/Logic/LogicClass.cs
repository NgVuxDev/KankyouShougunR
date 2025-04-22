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
using GrapeCity.Win.MultiRow;
using System.Windows.Forms;
using System.Data;
using r_framework.Dto;
using Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup;
using Shougun.Function.ShougunCSCommon.Utility;
using CommonChouhyouPopup;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Drawing;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.CustomControl;
using System.Data.SqlTypes;
using CommonChouhyouPopup.App;
using Shougun.Core.Common.BusinessCommon.Const;
using Seasar.Framework.Exceptions;
using Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Const;

namespace Shougun.Core.Inspection.KensyuuIchiran
{

    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class KensyuuIchiranLogicCls : IBuisinessLogic
    {
        M_SYS_INFO mSysInfo;

        /// <summary>
        /// 範囲条件指定結果
        /// </summary>
        private KenshuIchiranJokenShiteiPopup.Const.KenshuIshiranConstans.ConditionInfo param { get; set; }

        /// <summary>
        /// DTO
        /// </summary>
        private KensyuuIchiranDTOCls dto;

        /// <summary>
        /// Form
        /// </summary>
        private KensyuuIchiranForm form;

        /// <summary>
        /// ヘッダー
        /// </summary>
        private KensyuuIchiranHeader header;
        private DBAccessor CommonDBAccessor;
        private string sWindowId;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal MessageBoxShowLogic errmessage;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KensyuuIchiranLogicCls(KensyuuIchiranForm targetForm, KensyuuIchiranHeader targetHeader)
        {
            LogUtility.DebugMethodStart(targetForm, targetHeader);
            this.form = targetForm;
            this.header = targetHeader;
            this.header.logic = this;
            this.dto = new KensyuuIchiranDTOCls();
            sWindowId = Convert.ToString(WINDOW_ID.T_KENSYUU_ICHIRAN);
            this.CommonDBAccessor = new DBAccessor();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.errmessage = new MessageBoxShowLogic();
            LogUtility.DebugMethodEnd();
        }

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Inspection.KensyuuIchiran.Setting.ButtonSetting.xml";

        #region デフォルトメソッド
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //////システム情報を取得し、初期値をセットする
                M_SYS_INFO sysInfo = new M_SYS_INFO();
                sysInfo = CommonDBAccessor.GetSysInfo();
                this.mSysInfo = sysInfo;
                if (sysInfo != null)
                {
                    //システム情報からアラート件数を取得
                    this.header.ctxt_AlertNumber.Text = CommonCalc.DecimalFormat(Convert.ToDecimal(sysInfo.ICHIRAN_ALERT_KENSUU.ToString()));
                }
                else
                {
                    this.header.ctxt_AlertNumber.Text = "0";
                }

                // 画面初期表示設定
                this.InitializeScreen();

                //this.form.l
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.SetDefaultSearchConditionValues();

                this.SetEnabled();

                //setLoadControl();

                ////open KensuyIchiran popup
                //object sender = new object();
                //EventArgs e = new EventArgs();
                //bt_func8_Click(sender, e);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
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
            if (this.form.grdGetMultiRow.RowCount != 0)
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
        private void SetDefaultSearchConditionValues()
        {
            LogUtility.DebugMethodStart();

            this.header.ctxt_AlertNumber.ReadOnly = false;
            this.header.ctxt_AlertNumber.Enabled = true;
            this.header.ctxt_AlertNumber.MaxLength = 5;
            this.SetInitSearchConditionValues();

            dtReturnkenshumeIchiran01 = new DataTable();
            kenshumeIchiranEntity01 = new Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件表示用項目の初期化
        /// </summary>
        private void SetInitSearchConditionValues()
        {
            form.SHUKKA_ENTRY_DENPYOU_DATE_FROM.Value = string.Empty;
            form.SHUKKA_ENTRY_DENPYOU_DATE_TO.Value = string.Empty;
            form.SHUKKA_ENTRY_KENSYU_DATE_FROM.Value = string.Empty;
            form.SHUKKA_ENTRY_KENSYU_DATE_TO.Value = string.Empty;
            form.SHUKKA_DETAIL_HINMEI_CD_1.Text = string.Empty;
            form.SHUKKA_DETAIL_HINMEI_NAME_1.Text = string.Empty;
            form.SHUKKA_DETAIL_HINMEI_CD_2.Text = string.Empty;
            form.SHUKKA_DETAIL_HINMEI_NAME_2.Text = string.Empty;
            form.KENSHU_DETAIL_HINMEI_CD_1.Text = string.Empty;
            form.KENSHU_DETAIL_HINMEI_NAME_1.Text = string.Empty;
            form.KENSHU_DETAIL_HINMEI_CD_2.Text = string.Empty;
            form.KENSHU_DETAIL_HINMEI_NAME_2.Text = string.Empty;
            form.txt_Kenshuumu.Text = string.Empty;
            form.txt_Kenshuyohi.Text = string.Empty;
            form.SHUKKA_ENTRY_TORIHIKISAKI_CD_1.Text = string.Empty;
            form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_1.Text = string.Empty;
            form.SHUKKA_ENTRY_TORIHIKISAKI_CD_2.Text = string.Empty;
            form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_2.Text = string.Empty;
            form.SHUKKA_ENTRY_GYOUSHA_CD_1.Text = string.Empty;
            form.SHUKKA_ENTRY_GYOUSHA_NAME_1.Text = string.Empty;
            form.SHUKKA_ENTRY_GYOUSHA_CD_2.Text = string.Empty;
            form.SHUKKA_ENTRY_GYOUSHA_NAME_2.Text = string.Empty;
            form.SHUKKA_ENTRY_GENBA_CD_1.Text = string.Empty;
            form.SHUKKA_ENTRY_GENBA_NAME_1.Text = string.Empty;
            form.SHUKKA_ENTRY_GENBA_CD_2.Text = string.Empty;
            form.SHUKKA_ENTRY_GENBA_NAME_2.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_1.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_1.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_2.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_2.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_1.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_1.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_2.Text = string.Empty;
            form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_2.Text = string.Empty;
            form.SHUKKA_NET_JYUURYOU_TOTAL.Text = string.Empty;
            form.KENSHU_NET_JYUURYOU_TOTAL.Text = string.Empty;
            form.KENSHU_BUBIKI_TOTAL.Text = string.Empty;
            form.KENSHU_URIAGE_KINGAKU_TOTAL.Text = string.Empty;
            form.KENSHU_SHIHARAI_KINGAKU_TOTAL.Text = string.Empty;
        }

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen()
        {
            parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.ProcessButtonPanel.Visible = false;
            //this.header.ctxt_AlertNumber.Text = "1000";

            //this.form.gcCustomMultiRow1[0, ""].CellIndex = 0;
            // this.form.gcCustomMultiRow1[0, ""].CellIndex = 0;
        }
        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.form.gcCustomMultiRow1.RowCount > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        CSVExport csvLogic = new CSVExport();
                        WINDOW_ID id = this.form.WindowId;
                        csvLogic.ConvertCustomDataGridViewToCsv(this.form.grdGetMultiRow, true, false, id.ToTitleString(), this.form);

                    }
                }
                LogUtility.DebugMethodEnd(sender, e);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (dtReturnkenshumeIchiran01.Rows.Count > 0)
                {
                    ReportInfoR400 reportInfo = new ReportInfoR400(windowID: WINDOW_ID.T_KENSYUU_ICHIRAN);
                    setPrintData(reportInfo);
                    if (reportInfo != null)
                    {
                        reportInfo.Create(@".\Template\R400-Form.xml", "LAYOUT1", new DataTable());
                        reportInfo.Title = WINDOW_ID.T_KENSYUU_ICHIRAN.ToTitleString();
                        FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, "R400", WINDOW_ID.T_KENSYUU_ICHIRAN);
                        popup.PrintInitAction = 3;
                        popup.PrintXPS();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }

        public void setPrintData(CommonChouhyouPopup.App.ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart(reportInfo);
                /// <summary>
                /// ヘッダ部(R400)
                /// </summary>
                string[] headerReportItemName_R400 = { "CORP_RYAKU_NAME", "PRINT_DATE", "TITLE", "KYOTEN_NAME", "SHUKKA_DATE", "KENSHU_DATE", "FILL_COND_CD_1", "KENSHU_YOUHI", "KENSHU_UMU", "FILL_COND_CD_2",
                                                         "FILL_COND_CD_3", "FILL_COND_CD_4", "FILL_COND_CD_5", "FILL_COND_CD_6", "FILL_COND_CD_7"};

                /// <summary>
                /// 明細部(R400)
                /// </summary>
                string[] detailReportItemName_R400 = { "SHUKKA_NUMBER", "SHUKKA_DATE", "KENSHU_DATE", "GYOUSHA_CD", "GYOUSHA_NAME", "SHUKKA_HINMEI_CD", "SHUKKA_HINMEI_NAME", "SHUKKA_NET_JUURYOU",
                                                         "SHUKKA_SUURYOU", "SHUKKA_UNIT", "ROW_NO", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME", "GENBA_CD", "GENBA_NAME",
                                                         "KENSHU_HINMEI_CD", "KENSHU_HINMEI_NAME", "KENSHU_JUURYOU", "KENSHU_SUURYOU", "BUBUKI", "KENSHU_UNIT", "KENSHU_TANKA", "KENSHU_KINGAKU",
                                                         "SHUKKA_DENPYOU_KBN", "KENSHU_DENPYOU_KBN"};


                string[] groupFooterReportItemName_R400 = { "SHUKKA_SHOUMI_KEI", "SHUKKA_KINGAKU_KEI", "KENSHU_SHOUMI_KEI", "BUBUKI_KEI", "KENSHU_KINGAKU_KEI" };

                string[] footerReportItemName_R400 = { "SHUKKA_NET_JYUURYOU_TOTAL", "KENSHU_NET_JYUURYOU_TOTAL", "KENSHU_BUBIKI_TOTAL", "KENSHU_URIAGE_KINGAKU_TOTAL", "KENSHU_SHIHARAI_KINGAKU_TOTAL" };

                DataTable dataTableHeader = new DataTable();
                dataTableHeader.TableName = "Header";
                DataTable dataTableDetail = new DataTable();
                dataTableDetail.TableName = "Detail";
                DataTable dataTableGroupFooter = new DataTable();
                dataTableGroupFooter.TableName = "GroupFooter";
                DataTable dataTableFooter = new DataTable();
                dataTableFooter.TableName = "Footer";

                for (int i = 0; i < headerReportItemName_R400.Length; i++)
                {
                    dataTableHeader.Columns.Add(headerReportItemName_R400[i]);
                }

                for (int i = 0; i < detailReportItemName_R400.Length; i++)
                {
                    dataTableDetail.Columns.Add(detailReportItemName_R400[i]);
                }

                for (int i = 0; i < groupFooterReportItemName_R400.Length; i++)
                {
                    dataTableGroupFooter.Columns.Add(groupFooterReportItemName_R400[i]);
                }

                for (int i = 0; i < footerReportItemName_R400.Length; i++)
                {
                    dataTableFooter.Columns.Add(footerReportItemName_R400[i]);
                }

                //add header to report info
                AddDataHeader(dataTableHeader, reportInfo);
                //add detail to report info
                AddDataDetail(dataTableDetail, reportInfo);
                //add group footer to report info
                AddDataGroupFooter(dataTableGroupFooter, reportInfo);
                //add footer to report info
                AddDataFooter(dataTableFooter, reportInfo);
            }
            catch (Exception ex)
            {
                LogUtility.Error("setPrintData", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        private decimal KENSHU_DETAIL_BUBIKI = 0;
        private decimal KENSHU_DETAIL_KINGAKU = 0;
        private decimal SHUKKA_DETAIL_NET_JYUURYOU = 0;
        private decimal KENSHU_DETAIL_KENSHU_NET = 0;
        private decimal KENSHU_DETAIL_TANKA = 0;

        /// <summary>
        /// add data footer
        /// </summary>
        /// <param name="dataTableHeader"></param>
        /// <param name="reportInfo"></param>
        private void AddDataGroupFooter(DataTable dataTableFooter, CommonChouhyouPopup.App.ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart(dataTableFooter, reportInfo);
                // kenshumeIchiranEntity01
                DataRow rowTmpFooter = dataTableFooter.NewRow();

                rowTmpFooter["SHUKKA_SHOUMI_KEI"] = CommonCalc.DecimalFormat(SHUKKA_DETAIL_NET_JYUURYOU);
                // 検収時正味合計
                rowTmpFooter["KENSHU_SHOUMI_KEI"] = CommonCalc.DecimalFormat(KENSHU_DETAIL_KENSHU_NET);
                // 歩引
                rowTmpFooter["BUBUKI_KEI"] = CommonCalc.DecimalFormat(KENSHU_DETAIL_BUBIKI);
                // 検収時金額計
                rowTmpFooter["KENSHU_KINGAKU_KEI"] = CommonCalc.DecimalFormat(KENSHU_DETAIL_KINGAKU);

                dataTableFooter.Rows.Add(rowTmpFooter);
                reportInfo.DataTableList.Add("GroupFooter", dataTableFooter);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }
        }

        /// <summary>
        /// フッダー項目の追加
        /// </summary>
        /// <param name="dataTableFooter"></param>
        /// <param name="reportInfo"></param>
        private void AddDataFooter(DataTable dataTableFooter, CommonChouhyouPopup.App.ReportInfoBase reportInfo)
        {
            try
            {
                DataRow rowTmpFooter = dataTableFooter.NewRow();

                rowTmpFooter["SHUKKA_NET_JYUURYOU_TOTAL"] = this.form.SHUKKA_NET_JYUURYOU_TOTAL.Text;
                // 検収時正味合計
                rowTmpFooter["KENSHU_NET_JYUURYOU_TOTAL"] = this.form.KENSHU_NET_JYUURYOU_TOTAL.Text;
                // 歩引
                rowTmpFooter["KENSHU_BUBIKI_TOTAL"] = this.form.KENSHU_BUBIKI_TOTAL.Text;
                // 検収時金額計
                rowTmpFooter["KENSHU_URIAGE_KINGAKU_TOTAL"] = this.form.KENSHU_URIAGE_KINGAKU_TOTAL.Text;
                // 検収時金額計
                rowTmpFooter["KENSHU_SHIHARAI_KINGAKU_TOTAL"] = this.form.KENSHU_SHIHARAI_KINGAKU_TOTAL.Text;

                dataTableFooter.Rows.Add(rowTmpFooter);
                reportInfo.DataTableList.Add("Footer", dataTableFooter);

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
        }

        /// <summary>
        /// add data detail
        /// </summary>
        /// <param name="dataTableHeader"></param>
        /// <param name="reportInfo"></param>
        private void AddDataDetail(DataTable dataTableDetail, CommonChouhyouPopup.App.ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart(dataTableDetail, reportInfo);
                // kenshumeIchiranEntity01
                DataRow rowTmp;
                KENSHU_DETAIL_BUBIKI = 0;
                KENSHU_DETAIL_KINGAKU = 0;
                SHUKKA_DETAIL_NET_JYUURYOU = 0;
                KENSHU_DETAIL_KENSHU_NET = 0;

                foreach (Row row in this.form.gcCustomMultiRow1.Rows)
                {
                    if (row.Cells["SHUKKA_ENTRY_SHUKKA_NUMBER"].Value != null)
                    {
                        rowTmp = dataTableDetail.NewRow();

                        // 出荷番号
                        rowTmp["SHUKKA_NUMBER"] = row.Cells["SHUKKA_ENTRY_SHUKKA_NUMBER"].Value;
                        // 出荷日時
                        if (row.Cells["SHUKKA_ENTRY_DENPYOU_DATE"].Value != null)
                        {
                            rowTmp["SHUKKA_DATE"] = DateTime.Parse(row.Cells["SHUKKA_ENTRY_DENPYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd");// "2013/12/01 12:00:00";
                        }
                        // 検収日時
                        if (row.Cells["SHUKKA_ENTRY_KENSYU_DATE"].Value != null)
                        {
                            rowTmp["KENSHU_DATE"] = DateTime.Parse(row.Cells["SHUKKA_ENTRY_KENSYU_DATE"].Value.ToString()).ToString("yyyy/MM/dd");
                        }

                        // 業者コード
                        if (row.Cells["SHUKKA_ENTRY_GYOUSHA_CD"].Value != null)
                        {
                            rowTmp["GYOUSHA_CD"] = row.Cells["SHUKKA_ENTRY_GYOUSHA_CD"].Value.ToString();
                        }

                        // 業者名
                        if (row.Cells["SHUKKA_ENTRY_GYOUSHA_NAME"].Value != null)
                        {
                            rowTmp["GYOUSHA_NAME"] = row.Cells["SHUKKA_ENTRY_GYOUSHA_NAME"].Value.ToString();
                        }

                        // 出荷時品コード
                        if (row.Cells["SHUKKA_DETAIL_HINMEI_CD"].Value != null)
                        {
                            rowTmp["SHUKKA_HINMEI_CD"] = row.Cells["SHUKKA_DETAIL_HINMEI_CD"].Value.ToString();
                        }

                        // 出荷時品名
                        if (row.Cells["SHUKKA_DETAIL_HINMEI_NAME"].Value != null)
                        {
                            rowTmp["SHUKKA_HINMEI_NAME"] = row.Cells["SHUKKA_DETAIL_HINMEI_NAME"].Value.ToString();
                        }

                        // 出荷時伝票区分
                        if (row.Cells["SHUKKA_DETAIL_DENPYOU_KBN"].Value != null)
                        {
                            rowTmp["SHUKKA_DENPYOU_KBN"] = row.Cells["SHUKKA_DETAIL_DENPYOU_KBN"].Value.ToString();
                        }

                        // 出荷時正味（kg）
                        if (row.Cells["SHUKKA_DETAIL_NET_JYUURYOU"].FormattedValue != null)
                        {
                            rowTmp["SHUKKA_NET_JUURYOU"] = row.Cells["SHUKKA_DETAIL_NET_JYUURYOU"].FormattedValue.ToString();
                        }

                        // 出荷時数量
                        if (row.Cells["SHUKKA_DETAIL_SUURYOU"].FormattedValue != null)
                        {
                            rowTmp["SHUKKA_SUURYOU"] = row.Cells["SHUKKA_DETAIL_SUURYOU"].FormattedValue.ToString();
                        }

                        // 出荷単位
                        if (row.Cells["M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU"].Value != null)
                        {
                            rowTmp["SHUKKA_UNIT"] = row.Cells["M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU"].Value.ToString();
                        }

                        // 明細行番
                        if (row.Cells["ROWNO"].Value != null)
                        {
                            rowTmp["ROW_NO"] = row.Cells["ROWNO"].Value.ToString();
                        }

                        // 取引先コード
                        if (row.Cells["SHUKKA_ENTRY_TORIHIKISAKI_CD"].Value != null)
                        {
                            rowTmp["TORIHIKISAKI_CD"] = row.Cells["SHUKKA_ENTRY_TORIHIKISAKI_CD"].Value.ToString();
                        }

                        // 取引先
                        if (row.Cells["SHUKKA_ENTRY_TORIHIKISAKI_NAME"].Value != null)
                        {
                            rowTmp["TORIHIKISAKI_NAME"] = row.Cells["SHUKKA_ENTRY_TORIHIKISAKI_NAME"].Value.ToString();
                        }

                        // 現場コード
                        if (row.Cells["SHUKKA_ENTRY_GENBA_CD"].Value != null)
                        {
                            rowTmp["GENBA_CD"] = row.Cells["SHUKKA_ENTRY_GENBA_CD"].Value.ToString();
                        }

                        // 現場名
                        if (row.Cells["SHUKKA_ENTRY_GENBA_NAME"].Value != null)
                        {
                            rowTmp["GENBA_NAME"] = row.Cells["SHUKKA_ENTRY_GENBA_NAME"].Value.ToString();
                        }

                        // 検収時品コード
                        if (row.Cells["KENSHU_DETAIL_HINMEI_CD"].Value != null)
                        {
                            rowTmp["KENSHU_HINMEI_CD"] = row.Cells["KENSHU_DETAIL_HINMEI_CD"].Value.ToString();
                        }

                        // 検収時品名
                        if (row.Cells["KENSHU_DETAIL_HINMEI_NAME"].Value != null)
                        {
                            rowTmp["KENSHU_HINMEI_NAME"] = row.Cells["KENSHU_DETAIL_HINMEI_NAME"].Value.ToString();
                        }

                        // 検収時伝票区分
                        if (row.Cells["KENSHU_DETAIL_DENPYOU_KBN"].Value != null)
                        {
                            rowTmp["KENSHU_DENPYOU_KBN"] = row.Cells["KENSHU_DETAIL_DENPYOU_KBN"].Value.ToString();
                        }

                        // 検収時正味（kg）
                        if (row.Cells["KENSHU_DETAIL_KENSHU_NET"].FormattedValue != null)
                        {
                            rowTmp["KENSHU_JUURYOU"] = row.Cells["KENSHU_DETAIL_KENSHU_NET"].FormattedValue.ToString();
                        }

                        // 検収時数量
                        if (row.Cells["KENSHU_DETAIL_SUURYOU"].FormattedValue != null)
                        {
                            rowTmp["KENSHU_SUURYOU"] = row.Cells["KENSHU_DETAIL_SUURYOU"].FormattedValue.ToString();

                        }

                        // 歩引
                        if (row.Cells["KENSHU_DETAIL_BUBIKI"].FormattedValue != null)
                        {
                            rowTmp["BUBUKI"] = row.Cells["KENSHU_DETAIL_BUBIKI"].FormattedValue.ToString();
                        }

                        // 検収単位
                        if (row.Cells["M_UNIT_KENSHU_DETAIL_UNIT_NAME_RYAKU"].Value != null)
                        {
                            rowTmp["KENSHU_UNIT"] = row.Cells["M_UNIT_KENSHU_DETAIL_UNIT_NAME_RYAKU"].Value.ToString();
                        }

                        // 検収時単価
                        if (row.Cells["KENSHU_DETAIL_TANKA"].FormattedValue != null)
                        {
                            rowTmp["KENSHU_TANKA"] = row.Cells["KENSHU_DETAIL_TANKA"].FormattedValue.ToString();
                        }

                        // 検収時金額
                        if (row.Cells["KENSHU_DETAIL_KINGAKU"].FormattedValue != null)
                        {
                            rowTmp["KENSHU_KINGAKU"] = row.Cells["KENSHU_DETAIL_KINGAKU"].FormattedValue.ToString();
                        }

                        dataTableDetail.Rows.Add(rowTmp);
                    }
                    else
                    {
                        // フッダーに表示する情報を設定
                        // 明細行が多いと時間が掛かってしまうためここで設定

                        // 出荷時正味（kg）
                        decimal shukkaNetJyuuryou = 0;
                        if (row.Cells["SHUKKA_DETAIL_NET_JYUURYOU"].Value != null
                            && decimal.TryParse(Convert.ToString(row.Cells["SHUKKA_DETAIL_NET_JYUURYOU"].Value), out shukkaNetJyuuryou))
                        {
                            SHUKKA_DETAIL_NET_JYUURYOU = SHUKKA_DETAIL_NET_JYUURYOU + shukkaNetJyuuryou;
                        }

                        // 検収時正味（kg）
                        decimal kenshuNetJuuryou = 0;
                        if (row.Cells["KENSHU_DETAIL_KENSHU_NET"].Value != null
                            && decimal.TryParse(Convert.ToString(row.Cells["KENSHU_DETAIL_KENSHU_NET"].Value), out kenshuNetJuuryou))
                        {
                            KENSHU_DETAIL_KENSHU_NET = KENSHU_DETAIL_KENSHU_NET + kenshuNetJuuryou;
                        }

                        // 歩引
                        decimal bubiki = 0;
                        if (row.Cells["KENSHU_DETAIL_BUBIKI"].Value != null
                            && decimal.TryParse(Convert.ToString(row.Cells["KENSHU_DETAIL_BUBIKI"].Value), out bubiki))
                        {
                            KENSHU_DETAIL_BUBIKI = KENSHU_DETAIL_BUBIKI + bubiki;
                        }

                        // 検収時金額
                        decimal kenshuKingaku = 0;
                        if (row.Cells["KENSHU_DETAIL_KINGAKU"].Value != null
                            && decimal.TryParse(Convert.ToString(row.Cells["KENSHU_DETAIL_KINGAKU"].Value), out kenshuKingaku))
                        {
                            KENSHU_DETAIL_KINGAKU = KENSHU_DETAIL_KINGAKU + kenshuKingaku;
                        }
                    }
                }

                reportInfo.DataTableList.Add("Detail", dataTableDetail);
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw ex;
            }
        }

        /// <summary>
        /// add data header
        /// </summary>
        /// <param name="dataTableHeader"></param>
        /// <param name="reportInfo"></param>
        private void AddDataHeader(DataTable dataTableHeader, CommonChouhyouPopup.App.ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart(dataTableHeader, reportInfo);
                // kenshumeIchiranEntity01
                DataRow rowTmpHeader = dataTableHeader.NewRow();
                rowTmpHeader["CORP_RYAKU_NAME"] = kenshumeIchiranEntity01.Shukka_Entry_CORP_RYAKU_NAME;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                rowTmpHeader["PRINT_DATE"] = this.getDBDateTime().ToString();//kenshumeIchiranEntity01;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                rowTmpHeader["TITLE"] = WINDOW_ID.T_KENSYUU_ICHIRAN;
                rowTmpHeader["KYOTEN_NAME"] = kenshumeIchiranEntity01.Shukka_Entry_KYOTEN_NAME;
                if (!kenshumeIchiranEntity01.Shukka_Entry_Denpyou_Date_Begin.IsNull)
                {
                    rowTmpHeader["SHUKKA_DATE"] =
                        ((DateTime)kenshumeIchiranEntity01.Shukka_Entry_Denpyou_Date_Begin).ToString("yyyy/MM/dd")
                        + " ～ "
                        + ((DateTime)kenshumeIchiranEntity01.Shukka_Entry_Denpyou_Date_End).ToString("yyyy/MM/dd");
                }
                else
                {
                    rowTmpHeader["SHUKKA_DATE"] = string.Empty;
                }

                if (!kenshumeIchiranEntity01.Shukka_Entry_Kenshu_Date_Begin.IsNull)
                {
                    rowTmpHeader["KENSHU_DATE"] =
                        ((DateTime)kenshumeIchiranEntity01.Shukka_Entry_Kenshu_Date_Begin).ToString("yyyy/MM/dd")
                        + " ～ "
                        + ((DateTime)kenshumeIchiranEntity01.Shukka_Entry_Kenshu_Date_End).ToString("yyyy/MM/dd");
                }
                else
                {
                    rowTmpHeader["KENSHU_DATE"] = string.Empty;
                }

                // 取引先
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Torihikisaki_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Torihikisaki_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_1"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_1"] = kenshumeIchiranEntity01.Shukka_Entry_Torihikisaki_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_Torihikisaki_Cd_2;
                }

                rowTmpHeader["KENSHU_YOUHI"] = "";
                if (!string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_KENSHU_UMU))
                {
                    if (kenshumeIchiranEntity01.Shukka_Entry_KENSHU_UMU == "1")
                    {
                        rowTmpHeader["KENSHU_YOUHI"] = "無";
                    }
                    else if (kenshumeIchiranEntity01.Shukka_Entry_KENSHU_UMU == "2")
                    {
                        rowTmpHeader["KENSHU_YOUHI"] = "有";
                    }
                    else if (kenshumeIchiranEntity01.Shukka_Entry_KENSHU_UMU == "3")
                    {
                        rowTmpHeader["KENSHU_YOUHI"] = "全て";
                    }

                }
                rowTmpHeader["KENSHU_UMU"] = "";
                switch (kenshumeIchiranEntity01.Shukka_Entry_KENSHU_JYOUKYOU)
                {
                    case 1:
                        rowTmpHeader["KENSHU_UMU"] = "未検収";
                        break;

                    case 2:
                        rowTmpHeader["KENSHU_UMU"] = "検収済";
                        break;
                }

                // 業者範囲
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Gyousha_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Gyousha_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_2"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_2"] = kenshumeIchiranEntity01.Shukka_Entry_Gyousha_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_Gyousha_Cd_2;
                }
                // 現場範囲
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Genba_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Genba_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_3"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_3"] = kenshumeIchiranEntity01.Shukka_Entry_Genba_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_Genba_Cd_2;
                }
                // 荷積業者
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Gyousha_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Gyousha_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_4"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_4"] = kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Gyousha_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Gyousha_Cd_2;
                }
                // 荷積現場
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Genba_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Genba_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_5"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_5"] = kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Genba_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_Nizumi_Genba_Cd_2;
                }
                // 出荷品名
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Detail_Hinmei_Cd_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Detail_Hinmei_Cd_2))
                {
                    rowTmpHeader["FILL_COND_CD_6"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_6"] = kenshumeIchiranEntity01.Shukka_Detail_Hinmei_Cd_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Detail_Hinmei_Cd_2;
                }
                // 検収品名
                if (string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_KENSHUHINMEI_CD_1) && string.IsNullOrEmpty(kenshumeIchiranEntity01.Shukka_Entry_KENSHUHINMEI_CD_2))
                {
                    rowTmpHeader["FILL_COND_CD_7"] = "全て";
                }
                else
                {
                    rowTmpHeader["FILL_COND_CD_7"] = kenshumeIchiranEntity01.Shukka_Entry_KENSHUHINMEI_CD_1 + " ～ " + kenshumeIchiranEntity01.Shukka_Entry_KENSHUHINMEI_CD_2;
                }


                dataTableHeader.Rows.Add(rowTmpHeader);

                reportInfo.DataTableList.Add("Header", dataTableHeader);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }

        }

        /// <summary>
        /// set kenshumesaiEntityList
        /// </summary>

        private static Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls kenshumeIchiranEntity01 = new Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls();
        private static DataTable dtReturnkenshumeIchiran01 = new DataTable();
        private static KenshuIshiranConstans.ConditionInfo joken1 = new KenshuIshiranConstans.ConditionInfo();
        private void GetConditionKenshumeIchiran(Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls kenshumeIchiranEntity02, DataTable dtReturnkenshumeIchiran02, KenshuIshiranConstans.ConditionInfo joken2)
        {
            kenshumeIchiranEntity01 = kenshumeIchiranEntity02;
            dtReturnkenshumeIchiran01 = dtReturnkenshumeIchiran02;
            joken1 = joken2;
        }


        public void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodEnd(sender, e);
            try
            {

                // 受渡項目生成
                this.param = this.CreateParams();

                int alertNumber = this.header.ctxt_AlertNumber.Text == "" ? 0 : Convert.ToInt32(this.header.ctxt_AlertNumber.Text.Replace(",", ""));
                var callHeader = new Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.UIHeader();
                var callForm = new Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KenshuIchiranJokenShiteiPopupForm(callHeader, alertNumber, this.param, this.GetConditionKenshumeIchiran);
                var businessForm = new BasePopForm(callForm, callHeader);

                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                callForm.sysDate = this.parentForm.sysDate;
                businessForm.sysDate = this.parentForm.sysDate;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    // 画面表示位置を設定（親フォーム中央）
                    businessForm.StartPosition = FormStartPosition.Manual;
                    int parentFormHeight, parentFormWidth;
                    // 親フォームの高さ
                    parentFormHeight = parentForm.Height;
                    // 親フォームの幅
                    parentFormWidth = parentForm.Width;

                    //フォーム位置を中央に設定
                    businessForm.Location = new Point(parentForm.Left + (parentFormWidth - 700) / 2, parentForm.Top + (parentFormHeight - 460) / 2);

                    var dr = businessForm.ShowDialog();

                    //load detail-detail

                    this.param = joken1;

                    if (dtReturnkenshumeIchiran01.Rows.Count > 0)
                    {
                        this.form.gcCustomMultiRow1.Rows.Clear();
                        this.form.grdGetMultiRow.Rows.Clear();
                        this.SetInitSearchConditionValues();

                        loadDetailDetail(dtReturnkenshumeIchiran01);
                        if (kenshumeIchiranEntity01 != null)
                        {
                            loadSearchString(kenshumeIchiranEntity01);
                        }
                        this.SetEnabled();
                    }
                    //businessForm.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                this.errmessage.MessageBoxShow("E245");
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Load All Control
        private void setControl()
        {
            LogUtility.DebugMethodStart();
            #region Detail-Detail
            //this.form.gcCustomMultiRow1.Row
            foreach (Row dr in this.form.gcCustomMultiRow1.Rows)
            {
                dr.Cells["Sogoke"].Visible = false;
            }
            #endregion
            LogUtility.DebugMethodEnd();
        }


        ///<summary>
        ///clear value control
        /// </summary>
        private void ClearValueControl()
        {
            this.header.KYOTEN_CD.Text = "";
            this.header.KYOTEN_NAME_RYAKU.Text = "";
            this.form.SHUKKA_ENTRY_DENPYOU_DATE_FROM.Value = this.parentForm.sysDate;
            this.form.SHUKKA_ENTRY_DENPYOU_DATE_TO.Value = this.parentForm.sysDate;
            this.form.SHUKKA_ENTRY_KENSYU_DATE_FROM.Value = this.parentForm.sysDate;
            this.form.SHUKKA_ENTRY_KENSYU_DATE_TO.Value = this.parentForm.sysDate;
            this.form.SHUKKA_DETAIL_HINMEI_CD_1.Text = "";
            this.form.SHUKKA_DETAIL_HINMEI_NAME_1.Text = "";
            this.form.SHUKKA_DETAIL_HINMEI_CD_2.Text = "";
            this.form.SHUKKA_DETAIL_HINMEI_NAME_2.Text = "";
            this.form.KENSHU_DETAIL_HINMEI_CD_1.Text = "";
            this.form.KENSHU_DETAIL_HINMEI_NAME_1.Text = "";
            this.form.KENSHU_DETAIL_HINMEI_CD_2.Text = "";
            this.form.KENSHU_DETAIL_HINMEI_NAME_2.Text = "";
            this.form.txt_Kenshuyohi.Text = "";
            this.form.txt_Kenshuumu.Text = "";
            this.form.SHUKKA_ENTRY_TORIHIKISAKI_CD_1.Text = "";
            this.form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_1.Text = "";
            this.form.SHUKKA_ENTRY_TORIHIKISAKI_CD_2.Text = "";
            this.form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_2.Text = "";
            this.form.SHUKKA_ENTRY_GYOUSHA_CD_1.Text = "";
            this.form.SHUKKA_ENTRY_GYOUSHA_NAME_1.Text = "";
            this.form.SHUKKA_ENTRY_GYOUSHA_CD_2.Text = "";
            this.form.SHUKKA_ENTRY_GYOUSHA_NAME_2.Text = "";
            this.form.SHUKKA_ENTRY_GENBA_CD_1.Text = "";
            this.form.SHUKKA_ENTRY_GENBA_NAME_1.Text = "";
            this.form.SHUKKA_ENTRY_GENBA_CD_2.Text = "";
            this.form.SHUKKA_ENTRY_GENBA_NAME_2.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_1.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_1.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_2.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_2.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_1.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_1.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_2.Text = "";
            this.form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_2.Text = "";
        }

        /// <summary>
        /// load condition search to header
        /// </summary>
        /// <param name="entityDTO"></param>
        private void loadSearchString(Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.KensyuuIchiranDTOCls entityDTO)
        {
            LogUtility.DebugMethodStart();
            //ヘッダ
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KYOTEN_CD.ToString()))
            {
                this.header.KYOTEN_CD.Text = entityDTO.Shukka_Entry_KYOTEN_CD.ToString().PadLeft(2, '0');
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KYOTEN_NAME.ToString()))
            {
                this.header.KYOTEN_NAME_RYAKU.Text = entityDTO.Shukka_Entry_KYOTEN_NAME.ToString();
            }
            //出荷日付（開始）
            if (!entityDTO.Shukka_Entry_Denpyou_Date_Begin.IsNull)
            {
                this.form.SHUKKA_ENTRY_DENPYOU_DATE_FROM.Value = (DateTime)entityDTO.Shukka_Entry_Denpyou_Date_Begin;
            }

            if (!entityDTO.Shukka_Entry_Denpyou_Date_End.IsNull)
            {
                this.form.SHUKKA_ENTRY_DENPYOU_DATE_TO.Value = (DateTime)entityDTO.Shukka_Entry_Denpyou_Date_End;
            }
            //検収日付（開始）
            if (!entityDTO.Shukka_Entry_Kenshu_Date_Begin.IsNull)
            {
                this.form.SHUKKA_ENTRY_KENSYU_DATE_FROM.Value = (DateTime)entityDTO.Shukka_Entry_Kenshu_Date_Begin;
            }
            if (!entityDTO.Shukka_Entry_Kenshu_Date_End.IsNull)
            {
                this.form.SHUKKA_ENTRY_KENSYU_DATE_TO.Value = (DateTime)entityDTO.Shukka_Entry_Kenshu_Date_End;
            }
            //出荷品名
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Detail_Hinmei_Cd_1))
            {
                this.form.SHUKKA_DETAIL_HINMEI_CD_1.Text = entityDTO.Shukka_Detail_Hinmei_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Detail_Hinmei_Name_1))
            {
                this.form.SHUKKA_DETAIL_HINMEI_NAME_1.Text = entityDTO.Shukka_Detail_Hinmei_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Detail_Hinmei_Cd_2))
            {
                this.form.SHUKKA_DETAIL_HINMEI_CD_2.Text = entityDTO.Shukka_Detail_Hinmei_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Detail_Hinmei_Name_2))
            {
                this.form.SHUKKA_DETAIL_HINMEI_NAME_2.Text = entityDTO.Shukka_Detail_Hinmei_Name_2;
            }
            //検収品名
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KENSHUHINMEI_CD_1))
            {
                this.form.KENSHU_DETAIL_HINMEI_CD_1.Text = entityDTO.Shukka_Entry_KENSHUHINMEI_CD_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KENSHUHINMEI_NAME_1))
            {
                this.form.KENSHU_DETAIL_HINMEI_NAME_1.Text = entityDTO.Shukka_Entry_KENSHUHINMEI_NAME_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KENSHUHINMEI_CD_2))
            {
                this.form.KENSHU_DETAIL_HINMEI_CD_2.Text = entityDTO.Shukka_Entry_KENSHUHINMEI_CD_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KENSHUHINMEI_NAME_2))
            {
                this.form.KENSHU_DETAIL_HINMEI_NAME_2.Text = entityDTO.Shukka_Entry_KENSHUHINMEI_NAME_2;
            }
            //検収要否
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_KENSHU_UMU))
            {
                if (entityDTO.Shukka_Entry_KENSHU_UMU == "1")
                {
                    this.form.txt_Kenshuyohi.Text = "無";
                }
                else if (entityDTO.Shukka_Entry_KENSHU_UMU == "2")
                {
                    this.form.txt_Kenshuyohi.Text = "有";
                }
                else if (entityDTO.Shukka_Entry_KENSHU_UMU == "3")
                {
                    this.form.txt_Kenshuyohi.Text = "全て";
                }

            }
            //検収有無
            switch (entityDTO.Shukka_Entry_KENSHU_JYOUKYOU)
            {
                case 1:
                    this.form.txt_Kenshuumu.Text = "未検収";
                    break;

                case 2:
                    this.form.txt_Kenshuumu.Text = "検収済";
                    break;
            }
            //取引先
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Torihikisaki_Cd_1))
            {
                this.form.SHUKKA_ENTRY_TORIHIKISAKI_CD_1.Text = entityDTO.Shukka_Entry_Torihikisaki_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Torihikisaki_Name_1))
            {
                this.form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_1.Text = entityDTO.Shukka_Entry_Torihikisaki_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Torihikisaki_Cd_2))
            {
                this.form.SHUKKA_ENTRY_TORIHIKISAKI_CD_2.Text = entityDTO.Shukka_Entry_Torihikisaki_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Torihikisaki_Name_2))
            {
                this.form.SHUKKA_ENTRY_TORIHIKISAKI_NAME_2.Text = entityDTO.Shukka_Entry_Torihikisaki_Name_2;
            }
            //業者
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Gyousha_Cd_1))
            {
                this.form.SHUKKA_ENTRY_GYOUSHA_CD_1.Text = entityDTO.Shukka_Entry_Gyousha_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Gyousha_Name_1))
            {
                this.form.SHUKKA_ENTRY_GYOUSHA_NAME_1.Text = entityDTO.Shukka_Entry_Gyousha_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Gyousha_Cd_2))
            {
                this.form.SHUKKA_ENTRY_GYOUSHA_CD_2.Text = entityDTO.Shukka_Entry_Gyousha_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Gyousha_Name_2))
            {
                this.form.SHUKKA_ENTRY_GYOUSHA_NAME_2.Text = entityDTO.Shukka_Entry_Gyousha_Name_2;
            }
            //現場
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Genba_Cd_1))
            {
                this.form.SHUKKA_ENTRY_GENBA_CD_1.Text = entityDTO.Shukka_Entry_Genba_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Genba_Name_1))
            {
                this.form.SHUKKA_ENTRY_GENBA_NAME_1.Text = entityDTO.Shukka_Entry_Genba_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Genba_Cd_2))
            {
                this.form.SHUKKA_ENTRY_GENBA_CD_2.Text = entityDTO.Shukka_Entry_Genba_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Genba_Name_2))
            {
                this.form.SHUKKA_ENTRY_GENBA_NAME_2.Text = entityDTO.Shukka_Entry_Genba_Name_2;
            }
            //荷積業者
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Gyousha_Cd_1))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_1.Text = entityDTO.Shukka_Entry_Nizumi_Gyousha_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Gyousha_Name_1))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_1.Text = entityDTO.Shukka_Entry_Nizumi_Gyousha_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Gyousha_Cd_2))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_2.Text = entityDTO.Shukka_Entry_Nizumi_Gyousha_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Gyousha_Name_2))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_2.Text = entityDTO.Shukka_Entry_Nizumi_Gyousha_Name_2;
            }
            //荷積現場
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Genba_Cd_1))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_1.Text = entityDTO.Shukka_Entry_Nizumi_Genba_Cd_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Genba_Name_1))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_1.Text = entityDTO.Shukka_Entry_Nizumi_Genba_Name_1;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Genba_Cd_2))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GENBA_CD_2.Text = entityDTO.Shukka_Entry_Nizumi_Genba_Cd_2;
            }
            if (!string.IsNullOrEmpty(entityDTO.Shukka_Entry_Nizumi_Genba_Name_2))
            {
                this.form.SHUKKA_ENTRY_NIZUMI_GENBA_NAME_2.Text = entityDTO.Shukka_Entry_Nizumi_Genba_Name_2;
            }
            LogUtility.DebugMethodEnd();
        }


        private void loadDetailDetail(DataTable dataGcMulti)
        {
            decimal SHUKKA_DETAIL_NET_JYUURYOU = 0;
            decimal KENSHU_DETAIL_KENSHU_NET = 0;
            decimal KENSHU_DETAIL_BUBIKI = 0;
            decimal KENSHU_DETAIL_KINGAKU = 0;

            int count = dataGcMulti.Rows.Count;
            string previousLineSystemId = string.Empty;
            string previousLineDetailSystemId = string.Empty;
            string previousSeq = string.Empty;
            decimal KENSHU_URIAGE_KINGAKU_TOTAL = 0;
            decimal KENSHU_SHIHARAI_KINGAKU_TOTAL = 0;

            //読込データ件数を取得
            this.header.ctxt_ReadDataNumber.Text = count.ToString();

            for (int i = 0; i < dataGcMulti.Rows.Count; i++)
            {
                //
                string sSHUKKA_NUMBER = "";
                string sDENPYOU_DATE = "";
                string sKENSHU_DATE = "";
                string sGYOUSHA_CD = "";
                string sGYOUSHA_NAME = "";
                string sSHUKKA_DETAIL_HINMEI_CD = "";
                string sSHUKKA_DETAIL_HINMEI_NAME = "";
                string sNET_JYUURYOU = "";
                string sSHUKKA_DETAIL_SUURYOU = "";
                string sBUBIKI = "";
                string sSHUKKA_DETAIL_UNIT_NAME_RYAKU = "";
                string sRowNo = "";
                string sTORIHIKISAKI_CD = "";
                string sTORIHIKISAKI_NAME = "";
                string sGENBA_CD = "";
                string sGENBA_NAME = "";
                string sKENSYU_DETAIL_HINMEI_CD = "";
                string sKENSYU_DETAIL_HINMEI_NAME = "";
                string sKENSHU_NET = "";
                string sKENSYU_DETAIL_SUURYOU = "";
                string sKENSYU_DETAIL_UNIT_NAME_RYAKU = "";
                string sKENSYU_DETAIL_TANKA = "";
                string sKENSYU_DETAIL_KINGAKU = "";
                string sSHUKKA_DENPYOU_KBN = "";
                string sKENSYU_DENPYOU_KBN = "";
                decimal sKENSHU_URIAGE_KINGAKU_TOTAL = 0;
                decimal sKENSHU_SHIHARAI_KINGAKU_TOTAL = 0;

                //
                this.form.gcCustomMultiRow1.Rows.Add();
                //No.
                if (dataGcMulti.Rows[i]["KENSHU_ROW_NO"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSHU_ROW_NO"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i]["ROWNO"].Value = dataGcMulti.Rows[i]["KENSHU_ROW_NO"].ToString();
                    sRowNo = dataGcMulti.Rows[i]["KENSHU_ROW_NO"].ToString();
                }

                //出荷番号
                if (dataGcMulti.Rows[i]["SHUKKA_NUMBER"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["SHUKKA_NUMBER"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i]["SHUKKA_ENTRY_SHUKKA_NUMBER"].Value = dataGcMulti.Rows[i]["SHUKKA_NUMBER"].ToString();
                    sSHUKKA_NUMBER = dataGcMulti.Rows[i]["SHUKKA_NUMBER"].ToString();
                }
                //出荷日付
                if (dataGcMulti.Rows[i]["DENPYOU_DATE"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["DENPYOU_DATE"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_DENPYOU_DATE"].Value = DateTime.Parse(dataGcMulti.Rows[i]["DENPYOU_DATE"].ToString()).ToString("yyyy/MM/dd");
                    sDENPYOU_DATE = DateTime.Parse(dataGcMulti.Rows[i]["DENPYOU_DATE"].ToString()).ToString("yyyy/MM/dd");
                }
                //検収日付
                if (dataGcMulti.Rows[i]["KENSHU_DATE"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSHU_DATE"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_KENSYU_DATE"].Value = DateTime.Parse(dataGcMulti.Rows[i]["KENSHU_DATE"].ToString()).ToString("yyyy/MM/dd");
                    sKENSHU_DATE = DateTime.Parse(dataGcMulti.Rows[i]["KENSHU_DATE"].ToString()).ToString("yyyy/MM/dd");
                }
                //取引先CD
                if (dataGcMulti.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_TORIHIKISAKI_CD"].Value = dataGcMulti.Rows[i]["TORIHIKISAKI_CD"].ToString();
                    sTORIHIKISAKI_CD = dataGcMulti.Rows[i]["TORIHIKISAKI_CD"].ToString();
                }
                //取引先
                if (dataGcMulti.Rows[i]["TORIHIKISAKI_NAME"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["TORIHIKISAKI_NAME"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_TORIHIKISAKI_NAME"].Value = dataGcMulti.Rows[i]["TORIHIKISAKI_NAME"].ToString();
                    sTORIHIKISAKI_NAME = dataGcMulti.Rows[i]["TORIHIKISAKI_NAME"].ToString();
                }
                //業者CD
                if (dataGcMulti.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["GYOUSHA_CD"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_GYOUSHA_CD"].Value = dataGcMulti.Rows[i]["GYOUSHA_CD"].ToString();
                    sGYOUSHA_CD = dataGcMulti.Rows[i]["GYOUSHA_CD"].ToString();
                }
                //業者
                if (dataGcMulti.Rows[i]["GYOUSHA_NAME"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["GYOUSHA_NAME"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_GYOUSHA_NAME"].Value = dataGcMulti.Rows[i]["GYOUSHA_NAME"].ToString();
                    sGYOUSHA_NAME = dataGcMulti.Rows[i]["GYOUSHA_NAME"].ToString();
                }
                //現場CD
                if (dataGcMulti.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["GENBA_CD"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_GENBA_CD"].Value = dataGcMulti.Rows[i]["GENBA_CD"].ToString();
                    sGENBA_CD = dataGcMulti.Rows[i]["GENBA_CD"].ToString();
                }
                //現場
                if (dataGcMulti.Rows[i]["GENBA_NAME"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["GENBA_NAME"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_ENTRY_GENBA_NAME"].Value = dataGcMulti.Rows[i]["GENBA_NAME"].ToString();
                    sGENBA_NAME = dataGcMulti.Rows[i]["GENBA_NAME"].ToString();
                }
                //出荷時品名CD
                if (dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_CD"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_CD"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_DETAIL_HINMEI_CD"].Value = dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_CD"].ToString();
                    sSHUKKA_DETAIL_HINMEI_CD = dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_CD"].ToString();
                }
                //出荷時品名
                if (dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_NAME"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_NAME"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_DETAIL_HINMEI_NAME"].Value = dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_NAME"].ToString();
                    sSHUKKA_DETAIL_HINMEI_NAME = dataGcMulti.Rows[i]["SHUKKA_DETAIL_HINMEI_NAME"].ToString();
                }

                // 出荷時伝票区分
                if (dataGcMulti.Rows[i]["DETAIL_DENPYOU_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_DETAIL_DENPYOU_KBN"].Value = dataGcMulti.Rows[i]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString();
                    sSHUKKA_DENPYOU_KBN = dataGcMulti.Rows[i]["DETAIL_DENPYOU_KBN_NAME_RYAKU"].ToString();
                }

                //検収時品名CD 
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_CD"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_CD"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_HINMEI_CD"].Value = dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_CD"].ToString();
                    sKENSYU_DETAIL_HINMEI_CD = dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_CD"].ToString();
                }
                //検収時品名
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_NAME"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_NAME"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_HINMEI_NAME"].Value = dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_NAME"].ToString();
                    sKENSYU_DETAIL_HINMEI_NAME = dataGcMulti.Rows[i]["KENSYU_DETAIL_HINMEI_NAME"].ToString();
                }

                // 検収時伝票区分
                if (dataGcMulti.Rows[i]["KENSYU_DENPYOU_KBN_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DENPYOU_KBN_NAME_RYAKU"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_DENPYOU_KBN"].Value = dataGcMulti.Rows[i]["KENSYU_DENPYOU_KBN_NAME_RYAKU"].ToString();
                    sKENSYU_DENPYOU_KBN = dataGcMulti.Rows[i]["KENSYU_DENPYOU_KBN_NAME_RYAKU"].ToString();
                }

                //出荷時正味
                if (
                    !(previousLineSystemId.Equals(dataGcMulti.Rows[i]["SYSTEM_ID"].ToString())
                    && previousLineDetailSystemId.Equals(dataGcMulti.Rows[i]["DETAIL_SYSTEM_ID"].ToString())
                    && previousSeq.Equals(dataGcMulti.Rows[i]["SEQ"].ToString()))
                    )
                {
                    if (dataGcMulti.Rows[i]["NET_JYUURYOU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["NET_JYUURYOU"].ToString()))
                    {
                        this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_DETAIL_NET_JYUURYOU"].Value = dataGcMulti.Rows[i]["NET_JYUURYOU"].ToString();
                        // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。START
                        if (sSHUKKA_DENPYOU_KBN.Equals("売上"))
                        {
                            SHUKKA_DETAIL_NET_JYUURYOU = SHUKKA_DETAIL_NET_JYUURYOU + decimal.Parse(dataGcMulti.Rows[i]["NET_JYUURYOU"].ToString());
                        }
                        else
                        {
                            SHUKKA_DETAIL_NET_JYUURYOU = SHUKKA_DETAIL_NET_JYUURYOU - decimal.Parse(dataGcMulti.Rows[i]["NET_JYUURYOU"].ToString());
                        }
                        // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。END
                        sNET_JYUURYOU = dataGcMulti.Rows[i]["NET_JYUURYOU"].ToString();
                    }
                }
                //検収時正味 検収時正味
                if (dataGcMulti.Rows[i]["KENSHU_NET"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSHU_NET"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_KENSHU_NET"].Value = dataGcMulti.Rows[i]["KENSHU_NET"].ToString();
                    // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。START
                    if (sKENSYU_DENPYOU_KBN.Equals("売上"))
                    {
                        KENSHU_DETAIL_KENSHU_NET = KENSHU_DETAIL_KENSHU_NET + decimal.Parse(dataGcMulti.Rows[i]["KENSHU_NET"].ToString());
                    }
                    else
                    {
                        KENSHU_DETAIL_KENSHU_NET = KENSHU_DETAIL_KENSHU_NET - decimal.Parse(dataGcMulti.Rows[i]["KENSHU_NET"].ToString());
                    }
                    // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。END
                    sKENSHU_NET = dataGcMulti.Rows[i]["KENSHU_NET"].ToString();
                }
                //出荷時数量
                if (
                    !(previousLineSystemId.Equals(dataGcMulti.Rows[i]["SYSTEM_ID"].ToString())
                    && previousLineDetailSystemId.Equals(dataGcMulti.Rows[i]["DETAIL_SYSTEM_ID"].ToString())
                    && previousSeq.Equals(dataGcMulti.Rows[i]["SEQ"].ToString()))
                    )
                {
                    if (dataGcMulti.Rows[i]["SHUKKA_DETAIL_SUURYOU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["SHUKKA_DETAIL_SUURYOU"].ToString()))
                    {
                        this.form.gcCustomMultiRow1.Rows[i].Cells["SHUKKA_DETAIL_SUURYOU"].Value = dataGcMulti.Rows[i]["SHUKKA_DETAIL_SUURYOU"].ToString();
                        sSHUKKA_DETAIL_SUURYOU = dataGcMulti.Rows[i]["SHUKKA_DETAIL_SUURYOU"].ToString();
                    }
                }
                //検収時数量
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_SUURYOU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_SUURYOU"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_SUURYOU"].Value = dataGcMulti.Rows[i]["KENSYU_DETAIL_SUURYOU"].ToString();
                    sKENSYU_DETAIL_SUURYOU = dataGcMulti.Rows[i]["KENSYU_DETAIL_SUURYOU"].ToString();
                }
                //歩引き
                if (dataGcMulti.Rows[i]["BUBIKI"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["BUBIKI"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_BUBIKI"].Value = CommonCalc.DecimalFormat((decimal)dataGcMulti.Rows[i]["BUBIKI"]);
                    KENSHU_DETAIL_BUBIKI = KENSHU_DETAIL_BUBIKI + decimal.Parse(dataGcMulti.Rows[i]["BUBIKI"].ToString());
                    sBUBIKI = CommonCalc.DecimalFormat((decimal)dataGcMulti.Rows[i]["BUBIKI"]);
                }
                //出荷単位
                if (
                    !(previousLineSystemId.Equals(dataGcMulti.Rows[i]["SYSTEM_ID"].ToString())
                    && previousLineDetailSystemId.Equals(dataGcMulti.Rows[i]["DETAIL_SYSTEM_ID"].ToString())
                    && previousSeq.Equals(dataGcMulti.Rows[i]["SEQ"].ToString()))
                    )
                {
                    if (dataGcMulti.Rows[i]["SHUKKA_DETAIL_UNIT_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["SHUKKA_DETAIL_UNIT_NAME_RYAKU"].ToString()))
                    {
                        this.form.gcCustomMultiRow1.Rows[i].Cells["M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU"].Value = dataGcMulti.Rows[i]["SHUKKA_DETAIL_UNIT_NAME_RYAKU"].ToString();
                        sSHUKKA_DETAIL_UNIT_NAME_RYAKU = dataGcMulti.Rows[i]["SHUKKA_DETAIL_UNIT_NAME_RYAKU"].ToString();
                    }
                }
                //検収単位
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_UNIT_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_UNIT_NAME_RYAKU"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["M_UNIT_KENSHU_DETAIL_UNIT_NAME_RYAKU"].Value = dataGcMulti.Rows[i]["KENSYU_DETAIL_UNIT_NAME_RYAKU"].ToString();
                    sKENSYU_DETAIL_UNIT_NAME_RYAKU = dataGcMulti.Rows[i]["KENSYU_DETAIL_UNIT_NAME_RYAKU"].ToString();
                }
                //検収時単価
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_TANKA"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_TANKA"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_TANKA"].Value = Convert.ToDecimal(dataGcMulti.Rows[i]["KENSYU_DETAIL_TANKA"].ToString());
                    sKENSYU_DETAIL_TANKA = this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_TANKA"].Value.ToString();
                }
                //検収時金額
                if (dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"] != null && !string.IsNullOrEmpty(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString()))
                {
                    this.form.gcCustomMultiRow1.Rows[i].Cells["KENSHU_DETAIL_KINGAKU"].Value = CommonCalc.DecimalFormat((decimal)dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"]);
                    // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。START
                    if (sKENSYU_DENPYOU_KBN.Equals("売上"))
                    {
                        KENSHU_DETAIL_KINGAKU = KENSHU_DETAIL_KINGAKU + decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                    }
                    else
                    {
                        KENSHU_DETAIL_KINGAKU = KENSHU_DETAIL_KINGAKU - decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                    }
                    // 2017/06/08 DIQ 標準修正 #100079 差引金額が表示されるようにする。END
                    sKENSYU_DETAIL_KINGAKU = CommonCalc.DecimalFormat((decimal)dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"]);

                    // 合計金額集計用
                    if (CommonConst.DENPYOU_KBN_URIAGE.ToString().Equals(dataGcMulti.Rows[i]["KENSHU_DENPYOU_KBN_CD"].ToString()))
                    {
                        sKENSHU_URIAGE_KINGAKU_TOTAL = decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                        KENSHU_URIAGE_KINGAKU_TOTAL += decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                    }
                    else if (CommonConst.DENPYOU_KBN_SHIHARAI.ToString().Equals(dataGcMulti.Rows[i]["KENSHU_DENPYOU_KBN_CD"].ToString()))
                    {
                        sKENSHU_SHIHARAI_KINGAKU_TOTAL = decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                        KENSHU_SHIHARAI_KINGAKU_TOTAL += decimal.Parse(dataGcMulti.Rows[i]["KENSYU_DETAIL_KINGAKU"].ToString());
                    }
                }
                object[] obj = { sSHUKKA_NUMBER,sRowNo, sDENPYOU_DATE,sKENSHU_DATE, sTORIHIKISAKI_CD,sTORIHIKISAKI_NAME,sGYOUSHA_CD, sGYOUSHA_NAME,
                                   sGENBA_CD,sGENBA_NAME,sSHUKKA_DETAIL_HINMEI_CD, sSHUKKA_DETAIL_HINMEI_NAME, sSHUKKA_DENPYOU_KBN,
                                   sKENSYU_DETAIL_HINMEI_CD,sKENSYU_DETAIL_HINMEI_NAME, sKENSYU_DENPYOU_KBN,
                                   sNET_JYUURYOU,sKENSHU_NET,sSHUKKA_DETAIL_SUURYOU, sSHUKKA_DETAIL_UNIT_NAME_RYAKU,
                                   sKENSYU_DETAIL_SUURYOU, sKENSYU_DETAIL_UNIT_NAME_RYAKU,sBUBIKI,sKENSYU_DETAIL_TANKA, sKENSYU_DETAIL_KINGAKU 
                               };

                this.form.grdGetMultiRow.Rows.Add(obj);

                // 空制御用
                previousLineSystemId = dataGcMulti.Rows[i]["SYSTEM_ID"].ToString();
                previousLineDetailSystemId = dataGcMulti.Rows[i]["DETAIL_SYSTEM_ID"].ToString();
                previousSeq = dataGcMulti.Rows[i]["SEQ"].ToString();

            }
            setControl();

            #region Add row Result
            this.form.gcCustomMultiRow1.Rows.Add();
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["SHUKKA_DETAIL_NET_JYUURYOU"].Value = CommonCalc.DecimalFormat((decimal)SHUKKA_DETAIL_NET_JYUURYOU);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["SHUKKA_DETAIL_SUURYOU"].Value = string.Empty;
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["KENSHU_DETAIL_BUBIKI"].Value = CommonCalc.DecimalFormat((decimal)KENSHU_DETAIL_BUBIKI);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU"].Value = string.Empty;

            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["KENSHU_DETAIL_KENSHU_NET"].Value = CommonCalc.DecimalFormat((decimal)KENSHU_DETAIL_KENSHU_NET);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["KENSHU_DETAIL_SUURYOU"].Value = string.Empty;
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["M_UNIT_KENSHU_DETAIL_UNIT_NAME_RYAKU"].Value = string.Empty;
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["KENSHU_DETAIL_TANKA"].Value = KENSHU_DETAIL_TANKA.ToString(this.mSysInfo.SYS_TANKA_FORMAT);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["KENSHU_DETAIL_KINGAKU"].Value = CommonCalc.DecimalFormat((decimal)KENSHU_DETAIL_KINGAKU);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"].Style.BackColor = System.Drawing.Color.FromArgb(0, 105, 51);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"].Style.SelectionBackColor = Color.FromArgb(0, 105, 51);
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"].Style.ForeColor = Color.White;
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"].Style.SelectionForeColor = Color.White;
            GcCustomTextBoxCell cellKikan = (GcCustomTextBoxCell)this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"];
            // 自動背景色変更モード
            cellKikan.AutoChangeBackColorEnabled = false;
            this.form.gcCustomMultiRow1.Rows[this.form.gcCustomMultiRow1.Rows.Count - 1].Cells["Sogoke"].Value = "総合計";

            // 合計値設定
            this.form.SHUKKA_NET_JYUURYOU_TOTAL.Text = SHUKKA_DETAIL_NET_JYUURYOU.ToString();
            this.form.KENSHU_NET_JYUURYOU_TOTAL.Text = KENSHU_DETAIL_KENSHU_NET.ToString();
            this.form.KENSHU_BUBIKI_TOTAL.Text = KENSHU_DETAIL_BUBIKI.ToString();
            this.form.KENSHU_URIAGE_KINGAKU_TOTAL.Text = KENSHU_URIAGE_KINGAKU_TOTAL.ToString();
            this.form.KENSHU_SHIHARAI_KINGAKU_TOTAL.Text = KENSHU_SHIHARAI_KINGAKU_TOTAL.ToString();

            #endregion
        }
        #endregion



        #region [F12]閉じるボタンイベント

        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = this.parentForm.sysDate;
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 受渡項目生成
        /// </summary>
        /// <param name="type">画面種別</param>
        /// <returns name="ConditionInfo">範囲条件情報</returns>
        private KenshuIchiranJokenShiteiPopup.Const.KenshuIshiranConstans.ConditionInfo CreateParams()
        {
            KenshuIchiranJokenShiteiPopup.Const.KenshuIshiranConstans.ConditionInfo info;

            if (this.param.DataSetFlag == false)
            {
                // 初期設定条件を設定
                info.DataSetFlag = false;					                    // 値格納フラグ
                info.KyotenCD = "99";                                           // 拠点
                info.DStartDay = Convert.ToString(this.parentForm.sysDate);	    // 伝票日付開始日付
                info.DEndDay = Convert.ToString(this.parentForm.sysDate);		// 伝票日付終了日付
                info.KStartDay = String.Empty;	                                // 検収伝票日付開始日付
                info.KEndDay = String.Empty;		                            // 検収伝票日付終了日付
                info.StartTorihikisakiCD = String.Empty;		                // 開始取引先CD
                info.EndTorihikisakiCD = String.Empty;		                    // 終了取引先CD
                info.StartGyoushaCD = String.Empty;		                        // 開始業者CD
                info.EndGyoushaCD = String.Empty;		                        // 終了業者CD
                info.StartGenbaCD = String.Empty;		                        // 開始現場CD
                info.EndGenbaCD = String.Empty;		                            // 終了現場CD
                info.StartNGyoushaCD = String.Empty;		                    // 開始荷積業者CD
                info.EndNGyoushaCD = String.Empty;		                        // 終了荷積業者CD
                info.StartNGenbaCD = String.Empty;		                        // 開始荷積現場CD
                info.EndNGenbaCD = String.Empty;		                        // 終了荷積現場CD
                info.StartSHinmokuCD = String.Empty;		                    // 開始出荷品目CD
                info.EndSHinmokuCD = String.Empty;		                        // 終了出荷品目CD
                info.StartKHinmokuCD = String.Empty;		                    // 開始検収品目CD
                info.EndKHinmokuCD = String.Empty;		                        // 終了検収品目CD
                info.KenshuJoKBN = "1";	                                        // 検収状況
                info.KenshuUmKBN = "1";		                                    // 検収有無
            }
            else
            {
                // 前回値のまま
                info = this.param;
            }

            return info;
        }


    }
}
