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
using Shougun.Core.Master.HikiaiTorihikisakiIchiran.APP;
using Shougun.Core.Master.HikiaiTorihikisakiIchiran.Const;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.HikiaiTorihikisakiIchiran.Logic
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.HikiaiTorihikisakiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面連携に使用するキー取得項目名
        /// </summary>
        internal readonly string KEY_ID1 = "HIDDEN_TORIHIKISAKI_CD";

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

        private M_HIKIAI_TORIHIKISAKI torihikisakiEntity;

        private M_TODOUFUKEN todoufukenEntity;

        /// <summary>
        /// 引合取引先のDao
        /// TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
        /// </summary>
        public Shougun.Core.Master.HikiaiTorihikisakiIchiran.Dao.IM_HIKIAI_TORIHIKISAKIDao daoTorihikisaki;

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

        #endregion

        #region プロパティ

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.headerForm = (IchiranHeaderForm)((BusinessBaseForm)targetForm.ParentForm).headerForm;
                // TODO：いずれはフレームワーク側のDAOを使うようにリファクタリングが必要
                this.daoTorihikisaki = DaoInitUtility.GetComponent<Shougun.Core.Master.HikiaiTorihikisakiIchiran.Dao.IM_HIKIAI_TORIHIKISAKIDao>();
                this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoKyoten = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.daoTodoufuken = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("LogicClass", ex);
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
                this.InitHeaderArea();

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
                ret = false;
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
                parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);         //F11 フィルタ
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる
                parentForm.bt_process1.Click += new EventHandler(this.bt_process1_Click);             //パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(this.bt_process2_Click);             //検索条件設定画面へ遷移

                //画面上でESCキー押下時のイベント生成
                this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(this.form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

                //明細ダブルクリック時のイベント
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DetailCellDoubleClick);
                //明細エンター時のイベント
                this.form.customDataGridView1.KeyDown += new KeyEventHandler(this.DetailKeyDown);

                //取引先CDのチェック処理イベント
                this.form.TORIHIKISAKI_CD.Validating += new CancelEventHandler(this.form.TORIHIKISAKI_CD_Validating);

                // 20141201 teikyou ダブルクリックを追加する　start
                this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(TEKIYOU_END_MouseDoubleClick);
                // 20141201 teikyou ダブルクリックを追加する　end

                //表示条件イベント追加
                this.AddIchiranHyoujiJoukenEvent();

                /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　start
                this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(TEKIYOU_BEGIN_Leave);
                this.form.TEKIYOU_END.Leave += new System.EventHandler(TEKIYOU_END_Leave);
                /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　start
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

                if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
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
                this.form.TORIHIKISAKI_CD.Clear();
                this.form.TORIHIKISAKI_RNAME.Clear();
                this.form.TORIHIKISAKI_FURIGANA.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = string.Empty;
                this.form.TORIHIKISAKI_ADDRESS.Text = string.Empty;
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

                HikiaiTorihikisakiIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.Save();
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

                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　end

                //拠点コードがあるときは、検索条件にセットする
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

                HikiaiTorihikisakiIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_FURIGANA_TEXT = this.form.TORIHIKISAKI_FURIGANA.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME1_TEXT = this.form.TORIHIKISAKI_NAME1.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME2_TEXT = this.form.TORIHIKISAKI_NAME2.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_TODOUFUKEN_CD_TEXT = this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_ADDRESS_TEXT = this.form.TORIHIKISAKI_ADDRESS.Text;
                DateTime? dtTemp;
                dtTemp = this.form.TEKIYOU_BEGIN.Value as DateTime?;
                if (dtTemp.HasValue)
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp.Value;
                }
                else
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }
                dtTemp = this.form.TEKIYOU_END.Value as DateTime?;
                if (dtTemp.HasValue)
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp.Value;
                }
                else
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
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

                HikiaiTorihikisakiIchiran.Properties.Settings.Default.Save();
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

                HikiaiTorihikisakiIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT = this.headerForm.HEADER_KYOTEN_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU = this.headerForm.alertNumber.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT = this.form.TORIHIKISAKI_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_FURIGANA_TEXT = this.form.TORIHIKISAKI_FURIGANA.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME1_TEXT = this.form.TORIHIKISAKI_NAME1.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME2_TEXT = this.form.TORIHIKISAKI_NAME2.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_TODOUFUKEN_CD_TEXT = this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text;
                HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_ADDRESS_TEXT = this.form.TORIHIKISAKI_ADDRESS.Text;
                // VUNGUYEN 2015/04/14 MOD #3940 START
                //DateTime? dtTemp;
                //dtTemp = this.form.TEKIYOU_BEGIN.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                //}
                //dtTemp = this.form.TEKIYOU_END.Value as DateTime?;
                //if (dtTemp.HasValue)
                //{
                //    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp.Value;
                //}
                //else
                //{
                //    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                //}
                DateTime dtTemp;
                if (DateTime.TryParse(this.form.TEKIYOU_BEGIN.Text, out dtTemp))
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = dtTemp;
                }
                else
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE = new DateTime(0);
                }

                if (DateTime.TryParse(this.form.TEKIYOU_END.Text, out dtTemp))
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = dtTemp;
                }
                else
                {
                    HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE = new DateTime(0);
                }
                // VUNGUYEN 2015/04/14 MOD #3940 END
                //表示条件追記
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                HikiaiTorihikisakiIchiran.Properties.Settings.Default.Save();

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
        #endregion

        #region プロセスボタン押下処理
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

        #region 取引先CDのバリデーションイベント
        /// <summary>
        /// 取引先CDのチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                string torihikiCd = this.form.TORIHIKISAKI_CD.Text;
                if (string.IsNullOrWhiteSpace(torihikiCd))
                {
                    this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                }
                else
                {
                    torihikiCd = torihikiCd.PadLeft((int)this.form.TORIHIKISAKI_CD.CharactersNumber);
                    M_HIKIAI_TORIHIKISAKI entityTorihikisaki = this.daoTorihikisaki.GetDataByCd(torihikiCd);
                    if (entityTorihikisaki != null)
                    {
                        this.form.TORIHIKISAKI_CD.Text = entityTorihikisaki.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_RNAME.Text = entityTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_RNAME.Text = string.Empty;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "引合取引先");
                        e.Cancel = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("TORIHIKISAKI_CD_Validating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("TORIHIKISAKI_CD_Validating", ex);
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

        private void InitHeaderArea()
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
                //this.headerForm.HEADER_KYOTEN_CD.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.KYOTEN_CD_TEXT;
                this.form.TORIHIKISAKI_CD.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_CD_TEXT;
                this.form.TORIHIKISAKI_FURIGANA.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_FURIGANA_TEXT;
                this.form.TORIHIKISAKI_NAME1.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME1_TEXT;
                this.form.TORIHIKISAKI_NAME2.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME2_TEXT;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_NAME_RYAKU_TEXT;
                this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_TODOUFUKEN_CD_TEXT;
                this.form.TORIHIKISAKI_ADDRESS.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TORIHIKISAKI_ADDRESS_TEXT;
                if (HikiaiTorihikisakiIchiran.Properties.Settings.Default["TEKIYOU_BEGIN_VALUE"] == null)
                {
                    this.form.TEKIYOU_BEGIN.Value = null;
                }
                else if ((DateTime)HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE != new DateTime(0))
                {
                    this.form.TEKIYOU_BEGIN.Value = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_BEGIN_VALUE;
                }
                if (HikiaiTorihikisakiIchiran.Properties.Settings.Default["TEKIYOU_END_VALUE"] == null)
                {
                    this.form.TEKIYOU_END.Value = null;
                }
                else if ((DateTime)HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE != new DateTime(0))
                {
                    this.form.TEKIYOU_END.Value = HikiaiTorihikisakiIchiran.Properties.Settings.Default.TEKIYOU_END_VALUE;
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
                    this.torihikisakiEntity = daoTorihikisaki.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                    if (this.torihikisakiEntity != null)
                    {
                        this.form.TORIHIKISAKI_RNAME.Text = torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                //都道府県をセット
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text))
                {
                    this.todoufukenEntity = daoTodoufuken.GetDataByCd(this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text);
                    this.form.TORIHIKISAKI_TODOUFUKEN_NAME.Text = this.todoufukenEntity.TODOUFUKEN_NAME_RYAKU;
                }

                //アラート件数の初期値セット
                if (!sysinfoEntity.ICHIRAN_ALERT_KENSUU.IsNull)
                {
                    this.headerForm.alertNumber.Text = this.sysinfoEntity.ICHIRAN_ALERT_KENSUU.ToString();
                }

                //アラートの保存データがあればそちらを表示
                if (HikiaiTorihikisakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU != "")
                {
                    this.headerForm.alertNumber.Text = HikiaiTorihikisakiIchiran.Properties.Settings.Default.ICHIRAN_ALERT_KENSUU;
                }
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

                string formID_M461 = "M461";

                // 引数へのオブジェクトを作成する
                // 新規の場合は引数なし、ただし参照の場合は引数あり
                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
                {
                    FormManager.OpenFormWithAuth(formID_M461, WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
                }
                else
                {
                    // 表示されている行の取引先CDを取得する
                    DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                    if (row != null)    // No.3988
                    {
                        string cd1 = row.Cells[this.KEY_ID1].Value.ToString();

                        // データ削除チェックを行う
                        if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
                        {
                            M_HIKIAI_TORIHIKISAKI data = this.daoTorihikisaki.GetDataByCd(cd1);
                            if (data.DELETE_FLG)
                            {
                                //msgLogic.MessageBoxShow("E026", "コード");
                                //LogUtility.DebugMethodEnd();
                                //return;

                                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                                {
                                    //20150929 hoanghm #1931 start
                                    //MessageBoxUtility.MessageBoxShow("E202", "選択した取引先は");
                                    //LogUtility.DebugMethodEnd();
                                    //return;
                                    // 本登録済みデータチェック

                                    if (this.ExistedTorihikisaki(cd1))
                                    {
                                        var messageShowLogic = new MessageBoxShowLogic();
                                        messageShowLogic.MessageBoxShow("E202", "既に");
                                        return ret;
                                    }

                                    var result = MessageBoxUtility.MessageBoxShow("C057");
                                    if (result != DialogResult.Yes)
                                    {
                                        return ret;
                                    }
                                    //20150929 hoanghm #1931 end
                                }
                                else
                                {
                                    MessageBoxUtility.MessageBoxShow("E026", "コード");
                                    return ret;
                                }
                            }
                        }

                        switch (windowType)
                        {
                            case WINDOW_TYPE.NEW_WINDOW_FLAG:
                                // 複写モードで起動（新規モード）
                                FormManager.OpenFormWithAuth(formID_M461, WINDOW_TYPE.NEW_WINDOW_FLAG, windowType, cd1);
                                break;
                            case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                                // 修正モードの権限チェック
                                if (Manager.CheckAuthority(formID_M461, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                                {
                                    FormManager.OpenFormWithAuth(formID_M461, WINDOW_TYPE.UPDATE_WINDOW_FLAG, windowType, cd1);
                                }
                                // 参照モードの権限チェック
                                else if (Manager.CheckAuthority(formID_M461, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                                {
                                    FormManager.OpenFormWithAuth(formID_M461, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, cd1);
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
                                FormManager.OpenFormWithAuth(formID_M461, WINDOW_TYPE.DELETE_WINDOW_FLAG, windowType, cd1);
                                break;
                            default:
                                break;
                        }
                    }
                }
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
                        result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = {0}", strTemp);
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
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD = '{0}'", strTemp);
                }

                // フリガナ
                strTemp = this.form.TORIHIKISAKI_FURIGANA.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%{0}%'", strTemp);
                }

                // 取引先名1
                strTemp = this.form.TORIHIKISAKI_NAME1.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME1 LIKE '%{0}%'", strTemp);
                }

                // 取引先名2
                strTemp = this.form.TORIHIKISAKI_NAME2.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME2 LIKE '%{0}%'", strTemp);
                }

                // 略称名
                strTemp = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%'", strTemp);
                }

                // 都道府県CD
                strTemp = this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = {0}", strTemp);
                }

                // 住所
                strTemp = this.form.TORIHIKISAKI_ADDRESS.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1 + M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_ADDRESS2) LIKE '%{0}%'", strTemp);
                }

                // 適用開始
                strTemp = this.form.TEKIYOU_BEGIN.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TEKIYOU_END >= CONVERT(date,'{0}') OR M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)", strTemp);
                }

                // 適用終了
                strTemp = this.form.TEKIYOU_END.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TEKIYOU_END <= CONVERT(date,'{0}') OR M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)", strTemp);
                }

                // 表示条件
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.Append(" M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0");
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
                    result.Append(" OR (((M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN IS NULL and M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)) and M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0)");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    result.Append(" OR M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 1");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    result.Append(" OR ((M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) and M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0)");
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
                    this.form.Table = this.daoTorihikisaki.GetDateForStringSql(sql);
                }

                this.form.ShowData();

                return this.form.Table != null ? this.form.Table.Rows.Count : 0;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
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
                sb.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD AS {0}, ", this.KEY_ID1);
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

            // 取引先マスタ
            sb.Append(" FROM M_HIKIAI_TORIHIKISAKI ");

            // 取引先請求マスタ
            sb.Append(" LEFT JOIN M_HIKIAI_TORIHIKISAKI_SEIKYUU ");
            sb.Append(" ON M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD = M_HIKIAI_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ");

            // 取引先支払マスタ
            sb.Append(" LEFT JOIN M_HIKIAI_TORIHIKISAKI_SHIHARAI ");
            sb.Append(" ON M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD = M_HIKIAI_TORIHIKISAKI_SHIHARAI.TORIHIKISAKI_CD ");

            //振込銀行1
            sb.Append(" LEFT JOIN M_BANK M_BANK1 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = M_BANK1.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN M_BANK_SHITEN1 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD = M_BANK_SHITEN1.BANK_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD = M_BANK_SHITEN1.BANK_SHITEN_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_SHURUI = M_BANK_SHITEN1.KOUZA_SHURUI AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_NO = M_BANK_SHITEN1.KOUZA_NO ");

            //振込銀行2
            sb.Append(" LEFT JOIN M_BANK M_BANK2 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2 = M_BANK2.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN M_BANK_SHITEN2 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_2 = M_BANK_SHITEN2.BANK_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_2 = M_BANK_SHITEN2.BANK_SHITEN_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_2 = M_BANK_SHITEN2.KOUZA_SHURUI AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_NO_2 = M_BANK_SHITEN2.KOUZA_NO ");

            //振込銀行3
            sb.Append(" LEFT JOIN M_BANK M_BANK3 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3 = M_BANK3.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN M_BANK_SHITEN3 ON M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_CD_3 = M_BANK_SHITEN3.BANK_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.FURIKOMI_BANK_SHITEN_CD_3 = M_BANK_SHITEN3.BANK_SHITEN_CD  ");
            sb.Append("         AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_SHURUI_3 = M_BANK_SHITEN3.KOUZA_SHURUI AND M_HIKIAI_TORIHIKISAKI_SEIKYUU.KOUZA_NO_3 = M_BANK_SHITEN3.KOUZA_NO ");

            //160027 S
            sb.Append(" LEFT JOIN M_BANK M_BANK4 ON M_HIKIAI_TORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD = M_BANK4.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN M_BANK_SHITEN4 ON M_HIKIAI_TORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_CD = M_BANK_SHITEN4.BANK_CD ");
            sb.Append("     AND M_HIKIAI_TORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_BANK_SHITTEN_CD = M_BANK_SHITEN4.BANK_SHITEN_CD AND M_HIKIAI_TORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_SHURUI = M_BANK_SHITEN4.KOUZA_SHURUI ");
            sb.Append("     AND M_HIKIAI_TORIHIKISAKI_SHIHARAI.FURI_KOMI_MOTO_NO = M_BANK_SHITEN4.KOUZA_NO ");
            //160027 E

            // パターンから生成したJOIN句
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
                        result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = {0}", strTemp);
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
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD = '{0}'", strTemp);
                }

                // フリガナ
                strTemp = this.form.TORIHIKISAKI_FURIGANA.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%{0}%'", strTemp);
                }

                // 取引先名1
                strTemp = this.form.TORIHIKISAKI_NAME1.Text;
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME1 LIKE '%{0}%'", strTemp);
                }

                // 取引先名2
                strTemp = this.form.TORIHIKISAKI_NAME2.Text;
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME2 LIKE '%{0}%'", strTemp);
                }

                // 略称名
                strTemp = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU LIKE '%{0}%'", strTemp);
                }

                // 都道府県CD
                strTemp = this.form.TORIHIKISAKI_TODOUFUKEN_CD.Text;
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_TODOUFUKEN_CD = {0}", strTemp);
                }

                // 住所
                strTemp = this.form.TORIHIKISAKI_ADDRESS.Text;
                strTemp = SqlCreateUtility.CounterplanEscapeSequence(strTemp);
                if (!string.IsNullOrWhiteSpace(strTemp))
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_ADDRESS1 + M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_ADDRESS2) LIKE '%{0}%'", strTemp);
                }

                // 適用開始
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_BEGIN.Text))
                {
                    strTemp = Convert.ToString(this.form.TEKIYOU_BEGIN.Value);
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN >= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TEKIYOU_END >= CONVERT(date,'{0}') OR M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)", strTemp);
                }

                // 適用終了
                if (!string.IsNullOrWhiteSpace(this.form.TEKIYOU_END.Text))
                {
                    strTemp = Convert.ToString(this.form.TEKIYOU_END.Value);
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.AppendFormat(" M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(date,'{0}')", strTemp);
                    result.Append(" AND ");
                    result.AppendFormat(" (M_HIKIAI_TORIHIKISAKI.TEKIYOU_END <= CONVERT(date,'{0}') OR M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)", strTemp);
                }

                // 表示条件
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(result.ToString()))
                    {
                        result.Append(" AND ");
                    }
                    result.Append(" M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0");
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
                    result.Append(" OR (((M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) or (M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN IS NULL and M_HIKIAI_TORIHIKISAKI.TEKIYOU_END IS NULL)) and M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0)");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    result.Append(" OR M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 1");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    result.Append(" OR ((M_HIKIAI_TORIHIKISAKI.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > M_HIKIAI_TORIHIKISAKI.TEKIYOU_END) and M_HIKIAI_TORIHIKISAKI.DELETE_FLG = 0)");
                }
                if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    result.Append(")");
                }

                return result.Length > 0 ? result.Insert(0, " WHERE ").ToString() : string.Empty;
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

        /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　start
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
        /// 20141203 Houkakou 「引合取引先一覧」の日付チェックを追加する　end

        #region 本登録済みか判定
        /// <summary>
        /// 指定された取引先CDが本登録済みデータか判定
        /// </summary>
        /// <param name="torihikisakiCD">取引先CD</param>
        /// <returns>true:登録済み, false:未登録</returns>
        internal bool ExistedTorihikisaki(string torihikisakiCD)
        {
            if (string.IsNullOrEmpty(torihikisakiCD))
            {
                return false;
            }

            var entity = daoTorihikisaki.GetDataByCd(torihikisakiCD);
            if (entity != null && !string.IsNullOrEmpty(entity.TORIHIKISAKI_CD_AFTER))
            {
                // 移行後取引先CDに値がある場合は、本登録済みデータとみなす
                return true;
            }

            return false;
        }
        #endregion
    }
}