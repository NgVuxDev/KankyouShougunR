using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using CommonChouhyouPopup.Logic;
using r_framework.APP.Base;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Billing.SeikyuushoHakkou.APP;
using Shougun.Core.Billing.SeikyuushoHakkou.Const;
using Shougun.Core.Billing.SeikyuushoHakkou.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Billing.SeikyuushoHakkou.DTO;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using System.IO;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass SearchString { get; set; }

        /// <summary>
        /// 締め処理画面連携 - 画面初期表示用DTO
        /// </summary>
        public DTOClass InitDto { get; set; }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private static GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region フィールド
        ////<summary>
        ////パターン一覧のDao
        ////</summary>
        //private TSDDaoCls TsdDaoPatern;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 業者マスタ
        /// </summary>
        private IM_GYOUSHADao mgyoushaDao;

        /// <summary>
        /// 現場マスタ
        /// </summary>
        private IM_GENBADao mgenbaDao;
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        /// <summary>
        ///請求伝票
        /// </summary>
        private TSDDaoCls SeikyuDenpyouDao;

        /// <summary>	
        /// 取引先_請求情報マスタ	
        /// </summary>	
        private MTSDaoCls TorihikisakiSeikyuuDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Billing.SeikyuushoHakkou.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderSeikyuushoHakkou.cs
        /// </summary>
        private HeaderSeikyuushoHakkou headerForm;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO mSysInfo;

        private string strSystemDate = string.Empty;

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 明細発行先コードがないチェック
        /// </summary>
        private bool hakkousakuCheck = true;

        /// <summary>
        /// 入金計
        /// </summary>
        private decimal nyuukinKei = 0;
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        private TSDKDaoCls SeikyuKagamiDao; // 20211208 thucp v2.24_電子請求書 #157799

        MessageBoxShowLogic msgLogic;

        M_FILE_LINK_SYS_INFO fileLink;

        IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao;
        
        T_FILE_DATA fileData;

        FILE_DATADAO fileDataDao;

        FileUploadLogic uploadLogic;
        
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.SearchString = new DTOClass();
            this.SeikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<MTSDaoCls>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.SeikyuKagamiDao = DaoInitUtility.GetComponent<TSDKDaoCls>(); // 20211208 thucp v2.24_電子請求書 #157799
            this.msgLogic = new MessageBoxShowLogic();
            this.fileLink = new M_FILE_LINK_SYS_INFO();
            this.fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();
            this.fileData = new T_FILE_DATA();
            this.fileDataDao = DaoInitUtility.GetComponent<FILE_DATADAO>();
            this.uploadLogic = new FileUploadLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm, HeaderSeikyuushoHakkou headerForm)
        {
            LogUtility.DebugMethodStart(targetForm, headerForm);

            this.form = targetForm;
            this.headerForm = headerForm;
            this.SearchString = new DTOClass();
            this.SeikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<MTSDaoCls>();
            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 20160429 koukoukon v2.1_電子請求書 #16612 end
            this.msgLogic = new MessageBoxShowLogic();
            this.fileLink = new M_FILE_LINK_SYS_INFO();
            this.fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();
            this.fileData = new T_FILE_DATA();
            this.fileDataDao = DaoInitUtility.GetComponent<FILE_DATADAO>();
            this.uploadLogic = new FileUploadLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion コンストラクタ

        #region メソッド

        #region 初期化
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();


                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム設定
                mSysInfo = new DBAccessor().GetSysInfo();

                // ・画面の初期表示時、以下の項目を設定する。
                //【拠点】
                //【伝票日付From】【伝票日付To】
                //【発行拠点】【締日】【印刷順】
                //【請求用紙】【請求形態】【明細】
                //【検索条件】【請求書印刷日】【指定印刷日付】【取引先】【発行区分】
                this.headerForm.USER_KYOTEN_CD.Text = "99";
                // ユーザ拠点名称の取得
                M_KYOTEN mKyoten1 = new M_KYOTEN();
                if (this.InitDto != null)
                {
                    mKyoten1 = (M_KYOTEN)mkyotenDao.GetDataByCd(this.InitDto.InitKyotenCd);

                    if (mKyoten1 == null || string.IsNullOrEmpty(this.InitDto.InitKyotenCd))
                    {
                        this.headerForm.USER_KYOTEN_CD.Text = String.Empty;
                        this.headerForm.USER_KYOTEN_NAME.Text = String.Empty;
                    }
                    else
                    {
                        this.headerForm.USER_KYOTEN_CD.Text = this.InitDto.InitKyotenCd;
                        this.headerForm.USER_KYOTEN_NAME.Text = mKyoten1.KYOTEN_NAME_RYAKU;
                    }
                }
                else
                {
                    mKyoten1 = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headerForm.USER_KYOTEN_CD.Text);
                    // 拠点CDが空欄だと0検索されるためチェック
                    if (mKyoten1 == null || string.IsNullOrEmpty(this.headerForm.USER_KYOTEN_CD.Text))
                    {
                        this.headerForm.USER_KYOTEN_CD.Text = "";
                        this.headerForm.USER_KYOTEN_NAME.Text = "";
                    }
                    else
                    {
                        this.headerForm.USER_KYOTEN_NAME.Text = mKyoten1.KYOTEN_NAME_RYAKU;
                    }
                }

                // 【拠点】
                //================================CurrentUserCustomConfigProfile.xmlを読み込み============================
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                //ヘッダ拠点CD
                this.form.KYOTEN_CD.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

                var parentForm = (BusinessBaseForm)this.form.Parent;

                strSystemDate = parentForm.sysDate.ToShortDateString();
                // ユーザ拠点名称の取得
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.form.KYOTEN_CD.Text);
                // 拠点CDが空欄だと0検索されるためチェック
                if (mKyoten == null || string.IsNullOrEmpty(this.form.KYOTEN_CD.Text))
                {
                    this.form.KYOTEN_NAME.Text = "";
                }
                else
                {
                    this.form.KYOTEN_NAME.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }

                if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
                {
                    // 締め処理画面からの引継ぎ時
                    this.headerForm.DenpyouHidukeFrom.Text = this.InitDto.InitDenpyouHiduke;
                    this.headerForm.DenpyouHidukeTo.Text = this.InitDto.InitDenpyouHiduke;
                }
                else
                {
                    this.headerForm.DenpyouHidukeFrom.Text = parentForm.sysDate.ToString("yyyy-MM-dd");
                    this.headerForm.DenpyouHidukeTo.Text = parentForm.sysDate.ToString("yyyy-MM-dd");
                }

                int shimebiIndex = 0;
                if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitShimebi))
                {
                    // 締め処理画面からの引継ぎ時
                    for (int i = 0; i < this.form.cmbShimebi.Items.Count; i++)
                    {
                        if (this.form.cmbShimebi.Items[i].ToString().Equals(this.InitDto.InitShimebi))
                        {
                            shimebiIndex = i;
                            break;
                        }
                    }
                }
                this.form.cmbShimebi.SelectedIndex = shimebiIndex;

                this.form.PRINT_ORDER.Text = ConstCls.PRINT_ORDER_FURIGANA;
                this.form.SEIKYU_PAPER.Text = ConstCls.SEIKYU_PAPER_DATA_SAKUSEIJI_JISYA;
                this.form.SEIKYU_STYLE.Text = ConstCls.SEIKYU_KEITAI_DATA_SAKUSEIJI;
                //this.form.MEISAI.Text = ConstCls.NYUKIN_MEISAI_ARI;   // No.4004

                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitTorihiksiakiCd))
                {
                    // 締め処理画面からの引継ぎ時
                    M_TORIHIKISAKI mTorihikisaki = new M_TORIHIKISAKI();
                    mTorihikisaki = mtorihikisakiDao.GetDataByCd(this.InitDto.InitTorihiksiakiCd);

                    if (mTorihikisaki != null)
                    {
                        this.form.TORIHIKISAKI_CD.Text = this.InitDto.InitTorihiksiakiCd;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                this.form.HAKKOU_KBN.Text = ConstCls.HAKKOU_KBN_SUBETE;
                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.form.OUTPUT_KBN.Text = ConstCls.OUTPUT_KBN_SUBETE;
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                this.form.HIKAE_OUTPUT_KBN.Text = ConstCls.HIKAE_OUTPUT_NON;
                this.form.txtKensakuJouken.Text = string.Empty;
                this.form.SEIKYUSHO_PRINTDAY.Text = ConstCls.SEIKYU_PRINT_DAY_SIMEBI;
                this.form.SEIKYU_HAKKOU.Text = ConstCls.SEIKYU_HAKKOU_PRINT_SHINAI;
                this.form.cdtSiteiPrintHiduke.Text = string.Empty;
                //請求書印刷日活性制御
                if (!CdtSiteiPrintHidukeEnable(this.form.SEIKYUSHO_PRINTDAY.Text))
                {
                    ret = false;
                    return ret;
                }
                // 抽出データ
                this.form.FILTERING_DATA.Text = ConstCls.FILTERING_DATA_INCLUDE_OTHER.ToString();
                this.form.ZERO_KINGAKU_TAISHOGAI.Checked = true;//VAN 20201125 #136235, #136229
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errMsg.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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

            // 20160429 koukoukon v2.1_電子請求書 #16612 start
            //プレビューボタン(F1)イベント生成
            if (this.setDensiSeikyushoVisible())
            {
                parentForm.bt_func1.Click += new EventHandler(this.form.CSV);
            }
            else
            {
                parentForm.bt_func1.Enabled = true;
                parentForm.bt_func1.Text = string.Empty;
            }
            // 20160429 koukoukon v2.1_電子請求書 #16612 end

            //プレビューボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.PreView);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //×ボタンで閉じる場合のイベント生成
            parentForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form.UIForm_FormClosing);

            //取引先コード入力欄でロストフォーカスイベント生成
            this.form.TORIHIKISAKI_CD.Leave += new EventHandler(TORIHIKISAKI_CD_Leave);

            //締日プルダウン値変更イベント生成
            this.form.cmbShimebi.TextChanged += new EventHandler(cmbShimebi_TextChanged);

            /// 20141201 Houkakou 「請求書発行」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.headerForm.DenpyouHidukeTo.MouseDoubleClick += new MouseEventHandler(DenpyouHidukeTo_MouseDoubleClick);
            /// 20141201 Houkakou 「請求書発行」のダブルクリックを追加する　end
           
            /// 明細のダブルクリック
            /// #163721 start
            this.form.SeikyuuDenpyouItiran.ColumnHeadersHeightChanged += new EventHandler(SeikyuuDenpyouItiran_ColumnHeadersHeightChanged);
            this.form.SeikyuuDenpyouItiran.ColumnDividerDoubleClick += new DataGridViewColumnDividerDoubleClickEventHandler(SeikyuuDenpyouItiran_ColumnDividerDoubleClick);
            this.form.SeikyuuDenpyouItiran.RowDividerDoubleClick += new DataGridViewRowDividerDoubleClickEventHandler(SeikyuuDenpyouItiran_RowDividerDoubleClick);
            //#163721 end

            LogUtility.DebugMethodEnd();
        }

        //#163721 start
        /// <summary>
        /// RowDividerDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuDenpyouItiran_RowDividerDoubleClick(object sender, DataGridViewRowDividerDoubleClickEventArgs e)
        {
            this.form.SeikyuuDenpyouItiran.Tag = this.form.SeikyuuDenpyouItiran.ColumnHeadersHeight.ToString();
            return;
        }
        /// <summary>
        /// ColumnDividerDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeikyuuDenpyouItiran_ColumnDividerDoubleClick(object sender, DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            this.form.SeikyuuDenpyouItiran.Tag = this.form.SeikyuuDenpyouItiran.ColumnHeadersHeight.ToString();
            return;
        }

        /// <summary>
        /// ColumnHeadersHeightChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SeikyuuDenpyouItiran_ColumnHeadersHeightChanged(object sender, EventArgs e)
        {
            var gridView = sender as CustomDataGridView;
            if (gridView.Tag != null && gridView.Tag != "" && gridView.Tag != "0")
            {
                if (gridView.ColumnHeadersHeight != ConstCls.GridColumHeaderHeight)
                    gridView.ColumnHeadersHeight = ConstCls.GridColumHeaderHeight;
                gridView.Tag = null;
            }
            return;
        }
        //#163721 end

        #endregion 初期化

        #region データ取得
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public virtual int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // SQL文
                this.SearchResult = new DataTable();
                if (this.headerForm.DenpyouHidukeFrom.Value == null)
                {
                    this.SearchString.DenpyoHizukeFrom = string.Empty;
                }
                else
                {
                    this.SearchString.DenpyoHizukeFrom = DateTime.Parse(this.headerForm.DenpyouHidukeFrom.Value.ToString()).ToShortDateString();
                }
                if (this.headerForm.DenpyouHidukeTo.Value == null)
                {
                    this.SearchString.DenpyoHizukeTo = string.Empty;
                }
                else
                {
                    this.SearchString.DenpyoHizukeTo = DateTime.Parse(this.headerForm.DenpyouHidukeTo.Value.ToString()).ToShortDateString();
                }
                this.SearchString.HakkouKyotenCD = this.headerForm.USER_KYOTEN_CD.Text;
                this.SearchString.Simebi = this.form.cmbShimebi.Text;
                this.SearchString.PrintOrder = int.Parse(this.form.PRINT_ORDER.Text);
                this.SearchString.SeikyuPaper = int.Parse(this.form.SEIKYU_PAPER.Text);
                this.SearchString.TorihikisakiCD = this.form.TORIHIKISAKI_CD.Text;

                SqlBoolean hakkoKbn = SqlBoolean.Null;
                if (ConstCls.HAKKOU_KBN_MIHAKKOU.Equals(this.form.HAKKOU_KBN.Text))
                {
                    hakkoKbn = SqlBoolean.False;
                }
                else if (ConstCls.HAKKOU_KBN_HAKKOUZUMI.Equals(this.form.HAKKOU_KBN.Text))
                {
                    hakkoKbn = SqlBoolean.True;
                }
                else if (ConstCls.HAKKOU_KBN_SUBETE.Equals(this.form.HAKKOU_KBN.Text))
                {
                    hakkoKbn = SqlBoolean.Null;
                }
                this.SearchString.HakkoKbn = hakkoKbn;

                // 20160429 koukoukon v2.1_電子請求書 #16612 start
                this.SearchString.OutputKbn = int.Parse(this.form.OUTPUT_KBN.Text);
                // 20160429 koukoukon v2.1_電子請求書 #16612 end
                int filteringData = 0;
                int.TryParse(this.form.FILTERING_DATA.Text, out filteringData);
                this.SearchString.FilteringData = filteringData;

                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho())
                {
                    this.SearchString.UseInxsSeikyuuKbn = true; //[取引先入力][INXS請求区分] = ２．しない
                }
                // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                this.SearchString.ZeroKingakuTaishogai = this.form.ZERO_KINGAKU_TAISHOGAI.Checked;//VAN 20201125 #136235

                this.SearchResult = SeikyuDenpyouDao.GetDataForEntity(this.SearchString);
                int Count = this.SearchResult.Rows.Count;

                // 読み込み件数の設定
                this.headerForm.YOMIKOMI_COUNT.Text = string.Format("{0:#,0}", Convert.ToDecimal(Count.ToString()));

                if (Count == 0)
                {
                    errMsg.MessageBoxShow(ConstCls.ERR_MSG_CD_C001);
                }

                LogUtility.DebugMethodEnd(Count);
                return Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errMsg.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errMsg.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }
        #endregion データ取得

        #region データ表示
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //前の結果をクリア
                int k = this.form.SeikyuuDenpyouItiran.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.SeikyuuDenpyouItiran.Rows.RemoveAt(this.form.SeikyuuDenpyouItiran.Rows[i - 1].Index);
                }


                //検索結果を設定する
                var table = this.SearchResult;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.SeikyuuDenpyouItiran.Rows.Add();
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["HAKKOU"].Value = false;
                    // 20160429 koukoukon v2.1_電子請求書 #16612 start
                    // 20211207 thucp v2.24_電子請求書 #157799 begin
                    //this.form.SeikyuuDenpyouItiran.Rows[i].Cells["DETAIL_OUTPUT_KBN"].Value = false;
                    form.SeikyuuDenpyouItiran.Rows[i].Cells["DETAIL_OUTPUT_KBN"].Value = Convert.ToBoolean(table.Rows[i]["DETAIL_OUTPUT_KBN"]);
                    form.SeikyuuDenpyouItiran.Rows[i].Cells["RAKURAKU_CSV_OUTPUT"].Value = Convert.ToBoolean(table.Rows[i]["RAKURAKU_CSV_OUTPUT"]);
                    // 20211207 thucp v2.24_電子請求書 #157799 end
                    // 20160429 koukoukon v2.1_電子請求書 #16612 end
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["HAKKOUZUMI"].Value = table.Rows[i]["HAKKOU_KBN"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["SEIKYUU_NUMBER"].Value = table.Rows[i]["SEIKYUU_NUMBER"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["SEIKYUU_DATE"].Value = table.Rows[i]["SEIKYUU_DATE"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["TORIHIKISAKICD"].Value = table.Rows[i]["TORIHIKISAKI_CD"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["TORIHIKISAKINAME"].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["SHIMEBI2"].Value = table.Rows[i]["SHIMEBI"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["ZENKAI_KURIKOSI_GAKU"].Value = table.Rows[i]["ZENKAI_KURIKOSI_GAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["KONKAI_NYUUKIN_GAKU"].Value = table.Rows[i]["KONKAI_NYUUKIN_GAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["KONKAI_CHOUSEI_GAKU"].Value = table.Rows[i]["KONKAI_CHOUSEI_GAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["KONKAI_URIAGE_GAKU"].Value = table.Rows[i]["KONKAI_URIAGE_GAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["SHOHIZEI_GAKU"].Value = table.Rows[i]["SHOHIZEI_GAKU"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["KONKAI_SEIKYU_GAKU"].Value = table.Rows[i]["KONKAI_SEIKYU_GAKU"];
                    if (string.IsNullOrEmpty(table.Rows[i]["NYUUKIN_YOTEI_BI"].ToString()))
                    {
                        this.form.SeikyuuDenpyouItiran.Rows[i].Cells["NYUUKIN_YOTEI_BI"].Value = string.Empty;
                    }
                    else
                    {
                        this.form.SeikyuuDenpyouItiran.Rows[i].Cells["NYUUKIN_YOTEI_BI"].Value = DateTime.Parse(table.Rows[i]["NYUUKIN_YOTEI_BI"].ToString()).ToShortDateString();
                    }
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["colTimeStamp"].Value = table.Rows[i]["TIME_STAMP"];
                    this.form.SeikyuuDenpyouItiran.Rows[i].Cells["HAKKOU"].ToolTipText = ConstCls.ToolTipText1;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion データ表示

        #region データ更新

        /// <summary>
        /// 請求伝票テーブルUPDATE
        /// </summary>
        [Transaction]
        public static void UpdateT_SEIKYUU_DENPYOU(TSDDaoCls dao, T_SEIKYUU_DENPYOU val)
        {
            try
            {
                using (Transaction tran = new Transaction())
                {
                    dao.Update(val);
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    throw;
                }
            }

        }

        #endregion データ更新

        #region データ登録
        /// <summary>
        /// 締処理エラーテーブルUPDATEデータ作成
        /// </summary>
        public bool Regist()
        {
            bool ret = true;
            try
            {
                // 明細に何もなければエラー
                if (this.form.SeikyuuDenpyouItiran.Rows.Count <= 0)
                {
                    this.errMsg.MessageBoxShow("E061");
                    return ret;
                }

                if (this.errMsg.MessageBoxShow("C055", "登録") == DialogResult.Yes)
                {
                    // 削除されていない全てのEntityをDBから取得
                    var allDenpyou = this.SeikyuDenpyouDao.GetAllData().Where(e => e.DELETE_FLG.IsFalse).ToList();

                    // DGVの請求番号List取得
                    var dgvSeikyuuNumberList = this.form.SeikyuuDenpyouItiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["SEIKYUU_NUMBER"].Value).ToList();

                    // 請求番号ListをEntityのListにする
                    var deffEntitylist = new List<T_SEIKYUU_DENPYOU>();
                    dgvSeikyuuNumberList.ForEach(n => deffEntitylist.Add(new T_SEIKYUU_DENPYOU() { SEIKYUU_NUMBER = (Int64)n }));

                    // DGVに表示されている請求伝票のEntityを取得
                    var updateEntityList = allDenpyou.Where(d => d.DELETE_FLG.IsFalse).Intersect(deffEntitylist, new SeikyuuDenpyouPropComparer()).ToList();

                    // 発行済チェックが更新されていなければ登録対象外
                    foreach (DataGridViewRow row in this.form.SeikyuuDenpyouItiran.Rows)
                    {
                        updateEntityList.Remove(updateEntityList
                            .Where(w => w.SEIKYUU_NUMBER.Value.Equals(row.Cells["SEIKYUU_NUMBER"].Value)
                                && w.HAKKOU_KBN.Value.Equals(row.Cells["HAKKOUZUMI"].Value)).FirstOrDefault());
                    }

                    // 発行済チェックに更新のあった行だけ登録
                    updateEntityList.ForEach(f => f.HAKKOU_KBN = !f.HAKKOU_KBN);
                    //set 最終更新日、更新者、更新ＰＣ information
                    foreach (T_SEIKYUU_DENPYOU d in updateEntityList)
                    {
                        var databind = new DataBinderLogic<T_SEIKYUU_DENPYOU>(d);
                        databind.SetSystemProperty(d, false);
                    }
                    updateEntityList.ForEach(u => UpdateT_SEIKYUU_DENPYOU(this.SeikyuDenpyouDao, u));
                    this.errMsg.MessageBoxShow("I001", "登録");
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.errMsg.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.errMsg.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion データ登録

        #region [F5]プレビューボタン押下
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        //
        public virtual void PreView()
        {
            try
            {
                fileLink = fileLinkSysInfoDao.GetDataById("0");

                if (fileLink != null)
                {
                    // ファイルIDからファイル情報を取得
                    long fileId = (long)fileLink.FILE_ID;
                    fileData = fileDataDao.GetDataByKey(fileId);

                    // ファイルパスにファイルが存在しない場合
                    if (!(File.Exists(fileData.FILE_PATH)))
                    {
                        // ユーザ定義情報を取得
                        Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile userProfile = Shougun.Core.Common.BusinessCommon.Xml.CurrentUserCustomConfigProfile.Load();

                        // ファイルアップロード参照先のフォルダを取得
                        string folderPath = uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                        //システム個別設定入力の初期フォルダの設定有無をチェックする。
                        if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                        {
                            MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
                            errmessage.MessageBoxShowError("システム個別設定入力 - ファイルアップロード - 初期フォルダへ\r\nフォルダ情報を入力してください。");
                            return;
                        }
                    }
                }

                //請求書指定日が4.指定の場合は、指定日の未入力チェックを行う
                if (this.form.SEIKYUSHO_PRINTDAY.Text == ConstCls.SEIKYU_PRINT_DAY_SITEI &&
                    this.form.cdtSiteiPrintHiduke.Value == null)
                {
                    msgLogic.MessageBoxShow("E012", "指定日");

                    this.form.cdtSiteiPrintHiduke.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.HIKAE_OUTPUT_KBN.Text))
                {
                    msgLogic.MessageBoxShow("E001", "請求(控)印刷");
                    this.form.HIKAE_OUTPUT_KBN.Focus();
                }
                else
                {
                    SeikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();


                    //発行チェックボックスONカウント
                    int hakkouCnt = 0;

                    //請求書発行用DTO作成
                    SeikyuuDenpyouDto dto = new SeikyuuDenpyouDto();
                    dto.MSysInfo = this.mSysInfo;
                    //dto.Meisai = this.form.MEISAI.Text;   // No.4004
                    dto.SeikyuHakkou = this.form.SEIKYU_HAKKOU.Text;
                    dto.SeikyushoPrintDay = this.form.SEIKYUSHO_PRINTDAY.Text;
                    dto.HakkoBi = this.strSystemDate;
                    if (this.form.cdtSiteiPrintHiduke.Value != null)
                    {
                        dto.SeikyuDate = (DateTime)this.form.cdtSiteiPrintHiduke.Value;
                    }
                    dto.SeikyuStyle = this.form.SEIKYU_STYLE.Text;
                    dto.SeikyuPaper = this.form.SEIKYU_PAPER.Text;

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return;
                    }
                    bool isAbortRequired = false;
                    int printCount = 0;

                    //請求(控)印刷
                    //グループ印刷(HIKAE_OUTPUT_GROUP) →請求書発行処理を2回回す（請求書全部→控え全部）
                    //ソート印刷(HIKAE_OUTPUT_SORT)　→請求書と控えを交互で印刷する
                    //控えを印刷しない(HIKAE_OUTPUT_NON)　→請求書のみを印刷
                    int hikae_count = 1;
                    if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_GROUP)
                    {
                        hikae_count = 0;
                    }

                    for (int i = hikae_count; i <= 1; i++)
                    {
                        //グリッドの発行列にチェックが付いているデータのみ処理を行う
                        foreach (DataGridViewRow row in this.form.SeikyuuDenpyouItiran.Rows)
                        {
                            if ((bool)row.Cells["HAKKOU"].Value == true)
                            {
                                hakkouCnt++;

                                DataTable dt = new DataTable();
                                dt.Columns.Add();

                                //
                                dto.TorihikisakiCd = row.Cells["TORIHIKISAKICD"].Value.ToString();

                                //印刷用データを取得
                                //G102請求書確認の処理を参考
                                //精算番号
                                string seikyuNumber = row.Cells["SEIKYUU_NUMBER"].Value.ToString();

                                //請求伝票を取得
                                T_SEIKYUU_DENPYOU tseikyudenpyou = SeikyuDenpyouDao.GetDataByCd(seikyuNumber);
                                //書式区分
                                string shoshikiKbn = tseikyudenpyou.SHOSHIKI_KBN.ToString();
                                //書式明細区分
                                string shoshikiMeisaiKbn = tseikyudenpyou.SHOSHIKI_MEISAI_KBN.ToString();

                                //入金明細区分 (請求携帯：2.単月請求の場合は、入金明細なしで固定)
                                string nyuukinMeisaiKbn = "2";
                                if (this.form.SEIKYU_STYLE.Text != "2")
                                {
                                    nyuukinMeisaiKbn = tseikyudenpyou.NYUUKIN_MEISAI_KBN.ToString(); // No.4004
                                }
                                dto.Meisai = nyuukinMeisaiKbn;   // No.4004

                                //請求伝票データ取得
                                DataTable seikyuDt = GetSeikyudenpyo(this.SeikyuDenpyouDao, seikyuNumber, shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn, false, this.SearchString.ZeroKingakuTaishogai);
                                this.form.SeikyuDt = seikyuDt;

                                if (seikyuDt.Rows.Count != 0)
                                {
                                    //請求伝票データ設定
                                    //printCount = SetSeikyuDenpyo(seikyuDt, dto, report_r336, true);
                                    var result = new ArrayList();
                                    if (seikyuDt.Rows[0]["INVOICE_KBN"].ToString() == "2")
                                    {
                                        //適格請求書
                                        result = SetSeikyuDenpyo_invoice(seikyuDt, dto, true, false, "", this.headerForm.ZeiRate_Chk.Checked);
                                        if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_SORT)
                                        {
                                            result = SetSeikyuDenpyo_invoice(seikyuDt, dto, true, false, "", this.headerForm.ZeiRate_Chk.Checked);
                                        }
                                    }
                                    else
                                    {
                                        //旧請求書式
                                        result = SetSeikyuDenpyo(seikyuDt, dto, true);
                                        if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_SORT)
                                        {
                                            result = SetSeikyuDenpyo(seikyuDt, dto, true);
                                        }
                                    }
                                    printCount = (int)result[0];
                                }

                                // 印刷画面から中止要求があれば中断
                                if (ContinuousPrinting.IsAbortRequired)
                                {
                                    isAbortRequired = true;
                                    break;
                                }
                            }
                        }
                    }

                    // 印刷シーケンスの終了
                    ContinuousPrinting.End(isAbortRequired);

                    //発行チェックボックスがすべてOFFの場合はメッセージ表示
                    if (hakkouCnt == 0)
                    {
                        msgLogic.MessageBoxShow("E050", "請求書発行");
                        return;
                    }

                    //発行対象データが0件の場合はメッセージ表示
                    if (printCount == 0)
                    {
                        msgLogic.MessageBoxShow("I008", "請求書");
                    }
                    else if (!isAbortRequired)
                    {
                        foreach (DataGridViewRow row in this.form.SeikyuuDenpyouItiran.Rows)
                        {
                            if ((bool)row.Cells["HAKKOU"].Value == true)
                            {
                                //発行区分を更新
                                DataTable seikyuDenpyo = UpdateSeikyuDenpyouHakkouKbn(SeikyuDenpyouDao, row.Cells["SEIKYUU_NUMBER"].Value.ToString(), (byte[])row.Cells["colTimeStamp"].Value);

                                if (seikyuDenpyo != null && seikyuDenpyo.Rows.Count > 0)
                                {
                                    //発行済みチェックをON
                                    row.Cells["HAKKOUZUMI"].Value = seikyuDenpyo.Rows[0]["HAKKOU_KBN"];
                                    //タイムスタンプを設定
                                    row.Cells["colTimeStamp"].Value = seikyuDenpyo.Rows[0]["TIME_STAMP"];
                                }
                            }
                        }
                        //グリッドを再描画
                        this.form.SeikyuuDenpyouItiran.Refresh();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PreView", ex1);
                this.errMsg.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("PreView", ex);
                this.errMsg.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// レポートフォーム作成
        /// </summary>
        /// <param name="dto">請求書発行用DTO</param>
        /// <param name="aryPrint">帳票出力用データリスト</param>
        /// <returns></returns>
        public static FormReport CreateFormReport(SeikyuuDenpyouDto dto, ArrayList aryPrint)
        {
            FormReport formReport = null;

            // 現状では指定用紙がないが、条件判定だけ実装しておく
            if (dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SAKUSEIJI_JISYA
                    || dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_JISYA)
            {
                ReportInfoR336[] reportInfo = (ReportInfoR336[])aryPrint.ToArray(typeof(ReportInfoR336));
                formReport = new FormReport(reportInfo, "R336");
                formReport.Caption = "請求書";
            }
            else if (dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SAKUSEIJI_SHITEI
                         || dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SHITEI)
            {
                ReportInfoR336[] reportInfo = (ReportInfoR336[])aryPrint.ToArray(typeof(ReportInfoR336));
                formReport = new FormReport(reportInfo, "R336");
                formReport.Caption = "請求書";
            }

            return formReport;
        }

        /// <summary>
        /// 請求伝票の発行区分更新
        /// </summary>
        /// <param name="dao">請求伝票DAO</param>
        /// <param name="seikyuNumber">請求番号</param>
        /// <param name="timeStamp">請求伝票のタイムスタンプ</param>
        /// <returns></returns>
        public static DataTable UpdateSeikyuDenpyouHakkouKbn(TSDDaoCls dao, string seikyuNumber, byte[] timeStamp)
        {
            T_SEIKYUU_DENPYOU seikyuuentitys = dao.GetDataByCd(seikyuNumber);
            //発行区分を更新
            if (seikyuuentitys != null && !seikyuuentitys.DELETE_FLG.IsTrue)
            {
                seikyuuentitys.HAKKOU_KBN = true;
                var dataBinderEntry = new DataBinderLogic<T_SEIKYUU_DENPYOU>(seikyuuentitys);
                dataBinderEntry.SetSystemProperty(seikyuuentitys, false);
                UpdateT_SEIKYUU_DENPYOU(dao, seikyuuentitys);
            }
            //タイムスタンプを再取得
            DataTable seikyuDenpyo = dao.GetSeikyudenpyoUpdateData(seikyuNumber);

            return seikyuDenpyo;
        }

        /// <summary>
        /// 請求伝票データ取得
        /// </summary>
        /// <param name="dao">請求伝票DAO</param>
        /// <param name="seikyuNumber">請求番号</param>
        /// <param name="shoshikiKbn">書式区分</param>
        /// <param name="shoshikiMeisaiKbn">書式明細区分</param>
        /// <param name="nyuukinMeisaiKbn">入金明細区分</param>
        /// <returns></returns>
        public static DataTable GetSeikyudenpyo(TSDDaoCls dao, string seikyuNumber, string shoshikiKbn, string shoshikiMeisaiKbn, string nyuukinMeisaiKbn, bool IsCsvFlg = false, bool IsZeroKingakuTaishogai = false)
        {
            //①T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが1：請求先別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            //②T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが2：業者別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            string orderBy = " ";
            if ((ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                    && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                || (ConstCls.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                    && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.TSDE_GYOUSHA_CD , TSDKE.TSDE_GENBA_CD ";
            }
            //①T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが1：請求先別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ２：業者毎
            else if (ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                && (ConstCls.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.TSDE_GYOUSHA_CD ";
            }

            DataTable seikyuDt;

            if (nyuukinMeisaiKbn == ConstCls.NYUKIN_MEISAI_ARI)
            {
                seikyuDt = dao.GetSeikyudenpyo(seikyuNumber, nyuukinMeisaiKbn, orderBy, IsCsvFlg, IsZeroKingakuTaishogai);
            }
            else
            {
                seikyuDt = dao.GetSeikyudenpyoMeisaiNashi(seikyuNumber, nyuukinMeisaiKbn, orderBy, shoshikiKbn, IsCsvFlg, IsZeroKingakuTaishogai);

                if (seikyuDt.Rows.Count > 0)
                {
                    // 入金のみ締めの場合はデータを表示。それ以外は検索条件に合わせて絞り込む。
                    DataRow[] tempRow = seikyuDt.Select("DENPYOU_SHURUI_CD IS NULL");
                    if (tempRow != null && (seikyuDt.Rows.Count == tempRow.Length))
                    {
                        // 入金のみ締め
                    }
                    else
                    {
                        // 各種伝票締め
                        if (!shoshikiKbn.Equals("1") && nyuukinMeisaiKbn.Equals("2"))
                        {
                            seikyuDt = seikyuDt.Select("TSDK_GYOUSHA_CD <> ''").CopyToDataTable();
                        }
                    }
                }
            }

            return seikyuDt;
        }


        /// <summary>
        /// 請求伝票データテーブル設定
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateSeikyuuPrintTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("GROUP_NAME");
            table.Columns.Add("DENPYOU_DATE");
            table.Columns.Add("DENPYOU_NUMBER");
            table.Columns.Add("SHARYOU_NAME");
            table.Columns.Add("HINMEI_NAME");
            table.Columns.Add("SUURYOU");
            table.Columns.Add("UNIT_CD");
            table.Columns.Add("UNIT_NAME");
            table.Columns.Add("TANKA");
            table.Columns.Add("KINGAKU");
            table.Columns.Add("SHOUHIZEI");
            table.Columns.Add("MEISAI_BIKOU");
            table.Columns.Add("DATA_KBN");
            return table;
        }

        /// <summary>
        /// 請求伝票データ設定
        /// </summary>
        /// <param name="seikyuDt">請求伝票データテーブル</param>
        /// <param name="dto">請求書発行DTO</param>
        public static ArrayList SetSeikyuDenpyo(DataTable seikyuDt, SeikyuuDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "")
        {
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            List<KagamiFileExportDto> kagamiFileExportList = null;
            if (isExportPDF)
            {
                kagamiFileExportList = new List<KagamiFileExportDto>();
            }
            ArrayList result = new ArrayList();
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
            int count = 0;
            ArrayList list;
            FormReport formReport;

            ReportInfoR336 reportR336;
            DataTable denpyouPrintTable = CreateSeikyuuPrintTable();
            DataTable nyuukinPrintTable = CreateSeikyuuPrintTable();

            //先頭行の鏡番号を取得
            DataRow startRow = seikyuDt.Rows[0];

            List<DataTable> csvDtList = new List<DataTable>();
            
            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.NYUKIN_MEISAI_NASHI && string.IsNullOrEmpty(startRow["KAGAMI_NUMBER"].ToString()))
            {
                #region dto.Meisai = NYUKIN_MEISAI_NASHI
                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR336 = new ReportInfoR336();
                reportR336.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                reportR336.Title = "請求書()";
                // XPSプロパティ - 発行済み
                reportR336.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                var drEmpty = denpyouPrintTable.NewRow();
                denpyouPrintTable.Rows.Add(drEmpty);
                
                if (printFlg)
                {
                    reportR336.CreateReportData(dto, startRow,denpyouPrintTable, nyuukinPrintTable);

                    /* 即時XPS出力 */
                    list = new ArrayList();
                    list.Add(reportR336);
                    formReport = CreateFormReport(dto, list);

                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    //// 印刷アプリ初期動作(プレビュー)
                    //formReport.PrintInitAction = 2;
                    //formReport.PrintXPS();

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2; ;//add Direct Print option refs #158002
                        formReport.PrintXPS();
                    }
                    else
                    {
                        //INXS請求書アップロード G745 INXS請求書発行 START   
                        //ExportPDF
                        string pdfFileName = string.Empty;
                        if (ConstCls.SHOSHIKI_KBN_1 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}", startRow["SEIKYUU_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TSDK_TORIHIKISAKI_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_2 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}", startRow["SEIKYUU_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TSDK_TORIHIKISAKI_CD"].ToString(), startRow["TSDK_GYOUSHA_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_3 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", startRow["SEIKYUU_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TSDK_TORIHIKISAKI_CD"].ToString(), startRow["TSDK_GYOUSHA_CD"].ToString(), startRow["TSDK_GENBA_CD"].ToString());
                        }

                        //直接印刷
                        string fileExport = formReport.ExportPDF(pdfFileName, path);

                        KagamiFileExportDto kagamiFileExport = new KagamiFileExportDto()
                        {
                            KagamiNumber = Convert.ToInt32(startRow["KAGAMI_NUMBER"]),
                            FileExport = fileExport
                        };
                        kagamiFileExportList.Add(kagamiFileExport);
                        //INXS請求書アップロード G745 INXS請求書発行 END
                    }
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR336.CreateCsvData(dto, startRow, denpyouPrintTable, nyuukinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
                #endregion
            }
            else
            {
                #region dto.Meisai != NYUKIN_MEISAI_NASHI
                var isSeikyuuUchizei = false;
                var isSeikyuuSotozei = false;

                int nowKagamiNo = Convert.ToInt32(startRow["KAGAMI_NUMBER"]);
                
                for (int i = 0; i < seikyuDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = seikyuDt.Rows[i];

                    //鏡番号が同じか
                    if (Convert.ToInt32(tableRow["KAGAMI_NUMBER"]) != nowKagamiNo) // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338
                    {
                        //帳票出力データテーブルを帳票出力データArrayListに格納
                        reportR336 = new ReportInfoR336();
                        reportR336.MSysInfo = dto.MSysInfo;

                        // XPSプロパティ - タイトル(取引先も表示させる)
                        DataRow[] rows = seikyuDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                        reportR336.Title = "請求書(" + rows[0]["SEIKYUU_SOUFU_NAME1"] + ")";
                        // XPSプロパティ - 発行済み
                        reportR336.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定
                        
                        if (printFlg)
                        {
                            reportR336.CreateReportData(dto, rows[0], denpyouPrintTable, nyuukinPrintTable);

                            // 即時XPS出力
                            list = new ArrayList();
                            list.Add(reportR336);
                            formReport = CreateFormReport(dto, list);

                            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                            
                            //// 印刷アプリ初期動作(プレビュー)
                            //formReport.PrintInitAction = 2;
                            //formReport.PrintXPS();

                            if (!isExportPDF)
                            {
                                // 印刷アプリ初期動作(プレビュー)
                                formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add Direct Print option refs #158002
                                formReport.PrintXPS();
                            }
                            else
                            {
                                //ExportPDF
                                string pdfFileName = string.Empty;
                                if (ConstCls.SHOSHIKI_KBN_1 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}", rows[0]["SEIKYUU_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TSDK_TORIHIKISAKI_CD"].ToString());
                                }
                                else if (ConstCls.SHOSHIKI_KBN_2 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}_{3}", rows[0]["SEIKYUU_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TSDK_TORIHIKISAKI_CD"].ToString(), rows[0]["TSDK_GYOUSHA_CD"].ToString());
                                }
                                else if (ConstCls.SHOSHIKI_KBN_3 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", rows[0]["SEIKYUU_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TSDK_TORIHIKISAKI_CD"].ToString(), rows[0]["TSDK_GYOUSHA_CD"].ToString(), rows[0]["TSDK_GENBA_CD"].ToString());
                                }
                                //直接印刷
                                string fileExport = formReport.ExportPDF(pdfFileName, path);
                                KagamiFileExportDto kagamiFileExport = new KagamiFileExportDto()
                                {
                                    KagamiNumber = nowKagamiNo,
                                    FileExport = fileExport
                                };
                                kagamiFileExportList.Add(kagamiFileExport);
                            }
                            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
                        }
                        else
                        {
                            DataTable dtData = reportR336.CreateCsvData(dto, rows[0], denpyouPrintTable, nyuukinPrintTable);
                            csvDtList.Add(dtData);
                        }

                        count++;

                        //帳票出力データテーブルを初期化
                        denpyouPrintTable = CreateSeikyuuPrintTable();
                        nyuukinPrintTable = CreateSeikyuuPrintTable();

                        nowKagamiNo = Convert.ToInt16(tableRow["KAGAMI_NUMBER"]);

                        isSeikyuuUchizei = false;
                        isSeikyuuSotozei = false;
                    }

                    //現在行の前の行
                    DataRow tablePevRow = null;
                    if (i == 0)
                    {
                        //現在行の前の行
                        tablePevRow = null;
                    }
                    else
                    {
                        //現在行の前の行
                        tablePevRow = seikyuDt.Rows[i - 1];
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (i == seikyuDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = seikyuDt.Rows[i + 1];
                    }

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString()))
                    {
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_UCHI == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeikyuuUchizei = true;
                        }
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_SOTO == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeikyuuSotozei = true;
                        }

                        var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                        var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();
                        var shoshikiGenbaKubun = startRow["SHOSHIKI_GENBA_KBN"].ToString();

                        // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
                        bool isNyuukin = tableRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

                        // 業者名設定
                        if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                        {
                            if (printFlg)
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                                {
                                    var drGyousha = denpyouPrintTable.NewRow();
                                    drGyousha["GROUP_NAME"] = tableRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"].ToString();
                                    drGyousha["DATA_KBN"] = "GYOUSHA_GROUP";
                                    denpyouPrintTable.Rows.Add(drGyousha);
                                }

                            }
                            else
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                                {
                                    var drGyousha = denpyouPrintTable.NewRow();
                                    drGyousha["GROUP_NAME"] = tableRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"].ToString();
                                    drGyousha["DATA_KBN"] = "GYOUSHA_GROUP";
                                    denpyouPrintTable.Rows.Add(drGyousha);
                                }
                            }
                        }
                        // 現場名設定
                        if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                        {
                            // 現場名
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_3 == shoshikiKubun && ConstCls.SHOSHIKI_GENBA_KBN_1 == shoshikiGenbaKubun && !isNyuukin))
                            {
                                var drGenba = denpyouPrintTable.NewRow();
                                drGenba["GROUP_NAME"] = tableRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GENBA_NAME2"].ToString();
                                drGenba["DATA_KBN"] = "GENBA_GROUP";
                                denpyouPrintTable.Rows.Add(drGenba);
                            }
                        }

                        // 精算伝票明細データ設定
                        // ☆☆☆2-3☆☆☆
                        if (isNyuukin)
                        {
                            SetSeikyuuDenpyoMeisei(tableRow, tablePevRow, nyuukinPrintTable, tableNextRow, printFlg);
                        }
                        else
                        {
                            SetSeikyuuDenpyoMeisei(tableRow, tablePevRow, denpyouPrintTable, tableNextRow, printFlg);
                        }

                        // 現場金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                        {
                            // 現場金額と消費税
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                            {
                                var drGenbaGokei = denpyouPrintTable.NewRow();
                                drGenbaGokei["HINMEI_NAME"] = "現場計";
                                drGenbaGokei["KINGAKU"] = tableRow["GENBA_KINGAKU_1"].ToString();
                                drGenbaGokei["DATA_KBN"] = "GENBA_TOTAL";
                                decimal denpyouSotoZeiGaku;
                                decimal denpyouUchiZeiGaku;
                                GetDenpyouZei(seikyuDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), tableRow["TSDE_GENBA_CD"].ToString(), out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, true);

                                // 現場計：消費税(外税)
                                decimal genbaSoto = Convert.ToDecimal(tableRow["GENBA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                if (genbaSoto != 0)
                                {
                                    drGenbaGokei["SHOUHIZEI"] = genbaSoto;
                                }

                                // 現場計：備考(内税)
                                decimal genbaUchi = Convert.ToDecimal(tableRow["GENBA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                string biko = string.Empty;
                                if (genbaUchi != 0)
                                {
                                    biko = GetSyohizei(genbaUchi.ToString(), ConstCls.DENPYOU_ZEI_KBN_CD_2);
                                }

                                if (IsSeikyuData(tableRow))
                                {
                                    if (string.IsNullOrEmpty(biko))
                                    {
                                        if (genbaSoto != 0 || genbaUchi != 0)
                                        {
                                            biko = ConstCls.SEIKYU_ZEI_EXCEPT;
                                        }
                                    }
                                    else
                                    {
                                        biko += (ConstCls.ZENKAKU_SPACE + ConstCls.SEIKYU_ZEI_EXCEPT);
                                    }
                                }
                                drGenbaGokei["MEISAI_BIKOU"] = biko;
                                denpyouPrintTable.Rows.Add(drGenbaGokei);
                            }
                        }

                        // 業者金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"]))
                        {
                            // 業者金額と消費税
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票)
                            // ※入金計項目は業者計を使用しているが、業者計or現場計を出力する場合は入金計を出力するため
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                            {
                                var drGyoushaGokei = denpyouPrintTable.NewRow();
                                drGyoushaGokei["HINMEI_NAME"] = "業者計";
                                drGyoushaGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drGyoushaGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                decimal denpyouSotoZeiGaku;
                                decimal denpyouUchiZeiGaku;
                                GetDenpyouZei(seikyuDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), null, out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, false);

                                // 業者計：消費税(外税)
                                decimal gyoushaSoto = Convert.ToDecimal(tableRow["GYOUSHA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                if (gyoushaSoto != 0)
                                {
                                    drGyoushaGokei["SHOUHIZEI"] = gyoushaSoto;
                                }

                                // 業者計：備考(内税)
                                decimal gyoushaUchi = Convert.ToDecimal(tableRow["GYOUSHA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                string biko = string.Empty;
                                if (gyoushaUchi != 0)
                                {
                                    biko = GetSyohizei(gyoushaUchi.ToString(), ConstCls.DENPYOU_ZEI_KBN_CD_2);
                                }

                                if (IsSeikyuData(tableRow))
                                {
                                    if (string.IsNullOrEmpty(biko))
                                    {
                                        if (gyoushaSoto != 0 || gyoushaUchi != 0)
                                        {
                                            biko = ConstCls.SEIKYU_ZEI_EXCEPT;
                                        }
                                    }
                                    else
                                    {
                                        biko += (ConstCls.ZENKAKU_SPACE + ConstCls.SEIKYU_ZEI_EXCEPT);
                                    }
                                }
                                drGyoushaGokei["MEISAI_BIKOU"] = biko;

                                denpyouPrintTable.Rows.Add(drGyoushaGokei);
                            }
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && isNyuukin)
                               || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isNyuukin)
                               || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isNyuukin))
                            {
                                var drNyuukinGokei = nyuukinPrintTable.NewRow();
                                drNyuukinGokei["HINMEI_NAME"] = "入金計";
                                drNyuukinGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drNyuukinGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                nyuukinPrintTable.Rows.Add(drNyuukinGokei);
                            }
                        }

                        if (tableNextRow == null || !tableRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"]))
                        {
                            //請求毎消費税(内)
                            decimal seikyuUchizei1 = 0;
                            if (tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] != null)
                            {
                                seikyuUchizei1 = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
                            }
                            if (isSeikyuuUchizei)
                            {
                                var drSeikyuUchizei = denpyouPrintTable.NewRow();
                                drSeikyuUchizei["HINMEI_NAME"] = "【請求毎消費税(内)】";
                                drSeikyuUchizei["SHOUHIZEI"] = Const.ConstCls.KAKKO_START + SetComma((tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]).ToString()) + Const.ConstCls.KAKKO_END;
                                denpyouPrintTable.Rows.Add(drSeikyuUchizei);
                            }

                            //請求毎消費税(外)
                            decimal seikyuSotozei1 = 0;
                            if (tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] != null)
                            {
                                seikyuSotozei1 = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);
                            }
                            if (isSeikyuuSotozei)
                            {
                                var drSeikyuSotozei = denpyouPrintTable.NewRow();
                                drSeikyuSotozei["HINMEI_NAME"] = "【請求毎消費税】";
                                drSeikyuSotozei["SHOUHIZEI"] = tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"];
                                denpyouPrintTable.Rows.Add(drSeikyuSotozei);
                            }
                        }
                    }
                    else
                    {
                        var drEmpty = denpyouPrintTable.NewRow();
                        denpyouPrintTable.Rows.Add(drEmpty);
                    }
                }

                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR336 = new ReportInfoR336();
                reportR336.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                DataRow[] dataRows = seikyuDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                reportR336.Title = "請求書(" + dataRows[0]["SEIKYUU_SOUFU_NAME1"] + ")";
                // XPSプロパティ - 発行済み
                reportR336.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                if (printFlg)
                {
                    reportR336.CreateReportData(dto, dataRows[0],denpyouPrintTable, nyuukinPrintTable);
                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR336);
                    formReport = CreateFormReport(dto, list);

                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    //// 印刷アプリ初期動作(プレビュー)
                    //formReport.PrintInitAction = 2;
                    //formReport.PrintXPS();

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add Direct Print option refs #158002
                        formReport.PrintXPS();
                    }
                    else
                    {
                        //ExportPDF
                        string pdfFileName = string.Empty;
                        if (ConstCls.SHOSHIKI_KBN_1 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}", dataRows[0]["SEIKYUU_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TSDK_TORIHIKISAKI_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_2 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}", dataRows[0]["SEIKYUU_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TSDK_TORIHIKISAKI_CD"].ToString(), dataRows[0]["TSDK_GYOUSHA_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_3 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", dataRows[0]["SEIKYUU_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TSDK_TORIHIKISAKI_CD"].ToString(), dataRows[0]["TSDK_GYOUSHA_CD"].ToString(), dataRows[0]["TSDK_GENBA_CD"].ToString());
                        }
                        //直接印刷
                        string fileExport = formReport.ExportPDF(pdfFileName, path);
                        KagamiFileExportDto kagamiFileExport = new KagamiFileExportDto()
                        {
                            KagamiNumber = nowKagamiNo,
                            FileExport = fileExport
                        };
                        kagamiFileExportList.Add(kagamiFileExport);
                    }
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR336.CreateCsvData(dto, dataRows[0], denpyouPrintTable, nyuukinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
                #endregion
            }

            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
            //return count;
            result.Add(count);
            if (isExportPDF)
            {
                result.Add(kagamiFileExportList);
            }
            return result;
            // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
        }

        /// <summary>
        /// 現場、業者毎の伝票（内税・外税）額の合計を取得
        /// </summary>
        /// <param name="seikyuDt">請求伝票データ</param>
        /// <param name="tableRow">処理対象の請求伝票行</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denpyouUchiZeiGaku">伝票内税額合計</param>
        /// <param name="denpyouSotoZeiGaku">伝票外税額合計</param>
        /// <param name="isGenba">true:現場計取得, false:業者計取得</param>
        private static void GetDenpyouZei(DataTable seikyuDt, DataRow tableRow, string gyoushaCd, string genbaCd, out decimal denpyouUchiZeiGaku, out decimal denpyouSotoZeiGaku, bool isGenba)
        {
            denpyouUchiZeiGaku = 0;
            denpyouSotoZeiGaku = 0;

            // 業者CDは必須
            if (string.IsNullOrEmpty(gyoushaCd))
            {
                return;
            }

            // DENPYOU_SYSTEM_ID,DENPYOU_NUMBER
            Dictionary<string, string> keys = new Dictionary<string, string>();

            foreach (DataRow dr in seikyuDt.Rows)
            {
                // KAGAMI_NUMBER,TSDE_GYOUSHA_CDで同一レコードを取得
                if (!dr["KAGAMI_NUMBER"].Equals(tableRow["KAGAMI_NUMBER"])
                    || !dr["TSDE_GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    continue;
                }

                // 現場計を取得する場合は、現場CDも一致条件に含める
                if (isGenba && !dr["TSDE_GENBA_CD"].Equals(genbaCd))
                {
                    continue;
                }

                // 伝票外税額や、伝票内税額は伝票毎に同じ値が計上されるため、
                // 合計算出時には１回だけ計上する
                if (keys.ContainsKey(dr["DENPYOU_SYSTEM_ID"].ToString()))
                {
                    continue;
                }

                keys.Add(dr["DENPYOU_SYSTEM_ID"].ToString(), dr["DENPYOU_NUMBER"].ToString());

                denpyouUchiZeiGaku += Convert.ToDecimal(dr["DENPYOU_UCHIZEI_GAKU"]);
                denpyouSotoZeiGaku += Convert.ToDecimal(dr["DENPYOU_SOTOZEI_GAKU"]);
            }
        }

        /// <summary>
        /// 請求毎データ有無
        /// </summary>
        /// <param name="tableRow"></param>
        /// <returns></returns>
        private static bool IsSeikyuData(DataRow tableRow)
        {
            decimal seiUchiZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
            decimal seiSotoZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);

            bool result = 0 < (seiUchiZei + seiSotoZei);

            return result;
        }

        /// <summary>
        /// 請求伝票明細データ設定
        /// </summary>
        /// <param name="tableRow">請求伝票明細データ</param>
        /// <param name="tablePevRow">一行前請求伝票明細データ</param>
        /// <param name="printData">帳票出力用データテーブル</param>
        /// <param name="tableNextRow">一行後請求伝票明細データ</param>
        private static void SetSeikyuuDenpyoMeisei(DataRow tableRow, DataRow tablePevRow, DataTable printData, DataRow tableNextRow, bool printFlg)
        {
            DataRow dataRow;

            dataRow = printData.NewRow();

            if (!printFlg || tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                DateTime dateTime;
                if (tableRow["DENPYOU_DATE"] != null && DateTime.TryParse(tableRow["DENPYOU_DATE"].ToString(), out dateTime))
                {
                    dataRow["DENPYOU_DATE"] = dateTime.ToShortDateString();
                }
                else
                {
                    dataRow["DENPYOU_DATE"] = string.Empty;
                }
                //売上No
                dataRow["DENPYOU_NUMBER"] = tableRow["DENPYOU_NUMBER"].ToString();
            }
            else
            {
                //月日
                dataRow["DENPYOU_DATE"] = string.Empty;
                //売上No
                dataRow["DENPYOU_NUMBER"] = string.Empty;
            }
            //車輛
            dataRow["SHARYOU_NAME"] = tableRow["SHARYOU_NAME"].ToString();

            //品名
            dataRow["HINMEI_NAME"] = tableRow["HINMEI_NAME"].ToString();

            //数量
            if (ConstCls.DENPYOU_SHURUI_CD_10 == tableRow["DENPYOU_SHURUI_CD"].ToString())
            {
                dataRow["SUURYOU"] = string.Empty;
            }
            else
            {
                dataRow["SUURYOU"] = tableRow["SUURYOU"].ToString();
            }

            //単位
            dataRow["UNIT_NAME"] = tableRow["UNIT_NAME"].ToString();


            //単価
            if (ConstCls.DENPYOU_SHURUI_CD_10 == tableRow["DENPYOU_SHURUI_CD"].ToString())
            {
                dataRow["TANKA"] = string.Empty;
            }
            else
            {
                dataRow["TANKA"] = tableRow["TANKA"].ToString();
            }

            //金額
            dataRow["KINGAKU"] = tableRow["KINGAKU"].ToString();

            // 消費税
            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
            var meisai_shouhizei = tableRow["MEISEI_SYOHIZEI"].ToString();
            var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
            var meisai_zei_kbn_cd = tableRow["MEISAI_ZEI_KBN_CD"].ToString();
            var zei_kbn_cd = (ConstCls.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd)) ? denpyou_zei_kbn_cd : meisai_zei_kbn_cd;
            var shouhizei = GetSyohizei(meisai_shouhizei, zei_kbn_cd, true);
            if (ConstCls.DENPYOU_SHURUI_CD_10 == denpyou_shurui_cd)
            {
                // 入金伝票は出力しない
                dataRow["SHOUHIZEI"] = string.Empty;
            }
            else if (ConstCls.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd)
            {
                // 税計算区分が明細毎だったら明細税を出力
                dataRow["SHOUHIZEI"] = shouhizei;
            }
            else if (false == String.IsNullOrEmpty(meisai_zei_kbn_cd) && "0" != meisai_zei_kbn_cd)
            {
                // 伝票毎、請求毎でも明細税区分があれば明細税を出力
                dataRow["SHOUHIZEI"] = shouhizei;
            }
            else if (ConstCls.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd && ConstCls.ZEI_KBN_HIKAZEI == denpyou_zei_kbn_cd)
            {
                // 伝票毎、請求毎ではなく、非課税は 0 を出力
                dataRow["SHOUHIZEI"] = "0";
            }
            else
            {
                // 上記以外は出力しない
                dataRow["SHOUHIZEI"] = string.Empty;
            }

            //備考
            dataRow["MEISAI_BIKOU"] = tableRow["MEISAI_BIKOU"].ToString();

            printData.Rows.Add(dataRow);


            // 伝票毎消費税
            if (tableNextRow == null || !tableRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
            {
                // 入金明細ではない場合
                if (ConstCls.DENPYOU_SHURUI_CD_10 != denpyou_shurui_cd)
                {
                    // 税計算区分が伝票毎だったら伝票毎消費税を出力
                    if (ConstCls.ZEI_KEISAN_KBN_DENPYOU == denpyou_zei_keisan_kbn_cd)
                    {
                        var denpyou_uchizei_gaku = tableRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                        var denpyou_sotozei_gaku = tableRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                        var denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                        var shouhizeiStr = GetSyohizei(denpyou_syouhizei, denpyou_zei_kbn_cd, true);

                        var drDenpyouShouhizei = printData.NewRow();
                        drDenpyouShouhizei["HINMEI_NAME"] = "【伝票毎消費税】";
                        drDenpyouShouhizei["SHOUHIZEI"] = shouhizeiStr;
                        printData.Rows.Add(drDenpyouShouhizei);
                    }
                }
            }
        }


        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <param name="value">編集対象文字列</param>
        /// <returns></returns>
        private static string SetComma(string value)
        {
            if (value == null || value == String.Empty)
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        /// <summary>
        /// 金額加算
        /// </summary>
        /// <param name="a">加算値1</param>
        /// <param name="b">加算値2</param>
        /// <returns></returns>
        private static string KingakuAdd(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                a = "0";
            }

            if (string.IsNullOrEmpty(b))
            {
                b = "0";
            }

            return (Convert.ToDecimal(a) + Convert.ToDecimal(b)).ToString();
        }

        /// <summary>
        /// 金額減算
        /// </summary>
        /// <param name="a">引かれる値</param>
        /// <param name="b">引く値</param>
        /// <returns></returns>
        private static string KingakuSubtract(string a, string b)
        {
            if (string.IsNullOrEmpty(a))
            {
                a = "0";
            }

            if (string.IsNullOrEmpty(b))
            {
                b = "0";
            }

            return (Convert.ToDecimal(a) - Convert.ToDecimal(b)).ToString();
        }

        /// <summary>
        /// 消費税内税の場合は括弧を追加
        /// 値が0の場合は空文字を返す
        /// </summary>
        /// <param name="shouhizei">消費税額</param>
        /// <param name="zeiKubun">税区分</param>
        /// <returns>編集後の文字列</returns>
        private static string GetSyohizei(string shouhizei, string zeiKubun)
        {
            return GetSyohizei(shouhizei, zeiKubun, false);
        }

        /// <summary>
        /// 消費税を書式設定する。
        /// </summary>
        /// <param name="shouhizei">消費税額</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="isZero">True:0でも表示 False:0は表示しない</param>
        /// <returns>書式設定した文字列</returns>
        private static string GetSyohizei(string shouhizei, string zeiKubun, bool isZero)
        {
            var ret = String.Empty;
            var zeigaku = Decimal.Parse(shouhizei);
            if (isZero)
            {
                if (ConstCls.ZEI_KBN_UCHI == zeiKubun)
                {
                    ret = ConstCls.KAKKO_START + SetComma(zeigaku.ToString()) + ConstCls.KAKKO_END;
                }
                else
                {
                    ret = SetComma(zeigaku.ToString());
                }
            }
            else if (0 != zeigaku)
            {
                if (ConstCls.ZEI_KBN_UCHI == zeiKubun)
                {
                    ret = ConstCls.KAKKO_START + SetComma(zeigaku.ToString()) + ConstCls.KAKKO_END;
                }
                else
                {
                    ret = SetComma(zeigaku.ToString());
                }
            }

            // 非課税は0でも表示
            if (ConstCls.ZEI_KBN_HIKAZEI == zeiKubun)
            {
                ret = "0";
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvDtList"></param>
        /// <param name="torihikisakiCd"></param>
        public static void ExportCsvPrint(List<DataTable> csvDtList, string torihikisakiCd)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            if (csvDtList.Count == 0)
            {
                msgLogic.MessageBoxShow("E044");
                return;
            }
            // 出力先指定のポップアップを表示させる。
            if (msgLogic.MessageBoxShow("C013") == DialogResult.Yes)
            {
                string number = string.Empty;
                CSVExport csvExport = new CSVExport();
                if (torihikisakiCd != null && !string.IsNullOrEmpty(torihikisakiCd))
                {
                    number = "請求書_" + torihikisakiCd;
                }
                else
                {
                    number = "請求書";
                }
                csvExport.ConvertDataTableToManyCsv(csvDtList, false, true, number, new SuperForm());
            }
        }
        #endregion

        #region 請求書印刷日制御
        /// <summary>
        /// 請求書印刷日活性・日活性制御
        /// </summary>
        /// <param name="seikyushoPrintdayVal">請求書印刷選択値</param>
        internal bool CdtSiteiPrintHidukeEnable(string seikyushoPrintdayVal)
        {
            bool ret = true;
            try
            {
                if (seikyushoPrintdayVal.Equals(ConstCls.SEIKYU_PRINT_DAY_SITEI))
                {
                    this.form.cdtSiteiPrintHiduke.Enabled = true;
                }
                else
                {
                    this.form.cdtSiteiPrintHiduke.Value = null;
                    this.form.cdtSiteiPrintHiduke.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CdtSiteiPrintHidukeEnable", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        #endregion 請求書印刷日制御

        #region 締日プルダウン値変更イベント
        /// <summary>
        /// 締日プルダウン値変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbShimebi_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        #region 取引先CDのフォーカスアウトイベント
        /// <summary>
        /// 取引先CDのフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Leave(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //取引先CDチェック
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                //取引先マスタデータ取得
                M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
                string torihikisakiCd = this.form.TORIHIKISAKI_CD.Text.PadLeft(6, '0');
                torihikisakiEntity.TORIHIKISAKI_CD = torihikisakiCd;
                torihikisakiEntity.ISNOT_NEED_DELETE_FLG = true;
                var torihikisakiResult = mtorihikisakiDao.GetAllValidData(torihikisakiEntity);

                bool dataExist = true;

                if (torihikisakiResult.Length == 0)
                {
                    //マスタチェックエラー
                    this.form.TORIHIKISAKI_CD.Text = torihikisakiCd;
                    this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    this.form.TORIHIKISAKI_CD.Focus();
                    dataExist = false;
                }

                if (dataExist)
                {
                    if (!string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                    {
                        //締日チェック
                        var torihikisaki = TorihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                        SqlInt16 shimebi = SqlInt16.Parse(this.form.cmbShimebi.Text);
                        if (torihikisaki == null
                            || (!shimebi.Equals(torihikisaki.SHIMEBI1) && !shimebi.Equals(torihikisaki.SHIMEBI2) && !shimebi.Equals(torihikisaki.SHIMEBI3)))
                        {
                            //締日チェックエラー
                            this.form.TORIHIKISAKI_CD.Text = torihikisakiCd;
                            this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E058");
                            this.form.TORIHIKISAKI_CD.Focus();
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        public void SetHeaderForm(HeaderSeikyuushoHakkou hs)
        {
            this.headerForm = hs;
        }

        #endregion

        #region unused
        public void PhysicalDelete()
        {
        }

        public void LogicalDelete()
        {
        }

        public void Update(bool errorFlag)
        {
        }

        public void Regist(bool errorFlag)
        {
        }

        #endregion

        /// 20141201 Houkakou 「請求書発行」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DenpyouHidukeTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.headerForm.DenpyouHidukeFrom;
            var ToTextBox = this.headerForm.DenpyouHidukeTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141201 Houkakou 「請求書発行」のダブルクリックを追加する　end

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private static DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 画面起動時に電子請求書で追加するコントロール・項目の表示/非表示を切り替える
        /// </summary>
        internal bool setDensiSeikyushoVisible()
        {
            // densiVisible true場合表示false場合非表示
            bool densiVisible = r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice();
            // 電子請求楽楽明細オプションン
            bool rakurakuVisible = r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai(); // 20211207 thucp v2.24_電子請求書 #157799

            if (!densiVisible && !rakurakuVisible)
            {
                this.form.PanelOutputKbn.Visible = densiVisible;
                this.form.labelOutputKbn.Visible = densiVisible;
                this.form.label3.Location = new System.Drawing.Point(this.form.label3.Location.X, this.form.label3.Location.Y - 22);
                this.form.customPanel2.Location = new System.Drawing.Point(this.form.customPanel2.Location.X, this.form.customPanel2.Location.Y - 22);
                this.form.checkBoxAll_densiCsv.Visible = densiVisible;
                this.form.SeikyuuDenpyouItiran.Columns[1].Visible = densiVisible;
                // 20211207 thucp v2.24_電子請求書 #157799 begin
                form.checkBoxAll_rakurakuCsv.Visible = rakurakuVisible;
                form.SeikyuuDenpyouItiran.Columns[2].Visible = rakurakuVisible;
                // 20211207 thucp v2.24_電子請求書 #157799 end

                this.form.label6.Location = new System.Drawing.Point(this.form.label6.Location.X, this.form.label6.Location.Y - 22);
                this.form.customPanel5.Location = new System.Drawing.Point(this.form.customPanel5.Location.X, this.form.customPanel5.Location.Y - 22);
            }
            else // 20220216 thucp 電子請求書 #160213
            {
                // インフォマートCSV
                form.checkBoxAll_densiCsv.Visible = densiVisible;
                form.SeikyuuDenpyouItiran.Columns[1].Visible = densiVisible;
                form.OUTPUT_KBN_2.Enabled = densiVisible;
                // 楽楽明細CSV
                form.checkBoxAll_rakurakuCsv.Visible = rakurakuVisible;
                form.SeikyuuDenpyouItiran.Columns[2].Visible = rakurakuVisible;
                form.OUTPUT_KBN_3.Enabled = rakurakuVisible;
            }

            return densiVisible || rakurakuVisible;
        }

        #region [F1]CSV出力
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        //
        internal void CSV()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //請求書指定日が4.指定の場合は、指定日の未入力チェックを行う
                if (this.form.SEIKYUSHO_PRINTDAY.Text == ConstCls.SEIKYU_PRINT_DAY_SITEI &&
                    this.form.cdtSiteiPrintHiduke.Value == null)
                {
                    msgLogic.MessageBoxShow("E012", "指定日");

                    this.form.cdtSiteiPrintHiduke.Focus();
                }
                else
                {
                    SeikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();

                    //CSV出力チェックボックスONカウント
                    int csvCnt = 0;

                    //CSV出力用DTO作成
                    SeikyuuDenpyouDto dto = new SeikyuuDenpyouDto();
                    dto.MSysInfo = this.mSysInfo;
                    //dto.Meisai = this.form.MEISAI.Text;   // No.4004
                    dto.SeikyuHakkou = this.form.SEIKYU_HAKKOU.Text;
                    dto.SeikyushoPrintDay = this.form.SEIKYUSHO_PRINTDAY.Text;
                    dto.HakkoBi = this.strSystemDate;
                    if (this.form.cdtSiteiPrintHiduke.Value != null)
                    {
                        dto.SeikyuDate = (DateTime)this.form.cdtSiteiPrintHiduke.Value;
                    }
                    dto.SeikyuStyle = this.form.SEIKYU_STYLE.Text;
                    dto.SeikyuPaper = this.form.SEIKYU_PAPER.Text;

                    CustomDataGridView csvDataGridView = this.CreateCsvDataGridView();
                    this.hakkousakuCheck = true;

                    // 20211208 thucp v2.24_電子請求書 #157799 begin
                    // 出力区分
                    var outputKbn = form.OUTPUT_KBN.Text;
                    // 楽楽明細CSV出力チェックボックスONカウント
                    int rakurakuCnt = 0;
                    // 請求形態
                    string keitaiKbn = form.SEIKYU_STYLE.Text;
                    // 単月請求DataGridView to export CSV
                    // パターンA
                    CustomDataGridView tangetsuCsvDgvPtA = CreateCsvDgvRakuraku();
                    // パターンB
                    CustomDataGridView tangetsuCsvDgvPtB = CreateCsvDgvRakuraku();
                    // パターンC
                    CustomDataGridView tangetsuCsvDgvPtC = CreateCsvDgvRakuraku();
                    // 繰越請求DataGridView to export CSV
                    // パターンD
                    CustomDataGridView kurikoshiCsvDgvPtD = CreateCsvDgvRakuraku();
                    // パターンE
                    CustomDataGridView kurikoshiCsvDgvPtE = CreateCsvDgvRakuraku();
                    // パターンF
                    CustomDataGridView kurikoshiCsvDgvPtF = CreateCsvDgvRakuraku();
                    // 20211208 thucp v2.24_電子請求書 #157799 end

                    //グリッドの発行列にチェックが付いているデータのみ処理を行う
                    foreach (DataGridViewRow row in this.form.SeikyuuDenpyouItiran.Rows)
                    {
                        if ((bool)row.Cells["DETAIL_OUTPUT_KBN"].Value == true)
                        {
                            csvCnt++;

                            DataTable dt = new DataTable();
                            dt.Columns.Add();

                            //
                            dto.TorihikisakiCd = row.Cells["TORIHIKISAKICD"].Value.ToString();

                            //印刷用データを取得
                            //G102請求書確認の処理を参考
                            //精算番号
                            string seikyuNumber = row.Cells["SEIKYUU_NUMBER"].Value.ToString();

                            //請求伝票を取得
                            T_SEIKYUU_DENPYOU tseikyudenpyou = SeikyuDenpyouDao.GetDataByCd(seikyuNumber);
                            //書式区分
                            string shoshikiKbn = tseikyudenpyou.SHOSHIKI_KBN.ToString();
                            //書式明細区分
                            string shoshikiMeisaiKbn = tseikyudenpyou.SHOSHIKI_MEISAI_KBN.ToString();

                            //入金明細区分 (請求携帯：2.単月請求の場合は、入金明細なしで固定)
                            string nyuukinMeisaiKbn = "2";
                            if (this.form.SEIKYU_STYLE.Text != "2")
                            {
                                nyuukinMeisaiKbn = tseikyudenpyou.NYUUKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = nyuukinMeisaiKbn;   // No.4004

                            //請求伝票データ取得
                            DataTable seikyuDt = GetSeikyudenpyo(this.SeikyuDenpyouDao, seikyuNumber, shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn, true, false);
                            this.form.SeikyuDt = seikyuDt;

                            if (seikyuDt.Rows.Count != 0)
                            {
                                //請求伝票データ設定
                                csvDataGridView = SetSeikyuCsv(seikyuDt, dto, csvDataGridView);
                            }
                        }

                        // 20211207 thucp v2.24_電子請求書 #157799 start
                        if (Convert.ToBoolean(row.Cells["RAKURAKU_CSV_OUTPUT"].Value))
                        {
                            rakurakuCnt++;
                            // 取引先CD
                            dto.TorihikisakiCd = row.Cells["TORIHIKISAKICD"].Value.ToString();
                            // 請求番号
                            string seikyuNumber = row.Cells["SEIKYUU_NUMBER"].Value.ToString();
                            // 請求伝票を取得
                            T_SEIKYUU_DENPYOU tseikyudenpyou = SeikyuDenpyouDao.GetDataByCd(seikyuNumber);
                            // 書式区分
                            string shoshikiKbn = tseikyudenpyou.SHOSHIKI_KBN.ToString();
                            // 書式明細区分
                            string shoshikiMeisaiKbn = tseikyudenpyou.SHOSHIKI_MEISAI_KBN.ToString();
                            // 請求伝票形態
                            string seikyuKeitaiKbn = tseikyudenpyou.SEIKYUU_KEITAI_KBN.ToString();
                            // 入金明細区分 (請求携帯：2.単月請求の場合は、入金明細なしで固定)
                            string nyuukinMeisaiKbn = "2";
                            if (form.SEIKYU_STYLE.Text != "2")
                            {
                                nyuukinMeisaiKbn = tseikyudenpyou.NYUUKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = nyuukinMeisaiKbn;   // No.4004
                            // 請求伝票データ取得
                            DataTable seikyuDt = GetSeikyudenpyo(SeikyuDenpyouDao, seikyuNumber, shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn, true, false);
                            form.SeikyuDt = seikyuDt;
                            if (seikyuDt.Rows.Count != 0)
                            {
                                string errorTarget;
                                var errorMsg = CheckRakurakuCode(seikyuNumber, out errorTarget);
                                if (!string.IsNullOrEmpty(errorMsg))
                                {
                                    msgLogic.MessageBoxShow("E313", errorTarget, errorMsg);
                                    return;
                                }
                                // Temp DataGridView
                                var csvDgv = CreateCsvDgvRakuraku();
                                // 単月請求 OR 繰越請求
                                var csvStyle = GetSeikyuuStyle(shoshikiKbn, shoshikiMeisaiKbn, seikyuKeitaiKbn, keitaiKbn, row.Cells["TORIHIKISAKICD"].Value.ToString());
                                csvDgv = SetSeikyuCsvRakuraku(seikyuDt, shoshikiKbn, shoshikiMeisaiKbn, csvStyle, nyuukinMeisaiKbn, csvDgv);
                                // Copy csvDgv into export's DataGridView
                                if (csvStyle == 1)
                                {
                                    CopyRowsToDataGridView(tangetsuCsvDgvPtA, csvDgv);
                                }
                                else if (csvStyle == 2)
                                {
                                    CopyRowsToDataGridView(tangetsuCsvDgvPtB, csvDgv);
                                }
                                else if (csvStyle == 3)
                                {
                                    CopyRowsToDataGridView(tangetsuCsvDgvPtC, csvDgv);
                                }
                                else if (csvStyle == 4)
                                {
                                    CopyRowsToDataGridView(kurikoshiCsvDgvPtD, csvDgv);
                                }
                                else if (csvStyle == 5)
                                {
                                    CopyRowsToDataGridView(kurikoshiCsvDgvPtE, csvDgv);
                                }
                                else
                                {
                                    CopyRowsToDataGridView(kurikoshiCsvDgvPtF, csvDgv);
                                }
                            }
                        }
                        // 20211207 thucp v2.24_電子請求書 #157799 end
                    }

                    //発行チェックボックスがすべてOFFの場合はメッセージ表示
                    if (csvCnt == 0 && r_framework.Configuration.AppConfig.AppOptions.IsElectronicInvoice() && outputKbn.Equals("2")) // 20211227 thucp v2.24_電子請求書 #157799
                    {
                        msgLogic.MessageBoxShow("E314", "ｲﾝﾌｫﾏｰﾄ");
                        return;
                    }
                    else if (rakurakuCnt == 0 && r_framework.Configuration.AppConfig.AppOptions.IsRakurakuMeisai() && outputKbn.Equals("3")) // 20211227 thucp v2.24_電子請求書 #157799
                    {
                        msgLogic.MessageBoxShow("E314", "楽楽明細");
                        return;
                    }
                    else
                    {
                        // 20211207 thucp v2.24_電子請求書 #157799 start
                        // 出力先確認を表示
                        string filePath = SelectOutputFilePath();
                        if (string.IsNullOrEmpty(filePath))
                        {
                            return;
                        }
                        // 20211207 thucp v2.24_電子請求書 #157799 end

                        CSVExport csvLogic = new CSVExport();

                        // 電子CSV
                        bool isOutput = false;
                        if (hakkousakuCheck)
                        {
                            if (csvDataGridView.RowCount > 1)
                            {
                                csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);
                                isOutput = csvLogic.ConvertDataGridViewToJisCsvWithPath(csvDataGridView, false, true, filePath, "電子請求書", this.form);
                            }
                        }
                        else
                        {
                            // 発行先コードが登録されていない請求書が選択されています。発行先コードを登録してください。
                            msgLogic.MessageBoxShow("E260");
                            return;
                        }

                        // 20211207 thucp v2.24_電子請求書 #157799 start
                        // 単月請求CSV
                        bool tangetsuOutputA = false;
                        bool tangetsuOutputB = false;
                        bool tangetsuOutputC = false;
                        if (tangetsuCsvDgvPtA.Rows.Count > 1)
                        {
                            tangetsuOutputA = csvLogic.ConvertDataGridViewToJisCsvWithPath(tangetsuCsvDgvPtA, true, true, filePath, "パターンA_単月", form);
                        }
                        if (tangetsuCsvDgvPtB.Rows.Count > 1)
                        {
                            tangetsuOutputB = csvLogic.ConvertDataGridViewToJisCsvWithPath(tangetsuCsvDgvPtB, true, true, filePath, "パターンB_単月", form);
                        }
                        if (tangetsuCsvDgvPtC.Rows.Count > 1)
                        {
                            tangetsuOutputC = csvLogic.ConvertDataGridViewToJisCsvWithPath(tangetsuCsvDgvPtC, true, true, filePath, "パターンC_単月", form);
                        }
                        // 繰越請求CSV
                        bool kurikoshiOutputD = false;
                        bool kurikoshiOutputE = false;
                        bool kurikoshiOutputF = false;
                        if (kurikoshiCsvDgvPtD.Rows.Count > 1)
                        {
                            kurikoshiOutputD = csvLogic.ConvertDataGridViewToJisCsvWithPath(kurikoshiCsvDgvPtD, true, true, filePath, "パターンD_繰越", form);
                        }
                        if (kurikoshiCsvDgvPtE.Rows.Count > 1)
                        {
                            kurikoshiOutputE = csvLogic.ConvertDataGridViewToJisCsvWithPath(kurikoshiCsvDgvPtE, true, true, filePath, "パターンE_繰越", form);
                        }
                        if (kurikoshiCsvDgvPtF.Rows.Count > 1)
                        {
                            kurikoshiOutputF = csvLogic.ConvertDataGridViewToJisCsvWithPath(kurikoshiCsvDgvPtF, true, true, filePath, "パターンF_繰越", form);
                        }

                        if (isOutput || tangetsuOutputA || tangetsuOutputB || tangetsuOutputC || kurikoshiOutputD || kurikoshiOutputE || kurikoshiOutputF)
                        {
                            foreach (DataGridViewRow row in form.SeikyuuDenpyouItiran.Rows)
                            {
                                if (Convert.ToBoolean(row.Cells["DETAIL_OUTPUT_KBN"].Value) || Convert.ToBoolean(row.Cells["RAKURAKU_CSV_OUTPUT"].Value))
                                {
                                    // 発行区分を更新
                                    DataTable seikyuDenpyo = UpdateSeikyuDenpyouHakkouKbn(SeikyuDenpyouDao, row.Cells["SEIKYUU_NUMBER"].Value.ToString(), (byte[])row.Cells["colTimeStamp"].Value);
                                    if (seikyuDenpyo != null && seikyuDenpyo.Rows.Count > 0)
                                    {
                                        // 発行済みチェックをON
                                        row.Cells["HAKKOUZUMI"].Value = seikyuDenpyo.Rows[0]["HAKKOU_KBN"];
                                        // タイムスタンプを設定
                                        row.Cells["colTimeStamp"].Value = seikyuDenpyo.Rows[0]["TIME_STAMP"];
                                    }
                                }
                            }

                            // グリッドを再描画
                            form.SeikyuuDenpyouItiran.Refresh();
                            MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("対象データが無い為、出力を中止しました", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        // 20211207 thucp v2.24_電子請求書 #157799 end
                    }

                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CSV", ex1);
                this.errMsg.MessageBoxShow("E093", "");
                return;
            }
            catch (System.IO.IOException ex)
            {
                LogUtility.Error("CSV", ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogUtility.Error("CSV", ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.errMsg.MessageBoxShow("E245", "");
                return;
            }
        }

        /// <summary>
        /// 請求伝票データ設定
        /// </summary>
        /// <param name="seikyuDt">請求伝票データテーブル</param>
        /// <param name="dto">請求書発行DTO</param>
        private CustomDataGridView SetSeikyuCsv(DataTable seikyuDt, SeikyuuDenpyouDto dto, CustomDataGridView csvDataGridView)
        {

            //先頭行の鏡番号を取得
            DataRow startNewRow = seikyuDt.Rows[0];

            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.NYUKIN_MEISAI_NASHI && string.IsNullOrEmpty(startNewRow["KAGAMI_NUMBER"].ToString()))
            {
                #region 「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
                foreach (DataRow startRow in seikyuDt.Rows)
                {
                    csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);

                    // 「請求形態」の条件によって出力内容を分岐
                    //   1:請求書データ作成時 → T_SEIKYUU_DENPYOU.SEIKYUU_KEITAI_KBN を利用して発行
                    //   2:単月請求           → 単月請求として発行
                    //   3:繰越請求           → 繰越請求として発行
                    if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_DATA_SAKUSEIJI)
                    {
                        if (startRow["SEIKYUU_KEITAI_KBN"].ToString() == "1")
                        {
                            // 単月請求
                            csvDataGridView = this.SetTangetuSeikyuCsv(startRow, csvDataGridView, dto);

                        }
                        else
                        {
                            // 繰越請求
                            csvDataGridView = this.SetKurikosiSeikyuCsv(startRow, csvDataGridView, dto);
                        }
                    }
                    else if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_TANGETU_SEIKYU)
                    {
                        // 単月請求
                        csvDataGridView = this.SetTangetuSeikyuCsv(startRow, csvDataGridView, dto);
                    }
                    else
                    {
                        // 繰越請求
                        csvDataGridView = this.SetKurikosiSeikyuCsv(startRow, csvDataGridView, dto);
                    }

                    // 内税品名新しい行追加処理
                    this.CreateCsvDataGridViewForNaizei(csvDataGridView);
                    
                }

                return csvDataGridView;
            }
                #endregion
            else
            {
                #region【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力
                var isSeikyuuUchizei = false;
                var isSeikyuuSotozei = false;

                int nowKagamiNo = Convert.ToInt16(startNewRow["KAGAMI_NUMBER"]);
                int rowCount = 0;
                foreach (DataRow startRow in seikyuDt.Rows)
                {
                    //鏡番号が同じか 
                    if (Convert.ToInt16(startRow["KAGAMI_NUMBER"]) != nowKagamiNo)
                    {
                        isSeikyuuUchizei = false;
                        isSeikyuuSotozei = false;
                    }

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(startRow["ROW_NUMBER"].ToString()))
                    {
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_UCHI == startRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeikyuuUchizei = true;
                        }
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_SOTO == startRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeikyuuSotozei = true;
                        }
                    }

                    //現在行の前の行
                    DataRow tablePevRow = null;
                    if (rowCount == 0)
                    {
                        //現在行の前の行
                        tablePevRow = null;
                    }
                    else
                    {
                        //現在行の前の行
                        tablePevRow = seikyuDt.Rows[rowCount - 1];
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (rowCount == seikyuDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = seikyuDt.Rows[rowCount + 1];
                    }

                    rowCount++;

                    csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);

                    // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
                    bool isNyuukin = startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

                    // 「請求形態」の条件によって出力内容を分岐
                    //   1:請求書データ作成時 → T_SEIKYUU_DENPYOU.SEIKYUU_KEITAI_KBN を利用して発行
                    //   2:単月請求           → 単月請求として発行
                    //   3:繰越請求           → 繰越請求として発行
                    if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_DATA_SAKUSEIJI)
                    {
                        if (startRow["SEIKYUU_KEITAI_KBN"].ToString() == "1")
                        {
                            // 単月請求
                            csvDataGridView = this.SetTangetuSeikyuCsv(startRow, csvDataGridView, dto);

                        }
                        else
                        {
                            // 繰越請求
                            csvDataGridView = this.SetKurikosiSeikyuCsv(startRow, csvDataGridView, dto);
                        }
                    }
                    else if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_TANGETU_SEIKYU)
                    {
                        // 単月請求
                        csvDataGridView = this.SetTangetuSeikyuCsv(startRow, csvDataGridView, dto);
                    }
                    else
                    {
                        // 繰越請求
                        csvDataGridView = this.SetKurikosiSeikyuCsv(startRow, csvDataGridView, dto);
                    }
                    var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                    var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();
                    if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun)
                                || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun))
                    {
                        // 入金計が出力処理
                        if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10)
                        {
                            decimal kinGaku = 0;
                            if (!(startRow["KINGAKU"] is DBNull))
                            {
                                kinGaku = decimal.Parse(startRow["KINGAKU"].ToString());
                            }

                            this.nyuukinKei = this.nyuukinKei + kinGaku;
                        }

                        if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10
                            && (tableNextRow == null || tableNextRow["DENPYOU_SHURUI_CD"].ToString() != ConstCls.DENPYOU_SHURUI_CD_10))
                        {
                            csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);
                            // 明細項目
                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = "【入金計】";
                            // 金額
                            if (this.nyuukinKei == 0)
                            {
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = null;
                            }
                            else
                            {
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = this.nyuukinKei.ToString("##0");
                            }
                            // 入金額リーセット
                            this.nyuukinKei = 0;
                            // CSVに出力場合明細行空の項目をstring.emptyに設定する
                            csvDataGridView = this.SetCsvDataGridViewRowForNyuukin(csvDataGridView);

                        }
                    }

                    // 内税品名新しい行追加処理
                    this.CreateCsvDataGridViewForNaizei(csvDataGridView);

                    // 消費税
                    var denpyou_shurui_cd = startRow["DENPYOU_SHURUI_CD"].ToString();
                    var denpyou_zei_keisan_kbn_cd = startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
                    var denpyou_zei_kbn_cd = startRow["DENPYOU_ZEI_KBN_CD"].ToString();
                    if (tableNextRow == null || !startRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
                    {
                        // 入金明細ではない場合
                        if (ConstCls.DENPYOU_SHURUI_CD_10 != denpyou_shurui_cd)
                        {
                             // 税計算区分が伝票毎だったら伝票毎消費税を出力
                            if (ConstCls.ZEI_KEISAN_KBN_DENPYOU == denpyou_zei_keisan_kbn_cd)
                            {
                                csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);
                                // 明細項目
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = "【伝票毎消費税】";

                                //消費税
                                var denpyou_uchizei_gaku = startRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                                var denpyou_sotozei_gaku = startRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                                var denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                                string syouhize = GetSyohizei(denpyou_syouhizei, denpyou_zei_kbn_cd, true).Replace(",","");
                                if (!string.IsNullOrEmpty(syouhize) && (syouhize == "0" || syouhize == ConstCls.KAKKO_START + "0" + ConstCls.KAKKO_END))
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                                }
                                else
                                {

                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = syouhize;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = syouhize;
                                }
                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvDataGridViewRow(csvDataGridView);

                                // 内税品名新しい行追加処理
                                this.CreateCsvDataGridViewForNaizei(csvDataGridView);
                            }
                        }

                        if (seikyuDt.Rows.Count == rowCount || (tableNextRow == null || !startRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"])))
                        {
                            if (isSeikyuuUchizei)
                            {
                                csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);

                                // 明細項目
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = "【請求毎消費税(内)】";
                                //消費税
                                string strKonkaiSeiUtizeiGaku = "0";
                                if (!(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] is DBNull))
                                {
                                    strKonkaiSeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"].ToString()).ToString("##0");
                                }
                                string syouhize =Const.ConstCls.KAKKO_START + strKonkaiSeiUtizeiGaku + Const.ConstCls.KAKKO_END;
                                if (strKonkaiSeiUtizeiGaku == "0")
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                                }
                                else
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = syouhize;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = syouhize;
                                }
                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvDataGridViewRow(csvDataGridView);

                                // 内税品名新しい行追加処理
                                this.CreateCsvDataGridViewForNaizei(csvDataGridView);
                            }

                            if (isSeikyuuSotozei)
                            {
                                csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);

                                // 明細項目
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = "【請求毎消費税】";
                                //消費税
                                string strKonkaiSeiSotozeiGaku = "0";
                                if (!(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] is DBNull))
                                {
                                    strKonkaiSeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"].ToString()).ToString("##0");
                                }
                                string syouhize = strKonkaiSeiSotozeiGaku;
                                if (strKonkaiSeiSotozeiGaku == "0")
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                                }
                                else
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = syouhize;
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = syouhize;
                                }
                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvDataGridViewRow(csvDataGridView);

                                // 内税品名新しい行追加処理
                                this.CreateCsvDataGridViewForNaizei(csvDataGridView);

                            }
                        } 
                    }

                    nowKagamiNo = Convert.ToInt16(startRow["KAGAMI_NUMBER"]);
                }

                return csvDataGridView;
                #endregion
            }
            
        }

        /// <summary>
        /// csvデータヘッダ生成
        /// </summary>
        /// <returns>CsvTable</returns>
        private CustomDataGridView CreateCsvDataGridView()
        {
            // csvヘッダ生成
            CustomDataGridView csvDataGridView = new CustomDataGridView();
            // 請求書番号
            csvDataGridView.Columns.Add("0", string.Empty);
            // 発行先コード
            csvDataGridView.Columns.Add("1", string.Empty);
            // 件名
            csvDataGridView.Columns.Add("2", string.Empty);
            // 入金期限
            csvDataGridView.Columns.Add("3", string.Empty);
            // 前回請求金額
            csvDataGridView.Columns.Add("4", string.Empty);
            // 入金額
            csvDataGridView.Columns.Add("5", string.Empty);
            // 調整金額
            csvDataGridView.Columns.Add("6", string.Empty);
            // 繰越金額
            csvDataGridView.Columns.Add("7", string.Empty);
            // 今回請求金額（税抜）
            csvDataGridView.Columns.Add("8", string.Empty);
            // 今回消費税額
            csvDataGridView.Columns.Add("9", string.Empty);
            // 今回請求金額（税込）
            csvDataGridView.Columns.Add("10", string.Empty);
            // おもての請求金額
            csvDataGridView.Columns.Add("11", string.Empty);
            // 締日
            csvDataGridView.Columns.Add("12", string.Empty);
            // 備考
            csvDataGridView.Columns.Add("13", string.Empty);
            // 明細日付
            csvDataGridView.Columns.Add("14", string.Empty);
            // 明細番号
            csvDataGridView.Columns.Add("15", string.Empty);
            // 商品コード
            csvDataGridView.Columns.Add("16", string.Empty);
            // 明細項目
            csvDataGridView.Columns.Add("17", string.Empty);
            // 数量
            csvDataGridView.Columns.Add("18", string.Empty);
            // 単価
            csvDataGridView.Columns.Add("19", string.Empty);
            // 単位
            csvDataGridView.Columns.Add("20", string.Empty);
            // 金額
            csvDataGridView.Columns.Add("21", string.Empty);
            // 消費税額
            csvDataGridView.Columns.Add("22", string.Empty);
            // 請求金額
            csvDataGridView.Columns.Add("23", string.Empty);
            // 税区分（課税/非課税/免税/不課税）
            csvDataGridView.Columns.Add("24", string.Empty);
            // 税率
            csvDataGridView.Columns.Add("25", string.Empty);
            // 税額入力形式（税抜/税込/手入力）
            csvDataGridView.Columns.Add("26", string.Empty);
            // 部門コード
            csvDataGridView.Columns.Add("27", string.Empty);
            // 部門
            csvDataGridView.Columns.Add("28", string.Empty);
            // 備考
            csvDataGridView.Columns.Add("29", string.Empty);

            // ヘッダ名
            csvDataGridView.Rows[0].Cells[0].Value = "請求書番号";
            csvDataGridView.Rows[0].Cells[1].Value = "発行先コード";
            csvDataGridView.Rows[0].Cells[2].Value = "件名";
            csvDataGridView.Rows[0].Cells[3].Value = "入金期限";
            csvDataGridView.Rows[0].Cells[4].Value = "前回請求金額";
            csvDataGridView.Rows[0].Cells[5].Value = "入金額";
            csvDataGridView.Rows[0].Cells[6].Value = "調整金額";
            csvDataGridView.Rows[0].Cells[7].Value = "繰越金額";
            csvDataGridView.Rows[0].Cells[8].Value = "今回請求金額（税抜）";
            csvDataGridView.Rows[0].Cells[9].Value = "今回消費税額";
            csvDataGridView.Rows[0].Cells[10].Value = "今回請求金額（税込）";
            csvDataGridView.Rows[0].Cells[11].Value = "おもての請求金額";
            csvDataGridView.Rows[0].Cells[12].Value = "締日";
            csvDataGridView.Rows[0].Cells[13].Value = "備考";
            csvDataGridView.Rows[0].Cells[14].Value = "明細日付";
            csvDataGridView.Rows[0].Cells[15].Value = "明細番号";
            csvDataGridView.Rows[0].Cells[16].Value = "商品コード";
            csvDataGridView.Rows[0].Cells[17].Value = "明細項目";
            csvDataGridView.Rows[0].Cells[18].Value = "数量";
            csvDataGridView.Rows[0].Cells[19].Value = "単価";
            csvDataGridView.Rows[0].Cells[20].Value = "単位";
            csvDataGridView.Rows[0].Cells[21].Value = "金額";
            csvDataGridView.Rows[0].Cells[22].Value = "消費税額";
            csvDataGridView.Rows[0].Cells[23].Value = "請求金額";
            csvDataGridView.Rows[0].Cells[24].Value = "税区分（課税／非課税／免税／不課税）";
            csvDataGridView.Rows[0].Cells[25].Value = "税率";
            csvDataGridView.Rows[0].Cells[26].Value = "税額入力形式（税抜／税込／手入力）";
            csvDataGridView.Rows[0].Cells[27].Value = "部門コード";
            csvDataGridView.Rows[0].Cells[28].Value = "部門名";
            csvDataGridView.Rows[0].Cells[29].Value = "備考";

            return csvDataGridView;
        }

        /// <summary>
        /// DataGridView行追加時、前回行クリア対応
        /// </summary>
        /// <returns>CsvTable</returns>
        private CustomDataGridView CreateCsvDataGridViewRowAdd(CustomDataGridView csvDataGridView)
        {
            CustomDataGridView addDataGridView = CreateCsvDataGridView();
            if (csvDataGridView.RowCount > 1)
            {
                addDataGridView.Rows.Add(csvDataGridView.RowCount - 1);
            }
            for (int i = 0; i <= csvDataGridView.RowCount - 1; i++)
            {

                addDataGridView.Rows[i].Cells[0].Value = csvDataGridView.Rows[i].Cells[0].Value;
                addDataGridView.Rows[i].Cells[1].Value = csvDataGridView.Rows[i].Cells[1].Value;
                addDataGridView.Rows[i].Cells[2].Value = csvDataGridView.Rows[i].Cells[2].Value;
                addDataGridView.Rows[i].Cells[3].Value = csvDataGridView.Rows[i].Cells[3].Value;
                addDataGridView.Rows[i].Cells[4].Value = csvDataGridView.Rows[i].Cells[4].Value;
                addDataGridView.Rows[i].Cells[5].Value = csvDataGridView.Rows[i].Cells[5].Value;
                addDataGridView.Rows[i].Cells[6].Value = csvDataGridView.Rows[i].Cells[6].Value;
                addDataGridView.Rows[i].Cells[7].Value = csvDataGridView.Rows[i].Cells[7].Value;
                addDataGridView.Rows[i].Cells[8].Value = csvDataGridView.Rows[i].Cells[8].Value;
                addDataGridView.Rows[i].Cells[9].Value = csvDataGridView.Rows[i].Cells[9].Value;
                addDataGridView.Rows[i].Cells[10].Value = csvDataGridView.Rows[i].Cells[10].Value;
                addDataGridView.Rows[i].Cells[11].Value = csvDataGridView.Rows[i].Cells[11].Value;
                addDataGridView.Rows[i].Cells[12].Value = csvDataGridView.Rows[i].Cells[12].Value;
                addDataGridView.Rows[i].Cells[13].Value = csvDataGridView.Rows[i].Cells[13].Value;
                addDataGridView.Rows[i].Cells[14].Value = csvDataGridView.Rows[i].Cells[14].Value;
                addDataGridView.Rows[i].Cells[15].Value = csvDataGridView.Rows[i].Cells[15].Value;
                addDataGridView.Rows[i].Cells[16].Value = csvDataGridView.Rows[i].Cells[16].Value;
                addDataGridView.Rows[i].Cells[17].Value = csvDataGridView.Rows[i].Cells[17].Value;
                addDataGridView.Rows[i].Cells[18].Value = csvDataGridView.Rows[i].Cells[18].Value;
                addDataGridView.Rows[i].Cells[19].Value = csvDataGridView.Rows[i].Cells[19].Value;
                addDataGridView.Rows[i].Cells[20].Value = csvDataGridView.Rows[i].Cells[20].Value;
                addDataGridView.Rows[i].Cells[21].Value = csvDataGridView.Rows[i].Cells[21].Value;
                addDataGridView.Rows[i].Cells[22].Value = csvDataGridView.Rows[i].Cells[22].Value;
                addDataGridView.Rows[i].Cells[23].Value = csvDataGridView.Rows[i].Cells[23].Value;
                addDataGridView.Rows[i].Cells[24].Value = csvDataGridView.Rows[i].Cells[24].Value;
                addDataGridView.Rows[i].Cells[25].Value = csvDataGridView.Rows[i].Cells[25].Value;
                addDataGridView.Rows[i].Cells[26].Value = csvDataGridView.Rows[i].Cells[26].Value;
                addDataGridView.Rows[i].Cells[27].Value = csvDataGridView.Rows[i].Cells[27].Value;
                addDataGridView.Rows[i].Cells[28].Value = csvDataGridView.Rows[i].Cells[28].Value;
                addDataGridView.Rows[i].Cells[29].Value = csvDataGridView.Rows[i].Cells[29].Value;
            }

            csvDataGridView.Rows.Insert(csvDataGridView.RowCount - 1, 1);

            for (int j = 0; j <= addDataGridView.RowCount - 1; j++)
            {

                csvDataGridView.Rows[j].Cells[0].Value = addDataGridView.Rows[j].Cells[0].Value;
                csvDataGridView.Rows[j].Cells[1].Value = addDataGridView.Rows[j].Cells[1].Value;
                csvDataGridView.Rows[j].Cells[2].Value = addDataGridView.Rows[j].Cells[2].Value;
                csvDataGridView.Rows[j].Cells[3].Value = addDataGridView.Rows[j].Cells[3].Value;
                csvDataGridView.Rows[j].Cells[4].Value = addDataGridView.Rows[j].Cells[4].Value;
                csvDataGridView.Rows[j].Cells[5].Value = addDataGridView.Rows[j].Cells[5].Value;
                csvDataGridView.Rows[j].Cells[6].Value = addDataGridView.Rows[j].Cells[6].Value;
                csvDataGridView.Rows[j].Cells[7].Value = addDataGridView.Rows[j].Cells[7].Value;
                csvDataGridView.Rows[j].Cells[8].Value = addDataGridView.Rows[j].Cells[8].Value;
                csvDataGridView.Rows[j].Cells[9].Value = addDataGridView.Rows[j].Cells[9].Value;
                csvDataGridView.Rows[j].Cells[10].Value = addDataGridView.Rows[j].Cells[10].Value;
                csvDataGridView.Rows[j].Cells[11].Value = addDataGridView.Rows[j].Cells[11].Value;
                csvDataGridView.Rows[j].Cells[12].Value = addDataGridView.Rows[j].Cells[12].Value;
                csvDataGridView.Rows[j].Cells[13].Value = addDataGridView.Rows[j].Cells[13].Value;
                csvDataGridView.Rows[j].Cells[14].Value = addDataGridView.Rows[j].Cells[14].Value;
                csvDataGridView.Rows[j].Cells[15].Value = addDataGridView.Rows[j].Cells[15].Value;
                csvDataGridView.Rows[j].Cells[16].Value = addDataGridView.Rows[j].Cells[16].Value;
                csvDataGridView.Rows[j].Cells[17].Value = addDataGridView.Rows[j].Cells[17].Value;
                csvDataGridView.Rows[j].Cells[18].Value = addDataGridView.Rows[j].Cells[18].Value;
                csvDataGridView.Rows[j].Cells[19].Value = addDataGridView.Rows[j].Cells[19].Value;
                csvDataGridView.Rows[j].Cells[20].Value = addDataGridView.Rows[j].Cells[20].Value;
                csvDataGridView.Rows[j].Cells[21].Value = addDataGridView.Rows[j].Cells[21].Value;
                csvDataGridView.Rows[j].Cells[22].Value = addDataGridView.Rows[j].Cells[22].Value;
                csvDataGridView.Rows[j].Cells[23].Value = addDataGridView.Rows[j].Cells[23].Value;
                csvDataGridView.Rows[j].Cells[24].Value = addDataGridView.Rows[j].Cells[24].Value;
                csvDataGridView.Rows[j].Cells[25].Value = addDataGridView.Rows[j].Cells[25].Value;
                csvDataGridView.Rows[j].Cells[26].Value = addDataGridView.Rows[j].Cells[26].Value;
                csvDataGridView.Rows[j].Cells[27].Value = addDataGridView.Rows[j].Cells[27].Value;
                csvDataGridView.Rows[j].Cells[28].Value = addDataGridView.Rows[j].Cells[28].Value;
                csvDataGridView.Rows[j].Cells[29].Value = addDataGridView.Rows[j].Cells[29].Value;

            }

            return csvDataGridView;
        }

        /// <summary>
        /// 単月請求データ設定
        /// </summary>
        /// <param name="startRow">明細行</param>
        /// <param name="csvDataGridView">請求伝票データテーブル</param>
        /// <param name="dto">請求書発行DTO</param>
        /// <returns>請求伝票データ</returns>
        private CustomDataGridView SetTangetuSeikyuCsv(DataRow startRow, CustomDataGridView csvDataGridView, SeikyuuDenpyouDto dto)
        {
            // 単価フォーマット
            string tankaFormat = this.tankaFormat();
            // 数量フォーマット
            string suuryouFormat = this.suuryouFormat();
            // 請求書番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[0].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 発行先コード
            string hakkousakuCD = string.Empty;
            string shosikuKbn = startRow["SHOSHIKI_KBN"].ToString();
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 1の場合
            if (shosikuKbn == "1")
            {
                var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(startRow["TSDK_TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.TORIHIKISAKI_CD に対応する発行先コードを入れる
                    hakkousakuCD = torihikisakiSeikyuu.HAKKOUSAKI_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 2の場合
            if (shosikuKbn == "2")
            {
                var gyousha = mgyoushaDao.GetDataByCd(startRow["TSDK_GYOUSHA_CD"].ToString());
                if (gyousha != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.GYOUSHA_CD に対応する発行先コードを入れる
                    hakkousakuCD = gyousha.HAKKOUSAKI_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 3の場合
            if (shosikuKbn == "3")
            {
                M_GENBA data = new M_GENBA();
                data.GYOUSHA_CD = startRow["TSDK_GYOUSHA_CD"].ToString();
                data.GENBA_CD = startRow["TSDK_GENBA_CD"].ToString();

                var genba = mgenbaDao.GetDataByCd(data);
                if (genba != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.GENBA_CD に対応する発行先コードを入れる
                    hakkousakuCD = genba.HAKKOUSAKI_CD;
                }
            }

            if (string.IsNullOrEmpty(hakkousakuCD))
            {
                this.hakkousakuCheck = false;
            }

            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[1].Value = hakkousakuCD;
            // 件名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[2].Value = string.Empty;
            // 入金期限
            string nyuukinYoteiBi = string.Empty;
            if (!(startRow["NYUUKIN_YOTEI_BI"] is DBNull))
            {
                nyuukinYoteiBi = startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(0, 4) + startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(5, 2) + startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(8, 2);
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[3].Value = nyuukinYoteiBi;
            // 前回請求金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[4].Value = string.Empty;
            // 入金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[5].Value = string.Empty;
            // 調整金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[6].Value = string.Empty;
            // 繰越金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[7].Value = string.Empty;
            // 今回請求金額（税抜）
            string strKonkaiUriageGaku = "0";
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                strKonkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[8].Value = strKonkaiUriageGaku;
            // 今回消費税額
            string strSyouhizeiGaku = "0";
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                strSyouhizeiGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[9].Value = strSyouhizeiGaku;
            // 今回請求金額（税込） [9]今回取引額（税抜） + [10]消費税
            decimal konkaiSeiutizeGaku = 0;
            decimal syouhizeGaku = 0;
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                konkaiSeiutizeGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString());
            }

            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                syouhizeGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[10].Value = (konkaiSeiutizeGaku + syouhizeGaku).ToString("##0");
            // おもての請求金額  [11]今回取引額
            decimal sasihikuGaku = 0;
            if (!(startRow["SASIHIKIGAKU"] is DBNull))
            {
                sasihikuGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[11].Value = (konkaiSeiutizeGaku + syouhizeGaku).ToString("##0");
            // 締日
            // 請求日付にどの日付を入れるかは請求書発行画面の請求年月日で決まる。
            string seikyuDate = string.Empty;

            if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SIMEBI)
            {
                // １．締日	請求日付
                seikyuDate = ((DateTime)startRow["SEIKYUU_DATE"]).ToString("yyyyMMdd");
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_HAKKOBI)
            {
                // ２．発行日	当日
                seikyuDate = getDBDateTime().ToString("yyyyMMdd");
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SITEI)
            {
                // ４．指定	指定した日付
                DateTime daySitei = dto.SeikyuDate;
                seikyuDate = daySitei.ToString("yyyyMMdd");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[12].Value = seikyuDate;
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[13].Value = string.Empty;
            // 明細日付
            string denpyouDate = string.Empty;
            if (!(startRow["DENPYOU_DATE"] is DBNull))
            {
                denpyouDate = startRow["DENPYOU_DATE"].ToString().Substring(0, 4) + startRow["DENPYOU_DATE"].ToString().Substring(5, 2) + startRow["DENPYOU_DATE"].ToString().Substring(8, 2);
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = denpyouDate;
            // 明細番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = startRow["DENPYOU_NUMBER"].ToString();
            // 商品コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[16].Value = startRow["HINMEI_CD"] is DBNull ? string.Empty : startRow["HINMEI_CD"].ToString();
            // 明細項目
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = startRow["HINMEI_NAME"].ToString();
            if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10)
            {
                // 入金明細の数量、単価は「ブランク」とする。
                // 数量
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = string.Empty;
                // 単価
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = string.Empty;
            }
            else
            {
                // 数量
                decimal suuryou = 0;
                if (!(startRow["SUURYOU"] is DBNull))
                {
                    suuryou = decimal.Parse(startRow["SUURYOU"].ToString());
                }
                if (suuryou == 0)
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = null;
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = suuryou.ToString(suuryouFormat);
                }
                // 単価
                decimal tanka = 0;
                if (!(startRow["TANKA"] is DBNull))
                {
                    tanka = decimal.Parse(startRow["TANKA"].ToString());
                }
                if (tanka == 0)
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = null;
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = tanka.ToString(tankaFormat);
                }
            }

            // 単位
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = startRow["UNIT_NAME"] is DBNull ? string.Empty : startRow["UNIT_NAME"].ToString();

            // 金額
            string strkinGaku = "0";
            if (!(startRow["KINGAKU"] is DBNull))
            {
                strkinGaku = decimal.Parse(startRow["KINGAKU"].ToString()).ToString("##0");
            }
            if (strkinGaku == "0")
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = null;
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = strkinGaku;
            }
            // 消費税額
            string syouhize = string.Empty;
            decimal uchizeGaku = 0;
            decimal sotozeiGaku = 0;
            if (!(startRow["UCHIZEI_GAKU"] is DBNull))
            {
                uchizeGaku = decimal.Parse(startRow["UCHIZEI_GAKU"].ToString());
            }
            if (!(startRow["SOTOZEI_GAKU"] is DBNull))
            {
                sotozeiGaku = decimal.Parse(startRow["SOTOZEI_GAKU"].ToString());
            }

            syouhize = (uchizeGaku + sotozeiGaku).ToString("##0");

            bool isNaizei = false;

            if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "0")
            {
                // 明細税区分CDが0.税無し の場合は
                if (startRow["DENPYOU_ZEI_KBN_CD"].ToString() == "2")
                {
                    // 内税
                    syouhize = ConstCls.KAKKO_START + syouhize + ConstCls.KAKKO_END;
                    isNaizei = true;

                }
                else if (startRow["DENPYOU_ZEI_KBN_CD"].ToString() == "3")
                {
                    // 非課税
                    syouhize = "0";

                }

            }
            else if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "2")
            {
                // 内税
                syouhize = ConstCls.KAKKO_START + syouhize + ConstCls.KAKKO_END;
                isNaizei = true;

            }
            else if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "3")
            {
                // 非課税
                syouhize = "0";
            }
            if (syouhize == "0" || syouhize == ConstCls.KAKKO_START + "0" + ConstCls.KAKKO_END)
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = syouhize;
            }
            if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10)
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = string.Empty;
            }
            else
            {
                // 請求金額 [21]金額 + [22]消費税
                decimal kingaku = 0;
                if (!(startRow["KINGAKU"] is DBNull))
                {
                    // 入金明細の行には請求金額が出力されないようにする
                    kingaku = decimal.Parse(startRow["KINGAKU"].ToString());
                }
                if (isNaizei)
                {
                    if (kingaku == 0)
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                    }
                    else
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = kingaku.ToString("##0");
                    }
                }
                else
                {
                    if (kingaku + decimal.Parse(syouhize) == 0)
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                    }
                    else
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = (kingaku + decimal.Parse(syouhize)).ToString("##0");
                    }
                }
            }

            // 税区分（課税/非課税/免税/不課税）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = string.Empty;
            // 税率
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = string.Empty;
            // 税額入力形式（税抜/税込/手入力）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = string.Empty;
            // 部門コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = string.Empty;
            // 部門
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = string.Empty;

            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = startRow["MEISAI_BIKOU"].ToString();
            return csvDataGridView;
        }

        /// <summary>
        /// 繰越請求データ設定
        /// </summary>
        /// <param name="startRow">明細行</param>
        /// <param name="csvDataGridView">請求伝票データテーブル</param>
        /// <param name="dto">請求書発行DTO</param>
        /// <returns>請求伝票データ</returns>
        private CustomDataGridView SetKurikosiSeikyuCsv(DataRow startRow, CustomDataGridView csvDataGridView, SeikyuuDenpyouDto dto)
        {
            // 単価フォーマット
            string tankaFormat = this.tankaFormat();
            // 数量フォーマット
            string suuryouFormat = this.suuryouFormat();
            // 請求書番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[0].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 発行先コード
            string hakkousakuCD = string.Empty;
            string shosikuKbn = startRow["SHOSHIKI_KBN"].ToString();
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 1の場合
            if (shosikuKbn == "1")
            {
                var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(startRow["TSDK_TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.TORIHIKISAKI_CD に対応する発行先コードを入れる
                    hakkousakuCD = torihikisakiSeikyuu.HAKKOUSAKI_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 2の場合
            if (shosikuKbn == "2")
            {
                var gyousha = mgyoushaDao.GetDataByCd(startRow["TSDK_GYOUSHA_CD"].ToString());
                if (gyousha != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.GYOUSHA_CD に対応する発行先コードを入れる
                    hakkousakuCD = gyousha.HAKKOUSAKI_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 3の場合
            if (shosikuKbn == "3")
            {
                M_GENBA data = new M_GENBA();
                data.GYOUSHA_CD = startRow["TSDK_GYOUSHA_CD"].ToString();
                data.GENBA_CD = startRow["TSDK_GENBA_CD"].ToString();

                var genba = mgenbaDao.GetDataByCd(data);
                if (genba != null)
                {
                    // T_SEIKYUU_DENPYOU_KAGAMI.GENBA_CD に対応する発行先コードを入れる
                    hakkousakuCD = genba.HAKKOUSAKI_CD;
                }
            }

            if (string.IsNullOrEmpty(hakkousakuCD))
            {
                this.hakkousakuCheck = false;
            }

            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[1].Value = hakkousakuCD;
            // 件名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[2].Value = string.Empty;
            // 入金期限
            string nyuukinYoteiBi = string.Empty;
            if (!(startRow["NYUUKIN_YOTEI_BI"] is DBNull))
            {
                nyuukinYoteiBi = startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(0, 4) + startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(5, 2) + startRow["NYUUKIN_YOTEI_BI"].ToString().Substring(8, 2);
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[3].Value = nyuukinYoteiBi;


            var torihikisaki = TorihikisakiSeikyuuDao.GetDataByCd(startRow["TSDK_TORIHIKISAKI_CD"].ToString());
            // 伝票の書式区分が請求先別以外かつ
            // T_SEIKYUU_DETAIL .GYOUSHA_CDもしくは T_SEIKYUU_DETAIL .GENBA_CD のいずれかに値が入っている場合は
            if (torihikisaki != null && torihikisaki.SHOSHIKI_KBN != 1 &&
                 (!string.IsNullOrEmpty(startRow["TSDK_GYOUSHA_CD"].ToString()) ||
                  !string.IsNullOrEmpty(startRow["TSDK_GENBA_CD"].ToString())))
            {
                // 前回請求金額
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[4].Value = 0;
                // 入金額
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[5].Value = 0;
                // 調整金額
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[6].Value = 0;
                // 繰越金額
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[7].Value = 0;
                // 今回請求金額（税抜） 今回御請求額　=　今回取引額
                string strKonkaiUriageGaku = "0";
                if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
                {
                    strKonkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString()).ToString("##0");
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[8].Value = strKonkaiUriageGaku;
            }
            else
            {
                // 前回請求金額
                string zenkaiKurikosiGaku = "0";
                if (!(startRow["ZENKAI_KURIKOSI_GAKU"] is DBNull))
                {
                    zenkaiKurikosiGaku = decimal.Parse(startRow["ZENKAI_KURIKOSI_GAKU"].ToString()).ToString("##0");
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[4].Value = zenkaiKurikosiGaku;
                // 入金額
                string kongkaiNyuukinGaku = "0";
                if (!(startRow["KONKAI_NYUUKIN_GAKU"] is DBNull))
                {
                    kongkaiNyuukinGaku = decimal.Parse(startRow["KONKAI_NYUUKIN_GAKU"].ToString()).ToString("##0");
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[5].Value = kongkaiNyuukinGaku;
                // 調整金額
                string konkaiChouseiGaku = "0";
                if (!(startRow["KONKAI_CHOUSEI_GAKU"] is DBNull))
                {
                    konkaiChouseiGaku = decimal.Parse(startRow["KONKAI_CHOUSEI_GAKU"].ToString()).ToString("##0");
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[6].Value = konkaiChouseiGaku;
                // 繰越金額
                decimal SasihikiGaku = 0;
                string strSasihikiGaku = "0";
                if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
                {
                    strSasihikiGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString()).ToString("##0");
                    SasihikiGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString());
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[7].Value = strSasihikiGaku;
                // 今回請求金額（税抜）今回御請求額　=　 今回取引額
                string KonkaiUriageGaku = "0";
                if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
                {
                    KonkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString()).ToString("##0");      
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[8].Value = KonkaiUriageGaku;
            }


            
            // 今回消費税額
            string strSyouhizeiGaku = "0";
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                strSyouhizeiGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[9].Value = strSyouhizeiGaku;
            // 今回請求金額（税込） [9]今回取引額（税抜） + [10]消費税
            decimal konkaiSeiutizeGaku = 0;
            decimal syouhizeGaku = 0;
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                konkaiSeiutizeGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString());
            }

            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                syouhizeGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[10].Value = (konkaiSeiutizeGaku + syouhizeGaku).ToString("##0");
            // おもての請求金額 [8]繰越額 + [11]今回取引額
            decimal sasihikuGaku = 0;
            if (!(startRow["SASIHIKIGAKU"] is DBNull))
            {
                sasihikuGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[11].Value = (konkaiSeiutizeGaku + syouhizeGaku + sasihikuGaku).ToString("##0");
            // 締日
            // 請求日付にどの日付を入れるかは請求書発行画面の請求年月日で決まる。
            string seikyuDate = string.Empty;

            if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SIMEBI)
            {
                // １．締日	請求日付
                seikyuDate = ((DateTime)startRow["SEIKYUU_DATE"]).ToString("yyyyMMdd");
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_HAKKOBI)
            {
                // ２．発行日	当日
                seikyuDate = getDBDateTime().ToString("yyyyMMdd");
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SITEI)
            {
                // ４．指定	指定した日付
                DateTime daySitei = dto.SeikyuDate;
                seikyuDate = daySitei.ToString("yyyyMMdd");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[12].Value = seikyuDate;
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[13].Value = string.Empty;
            // 明細日付
            string denpyouDate = string.Empty;
            if (!(startRow["DENPYOU_DATE"] is DBNull))
            {
                denpyouDate = startRow["DENPYOU_DATE"].ToString().Substring(0, 4) + startRow["DENPYOU_DATE"].ToString().Substring(5, 2) + startRow["DENPYOU_DATE"].ToString().Substring(8, 2);
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = denpyouDate;
            // 明細番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = startRow["DENPYOU_NUMBER"].ToString();
            // 商品コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[16].Value = startRow["HINMEI_CD"] is DBNull ? string.Empty : startRow["HINMEI_CD"].ToString();
            // 明細項目
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = startRow["HINMEI_NAME"].ToString();
            if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10)
            {
                // 入金明細の数量、単価は「ブランク」とする。
                // 数量
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = string.Empty;
                // 単価
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = string.Empty;
            }
            else
            {
                // 数量
                decimal suuryou = 0;
                if (!(startRow["SUURYOU"] is DBNull))
                {
                    suuryou = decimal.Parse(startRow["SUURYOU"].ToString());
                }
                if (suuryou == 0)
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = null;
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = suuryou.ToString(suuryouFormat);
                }
                // 単価
                decimal tanka = 0;
                if (!(startRow["TANKA"] is DBNull))
                {
                    tanka = decimal.Parse(startRow["TANKA"].ToString());
                }
                if (tanka == 0)
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = null;
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = tanka.ToString(tankaFormat);
                }
            }

            // 単位
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = startRow["UNIT_NAME"] is DBNull ? string.Empty : startRow["UNIT_NAME"].ToString();
            // 金額
            string strkinGaku = "0";
            if (!(startRow["KINGAKU"] is DBNull))
            {
                strkinGaku = decimal.Parse(startRow["KINGAKU"].ToString()).ToString("##0");
            }
            if (strkinGaku == "0")
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = null;
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = strkinGaku;
            }
            // 消費税額
            string syouhize = string.Empty;
            decimal uchizeGaku = 0;
            decimal sotozeiGaku = 0;
            if (!(startRow["UCHIZEI_GAKU"] is DBNull))
            {
                uchizeGaku = decimal.Parse(startRow["UCHIZEI_GAKU"].ToString());
            }
            if (!(startRow["SOTOZEI_GAKU"] is DBNull))
            {
                sotozeiGaku = decimal.Parse(startRow["SOTOZEI_GAKU"].ToString());
            }

            syouhize = (uchizeGaku + sotozeiGaku).ToString("##0");

            bool isNaizei = false;

            if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "0")
            {
                // 明細税区分CDが0.税無し の場合は
                if (startRow["DENPYOU_ZEI_KBN_CD"].ToString() == "2")
                {
                    // 内税
                    syouhize = ConstCls.KAKKO_START + syouhize + ConstCls.KAKKO_END;
                    isNaizei = true;

                }
                else if (startRow["DENPYOU_ZEI_KBN_CD"].ToString() == "3")
                {
                    // 非課税
                    syouhize = "0";

                }

            }
            else if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "2")
            {
                // 内税
                syouhize = ConstCls.KAKKO_START + syouhize + ConstCls.KAKKO_END;
                isNaizei = true;

            }
            else if (startRow["MEISAI_ZEI_KBN_CD"].ToString() == "3")
            {
                // 非課税
                syouhize = "0";
            }
            if (syouhize == "0" || syouhize == ConstCls.KAKKO_START + "0" + ConstCls.KAKKO_END)
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = syouhize;
            }
            // 請求金額 [20]金額 + [21]消費税
            if (startRow["DENPYOU_SHURUI_CD"].ToString() == ConstCls.DENPYOU_SHURUI_CD_10)
            {
                // 入金明細の行には請求金額が出力されないようにする
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = string.Empty;
            }
            else
            {
                decimal kingaku = 0;
                if (!(startRow["KINGAKU"] is DBNull))
                {
                    kingaku = decimal.Parse(startRow["KINGAKU"].ToString());
                }
                if (isNaizei)
                {
                    if (kingaku == 0)
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                    }
                    else
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = kingaku.ToString("##0");
                    }      
                }
                else
                {
                    if (kingaku + decimal.Parse(syouhize) == 0)
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = null;
                    }
                    else
                    {
                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = (kingaku + decimal.Parse(syouhize)).ToString("##0");
                    }
                }
            }

            // 税区分（課税/非課税/免税/不課税）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = string.Empty;
            // 税率
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = string.Empty;
            // 税額入力形式（税抜/税込/手入力）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = string.Empty;
            // 部門コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = string.Empty;
            // 部門
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = string.Empty;
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = startRow["MEISAI_BIKOU"].ToString();
            
            return csvDataGridView;
        }

        /// <summary>
        /// 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
        /// </summary>
        /// <returns>CsvTable</returns>
        private CustomDataGridView SetCsvDataGridViewRow(CustomDataGridView csvDataGridView)
        {
            // 明細日付
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = string.Empty;
            // 明細番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = string.Empty;
            // 商品コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[16].Value = string.Empty;
            // 数量
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = string.Empty;
            // 単価
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = string.Empty;
            // 単位
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = string.Empty;
            // 金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = string.Empty;
            // 税区分（課税/非課税/免税/不課税）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = string.Empty;
            // 税率
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = string.Empty;
            // 税額入力形式（税抜/税込/手入力）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = string.Empty;
            // 部門コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = string.Empty;
            // 部門
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = string.Empty;
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = string.Empty;

            return csvDataGridView;
        }

        /// <summary>
        /// 【入金計】の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
        /// </summary>
        /// <returns>CsvTable</returns>
        private CustomDataGridView SetCsvDataGridViewRowForNyuukin(CustomDataGridView csvDataGridView)
        {
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[13].Value = string.Empty;
            // 明細日付
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = string.Empty;
            // 明細番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = string.Empty;
            // 数量
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = string.Empty;
            // 単価
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = string.Empty;
            // 単位
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = string.Empty;
            // 消費税
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = null;
            // 請求金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = string.Empty;
            // 税区分（課税/非課税/免税/不課税）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = string.Empty;
            // 税率
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = string.Empty;
            // 税額入力形式（税抜/税込/手入力）
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = string.Empty;
            // 部門コード
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = string.Empty;
            // 部門
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = string.Empty;
            // 備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = string.Empty;

            return csvDataGridView;
        }

        /// <summary>
        /// 内税品名新しい行追加処理
        /// </summary>
        /// <returns>CsvTable</returns>
        private CustomDataGridView CreateCsvDataGridViewForNaizei(CustomDataGridView csvDataGridView)
        {
            if (csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[22].Value != null &&
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[22].Value.ToString().Substring(0, 1) == ConstCls.KAKKO_START &&
                csvDataGridView.Rows.Count > 1) 

            {
                // DataGridView行追加時、前回行クリア対応
                csvDataGridView = CreateCsvDataGridViewRowAdd(csvDataGridView);

                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[0].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[0].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[1].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[1].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[2].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[2].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[3].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[3].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[4].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[4].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[5].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[5].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[6].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[6].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[7].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[7].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[8].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[8].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[9].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[9].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[10].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[10].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[11].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[11].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[12].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[12].Value;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[13].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[13].Value;
                // 空にする
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[14].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[15].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[16].Value = string.Empty;
                // 【内税】
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[17].Value = "【内税】";
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[18].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[19].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[20].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[21].Value = string.Empty;
                // 内税金額
                if (string.IsNullOrEmpty(csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[22].Value.ToString()))
                {
                    csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[22].Value = null;
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[22].Value = csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[22].Value.ToString().Substring(1, csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[22].Value.ToString().Length - 2);
                }
                // 元行内税金額
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 2].Cells[22].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[23].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[24].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[25].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[26].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[27].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[28].Value = string.Empty;
                csvDataGridView.Rows[csvDataGridView.Rows.Count - 1].Cells[29].Value = string.Empty;

            }

            return csvDataGridView;
        }

        /// <summary>
        /// 数量フォーマット
        /// </summary>
        /// <returns>フォーマット書式</returns>
        private string suuryouFormat()
        {
            SqlInt16 formatCD = this.mSysInfo.SYS_SUURYOU_FORMAT_CD;
            string strFormat = "###";

            if (formatCD == 1)
            {
                strFormat = "###";
            }
            else if (formatCD == 2)
            {
                strFormat = "##0";
            }
            else if (formatCD == 3)
            {
                strFormat = "##0.0";
            }
            else
            {
                // ただし、BtoBプラットフォームでは小数点以下２桁がMAXのため
                //「5．小数点第３位表示（#,##0.000）」が選択されているときは強制的に
                //「4．小数点第２位表示（#,##0.00）」を適応すること。
                strFormat = "##0.00";
            }


            return strFormat;
        }

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <returns>フォーマット書式</returns>
        private string tankaFormat()
        {
            SqlInt16 formatCD = this.mSysInfo.SYS_TANKA_FORMAT_CD;
            string strFormat = "###";

            if (formatCD == 1)
            {
                strFormat = "###";
            }
            else if (formatCD == 2)
            {
                strFormat = "##0";
            }
            else if (formatCD == 3)
            {
                strFormat = "##0.0";
            }
            else
            {
                // ただし、BtoBプラットフォームでは小数点以下２桁がMAXのため
                //「5．小数点第３位表示（#,##0.000）」が選択されているときは強制的に
                //「4．小数点第２位表示（#,##0.00）」を適応すること。
                strFormat = "##0.00";
            }

            return strFormat;
        }
        #endregion
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        #region thucp v2.24_電子請求書 #157799
        private string SelectOutputFilePath()
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "CSVファイルの出力場所を選択してください。";
            var initialPath = @"C:\Temp";
            var windowHandle = form.Handle;
            var isFileSelect = false;

            string filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            browserForFolder.Dispose();

            return filePath;
        }

        /// <summary>
        /// 楽楽顧客コードチェック
        /// </summary>
        private string CheckRakurakuCode(string seikyuNumber, out string errorTarget)
        {
            LogUtility.DebugMethodStart(seikyuNumber);

            errorTarget = string.Empty;
            string ret = string.Empty;

            var kagamis = SeikyuKagamiDao.GetDataByCd(seikyuNumber);

            foreach (var kagami in kagamis)
            {
                // GYOUSHA_CD ≠ BLANK and GENBA_CD ≠ BLANK
                if (!string.IsNullOrEmpty(kagami.GYOUSHA_CD) && !string.IsNullOrEmpty(kagami.GENBA_CD))
                {
                    var key = new M_GENBA
                    {
                        GYOUSHA_CD = kagami.GYOUSHA_CD,
                        GENBA_CD = kagami.GENBA_CD
                    };
                    var genba = mgenbaDao.GetDataByCd(key);
                    if (genba != null && string.IsNullOrEmpty(genba.RAKURAKU_CUSTOMER_CD))
                    {
                        errorTarget = "現場";
                        return string.Format("取引先CD＝{0}、業者CD＝{1}、現場CD＝{2}", kagami.TORIHIKISAKI_CD, kagami.GYOUSHA_CD, kagami.GENBA_CD);
                    }
                }

                // GYOUSHA_CD ≠ BLANK and GENBA_CD = BLANK
                if (!string.IsNullOrEmpty(kagami.GYOUSHA_CD) && string.IsNullOrEmpty(kagami.GENBA_CD))
                {
                    var gyousha = mgyoushaDao.GetDataByCd(kagami.GYOUSHA_CD);
                    if (gyousha != null && string.IsNullOrEmpty(gyousha.RAKURAKU_CUSTOMER_CD))
                    {
                        errorTarget = "業者";
                        return string.Format("取引先CD＝{0}、業者CD＝{1}", kagami.TORIHIKISAKI_CD, kagami.GYOUSHA_CD);
                    }
                }

                // GYOUSHA_CD = BLANK and GENBA_CD = BLANK
                if (string.IsNullOrEmpty(kagami.GYOUSHA_CD) && string.IsNullOrEmpty(kagami.GENBA_CD))
                {
                    var torihikisaki = TorihikisakiSeikyuuDao.GetDataByCd(kagami.TORIHIKISAKI_CD);
                    if (torihikisaki != null && string.IsNullOrEmpty(torihikisaki.RAKURAKU_CUSTOMER_CD))
                    {
                        errorTarget = "請求先";
                        return string.Format("取引先CD＝{0}", kagami.TORIHIKISAKI_CD);
                    }
                }
            }

            LogUtility.DebugMethodEnd(ret, errorTarget);
            return ret;
        }

        /// <summary>Gets CSV output style.</summary>
        /// <returns>1:パターンA_単月, 2:パターンB_単月, 3:パターンC_単月, 4:パターンD_繰越, 5:パターンE_繰越, 6:パターンF_繰越</returns>
        private int GetSeikyuuStyle(string shoshikiKbn, string shoshikiMeisaiKbn, string seikyuuKeitaiKbn, string keitaiKbn, string torihikisakiCd)
        {
            int ret;

            if (shoshikiKbn.Equals("1")) // 請求先別
            {
                if (keitaiKbn.Equals("2")) // 請求形態 = 2.単月請求
                {
                    if (shoshikiMeisaiKbn.Equals("1")) // 請求書書式2 = 1.なし
                    {
                        ret = 1;
                    }
                    else if (shoshikiMeisaiKbn.Equals("2")) // 請求書書式2 = 2.業者毎
                    {
                        ret = 2;
                    }
                    else if (shoshikiMeisaiKbn.Equals("3")) // 請求書書式2 = 3.現場毎
                    {
                        ret = 3;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
                else if (keitaiKbn.Equals("3")) // 請求形態 = 3.繰越請求
                {
                    if (shoshikiMeisaiKbn.Equals("1")) // 請求書書式2 = 1.なし
                    {
                        ret = 4;
                    }
                    else if (shoshikiMeisaiKbn.Equals("2")) // 請求書書式2 = 2.業者毎
                    {
                        ret = 5;
                    }
                    else if (shoshikiMeisaiKbn.Equals("3")) // 請求書書式2 = 3.現場毎
                    {
                        ret = 6;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
                else // 請求形態 = 1.請求データ作成時
                {
                    if (shoshikiMeisaiKbn.Equals("1") && seikyuuKeitaiKbn.Equals("1")) // 請求書書式2 = 1.なし かつ 請求形態 = 1.単月請求
                    {
                        ret = 1;
                    }
                    else if (shoshikiMeisaiKbn.Equals("2") && seikyuuKeitaiKbn.Equals("1")) // 請求書書式2 = 2.業者毎 かつ 請求形態 = 1.単月請求
                    {
                        ret = 2;
                    }
                    else if (shoshikiMeisaiKbn.Equals("3") && seikyuuKeitaiKbn.Equals("1")) // 請求書書式2 = 3.現場毎 かつ 請求形態 = 1.単月請求
                    {
                        ret = 3;
                    }
                    else if (shoshikiMeisaiKbn.Equals("1") && seikyuuKeitaiKbn.Equals("2")) // 請求書書式2 = 1.なし かつ 請求形態 = 2.繰越請求
                    {
                        ret = 4;
                    }
                    else if (shoshikiMeisaiKbn.Equals("2") && seikyuuKeitaiKbn.Equals("2")) // 請求書書式2 = 2.業者毎 かつ 請求形態 = 2.繰越請求
                    {
                        ret = 5;
                    }
                    else if (shoshikiMeisaiKbn.Equals("3") && seikyuuKeitaiKbn.Equals("2")) // 請求書書式2 = 3.現場毎 かつ 請求形態 = 2.繰越請求
                    {
                        ret = 6;
                    }
                    else
                    {
                        ret = 1;
                    }
                }
            }
            else if (shoshikiKbn.Equals("2")) // 業者別
            {
                if (shoshikiMeisaiKbn.Equals("1")) // 請求伝票形態 = 1.なし
                {
                    ret = 1;
                }
                else if (shoshikiMeisaiKbn.Equals("3")) // 請求伝票形態 = 3.現場毎
                {
                    ret = 2;
                }
                else
                {
                    ret = 1;
                }
            }
            else // 現場別
            {
                // 取引先CDから取引先マスタデータを取得
                M_TORIHIKISAKI_SEIKYUU torihikisakiSK = this.TorihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
                if (torihikisakiSK != null)
                {
                    if (torihikisakiSK.SHOSHIKI_GENBA_KBN == 1) // 現場名あり
                    {
                        ret = 2;
                    }
                    else // 現場名なし
                    {
                        ret = 1;
                    }
                }
                else
                {
                    ret = 1;
                }
            }

            return ret;
        }

        /// <summary>
        /// 請求伝票データ設定
        /// </summary>
        private CustomDataGridView SetSeikyuCsvRakuraku(DataTable seikyuDt, string shoshiki1, string shoshiki2, int seikyuuStyle, string nyuukinMeisaiKbn, CustomDataGridView csvDataGridView)
        {
            // 入金データと入金以外のデータを取得する。
            DataRow[] nyuukinIgaiRows = seikyuDt.Select("DENPYOU_SHURUI_CD <> " + ConstCls.DENPYOU_SHURUI_CD_10);
            DataRow[] nyuukinRows = seikyuDt.Select("DENPYOU_SHURUI_CD = " + ConstCls.DENPYOU_SHURUI_CD_10);
            if (nyuukinRows.Count() > 0)
            {
                seikyuDt = nyuukinRows.CopyToDataTable();

                // 入金以外のデータを入金データの後に結合する。
                if (nyuukinIgaiRows.Count() > 0)
                {
                    DataTable dt = nyuukinIgaiRows.CopyToDataTable();
                    seikyuDt.Merge(dt);
                }   
            }

            //先頭行の鏡番号を取得
            DataRow startNewRow = seikyuDt.Rows[0];

            if (nyuukinMeisaiKbn.Equals(ConstCls.NYUKIN_MEISAI_NASHI) && string.IsNullOrEmpty(startNewRow["KAGAMI_NUMBER"].ToString()))
            {
                #region 「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
                foreach (DataRow startRow in seikyuDt.Rows)
                {
                    csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);

                    // 「請求形態」の条件によって出力内容を分岐
                    if (seikyuuStyle == 1 || seikyuuStyle == 2 || seikyuuStyle == 3)
                    {
                        // 単月請求
                        csvDataGridView = this.SetTangetuSeikyuCsvRakuraku(shoshiki1, shoshiki2, startRow, csvDataGridView);
                    }
                    else
                    {
                        // 繰越請求
                        csvDataGridView = this.SetKurikosiSeikyuCsvRakuraku(shoshiki1, shoshiki2, startRow, csvDataGridView);
                    }
                }

                return csvDataGridView;
                #endregion
            }
            else
            {
                #region【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力
                var isSeikyuuUchizei = false;
                var isSeikyuuSotozei = false;
                bool isEmptyDetail;

                int nowKagamiNo = Convert.ToInt16(startNewRow["KAGAMI_NUMBER"]);
                int rowCount = 0;
                foreach (DataRow startRow in seikyuDt.Rows)
                {
                    bool gyoushaTotalFlg = false;
                    bool nyuukinTotalFlg = false;

                    isEmptyDetail = true;

                    //鏡番号が同じか 
                    if (Convert.ToInt16(startRow["KAGAMI_NUMBER"]) != nowKagamiNo)
                    {
                        isSeikyuuUchizei = false;
                        isSeikyuuSotozei = false;
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (rowCount == seikyuDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = seikyuDt.Rows[rowCount + 1];
                    }

                    rowCount++;

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(startRow["ROW_NUMBER"].ToString()))
                    {
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU.Equals(startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString()) && ConstCls.ZEI_KBN_UCHI.Equals(startRow["DENPYOU_ZEI_KBN_CD"].ToString()))
                        {
                            isSeikyuuUchizei = true;
                        }
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU.Equals(startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString()) && ConstCls.ZEI_KBN_SOTO.Equals(startRow["DENPYOU_ZEI_KBN_CD"].ToString()))
                        {
                            isSeikyuuSotozei = true;
                        }

                        // 明細情報が存在
                        isEmptyDetail = false;
                    }

                    if (isEmptyDetail)
                    {
                        continue;
                    }

                    csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);

                    // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
                    bool isNyuukin = startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

                    // 「請求形態」の条件によって出力内容を分岐
                    if (seikyuuStyle == 1 || seikyuuStyle == 2 || seikyuuStyle == 3)
                    {
                        // 単月請求
                        csvDataGridView = this.SetTangetuSeikyuCsvRakuraku(shoshiki1, shoshiki2, startRow, csvDataGridView);
                    }
                    else
                    {
                        // 繰越請求
                        csvDataGridView = this.SetKurikosiSeikyuCsvRakuraku(shoshiki1, shoshiki2, startRow, csvDataGridView);
                    }

                    // 消費税
                    var denpyou_zei_keisan_kbn_cd = startRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
                    var denpyou_zei_kbn_cd = startRow["DENPYOU_ZEI_KBN_CD"].ToString();
                    if (tableNextRow == null || !startRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
                    {
                        // 入金明細ではない場合
                        if (!isNyuukin)
                        {
                            // 税計算区分が伝票毎だったら伝票毎消費税を出力
                            if (ConstCls.ZEI_KEISAN_KBN_DENPYOU.Equals(denpyou_zei_keisan_kbn_cd))
                            {
                                csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);
                                // 品名
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "【伝票毎消費税】";

                                // 消費税
                                var denpyou_uchizei_gaku = startRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                                var denpyou_sotozei_gaku = startRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                                var denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                                string syouhize = GetSyohizei(denpyou_syouhizei, denpyou_zei_kbn_cd, true).Replace(",", "");
                                if (!string.IsNullOrEmpty(syouhize) && (syouhize.Equals(ConstCls.KAKKO_START + "0" + ConstCls.KAKKO_END)))
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
                                }
                                else
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(syouhize);
                                }
                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvRowRakuraku(csvDataGridView, true);
                            }
                        }
                    }

                    var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                    var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();

                    // 入金計や業者計
                    if (tableNextRow == null || !startRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"]))
                    {
                        if ((ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKubun)) ||
                            (ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun)) ||
                            (ConstCls.SHOSHIKI_KBN_2.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun) && isNyuukin))
                        {
                            if (isNyuukin) // 入金計が出力処理
                            {
                                decimal nyuukin = 0;
                                if (!(startRow["GYOUSHA_KINGAKU_1"] is DBNull))
                                {
                                    nyuukin += decimal.Parse(startRow["GYOUSHA_KINGAKU_1"].ToString());
                                }

                                if (tableNextRow == null || !ConstCls.DENPYOU_SHURUI_CD_10.Equals(tableNextRow["DENPYOU_SHURUI_CD"].ToString()))
                                {
                                    csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);
                                    // 品名
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "入金計";
                                    // 金額
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(nyuukin.ToString("#,##0"));
                                    // CSVに出力場合明細行空の項目をstring.emptyに設定する
                                    csvDataGridView = this.SetCsvRowForNyuukinRakuraku(csvDataGridView);

                                    nyuukinTotalFlg = true;
                                }
                            }
                            else // 業者計が出力処理
                            {
                                if (ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKubun))
                                {
                                    decimal gyoushaKei = 0;
                                    if (!(startRow["GYOUSHA_KINGAKU_1"] is DBNull))
                                    {
                                        gyoushaKei += decimal.Parse(startRow["GYOUSHA_KINGAKU_1"].ToString());
                                    }

                                    csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);
                                    // 品名
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "業者計";
                                    // 金額
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(gyoushaKei.ToString("#,##0"));
                                    // 消費税
                                    var gyousha_uchizei_gaku = startRow["GENBA_UCHIZEI"].ToString();
                                    var gyousha_sotozei_gaku = startRow["GENBA_SOTOZEI"].ToString();
                                    var gyousha_syouhizei = KingakuAdd(gyousha_uchizei_gaku, gyousha_sotozei_gaku);

                                    // 伝票毎消費税を加算する。
                                    var denpyou_uchizei_gaku = startRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                                    var denpyou_sotozei_gaku = startRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                                    var denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                                    var totalSyouhizei = (Convert.ToDecimal(gyousha_syouhizei) + Convert.ToDecimal(denpyou_syouhizei)).ToString();

                                    if ((!string.IsNullOrEmpty(totalSyouhizei) && totalSyouhizei.Equals("0.0000"))
                                        || (!gyousha_uchizei_gaku.Equals("0.0000") && gyousha_sotozei_gaku.Equals("0.0000")))
                                    {
                                        csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
                                    }
                                    else
                                    {
                                        if (gyousha_uchizei_gaku.Equals("0.0000") && gyousha_sotozei_gaku.Equals("0.0000"))
                                        {
                                            // 「請求形態」の条件によって出力内容を分岐
                                            if (seikyuuStyle == 1 || seikyuuStyle == 2 || seikyuuStyle == 3)
                                            {
                                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = decimal.Parse(startRow["TSD_KONKAI_DEN_SOTOZEI_GAKU"].ToString()).ToString("#,##0");
                                            }
                                            else
                                            {
                                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(decimal.Parse(totalSyouhizei).ToString("#,##0"));
                                            }
                                        }
                                        else
                                        {
                                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(decimal.Parse(totalSyouhizei).ToString("#,##0"));
                                        }
                                    }
                                    // CSVに出力場合明細行空の項目をstring.emptyに設定する
                                    csvDataGridView = this.SetCsvRowRakuraku(csvDataGridView, false);
                                    if (!string.IsNullOrEmpty(totalSyouhizei) && !totalSyouhizei.Equals("0.0000"))
                                    {
                                        // 明細備考: 請求毎税は除く
                                        if (!gyousha_uchizei_gaku.Equals("0.0000") && gyousha_sotozei_gaku.Equals("0.0000"))
                                        {
                                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = ConstCls.KAKKO_START + decimal.Parse(gyousha_uchizei_gaku).ToString("#,##0") + ConstCls.KAKKO_END + ConstCls.SEIKYU_ZEI_EXCEPT;
                                        }
                                        else
                                        {
                                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = ConstCls.SEIKYU_ZEI_EXCEPT;
                                        }
                                    }

                                    gyoushaTotalFlg = true;
                                }
                            }
                        }
                    }

                    // 現場計
                    if (tableNextRow == null || !startRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                    {
                        if ((ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun) && !isNyuukin) ||
                            (ConstCls.SHOSHIKI_KBN_2.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun) && !isNyuukin))
                        {
                            // 現場計が出力処理
                            decimal genbaKei = 0;
                            if (!(startRow["GENBA_KINGAKU_1"] is DBNull))
                            {
                                genbaKei += decimal.Parse(startRow["GENBA_KINGAKU_1"].ToString());
                            }

                            csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);
                            // 品名
                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "現場計";
                            // 金額
                            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(genbaKei.ToString("#,##0"));
                            // 消費税
                            var genba_uchizei_gaku = startRow["GENBA_UCHIZEI"].ToString();
                            var genba_sotozei_gaku = startRow["GENBA_SOTOZEI"].ToString();
                            var genba_syouhizei = KingakuAdd(genba_uchizei_gaku, genba_sotozei_gaku);

                            // 伝票毎消費税を加算する。
                            var denpyou_uchizei_gaku = startRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                            var denpyou_sotozei_gaku = startRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                            var denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                            var totalSyouhizei = (Convert.ToDecimal(genba_syouhizei) + Convert.ToDecimal(denpyou_syouhizei)).ToString();

                            if (!string.IsNullOrEmpty(totalSyouhizei) && totalSyouhizei.Equals("0.0000"))
                            {
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
                            }
                            else
                            {
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(decimal.Parse(totalSyouhizei).ToString("#,##0"));
                            }

                            // CSVに出力場合明細行空の項目をstring.emptyに設定する
                            csvDataGridView = this.SetCsvRowRakuraku(csvDataGridView, false);
                            if (!string.IsNullOrEmpty(totalSyouhizei) && !totalSyouhizei.Equals("0.0000"))
                            {
                                // 明細備考: 請求毎税は除く
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = ConstCls.SEIKYU_ZEI_EXCEPT;
                            }
                        }

                        // 業者計（業者計と入金計が未出力の場合）
                        if (!gyoushaTotalFlg && !nyuukinTotalFlg && (tableNextRow == null || !startRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"])))
                        {
                            if ((ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKubun)) ||
                                (ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun)) ||
                                (ConstCls.SHOSHIKI_KBN_2.Equals(shoshikiKubun) && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKubun) && isNyuukin))
                            {

                                decimal gyoushaKei = 0;
                                if (!(startRow["GYOUSHA_KINGAKU_1"] is DBNull))
                                {
                                    gyoushaKei += decimal.Parse(startRow["GYOUSHA_KINGAKU_1"].ToString());
                                }

                                csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);
                                // 品名
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "業者計";
                                // 金額
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(gyoushaKei.ToString("#,##0"));
                                // 消費税
                                var gyousha_uchizei_gaku = startRow["GYOUSHA_UCHIZEI"].ToString();
                                var gyousha_sotozei_gaku = startRow["GYOUSHA_SOTOZEI"].ToString();
                                var gyousha_syouhizei = KingakuAdd(gyousha_uchizei_gaku, gyousha_sotozei_gaku);

                                // 伝票毎消費税を加算する。
                                var denpyou_syouhizei = "0";
                                if (Convert.ToDecimal(gyousha_syouhizei) > 0)
                                {
                                    denpyou_syouhizei = startRow["TSDK_KONKAI_DEN_SOTOZEI_GAKU"].ToString();
                                }
                                else
                                {
                                    var denpyou_uchizei_gaku = startRow["DENPYOU_UCHIZEI_GAKU"].ToString();
                                    var denpyou_sotozei_gaku = startRow["DENPYOU_SOTOZEI_GAKU"].ToString();
                                    denpyou_syouhizei = KingakuAdd(denpyou_uchizei_gaku, denpyou_sotozei_gaku);
                                }
                                var totalSyouhizei = (Convert.ToDecimal(gyousha_syouhizei) + Convert.ToDecimal(denpyou_syouhizei)).ToString();

                                if (!string.IsNullOrEmpty(totalSyouhizei) && totalSyouhizei.Equals("0.0000"))
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
                                }
                                else
                                {
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(decimal.Parse(totalSyouhizei).ToString("#,##0"));
                                }
                                // CSVに出力場合明細行空の項目をstring.emptyに設定する
                                csvDataGridView = this.SetCsvRowRakuraku(csvDataGridView, false);
                                if (!string.IsNullOrEmpty(totalSyouhizei) && !totalSyouhizei.Equals("0.0000"))
                                {
                                    // 明細備考: 請求毎税は除く
                                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = ConstCls.SEIKYU_ZEI_EXCEPT;
                                }
                            }
                        }
                    }

                    if (tableNextRow == null || !startRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
                    {
                        // 請求毎消費税
                        if (seikyuDt.Rows.Count == rowCount || tableNextRow == null || !startRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"]))
                        {
                            if (isSeikyuuUchizei)
                            {
                                csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);

                                // 品名
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "【請求毎消費税(内)】";
                                // 消費税
                                string strKonkaiSeiUtizeiGaku = "0";
                                if (!(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] is DBNull))
                                {
                                    strKonkaiSeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"].ToString()).ToString("#,##0");
                                }
                                string syouhize = ConstCls.KAKKO_START + strKonkaiSeiUtizeiGaku + ConstCls.KAKKO_END;
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(syouhize);

                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvRowForSeikyuuZeiRakuraku(csvDataGridView);
                            }

                            if (isSeikyuuSotozei)
                            {
                                csvDataGridView = CreateCsvDgvRowAddRakuraku(csvDataGridView);

                                // 品名
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = "【請求毎消費税】";
                                // 消費税
                                string strKonkaiSeiSotozeiGaku = "0";
                                if (!(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] is DBNull))
                                {
                                    strKonkaiSeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"].ToString()).ToString("#,##0");
                                }
                                string syouhize = strKonkaiSeiSotozeiGaku;
                                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(syouhize);

                                // 【伝票毎消費税】や【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
                                this.SetCsvRowForSeikyuuZeiRakuraku(csvDataGridView);
                            }
                        }
                    }

                    nowKagamiNo = Convert.ToInt16(startRow["KAGAMI_NUMBER"]);
                }

                return csvDataGridView;
                #endregion
            }
        }

        /// <summary>
        /// CSVデータヘッダ生成
        /// </summary>
        private CustomDataGridView CreateCsvDgvRakuraku()
        {
            // csvDataGridView生成
            CustomDataGridView csvDataGridView = new CustomDataGridView();
            // csvヘッダ生成
            csvDataGridView.Columns.Add("0", "請求番号");
            csvDataGridView.Columns.Add("1", "帳票No");
            csvDataGridView.Columns.Add("2", "鑑番号");
            csvDataGridView.Columns.Add("3", "楽楽顧客コード");
            csvDataGridView.Columns.Add("4", "取引先CD");
            csvDataGridView.Columns.Add("5", "業者CD");
            csvDataGridView.Columns.Add("6", "現場CD");
            csvDataGridView.Columns.Add("7", "代表者印字区分");
            csvDataGridView.Columns.Add("8", "会社名");
            csvDataGridView.Columns.Add("9", "代表者名");
            csvDataGridView.Columns.Add("10", "拠点名印字区分");
            csvDataGridView.Columns.Add("11", "拠点CD");
            csvDataGridView.Columns.Add("12", "拠点名");
            csvDataGridView.Columns.Add("13", "拠点代表者名");
            csvDataGridView.Columns.Add("14", "拠点郵便番号");
            csvDataGridView.Columns.Add("15", "拠点住所1");
            csvDataGridView.Columns.Add("16", "拠点住所2");
            csvDataGridView.Columns.Add("17", "拠点TEL");
            csvDataGridView.Columns.Add("18", "拠点FAX");
            csvDataGridView.Columns.Add("19", "請求書送付先1");
            csvDataGridView.Columns.Add("20", "請求書送付先2");
            csvDataGridView.Columns.Add("21", "請求書送付先敬称1");
            csvDataGridView.Columns.Add("22", "請求書送付先敬称2");
            csvDataGridView.Columns.Add("23", "請求書送付先郵便番号");
            csvDataGridView.Columns.Add("24", "請求書送付先住所1");
            csvDataGridView.Columns.Add("25", "請求書送付先住所2");
            csvDataGridView.Columns.Add("26", "請求書送付先部署");
            csvDataGridView.Columns.Add("27", "請求書送付先担当者");
            csvDataGridView.Columns.Add("28", "請求書送付先TEL");
            csvDataGridView.Columns.Add("29", "請求書送付先FAX");
            csvDataGridView.Columns.Add("30", "請求担当者");
            csvDataGridView.Columns.Add("31", "請求年月日");
            csvDataGridView.Columns.Add("32", "前回繰越額");
            csvDataGridView.Columns.Add("33", "今回入金額");
            csvDataGridView.Columns.Add("34", "今回調整額");
            csvDataGridView.Columns.Add("35", "繰越額");
            csvDataGridView.Columns.Add("36", "今回売上額");
            csvDataGridView.Columns.Add("37", "今回請内税額");
            csvDataGridView.Columns.Add("38", "今回請外税額");
            csvDataGridView.Columns.Add("39", "今回伝内税額");
            csvDataGridView.Columns.Add("40", "今回伝外税額");
            csvDataGridView.Columns.Add("41", "今回明内税額");
            csvDataGridView.Columns.Add("42", "今回明外税額");
            csvDataGridView.Columns.Add("43", "今回消費税");
            csvDataGridView.Columns.Add("44", "今回取引額");
            csvDataGridView.Columns.Add("45", "今回御請求額");
            csvDataGridView.Columns.Add("46", "振込銀行1");
            csvDataGridView.Columns.Add("47", "振込支店1");
            csvDataGridView.Columns.Add("48", "振込口座種類1");
            csvDataGridView.Columns.Add("49", "振込口座番号1");
            csvDataGridView.Columns.Add("50", "振込口座名1");
            csvDataGridView.Columns.Add("51", "振込銀行2");
            csvDataGridView.Columns.Add("52", "振込支店2");
            csvDataGridView.Columns.Add("53", "振込口座種類2");
            csvDataGridView.Columns.Add("54", "振込口座番号2");
            csvDataGridView.Columns.Add("55", "振込口座名2");
            csvDataGridView.Columns.Add("56", "振込銀行3");
            csvDataGridView.Columns.Add("57", "振込支店3");
            csvDataGridView.Columns.Add("58", "振込口座種類3");
            csvDataGridView.Columns.Add("59", "振込口座番号3");
            csvDataGridView.Columns.Add("60", "振込口座名3");
            csvDataGridView.Columns.Add("61", "請求備考1");
            csvDataGridView.Columns.Add("62", "請求備考2");
            csvDataGridView.Columns.Add("63", "請求番号 ");
            csvDataGridView.Columns.Add("64", "鑑番号 ");
            csvDataGridView.Columns.Add("65", "行番号");
            csvDataGridView.Columns.Add("66", "伝票番号");
            csvDataGridView.Columns.Add("67", "伝票日付");
            csvDataGridView.Columns.Add("68", "取引先CD ");
            csvDataGridView.Columns.Add("69", "業者CD ");
            csvDataGridView.Columns.Add("70", "業者名1");
            csvDataGridView.Columns.Add("71", "業者名2");
            csvDataGridView.Columns.Add("72", "現場CD ");
            csvDataGridView.Columns.Add("73", "現場名1");
            csvDataGridView.Columns.Add("74", "現場名2");
            csvDataGridView.Columns.Add("75", "グループCD");
            csvDataGridView.Columns.Add("76", "グループ名");
            csvDataGridView.Columns.Add("77", "品名CD");
            csvDataGridView.Columns.Add("78", "品名");
            csvDataGridView.Columns.Add("79", "数量");
            csvDataGridView.Columns.Add("80", "単位CD");
            csvDataGridView.Columns.Add("81", "単位名");
            csvDataGridView.Columns.Add("82", "単価");
            csvDataGridView.Columns.Add("83", "金額");
            csvDataGridView.Columns.Add("84", "消費税");
            csvDataGridView.Columns.Add("85", "明細備考");

            return csvDataGridView;
        }

        /// <summary>
        /// DataGridView行追加時、前回行クリア対応
        /// </summary>
        private CustomDataGridView CreateCsvDgvRowAddRakuraku(CustomDataGridView csvDataGridView)
        {
            CustomDataGridView addDataGridView = CreateCsvDgvRakuraku();

            if (csvDataGridView.RowCount > 1)
            {
                addDataGridView.Rows.Add(csvDataGridView.RowCount - 1);
            }

            for (int i = 0; i <= csvDataGridView.RowCount - 1; i++)
            {
                for (int j = 0; j < ConstCls.RAKURAKU_CSV_COLUMNS_COUNT; j++)
                {
                    addDataGridView.Rows[i].Cells[j].Value = csvDataGridView.Rows[i].Cells[j].Value;
                }
            }

            csvDataGridView.Rows.Insert(csvDataGridView.RowCount - 1, 1);

            for (int j = 0; j <= addDataGridView.RowCount - 1; j++)
            {
                for (int i = 0; i < ConstCls.RAKURAKU_CSV_COLUMNS_COUNT; i++)
                {
                    csvDataGridView.Rows[j].Cells[i].Value = addDataGridView.Rows[j].Cells[i].Value;
                }
            }

            return csvDataGridView;
        }

        /// <summary>
        /// 単月請求データ設定
        /// </summary>
        private CustomDataGridView SetTangetuSeikyuCsvRakuraku(string shoshikiKbn, string shoshikiMeisaiKbn, DataRow startRow, CustomDataGridView csvDataGridView)
        {
            // 単価フォーマット
            string tankaFormat = this.tankaFormat();
            // 数量フォーマット
            string suuryouFormat = this.suuryouFormat();

            // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
            bool isNyuukin = startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

            // 請求番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[0].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 帳票No
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[1].Value = startRow["SEIKYUU_NUMBER"].ToString() + "_" + startRow["KAGAMI_NUMBER"].ToString();
            // 鑑番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[2].Value = startRow["KAGAMI_NUMBER"].ToString();
            // 楽楽顧客コード
            string rakurakuCode = string.Empty;
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 1の場合
            if (shoshikiKbn.Equals("1"))
            {
                var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(startRow["TSDK_TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    rakurakuCode = torihikisakiSeikyuu.RAKURAKU_CUSTOMER_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 2の場合
            if (shoshikiKbn.Equals("2"))
            {
                var gyousha = mgyoushaDao.GetDataByCd(startRow["TSDK_GYOUSHA_CD"].ToString());
                if (gyousha != null)
                {
                    rakurakuCode = gyousha.RAKURAKU_CUSTOMER_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 3の場合
            if (shoshikiKbn.Equals("3"))
            {
                var data = new M_GENBA();
                data.GYOUSHA_CD = startRow["TSDK_GYOUSHA_CD"].ToString();
                data.GENBA_CD = startRow["TSDK_GENBA_CD"].ToString();

                var genba = mgenbaDao.GetDataByCd(data);
                if (genba != null)
                {
                    rakurakuCode = genba.RAKURAKU_CUSTOMER_CD;
                }
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[3].Value = rakurakuCode;
            // 取引先CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[4].Value = startRow["TSDK_TORIHIKISAKI_CD"].ToString();
            // 業者CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[5].Value = startRow["TSDK_GYOUSHA_CD"].ToString();
            // 現場CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[6].Value = startRow["TSDK_GENBA_CD"].ToString();
            // 代表者印字区分
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[7].Value = startRow["DAIHYOU_PRINT_KBN"].ToString();
            // 会社名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[8].Value = startRow["CORP_NAME"].ToString();
            // 代表者名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[9].Value = startRow["CORP_DAIHYOU"].ToString();
            // 拠点名印字区分
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[10].Value = startRow["KYOTEN_NAME_PRINT_KBN"].ToString();
            // 拠点CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[11].Value = startRow["KYOTEN_CD"].ToString();
            // 拠点名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[12].Value = startRow["KYOTEN_NAME"].ToString();
            // 拠点代表者名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[13].Value = startRow["KYOTEN_DAIHYOU"].ToString();
            // 拠点郵便番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = startRow["KYOTEN_POST"].ToString();
            // 拠点住所1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = startRow["KYOTEN_ADDRESS1"].ToString();
            // 拠点住所2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[16].Value = startRow["KYOTEN_ADDRESS2"].ToString();
            // 拠点TEL
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = startRow["KYOTEN_TEL"].ToString();
            // 拠点FAX
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = startRow["KYOTEN_FAX"].ToString();
            // 請求書送付先1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = startRow["SEIKYUU_SOUFU_NAME1"].ToString();
            // 請求書送付先2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = startRow["SEIKYUU_SOUFU_NAME2"].ToString();
            // 請求書送付先敬称1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = startRow["SEIKYUU_SOUFU_KEISHOU1"].ToString();
            // 請求書送付先敬称2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = startRow["SEIKYUU_SOUFU_KEISHOU2"].ToString();
            // 請求書送付先郵便番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = startRow["SEIKYUU_SOUFU_POST"].ToString();
            // 請求書送付先住所1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = startRow["SEIKYUU_SOUFU_ADDRESS1"].ToString();
            // 請求書送付先住所2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = startRow["SEIKYUU_SOUFU_ADDRESS2"].ToString();
            // 請求書送付先部署
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = startRow["SEIKYUU_SOUFU_BUSHO"].ToString();
            // 請求書送付先担当者
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = startRow["SEIKYUU_SOUFU_TANTOU"].ToString();
            // 請求書送付先TEL
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = startRow["SEIKYUU_SOUFU_TEL"].ToString();
            // 請求書送付先FAX
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = startRow["SEIKYUU_SOUFU_FAX"].ToString();
            // 請求担当者
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[30].Value = startRow["SEIKYUU_TANTOU"].ToString();
            // 請求年月日
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[31].Value = ((DateTime)startRow["SEIKYUU_DATE"]).ToString("yyyy/MM/dd");

            // 前回繰越額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[32].Value = string.Empty;
            // 今回入金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[33].Value = string.Empty;
            // 今回調整額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[34].Value = string.Empty;
            // 繰越額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[35].Value = string.Empty;

            // 今回売上額
            string strKonkaiUriageGaku = "0";
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                strKonkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[36].Value = strKonkaiUriageGaku;
            // 今回請内税額
            string strKonkaiSeiUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiSeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[37].Value = strKonkaiSeiUtizeiGaku;
            // 今回請外税額
            string strKonkaiSeiSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiSeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[38].Value = strKonkaiSeiSotozeiGaku;
            // 今回伝内税額
            string strKonkaiDenUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_DEN_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiDenUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_DEN_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[39].Value = strKonkaiDenUtizeiGaku;
            // 今回伝外税額
            string strKonkaiDenSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_DEN_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiDenSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_DEN_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[40].Value = strKonkaiDenSotozeiGaku;
            // 今回明内税額
            string strKonkaiMeiUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_MEI_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiMeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_MEI_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[41].Value = strKonkaiMeiUtizeiGaku;
            // 今回明外税額
            string strKonkaiMeiSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_MEI_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiMeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_MEI_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[42].Value = strKonkaiMeiSotozeiGaku;
            // 今回消費税額
            string strSyouhizeiGaku = "0";
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                strSyouhizeiGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[43].Value = strSyouhizeiGaku;
            // 今回取引額 = 今回売上額 + 今回消費税額
            decimal konkaiUriageGaku = 0;
            decimal syouhizeGaku = 0;
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                konkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString());
            }
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                syouhizeGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[44].Value = (konkaiUriageGaku + syouhizeGaku).ToString("##0");
            // 今回御請求額 (請求先別 --> 今回御請求額, 業者別/現場別 --> 今回取引額)
            decimal konkaiSeikyuuGaku = 0;
            if (shoshikiKbn.Equals("1"))
            {
                if (!(startRow["TSD_KONKAI_URIAGE_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_URIAGE_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_SEI_UTIZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_SEI_UTIZEI_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_SEI_SOTOZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_SEI_SOTOZEI_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_DEN_UTIZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_DEN_UTIZEI_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_DEN_SOTOZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_DEN_SOTOZEI_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_MEI_UTIZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_MEI_UTIZEI_GAKU"].ToString());
                }
                if (!(startRow["TSD_KONKAI_MEI_SOTOZEI_GAKU"] is DBNull))
                {
                    konkaiSeikyuuGaku += decimal.Parse(startRow["TSD_KONKAI_MEI_SOTOZEI_GAKU"].ToString());
                }
            }
            else
            {
                konkaiSeikyuuGaku = konkaiUriageGaku + syouhizeGaku;
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[45].Value = konkaiSeikyuuGaku.ToString("##0");

            //振込銀行1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[46].Value = startRow["FURIKOMI_BANK_NAME"].ToString();
            //振込支店1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[47].Value = startRow["FURIKOMI_BANK_SHITEN_NAME"].ToString();
            //振込口座種類1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[48].Value = startRow["KOUZA_SHURUI"].ToString();
            //振込口座番号1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[49].Value = startRow["KOUZA_NO"].ToString();
            //振込口座名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[50].Value = startRow["KOUZA_NAME"].ToString();

            //振込銀行2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[51].Value = startRow["FURIKOMI_BANK_NAME_2"].ToString();
            //振込支店2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[52].Value = startRow["FURIKOMI_BANK_SHITEN_NAME_2"].ToString();
            //振込口座種類2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[53].Value = startRow["KOUZA_SHURUI_2"].ToString();
            //振込口座番号2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[54].Value = startRow["KOUZA_NO_2"].ToString();
            //振込口座名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[55].Value = startRow["KOUZA_NAME_2"].ToString();

            //振込銀行3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[56].Value = startRow["FURIKOMI_BANK_NAME_3"].ToString();
            //振込支店3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[57].Value = startRow["FURIKOMI_BANK_SHITEN_NAME_3"].ToString();
            //振込口座種類3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[58].Value = startRow["KOUZA_SHURUI_3"].ToString();
            //振込口座番号3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[59].Value = startRow["KOUZA_NO_3"].ToString();
            //振込口座名3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[60].Value = startRow["KOUZA_NAME_3"].ToString();

            //請求備考1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[61].Value = startRow["BIKOU_1"].ToString();
            //請求備考2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[62].Value = startRow["BIKOU_2"].ToString();

            // 請求番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[63].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 鑑番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[64].Value = startRow["KAGAMI_NUMBER"].ToString();
            // 行番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[65].Value = startRow["ROW_NUMBER"].ToString();
            // 伝票番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[66].Value = startRow["DENPYOU_NUMBER"].ToString();
            // 伝票日付
            if (!string.IsNullOrEmpty(Convert.ToString(startRow["DENPYOU_DATE"])))
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = ((DateTime)startRow["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = string.Empty;
            }
            // 取引先CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[68].Value = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
            // 業者CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[69].Value = startRow["TSDE_GYOUSHA_CD"].ToString();
            // 業者名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[70].Value = startRow["GYOUSHA_NAME1"].ToString();
            // 業者名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[71].Value = startRow["GYOUSHA_NAME2"].ToString();
            // 現場CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[72].Value = startRow["TSDE_GENBA_CD"].ToString();
            // 現場名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[73].Value = startRow["GENBA_NAME1"].ToString();
            // 現場名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[74].Value = startRow["GENBA_NAME2"].ToString();
            // グループ
            string groupCd = string.Empty;
            string groupNm = string.Empty;
            if (isNyuukin)
            {
                // 取引先CD
                groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
                // 取引先名1+取引先名2
                var torisaki = mtorihikisakiDao.GetDataByCd(groupCd);
                if (torisaki != null)
                {
                    groupNm = torisaki.TORIHIKISAKI_NAME1 + ConstCls.ZENKAKU_SPACE + torisaki.TORIHIKISAKI_NAME2;
                }
            }
            else
            {
                if (shoshikiKbn.Equals("1"))
                {
                    if (shoshikiMeisaiKbn.Equals("1"))
                    {
                        // 取引先CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
                        // 取引先名1+取引先名2
                        var torisaki = mtorihikisakiDao.GetDataByCd(groupCd);
                        if (torisaki != null)
                        {
                            groupNm = torisaki.TORIHIKISAKI_NAME1 + ConstCls.ZENKAKU_SPACE + torisaki.TORIHIKISAKI_NAME2;
                        }
                    }
                    if (shoshikiMeisaiKbn.Equals("2"))
                    {
                        // 取引先CD+業者CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString();
                        // 業者名1+業者名2
                        groupNm = startRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GYOUSHA_NAME2"].ToString();
                    }
                    if (shoshikiMeisaiKbn.Equals("3"))
                    {
                        // 取引先CD+業者CD+現場CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                        // 現場名1+現場名2
                        groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                    }
                }
                if (shoshikiKbn.Equals("2"))
                {
                    if (shoshikiMeisaiKbn.Equals("1"))
                    {
                        // 取引先CD+業者CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString();
                        // 業者名1+業者名2
                        groupNm = startRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GYOUSHA_NAME2"].ToString();
                    }
                    if (shoshikiMeisaiKbn.Equals("3"))
                    {
                        // 取引先CD+業者CD+現場CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                        // 現場名1+現場名2
                        groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                    }
                }
                if (shoshikiKbn.Equals("3"))
                {
                    // 取引先CD+業者CD+現場CD
                    groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                    // 現場名1+現場名2
                    groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                }
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[75].Value = groupCd;
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[76].Value = groupNm;
            // 品名CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[77].Value = startRow["HINMEI_CD"] is DBNull ? string.Empty : startRow["HINMEI_CD"].ToString();
            // 品名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = startRow["HINMEI_NAME"] is DBNull ? string.Empty : startRow["HINMEI_NAME"].ToString();
            // 数量, 単価
            if (startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10))
            {
                // 入金明細の数量、単価は「ブランク」とする。
                // 数量
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = string.Empty;
                // 単価
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = string.Empty;
            }
            else
            {
                // 数量
                decimal suuryou = 0;
                if (!(startRow["SUURYOU"] is DBNull))
                {
                    suuryou = decimal.Parse(startRow["SUURYOU"].ToString());
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = suuryou.ToString(suuryouFormat);
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = null;
                }
                // 単価
                decimal tanka = 0;
                if (!(startRow["TANKA"] is DBNull))
                {
                    tanka = decimal.Parse(startRow["TANKA"].ToString());
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = tanka.ToString(tankaFormat);
            }
            // 単位CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[80].Value = startRow["UNIT_CD"] is DBNull ? string.Empty : startRow["UNIT_CD"].ToString();
            // 単位名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[81].Value = startRow["UNIT_NAME"] is DBNull ? string.Empty : startRow["UNIT_NAME"].ToString();
            // 金額
            string strkinGaku = "0";
            if (!(startRow["KINGAKU"] is DBNull))
            {
                strkinGaku = decimal.Parse(startRow["KINGAKU"].ToString()).ToString("#,##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(strkinGaku);
            // 消費税
            string meisaizei = string.Empty;
            decimal uchizeGaku = 0;
            decimal sotozeiGaku = 0;
            if (!(startRow["UCHIZEI_GAKU"] is DBNull))
            {
                uchizeGaku = decimal.Parse(startRow["UCHIZEI_GAKU"].ToString());
            }
            if (!(startRow["SOTOZEI_GAKU"] is DBNull))
            {
                sotozeiGaku = decimal.Parse(startRow["SOTOZEI_GAKU"].ToString());
            }
            if (uchizeGaku > 0) // 内税
            {
                meisaizei = ConstCls.KAKKO_START + uchizeGaku.ToString("#,##0") + ConstCls.KAKKO_END;
            }
            if (sotozeiGaku > 0) // 外税
            {
                meisaizei = sotozeiGaku.ToString("#,##0");
            }
            if (!string.IsNullOrEmpty(meisaizei))
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(meisaizei);
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
            }
            // 明細備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = startRow["MEISAI_BIKOU"].ToString();

            return csvDataGridView;
        }

        /// <summary>
        /// 繰越請求データ設定
        /// </summary>
        private CustomDataGridView SetKurikosiSeikyuCsvRakuraku(string shoshikiKbn, string shoshikiMeisaiKbn, DataRow startRow, CustomDataGridView csvDataGridView)
        {
            // 単価フォーマット
            string tankaFormat = this.tankaFormat();
            // 数量フォーマット
            string suuryouFormat = this.suuryouFormat();

            // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
            bool isNyuukin = startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

            // 請求番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[0].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 帳票No
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[1].Value = startRow["SEIKYUU_NUMBER"].ToString() + "_" + startRow["KAGAMI_NUMBER"].ToString();
            // 鑑番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[2].Value = startRow["KAGAMI_NUMBER"].ToString();
            // 楽楽顧客コード
            string rakurakuCode = string.Empty;
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 1の場合
            if (shoshikiKbn.Equals("1"))
            {
                var torihikisakiSeikyuu = TorihikisakiSeikyuuDao.GetDataByCd(startRow["TSDK_TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    rakurakuCode = torihikisakiSeikyuu.RAKURAKU_CUSTOMER_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 2の場合
            if (shoshikiKbn.Equals("2"))
            {
                var gyousha = mgyoushaDao.GetDataByCd(startRow["TSDK_GYOUSHA_CD"].ToString());
                if (gyousha != null)
                {
                    rakurakuCode = gyousha.RAKURAKU_CUSTOMER_CD;
                }
            }
            // T_SEIKYUU_DENPYOU.SHOSHIKI_KBN = 3の場合
            if (shoshikiKbn.Equals("3"))
            {
                var data = new M_GENBA();
                data.GYOUSHA_CD = startRow["TSDK_GYOUSHA_CD"].ToString();
                data.GENBA_CD = startRow["TSDK_GENBA_CD"].ToString();

                var genba = mgenbaDao.GetDataByCd(data);
                if (genba != null)
                {
                    rakurakuCode = genba.RAKURAKU_CUSTOMER_CD;
                }
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[3].Value = rakurakuCode;
            // 取引先CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[4].Value = startRow["TSDK_TORIHIKISAKI_CD"].ToString();
            // 業者CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[5].Value = startRow["TSDK_GYOUSHA_CD"].ToString();
            // 現場CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[6].Value = startRow["TSDK_GENBA_CD"].ToString();
            // 代表者印字区分
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[7].Value = startRow["DAIHYOU_PRINT_KBN"].ToString();
            // 会社名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[8].Value = startRow["CORP_NAME"].ToString();
            // 代表者名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[9].Value = startRow["CORP_DAIHYOU"].ToString();
            // 拠点名印字区分
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[10].Value = startRow["KYOTEN_NAME_PRINT_KBN"].ToString();
            // 拠点CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[11].Value = startRow["KYOTEN_CD"].ToString();
            // 拠点名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[12].Value = startRow["KYOTEN_NAME"].ToString();
            // 拠点代表者名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[13].Value = startRow["KYOTEN_DAIHYOU"].ToString();
            // 拠点郵便番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[14].Value = startRow["KYOTEN_POST"].ToString();
            // 拠点住所1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[15].Value = startRow["KYOTEN_ADDRESS1"].ToString();
            // 拠点住所2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[16].Value = startRow["KYOTEN_ADDRESS2"].ToString();
            // 拠点TEL
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[17].Value = startRow["KYOTEN_TEL"].ToString();
            // 拠点FAX
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[18].Value = startRow["KYOTEN_FAX"].ToString();
            // 請求書送付先1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[19].Value = startRow["SEIKYUU_SOUFU_NAME1"].ToString();
            // 請求書送付先2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[20].Value = startRow["SEIKYUU_SOUFU_NAME2"].ToString();
            // 請求書送付先敬称1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[21].Value = startRow["SEIKYUU_SOUFU_KEISHOU1"].ToString();
            // 請求書送付先敬称2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[22].Value = startRow["SEIKYUU_SOUFU_KEISHOU2"].ToString();
            // 請求書送付先郵便番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[23].Value = startRow["SEIKYUU_SOUFU_POST"].ToString();
            // 請求書送付先住所1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[24].Value = startRow["SEIKYUU_SOUFU_ADDRESS1"].ToString();
            // 請求書送付先住所2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[25].Value = startRow["SEIKYUU_SOUFU_ADDRESS2"].ToString();
            // 請求書送付先部署
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[26].Value = startRow["SEIKYUU_SOUFU_BUSHO"].ToString();
            // 請求書送付先担当者
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[27].Value = startRow["SEIKYUU_SOUFU_TANTOU"].ToString();
            // 請求書送付先TEL
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[28].Value = startRow["SEIKYUU_SOUFU_TEL"].ToString();
            // 請求書送付先FAX
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[29].Value = startRow["SEIKYUU_SOUFU_FAX"].ToString();
            // 請求担当者
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[30].Value = startRow["SEIKYUU_TANTOU"].ToString();
            // 請求年月日
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[31].Value = ((DateTime)startRow["SEIKYUU_DATE"]).ToString("yyyy/MM/dd");

            // 前回繰越額
            string strZenkaiKurikosiGaku = "0";
            if (!(startRow["ZENKAI_KURIKOSI_GAKU"] is DBNull))
            {
                strZenkaiKurikosiGaku = decimal.Parse(startRow["ZENKAI_KURIKOSI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[32].Value = strZenkaiKurikosiGaku;
            // 今回入金額
            string strKonkaiNyuukinGaku = "0";
            if (!(startRow["KONKAI_NYUUKIN_GAKU"] is DBNull))
            {
                strKonkaiNyuukinGaku = decimal.Parse(startRow["KONKAI_NYUUKIN_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[33].Value = strKonkaiNyuukinGaku;
            // 今回調整額
            string strKonkaiChouseiGaku = "0";
            if (!(startRow["KONKAI_CHOUSEI_GAKU"] is DBNull))
            {
                strKonkaiChouseiGaku = decimal.Parse(startRow["KONKAI_CHOUSEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[34].Value = strKonkaiChouseiGaku;
            // 繰越額
            string strKurikosiGaku = "0";
            if (!(startRow["SASIHIKIGAKU"] is DBNull))
            {
                strKurikosiGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[35].Value = strKurikosiGaku;

            // 今回売上額
            string strKonkaiUriageGaku = "0";
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                strKonkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[36].Value = strKonkaiUriageGaku;
            // 今回請内税額
            string strKonkaiSeiUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiSeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[37].Value = strKonkaiSeiUtizeiGaku;
            // 今回請外税額
            string strKonkaiSeiSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiSeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[38].Value = strKonkaiSeiSotozeiGaku;
            // 今回伝内税額
            string strKonkaiDenUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_DEN_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiDenUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_DEN_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[39].Value = strKonkaiDenUtizeiGaku;
            // 今回伝外税額
            string strKonkaiDenSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_DEN_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiDenSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_DEN_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[40].Value = strKonkaiDenSotozeiGaku;
            // 今回明内税額
            string strKonkaiMeiUtizeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_MEI_UTIZEI_GAKU"] is DBNull))
            {
                strKonkaiMeiUtizeiGaku = decimal.Parse(startRow["TSDK_KONKAI_MEI_UTIZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[41].Value = strKonkaiMeiUtizeiGaku;
            // 今回明外税額
            string strKonkaiMeiSotozeiGaku = "0";
            if (!(startRow["TSDK_KONKAI_MEI_SOTOZEI_GAKU"] is DBNull))
            {
                strKonkaiMeiSotozeiGaku = decimal.Parse(startRow["TSDK_KONKAI_MEI_SOTOZEI_GAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[42].Value = strKonkaiMeiSotozeiGaku;
            // 今回消費税額
            string strSyouhizeiGaku = "0";
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                strSyouhizeiGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString()).ToString("##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[43].Value = strSyouhizeiGaku;
            // 今回取引額 = 今回売上額 + 今回消費税額
            decimal konkaiUriageGaku = 0;
            decimal syouhizeGaku = 0;
            if (!(startRow["TSDK_KONKAI_URIAGE_GAKU"] is DBNull))
            {
                konkaiUriageGaku = decimal.Parse(startRow["TSDK_KONKAI_URIAGE_GAKU"].ToString());
            }
            if (!(startRow["SYOUHIZEIGAKU"] is DBNull))
            {
                syouhizeGaku = decimal.Parse(startRow["SYOUHIZEIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[44].Value = (konkaiUriageGaku + syouhizeGaku).ToString("##0");
            // 今回御請求額 = 繰越額 + 今回取引額
            decimal kurikosiGaku = 0;
            if (!(startRow["SASIHIKIGAKU"] is DBNull))
            {
                kurikosiGaku = decimal.Parse(startRow["SASIHIKIGAKU"].ToString());
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[45].Value = (konkaiUriageGaku + syouhizeGaku + kurikosiGaku).ToString("##0");

            // 振込銀行1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[46].Value = startRow["FURIKOMI_BANK_NAME"].ToString();
            // 振込支店1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[47].Value = startRow["FURIKOMI_BANK_SHITEN_NAME"].ToString();
            // 振込口座種類1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[48].Value = startRow["KOUZA_SHURUI"].ToString();
            // 振込口座番号1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[49].Value = startRow["KOUZA_NO"].ToString();
            // 振込口座名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[50].Value = startRow["KOUZA_NAME"].ToString();

            //振込銀行2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[51].Value = startRow["FURIKOMI_BANK_NAME_2"].ToString();
            //振込支店2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[52].Value = startRow["FURIKOMI_BANK_SHITEN_NAME_2"].ToString();
            //振込口座種類2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[53].Value = startRow["KOUZA_SHURUI_2"].ToString();
            //振込口座番号2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[54].Value = startRow["KOUZA_NO_2"].ToString();
            //振込口座名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[55].Value = startRow["KOUZA_NAME_2"].ToString();

            //振込銀行3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[56].Value = startRow["FURIKOMI_BANK_NAME_3"].ToString();
            //振込支店3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[57].Value = startRow["FURIKOMI_BANK_SHITEN_NAME_3"].ToString();
            //振込口座種類3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[58].Value = startRow["KOUZA_SHURUI_3"].ToString();
            //振込口座番号3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[59].Value = startRow["KOUZA_NO_3"].ToString();
            //振込口座名3
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[60].Value = startRow["KOUZA_NAME_3"].ToString();

            //請求備考1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[61].Value = startRow["BIKOU_1"].ToString();
            //請求備考2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[62].Value = startRow["BIKOU_2"].ToString();

            // 請求番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[63].Value = startRow["SEIKYUU_NUMBER"].ToString();
            // 鑑番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[64].Value = startRow["KAGAMI_NUMBER"].ToString();
            // 行番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[65].Value = startRow["ROW_NUMBER"].ToString();
            // 伝票番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[66].Value = startRow["DENPYOU_NUMBER"].ToString();
            // 伝票日付
            if (!string.IsNullOrEmpty(Convert.ToString(startRow["DENPYOU_DATE"])))
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = ((DateTime)startRow["DENPYOU_DATE"]).ToString("yyyy/MM/dd");
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = string.Empty;
            }
            // 取引先CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[68].Value = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
            // 業者CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[69].Value = startRow["TSDE_GYOUSHA_CD"].ToString();
            // 業者名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[70].Value = startRow["GYOUSHA_NAME1"].ToString();
            // 業者名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[71].Value = startRow["GYOUSHA_NAME2"].ToString();
            // 現場CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[72].Value = startRow["TSDE_GENBA_CD"].ToString();
            // 現場名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[73].Value = startRow["GENBA_NAME1"].ToString();
            // 現場名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[74].Value = startRow["GENBA_NAME2"].ToString();
            // グループ
            string groupCd = string.Empty;
            string groupNm = string.Empty;
            if (isNyuukin)
            {
                // 取引先CD
                groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
                // 取引先名1+取引先名2
                var torisaki = mtorihikisakiDao.GetDataByCd(groupCd);
                if (torisaki != null)
                {
                    groupNm = torisaki.TORIHIKISAKI_NAME1 + ConstCls.ZENKAKU_SPACE + torisaki.TORIHIKISAKI_NAME2;
                }
            }
            else
            {
                if (shoshikiKbn.Equals("1"))
                {
                    if (shoshikiMeisaiKbn.Equals("1"))
                    {
                        // 取引先CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString();
                        // 取引先名1+取引先名2
                        var torisaki = mtorihikisakiDao.GetDataByCd(groupCd);
                        if (torisaki != null)
                        {
                            groupNm = torisaki.TORIHIKISAKI_NAME1 + ConstCls.ZENKAKU_SPACE + torisaki.TORIHIKISAKI_NAME2;
                        }
                    }
                    if (shoshikiMeisaiKbn.Equals("2"))
                    {
                        // 取引先CD+業者CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString();
                        // 業者名1+業者名2
                        groupNm = startRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GYOUSHA_NAME2"].ToString();
                    }
                    if (shoshikiMeisaiKbn.Equals("3"))
                    {
                        // 取引先CD+業者CD+現場CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                        // 現場名1+現場名2
                        groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                    }
                }
                if (shoshikiKbn.Equals("2"))
                {
                    if (shoshikiMeisaiKbn.Equals("1"))
                    {
                        // 取引先CD+業者CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString();
                        // 業者名1+業者名2
                        groupNm = startRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GYOUSHA_NAME2"].ToString();
                    }
                    if (shoshikiMeisaiKbn.Equals("3"))
                    {
                        // 取引先CD+業者CD+現場CD
                        groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                        // 現場名1+現場名2
                        groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                    }
                }
                if (shoshikiKbn.Equals("3"))
                {
                    // 取引先CD+業者CD+現場CD
                    groupCd = startRow["TSDE_TORIHIKISAKI_CD"].ToString() + startRow["TSDE_GYOUSHA_CD"].ToString() + startRow["TSDE_GENBA_CD"].ToString();
                    // 現場名1+現場名2
                    groupNm = startRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + startRow["GENBA_NAME2"].ToString();
                }
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[75].Value = groupCd;
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[76].Value = groupNm;
            // 品名CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[77].Value = startRow["HINMEI_CD"] is DBNull ? string.Empty : startRow["HINMEI_CD"].ToString();
            // 品名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[78].Value = startRow["HINMEI_NAME"] is DBNull ? string.Empty : startRow["HINMEI_NAME"].ToString();
            // 数量, 単価
            if (startRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10))
            {
                // 入金明細の数量、単価は「ブランク」とする。
                // 数量
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = string.Empty;
                // 単価
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = string.Empty;
            }
            else
            {
                // 数量
                decimal suuryou = 0;
                if (!(startRow["SUURYOU"] is DBNull))
                {
                    suuryou = decimal.Parse(startRow["SUURYOU"].ToString());
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = suuryou.ToString(suuryouFormat);
                }
                else
                {
                    csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = null;
                }
                // 単価
                decimal tanka = 0;
                if (!(startRow["TANKA"] is DBNull))
                {
                    tanka = decimal.Parse(startRow["TANKA"].ToString());
                }
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = tanka.ToString(tankaFormat);
            }
            // 単位CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[80].Value = startRow["UNIT_CD"] is DBNull ? string.Empty : startRow["UNIT_CD"].ToString();
            // 単位名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[81].Value = startRow["UNIT_NAME"] is DBNull ? string.Empty : startRow["UNIT_NAME"].ToString();
            // 金額
            string strkinGaku = "0";
            if (!(startRow["KINGAKU"] is DBNull))
            {
                strkinGaku = decimal.Parse(startRow["KINGAKU"].ToString()).ToString("#,##0");
            }
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = KingakuQuotes(strkinGaku);
            // 消費税
            string meisaizei = string.Empty;
            decimal uchizeGaku = 0;
            decimal sotozeiGaku = 0;
            if (!(startRow["UCHIZEI_GAKU"] is DBNull))
            {
                uchizeGaku = decimal.Parse(startRow["UCHIZEI_GAKU"].ToString());
            }
            if (!(startRow["SOTOZEI_GAKU"] is DBNull))
            {
                sotozeiGaku = decimal.Parse(startRow["SOTOZEI_GAKU"].ToString());
            }
            if (uchizeGaku > 0) // 内税
            {
                meisaizei = ConstCls.KAKKO_START + uchizeGaku.ToString("#,##0") + ConstCls.KAKKO_END;
            }
            if (sotozeiGaku > 0) // 外税
            {
                meisaizei = sotozeiGaku.ToString("#,##0");
            }
            if (!string.IsNullOrEmpty(meisaizei))
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = KingakuQuotes(meisaizei);
            }
            else
            {
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
            }
            // 明細備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = startRow["MEISAI_BIKOU"].ToString();

            return csvDataGridView;
        }

        /// <summary>
        /// 【請求毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
        /// </summary>
        private CustomDataGridView SetCsvRowForSeikyuuZeiRakuraku(CustomDataGridView csvDataGridView)
        {
            // 伝票番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[66].Value = string.Empty;
            // 伝票日付
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = string.Empty;
            // 取引先CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[68].Value = string.Empty;
            // 業者CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[69].Value = string.Empty;
            // 業者名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[70].Value = string.Empty;
            // 業者名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[71].Value = string.Empty;
            // 現場CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[72].Value = string.Empty;
            // 現場名1
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[73].Value = string.Empty;
            // 現場名2
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[74].Value = string.Empty;
            // 品名CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[77].Value = string.Empty;
            // 数量
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = string.Empty;
            // 単位CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[80].Value = string.Empty;
            // 単位名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[81].Value = string.Empty;
            // 単価
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = string.Empty;
            // 金額
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = null;
            // 明細備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = string.Empty;

            return csvDataGridView;
        }

        /// <summary>
        /// 入金計などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
        /// </summary>
        private CustomDataGridView SetCsvRowForNyuukinRakuraku(CustomDataGridView csvDataGridView)
        {
            // 伝票番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[66].Value = string.Empty;
            // 伝票日付
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = string.Empty;
            // 品名CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[77].Value = string.Empty;
            // 数量
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = string.Empty;
            // 単位CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[80].Value = string.Empty;
            // 単位名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[81].Value = string.Empty;
            // 単価
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = string.Empty;
            // 消費税
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[84].Value = null;
            // 明細備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = string.Empty;

            return csvDataGridView;
        }

        /// <summary>
        /// 業者計や現場計や【伝票毎消費税】などの消費税の行がCSVに出力場合明細行空の項目をstring.emptyに設定する
        /// </summary>
        private CustomDataGridView SetCsvRowRakuraku(CustomDataGridView csvDataGridView, bool isDenpyouZei)
        {
            // 伝票番号
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[66].Value = string.Empty;
            // 伝票日付
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[67].Value = string.Empty;
            // 品名CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[77].Value = string.Empty;
            // 数量
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[79].Value = string.Empty;
            // 単位CD
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[80].Value = string.Empty;
            // 単位名
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[81].Value = string.Empty;
            // 単価
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[82].Value = string.Empty;
            if (isDenpyouZei)
            {
                // 金額
                csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[83].Value = null;
            }
            // 明細備考
            csvDataGridView.Rows[csvDataGridView.RowCount - 1].Cells[85].Value = string.Empty;

            return csvDataGridView;
        }

        /// <summary>
        /// Copies rows from a DataGridView into another DataGridView.
        /// </summary>
        private void CopyRowsToDataGridView(CustomDataGridView targetDgv, CustomDataGridView copyDgv)
        {
            if (copyDgv.RowCount > 1) // Except headers
            {
                for (int i = 1; i < copyDgv.RowCount; i++)
                {
                    var row = CopyDataGridViewRow(copyDgv.Rows[i]);
                    targetDgv.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// Copies a DataGridViewRow.
        /// </summary>
        private DataGridViewRow CopyDataGridViewRow(DataGridViewRow source)
        {
            var row = source.Clone() as DataGridViewRow;

            foreach (DataGridViewCell cell in source.Cells)
            {
                row.Cells[cell.ColumnIndex].Value = cell.Value;
            }

            return row;
        }

        private string KingakuQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        #endregion
        /// <summary>
        /// 請求伝票データ設定（適格請求書用）
        /// </summary>
        /// <param name="seikyuDt">請求伝票データテーブル</param>
        /// <param name="dto">請求書発行DTO</param>
        public static ArrayList SetSeikyuDenpyo_invoice(DataTable seikyuDt, SeikyuuDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "", bool ZeiHyouji = false)
        {
            ArrayList result = new ArrayList();
            int count = 0;
            ArrayList list;
            FormReport formReport;

            ReportInfoR770 reportR770;
            DataTable denpyouPrintTable = CreateSeikyuuPrintTable();
            DataTable nyuukinPrintTable = CreateSeikyuuPrintTable();

            //先頭行の鏡番号を取得
            DataRow startRow = seikyuDt.Rows[0];

            List<DataTable> csvDtList = new List<DataTable>();

            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.NYUKIN_MEISAI_NASHI && string.IsNullOrEmpty(startRow["KAGAMI_NUMBER"].ToString()))
            {
                #region dto.Meisai = NYUKIN_MEISAI_NASHI
                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR770 = new ReportInfoR770();
                reportR770.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                reportR770.Title = "請求書()";
                // XPSプロパティ - 発行済み
                reportR770.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                var drEmpty = denpyouPrintTable.NewRow();
                denpyouPrintTable.Rows.Add(drEmpty);

                if (printFlg)
                {
                    reportR770.CreateReportData(dto, startRow, denpyouPrintTable, nyuukinPrintTable);

                    /* 即時XPS出力 */
                    list = new ArrayList();
                    list.Add(reportR770);
                    formReport = CreateFormReport_invoice(dto, list);

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2; ;//add Direct Print option refs #158002
                        formReport.PrintXPS();
                    }
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR770.CreateCsvData(dto, startRow, denpyouPrintTable, nyuukinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
                #endregion
            }
            else
            {
                #region dto.Meisai != NYUKIN_MEISAI_NASHI

                int nowKagamiNo = Convert.ToInt32(startRow["KAGAMI_NUMBER"]);

                for (int i = 0; i < seikyuDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = seikyuDt.Rows[i];

                    //鏡番号が同じか
                    if (Convert.ToInt32(tableRow["KAGAMI_NUMBER"]) != nowKagamiNo)
                    {
                        //帳票出力データテーブルを帳票出力データArrayListに格納
                        reportR770 = new ReportInfoR770();
                        reportR770.MSysInfo = dto.MSysInfo;

                        // XPSプロパティ - タイトル(取引先も表示させる)
                        DataRow[] rows = seikyuDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                        reportR770.Title = "請求書(" + rows[0]["SEIKYUU_SOUFU_NAME1"] + ")";
                        // XPSプロパティ - 発行済み
                        reportR770.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                        if (printFlg)
                        {
                            reportR770.CreateReportData(dto, rows[0], denpyouPrintTable, nyuukinPrintTable);

                            // 即時XPS出力
                            list = new ArrayList();
                            list.Add(reportR770);
                            formReport = CreateFormReport_invoice(dto, list);

                            if (!isExportPDF)
                            {
                                // 印刷アプリ初期動作(プレビュー)
                                formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add Direct Print option refs #158002
                                formReport.PrintXPS();
                            }
                        }
                        else
                        {
                            DataTable dtData = reportR770.CreateCsvData(dto, rows[0], denpyouPrintTable, nyuukinPrintTable);
                            csvDtList.Add(dtData);
                        }

                        count++;

                        //帳票出力データテーブルを初期化
                        denpyouPrintTable = CreateSeikyuuPrintTable();
                        nyuukinPrintTable = CreateSeikyuuPrintTable();

                        nowKagamiNo = Convert.ToInt16(tableRow["KAGAMI_NUMBER"]);
                    }

                    //現在行の前の行
                    DataRow tablePevRow = null;
                    if (i == 0)
                    {
                        //現在行の前の行
                        tablePevRow = null;
                    }
                    else
                    {
                        //現在行の前の行
                        tablePevRow = seikyuDt.Rows[i - 1];
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (i == seikyuDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = seikyuDt.Rows[i + 1];
                    }

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString()))
                    {
                        var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                        var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();
                        var shoshikiGenbaKubun = startRow["SHOSHIKI_GENBA_KBN"].ToString();

                        // 入金明細行判定フラグ(入金は別出力なので、レポート用DataTableを分ける)
                        bool isNyuukin = tableRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_10);

                        // 業者名設定
                        if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                        {
                            if (printFlg)
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                                {
                                    var drGyousha = denpyouPrintTable.NewRow();
                                    drGyousha["GROUP_NAME"] = tableRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"].ToString();
                                    drGyousha["DATA_KBN"] = "GYOUSHA_GROUP";
                                    denpyouPrintTable.Rows.Add(drGyousha);
                                }
                            }
                            else
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                    || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                                {
                                    var drGyousha = denpyouPrintTable.NewRow();
                                    drGyousha["GROUP_NAME"] = tableRow["GYOUSHA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"].ToString();
                                    drGyousha["DATA_KBN"] = "GYOUSHA_GROUP";
                                    denpyouPrintTable.Rows.Add(drGyousha);
                                }
                            }
                        }
                        // 現場名設定
                        if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                        {
                            // 現場名
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_3 == shoshikiKubun && ConstCls.SHOSHIKI_GENBA_KBN_1 == shoshikiGenbaKubun && !isNyuukin))
                            {
                                var drGenba = denpyouPrintTable.NewRow();
                                drGenba["GROUP_NAME"] = tableRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GENBA_NAME2"].ToString();
                                drGenba["DATA_KBN"] = "GENBA_GROUP";
                                denpyouPrintTable.Rows.Add(drGenba);
                            }
                        }

                        // 精算伝票明細データ設定
                        // ☆☆☆2-3☆☆☆
                        if (isNyuukin)
                        {
                            SetSeikyuuDenpyoMeisei_invoice(tableRow, tablePevRow, nyuukinPrintTable, tableNextRow, printFlg, ZeiHyouji);
                        }
                        else
                        {
                            SetSeikyuuDenpyoMeisei_invoice(tableRow, tablePevRow, denpyouPrintTable, tableNextRow, printFlg, ZeiHyouji);
                        }

                        // 現場金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                        {
                            // 現場金額と消費税
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                            {
                                var drGenbaGokei = denpyouPrintTable.NewRow();
                                drGenbaGokei["HINMEI_NAME"] = "現場計";
                                drGenbaGokei["KINGAKU"] = tableRow["GENBA_KINGAKU_1"].ToString();
                                drGenbaGokei["DATA_KBN"] = "GENBA_TOTAL";
                                denpyouPrintTable.Rows.Add(drGenbaGokei);
                            }
                        }

                        // 業者金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"]))
                        {
                            // 業者金額と消費税
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 入金伝票)
                            // ※入金計項目は業者計を使用しているが、業者計or現場計を出力する場合は入金計を出力するため
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isNyuukin)
                                || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isNyuukin))
                            {
                                var drGyoushaGokei = denpyouPrintTable.NewRow();
                                drGyoushaGokei["HINMEI_NAME"] = "業者計";
                                drGyoushaGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drGyoushaGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                denpyouPrintTable.Rows.Add(drGyoushaGokei);
                            }
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && isNyuukin)
                               || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isNyuukin)
                               || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isNyuukin))
                            {
                                var drNyuukinGokei = nyuukinPrintTable.NewRow();
                                drNyuukinGokei["HINMEI_NAME"] = "入金計";
                                drNyuukinGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drNyuukinGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                nyuukinPrintTable.Rows.Add(drNyuukinGokei);
                            }
                        }

                        if (tableNextRow == null || !tableRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"]))
                        {
                            if ((tableRow["KONKAI_KAZEI_KBN_1"].ToString() != "0") ||
                                (tableRow["KONKAI_KAZEI_KBN_2"].ToString() != "0") ||
                                (tableRow["KONKAI_KAZEI_KBN_3"].ToString() != "0") ||
                                (tableRow["KONKAI_KAZEI_KBN_4"].ToString() != "0") ||
                                (tableRow["KONKAI_HIKAZEI_KBN"].ToString() != "0"))
                            {
                                //空白行
                                var drZeikei = denpyouPrintTable.NewRow();
                                drZeikei["GROUP_NAME"] = "";
                                drZeikei["DATA_KBN"] = "ZEI_BLANK";
                                denpyouPrintTable.Rows.Add(drZeikei);

                                decimal zeiritu;

                                //課税計（１～４）行を表示
                                for (int y = 1; y <= 4; y++)
                                {
                                    if (tableRow["KONKAI_KAZEI_KBN_" + y].ToString() != "0")
                                    {
                                        drZeikei = denpyouPrintTable.NewRow();
                                        zeiritu = (decimal)(tableRow["KONKAI_KAZEI_RATE_" + y]);
                                        drZeikei["HINMEI_NAME"] = "【" + string.Format("{0:0%}", zeiritu) + "対象】";
                                        drZeikei["KINGAKU"] = tableRow["KONKAI_KAZEI_GAKU_" + y].ToString();
                                        drZeikei["SHOUHIZEI"] = tableRow["KONKAI_KAZEI_ZEIGAKU_" + y].ToString();
                                        drZeikei["DATA_KBN"] = "ZEI";
                                        denpyouPrintTable.Rows.Add(drZeikei);
                                    }
                                }

                                //非課税計行を表示
                                if (tableRow["KONKAI_HIKAZEI_KBN"].ToString() != "0")
                                {
                                    drZeikei = denpyouPrintTable.NewRow();
                                    drZeikei["HINMEI_NAME"] = "【非課税対象】";
                                    drZeikei["KINGAKU"] = tableRow["KONKAI_HIKAZEI_GAKU"].ToString();
                                    drZeikei["SHOUHIZEI"] = string.Empty;
                                    drZeikei["DATA_KBN"] = "ZEI";
                                    denpyouPrintTable.Rows.Add(drZeikei);
                                }
                            }
                        }
                    }
                    else
                    {
                        var drEmpty = denpyouPrintTable.NewRow();
                        denpyouPrintTable.Rows.Add(drEmpty);
                    }
                }

                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR770 = new ReportInfoR770();
                reportR770.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                DataRow[] dataRows = seikyuDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                reportR770.Title = "請求書(" + dataRows[0]["SEIKYUU_SOUFU_NAME1"] + ")";
                // XPSプロパティ - 発行済み
                reportR770.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                if (printFlg)
                {
                    reportR770.CreateReportData(dto, dataRows[0], denpyouPrintTable, nyuukinPrintTable);
                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR770);
                    formReport = CreateFormReport_invoice(dto, list);

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add Direct Print option refs #158002
                        formReport.PrintXPS();
                    }
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR770.CreateCsvData(dto, dataRows[0], denpyouPrintTable, nyuukinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
                #endregion
            }

            result.Add(count);
            return result;
        }
        /// <summary>
        /// 請求伝票明細データ設定（適格請求書用）
        /// </summary>
        /// <param name="tableRow">請求伝票明細データ</param>
        /// <param name="tablePevRow">一行前請求伝票明細データ</param>
        /// <param name="printData">帳票出力用データテーブル</param>
        /// <param name="tableNextRow">一行後請求伝票明細データ</param>
        private static void SetSeikyuuDenpyoMeisei_invoice(DataRow tableRow, DataRow tablePevRow, DataTable printData, DataRow tableNextRow, bool printFlg, bool ZeiHyouji)
        {
            DataRow dataRow;

            dataRow = printData.NewRow();

            if (!printFlg || tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                DateTime dateTime;
                if (tableRow["DENPYOU_DATE"] != null && DateTime.TryParse(tableRow["DENPYOU_DATE"].ToString(), out dateTime))
                {
                    dataRow["DENPYOU_DATE"] = dateTime.ToShortDateString();
                }
                else
                {
                    dataRow["DENPYOU_DATE"] = string.Empty;
                }
                //売上No
                dataRow["DENPYOU_NUMBER"] = tableRow["DENPYOU_NUMBER"].ToString();
            }
            else
            {
                //月日
                dataRow["DENPYOU_DATE"] = string.Empty;
                //売上No
                dataRow["DENPYOU_NUMBER"] = string.Empty;
            }
            //車輛
            dataRow["SHARYOU_NAME"] = tableRow["SHARYOU_NAME"].ToString();

            //品名
            dataRow["HINMEI_NAME"] = tableRow["HINMEI_NAME"].ToString();

            //数量
            if (ConstCls.DENPYOU_SHURUI_CD_10 == tableRow["DENPYOU_SHURUI_CD"].ToString())
            {
                dataRow["SUURYOU"] = string.Empty;
            }
            else
            {
                dataRow["SUURYOU"] = tableRow["SUURYOU"].ToString();
            }

            //単位
            dataRow["UNIT_NAME"] = tableRow["UNIT_NAME"].ToString();


            //単価
            if (ConstCls.DENPYOU_SHURUI_CD_10 == tableRow["DENPYOU_SHURUI_CD"].ToString())
            {
                dataRow["TANKA"] = string.Empty;
            }
            else
            {
                dataRow["TANKA"] = tableRow["TANKA"].ToString();
            }

            //金額
            dataRow["KINGAKU"] = tableRow["KINGAKU"].ToString();

            // 消費税
            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
            var meisai_shouhizei = tableRow["MEISEI_SYOHIZEI"].ToString();
            var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
            var meisai_zei_kbn_cd = tableRow["MEISAI_ZEI_KBN_CD"].ToString();
            var zei_kbn_cd = (ConstCls.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd)) ? denpyou_zei_kbn_cd : meisai_zei_kbn_cd;
            var shouhizei = GetSyohizei(meisai_shouhizei, zei_kbn_cd, true);

            if (ConstCls.DENPYOU_SHURUI_CD_10 == denpyou_shurui_cd)
            {
                // 入金伝票は出力しない
                dataRow["SHOUHIZEI"] = string.Empty;
            }
            else
            {
                decimal zeiritu;
                dataRow["SHOUHIZEI"] = string.Empty;
                zeiritu = (decimal)(tableRow["SHOUHIZEI_RATE"]);
                if (ZeiHyouji)
                {
                    //税率表示あり
                    if (ConstCls.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd))
                    {
                        //品名税なし
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == denpyou_zei_keisan_kbn_cd)
                        {
                            if (ConstCls.ZEI_KBN_HIKAZEI.Equals(denpyou_zei_kbn_cd))
                            {
                                dataRow["SHOUHIZEI"] = "非課税";
                            }
                            else if (ConstCls.ZEI_KBN_SOTO.Equals(denpyou_zei_kbn_cd))
                            {
                                dataRow["SHOUHIZEI"] = string.Format("{0:0%}", zeiritu);
                            }
                        }
                        else if (ConstCls.ZEI_KEISAN_KBN_DENPYOU == denpyou_zei_keisan_kbn_cd)
                        {
                            dataRow["SHOUHIZEI"] = "伝票\r" + string.Format("{0:0%}", zeiritu);   //※処理上ありあない
                        }
                        else if (ConstCls.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd)
                        {
                            if (ConstCls.ZEI_KBN_HIKAZEI.Equals(denpyou_zei_kbn_cd))
                            {
                                dataRow["SHOUHIZEI"] = "非課税";
                            }
                            else if (ConstCls.ZEI_KBN_UCHI.Equals(denpyou_zei_kbn_cd))
                            {
                                dataRow["SHOUHIZEI"] = "内税\r" + string.Format("{0:0%}", zeiritu);
                            }
                            else if (ConstCls.ZEI_KBN_SOTO.Equals(denpyou_zei_kbn_cd))
                            {
                                dataRow["SHOUHIZEI"] = "明細\r" + string.Format("{0:0%}", zeiritu);   //※処理上ありあない
                            }
                        }
                    }
                    else
                    {
                        //品名税あり
                        if (ConstCls.ZEI_KBN_HIKAZEI.Equals(meisai_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "非課税";
                        }
                        else if (ConstCls.ZEI_KBN_UCHI.Equals(meisai_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "内税\r" + string.Format("{0:0%}", zeiritu);
                        }
                        else if (ConstCls.ZEI_KBN_SOTO.Equals(meisai_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "明細\r" + string.Format("{0:0%}", zeiritu);   //※処理上ありあない
                        }
                    }
                }
                else
                {
                    //税率表示なし
                    if (ConstCls.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd))
                    {
                        //品名税なし
                        if (ConstCls.ZEI_KBN_HIKAZEI.Equals(denpyou_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "非課税";
                        }
                        else
                        {
                            if ((ConstCls.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd)
                                && (ConstCls.ZEI_KBN_UCHI.Equals(denpyou_zei_kbn_cd)))
                            {
                                dataRow["SHOUHIZEI"] = "内税";
                            }
                        }
                    }
                    else
                    {
                        //品名税あり
                        if (ConstCls.ZEI_KBN_HIKAZEI.Equals(meisai_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "非課税";
                        }
                        else if (ConstCls.ZEI_KBN_UCHI.Equals(meisai_zei_kbn_cd))
                        {
                            dataRow["SHOUHIZEI"] = "内税";
                        }
                    }
                }
            }

            //備考
            dataRow["MEISAI_BIKOU"] = tableRow["MEISAI_BIKOU"].ToString();

            printData.Rows.Add(dataRow);

        }
        /// <summary>
        /// レポートフォーム作成
        /// </summary>
        /// <param name="dto">請求書発行用DTO</param>
        /// <param name="aryPrint">帳票出力用データリスト</param>
        /// <returns></returns>
        public static FormReport CreateFormReport_invoice(SeikyuuDenpyouDto dto, ArrayList aryPrint)
        {
            FormReport formReport = null;

            // 現状では指定用紙がないが、条件判定だけ実装しておく
            if (dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SAKUSEIJI_JISYA
                    || dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_JISYA)
            {
                ReportInfoR770[] reportInfo = (ReportInfoR770[])aryPrint.ToArray(typeof(ReportInfoR770));
                formReport = new FormReport(reportInfo, "R770");
                formReport.Caption = "請求書";
            }
            else if (dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SAKUSEIJI_SHITEI
                         || dto.SeikyuPaper == ConstCls.SEIKYU_PAPER_DATA_SHITEI)
            {
                ReportInfoR770[] reportInfo = (ReportInfoR770[])aryPrint.ToArray(typeof(ReportInfoR770));
                formReport = new FormReport(reportInfo, "R770");
                formReport.Caption = "請求書";
            }

            return formReport;
        }
    }
}

/// <summary>
/// T_SEIKYUU_DENPYOUクラスの比較演算
/// </summary>
class SeikyuuDenpyouPropComparer : IEqualityComparer<T_SEIKYUU_DENPYOU>
{
    public bool Equals(T_SEIKYUU_DENPYOU x, T_SEIKYUU_DENPYOU y)
    {
        return x.SEIKYUU_NUMBER.Value == y.SEIKYUU_NUMBER.Value;
    }

    public int GetHashCode(T_SEIKYUU_DENPYOU obj)
    {
        return obj.SEIKYUU_NUMBER.Value.GetHashCode();
    }
}