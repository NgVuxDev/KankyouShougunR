// $Id: UIForm.cs 7176 2013-11-15 09:23:47Z sys_dev_27 $
using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Const;
using r_framework.Setting;
using System.Reflection;
using System.Data;
using GrapeCity.Win.MultiRow;
using r_framework.Logic;
using Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Entity;
using System.Collections.Generic;
using System.ComponentModel;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.APP
{

    /// <summary>
    /// G275 営業予実管理表
    /// </summary>
    public partial class F18_G275UIForm : SuperForm
    {
        #region fields
        /// <summary>
        /// メッセージロジック
        /// </summary>
        private readonly MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private F18Logic logic;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private F18_G275UIHeaderForm head;

        /// <summary>
        /// 自社期首月
        /// </summary>
        private int kishuMonth;

        /// <summary>
        /// 請求情報金額端数CD
        /// </summary>
        private int seikyuuKingakuHasuuCD;

        /// <summary>
        /// 現在日の年度
        /// </summary>
        private int thisNendo;

        /// <summary>
        /// 1年度の月配列
        /// </summary>
        private int[] months = new int[F18_G275ConstCls.COUNT_MONTH];

        /// <summary>
        /// 年度配列
        /// </summary>
        private int[] years = new int[F18_G275ConstCls.COUNT_YEAR];

        /// <summary>
        /// データなし判別フラグ
        /// </summary>
        private bool noData = true;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public F18_G275UIForm()
            : base(WINDOW_ID.T_YOJITSU_KANRIHYOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            this.logic = new F18Logic(this);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面読み込み処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            try
            {

                this.head = (F18_G275UIHeaderForm)(((BusinessBaseForm)this.Parent).headerForm);

                this.logic.parentForm = (BusinessBaseForm)this.Parent;

                // マスタデータ取得
                if (!this.getMasterData()) { return; }

                // ボタン初期化処理
                if (!this.ButtonInit()) { return; }
                // ボタンイベントの初期化処理
                if (!this.ButtonEventInit()) { return; }


                // 検索条件の初期化
                if (!this.initFormConditons()) { return; }
                // 明細部初期化
                if (!this.initFormDetail()) { return; }

				// Anchorの設定は必ずOnLoadで行うこと
                if (this.grdIchiran != null)
                {
                    int GRID_HEIGHT_MIN_VALUE = 379;
                    int GRID_WIDTH_MIN_VALUE = 1158;
                    int h = this.Height - 52;
                    int w = this.Width;

                    if (h < GRID_HEIGHT_MIN_VALUE)
                    {
                        this.grdIchiran.Height = GRID_HEIGHT_MIN_VALUE;
                    }
                    else
                    {
                        this.grdIchiran.Height = h;
                    }
                    if (w < GRID_WIDTH_MIN_VALUE)
                    {
                        this.grdIchiran.Width = GRID_WIDTH_MIN_VALUE;
                    }
                    else
                    {
                        this.grdIchiran.Width = w;
                    }

                    if (this.grdIchiran.Height <= GRID_HEIGHT_MIN_VALUE
                        || this.grdIchiran.Width <= GRID_WIDTH_MIN_VALUE)
                    {
                        this.grdIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    }
                    else
                    {
                        this.grdIchiran.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        /// <summary>
        /// マスタデータ関連処理
        /// </summary>
        private bool getMasterData()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                // １．年同算出
                // 現在年よりの年度を算出
                this.kishuMonth = this.logic.getSelfCorpKishuMonth();
                this.thisNendo = this.getNendo(this.logic.parentForm.sysDate);

                // アラート件数(マスタ値)
                this.head.alertNumber.Text = this.logic.getIchiranAlertKensuu().ToString();
                this.seikyuuKingakuHasuuCD = this.logic.getSeikyuuKingakuHasuuCD();
            }
            catch (Exception ex)
            {
                LogUtility.Error("getMasterData", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 指定日付より年度を算出
        /// </summary>
        /// <param name="p_day">指定日付</param>
        /// <returns>年度</returns>
        private int getNendo(DateTime p_day)
        {
            LogUtility.DebugMethodStart(p_day);

            DateTime nendoStart =
                new DateTime(p_day.Year, this.kishuMonth, 1, 0, 0, 0, 0);

            int ret = 0;

            if (p_day.CompareTo(nendoStart) < 0)
            {
                // 指定日は算出年度開始日以前の場合
                ret = p_day.Year - 1;
            }
            else
            {
                // その他場合
                ret = p_day.Year;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 条件部各項目を初期化
        /// </summary>
        private bool initFormConditons()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 年度初期化(現在年度)
                this.head.tb_nendo.Value = this.thisNendo.ToString();
                //// 年選択部品を現在日付に設定
                //this.head.dt_nendo.Value = DateTime.Today;

                // パターン初期化(形式 = [2:月次])
                this.head.txtKeisiki.Text = F18_G275ConstCls.KEISHIKI_GETUJI;

                // 読込データ件数(0)
                this.head.ReadDataNumber.Text = F18_G275ConstCls.INIT_RESULT_COUNT;

                // 部署(ブランク)
                this.tb_busho_cd.Text = F18_G275ConstCls.BLANK;
                this.tb_busho_name.Text = F18_G275ConstCls.BLANK;

                // 伝票区分（１．売上）
                this.DENPYOU_KBN_CD.Text = F18_G275ConstCls.DENPYOU_KBN_URIAGE;
            }
            catch (Exception ex)
            {
                LogUtility.Error("initFormConditons", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        #region ボタン関連初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private bool ButtonInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // ボタン設定の読込
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                ButtonSetting[] buttonSettingArray = buttonSetting.LoadButtonSetting(thisAssembly, F18_G275ConstCls.BUTTON_INFO_XML_PATH);
                var parentForm = (BusinessBaseForm)this.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSettingArray, parentForm, this.WindowType);

                // 必要ない項目を無効化
                parentForm.bt_process1.Enabled = false;
                parentForm.bt_process1.Visible = false;
                parentForm.bt_process2.Enabled = false;
                parentForm.bt_process2.Visible = false;
                parentForm.bt_process3.Enabled = false;
                parentForm.bt_process3.Visible = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process4.Visible = false;
                parentForm.bt_process5.Enabled = false;
                parentForm.bt_process5.Visible = false;
                parentForm.txb_process.Enabled = false;
                parentForm.txb_process.Visible = false;
                parentForm.lb_process.Visible = false;
                parentForm.ProcessButtonPanel.Enabled = false;
                parentForm.ProcessButtonPanel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタンイベントの初期化処理
        /// </summary>
        /// <returns></returns>
        private bool ButtonEventInit()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;

                //(F6 CSV出力)イベント生成
                parentForm.bt_func6.Click -= new EventHandler(this.CsvOutput);
                parentForm.bt_func6.Click += new EventHandler(this.CsvOutput);

                //(F7 条件ｸﾘｱ)イベント生成
                parentForm.bt_func7.Click -= new EventHandler(this.ResetConditions);
                parentForm.bt_func7.Click += new EventHandler(this.ResetConditions);

                //(F8 検索)イベント生成
                parentForm.bt_func8.Click -= new EventHandler(this.Search);
                parentForm.bt_func8.Click += new EventHandler(this.Search);

                //(F12 閉じる)イベント生成
                parentForm.bt_func12.Click -= new EventHandler(this.FormClose);
                parentForm.bt_func12.Click += new EventHandler(this.FormClose);

                // 日付選択イベント設定
                //this.head.dt_nendo.CloseUp -= new EventHandler(this.dt_nendo_CloseUp);
                //this.head.dt_nendo.CloseUp += new EventHandler(this.dt_nendo_CloseUp);

                // 日付入力イベント（年度からfocusが失う場合）
                //this.head.tb_nendo.Leave -= new EventHandler(this.tb_nendo_Leave);
                //this.head.tb_nendo.Leave += new EventHandler(this.tb_nendo_Leave);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonEventInit", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        #endregion
        #endregion

        #region 画面制御
        /// <summary>
        /// 一覧部を初期化
        /// </summary>
        private bool initFormDetail()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // 現在の年度より、月次の一覧タイトルを設定
                this.setGetujiHead();
                // 月次用のテンプレートを表示 ※ヘッダを編集してから、Templateに設定
                this.grdIchiran.Template = this.detailItilanGetsuji;
                // 月次の一覧を初期化(表示は空行１行)
                this.grdIchiran.Rows.Clear();
                this.grdGetsuji.Rows.Clear();// データ構造体クリア
                this.grdIchiran.Rows.Add();
                this.noData = true;

                // ボタン状態更新
                if (!this.refleshButtonStatus())
                {
                    throw new Exception("");
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("initFormDetail", ex);
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 月一覧ヘッダ部の月項目の表示設定
        /// </summary>
        private void setGetujiHead()
        {
            LogUtility.DebugMethodStart();

            DateTime nendoStart = new DateTime(this.logic.parentForm.sysDate.Year, this.kishuMonth, 1, 0, 0, 0, 0);

            for (int i = 0; i < F18_G275ConstCls.COUNT_MONTH; i++)
            {
                // 月次一覧ヘッダ部の月項目の表示文字設定
                this.months[i] = nendoStart.AddMonths(i).Month;
                string tmpHeadValue = this.months[i].ToString() + F18_G275ConstCls.GETUJI_HEADER_VALUE_END_STR;
                this.detailItilanGetsuji.ColumnHeaders[0].Cells[F18_G275ConstCls.GETUJI_HEADER_NAME_START_STR + (i + 1)].Value = tmpHeadValue;
                this.grdGetsuji.Columns[F18_G275ConstCls.GETUJI_HEADER_NAME_START_STR + (i + 1)].HeaderText = tmpHeadValue;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン状態更新
        /// </summary>
        private bool refleshButtonStatus()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;

                //if (this.noData)
                //{
                //    // 一覧データなし時、[F6]CSV出力を無効化
                //    parentForm.bt_func6.Enabled = false;
                //}
                //else
                //{
                //    // 一覧データ存在時、[F6]CSV出力を有効化
                //    parentForm.bt_func6.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                LogUtility.Error("refleshButtonStatus", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 年度一覧ヘッダ部の年度項目の表示設定
        /// </summary>
        /// <param name="p_targetNendo">対象年度</param>
        private bool setNendoHead(int p_targetNendo)
        {
            LogUtility.DebugMethodStart(p_targetNendo);
            bool ret = true;
            try
            {
                DateTime nendoStart = new DateTime(p_targetNendo, this.kishuMonth, 1, 0, 0, 0, 0);

                for (int j = 0; j < F18_G275ConstCls.COUNT_YEAR; j++)
                {
                    // 年度一覧ヘッダ部の年度項目の表示文字設定
                    this.years[j] = nendoStart.AddYears(j - F18_G275ConstCls.COUNT_YEAR + 1).Year;
                    string tmpHeadValue = this.years[j].ToString() + F18_G275ConstCls.NENDO_HEADER_VALUE_END_STR;
                    this.detailItilanNendo.ColumnHeaders[0].Cells[F18_G275ConstCls.NENDO_HEADER_NAME_START_STR + (j + 1)].Value = tmpHeadValue;
                    this.grdNendo.Columns[F18_G275ConstCls.NENDO_HEADER_NAME_START_STR + (j + 1)].HeaderText = tmpHeadValue;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setNendoHead", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (ex is SQLRuntimeException)
                {
                    msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region イベント処理
        ///// <summary>
        ///// 日付選択イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dt_nendo_CloseUp(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    // 選択した日付より年度設定
        //    this.head.tb_nendo.Text = this.getNendo(DateTime.Parse(this.head.dt_nendo.Text)).ToString();

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// 年度からfucosが失うイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tb_nendo_Leave(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    // 入力した日付より選択部品の時間を変更
        //    String nendo = this.head.tb_nendo.Text;
        //    string tmpNendo = this.getNendo(DateTime.Parse(this.head.dt_nendo.Text)).ToString();
        //    if (nendo != null && !"".Equals(nendo) && !tmpNendo.Equals(nendo))
        //    {
        //        // 別の年度を入力した場合
        //        this.head.dt_nendo.Text = new DateTime(int.Parse(nendo), this.kishuMonth, 1, 0, 0, 0).ToString();
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CsvOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //base.OnLoad(e);

                if (this.noData)
                {
                    // CSV出力データなし
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_NO_OUTPUT_DATA);
                    return;
                }

                if (DialogResult.Yes.Equals((new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_OUTPUT_COMFIRM)))
                {
                    if (this.detailItilanNendo.Equals(this.grdIchiran.Template))
                    {
                        // 形式は年度の場合
                        // 2014/01/24 oonaka delete CSVファイル名が不正 start
                        //this.logic.outputCSV(this.grdNendo, F18_G275ConstCls.CSV_DIALOG_INIT_FILE_NAME);
                        // 2014/01/24 oonaka delete CSVファイル名が不正 end
                        // 2014/01/24 oonaka add CSVファイル名が不正 start
                        this.logic.outputCSV(this.grdNendo, this.WindowId);
                        // 2014/01/24 oonaka add CSVファイル名が不正 end
                    }
                    else
                    {
                        // 形式は月次の場合
                        // 2014/01/24 oonaka add CSVファイル名が不正 start
                        //this.logic.outputCSV(this.grdGetsuji, F18_G275ConstCls.CSV_DIALOG_INIT_FILE_NAME);
                        // 2014/01/24 oonaka add CSVファイル名が不正 end
                        // 2014/01/24 oonaka add CSVファイル名が不正 start
                        this.logic.outputCSV(this.grdGetsuji, this.WindowId);
                        // 2014/01/24 oonaka add CSVファイル名が不正 end
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
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ResetConditions(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //base.OnLoad(e);

            try
            {
                // 表示クリア
                if (!this.initFormConditons()) { return; }
                if (!this.initFormDetail()) { return; }

                // 条件の部署にfucos移動
                this.tb_busho_cd.Focus();
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
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                //base.OnLoad(e);

                // ・必須入力チェック
                // 年度
                if (this.head.tb_nendo.Value == null || "".Equals(this.head.tb_nendo.Text.Trim()))
                {
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_REQUIRED, F18_G275ConstCls.MSG_PARAM_REQUIRED_NENDO);
                    return;
                }
                // 出力形式
                if (this.head.txtKeisiki.Text == null || "".Equals(this.head.txtKeisiki.Text.Trim()))
                {
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_REQUIRED, F18_G275ConstCls.MSG_PARAM_REQUIRED_OUTPUT_STYLE);
                    return;
                }
                // アラート件数
                if (this.head.alertNumber.Text == null || "".Equals(this.head.alertNumber.Text.Trim()))
                {
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_REQUIRED, F18_G275ConstCls.MSG_PARAM_REQUIRED_ALERT_COUNT);
                    return;
                }
                // No2661-->
                // 部署
                if (this.tb_busho_cd.Text == null || "".Equals(this.tb_busho_cd.Text.Trim()))
                {
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_REQUIRED, F18_G275ConstCls.MSG_PARAM_REQUIRED_BUSHO);
                    return;
                }
                string busho_cd = string.Empty;
                if (!this.tb_busho_cd.Text.Equals("999"))
                {
                    busho_cd = this.tb_busho_cd.Text;
                }
                // No2661<--
                if (this.DENPYOU_KBN_CD.Text == null || "".Equals(this.DENPYOU_KBN_CD.Text.Trim()))
                {
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_REQUIRED, F18_G275ConstCls.MSG_PARAM_REQUIRED_DENPYOU_KBN);
                    return;
                }

                // ・検索
                object[] searchResult = null;
                bool catchErr = false;
                if (F18_G275ConstCls.KEISHIKI_NENDO.Equals(this.head.txtKeisiki.Text))
                {
                    // 形式は年度の場合
                    // 検索
                    searchResult = this.logic.searchNendoData(this.head.tb_nendo.Text, this.kishuMonth, busho_cd, this.DENPYOU_KBN_CD.Text, out catchErr); // No2661
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    // 形式は月次の場合
                    // 検索
                    searchResult = this.logic.searchGetujiData(this.head.tb_nendo.Text, this.kishuMonth, busho_cd, this.DENPYOU_KBN_CD.Text, out catchErr); // No2661
                    if (catchErr)
                    {
                        return;
                    }
                }

                // ・結果チェック
                if (searchResult == null || searchResult.Length == 0)
                {

                    // ・データクリア
                    this.grdIchiran.Rows.Clear();
                    this.grdNendo.Rows.Clear();// データ構造体クリア
                    this.grdGetsuji.Rows.Clear();// データ構造体クリア
                    // 結果なしの場合
                    this.noData = true;

                    this.head.ReadDataNumber.Text = F18_G275ConstCls.INIT_RESULT_COUNT;

                    if (F18_G275ConstCls.KEISHIKI_NENDO.Equals(this.head.txtKeisiki.Text))
                    {
                        // 形式は年度の場合
                        if (!this.setNendoHead(int.Parse(this.head.tb_nendo.Text))) { return; }
                        this.grdIchiran.Template = this.detailItilanNendo;
                    }
                    else
                    {
                        // 形式は月次の場合
                        this.grdIchiran.Template = this.detailItilanGetsuji;
                    }

                    // 空行追加
                    this.grdIchiran.Rows.Add();

                    // エラーメッセージ表示
                    (new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_NO_DATA);
                }
                else
                {
                    int alertCount = 0;
                    if (!string.IsNullOrEmpty(this.head.alertNumber.Text))
                    {
                        alertCount = int.Parse(this.head.alertNumber.Text.Replace(",", ""));
                    }
                    if (searchResult.Length > alertCount && alertCount != 0)
                    {
                        // 結果件数 > アラート件数　の場合
                        if (DialogResult.Yes.Equals((new MessageBoxShowLogic()).MessageBoxShow(F18_G275ConstCls.MSG_ID_OVER_ALERT_COUNT)))
                        {
                            // 確認ダイアログのyesを押下時
                            if (!this.setDetailData(searchResult)) { return; }
                        }
                    }
                    else
                    {
                        // 結果件数 <= アラート件数　の場合
                        if (!this.setDetailData(searchResult)) { return; }

                    }
                }

                // ・ボタン状態更新
                if (!this.refleshButtonStatus()) { return; }
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
        /// 検索結果を画面に設定
        /// </summary>
        /// <param name="p_resuleData">検索結果</param>
        private bool setDetailData(object[] p_resuleData)
        {
            LogUtility.DebugMethodStart(p_resuleData);
            bool ret = true;
            try
            {
                if (F18_G275ConstCls.KEISHIKI_NENDO.Equals(this.head.txtKeisiki.Text))
                {
                    // 形式は年度の場合
                    this.setNendoDetail((NendoInfo[])p_resuleData);
                }
                else
                {
                    // 形式は月次の場合
                    this.setGetujiDetail((GetujiInfo[])p_resuleData);
                }

                // 結果ありs
                this.noData = false;
                this.head.ReadDataNumber.Text = p_resuleData.Length.ToString("###,###");
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("setDetailData", ex);
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (ex is SQLRuntimeException)
                    {
                        msgLogic.MessageBoxShow("E093", "");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E245", "");
                    }
                }
                ret = false;
            }

            //// 完了メッセージ
            //this.msgLogic.MessageBoxShow(F18_G275ConstCls.I_MSG_ID_PROCESS_FINISHED, F18_G275ConstCls.I_MSG_PARAM_SELECT_FINISHED);

            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 年度の一覧を設定
        /// </summary>
        /// <param name="p_nendoInfo">年度検索結果</param>
        private void setNendoDetail(NendoInfo[] p_nendoInfo)
        {
            LogUtility.DebugMethodStart(p_nendoInfo);

            // 年度形式表示
            if (!this.setNendoHead(int.Parse(this.head.tb_nendo.Text)))
            {
                throw new Exception("");
            }
            this.grdIchiran.Template = this.detailItilanNendo;
            this.grdIchiran.IsBrowsePurpose = false;

            // データクリア
            this.grdIchiran.Rows.Clear();
            this.grdNendo.Rows.Clear();// データ構造体クリア
            this.grdIchiran.Rows.Add(p_nendoInfo.Length);
            this.grdNendo.Rows.Add(p_nendoInfo.Length * 3);

            for (int i = 0; i < p_nendoInfo.Length; i++)
            {
                // 表示用データ取得
                string BUSHO_CD = p_nendoInfo[i].BUSHO_CD;
                string BUSHO_NAME = p_nendoInfo[i].BUSHO_NAME;
                string SHAIN_CD = p_nendoInfo[i].SHAIN_CD;
                string SHAIN_NAME = p_nendoInfo[i].SHAIN_NAME;
                string YOSAN_1 = p_nendoInfo[i].YOSAN_1.ToString("n0");
                string YOSAN_2 = p_nendoInfo[i].YOSAN_2.ToString("n0");
                string YOSAN_3 = p_nendoInfo[i].YOSAN_3.ToString("n0");
                string YOSAN_4 = p_nendoInfo[i].YOSAN_4.ToString("n0");
                string YOSAN_5 = p_nendoInfo[i].YOSAN_5.ToString("n0");
                string YOSAN_6 = p_nendoInfo[i].YOSAN_6.ToString("n0");
                string YOSAN_7 = p_nendoInfo[i].YOSAN_7.ToString("n0");
                string YOSAN_8 = p_nendoInfo[i].YOSAN_8.ToString("n0");
                string YOSAN_9 = p_nendoInfo[i].YOSAN_9.ToString("n0");
                string YOSAN_GOUKEI = p_nendoInfo[i].YOSAN_GOUKEI.ToString("n0");
                string JISSEKI_1 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_1);
                string JISSEKI_2 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_2);
                string JISSEKI_3 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_3);
                string JISSEKI_4 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_4);
                string JISSEKI_5 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_5);
                string JISSEKI_6 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_6);
                string JISSEKI_7 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_7);
                string JISSEKI_8 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_8);
                string JISSEKI_9 = this.editYenToYen1000(p_nendoInfo[i].JISSEKI_9);

                // 端数処理をした状態で合計値を再計算
                p_nendoInfo[i].JISSEKI_GOUKEI = Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_1)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_2)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_3)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_4)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_5)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_6)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_7)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_8)) +
                                                Convert.ToDecimal(this.editYenToYen1000(p_nendoInfo[i].JISSEKI_9));

                string JISSEKI_GOUKEI = this.editYenToYen(p_nendoInfo[i].JISSEKI_GOUKEI);
                string TASSEI_RITSU_1 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_1);
                string TASSEI_RITSU_2 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_2);
                string TASSEI_RITSU_3 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_3);
                string TASSEI_RITSU_4 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_4);
                string TASSEI_RITSU_5 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_5);
                string TASSEI_RITSU_6 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_6);
                string TASSEI_RITSU_7 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_7);
                string TASSEI_RITSU_8 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_8);
                string TASSEI_RITSU_9 = this.editTasseritu(p_nendoInfo[i].TASSEI_RITSU_9);
                string TASSEI_GOKEI = this.editTasseritu(p_nendoInfo[i].TASSEI_GOKEI);

                // 画面表示データ設定
                // 予算
                this.grdIchiran.Rows[i].Cells["BUSHO_CD"].Value = BUSHO_CD;
                this.grdIchiran.Rows[i].Cells["BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdIchiran.Rows[i].Cells["SHAIN_CD"].Value = SHAIN_CD;
                this.grdIchiran.Rows[i].Cells["SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdIchiran.Rows[i].Cells["YOSAN_1"].Value = YOSAN_1;
                this.grdIchiran.Rows[i].Cells["YOSAN_2"].Value = YOSAN_2;
                this.grdIchiran.Rows[i].Cells["YOSAN_3"].Value = YOSAN_3;
                this.grdIchiran.Rows[i].Cells["YOSAN_4"].Value = YOSAN_4;
                this.grdIchiran.Rows[i].Cells["YOSAN_5"].Value = YOSAN_5;
                this.grdIchiran.Rows[i].Cells["YOSAN_6"].Value = YOSAN_6;
                this.grdIchiran.Rows[i].Cells["YOSAN_7"].Value = YOSAN_7;
                this.grdIchiran.Rows[i].Cells["YOSAN_8"].Value = YOSAN_8;
                this.grdIchiran.Rows[i].Cells["YOSAN_9"].Value = YOSAN_9;
                this.grdIchiran.Rows[i].Cells["YOSAN_GOUKEI"].Value = YOSAN_GOUKEI;
                // 実績
                this.grdIchiran.Rows[i].Cells["JISSEKI_1"].Value = JISSEKI_1;
                this.grdIchiran.Rows[i].Cells["JISSEKI_2"].Value = JISSEKI_2;
                this.grdIchiran.Rows[i].Cells["JISSEKI_3"].Value = JISSEKI_3;
                this.grdIchiran.Rows[i].Cells["JISSEKI_4"].Value = JISSEKI_4;
                this.grdIchiran.Rows[i].Cells["JISSEKI_5"].Value = JISSEKI_5;
                this.grdIchiran.Rows[i].Cells["JISSEKI_6"].Value = JISSEKI_6;
                this.grdIchiran.Rows[i].Cells["JISSEKI_7"].Value = JISSEKI_7;
                this.grdIchiran.Rows[i].Cells["JISSEKI_8"].Value = JISSEKI_8;
                this.grdIchiran.Rows[i].Cells["JISSEKI_9"].Value = JISSEKI_9;
                this.grdIchiran.Rows[i].Cells["JISSEKI_GOUKEI"].Value = JISSEKI_GOUKEI;
                // 達成率
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_1"].Value = TASSEI_RITSU_1;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_2"].Value = TASSEI_RITSU_2;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_3"].Value = TASSEI_RITSU_3;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_4"].Value = TASSEI_RITSU_4;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_5"].Value = TASSEI_RITSU_5;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_6"].Value = TASSEI_RITSU_6;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_7"].Value = TASSEI_RITSU_7;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_8"].Value = TASSEI_RITSU_8;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_9"].Value = TASSEI_RITSU_9;
                this.grdIchiran.Rows[i].Cells["TASSEI_GOKEI"].Value = TASSEI_GOKEI;

                // CSV出力データ設定(画面の一行はCSVの3行として出力)
                // 予算
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_BUSHO_CD"].Value = BUSHO_CD;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_SHAIN_CD"].Value = SHAIN_CD;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_JYOUKYOU"].Value = "予算";
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_1"].Value = YOSAN_1;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_2"].Value = YOSAN_2;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_3"].Value = YOSAN_3;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_4"].Value = YOSAN_4;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_5"].Value = YOSAN_5;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_6"].Value = YOSAN_6;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_7"].Value = YOSAN_7;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_8"].Value = YOSAN_8;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_9"].Value = YOSAN_9;
                this.grdNendo.Rows[i * 3 + 0].Cells["NENDO_GOUKEI"].Value = YOSAN_GOUKEI;
                // 実績
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_BUSHO_CD"].Value = BUSHO_CD;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_SHAIN_CD"].Value = SHAIN_CD;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_JYOUKYOU"].Value = "実績";
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_1"].Value = JISSEKI_1;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_2"].Value = JISSEKI_2;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_3"].Value = JISSEKI_3;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_4"].Value = JISSEKI_4;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_5"].Value = JISSEKI_5;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_6"].Value = JISSEKI_6;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_7"].Value = JISSEKI_7;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_8"].Value = JISSEKI_8;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_9"].Value = JISSEKI_9;
                this.grdNendo.Rows[i * 3 + 1].Cells["NENDO_GOUKEI"].Value = JISSEKI_GOUKEI;
                // 達成率
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_BUSHO_CD"].Value = BUSHO_CD;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_SHAIN_CD"].Value = SHAIN_CD;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_JYOUKYOU"].Value = "達成率(%)";
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_1"].Value = TASSEI_RITSU_1;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_2"].Value = TASSEI_RITSU_2;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_3"].Value = TASSEI_RITSU_3;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_4"].Value = TASSEI_RITSU_4;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_5"].Value = TASSEI_RITSU_5;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_6"].Value = TASSEI_RITSU_6;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_7"].Value = TASSEI_RITSU_7;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_8"].Value = TASSEI_RITSU_8;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_9"].Value = TASSEI_RITSU_9;
                this.grdNendo.Rows[i * 3 + 2].Cells["NENDO_GOUKEI"].Value = TASSEI_GOKEI;

            }
            this.grdIchiran.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 月次の一覧を設定
        /// </summary>
        /// <param name="p_getujiInfo">月次検索結果</param>
        private void setGetujiDetail(GetujiInfo[] p_getujiInfo)
        {
            LogUtility.DebugMethodStart(p_getujiInfo);

            // 月次形式表示
            this.grdIchiran.Template = this.detailItilanGetsuji;
            this.grdIchiran.IsBrowsePurpose = false;

            // データクリア
            this.grdIchiran.Rows.Clear();
            this.grdGetsuji.Rows.Clear();// データ構造体クリア
            this.grdIchiran.Rows.Add(p_getujiInfo.Length);
            this.grdGetsuji.Rows.Add(p_getujiInfo.Length * 3);

            // 設定される側の項目を期首月に応じて並び替える
            string[] MonthArray = new string[12];
            int stertMonth = 1;

            if (this.kishuMonth != 1)
            {
                stertMonth = 14 - this.kishuMonth;
            }

            DateTime nendoStart = new DateTime(this.logic.parentForm.sysDate.Year, stertMonth, 1, 0, 0, 0, 0);
            for (int i = 0; i < F18_G275ConstCls.COUNT_MONTH; i++)
            {
                // 月次一覧ヘッダ部の月項目の表示文字設定
                MonthArray[i] = "YOSAN_" + nendoStart.AddMonths(i).Month.ToString();
            }

            for (int i = 0; i < p_getujiInfo.Length; i++)
            {
                // 表示用データ取得
                string BUSHO_CD = p_getujiInfo[i].BUSHO_CD;
                string BUSHO_NAME = p_getujiInfo[i].BUSHO_NAME;
                string SHAIN_CD = p_getujiInfo[i].SHAIN_CD;
                string SHAIN_NAME = p_getujiInfo[i].SHAIN_NAME;
                string YOSAN_1 = p_getujiInfo[i].YOSAN_1.ToString("n0");
                string YOSAN_2 = p_getujiInfo[i].YOSAN_2.ToString("n0");
                string YOSAN_3 = p_getujiInfo[i].YOSAN_3.ToString("n0");
                string YOSAN_4 = p_getujiInfo[i].YOSAN_4.ToString("n0");
                string YOSAN_5 = p_getujiInfo[i].YOSAN_5.ToString("n0");
                string YOSAN_6 = p_getujiInfo[i].YOSAN_6.ToString("n0");
                string YOSAN_7 = p_getujiInfo[i].YOSAN_7.ToString("n0");
                string YOSAN_8 = p_getujiInfo[i].YOSAN_8.ToString("n0");
                string YOSAN_9 = p_getujiInfo[i].YOSAN_9.ToString("n0");
                string YOSAN_10 = p_getujiInfo[i].YOSAN_10.ToString("n0");
                string YOSAN_11 = p_getujiInfo[i].YOSAN_11.ToString("n0");
                string YOSAN_12 = p_getujiInfo[i].YOSAN_12.ToString("n0");
                string YOSAN_GOUKEI = p_getujiInfo[i].YOSAN_GOUKEI.ToString("n0");
                string JISSEKI_1 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_1);
                string JISSEKI_2 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_2);
                string JISSEKI_3 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_3);
                string JISSEKI_4 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_4);
                string JISSEKI_5 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_5);
                string JISSEKI_6 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_6);
                string JISSEKI_7 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_7);
                string JISSEKI_8 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_8);
                string JISSEKI_9 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_9);
                string JISSEKI_10 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_10);
                string JISSEKI_11 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_11);
                string JISSEKI_12 = this.editYenToYen1000(p_getujiInfo[i].JISSEKI_12);

                // 端数処理をした状態で合計値を再計算
                p_getujiInfo[i].JISSEKI_GOUKEI = Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_1)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_2)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_3)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_4)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_5)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_6)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_7)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_8)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_9)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_10)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_11)) +
                                                 Convert.ToDecimal(this.editYenToYen1000(p_getujiInfo[i].JISSEKI_12));

                string JISSEKI_GOUKEI = this.editYenToYen(p_getujiInfo[i].JISSEKI_GOUKEI);
                string TASSEI_RITSU_1 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_1);
                string TASSEI_RITSU_2 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_2);
                string TASSEI_RITSU_3 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_3);
                string TASSEI_RITSU_4 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_4);
                string TASSEI_RITSU_5 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_5);
                string TASSEI_RITSU_6 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_6);
                string TASSEI_RITSU_7 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_7);
                string TASSEI_RITSU_8 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_8);
                string TASSEI_RITSU_9 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_9);
                string TASSEI_RITSU_10 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_10);
                string TASSEI_RITSU_11 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_11);
                string TASSEI_RITSU_12 = this.editTasseritu(p_getujiInfo[i].TASSEI_RITSU_12);
                string TASSEI_GOKEI = this.editTasseritu(p_getujiInfo[i].TASSEI_GOKEI);

                // 画面表示データ設定
                // 予算
                this.grdIchiran.Rows[i].Cells["BUSHO_CD"].Value = BUSHO_CD;
                this.grdIchiran.Rows[i].Cells["BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdIchiran.Rows[i].Cells["SHAIN_CD"].Value = SHAIN_CD;
                this.grdIchiran.Rows[i].Cells["SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdIchiran.Rows[i].Cells[MonthArray[0]].Value = YOSAN_1;
                this.grdIchiran.Rows[i].Cells[MonthArray[1]].Value = YOSAN_2;
                this.grdIchiran.Rows[i].Cells[MonthArray[2]].Value = YOSAN_3;
                this.grdIchiran.Rows[i].Cells[MonthArray[3]].Value = YOSAN_4;
                this.grdIchiran.Rows[i].Cells[MonthArray[4]].Value = YOSAN_5;
                this.grdIchiran.Rows[i].Cells[MonthArray[5]].Value = YOSAN_6;
                this.grdIchiran.Rows[i].Cells[MonthArray[6]].Value = YOSAN_7;
                this.grdIchiran.Rows[i].Cells[MonthArray[7]].Value = YOSAN_8;
                this.grdIchiran.Rows[i].Cells[MonthArray[8]].Value = YOSAN_9;
                this.grdIchiran.Rows[i].Cells[MonthArray[9]].Value = YOSAN_10;
                this.grdIchiran.Rows[i].Cells[MonthArray[10]].Value = YOSAN_11;
                this.grdIchiran.Rows[i].Cells[MonthArray[11]].Value = YOSAN_12;
                this.grdIchiran.Rows[i].Cells["YOSAN_GOUKEI"].Value = YOSAN_GOUKEI;
                // 実績
                this.grdIchiran.Rows[i].Cells["JISSEKI_1"].Value = JISSEKI_1;
                this.grdIchiran.Rows[i].Cells["JISSEKI_2"].Value = JISSEKI_2;
                this.grdIchiran.Rows[i].Cells["JISSEKI_3"].Value = JISSEKI_3;
                this.grdIchiran.Rows[i].Cells["JISSEKI_4"].Value = JISSEKI_4;
                this.grdIchiran.Rows[i].Cells["JISSEKI_5"].Value = JISSEKI_5;
                this.grdIchiran.Rows[i].Cells["JISSEKI_6"].Value = JISSEKI_6;
                this.grdIchiran.Rows[i].Cells["JISSEKI_7"].Value = JISSEKI_7;
                this.grdIchiran.Rows[i].Cells["JISSEKI_8"].Value = JISSEKI_8;
                this.grdIchiran.Rows[i].Cells["JISSEKI_9"].Value = JISSEKI_9;
                this.grdIchiran.Rows[i].Cells["JISSEKI_10"].Value = JISSEKI_10;
                this.grdIchiran.Rows[i].Cells["JISSEKI_11"].Value = JISSEKI_11;
                this.grdIchiran.Rows[i].Cells["JISSEKI_12"].Value = JISSEKI_12;
                this.grdIchiran.Rows[i].Cells["JISSEKI_GOUKEI"].Value = JISSEKI_GOUKEI;
                // 達成率
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_1"].Value = TASSEI_RITSU_1;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_2"].Value = TASSEI_RITSU_2;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_3"].Value = TASSEI_RITSU_3;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_4"].Value = TASSEI_RITSU_4;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_5"].Value = TASSEI_RITSU_5;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_6"].Value = TASSEI_RITSU_6;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_7"].Value = TASSEI_RITSU_7;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_8"].Value = TASSEI_RITSU_8;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_9"].Value = TASSEI_RITSU_9;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_10"].Value = TASSEI_RITSU_10;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_11"].Value = TASSEI_RITSU_11;
                this.grdIchiran.Rows[i].Cells["TASSEI_RITSU_12"].Value = TASSEI_RITSU_12;
                this.grdIchiran.Rows[i].Cells["TASSEI_GOKEI"].Value = TASSEI_GOKEI;

                // CSV出力データ設定(画面の一行はCSVの3行として出力)
                // 予算
                this.grdGetsuji.Rows[i * 3 + 0].Cells["BUSHO_CD"].Value = BUSHO_CD;
                this.grdGetsuji.Rows[i * 3 + 0].Cells["BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdGetsuji.Rows[i * 3 + 0].Cells["SHAIN_CD"].Value = SHAIN_CD;
                this.grdGetsuji.Rows[i * 3 + 0].Cells["SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdGetsuji.Rows[i * 3 + 0].Cells["JYOUKYOU"].Value = "予算";
                for (int j = 0; j < F18_G275ConstCls.COUNT_MONTH; j++)
                {
                    string month = "MONTH_" + (j + 1);
                    switch (this.months[j])
                    {
                        case 1:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_1;
                            break;
                        case 2:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_2;
                            break;
                        case 3:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_3;
                            break;
                        case 4:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_4;
                            break;
                        case 5:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_5;
                            break;
                        case 6:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_6;
                            break;
                        case 7:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_7;
                            break;
                        case 8:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_8;
                            break;
                        case 9:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_9;
                            break;
                        case 10:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_10;
                            break;
                        case 11:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_11;
                            break;
                        case 12:
                            this.grdGetsuji.Rows[i * 3 + 0].Cells[month].Value = YOSAN_12;
                            break;
                    }
                }
                this.grdGetsuji.Rows[i * 3 + 0].Cells["GOUKEI"].Value = YOSAN_GOUKEI;
                // 実績
                this.grdGetsuji.Rows[i * 3 + 1].Cells["BUSHO_CD"].Value = BUSHO_CD;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["SHAIN_CD"].Value = SHAIN_CD;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["JYOUKYOU"].Value = "実績";
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_1"].Value = JISSEKI_1;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_2"].Value = JISSEKI_2;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_3"].Value = JISSEKI_3;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_4"].Value = JISSEKI_4;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_5"].Value = JISSEKI_5;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_6"].Value = JISSEKI_6;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_7"].Value = JISSEKI_7;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_8"].Value = JISSEKI_8;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_9"].Value = JISSEKI_9;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_10"].Value = JISSEKI_10;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_11"].Value = JISSEKI_11;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["MONTH_12"].Value = JISSEKI_12;
                this.grdGetsuji.Rows[i * 3 + 1].Cells["GOUKEI"].Value = JISSEKI_GOUKEI;
                // 達成率
                this.grdGetsuji.Rows[i * 3 + 2].Cells["BUSHO_CD"].Value = BUSHO_CD;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["BUSHO_NAME"].Value = BUSHO_NAME;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["SHAIN_CD"].Value = SHAIN_CD;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["SHAIN_NAME"].Value = SHAIN_NAME;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["JYOUKYOU"].Value = "達成率(%)";
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_1"].Value = TASSEI_RITSU_1;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_2"].Value = TASSEI_RITSU_2;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_3"].Value = TASSEI_RITSU_3;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_4"].Value = TASSEI_RITSU_4;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_5"].Value = TASSEI_RITSU_5;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_6"].Value = TASSEI_RITSU_6;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_7"].Value = TASSEI_RITSU_7;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_8"].Value = TASSEI_RITSU_8;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_9"].Value = TASSEI_RITSU_9;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_10"].Value = TASSEI_RITSU_10;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_11"].Value = TASSEI_RITSU_11;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["MONTH_12"].Value = TASSEI_RITSU_12;
                this.grdGetsuji.Rows[i * 3 + 2].Cells["GOUKEI"].Value = TASSEI_GOKEI;

            }
            this.grdIchiran.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示用金額取得(端数処理のみ)
        /// </summary>
        /// <param name="p_yen">千円</param>
        /// <returns>千円</returns>
        internal string editYenToYen(decimal p_yen)
        {
            string yen1000 = "0";

            decimal result = 0;
            decimal sign = 1;
            if (p_yen < 0) { sign = -1; }

            p_yen = Math.Abs(p_yen);

            switch (this.seikyuuKingakuHasuuCD)
            {
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_1:
                    // 切り上げ
                    result = Math.Ceiling(p_yen);
                    break;
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_2:
                    // 切り捨て
                    result = Math.Floor(p_yen);
                    break;
                default:
                    // 四捨五入
                    result = Math.Round(p_yen, 0, MidpointRounding.AwayFromZero);
                    break;
            }

            yen1000 = (result * sign).ToString("n0");

            return yen1000;
        }

        /// <summary>
        /// 表示用金額取得(円を1000円単位に編集)
        /// </summary>
        /// <param name="p_yen">円</param>
        /// <returns>千円</returns>
        internal string editYenToYen1000(decimal p_yen)
        {
            // 当functionはデータ件数の倍数毎、重複呼出され、ログ出力すると動作は大変遅くなる
            //LogUtility.DebugMethodStart(p_yen);

            string yen1000 = "0";

            decimal result = 0;
            decimal sign = 1;
            if (p_yen < 0) { sign = -1; }

            p_yen = Math.Abs(p_yen / 1000m);

            switch (this.seikyuuKingakuHasuuCD)
            {
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_1:
                    // 切り上げ
                    result = Math.Ceiling(p_yen);
                    break;
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_2:
                    // 切り捨て
                    result = Math.Floor(p_yen);
                    break;
                default:
                    // 四捨五入
                    result = Math.Round(p_yen, 0, MidpointRounding.AwayFromZero);
                    break;
            }

            yen1000 = (result * sign).ToString("n0");

            //LogUtility.DebugMethodEnd(yen1000);
            return yen1000;
        }

        /// <summary>
        /// 表示用達成率取得
        /// </summary>
        /// <param name="p_tasseritu">達成率</param>
        /// <returns>表示用達成率</returns>
        private string editTasseritu(decimal p_tasseritu)
        {
            // 当functionはデータ件数の倍数毎、重複呼出され、ログ出力すると動作は大変遅くなる
            //LogUtility.DebugMethodStart(p_tasseritu);

            string ret = "0.00";

            decimal result = 0;
            decimal sign = 1;
            if (p_tasseritu < 0) { sign = -1; }

            p_tasseritu = Math.Abs(p_tasseritu * 100m);

            switch (this.seikyuuKingakuHasuuCD)
            {
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_1:
                    // 切り上げ
                    result = Math.Ceiling(p_tasseritu);
                    break;
                case F18_G275ConstCls.SEIKYUU_KINGAKU_HASUU_CD_2:
                    // 切り捨て
                    result = Math.Floor(p_tasseritu);
                    break;
                default:
                    // 四捨五入
                    result = Math.Round(p_tasseritu, 0, MidpointRounding.AwayFromZero);
                    break;
            }

            ret = ((result * sign) / 100m).ToString("n");

            //LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                this.Close();
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

        /// <summary>
        /// 部署検索popupのデータ(適用期間外の部署も含む)を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_busho_cd_Enter(object sender, EventArgs e)
        {
            try
            {
                bool catchErr = false;
                // データソース
                this.tb_busho_cd.PopupDataSource = this.logic.getPopupBusyoInfo(out catchErr);
                if (catchErr)
                {
                    return;
                }

                // 列名
                this.tb_busho_cd.PopupDataHeaderTitle = new string[] { "部署CD", "部署名" };
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

    }
}
