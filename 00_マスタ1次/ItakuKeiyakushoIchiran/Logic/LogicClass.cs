// $Id: LogicClass.cs 46576 2015-04-06 04:18:28Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ItakuKeiyakushoIchiran.APP;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using MasterKyoutsuPopup2.APP;
using System.Data;
using Seasar.Framework.Exceptions;

namespace ItakuKeiyakushoIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "ItakuKeiyakushoIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面連携に使用するキー取得項目名1
        /// </summary>
        internal readonly string KEY_ID1 = "HIDDEN_SYSTEM_ID";
        internal readonly string KEY_ID11 = "HIDDEN_ITAKU_KEIYAKU_TOUROKU_HOUHOU";

        /// <summary>
        /// 各種明細の枝番項目名
        /// </summary>
        internal readonly string KEY_ID2 = "HIDDEN_SEQ1";
        internal readonly string KEY_ID3 = "HIDDEN_SEQ2";
        internal readonly string KEY_ID4 = "HIDDEN_SEQ3";
        internal readonly string KEY_ID5 = "HIDDEN_SEQ4";
        internal readonly string KEY_ID6 = "HIDDEN_SEQ5";
        internal readonly string KEY_ID7 = "HIDDEN_SEQ6";
        internal readonly string KEY_ID8 = "HIDDEN_SEQ7";
        internal readonly string KEY_ID9 = "HIDDEN_SEQ8";
        internal readonly string KEY_ID10 = "HIDDEN_SEQ9";

        /// <summary>
        /// 委託契約種類のカラム名
        /// </summary>
        private const string ITAKU_KEIYAKU_SHURUI = "委託契約書種類";

        /// <summary>
        /// 委託契約種類のカラム名
        /// </summary>
        private const string ITAKU_KEIYAKU_STATUS = "委託契約ステータス";

        /// <summary>
        /// 委託契約登録方法のカラム名
        /// </summary>
        private const string ITAKU_KEIYAKU_TOUROKU_HOUHOU = "登録方法";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private ItakuKeiyakushoIchiranForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private IchiranHeaderForm2 headerForm;

        private M_SYS_INFO sysinfoEntity;
        private M_KYOTEN kyotenEntity;

        /// <summary>
        /// 委託契約基本のDao
        /// </summary>
        private IM_ITAKU_KEIYAKU_KIHONDao daoItaku;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// IM_SHOBUN_HOUHOUDao
        /// </summary>
        IM_SHOBUN_HOUHOUDao daoShobunHouhou;

        /// <summary>
        /// IM_HOUKOKUSHO_BUNRUIDao
        /// </summary>
        IM_HOUKOKUSHO_BUNRUIDao daoHoukokushoBunrui;

        /// <summary>
        /// IM_SHAINDao
        /// </summary>
        IM_SHAINDao daoShain;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        IM_GENBADao daoGenba;

        /// <summary>
        /// 電子契約送付先のDao
        /// </summary>
        DenshiKeiyakuSouhusakiDAO daoDenshiSouhusaki;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal IchiranBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(ItakuKeiyakushoIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.headerForm = (IchiranHeaderForm2)((IchiranBaseForm)targetForm.ParentForm).headerForm;
            this.daoItaku = DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoHoukokushoBunrui = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.daoGenba = DaoInitUtility.GetComponent<IM_GENBADao>();

            this.daoShobunHouhou = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();

            this.daoDenshiSouhusaki = DaoInitUtility.GetComponent<DenshiKeiyakuSouhusakiDAO>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (IchiranBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)
                //this.form.customDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro; //ヘッダの背景色
                //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;  //ヘッダの文字色
                this.form.customDataGridView1.Size = new Size(997, 220);
                this.form.customDataGridView1.Location = new Point(5, 230);
                this.form.customDataGridView1.TabStop = false;

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //this.form.DATE_FROM.Value = DateTime.Now;
                //this.form.DATE_TO.Value = DateTime.Now;
                this.form.DATE_FROM.Value = this.parentForm.sysDate;
                this.form.DATE_TO.Value = this.parentForm.sysDate;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                //システム設定情報読み込み
                this.GetSysInfo();
                // ヘッダーの初期化
                this.InitHeaderArea();

                // 表示条件初期化
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked) //&& !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }

                if (AppConfig.IsManiLite)
                {
                    // マニライト版(C8)の初期化処理
                    ManiLiteInit();
                }

                // 電子契約オプションがfalseの場合、サブファンクションの2,3は非表示/押下不可とする。
                if (!AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // テキストクリア
                    this.parentForm.bt_process2.Text = "";
                    this.parentForm.bt_process3.Text = "";

                    // 非活性
                    this.parentForm.bt_process2.Enabled = false;
                    this.parentForm.bt_process3.Enabled = false;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            //// ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (IchiranBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            //Functionボタンのイベント生成
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);            //表示切替
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);            //新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);            //修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);            //削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);            //複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);          //フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
            // 電子契約送信ボタン(process2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);
            // 電子契約履歴ボタン(process3)イベント生成
            parentForm.bt_process3.Click += new EventHandler(this.bt_process3_Click);

            //明細のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            this.form.customDataGridView1.KeyDown += new KeyEventHandler(this.DetailKeyDown);
            this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DetailCellFormatting);

            // 20141201 teikyou ダブルクリックを追加する　start
            //this.form.DATE_TO.MouseDoubleClick += new MouseEventHandler(DATE_TO_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

            this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
            this.form.GENBA_CD.Enter += new EventHandler(this.form.GENBA_CD_Enter);
            this.form.UNPANGYOUSHA_CD.Enter += new EventHandler(this.form.UNPANGYOUSHA_CD_Enter);
            this.form.UNPAN_TSUMIKAE_CD.Enter += new EventHandler(this.form.UNPAN_TSUMIKAE_CD_Enter);
            this.form.TSUMIKAEHOKAN_CD.Enter += new EventHandler(this.form.TSUMIKAEHOKAN_CD_Enter);
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Enter += new EventHandler(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD_Enter);
            this.form.SHOBUN_GENBA_CD.Enter += new EventHandler(this.form.SHOBUN_GENBA_CD_Enter);
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Enter += new EventHandler(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD_Enter);
            this.form.SAISHUU_SHOBUNJOU_CD.Enter += new EventHandler(this.form.SAISHUU_SHOBUNJOU_CD_Enter);
            this.form.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GYOUSHA_CD_Validating);
            this.form.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GENBA_CD_Validating);
            this.form.UNPANGYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.UNPANGYOUSHA_CD_Validating);
            this.form.UNPAN_TSUMIKAE_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.UNPAN_TSUMIKAE_CD_Validating);
            this.form.TSUMIKAEHOKAN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.TSUMIKAEHOKAN_CD_Validating);
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD_Validating);
            this.form.SHOBUN_GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_GENBA_CD_Validating);
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD_Validating);
            this.form.SAISHUU_SHOBUNJOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SAISHUU_SHOBUNJOU_CD_Validating);
            this.form.SHOBUN_HOUHOU_SHOBUN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_HOUHOU_SHOBUN_CD_Validating);
            this.form.SHOBUN_HOUHOU_SAISHU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.SHOBUN_HOUHOU_SAISHU_CD_Validating);
            this.form.HOUKOKUSHO_BUNRUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.HOUKOKUSHO_BUNRUI_CD_Validating);
            this.form.EIGYOU_TANTOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.EIGYOU_TANTOU_CD_Validating);

            LogUtility.DebugMethodEnd();
        }



        #endregion


        #region Functionボタン 押下処理

        /// <summary>
        /// F1 表示切替
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG,false,true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.DELETE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            CSVExport csvLogic = new CSVExport();
            DENSHU_KBN id = this.form.DenshuKbn;
            csvLogic.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, id.ToTitleString(), this.form);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 検索条件初期化
            this.form.beforGyousaCD = string.Empty;
            this.form.beforeGenbaCD = string.Empty;
            this.form.GYOUSHA_CD.Clear();
            this.form.GYOUSHA_NAME_RYAKU.Clear();
            this.form.GENBA_CD.Clear();
            this.form.GENBA_NAME_RYAKU.Clear();

            this.form.DATE_SELECT.Text = "8";

            this.form.KEIYAKUSHO_SHURUI_CD.Clear();
            this.form.KEIYAKUSHO_SHURUI_NAME.Clear();

            this.form.KEIYAKU_JYOUKYOU_CD.Clear();
            this.form.KEIYAKU_JYOUKYOU_NAME.Clear();

            this.form.beforeUnpanGyousaCD = string.Empty;
            this.form.UNPANGYOUSHA_CD.Clear();
            this.form.UNPANGYOUSHA_NAME.Clear();

            this.form.beforeShobunJyutakushaShobunCD = string.Empty;
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Clear();
            this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Clear();

            this.form.beforeShobunGenbaCD = string.Empty;
            this.form.SHOBUN_GENBA_CD.Clear();
            this.form.SHOBUN_GENBA_NAME_RYAKU.Clear();

            this.form.beforeShobunJyutakushaSaishuCD = string.Empty;
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Clear();
            this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Clear();

            this.form.beforeSaishuShobunCD = string.Empty;
            this.form.SAISHUU_SHOBUNJOU_CD.Clear();
            this.form.SAISHUU_SHOBUNJOU_NAME.Clear();

            this.form.beforeUnpanTsumikaeCD = string.Empty;
            this.form.UNPAN_TSUMIKAE_CD.Clear();
            this.form.UNPAN_TSUMIKAE_NAME.Clear();

            this.form.beforeTsumikaehokanCD = string.Empty;
            this.form.TSUMIKAEHOKAN_CD.Clear();
            this.form.TSUMIKAEHOKAN_NAME.Clear();

            this.form.beforeShobunHouhouShobunCD = string.Empty;
            this.form.SHOBUN_HOUHOU_SHOBUN_CD.Clear();
            this.form.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Clear();

            this.form.beforeShobunHouhouSaishuCD = string.Empty;
            this.form.SHOBUN_HOUHOU_SAISHU_CD.Clear();
            this.form.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Clear();

            this.form.beforeHoukokushoBunruiCD = string.Empty;
            this.form.HOUKOKUSHO_BUNRUI_CD.Clear();
            this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Clear();

            this.form.beforeEigyouTantouCD = string.Empty;
            this.form.EIGYOU_TANTOU_CD.Clear();
            this.form.EIGYOU_TANTOU_NAME_RYAKU.Clear();

            //開始
            this.form.DATE_FROM.Clear();
            //終了
            this.form.DATE_TO.Clear();
            this.form.ITAKU_KEIYAKU_NO.Clear();

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.DATE_SELECT.Text) && this.form.DATE_SELECT.Text != "8")
                {
                    if (string.IsNullOrWhiteSpace(this.form.DATE_FROM.Text) && string.IsNullOrWhiteSpace(this.form.DATE_TO.Text))
                    {
                        MessageBoxUtility.MessageBoxShow("E001", "日付選択");
                        return;
                    }
                }
                else if (string.IsNullOrEmpty(this.form.DATE_SELECT.Text))
                {
                    MessageBoxUtility.MessageBoxShow("E001", "日付選択");
                    return;
                }

                // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 start
                if (this.CheckDate())
                {
                    return;
                }
                // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 end

                //読込データ件数を取得
                this.headerForm.ReadDataNumber.Text = this.Search().ToString();
                if (this.form.customDataGridView1 != null)
                {
                    this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headerForm.ReadDataNumber.Text = "0";
                }
                if (this.headerForm.ReadDataNumber.Text == "0")
                {
                    MessageBoxUtility.MessageBoxShow("C001");
                }

                this.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
            if (this.form.customDataGridView1 != null)
            {
                this.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headerForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (IchiranBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex < 0)
            {
                return;
            }

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// セル表示内容編集処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            switch (this.form.customDataGridView1.Columns[e.ColumnIndex].Name)
            {
                case ITAKU_KEIYAKU_SHURUI:
                    if (e.Value != null)
                    {
                        switch (e.Value.ToString())
                        {
                            case "1":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_SHURUI_1;
                                e.FormattingApplied = true;
                                break;
                            case "2":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_SHURUI_2;
                                e.FormattingApplied = true;
                                break;
                            case "3":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_SHURUI_3;
                                e.FormattingApplied = true;
                                break;
                        }
                    }
                    break;
                case ITAKU_KEIYAKU_STATUS:
                    if (e.Value != null)
                    {
                        switch (e.Value.ToString())
                        {
                            case "1":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_STATUS_1;
                                e.FormattingApplied = true;
                                break;
                            case "2":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_STATUS_2;
                                e.FormattingApplied = true;
                                break;
                            case "3":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_STATUS_3;
                                e.FormattingApplied = true;
                                break;
                            case "4":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_STATUS_4;
                                e.FormattingApplied = true;
                                break;
                            case "5":
                                e.Value = Properties.Resources.ITAKU_KEIYAKU_STATUS_5;
                                e.FormattingApplied = true;
                                break;
                        }
                    }
                    break;
            }

            //LogUtility.DebugMethodEnd();
        }

        #endregion

        #region プロセスボタン押下処理
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.form.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 電子契約送信ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process2_Click(object sender, EventArgs e)
        {
            // エラーフラグ
            bool errFlg = false;

            // 事前チェック
            errFlg = this.DenshiKeiyakuSoushinJizenCheck();

            // エラーがなければ画面を起動
            if (!errFlg)
            {
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                string cd1 = row.Cells[this.KEY_ID1].Value.ToString();
                FormManager.OpenForm("G715", WINDOW_TYPE.NEW_WINDOW_FLAG, cd1, "");
            }
        }

        /// <summary>
        /// 電子契約送信の事前チェック
        /// </summary>
        /// <returns></returns>
        private bool DenshiKeiyakuSoushinJizenCheck()
        {
            // 一覧データが選択されているかチェックする。            
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row == null)
            {
                MessageBoxUtility.MessageBoxShow("E051", "対象データ");
                return true;
            }

            // 一覧選択中のsystemIdを保持する。
            string cd1 = row.Cells[this.KEY_ID1].Value.ToString();

            // 選択中のデータが削除されているデータかチェックする。
            M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
            cond.SYSTEM_ID = cd1;
            M_ITAKU_KEIYAKU_KIHON data = this.daoItaku.GetDataBySystemId(cond);
            if (data.DELETE_FLG)
            {
                MessageBoxUtility.MessageBoxShowError("委託契約書データが削除されているため、電子契約の送信ができません。");
                return true;
            }

            // 選択中の委託契約データから、M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKIにアドレスが設定されているかチェックする。
            M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI[] souhusakiData = this.daoDenshiSouhusaki.GetDataByCd(cd1);
            if (souhusakiData.Length == 0)
            {
                MessageBoxUtility.MessageBoxShowError("電子契約の送付先情報が設定されていません。");
                return true;
            }
            else
            {
                foreach (M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI souhusaki in souhusakiData)
                {
                    if (souhusaki.MAIL_ADDRESS == null
                        || string.IsNullOrEmpty(souhusaki.MAIL_ADDRESS))
                    {
                        MessageBoxUtility.MessageBoxShowError("電子契約の送付先情報にアドレスが設定されていません。");
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 電子契約履歴ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_process3_Click(object sender, EventArgs e)
        {
            // 画面を起動
            FormManager.OpenForm("G716", this.form.WindowType);
        }


        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        #endregion

        #region マニライト(C8)モード用初期化処理
        /// <summary>
        /// マニライト(C8)モード用初期化処理
        /// </summary>
        private void ManiLiteInit()
        {
            // 「契約書種類」項目を非表示
            this.form.label3.Visible = false;
            this.form.KEIYAKUSHO_SHURUI_CD.Visible = false;
            this.form.KEIYAKUSHO_SHURUI_NAME.Visible = false;
            this.form.KEIYAKUSHO_SEARCH_BUTTON.Visible = false;

        }
        #endregion

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {
            if (string.IsNullOrWhiteSpace(ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_SELECT_TEXT))
            {
                this.form.DATE_SELECT.Text = "8";
            }
            else
            {
                this.form.DATE_SELECT.Text = ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_SELECT_TEXT;
            }

            //開始
            if (ItakuKeiyakushoIchiran.Properties.Settings.Default["DATE_FROM"] == null || this.form.DATE_SELECT.Text == "8")
            {
                this.form.DATE_FROM.Value = null;
            }
            else
            {
                if ((DateTime)ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_FROM != new DateTime(0))
                {

                    this.form.DATE_FROM.Value = (DateTime)ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_FROM;
                }
            }

            //終了
            if (ItakuKeiyakushoIchiran.Properties.Settings.Default["DATE_TO"] == null || this.form.DATE_SELECT.Text == "8")
            {
                this.form.DATE_TO.Value = null;
            }
            else
            {
                if ((DateTime)ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_TO != new DateTime(0))
                {

                    this.form.DATE_TO.Value = (DateTime)ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_TO;
                }
            }

            //委託契約番号の初期値セット
            this.form.ITAKU_KEIYAKU_NO.Text = ItakuKeiyakushoIchiran.Properties.Settings.Default.ITAKU_KEIYAKU_NO_TEXT;

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }

            //アラートの保存データがあればそちらを表示
            if (ItakuKeiyakushoIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
            {
                this.headerForm.alertNumber.Text = ItakuKeiyakushoIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
            }
        }

        #endregion

        #region 委託契約書登録画面起動処理

        /// <summary>
        /// 委託契約書登録画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false,bool dispChangeFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg, dispChangeFlg);

            //システム設定情報読み込み
            this.GetSysInfo();

            // 引数へのオブジェクトを作成する
            // 新規の場合は引数なし、ただし参照の場合は引数あり
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                r_framework.FormManager.FormManager.OpenFormWithAuth("M001", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 表示されている行の取引先CDを取得する
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row == null)
                {
                    MessageBoxUtility.MessageBoxShow("E051", "対象データ");
                    LogUtility.DebugMethodEnd(windowType, newFlg);
                    return;
                }
                string cd1 = row.Cells[this.KEY_ID1].Value.ToString();

                short  cd2 = 0;

                // 登録方法の値が空だった場合
                if (String.IsNullOrEmpty(row.Cells[this.KEY_ID11].Value.ToString()))
                {
                    // M_SYS_INFOのITAKU_KEIYAKU_TOUROKU_HOUHOUの値を取得してセット
                     cd2 = this.sysinfoEntity.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Value;
                }
                else
                {
                    // 表示されている行の登録方法を取得する
                     cd2 = short.Parse(row.Cells[this.KEY_ID11].Value.ToString());
                }

                // 切替モードの時、登録方法を反転する
                if (dispChangeFlg)
                {
                    if (cd2 == 1)
                    {
                        cd2 = 2;
                    }
                    else if (cd2 == 2)
                    {
                        cd2 = 1;
                    }
                }

                // データ削除チェックを行う
                if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    M_ITAKU_KEIYAKU_KIHON cond = new M_ITAKU_KEIYAKU_KIHON();
                    cond.SYSTEM_ID = cd1;
                    M_ITAKU_KEIYAKU_KIHON data = this.daoItaku.GetDataBySystemId(cond);
                    if (data.DELETE_FLG)
                    {
                        //MessageBoxUtility.MessageBoxShow("E026", "コード");
                        //LogUtility.DebugMethodEnd();
                        //return;
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            var result = MessageBoxUtility.MessageBoxShow("C057");
                            if (result != DialogResult.Yes)
                            {
                                LogUtility.DebugMethodEnd();
                                return;
                            }
                        }
                        else
                        {
                            MessageBoxUtility.MessageBoxShow("E026", "コード");
                            LogUtility.DebugMethodEnd();
                            return;
                        }
                    }
                }

                // 権限チェック
                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                    !r_framework.Authority.Manager.CheckAuthority("M001", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (!r_framework.Authority.Manager.CheckAuthority("M001", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxUtility.MessageBoxShow("E158", "修正");
                        return;
                    }

                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                r_framework.FormManager.FormManager.OpenFormWithAuth("M001", windowType, windowType, cd1,cd2);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            this.sysinfoEntity = sysInfo[0];
        }

        #endregion

        #region IBuisinessLogicで必須実装(未使用)

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
        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            LogicClass localLogic = other as LogicClass;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region アラート件数取得処理

        /// <summary>
        /// アラート件数取得処理
        /// </summary>
        /// <returns></returns>
        public int GetAlertCount()
        {
            int result = 0;
            int.TryParse(this.headerForm.alertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            return result;
        }

        #endregion

        #region 検索条件作成

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.daoGenba.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        /// <summary>
        /// 検索条件作成
        /// </summary>
        /// <returns></returns>
        private string MakeSearchCondition()
        {
            LogUtility.DebugMethodStart();

            var res = string.Empty;

            try
            {
                var sb = new StringBuilder();

                #region SELECT句

                sb.Append("SELECT ");
                sb.Append(this.form.SelectQuery);
                sb.AppendFormat(" , M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID AS {0} ", this.KEY_ID1);
                sb.AppendFormat(" , M_ITAKU_KEIYAKU_KIHON.ITAKU_KEIYAKU_TOUROKU_HOUHOU AS {0} ", this.KEY_ID11);

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_KIHON_HST_GENBA")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_KIHON_HST_GENBA"))
                {
                    // 委託契約基本情報_排出事業場
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_KIHON_HST_GENBA.SEQ AS {0} ", this.KEY_ID2);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_HST")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_HST"))
                {
                    // 委託契約別表1（排出）
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_BETSU1_HST.SEQ AS {0} ", this.KEY_ID3);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_YOTEI")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_YOTEI"))
                {
                    // 委託契約別表1（予定）
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_BETSU1_YOTEI.SEQ AS {0} ", this.KEY_ID4);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU2")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU2"))
                {
                    // 委託契約別表2（運搬）
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_BETSU2.SEQ AS {0} ", this.KEY_ID5);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU3")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU3"))
                {
                    // 委託契約別表3（処分）
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_BETSU3.SEQ AS {0} ", this.KEY_ID6);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU4")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU4"))
                {
                    // 委託契約別表4（最終処分）
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_BETSU4.SEQ AS {0} ", this.KEY_ID7);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_OBOE")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_OBOE"))
                {
                    // 委託契約別表覚書
                    sb.AppendFormat(" , M_ITAKU_KEIYAKU_OBOE.SEQ AS {0} ", this.KEY_ID8);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_UPN_KYOKASHO")
                    || this.form.JoinQuery.Contains("M_ITAKU_UPN_KYOKASHO"))
                {
                    // 委託契約運搬許可証紐付
                    sb.AppendFormat(" , M_ITAKU_UPN_KYOKASHO.SEQ AS {0} ", this.KEY_ID9);
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_SBN_KYOKASHO")
                    || this.form.JoinQuery.Contains("M_ITAKU_SBN_KYOKASHO"))
                {
                    // 委託契約処分許可証紐付
                    sb.AppendFormat(" , M_ITAKU_SBN_KYOKASHO.SEQ AS {0} ", this.KEY_ID10);
                }

                #endregion

                #region FROM句

                // 委託契約基本情報
                sb.Append(" FROM ");

                sb.Append("(SELECT M_ITAKU_KEIYAKU_KIHON.*,CASE WHEN M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD IS NULL OR M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = '' THEN M_GYOUSHA.EIGYOU_TANTOU_CD ELSE M_GENBA.EIGYOU_TANTOU_CD END AS EIGYOU_TANTOU_CD ");
                // 委託契約書状況CD(検索用。表示用についてはM012_00190.xmlで定義している)
                sb.AppendFormat(" , CASE WHEN M_ITAKU_KEIYAKU_KIHON.YUUKOU_END IS NOT NULL AND ISNULL(M_ITAKU_KEIYAKU_KIHON.KOUSHIN_SHUBETSU, 0) = 2 AND CONVERT(date, M_ITAKU_KEIYAKU_KIHON.YUUKOU_END) < CONVERT(date, GETDATE())");
                sb.AppendFormat("     THEN '5'");
                sb.AppendFormat("     ELSE");
                sb.AppendFormat("       CASE WHEN M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE IS NOT NULL AND ISNULL(M_ITAKU_KEIYAKU_KIHON.KOUSHIN_SHUBETSU, 0) = 1 AND CONVERT(date, M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE) < CONVERT(date, GETDATE())");
                sb.AppendFormat("         THEN '5'");
                sb.AppendFormat("         ELSE ");
                sb.AppendFormat("           CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_END_DATE IS NOT NULL");
                sb.AppendFormat("             THEN '4'");
                sb.AppendFormat("             ELSE");
                sb.AppendFormat("               CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_RETURN_DATE IS NOT NULL");
                sb.AppendFormat("                 THEN '3'");
                sb.AppendFormat("                 ELSE");
                sb.AppendFormat("                   CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_SEND_DATE IS NOT NULL");
                sb.AppendFormat("                     THEN '2'");
                sb.AppendFormat("                     ELSE ");
                sb.AppendFormat("                       CASE WHEN M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_CREATE_DATE IS NOT NULL");
                sb.AppendFormat("                         THEN '1'");
                sb.AppendFormat("                         ELSE ''");
                sb.AppendFormat("                       END");
                sb.AppendFormat("                   END");
                sb.AppendFormat("               END");
                sb.AppendFormat("           END");
                sb.AppendFormat("       END");
                sb.AppendFormat(" END AS 'HIDDEN_ITAKU_KEIYAKU_STATUS' ");
                sb.Append("FROM M_ITAKU_KEIYAKU_KIHON ");
                sb.Append("LEFT JOIN M_GYOUSHA ON M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");
                sb.Append("LEFT JOIN M_GENBA ON M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = M_GENBA.GENBA_CD ) AS M_ITAKU_KEIYAKU_KIHON ");

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_KIHON_HST_GENBA")
                    || this.form.SelectQuery.Contains("M_GENBA1"))
                {
                    // 委託契約基本情報_排出事業場
                    sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_KIHON_HST_GENBA ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_KIHON_HST_GENBA.SYSTEM_ID ");
                    sb.Append(" LEFT JOIN M_GENBA M_GENBA1 ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON_HST_GENBA.HAISHUTSU_JIGYOUSHA_CD = M_GENBA1.GYOUSHA_CD AND M_ITAKU_KEIYAKU_KIHON_HST_GENBA.HAISHUTSU_JIGYOUJOU_CD = M_GENBA1.GENBA_CD ");
                    sb.Append(" LEFT JOIN M_TODOUFUKEN M_TODOUFUKEN_GENBA1 ");
                    sb.Append(" ON M_GENBA1.GENBA_TODOUFUKEN_CD = M_TODOUFUKEN_GENBA1.TODOUFUKEN_CD AND M_TODOUFUKEN_GENBA1.DELETE_FLG = 0 ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_HST")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_HST")
                    || !string.IsNullOrWhiteSpace(this.form.HOUKOKUSHO_BUNRUI_CD.Text)
                    )
                {
                    // 委託契約別表1（排出）
                    sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_BETSU1_HST ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU1_HST.SYSTEM_ID ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_YOTEI")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU1_YOTEI"))
                {
                    // 委託契約別表1（予定）
                    sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_BETSU1_YOTEI ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU1_YOTEI.SYSTEM_ID ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_BETSU2")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_BETSU2")
                    || !string.IsNullOrWhiteSpace(this.form.UNPANGYOUSHA_CD.Text))
                {
                    // 委託契約別表2（運搬）
                    sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_BETSU2 ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU2.SYSTEM_ID ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_OBOE")
                    || this.form.JoinQuery.Contains("M_ITAKU_KEIYAKU_OBOE"))
                {
                    // 委託契約別表覚書
                    sb.Append(" LEFT JOIN M_ITAKU_KEIYAKU_OBOE ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_OBOE.SYSTEM_ID ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_UPN_KYOKASHO")
                    || this.form.JoinQuery.Contains("M_ITAKU_UPN_KYOKASHO"))
                {
                    // 委託契約運搬許可証紐付
                    sb.Append(" LEFT JOIN M_ITAKU_UPN_KYOKASHO ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_UPN_KYOKASHO.SYSTEM_ID ");
                }

                if (this.form.SelectQuery.Contains("M_ITAKU_SBN_KYOKASHO")
                    || this.form.JoinQuery.Contains("M_ITAKU_SBN_KYOKASHO"))
                {
                    // 委託契約処分許可証紐付
                    sb.Append(" LEFT JOIN M_ITAKU_SBN_KYOKASHO ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_SBN_KYOKASHO.SYSTEM_ID ");
                }

                // 業者
                sb.Append(" LEFT JOIN M_GYOUSHA M_GYOUSHA1 ");
                sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = M_GYOUSHA1.GYOUSHA_CD ");
                sb.Append(" LEFT JOIN M_TODOUFUKEN M_TODOUFUKEN_GYOUSHA1 ");
                sb.Append(" ON M_GYOUSHA1.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN_GYOUSHA1.TODOUFUKEN_CD AND M_TODOUFUKEN_GYOUSHA1.DELETE_FLG = 0 ");

                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // 電子契約送付先マスタ
                    sb.Append(" LEFT JOIN ( ");
                    sb.Append(" select distinct itaku.SYSTEM_ID, itaku.ACCESS_CD, itaku.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD, keiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME , itaku.DENSHI_KEIYAKU_SHANAI_KEIRO ");
                    sb.Append(" from M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI itaku ");
                    sb.Append(" left join M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME as keiroName ");
                    sb.Append(" on itaku.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = keiroName.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD ) as SOUHUSAKI ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = SOUHUSAKI.SYSTEM_ID ");
                }
                else
                {
                    // M_SYS_INFOを紐付けて項目をでっちあげる
                    sb.Append(" LEFT JOIN ( ");
                    sb.Append(" select distinct '' AS SYSTEM_ID, '' AS ACCESS_CD, '' AS DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD, '' AS DENSHI_KEIYAKU_SHANAI_KEIRO_NAME , '' AS DENSHI_KEIYAKU_SHANAI_KEIRO ");
                    sb.Append(" from M_SYS_INFO WHERE SYS_ID = 0) as SOUHUSAKI ");
                    sb.Append(" ON M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = SOUHUSAKI.SYSTEM_ID ");
                }

                // パターンから作成したJOIN句
                sb.Append(this.form.JoinQuery);

                #endregion

                #region WHERE句

                var strTemp = string.Empty;

                sb.Append(" WHERE 1=1 ");

                // 委託契約番号
                strTemp = this.form.ITAKU_KEIYAKU_NO.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.ITAKU_KEIYAKU_NO LIKE '%{0}%'", strTemp);
                }

                /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　start
                // 契約書種類
                strTemp = this.form.KEIYAKUSHO_SHURUI_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.ITAKU_KEIYAKU_SHURUI LIKE '%{0}%'", strTemp);
                }

                // 委託契約ステータス
                strTemp = this.form.KEIYAKU_JYOUKYOU_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.HIDDEN_ITAKU_KEIYAKU_STATUS LIKE '%{0}%'", strTemp);
                }

                //DATE CHECK
                strTemp = this.form.DATE_SELECT.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    string strFrom = Convert.ToString(this.form.DATE_FROM.Value);
                    string strTo = Convert.ToString(this.form.DATE_TO.Value);
                    if (strTemp.Equals("1"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_CREATE_DATE >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_CREATE_DATE <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("2"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_SEND_DATE >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_SEND_DATE <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("3"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_RETURN_DATE >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_RETURN_DATE <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("4"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_END_DATE >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_END_DATE <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("5"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.YUUKOU_BEGIN >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.YUUKOU_BEGIN <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("6"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.YUUKOU_END >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.YUUKOU_END <= '{0}'", strTo);
                        }
                    }
                    else if (strTemp.Equals("7"))
                    {
                        if (!string.IsNullOrWhiteSpace(strFrom))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE >= '{0}'", strFrom);
                        }
                        if (!string.IsNullOrWhiteSpace(strTo))
                        {
                            sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE <= '{0}'", strTo);
                        }
                    }
                }
                /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　end

                // 排出事業者
                strTemp = this.form.GYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = '{0}'", strTemp);
                }

                // 排出事業場
                strTemp = this.form.GENBA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!this.form.SelectQuery.Contains("M_ITAKU_KEIYAKU_KIHON_HST_GENBA") && !this.form.SelectQuery.Contains("M_GENBA1"))
                    {
                        sb.AppendFormat(" AND EXISTS(SELECT HAISHUTSU_JIGYOUJOU_CD FROM M_ITAKU_KEIYAKU_KIHON_HST_GENBA WHERE M_ITAKU_KEIYAKU_KIHON_HST_GENBA.HAISHUTSU_JIGYOUJOU_CD = '{0}' AND M_ITAKU_KEIYAKU_KIHON_HST_GENBA.HAISHUTSU_JIGYOUSHA_CD = '{1}' AND M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_KIHON_HST_GENBA.SYSTEM_ID) ", strTemp, this.form.GYOUSHA_CD.Text);
                    }
                    else
                    {
                        sb.AppendFormat(" AND (M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = '{0}' OR M_ITAKU_KEIYAKU_KIHON_HST_GENBA.HAISHUTSU_JIGYOUJOU_CD = '{0}')", strTemp);
                    }
                }

                /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　start
                // 運搬業者
                strTemp = this.form.UNPANGYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU2.UNPAN_GYOUSHA_CD = '{0}'", strTemp);
                }

                if (!string.IsNullOrWhiteSpace(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SHOBUN_GENBA_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SHOBUN_HOUHOU_SHOBUN_CD.Text))
                {
                    sb.Append(" AND EXISTS ( ");
                    sb.Append(" SELECT * FROM M_ITAKU_KEIYAKU_BETSU3 WHERE M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU3.SYSTEM_ID ");
                    
                    // 処分受託者（処分）
                    strTemp = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU3.SHOBUN_GYOUSHA_CD = '{0}' ", strTemp);
                    }
                    // 処分事業場
                    strTemp = this.form.SHOBUN_GENBA_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU3.SHOBUN_JIGYOUJOU_CD = '{0}' ", strTemp);
                    }
                    // 処分方法（処分）
                    strTemp = this.form.SHOBUN_HOUHOU_SHOBUN_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU3.SHOBUN_HOUHOU_CD = '{0}' ", strTemp);
                    }
                    sb.Append(" ) ");
                }

                if (!string.IsNullOrWhiteSpace(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SAISHUU_SHOBUNJOU_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.SHOBUN_HOUHOU_SAISHU_CD.Text))
                {
                    sb.Append(" AND EXISTS ( ");
                    sb.Append(" SELECT * FROM M_ITAKU_KEIYAKU_BETSU4 WHERE M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_BETSU4.SYSTEM_ID ");
                    
                    // 処分受託者（最終）
                    strTemp = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU4.LAST_SHOBUN_GYOUSHA_CD = '{0}' ", strTemp);
                    }
                    // 最終処分場
                    strTemp = this.form.SAISHUU_SHOBUNJOU_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU4.LAST_SHOBUN_JIGYOUJOU_CD = '{0}'  ", strTemp);
                    }
                    // 処分方法（最終）
                    strTemp = this.form.SHOBUN_HOUHOU_SAISHU_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU4.SHOBUN_HOUHOU_CD = '{0}' ", strTemp);
                    }
                    sb.Append(" ) ");
                }

                if (!string.IsNullOrWhiteSpace(this.form.UNPAN_TSUMIKAE_CD.Text)
                    || !string.IsNullOrWhiteSpace(this.form.TSUMIKAEHOKAN_CD.Text))
                {
                    sb.Append(" AND EXISTS ( ");
                    sb.Append(" SELECT * FROM M_ITAKU_KEIYAKU_TSUMIKAE WHERE M_ITAKU_KEIYAKU_KIHON.SYSTEM_ID = M_ITAKU_KEIYAKU_TSUMIKAE.SYSTEM_ID ");

                    // 運搬業者（積替）
                    strTemp = this.form.UNPAN_TSUMIKAE_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_TSUMIKAE.UNPAN_GYOUSHA_CD = '{0}' ", strTemp);
                    }

                    // 積替保管場所
                    strTemp = this.form.TSUMIKAEHOKAN_CD.Text;
                    if (!string.IsNullOrWhiteSpace(strTemp))
                    {
                        sb.AppendFormat(" AND M_ITAKU_KEIYAKU_TSUMIKAE.TSUMIKAE_HOKANBA_CD = '{0}' ", strTemp);
                    }
                    sb.Append(" ) ");
                }

                // 報告書分類
                strTemp = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_BETSU1_HST.HOUKOKUSHO_BUNRUI_CD = '{0}'", strTemp);
                }

                // 営業担当者
                strTemp = this.form.EIGYOU_TANTOU_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    sb.AppendFormat(" AND M_ITAKU_KEIYAKU_KIHON.EIGYOU_TANTOU_CD = '{0}'", strTemp);
                }
                /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　end

                // 表示条件
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    sb.Append(" AND M_ITAKU_KEIYAKU_KIHON.DELETE_FLG = 0");
                }

                #endregion

                #region ORDER BY句

                if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
                {
                    sb.AppendFormat(" ORDER BY {0}", this.form.OrderByQuery);
                }

                #endregion

                res = sb.ToString();

                return res;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(res);
            }
        }

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string GetSearchString()
        {
            var result = new StringBuilder(256);
            string strTemp;

            // ヘッダー検索条件
            strTemp = this.GetHeaderSearchString();
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(strTemp);
            }

            // 委託契約番号
            strTemp = this.form.ITAKU_KEIYAKU_NO.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_ITAKU_KEIYAKU_KIHON.ITAKU_KEIYAKU_NO LIKE '%{0}%'", strTemp);
            }

            // 排出事業者
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUSHA_CD = '{0}'", strTemp);
            }

            // 排出事業場
            strTemp = this.form.GENBA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_ITAKU_KEIYAKU_KIHON.HAISHUTSU_JIGYOUJOU_CD = '{0}'", strTemp);
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" M_ITAKU_KEIYAKU_KIHON.DELETE_FLG = 0");
            }
            
            return result.ToString();
        }

        /// <summary>
        /// ヘッダー検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string GetHeaderSearchString()
        {
            var result = string.Empty;
            string columnName = string.Empty;
            List<string> conds = new List<string>();
            switch (this.form.DATE_SELECT.Text)
            {
                case "1":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_CREATE_DATE";
                    break;
                case "2":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_SEND_DATE";
                    break;
                case "3":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_RETURN_DATE";
                    break;
                case "4":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.KEIYAKUSHO_END_DATE";
                    break;
                case "5":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.YUUKOU_BEGIN";
                    break;
                case "6":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.YUUKOU_END";
                    break;
                case "7":
                    columnName = "M_ITAKU_KEIYAKU_KIHON.KOUSHIN_END_DATE";
                    break;
                case "8":
                    columnName = string.Empty;
                    break;
            }
            if (!string.IsNullOrWhiteSpace(columnName) && this.form.DATE_FROM.Value != null)
            {
                conds.Add(columnName + " >= CONVERT(DATETIME, '" + ((DateTime)this.form.DATE_FROM.Value).ToString("yyyy/MM/dd") + "', 120)");
            }
            if (!string.IsNullOrWhiteSpace(columnName) && this.form.DATE_TO.Value != null)
            {
                conds.Add(columnName + " <= CONVERT(DATETIME, '" + ((DateTime)this.form.DATE_TO.Value).ToString("yyyy/MM/dd") + "', 120)");
            }

            if (conds.Count > 0)
            {
                result = string.Join(" AND ", conds.ToArray());
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            if (this.sysinfoEntity != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 表示条件を次回呼出時のデフォルト値として保存します
        /// </summary>
        public bool SaveHyoujiJoukenDefault()
        {
            LogUtility.DebugMethodStart();

            try
            {
                ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_SELECT_TEXT = this.form.DATE_SELECT.Text;
                ItakuKeiyakushoIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                ItakuKeiyakushoIchiran.Properties.Settings.Default.ITAKU_KEIYAKU_NO_TEXT = this.form.ITAKU_KEIYAKU_NO.Text;

                //表示条件
                ItakuKeiyakushoIchiran.Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                DateTime timeFrom = new DateTime();
                DateTime timeTo = new DateTime();
                if (!string.IsNullOrWhiteSpace(this.form.DATE_FROM.Text))
                {
                    DateTime.TryParse(this.form.DATE_FROM.Text.ToString(), out timeFrom);
                    ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_FROM = timeFrom;
                }

                if (!string.IsNullOrWhiteSpace(this.form.DATE_TO.Text))
                {
                    DateTime.TryParse(this.form.DATE_TO.Text.ToString(), out timeTo);
                    ItakuKeiyakushoIchiran.Properties.Settings.Default.DATE_TO = timeTo;
                }

                ItakuKeiyakushoIchiran.Properties.Settings.Default.Save();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 業者チェック

        /// <summary>
        /// 業者チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckGyousha()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCd = this.form.GYOUSHA_CD.Text;

            // 業者を取得
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.daoGyousha.GetAllValidData(entity);
            if (null == gyousha || gyousha.Length == 0)
            {
                // 業者名設定
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", "業者");

                this.form.GYOUSHA_CD.Focus();
                return false;
            }

            // 業者を設定
            // 20151021 BUNN #12040 STR
            if (gyousha[0].HAISHUTSU_NIZUMI_GYOUSHA_KBN)
            // 20151021 BUNN #12040 END
            {
                this.form.GYOUSHA_NAME_RYAKU.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
            }
            else
            {
                // 業者名設定
                this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", "業者");

                this.form.GYOUSHA_CD.Focus();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        /// <summary>
        /// 現場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;

                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "排出事業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return false;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                var genbaEntityList = this.GetGenba(this.form.GENBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.GYOUSHA_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        // 20151021 BUNN #12040 STR
                        if (genbaEntity.HAISHUTSU_NIZUMI_GENBA_KBN)
                        // 20151021 BUNN #12040 END
                        {
                            isContinue = true;
                            this.form.GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    MessageBoxUtility.MessageBoxShow("E062", "業者");
                    this.form.GENBA_CD.Focus();
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA[] GetGenba(string genbaCd)
        {
            if (string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd.PadLeft(6, '0');
            keyEntity.ISNOT_NEED_DELETE_FLG = true;
            var genba = this.daoGenba.GetAllValidData(keyEntity);

            if (genba == null || genba.Length < 1)
            {
                return null;
            }

            return genba;
        }

        // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrWhiteSpace(this.form.DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.form.DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                //this.form.DATE_FROM.IsInputErrorOccured = true;
                //this.form.DATE_TO.IsInputErrorOccured = true;
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.form.DATE_SELECT_1.Checked)
                {
                    string[] errorMsg = { "作成日From", "作成日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_2.Checked)
                {
                    string[] errorMsg = { "送付日From", "送付日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_3.Checked)
                {
                    string[] errorMsg = { "返送日From", "返送日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_4.Checked)
                {
                    string[] errorMsg = { "保管日From", "保管日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_5.Checked)
                {
                    string[] errorMsg = { "有効期間開始From", "有効期間開始To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_6.Checked)
                {
                    string[] errorMsg = { "有効期間終了From", "有効期間終了To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.DATE_SELECT_7.Checked)
                {
                    string[] errorMsg = { "自動更新終了日From", "自動更新終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }

                this.form.DATE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 end

        /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　start
        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public bool CheckListPopup(int nPopupID)
        {
            try
            {
                if (nPopupID == 1)
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;

                    //01
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "収集運搬契約";
                    dt.Rows.Add(row);
                    //02
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "処分契約";
                    dt.Rows.Add(row);
                    //03
                    row = dt.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = "収集運搬/処分契約";
                    dt.Rows.Add(row);

                    form.table = dt;
                    //form.title = "契約書種類";
                    //form.headerList = new List<string>();
                    //form.headerList.Add("契約書種類CD");
                    //form.headerList.Add("契約書種類名");
                    form.PopupTitleLabel = "契約書種類";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約書種類CD", "契約書種類名" };
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.KEIYAKUSHO_SHURUI_CD.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.KEIYAKUSHO_SHURUI_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                else if (nPopupID == 2)
                {
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;

                    //01
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "作成";
                    dt.Rows.Add(row);
                    //02
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "送付";
                    dt.Rows.Add(row);
                    //03
                    row = dt.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = "返送";
                    dt.Rows.Add(row);

                    //04
                    row = dt.NewRow();
                    row["CD"] = "4";
                    row["VALUE"] = "保管";
                    dt.Rows.Add(row);

                    //05
                    row = dt.NewRow();
                    row["CD"] = "5";
                    row["VALUE"] = "解約済";
                    dt.Rows.Add(row);

                    form.table = dt;
                    form.PopupTitleLabel = "契約状況";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約状況CD", "契約状況名" };
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.KEIYAKU_JYOUKYOU_CD.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.KEIYAKU_JYOUKYOU_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckListPopup", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region 運搬業者チェック

        /// <summary>
        /// 業者運搬業者
        /// true:OK false:NG
        /// </summary>
        internal bool CheckUnpanGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var UnpangyoushaCd = this.form.UNPANGYOUSHA_CD.Text;
                var UnpangyoushaName = this.form.UNPANGYOUSHA_NAME.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(UnpangyoushaCd))
                {
                    // 関連項目クリア
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    this.form.beforeUnpanGyousaCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.UNPANGYOUSHA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.daoGyousha.GetAllValidData(entity);
                if (null == gyousha || gyousha.Length == 0)
                {
                    // 業者名設定
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "運搬業者");

                    this.form.UNPANGYOUSHA_CD.Focus();
                    return false;
                }

                // 業者を設定
                // 20151021 BUNN #12040 STR
                if (gyousha[0].UNPAN_JUTAKUSHA_KAISHA_KBN)
                // 20151021 BUNN #12040 END
                {
                    this.form.UNPANGYOUSHA_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.UNPANGYOUSHA_NAME.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "運搬業者");

                    this.form.UNPANGYOUSHA_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckUnpanGyousha", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分受託者（処分）チェック

        /// <summary>
        /// 処分受託者（処分）
        /// true:OK false:NG
        /// </summary>
        internal bool ShobunJyutakushaShobunCD()
        {
            LogUtility.DebugMethodStart();

            var ShobunJyutakushaShobunCd = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
            var ShobunJyutakushaShobunName = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text;

            // 入力されてない場合
            if (String.IsNullOrEmpty(ShobunJyutakushaShobunCd))
            {
                // 関連項目クリア
                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                this.form.beforeShobunJyutakushaShobunCD = string.Empty;


                return false;
            }

            // 業者を取得
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
            entity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.daoGyousha.GetAllValidData(entity);
            if (null == gyousha || gyousha.Length == 0)
            {
                // 業者名設定
                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", "処分受託者（処分）");

                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Focus();
                return false;
            }

            // 業者を設定
            // 20151021 BUNN #12040 STR
            if (gyousha[0].SHOBUN_NIOROSHI_GYOUSHA_KBN)
            // 20151021 BUNN #12040 END
            {
                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
            }
            else
            {
                // 業者名設定
                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", "処分受託者（処分）");

                this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Focus();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 処分事業場チェック
        /// <summary>
        /// 処分事業場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.SHOBUN_GENBA_CD.Text))
                {
                    return false;
                }

                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                var genbaEntityList = this.GetGenba(this.form.SHOBUN_GENBA_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "処分事業場");
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.form.SHOBUN_GENBA_CD.Focus();
                    return false;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        // 20151021 BUNN #12040 STR
                        if (genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN)
                        // 20151021 BUNN #12040 END
                        {
                            isContinue = true;
                            this.form.SHOBUN_GENBA_NAME_RYAKU.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    MessageBoxUtility.MessageBoxShow("E062", "処分受託者（処分）");
                    this.form.SHOBUN_GENBA_CD.Focus();
                    this.form.SHOBUN_GENBA_NAME_RYAKU.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分受託者（最終）チェック

        /// <summary>
        /// 処分受託者（最終）
        /// true:OK false:NG
        /// </summary>
        internal bool ShobunJyutakushaSaishuCD()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ShobunJyutakushaSaishuCd = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                var ShobunJyutakushaSaishuName = this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(ShobunJyutakushaSaishuCd))
                {
                    // 関連項目クリア
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    this.form.beforeShobunJyutakushaSaishuCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var gyousha = this.daoGyousha.GetAllValidData(entity);
                if (null == gyousha || gyousha.Length == 0)
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "処分受託者（最終）");

                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Focus();
                    return false;
                }

                // 業者を設定
                // 20151021 BUNN #12040 STR
                if (gyousha[0].SHOBUN_NIOROSHI_GYOUSHA_KBN)
                // 20151021 BUNN #12040 END
                {
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "処分受託者（最終）");

                    this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ShobunJyutakushaSaishuCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShobunJyutakushaSaishuCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 最終処分場チェック
        /// <summary>
        /// 最終処分場チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckSaishuGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.SAISHUU_SHOBUNJOU_CD.Text))
                {
                    return false;
                }

                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                var genbaEntityList = this.GetGenba(this.form.SAISHUU_SHOBUNJOU_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "最終処分場");
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                if (string.IsNullOrEmpty(this.form.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    return false;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        if (genbaEntity.SAISHUU_SHOBUNJOU_KBN)
                        {
                            isContinue = true;
                            this.form.SAISHUU_SHOBUNJOU_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    MessageBoxUtility.MessageBoxShow("E062", "処分受託者（最終）");
                    this.form.SAISHUU_SHOBUNJOU_CD.Focus();
                    this.form.SAISHUU_SHOBUNJOU_NAME.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckSaishuGenba", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSaishuGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 運搬業者（積替）チェック

        /// <summary>
        /// 運搬業者（積替）
        /// true:OK false:NG
        /// </summary>
        internal bool CheckUnpanTsumikaeCd()
        {
            LogUtility.DebugMethodStart();

            var UnpanTsumikaeCd = this.form.UNPAN_TSUMIKAE_CD.Text;
            var UnpanTsumikaeName = this.form.UNPAN_TSUMIKAE_NAME.Text;

            // 入力されてない場合
            if (String.IsNullOrEmpty(UnpanTsumikaeCd))
            {
                // 関連項目クリア
                this.form.UNPAN_TSUMIKAE_NAME.Text = String.Empty;
                this.form.beforeUnpanTsumikaeCD = string.Empty;


                return false;
            }

            // 業者を取得
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = this.form.UNPAN_TSUMIKAE_CD.Text;
            entity.ISNOT_NEED_DELETE_FLG = true;
            var gyousha = this.daoGyousha.GetAllValidData(entity);
            if (null == gyousha || gyousha.Length == 0)
            {
                // 業者名設定
                this.form.UNPAN_TSUMIKAE_NAME.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", " 運搬業者（積替）");

                this.form.UNPAN_TSUMIKAE_CD.Focus();
                return false;
            }

            // 業者を設定
            // 20151021 BUNN #12040 STR
            if (gyousha[0].UNPAN_JUTAKUSHA_KAISHA_KBN)
            // 20151021 BUNN #12040 END
            {
                this.form.UNPAN_TSUMIKAE_NAME.Text = gyousha[0].GYOUSHA_NAME_RYAKU;
            }
            else
            {
                // 業者名設定
                this.form.UNPAN_TSUMIKAE_NAME.Text = String.Empty;
                MessageBoxUtility.MessageBoxShow("E020", " 運搬業者（積替）");

                this.form.UNPAN_TSUMIKAE_CD.Focus();
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 積替保管場所チェック
        /// <summary>
        /// 積替保管場所チェック
        /// true:OK false:NG
        /// </summary>
        internal bool CheckTsumikaehokan()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.form.TSUMIKAEHOKAN_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.TSUMIKAEHOKAN_CD.Text))
                {
                    return false;
                }

                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
                if (string.IsNullOrEmpty(this.form.UNPAN_TSUMIKAE_CD.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "運搬業者（積替）");
                    this.form.TSUMIKAEHOKAN_CD.Text = string.Empty;
                    this.form.TSUMIKAEHOKAN_CD.Focus();
                    return false;
                }
                // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                var genbaEntityList = this.GetGenba(this.form.TSUMIKAEHOKAN_CD.Text);
                if (genbaEntityList == null || genbaEntityList.Length < 1)
                {
                    MessageBoxUtility.MessageBoxShow("E020", "積替保管場所");
                    this.form.TSUMIKAEHOKAN_CD.Focus();
                    return false;
                }

                bool isContinue = false;

                if (string.IsNullOrEmpty(this.form.UNPAN_TSUMIKAE_NAME.Text))
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShow("E051", "運搬業者（積替）");
                    this.form.TSUMIKAEHOKAN_CD.Text = string.Empty;
                    this.form.TSUMIKAEHOKAN_CD.Focus();
                    return false;
                }

                foreach (M_GENBA genbaEntity in genbaEntityList)
                {
                    if (this.form.UNPAN_TSUMIKAE_CD.Text.Equals(genbaEntity.GYOUSHA_CD))
                    {
                        if (genbaEntity.TSUMIKAEHOKAN_KBN)
                        {
                            isContinue = true;
                            this.form.TSUMIKAEHOKAN_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                            break;
                        }
                        else
                        {
                            isContinue = false;
                            break;
                        }
                    }
                }

                if (!isContinue)
                {
                    // 一致するものがないのでエラー
                    MessageBoxUtility.MessageBoxShow("E062", "運搬業者（積替）");
                    this.form.TSUMIKAEHOKAN_CD.Focus();
                    this.form.TSUMIKAEHOKAN_NAME.Text = String.Empty;
                    return false;
                }

                LogUtility.DebugMethodEnd();

                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckTsumikaehokan", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTsumikaehokan", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分方法（処分）チェック

        /// <summary>
        /// 処分方法（処分）
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunHouhouShobun()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ShobunHouhouShobunCd = this.form.SHOBUN_HOUHOU_SHOBUN_CD.Text;
                var ShobunHouhouShobunName = this.form.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(ShobunHouhouShobunCd))
                {
                    // 関連項目クリア
                    this.form.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Text = String.Empty;
                    this.form.beforeShobunHouhouShobunCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_SHOBUN_HOUHOU entity = new M_SHOBUN_HOUHOU();
                entity.SHOBUN_HOUHOU_CD = this.form.SHOBUN_HOUHOU_SHOBUN_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var shoubunhouhou = this.daoShobunHouhou.GetAllValidData(entity);
                if (null == shoubunhouhou || shoubunhouhou.Length == 0)
                {
                    // 業者名設定
                    this.form.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "処分方法（処分）");

                    this.form.SHOBUN_HOUHOU_SHOBUN_CD.Focus();
                    return false;
                }

                this.form.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Text = shoubunhouhou[0].SHOBUN_HOUHOU_NAME_RYAKU;

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunHouhouShobun", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunHouhouShobun", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 処分方法（最終）チェック

        /// <summary>
        /// 処分方法（最終）
        /// true:OK false:NG
        /// </summary>
        internal bool CheckShobunHouhouSaishu()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var ShobunHouhouSaishuCd = this.form.SHOBUN_HOUHOU_SAISHU_CD.Text;
                var ShobunHouhouSaishuName = this.form.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(ShobunHouhouSaishuCd))
                {
                    // 関連項目クリア
                    this.form.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Text = String.Empty;
                    this.form.beforeShobunHouhouSaishuCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_SHOBUN_HOUHOU entity = new M_SHOBUN_HOUHOU();
                entity.SHOBUN_HOUHOU_CD = this.form.SHOBUN_HOUHOU_SAISHU_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var shoubunhouhou = this.daoShobunHouhou.GetAllValidData(entity);
                if (null == shoubunhouhou || shoubunhouhou.Length == 0)
                {
                    // 業者名設定
                    this.form.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "処分方法（最終）");

                    this.form.SHOBUN_HOUHOU_SAISHU_CD.Focus();
                    return false;
                }

                this.form.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Text = shoubunhouhou[0].SHOBUN_HOUHOU_NAME_RYAKU;

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShobunHouhouSaishu", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShobunHouhouSaishu", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 報告書分類チェック

        /// <summary>
        /// 報告書分類
        /// true:OK false:NG
        /// </summary>
        internal bool CheckHoukokushoBunrui()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var HoukokushoBunruiCd = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                var HoukokushoBunruiName = this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(HoukokushoBunruiCd))
                {
                    // 関連項目クリア
                    this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = String.Empty;
                    this.form.beforeHoukokushoBunruiCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_HOUKOKUSHO_BUNRUI entity = new M_HOUKOKUSHO_BUNRUI();
                entity.HOUKOKUSHO_BUNRUI_CD = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var HoukokushoBunrui = this.daoHoukokushoBunrui.GetAllValidData(entity);
                if (null == HoukokushoBunrui || HoukokushoBunrui.Length == 0)
                {
                    // 業者名設定
                    this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "報告書分類");

                    this.form.HOUKOKUSHO_BUNRUI_CD.Focus();
                    return false;
                }

                this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = HoukokushoBunrui[0].HOUKOKUSHO_BUNRUI_NAME_RYAKU;

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckHoukokushoBunrui", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHoukokushoBunrui", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion

        #region 営業担当者チェック

        /// <summary>
        /// 営業担当者
        /// true:OK false:NG
        /// </summary>
        internal bool CheckEigyouTantou()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var EigyouTantouCd = this.form.EIGYOU_TANTOU_CD.Text;
                var EigyouTantouName = this.form.EIGYOU_TANTOU_NAME_RYAKU.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(EigyouTantouCd))
                {
                    // 関連項目クリア
                    this.form.EIGYOU_TANTOU_NAME_RYAKU.Text = String.Empty;
                    this.form.beforeEigyouTantouCD = string.Empty;


                    return false;
                }

                // 業者を取得
                M_SHAIN entity = new M_SHAIN();
                entity.SHAIN_CD = this.form.EIGYOU_TANTOU_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var shain = this.daoShain.GetAllValidData(entity);
                if (null == shain || shain.Length == 0)
                {
                    // 業者名設定
                    this.form.EIGYOU_TANTOU_NAME_RYAKU.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "営業担当者");

                    this.form.EIGYOU_TANTOU_CD.Focus();
                    return false;
                }

                // 業者を設定
                if (shain[0].EIGYOU_TANTOU_KBN)
                {
                    this.form.EIGYOU_TANTOU_NAME_RYAKU.Text = shain[0].SHAIN_NAME_RYAKU;
                }
                else
                {
                    // 業者名設定
                    this.form.EIGYOU_TANTOU_NAME_RYAKU.Text = String.Empty;
                    MessageBoxUtility.MessageBoxShow("E020", "営業担当者");

                    this.form.EIGYOU_TANTOU_CD.Focus();
                    return false;
                }

                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckEigyouTantou", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckEigyouTantou", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
        #endregion
        /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　end
    }
}
