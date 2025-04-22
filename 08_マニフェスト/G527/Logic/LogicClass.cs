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
using Seasar.Framework.Exceptions;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Setting.ButtonSetting.xml";
        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;
        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;
        /// <summary>
        /// DAOClass
        /// </summary>
        private DAOClass mDao;
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
         /// <summary>
         /// 拠点情報
         /// </summary>
        private IM_KYOTENDao kyotenDao;
         /// <summary>
         /// 
         /// </summary>
        private MessageBoxShowLogic messageShowLogic;
        /// <summary>
        /// 帳票情報データ
        /// </summary>
        private DataTable mReportInfoData;
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
        /// 検索条件 DTO
        /// </summary>
        public DTOClass serchCMDto { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public JoukenParam mJoukenParam { get; set; }
        /// <summary>
        /// 帳票情報を保持するプロパティ
        /// </summary>
        public ReportInfoR407 ReportInfo { get; set; }
        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity { get; set; }       
       
        /// <summary>
        /// 拠点情報
        /// </summary>
        public M_KYOTEN mKyotenInfo { get; set; }
        /// <summary>
        /// 帳票表示単位名
        /// </summary>
        private string unit = string.Empty;

        /// <summary>
        /// 帳票表示用会社名
        /// </summary>
        private string corpName = string.Empty;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.mDao = DaoInitUtility.GetComponent<DAOClass>(); //DAO
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();//システム情報のDao
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>(); //拠点情報のDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            messageShowLogic = new MessageBoxShowLogic();
            // 単位取得
            this.unit = this.mDao.GetUnit();
            // 会社名取得処理
            this.corpName = this.mDao.GetCorpName();

            LogUtility.DebugMethodEnd();
        }       
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダー項目
                this.headForm = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm; 
                // システム情報を取得
                this.GetSysInfoInit();
                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);
                // イベントの初期化処理
                this.EventInit(parentForm);
                this.parentForm = parentForm;
                //画面初期化処理
                this.DisplyInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
                    //// 数量フォーマット
                    //this.systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                    if (this.sysInfoEntity != null)
                    {
                        ////システム情報からアラート件数を取得
                        //this.alertCount = (int)this.sysInfoEntity.ICHIRAN_ALERT_KENSUU;
                        //this.headForm.alertNumber.Text = this.alertCount.ToString();
                        
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
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private M_KYOTEN GetKyotenProfileValue()
        {
            try
            {
                LogUtility.DebugMethodStart();
                CurrentUserCustomConfigProfile profile = CurrentUserCustomConfigProfile.Load();
                string result = string.Empty;

                foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
                {
                    if (item.Name.Equals("拠点CD"))
                    {
                        result = item.Value;
                    }
                }
                //拠点情報
                M_KYOTEN kyotenP = null;
                if (!string.IsNullOrEmpty(result))
                {
                    kyotenP = kyotenDao.GetDataByCd(result);
                }

                //拠点名称
                if (kyotenP != null && result != string.Empty)
                {
                    this.form.cstmANTexB_Kyoten.Text = result.PadLeft(this.form.cstmANTexB_Kyoten.MaxLength, '0'); ;
                    this.form.cstmTexBox_Kyoten.Text = kyotenP.KYOTEN_NAME_RYAKU;
                }
                else
                {
                    //拠点CD、拠点 : ブランク
                    this.form.cstmANTexB_Kyoten.Text = string.Empty;
                    this.form.cstmTexBox_Kyoten.Text = string.Empty;
                }
                return kyotenP;
            }
            catch (Exception ex)
            {
                LogUtility.Error( ex);
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

                // 印刷ボタン(F5)イベント
                parentForm.bt_func5.Click += new EventHandler(this.form.Print);

                // CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);
                //条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.ClearCondition);
                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new System.EventHandler(this.form.FormClose);
               // parentForm.ProcessButtonPanel.Visible = false;
                parentForm.txb_process.Enabled = false;

                /// 20141023 Houkakou 「マニ推移表」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.cstmDateTimePicker_NengappiTo.MouseDoubleClick += new MouseEventHandler(cstmDateTimePicker_NengappiTo_MouseDoubleClick);
                this.form.cstmANTexB_HaishutuJigyoushaTo.MouseDoubleClick += new MouseEventHandler(cstmANTexB_HaishutuJigyoushaTo_MouseDoubleClick);
                this.form.cstmANTexB_HaisyutsuJigyoubaTo.MouseDoubleClick += new MouseEventHandler(cstmANTexB_HaisyutsuJigyoubaTo_MouseDoubleClick);
                this.form.cstmANTexB_UnpanJutakushaTo.MouseDoubleClick += new MouseEventHandler(cstmANTexB_UnpanJutakushaTo_MouseDoubleClick);
                this.form.cstmANTexB_ShobunJutakushaTo.MouseDoubleClick += new MouseEventHandler(cstmANTexB_ShobunJutakushaTo_MouseDoubleClick);
                this.form.cstmANTexB_SaishuuShobunJouTo.MouseDoubleClick += new MouseEventHandler(cstmANTexB_SaishuuShobunJouTo_MouseDoubleClick);
                /// 20141023 Houkakou 「マニ推移表」のダブルクリックを追加する　end

                /// 20141209 teikyou 日付チェックを追加する　start
                this.form.cstmDateTimePicker_NengappiFrom.Leave += new System.EventHandler(cstmDateTimePicker_NengappiFrom_Leave);
                this.form.cstmDateTimePicker_NengappiTo.Leave += new System.EventHandler(cstmDateTimePicker_NengappiTo_Leave);
                /// 20141209 teikyou 日付チェックを追加する　end
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
        ///画面初期化処理
        ///</summary>
        private void DisplyInit()
        {
            //  1.初期値をセット
            // 「一次二次区分」 = 1
            this.form.cstmNmTxtB_IchijiNiji.Text = "1";
            //「年月日From」 = (現在の年度)+'/4/1'
            this.form.cstmDateTimePicker_NengappiFrom.Value = DateTime.Parse((this.parentForm.sysDate.Year).ToString() + "/04/01");
            // 「年月日To」 = (現在の年度+1)+'/3/31'
            this.form.cstmDateTimePicker_NengappiTo.Value = DateTime.Parse((this.parentForm.sysDate.Year + 1).ToString() + "/04/01").AddDays(-1);
          
            //出力内容
            this.form.cstmNmTxtB_ShuturyokuNaiyou.Text = "1";
            //出力区分
            this.form.cstmNmTxtB_ShuturyokuKubun.Text = "1";
            
             // 「拠点CD」 =　Blank

             //「排出事業者CDFrom」 = Blank
            this.form.cstmANTexB_HaishutuJigyoushaFrom.Text = string.Empty;
            this.form.cstmTexBox_HaishutuJigyoushaFrom.Text = string.Empty;
             //「排出事業者CDTo」 = Blank
            this.form.cstmANTexB_HaishutuJigyoushaTo.Text = string.Empty;
            this.form.cstmTexBox_HaishutuJigyoushaTo.Text = string.Empty;
             // 「排出事業場CDFrom」 = Blank
            this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text = string.Empty;
            this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
             // 「排出事業場CDTo」 = Blank
            this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text = string.Empty;
            this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
             // 「運搬受託者CDFrom」 = Blank
            this.form.cstmANTexB_UnpanJutakushaFrom.Text = string.Empty;
            this.form.cstmTexBox_UnpanJutakushaFrom.Text = string.Empty;
             // 「運搬受託者CDTo」 = Blank
            this.form.cstmANTexB_UnpanJutakushaTo.Text = string.Empty;
            this.form.cstmTexBox_UnpanJutakushaTo.Text = string.Empty;
             // 「処分受託者CDFrom」　= Blank
            this.form.cstmANTexB_ShobunJutakushaFrom.Text = string.Empty;
            this.form.cstmTexBox_ShobunJutakushaFrom.Text = string.Empty;
             // 「処分受託者CDTo」 = Blank
            this.form.cstmANTexB_ShobunJutakushaTo.Text = string.Empty;
            this.form.cstmTexBox_ShobunJutakushaTo.Text = string.Empty;
             // 「最終処分事業場CDFrom」 = Blank
            this.form.cstmANTexB_SaishuuShobunJouFrom.Text = string.Empty;
            this.form.cstmTexBox_SaishuuShobunJouFrom.Text = string.Empty;
             // 「最終処分事業場CDTo」 = Blank
            this.form.cstmANTexB_SaishuuShobunJouTo.Text = string.Empty;
            this.form.cstmTexBox_SaishuuShobunJouTo.Text = string.Empty;
             // 「産廃（直行）廃棄物種類CD」 = Blank
            this.form.cstmANTexB_Chokkou.Text = string.Empty;
            this.form.cstmTexBox_Chokkou.Text = string.Empty;
             // 「産廃（積替）廃棄物種類CD」 = Blank
            this.form.cstmANTexB_Tsumikae.Text = string.Empty;
            this.form.cstmTexBox_Tsumikae.Text = string.Empty;
             // 「建廃廃棄物種類CD」 = Blank
            this.form.cstmANTexB_Kenpai.Text = string.Empty;
            this.form.cstmTexBox_Kenpai.Text = string.Empty;
             // 「電子廃棄物種類CD」 = Blank
            this.form.cstmANTexB_Denshi.Text = string.Empty;
            this.form.cstmTexBox_Denshi.Text = string.Empty;           
            
            //グリッドの設定
            if (!this.GridViewInit()) { return; }
            //拠点情報取得
            this.mKyotenInfo = this.GetKyotenProfileValue();

            // 排出事業場制御
            this.form.HaisyutsuJigyoubaSeigyo();
        }

         /// <summary>
        /// グリッドの設定
         /// </summary>
        public bool GridViewInit()
        {
            bool ret = true;
            try
            {
                // グリッドの設定
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                this.MakeCustumDataGridView();
                this.CustumDataGridViewInitDisp();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GridViewInit", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region [F1]を取得
        /// <summary>
        /// 前年度
        /// </summary>
        internal bool GetPreviousNumber()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();
                DateTime dateFrom = DateTime.Parse(this.form.cstmDateTimePicker_NengappiFrom.Text);
                DateTime dateTo = DateTime.Parse(this.form.cstmDateTimePicker_NengappiTo.Text);
                //「「年月日From」 = 「年月日From」ー１(年)
                this.form.cstmDateTimePicker_NengappiFrom.Value = DateTime.Parse((dateFrom.Year -1).ToString() + "/"+dateFrom.Month+"/"+dateFrom.Day);
                // 「年月日To」 = 「年月日From」ー１(年)
                this.form.cstmDateTimePicker_NengappiTo.Value = DateTime.Parse((dateTo.Year - 1).ToString() + "/" + dateTo.Month + "/" + dateTo.Day);
               　//検索
                this.Search();
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

        #region [F2]を取得
        /// <summary>
        /// 翌年度
        /// </summary>
        internal bool GetNextNumber()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();
                DateTime dateFrom = DateTime.Parse(this.form.cstmDateTimePicker_NengappiFrom.Text);
                DateTime dateTo = DateTime.Parse(this.form.cstmDateTimePicker_NengappiTo.Text);
                //「「年月日From」 = 「年月日From」+１(年)
                this.form.cstmDateTimePicker_NengappiFrom.Value = DateTime.Parse((dateFrom.Year + 1).ToString() + "/" + dateFrom.Month + "/" + dateFrom.Day);
                // 「年月日To」 = 「年月日From」+１(年)
                this.form.cstmDateTimePicker_NengappiTo.Value = DateTime.Parse((dateTo.Year + 1).ToString() + "/" + dateTo.Month + "/" + dateTo.Day);
                //検索
                this.Search();
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
                // 画面表示名称（種類)
                string layoutName = string.Empty;

                if (this.form.customDataGridView1.Rows.Count <= 0)
                {
                    //読込データ件数を0にする                 
                    messageShowLogic.MessageBoxShow("E044");
                    returnVal = false;
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                #region G128の処理
                ReportInfoR391 report_r391 = new ReportInfoR391();
                DataTable printData = new DataTable();

                if ("1".Equals(this.mJoukenParam.syuturyokuNaiyoiu))
                {
                    report_r391.OutputFormLayout = "LAYOUT1";
                    // 排出事業者別
                    printData = this.MekePrintDataHaishutu();
                }
                else if ("2".Equals(this.mJoukenParam.syuturyokuNaiyoiu))
                {
                    report_r391.OutputFormLayout = "LAYOUT3";
                    // 運搬受託者別
                    printData = this.MekePrintDataUnpan();
                }
                else if ("3".Equals(this.mJoukenParam.syuturyokuNaiyoiu))
                {
                    report_r391.OutputFormLayout = "LAYOUT1";
                    // 処分受託者別
                    printData = this.MekePrintDataShobun();
                }
                else if ("4".Equals(this.mJoukenParam.syuturyokuNaiyoiu))
                {
                    report_r391.OutputFormLayout = "LAYOUT2";
                    // 最終処分場所別
                    printData = this.MekePrintDataSaishuu();
                }
                else if ("5".Equals(this.mJoukenParam.syuturyokuNaiyoiu))
                {
                    report_r391.OutputFormLayout = "LAYOUT2";
                    // 廃棄物種類別
                    printData = this.MekePrintDataHaiki();
                }

                // 画面側から渡ってきた帳票用データテーブルを引数へ設定
                report_r391.R391_Report(printData);

                // 印刷ポツプアップ画面表示
                using (FormReportPrintPopup report = new FormReportPrintPopup(report_r391))
                {
                    report.ShowDialog();
                    report.Dispose();
                }

                #endregion
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

        #region 帳票情報取得

        /// <summary>
        /// 印刷用のデータを作成する（排出事業者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataHaishutu()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"1\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 排出事業者CD
            string haishutuJigyoushaCd = this.form.customDataGridView1.Rows[0].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString();

            string condtion = "HAISHUTU_JIGYOUSHA_CD = '" + haishutuJigyoushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 排出事業者CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_CD"]) + "\",";
            // 排出事業者名
            mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_MEISHOU"]) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";               
                // 排出事業場CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_CD"]) + "\",";
                // 排出事業場名
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_MEISHOU"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 8; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);

                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 排出事業者計-月別
            string haishutuTukibetu = string.Empty;

            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                }
                haishutuTukibetu += dataRowVal.ToString() + ",";
            }

            haishutuTukibetu = this.Get12Tukibetu(haishutuTukibetu);

            // 排出事業者計-月別
            mesiData += "\"" + haishutuTukibetu + "\",";
            // 排出事業者計-合計
            mesiData += "\"" + this.GetGoukei(haishutuTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                if (!haishutuJigyoushaCd.Equals(
                    this.form.customDataGridView1.Rows[k].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString()))
                {
                    // 排出事業者CD
                    haishutuJigyoushaCd = this.form.customDataGridView1.Rows[k].Cells["HAISHUTU_JIGYOUSHA_CD"].Value.ToString();

                    condtion = "HAISHUTU_JIGYOUSHA_CD = '" + haishutuJigyoushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 排出事業者CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_CD"]) + "\",";

                    // 排出事業者名
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["HAISHUTU_JIGYOUSHA_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";
                        // 排出事業場CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_CD"]) + "\",";
                        // 排出事業場名
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAISHUTU_JIGYOUJOU_MEISHOU"]) + "\",";

                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int j = 8; j < gridData.Columns.Count; j++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);

                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 排出事業者計-月別
                    haishutuTukibetu = string.Empty;

                    for (int i = 8; i < gridData.Columns.Count; i++)
                    {
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                        }
                        haishutuTukibetu += dataRowVal.ToString() + ",";
                    }

                    haishutuTukibetu = this.Get12Tukibetu(haishutuTukibetu);

                    // 排出事業者計-月別
                    mesiData += "\"" + haishutuTukibetu + "\",";
                    // 排出事業者計-合計
                    mesiData += "\"" + this.GetGoukei(haishutuTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（運搬受託者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataUnpan()
        {

            LogUtility.DebugMethodStart();

            string mesiData = string.Empty;
            // 合計-月別
            string goukeiTukibetu = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"2\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 廃棄物区分
            string haikibutuKubun = this.form.customDataGridView1.Rows[0].Cells["HAIKIBUTU_KUBUN"].Value.ToString();
            // 運搬受託者CD
            string unpanJutakushaCd = this.form.customDataGridView1.Rows[0].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

            string condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            for (int j = 0; j < dataRow.Length; j++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-1\",";

                // 廃棄物区分ラベル
                mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                // 運搬受託者CD
                mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                // 運搬受託者名
                mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int k = 7; k < gridData.Columns.Count; k++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                }
                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                mesiData += "\"" + haikibutuTukibetu + "\",";

                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－２
            mesiData = "\"2-2\",";

            // 運搬受託者計-月別
            string unpanTukibetu = string.Empty;

            for (int j = 7; j < gridData.Columns.Count; j++)
            {
                decimal dataRowVal = 0;
                for (int k = 0; k < dataRow.Length; k++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[k][j]));
                }
                unpanTukibetu += dataRowVal.ToString() + ",";
            }
            unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

            // 運搬受託者計ラベル
            mesiData += "\"" + "運搬受託者計" + "\",";

            // 運搬受託者計-月別
            mesiData += "\"" + unpanTukibetu + "\",";
            // 運搬受託者計-合計
            mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
            {
                // 廃棄物区分が変わる
                if (!haikibutuKubun.Equals(this.form.customDataGridView1.Rows[i].Cells["HAIKIBUTU_KUBUN"].Value.ToString()))
                {
                    // 廃棄物区分
                    haikibutuKubun = this.form.customDataGridView1.Rows[i - 1].Cells["HAIKIBUTU_KUBUN"].Value.ToString();

                    // ２－３
                    mesiData = "\"2-3\",";

                    condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "'";
                    dataRow = gridData.Select(condtion);

                    // 合計-月別
                    goukeiTukibetu = string.Empty;
                    for (int j = 7; j < gridData.Columns.Count; j++)
                    {
                        decimal dataRowVal = 0;
                        for (int k = 0; k < dataRow.Length; k++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[k][j]));
                        }
                        goukeiTukibetu += dataRowVal.ToString() + ",";
                    }
                    goukeiTukibetu = this.Get12Tukibetu(goukeiTukibetu);

                    // 合計ラベル
                    if ("直行".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "直行用合計" + "\",";
                    }
                    else if ("建廃".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "建廃用合計" + "\",";
                    }
                    else if ("積替".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "積替用合計" + "\",";
                    }
                    else if ("電子".Equals(haikibutuKubun))
                    {
                        mesiData += "\"" + "電子用合計" + "\",";
                    }

                    // 合計-月別
                    mesiData += "\"" + goukeiTukibetu + "\",";
                    // 合計-合計
                    mesiData += "\"" + this.GetGoukei(goukeiTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // 廃棄物区分
                    haikibutuKubun = this.form.customDataGridView1.Rows[i].Cells["HAIKIBUTU_KUBUN"].Value.ToString();
                    // 運搬受託者CD
                    unpanJutakushaCd = this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

                    condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun
                           + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    for (int j = 0; j < dataRow.Length; j++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-1\",";

                        // 廃棄物区分ラベル
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                        // 運搬受託者CD
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                        // 運搬受託者名
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int k = 7; k < gridData.Columns.Count; k++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                        }
                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        mesiData += "\"" + haikibutuTukibetu + "\",";

                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－２
                    mesiData = "\"2-2\",";

                    // 運搬受託者計-月別
                    unpanTukibetu = string.Empty;

                    for (int j = 7; j < gridData.Columns.Count; j++)
                    {
                        decimal dataRowVal = 0;
                        for (int k = 0; k < dataRow.Length; k++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[k][j]));
                        }
                        unpanTukibetu += dataRowVal.ToString() + ",";
                    }
                    unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

                    // 運搬受託者計ラベル
                    mesiData += "\"" + "運搬受託者計" + "\",";

                    // 運搬受託者計-月別
                    mesiData += "\"" + unpanTukibetu + "\",";
                    // 運搬受託者計-合計
                    mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
                else
                {
                    // 運搬受託者CDが変わる
                    if (!unpanJutakushaCd.Equals(
                        this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString()))
                    {
                        // 運搬受託者CD
                        unpanJutakushaCd = this.form.customDataGridView1.Rows[i].Cells["UNPAN_JUTAKUSHA_CD"].Value.ToString();

                        condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun
                               + "' and UNPAN_JUTAKUSHA_CD = '" + unpanJutakushaCd + "'";
                        dataRow = gridData.Select(condtion);

                        // ２－１
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            // 廃棄物種類-月別
                            string haikibutuTukibetu = string.Empty;
                            mesiData = "\"2-1\",";

                            // 廃棄物区分ラベル
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_KUBUN"]) + "用" + "\",";

                            // 運搬受託者CD
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_CD"]) + "\",";

                            // 運搬受託者名
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["UNPAN_JUTAKUSHA_MEISHOU"]) + "\",";

                            // 廃棄物種類
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";

                            // 単位
                            mesiData += "\"" + this.GetDbValue(dataRow[j]["TANI"]) + "\",";

                            // 廃棄物種類-月別
                            for (int k = 7; k < gridData.Columns.Count; k++)
                            {
                                haikibutuTukibetu += this.GetDbValue(dataRow[j][k]) + ",";
                            }
                            haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                            mesiData += "\"" + haikibutuTukibetu + "\",";

                            // 廃棄物種類-合計
                            mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                            dr = returnTable.NewRow();
                            dr[0] = mesiData;
                            returnTable.Rows.Add(dr);
                        }

                        // ２－２
                        mesiData = "\"2-2\",";

                        // 運搬受託者計-月別
                        unpanTukibetu = string.Empty;

                        for (int j = 7; j < gridData.Columns.Count; j++)
                        {
                            decimal dataRowVal = 0;
                            for (int k = 0; k < dataRow.Length; k++)
                            {
                                dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[k][j]));
                            }
                            unpanTukibetu += dataRowVal.ToString() + ",";
                        }
                        unpanTukibetu = this.Get12Tukibetu(unpanTukibetu);

                        // 運搬受託者計ラベル
                        mesiData += "\"" + "運搬受託者計" + "\",";

                        // 運搬受託者計-月別
                        mesiData += "\"" + unpanTukibetu + "\",";
                        // 運搬受託者計-合計
                        mesiData += "\"" + this.GetGoukei(unpanTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }
                }
            }

            // ２－３
            mesiData = "\"2-3\",";

            condtion = "HAIKIBUTU_KUBUN = '" + haikibutuKubun + "'";
            dataRow = gridData.Select(condtion);

            // 合計-月別
            goukeiTukibetu = string.Empty;
            for (int j = 7; j < gridData.Columns.Count; j++)
            {
                decimal dataRowVal = 0;
                for (int k = 0; k < dataRow.Length; k++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[k][j]));
                }
                goukeiTukibetu += dataRowVal.ToString() + ",";
            }
            goukeiTukibetu = this.Get12Tukibetu(goukeiTukibetu);

            // 合計ラベル
            if ("直行".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "直行用合計" + "\",";
            }
            else if ("建廃".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "建廃用合計" + "\",";
            }
            else if ("積替".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "積替用合計" + "\",";
            }
            else if ("電子".Equals(haikibutuKubun))
            {
                mesiData += "\"" + "電子用合計" + "\",";
            }

            // 合計-月別
            mesiData += "\"" + goukeiTukibetu + "\",";
            // 合計-合計
            mesiData += "\"" + this.GetGoukei(goukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 7; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();

            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（処分受託者別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataShobun()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            dr = returnTable.NewRow();

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"3\",\"" + this.corpName + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            dr = returnTable.NewRow();
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 処分受託者CD
            string shobunJutakushaCd = this.form.customDataGridView1.Rows[0].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString();

            string condtion = "SHOBUN_JUTAKUSHA_CD = '" + shobunJutakushaCd + "'";
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 処分受託者CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_CD"]) + "\",";
            // 処分受託者名
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_MEISHOU"]) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";

                // 処分事業場CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_CD"]) + "\",";
                // 処分事業場名
                mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_MEISHOU"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 8; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 処分受託者計-月別
            string shobunTukibetu = string.Empty;

            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                }
                shobunTukibetu += dataRowVal.ToString() + ",";
            }

            shobunTukibetu = this.Get12Tukibetu(shobunTukibetu);

            // 処分受託者計-月別
            mesiData += "\"" + shobunTukibetu + "\",";
            // 処分受託者計-合計
            mesiData += "\"" + this.GetGoukei(shobunTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                if (!shobunJutakushaCd.Equals(
                    this.form.customDataGridView1.Rows[k].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString()))
                {
                    // 処分受託者CD
                    shobunJutakushaCd = this.form.customDataGridView1.Rows[k].Cells["SHOBUN_JUTAKUSHA_CD"].Value.ToString();

                    condtion = "SHOBUN_JUTAKUSHA_CD = '" + shobunJutakushaCd + "'";
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 処分受託者CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_CD"]) + "\",";
                    // 処分受託者名
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SHOBUN_JUTAKUSHA_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";

                        // 処分事業場CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_CD"]) + "\",";
                        // 処分事業場名
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["SHOBUN_JIGYOUJOU_MEISHOU"]) + "\",";
                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                        // 廃棄物種類-月別
                        for (int j = 8; j < gridData.Columns.Count; j++)
                        {
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 処分受託者計-月別
                    shobunTukibetu = string.Empty;

                    for (int i = 8; i < gridData.Columns.Count; i++)
                    {
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                        }
                        shobunTukibetu += dataRowVal.ToString() + ",";
                    }

                    shobunTukibetu = this.Get12Tukibetu(shobunTukibetu);

                    // 処分受託者計-月別
                    mesiData += "\"" + shobunTukibetu + "\",";
                    // 処分受託者計-合計
                    mesiData += "\"" + this.GetGoukei(shobunTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（最終処分場所別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataSaishuu()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;
            dr = returnTable.NewRow();

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"4\",\"" + this.corpName + "\"";
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // 最終処分場所CD
            string saishuuShobunjouCd = this.form.customDataGridView1.Rows[0].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString();
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start

            //string condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'";

            // 最終処分業者CD
            string saishuuShobungyoshaCd = this.form.customDataGridView1.Rows[0].Cells["SAISHUU_SHOBUNGYOSHA_CD"].Value.ToString();
            string condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'" + " AND " + "SAISHUU_SHOBUNGYOSHA_CD = '" + saishuuShobungyoshaCd + "'";

            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
            DataRow[] dataRow = gridData.Select(condtion);

            // ２－１
            mesiData = "\"2-1\",";

            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            // 最終処分業者CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNGYOSHA_CD"]) + "\",";
            // 最終処分業者名称
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNGYOSHA_MEISHOU"]) + "\",";
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end

            // 最終処分場所CD
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_CD"]) + "\",";
            // 最終処分場所
            mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_MEISHOU"]) + "\"";


            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // ２－２
            for (int i = 0; i < dataRow.Length; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-2\",";

                // 廃棄物種類CD
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                //for (int j = 6; j < gridData.Columns.Count; j++)
                //{
                for (int j = 8; j < gridData.Columns.Count; j++)
                {
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－３
            mesiData = "\"2-3\",";

            // 最終処分場所計-月別
            string saishuuTukibetu = string.Empty;
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            //for (int i = 6; i < gridData.Columns.Count; i++)
            //{
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                decimal dataRowVal = 0;
                for (int j = 0; j < dataRow.Length; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                }
                saishuuTukibetu += dataRowVal.ToString() + ",";
            }

            saishuuTukibetu = this.Get12Tukibetu(saishuuTukibetu);

            // 最終処分場所計-月別
            mesiData += "\"" + saishuuTukibetu + "\",";
            // 最終処分場所計-合計
            mesiData += "\"" + this.GetGoukei(saishuuTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            for (int k = 0; k < this.form.customDataGridView1.Rows.Count; k++)
            {
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                //if (!saishuuShobunjouCd.Equals(
                //    this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString()))
                //{
                if (!saishuuShobunjouCd.Equals(
                   this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString()) ||
                    !saishuuShobungyoshaCd.Equals(
                   this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNGYOSHA_CD"].Value.ToString()) 
                    )
                {
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    // 最終処分場所CD
                    saishuuShobunjouCd = this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNJOU_CD"].Value.ToString();
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start

                    //condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'";
                    // 最終処分業者CD
                    saishuuShobungyoshaCd = this.form.customDataGridView1.Rows[k].Cells["SAISHUU_SHOBUNGYOSHA_CD"].Value.ToString();
                    condtion = "SAISHUU_SHOBUNJOU_CD = '" + saishuuShobunjouCd + "'" + " AND " + "SAISHUU_SHOBUNGYOSHA_CD = '" + saishuuShobungyoshaCd + "'";

                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    dataRow = gridData.Select(condtion);

                    // ２－１
                    mesiData = "\"2-1\",";

                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start

                    // 最終処分業者CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNGYOSHA_CD"]) + "\",";
                    // 最終処分業者
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNGYOSHA_MEISHOU"]) + "\",";

                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end

                    // 最終処分場所CD
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_CD"]) + "\",";
                    // 最終処分場所
                    mesiData += "\"" + this.GetDbValue(dataRow[0]["SAISHUU_SHOBUNJOU_MEISHOU"]) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);

                    // ２－２
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        // 廃棄物種類-月別
                        string haikibutuTukibetu = string.Empty;
                        mesiData = "\"2-2\",";

                        // 廃棄物種類CD
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                        // 廃棄物種類
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                        // 単位
                        mesiData += "\"" + this.GetDbValue(dataRow[i]["TANI"]) + "\",";
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                        // 廃棄物種類-月別
                        //for (int j = 6; j < gridData.Columns.Count; j++)
                        //{
                        for (int j = 8; j < gridData.Columns.Count; j++)
                        {
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                            haikibutuTukibetu += this.GetDbValue(dataRow[i][j]) + ",";
                        }

                        haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                        // 廃棄物種類-月別
                        mesiData += "\"" + haikibutuTukibetu + "\",";
                        // 廃棄物種類-合計
                        mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                        dr = returnTable.NewRow();
                        dr[0] = mesiData;
                        returnTable.Rows.Add(dr);
                    }

                    // ２－３
                    mesiData = "\"2-3\",";

                    // 最終処分場所計-月別
                    saishuuTukibetu = string.Empty;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    //for (int i = 6; i < gridData.Columns.Count; i++)
                    //{
                    for (int i = 8; i < gridData.Columns.Count; i++)
                    {
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                        decimal dataRowVal = 0;
                        for (int j = 0; j < dataRow.Length; j++)
                        {
                            dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(dataRow[j][i]));
                        }
                        saishuuTukibetu += dataRowVal.ToString() + ",";
                    }

                    saishuuTukibetu = this.Get12Tukibetu(saishuuTukibetu);

                    // 最終処分場所計-月別
                    mesiData += "\"" + saishuuTukibetu + "\",";
                    // 最終処分場所計-合計
                    mesiData += "\"" + this.GetGoukei(saishuuTukibetu) + "\"";

                    dr = returnTable.NewRow();
                    dr[0] = mesiData;
                    returnTable.Rows.Add(dr);
                }
            }

            // ２－４
            mesiData = "\"2-4\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            //for (int i = 6; i < gridData.Columns.Count; i++)
            //{
            for (int i = 8; i < gridData.Columns.Count; i++)
            {
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 印刷用のデータを作成する（廃棄物種類別）
        /// </summary>
        /// <returns></returns>
        private DataTable MekePrintDataHaiki()
        {
            LogUtility.DebugMethodStart();
            string mesiData = string.Empty;

            DataTable gridData = new DataTable();
            DataTable returnTable = new DataTable();
            returnTable.Columns.Add();

            // ０－１
            DataRow dr;

            // レイアウトNo、会社名
            mesiData = "\"0-1\",\"5\",\"" + this.corpName + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            // １－１
            // 一次二次区分、月カラム
            mesiData = "\"1-1\",\"" + this.GetManikbnNm() + "\",\"" + this.GetTukiTitle() + "\"";
            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            gridData = (DataTable)this.form.customDataGridView1.DataSource;

            // ２－１
            for (int i = 0; i < gridData.Rows.Count; i++)
            {
                // 廃棄物種類-月別
                string haikibutuTukibetu = string.Empty;
                mesiData = "\"2-1\",";

                // 廃棄物種類CD
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["HAIKIBUTU_SHURUI_CD"]) + "\",";
                // 廃棄物種類
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["HAIKIBUTU_SHURUI_MEISHOU"]) + "\",";
                // 単位
                mesiData += "\"" + this.GetDbValue(gridData.Rows[i]["TANI"]) + "\",";

                // 廃棄物種類-月別
                for (int j = 4; j < gridData.Columns.Count; j++)
                {
                    haikibutuTukibetu += this.GetDbValue(gridData.Rows[i][j]) + ",";
                }

                haikibutuTukibetu = this.Get12Tukibetu(haikibutuTukibetu);
                // 廃棄物種類-月別
                mesiData += "\"" + haikibutuTukibetu + "\",";
                // 廃棄物種類-合計
                mesiData += "\"" + this.GetGoukei(haikibutuTukibetu) + "\"";

                dr = returnTable.NewRow();
                dr[0] = mesiData;
                returnTable.Rows.Add(dr);
            }

            // ２－２
            mesiData = "\"2-2\",";

            // 総合計-月別
            string sougoukeiTukibetu = string.Empty;
            for (int i = 4; i < gridData.Columns.Count; i++)
            {
                decimal dataRowVal = 0;
                for (int j = 0; j < gridData.Rows.Count; j++)
                {
                    dataRowVal += Convert.ToDecimal(this.GetDbValueDecimal(gridData.Rows[j][i]));
                }
                sougoukeiTukibetu += dataRowVal.ToString() + ",";
            }
            sougoukeiTukibetu = this.Get12Tukibetu(sougoukeiTukibetu);

            // 総合計-月別
            mesiData += "\"" + sougoukeiTukibetu + "\",";
            // 総合計-合計
            mesiData += "\"" + this.GetGoukei(sougoukeiTukibetu) + "\"";

            dr = returnTable.NewRow();
            dr[0] = mesiData;
            returnTable.Rows.Add(dr);

            LogUtility.DebugMethodEnd();
            return returnTable;
        }

        /// <summary>
        /// 合計計算
        /// </summary>
        private string GetGoukei(string tukikei)
        {
            decimal goukei = 0;

            // 廃棄物種類-合計
            string[] arrayGoukei = tukikei.Split(',');
            for (int i = 0; i < arrayGoukei.Length; i++)
            {
                if (!string.Empty.Equals(arrayGoukei[i]))
                {
                    goukei += Convert.ToDecimal(arrayGoukei[i]);
                }
            }

            return goukei.ToString();
        }

        /// <summary>
        /// 一次二次区分を取得
        /// </summary>
        private string GetManikbnNm()
        {
            string maniKbnNm = string.Empty;
            // 一時二次区分
            if ("1".Equals(this.mJoukenParam.ichijinijiKbn))
            {
                maniKbnNm = "一次マニフェスト";
            }
            else
            {
                maniKbnNm = "二次マニフェスト";
            }
            return maniKbnNm;
        }

        /// <summary>
        /// 月カラムタイトルを取得
        /// </summary>
        private string GetTukiTitle()
        {
            string tukiTitle = string.Empty;
            // タイトル月カラム
            for (int i = 17; i < this.form.customDataGridView1.Columns.Count; i++)
            {
                if (this.form.customDataGridView1.Columns[i].Visible == true)
                {
                    tukiTitle += this.form.customDataGridView1.Columns[i].HeaderText + ",";
                }
                else
                {
                    tukiTitle += ",";
                }
            }
            tukiTitle += ",";

            return tukiTitle;
        }
        /// <summary>
        /// １２月変換
        /// </summary>
        private string Get12Tukibetu(string tukibetu)
        {
            tukibetu = tukibetu.Substring(0, tukibetu.Length - 1);
            int len = tukibetu.Split(',').Length;

            // １２月未満の場合
            if (len < 12)
            {
                for (int i = 0; i < 12 - len; i++)
                {
                    tukibetu += "," + string.Empty;
                }
            }

            return tukibetu;
        }
        #endregion

        #endregion

        #region [F6]CSV出力処理 

        /// <summary>
        /// GridViewからCSVFile出力
        /// </summary>
        internal bool CsvOutput()
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart();
                CustomDataGridView objDataGridView = this.form.customDataGridView1;
                string csvName =string.Empty ;
                switch (this.form.cstmNmTxtB_ShuturyokuNaiyou.Text)
                {
                    case "1":
                        csvName="排出事業者別";
                        break;
                    case "2":
                        csvName = "運搬受託者別";
                        break;
                    case "3":
                        csvName = "処分受託者別";
                        break;
                    case "4":
                        csvName = "最終処分場所別";
                        break;
                    case "5":
                        csvName = "廃棄物種類別";
                        break;
                }

                //一覧に明細行がない場合
                if (objDataGridView == null || objDataGridView.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない                  
                    this.messageShowLogic.MessageBoxShow("E044");
                    returnVal = false;
                }
                else
                {
                    //CSV出力確認メッセージを表示する                   
                    if (this.messageShowLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //CSVExport.ConvertCustomDataGridViewToCsv(objDataGridView, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_SUII) + "(" + csvName + ")_" + DateTime.Now.ToString("yyyyMMddHHmmss"), this.form);
                        CSVExport.ConvertCustomDataGridViewToCsv(objDataGridView, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_SUII) + "(" + csvName + ")_" + this.getDBDateTime().ToString("yyyyMMddHHmmss"), this.form);
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CsvOutput", ex);
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

        #region [F7]条件クリア
        /// <summary>
        /// [F7]条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //条件クリア
                this.DisplyInit();
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

        #region [F8]検索

        public int Search()
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart();
                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //検索条件取得
                this.GetSearchCondition();
                //グリッドの設定
                if (!this.GridViewInit())
                {
                    ret = -1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                //検索実行前チェック処理
                if (CheckSearchCondition())
                {                   
                    // データ取得
                    DataTable dt = new DataTable();
                    dt = this.MakeGridData();
                    if (dt.Rows.Count <= 0)
                    {
                        //読込データ件数を0にする                 
                        messageShowLogic.MessageBoxShow("C001");
                    }
                    else
                    {
                        ret = dt.Rows.Count;
                        // グリッドのデータソースを指定
                        this.form.customDataGridView1.DataSource = dt;
                        // 画面再描画
                        this.form.customDataGridView1.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.messageShowLogic.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        /// <summary>
        /// 検索条件取得
        /// </summary>
        private void GetSearchCondition()
        {
            var info = new JoukenParam();

            DateTime dateFrom;
            DateTime.TryParse(this.form.cstmDateTimePicker_NengappiFrom.Value.ToString(), out dateFrom);

            DateTime dateTo;
            DateTime.TryParse(this.form.cstmDateTimePicker_NengappiTo.Value.ToString(), out dateTo);



            // 画面の初期化
            info.ichijinijiKbn = this.form.cstmNmTxtB_IchijiNiji.Text;                      // 一時二次区分
            info.nengappiFrom = dateFrom.ToString("yyyy/MM/dd");                            // 年月日開始
            info.nengappiTo = dateTo.ToString("yyyy/MM/dd");                                // 年月日終了
            if ("99".Equals(this.form.cstmANTexB_Kyoten.Text))
            {
                info.kyoten = string.Empty;
            }
            else
            {
                info.kyoten = this.form.cstmANTexB_Kyoten.Text;                                 // 拠点CD
            }
            info.haiJigyouShaFrom = this.form.cstmANTexB_HaishutuJigyoushaFrom.Text;        // 排出事業者CD開始
            info.haiJigyouShaTo = this.form.cstmANTexB_HaishutuJigyoushaTo.Text;            // 排出事業者CD終了
            info.haiJigyouBaFrom = this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text;         // 排出事業場CD開始
            info.haiJigyouBaTo = this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text;             // 排出事業場CD終了
            info.unpanJutakuShaFrom = this.form.cstmANTexB_UnpanJutakushaFrom.Text;         // 搬受託者CD開始
            info.unpanJutakuShaTo = this.form.cstmANTexB_UnpanJutakushaTo.Text;             // 運搬受託者CD終了
            info.shobunJutakuShaFrom = this.form.cstmANTexB_ShobunJutakushaFrom.Text;       // 処分受託者CD開始
            info.shobunJutakuShaTo = this.form.cstmANTexB_ShobunJutakushaTo.Text;           // 処分受託者CD終了
            info.saisyuuShobunBashoFrom = this.form.cstmANTexB_SaishuuShobunJouFrom.Text;   // 最終処分場所CD開始
            info.saisyuuShobunBashoTo = this.form.cstmANTexB_SaishuuShobunJouTo.Text;       // 最終処分場所CD終了
            info.chokkouHaikibutuSyurui = this.form.cstmANTexB_Chokkou.Text;                // 産廃（直行）廃棄物種類CD
            info.tsumikaeHaikibutuSyurui = this.form.cstmANTexB_Tsumikae.Text;              // 産廃（積替）廃棄物種類CD
            info.kenpaiHaikibutuSyurui = this.form.cstmANTexB_Kenpai.Text;                  // 建廃廃棄物種類CD
            info.denshiHaikibutuSyurui = this.form.cstmANTexB_Denshi.Text;                  // 電子廃棄物種類CD
            info.syuturyokuNaiyoiu = this.form.cstmNmTxtB_ShuturyokuNaiyou.Text;            // 出力内容
            info.syuturyokuKubun = this.form.cstmNmTxtB_ShuturyokuKubun.Text;               // 出力区分


            info.dateFrom = dateFrom;                            // 年月日開始
            info.dateTo = dateTo;                                // 年月日終了
            info.monthsBetween = DateAndTime.DateDiff(DateInterval.Month, dateFrom, dateTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
            
            this.mJoukenParam = info;
        }
        /// <summary>
        /// 実行前チェック処理
        /// </summary>
        private bool CheckSearchCondition()
        {
            var messageShowLogic = new MessageBoxShowLogic();
            // TO - FROMが１２か月を超えていたら強制的にToの値を変えて１２か月にする
            if (this.mJoukenParam.monthsBetween >= 12)
            {
                messageShowLogic.MessageBoxShow("E002", "年月日", "12ヶ月");
                return false;
            }
           return true;
        }
        /// <summary>
        /// グリッドの初期設定を行う
        /// </summary>
        private void MakeCustumDataGridView()
        {
            LogUtility.DebugMethodStart();

            DataGridViewTextBoxColumn column;

            // 1.一次マニフェスト区分
            column = new DataGridViewTextBoxColumn();
            column.Name = "MANIKUBUN";
            column.Width = 180;
            column.HeaderText = "一次マニフェスト区分";
            column.DataPropertyName = "MANIKUBUN";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 2.廃棄物区分
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_KUBUN";
            column.Width = 110;
            column.HeaderText = "廃棄物区分";
            column.DataPropertyName = "HAIKIBUTU_KUBUN";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 3.排出事業者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUSHA_CD";
            column.Width = 120;
            column.HeaderText = "排出事業者CD";
            column.DataPropertyName = "HAISHUTU_JIGYOUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 4.排出事業者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "排出事業者名称";
            column.DataPropertyName = "HAISHUTU_JIGYOUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 5.排出事業場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUJOU_CD";
            column.Width = 120;
            column.HeaderText = "排出事業場CD";
            column.DataPropertyName = "HAISHUTU_JIGYOUJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 6.排出事業場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAISHUTU_JIGYOUJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "排出事業場名称";
            column.DataPropertyName = "HAISHUTU_JIGYOUJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 7.処分受託者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JUTAKUSHA_CD";
            column.Width = 120;
            column.HeaderText = "処分受託者CD";
            column.DataPropertyName = "SHOBUN_JUTAKUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 8.処分受託者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JUTAKUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "処分受託者名称";
            column.DataPropertyName = "SHOBUN_JUTAKUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 9.処分事業場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JIGYOUJOU_CD";
            column.Width = 120;
            column.HeaderText = "処分事業場CD";
            column.DataPropertyName = "SHOBUN_JIGYOUJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 10.処分事業場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SHOBUN_JIGYOUJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "処分事業場名称";
            column.DataPropertyName = "SHOBUN_JIGYOUJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 11.運搬受託者CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "UNPAN_JUTAKUSHA_CD";
            column.Width = 120;
            column.HeaderText = "運搬受託者CD";
            column.DataPropertyName = "UNPAN_JUTAKUSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 12.運搬受託者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "UNPAN_JUTAKUSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "運搬受託者名称";
            column.DataPropertyName = "UNPAN_JUTAKUSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            // 最終処分業者ＣＤ
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNGYOSHA_CD";
            column.Width = 120;
            column.HeaderText = "最終処分業者CD";
            column.DataPropertyName = "SAISHUU_SHOBUNGYOSHA_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 最終処分業者名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNGYOSHA_MEISHOU";
            column.Width = 160;
            column.HeaderText = "最終処分業者名称";
            column.DataPropertyName = "SAISHUU_SHOBUNGYOSHA_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
            // 13.最終処分場CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNJOU_CD";
            column.Width = 120;
            column.HeaderText = "最終処分場CD";
            column.DataPropertyName = "SAISHUU_SHOBUNJOU_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 14.最終処分場名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "SAISHUU_SHOBUNJOU_MEISHOU";
            column.Width = 160;
            column.HeaderText = "最終処分場名称";
            column.DataPropertyName = "SAISHUU_SHOBUNJOU_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 15.廃棄物種類CD
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_SHURUI_CD";
            column.Width = 120;
            column.HeaderText = "廃棄物種類CD";
            column.DataPropertyName = "HAIKIBUTU_SHURUI_CD";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 16.廃棄物種類名称
            column = new DataGridViewTextBoxColumn();
            column.Name = "HAIKIBUTU_SHURUI_MEISHOU";
            column.Width = 160;
            column.HeaderText = "廃棄物種類名称";
            column.DataPropertyName = "HAIKIBUTU_SHURUI_MEISHOU";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 17.単位
            column = new DataGridViewTextBoxColumn();
            column.Name = "TANI";
            column.Width = 60;
            column.HeaderText = "単位";
            column.DataPropertyName = "TANI";
            column.ReadOnly = true;
            this.form.customDataGridView1.Columns.Add(column);

            // 18.月計1
            column = new DataGridViewTextBoxColumn();
            column.Width = 90;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 19.月計2
            column = new DataGridViewTextBoxColumn();
            column.Width = 90;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 20.月計3
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.ReadOnly = true;
            column.Visible = false;
            column.Width = 90;
            this.form.customDataGridView1.Columns.Add(column);

            // 21.月計4
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 22.月計5
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 23.月計6
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 24.月計7
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 25.月計8
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 26.月計9
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 27.月計10
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 28.月計11
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 29.月計12
            column = new DataGridViewTextBoxColumn();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.Width = 90;
            column.ReadOnly = true;
            column.Visible = false;
            this.form.customDataGridView1.Columns.Add(column);

            // 新規行追加不可
            this.form.customDataGridView1.AllowUserToAddRows = false;
            this.form.customDataGridView1.AutoGenerateColumns = false;

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 初期表示時のデータグリッドのカラム表示非表示の制御
        /// </summary>
        private void CustumDataGridViewInitDisp()
        {
            LogUtility.DebugMethodStart();

            #region - 月計以外のカラムの表示非表示制御 -
            switch (this.form.cstmNmTxtB_ShuturyokuNaiyou.Text)
            {
                case "1":
                    #region - 出力内容：排出 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "2":
                    #region - 出力内容：運搬 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "3":
                    #region - 出力内容：処分 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "4":
                    #region - 出力内容：最終 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_MEISHOU"].Visible = true;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    #endregion
                    break;

                case "5":
                    #region - 出力内容：廃棄種類 -
                    this.form.customDataGridView1.Columns["MANIKUBUN"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_CD"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_SHURUI_MEISHOU"].Visible = true;
                    this.form.customDataGridView1.Columns["TANI"].Visible = true;
                    this.form.customDataGridView1.Columns["HAIKIBUTU_KUBUN"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["HAISHUTU_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JUTAKUSHA_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SHOBUN_JIGYOUJOU_MEISHOU"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["UNPAN_JUTAKUSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNGYOSHA_MEISHOU"].Visible = false;
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_CD"].Visible = false;
                    this.form.customDataGridView1.Columns["SAISHUU_SHOBUNJOU_MEISHOU"].Visible = false;
                    #endregion
                    break;

                default:
                    break;
            }
            #endregion

            #region - 月計カラムの表示非表示制御 -

            // From-Toの分だけ表示し、
            int indexOffset = 17;

            DateTime dateFrom = DateTime.Parse(this.form.cstmDateTimePicker_NengappiFrom.Text);
            DateTime dateTo = DateTime.Parse(this.form.cstmDateTimePicker_NengappiTo.Text);
            long monthsBetween = DateAndTime.DateDiff(DateInterval.Month, dateFrom, dateTo, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);

            for (int i = 0; i < monthsBetween + 1; i++)
            {
                // 20140621 syunrei EV004869_明細の出し方がおかしい　start
                if (i + indexOffset >= this.form.customDataGridView1.ColumnCount)
                {
                    return;
                }
                // 20140621 syunrei EV004869_明細の出し方がおかしい　end
                this.form.customDataGridView1.Columns[i + indexOffset].Visible = true;
                DateTime dt = dateFrom.AddMonths(i);
                this.form.customDataGridView1.Columns[i + indexOffset].HeaderText = (dt.Month).ToString() + "月";
                this.form.customDataGridView1.Columns[i + indexOffset].Name = "TUKIKEI_" + (dt.Month).ToString();
                this.form.customDataGridView1.Columns[i + indexOffset].DataPropertyName = "TUKIKEI_" + (dt.Month).ToString();
            }
            #endregion

            LogUtility.DebugMethodEnd();
        }
       
        /// <summary>
        /// 画面表示データの元となる情報をDBより取得
        /// </summary>
        /// <returns> 画面表示データの元となる情報 </returns>
        private DataTable MakeGridData()
        {
            LogUtility.DebugMethodStart();

            this.serchCMDto = new DTOClass();
            // 条件を設定する
            this.serchCMDto.FIRST_MANIFEST_KBN = this.mJoukenParam.ichijinijiKbn;                   // 一時二次区分
            this.serchCMDto.DATE_START = this.mJoukenParam.nengappiFrom;                            // 年月日開始
            this.serchCMDto.DATE_END = this.mJoukenParam.nengappiTo;                                // 年月日終了
            this.serchCMDto.KYOTEN_CD = this.mJoukenParam.kyoten;                                   // 拠点CD
            this.serchCMDto.HST_GYOUSHA_CD_START = this.mJoukenParam.haiJigyouShaFrom;              // 排出事業者CD開始
            this.serchCMDto.HST_GYOUSHA_CD_END = this.mJoukenParam.haiJigyouShaTo;                  // 排出事業者CD終了
            this.serchCMDto.HST_GENBA_CD_START = this.mJoukenParam.haiJigyouBaFrom;                 // 排出事業場CD開始
            this.serchCMDto.HST_GENBA_CD_END = this.mJoukenParam.haiJigyouBaTo;                     // 排出事業場CD終了
            this.serchCMDto.HST_UPN_GYOUSHA_CD_START = this.mJoukenParam.unpanJutakuShaFrom;        // 運搬受託者CD開始
            this.serchCMDto.HST_UPN_GYOUSHA_CD_END = this.mJoukenParam.unpanJutakuShaTo;            // 運搬受託者CD終了
            this.serchCMDto.HST_UPN_SAKI_GYOUSHA_CD_START = this.mJoukenParam.shobunJutakuShaFrom;  // 処分受託者CD開始
            this.serchCMDto.HST_UPN_SAKI_GYOUSHA_CD_END = this.mJoukenParam.shobunJutakuShaTo;      // 処分受託者CD終了
            this.serchCMDto.HST_LAST_SBN_GENBA_CD_START = this.mJoukenParam.saisyuuShobunBashoFrom; // 最終処分場所CD開始
            this.serchCMDto.HST_LAST_SBN_GENBA_CD_END = this.mJoukenParam.saisyuuShobunBashoTo;     // 最終処分場所CD終了
            this.serchCMDto.HST_HAIKI_SHURUI_CD1 = this.mJoukenParam.chokkouHaikibutuSyurui;        // 産廃（直行）廃棄物種類CD
            this.serchCMDto.HST_HAIKI_SHURUI_CD2 = this.mJoukenParam.tsumikaeHaikibutuSyurui;       // 産廃（積替）廃棄物種類CD
            this.serchCMDto.HST_HAIKI_SHURUI_CD3 = this.mJoukenParam.kenpaiHaikibutuSyurui;         // 建廃廃棄物種類CD
            this.serchCMDto.HAIKIBUTU_DENSHI = this.mJoukenParam.denshiHaikibutuSyurui;             // 電子廃棄物種類CD
            this.serchCMDto.SHUTURYOKU_NAIYOU = this.mJoukenParam.syuturyokuNaiyoiu;                // 出力内容
            this.serchCMDto.SHUTURYOKU_KUBUN = this.mJoukenParam.syuturyokuKubun;                   // 出力区分

            DataTable returnDt = new DataTable();

            // 取得データ格納用のデータテーブルは出力内容によって切り替える
            switch (this.mJoukenParam.syuturyokuNaiyoiu)
            {
                case ("1"):
                    // 排出
                    returnDt = this.GetHaishutuData();
                    break;

                case ("2"):
                    // 運搬
                    returnDt = this.GetUnpanData();
                    break;

                case ("3"):
                    // 処分
                    returnDt = this.GetShobunData();
                    break;

                case ("4"):
                    // 最終
                    returnDt = this.GetSaishuuData();
                    break;

                case ("5"):
                    // 廃棄
                    returnDt = this.GetHaikiData();
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd();

            return returnDt;
        }
        /// <summary>
        /// 排出データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetHaishutuData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();
            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HST_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("HST_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                // 合算、紙
                if (!this.mJoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = this.mDao.GetKamiHaishutuData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // 合算、電子
                if (!this.mJoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = this.mDao.GetDenHaishutuData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAISHUTU_JIGYOUSHA_CD");
                returnData.Columns.Add("HAISHUTU_JIGYOUSHA_MEISHOU");
                returnData.Columns.Add("HAISHUTU_JIGYOUJOU_CD");
                returnData.Columns.Add("HAISHUTU_JIGYOUJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i <this.mJoukenParam.monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (this.mJoukenParam.dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                string gyoushaName = string.Empty;
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.mJoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    gyoushaCD = temp.Rows[i]["HST_GYOUSHA_CD"].ToString();
                    gyoushaName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    genbaCD = temp.Rows[i]["HST_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDecimal(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["HST_GYOUSHA_CD"].ToString())) ||
                            (!gyoushaName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["HST_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString())) ||
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAISHUTU_JIGYOUSHA_CD"] = gyoushaCD;
                        retRow["HAISHUTU_JIGYOUSHA_MEISHOU"] = gyoushaName;
                        retRow["HAISHUTU_JIGYOUJOU_CD"] = genbaCD;
                        retRow["HAISHUTU_JIGYOUJOU_MEISHOU"] = genbaName;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return returnData;
        }

        /// <summary>
        /// 運搬データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetUnpanData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HAIKI_KBN_CD");
                temp.Columns.Add("UPN_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!this.mJoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = this.mDao.GetKamiUnpanData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!this.mJoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = this.mDao.GetDenUnpanData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAIKIBUTU_KUBUN");
                returnData.Columns.Add("UNPAN_JUTAKUSHA_CD");
                returnData.Columns.Add("UNPAN_JUTAKUSHA_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < this.mJoukenParam.monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (this.mJoukenParam.dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName, typeof(Double));
                }

                #endregion

                #region - 取得したデータを加工 -

                string haikiKbn = string.Empty;
                string UnpanCD = string.Empty;
                string UnpanName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.mJoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                    haikiKbn = temp.Rows[i]["HAIKI_KBN_CD"].ToString();
                    UnpanCD = temp.Rows[i]["UPN_GYOUSHA_CD"].ToString();
                    UnpanName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDecimal(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!haikiKbn.Equals(temp.Rows[i + 1]["HAIKI_KBN_CD"].ToString())) ||
                            (!UnpanCD.Equals(temp.Rows[i + 1]["UPN_GYOUSHA_CD"].ToString())) ||
                            (!UnpanName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["HAIKIBUTU_KUBUN"] = haikiKbn;
                        retRow["UNPAN_JUTAKUSHA_CD"] = UnpanCD;
                        retRow["UNPAN_JUTAKUSHA_MEISHOU"] = UnpanName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return returnData;
        }

        /// <summary>
        /// 処分データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetShobunData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("UPN_SAKI_GYOUSHA_CD");
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                temp.Columns.Add("UPN_SAKI_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!this.mJoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = this.mDao.GetKamiShobunData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!this.mJoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = this.mDao.GetDenShobunData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("SHOBUN_JUTAKUSHA_CD");
                returnData.Columns.Add("SHOBUN_JUTAKUSHA_MEISHOU");
                returnData.Columns.Add("SHOBUN_JIGYOUJOU_CD");
                returnData.Columns.Add("SHOBUN_JIGYOUJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < this.mJoukenParam.monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (this.mJoukenParam.dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                string gyoushaName = string.Empty;
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.mJoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    gyoushaCD = temp.Rows[i]["UPN_SAKI_GYOUSHA_CD"].ToString();
                    gyoushaName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    genbaCD = temp.Rows[i]["UPN_SAKI_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDecimal(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["UPN_SAKI_GYOUSHA_CD"].ToString())) ||
                            (!gyoushaName.Equals(temp.Rows[i + 1]["GYOUSHA_NAME_RYAKU"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["UPN_SAKI_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["SHOBUN_JUTAKUSHA_CD"] = gyoushaCD;
                        retRow["SHOBUN_JUTAKUSHA_MEISHOU"] = gyoushaName;
                        retRow["SHOBUN_JIGYOUJOU_CD"] = genbaCD;
                        retRow["SHOBUN_JIGYOUJOU_MEISHOU"] = genbaName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return returnData;
        }

        /// <summary>
        /// 最終データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetSaishuuData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("LAST_SBN_GYOUSHA_CD");
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                temp.Columns.Add("GYOUSHA_NAME_RYAKU");
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                temp.Columns.Add("LAST_SBN_GENBA_CD");
                temp.Columns.Add("GENBA_NAME_RYAKU");
                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;

                if (!this.mJoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // データの取得
                    getDataTable = new DataTable();
                    getDataTable = this.mDao.GetKamiSaishuuData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!this.mJoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // データの取得
                    getDataTable = this.mDao.GetDenSaishuuData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                returnData.Columns.Add("SAISHUU_SHOBUNGYOSHA_CD");
                returnData.Columns.Add("SAISHUU_SHOBUNGYOSHA_MEISHOU");
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                returnData.Columns.Add("SAISHUU_SHOBUNJOU_CD");
                returnData.Columns.Add("SAISHUU_SHOBUNJOU_MEISHOU");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < this.mJoukenParam.monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (this.mJoukenParam.dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string gyoushaCD = string.Empty;
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                string gyoushaName = string.Empty;
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                string genbaCD = string.Empty;
                string genbaName = string.Empty;
                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.mJoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();
                    gyoushaCD = temp.Rows[i]["LAST_SBN_GYOUSHA_CD"].ToString();
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                    gyoushaName = temp.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                    // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                    genbaCD = temp.Rows[i]["LAST_SBN_GENBA_CD"].ToString();
                    genbaName = temp.Rows[i]["GENBA_NAME_RYAKU"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDecimal(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        (
                            (!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                            (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString())) ||
                            (!gyoushaCD.Equals(temp.Rows[i + 1]["LAST_SBN_GYOUSHA_CD"].ToString())) ||
                            (!gyoushaName.Equals(temp.Rows[i + 1]["SAISHUU_SHOBUNGYOSHA_MEISHOU"].ToString())) ||
                            (!genbaCD.Equals(temp.Rows[i + 1]["LAST_SBN_GENBA_CD"].ToString())) ||
                            (!genbaName.Equals(temp.Rows[i + 1]["GENBA_NAME_RYAKU"].ToString()))
                        )
                       )
                    {
                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                        retRow["SAISHUU_SHOBUNGYOSHA_CD"] = gyoushaCD;
                        retRow["SAISHUU_SHOBUNGYOSHA_MEISHOU"] = gyoushaName;
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                        retRow["SAISHUU_SHOBUNJOU_CD"] = genbaCD;
                        retRow["SAISHUU_SHOBUNJOU_MEISHOU"] = genbaName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return returnData;
        }

        /// <summary>
        /// 廃棄データ取得
        /// </summary>
        /// <returns></returns>
        private DataTable GetHaikiData()
        {
            LogUtility.DebugMethodStart();

            DataTable returnData = new DataTable();

            try
            {
                DataTable temp = new DataTable();

                temp.Columns.Add("HAIKI_SHURUI_CD");
                temp.Columns.Add("HAIKI_SHURUI_NAME");
                temp.Columns.Add("KOUFU_YM");
                temp.Columns.Add("KANSAN_SUU");

                DataTable getDataTable;


                if (!this.mJoukenParam.syuturyokuKubun.Equals("3"))
                {
                    // 廃棄データの取得
                    getDataTable = this.mDao.GetKamiHaikiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                if (!this.mJoukenParam.syuturyokuKubun.Equals("2"))
                {
                    // 廃棄データの取得
                    getDataTable = this.mDao.GetDenHaikiData(this.serchCMDto);
                    // 該当レコードの取得ができたら
                    if (getDataTable.Rows.Count != 0)
                    {
                        temp = this.AddRow(getDataTable, temp);
                    }
                }

                // ◆取得したデータを加工してreturnDataにセットする
                #region - カラムを設定 -
                returnData.Columns.Add("MANIKUBUN");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_CD");
                returnData.Columns.Add("HAIKIBUTU_SHURUI_MEISHOU");
                returnData.Columns.Add("TANI");

                for (int i = 0; i < this.mJoukenParam.monthsBetween + 1; i++)
                {
                    string cName = "TUKIKEI_" + (this.mJoukenParam.dateFrom.AddMonths(i).Month).ToString();
                    returnData.Columns.Add(cName);
                }

                #endregion

                #region - 取得したデータを加工 -

                string haikiCD = string.Empty;
                string haikiName = string.Empty;
                string maniKubun = string.Empty;

                if (this.mJoukenParam.ichijinijiKbn.Equals("1"))
                {
                    maniKubun = "一次マニフェスト";
                }
                else
                {
                    maniKubun = "二次マニフェスト";
                }

                DataRow retRow = returnData.NewRow();
                for (int i = 0; i < temp.Rows.Count; i++)
                {
                    haikiCD = temp.Rows[i]["HAIKI_SHURUI_CD"].ToString();
                    haikiName = temp.Rows[i]["HAIKI_SHURUI_NAME"].ToString();

                    DateTime targetDate;
                    DateTime.TryParse(temp.Rows[i]["KOUFU_YM"].ToString() + "/01", out targetDate);

                    string cName = "TUKIKEI_" + targetDate.Month.ToString();

                    retRow[cName] = this.GetDbValueDecimal(temp.Rows[i]["KANSAN_SUU"]);

                    if ((i >= temp.Rows.Count - 1) ||
                        ((!haikiCD.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_CD"].ToString())) ||
                        (!haikiName.Equals(temp.Rows[i + 1]["HAIKI_SHURUI_NAME"].ToString()))))
                    {

                        retRow["MANIKUBUN"] = maniKubun;
                        retRow["HAIKIBUTU_SHURUI_CD"] = haikiCD;
                        retRow["HAIKIBUTU_SHURUI_MEISHOU"] = haikiName;
                        retRow["TANI"] = this.unit;

                        returnData.Rows.Add(retRow);
                        retRow = returnData.NewRow();

                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();

            return returnData;
        }

        /// <summary> 取得したデータを一時テーブルに追加する </summary>
        /// <param name="getDataTable">getDataTable：取得したデータ</param>
        /// <param name="tempDataTable">tempDataTable：一時テーブル</param>
        /// <returns>一時テーブル</returns>
        private DataTable AddRow(DataTable getDataTable, DataTable tempDataTable)
        {
            LogUtility.DebugMethodStart(tempDataTable, tempDataTable);

            foreach (DataRow dr in getDataTable.Rows)
            {
                tempDataTable.ImportRow(dr);
            }

            LogUtility.DebugMethodEnd(tempDataTable, tempDataTable);

            return tempDataTable;
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
        /// DBデータを取得
        /// </summary>
        private string GetDbValue(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return string.Empty;
            }

            return obj.ToString();
        }
        /// <summary>
        /// DBデータを取得
        /// </summary>
        private decimal GetDbValueDecimal(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return 0.0m;
            }

            return Convert.ToDecimal(obj);
        }
        
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

        #region チェック

        /// <summary>
        /// 業者マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGyousha(string titleName, out bool catchErr)
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" SELECT M_GYOUSHA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_ADDRESS1, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_TEL ");
                sql.Append(" FROM M_GYOUSHA LEFT JOIN M_TODOUFUKEN ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_CD ");
                //sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                //sql.Append(" WHERE M_GYOUSHA.DELETE_FLG = 0 ");
                // 20151022 BUNN #12040 STR
                sql.Append(" WHERE M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                // 20151022 BUNN #12040 END

                // 排出事業者CDFrom
                if ("排出事業者CDFrom".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 排出事業者CDTo
                else if ("排出事業者CDTo".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 運搬受託者CDFrom
                else if ("運搬受託者CDFrom".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_UnpanJutakushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 運搬受託者CDTo
                else if ("運搬受託者CDTo".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_UnpanJutakushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 処分受託者CDFrom
                else if ("処分受託者CDFrom".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_ShobunJutakushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 処分受託者CDTo
                else if ("処分受託者CDTo".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GYOUSHA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_ShobunJutakushaTo.Text.PadLeft(6, '0') + "'");
                }

                searchResult = this.mDao.GetGyoushay(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 排出事業者CDFrom
                    if ("排出事業者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaFrom.Text
                            = this.GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 排出事業者CDTo
                    else if ("排出事業者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 運搬受託者CDFrom
                    else if ("運搬受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaFrom.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 運搬受託者CDTo
                    else if ("運搬受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 処分受託者CDFrom
                    else if ("処分受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaFrom.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    // 処分受託者CDTo
                    else if ("処分受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaTo.Text
                            = GetDbValue(searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"]);
                    }
                    ret = true;
                }
                else
                {
                    // 排出事業者CDFrom
                    if ("排出事業者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaFrom.Text = string.Empty;
                    }
                    // 排出事業者CDTo
                    else if ("排出事業者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_HaishutuJigyoushaTo.Text = string.Empty;
                    }
                    // 運搬受託者CDFrom
                    else if ("運搬受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaFrom.Text = string.Empty;
                    }
                    // 運搬受託者CDTo
                    else if ("運搬受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_UnpanJutakushaTo.Text = string.Empty;
                    }
                    // 処分受託者CDFrom
                    else if ("処分受託者CDFrom".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaFrom.Text = string.Empty;
                    }
                    // 処分受託者CDTo
                    else if ("処分受託者CDTo".Equals(titleName))
                    {
                        // 排出事業者名
                        this.form.cstmTexBox_ShobunJutakushaTo.Text = string.Empty;
                    }
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGyousha", ex);
                if (ex is SQLRuntimeException)
                {
                    this.messageShowLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E245", "");
                }
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 現場マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkGenba(string titleName, out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" SELECT M_GENBA.GYOUSHA_CD, ");
                sql.Append(" M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_CD, ");
                sql.Append(" M_GENBA.GENBA_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_FURIGANA, ");
                sql.Append(" M_GENBA.GENBA_POST, ");
                sql.Append(" M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU, ");
                sql.Append(" M_GENBA.GENBA_ADDRESS1, ");
                sql.Append(" M_GENBA.GENBA_TEL ");
                sql.Append(" FROM M_GENBA LEFT JOIN M_GYOUSHA ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                //sql.Append(" AND M_GYOUSHA.DELETE_FLG = 0 LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD ");
                sql.Append(" LEFT JOIN M_TODOUFUKEN ON M_GENBA.GENBA_TODOUFUKEN_CD ");
                sql.Append("  = M_TODOUFUKEN.TODOUFUKEN_CD ");
                //sql.Append(" AND M_TODOUFUKEN.DELETE_FLG = 0 ");
                //sql.Append(" WHERE M_GENBA.DELETE_FLG = 0 ");
                sql.Append(" WHERE 1 = 1 ");

                // 排出事業場CDFrom
                if ("排出事業場CDFrom".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_HaisyutsuJigyoubaFrom.Text.PadLeft(6, '0') + "'");
                    sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaFrom.Text.PadLeft(6, '0') + "'");
                }
                // 排出事業場CDTo
                else if ("排出事業場CDTo".Equals(titleName))
                {
                    // 20151022 BUNN #12040 STR
                    sql.Append(" AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = CONVERT(bit, 'True') ");
                    sql.Append(" AND M_GYOUSHA.GYOUSHAKBN_MANI = CONVERT(bit, 'True') ");
                    // 20151022 BUNN #12040 END
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_HaisyutsuJigyoubaTo.Text.PadLeft(6, '0') + "'");
                    sql.Append(" AND M_GENBA.GYOUSHA_CD = '"
                        + this.form.cstmANTexB_HaishutuJigyoushaTo.Text.PadLeft(6, '0') + "'");
                }
                // 最終処分事業場CDFrom
                else if ("最終処分事業場CDFrom".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_SaishuuShobunJouFrom.Text.PadLeft(6, '0') + "'");
                }
                // 最終処分事業場CDTo
                else if ("最終処分事業場CDTo".Equals(titleName))
                {
                    sql.Append(" AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = CONVERT(bit, 'True')");
                    sql.Append(" AND M_GENBA.GENBA_CD = '"
                        + this.form.cstmANTexB_SaishuuShobunJouTo.Text.PadLeft(6, '0') + "'");
                }

                searchResult = this.mDao.GetGenba(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 排出事業場CDFrom
                    if ("排出事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 排出事業場CDTo
                    else if ("排出事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 最終処分事業場CDFrom
                    else if ("最終処分事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouFrom.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                    // 最終処分事業場CDTo
                    else if ("最終処分事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouTo.Text
                            = this.GetDbValue(searchResult.Rows[0]["GENBA_NAME_RYAKU"]);
                    }
                }
                else
                {
                    // 排出事業場CDFrom
                    if ("排出事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text = string.Empty;
                    }
                    // 排出事業場CDTo
                    else if ("排出事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text = string.Empty;
                    }
                    // 最終処分事業場CDFrom
                    else if ("最終処分事業場CDFrom".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouFrom.Text = string.Empty;
                    }
                    // 最終処分事業場CDTo
                    else if ("最終処分事業場CDTo".Equals(titleName))
                    {
                        this.form.cstmTexBox_SaishuuShobunJouTo.Text = string.Empty;
                    }
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGenba", ex);
                if (ex is SQLRuntimeException)
                {
                    this.messageShowLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E245", "");
                }
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類マスタのチェック
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        public bool ChkHaikiShurui(string titleName, out bool catchErr)
        {
            bool ret = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                // SQL文
                DataTable searchResult = new DataTable();

                sql.Append(" select HAIKI_SHURUI_CD, ");
                sql.Append(" HAIKI_SHURUI_NAME_RYAKU ");
                sql.Append(" from M_HAIKI_SHURUI ");
                //sql.Append(" WHERE M_HAIKI_SHURUI.DELETE_FLG = 0 ");
                sql.Append(" WHERE 1 = 1 ");

                // 産廃（直行）廃棄物種類CD
                if ("直行".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '1') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Chokkou.Text.PadLeft(4, '0') + "'");
                }
                // 建廃廃棄物種類CD
                else if ("建廃".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '2') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Kenpai.Text.PadLeft(4, '0') + "'");
                }
                // 産廃（積替）廃棄物種類CD
                else if ("積替".Equals(titleName))
                {
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_KBN_CD = CONVERT(smallint, '3') ");
                    sql.Append(" AND M_HAIKI_SHURUI.HAIKI_SHURUI_CD = '"
                        + this.form.cstmANTexB_Tsumikae.Text.PadLeft(4, '0') + "'");
                }

                sql.Append(" group by HAIKI_KBN_CD, HAIKI_SHURUI_CD, HAIKI_SHURUI_NAME_RYAKU ");
                searchResult = this.mDao.GetHaikiShurui(sql.ToString());

                if (searchResult.Rows.Count > 0)
                {
                    // 産廃（直行）廃棄物種類CD
                    if ("直行".Equals(titleName))
                    {
                        this.form.cstmTexBox_Chokkou.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                    // 建廃廃棄物種類CD
                    else if ("建廃".Equals(titleName))
                    {
                        this.form.cstmTexBox_Kenpai.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                    // 産廃（積替）廃棄物種類CD
                    else if ("積替".Equals(titleName))
                    {
                        this.form.cstmTexBox_Tsumikae.Text
                            = this.GetDbValue(searchResult.Rows[0]["HAIKI_SHURUI_NAME_RYAKU"]);
                    }
                }
                else
                {
                    // 産廃（直行）廃棄物種類CD
                    if ("直行".Equals(titleName))
                    {
                        this.form.cstmTexBox_Chokkou.Text = string.Empty;
                    }
                    // 建廃廃棄物種類CD
                    else if ("建廃".Equals(titleName))
                    {
                        this.form.cstmTexBox_Kenpai.Text = string.Empty;
                    }
                    // 産廃（積替）廃棄物種類CD
                    else if ("積替".Equals(titleName))
                    {
                        this.form.cstmTexBox_Tsumikae.Text = string.Empty;
                    }
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikiShurui", ex);
                if (ex is SQLRuntimeException)
                {
                    this.messageShowLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.messageShowLogic.MessageBoxShow("E245", "");
                }
                ret = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
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

        /// 20141128 Houkakou 「マニ推移表」のダブルクリックを追加する　start
        #region cstmDateTimePicker_NengappiToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmDateTimePicker_NengappiTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmDateTimePicker_NengappiFrom;
            var ToTextBox = this.form.cstmDateTimePicker_NengappiTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cstmANTexB_HaishutuJigyoushaToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmANTexB_HaishutuJigyoushaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmANTexB_HaishutuJigyoushaFrom;
            var ToTextBox = this.form.cstmANTexB_HaishutuJigyoushaTo;

            ToTextBox.Text = FromTextBox.Text;
            this.form.cstmTexBox_HaishutuJigyoushaTo.Text = this.form.cstmTexBox_HaishutuJigyoushaFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cstmANTexB_HaisyutsuJigyoubaToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmANTexB_HaisyutsuJigyoubaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmANTexB_HaisyutsuJigyoubaFrom;
            var ToTextBox = this.form.cstmANTexB_HaisyutsuJigyoubaTo;

            ToTextBox.Text = FromTextBox.Text;
            this.form.cstmTexBox_HaisyutsuJigyoubaTo.Text = this.form.cstmTexBox_HaisyutsuJigyoubaFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cstmANTexB_UnpanJutakushaToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmANTexB_UnpanJutakushaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmANTexB_UnpanJutakushaFrom;
            var ToTextBox = this.form.cstmANTexB_UnpanJutakushaTo;

            ToTextBox.Text = FromTextBox.Text;
            this.form.cstmTexBox_UnpanJutakushaTo.Text = this.form.cstmTexBox_UnpanJutakushaFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cstmANTexB_ShobunJutakushaToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmANTexB_ShobunJutakushaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmANTexB_ShobunJutakushaFrom;
            var ToTextBox = this.form.cstmANTexB_ShobunJutakushaTo;

            ToTextBox.Text = FromTextBox.Text;
            this.form.cstmTexBox_ShobunJutakushaTo.Text = this.form.cstmTexBox_ShobunJutakushaFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region cstmANTexB_SaishuuShobunJouToダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cstmANTexB_SaishuuShobunJouTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cstmANTexB_SaishuuShobunJouFrom;
            var ToTextBox = this.form.cstmANTexB_SaishuuShobunJouTo;

            ToTextBox.Text = FromTextBox.Text;
            this.form.cstmTexBox_SaishuuShobunJouTo.Text = this.form.cstmTexBox_SaishuuShobunJouFrom.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「マニ推移表」のダブルクリックを追加する　end

        /// 20141209 teikyou 日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool isErr = false;
            try
            {
                this.form.cstmDateTimePicker_NengappiFrom.BackColor = Constans.NOMAL_COLOR;
                this.form.cstmDateTimePicker_NengappiTo.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.cstmDateTimePicker_NengappiFrom.Text))
                {
                    return isErr;
                }
                if (string.IsNullOrEmpty(this.form.cstmDateTimePicker_NengappiTo.Text))
                {
                    return isErr;
                }

                DateTime date_from = Convert.ToDateTime(this.form.cstmDateTimePicker_NengappiFrom.Value);
                DateTime date_to = Convert.ToDateTime(this.form.cstmDateTimePicker_NengappiTo.Value);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.cstmDateTimePicker_NengappiFrom.IsInputErrorOccured = true;
                    this.form.cstmDateTimePicker_NengappiTo.IsInputErrorOccured = true;
                    this.form.cstmDateTimePicker_NengappiFrom.BackColor = Constans.ERROR_COLOR;
                    this.form.cstmDateTimePicker_NengappiTo.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "年月日From", "年月日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.cstmDateTimePicker_NengappiFrom.Focus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                msgLogic.MessageBoxShow("E245", "");
                isErr = true;
            }
            return isErr;
        }
        #endregion

        #region cstmDateTimePicker_NengappiFrom_Leaveイベント
        /// <summary>
        /// cstmDateTimePicker_NengappiFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void cstmDateTimePicker_NengappiFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.cstmDateTimePicker_NengappiTo.Text))
            {
                this.form.cstmDateTimePicker_NengappiTo.IsInputErrorOccured = false;
                this.form.cstmDateTimePicker_NengappiTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region cstmDateTimePicker_NengappiTo_Leaveイベント
        /// <summary>
        /// cstmDateTimePicker_NengappiTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void cstmDateTimePicker_NengappiTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.cstmDateTimePicker_NengappiFrom.Text))
            {
                this.form.cstmDateTimePicker_NengappiFrom.IsInputErrorOccured = false;
                this.form.cstmDateTimePicker_NengappiFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141209 teikyou 日付チェックを追加する　end

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
