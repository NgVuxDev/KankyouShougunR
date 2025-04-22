using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>OpenMasterKbnの値で使用</summary>
        private enum OPEN_MASTER_KBN
        {
            /// <summary>引合取引先</summary>
            HIKIAI_TORIHIKISAKI,

            /// <summary>引合業者</summary>
            HIKIAI_GYOUSHA,

            /// <summary>引合現場(引合業者)</summary>
            HIKIAI_GENBA_FOR_HIKIAI_GYOUSHA,

            /// <summary>引合現場(既存業者)</summary>
            HIKIAI_GENBA_FOR_KIZON_GYOUSHA,

            /// <summary>既存取引先</summary>
            KIZON_TORIHIKISAKI,

            /// <summary>既存業者</summary>
            KIZON_GYOUSHA,

            /// <summary>既存現場</summary>
            KIZON_GENBA
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 起動マスタ区分
        /// 申請ボタンを押下されたときにどのマスタを開くかを判定するために使用
        /// </summary>
        private OPEN_MASTER_KBN OpenMasterKbn { get; set; }
        #endregion

        /// <summary>ボタン設定のパス</summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>申請内容１</summary>
        internal readonly string SHINSEI_KBN_1_HIKIAI = "1";
        internal readonly string SHINSEI_KBN_1_KIZON = "2";

        /// <summary>申請内容２</summary>
        internal readonly string SHINSEI_KBN_2_TORIHIKISAKI = "1";
        internal readonly string SHINSEI_KBN_2_GYOUSHA = "2";
        internal readonly string SHINSEI_KBN_2_GENBA = "3";

        /// <summary>業者区分</summary>
        internal readonly string GYOUSHA_KBN_HIKIAI = "1";
        internal readonly string GYOUSHA_KBN_KIZON = "2";

        /// <summary>検索条件区分</summary>
        internal readonly string SEAERCH_CONDITION_KBN_CODE = "1";
        internal readonly string SEAERCH_CONDITION_KBN_NAME_RYAKU = "2";
        internal readonly string SEAERCH_CONDITION_KBN_FURIGANA = "3";
        internal readonly string SEAERCH_CONDITION_KBN_ADDRESS = "4";
        internal readonly string SEAERCH_CONDITION_KBN_TEL = "5";
        internal readonly string SEAERCH_CONDITION_KBN_FREE = "6";

        /// <summary>
        /// CELL名
        /// 各申請画面を表示するときの引数を取得するときに使用する。
        /// </summary>
        internal readonly string CELL_NAME_TORIHIKISAKI_CD = "TORIHIKISAKI_CD";
        internal readonly string CELL_NAME_GYOUSHA_CD = "GYOUSHA_CD";
        internal readonly string CELL_NAME_GENBA_CD = "GENBA_CD";
        internal readonly string CELL_NAME_SHINSEI_JOUKYOU = "SHINSEI_JOUKYOU";

        // 以下は検索条件で使用
        internal readonly string CELL_NAME_TORIHIKISAKI_NAME = "TORIHIKISAKI_NAME_RYAKU";
        internal readonly string CELL_NAME_TORIHIKISAKI_FURIGANA = "TORIHIKISAKI_FURIGANA";
        internal readonly string CELL_NAME_TORIHIKISAKI_ADDRESS = "TORIHIKISAKI_ADDRESS1";
        internal readonly string CELL_NAME_TORIHIKISAKI_TEL = "TORIHIKISAKI_TEL";

        internal readonly string CELL_NAME_GYOUSHA_NAME = "GYOUSHA_NAME_RYAKU";
        internal readonly string CELL_NAME_GYOUSHA_FURIGANA = "GYOUSHA_FURIGANA";
        internal readonly string CELL_NAME_GYOUSHA_ADDRESS = "GYOUSHA_ADDRESS1";
        internal readonly string CELL_NAME_GYOUSHA_TEL = "GYOUSHA_TEL";

        internal readonly string CELL_NAME_GENBA_NAME = "GENBA_NAME_RYAKU";
        internal readonly string CELL_NAME_GENBA_FURIGANA = "GENBA_FURIGANA";
        internal readonly string CELL_NAME_GENBA_ADDRESS = "GENBA_ADDRESS1";
        internal readonly string CELL_NAME_GENBA_TEL = "GENBA_TEL";

        /// <summary>DTO</summary>
        private DTOClass dto;

        /// <summary>Dao</summary>
        private DAOClass dao;

        /// <summary>IM_TORIHIKISAKIDao</summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>Form</summary>
        private UIForm form;

        /// <summary>検索結果</summary>
        DataTable searchResult = new DataTable();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            this.form = targetForm;
            this.dto = new DTOClass();
        }

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 検索条件の初期化
                bool catchErr = this.SearchConditionInit();
                if (catchErr)
                {
                    return true;
                }

                // コントロールの初期化(暫定対応)
                // 申請内容：引合固定 対応
                this.ProvisionalControlInit();

                // dao初期化
                this.dao = DaoInitUtility.GetComponent<DAOClass>();
                this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClaerCondition);

            // 検索ボタン(F8)イベント生成
            this.form.C_Regist(parentForm.bt_func8);
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);
            parentForm.bt_func8.ProcessKbn = PROCESS_KBN.NONE;

            // 申請ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Shinsei);

            // 並び替えボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.Sort);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        internal bool SearchConditionInit()
        {
            try
            {
                this.form.txt_shinseiKbn_1.Text = this.SHINSEI_KBN_1_HIKIAI;
                this.form.txt_shinseiKbn_2.Text = this.SHINSEI_KBN_2_TORIHIKISAKI;
                this.form.txt_eigyouTantoushaCd.Text = string.Empty;
                this.form.txt_eigyouTantoushaName.Text = string.Empty;
                this.form.txt_gyousyaKbn.Text = this.GYOUSHA_KBN_HIKIAI;
                this.form.txt_gyousyaCd.Text = string.Empty;
                this.form.txt_gyousyaName.Text = string.Empty;
                this.form.txt_searchCondition_kbn.Text = this.SEAERCH_CONDITION_KBN_FURIGANA;
                this.form.txt_searchCondition_value.Text = string.Empty;
                this.form.txt_shinseiKbn_1.Focus();
                this.form.customSortHeader1.ClearCustomSortSetting();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchConditionInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        #endregion

        #region コントロールの初期化(暫定対応)
        /// <summary>
        /// コントロールの初期化(暫定対応)
        /// </summary>
        /// <remarks>
        /// 将来的に削除予定のメソッド。
        /// 新規に処理を記載した場合は、別メソッドにすること。
        /// 「申請内容：既存」の検索をさせないため。
        /// </remarks>
        private void ProvisionalControlInit()
        {
            this.form.customPanel1.Location = new System.Drawing.Point(0, 54);
            this.form.customPanel1.Visible = false;
            this.form.txt_shinseiKbn_1.Text = SHINSEI_KBN_1_HIKIAI; // 申請内容１は引合固定

            this.form.customPanel3.Location = new System.Drawing.Point(0, 10);
            this.form.label1.Text = "申請内容※";
            this.form.txt_shinseiKbn_2.DisplayItemName = "申請内容";


            this.form.customPanel8.Location = new System.Drawing.Point(0, 32);

            this.form.txt_shinseiKbn_2.Focus();
        }
        #endregion

        #endregion

        #region ファンクション用のロジック

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

        #region 検索
        /// <summary>
        /// F8 検索イベント
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                var dto = this.CreateDtoClass();

                string[] headersText = null;

                if (this.form.txt_shinseiKbn_1.Text.Equals(this.SHINSEI_KBN_1_HIKIAI))
                {
                    // 引合
                    switch (this.form.txt_shinseiKbn_2.Text)
                    {
                        case "1":
                            // 取引先
                            headersText = new string[] { "作成日", "取引先CD", "取引先名", "フリガナ", "住所", "電話番号", "営業担当者", "申請状況" };
                            this.searchResult = this.dao.GetHikiaiTorihikisakiData(dto);
                            this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.HIKIAI_TORIHIKISAKI;
                            break;

                        case "2":
                            // 業者
                            headersText = new string[] { "作成日", "業者CD", "業者名", "フリガナ", "住所", "電話番号", "営業担当者", "申請状況" };
                            this.searchResult = this.dao.GetHikiaiGyoushaData(dto);
                            this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.HIKIAI_GYOUSHA;
                            break;

                        case "3":
                            // 現場
                            headersText = new string[] { "作成日", "業者CD", "業者名", "現場CD", "現場名", "フリガナ", "住所", "電話番号", "営業担当者", "申請状況" };
                            if (this.form.txt_gyousyaKbn.Text.Equals(this.GYOUSHA_KBN_HIKIAI))
                            {
                                this.searchResult = this.dao.GetHikiaiGenbaDataForHikiaiGyousha(dto);
                                this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.HIKIAI_GENBA_FOR_HIKIAI_GYOUSHA;
                            }
                            else if (this.form.txt_gyousyaKbn.Text.Equals(this.GYOUSHA_KBN_KIZON))
                            {
                                this.searchResult = this.dao.GetHikiaiGenbaDataForKizonGyousha(dto);
                                this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.HIKIAI_GENBA_FOR_KIZON_GYOUSHA;
                            }
                            break;
                    }
                }
                else if (this.form.txt_shinseiKbn_1.Text.Equals(this.SHINSEI_KBN_1_KIZON))
                {
                    // 既存
                    switch (this.form.txt_shinseiKbn_2.Text)
                    {
                        case "1":
                            // 取引先
                            headersText = new string[] { "作成日", "取引先CD", "取引先名", "フリガナ", "住所", "電話番号", "営業担当者" };
                            this.searchResult = this.dao.GetTorihikisakiData(dto);
                            this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.KIZON_TORIHIKISAKI;
                            break;

                        case "2":
                            // 業者
                            headersText = new string[] { "作成日", "業者CD", "業者名", "フリガナ", "住所", "電話番号", "営業担当者" };
                            this.searchResult = this.dao.GetGyoushaData(dto);
                            this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.KIZON_GYOUSHA;
                            break;

                        case "3":
                            // 現場
                            headersText = new string[] { "作成日", "業者CD", "業者名", "現場CD", "現場名", "フリガナ", "住所", "電話番号", "営業担当者" };
                            if (this.form.txt_gyousyaKbn.Text.Equals(this.GYOUSHA_KBN_HIKIAI))
                            {
                                // 引合業者 - 既存現場の構成はありえないためSQLを発行できない
                            }
                            else if (this.form.txt_gyousyaKbn.Text.Equals(this.GYOUSHA_KBN_KIZON))
                            {
                                this.searchResult = this.dao.GetGenbaDataForKizonGyousha(dto);
                            }
                            this.OpenMasterKbn = LogicClass.OPEN_MASTER_KBN.KIZON_GENBA;
                            break;
                    }
                }

                // 画面へ表示。0件でも表示する
                this.form.customSortHeader1.SortDataTable(this.searchResult);
                this.SetSearchConditionToRowsFilter(this.searchResult);
                this.form.customDataGridView1.IsBrowsePurpose = false;
                this.form.customDataGridView1.DataSource = this.searchResult;
                this.form.customDataGridView1.IsBrowsePurpose = true;

                if (headersText != null)
                {
                    for (int i = 0; i < this.form.customDataGridView1.Columns.Count; i++)
                    {
                        if (i < headersText.Length)
                        {
                            this.form.customDataGridView1.Columns[i].HeaderText = headersText[i];
                            this.form.customDataGridView1.Columns[i].DisplayIndex = i;
                        }
                    }
                }

                return searchResult.Rows.Count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        #region 検索条件(名称等)で絞込
        /// <summary>
        /// 検索条件(名称等)で検索結果を絞り込む
        /// </summary>
        /// <param name="dt">絞り込む対象のDataTable</param>
        private void SetSearchConditionToRowsFilter(DataTable dt)
        {
            if (string.IsNullOrEmpty(this.form.txt_searchCondition_kbn.Text)
                || string.IsNullOrEmpty(this.form.txt_searchCondition_value.Text)
                || string.IsNullOrEmpty(this.form.txt_shinseiKbn_2.Text)
                || dt == null
                || dt.Rows.Count < 1)
            {
                return;
            }

            // 取引先、業者、現場でカラム名が異なるので、カラム名をセット
            string code = string.Empty;
            string nameRyaku = string.Empty;
            string furigana = string.Empty;
            string address = string.Empty;
            string tel = string.Empty;

            switch(this.form.txt_shinseiKbn_2.Text)
            {
                case "1":
                    code = this.CELL_NAME_TORIHIKISAKI_CD;
                    nameRyaku = this.CELL_NAME_TORIHIKISAKI_NAME;
                    furigana = this.CELL_NAME_TORIHIKISAKI_FURIGANA;
                    address = this.CELL_NAME_TORIHIKISAKI_ADDRESS;
                    tel = this.CELL_NAME_TORIHIKISAKI_TEL;
                    break;

                case "2":
                    code = this.CELL_NAME_GYOUSHA_CD;
                    nameRyaku = this.CELL_NAME_GYOUSHA_NAME;
                    furigana = this.CELL_NAME_GYOUSHA_FURIGANA;
                    address = this.CELL_NAME_GYOUSHA_ADDRESS;
                    tel = this.CELL_NAME_GYOUSHA_TEL;
                    break;

                case "3":
                    code = this.CELL_NAME_GENBA_CD;
                    nameRyaku = this.CELL_NAME_GENBA_NAME;
                    furigana = this.CELL_NAME_GENBA_FURIGANA;
                    address = this.CELL_NAME_GENBA_ADDRESS;
                    tel = this.CELL_NAME_GENBA_TEL;
                    break;

                default:
                    // 申請内容が上記以外の場合絞り込めないのでreturn
                    return;
            }

            /**
             * フィルタ実行
             */
            string conditionValue = this.form.txt_searchCondition_value.Text;

            if (this.SEAERCH_CONDITION_KBN_CODE.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // ｺｰﾄﾞ
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", code, conditionValue);
            }
            else if (this.SEAERCH_CONDITION_KBN_NAME_RYAKU.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // 略称
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", nameRyaku, conditionValue);
            }
            else if (this.SEAERCH_CONDITION_KBN_FURIGANA.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // ﾌﾘｶﾞﾅ
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", furigana, conditionValue);
            }
            else if (this.SEAERCH_CONDITION_KBN_ADDRESS.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // 住所
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", address, conditionValue);
            }
            else if (this.SEAERCH_CONDITION_KBN_TEL.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // 電話
                dt.DefaultView.RowFilter = string.Format("{0} LIKE '%{1}%'", tel, conditionValue);
            }
            else if (this.SEAERCH_CONDITION_KBN_FREE.Equals(this.form.txt_searchCondition_kbn.Text))
            {
                // ﾌﾘｰ
                string filterStr = string.Format("{0} LIKE '%{1}%'", code, conditionValue);
                filterStr += string.Format("OR {0} LIKE '%{1}%'", nameRyaku, conditionValue);
                filterStr += string.Format("OR {0} LIKE '%{1}%'", furigana, conditionValue);
                filterStr += string.Format("OR {0} LIKE '%{1}%'", address, conditionValue);
                filterStr += string.Format("OR {0} LIKE '%{1}%'", tel, conditionValue);

                dt.DefaultView.RowFilter = filterStr;
            }

        }
        #endregion

        #endregion

        #region 申請画面表示(マスタ画面を起動する)
        /// <summary>
        /// 申請画面を表示する
        /// </summary>
        /// <param name="row">選択行</param>
        internal bool ShowShinseiWindow(DataGridViewRow row)
        {
            try
            {
                if (row == null)
                {
                    return false;
                }

                string torihikisakiCd;
                string gyoushaCd;
                string genbaCd;
                string shinseiJoukyou;
                var dsUtility = new DenshiShinseiUtility();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string errorMessage = "選択された{0}は、ワークフロー申請中のため申請できません。";

                switch (this.OpenMasterKbn)
                {
                    case LogicClass.OPEN_MASTER_KBN.HIKIAI_TORIHIKISAKI:
                        // 引合取引先入力
                        torihikisakiCd = row.Cells[this.CELL_NAME_TORIHIKISAKI_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_TORIHIKISAKI_CD].Value.ToString();
                        shinseiJoukyou = row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value == null ? string.Empty : row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value.ToString();

                        if (!string.IsNullOrEmpty(shinseiJoukyou))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "引合取引先"));
                        }
                        else if (!string.IsNullOrEmpty(torihikisakiCd))
                        {
                            FormManager.OpenForm("M461", WINDOW_TYPE.UPDATE_WINDOW_FLAG, torihikisakiCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.HIKIAI_GYOUSHA:
                        // 引合業者入力
                        gyoushaCd = row.Cells[this.CELL_NAME_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GYOUSHA_CD].Value.ToString();
                        shinseiJoukyou = row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value == null ? string.Empty : row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value.ToString();

                        if (!string.IsNullOrEmpty(shinseiJoukyou))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "引合業者"));
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd))
                        {
                            FormManager.OpenForm("M462", WINDOW_TYPE.UPDATE_WINDOW_FLAG, gyoushaCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.HIKIAI_GENBA_FOR_HIKIAI_GYOUSHA:
                        // 引合現場入力(引合業者)
                        gyoushaCd = row.Cells[this.CELL_NAME_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GYOUSHA_CD].Value.ToString();
                        genbaCd = row.Cells[this.CELL_NAME_GENBA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GENBA_CD].Value.ToString();
                        shinseiJoukyou = row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value == null ? string.Empty : row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value.ToString();

                        if (!string.IsNullOrEmpty(shinseiJoukyou))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "引合現場"));
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd)
                            && !string.IsNullOrEmpty(genbaCd))
                        {
                            FormManager.OpenForm("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, true, gyoushaCd, genbaCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.HIKIAI_GENBA_FOR_KIZON_GYOUSHA:
                        // 引合現場入力(既存業者)
                        gyoushaCd = row.Cells[this.CELL_NAME_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GYOUSHA_CD].Value.ToString();
                        genbaCd = row.Cells[this.CELL_NAME_GENBA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GENBA_CD].Value.ToString();
                        shinseiJoukyou = row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value == null ? string.Empty : row.Cells[this.CELL_NAME_SHINSEI_JOUKYOU].Value.ToString();

                        if (!string.IsNullOrEmpty(shinseiJoukyou))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "引合現場"));
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd)
                            && !string.IsNullOrEmpty(genbaCd))
                        {
                            FormManager.OpenForm("M463", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false, gyoushaCd, genbaCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.KIZON_TORIHIKISAKI:
                        // 既存取引先入力
                        torihikisakiCd = row.Cells[this.CELL_NAME_TORIHIKISAKI_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_TORIHIKISAKI_CD].Value.ToString();

                        if (!dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.TORIHIKISAKI, torihikisakiCd, null, null))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "取引先"));
                        }
                        else if (!string.IsNullOrEmpty(torihikisakiCd))
                        {
                            FormManager.OpenForm("M213", WINDOW_TYPE.UPDATE_WINDOW_FLAG, torihikisakiCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.KIZON_GYOUSHA:
                        // 既存業者入力
                        gyoushaCd = row.Cells[this.CELL_NAME_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GYOUSHA_CD].Value.ToString();

                        if (!dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.GYOUSHA, null, gyoushaCd, null))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "業者"));
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd))
                        {
                            FormManager.OpenForm("M215", WINDOW_TYPE.UPDATE_WINDOW_FLAG, gyoushaCd, true);
                        }
                        break;

                    case LogicClass.OPEN_MASTER_KBN.KIZON_GENBA:
                        // 既存現場入力
                        gyoushaCd = row.Cells[this.CELL_NAME_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GYOUSHA_CD].Value.ToString();
                        genbaCd = row.Cells[this.CELL_NAME_GENBA_CD].Value == null ? string.Empty : row.Cells[this.CELL_NAME_GENBA_CD].Value.ToString();

                        if (!dsUtility.IsPossibleData(DenshiShinseiUtility.SHINSEI_MASTER_KBN.GENBA, null, gyoushaCd, genbaCd))
                        {
                            msgLogic.MessageBoxShowError(string.Format(errorMessage, "現場"));
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd)
                            && !string.IsNullOrEmpty(genbaCd))
                        {
                            FormManager.OpenForm("M217", WINDOW_TYPE.UPDATE_WINDOW_FLAG, gyoushaCd, genbaCd, true);
                        }

                        break;

                    default:
                        break;

                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowShinseiWindow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        #endregion

        #region 検索条件の表示制御
        /// <summary>
        /// 検索条件の表示制御
        /// </summary>
        /// <param name="isGenba">現場用かどうか。true:現場用、false:それ以外</param>
        internal bool ChangeVisibleForSearchCondition(bool isGenba)
        {
            try
            {
                // 現場の場合は業者の絞込を必ずさせる
                this.form.customPanel5.Visible = isGenba;
                this.form.customPanel7.Visible = isGenba;

                // 名称等の検索条件制御
                if (isGenba)
                {
                    // 現場の場合、業者検索用の項目があるため取引先、業者とは別の位置に配置
                    this.form.customPanel9.Location = new System.Drawing.Point(417, 54);
                }
                else
                {
                    this.form.customPanel9.Location = new System.Drawing.Point(417, 10);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeVisibleForSearchCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        #region 検索値妥当性チェック
        /// <summary>
        /// 検索値妥当性チェック
        /// 必須チェックはr-frameWorkの機能に任せる。
        /// このメソッドでは妥当性だけを判断する。
        /// </summary>
        /// <returns>true:妥当値, false:不正値が含まれている</returns>
        internal bool IsValidSearchValue()
        {
            bool returnVal = true;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 引合業者 - 既存現場の構成は無いため、その構成を検索しようとした場合はエラーとする
            if ((this.SHINSEI_KBN_1_KIZON.Equals(this.form.txt_shinseiKbn_1.Text)
                && this.SHINSEI_KBN_2_GENBA.Equals(this.form.txt_shinseiKbn_2.Text))
                && !this.GYOUSHA_KBN_KIZON.Equals(this.form.txt_gyousyaKbn.Text))
            {
                msgLogic.MessageBoxShow("E034", "既存の現場を検索する場合、引合業者は指定できません。既存業者");
                this.form.txt_gyousyaKbn.Focus();
                returnVal = false;
            }

            return returnVal;
        }
        #endregion

        #region 検索用DTO生成
        /// <summary>
        /// 検索用DTOの生成
        /// </summary>
        /// <returns>DTOClass</returns>
        internal DTOClass CreateDtoClass()
        {
            var dto = new DTOClass();

            dto.shinseiKbn1 = this.form.txt_shinseiKbn_1.Text;
            dto.shinseiKbn2 = this.form.txt_shinseiKbn_2.Text;
            dto.eigyouTantoushaCd = this.form.txt_eigyouTantoushaCd.Text;
            dto.gyoushaKbn = this.form.txt_gyousyaKbn.Text;
            dto.gyoushaCd = this.form.txt_gyousyaCd.Text;

            return dto;
        }
        #endregion

        #region 業者CD検証
        /// <summary>
        /// 業者CD検証
        /// </summary>
        /// <returns>true:有効なCD、false:無効なCD</returns>
        internal bool IsValidGyoushaCd()
        {
            try
            {
                bool returnVal = true;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 前回値から変更がない場合は何もしない
                if (!this.form.isError && this.form.BeforeGyoushaCd.Equals(this.form.txt_gyousyaCd.Text))
                {
                    return returnVal;
                }

                if (string.IsNullOrEmpty(this.form.txt_gyousyaCd.Text))
                {
                    // クリア
                    this.form.txt_gyousyaName.Text = string.Empty;
                    return returnVal;
                }

                if (this.form.txt_gyousyaKbn.Text.Equals(this.GYOUSHA_KBN_HIKIAI))
                {
                    // 引合業者検索
                    var hikiaiGyousha = this.dao.GetHikiaiGyoushaByCd(this.form.txt_gyousyaCd.Text);
                    if (hikiaiGyousha == null || hikiaiGyousha.DELETE_FLG)
                    {
                        msgLogic.MessageBoxShow("E020", "引合業者");
                        returnVal = false;
                        this.form.isError = true;
                    }
                    else
                    {
                        this.form.txt_gyousyaName.Text = hikiaiGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                else
                {
                    // 既存業者検索
                    // もし業者区分が設定されていなければ既存として判定する(開発者の独断で処理。要望があれば都度変更)
                    if (string.IsNullOrEmpty(this.form.txt_gyousyaKbn.Text))
                    {
                        this.form.txt_gyousyaKbn.Text = this.GYOUSHA_KBN_KIZON;
                    }

                    var kizonGyousha = this.gyoushaDao.GetDataByCd(this.form.txt_gyousyaCd.Text);

                    if (kizonGyousha == null || kizonGyousha.DELETE_FLG)
                    {
                        msgLogic.MessageBoxShow("E020", "業者");
                        returnVal = false;
                        this.form.isError = true;
                    }
                    else
                    {
                        this.form.txt_gyousyaName.Text = kizonGyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                return returnVal;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("IsValidGyoushaCd", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsValidGyoushaCd", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        #region 検索条件のIME制御
        /// <summary>
        /// 検索条件(名称等の絞込条件)のIMEモードを変更
        /// </summary>
        internal bool ChangeSearchValueIME()
        {
            try
            {
                if (this.SEAERCH_CONDITION_KBN_NAME_RYAKU.Equals(this.form.txt_searchCondition_kbn.Text.ToString())
                    || this.SEAERCH_CONDITION_KBN_ADDRESS.Equals(this.form.txt_searchCondition_kbn.Text.ToString())
                    || this.SEAERCH_CONDITION_KBN_FREE.Equals(this.form.txt_searchCondition_kbn.Text.ToString()))
                {
                    this.form.txt_searchCondition_value.ImeMode = ImeMode.Hiragana;
                }
                else if (this.SEAERCH_CONDITION_KBN_FURIGANA.Equals(this.form.txt_searchCondition_kbn.Text.ToString()))
                {
                    this.form.txt_searchCondition_value.ImeMode = ImeMode.Katakana;
                }
                else
                {
                    this.form.txt_searchCondition_value.ImeMode = ImeMode.Disable;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeSearchValueIME", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
        #endregion
    }
}
