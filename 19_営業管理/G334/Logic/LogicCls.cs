using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.APP;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Dao;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Dto;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Entity;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic, IDisposable
    {
        #region 内部変数

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 取引履歴一覧検索条件
        /// </summary>
        private SearchDTOClass whereDto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private TorihikiRirekiItiranDao daoIchiran;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// DGV表示データ有無
        /// </summary>
        bool dispDataRecord = false;

        /// <summary>
        /// 現場CDチェック用のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        private MessageBoxShowLogic MsgBox;

        #endregion

        #region プロパティ

        /// <summary>
        /// 取引履歴一覧表示データ
        /// </summary>
        public DataTable ResultTbl { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム取得
            this.form = targetForm;
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // フォームの初期化処理
                this.FormInit();

                // DTO初期化
                this.DaoInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 入力フィールドの初期化
                this.SetInitValue();

                // 0件時の画面設定
                this.SetNoDataDisplay();

                // フォーカス設定
                this.form.HeaderForm.Select();
                this.form.HeaderForm.Focus();
                this.form.HeaderForm.HIDUKE_FROM.Select();
                this.form.HeaderForm.HIDUKE_FROM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region フォームの初期化

        /// <summary>
        /// フォーム初期化処理
        /// </summary>
        /// <returns></returns>
        private void FormInit()
        {
            LogUtility.DebugMethodStart();

            // ベースタイトル設定
            var title = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            // ウインドウタイトル設定
            var parentForm = this.form.ParentBaseForm;
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(title);

            //プロセスボタンを非表示設定
            parentForm.ProcessButtonPanel.Visible = false;

            // ヘッダタイトル設定
            var headerForm = this.form.HeaderForm;
            headerForm.lb_title.Text = title;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            parentForm.bt_func1.Enabled = true;
            parentForm.bt_func2.Enabled = false;
            parentForm.bt_func3.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func5.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = false;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func11.Enabled = false;
            parentForm.bt_func12.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ButtonSetting[] res = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            LogUtility.DebugMethodStart(res);
            return res;
        }

        #endregion

        #region イベントの初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            // Functionボタンのイベント生成
            var parentForm = this.form.ParentBaseForm;
            parentForm.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);            //参照
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);          //閉じる

            // 業者CD、現場CDのロストフォーカスイベント生成(実装ストップ)
            //this.form.numTxtBox_GyousyaCD.Validated += new System.EventHandler(this.numTxtBox_GyousyaCD_Validated);
            //this.form.numTxtBox_GbCD.Validated += new System.EventHandler(this.numTxtBox_GbCD_Validated);

            // 画面表示イベント
            parentForm.Shown += new EventHandler(UIForm_Shown);

            // グリッドのイベント生成
            this.form.customDataGridView1.CellContentDoubleClick += new DataGridViewCellEventHandler(this.CellContentDoubleClick);

            // 20141127 teikyou ダブルクリックを追加する　start
            this.form.HeaderForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            // 20141127 teikyou ダブルクリックを追加する　end

            /// 20141203 Houkakou 「取引履歴」の日付チェックを追加する　start
            this.form.HeaderForm.HIDUKE_FROM.Leave += new System.EventHandler(HIDUKE_FROM_Leave);
            this.form.HeaderForm.HIDUKE_TO.Leave += new System.EventHandler(HIDUKE_TO_Leave);
            /// 20141203 Houkakou 「取引履歴」の日付チェックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベント処理の削除を行う
        /// </summary>
        private void EventDelete()
        {
            LogUtility.DebugMethodStart();

            // Functionボタンのイベント解除
            var parentForm = this.form.ParentBaseForm;
            parentForm.bt_func1.Click -= new System.EventHandler(this.bt_func1_Click);            //参照
            parentForm.bt_func6.Click -= new System.EventHandler(this.bt_func6_Click);            //CSV
            parentForm.bt_func7.Click -= new System.EventHandler(this.bt_func7_Click);            //検索条件クリア
            parentForm.bt_func8.Click -= new System.EventHandler(this.bt_func8_Click);            //検索
            parentForm.bt_func10.Click -= new System.EventHandler(this.bt_func10_Click);          //並び替え
            parentForm.bt_func12.Click -= new System.EventHandler(this.bt_func12_Click);          //閉じる

            // 業者CD、現場CDのロストフォーカスイベント解除(実装ストップ)
            //this.form.numTxtBox_GyousyaCD.Validated -= new System.EventHandler(this.numTxtBox_GyousyaCD_Validated);
            //this.form.numTxtBox_GbCD.Validated -= new System.EventHandler(this.numTxtBox_GbCD_Validated);

            // 画面表示イベント解除
            parentForm.Shown -= new EventHandler(UIForm_Shown);

            // グリッドのイベント解除
            this.form.customDataGridView1.CellContentDoubleClick -= new DataGridViewCellEventHandler(this.CellContentDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Daoの初期化

        /// <summary>
        /// Daoの初期化
        /// </summary>
        private void DaoInit()
        {
            LogUtility.DebugMethodStart();

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoIchiran = DaoInitUtility.GetComponent<TorihikiRirekiItiranDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 入力コントロールの初期設定

        /// <summary>
        /// 入力コントロールへ初期値設定
        /// </summary>
        private void SetInitValue()
        {
            LogUtility.DebugMethodStart();

            // ヘッダクラス取得
            var headerForm = this.form.HeaderForm;

            // 伝票日付
            headerForm.HIDUKE_FROM.Value = this.form.ParentBaseForm.sysDate.AddMonths(-3);
            headerForm.HIDUKE_TO.Value = this.form.ParentBaseForm.sysDate;

            // アラート件数
            M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
            if (sysInfo != null)
            {
                // システム情報からアラート件数を取得
                headerForm.alertNumber.Text = SetComma(sysInfo.ICHIRAN_ALERT_KENSUU.ToString());
            }

            // 読込データ件数
            headerForm.ReadDataNumber.Text = "0";


            // フォームクラス取得
            var form = this.form;

            // 営業担当者
            form.numTxtBox_EigyoTantosyaCD.Text = "";
            form.txtBox_Eigyosyamei.Text = "";

            // 取引先
            form.numTxtbox_TrhkskCD.Text = "";
            form.txtBox_TrhkskName.Text = "";

            // 業者
            form.numTxtBox_GyousyaCD.Text = "";
            form.txtBox_GyousyaName.Text = "";

            // 現場
            form.numTxtBox_GbCD.Text = "";
            form.txtBox_GbName.Text = "";

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [Fキー押下]イベント処理

        /// <summary>
        /// F1 参照
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.dispDataRecord)
                {
                    // 画面ポップアップ
                    this.ShowDenpyouWindowPopup();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
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

                // エラーメッセージクラス
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // データが0件の場合
                if (!this.dispDataRecord)
                {
                    msgLogic.MessageBoxShow("E044", "CSV出力");
                    return;
                }

                // CSV出力
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(this.form.WindowId), this.form);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
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

                // 入力フィールドの初期化
                this.SetInitValue();

                // ゼロ件表示
                this.SetNoDataDisplay();

                // ソート条件のクリア
                this.form.customSortHeader1.ClearCustomSortSetting();

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
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

                // 入力チェック
                if (this.CheckInputDate())
                {
                    // 検索
                    if (0 < this.Search())
                    {
                        // 画面反映
                        this.SetResultData();

                    }
                    else
                    {
                        // メッセージ表示後 → 画面クリアの順

                        // ゼロ件メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001", "検索結果");

                        // ゼロ件表示
                        this.SetNoDataDisplay();
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
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
                // 無条件に表示でよい
                this.form.customSortHeader1.ShowCustomSortSettingDialog();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

                this.form.ParentBaseForm.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region グリッドイベント処理

        /// <summary>
        /// セルダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                CustomDataGridView grid = sender as CustomDataGridView;
                if (grid != null)
                {
                    // 画面ポップアップ
                    this.ShowDenpyouWindowPopup(e.RowIndex, e.ColumnIndex);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 初期表示イベント

        /// <summary>
        /// 表示イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // フォーカス設定
            this.form.HeaderForm.Select();
            this.form.HeaderForm.Focus();
            this.form.HeaderForm.HIDUKE_FROM.Select();
            this.form.HeaderForm.HIDUKE_FROM.Focus();
        }
        #endregion

        #region チェック処理

        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckInputDate()
        {
            LogUtility.DebugMethodStart();

            // 必須チェック 伝票日付(TO) 
            if (string.IsNullOrWhiteSpace(this.form.HeaderForm.HIDUKE_TO.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "伝票日付");
                this.form.HeaderForm.Select();
                this.form.HeaderForm.Focus();
                this.form.HeaderForm.HIDUKE_TO.Select();
                this.form.HeaderForm.HIDUKE_TO.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 必須チェック 伝票日付(FROM) 
            if (string.IsNullOrWhiteSpace(this.form.HeaderForm.HIDUKE_FROM.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "伝票日付");
                this.form.HeaderForm.Select();
                this.form.HeaderForm.Focus();
                this.form.HeaderForm.HIDUKE_FROM.Select();
                this.form.HeaderForm.HIDUKE_FROM.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 整合性チェック 伝票日付 
            DateTime toDate = (DateTime)this.form.HeaderForm.HIDUKE_TO.Value;
            DateTime fromDate = (DateTime)this.form.HeaderForm.HIDUKE_FROM.Value;

            // 時間情報削除
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day);
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);

            if (fromDate > toDate)  
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //msgLogic.MessageBoxShow("E043", "日付範囲");
                //this.form.HeaderForm.Select();
                //this.form.HeaderForm.Focus();
                //this.form.HeaderForm.HIDUKE_FROM.Select();
                //this.form.HeaderForm.HIDUKE_FROM.Focus();
                this.form.HeaderForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.form.HeaderForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.form.HeaderForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HeaderForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "伝票日付From", "伝票日付To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.HeaderForm.HIDUKE_FROM.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 必須チェック 営業担当者
            if (string.IsNullOrWhiteSpace(this.form.numTxtBox_EigyoTantosyaCD.Text.Trim()))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E001", "営業担当者");
                this.form.Select();
                this.form.Focus();
                this.form.numTxtBox_EigyoTantosyaCD.Select();
                this.form.numTxtBox_EigyoTantosyaCD.Focus();
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 検索・検索結果反映

        /// <summary>
        /// 取引履歴一覧検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int res = 0;
            LogUtility.DebugMethodStart();

            // 表示データ初期化
            List<T_DISPLAY_ROW> resultList = new List<T_DISPLAY_ROW>();

            // 条件設定
            this.whereDto = new SearchDTOClass();
            this.whereDto.Import(this.form);

            // 検索
            List<T_RESULT_SQL> list = this.daoIchiran.GetTorihikiRirekiItiranDataAllForEntity(this.whereDto);

            // グループ化(取引先CD、業者CD、現場CD)、昇順
            var grouplist = list.GroupBy(t => new { t.TORIHIKISAKI_CD, t.GYOUSHA_CD, t.GENBA_CD })
                .ToList()
                .OrderBy(t => t.Key.TORIHIKISAKI_CD)
                .ThenBy(t => t.Key.GYOUSHA_CD)
                .ThenBy(t => t.Key.GENBA_CD)
                .ToList();

            // グループループ
            foreach (var group in grouplist)
            {
                // グループに属するレコード取得
                var whereResult = list.Where(t => group.Key.TORIHIKISAKI_CD.Equals(t.TORIHIKISAKI_CD))
                                    .Where(t => group.Key.GYOUSHA_CD.Equals(t.GYOUSHA_CD))
                                    .Where(t => group.Key.GENBA_CD.Equals(t.GENBA_CD))
                                    .ToList();

                var test = whereResult.Where(t => t.DENPYOU_NUMBER.Equals(93)).ToList();

                // グループ情報を取得
                T_RESULT_SQL groupInfo = whereResult.FirstOrDefault();

                // グループ情報を設定
                T_DISPLAY_ROW resultRow = new T_DISPLAY_ROW();
                resultRow.TORIHIKISAKI_CD = groupInfo.TORIHIKISAKI_CD;
                resultRow.TORIHIKISAKI_NAME_RYAKU = groupInfo.TORIHIKISAKI_NAME_RYAKU;
                resultRow.GYOUSHA_CD = groupInfo.GYOUSHA_CD;
                resultRow.GYOUSHA_NAME_RYAKU = groupInfo.GYOUSHA_NAME_RYAKU;
                resultRow.GENBA_CD = groupInfo.GENBA_CD;
                resultRow.GENBA_NAME_RYAKU = groupInfo.GENBA_NAME_RYAKU;

                // さらに伝票日付でグループ化
                var denpyoGrouplist = whereResult.GroupBy(u => u.DENPYOU_DATE)
                        .ToList()
                        .OrderByDescending(u => u.Key)
                        .ToList();

                // グループループ
                foreach (var denpyogroup in denpyoGrouplist)
                {
                    // 伝票日付でグループ化し、「作成日付」降順で並び替え
                    var denpyouInfoList = whereResult.Where(x => denpyogroup.Key.Equals(x.DENPYOU_DATE))
                        .OrderByDescending(x => x.CREATE_DATE)
                        .ToList();

                    foreach (var denpyouInfo in denpyouInfoList)
                    {
                        // 伝票データの設定
                        T_DENPYOU_DATA denpyoData = new T_DENPYOU_DATA();
                        denpyoData.DENPYOU_DATE = (DateTime)denpyouInfo.DENPYOU_DATE;
                        denpyoData.DENPYOU_KBN = (DenpyouKbn)denpyouInfo.DENPYOU_KBN;
                        denpyoData.DENPYOU_NUMBER = denpyouInfo.DENPYOU_NUMBER;
                        denpyoData.DAINOU_FLG = denpyouInfo.DAINOU_FLG.IsTrue;

                        // 伝票登録
                        resultRow.DENPYOU_LIST.Add(denpyoData);
                    }
                }

                // 取引間隔の算出
                resultRow.DEAL_AVERAGE = this.GetDateIntervalAverage(resultRow.DENPYOU_LIST.Select(t => t.DENPYOU_DATE).ToList<DateTime>());

                // 検索結果に登録
                resultList.Add(resultRow);
            }

            // ListからDataTableへ変換
            this.ResultTbl = this.ConvertDataTable(resultList);

            // グリッドレコード数
            res = ResultTbl.Rows.Count;

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// DataTable型へ変換
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable ConvertDataTable(List<T_DISPLAY_ROW> list)
        {
            LogUtility.DebugMethodStart(list);

            // テーブル作成
            DataTable tbl = new DataTable();
            //#受入試験指摘修正(数値表示項目のソート)Start
            //foreach (DataGridViewColumn colInfo in this.form.customDataGridView1.Columns)
            //{
            //    tbl.Columns.Add(colInfo.Name, Type.GetType("System.String"));
            //}
            foreach (DataGridViewColumn colInfo in this.form.customDataGridView1.Columns)
            {
                if (colInfo.Name.Equals("DEAL_AVERAGE"))
                {
                    tbl.Columns.Add(colInfo.Name, Type.GetType("System.Double"));
                }
                else
                {
                    tbl.Columns.Add(colInfo.Name, Type.GetType("System.String"));
                }

            }
            //#受入試験指摘修正(数値表示項目のソート)End

            // 表示データ設定
            foreach (T_DISPLAY_ROW item in list)
            {
                DataRow newRow = tbl.NewRow();
                newRow["TORIHIKISAKI_CD"] = item.TORIHIKISAKI_CD;
                newRow["TORIHIKISAKI_NAME_RYAKU"] = item.TORIHIKISAKI_NAME_RYAKU;
                newRow["GYOUSHA_CD"] = item.GYOUSHA_CD;
                newRow["GYOUSHA_NAME_RYAKU"] = item.GYOUSHA_NAME_RYAKU;
                newRow["GENBA_CD"] = item.GENBA_CD;
                newRow["GENBA_NAME_RYAKU"] = item.GENBA_NAME_RYAKU;
                newRow["DEAL_AVERAGE"] = decimal.Parse(item.DEAL_AVERAGE.ToString("0.0"));

                // 取引日ループ
                for (int j = 0; j < item.DENPYOU_LIST.Count && j < 5; j++)
                {
                    // 取引データ取得
                    T_DENPYOU_DATA denpyoRow = item.DENPYOU_LIST[j];

                    // グリッド設定
                    string timesFieldName = string.Format("DEAL_{0}_TIMES_AGO", j + 1);
                    newRow[timesFieldName] = ((DateTime)denpyoRow.DENPYOU_DATE).ToString("yyyy/MM/dd");

                    // 隠しフィールド
                    string hiddenFieldName = string.Format("HIDDEN_DATA_{0}", j + 1);
                    string hiddenValue = string.Format("{0},{1},{2}", (int)denpyoRow.DENPYOU_KBN, denpyoRow.DENPYOU_NUMBER, denpyoRow.DAINOU_FLG);
                    newRow[hiddenFieldName] = hiddenValue;
                }

                tbl.Rows.Add(newRow);
            }

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }

        /// <summary>
        /// 日付間隔の平均を求める
        /// </summary>
        /// <param name="dateList"></param>
        /// <returns></returns>
        private decimal GetDateIntervalAverage(IList<DateTime> dateList)
        {
            LogUtility.DebugMethodStart(dateList);

            decimal res = 0m;

            int total = 0;
            int cnt = 0;

            if (dateList.Count > 1)
            {
                DateTime access1 = dateList[0];

                int i = 1;
                for (; i < 5 && i < dateList.Count; i++)
                {
                    DateTime access2 = dateList[i];
                    int days = Math.Abs((access1 - access2).Days);
                    total += days;
                    cnt++;
                    access1 = access2;
                }
                res = Math.Round((decimal)total / (decimal)cnt, 1, MidpointRounding.AwayFromZero);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 検索データ反映
        /// </summary>
        private void SetResultData()
        {
            LogUtility.DebugMethodStart();

            // 表示判定
            DialogResult dlgRes = DialogResult.Yes;

            // アラート件数判断
            int alertCount = this.form.HeaderForm.AlertCount;
            if (alertCount != 0 && alertCount < this.ResultTbl.Rows.Count)
            {
                MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                dlgRes = showLogic.MessageBoxShow("C025");
            }

            // 表示処理
            if (DialogResult.Yes == dlgRes)
            {
                //表示データあり
                this.dispDataRecord = true;

                // 検索結果数設定
                this.form.HeaderForm.ReadDataNumber.Text = this.ResultTbl.Rows.Count.ToString();

                // ソートデータ登録
                this.form.customSortHeader1.SortDataTable(this.ResultTbl);

                // グリッド初期化、設定
                this.form.customDataGridView1.DataSource = this.ResultTbl;
                this.form.customDataGridView1.Refresh();
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region データ0件時の表示処理

        /// <summary>
        /// 表示データ0件時／初期起動時の表示処理
        /// </summary>
        void SetNoDataDisplay()
        {
            LogUtility.DebugMethodStart();

            //表示データなし
            this.dispDataRecord = false;

            // 結果レコード数0更新
            this.form.HeaderForm.ReadDataNumber.Text = "0";

            // 検索結果初期化
            DataTable tbl = new DataTable();
            foreach (DataGridViewColumn colInfo in this.form.customDataGridView1.Columns)
            {
                tbl.Columns.Add(colInfo.Name, Type.GetType("System.String"));
            }
            //tbl.Rows.Add(); // 空行追加
            this.ResultTbl = tbl;

            // 結果レコード登録
            this.form.customDataGridView1.DataSource = this.ResultTbl;
            this.form.customDataGridView1.Refresh();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 伝票画面表示

        /// <summary>
        /// 伝票詳細画面表示
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void ShowDenpyouWindowPopup()
        {
            LogUtility.DebugMethodStart();

            int rowIndex = -1;
            int colIndex = -1;
            foreach (DataGridViewCell c in this.form.customDataGridView1.SelectedCells)
            {
                rowIndex = c.RowIndex;
                colIndex = c.ColumnIndex;
            }
            ShowDenpyouWindowPopup(rowIndex, colIndex);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 伝票詳細画面表示
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void ShowDenpyouWindowPopup(int rowIndex, int colIndex)
        {
            LogUtility.DebugMethodStart(rowIndex, colIndex);

            if (rowIndex >= 0 && colIndex >= 6 && colIndex <= 10)
            {
                string target = this.form.customDataGridView1.Rows[rowIndex].Cells[colIndex + 6].Value as string;

                if (!string.IsNullOrWhiteSpace(target))
                {
                    string[] popUpData = target.Split(',');

                    if (popUpData.Length == 3)
                    {
                        int denpyoKbn;
                        string denpyoNumber = popUpData[1];
                        bool dainouFlg = Convert.ToBoolean(popUpData[2]);

                        if (int.TryParse(popUpData[0], out denpyoKbn))
                        {
                            switch (denpyoKbn)
                            {
                                case (int)DenpyouKbn.Ukeire:
                                    // 受入入力(参照モード)
                                    FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyoNumber);
                                    break;

                                case (int)DenpyouKbn.Syukka:
                                    // 出荷入力(参照モード)
                                    FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyoNumber);
                                    break;

                                case (int)DenpyouKbn.UriageSiharai:
                                    if (dainouFlg)
                                    {
                                        // 代納入力(参照モード)
                                        FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyoNumber);
                                    }
                                    else
                                    {
                                        // 売上・支払入力(参照モード)
                                        FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyoNumber);
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd(rowIndex, colIndex);
        }

        #endregion

        #region 共通処理

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            LogUtility.DebugMethodStart(value);

            string res = "0";
            if (!string.IsNullOrEmpty(value))
            {
                res = string.Format("{0:#,0}", Convert.ToDecimal(value));
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            LogUtility.DebugMethodStart(param);

            Regex re = new Regex("[　]+");
            string output = re.Replace(param, MyReplacer);

            LogUtility.DebugMethodEnd(output);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            LogUtility.DebugMethodStart(param);

            Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
            string output = re.Replace(param, MyReplacer);

            LogUtility.DebugMethodEnd(output);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        static string MyReplacer(Match m)
        {
            LogUtility.DebugMethodStart(m);

            string res = Strings.StrConv(m.Value, VbStrConv.Narrow, 0);

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region IDisposable

        /// <summary>
        /// 開放処理
        /// </summary>
        public void Dispose()
        {
            this.EventDelete();
        }

        #endregion

        #region コメントアウト：排出事業場ロストフォーカス処理(実装ストップ）

        ///// <summary>
        ///// 業者CD検証後メソッド
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void numTxtBox_GyousyaCD_Validated(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        var msgLogic = new MessageBoxShowLogic();

        //        // (親)業者CD未入力チェック
        //        if (this.form.numTxtBox_GyousyaCD.Text.Equals(""))
        //        {
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //        }

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        ///// <summary>
        ///// 現場CD検証後メソッド
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void numTxtBox_GbCD_Validated(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        var msgLogic = new MessageBoxShowLogic();

        //        // (子)現場CD未入力
        //        if (this.form.numTxtBox_GbCD.Text.Equals(""))
        //        {
        //            //未入力の場合は処理なし
        //            this.form.numTxtBox_GbCD.IsInputErrorOccured = false;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            return;
        //        }

        //        // (親)業者CD未入力チェック
        //        // 現場CDが設定されている場合であり、業者CDが空文字の場合
        //        if (this.form.numTxtBox_GyousyaCD.Text.Equals(""))
        //        {
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            msgLogic.MessageBoxShow("E001", "業者");
        //            this.form.numTxtBox_GyousyaCD.Focus();
        //            return;
        //        }

        //        //現場マスタ検索
        //        M_GENBA mGenba = new M_GENBA();
        //        mGenba.GENBA_CD = this.form.numTxtBox_GbCD.Text;
        //        mGenba.GYOUSHA_CD = this.form.numTxtBox_GyousyaCD.Text;
        //        mGenba = genbaDao.GetDataByCd(mGenba);

        //        if (mGenba != null)
        //        {
        //            this.form.txtBox_GbName.Text = mGenba.GENBA_NAME_RYAKU;
        //        }
        //        else
        //        {
        //            this.form.txtBox_GbName.Text = string.Empty;
        //            this.form.numTxtBox_GbCD.Text = string.Empty;
        //            msgLogic.MessageBoxShow("E020", "現場");
        //            this.form.numTxtBox_GbCD.Focus();
        //            this.form.numTxtBox_GbCD.IsInputErrorOccured = true;
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }

        //}

        #endregion

        #region インターフェース処理(未実装)

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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　start
        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            LogUtility.DebugMethodStart();

            // 初期化
            this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = string.Empty;

            if (string.IsNullOrEmpty(this.form.HeaderForm.txtBox_KyotenCd.Text))
            {
                this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", this.form.HeaderForm.txtBox_KyotenCd.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.form.accessor.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "拠点");
                this.form.HeaderForm.txtBox_KyotenCd.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.form.HeaderForm.txtBox_KyotenNameRyaku.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }

            LogUtility.DebugMethodEnd();
        }
        // 20140717 syunrei EV005298_システム設定-個別メニューの拠点を参照していない。　end
        #endregion

        /// <summary>
        /// 取引先を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先エンティティ</returns>
        internal M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            M_TORIHIKISAKI ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                var dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                ret = dao.GetAllValidData(new M_TORIHIKISAKI() { TORIHIKISAKI_CD = torihikisakiCd, ISNOT_NEED_DELETE_FLG = true }).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>業者エンティティ</returns>
        internal M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr)
        {
            M_GYOUSHA ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                ret = dao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd, ISNOT_NEED_DELETE_FLG = true }).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 現場リストを取得します
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティ</returns>
        internal List<M_GENBA> GetGenbaList(string genbaCd)
        {
            LogUtility.DebugMethodStart(genbaCd);

            List<M_GENBA> ret = new List<M_GENBA>();

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            ret = dao.GetAllValidData(new M_GENBA() { GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true}).ToList();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティ</returns>
        internal M_GENBA GetGenba(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            M_GENBA ret = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true }).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var hidukeFromTextBox = this.form.HeaderForm.HIDUKE_FROM;
            var hidukeToTextBox = this.form.HeaderForm.HIDUKE_TO;
            hidukeToTextBox.Text = hidukeFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141127 teikyou ダブルクリックを追加する　end
        #endregion

        /// 20141203 Houkakou 「取引履歴」の日付チェックを追加する　start
        #region HIDUKE_FROM_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_TO.Text))
            {
                this.form.HeaderForm.HIDUKE_TO.IsInputErrorOccured = false;
                this.form.HeaderForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region HIDUKE_TO_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.HeaderForm.HIDUKE_FROM.Text))
            {
                this.form.HeaderForm.HIDUKE_FROM.IsInputErrorOccured = false;
                this.form.HeaderForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「取引履歴」の日付チェックを追加する　end
    }
}
