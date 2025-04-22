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
using System.IO;
using System.Windows.Forms;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using GrapeCity.Win.MultiRow;
using System.Drawing;
using Microsoft.VisualBasic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran.Setting.ButtonSetting.xml";
        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;
        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;
        /// <summary>
        /// Message
        /// </summary>
        private MessageBoxShowLogic messageShowLogic;
        /// <summary>
        /// 帳票情報Header部データ
        /// </summary>
        private DataTable mReportHeaderInfo;
        /// <summary>
        ///  帳票情報Detail部データ
        /// </summary>
        private DataTable mReportDetailInfo;
        /// <summary>
        ///  帳票情報Footer部データ
        /// </summary>
        private DataTable mReportFooterInfo;
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        public string chiiki_name = string.Empty;

        public string Tag = string.Empty;
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件 DTO
        /// </summary>
        public DTOClass searchDtoCondition { get; set; }
        /// <summary>
        /// 総データ検索結果
        /// </summary>
        public DataTable searchAllIchiranDetailData { get; set; }
        /// <summary>
        /// 事業場について画面表示検索結果
        /// </summary>
        public DataTable searchIchiranDetailData { get; set; }
        /// <summary>
        /// 帳票情報を保持するプロパティ
        /// </summary>
        public ReportInfoR394 ReportInfo { get; set; }
        /// <summary>
        /// 外部からの受け渡しデーターテーブルリストを保持するプロパティ
        /// </summary>
        public Dictionary<string, object> ReportInfoList { get; set; }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            messageShowLogic = new MessageBoxShowLogic();
            // システム情報Dao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダー項目
                this.headForm = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;
                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);
                // イベントの初期化処理
                this.EventInit(parentForm);
                // システム情報を取得
                this.GetSysInfoInit();
                // システム設定による可変の表示項目を適用
                this.SetDispTextFromSysInfo();

                this.searchAllIchiranDetailData = (DataTable)this.ReportInfoList["SearchResult"];
                //画面ヘッダ初期化処理

                if (this.ReportInfoList["Genbashukei_Kbn"].ToString().Equals("2"))
                {
                    // 現場一括集計無
                    this.InitHeard(0);
                }
                else
                {
                    // 現場一括集計有
                    this.InitHeard_GenbaIkkatsu(0);
                }
                //詳細情報編集
                // this.EditDetailData();
                this.EditChokugyouDetailData();
                //画面詳細初期化処理
                this.InitDetail();

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("例外エラーが発生しました。", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        ///  システム設定による可変の表示項目を適用
        /// </summary>
        internal void SetDispTextFromSysInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 換算後単位はシステム設定の値によって変更。文言として「排出量(t)」が固定ではない
                short kansanKihonUnitCd = this.sysInfoEntity.MANI_KANSAN_KIHON_UNIT_CD.Value;

                IM_UNITDao unitDao;
                unitDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();

                M_UNIT mUnit = unitDao.GetDataByCd(kansanKihonUnitCd);
                if (mUnit != null)
                {
                    this.form.label1.Text = "排出量合計(" + mUnit.UNIT_NAME + ")";
                    this.form.IchiranGRD.Columns["HAISHU_RYOU"].HeaderText = "排出量(" + mUnit.UNIT_NAME + ")";
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("システム設定値の適用で例外エラーが発生しました。", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
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
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
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

        ///<summary>
        ///イベントの初期化処理
        ///</summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 前ボタン(F1)イベント
                parentForm.bt_func1.Click += new EventHandler(this.form.previousNumber_Click);
                // 後ボタン(F2)イベント
                parentForm.bt_func2.Click += new EventHandler(this.form.nextNumber_Click);

                // 20140709 chinchisi EV004844_交付状況報告書にてCSV出力機能がない start
                //F6 CSV出力
                parentForm.bt_func6.Click += new System.EventHandler(this.form.bt_func6_Click);
                // 20140709 chinchisi EV004844_交付状況報告書にてCSV出力機能がない end

                // 個別印刷(F4)イベント
                parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

                // 印刷ボタン(F5)イベント
                parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);

                //// 検索ボタン(F8)イベント生成
                //parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);
                parentForm.ProcessButtonPanel.Visible = false;
                parentForm.txb_process.Enabled = false;
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

        ///<summary>
        ///ヘッダ情報初期化処理
        ///</summary>
        private void InitHeard(int count)
        {
            DataTable pSearchResult = this.searchAllIchiranDetailData;

            //情報GROUP BY
            var dtGroupInfo = pSearchResult.AsEnumerable().GroupBy(r => r.Field<string>("HST_GYOUSHAGENBA"),
                          (k, g) => new
                          {
                              HST_GYOUSHAGENBA = k,
                              GOV_OR_MAY_NAME = g.First().Field<string>("GOV_OR_MAY_NAME"),
                              CHIIKI_NAME_RYAKU = g.First().Field<string>("CHIIKI_NAME_RYAKU"),
                              GYOUSHA_ADDRESS1 = g.First().Field<string>("TODOUFUKEN_NAME") + g.First().Field<string>("GYOUSHA_ADDRESS1"),
                              GYOUSHA_ADDRESS2 = g.First().Field<string>("GYOUSHA_ADDRESS2"),
                              GYOUSHA_NAME1 = g.First().Field<string>("GYOUSHA_NAME1"),
                              GYOUSHA_NAME2 = g.First().Field<string>("GYOUSHA_NAME2"),
                              DAIHYOUSHA = g.First().Field<string>("DAIHYOUSHA"),
                              GYOUSHA_TEL = g.First().Field<string>("GYOUSHA_TEL"),
                              TITLE1 = g.First().Field<string>("TITLE1"),
                              TITLE2 = g.First().Field<string>("TITLE2"),
                              JGB_NAME = g.First().Field<string>("JGB_NAME"),
                              JGB_ADDRESS = g.First().Field<string>("JGB_ADDRESS"),
                              GYOUSHU_CD = g.First().Field<string>("GYOUSHU_CD"),
                              GYOUSHU_NAME = g.First().Field<string>("GYOUSHU_NAME"),
                              GENBA_TEL = g.First().Field<string>("GENBA_TEL"),
                          }).ToList();

            this.Tag = dtGroupInfo.Count.ToString();
            this.form.IchiranGRD.Tag = string.Empty;
            if (dtGroupInfo.Count > count && count >= 0)
            {
                //事業場のCDIndex設定
                this.form.hiddenValue.Tag = dtGroupInfo.Count > 0 && dtGroupInfo[count].HST_GYOUSHAGENBA == null ? string.Empty : dtGroupInfo[count].HST_GYOUSHAGENBA.ToString();
                this.form.hiddenValue.Text = count.ToString();
                //if (!string.IsNullOrEmpty(this.form.hiddenValue.Tag.ToString()))
                //{
                //提出先
                this.form.txtTeishutusaki.Text = dtGroupInfo[count].GOV_OR_MAY_NAME;
                this.chiiki_name = dtGroupInfo[count].CHIIKI_NAME_RYAKU;
                //事業場の名称
                this.form.txtGenbaNm.Text = dtGroupInfo[count].JGB_NAME;
                //事業場の住所
                this.form.txtGenbaAddress.Text = dtGroupInfo[count].JGB_ADDRESS;
                //業種
                this.form.txtGYOUSHUCD.Text = dtGroupInfo[count].GYOUSHU_CD;
                this.form.txtGYOUSHUNM.Text = dtGroupInfo[count].GYOUSHU_NAME;
                //電話
                this.form.txtGenbaTel.Text = dtGroupInfo[count].GENBA_TEL;
                //作成日
                this.form.dtpCreadDate.Text = this.ReportInfoList["Cread_Date"].ToString();
                //報告者
                //住所
                this.form.txtADDRESS1.Text = dtGroupInfo[count].GYOUSHA_ADDRESS1;
                this.form.txtADDRESS2.Text = dtGroupInfo[count].GYOUSHA_ADDRESS2;

                //氏名
                this.form.txtName1.Text = dtGroupInfo[count].GYOUSHA_NAME1;
                this.form.txtName2.Text = dtGroupInfo[count].GYOUSHA_NAME2;
                //代表者
                this.form.txtDAIHYOUSHA.Text = dtGroupInfo[count].DAIHYOUSHA;
                //電話
                this.form.txtTel.Text = dtGroupInfo[count].GYOUSHA_TEL;
                //表題
                this.form.txtTitle1.Text = dtGroupInfo[count].TITLE1;
                this.form.txtTitle2.Text = dtGroupInfo[count].TITLE2;

                if (this.form.hiddenValue.Tag.ToString() == "")
                {
                    this.mReportDetailInfo = pSearchResult.Select(" HST_GYOUSHAGENBA IS NULL ").CopyToDataTable();
                }
                else
                {
                    this.mReportDetailInfo = pSearchResult.Select(" HST_GYOUSHAGENBA = '" + this.form.hiddenValue.Tag.ToString() + "'").CopyToDataTable();
                }

                //}

            }

            //次前ボタンの制御
            buttonControlPrevNext(count, dtGroupInfo.Count - 1);

        }

        ///<summary>
        ///ヘッダ情報初期化処理(現場一括有)
        ///</summary>
        private void InitHeard_GenbaIkkatsu(int count)
        {
            DataTable pSearchResult = this.searchAllIchiranDetailData;

            //情報GROUP BY
            var dtGroupInfo = (from t in pSearchResult.AsEnumerable()
                               group t by new { t = t.Field<string>("HST_GYOUSHAGENBA") } into m
                               select new
                               {
                                   HST_GYOUSHAGENBA = m.Key.t,
                                   GOV_OR_MAY_NAME = m.First().Field<string>("GOV_OR_MAY_NAME"),
                                   CHIIKI_NAME_RYAKU = m.First().Field<string>("CHIIKI_NAME_RYAKU"),
                                   GYOUSHA_ADDRESS1 = m.First().Field<string>("TODOUFUKEN_NAME") + m.First().Field<string>("GYOUSHA_ADDRESS1"),
                                   GYOUSHA_ADDRESS2 = m.First().Field<string>("GYOUSHA_ADDRESS2"),
                                   GYOUSHA_NAME1 = m.First().Field<string>("GYOUSHA_NAME1"),
                                   GYOUSHA_NAME2 = m.First().Field<string>("GYOUSHA_NAME2"),
                                   DAIHYOUSHA = m.First().Field<string>("DAIHYOUSHA"),
                                   GYOUSHA_TEL = m.First().Field<string>("GYOUSHA_TEL"),
                                   TITLE1 = m.First().Field<string>("TITLE1"),
                                   TITLE2 = m.First().Field<string>("TITLE2"),
                                   JGB_NAME = m.First().Field<string>("JGB_NAME"),
                                   JGB_ADDRESS = m.First().Field<string>("JGB_ADDRESS"),
                                   GYOUSHU_CD = m.First().Field<string>("GYOUSHU_CD"),
                                   GYOUSHU_NAME = m.First().Field<string>("GYOUSHU_NAME"),
                                   GENBA_TEL = m.First().Field<string>("GENBA_TEL"),
                               }).ToList();

            this.Tag = dtGroupInfo.Count.ToString();
            this.form.IchiranGRD.Tag = string.Empty;
            if (dtGroupInfo.Count > count && count >= 0)
            {
                //地域CDと事業者CDIndex設定
                this.form.hiddenValue.Tag = dtGroupInfo.Count > 0 && dtGroupInfo[count].HST_GYOUSHAGENBA == null ? string.Empty : dtGroupInfo[count].HST_GYOUSHAGENBA.ToString();
                this.form.hiddenValue.Text = count.ToString();
                //if (!string.IsNullOrEmpty(this.form.hiddenValue.Tag.ToString()))
                //{
                //提出先
                this.form.txtTeishutusaki.Text = dtGroupInfo[count].GOV_OR_MAY_NAME;
                this.chiiki_name = dtGroupInfo[count].CHIIKI_NAME_RYAKU;
                //事業場の名称
                this.form.txtGenbaNm.Text = dtGroupInfo[count].JGB_NAME;
                //事業場の住所
                this.form.txtGenbaAddress.Text = dtGroupInfo[count].JGB_ADDRESS;
                //業種
                this.form.txtGYOUSHUCD.Text = dtGroupInfo[count].GYOUSHU_CD;
                this.form.txtGYOUSHUNM.Text = dtGroupInfo[count].GYOUSHU_NAME;
                //電話
                this.form.txtGenbaTel.Text = dtGroupInfo[count].GENBA_TEL;
                //作成日
                this.form.dtpCreadDate.Text = this.ReportInfoList["Cread_Date"].ToString();
                //報告者
                //住所
                this.form.txtADDRESS1.Text = dtGroupInfo[count].GYOUSHA_ADDRESS1;
                this.form.txtADDRESS2.Text = dtGroupInfo[count].GYOUSHA_ADDRESS2;

                //氏名
                this.form.txtName1.Text = dtGroupInfo[count].GYOUSHA_NAME1;
                this.form.txtName2.Text = dtGroupInfo[count].GYOUSHA_NAME2;
                //代表者
                this.form.txtDAIHYOUSHA.Text = dtGroupInfo[count].DAIHYOUSHA;
                //電話
                this.form.txtTel.Text = dtGroupInfo[count].GYOUSHA_TEL;
                //表題
                this.form.txtTitle1.Text = dtGroupInfo[count].TITLE1;
                this.form.txtTitle2.Text = dtGroupInfo[count].TITLE2;

                if (this.form.hiddenValue.Tag.ToString() == "")
                {
                    this.mReportDetailInfo = pSearchResult.Select(" HST_GYOUSHAGENBA IS NULL ").CopyToDataTable();
                }
                else
                {
                    this.mReportDetailInfo = pSearchResult.Select(" HST_GYOUSHAGENBA = '" + this.form.hiddenValue.Tag.ToString() + "'").CopyToDataTable();
                }
            }

            //次前ボタンの制御
            buttonControlPrevNext(count, dtGroupInfo.Count - 1);

        }

        /// <summary>
        ///産廃(直行以外)データを編集
        /// </summary>
        private void EditChokugyouDetailData()
        {
            DataTable objData = this.mReportDetailInfo.Copy();

            #region 詳細データを作成する。
            DataTable newData = new DataTable();
            DataGridViewColumn dataCol = null;
            //カラム作成           
            for (int i = 0; i < this.form.IchiranGRD.Columns.Count; i++)
            {
                dataCol = this.form.IchiranGRD.Columns[i];

                if (dataCol.Name.Equals("ROW_NO"))
                {
                    newData.Columns.Add(dataCol.Name, typeof(int));
                }
                else
                {
                    newData.Columns.Add(dataCol.Name);
                }
            }

            newData.Columns.Add("HOUKOKUSHOBUNRUISORTKEY");
            newData.Columns.Add("UPN_GYOUSHA_CD");
            newData.Columns.Add("UPN_SAKI_GYOUSHA_CD");
            newData.Columns.Add("UPN_SAKI_GENBA_CD");
            newData.Columns.Add("SBN_GYOUSHA_CD");
            newData.Columns.Add("SBN_GENBA_CD");
            newData.Columns.Add("HAIKI_SHURUI_CD");
            newData.Columns.Add("CHOKKOU_ROUTE_NO");
            newData.Columns.Add("UPN_ROUTE_NO");
            newData.Columns.Add("SYSTEM_ID");
            newData.Columns.Add("HAIKI_KBN_CD");

            var resultByGYOUSHAGENBA = objData.AsEnumerable()
                    .GroupBy(
                        r => r.Field<string>("HST_GYOUSHAGENBA"),
                        (ym, ymGroup) => new
                        {
                            ym,
                            ymggGroups = ymGroup.GroupBy(
                                r2 => string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}",
                                    r2.Field<string>("HAIKI_SHURUI_NAME_RYAKU"),
                                    r2.Field<decimal>("HAISHU_RYOU"),
                                    r2.Field<Int32>("COUFUMAISUU"),
                                    r2.Field<string>("UPN_FUTSUU_KYOKA_NO"),
                                    r2.Field<string>("UPN_GYOUSHA_CD"),
                                    r2.Field<string>("UPN_GYOUSHA_NAME"),
                                    r2.Field<string>("UPN_SAKI_GYOUSHA_CD"),
                                    r2.Field<string>("UPN_SAKI_GENBA_CD"),
                                    r2.Field<string>("UPN_SAKI_GENBA_ADDRESS"),
                                    r2.Field<string>("SBN_FUTSUU_KYOKA_NO"),
                                    r2.Field<string>("SBN_GYOUSHA_CD"),
                                    r2.Field<string>("SBN_GYOUSHA_NAME"),
                                    r2.Field<string>("SBN_GENBA_CD"),
                                    r2.Field<string>("SBN_GENBA_ADDRESS"),
                                    r2.Field<string>("HAIKI_SHURUI_CD"),
                                    r2.Field<string>("CHOKKOU_ROUTE_NO"),
                                    (r2["UPN_ROUTE_NO"].GetType().Name == "Int16" ? r2.Field<Int16>("UPN_ROUTE_NO") : r2.Field<decimal>("UPN_ROUTE_NO")),
                                    r2.Field<Int64>("SYSTEM_ID"),
                                    r2.Field<Int32>("HOUKOKUSHOBUNRUISORTKEY"),
                                    r2.Field<Int16>("HAIKI_KBN_CD")),
                                (gg, ymggGroup) => new
                                {
                                    gg,
                                    ymggGroup
                                }
                            ).ToList()
                        }
                    ).ToList();


            int RowIndex = 0;
            // 業者住所1、業者住所2、事業場の名称より、ループ
            foreach (var grpYM in resultByGYOUSHAGENBA)
            {
                foreach (var grpYMGG in grpYM.ymggGroups)
                {
                    var grp = grpYMGG.ymggGroup;
                    int newRowcount = grp.Count();
                    //詳細情報を取得
                    string[] arrTmp = grpYMGG.gg.ToString().Split(',');
                    for (int i = 0; i < newRowcount; i++)
                    {
                        DataRow newRow = newData.NewRow();
                        newRow["ROW_NO"] = RowIndex;
                        newRow["HAIKI_SHURUI_NAME_RYAKU"] = arrTmp[0];
                        newRow["HAISHU_RYOU"] = arrTmp[1];
                        newRow["COUFUMAISUU"] = arrTmp[2];
                        newRow["UPN_FUTSUU_KYOKA_NO"] = arrTmp[3];
                        newRow["UPN_GYOUSHA_CD"] = arrTmp[4];
                        newRow["UPN_GYOUSHA_NAME"] = arrTmp[5];
                        newRow["UPN_SAKI_GYOUSHA_CD"] = arrTmp[6];
                        newRow["UPN_SAKI_GENBA_CD"] = arrTmp[7];
                        newRow["UPN_SAKI_GENBA_ADDRESS"] = arrTmp[8];
                        newRow["SBN_FUTSUU_KYOKA_NO"] = arrTmp[9];
                        newRow["SBN_GYOUSHA_CD"] = arrTmp[10];
                        newRow["SBN_GYOUSHA_NAME"] = arrTmp[11];
                        newRow["SBN_GENBA_CD"] = arrTmp[12];
                        newRow["SBN_GENBA_ADDRESS"] = arrTmp[13];
                        newRow["HAIKI_SHURUI_CD"] = arrTmp[14];
                        newRow["CHOKKOU_ROUTE_NO"] = arrTmp[15];
                        newRow["UPN_ROUTE_NO"] = arrTmp[16];
                        newRow["SYSTEM_ID"] = arrTmp[17];
                        newRow["HOUKOKUSHOBUNRUISORTKEY"] = arrTmp[18];
                        newRow["HAIKI_KBN_CD"] = arrTmp[19];
                        newData.Rows.Add(newRow);
                    }
                }
            }

            var resultByDetail = newData.AsEnumerable().GroupBy(
                        r2 => string.Format("{0},{1},{2}",
                            r2.Field<string>("SYSTEM_ID"),
                            r2.Field<string>("HAIKI_SHURUI_CD"),
                            r2.Field<string>("HAIKI_KBN_CD")),
                        (gg, ymggGroup) => new
                        {
                            gg,
                            ymggGroup
                        }
                    ).ToList();

            DataTable orgData = newData.Clone(); //最終代入用
            DataTable tempData = newData.Clone(); //途中代入用
            DataTable shuukeiData = new DataTable(); //交付枚数、排出量加算用

            shuukeiData.Columns.Add("NUMBER", typeof(int));
            shuukeiData.Columns.Add("SYSTEM_ID");
            shuukeiData.Columns.Add("HAIKI_SHURUI_CD");
            shuukeiData.Columns.Add("COUFUMAISUU");
            shuukeiData.Columns.Add("HAISHU_RYOU");
            shuukeiData.Columns.Add("HAIKI_KBN_CD", typeof(int));
            shuukeiData.Columns.Add("KEIJOU_END_KBN", typeof(Int16));

            RowIndex = 0;
            for (int i = 0; i < resultByDetail.Count; i++)
            {
                var grp = resultByDetail[i];
                int newRowcount = grp.ymggGroup.Count();
                //産業廃棄物の種類、運搬受託者名、運搬先の住所、処分受託者名、処分場所の住所を取得
                string[] arrTmp = grp.gg.ToString().Split(',');

                DataRow sysIdGrpRow = shuukeiData.NewRow();
                sysIdGrpRow["NUMBER"] = i;
                sysIdGrpRow["SYSTEM_ID"] = arrTmp[0];
                sysIdGrpRow["HAIKI_SHURUI_CD"] = arrTmp[1];
                sysIdGrpRow["HAIKI_KBN_CD"] = arrTmp[2];
                sysIdGrpRow["HAISHU_RYOU"] = 0;
                sysIdGrpRow["KEIJOU_END_KBN"] = 0;

                //抽出データから交付枚数を計上する
                var forCoufuMaisuuCntList = newData.AsEnumerable().Where(w => (
                                                                    (string)w["SYSTEM_ID"]) == (string)arrTmp[0]
                                                                    && (string)w["HAIKI_SHURUI_CD"] == (string)arrTmp[1]
                                                                    && (string)w["HAIKI_KBN_CD"] == (string)arrTmp[2]
                                                                ).ToList();
                int cntCoufuMaisuu = 0;
                for (int CoufuMaisuuCntListCnt = 0; CoufuMaisuuCntListCnt < forCoufuMaisuuCntList.Count; CoufuMaisuuCntListCnt++)
                {
                    cntCoufuMaisuu += int.Parse(forCoufuMaisuuCntList[CoufuMaisuuCntListCnt]["COUFUMAISUU"].ToString());
                }
                sysIdGrpRow["COUFUMAISUU"] = cntCoufuMaisuu;

                shuukeiData.Rows.Add(sysIdGrpRow);

                bool firstInsertFlag = false;

                for (int j = 0; j < newRowcount; j++)
                {
                    DataRow[] grpDataRow = (DataRow[])grp.ymggGroup;
                    DataRow newRow = tempData.NewRow();
                    RowIndex = RowIndex + 1;
                    newRow["ROW_NO"] = RowIndex;
                    newRow["HOUKOKUSHOBUNRUISORTKEY"] = grpDataRow[j]["HOUKOKUSHOBUNRUISORTKEY"];
                    newRow["UPN_GYOUSHA_CD"] = grpDataRow[j]["UPN_GYOUSHA_CD"];
                    newRow["UPN_GYOUSHA_NAME"] = grpDataRow[j]["UPN_GYOUSHA_NAME"];
                    newRow["UPN_SAKI_GYOUSHA_CD"] = grpDataRow[j]["UPN_SAKI_GYOUSHA_CD"];
                    newRow["UPN_SAKI_GENBA_CD"] = grpDataRow[j]["UPN_SAKI_GENBA_CD"];
                    newRow["UPN_SAKI_GENBA_ADDRESS"] = grpDataRow[j]["UPN_SAKI_GENBA_ADDRESS"];
                    newRow["UPN_FUTSUU_KYOKA_NO"] = grpDataRow[j]["UPN_FUTSUU_KYOKA_NO"];
                    newRow["SYSTEM_ID"] = grpDataRow[j]["SYSTEM_ID"];
                    newRow["HAIKI_SHURUI_CD"] = grpDataRow[j]["HAIKI_SHURUI_CD"];
                    newRow["HAIKI_KBN_CD"] = grpDataRow[j]["HAIKI_KBN_CD"];


                    // 1行目の発生
                    if (firstInsertFlag == false)
                    {
                        //①「番号」で最小番号の情報設定
                        newRow["HAIKI_SHURUI_NAME_RYAKU"] = grpDataRow[j]["HAIKI_SHURUI_NAME_RYAKU"];
                        //排出量
                        newRow["HAISHU_RYOU"] = grpDataRow[j]["HAISHU_RYOU"];
                        // 区間番号は強制的に1にする。
                        // 自社のみ、の場合で抽出したときに、自分の区間が2の場合は、そのレコードしか出てこないため。
                        newRow["UPN_ROUTE_NO"] = "1";

                        // 1行目判定フラグの追加
                        firstInsertFlag = true;
                    }

                    //①「番号」で最小番号 + 1 ～ 最大番号情報設定と「産業廃棄物の種類、排出量」をブランク
                    newRow["SBN_FUTSUU_KYOKA_NO"] = grpDataRow[j]["SBN_FUTSUU_KYOKA_NO"];
                    newRow["SBN_GYOUSHA_CD"] = grpDataRow[j]["SBN_GYOUSHA_CD"];
                    newRow["SBN_GYOUSHA_NAME"] = grpDataRow[j]["SBN_GYOUSHA_NAME"];
                    newRow["SBN_GENBA_CD"] = grpDataRow[j]["SBN_GENBA_CD"];

                    // 運搬先の住所と処分場所の住所が同じ場合は出力しない。
                    // (UPN_SAKI_GYOUSHA_CD = SBN_GYOUSHA_CD && UPN_SAKI_GENBA_CD = SBN_GENBA_CD)
                    // ロジックまたはSQLで上記の判断をかけてもいいけれど、ずれることがありえるのか？が不明なので、あえてここで文字列一致を採る。
                    // 交付等としては、「処分場所の住所は、運搬先の住所と同じである場合には記入する必要はない」となっているので、出力されてもいいはず。
                    // その際は、ここで対応できれば対応する。(そもそもSQLのほうが楽な可能性はある)
                    if (grpDataRow[j]["SBN_GENBA_ADDRESS"].Equals(grpDataRow[j]["UPN_SAKI_GENBA_ADDRESS"].ToString()))
                    {
                        newRow["SBN_GENBA_ADDRESS"] = "";
                    }
                    else
                    {
                        newRow["SBN_GENBA_ADDRESS"] = grpDataRow[j]["SBN_GENBA_ADDRESS"];
                    }

                    tempData.Rows.Add(newRow);
                }
            }

            string sysid1 = string.Empty;
            string sysid2 = string.Empty;
            string sysid3 = string.Empty;
            List<string> sysIdList = new List<string>();
            string haikiShurui1 = string.Empty;
            string haikiShurui2 = string.Empty;
            string haikiShurui3 = string.Empty;
            decimal haishutsuryo1 = 0;
            decimal haishutsuryo2 = 0;
            string haikiKbn1 = string.Empty;
            string haikiKbn2 = string.Empty;
            string haikiKbn3 = string.Empty;

            // 比較元のループ
            for (int k = 0; k < shuukeiData.Rows.Count; k++)
            {

                haikiKbn1 = shuukeiData.Rows[k]["HAIKI_KBN_CD"].ToString();
                sysid1 = shuukeiData.Rows[k]["SYSTEM_ID"].ToString();
                haikiShurui1 = shuukeiData.Rows[k]["HAIKI_SHURUI_CD"].ToString();
                DataRow[] rows1 = tempData.Select("SYSTEM_ID ='" + sysid1 + "' AND HAIKI_KBN_CD ='" + haikiKbn1 + "' AND HAIKI_SHURUI_CD ='" + haikiShurui1 + "'", "ROW_NO ASC");
                haishutsuryo1 = rows1[0]["HAISHU_RYOU"] == DBNull.Value || string.IsNullOrEmpty(rows1[0]["HAISHU_RYOU"].ToString()) ? 0 : decimal.Parse(rows1[0]["HAISHU_RYOU"].ToString());

                // チェック後に非表示になったマニは再チェックしない
                if (int.Parse(shuukeiData.Rows[k]["KEIJOU_END_KBN"].ToString()) != 0)
                {
                    continue;
                }
                // 比較先のループ
                for (int m = 0; m < shuukeiData.Rows.Count; m++)
                {
                    sysid2 = shuukeiData.Rows[m]["SYSTEM_ID"].ToString();
                    haikiKbn2 = shuukeiData.Rows[m]["HAIKI_KBN_CD"].ToString();
                    haikiShurui2 = shuukeiData.Rows[m]["HAIKI_SHURUI_CD"].ToString();

                    // 同一データは比較しない
                    if (k == m)
                    {
                        // 排出量は最初0なので自データも加算
                        shuukeiData.Rows[k]["HAISHU_RYOU"] = decimal.Parse(shuukeiData.Rows[k]["HAISHU_RYOU"].ToString()) + haishutsuryo1;
                        continue;
                    }

                    DataRow[] rows2 = tempData.Select("SYSTEM_ID ='" + sysid2 + "' AND HAIKI_KBN_CD ='" + haikiKbn2 + "' AND HAIKI_SHURUI_CD ='" + haikiShurui2 + "'", "ROW_NO ASC");
                    haishutsuryo2 = rows2[0]["HAISHU_RYOU"] == DBNull.Value || string.IsNullOrEmpty(rows2[0]["HAISHU_RYOU"].ToString()) ? 0 : decimal.Parse(rows2[0]["HAISHU_RYOU"].ToString());

                    // 区間数が一致した場合のみ比較
                    if (rows1.Length == rows2.Length)
                    {
                        int equalscnt = 0;

                        for (int n = 0; n < rows1.Length; n++)
                        {
                            // 排出量は比較しない
                            if (rows1[n]["HAIKI_SHURUI_CD"].Equals(rows2[n]["HAIKI_SHURUI_CD"]) &&
                                rows1[n]["UPN_FUTSUU_KYOKA_NO"].Equals(rows2[n]["UPN_FUTSUU_KYOKA_NO"]) &&
                                rows1[n]["UPN_GYOUSHA_CD"].Equals(rows2[n]["UPN_GYOUSHA_CD"]) &&
                                rows1[n]["UPN_SAKI_GYOUSHA_CD"].Equals(rows2[n]["UPN_SAKI_GYOUSHA_CD"]) &&
                                rows1[n]["UPN_SAKI_GENBA_CD"].Equals(rows2[n]["UPN_SAKI_GENBA_CD"]) &&
                                rows1[n]["SBN_FUTSUU_KYOKA_NO"].Equals(rows2[n]["SBN_FUTSUU_KYOKA_NO"]) &&
                                rows1[n]["SBN_GYOUSHA_CD"].Equals(rows2[n]["SBN_GYOUSHA_CD"]) &&
                                rows1[n]["SBN_GENBA_CD"].Equals(rows2[n]["SBN_GENBA_CD"])
                                )
                            {
                                equalscnt += 1;
                            }
                        }
                        // 区間数と比較結果の一致件数が同一なら完全一致
                        if (rows1.Length == equalscnt)
                        {
                            //比較元に加算
                            //交付枚数 + 1
                            shuukeiData.Rows[k]["COUFUMAISUU"] = int.Parse(shuukeiData.Rows[k]["COUFUMAISUU"].ToString()) + int.Parse(shuukeiData.Rows[m]["COUFUMAISUU"].ToString());
                            //排出量を加算
                            shuukeiData.Rows[k]["HAISHU_RYOU"] = decimal.Parse(shuukeiData.Rows[k]["HAISHU_RYOU"].ToString()) + haishutsuryo2;

                            //比較先は減算
                            shuukeiData.Rows[m]["COUFUMAISUU"] = 0;
                            //排出量を0に
                            shuukeiData.Rows[m]["HAISHU_RYOU"] = 0;

                            //他データへ計上済み区分を設定
                            shuukeiData.Rows[m]["KEIJOU_END_KBN"] = 1;

                        }
                    }
                }
            }

            // 交付枚数算出
            foreach (DataRow shuukeiRow in shuukeiData.Rows)
            {
                // 集計済みの項目は対象外
                if (int.Parse(shuukeiRow["KEIJOU_END_KBN"].ToString()) == 0)
                {
                    // 算出対象のSYSTEM_IDと報告書分類CDをセット
                    sysid3 = shuukeiRow["SYSTEM_ID"].ToString();
                    haikiShurui3 = shuukeiRow["HAIKI_SHURUI_CD"].ToString();
                    haikiKbn3 = shuukeiRow["HAIKI_KBN_CD"].ToString();

                    // SYSTEM_IDと報告書分類CDから集計前のデータから交付枚数と排出量以外のデータを追加用に取得
                    var temp = tempData.Select("SYSTEM_ID ='" + sysid3 + "' AND HAIKI_KBN_CD ='" + haikiKbn3 + "' AND HAIKI_SHURUI_CD ='" + haikiShurui3 + "'", "ROW_NO ASC");

                    for (int i = 0; i < temp.Length; i++)
                    {
                        //var addRow = temp[0];
                        var addRow = temp[i];

                        // 排出量は集計データのものをそのまま採用する
                        if (i == 0)
                        {
                            addRow["HAISHU_RYOU"] = shuukeiRow["HAISHU_RYOU"];
                            if (shuukeiRow["COUFUMAISUU"].ToString() == "")
                            {
                                addRow["COUFUMAISUU"] = DBNull.Value;
                            }
                            else if (shuukeiRow["COUFUMAISUU"].ToString() == "0")
                            {
                                addRow["COUFUMAISUU"] = DBNull.Value;
                            }
                            else
                            {
                                addRow["COUFUMAISUU"] = shuukeiRow["COUFUMAISUU"];
                            }
                        }
                        else
                        {
                            addRow["HAISHU_RYOU"] = DBNull.Value;
                            addRow["COUFUMAISUU"] = DBNull.Value;
                        }
                        // 表示用データとして追加
                        orgData.ImportRow(addRow);
                    }
                }
            }

            DataTable soreData = orgData.Clone();
            DataTable detailData = orgData.Clone();

            // 運搬経路による並替用キー項目追加
            soreData.Columns.Add("UPN_ROUTE_CD", typeof(string));

            for (int i = 0; i < orgData.Rows.Count; i++)
            {
                if (i > 0)
                {
                    string id1 = orgData.Rows[i]["SYSTEM_ID"].ToString();
                    string id2 = orgData.Rows[i - 1]["SYSTEM_ID"].ToString();
                    string kbnCd1 = orgData.Rows[i]["HAIKI_KBN_CD"].ToString();
                    string kbnCd2 = orgData.Rows[i - 1]["HAIKI_KBN_CD"].ToString();
                    string ShuruiCd1 = orgData.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    string ShuruiCd2 = orgData.Rows[i - 1]["HAIKI_SHURUI_CD"].ToString();
                    if (id1.Equals(id2) && kbnCd1.Equals(kbnCd2) && ShuruiCd1.Equals(ShuruiCd2))
                    {
                        continue;
                    }

                    soreData.ImportRow(orgData.Rows[i]);

                }
                else
                {
                    soreData.ImportRow(orgData.Rows[i]);
                }
            }

            //運搬経路の取得
            for (int i = 0; i < soreData.Rows.Count; i++)
            {
                string sysid = soreData.Rows[i]["SYSTEM_ID"].ToString();
                string haikiKbn = soreData.Rows[i]["HAIKI_KBN_CD"].ToString();
                string haikiShurui = soreData.Rows[i]["HAIKI_SHURUI_CD"].ToString();

                // 運搬経路用CD連結文字列
                string UPN_ROUTE_CD = "";

                var upnData = orgData.Select("SYSTEM_ID ='" + sysid + "' AND HAIKI_KBN_CD ='" + haikiKbn + "' AND HAIKI_SHURUI_CD ='" + haikiShurui + "'", "ROW_NO ASC");

                int lastRouteIndex = upnData.Length - 1;
                for (int j = 0; j < lastRouteIndex; j++)
                {
                    //当区間はどこへ運んだか運搬経路を記録
                    //000000 000000
                    //000000 000000 000001 000000
                    //000000 000000 000002 000000 000001 000000
                    //結果、上記のような文字列ができあがる。これを最終的にソートさせることで、経路の昇順にデータが並べられる
                    UPN_ROUTE_CD += upnData[j]["UPN_SAKI_GYOUSHA_CD"].ToString();
                    UPN_ROUTE_CD += upnData[j]["UPN_SAKI_GENBA_CD"].ToString();
                }

                //最終的な運搬経路は処分業者・処分現場
                UPN_ROUTE_CD += upnData[lastRouteIndex]["SBN_GYOUSHA_CD"].ToString();
                UPN_ROUTE_CD += upnData[lastRouteIndex]["SBN_GENBA_CD"].ToString();

                soreData.Rows[i]["UPN_ROUTE_CD"] = UPN_ROUTE_CD;

                // 運搬経路をクリア
                UPN_ROUTE_CD = "";
            }

            var sortTemp = soreData.Select("1=1", "HOUKOKUSHOBUNRUISORTKEY, HAIKI_SHURUI_CD, UPN_GYOUSHA_CD, UPN_ROUTE_CD ASC");
            //※同じ廃棄物種類(報告書分類)で同じ運搬業者で同じ経路のものは合算されているため、Sort条件としては一意になるはず

            for (int i = 0; i < sortTemp.Length; i++)
            {

                string sortSysId1 = sortTemp[i]["SYSTEM_ID"].ToString();
                string sortKbnCd1 = sortTemp[i]["HAIKI_KBN_CD"].ToString();
                string sortShuruiCd1 = sortTemp[i]["HAIKI_SHURUI_CD"].ToString();
                for (int j = 0; j < orgData.Rows.Count; j++)
                {
                    string sortSysId2 = orgData.Rows[j]["SYSTEM_ID"].ToString();
                    string sortKbnCd2 = orgData.Rows[j]["HAIKI_KBN_CD"].ToString();
                    string sortShuruiCd2 = orgData.Rows[j]["HAIKI_SHURUI_CD"].ToString();
                    if (sortSysId1.Equals(sortSysId2) && sortKbnCd1.Equals(sortKbnCd2) && sortShuruiCd1.Equals(sortShuruiCd2))
                    {
                        detailData.ImportRow(orgData.Rows[j]);
                    }
                }
            }
            this.mReportDetailInfo = detailData;

            #endregion
        }

        ///<summary>
        ///詳細情報画面初期化処理
        ///</summary>
        private void InitDetail()
        {
            DataTable objData = this.mReportDetailInfo;
            CustomDataGridView objGrid = this.form.IchiranGRD;
            objGrid.Rows.Clear();
            // 数量フォーマット
            String systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.MANIFEST_SUURYO_FORMAT, string.Empty).ToString();
            // 明細行を追加
            objGrid.Rows.Add(objData.Rows.Count);
            for (int j = 0; j < objData.Rows.Count; j++)
            {
                DataRow dtRow = objData.Rows[j];
                // 行番号
                objGrid["ROW_NO", j].Value = j + 1;
                // 産業廃棄物の種類
                objGrid["HAIKI_SHURUI_NAME_RYAKU", j].Value = this.ChgDBNullToValue(dtRow["HAIKI_SHURUI_NAME_RYAKU"], string.Empty);

                if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dtRow["HAISHU_RYOU"], string.Empty).ToString()))
                {
                    //排出量(t)
                    objGrid["HAISHU_RYOU", j].Value = decimal.Parse(this.ChgDBNullToValue(dtRow["HAISHU_RYOU"], 0).ToString()).ToString(systemSuuryouFormat);
                }
                else
                {
                    //排出量(t)
                    objGrid["HAISHU_RYOU", j].Value = string.Empty;
                }
                //管理票の交付枚数
                if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dtRow["COUFUMAISUU"], string.Empty).ToString()))
                {
                    objGrid["COUFUMAISUU", j].Value = decimal.Parse(this.ChgDBNullToValue(dtRow["COUFUMAISUU"], string.Empty).ToString()).ToString("#,###");
                }
                else
                {
                    objGrid["COUFUMAISUU", j].Value = string.Empty;
                }
                //運搬受託者の許可番号
                objGrid["UPN_FUTSUU_KYOKA_NO", j].Value = this.ChgDBNullToValue(dtRow["UPN_FUTSUU_KYOKA_NO"], string.Empty);
                //運搬受託者の氏名又は名称
                objGrid["UPN_GYOUSHA_NAME", j].Value = this.ChgDBNullToValue(dtRow["UPN_GYOUSHA_NAME"], string.Empty);
                //運搬先の住所
                objGrid["UPN_SAKI_GENBA_ADDRESS", j].Value = this.ChgDBNullToValue(dtRow["UPN_SAKI_GENBA_ADDRESS"], string.Empty);
                //処分受託者の許可番号
                objGrid["SBN_FUTSUU_KYOKA_NO", j].Value = this.ChgDBNullToValue(dtRow["SBN_FUTSUU_KYOKA_NO"], string.Empty);
                //処分受託者の氏名又は名称
                objGrid["SBN_GYOUSHA_NAME", j].Value = this.ChgDBNullToValue(dtRow["SBN_GYOUSHA_NAME"], string.Empty);
                //処分場所の住所
                objGrid["SBN_GENBA_ADDRESS", j].Value = this.ChgDBNullToValue(dtRow["SBN_GENBA_ADDRESS"], string.Empty);
            }

            //排出量合計(t)
            this.form.txtAllHaishuRyo.Text = objData.AsEnumerable().Sum(r => decimal.Parse(this.ChgDBNullToValue(r["HAISHU_RYOU"], 0).ToString())).ToString(systemSuuryouFormat);
        }

        #endregion

        #region 次前ボタンの制御
        internal void buttonControlPrevNext(int countCurrent, int countMax)
        {
            //次前ボタンの制御
            var parentForm = (BusinessBaseForm)this.form.Parent;

            //現在カウントが0より大なら前ページが存在する
            if (countCurrent > 0)
            {
                parentForm.bt_func1.Enabled = true;
            }
            else
            {
                parentForm.bt_func1.Enabled = false;
            }

            //現在カウントが最大カウントより小なら次ページが存在する
            if (countCurrent < countMax)
            {
                parentForm.bt_func2.Enabled = true;
            }
            else
            {
                parentForm.bt_func2.Enabled = false;
            }

            parentForm = null;
        }
        #endregion

        #region [F1]前を取得
        /// <summary>
        /// 前の番号を取得
        /// </summary>
        internal bool GetPreviousNumber()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                var dtIchiranData = this.searchAllIchiranDetailData;
                if (dtIchiranData == null)
                {
                    return returnVal;
                }
                //前のindex取得
                int count = int.Parse(this.form.hiddenValue.Text.ToString()) - 1;

                if (count < 0)
                {
                    return returnVal;
                }
                //画面ヘッダ初期化処理
                if (this.ReportInfoList["Genbashukei_Kbn"].ToString().Equals("2"))
                {
                    // 現場一括集計無
                    this.InitHeard(count);
                }
                else
                {
                    // 現場一括集計有
                    this.InitHeard_GenbaIkkatsu(count);
                }
                // //詳細情報編集
                //this.EditDetailData();
                this.EditChokugyouDetailData();
                //画面詳細初期化処理
                this.InitDetail();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region [F2]次を取得
        /// <summary>
        /// 次の番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">番号</param>
        /// <returns>次の番号</returns>
        internal bool GetNextNumber()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();
                var dtIchiranData = this.searchAllIchiranDetailData;
                if (dtIchiranData == null)
                {
                    return returnVal;
                }
                //次のindex取得
                int count = int.Parse(this.form.hiddenValue.Text.ToString()) + 1;
                if (count >= int.Parse(this.Tag.ToString()))
                {
                    return returnVal;
                }
                //画面ヘッダ初期化処理
                if (this.ReportInfoList["Genbashukei_Kbn"].ToString().Equals("2"))
                {
                    // 現場一括集計無
                    this.InitHeard(count);
                }
                else
                {
                    // 現場一括集計有
                    this.InitHeard_GenbaIkkatsu(count);
                }
                //詳細情報編集
                //this.EditDetailData();
                this.EditChokugyouDetailData();
                //画面詳細初期化処理
                this.InitDetail();

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region [F5]印刷
        /// <summary>
        /// [F5]印刷　処理
        /// </summary>
        internal bool Print()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();

                //レポートＩＮＦＯ（DataTableList）作成
                this.ReportInfo = new ReportInfoR394(WINDOW_ID.T_KOUHUJYOKYO_HOKOKUSHO);
                //帳票Header情報取得
                this.CreateReportHeaderInfo();
                //帳票詳細情報取得
                this.CreateDetailDataTable();
                //帳票Footer情報取得
                this.CreateFooterDataTable();
                ////テストデータ作成
                //this.ReportInfo.CreateSampleData();

                this.ReportInfo.Create(@".\Template\R394-Form.xml", "LAYOUT1", new DataTable());
                using (FormReportPrintPopup formReportPrintPopup = new FormReportPrintPopup(this.ReportInfo, WINDOW_ID.T_KOUHUJYOKYO_HOKOKUSHO))
                {
                    //レポートタイトルの設定
                    formReportPrintPopup.ReportCaption = "交付等状況報告書";
                    
                    formReportPrintPopup.ShowDialog();
                    formReportPrintPopup.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Print", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 全印刷　処理
        /// </summary>
        internal void AllPrint()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //ページ分回す回数取得
                int maxCount = int.Parse(this.Tag.ToString());

                //処理後に表示画面に戻るための添え字
                int currentCount = int.Parse(this.form.hiddenValue.Text.ToString());

                //count=0ページに移動して表示
                int count = 0;

                if (this.ReportInfoList["Genbashukei_Kbn"].ToString().Equals("2"))
                {
                    // 現場一括集計無
                    this.InitHeard(count);
                }
                else
                {
                    // 現場一括集計有
                    this.InitHeard_GenbaIkkatsu(count);
                }
                this.EditChokugyouDetailData();
                this.InitDetail();

                for (count = 0; count < maxCount; count++)
                {
                    //printし続ける
                    Print();

                    //maxページまで次ボタンロジック使用
                    GetNextNumber();
                }

                //表示していた画面に戻る
                this.InitHeard(currentCount);
                this.EditChokugyouDetailData();
                this.InitDetail();


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

        #region 帳票情報取得
        /// <summary>帳票Header情報取得</summary>
        public void CreateReportHeaderInfo()
        {
            #region - Header -

            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";

            //提出先
            dataTableTmp.Columns.Add("GOV_OR_MAY_NAME");
            dataTableTmp.Columns.Add("CHIIKI_NAME_RYAKU");
            //事業場の名称
            dataTableTmp.Columns.Add("JGB_NAME");
            //事業場の住所
            dataTableTmp.Columns.Add("JGB_ADDRESS");
            //業種
            dataTableTmp.Columns.Add("GYOUSHU_CD");
            dataTableTmp.Columns.Add("GYOUSHU_NAME");
            //電話
            dataTableTmp.Columns.Add("GENBA_TEL");
            //作成日
            dataTableTmp.Columns.Add("PRINT_DATE");
            // 年度の基準日
            dataTableTmp.Columns.Add("NENDO_BASE_DATE");

            //報告者
            //住所
            dataTableTmp.Columns.Add("GYOUSHA_ADDRESS1");
            dataTableTmp.Columns.Add("GYOUSHA_ADDRESS2");
            //氏名
            dataTableTmp.Columns.Add("GYOUSHA_NAME1");
            dataTableTmp.Columns.Add("GYOUSHA_NAME2");
            //代表者
            dataTableTmp.Columns.Add("DAIHYOUSHA");
            //電話
            dataTableTmp.Columns.Add("GYOUSHA_TEL");
            //表題
            dataTableTmp.Columns.Add("TITLE1");
            dataTableTmp.Columns.Add("TITLE2");

            //行データ設定
            rowTmp = dataTableTmp.NewRow();
            //提出先
            rowTmp["GOV_OR_MAY_NAME"] = this.form.txtTeishutusaki.Text;

            rowTmp["CHIIKI_NAME_RYAKU"] = this.chiiki_name;
            //事業場の名称
            rowTmp["JGB_NAME"] = this.form.txtGenbaNm.Text;
            //事業場の住所         
            rowTmp["JGB_ADDRESS"] = this.form.txtGenbaAddress.Text;
            //業種
            rowTmp["GYOUSHU_CD"] = this.form.txtGYOUSHUCD.Text;
            rowTmp["GYOUSHU_NAME"] = this.form.txtGYOUSHUNM.Text;
            //電話
            rowTmp["GENBA_TEL"] = this.form.txtGenbaTel.Text;

            //作成日
            rowTmp["PRINT_DATE"] = this.form.dtpCreadDate.Text;

            // 年度の基準日
            rowTmp["NENDO_BASE_DATE"] = this.ReportInfoList["Koufu_Date_From"].ToString();

            //報告者
            //住所
            rowTmp["GYOUSHA_ADDRESS1"] = this.form.txtADDRESS1.Text;
            rowTmp["GYOUSHA_ADDRESS2"] = this.form.txtADDRESS2.Text;
            //氏名
            rowTmp["GYOUSHA_NAME1"] = this.form.txtName1.Text;
            rowTmp["GYOUSHA_NAME2"] = this.form.txtName2.Text;
            //代表者         
            rowTmp["DAIHYOUSHA"] = this.form.txtDAIHYOUSHA.Text;
            //電話
            rowTmp["GYOUSHA_TEL"] = this.form.txtTel.Text;
            //表題
            rowTmp["TITLE1"] = this.form.txtTitle1.Text;
            rowTmp["TITLE2"] = this.form.txtTitle2.Text;

            dataTableTmp.Rows.Add(rowTmp);
            mReportHeaderInfo = dataTableTmp;
            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Header", this.mReportHeaderInfo);

            #endregion - Header -
        }
        /// <summary>帳票詳細情報取得 </summary>
        public void CreateDetailDataTable()
        {
            #region - Detail -
            this.mReportDetailInfo.TableName = "Detail";
            // データを設定する処理を入れる
            this.ReportInfo.DataTableList.Add("Detail", this.mReportDetailInfo);
            #endregion - Detail -
        }
        /// <summary>/帳票Footer情報取得</summary>
        public void CreateFooterDataTable()
        {

            string ShuturyokuNaiyo = string.Empty;
            #region - Footer -
            DataTable dataTableTmp;
            DataRow rowTmp;
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            //排出量合計							
            dataTableTmp.Columns.Add("ALLHAISHURYO");
            rowTmp = dataTableTmp.NewRow();

            //合計						
            rowTmp["ALLHAISHURYO"] = this.form.txtAllHaishuRyo.Text;

            dataTableTmp.Rows.Add(rowTmp);
            this.mReportFooterInfo = dataTableTmp;
            this.ReportInfo.DataTableList.Add("Footer", this.mReportFooterInfo);

            #endregion - Footer -
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
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.Close();
                parentForm.Close();

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

        #region private Method

        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //検索データ取得と検索データ処理              
                return 0;
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
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToDateTimeValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else if (string.IsNullOrEmpty(obj.ToString()))
            {
                return value;
            }
            else
            {
                return ((DateTime)obj).ToShortDateString();
            }
        }

        /// <summary>
        /// 単価、数量の共通フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string SuuryouAndTankFormat(object obj, String format)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(obj, format);

                decimal num = 0;
                decimal.TryParse(Convert.ToString(obj), out num);

                returnVal = string.Format("{0:" + format + "}", num);

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

        #region
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

        //public int Search()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
