using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.FormManager;
using Shougun.Core.Common.DenpyouHimodukeIchiran.DAO;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Message;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region Const
        /// <summary>
        /// 伝票日付 - 交付年月日
        /// </summary>
        private const string KOUHU_DATE_NUM = "1";
        /// <summary>
        /// 伝票日付 - 運搬終了日
        /// </summary>
        private const string UNPAN_DATE_NUM = "2";
        /// <summary>
        /// 伝票日付 - 処分終了日
        /// </summary>
        private const string SHOBUN_DATE_NUM = "3";
        /// <summary>
        /// 伝票日付 - 最終処分終了日
        /// </summary>
        private const string SAISHUU_SHOBUN_DATE_NUM = "4";
        #endregion

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        HeaderForm headForm;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao m_kyotendao;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 受付テーブル名称
        /// </summary>
        private string[] strUketsukeTableName;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal UIBaseForm parentForm;

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.DenpyouHimodukeIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DenpyouHimodukeIchiranDao daoIchiran;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string mcreateSql { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public int denShu_Kbn { get; set; }

        /// <summary>
        /// 伝票種類フラグ
        /// </summary>
        public int disp_Flg { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.daoIchiran = DaoInitUtility.GetComponent<DenpyouHimodukeIchiranDao>();
                this.form = targetForm;
                this.dto = new DTOClass();
                //this.parentForm = (UIBaseForm)this.form.Parent;
                this.m_kyotendao = DaoInitUtility.GetComponent<IM_KYOTENDao>();

            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (UIBaseForm)this.form.Parent;

                GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                System.Data.DataTable dt = dao.GetDateForStringSql("SELECT CONVERT(DATE, GETDATE()) AS DATE_TIME");//DBサーバ日付を取得する
                if (dt.Rows.Count > 0)
                {
                    //取得した場合(基本的に取得できないのはありえない)
                    DateTime sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);//取得した結果をDateTimeに転換する
                    //念のため、転換結果をチェックする
                    this.parentForm.sysDate = sysDate;//画面フォームにDBサーバ日付を設定する
                }

                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                    this.headForm.alertNumber.Text = SetComma(sysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                }

                //初期化時全てチャックボックスが入れる
                this.form.numTxtBox_KsTsKn.Text = "1";

                //伝票日付初期化
                this.form.dtpDateFrom.Text = this.parentForm.sysDate.ToString();
                this.form.dtpDateTo.Text = this.parentForm.sysDate.ToString();

                this.headForm.lb_title.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 初期値設定
                this.SetHeaderInit();

                this.allControl = this.form.allControl;

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (UIBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();

                LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (UIBaseForm)this.form.Parent;

                parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)

                //Functionボタンのイベント生成
                parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);       //F3 修正
                parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);       //F6 CSV出力
                parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //F7 検索条件クリア
                parentForm.bt_func7.CausesValidation = false;
                parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //F8 検索
                parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //F10 並び替え
                parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
                parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移

                parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002

                //画面上でESCキー押下時のイベント生成
                //this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動
                //  明細画面上でダブルクリック時のイベント生成
                this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

                /// 20141023 Houkakou 「伝票紐付一覧」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.dtpDateTo.MouseDoubleClick += new MouseEventHandler(dtpDateTo_MouseDoubleClick);
                /// 20141023 Houkakou 「伝票紐付一覧」のダブルクリックを追加する　end

                /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　start
                this.form.dtpDateFrom.Leave += new System.EventHandler(dtpDateFrom_Leave);
                this.form.dtpDateTo.Leave += new System.EventHandler(dtpDateTo_Leave);
                /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　end
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ヘッダ初期値設定
        // No.777
        /// <summary>
        /// ヘッダ初期値設定
        /// </summary>
        private void SetHeaderInit()
        {
            //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
            //拠点CDを取得            
            if (this.headForm.KYOTEN_CD.Text != null)
            {
                var kyotenCd = Properties.Settings.Default.SET_KYOTEN_CD;
                this.headForm.KYOTEN_CD.Text = string.Empty;
                var kyoten_cd = 0;
                //数字チェック + 空チェック
                var kyoten_res = int.TryParse(kyotenCd, out kyoten_cd);
                if (kyoten_res)
                {
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten.KYOTEN_CD = (SqlInt16)kyoten_cd;
                    //削除フラグがたっていない適用期間内の情報を取得する
                    var mKyotenList = m_kyotendao.GetAllValidData(mKyoten);
                    if (mKyotenList.Count() > 0)
                    {
                        this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", kyoten_cd);
                    }
                }
            }

            if (this.headForm.KYOTEN_CD.Text == "")
            {
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));
            }

            // ユーザ拠点名称の取得
            if (this.headForm.KYOTEN_CD.Text != null)
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)m_kyotendao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                if (mKyoten == null || this.headForm.KYOTEN_CD.Text == "")
                {
                    this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                }
                else
                {
                    this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }
        }
        #endregion


        #region Functionボタン 押下処理

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

                if (this.form.customDataGridView1.DataSource == null ||
                    this.form.customDataGridView1.Rows.Count == 0)
                {
                    return;
                }

                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;

                if (datagridviewcell.RowIndex >= 0 &&
                    datagridviewcell.ColumnIndex >= 0)
                {
                    // 列名取得
                    string strClmNm = this.form.customDataGridView1.Columns[datagridviewcell.ColumnIndex].Name;

                    // 「1.受付」
                    if (strClmNm.Contains("(受付)") &&
                        strUketsukeTableName.Length > 0)
                    {
                        // 受付（収集）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_SS_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }

                        // 受付（出荷）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_SK_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G016", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }

                        // 受付（持込）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_MK_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G018", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }

                    }

                    // 「2.計量」
                    if (strClmNm.Contains("(計量)"))
                    {
                        // 計量番号--KEIRYOU_NUMBER
                        string strKeiryouNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(計量)\r\n\r\n計量番号"].Value.ToString();

                        FormManager.OpenForm("G045", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strKeiryouNumber);
                    }

                    // 「3.受入」
                    if (strClmNm.Contains("(受入)"))
                    {
                        // 受入番号--UKEIRE_NUMBER
                        string strUkeireNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受入)\r\n\r\n受入番号"].Value.ToString();

                        FormManager.OpenForm("G051", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUkeireNumber);
                    }

                    // 「4.出荷」
                    if (strClmNm.Contains("(出荷)"))
                    {
                        // 出荷番号--SHUKKA_NUMBER
                        string strShukkaNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(出荷)\r\n\r\n出荷番号"].Value.ToString();

                        FormManager.OpenForm("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strShukkaNumber);
                    }

                    // 「5.売上/支払」
                    if (strClmNm.Contains("(売上/支払)"))
                    {
                        // 売上/支払番号--UR_SH_NUMBER
                        string strUrShNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(売上/支払)\r\n\r\n売上/支払番号"].Value.ToString();

                        FormManager.OpenForm("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUrShNumber);
                    }

                    // 「6.マニ１次」
                    if (strClmNm.Contains("(マニ１次)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 連携伝種区分CD--RENKEI_DENSHU_KBN_CD
                        string strRenkeiDenshuKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携伝種区分CD"].Value.ToString();
                        // 連携システムID--RENKEI_SYSTEM_ID
                        string strRenkeiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携システムID"].Value.ToString();
                        // 連携明細システムID--RENKEI_MEISAI_SYSTEM_ID
                        string strRenkeiMeisaiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携明細システムID"].Value.ToString();

                        // 産廃マニフェスト（直行用）一次--1.産廃
                        if (strHaikiKbnCd.Equals("1"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G119", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 建廃マニフェスト一次--2 建廃
                        if (strHaikiKbnCd.Equals("2"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 産廃マニフェスト（積替用）一次--3 積替
                        if (strHaikiKbnCd.Equals("3"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }
                    }

                    // 「7.マニ２次」
                    if (strClmNm.Contains("(マニ２次)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 連携伝種区分CD--RENKEI_DENSHU_KBN_CD
                        string strRenkeiDenshuKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携伝種区分CD"].Value.ToString();
                        // 連携システムID--RENKEI_SYSTEM_ID
                        string strRenkeiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携システムID"].Value.ToString();
                        // 連携明細システムID--RENKEI_MEISAI_SYSTEM_ID
                        string strRenkeiMeisaiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携明細システムID"].Value.ToString();

                        // 産廃マニフェスト（直行用）一次--1.産廃
                        if (strHaikiKbnCd.Equals("1"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G119", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 建廃マニフェスト一次--2 建廃
                        if (strHaikiKbnCd.Equals("2"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }
                    }

                    // 「8.電マニ」
                    if (strClmNm.Contains("(電マニ)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 管理番号--KANRI_ID
                        string strKanriID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(電マニ)\r\n\r\n管理番号"].Value.ToString();

                        // 最新SEQ--LATEST_SEQ
                        string strLastSeq = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(電マニ)\r\n\r\n最新SEQ"].Value.ToString();

                        // 廃棄物区分CD--4 電子
                        if (strHaikiKbnCd.Equals("4"))
                        {
                            FormManager.OpenForm("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strKanriID, strLastSeq);
                        }
                    }

                    // 「9.運賃」
                    if (strClmNm.Contains("(運賃)"))
                    {
                        // 伝票番号
                        string strRenkeiSystemID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(運賃)\r\n\r\n伝票番号"].Value.ToString();

                        FormManager.OpenFormWithAuth("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiSystemID);
                    }

                    // 「10.代納」
                    if (strClmNm.Contains("(代納)"))
                    {
                        // 代納番号--DAINOU_NUMBER
                        string strDainouNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(代納)\r\n\r\n代納番号"].Value.ToString();

                        FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, strDainouNumber);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.customDataGridView1.DataSource == null ||
                    this.form.customDataGridView1.Rows.Count == 0)
                {
                    return;
                }

                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                if (datagridviewcell.RowIndex >= 0)
                {
                    if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
                    {
                        CSVExport csvExp = new CSVExport();
                        csvExp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.C_DENPYOU_HIMODUKE_ICHIRAN), this.form);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
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

                var parentForm = (UIBaseForm)this.form.Parent;

                this.form.searchString.Clear();
                //this.form.customDataGridView1.DataSource = null;
                this.headForm.KYOTEN_CD.Clear();
                this.headForm.KYOTEN_NAME_RYAKU.Clear();
                this.headForm.ReadDataNumber.Text = "0";

                //this.headForm.alertNumber.Clear();
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                    this.headForm.alertNumber.Text = sysInfo.ICHIRAN_ALERT_KENSUU.ToString();
                }
                this.form.numTxtBox_KsTsKn.Text = "1";
                this.form.customSortHeader1.ClearCustomSortSetting();

                this.form.dtpDateFrom.Text = this.parentForm.sysDate.ToString();
                this.form.dtpDateTo.Text = this.parentForm.sysDate.ToString();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // パターンチェック
                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                var autoCheckLogic = new AutoRegistCheckLogic(allControl, allControl);
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                // Ditailの行数チェックはFWでできないので自前でチェック
                if (!this.form.RegistErrorFlag)
                {
                    /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　start
                    if (this.DateCheck())
                    {
                        return;
                    }
                    /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　end
                    //読込データ件数を取得
                    this.headForm.ReadDataNumber.Text = this.Search().ToString();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                //throw;
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

                if (this.form.customDataGridView1.DataSource == null)
                {
                    return;
                }

                this.form.customSortHeader1.ShowCustomSortSettingDialog();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
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

                // No.777
                // 以下の項目をセッティングファイルに保存する
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;     //拠点CD
                Properties.Settings.Default.Save();

                var parentForm = (UIBaseForm)this.form.Parent;
                parentForm.Close();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region Windowクローズ処理
        // No.2002
        /// <summary>
        /// Windowクローズ処理
        /// </summary>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;     //拠点CD

            Properties.Settings.Default.Save();
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

                ////戻り値
                //String rtnSysID = String.Empty;
                //rtnSysID = this.syainCode;
                //var assembly = Assembly.LoadFrom("PatternIchiran.dll");
                //// 社員コード、伝種区分を共通画面に渡す
                //var callForm1 = (SuperForm)assembly.CreateInstance(
                //        "Shougun.Core.Common.PatternIchiran.UIForm",
                //        false,
                //        BindingFlags.CreateInstance,
                //        null,
                //        new object[] { rtnSysID, denShu_Kbn.ToString() },
                //        null,
                //        null
                //      );
                //if (callForm1.IsDisposed)
                //{
                //    LogUtility.DebugMethodEnd();
                //    return;
                //}
                //var businessForm = new BusinessBaseForm(callForm1, WINDOW_TYPE.NONE);
                //var ret = businessForm.ShowDialog();

                ////戻り値
                //Type baseObj = assembly.GetType("Shougun.Core.Common.PatternIchiran.UIForm");
                //PropertyInfo val = baseObj.GetProperty("ParamOut_SysID");
                //rtnSysID = (String)val.GetValue(callForm1, null);

                // 社員ID
                String rtnSysID = String.Empty;
                rtnSysID = this.syainCode;

                // 共通で使うテーブル情報取得
                FormManager.OpenForm("G554", rtnSysID);

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
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //var us = new KensakujoukenSetteiForm(this.form.DenshuKbn);
                //us.Show();

                //LogUtility.DebugMethodEnd();

            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント(※遷移先画面が存在しない為、未実装)
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

                var parentForm = (UIBaseForm)this.form.Parent;

                if ("1".Equals(parentForm.txb_process.Text))
                {
                    //パターン一覧画面へ遷移


                }
                else if ("2".Equals(parentForm.txb_process.Text))
                {
                    //検索条件設定画面へ遷移
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("txb_process_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ESCキー押下イベント
        //void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var parentForm = (BusinessBaseForm)this.form.Parent;

        //    if (e.KeyCode == Keys.Escape)
        //    {
        //        //処理No(ESC)へカーソル移動
        //        parentForm.txb_process.Focus();

        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        #endregion

        #region 明細データダブルクリックイベント

        private void customDataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;

                if (datagridviewcell.RowIndex >= 0 &&
                    datagridviewcell.ColumnIndex >= 0)
                {
                    // 列名取得
                    string strClmNm = this.form.customDataGridView1.Columns[datagridviewcell.ColumnIndex].Name;

                    // 「1.受付」
                    if (strClmNm.Contains("(受付)") &&
                        strUketsukeTableName.Length > 0)
                    {
                        // 受付（収集）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_SS_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }

                        // 受付（出荷）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_SK_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G016", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }

                        // 受付（持込）
                        if (strUketsukeTableName[datagridviewcell.ColumnIndex].Contains("T_UKETSUKE_MK_"))
                        {
                            // 受付番号--UKETSUKE_NUMBER
                            string strUketsukeNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受付)\r\n\r\n受付番号"].Value.ToString();

                            FormManager.OpenForm("G018", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUketsukeNumber);
                        }
                    }

                    // 「2.計量」
                    if (strClmNm.Contains("(計量)"))
                    {
                        // 計量番号--KEIRYOU_NUMBER
                        string strKeiryouNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(計量)\r\n\r\n計量番号"].Value.ToString();

                        FormManager.OpenForm("G045", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strKeiryouNumber);
                    }

                    // 「3.受入」
                    if (strClmNm.Contains("(受入)"))
                    {
                        // 受入番号--UKEIRE_NUMBER
                        string strUkeireNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(受入)\r\n\r\n受入番号"].Value.ToString();

                        FormManager.OpenForm("G051", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUkeireNumber);
                    }

                    // 「4.出荷」
                    if (strClmNm.Contains("(出荷)"))
                    {
                        // 出荷番号--SHUKKA_NUMBER
                        string strShukkaNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(出荷)\r\n\r\n出荷番号"].Value.ToString();

                        FormManager.OpenForm("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strShukkaNumber);
                    }

                    // 「5.売上/支払」
                    if (strClmNm.Contains("(売上/支払)"))
                    {
                        // 売上/支払番号--UR_SH_NUMBER
                        string strUrShNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(売上/支払)\r\n\r\n売上/支払番号"].Value.ToString();

                        FormManager.OpenForm("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strUrShNumber);
                    }

                    // 「6.マニ１次」
                    if (strClmNm.Contains("(マニ１次)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 連携伝種区分CD--RENKEI_DENSHU_KBN_CD
                        string strRenkeiDenshuKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携伝種区分CD"].Value.ToString();
                        // 連携システムID--RENKEI_SYSTEM_ID
                        string strRenkeiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携システムID"].Value.ToString();
                        // 連携明細システムID--RENKEI_MEISAI_SYSTEM_ID
                        string strRenkeiMeisaiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ１次)\r\n\r\n連携明細システムID"].Value.ToString();

                        // 産廃マニフェスト（直行用）一次--1.産廃
                        if (strHaikiKbnCd.Equals("1"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G119", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 建廃マニフェスト一次--2 建廃
                        if (strHaikiKbnCd.Equals("2"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 産廃マニフェスト（積替用）一次--3 積替
                        if (strHaikiKbnCd.Equals("3"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G120", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }
                    }

                    // 「7.マニ２次」
                    if (strClmNm.Contains("(マニ２次)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 連携伝種区分CD--RENKEI_DENSHU_KBN_CD
                        string strRenkeiDenshuKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携伝種区分CD"].Value.ToString();
                        // 連携システムID--RENKEI_SYSTEM_ID
                        string strRenkeiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携システムID"].Value.ToString();
                        // 連携明細システムID--RENKEI_MEISAI_SYSTEM_ID
                        string strRenkeiMeisaiSystemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n連携明細システムID"].Value.ToString();

                        // 産廃マニフェスト（直行用）一次--1.産廃
                        if (strHaikiKbnCd.Equals("1"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G119", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }

                        // 建廃マニフェスト一次--2 建廃
                        if (strHaikiKbnCd.Equals("2"))
                        {
                            //フォーム起動
                            FormManager.OpenForm("G121", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiDenshuKbnCd, strRenkeiSystemId, strRenkeiMeisaiSystemId, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                        }
                    }

                    // 「8.電マニ」
                    if (strClmNm.Contains("(電マニ)"))
                    {
                        // 廃棄物区分CD取得--HAIKI_KBN_CD
                        string strHaikiKbnCd = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(マニ２次)\r\n\r\n廃棄物区分CD"].Value.ToString();

                        // 管理番号--KANRI_ID
                        string strKanriID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(電マニ)\r\n\r\n管理番号"].Value.ToString();

                        // 最新SEQ--LATEST_SEQ
                        string strLastSeq = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(電マニ)\r\n\r\n最新SEQ"].Value.ToString();

                        // 廃棄物区分CD--4 電子
                        if (strHaikiKbnCd.Equals("4"))
                        {
                            FormManager.OpenForm("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, strKanriID, strLastSeq);
                        }
                    }

                    // 「9.運賃」
                    if (strClmNm.Contains("(運賃)"))
                    {
                        // 伝票番号
                        string strRenkeiSystemID = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(運賃)\r\n\r\n伝票番号"].Value.ToString();

                        FormManager.OpenFormWithAuth("G153", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, strRenkeiSystemID);
                    }

                    // 「10.代納」
                    if (strClmNm.Contains("(代納)"))
                    {
                        // 代納番号--DAINOU_NUMBER
                        string strDainouNumber = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["(代納)\r\n\r\n代納番号"].Value.ToString();

                        FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, strDainouNumber);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_MouseDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 共通メソッド

        /// <summary>
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderForm hs)
        {
            try
            {
                LogUtility.DebugMethodStart(hs);

                this.headForm = hs;

            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力画面表示
        /// </summary>
        /// <param name="wintype"></param>
        /// <param name="DenpyouNum"></param>
        private void EditDetail(WINDOW_TYPE wintype, string DenpyouNum)
        {
            try
            {
                LogUtility.DebugMethodStart(wintype, DenpyouNum);

                long denpyo = -1;

                if (!string.IsNullOrEmpty(DenpyouNum))
                {
                    denpyo = long.Parse(DenpyouNum);
                }

                string strFormId = "";
                switch (disp_Flg)
                {
                    case 1:
                        strFormId = "G051";
                        break;
                    case 2:
                        strFormId = "G053";
                        break;
                    case 3:
                        strFormId = "G054";
                        break;
                    case 4:
                        strFormId = "G045";
                        break;
                    case 5:
                        strFormId = "G153";
                        break;
                    case 6:
                        strFormId = "G161";
                        break;
                }

                switch (wintype)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        //新規モードで起動
                        FormManager.OpenForm(strFormId);
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        //修正モードで起動
                        FormManager.OpenForm(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyo);
                        break;
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        //修正モードで起動
                        FormManager.OpenForm(strFormId, WINDOW_TYPE.DELETE_WINDOW_FLAG, denpyo);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("EditDetail", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();


                //SELECT句未取得なら検索できない
                if (!string.IsNullOrEmpty(this.selectQuery))
                {
                    // SELECT項目作成
                    string strSelect = this.selectQuery;
                    string strSelect1 = "";
                    string strSelect2 = "";
                    string strSelect3 = "";
                    string strSelect4 = "";
                    string strSelect5 = "";

                    MakeSelectQuery(strSelect, ref strSelect1, ref strSelect2, ref strSelect3, ref strSelect4, ref strSelect5);

                    var sql = new StringBuilder();

                    // TABLE GROUP1
                    if (!string.IsNullOrEmpty(strSelect1))
                    {
                        strUketsukeTableName = strSelect1.Split(',');
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect1);
                        MakeSqlTB1(sql);
                    }

                    // TABLE GROUP2
                    if (!string.IsNullOrEmpty(sql.ToString()) &&
                        !string.IsNullOrEmpty(strSelect2))
                    {
                        sql.Append(" UNION ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect2);
                        MakeSqlTB2(sql);
                    }
                    else if (string.IsNullOrEmpty(sql.ToString()) &&
                             !string.IsNullOrEmpty(strSelect2))
                    {
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect2);
                        MakeSqlTB2(sql);
                    }

                    // TABLE GROUP3
                    if (!string.IsNullOrEmpty(sql.ToString()) &&
        !string.IsNullOrEmpty(strSelect3))
                    {
                        sql.Append(" UNION ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect3);
                        MakeSqlTB3(sql);
                    }
                    else if (string.IsNullOrEmpty(sql.ToString()) &&
                             !string.IsNullOrEmpty(strSelect3))
                    {
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect3);
                        MakeSqlTB3(sql);
                    }

                    // TABLE GROUP4
                    if (!string.IsNullOrEmpty(sql.ToString()) &&
        !string.IsNullOrEmpty(strSelect4))
                    {
                        sql.Append(" UNION ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect4);
                        MakeSqlTB4(sql);
                    }
                    else if (string.IsNullOrEmpty(sql.ToString()) &&
                             !string.IsNullOrEmpty(strSelect4))
                    {
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect4);
                        MakeSqlTB4(sql);
                    }

                    // TABLE GROUP5
                    if (!string.IsNullOrEmpty(sql.ToString()) &&
        !string.IsNullOrEmpty(strSelect5))
                    {
                        sql.Append(" UNION ");
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect5);
                        MakeSqlTB5(sql);
                    }
                    else if (string.IsNullOrEmpty(sql.ToString()) &&
                             !string.IsNullOrEmpty(strSelect5))
                    {
                        sql.Append(" SELECT DISTINCT ");
                        sql.Append(strSelect5);
                        MakeSqlTB5(sql);
                    }

                    //MakeWhereSql(sql);
                    this.mcreateSql = sql.ToString();

                    //検索実行
                    this.searchResult = new DataTable();
                    if (!string.IsNullOrEmpty(this.mcreateSql))
                    {
                        searchResult = daoIchiran.getdateforstringsql(this.mcreateSql);
                    }
                    ret_cnt = searchResult.Rows.Count;
                    this.alertCount = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                    //検索結果表示
                    this.form.ShowData();
                    this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                }

                this.headForm.ReadDataNumber.Text = ret_cnt.ToString();

                LogUtility.DebugMethodEnd(ret_cnt);

            }
            catch (Exception ex)
            {
                if (ex is Seasar.Framework.Exceptions.SRuntimeException)
                {
                    MessageBoxUtility.MessageBoxShow("E130");
                }

                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret_cnt);
            }

            return ret_cnt;
        }

        /// <summary>
        /// 列名設定
        /// </summary>
        /// <returns></returns>
        public int ColumnNameSet()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int ret_cnt = 0;
                //SELECT句未取得なら検索できない
                if (!string.IsNullOrEmpty(this.selectQuery))
                {
                    var dataTable = new DataTable();
                    string[] header = this.selectQuery.Split('"');
                    for (int i = 1; i < header.Length; i = i + 2)
                    {
                        dataTable.Columns.Add(new DataColumn(header[i]));
                    }

                    this.searchResult = dataTable;
                    ret_cnt = dataTable.Rows.Count;
                    this.alertCount = int.Parse(this.headForm.alertNumber.Text.Replace(",", ""));
                    //検索結果表示
                    this.form.ShowData();
                    this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                }

                this.headForm.ReadDataNumber.Text = ret_cnt.ToString();

                LogUtility.DebugMethodEnd(ret_cnt);
                return ret_cnt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ColumnNameSet", ex);
                throw;
            }
        }

        #endregion

        #region 検索文字列の作成

        /// <summary>
        /// 検索文字列の作成
        /// <param name="strSelect">string</param>
        /// <param name="strSelect1">string</param>
        /// <param name="strSelect2">string</param>
        /// <param name="strSelect3">string</param>
        /// <param name="strSelect4">string</param>
        /// <param name="strSelect5">string</param>
        /// </summary>
        private void MakeSelectQuery(string strSelect,
                                     ref string strSelect1,
                                     ref string strSelect2,
                                     ref string strSelect3,
                                     ref string strSelect4,
                                     ref string strSelect5)
        {
            try
            {
                LogUtility.DebugMethodStart(strSelect, strSelect1, strSelect2, strSelect3, strSelect4, strSelect5);

                // 変数定義
                string[] fieldsArray = strSelect.Split(',');
                bool flgTB1 = false;
                bool flgTB2 = false;
                bool flgTB3 = false;
                bool flgTB4 = false;
                bool flgTB5 = false;

                string strTB1 = "T_UKETSUKE_SS_ENTRY,T_UKETSUKE_SS_DETAIL,T_UKETSUKE_SK_ENTRY,T_UKETSUKE_SK_DETAIL,T_UKETSUKE_MK_ENTRY,T_UKETSUKE_MK_DETAIL,T_UKETSUKE_BP_ENTRY,T_UKETSUKE_BP_DETAIL,T_UKETSUKE_CM_ENTRY,";
                strTB1 = strTB1 + "M_KYOTEN_11E1," + "M_MANIFEST_SHURUI_11E3,M_MANIFEST_TEHAI_11E4,M_CONTENA_SOUSA_11E5,M_COURSE_NAME_11E6,M_DENPYOU_KBN_11D1,M_UNIT_11D2,M_SHOUHIZEI_11D3,M_KYOTEN_12E1,";
                strTB1 = strTB1 + "M_MANIFEST_SHURUI_12E3,M_MANIFEST_TEHAI_12E4,M_COURSE_NAME_12E5,M_DENPYOU_KBN_12D1,M_UNIT_12D2,M_SHOUHIZEI_12D3,M_KYOTEN_13E1," + "M_DENPYOU_KBN_13D1,M_UNIT_13D2,M_SHOUHIZEI_13D3,";
                strTB1 = strTB1 + "M_KYOTEN_14E1," + "M_DENPYOU_KBN_14D1,M_UNIT_14D2,M_SHOUHIZEI_14D3";
                string strTB2 = "T_KEIRYOU_ENTRY,T_KEIRYOU_DETAIL,T_UKEIRE_ENTRY,T_UKEIRE_DETAIL,T_SHUKKA_ENTRY,T_SHUKKA_DETAIL,T_UR_SH_ENTRY,T_UR_SH_DETAIL,";
                strTB2 = strTB2 + "M_KYOTEN_2E1," + "M_KEITAI_KBN_2E3,M_CONTENA_SOUSA_2E4,M_MANIFEST_SHURUI_2E5,M_MANIFEST_TEHAI_2E6,M_YOUKI_2D1,M_DENPYOU_KBN_2D2,M_UNIT_2D3,M_KYOTEN_3E1," + "M_KEITAI_KBN_3E3,";
                strTB2 = strTB2 + "M_CONTENA_SOUSA_3E4,M_MANIFEST_SHURUI_3E5,M_MANIFEST_TEHAI_3E6,M_SHOUHIZEI_3E7,M_SHOUHIZEI_3E8,M_DENPYOU_KBN_3D1,M_UNIT_3D2,M_SHOUHIZEI_3D3,M_UNIT_3D4,M_KYOTEN_4E1," + "M_KEITAI_KBN_4E3,";
                strTB2 = strTB2 + "M_CONTENA_SOUSA_4E4,M_MANIFEST_SHURUI_4E5,M_MANIFEST_TEHAI_4E6,M_SHOUHIZEI_4E7,M_SHOUHIZEI_4E8,M_YOUKI_4D1,M_DENPYOU_KBN_4D2,M_UNIT_4D3,M_SHOUHIZEI_4D4,M_UNIT_4D5,M_KYOTEN_5E1,";
                strTB2 = strTB2 + "M_KEITAI_KBN_5E3,M_CONTENA_SOUSA_5E4,M_MANIFEST_SHURUI_5E5,M_MANIFEST_TEHAI_5E6,M_SHOUHIZEI_5E7,M_SHOUHIZEI_5E8,M_DENPYOU_KBN_5D1,M_UNIT_5D2,M_SHOUHIZEI_5D3,M_UNIT_5D4";
                string strTB3 = "T_MANIFEST_ENTRY6,T_MANIFEST_DETAIL6,T_MANIFEST_UPN6,T_MANIFEST_ENTRY7,T_MANIFEST_DETAIL7,T_MANIFEST_UPN7,DT_R18_EX,DT_MF_TOC,DT_R18,DT_R19,";
                strTB3 = strTB3 + "M_HAIKI_KBN_6E1,M_KYOTEN_6E2," + "M_TORIHIKISAKI_6E4,M_KONGOU_SHURUI_6E5,M_UNIT_6E6,M_GYOUSHA_6E7,M_GENBA_6E8,M_GYOUSHA_6E9,M_HAIKI_SHURUI_6D1,M_HAIKI_NAME_6D2,M_NISUGATA_6D3,M_UNIT_6D4,";
                strTB3 = strTB3 + "M_SHOBUN_HOUHOU_6D5,M_GYOUSHA_6D6,M_GENBA_6D7,M_HAIKI_KBN_7E1,M_KYOTEN_7E2," + "M_TORIHIKISAKI_7E4,M_KONGOU_SHURUI_7E5,M_UNIT_7E6,M_GYOUSHA_7E7,M_GENBA_7E8,M_UNIT_7E9,M_GYOUSHA_7E10,";
                strTB3 = strTB3 + "M_HAIKI_SHURUI_7D1,M_HAIKI_NAME_7D2,M_NISUGATA_7D3,M_UNIT_7D4,M_SHOBUN_HOUHOU_7D5,M_GYOUSHA_7D6,M_GENBA_7D7,M_GYOUSHA_8E1,M_GENBA_8E2,M_GYOUSHA_8E3,M_GENBA_8E4,M_HAIKI_NAME_8E5,M_SHOBUN_HOUHOU_8E6,";
                strTB3 = strTB3 + "M_DENSHI_TANTOUSHA_8E7,M_DENSHI_TANTOUSHA_8E8,M_DENSHI_TANTOUSHA_8E9,M_SHARYOU_8E10";
                string strTB4 = "T_UNCHIN_ENTRY9,T_UNCHIN_DETAIL,M_KYOTEN_9E1," + "M_DENSHU_KBN_9E3,M_UNIT_KBN_9D1";
                string strTB5 = "DAINO_ENTRY10,DAINO_DETAIL10,M_KYOTEN_10E1,";

                // TB1検索文字列の作成
                for (int i = 0; i < fieldsArray.Length; i++)
                {
                    if (strTB1.Contains(fieldsArray[i].Substring(0, fieldsArray[i].IndexOf("."))))
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect1 = strSelect1 + fieldsArray[i];
                        }
                        else
                        {
                            strSelect1 = strSelect1 + fieldsArray[i] + ",";
                        }

                        flgTB1 = true;
                    }
                    else
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect1 = strSelect1 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS "));
                        }
                        else
                        {
                            strSelect1 = strSelect1 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS ")) + ",";
                        }
                    }
                }

                if (flgTB1 == false)
                {
                    strSelect1 = "";
                }

                // TB2検索文字列の作成
                for (int i = 0; i < fieldsArray.Length; i++)
                {
                    if (strTB2.Contains(fieldsArray[i].Substring(0, fieldsArray[i].IndexOf("."))))
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect2 = strSelect2 + fieldsArray[i];
                        }
                        else
                        {
                            strSelect2 = strSelect2 + fieldsArray[i] + ",";
                        }

                        flgTB2 = true;
                    }
                    else
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect2 = strSelect2 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS "));
                        }
                        else
                        {
                            strSelect2 = strSelect2 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS ")) + ",";
                        }
                    }
                }

                if (flgTB2 == false)
                {
                    strSelect2 = "";
                }

                // TB3検索文字列の作成
                for (int i = 0; i < fieldsArray.Length; i++)
                {
                    if (strTB3.Contains(fieldsArray[i].Substring(0, fieldsArray[i].IndexOf("."))))
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect3 = strSelect3 + fieldsArray[i];
                        }
                        else
                        {
                            strSelect3 = strSelect3 + fieldsArray[i] + ",";
                        }

                        flgTB3 = true;
                    }
                    else
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect3 = strSelect3 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS "));
                        }
                        else
                        {
                            strSelect3 = strSelect3 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS ")) + ",";
                        }
                    }
                }

                if (flgTB3 == false)
                {
                    strSelect3 = "";
                }

                // TB4検索文字列の作成
                for (int i = 0; i < fieldsArray.Length; i++)
                {
                    if (strTB4.Contains(fieldsArray[i].Substring(0, fieldsArray[i].IndexOf("."))))
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect4 = strSelect4 + fieldsArray[i];
                        }
                        else
                        {
                            strSelect4 = strSelect4 + fieldsArray[i] + ",";
                        }
                        flgTB4 = true;
                    }
                    else
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect4 = strSelect4 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS "));
                        }
                        else
                        {
                            strSelect4 = strSelect4 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS ")) + ",";
                        }
                    }
                }

                if (flgTB4 == false)
                {
                    strSelect4 = "";
                }

                // TB5検索文字列の作成
                for (int i = 0; i < fieldsArray.Length; i++)
                {
                    if (strTB5.Contains(fieldsArray[i].Substring(0, fieldsArray[i].IndexOf("."))))
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect5 = strSelect5 + fieldsArray[i];
                        }
                        else
                        {
                            strSelect5 = strSelect5 + fieldsArray[i] + ",";
                        }
                        flgTB5 = true;
                    }
                    else
                    {
                        if (i == fieldsArray.Length - 1)
                        {
                            strSelect5 = strSelect5 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS "));
                        }
                        else
                        {
                            strSelect5 = strSelect5 + "NULL" + fieldsArray[i].Substring(fieldsArray[i].IndexOf(" AS ")) + ",";
                        }
                    }
                }

                if (flgTB5 == false)
                {
                    strSelect5 = "";
                }

                LogUtility.DebugMethodEnd(strSelect1, strSelect2, strSelect3, strSelect4, strSelect5);
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSelectQuery", ex);
                throw;
            }
        }

        /// <summary>
        /// TB1のSQL文作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSqlTB1(StringBuilder sql)
        {
            try
            {
                LogUtility.DebugMethodStart(sql);

                var parentForm = (UIBaseForm)this.form.Parent;

                //SQL文格納StringBuilder
                sql.Append(" FROM T_UKETSUKE_SS_ENTRY ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_11E1 ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.KYOTEN_CD = M_KYOTEN_11E1.KYOTEN_CD ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_11E3 ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_11E3.MANIFEST_SHURUI_CD ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_11E4 ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_11E4.MANIFEST_TEHAI_CD ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_CONTENA_SOUSA M_CONTENA_SOUSA_11E5 ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.CONTENA_SOUSA_CD = M_CONTENA_SOUSA_11E5.CONTENA_SOUSA_CD ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_COURSE_NAME M_COURSE_NAME_11E6 ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.COURSE_NAME_CD = M_COURSE_NAME_11E6.COURSE_NAME_CD ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UKETSUKE_SK_ENTRY ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_12E1 ON ");
                sql.Append(" T_UKETSUKE_SK_ENTRY.KYOTEN_CD = M_KYOTEN_12E1.KYOTEN_CD ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_12E3 ON ");
                sql.Append(" T_UKETSUKE_SK_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_12E3.MANIFEST_SHURUI_CD ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_12E4 ON ");
                sql.Append(" T_UKETSUKE_SK_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_12E4.MANIFEST_TEHAI_CD ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_COURSE_NAME M_COURSE_NAME_12E5 ON ");
                sql.Append(" T_UKETSUKE_SK_ENTRY.COURSE_NAME_CD = M_COURSE_NAME_12E5.COURSE_NAME_CD ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UKETSUKE_MK_ENTRY ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_UKETSUKE_MK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_MK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_13E1 ON ");
                sql.Append(" T_UKETSUKE_MK_ENTRY.KYOTEN_CD = M_KYOTEN_13E1.KYOTEN_CD ");
                sql.Append(" AND (T_UKETSUKE_MK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_MK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UKETSUKE_BP_ENTRY ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = T_UKETSUKE_BP_ENTRY.UKETSUKE_NUMBER ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_UKETSUKE_BP_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_BP_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_14E1 ON ");
                sql.Append(" T_UKETSUKE_BP_ENTRY.KYOTEN_CD = M_KYOTEN_14E1.KYOTEN_CD ");
                sql.Append(" AND (T_UKETSUKE_BP_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_BP_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UKETSUKE_CM_ENTRY ON ");
                sql.Append(" T_UKETSUKE_CM_ENTRY.UKETSUKE_NUMBER = T_UKETSUKE_CM_ENTRY.UKETSUKE_NUMBER ");
                sql.Append(" AND (T_UKETSUKE_CM_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_CM_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UKETSUKE_SS_DETAIL ON ");
                sql.Append(" T_UKETSUKE_SS_ENTRY.SYSTEM_ID = T_UKETSUKE_SS_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_UKETSUKE_SS_ENTRY.SEQ = T_UKETSUKE_SS_DETAIL.SEQ ");
                sql.Append(" AND (T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SS_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_11D1 ON ");
                sql.Append(" T_UKETSUKE_SS_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_11D1.DENPYOU_KBN_CD ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_11D2 ON ");
                sql.Append(" T_UKETSUKE_SS_DETAIL.UNIT_CD = M_UNIT_11D2.UNIT_CD ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_11D3 ON ");
                sql.Append(" T_UKETSUKE_SS_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_11D3.SYS_ID ");
                sql.Append(" LEFT JOIN T_UKETSUKE_SK_DETAIL ON ");
                sql.Append(" T_UKETSUKE_SK_ENTRY.SYSTEM_ID = T_UKETSUKE_SK_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_UKETSUKE_SK_ENTRY.SEQ = T_UKETSUKE_SK_DETAIL.SEQ ");
                sql.Append(" AND (T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_SK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_12D1 ON ");
                sql.Append(" T_UKETSUKE_SK_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_12D1.DENPYOU_KBN_CD ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_12D2 ON ");
                sql.Append(" T_UKETSUKE_SK_DETAIL.UNIT_CD = M_UNIT_12D2.UNIT_CD ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_12D3 ON ");
                sql.Append(" T_UKETSUKE_SK_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_12D3.SYS_ID ");
                sql.Append(" LEFT JOIN T_UKETSUKE_MK_DETAIL ON ");
                sql.Append(" T_UKETSUKE_MK_ENTRY.SYSTEM_ID = T_UKETSUKE_MK_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_UKETSUKE_MK_ENTRY.SEQ = T_UKETSUKE_MK_DETAIL.SEQ ");
                sql.Append(" AND (T_UKETSUKE_MK_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_MK_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_13D1 ON ");
                sql.Append(" T_UKETSUKE_MK_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_13D1.DENPYOU_KBN_CD ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_13D2 ON ");
                sql.Append(" T_UKETSUKE_MK_DETAIL.UNIT_CD = M_UNIT_13D2.UNIT_CD ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_13D3 ON ");
                sql.Append(" T_UKETSUKE_MK_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_13D3.SYS_ID ");
                sql.Append(" LEFT JOIN T_UKETSUKE_BP_DETAIL ON ");
                sql.Append(" T_UKETSUKE_BP_ENTRY.SYSTEM_ID = T_UKETSUKE_BP_DETAIL.SYSTEM_ID ");
                sql.Append(" AND T_UKETSUKE_BP_ENTRY.SEQ = T_UKETSUKE_BP_DETAIL.SEQ ");
                sql.Append(" AND (T_UKETSUKE_BP_ENTRY.DELETE_FLG = 0 OR T_UKETSUKE_BP_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_14D1 ON ");
                sql.Append(" T_UKETSUKE_BP_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_14D1.DENPYOU_KBN_CD ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_14D2 ON ");
                sql.Append(" T_UKETSUKE_BP_DETAIL.UNIT_CD = M_UNIT_14D2.UNIT_CD ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_14D3 ON ");
                sql.Append(" T_UKETSUKE_BP_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_14D3.SYS_ID ");
                sql.Append(" WHERE 1 = 1 ");

                if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_UKETSUKE_SS_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UKETSUKE_SK_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UKETSUKE_MK_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UKETSUKE_BP_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UKETSUKE_CM_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                }

                // 1.受付-伝票日付
                if (this.disp_Flg == 1)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_UKETSUKE_SS_ENTRY.UKETSUKE_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_SK_ENTRY.UKETSUKE_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_MK_ENTRY.UKETSUKE_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_BP_ENTRY.UKETSUKE_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_CM_ENTRY.UKETSUKE_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_UKETSUKE_SS_ENTRY.UKETSUKE_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_SK_ENTRY.UKETSUKE_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_MK_ENTRY.UKETSUKE_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_BP_ENTRY.UKETSUKE_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                        sql.Append(" AND T_UKETSUKE_CM_ENTRY.UKETSUKE_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSqlTB1", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// TB2のSQL文作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSqlTB2(StringBuilder sql)
        {
            try
            {
                LogUtility.DebugMethodStart(sql);

                var parentForm = (UIBaseForm)this.form.Parent;

                //SQL文格納StringBuilder
                sql.Append(" FROM T_KEIRYOU_ENTRY ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_2E1 ON ");
                sql.Append(" T_KEIRYOU_ENTRY.KYOTEN_CD = M_KYOTEN_2E1.KYOTEN_CD  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KEITAI_KBN M_KEITAI_KBN_2E3 ON ");
                sql.Append(" T_KEIRYOU_ENTRY.KEITAI_KBN_CD = M_KEITAI_KBN_2E3.KEITAI_KBN_CD  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_CONTENA_SOUSA M_CONTENA_SOUSA_2E4 ON ");
                sql.Append(" T_KEIRYOU_ENTRY.CONTENA_SOUSA_CD = M_CONTENA_SOUSA_2E4.CONTENA_SOUSA_CD  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_2E5 ON ");
                sql.Append(" T_KEIRYOU_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_2E5.MANIFEST_SHURUI_CD  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_2E6 ON ");
                sql.Append(" T_KEIRYOU_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_2E6.MANIFEST_TEHAI_CD  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" INNER JOIN T_UKEIRE_ENTRY ON ");
                sql.Append(" T_KEIRYOU_ENTRY.UKETSUKE_NUMBER = T_UKEIRE_ENTRY.UKETSUKE_NUMBER  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_3E1 ON ");
                sql.Append(" T_UKEIRE_ENTRY.KYOTEN_CD = M_KYOTEN_3E1.KYOTEN_CD  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KEITAI_KBN M_KEITAI_KBN_3E3 ON ");
                sql.Append(" T_UKEIRE_ENTRY.KEITAI_KBN_CD = M_KEITAI_KBN_3E3.KEITAI_KBN_CD  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_CONTENA_SOUSA M_CONTENA_SOUSA_3E4 ON ");
                sql.Append(" T_UKEIRE_ENTRY.CONTENA_SOUSA_CD = M_CONTENA_SOUSA_3E4.CONTENA_SOUSA_CD  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_3E5 ON ");
                sql.Append(" T_UKEIRE_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_3E5.MANIFEST_SHURUI_CD  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_3E6 ON ");
                sql.Append(" T_UKEIRE_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_3E6.MANIFEST_TEHAI_CD  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_3E7 ON ");
                sql.Append(" T_UKEIRE_ENTRY.URIAGE_ZEI_KBN_CD = M_SHOUHIZEI_3E7.SYS_ID  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_3E8 ON ");
                sql.Append(" T_UKEIRE_ENTRY.SHIHARAI_ZEI_KBN_CD = M_SHOUHIZEI_3E8.SYS_ID  ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" INNER JOIN T_SHUKKA_ENTRY ON ");
                sql.Append(" T_KEIRYOU_ENTRY.UKETSUKE_NUMBER = T_SHUKKA_ENTRY.UKETSUKE_NUMBER  ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_4E1 ON ");
                sql.Append(" T_SHUKKA_ENTRY.KYOTEN_CD = M_KYOTEN_4E1.KYOTEN_CD  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KEITAI_KBN M_KEITAI_KBN_4E3 ON ");
                sql.Append(" T_SHUKKA_ENTRY.KEITAI_KBN_CD = M_KEITAI_KBN_4E3.KEITAI_KBN_CD  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_CONTENA_SOUSA M_CONTENA_SOUSA_4E4 ON ");
                sql.Append(" T_SHUKKA_ENTRY.CONTENA_SOUSA_CD = M_CONTENA_SOUSA_4E4.CONTENA_SOUSA_CD  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_4E5 ON ");
                sql.Append(" T_SHUKKA_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_4E5.MANIFEST_SHURUI_CD  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_4E6 ON ");
                sql.Append(" T_SHUKKA_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_4E6.MANIFEST_TEHAI_CD  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_4E7 ON ");
                sql.Append(" T_SHUKKA_ENTRY.URIAGE_ZEI_KBN_CD = M_SHOUHIZEI_4E7.SYS_ID  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_4E8 ON ");
                sql.Append(" T_SHUKKA_ENTRY.SHIHARAI_ZEI_KBN_CD = M_SHOUHIZEI_4E8.SYS_ID  ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" INNER JOIN T_UR_SH_ENTRY ON ");
                sql.Append(" T_KEIRYOU_ENTRY.UKETSUKE_NUMBER = T_UR_SH_ENTRY.UKETSUKE_NUMBER  ");
                sql.Append(" AND ISNULL(T_UR_SH_ENTRY.DAINOU_FLG,0) != 1 ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_5E1 ON ");
                sql.Append(" T_UR_SH_ENTRY.KYOTEN_CD = M_KYOTEN_5E1.KYOTEN_CD  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KEITAI_KBN M_KEITAI_KBN_5E3 ON ");
                sql.Append(" T_UR_SH_ENTRY.KEITAI_KBN_CD = M_KEITAI_KBN_5E3.KEITAI_KBN_CD  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_CONTENA_SOUSA M_CONTENA_SOUSA_5E4 ON ");
                sql.Append(" T_UR_SH_ENTRY.CONTENA_SOUSA_CD = M_CONTENA_SOUSA_5E4.CONTENA_SOUSA_CD  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_SHURUI M_MANIFEST_SHURUI_5E5 ON ");
                sql.Append(" T_UR_SH_ENTRY.MANIFEST_SHURUI_CD = M_MANIFEST_SHURUI_5E5.MANIFEST_SHURUI_CD  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_MANIFEST_TEHAI M_MANIFEST_TEHAI_5E6 ON ");
                sql.Append(" T_UR_SH_ENTRY.MANIFEST_TEHAI_CD = M_MANIFEST_TEHAI_5E6.MANIFEST_TEHAI_CD  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_5E7 ON ");
                sql.Append(" T_UR_SH_ENTRY.URIAGE_ZEI_KBN_CD = M_SHOUHIZEI_5E7.SYS_ID  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_5E8 ON ");
                sql.Append(" T_UR_SH_ENTRY.SHIHARAI_ZEI_KBN_CD = M_SHOUHIZEI_5E8.SYS_ID  ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_KEIRYOU_DETAIL ON ");
                sql.Append(" T_KEIRYOU_ENTRY.SYSTEM_ID = T_KEIRYOU_DETAIL.SYSTEM_ID  ");
                sql.Append(" AND T_KEIRYOU_ENTRY.SEQ = T_KEIRYOU_DETAIL.SEQ ");
                sql.Append(" AND (T_KEIRYOU_ENTRY.DELETE_FLG = 0 OR T_KEIRYOU_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_YOUKI M_YOUKI_2D1 ON ");
                sql.Append(" T_KEIRYOU_DETAIL.YOUKI_CD = M_YOUKI_2D1.YOUKI_CD  ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_2D2 ON ");
                sql.Append(" T_KEIRYOU_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_2D2.DENPYOU_KBN_CD  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_2D3 ON ");
                sql.Append(" T_KEIRYOU_DETAIL.NISUGATA_UNIT_CD = M_UNIT_2D3.UNIT_CD  ");
                sql.Append(" LEFT JOIN T_UKEIRE_DETAIL ON ");
                sql.Append(" T_UKEIRE_ENTRY.SYSTEM_ID = T_UKEIRE_DETAIL.SYSTEM_ID  ");
                sql.Append(" AND T_UKEIRE_ENTRY.SEQ = T_UKEIRE_DETAIL.SEQ ");
                sql.Append(" AND (T_UKEIRE_ENTRY.DELETE_FLG = 0 OR T_UKEIRE_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_3D1 ON ");
                sql.Append(" T_UKEIRE_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_3D1.DENPYOU_KBN_CD  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_3D2 ON ");
                sql.Append(" T_UKEIRE_DETAIL.UNIT_CD = M_UNIT_3D2.UNIT_CD  ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_3D3 ON ");
                sql.Append(" T_UKEIRE_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_3D3.SYS_ID  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_3D4 ON ");
                sql.Append(" T_UKEIRE_DETAIL.NISUGATA_UNIT_CD = M_UNIT_3D4.UNIT_CD  ");
                sql.Append(" LEFT JOIN T_SHUKKA_DETAIL ON ");
                sql.Append(" T_SHUKKA_ENTRY.SYSTEM_ID = T_SHUKKA_DETAIL.SYSTEM_ID  ");
                sql.Append(" AND T_SHUKKA_ENTRY.SEQ = T_SHUKKA_DETAIL.SEQ ");
                sql.Append(" AND (T_SHUKKA_ENTRY.DELETE_FLG = 0 OR T_SHUKKA_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_YOUKI M_YOUKI_4D1 ON ");
                sql.Append(" T_SHUKKA_DETAIL.YOUKI_CD = M_YOUKI_4D1.YOUKI_CD  ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_4D2 ON ");
                sql.Append(" T_SHUKKA_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_4D2.DENPYOU_KBN_CD  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_4D3 ON ");
                sql.Append(" T_SHUKKA_DETAIL.UNIT_CD = M_UNIT_4D3.UNIT_CD  ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_4D4 ON ");
                sql.Append(" T_SHUKKA_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_4D4.SYS_ID  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_4D5 ON ");
                sql.Append(" T_SHUKKA_DETAIL.NISUGATA_UNIT_CD = M_UNIT_4D5.UNIT_CD  ");
                sql.Append(" LEFT JOIN T_UR_SH_DETAIL ON ");
                sql.Append(" T_UR_SH_ENTRY.SYSTEM_ID = T_UR_SH_DETAIL.SYSTEM_ID  ");
                sql.Append(" AND T_UR_SH_ENTRY.SEQ = T_UR_SH_DETAIL.SEQ ");
                sql.Append(" AND (T_UR_SH_ENTRY.DELETE_FLG = 0 OR T_UR_SH_ENTRY.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENPYOU_KBN M_DENPYOU_KBN_5D1 ON ");
                sql.Append(" T_UR_SH_DETAIL.DENPYOU_KBN_CD = M_DENPYOU_KBN_5D1.DENPYOU_KBN_CD  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_5D2 ON ");
                sql.Append(" T_UR_SH_DETAIL.UNIT_CD = M_UNIT_5D2.UNIT_CD  ");
                sql.Append(" LEFT JOIN M_SHOUHIZEI M_SHOUHIZEI_5D3 ON ");
                sql.Append(" T_UR_SH_DETAIL.HINMEI_ZEI_KBN_CD = M_SHOUHIZEI_5D3.SYS_ID  ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_5D4 ON ");
                sql.Append(" T_UR_SH_DETAIL.NISUGATA_UNIT_CD = M_UNIT_5D4.UNIT_CD  ");
                sql.Append(" WHERE 1 = 1 ");

                if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_KEIRYOU_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UKEIRE_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_SHUKKA_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_UR_SH_ENTRY.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                }

                // 2.計量-伝票日付
                if (this.disp_Flg == 2)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_KEIRYOU_ENTRY.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_KEIRYOU_ENTRY.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 3.受入-伝票日付
                if (this.disp_Flg == 3)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_UKEIRE_ENTRY.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_UKEIRE_ENTRY.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 4.出荷-伝票日付
                if (this.disp_Flg == 4)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_SHUKKA_ENTRY.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_SHUKKA_ENTRY.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 5.売上/支払-伝票日付
                if (this.disp_Flg == 5)
                {
                    //if (!string.IsNullOrEmpty(parentForm.customDateTimePicker1.Value.ToString()))
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_UR_SH_ENTRY.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_UR_SH_ENTRY.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSqlTB2", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// TB3のSQL文作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSqlTB3(StringBuilder sql)
        {
            try
            {
                LogUtility.DebugMethodStart(sql);

                var parentForm = (UIBaseForm)this.form.Parent;

                //SQL文格納StringBuilder
                sql.Append(" 	FROM T_MANIFEST_ENTRY T_MANIFEST_ENTRY6	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_KBN M_HAIKI_KBN_6E1 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.HAIKI_KBN_CD = M_HAIKI_KBN_6E1.HAIKI_KBN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_KYOTEN M_KYOTEN_6E2 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.KYOTEN_CD = M_KYOTEN_6E2.KYOTEN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_TORIHIKISAKI M_TORIHIKISAKI_6E4 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.TORIHIKISAKI_CD = M_TORIHIKISAKI_6E4.TORIHIKISAKI_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_KONGOU_SHURUI M_KONGOU_SHURUI_6E5 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.HAIKI_KBN_CD = M_KONGOU_SHURUI_6E5.HAIKI_KBN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.KONGOU_SHURUI_CD = M_KONGOU_SHURUI_6E5.KONGOU_SHURUI_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_UNIT M_UNIT_6E6 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.HAIKI_UNIT_CD = M_UNIT_6E6.UNIT_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_6E7 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.LAST_SBN_YOTEI_GYOUSHA_CD = M_GYOUSHA_6E7.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_6E8 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.LAST_SBN_YOTEI_GYOUSHA_CD = M_GENBA_6E8.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.LAST_SBN_YOTEI_GENBA_CD = M_GENBA_6E8.GENBA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_6E9 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.LAST_SBN_GYOUSHA_CD = M_GYOUSHA_6E9.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN T_MANIFEST_DETAIL T_MANIFEST_DETAIL6 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.SYSTEM_ID = T_MANIFEST_DETAIL6.SYSTEM_ID	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.SEQ = T_MANIFEST_DETAIL6.SEQ	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_SHURUI M_HAIKI_SHURUI_6D1 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.HAIKI_SHURUI_CD = M_HAIKI_SHURUI_6D1.HAIKI_SHURUI_CD	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_NAME M_HAIKI_NAME_6D2 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.HAIKI_NAME_CD = M_HAIKI_NAME_6D2.HAIKI_NAME_CD	 ");
                sql.Append(" 	LEFT JOIN M_NISUGATA M_NISUGATA_6D3 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.NISUGATA_CD = M_NISUGATA_6D3.NISUGATA_CD	 ");
                sql.Append(" 	LEFT JOIN M_UNIT M_UNIT_6D4 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.HAIKI_UNIT_CD = M_UNIT_6D4.UNIT_CD	 ");
                sql.Append(" 	LEFT JOIN M_SHOBUN_HOUHOU M_SHOBUN_HOUHOU_6D5 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU_6D5.SHOBUN_HOUHOU_CD	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_6D6 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.LAST_SBN_GYOUSHA_CD = M_GYOUSHA_6D6.GYOUSHA_CD	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_6D7 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL6.LAST_SBN_GYOUSHA_CD = M_GENBA_6D7.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_DETAIL6.LAST_SBN_GENBA_CD = M_GENBA_6D7.GENBA_CD	 ");
                sql.Append(" 	LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN6 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.SYSTEM_ID = T_MANIFEST_UPN6.SYSTEM_ID	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.SEQ = T_MANIFEST_UPN6.SEQ	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	INNER JOIN T_MANIFEST_RELATION ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY6.SYSTEM_ID = T_MANIFEST_RELATION.FIRST_SYSTEM_ID 	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY6.FIRST_MANIFEST_KBN = 0	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY6.DELETE_FLG = 0 OR T_MANIFEST_ENTRY6.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	AND (T_MANIFEST_RELATION.DELETE_FLG = 0 OR T_MANIFEST_RELATION.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	INNER JOIN T_MANIFEST_ENTRY T_MANIFEST_ENTRY7 ON	 ");
                sql.Append(" 	T_MANIFEST_RELATION.NEXT_SYSTEM_ID = T_MANIFEST_ENTRY7.SYSTEM_ID	 ");
                sql.Append(" 	AND (T_MANIFEST_RELATION.DELETE_FLG = 0 OR T_MANIFEST_RELATION.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_KBN M_HAIKI_KBN_7E1 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.HAIKI_KBN_CD = M_HAIKI_KBN_7E1.HAIKI_KBN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_KYOTEN M_KYOTEN_7E2 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.KYOTEN_CD = M_KYOTEN_7E2.KYOTEN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_TORIHIKISAKI M_TORIHIKISAKI_7E4 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.TORIHIKISAKI_CD = M_TORIHIKISAKI_7E4.TORIHIKISAKI_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_KONGOU_SHURUI M_KONGOU_SHURUI_7E5 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.HAIKI_KBN_CD = M_KONGOU_SHURUI_7E5.HAIKI_KBN_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.KONGOU_SHURUI_CD = M_KONGOU_SHURUI_7E5.KONGOU_SHURUI_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_UNIT M_UNIT_7E6 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.HAIKI_UNIT_CD = M_UNIT_7E6.UNIT_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_7E7 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.LAST_SBN_YOTEI_GYOUSHA_CD = M_GYOUSHA_7E7.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_7E8 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.LAST_SBN_YOTEI_GYOUSHA_CD = M_GENBA_7E8.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.LAST_SBN_YOTEI_GENBA_CD = M_GENBA_7E8.GENBA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_UNIT M_UNIT_7E9 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.YUUKA_UNIT_CD = M_UNIT_7E9.UNIT_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_7E10 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.LAST_SBN_GYOUSHA_CD = M_GYOUSHA_7E10.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN T_MANIFEST_DETAIL T_MANIFEST_DETAIL7 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.SYSTEM_ID = T_MANIFEST_DETAIL7.SYSTEM_ID	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.SEQ = T_MANIFEST_DETAIL7.SEQ	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_SHURUI M_HAIKI_SHURUI_7D1 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.HAIKI_SHURUI_CD = M_HAIKI_SHURUI_7D1.HAIKI_SHURUI_CD	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_NAME M_HAIKI_NAME_7D2 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.HAIKI_NAME_CD = M_HAIKI_NAME_7D2.HAIKI_NAME_CD	 ");
                sql.Append(" 	LEFT JOIN M_NISUGATA M_NISUGATA_7D3 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.NISUGATA_CD = M_NISUGATA_7D3.NISUGATA_CD	 ");
                sql.Append(" 	LEFT JOIN M_UNIT M_UNIT_7D4 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.HAIKI_UNIT_CD = M_UNIT_7D4.UNIT_CD	 ");
                sql.Append(" 	LEFT JOIN M_SHOBUN_HOUHOU M_SHOBUN_HOUHOU_7D5 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU_7D5.SHOBUN_HOUHOU_CD	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_7D6 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.LAST_SBN_GYOUSHA_CD = M_GYOUSHA_7D6.GYOUSHA_CD	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_7D7 ON	 ");
                sql.Append(" 	T_MANIFEST_DETAIL7.LAST_SBN_GYOUSHA_CD = M_GENBA_7D7.GYOUSHA_CD	 ");
                sql.Append(" 	AND T_MANIFEST_DETAIL7.LAST_SBN_GENBA_CD = M_GENBA_7D7.GENBA_CD	 ");
                sql.Append(" 	LEFT JOIN T_MANIFEST_UPN T_MANIFEST_UPN7 ON	 ");
                sql.Append(" 	T_MANIFEST_ENTRY7.SYSTEM_ID = T_MANIFEST_UPN7.SYSTEM_ID	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.SEQ = T_MANIFEST_UPN7.SEQ	 ");
                sql.Append(" 	AND T_MANIFEST_ENTRY7.FIRST_MANIFEST_KBN = 1	 ");
                sql.Append(" 	AND (T_MANIFEST_ENTRY7.DELETE_FLG = 0 OR T_MANIFEST_ENTRY7.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	INNER JOIN DT_R18_EX ON	 ");
                sql.Append(" 	T_MANIFEST_RELATION.FIRST_SYSTEM_ID = DT_R18_EX.SYSTEM_ID	 ");
                sql.Append(" 	AND (T_MANIFEST_RELATION.DELETE_FLG = 0 OR T_MANIFEST_RELATION.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_8E1 ON	 ");
                sql.Append(" 	DT_R18_EX.HST_GYOUSHA_CD = M_GYOUSHA_8E1.GYOUSHA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_8E2 ON	 ");
                sql.Append(" 	DT_R18_EX.HST_GYOUSHA_CD = M_GENBA_8E2.GYOUSHA_CD	 ");
                sql.Append(" 	AND DT_R18_EX.HST_GENBA_CD = M_GENBA_8E2.GENBA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GYOUSHA M_GYOUSHA_8E3 ON	 ");
                sql.Append(" 	DT_R18_EX.SBN_GYOUSHA_CD = M_GYOUSHA_8E3.GYOUSHA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_GENBA M_GENBA_8E4 ON	 ");
                sql.Append(" 	DT_R18_EX.SBN_GYOUSHA_CD = M_GENBA_8E4.GYOUSHA_CD	 ");
                sql.Append(" 	AND DT_R18_EX.SBN_GENBA_CD = M_GENBA_8E4.GENBA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_HAIKI_NAME M_HAIKI_NAME_8E5 ON	 ");
                sql.Append(" 	DT_R18_EX.HAIKI_NAME_CD = M_HAIKI_NAME_8E5.HAIKI_NAME_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_SHOBUN_HOUHOU M_SHOBUN_HOUHOU_8E6 ON	 ");
                sql.Append(" 	DT_R18_EX.SBN_HOUHOU_CD = M_SHOBUN_HOUHOU_8E6.SHOBUN_HOUHOU_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_DENSHI_TANTOUSHA M_DENSHI_TANTOUSHA_8E7 ON	 ");
                sql.Append(" 	DT_R18_EX.HOUKOKU_TANTOUSHA_CD = M_DENSHI_TANTOUSHA_8E7.TANTOUSHA_CD	 ");
                sql.Append(" 	AND M_DENSHI_TANTOUSHA_8E7.TANTOUSHA_KBN = 4	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	LEFT JOIN M_DENSHI_TANTOUSHA M_DENSHI_TANTOUSHA_8E8 ON	 ");
                sql.Append(" 	DT_R18_EX.SBN_TANTOUSHA_CD = M_DENSHI_TANTOUSHA_8E8.TANTOUSHA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	AND M_DENSHI_TANTOUSHA_8E8.TANTOUSHA_KBN = 5	 ");
                sql.Append(" 	LEFT JOIN M_DENSHI_TANTOUSHA M_DENSHI_TANTOUSHA_8E9 ON	 ");
                sql.Append(" 	DT_R18_EX.UPN_TANTOUSHA_CD = M_DENSHI_TANTOUSHA_8E9.TANTOUSHA_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	AND M_DENSHI_TANTOUSHA_8E9.TANTOUSHA_KBN = 3	 ");
                sql.Append(" 	LEFT JOIN M_SHARYOU M_SHARYOU_8E10 ON	 ");
                sql.Append(" 	DT_R18_EX.SBN_GYOUSHA_CD = M_SHARYOU_8E10.GYOUSHA_CD	 ");
                sql.Append(" 	AND DT_R18_EX.SHARYOU_CD = M_SHARYOU_8E10.SHARYOU_CD	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	INNER JOIN DT_MF_TOC ON	 ");
                sql.Append(" 	DT_R18_EX.KANRI_ID = DT_MF_TOC.KANRI_ID 	 ");
                sql.Append(" 	AND (DT_R18_EX.DELETE_FLG = 0 OR DT_R18_EX.DELETE_FLG IS NULL)	 ");
                sql.Append(" 	INNER JOIN DT_R18 ON	 ");
                sql.Append(" 	DT_MF_TOC.KANRI_ID = DT_R18.KANRI_ID	 ");
                sql.Append(" 	AND DT_MF_TOC.LATEST_SEQ = DT_R18.SEQ	 ");
                sql.Append(" 	INNER JOIN DT_R19 ON	 ");
                sql.Append(" 	DT_R18.KANRI_ID = DT_R19.KANRI_ID	 ");
                sql.Append(" 	AND DT_R18.SEQ = DT_R19.SEQ	 ");
                sql.Append(" 	WHERE 1 = 1	 ");

                if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_MANIFEST_ENTRY6.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                    sql.Append(" AND T_MANIFEST_ENTRY7.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                }

                // 6.マニ１次-1.交付年月日
                if (this.disp_Flg == 6 && this.form.numTxtBox_Denpyou_Date.Text.Equals(KOUHU_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_ENTRY6.KOUFU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_ENTRY6.KOUFU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 6.マニ１次-2.運搬終了日
                if (this.disp_Flg == 6 && this.form.numTxtBox_Denpyou_Date.Text.Equals(UNPAN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_UPN6.UPN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_UPN6.UPN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 6.マニ１次-3.処分終了日
                if (this.disp_Flg == 6 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL6.SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL6.SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 6.マニ１次-4.最終処分終了日
                if (this.disp_Flg == 6 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SAISHUU_SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL6.LAST_SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL6.LAST_SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 7.マニ２次-1.交付年月日
                if (this.disp_Flg == 7 && this.form.numTxtBox_Denpyou_Date.Text.Equals(KOUHU_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_ENTRY7.KOUFU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_ENTRY7.KOUFU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 7.マニ２次-2.運搬終了日
                if (this.disp_Flg == 7 && this.form.numTxtBox_Denpyou_Date.Text.Equals(UNPAN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_UPN7.UPN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_UPN7.UPN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 7.マニ２次-3.処分終了日
                if (this.disp_Flg == 7 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL7.SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL7.SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 7.マニ２次-4.最終処分終了日
                if (this.disp_Flg == 7 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SAISHUU_SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL7.LAST_SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_MANIFEST_DETAIL7.LAST_SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 8.電マニ-1.交付年月日
                if (this.disp_Flg == 8 && this.form.numTxtBox_Denpyou_Date.Text.Equals(KOUHU_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND DT_R18.HIKIWATASHI_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND DT_R18.HIKIWATASHI_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 8.電マニ-2.運搬終了日
                if (this.disp_Flg == 8 && this.form.numTxtBox_Denpyou_Date.Text.Equals(UNPAN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND DT_R19.UPN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND DT_R19.UPN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 8.電マニ-3.処分終了日
                if (this.disp_Flg == 8 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND DT_R18.SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND DT_R18.SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

                // 8.電マニ-4.最終処分終了日
                if (this.disp_Flg == 8 && this.form.numTxtBox_Denpyou_Date.Text.Equals(SAISHUU_SHOBUN_DATE_NUM))
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND DT_R18.LAST_SBN_END_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND DT_R18.LAST_SBN_END_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSqlTB3", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// TB4のSQL文作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSqlTB4(StringBuilder sql)
        {
            try
            {
                LogUtility.DebugMethodStart(sql);

                var parentForm = (UIBaseForm)this.form.Parent;

                //SQL文格納StringBuilder
                sql.Append(" FROM T_UNCHIN_ENTRY T_UNCHIN_ENTRY9 ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_9E1 ON ");
                sql.Append(" T_UNCHIN_ENTRY9.KYOTEN_CD = M_KYOTEN_9E1.KYOTEN_CD ");
                sql.Append(" AND (T_UNCHIN_ENTRY9.DELETE_FLG = 0 OR T_UNCHIN_ENTRY9.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_DENSHU_KBN M_DENSHU_KBN_9E3 ON ");
                sql.Append(" T_UNCHIN_ENTRY9.DENSHU_KBN_CD = M_DENSHU_KBN_9E3.DENSHU_KBN_CD ");
                sql.Append(" AND (T_UNCHIN_ENTRY9.DELETE_FLG = 0 OR T_UNCHIN_ENTRY9.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN T_UNCHIN_DETAIL ON ");
                sql.Append(" T_UNCHIN_ENTRY9.SYSTEM_ID = T_UNCHIN_DETAIL.SYSTEM_ID  ");
                sql.Append(" AND T_UNCHIN_ENTRY9.SEQ = T_UNCHIN_DETAIL.SEQ ");
                sql.Append(" AND (T_UNCHIN_ENTRY9.DELETE_FLG = 0 OR T_UNCHIN_ENTRY9.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_UNIT M_UNIT_KBN_9D1 ON ");
                sql.Append(" T_UNCHIN_DETAIL.UNIT_CD = M_UNIT_KBN_9D1.UNIT_CD ");
                sql.Append(" WHERE 1 = 1 ");

                if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_UNCHIN_ENTRY9.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                }

                // 9.運賃-伝票日付
                if (this.disp_Flg == 9)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND T_UNCHIN_ENTRY9.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND T_UNCHIN_ENTRY9.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSqlTB4", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// TB5のSQL文作成
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSqlTB5(StringBuilder sql)
        {
            try
            {
                LogUtility.DebugMethodStart(sql);

                var parentForm = (UIBaseForm)this.form.Parent;

                //SQL文格納StringBuilder
                sql.Append(" FROM T_UR_SH_ENTRY DAINO_ENTRY10 ");
                sql.Append(" LEFT JOIN T_UR_SH_DETAIL DAINO_DETAIL10 ON ");
                sql.Append(" DAINO_ENTRY10.SYSTEM_ID = DAINO_DETAIL10.SYSTEM_ID ");
                sql.Append(" AND DAINO_ENTRY10.SEQ = DAINO_DETAIL10.SEQ ");
                sql.Append(" AND (DAINO_ENTRY10.DELETE_FLG = 0 OR DAINO_ENTRY10.DELETE_FLG IS NULL) ");
                sql.Append(" LEFT JOIN M_KYOTEN M_KYOTEN_10E1 ON ");
                sql.Append(" DAINO_ENTRY10.KYOTEN_CD = M_KYOTEN_10E1.KYOTEN_CD ");
                sql.Append(" AND (DAINO_ENTRY10.DELETE_FLG = 0 OR DAINO_ENTRY10.DELETE_FLG IS NULL) ");
                sql.Append(" WHERE 1 = 1 ");
                sql.Append(" AND ISNULL(DAINO_ENTRY10.DAINOU_FLG,0) = 1 ");

                // 拠点
                if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND DAINO_ENTRY10.KYOTEN_CD = '" + this.headForm.KYOTEN_CD.Text + "' ");
                }

                // 10.代納-伝票日付
                if (this.disp_Flg == 10)
                {
                    if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
                    {
                        sql.Append(" AND DAINO_ENTRY10.DENPYOU_DATE >= '" + this.form.dtpDateFrom.Value.ToString() + "' ");
                    }

                    if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
                    {
                        sql.Append(" AND DAINO_ENTRY10.DENPYOU_DATE <= '" + this.form.dtpDateTo.Value.ToString() + "' ");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSqlTB5", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            try
            {
                LogUtility.DebugMethodStart(value);

                if (string.IsNullOrEmpty(value) == true)
                {
                    return "0";
                }
                else
                {
                    return string.Format("{0:#,0}", Convert.ToDecimal(value));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetComma", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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

        #region 未使用メソッド

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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

        #endregion

        /// 20141023 Houkakou 「伝票紐付一覧」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtpDateFrom;
            var ToTextBox = this.form.dtpDateTo;

            ToTextBox.Text = FromTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141023 Houkakou 「伝票紐付一覧」のダブルクリックを追加する　end

        /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.dtpDateFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.dtpDateTo.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.dtpDateTo.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.dtpDateFrom.Value);
            DateTime date_to = Convert.ToDateTime(this.form.dtpDateTo.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.dtpDateFrom.IsInputErrorOccured = true;
                this.form.dtpDateTo.IsInputErrorOccured = true;
                this.form.dtpDateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.dtpDateTo.BackColor = Constans.ERROR_COLOR;

                string strFrom = this.form.DenoyouDate_Label.Text + "From";
                string strTo = this.form.DenoyouDate_Label.Text + "To";

                if (this.form.Denpyou_Date_Panel.Visible == true)
                {
                    if (this.form.Denpyou_Date_RdoBtn1.Checked)
                    {
                        strFrom = "交付年月日From";
                        strTo = "交付年月日To";
                    }
                    else if (this.form.Denpyou_Date_RdoBtn2.Checked)
                    {
                        strFrom = "運搬終了日From";
                        strTo = "運搬終了日To";
                    }
                    else if (this.form.Denpyou_Date_RdoBtn3.Checked)
                    {
                        strFrom = "処分終了日From";
                        strTo = "処分終了日To";
                    }
                    else if (this.form.Denpyou_Date_RdoBtn4.Checked)
                    {
                        strFrom = "最終処分終了日From";
                        strTo = "最終処分終了日To";
                    }
                }
                else
                {
                    strFrom = "伝票日付From";
                    strTo = "伝票日付To";
                }

                string[] errorMsg = { strFrom, strTo };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.dtpDateFrom.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region dtpDateFrom_Leaveイベント
        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtpDateFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtpDateTo.Text))
            {
                this.form.dtpDateTo.IsInputErrorOccured = false;
                this.form.dtpDateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region dtpDateTo_Leaveイベント
        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void dtpDateTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.dtpDateFrom.Text))
            {
                this.form.dtpDateFrom.IsInputErrorOccured = false;
                this.form.dtpDateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「伝票紐付一覧」の日付チェックを追加する　end
    }
}
