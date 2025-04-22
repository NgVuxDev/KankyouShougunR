using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        //■↓テーブルコピー用
        ///// <summary>
        ///// 削除処理の切り分け　０：OLDﾃｰﾌﾞﾙに移動して物理削除　１：物理削除のみ行う
        ///// </summary>
        //private int DelOnlyFLG = 1;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.Setting.ButtonSetting.xml";

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        internal int HisDelRange;       //履歴削除範囲(m_sys_infoから取得)

        /// <summary>
        /// 画面フォーム
        /// </summary>
        private UIForm form;
        private HeaderForm header;
        private BusinessBaseForm footer;

        /// <summary>
        /// DTO
        /// </summary>
        internal DTOClass dto;

        /// <summary>
        /// 会社情報のDao
        /// </summary>
        private M_SYS_INFO[] SysEntitys;
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 自社情報入力のDao
        /// </summary>
        private M_CORP_INFO[] entitys;
        private IM_CORP_INFODao daoCorpInfo;

        /// <summary>
        /// データ削除範囲のDao
        /// </summary>
        private M_OLD_DATA_DEL entityOldData;
        private IM_OLD_DATA_DELDao daoOldDataDel;

        /// <summary>
        /// コンバート用
        /// </summary>
        private ConvertDaoCls daoConvertClass;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();

            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.daoOldDataDel = DaoInitUtility.GetComponent<IM_OLD_DATA_DELDao>();
            this.daoConvertClass = DaoInitUtility.GetComponent<ConvertDaoCls>();

            LogUtility.DebugMethodEnd();
        }

        #region 実現必須メソッド
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

        #region 画面初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンを初期化
                this.ButtonInit();

                //footボタン処理イベントを初期化
                EventInit();

                // 画面表示初期化
                this.SetInitDisp();

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region ヘッダー初期化処理
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderForm targetHeader = (HeaderForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = "履歴データ削除";

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //実行ボタン(F9)イベント生成
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region シリーズ毎表示制御
        /// <summary>
        /// 起動シリーズ毎に、使用可不可の画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            //自社情報マスタから機種情報を取得
            //期首年月日の前年の前日とする。
            DateTime datToday = DateTime.Today;
            this.entitys = daoCorpInfo.GetAllData();

            if (this.entitys.Length != 0)
            {
                string DefDay = string.Empty;

                if (entitys[0].KISHU_MONTH.Value <= int.Parse(datToday.Month.ToString()))
                {
                    //システム日付が、期首月or期首月を過ぎている→同年
                    //例)sys 2020/4/12, 期首4月→2020/4/12→2019/4/1→2019/3/31
                    DefDay = DateTime.Parse(datToday.AddYears(-1).ToString("yyyy") + "/" + (entitys[0].KISHU_MONTH.Value) + "/1").AddDays(-1).ToString();
                }
                else
                {
                    //システム日付が、期首月を迎えてない→前年
                    //例)sys 2020/4/12, 期首10月→2020/4/12→2018/10/1→2018/9/30
                    DefDay = DateTime.Parse(datToday.AddYears(-2).ToString("yyyy") + "/" + (entitys[0].KISHU_MONTH.Value) + "/1").AddDays(-1).ToString();
                }

                this.form.EIGYOU_DAY.Text = DefDay;
                this.form.UKETSUKE_DAY.Text = DefDay;
                this.form.HAISHA_DAY.Text = DefDay;
                this.form.KEIRYOU_DAY.Text = DefDay;
                this.form.HANKAN_DAY.Text = DefDay;
                this.form.MANIFEST_DAY.Text = DefDay;
                this.form.UNCHIN_DAY.Text = DefDay;
                this.form.RENKEI_DAY.Text = DefDay;
            }

            //シリーズ設定
            if (AppConfig.Series == "A1" || AppConfig.Series == "A2")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(1, 1, 1, 2, 1, 1, 1, 1);
            }
            else if (AppConfig.Series == "C1")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 1, 1, 2, 1, 1, 1, 1);
            }
            else if (AppConfig.Series == "C2")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 2, 1, 2, 1, 2);
            }
            else if (AppConfig.Series == "C3")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 2, 1, 1, 1, 2);
            }
            else if (AppConfig.Series == "C4")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 1, 1, 2, 1, 2, 1, 1);
            }
            else if (AppConfig.Series == "C5")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 2, 1, 2, 1, 2);
            }
            else if (AppConfig.Series == "C6")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 1, 1, 2, 2, 2, 2, 1);
            }
            else if (AppConfig.Series == "C7" || AppConfig.Series == "C8")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 2, 2, 1, 2, 2);
            }
            else if (AppConfig.Series == "C9")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 1, 2, 2, 2, 2);
            }
            else if (AppConfig.Series == "D1" || AppConfig.Series == "D2")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 1, 1, 2, 2, 1, 2, 1);
            }
            else if (AppConfig.Series == "D3" || AppConfig.Series == "D4")
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(2, 2, 2, 1, 2, 1, 2, 2);
            }
            else
            {
                //営業・受付・配車・計量・販管・マニ・運賃・外部
                SetInitDispSeries(1, 1, 1, 1, 1, 1, 1, 1);
            }

        }
        /// <summary>
        /// SetInitDispから来た設定値で、メニューの使用可不可を設定
        /// </summary>
        /// <param name="intEigyou">営業使用有無</param>
        /// <param name="intUketsuke">受付使用有無</param>
        /// <param name="intHaisha">配車使用有無</param>
        /// <param name="intKeiryou">計量使用有無</param>
        /// <param name="intHankan">販管使用有無</param>
        /// <param name="intManifest">マニ使用有無</param>
        /// <param name="intUnchin">運賃使用有無</param>
        /// <param name="intRenkei">外部連携使用有無</param>
        private void SetInitDispSeries(int intEigyou, int intUketsuke, int intHaisha, int intKeiryou,
                               int intHankan, int intManifest, int intUnchin, int intRenkei)
        {

            //初期化(表示)
            this.form.txt_EigyouRange.Text = "1";
            this.form.txt_EigyouRange.Enabled = true;
            this.form.panel_Eigyou.Enabled = true;
            this.form.txt_UketsukeRange.Text = "1";
            this.form.txt_UketsukeRange.Enabled = true;
            this.form.panel_Uketsuke.Enabled = true;
            this.form.txt_HaishaRange.Text = "1";
            this.form.txt_HaishaRange.Enabled = true;
            this.form.panel_Haisha.Enabled = true;
            this.form.txt_KeiryouRange.Text = "1";
            this.form.txt_KeiryouRange.Enabled = true;
            this.form.panel_Keiryou.Enabled = true;
            this.form.txt_HankanRange.Text = "1";
            this.form.txt_HankanRange.Enabled = true;
            this.form.panel_Hankan.Enabled = true;
            this.form.txt_ManifestRange.Text = "1";
            this.form.txt_ManifestRange.Enabled = true;
            this.form.panel_Manifest.Enabled = true;
            this.form.txt_UnchinRange.Text = "1";
            this.form.txt_UnchinRange.Enabled = true;
            this.form.panel_Unchin.Enabled = true;
            this.form.txt_RenkeiRange.Text = "1";
            this.form.txt_RenkeiRange.Enabled = true;
            this.form.panel_Renkei.Enabled = true;

            if (intEigyou == 2)
            {
                this.form.txt_EigyouRange.Text = "2";
                this.form.txt_EigyouRange.Enabled = false;
                this.form.panel_Eigyou.Enabled = false;
                this.form.EIGYOU_DAY.Text = "";
            }
            if (intUketsuke == 2)
            {
                this.form.txt_UketsukeRange.Text = "2";
                this.form.txt_UketsukeRange.Enabled = false;
                this.form.panel_Uketsuke.Enabled = false;
                this.form.UKETSUKE_DAY.Text = "";
            }
            if (intHaisha == 2)
            {
                this.form.txt_HaishaRange.Text = "2";
                this.form.txt_HaishaRange.Enabled = false;
                this.form.panel_Haisha.Enabled = false;
                this.form.HAISHA_DAY.Text = "";
            }
            if (intKeiryou == 2)
            {
                this.form.txt_KeiryouRange.Text = "2";
                this.form.txt_KeiryouRange.Enabled = false;
                this.form.panel_Keiryou.Enabled = false;
                this.form.KEIRYOU_DAY.Text = "";
            }
            if (intHankan == 2)
            {
                this.form.txt_HankanRange.Text = "2";
                this.form.txt_HankanRange.Enabled = false;
                this.form.panel_Hankan.Enabled = false;
                this.form.HANKAN_DAY.Text = "";
            }
            if (intManifest == 2)
            {
                this.form.txt_ManifestRange.Text = "2";
                this.form.txt_ManifestRange.Enabled = false;
                this.form.panel_Manifest.Enabled = false;
                this.form.MANIFEST_DAY.Text = "";
            }
            if (intUnchin == 2)
            {
                this.form.txt_UnchinRange.Text = "2";
                this.form.txt_UnchinRange.Enabled = false;
                this.form.panel_Unchin.Enabled = false;
                this.form.UNCHIN_DAY.Text = "";
            }
            if (intRenkei == 2)
            {
                this.form.txt_RenkeiRange.Text = "2";
                this.form.txt_RenkeiRange.Enabled = false;
                this.form.panel_Renkei.Enabled = false;
                this.form.RENKEI_DAY.Text = "";
            }

        }
        #endregion

        /// <summary>
        /// 実行
        /// </summary>
        public void jikkou()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.SysEntitys = daoSysInfo.GetAllData();
                HisDelRange = this.SysEntitys[0].HISTORY_DELETE_RANGE.Value;
            }
            catch (SQLRuntimeException ex2)
            {
                errmessage.MessageBoxShowWarn(ex2.MessageCode);
                return;
            }

            //プログレスバー設定
            //履歴削除テーブルが増減する場合は、プログレスバーのパラメーター数を変更する事
            SetProgressBar();

            //各範囲毎に、データ物理削除
            if (TableConvert().Equals(true))
            {
                //プログレスバーをリセット
                ResetProgressBar();
                //正常完了
                errmessage.MessageBoxShow("I001", "データ削除");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// プログレスバーの範囲を設定
        /// ※履歴削除テーブルが増減する場合は、プログレスバーのパラメーター数を変更する事
        /// 　コミットのブロック単位を１とする。
        /// </summary>
        private void SetProgressBar()
        {
            int max = 0;
            var parentForm = (BusinessBaseForm)this.form.Parent;
            #region パラメーター数
            if (this.form.EigyouRange_1.Checked == true)
            {
                //営業：4
                max = max + 4;
            }
            if (this.form.UkersukeRange_1.Checked == true)
            {
                //受付：5
                max = max + 5;
            }
            if (this.form.HaishaRange_1.Checked == true)
            {
                //配車：5
                max = max + 5;
            }
            if (this.form.KeiryouRange_1.Checked == true)
            {
                //計量：2
                max = max + 2;
            }
            if (this.form.HankanRange_1.Checked == true)
            {
                //販管：16
                max = max + 16;
            }
            if (this.form.ManifestRange_1.Checked == true)
            {
                //マニフェスト：17
                max = max + 17;
            }
            if (this.form.UnchinRange_1.Checked == true)
            {
                //運賃：1
                max = max + 1;
            }
            if (this.form.RenkeiRange_1.Checked == true)
            {
                //外部連携：11
                max = max + 11;
            }
            #endregion
            parentForm.progresBar.Maximum = max;
            parentForm.progresBar.Minimum = 0;
            parentForm.progresBar.Value = 0;
        }

        /// <summary>
        /// プログレスバーを加算
        /// </summary>
        private void IncProgressBar()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (parentForm.progresBar.Maximum > parentForm.progresBar.Value)
            {
                parentForm.progresBar.Value += 1;
            }
            Application.DoEvents();
        }

        /// <summary>
        /// プログレスバーをリセット
        /// </summary>
        private void ResetProgressBar()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.progresBar.Value = 0;
        }

        #region データ移動/物理削除
        /// <summary>
        /// データ移動/物理削除
        /// 各項目の実施「１．する」範囲のデータを、データコピー/削除する。
        /// ※伝票のキーを使用して明細等のデータを処理しているので、明細→伝票の順に行う事。
        /// ※履歴削除テーブルが増減する場合、SetProgressBar()にて、プログレスバーのパラメーター数も変更する事。
        /// </summary>
        [Transaction]
        public bool TableConvert()
        {

            LogUtility.DebugMethodStart();

            DateTime DelStart;
            DateTime DelEnd;

            string delday = "";         //汎用：各ブロックの対象範囲の日付を画面取得して使用
            string deldayOld = "";      //汎用：各ブロックの対象範囲の日付を画面取得して使用
            string delJoin = "";        //共通：SYSTEM_ID,SEQでJOINに使用（変更不可）
            string delJoin2 = "";       //汎用：SYSTEM_ID,SEQでJOINしない場合、適宜変えながら使用
            string delWhere = "";       //汎用：削除条件の指定に使用（主に日付・明細系）
            string delWhere2 = "";      //汎用：削除条件の指定に使用（主に日付・伝票系）
            string delflgWhere = "";    //DELETE_FLGが立っている一番古い日付を取得用
            delJoin = "E.SYSTEM_ID = DelT.SYSTEM_ID AND E.SEQ = DelT.SEQ";

            #region 営業
            if (this.form.EigyouRange_1.Checked == true)
            {
                try
                {
                    DataTable retEigyouRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.EIGYOU_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■見積
                    retEigyouRange = this.daoConvertClass.TableDeleteFLG("T_MITSUMORI_ENTRY", delflgWhere);
                    //範囲内にDELETE_FLGのデータが無ければ処理を行わない
                    if (!(retEigyouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retEigyouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MITSUMORI_DETAIL",
                                                "T_MITSUMORI_ENTRY E INNER JOIN T_MITSUMORI_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MITSUMORI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MITSUMORI_ENTRY",
                                                "T_MITSUMORI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■受注目標
                    retEigyouRange.Clear();
                    retEigyouRange = this.daoConvertClass.TableDeleteFLG("T_JUCHU_M_KENSU", delflgWhere);
                    if (!(retEigyouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retEigyouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_JUCHU_M_KENSU",
                                                "T_JUCHU_M_KENSU DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■営業予算
                    retEigyouRange.Clear();
                    retEigyouRange = this.daoConvertClass.TableDeleteFLG("T_EIGYO_YOSAN", delflgWhere);
                    if (!(retEigyouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retEigyouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_EIGYO_YOSAN",
                                                "T_EIGYO_YOSAN DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region■電子申請
                    retEigyouRange.Clear();
                    retEigyouRange = this.daoConvertClass.TableDeleteFLG("T_DENSHI_SHINSEI_ENTRY", delflgWhere);
                    if (!(retEigyouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retEigyouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                ////電子申請明細承認否認(申請の承認否認情報→DETAIL.DTAIL_SYSTEM_IDと紐づいている)
                                InsertAndDelete("T_DENSHI_SHINSEI_DETAIL_ACTION",
                                                "T_DENSHI_SHINSEI_ENTRY E INNER JOIN T_DENSHI_SHINSEI_DETAIL D ON E.SYSTEM_ID = D.SYSTEM_ID AND E.SEQ = D.SEQ" +
                                                " INNER JOIN T_DENSHI_SHINSEI_DETAIL_ACTION DelT ON D.DETAIL_SYSTEM_ID = DelT.DETAIL_SYSTEM_ID AND D.SEQ = DelT.SEQ" + delWhere,
                                                " INNER JOIN (SELECT E.SHINSEI_DATE, D.* FROM T_DENSHI_SHINSEI_ENTRY E INNER JOIN T_DENSHI_SHINSEI_DETAIL D ON E.SYSTEM_ID = D.SYSTEM_ID AND E.SEQ = D.SEQ" + delWhere + ") AS F" +
                                                " ON F.DETAIL_SYSTEM_ID = DelT.DETAIL_SYSTEM_ID AND F.SEQ = DelT.SEQ");
                                ////電子申請状態(申請がどのステータスか→ENTRY.SYSTEM_IDと紐づいている)
                                InsertAndDelete("T_DENSHI_SHINSEI_STATUS",
                                                "T_DENSHI_SHINSEI_ENTRY E INNER JOIN T_DENSHI_SHINSEI_STATUS DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_DENSHI_SHINSEI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_DENSHI_SHINSEI_DETAIL",
                                                "T_DENSHI_SHINSEI_ENTRY E INNER JOIN T_DENSHI_SHINSEI_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_DENSHI_SHINSEI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_DENSHI_SHINSEI_ENTRY",
                                                "T_DENSHI_SHINSEI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("営業");
                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/営業" + ex2.MessageCode);
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion 営業

            #region 受付
            if (this.form.UkersukeRange_1.Checked == true)
            {
                try
                {
                    DataTable retUkersukeRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.UKETSUKE_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■受付収集伝票
                    retUkersukeRange = this.daoConvertClass.TableDeleteFLG("T_UKETSUKE_SS_ENTRY", delflgWhere);
                    if (!(retUkersukeRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retUkersukeRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_CONTENA_RESERVE",
                                                "T_UKETSUKE_SS_ENTRY E INNER JOIN T_CONTENA_RESERVE DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UKETSUKE_SS_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UKETSUKE_SS_DETAIL",
                                                "T_UKETSUKE_SS_ENTRY E INNER JOIN T_UKETSUKE_SS_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UKETSUKE_SS_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UKETSUKE_SS_ENTRY",
                                                "T_UKETSUKE_SS_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■受付出荷伝票
                    retUkersukeRange.Clear();
                    retUkersukeRange = this.daoConvertClass.TableDeleteFLG("T_UKETSUKE_SK_ENTRY", delflgWhere);
                    if (!(retUkersukeRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retUkersukeRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UKETSUKE_SK_DETAIL",
                                                "T_UKETSUKE_SK_ENTRY E INNER JOIN T_UKETSUKE_SK_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UKETSUKE_SK_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UKETSUKE_SK_ENTRY",
                                                "T_UKETSUKE_SK_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■受付持込伝票
                    retUkersukeRange.Clear();
                    retUkersukeRange = this.daoConvertClass.TableDeleteFLG("T_UKETSUKE_MK_ENTRY", delflgWhere);
                    if (!(retUkersukeRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retUkersukeRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UKETSUKE_MK_DETAIL",
                                                "T_UKETSUKE_MK_ENTRY E INNER JOIN T_UKETSUKE_MK_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UKETSUKE_MK_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UKETSUKE_MK_ENTRY",
                                                "T_UKETSUKE_MK_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    //■受付物販伝票(Ver2でプロジェクトの物理削除されてるので、V2では対応しない
                    //→(T_UKETSUKE_BP_ENTRY(受付(物販)入力)/T_UKETSUKE_BP_DETAIL(受付(物販)明細)/T_SALES_ZAIKO_DETAIL(物販在庫明細)/T_CONTENA_RESERVE(コンテナ稼動予定))
                    #region ■受付クレーム伝票
                    retUkersukeRange.Clear();
                    retUkersukeRange = this.daoConvertClass.TableDeleteFLG("T_UKETSUKE_CM_ENTRY", delflgWhere);
                    if (!(retUkersukeRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retUkersukeRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UKETSUKE_CM_ENTRY",
                                                "T_UKETSUKE_CM_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion

                    #region ■現場メモ
                    //発生元が、受付に由来するもの('2','3','4')、発生元なし('1','')のデータを対象とする
                    delflgWhere = " WHERE DELETE_FLG = 1 AND HASSEIMOTO_CD IN ('1','2','3','4','') AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    retUkersukeRange.Clear();
                    retUkersukeRange = this.daoConvertClass.TableDeleteFLG("T_GENBAMEMO_ENTRY", delflgWhere);
                    if (!(retUkersukeRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retUkersukeRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.HASSEIMOTO_CD IN ('1','2','3','4','') AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.HASSEIMOTO_CD IN ('1','2','3','4','') AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_GENBAMEMO_DETAIL",
                                                "T_GENBAMEMO_ENTRY E INNER JOIN T_GENBAMEMO_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_GENBAMEMO_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_GENBAMEMO_ENTRY",
                                                "T_GENBAMEMO_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    //初期化
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("受付");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/受付");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region 配車
            if (this.form.HaishaRange_1.Checked == true)
            {
                try
                {
                    DataTable retHaishaRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.HAISHA_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region■配車割当
                    retHaishaRange = this.daoConvertClass.TableDeleteFLG("T_HAISHA_WARIATE_DAY", delflgWhere);
                    if (!(retHaishaRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retHaishaRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_HAISHA_WARIATE_DAY",
                                                "T_HAISHA_WARIATE_DAY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■配車メモ
                    retHaishaRange.Clear();
                    retHaishaRange = this.daoConvertClass.TableDeleteFLG("T_HAISHA_MEMO", delflgWhere);
                    if (!(retHaishaRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retHaishaRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_HAISHA_MEMO",
                                                "T_HAISHA_MEMO DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■定期配車
                    retHaishaRange.Clear();
                    retHaishaRange = this.daoConvertClass.TableDeleteFLG("T_TEIKI_HAISHA_ENTRY", delflgWhere);
                    if (!(retHaishaRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retHaishaRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_TEIKI_HAISHA_NIOROSHI",
                                                "T_TEIKI_HAISHA_ENTRY E INNER JOIN T_TEIKI_HAISHA_NIOROSHI DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_TEIKI_HAISHA_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_TEIKI_HAISHA_SHOUSAI",
                                                "T_TEIKI_HAISHA_ENTRY E INNER JOIN T_TEIKI_HAISHA_SHOUSAI DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_TEIKI_HAISHA_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_TEIKI_HAISHA_DETAIL",
                                                "T_TEIKI_HAISHA_ENTRY E INNER JOIN T_TEIKI_HAISHA_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_TEIKI_HAISHA_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_TEIKI_HAISHA_ENTRY",
                                                "T_TEIKI_HAISHA_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■定期配車実績
                    retHaishaRange.Clear();
                    retHaishaRange = this.daoConvertClass.TableDeleteFLG("T_TEIKI_JISSEKI_ENTRY", delflgWhere);
                    if (!(retHaishaRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retHaishaRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_TEIKI_JISSEKI_NIOROSHI",
                                                "T_TEIKI_JISSEKI_ENTRY E INNER JOIN T_TEIKI_JISSEKI_NIOROSHI DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_TEIKI_JISSEKI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_TEIKI_JISSEKI_DETAIL",
                                                "T_TEIKI_JISSEKI_ENTRY E INNER JOIN T_TEIKI_JISSEKI_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_TEIKI_JISSEKI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_TEIKI_JISSEKI_ENTRY",
                                                "T_TEIKI_JISSEKI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion

                    #region ■現場メモ
                    //発生元が、配車('5')に由来するもの、発生元なし('1','')のデータを対象とする
                    delflgWhere = " WHERE DELETE_FLG = 1 AND HASSEIMOTO_CD IN ('1','5','') AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    retHaishaRange.Clear();
                    retHaishaRange = this.daoConvertClass.TableDeleteFLG("T_GENBAMEMO_ENTRY", delflgWhere);
                    if (!(retHaishaRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(retHaishaRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.HASSEIMOTO_CD IN ('1','5','') AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.HASSEIMOTO_CD IN ('1','5','') AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_GENBAMEMO_DETAIL",
                                                "T_GENBAMEMO_ENTRY E INNER JOIN T_GENBAMEMO_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_GENBAMEMO_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_GENBAMEMO_ENTRY",
                                                "T_GENBAMEMO_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    //初期化
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("配車");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/配車");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region 計量
            if (this.form.KeiryouRange_1.Checked == true)
            {
                try
                {
                    DataTable KeiryouRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.KEIRYOU_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■計量
                    KeiryouRange = this.daoConvertClass.TableDeleteFLG("T_KEIRYOU_ENTRY", delflgWhere);
                    if (!(KeiryouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(KeiryouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_KEIRYOU_DETAIL",
                                                "T_KEIRYOU_ENTRY E INNER JOIN T_KEIRYOU_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_KEIRYOU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_KEIRYOU_ENTRY",
                                                "T_KEIRYOU_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■受入実績(DENPYOU_SHURUI⇒１：計量、２：受入
                    delflgWhere = " WHERE DELETE_FLG = 1 AND DENPYOU_SHURUI = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                    delJoin2 = "E.DENPYOU_SHURUI = DelT.DENPYOU_SHURUI AND E.DENPYOU_SYSTEM_ID = DelT.DENPYOU_SYSTEM_ID AND E.SEQ = DelT.SEQ";
                    KeiryouRange = this.daoConvertClass.TableDeleteFLG("T_UKEIRE_JISSEKI_ENTRY", delflgWhere);
                    if (!(KeiryouRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(KeiryouRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.DENPYOU_SHURUI = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.DENPYOU_SHURUI = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UKEIRE_JISSEKI_DETAIL",
                                                "T_UKEIRE_JISSEKI_ENTRY E INNER JOIN T_UKEIRE_JISSEKI_DETAIL DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_UKEIRE_JISSEKI_ENTRY E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_UKEIRE_JISSEKI_ENTRY",
                                                "T_UKEIRE_JISSEKI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    //受入実績で変更したので一応戻しておく
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("計量");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/計量");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region 販売管理
            if (this.form.HankanRange_1.Checked == true)
            {
                try
                {
                    DataTable HankanRange;
                    //日付設定
                    delday = DateTime.Parse(this.form.HANKAN_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region 受入関連
                    #region ■受入
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_UKEIRE_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_CONTENA_RESULT",
                                                "T_UKEIRE_ENTRY E INNER JOIN T_CONTENA_RESULT DelT ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 1",
                                                "INNER JOIN T_UKEIRE_ENTRY E ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 1");
                                InsertAndDelete("T_ZAIKO_HINMEI_HURIWAKE",
                                                "T_UKEIRE_ENTRY E INNER JOIN T_ZAIKO_HINMEI_HURIWAKE DelT ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 1",
                                                "INNER JOIN T_UKEIRE_ENTRY E ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 1");
                                InsertAndDelete("T_UKEIRE_DETAIL",
                                                "T_UKEIRE_ENTRY E INNER JOIN T_UKEIRE_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UKEIRE_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UKEIRE_ENTRY",
                                                "T_UKEIRE_ENTRY DelT" + delWhere2, delWhere2);
                                //Ver2で在庫締が未使用になっているので、関連するテーブルは、V2では対応しない
                                //→(T_ZAIKO_UKEIRE_DETAIL(在庫明細_受入))
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 受入
                    #region ■受入実績(DENPYOU_SHURUI⇒１：計量、２：受入
                    delflgWhere = " WHERE DELETE_FLG = 1 AND DENPYOU_SHURUI = 2 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                    delJoin2 = "E.DENPYOU_SHURUI = DelT.DENPYOU_SHURUI AND E.DENPYOU_SYSTEM_ID = DelT.DENPYOU_SYSTEM_ID AND E.SEQ = DelT.SEQ";
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_UKEIRE_JISSEKI_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.DENPYOU_SHURUI = 2 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.DENPYOU_SHURUI = 2 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UKEIRE_JISSEKI_DETAIL",
                                                "T_UKEIRE_JISSEKI_ENTRY E INNER JOIN T_UKEIRE_JISSEKI_DETAIL DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_UKEIRE_JISSEKI_ENTRY E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_UKEIRE_JISSEKI_ENTRY",
                                                "T_UKEIRE_JISSEKI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    //受入実績で変更したので戻しておく
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■出荷
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_SHUKKA_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_CONTENA_RESULT",
                                                "T_SHUKKA_ENTRY E INNER JOIN T_CONTENA_RESULT DelT ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 2",
                                                "INNER JOIN T_SHUKKA_ENTRY E ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 2");
                                InsertAndDelete("T_ZAIKO_HINMEI_HURIWAKE",
                                                "T_SHUKKA_ENTRY E INNER JOIN T_ZAIKO_HINMEI_HURIWAKE DelT ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 2",
                                                "INNER JOIN T_SHUKKA_ENTRY E ON " + delJoin + delWhere + " AND DelT.DENSHU_KBN_CD = 2");
                                InsertAndDelete("T_KENSHU_DETAIL",
                                                "T_SHUKKA_ENTRY E INNER JOIN T_KENSHU_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_SHUKKA_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_SHUKKA_DETAIL",
                                                "T_SHUKKA_ENTRY E INNER JOIN T_SHUKKA_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_SHUKKA_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_SHUKKA_ENTRY",
                                                "T_SHUKKA_ENTRY DelT" + delWhere2, delWhere2);
                                //Ver2で在庫締が未使用になっているので、関連するテーブルは、V2では対応しない
                                //→(T_ZAIKO_SHUKKA_DETAIL(在庫明細_出荷))
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 出荷
                    #endregion
                    #region 売上支払関連
                    #region ■売上支払
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_UR_SH_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UR_SH_DETAIL",
                                                "T_UR_SH_ENTRY E INNER JOIN T_UR_SH_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UR_SH_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UR_SH_ENTRY",
                                                "T_UR_SH_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 売上支払
                    #endregion
                    #region 入出金関連
                    //手形の登録箇所が無かったのでV2では対応しない
                    //→(T_UKETORI_TEGATA_ENTRY(受取手形入力))
                    #region ■入金➀
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_NYUUKIN_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_NYUUKIN_DETAIL",
                                                "T_NYUUKIN_ENTRY E INNER JOIN T_NYUUKIN_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_NYUUKIN_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_NYUUKIN_ENTRY",
                                                "T_NYUUKIN_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 入金➀
                    #region ■入金②
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_NYUUKIN_SUM_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_KARIUKE_CHOUSEI",
                                                "T_NYUUKIN_SUM_ENTRY E INNER JOIN T_KARIUKE_CHOUSEI DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_NYUUKIN_SUM_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_NYUUKIN_SUM_DETAIL",
                                                "T_NYUUKIN_SUM_ENTRY E INNER JOIN T_NYUUKIN_SUM_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_NYUUKIN_SUM_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_NYUUKIN_SUM_ENTRY",
                                                "T_NYUUKIN_SUM_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 入金②
                    #region ■出金
                    //手形の登録箇所が無かったのでV2では対応しない
                    //→(T_SHIHARAI_TEGATA_ENTRY(支払手形入力))
                    //出金消込のデータの登録箇所が無かったのでV2では対応しない
                    //→(T_SHUKKIN_KESHIKOMI(出金消込))
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_SHUKKIN_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_SHUKKIN_DETAIL",
                                                "T_SHUKKIN_ENTRY E INNER JOIN T_SHUKKIN_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_SHUKKIN_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_SHUKKIN_ENTRY",
                                                "T_SHUKKIN_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 出金
                    #endregion
                    #region 請求・清算関連
                    //■締め関連(履歴データなので削除対象外/作成しっぱなし且つ、DELETE_FLG自体存在しない
                    //→(T_SHIME_SHORI_ERROR(締処理エラー)、T_SHIME_JIKKOU_RIREKI(締実行履歴)、T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI(締実行履歴_取引先))
                    #region ■精算(※再作成時に同じキーを使用しない
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_SEISAN_DENPYOU", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                delJoin2 = "E.SEISAN_NUMBER = DelT.SEISAN_NUMBER";
                                InsertAndDelete("T_SEISAN_DETAIL",
                                                "T_SEISAN_DENPYOU E INNER JOIN T_SEISAN_DETAIL DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_SEISAN_DENPYOU E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_SEISAN_DENPYOU_KAGAMI",
                                                "T_SEISAN_DENPYOU E INNER JOIN T_SEISAN_DENPYOU_KAGAMI DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_SEISAN_DENPYOU E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_SEISAN_DENPYOU",
                                                "T_SEISAN_DENPYOU DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 精算
                    #region ■請求(※再作成時に同じキーを使用しない
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_SEIKYUU_DENPYOU", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                delJoin2 = "E.SEIKYUU_NUMBER = DelT.SEIKYUU_NUMBER";
                                InsertAndDelete("T_NYUUKIN_KESHIKOMI",
                                                "T_SEIKYUU_DENPYOU E INNER JOIN T_NYUUKIN_KESHIKOMI DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_SEIKYUU_DENPYOU E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_SEIKYUU_DETAIL",
                                                "T_SEIKYUU_DENPYOU E INNER JOIN T_SEIKYUU_DETAIL DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_SEIKYUU_DENPYOU E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_SEIKYUU_DENPYOU_KAGAMI",
                                                "T_SEIKYUU_DENPYOU E INNER JOIN T_SEIKYUU_DENPYOU_KAGAMI DelT ON " + delJoin2 + delWhere,
                                                "INNER JOIN T_SEIKYUU_DENPYOU E ON " + delJoin2 + delWhere);
                                InsertAndDelete("T_SEIKYUU_DENPYOU",
                                                "T_SEIKYUU_DENPYOU DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 請求
                    #region ■月次関連➀
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_MONTHLY_LOCK_UR", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MONTHLY_LOCK_UR",
                                                "T_MONTHLY_LOCK_UR DelT " + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 月次関連➀
                    #region ■月次関連②
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_MONTHLY_ADJUST_UR", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MONTHLY_ADJUST_UR",
                                                "T_MONTHLY_ADJUST_UR DelT " + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 月次関連②
                    #region ■月次関連③
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_MONTHLY_LOCK_SH", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MONTHLY_LOCK_SH",
                                                "T_MONTHLY_LOCK_SH DelT " + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 月次関連③
                    #region ■月次関連④
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_MONTHLY_ADJUST_SH", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MONTHLY_ADJUST_SH",
                                                "T_MONTHLY_ADJUST_SH DelT " + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 月次関連④
                    #region ■月次関連⑤
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_MONTHLY_LOCK_ZAIKO", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MONTHLY_LOCK_ZAIKO",
                                                "T_MONTHLY_LOCK_ZAIKO DelT " + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 月次関連⑤
                    #endregion
                    #region 在庫関連
                    #region ■在庫調整(基本修正も削除もできない)
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_ZAIKO_TYOUSEI_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_ZAIKO_TYOUSEI_DETAIL",
                                                "T_ZAIKO_TYOUSEI_ENTRY E INNER JOIN T_ZAIKO_TYOUSEI_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_ZAIKO_TYOUSEI_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_ZAIKO_TYOUSEI_ENTRY",
                                                "T_ZAIKO_TYOUSEI_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 在庫調整
                    #region ■在庫移動(基本修正も削除もできない)
                    HankanRange.Clear();
                    HankanRange = this.daoConvertClass.TableDeleteFLG("T_ZAIKO_IDOU_ENTRY", delflgWhere);
                    if (!(HankanRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(HankanRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_ZAIKO_IDOU_DETAIL",
                                                "T_ZAIKO_IDOU_ENTRY E INNER JOIN T_ZAIKO_IDOU_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_ZAIKO_IDOU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_ZAIKO_IDOU_ENTRY",
                                                "T_ZAIKO_IDOU_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 在庫移動
                    #endregion
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("販売管理");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/販売管理");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region マニ
            if (this.form.ManifestRange_1.Checked == true)
            {
                try
                {
                    DataTable ManifestRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.MANIFEST_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■紐付けデータ
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("T_MANIFEST_RELATION", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MANIFEST_RELATION",
                                                "T_MANIFEST_RELATION DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion 紐付けデータ
                    #region■紙マニ
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("T_MANIFEST_ENTRY", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_MANIFEST_KP_SBN_HOUHOU",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_KP_SBN_HOUHOU DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_KP_NISUGATA",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_KP_NISUGATA DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_KP_KEIJYOU",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_KP_KEIJYOU DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_RET_DATE",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_RET_DATE DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_UPN",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_UPN DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_DETAIL_PRT",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_DETAIL_PRT DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_DETAIL",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_PRT",
                                                "T_MANIFEST_ENTRY E INNER JOIN T_MANIFEST_PRT DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_MANIFEST_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_MANIFEST_ENTRY",
                                                "T_MANIFEST_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region 電マニ
                    //■メモ
                    //1)DT_MF_TOC⇒1マニ1データなので移動しない
                    //2)DT_MF_MEMBER⇒1マニ1データなので移動しない
                    //3)DT_Dxx系⇒SEQで紐づいていないので移動しない
                    //4)★DT_Rxx系⇒tocのlatest_seq/approvalより若いデータのみ移動
                    //5)★DT_Rxx_EX⇒DELETE_FLG = 1のデータを移動
                    //6)JWNET_SEND_LOG⇒蓄積データなので移動しない
                    //7)QUE_INFO⇒蓄積データなので移動しない
                    //8)DT_R24⇒蓄積データなので移動しない

                    delflgWhere = "R INNER JOIN DT_MF_TOC TOC ON R.KANRI_ID = TOC.KANRI_ID AND R.SEQ < TOC.LATEST_SEQ WHERE R.UPDATE_TS <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)➀
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R13", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //最終処分場情報
                                InsertAndDelete("DT_R13",
                                                "DT_MF_TOC TOC INNER JOIN DT_R13 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)②
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R08", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //一次情報
                                InsertAndDelete("DT_R08",
                                                "DT_MF_TOC TOC INNER JOIN DT_R08 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)③
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R06", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //備考
                                InsertAndDelete("DT_R06",
                                                "DT_MF_TOC TOC INNER JOIN DT_R06 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)④
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R05", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //連絡番号
                                InsertAndDelete("DT_R05",
                                                "DT_MF_TOC TOC INNER JOIN DT_R05 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)⑤
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R04", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //最終処分場情報(予定)
                                InsertAndDelete("DT_R04",
                                                "DT_MF_TOC TOC INNER JOIN DT_R04 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)⑥
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R02", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //有害物質
                                InsertAndDelete("DT_R02",
                                                "DT_MF_TOC TOC INNER JOIN DT_R02 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)⑦
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R19", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //運搬
                                InsertAndDelete("DT_R19",
                                                "DT_MF_TOC TOC INNER JOIN DT_R19 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx メモ)4 TOCのLATEST_SEQより前のデータを削除(※APPROVAL_SEQを見ると確定行が消える)⑧
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R18", delflgWhere, "R.UPDATE_TS");
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.UPDATE_TS BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //本体
                                InsertAndDelete("DT_R18",
                                                "DT_MF_TOC TOC INNER JOIN DT_R18 DelT ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2,
                                                "INNER JOIN DT_MF_TOC TOC ON DelT.KANRI_ID = TOC.KANRI_ID AND DelT.SEQ < TOC.LATEST_SEQ" + delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion DT_Rxx メモ)4

                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除➀
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R13_EX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //最終処分場情報
                                InsertAndDelete("DT_R13_EX",
                                                "DT_R13_EX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除②
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R08_EX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //一次情報
                                InsertAndDelete("DT_R08_EX",
                                                "DT_R08_EX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除③
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R04_EX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //最終処分場情報(予定)
                                InsertAndDelete("DT_R04_EX",
                                                "DT_R04_EX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除④
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R19_EX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //運搬
                                InsertAndDelete("DT_R19_EX",
                                                "DT_R19_EX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除⑤
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R18_MIX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //本体
                                InsertAndDelete("DT_R18_MIX",
                                                "DT_R18_MIX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■DT_Rxx_EX メモ)5 EX系はDELETE_FLG=1のデータを削除⑥
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("DT_R18_EX", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //本体
                                InsertAndDelete("DT_R18_EX",
                                                "DT_R18_EX DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #endregion

                    #region■実績報告
                    ManifestRange.Clear();
                    ManifestRange = this.daoConvertClass.TableDeleteFLG("T_JISSEKI_HOUKOKU_ENTRY", delflgWhere);
                    if (!(ManifestRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(ManifestRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_JISSEKI_HOUKOKU_UPN_DETAIL",
                                                "T_JISSEKI_HOUKOKU_ENTRY E INNER JOIN T_JISSEKI_HOUKOKU_UPN_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_JISSEKI_HOUKOKU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_JISSEKI_HOUKOKU_SHORI_DETAIL",
                                                "T_JISSEKI_HOUKOKU_ENTRY E INNER JOIN T_JISSEKI_HOUKOKU_SHORI_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_JISSEKI_HOUKOKU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_JISSEKI_HOUKOKU_SBN_DETAIL",
                                                "T_JISSEKI_HOUKOKU_ENTRY E INNER JOIN T_JISSEKI_HOUKOKU_SBN_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_JISSEKI_HOUKOKU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_JISSEKI_HOUKOKU_MANIFEST_DETAIL",
                                                "T_JISSEKI_HOUKOKU_ENTRY E INNER JOIN T_JISSEKI_HOUKOKU_MANIFEST_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_JISSEKI_HOUKOKU_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_JISSEKI_HOUKOKU_ENTRY",
                                                "T_JISSEKI_HOUKOKU_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("マニ");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/マニ");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region 運賃

            if (this.form.UnchinRange_1.Checked == true)
            {
                try
                {
                    DataTable UnchinRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.UNCHIN_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                    #region ■運賃
                    UnchinRange = this.daoConvertClass.TableDeleteFLG("T_UNCHIN_ENTRY", delflgWhere);
                    if (!(UnchinRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(UnchinRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_UNCHIN_DETAIL",
                                                "T_UNCHIN_ENTRY E INNER JOIN T_UNCHIN_DETAIL DelT ON " + delJoin + delWhere,
                                                "INNER JOIN T_UNCHIN_ENTRY E ON " + delJoin + delWhere);
                                InsertAndDelete("T_UNCHIN_ENTRY",
                                                "T_UNCHIN_ENTRY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("運賃");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/運賃");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            #region 外部連携
            if (this.form.RenkeiRange_1.Checked == true)
            {
                try
                {
                    DataTable RenkeiRange;

                    //日付設定
                    delday = DateTime.Parse(this.form.RENKEI_DAY.Value.ToString()).ToString("yyyy/MM/dd");
                    delflgWhere = " WHERE DELETE_FLG = 1 AND CREATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    //キャッシャ（IDがオートナンバー。
                    //今回は見送り→DELETE_FLGが無い為、削除済みか判断できない。且つ、直近のデータを誤って指定し、削除される可能性がある為。
                    //→(T_CASHERDATA(キャッシャ))

                    #region■入金データ取込(オプション/オンラインバンク連携
                    RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_NYUUKIN_DATA_TORIKOMI", delflgWhere, "CREATE_DATE");
                    if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.CREATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                //delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.CREATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";
                                InsertAndDelete("T_NYUUKIN_DATA_TORIKOMI",
                                                "T_NYUUKIN_DATA_TORIKOMI DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion

                    delflgWhere = " WHERE DELETE_FLG = 1 AND UPDATE_DATE <= '" + DateTime.Parse(delday).ToShortDateString() + " 23:59:59'";

                    //配車系
                    if (AppConfig.Series == "A1" || AppConfig.Series == "A2"
                        || AppConfig.Series == "C1" || AppConfig.Series == "C4" || AppConfig.Series == "C6"
                        || AppConfig.Series == "D1" || AppConfig.Series == "D2"
                        || AppConfig.Series == "TEST")
                    {
                        #region ■配送計画➀
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_LOGI_TO_URSH", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //配送計画to売上支払い
                                    InsertAndDelete("T_LOGI_TO_URSH",
                                                    "T_LOGI_TO_URSH DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion 配送計画➀
                        #region ■配送計画②
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_LOGI_TO_TEIKI", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //配送計画to定期実績
                                    InsertAndDelete("T_LOGI_TO_TEIKI",
                                                    "T_LOGI_TO_TEIKI DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion 配送計画②
                        #region ■配送計画③
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_LOGI_LINK_STATUS", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //配送計画連携状況管理
                                    InsertAndDelete("T_LOGI_LINK_STATUS",
                                                    "T_LOGI_LINK_STATUS DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion 配送計画③
                        #region ■配送計画④
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_LOGI_DELIVERY", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere = " WHERE E.DELETE_FLG = 1 AND E.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //配送計画明細
                                    InsertAndDelete("T_LOGI_DELIVERY_DETAIL",
                                                    "T_LOGI_DELIVERY E INNER JOIN T_LOGI_DELIVERY_DETAIL DelT ON E.SYSTEM_ID = DelT.SYSTEM_ID" + delWhere,
                                                    "INNER JOIN T_LOGI_DELIVERY E ON E.SYSTEM_ID = DelT.SYSTEM_ID" + delWhere);
                                    //配送計画(ロジコン #111150)
                                    InsertAndDelete("T_LOGI_DELIVERY",
                                                    "T_LOGI_DELIVERY DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion 配送計画④

                        #region ■モバイル状況➀
                        //Ver2.1から「モバイル将軍取込」がメニューから削除されたので、V2では対応しない
                        //→(T_MOBILE_SYOGUN_DATA_INSERT(モバイル将軍用データ取込画面専用テーブル))
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_MOBISYO_RT_CONTENA", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //モバイル将軍業務コンテナ
                                    InsertAndDelete("T_MOBISYO_RT_CONTENA",
                                                    "T_MOBISYO_RT_CONTENA DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion モバイル状況➀
                        #region ■モバイル状況②
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_MOBISYO_RT_HANNYUU", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //モバイル将軍業務搬入
                                    InsertAndDelete("T_MOBISYO_RT_HANNYUU",
                                                    "T_MOBISYO_RT_HANNYUU DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion モバイル状況②
                        #region ■モバイル状況③
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_MOBISYO_RT_DTL", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //モバイル将軍業務詳細
                                    InsertAndDelete("T_MOBISYO_RT_DTL",
                                                    "T_MOBISYO_RT_DTL DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion モバイル状況③
                        #region ■モバイル状況④
                        RenkeiRange.Clear();
                        RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_MOBISYO_RT", delflgWhere);
                        if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                        {
                            deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                            int EndLoop = 0;
                            for (int sDay = 0; EndLoop == 0; sDay++)
                            {
                                DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                                DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                                if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                                {
                                    DelEnd = DateTime.Parse(delday);
                                    EndLoop = 1;
                                }
                                delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                                using (var tran = new TransactionUtility())
                                {
                                    //モバイル将軍業務TBL
                                    InsertAndDelete("T_MOBISYO_RT",
                                                    "T_MOBISYO_RT DelT" + delWhere2, delWhere2);
                                    tran.Commit();
                                }
                            }
                        }
                        IncProgressBar();
                        #endregion モバイル状況④
                    }

                    #region ■NAVITIME配送計画➀
                    RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_NAVI_LINK_STATUS", delflgWhere);
                    //範囲内にDELETE_FLGのデータが無ければ処理を行わない
                    if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_NAVI_LINK_STATUS",
                                                "T_NAVI_LINK_STATUS DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                    #region ■NAVITIME配送計画②
                    RenkeiRange = this.daoConvertClass.TableDeleteFLG("T_NAVI_DELIVERY", delflgWhere);
                    //範囲内にDELETE_FLGのデータが無ければ処理を行わない
                    if (!(RenkeiRange.Rows[0]["OLD_DATE"].Equals(DBNull.Value)))
                    {
                        deldayOld = DateTime.Parse(RenkeiRange.Rows[0]["OLD_DATE"].ToString()).AddDays(-1).ToString();
                        int EndLoop = 0;
                        for (int sDay = 0; EndLoop == 0; sDay++)
                        {
                            DelStart = DateTime.Parse(deldayOld).AddDays((HisDelRange * sDay) + 1);   //削除範囲(30) * 周回数 + 1　　→削除の開始位置
                            DelEnd = DateTime.Parse(deldayOld).AddDays(HisDelRange * (sDay + 1));    //削除範囲(30) * (周回数 + 1)　→削除の終了位置
                            if (DateTime.Parse(delday) <= DelEnd)                                   //削除指定日付を超えた場合は、削除指定日を削除の終了位置にする
                            {
                                DelEnd = DateTime.Parse(delday);
                                EndLoop = 1;
                            }
                            delWhere2 = " WHERE DelT.DELETE_FLG = 1 AND DelT.UPDATE_DATE BETWEEN '" + DelStart.ToShortDateString() + " 00:00:00' AND '" + DelEnd.ToShortDateString() + " 23:59:59'";
                            using (var tran = new TransactionUtility())
                            {
                                InsertAndDelete("T_NAVI_DELIVERY",
                                                "T_NAVI_DELIVERY DelT" + delWhere2, delWhere2);
                                tran.Commit();
                            }
                        }
                    }
                    IncProgressBar();
                    #endregion
                }
                catch (SQLRuntimeException ex2)
                {
                    //削除範囲作業履歴更新
                    UpdateOldData("外部連携");

                    LogUtility.Error("TableConvert", ex2);
                    errmessage.MessageBoxShowWarn("データ削除に失敗しました。/外部連携");
                    //パラメーターリセット
                    ResetProgressBar();
                    return false;
                }
            }
            #endregion

            //削除範囲作業履歴更新
            UpdateOldData();

            LogUtility.DebugMethodEnd();

            return true;
        }
        #endregion

        #region データコピー＆データ削除共通処理
        /// <summary>
        /// １）削除範囲のデータをOLDテーブルにコピー　※DelOnlyFLG=0の場合のみ
        /// ２）削除範囲の通常データをを物理削除
        /// 対象：各項目で「1.する」を選択している
        /// </summary>
        /// <param name="TableName">移動対象テーブル名(元になるテーブル)</param>
        /// <param name="SelectWHERE">移動対象の条件を指定（元になるテーブルで条件を指定する）</param>
        /// <param name="DeleteWHERE">削除対象の条件を指定（元になるテーブルで条件を指定する）</param>
        public void InsertAndDelete(string TableName, string SelectWHERE, string DeleteWHERE)
        {

            LogUtility.DebugMethodStart(TableName, SelectWHERE, DeleteWHERE);

            //元テーブルから、物理削除
            int ret = this.daoConvertClass.DeleteTable(TableName, DeleteWHERE);
            //DELETE DelT FROM【TableName】DelT【DeleteWHERE】
            //⇒DELETE DelT FROM T_MITSUMORI_DETAIL DelT INNER JOIN T_MITSUMORI_ENTRY E ON E.SYSTEM_ID = DelT.SYSTEM_ID AND E.SEQ = DelT.SEQ WHERE E.MITSUMORI_DATE <= '2018/08/09' AND E.DELETE_FLG = 1


            //■↓テーブルコピーがある場合はこちらを復活し、↑をコメントアウトする■
            //■テーブルコピーの動作検証はしてないので、使用する場合感ならず動作検証をすること■
            //var Komoku_insert = "";         //INSERTの方のテーブル項目名
            //var Komoku_select = "";         //SELECTの方のテーブル項目名
            ////is_identity→オートナンバー(1)は除外する
            ////user_type_id →データ型(189:timestamp)
            //DataTable tableColumn = this.daoConvertClass.GetTableColumn(TableName);

            //if (tableColumn != null)
            //{
            //    for (int i = 0; i < tableColumn.Rows.Count; i++)
            //    {
            //        if (tableColumn.Rows[i]["name"].ToString() != "TIME_STAMP")
            //        {
            //            //TIME_STAMP以外の項目名を取得。
            //            Komoku_select = Komoku_select + "DelT." + tableColumn.Rows[i]["name"].ToString() + ",";
            //        }
            //    }
            //    if (Komoku_select.Length > 1)
            //    {
            //        Komoku_select = Komoku_select.TrimEnd(',');         //最後のカンマを削除
            //        Komoku_insert = Komoku_select.Replace("DelT.", "");  //INSERT用に、「DelT.」を削除    

            //        if (DelOnlyFLG == 0)
            //        {
            //            //元テーブル→OLDテーブルにコピー
            //            int re = this.daoConvertClass.INSERTOldTable("OLD_" + TableName, Komoku_insert, Komoku_select, SelectWHERE);
            //            //INSERT INTO 【"OLD_" + TableName】(【Komoku_insert】) SELECT DISTINCT【Komoku_select】FROM【SelectWHERE】
            //            //⇒INSERT INTO OLD_T_MITSUMORI_DETAIL (SYSTEM_ID,SEQ,～) SELECT DISTINCT DelT.SYSTEM_ID,DelT.SEQ,～ 
            //            //  　　　　FROM T_MITSUMORI_ENTRY E INNER JOIN T_MITSUMORI_DETAIL DelT ON E.SYSTEM_ID = DelT.SYSTEM_ID AND E.SEQ = DelT.SEQ WHERE E.MITSUMORI_DATE <= '2018/08/09' AND E.DELETE_FLG = 1
            //        }

            //        //元テーブルから、物理削除
            //        int ret = this.daoConvertClass.DeleteTable(TableName, DeleteWHERE);
            //        //DELETE DelT FROM【TableName】DelT【DeleteWHERE】
            //        //⇒DELETE DelT FROM T_MITSUMORI_DETAIL DelT INNER JOIN T_MITSUMORI_ENTRY E ON E.SYSTEM_ID = DelT.SYSTEM_ID AND E.SEQ = DelT.SEQ WHERE E.MITSUMORI_DATE <= '2018/08/09' AND E.DELETE_FLG = 1
            //    }
            //}

            LogUtility.DebugMethodEnd(TableName, SelectWHERE, DeleteWHERE);
        }

        #endregion

        #region 削除実行履歴の作成
        /// <summary>
        /// 削除実行時の履歴を作成する
        /// IDはオートナンバー
        /// </summary>
        public void UpdateOldData(string StrErr = "")
        {

            LogUtility.DebugMethodStart();

            if (this.form.EigyouRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.EIGYOU_DAY = (DateTime)this.form.EIGYOU_DAY.Value;
            }
            if (this.form.UkersukeRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.UKETSUKE_DAY = (DateTime)this.form.UKETSUKE_DAY.Value;
            }
            if (this.form.HaishaRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.HAISHA_DAY = (DateTime)this.form.HAISHA_DAY.Value;
            }
            if (this.form.KeiryouRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.KEIRYOU_DAY = (DateTime)this.form.KEIRYOU_DAY.Value;
            }
            if (this.form.HankanRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.HANKAN_DAY = (DateTime)this.form.HANKAN_DAY.Value;
            }
            if (this.form.ManifestRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.MANIFEST_DAY = (DateTime)this.form.MANIFEST_DAY.Value;
            }
            if (this.form.UnchinRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.UNCHIN_DAY = (DateTime)this.form.UNCHIN_DAY.Value;
            }
            if (this.form.RenkeiRange_1.Checked == true)
            {
                this.dto.OldDataDelEntity.RENKEI_DAY = (DateTime)this.form.RENKEI_DAY.Value;
            }
            this.dto.OldDataDelEntity.ERR_STEP = StrErr;
            var dataBinderOldDataDel = new DataBinderLogic<M_OLD_DATA_DEL>(this.dto.OldDataDelEntity);
            dataBinderOldDataDel.SetSystemProperty(this.dto.OldDataDelEntity, false);

            this.daoOldDataDel.Insert(this.dto.OldDataDelEntity);

            LogUtility.DebugMethodEnd();
        }
        #endregion

    }
}
