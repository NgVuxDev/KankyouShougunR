using System;
using System.Collections.Generic;
using System.Data;
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
using Shougun.Core.Adjustment.ShiharaiMeisaishoHakko;
using Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Const;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Linq;
using Shougun.Core.ExternalConnection.CommunicateLib.Dtos;
using Shougun.Core.ExternalConnection.CommunicateLib;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.Adjustment.Shiharaimeisaishokakunin.DTO;
using System.Data.SqlTypes;

namespace Shougun.Core.Adjustment.Shiharaimeisaishokakunin
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
        ///精算伝票
        /// </summary>
        private TSDDaoCls SeisanDenpyouDao;
        /// <summary>
        /// 表示条件パターン区分
        /// </summary>
        private string HyojiKbn;
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Setting.ButtonSetting.xml";
        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgcls;
        /// 出金消込
        /// </summary> 
        private TSKDao ShukkinKeshikomiDao;//160019

        /// <summary>	
        /// 取引先_支払情報マスタ	
        /// </summary>	
        private MTSDaoCls TorihikisakiShiharaiDao;
        // 20150602 代納伝票対応(代納不具合一覧52) Start
        /// <summary>
        /// 売上支払Dao
        /// </summary>
        internal TUSEDaoCls UrShEntryDao;
        // 20150602 代納伝票対応(代納不具合一覧52) End
        /// <summary>
        /// 精算伝票
        /// </summary>
        private T_SEISAN_DENPYOU tSeisanDenpyou;

        /// <summary>
        /// DBシステム日付
        /// </summary>
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
                // ラジオボタン
                this.MyForm.txtShiharaiYoshi.Text = "1";
                this.MyForm.txtShiharaiKeitai.Text = "1";
                this.MyForm.txtMeisaishoInsatuDate.Text = "1";
                this.MyForm.txtInsatujun.Text = "1";

                var parentForm = (BusinessBaseForm)this.MyForm.Parent;
                this.headerForm = (UIHeader)((BusinessBaseForm)this.MyForm.Parent).headerForm;

                strSystemDate = parentForm.sysDate.ToShortDateString();

                //精算番号
                string seisanNumber = Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Properties.Settings.Default.SEISAN_NUMBER;
                SeisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
                TorihikisakiShiharaiDao = DaoInitUtility.GetComponent<MTSDaoCls>();
                ShukkinKeshikomiDao = DaoInitUtility.GetComponent<TSKDao>();//160019
                // 20150602 代納伝票対応(代納不具合一覧52) Start
                UrShEntryDao = DaoInitUtility.GetComponent<TUSEDaoCls>();
                // 20150602 代納伝票対応(代納不具合一覧52) End
                //精算伝票を取得
                this.tSeisanDenpyou = SeisanDenpyouDao.GetDataByCd(seisanNumber);
                if (this.tSeisanDenpyou != null)
                {
                    //書式区分
                    string shoshikiKbn = this.tSeisanDenpyou.SHOSHIKI_KBN.ToString();
                    //書式明細区分
                    string shoshikiMeisaiKbn = this.tSeisanDenpyou.SHOSHIKI_MEISAI_KBN.ToString();
                    //出金明細区分
                    string shukkinMeisaiKbn = this.tSeisanDenpyou.SHUKKIN_MEISAI_KBN.ToString();

                    //精算伝票データ取得
                    DataTable seisanDt = GetSeisandenpyo(shoshikiKbn, shoshikiMeisaiKbn, shukkinMeisaiKbn);
                    this.MyForm.ShiharaiDt = seisanDt;
                    //set readonly = false when set data bikou
                    this.MyForm.ShiharaiDt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);
                    if (seisanDt.Rows.Count != 0)
                    {
                        //総ページ数
                        if (string.IsNullOrEmpty(seisanDt.Rows[seisanDt.Rows.Count - 1]["KAGAMI_NUMBER"].ToString()))
                        {
                            this.MyForm.PageCnt = "1";
                        }
                        else
                        {
                            this.MyForm.PageCnt = (seisanDt.Rows[seisanDt.Rows.Count - 1]["KAGAMI_NUMBER"]).ToString();
                        }

                        //現在のページ番号
                        if (string.IsNullOrEmpty(seisanDt.Rows[0]["KAGAMI_NUMBER"].ToString()))
                        {
                            this.MyForm.NowPageNo = 1;
                        }
                        else
                        {
                            this.MyForm.NowPageNo = Convert.ToInt16(seisanDt.Rows[0]["KAGAMI_NUMBER"]);
                        }

                        //請求書区分
                        this.MyForm.txtInvoiceKBN.Text = seisanDt.Rows[0]["INVOICE_KBN"].ToString();

                        //Header部データ設定
                        SetHead(this.tSeisanDenpyou);

                        if (this.MyForm.txtInvoiceKBN.Text == "2")
                        {
                            //精算伝票データ設定
                            if (!SetSeisanDenpyo_invoice())
                            {
                                return false;
                            }
                        }
                        else
                        {
                            //精算伝票データ設定
                            if (!SetSeisanDenpyo())
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
                // 支払明細書発行日
                this.MyForm.txtShiharaiHakkou.Text = ConstClass.SHIHARAI_HAKKOU_PRINT_SHINAI;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
        /// <summary>
        /// 精算伝票明細データ設定
        /// </summary>
        private void SetHead(T_SEISAN_DENPYOU tseisandenpyou)
        {
            UIHeader headerForm = (UIHeader)((BusinessBaseForm)this.MyForm.Parent).headerForm;
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
            headerForm.txtKyotenCd.Text = tseisandenpyou.KYOTEN_CD.ToString().PadLeft(2, '0');
            //拠点名
            IM_KYOTENDao KyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            M_KYOTEN mKyoten = KyotenDao.GetDataByCd(tseisandenpyou.KYOTEN_CD.ToString());
            if (mKyoten != null)
            {
                headerForm.txtKyotenMei.Text = mKyoten.KYOTEN_NAME_RYAKU;
            }           
        }

        /// <summary>
        /// 精算伝票明細データ設定
        /// </summary>
        public bool SetSeisanDenpyo()
        {
            bool ret = true;
            try
            {
                this.MyForm.dgvMeisai.IsBrowsePurpose = false;

                //初期化クリア
                this.MyForm.dgvMeisai.Rows.Clear();
                //ヘッダ設定用フラグ
                Boolean headFlg = true;

                var isSeisanUchizei = false;
                var isSeisanSotozei = false;

                for (int i = 0; i < this.MyForm.ShiharaiDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = this.MyForm.ShiharaiDt.Rows[i];

                    //鑑テーブルにテータがあるか
                    if (string.IsNullOrEmpty(tableRow["KAGAMI_NUMBER"].ToString()))
                    {
                        //書式区分
                        string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();

                        //伝票テーブルから設定できる項目のみを設定
                        //支払番号
                        this.MyForm.txtShiharaiNo.Text = tableRow["SEISAN_NUMBER"].ToString();
                        //鑑番号ラベル
                        this.MyForm.txtGyosyaSu.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
                        //差引繰越額
                        this.MyForm.txtSashihikiKurikoshiGaku.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
                        //今回精算額
                        this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));
                        //今回精算額(【差引繰越額】＋【今回支払額】＋【消費税額】)
                        if (!(Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString())
                            || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                            || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
                        {
                            this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtSeisangaku.Text, this.MyForm.txtSashihikiKurikoshiGaku.Text));
                        }
                        //支払年月日
                        this.MyForm.txtShiharaiYmd.Text = ((DateTime)tableRow["SEISAN_DATE"]).ToShortDateString();
                        //コード
                        this.MyForm.txtCode.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
                        //前回繰越額
                        this.MyForm.txtPrevKurikoshiGaku.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
                        //支払額
                        this.MyForm.txtShiharaiGaku.Text = SetComma(tableRow["KONKAI_SHUKKIN_GAKU"].ToString());
                        //調整額
                        this.MyForm.txtTyoseiGaku.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
                        //今回支払額
                        this.MyForm.txtKonkaiShiharaiGaku.Text = "0";
                        //消費税額
                        this.MyForm.txtSyohizeiGaku.Text = "0";
                        //合計精算額
                        this.MyForm.txtGokeiSeisanGaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));

                        //単月精算の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
                        if (Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString()) ||
                            Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
                        {
                            //①前回繰越額
                            this.MyForm.txtPrevKurikoshiGaku.Text = "";
                            //②支払額
                            this.MyForm.txtShiharaiGaku.Text = "";
                            //③調整額
                            this.MyForm.txtTyoseiGaku.Text = "";
                            //④差引繰越額
                            this.MyForm.txtSashihikiKurikoshiGaku.Text = "";

                            // 項目を非表示にする
                            this.MyForm.前回繰越額.Visible = false;
                            this.MyForm.支払額.Visible = false;
                            this.MyForm.調整額.Visible = false;
                            this.MyForm.差引繰越額.Visible = false;
                            this.MyForm.txtPrevKurikoshiGaku.Visible = false;
                            this.MyForm.txtShiharaiGaku.Visible = false;
                            this.MyForm.txtTyoseiGaku.Visible = false;
                            this.MyForm.txtSashihikiKurikoshiGaku.Visible = false;
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
                                //精算伝票ヘッダデータ設定
                                SetSeisanDenpyoHeader(tableRow);
                                headFlg = false;

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
                                tablePevRow = this.MyForm.ShiharaiDt.Rows[i - 1];
                            }
                            //現在行の次の行
                            DataRow tableNextRow = null;
                            if (i == this.MyForm.ShiharaiDt.Rows.Count - 1)
                            {
                                //現在行の次の行
                                tableNextRow = null;
                            }
                            else
                            {
                                //現在行の次の行
                                tableNextRow = this.MyForm.ShiharaiDt.Rows[i + 1];
                            }

                            //明細データが存在するか
                            if (!(string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString())))
                            {
                                //業者名設定
                                if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者名(出金データ時は出力しない)
                                    if (("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //業者シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "1";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GYOUSHA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"]);
                                    }
                                }
                                //現場名設定
                                if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                                {
                                    //現場名(出金データ時は出力しない)
                                    if (("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //現場シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GENBA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GENBA_NAME2"]);
                                    }
                                }
                                //精算伝票明細データ設定
                                SetSeisanDenpyoMeisei(tableRow, tablePevRow);
                                //伝票金額と消費税
                                if (tableNextRow == null || !tableRow["RANK_DENPYO_1"].Equals(tableNextRow["RANK_DENPYO_1"]))
                                {
                                    //伝票種類が【出金】の場合以外
                                    if (!Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        var denpyou_zei_keisan_kbn_cd = tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString();
                                        var denpyou_zei_kbn_cd = tableRow["DENPYOU_ZEI_KBN_CD"].ToString();
                                        var denpyou_uchizei_gaku = Convert.ToDecimal(tableRow["DENPYOU_UCHIZEI_GAKU"]);
                                        var denpyou_sotozei_gaku = Convert.ToDecimal(tableRow["DENPYOU_SOTOZEI_GAKU"]);
                                        var denpyou_syouhizei = denpyou_uchizei_gaku + denpyou_sotozei_gaku;
                                        if (Const.ConstClass.ZEI_KEISAN_KBN_DENPYOU == denpyou_zei_keisan_kbn_cd)
                                        {
                                            //明細設定用配列
                                            int index = this.MyForm.dgvMeisai.Rows.Add();
                                            DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
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
                                    //「パターン1 or 2かつ出金データ以外」又は「パターン2かつ出金データ」の場合に出力
                                    if ((("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn)) && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        || ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        if ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【出金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【現場計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GENBA_KINGAKU_1"]);

                                        decimal denpyouSotoZeiGaku;
                                        decimal denpyouUchiZeiGaku;
                                        GetDenpyouZei(this.MyForm.ShiharaiDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), tableRow["TSDE_GENBA_CD"].ToString(), out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, true);

                                        //消費税(外税表示)
                                        decimal genbaSoto = Convert.ToDecimal(tableRow["GENBA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                        if (genbaSoto != 0)
                                        {
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = genbaSoto;
                                        }

                                        //備考(内税表示)
                                        if (!("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                        {
                                            decimal genbaUchi = Convert.ToDecimal(tableRow["GENBA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                            if (genbaUchi != 0)
                                            {
                                                row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = GetSyohizei(genbaUchi, Const.ConstClass.DENPYOU_ZEI_KBN_CD_2);
                                            }

                                            if (genbaSoto != 0 || genbaUchi != 0)
                                            {
                                                if (IsSeisanData(tableRow))
                                                {
                                                    row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (Convert.ToString(row.Cells[ConstClass.COL_MEISAI_BIKOU].Value) + Const.ConstClass.ZENKAKU_SPACE + Const.ConstClass.SEISAN_ZEI_EXCEPT).ToString().Trim();
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
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        if (Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【出金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【業者計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GYOUSHA_KINGAKU_1"]);

                                        decimal denpyouSotoZeiGaku;
                                        decimal denpyouUchiZeiGaku;
                                        GetDenpyouZei(this.MyForm.ShiharaiDt, tableRow, tableRow["TSDE_GYOUSHA_CD"].ToString(), null, out denpyouUchiZeiGaku, out denpyouSotoZeiGaku, false);

                                        //消費税(外税表示)
                                        decimal gyoushaSoto = Convert.ToDecimal(tableRow["GYOUSHA_SOTOZEI"]) + denpyouSotoZeiGaku;
                                        if (gyoushaSoto != 0)
                                        {
                                            row.Cells[ConstClass.COL_SHOUHIZEI].Value = gyoushaSoto;
                                        }

                                        //備考(内税表示)
                                        if (!Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            decimal gyoushaUchi = Convert.ToDecimal(tableRow["GYOUSHA_UCHIZEI"]) + denpyouUchiZeiGaku;
                                            if (gyoushaUchi != 0)
                                            {
                                                row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = GetSyohizei(gyoushaUchi, Const.ConstClass.DENPYOU_ZEI_KBN_CD_2);
                                            }

                                            if (gyoushaSoto != 0 || gyoushaUchi != 0)
                                            {
                                                if (IsSeisanData(tableRow))
                                                {
                                                    row.Cells[ConstClass.COL_MEISAI_BIKOU].Value = (Convert.ToString(row.Cells[ConstClass.COL_MEISAI_BIKOU].Value) + Const.ConstClass.ZENKAKU_SPACE + Const.ConstClass.SEISAN_ZEI_EXCEPT).ToString().Trim();
                                                }
                                            }
                                        }
                                    }
                                }

                                if (ConstClass.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstClass.ZEI_KBN_UCHI == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                                {
                                    isSeisanUchizei = true;
                                }
                                if (ConstClass.ZEI_KEISAN_KBN_SEIKYUU == tableRow["DENPYOU_ZEI_KEISAN_KBN_CD"].ToString() && ConstClass.ZEI_KBN_SOTO == tableRow["DENPYOU_ZEI_KBN_CD"].ToString())
                                {
                                    isSeisanSotozei = true;
                                }

                                //精算消費税設定
                                if (tableNextRow == null || !tableRow["RANK_SEISAN_1"].Equals(tableNextRow["RANK_SEISAN_1"]))
                                {
                                    //精算毎消費税(内)
                                    //明細設定用配列
                                    if (isSeisanUchizei)
                                    {
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【精算毎消費税（内）】");
                                        //消費税
                                        row.Cells[ConstClass.COL_SHOUHIZEI].Value = "(" + SetComma((tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]).ToString()) + ")";
                                    }
                                    if (isSeisanSotozei)
                                    {
                                        //精算毎消費税(外)
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【精算毎消費税】");      // No.4221
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
                LogUtility.Error("SetSeisanDenpyo", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }

            this.MyForm.dgvMeisai.IsBrowsePurpose = true;
            return ret;
        }

        /// <summary>
        /// 現場、業者毎の伝票（内税・外税）額の合計を取得
        /// </summary>
        /// <param name="shiharaiDt">精算伝票データテーブル</param>
        /// <param name="tableRow">処理対象の請求伝票行</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="denpyouUchiZeiGaku">伝票内税額合計</param>
        /// <param name="denpyouSotoZeiGaku">伝票外税額合計</param>
        /// <param name="isGenba">true:現場計取得, false:業者計取得</param>
        private void GetDenpyouZei(DataTable shiharaiDt, DataRow tableRow, string gyoushaCd, string genbaCd, out decimal denpyouUchiZeiGaku, out decimal denpyouSotoZeiGaku, bool isGenba)
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
        /// 精算毎データ有無
        /// </summary>
        /// <param name="tableRow"></param>
        /// <returns></returns>
        private bool IsSeisanData(DataRow tableRow)
        {
            decimal seiUchiZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_UTIZEI_GAKU"]);
            decimal seiSotoZei = Convert.ToDecimal(tableRow["TSDK_KONKAI_SEI_SOTOZEI_GAKU"]);

            bool result = 0 < (seiUchiZei + seiSotoZei);

            return result;
        }

        /// <summary>
        /// 精算伝票明細データ設定
        /// </summary>
        private void SetSeisanDenpyoMeisei(DataRow tableRow, DataRow tablePevRow)
        {
            //明細設定用配列
            //object[] row = new object[14];
            //row.Initialize();
            int index = this.MyForm.dgvMeisai.Rows.Add();
            DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
            if (tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["DENPYOU_DATE"]);
            }
            //支払No
            row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value = (tableRow["DENPYOU_NUMBER"]);
            //品名
            row.Cells[ConstClass.COL_HINMEI_NAME].Value = (tableRow["HINMEI_NAME"]);
            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            if (Const.ConstClass.DENPYOU_SHURUI_CD_20 != denpyou_shurui_cd)
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
            if (Const.ConstClass.DENPYOU_SHURUI_CD_20 == denpyou_shurui_cd)
            {
                // 出金伝票は出力しない
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = "";
            }
            else if (Const.ConstClass.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd)
            {
                // 税計算区分が明細毎だったら明細税を出力
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = shouhizei;
            }
            else if (false == String.IsNullOrEmpty(meisai_zei_kbn_cd) && "0" != meisai_zei_kbn_cd)
            {
                // 伝票毎、清算毎でも明細税区分があれば明細税を出力
                row.Cells[ConstClass.COL_SHOUHIZEI].Value = shouhizei;
            }
            else if (Const.ConstClass.ZEI_KEISAN_KBN_MEISAI == denpyou_zei_keisan_kbn_cd && Const.ConstClass.ZEI_KBN_HIKAZEI == denpyou_zei_kbn_cd)
            {
                // 伝票毎、精算毎ではなく非課税は 0 を出力
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
        /// 精算伝票ヘッダデータ設定
        /// </summary>
        private void SetSeisanDenpyoHeader(DataRow tableRow)
        {
            //支払先郵便番号            
            if (!String.IsNullOrEmpty(tableRow["SHIHARAI_SOUFU_POST"].ToString()))
            {
                this.MyForm.txtPostNo.Text = Const.ConstClass.YUBIN + tableRow["SHIHARAI_SOUFU_POST"].ToString();
            }
            //支払先住所1
            this.MyForm.txtAddress1.Text = tableRow["SHIHARAI_SOUFU_ADDRESS1"].ToString();
            //支払先住所2
            this.MyForm.txtAddress2.Text = tableRow["SHIHARAI_SOUFU_ADDRESS2"].ToString();
            //支払先1
            this.MyForm.txtShiharaisaki1.Text = tableRow["SHIHARAI_SOUFU_NAME1"].ToString() + Const.ConstClass.ZENKAKU_SPACE + tableRow["SHIHARAI_SOUFU_KEISHOU1"].ToString();
            //支払先2
            this.MyForm.txtShiharaisaki2.Text = tableRow["SHIHARAI_SOUFU_NAME2"].ToString() + Const.ConstClass.ZENKAKU_SPACE + tableRow["SHIHARAI_SOUFU_KEISHOU2"].ToString();
            // 支払先部署
            this.MyForm.txtBusho.Text = tableRow["SHIHARAI_SOUFU_BUSHO"].ToString();
            //支払先担当者
            if (!string.IsNullOrEmpty(tableRow["SHIHARAI_SOUFU_TANTOU"].ToString()))
            {
                // 支払先部署の値有無によって支払先担当者をつめる
                if (string.IsNullOrEmpty(this.MyForm.txtBusho.Text))
                {
                    this.MyForm.txtBusho.Text = tableRow["SHIHARAI_SOUFU_TANTOU"].ToString() + Const.ConstClass.ZENKAKU_SPACE + "様";
                    this.MyForm.txtTantousha.Text = string.Empty;
                }
                else
                {
                    this.MyForm.txtTantousha.Text = tableRow["SHIHARAI_SOUFU_TANTOU"].ToString() + Const.ConstClass.ZENKAKU_SPACE + "様";
                }
            }
            else
            {
                this.MyForm.txtTantousha.Text = string.Empty;
            }
            //登録番号
            if (string.IsNullOrEmpty(StringUtil.ConverToString(tableRow["TOUROKU_NO"])))
            {
                this.MyForm.txtTourokuNo.Text = string.Empty;
            }
            else
            {
                this.MyForm.txtTourokuNo.Text = "登録番号：" + StringUtil.ConverToString(tableRow["TOUROKU_NO"]);
            }
            //支払番号
            this.MyForm.txtShiharaiNo.Text = tableRow["SEISAN_NUMBER"].ToString();
            //書式区分
            string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();
            //(支払先別）の場合「支払先数」
            if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn))
            {
                this.MyForm.label6.Text = "支払先数";
            }
            //(業者別）の場合「業者数」
            else if (Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn))
            {
                this.MyForm.label6.Text = "業者数";
            }
            //(現場別）の場合「現場数」
            else if (Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
            {
                this.MyForm.label6.Text = "現場数";
            }
            //鑑番号
            this.MyForm.txtGyosyaSu.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
            //自社名1
            this.MyForm.txtJisyaMei1.Text = tableRow["CORP_NAME"].ToString();
            //自社名2
            //＜T_SEISAN_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する場合＞
            if (Const.ConstClass.PRINT_KBN_1.Equals(tableRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                this.MyForm.txtJisyaMei2.Text = tableRow["KYOTEN_NAME"].ToString();
            }
            else
            {
                this.MyForm.txtJisyaMei2.Text = "";
            }
            //自社郵便番号
            if (!String.IsNullOrEmpty(tableRow["KYOTEN_POST"].ToString()))
            {
                this.MyForm.txtJisyaPostNo.Text = Const.ConstClass.YUBIN + tableRow["KYOTEN_POST"].ToString();
            }
            //自社住所1
            this.MyForm.txtJisyaAddress1.Text = tableRow["KYOTEN_ADDRESS1"].ToString();
            //自社住所2
            this.MyForm.txtJisyaAddress2.Text = tableRow["KYOTEN_ADDRESS2"].ToString();
            //電話
            this.MyForm.txtTelNo.Text = "電話  " + tableRow["KYOTEN_TEL"].ToString();
            //FAX
            this.MyForm.txtFaxNo.Text = "FAX  " + tableRow["KYOTEN_FAX"].ToString();
            //備考1
            this.MyForm.txtBiko1.Text = StringUtil.ConverToString(tableRow["BIKOU_1"]);
            //備考2
            this.MyForm.txtBiko2.Text = StringUtil.ConverToString(tableRow["BIKOU_2"]);
            //支払年月日
            this.MyForm.txtShiharaiYmd.Text = ((DateTime)tableRow["SEISAN_DATE"]).ToShortDateString();
            //コード
            this.MyForm.txtCode.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
            //前回繰越額
            this.MyForm.txtPrevKurikoshiGaku.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
            //支払額
            this.MyForm.txtShiharaiGaku.Text = SetComma(tableRow["KONKAI_SHUKKIN_GAKU"].ToString());
            //調整額
            this.MyForm.txtTyoseiGaku.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
            //差引繰越額
            this.MyForm.txtSashihikiKurikoshiGaku.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
            //今回支払額
            this.MyForm.txtKonkaiShiharaiGaku.Text = SetComma(tableRow["TSDK_KONKAI_SHIHARAI_GAKU"].ToString());
            //消費税額
            this.MyForm.txtSyohizeiGaku.Text = SetComma(tableRow["SYOUHIZEIGAKU"].ToString());
            //今回精算額
            this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));
            //今回精算額(【差引繰越額】＋【今回支払額】＋【消費税額】)
            if (!(Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString())
                || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
            {
                this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtSeisangaku.Text, this.MyForm.txtSashihikiKurikoshiGaku.Text));
            }
            //合計精算額
            this.MyForm.txtGokeiSeisanGaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));
            //単月精算の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
            if (Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString()) ||
                Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
            {
                //①前回繰越額
                this.MyForm.txtPrevKurikoshiGaku.Text = "";
                //②支払額
                this.MyForm.txtShiharaiGaku.Text = "";
                //③調整額
                this.MyForm.txtTyoseiGaku.Text = "";
                //④差引繰越額
                this.MyForm.txtSashihikiKurikoshiGaku.Text = "";

                // 項目を非表示にする
                this.MyForm.前回繰越額.Visible = false;
                this.MyForm.支払額.Visible = false;
                this.MyForm.調整額.Visible = false;
                this.MyForm.差引繰越額.Visible = false;
                this.MyForm.txtPrevKurikoshiGaku.Visible = false;
                this.MyForm.txtShiharaiGaku.Visible = false;
                this.MyForm.txtTyoseiGaku.Visible = false;
                this.MyForm.txtSashihikiKurikoshiGaku.Visible = false;
            }
        }

        /// <summary>
        /// 精算伝票データ取得
        /// </summary>
        private DataTable GetSeisandenpyo(string shoshikiKbn, string shoshikiMeisaiKbn, string shukkinMeisaiKbn)
        {
            //精算番号
            string seisanNumber = Shougun.Core.Adjustment.Shiharaimeisaishokakunin.Properties.Settings.Default.SEISAN_NUMBER;
            //①T_SEISAN_DENPYOU.SHOSHIKI_KBNが1：支払先別　且つ　T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            //②T_SEISAN_DENPYOU.SHOSHIKI_KBNが2：業者別　且つ　T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ３：現場毎
            string orderBy = " ";
            if ((Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn)
                    && Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn))
                || (Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                    && Const.ConstClass.SHOSHIKI_MEISAI_KBN_3.Equals(shoshikiMeisaiKbn)))
            {
                orderBy = ", TSDKE.TSDE_GYOUSHA_CD , TSDKE.TSDE_GENBA_CD ";
            }
            //①T_SEISAN_DENPYOU.SHOSHIKI_KBNが1：支払先別　且つ　T_SEISAN_DENPYOU.SHOSHIKI_MEISAI_KBNが ２：業者毎
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
            DataTable seisanDt;
            if (shukkinMeisaiKbn == Const.ConstClass.SHUKKIN_MEISAI_KBN1)
            {
                seisanDt = SeisanDenpyouDao.GetSeisandenpyo(seisanNumber, shukkinMeisaiKbn, orderBy);
            }
            else
            {
                seisanDt = SeisanDenpyouDao.GetSeisandenpyoMeisaiNashi(seisanNumber, shukkinMeisaiKbn, orderBy);

                // 伝票が２伝票以上ある場合
                if (seisanDt.Rows.Count > 1)
                {
                    // 鑑のみの伝票を削除
                    foreach (DataRow row in seisanDt.Rows)
                    {
                        if (row["DENPYOU_SYSTEM_ID"].Equals(DBNull.Value))
                        {
                            seisanDt.Rows.Remove(row);
                            break;
                        }
                    }
                }
            }

            // 並び替え
            if (Const.ConstClass.SHOSHIKI_KBN_1.Equals(shoshikiKbn) && Const.ConstClass.SHOSHIKI_MEISAI_KBN_1.Equals(shoshikiMeisaiKbn))
            {
                // 出金、その他伝票順にソート
                DataRow[] shukkinRows = seisanDt.Select(string.Format("DENPYOU_SHURUI_CD = {0}", Const.ConstClass.DENPYOU_SHURUI_CD_20));
                DataRow[] denpyoRows = seisanDt.Select(string.Format("DENPYOU_SHURUI_CD <> {0} OR DENPYOU_SHURUI_CD IS NULL", Const.ConstClass.DENPYOU_SHURUI_CD_20));

                DataTable dt = seisanDt.Clone();

                foreach (DataRow row in shukkinRows)
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
                return seisanDt;
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

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.MyForm.CSVPrint);

            //登録ボタン(F9)イベント生成
            this.MyForm.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.MyForm.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.UPDATE;

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.MyForm.FormClose);

            this.MyForm.dgvMeisai.CellClick += new DataGridViewCellEventHandler(this.MyForm.selectCellCange);

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            parentForm.OnReceiveMessageEvent += ParentForm_OnReceiveMessageEvent;
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

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
                if (r_framework.Authority.Manager.CheckAuthority("G112", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            if (this.MyForm.ShiharaiDt == null || this.MyForm.ShiharaiDt.Rows.Count == 0)
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
            // 確認メッセージ表示
            //DialogResult dlgres = msgcls.MessageBoxShow("C026");
            DialogResult dlgres = DialogResult.None;
            T_SEISAN_DENPYOU seisanEntity = null;
            List<T_SEISAN_DENPYOU_KAGAMI> arrSeisanKagami = null;
            string titleMsg = string.Empty;
            bool selectedYes = false;
            if (showMsg >0)
            {
                if (this.MyForm.SyoriMode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    //160019 S
                    T_SHUKKIN_KESHIKOMI shukkin = new T_SHUKKIN_KESHIKOMI();
                    shukkin.SEISAN_NUMBER = SqlInt64.Parse(this.MyForm.SeisanNumber);
                    shukkin.DELETE_FLG = SqlBoolean.False;
                    var keshikomiEntitys = ShukkinKeshikomiDao.GetDataForEntity(shukkin);
                    if (keshikomiEntitys != null && 0 < keshikomiEntitys.Length)
                    {
                        msgcls.MessageBoxShow("E151", "精算伝票", "該当の出金伝票を見直して");
                        return false;
                    }
                    //160019 E
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
                LogUtility.DebugMethodStart();
                try
                {
                    //伝票は参照する伝票のとき、T_SEISAN_DENPYOU　Entity を作成する後、備考情報を登録する、
                    if (this.MyForm.SyoriMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                    {
                        seisanEntity = new T_SEISAN_DENPYOU();
                        seisanEntity.SEISAN_NUMBER = Convert.ToInt64(this.MyForm.SeisanNumber);
                        var databind = new DataBinderLogic<T_SEISAN_DENPYOU>(seisanEntity);
                        databind.SetSystemProperty(seisanEntity, false);

                        arrSeisanKagami = this.CreateEntitySeisanUpdate();
                    }
                    // トランザクション開始（エラーまたはコミットしなければ自動でロールバックされる）
                    using (Transaction tran = new Transaction())
                    {
                        if (seisanEntity == null)//伝票削除します
                        {
                            //精算伝票更新
                            SeisanDenpyouDao.UpdateSeisan(this.MyForm.SeisanNumber);
                            //精算伝票_鑑更新
                            SeisanDenpyouDao.UpdateSeisanKagami(this.MyForm.SeisanNumber);
                            //精算明細
                            SeisanDenpyouDao.UpdateSeisanDetail(this.MyForm.SeisanNumber);
                            //出金消込更新
                            SeisanDenpyouDao.UpdateShukkin(this.MyForm.SeisanNumber);
                        }
                        else//伝票を更新します
                        {
                            //精算伝票
                            SeisanDenpyouDao.UpdateSeisanInfo(seisanEntity);
                            //精算伝票鏡_備考
                            if (arrSeisanKagami != null && arrSeisanKagami.Count >0)
                            {
                                foreach (T_SEISAN_DENPYOU_KAGAMI seisanKagamiEntity in arrSeisanKagami)
                                {
                                    SeisanDenpyouDao.UpdateSeisanKagamiBikou(seisanKagamiEntity);
                                }
                            }
                        }
                        // コミット
                        tran.Commit();
                    }

                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
                    if (this.MyForm.SyoriMode == WINDOW_TYPE.DELETE_WINDOW_FLAG
                        && r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai())
                    {
                        this.DeleteInxsSeisan(Convert.ToInt64(this.MyForm.SeisanNumber));
                    }
                    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

                    // 完了メッセージ表示
                    if (showMsg > 0)
                    {
                        msgcls.MessageBoxShow("I001", titleMsg);
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
                    else
                    {
                        throw;
                    }
                }
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
        /// 支払明細書CSV処理
        /// </summary>
        public bool CSVPrint()
        {
            // プレビュ/CSV
            bool printFlg = false;

            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.MyForm.txtShiharaiHakkou.Text))
                {
                    MessageBox.Show(ConstClass.ErrStop1, ConstClass.DialogTitle);
                    return ret;
                }

                DataRow row = this.MyForm.ShiharaiDt.Rows[0];

                ShiharaiDenpyouDto dto = new ShiharaiDenpyouDto();
                dto.TorihikisakiCd = row["TSD_TORIHIKISAKI_CD"].ToString();
                dto.MSysInfo = new DBAccessor().GetSysInfo();
                dto.Meisai = row["SHUKKIN_MEISAI_KBN"].ToString();
                dto.ShiharaiHakkou = this.MyForm.txtShiharaiHakkou.Text;
                dto.ShiharaiPrintDay = ConstClass.SHIHARAI_PRINT_DAY_SIMEBI;    //締日固定
                dto.ShiharaiStyle = ConstClass.SHIHARAI_KEITAI_DATA_SAKUSEIJI;
                dto.ShiharaiPaper = row["YOUSHI_KBN"].ToString();
                dto.HakkoBi = this.strSystemDate;

                ShiharaiDenpyouLogicClass.PreviewShiharaiDenpyou(this.tSeisanDenpyou, dto, printFlg, this.MyForm.txtInvoiceKBN.Text, this.headerForm.ZeiRate_Chk.Checked);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Preview", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 支払明細書プレビュー処理
        /// </summary>
        public bool Preview()
        {
        	// プレビュ/CSV
            bool printFlg = true;

            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                if (string.IsNullOrEmpty(this.MyForm.txtShiharaiHakkou.Text))
                {
                    MessageBox.Show(ConstClass.ErrStop1, ConstClass.DialogTitle);
                    return ret;
                }
                //save bikou value before when printed, beacause when printing is data reloading
                if (this.MyForm.SyoriMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    this.Regist(0);
                }
                DataRow row = this.MyForm.ShiharaiDt.Rows[0];

                ShiharaiDenpyouDto dto = new ShiharaiDenpyouDto();
                dto.TorihikisakiCd = row["TSD_TORIHIKISAKI_CD"].ToString();
                dto.MSysInfo = new DBAccessor().GetSysInfo();
                dto.Meisai = row["SHUKKIN_MEISAI_KBN"].ToString();
                dto.ShiharaiHakkou = this.MyForm.txtShiharaiHakkou.Text;
                dto.ShiharaiPrintDay = ConstClass.SHIHARAI_PRINT_DAY_SIMEBI;    //締日固定
                dto.ShiharaiStyle = ConstClass.SHIHARAI_KEITAI_DATA_SAKUSEIJI;
                dto.ShiharaiPaper = row["YOUSHI_KBN"].ToString();
                dto.HakkoBi = this.strSystemDate;

                ShiharaiDenpyouLogicClass.PreviewShiharaiDenpyou(this.tSeisanDenpyou, dto, printFlg, this.MyForm.txtInvoiceKBN.Text, this.headerForm.ZeiRate_Chk.Checked);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Preview", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
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
        /// 指定した精算番号のデータが存在するか返す
        /// </summary>
        /// <param name="seisanNumber">精算番号</param>
        /// <returns>true:存在する, false:存在しない</returns>
        internal bool IsExistSeisanData(string seisanNumber, out bool catchErr)
        {
            LogUtility.DebugMethodStart();

            bool returnVal = false;
            catchErr = true;
            try
            {
                //精算伝票を取得
                SeisanDenpyouDao = DaoInitUtility.GetComponent<TSDDaoCls>();
                T_SEISAN_DENPYOU tseisandenpyou = SeisanDenpyouDao.GetDataByCd(seisanNumber);

                //削除済みの伝票かチェック
                if (tseisandenpyou.DELETE_FLG == false)
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
                LogUtility.Error("IsExistSeisanData", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExistSeisanData", ex);
                this.msgcls.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(returnVal, catchErr);
            return returnVal;
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
        /// <summary>
        /// set info kagami when bikou change
        /// </summary>
        /// <param name="kagamiNo"></param>
        internal void setSeisanKagamiBikouInfo()
        {
            LogUtility.DebugMethodStart();
            try
            {
                if (string.IsNullOrEmpty(this.MyForm.txtGyosyaSu.Text))
                {
                    return;
                }
                string[] strplit = this.MyForm.txtGyosyaSu.Text.Split(new char[]{'/'});
                if(strplit.Length ==0)
                {
                    return;
                }
                string strKagamiNo = strplit[0];
                var getDataSeisan = from row in this.MyForm.ShiharaiDt.AsEnumerable()
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
        /// create entity seisan update
        /// </summary>
        private List<T_SEISAN_DENPYOU_KAGAMI> CreateEntitySeisanUpdate()
        {
            LogUtility.DebugMethodStart();
            List<T_SEISAN_DENPYOU_KAGAMI> arrSeisanKagami = new List<T_SEISAN_DENPYOU_KAGAMI>();
            try
            {
                //save value bikou before when registry
                this.setSeisanKagamiBikouInfo();
                var groupSeisanKagami = from r in this.MyForm.ShiharaiDt.AsEnumerable()
                                        group r by new
                                        {
                                            SEISAN_NUMBER = Convert.ToInt64(r["SEISAN_NUMBER"]),
                                            KAGAMI_NUMBER = Convert.ToInt32(r["KAGAMI_NUMBER"]),
                                            BIKOU_1 = StringUtil.ConverToString(r["BIKOU_1"]),
                                            BIKOU_2 = StringUtil.ConverToString(r["BIKOU_2"]),
                                        } into grps
                                        select new
                                        {
                                            SEISAN_NUMBER = grps.Key.SEISAN_NUMBER,
                                            KAGAMI_NUMBER = grps.Key.KAGAMI_NUMBER,
                                            BIKOU_1 = grps.Key.BIKOU_1,
                                            BIKOU_2 = grps.Key.BIKOU_2
                                        };
                //create entity
                foreach (var gr in groupSeisanKagami)
                {
                    T_SEISAN_DENPYOU_KAGAMI kagami = new T_SEISAN_DENPYOU_KAGAMI();
                    kagami.SEISAN_NUMBER = gr.SEISAN_NUMBER;
                    kagami.KAGAMI_NUMBER = gr.KAGAMI_NUMBER;
                    kagami.BIKOU_1 = gr.BIKOU_1;
                    kagami.BIKOU_2 = gr.BIKOU_2;
                    arrSeisanKagami.Add(kagami);
                }
                
            }
            catch (Exception ee)
            {
                LogUtility.Error("CreateEntitySeisanUpdate", ee);
                throw;
            }
            LogUtility.DebugMethodEnd();
            return arrSeisanKagami;
        }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        #region InxsApplication - Shiharai option

        private void DeleteInxsSeisan(long seisanNumber)
        {
            var keyDto = new CommonKeyDto()
            {
                Id = seisanNumber
            };

            var parentForm = (BusinessBaseForm)this.MyForm.Parent;
            var requestDto = new
            {
                CommandName = 6,//delete single seisan data
                ShougunParentWindowName = parentForm.Text,
                CommandArgs = new List<CommonKeyDto>() { keyDto }
            };
            RemoteAppCls remoteAppCls = new RemoteAppCls();
            var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
            {
                TransactionId = this.MyForm.transactionId,
                ReferenceID = seisanNumber
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
                if (!r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai())
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
                                && tokenDto.ReferenceID != null && tokenDto.ReferenceID.ToString() == this.MyForm.SeisanNumber)
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

        #endregion
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

        /// <summary>
        /// 精算伝票明細データ設定
        /// ※【適格請求書】対応
        /// </summary>
        public bool SetSeisanDenpyo_invoice()
        {
            bool ret = true;
            try
            {
                this.MyForm.dgvMeisai.IsBrowsePurpose = false;

                //初期化クリア
                this.MyForm.dgvMeisai.Rows.Clear();
                //ヘッダ設定用フラグ
                Boolean headFlg = true;

                for (int i = 0; i < this.MyForm.ShiharaiDt.Rows.Count; i++)
                {
                    //現在の行
                    DataRow tableRow = this.MyForm.ShiharaiDt.Rows[i];

                    //鑑テーブルにテータがあるか
                    if (string.IsNullOrEmpty(tableRow["KAGAMI_NUMBER"].ToString()))
                    {
                        //書式区分
                        string shoshikiKbn = tableRow["SHOSHIKI_KBN"].ToString();

                        //伝票テーブルから設定できる項目のみを設定
                        //支払番号
                        this.MyForm.txtShiharaiNo.Text = tableRow["SEISAN_NUMBER"].ToString();
                        //鑑番号ラベル
                        this.MyForm.txtGyosyaSu.Text = this.MyForm.NowPageNo.ToString() + Const.ConstClass.SLASH + this.MyForm.PageCnt;
                        //差引繰越額
                        this.MyForm.txtSashihikiKurikoshiGaku.Text = SetComma(tableRow["SASIHIKIGAKU"].ToString());
                        //今回精算額
                        this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));
                        //今回精算額(【差引繰越額】＋【今回支払額】＋【消費税額】)
                        if (!(Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString())
                            || Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn)
                            || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn)))
                        {
                            this.MyForm.txtSeisangaku.Text = SetComma(KigakuAdd(this.MyForm.txtSeisangaku.Text, this.MyForm.txtSashihikiKurikoshiGaku.Text));
                        }
                        //支払年月日
                        this.MyForm.txtShiharaiYmd.Text = ((DateTime)tableRow["SEISAN_DATE"]).ToShortDateString();
                        //コード
                        this.MyForm.txtCode.Text = tableRow["TSD_TORIHIKISAKI_CD"].ToString();
                        //前回繰越額
                        this.MyForm.txtPrevKurikoshiGaku.Text = SetComma(tableRow["ZENKAI_KURIKOSI_GAKU"].ToString());
                        //支払額
                        this.MyForm.txtShiharaiGaku.Text = SetComma(tableRow["KONKAI_SHUKKIN_GAKU"].ToString());
                        //調整額
                        this.MyForm.txtTyoseiGaku.Text = SetComma(tableRow["KONKAI_CHOUSEI_GAKU"].ToString());
                        //今回支払額
                        this.MyForm.txtKonkaiShiharaiGaku.Text = "0";
                        //消費税額
                        this.MyForm.txtSyohizeiGaku.Text = "0";
                        //合計精算額
                        this.MyForm.txtGokeiSeisanGaku.Text = SetComma(KigakuAdd(this.MyForm.txtKonkaiShiharaiGaku.Text, this.MyForm.txtSyohizeiGaku.Text));

                        //単月精算の場合 || (業者別）の場合「業者数」|| 現場別）の場合「現場数」
                        if (Const.ConstClass.SEISAN_KEITAI_KBN_1.Equals(tableRow["SHIHARAI_KEITAI_KBN"].ToString()) ||
                            Const.ConstClass.SHOSHIKI_KBN_2.Equals(shoshikiKbn) || Const.ConstClass.SHOSHIKI_KBN_3.Equals(shoshikiKbn))
                        {
                            //①前回繰越額
                            this.MyForm.txtPrevKurikoshiGaku.Text = "";
                            //②支払額
                            this.MyForm.txtShiharaiGaku.Text = "";
                            //③調整額
                            this.MyForm.txtTyoseiGaku.Text = "";
                            //④差引繰越額
                            this.MyForm.txtSashihikiKurikoshiGaku.Text = "";

                            // 項目を非表示にする
                            this.MyForm.前回繰越額.Visible = false;
                            this.MyForm.支払額.Visible = false;
                            this.MyForm.調整額.Visible = false;
                            this.MyForm.差引繰越額.Visible = false;
                            this.MyForm.txtPrevKurikoshiGaku.Visible = false;
                            this.MyForm.txtShiharaiGaku.Visible = false;
                            this.MyForm.txtTyoseiGaku.Visible = false;
                            this.MyForm.txtSashihikiKurikoshiGaku.Visible = false;
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
                                //精算伝票ヘッダデータ設定
                                SetSeisanDenpyoHeader(tableRow);
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
                                tablePevRow = this.MyForm.ShiharaiDt.Rows[i - 1];
                            }
                            //現在行の次の行
                            DataRow tableNextRow = null;
                            if (i == this.MyForm.ShiharaiDt.Rows.Count - 1)
                            {
                                //現在行の次の行
                                tableNextRow = null;
                            }
                            else
                            {
                                //現在行の次の行
                                tableNextRow = this.MyForm.ShiharaiDt.Rows[i + 1];
                            }

                            //明細データが存在するか
                            if (!(string.IsNullOrEmpty(tableRow["ROW_NUMBER"].ToString())))
                            {
                                //業者名設定
                                if (tablePevRow == null || !tableRow["RANK_GYOUSHA_1"].Equals(tablePevRow["RANK_GYOUSHA_1"]))
                                {
                                    //業者名(出金データ時は出力しない)
                                    if (("PARTTEN_1".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //業者シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "1";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GYOUSHA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GYOUSHA_NAME2"]);
                                    }
                                }
                                //現場名設定
                                if (tablePevRow == null || !tableRow["RANK_GENBA_1"].Equals(tablePevRow["RANK_GENBA_1"]))
                                {
                                    //現場名(出金データ時は出力しない)
                                    if (("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn))
                                        && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //現場シェル結合用のフラグ
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        //月日
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["GENBA_NAME1"] + Const.ConstClass.ZENKAKU_SPACE + tableRow["GENBA_NAME2"]);
                                    }
                                }
                                //精算伝票明細データ設定
                                SetSeisanDenpyoMeisei_invoice(tableRow, tablePevRow);

                                //現場金額と消費税設定
                                if (tableNextRow == null || !tableRow["RANK_GENBA_1"].Equals(tableNextRow["RANK_GENBA_1"]))
                                {
                                    //現場金額と消費税
                                    //「パターン1 or 2かつ出金データ以外」又は「パターン2かつ出金データ」の場合に出力
                                    if ((("PARTTEN_2".Equals(HyojiKbn) || "PARTTEN_12".Equals(HyojiKbn)) && !Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        || ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString())))
                                    {
                                        //明細設定用配列
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        if ("PARTTEN_2".Equals(HyojiKbn) && Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【出金計】");
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
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        //品名
                                        if (Const.ConstClass.DENPYOU_SHURUI_CD_20.Equals(tableRow["DENPYOU_SHURUI_CD"].ToString()))
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【出金計】");
                                        }
                                        else
                                        {
                                            row.Cells[ConstClass.COL_HINMEI_NAME].Value = ("【業者計】");
                                        }
                                        //金額
                                        row.Cells[ConstClass.COL_KINGAKU].Value = (tableRow["GYOUSHA_KINGAKU_1"]);
                                    }
                                }

                                //精算消費税設定
                                if (tableNextRow == null || !tableRow["RANK_SEISAN_1"].Equals(tableNextRow["RANK_SEISAN_1"]))
                                {
                                    if ((tableRow["KONKAI_KAZEI_KBN_1"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_2"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_3"].ToString() != "0") ||
                                        (tableRow["KONKAI_KAZEI_KBN_4"].ToString() != "0") ||
                                        (tableRow["KONKAI_HIKAZEI_KBN"].ToString() != "0"))
                                    {
                                        //空白行
                                        int index = this.MyForm.dgvMeisai.Rows.Add();
                                        DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
                                        row.Cells[ConstClass.COL_OUT_FLG].Value = "2";
                                        row.Cells[ConstClass.COL_DENPYOU_DATE].Value = "";

                                        decimal zeiritu;

                                        //課税計（１～４）行を表示
                                        for (int y = 1; y <= 4; y++)
                                        {
                                            if (tableRow["KONKAI_KAZEI_KBN_" + y].ToString() != "0")
                                            {
                                                index = this.MyForm.dgvMeisai.Rows.Add();
                                                row = this.MyForm.dgvMeisai.Rows[index];
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
                                            index = this.MyForm.dgvMeisai.Rows.Add();
                                            row = this.MyForm.dgvMeisai.Rows[index];
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
                LogUtility.Error("SetSeisanDenpyo", ex);
                this.msgcls.MessageBoxShow("E245", "");
                ret = false;
            }

            this.MyForm.dgvMeisai.IsBrowsePurpose = true;
            return ret;
        }

        /// <summary>
        /// 精算伝票明細データ設定
        /// ※【適格請求書】対応
        /// </summary>
        private void SetSeisanDenpyoMeisei_invoice(DataRow tableRow, DataRow tablePevRow)
        {
            //明細設定用配列
            //object[] row = new object[14];
            //row.Initialize();
            int index = this.MyForm.dgvMeisai.Rows.Add();
            DataGridViewRow row = this.MyForm.dgvMeisai.Rows[index];
            if (tablePevRow == null || !tablePevRow["RANK_DENPYO_1"].Equals(tableRow["RANK_DENPYO_1"]) || !tablePevRow["DENPYOU_DATE"].Equals(tableRow["DENPYOU_DATE"]))
            {
                //月日
                row.Cells[ConstClass.COL_DENPYOU_DATE].Value = (tableRow["DENPYOU_DATE"]);
            }
            //支払No
            row.Cells[ConstClass.COL_DENPYOU_NUMBER].Value = (tableRow["DENPYOU_NUMBER"]);
            //品名
            row.Cells[ConstClass.COL_HINMEI_NAME].Value = (tableRow["HINMEI_NAME"]);
            var denpyou_shurui_cd = tableRow["DENPYOU_SHURUI_CD"].ToString();
            if (Const.ConstClass.DENPYOU_SHURUI_CD_20 != denpyou_shurui_cd)
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
            if (Const.ConstClass.DENPYOU_SHURUI_CD_20 == denpyou_shurui_cd)
            {
                // 出金伝票は出力しない
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
