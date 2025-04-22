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
using r_framework.CustomControl;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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
        private TSDDaoCls TsdDaoPatern;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        ///精算伝票
        /// </summary>
        private TSDDaoCls SeisanDenpyouDao;

        /// <summary>
        /// 取引先_支払情報マスタ
        /// </summary>
        private MTSDaoCls TorihikisakiShiharaiDao;

        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Adjustment.ShiharaiMeisaishoHakko.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderShiharaiMeisaishoHakko.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// BaseForm
        /// </summary>
        private BusinessBaseForm parentForm;

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
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private static GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

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
            this.TsdDaoPatern = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.SeisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<MTSDaoCls>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

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
                this.form.txtHakkoKbn.Text = ConstCls.HAKKOU_KBN_SUBETE;

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
                        this.form.TORIHIKISAKI_CD.Text = this.InitDto.InitTorihiksiakiCd;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }
                this.form.HIKAE_OUTPUT_KBN.Text = ConstCls.HIKAE_OUTPUT_NON;

                this.headerForm.txtHeaderKyotenCd.Select();
                this.headerForm.txtHeaderKyotenCd.Select();

                //請求書印刷日活性制御
                if (!CdtSiteiPrintHidukeEnable(this.form.txtInsatsubi.Text))
                {
                    return false;
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

            //プレビューボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Function5Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Function8Click);

            //登録ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.Function9Click);

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
            
            //#159156 start
            /// 明細のダブルクリック
            this.form.dgvSeisanDenpyouItiran.ColumnHeadersHeightChanged += new EventHandler(SeisanDenpyouItiran_ColumnHeadersHeightChanged);
            this.form.dgvSeisanDenpyouItiran.ColumnDividerDoubleClick += new DataGridViewColumnDividerDoubleClickEventHandler(SeisanDenpyouItiran_ColumnDividerDoubleClick);
            this.form.dgvSeisanDenpyouItiran.RowDividerDoubleClick += new DataGridViewRowDividerDoubleClickEventHandler(SeisanDenpyouItiran_RowDividerDoubleClick);
            //#159156 end
            LogUtility.DebugMethodEnd();
        }

        //#159156 start
        /// <summary>
        /// RowDividerDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_RowDividerDoubleClick(object sender, DataGridViewRowDividerDoubleClickEventArgs e)
        {
            this.form.dgvSeisanDenpyouItiran.Tag = this.form.dgvSeisanDenpyouItiran.ColumnHeadersHeight.ToString();
            return;
        }
        /// <summary>
        /// ColumnDividerDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_ColumnDividerDoubleClick(object sender, DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            this.form.dgvSeisanDenpyouItiran.Tag = this.form.dgvSeisanDenpyouItiran.ColumnHeadersHeight.ToString();
            return;
        }

        /// <summary>
        /// ColumnHeadersHeightChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SeisanDenpyouItiran_ColumnHeadersHeightChanged(object sender, EventArgs e)
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
        //#159156 end

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
        internal bool Function5ClickLogic()
        {
            bool ret = true;
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //支払明細書指定日が4.指定の場合は、指定日の未入力チェックを行う
                if (this.form.txtInsatsubi.Text == ConstCls.SHIHARAI_PRINT_DAY_SITEI &&
                    this.form.dtpSiteiPrintHiduke.Value == null)
                {
                    msgLogic.MessageBoxShow("E012", "指定日");

                    this.form.dtpSiteiPrintHiduke.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.HIKAE_OUTPUT_KBN.Text))
                {
                    msgLogic.MessageBoxShow("E001", "支払(控)印刷");
                    this.form.HIKAE_OUTPUT_KBN.Focus();
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

                    // 印刷シーケンスの開始
                    if (!ContinuousPrinting.Begin())
                    {
                        return ret;
                    }
                    bool isAbortRequired = false;
                    int printCount = 0;

                    //支払(控)印刷
                    //グループ印刷(HIKAE_OUTPUT_GROUP) →支払明細書発行処理を2回回す（支払明細書全部→控え全部）
                    //ソート印刷(HIKAE_OUTPUT_SORT)　→支払明細書と控えを交互で印刷する
                    //控えを印刷しない(HIKAE_OUTPUT_NON)　→支払明細書のみを印刷
                    int hikae_count = 1;
                    if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_GROUP)
                    {
                        hikae_count = 0;
                    }

                    for (int i = hikae_count; i <= 1; i++)
                    {
                        //グリッドの発行列にチェックが付いているデータのみ処理を行う
                        foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                        {
                            if ((bool)row.Cells["colHakkou"].Value == true)
                            {
                                hakkouCnt++;

                                DataTable dt = new DataTable();
                                dt.Columns.Add();

                                //
                                dto.TorihikisakiCd = row.Cells["colTorihikisakiCd"].Value.ToString();

                                //印刷用データを取得
                                //G111支払明細書確認の処理を参考
                                //精算番号
                                string seisanNumber = row.Cells["colDenpyoNumber"].Value.ToString();

                                //精算伝票を取得
                                T_SEISAN_DENPYOU tseisandenpyou = this.SeisanDenpyouDao.GetDataByCd(seisanNumber);
                                //書式区分
                                string shoshikiKbn = tseisandenpyou.SHOSHIKI_KBN.ToString();
                                //書式明細区分
                                string shoshikiMeisaiKbn = tseisandenpyou.SHOSHIKI_MEISAI_KBN.ToString();
                                //出金明細区分 (支払携帯：2.単月請求の場合は、出金明細なしで固定)
                                string shukkinMeisaiKbn = "2";
                                if (this.form.txtShiharaiStyle.Text != "2")
                                {
                                    shukkinMeisaiKbn = tseisandenpyou.SHUKKIN_MEISAI_KBN.ToString(); // No.4004
                                }
                                dto.Meisai = shukkinMeisaiKbn;  // No.4004

                                //精算伝票データ取得
                                DataTable seisanDt = GetSeisandenpyo(this.SeisanDenpyouDao, seisanNumber, shoshikiKbn, shoshikiMeisaiKbn, shukkinMeisaiKbn, this.SearchString.ZeroKingakuTaishogai);
                                this.form.ShiharaiDt = seisanDt;

                                if (seisanDt.Rows.Count != 0)
                                {
                                    //精算伝票データ設定

                                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                                    //printCount = SetSeisanDenpyo(seisanDt, dto, report_r337, true);
                                    var result = new ArrayList();
                                    if (seisanDt.Rows[0]["INVOICE_KBN"].ToString() == "2")
                                    {
                                        //適格請求書
                                        result = SetSeisanDenpyo_invoice(seisanDt, dto, true, false, "", this.headerForm.ZeiRate_Chk.Checked);
                                        if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_SORT)
                                        {
                                            result = SetSeisanDenpyo_invoice(seisanDt, dto, true, false, "", this.headerForm.ZeiRate_Chk.Checked);
                                        }
                                    }
                                    else
                                    {
                                        result = SetSeisanDenpyo(seisanDt, dto, true);
                                        if (this.form.HIKAE_OUTPUT_KBN.Text == ConstCls.HIKAE_OUTPUT_SORT)
                                        {
                                            result = SetSeisanDenpyo(seisanDt, dto, true);
                                        }
                                    } 
                                    printCount = (int)result[0];
                                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

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
                        msgLogic.MessageBoxShow("E050", "支払明細書発行");
                        return ret;
                    }

                    //発行対象データが0件の場合はメッセージ表示
                    if (printCount == 0)
                    {
                        msgLogic.MessageBoxShow("I008", "支払明細書");
                    }
                    else if (!isAbortRequired)
                    {
                        foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                        {
                            if ((bool)row.Cells["colHakkou"].Value == true)
                            {
                                //精算伝票の発行区分更新
                                DataTable seisanDenpyo = UpdateSeisanDenpyouHakkouKbn(SeisanDenpyouDao, row.Cells["colDenpyoNumber"].Value.ToString(), (byte[])row.Cells["colTimeStamp"].Value);

                                if (seisanDenpyo != null && seisanDenpyo.Rows.Count > 0)
                                {
                                    //発行済みチェックをON
                                    row.Cells["colHakkouzumi"].Value = seisanDenpyo.Rows[0]["HAKKOU_KBN"];
                                    //タイムスタンプを設定
                                    row.Cells["colTimeStamp"].Value = seisanDenpyo.Rows[0]["TIME_STAMP"];
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

        /// <summary>
        /// レポートフォーム作成
        /// </summary>
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <param name="aryPrint">帳票出力用データリスト</param>
        /// <returns></returns>
        public static FormReport CreateFormReport(ShiharaiDenpyouDto dto, ArrayList aryPrint)
        {
            FormReport formReport = null;

            // 現状では指定用紙がないが、条件判定だけ実装しておく
            if (dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SAKUSEIJI_JISYA
                   || dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_JISYA)
            {
                ReportInfoR337[] reportInfo = (ReportInfoR337[])aryPrint.ToArray(typeof(ReportInfoR337));
                formReport = new FormReport(reportInfo, "R337");
                formReport.Caption = "支払明細書";
            }
            else if (dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SAKUSEIJI_SHITEI
                         || dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SHITEI)
            {
                ReportInfoR337[] reportInfo = (ReportInfoR337[])aryPrint.ToArray(typeof(ReportInfoR337));
                formReport = new FormReport(reportInfo, "R337");
                formReport.Caption = "支払明細書";
            }

            return formReport;
        }

        /// <summary>
        /// 精算伝票の発行区分更新
        /// </summary>
        /// <param name="dao">精算伝票DAO</param>
        /// <param name="seisanNumber">精算番号</param>
        /// <param name="timeStamp">精算伝票のタイムスタンプ</param>
        /// <returns></returns>
        public static DataTable UpdateSeisanDenpyouHakkouKbn(TSDDaoCls dao, string seisanNumber, byte[] timeStamp)
        {
            T_SEISAN_DENPYOU seisanentitys = dao.GetDataByCd(seisanNumber);
            //発行区分を更新
            if (seisanentitys != null)
            {
                if (seisanentitys.DELETE_FLG == false)
                {
                    seisanentitys.HAKKOU_KBN = true;
                    var dataBinderEntry = new DataBinderLogic<T_SEISAN_DENPYOU>(seisanentitys);
                    dataBinderEntry.SetSystemProperty(seisanentitys, false);
                    UpdateT_SEISAN_DENPYOU(dao, seisanentitys);
                }
            }

            //タイムスタンプを再取得
            DataTable seisanDenpyo = dao.GetSeisanDenpyouUpdateData(seisanNumber);

            return seisanDenpyo;
        }

        /// <summary>
        /// 精算伝票データ取得
        /// </summary>
        /// <param name="dao">精算伝票DAO</param>
        /// <param name="seisanNumber">精算番号</param>
        /// <param name="shoshikiKbn">書式区分</param>
        /// <param name="shoshikiMeisaiKbn">書式明細区分</param>
        /// <param name="shukkinMeisaiKbn">出金明細区分</param>
        /// <returns></returns>
        public static DataTable GetSeisandenpyo(TSDDaoCls dao, string seisanNumber, string shoshikiKbn, string shoshikiMeisaiKbn, string shukkinMeisaiKbn, bool IsZeroKingakuTaishogai = false)
        {
            //①T_SEISAN_DENPYOU.SHOSHIKI_KBNが1：支払先別 且つ T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            //②T_SEISAN_DENPYOU.SHOSHIKI_KBNが2：業者別 且つ T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            string orderBy = " ";
            if ((ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                    && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                || (ConstCls.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                    && ConstCls.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.GYOUSHA_CD , TSDKE.GENBA_CD ";
            }
            //①T_SEISAN_DENPYOU.SHOSHIKI_KBNが1：支払先別 且つ T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ２：業者毎
            else if (ConstCls.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                    && (ConstCls.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.GYOUSHA_CD ";
            }

            DataTable seisanDt;

            if (shukkinMeisaiKbn == ConstCls.SHUKKIN_MEISAI_ARI)
            {
                seisanDt = dao.GetSeisanDenpyou(seisanNumber, shukkinMeisaiKbn, orderBy, IsZeroKingakuTaishogai);
            }
            else
            {
                seisanDt = dao.GetSeisanDenpyouMeisaiNashi(seisanNumber, shukkinMeisaiKbn, orderBy, shoshikiKbn, IsZeroKingakuTaishogai);

                if (seisanDt.Rows.Count > 0)
                {
                    // 入金のみ締めの場合はデータを表示。それ以外は検索条件に合わせて絞り込む。
                    DataRow[] tempRow = seisanDt.Select("DENPYOU_SHURUI_CD IS NULL");
                    if (tempRow != null && (seisanDt.Rows.Count == tempRow.Length))
                    {
                        // 入金のみ締め
                    }
                    else
                    {
                        // 各種伝票締め
                        if (!shoshikiKbn.Equals("1") && shukkinMeisaiKbn.Equals("2"))
                        {
                            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                            //seisanDt = seisanDt.Select("TSDK_GYOUSHA_CD <> ''").CopyToDataTable();
                            seisanDt = seisanDt.Select("GYOUSHA_CD <> ''").CopyToDataTable();
                            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
                        }
                    }
                }
            }

            return seisanDt;
        }

        /// <summary>
        /// 精算伝票データテーブル設定
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateSeisanPrintTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("GROUP_NAME");
            table.Columns.Add("DENPYOU_DATE");
            table.Columns.Add("DENPYOU_NUMBER");
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
        /// 精算伝票データ設定
        /// </summary>
        /// <param name="shiharaiDt">精算伝票データテーブル</param>
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <param name="reportR337">支払明細書レポート</param>
        
        public static ArrayList SetSeisanDenpyo(DataTable shiharaiDt, ShiharaiDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "")
        {

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            List<KagamiFileExportDto> kagamiFileExportList = null;
            if (isExportPDF)
            {
                kagamiFileExportList = new List<KagamiFileExportDto>();
            }
            ArrayList result = new ArrayList();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            int count = 0;
            ArrayList list;
            FormReport formReport;

            ReportInfoR337 reportR337;
            DataTable denpyouPrintTable = CreateSeisanPrintTable();
            DataTable shukkinPrintTable = CreateSeisanPrintTable();

            //先頭行の鏡番号を取得
            DataRow startRow = shiharaiDt.Rows[0];

            List<DataTable> csvDtList = new List<DataTable>();

            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.SHUKKIN_MEISAI_NASHI && string.IsNullOrEmpty(startRow["KAGAMI_NUMBER"].ToString()))
            {
                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR337 = new ReportInfoR337();
                reportR337.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                reportR337.Title = "支払明細書()";
                // XPSプロパティ - 発行済み
                reportR337.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                var drEmpty = denpyouPrintTable.NewRow();
                denpyouPrintTable.Rows.Add(drEmpty);

                if (printFlg)
                {
                    reportR337.CreateReportData(dto, startRow, denpyouPrintTable, shukkinPrintTable);

                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR337);
                    formReport = CreateFormReport(dto, list);
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    //// 印刷アプリ初期動作(プレビュー)
                    //formReport.PrintInitAction = 2;
                    //formReport.PrintXPS();

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                        formReport.PrintXPS();
                    }
                    else
                    {
                        //ExportPDF
                        string pdfFileName = string.Empty;
                        if (ConstCls.SHOSHIKI_KBN_1 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}", startRow["SEISAN_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TORIHIKISAKI_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_2 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}", startRow["SEISAN_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TORIHIKISAKI_CD"].ToString(), startRow["GYOUSHA_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_3 == startRow["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", startRow["SEISAN_NUMBER"].ToString(), startRow["SHIMEBI"].ToString(), startRow["TORIHIKISAKI_CD"].ToString(), startRow["GYOUSHA_CD"].ToString(), startRow["GENBA_CD"].ToString());
                        }

                        //直接印刷
                        string fileExport = formReport.ExportPDF(pdfFileName, path);

                        KagamiFileExportDto kagamiFileExport = new KagamiFileExportDto()
                        {
                            KagamiNumber = Convert.ToInt32(startRow["KAGAMI_NUMBER"]),
                            FileExport = fileExport
                        };
                        kagamiFileExportList.Add(kagamiFileExport);
                    }
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR337.CreateCsvData(dto, startRow, denpyouPrintTable, shukkinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
            }
            else
            {
                var isSeisanUchizei = false;
                var isSeisanSotozei = false;

                int nowKagamiNo = Convert.ToInt32(startRow["KAGAMI_NUMBER"]);
                
                for (int i = 0; i < shiharaiDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = shiharaiDt.Rows[i];

                    //鏡番号が同じか
                    if (Convert.ToInt16(tableRow["KAGAMI_NUMBER"]) != nowKagamiNo)
                    {
                        //帳票出力データテーブルを帳票出力データArrayListに格納
                        reportR337 = new ReportInfoR337();
                        reportR337.MSysInfo = dto.MSysInfo;

                        // XPSプロパティ - タイトル(取引先も表示させる)
                        DataRow[] rows = shiharaiDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                        reportR337.Title = "支払明細書(" + rows[0]["SHIHARAI_SOUFU_NAME1"] + ")";
                        // XPSプロパティ - 発行済み
                        reportR337.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                        if (printFlg)
                        {
                            reportR337.CreateReportData(dto, rows[0], denpyouPrintTable, shukkinPrintTable);

                            // 即時XPS出力
                            list = new ArrayList();
                            list.Add(reportR337);
                            formReport = CreateFormReport(dto, list);

                            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                            //// 印刷アプリ初期動作(プレビュー)
                            //formReport.PrintInitAction = 2;
                            //formReport.PrintXPS();

                            if (!isExportPDF)
                            {
                                // 印刷アプリ初期動作(プレビュー)
                                formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                                formReport.PrintXPS();
                            }
                            else
                            {
                                //ExportPDF
                                string pdfFileName = string.Empty;
                                if (ConstCls.SHOSHIKI_KBN_1 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}", rows[0]["SEISAN_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TORIHIKISAKI_CD"].ToString());
                                }
                                else if (ConstCls.SHOSHIKI_KBN_2 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}_{3}", rows[0]["SEISAN_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TORIHIKISAKI_CD"].ToString(), rows[0]["GYOUSHA_CD"].ToString());
                                }
                                else if (ConstCls.SHOSHIKI_KBN_3 == rows[0]["SHOSHIKI_KBN"].ToString())
                                {
                                    pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", rows[0]["SEISAN_NUMBER"].ToString(), rows[0]["SHIMEBI"].ToString(), rows[0]["TORIHIKISAKI_CD"].ToString(), rows[0]["GYOUSHA_CD"].ToString(), rows[0]["GENBA_CD"].ToString());
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
                            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end


                            formReport.Dispose();
                        }
                        else
                        {
                            DataTable dtData = reportR337.CreateCsvData(dto, rows[0], denpyouPrintTable, shukkinPrintTable);
                            csvDtList.Add(dtData);
                        }

                        count++;

                        //帳票出力データテーブルを初期化
                        denpyouPrintTable = CreateSeisanPrintTable();
                        shukkinPrintTable = CreateSeisanPrintTable();

                        nowKagamiNo = Convert.ToInt16(tableRow["KAGAMI_NUMBER"]);

                        isSeisanUchizei = false;
                        isSeisanSotozei = false;
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
                        tablePevRow = shiharaiDt.Rows[i - 1];
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (i == shiharaiDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = shiharaiDt.Rows[i + 1];
                    }

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString()))
                    {
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_UCHI == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeisanUchizei = true;
                        }
                        if (ConstCls.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstCls.ZEI_KBN_SOTO == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                        {
                            isSeisanSotozei = true;
                        }

                        var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                        var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();
                        var shoshikiGenbaKubun = startRow["SHOSHIKI_GENBA_KBN"].ToString();
                        // 出金明細行判定フラグ(出金は別出力なので、レポート用DataTableを分ける)
                        bool isShukkin = tableRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_20);

                        // 業者名設定
                        if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                        {
                            if (printFlg)
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
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
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                  || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                  || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
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
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_3 == shoshikiKubun && ConstCls.SHOSHIKI_GENBA_KBN_1 == shoshikiGenbaKubun && !isShukkin))
                            {
                                var drGenba = denpyouPrintTable.NewRow();
                                drGenba["GROUP_NAME"] = tableRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GENBA_NAME2"].ToString();
                                drGenba["DATA_KBN"] = "GENBA_GROUP";
                                denpyouPrintTable.Rows.Add(drGenba);
                            }
                        }
                        //精算伝票明細データ設定
                        //☆☆☆2-3☆☆☆
                        if (isShukkin)
                        {
                            SetSeisanDenpyoMeisei(tableRow, tablePevRow, shukkinPrintTable, tableNextRow, printFlg);
                        }
                        else
                        {
                            SetSeisanDenpyoMeisei(tableRow, tablePevRow, denpyouPrintTable, tableNextRow, printFlg);
                        }

                        // 現場金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                        {
                            // 現場名
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
                            {
                                var drGenbaGokei = denpyouPrintTable.NewRow();
                                drGenbaGokei["HINMEI_NAME"] = "現場計";
                                drGenbaGokei["KINGAKU"] = tableRow["GENBA_KINGAKU_1"].ToString();
                                drGenbaGokei["DATA_KBN"] = "GENBA_TOTAL";

                                decimal denpyouSotoZeiGaku;
                                decimal denpyouUchiZeiGaku;
                                GetDenpyouZei(shiharaiDt, tableRow, tableRow["GYOUSHA_CD"].ToString(), tableRow["GENBA_CD"].ToString(), out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, true);

                                //現場計：消費税(外税)
                                decimal genbaSoto = Convert.ToDecimal(tableRow["GENBA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                if (genbaSoto != 0)
                                {
                                    drGenbaGokei["SHOUHIZEI"] = genbaSoto;
                                }

                                //現場計：消費税(内税)
                                decimal genbaUchi = Convert.ToDecimal(tableRow["GENBA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                string biko = string.Empty;
                                if (genbaUchi != 0)
                                {
                                    biko = GetSyohizei(genbaUchi.ToString(), ConstCls.DENPYOU_ZEI_KBN_CD_2);
                                }

                                if (IsSeisanData(tableRow))
                                {
                                    if (string.IsNullOrEmpty(biko))
                                    {
                                        if (genbaSoto != 0 || genbaUchi != 0)
                                        {
                                            biko = ConstCls.SEISAN_ZEI_EXCEPT;
                                        }
                                    }
                                    else
                                    {
                                        biko += (ConstCls.ZENKAKU_SPACE + ConstCls.SEISAN_ZEI_EXCEPT);
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
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票)
                            // ※出金計項目は業者計を使用しているが、業者計or現場計を出力する場合は出金計を出力するため
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
                            {
                                var drGyoushaGokei = denpyouPrintTable.NewRow();
                                drGyoushaGokei["HINMEI_NAME"] = "業者計";
                                drGyoushaGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drGyoushaGokei["DATA_KBN"] = "GYOUSHA_TOTAL";

                                decimal denpyouSotoZeiGaku;
                                decimal denpyouUchiZeiGaku;
                                GetDenpyouZei(shiharaiDt, tableRow, tableRow["GYOUSHA_CD"].ToString(), null, out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, false);

                                //業者計：消費税(外税)
                                decimal gyoushaSoto = Convert.ToDecimal(tableRow["GYOUSHA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                if (gyoushaSoto != 0)
                                {
                                    drGyoushaGokei["SHOUHIZEI"] = gyoushaSoto;
                                }

                                //業者計：消費税(内税)
                                decimal gyoushaUchi = Convert.ToDecimal(tableRow["GYOUSHA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                string biko = string.Empty;
                                if (gyoushaUchi != 0)
                                {
                                    biko = GetSyohizei(gyoushaUchi.ToString(), ConstCls.DENPYOU_ZEI_KBN_CD_2);
                                }

                                if (IsSeisanData(tableRow))
                                {
                                    if (string.IsNullOrEmpty(biko))
                                    {
                                        if (gyoushaSoto != 0 || gyoushaUchi != 0)
                                        {
                                            biko = ConstCls.SEISAN_ZEI_EXCEPT;
                                        }
                                    }
                                    else
                                    {
                                        biko += (ConstCls.ZENKAKU_SPACE + ConstCls.SEISAN_ZEI_EXCEPT);
                                    }
                                }

                                drGyoushaGokei["MEISAI_BIKOU"] = biko;
                                denpyouPrintTable.Rows.Add(drGyoushaGokei);
                            }
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && isShukkin)
                               || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isShukkin)
                               || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isShukkin))
                            {
                                var drShukkinGokei = shukkinPrintTable.NewRow();
                                drShukkinGokei["HINMEI_NAME"] = "出金計";
                                drShukkinGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drShukkinGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                shukkinPrintTable.Rows.Add(drShukkinGokei);
                            }
                        }

                        if (tableNextRow == null || !tableRow["RANK_SEISAN_1"].Equals(tableNextRow["RANK_SEISAN_1"]))
                        {
                            //請求毎消費税(内)
                            decimal seikyuUchizei1 = 0;
                            if (tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"] != null)
                            {
                                seikyuUchizei1 = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
                            }
                            if (isSeisanUchizei)
                            {
                                var drSeikyuUchizei = denpyouPrintTable.NewRow();
                                drSeikyuUchizei["HINMEI_NAME"] = "【精算毎消費税(内)】";
                                drSeikyuUchizei["SHOUHIZEI"] = ConstCls.KAKKO_START + SetComma((tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]).ToString()) + ConstCls.KAKKO_END;
                                denpyouPrintTable.Rows.Add(drSeikyuUchizei);
                            }

                            //請求毎消費税(外)
                            decimal seikyuSotozei1 = 0;
                            if (tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"] != null)
                            {
                                seikyuSotozei1 = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);
                            }
                            if (isSeisanSotozei)
                            {
                                var drSeikyuSotozei = denpyouPrintTable.NewRow();
                                drSeikyuSotozei["HINMEI_NAME"] = "【精算毎消費税】";
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
                reportR337 = new ReportInfoR337();
                reportR337.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                DataRow[] dataRows = shiharaiDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                reportR337.Title = "支払明細書(" + dataRows[0]["SHIHARAI_SOUFU_NAME1"] + ")";
                // XPSプロパティ - 発行済み
                reportR337.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                if (printFlg)
                {
                    reportR337.CreateReportData(dto, dataRows[0], denpyouPrintTable, shukkinPrintTable);
                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR337);
                    formReport = CreateFormReport(dto, list);
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    //// 印刷アプリ初期動作(プレビュー)
                    //formReport.PrintInitAction = 2;
                    //formReport.PrintXPS();

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                        formReport.PrintXPS();
                    }
                    else
                    {
                        //ExportPDF
                        string pdfFileName = string.Empty;
                        if (ConstCls.SHOSHIKI_KBN_1 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}", dataRows[0]["SEISAN_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TORIHIKISAKI_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_2 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}", dataRows[0]["SEISAN_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TORIHIKISAKI_CD"].ToString(), dataRows[0]["GYOUSHA_CD"].ToString());
                        }
                        else if (ConstCls.SHOSHIKI_KBN_3 == dataRows[0]["SHOSHIKI_KBN"].ToString())
                        {
                            pdfFileName = string.Format("{0}_{1}_{2}_{3}_{4}", dataRows[0]["SEISAN_NUMBER"].ToString(), dataRows[0]["SHIMEBI"].ToString(), dataRows[0]["TORIHIKISAKI_CD"].ToString(), dataRows[0]["GYOUSHA_CD"].ToString(), dataRows[0]["GENBA_CD"].ToString());
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
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR337.CreateCsvData(dto, dataRows[0], denpyouPrintTable, shukkinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
            }

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            //return count;

            result.Add(count);
            if (isExportPDF)
            {
                result.Add(kagamiFileExportList);
            }
            return result;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
            
        }

        /// <summary>
        /// 現場、業者毎の伝票（内税・外税）額の合計を取得
        /// </summary>
        /// <param name="shiharaiDt">精算伝票データテーブル</param>
        /// <param name="tableRow">処理対象の精算伝票行</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denpyouUchiZeiGaku">伝票内税額合計</param>
        /// <param name="denpyouSotoZeiGaku">伝票外税額合計</param>
        /// <param name="isGenba">true:現場計取得, false:業者計取得</param>
        private static void GetDenpyouZei(DataTable shiharaiDt, DataRow tableRow, string gyoushaCd, string genbaCd, out decimal denpyouUchiZeiGaku, out decimal denpyouSotoZeiGaku, bool isGenba)
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

            foreach (DataRow dr in shiharaiDt.Rows)
            {
                // KAGAMI_NUMBER,GYOUSHA_CDで同一レコードを取得
                if (!dr["KAGAMI_NUMBER"].Equals(tableRow["KAGAMI_NUMBER"])
                    || !dr["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    continue;
                }

                // 現場計を取得する場合は、現場CDも一致条件に含める
                if (isGenba && !dr["GENBA_CD"].Equals(genbaCd))
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
        /// 精算毎データ有無
        /// </summary>
        /// <param name="tableRow"></param>
        /// <returns></returns>
        private static bool IsSeisanData(DataRow tableRow)
        {
            decimal seiUchiZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
            decimal seiSotoZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);

            bool result = 0 < (seiUchiZei + seiSotoZei);

            return result;
        }

        /// <summary>
        /// 精算伝票明細データ設定
        /// </summary>
        /// <param name="tableRow">精算伝票明細データ</param>
        /// <param name="tablePevRow">一行前精算伝票明細データ</param>
        /// <param name="printData">帳票出力用データテーブル</param>
        /// <param name="tableNextRow">一行後精算伝票明細データ</param>
        private static void SetSeisanDenpyoMeisei(DataRow tableRow, DataRow tablePevRow, DataTable printData, DataRow tableNextRow, bool printFlg)
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

            //品名
            dataRow["HINMEI_NAME"] = tableRow["HINMEI_NAME"].ToString();

            //数量
            if (ConstCls.DENPYOU_SHURUI_CD_20 == tableRow["DENPYOU_SHURUI_CD"].ToString())
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
            if (ConstCls.DENPYOU_SHURUI_CD_20 == tableRow["DENPYOU_SHURUI_CD"].ToString())
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
            if (ConstCls.DENPYOU_SHURUI_CD_20 == denpyou_shurui_cd)
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
                // 出金明細ではない場合
                if (ConstCls.DENPYOU_SHURUI_CD_20 != denpyou_shurui_cd)
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
                if (torihikisakiCd != null && !string.IsNullOrEmpty(torihikisakiCd))
                {
                    number = "支払明細書_" + torihikisakiCd;
                }
                else
                {
                    number = "支払明細書";
                }
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertDataTableToManyCsv(csvDtList, false, true, number, new SuperForm());
            }
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
                    this.form.checkBoxAll_zumi.Checked = false;
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

        #region [F9]データ登録

        /// <summary>
        /// 締処理エラーテーブルUPDATEデータ作成
        /// </summary>
        public bool Function9ClickLogic()
        {
            bool ret = true;
            try
            {
                // 明細に何もなければエラー
                if (this.form.dgvSeisanDenpyouItiran.Rows.Count <= 0)
                {
                    this.errMsg.MessageBoxShow("E061");
                    return ret;
                }

                if (this.errMsg.MessageBoxShow("C055", "登録") == DialogResult.Yes)
                {
                    // 削除されていない全てのEntityをDBから取得
                    var allDenpyou = this.SeisanDenpyouDao.GetAllData().Where(e => e.DELETE_FLG.IsFalse).ToList();

                    // DGVの精算番号List取得
                    var dgvSeisanNumberList = this.form.dgvSeisanDenpyouItiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["colDenpyoNumber"].Value).ToList();

                    // 精算番号ListをEntityのListにする
                    var deffEntitylist = new List<T_SEISAN_DENPYOU>();
                    dgvSeisanNumberList.ForEach(n => deffEntitylist.Add(new T_SEISAN_DENPYOU() { SEISAN_NUMBER = (Int64)n }));

                    // DGVに表示されている精算伝票のEntityを取得
                    var updateEntityList = allDenpyou.Where(d => d.DELETE_FLG.IsFalse).Intersect(deffEntitylist, new SeisanDenpyouPropComparer()).ToList();

                    // 発行済チェックが更新されていなければ登録対象外
                    foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                    {
                        updateEntityList.Remove(updateEntityList
                            .Where(w => w.SEISAN_NUMBER.Value.Equals(row.Cells["colDenpyoNumber"].Value)
                                && w.HAKKOU_KBN.Value.Equals(row.Cells["colHakkouzumi"].Value)).FirstOrDefault());
                    }

                    // 発行済チェックに更新のあった行だけ登録
                    updateEntityList.ForEach(f => f.HAKKOU_KBN = !f.HAKKOU_KBN);
                    //set 最終更新日、更新者、更新ＰＣ information
                    foreach (T_SEISAN_DENPYOU d in updateEntityList)
                    {
                        var databind = new DataBinderLogic<T_SEISAN_DENPYOU>(d);
                        databind.SetSystemProperty(d, false);
                    }
                    updateEntityList.ForEach(u => UpdateT_SEISAN_DENPYOU(this.SeisanDenpyouDao, u));
                    this.errMsg.MessageBoxShow("I001", "登録");
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Function9ClickLogic", ex1);
                this.errMsg.MessageBoxShow("E080", "");
                ret = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Function9ClickLogic", ex2);
                this.errMsg.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Function9ClickLogic", ex);
                this.errMsg.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion [F9]データ登録

        #region 入力値チェック

        /// <summary>
        /// 検索必須項目入力チェック
        /// ラジオボタン未入力時に自動で値を設定する方針となった場合は不要
        /// </summary>
        /// <returns></returns>
        internal bool InputCheck()
        {
            var messageShowLogic = new MessageBoxShowLogic();

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

            if (string.IsNullOrEmpty(this.form.txtHakkoKbn.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "発行区分");
                this.form.txtHakkoKbn.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.form.FILTERING_DATA.Text))
            {
                messageShowLogic.MessageBoxShow("E001", "抽出データ");
                return false;
            }

            // 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する start
            this.headerForm.tdpDenpyouHidukeFrom.IsInputErrorOccured = false;
            this.headerForm.tdpDenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            this.headerForm.tdpDenpyouHidukeTo.IsInputErrorOccured = false;
            this.headerForm.tdpDenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            // 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する end

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

                    // 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する start
                    this.headerForm.tdpDenpyouHidukeFrom.BackColor = Constans.ERROR_COLOR;
                    this.headerForm.tdpDenpyouHidukeTo.IsInputErrorOccured = true;
                    this.headerForm.tdpDenpyouHidukeTo.BackColor = Constans.ERROR_COLOR;
                    //msgLogic.MessageBoxShow("E030", this.headerForm.tdpDenpyouHidukeFrom.DisplayItemName, this.headerForm.tdpDenpyouHidukeTo.DisplayItemName);
                    msgLogic.MessageBoxShow("E030", "精算日付From", "精算日付To");
                    // 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する end

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

            SqlBoolean hakkoKbn = SqlBoolean.Null;
            if (ConstCls.HAKKOU_KBN_MIHAKKOU.Equals(this.form.txtHakkoKbn.Text))
            {
                hakkoKbn = SqlBoolean.False;
            }
            else if (ConstCls.HAKKOU_KBN_HAKKOUZUMI.Equals(this.form.txtHakkoKbn.Text))
            {
                hakkoKbn = SqlBoolean.True;
            }
            else if (ConstCls.HAKKOU_KBN_SUBETE.Equals(this.form.txtHakkoKbn.Text))
            {
                hakkoKbn = SqlBoolean.Null;
            }
            this.SearchString.HakkoKbn = hakkoKbn;

            // 抽出データ
            int filteringData = 0;
            int.TryParse(this.form.FILTERING_DATA.Text, out filteringData);
            this.SearchString.FilteringData = filteringData;

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            //将軍-INXS 支払明細書アップロード
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai())
            {
                this.SearchString.UseInxsShiharaiKbn = true;
            }
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            this.SearchString.ZeroKingakuTaishogai = this.form.ZERO_KINGAKU_TAISHOGAI.Checked;//VAN 20201125 #136236, #136230

            this.SearchResult = TsdDaoPatern.GetDataForEntity(this.SearchString);
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
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colHakkou"].Value = false;
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colHakkouzumi"].Value = table.Rows[i]["HAKKOU_KBN"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colDenpyoNumber"].Value = table.Rows[i]["SEISAN_NUMBER"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colSeisanDate"].Value = table.Rows[i]["SEISAN_DATE"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colTorihikisakiCd"].Value = table.Rows[i]["TORIHIKISAKI_CD"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colTorihikisakiName"].Value = table.Rows[i]["TORIHIKISAKI_NAME_RYAKU"];

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colShimebi"].Value = table.Rows[i]["SHIMEBI"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colZenkaiKurikoshiGaku"].Value = table.Rows[i]["ZENKAI_KURIKOSI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colShiharaiGaku"].Value = table.Rows[i]["KONKAI_SHUKKIN_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colChouseiGaku"].Value = table.Rows[i]["KONKAI_CHOUSEI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colKonkaiShiharaiGaku"].Value = table.Rows[i]["KONKAI_SHIHARAI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colShohizei"].Value = table.Rows[i]["SHOHIZEI_GAKU"];
                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colKonkaiSeisanGaku"].Value = table.Rows[i]["KONKAI_SEISAN_GAKU"];
                if (string.IsNullOrEmpty(table.Rows[i]["SHUKKIN_YOTEI_BI"].ToString()))
                {
                    this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colShiharaiYoteiBi"].Value = string.Empty;
                }
                else
                {
                    this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colShiharaiYoteiBi"].Value = table.Rows[i]["SHUKKIN_YOTEI_BI"].ToString().Substring(0, 10);
                }

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colHakkou"].ToolTipText = ConstCls.ToolTipText1;

                this.form.dgvSeisanDenpyouItiran.Rows[i].Cells["colTimeStamp"].Value = table.Rows[i]["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion データ表示

        #region データ更新

        /// <summary>
        /// 精算伝票テーブルUPDATE
        /// </summary>
        [Transaction]
        public static void UpdateT_SEISAN_DENPYOU(TSDDaoCls dao, T_SEISAN_DENPYOU val)
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

                if (e.ColumnIndex == 1 && e.RowIndex == -1)
                {
                    using (Bitmap bmp2 = new Bitmap(100, 100))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp2))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp2.Width - this.form.checkBoxAll_zumi.Width) / 2, (bmp2.Height - this.form.checkBoxAll_zumi.Height + 28) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        this.form.checkBoxAll_zumi.DrawToBitmap(bmp2, new Rectangle(pt1.X, pt1.Y, bmp2.Width, bmp2.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp2.Width) / 2; ;
                        int y = (e.CellBounds.Height - bmp2.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp2, pt2);
                        e.Handled = true;
                    }
                }
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

            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                this.form.checkBoxAll_zumi.Checked = !this.form.checkBoxAll_zumi.Checked;
                this.form.dgvSeisanDenpyouItiran.Refresh();
                this.form.checkBoxAll_zumi.Focus();
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

        /// <summary>
        /// 発行済みチェック列すべての行のチェック状態を切り替える
        /// </summary>
        internal bool checkBoxAllZumiCheckedChangedLogic()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                bool isChecked = false;
                foreach (DataGridViewRow row in this.form.dgvSeisanDenpyouItiran.Rows)
                {
                    row.Cells[1].Value = this.form.checkBoxAll_zumi.Checked;
                    isChecked = true;
                }
                if (isChecked)
                {
                    this.form.dgvSeisanDenpyouItiran.CurrentCell = this.form.dgvSeisanDenpyouItiran.Rows[0].Cells[1];
                }

                this.form.dgvSeisanDenpyouItiran.RefreshEdit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("checkBoxAllZumiCheckedChangedLogic", ex);
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
                    if (!string.IsNullOrEmpty(this.form.cmbShimebi.Text))
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

        /// <summary>
        /// 精算伝票データ設定（適格請求書用）
        /// </summary>
        /// <param name="shiharaiDt">精算伝票データテーブル</param>
        /// <param name="dto">支払明細書発行用DTO</param>
        public static ArrayList SetSeisanDenpyo_invoice(DataTable shiharaiDt, ShiharaiDenpyouDto dto, bool printFlg, bool isExportPDF = false, string path = "", bool ZeiHyouji = false)
        {
            ArrayList result = new ArrayList();
            int count = 0;
            ArrayList list;
            FormReport formReport;

            ReportInfoR771 reportR771;
            DataTable denpyouPrintTable = CreateSeisanPrintTable();
            DataTable shukkinPrintTable = CreateSeisanPrintTable();

            //先頭行の鏡番号を取得
            DataRow startRow = shiharaiDt.Rows[0];

            List<DataTable> csvDtList = new List<DataTable>();

            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.SHUKKIN_MEISAI_NASHI && string.IsNullOrEmpty(startRow["KAGAMI_NUMBER"].ToString()))
            {
                //帳票出力データテーブルを帳票出力データArrayListに格納
                reportR771 = new ReportInfoR771();
                reportR771.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                reportR771.Title = "支払明細書()";
                // XPSプロパティ - 発行済み
                reportR771.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                var drEmpty = denpyouPrintTable.NewRow();
                denpyouPrintTable.Rows.Add(drEmpty);

                if (printFlg)
                {
                    reportR771.CreateReportData(dto, startRow, denpyouPrintTable, shukkinPrintTable);

                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR771);
                    formReport = CreateFormReport_invoice(dto, list);

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                        formReport.PrintXPS();
                    }
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR771.CreateCsvData(dto, startRow, denpyouPrintTable, shukkinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
            }
            else
            {
                int nowKagamiNo = Convert.ToInt32(startRow["KAGAMI_NUMBER"]);

                for (int i = 0; i < shiharaiDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = shiharaiDt.Rows[i];

                    //鏡番号が同じか
                    if (Convert.ToInt16(tableRow["KAGAMI_NUMBER"]) != nowKagamiNo)
                    {
                        //帳票出力データテーブルを帳票出力データArrayListに格納
                        reportR771 = new ReportInfoR771();
                        reportR771.MSysInfo = dto.MSysInfo;

                        // XPSプロパティ - タイトル(取引先も表示させる)
                        DataRow[] rows = shiharaiDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                        reportR771.Title = "支払明細書(" + rows[0]["SHIHARAI_SOUFU_NAME1"] + ")";
                        // XPSプロパティ - 発行済み
                        reportR771.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                        if (printFlg)
                        {
                            reportR771.CreateReportData(dto, rows[0], denpyouPrintTable, shukkinPrintTable);

                            // 即時XPS出力
                            list = new ArrayList();
                            list.Add(reportR771);
                            formReport = CreateFormReport_invoice(dto, list);

                            if (!isExportPDF)
                            {
                                // 印刷アプリ初期動作(プレビュー)
                                formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                                formReport.PrintXPS();
                            }
                            formReport.Dispose();
                        }
                        else
                        {
                            DataTable dtData = reportR771.CreateCsvData(dto, rows[0], denpyouPrintTable, shukkinPrintTable);
                            csvDtList.Add(dtData);
                        }

                        count++;

                        //帳票出力データテーブルを初期化
                        denpyouPrintTable = CreateSeisanPrintTable();
                        shukkinPrintTable = CreateSeisanPrintTable();

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
                        tablePevRow = shiharaiDt.Rows[i - 1];
                    }

                    //現在行の次の行
                    DataRow tableNextRow = null;
                    if (i == shiharaiDt.Rows.Count - 1)
                    {
                        //現在行の次の行
                        tableNextRow = null;
                    }
                    else
                    {
                        //現在行の次の行
                        tableNextRow = shiharaiDt.Rows[i + 1];
                    }

                    //明細情報が存在しない場合は、明細出力を行わない
                    if (!string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString()))
                    {

                        var shoshikiKubun = startRow["SHOSHIKI_KBN"].ToString();
                        var shoshikiMeisaiKubun = startRow["SHOSHIKI_MEISAI_KBN"].ToString();
                        var shoshikiGenbaKubun = startRow["SHOSHIKI_GENBA_KBN"].ToString();
                        // 出金明細行判定フラグ(出金は別出力なので、レポート用DataTableを分ける)
                        bool isShukkin = tableRow["DENPYOU_SHURUI_CD"].ToString().Equals(ConstCls.DENPYOU_SHURUI_CD_20);

                        // 業者名設定
                        if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                        {
                            if (printFlg)
                            {
                                // 業者名
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                    || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
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
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=2 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // or
                                // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                                // のとき出力
                                if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                  || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                  || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
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
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_3 == shoshikiKubun && ConstCls.SHOSHIKI_GENBA_KBN_1 == shoshikiGenbaKubun && !isShukkin))
                            {
                                var drGenba = denpyouPrintTable.NewRow();
                                drGenba["GROUP_NAME"] = tableRow["GENBA_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + tableRow["GENBA_NAME2"].ToString();
                                drGenba["DATA_KBN"] = "GENBA_GROUP";
                                denpyouPrintTable.Rows.Add(drGenba);
                            }
                        }
                        //精算伝票明細データ設定
                        //☆☆☆2-3☆☆☆
                        if (isShukkin)
                        {
                            SetSeisanDenpyoMeisei_invoice(tableRow, tablePevRow, shukkinPrintTable, tableNextRow, printFlg, ZeiHyouji);
                        }
                        else
                        {
                            SetSeisanDenpyoMeisei_invoice(tableRow, tablePevRow, denpyouPrintTable, tableNextRow, printFlg, ZeiHyouji);
                        }

                        // 現場金額と消費税設定
                        if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                        {
                            // 現場名
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=1 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // or
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票以外)
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
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
                            // (T_SEIKYU_DENPYOU.SHOSHIKI_KBN=2 && T_SEIKYU_DENPYOU.SHOSHIKI_MEISAI_KBN=3 && 出金伝票)
                            // ※出金計項目は業者計を使用しているが、業者計or現場計を出力する場合は出金計を出力するため
                            // のとき出力
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && !isShukkin)
                                || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && !isShukkin))
                            {
                                var drGyoushaGokei = denpyouPrintTable.NewRow();
                                drGyoushaGokei["HINMEI_NAME"] = "業者計";
                                drGyoushaGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drGyoushaGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                denpyouPrintTable.Rows.Add(drGyoushaGokei);
                            }
                            if ((ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_2 == shoshikiMeisaiKubun && isShukkin)
                               || (ConstCls.SHOSHIKI_KBN_1 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isShukkin)
                               || (ConstCls.SHOSHIKI_KBN_2 == shoshikiKubun && ConstCls.SHOSHIKI_MEISAI_KBN_3 == shoshikiMeisaiKubun && isShukkin))
                            {
                                var drShukkinGokei = shukkinPrintTable.NewRow();
                                drShukkinGokei["HINMEI_NAME"] = "出金計";
                                drShukkinGokei["KINGAKU"] = tableRow["GYOUSHA_KINGAKU_1"].ToString();
                                drShukkinGokei["DATA_KBN"] = "GYOUSHA_TOTAL";
                                shukkinPrintTable.Rows.Add(drShukkinGokei);
                            }
                        }

                        if (tableNextRow == null || !tableRow["RANK_SEISAN_1"].Equals(tableNextRow["RANK_SEISAN_1"]))
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
                reportR771 = new ReportInfoR771();
                reportR771.MSysInfo = dto.MSysInfo;

                // XPSプロパティ - タイトル(取引先も表示させる)
                DataRow[] dataRows = shiharaiDt.Select("KAGAMI_NUMBER = " + nowKagamiNo);
                reportR771.Title = "支払明細書(" + dataRows[0]["SHIHARAI_SOUFU_NAME1"] + ")";
                // XPSプロパティ - 発行済み
                reportR771.Hakkouzumi = "発行済"; // TODO: 発行済み判別文字列の決定

                if (printFlg)
                {
                    reportR771.CreateReportData(dto, dataRows[0], denpyouPrintTable, shukkinPrintTable);
                    // 即時XPS出力
                    list = new ArrayList();
                    list.Add(reportR771);
                    formReport = CreateFormReport_invoice(dto, list);

                    if (!isExportPDF)
                    {
                        // 印刷アプリ初期動作(プレビュー)
                        formReport.PrintInitAction = dto.PrintDirectFlg ? 4 : 2;//add PrintDirect option refs #158003
                        formReport.PrintXPS();
                    }
                    formReport.Dispose();
                }
                else
                {
                    // CSV出力
                    DataTable dtData = reportR771.CreateCsvData(dto, dataRows[0], denpyouPrintTable, shukkinPrintTable);
                    csvDtList.Add(dtData);
                    ExportCsvPrint(csvDtList, dto.TorihikisakiCd);
                }

                count++;
            }

            result.Add(count);
            return result;
        }

        /// <summary>
        /// 精算伝票明細データ設定（適格請求書用）
        /// </summary>
        /// <param name="tableRow">精算伝票明細データ</param>
        /// <param name="tablePevRow">一行前精算伝票明細データ</param>
        /// <param name="printData">帳票出力用データテーブル</param>
        /// <param name="tableNextRow">一行後精算伝票明細データ</param>
        private static void SetSeisanDenpyoMeisei_invoice(DataRow tableRow, DataRow tablePevRow, DataTable printData, DataRow tableNextRow, bool printFlg, bool ZeiHyouji)
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

            //品名
            dataRow["HINMEI_NAME"] = tableRow["HINMEI_NAME"].ToString();

            //数量
            if (ConstCls.DENPYOU_SHURUI_CD_20 == tableRow["DENPYOU_SHURUI_CD"].ToString())
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
            if (ConstCls.DENPYOU_SHURUI_CD_20 == tableRow["DENPYOU_SHURUI_CD"].ToString())
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
            if (ConstCls.DENPYOU_SHURUI_CD_20 == denpyou_shurui_cd)
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
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <param name="aryPrint">帳票出力用データリスト</param>
        /// <returns></returns>
        public static FormReport CreateFormReport_invoice(ShiharaiDenpyouDto dto, ArrayList aryPrint)
        {
            FormReport formReport = null;

            // 現状では指定用紙がないが、条件判定だけ実装しておく
            if (dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SAKUSEIJI_JISYA
                   || dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_JISYA)
            {
                ReportInfoR771[] reportInfo = (ReportInfoR771[])aryPrint.ToArray(typeof(ReportInfoR771));
                formReport = new FormReport(reportInfo, "R771");
                formReport.Caption = "支払明細書";
            }
            else if (dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SAKUSEIJI_SHITEI
                         || dto.ShiharaiPaper == ConstCls.SHIHARAI_PAPER_DATA_SHITEI)
            {
                ReportInfoR771[] reportInfo = (ReportInfoR771[])aryPrint.ToArray(typeof(ReportInfoR771));
                formReport = new FormReport(reportInfo, "R771");
                formReport.Caption = "支払明細書";
            }

            return formReport;
        }
    
    }
}

/// <summary>
/// T_SEISAN_DENPYOUクラスの比較演算
/// </summary>
internal class SeisanDenpyouPropComparer : IEqualityComparer<T_SEISAN_DENPYOU>
{
    public bool Equals(T_SEISAN_DENPYOU x, T_SEISAN_DENPYOU y)
    {
        return x.SEISAN_NUMBER.Value == y.SEISAN_NUMBER.Value;
    }

    public int GetHashCode(T_SEISAN_DENPYOU obj)
    {
        return obj.SEISAN_NUMBER.Value.GetHashCode();
    }
}