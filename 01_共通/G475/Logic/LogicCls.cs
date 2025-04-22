// $Id: LogicCls.cs 40512 2015-01-23 08:35:41Z huangxy@oec-h.com $
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.ItakuKeiyakuSearch.Dao;

namespace Shougun.Core.Common.ItakuKeiyakuSearch
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class ItakuKeiyakuSearchLogic : IBuisinessLogic
    {
        /// <summary>
        /// DTO
        /// </summary>
        private ItakuKeiyakuSearchDto dto;

        /// <summary>
        /// DAO
        /// </summary>
        private ItakuKeiyakuSearchDao dao;
        private IM_SYS_INFODao sysInfoDao;
        private IM_GENBADao GENBADao;
        #region 削除
        ///// <summary>
        ///// 業者DAO
        ///// </summary>
        //private IM_GyoushaDao GyoushaInfoDao;
        ///// <summary>
        ///// 現場DAO
        ///// </summary>
        //private IM_GenbaDao GenbaInfoDao;
        #endregion

        /// <summary>
        ///業者検索結果
        /// </summary>
        internal DataTable GyoushaSearchResult { get; set; }
        internal DataTable GyoushaInfoSearchResult { get; set; }

        /// <summary>
        /// Form
        /// </summary>
        private ItakuKeiyakuSearchForm form;

        /// <summary>
        /// 部署マスタデータ取得テーブル
        /// </summary>
        DataTable bushoResult { get; set; }

        /// <summary>
        /// 自社情報マスタデータ取得テーブル
        /// </summary>
        DataTable corpResult { get; set; }
        
        /// <summary>
        /// DataTable
        /// </summary>
        DataTable ResultTable { get; set; }

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Common.ItakuKeiyakuSearch.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headerForm;

        /// <summary>
        /// アラート件数超過時
        /// </summary>
        DialogResult showBool;

        internal string gyoshaCd_Def = "";


        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItakuKeiyakuSearchLogic(ItakuKeiyakuSearchForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);
            try
            {
                this.form = targetForm;
                this.dto = new ItakuKeiyakuSearchDto();
                this.dao = DaoInitUtility.GetComponent<ItakuKeiyakuSearchDao>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                GENBADao = DaoInitUtility.GetComponent<IM_GENBADao>();
                //this.form.customDataGridView1.Rows.Add();


                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //プロセスボタンを非表示設定
                parentForm.ProcessButtonPanel.Visible = false;
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                //部署CDにFocus当てる
                this.form.tb_JigyoushaCd.Focus();
                //DGVヘッダの高さ調整
                this.form.customDataGridView1.ColumnHeadersHeight = 20;
                //DataGridViewに空レコード追加
                this.CreateNoDataRecord();


                // アラート件数
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.headerForm.alertNumber.Text = ((int)sysInfo.ICHIRAN_ALERT_KENSUU).ToString("#,0");
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }

        private void SelectCheckDto()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // ボタンの設定情報をファイルから読み込む
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();

                LogUtility.DebugMethodEnd();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //Functionボタンのイベント生成
                parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);            //[F7]条件ｸﾘｱイベント
                parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);            //[F8]検索イベント
                parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);            //[F9]確定イベント
                parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);          //[F10]並び替えイベント
                parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);          //[F12]閉じるイベント

                this.form.tb_JigyouJouCd.Validated += new EventHandler(this.form.tb_JigyouJouCd_Validated);
                this.form.tb_JigyoushaCd.Enter += new EventHandler(this.form.tb_JigyoushaCd_Enter);

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [F7]条件ｸﾘｱボタンイベント
        /// <summary>
        /// [F7]条件ｸﾘｱボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // アラート件数
                this.headerForm.alertNumber.Text = "0";
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.headerForm.alertNumber.Text = ((int)sysInfo.ICHIRAN_ALERT_KENSUU).ToString("#,0");
                }
                this.form.tb_JigyoushaCd.Text = string.Empty;
                this.form.tb_JigyoushaName.Text = string.Empty;
                this.form.tb_JigyouJouCd.Text = string.Empty;
                this.form.tb_JigyouJouName.Text = string.Empty;
                this.form.tb_UnpanshaCd.Text = string.Empty;
                this.form.tb_UnpanshaName.Text = string.Empty;
                this.form.tb_ShobunshaCd.Text = string.Empty;
                this.form.tb_ShobunshaName.Text = string.Empty;
                this.form.txtDayHani.Text = "8";
                this.form.txtStatus.Text = "6";

                this.form.customSortHeader1.ClearCustomSortSetting();
                //DGVヘッダの高さ調整
                this.form.customDataGridView1.ColumnHeadersHeight = 20;
                //DataGridViewに空レコード追加
                this.CreateNoDataRecord();

                this.form.rdbStAll.Checked = true;
                this.form.rdbDayHidukeNasi.Checked = true;
                this.dto = new ItakuKeiyakuSearchDto();
                this.form.tb_JigyoushaCd.Focus();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region [F8]検索ボタンイベント
        /// <summary>
        /// [F8]検索ボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                #region 入力値チェック
                //必須チェック(日付範囲が8:日付なしの場合は判定不要)
                if (!this.form.txtDayHani.Text.Equals("8"))
                {
                    //開始日の必須チェック
                    if (string.IsNullOrEmpty(this.form.dtpDayFrom.Text.Trim()))
                    {
                        //未入力チェック
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", this.form.lblDayHanni.Text);
                        this.form.dtpDayFrom.Focus();
                        LogUtility.DebugMethodEnd();
                        return;
                    }
                    //終了日の必須チェック
                    if (string.IsNullOrEmpty(this.form.dtpDayTo.Text.Trim()))
                    {
                        //未入力チェック
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", this.form.lblDayHanni.Text);
                        this.form.dtpDayTo.Focus();
                        LogUtility.DebugMethodEnd();
                        return;
                    }

                    //未入力チェック:日付指定範囲
                    if (string.IsNullOrEmpty(this.form.txtDayHani.Text))
                    {
                        //未入力チェック
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", "日付範囲指定");
                        this.form.txtDayHani.Focus();
                        LogUtility.DebugMethodEnd();
                        return;
                    }
                }
                //未入力チェック:ステータス指定
                if (string.IsNullOrEmpty(this.form.txtStatus.Text))
                {
                    //未入力チェック
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "ステータス指定");
                    this.form.txtStatus.Focus();
                    LogUtility.DebugMethodEnd();
                    return;
                }
                #endregion

                #region 検索条件設定
                this.dto = new ItakuKeiyakuSearchDto();

                this.dto.JIGYOUSHA_CD = this.form.tb_JigyoushaCd.Text;
                this.dto.JIGYOUJOU_CD = this.form.tb_JigyouJouCd.Text;
                this.dto.UNPANSHA_CD = this.form.tb_UnpanshaCd.Text;
                this.dto.SHOBUNSHA_CD = this.form.tb_ShobunshaCd.Text;
                if (!string.IsNullOrEmpty(this.form.txtDayHani.Text))
                {
                    this.dto.DAY_HANI = int.Parse(this.form.txtDayHani.Text);
                }
                if (!string.IsNullOrEmpty(this.form.txtStatus.Text))
                {
                    this.dto.ITAKU_STATUS = int.Parse(this.form.txtStatus.Text);
                }
                this.dto.DAY_FROM = castTimeDate(this.form.dtpDayFrom.Text);
                this.dto.DAY_TO = castTimeDate(this.form.dtpDayTo.Text);
                #endregion

                #region DBから明細部データ取得
                
                //検索処理
                DataTable table = this.dao.GetDispDataForEntity(this.dto);

                //読込レコード件数の設定
                this.headerForm.ReadDataNumber.Text = table.Rows.Count.ToString("#,0");
                if (0 == table.Rows.Count)
                {
                    //検索結果が0件の場合、メッセージを出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                    //前の結果をクリア
                    this.CreateNoDataRecord();
                    LogUtility.DebugMethodEnd();
                    return;
                }
                //アラート件数超過時
                showBool = DialogResult.Yes;
                int alertCount = 0;
                if (!string.IsNullOrEmpty(this.headerForm.alertNumber.Text))
                {
                    alertCount = int.Parse(this.headerForm.alertNumber.Text.Replace(",", ""));
                }
                if (alertCount != 0 && alertCount < table.Rows.Count)
                {
                    MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                    showBool = showLogic.MessageBoxShow("C025");
                }
                if (showBool == DialogResult.Yes)
                {
                    //DataGridViewに検索結果を設定
                    SetDataGridView(table);
                }
                else
                {
                    //戻り値用変数の初期化
                    this.form.retHaishutsuShaCD = "";
                    this.form.retHaishutsuJouCD = "";
                    this.form.retUnpanShaCD = "";
                    this.form.retShobunShaCD = "";
                    this.form.retShobunJouCD = "";
                    this.form.retSaishuuShobunShaCD = "";
                    this.form.retSaishuuShobunJouCD = "";
                    //PhuocLoc 2022/01/18 #158902 -Start
                    this.form.retKeiyakuNumber = "";
                    this.form.retSystemId = "";
                    //PhuocLoc 2022/01/18 #158902 -End

                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    LogUtility.DebugMethodEnd();

                    //画面クローズ
                    parentForm.Close();
                }
                LogUtility.DebugMethodEnd();
                #endregion
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region [F9]確定ボタンイベント

        void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                int rowIndex = 0;
                if (0 < this.form.customDataGridView1.RowCount)
                {
                    rowIndex = this.form.customDataGridView1.SelectedCells[0].RowIndex;
                    this.form.retHaishutsuShaCD = this.form.customDataGridView1.Rows[rowIndex].Cells["HAISHUTSU_JIGYOUSHA_CD"].Value.ToString();
                    this.form.retHaishutsuJouCD = this.form.customDataGridView1.Rows[rowIndex].Cells["HAISHUTSU_JIGYOUJOU_CD"].Value.ToString();
                    this.form.retUnpanShaCD = this.form.customDataGridView1.Rows[rowIndex].Cells["UNPAN_GYOUSHA_CD"].Value.ToString();
                    this.form.retShobunShaCD = this.form.customDataGridView1.Rows[rowIndex].Cells["SHOBUN_GYOUSHA_CD"].Value.ToString();
                    this.form.retShobunJouCD = this.form.customDataGridView1.Rows[rowIndex].Cells["SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                    this.form.retSaishuuShobunShaCD = this.form.customDataGridView1.Rows[rowIndex].Cells["LAST_SHOBUN_GYOUSHA_CD"].Value.ToString();
                    this.form.retSaishuuShobunJouCD = this.form.customDataGridView1.Rows[rowIndex].Cells["LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString();
                    this.form.retSystemId = this.form.customDataGridView1.Rows[rowIndex].Cells["SYSTEM_ID"].Value.ToString();//#160047 20220328 CongBinh
                    this.form.retKeiyakuNumber = this.form.customDataGridView1.Rows[rowIndex].Cells["ITAKU_KEIYAKU_NO"].Value.ToString(); //PhuocLoc 2022/01/18 #158902
                    var parentForm = (BusinessBaseForm)this.form.Parent;
                    this.form.DialogResult = DialogResult.OK;
                    LogUtility.DebugMethodEnd(this.form.DialogResult);
                    //画面クローズ
                    parentForm.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region [F10]並び替えボタンイベント

        void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 画面反映
                this.form.customSortHeader1.ShowCustomSortSettingDialog();
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region [F12]閉じるボタンイベント
        /// <summary>
        /// [F12]閉じるボタンイベント
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="r_framework.OriginalException.EdisonException"></exception>
        void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //戻り値用変数の初期化
                this.form.retHaishutsuShaCD = "";
                this.form.retHaishutsuJouCD = "";
                this.form.retUnpanShaCD = "";
                this.form.retShobunShaCD = "";
                this.form.retShobunJouCD = "";
                this.form.retSaishuuShobunShaCD = "";
                this.form.retSaishuuShobunJouCD = "";
                //PhuocLoc 2022/01/18 #158902 -Start
                this.form.retKeiyakuNumber = "";
                this.form.retSystemId = "";
                //PhuocLoc 2022/01/18 #158902 -End

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.DialogResult = DialogResult.Cancel;
                LogUtility.DebugMethodEnd(this.form.DialogResult);

                //画面クローズ
                parentForm.Close();
            }
            catch
            {
                throw;
            }
        }

        #endregion
        #region 空白1レコードを追加
        /// <summary>
        /// 空白1レコードを追加する
        /// </summary>
        void CreateNoDataRecord()
        {
            LogUtility.DebugMethodStart();
            try
            {
                #region テーブル作成
                this.ResultTable = new DataTable();
                ResultTable.Columns.Add("ITAKU_KEIYAKU_NO", Type.GetType("System.String"));
                ResultTable.Columns.Add("GYOUSHA_NAME_RYAKU", Type.GetType("System.String"));
                ResultTable.Columns.Add("GENBA_NAME_RYAKU", Type.GetType("System.String"));
                ResultTable.Columns.Add("ITAKU_KEIYAKU_STATUS", Type.GetType("System.String"));
                ResultTable.Columns.Add("ITAKU_KEIYAKU_SHURUI", Type.GetType("System.String"));
                ResultTable.Columns.Add("KOUSHIN_SHUBETSU", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_CREATE_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_SEND_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_RETURN_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_END_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("YUUKOU_BEGIN", Type.GetType("System.String"));
                ResultTable.Columns.Add("YUUKOU_END", Type.GetType("System.String"));
                ResultTable.Columns.Add("HAISHUTSU_JIGYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("HAISHUTSU_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("UNPAN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SHOBUN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SHOBUN_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("LAST_SHOBUN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("LAST_SHOBUN_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));//#160047 20220328 CongBinh
                #endregion

                #region 空白レコードの追加

                DataRow newRow = this.ResultTable.NewRow();
                newRow["ITAKU_KEIYAKU_NO"] = "";
                newRow["GYOUSHA_NAME_RYAKU"] = "";
                newRow["GENBA_NAME_RYAKU"] = "";
                newRow["ITAKU_KEIYAKU_STATUS"] = "";
                newRow["ITAKU_KEIYAKU_SHURUI"] = "";
                newRow["KOUSHIN_SHUBETSU"] = "";
                newRow["KEIYAKUSHO_CREATE_DATE"] = "";
                newRow["KEIYAKUSHO_SEND_DATE"] = "";
                newRow["KEIYAKUSHO_RETURN_DATE"] = "";
                newRow["KEIYAKUSHO_END_DATE"] = "";
                newRow["YUUKOU_BEGIN"] = "";
                newRow["YUUKOU_END"] = "";

                //非表示項目
                newRow["HAISHUTSU_JIGYOUSHA_CD"] = "";
                newRow["HAISHUTSU_JIGYOUJOU_CD"] = "";
                newRow["UNPAN_GYOUSHA_CD"] = "";
                newRow["SHOBUN_GYOUSHA_CD"] = "";
                newRow["SHOBUN_JIGYOUJOU_CD"] = "";
                newRow["LAST_SHOBUN_GYOUSHA_CD"] = "";
                newRow["LAST_SHOBUN_JIGYOUJOU_CD"] = "";
                newRow["SYSTEM_ID"] = "";//#160047 20220328 CongBinh

                //DataGridViewに空レコードを追加
                this.ResultTable.Rows.Add(newRow);
                this.form.customDataGridView1.DataSource = this.ResultTable;
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[0].Index);
                #endregion

                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 日付範囲指定を変更時にラベルを編集する。
        internal void txtDayHani_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.form.dtpDayFrom.Visible = true;
                this.form.dtpDayTo.Visible = true;
                this.form.lblKugiri.Visible = true;
                switch (this.form.txtDayHani.Text)
                {
                    case "1":
                        this.form.lblDayHanni.Text = "作成日";
                        break;
                    case "2":
                        this.form.lblDayHanni.Text = "送付日";
                        break;
                    case "3":
                        this.form.lblDayHanni.Text = "返送日";
                        break;
                    case "4":
                        this.form.lblDayHanni.Text = "完了日";
                        break;
                    case "5":
                        this.form.lblDayHanni.Text = "有効期間開始";
                        break;
                    case "6":
                        this.form.lblDayHanni.Text = "有効期間終了";
                        break;
                    case "7":
                        this.form.lblDayHanni.Text = "自動更新終了日";
                        break;
                    case "8":
                        this.form.lblDayHanni.Text = "日付なし";
                        this.form.dtpDayFrom.Visible = false;
                        this.form.dtpDayTo.Visible = false;
                        this.form.lblKugiri.Visible = false;
                        break;

                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region DGVへ取得したデータを設定する
        /// <summary>
        ///  DGVへ取得したデータを設定する
        /// </summary>
        /// <param name="table">table</param>
        void SetDataGridView(DataTable table)
        {
            LogUtility.DebugMethodStart();
            try
            {
                #region テーブル作成
                this.ResultTable = new DataTable();
                ResultTable.Columns.Add("ITAKU_KEIYAKU_NO", Type.GetType("System.String"));
                ResultTable.Columns.Add("GYOUSHA_NAME_RYAKU", Type.GetType("System.String"));
                ResultTable.Columns.Add("GENBA_NAME_RYAKU", Type.GetType("System.String"));
                ResultTable.Columns.Add("ITAKU_KEIYAKU_STATUS", Type.GetType("System.String"));
                ResultTable.Columns.Add("ITAKU_KEIYAKU_SHURUI", Type.GetType("System.String"));
                ResultTable.Columns.Add("KOUSHIN_SHUBETSU", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_CREATE_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_SEND_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_RETURN_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("KEIYAKUSHO_END_DATE", Type.GetType("System.String"));
                ResultTable.Columns.Add("YUUKOU_BEGIN", Type.GetType("System.String"));
                ResultTable.Columns.Add("YUUKOU_END", Type.GetType("System.String"));
                ResultTable.Columns.Add("HAISHUTSU_JIGYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("HAISHUTSU_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("UNPAN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SHOBUN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SHOBUN_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("LAST_SHOBUN_GYOUSHA_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("LAST_SHOBUN_JIGYOUJOU_CD", Type.GetType("System.String"));
                ResultTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));//#160047 20220328 CongBinh
                #endregion

                //DGV設定用のDataTableに設定
                #region DataTableに設定

                //this.form.customDataGridView1.Rows.Add();
                foreach (DataRow r in table.Rows)
                {
                    #region 値をRowへ設定後、DataTableに追加

                    DataRow newRow = this.ResultTable.NewRow();
                    newRow["ITAKU_KEIYAKU_NO"] = r["ITAKU_KEIYAKU_NO"];
                    newRow["GYOUSHA_NAME_RYAKU"] = r["GYOUSHA_NAME_RYAKU"];
                    newRow["GENBA_NAME_RYAKU"] = r["GENBA_NAME_RYAKU"];
                    newRow["ITAKU_KEIYAKU_STATUS"] = castStatus((r["ITAKU_KEIYAKU_STATUS"]).ToString());
                    newRow["ITAKU_KEIYAKU_SHURUI"] = castShurui((r["ITAKU_KEIYAKU_SHURUI"]).ToString());
                    newRow["KOUSHIN_SHUBETSU"] = castShubetsu((r["KOUSHIN_SHUBETSU"]).ToString());
                    newRow["KEIYAKUSHO_CREATE_DATE"] = castTimeDate((r["KEIYAKUSHO_CREATE_DATE"]).ToString());
                    newRow["KEIYAKUSHO_SEND_DATE"] = castTimeDate((r["KEIYAKUSHO_SEND_DATE"]).ToString());
                    newRow["KEIYAKUSHO_RETURN_DATE"] = castTimeDate((r["KEIYAKUSHO_RETURN_DATE"]).ToString());
                    newRow["KEIYAKUSHO_END_DATE"] = castTimeDate((r["KEIYAKUSHO_END_DATE"]).ToString());
                    newRow["YUUKOU_BEGIN"] = castTimeDate((r["YUUKOU_BEGIN"]).ToString());
                    newRow["YUUKOU_END"] = castTimeDate((r["YUUKOU_END"]).ToString());

                    //非表示項目
                    newRow["HAISHUTSU_JIGYOUSHA_CD"] = r["HAISHUTSU_JIGYOUSHA_CD"];
                    newRow["HAISHUTSU_JIGYOUJOU_CD"] = r["HAISHUTSU_JIGYOUJOU_CD"];
                    newRow["UNPAN_GYOUSHA_CD"] = r["UNPAN_GYOUSHA_CD"];
                    newRow["SHOBUN_GYOUSHA_CD"] = r["SHOBUN_GYOUSHA_CD"];
                    newRow["SHOBUN_JIGYOUJOU_CD"] = r["SHOBUN_JIGYOUJOU_CD"];
                    newRow["LAST_SHOBUN_GYOUSHA_CD"] = r["LAST_SHOBUN_GYOUSHA_CD"];
                    newRow["LAST_SHOBUN_JIGYOUJOU_CD"] = r["LAST_SHOBUN_JIGYOUJOU_CD"];
                    newRow["SYSTEM_ID"] = r["SYSTEM_ID"];//#160047 20220328 CongBinh

                    this.ResultTable.Rows.Add(newRow);
                    #endregion
                }
                #endregion

                #region DGVに設定したレコードが0件の場合、空白1レコード作成
                if (0 != this.ResultTable.Rows.Count)
                {
                    // 検索結果数設定
                    this.form.header.ReadDataNumber.Text = this.ResultTable.Rows.Count.ToString();
                    // ソートデータ登録
                    this.form.customSortHeader1.SortDataTable(this.ResultTable);
                    // グリッド初期化、設定
                    this.form.customDataGridView1.DataSource = this.ResultTable;
                    this.form.customDataGridView1.Refresh();
                }
                else
                {
                    CreateNoDataRecord();
                }
                LogUtility.DebugMethodEnd();
                #endregion
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region Null値チェック
        /// <summary>
        /// Null値チェック
        /// </summary>
        /// <param name="tage">Nullチェック文字列</param>
        private string castNull(string tage)
        {
            LogUtility.DebugMethodStart(tage);
            try
            {
                string res = "";
                if (!string.IsNullOrWhiteSpace(tage))
                {
                    res = tage;
                }
                LogUtility.DebugMethodStart(res);
                return res;
            }
            catch
            {
                throw;
            }

        }
        #endregion
        #region yyyy/MM/dd変換
        /// <summary>
        /// yyyy/MM/dd変換
        /// </summary>
        /// <param name="tage">変換文字列</param>
        private string castTimeDate(string tage)
        {
            LogUtility.DebugMethodStart(tage);
            try
            {
                string res = tage;
                if (string.IsNullOrWhiteSpace(res))
                {
                    res = "";
                }
                else
                {
                    res = DateTime.Parse(tage).ToString("yyyy/MM/dd");
                }
                LogUtility.DebugMethodEnd(res);
                return res;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 更新種別を文字列に変換
        /// <summary>
        /// 更新種別を文字列に変換
        /// </summary>
        /// <param name="tage">変換文字列</param>
        private string castShubetsu(string tage)
        {
            LogUtility.DebugMethodStart(tage);
            try
            {
                string res = "";
                if (!string.IsNullOrWhiteSpace(tage))
                {
                    switch (tage)
                    {
                        case "1":
                            res = "自動更新";
                            break;
                        case "2":
                            res = "単発";
                            break;
                        default:
                            res = "";
                            break;
                    }
                }
                LogUtility.DebugMethodEnd(res);
                return res;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 契約ステータスを文字列に変換
        /// <summary>
        /// 契約ステータスを文字列に変換
        /// </summary>
        /// <param name="tage">変換文字列</param>
        private string castStatus(string tage)
        {
            LogUtility.DebugMethodStart(tage);
            try
            {
                string res = "";
                if (!string.IsNullOrWhiteSpace(tage))
                {
                    switch (tage)
                    {
                        case "1":
                            res = "作成";
                            break;
                        case "2":
                            res = "送付";
                            break;
                        case "3":
                            res = "返送";
                            break;
                        case "4":
                            res = "完了";
                            break;
                        case "5":
                            res = "解約";
                            break;
                        default:
                            res = "";
                            break;
                    }
                }
                LogUtility.DebugMethodEnd(res);
                return res;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 委託契約書種類を文字列に変換
        /// <summary>
        /// 委託契約書種類を文字列に変換
        /// </summary>
        /// <param name="tage">変換文字列</param>
        private string castShurui(string tage)
        {
            LogUtility.DebugMethodStart(tage);
            try
            {
                string res = "";
                if (!string.IsNullOrWhiteSpace(tage))
                {
                    switch (tage)
                    {
                        case "1":
                            res = "収集・運搬";
                            break;
                        case "2":
                            res = "処分";
                            break;
                        case "3":
                            res = "収集・運搬/処分";
                            break;
                        default:
                            res = "";    
                            break;
                    }
                }
                LogUtility.DebugMethodEnd(res);
                return res;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 排出事業者ロストフォーカス処理
        /// <summary>
        /// 排出事業者ロストフォーカス処理
        /// </summary>
        internal void HaishutsuShaLostFocus()
        {
            LogUtility.DebugMethodStart();
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                //排出事業者が空の場合、排出事業場を空にする。
                if (this.form.tb_JigyoushaCd.Text.Trim().Equals(""))
                {
                    this.form.tb_JigyouJouCd.Text = string.Empty;
                    this.form.tb_JigyouJouName.Text = string.Empty;
                }

                //排出事業者が変更された場合、排出事業場を空にする。
                if (!(gyoshaCd_Def == this.form.tb_JigyoushaCd.Text))
                {
                    this.form.tb_JigyouJouCd.Text = string.Empty;
                    this.form.tb_JigyouJouName.Text = string.Empty;

                    if (this.form.tb_JigyoushaKbn.Text.Equals("False"))
                    {
                        this.form.tb_JigyoushaName.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.form.tb_JigyoushaCd.Focus();
                        this.form.tb_JigyoushaCd.SelectAll();
                        this.form.tb_JigyoushaCd.IsInputErrorOccured = true;
                    }
                }
                gyoshaCd_Def = this.form.tb_JigyoushaCd.Text;
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 排出事業場ロストフォーカス処理
        /// <summary>
        /// 排出事業場ロストフォーカス処理
        /// </summary>
        internal void HaishutsuJouLostFocus()
        {
            LogUtility.DebugMethodStart();
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                if(this.form.tb_JigyouJouCd.Text.Trim().Equals(""))
                {
                    //未入力の場合は処理なし
                    this.form.tb_JigyouJouCd.IsInputErrorOccured = false;
                    this.form.tb_JigyouJouName.Text = string.Empty;
                    LogUtility.DebugMethodEnd();
                    return;
                }
                ////排出事業者CD未入力チェック
                //if (this.form.tb_JigyoushaCd.Text.Trim().Equals(""))
                //{
                //    this.form.tb_JigyouJouCd.Text = string.Empty;
                //    this.form.tb_JigyouJouName.Text = string.Empty;
                //    msgLogic.MessageBoxShow("E001", "排出事業者");
                //    this.form.tb_JigyoushaCd.Focus();
                //    LogUtility.DebugMethodEnd();
                //    return;
                //}

                //現場マスタ検索
                M_GENBA data = new M_GENBA();
                data.GENBA_CD = this.form.tb_JigyouJouCd.Text;
                data.GYOUSHA_CD = this.form.tb_JigyoushaCd.Text;
                data = GENBADao.GetDataByCd(data);

                if (data != null && data.SHOBUN_NIOROSHI_GENBA_KBN)
                {
                    
                    this.form.tb_JigyouJouName.Text = data.GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.tb_JigyouJouName.Text = string.Empty;
                    this.form.tb_JigyouJouCd.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.tb_JigyouJouCd.Focus();
                    this.form.tb_JigyouJouCd.IsInputErrorOccured = true;
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 運搬事業者ロストフォーカス処理
        /// <summary>
        /// 運搬事業者ロストフォーカス処理
        /// </summary>
        internal void UnpanCdLostFocus()
        {
            LogUtility.DebugMethodStart();
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.tb_UnpanshaKbn.Text.Equals("False"))
                {
                    this.form.tb_UnpanshaName.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.tb_UnpanshaCd.Focus();
                    this.form.tb_UnpanshaCd.IsInputErrorOccured = true;
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 処分受託者ロストフォーカス処理
        /// <summary>
        /// 処分受託者ロストフォーカス処理
        /// </summary>
        internal void ShobunCdLostFocus()
        {
            LogUtility.DebugMethodStart();
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.tb_ShobunshaKbn.Text.Equals("False"))
                {
                    this.form.tb_ShobunshaName.Text = string.Empty;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.tb_ShobunshaCd.Focus();
                    this.form.tb_ShobunshaCd.IsInputErrorOccured = true;
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 日付Fromロストフォーカス処理
        /// <summary>
        /// 日付Fromロストフォーカス処理
        /// </summary>
        internal void DayFromLostFocus(CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.dtpDayTo.Text.Trim().Equals(""))
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }

                int iCompare = string.Compare(this.form.dtpDayFrom.Text, this.form.dtpDayTo.Text);
                if (iCompare == 1)
                {
                    e.Cancel = true;
                    msgLogic.MessageBoxShow("E043");
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 日付Toロストフォーカス処理
        /// <summary>
        /// 日付Toロストフォーカス処理
        /// </summary>
        internal void DayToLostFocus(CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            var msgLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.dtpDayFrom.Text.Trim().Equals(""))
                {
                    return;
                }

                int iCompare = string.Compare(this.form.dtpDayFrom.Text, this.form.dtpDayTo.Text);
                if (iCompare == 1)
                {
                    e.Cancel = true;
                    msgLogic.MessageBoxShow("E043");
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region １～４カラム+最終カラム(１７)目を編集不可にする
        /// <summary>
        /// １～４カラム目を編集不可にする
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        internal void customDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (e.ColumnIndex <= 3 || e.ColumnIndex > 15)
                {
                    e.Cancel = true;
                }
                LogUtility.DebugMethodEnd(sender, e);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 業者名称取得(排出、運搬、処分)
        /// <summary>
        /// 業者名称取得(排出、運搬、処分)
        /// </summary>
        /// <param name="gyoshaCd">業者CD</param>
        /// <param name="shutokuKbn">排出:0、運搬:1、処分:2</param>
        internal void gyoshaGetData(string gyoshaCd, int shutokuKbn)
        {
            LogUtility.DebugMethodStart(gyoshaCd, shutokuKbn);
            try
            {
                #region 検索条件設定
                this.dto = new ItakuKeiyakuSearchDto();

                this.dto.SHUTOKU_KBN = shutokuKbn;
                switch(shutokuKbn)
                {
                    case 0://排出事業者
                        this.dto.GYOUSHA_CD = this.form.tb_JigyoushaCd.Text;
                        gyoshaCd_Def = this.dto.GYOUSHA_CD;
                        break;
                        
                    case 1://運搬受託者
                        this.dto.GYOUSHA_CD = this.form.tb_UnpanshaCd.Text;
                        break;

                    case 2://処分受託者
                        this.dto.GYOUSHA_CD = this.form.tb_ShobunshaCd.Text;
                        break;
                    default:
                        break;
                }

                #endregion
                
                #region DBから明細部データ取得
                //検索処理
                DataTable table = this.dao.GetGyoshaDataForEntity(this.dto);

                //読込レコード件数の設定
                if (0 < table.Rows.Count)
                {
                    switch (shutokuKbn)
                    {
                        case 0:
                            this.form.tb_JigyoushaName.Text = table.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                            this.form.tb_JigyoushaKbn.Text = "1";
                            break;
                        case 1:
                            this.form.tb_UnpanshaName.Text = table.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                            this.form.tb_UnpanshaKbn.Text = "1";
                            break;
                        case 2:
                            this.form.tb_ShobunshaName.Text = table.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                            this.form.tb_ShobunshaKbn.Text = "1";
                            break;
                        default:
                            break;
                    }
                #endregion
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 現場名称取得(排出)
        /// <summary>
        /// 現場名称取得(排出)
        /// </summary>
        /// <param name="gyoshaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        internal void genbaGetData(string gyoshaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoshaCd, genbaCd);
            try
            {
                #region 検索条件設定
                this.dto = new ItakuKeiyakuSearchDto();

                this.dto.GYOUSHA_CD = this.form.tb_JigyoushaCd.Text;
                this.dto.GENBA_CD = this.form.tb_JigyouJouCd.Text;
                #endregion

                //検索処理
                DataTable table = this.dao.GetGenbaDataForEntity(this.dto);

                //読込レコード件数の設定
                if (0 < table.Rows.Count)
                {
                    this.form.tb_JigyouJouName.Text = table.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                }
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region HeaderForm取得
        /// <summary>
        /// HeaderForm取得
        /// </summary>
        /// <param name="hs">hs</param>
        public void SetHeaderInfo(HeaderForm hs)
        {
            LogUtility.DebugMethodStart(hs);
            try
            {
                this.headerForm = hs;
                LogUtility.DebugMethodEnd();
            }
            catch
            {
                throw;
            }
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
        #region デフォルトメソッド
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

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        internal void JigyouJouCdValidated()
        {
            if (string.IsNullOrEmpty(this.form.tb_JigyouJouCd.Text))
            {
                this.form.tb_JigyouJouName.Text = string.Empty;
                return;
            }

            var msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.form.tb_JigyoushaCd.Text))
            {
                msgLogic.MessageBoxShow("E051", "排出事業者");
                this.form.tb_JigyouJouCd.Text = string.Empty;
                this.form.tb_JigyouJouCd.Focus();
                return;
            }

            M_GENBA entity = new M_GENBA();
            entity.GYOUSHA_CD = this.form.tb_JigyoushaCd.Text;
            entity.GENBA_CD = this.form.tb_JigyouJouCd.Text;
            entity.ISNOT_NEED_DELETE_FLG = true;
            var entitys = this.GENBADao.GetAllValidData(entity);
            if (entitys == null || entitys.Length == 0)
            {
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.tb_JigyouJouName.Text = string.Empty;
                this.form.tb_JigyouJouCd.Focus();
                return;
            }
            // 20151023 BUNN #12040 STR
            else if (!entitys[0].HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue)
            {
                msgLogic.MessageBoxShow("E020", "現場");
                this.form.tb_JigyouJouName.Text = string.Empty;
                this.form.tb_JigyouJouCd.Focus();
                return;
            }
            // 20151023 BUNN #12040 END
            else
            {
                this.form.tb_JigyouJouName.Text = entitys[0].GENBA_NAME_RYAKU;
            }
        }
    }
}
