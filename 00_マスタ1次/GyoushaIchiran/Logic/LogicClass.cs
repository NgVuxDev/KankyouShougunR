// $Id: LogicClass.cs 54103 2015-06-30 08:35:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GyoushaIchiran.APP;
using GyoushaIchiran.Const;
using r_framework.APP.Base;
using r_framework.APP.Base.IchiranHeader;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using System.Collections.Generic;
using r_framework.Configuration;

namespace GyoushaIchiran.Logic
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
        private readonly string ButtonInfoXmlPath = "GyoushaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private GyoushaIchiranForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        private IchiranHeaderForm1 headerForm;

        /// <summary>
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム設定マスタのエンティティ
        /// </summary>
        private M_SYS_INFO sysinfoEntity;

        /// <summary>
        /// 拠点マスタのエンティティ
        /// </summary>
        private M_KYOTEN kyotenEntity;

        /// <summary>
        /// 取引先マスタのエンティティ
        /// </summary>
        private M_TORIHIKISAKI torihikisakiEntity;

        /// <summary>
        /// 業者マスタのエンティティ
        /// </summary>
        private M_GYOUSHA gyoushaEntity;

        /// <summary>
        /// 都道府県マスタのエンティティ
        /// </summary>
        private M_TODOUFUKEN todoufukenEntity;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao daoKyoten;

        /// <summary>
        /// 都道府県のDao
        /// </summary>
        private IM_TODOUFUKENDao daoTodoufuken;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        private bool SpaceChk = false;
        private bool SpaceON = false;

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(GyoushaIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.headerForm = (IchiranHeaderForm1)((IchiranBaseForm)targetForm.ParentForm).headerForm;
            this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();

            // ヘッダーの拠点を非表示にする
            this.headerForm.lbl_読込日時.Visible = false;
            this.headerForm.HEADER_KYOTEN_CD.Visible = false;
            this.headerForm.HEADER_KYOTEN_NAME.Visible = false;
            this.headerForm.HEADER_KYOTEN_CD.Text = "99";

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
                if (!this.form.EventSetFlg)
                {
                    // イベントの初期化処理
                    this.EventInit();
                    this.form.EventSetFlg = true;
                }
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;   // 行の追加オプション(false)

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    var parentForm = (IchiranBaseForm)this.form.Parent;

                    // mapbox用ボタン無効化
                    parentForm.bt_process3.Text = string.Empty;
                    parentForm.bt_process3.Enabled = false;
                }
                else
                {
                    // 一覧内のチェックボックスの設定
                    this.HeaderCheckBoxSupport();
                }

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                this.InitHeaderArea();

                // 表示条件初期化
                this.RemoveIchiranHyoujiJoukenEvent();
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
                this.AddIchiranHyoujiJoukenEvent();
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
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
            parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);      // 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);      // 修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);      // 削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);      // 複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);      // CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);      // 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);      // 検索
            parentForm.bt_func9.Click += new System.EventHandler(this.bt_func9_Click);      // 現場メモ登録
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);    // 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);    // F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);    // 閉じる
            parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);       // パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);       // 現場メモ一覧画面へ遷移
            parentForm.bt_process3.Click += new EventHandler(this.bt_process3_Click);       // 地図を表示

            //明細ダブルクリック時のイベント
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);

            // 20141201 teikyou ダブルクリックを追加する　start
            this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
            // 20141201 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　start
            this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(TEKIYOU_BEGIN_Leave);
            this.form.TEKIYOU_END.Leave += new System.EventHandler(TEKIYOU_END_Leave);
            /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　start

            // チェックボックスの挙動制御
            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.DetailCellClick);
            this.form.customDataGridView1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.DetailPreviewKeyDown);

            //表示条件イベント生成
            this.AddIchiranHyoujiJoukenEvent();

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
            this.headerForm.HEADER_KYOTEN_CD.Clear();
            this.headerForm.HEADER_KYOTEN_NAME.Clear();
            this.form.TORIHIKISAKI_CD.Clear();
            this.form.TORIHIKISAKI_RNAME.Clear();
            this.form.GYOUSHA_CD.Clear();
            this.form.GYOUSHA_RNAME.Clear();
            this.form.TORIHIKISAKI_NAME_RYAKU.Clear();
            this.form.GYOUSHA_FURIGANA.Clear();
            this.form.GYOUSHA_NAME1.Clear();
            this.form.GYOUSHA_NAME2.Clear();
            this.form.GYOUSHA_NAME_RYAKU.Clear();
            this.form.GYOUSHA_TODOUFUKEN_CD.Clear();
            this.form.TODOUFUKEN_NAME.Clear();
            this.form.GYOUSHA_ADDRESS.Clear();
            this.form.TEKIYOU_BEGIN.Clear();
            this.form.TEKIYOU_END.Clear();
            //20150413 minhhoang edit #1748
            this.SetHyoujiJoukenInit();
            //20150413 minhhoang end edit #1748
            var ds = (DataTable)this.form.customDataGridView1.DataSource;
            if (ds != null)
            {
                ds.Clear();
                this.form.customDataGridView1.DataSource = ds;
            }

            GyoushaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
            GyoushaIchiran.Properties.Settings.Default.Save();

            this.form.customSearchHeader1.ClearCustomSearchSetting();
            this.form.customSortHeader1.ClearCustomSortSetting();

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
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　end

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
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }

                this.SaveHyoujiJoukenDefault();

                this.HeaderCheckBoxFalse();
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
        /// F9 現場メモ登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                // 明細が削除済みの場合は、エラーとする。
                string cd1 = row.Cells[GyoushaIchiranConstans.KEY_ID1].Value.ToString();
                M_GYOUSHA data = this.daoGyousha.GetDataByCd(cd1);
                if (data.DELETE_FLG)
                {
                    msgLogic.MessageBoxShowError("削除済みの為、登録を行う事は出来ません。");
                    return;
                }

                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.HASSEIMOTO_CD = "1";
                paramEntry.HASSEIMOTO_NAME = "発生元無し";
                paramEntry.GYOUSHA_CD = data.GYOUSHA_CD;
                paramEntry.GYOUSHA_NAME = data.GYOUSHA_NAME_RYAKU;

                WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                FormManager.OpenFormWithAuth("G741", windowType, windowType, string.Empty, string.Empty, paramEntry, WINDOW_ID.M_GYOUSHA_ICHIRAN.ToString());

            }
            else
            {
                // 明細が未選択の場合は、エラーとする。
                msgLogic.MessageBoxShow("E051", "対象データ");
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
        }

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

            this.SaveHyoujiJoukenDefault();

            var parentForm = (IchiranBaseForm)this.form.Parent;
            if (parentForm != null)
            {
                parentForm.Close();
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
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
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
                LogUtility.Fatal("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        
        /// <summary>
        /// 現場メモ一覧画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row != null)
            {
                string cd1 = row.Cells[GyoushaIchiranConstans.KEY_ID1].Value.ToString();
                M_GYOUSHA data = this.daoGyousha.GetDataByCd(cd1);

                // パラメータを設定する。
                T_GENBAMEMO_ENTRY paramEntry = new T_GENBAMEMO_ENTRY();
                paramEntry.GYOUSHA_CD = data.GYOUSHA_CD;
                paramEntry.GYOUSHA_NAME = data.GYOUSHA_NAME_RYAKU;

                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, paramEntry, WINDOW_ID.M_GYOUSHA_ICHIRAN.ToString());

            }
            else
            {
                // 明細が未選択の場合は、画面遷移のみとする。
                FormManager.OpenFormWithAuth("G742", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process3_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 件数チェック
            if (!this.CheckForCheckBox())
            {
                return;
            }

            if (this.form.errmessage.MessageBoxShowConfirm("地図を表示しますか？") == DialogResult.No)
            {
                return;
            }

            // 出力処理
            MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

            // 地図に渡すDTO作成
            List<mapDtoList> dtos = new List<mapDtoList>();
            dtos = this.createMapboxDto();
            if (dtos == null)
            {
                return;
            }

            // 地図表示
            gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.M_GYOUSHA_ICHIRAN);

            LogUtility.DebugMethodEnd();
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
            // チェックメソッド設定
            this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField = this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField;
            this.headerForm.HEADER_KYOTEN_CD.SetFormField = this.headerForm.HEADER_KYOTEN_CD.PopupSetFormField;
            SelectCheckDto dto = new SelectCheckDto();
            dto.CheckMethodName = "拠点マスタコードチェックandセッティング";
            this.headerForm.HEADER_KYOTEN_CD.FocusOutCheckMethod.Add(dto);

            // ポップアップ設定
            this.headerForm.HEADER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";

            // 初期値設定
            //this.headerForm.HEADER_KYOTEN_CD.Text = GyoushaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT;
            this.form.TORIHIKISAKI_CD.Text = GyoushaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
            this.form.GYOUSHA_CD.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = GyoushaIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT;
            this.form.GYOUSHA_FURIGANA.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_FURIGANA_TEXT;
            this.form.GYOUSHA_NAME1.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME1_TEXT;
            this.form.GYOUSHA_NAME2.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME2_TEXT;
            this.form.GYOUSHA_NAME_RYAKU.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;
            this.form.GYOUSHA_TODOUFUKEN_CD.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_TODOUFUKEN_CD_TEXT;
            this.form.GYOUSHA_ADDRESS.Text = GyoushaIchiran.Properties.Settings.Default.GYOUSHA_ADDRESS_TEXT;
            if (GyoushaIchiran.Properties.Settings.Default["TEKIYOU_BEGIN_VALUE"] == null)
            {
                this.form.TEKIYOU_BEGIN.Value = null;
            }
            else if ((DateTime)GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE != new DateTime(0))
            {
                this.form.TEKIYOU_BEGIN.Value = GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE;
            }
            if (GyoushaIchiran.Properties.Settings.Default["TEKIYOU_END_VALUE"] == null)
            {
                this.form.TEKIYOU_END.Value = null;
            }
            else if ((DateTime)GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE != new DateTime(0))
            {
                this.form.TEKIYOU_END.Value = GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE;
            }

            ////拠点名をセット
            //if (!string.IsNullOrEmpty(this.headerForm.HEADER_KYOTEN_CD.Text))
            //{
            //    this.kyotenEntity = daoKyoten.GetDataByCd(this.headerForm.HEADER_KYOTEN_CD.Text);
            //    this.headerForm.HEADER_KYOTEN_NAME.Text = this.kyotenEntity == null ? string.Empty : kyotenEntity.KYOTEN_NAME_RYAKU;
            //}

            //取引先名をセット
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                this.form.TORIHIKISAKI_RNAME.Text = this.torihikisakiEntity == null ? string.Empty : torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
            }

            //業者名をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.gyoushaEntity = daoGyousha.GetDataByCd(this.form.GYOUSHA_CD.Text);
                this.form.GYOUSHA_RNAME.Text = this.gyoushaEntity == null ? string.Empty : gyoushaEntity.GYOUSHA_NAME_RYAKU;
            }

            //都道府県をセット
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_TODOUFUKEN_CD.Text))
            {
                this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.form.GYOUSHA_TODOUFUKEN_CD.Text);
                this.form.TODOUFUKEN_NAME.Text = this.todoufukenEntity == null ? string.Empty : this.todoufukenEntity.TODOUFUKEN_NAME_RYAKU;
            }

            //アラート件数の初期値セット
            if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
            }

            //アラートの保存データがあればそちらを表示
            if (GyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
            {
                this.headerForm.alertNumber.Text = GyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
            }
        }

        #endregion

        #region 業者入力画面起動処理

        /// <summary>
        /// 業者入力画面の呼び出し
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
                r_framework.FormManager.FormManager.OpenFormWithAuth("M215", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 表示されている行の取引先CDを取得する
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)    // No.3988
                {
                    string cd1 = row.Cells[GyoushaIchiranConstans.KEY_ID1].Value.ToString();
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    // データ削除チェックを行う
                    if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        M_GYOUSHA data = this.daoGyousha.GetDataByCd(cd1);
                        if (data.DELETE_FLG)
                        {
                            // 削除されている明細を一覧から修正実行されたときは復活をさせるかさせないかの選択ダイアログを表示
                            // 「はい」を選択した場合は修正モードで表示を行い、登録することにより削除フラグを外す。
                            if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                            {
                                var result = msgLogic.MessageBoxShow("C057");
                                if (result != DialogResult.Yes)
                                {
                                    LogUtility.DebugMethodEnd();
                                    return;
                                }
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E026", "コード");
                                LogUtility.DebugMethodEnd();
                                return;
                            }
                        }
                    }

                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                        !r_framework.Authority.Manager.CheckAuthority("M215", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("M215", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            msgLogic.MessageBoxShow("E158", "修正");
                            return;
                        }

                        windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }

                    r_framework.FormManager.FormManager.OpenFormWithAuth("M215", windowType, windowType, cd1);
                }
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
            int.TryParse(this.headerForm.alertNumber.Text, System.Globalization.NumberStyles.AllowThousands, null, out result);
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
            this.form.Table = null;

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                // 検索文字列の作成
                var sql = this.MakeSearchCondition();
                this.form.Table = this.daoGyousha.GetDateForStringSql(sql);
            }

            this.form.ShowData();

            return this.form.Table != null ? this.form.Table.Rows.Count : 0;
        }

        /// <summary>
        /// 検索文字列を作成
        /// </summary>
        private string MakeSearchCondition()
        {
            var selectQuery = this.CreateSelectQuery();
            var fromQuery = this.CreateFromQuery();
            var whereQuery = this.CreateWhereQuery();
            var orderByQuery = this.CreateOrderByQuery();

            return selectQuery + fromQuery + whereQuery + orderByQuery;
        }

        /// <summary>
        /// Select句作成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectQuery()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(this.form.SelectQuery))
            {
                sb.Append("SELECT DISTINCT ");
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_CD AS {0}, ", GyoushaIchiranConstans.KEY_ID1);
                // 以下、mapbox連携で利用する項目
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_LATITUDE AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_LONGITUDE AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_LONGITUDE);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_NAME_RYAKU AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_NAME_RYAKU);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_ADDRESS1 AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS1);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_ADDRESS2 AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS2);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_POST AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_POST);
                sb.AppendFormat(" M_GYOUSHA.GYOUSHA_TEL AS {0}, ", GyoushaIchiranConstans.HIDDEN_GYOUSHA_TEL);
                sb.AppendFormat(" M_GYOUSHA.BIKOU1 AS {0}, ", GyoushaIchiranConstans.HIDDEN_BIKOU1);
                sb.AppendFormat(" M_GYOUSHA.BIKOU2 AS {0}, ", GyoushaIchiranConstans.HIDDEN_BIKOU2);
                sb.AppendFormat(" M_TODOUFUKEN.TODOUFUKEN_NAME AS {0}, ", GyoushaIchiranConstans.HIDDEN_TODOUFUKEN_NAME);
                sb.Append(this.form.SelectQuery);
            }

            return sb.ToString();
        }

        /// <summary>
        /// From句作成
        /// </summary>
        /// <returns></returns>
        private string CreateFromQuery()
        {
            var sb = new StringBuilder();

            // 業者マスタ
            sb.Append(" FROM M_GYOUSHA ");

            // 取引先マスタ
            sb.Append(" LEFT JOIN M_TORIHIKISAKI ");
            sb.Append(" ON M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");

            // 都道府県マスタ
            sb.Append(" LEFT JOIN M_TODOUFUKEN ");
            sb.Append(" ON M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = M_TODOUFUKEN.TODOUFUKEN_CD ");

            // 現場メモEntry
            sb.Append(" LEFT JOIN T_GENBAMEMO_ENTRY ");
            sb.Append(" ON M_GYOUSHA.GYOUSHA_CD = T_GENBAMEMO_ENTRY.GYOUSHA_CD ");

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }

        /// <summary>
        /// Where句作成
        /// </summary>
        /// <returns></returns>
        private string CreateWhereQuery()
        {
            var result = new StringBuilder();
            string strTemp;

            // 拠点CD
            strTemp = this.headerForm.HEADER_KYOTEN_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                // 拠点で全社が指定された場合は全データ検索
                // 上記以外の場合は指定された拠点のみ検索
                if (!strTemp.Equals(GyoushaIchiranConstans.KYOTEN_CD_ALL))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_GYOUSHA.KYOTEN_CD = {0}", strTemp);
                }
            }

            // 取引先CD
            strTemp = this.form.TORIHIKISAKI_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.TORIHIKISAKI_CD = '{0}'", strTemp);
            }

            // 業者CD
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_CD = '{0}'", strTemp);
            }

            // 取引先略称
            strTemp = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%'", strTemp);
            }

            // フリガナ
            strTemp = this.form.GYOUSHA_FURIGANA.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%{0}%'", strTemp);
            }

            // 業者名1
            strTemp = this.form.GYOUSHA_NAME1.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_NAME1 LIKE '%{0}%'", strTemp);
            }

            // 業者名2
            strTemp = this.form.GYOUSHA_NAME2.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_NAME2 LIKE '%{0}%'", strTemp);
            }

            // 略称名
            strTemp = this.form.GYOUSHA_NAME_RYAKU.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_NAME_RYAKU LIKE '%{0}%'", strTemp);
            }

            // 都道府県CD
            strTemp = this.form.GYOUSHA_TODOUFUKEN_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.GYOUSHA_TODOUFUKEN_CD = {0}", strTemp);
            }

            // 住所
            strTemp = this.form.GYOUSHA_ADDRESS.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" (M_GYOUSHA.GYOUSHA_ADDRESS1 + M_GYOUSHA.GYOUSHA_ADDRESS2) LIKE '%{0}%'", strTemp);
            }

            // 適用開始
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
            {
                strTemp = this.form.TEKIYOU_BEGIN.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (M_GYOUSHA.TEKIYOU_END >= CONVERT(date,'{0}') OR M_GYOUSHA.TEKIYOU_END IS NULL)", strTemp);
            }

            // 適用終了
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
            {
                strTemp = this.form.TEKIYOU_END.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (M_GYOUSHA.TEKIYOU_END <= CONVERT(date,'{0}') OR M_GYOUSHA.TEKIYOU_END IS NULL)", strTemp);
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" M_GYOUSHA.DELETE_FLG = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" (1 = 0");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked)
            {
                result.Append(" OR (((M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_GYOUSHA.TEKIYOU_END) or (M_GYOUSHA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and M_GYOUSHA.TEKIYOU_END IS NULL) or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_GYOUSHA.TEKIYOU_END) or (M_GYOUSHA.TEKIYOU_BEGIN IS NULL and M_GYOUSHA.TEKIYOU_END IS NULL)) and M_GYOUSHA.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                result.Append(" OR M_GYOUSHA.DELETE_FLG = 1");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(" OR ((M_GYOUSHA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > M_GYOUSHA.TEKIYOU_END) and M_GYOUSHA.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(")");
            }

            return result.Length > 0 ? result.Insert(0, " WHERE ").ToString() : string.Empty;
        }

        /// <summary>
        /// OrderBy句作成
        /// </summary>
        /// <returns></returns>
        private string CreateOrderByQuery()
        {
            var query = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.form.OrderByQuery))
            {
                query += " ORDER BY " + this.form.OrderByQuery;
            }

            return query;
        }

        #endregion

        #region 表示条件

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            // 一覧表示イベントの削除
            this.RemoveIchiranHyoujiJoukenEvent();

            if (this.sysinfoEntity != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.sysinfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            // 一覧表示イベントの追加
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面の表示条件を保存します
        /// </summary>
        public bool SaveHyoujiJoukenDefault()
        {
            try
            {
                GyoushaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                GyoushaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                GyoushaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                GyoushaIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_FURIGANA_TEXT = this.form.GYOUSHA_FURIGANA.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME1_TEXT = this.form.GYOUSHA_NAME1.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME2_TEXT = this.form.GYOUSHA_NAME2.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_NAME_RYAKU.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_TODOUFUKEN_CD_TEXT = this.form.GYOUSHA_TODOUFUKEN_CD.Text;
                GyoushaIchiran.Properties.Settings.Default.GYOUSHA_ADDRESS_TEXT = this.form.GYOUSHA_ADDRESS.Text;
                // VUNGUYEN 2015/04/14 MOD #3940 START
                //DateTime? dtTemp;
                //dtTemp = this.form.TEKIYOU_BEGIN.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                //}
                //dtTemp = this.form.TEKIYOU_END.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                //}
                DateTime dtTemp;
                if (DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out dtTemp))
                {
                    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp;
                }
                else
                {
                    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }

                if (DateTime.TryParse(this.form.TEKIYOU_END.Text, out dtTemp))
                {
                    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp;
                }
                else
                {
                    GyoushaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                }
                // VUNGUYEN 2015/04/14 MOD #3940 END
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                GyoushaIchiran.Properties.Settings.Default.Save();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaveHyoujiJoukenDefault", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 一覧表示イベントの削除
        /// </summary>
        public void RemoveIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// 一覧表示イベントの追加
        /// </summary>
        public void AddIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        private void TEKIYOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var tekiyouBeginTextBox = this.form.TEKIYOU_BEGIN;
            var tekiyouEndTextBox = this.form.TEKIYOU_END;
            tekiyouEndTextBox.Text = tekiyouBeginTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141201 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TEKIYOU_END.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.TEKIYOU_BEGIN.Text);
            DateTime date_to = DateTime.Parse(this.form.TEKIYOU_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.TEKIYOU_BEGIN.IsInputErrorOccured = true;
                this.form.TEKIYOU_END.IsInputErrorOccured = true;
                this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "適用期間From", "適用期間To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.TEKIYOU_BEGIN.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region TEKIYOU_BEGIN_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TEKIYOU_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TEKIYOU_END.Text))
            {
                this.form.TEKIYOU_END.IsInputErrorOccured = false;
                this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region TEKIYOU_END_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void TEKIYOU_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
            {
                this.form.TEKIYOU_BEGIN.IsInputErrorOccured = false;
                this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「業者一覧」の日付チェックを追加する　end

        #region 明細ヘッダーにチェックボックスを追加

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();

            if (!this.form.customDataGridView1.Columns.Contains("DATA_TAISHO"))
            {
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = "DATA_TAISHO";
                    newColumn.HeaderText = "地図";
                    newColumn.DataPropertyName = "TAISHO";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "地図   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    newColumn.ReadOnly = false;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 明細ヘッダーのチェックボックス解除

        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        private void HeaderCheckBoxFalse()
        {
            if (this.form.customDataGridView1.Columns.Contains("DATA_TAISHO"))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns["DATA_TAISHO"].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }

        #endregion

        #region mapbox連携

        #region DTO作成

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // 業者一覧は1レイヤーとする
                mapDtoList dtoList = new mapDtoList();
                int layerId = 1;
                dtoList.layerId = layerId;

                List<mapDto> dtos = new List<mapDto>();

                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    // チェックなしデータを排除する
                    if (row.Cells[GyoushaIchiranConstans.DATA_TAISHO].Value == null) continue;
                    if ((bool)row.Cells[GyoushaIchiranConstans.DATA_TAISHO].Value == false) continue;

                    // 緯度経度なしデータを排除する
                    if (string.IsNullOrEmpty(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE].Value.ToString())) continue;

                    mapDto dto = new mapDto();
                    dto.id = layerId;
                    dto.layerNo = layerId;
                    dto.torihikisakiCd = string.Empty;
                    dto.torihikisakiName = string.Empty;
                    dto.gyoushaCd = Convert.ToString(row.Cells[GyoushaIchiranConstans.KEY_ID1].Value);
                    dto.gyoushaName = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_NAME_RYAKU].Value);
                    dto.genbaCd = string.Empty;
                    dto.genbaName = string.Empty;
                    dto.post = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_POST].Value);
                    dto.address = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_TODOUFUKEN_NAME].Value)
                                + Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS1].Value)
                                + Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_ADDRESS2].Value);
                    dto.tel = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_TEL].Value);
                    dto.bikou1 = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_BIKOU1].Value);
                    dto.bikou2 = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_BIKOU2].Value);
                    dto.latitude = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE].Value);
                    dto.longitude = Convert.ToString(row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LONGITUDE].Value);
                    dtos.Add(dto);
                    dtoList.dtos = dtos;
                }
                if (dtoList.dtos != null && dtoList.dtos.Count != 0)
                {
                    dtoLists.Add(dtoList);
                }

                return dtoLists;
            }
            catch (Exception ex)
            {
                this.form.errmessage.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        #region チェック

        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox()
        {
            bool ret = false;

            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[GyoushaIchiranConstans.DATA_TAISHO].Value != null)
                {
                    ret = bool.Parse(row.Cells[GyoushaIchiranConstans.DATA_TAISHO].Value.ToString());
                    if (ret)
                    {
                        return ret;
                    }
                }
            }
            if (!ret)
            {
                this.form.errmessage.MessageBoxShowError("地図表示対象の明細がありません");
            }

            return ret;
        }

        #endregion

        #region 明細チェックボックスクリック処理

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0)
            {
                return;
            }

            //スペースで、OFFの場合は抜ける
            if (this.SpaceChk && !this.SpaceON)
            {
                return;
            }
            this.SpaceON = false;

            if (this.form.customDataGridView1.CurrentCell.Value == null)
            {
                // falseと同義なのでチェックを行う
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)
                {
                    string lat = row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE].Value.ToString();
                    string lon = row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LONGITUDE].Value.ToString();
                    if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
                    {
                        this.form.errmessage.MessageBoxShowError("緯度/経度が入力されていません");
                        // クリックイベントの時点での値とは反対の値がセットされる使用のため、
                        // キャンセルさせたい場合はここでtrueをセットしておく
                        if (!this.SpaceChk)
                        {
                            this.form.customDataGridView1.CurrentCell.Value = true;
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[0, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[0, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[0, e.RowIndex].Value = !(bool)this.form.customDataGridView1[0, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }
                }
            }
            else if (this.form.customDataGridView1.CurrentCell.Value.Equals(false))
            {
                // チェックを行う
                // falseと同義なのでチェックを行う
                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row != null)
                {
                    string lat = row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LATITUDE].Value.ToString();
                    string lon = row.Cells[GyoushaIchiranConstans.HIDDEN_GYOUSHA_LONGITUDE].Value.ToString();
                    if (string.IsNullOrEmpty(lat) || string.IsNullOrEmpty(lon))
                    {
                        this.form.errmessage.MessageBoxShowError("緯度/経度が入力されていません");
                        // クリックイベントの時点での値とは反対の値がセットされる使用のため、
                        // キャンセルさせたい場合はここでtrueをセットしておく
                        if (!this.SpaceChk)
                        {
                            this.form.customDataGridView1.CurrentCell.Value = true;
                        }
                        this.SpaceChk = false;
                        return;
                    }
                    if (this.SpaceChk)
                    {
                        if (this.form.customDataGridView1[0, e.RowIndex].Value == null)
                        {
                            this.form.customDataGridView1[0, e.RowIndex].Value = true;
                        }
                        else
                        {
                            this.form.customDataGridView1[0, e.RowIndex].Value = !(bool)this.form.customDataGridView1[0, e.RowIndex].Value;
                        }
                        this.SpaceChk = false;
                    }
                }
            }
            else
            {
                // チェックを外した場合なので何もしない
            }
        }

        #endregion

        #region 明細チェックボックスのスペースキー押下時の制御

        /// <summary>
        /// [地図]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.form.customDataGridView1.CurrentCell;

                if (curCell.RowIndex < 0 || curCell.ColumnIndex != 0)
                {
                    return;
                }

                this.SpaceChk = true;
                this.SpaceON = false;
                //[地図]OFFにする場合は、何もしない。
                //[地図]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                if (this.form.customDataGridView1[0, curCell.RowIndex].Value == null)
                {
                    this.SpaceON = true;
                    this.form.customDataGridView1[0, curCell.RowIndex].Value = true;
                }
                else
                {
                    if (!(bool)this.form.customDataGridView1[0, curCell.RowIndex].Value)
                    {
                        this.SpaceON = true;
                        this.form.customDataGridView1[0, curCell.RowIndex].Value = !(bool)this.form.customDataGridView1[0, curCell.RowIndex].Value;
                    }
                }
                this.form.customDataGridView1.Refresh();

            }
        }

        #endregion

        #endregion

        ///<summary>
        ///取引先CDの削除済みチェック
        ///</summary>
        public DialogResult checkDelTorihikisaki(string cd)
        {
            DialogResult result = DialogResult.None;
            var target = this.daoTorihikisaki.GetTorihikisakiData(cd);
            if (target != null)
            {
                var isDel = target.DELETE_FLG;
                if (isDel)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    result = msgLogic.MessageBoxShow("C121", "取引先");
                }
            }
                return result;
        }

        ///<summary>
        ///業者CDの削除済みチェック
        ///</summary>
        public DialogResult checkDelGyousha(string cd)
        {
            DialogResult result = DialogResult.None;
            var target = this.daoGyousha.GetDataByCd(cd);
            if (target != null)
            {
                var isDel = target.DELETE_FLG;
                if (isDel)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    result = msgLogic.MessageBoxShow("C121", "業者");
                }
            }

            return result;
        }
    }
}
