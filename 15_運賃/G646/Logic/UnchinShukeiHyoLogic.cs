using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表ロジッククラス
    /// </summary>
    internal class UnchinShukeiHyoLogic : IBuisinessLogic
    {
        /// <summary>
        /// 運賃集計表画面クラス
        /// </summary>
        private UnchinShukeiHyoUIForm form;

        /// <summary>
        /// 集計データを取得・設定します
        /// </summary>
        internal List<UnchinData> ShukeiDataList { get; private set; }

        /// <summary>
        /// 集計表帳票出力用DTOリストを取得・設定します
        /// </summary>
        internal List<UnchinShukeiHyoReportDto> ShukeiHyoReportDtoList { get; private set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        IM_GENBADao genbaDao;

        /// <summary>
        /// 搬入先休動マスタのDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workClosedHannyuusakiDao;

        private Format format;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">運賃集計表画面クラス</param>
        public UnchinShukeiHyoLogic(UnchinShukeiHyoUIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.workClosedHannyuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();

            this.form = targetForm;

            this.format = Format.CreateFormat();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.HeaderInit();
                this.ButtonInit();
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                var msgBox = new MessageBoxShowLogic();
                msgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ヘッダを初期化します
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタンを初期化します
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.C_Regist(parentForm.bt_func5);
            this.form.C_Regist(parentForm.bt_func7);

            parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);
            this.form.NIOROSHI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.NIOROSHI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIOROSHI_GENBA_CD_TO_MouseDoubleClick);
            this.form.NIZUMI_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.NIZUMI_GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(NIZUMI_GENBA_CD_TO_MouseDoubleClick);
            this.form.UNPAN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(UNPAN_GYOUSHA_CD_TO_MouseDoubleClick);
            this.form.SHASHU_CD_TO.MouseDoubleClick += new MouseEventHandler(SHASHU_CD_TO_MouseDoubleClick);
            this.form.SHARYOU_CD_TO.MouseDoubleClick += new MouseEventHandler(SHARYOU_CD_TO_MouseDoubleClick);

            this.form.NIOROSHI_GENBA_CD_FROM.Validating += new System.ComponentModel.CancelEventHandler(this.form.NIOROSHI_GENBA_CD_FROM_Validated);
            this.form.NIOROSHI_GENBA_CD_TO.Validating += new System.ComponentModel.CancelEventHandler(this.form.NIOROSHI_GENBA_CD_TO_Validated);
            this.form.NIZUMI_GENBA_CD_TO.Validating += new System.ComponentModel.CancelEventHandler(this.form.NIOROSHI_GENBA_CD_FROM_Validated);
            this.form.NIZUMI_GENBA_CD_FROM.Validating += new System.ComponentModel.CancelEventHandler(this.form.NIOROSHI_GENBA_CD_FROM_Validated);

            this.EnterEventInit();

            this.form.DENPYOU_DATE.Value = parentForm.sysDate;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索します
        /// </summary>
        /// <returns>件数</returns>
        public int Search()
        {
            var ret = 0;
            var msgBox = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart();

                var dao = DaoInitUtility.GetComponent<UnchinShukeiHyoDao>();

                this.ShukeiDataList = new List<UnchinData>();
                this.ShukeiDataList.AddRange(dao.GetShukeiHyoDataUnchin(this.form.FormDataDto));

                ret = this.ShukeiDataList.Count();
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("Search", sqlEx);
                msgBox.MessageBoxShow("E093", "");
                ret = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                msgBox.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 登録します
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新します
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除します
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除します
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// 
        internal bool CSVPrint()
        {
            bool ret = true;
            var msgBox = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ShukeiDataList.Count() > 0)
                {
                    this.CreateReportDtoList();
                    this.CreateSummaryData();
                    this.CSV_CalcTotal();

                    // 自社情報マスタを取得して帳票出力用DTOにセット
                    var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                    var mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

                    this.ShukeiHyoReportDtoList.ForEach(u =>
                    {
                        u.CORP_INFO = mCorpInfo;
                        u.Format = this.format;
                    });

                    this.CreateJoukenFieldData();

                    var reportLogic = new UnchinShukeiHyoReportLogic();
                    Creat_CSV(this.ConvertToDataTable(this.ShukeiHyoReportDtoList));
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUntenshaCd", sqlEx);
                msgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
                msgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        /// <summary>
        /// headによって対したCSV出力
        /// </summary>
        public void Creat_CSV(DataTable dt)
        {
            var msgBox = new MessageBoxShowLogic();
            try
            {
                DataTable csvDT = new DataTable();
                string head_dt = "";
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_1"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_1"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_1"].ToString());
                    head_dt += "CD_1";
                    head_dt += ",NAME_1";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_2"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_2"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_2"].ToString());
                    head_dt += ",CD_2";
                    head_dt += ",NAME_2";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_3"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_3"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_3"].ToString());
                    head_dt += ",CD_3";
                    head_dt += ",NAME_3";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_4"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_4"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_4"].ToString());
                    head_dt += ",CD_4";
                    head_dt += ",NAME_4";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["COLUMN_5"].ToString()))
                {
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_5"].ToString() + "CD");
                    csvDT.Columns.Add(dt.Rows[0]["COLUMN_5"].ToString());
                    head_dt += ",CD_5";
                    head_dt += ",NAME_5";
                }
                csvDT.Columns.Add("正味重量");
                csvDT.Columns.Add("数量");
                csvDT.Columns.Add("単位CD");
                csvDT.Columns.Add("単位");
                csvDT.Columns.Add("金額");
                head_dt += ",FORMAT_NET_JYUURYOU,FORMAT_SUURYOU,UNIT_CD,UNIT_NAME,FORMAT_KINGAKU";
                string[] head_array = head_dt.Split(',');
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = csvDT.NewRow();
                    for (int n = 0; n < csvDT.Columns.Count; n++)
                    {

                        row[n] = dt.Rows[i][head_array[n]];
                    }
                    csvDT.Rows.Add(row);
                }

                // 出力先指定のポップアップを表示させる。
                if (msgBox.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    // CSV出力
                    csvExport.ConvertDataTableToCsv(csvDT, true, true, "運賃集計表", this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                msgBox.MessageBoxShow("E245", "");
            }
        }

        private void CSV_CalcTotal()
        {
            decimal? allJyuuryouSum = null;
            decimal? allKingakuSum = 0m;

            var group1key = string.Empty;
            var group2key = string.Empty;
            var group3key = string.Empty;
            var group4key = string.Empty;
            decimal? group1netJyuuryouSum = null;
            decimal? group2netJyuuryouSum = null;
            decimal? group3netJyuuryouSum = null;
            decimal? group4netJyuuryouSum = null;
            decimal? group1kingakuSum = null;
            decimal? group2kingakuSum = null;
            decimal? group3kingakuSum = null;
            decimal? group4kingakuSum = null;

            foreach (var shukeiHyoReporDto in this.ShukeiHyoReportDtoList)
            {
                
                    group1netJyuuryouSum = null;
                    group1kingakuSum = null;

                    group1key = shukeiHyoReporDto.CD_1;
                   
                    group2netJyuuryouSum = null;
                    group2kingakuSum = null;

                    group2key = shukeiHyoReporDto.CD_2;
                    
                    group3netJyuuryouSum = null;
                    group3kingakuSum = null;

                    group3key = shukeiHyoReporDto.CD_3;
               
                    group4netJyuuryouSum = null;
                    group4kingakuSum = null;

                    group4key = shukeiHyoReporDto.CD_4;
               
                var rptNetJyuuryou = shukeiHyoReporDto.NET_JYUURYOU;
                if (rptNetJyuuryou.HasValue)
                {
                    group1netJyuuryouSum = (group1netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group2netJyuuryouSum = (group2netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group3netJyuuryouSum = (group3netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group4netJyuuryouSum = (group4netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    allJyuuryouSum = (allJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                }

                var rptKingaku = shukeiHyoReporDto.KINGAKU;
                if (rptKingaku.HasValue)
                {
                    group1kingakuSum = (group1kingakuSum ?? decimal.Zero) + rptKingaku;
                    group2kingakuSum = (group2kingakuSum ?? decimal.Zero) + rptKingaku;
                    group3kingakuSum = (group3kingakuSum ?? decimal.Zero) + rptKingaku;
                    group4kingakuSum = (group4kingakuSum ?? decimal.Zero) + rptKingaku;
                    allKingakuSum = (allKingakuSum ?? decimal.Zero) + rptKingaku;
                }

                shukeiHyoReporDto.GROUP1_KEY = group1key;
                shukeiHyoReporDto.GROUP1_NET_JYUURYOU_SUM = group1netJyuuryouSum;
                shukeiHyoReporDto.GROUP1_KINGAKU_SUM = group1kingakuSum;
                shukeiHyoReporDto.GROUP2_KEY = group2key;
                shukeiHyoReporDto.GROUP2_NET_JYUURYOU_SUM = group2netJyuuryouSum;
                shukeiHyoReporDto.GROUP2_KINGAKU_SUM = group2kingakuSum;
                shukeiHyoReporDto.GROUP3_KEY = group3key;
                shukeiHyoReporDto.GROUP3_NET_JYUURYOU_SUM = group3netJyuuryouSum;
                shukeiHyoReporDto.GROUP3_KINGAKU_SUM = group3kingakuSum;
                shukeiHyoReporDto.GROUP4_KEY = group4key;
                shukeiHyoReporDto.GROUP4_NET_JYUURYOU_SUM = group4netJyuuryouSum;
                shukeiHyoReporDto.GROUP4_KINGAKU_SUM = group4kingakuSum;
                shukeiHyoReporDto.ALL_NET_JYUURYOU_SUM = allJyuuryouSum;
                shukeiHyoReporDto.ALL_KINGAKU_SUM = allKingakuSum;
            }
        }
        #endregion

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <returns></returns>
        internal bool CreateForm()
        {
            bool ret = true;
            var msgBox = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ShukeiDataList.Count() > 0)
                {
                    this.CreateReportDtoList();
                    this.CreateSummaryData();
                    this.CalcTotal();

                    // 自社情報マスタを取得して帳票出力用DTOにセット
                    var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                    var mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

                    this.ShukeiHyoReportDtoList.ForEach(u =>
                    {
                        u.CORP_INFO = mCorpInfo;
                        u.Format = this.format;
                    });

                    this.CreateJoukenFieldData();

                    var reportLogic = new UnchinShukeiHyoReportLogic();
                    reportLogic.CreateReport(this.ConvertToDataTable(this.ShukeiHyoReportDtoList), this.form.FormDataDto);
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUntenshaCd", sqlEx);
                msgBox.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
                msgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 売上データから帳票出力用DTOリストを作成します
        /// </summary>
        private void CreateReportDtoList()
        {
            LogUtility.DebugMethodStart();

            var column1 = string.Empty;
            var cd1 = string.Empty;
            var name1 = string.Empty;
            var groupName1 = string.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(1) != null)
            {
                column1 = this.form.FormDataDto.Pattern.GetColumnSelect(1).KOUMOKU_RONRI_NAME;
                cd1 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(1).BUTSURI_NAME;
                name1 = this.GetColumnName(cd1);
                groupName1 = column1 + "合計";
            }
            var column2 = string.Empty;
            var cd2 = string.Empty;
            var name2 = string.Empty;
            var groupName2 = string.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(2) != null)
            {
                column2 = this.form.FormDataDto.Pattern.GetColumnSelect(2).KOUMOKU_RONRI_NAME;
                cd2 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(2).BUTSURI_NAME;
                name2 = this.GetColumnName(cd2);
                groupName2 = column2 + "合計";
            }
            var column3 = string.Empty;
            var cd3 = string.Empty;
            var name3 = string.Empty;
            var groupName3 = string.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(3) != null)
            {
                column3 = this.form.FormDataDto.Pattern.GetColumnSelect(3).KOUMOKU_RONRI_NAME;
                cd3 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(3).BUTSURI_NAME;
                name3 = this.GetColumnName(cd3);
                groupName3 = column3 + "合計";
            }
            var column4 = string.Empty;
            var cd4 = string.Empty;
            var name4 = string.Empty;
            var groupName4 = string.Empty;
            if (this.form.FormDataDto.Pattern.GetColumnSelect(4) != null)
            {
                column4 = this.form.FormDataDto.Pattern.GetColumnSelect(4).KOUMOKU_RONRI_NAME;
                cd4 = this.form.FormDataDto.Pattern.GetColumnSelectDetail(4).BUTSURI_NAME;
                name4 = this.GetColumnName(cd4);
                groupName4 = column4 + "合計";
            }
            var column5 = string.Empty;
            var cd5 = string.Empty;
            var name5 = string.Empty;
            if (this.form.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() == null)
            {
                column5 = "運賃品名";
                cd5 = "HINMEI_CD";
                name5 = "HINMEI_NAME";
            }

            this.ShukeiHyoReportDtoList = this.ShukeiDataList.Select(u => new UnchinShukeiHyoReportDto()
            {
                COLUMN_1 = column1,
                COLUMN_2 = column2,
                COLUMN_3 = column3,
                COLUMN_4 = column4,
                COLUMN_5 = column5,
                CD_1 = this.ConvertToString(this.GetValue(u, cd1)),
                CD_2 = this.ConvertToString(this.GetValue(u, cd2)),
                CD_3 = this.ConvertToString(this.GetValue(u, cd3)),
                CD_4 = this.ConvertToString(this.GetValue(u, cd4)),
                CD_5 = this.ConvertToString(this.GetValue(u, cd5)),
                NAME_1 = this.ConvertToString(this.GetValue(u, name1)),
                NAME_2 = this.ConvertToString(this.GetValue(u, name2)),
                NAME_3 = this.ConvertToString(this.GetValue(u, name3)),
                NAME_4 = this.ConvertToString(this.GetValue(u, name4)),
                NAME_5 = this.ConvertToString(this.GetValue(u, name5)),
                UNIT_CD = this.ConvertToString(this.GetValue(u, "UNIT_CD")),
                UNIT_NAME = this.ConvertToString(this.GetValue(u, "UNIT_NAME")),
                NET_JYUURYOU = this.ConvertToNullableDecimal(this.GetValue(u, "NET_JYUURYOU")),
                SUURYOU = this.ConvertToNullableDecimal(this.GetValue(u, "SUURYOU")),
                KINGAKU = this.ConvertToNullableDecimal(this.GetValue(u, "KINGAKU")),
                GROUP1_KEY = string.Empty,
                GROUP1_NAME = groupName1,
                GROUP1_NET_JYUURYOU_SUM = null,
                GROUP1_KINGAKU_SUM = null,
                GROUP2_KEY = string.Empty,
                GROUP2_NAME = groupName2,
                GROUP2_NET_JYUURYOU_SUM = null,
                GROUP2_KINGAKU_SUM = null,
                GROUP3_KEY = string.Empty,
                GROUP3_NAME = groupName3,
                GROUP3_NET_JYUURYOU_SUM = null,
                GROUP3_KINGAKU_SUM = null,
                GROUP4_KEY = string.Empty,
                GROUP4_NAME = groupName4,
                GROUP4_NET_JYUURYOU_SUM = null,
                GROUP4_KINGAKU_SUM = null,
            }).ToList();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CDのカラム名から名称のカラム名を取得します（台貫の場合はカラム名にCDがついていないので別で処理）
        /// </summary>
        /// <param name="cdColumnName">CDカラム名</param>
        /// <returns>名称カラム名</returns>
        private string GetColumnName(string cdColumnName)
        {
            LogUtility.DebugMethodStart(cdColumnName);

            var ret = cdColumnName.Replace("_CD", "_NAME");

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 帳票出力用DTOリストを集計します
        /// </summary>
        private void CreateSummaryData()
        {
            LogUtility.DebugMethodStart();

            // 集計処理
            var shukeiHyoReportDtoListTmp = new List<UnchinShukeiHyoReportDto>();
            this.ShukeiHyoReportDtoList = this.ShukeiHyoReportDtoList.GroupBy(u => new { u.CD_1, u.CD_2, u.CD_3, u.CD_4, u.CD_5, u.UNIT_CD })
                                                                     .Select(u => new UnchinShukeiHyoReportDto()
                                                                     {
                                                                         COLUMN_1 = u.FirstOrDefault().COLUMN_1,
                                                                         COLUMN_2 = u.FirstOrDefault().COLUMN_2,
                                                                         COLUMN_3 = u.FirstOrDefault().COLUMN_3,
                                                                         COLUMN_4 = u.FirstOrDefault().COLUMN_4,
                                                                         COLUMN_5 = u.FirstOrDefault().COLUMN_5,
                                                                         CD_1 = u.FirstOrDefault().CD_1,
                                                                         CD_2 = u.FirstOrDefault().CD_2,
                                                                         CD_3 = u.FirstOrDefault().CD_3,
                                                                         CD_4 = u.FirstOrDefault().CD_4,
                                                                         CD_5 = u.FirstOrDefault().CD_5,
                                                                         NAME_1 = u.FirstOrDefault().NAME_1,
                                                                         NAME_2 = u.FirstOrDefault().NAME_2,
                                                                         NAME_3 = u.FirstOrDefault().NAME_3,
                                                                         NAME_4 = u.FirstOrDefault().NAME_4,
                                                                         NAME_5 = u.FirstOrDefault().NAME_5,
                                                                         UNIT_CD = u.FirstOrDefault().UNIT_CD,
                                                                         UNIT_NAME = u.FirstOrDefault().UNIT_NAME,
                                                                         NET_JYUURYOU = u.Sum(k => k.NET_JYUURYOU),
                                                                         SUURYOU = u.Sum(k => k.SUURYOU),
                                                                         KINGAKU = u.Sum(k => k.KINGAKU),
                                                                         GROUP1_KEY = string.Empty,
                                                                         GROUP1_NAME = u.FirstOrDefault().GROUP1_NAME,
                                                                         GROUP1_NET_JYUURYOU_SUM = null,
                                                                         GROUP1_KINGAKU_SUM = null,
                                                                         GROUP2_KEY = string.Empty,
                                                                         GROUP2_NAME = u.FirstOrDefault().GROUP2_NAME,
                                                                         GROUP2_NET_JYUURYOU_SUM = null,
                                                                         GROUP2_KINGAKU_SUM = null,
                                                                         GROUP3_KEY = string.Empty,
                                                                         GROUP3_NAME = u.FirstOrDefault().GROUP3_NAME,
                                                                         GROUP3_NET_JYUURYOU_SUM = null,
                                                                         GROUP3_KINGAKU_SUM = null,
                                                                         GROUP4_KEY = string.Empty,
                                                                         GROUP4_NAME = u.FirstOrDefault().GROUP4_NAME,
                                                                         GROUP4_NET_JYUURYOU_SUM = null,
                                                                         GROUP4_KINGAKU_SUM = null,
                                                                     })
                                                                     .OrderBy(u => u.CD_1)
                                                                     .ThenBy(u => u.CD_2)
                                                                     .ThenBy(u => u.CD_3)
                                                                     .ThenBy(u => u.CD_4)
                                                                     .ThenBy(u => u.CD_5)
                                                                     .ThenBy(u => this.ConvertToInt32(u.UNIT_CD))
                                                                     .ToList();

            //foreach (var shukeiHyoReportDtoList in grpShukeiHyoReportDtoList)
            //{
            //    decimal? grpNetJyuuryou = null;
            //    decimal? grpSuuryou = null;
            //    decimal? grpKingaku = null;

            //    foreach (var shukeiHyoReportDto in shukeiHyoReportDtoList)
            //    {
            //        if (shukeiHyoReportDto.NET_JYUURYOU.HasValue &&
            //            (shukeiHyoReportDto.NET_JYUURYOU.Value != decimal.Zero || !format.JYUURYOU_EMPTY_ZERO))
            //        {
            //            grpNetJyuuryou = (grpNetJyuuryou ?? decimal.Zero) + shukeiHyoReportDto.NET_JYUURYOU;
            //        }
            //        if (shukeiHyoReportDto.SUURYOU.HasValue &&
            //            (shukeiHyoReportDto.SUURYOU.Value != decimal.Zero || !format.SUURYOU_EMPTY_ZERO))
            //        {
            //            grpSuuryou = (grpSuuryou ?? decimal.Zero) + shukeiHyoReportDto.SUURYOU;
            //        }
            //        if (shukeiHyoReportDto.KINGAKU.HasValue &&
            //            (shukeiHyoReportDto.KINGAKU.Value != decimal.Zero || !format.KINGAKU_EMPTY_ZERO))
            //        {
            //            grpKingaku = (grpKingaku ?? decimal.Zero) + shukeiHyoReportDto.KINGAKU;
            //        }
            //    }

            //    shukeiHyoReportDtoListTmp.Add(new UnchinShukeiHyoReportDto()
            //    {
            //        COLUMN_1 = shukeiHyoReportDtoList.FirstOrDefault().COLUMN_1,
            //        COLUMN_2 = shukeiHyoReportDtoList.FirstOrDefault().COLUMN_2,
            //        COLUMN_3 = shukeiHyoReportDtoList.FirstOrDefault().COLUMN_3,
            //        COLUMN_4 = shukeiHyoReportDtoList.FirstOrDefault().COLUMN_4,
            //        COLUMN_5 = shukeiHyoReportDtoList.FirstOrDefault().COLUMN_5,
            //        CD_1 = shukeiHyoReportDtoList.FirstOrDefault().CD_1,
            //        CD_2 = shukeiHyoReportDtoList.FirstOrDefault().CD_2,
            //        CD_3 = shukeiHyoReportDtoList.FirstOrDefault().CD_3,
            //        CD_4 = shukeiHyoReportDtoList.FirstOrDefault().CD_4,
            //        CD_5 = shukeiHyoReportDtoList.FirstOrDefault().CD_5,
            //        NAME_1 = shukeiHyoReportDtoList.FirstOrDefault().NAME_1,
            //        NAME_2 = shukeiHyoReportDtoList.FirstOrDefault().NAME_2,
            //        NAME_3 = shukeiHyoReportDtoList.FirstOrDefault().NAME_3,
            //        NAME_4 = shukeiHyoReportDtoList.FirstOrDefault().NAME_4,
            //        NAME_5 = shukeiHyoReportDtoList.FirstOrDefault().NAME_5,
            //        UNIT_CD = shukeiHyoReportDtoList.FirstOrDefault().UNIT_CD,
            //        UNIT_NAME = shukeiHyoReportDtoList.FirstOrDefault().UNIT_NAME,
            //        NET_JYUURYOU = grpNetJyuuryou,
            //        SUURYOU = grpSuuryou,
            //        KINGAKU = grpKingaku,
            //        GROUP1_KEY = string.Empty,
            //        GROUP1_NAME = shukeiHyoReportDtoList.FirstOrDefault().GROUP1_NAME,
            //        GROUP1_NET_JYUURYOU_SUM = null,
            //        GROUP1_KINGAKU_SUM = null,
            //        GROUP2_KEY = string.Empty,
            //        GROUP2_NAME = shukeiHyoReportDtoList.FirstOrDefault().GROUP2_NAME,
            //        GROUP2_NET_JYUURYOU_SUM = null,
            //        GROUP2_KINGAKU_SUM = null,
            //        GROUP3_KEY = string.Empty,
            //        GROUP3_NAME = shukeiHyoReportDtoList.FirstOrDefault().GROUP3_NAME,
            //        GROUP3_NET_JYUURYOU_SUM = null,
            //        GROUP3_KINGAKU_SUM = null,
            //        GROUP4_KEY = string.Empty,
            //        GROUP4_NAME = shukeiHyoReportDtoList.FirstOrDefault().GROUP4_NAME,
            //        GROUP4_NET_JYUURYOU_SUM = null,
            //        GROUP4_KINGAKU_SUM = null,
            //    });
            //}

            //this.ShukeiHyoReportDtoList.Clear();
            //this.ShukeiHyoReportDtoList.AddRange(shukeiHyoReportDtoListTmp);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票出力用DTOリストの小計・合計計算をします
        /// </summary>
        private void CalcTotal()
        {
            decimal? allJyuuryouSum = null;
            decimal? allKingakuSum = 0m;

            var isGroup1keyChange = false;
            var isGroup2keyChange = false;
            var isGroup3keyChange = false;
            var group1key = string.Empty;
            var group2key = string.Empty;
            var group3key = string.Empty;
            var group4key = string.Empty;
            decimal? group1netJyuuryouSum = null;
            decimal? group2netJyuuryouSum = null;
            decimal? group3netJyuuryouSum = null;
            decimal? group4netJyuuryouSum = null;
            decimal? group1kingakuSum = null;
            decimal? group2kingakuSum = null;
            decimal? group3kingakuSum = null;
            decimal? group4kingakuSum = null;

            foreach (var shukeiHyoReporDto in this.ShukeiHyoReportDtoList)
            {
                if (shukeiHyoReporDto.CD_1 != group1key)
                {
                    group1netJyuuryouSum = null;
                    group1kingakuSum = null;

                    group1key = shukeiHyoReporDto.CD_1;
                    isGroup1keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_1 = string.Empty;
                    shukeiHyoReporDto.NAME_1 = string.Empty;
                    isGroup1keyChange = false;
                }

                if (isGroup1keyChange || shukeiHyoReporDto.CD_2 != group2key)
                {
                    group2netJyuuryouSum = null;
                    group2kingakuSum = null;

                    group2key = shukeiHyoReporDto.CD_2;
                    isGroup2keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_2 = string.Empty;
                    shukeiHyoReporDto.NAME_2 = string.Empty;
                    isGroup2keyChange = false;
                }

                if (isGroup1keyChange || isGroup2keyChange || shukeiHyoReporDto.CD_3 != group3key)
                {
                    group3netJyuuryouSum = null;
                    group3kingakuSum = null;

                    group3key = shukeiHyoReporDto.CD_3;
                    isGroup3keyChange = true;
                }
                else
                {
                    shukeiHyoReporDto.CD_3 = string.Empty;
                    shukeiHyoReporDto.NAME_3 = string.Empty;
                    isGroup3keyChange = false;
                }

                if (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || shukeiHyoReporDto.CD_4 != group4key)
                {
                    group4netJyuuryouSum = null;
                    group4kingakuSum = null;

                    group4key = shukeiHyoReporDto.CD_4;
                }
                else
                {
                    shukeiHyoReporDto.CD_4 = string.Empty;
                    shukeiHyoReporDto.NAME_4 = string.Empty;
                }

                var rptNetJyuuryou = shukeiHyoReporDto.NET_JYUURYOU;
                if (rptNetJyuuryou.HasValue)
                {
                    group1netJyuuryouSum = (group1netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group2netJyuuryouSum = (group2netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group3netJyuuryouSum = (group3netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    group4netJyuuryouSum = (group4netJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                    allJyuuryouSum = (allJyuuryouSum ?? decimal.Zero) + rptNetJyuuryou;
                }

                var rptKingaku = shukeiHyoReporDto.KINGAKU;
                if (rptKingaku.HasValue)
                {
                    group1kingakuSum = (group1kingakuSum ?? decimal.Zero) + rptKingaku;
                    group2kingakuSum = (group2kingakuSum ?? decimal.Zero) + rptKingaku;
                    group3kingakuSum = (group3kingakuSum ?? decimal.Zero) + rptKingaku;
                    group4kingakuSum = (group4kingakuSum ?? decimal.Zero) + rptKingaku;
                    allKingakuSum = (allKingakuSum ?? decimal.Zero) + rptKingaku;
                }

                shukeiHyoReporDto.GROUP1_KEY = group1key;
                shukeiHyoReporDto.GROUP1_NET_JYUURYOU_SUM = group1netJyuuryouSum;
                shukeiHyoReporDto.GROUP1_KINGAKU_SUM = group1kingakuSum;
                shukeiHyoReporDto.GROUP2_KEY = group2key;
                shukeiHyoReporDto.GROUP2_NET_JYUURYOU_SUM = group2netJyuuryouSum;
                shukeiHyoReporDto.GROUP2_KINGAKU_SUM = group2kingakuSum;
                shukeiHyoReporDto.GROUP3_KEY = group3key;
                shukeiHyoReporDto.GROUP3_NET_JYUURYOU_SUM = group3netJyuuryouSum;
                shukeiHyoReporDto.GROUP3_KINGAKU_SUM = group3kingakuSum;
                shukeiHyoReporDto.GROUP4_KEY = group4key;
                shukeiHyoReporDto.GROUP4_NET_JYUURYOU_SUM = group4netJyuuryouSum;
                shukeiHyoReporDto.GROUP4_KINGAKU_SUM = group4kingakuSum;
                shukeiHyoReporDto.ALL_NET_JYUURYOU_SUM = allJyuuryouSum;
                shukeiHyoReporDto.ALL_KINGAKU_SUM = allKingakuSum;
            }
        }

        /// <summary>
        /// 帳票の条件欄を作成します
        /// </summary>
        private void CreateJoukenFieldData()
        {
            this.ShukeiHyoReportDtoList.ForEach(u =>
            {
                u.TITLE = "運賃集計表（" + this.form.FormDataDto.Pattern.PATTERN_NAME + "）";
                u.KYOTEN = this.form.FormDataDto.KyotenName;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //u.HAKKOU_DATE = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
                u.HAKKOU_DATE = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            });

            // 抽出条件文字列を作成（左側）
            var jouken1 = new StringBuilder();
            jouken1.AppendLine("[抽出条件]");
            jouken1.AppendLine("　[" + this.form.FormDataDto.DateShuruiName + "] " + this.form.FormDataDto.DateFrom.ToString("yyyy/MM/dd") + " ～ " + this.form.FormDataDto.DateTo.ToString("yyyy/MM/dd"));
            jouken1.AppendLine("　[伝票種類] " + this.form.FormDataDto.DenpyouShuruiName);
            jouken1.Append(this.form.FormDataDto.Kyoten);
            jouken1.Append(this.form.FormDataDto.KeitaiKbn);
            jouken1.Append(this.form.FormDataDto.UnpanGyousha);
            jouken1.Append(this.form.FormDataDto.NioroshiGyousha);
            jouken1.Append(this.form.FormDataDto.NioroshiGenba);
            jouken1.Append(this.form.FormDataDto.NizumiGyousha);
            jouken1.Append(this.form.FormDataDto.NizumiGenba);
            jouken1.Append(this.form.FormDataDto.Sharyou);
            jouken1.Append(this.form.FormDataDto.Shashu);
            jouken1.AppendLine(string.Empty);

            // 抽出条件文字列を作成（右側）
            var jouken2 = new StringBuilder();
            jouken2.AppendLine("[集計項目]");
            jouken2.Append("　[1] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(1) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(1).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[2] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(2) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(2).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[3] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(3) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(3).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[4] ");
            if (this.form.FormDataDto.Pattern.GetColumnSelect(4) != null)
            {
                jouken2.Append(this.form.FormDataDto.Pattern.GetColumnSelect(4).KOUMOKU_RONRI_NAME);
            }
            jouken2.AppendLine(string.Empty);
            jouken2.AppendLine(string.Empty);
            jouken2.AppendLine("[明細項目]");
            jouken2.Append("　[1] ");
            if (this.form.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() == null)
            {
                jouken2.Append("運賃品名");
            }
            else
            {
                jouken2.Append("正味重量");
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[2] ");
            if (this.form.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() == null)
            {
                jouken2.Append("正味重量");
            }
            else
            {
                jouken2.Append("数量/単位");
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[3] ");
            if (this.form.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() == null)
            {
                jouken2.Append("数量/単位");
            }
            else
            {
                jouken2.Append("金額");
            }
            jouken2.AppendLine(string.Empty);
            jouken2.Append("　[4] ");
            if (this.form.FormDataDto.Pattern.ColumnSelectDetailList.Where(s => s.BUTSURI_NAME == "HINMEI_CD").FirstOrDefault() == null)
            {
                jouken2.Append("金額");
            }
            else
            {
                jouken2.Append(string.Empty);
            }
            jouken2.AppendLine(string.Empty);

            this.ShukeiHyoReportDtoList.ForEach(u =>
            {
                u.JOUKEN_1 = jouken1.ToString();
                u.JOUKEN_2 = jouken2.ToString();
            });
        }

        /// <summary>
        /// オブジェクトを文字列に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した文字列（オブジェクトがnullの場合は空文字列）</returns>
        private string ConvertToString(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            var ret = string.Empty;
            if (obj != null)
            {
                ret = obj.ToString();
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// オブジェクトを数値に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した数値（オブジェクトがnullの場合は0）</returns>
        private int ConvertToInt32(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            var ret = 0;
            Int32.TryParse(obj.ToString(), out ret);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// オブジェクトを数値に変換します
        /// </summary>
        /// <param name="obj">対象のオブジェクト</param>
        /// <returns>変換した数値（オブジェクトがnullの場合は0）</returns>
        private decimal? ConvertToNullableDecimal(object obj)
        {
            LogUtility.DebugMethodStart(obj);

            decimal? ret = null;
            decimal tmp = 0m;
            if (decimal.TryParse(obj.ToString(), out tmp))
            {
                ret = tmp;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// オブジェクトから指定したプロパティの値を取得します
        /// </summary>
        /// <param name="targetObject">対象のオブジェクト</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>取得した値</returns>
        private object GetValue(object targetObject, string propertyName)
        {
            LogUtility.DebugMethodStart(targetObject, propertyName);

            object ret = null;
            if (!string.IsNullOrEmpty(propertyName))
            {
                ret = targetObject.GetType().GetProperty(propertyName).GetValue(targetObject, null);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// リストをDataTableに変換します
        /// </summary>
        /// <typeparam name="T">リストの型</typeparam>
        /// <param name="list">対象のリスト</param>
        /// <returns>変換したDataTable</returns>
        private DataTable ConvertToDataTable<T>(T list) where T : IList
        {
            LogUtility.DebugMethodStart(list);

            var ret = new DataTable(typeof(T).GetGenericArguments()[0].Name);
            typeof(T).GetGenericArguments()[0].GetProperties().ToList().ForEach(p => ret.Columns.Add(p.Name, p.PropertyType));
            foreach (var item in list)
            {
                var row = ret.NewRow();
                item.GetType().GetProperties().ToList().ForEach(p => row[p.Name] = p.GetValue(item, null));
                ret.Rows.Add(row);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ボタン情報を作成します
        /// </summary>
        /// <returns>ボタン情報</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, UnchinShukeiHyoConst.BUTTON_SETTING_XML);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場</returns>
        internal M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 荷積降現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>荷積降現場</returns>
        internal M_GENBA GetNizumioroshiGenba(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 区分に関係なく取得する
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力担当者を取得します
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns>入力担当者</returns>
        internal M_SHAIN GetNyuuryokuTantousha(string shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            M_SHAIN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            ret = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = shainCd, NYUURYOKU_TANTOU_KBN = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 車輌リストを取得します
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns>車輌リスト</returns>
        internal List<M_SHARYOU> GetSharyou(string shashuCd, string sharyouCd)
        {
            LogUtility.DebugMethodStart(shashuCd, sharyouCd);

            var ret = new List<M_SHARYOU>();

            var dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            var keyEntity = new M_SHARYOU();
            if (!string.IsNullOrEmpty(shashuCd))
            {
                keyEntity.SHASYU_CD = shashuCd;
            }
            keyEntity.SHARYOU_CD = sharyouCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            ret = dao.GetAllValidData(keyEntity).ToList();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.DATE_FROM;
            var ToTextBox = this.form.DATE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIOROSHI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.NIOROSHI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GYOUSHA_NAME_TO.Text = this.form.NIOROSHI_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIOROSHI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIOROSHI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIOROSHI_GENBA_CD_FROM;
            var ToTextBox = this.form.NIOROSHI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIOROSHI_GENBA_NAME_TO.Text = this.form.NIOROSHI_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIZUMI_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.NIZUMI_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GYOUSHA_NAME_TO.Text = this.form.NIZUMI_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NIZUMI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// NIZUMI_GENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.NIZUMI_GENBA_CD_FROM;
            var ToTextBox = this.form.NIZUMI_GENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.NIZUMI_GENBA_NAME_TO.Text = this.form.NIZUMI_GENBA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region NYUURYOKU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// NYUURYOKU_TANTOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void NYUURYOKU_TANTOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.form.NYUURYOKU_TANTOUSHA_CD_FROM;
        //    var ToTextBox = this.form.NYUURYOKU_TANTOUSHA_CD_TO;

        //    ToTextBox.Text = FromTextBox.Text;
        //    this.form.NYUURYOKU_TANTOUSHA_NAME_TO.Text = this.form.NYUURYOKU_TANTOUSHA_NAME_FROM.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// UNPAN_GYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.UNPAN_GYOUSHA_CD_FROM;
            var ToTextBox = this.form.UNPAN_GYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.UNPAN_GYOUSHA_NAME_TO.Text = this.form.UNPAN_GYOUSHA_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHASHU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHASHU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHASHU_CD_FROM;
            var ToTextBox = this.form.SHASHU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHASHU_NAME_TO.Text = this.form.SHASHU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region SHARYOU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// SHARYOU_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.SHARYOU_CD_FROM;
            var ToTextBox = this.form.SHARYOU_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.SHARYOU_NAME_TO.Text = this.form.SHARYOU_NAME_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region KEITAI_KBN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// KEITAI_KBN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void KEITAI_KBN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.form.KEITAI_KBN_CD_FROM;
        //    var ToTextBox = this.form.KEITAI_KBN_CD_TO;

        //    ToTextBox.Text = FromTextBox.Text;

        //    this.form.KEITAI_KBN_NAME_TO.Text = this.form.KEITAI_KBN_NAME_FROM.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region DAIKAN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// DAIKAN_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DAIKAN_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.form.DAIKAN_CD_FROM;
        //    var ToTextBox = this.form.DAIKAN_CD_TO;

        //    ToTextBox.Text = FromTextBox.Text;

        //    this.form.DAIKAN_NAME_TO.Text = this.form.DAIKAN_NAME_FROM.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SHURUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.form.SHURUI_CD_FROM;
        //    var ToTextBox = this.form.SHURUI_CD_TO;

        //    ToTextBox.Text = FromTextBox.Text;
        //    this.form.SHURUI_NAME_TO.Text = this.form.SHURUI_NAME_FROM.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region BUNRUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// SHURUI_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void BUNRUI_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.form.BUNRUI_CD_FROM;
        //    var ToTextBox = this.form.BUNRUI_CD_TO;

        //    ToTextBox.Text = FromTextBox.Text;
        //    this.form.BUNRUI_NAME_TO.Text = this.form.BUNRUI_NAME_FROM.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        #region 運搬業者
        /// <summary>
        /// 運搬業者
        /// </summary>
        /// <param name="Cd"></param>
        /// <param name="Name"></param>
        /// <param name="e"></param>
        internal void CheckUpanGyoushaCdFrom(CustomAlphaNumTextBox Cd, CustomTextBox Name, CancelEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(Cd, Name, e);

                if (string.IsNullOrEmpty(Cd.Text))
                {
                    Name.Text = string.Empty;
                    return;
                }

                var gyousha = this.GetGyousha(Cd.Text.PadLeft(6, '0'));

                if (gyousha == null || !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    Cd.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    Cd.Text = Cd.Text.PadLeft(6, '0');
                    Name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUpanGyoushaCdFrom", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUpanGyoushaCdFrom", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 業者
        /// <summary>
        /// 荷降業者CD の存在チェック
        /// </summary>
        internal void CheckNioroshiGyoushaCdFrom(CustomAlphaNumTextBox Cd, CustomTextBox Name)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(Cd, Name);

                if (string.IsNullOrEmpty(Cd.Text))
                {
                    Name.Text = string.Empty;
                    return;
                }

                var gyousha = this.GetGyousha(Cd.Text.PadLeft(6, '0'));

                if (gyousha == null
                    || (!gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue
                    && !gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue))
                {
                    Cd.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "業者");
                    Cd.Focus();
                    return;
                }
                else
                {
                    Cd.Text = Cd.Text.PadLeft(6, '0');
                    Name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNioroshiGyoushaCdFrom", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCdFrom", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 荷積業者CD の存在チェック
        /// </summary>
        internal void CheckNizumiGyoushaCdFrom(CustomAlphaNumTextBox Cd, CustomTextBox Name)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(Cd, Name);

                if (string.IsNullOrEmpty(Cd.Text))
                {
                    Name.Text = string.Empty;
                    return;
                }

                var gyousha = this.GetGyousha(Cd.Text.PadLeft(6, '0'));

                if (gyousha == null
                    || (!gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue
                    && !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                {
                    Cd.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "業者");
                    Cd.Focus();
                    return;
                }
                else
                {
                    Cd.Text = Cd.Text.PadLeft(6, '0');
                    Name.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNizumiGyoushaCdFrom", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGyoushaCdFrom", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 現場
        /// <summary>
        /// 荷降現場CDの存在チェック
        /// </summary>
        internal void CheckNioroshiGenbaCdFrom(CustomAlphaNumTextBox Cd, CustomAlphaNumTextBox GyoushaCd, CustomTextBox Name)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(Cd, GyoushaCd, Name);

                if (string.IsNullOrEmpty(Cd.Text))
                {
                    Name.Text = string.Empty;
                    return;
                }
                Cd.Text = Cd.Text.PadLeft(6, '0');

                M_GENBA genba = new M_GENBA();

                genba = this.GetGenbacheck(GyoushaCd.Text, Cd.Text);

                if (genba == null
                    || (!genba.TSUMIKAEHOKAN_KBN.IsTrue
                    && !genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                    && !genba.SAISHUU_SHOBUNJOU_KBN.IsTrue))
                {
                    Cd.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "現場");
                    Cd.Focus();
                    return;
                }
                else
                {
                    if (HannyuusakiDateCheck(Cd, GyoushaCd))
                    {
                        Name.Text = genba.GENBA_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNioroshiGenbaCdFrom", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGenbaCdFrom", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 搬入先休動チェック
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cd"></param>
        /// <param name="GyoushaCd"></param>
        /// <returns></returns>
        internal bool HannyuusakiDateCheck(CustomAlphaNumTextBox Cd, CustomAlphaNumTextBox GyoushaCd)
        {
            string inputNioroshiGyoushaCd = GyoushaCd.Text;
            string inputNioroshiGenbaCd = Cd.Text;
            string inputSagyouDate = Convert.ToString(this.form.DENPYOU_DATE.Text);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(inputSagyouDate))
            {
                return true;
            }

            M_WORK_CLOSED_HANNYUUSAKI workClosedHannyuusakiEntry = new M_WORK_CLOSED_HANNYUUSAKI();
            //荷降業者CD取得
            workClosedHannyuusakiEntry.GYOUSHA_CD = inputNioroshiGyoushaCd;
            //荷降現場CD取得
            workClosedHannyuusakiEntry.GENBA_CD = inputNioroshiGenbaCd;
            //作業日取得
            workClosedHannyuusakiEntry.CLOSED_DATE = Convert.ToDateTime(inputSagyouDate);

            M_WORK_CLOSED_HANNYUUSAKI[] workclosedhannyuusakiList = workClosedHannyuusakiDao.GetAllValidData(workClosedHannyuusakiEntry);

            //取得テータ
            if (workclosedhannyuusakiList.Count() >= 1)
            {
                Cd.BackColor = Constans.ERROR_COLOR;
                Cd.IsInputErrorOccured = true;
                msgLogic.MessageBoxShow("E206", "荷降現場", "伝票日付：" + workClosedHannyuusakiEntry.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                Cd.Focus();
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 荷積現場CDの存在チェック
        /// </summary>
        internal void CheckNizumiGenbaCdFrom(CustomAlphaNumTextBox Cd, CustomAlphaNumTextBox GyoushaCd, CustomTextBox Name)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart(Cd, GyoushaCd, Name);

                if (string.IsNullOrEmpty(Cd.Text))
                {
                    Name.Text = string.Empty;
                    return;
                }
                Cd.Text = Cd.Text.PadLeft(6, '0');

                M_GENBA genba = new M_GENBA();

                genba = this.GetGenbacheck(GyoushaCd.Text, Cd.Text);

                if (genba == null
                    || (!genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue
                    && !genba.TSUMIKAEHOKAN_KBN.IsTrue))
                {
                    Cd.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "現場");
                    Cd.Focus();
                    return;
                }
                else
                {
                    Name.Text = genba.GENBA_NAME_RYAKU;
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckNizumiGenbaCdFrom", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNizumiGenbaCdFrom", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return null;
            }

            M_GYOUSHA keyEntity = new M_GYOUSHA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

            if (gyousha == null || gyousha.Length < 1)
            {
                return null;
            }
            else
            {
                return gyousha[0];
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenbacheck(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var genba = this.genbaDao.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            // PK指定のため1件
            return genba[0];
        }

        #region 値保持

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.form.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }

        /// <summary>
        /// Enter時 入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }

        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender, string newText)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, newText); //一致する場合変更なし
        }

        #endregion

        #region 内部クラス
        /// <summary>
        /// フォーマット
        /// </summary>
        internal class Format
        {
            internal readonly string SHARP_FORMAT_DECIMAL = "#,###.###";
            internal readonly string ZERO_FORMAT_DECIMAL = "#,##0.###";
            internal readonly string SHARP_FORMAT_INTEGER = "#,###";
            internal readonly string ZERO_FORMAT_INTEGER = "#,##0";

            internal bool SUURYOU_EMPTY_ZERO { get; private set; }
            internal bool JYUURYOU_EMPTY_ZERO { get; private set; }
            internal bool KINGAKU_EMPTY_ZERO { get; private set; }
            internal string SUURYOU_FORMAT { get; private set; }
            internal string JYUURYOU_FORMAT { get; private set; }
            internal string KINGAKU_FORMAT { get; private set; }

            private Format() { }

            static internal Format CreateFormat()
            {
                Format f = new Format();

                if (SystemProperty.Format.Suuryou.Split('.')[0].EndsWith("#"))
                {
                    f.SUURYOU_EMPTY_ZERO = true;
                    f.SUURYOU_FORMAT = f.SHARP_FORMAT_DECIMAL;
                }
                else
                {
                    f.SUURYOU_EMPTY_ZERO = false;
                    f.SUURYOU_FORMAT = f.ZERO_FORMAT_DECIMAL;
                }
                if (SystemProperty.Format.Jyuryou.Split('.')[0].EndsWith("#"))
                {
                    f.JYUURYOU_EMPTY_ZERO = true;
                    f.JYUURYOU_FORMAT = f.SHARP_FORMAT_DECIMAL;
                }
                else
                {
                    f.JYUURYOU_EMPTY_ZERO = false;
                    f.JYUURYOU_FORMAT = f.ZERO_FORMAT_DECIMAL;
                }

                f.KINGAKU_EMPTY_ZERO = false;
                f.KINGAKU_FORMAT = f.ZERO_FORMAT_INTEGER;

                return f;
            }
        }
        #endregion

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}