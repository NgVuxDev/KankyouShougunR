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
using Shougun.Core.Billing.InxsSeikyuushoHakkou.APP;
using Shougun.Core.Billing.InxsSeikyuushoHakkou.Const;
using Shougun.Core.Billing.InxsSeikyuushoHakkou.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Seasar.Framework.Exceptions;
using Seasar.Dao;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Billing.SeikyuushoHakkou;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using System.IO;
using Shougun.Core.Billing.SeikyuushoHakkou.DTO;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.FormManager;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Enums;
using Shougun.Core.Common.BusinessCommon.Const;

namespace Shougun.Core.Billing.InxsSeikyuushoHakkou
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
        #endregion

        #region フィールド
        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        ///請求伝票
        /// </summary>
        private TSDDaoCls seikyuDenpyouDao;

        /// <summary>	
        /// 取引先_請求情報マスタ	
        /// </summary>	
        private MTSDaoCls torihikisakiSeikyuuDao;

        private IT_SEIKYUU_DENPYOU_INXSDao seikyuDenpyouInxsDao;

        private IT_SEIKYUU_DENPYOU_KAGAMI_INXSDao seikyuDenpyouKagamiInxsDao;

        private IT_SEIKYUU_DENPYOU_KAGAMI_USER_INXSDao seikyuDenpyouKagamiUserInxsDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Billing.InxsSeikyuushoHakkou.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderInxsSeikyuushoHakkou.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        internal readonly string msgA = "対象データは存在しないため再度検索を実行してください。";
        internal readonly string msgB = "公開ユーザーが未設定です。再度確認してください。伝票番号：{0}";
        internal readonly string msgC = "INXSに対象請求データをアップロードします。\nよろしいですか？";
        internal readonly string msgD = "アップロードに失敗しました。再度アップロードまたは検索を実行してください。\n繰り返し発生する場合はシステム管理者へ問い合わせてください。";
        internal readonly string msgE = "INXS担当者の権限がないため、アップロードできません";
        internal readonly string msgF = "該当するデータがありません";

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO mSysInfo;

        private string strSystemDate = string.Empty;

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
            this.seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<MTSDaoCls>();

            this.seikyuDenpyouInxsDao = DaoInitUtility.GetComponent<IT_SEIKYUU_DENPYOU_INXSDao>();
            this.seikyuDenpyouKagamiInxsDao = DaoInitUtility.GetComponent<IT_SEIKYUU_DENPYOU_KAGAMI_INXSDao>();
            this.seikyuDenpyouKagamiUserInxsDao = DaoInitUtility.GetComponent<IT_SEIKYUU_DENPYOU_KAGAMI_USER_INXSDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm, UIHeader headerForm)
        {
            LogUtility.DebugMethodStart(targetForm, headerForm);

            this.form = targetForm;
            this.headerForm = headerForm;
            this.SearchString = new DTOClass();
            this.seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.torihikisakiSeikyuuDao = DaoInitUtility.GetComponent<MTSDaoCls>();
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
                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;

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
                    mKyoten1 = (M_KYOTEN)kyotenDao.GetDataByCd(this.InitDto.InitKyotenCd);

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
                    mKyoten1 = (M_KYOTEN)kyotenDao.GetDataByCd(this.headerForm.USER_KYOTEN_CD.Text);
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

                strSystemDate = this.parentForm.sysDate.ToShortDateString();
                // ユーザ拠点名称の取得
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)kyotenDao.GetDataByCd(this.form.KYOTEN_CD.Text);
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
                    this.headerForm.DenpyouHidukeFrom.Text = this.parentForm.sysDate.ToString("yyyy-MM-dd");
                    this.headerForm.DenpyouHidukeTo.Text = this.parentForm.sysDate.ToString("yyyy-MM-dd");
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
                    mTorihikisaki = torihikisakiDao.GetDataByCd(this.InitDto.InitTorihiksiakiCd);

                    if (mTorihikisaki != null)
                    {
                        this.form.TORIHIKISAKI_CD.Text = mTorihikisaki.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                this.form.HIKAE_INSATSU_KBN.Text = ConstCls.HIKAE_INSATSU_KBN_SUBETE;
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

                //アップロード状況
                this.form.UPLOAD_STATUS.Text = ((int)EnumUploadSatus.SUBETE).ToString();
                this.form.ZERO_KINGAKU_TAISHOGAI.Checked = true;
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
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, this.form.WindowType);

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

            parentForm.bt_func1.Click += new EventHandler(this.form.CSV);

            //控え印刷ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.PrintDirect);

            //プレビューボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.PreView);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //[1]INXSアップロード
            parentForm.bt_process1.Click += new EventHandler(this.form.UploadToINXS);

            //取引先コード入力欄でロストフォーカスイベント生成
            this.form.TORIHIKISAKI_CD.Leave += new EventHandler(TORIHIKISAKI_CD_Leave);

            //締日プルダウン値変更イベント生成
            this.form.cmbShimebi.TextChanged += new EventHandler(cmbShimebi_TextChanged);

            // 「To」のイベント生成
            this.headerForm.DenpyouHidukeTo.MouseDoubleClick += new MouseEventHandler(DenpyouHidukeTo_MouseDoubleClick);

            // Receive data call back from SubApp
            parentForm.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(this.form.ParentForm_OnReceiveMessageEvent);
            parentForm.FormClosing += new FormClosingEventHandler(this.form.ParentForm_FormClosing);

            LogUtility.DebugMethodEnd();
        }

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
                //請求日付
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
                //拠点
                this.SearchString.HakkouKyotenCD = this.headerForm.USER_KYOTEN_CD.Text;
                //締日
                this.SearchString.Simebi = this.form.cmbShimebi.Text;
                //印刷順
                this.SearchString.PrintOrder = int.Parse(this.form.PRINT_ORDER.Text);
                //請求用紙
                this.SearchString.SeikyuPaper = int.Parse(this.form.SEIKYU_PAPER.Text);
                //取引先
                this.SearchString.TorihikisakiCD = this.form.TORIHIKISAKI_CD.Text;

                //発行区分
                SqlBoolean insatsuKbn = SqlBoolean.Null;
                if (ConstCls.HIKAE_INSATSU_KBN_MIINSATSU.Equals(this.form.HIKAE_INSATSU_KBN.Text))
                {
                    insatsuKbn = SqlBoolean.False;
                }
                else if (ConstCls.HIKAE_INSATSU_KBN_INSATSUZUMI.Equals(this.form.HIKAE_INSATSU_KBN.Text))
                {
                    insatsuKbn = SqlBoolean.True;
                }
                else if (ConstCls.HIKAE_INSATSU_KBN_SUBETE.Equals(this.form.HIKAE_INSATSU_KBN.Text))
                {
                    insatsuKbn = SqlBoolean.Null;
                }
                this.SearchString.HikaeInsatsuKbn = insatsuKbn;

                //抽出データ
                int filteringData = 0;
                int.TryParse(this.form.FILTERING_DATA.Text, out filteringData);
                this.SearchString.FilteringData = filteringData;
                //アップロード状況
                if (!this.form.UPLOAD_STATUS.Text.Equals(((int)EnumUploadSatus.SUBETE).ToString()))
                {
                    this.SearchString.UploadStatus = int.Parse(this.form.UPLOAD_STATUS.Text);
                }
                else
                {
                    this.SearchString.UploadStatus = null;
                }
                
                this.SearchString.ZeroKingakuTaishogai = this.form.ZERO_KINGAKU_TAISHOGAI.Checked;

                this.SearchResult = seikyuDenpyouDao.GetDataForEntity(this.SearchString);
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
                int k = this.form.SeikyuuDenpyouIchiran.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.SeikyuuDenpyouIchiran.Rows.RemoveAt(this.form.SeikyuuDenpyouIchiran.Rows[i - 1].Index);
                }


                //検索結果を設定する
                var table = this.SearchResult;
                table.BeginLoadData();

                //検索結果設定
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    this.form.SeikyuuDenpyouIchiran.Rows.Add();
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_HAKKOU].Value = false;
                    //公開ユーザー確認
                    bool isNeedUserConfirmation = false;
                    if (!table.Rows[i].IsNull("NEED_USER_CONFIRMATION"))
                    {
                        isNeedUserConfirmation = Convert.ToBoolean(table.Rows[i]["NEED_USER_CONFIRMATION"]);
                    }
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = isNeedUserConfirmation ? CommonConst.PUBLIC_USER_CONFIRM_TEXT : string.Empty;
                    
                    //公開ユーザー設定
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = table.Rows[i]["PUBLISHED_USER_SETTING"];

                    //アップロード状況
                    EnumUploadSatus uploadStatus = EnumUploadSatus.MI;
                    if (!table.Rows[i].IsNull("UPLOAD_STATUS"))
                    {
                        uploadStatus = Convert.ToInt32(table.Rows[i]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>();
                    }
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_UPLOAD_STATUS].Value = uploadStatus.ToEnumDescription();

                    //ダウンロード状況
                    EnumDownloadSatus downloadStatus = EnumDownloadSatus.MI;
                    if (!table.Rows[i].IsNull("DOWNLOAD_STATUS"))
                    {
                        downloadStatus = Convert.ToInt32(table.Rows[i]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>();
                    }
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = downloadStatus.ToEnumDescription();

                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_SEIKYUU_NUMBER].Value = table.Rows[i]["SEIKYUU_NUMBER"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_SEIKYUU_DATE].Value = table.Rows[i]["SEIKYUU_DATE"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_TORIHIKISAKI_CD].Value = table.Rows[i]["TORIHIKISAKI_CD"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_TORIHIKISAKI_NAME].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_SHIMEBI].Value = table.Rows[i]["SHIMEBI"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_ZENKAI_KURIKOSI_GAKU].Value = table.Rows[i]["ZENKAI_KURIKOSI_GAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_KONKAI_NYUUKIN_GAKU].Value = table.Rows[i]["KONKAI_NYUUKIN_GAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_KONKAI_CHOUSEI_GAKU].Value = table.Rows[i]["KONKAI_CHOUSEI_GAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_KONKAI_URIAGE_GAKU].Value = table.Rows[i]["KONKAI_URIAGE_GAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_SHOHIZEI_GAKU].Value = table.Rows[i]["SHOHIZEI_GAKU"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_KONKAI_SEIKYU_GAKU].Value = table.Rows[i]["KONKAI_SEIKYU_GAKU"];
                    if (string.IsNullOrEmpty(table.Rows[i]["NYUUKIN_YOTEI_BI"].ToString()))
                    {
                        this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_NYUUKIN_YOTEI_BI].Value = string.Empty;
                    }
                    else
                    {
                        this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_NYUUKIN_YOTEI_BI].Value = DateTime.Parse(table.Rows[i]["NYUUKIN_YOTEI_BI"].ToString()).ToShortDateString();
                    }
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_TIME_STAMP].Value = table.Rows[i]["TIME_STAMP"];
                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_HAKKOU].ToolTipText = ConstCls.ToolTipText1;

                    this.form.SeikyuuDenpyouIchiran.Rows[i].Cells[ConstCls.COL_HIKAE_INSATSU_KBN].Value = table.Rows[i]["HIKAE_INSATSU_KBN"];

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

        #region [F5]プレビューボタン押下
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        //
        public virtual void PreView(bool printDirectFlg)
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
                    seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();

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
                    dto.PrintDirectFlg = printDirectFlg;

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return;
                    }
                    bool isAbortRequired = false;
                    int printCount = 0;

                    //グリッドの発行列にチェックが付いているデータのみ処理を行う
                    foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                    {
                        if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                        {
                            hakkouCnt++;

                            DataTable dt = new DataTable();
                            dt.Columns.Add();

                            //
                            dto.TorihikisakiCd = row.Cells[ConstCls.COL_TORIHIKISAKI_CD].Value.ToString();

                            //印刷用データを取得
                            //G102請求書確認の処理を参考
                            //精算番号
                            string seikyuNumber = row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString();

                            //請求伝票を取得
                            T_SEIKYUU_DENPYOU tseikyudenpyou = seikyuDenpyouDao.GetDataByCd(seikyuNumber);

                            dto.SeikyuPaper = tseikyudenpyou.YOUSHI_KBN.Value.ToString();

                            //入金明細区分 (請求携帯：2.単月請求の場合は、入金明細なしで固定)
                            string nyuukinMeisaiKbn = "2";
                            if (this.form.SEIKYU_STYLE.Text != "2")
                            {
                                nyuukinMeisaiKbn = tseikyudenpyou.NYUUKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = nyuukinMeisaiKbn;   // No.4004

                            var result = SeikyuuDenpyouLogicClass.PreViewSeikyuDenpyouRemote(tseikyudenpyou, dto, true, false, string.Empty, this.SearchString.ZeroKingakuTaishogai);
                            if (result != null)
                            {
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

                    // 印刷シーケンスの終了
                    ContinuousPrinting.End(isAbortRequired);

                    //発行チェックボックスがすべてOFFの場合はメッセージ表示
                    if (hakkouCnt == 0)
                    {
                        msgLogic.MessageBoxShow("E050", "INXS請求書発行");
                    }

                    //発行対象データが0件の場合はメッセージ表示
                    if (printCount == 0)
                    {
                        msgLogic.MessageBoxShow("I008", "請求書");
                    }
                    else if (!isAbortRequired)
                    {
                        foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                        {
                            if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                            {
                                //発行区分を更新
                                DataTable seikyuDenpyo = SeikyuuDenpyouLogicClass.UpdateSeikyuDenpyouHakkouKbnRemote(row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString(), (byte[])row.Cells[ConstCls.COL_TIME_STAMP].Value);

                                if (seikyuDenpyo != null && seikyuDenpyo.Rows.Count > 0)
                                {
                                    //タイムスタンプを設定
                                    row.Cells[ConstCls.COL_TIME_STAMP].Value = seikyuDenpyo.Rows[0]["TIME_STAMP"];
                                }

                                if (printDirectFlg && (bool)row.Cells[ConstCls.COL_HIKAE_INSATSU_KBN].Value == false)
                                {
                                    var seikyuuEntity = this.UpdateSeikyuDenpyouHikaeInsatsuKbn(row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString());
                                    if (seikyuuEntity != null)
                                    {
                                        // 控え済みチェックをON
                                        row.Cells[ConstCls.COL_HIKAE_INSATSU_KBN].Value = seikyuuEntity.HIKAE_INSATSU_KBN.Value;
                                        //タイムスタンプを設定
                                        row.Cells[ConstCls.COL_TIME_STAMP].Value = seikyuuEntity.TIME_STAMP;
                                    }
                                }
                            }
                        }
                        //グリッドを再描画
                        this.form.SeikyuuDenpyouIchiran.Refresh();
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
        #endregion

        #region Sub [1]INXSアップロード
        public virtual void UploadToINXS()
        {
            string transFolderPath = string.Empty;
            try
            {
                if (!SystemProperty.Shain.InxsTantouFlg) //【社員入力】[INXS担当者]＝OFF
                {
                    this.errMsg.MessageBoxShowError(msgE); //Mess E
                    return;
                }
                List<long> seikyuuNumbers = new List<long>();
                Dictionary<long, DataGridViewRow> dicUploadRows = new Dictionary<long, DataGridViewRow>();

                foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                {
                    if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value)
                    {
                        long seikyuuNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value);
                        seikyuuNumbers.Add(seikyuuNumber);
                        dicUploadRows.Add(seikyuuNumber, row);
                    }
                }
                if (!seikyuuNumbers.Any())
                {
                    this.errMsg.MessageBoxShowError(msgF); //Mess F
                    return;
                }

                Dictionary<string, List<KagamiUserListDto>> dicUserSettings = new Dictionary<string, List<KagamiUserListDto>>();
                #region Check [対象]＝ON and 公開ユーザー
                List<long> seikyuuNumberErrors = new List<long>();
                foreach (long seikyuuNumber in seikyuuNumbers)
                {
                    List<KagamiUserListDto> currentUserSettings = GetKagamiUserSettings(dicUploadRows[seikyuuNumber]);
                    long[] ignoreUserSysIds = null;
                    long[] userSysIds = null;
                    if (currentUserSettings != null)
                    {
                        ignoreUserSysIds = currentUserSettings.SelectMany(x => x.UserSettingInfos)
                                                              .Where(x => !x.IsSend).Select(x => x.UserSysId).ToArray();
                        if (ignoreUserSysIds.Length == 0)
                        {
                            ignoreUserSysIds = null;
                        }


                        userSysIds = currentUserSettings.SelectMany(x => x.UserSettingInfos)
                                                        .Where(x => x.IsSend).Select(x => x.UserSysId).ToArray();
                        if (userSysIds.Length == 0)
                        {
                            userSysIds = null;
                        }
                    }

                    //Get Master
                    DataTable tbUser = this.seikyuDenpyouDao.GetPublishedUserSettingData(seikyuuNumber, ignoreUserSysIds, userSysIds);
                    if (tbUser == null || tbUser.Rows.Count == 0)
                    {
                        seikyuuNumberErrors.Add(seikyuuNumber);
                        continue;
                    }

                    List<KagamiUserListDto> kagamiUserSettings = tbUser.AsEnumerable().GroupBy(x => x.Field<int>("KAGAMI_NUMBER"))
                                                                                .Select(x => new KagamiUserListDto
                                                                                {
                                                                                    KagamiNumber = x.Key,
                                                                                    UserSettingInfos = x.Where(p => p.Field<long?>("USER_SYS_ID") != null)
                                                                                                        .Select(k => new UserSettingInfoDto
                                                                                                        {
                                                                                                            UserSysId = k.Field<long>("USER_SYS_ID"),
                                                                                                            UserId = k.Field<long>("USER_ID"),
                                                                                                            IsSend = true
                                                                                                        }).ToList()
                                                                                }).ToList();

                    List<KagamiUserListDto> userSettings = new List<KagamiUserListDto>();
                    foreach (var kagamiUserSetting in kagamiUserSettings)
                    {
                        if (currentUserSettings != null)
                        {
                            var users = currentUserSettings.Where(x => x.KagamiNumber == kagamiUserSetting.KagamiNumber)
                                                           .SelectMany(x => x.UserSettingInfos)
                                                           .Where(x => x.IsSend).ToList();
                            if (users.Any())
                            {
                                KagamiUserListDto userSetting = new KagamiUserListDto()
                                {
                                    KagamiNumber = kagamiUserSetting.KagamiNumber,
                                    UserSettingInfos = users
                                };
                                userSettings.Add(userSetting);
                                continue;
                            }
                        }

                        userSettings.Add(kagamiUserSetting);
                    }

                    if (userSettings.Any(x => !x.UserSettingInfos.Any()))
                    {
                        seikyuuNumberErrors.Add(seikyuuNumber);
                        continue;
                    }

                    dicUserSettings.Add(seikyuuNumber.ToString(), userSettings);
                }

                //Check
                if (seikyuuNumberErrors.Any())
                {
                    this.errMsg.MessageBoxShowError(string.Format(msgB, string.Join("、", seikyuuNumberErrors))); //Msg B
                    return;
                }

                #endregion [対象]＝ON and 公開ユーザー

                if (this.form.SEIKYUSHO_PRINTDAY.Text == ConstCls.SEIKYU_PRINT_DAY_SITEI &&
                    this.form.cdtSiteiPrintHiduke.Value == null)
                {
                    this.errMsg.MessageBoxShow("E012", "指定日");

                    this.form.cdtSiteiPrintHiduke.Focus();
                }
                else
                {
                    if (this.errMsg.MessageBoxShowConfirm(msgC, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }

                    if (!File.Exists(SystemProperty.InxsSettings.FilePath))
                    {
                        return;
                    }

                    seikyuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
                    //INXS請求書発行用DTO作成
                    SeikyuuDenpyouDto dto = new SeikyuuDenpyouDto();
                    dto.MSysInfo = this.mSysInfo;
                    dto.SeikyuHakkou = this.form.SEIKYU_HAKKOU.Text;
                    dto.SeikyushoPrintDay = this.form.SEIKYUSHO_PRINTDAY.Text;
                    dto.HakkoBi = this.strSystemDate;
                    if (this.form.cdtSiteiPrintHiduke.Value != null)
                    {
                        dto.SeikyuDate = (DateTime)this.form.cdtSiteiPrintHiduke.Value;
                    }
                    dto.SeikyuStyle = this.form.SEIKYU_STYLE.Text;

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return;
                    }

                    var seikyuuEntities = this.seikyuDenpyouDao.GetSeyikyuuEntities(seikyuuNumbers.ToArray());
                    if (seikyuuEntities == null || seikyuuEntities.Length != seikyuuNumbers.Count)
                    {
                        this.errMsg.MessageBoxShowError(msgA); //Mess A
                        return;
                    }

                    Dictionary<string, T_SEIKYUU_DENPYOU> dicSeikyuuEntities = seikyuuEntities.ToDictionary(x => x.SEIKYUU_NUMBER.Value.ToString(), x => x);

                    Dictionary<string, List<KagamiFileExportDto>> dicKagamiFileExport = new Dictionary<string, List<KagamiFileExportDto>>();
                    transFolderPath = Path.Combine(Path.GetDirectoryName(SystemProperty.InxsSettings.FilePath), "Attachments", "Seikyuusho", Guid.NewGuid().ToString());
                    if (!Directory.Exists(transFolderPath))
                    {
                        Directory.CreateDirectory(transFolderPath);
                    }

                    bool isAbortRequired = false;
                    int printCount = 0;
                    foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                    {
                        if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add();

                            //
                            dto.TorihikisakiCd = row.Cells[ConstCls.COL_TORIHIKISAKI_CD].Value.ToString();
                            //精算番号
                            string seikyuNumber = row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString();


                            //請求伝票を取得
                            T_SEIKYUU_DENPYOU tseikyudenpyou = dicSeikyuuEntities[seikyuNumber];
                            tseikyudenpyou.TIME_STAMP = (byte[])row.Cells[ConstCls.COL_TIME_STAMP].Value;

                            dto.SeikyuPaper = tseikyudenpyou.YOUSHI_KBN.Value.ToString();

                            //入金明細区分 (請求携帯：2.単月請求の場合は、入金明細なしで固定)
                            string nyuukinMeisaiKbn = "2";
                            if (this.form.SEIKYU_STYLE.Text != "2")
                            {
                                nyuukinMeisaiKbn = tseikyudenpyou.NYUUKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = nyuukinMeisaiKbn;   // No.4004

                            string folderPath = Path.Combine(transFolderPath, seikyuNumber);
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            var result = SeikyuuDenpyouLogicClass.PreViewSeikyuDenpyouRemote(tseikyudenpyou, dto, true, true, folderPath, this.SearchString.ZeroKingakuTaishogai);

                            if (result != null)
                            {
                                printCount = (int)result[0];
                                dicKagamiFileExport.Add(seikyuNumber, (List<KagamiFileExportDto>)result[1]);
                            }

                            // 印刷画面から中止要求があれば中断
                            if (ContinuousPrinting.IsAbortRequired)
                            {
                                isAbortRequired = true;
                                break;
                            }
                        }
                    }

                    // 印刷シーケンスの終了
                    ContinuousPrinting.End(isAbortRequired);

                    #region Save to INXS
                    using (var tran = new Transaction())
                    {
                        List<string> oldFolderPaths = new List<string>();
                        foreach (var item in dicKagamiFileExport)
                        {
                            long seikyuuNumber = long.Parse(item.Key);
                            var dicKagamiUserList = dicUserSettings[item.Key].ToDictionary(x => x.KagamiNumber, x => x.UserSettingInfos);

                            //Check TIME_STAMP
                            T_SEIKYUU_DENPYOU entity = dicSeikyuuEntities[item.Key];
                            this.seikyuDenpyouDao.Update(entity);

                            T_SEIKYUU_DENPYOU_INXS denpyouInxsEntity = this.seikyuDenpyouInxsDao.GetDataByCd(item.Key);
                            if (denpyouInxsEntity != null)
                            {
                                string oldFilePath = this.seikyuDenpyouKagamiInxsDao.GetDataFolderPathUpload(item.Key);
                                if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(oldFilePath))
                                {
                                    oldFolderPaths.Add(Path.GetDirectoryName(oldFilePath));
                                }
                                //Delete kagami
                                this.seikyuDenpyouKagamiInxsDao.DeleteBySeikyuu(item.Key);
                                //Delete kagamiUser
                                this.seikyuDenpyouKagamiUserInxsDao.DeleteBySeikyuu(item.Key);

                                //Update
                                denpyouInxsEntity.UPLOAD_STATUS = (int)EnumUploadSatus.CHUU;
                                denpyouInxsEntity.DOWNLOAD_STATUS = (int)EnumDownloadSatus.MI;
                                this.seikyuDenpyouInxsDao.Update(denpyouInxsEntity);
                            }
                            else
                            {
                                //Add new
                                denpyouInxsEntity = new T_SEIKYUU_DENPYOU_INXS()
                                {
                                    SEIKYUU_NUMBER = seikyuuNumber,
                                    UPLOAD_STATUS = (int)EnumUploadSatus.CHUU,
                                    DOWNLOAD_STATUS = (int)EnumDownloadSatus.MI
                                };
                                this.seikyuDenpyouInxsDao.Insert(denpyouInxsEntity);
                            }

                            foreach (var kagamiItem in item.Value)
                            {
                                T_SEIKYUU_DENPYOU_KAGAMI_INXS kagamiEntity = new T_SEIKYUU_DENPYOU_KAGAMI_INXS()
                                {
                                    SEIKYUU_NUMBER = seikyuuNumber,
                                    KAGAMI_NUMBER = kagamiItem.KagamiNumber,
                                    POSTED_FILE_PATH = kagamiItem.FileExport
                                };
                                this.seikyuDenpyouKagamiInxsDao.Insert(kagamiEntity);


                                var userSettings = dicKagamiUserList[kagamiItem.KagamiNumber];
                                foreach (var userSetting in userSettings)
                                {
                                    T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS userEntity = new T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS()
                                    {
                                        SEIKYUU_NUMBER = seikyuuNumber,
                                        KAGAMI_NUMBER = kagamiItem.KagamiNumber,
                                        USER_SYS_ID = userSetting.UserSysId
                                    };
                                    this.seikyuDenpyouKagamiUserInxsDao.Insert(userEntity);
                                }
                            }
                        }

                        tran.Commit();

                        //Delete Old File
                        if (oldFolderPaths.Any())
                        {
                            foreach (var oldPath in oldFolderPaths)
                            {
                                DeleteFolderAndParentIfEmpty(oldPath);
                            }
                        }
                    }

                    //発行対象データが0件の場合はメッセージ表示
                    if (printCount == 0)
                    {
                        this.errMsg.MessageBoxShow("I008", "請求書");
                    }
                    else if (!isAbortRequired)
                    {
                        //seikyuuNumbers
                        List<UploadLoadDto> uploadList = new List<UploadLoadDto>();
                        DataTable tbData = seikyuDenpyouDao.GetDataUpdateSeikyuu(seikyuuNumbers.ToArray());
                        if (tbData != null && tbData.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                            {
                                if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                                {
                                    long seikyuuNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value);
                                    string filter = string.Format("SEIKYUU_NUMBER = {0}", seikyuuNumber);
                                    DataRow[] results = tbData.Select(filter);
                                    if (results != null && results.Length > 0)
                                    {
                                        row.Cells[ConstCls.COL_HAKKOU].Value = false;
                                        row.Cells[ConstCls.COL_UPLOAD_STATUS].Value = Convert.ToInt32(results[0]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>().ToEnumDescription();
                                        row.Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = Convert.ToInt32(results[0]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>().ToEnumDescription();
                                        row.Cells[ConstCls.COL_TIME_STAMP].Value = results[0]["TIME_STAMP"];
                                        //row.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = DBNull.Value;

                                        //Add
                                        UploadLoadDto uploadDto = new UploadLoadDto()
                                        {
                                            SEIKYUU_NUMBER = seikyuuNumber,
                                            TIME_STAMP = (byte[])results[0]["TIME_STAMP"]
                                        };
                                        uploadList.Add(uploadDto);
                                    }
                                }
                            }

                            form.checkBoxAll.CheckedChanged -= new EventHandler(form.checkBoxAll_CheckedChanged);
                            form.checkBoxAll.Checked = false;
                            form.checkBoxAll.CheckedChanged += new EventHandler(form.checkBoxAll_CheckedChanged);

                            if (uploadList.Any())
                            {
                                RemoteAppCls remoteAppCls = new RemoteAppCls();
                                var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                                {
                                    TransactionId = form.transactionUploadId,
                                    ReferenceID = "UploadInxs"
                                });
                                var arg = JsonUtility.SerializeObject<List<UploadLoadDto>>(uploadList);
                                FormManager.OpenFormSubApp("S012", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, arg, token, parentForm.Text);
                            }
                        }

                        //グリッドを再描画
                        this.form.SeikyuuDenpyouIchiran.Refresh();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(transFolderPath) && Directory.Exists(transFolderPath))
                {
                    Directory.Delete(transFolderPath, true);
                }

                LogUtility.Error("UploadToINXS", ex);
                if (ex is SQLRuntimeException)
                {
                    this.errMsg.MessageBoxShow("E093", "");
                }
                else if (ex is NotSingleRowUpdatedRuntimeException)
                {
                    this.errMsg.MessageBoxShow("E080", "");
                }
                else
                {
                    this.errMsg.MessageBoxShow("E245", "");
                }
            }
        }

        private void DeleteFolderAndParentIfEmpty(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true);
                    var parentFolder = Directory.GetParent(dir);
                    var entries = Directory.EnumerateFileSystemEntries(parentFolder.FullName);
                    if (!entries.Any())
                    {
                        parentFolder.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteFolderAndParentIfEmpty", ex);
                this.errMsg.MessageBoxShow("E245", "");
            }
        }

        #endregion Sub [1]INXSアップロード

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
                var torihikisakiResult = torihikisakiDao.GetAllValidData(torihikisakiEntity);

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
                    //締日チェック
                    var torihikisaki = torihikisakiSeikyuuDao.GetDataByCd(torihikisakiCd);
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

            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion

        public void SetHeaderForm(UIHeader hs)
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

        #region [F1]CSV出力

        internal void ExportCSV()
        {
            // No.2180
            LogUtility.DebugMethodStart();
            try
            {
                CustomDataGridView dgvExportCSV = this.MakeDataGridViewExportCSV();
                // 一覧にデータ行がない場合
                if (dgvExportCSV.RowCount == 0)
                {
                    // アラートを表示し、CSV出力処理はしない
                    this.errMsg.MessageBoxShow("E044");
                }
                else if (this.errMsg.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport exp = new CSVExport();
                    exp.ConvertCustomDataGridViewToCsv(dgvExportCSV, true, true, "INXS請求書発行", this.form);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
            return;
        }

        internal CustomDataGridView MakeDataGridViewExportCSV()
        {
            CustomDataGridView dgvExport = new CustomDataGridView();
            try
            {
                string[] ignoreColumns = new string[] { ConstCls.COL_HAKKOU, ConstCls.COL_PUBLISHED_USER_SETTING_BUTTON };
                string[] moneyColumns = new string[] { ConstCls.COL_ZENKAI_KURIKOSI_GAKU, ConstCls.COL_KONKAI_NYUUKIN_GAKU, ConstCls.COL_KONKAI_CHOUSEI_GAKU
                                                       ,ConstCls.COL_KONKAI_URIAGE_GAKU, ConstCls.COL_SHOHIZEI_GAKU, ConstCls.COL_KONKAI_SEIKYU_GAKU};
                if (dgvExport.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvCol in this.form.SeikyuuDenpyouIchiran.Columns)
                    {
                        if (ignoreColumns.Contains(dgvCol.Name))
                        {
                            continue;
                        }

                        var column = dgvCol.Clone() as DataGridViewColumn;
                        if (moneyColumns.Contains(dgvCol.Name))
                        {
                            column.DefaultCellStyle.Format = "###";
                        }
                        dgvExport.Columns.Add(column);

                    }
                }

                DataGridViewRow row = new DataGridViewRow();
                for (int i = 0; i < this.form.SeikyuuDenpyouIchiran.Rows.Count; i++)
                {
                    int rowId = dgvExport.Rows.Add();
                    // Grab the new row!
                    row = dgvExport.Rows[rowId];
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in this.form.SeikyuuDenpyouIchiran.Rows[i].Cells)
                    {
                        string columnName = this.form.SeikyuuDenpyouIchiran.Columns[cell.ColumnIndex].Name;
                        if (ignoreColumns.Contains(columnName))
                        {
                            continue;
                        }
                        row.Cells[intColIndex].Value = cell.Value;

                        intColIndex++;
                    }
                }

                dgvExport.AllowUserToAddRows = false;
                dgvExport.Refresh();
            }
            catch (Exception ex)
            {
                dgvExport = new CustomDataGridView();
                LogUtility.Error("MakeDataGridViewExportCSV", ex);
                this.errMsg.MessageBoxShow("E245", "");
            }
            return dgvExport;
        }
        #endregion


        internal void SetPublishedUserSetting(string seikyuuNumber, string PublishedUserSetting)
        {
            SeikyuuUserSettingsDto seikyuuUserSettings = null;

            foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
            {
                if (row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value == null || !row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value.ToString().Equals(seikyuuNumber))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(PublishedUserSetting))
                {
                    seikyuuUserSettings = JsonUtility.DeserializeObject<SeikyuuUserSettingsDto>(PublishedUserSetting);
                    if (seikyuuUserSettings.KagamiUserList.Any())
                    {
                        var arr = seikyuuUserSettings.KagamiUserList.SelectMany(x => x.UserSettingInfos.Select(k => string.Concat(x.KagamiNumber, "-", k.UserSysId, "-", k.UserId, "-", Convert.ToInt32(k.IsSend)))).ToList();
                        PublishedUserSetting = string.Join(",", arr);
                    }
                }
                row.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = PublishedUserSetting;

                //Set [要確認]
                if (seikyuuUserSettings != null && seikyuuUserSettings.KagamiUserList.All(x => x.UserSettingInfos.Any(k=>k.IsSend)))
                {
                    row.Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = string.Empty;
                }
                else
                {
                    row.Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = ConstCls.NEED_USER_CONFIRMATION_TEXT;
                }
                break;
            }

            form.SeikyuuDenpyouIchiran.Invalidate();
        }

        internal void LoadUploadStatus(long[] seikyuuNumbers)
        {
            if (seikyuuNumbers == null || seikyuuNumbers.Length == 0)
            {
                return;
            }
            DataTable tbData = seikyuDenpyouDao.GetDataUpdateSeikyuu(seikyuuNumbers);
            if (tbData != null && tbData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.form.SeikyuuDenpyouIchiran.Rows)
                {
                    long seikyuuNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEIKYUU_NUMBER].Value);
                    string filter = string.Format("SEIKYUU_NUMBER = {0}", seikyuuNumber);
                    DataRow[] results = tbData.Select(filter);
                    if (results != null && results.Length > 0)
                    {
                        row.Cells[ConstCls.COL_HAKKOU].Value = false;
                        row.Cells[ConstCls.COL_UPLOAD_STATUS].Value = Convert.ToInt32(results[0]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>().ToEnumDescription();
                        row.Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = Convert.ToInt32(results[0]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>().ToEnumDescription();
                        row.Cells[ConstCls.COL_TIME_STAMP].Value = results[0]["TIME_STAMP"];
                        //row.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = DBNull.Value;
                    }
                }
                this.form.SeikyuuDenpyouIchiran.Refresh();
            }
        }


        public List<KagamiUserListDto> GetKagamiUserSettings(DataGridViewRow dgvRow)
        {
            if (dgvRow.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value != null
                    && !string.IsNullOrEmpty(dgvRow.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value.ToString()))
            {
                string[] arrUserSettings = dgvRow.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value.ToString().Split(',');
                List<KagamiUserDto> userSettings = new List<KagamiUserDto>();
                foreach (var arrUserSetting in arrUserSettings)
                {
                    var arr = arrUserSetting.Split('-');
                    KagamiUserDto userSetting = new KagamiUserDto
                    {
                        KagamiNumber = Convert.ToInt32(arr[0]),
                        UserSysId = Convert.ToInt64(arr[1]),
                        UserId = Convert.ToInt64(arr[2]),
                        IsSend = arr[3] == "1"
                    };
                    userSettings.Add(userSetting);
                }

                //
                return userSettings.GroupBy(x => x.KagamiNumber).Select(x => new KagamiUserListDto
                {
                    KagamiNumber = x.Key,
                    UserSettingInfos = x.Select(k => new UserSettingInfoDto
                    {
                        UserSysId = k.UserSysId,
                        UserId = k.UserId,
                        IsSend = k.IsSend
                    }).ToList()
                }).ToList();

            }

            return null;
        }

        /// <summary>
        /// 請求伝票の控え印刷区分更新
        /// </summary>
        /// <param name="dao">請求伝票DAO</param>
        /// <param name="seikyuNumber">請求番号</param>
        /// <returns></returns>
        public T_SEIKYUU_DENPYOU UpdateSeikyuDenpyouHikaeInsatsuKbn(string seikyuNumber)
        {
            try
            {
                T_SEIKYUU_DENPYOU seikyuuentity = seikyuDenpyouDao.GetDataByCd(seikyuNumber);

                if (seikyuuentity != null && !seikyuuentity.DELETE_FLG.IsTrue)
                {
                    seikyuuentity.HIKAE_INSATSU_KBN = true;
                    var dataBinderEntry = new DataBinderLogic<T_SEIKYUU_DENPYOU>(seikyuuentity);
                    dataBinderEntry.SetSystemProperty(seikyuuentity, false);
                    seikyuDenpyouDao.Update(seikyuuentity);
                }

                return seikyuDenpyouDao.GetDataByCd(seikyuNumber);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                return null;
            }
        }
    }
}