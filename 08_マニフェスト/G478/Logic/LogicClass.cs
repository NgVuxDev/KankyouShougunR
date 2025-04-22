using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    /// <summary>
    /// マニフェスト集計表ロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// マニフェスト集計表画面クラス
        /// </summary>
        private UIForm form;

        /// <summary>
        /// マニフェスト集計表Dtoを取得・設定します
        /// </summary>
        internal ManifestShukeihyoDto ManifestShukeihyoDto { get; set; }

        /// <summary>
        /// マニフェストデータテーブルを取得・設定します
        /// </summary>
        internal DataTable ManifestDataTable { get; set; }


        private decimal kansangoSuryoSum = 0;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic MsgBox;

        // テープルのcolumn
        private string[] csvColumn;

        //csvのColumn
        private List<CsvColumnDto> ccdList;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.ButtonInit();
                this.EventInit();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理を行います
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込を行います
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.C_Regist(parentForm.bt_func7);

            parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);
            // csv出力
            this.form.C_Regist(parentForm.bt_func5);
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 抽出範囲 「一次二次区分」項目を非表示
            this.form.label8.Visible = false;
            this.form.customPanel7.Visible = false;

            // Location調整
            // 交付年月日
            LocationAdjustmentForManiLite(this.form.label16);
            LocationAdjustmentForManiLite(this.form.KOFU_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label38);
            LocationAdjustmentForManiLite(this.form.KOFU_DATE_TO);

            // 運搬終了日
            LocationAdjustmentForManiLite(this.form.label18);
            LocationAdjustmentForManiLite(this.form.UNPAN_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label39);
            LocationAdjustmentForManiLite(this.form.UNPAN_DATE_TO);

            // 処分終了日
            LocationAdjustmentForManiLite(this.form.label19);
            LocationAdjustmentForManiLite(this.form.SHOBUN_END_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label40);
            LocationAdjustmentForManiLite(this.form.SHOBUN_END_DATE_TO);

            // 最終処分終了日
            LocationAdjustmentForManiLite(this.form.label23);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_END_DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label41);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_END_DATE_TO);

            // 拠点
            LocationAdjustmentForManiLite(this.form.label5);
            LocationAdjustmentForManiLite(this.form.KYOTEN_CD);
            LocationAdjustmentForManiLite(this.form.KYOTEN_NAME);
            LocationAdjustmentForManiLite(this.form.KYOYTEN_POPUP);

            // 取引先
            LocationAdjustmentForManiLite(this.form.label24);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_CD_FROM);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label37);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_CD_TO);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_NAME_TO);
            LocationAdjustmentForManiLite(this.form.TORIHIKISAKI_TO_POPUP);

            // 排出事業者
            LocationAdjustmentForManiLite(this.form.label9);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.lblSyukeiKomoku1Kara);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GYOUSHA_TO_POPUP);

            // 排出事業場
            LocationAdjustmentForManiLite(this.form.label10);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label27);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAISHUTSU_GENBA_TO_POPUP);

            // 運搬受託者
            LocationAdjustmentForManiLite(this.form.label11);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label28);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.UNPAN_GYOUSHA_TO_POPUP);

            // 処分受託者
            LocationAdjustmentForManiLite(this.form.label12);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label29);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GYOUSHA_TO_POPUP);

            // 処分事業場
            LocationAdjustmentForManiLite(this.form.label13);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label30);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_GENBA_TO_POPUP);

            // 最終処分業者
            LocationAdjustmentForManiLite(this.form.label21);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label35);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_CD_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GYOUSHA_TO_POPUP);

            // 最終処分場所
            LocationAdjustmentForManiLite(this.form.label22);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_CD_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label36);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_CD_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_NAME_TO);
            LocationAdjustmentForManiLite(this.form.LAST_SHOBUN_GENBA_TO_POPUP);

            // 報告分類
            LocationAdjustmentForManiLite(this.form.label14);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label31);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_CD_TO);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HOUKOKUSHO_TO_POPUP);

            // 廃棄物名称
            LocationAdjustmentForManiLite(this.form.label15);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_CD_FROM);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label32);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_CD_TO);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_NAME_TO);
            LocationAdjustmentForManiLite(this.form.HAIKIBUTSU_TO_POPUP);

            // 処分方法
            LocationAdjustmentForManiLite(this.form.label17);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_CD_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_NAME_FROM);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_FROM_POPUP);
            LocationAdjustmentForManiLite(this.form.label34);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_CD_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_NAME_TO);
            LocationAdjustmentForManiLite(this.form.SHOBUN_HOUHOU_TO_POPUP);
        }

        /// <summary>
        /// マニライト用にLocation調整
        /// </summary>
        /// <param name="ctrl"></param>
        private void LocationAdjustmentForManiLite(Control ctrl)
        {
            ctrl.Location = new System.Drawing.Point(ctrl.Location.X, ctrl.Location.Y - 23);
        }

        /// <summary>
        /// 検索します
        /// </summary>
        /// <returns>件数</returns>
        public int Search()
        {
            int searchResult = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.SetManifestShukeihyoDto();

                var dao = DaoInitUtility.GetComponent<IManifestShukeihyoDao>();
                this.ManifestDataTable = dao.GetManifestData(this.ManifestShukeihyoDto);

                LogUtility.DebugMethodEnd();

                if (this.ManifestDataTable != null)
                {
                    searchResult = this.ManifestDataTable.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                searchResult = -1;
            }
            return searchResult;
        }

        /// <summary>
        /// CSV作成
        /// </summary>
        internal bool CSVPrint()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 端数桁を設定
                int hasuuKeta = 0;
                if (SystemProperty.Format.ManifestSuuryou.IndexOf(".") != -1)
                {
                    hasuuKeta = SystemProperty.Format.ManifestSuuryou.Length
                                    - SystemProperty.Format.ManifestSuuryou.IndexOf(".") - 1;
                }

                if (this.ManifestDataTable != null && 0 < this.ManifestDataTable.Rows.Count)
                {
                    var csvDT = this.CreateCsvHeaderTable();
                    DataRow rowTmp;
                    foreach (DataRow dataRow in this.ManifestDataTable.Rows)
                    {
                        rowTmp = csvDT.NewRow();
                        foreach (CsvColumnDto ccd in this.ccdList)
                        {
                            if (dataRow[ccd.tableColumnCD] != null && !string.IsNullOrEmpty(dataRow[ccd.tableColumnCD].ToString()))
                            {
                                rowTmp[ccd.csvColumnCD] = dataRow[ccd.tableColumnCD].ToString().Trim();
                            }

                            if (dataRow[ccd.tableColumnName] != null && !string.IsNullOrEmpty(dataRow[ccd.tableColumnName].ToString()))
                            {
                                rowTmp[ccd.csvColumnName] = dataRow[ccd.tableColumnName].ToString().Trim();
                            }
                        }

                        if (dataRow["KANSANGO_SURYO"] != null && !string.IsNullOrEmpty(dataRow["KANSANGO_SURYO"].ToString()))
                        {

                            decimal kansangoSuuryo = FractionLogic.FractionCalc(Convert.ToDecimal(dataRow["KANSANGO_SURYO"].ToString()),
                                                                  FractionLogic.FractionType.ROUND,
                                                                  hasuuKeta);
                            rowTmp["換算後数量（ｔ）"] = kansangoSuuryo.ToString(SystemProperty.Format.ManifestSuuryou);
                        }
                        csvDT.Rows.Add(rowTmp);
                    }

                    if (csvDT.Rows.Count == 0)
                    {
                        this.MsgBox.MessageBoxShow("E044");
                        return false;
                    }
                    // 出力先指定のポップアップを表示させる。
                    if (this.MsgBox.MessageBoxShow("C013") == DialogResult.Yes)
                    {
                        CSVExport csvExport = new CSVExport();
                        csvExport.ConvertDataTableToCsv(csvDT, true, true, "マニフェスト集計表", this.form);
                    }
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// CSVのheader作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateCsvHeaderTable()
        {
            // CD取得
            var cd1 = GetButsuriName(1);
            var cd2 = GetButsuriName(2);
            var cd3 = GetButsuriName(3);
            var cd4 = GetButsuriName(4);
            var cd5 = GetButsuriName(5);
            var cd6 = GetButsuriName(6);
            var cd7 = GetButsuriName(7);

            // NAME取得
            var name1 = GetKoumokuRonriName(1);
            var name2 = GetKoumokuRonriName(2);
            var name3 = GetKoumokuRonriName(3);
            var name4 = GetKoumokuRonriName(4);
            var name5 = GetKoumokuRonriName(5);
            var name6 = GetKoumokuRonriName(6);
            var name7 = GetKoumokuRonriName(7);

            string headStr = "";
            this.ccdList = new List<CsvColumnDto>();

            if (!string.IsNullOrEmpty(cd1))
            {
                headStr += name1 + "CD" + "," + name1;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name1 + "CD";
                ccd.csvColumnName = name1;
                ccd.tableColumnCD = cd1;
                ccd.tableColumnName = cd1 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd2))
            {
                if(!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name2 + "CD" + "," + name2;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name2 + "CD";
                ccd.csvColumnName = name2;
                ccd.tableColumnCD = cd2;
                ccd.tableColumnName = cd2 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd3))
            {
                if (!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name3 + "CD" + "," + name3;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name3 + "CD";
                ccd.csvColumnName = name3;
                ccd.tableColumnCD = cd3;
                ccd.tableColumnName = cd3 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd4))
            {
                if (!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name4 + "CD" + "," + name4;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name4 + "CD";
                ccd.csvColumnName = name4;
                ccd.tableColumnCD = cd4;
                ccd.tableColumnName = cd4 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd5))
            {
                if (!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name5 + "CD" + "," + name5;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name5 + "CD";
                ccd.csvColumnName = name5;
                ccd.tableColumnCD = cd5;
                ccd.tableColumnName = cd5 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd6))
            {
                if (!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name6 + "CD" + "," + name6;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name6 + "CD";
                ccd.csvColumnName = name6;
                ccd.tableColumnCD = cd6;
                ccd.tableColumnName = cd6 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(cd7))
            {
                if (!string.IsNullOrEmpty(headStr))
                {
                    headStr += ",";
                }

                headStr += name7 + "CD" + "," + name7;

                CsvColumnDto ccd = new CsvColumnDto();
                ccd.csvColumnCD = name7 + "CD";
                ccd.csvColumnName = name7;
                ccd.tableColumnCD = cd7;
                ccd.tableColumnName = cd7 + "_NAME";
                ccdList.Add(ccd);
            }

            if (!string.IsNullOrEmpty(headStr))
            {
                headStr += ",";
            }

            headStr += "換算後数量（ｔ）";

            string[] csvHead;
            csvHead = headStr.Split(',');

            this.csvColumn = csvHead;

            DataTable csvDT = new DataTable();
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }

            return csvDT;
        }

        /// <summary>
        /// マニフェスト集計表作成
        /// </summary>
        internal bool CreateManifestShukeihyo()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ManifestDataTable != null && 0 < this.ManifestDataTable.Rows.Count)
                {
                    var headerTable = this.CreateHeaderTable();
                    var detailTable = this.CreateDetailTable();
                    // 換算後数量合計値
                    headerTable.Rows[0]["KANSANGO_SURYO_SUM"] = kansangoSuryoSum;

                    var reportInfo = new ReportInfoR479(headerTable, detailTable, this.ManifestShukeihyoDto);
                    reportInfo.SetRecord(detailTable);
                    reportInfo.CreateR479();
                    reportInfo.Title = headerTable.Rows[0]["TITLE"].ToString();
                    reportInfo.ReportID = "R479";

                    // 印刷ポツプアップ画面表示
                    using (FormReportPrintPopup report = new FormReportPrintPopup(reportInfo))
                    {
                        report.ShowDialog();
                        report.Dispose();
                    }
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateManifestShukeihyo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

        /// <summary>
        /// 検索条件用Dtoに画面の値を設定
        /// </summary>
        /// <returns></returns>
        private void SetManifestShukeihyoDto()
        {
            this.ManifestShukeihyoDto = new ManifestShukeihyoDto();

            this.ManifestShukeihyoDto.Pattern = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            this.ManifestShukeihyoDto.KyotenCd = Int32.Parse(this.form.KYOTEN_CD.Text);
            this.ManifestShukeihyoDto.KyotenName = this.form.KYOTEN_NAME.Text;
            this.ManifestShukeihyoDto.IsKamiMani = this.form.KBN_KAMI_MANI.Checked;
            this.ManifestShukeihyoDto.IsDenMani = this.form.KBN_DEN_MANI.Checked;
            this.ManifestShukeihyoDto.IchijiNijiKbn = Int32.Parse(this.form.ICHIJI_NIJI_KBN.Text);
            if (null != this.form.KOFU_DATE_FROM.Value)
            {
                this.ManifestShukeihyoDto.KofuDateFrom = ((DateTime)this.form.KOFU_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.KofuDateFrom = null;
            }
            if (null != this.form.KOFU_DATE_TO.Value)
            {
                this.ManifestShukeihyoDto.KofuDateTo = ((DateTime)this.form.KOFU_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.KofuDateTo = null;
            }
            if (null != this.form.UNPAN_DATE_FROM.Value)
            {
                this.ManifestShukeihyoDto.UnpanEndDateFrom = ((DateTime)this.form.UNPAN_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.UnpanEndDateFrom = null;
            }
            if (null != this.form.UNPAN_DATE_TO.Value)
            {
                this.ManifestShukeihyoDto.UnpanEndDateTo = ((DateTime)this.form.UNPAN_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.UnpanEndDateTo = null;
            }
            if (null != this.form.SHOBUN_END_DATE_FROM.Value)
            {
                this.ManifestShukeihyoDto.ShobunEndDateFrom = ((DateTime)this.form.SHOBUN_END_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.ShobunEndDateFrom = null;
            }
            if (null != this.form.SHOBUN_END_DATE_TO.Value)
            {
                this.ManifestShukeihyoDto.ShobunEndDateTo = ((DateTime)this.form.SHOBUN_END_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.ShobunEndDateTo = null;
            }
            if (null != this.form.LAST_SHOBUN_END_DATE_FROM.Value)
            {
                this.ManifestShukeihyoDto.LastShobunEndDateFrom = ((DateTime)this.form.LAST_SHOBUN_END_DATE_FROM.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.LastShobunEndDateFrom = null;
            }
            if (null != this.form.LAST_SHOBUN_END_DATE_TO.Value)
            {
                this.ManifestShukeihyoDto.LastShobunEndDateTo = ((DateTime)this.form.LAST_SHOBUN_END_DATE_TO.Value).ToString("yyyyMMdd");
            }
            else
            {
                this.ManifestShukeihyoDto.LastShobunEndDateTo = null;
            }
            this.ManifestShukeihyoDto.HaishutsuJigyoushaCdFrom = this.form.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoushaNameFrom = this.form.HAISHUTSU_GYOUSHA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoushaCdTo = this.form.HAISHUTSU_GYOUSHA_CD_TO.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoushaNameTo = this.form.HAISHUTSU_GYOUSHA_NAME_TO.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoujouCdFrom = this.form.HAISHUTSU_GENBA_CD_FROM.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoujouNameFrom = this.form.HAISHUTSU_GENBA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoujouCdTo = this.form.HAISHUTSU_GENBA_CD_TO.Text;
            this.ManifestShukeihyoDto.HaishutsuJigyoujouNameTo = this.form.HAISHUTSU_GENBA_NAME_TO.Text;
            this.ManifestShukeihyoDto.UnpanJutakushaCdFrom = this.form.UNPAN_GYOUSHA_CD_FROM.Text;
            this.ManifestShukeihyoDto.UnpanJutakushaNameFrom = this.form.UNPAN_GYOUSHA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.UnpanJutakushaCdTo = this.form.UNPAN_GYOUSHA_CD_TO.Text;
            this.ManifestShukeihyoDto.UnpanJutakushaNameTo = this.form.UNPAN_GYOUSHA_NAME_TO.Text;
            this.ManifestShukeihyoDto.ShobunJigyoushaCdFrom = this.form.SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestShukeihyoDto.ShobunJigyoushaNameFrom = this.form.SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.ShobunJigyoushaCdTo = this.form.SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestShukeihyoDto.ShobunJigyoushaNameTo = this.form.SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestShukeihyoDto.ShobunJigyoujouCdFrom = this.form.SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestShukeihyoDto.ShobunJigyoujouNameFrom = this.form.SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.ShobunJigyoujouCdTo = this.form.SHOBUN_GENBA_CD_TO.Text;
            this.ManifestShukeihyoDto.ShobunJigyoujouNameTo = this.form.SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoushaCdFrom = this.form.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoushaNameFrom = this.form.LAST_SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoushaCdTo = this.form.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoushaNameTo = this.form.LAST_SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoujouCdFrom = this.form.LAST_SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoujouNameFrom = this.form.LAST_SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoujouCdTo = this.form.LAST_SHOBUN_GENBA_CD_TO.Text;
            this.ManifestShukeihyoDto.LastShobunJigyoujouNameTo = this.form.LAST_SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestShukeihyoDto.HoukokushoBunruiCdFrom = this.form.HOUKOKUSHO_CD_FROM.Text;
            this.ManifestShukeihyoDto.HoukokushoBunruiNameFrom = this.form.HOUKOKUSHO_NAME_FROM.Text;
            this.ManifestShukeihyoDto.HoukokushoBunruiCdTo = this.form.HOUKOKUSHO_CD_TO.Text;
            this.ManifestShukeihyoDto.HoukokushoBunruiNameTo = this.form.HOUKOKUSHO_NAME_TO.Text;
            this.ManifestShukeihyoDto.HaikibutsuMeishouCdFrom = this.form.HAIKIBUTSU_CD_FROM.Text;
            this.ManifestShukeihyoDto.HaikibutsuMeishouNameFrom = this.form.HAIKIBUTSU_NAME_FROM.Text;
            this.ManifestShukeihyoDto.HaikibutsuMeishouCdTo = this.form.HAIKIBUTSU_CD_TO.Text;
            this.ManifestShukeihyoDto.HaikibutsuMeishouNameTo = this.form.HAIKIBUTSU_NAME_TO.Text;
            this.ManifestShukeihyoDto.ShobunHouhouCdFrom = this.form.SHOBUN_HOUHOU_CD_FROM.Text;
            this.ManifestShukeihyoDto.ShobunHouhouNameFrom = this.form.SHOBUN_HOUHOU_NAME_FROM.Text;
            this.ManifestShukeihyoDto.ShobunHouhouCdTo = this.form.SHOBUN_HOUHOU_CD_TO.Text;
            this.ManifestShukeihyoDto.ShobunHouhouNameTo = this.form.SHOBUN_HOUHOU_NAME_TO.Text;
            this.ManifestShukeihyoDto.TorihikisakiCdFrom = this.form.TORIHIKISAKI_CD_FROM.Text;
            this.ManifestShukeihyoDto.TorihikisakiNameFrom = this.form.TORIHIKISAKI_NAME_FROM.Text;
            this.ManifestShukeihyoDto.TorihikisakiCdTo = this.form.TORIHIKISAKI_CD_TO.Text;
            this.ManifestShukeihyoDto.TorihikisakiNameTo = this.form.TORIHIKISAKI_NAME_TO.Text;

            var cd1 = GetButsuriName(1);
            var cd2 = GetButsuriName(2);
            var cd3 = GetButsuriName(3);
            var cd4 = GetButsuriName(4);
            var cd5 = GetButsuriName(5);
            var cd6 = GetButsuriName(6);
            var cd7 = GetButsuriName(7);

            if (ConstClass.UPN_GYOUSHA_CD == cd1
                || ConstClass.UPN_GYOUSHA_CD == cd2
                || ConstClass.UPN_GYOUSHA_CD == cd3
                || ConstClass.UPN_GYOUSHA_CD == cd4
                || ConstClass.UPN_GYOUSHA_CD == cd5
                || ConstClass.UPN_GYOUSHA_CD == cd6
                || ConstClass.UPN_GYOUSHA_CD == cd7)
            {
                this.ManifestShukeihyoDto.SelectedColumnUnpanJutakushaCd = true;
            }
            else
            {
                this.ManifestShukeihyoDto.SelectedColumnUnpanJutakushaCd = false;
            }

            if (ConstClass.UPN_SAKI_GENBA_CD == cd1
                || ConstClass.UPN_SAKI_GENBA_CD == cd2
                || ConstClass.UPN_SAKI_GENBA_CD == cd3
                || ConstClass.UPN_SAKI_GENBA_CD == cd4
                || ConstClass.UPN_SAKI_GENBA_CD == cd5
                || ConstClass.UPN_SAKI_GENBA_CD == cd6
                || ConstClass.UPN_SAKI_GENBA_CD == cd7)
            {
                this.ManifestShukeihyoDto.SelectedColumnShobunJigyoujouCd = true;
            }
            else
            {
                this.ManifestShukeihyoDto.SelectedColumnShobunJigyoujouCd = false;
            }

            // グループ化対象のカラム名を決定
            var gc1 = CreateGroupColumn(cd1);
            var gc2 = CreateGroupColumn(cd2);
            var gc3 = CreateGroupColumn(cd3);
            var gc4 = CreateGroupColumn(cd4);
            var gc5 = CreateGroupColumn(cd5);
            var gc6 = CreateGroupColumn(cd6);
            var gc7 = CreateGroupColumn(cd7);

            var sb = new StringBuilder();
            sb.Append(gc1)
                .Append(gc2)
                .Append(gc3)
                .Append(gc4)
                .Append(gc5)
                .Append(gc6)
                .Append(gc7);

            this.ManifestShukeihyoDto.GroupColumn = sb.ToString();
        }

        /// <summary>
        /// (廃棄物種類マスタを考慮した)グループ化の対象カラム作成
        /// </summary>
        /// <param name="butsuriName">物理名</param>
        /// <returns></returns>
        private string CreateGroupColumn(string butsuriName)
        {
            if (string.IsNullOrEmpty(butsuriName))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            if (ConstClass.HAIKI_SHURUI_CD.Equals(butsuriName))
            {
                // 廃棄物種類マスタ(M_HAIKI_SHURUI)はPKが2カラムあるので不足分の項目追加
                sb.Append(",").Append(butsuriName)
                  .Append(",").Append(ConstClass.HAIKI_KBN_CD)
                  .Append(",").Append(butsuriName + "_NAME");
            }
            else
            {
                sb.Append(",").Append(butsuriName)
                  .Append(",").Append(butsuriName + "_NAME");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 鑑と一覧のヘッダー名称を保持するデータテーブルを作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateHeaderTable()
        {
            var dt = new DataTable();

            // カラム作成
            dt.Columns.Add("TITLE");
            dt.Columns.Add("JISHA");
            dt.Columns.Add("KYOTEN");
            dt.Columns.Add("HAKKOU_DATE");
            dt.Columns.Add("JOUKEN_1");
            dt.Columns.Add("JOUKEN_2");
            dt.Columns.Add("COLUMN_1");
            dt.Columns.Add("COLUMN_2");
            dt.Columns.Add("COLUMN_3");
            dt.Columns.Add("COLUMN_4");
            dt.Columns.Add("COLUMN_5");
            dt.Columns.Add("COLUMN_6");
            dt.Columns.Add("COLUMN_7");
            dt.Columns.Add("COLUMN_KANSANGO_SURYO");
            dt.Columns.Add("KANSANGO_SURYO_SUM");

            var row = dt.NewRow();

            #region Header項目設定

            // 自社情報マスタを取得して会社略称名を取得
            var jisha = string.Empty;
            var mCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>().GetAllData().FirstOrDefault();
            if (mCorpInfo != null)
            {
                jisha = mCorpInfo.CORP_RYAKU_NAME ?? string.Empty;
            }

            row["TITLE"] = string.Format("マニフェスト集計表（{0}）", this.ManifestShukeihyoDto.Pattern.PATTERN_NAME);
            row["JISHA"] = jisha;
            row["KYOTEN"] = this.ManifestShukeihyoDto.KyotenName;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //row["HAKKOU_DATE"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
            row["HAKKOU_DATE"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            row["JOUKEN_1"] = CreateJouken1FieldData(this.ManifestShukeihyoDto);
            row["JOUKEN_2"] = CreateJouken2FieldData();

            #endregion

            #region PageHeader項目

            row["COLUMN_1"] = GetKoumokuRonriName(1);
            row["COLUMN_2"] = GetKoumokuRonriName(2);
            row["COLUMN_3"] = GetKoumokuRonriName(3);
            row["COLUMN_4"] = GetKoumokuRonriName(4);
            row["COLUMN_5"] = GetKoumokuRonriName(5);
            row["COLUMN_6"] = GetKoumokuRonriName(6);
            row["COLUMN_7"] = GetKoumokuRonriName(7);

            // 廃棄物種類（報告書分類） ⇒ 廃棄物種類 に表示を変更
            for (int i = 1; i < 8; i++)
            {
                if (row["COLUMN_" + i] != null && !string.IsNullOrEmpty(row["COLUMN_" + i].ToString()))
                {
                    row["COLUMN_" + i] = row["COLUMN_" + i].ToString().Equals(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME) ? ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME : row["COLUMN_" + i];
                }
            }

            row["COLUMN_KANSANGO_SURYO"] = CreateSuryoFieldData();

            #endregion

            dt.Rows.Add(row);

            return dt;
        }

        /// <summary>
        /// レポート鏡の抽出条件文字列を作成（左側）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string CreateJouken1FieldData(ManifestShukeihyoDto dto)
        {
            if (dto == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.AppendLine("[抽出条件]");

            sb.Append(dto.Kyoten);
            sb.Append(dto.OutputKbn);

            if (!AppConfig.IsManiLite)
            {
                // マニライト(C8)版以外の場合、抽出条件の欄に「一次二次区分」を出力
                sb.Append(dto.IchijiNijiKbnName);
            }

            sb.Append(CreateJouken1Date(this.form.KOFU_DATE_FROM, this.form.KOFU_DATE_TO, this.form.label16.Text));
            sb.Append(CreateJouken1Date(this.form.UNPAN_DATE_FROM, this.form.UNPAN_DATE_TO, this.form.label18.Text));
            sb.Append(CreateJouken1Date(this.form.SHOBUN_END_DATE_FROM, this.form.SHOBUN_END_DATE_TO, this.form.label19.Text));
            sb.Append(CreateJouken1Date(this.form.LAST_SHOBUN_END_DATE_FROM, this.form.LAST_SHOBUN_END_DATE_TO, this.form.label23.Text));

            sb.Append(dto.HaishutsuJigyousha);
            sb.Append(dto.HaishutsuJigyoujou);
            sb.Append(dto.UnpanJutakusha);
            sb.Append(dto.ShobunJigyousha);
            sb.Append(dto.ShobunJigyoujou);
            sb.Append(dto.LastShobunJigyousha);
            sb.Append(dto.LastShobunJigyoujou);
            sb.Append(dto.HoukokushoBunrui);
            sb.Append(dto.HaikibutsuMeishou);
            sb.Append(dto.ShobunHouhou);
            sb.Append(dto.Torihikisaki);

            return sb.ToString();
        }

        /// <summary>
        /// 抽出条件の日付系の文言作成
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string CreateJouken1Date(CustomDateTimePicker from, CustomDateTimePicker to, string name)
        {
            if (from.Value == null && to.Value == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            if (from.Value != null)
            {
                sb.Append(((DateTime)from.Value).ToString("yyyy/MM/dd"));
            }

            sb.Append(" ～ ");

            if (to.Value != null)
            {
                sb.Append(((DateTime)to.Value).ToString("yyyy/MM/dd"));
            }

            return string.Format("　[{0}] {1}{2}", name, sb.ToString(), Environment.NewLine);
        }

        /// <summary>
        /// レポート鏡の抽出条件文字列を作成（右側）
        /// </summary>
        /// <returns></returns>
        private string CreateJouken2FieldData()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[集計項目]");
            sb.Append("　[1] ");
            sb.AppendLine(GetKoumokuRonriName(1));
            sb.Append("　[2] ");
            sb.AppendLine(GetKoumokuRonriName(2));
            sb.Append("　[3] ");
            sb.AppendLine(GetKoumokuRonriName(3));
            sb.Append("　[4] ");
            sb.AppendLine(GetKoumokuRonriName(4));
            sb.Append("　[5] ");
            sb.AppendLine(GetKoumokuRonriName(5));
            sb.Append("　[6] ");
            sb.AppendLine(GetKoumokuRonriName(6));
            sb.Append("　[7] ");
            sb.AppendLine(GetKoumokuRonriName(7));

            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);

            sb.AppendLine("[明細項目]");
            sb.Append("　[1] ");
            sb.AppendLine(CreateSuryoFieldData());

            return sb.ToString();
        }

        /// <summary>
        /// システム設定から換算後数量の単位を取得して文言を作成
        /// </summary>
        /// <returns></returns>
        private string CreateSuryoFieldData()
        {
            var unitName = string.Empty;

            var entity = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData().FirstOrDefault();
            if (entity != null && !entity.MANI_KANSAN_KIHON_UNIT_CD.IsNull)
            {
                var unit = DaoInitUtility.GetComponent<IM_UNITDao>().GetDataByCd(entity.MANI_KANSAN_KIHON_UNIT_CD.Value);
                if (unit != null && !string.IsNullOrEmpty(unit.UNIT_NAME_RYAKU))
                {
                    unitName = string.Format("({0})", unit.UNIT_NAME_RYAKU);
                }
            }

            return string.Format(ConstClass.SURYO, unitName);
        }

        /// <summary>
        /// 一覧情報を保持するデータテーブルを作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDetailTable()
        {
            var dt = new DataTable();

            // カラム作成
            dt.Columns.Add("CD_1");
            dt.Columns.Add("NAME_1");
            dt.Columns.Add("CD_2");
            dt.Columns.Add("NAME_2");
            dt.Columns.Add("CD_3");
            dt.Columns.Add("NAME_3");
            dt.Columns.Add("CD_4");
            dt.Columns.Add("NAME_4");
            dt.Columns.Add("CD_5");
            dt.Columns.Add("NAME_5");
            dt.Columns.Add("CD_6");
            dt.Columns.Add("NAME_6");
            dt.Columns.Add("CD_7");
            dt.Columns.Add("NAME_7");
            dt.Columns.Add("KANSANGO_SURYO");
            dt.Columns.Add("GROUP1_NAME");
            dt.Columns.Add("GROUP1_KEY");
            dt.Columns.Add("GROUP1_VALUE");
            dt.Columns.Add("GROUP2_NAME");
            dt.Columns.Add("GROUP2_KEY");
            dt.Columns.Add("GROUP2_VALUE");
            dt.Columns.Add("GROUP3_NAME");
            dt.Columns.Add("GROUP3_KEY");
            dt.Columns.Add("GROUP3_VALUE");
            dt.Columns.Add("GROUP4_NAME");
            dt.Columns.Add("GROUP4_KEY");
            dt.Columns.Add("GROUP4_VALUE");
            dt.Columns.Add("GROUP5_NAME");
            dt.Columns.Add("GROUP5_KEY");
            dt.Columns.Add("GROUP5_VALUE");
            dt.Columns.Add("GROUP6_NAME");
            dt.Columns.Add("GROUP6_KEY");
            dt.Columns.Add("GROUP6_VALUE");
            dt.Columns.Add("GROUP7_NAME");
            dt.Columns.Add("GROUP7_KEY");
            dt.Columns.Add("GROUP7_VALUE");

            // 物理名取得
            var cd1 = GetButsuriName(1);
            var cd2 = GetButsuriName(2);
            var cd3 = GetButsuriName(3);
            var cd4 = GetButsuriName(4);
            var cd5 = GetButsuriName(5);
            var cd6 = GetButsuriName(6);
            var cd7 = GetButsuriName(7);

            // 集計有無判定用変数
            var shuukeiFlg1 = false;
            var shuukeiFlg2 = false;
            var shuukeiFlg3 = false;
            var shuukeiFlg4 = false;
            var shuukeiFlg5 = false;
            var shuukeiFlg6 = false;
            var shuukeiFlg7 = false;

            if (this.ManifestShukeihyoDto != null && this.ManifestShukeihyoDto.Pattern != null)
            {
                shuukeiFlg1 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(1);
                shuukeiFlg2 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(2);
                shuukeiFlg3 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(3);
                shuukeiFlg4 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(4);
                shuukeiFlg5 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(5);
                shuukeiFlg6 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(6);
                shuukeiFlg7 = this.ManifestShukeihyoDto.Pattern.GetShuukeiFlag(7);
            }

            // 各小計「～計」の名称取得
            var gr1Name = GetGroupName(1);
            var gr2Name = GetGroupName(2);
            var gr3Name = GetGroupName(3);
            var gr4Name = GetGroupName(4);
            var gr5Name = GetGroupName(5);
            var gr6Name = GetGroupName(6);
            var gr7Name = GetGroupName(7);

            // 廃棄物種類（報告書分類）計 ⇒ 廃棄物種類計 に表示を変更
            gr1Name = (gr1Name != null && !string.IsNullOrEmpty(gr1Name.ToString())) ? gr1Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr1Name;
            gr2Name = (gr2Name != null && !string.IsNullOrEmpty(gr2Name.ToString())) ? gr2Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr2Name;
            gr3Name = (gr3Name != null && !string.IsNullOrEmpty(gr3Name.ToString())) ? gr3Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr3Name;
            gr4Name = (gr4Name != null && !string.IsNullOrEmpty(gr4Name.ToString())) ? gr4Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr4Name;
            gr5Name = (gr5Name != null && !string.IsNullOrEmpty(gr5Name.ToString())) ? gr5Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr5Name;
            gr6Name = (gr6Name != null && !string.IsNullOrEmpty(gr6Name.ToString())) ? gr6Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr6Name;
            gr7Name = (gr7Name != null && !string.IsNullOrEmpty(gr7Name.ToString())) ? gr7Name.Replace(ConstClass.HAIKIBUTSU_SHURUI_SCREEN_NAME, ConstClass.HAIKIBUTSU_SHURUI_PRINT_NAME) : gr7Name;

            // グループ化判定用変数
            var group1key = String.Empty;
            var isGroup1keyChange = false;
            var group2key = String.Empty;
            var isGroup2keyChange = false;
            var group3key = String.Empty;
            var isGroup3keyChange = false;
            var group4key = String.Empty;
            var isGroup4keyChange = false;
            var group5key = String.Empty;
            var isGroup5keyChange = false;
            var group6key = String.Empty;
            var isGroup6keyChange = false;
            var group7key = String.Empty;

            var group1keyN = String.Empty;
            var group2keyN = String.Empty;
            var group3keyN = String.Empty;
            var group4keyN = String.Empty;
            var group5keyN = String.Empty;
            var group6keyN = String.Empty;
            var group7keyN = String.Empty;

            
            // 端数桁を設定
            int hasuuKeta = 0;
            if (SystemProperty.Format.ManifestSuuryou.IndexOf(".") != -1)
            {
                hasuuKeta = SystemProperty.Format.ManifestSuuryou.Length 
                                - SystemProperty.Format.ManifestSuuryou.IndexOf(".") - 1;
            }
            kansangoSuryoSum = 0;

            foreach (DataRow dataRow in this.ManifestDataTable.Rows)
            {
                var row = dt.NewRow();

                #region Detail項目設定

                // 名称は対象CDの物理名+「_NAME」で取得
                var gk1 = CreateGroupKey(dataRow, cd1);
                var gk1n = String.Empty;
                if (!string.IsNullOrEmpty(cd1))
                {
                    gk1n = CreateGroupKey(dataRow, cd1 + "_NAME");
                }
                if (!string.IsNullOrEmpty(cd1) && (!group1key.Equals(gk1) || !group1keyN.Equals(gk1n)))
                {
                    row["CD_1"] = dataRow[cd1];
                    row["NAME_1"] = dataRow[cd1 + "_NAME"];
                    group1key = gk1;
                    group1keyN = gk1n;
                    isGroup1keyChange = true;
                }
                else
                {
                    row["CD_1"] = string.Empty;
                    row["NAME_1"] = string.Empty;
                    isGroup1keyChange = false;
                }

                var gk2 = CreateGroupKey(dataRow, cd2);
                var gk2n = String.Empty;
                if (!string.IsNullOrEmpty(cd2))
                {
                    gk2n = CreateGroupKey(dataRow, cd2 + "_NAME");
                }
                if (!string.IsNullOrEmpty(cd2) && (isGroup1keyChange || !group2key.Equals(gk2) || !group2keyN.Equals(gk2n)))
                {
                    row["CD_2"] = dataRow[cd2];
                    row["NAME_2"] = dataRow[cd2 + "_NAME"];
                    group2key = gk2;
                    group2keyN = gk2n;
                    isGroup2keyChange = true;
                }
                else
                {
                    row["CD_2"] = string.Empty;
                    row["NAME_2"] = string.Empty;
                    isGroup2keyChange = false;
                }

                var gk3 = CreateGroupKey(dataRow, cd3);
                var gk3n = String.Empty;
                if (!string.IsNullOrEmpty(cd3))
                {
                    gk3n = CreateGroupKey(dataRow, cd3 + "_NAME");
                } 
                if (!string.IsNullOrEmpty(cd3) && (isGroup1keyChange || isGroup2keyChange || !group3key.Equals(gk3) || !group3keyN.Equals(gk3n)))
                {
                    row["CD_3"] = dataRow[cd3];
                    row["NAME_3"] = dataRow[cd3 + "_NAME"];
                    group3key = gk3;
                    group3keyN = gk3n;
                    isGroup3keyChange = true;
                }
                else
                {
                    row["CD_3"] = string.Empty;
                    row["NAME_3"] = string.Empty;
                    isGroup3keyChange = false;
                }

                var gk4 = CreateGroupKey(dataRow, cd4);
                var gk4n = String.Empty;
                if (!string.IsNullOrEmpty(cd4))
                {
                    gk4n = CreateGroupKey(dataRow, cd4 + "_NAME");
                } 
                if (!string.IsNullOrEmpty(cd4) && (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || !group4key.Equals(gk4) || !group4keyN.Equals(gk4n)))
                {
                    row["CD_4"] = dataRow[cd4];
                    row["NAME_4"] = dataRow[cd4 + "_NAME"];
                    group4key = gk4;
                    group4keyN = gk4n;
                    isGroup4keyChange = true;
                }
                else
                {
                    row["CD_4"] = string.Empty;
                    row["NAME_4"] = string.Empty;
                    isGroup4keyChange = false;
                }

                var gk5 = CreateGroupKey(dataRow, cd5);
                var gk5n = String.Empty;
                if (!string.IsNullOrEmpty(cd5))
                {
                    gk5n = CreateGroupKey(dataRow, cd5 + "_NAME");
                } 
                if (!string.IsNullOrEmpty(cd5) && (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || isGroup4keyChange || !group5key.Equals(gk5) || !group5keyN.Equals(gk5n)))
                {
                    row["CD_5"] = dataRow[cd5];
                    row["NAME_5"] = dataRow[cd5 + "_NAME"];
                    group5key = gk5;
                    group5keyN = gk5n;
                    isGroup5keyChange = true;
                }
                else
                {
                    row["CD_5"] = string.Empty;
                    row["NAME_5"] = string.Empty;
                    isGroup5keyChange = false;
                }

                var gk6 = CreateGroupKey(dataRow, cd6);
                var gk6n = String.Empty;
                if (!string.IsNullOrEmpty(cd6))
                {
                    gk6n = CreateGroupKey(dataRow, cd6 + "_NAME");
                } 
                if (!string.IsNullOrEmpty(cd6) && (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || isGroup4keyChange || isGroup5keyChange || !group6key.Equals(gk6) || !group6keyN.Equals(gk6n)))
                {
                    row["CD_6"] = dataRow[cd6];
                    row["NAME_6"] = dataRow[cd6 + "_NAME"];
                    group6key = gk6;
                    group6keyN = gk6n;
                    isGroup6keyChange = true;
                }
                else
                {
                    row["CD_6"] = string.Empty;
                    row["NAME_6"] = string.Empty;
                    isGroup6keyChange = false;
                }

                var gk7 = CreateGroupKey(dataRow, cd7);
                var gk7n = String.Empty;
                if (!string.IsNullOrEmpty(cd7))
                {
                    gk7n = CreateGroupKey(dataRow, cd7 + "_NAME");
                } 
                if (!string.IsNullOrEmpty(cd7) && (isGroup1keyChange || isGroup2keyChange || isGroup3keyChange || isGroup4keyChange || isGroup5keyChange || isGroup6keyChange || !group7key.Equals(gk7) || !group7keyN.Equals(gk7n)))
                {
                    row["CD_7"] = dataRow[cd7];
                    row["NAME_7"] = dataRow[cd7 + "_NAME"];
                    group7key = gk7;
                    group7keyN = gk7n;
                }
                else
                {
                    row["CD_7"] = string.Empty;
                    row["NAME_7"] = string.Empty;
                }

                //row["KANSANGO_SURYO"] = dataRow["KANSANGO_SURYO"];
                decimal kansangoSuuryo = FractionLogic.FractionCalc(Convert.ToDecimal(dataRow["KANSANGO_SURYO"].ToString()),
                                                                   FractionLogic.FractionType.ROUND,
                                                                   hasuuKeta);
                row["KANSANGO_SURYO"] = kansangoSuuryo;
                kansangoSuryoSum += kansangoSuuryo;

                #endregion

                #region Group項目設定

                if (shuukeiFlg1)
                {
                    row["GROUP1_NAME"] = gr1Name;
                    row["GROUP1_KEY"] = gk1;
                }
                else
                {
                    row["GROUP1_NAME"] = string.Empty;
                    row["GROUP1_KEY"] = string.Empty;
                }

                if (shuukeiFlg2)
                {
                    row["GROUP2_NAME"] = gr2Name;
                    row["GROUP2_KEY"] = gk2;
                }
                else
                {
                    row["GROUP2_NAME"] = string.Empty;
                    row["GROUP2_KEY"] = string.Empty;
                }

                if (shuukeiFlg3)
                {
                    row["GROUP3_NAME"] = gr3Name;
                    row["GROUP3_KEY"] = gk3;
                }
                else
                {
                    row["GROUP3_NAME"] = string.Empty;
                    row["GROUP3_KEY"] = string.Empty;
                }

                if (shuukeiFlg4)
                {
                    row["GROUP4_NAME"] = gr4Name;
                    row["GROUP4_KEY"] = gk4;
                }
                else
                {
                    row["GROUP4_NAME"] = string.Empty;
                    row["GROUP4_KEY"] = string.Empty;
                }

                if (shuukeiFlg5)
                {
                    row["GROUP5_NAME"] = gr5Name;
                    row["GROUP5_KEY"] = gk5;
                }
                else
                {
                    row["GROUP5_NAME"] = string.Empty;
                    row["GROUP5_KEY"] = string.Empty;
                }

                if (shuukeiFlg6)
                {
                    row["GROUP6_NAME"] = gr6Name;
                    row["GROUP6_KEY"] = gk6;
                }
                else
                {
                    row["GROUP6_NAME"] = string.Empty;
                    row["GROUP6_KEY"] = string.Empty;
                }

                if (shuukeiFlg7)
                {
                    row["GROUP7_NAME"] = gr7Name;
                    row["GROUP7_KEY"] = gk7;
                }
                else
                {
                    row["GROUP7_NAME"] = string.Empty;
                    row["GROUP7_KEY"] = string.Empty;
                }

                #endregion

                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// (廃棄物種類マスタを考慮した)グループキー作成
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="butsuriName"></param>
        /// <returns></returns>
        private string CreateGroupKey(DataRow dataRow, string butsuriName)
        {
            if (string.IsNullOrEmpty(butsuriName))
            {
                return string.Empty;
            }
            
            var groupkey = string.Empty;
            // 廃棄物種類マスタ(M_HAIKI_SHURUI)はPKが2カラムあるので不足分の項目追加
            if(ConstClass.HAIKI_SHURUI_CD.Equals(butsuriName))
            {
                // HAIKI_SHURUI_CD/HAIKI_KBN_CD 形式で設定
                groupkey = string.Format("{0}/{1}", dataRow[butsuriName].ToString(), dataRow[ConstClass.HAIKI_KBN_CD].ToString());
            }
            else
            {
                groupkey = dataRow[butsuriName].ToString();
            }

            return groupkey;
        }


        /// <summary>
        /// 項目論理名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns></returns>
        private string GetKoumokuRonriName(int shuukeiKoumokuNo)
        {
            if (this.ManifestShukeihyoDto == null || this.ManifestShukeihyoDto.Pattern == null)
            {
                return string.Empty;
            }

            var select = this.ManifestShukeihyoDto.Pattern.GetColumnSelect(shuukeiKoumokuNo);
            if (select == null)
            {
                return string.Empty;
            }

            return select.KOUMOKU_RONRI_NAME;
        }

        /// <summary>
        /// 物理名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns></returns>
        private string GetButsuriName(int shuukeiKoumokuNo)
        {
            if (this.ManifestShukeihyoDto == null || this.ManifestShukeihyoDto.Pattern == null)
            {
                return string.Empty;
            }

            var select = this.ManifestShukeihyoDto.Pattern.GetColumnSelectDetail(shuukeiKoumokuNo);
            if (select == null)
            {
                return string.Empty;
            }

            return select.BUTSURI_NAME;
        }

        /// <summary>
        /// 帳票集計用のグループ名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo"></param>
        /// <returns></returns>
        private string GetGroupName(int shuukeiKoumokuNo)
        {
            var ronriName = GetKoumokuRonriName(shuukeiKoumokuNo);

            if (string.IsNullOrEmpty(ronriName))
            {
                return string.Empty;
            }

            return ronriName + "合計";
        }

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
