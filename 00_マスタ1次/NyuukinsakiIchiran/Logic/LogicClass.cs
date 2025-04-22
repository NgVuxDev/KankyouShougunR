// $Id: LogicClass.cs 6496 2013-11-11 06:21:37Z gai $
using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NyuukinsakiIchiran.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using System.Data;

namespace NyuukinsakiIchiran.Logic
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
        private readonly string ButtonInfoXmlPath = "NyuukinsakiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面連携に使用するキー取得項目名
        /// </summary>
        internal readonly string KEY_ID1 = "HIDDEN_NYUUKINSAKI_CD";

        /// <summary>
        /// キー項目名
        /// </summary>
        internal readonly string KEY_ID2 = "HIDDEN_FURIKOMI_SEQ";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private NyuukinsakiIchiranForm form;

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 都道府県エンティティ
        /// </summary>
        private M_TODOUFUKEN todoufukenEntity;

        /// <summary>
        /// 入金先のDao
        /// </summary>
        private IM_NYUUKINSAKIDao daoNyuukinsaki;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(NyuukinsakiIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoNyuukinsaki = DaoInitUtility.GetComponent<IM_NYUUKINSAKIDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

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

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                this.InitHeaderArea();

                // 表示条件初期化
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
            var parentForm = (BusinessBaseForm)this.form.Parent;
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

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);            //新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);            //修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);            //削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);            //複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリ
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索ア
            /// 20150824 katen #11992 「並び順」の項目があるにもかかわらず、[F10]並び替えボタンがない。 start
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);            //並び替え
            /// 20150824 katen #11992 「並び順」の項目があるにもかかわらず、[F10]並び替えボタンがない。 end
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);        //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);             //検索条件設定画面へ遷移

            //明細ダブルクリック時のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            //明細エンター時のイベント
            this.form.customDataGridView1.KeyDown += new KeyEventHandler(this.DetailKeyDown);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd(sender, e);
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

            LogUtility.DebugMethodEnd(sender, e);
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

            LogUtility.DebugMethodEnd(sender, e);
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

            LogUtility.DebugMethodEnd(sender, e);
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

            LogUtility.DebugMethodEnd(sender, e);
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
            this.form.Nyuukinsaki_FURIGANA.Text = string.Empty;
            this.form.Nyuukinsaki_NAME1.Text = string.Empty;
            this.form.Nyuukinsaki_NAME2.Text = string.Empty;
            this.form.Nyuukinsaki_NAME_RYAKU.Text = string.Empty;
            this.form.Nyuukinsaki_TODOUFUKEN_CD.Text = string.Empty;
            this.form.Nyuukinsaki_TODOUFUKEN_NAME.Text = string.Empty;
            this.form.Nyuukinsaki_ADDRESS.Text = string.Empty;

            var ds = (DataTable)this.form.customDataGridView1.DataSource as DataTable;
            if (ds != null)
            {
                ds.Clear();
                this.form.customDataGridView1.DataSource = ds;
            }

            NyuukinsakiIchiran.Properties.Settings.Default.Save();

            this.form.customSearchHeader1.ClearCustomSearchSetting();
            this.form.customSortHeader1.ClearCustomSortSetting();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // パターン未登録の場合検索処理を行わない
            if (this.form.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
            
            //読込データ件数を取得
            this.form.headerForm.ReadDataNumber.Text = this.Search().ToString();

            if (this.form.customDataGridView1 != null)
            {
                this.form.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.form.headerForm.ReadDataNumber.Text = "0";
            }

            if (this.form.headerForm.ReadDataNumber.Text == "0")
            {
                MessageBoxUtility.MessageBoxShow("C001");
            }

            NyuukinsakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.form.headerForm.AlertNumber.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_FURIGANA_TEXT = this.form.Nyuukinsaki_FURIGANA.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME1_TEXT = this.form.Nyuukinsaki_NAME1.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME2_TEXT = this.form.Nyuukinsaki_NAME2.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME_RYAKU_TEXT = this.form.Nyuukinsaki_NAME_RYAKU.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_TODOUFUKEN_CD_TEXT = this.form.Nyuukinsaki_TODOUFUKEN_CD.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_ADDRESS_TEXT = this.form.Nyuukinsaki_ADDRESS.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// 20150824 katen #11992 「並び順」の項目があるにもかかわらず、[F10]並び替えボタンがない。 start
        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }
        /// 20150824 katen #11992 「並び順」の項目があるにもかかわらず、[F10]並び替えボタンがない。 end

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.form.headerForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.form.headerForm.ReadDataNumber.Text = "0";
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

            NyuukinsakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.form.headerForm.AlertNumber.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_FURIGANA_TEXT = this.form.Nyuukinsaki_FURIGANA.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME1_TEXT = this.form.Nyuukinsaki_NAME1.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME2_TEXT = this.form.Nyuukinsaki_NAME2.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME_RYAKU_TEXT = this.form.Nyuukinsaki_NAME_RYAKU.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_TODOUFUKEN_CD_TEXT = this.form.Nyuukinsaki_TODOUFUKEN_CD.Text;
            NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_ADDRESS_TEXT = this.form.Nyuukinsaki_ADDRESS.Text;
            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

            NyuukinsakiIchiran.Properties.Settings.Default.Save();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

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
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //var us = new KensakuJoukenSetteiForm(this.form.DenshuKbn);
            //us.Show();

            LogUtility.DebugMethodEnd(sender, e);

        }

        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if ("1".Equals(parentForm.txb_process.Text))
            {
                //パターン一覧画面へ遷移
                this.bt_process1_Click(sender, e);
            }
            else if ("2".Equals(parentForm.txb_process.Text))
            {
                //検索条件設定画面へ遷移
                this.bt_process2_Click(sender, e);
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();
            }

            LogUtility.DebugMethodEnd(sender, e);
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

        #region ヘッダーの初期化

        private void InitHeaderArea()
        {
            // タイトル設定
            this.form.Text = this.form.DenshuKbn.ToTitleString();
            this.form.headerForm.lb_title.Text = this.form.DenshuKbn.ToTitleString();

            // 初期値設定
            this.form.Nyuukinsaki_FURIGANA.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_FURIGANA_TEXT;
            this.form.Nyuukinsaki_NAME1.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME1_TEXT;
            this.form.Nyuukinsaki_NAME2.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME2_TEXT;
            this.form.Nyuukinsaki_NAME_RYAKU.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_NAME_RYAKU_TEXT;
            this.form.Nyuukinsaki_TODOUFUKEN_CD.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_TODOUFUKEN_CD_TEXT;
            this.form.Nyuukinsaki_ADDRESS.Text = NyuukinsakiIchiran.Properties.Settings.Default.Nyuukinsaki_ADDRESS_TEXT;

            ////拠点名をセット
            //if (!string.IsNullOrEmpty(this.form.headerForm.HEADER_KYOTEN_CD.Text))
            //{
            //    this.kyotenEntity = daoKyoten.GetDataByCd(this.form.headerForm.HEADER_KYOTEN_CD.Text);
            //    this.form.headerForm.HEADER_KYOTEN_NAME.Text = kyotenEntity.KYOTEN_NAME_RYAKU;
            //}

            //都道府県をセット
            if (!string.IsNullOrEmpty(this.form.Nyuukinsaki_TODOUFUKEN_CD.Text))
            {
                this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.form.Nyuukinsaki_TODOUFUKEN_CD.Text);
                this.form.Nyuukinsaki_TODOUFUKEN_NAME.Text = this.todoufukenEntity == null ? string.Empty : this.todoufukenEntity.TODOUFUKEN_NAME_RYAKU;
            }

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.form.headerForm.AlertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }

            //アラートの保存データがあればそちらを表示
            if (NyuukinsakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
            {
                this.form.headerForm.AlertNumber.Text = NyuukinsakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
            }
        }

        #endregion

        #region 入金先入力画面起動処理

        /// <summary>
        /// 入金先入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg);

            // 引数へのオブジェクトを作成する
            // 新規の場合は引数なし、ただし参照の場合は引数あり
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                r_framework.FormManager.FormManager.OpenFormWithAuth("M209", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 表示されている行の入金先CDを取得する
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                //20150615 #1674 minhhoang start
                //return when current eRow is null
                if (row == null)
                {
                    MessageBoxUtility.MessageBoxShow("E051", "対象データ");
                    LogUtility.DebugMethodEnd(windowType, newFlg);
                    return;
                }
                //20150615 #1674 minhhoang end
                string cd1 = row.Cells[this.KEY_ID1].Value.ToString();
                var entTorihikisaki = this.daoTorihikisaki.GetDataByCd(cd1);

                // データ削除チェックを行う
                if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    M_NYUUKINSAKI data = this.daoNyuukinsaki.GetDataByCd(cd1);
                    if (data.DELETE_FLG)
                    {
                        if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            if (entTorihikisaki != null)
                            {
                                // 自動的に作成された（取引先CDに同CDが存在する）入金先データ
                                // は一覧から編集不可とする
                                MessageBoxUtility.MessageBoxShow("E142", "該当の入金先", "取引先作成時");
                                LogUtility.DebugMethodEnd();
                                return;
                            }
                            else
                            {
                                // 削除されている明細を一覧から修正実行されたときは復活をさせるかさせないかの選択ダイアログを表示
                                // 「はい」を選択した場合は修正モードで表示を行い、登録することにより削除フラグを外す。
                                var result = MessageBoxUtility.MessageBoxShow("C057");
                                if (result != DialogResult.Yes)
                                {
                                    LogUtility.DebugMethodEnd();
                                    return;
                                }
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

                // 自動的に作成された（取引先CDに同CDが存在する）入金先データ
                // は一覧から削除不可とする
                if (windowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    if (entTorihikisaki != null)
                    {
                        MessageBoxUtility.MessageBoxShow("E110", "該当の入金先", "取引先作成時");
                        LogUtility.DebugMethodEnd();
                        return;
                    }
                }

                // 権限チェック
                // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                    !r_framework.Authority.Manager.CheckAuthority("M209", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (!r_framework.Authority.Manager.CheckAuthority("M209", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                        return;
                    }

                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }

                r_framework.FormManager.FormManager.OpenFormWithAuth("M209", windowType, windowType, cd1);
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
            int.TryParse(this.form.headerForm.AlertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            return result;
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int res = -1;

            try
            {
                if (!string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    // 検索文字列取得
                    var sql = this.GetSearchString();

                    // 検索
                    this.form.Table = daoNyuukinsaki.GetDateForStringSql(sql);
                    this.form.ShowData();

                    // 件数を返す
                    res = this.form.Table.Rows.Count;
                }

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
            var result = new StringBuilder();
            string strTemp;
            bool isDetail = this.form.CurrentPatternOutputKbn == (int)OUTPUT_KBN.MEISAI;

            #region SELECT句

            result.Append(" SELECT DISTINCT ");
            result.Append(this.form.SelectQuery);
            result.AppendFormat(" , M_NYUUKINSAKI.NYUUKINSAKI_CD AS {0}", this.KEY_ID1);
            if (isDetail)
            {
                result.AppendFormat(" , M_NYUUKINSAKI_FURIKOMI.FURIKOMI_SEQ AS {0}", this.KEY_ID2);
            }

            #endregion

            #region FROM句

            result.Append(" FROM M_NYUUKINSAKI ");
            if (isDetail)
            {
                result.Append(" LEFT JOIN M_NYUUKINSAKI_FURIKOMI ");
                result.Append(" ON M_NYUUKINSAKI.NYUUKINSAKI_CD = M_NYUUKINSAKI_FURIKOMI.NYUUKINSAKI_CD ");
            }

            result.Append(this.form.JoinQuery);

            #endregion

            #region WHERE句

            result.Append(" WHERE 1=1 ");

            // フリガナ
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.Nyuukinsaki_FURIGANA.Text);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_NYUUKINSAKI.NYUUKINSAKI_FURIGANA LIKE '%{0}%'", strTemp);
            }

            // 入金先名1
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.Nyuukinsaki_NAME1.Text);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_NYUUKINSAKI.NYUUKINSAKI_NAME1 LIKE '%{0}%'", strTemp);
            }

            // 入金先名2
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.Nyuukinsaki_NAME2.Text);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_NYUUKINSAKI.NYUUKINSAKI_NAME2 LIKE '%{0}%'", strTemp);
            }

            // 略称名
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.Nyuukinsaki_NAME_RYAKU.Text);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_NYUUKINSAKI.NYUUKINSAKI_NAME_RYAKU LIKE '%{0}%'", strTemp);
            }

            // 都道府県CD
            strTemp = this.form.Nyuukinsaki_TODOUFUKEN_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_NYUUKINSAKI.NYUUKINSAKI_TODOUFUKEN_CD = {0}", strTemp);
            }

            // 住所
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(this.form.Nyuukinsaki_ADDRESS.Text);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" (M_NYUUKINSAKI.NYUUKINSAKI_ADDRESS1 + M_NYUUKINSAKI.NYUUKINSAKI_ADDRESS2) LIKE '%{0}%'", strTemp);
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" M_NYUUKINSAKI.DELETE_FLG = 0");
            }
            
            #endregion

            #region OEDREBY句

            if (!string.IsNullOrEmpty(this.form.OrderByQuery))
            {
                result.Append(" ORDER BY ");
                result.Append(this.form.OrderByQuery);
            }

            #endregion

            return result.ToString();
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
        /// 20141203 Houkakou 「入金先一覧」の日付チェックを追加する　end
    }
}
