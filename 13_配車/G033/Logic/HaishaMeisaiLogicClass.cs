using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Dao;

namespace Shougun.Core.Allocation.HaishaMeisai
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class HaishaMeisaiLogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// UIForm
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        MessageBoxShowLogic msgLogic;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// DTO
        /// </summary>
        private HaishaMeisaiDTOClass dto;

        /// <summary>
        /// 配車明細表のDao
        /// </summary>
        private HaishaMeisaiDAOClass dao;

        /// <summary>
        /// 運転者のDao
        /// </summary>
        private UntenshaNameDAOClass untenshaNameDAO;

        /// <summary>
        /// 運転者名称データ
        /// </summary>
        public M_SHAIN[] untenshaAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public HaishaMeisaiDTOClass SearchString { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public HaishaMeisaiDTOClass[] SearchDetailResult;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.HaishaMeisai.Setting.ButtonSetting.xml";

        #endregion

        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HaishaMeisaiLogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                // dto initial
                this.dto = new HaishaMeisaiDTOClass();
                msgLogic = new MessageBoxShowLogic();
                dao = DaoInitUtility.GetComponent<HaishaMeisaiDAOClass>();
                this.untenshaNameDAO = DaoInitUtility.GetComponent<UntenshaNameDAOClass>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("HaishaMeisaiLogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion Constructor

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 画面初期表示設定
                this.InitializeScreen();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //2014/01/15 追加 qiao start
                // サブファンクション非表示
                this.parentForm.ProcessButtonPanel.Visible = false;
                //2014/01/15 追加 qiao end
                this.allControl = this.form.allControl;

                // 運転者POPUPデータ設定
                UntenshaPopUpDataInit();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //「期間From」／システム日付
                this.form.HIDUKE_FROM.Value = parentForm.sysDate;

                //「期間To」／作業開始日
                this.form.HIDUKE_TO.Value = parentForm.sysDate;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitializeScreen", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタンイベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //2014/01/15 削除 qiao start
                // 「F5印刷ボタン」初期状態では非アクティブとする
                //parentForm.bt_func5.Enabled = false;
                // 「F6CSV出力ボタン」初期状態では非アクティブとする
                //parentForm.bt_func6.Enabled = false;
                //2014/01/15 削除 qiao end

                //2014/01/15 追加 qiao start
                parentForm.bt_func7.Click += (sender, e) =>
                {
                    //画面期間は必須入力項目チェック
                    this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = false;
                    this.form.UNTENSHA_CD_TO.IsInputErrorOccured = false;
                };
                this.form.C_Regist(parentForm.bt_func7);
                //2014/01/15 追加 qiao end

                // 「Ｆ７ 表示ボタン」イベントのイベント生成
                parentForm.bt_func7.Click += new EventHandler(this.bt_func7_Click);

                //2014/01/15 追加 qiao start
                parentForm.bt_func6.Click += (sender, e) =>
                {
                    //画面期間は必須入力項目チェック
                    this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = false;
                    this.form.UNTENSHA_CD_TO.IsInputErrorOccured = false;
                };
                this.form.C_Regist(parentForm.bt_func6);
                //2014/01/15 追加 qiao end

                // 「Ｆ6 CSV出力ボタン」イベントのイベント生成
                parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);

                //2014/01/15 削除 qiao start
                // 「Ｆ9 実行ボタン」イベントのイベント生成
                //parentForm.bt_func9.Click += (sender, e) =>
                //{
                //    //画面期間は必須入力項目チェック
                //    this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = false;
                //    this.form.UNTENSHA_CD_TO.IsInputErrorOccured = false;
                //};

                //this.form.C_Regist(parentForm.bt_func9);
                //parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
                //2014/01/15 削除 qiao end

                // 「Ｆ12 ﾃﾞｰﾀ出力ボタン」イベントのイベント生成
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

                // 20141127 teikyou ダブルクリックを追加する　start
                this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                this.form.UNTENSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(UNTENSHA_CD_TO_MouseDoubleClick);
                // 20141127 teikyou ダブルクリックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 実行処理
        /// <summary>
        /// 実行処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件を設定する
                HaishaMeisaiDTOClass searchCondition = new HaishaMeisaiDTOClass();
                searchCondition.kikanFrom = DateTime.ParseExact(this.form.HIDUKE_FROM.Text.Substring(0, 10) + " 00:00:00", "yyyy/MM/dd HH:mm:ss", null);
                searchCondition.kikanTo = DateTime.ParseExact(this.form.HIDUKE_TO.Text.Substring(0, 10) + " 23:59:59", "yyyy/MM/dd HH:mm:ss", null);
                searchCondition.untenshaFrom = this.form.UNTENSHA_CD_FROM.Text;
                searchCondition.untenshaTo = this.form.UNTENSHA_CD_TO.Text;
                // 検索結果を取得する
                this.SearchDetailResult = this.dao.GetReportDetailData(searchCondition);

                // 検索結果を画面に設定する
                int count = this.SearchDetailResult.Length;
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");

                    //明細をクリア
                    this.form.customDataGridView1.DataSource = null;
                }
                return count;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 検索条件チェック
        /// <summary>
        /// 検索条件チェック
        /// </summary>
        private bool isOKCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool returnVal = false;
                // 運転者From、Toのチェック
                string untenshaFrom = this.form.UNTENSHA_CD_FROM.Text;
                string untenshaTo = this.form.UNTENSHA_CD_TO.Text;
                if (untenshaFrom.CompareTo(untenshaTo) == 1)
                {
                    this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD_TO.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD_FROM.Focus();
                    // 運転者コードFromが運転者コードToより大きい、messageが出る
                    //msgLogic.MessageBoxShow("E085", "運転者CD(To)");
                    msgLogic.MessageBoxShow("E147", "運転者CD");

                    return returnVal;
                }
                else
                {
                    this.form.UNTENSHA_CD_FROM.IsInputErrorOccured = false;
                    this.form.UNTENSHA_CD_TO.IsInputErrorOccured = false;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("isOKCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「F7 帳票印刷ボタン」イベント処理

        #region　帳票データ

        public void setPrintData(ReportInfoBase reportInfo)
        {
            try
            {
                LogUtility.DebugMethodStart(reportInfo);
                /// <summary>
                /// ヘッダ部(R346)
                /// </summary>
                string[] headerReportItemName_R346 = { "SAGYOU_DATE", "UNTENSHA", "SYUBETU" };

                /// <summary>
                /// 明細部(R346)
                /// </summary>
                string[] detailReportItemName_R346 = { "ROW", "MANIFEST_SHURUI_NAME", "MANIFEST_TEHAI_NAME", "UKETSUKE_NUMBER", "GENCHAKU_TIME_NAME", 
                                                       "GENCHAKU_TIME", "SAGYOU_TIME", "GYOUSHA_CD", "GYOUSHA_NAME", "SHASHU_CD", 
                                                       "SHASHU_NAME", "SHARYOU_CD", "SHARYOU_NAME", "GENBA_CD", "GENBA_NAME", 
                                                       "NIOROSHI_NIZUMI_CD", "NIOROSHI_NIZUMI_NAME", "GENBA_ADDRESS", "NIOROSHI_NIZUMI_ADDRESS", "GENBA_TEL", 
                                                       "TANTOUSHA", "GENBA_KEITAI_TEL", "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3",
                                                       "UNTENSHA_SIJIJIKOU1", "UNTENSHA_SIJIJIKOU2", "UNTENSHA_SIJIJIKOU3", "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME", 
                                                       "CONTENA_JOUKYOU_NAME", "DAISUU", "HINMEI_CD", "HINMEI_NAME", "SUURYOU", "UNIT_NAME", };

                DataTable dataTableHeader = new DataTable();
                dataTableHeader.TableName = "Header";
                DataTable dataTableDetail = new DataTable();
                dataTableDetail.TableName = "Detail";
                for (int i = 0; i < headerReportItemName_R346.Length; i++)
                {
                    dataTableHeader.Columns.Add(headerReportItemName_R346[i]);
                }
                for (int i = 0; i < detailReportItemName_R346.Length; i++)
                {
                    dataTableDetail.Columns.Add(detailReportItemName_R346[i]);
                }
                DataRow rowTmpHeader;
                DataRow rowTmpDetail;
                Dictionary<string, Dictionary<string, DataTable>> DataTablePageList = new Dictionary<string, Dictionary<string, DataTable>>();
                string key = string.Empty;
                string keyWork = string.Empty;
                //種別
                string syubetsu = string.Empty;
                //作業日
                string sagyobi = string.Empty;
                //運転者
                string utensha = string.Empty;
                HaishaMeisaiDTOClass printDto;
                object value;
                Int64 rowNo = 1;
                for (int i = 0; i < this.SearchDetailResult.Length; i++)
                {
                    printDto = SearchDetailResult[i];
                    //改ページ条件
                    key = printDto.SYUBETUCD + "_" + printDto.SAGYOU_DATE.Substring(0, 10).Replace("/", "") + "_" + printDto.UNTENSHA_CD;
                    if (i < this.SearchDetailResult.Length - 1)
                    {
                        keyWork = SearchDetailResult[i + 1].SYUBETUCD + "_"
                                  + SearchDetailResult[i + 1].SAGYOU_DATE.Substring(0, 10).Replace("/", "") + "_"
                                  + SearchDetailResult[i + 1].UNTENSHA_CD;
                    }
                    #region - Header -
                    if (key != keyWork || i == this.SearchDetailResult.Length - 1)
                    {
                        rowTmpHeader = dataTableHeader.NewRow();
                        var t = printDto.GetType();
                        foreach (var pn in headerReportItemName_R346)
                        {
                            value = t.GetProperty(pn).GetValue(printDto, null);
                            rowTmpHeader[pn] = (value == null ? string.Empty : value.ToString());
                        }
                        rowTmpHeader["SAGYOU_DATE"] = DateTime.Parse(printDto.SAGYOU_DATE).ToString("yyyy年MM月dd日(ddd)");
                        dataTableHeader.Rows.Add(rowTmpHeader);
                    }

                    #endregion - Header -

                    #region - Detail -
                    rowTmpDetail = dataTableDetail.NewRow();
                    var t2 = printDto.GetType();
                    foreach (var pn in detailReportItemName_R346)
                    {
                        value = t2.GetProperty(pn).GetValue(printDto, null);
                        rowTmpDetail[pn] = (value == null ? string.Empty : value.ToString());
                    }
                    //種別_作業日_運転者CDの組合は変えると、1から連番
                    rowTmpDetail["ROW"] = (rowNo++).ToString();
                    //台数
                    rowTmpDetail["DAISUU"] = (printDto.DAISUU == null ? string.Empty :
                                                            decimal.Parse(printDto.DAISUU).ToString("N").Replace(".00", "") + "台");
                    //数量
                    rowTmpDetail["SUURYOU"] = (printDto.SUURYOU == null ? string.Empty :
                                                            decimal.Parse(printDto.SUURYOU).ToString("N").Replace(".00", ""));
                    //現着時間
                    rowTmpDetail["GENCHAKU_TIME"] = printDto.GENCHAKU_TIME == null ? "" : DateTime.Parse(printDto.GENCHAKU_TIME).ToString("HH時mm分");
                    //作業時間
                    rowTmpDetail["SAGYOU_TIME"] = printDto.SAGYOU_TIME == null ? "" : DateTime.Parse(printDto.SAGYOU_TIME).ToString("HH時間mm分");
                    dataTableDetail.Rows.Add(rowTmpDetail);
                    if (key != keyWork || i == SearchDetailResult.Length - 1)
                    {
                        rowNo = 1;
                        reportInfo.DataTablePageList[key] = new Dictionary<string, DataTable>();
                        reportInfo.DataTablePageList[key].Add("Header", dataTableHeader);
                        reportInfo.DataTablePageList[key].Add("Detail", dataTableDetail);
                        dataTableHeader = new DataTable();
                        dataTableHeader.TableName = "Header";
                        dataTableDetail = new DataTable();
                        dataTableDetail.TableName = "Detail";
                        for (int j = 0; j < headerReportItemName_R346.Length; j++)
                        {
                            dataTableHeader.Columns.Add(headerReportItemName_R346[j]);
                        }
                        for (int j = 0; j < detailReportItemName_R346.Length; j++)
                        {
                            dataTableDetail.Columns.Add(detailReportItemName_R346[j]);
                        }
                    }

                    #endregion - Detail -
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setPrintData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion
        /// <summary>
        /// 「F7 帳票印刷ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return;
                }
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

                var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();

                var fromUntensha = this.GetUntensha().OrderBy(u => u.SHAIN_CD).FirstOrDefault();
                var toUntensha = this.GetUntensha().OrderBy(u => u.SHAIN_CD).LastOrDefault();
                if (null != fromUntensha && String.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text))
                {
                    var shain = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = fromUntensha.SHAIN_CD}).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                    this.form.UNTENSHA_CD_FROM.Text = fromUntensha.SHAIN_CD;
                    this.form.UNTENSHA_NAME_FROM.Text = shain.SHAIN_NAME_RYAKU;
                }
                if (null != toUntensha && String.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text))
                {
                    var shain = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = toUntensha.SHAIN_CD}).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                    this.form.UNTENSHA_CD_TO.Text = toUntensha.SHAIN_CD;
                    this.form.UNTENSHA_NAME_TO.Text = shain.SHAIN_NAME_RYAKU;
                }

                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start

                //ReportInfoBase reportInfo;
                ReportInfoR346 reportInfo = new ReportInfoR346(WINDOW_ID.R_HAISYA_MEISAISYO);
                //帳票データ
                //reportInfo.CreateSampleData();

                setPrintData(reportInfo);
                reportInfo.Create(@".\Template\R346-Form.xml", "LAYOUT1", new DataTable());

                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(reportInfo, WINDOW_ID.R_HAISYA_MEISAISYO))
                {
                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion 「F7 帳票印刷ボタン」イベント処理

        #region 「F6 CSV出力ボタン」イベント処理
        /// <summary>
        /// 「F6 CSV出力ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e"></param>
        void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return;
                }
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

                var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();

                var fromUntensha = this.GetUntensha().OrderBy(u => u.SHAIN_CD).FirstOrDefault();
                var toUntensha = this.GetUntensha().OrderBy(u => u.SHAIN_CD).LastOrDefault();
                if (null != fromUntensha && String.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text))
                {
                    var shain = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = fromUntensha.SHAIN_CD, DELETE_FLG = false }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                    this.form.UNTENSHA_CD_FROM.Text = fromUntensha.SHAIN_CD;
                    this.form.UNTENSHA_NAME_FROM.Text = shain.SHAIN_NAME_RYAKU;
                }
                if (null != toUntensha && String.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text))
                {
                    var shain = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = toUntensha.SHAIN_CD, DELETE_FLG = false }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();

                    this.form.UNTENSHA_CD_TO.Text = toUntensha.SHAIN_CD;
                    this.form.UNTENSHA_NAME_TO.Text = shain.SHAIN_NAME_RYAKU;
                }

                //2014/01/15 追加 qiao start
                if (!dataGet())
                {
                    //データが0件の場合
                    return;
                }
                //2014/01/15 追加 qiao start
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string[] csvHead = { "作業日", "運転者", "NO", "収集マニ情報", "マニ手配", "受付番号", "現着時間名", 
                                       "現着時間", "作業時間", "業者CD", "業者名", "車種CD", 
                                       "車種名", "車輌CD", "車輌名", "現場CD", "現場名", 
                                       "荷降先・荷積先CD", "荷降先・荷積先", "現場住所", "荷降先・荷積先住所", "現場電話番号", 
                                       "担当者名", "担当者携帯番号", "受付備考", "運転者指示事項", "コンテナ種類CD", 
                                       "コンテナ種類", "設置・引揚", "台数", "品名CD", "品名", 
                                       "数量", "単位" };
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                HaishaMeisaiDTOClass csvDto;
                for (int i = 0; i < csvHead.Length; i++)
                {
                    csvDT.Columns.Add(csvHead[i]);
                }
                for (int i = 0; i < this.SearchDetailResult.Length; i++)
                {
                    rowTmp = csvDT.NewRow();
                    csvDto = this.SearchDetailResult[i];
                    rowTmp["作業日"] = csvDto.SAGYOU_DATE.Substring(0, 10);
                    rowTmp["運転者"] = csvDto.UNTENSHA;
                    rowTmp["NO"] = (i + 1).ToString();
                    rowTmp["収集マニ情報"] = csvDto.MANIFEST_SHURUI_NAME;
                    rowTmp["マニ手配"] = csvDto.MANIFEST_TEHAI_NAME;
                    rowTmp["受付番号"] = csvDto.UKETSUKE_NUMBER;
                    rowTmp["現着時間名"] = csvDto.GENCHAKU_TIME_NAME;
                    rowTmp["現着時間"] = csvDto.GENCHAKU_TIME;
                    rowTmp["作業時間"] = csvDto.SAGYOU_DATE;
                    rowTmp["業者CD"] = csvDto.GYOUSHA_CD;
                    rowTmp["業者名"] = csvDto.GYOUSHA_NAME;
                    rowTmp["車種CD"] = csvDto.SHASHU_CD;
                    rowTmp["車種名"] = csvDto.SHASHU_NAME;
                    rowTmp["車輌CD"] = csvDto.SHARYOU_CD;
                    rowTmp["車輌名"] = csvDto.SHARYOU_NAME;
                    rowTmp["現場CD"] = csvDto.GENBA_CD;
                    rowTmp["現場名"] = csvDto.GENBA_NAME;
                    rowTmp["荷降先・荷積先CD"] = csvDto.NIOROSHI_NIZUMI_CD;
                    rowTmp["荷降先・荷積先"] = csvDto.NIOROSHI_NIZUMI_NAME;
                    rowTmp["現場住所"] = csvDto.GENBA_ADDRESS;
                    rowTmp["荷降先・荷積先住所"] = csvDto.NIOROSHI_NIZUMI_ADDRESS;
                    rowTmp["現場電話番号"] = csvDto.GENBA_TEL;
                    rowTmp["担当者名"] = csvDto.TANTOUSHA;
                    rowTmp["担当者携帯番号"] = csvDto.GENBA_KEITAI_TEL;
                    rowTmp["受付備考"] = csvDto.UKETSUKE_BIKOU1 + csvDto.UKETSUKE_BIKOU2 + csvDto.UKETSUKE_BIKOU3;
                    rowTmp["運転者指示事項"] = csvDto.UNTENSHA_SIJIJIKOU1 + csvDto.UNTENSHA_SIJIJIKOU2 + csvDto.UNTENSHA_SIJIJIKOU3;
                    rowTmp["コンテナ種類CD"] = csvDto.CONTENA_SHURUI_CD;
                    rowTmp["コンテナ種類"] = csvDto.CONTENA_SHURUI_NAME;
                    rowTmp["設置・引揚"] = csvDto.CONTENA_JOUKYOU_NAME;
                    rowTmp["台数"] = (csvDto.DAISUU == null ? string.Empty :
                                                               decimal.Parse(csvDto.DAISUU).ToString("N").Replace(".00", "") + "台");
                    rowTmp["品名CD"] = csvDto.HINMEI_CD;
                    rowTmp["品名"] = csvDto.HINMEI_NAME;
                    rowTmp["数量"] = (csvDto.SUURYOU == null ? string.Empty :
                                                               decimal.Parse(csvDto.SUURYOU).ToString("N").Replace(".00", ""));
                    rowTmp["単位"] = csvDto.UNIT_NAME;
                    csvDT.Rows.Add(rowTmp);
                }

                this.form.customDataGridView1.DataSource = csvDT;
                // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
                if (this.form.customDataGridView1.Rows.Count == 0)
                {
                    msgLogic.MessageBoxShow("E044");
                    return;
                }
                // 出力先指定のポップアップを表示させる。
                //if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HAISHA_MEISAI), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 「Ｆ9実行ボタン」イベント
        //2014/01/15 削除 qiao start
        ///// <summary>
        ///// 「Ｆ9実行ボタン」イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void bt_func9_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        if (this.form.RegistErrorFlag)
        //        {
        //            return;
        //        }

        //        //運転者範囲チェック
        //        if (!isOKCheck())
        //        {
        //            return;
        //        }
        //        else if (msgLogic.MessageBoxShow("C046", "実行") == DialogResult.Yes)
        //        {
        //            if (Search() == 0)
        //            {
        //                //「F5印刷ボタン」初期状態では非活性にする
        //                parentForm.bt_func5.Enabled = false;
        //                // 「F6CSV出力ボタン」初期状態では非活性にする
        //                parentForm.bt_func6.Enabled = false;
        //                return;
        //            }
        //            else
        //            {
        //                //「F5印刷ボタン」初期状態ではアクティブにする
        //                parentForm.bt_func5.Enabled = true;
        //                // 「F6CSV出力ボタン」初期状態ではアクティブにする
        //                parentForm.bt_func6.Enabled = true;
        //                // メッセージ通知
        //                msgLogic.MessageBoxShow("I001", "実行処理");
        //            }
        //        }
        //        else
        //        {
        //            //「F5印刷ボタン」初期状態では非活性にする
        //            parentForm.bt_func5.Enabled = false;
        //            // 「F6CSV出力ボタン」初期状態では非活性にする
        //            parentForm.bt_func6.Enabled = false;
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("bt_func9_Click", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}
        //2014/01/15 削除 qiao end
        #endregion

        #region 実行
        //2014/01/15 追加 qiao start
        /// <summary>
        /// 「F5印刷」または「F6CSV」押した、データを出力する
        /// </summary>
        private bool dataGet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.RegistErrorFlag)
                {
                    return false;
                }

                //運転者範囲チェック
                if (!isOKCheck())
                {
                    return false;
                }
                //else if (msgLogic.MessageBoxShow("C046", "実行") == DialogResult.Yes)
                else
                {
                    if (Search() == 0)
                    {
                        //データが０件の場合
                        return false;
                    }
                    else
                    {
                        // メッセージ通知
                        //msgLogic.MessageBoxShow("I001", "実行処理");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dataGet", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        //2014/01/15 追加 qiao end
        #endregion

        #region 「Ｆ12 閉じるボタン」イベント
        /// <summary>
        /// 「Ｆ12 閉じるボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
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

        #region 運転者 ポップアップ
        /// <summary>
        /// 運転者 ポップアップ初期化
        /// </summary>
        public void UntenshaPopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 運転者 ポップアップ取得
                // 表示用データ取得＆加工
                var untenshaJouDataTableFrom = this.GetUntenshaPopUpData(this.form.UNTENSHA_CD_FROM.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // TableNameを設定すれば、ポップアップのタイトルになる
                untenshaJouDataTableFrom.TableName = "運転者名称情報";

                // 運転者textbox列名とデータソース設定
                this.form.UNTENSHA_CD_FROM.PopupDataHeaderTitle = new string[] { "運転者CD", "運転者名称" };
                this.form.UNTENSHA_CD_FROM.PopupDataSource = untenshaJouDataTableFrom;


                var untenshaJouDataTableTo = this.GetUntenshaPopUpData(this.form.UNTENSHA_CD_TO.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // TableNameを設定すれば、ポップアップのタイトルになる
                untenshaJouDataTableTo.TableName = "運転者名称情報";

                // 運転者textbox列名とデータソース設定
                this.form.UNTENSHA_CD_TO.PopupDataHeaderTitle = new string[] { "運転者CD", "運転者名称" };
                this.form.UNTENSHA_CD_TO.PopupDataSource = untenshaJouDataTableTo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UntenshaPopUpDataInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 運転者マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetUntenshaPopUpData(IEnumerable<string> displayCol)
        {
            try
            {
                LogUtility.DebugMethodStart(displayCol);
                M_SHAIN[] shaiinAll;
                shaiinAll = DaoInitUtility.GetComponent<UntenshaNameDAOClass>().GetAllValidData(new M_SHAIN());
                this.untenshaAll = shaiinAll;
                if (displayCol.Any(s => s.Length == 0))
                {
                    LogUtility.DebugMethodEnd(displayCol);
                    return new DataTable();
                }

                var dt = EntityUtility.EntityToDataTable(shaiinAll);
                if (dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(displayCol);
                    return dt;
                }

                var sortedDt = new DataTable();
                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
                return sortedDt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetUntenshaPopUpData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 運転者を取得します
        /// </summary>
        /// <returns>運転者リスト</returns>
        private List<M_UNTENSHA> GetUntensha()
        {
            LogUtility.DebugMethodStart();

            var ret = new List<M_UNTENSHA>();

            var dao = DaoInitUtility.GetComponent<IM_UNTENSHADao>();
            ret = dao.GetAllValidData(new M_UNTENSHA()).ToList();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region 自動生成（実装なし）
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

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                this.form.HIDUKE_TO.IsInputErrorOccured = true;
                this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShow("E030", errorMsg);
                this.form.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        // 作業日のダブルクリック
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.form.HIDUKE_FROM;
            var hidukeToTextBox = this.form.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 運転者のダブルクリック
        private void UNTENSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var untenshaCDFromTextBox = this.form.UNTENSHA_CD_FROM;
            var untenshaCDToTextBox = this.form.UNTENSHA_CD_TO;
            var untenshaNameFromTextBox = this.form.UNTENSHA_NAME_FROM;
            var untenshaNameToTextBox = this.form.UNTENSHA_NAME_TO;
            untenshaCDToTextBox.Text = untenshaCDFromTextBox.Text;
            untenshaNameToTextBox.Text = untenshaNameFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion
    }
}
