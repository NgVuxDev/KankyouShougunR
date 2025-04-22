using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using CommonChouhyouPopup.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Enums;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Adjustment.ShiharaiMeisaishoHakko;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using r_framework.Dto;
using System.IO;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.FormManager;

namespace Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko
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

        #endregion プロパティ

        #region フィールド

        ////<summary>
        ////パターン一覧のDao
        ////</summary>
        private TSDDaoCls seisanDenpyouDao;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 取引先_支払情報マスタ
        /// </summary>
        private MTSDaoCls TorihikisakiShiharaiDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderInxsShiharaiMeisaishoHakko.cs
        /// </summary>
        internal UIHeader headerForm;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// メッセージボックス
        /// </summary>
        private MessageBoxShowLogic errMsg = new MessageBoxShowLogic();

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO mSysInfo;

        /// <summary>
        /// DBシステム日付
        /// </summary>
        private string strSystemDate = string.Empty;

        private IT_SEISAN_DENPYOU_INXSDao seisanDenpyouInxsDao;

        private IT_SEISAN_DENPYOU_KAGAMI_INXSDao seisanDenpyouKagamiInxsDao;

        private IT_SEISAN_DENPYOU_KAGAMI_USER_INXSDao seisanDenpyouKagamiUserInxsDao;

        internal readonly string msgA = "対象データは存在しないため再度検索を実行してください。";
        internal readonly string msgB = "公開ユーザーが未設定です。再度確認してください。伝票番号：{0}";
        //internal readonly string msgC = "INXSに対象支払データをアップロードします。\nよろしいですか？";
        internal readonly string msgC = "処理が完了しました。";
        internal readonly string msgD = "アップロードに失敗しました。再度アップロードまたは検索を実行してください。\n繰り返し発生する場合はシステム管理者へ問い合わせてください。";
        internal readonly string msgE = "INXS担当者の権限がないため、アップロードできません";
        internal readonly string msgF = "該当するデータがありません";

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.SearchString = new DTOClass();
            this.seisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<MTSDaoCls>();

            this.seisanDenpyouInxsDao = DaoInitUtility.GetComponent<IT_SEISAN_DENPYOU_INXSDao>();
            this.seisanDenpyouKagamiInxsDao = DaoInitUtility.GetComponent<IT_SEISAN_DENPYOU_KAGAMI_INXSDao>();
            this.seisanDenpyouKagamiUserInxsDao = DaoInitUtility.GetComponent<IT_SEISAN_DENPYOU_KAGAMI_USER_INXSDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion コンストラクタ

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

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム設定
                this.mSysInfo = new DBAccessor().GetSysInfo();

                var parentForm = (BusinessBaseForm)this.form.Parent;

                strSystemDate = parentForm.sysDate.ToShortDateString();

                // ヘッダ項目の設定
                //================================CurrentUserCustomConfigProfile.xmlを読み込み============================
                XMLAccessor fileAccess = new XMLAccessor();
                CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();

                // ヘッダ拠点CD
                this.form.txtKyotenCd.Text = configProfile.ItemSetVal1.PadLeft(2, '0');

                // 拠点名称
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.form.txtKyotenCd.Text);
                // 拠点CDが空欄だと0検索されるためチェック
                if (mKyoten == null || string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
                {
                    this.form.txtKyotenMei.Text = "";
                }
                else
                {
                    this.form.txtKyotenMei.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }

                // 伝票日付FROM・TO
                if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitDenpyouHiduke))
                {
                    // 締め処理画面からの引継ぎ時
                    this.headerForm.tdpDenpyouHidukeFrom.Text = this.InitDto.InitDenpyouHiduke;
                    this.headerForm.tdpDenpyouHidukeTo.Text = this.InitDto.InitDenpyouHiduke;
                }
                else
                {
                    this.headerForm.tdpDenpyouHidukeFrom.Text = this.parentForm.sysDate.ToString("yyyy-MM-dd");
                    this.headerForm.tdpDenpyouHidukeTo.Text = this.parentForm.sysDate.ToString("yyyy-MM-dd");
                }

                // 明細項目の設定
                // 拠点CD
                this.headerForm.txtHeaderKyotenCd.Text = "99";

                // 拠点名称
                mKyoten = new M_KYOTEN();
                if (this.InitDto != null)
                {
                    // 締め処理画面からの引継ぎ時
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.InitDto.InitKyotenCd);

                    if (mKyoten == null || string.IsNullOrEmpty(this.InitDto.InitKyotenCd))
                    {
                        this.headerForm.txtHeaderKyotenCd.Text = String.Empty;
                        this.headerForm.txtHeaderKyotenMei.Text = String.Empty;
                    }
                    else
                    {
                        this.headerForm.txtHeaderKyotenCd.Text = this.InitDto.InitKyotenCd;
                        this.headerForm.txtHeaderKyotenMei.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }
                else
                {
                    // 通常起動時
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headerForm.txtHeaderKyotenCd.Text);
                    // 拠点CDが空欄だと0検索されるためチェック
                    if (mKyoten == null || string.IsNullOrEmpty(this.headerForm.txtHeaderKyotenCd.Text))
                    {
                        this.headerForm.txtHeaderKyotenCd.Text = String.Empty;
                        this.headerForm.txtHeaderKyotenMei.Text = String.Empty;
                    }
                    else
                    {
                        this.headerForm.txtHeaderKyotenMei.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }

                // 締日
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

                // 印刷順
                this.form.txtInsatsuJun.Text = ConstCls.PRINT_ORDER_FURIGANA;

                // 支払用紙
                this.form.txtShiharaiPaper.Text = ConstCls.SHIHARAI_PAPER_DATA_SAKUSEIJI_JISYA;

                // 支払形態
                this.form.txtShiharaiStyle.Text = ConstCls.SHIHARAI_KEITAI_DATA_SAKUSEIJI;

                // 明細
                //this.form.txtMeisai.Text = ConstCls.SHUKKIN_MEISAI_ARI;   // No.4004

                // 検索条件
                this.form.txtKensakuJouken.Text = string.Empty;

                // 支払明細書印刷日
                this.form.txtInsatsubi.Text = ConstCls.SHIHARAI_PRINT_DAY_SIMEBI;

                // 指定印刷日付
                this.form.dtpSiteiPrintHiduke.Text = string.Empty;

                // 支払明細書発行日
                this.form.txtShiharaiHakkou.Text = ConstCls.SHIHARAI_HAKKOU_PRINT_SHINAI;

                // 発行区分
                this.form.txtHikaeInsatsuKbn.Text = ConstCls.HIKAE_INSATSU_KBN_SUBETE;

                // 取引先
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                if (this.InitDto != null && !string.IsNullOrEmpty(this.InitDto.InitTorihiksiakiCd))
                {
                    // 締め処理画面からの引継ぎ時
                    M_TORIHIKISAKI mTorihikisaki = new M_TORIHIKISAKI();
                    mTorihikisaki = mtorihikisakiDao.GetDataByCd(this.InitDto.InitTorihiksiakiCd);

                    if (mTorihikisaki != null)
                    {
                        this.form.TORIHIKISAKI_CD.Text = mTorihikisaki.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }

                this.headerForm.txtHeaderKyotenCd.Select();
                this.headerForm.txtHeaderKyotenCd.Select();

                //支払明細書印刷日活性制御
                if (!CdtSiteiPrintHidukeEnable(this.form.txtInsatsubi.Text))
                {
                    return false;
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

            //控え印刷ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.PrintDirect);

            //プレビューボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Function5Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Function8Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //×ボタンで閉じる場合のイベント生成
            parentForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form.UIForm_FormClosing);

            //取引先コード入力欄でロストフォーカスイベント生成
            this.form.TORIHIKISAKI_CD.Leave += new EventHandler(TORIHIKISAKI_CD_Leave);

            //締日プルダウン値変更イベント生成
            this.form.cmbShimebi.TextChanged += new EventHandler(cmbShimebi_TextChanged);

            // 20141128 Houkakou 「支払明細書発行」のダブルクリックを追加する start
            // 「To」のイベント生成
            this.headerForm.tdpDenpyouHidukeTo.MouseDoubleClick += new MouseEventHandler(tdpDenpyouHidukeTo_MouseDoubleClick);
            // 20141128 Houkakou 「支払明細書発行」のダブルクリックを追加する end

            //[1]INXSアップロード
            parentForm.bt_process1.Click += new EventHandler(this.form.UploadToINXS);

            // Receive data call back from SubApp
            parentForm.OnReceiveMessageEvent += new BaseBaseForm.OnReceiveMessage(this.form.ParentForm_OnReceiveMessageEvent);
            parentForm.FormClosing += new FormClosingEventHandler(this.form.ParentForm_FormClosing);

            LogUtility.DebugMethodEnd();
        }

        #endregion 初期化

        #region ヘッダフォーム設定

        /// <summary>
        /// ヘッダフォーム設定
        /// </summary>
        /// <param name="hs">ヘッダフォーム</param>
        public void setHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
        }

        #endregion ヘッダフォーム設定

        #region [F5]プレビュー押下時

        /// <summary>
        /// プレビューボタン押下時処理
        /// </summary>
        internal bool Function5ClickLogic(bool printDirectFlg)
        {
            bool ret = true;
            try
            {
                //支払明細書指定日が4.指定の場合は、指定日の未入力チェックを行う
                if (this.form.txtInsatsubi.Text == ConstCls.SHIHARAI_PRINT_DAY_SITEI &&
                    this.form.dtpSiteiPrintHiduke.Value == null)
                {
                    this.errMsg.MessageBoxShow("E012", "指定日");
                    this.form.dtpSiteiPrintHiduke.Focus();
                }
                else
                {
                    //発行チェックボックスONカウント
                    int hakkouCnt = 0;

                    //支払明細書発行用DTO作成
                    ShiharaiDenpyouDto dto = new ShiharaiDenpyouDto();
                    dto.MSysInfo = this.mSysInfo;
                    //dto.Meisai = this.form.txtMeisai.Text;    // No.4004
                    dto.ShiharaiHakkou = this.form.txtShiharaiHakkou.Text;
                    dto.ShiharaiPrintDay = this.form.txtInsatsubi.Text;
                    dto.HakkoBi = this.strSystemDate;
                    if (this.form.dtpSiteiPrintHiduke.Value != null)
                    {
                        dto.ShiharaiDate = (DateTime)this.form.dtpSiteiPrintHiduke.Value;
                    }
                    dto.ShiharaiStyle = this.form.txtShiharaiStyle.Text;
                    dto.ShiharaiPaper = this.form.txtShiharaiPaper.Text;
                    dto.PrintDirectFlg = printDirectFlg;

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return ret;
                    }
                    bool isAbortRequired = false;
                    int printCount = 0;

                    //グリッドの発行列にチェックが付いているデータのみ処理を行う
                    foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                    {
                        if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                        {
                            hakkouCnt++;

                            DataTable dt = new DataTable();
                            dt.Columns.Add();

                            //
                            dto.TorihikisakiCd = row.Cells[ConstCls.COL_TORIHIKISAKI_CD].Value.ToString();

                            //印刷用データを取得
                            //G111支払明細書確認の処理を参考
                            //精算番号
                            string seisanNumber = row.Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString();

                            //精算伝票を取得
                            T_SEISAN_DENPYOU tseisandenpyou = this.seisanDenpyouDao.GetDataByCd(seisanNumber);

                            dto.ShiharaiPaper = tseisandenpyou.YOUSHI_KBN.Value.ToString();

                            //書式区分
                            string shoshikiKbn = tseisandenpyou.SHOSHIKI_KBN.ToString();
                            //書式明細区分
                            string shoshikiMeisaiKbn = tseisandenpyou.SHOSHIKI_MEISAI_KBN.ToString();
                            //出金明細区分 (支払携帯：2.単月支払明細の場合は、出金明細なしで固定)
                            string shukkinMeisaiKbn = "2";
                            if (this.form.txtShiharaiStyle.Text != "2")
                            {
                                shukkinMeisaiKbn = tseisandenpyou.SHUKKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = shukkinMeisaiKbn;  // No.4004

                            var result = ShiharaiDenpyouLogicClass.PreViewShiharaiDenpyouRemote(tseisandenpyou, dto, true, false, string.Empty, this.SearchString.ZeroKingakuTaishogai);
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
                        this.errMsg.MessageBoxShow("E050", "支払明細書発行");
                    }

                    //発行対象データが0件の場合はメッセージ表示
                    if (printCount == 0)
                    {
                        this.errMsg.MessageBoxShow("I008", "支払明細書");
                    }
                    else if (!isAbortRequired)
                    {
                        foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                        {
                            if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                            {
                                //精算伝票の発行区分更新
                                DataTable seisanDenpyo = ShiharaiDenpyouLogicClass.UpdateSeisanDenpyouHakkouKbnRemote(row.Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString(), (byte[])row.Cells[ConstCls.COL_TIME_STAMP].Value);

                                if (seisanDenpyo != null && seisanDenpyo.Rows.Count > 0)
                                {
                                    //タイムスタンプを設定
                                    row.Cells[ConstCls.COL_TIME_STAMP].Value = seisanDenpyo.Rows[0]["TIME_STAMP"];
                                }

                                if (printDirectFlg && (bool)row.Cells[ConstCls.COL_HIKAE_INSATSU_KBN].Value == false)
                                {
                                    var seikyuuEntity = this.UpdateSeikyuDenpyouHikaeInsatsuKbn(row.Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString());
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
                        this.form.dgvSeisanDenpyouItiran.Refresh();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function5ClickLogic", ex1);
                this.errMsg.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function5ClickLogic", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion [F5]プレビュー押下時

        #region [F8]検索押下時

        /// <summary>
        /// 検索ボタン押下時処理
        /// </summary>
        internal bool Function8ClickLogic()
        {
            bool ret = true;
            try
            {
                //入力チェックOKの場合のみ検索と表示を行う
                if (InputCheck())
                {
                    Search();
                    SetIchiran();

                    // 列ヘッダチェックボックスを初期化
                    this.form.chkHakko.Checked = false;

                    this.form.IsCheckedChangedEventRun = false;
                    this.form.IsCheckedChangedEventRun = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Function8ClickLogic", ex1);
                this.errMsg.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function8ClickLogic", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion [F8]検索押下時

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

                List<long> seisanNumbers = new List<long>();
                Dictionary<long, DataGridViewRow> dicUploadRows = new Dictionary<long, DataGridViewRow>(); 

                foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                {
                    if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value)
                    {
                        long seisanNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEISAN_NUMBER].Value);
                        seisanNumbers.Add(seisanNumber);
                        dicUploadRows.Add(seisanNumber, row);
                    }
                }
                if (!seisanNumbers.Any())
                {
                    this.errMsg.MessageBoxShowError(msgF); //Mess F
                    return;
                }

                Dictionary<string, List<KagamiUserListDto>> dicUserSettings = new Dictionary<string, List<KagamiUserListDto>>();
                #region Check [対象]＝ON and 公開ユーザー
                List<long> seisanNumberErrors = new List<long>();
                foreach (long seisanNumber in seisanNumbers)
                {
                    List<KagamiUserListDto> currentUserSettings = GetKagamiUserSettings(dicUploadRows[seisanNumber]);
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
                    DataTable tbUser = this.seisanDenpyouDao.GetPublishedUserSettingData(seisanNumber, ignoreUserSysIds, userSysIds);
                    if (tbUser == null || tbUser.Rows.Count == 0)
                    {
                        seisanNumberErrors.Add(seisanNumber);
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
                        seisanNumberErrors.Add(seisanNumber);
                        continue;
                    }

                    dicUserSettings.Add(seisanNumber.ToString(), userSettings);
                }

                //Check
                if (seisanNumberErrors.Any())
                {
                    this.errMsg.MessageBoxShowError(string.Format(msgB, string.Join("、", seisanNumberErrors))); //Msg B
                    return;
                }

                #endregion [対象]＝ON and 公開ユーザー

                if (this.form.txtInsatsubi.Text == ConstCls.SHIHARAI_PRINT_DAY_SITEI &&
                    this.form.dtpSiteiPrintHiduke.Value == null)
                {
                    this.errMsg.MessageBoxShow("E012", "指定日");

                    this.form.dtpSiteiPrintHiduke.Focus();
                }
                else
                {
                    //if (this.errMsg.MessageBoxShowConfirm(msgC, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    //{
                    //    return;
                    //}

                    if (!File.Exists(SystemProperty.InxsSettings.FilePath))
                    {
                        return;
                    }

                    //INXS支払明細書発行用DTO作成
                    ShiharaiDenpyouDto dto = new ShiharaiDenpyouDto();
                    dto.MSysInfo = this.mSysInfo;
                    dto.ShiharaiHakkou = this.form.txtShiharaiHakkou.Text;
                    dto.ShiharaiPrintDay = this.form.txtInsatsubi.Text;
                    dto.HakkoBi = this.strSystemDate;
                    if (this.form.dtpSiteiPrintHiduke.Value != null)
                    {
                        dto.ShiharaiDate = (DateTime)this.form.dtpSiteiPrintHiduke.Value;
                    }
                    dto.ShiharaiStyle = this.form.txtShiharaiStyle.Text;

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return;
                    }

                    var seisanEntities = this.seisanDenpyouDao.GetSeisanEntities(seisanNumbers.ToArray());
                    if (seisanEntities == null || seisanEntities.Length != seisanNumbers.Count)
                    {
                        this.errMsg.MessageBoxShowError(msgA); //Mess A
                        return;
                    }

                    Dictionary<string, T_SEISAN_DENPYOU> dicSeisanEntities = seisanEntities.ToDictionary(x => x.SEISAN_NUMBER.Value.ToString(), x => x);

                    Dictionary<string, List<KagamiFileExportDto>> dicKagamiFileExport = new Dictionary<string, List<KagamiFileExportDto>>();
                    transFolderPath = Path.Combine(Path.GetDirectoryName(SystemProperty.InxsSettings.FilePath), "Attachments", "G747", Guid.NewGuid().ToString("N"));
                    if (!Directory.Exists(transFolderPath))
                    {
                        Directory.CreateDirectory(transFolderPath);
                    }

                    bool isAbortRequired = false;
                    int printCount = 0;
                    foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                    {
                        if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add();

                            //
                            dto.TorihikisakiCd = row.Cells[ConstCls.COL_TORIHIKISAKI_CD].Value.ToString();
                            //精算番号
                            string seisanNumber = row.Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString();


                            //支払明細伝票を取得
                            T_SEISAN_DENPYOU tseisandenpyou = dicSeisanEntities[seisanNumber];
                            tseisandenpyou.TIME_STAMP = (byte[])row.Cells[ConstCls.COL_TIME_STAMP].Value;

                            dto.ShiharaiPaper = tseisandenpyou.YOUSHI_KBN.Value.ToString();

                            //入金明細区分 (支払明細携帯：2.単月支払明細の場合は、入金明細なしで固定)
                            string nyuukinMeisaiKbn = "2";
                            if (this.form.txtShiharaiStyle.Text != "2")
                            {
                                nyuukinMeisaiKbn = tseisandenpyou.SHUKKIN_MEISAI_KBN.ToString(); // No.4004
                            }
                            dto.Meisai = nyuukinMeisaiKbn;   // No.4004

                            string folderPath = Path.Combine(transFolderPath, seisanNumber);
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            var result = ShiharaiDenpyouLogicClass.PreViewShiharaiDenpyouRemote(tseisandenpyou, dto, true, true, folderPath, this.SearchString.ZeroKingakuTaishogai);

                            if (result != null)
                            {
                                printCount = (int)result[0];
                                dicKagamiFileExport.Add(seisanNumber, (List<KagamiFileExportDto>)result[1]);
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
                            long seisanNumber = long.Parse(item.Key);
                            var dicKagamiUserList = dicUserSettings[item.Key].ToDictionary(x => x.KagamiNumber, x => x.UserSettingInfos);

                            //Check TIME_STAMP
                            T_SEISAN_DENPYOU entity = dicSeisanEntities[item.Key];
                            this.seisanDenpyouDao.Update(entity);

                            T_SEISAN_DENPYOU_INXS denpyouInxsEntity = this.seisanDenpyouInxsDao.GetDataByCd(item.Key);
                            if (denpyouInxsEntity != null)
                            {
                                string oldFilePath = this.seisanDenpyouKagamiInxsDao.GetDataFolderPathUpload(item.Key);
                                if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(oldFilePath))
                                {
                                    oldFolderPaths.Add(Path.GetDirectoryName(oldFilePath));
                                }
                                //Delete kagami
                                this.seisanDenpyouKagamiInxsDao.DeleteBySeisan(item.Key);
                                //Delete kagamiUser
                                this.seisanDenpyouKagamiUserInxsDao.DeleteBySeisan(item.Key);

                                //Update
                                denpyouInxsEntity.UPLOAD_STATUS = (int)EnumUploadSatus.CHUU;
                                denpyouInxsEntity.DOWNLOAD_STATUS = (int)EnumDownloadSatus.MI;
                                this.seisanDenpyouInxsDao.Update(denpyouInxsEntity);
                            }
                            else
                            {
                                //Add new
                                denpyouInxsEntity = new T_SEISAN_DENPYOU_INXS()
                                {
                                    SEISAN_NUMBER = seisanNumber,
                                    UPLOAD_STATUS = (int)EnumUploadSatus.CHUU,
                                    DOWNLOAD_STATUS = (int)EnumDownloadSatus.MI
                                };
                                this.seisanDenpyouInxsDao.Insert(denpyouInxsEntity);
                            }

                            foreach (var kagamiItem in item.Value)
                            {
                                T_SEISAN_DENPYOU_KAGAMI_INXS kagamiEntity = new T_SEISAN_DENPYOU_KAGAMI_INXS()
                                {
                                    SEISAN_NUMBER = seisanNumber,
                                    KAGAMI_NUMBER = kagamiItem.KagamiNumber,
                                    POSTED_FILE_PATH = kagamiItem.FileExport
                                };
                                this.seisanDenpyouKagamiInxsDao.Insert(kagamiEntity);


                                var userSettings = dicKagamiUserList[kagamiItem.KagamiNumber];
                                foreach (var userSetting in userSettings)
                                {
                                    T_SEISAN_DENPYOU_KAGAMI_USER_INXS userEntity = new T_SEISAN_DENPYOU_KAGAMI_USER_INXS()
                                    {
                                        SEISAN_NUMBER = seisanNumber,
                                        KAGAMI_NUMBER = kagamiItem.KagamiNumber,
                                        USER_SYS_ID = userSetting.UserSysId
                                    };
                                    this.seisanDenpyouKagamiUserInxsDao.Insert(userEntity);
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
                        this.errMsg.MessageBoxShow("I008", "支払明細書");
                    }
                    else if (!isAbortRequired)
                    {
                        //seisanNumbers
                        List<UploadLoadDto> uploadList = new List<UploadLoadDto>();
                        DataTable tbData = seisanDenpyouDao.GetDataUpdateSeisan(seisanNumbers.ToArray());
                        if (tbData != null && tbData.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                            {
                                if ((bool)row.Cells[ConstCls.COL_HAKKOU].Value == true)
                                {
                                    long seisanNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEISAN_NUMBER].Value);
                                    string filter = string.Format("SEISAN_NUMBER = {0}", seisanNumber);
                                    DataRow[] results = tbData.Select(filter);
                                    if (results != null && results.Length > 0)
                                    {
                                        row.Cells[ConstCls.COL_HAKKOU].Value = false;
                                        row.Cells[ConstCls.COL_UPLOAD_STATUS].Value = Convert.ToInt32(results[0]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>().ToEnumDescription();
                                        row.Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = Convert.ToInt32(results[0]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>().ToEnumDescription();
                                        row.Cells[ConstCls.COL_TIME_STAMP].Value = results[0]["TIME_STAMP"];

                                        //Add
                                        UploadLoadDto uploadDto = new UploadLoadDto()
                                        {
                                            SEISAN_NUMBER = seisanNumber,
                                            TIME_STAMP = (byte[])results[0]["TIME_STAMP"]
                                        };
                                        uploadList.Add(uploadDto);
                                    }
                                }
                            }

                            form.chkHakko.CheckedChanged -= new EventHandler(form.checkBoxAll_CheckedChanged);
                            form.chkHakko.Checked = false;
                            form.chkHakko.CheckedChanged += new EventHandler(form.checkBoxAll_CheckedChanged);

                            if (uploadList.Any())
                            {
                                RemoteAppCls remoteAppCls = new RemoteAppCls();
                                var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                                {
                                    TransactionId = form.transactionUploadId,
                                    ReferenceID = "UploadInxs"
                                });
                                var arg = JsonUtility.SerializeObject<List<UploadLoadDto>>(uploadList);
                                FormManager.OpenFormSubApp("S017", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, arg, token, parentForm.Text);
                            }
                        }

                        //グリッドを再描画
                        this.form.dgvSeisanDenpyouItiran.Invalidate();
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

        #region 入力値チェック

        /// <summary>
        /// 検索必須項目入力チェック
        /// ラジオボタン未入力時に自動で値を設定する方針となった場合は不要
        /// </summary>
        /// <returns></returns>
        internal bool InputCheck()
        {
            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.form.cmbShimebi.Text))
            {
                MessageBox.Show(ConstCls.ErrStop2, ConstCls.DialogTitle);
                this.form.cmbShimebi.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.txtInsatsuJun.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "印刷順");
                this.form.txtInsatsuJun.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.txtShiharaiPaper.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "支払用紙");
                this.form.txtShiharaiPaper.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.txtShiharaiStyle.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "支払形態");
                this.form.txtShiharaiStyle.Focus();
                return false;
            }

            // No.4004-->
            //if (string.IsNullOrEmpty(this.form.txtMeisai.Text))
            //{
            //    MessageBox.Show(ConstCls.ErrStop6, ConstCls.DialogTitle);
            //    this.form.txtMeisai.Focus();
            //    return false;
            //}
            // No.4004<--

            if (string.IsNullOrEmpty(this.form.txtInsatsubi.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "支払年月日");
                this.form.txtInsatsubi.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.txtShiharaiHakkou.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "支払明細書発行日");
                this.form.txtShiharaiHakkou.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.txtHikaeInsatsuKbn.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "発行区分");
                this.form.txtHikaeInsatsuKbn.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.FILTERING_DATA.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "抽出データ");
                return false;
            }

            if (string.IsNullOrEmpty(this.form.UPLOAD_STATUS.Text))
            {
                errMsg.MessageBoxShow("E001", "アップロード状況");
                return false;
            }

            this.headerForm.tdpDenpyouHidukeFrom.IsInputErrorOccured = false;
            this.headerForm.tdpDenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            this.headerForm.tdpDenpyouHidukeTo.IsInputErrorOccured = false;
            this.headerForm.tdpDenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            if (!string.IsNullOrEmpty(this.headerForm.tdpDenpyouHidukeFrom.GetResultText())
                && !string.IsNullOrEmpty(this.headerForm.tdpDenpyouHidukeTo.GetResultText()))
            {
                DateTime dtpFrom = DateTime.Parse(this.headerForm.tdpDenpyouHidukeFrom.GetResultText());
                DateTime dtpTo = DateTime.Parse(this.headerForm.tdpDenpyouHidukeTo.GetResultText());
                DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                if (0 < diff)
                {
                    //対象期間内でないならエラーメッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    this.headerForm.tdpDenpyouHidukeFrom.IsInputErrorOccured = true;
                    this.headerForm.tdpDenpyouHidukeFrom.BackColor = Constans.ERROR_COLOR;
                    this.headerForm.tdpDenpyouHidukeTo.IsInputErrorOccured = true;
                    this.headerForm.tdpDenpyouHidukeTo.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E030", "精算日付From", "精算日付To");
                    this.headerForm.tdpDenpyouHidukeFrom.Select();
                    this.headerForm.tdpDenpyouHidukeFrom.Focus();

                    return false;
                }
            }

            return true;
        }

        #endregion 入力値チェック

        #region データ取得

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public virtual int Search()
        {
            LogUtility.DebugMethodStart();

            // SQL文
            this.SearchResult = new DataTable();
            if (this.headerForm.tdpDenpyouHidukeFrom.Value == null)
            {
                this.SearchString.DenpyoHizukeFrom = string.Empty;
            }
            else
            {
                this.SearchString.DenpyoHizukeFrom = this.headerForm.tdpDenpyouHidukeFrom.Value.ToString().Substring(0, 10);
            }
            if (this.headerForm.tdpDenpyouHidukeTo.Value == null)
            {
                this.SearchString.DenpyoHizukeTo = string.Empty;
            }
            else
            {
                this.SearchString.DenpyoHizukeTo = this.headerForm.tdpDenpyouHidukeTo.Value.ToString().Substring(0, 10);
            }
            this.SearchString.HakkouKyotenCD = this.headerForm.txtHeaderKyotenCd.Text;
            this.SearchString.Simebi = this.form.cmbShimebi.Text;
            this.SearchString.PrintOrder = int.Parse(this.form.txtInsatsuJun.Text);
            this.SearchString.ShiharaiPaper = int.Parse(this.form.txtShiharaiPaper.Text);
            this.SearchString.TorihikisakiCD = this.form.TORIHIKISAKI_CD.Text;

            SqlBoolean hikaeInsatsuKbn = SqlBoolean.Null;
            if (ConstCls.HIKAE_INSATSU_KBN_MIINSATUS.Equals(this.form.txtHikaeInsatsuKbn.Text))
            {
                hikaeInsatsuKbn = SqlBoolean.False;
            }
            else if (ConstCls.HIKAE_INSATSU_KBN_INSATSUZUMI.Equals(this.form.txtHikaeInsatsuKbn.Text))
            {
                hikaeInsatsuKbn = SqlBoolean.True;
            }
            else if (ConstCls.HIKAE_INSATSU_KBN_SUBETE.Equals(this.form.txtHikaeInsatsuKbn.Text))
            {
                hikaeInsatsuKbn = SqlBoolean.Null;
            }
            this.SearchString.HikaeInsatsuKbn = hikaeInsatsuKbn;

            //アップロード状況
            if (!this.form.UPLOAD_STATUS.Text.Equals(((int)EnumUploadSatus.SUBETE).ToString()))
            {
                this.SearchString.UploadStatus = int.Parse(this.form.UPLOAD_STATUS.Text);
            }
            else
            {
                this.SearchString.UploadStatus = null;
            }

            // 抽出データ
            int filteringData = 0;
            int.TryParse(this.form.FILTERING_DATA.Text, out filteringData);
            this.SearchString.FilteringData = filteringData;

            this.SearchString.ZeroKingakuTaishogai = this.form.ZERO_KINGAKU_TAISHOGAI.Checked;

            this.SearchResult = seisanDenpyouDao.GetDataForEntity(this.SearchString);
            int count = this.SearchResult.Rows.Count;

            // 読み込み件数の設定
            this.headerForm.txtYomikomiDataNum.Text = string.Format("{0:#,0}", Convert.ToDecimal(count.ToString()));

            if (count == 0)
            {
                errMsg.MessageBoxShow(ConstCls.ERR_MSG_CD_C001);
            }

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion データ取得

        #region データ表示

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            //前の結果をクリア
            int k = this.form.dgvSeisanDenpyouItiran.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.dgvSeisanDenpyouItiran.Rows.RemoveAt(this.form.dgvSeisanDenpyouItiran.Rows[i - 1].Index);
            }

            //検索結果を設定する
            var table = this.SearchResult;
            table.BeginLoadData();

            //検索結果設定
            for (int i = 0; i < table.Rows.Count; i++)
            {
                this.form.dgvSeisanDenpyouItiran.Rows.Add();
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_HAKKOU].Value = false;

                //公開ユーザー確認
                bool isNeedUserConfirmation = false;
                if (!table.Rows[i].IsNull("NEED_USER_CONFIRMATION"))
                {
                    isNeedUserConfirmation = Convert.ToBoolean(table.Rows[i]["NEED_USER_CONFIRMATION"]);
                }
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = isNeedUserConfirmation ? CommonConst.PUBLIC_USER_CONFIRM_TEXT : string.Empty;
                //公開ユーザー設定
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = table.Rows[i]["PUBLISHED_USER_SETTING"];
                //アップロード状況
                EnumUploadSatus uploadStatus = EnumUploadSatus.MI;
                if (!table.Rows[i].IsNull("UPLOAD_STATUS"))
                {
                    uploadStatus = Convert.ToInt32(table.Rows[i]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>();
                }
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_UPLOAD_STATUS].Value = uploadStatus.ToEnumDescription();
                //ダウンロード状況
                EnumDownloadSatus downloadStatus = EnumDownloadSatus.MI;
                if (!table.Rows[i].IsNull("DOWNLOAD_STATUS"))
                {
                    downloadStatus = Convert.ToInt32(table.Rows[i]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>();
                }
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = downloadStatus.ToEnumDescription();
                //伝票番号
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SEISAN_NUMBER].Value = table.Rows[i]["SEISAN_NUMBER"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SEISAN_DATE].Value = table.Rows[i]["SEISAN_DATE"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_TORIHIKISAKI_CD].Value = table.Rows[i]["TORIHIKISAKI_CD"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_TORIHIKISAKI_NAME].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SHIMEBI].Value = table.Rows[i]["SHIMEBI"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_ZENKAIKURIKOSHI_GAKU].Value = table.Rows[i]["ZENKAI_KURIKOSI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SHIHARAI_GAKU].Value = table.Rows[i]["KONKAI_SHUKKIN_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_CHOUSEI_GAKU].Value = table.Rows[i]["KONKAI_CHOUSEI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_KONKAI_SHIHARAI_GAKU].Value = table.Rows[i]["KONKAI_SHIHARAI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SHOHIZEI].Value = table.Rows[i]["SHOHIZEI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_KONKAI_SEISAN_GAKU].Value = table.Rows[i]["KONKAI_SEISAN_GAKU"];
                if (string.IsNullOrEmpty(table.Rows[i]["SHUKKIN_YOTEI_BI"].ToString()))
                {
                    this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SHIHARAI_YOTEI_BI].Value = string.Empty;
                }
                else
                {
                    this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_SHIHARAI_YOTEI_BI].Value = table.Rows[i]["SHUKKIN_YOTEI_BI"].ToString().Substring(0, 10);
                }

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_HAKKOU].ToolTipText = ConstCls.ToolTipText1;

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_TIME_STAMP].Value = table.Rows[i]["TIME_STAMP"];

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells[ConstCls.COL_HIKAE_INSATSU_KBN].Value = table.Rows[i]["HIKAE_INSATSU_KBN"];
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion データ表示

        #region グリッド発行列制御関連

        /// <summary>
        /// 列ヘッダチェックボックス表示
        /// </summary>
        /// <param name="e"></param>
        internal bool SeisanDenpyouItiranCellPaintingLogic(DataGridViewCellPaintingEventArgs e)
        {
            bool ret = true;
            try
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.ColumnIndex == 0 && e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - this.form.chkHakko.Width) / 2, (bmp.Height - this.form.chkHakko.Height + 28) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        this.form.chkHakko.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width) / 2;
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }

                //if (e.ColumnIndex == 1 && e.RowIndex == -1)
                //{
                //    using (Bitmap bmp2 = new Bitmap(100, 100))
                //    {
                //        // チェックボックスの描画領域を確保
                //        using (Graphics g = Graphics.FromImage(bmp2))
                //        {
                //            g.Clear(Color.Transparent);
                //        }

                //        // 描画領域の中央に配置
                //        Point pt1 = new Point((bmp2.Width - this.form.checkBoxAll_zumi.Width) / 2, (bmp2.Height - this.form.checkBoxAll_zumi.Height + 28) / 2);
                //        if (pt1.X < 0) pt1.X = 0;
                //        if (pt1.Y < 0) pt1.Y = 0;

                //        // Bitmapに描画
                //        this.form.checkBoxAll_zumi.DrawToBitmap(bmp2, new Rectangle(pt1.X, pt1.Y, bmp2.Width, bmp2.Height));

                //        // DataGridViewの現在描画中のセルの中央に描画
                //        int x = (e.CellBounds.Width - bmp2.Width) / 2; ;
                //        int y = (e.CellBounds.Height - bmp2.Height) / 2;

                //        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                //        e.Paint(e.ClipBounds, e.PaintParts);
                //        e.Graphics.DrawImage(bmp2, pt2);
                //        e.Handled = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogUtility.Error("SeisanDenpyouItiranCellPaintingLogic", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="e"></param>
        internal void SeisanDenpyouItiranCellClickLogic(DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart();

            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                this.form.chkHakko.Checked = !this.form.chkHakko.Checked;
                this.form.dgvSeisanDenpyouItiran.Refresh();
                this.form.chkHakko.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 発行列すべての行のチェック状態を切り替える
        /// </summary>
        internal bool checkBoxAllCheckedChangedLogic()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isChecked = false;
                foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                {
                    row.Cells[0].Value = this.form.chkHakko.Checked;
                    isChecked = true;
                }
                if (isChecked)
                {
                    //this.form.dgvSeisanDenpyouItiran.CurrentCell = this.form.dgvSeisanDenpyouItiran.Rows[0].Cells[1];
                    this.form.dgvSeisanDenpyouItiran.CurrentCell = this.form.dgvSeisanDenpyouItiran.Rows[0].Cells[0];
                }

                this.form.dgvSeisanDenpyouItiran.RefreshEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkBoxAllCheckedChangedLogic", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion グリッド発行列制御関連

        #region ラジオボタン項目未選択時の自動設定

        /// <summary>
        /// ラジオボタン項目未選択時の自動設定
        /// </summary>
        internal void SetOfRadioButtonNotSelectedLogic(r_framework.CustomControl.CustomNumericTextBox2 textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "1";
            }
        }

        #endregion ラジオボタン項目未選択時の自動設定

        #region 支払明細書印刷日制御

        /// <summary>
        /// 支払明細書印刷日活性・日活性制御
        /// </summary>
        /// <param name="shiharaiMeisaishoPrintdayVal">支払明細書印刷選択値</param>
        internal bool CdtSiteiPrintHidukeEnable(string shiharaiMeisaishoPrintdayVal)
        {
            bool ret = true;
            try
            {
                if (shiharaiMeisaishoPrintdayVal.Equals(ConstCls.SHIHARAI_PRINT_DAY_SITEI))
                {
                    this.form.dtpSiteiPrintHiduke.Enabled = true;
                }
                else
                {
                    this.form.dtpSiteiPrintHiduke.Value = null;
                    this.form.dtpSiteiPrintHiduke.Enabled = false;
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

        #endregion 支払明細書印刷日制御

        #region 取引先CDのフォーカスアウトイベント

        /// <summary>
        /// 取引先CDのフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Leave(object sender, EventArgs e)
        {
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
                    //締日チェック
                    var torihikisaki = TorihikisakiShiharaiDao.GetDataByCd(torihikisakiCd);
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

        #endregion

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

        // 20141128 Houkakou 「支払明細書発行」のダブルクリックを追加する start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdpDenpyouHidukeTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.headerForm.tdpDenpyouHidukeFrom;
            var ToTextBox = this.headerForm.tdpDenpyouHidukeTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141128 Houkakou 「支払明細書発行」のダブルクリックを追加する end

        public void PhysicalDelete()
        {
            //throw new NotImplementedException();
        }

        public void LogicalDelete()
        {
            //throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            //throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            //throw new NotImplementedException();
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

        internal void SetPublishedUserSetting(string seisanNumber, string PublishedUserSetting)
        {
            SeisanUserSettingsDto seisanUserSettings = null;

            foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
            {
                if (row.Cells[ConstCls.COL_SEISAN_NUMBER].Value == null || !row.Cells[ConstCls.COL_SEISAN_NUMBER].Value.ToString().Equals(seisanNumber))
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(PublishedUserSetting))
                {
                    seisanUserSettings = JsonUtility.DeserializeObject<SeisanUserSettingsDto>(PublishedUserSetting);
                    if (seisanUserSettings.KagamiUserList.Any())
                    {
                        var arr = seisanUserSettings.KagamiUserList.SelectMany(x => x.UserSettingInfos.Select(k => string.Concat(x.KagamiNumber, "-", k.UserSysId, "-", k.UserId, "-", Convert.ToInt32(k.IsSend)))).ToList();
                        PublishedUserSetting = string.Join(",", arr);
                    }
                }
                row.Cells[ConstCls.COL_PUBLISHED_USER_SETTING].Value = PublishedUserSetting;

                //Set [要確認]
                if (seisanUserSettings != null && seisanUserSettings.KagamiUserList.All(x => x.UserSettingInfos.Any(k => k.IsSend)))
                {
                    row.Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = string.Empty;
                }
                else
                {
                    row.Cells[ConstCls.COL_PUBLIC_USER_CONFIRM].Value = CommonConst.PUBLIC_USER_CONFIRM_TEXT;
                }
                break;
            }

            form.dgvSeisanDenpyouItiran.Invalidate();
        }

        internal void LoadUploadStatus(long[] seisanNumbers)
        {
            if (seisanNumbers == null || seisanNumbers.Length == 0)
            {
                return;
            }
            DataTable tbData = seisanDenpyouDao.GetDataUpdateSeisan(seisanNumbers);
            if (tbData != null && tbData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                {
                    long seisanNumber = Convert.ToInt64(row.Cells[ConstCls.COL_SEISAN_NUMBER].Value);
                    string filter = string.Format("SEISAN_NUMBER = {0}", seisanNumber);
                    DataRow[] results = tbData.Select(filter);
                    if (results != null && results.Length > 0)
                    {
                        row.Cells[ConstCls.COL_HAKKOU].Value = false;
                        row.Cells[ConstCls.COL_UPLOAD_STATUS].Value = Convert.ToInt32(results[0]["UPLOAD_STATUS"]).ToEnum<EnumUploadSatus>().ToEnumDescription();
                        row.Cells[ConstCls.COL_DOWNLOAD_STATUS].Value = Convert.ToInt32(results[0]["DOWNLOAD_STATUS"]).ToEnum<EnumDownloadSatus>().ToEnumDescription();
                        row.Cells[ConstCls.COL_TIME_STAMP].Value = results[0]["TIME_STAMP"];
                    }
                }
                this.form.dgvSeisanDenpyouItiran.Invalidate();
            }
        }



        /// <summary>
        /// 精算伝票の控え印刷区分更新
        /// </summary>
        /// <param name="dao">請求伝票DAO</param>
        /// <param name="seikyuNumber">請求番号</param>
        /// <returns></returns>
        public T_SEISAN_DENPYOU UpdateSeikyuDenpyouHikaeInsatsuKbn(string seisanNumber)
        {
            try
            {
                T_SEISAN_DENPYOU seisanEntity = seisanDenpyouDao.GetDataByCd(seisanNumber);

                if (seisanEntity != null && !seisanEntity.DELETE_FLG.IsTrue)
                {
                    seisanEntity.HIKAE_INSATSU_KBN = true;
                    var dataBinderEntry = new DataBinderLogic<T_SEISAN_DENPYOU>(seisanEntity);
                    dataBinderEntry.SetSystemProperty(seisanEntity, false);
                    seisanDenpyouDao.Update(seisanEntity);
                }

                return seisanDenpyouDao.GetDataByCd(seisanNumber);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                return null;
            }
        }
    }
}