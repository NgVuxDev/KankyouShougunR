using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Billing.Seikyushokakunin.Const;
using Shougun.Core.Billing.SeikyuushoHakkou;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Text;
using System.Linq;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using r_framework.Dto;
using Shougun.Core.Billing.Seikyushokakunin.DTO;

namespace Shougun.Core.Billing.Seikyushokakunin
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// Form
        /// </summary>
        private UIForm MyForm;
        /// <summary>
        /// Header
        /// </summary>
        private UIHeader headerForm;
        /// <summary>
        /// 請求伝票
        /// </summary>
        private TSDDaoCls SeikyuuDenpyouDao;
        /// <summary>
        /// 表示条件パターン区分
        /// </summary>
        private string HyojiKbn;
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Billing.Seikyushokakunin.Setting.ButtonSetting.xml";
        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgcls;
        /// <summary>
        /// 取引先_請求情報マスタ
        /// </summary>
        private MTSDaoCls TorihikisakiSeikyuuDao;
        /// <summary>
        /// 請求伝票
        /// </summary>
        private T_SEIKYUU_DENPYOU tSeikyuuDenpyou;
        /// <summary>
        /// 入金消込
        /// </summary>
        private TNKDao NyuukinKeshikomiDao;
        /// <summary>
        /// 入金
        /// </summary>
        public TNEDao NyuukinEntryDao;
        // 20150602 代納伝票対応(代納不具合一覧52) Start
        /// <summary>
        /// 売上支払
        /// </summary>
        public TUSEDao UrShEntryDao;
        // 20150602 代納伝票対応(代納不具合一覧52) End

        private string strSystemDate = string.Empty;

        private IM_SYS_INFODao sysInfoDao;
        internal M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.MyForm = targetForm;
            // メッセージ出力用
            this.msgcls = new MessageBoxShowLogic();

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
                //初期化
                //総ページ数
                this.MyForm.PageCnt = "1";
                //現在のページ番号
                this.MyForm.NowPageNo = 1;
                //ラジオボタン
                this.MyForm.CustomNumericTextBox21.Text = "1";
                this.MyForm.CustomNumericTextBox22.Text = "1";
                this.MyForm.CustomNumericTextBox23.Text = "1";
                this.MyForm.CustomNumericTextBox24.Text = "1";

                var parentForm = (BusinessBaseForm)this.MyForm.Parent;
                this.headerForm = (UIHeader)((BusinessBaseForm)this.MyForm.Parent).headerForm;
                strSystemDate = parentForm.sysDate.ToShortDateString();

                //請求番号
                string seikyuNumber = Shougun.Core.Billing.Seikyushokakunin.Properties.Settings.Default.SEIKYUU_NUMBER;
                SeikyuuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
                TorihikisakiSeikyuuDao = DaoInitUtility.GetComponent<MTSDaoCls>();
                NyuukinKeshikomiDao = DaoInitUtility.GetComponent<TNKDao>();
                NyuukinEntryDao = DaoInitUtility.GetComponent<TNEDao>();
                // 20150602 代納伝票対応(代納不具合一覧52) Start
                UrShEntryDao = DaoInitUtility.GetComponent<TUSEDao>();
                // 20150602 代納伝票対応(代納不具合一覧52) End
                //請求伝票を取得
                this.tSeikyuuDenpyou = SeikyuuDenpyouDao.GetDataByCd(seikyuNumber);
                if (this.tSeikyuuDenpyou != null)
                {
                    //書式区分
                    string shoshikiKbn = this.tSeikyuuDenpyou.SHOSHIKI_KBN.ToString();
                    //書式明細区分
                    string shoshikiMeisaiKbn = this.tSeikyuuDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
                    //入金明細区分
                    string nyuukinMeisaiKbn = this.tSeikyuuDenpyou.NYUUKIN_MEISAI_KBN.ToString();

                    //請求伝票データ取得
                    DataTable seikyuDt = GetSeikyudenpyo(shoshikiKbn, shoshikiMeisaiKbn, nyuukinMeisaiKbn);
                    this.MyForm.SeikyuDt = seikyuDt;
                    //set readonly = false when set data bikou
                    this.MyForm.SeikyuDt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);
                    if (seikyuDt.Rows.Count != 0)
                    {
                        //総ページ数
                        if (string.IsNullOrEmpty(seikyuDt.Rows[seikyuDt.Rows.Count - 1]["KAGAMI_NUMBER"].ToString()))
                        {
                            this.MyForm.PageCnt = "1";
                        }
                        else
                        {
                            this.MyForm.PageCnt = (seikyuDt.Rows[seikyuDt.Rows.Count - 1]["KAGAMI_NUMBER"]).ToString();
                        }

                        //現在のページ番号
                        if (string.IsNullOrEmpty(seikyuDt.Rows[0]["KAGAMI_NUMBER"].ToString()))
                        {
                            this.MyForm.NowPageNo = 1;
                        }
                        else
                        {
                            this.MyForm.NowPageNo = Convert.ToInt16(seikyuDt.Rows[0]["KAGAMI_NUMBER"]);
                        }

                        //請求書区分
                        this.MyForm.txtInvoiceKBN.Text = seikyuDt.Rows[0]["INVOICE_KBN"].ToString();

                        //Header部データ設定
                        SetHead(this.tSeikyuuDenpyou);
                        if (this.MyForm.txtInvoiceKBN.Text == "2")
                        {
                            //請求伝票データ設定
                            if (!SetSeikyuDenpyo_invoice())
                            {
                                return false;
                            }
                        }
                        else
                        {
                            //請求伝票データ設定
                            if (!SetSeikyuDenpyo())
                            {
                                return false;
                            }
                        }
                    }
                }
                // ボタンのテキストを初期化
                ButtonInit();
                // イベントの初期化処理
                EventInit();
                //ToolTip設定>
                SetTipTxt();
                // 請求書発行日
                this.headerForm.SEIKYU_HAKKOU.Text = ConstClass.SEIKYU_HAKKOU_PRINT_SHINAI;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgcls.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 請求伝票明細データ設定
        /// </summary>
        private void SetHead(T_SEIKYUU_DENPYOU tseikyuudenpyou)
        {            
            if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG == this.MyForm.SyoriMode)
            {
                headerForm.windowTypeLabel.Text = "参照";
            }
            else if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.MyForm.SyoriMode)
            {
                headerForm.windowTypeLabel.Text = "削除";
            }
            headerForm.windowTypeLabel.Show();
            //拠点コード
            headerForm.KYOTEN_CD.Text = tseikyuudenpyou.KYOTEN_CD.ToString();
            //拠点名
            IM_KYOTENDao KyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            M_KYOTEN mKyoten = KyotenDao.GetDataByCd(tseikyuudenpyou.KYOTEN_CD.ToString());
            if (mKyoten != null)
            {
                headerForm.KYOTEN_NM.Text = mKyoten.KYOTEN_NAME_RYAKU;
            }
        }

        /// <summary>
        /// 請求伝票明細データ設定
        /// </summary>
        public bool SetSeikyuDenpyo()
        {
            bool ret = true;
            try
            {
                this.MyForm.MEISEI_DGV.IsBrowsePurpose = false;

                //初期化クリア
                this.MyForm.MEISEI_DGV.Rows.Clear();
                //ヘッダ設定用フラグ
                Boolean headFlg = true;

                var isSeikyuuUchizei = false;
                var isSeikyuuSotozei = false;

                for (int i = 0; i < this.MyForm.SeikyuDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = this.MyForm.SeikyuDt.Rows[i];

                    //鑑テーブルにテータがあるか
                    if (string.IsNullOrEmpty(tableRow["KAGAMI_NUMBER"].ToString()))
                    {
                        //書式区分
                        string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();

                        //伝票テーブルから設定できる項目のみを設定
                        //請求番号
                        this.MyForm.SEIKYU_NO.Text = tableRow["SEIKYUU_NUMBER"].ToString();
                        //鑑番号ラベル
                        this.MyForm.GYOUSYA_CNT.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
                        //差引繰越額
                        this.MyForm.SASHIHIKI_GAKU.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
                        //今回請求額
                        this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));
                        //今回請求額(差引繰越額】＋【今回売上額】＋【消費税額】)
                        if (!(Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString())
                            || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                            || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
                        {
                            this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KONKAI_SEIKYUGAKU.Text, this.MyForm.SASHIHIKI_GAKU.Text));
                        }
                        //請求年月日
                        this.MyForm.SEIKYU_YMD.Text = ((DateTime)tableRow["SEIKYUU_DATE"]).ToShortDateString();
                        //コード
                        this.MyForm.CD.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
                        //前回請求額
                        this.MyForm.ZENKAI_SEIKYUGAKU.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
                        //入金額
                        this.MyForm.NYUKIN_GAKU.Text = SetComma(tableRow["KONKAI_NYUUKIN_GAKU"].ToString());
                        //相殺他
                        this.MyForm.SOUSAI.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
                        //今回売上額
                        this.MyForm.KOUKAI_URIAGEGAKU.Text = "0";
                        //消費税額
                        this.MyForm.SYOUHIZEI_GAKU.Text = "0";
                        //合計請求額
                        this.MyForm.GOKEI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));

                        /* 銀行情報をセットします
                         * 
                        //振込銀行２(銀行名)
                        this.MyForm.GINKOU1.Text = "　　" + tableRow["FURIKOMI_BANK_NAME"];
                        //振込銀行３(支店名)
                        this.MyForm.GINKOU2.Text = "　　" + tableRow["FURIKOMI_BANK_SHITEN_NAME"];
                        //振込銀行４(口座種類 + 口座番号)
                        this.MyForm.GINKOU3.Text = "　　" + tableRow["KOUZA_SHURUI"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["KOUZA_NO"];
                        //振込銀行５(口座名義)
                        this.MyForm.GINKOU4.Text = "　　" + tableRow["KOUZA_NAME"];*/

                        this.setBankInfo(tableRow);

                        //単月請求の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
                        if (Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString()) ||
                            Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
                        {
                            //①前回請求額
                            this.MyForm.ZENKAI_SEIKYUGAKU.Text = "";
                            //②入金額
                            this.MyForm.NYUKIN_GAKU.Text = "";
                            //③相殺他
                            this.MyForm.SOUSAI.Text = "";
                            //④差引繰越額
                            this.MyForm.SASHIHIKI_GAKU.Text = "";

                            // 項目を非表示にする
                            this.MyForm.label10.Visible = false;
                            this.MyForm.label11.Visible = false;
                            this.MyForm.label12.Visible = false;
                            this.MyForm.label38.Visible = false;
                            this.MyForm.ZENKAI_SEIKYUGAKU.Visible = false;
                            this.MyForm.NYUKIN_GAKU.Visible = false;
                            this.MyForm.SOUSAI.Visible = false;
                            this.MyForm.SASHIHIKI_GAKU.Visible = false;
                        }
                    }

                    else
                    {
                        //現在ページ設定
                        if (Convert.ToInt16(tableRow["KAGAMI_NUMBER"]) == this.MyForm.NowPageNo)
                        {
                            //一回目だけヘッダ設定
                            if (headFlg)
                            {
                                //請求伝票ヘッダデータ設定
                                SetSeikyuDenpyoHeader(tableRow);
                                headFlg = false;

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
                                tablePevRow = this.MyForm.SeikyuDt.Rows[i - 1];
                            }
                            //現在行の次の行
                            DataRow tableNextRow = null;
                            if (i == this.MyForm.SeikyuDt.Rows.Count - 1)
                            {
                                //現在行の次の行
                                tableNextRow = null;
                            }
                            else
                            {
                                //現在行の次の行
                                tableNextRow = this.MyForm.SeikyuDt.Rows[i + 1];
                            }

                            //明細データが存在するか
                            if (!(string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString())))
                            {
                                //業者名設定
                                if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者名(入金データ時は出力しない)
                                    if (("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //業者シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "1";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GYOUSHA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"]);
                                    }
                                }
                                //現場名設定
                                if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                                {
                                    //現場名(入金データ時は出力しない)
                                    if (("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //現場シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        //拠点
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GENBA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GENBA_NAME2"]);
                                    }
                                }
                                //請求伝票明細データ設定
                                SetSeikyuDenpyoMeisei(tableRow, tablePevRow);
                                //伝票金額と消費税
                                if (tableNextRow == null || !tableRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
                                {
                                    //伝票種類が【入金】の場合以外
                                    if (!Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
                                        var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
                                        var denpyou_uchizei_gaku = Convert.ToDecimal(tableRow["DENPYOU_UCHIZEI_GAKU"]);
                                        var denpyou_sotozei_gaku = Convert.ToDecimal(tableRow["DENPYOU_SOTOZEI_GAKU"]);
                                        var denpyou_syouhizei = denpyou_uchizei_gaku + denpyou_sotozei_gaku;
                                        if (Const.ConstClass.ZEI_KEISAN_KBN_DENPYOU == denpyou_zei_keisan_kbn_cd)
                                        {
                                            //明細設定用配列
                                            int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                            DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                            //品名
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【伝票毎消費税】");
                                            //消費税
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = GetSyohizei(denpyou_syouhizei, denpyou_zei_kbn_cd, true);
                                        }
                                    }
                                }
                                //現場金額と消費税設定
                                if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                                {
                                    //現場金額と消費税
                                    //「パターン1 or 2かつ入金データ以外」又は「パターン2かつ入金データ」の場合に出力
                                    if ((("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn)) && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        || ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        if ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【入金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【現場計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GENBA_KINGAKU_1"]);
                                        /////---------------------------///////
                                        decimal denpyouSotoZeiGaku;
                                        decimal denpyouUchiZeiGaku;
                                        GetDenpyouZei(this.MyForm.SeikyuDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), tableRow["TSDE_GENBA_CD"].ToString(), out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, true);

                                        //消費税(外税表示)
                                        decimal genbaSoto = Convert.ToDecimal(tableRow["GENBA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                        if (genbaSoto != 0)
                                        {
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = genbaSoto;
                                        }

                                        //備考(内税表示)
                                        if (!("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                        {
                                            decimal genbaUchi = Convert.ToDecimal(tableRow["GENBA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                            if (genbaUchi != 0)
                                            {
                                                row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = GetSyohizei(genbaUchi, Const.ConstClass.DENPYOU_ZEI_KBN_CD_2);
                                            }

                                            if (genbaSoto != 0 || genbaUchi != 0)
                                            {
                                                if (IsSeikyuData(tableRow))
                                                {
                                                    row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (Convert.ToString(row.Cells[ConstClass.COL_MEISAI_BIKOU].Value) + Const.ConstClass.ZENKAKU_SPACE + Const.ConstClass.SEIKYU_ZEI_EXCEPT).ToString().Trim();
                                                }
                                            }
                                        }
                                    }
                                }
                                //業者金額と消費税設定
                                if (tableNextRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者金額と消費税
                                    if ("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        if (Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【入金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【業者計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GYOUSHA_KINGAKU_1"]);

                                        decimal denpyouSotoZeiGaku;
                                        decimal denpyouUchiZeiGaku;
                                        GetDenpyouZei(this.MyForm.SeikyuDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), null, out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, false);

                                        //消費税(外税表示)
                                        decimal gyoushaSoto = Convert.ToDecimal(tableRow["GYOUSHA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                        if (gyoushaSoto != 0)
                                        {
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = gyoushaSoto;
                                        }

                                        //備考(内税表示)
                                        if (!Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            decimal gyoushaUchi = Convert.ToDecimal(tableRow["GYOUSHA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                            if (gyoushaUchi != 0)
                                            {
                                                row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = GetSyohizei(gyoushaUchi, Const.ConstClass.DENPYOU_ZEI_KBN_CD_2);
                                            }

                                            if (gyoushaSoto != 0 || gyoushaUchi != 0)
                                            {
                                                if (IsSeikyuData(tableRow))
                                                {
                                                    row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (Convert.ToString(row.Cells[ConstClass.COL_MEISAI_BIKOU].Value) + Const.ConstClass.ZENKAKU_SPACE + Const.ConstClass.SEIKYU_ZEI_EXCEPT).ToString().Trim();
                                                }
                                            }
                                        }
                                    }
                                }

                                if (ConstClass.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstClass.ZEI_KBN_UCHI == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                                {
                                    isSeikyuuUchizei = true;
                                }
                                if (ConstClass.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstClass.ZEI_KBN_SOTO == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                                {
                                    isSeikyuuSotozei = true;
                                }

                                //請求消費税設定
                                if (tableNextRow == null || !tableRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"]))
                                {
                                    //請求毎消費税(内)
                                    //明細設定用配列
                                    if (isSeikyuuUchizei)
                                    {
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【請求毎消費税（内）】");
                                        //消費税
                                        row.Cells[ConstClass.COL_SHOUHIZEI].Value = "(" + SetComma((tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]).ToString()) + ")";
                                    }
                                    if (isSeikyuuSotozei)
                                    {
                                        //請求毎消費税(外)
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        //row[3] = ("【請求毎消費税（外）】");
                                        row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【請求毎消費税】");      // No.4221
                                        //消費税
                                        row.Cells[ConstClass.COL_SHOUHIZEI].Value = (tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuDenpyo", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            this.MyForm.MEISEI_DGV.IsBrowsePurpose = true;
            return ret;
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
        private void GetDenpyouZei(DataTable seikyuDt, DataRow tableRow, string gyoushaCd, string genbaCd, out decimal denpyouUchiZeiGaku, out decimal denpyouSotoZeiGaku, bool isGenba)
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
        private bool IsSeikyuData(DataRow tableRow)
        {
            decimal seiUchiZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
            decimal seiSotoZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);

            bool result = 0 < (seiUchiZei + seiSotoZei);

            return result;
        }

        /// <summary>
        /// 請求伝票明細データ設定
        /// </summary>
        private void SetSeikyuDenpyoMeisei(DataRow tableRow, DataRow tablePevRow)
        {
            //明細設定用配列
            int index = this.MyForm.MEISEI_DGV.Rows.Add();
            DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
            if (tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["DENPYOU_DATE"]);
            }
            //売上No
            row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value = (tableRow["DENPYOU_NUMBER"]);
            //品名
            row.Cells[ConstClass.COL_HINMEI_NAME].Value = (tableRow["HINMEI_NAME"]);

            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            if (Const.ConstClass.DENPYOU_SHURUI_CD_10 != denpyou_shurui_cd)
            {
                //数量１
                row.Cells[ConstClass.COL_SUURYOU].Value = (tableRow["SUURYOU"]);
                //単位
                row.Cells[ConstClass.COL_UNIT_NAME].Value = (tableRow["UNIT_NAME"]);
                //単価
                row.Cells[ConstClass.COL_TANKA].Value = (tableRow["TANKA"]);
            }
            else
            {
                //数量１
                row.Cells[ConstClass.COL_SUURYOU].Value = null;
                //単位
                row.Cells[ConstClass.COL_UNIT_NAME].Value = null;
                //単価
                row.Cells[ConstClass.COL_TANKA].Value = null;
            }
            //金額
            row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["KINGAKU"]);
            //消費税
            var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
            var meisai_shouhizei = tableRow["MEISEI_SYOHIZEI"].ToString();
            var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
            var meisai_zei_kbn_cd = tableRow["MEISAI_ZEI_KBN_CD"].ToString();
            var zei_kbn_cd = (Const.ConstClass.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd)) ? denpyou_zei_kbn_cd : meisai_zei_kbn_cd;
            var shouhizei = GetSyohizei(meisai_shouhizei, zei_kbn_cd, true);
            if (Const.ConstClass.DENPYOU_SHURUI_CD_10 == denpyou_shurui_cd)
            {
                // 入金伝票は出力しない
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = "";
            }
            else if (Const.ConstClass.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd)
            {
                // 税計算区分が明細毎だったら明細税を出力
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = shouhizei;
            }
            else if (false == String.IsNullOrEmpty(meisai_zei_kbn_cd) && "0" != meisai_zei_kbn_cd)
            {
                // 伝票毎、請求毎でも明細税区分があれば明細税を出力
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = shouhizei;
            }
            else if (Const.ConstClass.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd && Const.ConstClass.ZEI_KBN_HIKAZEI == denpyou_zei_kbn_cd)
            {
                // 伝票毎、請求毎ではなく非課税は 0 を出力
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = "0";
            }
            else
            {
                // 上記以外は出力しない
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = "";
            }
            //備考
            row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (tableRow["MEISAI_BIKOU"]);
            //明細行判断用フラグ
            row.Cells[ConstClass.COL_OUT_FLG].Value = "3";
            //システムID_DENPYOU_SYSTEM_ID
            row.Cells[ConstClass.COL_DENPYOU_SYSTEM_ID].Value = (tableRow["DENPYOU_SYSTEM_ID"]);
            //枝番_DENPYOU_SEQ
            row.Cells[ConstClass.COL_DENPYOU_SEQ].Value = (tableRow["DENPYOU_SEQ"]);
            //伝票種類_DENPYOU_SHURUI_CD
            row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value = (tableRow["DENPYOU_SHURUI_CD"]);
        }
        /// <summary>
        /// 消費税内税の場合は括弧を追加
        /// </summary>
        private object GetSyohizei(object syohizei, object zeiKbn)
        {
            return GetSyohizei(syohizei, zeiKbn, false);
        }

        /// <summary>
        /// 消費税を書式設定する。
        /// </summary>
        /// <param name="shouhizei">消費税額</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="isZero">True:0でも表示 False:0は表示しない</param>
        /// <returns>書式設定した文字列</returns>
        private static string GetSyohizei(object shouhizei, object zeiKubun, bool isZero)
        {
            var ret = String.Empty;
            var zeigaku = Decimal.Parse(shouhizei.ToString());
            if (isZero)
            {
                if (Const.ConstClass.ZEI_KBN_UCHI == zeiKubun.ToString())
                {
                    ret = "(" + SetComma(zeigaku.ToString()) + ")";
                }
                else
                {
                    ret = SetComma(zeigaku.ToString());
                }
            }
            else if (0 != zeigaku)
            {
                if (Const.ConstClass.ZEI_KBN_UCHI == zeiKubun.ToString())
                {
                    ret = "(" + SetComma(zeigaku.ToString()) + ")";
                }
                else
                {
                    ret = SetComma(zeigaku.ToString());
                }
            }

            // 非課税は0でも表示
            if (Const.ConstClass.ZEI_KBN_HIKAZEI == zeiKubun.ToString())
            {
                ret = "0";
            }

            return ret;
        }

        /// <summary>
        /// 請求伝票ヘッダデータ設定
        /// </summary>
        private void SetSeikyuDenpyoHeader(DataRow tableRow)
        {
            //請求先郵便番号
            if (!String.IsNullOrEmpty(tableRow["SEIKYUU_SOUFU_POST"].ToString()))
            {
                this.MyForm.SEIKYU_YUBIN.Text = Const.ConstClass.YUBIN + tableRow["SEIKYUU_SOUFU_POST"].ToString();
            }
            //請求先住所1
            this.MyForm.SEIKYU_JYUSYO1.Text = tableRow["SEIKYUU_SOUFU_ADDRESS1"].ToString();
            //請求先住所2
            this.MyForm.SEIKYU_JYUSYO2.Text = tableRow["SEIKYUU_SOUFU_ADDRESS2"].ToString();
            //請求先1
            this.MyForm.SEIKYU_SYAMEI1.Text = tableRow["SEIKYUU_SOUFU_NAME1"].ToString() + Const.ConstClass.ZENKAKU_SPACE + tableRow["SEIKYUU_SOUFU_KEISHOU1"].ToString();
            //請求先2
            this.MyForm.SEIKYU_SYAMEI2.Text = tableRow["SEIKYUU_SOUFU_NAME2"].ToString() + Const.ConstClass.ZENKAKU_SPACE + tableRow["SEIKYUU_SOUFU_KEISHOU2"].ToString();
            // 請求先部署
            this.MyForm.SEIKYU_BUSHO.Text = tableRow["SEIKYUU_SOUFU_BUSHO"].ToString();
            //請求先担当者
            if (!string.IsNullOrEmpty(tableRow["SEIKYUU_SOUFU_TANTOU"].ToString()))
            {
                // 請求先部署の値有無によって請求先担当者をつめる
                if (string.IsNullOrEmpty(this.MyForm.SEIKYU_BUSHO.Text))
                {
                    this.MyForm.SEIKYU_BUSHO.Text = tableRow["SEIKYUU_SOUFU_TANTOU"].ToString() + Const.ConstClass.ZENKAKU_SPACE + "様";
                    this.MyForm.SEIKYU_TANTOUSHA.Text = string.Empty;
                }
                else
                {
                    this.MyForm.SEIKYU_TANTOUSHA.Text = tableRow["SEIKYUU_SOUFU_TANTOU"].ToString() + Const.ConstClass.ZENKAKU_SPACE + "様";
                }
            }
            else
            {
                this.MyForm.SEIKYU_TANTOUSHA.Text = string.Empty;
            }
            //請求番号
            this.MyForm.SEIKYU_NO.Text = tableRow["SEIKYUU_NUMBER"].ToString();
            //書式区分
            string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();
            //(請求先別）の場合「請求先数」
            if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn))
            {
                this.MyForm.GYOUSYA_NM.Text = "請求先数";
            }
            //(業者別）の場合「業者数」
            else if (Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn))
            {
                this.MyForm.GYOUSYA_NM.Text = "業者数";
            }
            //(現場別）の場合「現場数」
            else if (Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
            {
                this.MyForm.GYOUSYA_NM.Text = "現場数";
            }
            //鑑番号
            this.MyForm.GYOUSYA_CNT.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
            //自社名1
            this.MyForm.JISYA_SYAMEI1.Text = tableRow["CORP_NAME"].ToString();
            //自社名2
            // 設定前にクリア
            this.MyForm.JISYA_SYAMEI2.Text = "";
            //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 2:印字しない場合＞
            if (Const.ConstClass.PRINT_KBN_2.Equals(tableRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                //＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 1:印字の場合＞
                if (Const.ConstClass.PRINT_KBN_1.Equals(tableRow["DAIHYOU_PRINT_KBN"].ToString()))
                {
                    //20151002 hoanghm #12809 start
                    //this.MyForm.JISYA_SYAMEI2.Text = "代表者  " + tableRow["CORP_DAIHYOU"].ToString();
                    this.MyForm.JISYA_SYAMEI2.Text = tableRow["CORP_DAIHYOU"].ToString();
                    //20151002 hoanghm #12809 end
                }
            }
            //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する場合＞
            else if (Const.ConstClass.PRINT_KBN_1.Equals(tableRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                this.MyForm.JISYA_SYAMEI2.Text = tableRow["KYOTEN_NAME"].ToString();
            }
            //代表者名
            // 設定前にクリア
            this.MyForm.TORISIMARIYAKU.Text = "";
            //＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 1:印字の場合＞
            if (Const.ConstClass.PRINT_KBN_1.Equals(tableRow["DAIHYOU_PRINT_KBN"].ToString()))
            {
                //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する場合＞
                if (Const.ConstClass.PRINT_KBN_1.Equals(tableRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
                {
                    //20151002 hoanghm #12809 start
                    //this.MyForm.TORISIMARIYAKU.Text = "代表者  " + tableRow["KYOTEN_DAIHYOU"].ToString();
                    this.MyForm.TORISIMARIYAKU.Text = tableRow["KYOTEN_DAIHYOU"].ToString();
                    //20151002 hoanghm #12809 end
                }
            }
            //自社郵便番号
            if (!String.IsNullOrEmpty(tableRow["KYOTEN_POST"].ToString()))
            {
                this.MyForm.JISYA_YUBIN.Text = Const.ConstClass.YUBIN + tableRow["KYOTEN_POST"].ToString();
            }
            //自社住所1
            this.MyForm.JISYA_JYUSYO1.Text = tableRow["KYOTEN_ADDRESS1"].ToString();
            //自社住所2
            this.MyForm.JISYA_JYUSYO2.Text = tableRow["KYOTEN_ADDRESS2"].ToString();
            //電話
            this.MyForm.DENWA_NO.Text = "電話  " + tableRow["KYOTEN_TEL"].ToString();
            //FAX
            this.MyForm.FAX_NO.Text = "FAX  " + tableRow["KYOTEN_FAX"].ToString();
            //備考1
            this.MyForm.txtBiko1.Text = StringUtil.ConverToString(tableRow["BIKOU_1"]);
            //備考2
            this.MyForm.txtBiko2.Text = StringUtil.ConverToString(tableRow["BIKOU_2"]);
            //請求担当者
            this.MyForm.TANTOU.Text = "請求担当者  " + tableRow["SEIKYUU_TANTOU"].ToString();
            //請求年月日
            this.MyForm.SEIKYU_YMD.Text = ((DateTime)tableRow["SEIKYUU_DATE"]).ToShortDateString();
            //コード
            this.MyForm.CD.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
            //前回請求額
            this.MyForm.ZENKAI_SEIKYUGAKU.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
            //入金額
            this.MyForm.NYUKIN_GAKU.Text = SetComma(tableRow["KONKAI_NYUUKIN_GAKU"].ToString());
            //相殺他
            this.MyForm.SOUSAI.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
            //差引繰越額
            this.MyForm.SASHIHIKI_GAKU.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
            //今回売上額
            this.MyForm.KOUKAI_URIAGEGAKU.Text = SetComma(tableRow["TSDK_KONKAI_URIAGE_GAKU"].ToString());
            //消費税額
            this.MyForm.SYOUHIZEI_GAKU.Text = SetComma(tableRow["SYOUHIZEIGAKU"].ToString());
            //今回請求額
            this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));
            //今回請求額(差引繰越額】＋【今回売上額】＋【消費税額】)
            if (!(Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString())
                || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
            {
                this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KONKAI_SEIKYUGAKU.Text, this.MyForm.SASHIHIKI_GAKU.Text));
            }
            //合計請求額
            this.MyForm.GOKEI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));

            //登録番号
            this.MyForm.txtTourokuNo.Text = StringUtil.ConverToString(tableRow["TOUROKU_NO"]);

            /* 銀行情報をセットします
             * 
            //振込銀行2(銀行名)
            this.MyForm.GINKOU1.Text = "　　" + tableRow["FURIKOMI_BANK_NAME"];
            //振込銀行3(支店名)
            this.MyForm.GINKOU2.Text = "　　" + tableRow["FURIKOMI_BANK_SHITEN_NAME"];
            //振込銀行4(口座種類 + 口座番号)
            this.MyForm.GINKOU3.Text = "　　" + tableRow["KOUZA_SHURUI"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["KOUZA_NO"];
            //振込銀行5(口座名義)
            this.MyForm.GINKOU4.Text = "　　" + tableRow["KOUZA_NAME"];*/

            this.setBankInfo(tableRow);

            //単月請求の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
            if (Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString()) ||
                Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
            {
                //①前回請求額
                this.MyForm.ZENKAI_SEIKYUGAKU.Text = "";
                //②入金額
                this.MyForm.NYUKIN_GAKU.Text = "";
                //③相殺他
                this.MyForm.SOUSAI.Text = "";
                //④差引繰越額
                this.MyForm.SASHIHIKI_GAKU.Text = "";

                // 項目を非表示にする
                this.MyForm.label10.Visible = false;
                this.MyForm.label11.Visible = false;
                this.MyForm.label12.Visible = false;
                this.MyForm.label38.Visible = false;
                this.MyForm.ZENKAI_SEIKYUGAKU.Visible = false;
                this.MyForm.NYUKIN_GAKU.Visible = false;
                this.MyForm.SOUSAI.Visible = false;
                this.MyForm.SASHIHIKI_GAKU.Visible = false;
            }
        }

        /// <summary>
        /// 請求伝票データ取得
        /// </summary>
        private DataTable GetSeikyudenpyo(string shoshikiKbn, string shoshikiMeisaiKbn, string nyuukinMeisaiKbn)
        {
            //請求番号
            string seikyuNumber = Shougun.Core.Billing.Seikyushokakunin.Properties.Settings.Default.SEIKYUU_NUMBER;
            //①T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが1：請求先別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            //②T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが2：業者別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            string orderBy = " ";
            if ((Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                    && Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                || (Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                    && Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.TSDE_GYOUSHA_CD , TSDKE.TSDE_GENBA_CD ";
            }
            //①T_SEIKYUU_DENPYOU.SHOSHIKI_KBNが1：請求先別　且つ　T_SEIKYUU_DENPYOU.SHOSHIKI_MEISAI_KBNが ２：業者毎
            else if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                && (Const.ConstClass.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.TSDE_GYOUSHA_CD ";
            }

            //表示区分設定
            if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn))
            {
                if (Const.ConstClass.SHOSHIKI_MEISAI_KBN_2.Equals(shoshikiMeisaiKbn))
                {
                    HyojiKbn = "PARTTEN_1";
                }
                else if (Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                {
                    HyojiKbn = "PARTTEN_12";
                }
            }
            else if (Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn))
            {
                if (Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                {
                    HyojiKbn = "PARTTEN_2";
                }
            }

            DataTable seikyuDt;

            if (nyuukinMeisaiKbn == Const.ConstClass.NYUUKIN_MEISAI_KBN1)
            {
                seikyuDt = SeikyuuDenpyouDao.GetSeikyudenpyo(seikyuNumber, nyuukinMeisaiKbn, orderBy);
            }
            else
            {
                seikyuDt = SeikyuuDenpyouDao.GetSeikyudenpyoMeisaiNashi(seikyuNumber, nyuukinMeisaiKbn, orderBy);

                // 伝票が２伝票以上ある場合
                if (seikyuDt.Rows.Count > 1)
                {
                    // 鑑のみの伝票を削除
                    foreach (DataRow row in seikyuDt.Rows)
                    {
                        if (row["DENPYOU_SYSTEM_ID"].Equals(DBNull.Value))
                        {
                            seikyuDt.Rows.Remove(row);
                            break;
                        }
                    }
                }
            }

            // 並び替え
            if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn) && Const.ConstClass.SHOSHIKI_MEISAI_KBN_1.Equals(shoshikiMeisaiKbn))
            {
                // 入金、その他伝票順にソート
                DataRow[] nyukkinRows = seikyuDt.Select(string.Format("DENPYOU_SHURUI_CD = {0}", Const.ConstClass.DENPYOU_SHURUI_CD_10));
                DataRow[] denpyoRows = seikyuDt.Select(string.Format("DENPYOU_SHURUI_CD <> {0} OR DENPYOU_SHURUI_CD IS NULL", Const.ConstClass.DENPYOU_SHURUI_CD_10));

                DataTable dt = seikyuDt.Clone();

                foreach (DataRow row in nyukkinRows)
                {
                    DataRow r = dt.NewRow();
                    r.ItemArray = row.ItemArray;

                    dt.Rows.Add(r);
                }

                foreach (DataRow row in denpyoRows)
                {
                    DataRow r = dt.NewRow();
                    r.ItemArray = row.ItemArray;

                    dt.Rows.Add(r);
                }

                return dt;
            }
            else
            {
                return seikyuDt;
            }
        }
        /// <summary>
        /// 金額加算
        /// </summary>
        private string KigakuAdd(string a, string b)
        {
            return (Convert.ToDecimal(a == null || a.Equals("") ? "0" : a) + Convert.ToDecimal(b == null || b.Equals("") ? "0" : b)).ToString();

        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <returns></returns>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.MyForm.WindowType);

            //ボタン活性/非活性設定
            SetEnabled();
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        internal void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.MyForm.Parent;

            //前ボタン(F1)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func1);
            parentForm.bt_func1.Click += new EventHandler(this.MyForm.PrevPage);
            //次ボタン(F2)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func2);
            parentForm.bt_func2.Click += new EventHandler(this.MyForm.NextPage);

            //一覧ボタン(F7)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func7);
            parentForm.bt_func7.Click += new EventHandler(this.MyForm.Ichiran);

            //参照ボタン(F3)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func3);
            parentForm.bt_func3.Click += new EventHandler(this.MyForm.Sansyo);

            //プレビューボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.MyForm.PreView);

            //プレビューボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.MyForm.CSVPrint);

            //登録ボタン(F9)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.MyForm.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.UPDATE;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.MyForm.FormClose);

            this.MyForm.MEISEI_DGV.CellClick += new DataGridViewCellEventHandler(this.MyForm.selectCellCange);

            //InxsSubApplication - Seikyuu option
            parentForm.OnReceiveMessageEvent += ParentForm_OnReceiveMessageEvent;

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// ボタン活性/非活性、項目表示/非表示設定
        /// </summary>
        /// <returns></returns>
        private void SetEnabled()
        {
            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            // 参照
            if (WINDOW_TYPE.REFERENCE_WINDOW_FLAG == this.MyForm.SyoriMode)
            {
                //備考情報を変更できるから、【F9：登録】を活性とする。
                if (r_framework.Authority.Manager.CheckAuthority("G103", WINDOW_TYPE.UPDATE_WINDOW_FLAG,false))
                {
                    parentForm.bt_func9.Enabled = true;
                }
                else
                {
                    parentForm.bt_func9.Enabled = false;
                }
                //備考を活性とする。
                this.MyForm.txtBiko1.ReadOnly = false;
                this.MyForm.txtBiko2.ReadOnly = false;
            }
            // 削除
            else if (WINDOW_TYPE.DELETE_WINDOW_FLAG == this.MyForm.SyoriMode)
            {
                //【F9：登録】を活性とする。
                parentForm.bt_func9.Enabled = true;
                //備考を非活性とする
                this.MyForm.txtBiko1.ReadOnly = true;
                this.MyForm.txtBiko2.ReadOnly = true;
            }
            //F1F2
            SetF1F2ButtonEnabled();
            if (this.MyForm.SeikyuDt == null || this.MyForm.SeikyuDt.Rows.Count == 0)
            {
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func5.Enabled = false;
                parentForm.bt_func7.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                //備考を非活性とする
                this.MyForm.txtBiko1.ReadOnly = true;
                this.MyForm.txtBiko2.ReadOnly = true;
            }
        }

        /// <summary>
        /// F1F2ボタン活性/非活性設定
        /// </summary>
        /// <returns></returns>
        public void SetF1F2ButtonEnabled()
        {
            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            if (1 == this.MyForm.NowPageNo)
            {
                parentForm.bt_func1.Enabled = false;
            }
            else
            {
                parentForm.bt_func1.Enabled = true;
            }
            if (this.MyForm.PageCnt.Equals(this.MyForm.NowPageNo.ToString()))
            {
                parentForm.bt_func2.Enabled = false;
            }
            else
            {
                parentForm.bt_func2.Enabled = true;
            }
        }
        /// <summary>
        /// 登録イベント処理
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public virtual bool Regist(int showMsg = 1)
        {
            LogUtility.DebugMethodStart();
            string titleMsg = string.Empty;
            bool selectedYes = false;
            T_SEIKYUU_DENPYOU seikyuuEntity = null;
            List<T_SEIKYUU_DENPYOU_KAGAMI> arrSeikyuuKagami = null;
            // 確認メッセージ表示
            DialogResult dlgres = DialogResult.None;
            try
            {
                if (showMsg > 0)
                {
                    if (this.MyForm.SyoriMode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    {
                        T_NYUUKIN_KESHIKOMI nyuukin = new T_NYUUKIN_KESHIKOMI();
                        nyuukin.SEIKYUU_NUMBER = SqlInt64.Parse(this.MyForm.SeikyuNumber);
                        nyuukin.DELETE_FLG = SqlBoolean.False;
                        var keshikomiEntitys = NyuukinKeshikomiDao.GetDataForEntity(nyuukin);
                        if (keshikomiEntitys != null && 0 < keshikomiEntitys.Length)
                        {
                            msgcls.MessageBoxShow("E151", "請求伝票", "該当の入金伝票を見直して");
                            return false;
                        }
                        dlgres = msgcls.MessageBoxShow("C026");
                        titleMsg = "削除";
                    }
                    else if (this.MyForm.SyoriMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        dlgres = msgcls.MessageBoxShow("C038");
                        titleMsg = "修正";
                    }
                }
                else
                {
                    dlgres = DialogResult.Yes;
                }

                selectedYes = (dlgres == DialogResult.Yes);
                // 「はい」のみ実行
                if (dlgres == DialogResult.Yes)
                {
                    //伝票は参照する伝票のとき、T_SEIKYUU_DENPYOU　Entity を作成する後、備考情報を登録する、
                    if (this.MyForm.SyoriMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        seikyuuEntity = new T_SEIKYUU_DENPYOU();
                        seikyuuEntity.SEIKYUU_NUMBER = Convert.ToInt64(this.MyForm.SeikyuNumber);
                        var databind = new DataBinderLogic<T_SEIKYUU_DENPYOU>(seikyuuEntity);
                        databind.SetSystemProperty(seikyuuEntity, false);

                        arrSeikyuuKagami = this.CreateEntitySeikyuuUpdate();
                    }

                    // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                    using (Transaction tran = new Transaction())
                    {
                        if (seikyuuEntity == null)//伝票削除します
                        {
                            //請求伝票更新
                            SeikyuuDenpyouDao.UpdateSeikyu(this.MyForm.SeikyuNumber);
                            //請求伝票_鑑更新
                            SeikyuuDenpyouDao.UpdateSeikyuKagami(this.MyForm.SeikyuNumber);
                            //請求明細
                            SeikyuuDenpyouDao.UpdateSeikyuDetail(this.MyForm.SeikyuNumber);
                            //入金消込更新
                            SeikyuuDenpyouDao.UpdateNyukin(this.MyForm.SeikyuNumber);
                        }
                        else//伝票を更新します
                        {
                            //請求伝票更新
                            SeikyuuDenpyouDao.UpdateSeikyuuInfo(seikyuuEntity);
                            //請求伝票鏡_備考
                            if (arrSeikyuuKagami != null && arrSeikyuuKagami.Count > 0)
                            {
                                foreach (T_SEIKYUU_DENPYOU_KAGAMI seikyuuKagamiEntity in arrSeikyuuKagami)
                                {
                                    SeikyuuDenpyouDao.UpdateSeikyuuKagamiBikou(seikyuuKagamiEntity);
                                }
                            }
                        }
                        // コミット
                        tran.Commit();
                    }

                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
                    if (this.MyForm.SyoriMode == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        && r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho())
                    {
                        this.DeleteInxsSeikyuu(Convert.ToInt64(this.MyForm.SeikyuNumber));
                    }
                    // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

                    // 完了メッセージ表示
                    if (showMsg > 0)
                    {
                        msgcls.MessageBoxShow("I001", titleMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラー
                    //メッセージ出力して継続
                    msgcls.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    msgcls.MessageBoxShow("E093", "");
                }
                else
                {
                    msgcls.MessageBoxShow("E245", "");
                }
                return false;
            }
            LogUtility.DebugMethodEnd();
            return selectedYes;
        }


        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        /// <summary>
        /// 請求書プレビュー処理
        /// </summary>
        public bool PreView()
        {
        	// プレビュ/CSV
            bool printFlg = true;

            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.headerForm.SEIKYU_HAKKOU.Text))
                {
                    MessageBox.Show(ConstClass.ErrStop1, ConstClass.DialogTitle);
                    return ret;
                }
                //save bikou value before when printed, beacause when printing is data reloading
                if (this.MyForm.SyoriMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.Regist(0);
                }
                DataRow row = this.MyForm.SeikyuDt.Rows[0];

                SeikyuuDenpyouDto dto = new SeikyuuDenpyouDto();
                dto.TorihikisakiCd = row["TSD_TORIHIKISAKI_CD"].ToString();
                dto.MSysInfo = new DBAccessor().GetSysInfo();
                //発行条件はデータから取得
                dto.Meisai = row["NYUUKIN_MEISAI_KBN"].ToString();
                dto.SeikyuHakkou = this.headerForm.SEIKYU_HAKKOU.Text;
                dto.SeikyushoPrintDay = ConstClass.SEIKYU_PRINT_DAY_SIMEBI; //締日固定
                dto.SeikyuStyle = ConstClass.SEIKYU_KEITAI_DATA_SAKUSEIJI;
                dto.SeikyuPaper = row["YOUSHI_KBN"].ToString();

                dto.HakkoBi = this.strSystemDate;

                SeikyuuDenpyouLogicClass.PreViewSeikyuDenpyou(tSeikyuuDenpyou, dto, printFlg, this.MyForm.txtInvoiceKBN.Text, this.headerForm.ZeiRate_Chk.Checked);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PreView", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PreView", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 請求書CSV処理
        /// </summary>
        public bool CSVPrint()
        {
            // プレビュ/CSV
            bool printFlg = false;

            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.headerForm.SEIKYU_HAKKOU.Text))
                {
                    MessageBox.Show(ConstClass.ErrStop1, ConstClass.DialogTitle);
                    return ret;
                }

                DataRow row = this.MyForm.SeikyuDt.Rows[0];

                SeikyuuDenpyouDto dto = new SeikyuuDenpyouDto();
                dto.TorihikisakiCd = row["TSD_TORIHIKISAKI_CD"].ToString();
                dto.MSysInfo = new DBAccessor().GetSysInfo();
                //発行条件はデータから取得
                dto.Meisai = row["NYUUKIN_MEISAI_KBN"].ToString();
                dto.SeikyuHakkou = this.headerForm.SEIKYU_HAKKOU.Text;
                dto.SeikyushoPrintDay = ConstClass.SEIKYU_PRINT_DAY_SIMEBI; //締日固定
                dto.SeikyuStyle = ConstClass.SEIKYU_KEITAI_DATA_SAKUSEIJI;
                dto.SeikyuPaper = row["YOUSHI_KBN"].ToString();

                dto.HakkoBi = this.strSystemDate;

                SeikyuuDenpyouLogicClass.PreViewSeikyuDenpyou(tSeikyuuDenpyou, dto, printFlg, this.MyForm.txtInvoiceKBN.Text, this.headerForm.ZeiRate_Chk.Checked);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PreView", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PreView", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// ToolTip設定
        /// </summary>
        private void SetTipTxt()
        {
            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            parentForm.bt_func1.Tag = Const.ConstClass.TOOL_TIP_TXT_18;
            parentForm.bt_func2.Tag = Const.ConstClass.TOOL_TIP_TXT_19;
            parentForm.bt_func3.Tag = Const.ConstClass.TOOL_TIP_TXT_20;
            parentForm.bt_func5.Tag = Const.ConstClass.TOOL_TIP_TXT_21;
            parentForm.bt_func7.Tag = Const.ConstClass.TOOL_TIP_TXT_22;
            parentForm.bt_func9.Tag = Const.ConstClass.TOOL_TIP_TXT_23;
            parentForm.bt_func12.Tag = Const.ConstClass.TOOL_TIP_TXT_24;
            parentForm.txb_process.Tag = Const.ConstClass.TOOL_TIP_TXT_25;
        }
        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private static string SetComma(string value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                return string.Format("{0:#,0}", Convert.ToDecimal(value));
            }
        }

        /// <summary>
        /// 指定した請求番号のデータが存在するか返す
        /// </summary>
        /// <param name="seikyuNumber">請求番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistSeikyuData(string seikyuNumber, out bool catchErr)
        {
            catchErr = true;
            LogUtility.DebugMethodStart(seikyuNumber);

            bool returnVal = false;
            try
            {
                //請求伝票を取得
                SeikyuuDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
                T_SEIKYUU_DENPYOU tseikyuudenpyou = SeikyuuDenpyouDao.GetDataByCd(seikyuNumber);

                //削除済みの伝票かチェック
                if (tseikyuudenpyou.DELETE_FLG == false)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IsExistSeikyuData", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistSeikyuData", ex);
                this.msgcls.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        /// <summary>
        /// 銀行情報のFUNCTION
        /// </summary>
        /// <param name="tableRow"></param>
        private void setBankInfo(DataRow tableRow)
        {
            LogUtility.DebugMethodStart(tableRow);           
            //1.請求書を印字する取引先に銀行が3銀行がセットされている場合
            string bankCd1 = StringUtil.ConverToString(tableRow["FURIKOMI_BANK_CD"]);
            string bankCd2 = StringUtil.ConverToString(tableRow["FURIKOMI_BANK_CD_2"]);
            string bankCd3 = StringUtil.ConverToString(tableRow["FURIKOMI_BANK_CD_3"]);
            if (string.IsNullOrEmpty(bankCd1) &&
                string.IsNullOrEmpty(bankCd2) &&
                string.IsNullOrEmpty(bankCd3))
            {
                return;
            }

            StringBuilder strBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(bankCd1) &&
               !string.IsNullOrEmpty(bankCd2) &&
               !string.IsNullOrEmpty(bankCd3))
            {
                strBuilder.AppendFormat("{0}  {1}  {2}  {3}  {4}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME"]),
                                                                   this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME"])
                                                                   );
                strBuilder.Append("@");
                strBuilder.AppendFormat("{0}  {1}  {2}  {3}  {4}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME_2"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME_2"]),
                                                                   this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI_2"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO_2"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME_2"])
                                                                   );
                strBuilder.Append("@");
                strBuilder.AppendFormat("{0}  {1}  {2}  {3}  {4}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME_3"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME_3"]),
                                                                   this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI_3"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO_3"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME_3"])
                                                                   );
            }
            //2.請求書を印字する取引先に銀行が２銀行、または1銀行がセットされている場合 
            else
            {
                if (!string.IsNullOrEmpty(bankCd1))
                {
                    strBuilder.AppendFormat("{0}  {1}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME"])
                                                                   );
                    strBuilder.Append("@");
                    strBuilder.AppendFormat("  {0}  {1}  {2}", this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME"])
                                                                   );
                }
                if (!string.IsNullOrEmpty(bankCd2))
                {
                    if (strBuilder.Length > 0)
                    {
                        strBuilder.Append("@");
                    }
                    strBuilder.AppendFormat("{0}  {1}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME_2"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME_2"])
                                                                   );
                    strBuilder.Append("@");
                    strBuilder.AppendFormat("  {0}  {1}  {2}", this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI_2"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO_2"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME_2"])
                                                                   );
                }
                if (!string.IsNullOrEmpty(bankCd3))
                {
                    if (strBuilder.Length > 0)
                    {
                        strBuilder.Append("@");
                    }
                    strBuilder.AppendFormat("{0}  {1}", StringUtil.ConverToString(tableRow["FURIKOMI_BANK_NAME_3"]),
                                                                   StringUtil.ConverToString(tableRow["FURIKOMI_BANK_SHITEN_NAME_3"])
                                                                   );
                    strBuilder.Append("@");
                    strBuilder.AppendFormat("  {0}  {1}  {2}", this.revertKouzaShuruiName(StringUtil.ConverToString(tableRow["KOUZA_SHURUI_3"])),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NO_3"]),
                                                                   StringUtil.ConverToString(tableRow["KOUZA_NAME_3"])
                                                                   );
                }

            }
            string str = strBuilder.ToString();
            string[] strSplit = str.Split(new char[] {'@' });
            if (strSplit.Length > 0)
            {
                this.MyForm.GINKOU1.Text = strSplit[0];
            }
            if (strSplit.Length > 1)
            {
                this.MyForm.GINKOU2.Text = strSplit[1];
            }
            if (strSplit.Length > 2)
            {
                this.MyForm.GINKOU3.Text = strSplit[2];
            }
            if (strSplit.Length > 3)
            {
                this.MyForm.GINKOU4.Text = strSplit[3];
            }
            LogUtility.DebugMethodEnd();
        }
        private string revertKouzaShuruiName(string kouzaShurui)
        {
            string returnVl = string.Empty;
            switch (kouzaShurui)
            {
                case "普通預金":
                    returnVl = "普通";
                    break;
                case "当座預金":
                    returnVl = "当座";
                    break;
                case "その他":
                    returnVl = "その他";
                    break;
            }

            return returnVl;
        }
        /// <summary>
        /// set info kagami when bikou change
        /// </summary>
        /// <param name="kagamiNo"></param>
        internal void setSeikyuuKagamiBikouInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (string.IsNullOrEmpty(this.MyForm.GYOUSYA_CNT.Text))
                {
                    return;
                }
                string[] strplit = this.MyForm.GYOUSYA_CNT.Text.Split(new char[] { '/' });
                if (strplit.Length == 0)
                {
                    return;
                }
                string strKagamiNo = strplit[0];
                var getDataSeisan = from row in this.MyForm.SeikyuDt.AsEnumerable()
                                    where StringUtil.ConverToString(row["KAGAMI_NUMBER"]).Equals(strKagamiNo)
                                    select row;
                if (getDataSeisan == null || getDataSeisan.Count() == 0)
                {
                    return;
                }
                foreach (var row in getDataSeisan)
                {
                    row["BIKOU_1"] = this.MyForm.txtBiko1.Text;
                    row["BIKOU_2"] = this.MyForm.txtBiko2.Text;
                }
            }
            catch (Exception ee)
            {

                LogUtility.Error("setSeisanKagamiBikouInfo", ee);
                return;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// create entity seikyuu update
        /// </summary>
        private List<T_SEIKYUU_DENPYOU_KAGAMI> CreateEntitySeikyuuUpdate()
        {
            LogUtility.DebugMethodStart();
            List<T_SEIKYUU_DENPYOU_KAGAMI> arrSeikyuuKagami = new List<T_SEIKYUU_DENPYOU_KAGAMI>();
            try
            {
                //save value bikou before when registry
                this.setSeikyuuKagamiBikouInfo();
                var groupSeiKyuuKagami = from r in this.MyForm.SeikyuDt.AsEnumerable()
                                        group r by new
                                        {
                                            SEIKYUU_NUMBER = Convert.ToInt64(r["SEIKYUU_NUMBER"]),
                                            KAGAMI_NUMBER = Convert.ToInt32(r["KAGAMI_NUMBER"]),
                                            BIKOU_1 = StringUtil.ConverToString(r["BIKOU_1"]),
                                            BIKOU_2 = StringUtil.ConverToString(r["BIKOU_2"]),
                                        } into grps
                                        select new
                                        {
                                            SEIKYUU_NUMBER = grps.Key.SEIKYUU_NUMBER,
                                            KAGAMI_NUMBER = grps.Key.KAGAMI_NUMBER,
                                            BIKOU_1 = grps.Key.BIKOU_1,
                                            BIKOU_2 = grps.Key.BIKOU_2
                                        };
                //create entity
                foreach (var gr in groupSeiKyuuKagami)
                {
                    T_SEIKYUU_DENPYOU_KAGAMI kagami = new T_SEIKYUU_DENPYOU_KAGAMI();
                    kagami.SEIKYUU_NUMBER = gr.SEIKYUU_NUMBER;
                    kagami.KAGAMI_NUMBER = gr.KAGAMI_NUMBER;
                    kagami.BIKOU_1 = gr.BIKOU_1;
                    kagami.BIKOU_2 = gr.BIKOU_2;
                    arrSeikyuuKagami.Add(kagami);
                }

            }
            catch (Exception ee)
            {
                LogUtility.Error("CreateEntitySeikyuuUpdate", ee);
                throw;
            }
            LogUtility.DebugMethodEnd();
            return arrSeikyuuKagami;
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

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        private void DeleteInxsSeikyuu(long seikyuuNumber)
        {
            var keyDto = new CommonKeyDto()
            {
                Id = seikyuuNumber
            };

            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            var requestDto = new
            {
                CommandName = 3,//delete single seikyuu data
                ShougunParentWindowName = parentForm.Text,
                CommandArgs = new List<CommonKeyDto>() { keyDto }
            };
            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = this.MyForm.transactionId,
                ReferenceID = seikyuuNumber
            });
            var execCommandDto = new ExecuteCommandDto()
            {
                Token = token,
                Type = ExternalConnection.CommunicateLib.Enums.NotificationType.ExecuteCommand,
                Args = new object[] { JsonUtility.SerializeObject(requestDto) }
            };
            remoteAppCls.ExecuteCommand(Constans.StartFormText, execCommandDto);

        }

        private void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!r_framework.Configuration.AppConfig.AppOptions.IsInxsSeikyuusho())
                {
                    return;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                    if (arg != null)
                    {
                        var msgDto = (CommunicateAppDto)arg;
                        var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                        if (token != null)
                        {
                            var tokenDto = (CommunicateTokenDto)token;
                            if (tokenDto.TransactionId == this.MyForm.transactionId
                                && tokenDto.ReferenceID != null && tokenDto.ReferenceID.ToString() == this.MyForm.SeikyuNumber)
                            {
                                if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                                {
                                    var responeDto = JsonUtility.DeserializeObject<InxsSubAppResponseDto>(msgDto.Args[0].ToString());
                                    if (responeDto != null)
                                    {
                                        this.ShowInxsMessage(responeDto.MessageType, responeDto.ResponseMessage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// CONFIRM = 1, WARN = 2, ERROR = 3, INFO = 4,
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strMsg"></param>
        private void ShowInxsMessage(int type, string strMsg)
        {
            switch (type)
            {
                case 1:
                    this.msgcls.MessageBoxShowConfirm(strMsg);
                    break;
                case 2:
                    this.msgcls.MessageBoxShowWarn(strMsg);
                    break;
                case 3:
                    this.msgcls.MessageBoxShowError(strMsg);
                    break;
                case 4:
                    this.msgcls.MessageBoxShowInformation(strMsg);
                    break;
            }
        }
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        /// <summary>
        /// 請求伝票明細データ設定
        /// ※【適格請求書】対応
        /// </summary>
        public bool SetSeikyuDenpyo_invoice()
        {
            bool ret = true;
            try
            {
                this.MyForm.MEISEI_DGV.IsBrowsePurpose = false;

                //初期化クリア
                this.MyForm.MEISEI_DGV.Rows.Clear();
                //ヘッダ設定用フラグ
                Boolean headFlg = true;

                for (int i = 0; i < this.MyForm.SeikyuDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = this.MyForm.SeikyuDt.Rows[i];

                    //鑑テーブルにテータがあるか
                    if (string.IsNullOrEmpty(tableRow["KAGAMI_NUMBER"].ToString()))
                    {
                        //書式区分
                        string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();

                        //伝票テーブルから設定できる項目のみを設定
                        //請求番号
                        this.MyForm.SEIKYU_NO.Text = tableRow["SEIKYUU_NUMBER"].ToString();
                        //鑑番号ラベル
                        this.MyForm.GYOUSYA_CNT.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
                        //差引繰越額
                        this.MyForm.SASHIHIKI_GAKU.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
                        //今回請求額
                        this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));
                        //今回請求額(差引繰越額】＋【今回売上額】＋【消費税額】)
                        if (!(Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString())
                            || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                            || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
                        {
                            this.MyForm.KONKAI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KONKAI_SEIKYUGAKU.Text, this.MyForm.SASHIHIKI_GAKU.Text));
                        }
                        //請求年月日
                        this.MyForm.SEIKYU_YMD.Text = ((DateTime)tableRow["SEIKYUU_DATE"]).ToShortDateString();
                        //コード
                        this.MyForm.CD.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
                        //前回請求額
                        this.MyForm.ZENKAI_SEIKYUGAKU.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
                        //入金額
                        this.MyForm.NYUKIN_GAKU.Text = SetComma(tableRow["KONKAI_NYUUKIN_GAKU"].ToString());
                        //相殺他
                        this.MyForm.SOUSAI.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
                        //今回売上額
                        this.MyForm.KOUKAI_URIAGEGAKU.Text = "0";
                        //消費税額
                        this.MyForm.SYOUHIZEI_GAKU.Text = "0";
                        //合計請求額
                        this.MyForm.GOKEI_SEIKYUGAKU.Text = SetComma(KigakuAdd(this.MyForm.KOUKAI_URIAGEGAKU.Text, this.MyForm.SYOUHIZEI_GAKU.Text));

                        this.setBankInfo(tableRow);

                        //単月請求の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
                        if (Const.ConstClass.SEIKYUU_KEITAI_KBN_1.Equals(tableRow["SEIKYUU_KEITAI_KBN"].ToString()) ||
                            Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
                        {
                            //①前回請求額
                            this.MyForm.ZENKAI_SEIKYUGAKU.Text = "";
                            //②入金額
                            this.MyForm.NYUKIN_GAKU.Text = "";
                            //③相殺他
                            this.MyForm.SOUSAI.Text = "";
                            //④差引繰越額
                            this.MyForm.SASHIHIKI_GAKU.Text = "";

                            // 項目を非表示にする
                            this.MyForm.label10.Visible = false;
                            this.MyForm.label11.Visible = false;
                            this.MyForm.label12.Visible = false;
                            this.MyForm.label38.Visible = false;
                            this.MyForm.ZENKAI_SEIKYUGAKU.Visible = false;
                            this.MyForm.NYUKIN_GAKU.Visible = false;
                            this.MyForm.SOUSAI.Visible = false;
                            this.MyForm.SASHIHIKI_GAKU.Visible = false;
                        }
                    }

                    else
                    {
                        //現在ページ設定
                        if (Convert.ToInt16(tableRow["KAGAMI_NUMBER"]) == this.MyForm.NowPageNo)
                        {
                            //一回目だけヘッダ設定
                            if (headFlg)
                            {
                                //請求伝票ヘッダデータ設定
                                SetSeikyuDenpyoHeader(tableRow);
                                headFlg = false;
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
                                tablePevRow = this.MyForm.SeikyuDt.Rows[i - 1];
                            }
                            //現在行の次の行
                            DataRow tableNextRow = null;
                            if (i == this.MyForm.SeikyuDt.Rows.Count - 1)
                            {
                                //現在行の次の行
                                tableNextRow = null;
                            }
                            else
                            {
                                //現在行の次の行
                                tableNextRow = this.MyForm.SeikyuDt.Rows[i + 1];
                            }

                            //明細データが存在するか
                            if (!(string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString())))
                            {
                                //業者名設定
                                if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者名(入金データ時は出力しない)
                                    if (("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //業者シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "1";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GYOUSHA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"]);
                                    }
                                }
                                //現場名設定
                                if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                                {
                                    //現場名(入金データ時は出力しない)
                                    if (("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //現場シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        //拠点
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GENBA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GENBA_NAME2"]);
                                    }
                                }
                                //請求伝票明細データ設定
                                SetSeikyuDenpyoMeisei_invoice(tableRow, tablePevRow);

                                //現場金額と消費税設定
                                if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                                {
                                    //現場金額と消費税
                                    //「パターン1 or 2かつ入金データ以外」又は「パターン2かつ入金データ」の場合に出力
                                    if ((("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn)) && !Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        || ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        if ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【入金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【現場計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GENBA_KINGAKU_1"]);
                                    }
                                }
                                //業者金額と消費税設定
                                if (tableNextRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tableNextRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者金額と消費税
                                    if ("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        //品名
                                        if (Const.ConstClass.DENPYOU_SHURUI_CD_10.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【入金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【業者計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GYOUSHA_KINGAKU_1"]);
                                    }
                                }

                                //請求消費税設定
                                if (tableNextRow == null || !tableRow["RANK_SEIKYU_1"].Equals(tableNextRow["RANK_SEIKYU_1"]))
                                {
                                    if ((tableRow["KONKAI_KAZEI_KBN_1"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_2"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_3"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_4"].ToString() != "0") ||
                                        (tableRow["KONKAI_HIKAZEI_KBN"].ToString() != "0"))
                                    {
                                        //空白行
                                        int index = this.MyForm.MEISEI_DGV.Rows.Add();
                                        DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = "";

                                        decimal zeiritu;


                                        //課税計（１～４）行を表示
                                        for (int y = 1; y <= 4; y++)
                                        {
                                            if (tableRow["KONKAI_KAZEI_KBN_" + y].ToString() != "0")
                                            {
                                                index = this.MyForm.MEISEI_DGV.Rows.Add();
                                                row = this.MyForm.MEISEI_DGV.Rows[index];
                                                zeiritu = (decimal)(tableRow["KONKAI_KAZEI_RATE_" + y]);
                                                row.Cells[ConstClass.COL_HINMEI_NAME].Value = "【" + string.Format("{0:0%}", zeiritu) + "対象】";
                                                row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["KONKAI_KAZEI_GAKU_" + y]);
                                                row.Cells[ConstClass.COL_SHOUHIZEI].Value = (tableRow["KONKAI_KAZEI_ZEIGAKU_" + y]);
                                                row.Cells[ConstClass.COL_OUT_FLG].Value = "4";
                                            }
                                        }

                                        //非課税計行を表示
                                        if (tableRow["KONKAI_HIKAZEI_KBN"].ToString() != "0")
                                        {
                                            index = this.MyForm.MEISEI_DGV.Rows.Add();
                                            row = this.MyForm.MEISEI_DGV.Rows[index];
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = "【非課税対象】";
                                            row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["KONKAI_HIKAZEI_GAKU"]);
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = string.Empty;
                                            row.Cells[ConstClass.COL_OUT_FLG].Value = "4";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuDenpyo_invoice", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            this.MyForm.MEISEI_DGV.IsBrowsePurpose = true;
            return ret;
        }

        /// <summary>
        /// 請求伝票明細データ設定
        /// ※【適格請求書】対応
        /// </summary>
        private void SetSeikyuDenpyoMeisei_invoice(DataRow tableRow, DataRow tablePevRow)
        {
            //明細設定用配列
            int index = this.MyForm.MEISEI_DGV.Rows.Add();
            DataGridViewRow row = this.MyForm.MEISEI_DGV.Rows[index];
            if (tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["DENPYOU_DATE"]);
            }
            //売上No
            row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value = (tableRow["DENPYOU_NUMBER"]);
            //品名
            row.Cells[ConstClass.COL_HINMEI_NAME].Value = (tableRow["HINMEI_NAME"]);

            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            if (Const.ConstClass.DENPYOU_SHURUI_CD_10 != denpyou_shurui_cd)
            {
                //数量１
                row.Cells[ConstClass.COL_SUURYOU].Value = (tableRow["SUURYOU"]);
                //単位
                row.Cells[ConstClass.COL_UNIT_NAME].Value = (tableRow["UNIT_NAME"]);
                //単価
                row.Cells[ConstClass.COL_TANKA].Value = (tableRow["TANKA"]);
            }
            else
            {
                //数量１
                row.Cells[ConstClass.COL_SUURYOU].Value = null;
                //単位
                row.Cells[ConstClass.COL_UNIT_NAME].Value = null;
                //単価
                row.Cells[ConstClass.COL_TANKA].Value = null;
            }
            //金額
            row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["KINGAKU"]);
            //消費税
            var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
            var meisai_shouhizei = tableRow["MEISEI_SYOHIZEI"].ToString();
            var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
            var meisai_zei_kbn_cd = tableRow["MEISAI_ZEI_KBN_CD"].ToString();
            var zei_kbn_cd = (Const.ConstClass.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd)) ? denpyou_zei_kbn_cd : meisai_zei_kbn_cd;
            var shouhizei = GetSyohizei(meisai_shouhizei, zei_kbn_cd, true);
            if (Const.ConstClass.DENPYOU_SHURUI_CD_10 == denpyou_shurui_cd)
            {
                // 入金伝票は出力しない
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = "";
            }
            else
            {
                string zei_kbn = denpyou_zei_kbn_cd;
                if (!Const.ConstClass.ZEI_KBN_NASHI.Equals(meisai_zei_kbn_cd))
                {
                    zei_kbn = meisai_zei_kbn_cd;
                }

                if (Const.ConstClass.ZEI_KBN_HIKAZEI.Equals(zei_kbn))
                {
                    row.Cells[ConstClass.COL_SHOUHIZEI].Value = "非課税";
                }
                else if (Const.ConstClass.ZEI_KBN_UCHI.Equals(zei_kbn))
                {
                    row.Cells[ConstClass.COL_SHOUHIZEI].Value = "内税";
                }
            }
            //備考
            row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (tableRow["MEISAI_BIKOU"]);
            //明細行判断用フラグ
            row.Cells[ConstClass.COL_OUT_FLG].Value = "3";
            //システムID_DENPYOU_SYSTEM_ID
            row.Cells[ConstClass.COL_DENPYOU_SYSTEM_ID].Value = (tableRow["DENPYOU_SYSTEM_ID"]);
            //枝番_DENPYOU_SEQ
            row.Cells[ConstClass.COL_DENPYOU_SEQ].Value = (tableRow["DENPYOU_SEQ"]);
            //伝票種類_DENPYOU_SHURUI_CD
            row.Cells[ConstClass.COL_DENPYOU_SHURUI_CD].Value = (tableRow["DENPYOU_SHURUI_CD"]);
        }
    }
}
