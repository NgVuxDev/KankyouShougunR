using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Adjustment.Shiharaishimesyori.DAO;
using Shougun.Core.Adjustment.Shiharaishimesyori.DTO;
using Shougun.Core.Adjustment.Shiharaishimesyori.Logic;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using r_framework.CustomControl;
using System.Data.SqlTypes;

namespace Shougun.Core.Adjustment.Shiharaishimesyori
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// DTO
        /// </summary>
        private ShiharaiShimeShoriDispDto dto;

        /// <summary>
        /// SeikyuShimeShoriDao
        /// </summary>
        private ShiharaiShimeShoriDao shimeShoriDao;

        /// <summary>
        /// ShimeshorichuuDao
        /// </summary>
        private ShimeShoriChuuDao shimeshorichuuDao;

        //締め処理へ渡すデータ作成用
        /// <summary>
        /// SeikyuShimeShoriDto
        /// </summary>
        private SeikyuShimeShoriDto shimeDto;

        /// <summary>
        /// List<SeikyuShimeShoriDto>
        /// </summary>
        private List<SeikyuShimeShoriDto> shimeDataList;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = string.Empty;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_TORIHIKISAKI_SHIHARAIDao
        /// </summary>
        private IM_TORIHIKISAKI_SHIHARAIDao shiharaiDao;

        /// <summary>
        /// SeisanDenpyouDao
        /// </summary>
        private SeisanDenpyouDao denpyouDao;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// shimeChangeFlag[false:期間画面/true:伝票画面or明細画面]
        /// </summary>
        internal bool shimeChangeFlag = false;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao mkyotenDao;

        ///// <summary>
        ///// 前回表示の拠点CD
        ///// </summary>
        //private string beforeKyotenCd = string.Empty;

        ///// <summary>
        ///// 前回表示の拠点名称
        ///// </summary>
        //private string beforeKyotenName = string.Empty;

        ///// <summary>
        ///// 前回表示の取引先CD
        ///// </summary>
        //private string beforeTorihikisakiCd = string.Empty;

        ///// <summary>
        ///// 前回表示の取引先名称
        ///// </summary>
        //private string beforeTorihikisakiName = string.Empty;

        ///// <summary>
        ///// 前回表示の締日
        ///// </summary>
        //private string beforeShimebi = string.Empty;

        ///// <summary>
        ///// 前回表示の請求締日FROM
        ///// </summary>
        //private string beforeSeikyuShimebiFrom = string.Empty;

        ///// <summary>
        ///// 前回表示の請求締日TO
        ///// </summary>
        //private string beforeSeikyuShimebiTo = string.Empty;

        ///// <summary>
        ///// 前回表示の伝票種類
        ///// </summary>
        //private string beforeDenpyouShurui = string.Empty;

        /// <summary>
        /// firstFlag[false:初期起動]
        /// </summary>
        private bool firstFlag = false;

        /// <summary>
        /// 締め処理中かどうか確認するためのフラグ
        /// </summary>
        private bool bShimeSyoriZikkochu = false;

        ///// <summary>
        ///// 変更前の締日
        ///// </summary>
        //private string enterShimebi = string.Empty;

        //チェッククラスへ渡すデータ作成用
        /// <summary>
        /// CheckDto
        /// </summary>
        private CheckDto chkDto;

        /// <summary>
        /// List<CheckDto>
        /// </summary>
        private List<CheckDto> checkDataList;

        private DataTable kyotenResult { get; set; }
        private DataTable torihikisakiResult { get; set; }
        private DataTable dispResult { get; set; }

        /// <summary>
        /// 伝種区分名 - 受入
        /// </summary>
        private string denshuName_Ukeire = "受入";

        /// <summary>
        /// 伝種区分名 - 出荷
        /// </summary>
        private string denshuName_Shukka = "出荷";

        /// <summary>
        /// 伝種区分名 - 売上/支払
        /// </summary>
        private string denshuName_UrSh = "売上/支払";

        /// <summary>
        /// 伝種区分名 - 出金
        /// </summary>
        private string denshuName_Shukkin = "出金";

        /// <summary>
        /// 伝種区分マスタ
        /// </summary>
        private IM_DENSHU_KBNDao mdenshuKbnDao;

        internal MessageBoxShowLogic msgLogic;

        ///// <summary>
        ///// 拠点の前回値保持用
        ///// </summary>
        //internal string oldKyotenCd;

        // 支払日付
        DataTable shiharadata;

        //InxsShiharaiフラグ refs #158003
        internal readonly bool isInxsShiharai;

        //入力エラーフラグ 
        internal bool inputErrorFlg = false;
        //配列入力制御 
        internal Control[] arrInputControl;
        //辞書には、コントロールの以前のテキストが含まれています
        internal Dictionary<Control, string> dicPrevValue = new Dictionary<Control, string>();

        /// <summary>
        /// 締処理の実行区分[true:旧請求締処理/false:適格請求書]
        /// </summary>
        internal bool OldInvoiceFlag = false;

        /// <summary>
        /// システム設定-支払明細タブ：旧支払明細書印刷「1.する」「2.しない」
        /// </summary>
        internal string OldShiharaiPrintKBN;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            //システム設定取得
            this.form = targetForm;
            DBAccessor accessor = new DBAccessor();
            M_SYS_INFO sysInfo = accessor.GetSysInfo();

            //拠点取得ＤＡＯのインスタンス化
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mdenshuKbnDao = DaoInitUtility.GetComponent<IM_DENSHU_KBNDao>();
            this.shimeshorichuuDao = DaoInitUtility.GetComponent<ShimeShoriChuuDao>();

            this.dto = new ShiharaiShimeShoriDispDto();
            this.shimeShoriDao = DaoInitUtility.GetComponent<ShiharaiShimeShoriDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.shiharaiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKI_SHIHARAIDao>();
            this.denpyouDao = DaoInitUtility.GetComponent<SeisanDenpyouDao>();
            this.msgLogic = new MessageBoxShowLogic();

            this.arrInputControl = new Control[] { this.form.txtDenpyouShurui, this.form.cmbShimebi
                                            , this.form.txtKyotenCd, this.form.txtKyotenName 
                                            , this.form.txtTorihikisakiCd, this.form.txtTorihikisakiName
                                            , this.form.dtpSearchDateFrom, this.form.dtpSearchDateTo};
            this.isInxsShiharai = r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai();
            this.OldShiharaiPrintKBN = sysInfo.OLD_SHIHARAI_PRINT_KBN.ToString();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                //締日(プルダウン値)設定
                this.SetShimebi();

                #region DGVヘッダの幅変更

                //DGVヘッダの幅変更
                this.form.customDataGridView1.ColumnHeadersHeight = 47;

                #endregion

                #region 画面表示切替

                #region 期間+伝票画面 又は 期間+明細画面

                if (this.shimeChangeFlag)
                {
                    //伝票締め画面 又は 明細締め画面
                    //伝票
                    //表示に設定
                    this.form.lblTorihikisaki.Visible = true;
                    this.form.txtTorihikisakiCd.Visible = true;
                    this.form.txtTorihikisakiCd.Text = "";
                    this.form.txtTorihikisakiName.Visible = true;
                    this.form.txtTorihikisakiName.Text = "";
                    this.form.btnTorihikisakiPopup.Visible = true;
                    //ラベル「伝票種類」の名称を「伝票種類※」へ変更
                    this.form.lblDenpyouShurui.Text = "伝票種類※";
                    //ラベル「請求締日」の名称を「検索期間」へ変更
                    this.form.lblShiharaiShimebi.Text = "検索期間※";
                    // ラベル「取引先」の名称を「取引先※」へ変更
                    this.form.lblTorihikisaki.Text = "取引先※";
                    //請求締日Toと波線を表示
                    this.form.lb_hasen.Visible = true;
                    this.form.dtpSearchDateTo.Visible = true;
                    //編集可能に設定
                    this.form.dtpSearchDateFrom.Enabled = true;
                    this.form.dtpSearchDateTo.Enabled = true;
                    //※全選択/未選択チェックボックス
                    this.form.checkBoxAll.Visible = false;
                    this.form.checkBoxAllSaiShime.Visible = false;
                    //伝票締め画面遷移ボタンを非表示
                    parentForm.bt_func2.Enabled = false;
                    parentForm.bt_func2.Text = string.Empty;
                    parentForm.bt_func9.Enabled = true;
                    // 合計金額初期化(表示)
                    this.form.pnlGoukei.Visible = true;
                    //this.form.lblGoukeikingaku.Visible = true;
                    //this.form.txtGoukeikingaku.Visible = true;
                    this.form.txtGoukeikingaku.Text = "0";
                    //this.form.lb_goukeikingaku_shukkin.Visible = true;
                    //this.form.tb_goukeikingaku_shukkin.Visible = true;
                    this.form.tb_goukeikingaku_shukkin.Text = "0";
                    /* 160017 DELETE
                    //出金合計金額位置を調整
                    int x1 = this.form.customDataGridView1.Location.X + this.form.customDataGridView1.Width - this.form.lb_goukeikingaku_shukkin.Width - this.form.tb_goukeikingaku_shukkin.Width - 5;
                    int y1 = this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Height + 5;
                    this.form.lb_goukeikingaku_shukkin.Location = new System.Drawing.Point(x1, y1);

                    int x2 = this.form.lb_goukeikingaku_shukkin.Location.X + this.form.lb_goukeikingaku_shukkin.Width + 5;
                    int y2 = y1;
                    this.form.tb_goukeikingaku_shukkin.Location = new System.Drawing.Point(x2, y2);

                    //売上合計金額位置を調整
                    x1 = this.form.lb_goukeikingaku_shukkin.Location.X - this.form.lblGoukeikingaku.Width - this.form.txtGoukeikingaku.Width - 10;
                    y1 = this.form.lb_goukeikingaku_shukkin.Location.Y;
                    this.form.lblGoukeikingaku.Location = new System.Drawing.Point(x1, y1);

                    x2 = this.form.lblGoukeikingaku.Location.X + this.form.lblGoukeikingaku.Width + 5;
                    y2 = y1;
                    this.form.txtGoukeikingaku.Location = new System.Drawing.Point(x2, y2);*/

                    this.form.HYOUJI_JOUKEN_KBN.Text = "3";
                    this.form.pnlHYOUJI_JOUKEN_KBN.Enabled = false;

                    //160017 S
                    //締日 & 取引先
                    int y = this.form.txtKyotenCd.Location.Y + this.form.txtKyotenCd.Height + 2;
                    int tabIndex = this.form.txtKyotenCd.TabIndex + 1;
                    this.form.pnlTorihikisaki.Location = new System.Drawing.Point(this.form.pnlTorihikisaki.Location.X, y);
                    this.form.pnlTorihikisaki.TabIndex = tabIndex;

                    y += this.form.pnlTorihikisaki.Height + 1;
                    tabIndex++;
                    //QN_QUAN add 20220506 #162927 S
                    //this.form.pnlShimebi.Location = new System.Drawing.Point(this.form.pnlShimebi.Location.X, y);
                    //this.form.pnlShimebi.TabIndex = tabIndex;
                    this.form.pnlShimebi.Visible = false;
                    this.form.pnlseikyu_shimebi.Location = new System.Drawing.Point(this.form.pnlseikyu_shimebi.Location.X, y);
                    this.form.pnlseikyu_shimebi.TabIndex = tabIndex;
                    //QN_QUAN add 20220506 #162927 E
                    //入金予定日
                    this.form.lblSHUKKIN_YOUTEI_BI.Visible = true;
                    this.form.SHUKKIN_YOUTEI_BI.Visible = true;
                    this.form.SHUKKIN_YOUTEI_BI.Text = string.Empty;
                    //バーコード
                    this.headerForm.lblBARCODE_KBN.Visible = true;
                    this.headerForm.pnlBARCODE_KBN.Visible = true;
                    this.form.lblBARCODE_NUMBER.Visible = true;
                    this.form.BARCODE_NUMBER.Visible = true;

                    int heightChanged = this.form.SHUKKIN_YOUTEI_BI.Height + this.form.BARCODE_NUMBER.Height + this.form.pnlGoukei.Height;
                    int gridHeight = this.form.customDataGridView1.Height;
                    gridHeight -= heightChanged;
                    this.form.customDataGridView1.Height = gridHeight;
                    this.form.customDataGridView1.Location = new System.Drawing.Point(this.form.customDataGridView1.Location.X, this.form.customDataGridView1.Location.Y + (this.form.SHUKKIN_YOUTEI_BI.Height + this.form.BARCODE_NUMBER.Height + 2));
                    this.BARCODE_KBN_TextChanged(this.headerForm.BARCODE_KBN, new EventArgs());
                    this.headerForm.BARCODE_KBN.Focus();
                    //160017 E
                }
                else
                {
                    //期間締め画面
                    //非表示に設定(伝票・明細で表示項目)
                    this.form.txtDenpyouShurui.Enabled = true;//160017
                    this.form.lblTorihikisaki.Visible = true;
                    this.form.txtTorihikisakiCd.Visible = true;
                    this.form.txtTorihikisakiName.Visible = true;
                    this.form.btnTorihikisakiPopup.Visible = true;
                    //ラベル「伝票種類※」の名称を「伝票種類」へ変更
                    this.form.lblDenpyouShurui.Text = "伝票種類";
                    //ラベル「検索期間」の名称を「支払締日」へ変更
                    this.form.lblShiharaiShimebi.Text = "支払締日※";
                    // ラベル「取引先※」の名称を「取引先」へ変更
                    this.form.lblTorihikisaki.Text = "取引先";
                    //期間締め画面遷移ボタンを非表示
                    parentForm.bt_func1.Enabled = false;
                    parentForm.bt_func1.Text = string.Empty;
                    //※全選択/未選択チェックボックス
                    parentForm.bt_func2.Enabled = true;
                    this.form.checkBoxAll.Visible = false;
                    this.form.checkBoxAllSaiShime.Visible = false;                    
                    this.form.pnlGoukei.Visible = false;//DAT 2022/04/29 #162924 
                    parentForm.bt_func9.Enabled = true;
                    // 合計金額初期化(非表示)
                    //this.form.lblGoukeikingaku.Visible = false;
                    //this.form.txtGoukeikingaku.Visible = false;
                    this.form.txtGoukeikingaku.Text = "0";
                    //this.form.lb_goukeikingaku_shukkin.Visible = false;
                    //this.form.tb_goukeikingaku_shukkin.Visible = false;
                    this.form.tb_goukeikingaku_shukkin.Text = "0";

                    this.form.HYOUJI_JOUKEN_KBN.Text = "1";
                    this.form.pnlHYOUJI_JOUKEN_KBN.Enabled = true;

                    //160017 S
                    //[F8]検索
                    this.parentForm.bt_func8.Enabled = true;
                    //締日 & 取引先
                    int y = this.form.txtKyotenCd.Location.Y + this.form.txtKyotenCd.Height + 2;
                    int tabIndex = this.form.txtKyotenCd.TabIndex + 1;
                    this.form.pnlShimebi.Location = new System.Drawing.Point(this.form.pnlShimebi.Location.X, y);
                    this.form.pnlShimebi.TabIndex = tabIndex;

                    y += this.form.pnlShimebi.Height + 1;
                    tabIndex++;
                    this.form.pnlTorihikisaki.Location = new System.Drawing.Point(this.form.pnlTorihikisaki.Location.X, y);
                    this.form.pnlTorihikisaki.TabIndex = tabIndex;
                    //QN_QUAN add 20220506 #162927 S
                    y += this.form.pnlTorihikisaki.Height + 1;
                    tabIndex++;
                    this.form.pnlseikyu_shimebi.Location = new System.Drawing.Point(this.form.pnlseikyu_shimebi.Location.X, y);
                    this.form.pnlseikyu_shimebi.TabIndex = tabIndex;
                    this.form.pnlShimebi.Visible = true;
                    //QN_QUAN add 20220506 #162927 E
                    //入金予定日
                    this.form.lblSHUKKIN_YOUTEI_BI.Visible = false;
                    this.form.SHUKKIN_YOUTEI_BI.Visible = false;
                    //バーコード
                    this.headerForm.lblBARCODE_KBN.Visible = false;
                    this.headerForm.pnlBARCODE_KBN.Visible = false;
                    this.form.lblBARCODE_NUMBER.Visible = false;
                    this.form.BARCODE_NUMBER.Visible = false;

                    int heightChanged = this.form.lblSHUKKIN_YOUTEI_BI.Height + this.form.BARCODE_NUMBER.Height + this.form.pnlGoukei.Height;
                    int gridHeight = this.form.customDataGridView1.Height;
                    gridHeight += heightChanged;
                    this.form.customDataGridView1.Height = gridHeight;
                    this.form.customDataGridView1.Location = new System.Drawing.Point(this.form.customDataGridView1.Location.X, this.form.customDataGridView1.Location.Y - (this.form.lblSHUKKIN_YOUTEI_BI.Height + this.form.BARCODE_NUMBER.Height + 2));
                    //160017 E
                }
                this.TorihikisakiSearchConditionsShimei();//QN_QUAN add 20220509 #162927
                #endregion

                #endregion

                #region 初期化

                //ラジオボタン初期設定
                this.form.txtDenpyouShurui.Text = "1";

                //適格請求書対応　
                if (string.IsNullOrEmpty(this.headerForm.INVOICE_KBN.Text))
                {
                    this.headerForm.INVOICE_KBN.Text = "1";
                }
                //旧請求書印刷「1.する」「2.しない」
                if (this.OldShiharaiPrintKBN == "1")
                {
                    this.headerForm.lblINVOICE_KBN.Visible = true;
                    this.headerForm.pnlINVOICE_KBN.Visible = true;
                }
                else
                {
                    this.headerForm.lblINVOICE_KBN.Visible = false;
                    this.headerForm.pnlINVOICE_KBN.Visible = false;
                }
                #endregion

                // ボタンのテキストを初期化
                this.ButtonInit();

                if (firstFlag == false)
                {
                    firstFlag = true;
                    // イベントの初期化処理
                    this.EventInit();
                }
                //160017 S
                //バーコード区分
                string barcodeKbn = Properties.Settings.Default.BARCODE_KBN;
                if (string.IsNullOrEmpty(barcodeKbn))
                {
                    //2.オフ
                    barcodeKbn = "2";
                }
                if (string.IsNullOrEmpty(this.headerForm.BARCODE_KBN.Text))
                {
                    this.headerForm.BARCODE_KBN.Text = barcodeKbn;
                }
                this.BARCODE_KBN_TextChanged(this.headerForm.BARCODE_KBN, new EventArgs());
                //160017 E
                /*本来伝種などの固定値を見たいが、画面のデザイナで固定文言がいるので保留*/
                // 伝票種類名設定
                //setDenpyouNameFromDenshu();

                // 拠点CDが空もしくは99(全社)の場合は、拠点を取引先CDポップアップの条件から外す
                var kyoten = this.form.txtKyotenCd.Text;
                this.TorihikisakiSearchConditionsKyoten(string.IsNullOrEmpty(kyoten) || kyoten == "99");

                // 表示の初期化
                CreateDataGridView(); 
                
                this.SaveDicPrevValue();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 辞書を初期化する 
        /// </summary>
        private void SaveDicPrevValue()
        {
            foreach (var ctr in this.arrInputControl)
            {
                this.SaveDicPrevValue(ctr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        private void SaveDicPrevValue(Control ctr)
        {
            if (!dicPrevValue.ContainsKey(ctr))
                dicPrevValue.Add(ctr, ctr.Text);
            else
                dicPrevValue[ctr] = ctr.Text;
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
            var parentForm = (BusinessBaseForm)this.form.Parent;
            //160013 S
            this.parentForm.bt_process6.Visible = false;
            if (this.isInxsShiharai)
            {
                this.parentForm.ProcessButtonPanel.Size = new System.Drawing.Size(this.parentForm.ProcessButtonPanel.Width, 211);
                this.parentForm.ProcessButtonPanel.Location = new System.Drawing.Point(this.parentForm.ProcessButtonPanel.Location.X, 494);
                this.parentForm.lb_process.Location = new System.Drawing.Point(3, 190);
                this.parentForm.txb_process.Location = new System.Drawing.Point(110, 189);
                this.parentForm.bt_process6.Visible = true;
            }
            this.parentForm.ProcessButtonPanel.Controls.Add(this.parentForm.bt_process6);
            //160013 E
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            // 締処理中テーブルに支払締め処理中のデータがあればF10を表示
            var entityArray = this.shimeshorichuuDao.GetAllData();
            parentForm.bt_func10.Enabled = (!entityArray.Where(e => e.SHORI_KBN.Value.Equals(2)).ToList().Count.Equals(0));
            parentForm.bt_func10.Text = parentForm.bt_func10.Enabled ? parentForm.bt_func10.Text : string.Empty;
            //160017 S
            if (this.shimeChangeFlag)
            {
                parentForm.bt_func5.Enabled = true;
                parentForm.bt_process4.Enabled = true;
                parentForm.bt_process5.Enabled = true;
            }
            else
            {
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_process4.Enabled = false;
                parentForm.bt_process5.Enabled = false;
            }            
            ////INXS支払明細書発行buttonを追加 refs #158003
            //if (!this.isInxsShiharai)
            //{
            //    parentForm.bt_process4.Enabled = false;
            //    parentForm.bt_process4.Text = string.Empty;
            //}
            //else
            //{
            //    parentForm.bt_process4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //}
            //160017 E

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            //Functionボタンのイベント生成
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);       //画面遷移[期間⇒伝票][期間⇒明細]
            parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);       //画面遷移[伝票⇒期間][明細⇒期間]
            parentForm.bt_func3.Click += new EventHandler(bt_func3_Click);       //前月
            parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);       //翌月
            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);       //ｫｰｶｽ設定//160017
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);       //検索
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);       //実行
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);     //ﾛｯｸ解除
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);     //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click); //請求チェックボタン
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click); //請求書発行ボタン // No.1712
            parentForm.bt_process3.Click += new EventHandler(bt_process3_Click); //請求一覧ボタン  // No.1712
            //160017 S
            parentForm.bt_process4.Click += new EventHandler(bt_process4_Click); //[4]実行⇒現金入金
            parentForm.bt_process5.Click += new EventHandler(bt_process5_Click); //[5]実行⇒振込入金
            if (this.isInxsShiharai)
            {
                parentForm.bt_process6.Click += new EventHandler(bt_process4_Click);//INXS支払明細書発行ボタン refs #158003
            }
            //160017
            ////伝票種類の変更イベント
            //this.form.txtDenpyouShurui.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
            ////伝票種類ラジオボタンのイベント
            //this.form.rdoAll.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
            //this.form.rdoShukka.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
            //this.form.rdoUkeire.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
            //this.form.rdoUriageShiharai.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
            ////拠点コード入力欄でロストフォーカス
            //this.form.txtKyotenCd.Leave += new EventHandler(txtKyotenCd_LostFocus);
            ////拠点コード変更イベント
            //this.form.txtKyotenCd.TextChanged += new EventHandler(txtKyotenCd_TextChanged);
            ////締日のフォーカス取得イベント
            //this.form.cmbShimebi.Enter += new EventHandler(cmbShimebi_Enter);
            ////締日変更イベント
            //this.form.cmbShimebi.Validated += new EventHandler(cmbShimebi_Validated);
            ////取引先コード入力欄でロストフォーカス
            //this.form.txtTorihikisakiCd.Leave += new EventHandler(txtTorihikisakiCd_Leave);
            ////請求締日の変更時イベント
            //this.form.dtpSearchDateFrom.Validated += new EventHandler(dtpSearchDateFrom_TextChanged);
            //this.form.dtpSearchDateTo.Validated += new EventHandler(dtpSearchDateTo_TextChanged);

            // 20141201 Houkakou 「支払締処理」のダブルクリックを追加する start
            // 「To」のイベント生成
            this.form.dtpSearchDateTo.MouseDoubleClick += new MouseEventHandler(dtpSearchDateTo_MouseDoubleClick);
            // 20141201 Houkakou 「支払締処理」のダブルクリックを追加する end

            //VAN 20210502 #148578 S
            this.form.customDataGridView1.RowsRemoved += new DataGridViewRowsRemovedEventHandler(customDataGridView1_RowsRemoved);
            //VAN 20210502 #148578 E
            foreach (var ctr in this.arrInputControl)
            {
                ctr.Enter += new EventHandler(Control_Enter);
                ctr.Validated += new EventHandler(Control_Validated);
            }
            //伝票種類のラジオボタンイベント
            this.form.rdoAll.CheckedChanged += new EventHandler(rb_DenpyouShurui_CheckedChanged);
            this.form.rdoUkeire.CheckedChanged += new EventHandler(rb_DenpyouShurui_CheckedChanged);
            this.form.rdoShukka.CheckedChanged += new EventHandler(rb_DenpyouShurui_CheckedChanged);
            this.form.rdoUriageShiharai.CheckedChanged += new EventHandler(rb_DenpyouShurui_CheckedChanged);
            //160017 S
            this.headerForm.BARCODE_KBN.TextChanged += new EventHandler(BARCODE_KBN_TextChanged);
            this.form.BARCODE_NUMBER.Validated += new EventHandler(BARCODE_NUMBER_Validated);
            this.parentForm.FormClosing += new FormClosingEventHandler(FormClosing);
            this.form.dtpSearchDateTo.Validating += new System.ComponentModel.CancelEventHandler(dt_seikyushimebi2_Validating);   
            //160017 E
            this.headerForm.INVOICE_KBN.TextChanged += new EventHandler(INVOICE_KBN_CheckedChanged);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 伝票種類名の取得・設定

        /// <summary>
        /// 伝票種類名称を伝種区分マスタの値で設定します
        /// </summary>
        private void SetDenpyouNameFromDenshu()
        {
            // 受入
            this.denshuName_Ukeire = GetDenshuName((int)DENSHU_KBN.UKEIRE);
            // 出荷
            this.denshuName_Shukka = GetDenshuName((int)DENSHU_KBN.SHUKKA);
            // 売上/支払
            this.denshuName_UrSh = GetDenshuName((int)DENSHU_KBN.URIAGE_SHIHARAI);
            // 出金
            this.denshuName_Shukkin = GetDenshuName((int)DENSHU_KBN.SHUKKIN);
        }

        /// <summary>
        /// 伝種区分マスタより伝種区分名を取得する
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns>伝種区分名</returns>
        private string GetDenshuName(int denshuKbnCd)
        {
            LogUtility.DebugMethodStart(denshuKbnCd);

            M_DENSHU_KBN denshuKbnEntity = new M_DENSHU_KBN();
            if (denshuKbnCd != null)
            {
                denshuKbnEntity = this.mdenshuKbnDao.GetDataByCd(denshuKbnCd.ToString());
            }

            LogUtility.DebugMethodEnd(denshuKbnCd);
            return denshuKbnEntity.DENSHU_KBN_NAME;
        }

        #endregion 伝票種類名の取得・設定

        #region 画面遷移制御([伝票⇒期間][明細⇒期間]の場合のみ)

        /// <summary>
        /// 画面遷移制御([伝票⇒期間][明細⇒期間]の場合のみ)
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);


            //画面遷移フラグをfalse(期間締め画面)に設定
            this.shimeChangeFlag = false;

            //※明細が表示されていると画面切替え時に
            //ダイアログが表示されてしまうので明細部をクリア
            int k = this.form.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
            }

            //表示エリアのクリア
            this.dispResult = null;
            this.form.txtTorihikisakiCd.Text = string.Empty;
            this.form.txtTorihikisakiName.Text = string.Empty;

            //画面の表示切替え
            this.SetHeaderTitle();
            this.SetShimebi();
            if (!WindowInit())
            {
                return;
            }
            CreateDataGridView();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 画面遷移制御([期間⇒伝票][期間⇒明細]の場合のみ)

        /// <summary>
        /// 画面遷移の制御をする[期間⇒伝票][期間⇒明細]の場合のみ
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //※明細が表示されていると画面切替え時に
            //ダイアログが表示されてしまうので明細部をクリア
            int k = this.form.customDataGridView1.Rows.Count;
            for (int i = k; i >= 1; i--)
            {
                this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
            }

            //画面遷移フラグをtrue(伝票or明細締め画面)に設定
            this.shimeChangeFlag = true;

            //表示エリアのクリア
            this.dispResult = null;
            this.form.txtTorihikisakiCd.Text = string.Empty;
            this.form.txtTorihikisakiName.Text = string.Empty;

            //画面の表示切替え
            this.SetHeaderTitle();
            this.SetShimebi();
            if (!WindowInit())
            {
                return;
            }
            CreateDataGridView();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 請求締日を前月に設定

        /// <summary>
        /// 請求締日を前月に設定
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            
            this.SaveDicPrevValue();
            if (!this.shimeChangeFlag)
            {
                //前月
                if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    //160017 S
                    //請求締日1
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                    {
                        DateTime nowDate = (DateTime)this.form.dtpSearchDateFrom.Value;
                        if (SqlDateTime.MinValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, 1)) < 0)
                        {
                            nowDate = nowDate.AddMonths(-1);
                            this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                        }
                    }
                    //請求締日2
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                    {
                        DateTime nowDate = (DateTime)this.form.dtpSearchDateTo.Value;
                        if (SqlDateTime.MinValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, 1)) < 0)
                        {
                            nowDate = nowDate.AddMonths(-1);
                            this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                        }
                    }
                    //160017 E
                    //編集可能に設定
                    this.form.dtpSearchDateFrom.Enabled = true;
                    this.form.dtpSearchDateTo.Enabled = true;
                }
                else if ("31".Equals(this.form.cmbShimebi.Text))
                {
                    //請求締日1
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(-1);
                    this.form.dtpSearchDateFrom.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                    //編集不可に設定
                    this.form.dtpSearchDateFrom.Enabled = false;
                }
                else
                {
                    //請求締日1
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(-1);
                    this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                    //編集不可に設定
                    this.form.dtpSearchDateFrom.Enabled = false;
                }
            }
            else
            {
                /* DELETE BY 160017
                //請求締日1で選択されている月の月末日を計算
                DateTime sentakuDate1 = DateTime.Parse(this.form.dtpSearchDateFrom.Text);
                DateTime getsumatsuDate1 = new DateTime(sentakuDate1.Year, sentakuDate1.Month, DateTime.DaysInMonth(sentakuDate1.Year, sentakuDate1.Month));

                //請求締日2で選択されている月の月末日を計算
                DateTime sentakuDate2 = DateTime.Parse(this.form.dtpSearchDateTo.Text);
                DateTime getsumatsuDate2 = new DateTime(sentakuDate2.Year, sentakuDate2.Month, DateTime.DaysInMonth(sentakuDate2.Year, sentakuDate2.Month));

                //請求締日1の月末判定
                if (sentakuDate1 == getsumatsuDate1)
                {
                    //月末の場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(-1);
                    this.form.dtpSearchDateFrom.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                }
                else
                {
                    //月末でない場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(-1);
                    this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                }

                //請求締日2の月末判定
                if (sentakuDate2 == getsumatsuDate2)
                {
                    //月末の場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).AddMonths(-1);
                    this.form.dtpSearchDateTo.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                }
                else
                {
                    //月末でない場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).AddMonths(-1);
                    this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                }

                //編集可能に設定
                this.form.dtpSearchDateFrom.Enabled = true;
                this.form.dtpSearchDateTo.Enabled = true;*/

                ////チェックされたレコードがある場合検索条件が変更されていないかチェック
                //if (0 < CheckDGVCheckCount())
                //{
                //    CheckSearchCondition(this.form.dtpSearchDateFrom.Name);
                //}
                //else
                //{
                //    CheckGridClear(this.form.dtpSearchDateFrom.Name);
                //}
                //160017 S
                //請求締日1
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                {
                    DateTime nowDate = (DateTime)this.form.dtpSearchDateFrom.Value;
                    if (SqlDateTime.MinValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, 1)) < 0)
                    {
                        nowDate = nowDate.AddMonths(-1);
                        this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                    }
                }
                //請求締日2
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                {
                    DateTime nowDate = (DateTime)this.form.dtpSearchDateTo.Value;
                    if (SqlDateTime.MinValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, 1)) < 0)
                    {
                        nowDate = nowDate.AddMonths(-1);
                        this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                    }
                }
                //160017 E
            }
            //160017 S
            //if (this.IsValueChanged(this.form.dtpSearchDateFrom)
            //    && this.form.customDataGridView1.Rows.Count > 0)
            //{
            //    var confirm = this.ConfirmValueChanged(this.form.dtpSearchDateFrom);
            //    if (confirm)
            //    {
            //        this.SaveDicPrevValue();
            //    }
            //}
            bool setShukkinYouteiFlg = false;
            bool confirmFlg = true;
            if (this.IsValueChanged(this.form.dtpSearchDateTo))
            {
                setShukkinYouteiFlg = true;
            }
            if ((this.IsValueChanged(this.form.dtpSearchDateFrom) || this.IsValueChanged(this.form.dtpSearchDateTo))
                && this.form.customDataGridView1.Rows.Count > 0)
            {
                confirmFlg = this.ConfirmValueChanged(this.form.dtpSearchDateFrom);
                if (confirmFlg)
                {
                    this.SaveDicPrevValue();
                }
            }
            if (setShukkinYouteiFlg && confirmFlg)
            {
                this.SetShukkinYouteibi();
            }
            //160017 E
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 請求締日を翌月に設定

        /// <summary>
        /// 請求締日を翌月に設定
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            
            this.SaveDicPrevValue();
            if (!this.shimeChangeFlag)
            {
                //翌月
                if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    //160017 S
                    //請求締日1
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                    {
                        DateTime nowDate = (DateTime)this.form.dtpSearchDateFrom.Value;
                        if (SqlDateTime.MaxValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month))) > 0)
                        {
                            nowDate = nowDate.AddMonths(1);
                            this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                        }
                    }
                    //請求締日2
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                    {
                        DateTime nowDate = (DateTime)this.form.dtpSearchDateTo.Value;
                        if (SqlDateTime.MaxValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month))) > 0)
                        {
                            nowDate = nowDate.AddMonths(1);
                            this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                        }
                    }
                    //160017 E
                }
                else if ("31".Equals(this.form.cmbShimebi.Text))
                {
                    //請求締日1
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(1);
                    this.form.dtpSearchDateFrom.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                }
                else
                {
                    //請求締日1
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(1);
                    this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                }
            }
            else
            {
                /* DELETE BY 160017
                //請求締日1で選択されている月の月末日を計算
                DateTime sentakuDate1 = DateTime.Parse(this.form.dtpSearchDateFrom.Text);
                DateTime getsumatsuDate1 = new DateTime(sentakuDate1.Year, sentakuDate1.Month, DateTime.DaysInMonth(sentakuDate1.Year, sentakuDate1.Month));

                //請求締日2で選択されている月の月末日を計算
                DateTime sentakuDate2 = DateTime.Parse(this.form.dtpSearchDateTo.Text);
                DateTime getsumatsuDate2 = new DateTime(sentakuDate2.Year, sentakuDate2.Month, DateTime.DaysInMonth(sentakuDate2.Year, sentakuDate2.Month));

                //請求締日1の月末判定
                if (sentakuDate1 == getsumatsuDate1)
                {
                    //月末の場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(1);
                    this.form.dtpSearchDateFrom.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                }
                else
                {
                    //月末でない場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).AddMonths(1);
                    this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                }

                //請求締日2の月末判定
                if (sentakuDate2 == getsumatsuDate2)
                {
                    //月末の場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).AddMonths(1);
                    this.form.dtpSearchDateTo.Text = new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month)).ToString("yyyy/MM/dd");
                }
                else
                {
                    //月末でない場合
                    DateTime nowDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).AddMonths(1);
                    this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                }*/

                ////チェックされたレコードがある場合検索条件が変更されていないかチェック
                //if (0 < CheckDGVCheckCount())
                //{
                //    CheckSearchCondition(this.form.dtpSearchDateFrom.Name);
                //}
                //else
                //{
                //    CheckGridClear(this.form.dtpSearchDateFrom.Name);
                //}
                //160017 S
                //請求締日1
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                {
                    DateTime nowDate = (DateTime)this.form.dtpSearchDateFrom.Value;
                    if (SqlDateTime.MaxValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month))) > 0)
                    {
                        nowDate = nowDate.AddMonths(1);
                        this.form.dtpSearchDateFrom.Text = nowDate.ToString("yyyy/MM/dd");
                    }
                }
                //請求締日2
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                {
                    DateTime nowDate = (DateTime)this.form.dtpSearchDateTo.Value;
                    if (SqlDateTime.MaxValue.Value.Date.CompareTo(new DateTime(nowDate.Year, nowDate.Month, DateTime.DaysInMonth(nowDate.Year, nowDate.Month))) > 0)
                    {
                        nowDate = nowDate.AddMonths(1);
                        this.form.dtpSearchDateTo.Text = nowDate.ToString("yyyy/MM/dd");
                    }
                }
                //160017 E
            }
            //160017 S
            //if (this.IsValueChanged(this.form.dtpSearchDateFrom) 
            //    && this.form.customDataGridView1.Rows.Count > 0)
            //{
            //    var confirm = this.ConfirmValueChanged(this.form.dtpSearchDateFrom);
            //    if (confirm)
            //    {
            //        this.SaveDicPrevValue();
            //    }
            //}
            bool setShukkinYouteiFlg = false;
            bool confirmFlg = true;
            if (this.IsValueChanged(this.form.dtpSearchDateTo))
            {
                setShukkinYouteiFlg = true;
            }
            if ((this.IsValueChanged(this.form.dtpSearchDateFrom) || this.IsValueChanged(this.form.dtpSearchDateTo))
                && this.form.customDataGridView1.Rows.Count > 0)
            {
                confirmFlg = this.ConfirmValueChanged(this.form.dtpSearchDateFrom);
                if (confirmFlg)
                {
                    this.SaveDicPrevValue();
                }
            }
            if (setShukkinYouteiFlg && confirmFlg)
            {
                this.SetShukkinYouteibi();
            }
            //160017 E
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region [F8]押下での検索処理実行イベント

        /// <summary>
        /// [F8]押下での検索処理の実行
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 取引先と拠点の関係をチェック
            if (!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text))
            {
                if (!this.CheckTorihikisakiKyoten())
                {
                    this.form.txtTorihikisakiCd.Focus();
                    this.form.txtTorihikisakiName.Text = string.Empty;
                    return;
                }
            }

            bool checkResult = true;

            //未入力チェック処理
            checkResult = CheckInput(sender);

            if (checkResult)
            {
                this.GetMeisaiData();

                int count = 0;

                if (dispResult != null)
                {
                    count = dispResult.Rows.Count;
                }

                if (0 == count)
                {
                    //表示データなし
                    //前の結果をクリア
                    this.form.customDataGridView1.Rows.Clear();

                    if (!bShimeSyoriZikkochu)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
                else
                {
                    CreateDataGridView();
                    this.SaveDicPrevValue();
                    ////検索条件を保存
                    //SaveSearchCondition();
                }

                // 合計金額再計算
                CalcGoukeiKingaku();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 締め処理の実行イベント（※チェック処理も実行）

        /// <summary>
        /// 締め処理の実行
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bShimeSyoriZikkochu = true;

            //適格請求対応
            //(レイアウト区分非表示orレイアウト区分1:適格
            if ((!this.headerForm.INVOICE_KBN.Visible) || (this.headerForm.INVOICE_KBN.Text == "1"))
            {
                OldInvoiceFlag = false;
            }
            else
            {
                OldInvoiceFlag = true;
            }


            //締めチェック結果
            bool chkResult = true;

            //締めチェック処理
            chkResult = this.CheckShimeData(sender);

            //チェック戻り値がtrueの場合、締め処理実行
            if (chkResult)
            {
                //伝票精算締処理⇒出金入力 パラメータ 
                List<string> shukkinPrm = null;//160017

                //他ユーザの処理状況チェック
                chkResult = this.CheckShimeChuu();

                // 他ユーザーが締処理実行中でなければ処理続行
                if (chkResult)
                {
                    ShimeLogicClass ShimeJikkou = new ShimeLogicClass();
                    //メッセージ出力
                    MessageBoxShowLogic msgShimeKakunin = new MessageBoxShowLogic();
                    if (msgShimeKakunin.MessageBoxShow("C049", "支払") == DialogResult.Yes)
                    {
                        //締め実行処理へshimeDataListを渡す
                        //締め実行処理が正常/異常終了をtrue/falseで戻す
                        bool shimeJikkouResult = true;
                        if (OldInvoiceFlag)
                        {
                            shimeJikkouResult = ShimeJikkou.Shime(shimeDataList, 1); //旧請求書用締処理
                        }
                        else
                        {
                            shimeJikkouResult = ShimeJikkou.Shime(shimeDataList, 2); //適格請求書用締処理
                            //shimeJikkouResult = ShimeJikkou.Shime(shimeDataList, 3); //合算請求書用締処理
                        }

                        //締め実行処理が正常終了の場合、メッセージ出力
                        if (shimeJikkouResult)
                        {
                            //160017 S
                            //バーコード読み取りモードでない場合は、もう一度検索してください 
                            if (!this.IsBarcodeReadingMode())
                            {
                                // 再検索を行い、画面の再描画を行う
                                bt_func8_Click(sender, e);
                            }
                            else
                            {
                                this.form.customDataGridView1.Rows.Clear();
                            }
                            //出金入力のパラメータを作成します 
                            if (sender == this.parentForm.bt_process4 || sender == this.parentForm.bt_process5)
                            {
                                shukkinPrm = new List<string>();
                                //取引先CD
                                shukkinPrm.Add(shimeDataList[0].SHIHARAI_CD);
                                //精算日付
                                shukkinPrm.Add(shimeDataList[0].SHIHARAISHIMEBI_TO);
                                //実行⇒現金出金
                                if (sender == this.parentForm.bt_process4)
                                {
                                    //出金区分CD
                                    shukkinPrm.Add("1");
                                }
                                //実行⇒振込入金
                                else
                                {
                                    //出金区分CD
                                    shukkinPrm.Add("2");
                                }
                                shukkinPrm.AddRange(ShimeJikkou.seisanBangoList.Select(number => number.ToString()));
                            }
                            //160017 E
                            MessageBoxShowLogic msgLgc = new MessageBoxShowLogic();
                            msgLgc.MessageBoxShow("I001", "支払締処理");
                        }
                        else
                        {
                            if (ShimeJikkou.DisplayErrorMsg == true)
                            {
                                //締め実行処理で異常終了
                                MessageBoxShowLogic msgLgc = new MessageBoxShowLogic();
                                msgLgc.MessageBoxShow("I007", "支払締処理");
                            }
                        }
                    }

                    // 締処理中テーブル削除
                    if (this.shimeDataList[0].SHIME_TANI == 1)
                    {
                        // 期間締処理の場合は全削除
                        foreach (SeikyuShimeShoriDto dto in this.shimeDataList)
                        {
                            ShimeJikkou.DeleteTShimeShoriChuu(dto, this.shimeDataList[0].SHIME_JIKKOU_NO);
                        }
                    }
                    else
                    {
                        ShimeJikkou.DeleteTShimeShoriChuu(this.shimeDataList[0], this.shimeDataList[0].SHIME_JIKKOU_NO);
                    }
                    //160017 S
                    //【出金入力】画面を起動する。
                    if (shukkinPrm != null)
                    {
                        FormManager.OpenFormWithAuth("G090", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, shukkinPrm);
                    }
                    //160017 E
                }
            }

            bShimeSyoriZikkochu = false;
            
            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region ﾛｯｸ解除イベント

        /// <summary>
        /// 締処理中に強制終了してしまった場合、締処理中フラグをクリア出来るようにする
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            // CLIENT_COMPUTER_NAME、CLIENT_USER_NAME、CREATE_USERが一致した場合T_SHIME_SHORI_CHUUテーブルをクリア
            GetEnvironmentInfoClass environment = new GetEnvironmentInfoClass();
            var nowEnvName = environment.GetComputerAndUserName();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            var user = SystemProperty.UserName;
            string createPc = string.Empty;
            string clientUserName = string.Empty;
            string createUser = string.Empty;
            var entityArray = this.shimeshorichuuDao.GetAllData();

            if (entityArray.Count() <= 0)
            {
                msgLogic.MessageBoxShow("E241", "締処理");
                return;
            }

            // 締処理中テーブルに自分がいるか検索
            foreach (T_SHIME_SHORI_CHUU entity in entityArray)
            {
                if (entity.CLIENT_COMPUTER_NAME.Equals(nowEnvName.Item1)
                    && entity.CLIENT_USER_NAME.Equals(nowEnvName.Item2)
                    && entity.CREATE_USER.Equals(user)
                    && entity.SHORI_KBN.Value == 2)
                {
                    var result = msgLogic.MessageBoxShow("C094", "締処理");
                    if (result == DialogResult.Yes)
                    {
                        // 削除対象のListを作成
                        var deleteEntity = new T_SHIME_SHORI_CHUU()
                        {
                            CLIENT_COMPUTER_NAME = nowEnvName.Item1,
                            CLIENT_USER_NAME = nowEnvName.Item2,
                            CREATE_USER = user,
                            SHORI_KBN = 2
                        };

                        // 締処理中テーブルをDeleteします。
                        using (Transaction tran = new Transaction())
                        {
                            // 削除対象を全てDelete
                            int CntNyuukinIns = this.shimeshorichuuDao.DesignateDelete(deleteEntity);
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I017", "締処理を中止");
                    }
                    this.ButtonInit();
                    return;
                }

                // どのＰＣがロックしているかを取得
                if (!string.IsNullOrEmpty(entity.CLIENT_COMPUTER_NAME)
                    && !string.IsNullOrEmpty(entity.CLIENT_USER_NAME)
                    && !string.IsNullOrEmpty(entity.CREATE_USER)
                    && entity.SHORI_KBN == 2)
                {
                    createPc = entity.CLIENT_COMPUTER_NAME;
                    clientUserName = entity.CLIENT_USER_NAME;
                    createUser = entity.CREATE_USER;
                }
            }

            // 締処理中テーブルにデータは存在するが、自分ではなかった場合
            if (!string.IsNullOrEmpty(createPc) && !string.IsNullOrEmpty(clientUserName) && !string.IsNullOrEmpty(createUser))
            {
                msgLogic.MessageBoxShow("E240", "締処理", createPc, clientUserName, createUser);
            }
            else
            {
                // createPcが取得できずにここまで来た場合はデータは存在しているが、
                // 請求締め処理のデータでは存在していない
                msgLogic.MessageBoxShow("E241", "締処理");
            }
            return;
        }

        #endregion

        #region 締めチェックボタン押下イベント

        /// <summary>
        /// 締めチェック処理を実行する
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G117", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 支払明細書発行ボタン押下イベント

        /// <summary>
        /// 支払明細書発行発行処理を実行する
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            string date = string.Empty;
            string shimebi = string.Empty;

            if (!this.shimeChangeFlag)
            {
                if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    // 期間締め - 0日締め時はTo
                    date = this.form.dtpSearchDateTo.Text;
                }
                else
                {
                    // 期間締め - 0日締め以外はFrom
                    date = this.form.dtpSearchDateFrom.Text;
                }

                // 期間締の場合、画面で選択されている締日を設定
                shimebi = this.form.cmbShimebi.SelectedItem.ToString();
            }
            else
            {
                // 伝票or明細はTo
                date = this.form.dtpSearchDateTo.Text;
            }

            FormManager.OpenFormWithAuth("G116", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.form.txtKyotenCd.Text,
                            shimebi, this.form.txtTorihikisakiCd.Text, date);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 支払明細一覧ボタン押下イベント

        /// <summary>
        /// 支払明細一覧処理を実行する
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_process3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            string date = string.Empty;
            if (!this.shimeChangeFlag)
            {
                if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    // 期間締め - 0日締め時はTo
                    date = this.form.dtpSearchDateTo.Text;
                }
                else
                {
                    // 期間締め - 0日締め以外はFrom
                    date = this.form.dtpSearchDateFrom.Text;
                }
            }
            else
            {
                // 伝票or明細はTo
                date = this.form.dtpSearchDateTo.Text;
            }

            FormManager.OpenFormWithAuth("G112", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.form.txtKyotenCd.Text, this.form.txtTorihikisakiCd.Text, date);

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region 締めチェック処理の実行イベント（チェックのみ）

        /// <summary>
        ///  締めチェック処理を実行する
        /// </summary>
        private bool CheckShimeData(object sender)
        {
            LogUtility.DebugMethodStart(sender);

            bool checkResult = true;
            bool errorIchiranResult = true;

            //未入力チェック処理
            checkResult = CheckInput(sender);
            if (!checkResult)
            {
                LogUtility.DebugMethodEnd();
                return checkResult;
            }
            //160017 S
            //出金予定日チェック処理
            checkResult = this.CheckShukkinYoteiBi();
            if (!checkResult)
            {
                LogUtility.DebugMethodEnd();
                return checkResult;
            }
            //160017 E
            //DGVチェック数チェック処理
            checkResult = CheckDGVCheck();
            if (!checkResult)
            {
                LogUtility.DebugMethodEnd();
                return checkResult;
            }

            #region 適格請求書
            string sql = string.Empty;
            string tourokuno = string.Empty;
            if (!OldInvoiceFlag)
            {
                if (this.shimeChangeFlag)
                {
                    //伝票締
                    sql = string.Format("SELECT TOUROKU_NO FROM M_TORIHIKISAKI_SHIHARAI WHERE TORIHIKISAKI_CD = '{0}'", this.form.txtTorihikisakiCd.Text);
                    DataTable TorihikiTouroku = shimeShoriDao.GetDataForStringSql(sql);
                    if (TorihikiTouroku.Rows.Count != 0)
                    {
                        tourokuno = TorihikiTouroku.Rows[0]["TOUROKU_NO"].ToString();
                        if (String.IsNullOrEmpty(tourokuno))
                        {
                            MessageBox.Show("登録番号が未入力です。\r取引先入力画面より取引先の登録番号の登録を行ってください。\n（取引先CD=" + this.form.txtTorihikisakiCd.Text + "）", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            #endregion 適格請求書

            //チェック対象のデータ抜き出し
            //締め処理へ渡すデータ作成用
            shimeDataList = new List<SeikyuShimeShoriDto>();
            //チェッククラスへ渡すデータ作成用
            checkDataList = new List<CheckDto>();

            //締め処理へ渡すデータを作成
            int rowCount = this.form.customDataGridView1.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                shimeDto = new SeikyuShimeShoriDto();

                //checkboxにチェックされたレコードのみ取得
                if (bool.Parse(this.form.customDataGridView1.Rows[i].Cells[0].Value.ToString()))
                {
                    DataGridViewRow dr = this.form.customDataGridView1.Rows[i];

                    //期間(伝票)締め処理
                    if (this.shimeChangeFlag)
                    {
                        shimeDto.SHIME_TANI = 2;
                    }
                    else
                    {
                        shimeDto.SHIME_TANI = 1;
                    }

                    // 期間締の場合、画面の締日を設定する。
                    if (!this.shimeChangeFlag)
                    {
                        //画面入力情報:締日
                        shimeDto.SHIMEBI = int.Parse(this.form.cmbShimebi.Text);
                    }

                    //画面入力情報:伝票種類
                    shimeDto.DENPYO_SHURUI = int.Parse(this.form.txtDenpyouShurui.Text);

                    //画面入力情報:拠点CD
                    shimeDto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);
                    //160017 S
                    //出金予定日 
                    if (this.shimeChangeFlag && !string.IsNullOrEmpty(this.form.SHUKKIN_YOUTEI_BI.Text))
                    {
                        shimeDto.SHUKKIN_YOTEI_BI = (DateTime)this.form.SHUKKIN_YOUTEI_BI.Value;
                    }
                    //160017 E

                    //画面入力情報:請求締日FROM TOの設定
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text) && this.form.dtpSearchDateTo.Enabled == true)
                    {
                        //FROM/TO両方に入力があれば各々に設定
                        if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))//160017
                        {
                            shimeDto.SHIHARAISHIMEBI_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                        }
                        shimeDto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        shimeDto.SHIHARAISHIMEBI_FROM = null;
                        shimeDto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                    }

                    //明細部表示情報:取引先CD,伝票番号,明細番号の設定
                    if (!this.shimeChangeFlag)
                    {
                        //期間締め画面の場合
                        shimeDto.SHIHARAI_CD = dr.Cells["TORIHIKI_CD"].Value.ToString();
                        #region 適格請求書
                        tourokuno = string.Empty;
                        if (!OldInvoiceFlag)
                        {
                            sql = string.Format("SELECT TOUROKU_NO FROM M_TORIHIKISAKI_SHIHARAI WHERE TORIHIKISAKI_CD = '{0}'", dr.Cells["TORIHIKI_CD"].Value.ToString());
                            DataTable TorihikiTouroku = shimeShoriDao.GetDataForStringSql(sql);
                            if (TorihikiTouroku.Rows.Count != 0)
                            {
                                tourokuno = TorihikiTouroku.Rows[0]["TOUROKU_NO"].ToString();
                                if (String.IsNullOrEmpty(tourokuno))
                                {
                                    MessageBox.Show("登録番号が未入力です。\r取引先入力画面より取引先の登録番号の登録を行ってください。\n（取引先CD=" + dr.Cells["TORIHIKI_CD"].Value.ToString() + "）", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return false;
                                }
                            }
                        }
                        shimeDto.TOUROKU_NO = tourokuno;
                        #endregion 適格請求書
                        if (dr.Cells["CHECK_BOX_SAISHIME"].Value != null)
                        {
                            shimeDto.SAISHIME_FLG = (bool)dr.Cells["CHECK_BOX_SAISHIME"].Value;
                            if (shimeDto.SAISHIME_FLG)
                            {
                                shimeDto.SAISHIME_NUMBER_LIST = new List<Int64>();
                                SeikyuShimeShoriDto saishimeSearchDto = new SeikyuShimeShoriDto();
                                saishimeSearchDto.SHIHARAI_CD = shimeDto.SHIHARAI_CD;
                                if (shimeDto.SHIMEBI == 0)
                                {
                                    saishimeSearchDto.SHIHARAISHIMEBI_FROM = shimeDto.SHIHARAISHIMEBI_FROM;
                                    saishimeSearchDto.SHIHARAISHIMEBI_TO = shimeDto.SHIHARAISHIMEBI_TO;
                                }
                                else if (shimeDto.SHIMEBI == 31)
                                {
                                    DateTime seisanDate = DateTime.Parse(shimeDto.SHIHARAISHIMEBI_TO);
                                    saishimeSearchDto.SHIHARAISHIMEBI_TO = seisanDate.ToString("yyyy/MM/dd");
                                    saishimeSearchDto.SHIHARAISHIMEBI_FROM = new DateTime(seisanDate.Year, seisanDate.Month, 1).ToString("yyyy/MM/dd");
                                }
                                else
                                {
                                    DateTime seisanDate = DateTime.Parse(shimeDto.SHIHARAISHIMEBI_TO);
                                    saishimeSearchDto.SHIHARAISHIMEBI_TO = seisanDate.ToString("yyyy/MM/dd");
                                    saishimeSearchDto.SHIHARAISHIMEBI_FROM = seisanDate.AddMonths(-1).AddDays(1).ToString("yyyy/MM/dd");
                                }
                                DataTable seisanSaishimeTable = shimeShoriDao.GetSeisanSaishimeData(saishimeSearchDto);

                                foreach (DataRow seisanSaishimeRow in seisanSaishimeTable.Rows)
                                {
                                    Int64 seikyuuSaishimeiBango = Convert.ToInt64(seisanSaishimeRow["SEISAN_NUMBER"]);
                                    shimeDto.SAISHIME_NUMBER_LIST.Add(seikyuuSaishimeiBango);
                                }
                                dr.Cells["SAISHIME_NUMBER_LIST"].Value = shimeDto.SAISHIME_NUMBER_LIST;
                            }
                        }
                    }
                    else
                    {
                        //伝票締め画面の場合
                        shimeDto.SHIHARAI_CD = this.form.txtTorihikisakiCd.Text;
                        #region 適格請求書
                        shimeDto.TOUROKU_NO = tourokuno;
                        #endregion 適格請求書
                        shimeDto.DENPYOU_NUMBER = long.Parse(dr.Cells["DENPYOU_NUMBER"].Value.ToString());

                        if (denshuName_Ukeire.Equals(dr.Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 受入
                            shimeDto.DATA_SHURUI = 2;
                        }
                        else if (denshuName_Shukka.Equals(dr.Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 出荷
                            shimeDto.DATA_SHURUI = 3;
                        }
                        else if (denshuName_UrSh.Equals(dr.Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 売上/支払
                            shimeDto.DATA_SHURUI = 4;
                        }
                        //VAN 20210502 #148578 S
                        else if (denshuName_Shukkin.Equals(dr.Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 出金
                            shimeDto.DATA_SHURUI = Shougun.Core.Common.BusinessCommon.Const.CommonConst.DENSHU_KBN_SHUKKINN;
                        }
                        //VAN 20210502 #148578 E
                    }

                    //売上データ取得回避フラグOFF
                    shimeDto.KAIHI_FLG = false;

                    //締日によってチェック振り分け
                    int recordCount = 0;

                    //未来日の請求日付データがないかチェック
                    recordCount = shimeShoriDao.CheckDateSelectedZenshaForEntity(shimeDto);
                    if (0 != recordCount)
                    {
                        //データがあればエラーのため処理中止
                        checkResult = false;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E047", "取引先", "精算");
                        break;
                    }

                    //締め処理実行用データ追加
                    shimeDataList.Add(shimeDto);
                }
            }


            //チェッククラスへ渡すデータを作成
            if (!this.shimeChangeFlag)
            {
                //期間締め画面の場合
                for (int i = 0; i < rowCount; i++)
                {
                    chkDto = new CheckDto();

                    //checkboxにチェックされたレコードのみ取得
                    if (bool.Parse(this.form.customDataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        DataGridViewRow dr = this.form.customDataGridView1.Rows[i];

                        //売上・支払区分固定/2:支払)
                        chkDto.URIAGE_SHIHARAI_KBN = 2;

                        //使用画面(固定/1:締め処理画面)
                        chkDto.SHIYOU_GAMEN = 1;

                        //締め単位
                        chkDto.SHIME_TANI = 1;

                        //画面入力情報:伝票種類
                        chkDto.DENPYOU_SHURUI = int.Parse(this.form.txtDenpyouShurui.Text);

                        //画面入力情報:拠点CD
                        chkDto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);

                        //画面入力情報:請求締日FROM TOの設定
                        if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text) && this.form.dtpSearchDateTo.Enabled == true)
                        {
                            //FROM/TO両方に入力があれば各々に設定
                            if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))//160017
                            {
                                chkDto.KIKAN_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                            }
                            chkDto.KIKAN_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            chkDto.KIKAN_FROM = null;
                            chkDto.KIKAN_TO = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                        }

                        //明細部表示情報:取引先CDの設定
                        chkDto.TORIHIKISAKI_CD = dr.Cells["TORIHIKI_CD"].Value.ToString();

                        if (dr.Cells["CHECK_BOX_SAISHIME"].Value != null)
                        {
                            chkDto.SAISHIME_FLG = (bool)dr.Cells["CHECK_BOX_SAISHIME"].Value;
                            if (chkDto.SAISHIME_FLG && dr.Cells["SAISHIME_NUMBER_LIST"].Value != null && dr.Cells["SAISHIME_NUMBER_LIST"].Value is List<Int64>)
                            {
                                chkDto.SAISHIME_NUMBER_LIST = dr.Cells["SAISHIME_NUMBER_LIST"].Value as List<Int64>;
                            }
                        }

                        //チェック用データ追加
                        checkDataList.Add(chkDto);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    //checkboxにチェックされたレコードのみ取得
                    if (bool.Parse(this.form.customDataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        chkDto = new CheckDto();

                        //売上・支払区分固定/2:支払)
                        chkDto.URIAGE_SHIHARAI_KBN = 2;

                        //使用画面(固定/1:締め処理画面)
                        chkDto.SHIYOU_GAMEN = 1;

                        //画面入力情報:伝票種類
                        chkDto.DENPYOU_SHURUI = int.Parse(this.form.txtDenpyouShurui.Text);

                        //画面入力情報:拠点CD
                        chkDto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);

                        //画面入力情報:支払締日FROM TOの設定
                        if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text) && this.form.dtpSearchDateTo.Enabled == true)
                        {
                            //FROM/TO両方に入力があれば各々に設定
                            if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))//160017
                            {
                                chkDto.KIKAN_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                            }
                            chkDto.KIKAN_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            chkDto.KIKAN_FROM = null;
                            chkDto.KIKAN_TO = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                        }

                        //画面入力情報：取引先CD
                        chkDto.TORIHIKISAKI_CD = this.form.txtTorihikisakiCd.Text;

                        //伝票締め画面の場合
                        chkDto.SHIME_TANI = 2;

                        // 伝票種類
                        if (denshuName_Ukeire.Equals(this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 受入
                            chkDto.DENPYOU_TYPE = 1;
                        }
                        else if (denshuName_Shukka.Equals(this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 出荷
                            chkDto.DENPYOU_TYPE = 2;
                        }
                        else if (denshuName_UrSh.Equals(this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 売上/支払
                            chkDto.DENPYOU_TYPE = 3;
                        }
                        //VAN 20210429 #148578 S
                        else if (denshuName_Shukkin.Equals(this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value.ToString()))
                        {
                            // 売上/支払
                            chkDto.DENPYOU_TYPE = Shougun.Core.Common.BusinessCommon.Const.CommonConst.DENSHU_KBN_SHUKKINN;
                        }
                        //VAN 20210429 #148578 E
                        else
                        {
                            // 想定外
                        }

                        //伝票番号
                        chkDto.DENPYOU_NUMBER = Convert.ToInt64(this.form.customDataGridView1.Rows[i].Cells["DENPYOU_NUMBER"].Value.ToString());

                        //チェック用データ追加
                        checkDataList.Add(chkDto);
                    }
                }
            }

            //適格請求書用チェック(伝票締のチェックは、必須
            if ((!OldInvoiceFlag) && (this.shimeChangeFlag))
            {
                ShimeCheckLogic checkLogic2 = new ShimeCheckLogic();
                DataTable resultTable2 = checkLogic2.checkShimeData_invoice(checkDataList);
                if (resultTable2.Rows.Count != 0)
                {
                    //伝票締は、適格請求書用のチェックで引っかかったら締処理を抜ける抜ける

                    MessageBox.Show(string.Format("適格請求書様式の支払明細書では、明細毎の消費税計算が行えません。\r伝票の修正を行ってください。\r(伝票種類：{0}、伝票番号：{1}、伝票日付：{2})"
                                , resultTable2.Rows[0]["ERROR_NAIYOU"].ToString()
                                , resultTable2.Rows[0]["DENPYOU_NUMBER"].ToString()
                                , resultTable2.Rows[0]["DENPYOU_DATE"].ToString()), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //締め処理チェック実行
            //システム設定で「締め処理時に支払明細チェックを実行」が「する」になっている場合
            DBAccessor accessor = new DBAccessor();
            M_SYS_INFO sysInfo = accessor.GetSysInfo();
            string shiharaiCheck = sysInfo.SHIHARAI_SHIME_SHIHARAI_CHECK.ToString();
            if (checkResult && "1" == shiharaiCheck)
            {
                //共通チェッククラスへ対象のデータリストを渡す(戻り値は一時エラーメッセージテーブル)
                ShimeCheckLogic checkLogic = new ShimeCheckLogic();
                DataTable resultTable = checkLogic.checkShimeData(checkDataList);

                //適格請求書用チェック（期間締のチェックは、システム設定に依存
                if ((!OldInvoiceFlag) && (!this.shimeChangeFlag))
                {
                    resultTable.Merge(checkLogic.checkShimeData_invoice(checkDataList));
                }

                //締処理エラーテーブル(T_SHIME_SHORI_ERROR)に既に存在しないかチェック
                //一致した場合は、↑のレコードを使う。なければ作成したレコードを適用

                //エラー一時テーブル作成
                DataTable ResultErrTable = new DataTable();
                ResultErrTable.Columns.Add("SHORI_KBN", Type.GetType("System.String"));//処理区分
                ResultErrTable.Columns.Add("CHECK_KBN", Type.GetType("System.String"));//チェック区分
                ResultErrTable.Columns.Add("DENPYOU_SHURUI_CD", Type.GetType("System.String"));//伝票書類CD
                ResultErrTable.Columns.Add("SYSTEM_ID", Type.GetType("System.String"));//システムID
                ResultErrTable.Columns.Add("SEQ", Type.GetType("System.String"));//枝番
                ResultErrTable.Columns.Add("DETAIL_SYSTEM_ID", Type.GetType("System.String"));//明細システムID
                ResultErrTable.Columns.Add("GYO_NUMBER", Type.GetType("System.String"));//行番号
                ResultErrTable.Columns.Add("ERROR_NAIYOU", Type.GetType("System.String"));//エラー内容
                ResultErrTable.Columns.Add("TORIHIKISAKI_CD", Type.GetType("System.String"));//取引先CD
                ResultErrTable.Columns.Add("RIYUU", Type.GetType("System.String"));//理由(締め処理エラーtblの値格納用)
                ResultErrTable.Columns.Add("KYOTEN_CD", Type.GetType("System.String"));//取引先名称
                ResultErrTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.String"));//伝票日付
                ResultErrTable.Columns.Add("URIAGE_DATE", Type.GetType("System.String"));
                ResultErrTable.Columns.Add("SHIHARAI_DATE", Type.GetType("System.String"));
                ResultErrTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.String"));//伝票番号
                ResultErrTable.Columns.Add("DAINOU_FLG", Type.GetType("System.String"));//代納

                CheckErrorMessageDTOClass errDto;
                foreach (DataRow r in resultTable.Rows)
                {
                    errDto = new CheckErrorMessageDTOClass();

                    errDto.SHORI_KBN = Int16.Parse(r["SHORI_KBN"].ToString());//処理区分
                    errDto.CHECK_KBN = Int16.Parse(r["CHECK_KBN"].ToString());//チェック区分
                    errDto.DENPYOU_SHURUI_CD = Int16.Parse(r["DENPYOU_SHURUI_CD"].ToString());//伝票種類CD
                    errDto.SYSTEM_ID = Int64.Parse(r["SYSTEM_ID"].ToString());//システムID
                    errDto.SEQ = Int32.Parse(r["SEQ"].ToString());//枝番

                    string DETAIL_SYSTEM_ID = r["DETAIL_SYSTEM_ID"].ToString();
                    if (!string.IsNullOrEmpty(DETAIL_SYSTEM_ID))
                    {
                        errDto.DETAIL_SYSTEM_ID = Int64.Parse(r["DETAIL_SYSTEM_ID"].ToString());//明細システムID
                    }

                    //一致するレコードチェック
                    int count = shimeShoriDao.CheckErrorTableDataForEntity(errDto);
                    if (0 < count)
                    {
                        //取得して新しい一時エラーテーブルへ詰め替え
                        DataTable table = shimeShoriDao.SelectErrorTableDataForEntity(errDto);
                        foreach (DataRow errRow in table.Rows)
                        {
                            //DB取得値を詰め直し
                            DataRow newRow = ResultErrTable.NewRow();
                            newRow["SHORI_KBN"] = errRow["SHORI_KBN"];
                            newRow["CHECK_KBN"] = errRow["CHECK_KBN"];
                            newRow["DENPYOU_SHURUI_CD"] = errRow["DENPYOU_SHURUI_CD"];
                            newRow["SYSTEM_ID"] = errRow["SYSTEM_ID"];
                            newRow["SEQ"] = errRow["SEQ"];
                            newRow["DETAIL_SYSTEM_ID"] = errRow["DETAIL_SYSTEM_ID"];
                            newRow["GYO_NUMBER"] = errRow["GYO_NUMBER"];
                            newRow["ERROR_NAIYOU"] = errRow["ERROR_NAIYOU"];
                            newRow["RIYUU"] = errRow["RIYUU"];

                            //DBから取得できないデータの詰め直し
                            newRow["TORIHIKISAKI_CD"] = r["TORIHIKISAKI_CD"];
                            newRow["KYOTEN_CD"] = r["KYOTEN_CD"];
                            newRow["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                            newRow["URIAGE_DATE"] = r["URIAGE_DATE"];
                            newRow["SHIHARAI_DATE"] = r["SHIHARAI_DATE"];
                            newRow["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                            newRow["DAINOU_FLG"] = r["DAINOU_FLG"];

                            //一致したレコードを追加
                            ResultErrTable.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        DataRow addRow = ResultErrTable.NewRow();
                        //値を詰め直し
                        addRow["SHORI_KBN"] = r["SHORI_KBN"];
                        addRow["CHECK_KBN"] = r["CHECK_KBN"];
                        addRow["DENPYOU_SHURUI_CD"] = r["DENPYOU_SHURUI_CD"];
                        addRow["SYSTEM_ID"] = r["SYSTEM_ID"];
                        addRow["SEQ"] = r["SEQ"];
                        addRow["DETAIL_SYSTEM_ID"] = r["DETAIL_SYSTEM_ID"];
                        addRow["GYO_NUMBER"] = r["GYO_NUMBER"];
                        addRow["ERROR_NAIYOU"] = r["ERROR_NAIYOU"];
                        addRow["RIYUU"] = r["RIYUU"];

                        addRow["TORIHIKISAKI_CD"] = r["TORIHIKISAKI_CD"];
                        addRow["KYOTEN_CD"] = r["KYOTEN_CD"];
                        addRow["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                        addRow["URIAGE_DATE"] = r["URIAGE_DATE"];
                        addRow["SHIHARAI_DATE"] = r["SHIHARAI_DATE"];
                        addRow["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                        addRow["DAINOU_FLG"] = r["DAINOU_FLG"];

                        //addRow = r;
                        //一致しないレコード追加
                        ResultErrTable.Rows.Add(addRow);
                    }
                }

                //一時テーブルの件数が1件以上ある場合
                if (0 < ResultErrTable.Rows.Count)
                {
                    //メッセージ出力
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (msgLogic.MessageBoxShow("I002", "支払") == DialogResult.Yes)
                    {
                        //YES選択
                        //支払締処理エラー一覧へ遷移
                        Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran.UIHeader headerForm = new Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran.UIHeader();
                        var callForm1 = new Shougun.Core.Adjustment.Shiharaijimesyorierrorichiran.UIForm(ResultErrTable, headerForm, this.form.txtKyotenCd.Text);
                        var BusinessBaseForm1 = new BasePopForm(callForm1, headerForm);
                        //支払締処理エラー一覧画面を起動する
                        var isExistForm1 = new FormControlLogic().ScreenPresenceCheck(callForm1);
                        if (!isExistForm1)
                        {
                            BusinessBaseForm1.ShowDialog(this.form);
                        }

                        //戻り値
                        errorIchiranResult = callForm1.ParamOut_Continue;
                        //(戻るまで処理待機)
                        if (!errorIchiranResult)
                        {
                            //処理中止の為終了
                            return errorIchiranResult;
                        }
                    }
                    else
                    {
                        //NO選択
                        //締めエラー一覧画面表示をキャンセル
                        LogUtility.DebugMethodEnd();
                        return errorIchiranResult;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
            return checkResult;
        }

        #endregion

        #region 閉じるボタンイベント

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //閉じる
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        //#region 伝票種類変更(RadioButtonと連動)イベント

        ///// <summary>
        ///// 伝票種類変更(RadioButtonと連動)イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void txtDenpyouShurui_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    if (this.shimeChangeFlag)
        //    {
        //        //伝票締め 又は 明細締めの場合
        //        //チェックされたレコードがある場合検索条件が変更されていないかチェック
        //        if (0 < CheckDGVCheckCount())
        //        {
        //            CheckSearchCondition(this.form.txtDenpyouShurui.Name);
        //        }
        //        else
        //        {
        //            CheckGridClear(this.form.txtDenpyouShurui.Name);
        //        }
        //    }

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 拠点コード入力欄でのロストフォーカスイベント

        ///// <summary>
        ///// 拠点CD入力欄でのロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void txtKyotenCd_LostFocus(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    //拠点CD・名称取得処理
        //    GetkyotenCd_Name();

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 拠点CD値変更イベント

        ///// <summary>
        ///// 拠点CD値変更イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void txtKyotenCd_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    // 拠点CDが空もしくは99(全社)の場合は、拠点を取引先CDポップアップの条件から外す
        //    var kyoten = this.form.txtKyotenCd.Text;
        //    this.TorihikisakiSearchConditionsKyoten(string.IsNullOrEmpty(kyoten) || kyoten == "99");

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 締日プルダウン値変更イベント

        ///// <summary>
        ///// 締日プルダウン値変更イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void cmbShimebi_Validated(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    // 拠点CDが空もしくは99(全社)の場合は、拠点を取引先CDポップアップの条件から外す
        //    var kyoten = this.form.txtKyotenCd.Text;

        //    // 支払締日のコントロール設定
        //    SetSearchDateState();

        //    // 拠点によって取引先CDのポップアップ検索条件を変更します
        //    this.TorihikisakiSearchConditionsKyoten(string.IsNullOrEmpty(kyoten) || kyoten == "99");

        //    if (0 < CheckDGVCheckCount())
        //    {
        //        CheckSearchCondition(this.form.cmbShimebi.Name);
        //    }
        //    else
        //    {
        //        CheckGridClear(this.form.cmbShimebi.Name);
        //    }

        //    //取引先CDが入力済みの場合、取引先CDをクリアする。
        //    if ((!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text)) && (enterShimebi != this.form.cmbShimebi.Text))
        //    {
        //        this.form.txtTorihikisakiCd.Text = string.Empty;
        //        this.form.txtTorihikisakiName.Text = string.Empty;
        //    }
        //    if (enterShimebi != this.form.cmbShimebi.Text)
        //    {
        //        this.form.checkBoxAll.Checked = false;  // No.2161
        //        this.form.txtGoukeikingaku.Text = "0";
        //        this.form.tb_goukeikingaku_shukkin.Text = "0";
        //    }

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        ///// <summary>
        ///// 変更前の締日情報を保持
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cmbShimebi_Enter(object sender, EventArgs e)
        //{
        //    enterShimebi = this.form.cmbShimebi.Text;
        //}

        //#endregion

        #region 締日・締日の設定-振分け

        /// <summary>
        /// 支払締日のコントロール設定
        /// </summary>
        private void SetSearchDateState()
        {
            string selected = this.form.cmbShimebi.Text;

            if (!this.shimeChangeFlag)
            {
                //期間締めの場合
                DateTime dtToday = parentForm.sysDate;
                DateTime dtLast;
                string lastday = string.Empty;
                string today = string.Empty;
                string strTodayDate = parentForm.sysDate.ToString("dd");

                if (0 == int.Parse(selected))
                {
                    //前月末日の取得
                    dtLast = new DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1);
                    lastday = dtLast.ToString("yyyy/MM/dd");

                    //当月当日を設定
                    this.form.dtpSearchDateFrom.SetResultText(dtToday.ToString("yyyy/MM/dd"));
                    this.form.dtpSearchDateTo.SetResultText(dtToday.ToString("yyyy/MM/dd"));

                    //請求締日Toと波線を表示
                    this.form.lb_hasen.Visible = true;
                    this.form.dtpSearchDateTo.Visible = true;
                    //編集可能に設定
                    this.form.dtpSearchDateFrom.Enabled = true;
                    this.form.dtpSearchDateTo.Enabled = true;
                }
                else
                {
                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }
            }
        }

        #endregion

        //#region 取引先コード入力欄でのロストフォーカスイベント

        ///// <summary>
        ///// 取引先コード入力欄でのロストフォーカスイベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void txtTorihikisakiCd_Leave(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    //取引先CD・名称取得処理
        //    GetTorihikisakiCdAndName();

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 請求締日Fromの変更イベント

        ///// <summary>
        ///// 請求締日Fromの変更イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void dtpSearchDateFrom_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    //チェックされたレコードがある場合検索条件が変更されていないかチェック
        //    if (0 < CheckDGVCheckCount())
        //    {
        //        CheckSearchCondition(this.form.dtpSearchDateFrom.Name);
        //    }
        //    else
        //    {
        //        CheckGridClear(this.form.dtpSearchDateFrom.Name);
        //    }

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 請求締日Toの変更イベント

        ///// <summary>
        ///// 請求締日Toの変更イベント
        ///// </summary>
        ///// <param name="sender">イベント呼び出し元オブジェクト</param>
        ///// <param name="e">e</param>
        //private void dtpSearchDateTo_TextChanged(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    //チェックされたレコードがある場合検索条件が変更されていないかチェック
        //    if (0 < CheckDGVCheckCount())
        //    {
        //        CheckSearchCondition(this.form.dtpSearchDateTo.Name);
        //    }
        //    else
        //    {
        //        //チェック件数0件。明細クリア
        //        CheckGridClear(this.form.dtpSearchDateTo.Name);
        //        this.form.customDataGridView1.Rows.Clear();
        //    }

        //    LogUtility.DebugMethodEnd(sender, e);
        //}

        //#endregion

        //#region 拠点CD・名称取得処理

        ///// <summary>
        ///// 拠点CD・名称取得処理
        ///// </summary>
        //internal void GetkyotenCd_Name()
        //{
        //    LogUtility.DebugMethodStart();

        //    //伝票締めの場合
        //    //チェックされたレコードがある場合検索条件が変更されていないかチェック
        //    if (0 < CheckDGVCheckCount())
        //    {
        //        CheckSearchCondition(this.form.txtKyotenCd.Name);
        //    }
        //    else
        //    {
        //        CheckGridClear(this.form.txtKyotenCd.Name);
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        #region DataGrigViewチェック件数チェック処理

        /// <summary>
        /// DataGrigViewチェック件数チェック処理
        /// </summary>
        /// <returns></returns>
        private int CheckDGVCheckCount()
        {
            LogUtility.DebugMethodStart();

            int checkCount = 0;
            int rowCount = this.form.customDataGridView1.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                if (bool.Parse(this.form.customDataGridView1.Rows[i].Cells[0].Value.ToString()))
                {
                    checkCount += 1;
                }
            }

            LogUtility.DebugMethodEnd();

            return checkCount;
        }

        #endregion

        //#region 取引先CD・名称取得処理

        ///// <summary>
        ///// 取引先CD・名称取得処理
        ///// </summary>
        //private void GetTorihikisakiCdAndName()
        //{
        //    LogUtility.DebugMethodStart();

        //    int count = 0;
        //    bool dataExist = true;
        //    if (!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text))
        //    {
        //        //取引先マスタデータ取得
        //        M_TORIHIKISAKI torihikisakiEntity = new M_TORIHIKISAKI();
        //        string torihikisakiCd = this.form.txtTorihikisakiCd.Text.PadLeft(6, '0');
        //        torihikisakiEntity.TORIHIKISAKI_CD = torihikisakiCd;
        //        this.torihikisakiResult = shimeShoriDao.GetTorihikisakiDataForEntity(torihikisakiEntity);

        //        count = torihikisakiResult.Rows.Count;

        //        if (count == 0)
        //        {
        //            //マスタチェックエラー
        //            this.form.txtTorihikisakiCd.Text = torihikisakiCd;
        //            this.form.txtTorihikisakiCd.IsInputErrorOccured = true;
        //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //            msgLogic.MessageBoxShow("E020", "取引先");
        //            this.form.txtTorihikisakiCd.Focus();
        //            dataExist = false;
        //        }

        //        if (dataExist)
        //        {
        //            //締日チェック
        //            string shimebi = this.form.cmbShimebi.Text;
        //            DataTable shimebiResult = shimeShoriDao.GetShimebiCheck(torihikisakiCd, shimebi);

        //            count = shimebiResult.Rows.Count;

        //            if (count == 0)
        //            {
        //                //締日チェックエラー
        //                this.form.txtTorihikisakiCd.Text = torihikisakiCd;
        //                this.form.txtTorihikisakiCd.IsInputErrorOccured = true;
        //                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //                msgLogic.MessageBoxShow("E058");
        //                this.form.txtTorihikisakiCd.Focus();
        //            }
        //        }
        //    }

        //    //取引先マスタ存在チェック
        //    if (0 != count)
        //    {
        //        //取引先名取得
        //        SetTorihikisakiCdAndName();

        //        //明細部に表示があるかチェック(なければ最新の請求日付+1を取得して設定)
        //        if (0 == this.form.customDataGridView1.Rows.Count)
        //        {
        //            if ("0".Equals(this.form.cmbShimebi.Text))
        //            {
        //                //明細部に表示がなく「締日」が0の場合のみ設定
        //                this.SetSeisanDate();
        //            }
        //        }
        //        else
        //        {
        //            //ある場合、チェックの件数をチェック
        //            if (0 < CheckDGVCheckCount())
        //            {
        //                CheckSearchCondition(this.form.txtTorihikisakiCd.Name);
        //            }
        //            else
        //            {
        //                CheckGridClear(this.form.txtTorihikisakiCd.Name);

        //                //締日が0
        //                if ("0".Equals(this.form.cmbShimebi.Text))
        //                {
        //                    //明細部に表示があるがチェックなしで「締日」が0の場合のみ設定
        //                    this.SetSeisanDate();
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (0 < CheckDGVCheckCount())
        //        {
        //            CheckSearchCondition(this.form.txtTorihikisakiCd.Name);
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        //#region 取得した取引先CD、取引先名称の設定

        ///// <summary>
        ///// 取得した取引先CD、取引先名称の設定を実行する
        ///// </summary>
        //private void SetTorihikisakiCdAndName()
        //{
        //    LogUtility.DebugMethodStart();

        //    this.form.txtTorihikisakiCd.Text = this.torihikisakiResult.Rows[0].ItemArray[0].ToString();
        //    this.form.txtTorihikisakiName.Text = string.IsNullOrEmpty(this.torihikisakiResult.Rows[0].ItemArray[1].ToString()) ? "" : this.torihikisakiResult.Rows[0].ItemArray[1].ToString();
        //    //取引先の情報を設定
        //    this.beforeTorihikisakiCd = this.torihikisakiResult.Rows[0].ItemArray[0].ToString();
        //    this.beforeTorihikisakiName = string.IsNullOrEmpty(this.torihikisakiResult.Rows[0].ItemArray[1].ToString()) ? "" : this.torihikisakiResult.Rows[0].ItemArray[1].ToString();

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        #region 最新の精算日付+1を設定

        /// <summary>
        /// 最新の精算日付の設定を実行する
        /// </summary>
        private void SetSeisanDate()
        {
            LogUtility.DebugMethodStart();

            //精算伝票から最新の精算日付取得
            SeikyuShimeShoriDto newDateDto = new SeikyuShimeShoriDto();
            newDateDto.SHIHARAI_CD = this.form.txtTorihikisakiCd.Text;
            //※取得できない場合は何もしない
            string getStrDate = shimeShoriDao.SelectSeisanDenpyouNewDateForEntity(newDateDto);
            if (!string.IsNullOrEmpty(getStrDate))
            {
                DateTime getDate = DateTime.Parse(getStrDate);
                //精算伝票から取得した最新の精算日付日付に1日足した日付を精算日付Fromに設定
                this.form.dtpSearchDateFrom.Text = new DateTime(getDate.Year, getDate.Month, getDate.Day).AddDays(1).ToString("yyyy/MM/dd");
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            #region ボタン設定切り替え

            if (this.shimeChangeFlag)
            {
                //伝票画面
                //期間ボタン有り
                this.ButtonInfoXmlPath = "Shougun.Core.Adjustment.Shiharaishimesyori.Setting.ButtonSettingKikanChange.xml";
            }
            else
            {
                //期間画面
                //伝票ボタン有り
                this.ButtonInfoXmlPath = "Shougun.Core.Adjustment.Shiharaishimesyori.Setting.ButtonSettingKikanDenpyou.xml";
            }

            #endregion

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region 一覧明細データ取得処理

        /// <summary>
        /// 一覧明細データ取得処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        private void GetMeisaiData()
        {
            LogUtility.DebugMethodStart();
            this.dto = new ShiharaiShimeShoriDispDto();//160017
            //明細ヘッダ表示項目の取得
            //期間締めの明細表示項目取得
            if (!this.shimeChangeFlag)
            {
                dto.SHIMEBI = Int16.Parse(this.form.cmbShimebi.Text);
                dto.SHIHARAI_CD = this.form.txtTorihikisakiCd.Text;
                dto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))//160017
                {
                    dto.SHIHARAISHIMEBI_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                }
                //検索期間To
                if (this.form.cmbShimebi.Text == "0")
                {
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                    {
                        dto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                    }
                }
                else
                {
                    dto.SHIHARAISHIMEBI_TO = null;
                }
                //取引先マスタ・取引先請求から該当データ抽出
                this.dispResult = this.shimeShoriDao.GetDispDataForEntity(dto);
            }
            else 
            {
                //伝票締めの明細表示項目取得
                //検索期間From
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))//160017
                {
                    dto.SHIHARAISHIMEBI_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                }
                //検索期間To
                if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))//160017
                {
                    dto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                }
                //取引先cd
                dto.SHIHARAI_CD = this.form.txtTorihikisakiCd.Text;
                //拠点cd
                dto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);

                //伝票締め表示項目取得
                GetDenpyouDispData();
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ①伝票締めの明細部の表示レコード取得

        /// <summary>
        /// 伝票締めの明細部の表示レコード取得
        /// </summary>
        private void GetDenpyouDispData()
        {
            LogUtility.DebugMethodStart();

            #region 抽出結果を統合するテーブル(DataTableの型指定)

            DataTable addTable = new DataTable();
            addTable.Columns.Add("DENPYOUSHURUI", Type.GetType("System.String"));
            addTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            addTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            addTable.Columns.Add("SHIHARAI_ZEI_KEISAN_KBN_CD", Type.GetType("System.String"));
            addTable.Columns.Add("SEISAN_SHIMEBI", Type.GetType("System.DateTime"));
            addTable.Columns.Add("GYOUSHA_NAME", Type.GetType("System.String"));
            addTable.Columns.Add("GENBA_NAME", Type.GetType("System.String"));
            addTable.Columns.Add("KINGAKU", Type.GetType("System.Decimal"));
            addTable.Columns.Add("HINMEI_SOTO_ZEI_COUNT", Type.GetType("System.Int64"));
            addTable.Columns.Add("HINMEI_NASI_ZEI_COUNT", Type.GetType("System.Int64"));
            addTable.Columns.Add("SHIHARAI_ZEI_KBN_CD", Type.GetType("System.String"));

            #endregion

            if ("1".Equals(this.form.txtDenpyouShurui.Text))
            {
                #region すべて(受入・出荷・売上支払)

                //受入入力/明細テーブル
                dto.DENPYO_SHURUI = 2;
                DataTable ukeireTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);

                //出荷入力/明細テーブル
                dto.DENPYO_SHURUI = 3;
                DataTable shukkaTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);

                //売上/支払入力/明細テーブル
                dto.DENPYO_SHURUI = 4;
                DataTable uriageTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);

                //抽出結果の結合
                SetTableResultDenpyou(ukeireTable, shukkaTable, uriageTable);

                #endregion
            }
            else if ("2".Equals(this.form.txtDenpyouShurui.Text))
            {
                #region 受入入力/明細テーブル

                dto.DENPYO_SHURUI = 2;
                DataTable ukeireTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);
                foreach (DataRow r in ukeireTable.Rows)
                {
                    DataRow row = addTable.NewRow();
                    row["DENPYOUSHURUI"] = "2";
                    row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                    row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                    row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                    row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                    row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                    row["GENBA_NAME"] = r["GENBA_NAME"];
                    row["KINGAKU"] = r["KINGAKU"];
                    row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                    row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                    row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                    addTable.Rows.Add(row);
                }
                //統合したDataTableをSortする
                SetSortedTableDenpyou(addTable);

                #endregion
            }
            else if ("3".Equals(this.form.txtDenpyouShurui.Text))
            {
                #region 出荷入力/明細テーブル

                dto.DENPYO_SHURUI = 3;
                DataTable shukkaTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);
                foreach (DataRow r in shukkaTable.Rows)
                {
                    DataRow row = addTable.NewRow();
                    row["DENPYOUSHURUI"] = "3";
                    row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                    row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                    row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                    row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                    row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                    row["GENBA_NAME"] = r["GENBA_NAME"];
                    row["KINGAKU"] = r["KINGAKU"];
                    row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                    row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                    row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                    addTable.Rows.Add(row);
                }
                //統合したDataTableをSortする
                SetSortedTableDenpyou(addTable);

                #endregion
            }
            else if ("4".Equals(this.form.txtDenpyouShurui.Text))
            {
                #region 売上/支払入力/明細テーブル

                dto.DENPYO_SHURUI = 4;
                DataTable uriageTable = shimeShoriDao.GetDispDataDenpyouShimeForEntity(dto);
                foreach (DataRow r in uriageTable.Rows)
                {
                    DataRow row = addTable.NewRow();
                    row["DENPYOUSHURUI"] = "4";
                    row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                    row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                    row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                    row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                    row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                    row["GENBA_NAME"] = r["GENBA_NAME"];
                    row["KINGAKU"] = r["KINGAKU"];
                    row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                    row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                    row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                    addTable.Rows.Add(row);
                }
                //統合したDataTableをSortする
                SetSortedTableDenpyou(addTable);

                #endregion
            }

            //VAN 20210429 #148578 S
            //出金データ
            DataTable shukkinTable = shimeShoriDao.GetDispDataDenpyouShimeShukkinForEntity(dto);
            var shukkinData = addTable.Clone();
            foreach (DataRow r in shukkinTable.Rows)
            {
                DataRow row = shukkinData.NewRow();
                row["DENPYOUSHURUI"] = Shougun.Core.Common.BusinessCommon.Const.CommonConst.DENSHU_KBN_SHUKKINN.ToString();
                row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                row["GENBA_NAME"] = r["GENBA_NAME"];
                row["KINGAKU"] = r["KINGAKU"];
                row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                shukkinData.Rows.Add(row);
            }

            // Add data shukkin to dispResult
            // 出金伝票>支払伝票
            var copyData = addTable.Clone();
            foreach (DataRow drv in shukkinData.Rows)
            {
                copyData.ImportRow(drv);
            }

            foreach (DataRow drv in this.dispResult.Rows)
            {
                copyData.ImportRow(drv);
            }

            this.dispResult = copyData.Copy();
            //VAN 20210429 #148578 E

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ②各テーブルから抽出したレコードをまとめる

        /// <summary>
        /// 各テーブルから抽出したレコードをまとめる
        /// </summary>
        /// <param name="ukeireTable">ukeireTable</param>
        /// <param name="shukkaTable">shukkaTable</param>
        /// <param name="uriageTable">uriageTable</param>
        private void SetTableResultDenpyou(DataTable ukeireTable, DataTable shukkaTable, DataTable uriageTable)
        {
            LogUtility.DebugMethodStart(ukeireTable, shukkaTable, uriageTable);

            #region 抽出結果を統合するテーブル(DataTableの型指定)

            DataTable addTable = new DataTable();
            addTable.Columns.Add("DENPYOUSHURUI", Type.GetType("System.String"));
            addTable.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            addTable.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            addTable.Columns.Add("SHIHARAI_ZEI_KEISAN_KBN_CD", Type.GetType("System.String"));
            addTable.Columns.Add("SEISAN_SHIMEBI", Type.GetType("System.DateTime"));
            addTable.Columns.Add("GYOUSHA_NAME", Type.GetType("System.String"));
            addTable.Columns.Add("GENBA_NAME", Type.GetType("System.String"));
            addTable.Columns.Add("KINGAKU", Type.GetType("System.Decimal"));
            addTable.Columns.Add("HINMEI_SOTO_ZEI_COUNT", Type.GetType("System.Int64"));
            addTable.Columns.Add("HINMEI_NASI_ZEI_COUNT", Type.GetType("System.Int64"));
            addTable.Columns.Add("SHIHARAI_ZEI_KBN_CD", Type.GetType("System.String"));

            #endregion

            #region Sortまで完了したData格納用(DataTableの型指定)

            this.dispResult = new DataTable();
            this.dispResult.Columns.Add("DENPYOUSHURUI", Type.GetType("System.String"));
            this.dispResult.Columns.Add("DENPYOU_NUMBER", Type.GetType("System.Int64"));
            this.dispResult.Columns.Add("DENPYOU_DATE", Type.GetType("System.DateTime"));
            this.dispResult.Columns.Add("SHIHARAI_ZEI_KEISAN_KBN_CD", Type.GetType("System.String"));
            this.dispResult.Columns.Add("SEISAN_SHIMEBI", Type.GetType("System.DateTime"));
            this.dispResult.Columns.Add("GYOUSHA_NAME", Type.GetType("System.String"));
            this.dispResult.Columns.Add("GENBA_NAME", Type.GetType("System.String"));
            this.dispResult.Columns.Add("KINGAKU", Type.GetType("System.Decimal"));
            this.dispResult.Columns.Add("HINMEI_SOTO_ZEI_COUNT", Type.GetType("System.Int64"));
            this.dispResult.Columns.Add("HINMEI_NASI_ZEI_COUNT", Type.GetType("System.Int64"));
            this.dispResult.Columns.Add("SHIHARAI_ZEI_KBN_CD", Type.GetType("System.String"));

            #endregion

            #region 抽出結果の統合

            foreach (DataRow r in ukeireTable.Rows)
            {
                DataRow row = addTable.NewRow();
                row["DENPYOUSHURUI"] = "2";
                row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                row["GENBA_NAME"] = r["GENBA_NAME"];
                row["KINGAKU"] = r["KINGAKU"];
                row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                addTable.Rows.Add(row);
            }
            foreach (DataRow r in shukkaTable.Rows)
            {
                DataRow row = addTable.NewRow();
                row["DENPYOUSHURUI"] = "3";
                row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                row["GENBA_NAME"] = r["GENBA_NAME"];
                row["KINGAKU"] = r["KINGAKU"];
                row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                addTable.Rows.Add(row);
            }
            foreach (DataRow r in uriageTable.Rows)
            {
                DataRow row = addTable.NewRow();
                row["DENPYOUSHURUI"] = "4";
                row["DENPYOU_NUMBER"] = r["DENPYOU_NUMBER"];
                row["DENPYOU_DATE"] = r["DENPYOU_DATE"];
                row["SHIHARAI_ZEI_KEISAN_KBN_CD"] = r["SHIHARAI_ZEI_KEISAN_KBN_CD"];
                row["SEISAN_SHIMEBI"] = r["SEISAN_SHIMEBI"];
                row["GYOUSHA_NAME"] = r["GYOUSHA_NAME"];
                row["GENBA_NAME"] = r["GENBA_NAME"];
                row["KINGAKU"] = r["KINGAKU"];
                row["HINMEI_SOTO_ZEI_COUNT"] = r["HINMEI_SOTO_ZEI_COUNT"];
                row["HINMEI_NASI_ZEI_COUNT"] = r["HINMEI_NASI_ZEI_COUNT"];
                row["SHIHARAI_ZEI_KBN_CD"] = r["SHIHARAI_ZEI_KBN_CD"];
                addTable.Rows.Add(row);
            }

            #endregion

            //統合したDataTableをSortする
            SetSortedTableDenpyou(addTable);

            LogUtility.DebugMethodEnd(ukeireTable, shukkaTable, uriageTable);
        }

        #endregion

        #region ③各テーブルから取得、統合後のDataTableをSortする

        /// <summary>
        /// 各テーブルから取得、統合後のDataTableをSortする
        /// </summary>
        /// <param name="addTable">addTable</param>
        private void SetSortedTableDenpyou(DataTable addTable)
        {
            LogUtility.DebugMethodStart(addTable);

            this.dispResult = addTable.Clone();
            DataView dv = new DataView(addTable);
            dv.Sort = "DENPYOU_DATE,DENPYOUSHURUI,DENPYOU_NUMBER";
            foreach (DataRowView drv in dv)
            {
                this.dispResult.ImportRow(drv.Row);
            }

            LogUtility.DebugMethodEnd(addTable);
        }

        #endregion

        #region DataGridViewに値の設定を行う

        /// <summary>
        /// DataGridViewに値の設定を行う
        /// </summary>
        /// <param name="table"></param>
        private void CreateDataGridView()
        {
            LogUtility.DebugMethodStart();

            this.form.customDataGridView1.IsBrowsePurpose = false;

            if (!this.shimeChangeFlag)
            {
                #region 期間締め

                //前の結果をクリア
                int k = this.form.customDataGridView1.Rows.Count;
                // カーソル位置DataGridViewにあると、検索直後にRow(0,0)が選択状態になりチェックボックスが編集中になってしまう。
                // そのためチェックボックスのEditedFormattedValueが更新されない現象が発生する。
                this.form.ActiveControl = null;
                for (int i = k; i >= 1; i--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                }

                #region 表示するカラムの設定

                //※期間締め処理で表示するカラムを表示に設定
                this.form.customDataGridView1.Columns["CHECK_BOX"].Visible = true; //締(checkbox)
                this.form.customDataGridView1.Columns["CHECK_BOX_SAISHIME"].Visible = true; //再締(checkbox)
                this.form.customDataGridView1.Columns["TORIHIKI_CD"].Visible = true; //取引先CD(期間)
                this.form.customDataGridView1.Columns["TORIHIKI_NAME"].Visible = true; //取引先名(期間)

                this.form.customDataGridView1.Columns["SHIMEBI_SHOW"].Visible = true; //締(期間)

                this.form.customDataGridView1.Columns["SHIMEBI1"].Visible = true; //締日1(期間)
                this.form.customDataGridView1.Columns["SHIMEBI2"].Visible = true; //締日2(期間)
                this.form.customDataGridView1.Columns["SHIMEBI3"].Visible = true; //締日3(期間)
                this.form.customDataGridView1.Columns["ADDRESS1"].Visible = true; //住所1(期間)
                this.form.customDataGridView1.Columns["ADDRESS2"].Visible = true; //住所2(期間)
                this.form.customDataGridView1.Columns["TEL"].Visible = true; //TEL(期間)
                this.form.customDataGridView1.Columns["FAX"].Visible = true; //FAX(期間)
                //※伝票/明細締め処理で表示するカラムを非表示に設定
                this.form.customDataGridView1.Columns["DENPYOUSHURUI"].Visible = false; //伝票種類(伝票/明細)
                this.form.customDataGridView1.Columns["DENPYOU_NUMBER"].Visible = false; //伝票番号(伝票/明細)
                this.form.customDataGridView1.Columns["MEISAI_NO"].Visible = false; //明細番号(明細)
                this.form.customDataGridView1.Columns["DENPYOU_DATE"].Visible = false; //伝票日付(伝票/明細)
                this.form.customDataGridView1.Columns["SHIHARAI_SHIMEBI"].Visible = false; //支払締日(伝票/明細)
                this.form.customDataGridView1.Columns["GYOUSHA_NAME"].Visible = false; //業者名(伝票/明細)
                this.form.customDataGridView1.Columns["GENBA_NAME"].Visible = false; //現場名(伝票/明細)
                this.form.customDataGridView1.Columns["HIN_NAME"].Visible = false; //品名(明細)
                this.form.customDataGridView1.Columns["SUURYOU"].Visible = false; //数量(明細)
                this.form.customDataGridView1.Columns["TANI"].Visible = false; //単位(明細)
                this.form.customDataGridView1.Columns["TANKA"].Visible = false; //単価(明細)
                this.form.customDataGridView1.Columns["KINGAKU"].Visible = false; //金額(伝票/明細)
                this.form.customDataGridView1.Columns["ZEI_KEISAN_KBN_NAME"].Visible = false; //税計算区分(伝票/明細)

                #endregion
                this.form.checkBoxAllSaiShime.Checked = false;
                if (this.form.HYOUJI_JOUKEN_KBN.Text == "1")//１.未締め「取引先」を抽出（請求書無し）
                {
                    this.form.checkBoxAllSaiShime.Enabled = false;
                    this.form.customDataGridView1.Columns["CHECK_BOX_SAISHIME"].ReadOnly = true;
                }
                else//２.締済の「取引先」を抽出（請求書有り）//３.全ての取引先を抽出
                {
                    this.form.checkBoxAllSaiShime.Enabled = true;
                    this.form.customDataGridView1.Columns["CHECK_BOX_SAISHIME"].ReadOnly = false;
                }
                //検索結果設定
                if (this.dispResult != null)
                {
                    int index = 0;
                    for (int i = 0; i < this.dispResult.Rows.Count; i++)
                    {
                        string shiharaCd = this.dispResult.Rows[i]["SHIHARAI_CD"].ToString();
                        if (this.form.HYOUJI_JOUKEN_KBN.Text == "1")//１.未締め「取引先」を抽出（請求書無し）
                        {
                            if (!(CheckShiharaData(shiharaCd, true) && (CheckShiharaDataNoShimei(shiharaCd)
                                || CheckShiharaDataSonzai(shiharaCd) || CheckSeikyuDataSonzaiNashi(shiharaCd))))
                            {
                                continue;
                            }
                            this.form.customDataGridView1.Rows.Add();
                        }
                       else if (this.form.HYOUJI_JOUKEN_KBN.Text == "2")//２.締済の「取引先」を抽出（請求書有り）
                        {
                            if (CheckShiharaData(shiharaCd, true))
                            {
                                 continue;
                            }
                            this.form.customDataGridView1.Rows.Add();
                            this.form.customDataGridView1.Rows[index].Cells["SHIMEBI_SHOW"].Value = "済";
                        }
                        else//３.全ての取引先を抽出
                        {
                            if (!CheckShiharaData(shiharaCd, true))
                            {
                                this.form.customDataGridView1.Rows.Add();
                                this.form.customDataGridView1.Rows[index].Cells["SHIMEBI_SHOW"].Value = "済";
                            }
                            else
                            {
                                this.form.customDataGridView1.Rows.Add();
                                this.form.customDataGridView1.Rows[index].Cells["SHIMEBI_SHOW"].Value = "";
                            }
                        }

                        if ("0".Equals(this.form.cmbShimebi.Text))
                        {
                            this.form.customDataGridView1.Rows[index].Cells["CHECK_BOX"].Value = false;
                            //全件制御用チェックボックスもtrueに設定
                            this.form.checkBoxAll.Checked = false;
                            //this.form.checkBoxAll.Enabled = false;   // No.2161
                            this.form.checkBoxAll.Enabled = true;
                        }
                        else
                        {
                            this.form.customDataGridView1.Rows[index].Cells["CHECK_BOX"].Value = true;
                            //全件制御用チェックボックスもtrueに設定
                            this.form.checkBoxAll.Checked = true;
                            this.form.checkBoxAll.Enabled = true;   // No.2161
                        }
                        if (!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text) &&
                                this.form.customDataGridView1.Rows[index].Cells["SHIMEBI_SHOW"].Value == "済")
                        {
                            this.form.customDataGridView1.Rows[index].Cells["CHECK_BOX_SAISHIME"].ReadOnly = false;
                        }
                        else
                        {
                            this.form.customDataGridView1.Rows[index].Cells["CHECK_BOX_SAISHIME"].ReadOnly = true;
                        }
                        this.form.customDataGridView1.Rows[index].Cells["CHECK_BOX_SAISHIME"].Value = false;
                        this.form.customDataGridView1.Rows[index].Cells["TORIHIKI_CD"].Value = this.dispResult.Rows[i]["SHIHARAI_CD"];
                        this.form.customDataGridView1.Rows[index].Cells["TORIHIKI_NAME"].Value = this.dispResult.Rows[i]["SHIHARAI_NAME"];
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI1"].Value = this.dispResult.Rows[i]["SHIMEBI1"];
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI2"].Value = this.dispResult.Rows[i]["SHIMEBI2"];
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI3"].Value = this.dispResult.Rows[i]["SHIMEBI3"];
                        this.form.customDataGridView1.Rows[index].Cells["ADDRESS1"].Value = this.dispResult.Rows[i]["ADDRESS1"];
                        this.form.customDataGridView1.Rows[index].Cells["ADDRESS2"].Value = this.dispResult.Rows[i]["ADDRESS2"];
                        this.form.customDataGridView1.Rows[index].Cells["TEL"].Value = this.dispResult.Rows[i]["TEL"];
                        this.form.customDataGridView1.Rows[index].Cells["FAX"].Value = this.dispResult.Rows[i]["FAX"];
                        //ReadOnly(編集不可) = trueに設定
                        this.form.customDataGridView1.Rows[index].Cells["TORIHIKI_CD"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["TORIHIKI_NAME"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI1"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI2"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI3"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["ADDRESS1"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["ADDRESS2"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["TEL"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["FAX"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[index].Cells["SHIMEBI_SHOW"].ReadOnly = true;
                        index++;
                    }
                    if (index == 0)
                    {
                        this.form.customDataGridView1.Rows.Clear();
                        if (!bShimeSyoriZikkochu)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("C001");
                        }
                    }
                }

                #endregion
            }
            //※表示項目の制御
            else
            {
                #region 伝票締め

                //前の結果をクリア
                int k = this.form.customDataGridView1.Rows.Count;
                for (int i = k; i >= 1; i--)
                {
                    this.form.customDataGridView1.Rows.RemoveAt(this.form.customDataGridView1.Rows[i - 1].Index);
                }

                #region 表示するカラムの設定

                //チェックボックス列
                this.form.customDataGridView1.Columns["CHECK_BOX"].Visible = true; //締(checkbox)
                //※期間締め処理で表示するカラムを非表示に設定
                this.form.customDataGridView1.Columns["CHECK_BOX_SAISHIME"].Visible = false; //再締(checkbox)
                this.form.customDataGridView1.Columns["TORIHIKI_CD"].Visible = false; //取引先CD(期間)
                this.form.customDataGridView1.Columns["TORIHIKI_NAME"].Visible = false; //取引先名(期間)
                this.form.customDataGridView1.Columns["SHIMEBI_SHOW"].Visible = false; //締(期間)
                this.form.customDataGridView1.Columns["SHIMEBI1"].Visible = false; //締日1(期間)
                this.form.customDataGridView1.Columns["SHIMEBI2"].Visible = false; //締日2(期間)
                this.form.customDataGridView1.Columns["SHIMEBI3"].Visible = false; //締日3(期間)
                this.form.customDataGridView1.Columns["ADDRESS1"].Visible = false; //住所1(期間)
                this.form.customDataGridView1.Columns["ADDRESS2"].Visible = false; //住所2(期間)
                this.form.customDataGridView1.Columns["TEL"].Visible = false; //TEL(期間)
                this.form.customDataGridView1.Columns["FAX"].Visible = false; //FAX(期間)
                //※伝票/明細締め処理で表示するカラムを表示に設定
                this.form.customDataGridView1.Columns["DENPYOUSHURUI"].Visible = true; //伝票種類(伝票/明細)
                this.form.customDataGridView1.Columns["DENPYOU_NUMBER"].Visible = true; //伝票番号(伝票/明細)
                this.form.customDataGridView1.Columns["MEISAI_NO"].Visible = false; //明細番号(明細)
                this.form.customDataGridView1.Columns["DENPYOU_DATE"].Visible = true; //伝票日付(伝票/明細)
                this.form.customDataGridView1.Columns["SHIHARAI_SHIMEBI"].Visible = true; //支払締日(伝票/明細)
                this.form.customDataGridView1.Columns["GYOUSHA_NAME"].Visible = true; //業者名(伝票/明細)
                this.form.customDataGridView1.Columns["GENBA_NAME"].Visible = true; //現場名(伝票/明細)
                this.form.customDataGridView1.Columns["HIN_NAME"].Visible = false; //品名(明細)
                this.form.customDataGridView1.Columns["SUURYOU"].Visible = false; //数量(明細)
                this.form.customDataGridView1.Columns["TANI"].Visible = false; //単位(明細)
                this.form.customDataGridView1.Columns["TANKA"].Visible = false; //単価(明細)
                this.form.customDataGridView1.Columns["KINGAKU"].Visible = true; //金額(伝票/明細)
                this.form.customDataGridView1.Columns["ZEI_KEISAN_KBN_NAME"].Visible = true; //税計算区分(伝票/明細)

                #endregion

                //検索結果設定
                if (this.dispResult != null)
                {
                    for (int i = 0; i < this.dispResult.Rows.Count; i++)
                    {
                        this.form.customDataGridView1.Rows.Add();

                        //初期表示はチェックをtrueに設定
                        this.form.customDataGridView1.Rows[i].Cells["CHECK_BOX"].Value = true;
                        //全件制御用チェックボックスもtrueに設定
                        this.form.checkBoxAll.Checked = true;
                        this.form.checkBoxAll.Enabled = true;

                        DateTime dt;
                        string strDenpyouDate = string.Empty;
                        if (DateTime.TryParse(this.dispResult.Rows[i]["DENPYOU_DATE"].ToString(), out dt))
                        {
                            strDenpyouDate = dt.ToString("yyyy/MM/dd");
                        }
                        string strShimebiDate = string.Empty;
                        if (DateTime.TryParse(this.dispResult.Rows[i]["SEISAN_SHIMEBI"].ToString(), out dt))
                        {
                            strShimebiDate = dt.ToString("yyyy/MM/dd");
                        }
                        if ("2".Equals(this.dispResult.Rows[i]["DENPYOUSHURUI"]))
                        {
                            // 受入
                            this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value = denshuName_Ukeire;
                        }
                        else if ("3".Equals(this.dispResult.Rows[i]["DENPYOUSHURUI"]))
                        {
                            //出荷
                            this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value = denshuName_Shukka;
                        }
                        else if ("4".Equals(this.dispResult.Rows[i]["DENPYOUSHURUI"]))
                        {
                            // 売上/支払
                            this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value = denshuName_UrSh;
                        }
                        //VAN 20210429 #148578 S
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.DENSHU_KBN_SHUKKINN.ToString().Equals(this.dispResult.Rows[i]["DENPYOUSHURUI"]))
                        {
                            // 出金
                            this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value = denshuName_Shukkin;
                        }
                        //VAN 20210429 #148578 E
                        #region 適格請求書
                        this.form.customDataGridView1.Rows[i].Cells["HINMEI_SOTO_ZEI_COUNT"].Value = this.dispResult.Rows[i]["HINMEI_SOTO_ZEI_COUNT"];
                        this.form.customDataGridView1.Rows[i].Cells["HINMEI_NASI_ZEI_COUNT"].Value = this.dispResult.Rows[i]["HINMEI_NASI_ZEI_COUNT"];
                        this.form.customDataGridView1.Rows[i].Cells["SHIHARAI_ZEI_KBN_CD"].Value = this.dispResult.Rows[i]["SHIHARAI_ZEI_KBN_CD"];

                        if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(this.dispResult.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                        {
                            this.form.customDataGridView1.Rows[i].Cells["ZEI_KEISAN_KBN_NAME"].Value = "伝票毎";
                            //レイアウト区分　1.適格請求書、且つ、
                            //伝票毎(品名税なしor品名外税)の場合、チェックOFF
                            if ((this.headerForm.INVOICE_KBN.Text == "1") &&
                                ((this.dispResult.Rows[i]["HINMEI_SOTO_ZEI_COUNT"].ToString() != "0") ||
                                (this.dispResult.Rows[i]["HINMEI_NASI_ZEI_COUNT"].ToString() != "0")))
                            {   
                                this.form.customDataGridView1.Rows[i].Cells["CHECK_BOX"].Value = false;
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(this.dispResult.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                        {
                            this.form.customDataGridView1.Rows[i].Cells["ZEI_KEISAN_KBN_NAME"].Value = "精算毎";
                            //レイアウト区分　1.適格請求書且つ、
                            //請求毎(品名外税)の場合、チェックOFF
                            if ((this.headerForm.INVOICE_KBN.Text == "1") &&
                                (this.dispResult.Rows[i]["HINMEI_SOTO_ZEI_COUNT"].ToString() != "0"))
                            {
                                this.form.customDataGridView1.Rows[i].Cells["CHECK_BOX"].Value = false;
                            }
                        }
                        else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString().Equals(this.dispResult.Rows[i]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                        {
                            this.form.customDataGridView1.Rows[i].Cells["ZEI_KEISAN_KBN_NAME"].Value = "明細毎";
                            //レイアウト区分　1.適格請求書且つ、
                            //明細毎(品名外税 or 品名税なしand外税)の場合、締チェックOFF
                            if ((this.headerForm.INVOICE_KBN.Text == "1") &&
                                ((this.dispResult.Rows[i]["HINMEI_SOTO_ZEI_COUNT"].ToString() != "0") ||
                                ((this.dispResult.Rows[i]["HINMEI_NASI_ZEI_COUNT"].ToString() != "0") && (this.dispResult.Rows[i]["SHIHARAI_ZEI_KBN_CD"].ToString() == "1"))))
                            {
                                this.form.customDataGridView1.Rows[i].Cells["CHECK_BOX"].Value = false;
                            }
                        }
                        #endregion 適格請求書
                        //this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].Value = this.dispResult.Rows[i]["DENPYOUSHURUI"];
                        this.form.customDataGridView1.Rows[i].Cells["DENPYOU_NUMBER"].Value = this.dispResult.Rows[i]["DENPYOU_NUMBER"];
                        this.form.customDataGridView1.Rows[i].Cells["DENPYOU_DATE"].Value = strDenpyouDate;
                        this.form.customDataGridView1.Rows[i].Cells["SHIHARAI_SHIMEBI"].Value = strShimebiDate;
                        this.form.customDataGridView1.Rows[i].Cells["GYOUSHA_NAME"].Value = this.dispResult.Rows[i]["GYOUSHA_NAME"];
                        this.form.customDataGridView1.Rows[i].Cells["GENBA_NAME"].Value = this.dispResult.Rows[i]["GENBA_NAME"];
                        this.form.customDataGridView1.Rows[i].Cells["KINGAKU"].Value = SetComma(this.dispResult.Rows[i]["KINGAKU"].ToString());
                        //ReadOnly(編集不可) = trueに設定
                        this.form.customDataGridView1.Rows[i].Cells["DENPYOUSHURUI"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["DENPYOU_NUMBER"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["DENPYOU_DATE"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["SHIHARAI_SHIMEBI"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["GYOUSHA_NAME"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["GENBA_NAME"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["KINGAKU"].ReadOnly = true;
                        this.form.customDataGridView1.Rows[i].Cells["ZEI_KEISAN_KBN_NAME"].ReadOnly = true;
                    }

                    parentForm.bt_func9.Enabled = true;
                    parentForm.bt_process4.Enabled = true;
                    parentForm.bt_process5.Enabled = true;

                }

                #endregion
            }

            this.form.customDataGridView1.IsBrowsePurpose = true;

            LogUtility.DebugMethodEnd();
        }

        // チェック支払明細書が作成かどうか
        public bool CheckShiharaData(string shiharaCd, bool selectFlg)
        {
            //string shiharaDate;
            //if(this.form.cmbShimebi.Text == "0")
            //{
            //    shiharaDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
            //}
            //else
            //{
            //    shiharaDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
            //}

            //this.shiharadata = this.shimeShoriDao.CheckShiharaData(shiharaCd, shiharaDate, selectFlg);
            //if (shiharadata != null && shiharadata.Rows.Count > 0)
            //{
            //    return false;
            //}
            //return true;

            SeikyuShimeShoriDto saishimeSearchDto = new SeikyuShimeShoriDto();
            saishimeSearchDto.SHIHARAI_CD = shiharaCd;
            if (dto.SHIMEBI == 0)
            {
                saishimeSearchDto.SHIHARAISHIMEBI_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                saishimeSearchDto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
            }
            else if (dto.SHIMEBI == 31)
            {
                DateTime seisanDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text);
                saishimeSearchDto.SHIHARAISHIMEBI_TO = seisanDate.ToString("yyyy/MM/dd");
                saishimeSearchDto.SHIHARAISHIMEBI_FROM = new DateTime(seisanDate.Year, seisanDate.Month, 1).ToString("yyyy/MM/dd");
            }
            else
            {
                DateTime seisanDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text);
                saishimeSearchDto.SHIHARAISHIMEBI_TO = seisanDate.ToString("yyyy/MM/dd");
                saishimeSearchDto.SHIHARAISHIMEBI_FROM = seisanDate.AddMonths(-1).AddDays(1).ToString("yyyy/MM/dd");
            }

            this.shiharadata = shimeShoriDao.GetSeisanSaishimeData(saishimeSearchDto);
            if (shiharadata != null && shiharadata.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        // 支払明細書有り場合、前回繰越額ー今回出金額ー今回調整額＋今回支払額≠０かどうか
        public bool CheckShiharaDataSonzai(string shiharaCd)
        {
            string shiharaDate;
            if (this.form.cmbShimebi.Text == "0")
            {
                shiharaDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
            }
            else
            {
                shiharaDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
            }

            DataTable shiharadata = this.shimeShoriDao.CheckShiharaData(shiharaCd, shiharaDate, false);

            if (shiharadata != null && shiharadata.Rows.Count > 0)
            {
                decimal kinGaku = 0;
                decimal ZENKAI_KURIKOSI_GAKU = 0;
                decimal KONKAI_SHUKKIN_GAKU = 0;
                decimal KONKAI_CHOUSEI_GAKU = 0;
                decimal KONKAI_SHIHARAI_GAKU = 0;
                decimal KONKAI_SEI_UTIZEI_GAKU = 0;
                decimal KONKAI_SEI_SOTOZEI_GAKU = 0;
                decimal KONKAI_DEN_UTIZEI_GAKU = 0;
                decimal KONKAI_DEN_SOTOZEI_GAKU = 0;
                decimal KONKAI_MEI_UTIZEI_GAKU = 0;
                decimal KONKAI_MEI_SOTOZEI_GAKU = 0;

                if (shiharadata.Rows[0]["ZENKAI_KURIKOSI_GAKU"] != null)
                {
                    ZENKAI_KURIKOSI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["ZENKAI_KURIKOSI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_SHUKKIN_GAKU"] != null)
                {
                    KONKAI_SHUKKIN_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_SHUKKIN_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_CHOUSEI_GAKU"] != null)
                {
                    KONKAI_CHOUSEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_CHOUSEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_SHIHARAI_GAKU"] != null)
                {
                    KONKAI_SHIHARAI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_SHIHARAI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_SEI_UTIZEI_GAKU"] != null)
                {
                    KONKAI_SEI_UTIZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_SEI_UTIZEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_SEI_SOTOZEI_GAKU"] != null)
                {
                    KONKAI_SEI_SOTOZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_SEI_SOTOZEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_DEN_UTIZEI_GAKU"] != null)
                {
                    KONKAI_DEN_UTIZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_DEN_UTIZEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_DEN_SOTOZEI_GAKU"] != null)
                {
                    KONKAI_DEN_SOTOZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_DEN_SOTOZEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_MEI_UTIZEI_GAKU"] != null)
                {
                    KONKAI_MEI_UTIZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_MEI_UTIZEI_GAKU"].ToString());
                }

                if (shiharadata.Rows[0]["KONKAI_MEI_SOTOZEI_GAKU"] != null)
                {
                    KONKAI_MEI_SOTOZEI_GAKU = Convert.ToDecimal(shiharadata.Rows[0]["KONKAI_MEI_SOTOZEI_GAKU"].ToString());
                }

                kinGaku = ZENKAI_KURIKOSI_GAKU - KONKAI_SHUKKIN_GAKU - KONKAI_CHOUSEI_GAKU + KONKAI_SHIHARAI_GAKU + KONKAI_SEI_UTIZEI_GAKU
                          + KONKAI_SEI_SOTOZEI_GAKU + KONKAI_DEN_UTIZEI_GAKU + KONKAI_DEN_SOTOZEI_GAKU + KONKAI_MEI_UTIZEI_GAKU + KONKAI_MEI_SOTOZEI_GAKU;

                if (kinGaku != 0)
                {
                    return true;
                }
            }
            return false;
        }

        // 支払明細書無し場合、該当する取引先ー開始買掛残高≠０かどうか
        public bool CheckSeikyuDataSonzaiNashi(string shiharaCd)
        {
            M_TORIHIKISAKI_SHIHARAI toriShiharadata = new M_TORIHIKISAKI_SHIHARAI();

            string shiharaDate;
            if (this.form.cmbShimebi.Text == "0")
            {
                shiharaDate = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
            }
            else
            {
                shiharaDate = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
            }

            DataTable shiharadata = this.shimeShoriDao.CheckShiharaData(shiharaCd, shiharaDate, false);

            if(shiharadata == null || shiharadata.Rows.Count == 0)
            {
                toriShiharadata = this.shiharaiDao.GetDataByCd(shiharaCd);
                if (toriShiharadata != null && !toriShiharadata.KAISHI_KAIKAKE_ZANDAKA.IsNull && toriShiharadata.KAISHI_KAIKAKE_ZANDAKA != 0)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool CheckShiharaDataNoShimei(string shiharaCd)
        {
            //期間締めの明細表示項目取得
            bool ret = false;
            SeikyuShimeShoriDto dto = new SeikyuShimeShoriDto();
            dto.SHIHARAI_CD = shiharaCd;
            dto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);

            if (this.form.cmbShimebi.Text == "0")
            {
                dto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
            }
            else
            {
                dto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
            }
            dto.INVOICE_KBN = 1;

            //伝票種類で取得データを判定
            switch (int.Parse(this.form.txtDenpyouShurui.Text))
            {
                case 2:
                    //受入データ
                    if (GetUkeireData(dto))
                    {
                        ret = true;
                    }
                    break;
                case 3:
                    //出荷データ
                    if (GetSyukkaData(dto))
                    {
                        ret = true;
                    }
                    break;
                case 4:
                    //売上/支払データ
                    if (GetUriageShiharaiData(dto))
                    {
                        ret = true;
                    }
                    break;
                case 1:
                    //受入データ、出荷データ、売上/支払データ
                    if (GetUkeireData(dto) || GetSyukkaData(dto) || GetUriageShiharaiData(dto))
                    {
                        ret = true;
                    }
                    break;
            }
            if (!ret && GetNyuukinData(dto))
            {
                ret = true;
            }
            return ret;
        }

        //受入データ取得(期間)
        public bool GetUkeireData(SeikyuShimeShoriDto dto)
        {
            DataTable result;
            dto.DENPYO_SHURUI = 2;
            result = shimeShoriDao.GetUriageDataKikanForEntity(dto);
            if (result != null && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        // 出荷データ取得(期間)
        public bool GetSyukkaData(SeikyuShimeShoriDto dto)
        {
            DataTable result;
            dto.DENPYO_SHURUI = 3;
            result = shimeShoriDao.GetUriageDataKikanForEntity(dto);
            if (result != null && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        //売上 / 支払データ(期間)
        public bool GetUriageShiharaiData(SeikyuShimeShoriDto dto)
        {
            DataTable result;
            dto.DENPYO_SHURUI = 4;
            result = shimeShoriDao.GetUriageDataKikanForEntity(dto);
            if (result != null && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        //入金データ取得(期間)
        public bool GetNyuukinData(SeikyuShimeShoriDto dto)
        {
            DataTable result;
            result = shimeShoriDao.GetShukkinDataKikanForEntity(dto);
            if (result != null && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 未入力チェック処理

        /// <summary>
        /// 未入力チェック処理
        /// </summary>
        /// <returns></returns>
        private bool CheckInput(object sender)
        {
            LogUtility.DebugMethodStart(sender);

            bool result = true;
            if (!this.shimeChangeFlag)
            {
                #region 期間締め処理

                //拠点CD
                if (string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点CD");
                    result = false;
                    //フォーカス設定
                    this.form.txtKyotenCd.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.txtDenpyouShurui.Text))
                {
                    //伝票種類
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "伝票種類");
                    result = false;
                    //フォーカス設定
                    this.form.txtDenpyouShurui.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                {
                    //締日
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "締日");
                    result = false;
                    //フォーカス設定
                    this.form.cmbShimebi.Focus();
                }
                else if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    //締日の設定が0の場合⇒請求締日FROM-TO
                    if (string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", "支払締日");
                        result = false;
                        //フォーカス設定
                        this.form.dtpSearchDateFrom.Focus();
                    }
                    else if (string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", "支払締日");
                        result = false;
                        //フォーカス設定
                        this.form.dtpSearchDateTo.Focus();
                    }
                    else
                    {
                        //請求締日FROM-TOの逆転チェック
                        string date1 = this.form.dtpSearchDateFrom.Text;
                        string date2 = this.form.dtpSearchDateTo.Text;
                        DateTime input1 = DateTime.Parse(date1);
                        DateTime input2 = DateTime.Parse(date2);

                        if (input2 < input1)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            string[] errorMsg = { "支払締日From", "支払締日To" };
                            msgLogic.MessageBoxShow("E030", errorMsg);
                            result = false;
                        }
                    }
                }
                else if (!"0".Equals(this.form.cmbShimebi.Text))
                {
                    //締日の設定が0以外の場合⇒請求締日FROM-TO
                    if (string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", "支払締日");
                        result = false;
                        //フォーカス設定
                        this.form.dtpSearchDateFrom.Focus();
                    }
                }

                if (result && string.IsNullOrEmpty(this.form.HYOUJI_JOUKEN_KBN.Text))
                {
                    //締日
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "表示条件");
                    result = false;
                    //フォーカス設定
                    this.form.HYOUJI_JOUKEN_KBN.Focus();
                }
                #endregion
            }
            else
            {
                #region 伝票・明細締め処理

                //拠点CD
                if (string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点CD");
                    result = false;
                    //フォーカス設定
                    this.form.txtKyotenCd.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.txtDenpyouShurui.Text))
                {
                    //伝票種類
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "伝票種類");
                    result = false;
                    //フォーカス設定
                    this.form.txtDenpyouShurui.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.cmbShimebi.Text))
                {
                    //締日
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "締日");
                    result = false;
                    //フォーカス設定
                    this.form.cmbShimebi.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text))
                {
                    //取引先CD
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "取引先");
                    result = false;
                    //フォーカス設定
                    this.form.txtTorihikisakiCd.Focus();
                }
                //160017 S
                //else if (string.IsNullOrWhiteSpace(this.form.dtpSearchDateFrom.Text)
                //              || string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                //{
                //    //請求締日FROM
                //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //    msgLogic.MessageBoxShow("E001", "検索期間");
                //    result = false;
                //    //フォーカス設定
                //    this.form.dtpSearchDateFrom.Focus();
                //}
                //160017 E
                else if (string.IsNullOrWhiteSpace(this.form.dtpSearchDateTo.Text)
                          || string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                {
                    //請求締日TO
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "検索期間");
                    result = false;
                    //フォーカス設定
                    this.form.dtpSearchDateTo.Focus();
                }
                //160017 S
                else if (sender == this.parentForm.bt_func9
                    && (string.IsNullOrWhiteSpace(this.form.SHUKKIN_YOUTEI_BI.Text)
                     || string.IsNullOrEmpty(this.form.SHUKKIN_YOUTEI_BI.Text)))
                {
                    //出金予定日
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "出金予定日");
                    result = false;
                    //フォーカス設定
                    this.form.SHUKKIN_YOUTEI_BI.Focus();
                }                
                else if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text)
                    && !string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                {
                //160017 E
                    //請求締日FROM-TOの逆転チェック
                    string date1 = this.form.dtpSearchDateFrom.Text;
                    string date2 = this.form.dtpSearchDateTo.Text;
                    DateTime input1 = DateTime.Parse(date1);
                    DateTime input2 = DateTime.Parse(date2);

                    if (input2 < input1)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string[] errorMsg = { "検索期間From", "検索期間To" };
                        msgLogic.MessageBoxShow("E030", errorMsg);
                        result = false;
                    }
                }

                #endregion
            }

            LogUtility.DebugMethodEnd();

            return result;
        }

        #endregion

        #region DataGrigView(チェックボックス)0件チェック処理

        /// <summary>
        /// DataGrigView(チェックボックス)チェック処理
        /// </summary>
        /// <returns></returns>
        private bool CheckDGVCheck()
        {
            LogUtility.DebugMethodStart();

            bool result = true;
            int checkCount = 0;
            int rowCount = this.form.customDataGridView1.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                if (bool.Parse(this.form.customDataGridView1.Rows[i].Cells[0].Value.ToString()))
                {
                    checkCount += 1;
                }
            }

            if (checkCount == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //チェック件数0件の場合、メッセージ出力
                msgLogic.MessageBoxShow("E034", "締対象のデータ");
                result = false;
                //フォーカス設定
                this.form.txtKyotenCd.Focus();
            }

            LogUtility.DebugMethodEnd();

            return result;
        }

        #endregion

        #region DGVでのチェックボックス制御処理(チェック可能件数 締日=0⇒1件：締日!=0⇒複数件)

        /// <summary>
        /// DGVでのチェックボックス制御処理
        /// (チェック可能件数 締日=0⇒1件：締日!=0⇒複数件)
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="System.NotImplementedException"></exception>
        internal bool ControlCheckDgv(object sender, DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (-1 != e.RowIndex)
                {
                    // 他にチェックされている項目がある場合はそのチェックを変更しない
                    for (int rowIndex = 0; rowIndex < this.form.customDataGridView1.Rows.Count; rowIndex++)
                    {
                        if ((rowIndex != e.RowIndex) && ((bool)this.form.customDataGridView1[0, rowIndex].Value == true))
                        {
                            // チェックを変更しない(trueのまま)
                            this.form.customDataGridView1[0, rowIndex].Value = true;
                            // ReadOnlyを解除
                            this.form.customDataGridView1[0, rowIndex].ReadOnly = false;
                        }
                    }
                    // 合計金額を表示
                    if (!CalcGoukeiKingaku())
                    {
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("controlcheckdgv", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            //this.form.customDataGridView1.Refresh();
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 合計金額を計算

        /// <summary>
        /// 合計金額を計算
        /// </summary>
        internal bool CalcGoukeiKingaku()
        {
            bool ret = true;
            try
            {
                // 合計金額
                decimal goukeiKingaku = 0;
                decimal goukeiKingakuShukkin = 0;
                for (int rowIndex = 0; rowIndex < this.form.customDataGridView1.Rows.Count; rowIndex++)
                {
                    // 期間締め以外の場合はチェックされている行の金額を加算し合計金額を表示する
                    if (this.shimeChangeFlag)
                    {
                        if ((bool)this.form.customDataGridView1[0, rowIndex].EditedFormattedValue)
                        {
                            decimal kingaku = 0;
                            if (decimal.TryParse(this.form.customDataGridView1.Rows[rowIndex].Cells["KINGAKU"].Value.ToString(), out kingaku))
                            {
                                if (String.Equals(denshuName_Shukkin, Convert.ToString(this.form.customDataGridView1.Rows[rowIndex].Cells["DENPYOUSHURUI"].Value)))
                                {
                                    goukeiKingakuShukkin += kingaku;
                                }
                                else
                                {
                                    goukeiKingaku += kingaku;
                                }
                            }
                        }
                    }
                }

                // 合計金額を表示
                string format = "#,##0";
                this.form.txtGoukeikingaku.Text = string.Format("{0:" + format + "}", goukeiKingaku);
                this.form.tb_goukeikingaku_shukkin.Text = string.Format("{0:" + format + "}", goukeiKingakuShukkin);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalcGoukeiKingaku", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 支払締日の設定処理(期間締めの場合に使用)

        /// <summary>
        /// 支払締日の設定
        /// </summary>
        /// <param name="dtToday">dtToday</param>
        /// <param name="lastday">前月締日を表す文字列</param>
        /// <param name="today">当月締日を表す文字列</param>
        /// <param name="strTodayDate">システム日付(dd)を表す文字列</param>
        private void SetSeikyuShimebi(DateTime dtToday, ref string lastday, ref string today, string strTodayDate)
        {
            LogUtility.DebugMethodStart(dtToday, lastday, today, strTodayDate);

            DateTime dtLast;
            if (int.Parse(this.form.cmbShimebi.Text) >= int.Parse(strTodayDate))
            {
                //前月
                //前月締日の取得
                if ("31".Equals(this.form.cmbShimebi.Text))
                {
                    //前月末日の設定
                    dtLast = new DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1);
                    lastday = dtLast.ToString("yyyy/MM/dd");
                }
                else
                {
                    dtLast = new DateTime(dtToday.Year, dtToday.Month, int.Parse(this.form.cmbShimebi.Text)).AddMonths(-1);
                }

                lastday = dtLast.ToString("yyyy/MM/dd");
                //支払締日Fromに前月末日設定
                this.form.dtpSearchDateFrom.SetResultText(lastday);
            }
            else
            {
                //当月
                //当月締日の取得
                today = dtToday.ToString("yyyy/MM/" + this.form.cmbShimebi.Text);
                //支払締日Fromに当月末日設定
                this.form.dtpSearchDateFrom.SetResultText(today);
            }

            //支払締日2と波線を非表示
            this.form.lb_hasen.Visible = false;
            this.form.dtpSearchDateTo.Visible = false;
            this.form.dtpSearchDateTo.Enabled = false;
            //編集不可に設定
            this.form.dtpSearchDateFrom.Enabled = false;

            LogUtility.DebugMethodEnd(dtToday, lastday, today, strTodayDate);
        }

        #endregion

        #region ヘッダタイトルの設定

        /// <summary>
        /// ヘッダタイトルの設定を実行する
        /// </summary>
        internal void SetHeaderTitle()
        {
            LogUtility.DebugMethodStart();

            if (!this.shimeChangeFlag)
            {
                this.headerForm.lb_title.Text = "期間支払締処理";
            }
            else
            {
                this.headerForm.lb_title.Text = "伝票支払締処理";
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region HeaderFormに設定する値取得(拠点)

        /// <summary>
        /// HeaderFormに設定する値取得(拠点)
        /// </summary>
        /// /// <returns>hs</returns>
        internal bool GetHeaderInfo(UIHeader hs)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(hs);

                // ユーザ拠点名称の取得
                if (!string.IsNullOrEmpty(this.headerForm.txtKyotenCDHeader.Text))
                {
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headerForm.txtKyotenCDHeader.Text);
                    if (mKyoten == null)
                    {
                        hs.txtKyotenNameHeader.Text = "";
                        hs.txtKyotenCDHeader.Text = "";
                    }
                    else
                    {
                        hs.txtKyotenNameHeader.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getheaderinfo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getheaderinfo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 締日(プルダウン値)・請求締日の設定(初期表示)

        /// <summary>
        /// 締日(プルダウン値)・請求締日の設定処理
        /// </summary>
        internal void SetShimebi()
        {
            LogUtility.DebugMethodStart();

            DateTime dtToday = parentForm.sysDate;
            DateTime dtLast;
            string lastday = string.Empty;
            string today = string.Empty;
            string strTodayDate = string.Empty;
            strTodayDate = "0";

            if ("0".Equals(this.form.cmbShimebi.Text))
            {
                //締日(0)の場合
                strTodayDate = this.form.cmbShimebi.Text;
            }
            else
            {
                strTodayDate = parentForm.sysDate.ToString("dd");
            }

            // 当日日付の直前の締日に設定
            // 当日日付
            var toDay = parentForm.sysDate;
            var toDayDate = toDay.Day;

            // 当月月末日
            var endOfTheMonth = new DateTime(toDay.Year, toDay.Month, DateTime.DaysInMonth(toDay.Year, toDay.Month));

            #region 締日・請求締日の設定-振分け

            if (!this.shimeChangeFlag)
            {
                #region 期間締めの場合

                if (0 == int.Parse(strTodayDate))
                {
                    //締日の設定
                    this.form.cmbShimebi.Text = "0";

                    //請求締日Fromに当月当日設定
                    this.form.dtpSearchDateFrom.SetResultText(dtToday.ToString("yyyy/MM/dd"));
                    //請求締日Toに当月当日設定
                    this.form.dtpSearchDateTo.SetResultText(dtToday.ToString("yyyy/MM/dd"));
                    this.form.dtpSearchDateFrom.Enabled = true;
                    this.form.dtpSearchDateTo.Enabled = true;
                }
                else if (toDayDate == endOfTheMonth.Day || toDayDate >= 1 && toDayDate <= 4)
                {
                    //締日の設定(31)
                    this.form.cmbShimebi.Text = "31";

                    //前月末日の取得
                    dtLast = new DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1);
                    lastday = dtLast.ToString("yyyy/MM/dd");
                    //請求締日Fromに前月末日設定
                    this.form.dtpSearchDateFrom.SetResultText(lastday);
                    //請求締日Toと波線を非表示
                    this.form.lb_hasen.Visible = false;
                    this.form.dtpSearchDateTo.Visible = false;
                    this.form.dtpSearchDateTo.Enabled = false;
                    //編集不可に設定
                    this.form.dtpSearchDateFrom.Enabled = false;
                }
                else if (toDayDate >= 5 && toDayDate <= 9)
                {
                    //締日の設定(5)
                    this.form.cmbShimebi.Text = "5";

                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }
                else if (toDayDate >= 10 && toDayDate <= 14)
                {
                    //締日の設定(10)
                    this.form.cmbShimebi.Text = "10";

                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }
                else if (toDayDate >= 15 && toDayDate <= 19)
                {
                    //締日の設定(15)
                    this.form.cmbShimebi.Text = "15";

                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }
                else if (toDayDate >= 20 && toDayDate <= 24)
                {
                    //締日の設定(20)
                    this.form.cmbShimebi.Text = "20";

                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }
                else if (toDayDate >= 25 && toDayDate <= endOfTheMonth.Day - 1)
                {
                    //締日の設定(25)
                    this.form.cmbShimebi.Text = "25";

                    this.SetSeikyuShimebi(dtToday, ref lastday, ref today, strTodayDate);
                }


                #endregion
            }
            else 
            {
                #region 伝票締め 又は 明細締め の場合

                //請求締日Toと波線を表示
                this.form.lb_hasen.Visible = true;
                this.form.dtpSearchDateTo.Visible = true;
                this.form.dtpSearchDateTo.Enabled = true;
                //編集可に設定
                this.form.dtpSearchDateFrom.Enabled = true;
                this.form.dtpSearchDateFrom.SetResultText(string.Empty);//160017
                if (0 == int.Parse(strTodayDate))
                {
                    //締日の設定
                    this.form.cmbShimebi.Text = "0";
                }
                else if (toDayDate == endOfTheMonth.Day || toDayDate >= 1 && toDayDate <= 4)
                {
                    //締日の設定(31)
                    this.form.cmbShimebi.Text = "31";
                }
                else if (toDayDate >= 5 && toDayDate <= 9)
                {
                    //締日の設定(5)
                    this.form.cmbShimebi.Text = "5";
                }
                else if (toDayDate >= 10 && toDayDate <= 14)
                {
                    //締日の設定(10)
                    this.form.cmbShimebi.Text = "10";
                }
                else if (toDayDate >= 15 && toDayDate <= 19)
                {
                    //締日の設定(15)
                    this.form.cmbShimebi.Text = "15";
                }
                else if (toDayDate >= 20 && toDayDate <= 24)
                {
                    //締日の設定(20)
                    this.form.cmbShimebi.Text = "20";
                }
                else if (toDayDate >= 25 && toDayDate <= endOfTheMonth.Day - 1)
                {
                    //締日の設定(25)
                    this.form.cmbShimebi.Text = "25";
                }

                //請求締日Fromに当月当日設定
                //this.form.dtpSearchDateFrom.SetResultText(dtToday.ToString("yyyy/MM/dd"));160017
                //請求締日Toに当月当日設定
                this.form.dtpSearchDateTo.SetResultText(dtToday.ToString("yyyy/MM/dd"));

                #endregion
            }

            #endregion

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region カンマ編集

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        #endregion カンマ編集

        //#region 検索実行時条件を保存

        ///// <summary>
        ///// 検索条件を保存する
        ///// </summary>
        //private void SaveSearchCondition()
        //{
        //    beforeDenpyouShurui = this.form.txtDenpyouShurui.Text;
        //    beforeKyotenCd = this.form.txtKyotenCd.Text;
        //    beforeKyotenName = this.form.txtKyotenName.Text;
        //    beforeSeikyuShimebiFrom = this.form.dtpSearchDateFrom.Value.ToString();
        //    beforeSeikyuShimebiTo = this.form.dtpSearchDateTo.Value.ToString();
        //    beforeShimebi = this.form.cmbShimebi.SelectedIndex.ToString();
        //    beforeTorihikisakiCd = this.form.txtTorihikisakiCd.Text;
        //    beforeTorihikisakiName = this.form.txtTorihikisakiName.Text;
        //}

        //#endregion 検索実行時検索条件を保存

        //#region 検索実行時条件をクリア

        ///// <summary>
        ///// 検索条件をクリアする
        ///// </summary>
        //private void ClearSearchCondition()
        //{
        //    beforeDenpyouShurui = string.Empty;
        //    beforeKyotenCd = string.Empty;
        //    beforeKyotenName = string.Empty;
        //    beforeSeikyuShimebiFrom = string.Empty;
        //    beforeSeikyuShimebiTo = string.Empty;
        //    beforeShimebi = string.Empty;
        //    beforeTorihikisakiCd = string.Empty;
        //    beforeTorihikisakiName = string.Empty;
        //}

        //#endregion 検索実行時条件をクリア

        //#region 検索実行時条件チェック

        ///// <summary>
        ///// 検索条件が変更されたかチェックする
        ///// </summary>
        ///// <param name="controlName"></param>
        //private void CheckSearchCondition(string controlName)
        //{
        //    // ダイアログボックスの戻り値を示します
        //    // true = YES   false = NO
        //    bool popupResult = false;

        //    //検索実行前の場合は以降の処理を行わずに終了
        //    if (string.IsNullOrEmpty(beforeDenpyouShurui) &&
        //        string.IsNullOrEmpty(beforeKyotenCd) &&
        //        string.IsNullOrEmpty(beforeKyotenName) &&
        //        string.IsNullOrEmpty(beforeSeikyuShimebiFrom) &&
        //        string.IsNullOrEmpty(beforeSeikyuShimebiTo) &&
        //        string.IsNullOrEmpty(beforeShimebi) &&
        //        string.IsNullOrEmpty(beforeTorihikisakiCd) &&
        //        string.IsNullOrEmpty(beforeTorihikisakiName))
        //    {
        //        return;
        //    }

        //    bool isDenpyouShuruiChanged = false;
        //    bool isKyotenCdChanged = false;
        //    bool isSeikyuShimebiFromChanged = false;
        //    bool isSeikyuShimebiToChanged = false;
        //    bool isShimebiChanged = false;
        //    bool isTorihikisakiCdChanged = false;

        //    //日付のVALUE値NULLの場合変換
        //    string seikyuShimebiFrom = string.Empty;
        //    string seikyuShimebiTo = string.Empty;

        //    if (this.form.dtpSearchDateFrom.Value != null)
        //    {
        //        seikyuShimebiFrom = this.form.dtpSearchDateFrom.Value.ToString();
        //    }

        //    if (this.form.dtpSearchDateTo.Value != null)
        //    {
        //        seikyuShimebiTo = this.form.dtpSearchDateTo.Value.ToString();
        //    }

        //    //検索条件の値が変更になっているかチェック
        //    if (beforeDenpyouShurui != this.form.txtDenpyouShurui.Text)
        //    {
        //        isDenpyouShuruiChanged = true;
        //    }

        //    if (beforeKyotenCd != this.form.txtKyotenCd.Text)
        //    {
        //        isKyotenCdChanged = true;
        //    }

        //    if (beforeSeikyuShimebiFrom != seikyuShimebiFrom)
        //    {
        //        isSeikyuShimebiFromChanged = true;
        //    }

        //    if (beforeSeikyuShimebiTo != seikyuShimebiTo)
        //    {
        //        isSeikyuShimebiToChanged = true;
        //    }

        //    if (beforeShimebi != this.form.cmbShimebi.SelectedIndex.ToString())
        //    {
        //        isShimebiChanged = true;
        //    }

        //    if (beforeTorihikisakiCd != this.form.txtTorihikisakiCd.Text)
        //    {
        //        isTorihikisakiCdChanged = true;
        //    }

        //    //変更された項目があれば確認メッセージを表示
        //    if (isDenpyouShuruiChanged || isKyotenCdChanged || isSeikyuShimebiFromChanged ||
        //        isSeikyuShimebiToChanged || isShimebiChanged || isTorihikisakiCdChanged)
        //    {
        //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //        if (msgLogic.MessageBoxShow("C031") == DialogResult.Yes)
        //        {
        //            // ダイアログ ボックスの戻り値に:true(YES)を格納
        //            popupResult = true;

        //            //「はい」選択時はグリッドの表示と取引先情報をクリア
        //            this.form.customDataGridView1.Rows.Clear();
        //            this.form.txtTorihikisakiCd.Text = string.Empty;
        //            this.form.txtTorihikisakiName.Text = string.Empty;

        //            //検索条件を初期化
        //            ClearSearchCondition();

        //            //伝票締め、明細締めの場合で取引先CDのロストフォーカス時で締日が0の場合
        //            //日付FROMの値を更新
        //            if (this.shimeChangeFlag &&
        //                controlName == this.form.txtTorihikisakiCd.Name &&
        //                "0".Equals(this.form.cmbShimebi.Text))
        //            {
        //                this.SetSeisanDate();
        //            }
        //            else if (controlName == this.form.cmbShimebi.Name)
        //            {
        //                //取引先CDと取引先名をクリア
        //                this.form.txtTorihikisakiCd.Text = "";
        //                this.form.txtTorihikisakiName.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            // ダイアログ ボックスの戻り値に:false(NO)を格納
        //            popupResult = false;

        //            //「いいえ」選択時は入力内容を元に戻す
        //            if (isDenpyouShuruiChanged)
        //            {
        //                this.form.txtDenpyouShurui.Validated -= new EventHandler(txtDenpyouShurui_TextChanged);
        //                this.form.txtDenpyouShurui.Text = beforeDenpyouShurui;
        //                this.form.txtDenpyouShurui.Validated += new EventHandler(txtDenpyouShurui_TextChanged);
        //            }
        //            if (isKyotenCdChanged)
        //            {
        //                this.form.txtKyotenCd.Text = beforeKyotenCd;
        //                this.form.txtKyotenName.Text = beforeKyotenName;
        //            }
        //            if (isShimebiChanged)
        //            {
        //                this.form.cmbShimebi.Validated -= new EventHandler(cmbShimebi_Validated);
        //                this.form.cmbShimebi.SelectedIndex = Convert.ToInt16(beforeShimebi);
        //                SetSearchDateState();
        //                this.form.cmbShimebi.Validated += new EventHandler(cmbShimebi_Validated);
        //            }
        //            if (isSeikyuShimebiFromChanged)
        //            {
        //                this.form.dtpSearchDateFrom.Value = DateTime.Parse(beforeSeikyuShimebiFrom);
        //            }
        //            if (isSeikyuShimebiToChanged)
        //            {
        //                this.form.dtpSearchDateTo.Value = DateTime.Parse(beforeSeikyuShimebiTo);
        //            }
        //            if (isTorihikisakiCdChanged)
        //            {
        //                this.form.txtTorihikisakiCd.Text = beforeTorihikisakiCd;
        //                this.form.txtTorihikisakiName.Text = beforeTorihikisakiName;
        //            }
        //        }

        //        //フォーカス設定
        //        if (controlName == this.form.txtDenpyouShurui.Name)
        //        {
        //            this.form.txtDenpyouShurui.Focus();
        //        }
        //        else if (controlName == this.form.txtKyotenCd.Name)
        //        {
        //            if (!popupResult)
        //            {
        //                this.form.txtKyotenCd.Focus();
        //            }
        //        }
        //        else if (controlName == this.form.dtpSearchDateFrom.Name)
        //        {
        //            this.form.dtpSearchDateFrom.Focus();
        //        }
        //        else if (controlName == this.form.dtpSearchDateTo.Name)
        //        {
        //            this.form.dtpSearchDateTo.Focus();
        //        }
        //        else if (controlName == this.form.cmbShimebi.Name)
        //        {
        //            this.form.cmbShimebi.Focus();
        //        }
        //        else
        //        {
        //            this.form.txtTorihikisakiCd.Focus();
        //        }
        //    }
        //    else
        //    {
        //        //変更がなければ処理終了
        //        return;
        //    }
        //}

        //#endregion 検索実行時条件チェック

        //#region グリッドクリアチェック

        ///// <summary>
        ///// グリッドクリアを行うかチェックする
        ///// </summary>
        ///// <param name="controlName"></param>
        //private void CheckGridClear(string controlName)
        //{
        //    //日付のVALUE値NULLの場合変換
        //    string seikyuShimebiFrom = string.Empty;
        //    string seikyuShimebiTo = string.Empty;

        //    if (this.form.dtpSearchDateFrom.Value != null)
        //    {
        //        seikyuShimebiFrom = this.form.dtpSearchDateFrom.Value.ToString();
        //    }

        //    if (this.form.dtpSearchDateTo.Value != null)
        //    {
        //        seikyuShimebiTo = this.form.dtpSearchDateTo.Value.ToString();
        //    }

        //    if (controlName == this.form.txtDenpyouShurui.Name &&
        //        beforeDenpyouShurui != this.form.txtDenpyouShurui.Text)
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //    else if (controlName == this.form.cmbShimebi.Name &&
        //             beforeShimebi != this.form.cmbShimebi.SelectedIndex.ToString())
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //    else if (controlName == this.form.txtTorihikisakiCd.Name &&
        //             beforeTorihikisakiCd != this.form.txtTorihikisakiCd.Text)
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //    else if (controlName == this.form.dtpSearchDateFrom.Name &&
        //             beforeSeikyuShimebiFrom != seikyuShimebiFrom)
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //    else if (controlName == this.form.dtpSearchDateTo.Name &&
        //             beforeSeikyuShimebiTo != seikyuShimebiTo)
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //    else if (controlName == this.form.txtKyotenCd.Name &&
        //             beforeKyotenCd != this.form.txtKyotenCd.Text)
        //    {
        //        //明細クリア
        //        this.form.customDataGridView1.Rows.Clear();
        //    }
        //}

        //#endregion グリッドクリアチェック

        #region 締処理実行中チェック

        /// <summary>
        /// 締処理実行中チェック
        /// </summary>
        /// <returns name="bool">TRUE:実行中ではない, FALSE:締処理実行中</returns>
        private bool CheckShimeChuu()
        {
            var bRet = true;

            using (Transaction tran = new Transaction())
            {
                //他ユーザの処理状況チェック
                //締処理中テーブル(T_SHIME_SHORI_CHUU)
                foreach (CheckDto dto in checkDataList)
                {
                    string createPc = string.Empty;
                    string clientUserName = string.Empty;
                    string createUser = string.Empty;

                    DataTable shoriChuuEntitys = shimeShoriDao.CheckShimeShoriUserDataForEntity(dto);

                    if (shoriChuuEntitys != null && shoriChuuEntitys.Rows.Count > 0)
                    {
                        DataRow dr = shoriChuuEntitys.Rows[0];
                        createPc = dr["CLIENT_COMPUTER_NAME"].ToString();
                        clientUserName = dr["CLIENT_USER_NAME"].ToString();
                        createUser = dr["CREATE_USER"].ToString();

                        //1件でもあればメッセージを表示して処理キャンセル
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E248", createPc, clientUserName, createUser);
                        bRet = false;
                        break;
                    }
                }

                // 締処理実行可能状態の場合
                if (bRet)
                {
                    //締実行番号を取得
                    ShimeLogicClass ShimeJikkou = new ShimeLogicClass();
                    var shimeNo = ShimeJikkou.GetDensyuSaibanNo("40");

                    if (this.shimeDataList[0].SHIME_TANI == 1)
                    {
                        // 期間締処理の場合は全取引先に対して登録
                        foreach (var dto in this.shimeDataList)
                        {
                            // 引渡し用データに締実行番号をセット
                            dto.SHIME_JIKKOU_NO = shimeNo.Value;

                            //締処理中テーブルにレコードを追加
                            ShimeJikkou.InsertTShimeShoriChuu(dto, shimeNo);
                        }
                    }
                    else
                    {
                        // 引渡し用データに締実行番号をセット
                        this.shimeDataList[0].SHIME_JIKKOU_NO = shimeNo.Value;

                        // それ以外は単一取引先に対して登録
                        ShimeJikkou.InsertTShimeShoriChuu(this.shimeDataList[0], shimeNo);
                    }
                }

                // コミット
                tran.Commit();
            }

            return bRet;
        }

        #endregion 締処理実行中チェック

        #region 拠点によって取引先CDのポップアップ検索条件を変更します

        /// <summary>
        /// 拠点によって取引先CDのポップアップ検索条件を変更します
        /// </summary>
        /// <param name="allSearchFlg">全検索をするか判断します</param>
        internal void TorihikisakiSearchConditionsKyoten(bool allSearchFlg)
        {
            // 初期化
            this.form.txtTorihikisakiCd.PopupSearchSendParams.Clear();
            this.form.btnTorihikisakiPopup.PopupSearchSendParams.Clear();
            PopupSearchSendParamDto searchParam = new PopupSearchSendParamDto();

            if (!allSearchFlg)
            {
                // 拠点CDによって条件が変わるように設定
                PopupSearchSendParamDto subSearchParam1 = new PopupSearchSendParamDto();
                PopupSearchSendParamDto subSearchParam2 = new PopupSearchSendParamDto();
                // サブ条件
                subSearchParam1.And_Or = CONDITION_OPERATOR.AND;
                subSearchParam1.Control = "txtKyotenCd";
                subSearchParam1.KeyName = "TORIHIKISAKI_KYOTEN_CD";
                subSearchParam2.And_Or = CONDITION_OPERATOR.OR;
                subSearchParam2.KeyName = "TORIHIKISAKI_KYOTEN_CD";
                subSearchParam2.Value = "99";
                // メイン条件
                searchParam.And_Or = CONDITION_OPERATOR.AND;
                searchParam.SubCondition.Add(subSearchParam1);
                searchParam.SubCondition.Add(subSearchParam2);
            }

            // allSearchFlgがtrueの場合は拠点CDによる条件を外す
            // 取引先CDへ条件を反映
            this.form.txtTorihikisakiCd.PopupSearchSendParams.Add(searchParam);
            this.form.btnTorihikisakiPopup.PopupSearchSendParams.Add(searchParam);

            PopupSearchSendParamDto searchParam2 = new PopupSearchSendParamDto();
            if (this.form.dtpSearchDateTo.Visible)
            {
                // 拠点CDによって条件が変わるように設定
                PopupSearchSendParamDto subSearchParam3 = new PopupSearchSendParamDto();
                PopupSearchSendParamDto subSearchParam4 = new PopupSearchSendParamDto();
                // サブ条件
                subSearchParam3.And_Or = CONDITION_OPERATOR.AND;
                subSearchParam3.Control = "dtpSearchDateFrom,dtpSearchDateTo";
                subSearchParam3.KeyName = "TEKIYOU_BEGIN";
                // メイン条件
                searchParam2.And_Or = CONDITION_OPERATOR.AND;
                searchParam2.SubCondition.Add(subSearchParam3);
                searchParam2.SubCondition.Add(subSearchParam4);
                this.form.txtTorihikisakiCd.PopupWindowId = WINDOW_ID.T_SHIHARAI_SHIME;
                this.form.btnTorihikisakiPopup.PopupWindowId = WINDOW_ID.T_SHIHARAI_SHIME;
            }
            else
            {
                searchParam2.And_Or = CONDITION_OPERATOR.AND;
                searchParam2.Control = "dtpSearchDateFrom";
                searchParam2.KeyName = "TEKIYOU_BEGIN";
                this.form.txtTorihikisakiCd.PopupWindowId = WINDOW_ID.T_SHIHARAI_SHIME;
                this.form.btnTorihikisakiPopup.PopupWindowId = WINDOW_ID.T_SHIHARAI_SHIME;
            }
            this.form.txtTorihikisakiCd.PopupSearchSendParams.Add(searchParam2);
            this.form.btnTorihikisakiPopup.PopupSearchSendParams.Add(searchParam2);
        }

        #endregion 拠点によって取引先CDのポップアップ検索条件を変更します

        /// <summary>
        /// 取引先と拠点の関係をチェックします
        /// </summary>
        /// <param name="headerKyotenCd">拠点CD</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>チェック結果</returns>
        internal bool CheckTorihikisakiKyoten()
        {
            try
            {
                string torihikisakiCd = this.form.txtTorihikisakiCd.Text;
                //取引先が空だったらReturn
                if (string.Empty == torihikisakiCd)
                {
                    this.form.txtTorihikisakiName.Text = string.Empty;
                    return true;
                }

                //// 取引先の拠点をチェック
                //if (String.IsNullOrEmpty(KyotenCd))
                //{
                //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //    msgLogic.MessageBoxShow("E146");

                //    return false;
                //}
                var torihikisaki = this.torihikisakiDao.GetDataByCd(torihikisakiCd);
                if (torihikisaki != null)
                {
                    string strBegin = "0001/01/01 00:00:01";
                    string strEnd = "9999/12/31 23:59:59";
                    string dateFrom = this.parentForm.sysDate.Date.ToString();
                    string dateTo = this.parentForm.sysDate.Date.ToString();
                    if (this.shimeChangeFlag || this.form.cmbShimebi.Text == "0")
                    {
                        if (this.form.dtpSearchDateFrom.Value != null)
                        {
                            dateFrom = this.form.dtpSearchDateFrom.Value.ToString();
                        }
                        if (this.form.dtpSearchDateTo.Value != null)
                        {
                            dateTo = this.form.dtpSearchDateTo.Value.ToString();
                        }
                    }
                    else
                    {
                        if (this.form.dtpSearchDateFrom.Value != null)
                        {
                            dateFrom = this.form.dtpSearchDateFrom.Value.ToString();
                            dateTo = this.form.dtpSearchDateFrom.Value.ToString();
                        }
                    }

                    if (!torihikisaki.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = torihikisaki.TEKIYOU_BEGIN.Value.ToString();
                    }

                    if (!torihikisaki.TEKIYOU_END.IsNull)
                    {
                        strEnd = torihikisaki.TEKIYOU_END.Value.ToString();
                    }

                    //適用期間外の場合
                    if (strEnd.CompareTo(dateFrom) < 0 || strBegin.CompareTo(dateTo) > 0 || torihikisaki.DELETE_FLG.IsTrue)
                    {
                        var mishime = shimeShoriDao.CheckMishimeDate(torihikisaki.TORIHIKISAKI_CD);
                        if (mishime == null || mishime.Rows.Count == 0)
                        {
                            decimal zandaka = 0;
                            SeikyuShimeShoriDto data = new SeikyuShimeShoriDto();
                            data.SEIKYU_CD = torihikisaki.TORIHIKISAKI_CD;
                            data.SEIKYUSHIMEBI_TO = dateTo;
                            var zenkai = shimeShoriDao.GetZenkaiKurikosigakuDataKikanForEntity(data);
                            if (zenkai != null && zenkai.Rows.Count > 0)
                            {
                                decimal.TryParse(Convert.ToString(zenkai.Rows[0][0]), out zandaka);
                            }
                            else
                            {
                                var shiharai = shiharaiDao.GetDataByCd(torihikisaki.TORIHIKISAKI_CD);
                                if (shiharai != null && !shiharai.KAISHI_KAIKAKE_ZANDAKA.IsNull)
                                {
                                    zandaka = shiharai.KAISHI_KAIKAKE_ZANDAKA.Value;
                                }
                            }
                            if (zandaka == 0)
                            {
                                torihikisaki = null;
                            }
                        }
                    }
                }
                bool checkShimeiFlg = this.CheckShimebiByTorihikisaki(this.form.cmbShimebi.Text, this.form.txtTorihikisakiCd.Text);
                if (!checkShimeiFlg)
                {
                    torihikisaki = null;
                }
                if (torihikisaki == null)
                {
                    // 取引先名設定
                    this.form.txtTorihikisakiName.Text = String.Empty;
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "取引先");
                    this.inputErrorFlg = true;
                    return false;
                }
                else
                {
                    this.form.txtTorihikisakiName.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                }

                //if (KyotenCd != "99")
                //{
                //    var kyotenCd = (int)torihikisaki.TORIHIKISAKI_KYOTEN_CD;
                //    if (99 != kyotenCd && Convert.ToInt16(KyotenCd) != kyotenCd)
                //    {
                //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //        msgLogic.MessageBoxShow("E146");

                //        return false;
                //    }
                //}
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckTorihikisakiKyoten", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void SetHeaderForm(UIHeader hs)
        {
            this.headerForm = hs;
        }

        // 20141201 Houkakou 「支払締処理」のダブルクリックを追加する start

        #region ダブルクリック時にFrom項目の入力内容をコピーする

        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSearchDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.dtpSearchDateFrom;
            var ToTextBox = this.form.dtpSearchDateTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // 20141201 Houkakou 「支払締処理」のダブルクリックを追加する end

        //VAN 20210502 #148578 S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcGoukeiKingaku();
        }
        //VAN 20210502 #148578 E

        #region ProcessOfValueChange
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_DenpyouShurui_CheckedChanged(object sender, EventArgs e)
        {
            var ctr = sender as CustomRadioButton;
            if (ctr.Checked && this.form.customDataGridView1.Rows.Count > 0)
            {
                this.inputErrorFlg = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Validated(object sender, EventArgs e)
        {
            var ctr = sender as Control;
            //160017 S
            bool setShukkinYouteiFlg = false;
            bool confirmFlg = true;
            //出金予定日
            if (this.IsValueChanged(ctr) && (ctr == this.form.txtTorihikisakiCd || ctr == this.form.dtpSearchDateTo))
            {
                setShukkinYouteiFlg = true;
            }
            //160017 E
            if (this.IsValueChanged(ctr) && this.form.customDataGridView1.Rows.Count > 0)
            {
                confirmFlg = this.ConfirmValueChanged(ctr);
                if (confirmFlg && ctr == this.form.cmbShimebi)
                {
                    bool checkShimeiFlg = this.CheckShimebiByTorihikisaki(this.form.cmbShimebi.Text, this.form.txtTorihikisakiName.Text);
                    if (!checkShimeiFlg)
                    {
                        this.form.txtTorihikisakiCd.Text = string.Empty;
                        this.form.txtTorihikisakiName.Text = string.Empty;
                    }
                    this.SetSearchDateState();
                    this.SaveDicPrevValue();
                }
            }
            else if (ctr == this.form.cmbShimebi && this.IsValueChanged(ctr)
                && this.form.customDataGridView1.Rows.Count == 0)
            {
                bool checkShimeiFlg = this.CheckShimebiByTorihikisaki(this.form.cmbShimebi.Text, this.form.txtTorihikisakiName.Text);
                if (!checkShimeiFlg)
                {
                    this.form.txtTorihikisakiCd.Text = string.Empty;
                    this.form.txtTorihikisakiName.Text = string.Empty;
                }
                this.SetSearchDateState();
                this.SaveDicPrevValue();
            }
            //160017 S
            if (setShukkinYouteiFlg && confirmFlg)
            {
                this.SetShukkinYouteibi();
            }
            //160017 E
            this.inputErrorFlg = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            if (!this.inputErrorFlg)
            {
                var ctr = sender as Control;
                this.dicPrevValue[ctr] = ctr.Text;
                if (ctr == this.form.txtKyotenCd)
                {
                    this.dicPrevValue[this.form.txtKyotenName] = this.form.txtKyotenName.Text;
                }
                else if (ctr == this.form.txtTorihikisakiCd)
                {
                    this.dicPrevValue[this.form.txtTorihikisakiName] = this.form.txtTorihikisakiName.Text;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        /// <returns></returns>
        private bool IsValueChanged(Control ctr)
        {
            if (this.dicPrevValue[ctr] != ctr.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        private bool ConfirmValueChanged(Control ctr)
        {
            if (this.msgLogic.MessageBoxShow("C031") == DialogResult.Yes)
            {
                this.form.customDataGridView1.Rows.Clear();
                this.form.txtGoukeikingaku.Text = "0";
                this.form.tb_goukeikingaku_shukkin.Text = "0";
                return true;
            }
            else
            {
                ctr.Text = this.dicPrevValue[ctr];
                if (ctr == this.form.txtKyotenCd)
                {
                    this.form.txtKyotenName.Text = this.dicPrevValue[this.form.txtKyotenName];
                }
                else if (ctr == this.form.txtTorihikisakiCd)
                {
                    this.form.txtTorihikisakiName.Text = this.dicPrevValue[this.form.txtTorihikisakiName];
                }
                else if (ctr == this.form.dtpSearchDateFrom)
                {
                    this.form.dtpSearchDateTo.Text = this.dicPrevValue[this.form.dtpSearchDateTo];
                }
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shimebi"></param>
        /// <param name="seikyuCd"></param>
        /// <returns></returns>
        internal bool CheckShimebiByTorihikisaki(string shimebi, string seikyuCd)
        {
            bool ret = false;
            var entity = this.shiharaiDao.GetDataByCd(seikyuCd);
            if (entity != null)
            {
                //QN_QUAN add 20220509 #162927 E
                if (this.shimeChangeFlag)
                {
                    if (entity.TORIHIKI_KBN_CD == 2)
                    {
                        return true;
                    }
                }
                else
                {
                    //QN_QUAN add 20220509 #162927 E
                    if (Convert.ToString(entity.SHIMEBI1) == shimebi
                        || Convert.ToString(entity.SHIMEBI2) == shimebi
                        || Convert.ToString(entity.SHIMEBI3) == shimebi)
                    {
                        return true;
                    }
                }
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// INXS支払明細書発行処理を実行する refs #158003
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void bt_process6_Click(object sender, EventArgs e)
        {
            string date;
            if (!this.shimeChangeFlag)
            {
                if ("0".Equals(this.form.cmbShimebi.Text))
                {
                    // 期間締め - 0日締め時はTo
                    date = this.form.dtpSearchDateTo.Text;
                }
                else
                {
                    // 期間締め - 0日締め以外はFrom
                    date = this.form.dtpSearchDateFrom.Text;
                }
            }
            else
            {
                // 伝票or明細はTo
                date = this.form.dtpSearchDateTo.Text;
            }

            FormManager.OpenFormWithAuth("G747", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.form.txtKyotenCd.Text, this.form.cmbShimebi.Text, this.form.txtTorihikisakiCd.Text, date);
        }
        //160017 S
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CheckShukkinYoteiBi()
        {
            bool result = true;
            var seikyuuDateCtl = this.form.dtpSearchDateTo;
            string displayItemName = "検索期間To";
            if (this.shimeChangeFlag)
            {
                if (!string.IsNullOrEmpty(seikyuuDateCtl.Text)
                        && !string.IsNullOrEmpty(this.form.SHUKKIN_YOUTEI_BI.Text))
                {
                    //請求締日FROM-TOの逆転チェック
                    string date1 = seikyuuDateCtl.Text;
                    string date2 = this.form.SHUKKIN_YOUTEI_BI.Text;
                    DateTime input1 = DateTime.Parse(date1);
                    DateTime input2 = DateTime.Parse(date2);

                    if (input2.CompareTo(input1) < 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        string[] errorMsg = { displayItemName, "出金予定日" };
                        msgLogic.MessageBoxShow("E030", errorMsg);
                        this.form.SHUKKIN_YOUTEI_BI.Focus();
                        result = false;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.BARCODE_KBN = this.headerForm.BARCODE_KBN.Text;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dt_seikyushimebi2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.inputErrorFlg = false;
            if (this.shimeChangeFlag && string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
            {
                this.form.dtpSearchDateTo.IsInputErrorOccured = true;
                this.msgLogic.MessageBoxShow("E001", "終了日");
                e.Cancel = true;
                this.inputErrorFlg = true;
            }
        }
        #region バーコード
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal bool IsBarcodeReadingMode()
        {
            if (this.shimeChangeFlag && this.headerForm.BARCODE_KBN.Text == "1")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BARCODE_KBN_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as CustomTextBox;
            var barcodeFlg = this.IsBarcodeReadingMode();
            //1.オン
            if (txt.Text == "1")
            {
                this.form.customDataGridView1.Rows.Clear();
                this.form.txtDenpyouShurui.Text = "1";
                this.form.BARCODE_NUMBER.Enabled = true;
                this.form.checkBoxAll.Checked = false;
            }
            //2.オフ
            else if (txt.Text == "2")
            {
                this.form.customDataGridView1.Rows.Clear();
                this.form.BARCODE_NUMBER.Enabled = false;
                this.form.checkBoxAll.Checked = false;
            }
            this.parentForm.bt_func5.Enabled = barcodeFlg;
            this.parentForm.bt_func8.Enabled = !barcodeFlg;
            this.form.pnlDenpyouShurui.Enabled = !barcodeFlg;
        }
        /// <summary>
        /// バーコードを読んで伝票を検索する 
        /// </summary>
        private void SearchDenpyou()
        {
            if (string.IsNullOrEmpty(this.form.txtKyotenCd.Text))
            {
                this.form.txtKyotenCd.IsInputErrorOccured = true;
                this.msgLogic.MessageBoxShow("E001", this.form.txtKyotenCd.DisplayItemName);
                this.form.txtKyotenCd.Focus();
                this.form.BARCODE_NUMBER.Text = string.Empty;
                return;
            }
            if (!string.IsNullOrEmpty(this.form.BARCODE_NUMBER.Text))
            {
                string shuruiName = string.Empty;
                DataTable dtResult = new DataTable();

                string barcode = this.form.BARCODE_NUMBER.Text;
                string denpyouShuruiCd = barcode.Substring(0, 1);
                string sDenpyouNumber = barcode.Remove(0, 1);
                Int64 denpyouNumber = 0;
                if (Int64.TryParse(sDenpyouNumber, out denpyouNumber))
                {
                    ShiharaiShimeShoriDispDto dto = new ShiharaiShimeShoriDispDto();
                    //検索期間From
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateFrom.Text))
                    {
                        dto.SHIHARAISHIMEBI_FROM = DateTime.Parse(this.form.dtpSearchDateFrom.Text).ToString("yyyy/MM/dd");
                    }
                    //検索期間To
                    if (!string.IsNullOrEmpty(this.form.dtpSearchDateTo.Text))
                    {
                        dto.SHIHARAISHIMEBI_TO = DateTime.Parse(this.form.dtpSearchDateTo.Text).ToString("yyyy/MM/dd");
                    }
                    //取引先CD
                    if (!string.IsNullOrEmpty(this.form.txtTorihikisakiCd.Text)
                        && this.form.customDataGridView1.Rows.Count > 0)
                    {
                        dto.SHIHARAI_CD = this.form.txtTorihikisakiCd.Text;
                    }
                    //拠点cd
                    dto.KYOTEN_CD = int.Parse(this.form.txtKyotenCd.Text);
                    //伝票番号
                    dto.DENPYOU_NUMBER = denpyouNumber;

                    switch (denpyouShuruiCd)
                    {
                        case "1":
                            shuruiName = denshuName_Ukeire;
                            dtResult = shimeShoriDao.BarcodeGetUkeireData(dto);
                            break;
                        case "2":
                            shuruiName = denshuName_Shukka;
                            dtResult = shimeShoriDao.BarcodeGetShukkaData(dto);
                            break;
                        case "3":
                            shuruiName = denshuName_UrSh;
                            dtResult = shimeShoriDao.BarcodeGetUriageData(dto);
                            break;
                    }

                }
                if (dtResult.Rows.Count > 0)
                {
                    DataRow searchRow = dtResult.Rows[0];
                    if (this.form.customDataGridView1.Rows.Count == 0)
                    {
                        //全件制御用チェックボックスもtrueに設定
                        this.form.checkBoxAll.Checked = true;
                        this.form.checkBoxAll.Enabled = true;
                        //Set to search condition controls
                        DateTime toDate = this.shimeShoriDao.GetSystemDate(new ShiharaiShimeShoriDispDto()).Date;
                        //[検索期間](from)= システム日付の1ヶ月前の翌日
                        this.form.dtpSearchDateFrom.Value = toDate.AddMonths(-1).AddDays(1);
                        ////[検索期間](to)= システム日付
                        //this.form.dt_seikyushimebi2.Value = toDate;
                        //取引先
                        this.form.txtTorihikisakiCd.Text = searchRow["TORIHIKISAKI_CD"].ToString();
                        this.form.txtTorihikisakiName.Text = searchRow["TORIHIKISAKI_NAME_RYAKU"].ToString();
                        if (!string.IsNullOrEmpty(Convert.ToString(searchRow["SHIMEBI"])))
                        {
                            this.form.cmbShimebi.Text = searchRow["SHIMEBI"].ToString();
                        }
                        //出金予定日
                        this.SetShukkinYouteibi();
                    }
                    var detailRow = this.form.customDataGridView1.Rows.Cast<DataGridViewRow>()
                        .Where(r => r.Cells["DENPYOU_NUMBER"].Value.Equals(searchRow["DENPYOU_NUMBER"])
                                && r.Cells["DENPYOUSHURUI"].Value.Equals(shuruiName)).FirstOrDefault();
                    if (detailRow == null)
                    {
                        var index = this.form.customDataGridView1.Rows.Add();
                        detailRow = this.form.customDataGridView1.Rows[index];
                    }
                    //初期表示はチェックをtrueに設定
                    detailRow.Cells["CHECK_BOX"].Value = true;
                    //適格請求書対応(1.適格請求書の場合、締チェックはONにしない)
                    if (this.headerForm.INVOICE_KBN.Text == "1")
                    {
                        detailRow.Cells["CHECK_BOX"].Value = false;
                    }
                    DateTime dt;
                    string strDenpyouDate = string.Empty;
                    if (DateTime.TryParse(searchRow["DENPYOU_DATE"].ToString(), out dt))
                    {
                        strDenpyouDate = dt.ToString("yyyy/MM/dd");
                    }
                    string strShimebiDate = string.Empty;
                    if (DateTime.TryParse(searchRow["SEISAN_SHIMEBI"].ToString(), out dt))
                    {
                        strShimebiDate = dt.ToString("yyyy/MM/dd");
                    }
                    detailRow.Cells["DENPYOUSHURUI"].Value = shuruiName;
                    detailRow.Cells["DENPYOU_NUMBER"].Value = searchRow["DENPYOU_NUMBER"];
                    detailRow.Cells["DENPYOU_DATE"].Value = strDenpyouDate;
                    detailRow.Cells["SHIHARAI_SHIMEBI"].Value = strShimebiDate;
                    detailRow.Cells["GYOUSHA_NAME"].Value = searchRow["GYOUSHA_NAME"];
                    detailRow.Cells["GENBA_NAME"].Value = searchRow["GENBA_NAME"];
                    detailRow.Cells["KINGAKU"].Value = SetComma(searchRow["KINGAKU"].ToString());
                    detailRow.Cells["HINMEI_SOTO_ZEI_COUNT"].Value = searchRow["HINMEI_SOTO_ZEI_COUNT"];
                    detailRow.Cells["HINMEI_NASI_ZEI_COUNT"].Value = searchRow["HINMEI_NASI_ZEI_COUNT"];
                    detailRow.Cells["SHIHARAI_ZEI_KBN_CD"].Value = searchRow["SHIHARAI_ZEI_KBN_CD"];
                    if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_DENPYOU.ToString().Equals(searchRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    {
                        detailRow.Cells["ZEI_KEISAN_KBN_NAME"].Value = "伝票毎";
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_SEIKYUU.ToString().Equals(searchRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    {
                        detailRow.Cells["ZEI_KEISAN_KBN_NAME"].Value = "精算毎";
                    }
                    else if (Shougun.Core.Common.BusinessCommon.Const.CommonConst.ZEI_KEISAN_KBN_MEISAI.ToString().Equals(searchRow["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    {
                        detailRow.Cells["ZEI_KEISAN_KBN_NAME"].Value = "明細毎";
                    }                    
                    //ReadOnly(編集不可) = trueに設定
                    detailRow.Cells["DENPYOUSHURUI"].ReadOnly = true;
                    detailRow.Cells["DENPYOU_NUMBER"].ReadOnly = true;
                    detailRow.Cells["DENPYOU_DATE"].ReadOnly = true;
                    detailRow.Cells["SHIHARAI_SHIMEBI"].ReadOnly = true;
                    detailRow.Cells["GYOUSHA_NAME"].ReadOnly = true;
                    detailRow.Cells["GENBA_NAME"].ReadOnly = true;
                    detailRow.Cells["KINGAKU"].ReadOnly = true;
                    detailRow.Cells["ZEI_KEISAN_KBN_NAME"].ReadOnly = true;
                    detailRow.DataGridView.CurrentCell = detailRow.Cells["CHECK_BOX"];
                    // 合計金額再計算
                    this.CalcGoukeiKingaku();
                    //クリアバーコードテキスト 
                    this.form.BARCODE_NUMBER.Text = string.Empty;
                    this.form.BARCODE_NUMBER.Focus();
                    this.form.BARCODE_NUMBER.UpdateBackColor(true);
                }
                else
                {
                    this.form.BARCODE_NUMBER.IsInputErrorOccured = true;
                    this.form.BARCODE_NUMBER.UpdateBackColor(false);
                    this.msgLogic.MessageBoxShow("C001");

                    this.form.BARCODE_NUMBER.Text = string.Empty;
                    this.form.BARCODE_NUMBER.Focus();
                    this.form.BARCODE_NUMBER.UpdateBackColor(true);
                }
            }
        }
        /// <summary>
        /// 出金予定日を計算
        /// </summary>
        private void SetShukkinYouteibi()
        {
            if (this.shimeChangeFlag)
            {
                var torihikisakiShiharai = this.shiharaiDao.GetDataByCd(this.form.txtTorihikisakiCd.Text);
                if (torihikisakiShiharai != null)
                {
                    //「1.日にち指定」の場合、算出方法は標準の仕様に準拠する。
                    if (!torihikisakiShiharai.SHIHARAI_BETSU_KBN.IsNull
                        && torihikisakiShiharai.SHIHARAI_BETSU_KBN.Value == 1)
                    {
                        if (this.form.dtpSearchDateTo.Value != null)
                        {
                            DateTime seikyuSimebi = (DateTime)this.form.dtpSearchDateTo.Value;
                            var addMonth = torihikisakiShiharai.SHIHARAI_MONTH.Value - 1;
                            //月を加算
                            DateTime dtYoutei = seikyuSimebi.AddMonths(addMonth);
                            //末日取得
                            int day = DateTime.DaysInMonth(dtYoutei.Year, dtYoutei.Month);
                            //支払日が末日を超えていないか判定
                            DateTime rdt;
                            if (day < torihikisakiShiharai.SHIHARAI_DAY.Value)
                            {
                                rdt = new DateTime(dtYoutei.Year, dtYoutei.Month, day);
                            }
                            else
                            {
                                rdt = new DateTime(dtYoutei.Year, dtYoutei.Month, torihikisakiShiharai.SHIHARAI_DAY.Value);
                            }
                            this.form.SHUKKIN_YOUTEI_BI.Value = rdt;
                        }
                    }
                    //「2.○日後指定」の場合
                    else if (!torihikisakiShiharai.SHIHARAI_BETSU_KBN.IsNull
                        && torihikisakiShiharai.SHIHARAI_BETSU_KBN.Value == 2)
                    {
                        if (this.form.dtpSearchDateTo.Value != null
                            && !torihikisakiShiharai.SHIHARAI_BETSU_NICHIGO.IsNull)
                        {
                            DateTime rdt = (DateTime)this.form.dtpSearchDateTo.Value;
                            this.TryAddDay(torihikisakiShiharai.SHIHARAI_BETSU_NICHIGO.Value, ref rdt);
                            this.form.SHUKKIN_YOUTEI_BI.Value = rdt;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        /// <param name="rdt"></param>
        /// <returns></returns>
        private bool TryAddDay(int days, ref DateTime rdt)
        {
            try
            {
                rdt = rdt.AddDays(days);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BARCODE_NUMBER_Validated(object sender, EventArgs e)
        {
            this.SearchDenpyou();
        }
        /// <summary>
        /// [F5]ﾌｫｰｶｽ設定
        /// 抽出条件[伝票番号]にフォーカスをセットする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.shimeChangeFlag
                && this.form.BARCODE_NUMBER.Visible
                && this.form.BARCODE_NUMBER.Enabled
                && !this.form.BARCODE_NUMBER.ReadOnly)
            {
                this.form.BARCODE_NUMBER.Focus();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }
        /// <summary>
        /// 【入金入力（取引先）】画面を起動する。
        /// 実行⇒現金入金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.bt_func9_Click(sender, e);
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 【入金入力（取引先）】画面を起動する。
        /// 実行⇒振込入金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.bt_func9_Click(sender, e);
            LogUtility.DebugMethodEnd();
        }
        #endregion
        //160017 E


        private void TorihikisakiSearchConditionsShimei()
        {
            this.form.txtTorihikisakiCd.popupWindowSetting.Clear();
            r_framework.Dto.JoinMethodDto dtowhere = new r_framework.Dto.JoinMethodDto();
            r_framework.Dto.SearchConditionsDto serdto = new r_framework.Dto.SearchConditionsDto();
            if (this.shimeChangeFlag)
            {
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = false;
                dtowhere.Join = JOIN_METHOD.INNER_JOIN;
                dtowhere.LeftTable = "M_TORIHIKISAKI_SHIHARAI";
                dtowhere.LeftKeyColumn = "TORIHIKISAKI_CD";
                dtowhere.RightKeyColumn = "TORIHIKISAKI_CD";
                dtowhere.RightTable = "M_TORIHIKISAKI";
                this.form.txtTorihikisakiCd.popupWindowSetting.Add(dtowhere);

                dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_TORIHIKISAKI_SHIHARAI";

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "TORIHIKI_KBN_CD";
                serdto.Value = "2";
                serdto.ValueColumnType = DB_TYPE.SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                this.form.txtTorihikisakiCd.popupWindowSetting.Add(dtowhere);

            }
            else
            {
                dtowhere.IsCheckLeftTable = true;
                dtowhere.IsCheckRightTable = false;
                dtowhere.Join = JOIN_METHOD.INNER_JOIN;
                dtowhere.LeftTable = "M_TORIHIKISAKI_SHIHARAI";
                dtowhere.LeftKeyColumn = "TORIHIKISAKI_CD";
                dtowhere.RightKeyColumn = "TORIHIKISAKI_CD";
                dtowhere.RightTable = "M_TORIHIKISAKI";
                this.form.txtTorihikisakiCd.popupWindowSetting.Add(dtowhere);


                dtowhere = new r_framework.Dto.JoinMethodDto();
                dtowhere.Join = JOIN_METHOD.WHERE;
                dtowhere.LeftTable = "M_TORIHIKISAKI_SHIHARAI";


                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHIMEBI1";
                serdto.Value = "cmbShimebi";
                serdto.ValueColumnType = DB_TYPE.IN_SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHIMEBI2";
                serdto.Value = "cmbShimebi";
                serdto.ValueColumnType = DB_TYPE.IN_SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                serdto = new r_framework.Dto.SearchConditionsDto();
                serdto.And_Or = CONDITION_OPERATOR.OR;
                serdto.Condition = JUGGMENT_CONDITION.EQUALS;
                serdto.LeftColumn = "SHIMEBI3";
                serdto.Value = "cmbShimebi";
                serdto.ValueColumnType = DB_TYPE.IN_SMALLINT;
                dtowhere.SearchCondition.Add(serdto);

                this.form.txtTorihikisakiCd.popupWindowSetting.Add(dtowhere);
            }

        }

        /// <summary>
        /// レイアウト区分変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INVOICE_KBN_CheckedChanged(object sender, EventArgs e)
        {
            //画面遷移フラグtrue(伝票締画面)、明細あり
            if ((this.shimeChangeFlag) && (this.form.customDataGridView1.Rows.Count > 0))
            {
                if (this.headerForm.INVOICE_KBN.Text == "1")
                {
                    //旧締→適格支払明細
                    if (MessageBox.Show("レイアウト区分を変更した場合\r締チェックが解除されますがよろしいですか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.form.checkBoxAll.Checked = false;
                        //明細のチェック全てOFF
                        foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
                        {
                            row.Cells[0].Value = false;
                        }
                        if (this.form.customDataGridView1.Rows.Count > 0)
                        {
                            this.form.customDataGridView1.CurrentCell = null;
                            this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[0].Cells[0];
                        }

                        this.form.customDataGridView1.Refresh();

                        parentForm.bt_func9.Enabled = true;
                        parentForm.bt_process4.Enabled = true;
                        parentForm.bt_process5.Enabled = true;

                        // 合計金額の再計算
                        this.CalcGoukeiKingaku();
                    }
                    else
                    {
                        //処理中止
                        this.headerForm.INVOICE_KBN.Text = "2";
                    }
                }
                else
                {
                    //適格支払明細→旧締
                    parentForm.bt_func9.Enabled = true;
                    parentForm.bt_process4.Enabled = true;
                    parentForm.bt_process5.Enabled = true;
                }
            }
        }
    }
}