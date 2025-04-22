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
using System.Data;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Reflection;
using System.ComponentModel;
using Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Const;
using Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Accessor;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class KenshuIchiranJokenShiteiPopupLogic
    {
        /// <summary>
        /// DAO
        /// </summary>
        private KenshuIchiranJokenShiteiPopupDAOClass dao;

        /// <summary>
        /// DTO
        /// </summary>
        private KenshuIchiranJokenShiteiPopupDTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private KenshuIchiranJokenShiteiPopupForm form;

        /// <summary>
        /// 検索結果取得用
        /// </summary>
        public DataTable dataResult { get; set; }

        /// <summary>
        /// 現在表示中の一覧
        /// </summary>
        public DataRow[] selectedRowsIchiran;

        ///<summary>
        /// alert number
        /// </summary>
        public int iAlertNumber;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Setting.ButtonSetting.xml";

        /// <summary>検収有無：無</summary>
        private const int KENSHU_MUST_KBN_FALSE = 1;
        /// <summary>検収有無：有</summary>
        private const int KENSHU_MUST_KBN_TRUE = 2;

        /// <summary>検収状況：未検収</summary>
        private const int KENSHU_JYOUKYOU_MIKENSHU = 1;
        /// <summary>検収状況：済検収</summary>
        private const int KENSHU_JYOUKYOU_KENSHUZUMI = 2;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        internal BasePopForm parentForm;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        internal MessageBoxShowLogic errmessage;
        /// <summary>
        /// DBAccessor
        /// </summary>
        private DBAccessor dba;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KenshuIchiranJokenShiteiPopupLogic(KenshuIchiranJokenShiteiPopupForm targetForm, int alertNumber)
        {
            LogUtility.DebugMethodStart(targetForm, alertNumber);

            this.form = targetForm;
            this.dto = new KenshuIchiranJokenShiteiPopupDTOClass();
            this.dao = DaoInitUtility.GetComponent<KenshuIchiranJokenShiteiPopupDAOClass>();
            iAlertNumber = alertNumber;
            this.errmessage = new MessageBoxShowLogic();
            this.dba = new DBAccessor();
            LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 初期化
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit(KenshuIshiranConstans.ConditionInfo joken)
        {
            bool ret = true;
            try
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BasePopForm)this.form.Parent;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                //　ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 画面初期表示設定
                this.InitializeScreen(joken);
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 画面初期表示設定
        /// </summary>
        private void InitializeScreen(KenshuIshiranConstans.ConditionInfo joken)
        {

           // 拠点
            this.form.txt_KyotenCD.Text = joken.KyotenCD;
            if (false == string.IsNullOrEmpty(joken.KyotenCD))
            {
                this.form.txt_KyotenName.Text = dba.GetNameK(joken.KyotenCD);
            }
            this.form.txt_KyotenCD.Focus();
            // 伝票日付開始日付
            if (joken.DStartDay != null && joken.DStartDay != "")
            {
                this.form.dtp_ShukkaFrom.Value = Convert.ToDateTime(joken.DStartDay);
            }
            else
            {
                this.form.dtp_ShukkaFrom.Value = string.Empty;
            }
            // 伝票日付終了日付
            if (joken.DEndDay != null && joken.DEndDay != "")
            {
                this.form.dtp_ShukkaTo.Value = Convert.ToDateTime(joken.DEndDay);
            }
            else
            {
                this.form.dtp_ShukkaTo.Value = string.Empty;
            }
            // 検収伝票日付開始日付
            if (joken.KStartDay != null && joken.KStartDay != "")
            {
                this.form.dtp_KenshuFrom.Value = Convert.ToDateTime(joken.KStartDay);
            }
            else
            {
                this.form.dtp_KenshuFrom.Value = string.Empty;
            }
            // 検収伝票日付終了日付
            if (joken.KEndDay != null && joken.KEndDay != "")
            {
                this.form.dtp_KenshuTo.Value = Convert.ToDateTime(joken.KEndDay);
            }
            else
            {
                this.form.dtp_KenshuTo.Value = string.Empty;
            }
            // 開始取引先CD
            this.form.txt_TorihikisakiCD.Text = joken.StartTorihikisakiCD;
            if (false == string.IsNullOrEmpty(joken.StartTorihikisakiCD))
            {
                this.form.txt_TorihikisakiName.Text = dba.GetNameT(joken.StartTorihikisakiCD);
            }
            // 終了取引先CD
            this.form.txt_TorihikisakiCDTo.Text = joken.EndTorihikisakiCD;
            if (false == string.IsNullOrEmpty(joken.EndTorihikisakiCD))
            {
                this.form.txt_TorihikisakiNameTo.Text = dba.GetNameT(joken.EndTorihikisakiCD);
            }
            // 開始業者CD
            this.form.txt_GyoushaCD.Text = joken.StartGyoushaCD;
            if (false == string.IsNullOrEmpty(joken.StartGyoushaCD))
            {
                this.form.txt_GyoushaName.Text = dba.GetNameGY(joken.StartGyoushaCD);
            }
            // 終了業者CD
            this.form.txt_GyoushaCDTo.Text = joken.EndGyoushaCD;
            if (false == string.IsNullOrEmpty(joken.EndGyoushaCD))
            {
                this.form.txt_GyoushaNameTo.Text = dba.GetNameGY(joken.EndGyoushaCD);
            }
            M_GENBA dataG = new M_GENBA();
            // 開始現場CD
            this.form.txt_GenbaCD.Text = joken.StartGenbaCD;
            if (false == string.IsNullOrEmpty(joken.StartGenbaCD) && false == string.IsNullOrEmpty(joken.StartGyoushaCD))
            {
                dataG.GYOUSHA_CD = joken.StartGyoushaCD;
                dataG.GENBA_CD = joken.StartGenbaCD;
                this.form.txt_GenbaName.Text = dba.GetNameGE(dataG);
            }
            // 終了現場CD
            this.form.txt_GenbaCDTo.Text = joken.EndGenbaCD;
            if (false == string.IsNullOrEmpty(joken.EndGenbaCD) && false == string.IsNullOrEmpty(joken.EndGyoushaCD))
            {
                dataG.GYOUSHA_CD = joken.EndGyoushaCD;
                dataG.GENBA_CD = joken.EndGenbaCD;
                this.form.txt_GenbaNameTo.Text = dba.GetNameGE(dataG);
            }
            // 開始荷積業者CD
            this.form.txt_NizumiGyoshaCD.Text = joken.StartNGyoushaCD;
            if (false == string.IsNullOrEmpty(joken.StartNGyoushaCD))
            {
                this.form.txt_NizumiGyoushaNameRyaku.Text = dba.GetNameGY(joken.StartNGyoushaCD);
            }
            // 終了荷積業者CD
            this.form.txt_NizumiGyoshaCDTo.Text = joken.EndNGyoushaCD;
            if (false == string.IsNullOrEmpty(joken.EndNGyoushaCD))
            {
                this.form.txt_NizumiGyoushaNameRyakuTo.Text = dba.GetNameGY(joken.EndNGyoushaCD);
            }
            // 開始荷積現場CD
            this.form.txt_NizumiGenbaCD.Text = joken.StartNGenbaCD;
            if (false == string.IsNullOrEmpty(joken.StartNGenbaCD) && false == string.IsNullOrEmpty(joken.StartNGyoushaCD))
            {
                dataG.GYOUSHA_CD = joken.StartNGyoushaCD;
                dataG.GENBA_CD = joken.StartNGenbaCD;
                this.form.txt_NizumiGenbaNameRyaku.Text = dba.GetNameGE(dataG);
            }
            // 終了荷積現場CD
            this.form.txt_NizumiGenbaCDTo.Text = joken.EndNGenbaCD;
            if (false == string.IsNullOrEmpty(joken.EndNGenbaCD) && false == string.IsNullOrEmpty(joken.EndNGyoushaCD))
            {
                dataG.GYOUSHA_CD = joken.EndNGyoushaCD;
                dataG.GENBA_CD = joken.EndNGenbaCD;
                this.form.txt_NizumiGenbaNameRyakuTo.Text = dba.GetNameGE(dataG);
            }
            // 開始出荷品目CD
            this.form.txt_HinmeiCD.Text = joken.StartSHinmokuCD;
            if (false == string.IsNullOrEmpty(joken.StartSHinmokuCD))
            {
                this.form.txt_HinmeiNameRyaku.Text = dba.GetNameH(joken.StartSHinmokuCD);
            }
            // 終了出荷品目CD
            this.form.txt_HinmeiCDTo.Text = joken.EndSHinmokuCD;
            if (false == string.IsNullOrEmpty(joken.EndSHinmokuCD))
            {
                this.form.txt_HinmeiNameRyakuTo.Text = dba.GetNameH(joken.EndSHinmokuCD);
            }            
            // 開始検収品目CD
            this.form.txt_KenshuHinmeiCD.Text = joken.StartKHinmokuCD;
            if (false == string.IsNullOrEmpty(joken.StartKHinmokuCD))
            {
                this.form.txt_KenshuHinmeiNameRyaku.Text = dba.GetNameH(joken.StartKHinmokuCD);
            } 
            // 終了検収品目CD
            this.form.txt_KenshuHinmeiCDTo.Text = joken.EndKHinmokuCD;
            if (false == string.IsNullOrEmpty(joken.EndKHinmokuCD))
            {
                this.form.txt_KenshuHinmeiNameRyakuTo.Text = dba.GetNameH(joken.EndKHinmokuCD);
            }
            // 検収状況
            this.form.txt_KenshuJyoukyou.Text = joken.KenshuJoKBN.ToString();

            // 検収有無
            this.form.txt_KenshuMustMbn.Text = joken.KenshuUmKBN.ToString();

            //remove control not use of parent form
            var parentForm = (BusinessBaseForm)this.form.Parent;
            //parentForm.Controls.Remove(parentForm.pn_foot);
            parentForm.Controls.Remove(parentForm.ProcessButtonPanel);
            parentForm.Controls.Remove(parentForm.statusStrip1);
            //parentForm.Controls.Remove(parentForm.ribbonForm);
            //ポップアップでは、最小化と最大化、リサイズはできない。
            parentForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

            // Location control
            parentForm.Size = new System.Drawing.Size(980, 480);
            parentForm.StartPosition = FormStartPosition.CenterScreen;
            // 内部フォームが縮んでしまうため修正
            this.form.Size = new System.Drawing.Size(954, 302);

            parentForm.bt_func1.Visible = false;
            parentForm.bt_func2.Visible = false;
            parentForm.bt_func3.Visible = false;
            parentForm.bt_func4.Visible = false;
            parentForm.bt_func5.Visible = false;
            parentForm.bt_func6.Visible = false;
            parentForm.bt_func7.Visible = false;
            parentForm.bt_func8.Visible = false;
            parentForm.bt_func9.Visible = true;
            parentForm.bt_func10.Visible = false;
            parentForm.bt_func11.Visible = false;
            parentForm.bt_func12.Visible = true;
            parentForm.lb_hint.Visible = false;

            var p = parentForm.bt_func9.Location;
            parentForm.bt_func9.Location = new System.Drawing.Point(759, p.Y - 20);
            parentForm.bt_func12.Location = new System.Drawing.Point(862, p.Y - 20);

        }

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            // 「取引先CD」のイベント
            // マスタ存在チェックはFWに任せる
            this.form.txt_TorihikisakiCDTo.DoubleClick += new EventHandler(txt_TorihikisakiCDTo_DoubleClick);

            // 「業者CD」のイベント生成
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.form.txt_GyoushaCD.Validated += new EventHandler(txt_GyoushaCD_Validated);
            //this.form.txt_GyoushaCDTo.Validated += new EventHandler(txt_GyoushaCDTo_Validated);
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
            this.form.txt_GyoushaCDTo.DoubleClick += new EventHandler(txt_GyoushaCDTo_DoubleClick);

            // 「現場CD」のイベント生成
            this.form.txt_GenbaCD.Validated += new EventHandler(txt_GenbaCD_Validated);
            this.form.txt_GenbaCDTo.Validated += new EventHandler(txt_GenbaCDTo_Validated);
            this.form.txt_GenbaCDTo.DoubleClick += new EventHandler(txt_GenbaCDTo_DoubleClick);

            // 「荷積業者CD」のイベント生成
            this.form.txt_NizumiGyoshaCD.Validated += new EventHandler(txt_NizumiGyoshaCD_Validated);
            this.form.txt_NizumiGyoshaCDTo.Validated += new EventHandler(txt_NizumiGyoshaCDTo_Validated);
            this.form.txt_NizumiGyoshaCDTo.DoubleClick += new EventHandler(txt_NizumiGyoshaCDTo_DoubleClick);

            // 「荷積現場CD」のイベント生成
            this.form.txt_NizumiGenbaCD.Validated += new EventHandler(txt_NizumiGenbaCD_Validated);
            this.form.txt_NizumiGenbaCDTo.Validated += new EventHandler(txt_NizumiGenbaCDTo_Validated);
            this.form.txt_NizumiGenbaCDTo.DoubleClick += new EventHandler(txt_NizumiGenbaCDTo_DoubleClick);

            // 「出荷品名CD」のイベント生成
            // マスタ存在チェックはFWに任せる
            this.form.txt_HinmeiCDTo.DoubleClick += new EventHandler(txt_HinmeiCDTo_DoubleClick);

            // 「検収品名CD」のイベント生成
            // マスタ存在チェックはFWに任せる
            this.form.txt_KenshuHinmeiCDTo.DoubleClick += new EventHandler(txt_KenshuHinmeiCDTo_DoubleClick);

            // 検収有無のイベント
            this.form.txt_KenshuMustMbn.TextChanged += new EventHandler(this.txt_KenshuMustMbn_TextChanged);

            ////フォームを閉じる
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 検索
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(bt_Search_Click);
            //閉じる
            parentForm.bt_func12.Click += new EventHandler(bt_func7_Click);

            // 20141128 teikyou ダブルクリックを追加する　start
            //伝票日付のダブルクリック
            this.form.dtp_ShukkaTo.MouseDoubleClick += new MouseEventHandler(dtp_ShukkaTo_MouseDoubleClick);
            // 検収伝票日付のダブルクリック
            this.form.dtp_KenshuTo.MouseDoubleClick += new MouseEventHandler(dtp_KenshuTo_MouseDoubleClick);
            // 20141128 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 日付チェックを追加する　start
            this.form.dtp_ShukkaFrom.Leave += new System.EventHandler(dtp_ShukkaFrom_Leave);
            this.form.dtp_ShukkaTo.Leave += new System.EventHandler(dtp_ShukkaTo_Leave);

            this.form.dtp_KenshuFrom.Leave += new System.EventHandler(dtp_KenshuFrom_Leave);
            this.form.dtp_KenshuTo.Leave += new System.EventHandler(dtp_KenshuTo_Leave);
            /// 20141203 Houkakou 日付チェックを追加する　end
        }
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 入力項目のイベント

        #region ダブルクリックイベント
        /// <summary>
        ///  copy txt_NizumiGyoshaCDTo to txt_NizumiGyoshaCD when double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_NizumiGyoshaCDTo_DoubleClick(object sender, EventArgs e)
        {
            //copy txt_NizumiGenbaCDTo to txt_NizumiGenbaCD, txt_NizumiGenbaNameTo to txt_NizumiGenbaName
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_NizumiGyoshaCD.Text = this.form.txt_NizumiGyoshaCDTo.Text;
            //this.form.txt_NizumiGyoushaNameRyaku.Text = this.form.txt_NizumiGyoushaNameRyakuTo.Text;
            LogUtility.DebugMethodStart(sender, e);

            var nizumiGyoshaCDTextBox = this.form.txt_NizumiGyoshaCD;
            var nizumiGyoshaCDToTextBox = this.form.txt_NizumiGyoshaCDTo;
            var nizumiGyoushaNameRyakuTextBox = this.form.txt_NizumiGyoushaNameRyaku;
            var nizumiGyoushaNameRyakuToTextBox = this.form.txt_NizumiGyoushaNameRyakuTo;
            nizumiGyoshaCDToTextBox.Text = nizumiGyoshaCDTextBox.Text;
            nizumiGyoushaNameRyakuToTextBox.Text = nizumiGyoushaNameRyakuTextBox.Text;

            LogUtility.DebugMethodEnd();
            // 20141128 teikyou ダブルクリックを追加する　end
        }


        /// <summary>
        /// copy txt_NizumiGenbaCDTo to txt_NizumiGenbaCD when double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_NizumiGenbaCDTo_DoubleClick(object sender, EventArgs e)
        {
            //copy txt_NizumiGenbaCDTo to txt_NizumiGenbaCD, txt_NizumiGenbaNameTo to txt_NizumiGenbaName
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_NizumiGenbaCD.Text = this.form.txt_NizumiGenbaCDTo.Text;
            //this.form.txt_NizumiGenbaNameRyaku.Text = this.form.txt_NizumiGenbaNameRyakuTo.Text;
            LogUtility.DebugMethodStart(sender, e);

            var nizumiGenbaCDTextBox = this.form.txt_NizumiGenbaCD;
            var nizumiGenbaCDToTextBox = this.form.txt_NizumiGenbaCDTo;
            var nizumiGenbaNameRyakuTextBox = this.form.txt_NizumiGenbaNameRyaku;
            var nizumiGenbaNameRyakuToToTextBox = this.form.txt_NizumiGenbaNameRyakuTo;
            nizumiGenbaCDToTextBox.Text = nizumiGenbaCDTextBox.Text;
            nizumiGenbaNameRyakuToToTextBox.Text = nizumiGenbaNameRyakuTextBox.Text;

            LogUtility.DebugMethodEnd();
            // 20141128 teikyou ダブルクリックを追加する　end
        }


        /// <summary>
        /// copy txt_GenbaCDTo to txt_GenbaCD when double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GenbaCDTo_DoubleClick(object sender, EventArgs e)
        {
            //copy TorihikisakiCDTo to TorihikisakiCD, TorihikisakiNameTo to TorihikisakiName
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_GenbaCD.Text = this.form.txt_GenbaCDTo.Text;
            //this.form.txt_GenbaName.Text = this.form.txt_GenbaNameTo.Text;
            LogUtility.DebugMethodStart(sender, e);

            var genbaCDTextBox = this.form.txt_GenbaCD;
            var genbaCDToTextBox = this.form.txt_GenbaCDTo;
            var genbaNameTextBox = this.form.txt_GenbaName;
            var genbaNameToToTextBox = this.form.txt_GenbaNameTo;
            genbaCDToTextBox.Text = genbaCDTextBox.Text;
            genbaNameToToTextBox.Text = genbaNameTextBox.Text;

            LogUtility.DebugMethodEnd();
            // 20141128 teikyou ダブルクリックを追加する　end
        }

        /// <summary>
        /// copy txt_GyoushaCDTo to txt_GyoushaCD when double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_GyoushaCDTo_DoubleClick(object sender, EventArgs e)
        {
            //copy TorihikisakiCDTo to TorihikisakiCD, TorihikisakiNameTo to TorihikisakiName
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_GyoushaCD.Text = this.form.txt_GyoushaCDTo.Text;
            //this.form.txt_GyoushaName.Text = this.form.txt_GyoushaNameTo.Text;
            LogUtility.DebugMethodStart(sender, e);

            var gyoushaCDTextBox = this.form.txt_GyoushaCD;
            var gyoushaCDToTextBox = this.form.txt_GyoushaCDTo;
            var gyoushaNameTextBox = this.form.txt_GyoushaName;
            var gyoushaNameToToTextBox = this.form.txt_GyoushaNameTo;
            gyoushaCDToTextBox.Text = gyoushaCDTextBox.Text;
            gyoushaNameToToTextBox.Text = gyoushaNameTextBox.Text;

            LogUtility.DebugMethodEnd();
            // 20141128 teikyou ダブルクリックを追加する　end
        }

        /// <summary>
        /// copy TorihikisakiCDTo to TorihikisakiCD when double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TorihikisakiCDTo_DoubleClick(object sender, EventArgs e)
        {
            //copy TorihikisakiCDTo to TorihikisakiCD, TorihikisakiNameTo to TorihikisakiName
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_TorihikisakiCD.Text = this.form.txt_TorihikisakiCDTo.Text;
            //this.form.txt_TorihikisakiName.Text = this.form.txt_TorihikisakiNameTo.Text;
            LogUtility.DebugMethodStart(sender, e);

            var torihikisakiCDTextBox = this.form.txt_TorihikisakiCD;
            var torihikisakiCDToTextBox = this.form.txt_TorihikisakiCDTo;
            var torihikisakiNameTextBox = this.form.txt_TorihikisakiName;
            var torihikisakiNameToTextBox = this.form.txt_TorihikisakiNameTo;
            torihikisakiCDToTextBox.Text = torihikisakiCDTextBox.Text;
            torihikisakiNameToTextBox.Text = torihikisakiNameTextBox.Text;

            LogUtility.DebugMethodEnd();
            // 20141128 teikyou ダブルクリックを追加する　end
        }

        /// <summary>
        /// close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            DataTable dt = new DataTable();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // this.form.resultReturnMethod(null, dt);
            this.form.Close();
            parentForm.Close();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// double click KenshuHinmeiCDTo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KenshuHinmeiCDTo_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_KenshuHinmeiCD.Text = this.form.txt_KenshuHinmeiCDTo.Text;
            //this.form.txt_KenshuHinmeiNameRyaku.Text = this.form.txt_KenshuHinmeiNameRyakuTo.Text;
            var kenshuHinmeiCDTextBox = this.form.txt_KenshuHinmeiCD;
            var kenshuHinmeiCDToTextBox = this.form.txt_KenshuHinmeiCDTo;
            var kenshuHinmeiNameRyakuTextBox = this.form.txt_KenshuHinmeiNameRyaku;
            var kenshuHinmeiNameRyakuToTextBox = this.form.txt_KenshuHinmeiNameRyakuTo;
            kenshuHinmeiCDToTextBox.Text = kenshuHinmeiCDTextBox.Text;
            kenshuHinmeiNameRyakuToTextBox.Text = kenshuHinmeiNameRyakuTextBox.Text;
            // 20141128 teikyou ダブルクリックを追加する　end
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// double click HinmeiCDTo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_HinmeiCDTo_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            // 20141128 teikyou ダブルクリックを追加する　start
            //this.form.txt_HinmeiCD.Text = this.form.txt_HinmeiCDTo.Text;
            //this.form.txt_HinmeiNameRyaku.Text = this.form.txt_HinmeiNameRyakuTo.Text;
            var hinmeiCDTextBox = this.form.txt_HinmeiCD;
            var hinmeiCDToTextBox = this.form.txt_HinmeiCDTo;
            var hinmeiNameRyakuTextBox = this.form.txt_HinmeiNameRyaku;
            var hinmeiNameRyakuToTextBox = this.form.txt_HinmeiNameRyakuTo;
            hinmeiCDToTextBox.Text = hinmeiCDTextBox.Text;
            hinmeiNameRyakuToTextBox.Text = hinmeiNameRyakuTextBox.Text;
            // 20141128 teikyou ダブルクリックを追加する　end
            LogUtility.DebugMethodEnd();
        }
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141128 teikyou ダブルクリックを追加する　start
        private void dtp_ShukkaTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var shukkaFromTextBox = this.form.dtp_ShukkaFrom;
            var shukkaToTextBox = this.form.dtp_ShukkaTo;
            shukkaToTextBox.Text = shukkaFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        private void dtp_KenshuTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var kenshuFromTextBox = this.form.dtp_KenshuFrom;
            var kenshuToTextBox = this.form.dtp_KenshuTo;
            kenshuToTextBox.Text = kenshuFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141128 teikyou ダブルクリックを追加する　end
        #endregion

        #endregion

        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 業者CD
        ///// <summary>
        ///// 業者CD(From)検証後イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txt_GyoushaCD_Validated(object sender, EventArgs e)
        //{
        //    // マスタ存在チェックはFWの制御に任せる

        //    // 現場CD制御
        //    this.ControlGenbaControlEnabled();
        //}

        ///// <summary>
        ///// 業者CD(To)検証後イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txt_GyoushaCDTo_Validated(object sender, EventArgs e)
        //{
        //    // マスタ存在チェックはFWの制御に任せる

        //    // 現場CD制御
        //    this.ControlGenbaControlEnabled();
        //}

        ///// <summary>
        ///// 現場コントロールの活性制御
        ///// </summary>
        //private void ControlGenbaControlEnabled()
        //{
        //    var gyoushaCdFrom = this.form.txt_GyoushaCD.Text;
        //    var gyoushaCdTo = this.form.txt_GyoushaCDTo.Text;

        //    // 現場CDテキストボックスの活性状態を初期化
        //    this.form.txt_GenbaCD.Enabled = true;
        //    this.form.txt_GenbaName.Enabled = true;
        //    this.form.customPopupOpenButton4.Enabled = true;
        //    this.form.txt_GenbaCDTo.Enabled = true;
        //    this.form.txt_GenbaNameTo.Enabled = true;
        //    this.form.customPopupOpenButton11.Enabled = true;

        //    if (!String.IsNullOrEmpty(gyoushaCdFrom) && !String.IsNullOrEmpty(gyoushaCdTo))
        //    {
        //        // どちらも入力されていて業者CDが異なる場合は、現場CDは入力不可
        //        if (gyoushaCdFrom != gyoushaCdTo)
        //        {
        //            this.form.txt_GenbaCD.Enabled = false;
        //            this.form.txt_GenbaName.Enabled = false;
        //            this.form.customPopupOpenButton4.Enabled = false;
        //            this.form.txt_GenbaCDTo.Enabled = false;
        //            this.form.txt_GenbaNameTo.Enabled = false;
        //            this.form.customPopupOpenButton11.Enabled = false;

        //            this.form.txt_GenbaCD.Text = String.Empty;
        //            this.form.txt_GenbaName.Text = String.Empty;
        //            this.form.txt_GenbaCDTo.Text = String.Empty;
        //            this.form.txt_GenbaNameTo.Text = String.Empty;
        //        }
        //    }
        //}
        #endregion
        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        #region 現場CD
        /// <summary>
        /// 現場CD(From)検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e</param>
        private void txt_GenbaCD_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_GenbaCD.Text))
            {
                this.form.txt_GenbaName.Text = string.Empty;
                return;
            }

            // 業者CDチェック
            if (!this.CheckGyoushaCd())
            {
                this.form.txt_GenbaName.Text = string.Empty;
                this.form.txt_GenbaCD.Focus();
                return;
            }

            // マスタ存在チェックのタイミングを制御するためFWではマスタ存在チェックしない
            var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA targetGenba = new M_GENBA();
            targetGenba.GYOUSHA_CD = this.form.txt_GyoushaCD.Text;
            targetGenba.GENBA_CD = this.form.txt_GenbaCD.Text;
            targetGenba.ISNOT_NEED_DELETE_FLG = true;
            // FWのマスタ存在チェックと併せるためGetAllValidDataを使用
            var genba = genbaDao.GetAllValidData(targetGenba);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (genba == null || genba.Length == 0)
            {
                this.form.txt_GenbaName.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.txt_GenbaCD.Focus();
                return;
            }
            else
            {
                // この時点では一意に識別できているはず
                if (genba.Length == 1)
                {
                    this.form.txt_GenbaName.Text = genba[0].GENBA_NAME_RYAKU;
                }
            }

        }

        /// <summary>
        /// 現場CD(To)検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e</param>
        private void txt_GenbaCDTo_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_GenbaCDTo.Text))
            {
                this.form.txt_GenbaNameTo.Text = string.Empty;
                return;
            }

            // 業者CDチェック
            if (!this.CheckGyoushaCd())
            {
                this.form.txt_GenbaCDTo.Focus();
            }

            // マスタ存在チェックのタイミングを制御するためFWではマスタ存在チェックしない
            var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA targetGenba = new M_GENBA();
            targetGenba.GYOUSHA_CD = this.form.txt_GyoushaCDTo.Text;
            targetGenba.GENBA_CD = this.form.txt_GenbaCDTo.Text;
            targetGenba.ISNOT_NEED_DELETE_FLG = true;
            // FWのマスタ存在チェックと併せるためGetAllValidDataを使用
            var genba = genbaDao.GetAllValidData(targetGenba);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (genba == null || genba.Length == 0)
            {
                this.form.txt_GenbaNameTo.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.txt_GenbaCDTo.Focus();
                return;
            }
            else
            {
                // この時点では一意に識別できているはず
                if (genba.Length == 1)
                {
                    this.form.txt_GenbaNameTo.Text = genba[0].GENBA_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// 業者CD入力チェック
        /// </summary>
        /// <returns>true:問題なし、false:問題あり</returns>
        private bool CheckGyoushaCd()
        {
            bool ret = true;

            var gyoushaCdFrom = this.form.txt_GyoushaCD.Text;
            var gyoushaCdTo = this.form.txt_GyoushaCDTo.Text;

            if (String.IsNullOrEmpty(gyoushaCdFrom) && String.IsNullOrEmpty(gyoushaCdTo))
            {
                ret = false;

                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E012", "業者CD");
            }

            return ret;
        }
        #endregion

        #region 荷積業者CD
        /// <summary>
        /// 荷積業者CD(From)検証後イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void txt_NizumiGyoshaCD_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_NizumiGyoshaCD.Text))
            {
                this.form.txt_NizumiGyoshaCD.Text = string.Empty;
                this.form.txt_NizumiGyoushaNameRyaku.Text = string.Empty;
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.ControlNizumiGenbaControlEnabled();
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var targetGyousha = new M_GYOUSHA();
            targetGyousha.GYOUSHA_CD = this.form.txt_NizumiGyoshaCD.Text;
            targetGyousha.ISNOT_NEED_DELETE_FLG = true;
            // FWのマスタ存在チェックと併せるためGetAllValidDataを使用
            var nizumiGyousha = gyoushaDao.GetAllValidData(targetGyousha);

            // マスタ存在チェックはFWに任せずに、独自チェック
            if (nizumiGyousha == null || nizumiGyousha.Length == 0)
            {
                //エラーメッセージ
                this.form.txt_NizumiGyoushaNameRyaku.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "業者");
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.ControlNizumiGenbaControlEnabled();
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                this.form.txt_NizumiGyoshaCD.Focus();
                return;
            }

            if (nizumiGyousha.Length == 1)
            {
                // この時点では一意に取得できるはず
                M_GYOUSHA gyousha = nizumiGyousha[0];

                // 排出事業者区分、運搬受託者区分、荷積卸業者区分チェック
                // 20151026 BUNN #12040 STR
                if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.form.txt_NizumiGyoushaNameRyaku.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.form.txt_NizumiGyoushaNameRyaku.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.txt_NizumiGyoshaCD.Focus();
                }
            }
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.ControlNizumiGenbaControlEnabled();
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 荷積業者CD(To)検証後イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void txt_NizumiGyoshaCDTo_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_NizumiGyoshaCDTo.Text))
            {
                this.form.txt_NizumiGyoshaCDTo.Text = string.Empty;
                this.form.txt_NizumiGyoushaNameRyakuTo.Text = string.Empty;
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.ControlNizumiGenbaControlEnabled();
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                return;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            var targetGyousha = new M_GYOUSHA();
            targetGyousha.GYOUSHA_CD = this.form.txt_NizumiGyoshaCDTo.Text;
            targetGyousha.ISNOT_NEED_DELETE_FLG = true;
            // FWのマスタ存在チェックと併せるためGetAllValidDataを使用
            var nizumiGyousha = gyoushaDao.GetAllValidData(targetGyousha);

            // マスタ存在チェックはFWに任せずに、独自チェック
            if (nizumiGyousha == null || nizumiGyousha.Length == 0)
            {
                //エラーメッセージ
                this.form.txt_NizumiGyoushaNameRyakuTo.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "業者");
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.ControlNizumiGenbaControlEnabled();
                // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                this.form.txt_NizumiGyoshaCDTo.Focus();
                return;
            }

            if (nizumiGyousha.Length == 1)
            {
                // この時点では一意に識別できるはず
                M_GYOUSHA gyousha = nizumiGyousha[0];

                // 排出事業者区分、運搬受託者区分、荷積卸業者区分チェック
                // 20151026 BUNN #12040 STR
                if (gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.form.txt_NizumiGyoushaNameRyakuTo.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    this.form.txt_NizumiGyoushaNameRyakuTo.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.txt_NizumiGyoshaCDTo.Focus();
                }
            }
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            //this.ControlNizumiGenbaControlEnabled();
            // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 現場コントロールの活性制御
        /// </summary>
        //private void ControlNizumiGenbaControlEnabled()
        //{
        //    var nizumiGyoushaCdFrom = this.form.txt_NizumiGyoshaCD.Text;
        //    var nizumiGyoushaCdTo = this.form.txt_NizumiGyoshaCDTo.Text;

        //    // 現場CDテキストボックスの活性状態を初期化
        //    this.form.txt_NizumiGenbaCD.Enabled = true;
        //    this.form.txt_NizumiGenbaNameRyaku.Enabled = true;
        //    this.form.customPopupOpenButton6.Enabled = true;
        //    this.form.txt_NizumiGenbaCDTo.Enabled = true;
        //    this.form.txt_NizumiGenbaNameRyakuTo.Enabled = true;
        //    this.form.customPopupOpenButton13.Enabled = true;

        //    if (!String.IsNullOrEmpty(nizumiGyoushaCdFrom) && !String.IsNullOrEmpty(nizumiGyoushaCdTo))
        //    {
        //        // どちらも入力されていて業者CDが異なる場合は、現場CDは入力不可
        //        if (nizumiGyoushaCdFrom != nizumiGyoushaCdTo)
        //        {
        //            this.form.txt_NizumiGenbaCD.Enabled = false;
        //            this.form.txt_NizumiGenbaNameRyaku.Enabled = false;
        //            this.form.customPopupOpenButton6.Enabled = false;
        //            this.form.txt_NizumiGenbaCDTo.Enabled = false;
        //            this.form.txt_NizumiGenbaNameRyakuTo.Enabled = false;
        //            this.form.customPopupOpenButton13.Enabled = false;

        //            this.form.txt_NizumiGenbaCD.Text = String.Empty;
        //            this.form.txt_NizumiGenbaNameRyaku.Text = String.Empty;
        //            this.form.txt_NizumiGenbaCDTo.Text = String.Empty;
        //            this.form.txt_NizumiGenbaNameRyakuTo.Text = String.Empty;
        //        }
        //    }
        //}
        // 20150923 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        #endregion

        #region 荷積現場CD
        /// <summary>
        /// 荷積現場(From)検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_NizumiGenbaCD_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_NizumiGenbaCD.Text))
            {
                this.form.txt_NizumiGenbaNameRyaku.Text = string.Empty;
                return;
            }

            // 業者CDチェック
            if (!this.CheckNizumiGyoushaCd())
            {
                this.form.txt_NizumiGenbaCD.Text = string.Empty;
                this.form.txt_NizumiGenbaCD.Focus();
                return;
            }

            // マスタ存在チェックのタイミングを制御するためFWではマスタ存在チェックしない
            var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA targetGenba = new M_GENBA();
            targetGenba.GYOUSHA_CD = this.form.txt_NizumiGyoshaCD.Text;
            targetGenba.GENBA_CD = this.form.txt_NizumiGenbaCD.Text;
            targetGenba.ISNOT_NEED_DELETE_FLG = true;
            var nizumiGenba = genbaDao.GetAllValidData(targetGenba);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (nizumiGenba == null || nizumiGenba.Length == 0)
            {
                this.form.txt_NizumiGenbaNameRyaku.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.txt_NizumiGenbaCD.Focus();
                return;
            }

            if (nizumiGenba.Length == 1)
            {
                M_GENBA genba = nizumiGenba[0];
                //排出事業場区分、積替え保管区分、荷積積現場区分チェック
                // 20151026 BUNN #12040 STR
                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.form.txt_NizumiGenbaNameRyaku.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    //一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.txt_NizumiGenbaCD.Focus();
                }
            }
        }

        /// <summary>
        /// 荷積現場(From)検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_NizumiGenbaCDTo_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.form.txt_NizumiGenbaCDTo.Text))
            {
                this.form.txt_NizumiGenbaNameRyakuTo.Text = string.Empty;
                return;
            }

            // 業者CDチェック
            if (!this.CheckNizumiGyoushaCd())
            {
                this.form.txt_NizumiGenbaCDTo.Text = string.Empty;
                this.form.txt_NizumiGenbaCDTo.Focus();
                return;
            }

            // マスタ存在チェックのタイミングを制御するためFWではマスタ存在チェックしない
            var genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            M_GENBA targetGenba = new M_GENBA();
            targetGenba.GYOUSHA_CD = this.form.txt_NizumiGyoshaCDTo.Text;
            targetGenba.GENBA_CD = this.form.txt_NizumiGenbaCDTo.Text;
            targetGenba.ISNOT_NEED_DELETE_FLG = true;
            var nizumiGenba = genbaDao.GetAllValidData(targetGenba);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (nizumiGenba == null || nizumiGenba.Length == 0)
            {
                this.form.txt_NizumiGenbaNameRyakuTo.Text = string.Empty;
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.txt_NizumiGenbaCDTo.Focus();
                return;
            }

            if (nizumiGenba.Length == 1)
            {
                M_GENBA genba = nizumiGenba[0];
                //排出事業場区分、積替え保管区分、荷積積現場区分チェック
                // 20151026 BUNN #12040 STR
                if (genba.HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    this.form.txt_NizumiGenbaNameRyakuTo.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    //一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.txt_NizumiGenbaCDTo.Focus();
                }
            }
        }

        /// <summary>
        /// 荷積業者CDチェック
        /// </summary>
        /// <returns>true:問題なし、false：問題あり</returns>
        private bool CheckNizumiGyoushaCd()
        {
            bool ret = true;

            var nizumiGyoushaCdFrom = this.form.txt_NizumiGyoshaCD.Text;
            var nizumiGyoushaCdTo = this.form.txt_NizumiGyoshaCDTo.Text;

            if (String.IsNullOrEmpty(nizumiGyoushaCdFrom) && String.IsNullOrEmpty(nizumiGyoushaCdTo))
            {
                ret = false;

                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E012", "荷積業者CD");
            }

            return ret;
        }
        #endregion

        #region 検収有無
        /// <summary>
        /// 検収有無が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txt_KenshuMustMbn_TextChanged(object sender, EventArgs e)
        {
            // つくばより検収有無が必要と要求あれば以下のコメントを復活させる
            //if (KENSHU_MUST_KBN_TRUE.ToString().Equals(this.form.txt_KenshuMustMbn.Text))
            //{
            //    this.ChangeKenshuJyoukyouVisible(true);
            //}
            //else
            //{
            //    this.ChangeKenshuJyoukyouVisible(false);
            //}
        }

        /// <summary>
        /// 検収状況の表示状態変更処理
        /// </summary>
        /// <param name="isVisible">True:表示、false:非表示</param>
        private void ChangeKenshuJyoukyouVisible(bool isVisible)
        {
            this.form.label19.Visible = isVisible;
            this.form.txt_KenshuJyoukyou.Visible = isVisible;
            this.form.panel2.Visible = isVisible;
        }
        #endregion

        #endregion

        #region コントロールのイベント
        /// <summary>
        /// 検索条件取得処理
        /// </summary>
        public string getSelectWhere()
        {
            string selectWhere = " ";

            selectWhere = " 1=1 ";
            // 拠点あり
            if (!(string.IsNullOrEmpty(this.form.txt_KyotenCD.Text)))
            {
                selectWhere = selectWhere + " AND KYOTEN_CD = " + this.form.txt_KyotenCD.Text;
            }

            // 取引先Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_TorihikisakiCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  TORIHIKISAKI_CD >= " + this.form.txt_TorihikisakiCD.Text;
            }

            // 取引先Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_TorihikisakiCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND TORIHIKISAKI_CD <= " + this.form.txt_TorihikisakiCDTo.Text;
            }

            // 業者Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_GyoushaCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND GYOUSHA_CD >= " + this.form.txt_GyoushaCD.Text;
            }

            // 業者Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_GyoushaCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND GYOUSHA_CD <= " + this.form.txt_GyoushaCDTo.Text;
            }

            // 現場Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_GenbaCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  GENBA_CD >= " + this.form.txt_GenbaCD.Text;
            }

            // 現場Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_GenbaCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  GENBA_CD <=" + this.form.txt_GenbaCDTo.Text;
            }

            // 荷積業者Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_NizumiGyoshaCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  NIZUMI_GYOUSHA_CD >= " + this.form.txt_NizumiGyoshaCD.Text;
            }

            // 荷積業者Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_NizumiGyoshaCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  NIZUMI_GYOUSHA_CD <= " + this.form.txt_NizumiGyoshaCDTo.Text;
            }

            //荷積現場Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_NizumiGenbaCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  NIZUMI_GENBA_CD >= " + this.form.txt_NizumiGenbaCD.Text;
            }

            //荷積現場Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_NizumiGenbaCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  NIZUMI_GENBA_CD <= " + this.form.txt_NizumiGenbaCDTo.Text;
            }

            //出荷品名Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_HinmeiCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  SHUKKA_DETAIL_HINMEI_CD >= " + this.form.txt_HinmeiCD.Text;
            }

            //出荷品名Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_HinmeiCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  SHUKKA_DETAIL_HINMEI_CD <= " + this.form.txt_HinmeiCDTo.Text;
            }

            //検収品名Fromあり
            if (!(string.IsNullOrEmpty(this.form.txt_KenshuHinmeiCD.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  KENSYU_DETAIL_HINMEI_CD >= " + this.form.txt_KenshuHinmeiCD.Text;
            }

            //検収品名Toあり
            if (!(string.IsNullOrEmpty(this.form.txt_KenshuHinmeiCDTo.Text.Trim())))
            {
                selectWhere = selectWhere + " AND  KENSYU_DETAIL_HINMEI_CD <= " + this.form.txt_KenshuHinmeiCDTo.Text;
            }
            //検収必要
            if (this.form.rdo_KenshuMustKbn_True.Checked)
            {
                selectWhere = selectWhere + " AND KENSHU_YOUHI = 1";
            }

            if (this.form.rdo_KenshuMustKbn_False.Checked)
            {
                selectWhere = selectWhere + " AND KENSHU_YOUHI = 0";
            }

            return selectWhere;
        }

        /// <summary>
        /// 「検索ボタン」イベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        public void bt_Search_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (!this.form.RegistErrorFlag)
                {
                    //ThangNguyen [Add] #11926 Start
                    if (this.CheckInputData())
                    {
                        return;
                    }
                    //ThangNguyen [Add] #11926 End

                    /// 20141203 Houkakou 日付チェックを追加する　start
                    if (this.ShukkaDateCheck())
                    {
                        return;
                    }
                    else if (this.KenshuDateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 日付チェックを追加する　end

                    if (!this.SerchSave())
                    {
                        return;
                    }

                    // 検索条件取得処理
                    KensyuuIchiranDTOCls entity = this.ReturnSearchCondition();
                    this.dataResult = this.dao.GetReportData(entity);
                    // return SearchCondition, KenshuIchiranConditionEntity
                    this.form.resultReturnMethod(ReturnSearchCondition(), dataResult, this.form.Joken);

                    if (dataResult != null && dataResult.Rows.Count <= 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E044", "出力する該当データがありません。");
                        //this.form.resultReturnMethod(ReturnSearchCondition(), dataResult, this.form.Joken);
                        return;
                    }

                    ///record > alertNumber
                    if (iAlertNumber != 0 && dataResult.Rows.Count >= iAlertNumber)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        var result = msgLogic.MessageBoxShow("C035");
                        if (result == DialogResult.No)
                        {
                            //this.form.resultReturnMethod(ReturnSearchCondition(), dataResult, this.form.Joken);
                            return;
                        }
                    }

                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    this.form.Close();
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
            }

            LogUtility.DebugMethodEnd();
        }

        //ThangNguyen [Add] #11926 Start
        private bool CheckInputData()
        {
            bool checkValue = false;
            string errorMessage = "";
            List<string> _messageList = new List<string>();
            this.form.RegistErrorFlag = false;

            //拠点
            if (this.form.txt_KyotenCD.Text == "")
            {
                var itemName = this.form.txt_KyotenCD.DisplayItemName;
                var messageUtil = new MessageUtility();
                var returnStr = messageUtil.GetMessage("E001").MESSAGE;
                this.form.txt_KyotenCD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                _messageList.Add(String.Format(returnStr, itemName));
            }

            //取引先
            errorMessage = this.GetErrorMessage(this.form.txt_TorihikisakiCD, this.form.txt_TorihikisakiCDTo);
            if (errorMessage != "") 
            {
                _messageList.Add(errorMessage);
            }

            //現場
            errorMessage = this.GetErrorMessage(this.form.txt_GyoushaCD, this.form.txt_GyoushaCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            //業者
            errorMessage = this.GetErrorMessage(this.form.txt_GenbaCD, this.form.txt_GenbaCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            //荷積業者
            errorMessage = this.GetErrorMessage(this.form.txt_NizumiGyoshaCD, this.form.txt_NizumiGyoshaCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            //荷積現場
            errorMessage = this.GetErrorMessage(this.form.txt_NizumiGenbaCD, this.form.txt_NizumiGenbaCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            //出荷時品名
            errorMessage = this.GetErrorMessage(this.form.txt_HinmeiCD, this.form.txt_HinmeiCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            //検収品名
            errorMessage = this.GetErrorMessage(this.form.txt_KenshuHinmeiCD, this.form.txt_KenshuHinmeiCDTo);
            if (errorMessage != "")
            {
                _messageList.Add(errorMessage);
            }

            if (_messageList.Count > 0)
            {
                var result = new StringBuilder(256);
                for (int i = 0; i < _messageList.Count; i++)
                {
                    result.AppendLine(_messageList[i]);
                }
                MessageBox.Show(result.ToString(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkValue = true;
            }
            return checkValue;
        }

        private string GetErrorMessage(r_framework.CustomControl.CustomAlphaNumTextBox controlFrom, r_framework.CustomControl.CustomAlphaNumTextBox controlTo)
        {
            string returnMessage = "";
            string valueFrom = controlFrom.Text;
            string valueTo = controlTo.Text;

            controlFrom.BackColor = r_framework.Const.Constans.NOMAL_COLOR;
            controlTo.BackColor = r_framework.Const.Constans.NOMAL_COLOR;

            if (valueFrom != "" && valueTo != "")
            {
                if (0 < string.Compare(valueFrom, valueTo))
                {
                    var messageUtil = new MessageUtility();
                    returnMessage = messageUtil.GetMessage("E032").MESSAGE;
                    returnMessage = String.Format(returnMessage, controlFrom.DisplayItemName, controlTo.DisplayItemName);

                    controlFrom.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    controlTo.BackColor = r_framework.Const.Constans.ERROR_COLOR;

                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        returnMessage = returnMessage.Replace("\\n", System.Environment.NewLine);
                    }
                }
            }
            return returnMessage;
        }

        //ThangNguyen [Add] #11926 End

        /// <summary>
        /// return condition search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e</param>
        public KensyuuIchiranDTOCls ReturnSearchCondition()
        {
            KensyuuIchiranDTOCls kenSyuuIchiranDTO = new KensyuuIchiranDTOCls();
            kenSyuuIchiranDTO.Shukka_Entry_KYOTEN_CD = this.form.txt_KyotenCD.Text == "" ? 0 : Convert.ToInt32(this.form.txt_KyotenCD.Text);
            kenSyuuIchiranDTO.Shukka_Entry_KYOTEN_NAME = this.form.txt_KyotenName.Text;
            if (this.form.dtp_ShukkaFrom.Value != null)
            {
                kenSyuuIchiranDTO.Shukka_Entry_Denpyou_Date_Begin = Convert.ToDateTime(this.form.dtp_ShukkaFrom.Value).Date;
            }
            if (this.form.dtp_ShukkaTo.Value != null)
            {
                kenSyuuIchiranDTO.Shukka_Entry_Denpyou_Date_End = Convert.ToDateTime(this.form.dtp_ShukkaTo.Value).Date;
            }

            if (this.form.dtp_KenshuFrom.Value != null)
            {
                kenSyuuIchiranDTO.Shukka_Entry_Kenshu_Date_Begin = Convert.ToDateTime(this.form.dtp_KenshuFrom.Value);
            }
            if (this.form.dtp_KenshuTo.Value != null)
            {
                kenSyuuIchiranDTO.Shukka_Entry_Kenshu_Date_End = Convert.ToDateTime(this.form.dtp_KenshuTo.Value);
            }
            kenSyuuIchiranDTO.Shukka_Entry_Torihikisaki_Cd_1 = this.form.txt_TorihikisakiCD.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Torihikisaki_Name_1 = this.form.txt_TorihikisakiName.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Torihikisaki_Cd_2 = this.form.txt_TorihikisakiCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Torihikisaki_Name_2 = this.form.txt_TorihikisakiNameTo.Text;

            kenSyuuIchiranDTO.Shukka_Entry_Gyousha_Cd_1 = this.form.txt_GyoushaCD.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Gyousha_Name_1 = this.form.txt_GyoushaName.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Gyousha_Cd_2 = this.form.txt_GyoushaCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Gyousha_Name_2 = this.form.txt_GyoushaNameTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Genba_Cd_1 = this.form.txt_GenbaCD.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Genba_Name_1 = this.form.txt_GenbaName.Text; ;
            kenSyuuIchiranDTO.Shukka_Entry_Genba_Cd_2 = this.form.txt_GenbaCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Genba_Name_2 = this.form.txt_GenbaNameTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Gyousha_Cd_1 = this.form.txt_NizumiGyoshaCD.Text; ;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Gyousha_Name_1 = this.form.txt_NizumiGyoushaNameRyaku.Text;

            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Gyousha_Cd_2 = this.form.txt_NizumiGyoshaCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Gyousha_Name_2 = this.form.txt_NizumiGyoushaNameRyakuTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Genba_Cd_1 = this.form.txt_NizumiGenbaCD.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Genba_Name_1 = this.form.txt_NizumiGenbaNameRyaku.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Genba_Cd_2 = this.form.txt_NizumiGenbaCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_Nizumi_Genba_Name_2 = this.form.txt_NizumiGenbaNameRyakuTo.Text;
            kenSyuuIchiranDTO.Shukka_Detail_Hinmei_Cd_1 = this.form.txt_HinmeiCD.Text;
            kenSyuuIchiranDTO.Shukka_Detail_Hinmei_Name_1 = this.form.txt_HinmeiNameRyaku.Text;
            kenSyuuIchiranDTO.Shukka_Detail_Hinmei_Cd_2 = this.form.txt_HinmeiCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Detail_Hinmei_Name_2 = this.form.txt_HinmeiNameRyakuTo.Text;

            kenSyuuIchiranDTO.Shukka_Entry_KENSHUHINMEI_CD_1 = this.form.txt_KenshuHinmeiCD.Text;
            kenSyuuIchiranDTO.Shukka_Entry_KENSHUHINMEI_NAME_1 = this.form.txt_KenshuHinmeiNameRyaku.Text;
            kenSyuuIchiranDTO.Shukka_Entry_KENSHUHINMEI_CD_2 = this.form.txt_KenshuHinmeiCDTo.Text;
            kenSyuuIchiranDTO.Shukka_Entry_KENSHUHINMEI_NAME_2 = this.form.txt_KenshuHinmeiNameRyakuTo.Text;
            if (this.form.txt_KenshuMustMbn.Visible)
            {
                kenSyuuIchiranDTO.Shukka_Entry_KENSHU_UMU = this.form.txt_KenshuMustMbn.Text;
            }
            if (this.form.txt_KenshuJyoukyou.Visible)
            {
                // 必須にしているので値はあるはず。
                short kenshuJyoukyou = 0;
                if (short.TryParse(this.form.txt_KenshuJyoukyou.Text, out kenshuJyoukyou))
                {
                    kenSyuuIchiranDTO.Shukka_Entry_KENSHU_JYOUKYOU = kenshuJyoukyou;
                }
            }

            // 会社略称情報
            DataTable dataTable = this.dao.GetDateForStringSql("SELECT M_CORP_INFO.CORP_RYAKU_NAME from M_CORP_INFO");

            // 会社略称
            kenSyuuIchiranDTO.Shukka_Entry_CORP_RYAKU_NAME = (string)dataTable.Rows[0].ItemArray[0];
            return kenSyuuIchiranDTO;
        }

        /// <summary>
        /// create header
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("KyotenCD");
            dataTable.Columns.Add("KyotenName");
            dataTable.Columns.Add("ShukkaFrom");
            dataTable.Columns.Add("ShukkaTo");
            dataTable.Columns.Add("KenshuFrom");
            dataTable.Columns.Add("KenshuTo");
            dataTable.Columns.Add("TorihikisakiCD");
            dataTable.Columns.Add("TorihikisakiName");
            dataTable.Columns.Add("TorihikisakiCDTo");
            dataTable.Columns.Add("TorihikisakiNameTo");

            dataTable.Columns.Add("GyoushaCD");
            dataTable.Columns.Add("GyoushaName");
            dataTable.Columns.Add("GyoushaCDTo");
            dataTable.Columns.Add("GyoushaNameTo");
            dataTable.Columns.Add("GenbaCD");
            dataTable.Columns.Add("GenbaName");
            dataTable.Columns.Add("GenbaCDTo");
            dataTable.Columns.Add("GenbaNameTo");
            dataTable.Columns.Add("NizumiGyoshaCD");
            dataTable.Columns.Add("NizumiGyoushaNameRyaku");

            dataTable.Columns.Add("NizumiGyoshaCDTo");
            dataTable.Columns.Add("NizumiGyoushaNameRyakuTo");
            dataTable.Columns.Add("NizumiGenbaCD");
            dataTable.Columns.Add("NizumiGenbaNameRyaku");
            dataTable.Columns.Add("NizumiGenbaCDTo");
            dataTable.Columns.Add("NizumiGenbaNameRyakuTo");
            dataTable.Columns.Add("HinmeiCD");
            dataTable.Columns.Add("HinmeiNameRyaku");
            dataTable.Columns.Add("HinmeiCDTo");
            dataTable.Columns.Add("HinmeiNameRyakuTo");

            dataTable.Columns.Add("KenshuHinmeiCD");
            dataTable.Columns.Add("KenshuHinmeiNameRyaku");
            dataTable.Columns.Add("KenshuHinmeiCDTo");
            dataTable.Columns.Add("KenshuHinmeiNameRyakuTo");
            dataTable.Columns.Add("Kenshuyohi");
            dataTable.Columns.Add("Kenshuumu");

            return dataTable;
        }
        #endregion

        /// <summary>
        /// 設定値保存
        /// </summary>
        internal bool SerchSave()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 設定条件を保存
                var info = new KenshuIshiranConstans.ConditionInfo();
                info.KyotenCD = this.form.txt_KyotenCD.Text;                        // 拠点
                info.DStartDay = Convert.ToString(this.form.dtp_ShukkaFrom.Value);	// 伝票日付開始日付
                info.DEndDay = Convert.ToString(this.form.dtp_ShukkaTo.Value);		// 伝票日付終了日付
                info.KStartDay = Convert.ToString(this.form.dtp_KenshuFrom.Value);	// 検収伝票日付開始日付
                info.KEndDay = Convert.ToString(this.form.dtp_KenshuTo.Value);		// 検収伝票日付終了日付
                info.StartTorihikisakiCD = this.form.txt_TorihikisakiCD.Text;		// 開始取引先CD
                info.EndTorihikisakiCD = this.form.txt_TorihikisakiCDTo.Text;		// 終了取引先CD
                info.StartGyoushaCD = this.form.txt_GyoushaCD.Text;		            // 開始業者CD
                info.EndGyoushaCD = this.form.txt_GyoushaCDTo.Text;		            // 終了業者CD
                info.StartGenbaCD = this.form.txt_GenbaCD.Text;		                // 開始現場CD
                info.EndGenbaCD = this.form.txt_GenbaCDTo.Text;		                // 終了現場CD
                info.StartNGyoushaCD = this.form.txt_NizumiGyoshaCD.Text;		    // 開始荷積業者CD
                info.EndNGyoushaCD = this.form.txt_NizumiGyoshaCDTo.Text;		    // 終了荷積業者CD
                info.StartNGenbaCD = this.form.txt_NizumiGenbaCD.Text;		        // 開始荷積現場CD
                info.EndNGenbaCD = this.form.txt_NizumiGenbaCDTo.Text;		        // 終了荷積現場CD
                info.StartSHinmokuCD = this.form.txt_HinmeiCD.Text;		            // 開始出荷品目CD
                info.EndSHinmokuCD = this.form.txt_HinmeiCDTo.Text;		            // 終了出荷品目CD
                info.StartKHinmokuCD = this.form.txt_KenshuHinmeiCD.Text;		    // 開始検収品目CD
                info.EndKHinmokuCD = this.form.txt_KenshuHinmeiCDTo.Text;		    // 終了検収品目CD
                info.KenshuJoKBN = this.form.txt_KenshuJyoukyou.Text;	// 検収状況
                info.KenshuUmKBN = this.form.txt_KenshuMustMbn.Text;		// 検収有無
                info.DataSetFlag = true;
                this.form.Joken = info;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SerchSave", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// 20141203 Houkakou 日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShukkaDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtp_ShukkaFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtp_ShukkaTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtp_ShukkaFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtp_ShukkaTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.dtp_ShukkaFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.form.dtp_ShukkaTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtp_ShukkaFrom.IsInputErrorOccured = true;
                this.form.dtp_ShukkaTo.IsInputErrorOccured = true;
                this.form.dtp_ShukkaFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_ShukkaTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "伝票日付範囲指定From", "伝票日付範囲指定To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_ShukkaFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtp_ShukkaFrom_Leaveイベント
        /// <summary>
        /// dtp_ShukkaFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_ShukkaFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_ShukkaTo.Text))
            {
                this.form.dtp_ShukkaTo.IsInputErrorOccured = false;
                this.form.dtp_ShukkaTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtp_ShukkaTo_Leaveイベント
        /// <summary>
        /// dtp_ShukkaTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_ShukkaTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_ShukkaFrom.Text))
            {
                this.form.dtp_ShukkaFrom.IsInputErrorOccured = false;
                this.form.dtp_ShukkaFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool KenshuDateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtp_KenshuFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtp_KenshuTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtp_KenshuFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtp_KenshuTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.dtp_KenshuFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.form.dtp_KenshuTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtp_KenshuFrom.IsInputErrorOccured = true;
                this.form.dtp_KenshuTo.IsInputErrorOccured = true;
                this.form.dtp_KenshuFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtp_KenshuTo.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "検収伝票日付範囲指定From", "検収伝票日付範囲指定To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtp_KenshuFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtp_KenshuFrom_Leaveイベント
        /// <summary>
        /// dtp_KenshuFrom_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KenshuFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KenshuTo.Text))
            {
                this.form.dtp_KenshuTo.IsInputErrorOccured = false;
                this.form.dtp_KenshuTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtp_KenshuTo_Leaveイベント
        /// <summary>
        /// dtp_KenshuTo_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtp_KenshuTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtp_KenshuFrom.Text))
            {
                this.form.dtp_KenshuFrom.IsInputErrorOccured = false;
                this.form.dtp_KenshuFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 日付チェックを追加する　end
    }
}
