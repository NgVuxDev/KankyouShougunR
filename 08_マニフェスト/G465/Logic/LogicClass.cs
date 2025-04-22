using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Accessor;
using System.ComponentModel;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.DTO;
using Seasar.Framework.Exceptions;
using System.Collections.ObjectModel;

namespace Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran
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
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        private BusinessBaseForm footer;

        #endregion

        #region プロパティ

        /// <summary>
        /// DAO
        /// </summary>
        private GetManifestIchiranDaoCls ManiDataDao;

        private IM_SHOBUN_HOUHOUDao shobunHouhouDao;

        /// <summary>検索結果</summary>
        public DataTable SearchResult { get; set; }

        /// <summary>検索条件</summary>
        public SearchInfoDto SearchString { get; set; }

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 廃棄物種類コード名称検索用Dao
        /// </summary>
        private IM_HAIKI_NAMEDao dao_HaikiName;

        /// <summary>
        /// 廃棄物名称コードと名称検索用Dao
        /// </summary>
        private IM_HAIKI_SHURUIDao dao_HaikiShurui;

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allControl;

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>共通</summary>
        Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic = null;

        /// <summary>マニフェスト情報数量書式CD</summary>
        internal string ManifestSuuryoFormatCD = String.Empty;

        /// <summary>マニフェスト情報数量書式</summary>
        internal string ManifestSuuryoFormat = String.Empty;

        internal string searchKbnCd = string.Empty;

        private MessageBoxShowLogic MsgBox;

        internal bool shubunFlg = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
            //DAO
            //ここ直す
            this.ManiDataDao = DaoInitUtility.GetComponent<GetManifestIchiranDaoCls>();
            this.shobunHouhouDao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.dao_HaikiName = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
            this.dao_HaikiShurui = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
            this.MsgBox = new MessageBoxShowLogic();

            //マスタデータを取得
            this.GetPopUpDenshiHaikiNameData();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.header = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            //タイトルの初期化
            SetHeaderTitleName();
            this.SetKyoten();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面見出しの設定
        /// </summary>
        private void SetHeaderTitleName()
        {
            LogUtility.DebugMethodStart();

            this.header.lb_title.Text = "マニフェスト換算値再計算";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //一括選択ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

            //再計算ボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new System.EventHandler(this.form.bt_func9_Click);

            //並び替え(F10)イベント生成
            //parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);

            //フィルタ(F11)イベント生成
            //parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //this.form.customDataGridView1.EditingControlShowing += this.form.Ichiran_EditingControlShowing;
            this.form.customDataGridView1.CellFormatting += this.form.Ichiran_CellFormatting;

            //排出事業者Validatingイベント生成
            this.form.cantxt_HaisyutuGyousyaCd.Validating += new CancelEventHandler(this.form.cantxt_HaisyutuGyousyaCd_Validating);

            //排出事業場Validatingイベント生成
            this.form.cantxt_HaisyutuJigyoujouCd.Validating += new CancelEventHandler(this.form.cantxt_HaisyutuJigyoujouCd_Validating);

            //運搬受託者Validatingイベント生成
            this.form.cantxt_UnpanJyutakushaCd.Validating += new CancelEventHandler(this.form.cantxt_UnpanJyutakushaCd_Validating);

            //処分受託者Validatingイベント生成
            this.form.cantxt_SyobunJyutakushaCd.Validating += new CancelEventHandler(this.form.cantxt_SyobunJyutakushaCd_Validating);

            //廃棄物種類Validatingイベント生成
            this.form.cantxt_HaikibutuShuruiCd.Validating += new CancelEventHandler(this.form.cantxt_HaikibutuShuruiCd_Validated);

            //廃棄物名称Validatingイベント生成
            this.form.cantxt_HaikibutuNameCd.Validating += new CancelEventHandler(this.form.cantxt_HaikibutuNameCd_Validated);

            //電子廃棄物種類Validatingイベント生成
            this.form.cantxt_ElecHaikiShuruiCd.Validating += new CancelEventHandler(this.form.cantxt_ElecHaikiShuruiCd_Validated);

            //電子廃棄物名称Validatingイベント生成
            this.form.cantxt_ElecHaikiNameCd.Validating += new CancelEventHandler(this.form.cantxt_ElecHaikiNameCd_Validated);

            //日付TOダブルクリックイベント生成	
            this.form.DATE_TO.DoubleClick += new EventHandler(this.form.DATE_TO_DoubleClick);

            //前回値保存の仕組み初期化
            this.form.EnterEventInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 継承したフォームのDGVのプロパティはデザイナで変更できない為、ここで設定
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                //this.form.customDataGridView1.Location = new System.Drawing.Point(1, 27);
                //this.form.customDataGridView1.Height = 437;
                this.form.customDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));

                //非表示項目削除
                this.form.RemoveHiddenCol();

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.header.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.header.NumberAlert = this.header.InitialNumberAlert;
                // システム情報からアラート件数を取得
                this.alertCount = (int)mSysInfo.ICHIRAN_ALERT_KENSUU;

                //読込データ件数
                this.header.ReadDataNumber.Text = "0";

                //アラート件数
                this.header.alertNumber.Text = this.header.NumberAlert.ToString();

                ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
                ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

                //検索条件の初期化処理
                this.SearchInfoInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // 権限チェックによるボタン制御
                var enabled = Manager.CheckAuthority("G465", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                parentForm.bt_func9.Enabled = enabled;  // 登録

                //※全選択/未選択チェックボックス
                this.form.checkBoxAll.Visible = false;
                //DGVヘッダの幅変更
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

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        internal void SearchInfoInit()
        {
            LogUtility.DebugMethodStart();

            this.form.cntxt_HaikiKbnCD.Text = "1";
            this.form.crbtnHaikiKbnCD_1.Checked = true;

            this.form.cntxt_KoufuDateKbn.Text = "1";
            this.form.crbtn_KoufuDateKbn_1.Checked = true;

            this.form.cantxt_HaisyutuGyousyaCd.Text = "";
            this.form.ctxt_HaisyutuGyousyaName.Text = "";

            this.form.cantxt_HaisyutuJigyoujouCd.Text = "";
            this.form.ctxt_HaisyutuJigyoujouName.Text = "";

            this.form.cantxt_UnpanJyutakushaCd.Text = "";
            this.form.ctxt_UnpanJyutakushaName.Text = "";

            this.form.cantxt_SyobunJyutakushaCd.Text = "";
            this.form.ctxt_SyobunJyutakushaName.Text = "";

            this.form.cantxt_HokokushoBunruiCd.Text = "";
            this.form.ctxt_HokokushoBunruiName.Text = "";

            this.form.cantxt_HaikibutuShuruiCd.Text = "";
            this.form.ctxt_HaikibutuShuruiName.Text = "";

            this.form.cantxt_HaikibutuNameCd.Text = "";
            this.form.ctxt_HaikibutuName.Text = "";

            this.form.DATE_FROM.Text = this.footer.sysDate.ToString("yyyy/MM/dd");
            this.form.DATE_TO.Text = this.footer.sysDate.ToString("yyyy/MM/dd");

            // 処分方法条件追加
            this.shobunJyokenAdd();

            LogUtility.DebugMethodEnd();
        }


        #endregion

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            Int32 dataCount = 0;
            try
            {
                //明細データ
                this.SearchResult = new DataTable();

                //表示クリア
                this.form.customDataGridView1.Rows.Clear();
                GC.Collect();
                GC.Collect();
                GC.Collect();

                this.SearchString = getSearchInfo();

                //1.産廃(直行) 2.産廃(積替) 3.建廃 4.電子 5.電子(混廃のみ) 6.全て(電子除く)
                if (this.form.cntxt_HaikiKbnCD.Text.ToString() == "4")
                {
                    this.SearchResult = this.ManiDataDao.GetDenshiManifestIchiranData(this.SearchString);
                    this.shubunFlg = false;
                }
                else if (this.form.cntxt_HaikiKbnCD.Text.ToString() == "5")
                {
                    this.SearchResult = this.ManiDataDao.GetDenshiManifestKongouHaikiIchiranData(this.SearchString);
                    this.shubunFlg = false;
                }
                else
                {
                    this.SearchResult = this.ManiDataDao.GetPaperManifestIchiranData(this.SearchString);
                    shubunFlg = true;
                }

                if (this.SearchResult.Rows.Count > 0)
                {
                    dataCount = this.SearchResult.Rows.Count;
                }
                else
                {
                    this.SearchResult = null;
                }

                this.setIchiran();
                //this.form.customDataGridView1.DataSource = this.SearchResult;

                //初期化
                //this.form.customDataGridView1.DataSource = null;
                //this.form.customDataGridView1.Rows.Clear();
                //this.form.customDataGridView1.Columns.Clear();

                //this.form.Table = this.SearchResult;
                //this.form.customDataGridView1.DataSource = this.SearchResult;

                //初期列名設定
                this.form.SetInitCol();

                this.form.checkBoxAll.Checked = false;

                //読込データ件数
                this.header.ReadDataNumber.Text = dataCount.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                dataCount = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dataCount);
            }
            //取得件数
            return dataCount;
        }

        /// <summary>
        /// 一覧にセット
        /// </summary>
        private void setIchiran()
        {

            if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
            {
                this.form.customDataGridView1.Rows.Add(this.SearchResult.Rows.Count);
                for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                    DataRow dr = this.SearchResult.Rows[i];
                    //row.Cells["SYSTEM_ID"].Value = dr["SYSTEM_ID"];
                    //row.Cells["SEQ"].Value = dr["SEQ"];
                    //row.Cells["DETAIL_SYSTEM_ID"].Value = dr["DETAIL_SYSTEM_ID"];
                    //row.Cells["HAIKI_KBN_CD"].Value = dr["HAIKI_KBN_CD"];
                    //row.Cells["ISKONGOU"].Value = dr["ISKONGOU"];
                    row.Cells["HAIKI_KBN_NAME"].Value = dr["HAIKI_KBN_NAME"];
                    row.Cells["KOUFU_DATE"].Value = dr["KOUFU_DATE"];
                    row.Cells["MANIFEST_ID"].Value = dr["MANIFEST_ID"];
                    //row.Cells["HAIKI_SHURUI_CD"].Value = dr["HAIKI_SHURUI_CD"];
                    row.Cells["HAIKI_SHURUI_NAME"].Value = dr["HAIKI_SHURUI_NAME"];
                    //row.Cells["HOUKOKUSHO_BUNRUI_CD"].Value = dr["HOUKOKUSHO_BUNRUI_CD"];
                    row.Cells["HOUKOKUSHO_BUNRUI_NAME"].Value = dr["HOUKOKUSHO_BUNRUI_NAME"];

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["HAIKI_SUU"])))
                    {
                        decimal HAIKI_SUU = Convert.ToDecimal(dr["HAIKI_SUU"]);
                        row.Cells["HAIKI_SUU"].Value = HAIKI_SUU.ToString(this.ManifestSuuryoFormat);
                    }
                    //row.Cells["HAIKI_UNIT_CD"].Value = dr["HAIKI_UNIT_CD"];
                    row.Cells["HAIKI_UNIT_NAME"].Value = dr["HAIKI_UNIT_NAME"];

                    // 換算用数量と単位CD（非混合電子用、非表示） start
                    //row.Cells["HAIKI_KAKUTEI_SUU"].Value = dr["HAIKI_KAKUTEI_SUU"];
                    //row.Cells["HAIKI_KAKUTEI_UNIT_CODE"].Value = dr["HAIKI_KAKUTEI_UNIT_CODE"];
                    //row.Cells["SU1_UPN_SHA_EDI_PASSWORD"].Value = dr["SU1_UPN_SHA_EDI_PASSWORD"];
                    //row.Cells["SU1_UPN_SUU"].Value = dr["SU1_UPN_SUU"];
                    //row.Cells["SU1_UPN_UNIT_CODE"].Value = dr["SU1_UPN_UNIT_CODE"];
                    //row.Cells["SU2_UPN_SHA_EDI_PASSWORD"].Value = dr["SU2_UPN_SHA_EDI_PASSWORD"];
                    //row.Cells["SU2_UPN_SUU"].Value = dr["SU2_UPN_SUU"];
                    //row.Cells["SU2_UPN_UNIT_CODE"].Value = dr["SU2_UPN_UNIT_CODE"];
                    //row.Cells["SU3_UPN_SHA_EDI_PASSWORD"].Value = dr["SU3_UPN_SHA_EDI_PASSWORD"];
                    //row.Cells["SU3_UPN_SUU"].Value = dr["SU3_UPN_SUU"];
                    //row.Cells["SU3_UPN_UNIT_CODE"].Value = dr["SU3_UPN_UNIT_CODE"];
                    //row.Cells["SU4_UPN_SHA_EDI_PASSWORD"].Value = dr["SU4_UPN_SHA_EDI_PASSWORD"];
                    //row.Cells["SU4_UPN_SUU"].Value = dr["SU4_UPN_SUU"];
                    //row.Cells["SU4_UPN_UNIT_CODE"].Value = dr["SU4_UPN_UNIT_CODE"];
                    //row.Cells["SU5_UPN_SHA_EDI_PASSWORD"].Value = dr["SU5_UPN_SHA_EDI_PASSWORD"];
                    //row.Cells["SU5_UPN_SUU"].Value = dr["SU5_UPN_SUU"];
                    //row.Cells["SU5_UPN_UNIT_CODE"].Value = dr["SU5_UPN_UNIT_CODE"];
                    //row.Cells["RECEPT_SUU"].Value = dr["RECEPT_SUU"];
                    //row.Cells["RECEPT_UNIT_CODE"].Value = dr["RECEPT_UNIT_CODE"];
                    // 換算用数量と単位CD end

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["OLD_KANSAN_SUU"])))
                    {
                        decimal KANSAN_SUU = Convert.ToDecimal(dr["OLD_KANSAN_SUU"]);
                        row.Cells["OLD_KANSAN_SUU"].Value = KANSAN_SUU.ToString(this.ManifestSuuryoFormat);
                        row.Cells["NEW_KANSAN_SUU"].Value = KANSAN_SUU.ToString(this.ManifestSuuryoFormat);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr["OLD_GENNYOU_SUU"])))
                    {
                        decimal GENNYOU_SUU = Convert.ToDecimal(dr["OLD_GENNYOU_SUU"]);
                        row.Cells["OLD_GENNYOU_SUU"].Value = GENNYOU_SUU.ToString(this.ManifestSuuryoFormat);
                        row.Cells["NEW_GENNYOU_SUU"].Value = GENNYOU_SUU.ToString(this.ManifestSuuryoFormat);
                    }

                    //row.Cells["HST_GYOUSHA_CD"].Value = dr["HST_GYOUSHA_CD"];
                    row.Cells["HST_GYOUSHA_NAME"].Value = dr["HST_GYOUSHA_NAME"];
                    //row.Cells["HST_GENBA_CD"].Value = dr["HST_GENBA_CD"];
                    row.Cells["HST_GENBA_NAME"].Value = dr["HST_GENBA_NAME"];
                    //row.Cells["UPN_GYOUSHA_CD"].Value = dr["UPN_GYOUSHA_CD"];
                    row.Cells["UPN_GYOUSHA_NAME"].Value = dr["UPN_GYOUSHA_NAME"];
                    //row.Cells["SBN_GYOUSHA_CD"].Value = dr["SBN_GYOUSHA_CD"];
                    row.Cells["SBN_GYOUSHA_NAME"].Value = dr["SBN_GYOUSHA_NAME"];
                    //row.Cells["HAIKI_NAME_CD"].Value = dr["HAIKI_NAME_CD"];
                    row.Cells["HAIKI_NAME"].Value = dr["HAIKI_NAME"];
                    //row.Cells["NISUGATA_CD"].Value = dr["NISUGATA_CD"];
                    row.Cells["NISUGATA_NAME"].Value = dr["NISUGATA_NAME"];
                    row.Cells["SBN_HOUHOU_CD"].Value = dr["SBN_HOUHOU_CD"];
                    row.Cells["SHOBUN_HOUHOU_NAME"].Value = dr["SHOBUN_HOUHOU_NAME"];

                    row.Cells["NEW_SBN_HOUHOU_CD"].Value = dr["SBN_HOUHOU_CD"];
                    row.Cells["NEW_SBN_HOUHOU_NAME"].Value = dr["SHOBUN_HOUHOU_NAME"];

                    row.Cells["DEN_OLD_KANSAN_SUU"].Value = dr["DEN_OLD_KANSAN_SUU"];
                    row.Cells["DEN_OLD_KANSAN_UNIT_NAME"].Value = dr["DEN_OLD_KANSAN_UNIT_NAME"];

                    if (dr["NEXT_SYSTEM_ID"] != null && !string.IsNullOrEmpty(dr["NEXT_SYSTEM_ID"].ToString()))
                    {
                        row.Cells["SBN_HOUHOU_CD"].ReadOnly = true;
                        //row.Cells["NEXT_MANIFEST_ID"].Value = dr["NEXT_SYSTEM_ID"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 換算値算出処理
        /// </summary>
        public void SetKansanti(int iRow)
        {
            LogUtility.DebugMethodStart(iRow);

            DataGridViewRow row = this.form.customDataGridView1.Rows[iRow];
            DataRow dr = this.SearchResult.Rows[iRow];
            switch (Convert.ToString(dr["HAIKI_KBN_CD"]))
            {
                case "1":
                case "2":
                case "3":
                    //換算値計算を共通化
                    mlogic.SetKansanti2(
                        Convert.ToString(dr["HAIKI_KBN_CD"]),
                        Convert.ToString(dr["HAIKI_SHURUI_CD"]),
                        Convert.ToString(dr["HAIKI_NAME_CD"]),
                        Convert.ToString(dr["HAIKI_SUU"]),
                        Convert.ToString(dr["NISUGATA_CD"]),
                        Convert.ToString(dr["HAIKI_UNIT_CD"]),
                        this.ManifestSuuryoFormatCD,
                        this.ManifestSuuryoFormat,
                        (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.customDataGridView1["NEW_KANSAN_SUU", iRow]
                        );
                    break;
                case "4":
                    this.SetDenshiKansanti(row, dr);
                    break;
                default:
                    break;
            }

            LogUtility.DebugMethodEnd();
            return;
        }

        /// <summary>
        /// 電マニ換算値計算　※必ず計算するので、呼び出し元はセルの値変更のない場合は呼ばないようにすること
        /// </summary>
        public void SetDenshiKansanti(DataGridViewRow row, DataRow dr)
        {
            ManifestoLogic maniLogic = new ManifestoLogic();
            //if (!this.IsNullOrEmpty(row.Cells["HAIKI_KAKUTEI_SUU"])
            //        && SqlDecimal.Parse(row.Cells["HAIKI_KAKUTEI_SUU"].ToString()) != 0)
            //{
            bool iskongou = "1".Equals(dr["ISKONGOU"]);
            SqlDecimal kakuteiSuu = 0;
            string kakuteiUnitCd = string.Empty;
            if (!iskongou)
            {
                #region 確定数量取得
                var sysInfos = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
                M_SYS_INFO sysInfo = sysInfos != null && sysInfos.Count() > 0 ? sysInfos[0] : new M_SYS_INFO();
                bool isEmptySuuryou = true;

                if (sysInfo != null && !sysInfo.MANIFEST_REPORT_SUU_KBN.IsNull)
                {
                    switch (sysInfo.MANIFEST_REPORT_SUU_KBN.ToString())
                    {
                        case "1":
                            // 1.確定数量
                            if (!string.IsNullOrEmpty(dr["HAIKI_KAKUTEI_SUU"].ToString()))
                            {
                                kakuteiSuu = SqlDecimal.Parse(dr["HAIKI_KAKUTEI_SUU"].ToString());
                                kakuteiUnitCd = dr["HAIKI_KAKUTEI_UNIT_CODE"].ToString();
                                isEmptySuuryou = false;
                            }
                            break;

                        case "2":
                            // 2.排出事業者
                            kakuteiSuu = Convert.ToDecimal(dr["HAIKI_SUU"]);
                            kakuteiUnitCd = dr["HAIKI_UNIT_CD"].ToString();
                            isEmptySuuryou = false;
                            break;

                        case "3":
                            // 3.収集運搬業者
                            // 区間5からチェックし、EDI_PASSWORDが存在した区間の運搬量を使用する
                            for (int i = 4; i >= 0; i--)
                            {
                                int no = i + 1;
                                string upnShaEdiPass = dr["SU" + no + "_UPN_SHA_EDI_PASSWORD"].ToString();
                                if (!string.IsNullOrEmpty(upnShaEdiPass))
                                {
                                    if (!string.IsNullOrEmpty(dr["SU" + no + "_UPN_SUU"].ToString()))
                                    {
                                        kakuteiSuu = SqlDecimal.Parse(dr["SU" + no + "_UPN_SUU"].ToString());
                                        kakuteiUnitCd = dr["SU" + no + "_UPN_UNIT_CODE"].ToString();
                                        isEmptySuuryou = false;
                                    }
                                    break;
                                }
                            }
                            break;

                        case "4":
                            // 4.処分事業者
                            if (!string.IsNullOrEmpty(dr["RECEPT_SUU"].ToString()))
                            {
                                kakuteiSuu = SqlDecimal.Parse(dr["RECEPT_SUU"].ToString());
                                kakuteiUnitCd = dr["RECEPT_UNIT_CODE"].ToString();
                                isEmptySuuryou = false;
                            }
                            break;

                        default:
                            break;
                    }
                }

                if (isEmptySuuryou)
                {
                    kakuteiSuu = Convert.ToDecimal(dr["HAIKI_SUU"]);
                    kakuteiUnitCd = dr["HAIKI_UNIT_CD"].ToString();
                }
                #endregion
            }
            else
            {
                kakuteiSuu = Convert.ToDecimal(dr["HAIKI_SUU"]);
                kakuteiUnitCd = dr["HAIKI_UNIT_CD"].ToString();
            }

            var dto = new DenshiSearchParameterDtoCls();

            string haikisyuruyiCd = Convert.ToString(dr["HAIKI_SHURUI_CD"]);

            dto.HAIKI_SHURUI_CD = haikisyuruyiCd;
            dto.HAIKI_NAME_CD = Convert.ToString(dr["HAIKI_NAME_CD"]);
            dto.UNIT_CD = kakuteiUnitCd;
            dto.NISUGATA_CD = dr["NISUGATA_CD"].ToString();

            var dataLogic = new DenshiMasterDataLogic();
            DataTable tbl = dataLogic.GetDenmaniKansanData(dto);
            if (tbl.Rows.Count == 1)
            {   //換算式の取得
                if (tbl.Rows[0]["KANSANCHI"] != null)
                {
                    string val = tbl.Rows[0]["KANSANCHI"].ToString();

                    //乗算式
                    if (tbl.Rows[0]["KANSANSHIKI"].ToString() == "0")
                    {
                        row.Cells["NEW_KANSAN_SUU"].Value = maniLogic.Round((decimal)SqlDecimal.Multiply(kakuteiSuu, SqlDecimal.Parse(val)), SystemProperty.Format.ManifestSuuryou);
                    }
                    //除算式
                    else
                    {
                        row.Cells["NEW_KANSAN_SUU"].Value = maniLogic.Round((decimal)SqlDecimal.Divide(kakuteiSuu, SqlDecimal.Parse(val)), SystemProperty.Format.ManifestSuuryou);
                    }
                }
            }
            else
            {
                row.Cells["NEW_KANSAN_SUU"].Value = string.Empty;
            }
            //}
        }

        /// <summary>
        /// 減容値算出処理
        /// </summary>
        internal bool SetGenyouti(int iRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(iRow);
                DataGridViewRow row = this.form.customDataGridView1.Rows[iRow];
                DataRow dr = this.SearchResult.Rows[iRow];
                switch (Convert.ToString(dr["HAIKI_KBN_CD"]))
                {
                    case "1":
                    case "2":
                    case "3":
                        //減容値計算を共通化
                        mlogic.SetGenyouti2(
                            Convert.ToString(dr["HAIKI_KBN_CD"]),
                            Convert.ToString(dr["HAIKI_KBN_CD"]),
                            Convert.ToString(dr["HAIKI_NAME_CD"]),
                            (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.customDataGridView1["SBN_HOUHOU_CD", iRow],
                            (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.customDataGridView1["NEW_KANSAN_SUU", iRow],
                            this.ManifestSuuryoFormatCD,
                            this.ManifestSuuryoFormat,
                            (r_framework.CustomControl.DgvCustomTextBoxCell)this.form.customDataGridView1["NEW_GENNYOU_SUU", iRow]
                            );
                        break;
                    case "4":
                        this.SetDenshiGenyouti(row, dr);
                        break;
                    case "5":
                        this.SetDenshiGenyouti(row, dr);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenyouti", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 電マニ減容値計算　※必ず計算するので、呼び出し元はセルの値変更のない場合は呼ばないようにすること
        /// </summary>
        public void SetDenshiGenyouti(DataGridViewRow row, DataRow dr)
        {
            ManifestoLogic maniLogic = new ManifestoLogic();
            SqlDecimal genyou_suu = SqlDecimal.Null;
            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["NEW_KANSAN_SUU"].Value)))
            {
                genyou_suu = Convert.ToDecimal(row.Cells["NEW_KANSAN_SUU"].Value);
            }
            if (!genyou_suu.IsNull && genyou_suu != 0)
            {
                //減容率の取得
                SqlDecimal GENNYOURITSU = 0;
                SearchDTOForDTExClass dto = new SearchDTOForDTExClass();
                //廃棄物種類CDが画面から取得する
                string haikisyuruyiCd = Convert.ToString(dr["HAIKI_SHURUI_CD"]);

                DataTable tbl = new DataTable();

                //報告書分類＋廃棄物名称＋処分方法＋減容率 で検索
                if (!string.IsNullOrEmpty(haikisyuruyiCd)
                    && !string.IsNullOrEmpty(Convert.ToString(dr["HAIKI_NAME_CD"]))
                    && !string.IsNullOrEmpty(Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value)))
                {
                    dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                    dto.HAIKI_NAME_CD = Convert.ToString(dr["HAIKI_NAME_CD"]);
                    dto.SHOBUN_HOUHOU_CD = Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value);

                    tbl = this.ManiDataDao.GetGenYourituData(dto);
                }

                // 報告書分類＋処分方法＋減容率
                if (tbl.Rows.Count < 1)
                {
                    if (!string.IsNullOrEmpty(haikisyuruyiCd)
                        && !string.IsNullOrEmpty(Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value)))
                    {
                        dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                        dto.HAIKI_NAME_CD = string.Empty;
                        dto.SHOBUN_HOUHOU_CD = Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value);

                        tbl = this.ManiDataDao.GetGenYourituData(dto);
                    }
                }

                // 報告書分類＋減容率
                if (tbl.Rows.Count < 1)
                {
                    if (!string.IsNullOrEmpty(haikisyuruyiCd))
                    {
                        dto.HAIKI_SHURUI_CD = haikisyuruyiCd.ToString().Substring(0, 4);
                        dto.HAIKI_NAME_CD = string.Empty;
                        dto.SHOBUN_HOUHOU_CD = string.Empty;
                        tbl = this.ManiDataDao.GetGenYourituData(dto);
                    }
                }

                if (tbl.Rows.Count == 1
                    && tbl.Rows[0]["GENNYOURITSU"] != null)
                {   //減容率の取得
                    GENNYOURITSU = SqlDecimal.Parse(tbl.Rows[0]["GENNYOURITSU"].ToString());
                    genyou_suu = (SqlDecimal.Divide(SqlDecimal.Multiply((SqlDecimal)Convert.ToDecimal(row.Cells["NEW_KANSAN_SUU"].Value), 100m - GENNYOURITSU), 100.00m));
                    genyou_suu = maniLogic.Round((decimal)genyou_suu, SystemProperty.Format.ManifestSuuryou);
                }
            }

            if (!genyou_suu.IsNull)
            {
                row.Cells["NEW_GENNYOU_SUU"].Value = Convert.ToDecimal(genyou_suu.Value);
            }
            else
            {
                row.Cells["NEW_GENNYOU_SUU"].Value = string.Empty;
            }
        }

        /// <summary>
        /// 換算値と減容値を再計算
        /// </summary>
        /// <returns></returns>
        internal void Saikeisann()
        {
            try
            {
                bool isCheck = false;
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];
                    if (Convert.ToBoolean(row.Cells["Flg"].Value) == true)
                    {
                        isCheck = true;
                        this.SetKansanti(i);
                        if (!this.SetGenyouti(i)) { return; }
                    }
                }

                if (!isCheck)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("W002", "再計算");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saikeisann", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 検索条件を取得する
        /// </summary>
        /// <returns></returns>
        private SearchInfoDto getSearchInfo()
        {
            SearchInfoDto searchInfo = new SearchInfoDto();

            //DTO項目初期化
            searchInfo.ConditionInit();
            searchInfo.KYOTEN_CD = this.header.KYOTEN_CD.Text == "99" ? string.Empty : this.header.KYOTEN_CD.Text;
            string tmpHaikiKbnCd = this.form.cntxt_HaikiKbnCD.Text.ToString();
            this.searchKbnCd = tmpHaikiKbnCd;
            switch (tmpHaikiKbnCd)
            {
                //1.産廃(直行) 2.産廃(積替) 3.建廃 4.電子 5.電子(混合のみ) 6.全て(電子除く)
                //DB{1.産廃(直行) 2.産廃(積替) 3.建廃 4.電子}
                //差を埋める
                case "1":
                    tmpHaikiKbnCd = "1"; break;
                case "2":
                    tmpHaikiKbnCd = "3"; break;
                case "3":
                    tmpHaikiKbnCd = "2"; break;
                case "4":
                case "5":
                    tmpHaikiKbnCd = "4"; break;
                default:
                    tmpHaikiKbnCd = ""; break;
            }
            searchInfo.HAIKI_KBN_CD = tmpHaikiKbnCd;
            Int16 tmpInt16 = 0;
            Int16.TryParse(this.form.cntxt_KoufuDateKbn.Text.ToString(), out tmpInt16);
            searchInfo.DATE_KBN = tmpInt16;
            DateTime tmpDateFrom;
            DateTime tmpDateTo;
            if (this.form.DATE_FROM.Value != null)
            {
                DateTime.TryParse(this.form.DATE_FROM.Value.ToString(), out tmpDateFrom);
                searchInfo.DATE_FR = tmpDateFrom.ToShortDateString();
            }
            else
            {
                searchInfo.DATE_FR = string.Empty;
            }
            if (this.form.DATE_TO.Value != null)
            {
                DateTime.TryParse(this.form.DATE_TO.Value.ToString(), out tmpDateTo);
                searchInfo.DATE_TO = tmpDateTo.ToShortDateString();
            }
            else
            {
                searchInfo.DATE_TO = string.Empty;
            }
            //電子マニフェストの日付書式は[yyyyMMdd]のため、/を除去すれば良い
            switch (this.form.cntxt_HaikiKbnCD.Text.ToString())
            {
                case "4":
                case "5":
                    searchInfo.DATE_FR = searchInfo.DATE_FR.Replace("/", "");
                    searchInfo.DATE_TO = searchInfo.DATE_TO.Replace("/", "");
                    break;
                default:
                    break;
            }
            searchInfo.HST_GYOUSHA_CD = this.form.cantxt_HaisyutuGyousyaCd.Text.ToString();
            searchInfo.HST_GENBA_CD = this.form.cantxt_HaisyutuJigyoujouCd.Text.ToString();
            searchInfo.UPN_GYOUSHA_CD = this.form.cantxt_UnpanJyutakushaCd.Text.ToString();
            searchInfo.SBN_GYOUSHA_CD = this.form.cantxt_SyobunJyutakushaCd.Text.ToString();
            searchInfo.SBN_GENBA_CD = this.form.cantxt_UnpanJyugyobaNameCd.Text.ToString();//157951
            searchInfo.HOUKOKUSHO_BUNRUI_CD = this.form.cantxt_HokokushoBunruiCd.Text.ToString();
            if (searchInfo.HAIKI_KBN_CD != "4" && searchInfo.HAIKI_KBN_CD != "5")
            {
                searchInfo.HAIKI_SHURUI_CD = this.form.cantxt_HaikibutuShuruiCd.Text.ToString();
                searchInfo.HAIKI_NAME_CD = this.form.cantxt_HaikibutuNameCd.Text.ToString();
            }
            else
            {
                searchInfo.HAIKI_SHURUI_CD = this.form.cantxt_ElecHaikiShuruiCd.Text.ToString();
                searchInfo.HAIKI_NAME_CD = this.form.cantxt_ElecHaikiNameCd.Text.ToString();
            }

            var sysInfos = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllData();
            M_SYS_INFO sysInfo = sysInfos != null && sysInfos.Count() > 0 ? sysInfos[0] : new M_SYS_INFO();
            if (sysInfo != null && !sysInfo.MANIFEST_REPORT_SUU_KBN.IsNull)
            {
                searchInfo.MANIFEST_REPORT_SUU_KBN = sysInfo.MANIFEST_REPORT_SUU_KBN.Value;
            }

            if(!string.IsNullOrEmpty(this.form.SHOBUN_HOUHOU_CD_SELECT.Text))
            {
                searchInfo.SBN_HOUHOU_CD = this.form.SHOBUN_HOUHOU_CD_SELECT.Text;
            }

            searchInfo.SHOBUN_CHECK = this.form.SHOBUN_CHECK.Checked;

            //画面から項目を取得

            //DTO返却
            return searchInfo;
        }

        /// <summary>
        /// DB値有無判断
        /// </summary>
        private bool IsNullOrEmpty(object obj)
        {
            if (obj == System.DBNull.Value || string.Empty.Equals(obj.ToString().Trim()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// DB値変換
        /// </summary>
        private string DbToString(object obj)
        {
            if (IsNullOrEmpty(obj))
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        /// <summary>
        /// 日付フォーマット
        /// </summary>
        private string DateFormat(object obj)
        {
            string objStr = DbToString(obj);
            if (objStr.Length == 8)
            {
                objStr = objStr.Substring(0, 4) + "/" + objStr.Substring(4, 2) + "/" + objStr.Substring(6, 2);
            }

            return objStr;
        }


        public void Update(bool bl)
        {
        }

        public void Regist(bool bl)
        {
            LogUtility.DebugMethodStart(bl);
            bool ret = true;
            bool isCheck = false;
            try
            {
                //エラーではない場合登録処理を行う
                //if (!bl)
                //{
                using (Transaction tran = new Transaction())
                {
                    foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                    {
                        if (!Convert.ToBoolean(row.Cells["Flg"].Value))
                        {
                            continue;
                        }
                        else
                        {
                            DataRow dr = this.SearchResult.Rows[row.Index];
                            isCheck = true;
                            SqlInt64 system_id = Convert.ToInt64(dr["SYSTEM_ID"]);
                            SqlInt32 seq = Convert.ToInt32(dr["SEQ"]);
                            SqlInt64 detail_system_id = SqlInt64.Null;
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["DETAIL_SYSTEM_ID"])))
                            {
                                detail_system_id = Convert.ToInt64(dr["DETAIL_SYSTEM_ID"]);
                            }
                            SqlDecimal kansan_suu = 0;
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["NEW_KANSAN_SUU"].Value)))
                            {
                                kansan_suu = Convert.ToDecimal(row.Cells["NEW_KANSAN_SUU"].Value);
                            }
                            else
                            {
                                kansan_suu = SqlDecimal.Null;
                            }
                            SqlDecimal gennyou_suu = 0;
                            if (!string.IsNullOrEmpty(Convert.ToString(row.Cells["NEW_GENNYOU_SUU"].Value)))
                            {
                                gennyou_suu = Convert.ToDecimal(Convert.ToString(row.Cells["NEW_GENNYOU_SUU"].Value));
                            }
                            else
                            {
                                gennyou_suu = SqlDecimal.Null;
                            }
                            string sbn_houhou_cd = string.Empty;

                            switch (this.searchKbnCd)
                            {
                                case "1":
                                case "2":
                                case "3":
                                case "6":
                                    sbn_houhou_cd = Convert.ToString(row.Cells["SBN_HOUHOU_CD"].Value);
                                    this.ManiDataDao.UpdateMani(kansan_suu, gennyou_suu, sbn_houhou_cd, system_id, seq, detail_system_id, 1);

                                    // 換算後数量、減容後数量が更新されているので、合計値も再計算
                                    this.ManiDataDao.UpdateTotalSuu(system_id, seq);

                                    break;
                                case "4":
                                    sbn_houhou_cd = Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value);
                                    this.ManiDataDao.UpdateMani(kansan_suu, gennyou_suu, sbn_houhou_cd, system_id, seq, detail_system_id, 2);
                                    break;
                                case "5":
                                    sbn_houhou_cd = Convert.ToString(row.Cells["NEW_SBN_HOUHOU_CD"].Value);
                                    this.ManiDataDao.UpdateMani(kansan_suu, gennyou_suu, sbn_houhou_cd, system_id, seq, detail_system_id, 3);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    tran.Commit();
                }
                //}
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }

            if (ret)
            {
                var msgLogic = new MessageBoxShowLogic();

                if (isCheck)
                {
                    msgLogic.MessageBoxShow("I001", "登録");
                    if (this.Search() == -1) { return; }
                    this.form.customDataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["Flg"].Value == null).ToList().ForEach(c => c.Cells["Flg"].Value = false);
                }
                else
                {
                    msgLogic.MessageBoxShow("W002", "登録");
                }
                //setDefaultCondition();
            }
            LogUtility.DebugMethodEnd(bl);
        }
        public void PhysicalDelete()
        {
        }
        public void LogicalDelete()
        {
        }

        //アラート件数
        public Boolean CheckNumberAlert(Int32 Kensu)
        {
            LogUtility.DebugMethodStart();

            Boolean check = false;

            if (Int32.Parse(this.header.NumberAlert.ToString()) < Kensu)
            {
                //検索件数がアラート件数を超えた場合
                //メッセージ「検索件数がアラート件数を超えました。\n表示を行いますか？」を表示する
                switch (MessageBoxUtility.MessageBoxShow("C025"))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        check = true;
                        break;
                }
            }

            LogUtility.DebugMethodEnd();
            return check;
        }

        internal bool CheckDate()
        {
            this.form.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.DATE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_FROM.IsInputErrorOccured = true;
                this.form.DATE_TO.IsInputErrorOccured = true;
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();

                //1:交付年月日　2:運搬終了日　3:処分終了日　4:最終処分終了日
                if (this.form.cntxt_KoufuDateKbn.Text == "1")
                {
                    string[] errorMsg = { "交付年月日From", "交付年月日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntxt_KoufuDateKbn.Text == "2")
                {
                    string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntxt_KoufuDateKbn.Text == "3")
                {
                    string[] errorMsg = { "処分終了日From", "処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntxt_KoufuDateKbn.Text == "4")
                {
                    string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.form.DATE_FROM.Focus();
                return true;
            }
            return false;
        }

        internal bool checkHouhou(int index, string sbnHouhouCd, string sbnHouhouName)
        {
            try
            {
                this.form.isErr = false;
                string cd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells[sbnHouhouCd].Value);
                if (string.IsNullOrEmpty(cd))
                {
                    this.form.customDataGridView1.Rows[index].Cells[sbnHouhouCd].Value = string.Empty;
                    this.form.customDataGridView1.Rows[index].Cells[sbnHouhouName].Value = string.Empty;
                }
                else
                {
                    if (cd != this.form.preSbnHouhouCd)
                    {
                        M_SHOBUN_HOUHOU shobunHouhou = new M_SHOBUN_HOUHOU();

                        if(sbnHouhouCd.Equals("SBN_HOUHOU_CD"))
                        {
                            if (!this.shubunFlg)
                            {
                                shobunHouhou.DENSHI_USE_KBN = true;
                            }
                            else
                            {
                                shobunHouhou.KAMI_USE_KBN = true;
                            }
                        }

                        shobunHouhou.SHOBUN_HOUHOU_CD = cd;
                        shobunHouhou.DELETE_FLG = false;
                        M_SHOBUN_HOUHOU[] result = this.shobunHouhouDao.GetAllValidData(shobunHouhou);
                        if (result != null && result.Length > 0)
                        {
                            this.form.customDataGridView1.Rows[index].Cells[sbnHouhouCd].Value = result[0].SHOBUN_HOUHOU_CD;
                            this.form.customDataGridView1.Rows[index].Cells[sbnHouhouName].Value = result[0].SHOBUN_HOUHOU_NAME;
                        }
                        else
                        {
                            MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                            msglogic.MessageBoxShow("E020", "処分方法");
                            this.form.isErr = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkHouhou", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.form.isErr = true;
            }
            return this.form.isErr;
        }

        /// <summary>
        /// 廃棄物名称チェック
        /// </summary>
        /// <param name="haikibutuShurui">廃棄物名称CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuName(object haikibutuName)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuName);

                TextBox txt1 = (TextBox)haikibutuName;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_HAIKI_NAME haikiName = this.dao_HaikiName.GetDataByCd(txt1.Text);

                if (haikiName == null || string.IsNullOrEmpty(haikiName.HAIKI_NAME_CD))
                {
                    this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物名称");
                    txt1.Focus();
                    txt1.SelectAll();
                    ret = 2;
                }
                else
                {
                    this.form.ctxt_HaikibutuName.Text = haikiName.HAIKI_NAME_RYAKU;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="haikibutuShurui">廃棄物種類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkHaikibutuShurui(object haikiKbn, object haikibutuShurui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikiKbn, haikibutuShurui);

                TextBox txt1 = (TextBox)haikiKbn;
                TextBox txt2 = (TextBox)haikibutuShurui;

                //空
                if (txt2.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_HAIKI_SHURUI haikiShurui = new M_HAIKI_SHURUI();
                switch (txt1.Text)
                {
                    case "1":
                        haikiShurui.HAIKI_KBN_CD = 1;
                        break;
                    case "2":
                        haikiShurui.HAIKI_KBN_CD = 3;
                        break;
                    case "3":
                        haikiShurui.HAIKI_KBN_CD = 2;
                        break;
                    case "4":
                    case "5":
                        haikiShurui.HAIKI_KBN_CD = 4;
                        break;
                }
                haikiShurui.HAIKI_SHURUI_CD = txt2.Text;
                haikiShurui = this.dao_HaikiShurui.GetDataByCd(haikiShurui);
                if (haikiShurui == null || string.IsNullOrEmpty(haikiShurui.HAIKI_SHURUI_CD))
                {
                    this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                    txt2.Focus();
                    txt2.SelectAll();
                    ret = 2;
                }
                else
                {
                    this.form.ctxt_HaikibutuShuruiName.Text = Convert.ToString(haikiShurui.HAIKI_SHURUI_NAME_RYAKU);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkHaikibutuShurui", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物種類チェック
        /// </summary>
        /// <param name="haikiKbn">廃棄区分</param>
        /// <param name="haikibutuShurui">廃棄物種類CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkDenshiHaikibutuShurui(object haikibutuShurui)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuShurui);

                TextBox txt1 = (TextBox)haikibutuShurui;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                M_DENSHI_HAIKI_SHURUI DenshiHaikiShurui = new M_DENSHI_HAIKI_SHURUI();
                DenshiHaikiShurui.HAIKI_SHURUI_CD = txt1.Text;

                DataTable dt = this.ManiDataDao.GetDenshiHaikiShuruiByCd(DenshiHaikiShurui);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物種類");
                        txt1.Focus();
                        txt1.SelectAll();
                        ret = 2;
                        break;

                    case 1:
                        this.form.ctxt_HaikibutuShuruiName.Text = Convert.ToString(dt.Rows[0]["HAIKI_SHURUI_NAME"]);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshiHaikibutuShurui", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 廃棄物名称チェック
        /// </summary>
        /// <param name="haikibutuShurui">廃棄物名称CD</param>
        /// <returns>０：正常　1:空　2：エラー</returns>
        public int ChkDenshiHaikibutuName(object haikibutuName)
        {
            int ret = 0;
            try
            {
                LogUtility.DebugMethodStart(haikibutuName);

                TextBox txt1 = (TextBox)haikibutuName;

                //空
                if (txt1.Text == string.Empty)
                {
                    ret = 1;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_DENSHI_HAIKI_NAME denshiHaikiName = new M_DENSHI_HAIKI_NAME();
                denshiHaikiName.HAIKI_NAME_CD = txt1.Text;

                DataTable dt = this.ManiDataDao.GetDenshiHaikiNameByCd(denshiHaikiName);
                switch (dt.Rows.Count)
                {
                    case 0:
                        this.form.messageShowLogic.MessageBoxShow("E020", "廃棄物名称");
                        txt1.Focus();
                        txt1.SelectAll();
                        ret = 2;
                        break;

                    default:
                        this.form.ctxt_HaikibutuName.Text = Convert.ToString(dt.Rows[0]["HAIKI_NAME"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshiHaikibutuName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = 2;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ユーザ設定から拠点を画面に設定します
        /// </summary>
        internal void SetKyoten()
        {
            LogUtility.DebugMethodStart();

            var fileAccess = new XMLAccessor();
            var config = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

            var kyotenCd = config.ItemSetVal1;

            if (!string.IsNullOrEmpty(kyotenCd))
            {
                this.header.KYOTEN_CD.Text = config.ItemSetVal1.PadLeft(2, '0');
            }

            this.header.KYOTEN_NAME.Text = string.Empty;

            if (!string.IsNullOrEmpty(this.header.KYOTEN_CD.Text))
            {
                var kyoten = this.kyotenDao.GetDataByCd(this.header.KYOTEN_CD.Text);
                if (null != kyoten)
                {
                    this.header.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #region マスタ検索処理
        /// <summary>
        /// 業者検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        /// <param name="type">１、排出　２、運搬　３、処分</param>
        internal M_GYOUSHA[] GetGyousha(string cd, int type, out bool catchErr)
        {
            M_GYOUSHA[] results = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(cd, type);
                M_GYOUSHA dto = new M_GYOUSHA();
                dto.GYOUSHA_CD = cd;
                dto.GYOUSHAKBN_MANI = true;
                switch (type)
                {
                    case 1:
                        dto.HAISHUTSU_NIZUMI_GYOUSHA_KBN = true;
                        break;
                    case 2:
                        dto.UNPAN_JUTAKUSHA_KAISHA_KBN = true;
                        break;
                    case 3:
                        dto.SHOBUN_NIOROSHI_GYOUSHA_KBN = true;
                        break;
                    default:
                        break;
                }
                dto.ISNOT_NEED_DELETE_FLG = true;
                IM_GYOUSHADao dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }

        /// <summary>
        /// 現場検索処理
        /// </summary>
        /// <param name="cd">CD</param>
        internal M_GENBA[] GetGenba(string gyoushaCd, string genbaCd, int Kbn, out bool catchErr)
        {
            M_GENBA[] results = null;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd, Kbn);
                M_GENBA dto = new M_GENBA();
                dto.GYOUSHA_CD = gyoushaCd;
                dto.GENBA_CD = genbaCd;
                if (Kbn == 1)
                {
                    dto.HAISHUTSU_NIZUMI_GENBA_KBN = true;
                }
                dto.ISNOT_NEED_DELETE_FLG = true;
                IM_GENBADao dao = DaoInitUtility.GetComponent<IM_GENBADao>();
                results = dao.GetAllValidData(dto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(results, catchErr);
            }
            return results;
        }
        #endregion

        #region マスタコードのチェック処理初期化（DataTableの準備）
        /// <summary>
        /// 電子廃棄物名称選択ポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public void GetPopUpDenshiHaikiNameData()
        {
            LogUtility.DebugMethodStart();
            try
            {
                DataTable DenshiHaikiNameCodeResult = new DataTable();
                M_DENSHI_HAIKI_NAME denshiHaikiName = new M_DENSHI_HAIKI_NAME();
                DenshiHaikiNameCodeResult = this.ManiDataDao.DenshiHaikiNameSearchAndCheckSql(denshiHaikiName);

                // 列名とデータソース設
                this.form.cantxt_ElecHaikiNameCd.PopupDataHeaderTitle = new string[] { "電子廃棄物CD", "電子廃棄物名称" };
                this.form.cantxt_ElecHaikiNameCd.PopupDataSource = DenshiHaikiNameCodeResult;
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataHeaderTitle = new string[] { "電子廃棄物CD", "電子廃棄物名称" };
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource = DenshiHaikiNameCodeResult;
                //検索画面のタイトルを設定
                this.form.cantxt_ElecHaikiNameCd.PopupDataSource.TableName = "電子廃棄物名称";
                this.form.cbtn_ElecHaikibutuNameSan.PopupDataSource.TableName = "電子廃棄物名称";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                    throw;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 検索ボタン押下時のチェック
        /// </summary>
        /// <returns>TRUE:入力OK, FALSE:エラー</returns>
        internal bool InputSearchCheck()
        {
            bool result = true;
            try
            {
                if (!dateIntegrityCheck())
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("InputSearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 日付入力不正チェック
        /// </summary>
        /// <returns>TRUE:入力OK, FALSE:エラー</returns>
        private bool dateIntegrityCheck()
        {
            var bRet = true;
            var fromDateStr = this.form.DATE_FROM.GetResultText();
            var toDateStr = this.form.DATE_TO.GetResultText();

            // エラー状態初期化
            this.form.DATE_FROM.IsInputErrorOccured = false;
            this.form.DATE_TO.IsInputErrorOccured = false;

            if ((!string.IsNullOrEmpty(fromDateStr)) && (!string.IsNullOrEmpty(toDateStr)))
            {
                // 日付変換後、比較を行う
                var fromDate = DateTime.Parse(fromDateStr);
                var toDate = DateTime.Parse(toDateStr);
                if (0 < fromDate.CompareTo(toDate))
                {
                    // 日付入力不正のため、エラー表示
                    this.form.DATE_FROM.IsInputErrorOccured = true;
                    this.form.DATE_TO.IsInputErrorOccured = true;
                    this.form.messageShowLogic.MessageBoxShow("E030", this.form.DATE_FROM.DisplayItemName, this.form.DATE_TO.DisplayItemName);
                    this.form.DATE_FROM.Focus();
                    bRet = false;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SearchCheck()
        {
            bool isErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                var allControlAndHeaderControls = allControl.ToList();
                allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.header));
                var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
                this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (this.form.RegistErrorFlag)
                {
                    //必須チェックエラーフォーカス処理
                    this.SetErrorFocus();
                    isErr = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                isErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isErr);
            }
            return isErr;
        }

        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.header.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }

        public void ShubunHouhouReplace(object sender, EventArgs e)
        {
            LogUtility.DebugMethodEnd(sender, e);
            try
            {
                bool dataFlg = false;
                foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                {
                    int rIndex = row.Index;
                    DataRow dr = this.SearchResult.Rows[rIndex];
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
                    {
                        if (row.Cells["Flg"].Value != null && Convert.ToBoolean(row.Cells["Flg"].Value))
                        {
                            dataFlg = true;
                            row.Cells["SBN_HOUHOU_CD"].Value = this.form.SHOBUN_HOUHOU_CD_REPLACE.Text;
                            row.Cells["SHOBUN_HOUHOU_NAME"].Value = this.form.SHOBUN_HOUHOU_NAME_REPLACE.Text;
                        }
                    }
                    else
                    {
                        if(row.Cells["Flg"].Value != null && Convert.ToBoolean(row.Cells["Flg"].Value))
                        {
                            dataFlg = true;
                            row.Cells["NEW_SBN_HOUHOU_CD"].Value = this.form.SHOBUN_HOUHOU_CD_REPLACE.Text;
                            row.Cells["NEW_SBN_HOUHOU_NAME"].Value = this.form.SHOBUN_HOUHOU_NAME_REPLACE.Text;
                        }
                    }
                }
                if (!dataFlg)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShowWarn("処分方法を置換する対象レコードが0件です。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // 処分方法条件追加
        public void shobunJyokenAdd()
        {
            if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
            {
                // 処分方法CD
                this.form.SHOBUN_HOUHOU_CD_SELECT.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto = new r_framework.Dto.JoinMethodDto();
                joinMethodDto.Join = JOIN_METHOD.WHERE;
                joinMethodDto.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto.LeftColumn = "KAMI_USE_KBN";
                searchConditionsDto.Value = "1";
                searchConditionsDto.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto.SearchCondition.Add(searchConditionsDto);

                this.form.SHOBUN_HOUHOU_CD_SELECT.popupWindowSetting.Add(joinMethodDto);

                // 処分方法CD POP
                this.form.cbtn_SHOBUN_HOUHOU.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDtoPop = new r_framework.Dto.JoinMethodDto();
                joinMethodDtoPop.Join = JOIN_METHOD.WHERE;
                joinMethodDtoPop.LeftTable = "M_SHOBUN_HOUHOU";

                // 検索条件設定
                var searchConditionsDtoPop = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDtoPop.LeftColumn = "KAMI_USE_KBN";
                searchConditionsDtoPop.Value = "1";
                searchConditionsDtoPop.ValueColumnType = DB_TYPE.BIT;

                joinMethodDtoPop.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDtoPop.SearchCondition.Add(searchConditionsDtoPop);

                this.form.cbtn_SHOBUN_HOUHOU.popupWindowSetting.Add(joinMethodDtoPop);

                //処分方法（置換後）
                this.form.SHOBUN_HOUHOU_CD_REPLACE.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto1 = new r_framework.Dto.JoinMethodDto();
                joinMethodDto1.Join = JOIN_METHOD.WHERE;
                joinMethodDto1.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto1 = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto1.LeftColumn = "KAMI_USE_KBN";
                searchConditionsDto1.Value = "1";
                searchConditionsDto1.ValueColumnType = DB_TYPE.BIT;

                var searchConditionsDto2 = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto2.LeftColumn = "DELETE_FLG";
                searchConditionsDto2.Value = "0";
                searchConditionsDto2.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto1.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto1.SearchCondition.Add(searchConditionsDto1);
                joinMethodDto1.SearchCondition.Add(searchConditionsDto2);

                this.form.SHOBUN_HOUHOU_CD_REPLACE.popupWindowSetting.Add(joinMethodDto1);

                //処分方法（置換後）POP
                this.form.cbtn_SHOBUN_HOUHOU_REPLACE.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto1Pop = new r_framework.Dto.JoinMethodDto();
                joinMethodDto1Pop.Join = JOIN_METHOD.WHERE;
                joinMethodDto1Pop.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto1Pop = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto1Pop.LeftColumn = "KAMI_USE_KBN";
                searchConditionsDto1Pop.Value = "1";
                searchConditionsDto1Pop.ValueColumnType = DB_TYPE.BIT;

                var searchConditionsDto2Pop = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto2Pop.LeftColumn = "DELETE_FLG";
                searchConditionsDto2Pop.Value = "0";
                searchConditionsDto2Pop.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto1Pop.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto1Pop.SearchCondition.Add(searchConditionsDto1Pop);
                joinMethodDto1Pop.SearchCondition.Add(searchConditionsDto2Pop);

                this.form.cbtn_SHOBUN_HOUHOU_REPLACE.popupWindowSetting.Add(joinMethodDto1Pop);

            }
            else
            {
                // 処分方法CD
                this.form.SHOBUN_HOUHOU_CD_SELECT.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto = new r_framework.Dto.JoinMethodDto();
                joinMethodDto.Join = JOIN_METHOD.WHERE;
                joinMethodDto.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto.LeftColumn = "DENSHI_USE_KBN";
                searchConditionsDto.Value = "1";
                searchConditionsDto.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto.SearchCondition.Add(searchConditionsDto);

                this.form.SHOBUN_HOUHOU_CD_SELECT.popupWindowSetting.Add(joinMethodDto);

                // 処分方法CD POP
                this.form.cbtn_SHOBUN_HOUHOU.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDtoPop = new r_framework.Dto.JoinMethodDto();
                joinMethodDtoPop.Join = JOIN_METHOD.WHERE;
                joinMethodDtoPop.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDtoPop = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDtoPop.LeftColumn = "DENSHI_USE_KBN";
                searchConditionsDtoPop.Value = "1";
                searchConditionsDtoPop.ValueColumnType = DB_TYPE.BIT;

                joinMethodDtoPop.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDtoPop.SearchCondition.Add(searchConditionsDtoPop);

                this.form.cbtn_SHOBUN_HOUHOU.popupWindowSetting.Add(joinMethodDtoPop);

                //処分方法（置換後）
                this.form.SHOBUN_HOUHOU_CD_REPLACE.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto1 = new r_framework.Dto.JoinMethodDto();
                joinMethodDto1.Join = JOIN_METHOD.WHERE;
                joinMethodDto1.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto2 = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto2.LeftColumn = "DELETE_FLG";
                searchConditionsDto2.Value = "0";
                searchConditionsDto2.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto1.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto1.SearchCondition.Add(searchConditionsDto2);

                this.form.SHOBUN_HOUHOU_CD_REPLACE.popupWindowSetting.Add(joinMethodDto1);

                //処分方法（置換後）POP
                this.form.cbtn_SHOBUN_HOUHOU_REPLACE.popupWindowSetting.Clear();
                // 結合条件設定
                var joinMethodDto1Pop = new r_framework.Dto.JoinMethodDto();
                joinMethodDto1Pop.Join = JOIN_METHOD.WHERE;
                joinMethodDto1Pop.LeftTable = "M_SHOBUN_HOUHOU";
                // 検索条件設定
                var searchConditionsDto2Pop = new r_framework.Dto.SearchConditionsDto();
                searchConditionsDto2Pop.LeftColumn = "DELETE_FLG";
                searchConditionsDto2Pop.Value = "0";
                searchConditionsDto2Pop.ValueColumnType = DB_TYPE.BIT;

                joinMethodDto1Pop.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                joinMethodDto1Pop.SearchCondition.Add(searchConditionsDto2Pop);

                this.form.cbtn_SHOBUN_HOUHOU_REPLACE.popupWindowSetting.Add(joinMethodDto1Pop);
            }
        }

        //処分方法チェック
        internal bool checkHouhouReplace(string sbnHouhouCd)
        {
            try
            {
                M_SHOBUN_HOUHOU shobunHouhou = new M_SHOBUN_HOUHOU();

                if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
                {
                    shobunHouhou.KAMI_USE_KBN = true;
                }

                shobunHouhou.SHOBUN_HOUHOU_CD = sbnHouhouCd;
                shobunHouhou.DELETE_FLG = false;
                M_SHOBUN_HOUHOU[] result = this.shobunHouhouDao.GetAllValidData(shobunHouhou);
                if (result != null && result.Length > 0)
                {
                    this.form.SHOBUN_HOUHOU_CD_REPLACE.Text = result[0].SHOBUN_HOUHOU_CD;
                    this.form.SHOBUN_HOUHOU_NAME_REPLACE.Text = result[0].SHOBUN_HOUHOU_NAME;
                }
                else
                {
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E020", "処分方法");
                    this.form.SHOBUN_HOUHOU_CD_REPLACE.Focus();
                    this.form.SHOBUN_HOUHOU_NAME_REPLACE.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkHouhouReplace", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.form.isErr = true;
            }
            return this.form.isErr;
        }

        //処分方法チェック
        internal bool checkHouhouSelect(string sbnHouhouCd)
        {
            try
            {
                M_SHOBUN_HOUHOU shobunHouhou = new M_SHOBUN_HOUHOU();

                if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
                {
                    shobunHouhou.KAMI_USE_KBN = true;
                }
                else
                {
                    shobunHouhou.DENSHI_USE_KBN = true;
                }

                shobunHouhou.SHOBUN_HOUHOU_CD = sbnHouhouCd;
                shobunHouhou.DELETE_FLG = false;
                M_SHOBUN_HOUHOU[] result = this.shobunHouhouDao.GetAllValidData(shobunHouhou);
                if (result != null && result.Length > 0)
                {
                    this.form.SHOBUN_HOUHOU_CD_SELECT.Text = result[0].SHOBUN_HOUHOU_CD;
                    this.form.SHOBUN_HOUHOU_NAME_SELECT.Text = result[0].SHOBUN_HOUHOU_NAME;
                }
                else
                {
                    MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                    msglogic.MessageBoxShow("E020", "処分方法");
                    this.form.SHOBUN_HOUHOU_CD_SELECT.Focus();
                    this.form.SHOBUN_HOUHOU_NAME_SELECT.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkHouhouReplace", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.form.isErr = true;
            }
            return this.form.isErr;
        }

        /// <summary>
        /// 明細のレイアウト調整
        /// </summary>
        internal void ExecuteAlignmentForDetail()
        {
            this.SearchResult = new DataTable();
            this.form.customDataGridView1.Rows.Clear();
            GC.Collect();
            GC.Collect();
            GC.Collect();

            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if(column.Name.Equals("DEN_OLD_KANSAN_SUU"))
                {
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() == "4")
                    {
                        column.Visible = true;
                    }
                    else
                    {
                        column.Visible = false;
                    }
                }

                if (column.Name.Equals("DEN_OLD_KANSAN_UNIT_NAME"))
                {
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() == "4")
                    {
                        column.Visible = true;
                    }
                    else
                    {
                        column.Visible = false;
                    }
                }

                if (column.Name.Equals("SBN_HOUHOU_CD"))
                {
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
                    {
                        column.HeaderText = "処分方法CD";
                        column.Visible = true;
                        column.ReadOnly = false;

                        var col = this.form.customDataGridView1.Columns["SBN_HOUHOU_CD"] as DgvCustomAlphaNumTextBoxColumn;
                        col.popupWindowSetting.Clear();

                        // 結合条件設定
                        var joinMethodDto = new r_framework.Dto.JoinMethodDto();
                        joinMethodDto.Join = JOIN_METHOD.WHERE;
                        joinMethodDto.LeftTable = "M_SHOBUN_HOUHOU";
                        // 検索条件設定
                        var searchConditionsDto = new r_framework.Dto.SearchConditionsDto();
                        searchConditionsDto.LeftColumn = "DELETE_FLG";
                        searchConditionsDto.Value = "0";
                        searchConditionsDto.ValueColumnType = DB_TYPE.BIT;

                        var searchConditionsDto1 = new r_framework.Dto.SearchConditionsDto();
                        searchConditionsDto1.LeftColumn = "KAMI_USE_KBN";
                        searchConditionsDto1.Value = "1";
                        searchConditionsDto1.ValueColumnType = DB_TYPE.BIT;

                        joinMethodDto.SearchCondition = new Collection<r_framework.Dto.SearchConditionsDto>();
                        joinMethodDto.SearchCondition.Add(searchConditionsDto);
                        joinMethodDto.SearchCondition.Add(searchConditionsDto1);

                        col.popupWindowSetting.Add(joinMethodDto);

                    }
                    else
                    {
                        column.HeaderText = "処分方法CD";
                        column.Visible = false;
                        column.ReadOnly = true;
                    }
                }

                if (column.Name.Equals("SHOBUN_HOUHOU_NAME"))
                {
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() != "4" && this.form.cntxt_HaikiKbnCD.Text.ToString() != "5")
                    {
                        column.Visible = true;
                    }
                    else
                    {
                        column.Visible = false;
                    }
                }

                if (column.Name.Equals("NEW_SBN_HOUHOU_CD") || column.Name.Equals("NEW_SBN_HOUHOU_NAME"))
                {
                    if (this.form.cntxt_HaikiKbnCD.Text.ToString() == "4" || this.form.cntxt_HaikiKbnCD.Text.ToString() == "5")
                    {
                        column.Visible = true;
                    }
                    else
                    {
                        column.Visible = false;
                    }
                }
            }
        }
    }
}
