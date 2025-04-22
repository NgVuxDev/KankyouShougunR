using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

namespace Shougun.Core.Billing.AtenaLabel
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// 宛名ラベル画面Form
        /// </summary>
        private AtenaLabel.UIForm form;

        /// <summary>
        /// ParentForm
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private r_framework.Dao.IM_GENBADao genbaDao;

        /// <summary>
        /// 宛名Dao
        /// </summary>
        private Shougun.Core.Billing.AtenaLabel.DAO.DaoClass atenaDataDao;

        /// <summary>
        /// CD・名称Dictionary
        /// </summary>
        private Dictionary<r_framework.CustomControl.CustomAlphaNumTextBox, r_framework.CustomControl.CustomTextBox> allControlDict;

        // No.3883-->
        /// <summary>
        /// コンボボックス初期化フラグ
        /// </summary>
        private bool bInitialize = true;

        /// <summary>
        /// 検索開始日付
        /// </summary>
        //private DateTime selectFromDate;

        /// <summary>
        /// 検索終了日付
        /// </summary>
        //private DateTime selectToDate;
        // No.3883<--

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.atenaDataDao = DaoInitUtility.GetComponent<Shougun.Core.Billing.AtenaLabel.DAO.DaoClass>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ParentFormのSet
            parentForm = (BusinessBaseForm)this.form.Parent;

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;

            // 画面表示初期化
            this.SetInitDisp();

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

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns name="ButtonSetting[]">XMLに記載されたButtonのリスト</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var tmp = buttonSetting.LoadButtonSetting(thisAssembly, Const.UIConstans.ButtonInfoXmlPath);
            return tmp;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //印刷区分イベント
            this.form.txtPrintKbn.TextChanged += new EventHandler(txtPrintKbn_TextChanged);
            this.form.rdoTorihikisaki.CheckedChanged += new EventHandler(rdoTorihikisaki_CheckedChanged);
            this.form.rdoGyousha.CheckedChanged += new EventHandler(rdoGyousha_CheckedChanged);
            this.form.rdoGenba.CheckedChanged += new EventHandler(rdoGenba_CheckedChanged);

            //印刷方法イベント
            this.form.txtPrintHouhou.TextChanged += new EventHandler(txtPrintHouhou_TextChanged);
            this.form.rdoKobetu.CheckedChanged += new EventHandler(rdoKobetu_CheckedChanged);
            this.form.rdoPage.CheckedChanged += new EventHandler(rdoPage_CheckedChanged);

            //ファンクションキーイベント
            this.parentForm.bt_func3.Click += new EventHandler(bt_func3_Click); // No.3883
            this.parentForm.bt_func4.Click += new EventHandler(bt_func4_Click); // No.3883
            this.parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);
            this.parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);

            //フォーカスアウトイベント
            this.form.txtPrintKbn.Validating += new CancelEventHandler(txtPrintKbn_Validating);
            this.form.txtTorihikisakiCd.Validating += new CancelEventHandler(txtTorihikisakiCd_Validating);
            this.form.txtGyoushaCd.Validating += new CancelEventHandler(txtGyoushaCd_Validating);
            this.form.txtGenbaCd.Validating += new CancelEventHandler(txtGenbaCd_Validating);
            this.form.txtKobetuShiteiCd1.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd2.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd3.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd4.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd5.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd6.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd7.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd8.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd9.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd10.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd11.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);
            this.form.txtKobetuShiteiCd12.Validating += new CancelEventHandler(txtKobetuShiteiCd_Validating);

            // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            this.form.txtGyoushaCd.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd1.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd2.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd3.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd4.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd5.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd6.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd7.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd8.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd9.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd10.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd11.Enter += new EventHandler(txtGyoushaCd_Enter);
            this.form.txtKobetuShiteiCd12.Enter += new EventHandler(txtGyoushaCd_Enter);
            // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
            this.form.txtNum_HensousakiKbn.Validated += new EventHandler(txtNum_HensousakiKbn_Validated);
            this.form.txtNum_HensousakiKbn.TextChanged += new EventHandler(txtNum_HensousakiKbn_TextChanged);
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end

            // 20141226 Houkakou 「宛名ラベル」のダブルクリックを追加する start
            // 「To」のイベント生成
            this.form.dtpTaishoKikanTo.MouseDoubleClick += new MouseEventHandler(dtpTaishoKikanTo_MouseDoubleClick);
            // 20141226 Houkakou 「宛名ラベル」のダブルクリックを追加する end

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 印刷区分（テキストボックス）イベント

        /// <summary>
        /// 印刷区分（テキストボックス）イベント
        /// </summary>
        private void txtPrintKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            //フォーカスアウト時とスペースキー押下時の検索対象を変更する
            this.searchParamsChanged(true);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷区分（取引先）イベント

        /// <summary>
        /// 印刷区分イベント
        /// </summary>
        private void rdoTorihikisaki_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            //フォーカスアウト時とスペースキー押下時の検索対象を変更する
            this.searchParamsChanged(true);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷区分（業者）イベント

        /// <summary>
        /// 印刷区分イベント
        /// </summary>
        private void rdoGyousha_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            //フォーカスアウト時とスペースキー押下時の検索対象を変更する
            this.searchParamsChanged(true);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷区分（現場）イベント

        /// <summary>
        /// 印刷区分イベント
        /// </summary>
        private void rdoGenba_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            //フォーカスアウト時とスペースキー押下時の検索対象を変更する
            this.searchParamsChanged(true);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷方法（テキストボックス）イベント

        /// <summary>
        /// 印刷方法イベント
        /// </summary>
        private void txtPrintHouhou_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷方法（個別）イベント

        /// <summary>
        /// 印刷方法イベント
        /// </summary>
        private void rdoKobetu_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 印刷方法（ページ）イベント

        /// <summary>
        /// 印刷方法イベント
        /// </summary>
        private void rdoPage_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //コントロールの活性/非活性を制御する
            this.controlmanagement();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
        /// <summary>
        /// 返送先区分 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_HensousakiKbn_Validated(object sender, EventArgs e)
        {
            r_framework.CustomControl.CustomNumericTextBox2 text = sender as r_framework.CustomControl.CustomNumericTextBox2;
            if (this.form.txtPrintKbn.Text != "3" || !text.Enabled) { return; }
            if (string.IsNullOrEmpty(text.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //返送先区分が空の場合、メッセージ「返送先区分は必須項目です。入力してください。」を表示する
                msgLogic.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを返送先区分へ移動
                text.Select();
            }
        }

        private string txtNum_HensousakiKbn_OldValue = "";

        /// <summary>
        /// 返送先区分 TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_HensousakiKbn_TextChanged(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;

            // マニフェスト宛名ラベルかつ印刷区分が現場の場合
            if (this.form.dispType == Const.UIConstans.MANIFEST && this.form.txtPrintKbn.Text == "3")
            {
                // 返送先区分入力時
                if (false == string.IsNullOrEmpty(text.Text))
                {
                    // 入力された区分が無効であれば、入力を取り消す
                    bool cancel = false;
                    switch (text.Text)
                    {
                        case "1":
                            // A票
                            if (this.form.radbtn_A.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "2":
                            // B1票
                            if (this.form.radbtn_B1.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "3":
                            // B2票
                            if (this.form.radbtn_B2.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "4":
                            // B4票
                            if (this.form.radbtn_B4.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "5":
                            // B6票
                            if (this.form.radbtn_B6.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "6":
                            // C1票
                            if (this.form.radbtn_C1.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "7":
                            // C2票
                            if (this.form.radbtn_C2.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "8":
                            // D票
                            if (this.form.radbtn_D.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;

                        case "9":
                            // E票
                            if (this.form.radbtn_E.Enabled == false)
                            {
                                // 入力取り消し
                                cancel = true;
                            }
                            break;
                    }

                    if (cancel == true)
                    {
                        // 入力を取り消す
                        text.Text = this.txtNum_HensousakiKbn_OldValue;
                    }
                }
            }

            if (this.form.txtPrintHouhou.Text == "2" || string.IsNullOrEmpty(text.Text))
            {
                this.txtNum_HensousakiKbn_OldValue = text.Text;
                return;
            }

            if (this.txtNum_HensousakiKbn_OldValue != text.Text)
            {
                if (!string.IsNullOrEmpty(this.form.txtKobetuShiteiCd1.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd2.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd3.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd4.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd5.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd6.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd7.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd8.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd9.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd10.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd11.Text)
                || !string.IsNullOrEmpty(this.form.txtKobetuShiteiCd12.Text))
                {
                    // 個別指定CDが入力されていない場合は、メッセージ表示を行わない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult result = msgLogic.MessageBoxShow("C073");
                    if (result == DialogResult.OK || result == DialogResult.Yes)
                    {
                        this.form.txtKobetuShiteiCd1.Text = string.Empty;
                        this.form.txtKobetuShiteiName1.Text = string.Empty;
                        this.form.txtKobetuShiteiCd2.Text = string.Empty;
                        this.form.txtKobetuShiteiName2.Text = string.Empty;
                        this.form.txtKobetuShiteiCd3.Text = string.Empty;
                        this.form.txtKobetuShiteiName3.Text = string.Empty;
                        this.form.txtKobetuShiteiCd4.Text = string.Empty;
                        this.form.txtKobetuShiteiName4.Text = string.Empty;
                        this.form.txtKobetuShiteiCd5.Text = string.Empty;
                        this.form.txtKobetuShiteiName5.Text = string.Empty;
                        this.form.txtKobetuShiteiCd6.Text = string.Empty;
                        this.form.txtKobetuShiteiName6.Text = string.Empty;
                        this.form.txtKobetuShiteiCd7.Text = string.Empty;
                        this.form.txtKobetuShiteiName7.Text = string.Empty;
                        this.form.txtKobetuShiteiCd8.Text = string.Empty;
                        this.form.txtKobetuShiteiName8.Text = string.Empty;
                        this.form.txtKobetuShiteiCd9.Text = string.Empty;
                        this.form.txtKobetuShiteiName9.Text = string.Empty;
                        this.form.txtKobetuShiteiCd10.Text = string.Empty;
                        this.form.txtKobetuShiteiName10.Text = string.Empty;
                        this.form.txtKobetuShiteiCd11.Text = string.Empty;
                        this.form.txtKobetuShiteiName11.Text = string.Empty;
                        this.form.txtKobetuShiteiCd12.Text = string.Empty;
                        this.form.txtKobetuShiteiName12.Text = string.Empty;
                    }
                    else
                    {
                        text.Text = this.txtNum_HensousakiKbn_OldValue;
                    }
                }

                // 検索条件更新
                this.searchParamsChanged(false);
                this.txtNum_HensousakiKbn_OldValue = text.Text;
            }
        }

        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end

        #region 画面項目制御

        /// <summary>
        /// 画面項目制御
        /// </summary>
        private void controlmanagement()
        {
            LogUtility.DebugMethodStart();

            //コントロールの活性/非活性を制御する
            if (this.form.txtPrintHouhou.Text == "1")
            {
                //印刷方法が個別の場合

                //個別設定の制御
                this.form.txtKobetuShiteiCd1.Enabled = true;
                this.form.txtKobetuShiteiCd2.Enabled = true;
                this.form.txtKobetuShiteiCd3.Enabled = true;
                this.form.txtKobetuShiteiCd4.Enabled = true;
                this.form.txtKobetuShiteiCd5.Enabled = true;
                this.form.txtKobetuShiteiCd6.Enabled = true;
                this.form.txtKobetuShiteiCd7.Enabled = true;
                this.form.txtKobetuShiteiCd8.Enabled = true;
                this.form.txtKobetuShiteiCd9.Enabled = true;
                this.form.txtKobetuShiteiCd10.Enabled = true;
                this.form.txtKobetuShiteiCd11.Enabled = true;
                this.form.txtKobetuShiteiCd12.Enabled = true;
                this.form.cmbPrintKaishiIti.Text = "1";
                this.form.cmbPrintKaishiIti.Enabled = false;

                if ((this.form.txtPrintKbn.Text == "1") || (this.form.txtPrintKbn.Text == "2"))
                {
                    //印刷区分が「1.取引先　2.業者」の場合
                    //取引先、業者、現場の制御
                    this.form.txtTorihikisakiCd.Enabled = false;
                    this.form.txtGyoushaCd.Enabled = false;
                    this.form.txtGenbaCd.Enabled = false;
                }
                else if (this.form.txtPrintKbn.Text == "3")
                {
                    //印刷区分が「3.現場」の場合
                    //取引先、業者、現場の制御
                    this.form.txtGyoushaCd.Enabled = true;
                    this.form.txtTorihikisakiCd.Enabled = false;
                    this.form.txtGenbaCd.Enabled = false;
                }
            }
            else if (this.form.txtPrintHouhou.Text == "2")
            {
                //印刷方法がページの場合

                //個別設定の制御
                this.form.txtKobetuShiteiCd1.Enabled = false;
                this.form.txtKobetuShiteiCd2.Enabled = false;
                this.form.txtKobetuShiteiCd3.Enabled = false;
                this.form.txtKobetuShiteiCd4.Enabled = false;
                this.form.txtKobetuShiteiCd5.Enabled = false;
                this.form.txtKobetuShiteiCd6.Enabled = false;
                this.form.txtKobetuShiteiCd7.Enabled = false;
                this.form.txtKobetuShiteiCd8.Enabled = false;
                this.form.txtKobetuShiteiCd9.Enabled = false;
                this.form.txtKobetuShiteiCd10.Enabled = false;
                this.form.txtKobetuShiteiCd11.Enabled = false;
                this.form.txtKobetuShiteiCd12.Enabled = false;
                this.form.cmbPrintKaishiIti.Enabled = true;
                if (this.form.txtPrintKbn.Text == "1")
                {
                    //印刷区分が「1.取引先」の場合
                    //取引先、業者、現場の制御
                    this.form.txtTorihikisakiCd.Enabled = true;
                    this.form.txtGyoushaCd.Enabled = false;
                    this.form.txtGenbaCd.Enabled = false;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 start
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaName.Text = string.Empty;
                    this.form.txtGyoushaCd.Text = string.Empty;
                    this.form.txtGyoushaName.Text = string.Empty;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 end
                }
                else if (this.form.txtPrintKbn.Text == "2")
                {
                    //印刷区分が「2.業者」の場合
                    //取引先、業者、現場の制御
                    this.form.txtGyoushaCd.Enabled = true;
                    this.form.txtTorihikisakiCd.Enabled = false;
                    this.form.txtGenbaCd.Enabled = false;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 start
                    this.form.txtTorihikisakiCd.Text = string.Empty;
                    this.form.txtTorihikisakiName.Text = string.Empty;
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaName.Text = string.Empty;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 end
                }
                else if (this.form.txtPrintKbn.Text == "3")
                {
                    //印刷区分が「3.現場」の場合
                    //取引先、業者、現場の制御
                    this.form.txtGyoushaCd.Enabled = true;
                    this.form.txtGenbaCd.Enabled = true;
                    this.form.txtTorihikisakiCd.Enabled = false;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 start
                    this.form.txtTorihikisakiCd.Text = string.Empty;
                    this.form.txtTorihikisakiName.Text = string.Empty;
                    this.form.txtGyoushaCd.Text = string.Empty;
                    this.form.txtGyoushaName.Text = string.Empty;
                    this.form.txtGenbaCd.Text = string.Empty;
                    this.form.txtGenbaName.Text = string.Empty;
                    // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 end
                }

                // 印刷方法「２．ページ」かつ印刷区分が「3.現場」を選択している時、現場マスタにて設定している返送先区分のみ活性化しそれ以外を非活性とする
                if (this.form.txtPrintKbn.Text == "3")
                {
                    DataTable Dt = new DataTable();
                    Dt = this.atenaDataDao.GetGenbaCd(this.form.txtGyoushaCd.Text, this.form.txtGenbaCd.Text, this.form.dispType.ToString());
                    if (Dt.Rows.Count > 0)
                    {
                        string enable = "";
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_A"], this.form.radbtn_A, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B1"], this.form.radbtn_B1, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B2"], this.form.radbtn_B2, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B4"], this.form.radbtn_B4, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B6"], this.form.radbtn_B6, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_C1"], this.form.radbtn_C1, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_C2"], this.form.radbtn_C2, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_D"], this.form.radbtn_D, ref enable);
                        this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_E"], this.form.radbtn_E, ref enable);
                        if (string.IsNullOrEmpty(enable))
                        {
                            //全ての返送先区分ラジオボタンが利用不可になった場合
                            this.txtNum_HensousakiKbn_OldValue = string.Empty;
                            this.form.txtNum_HensousakiKbn.Text = string.Empty;
                            this.form.txtNum_HensousakiKbn.Enabled = false;
                        }
                        else
                        {
                            //ある返送先区分ラジオボタンが利用可になった場合
                            //一番目の利用可ラジオボタンを選択する
                            this.txtNum_HensousakiKbn_OldValue = enable;
                            this.form.txtNum_HensousakiKbn.Text = enable;
                            this.form.txtNum_HensousakiKbn.Enabled = true;
                        }
                    }
                    else
                    {
                        // 該当CDがなかった場合は返送先区分を非活性とする
                        this.txtNum_HensousakiKbn_OldValue = string.Empty;
                        this.form.txtNum_HensousakiKbn.Text = string.Empty;
                        this.form.txtNum_HensousakiKbn.Enabled = false;
                    }
                }
            }
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
            switch (this.form.txtPrintKbn.Text)
            {
                case "1":
                case "2":
                    //印刷区分が「1.取引先　2.業者」の場合
                    this.txtNum_HensousakiKbn_OldValue = string.Empty;
                    this.form.txtNum_HensousakiKbn.Text = string.Empty;
                    this.form.txtNum_HensousakiKbn.Enabled = false;
                    this.form.radbtn_A.Enabled = false;
                    this.form.radbtn_B1.Enabled = false;
                    this.form.radbtn_B2.Enabled = false;
                    this.form.radbtn_B4.Enabled = false;
                    this.form.radbtn_B6.Enabled = false;
                    this.form.radbtn_C1.Enabled = false;
                    this.form.radbtn_C2.Enabled = false;
                    this.form.radbtn_D.Enabled = false;
                    this.form.radbtn_E.Enabled = false;
                    break;

                case "3":
                    //印刷区分が「3.現場」の場合
                    if (this.form.txtPrintHouhou.Text == "1")
                    {
                        //印刷方法が個別の場合
                        this.form.radbtn_A.Enabled = true;
                        this.form.radbtn_B1.Enabled = true;
                        this.form.radbtn_B2.Enabled = true;
                        this.form.radbtn_B4.Enabled = true;
                        this.form.radbtn_B6.Enabled = true;
                        this.form.radbtn_C1.Enabled = true;
                        this.form.radbtn_C2.Enabled = true;
                        this.form.radbtn_D.Enabled = true;
                        this.form.radbtn_E.Enabled = true;
                        this.form.txtNum_HensousakiKbn.Enabled = true;
                        this.txtNum_HensousakiKbn_OldValue = "1";
                        this.form.txtNum_HensousakiKbn.Text = "1";
                        this.searchParamsChanged(true);
                    }
                    else
                    {
                        //印刷方法がページの場合
                        // 20140703 ria EV005130 印刷方法を「１．個別」⇒「２．ページ」に変更した時、返送先区分のテキストのみ非活性でラジオボタン自体は活性化してしまっている。 start
                        this.txtNum_HensousakiKbn_OldValue = string.Empty;
                        this.form.txtNum_HensousakiKbn.Text = string.Empty;
                        this.form.txtNum_HensousakiKbn.Enabled = false;
                        this.form.radbtn_A.Enabled = false;
                        this.form.radbtn_B1.Enabled = false;
                        this.form.radbtn_B2.Enabled = false;
                        this.form.radbtn_B4.Enabled = false;
                        this.form.radbtn_B6.Enabled = false;
                        this.form.radbtn_C1.Enabled = false;
                        this.form.radbtn_C2.Enabled = false;
                        this.form.radbtn_D.Enabled = false;
                        this.form.radbtn_E.Enabled = false;
                        // 20140703 ria EV005130 印刷方法を「１．個別」⇒「２．ページ」に変更した時、返送先区分のテキストのみ非活性でラジオボタン自体は活性化してしまっている。 end
                    }
                    break;
            }
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
        /// <summary>
        /// 現場のデータによって、返送先区分ラジオボタンのEnableを設定する
        /// </summary>
        /// <param name="use">現場データの返送先利用可区分</param>
        /// <param name="btn">返送先区分ラジオボタン</param>
        /// <param name="enable">一番目の利用可返送先区分ラジオボタンのValue</param>
        private void setRadbtn(object use, r_framework.CustomControl.CustomRadioButton btn, ref string enable)
        {
            btn.Enabled = use != DBNull.Value && Convert.ToInt16(use) == 1;
            if (btn.Enabled)
            {
                enable = string.IsNullOrEmpty(enable) ? btn.Value : enable;
            }
        }

        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end

        #region 検索対象変更イベント

        /// <summary>
        /// 検索対象変更イベント
        /// </summary>
        /// <param name="txtInit">TRUE:条件設定時にtxt初期化</param>
        private void searchParamsChanged(bool txtInit)
        {
            LogUtility.DebugMethodStart();

            // 請求・精算宛名ラベルでマニフェスト返送先を検索条件に含めないために条件追加
            if (this.form.dispType == Const.UIConstans.MANIFEST)
            {
                if (this.form.txtPrintKbn.Text == "2")
                {
                    // 印刷区分が2.業者の場合
                    // マニフェスト返送先になっている業者のみ抽出
                    this.form.txtGyoushaCd.PopupSearchSendParams = this.form.txtManiGyoushaCd.PopupSearchSendParams;
                }
                else if(this.form.txtPrintKbn.Text == "3")
                {
                    // 印刷区分が3.現場の場合
                    // マニフェスト返送先になっている現場を持っている業者のみ抽出
                    this.form.txtGyoushaCd.PopupSearchSendParams = new Collection<r_framework.Dto.PopupSearchSendParamDto>();
                }
            }
            Dictionary<r_framework.CustomControl.CustomAlphaNumTextBox, r_framework.CustomControl.CustomTextBox> kobetsuControlDict
                = new Dictionary<r_framework.CustomControl.CustomAlphaNumTextBox, r_framework.CustomControl.CustomTextBox>();

            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd1, this.form.txtKobetuShiteiName1);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd2, this.form.txtKobetuShiteiName2);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd3, this.form.txtKobetuShiteiName3);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd4, this.form.txtKobetuShiteiName4);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd5, this.form.txtKobetuShiteiName5);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd6, this.form.txtKobetuShiteiName6);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd7, this.form.txtKobetuShiteiName7);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd8, this.form.txtKobetuShiteiName8);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd9, this.form.txtKobetuShiteiName9);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd10, this.form.txtKobetuShiteiName10);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd11, this.form.txtKobetuShiteiName11);
            kobetsuControlDict.Add(this.form.txtKobetuShiteiCd12, this.form.txtKobetuShiteiName12);

            //コントロールの検索対象を変更する
            if (this.form.txtPrintKbn.Text == "1")
            {
                foreach (r_framework.CustomControl.CustomAlphaNumTextBox ctrl in kobetsuControlDict.Keys)
                {
                    ctrl.DBFieldsName = "TORIHIKISAKI_CD";
                    ctrl.FocusOutCheckMethod = this.form.txtTorihikisakiCd.FocusOutCheckMethod;
                    ctrl.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
                    ctrl.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
                    ctrl.PopupSetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name;
                    ctrl.PopupSearchSendParams = this.form.txtTorihikisakiCd.PopupSearchSendParams;
                    ctrl.PopupWindowId = WINDOW_ID.M_TORIHIKISAKI;
                    ctrl.PopupWindowName = "検索共通ポップアップ";
                    ctrl.popupWindowSetting = this.form.txtTorihikisakiCd.popupWindowSetting;
                    ctrl.SetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name;
                    if (txtInit == true)
                    {
                        ctrl.Text = "";
                        kobetsuControlDict[ctrl].Text = "";
                    }
                }
            }
            else if (this.form.txtPrintKbn.Text == "2")
            {
                if (this.form.dispType != Const.UIConstans.MANIFEST)
                {
                    this.form.txtGyoushaCd.popupWindowSetting[3].SearchCondition[3].Value = "2";
                }

                foreach (r_framework.CustomControl.CustomAlphaNumTextBox ctrl in kobetsuControlDict.Keys)
                {
                    ctrl.DBFieldsName = "GYOUSHA_CD";
                    ctrl.FocusOutCheckMethod = this.form.txtGyoushaCd.FocusOutCheckMethod;
                    ctrl.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
                    ctrl.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
                    ctrl.PopupSetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name;
                    ctrl.PopupSearchSendParams = this.form.txtGyoushaCd.PopupSearchSendParams;
                    ctrl.PopupWindowId = WINDOW_ID.M_GYOUSHA;
                    ctrl.PopupWindowName = "検索共通ポップアップ";
                    ctrl.popupWindowSetting = this.form.txtGyoushaCd.popupWindowSetting;
                    ctrl.SetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name;
                    if (txtInit == true)
                    {
                        ctrl.Text = "";
                        kobetsuControlDict[ctrl].Text = "";
                    }
                }
            }
            else if (this.form.txtPrintKbn.Text == "3")
            {
                if (this.form.dispType != Const.UIConstans.MANIFEST)
                {
                    this.form.txtGyoushaCd.popupWindowSetting[3].SearchCondition[3].Value = "3";
                }

                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                Collection<r_framework.Dto.PopupSearchSendParamDto> paramDtos = new Collection<r_framework.Dto.PopupSearchSendParamDto>();
                foreach (r_framework.Dto.PopupSearchSendParamDto paramDto in this.form.txtGenbaCd.PopupSearchSendParams)
                {
                    paramDtos.Add(paramDto);
                }
                if ((this.form.dispType == Const.UIConstans.MANIFEST) &&
                   (!string.IsNullOrEmpty(this.form.txtNum_HensousakiKbn.Text)))
                {
                    r_framework.Dto.PopupSearchSendParamDto dto = new r_framework.Dto.PopupSearchSendParamDto();
                    dto.And_Or = CONDITION_OPERATOR.AND;
                    switch (this.form.txtNum_HensousakiKbn.Text)
                    {
                        case "1":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_A";
                            break;

                        case "2":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_B1";
                            break;

                        case "3":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_B2";
                            break;

                        case "4":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_B4";
                            break;

                        case "5":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_B6";
                            break;

                        case "6":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_C1";
                            break;

                        case "7":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_C2";
                            break;

                        case "8":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_D";
                            break;

                        case "9":
                            dto.KeyName = "MANI_HENSOUSAKI_USE_E";
                            break;
                    }
                    dto.Value = "1";
                    paramDtos.Add(dto);
                }
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                foreach (r_framework.CustomControl.CustomAlphaNumTextBox ctrl in kobetsuControlDict.Keys)
                {
                    ctrl.DBFieldsName = "GENBA_CD";
                    ctrl.FocusOutCheckMethod = this.form.txtGenbaCd.FocusOutCheckMethod;
                    ctrl.GetCodeMasterField = "";
                    ctrl.PopupGetMasterField = this.form.txtGenbaCd.PopupGetMasterField;
                    ctrl.PopupSearchSendParams = paramDtos;
                    ctrl.PopupSetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name + ", txtGyoushaCd, txtGyoushaName";
                    ctrl.PopupWindowId = WINDOW_ID.M_GENBA;
                    ctrl.PopupWindowName = "複数キー用検索共通ポップアップ";
                    ctrl.popupWindowSetting = this.form.txtGenbaCd.popupWindowSetting;
                    ctrl.SetFormField = ctrl.Name + "," + kobetsuControlDict[ctrl].Name;
                    if (txtInit == true)
                    {
                        ctrl.Text = "";
                        kobetsuControlDict[ctrl].Text = "";
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // No.3883-->
        #region "[F3]前月、[F4]翌月押下時"

        /// <summary>
        /// 対象期間を１ヶ月前にします
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            this.SentakuClear();
            this.MinusMonth();
        }

        /// <summary>
        /// 対象期間を１ヶ月後にします
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void bt_func4_Click(object sender, EventArgs e)
        {
            this.SentakuClear();
            this.PlusMonth();
        }

        #region 一月後に変更

        /// <summary>
        /// 一月後に変更
        /// </summary>
        internal void PlusMonth()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cmbShimebi != null)
            {
                DateTime dtpFrom = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());

                DateTime chkFrom = dtpFrom.AddDays(1);
                DateTime chkTo = dtpTo.AddDays(1);

                DateTime nextToMonth = dtpTo.AddMonths(1);
                DateTime nextFromMonth = dtpFrom.AddMonths(1);

                if (chkFrom.Month != dtpFrom.Month)
                {
                    //入力された日付Fromが末日
                    int nextDays = DateTime.DaysInMonth(nextFromMonth.Year, nextFromMonth.Month);
                    DateTime setFromDtp = new DateTime(nextFromMonth.Year, nextFromMonth.Month, nextDays);

                    form.dtpTaishoKikanFrom.SetResultText(setFromDtp.ToString());
                }
                else
                {
                    form.dtpTaishoKikanFrom.SetResultText(nextFromMonth.ToString());
                }

                if (chkTo.Month != dtpTo.Month)
                {
                    //入力された日付Toが末日
                    int nextDays = DateTime.DaysInMonth(nextToMonth.Year, nextToMonth.Month);
                    DateTime setToDtp = new DateTime(nextToMonth.Year, nextToMonth.Month, nextDays);

                    form.dtpTaishoKikanTo.SetResultText(setToDtp.ToString());
                }
                else
                {
                    form.dtpTaishoKikanTo.SetResultText(nextToMonth.ToString());
                }

                // 検索期間を取得
                if (form.cmbShimebi.SelectedIndex == 0)
                {
                    form.selectFromDate.Value = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    form.selectToDate.Value = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());
                }
                else
                {
                    form.selectToDate.Value = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime workdate1 = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime workDate2 = workdate1.AddMonths(-1);
                    form.selectFromDate.Value = workDate2.AddDays(1);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 一月後に変更

        #region 一か月前に変更

        /// <summary>
        /// 一か月前に変更
        /// </summary>
        internal void MinusMonth()
        {
            LogUtility.DebugMethodStart();

            if (this.form.cmbShimebi != null)
            {
                DateTime dtpFrom = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());

                DateTime chkFrom = dtpFrom.AddDays(1);
                DateTime chkTo = dtpTo.AddDays(1);

                DateTime preToMonth = dtpTo.AddMonths(-1);
                DateTime preFromMonth = dtpFrom.AddMonths(-1);

                if (chkFrom.Month != dtpFrom.Month)
                {
                    //入力された日付Fromが末日
                    int preDays = DateTime.DaysInMonth(preFromMonth.Year, preFromMonth.Month);
                    DateTime setFromDtp = new DateTime(preFromMonth.Year, preFromMonth.Month, preDays);

                    form.dtpTaishoKikanFrom.SetResultText(setFromDtp.ToString());
                }
                else
                {
                    form.dtpTaishoKikanFrom.SetResultText(preFromMonth.ToString());
                }

                if (chkTo.Month != dtpTo.Month)
                {
                    //入力された日付Toが末日
                    int preDays = DateTime.DaysInMonth(preToMonth.Year, preToMonth.Month);
                    DateTime setToDtp = new DateTime(preToMonth.Year, preToMonth.Month, preDays);

                    form.dtpTaishoKikanTo.SetResultText(setToDtp.ToString());
                }
                else
                {
                    form.dtpTaishoKikanTo.SetResultText(preToMonth.ToString());
                }

                // 検索期間を取得
                if (form.cmbShimebi.SelectedIndex == 0)
                {
                    form.selectFromDate.Value = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    form.selectToDate.Value = DateTime.Parse(form.dtpTaishoKikanTo.GetResultText());
                }
                else
                {
                    form.selectToDate.Value = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime workDate1 = DateTime.Parse(form.dtpTaishoKikanFrom.GetResultText());
                    DateTime workDate2 = workDate1.AddMonths(-1);
                    form.selectFromDate.Value = workDate2.AddDays(1);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion 一か月前に変更

        #endregion "[F3]前月、[F4]翌月押下時"
        // No.3883<--

        #region 印刷ボタンイベント

        /// <summary>
        /// 印刷ボタンイベント
        /// </summary>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //必須チェック
            /*int iNumControl = this.form.allControl.Length;
            Control[] aryCtrl = new Control[iNumControl];
            this.form.allControl.CopyTo(aryCtrl, 0);
            this.form.allControl = aryCtrl;

            var autoCheckLogic = new AutoRegistCheckLogic(aryCtrl, aryCtrl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();*/

            var autoRegistCheckLogic = new AutoRegistCheckLogic(this.allControl);
            this.form.RegistErrorFlag = autoRegistCheckLogic.AutoRegistCheck();

            // マニ返送先区分は必須。選択された現場のマニ返送先区分が全て「使用しない」の時に
            // ここを通過してしまう。
            if ((this.form.dispType == Const.UIConstans.MANIFEST)
                && (this.form.txtPrintKbn.Text == "3")
                && (this.form.txtPrintHouhou.Text == "2")
                && !this.form.txtNum_HensousakiKbn.Enabled
                && string.IsNullOrEmpty(this.form.txtNum_HensousakiKbn.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //返送先区分が空の場合、メッセージ「返送先区分は必須項目です。入力してください。」を表示する
                msgLogic.MessageBoxShow("E001", this.form.txtNum_HensousakiKbn.DisplayItemName);
                this.form.RegistErrorFlag = true;
            }

            if (!this.form.RegistErrorFlag)
            {
                //DTO作成
                this.dto = new DTOClass();

                //DTO初期化
                this.dto.printHouhou = "";
                this.dto.printKubun = "";
                this.dto.TorihikisakiCd = "";
                this.dto.GyoushaCd = "";
                this.dto.GenbaCd = "";
                this.dto.kobetsuShitei = "";

                //個別指定用リスト作成
                List<string> kobetsuList = new List<string>();
                //個別指定用番号リスト作成
                List<string> kobetsuNumberList = new List<string>();

                //印刷方法
                this.dto.printHouhou = this.form.txtPrintHouhou.Text;
                //印刷区分
                this.dto.printKubun = this.form.txtPrintKbn.Text;

                if (this.form.txtPrintHouhou.Text == "1")
                {
                    //印刷方法が「1.個別」の場合
                    //個別指定の設定
                    kobetsuList.Add(this.form.txtKobetuShiteiCd1.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd2.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd3.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd4.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd5.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd6.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd7.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd8.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd9.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd10.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd11.Text);
                    kobetsuList.Add(this.form.txtKobetuShiteiCd12.Text);

                    this.dto.kobetsuShitei = listToString(kobetsuList);

                    if ((this.form.txtPrintKbn.Text == "3") && (this.form.txtGyoushaCd.Text != ""))
                    {
                        //印刷区分が「3.現場」の場合
                        this.dto.GyoushaCd = this.form.txtGyoushaCd.Text;
                    }
                }
                else if (this.form.txtPrintHouhou.Text == "2")
                {
                    //印刷方法が「2.ページ」の場合
                    if ((this.form.txtPrintKbn.Text == "1") && (this.form.txtTorihikisakiCd.Text != ""))
                    {
                        //印刷区分が「1.取引先」の場合
                        this.dto.TorihikisakiCd = this.form.txtTorihikisakiCd.Text;
                    }
                    else if ((this.form.txtPrintKbn.Text == "2") && (this.form.txtGyoushaCd.Text != ""))
                    {
                        //印刷区分が「2.業者」の場合
                        this.dto.GyoushaCd = this.form.txtGyoushaCd.Text;
                    }
                    else if ((this.form.txtPrintKbn.Text == "3") && (this.form.txtGyoushaCd.Text != "") && (this.form.txtGenbaCd.Text != ""))
                    {
                        //印刷区分が「3.現場」の場合
                        this.dto.GyoushaCd = this.form.txtGyoushaCd.Text;
                        this.dto.GenbaCd = this.form.txtGenbaCd.Text;
                    }
                }
                DataTable dt = new DataTable();

                dt = GetAtenaData(this.dto);
                dt = setPrintDetail(dt, kobetsuList);

                ReportInfoR383 report_r383 = new ReportInfoR383();
                report_r383.R383_Report(dt);
                report_r383.Title = "宛名ラベル";

                // 印刷ポツプアップ画面表示
                using (var report = new FormReportPrintPopup(report_r383, "R383"))
                {
                    report.ShowDialog();
                    report.Dispose();
                }
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 閉じるボタンイベント

        /// <summary>
        /// 閉じるボタンイベント
        /// </summary>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //閉じる
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();

            //QN_QUAN add 20220216 #160470 S
            string caption = "";
            switch (this.form.dispType)
            {
                case 0:
                    caption = "請求宛名ラベル";
                    break;

                case 1:
                    caption = "精算宛名ラベル";
                    break;

                case 2:
                    caption = "マニフェスト宛名ラベル";
                    break;

            }
            this.InserDBLog(caption);
            //QN_QUAN add 20220216 #160470 E
            LogUtility.DebugMethodEnd(sender, e);
        }

        //QN_QUAN add 20220216 #160470 S
        private void InserDBLog(string caption)
        {
            DBConnectionLogLogic db = new DBConnectionLogLogic();
            if (!db.CanConnectDB() || !db.ConnectDB())
            {
                return;
            }
            db.InserDBLog(caption);
        }
        //QN_QUAN add 20220216 #160470 E

        #endregion

        #region リストから検索条件文字列への変換処理

        /// <summary>
        /// リストから検索条件文字列への変換処理
        /// </summary>
        private String listToString(List<string> list)
        {
            System.Text.StringBuilder sb = new StringBuilder();

            if (list.Count != 0)
            {
                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (sb.Length != 0)
                        {
                            sb.Append(",");
                        }
                        else
                        {
                            sb.Append("(");
                        }
                        sb.Append("'" + str + "'");
                    }
                }
                if (sb.Length != 0)
                {
                    sb.Append(")");
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// データ取得
        /// </summary>
        private DataTable GetAtenaData(DTOClass dto)
        {
            // 検索用データテーブル
            DataTable atenaDt = new DataTable();

            if (checkInput(dto))
            {
                //データ取得
                if (this.form.dispType == Const.UIConstans.SEIKYUU)
                {
                    //画面種別が「請求宛名ラベル」の場合
                    atenaDt = this.atenaDataDao.GetAtenaSeikyuuData(this.dto.printHouhou, this.dto.printKubun, this.dto.TorihikisakiCd, this.dto.GyoushaCd, this.dto.GenbaCd, this.dto.kobetsuShitei);
                }
                else if (this.form.dispType == Const.UIConstans.SEISAN)
                {
                    //画面種別が「支払宛名ラベル」の場合
                    atenaDt = this.atenaDataDao.GetAtenaShiharaiData(this.dto.printHouhou, this.dto.printKubun, this.dto.TorihikisakiCd, this.dto.GyoushaCd, this.dto.GenbaCd, this.dto.kobetsuShitei);
                }
                else if (this.form.dispType == Const.UIConstans.MANIFEST)
                {
                    //画面種別が「マニフェスト宛名ラベル」の場合
                    // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                    if (this.dto.printKubun != "3")
                    {
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                        atenaDt = this.atenaDataDao.GetAtenaManiData(this.dto.printHouhou, this.dto.printKubun, this.dto.TorihikisakiCd, this.dto.GyoushaCd, this.dto.GenbaCd, this.dto.kobetsuShitei);
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                    }
                    else
                    {
                        atenaDt = this.atenaDataDao.GetGenbaManiData(this.dto.printHouhou, this.dto.GyoushaCd, this.dto.GenbaCd, this.dto.kobetsuShitei, this.form.txtNum_HensousakiKbn.Text);
                    }
                    // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                }
            }
            return atenaDt;
        }

        #endregion

        #region 入力チェック

        /// <summary>
        /// 入力チェック
        /// </summary>
        private Boolean checkInput(DTOClass dto)
        {
            if (this.dto.TorihikisakiCd == ""
                && this.dto.GyoushaCd == ""
                && this.dto.GenbaCd == ""
                && this.dto.kobetsuShitei == "")
            {
                // 全未入力の場合
                return false;
            }
            else if (this.form.txtPrintKbn.Text == "3")
            {
                //印刷区分が「3.現場」の場合
                if (this.form.txtPrintHouhou.Text == "1")
                {
                    //印刷方法が「1.個別」の場合
                    if (this.dto.kobetsuShitei == ""
                        || this.dto.GyoushaCd == "")
                    {
                        //個別指定または業者CDが未入力の場合
                        return false;
                    }
                }
                else if (this.form.txtPrintHouhou.Text == "2")
                {
                    //印刷方法が「2.ページ」の場合
                    if (this.dto.GyoushaCd == ""
                        || this.dto.GenbaCd == "")
                    {
                        //業者CDまたは現場CDが未入力の場合
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 印刷編集処理

        /// <summary>
        /// 印刷編集処理
        /// </summary>
        private DataTable setPrintDetail(DataTable atenaDt, List<String> cdList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();

            System.Text.StringBuilder sBuilder;
            DataRow dr;
            dr = dt.NewRow();

            sBuilder = new StringBuilder();
            sBuilder.Append("\"");
            sBuilder.Append("1-1");

            if (this.form.txtPrintHouhou.Text == "1")
            {
                //印刷方法が「1.個別」の場合
                foreach (string cd in cdList)
                {
                    if (atenaDt.Rows.Count != 0 && !string.IsNullOrEmpty(cd))
                    {
                        // 検索結果から個別指定に当てはまるCDを抽出
                        DataRow[] rows = atenaDt.Select(string.Format("CD LIKE '%{0}%'", cd));
                        if (rows.Length != 0)
                        {
                            // データセット
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["POST"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["ADDRESS1"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["ADDRESS2"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["NAME1"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["KEISHOU1"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["NAME2"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["KEISHOU2"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["BUSHO"].ToString());
                            sBuilder.Append("\",\"");
                            sBuilder.Append(rows[0]["TANTOU"].ToString());
                            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                            sBuilder.Append("\",\"");
                            if (this.form.dispType == Const.UIConstans.MANIFEST && this.form.txtPrintKbn.Text == "3")
                            {
                                sBuilder.Append(rows[0]["HENSOUKBN_NAME"].ToString());
                            }
                            else
                            {
                                sBuilder.Append("");
                            }
                            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                        }
                        else
                        {
                            // 印刷用に空白をセット
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                            sBuilder.Append("\",\"");
                            sBuilder.Append("");
                            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                        }
                    }
                    else
                    {
                        // 印刷用に空白をセット
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                    }
                }
            }
            else if (this.form.txtPrintHouhou.Text == "2")
            {
                //印刷方法が「2.ページ」の場合
                for (int i = 0; i < Const.UIConstans.PRINTMAX; i++)
                {
                    if (atenaDt.Rows.Count != 0
                        && i >= int.Parse(this.form.cmbPrintKaishiIti.Text) - 1)
                    {
                        // 印刷方法が「2.ページ」の場合、印刷開始位置の指定位置まで空白をセット
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["POST"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["ADDRESS1"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["ADDRESS2"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["NAME1"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["KEISHOU1"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["NAME2"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["KEISHOU2"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["BUSHO"].ToString());
                        sBuilder.Append("\",\"");
                        sBuilder.Append(atenaDt.Rows[0]["TANTOU"].ToString());
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                        sBuilder.Append("\",\"");
                        if (this.form.dispType == Const.UIConstans.MANIFEST && this.form.txtPrintKbn.Text == "3")
                        {
                            sBuilder.Append(atenaDt.Rows[0]["HENSOUKBN_NAME"].ToString());
                        }
                        else
                        {
                            sBuilder.Append("");
                        }
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                    }
                    else
                    {
                        // 印刷用に空白をセット
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                        sBuilder.Append("\",\"");
                        sBuilder.Append("");
                        // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                    }
                }
            }
            sBuilder.Append("\"");

            dr[0] = sBuilder.ToString();
            dt.Rows.Add(dr);

            return dt;
        }

        #endregion

        #region 印刷区分更新後処理

        /// <summary>
        /// 印刷区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrintKbn_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                if (string.IsNullOrEmpty(this.form.txtPrintKbn.Text))
                {
                    if (this.form.ActiveControl != null)
                    {
                        if (this.form.ActiveControl.Name != "rdoTorihikisaki"
                        && this.form.ActiveControl.Name != "rdoGyousha"
                        && this.form.ActiveControl.Name != "rdoGenba")
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #endregion

        #region 取引先更新後処理

        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTorihikisakiCd_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                if (!this.CheckTorihikisakiCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region 業者更新後処理

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGyoushaCd_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                if (!this.CheckGyoushaCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                {
                    e.Cancel = true;
                }
                // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 start
                else
                {
                    if (this.form.txtPrintHouhou.Text == "2")
                    {
                        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        if (string.IsNullOrEmpty(this.form.txtGyoushaCd.Text) || this.form.previousGyousha != this.form.txtGyoushaCd.Text)
                        {
                            this.form.txtGenbaCd.Text = string.Empty;
                            this.form.txtGenbaName.Text = string.Empty;
                        }
                        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        this.form.radbtn_A.Enabled = false;
                        this.form.radbtn_B1.Enabled = false;
                        this.form.radbtn_B2.Enabled = false;
                        this.form.radbtn_B4.Enabled = false;
                        this.form.radbtn_B6.Enabled = false;
                        this.form.radbtn_C1.Enabled = false;
                        this.form.radbtn_C2.Enabled = false;
                        this.form.radbtn_D.Enabled = false;
                        this.form.radbtn_E.Enabled = false;
                        this.form.txtNum_HensousakiKbn.Enabled = false;
                        this.txtNum_HensousakiKbn_OldValue = "";
                        this.form.txtNum_HensousakiKbn.Text = "";
                    }
                    else if (this.form.txtPrintHouhou.Text == "1")
                    {
                        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        if (string.IsNullOrEmpty(this.form.txtGyoushaCd.Text) || this.form.previousGyousha != this.form.txtGyoushaCd.Text)
                        {
                            //個別設定の制御
                            this.form.txtKobetuShiteiCd1.Text = string.Empty;
                            this.form.txtKobetuShiteiCd2.Text = string.Empty;
                            this.form.txtKobetuShiteiCd3.Text = string.Empty;
                            this.form.txtKobetuShiteiCd4.Text = string.Empty;
                            this.form.txtKobetuShiteiCd5.Text = string.Empty;
                            this.form.txtKobetuShiteiCd6.Text = string.Empty;
                            this.form.txtKobetuShiteiCd7.Text = string.Empty;
                            this.form.txtKobetuShiteiCd8.Text = string.Empty;
                            this.form.txtKobetuShiteiCd9.Text = string.Empty;
                            this.form.txtKobetuShiteiCd10.Text = string.Empty;
                            this.form.txtKobetuShiteiCd11.Text = string.Empty;
                            this.form.txtKobetuShiteiCd12.Text = string.Empty;

                            this.form.txtKobetuShiteiName1.Text = string.Empty;
                            this.form.txtKobetuShiteiName2.Text = string.Empty;
                            this.form.txtKobetuShiteiName3.Text = string.Empty;
                            this.form.txtKobetuShiteiName4.Text = string.Empty;
                            this.form.txtKobetuShiteiName5.Text = string.Empty;
                            this.form.txtKobetuShiteiName6.Text = string.Empty;
                            this.form.txtKobetuShiteiName7.Text = string.Empty;
                            this.form.txtKobetuShiteiName8.Text = string.Empty;
                            this.form.txtKobetuShiteiName9.Text = string.Empty;
                            this.form.txtKobetuShiteiName10.Text = string.Empty;
                            this.form.txtKobetuShiteiName11.Text = string.Empty;
                            this.form.txtKobetuShiteiName12.Text = string.Empty;
                        }
                        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    }
                }
                // 20140703 ria EV005129 印刷方法「２．ページ」の時、業者CDをクリア後フォーカスアウトしても現場CDがクリアされない。 end
            }
        }

        #endregion

        #region 現場更新後処理

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGenbaCd_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                if (!this.CheckGenbaCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion

        #region 個別指定更新後処理

        /// <summary>
        /// 個別指定更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtKobetuShiteiCd_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel == false)
            {
                if (this.form.txtPrintKbn.Text == "1")
                {
                    if (!this.CheckTorihikisakiCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                    {
                        e.Cancel = true;
                    }
                }
                else if (this.form.txtPrintKbn.Text == "2")
                {
                    if (!this.CheckGyoushaCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                    {
                        e.Cancel = true;
                    }
                }
                else if (this.form.txtPrintKbn.Text == "3")
                {
                    if (!this.CheckGenbaCd((r_framework.CustomControl.CustomAlphaNumTextBox)sender))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #endregion

        #region 取引先CDの存在チェック

        /// <summary>
        /// 取引先CDの存在チェック
        /// </summary>
        private bool CheckTorihikisakiCd(r_framework.CustomControl.CustomAlphaNumTextBox ctrl)
        {
            if (ctrl.Text != "")
            {
                // 検索用データテーブル
                DataTable Dt = new DataTable();

                //取引先マスタデータ取得
                Dt = this.atenaDataDao.GetTorihikisakiCd(ctrl.Text, this.form.dispType.ToString());

                if (Dt.Rows.Count == 0)
                {
                    // 名称に空白をセット
                    allControlDict[ctrl].Text = "";
                    // 背景色を赤に変更
                    ctrl.IsInputErrorOccured = true;
                    // メッセージの表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058");

                    return false;
                }

                // No.3883-->
                if (this.form.dispType == Const.UIConstans.SEIKYUU || this.form.dispType == Const.UIConstans.SEISAN)
                {
                    var err = 0;
                    // 締日チェック
                    string sime = form.cmbShimebi.Items[form.cmbShimebi.SelectedIndex].ToString();
                    string sime1 = Dt.Rows[0]["SHIMEBI1"].ToString();
                    string sime2 = Dt.Rows[0]["SHIMEBI2"].ToString();
                    string sime3 = Dt.Rows[0]["SHIMEBI3"].ToString();
                    if (!sime.Equals(sime1) && !sime.Equals(sime2) && !sime.Equals(sime3))
                    {   // 締日が違う取引先
                        err = 1;
                    }

                    if (this.form.dispType == Const.UIConstans.SEIKYUU)
                    {   // 請求データチェック
                        Dt = this.atenaDataDao.GetSeikyuuDenpyou(ctrl.Text, this.form.selectFromDate.Text, this.form.selectToDate.Text);
                    }
                    else
                    {   // 精算データチェック
                        Dt = this.atenaDataDao.GetSeisanDenpyou(ctrl.Text, this.form.selectFromDate.Text, this.form.selectToDate.Text);
                    }
                    if (Dt.Rows.Count == 0)
                    {   // データが無い取引先
                        if (err == 1)
                        {   // 締日、データ両方エラー
                            err = 3;
                        }
                        else
                        {
                            err = 2;
                        }
                    }

                    if (err != 0)
                    {   // エラー
                        // 名称に空白をセット
                        allControlDict[ctrl].Text = "";
                        // 背景色を赤に変更
                        ctrl.IsInputErrorOccured = true;
                        // メッセージの表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        if (err == 1)
                        {
                            msgLogic.MessageBoxShow("E159", "");
                        }
                        else if (err == 2)
                        {
                            if (this.form.dispType == Const.UIConstans.SEIKYUU)
                            {
                                msgLogic.MessageBoxShow("E160", "請求");
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E160", "精算");
                            }
                        }
                        else
                        {   // エラーメッセージ２行表示
                            MessageUtility msguty = new MessageUtility();
                            string msg = msguty.GetMessage("E160").MESSAGE;
                            if (this.form.dispType == Const.UIConstans.SEIKYUU)
                            {
                                msg = msg.Replace("{0}", "請求");
                            }
                            else
                            {
                                msg = msg.Replace("{0}", "精算");
                            }
                            msgLogic.MessageBoxShow("E159", msg);
                        }
                        return false;
                    }
                }
                // No.3883<--
            }
            return true;
        }

        #endregion

        #region 業者CDの存在チェック

        /// <summary>
        /// 業者CDの存在チェック
        /// </summary>
        private bool CheckGyoushaCd(r_framework.CustomControl.CustomAlphaNumTextBox ctrl)
        {
            if (ctrl.Text != "")
            {
                // 検索用データテーブル
                DataTable Dt = new DataTable();

                var jouken = string.Empty;
                if(this.form.dispType.ToString() == "2")
                {
                    // 印刷区分が業者の時だけマニフェスト返送先区分をチェックします。
                    jouken = this.form.txtPrintKbn.Text;
                }

                //業者マスタデータ取得
                Dt = this.atenaDataDao.GetGyoshaCd(ctrl.Text, jouken);

                if (Dt.Rows.Count == 0)
                {
                    // 名称に空白をセット
                    allControlDict[ctrl].Text = "";
                    // 背景色を赤に変更
                    ctrl.IsInputErrorOccured = true;
                    // メッセージの表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058");

                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 現場CDの存在チェック

        /// <summary>
        /// 現場CDの存在チェック
        /// </summary>
        private bool CheckGenbaCd(r_framework.CustomControl.CustomAlphaNumTextBox ctrl)
        {
            if (ctrl.Text != "")
            {
                ctrl.Text = ctrl.Text.PadLeft(6, '0').ToUpper();
                if (this.form.txtGyoushaCd.Text == "")
                {
                    //業者CDがブランクの場合
                    this.form.txtGyoushaCd.Text = "";
                    this.form.txtGyoushaName.Text = "";

                    // メッセージの表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (this.form.dispType == Const.UIConstans.SEIKYUU)
                    {
                        msgLogic.MessageBoxShow("E051", "業者(請求先)CD");
                    }
                    else if (this.form.dispType == Const.UIConstans.SEISAN)
                    {
                        msgLogic.MessageBoxShow("E051", "業者(精算先)CD");
                    }
                    else if (this.form.dispType == Const.UIConstans.MANIFEST)
                    {
                        msgLogic.MessageBoxShow("E051", "業者(返送先)CD");
                    }

                    ctrl.Text = "";
                    allControlDict[ctrl].Text = "";
                    ctrl.Focus();

                    return true;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = ctrl.Text;
                keyEntity.GYOUSHA_CD = this.form.txtGyoushaCd.Text;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                var genbaEntityList = this.genbaDao.GetAllValidData(keyEntity);

                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    // 名称に空白をセット
                    allControlDict[ctrl].Text = "";
                    // 背景色を赤に変更
                    ctrl.IsInputErrorOccured = true;
                    // メッセージの表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    return false;
                }

                // 検索用データテーブル
                DataTable Dt = new DataTable();

                //現場マスタデータ取得
                Dt = this.atenaDataDao.GetGenbaCd(this.form.txtGyoushaCd.Text, ctrl.Text, this.form.dispType.ToString());

                if (Dt.Rows.Count == 0)
                {
                    // 名称に空白をセット
                    allControlDict[ctrl].Text = "";
                    // 背景色を赤に変更
                    ctrl.IsInputErrorOccured = true;
                    // メッセージの表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E058");

                    return false;
                }

                if (ctrl.Name != "txtGenbaCd")
                {
                    // 返送先区分チェックは個別指定CD入力時のみ
                    if ((this.form.dispType == Const.UIConstans.MANIFEST) &&
                       (!string.IsNullOrEmpty(this.form.txtNum_HensousakiKbn.Text)))
                    {
                        // 入力した現場CDが返送先区分に合致しているかチェック
                        string maniStr = string.Empty;
                        switch (this.form.txtNum_HensousakiKbn.Text)
                        {
                            case "1":
                                // A票
                                maniStr = "A";
                                break;

                            case "2":
                                // B1票
                                maniStr = "B1";
                                break;

                            case "3":
                                // B2票
                                maniStr = "B2";
                                break;

                            case "4":
                                // B4票
                                maniStr = "B4";
                                break;

                            case "5":
                                // B6票
                                maniStr = "B6";
                                break;

                            case "6":
                                // C1票
                                maniStr = "C1";
                                break;

                            case "7":
                                // C2票
                                maniStr = "C2";
                                break;

                            case "8":
                                // D票
                                maniStr = "D";
                                break;

                            case "9":
                                // E票
                                maniStr = "E";
                                break;
                        }
                        if ("2" == Dt.Rows[0]["MANI_HENSOUSAKI_USE_" + maniStr].ToString())
                        {
                            // 名称に空白をセット
                            allControlDict[ctrl].Text = "";
                            // 背景色を赤に変更
                            ctrl.IsInputErrorOccured = true;
                            // メッセージの表示
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "現場");

                            return false;
                        }
                    }
                }

                //現場マスタデータセット
                this.form.txtGyoushaCd.Text = Dt.Rows[0]["GYOUSHA_CD"].ToString();
                this.form.txtGyoushaName.Text = Dt.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                ctrl.Text = Dt.Rows[0]["GENBA_CD"].ToString();
                allControlDict[ctrl].Text = Dt.Rows[0]["GENBA_NAME_RYAKU"].ToString();

                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                if (this.form.txtPrintHouhou.Text == "2")
                {
                    string enable = "";
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_A"], this.form.radbtn_A, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B1"], this.form.radbtn_B1, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B2"], this.form.radbtn_B2, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B4"], this.form.radbtn_B4, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_B6"], this.form.radbtn_B6, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_C1"], this.form.radbtn_C1, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_C2"], this.form.radbtn_C2, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_D"], this.form.radbtn_D, ref enable);
                    this.setRadbtn(Dt.Rows[0]["MANI_HENSOUSAKI_USE_E"], this.form.radbtn_E, ref enable);
                    if (string.IsNullOrEmpty(enable))
                    {
                        //全ての返送先区分ラジオボタンが利用不可になった場合
                        this.txtNum_HensousakiKbn_OldValue = string.Empty;
                        this.form.txtNum_HensousakiKbn.Text = string.Empty;
                        this.form.txtNum_HensousakiKbn.Enabled = false;
                    }
                    else
                    {
                        //ある返送先区分ラジオボタンが利用可になった場合
                        //一番目の利用可ラジオボタンを選択する
                        this.txtNum_HensousakiKbn_OldValue = enable;
                        this.form.txtNum_HensousakiKbn.Text = enable;
                        this.form.txtNum_HensousakiKbn.Enabled = true;
                    }
                }
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }
            else
            {
                // 名称に空白をセット
                allControlDict[ctrl].Text = "";

                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                if (this.form.txtPrintHouhou.Text == "2")
                {
                    this.form.radbtn_A.Enabled = false;
                    this.form.radbtn_B1.Enabled = false;
                    this.form.radbtn_B2.Enabled = false;
                    this.form.radbtn_B4.Enabled = false;
                    this.form.radbtn_B6.Enabled = false;
                    this.form.radbtn_C1.Enabled = false;
                    this.form.radbtn_C2.Enabled = false;
                    this.form.radbtn_D.Enabled = false;
                    this.form.radbtn_E.Enabled = false;
                    this.form.txtNum_HensousakiKbn.Enabled = false;
                    this.txtNum_HensousakiKbn_OldValue = "";
                    this.form.txtNum_HensousakiKbn.Text = "";
                }
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }
            return true;
        }

        #endregion

        #region 画面表示初期化

        /// <summary>
        /// 画面表示初期化
        /// </summary>
        private void SetInitDisp()
        {
            //個別指定CDと名称のDictionaryを作成
            this.allControlDict = new Dictionary<r_framework.CustomControl.CustomAlphaNumTextBox, r_framework.CustomControl.CustomTextBox>();

            allControlDict.Add(this.form.txtTorihikisakiCd, this.form.txtTorihikisakiName);
            allControlDict.Add(this.form.txtGyoushaCd, this.form.txtGyoushaName);
            allControlDict.Add(this.form.txtGenbaCd, this.form.txtGenbaName);
            allControlDict.Add(this.form.txtKobetuShiteiCd1, this.form.txtKobetuShiteiName1);
            allControlDict.Add(this.form.txtKobetuShiteiCd2, this.form.txtKobetuShiteiName2);
            allControlDict.Add(this.form.txtKobetuShiteiCd3, this.form.txtKobetuShiteiName3);
            allControlDict.Add(this.form.txtKobetuShiteiCd4, this.form.txtKobetuShiteiName4);
            allControlDict.Add(this.form.txtKobetuShiteiCd5, this.form.txtKobetuShiteiName5);
            allControlDict.Add(this.form.txtKobetuShiteiCd6, this.form.txtKobetuShiteiName6);
            allControlDict.Add(this.form.txtKobetuShiteiCd7, this.form.txtKobetuShiteiName7);
            allControlDict.Add(this.form.txtKobetuShiteiCd8, this.form.txtKobetuShiteiName8);
            allControlDict.Add(this.form.txtKobetuShiteiCd9, this.form.txtKobetuShiteiName9);
            allControlDict.Add(this.form.txtKobetuShiteiCd10, this.form.txtKobetuShiteiName10);
            allControlDict.Add(this.form.txtKobetuShiteiCd11, this.form.txtKobetuShiteiName11);
            allControlDict.Add(this.form.txtKobetuShiteiCd12, this.form.txtKobetuShiteiName12);

            if (this.form.dispType == Const.UIConstans.SEIKYUU)
            {
                //現場検索時の「検索共通ポップアップ」でタイトルを参照しているため、変更の際は注意
                this.headerForm.lb_title.Text = "請求宛名ラベル　条件指定";
                this.form.lblTorihikisaki.Text = "取引先（請求先）";
                this.form.txtTorihikisakiCd.PopupSearchSendParams = this.form.txtSeikyuuTorihikisakiCd.PopupSearchSendParams;
                this.form.txtTorihikisakiCd.popupWindowSetting = this.form.txtSeikyuuTorihikisakiCd.popupWindowSetting;
                this.form.txtTorihikisakiCd.FocusOutCheckMethod = this.form.txtSeikyuuTorihikisakiCd.FocusOutCheckMethod;
                this.form.lblGyousha.Text = "業者（請求先）";
                this.form.txtGyoushaCd.PopupSearchSendParams = this.form.txtSeikyuuGyoushaCd.PopupSearchSendParams;
                this.form.txtGyoushaCd.popupWindowSetting = this.form.txtSeikyuuGyoushaCd.popupWindowSetting;
                this.form.txtGyoushaCd.FocusOutCheckMethod = this.form.txtSeikyuuGyoushaCd.FocusOutCheckMethod;
                this.form.lblGenba.Text = "現場（請求先）";
                this.form.txtGenbaCd.PopupSearchSendParams = this.form.txtSeikyuuGenbaCd.PopupSearchSendParams;
                this.form.txtGenbaCd.popupWindowSetting = this.form.txtSeikyuuGenbaCd.popupWindowSetting;
                this.form.txtGenbaCd.FocusOutCheckMethod = this.form.txtSeikyuuGenbaCd.FocusOutCheckMethod;

                this.form.lblSimebiKikan.Text = "請求締日※"; // No.3883
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                this.form.lbl_HensousakiKbn.Visible = false;
                this.form.customPanel4.Visible = false;
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }
            else if (this.form.dispType == Const.UIConstans.SEISAN)
            {
                //現場検索時の「検索共通ポップアップ」でタイトル名を参照しているため、変更の際は注意
                this.headerForm.lb_title.Text = "精算宛名ラベル　条件指定";
                this.form.lblTorihikisaki.Text = "取引先（精算先）";
                this.form.txtTorihikisakiCd.PopupSearchSendParams = this.form.txtSeisanTorihikisakiCd.PopupSearchSendParams;
                this.form.txtTorihikisakiCd.popupWindowSetting = this.form.txtSeisanTorihikisakiCd.popupWindowSetting;
                this.form.txtTorihikisakiCd.FocusOutCheckMethod = this.form.txtSeisanTorihikisakiCd.FocusOutCheckMethod;
                this.form.lblGyousha.Text = "業者（精算先）";
                this.form.txtGyoushaCd.PopupSearchSendParams = this.form.txtSeisanGyoushaCd.PopupSearchSendParams;
                this.form.txtGyoushaCd.popupWindowSetting = this.form.txtSeisanGyoushaCd.popupWindowSetting;
                this.form.txtGyoushaCd.FocusOutCheckMethod = this.form.txtSeisanGyoushaCd.FocusOutCheckMethod;
                this.form.lblGenba.Text = "現場（精算先）";
                this.form.txtGenbaCd.PopupSearchSendParams = this.form.txtSeisanGenbaCd.PopupSearchSendParams;
                this.form.txtGenbaCd.popupWindowSetting = this.form.txtSeisanGenbaCd.popupWindowSetting;
                this.form.txtGenbaCd.FocusOutCheckMethod = this.form.txtSeisanGenbaCd.FocusOutCheckMethod;

                this.form.lblSimebiKikan.Text = "精算締日※"; // No.3883
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                this.form.lbl_HensousakiKbn.Visible = false;
                this.form.customPanel4.Visible = false;
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }
            else if (this.form.dispType == Const.UIConstans.MANIFEST)
            {
                this.headerForm.lb_title.Text = "マニフェスト宛名ラベル　条件指定";
                this.form.lblTorihikisaki.Text = "取引先（返送先）";
                this.form.txtTorihikisakiCd.PopupSearchSendParams = this.form.txtManiTorihikisakiCd.PopupSearchSendParams;
                this.form.txtTorihikisakiCd.popupWindowSetting = this.form.txtManiTorihikisakiCd.popupWindowSetting;
                this.form.txtTorihikisakiCd.FocusOutCheckMethod = this.form.txtManiTorihikisakiCd.FocusOutCheckMethod;
                this.form.lblGyousha.Text = "業者（返送先）";
                this.form.txtGyoushaCd.PopupSearchSendParams = this.form.txtManiGyoushaCd.PopupSearchSendParams;
                this.form.txtGyoushaCd.popupWindowSetting = this.form.txtManiGyoushaCd.popupWindowSetting;
                this.form.txtGyoushaCd.FocusOutCheckMethod = this.form.txtManiGyoushaCd.FocusOutCheckMethod;
                this.form.lblGenba.Text = "現場（返送先）";
                this.form.txtGenbaCd.PopupSearchSendParams = this.form.txtManiGenbaCd.PopupSearchSendParams;
                this.form.txtGenbaCd.popupWindowSetting = this.form.txtManiGenbaCd.popupWindowSetting;
                this.form.txtGenbaCd.FocusOutCheckMethod = this.form.txtManiGenbaCd.FocusOutCheckMethod;

                // No.3883-->
                // マニフェストは締日関係非表示
                this.form.lblShimebi.Visible = false;
                this.form.cmbShimebi.Visible = false;
                this.form.lblSimebiKikan.Visible = false;
                this.form.dtpTaishoKikanFrom.Visible = false;
                this.form.lblFromTo.Visible = false;
                this.form.dtpTaishoKikanTo.Visible = false;
                this.parentForm.bt_func3.Text = "";
                this.parentForm.bt_func3.Enabled = false;
                this.parentForm.bt_func4.Text = "";
                this.parentForm.bt_func4.Enabled = false;
                // No.3883<--
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                this.form.lbl_HensousakiKbn.Visible = true;
                this.form.customPanel4.Visible = true;
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }
            // 画面表示初期化
            //ラジオボタン初期化
            this.form.txtPrintKbn.Text = "1";
            this.form.txtPrintHouhou.Text = "1";
            //コンボボックス初期化
            this.form.cmbPrintKaishiIti.Text = "1";

            //個別指定コントロール初期設定
            this.searchParamsChanged(true);

            // 締日初期化
            InitSetData(); // No.3883
        }

        #endregion

        // No.3883-->
        #region 締日初期化

        /// <summary>
        /// 締日初期データ設定処理
        /// </summary>
        private void InitSetData()
        {
            LogUtility.DebugMethodStart();

            DateTime dtToday = this.parentForm.sysDate; // 現在日付
            DateTime dtPrevMonth = dtToday.AddMonths(-1);
            DateTime dtEndDateOfMonth = new DateTime(dtToday.Year, dtToday.Month,   //月末日
                                        DateTime.DaysInMonth(dtToday.Year, dtToday.Month));

            //デフォルトで設定される締め日を求める。
            //締め日が5日単位で切り替わるので、5で割った商の値に
            //5を掛けることで締日を求める。
            //0の場合は、31を設定

            int iShimebi = 0;
            int iValue = dtToday.Day / 5;
            if (iValue == 0)
            {
                iShimebi = dtEndDateOfMonth.Day;
                form.cmbShimebi.SelectedIndex = 6;
            }
            else
            {
                iShimebi = iValue * 5;
                form.cmbShimebi.SelectedIndex = iValue;
            }

            bInitialize = false;

            //日付再設定処理
            ResetDate();

            LogUtility.DebugMethodEnd();
        }

        #endregion 締日初期化

        #region 締日変更時処理

        /// <summary>
        /// 締日変更時処理
        /// </summary>
        public void ChangeShimebiProcess()
        {
            LogUtility.DebugMethodStart();

            if (form.cmbShimebi.SelectedIndex == 0)
            {
                form.dtpTaishoKikanFrom.Enabled = true;
                form.dtpTaishoKikanTo.Enabled = true;
            }
            else
            {
                form.dtpTaishoKikanFrom.Enabled = false;
                form.dtpTaishoKikanTo.Enabled = false;
            }
            TorihikisakiClear();

            // 日付再設定処理
            ResetDate();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先クリア
        /// </summary>
        private void TorihikisakiClear()
        {
            form.txtTorihikisakiCd.Text = string.Empty;
            form.txtTorihikisakiName.Text = string.Empty;

            form.txtKobetuShiteiCd1.Text = string.Empty;
            form.txtKobetuShiteiName1.Text = string.Empty;
            form.txtKobetuShiteiCd2.Text = string.Empty;
            form.txtKobetuShiteiName2.Text = string.Empty;
            form.txtKobetuShiteiCd3.Text = string.Empty;
            form.txtKobetuShiteiName3.Text = string.Empty;
            form.txtKobetuShiteiCd4.Text = string.Empty;
            form.txtKobetuShiteiName4.Text = string.Empty;
            form.txtKobetuShiteiCd5.Text = string.Empty;
            form.txtKobetuShiteiName5.Text = string.Empty;
            form.txtKobetuShiteiCd6.Text = string.Empty;
            form.txtKobetuShiteiName6.Text = string.Empty;
            form.txtKobetuShiteiCd7.Text = string.Empty;
            form.txtKobetuShiteiName7.Text = string.Empty;
            form.txtKobetuShiteiCd8.Text = string.Empty;
            form.txtKobetuShiteiName8.Text = string.Empty;
            form.txtKobetuShiteiCd9.Text = string.Empty;
            form.txtKobetuShiteiName9.Text = string.Empty;
            form.txtKobetuShiteiCd10.Text = string.Empty;
            form.txtKobetuShiteiName10.Text = string.Empty;
            form.txtKobetuShiteiCd11.Text = string.Empty;
            form.txtKobetuShiteiName11.Text = string.Empty;
            form.txtKobetuShiteiCd12.Text = string.Empty;
            form.txtKobetuShiteiName12.Text = string.Empty;
        }

        /// <summary>
        /// 日付再設定処理
        /// </summary>
        private void ResetDate()
        {
            LogUtility.DebugMethodStart();

            if (form.cmbShimebi.SelectedIndex == 0)
            {
                this.form.lblFromTo.Visible = true;
                this.form.dtpTaishoKikanTo.Visible = true;
            }
            else
            {
                this.form.lblFromTo.Visible = false;
                this.form.dtpTaishoKikanTo.Visible = false;
            }

            if (!bInitialize)
            {
                DateTime dtToday = this.parentForm.sysDate; // 現在日付
                DateTime dtPrevMonth = dtToday.AddMonths(-1);
                DateTime dtPrevBackMonth = dtPrevMonth.AddMonths(-1);

                int iShimebi = int.Parse(form.cmbShimebi.Items[form.cmbShimebi.SelectedIndex].ToString());

                //対象期間-FROM,対象期間-TOの設定
                if (iShimebi == 0)
                {
                    form.dtpTaishoKikanFrom.Value = new DateTime(dtToday.Year, dtToday.Month, dtToday.Day);
                    form.dtpTaishoKikanTo.Value = new DateTime(dtToday.Year, dtToday.Month, dtToday.Day);

                    // 検索期間設定
                    form.selectFromDate.Value = form.dtpTaishoKikanFrom.Value;
                    form.selectToDate.Value = form.dtpTaishoKikanTo.Value;
                }
                else if (iShimebi == 31)
                {
                    //前月の末日を取得(今月1日の一日前)
                    form.dtpTaishoKikanFrom.Value = new DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1);

                    // 検索期間設定
                    form.selectFromDate.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, 1);
                    form.selectToDate.Value = form.dtpTaishoKikanFrom.Value;
                }
                else if (iShimebi >= dtToday.Day)
                {   // 締日が本日より後ろの場合前月
                    form.dtpTaishoKikanFrom.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, iShimebi);

                    // 検索期間設定
                    form.selectFromDate.Value = new DateTime(dtPrevBackMonth.Year, dtPrevBackMonth.Month, iShimebi + 1);
                    form.selectToDate.Value = form.dtpTaishoKikanFrom.Value;
                }
                else
                {
                    form.dtpTaishoKikanFrom.Value = new DateTime(dtToday.Year, dtToday.Month, iShimebi);

                    // 検索期間設定
                    form.selectFromDate.Value = new DateTime(dtPrevMonth.Year, dtPrevMonth.Month, iShimebi + 1);
                    form.selectToDate.Value = form.dtpTaishoKikanFrom.Value;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion 締日変更時処理

        #region 検索期間変更

        /// <summary>
        /// 検索期間先頭日変更処理
        /// </summary>
        public void TaishoKikanFrom_TextChanged()
        {
            if (form.cmbShimebi.SelectedIndex == 0)
            {
                string taishoKikanFrom = string.Empty;
                string selectFromDate = string.Empty;

                if (this.form.dtpTaishoKikanFrom.Value != null)
                {
                    taishoKikanFrom = this.form.dtpTaishoKikanFrom.Value.ToString();
                }

                if (this.form.selectFromDate.Value != null)
                {
                    selectFromDate = this.form.selectFromDate.Value.ToString();
                }

                if (!taishoKikanFrom.Equals(selectFromDate))
                {
                    this.form.selectFromDate.Value = this.form.dtpTaishoKikanFrom.Value;
                    TorihikisakiClear();
                }
            }
        }

        /// <summary>
        /// 検索期間終了日変更処理
        /// </summary>
        public void TaishoKikanTo_TextChanged()
        {
            if (form.cmbShimebi.SelectedIndex == 0)
            {
                string taishoKikanTo = string.Empty;
                string selectToDate = string.Empty;

                if (this.form.dtpTaishoKikanTo.Value != null)
                {
                    taishoKikanTo = this.form.dtpTaishoKikanTo.Value.ToString();
                }

                if (this.form.selectToDate.Value != null)
                {
                    selectToDate = this.form.selectToDate.Value.ToString();
                }

                if (!taishoKikanTo.Equals(selectToDate))
                {
                    this.form.selectToDate.Value = this.form.dtpTaishoKikanTo.Value;
                    TorihikisakiClear();
                }
            }
        }

        #endregion 検索期間変更
        // No.3883<--

        #region

        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
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

        #endregion

        // 20141226 Houkakou 「宛名ラベル」のダブルクリックを追加する start
        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTaishoKikanTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtpTaishoKikanFrom;
            var ToTextBox = this.form.dtpTaishoKikanTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion
        // 20141226 Houkakou 「宛名ラベル」のダブルクリックを追加する end

        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 業者前回値を取得処理

        /// <summary>
        /// 業者前回値を取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGyoushaCd_Enter(object sender, EventArgs e)
        {
            this.form.previousGyousha = this.form.txtGyoushaCd.Text;
        }

        #endregion
        // 20150917 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        /// <summary>
        /// 前月/翌月ボタンを押下時に、画面に設定している業者情報をクリアする
        /// 移動した月に請求が無いとエラーになるため。
        /// </summary>
        private void SentakuClear()
        {
            form.txtTorihikisakiCd.Text = string.Empty;
            form.txtTorihikisakiName.Text = string.Empty;
            form.txtGyoushaCd.Text = string.Empty;
            form.txtGyoushaName.Text = string.Empty;
            form.txtGenbaCd.Text = string.Empty;
            form.txtGenbaName.Text = string.Empty;

            form.txtKobetuShiteiCd1.Text = string.Empty;
            form.txtKobetuShiteiName1.Text = string.Empty;
            form.txtKobetuShiteiCd2.Text = string.Empty;
            form.txtKobetuShiteiName2.Text = string.Empty;
            form.txtKobetuShiteiCd3.Text = string.Empty;
            form.txtKobetuShiteiName3.Text = string.Empty;
            form.txtKobetuShiteiCd4.Text = string.Empty;
            form.txtKobetuShiteiName4.Text = string.Empty;
            form.txtKobetuShiteiCd5.Text = string.Empty;
            form.txtKobetuShiteiName5.Text = string.Empty;
            form.txtKobetuShiteiCd6.Text = string.Empty;
            form.txtKobetuShiteiName6.Text = string.Empty;
            form.txtKobetuShiteiCd7.Text = string.Empty;
            form.txtKobetuShiteiName7.Text = string.Empty;
            form.txtKobetuShiteiCd8.Text = string.Empty;
            form.txtKobetuShiteiName8.Text = string.Empty;
            form.txtKobetuShiteiCd9.Text = string.Empty;
            form.txtKobetuShiteiName9.Text = string.Empty;
            form.txtKobetuShiteiCd10.Text = string.Empty;
            form.txtKobetuShiteiName10.Text = string.Empty;
            form.txtKobetuShiteiCd11.Text = string.Empty;
            form.txtKobetuShiteiName11.Text = string.Empty;
            form.txtKobetuShiteiCd12.Text = string.Empty;
            form.txtKobetuShiteiName12.Text = string.Empty;
        }
    }
}