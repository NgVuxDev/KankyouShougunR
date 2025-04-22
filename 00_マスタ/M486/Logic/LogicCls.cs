// $Id: LogicCls.cs 54103 2015-06-30 08:35:46Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Master.HikiaiGenbaIchiran.APP;
using Shougun.Core.Master.HikiaiGenbaIchiran.Const;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.Master.HikiaiGenbaIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.HikiaiGenbaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面連携に使用するキー取得項目名
        /// </summary>
        internal readonly string KEY_FLG = "HIDDEN_HIKIAI_KIZON_FLG";

        /// <summary>
        /// 画面連携に使用するキー取得項目名1
        /// </summary>
        internal readonly string KEY_ID1 = "HIDDEN_GYOUSHA_CD";

        /// <summary>
        /// 画面連携に使用するキー取得項目名2
        /// </summary>
        internal readonly string KEY_ID2 = "HIDDEN_GENBA_CD";

        /// <summary>
        /// 画面オブジェクト
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        public IchiranHeaderForm headerForm;

        private M_SYS_INFO sysinfoEntity;

        private M_KYOTEN kyotenEntity;

        private M_HIKIAI_GENBA genbaEntity;

        private M_TODOUFUKEN todoufukenEntity;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao daoTorihikisaki;

        /// <summary>
        /// 業者のDao
        /// </summary>
        public IM_GYOUSHADao daoGyousha;

        /// <summary>
        /// 引合取引先のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        private Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_TORIHIKISAKIDao daoHikiaiTorihikisaki;

        /// <summary>
        /// 引合業者のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        public Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_GYOUSHADao daoHikiaiGyousha;

        /// <summary>
        /// 引合現場のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        public Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_GENBADao daoGenba;

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
        /// 全コントロール一覧
        /// </summary>
        private Control[] allControl;

        private String ShainCd;

        #endregion

        #region プロパティ

        /// <summary>
        /// ポップアップからデータセットを実施したか否かのフラグ
        /// </summary>
        public bool IsSetDataFromPopup { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.headerForm = (IchiranHeaderForm)((BusinessBaseForm)targetForm.ParentForm).headerForm;
                this.daoTorihikisaki = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                this.daoGyousha = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoHikiaiGyousha = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_GYOUSHADao>();
                this.daoGenba = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiGenbaIchiran.Dao.IM_HIKIAI_GENBADao>();
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("LogicCls", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
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
                this.form.customDataGridView1.BringToFront();
                this.form.customDataGridView1.AllowUserToAddRows = false;   //行の追加オプション(false)

                //システム設定情報読み込み
                this.GetSysInfo();

                // ヘッダーの初期化
                if (!this.InitHeaderArea())
                {
                    return false;
                }

                // コントロールの非活性化
                this.ControlFunctionButton((BusinessBaseForm)this.form.ParentForm, false);


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

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //// ボタンの設定情報をファイルから読み込む
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ButtonInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                //Functionボタンのイベント生成
                parentForm.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);            //新規
                parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);            //修正
                parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);            //削除
                parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);            //複写
                parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並べ替え
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
                parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);             //検索条件設定画面へ遷移

                //画面上でESCキー押下時のイベント生成
                this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

                //明細ダブルクリック時のイベント
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
                //明細エンター時のイベント
                this.form.customDataGridView1.KeyDown += new KeyEventHandler(this.DetailKeyDown);
                this.form.customDataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.CellFormatting);

                //取引先CDのチェック処理イベント
                this.form.TORIHIKISAKI_CD.Enter += new EventHandler(this.form.TORIHIKISAKI_CD_Enter);
                this.form.TORIHIKISAKI_CD.Validating += new CancelEventHandler(this.form.TORIHIKISAKI_CD_Validating);

                //業者CDのチェック処理イベント
                this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
                this.form.GYOUSHA_CD.Validating += new CancelEventHandler(this.form.GYOUSHA_CD_Validating);

                //現場CDのチェック処理イベント
                this.form.GENBA_CD.Validating += new CancelEventHandler(this.form.GENBA_CD_Validating);

                // 20141201 teikyou ダブルクリックを追加する　start
                this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
                // 20141201 teikyou ダブルクリックを追加する　end

                //表示条件イベント追加
                this.AddIchiranHyoujiJoukenEvent();

                /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　start
                this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(TEKIYOU_BEGIN_Leave);
                this.form.TEKIYOU_END.Leave += new System.EventHandler(TEKIYOU_END_Leave);
                /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　start
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("ButtonInit", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func2_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func3_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.OpenWindow(WINDOW_TYPE.DELETE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func4_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func5_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                {
                    CSVExport csvExp = new CSVExport();
                    csvExp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, this.form.DenshuKbn.ToTitleString(), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func6_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 検索条件初期化
                this.headerForm.HEADER_KYOTEN_CD.Text = "99";
                this.headerForm.HEADER_KYOTEN_NAME.Text = string.Empty;
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_RNAME.Text = string.Empty;
                this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_RNAME.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_FURIGANA.Text = string.Empty;
                this.form.GENBA_NAME1.Text = string.Empty;
                this.form.GENBA_NAME2.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_TODOUFUKEN_CD.Text = string.Empty;
                this.form.GenbaTodoufukenNameRyaku.Text = string.Empty;
                this.form.GENBA_ADDRESS.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = null;
                this.form.TEKIYOU_END.Value = null;
                //20150416 minhhoang edit #1748
                this.SetHyoujiJoukenInit();
                //20150416 minhhoang end edit #1748
                //this.form.GetPattern(true);
                //this.form.ShowData();

                //if (this.form.customDataGridView1.Rows.Count > 0)
                //{
                //    this.ControlFunctionButton((BusinessBaseForm)this.form.ParentForm, true);
                //}
                //else
                //{
                //    this.ControlFunctionButton((BusinessBaseForm)this.form.ParentForm, false);
                //}

                var ds = (DataTable)this.form.customDataGridView1.DataSource as DataTable;
                if (ds != null)
                {
                    ds.Clear();
                    this.form.customDataGridView1.DataSource = ds;
                }

                HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.Save();
                this.form.customSearchHeader1.ClearCustomSearchSetting();
                this.form.customSortHeader1.ClearCustomSortSetting();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func7_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　end

                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                // 検索
                var resultCount = this.Search();
                if (resultCount == -1)
                {
                    return;
                }

                if (this.form.customDataGridView1.Rows.Count > 0)
                {
                    this.ControlFunctionButton((BusinessBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.ControlFunctionButton((BusinessBaseForm)this.form.ParentForm, false);
                }

                HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_TORIHIKISAKI_USE_FLG_TEXT = this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_GYOUSHA_USE_FLG_TEXT = this.form.HIKIAI_GYOUSHA_USE_FLG.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_FURIGANA_TEXT = this.form.GENBA_FURIGANA.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME1_TEXT = this.form.GENBA_NAME1.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME2_TEXT = this.form.GENBA_NAME2.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT = this.form.GENBA_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_TODOUFUKEN_CD_TEXT = this.form.GENBA_TODOUFUKEN_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_ADDRESS_TEXT = this.form.GENBA_ADDRESS.Text;
                DateTime? dtTemp;
                dtTemp = this.form.TEKIYOU_BEGIN.Value as DateTime?;
                if (dtTemp.HasValue)
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp.Value;
                }
                else
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }
                dtTemp = this.form.TEKIYOU_END.Value as DateTime?;
                if (dtTemp.HasValue)
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp.Value;
                }
                else
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                }

                //読込データ件数を取得
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

                HikiaiGenbaIchiran.Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func8_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.form.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func10_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_TORIHIKISAKI_USE_FLG_TEXT = this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT = this.form.GYOUSHA_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_GYOUSHA_USE_FLG_TEXT = this.form.HIKIAI_GYOUSHA_USE_FLG.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_CD_TEXT = this.form.GENBA_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT = this.form.GYOUSHA_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_FURIGANA_TEXT = this.form.GENBA_FURIGANA.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME1_TEXT = this.form.GENBA_NAME1.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME2_TEXT = this.form.GENBA_NAME2.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT = this.form.GENBA_NAME_RYAKU.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_TODOUFUKEN_CD_TEXT = this.form.GENBA_TODOUFUKEN_CD.Text;
                HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_ADDRESS_TEXT = this.form.GENBA_ADDRESS.Text;
                // VUNGUYEN 2015/04/14 MOD #3940 START
                //DateTime? dtTemp;
                //dtTemp = this.form.TEKIYOU_BEGIN.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                //}
                //dtTemp = this.form.TEKIYOU_END.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                //}
                DateTime dtTemp;
                if (DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out dtTemp))
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp;
                }
                else
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }

                if (DateTime.TryParse(this.form.TEKIYOU_END.Text, out dtTemp))
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp;
                }
                else
                {
                    HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                }
                // VUNGUYEN 2015/04/14 MOD #3940 END
                //表示条件追記
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                HikiaiGenbaIchiran.Properties.Settings.Default.Save();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_func12_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("DetailKeyDown", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                this.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("DetailCellDoubleClick", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.form.customDataGridView1.Columns[e.ColumnIndex].Name.Equals(this.KEY_FLG))
                {
                    if (bool.Parse(e.Value.ToString()))
                    {
                        e.Value = "引合業者";
                    }
                    else
                    {
                        e.Value = "既存業者";
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CellFormatting", ex);
                throw ex;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

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
                throw ex;
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
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //var us = new KensakuJoukenSetteiForm(this.form.DenshuKbn);
                //us.Show();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process2_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
            try
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
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("bt_process2_Click", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BusinessBaseForm)this.form.Parent;

                if (e.KeyCode == Keys.Escape)
                {
                    //処理No(ESC)へカーソル移動
                    parentForm.txb_process.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("form_PreviewKeyDown", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            ButtonSetting[] result = null;

            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                result = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("CreateButtonInfo", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }
        #endregion

        #region ヘッダーの初期化

        private bool InitHeaderArea()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // チェックメソッド設定
                this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField = this.headerForm.HEADER_KYOTEN_CD.PopupGetMasterField;
                this.headerForm.HEADER_KYOTEN_CD.SetFormField = this.headerForm.HEADER_KYOTEN_CD.PopupSetFormField;
                Collection<SelectCheckDto> dtoCollection = new Collection<SelectCheckDto>();
                SelectCheckDto dto = new SelectCheckDto();
                dto.CheckMethodName = "拠点マスタコードチェックandセッティング";
                dtoCollection.Add(dto);
                this.headerForm.HEADER_KYOTEN_CD.FocusOutCheckMethod = dtoCollection;

                // ポップアップ設定
                this.headerForm.HEADER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";

                // 初期値設定
                //this.headerForm.HEADER_KYOTEN_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT;
                this.form.TORIHIKISAKI_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
                this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_TORIHIKISAKI_USE_FLG_TEXT;
                if (string.IsNullOrWhiteSpace(this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text))
                {
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                }
                this.form.GYOUSHA_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_CD_TEXT;
                this.form.HIKIAI_GYOUSHA_USE_FLG.Text = HikiaiGenbaIchiran.Properties.Settings.Default.HIKIAI_GYOUSHA_USE_FLG_TEXT;
                if (string.IsNullOrWhiteSpace(this.form.HIKIAI_GYOUSHA_USE_FLG.Text))
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }
                this.form.GENBA_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_CD_TEXT;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = HikiaiGenbaIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT;
                this.form.GYOUSHA_NAME_RYAKU.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GYOUSHA_NAME_RYAKU_TEXT;
                this.form.GENBA_FURIGANA.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_FURIGANA_TEXT;
                this.form.GENBA_NAME1.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME1_TEXT;
                this.form.GENBA_NAME2.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME2_TEXT;
                this.form.GENBA_NAME_RYAKU.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_NAME_RYAKU_TEXT;
                this.form.GENBA_TODOUFUKEN_CD.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_TODOUFUKEN_CD_TEXT;
                this.form.GENBA_ADDRESS.Text = HikiaiGenbaIchiran.Properties.Settings.Default.GENBA_ADDRESS_TEXT;
                if (HikiaiGenbaIchiran.Properties.Settings.Default["TEKIYOU_BEGIN_VALUE"] == null)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else if ((DateTime)HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE != new DateTime(0))
                {
                    this.form.TEKIYOU_BEGIN.Value = HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE;
                }
                if (HikiaiGenbaIchiran.Properties.Settings.Default["TEKIYOU_END_VALUE"] == null)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else if ((DateTime)HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE != new DateTime(0))
                {
                    this.form.TEKIYOU_END.Value = HikiaiGenbaIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE;
                }

                //拠点名をセット
                if (!string.IsNullOrEmpty(this.headerForm.HEADER_KYOTEN_CD.Text))
                {
                    this.kyotenEntity = daoKyoten.GetDataByCd(this.headerForm.HEADER_KYOTEN_CD.Text);
                    this.headerForm.HEADER_KYOTEN_NAME.Text = kyotenEntity.KYOTEN_NAME_RYAKU;
                }

                //取引先名をセット
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    if (this.SearchsetTorihikisaki(null) == -1)
                    {
                        return false;
                    }
                }

                //業者名をセット
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    if (this.SearchsetGyousha(null) == -1)
                    {
                        return false;
                    }
                }

                //現場名をセット
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.genbaEntity = daoGenba.GetDataByCd(this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"), this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                    if (genbaEntity != null)
                    {
                        this.form.GENBA_RNAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }
                }

                //都道府県をセット
                if (!string.IsNullOrEmpty(this.form.GENBA_TODOUFUKEN_CD.Text))
                {
                    this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.form.GENBA_TODOUFUKEN_CD.Text);
                    this.form.GenbaTodoufukenNameRyaku.Text = this.todoufukenEntity.TODOUFUKEN_NAME_RYAKU;
                }

                //アラート件数の初期値セット
                if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
                {
                    this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
                }

                //アラートの保存データがあればそちらを表示
                if (HikiaiGenbaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
                {
                    this.headerForm.alertNumber.Text = HikiaiGenbaIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("InitHeaderArea", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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

        #endregion

        #region 引合取引先入力画面起動処理

        /// <summary>
        /// 取引先入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        private bool OpenWindow(WINDOW_TYPE windowType, bool newFlg = false)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType, newFlg);

                string formID_M463 = "M463";

                // 引数へのオブジェクトを作成する
                // 新規の場合は引数なし、ただし参照の場合は引数あり
                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
                {
                    FormManager.OpenFormWithAuth(formID_M463, WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
                }
                else
                {
                    // 表示されている行の取引先CDを取得する
                    DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                    if (row != null)    // No.3988
                    {
                        bool flg = (bool)row.Cells[this.KEY_FLG].Value;
                        string cd1 = row.Cells[this.KEY_ID1].Value.ToString();
                        string cd2 = row.Cells[this.KEY_ID2].Value.ToString();

                        // データ削除チェックを行う
                        if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            M_HIKIAI_GENBA data = this.daoGenba.GetDataByCd(flg, cd1, cd2);
                            if (data.DELETE_FLG)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                //msgLogic.MessageBoxShow("E026", "コード");
                                //LogUtility.DebugMethodEnd();
                                //return;

                                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                                {
                                    //20150929 hoanghm #1931 start
                                    //msgLogic.MessageBoxShow("E202", "選択した現場は");
                                    //LogUtility.DebugMethodEnd();
                                    //return;

                                    var existGyousha = this.ExistedGyousha(flg, cd1);
                                    var existGenba = this.ExistedGenba(flg, cd1, cd2);

                                    if (existGyousha)
                                    {
                                        // 引合業者
                                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                                        if (existGenba)
                                        {
                                            // 本登録前(業者のみ本登録)
                                            messageShowLogic.MessageBoxShow("E204", "既に業者が", "業者");
                                        }
                                        else
                                        {
                                            // 本登録後(業者と現場も本登録)
                                            messageShowLogic.MessageBoxShow("E202", "既に");
                                        }

                                        return ret;
                                    }
                                    else
                                    {
                                        // 既存業者
                                        MessageBoxShowLogic messageShowLogic = new MessageBoxShowLogic();
                                        if (existGenba)
                                        {
                                            // 本登録後(既存業者で現場が本登録)
                                            messageShowLogic.MessageBoxShow("E202", "既に");

                                            return ret;
                                        }
                                    }

                                    var result = Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("C057");
                                    if (result != DialogResult.Yes)
                                    {
                                        LogUtility.DebugMethodEnd();
                                        return ret;
                                    }

                                    //20150929 hoanghm #1931 end
                                }
                                else
                                {
                                    msgLogic.MessageBoxShow("E026", "コード");
                                    LogUtility.DebugMethodEnd();
                                    return ret;
                                }
                            }
                        }

                        switch (windowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 複写モードで起動（新規モード）
                                FormManager.OpenFormWithAuth(formID_M463, WINDOW_TYPE.NEW_WINDOW_FLAG, windowType, flg, cd1, cd2);
                                break;
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                // 修正モードの権限チェック
                                if (Manager.CheckAuthority(formID_M463, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    FormManager.OpenFormWithAuth(formID_M463, WINDOW_TYPE.UPDATE_WINDOW_FLAG, windowType, flg, cd1, cd2);
                                }
                                // 参照モードの権限チェック
                                else if (Manager.CheckAuthority(formID_M463, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    FormManager.OpenFormWithAuth(formID_M463, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, flg, cd1, cd2);
                                }
                                else
                                {
                                    // 修正モードの権限なしのアラームを上げる
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E158", "修正");
                                }
                                break;
                            case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                                // 削除モードで起動
                                FormManager.OpenFormWithAuth(formID_M463, WINDOW_TYPE.DELETE_WINDOW_FLAG, windowType, flg, cd1, cd2);
                                break;
                            default:
                                break;
                        }
                        //FormManager.OpenFormWithAuth(formIDM463, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, windowType, flg, cd1, cd2);
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("OpenWindow", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("OpenWindow", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OpenWindow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// システム設定マスタ取得
        /// </summary>
        private void GetSysInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
                this.sysinfoEntity = sysInfo[0];
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("GetSysInfo", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

            LogicCls localLogic = other as LogicCls;
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

        #region 共通関数

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            string result = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(value);

                if (string.IsNullOrEmpty(value) == true)
                {
                    result = "0";
                }
                else
                {
                    result = string.Format("{0:#,0}", Convert.ToDecimal(value));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("SetComma", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
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

            try
            {
                LogUtility.DebugMethodStart();

                int.TryParse(this.headerForm.alertNumber.Text,System.Globalization.NumberStyles.AllowThousands,null, out result);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("GetAlertCount", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        #endregion

        #region 検索条件作成

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string GetSearchString()
        {
            var result = new StringBuilder(256);

            try
            {
                LogUtility.DebugMethodStart();

                string strTemp;

                // 拠点CD
                strTemp = this.headerForm.HEADER_KYOTEN_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    // 拠点で全社が指定された場合は全データ検索
                    // 上記以外の場合は指定された拠点のみ検索
                    if (!strTemp.Equals(ConstCls.KYOTEN_CD_ALL))
                    {
                        if (!string.IsNullOrWhiteSpace(result.ToString()))
                        {
                            result.Append(" AND ");
                        }
                        result.AppendFormat(" M_HIKIAI_GENBA.KYOTEN_CD = {0}", strTemp);
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
                    result.AppendFormat(" M_HIKIAI_GENBA.TORIHIKISAKI_CD = '{0}' AND M_HIKIAI_GENBA.HIKIAI_TORIHIKISAKI_USE_FLG = '{1}'", strTemp, this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text);
                }

                // 業者CD
                strTemp = this.form.GYOUSHA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GYOUSHA_CD = '{0}' AND M_HIKIAI_GENBA.HIKIAI_GYOUSHA_USE_FLG = '{1}'", strTemp, this.form.HIKIAI_GYOUSHA_USE_FLG.Text);
                }

                // 現場CD
                strTemp = this.form.GENBA_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_CD = '{0}'", strTemp);
                }

                // 取引先略称
                strTemp = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" (M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%' OR M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%')", strTemp);
                }

                // 業者略称
                strTemp = this.form.GYOUSHA_NAME_RYAKU.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" (M_GYOUSHA.GYOUSHA_NAME_RYAKU LIKE '%{0}%' OR M_HIKIAI_GYOUSHA.GYOUSHA_NAME_RYAKU LIKE '%{0}%')", strTemp);
                }

                // フリガナ
                strTemp = this.form.GENBA_FURIGANA.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_FURIGANA LIKE '%{0}%'", strTemp);
                }

                // 現場名1
                strTemp = this.form.GENBA_NAME1.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME1 LIKE '%{0}%'", strTemp);
                }

                // 現場名2
                strTemp = this.form.GENBA_NAME2.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME2 LIKE '%{0}%'", strTemp);
                }

                // 略称名
                strTemp = this.form.GENBA_NAME_RYAKU.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME_RYAKU LIKE '%{0}%'", strTemp);
                }

                // 都道府県CD
                strTemp = this.form.GENBA_TODOUFUKEN_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.GENBA_TODOUFUKEN_CD = {0}", strTemp);
                }

                // 住所
                strTemp = this.form.GENBA_ADDRESS.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" (M_HIKIAI_GENBA.GENBA_ADDRESS1 + M_HIKIAI_GENBA.GENBA_ADDRESS2) LIKE '%{0}%'", strTemp);
                }

                // 適用開始
                strTemp = this.form.TEKIYOU_BEGIN.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_GENBA.TEKIYOU_END >= CONVERT(date,'{0}') OR M_HIKIAI_GENBA.TEKIYOU_END IS NULL)", strTemp);
                }

                // 適用終了
                strTemp = this.form.TEKIYOU_END.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_GENBA.TEKIYOU_END <= CONVERT(date,'{0}') OR M_HIKIAI_GENBA.TEKIYOU_END IS NULL)", strTemp);
                }

                // 表示条件
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.Append(" M_HIKIAI_GENBA.DELETE_FLG = 0");
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
                    result.Append(" OR (((M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_GENBA.TEKIYOU_END) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and M_HIKIAI_GENBA.TEKIYOU_END IS NULL) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_GENBA.TEKIYOU_END) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN IS NULL and M_HIKIAI_GENBA.TEKIYOU_END IS NULL)) and M_HIKIAI_GENBA.DELETE_FLG = 0)");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    result.Append(" OR M_HIKIAI_GENBA.DELETE_FLG = 1");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    result.Append(" OR ((M_HIKIAI_GENBA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > M_HIKIAI_GENBA.TEKIYOU_END) and M_HIKIAI_GENBA.DELETE_FLG = 0)");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    result.Append(")");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Fatal("GetSearchString", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result.ToString();
        }
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return -1;
            }
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
                sb.AppendFormat(" M_HIKIAI_GENBA.GYOUSHA_CD AS {0}, ", this.KEY_ID1);
                sb.AppendFormat(" M_HIKIAI_GENBA.GENBA_CD AS {0}, ", this.KEY_ID2);
                sb.AppendFormat(" M_HIKIAI_GENBA.HIKIAI_GYOUSHA_USE_FLG AS {0}, ", this.KEY_FLG);
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

            // 現場マスタ
            sb.Append(" FROM M_HIKIAI_GENBA ");

            // 20140722 chenchisi EV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー start
            sb.Append(" LEFT JOIN M_TORIHIKISAKI M_TORIHIKISAKI1 ON M_HIKIAI_GENBA.TORIHIKISAKI_CD = M_TORIHIKISAKI1.TORIHIKISAKI_CD and M_HIKIAI_GENBA.HIKIAI_TORIHIKISAKI_USE_FLG = 0 ");
            sb.Append(" LEFT JOIN M_HIKIAI_TORIHIKISAKI M_HIKIAI_TORIHIKISAKI1 ON M_HIKIAI_GENBA.TORIHIKISAKI_CD = M_HIKIAI_TORIHIKISAKI1.TORIHIKISAKI_CD and M_HIKIAI_GENBA.HIKIAI_TORIHIKISAKI_USE_FLG = 1 ");
            sb.Append(" LEFT JOIN M_GYOUSHA M_GYOUSHA1 ON M_HIKIAI_GENBA.GYOUSHA_CD = M_GYOUSHA1.GYOUSHA_CD and M_HIKIAI_GENBA.HIKIAI_GYOUSHA_USE_FLG = 0 ");
            sb.Append(" LEFT JOIN M_HIKIAI_GYOUSHA M_HIKIAI_GYOUSHA1 ON M_HIKIAI_GENBA.GYOUSHA_CD = M_HIKIAI_GYOUSHA1.GYOUSHA_CD and M_HIKIAI_GENBA.HIKIAI_GYOUSHA_USE_FLG = 1 ");
            // 20140722 chenchisi EV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー end

            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                // 現場_定期品名マスタ
                sb.Append(" LEFT JOIN M_HIKIAI_GENBA_TEIKI_HINMEI ");
                sb.Append(" ON M_HIKIAI_GENBA.GYOUSHA_CD = M_HIKIAI_GENBA_TEIKI_HINMEI.GYOUSHA_CD ");
                sb.Append(" AND M_HIKIAI_GENBA.GENBA_CD = M_HIKIAI_GENBA_TEIKI_HINMEI.GENBA_CD ");

                // 現場_月極品名マスタ
                sb.Append(" LEFT JOIN M_HIKIAI_GENBA_TSUKI_HINMEI ");
                sb.Append(" ON M_HIKIAI_GENBA.GYOUSHA_CD = M_HIKIAI_GENBA_TSUKI_HINMEI.GYOUSHA_CD ");
                sb.Append(" AND M_HIKIAI_GENBA.GENBA_CD = M_HIKIAI_GENBA_TSUKI_HINMEI.GENBA_CD ");

                sb.Append(" LEFT JOIN M_HINMEI M_HINMEI2 ON M_HIKIAI_GENBA_TEIKI_HINMEI.TSUKI_HINMEI_CD = M_HINMEI2.HINMEI_CD ");
                sb.Append(" LEFT JOIN M_HINMEI M_HINMEI3 ON M_HIKIAI_GENBA_TSUKI_HINMEI.HINMEI_CD = M_HINMEI3.HINMEI_CD ");
            }

            // パターンから作成したJOIN句
            sb.Append(this.form.JoinQuery);

            return sb.ToString();
        }

        /// <summary>
        /// 検索条件作成処理
        /// </summary>
        /// <returns>検索条件</returns>
        public string CreateWhereQuery()
        {
            var result = new StringBuilder();
            string strTemp;

            // 拠点CD
            strTemp = this.headerForm.HEADER_KYOTEN_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                // 拠点で全社が指定された場合は全データ検索
                // 上記以外の場合は指定された拠点のみ検索
                if (!strTemp.Equals(ConstCls.KYOTEN_CD_ALL))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_GENBA.KYOTEN_CD = {0}", strTemp);
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
                result.AppendFormat(" M_HIKIAI_GENBA.TORIHIKISAKI_CD = '{0}' AND M_HIKIAI_GENBA.HIKIAI_TORIHIKISAKI_USE_FLG = '{1}'", strTemp, this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text);
            }

            // 業者CD
            strTemp = this.form.GYOUSHA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GYOUSHA_CD = '{0}' AND M_HIKIAI_GENBA.HIKIAI_GYOUSHA_USE_FLG = '{1}'", strTemp, this.form.HIKIAI_GYOUSHA_USE_FLG.Text);
            }

            // 現場CD
            strTemp = this.form.GENBA_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_CD = '{0}'", strTemp);
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
                // 20140722 chenchisiEV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー start
                result.AppendFormat(" M_HIKIAI_TORIHIKISAKI1.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%'", strTemp);
                // 20140722 chenchisi EV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー end
            }

            // 業者略称
            strTemp = this.form.GYOUSHA_NAME_RYAKU.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                // 20140722 chenchisiEV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー start
                result.AppendFormat(" M_HIKIAI_GYOUSHA1.GYOUSHA_NAME_RYAKU LIKE '%{0}%'", strTemp);
                // 20140722 chenchisi EV005368_引合現場一覧の取引先略称、業者略称に入力して検索するとシステムエラー end
            }

            // フリガナ
            strTemp = this.form.GENBA_FURIGANA.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_FURIGANA LIKE '%{0}%'", strTemp);
            }

            // 現場名1
            strTemp = this.form.GENBA_NAME1.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME1 LIKE '%{0}%'", strTemp);
            }

            // 現場名2
            strTemp = this.form.GENBA_NAME2.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME2 LIKE '%{0}%'", strTemp);
            }

            // 略称名
            strTemp = this.form.GENBA_NAME_RYAKU.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_NAME_RYAKU LIKE '%{0}%'", strTemp);
            }

            // 都道府県CD
            strTemp = this.form.GENBA_TODOUFUKEN_CD.Text;
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.GENBA_TODOUFUKEN_CD = {0}", strTemp);
            }

            // 住所
            strTemp = this.form.GENBA_ADDRESS.Text;
            strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
            if (!string.IsNullOrWhiteSpace(strTemp))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" (M_HIKIAI_GENBA.GENBA_ADDRESS1 + M_HIKIAI_GENBA.GENBA_ADDRESS2) LIKE '%{0}%'", strTemp);
            }

            // 適用開始
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
            {
                strTemp = this.form.TEKIYOU_BEGIN.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (M_HIKIAI_GENBA.TEKIYOU_END >= CONVERT(date,'{0}') OR M_HIKIAI_GENBA.TEKIYOU_END IS NULL)", strTemp);
            }

            // 適用終了
            if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
            {
                strTemp = this.form.TEKIYOU_END.Value.ToString();
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.AppendFormat(" M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                result.Append(" AND ");
                result.AppendFormat(" (M_HIKIAI_GENBA.TEKIYOU_END <= CONVERT(date,'{0}') OR M_HIKIAI_GENBA.TEKIYOU_END IS NULL)", strTemp);
            }

            // 表示条件
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                {
                    result.Append(" AND ");
                }
                result.Append(" M_HIKIAI_GENBA.DELETE_FLG = 0");
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
                result.Append(" OR (((M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_GENBA.TEKIYOU_END) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and M_HIKIAI_GENBA.TEKIYOU_END IS NULL) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_GENBA.TEKIYOU_END) or (M_HIKIAI_GENBA.TEKIYOU_BEGIN IS NULL and M_HIKIAI_GENBA.TEKIYOU_END IS NULL)) and M_HIKIAI_GENBA.DELETE_FLG = 0)");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
            {
                result.Append(" OR M_HIKIAI_GENBA.DELETE_FLG = 1");
            }
            if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                result.Append(" OR ((M_HIKIAI_GENBA.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > M_HIKIAI_GENBA.TEKIYOU_END) and M_HIKIAI_GENBA.DELETE_FLG = 0)");
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

        #region ファンクションボタン制御
        /// <summary>
        /// BusinessBaseForm用ファンクションボタン制御処理
        /// </summary>
        /// <param name="isEnable"></param>
        public void ControlFunctionButton(BusinessBaseForm baseForm, bool isEnable)
        {
            LogUtility.DebugMethodStart(baseForm, isEnable);

            if (!string.IsNullOrWhiteSpace(baseForm.bt_func3.Text))
            {
                baseForm.bt_func3.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func4.Text))
            {
                baseForm.bt_func4.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func5.Text))
            {
                baseForm.bt_func5.Enabled = isEnable;
            }
            if (!string.IsNullOrWhiteSpace(baseForm.bt_func6.Text))
            {
                baseForm.bt_func6.Enabled = isEnable;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 一覧表示イベント追加、削除
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

        #region 取引先情報取得処理
        /// <summary>
        /// データ取得処理(取引先)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int SearchsetTorihikisaki(CancelEventArgs e)
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                bool isSet = false;
                if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("0"))
                {
                    M_TORIHIKISAKI entity = null;
                    M_TORIHIKISAKI queryParam = new M_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    queryParam.ISNOT_NEED_DELETE_FLG = true;
                    M_TORIHIKISAKI[] result = this.daoTorihikisaki.GetAllValidData(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.TORIHIKISAKI_RNAME.Text = entity.TORIHIKISAKI_NAME_RYAKU;
                        isSet = true;
                    }
                }
                else if (this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_TORIHIKISAKI entity = null;
                    M_HIKIAI_TORIHIKISAKI queryParam = new M_HIKIAI_TORIHIKISAKI();
                    queryParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    M_HIKIAI_TORIHIKISAKI[] result = this.daoHikiaiTorihikisaki.GetAllValidDataMinCols(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.TORIHIKISAKI_RNAME.Text = entity.TORIHIKISAKI_NAME_RYAKU;
                        isSet = true;
                    }
                }
                if (string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_CD.Text))
                {
                    isSet = true;
                    this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";
                }

                if (!isSet)
                {
                    this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                    this.form.HIKIAI_TORIHIKISAKI_USE_FLG.Text = "0";

                    if (this.form.TORIHIKISAKI_CD.Text != "")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "取引先");
                        this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetTorihikisaki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }
        #endregion

        #region 業者情報取得処理
        /// <summary>
        /// データ取得処理(業者)
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int SearchsetGyousha(CancelEventArgs e)
        {
            int count = 0;

            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("True"))
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "1";
                }
                else if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("False"))
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }

                bool isSet = false;
                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("0"))
                {
                    M_GYOUSHA entity = null;
                    M_GYOUSHA queryParam = new M_GYOUSHA();
                    queryParam.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    queryParam.ISNOT_NEED_DELETE_FLG = true;
                    M_GYOUSHA[] result = this.daoGyousha.GetAllValidData(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.GYOUSHA_RNAME.Text = entity.GYOUSHA_NAME_RYAKU;
                        isSet = true;
                    }
                }
                else if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                {
                    M_HIKIAI_GYOUSHA entity = null;
                    M_HIKIAI_GYOUSHA queryParam = new M_HIKIAI_GYOUSHA();
                    queryParam.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    M_HIKIAI_GYOUSHA[] result = this.daoHikiaiGyousha.GetAllValidDataMinCols(queryParam);
                    if (result != null && result.Length > 0)
                    {
                        entity = result[0];
                        this.form.GYOUSHA_RNAME.Text = entity.GYOUSHA_NAME_RYAKU;
                        isSet = true;
                    }
                }
                if (string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    isSet = true;
                    this.form.GYOUSHA_RNAME.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_RNAME.Text = string.Empty;
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }

                if (!isSet)
                {
                    this.form.GYOUSHA_RNAME.Text = string.Empty;
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";

                    if (this.form.GYOUSHA_CD.Text != "")
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD.Focus();
                        this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchsetGyousha", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchsetGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }

            return count;
        }
        #endregion

        #region 現場のバリデーションイベント
        /// <summary>
        /// 現場CDのチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) STR
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.form.GENBA_RNAME.Text = string.Empty;
                    return ret;
                }
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    e.Cancel = true;
                    return ret;
                }
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(一覧タイプ) END

                if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("True"))
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "1";
                }
                else if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("False"))
                {
                    this.form.HIKIAI_GYOUSHA_USE_FLG.Text = "0";
                }

                string gyoushaCd = this.form.GYOUSHA_CD.Text;
                string genbaCd = this.form.GENBA_CD.Text;
                if (string.IsNullOrWhiteSpace(gyoushaCd))
                {
                    M_HIKIAI_GENBA[] genba = this.daoGenba.GetDataByCdOnly(genbaCd);
                    if (genba.Length <= 0)
                    {
                        this.form.GYOUSHA_RNAME.Text = string.Empty;
                        this.form.GENBA_CD.Text = string.Empty;
                        this.form.GENBA_RNAME.Text = string.Empty;
                    }
                    else if (genba.Length == 1)
                    {
                        if (genba[0].HIKIAI_GYOUSHA_USE_FLG)
                        {
                            M_HIKIAI_GYOUSHA gyo = this.daoHikiaiGyousha.GetDataByCd(genba[0].GYOUSHA_CD);
                            if (gyo != null)
                            {
                                this.form.GYOUSHA_CD.Text = gyo.GYOUSHA_CD;
                                this.form.GYOUSHA_RNAME.Text = gyo.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.GYOUSHA_CD.Text = string.Empty;
                                this.form.GYOUSHA_RNAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            M_GYOUSHA gyo = this.daoGyousha.GetDataByCd(genba[0].GYOUSHA_CD);
                            if (gyo != null)
                            {
                                this.form.GYOUSHA_CD.Text = gyo.GYOUSHA_CD;
                                this.form.GYOUSHA_RNAME.Text = gyo.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.GYOUSHA_CD.Text = string.Empty;
                                this.form.GYOUSHA_RNAME.Text = string.Empty;
                            }
                        }
                        this.form.GENBA_CD.Text = genba[0].GENBA_CD;
                        this.form.GENBA_RNAME.Text = genba[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.GENBA_CD.PopUp();
                    }
                }
                else if (string.IsNullOrWhiteSpace(genbaCd))
                {
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_RNAME.Text = string.Empty;
                }
                else
                {
                    gyoushaCd = gyoushaCd.PadLeft((int)this.form.GYOUSHA_CD.CharactersNumber);
                    genbaCd = genbaCd.PadLeft((int)this.form.GENBA_CD.CharactersNumber);
                    M_HIKIAI_GENBA entityGenba = this.daoGenba.GetDataByCd(this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"), gyoushaCd, genbaCd);
                    if (entityGenba != null)
                    {
                        if (this.form.HIKIAI_GYOUSHA_USE_FLG.Text.Equals("1"))
                        {
                            M_HIKIAI_GYOUSHA gyo = this.daoHikiaiGyousha.GetDataByCd(gyoushaCd);
                            if (gyo != null)
                            {
                                this.form.GYOUSHA_CD.Text = gyo.GYOUSHA_CD;
                                this.form.GYOUSHA_RNAME.Text = gyo.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.GYOUSHA_RNAME.Text = string.Empty;
                            }
                        }
                        else
                        {
                            M_GYOUSHA gyo = this.daoGyousha.GetDataByCd(gyoushaCd);
                            if (gyo != null)
                            {
                                this.form.GYOUSHA_CD.Text = gyo.GYOUSHA_CD;
                                this.form.GYOUSHA_RNAME.Text = gyo.GYOUSHA_NAME_RYAKU;
                            }
                            else
                            {
                                this.form.GYOUSHA_RNAME.Text = string.Empty;
                            }
                        }
                        this.form.GENBA_CD.Text = entityGenba.GENBA_CD;
                        this.form.GENBA_RNAME.Text = entityGenba.GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.GENBA_RNAME.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "引合現場");
                        e.Cancel = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GENBA_CD_Validating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("GENBA_CD_Validating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

        /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            try
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

                DateTime date_from = Convert.ToDateTime(this.form.TEKIYOU_BEGIN.Value);
                DateTime date_to = Convert.ToDateTime(this.form.TEKIYOU_END.Value);

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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DateCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
        /// 20141203 Houkakou 「引合現場一覧」の日付チェックを追加する　end

        #region 本登録済みか判定
        /// <summary>
        /// 指定された引合業者利用フラグ,業者CDが本登録済みデータか判定
        /// </summary>
        /// <param name="hikiaiGyoushaUseFlg">引合業者利用フラグ</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedGyousha(bool hikiaiGyoushaUseFlg, string gyoushaCD)
        {
            if (string.IsNullOrEmpty(gyoushaCD))
            {
                return false;
            }

            if (hikiaiGyoushaUseFlg)
            {
                // 引合業者使用時は、引合業者CDも本登録済みかチェックを行う
                var gyoushaEntity = daoHikiaiGyousha.GetDataByCd(gyoushaCD);
                if (gyoushaEntity != null && !string.IsNullOrEmpty(gyoushaEntity.GYOUSHA_CD_AFTER))
                {
                    // 移行後業者CDに値がある場合は、本登録済みデータとみなす
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 指定された引合業者利用フラグ,業者CD,現場CDが本登録済みデータか判定
        /// </summary>
        /// <param name="hikiaiGyoushaUseFlg">引合業者利用フラグ</param>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedGenba(bool hikiaiGyoushaUseFlg, string gyoushaCD, string genbaCD)
        {
            if (string.IsNullOrEmpty(gyoushaCD)
                || string.IsNullOrEmpty(genbaCD))
            {
                return false;
            }

            var entity = daoGenba.GetDataByCd(hikiaiGyoushaUseFlg, gyoushaCD, genbaCD);
            if (entity != null && !string.IsNullOrEmpty(entity.GENBA_CD_AFTER))
            {
                // 移行後現場CDに値がある場合は、本登録済みデータとみなす
                return true;
            }

            return false;
        }
        #endregion
    }
}
