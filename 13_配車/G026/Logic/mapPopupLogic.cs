using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    public class mapPopupLogic
    {
        #region フィールド

        private mapPopupForm form;

        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.HaishaWariateDay.Setting.PopupButtonSetting.xml";

        private DataTable parentDataTable;

        /// <summary>
        /// コンテナ稼働予定検索結果
        /// </summary>
        private T_CONTENA_RESERVE[] arrContenaReserve;

        /// <summary>
        /// システム情報エンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        #region DAO

        /// <summary>
        /// システム情報Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 地図表示用Dao
        /// </summary>
        private DAO_MAP mapDao;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 現場Dao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// 都道府県Dao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;
        /// <summary>
        /// コース名Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseDao;
        /// <summary>
        /// 単位Dao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// 配車割当（一日）DAO
        /// </summary>
        private DAO_T_HAISHA_WARIATE_DAY dao_HAISHA;
        /// <summary>
        /// 受付（収集）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_ENTRY daoT_UKETSUKE_SS_ENTRY;
        /// <summary>
        /// 受付（収集）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_DETAIL daoT_UKETSUKE_SS_DETAIL;
        /// <summary>
        /// 受付（出荷）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_ENTRY daoT_UKETSUKE_SK_ENTRY;
        /// <summary>
        /// 受付（出荷）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_DETAIL daoT_UKETSUKE_SK_DETAIL;
        /// <summary>
        /// 定期配車入力DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_ENTRY daoT_TEIKI_HAISHA_ENTRY;
        /// <summary>
        /// 定期配車明細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_DETAIL daoT_TEIKI_HAISHA_DETAIL;
        /// <summary>
        /// 定期配車詳細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_SHOUSAI daoT_TEIKI_HAISHA_SHOUSAI;
        /// <summary>
        /// 品名DAO
        /// </summary>
        private DAO_M_HINMEI daoM_HINMEI;
        /// <summary>
        /// コンテナ稼動予定DAO
        /// </summary>
        private DAO_T_CONTENA_RESERVE daoT_CONTENA_RESERVE;

        #endregion

        /// <summary>
        /// 配車割当（一日）DTO
        /// </summary>
        private DTO_Haisha dtoHaisha = new DTO_Haisha();
        /// <summary>
        /// システムID枝番DTO
        /// </summary>
        private DTO_IdSeq dtoIdSeq = new DTO_IdSeq();

        /// <summary>
        /// システムID枝番明細システムIDDTO
        /// </summary>
        private DTO_IdSeqDetid dtoIdSeqDetid = new DTO_IdSeqDetid();

        private DataTable resultMihaisha = new DataTable();

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        // 検索結果のカラム名(個体管理)
        private string[] columnNamesForKotaiknri = { "SecchiChouhuku", "CONTENA_SHURUI_CD", "CONTENA_SHURUI_NAME_RYAKU", "CONTENA_CD", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "GYOUSHA_NAME_RYAKU"
                                                    , "GENBA_CD", "GENBA_NAME_RYAKU", "EIGYOU_TANTOU_CD", "SHAIN_NAME_RYAKU", "SECCHI_DATE", "DAYSCOUNT", "GRAPH"};
        private string[] columnTyepesForKotaiKanri = { "System.String","System.String","System.String","System.String","System.String","System.String","System.String"
                                        ,"System.String","System.String","System.String","System.String","System.DateTime","System.Int32","System.Double"};

        private string COLUMN_NAME_CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";
        private string COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU = "CONTENA_SHURUI_NAME_RYAKU";
        private string COLUMN_NAME_CONTENA_CD = "CONTENA_CD";
        private string COLUMN_NAME_CONTENA_NAME_RYAKU = "CONTENA_NAME_RYAKU";
        private string COLUMN_NAME_GYOUSHA_CD = "GYOUSHA_CD";
        private string COLUMN_NAME_GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
        private string COLUMN_NAME_GENBA_CD = "GENBA_CD";
        private string COLUMN_NAME_GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
        private string COLUMN_NAME_EIGYOU_TANTOU_CD = "EIGYOU_TANTOU_CD";
        private string COLUMN_NAME_SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";
        private string COLUMN_NAME_SECCHI_DATE = "SECCHI_DATE";
        private string COLUMN_NAME_DAYSCOUNT = "DAYSCOUNT";
        private string COLUMN_NAME_GRAPH = "GRAPH";
        private string COLUMN_NAME_DAISUU = "DAISUU";
        private string COLUMN_NAME_SECCHICHOUUHUKU = "SecchiChouhuku";

        // 重複設置カラムに表示する文字列
        private string CHOUHUKU_SECCHI_VALUE = "○";

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOCls SearchString { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public mapPopupLogic(mapPopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.courseDao = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.dao_HAISHA = DaoInitUtility.GetComponent<DAO_T_HAISHA_WARIATE_DAY>();
            this.daoT_UKETSUKE_SS_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_ENTRY>();
            this.daoT_UKETSUKE_SS_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_DETAIL>();
            this.daoT_UKETSUKE_SK_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_ENTRY>();
            this.daoT_UKETSUKE_SK_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_DETAIL>();
            this.daoT_TEIKI_HAISHA_ENTRY = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_ENTRY>();
            this.daoT_TEIKI_HAISHA_DETAIL = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_DETAIL>();
            this.daoT_TEIKI_HAISHA_SHOUSAI = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_SHOUSAI>();
            this.daoM_HINMEI = DaoInitUtility.GetComponent<DAO_M_HINMEI>();
            this.daoT_CONTENA_RESERVE = DaoInitUtility.GetComponent<DAO_T_CONTENA_RESERVE>();

            this.mapDao = DaoInitUtility.GetComponent<DAO_MAP>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化

        #region WindowInit

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                // ポップアップのサイズを指定
                this.form.Size = new Size(1209, 768);

                // ボタンの初期化
                this.ButtonInit();

                // システム設定の取得
                this.GetSysInfoInit();

                // イベント初期化
                this.EventInit();

                // 配車割当から持ってきた値を変数に保存
                this.parentDataTable = this.form.table;

                // 画面の項目初期化
                this.ObjectInit();

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 画面の項目初期化

        /// <summary>
        /// 画面項目の初期値設定
        /// </summary>
        private void ObjectInit()
        {
            #region ヘッダ(配車割当から値を引き継ぎ)

            foreach (DataRow row in parentDataTable.Rows)
            {
                // 未配車列はこれらの情報を保持していない
                if (Convert.ToString(row["DATA_SHURUI"]) == "0")
                {
                    // 作業日
                    this.form.SAGYOU_DATE.Text = Convert.ToString(row["SAGYOU_DATE"]);

                    // 車輛
                    this.form.SHARYOU_CD.Text = Convert.ToString(row["SARYOU_CD"]);
                    this.form.SHARYOU_NAME_RYAKU.Text = Convert.ToString(row["SARYOU_NAME"]);

                    // 車種
                    this.form.SHASHU_CD.Text = Convert.ToString(row["SHASHU_CD"]);
                    this.form.SHASHU_NAME_RYAKU.Text = Convert.ToString(row["SHASHU_NAME"]);

                    // 運転者
                    this.form.UNTENSHA_CD.Text = Convert.ToString(row["UNTENSHA_CD"]);
                    this.form.UNTENSHA_NAME.Text = Convert.ToString(row["UNTENSHA_NAME"]);

                    // 運搬業者
                    this.form.UNPAN_GYOUSHA_CD.Text = Convert.ToString(row["UNPAN_GYOUSHA_CD"]);
                    this.form.UNPAN_GYOUSHA_NAME.Text = Convert.ToString(row["UNPAN_GYOUSHA_NAME"]);
                }
            }

            #endregion

            #region 未配車データ絞り込み設定タブ

            // 未配車表示は最後に

            // 伝票種類指定
            this.form.chk_Shushu.Checked = true;
            this.form.chk_Shukka.Checked = true;
            this.form.chk_Teiki.Checked = true;

            // 作業日指定
            this.form.txtNum_SagyoubiShitei.Text = "3";

            // 拠点指定
            this.form.txtNum_KyotenShitei.Text = "2";

            // 拠点DGV
            this.KyotenDGV();

            // 現着時間指定
            this.form.txtNum_GenchakuShitei.Text = "2";
            this.form.chk_Genchaku.Checked = true;

            // 現着時間DGV
            this.GenchakuTimeDGV();

            // 車種指定
            this.form.txtNum_ShashuShitei.Text = "2";
            this.form.chk_Shashu.Checked = true;

            // 車種DGV
            this.ShashuDGV();

            // 運搬業者指定
            this.form.txtNum_UnpanGyousha.Text = "3";
            this.form.chk_UnpanGyousha.Checked = true;

            // 運転者指定
            this.form.txtNum_Untensha.Text = "3";
            this.form.chk_Untensha.Checked = true;

            // コンテナ作業指定
            this.form.txtNum_ContenaSagyouShitei.Text = "3";

            // コンテナ状況指定
            this.form.chk_Secchi.Checked = true;
            this.form.chk_Hikiage.Checked = true;
            this.form.chk_Koukan.Checked = true;

            // コンテナ種類指定
            this.form.txtNum_ContenaShurui.Text = "2";

            // コンテナ種類DGV
            this.ContenaShuruiDGV();

            // 未配車表示
            this.form.txtNum_MihaishaHyouzi.Text = "2";

            #endregion

            #region 設置コンテナ一覧表示設定タブ

            // 設置コンテナ表示は最後に

            // コンテナ種類指定
            this.form.txtNum_ContenaShurui2.Text = "2";

            // コンテナ種類DGV2

            // 設置期間指定
            this.form.txtNum_SecchiKikan.Text = "2";
            this.form.txtNum_SecchiFrom.Text = string.Empty;
            this.form.txtNum_SecchiTo.Text = string.Empty;

            // 重複設置絞込
            this.form.txtNum_ChouhukuSecchiNomi.Text = "2";

            // 設置コンテナ表示
            this.form.txtNum_SecchiContena.Text = "2";

            #endregion
        }

        #endregion

        #region EventInit

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            // 反映ボタン(F9)イベント生成
            this.form.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            this.form.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            // 閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 未配車表示
            this.form.txtNum_MihaishaHyouzi.TextChanged += new EventHandler(this.form.txtNum_MihaishaHyouzi_TextChanged);
            // 伝票種類指定
            this.form.chk_Shushu.CheckedChanged += new EventHandler(this.form.chk_DenpyouShurui_CheckedChanged);
            this.form.chk_Shukka.CheckedChanged += new EventHandler(this.form.chk_DenpyouShurui_CheckedChanged);
            this.form.chk_Teiki.CheckedChanged += new EventHandler(this.form.chk_DenpyouShurui_CheckedChanged);
            // 拠点指定
            this.form.txtNum_KyotenShitei.TextChanged += new EventHandler(this.form.txtNum_KyotenShitei_TextChanged);
            // 現着時間指定
            this.form.txtNum_GenchakuShitei.TextChanged += new EventHandler(this.form.txtNum_GenchakuShitei_TextChanged);
            // 車種指定
            this.form.txtNum_ShashuShitei.TextChanged += new EventHandler(this.form.txtNum_ShashuShitei_TextChanged);
            // 運搬業者指定
            this.form.txtNum_UnpanGyousha.TextChanged += new EventHandler(this.form.txtNum_UnpanGyousha_TextChanged);
            // 運転者指定
            this.form.txtNum_Untensha.TextChanged += new EventHandler(this.form.txtNum_Untensha_TextChanged);
            // コンテナ作業指定
            this.form.txtNum_ContenaSagyouShitei.TextChanged += new EventHandler(this.form.txtNum_ContenaSagyouShitei_TextChanged);
            // コンテナ種類指定
            this.form.txtNum_ContenaShurui.TextChanged += new EventHandler(this.form.txtNum_ContenaShurui_TextChanged);
            // 設置コンテナ表示
            this.form.txtNum_SecchiContena.TextChanged += new EventHandler(this.form.txtNum_SecchiContena_TextChanged);
            // コンテナ種類指定
            this.form.txtNum_ContenaShurui2.TextChanged += new EventHandler(this.form.txtNum_ContenaShurui2_TextChanged);
            // 設置期間指定
            this.form.txtNum_SecchiKikan.TextChanged += new EventHandler(this.form.txtNum_SecchiKikan_TextChanged);
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (SuperPopupForm)this.form;
            var controlUtil = new ControlUtility();
            foreach (var button in buttonSetting)
            {
                //設定対象のコントロールを探して名称の設定を行う
                var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                cont.Text = button.IchiranButtonName;
                cont.Tag = button.IchiranButtonHintText;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン情報の設定を行う
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();

            //生成したアセンブリの情報を送って
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }
        #endregion

        #endregion

        #region F9 地図表示処理

        internal void MapOpen()
        {
            try
            {
                if (this.form.msgLogic.MessageBoxShowConfirm("地図を表示しますか？" +
    Environment.NewLine + "※緯度/経度が登録されていない現場は表示されません。") == DialogResult.No)
                {
                    return;
                }

                // 入力チェック
                if (!MapOpenCheck()) return;

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.createMapboxDto();
                if (dtos.Count == 0)
                {
                    this.form.msgLogic.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_HAISHA_WARIATE_DAY);
            }
            catch (Exception ex)
            {
                LogUtility.Error("MapOpen", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #region 入力チェック

        private bool MapOpenCheck()
        {
            // 必須項目のアラート

            string errMsg = "検索条件を正しく設定してください。" + Environment.NewLine + Environment.NewLine + "タブ名：未配車データ絞り込み設定" + Environment.NewLine;
            bool chk = false;

            // 未配車表示
            if (string.IsNullOrEmpty(this.form.txtNum_MihaishaHyouzi.Text))
            {
                this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：未配車表示");
                return false;
            }

            if (this.form.txtNum_MihaishaHyouzi.Text == "1")
            {
                // 伝票種類指定
                if ((!this.form.chk_Shushu.Checked && !this.form.chk_Shukka.Checked && !this.form.chk_Teiki.Checked) ||
                    string.IsNullOrEmpty(this.form.txtNum_MihaishaHyouzi.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：伝票種類");
                    return false;
                }

                // 作業日指定
                if (string.IsNullOrEmpty(this.form.txtNum_SagyoubiShitei.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：作業日指定");
                    return false;
                }

                // 拠点指定
                chk = false;
                if (string.IsNullOrEmpty(this.form.txtNum_KyotenShitei.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：拠点指定");
                    return false;
                }
                if (this.form.txtNum_KyotenShitei.Text == "1")
                {
                    // dgv
                    foreach (DataGridViewRow r in this.form.dgvKyoten.Rows)
                    {
                        if (Convert.ToBoolean(r.Cells["CHECKBOX"].Value)) { chk = true; }
                    }
                    if (!chk)
                    {
                        this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：拠点");
                        return false;
                    }
                }

                // 現着時間指定
                chk = false;
                if (string.IsNullOrEmpty(this.form.txtNum_GenchakuShitei.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：現着時間指定");
                    return false;
                }
                if (this.form.txtNum_GenchakuShitei.Text == "1")
                {
                    // 現着時間は収集か出荷にチェックが入っている場合のみチェック
                    if (this.form.chk_Shushu.Checked || this.form.chk_Shukka.Checked)
                    {
                        // dgv
                        foreach (DataGridViewRow r in this.form.dgvGenchaku.Rows)
                        {
                            if (Convert.ToBoolean(r.Cells["CHECKBOX2"].Value)) { chk = true; }
                        }
                        if (!chk && !this.form.chk_Genchaku.Checked)
                        {
                            this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：現着時間");
                            return false;
                        }
                    }
                }

                // 車種指定
                chk = false;
                if (string.IsNullOrEmpty(this.form.txtNum_ShashuShitei.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：車種指定");
                    return false;
                }
                if (this.form.txtNum_ShashuShitei.Text == "1")
                {
                    // dgv
                    foreach (DataGridViewRow r in this.form.dgvShashu.Rows)
                    {
                        if (Convert.ToBoolean(r.Cells["CHECKBOX3"].Value)) { chk = true; }
                    }
                    if (!chk && !this.form.chk_Shashu.Checked)
                    {
                        this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：車種");
                        return false;
                    }
                }

                // 運搬業者指定
                if (string.IsNullOrEmpty(this.form.txtNum_UnpanGyousha.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：運搬業者指定");
                    return false;
                }

                // 運転者指定
                if (string.IsNullOrEmpty(this.form.txtNum_Untensha.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：運転者指定");
                    return false;
                }

                // コンテナ作業指定
                if (string.IsNullOrEmpty(this.form.txtNum_ContenaSagyouShitei.Text))
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：コンテナ作業指定");
                    return false;
                }


                // コンテナ状況指定
                if (this.form.txtNum_ContenaSagyouShitei.Text == "1" &&
                    !this.form.chk_Secchi.Checked && !this.form.chk_Hikiage.Checked && !this.form.chk_Koukan.Checked)
                {
                    this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：コンテナ状況");
                    return false;
                }

                // コンテナ種類指定
                chk = false;
                if (this.form.txtNum_ContenaSagyouShitei.Text == "1" && this.form.txtNum_ContenaShurui.Text == "1")
                {
                    // 現着時間は収集か出荷にチェックが入っている場合のみチェック
                    if (this.form.chk_Shushu.Checked || this.form.chk_Shukka.Checked)
                    {
                        // dgv
                        foreach (DataGridViewRow r in this.form.dgvContena.Rows)
                        {
                            if (Convert.ToBoolean(r.Cells["CHECKBOX4"].Value)) { chk = true; }
                        }
                        if (!chk)
                        {
                            this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：コンテナ種類");
                            return false;
                        }
                    }
                }
            }

            errMsg = "検索条件を正しく設定してください。" + Environment.NewLine + Environment.NewLine + "タブ名：設置コンテナ一覧表示設定" + Environment.NewLine;

            // 設置コンテナ表示
            if (string.IsNullOrEmpty(this.form.txtNum_SecchiContena.Text))
            {
                this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：設置コンテナ表示");
                return false;
            }

            if (this.form.txtNum_SecchiContena.Text == "1")
            {
                // コンテナ種類指定
                chk = false;
                if (this.form.txtNum_ContenaShurui2.Text == "1")
                {
                    // dgv
                    foreach (DataGridViewRow r in this.form.dgvContena2.Rows)
                    {
                        if (Convert.ToBoolean(r.Cells["CHECKBOX5"].Value)) { chk = true; }
                    }
                    if (!chk)
                    {
                        this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：コンテナ種類");
                        return false;
                    }
                }

                // 設置期間指定
                if (this.form.txtNum_SecchiKikan.Text == "1")
                {
                    if (string.IsNullOrEmpty(this.form.txtNum_SecchiFrom.Text) && string.IsNullOrEmpty(this.form.txtNum_SecchiTo.Text))
                    {
                        this.form.msgLogic.MessageBoxShowError(errMsg + "項目名：設置期間");
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 連携処理

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        private List<mapDtoList> createMapboxDto()
        {
            try
            {
                List<mapDtoList> dtoLists = new List<mapDtoList>();

                int layerId = 1;
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = layerId;
                List<mapDto> dtos = new List<mapDto>();

                // 地図出力に必要な情報を収集
                #region 割当済み

                if (this.parentDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in parentDataTable.Rows)
                    {
                        if (row["DATA_SHURUI"].ToString() != "0")
                        {
                            continue;
                        }
                        if (row["SYSTEM_ID"].ToString() != "0")
                        {
                            this.dtoIdSeq.SystemId = Convert.ToInt64(row["SYSTEM_ID"]);
                            this.dtoIdSeq.Seq = Convert.ToInt32(row["SEQ"]);

                            switch (Convert.ToInt16(row["SHUBETSU_KBN"]))
                            {
                                case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                                    #region 収集受付
                                    var sse = this.daoT_UKETSUKE_SS_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                    if (sse != null)
                                    {
                                        mapDto dto = new mapDto();
                                        dto.id = layerId;
                                        dto.layerNo = layerId;
                                        dto.courseName = string.Empty;
                                        dto.dayName = string.Empty;
                                        dto.teikiHaishaNo = this.NullToSpace(sse.UKETSUKE_NUMBER);
                                        dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                   + "　"
                                                   + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                        dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                   + "　"
                                                   + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                                        dto.dataShurui = "0";
                                        dto.dataKBN = "1";
                                        dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);
                                        if (string.IsNullOrEmpty(sse.GENCHAKU_TIME))
                                        {
                                            dto.genbaChaku = this.NullToSpace(sse.GENCHAKU_TIME_NAME);
                                        }
                                        else
                                        {
                                            dto.genbaChaku = this.NullToSpace(sse.GENCHAKU_TIME_NAME) + DateTime.Parse(sse.GENCHAKU_TIME).ToString("HH:mm");
                                        }
                                        MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                        MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(sse.NIOROSHI_GYOUSHA_CD, sse.NIOROSHI_GENBA_CD);
                                        dto.NNGyoushaName = this.NullToSpace(sse.NIOROSHI_GYOUSHA_NAME);    // マスタではなく伝票の業者名を抽出する
                                        dto.NNGenbaName = this.NullToSpace(sse.NIOROSHI_GENBA_NAME);        // マスタではなく伝票の現場名を抽出する
                                        dto.NNAddress = this.NullToSpace(mapGenbaDto.ADDRESS);

                                        // コンテナ稼働予定データを検索
                                        this.arrContenaReserve = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                                        // 設置台数を取得
                                        string setti = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI).Sum(r => (int)r.DAISUU_CNT));
                                        // 引揚台数を取得
                                        string hikiage = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE).Sum(r => (int)r.DAISUU_CNT));
                                        dto.contenaName = string.Format("設置{0}台　引揚{1}台", setti, hikiage);

                                        var sedArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                                        string hinmeiName = string.Empty;
                                        foreach (var sed in sedArray)
                                        {
                                            string suuryou = string.Empty;
                                            if (!string.IsNullOrEmpty(this.NullToSpace(sed.SUURYOU)))
                                            {
                                                suuryou = Convert.ToDecimal(sed.SUURYOU.Value).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                            }
                                            string unitName = this.getUnitName(Convert.ToString(sed.UNIT_CD));
                                            if (string.IsNullOrEmpty(hinmeiName))
                                            {
                                                hinmeiName += sed.HINMEI_NAME + " " + suuryou + unitName;
                                            }
                                            else
                                            {
                                                hinmeiName += "/" + sed.HINMEI_NAME + " " + suuryou + unitName;
                                            }
                                        }
                                        dto.hinmei = hinmeiName;

                                        mapLogic = new MapboxGLJSLogic();
                                        mapGenbaDto = mapLogic.mapGenbaInfo(sse.GYOUSHA_CD, sse.GENBA_CD);
                                        dto.gyoushaCd = mapGenbaDto.GYOUSHA_CD;
                                        dto.gyoushaName = sse.GYOUSHA_NAME;     // マスタではなく伝票の業者名を抽出する
                                        dto.genbaCd = mapGenbaDto.GENBA_CD;
                                        dto.genbaName = sse.GENBA_NAME;         // マスタではなく伝票の現場名を抽出する
                                        dto.post = mapGenbaDto.POST;
                                        dto.address = mapGenbaDto.ADDRESS;
                                        dto.tel = mapGenbaDto.TEL;
                                        dto.bikou1 = mapGenbaDto.BIKOU1;
                                        dto.bikou2 = mapGenbaDto.BIKOU2;
                                        dto.latitude = mapGenbaDto.LATITUDE;
                                        dto.longitude = mapGenbaDto.LONGITUDE;

                                        dtos.Add(dto);
                                    }
                                    break;
                                    #endregion
                                case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                                    #region 出荷受付
                                    var ske = this.daoT_UKETSUKE_SK_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                    if (ske != null)
                                    {
                                        mapDto dto = new mapDto();
                                        dto.id = layerId;
                                        dto.layerNo = layerId;
                                        dto.courseName = string.Empty;
                                        dto.dayName = string.Empty;
                                        dto.teikiHaishaNo = this.NullToSpace(ske.UKETSUKE_NUMBER);
                                        dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                   + "　"
                                                   + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                        dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                   + "　"
                                                   + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                                        dto.dataShurui = "0";
                                        dto.dataKBN = "2";
                                        dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);
                                        if (string.IsNullOrEmpty(ske.GENCHAKU_TIME))
                                        {
                                            dto.genbaChaku = this.NullToSpace(ske.GENCHAKU_TIME_NAME);
                                        }
                                        else
                                        {
                                            dto.genbaChaku = this.NullToSpace(ske.GENCHAKU_TIME_NAME) + DateTime.Parse(ske.GENCHAKU_TIME).ToString("HH:mm");
                                        }
                                        MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                        MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(ske.NIZUMI_GYOUSHA_CD, ske.NIZUMI_GENBA_CD);
                                        dto.NNGyoushaName = this.NullToSpace(ske.NIZUMI_GYOUSHA_NAME);    // マスタではなく伝票の業者名を抽出する
                                        dto.NNGenbaName = this.NullToSpace(ske.NIZUMI_GENBA_NAME);        // マスタではなく伝票の現場名を抽出する
                                        dto.NNAddress = this.NullToSpace(mapGenbaDto.ADDRESS);

                                        // コンテナ稼働予定データを検索
                                        this.arrContenaReserve = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                                        // 設置台数を取得
                                        string setti = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI).Sum(r => (int)r.DAISUU_CNT));
                                        // 引揚台数を取得
                                        string hikiage = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE).Sum(r => (int)r.DAISUU_CNT));
                                        dto.contenaName = string.Format("設置{0}台　引揚{1}台", setti, hikiage);

                                        var skdArray = this.daoT_UKETSUKE_SK_DETAIL.GetData(this.dtoIdSeq);
                                        string hinmeiName = string.Empty;
                                        foreach (var skd in skdArray)
                                        {
                                            string suuryou = string.Empty;
                                            if (!string.IsNullOrEmpty(this.NullToSpace(skd.SUURYOU)))
                                            {
                                                suuryou = Convert.ToDecimal(skd.SUURYOU.Value).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                            }
                                            string unitName = this.getUnitName(Convert.ToString(skd.UNIT_CD));
                                            if (string.IsNullOrEmpty(hinmeiName))
                                            {
                                                hinmeiName += skd.HINMEI_NAME + " " + suuryou + unitName;
                                            }
                                            else
                                            {
                                                hinmeiName += "/" + skd.HINMEI_NAME + " " + suuryou + unitName;
                                            }
                                        }
                                        dto.hinmei = hinmeiName;

                                        mapLogic = new MapboxGLJSLogic();
                                        mapGenbaDto = mapLogic.mapGenbaInfo(ske.GYOUSHA_CD, ske.GENBA_CD);
                                        dto.gyoushaCd = mapGenbaDto.GYOUSHA_CD;
                                        dto.gyoushaName = ske.GYOUSHA_NAME;     // マスタではなく伝票の業者名を抽出する
                                        dto.genbaCd = mapGenbaDto.GENBA_CD;
                                        dto.genbaName = ske.GENBA_NAME;         // マスタではなく伝票の業者名を抽出する
                                        dto.post = mapGenbaDto.POST;
                                        dto.address = mapGenbaDto.ADDRESS;
                                        dto.tel = mapGenbaDto.TEL;
                                        dto.bikou1 = mapGenbaDto.BIKOU1;
                                        dto.bikou2 = mapGenbaDto.BIKOU2;
                                        dto.latitude = mapGenbaDto.LATITUDE;
                                        dto.longitude = mapGenbaDto.LONGITUDE;

                                        dtos.Add(dto);
                                    }
                                    break;
                                    #endregion
                                case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                                    #region 定期配車
                                    var tke = this.daoT_TEIKI_HAISHA_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                    if (tke != null)
                                    {
                                        mapDto dto = new mapDto();
                                        dto.id = layerId;
                                        dto.layerNo = layerId;
                                        dto.dataShurui = "0";
                                        dto.dayName = string.Empty;
                                        dto.teikiHaishaNo = this.NullToSpace(tke.TEIKI_HAISHA_NUMBER);
                                        dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                   + "　"
                                                   + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                        dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                   + "　"
                                                   + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                                        dto.dataShurui = "0";
                                        dto.dataKBN = "3";
                                        dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);
                                        string dTimeS = string.Empty;
                                        string dTimeE = string.Empty;
                                        if (!tke.SAGYOU_BEGIN_HOUR.IsNull && !tke.SAGYOU_BEGIN_MINUTE.IsNull)
                                        {
                                            dTimeS = DateTime.Parse(tke.SAGYOU_BEGIN_HOUR + ":" + tke.SAGYOU_BEGIN_MINUTE).ToString("HH:mm");
                                        }
                                        if (!tke.SAGYOU_END_HOUR.IsNull && !tke.SAGYOU_END_MINUTE.IsNull)
                                        {
                                            dTimeE = DateTime.Parse(tke.SAGYOU_END_HOUR + ":" + tke.SAGYOU_END_MINUTE).ToString("HH:mm");
                                        }
                                        if (string.IsNullOrEmpty(dTimeS) && string.IsNullOrEmpty(dTimeE))
                                        {
                                            dto.genbaChaku = string.Empty;
                                        }
                                        else
                                        {
                                            dto.genbaChaku = dTimeS + " ～ " + dTimeE;
                                        }

                                        M_COURSE_NAME courseNameDao = new M_COURSE_NAME();
                                        courseNameDao.COURSE_NAME_CD = tke.COURSE_NAME_CD;

                                        var cn = this.courseDao.GetAllValidData(courseNameDao);
                                        foreach (M_COURSE_NAME item in cn)
                                        {
                                            dto.courseName = item.COURSE_NAME_RYAKU;
                                        }


                                        var tkdArray = this.daoT_TEIKI_HAISHA_DETAIL.GetData(this.dtoIdSeq);

                                        // 開始行のみ抽出
                                        // 出発業者のみ、または出発業者と出発現場が設定されている場合は開始として表示
                                        // 出発業者、出発現場が設定されていない場合は、先頭の現場を表示
                                        var tkdmin = (from a in tkdArray orderby a.ROW_NUMBER select a).First();

                                        string gyoushaCd = string.Empty;
                                        string genbaCd = string.Empty;
                                        bool shuppatsuFlg = false;
                                        if (!string.IsNullOrEmpty(tke.SHUPPATSU_GYOUSHA_CD))
                                        {
                                            gyoushaCd = tke.SHUPPATSU_GYOUSHA_CD;
                                            genbaCd = tke.SHUPPATSU_GENBA_CD;
                                            shuppatsuFlg = true;
                                        }
                                        else
                                        {
                                            gyoushaCd = tkdmin.GYOUSHA_CD;
                                            genbaCd = tkdmin.GENBA_CD;
                                        }

                                        MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                        MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(gyoushaCd, genbaCd);
                                        dto.gyoushaCd = mapGenbaDto.GYOUSHA_CD;
                                        dto.gyoushaName = mapGenbaDto.GYOUSHA_NAME;
                                        dto.genbaCd = mapGenbaDto.GENBA_CD;
                                        dto.genbaName = mapGenbaDto.GENBA_NAME;
                                        dto.post = mapGenbaDto.POST;
                                        dto.address = mapGenbaDto.ADDRESS;
                                        dto.tel = mapGenbaDto.TEL;
                                        dto.bikou1 = mapGenbaDto.BIKOU1;
                                        dto.bikou2 = mapGenbaDto.BIKOU2;
                                        dto.latitude = mapGenbaDto.LATITUDE;
                                        dto.longitude = mapGenbaDto.LONGITUDE;

                                        // 出発業者、出発現場が設定されていない場合は、先頭現場の詳細情報を設定する。
                                        string hinmei = string.Empty;
                                        if (!shuppatsuFlg)
                                        {
                                            this.dtoIdSeqDetid.SystemId = Convert.ToInt64(tkdmin.SYSTEM_ID.ToString());
                                            this.dtoIdSeqDetid.Seq = Convert.ToInt32(tkdmin.SEQ.ToString());
                                            this.dtoIdSeqDetid.DetailSystemId = Convert.ToInt64(tkdmin.DETAIL_SYSTEM_ID.ToString());

                                            var tksminArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData2(this.dtoIdSeqDetid);
                                            foreach (T_TEIKI_HAISHA_SHOUSAI item in tksminArray)
                                            {
                                                M_HINMEI m_hinmei = new M_HINMEI();
                                                m_hinmei = daoM_HINMEI.GetDataByCode(item.HINMEI_CD);
                                                string unitName = this.getUnitName(Convert.ToString(item.UNIT_CD));
                                                if (string.IsNullOrEmpty(hinmei))
                                                    hinmei += m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                                else
                                                    hinmei += "/" + m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                            }
                                        }
                                        dto.hinmei = hinmei;

                                        // 最終行のみ抽出
                                        var tkdmax = (from a in tkdArray orderby a.ROW_NUMBER descending select a).First();
                                        mapLogic = new MapboxGLJSLogic();
                                        mapGenbaDto = mapLogic.mapGenbaInfo(tkdmax.GYOUSHA_CD, tkdmax.GENBA_CD);
                                        dto.gyoushaName_2 = mapGenbaDto.GYOUSHA_NAME;
                                        dto.genbaName_2 = mapGenbaDto.GENBA_NAME;
                                        dto.post_2 = mapGenbaDto.POST;
                                        dto.address_2 = mapGenbaDto.ADDRESS;
                                        dto.tel_2 = mapGenbaDto.TEL;
                                        dto.bikou1_2 = mapGenbaDto.BIKOU1;
                                        dto.bikou2_2 = mapGenbaDto.BIKOU2;
                                        dto.latitude_2 = mapGenbaDto.LATITUDE;
                                        dto.longitude_2 = mapGenbaDto.LONGITUDE;

                                        this.dtoIdSeqDetid.SystemId = Convert.ToInt64(tkdmax.SYSTEM_ID.ToString());
                                        this.dtoIdSeqDetid.Seq = Convert.ToInt32(tkdmax.SEQ.ToString());
                                        this.dtoIdSeqDetid.DetailSystemId = Convert.ToInt64(tkdmax.DETAIL_SYSTEM_ID.ToString());

                                        var tksmaxArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData2(this.dtoIdSeqDetid);
                                        hinmei = string.Empty;
                                        foreach (T_TEIKI_HAISHA_SHOUSAI item in tksmaxArray)
                                        {
                                            M_HINMEI m_hinmei = new M_HINMEI();
                                            m_hinmei = daoM_HINMEI.GetDataByCode(item.HINMEI_CD);
                                            string unitName = this.getUnitName(Convert.ToString(item.UNIT_CD));
                                            if (string.IsNullOrEmpty(hinmei))
                                                hinmei += m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                            else
                                                hinmei += "/" + m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                        }
                                        dto.hinmei_2 = hinmei;

                                        dtos.Add(dto);
                                    }
                                    break;
                                default:
                                    break;
                                    #endregion
                            }
                        }
                    }
                    // 1コース終わったらリストにセット
                    dtoList.dtos = dtos;
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }

                #endregion

                #region 未割当

                if (this.form.txtNum_MihaishaHyouzi.Text == "1")
                {
                    if (this.parentDataTable.Rows.Count > 0)
                    {
                        layerId = 0;
                        dtoList = new mapDtoList();
                        dtoList.layerId = layerId;
                        dtos = new List<mapDto>();

                        foreach (DataRow row in parentDataTable.Rows)
                        {
                            if (row["DATA_SHURUI"].ToString() != "1")
                            {
                                continue;
                            }
                            if (row["SYSTEM_ID"].ToString() != "0")
                            {
                                this.dtoIdSeq.SystemId = Convert.ToInt64(row["SYSTEM_ID"]);
                                this.dtoIdSeq.Seq = Convert.ToInt32(row["SEQ"]);

                                switch (Convert.ToInt16(row["SHUBETSU_KBN"]))
                                {
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                                        #region 収集受付
                                        var sse = this.daoT_UKETSUKE_SS_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                        if (sse != null)
                                        {
                                            // 抽出条件はここで引っ掛ける
                                            #region 収集受付抽出条件設定

                                            // 伝票種類指定
                                            if (!this.form.chk_Shushu.Checked) { break; }

                                            string value = string.Empty;

                                            // 作業日
                                            value = this.NullToSpace(sse.SAGYOU_DATE);
                                            if (this.form.txtNum_SagyoubiShitei.Text == "1")
                                            {
                                                if (!this.sagyouDateWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_SagyoubiShitei.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 拠点指定
                                            value = this.NullToSpace(sse.KYOTEN_CD);
                                            if (this.form.txtNum_KyotenShitei.Text == "1")
                                            {
                                                if (!this.kyotenWHERE(value)) { break; }
                                            }

                                            // 現着時間指定
                                            value = this.NullToSpace(sse.GENCHAKU_TIME_CD);
                                            if (this.form.txtNum_GenchakuShitei.Text == "1")
                                            {
                                                if (!this.genchakuWHERE(value)) { break; }
                                            }

                                            // 車種指定
                                            value = this.NullToSpace(sse.SHASHU_CD);
                                            if (this.form.txtNum_ShashuShitei.Text == "1")
                                            {
                                                if (!this.shashuWHERE(value)) { break; }
                                            }

                                            // 運搬業者指定
                                            value = this.NullToSpace(sse.UNPAN_GYOUSHA_CD);
                                            if (this.form.txtNum_UnpanGyousha.Text == "1")
                                            {
                                                if (this.unpanGyoushaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_UnpanGyousha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 運転者指定
                                            value = this.NullToSpace(sse.UNTENSHA_CD);
                                            if (this.form.txtNum_Untensha.Text == "1")
                                            {
                                                if (!this.untenshaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_Untensha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // コンテナ稼働予定データを検索
                                            this.arrContenaReserve = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                                            // 設置台数を取得
                                            string setti = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI).Sum(r => (int)r.DAISUU_CNT));
                                            // 引揚台数を取得
                                            string hikiage = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE).Sum(r => (int)r.DAISUU_CNT));

                                            // コンテナ作業指定
                                            if (this.form.txtNum_ContenaSagyouShitei.Text == "1")
                                            {
                                                // コンテナ種類指定
                                                if (this.form.txtNum_ContenaShurui.Text == "1")
                                                {
                                                    // コンテナ種類CD指定 ※コンテナ状況指定もこの中で
                                                    if (!this.contenaShuruiWHERE(this.NullToSpace(sse.CONTENA_SOUSA_CD))) { break; }
                                                }
                                                else
                                                {
                                                    // コンテナ状況指定
                                                    if (!this.contenaJoukyouWHERE(this.NullToSpace(sse.CONTENA_SOUSA_CD))) { break; }
                                                }
                                            }
                                            else if (this.form.txtNum_ContenaSagyouShitei.Text == "2")
                                            {
                                                bool bolContena = false;
                                                foreach (T_CONTENA_RESERVE item in this.arrContenaReserve)
                                                {
                                                    bolContena = true;
                                                }
                                                if (bolContena) { break; }
                                            }

                                            #endregion

                                            mapDto dto = new mapDto();
                                            dto.id = layerId;
                                            dto.layerNo = layerId;
                                            dto.courseName = string.Empty;
                                            dto.dayName = string.Empty;
                                            dto.teikiHaishaNo = this.NullToSpace(sse.UKETSUKE_NUMBER);
                                            dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                       + "　"
                                                       + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                            dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                       + "　"
                                                       + "【運転者】" + this.form.UNTENSHA_NAME.Text;
                                            dto.dataShurui = "1";
                                            dto.dataKBN = "1";
                                            dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);
                                            if (string.IsNullOrEmpty(sse.GENCHAKU_TIME))
                                            {
                                                dto.genbaChaku = this.NullToSpace(sse.GENCHAKU_TIME_NAME);
                                            }
                                            else
                                            {
                                                dto.genbaChaku = this.NullToSpace(sse.GENCHAKU_TIME_NAME) + DateTime.Parse(sse.GENCHAKU_TIME).ToString("HH:mm");
                                            }
                                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                            MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(sse.NIOROSHI_GYOUSHA_CD, sse.NIOROSHI_GENBA_CD);
                                            dto.NNGyoushaName = this.NullToSpace(sse.NIOROSHI_GYOUSHA_NAME);    // マスタではなく伝票の業者名を抽出する
                                            dto.NNGenbaName = this.NullToSpace(sse.NIOROSHI_GENBA_NAME);        // マスタではなく伝票の現場名を抽出する
                                            dto.NNAddress = this.NullToSpace(mapGenbaDto.ADDRESS);

                                            dto.contenaName = string.Format("設置{0}台　引揚{1}台", setti, hikiage);

                                            var sedArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                                            string hinmeiName = string.Empty;
                                            foreach (var sed in sedArray)
                                            {
                                                string suuryou = string.Empty;
                                                if (!string.IsNullOrEmpty(this.NullToSpace(sed.SUURYOU)))
                                                {
                                                    suuryou = Convert.ToDecimal(sed.SUURYOU.Value).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                                }
                                                string unitName = this.getUnitName(Convert.ToString(sed.UNIT_CD));
                                                if (string.IsNullOrEmpty(hinmeiName))
                                                {
                                                    hinmeiName += sed.HINMEI_NAME + " " + suuryou + unitName;
                                                }
                                                else
                                                {
                                                    hinmeiName += "/" + sed.HINMEI_NAME + " " + suuryou + unitName;
                                                }
                                            }
                                            dto.hinmei = hinmeiName;

                                            mapLogic = new MapboxGLJSLogic();
                                            mapGenbaDto = mapLogic.mapGenbaInfo(sse.GYOUSHA_CD, sse.GENBA_CD);
                                            dto.gyoushaCd = this.NullToSpace(mapGenbaDto.GYOUSHA_CD);
                                            dto.gyoushaName = this.NullToSpace(sse.GYOUSHA_NAME);     // マスタではなく伝票の業者名を抽出する
                                            dto.genbaCd = this.NullToSpace(mapGenbaDto.GENBA_CD);
                                            dto.genbaName = this.NullToSpace(sse.GENBA_NAME);         // マスタではなく伝票の現場名を抽出する
                                            dto.post = this.NullToSpace(mapGenbaDto.POST);
                                            dto.address = this.NullToSpace(mapGenbaDto.ADDRESS);
                                            dto.tel = this.NullToSpace(mapGenbaDto.TEL);
                                            dto.bikou1 = this.NullToSpace(mapGenbaDto.BIKOU1);
                                            dto.bikou2 = this.NullToSpace(mapGenbaDto.BIKOU2);
                                            dto.latitude = this.NullToSpace(mapGenbaDto.LATITUDE);
                                            dto.longitude = this.NullToSpace(mapGenbaDto.LONGITUDE);

                                            dtos.Add(dto);
                                        }
                                        break;
                                        #endregion
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                                        #region 出荷受付
                                        var ske = this.daoT_UKETSUKE_SK_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                        if (ske != null)
                                        {
                                            // 抽出条件はここで引っ掛ける
                                            #region 出荷受付抽出条件設定

                                            // 伝票種類指定
                                            if (!this.form.chk_Shushu.Checked) { break; }

                                            // 作業日
                                            string value = this.NullToSpace(ske.SAGYOU_DATE);
                                            if (this.form.txtNum_SagyoubiShitei.Text == "1")
                                            {
                                                if (!this.sagyouDateWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_SagyoubiShitei.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            value = string.Empty;
                                            // 拠点指定
                                            value = this.NullToSpace(ske.KYOTEN_CD);
                                            if (this.form.txtNum_KyotenShitei.Text == "1")
                                            {
                                                if (!this.kyotenWHERE(value)) { break; }
                                            }

                                            // 現着時間指定
                                            value = this.NullToSpace(ske.GENCHAKU_TIME_CD);
                                            if (this.form.txtNum_GenchakuShitei.Text == "1")
                                            {
                                                if (!this.genchakuWHERE(value)) { break; }
                                            }

                                            // 車種指定
                                            value = this.NullToSpace(ske.SHASHU_CD);
                                            if (this.form.txtNum_ShashuShitei.Text == "1")
                                            {
                                                if (!this.shashuWHERE(value)) { break; }
                                            }

                                            // 運搬業者指定
                                            value = this.NullToSpace(ske.UNPAN_GYOUSHA_CD);
                                            if (this.form.txtNum_UnpanGyousha.Text == "1")
                                            {
                                                if (this.unpanGyoushaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_UnpanGyousha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 運転者指定
                                            value = this.NullToSpace(ske.UNTENSHA_CD);
                                            if (this.form.txtNum_Untensha.Text == "1")
                                            {
                                                if (!this.untenshaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_Untensha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 出荷データにコンテナ情報はなし

                                            #endregion

                                            mapDto dto = new mapDto();
                                            dto.id = layerId;
                                            dto.layerNo = layerId;
                                            dto.courseName = string.Empty;
                                            dto.dayName = string.Empty;
                                            dto.teikiHaishaNo = this.NullToSpace(ske.UKETSUKE_NUMBER);
                                            dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                       + "　"
                                                       + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                            dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                       + "　"
                                                       + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                                            dto.dataShurui = "1";
                                            dto.dataKBN = "2";
                                            dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);
                                            if (string.IsNullOrEmpty(ske.GENCHAKU_TIME))
                                            {
                                                dto.genbaChaku = this.NullToSpace(ske.GENCHAKU_TIME_NAME);
                                            }
                                            else
                                            {
                                                dto.genbaChaku = this.NullToSpace(ske.GENCHAKU_TIME_NAME) + DateTime.Parse(ske.GENCHAKU_TIME).ToString("HH:mm");
                                            }

                                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                            MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(ske.NIZUMI_GYOUSHA_CD, ske.NIZUMI_GENBA_CD);
                                            dto.NNGyoushaName = this.NullToSpace(ske.NIZUMI_GYOUSHA_NAME);    // マスタではなく伝票の業者名を抽出する
                                            dto.NNGenbaName = this.NullToSpace(ske.NIZUMI_GENBA_NAME);        // マスタではなく伝票の現場名を抽出する
                                            dto.NNAddress = this.NullToSpace(mapGenbaDto.ADDRESS);

                                            // コンテナ稼働予定データを検索
                                            this.arrContenaReserve = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                                            // 設置台数を取得
                                            string setti = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_SECCHI).Sum(r => (int)r.DAISUU_CNT));
                                            // 引揚台数を取得
                                            string hikiage = Convert.ToString(this.arrContenaReserve.Where(r => (int)r.CONTENA_SET_KBN == CommonConst.CONTENA_SET_KBN_HIKIAGE).Sum(r => (int)r.DAISUU_CNT));
                                            dto.contenaName = string.Format("設置{0}台　引揚{1}台", setti, hikiage);

                                            var skdArray = this.daoT_UKETSUKE_SK_DETAIL.GetData(this.dtoIdSeq);
                                            string hinmeiName = string.Empty;
                                            foreach (var skd in skdArray)
                                            {
                                                string suuryou = string.Empty;
                                                if (!string.IsNullOrEmpty(this.NullToSpace(skd.SUURYOU)))
                                                {
                                                    suuryou = Convert.ToDecimal(skd.SUURYOU.Value).ToString(this.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                                }
                                                string unitName = this.getUnitName(Convert.ToString(skd.UNIT_CD));
                                                if (string.IsNullOrEmpty(hinmeiName))
                                                {
                                                    hinmeiName += skd.HINMEI_NAME + " " + suuryou + unitName;
                                                }
                                                else
                                                {
                                                    hinmeiName += "/" + skd.HINMEI_NAME + " " + suuryou + unitName;
                                                }
                                            }
                                            dto.hinmei = hinmeiName;

                                            mapLogic = new MapboxGLJSLogic();
                                            mapGenbaDto = mapLogic.mapGenbaInfo(ske.GYOUSHA_CD, ske.GENBA_CD);
                                            dto.gyoushaCd = this.NullToSpace(mapGenbaDto.GYOUSHA_CD);
                                            dto.gyoushaName = this.NullToSpace(ske.GYOUSHA_NAME);         // マスタではなく伝票の業者名を抽出する
                                            dto.genbaCd = this.NullToSpace(mapGenbaDto.GENBA_CD);
                                            dto.genbaName = this.NullToSpace(ske.GENBA_NAME);             // マスタではなく伝票の業者名を抽出する
                                            dto.post = this.NullToSpace(mapGenbaDto.POST);
                                            dto.address = this.NullToSpace(mapGenbaDto.ADDRESS);
                                            dto.tel = this.NullToSpace(mapGenbaDto.TEL);
                                            dto.bikou1 = this.NullToSpace(mapGenbaDto.BIKOU1);
                                            dto.bikou2 = this.NullToSpace(mapGenbaDto.BIKOU2);
                                            dto.latitude = this.NullToSpace(mapGenbaDto.LATITUDE);
                                            dto.longitude = this.NullToSpace(mapGenbaDto.LONGITUDE);

                                            dtos.Add(dto);
                                        }
                                        break;
                                        #endregion
                                    case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                                        #region 定期配車
                                        var tke = this.daoT_TEIKI_HAISHA_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                                        if (tke != null)
                                        {
                                            // 抽出条件はここで引っ掛ける
                                            #region 定期配車抽出条件設定

                                            // 伝票種類指定
                                            if (!this.form.chk_Teiki.Checked) { break; }

                                            string value = string.Empty;

                                            // 作業日
                                            // 定期は作業日が必須項目のため抽出条件にしない

                                            // 拠点指定
                                            value = this.NullToSpace(tke.KYOTEN_CD);
                                            if (this.form.txtNum_KyotenShitei.Text == "1")
                                            {
                                                if (!this.kyotenWHERE(value)) { break; }
                                            }

                                            // 現着時間指定(定期はなし)

                                            // 車種指定
                                            value = this.NullToSpace(tke.SHASHU_CD);
                                            if (this.form.txtNum_ShashuShitei.Text == "1")
                                            {
                                                if (!this.genchakuWHERE(value)) { break; }
                                            }

                                            // 運搬業者指定
                                            value = this.NullToSpace(tke.UNPAN_GYOUSHA_CD);
                                            if (this.form.txtNum_UnpanGyousha.Text == "1")
                                            {
                                                if (this.unpanGyoushaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_UnpanGyousha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 運転者指定
                                            value = this.NullToSpace(tke.UNTENSHA_CD);
                                            if (this.form.txtNum_Untensha.Text == "1")
                                            {
                                                if (!this.untenshaWHERE(value)) { break; }
                                            }
                                            else if (this.form.txtNum_Untensha.Text == "2")
                                            {
                                                if (!string.IsNullOrEmpty(value)) { break; }
                                            }

                                            // 定期データにコンテナ情報はなし

                                            #endregion

                                            mapDto dto = new mapDto();
                                            dto.id = layerId;
                                            dto.layerNo = layerId;
                                            dto.dayName = string.Empty;
                                            dto.teikiHaishaNo = this.NullToSpace(tke.TEIKI_HAISHA_NUMBER);
                                            dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                                       + "　"
                                                       + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                                            dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                                       + "　"
                                                       + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                                            dto.dataShurui = "1";
                                            dto.dataKBN = "3";
                                            dto.rowNo = Convert.ToInt32(row["COLUMN_NUMBER"]);

                                            string dTimeS = string.Empty;
                                            string dTimeE = string.Empty;
                                            if (!tke.SAGYOU_BEGIN_HOUR.IsNull && !tke.SAGYOU_BEGIN_MINUTE.IsNull)
                                            {
                                                dTimeS = DateTime.Parse(tke.SAGYOU_BEGIN_HOUR + ":" + tke.SAGYOU_BEGIN_MINUTE).ToString("HH:mm");
                                            }
                                            if (!tke.SAGYOU_END_HOUR.IsNull && !tke.SAGYOU_END_MINUTE.IsNull)
                                            {
                                                dTimeE = DateTime.Parse(tke.SAGYOU_END_HOUR + ":" + tke.SAGYOU_END_MINUTE).ToString("HH:mm");
                                            }
                                            if (string.IsNullOrEmpty(dTimeS) && string.IsNullOrEmpty(dTimeE))
                                            {
                                                dto.genbaChaku = string.Empty;
                                            }
                                            else
                                            {
                                                dto.genbaChaku = dTimeS + " ～ " + dTimeE;
                                            }

                                            M_COURSE_NAME courseNameDao = new M_COURSE_NAME();
                                            courseNameDao.COURSE_NAME_CD = tke.COURSE_NAME_CD;

                                            var cn = this.courseDao.GetAllValidData(courseNameDao);
                                            foreach (M_COURSE_NAME item in cn)
                                            {
                                                dto.courseName = item.COURSE_NAME_RYAKU;
                                            }

                                            var tkdArray = this.daoT_TEIKI_HAISHA_DETAIL.GetData(this.dtoIdSeq);

                                            // 開始行のみ抽出
                                            // 出発業者のみ、または出発業者と出発現場が設定されている場合は開始として表示
                                            // 出発業者、出発現場が設定されていない場合は、先頭の現場を表示
                                            var tkdmin = (from a in tkdArray orderby a.ROW_NUMBER select a).First();

                                            string gyoushaCd = string.Empty;
                                            string genbaCd = string.Empty;
                                            bool shuppatsuFlg = false;
                                            if (!string.IsNullOrEmpty(tke.SHUPPATSU_GYOUSHA_CD))
                                            {
                                                gyoushaCd = tke.SHUPPATSU_GYOUSHA_CD;
                                                genbaCd = tke.SHUPPATSU_GENBA_CD;
                                                shuppatsuFlg = true;
                                            }
                                            else
                                            {
                                                gyoushaCd = tkdmin.GYOUSHA_CD;
                                                genbaCd = tkdmin.GENBA_CD;
                                            }

                                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                            MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(gyoushaCd, genbaCd);
                                            dto.gyoushaCd = mapGenbaDto.GYOUSHA_CD;
                                            dto.gyoushaName = mapGenbaDto.GYOUSHA_NAME;
                                            dto.genbaCd = mapGenbaDto.GENBA_CD;
                                            dto.genbaName = mapGenbaDto.GENBA_NAME;
                                            dto.post = mapGenbaDto.POST;
                                            dto.address = mapGenbaDto.ADDRESS;
                                            dto.tel = mapGenbaDto.TEL;
                                            dto.bikou1 = mapGenbaDto.BIKOU1;
                                            dto.bikou2 = mapGenbaDto.BIKOU2;
                                            dto.latitude = mapGenbaDto.LATITUDE;
                                            dto.longitude = mapGenbaDto.LONGITUDE;

                                            // 出発業者、出発現場が設定されていない場合は、先頭現場の詳細情報を設定する。
                                            string hinmei = string.Empty;
                                            if (!shuppatsuFlg)
                                            {
                                                this.dtoIdSeqDetid.SystemId = Convert.ToInt64(tkdmin.SYSTEM_ID.ToString());
                                                this.dtoIdSeqDetid.Seq = Convert.ToInt32(tkdmin.SEQ.ToString());
                                                this.dtoIdSeqDetid.DetailSystemId = Convert.ToInt64(tkdmin.DETAIL_SYSTEM_ID.ToString());

                                                var tksminArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData2(this.dtoIdSeqDetid);
                                                foreach (T_TEIKI_HAISHA_SHOUSAI item in tksminArray)
                                                {
                                                    M_HINMEI m_hinmei = new M_HINMEI();
                                                    m_hinmei = daoM_HINMEI.GetDataByCode(item.HINMEI_CD);
                                                    string unitName = this.getUnitName(Convert.ToString(item.UNIT_CD));
                                                    if (string.IsNullOrEmpty(hinmei))
                                                        hinmei += m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                                    else
                                                        hinmei += "/" + m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                                }
                                            }
                                            dto.hinmei = hinmei;

                                            // 最終行のみ抽出
                                            var tkdmax = (from a in tkdArray orderby a.ROW_NUMBER descending select a).First();
                                            mapLogic = new MapboxGLJSLogic();
                                            mapGenbaDto = mapLogic.mapGenbaInfo(tkdmax.GYOUSHA_CD, tkdmax.GENBA_CD);
                                            dto.gyoushaName_2 = mapGenbaDto.GYOUSHA_NAME;
                                            dto.genbaName_2 = mapGenbaDto.GENBA_NAME;
                                            dto.post_2 = mapGenbaDto.POST;
                                            dto.address_2 = mapGenbaDto.ADDRESS;
                                            dto.tel_2 = mapGenbaDto.TEL;
                                            dto.bikou1_2 = mapGenbaDto.BIKOU1;
                                            dto.bikou2_2 = mapGenbaDto.BIKOU2;
                                            dto.latitude_2 = mapGenbaDto.LATITUDE;
                                            dto.longitude_2 = mapGenbaDto.LONGITUDE;

                                            this.dtoIdSeqDetid.SystemId = Convert.ToInt64(tkdmax.SYSTEM_ID.ToString());
                                            this.dtoIdSeqDetid.Seq = Convert.ToInt32(tkdmax.SEQ.ToString());
                                            this.dtoIdSeqDetid.DetailSystemId = Convert.ToInt64(tkdmax.DETAIL_SYSTEM_ID.ToString());

                                            var tksmaxArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData2(this.dtoIdSeqDetid);
                                            hinmei = string.Empty;
                                            foreach (T_TEIKI_HAISHA_SHOUSAI item in tksmaxArray)
                                            {
                                                M_HINMEI m_hinmei = new M_HINMEI();
                                                m_hinmei = daoM_HINMEI.GetDataByCode(item.HINMEI_CD);
                                                string unitName = this.getUnitName(Convert.ToString(item.UNIT_CD));
                                                if (string.IsNullOrEmpty(hinmei))
                                                    hinmei += m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                                else
                                                    hinmei += "/" + m_hinmei.HINMEI_NAME_RYAKU + " " + unitName;
                                            }
                                            dto.hinmei_2 = hinmei;

                                            dtos.Add(dto);
                                        }
                                        break;
                                    default:
                                        break;
                                        #endregion
                                }
                            }
                        }

                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }

                #endregion

                #region 設置コンテナ(個体管理ベースの抽出のみ行う)

                if (this.form.txtNum_SecchiContena.Text == "1")
                {
                    // 検索条件を設定する
                    SetSearchString();

                    // 検索結果を取得する
                    int kontenaKanriHouhou = this.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                        ? CommonConst.CONTENA_KANRI_HOUHOU_SUURYOU : (int)this.sysInfoEntity.CONTENA_KANRI_HOUHOU;


                    // 実績データ(収集受付、受入、売上支払)から取得
                    var resulutList = this.mapDao.GetIchiranJissekiDataSql(this.SearchString);

                    if (resulutList != null
                        && resulutList.Count > 0)
                    {
                        // 引揚がされていないデータを抽出
                        var genbas = resulutList.AsEnumerable().Select(s => new { s.CONTENA_SHURUI_CD, s.CONTENA_CD, s.GYOUSHA_CD, s.GENBA_CD })
                            .GroupBy(g => new { g.CONTENA_SHURUI_CD, g.CONTENA_CD, g.GYOUSHA_CD, g.GENBA_CD });

                        foreach (var genba in genbas)
                        {
                            var rows = resulutList.AsEnumerable()
                                .Where(w => w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                    && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                    && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD)
                                    && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                    && w.CONTENA_SET_KBN == 2).ToArray();

                            foreach (var row in rows)
                            {
                                var secchiRow = resulutList.AsEnumerable().Where(w => w.SECCHI_DATE != null
                                    && Convert.ToDateTime(w.SECCHI_DATE) <= (Convert.ToDateTime(row.SECCHI_DATE))
                                    && w.CONTENA_SHURUI_CD.Equals(genba.Key.CONTENA_SHURUI_CD)
                                    && w.CONTENA_CD.Equals(genba.Key.CONTENA_CD)
                                    && w.GYOUSHA_CD.Equals(genba.Key.GYOUSHA_CD.ToString())
                                    && w.GENBA_CD.Equals(genba.Key.GENBA_CD)
                                    && w.CONTENA_SET_KBN == 1)
                                    .OrderByDescending(o => o.SECCHI_DATE).ToArray();

                                // 設置 -> 引揚の操作のセットがあった場合はリストから除外
                                if (secchiRow != null && secchiRow.Count() > 0)
                                {
                                    // 設置分
                                    resulutList.Remove(secchiRow.First());
                                }

                                // 引揚分を除外
                                resulutList.Remove(row);
                            }

                        }
                    }

                    DataTable displayData = new DataTable();
                    for (int i = 0; i < this.columnNamesForKotaiknri.Length; i++)
                    {
                        displayData.Columns.Add(columnNamesForKotaiknri[i].ToString(), System.Type.GetType(columnTyepesForKotaiKanri[i]));
                    }
                    foreach (SearchResult data in resulutList)
                    {
                        DataRow row = displayData.NewRow();
                        row[COLUMN_NAME_SECCHICHOUUHUKU] = data.SecchiChouhuku;
                        row[COLUMN_NAME_CONTENA_SHURUI_CD] = data.CONTENA_SHURUI_CD;
                        row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU] = data.CONTENA_SHURUI_NAME_RYAKU;
                        row[COLUMN_NAME_CONTENA_CD] = data.CONTENA_CD;
                        row[COLUMN_NAME_CONTENA_NAME_RYAKU] = data.CONTENA_NAME_RYAKU;
                        row[COLUMN_NAME_GYOUSHA_CD] = data.GYOUSHA_CD;
                        row[COLUMN_NAME_GYOUSHA_NAME_RYAKU] = data.GYOUSHA_NAME_RYAKU;
                        row[COLUMN_NAME_GENBA_CD] = data.GENBA_CD;
                        row[COLUMN_NAME_GENBA_NAME_RYAKU] = data.GENBA_NAME_RYAKU;
                        row[COLUMN_NAME_EIGYOU_TANTOU_CD] = data.EIGYOU_TANTOU_CD;
                        row[COLUMN_NAME_SHAIN_NAME_RYAKU] = data.SHAIN_NAME_RYAKU;
                        row[COLUMN_NAME_SECCHI_DATE] = data.SECCHI_DATE;
                        row[COLUMN_NAME_DAYSCOUNT] = data.DAYSCOUNT;
                        displayData.Rows.Add(row);
                    }

                    this.SearchResult = displayData;

                    this.SearchResult.DefaultView.RowFilter = string.Format("{0} >= '{1}'", COLUMN_NAME_DAYSCOUNT, this.SearchString.ELAPSED_DAYS);
                    // [F11]フィルタ機能でRowFilterが上書かれるため、その対策
                    this.SearchResult = this.SearchResult.DefaultView.ToTable();

                    foreach (DataRow row in this.SearchResult.Rows)
                    {
                        var filterRow = this.SearchResult.AsEnumerable().Where(w => w[COLUMN_NAME_CONTENA_SHURUI_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_SHURUI_CD].ToString())
                                                                                    && w[COLUMN_NAME_CONTENA_CD].ToString().Equals(row[COLUMN_NAME_CONTENA_CD].ToString()));

                        if (filterRow != null && filterRow.Count() > 1)
                        {
                            foreach (DataRow tempRow in filterRow)
                            {
                                tempRow[COLUMN_NAME_SECCHICHOUUHUKU] = this.CHOUHUKU_SECCHI_VALUE;
                            }
                        }
                    }

                    if (this.SearchResult.Rows.Count != 0)
                    {
                        layerId = 1;
                    }
                    foreach (DataRow row in this.SearchResult.Rows)
                    {
                        #region 設置コンテナ一覧抽出条件指定

                        // コンテナ種類指定
                        if (this.form.txtNum_ContenaShurui2.Text == "1")
                        {
                            if (!this.contenaShurui2WHERE(row[COLUMN_NAME_CONTENA_SHURUI_CD].ToString())) { continue; }
                        }

                        // 設置期間指定
                        if (this.form.txtNum_SecchiKikan.Text == "1")
                        {
                            if (!string.IsNullOrEmpty(this.form.txtNum_SecchiFrom.Text) &&
                                !string.IsNullOrEmpty(this.form.txtNum_SecchiTo.Text))
                            {
                                if (Convert.ToInt32(this.form.txtNum_SecchiFrom.Text) > Convert.ToInt32(row[COLUMN_NAME_DAYSCOUNT]) ||
                                    Convert.ToInt32(this.form.txtNum_SecchiTo.Text) < Convert.ToInt32(row[COLUMN_NAME_DAYSCOUNT]))
                                {
                                    continue;
                                }
                            }
                            else if (!string.IsNullOrEmpty(this.form.txtNum_SecchiFrom.Text) &&
                                      string.IsNullOrEmpty(this.form.txtNum_SecchiTo.Text))
                            {
                                if (Convert.ToInt32(this.form.txtNum_SecchiFrom.Text) > Convert.ToInt32(row[COLUMN_NAME_DAYSCOUNT]))
                                {
                                    continue;
                                }
                            }
                            else if (string.IsNullOrEmpty(this.form.txtNum_SecchiFrom.Text) &&
                                    !string.IsNullOrEmpty(this.form.txtNum_SecchiTo.Text))
                            {
                                if (Convert.ToInt32(this.form.txtNum_SecchiTo.Text) < Convert.ToInt32(row[COLUMN_NAME_DAYSCOUNT]))
                                {
                                    continue;
                                }
                            }
                        }

                        // 重複設置絞込
                        if (this.form.txtNum_ChouhukuSecchiNomi.Text == "1")
                        {
                            if (Convert.ToString(row[COLUMN_NAME_SECCHICHOUUHUKU]) != CHOUHUKU_SECCHI_VALUE)
                            {
                                continue;
                            }
                        }

                        #endregion

                        layerId++;
                        dtoList = new mapDtoList();
                        dtoList.layerId = layerId;
                        dtos = new List<mapDto>();

                        mapDto dto = new mapDto();
                        dto.id = layerId;
                        dto.layerNo = layerId;
                        dto.dataShurui = "2";
                        dto.courseName = string.Empty;
                        dto.dayName = string.Empty;
                        dto.teikiHaishaNo = string.Empty;
                        dto.header = "【作業日】" + this.form.SAGYOU_DATE.Text
                                   + "　"
                                   + "【運搬業者】" + this.form.UNPAN_GYOUSHA_NAME.Text;
                        dto.header2 = "【車輛】" + this.form.SHARYOU_NAME_RYAKU.Text
                                   + "　"
                                   + "【運転者】" + this.form.UNTENSHA_NAME.Text;

                        dto.contenaShuruiName = Convert.ToString(row[COLUMN_NAME_CONTENA_SHURUI_NAME_RYAKU]);
                        dto.contenaName = Convert.ToString(row[COLUMN_NAME_CONTENA_NAME_RYAKU]);
                        dto.daysCount = Convert.ToString(row[COLUMN_NAME_DAYSCOUNT]);
                        dto.rowNo = Convert.ToInt32(row[COLUMN_NAME_DAYSCOUNT]);
                        dto.secchiChouhuku = Convert.ToString(row[COLUMN_NAME_SECCHICHOUUHUKU]);

                        MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                        MAP_GENBA_DTO mapGenbaDto = mapLogic.mapGenbaInfo(Convert.ToString(row[COLUMN_NAME_GYOUSHA_CD]), Convert.ToString(row[COLUMN_NAME_GENBA_CD]));
                        dto.gyoushaCd = mapGenbaDto.GYOUSHA_CD;
                        dto.gyoushaName = mapGenbaDto.GYOUSHA_NAME;
                        dto.genbaCd = mapGenbaDto.GENBA_CD;
                        dto.genbaName = mapGenbaDto.GENBA_NAME;
                        dto.post = mapGenbaDto.POST;
                        dto.address = mapGenbaDto.ADDRESS;
                        dto.tel = mapGenbaDto.TEL;
                        dto.bikou1 = mapGenbaDto.BIKOU1;
                        dto.bikou2 = mapGenbaDto.BIKOU2;
                        dto.latitude = mapGenbaDto.LATITUDE;
                        dto.longitude = mapGenbaDto.LONGITUDE;

                        dtos.Add(dto);

                        // 1件ずつアイコンが異なるケースもあるため毎回追加
                        dtoList.dtos = dtos;
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }

                #endregion

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.form.msgLogic.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DTOCls searchCondition = new DTOCls();

                // 作業日
                searchCondition.SAGYOU_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd");

                this.SearchString = searchCondition;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region 抽出条件指定

        /// <summary>
        /// 作業日抽出条件指定
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        private bool sagyouDateWHERE(string sagyouDate)
        {
            bool ret = false;
            if (sagyouDate == Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString()) { ret = true; }
            return ret;
        }

        /// <summary>
        /// 拠点抽出条件指定
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        private bool kyotenWHERE(string kyotenCd)
        {
            bool ret = false;

            foreach (DataGridViewRow r in this.form.dgvKyoten.Rows)
            {
                if (kyotenCd == Convert.ToString(r.Cells["dgv_KYOTEN_CD"].Value))
                    if (Convert.ToBoolean(r.Cells["CHECKBOX"].Value)) { ret = true; }
            }
            return ret;
        }

        private bool genchakuWHERE(string genchakuCd)
        {
            bool ret = false;
            foreach (DataGridViewRow r in this.form.dgvGenchaku.Rows)
            {
                if (genchakuCd == Convert.ToString(r.Cells["dgv_GENCHAKU_TIME_CD"].Value))
                    if (Convert.ToBoolean(r.Cells["CHECKBOX2"].Value)) { ret = true; }
            }

            if (this.form.chk_Genchaku.Checked)
                if (string.IsNullOrEmpty(genchakuCd)) { ret = true; }

            return ret;
        }

        private bool shashuWHERE(string shashuCd)
        {
            bool ret = false;
            foreach (DataGridViewRow r in this.form.dgvShashu.Rows)
            {
                if (shashuCd == Convert.ToString(r.Cells["dgv_SHASHU_CD"].Value))
                    if (Convert.ToBoolean(r.Cells["CHECKBOX3"].Value)) { ret = true; }
            }

            if (this.form.chk_Shashu.Checked)
                if (string.IsNullOrEmpty(shashuCd)) { ret = true; }

            return ret;
        }

        private bool unpanGyoushaWHERE(string unpanGyoushaCd)
        {
            bool ret = false;
            if (unpanGyoushaCd == this.form.UNPAN_GYOUSHA_CD.Text) { ret = true; }

            if (this.form.chk_UnpanGyousha.Checked)
                if (string.IsNullOrEmpty(unpanGyoushaCd)) { ret = true; }

            return ret;
        }

        private bool untenshaWHERE(string untenshaCd)
        {
            bool ret = false;
            if (untenshaCd == this.form.UNTENSHA_CD.Text) { ret = true; }
            if (this.form.chk_Untensha.Checked)
                if (string.IsNullOrEmpty(untenshaCd)) { ret = true; }

            return ret;
        }

        private bool contenaJoukyouWHERE(string contenaSousaCd)
        {
            bool ret = false;

            // 設置
            if (this.form.chk_Secchi.Checked)
                if (contenaSousaCd == "2") { ret = true; }

            // 引揚
            if (this.form.chk_Hikiage.Checked)
                if (contenaSousaCd == "3") { ret = true; }

            // 交換
            if (this.form.chk_Koukan.Checked)
                if (contenaSousaCd == "1") { ret = true; }

            return ret;
        }

        private bool contenaShuruiWHERE(string contenaSousaCd)
        {
            bool ret = false;

            // コンテナ種類をループさせる
            foreach (T_CONTENA_RESERVE item in this.arrContenaReserve)
            {

                foreach (DataGridViewRow r in this.form.dgvContena.Rows)
                {
                    if (item.CONTENA_SHURUI_CD == Convert.ToString(r.Cells["dgv_CONTENA_SHURUI_CD"].Value))
                    {
                        // 種類が一致したら状況のチェックを行う
                        if (Convert.ToBoolean(r.Cells["CHECKBOX4"].Value))
                        {
                            // 設置
                            if (this.form.chk_Secchi.Checked)
                            {
                                if (item.CONTENA_SET_KBN == 1 && contenaSousaCd == "2")
                                {
                                    ret = true;
                                }
                            }
                            // 引揚
                            if (this.form.chk_Hikiage.Checked)
                            {
                                if (item.CONTENA_SET_KBN == 2 && contenaSousaCd == "3")
                                {
                                    ret = true;
                                }
                            }
                            // 交換
                            // ※引揚設置するコンテナ種類のどちらか一方でもリスト内で選択されていればOK、という判定
                            if (this.form.chk_Koukan.Checked && contenaSousaCd == "1")
                            {
                                if (item.CONTENA_SET_KBN == 1)
                                {
                                    ret = true;
                                }
                                if (item.CONTENA_SET_KBN == 2)
                                {
                                    ret = true;
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private bool contenaShurui2WHERE(string contenaShuruiCd)
        {
            bool ret = false;
            foreach (DataGridViewRow r in this.form.dgvContena2.Rows)
            {
                if (contenaShuruiCd == Convert.ToString(r.Cells["dgv_CONTENA_SHURUI_CD2"].Value))
                    if (Convert.ToBoolean(r.Cells["CHECKBOX5"].Value)) { ret = true; }
            }
            return ret;
        }

        #endregion

        #endregion

        #region イベント

        /// <summary>
        /// 未配車表示変更イベント
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal bool txtNum_MihaishaHyouzi_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_MihaishaHyouzi.Text == "1")
            {
                // 同タブ内の項目を使用可能に

                this.form.txtNum_SagyoubiShitei.Enabled = true;
                this.form.customPanel14.Enabled = true;

                this.form.txtNum_KyotenShitei.Enabled = true;
                this.form.customPanel2.Enabled = true;
                this.form.dgvKyoten.Enabled = true;
                this.txtNum_KyotenShitei_TextChanged();

                this.form.txtNum_GenchakuShitei.Enabled = true;
                this.form.customPanel3.Enabled = true;
                this.form.chk_Genchaku.Enabled = true;
                this.form.dgvGenchaku.Enabled = true;
                this.txtNum_GenchakuShitei_TextChanged();

                this.form.txtNum_ShashuShitei.Enabled = true;
                this.form.customPanel4.Enabled = true;
                this.form.chk_Shashu.Enabled = true;
                this.form.dgvShashu.Enabled = true;
                this.txtNum_ShashuShitei_TextChanged();

                this.form.txtNum_UnpanGyousha.Enabled = true;
                this.form.customPanel5.Enabled = true;
                this.form.chk_UnpanGyousha.Enabled = true;
                this.txtNum_UnpanGyousha_TextChanged();

                this.form.txtNum_Untensha.Enabled = true;
                this.form.customPanel8.Enabled = true;
                this.form.chk_Untensha.Enabled = true;
                this.txtNum_Untensha_TextChanged();

                this.form.customPanel7.Enabled = true;

                this.form.txtNum_ContenaShurui.Enabled = true;
                this.form.customPanel9.Enabled = true;
                this.form.dgvContena.Enabled = true;
                this.txtNum_ContenaShurui_TextChanged();

                this.form.customPanel13.Enabled = true;
                this.chk_DenpyouShurui_CheckedChanged();

                this.form.txtNum_ContenaSagyouShitei.Enabled = true;
                this.form.customPanel6.Enabled = true;
                this.txtNum_ContenaSagyouShitei_TextChanged();
            }
            else
            {
                // 同タブ内の項目を使用不可に
                this.form.customPanel13.Enabled = false;

                this.form.txtNum_SagyoubiShitei.Enabled = false;
                this.form.customPanel14.Enabled = false;

                this.form.txtNum_KyotenShitei.Enabled = false;
                this.form.customPanel2.Enabled = false;
                this.form.dgvKyoten.Enabled = false;

                this.form.txtNum_GenchakuShitei.Enabled = false;
                this.form.customPanel3.Enabled = false;
                this.form.chk_Genchaku.Enabled = false;
                this.form.dgvGenchaku.Enabled = false;

                this.form.txtNum_ShashuShitei.Enabled = false;
                this.form.customPanel4.Enabled = false;
                this.form.chk_Shashu.Enabled = false;
                this.form.dgvShashu.Enabled = false;

                this.form.txtNum_UnpanGyousha.Enabled = false;
                this.form.customPanel5.Enabled = false;
                this.form.chk_UnpanGyousha.Enabled = false;

                this.form.txtNum_Untensha.Enabled = false;
                this.form.customPanel8.Enabled = false;
                this.form.chk_Untensha.Enabled = false;

                this.form.txtNum_ContenaSagyouShitei.Enabled = false;
                this.form.customPanel6.Enabled = false;

                this.form.customPanel7.Enabled = false;

                this.form.txtNum_ContenaShurui.Enabled = false;
                this.form.customPanel9.Enabled = false;
                this.form.dgvContena.Enabled = false;


            }

            return ret;
        }

        /// <summary>
        /// 伝票種類指定変更イベント
        /// </summary>
        /// <returns></returns>
        internal void chk_DenpyouShurui_CheckedChanged()
        {

            if (!this.form.chk_Shushu.Checked &&
                !this.form.chk_Shukka.Checked &&
                this.form.chk_Teiki.Checked)
            {
                this.form.txtNum_SagyoubiShitei.Enabled = false;
                this.form.customPanel14.Enabled = false;
                this.form.txtNum_GenchakuShitei.Enabled = false;
                this.form.customPanel3.Enabled = false;
                this.form.chk_Genchaku.Enabled = false;
                this.form.dgvGenchaku.Enabled = false;

                this.form.txtNum_ContenaSagyouShitei.Enabled = false;
                this.form.customPanel6.Enabled = false;

                this.form.customPanel7.Enabled = false;

                this.form.txtNum_ContenaShurui.Enabled = false;
                this.form.customPanel9.Enabled = false;
                this.form.dgvContena.Enabled = false;
                this.txtNum_ContenaShurui_TextChanged();
            }
            else
            {
                this.form.txtNum_SagyoubiShitei.Enabled = true;
                this.form.customPanel14.Enabled = true;

                this.form.txtNum_GenchakuShitei.Enabled = true;
                this.form.customPanel3.Enabled = true;
                this.form.chk_Genchaku.Enabled = true;
                this.form.dgvGenchaku.Enabled = true;
                this.txtNum_GenchakuShitei_TextChanged();

                this.form.txtNum_ContenaSagyouShitei.Enabled = true;
                this.form.customPanel6.Enabled = true;
                this.txtNum_ContenaSagyouShitei_TextChanged();
            }
        }

        /// <summary>
        /// 拠点指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_KyotenShitei_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_KyotenShitei.Text == "1")
            {
                this.form.dgvKyoten.Enabled = true;
            }
            else
            {
                this.form.dgvKyoten.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 現着時間指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_GenchakuShitei_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_GenchakuShitei.Text == "1")
            {
                this.form.chk_Genchaku.Enabled = true;
                this.form.dgvGenchaku.Enabled = true;
            }
            else
            {
                this.form.chk_Genchaku.Enabled = false;
                this.form.dgvGenchaku.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 車種指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_ShashuShitei_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_ShashuShitei.Text == "1")
            {
                this.form.chk_Shashu.Enabled = true;
                this.form.dgvShashu.Enabled = true;
            }
            else
            {
                this.form.chk_Shashu.Enabled = false;
                this.form.dgvShashu.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 運搬業者指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_UnpanGyousha_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_UnpanGyousha.Text == "1")
            {
                this.form.chk_UnpanGyousha.Enabled = true;
            }
            else
            {
                this.form.chk_UnpanGyousha.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 運転者指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_Untensha_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_Untensha.Text == "1")
            {
                this.form.chk_Untensha.Enabled = true;
            }
            else
            {
                this.form.chk_Untensha.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// コンテナ作業指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_ContenaSagyouShitei_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_ContenaSagyouShitei.Text == "1")
            {
                this.form.customPanel9.Enabled = true;
                this.form.txtNum_ContenaShurui.Enabled = true;
                this.form.customPanel7.Enabled = true;
                this.txtNum_ContenaShurui_TextChanged();
            }
            else
            {
                this.form.customPanel9.Enabled = false;
                this.form.txtNum_ContenaShurui.Enabled = false;
                this.form.customPanel7.Enabled = false;
                this.txtNum_ContenaShurui_TextChanged();
            }

            return ret;
        }

        /// <summary>
        /// コンテナ種類指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_ContenaShurui_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_ContenaShurui.Text == "1")
            {
                this.form.dgvContena.Enabled = true;
            }
            else
            {
                this.form.dgvContena.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 設置コンテナ表示変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_SecchiContena_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_SecchiContena.Text == "1")
            {
                this.form.txtNum_ContenaShurui2.Enabled = true;
                this.form.customPanel11.Enabled = true;
                this.form.dgvContena2.Enabled = true;
                this.txtNum_ContenaShurui2_TextChanged();

                this.form.txtNum_SecchiKikan.Enabled = true;
                this.form.customPanel12.Enabled = true;
                this.form.txtNum_SecchiFrom.Enabled = true;
                this.form.txtNum_SecchiTo.Enabled = true;
                this.txtNum_SecchiKikan_TextChanged();

                this.form.txtNum_ChouhukuSecchiNomi.Enabled = true;
                this.form.customPanel1.Enabled = true;
            }
            else
            {
                this.form.txtNum_ContenaShurui2.Enabled = false;
                this.form.customPanel11.Enabled = false;
                this.form.dgvContena2.Enabled = false;
                this.form.txtNum_SecchiKikan.Enabled = false;
                this.form.customPanel12.Enabled = false;
                this.form.txtNum_SecchiFrom.Enabled = false;
                this.form.txtNum_SecchiTo.Enabled = false;
                this.form.txtNum_ChouhukuSecchiNomi.Enabled = false;
                this.form.customPanel1.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// コンテナ種類指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_ContenaShurui2_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_ContenaShurui2.Text == "1")
            {
                this.form.dgvContena2.Enabled = true;
            }
            else
            {
                this.form.dgvContena2.Enabled = false;
            }

            return ret;
        }

        /// <summary>
        /// 設置期間指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool txtNum_SecchiKikan_TextChanged()
        {
            bool ret = false;

            if (this.form.txtNum_SecchiKikan.Text == "1")
            {
                this.form.txtNum_SecchiFrom.Enabled = true;
                this.form.txtNum_SecchiTo.Enabled = true;
            }
            else
            {
                this.form.txtNum_SecchiFrom.Enabled = false;
                this.form.txtNum_SecchiTo.Enabled = false;
            }

            return ret;
        }
        #endregion

        #region DGV抽出

        private void KyotenDGV()
        {
            string sql = "SELECT KYOTEN_CD, KYOTEN_NAME FROM M_KYOTEN WHERE KYOTEN_CD != 99 AND DELETE_FLG=0";
            DataTable dt = this.sysInfoDao.GetDateForStringSql(sql);
            this.form.dgvKyoten.DataSource = dt;
        }

        private void GenchakuTimeDGV()
        {
            string sql = "SELECT GENCHAKU_TIME_CD, GENCHAKU_TIME_NAME FROM M_GENCHAKU_TIME WHERE DELETE_FLG=0";
            DataTable dt = this.sysInfoDao.GetDateForStringSql(sql);
            this.form.dgvGenchaku.DataSource = dt;
        }

        private void ShashuDGV()
        {
            string sql = "SELECT SHASHU_CD, SHASHU_NAME FROM M_SHASHU WHERE DELETE_FLG=0";
            DataTable dt = this.sysInfoDao.GetDateForStringSql(sql);
            this.form.dgvShashu.DataSource = dt;
        }

        private void ContenaShuruiDGV()
        {
            string sql = "SELECT CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME FROM M_CONTENA_SHURUI WHERE DELETE_FLG=0";
            DataTable dt = this.sysInfoDao.GetDateForStringSql(sql);
            this.form.dgvContena.DataSource = dt;
            this.form.dgvContena2.DataSource = dt;
        }

        #endregion

        #region 明細のヘッダーチェックボックス

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        internal void HeaderCheckBoxSupport()
        {
            try
            {
                if (!this.form.dgvKyoten.Columns.Contains("CHECKBOX"))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX";
                    newColumn.HeaderText = "";
                    newColumn.Width = 30;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 30;
                    newColumn.Resizable = DataGridViewTriState.False;

                    if (this.form.dgvKyoten.Columns.Count > 0)
                    {
                        this.form.dgvKyoten.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.dgvKyoten.Columns.Add(newColumn);
                    }
                    this.form.dgvKyoten.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }

                if (!this.form.dgvGenchaku.Columns.Contains("CHECKBOX2"))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX2";
                    newColumn.HeaderText = "";
                    newColumn.Width = 30;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 30;
                    newColumn.Resizable = DataGridViewTriState.False;

                    if (this.form.dgvGenchaku.Columns.Count > 0)
                    {
                        this.form.dgvGenchaku.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.dgvGenchaku.Columns.Add(newColumn);
                    }
                    this.form.dgvGenchaku.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }

                if (!this.form.dgvShashu.Columns.Contains("CHECKBOX3"))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX3";
                    newColumn.HeaderText = "";
                    newColumn.Width = 30;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 30;
                    newColumn.Resizable = DataGridViewTriState.False;
                    if (this.form.dgvShashu.Columns.Count > 0)
                    {
                        this.form.dgvShashu.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.dgvShashu.Columns.Add(newColumn);
                    }
                    this.form.dgvShashu.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }

                if (!this.form.dgvContena.Columns.Contains("CHECKBOX4"))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX4";
                    newColumn.HeaderText = "";
                    newColumn.Width = 30;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 30;
                    newColumn.Resizable = DataGridViewTriState.False;
                    if (this.form.dgvContena.Columns.Count > 0)
                    {
                        this.form.dgvContena.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.dgvContena.Columns.Add(newColumn);
                    }
                    this.form.dgvContena.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }

                if (!this.form.dgvContena2.Columns.Contains("CHECKBOX5"))
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();
                    newColumn.Name = "CHECKBOX5";
                    newColumn.HeaderText = "";
                    newColumn.Width = 30;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "";
                    newColumn.HeaderCell = newheader;
                    newColumn.MinimumWidth = 30;
                    newColumn.Resizable = DataGridViewTriState.False;
                    if (this.form.dgvContena2.Columns.Count > 0)
                    {
                        this.form.dgvContena2.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.dgvContena2.Columns.Add(newColumn);
                    }
                    this.form.dgvContena2.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region nullをブランクに変換

        private string NullToSpace(SqlDecimal value)
        {
            string ret = string.Empty;

            if (value.IsNull)
            {
                ret = string.Empty;
            }
            else
            {
                ret = Convert.ToString(value);
            }

            return ret;
        }

        private string NullToSpace(SqlInt64 value)
        {
            string ret = string.Empty;

            if (value.IsNull)
            {
                ret = string.Empty;
            }
            else
            {
                ret = Convert.ToString(value);
            }

            return ret;
        }

        private string NullToSpace(SqlInt16 value)
        {
            string ret = string.Empty;

            if (value.IsNull)
            {
                ret = string.Empty;
            }
            else
            {
                ret = Convert.ToString(value);
            }

            return ret;
        }

        private string NullToSpace(string value)
        {
            string ret = string.Empty;

            if (value != null)
            {
                ret = value;
            }

            return ret;
        }

        #endregion

        #region 単位の取得
        /// <summary>
        /// 単位名の取得
        /// </summary>
        /// <param name="unitCd"></param>
        /// <returns></returns>
        private string getUnitName(string unitCd)
        {
            string unitName = string.Empty;
            if (!string.IsNullOrEmpty(unitCd))
            {
                M_UNIT unit = new M_UNIT();
                unit = this.unitDao.GetDataByCd(Convert.ToInt16(unitCd));
                unitName = unit.UNIT_NAME_RYAKU;
            }
            return unitName;
        }

        #endregion
    }
}