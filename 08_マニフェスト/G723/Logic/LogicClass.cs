using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.ManifestSuiihyoData
{
    /// <summary>
    /// マニフェスト推移表ロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// マニフェスト推移表画面クラス
        /// </summary>
        private UIForm form;

        /// <summary>
        /// マニフェスト推移表Dtoを取得・設定します
        /// </summary>
        internal ManifestSuiihyoDto ManifestSuiihyoDto { get; set; }

        /// <summary>
        /// マニフェストデータテーブルを取得・設定します
        /// </summary>
        internal DataTable ManifestDataTable { get; set; }

        /// <summary>
        /// DBサーバ日付取得用のDao
        /// </summary>
        private GET_SYSDATEDao dao;

        /// <summary>
        /// メッセージ出力用
        /// </summary>
        private MessageBoxShowLogic MsgBox;

        /// <summary>
        /// 自社情報入力のDao
        /// </summary>
        private IM_CORP_INFODao daoCorpInfo;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 自社情報入力のDto
        /// </summary>
        private M_CORP_INFO[] entitys;

        /// <summary>
        /// SQLに渡す抽出用の年月
        /// </summary>
        private string strSelectDate;

        /// <summary>
        /// SQLに渡す項目用の年月
        /// </summary>
        private string piv;

        /// <summary>
        /// CSVの項目用の年月
        /// </summary>
        private ArrayList arrayPivot;

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        internal M_SYS_INFO mSysInfo;

        /// <summary>
        /// 単位マスタのDao
        /// </summary>
        private IM_UNITDao daoUnit;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            this.MsgBox = new MessageBoxShowLogic();
            // システム設定のデータを取得
            var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

            daoUnit = DaoInitUtility.GetComponent<IM_UNITDao>();

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

                // ボタン初期化
                this.ButtonInit();
                
                // イベント初期化
                this.EventInit();

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                // 親フォーム情報を取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 日付設定
                this.form.DATE_SHURUI.Text = "1";

                // 日付範囲設定
                this.daoCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                this.entitys = daoCorpInfo.GetAllData();
                this.form.DATE_FROM.Text = this.parentForm.sysDate.Year + "/" + ((int)this.entitys[0].KISHU_MONTH).ToString("00") + "/01";
                this.form.DATE_TO.Text = DateTime.Parse(this.form.DATE_FROM.Text).AddMonths(12).AddDays(-1.0).ToString("yyyy/MM/dd");
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
            // 日付
            LocationAdjustmentForManiLite(this.form.label16);
            LocationAdjustmentForManiLite(this.form.DATE_SHURUI);
            LocationAdjustmentForManiLite(this.form.customPanel4);

            // 日付範囲
            LocationAdjustmentForManiLite(this.form.label7);
            LocationAdjustmentForManiLite(this.form.DATE_FROM);
            LocationAdjustmentForManiLite(this.form.label4);
            LocationAdjustmentForManiLite(this.form.DATE_TO);

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
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            long monthDiff = 0;
            this.strSelectDate = string.Empty;
            this.piv = string.Empty;

            MessageBoxShowLogic msglogic = new MessageBoxShowLogic();

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_FROM.IsInputErrorOccured = true;
                this.form.DATE_TO.IsInputErrorOccured = true;
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;

                if (this.form.DATE_SHURUI_1.Checked)
                {
                    string[] errorMsg = { "交付年月日From", "交付年月日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SHURUI_2.Checked)
                {
                    string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SHURUI_3.Checked)
                {
                    string[] errorMsg = { "処分終了日From", "処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SHURUI_4.Checked)
                {
                    string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else
                {
                    string[] errorMsg = { "入力日付From", "入力日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.form.DATE_FROM.Focus();
                return false;
            }

            // 12か月を上回っている場合
            DateTime fromBeginMonth = DateTime.Parse(date_from.Year.ToString() + "/" + date_from.Month.ToString("00") + "/01");
            DateTime toBeginMonth = DateTime.Parse(date_to.Year.ToString() + "/" + date_to.Month.ToString("00") + "/01");

            monthDiff = DateAndTime.DateDiff(DateInterval.Month, fromBeginMonth, toBeginMonth);

            if (monthDiff > 11)
            {
                string strMsg = "12カ月を超える日付範囲は指定出来ません。";
                MessageBox.Show(strMsg, "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.form.DATE_TO.Focus();
                return false;
            }
            else
            {
                this.piv = "\"";
                this.arrayPivot = new ArrayList();
                // 12カ月分の文字列を作成
                for (int i = 0; i < monthDiff + 1; i++)
                {
                    DateTime dt = date_from.AddMonths(i);
                    if (i == 0)
                    {
                        this.piv = this.piv + dt.Year + "/" + dt.Month.ToString("00") + "\"";
                    }
                    else
                    {
                        this.piv = this.piv + ", \"" + dt.Year + "/" + dt.Month.ToString("00") + "\"";
                    }
                    arrayPivot.Add(dt.Year + "/" + dt.Month.ToString("00"));
                    this.strSelectDate += "CONVERT(MONEY, ISNULL(\"" + dt.Year + "/" + dt.Month.ToString("00")
                                  + "\",0)) " + "\"" + dt.Year + "/" + dt.Month.ToString("00") + "\",";
                }

                this.strSelectDate = this.strSelectDate.TrimEnd(',');
                return true;
            }
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

                this.SetManifestSuiihyoDto();

                var dao = DaoInitUtility.GetComponent<IManifestSuiihyoDao>();
                this.ManifestDataTable = dao.GetManifestData(this.ManifestSuiihyoDto);

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
        /// マニフェスト推移表作成
        /// </summary>
        internal bool CreateManifestSuiihyo()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.ManifestDataTable != null && 0 < this.ManifestDataTable.Rows.Count)
                {
                    var reportLogic = new ManifestSuiihyouReportClass();
                    reportLogic.CreateReport(this.ManifestDataTable, this.ManifestSuiihyoDto);
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateManifestSuiihyo", ex);
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
        /// 検索条件用Dtoに画面の値を設定
        /// </summary>
        private void SetManifestSuiihyoDto()
        {
            this.ManifestSuiihyoDto = new ManifestSuiihyoDto();

            // 集計パターンの値
            this.ManifestSuiihyoDto.Pattern = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            // 画面項目の値
            this.ManifestSuiihyoDto.KyotenCd = Int32.Parse(this.form.KYOTEN_CD.Text);
            this.ManifestSuiihyoDto.KyotenName = this.form.KYOTEN_NAME.Text;
            this.ManifestSuiihyoDto.IsKamiMani = this.form.KBN_KAMI_MANI.Checked;
            this.ManifestSuiihyoDto.IsDenMani = this.form.KBN_DEN_MANI.Checked;
            this.ManifestSuiihyoDto.IchijiNijiKbn = Int32.Parse(this.form.ICHIJI_NIJI_KBN.Text);
            if (this.form.DATE_SHURUI.Text.Equals("1"))
            {
                if (this.form.DATE_FROM.Value != null)
                {
                    this.ManifestSuiihyoDto.KofuDateFrom = ((DateTime)this.form.DATE_FROM.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.KofuDateFrom = null;
                }
                if (this.form.DATE_TO.Value != null)
                {
                    this.ManifestSuiihyoDto.KofuDateTo = ((DateTime)this.form.DATE_TO.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.KofuDateTo = null;
                }
            }
            else if (this.form.DATE_SHURUI.Text.Equals("2"))
            {
                if (this.form.DATE_FROM.Value != null)
                {
                    this.ManifestSuiihyoDto.UnpanEndDateFrom = ((DateTime)this.form.DATE_FROM.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.UnpanEndDateFrom = null;
                }
                if (this.form.DATE_TO.Value != null)
                {
                    this.ManifestSuiihyoDto.UnpanEndDateTo = ((DateTime)this.form.DATE_TO.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.UnpanEndDateTo = null;
                }
            }
            else if (this.form.DATE_SHURUI.Text.Equals("3"))
            {
                if (this.form.DATE_FROM.Value != null)
                {
                    this.ManifestSuiihyoDto.ShobunEndDateFrom = ((DateTime)this.form.DATE_FROM.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.ShobunEndDateFrom = null;
                }
                if (this.form.DATE_TO.Value != null)
                {
                    this.ManifestSuiihyoDto.ShobunEndDateTo = ((DateTime)this.form.DATE_TO.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.ShobunEndDateTo = null;
                }
            }
            else if (this.form.DATE_SHURUI.Text.Equals("4"))
            {
                if (this.form.DATE_FROM.Value != null)
                {
                    this.ManifestSuiihyoDto.LastShobunEndDateFrom = ((DateTime)this.form.DATE_FROM.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.LastShobunEndDateFrom = null;
                }
                if (this.form.DATE_TO.Value != null)
                {
                    this.ManifestSuiihyoDto.LastShobunEndDateTo = ((DateTime)this.form.DATE_TO.Value).ToString("yyyyMMdd");
                }
                else
                {
                    this.ManifestSuiihyoDto.LastShobunEndDateTo = null;
                }
            }
            this.ManifestSuiihyoDto.HaishutsuJigyoushaCdFrom = this.form.HAISHUTSU_GYOUSHA_CD_FROM.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoushaNameFrom = this.form.HAISHUTSU_GYOUSHA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoushaCdTo = this.form.HAISHUTSU_GYOUSHA_CD_TO.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoushaNameTo = this.form.HAISHUTSU_GYOUSHA_NAME_TO.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoujouCdFrom = this.form.HAISHUTSU_GENBA_CD_FROM.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoujouNameFrom = this.form.HAISHUTSU_GENBA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoujouCdTo = this.form.HAISHUTSU_GENBA_CD_TO.Text;
            this.ManifestSuiihyoDto.HaishutsuJigyoujouNameTo = this.form.HAISHUTSU_GENBA_NAME_TO.Text;
            this.ManifestSuiihyoDto.UnpanJutakushaCdFrom = this.form.UNPAN_GYOUSHA_CD_FROM.Text;
            this.ManifestSuiihyoDto.UnpanJutakushaNameFrom = this.form.UNPAN_GYOUSHA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.UnpanJutakushaCdTo = this.form.UNPAN_GYOUSHA_CD_TO.Text;
            this.ManifestSuiihyoDto.UnpanJutakushaNameTo = this.form.UNPAN_GYOUSHA_NAME_TO.Text;
            this.ManifestSuiihyoDto.ShobunJigyoushaCdFrom = this.form.SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestSuiihyoDto.ShobunJigyoushaNameFrom = this.form.SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.ShobunJigyoushaCdTo = this.form.SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestSuiihyoDto.ShobunJigyoushaNameTo = this.form.SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestSuiihyoDto.ShobunJigyoujouCdFrom = this.form.SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestSuiihyoDto.ShobunJigyoujouNameFrom = this.form.SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.ShobunJigyoujouCdTo = this.form.SHOBUN_GENBA_CD_TO.Text;
            this.ManifestSuiihyoDto.ShobunJigyoujouNameTo = this.form.SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoushaCdFrom = this.form.LAST_SHOBUN_GYOUSHA_CD_FROM.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoushaNameFrom = this.form.LAST_SHOBUN_GYOUSHA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoushaCdTo = this.form.LAST_SHOBUN_GYOUSHA_CD_TO.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoushaNameTo = this.form.LAST_SHOBUN_GYOUSHA_NAME_TO.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoujouCdFrom = this.form.LAST_SHOBUN_GENBA_CD_FROM.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoujouNameFrom = this.form.LAST_SHOBUN_GENBA_NAME_FROM.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoujouCdTo = this.form.LAST_SHOBUN_GENBA_CD_TO.Text;
            this.ManifestSuiihyoDto.LastShobunJigyoujouNameTo = this.form.LAST_SHOBUN_GENBA_NAME_TO.Text;
            this.ManifestSuiihyoDto.HoukokushoBunruiCdFrom = this.form.HOUKOKUSHO_CD_FROM.Text;
            this.ManifestSuiihyoDto.HoukokushoBunruiNameFrom = this.form.HOUKOKUSHO_NAME_FROM.Text;
            this.ManifestSuiihyoDto.HoukokushoBunruiCdTo = this.form.HOUKOKUSHO_CD_TO.Text;
            this.ManifestSuiihyoDto.HoukokushoBunruiNameTo = this.form.HOUKOKUSHO_NAME_TO.Text;
            this.ManifestSuiihyoDto.HaikibutsuMeishouCdFrom = this.form.HAIKIBUTSU_CD_FROM.Text;
            this.ManifestSuiihyoDto.HaikibutsuMeishouNameFrom = this.form.HAIKIBUTSU_NAME_FROM.Text;
            this.ManifestSuiihyoDto.HaikibutsuMeishouCdTo = this.form.HAIKIBUTSU_CD_TO.Text;
            this.ManifestSuiihyoDto.HaikibutsuMeishouNameTo = this.form.HAIKIBUTSU_NAME_TO.Text;
            this.ManifestSuiihyoDto.ShobunHouhouCdFrom = this.form.SHOBUN_HOUHOU_CD_FROM.Text;
            this.ManifestSuiihyoDto.ShobunHouhouNameFrom = this.form.SHOBUN_HOUHOU_NAME_FROM.Text;
            this.ManifestSuiihyoDto.ShobunHouhouCdTo = this.form.SHOBUN_HOUHOU_CD_TO.Text;
            this.ManifestSuiihyoDto.ShobunHouhouNameTo = this.form.SHOBUN_HOUHOU_NAME_TO.Text;
            this.ManifestSuiihyoDto.TorihikisakiCdFrom = this.form.TORIHIKISAKI_CD_FROM.Text;
            this.ManifestSuiihyoDto.TorihikisakiNameFrom = this.form.TORIHIKISAKI_NAME_FROM.Text;
            this.ManifestSuiihyoDto.TorihikisakiCdTo = this.form.TORIHIKISAKI_CD_TO.Text;
            this.ManifestSuiihyoDto.TorihikisakiNameTo = this.form.TORIHIKISAKI_NAME_TO.Text;

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
                this.ManifestSuiihyoDto.SelectedColumnUnpanJutakushaCd = true;
            }
            else
            {
                this.ManifestSuiihyoDto.SelectedColumnUnpanJutakushaCd = false;
            }

            if (ConstClass.UPN_SAKI_GENBA_CD == cd1
                || ConstClass.UPN_SAKI_GENBA_CD == cd2
                || ConstClass.UPN_SAKI_GENBA_CD == cd3
                || ConstClass.UPN_SAKI_GENBA_CD == cd4
                || ConstClass.UPN_SAKI_GENBA_CD == cd5
                || ConstClass.UPN_SAKI_GENBA_CD == cd6
                || ConstClass.UPN_SAKI_GENBA_CD == cd7)
            {
                this.ManifestSuiihyoDto.SelectedColumnShobunJigyoujouCd = true;
            }
            else
            {
                this.ManifestSuiihyoDto.SelectedColumnShobunJigyoujouCd = false;
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

            // 先頭のカンマは削除する。
            string str = sb.ToString().Remove(0, 1);

            this.ManifestSuiihyoDto.GroupColumn = str;

            // SELECT句の項目名
            var dto = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            List<S_LIST_COLUMN_SELECT_DETAIL> list = dto.ColumnSelectDetailList;
            this.ManifestSuiihyoDto.Select = new ArrayList();
            foreach (S_LIST_COLUMN_SELECT_DETAIL detail in list)
            {
                this.ManifestSuiihyoDto.Select.Add(detail.BUTSURI_NAME);
            }
            // 条件１
            this.ManifestSuiihyoDto.Jyouken1 = CreateJouken1FieldData(this.ManifestSuiihyoDto);
            // 条件２
            this.ManifestSuiihyoDto.Jyouken2 = CreateJouken2FieldData();

            // マニフェスト数量桁数
            string format = this.mSysInfo.MANIFEST_SUURYO_FORMAT;
            int ketasuu = 0;
            if (format.IndexOf(".") > 0)
            {
                ketasuu = format.Length - format.IndexOf(".") - 1;
            }
            if (ketasuu > 0)
            {
                this.ManifestSuiihyoDto.ManiKetasu = ketasuu;
            }
            else
            {
                this.ManifestSuiihyoDto.ManiKetasu = 0;
            }

            // 抽出・集計処理用の値
            this.ManifestSuiihyoDto.SelectDate = this.strSelectDate;
            this.ManifestSuiihyoDto.Pivot = this.piv;
            this.ManifestSuiihyoDto.ArrayPivot = this.arrayPivot;
            this.ManifestSuiihyoDto.format = format;
            this.ManifestSuiihyoDto.MonthCount = this.arrayPivot.Count;

            // 合計の単位をシステム設定から取得する。
            int maniKansanKihonUnitCd = int.Parse(this.mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.ToString());
            M_UNIT mUnit = this.daoUnit.GetDataByCd(maniKansanKihonUnitCd);
            if (mUnit != null)
            {
                this.ManifestSuiihyoDto.maniUnitName = mUnit.UNIT_NAME;
            }
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
        /// レポート鏡の抽出条件文字列を作成（左側）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string CreateJouken1FieldData(ManifestSuiihyoDto dto)
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

            sb.Append(CreateJouken1Date(this.form.DATE_FROM, this.form.DATE_TO, this.form.DATE_SHURUI.Text));

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
        /// <param name="dateShurui">日付種類</param>
        /// <returns></returns>
        private string CreateJouken1Date(CustomDateTimePicker from, CustomDateTimePicker to, string dateShurui)
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

            string koumokuName = string.Empty;
            if (dateShurui.Equals("1"))
            {
                koumokuName = "交付年月日";
            }
            else if (dateShurui.Equals("2"))
            {
                koumokuName = "運搬終了日";
            }
            else if (dateShurui.Equals("3"))
            {
                koumokuName = "処分終了日";
            }
            else if (dateShurui.Equals("4"))
            {
                koumokuName = "最終処分終了日";
            }

            return string.Format("　[{0}] {1}{2}", koumokuName, sb.ToString(), Environment.NewLine);
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

            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);

            return sb.ToString();
        }

        /// <summary>
        /// 項目論理名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns></returns>
        private string GetKoumokuRonriName(int shuukeiKoumokuNo)
        {
            if (this.ManifestSuiihyoDto == null || this.ManifestSuiihyoDto.Pattern == null)
            {
                return string.Empty;
            }

            var select = this.ManifestSuiihyoDto.Pattern.GetColumnSelect(shuukeiKoumokuNo);
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
            if (this.ManifestSuiihyoDto == null || this.ManifestSuiihyoDto.Pattern == null)
            {
                return string.Empty;
            }

            var select = this.ManifestSuiihyoDto.Pattern.GetColumnSelectDetail(shuukeiKoumokuNo);
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

        /// <summary>
        /// DBサーバの日付取得
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// CSVデータ作成
        /// </summary>
        /// <param name="maniDt"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public DataTable CreateCSVDataDt(DataTable maniDt, ManifestSuiihyoDto dto)
        {
            DataTable retDt = new DataTable();

            for (int i = 0; i < dto.Select.Count; i++)
            {
                string ronriName = this.GetKoumokuRonriName(i + 1);
                retDt.Columns.Add(ronriName + "CD");
                retDt.Columns.Add(ronriName);
            }

            for (int i = 0; i < dto.ArrayPivot.Count; i++)
            {
                retDt.Columns.Add(dto.ArrayPivot[i].ToString());
            }

            retDt.Columns.Add("合計(" + dto.maniUnitName + ")");
            retDt.Columns.Add("月平均");

            // 端数桁を設定
            string format = this.mSysInfo.MANIFEST_SUURYO_FORMAT;

            foreach (DataRow row in maniDt.Rows)
            {
                DataRow dr = retDt.NewRow();

                // 集計項目
                for (int x = 0; x < dto.Select.Count; x++)
                {
                    string ronriName = this.GetKoumokuRonriName(x + 1);
                    dr[ronriName + "CD"] = row[dto.Select[x].ToString()];
                    dr[ronriName] = row[dto.Select[x].ToString() + "_NAME"];
                }

                // 月別の値
                for (int i = 0; i < 12; i++)
                {
                    if (dto.ArrayPivot.Count > i)
                    {
                        dr[dto.ArrayPivot[i].ToString()] = Convert.ToDecimal(row[dto.ArrayPivot[i].ToString()]).ToString(format);
                    }
                }

                dr["合計(" + dto.maniUnitName + ")"] = Convert.ToDecimal(row["SUM_KANSANGO_SURYO"]).ToString(format);
                dr["月平均"] = Convert.ToDecimal(row["AVR"]).ToString(format);

                retDt.Rows.Add(dr);
            }

            return retDt;
        }

        #region 使用しない

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
