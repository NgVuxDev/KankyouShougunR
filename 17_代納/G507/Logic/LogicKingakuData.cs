using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using r_framework.Utility;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Const;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.Logic
{
    /// <summary>
    /// 金額算出クラス
    /// </summary>
    public class LogicKingakuData
    {
        #region ENUM

        /// <summary>
        /// 表示データ行種類
        /// </summary>
        public enum DisplayDataRowType
        {
            /// <summary>
            /// なし(0)
            /// </summary>
            None = 0,

            /// <summary>
            /// 受入請求(1)
            /// </summary>
            UkeireSeikyu = 1,

            /// <summary>
            /// 受入支払(2)
            /// </summary>
            UkeireShiharai = 2,

            /// <summary>
            /// 出荷請求(3)
            /// </summary>
            ShukkaSeikyu = 3,

            /// <summary>
            /// 出荷支払 (4)
            /// </summary>
            ShukkaShiharai = 4,

            /// <summary>
            /// 合計(5)
            /// </summary>
            TotalValue = 5,
        }

        #endregion

        #region 内部変数

        /// <summary>
        /// 代納番号
        /// </summary>
        private long DainoNo;

        /// <summary>
        /// 代納明細(今回)DAO
        /// </summary>
        private DainoDetailKonkaiDao konkaiDao;

        /// <summary>
        /// 代納明細(前回)DAO
        /// </summary>
        private DainoDetailZenkaiDao zenkaiDao;

        /// <summary>
        /// 金額集計結果(相殺しないパターンで集計）
        /// </summary>
        private List<ShukeiResultDetail> shukeiResultDetailList = null;

        #endregion

        #region プロパティ（代納基本情報）

        /// <summary>
        /// 代納基本情報
        /// </summary>
        internal List<ResultDainoEntryDto> dainoEntryList { set; get; }

        #endregion

        #region プロパティ（代納明細レコード）

        /// <summary>
        ///  [受入請求] 代納詳細情報（今回）
        /// </summary>
        internal List<ResultDainoDetailKonkaiDto> konkaiUkeireSeikyuList { set; get; }
        /// <summary>
        /// [受入支払] 代納詳細情報（今回）
        /// </summary>
        internal List<ResultDainoDetailKonkaiDto> konkaiUkeireShiharaiList { set; get; }
        /// <summary>
        /// [入荷請求] 代納詳細情報（今回）
        /// </summary>
        internal List<ResultDainoDetailKonkaiDto> konkaiShukkaSeikyuList { set; get; }
        /// <summary>
        /// [入荷支払] 代納詳細情報（今回）
        /// </summary>
        internal List<ResultDainoDetailKonkaiDto> konkaiShukkaShiharaiList { set; get; }

        /// <summary>
        /// [受入請求] 代納詳細情報（前回）
        /// </summary>
        internal List<ResultDainoDetailZenkaiDto> zenkaiUkeireSeikyuList { set; get; }
        /// <summary>
        /// [受入支払] 代納詳細情報（前回）
        /// </summary>
        internal List<ResultDainoDetailZenkaiDto> zenkaiUkeireShiharaiList { set; get; }
        /// <summary>
        /// [入荷請求] 代納詳細情報（前回）
        /// </summary>
        internal List<ResultDainoDetailZenkaiDto> zenkaiShukkaSeikyuList { set; get; }
        /// <summary>
        /// [入荷支払] 代納詳細情報（前回）
        /// </summary>
        internal List<ResultDainoDetailZenkaiDto> zenkaiShukkaShiharaiList { set; get; }

        #endregion

        #region プロパティ 税計算区分

        /// <summary>
        /// 受入請求 税計算区分
        /// </summary>
        public ConstClass.ZeiKeisanKbn UkeireSeikyuZeiKeisanKbnCd { set; get; }
        /// <summary>
        /// 受入支払 税計算区分
        /// </summary>
        public ConstClass.ZeiKeisanKbn UkeireShiharaiZeiKeisanKbnCd { set; get; }
        /// <summary>
        /// 出荷請求 税計算区分
        /// </summary>
        public ConstClass.ZeiKeisanKbn ShukkaSeikyuZeiKeisanKbnCd { set; get; }
        /// <summary>
        /// 出荷支払 税計算区分
        /// </summary>
        public ConstClass.ZeiKeisanKbn ShukkaShiharaiZeiKeisanKbnCd { set; get; }

        #endregion

        #region プロパティ 税区分

        /// <summary>
        /// 受入請求 税区分
        /// </summary>
        public ConstClass.ZeiKbnCd UkeireSeikyuZeiKbnCd { set; get; }
        /// <summary>
        /// 受入支払 税区分
        /// </summary>
        public ConstClass.ZeiKbnCd UkeireShiharaiZeiKbnCd { set; get; }

        /// <summary>
        /// 出荷請求 税区分
        /// </summary>
        public ConstClass.ZeiKbnCd ShukkaSeikyuZeiKbnCd { set; get; }
        /// <summary>
        /// 出荷支払 税区分
        /// </summary>
        public ConstClass.ZeiKbnCd ShukkaShiharaiZeiKbnCd { set; get; }

        #endregion

        #region プロパティ 発行区分

        /// <summary>
        /// 受入発行区分
        /// </summary>
        public ConstClass.HakkouKbnCd UkeireHakkouKbnCd { set; get; }

        /// <summary>
        /// 出荷発行区分
        /// </summary>
        public ConstClass.HakkouKbnCd ShukkaHakkouKbnCd { set; get; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicKingakuData(long dainoNumber)
        {
            LogUtility.DebugMethodStart(dainoNumber);
            this.DainoNo = dainoNumber;
            this.konkaiDao = DaoInitUtility.GetComponent<DainoDetailKonkaiDao>();
            this.zenkaiDao = DaoInitUtility.GetComponent<DainoDetailZenkaiDao>();
            LogUtility.DebugMethodEnd();
        }
        #endregion


        #region 代納明細レコードの取得、金額算出

        /// <summary>
        /// 代納明細情報を取得
        /// 税計算区分の選択値が検索パラメータ
        /// </summary>
        /// <param name="form">UIフォーム</param>
        /// <param name="resultDainoEntryList">代納基本情報</param>
        internal void ReadDateilData(UIForm form, List<ResultDainoEntryDto> resultDainoEntryList)
        {
            LogUtility.DebugMethodStart(form, resultDainoEntryList);

            // 代納基本情報を取得
            this.dainoEntryList = resultDainoEntryList;

            // 税計算区分を設定
            this.UkeireSeikyuZeiKeisanKbnCd = ConstClass.GetZeiKeisanKbnCd(form.numtxt_UkeireSeikyuZeiKeisanKbn.Text);
            this.UkeireShiharaiZeiKeisanKbnCd = ConstClass.GetZeiKeisanKbnCd(form.numtxt_UkeireShiharaiZeiKeisanKbn.Text);
            this.ShukkaSeikyuZeiKeisanKbnCd = ConstClass.GetZeiKeisanKbnCd(form.numtxt_ShukkaSeikyuZeiKeisanKbn.Text);
            this.ShukkaShiharaiZeiKeisanKbnCd = ConstClass.GetZeiKeisanKbnCd(form.numtxt_ShukkaShiharaiZeiKeisanKbn.Text);

            // DB検索
            this.SelectDetailData();

            // 金額データ作成
            this.MakeDisplayData();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [DB検索] 代納明細レコード取得

        /// <summary>
        /// DB検索代納明細レコード取得
        /// </summary>
        private void SelectDetailData()
        {
            LogUtility.DebugMethodStart();

            bool meisaiChecked;

            // 受入請求
            meisaiChecked = this.UkeireSeikyuZeiKeisanKbnCd.Equals(ConstClass.ZeiKeisanKbn.Meisai);
            this.konkaiUkeireSeikyuList = this.konkaiDao.GetKonkaiUkeireSeikyuList(this.DainoNo,meisaiChecked);
            this.zenkaiUkeireSeikyuList = this.zenkaiDao.GetZenkaiUkeireSeikyuu(this.DainoNo,meisaiChecked);

            // 受入支払
            meisaiChecked = this.UkeireShiharaiZeiKeisanKbnCd.Equals(ConstClass.ZeiKeisanKbn.Meisai);
            this.konkaiUkeireShiharaiList = this.konkaiDao.GetKonkaiUkeireShiharaiList(this.DainoNo, meisaiChecked);
            this.zenkaiUkeireShiharaiList = this.zenkaiDao.GetZenkaiUkeireShiharai(this.DainoNo, meisaiChecked);

            // 出荷請求
            meisaiChecked = this.ShukkaSeikyuZeiKeisanKbnCd.Equals(ConstClass.ZeiKeisanKbn.Meisai);
            this.konkaiShukkaSeikyuList = this.konkaiDao.GetKonkaiShukkaSeikyuList(this.DainoNo, meisaiChecked);
            this.zenkaiShukkaSeikyuList = this.zenkaiDao.GetZenkaiShukkaSeikyuu(this.DainoNo, meisaiChecked);

            // 出荷支払
            meisaiChecked = this.ShukkaShiharaiZeiKeisanKbnCd.Equals(ConstClass.ZeiKeisanKbn.Meisai);
            this.konkaiShukkaShiharaiList = this.konkaiDao.GetKonkaiShukkaShiharaiList(this.DainoNo, meisaiChecked);
            this.zenkaiShukkaShiharaiList = this.zenkaiDao.GetZenkaiShukkaShiharai(this.DainoNo, meisaiChecked);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [金額データ作成] 受入請求、受入支払、出荷請求、出荷支払を作成

        /// <summary>
        /// 受入請求、受入支払、出荷請求、出荷支払を作成
        /// </summary>
        private void MakeDisplayData()
        {
            LogUtility.DebugMethodStart();

            // UIデータリスト初期化
            this.shukeiResultDetailList = new List<ShukeiResultDetail>();

            // 今回データと前回データの集計結果
            ShukeiResultDetail uiRecord;                    


            // ----------------------------------------------------------------
            // 受入請求
            // 金額算出(前回残高、今回金額、今回税額、今回取引)
            uiRecord = this.MakeShukeiRecordNotSousai(DisplayDataRowType.UkeireSeikyu,
                                                        this.konkaiUkeireSeikyuList,
                                                        this.zenkaiUkeireSeikyuList,
                                                        this.dainoEntryList.Where(t => t.DAINOUU_INOUT_TYPE.Equals(
                                                                                    (int)ConstClass.SelectRecordType.UkeireSeikyu)
                                                                            ).FirstOrDefault()
                                                      );
            // 登録
            this.shukeiResultDetailList.Add(uiRecord);


            // ----------------------------------------------------------------
            // 受入支払
            // 金額算出(前回残高、今回金額、今回税額、今回取引)
            uiRecord = this.MakeShukeiRecordNotSousai(DisplayDataRowType.UkeireShiharai,
                                                        this.konkaiUkeireShiharaiList,
                                                        this.zenkaiUkeireShiharaiList,
                                                        this.dainoEntryList.Where(t => t.DAINOUU_INOUT_TYPE.Equals(
                                                                                    (int)ConstClass.SelectRecordType.UkeireShiharai)
                                                                            ).FirstOrDefault()
                                                      );
            // 登録
            this.shukeiResultDetailList.Add(uiRecord);


            // ----------------------------------------------------------------
            // 出荷請求
            // 金額算出(前回残高、今回金額、今回税額、今回取引)
            uiRecord = this.MakeShukeiRecordNotSousai(DisplayDataRowType.ShukkaSeikyu,
                                                        this.konkaiShukkaSeikyuList,
                                                        this.zenkaiShukkaSeikyuList,
                                                        this.dainoEntryList.Where(t => t.DAINOUU_INOUT_TYPE.Equals(
                                                                                    (int)ConstClass.SelectRecordType.ShukkaSeikyu)
                                                                            ).FirstOrDefault()
                                                      );
            // 登録
            this.shukeiResultDetailList.Add(uiRecord);


            // ----------------------------------------------------------------
            // 出荷支払
            // 金額算出(前回残高、今回金額、今回税額、今回取引)
            uiRecord = this.MakeShukeiRecordNotSousai(DisplayDataRowType.ShukkaShiharai,
                                                        this.konkaiShukkaShiharaiList,
                                                        this.zenkaiShukkaShiharaiList,
                                                        this.dainoEntryList.Where(t => t.DAINOUU_INOUT_TYPE.Equals(
                                                                                    (int)ConstClass.SelectRecordType.ShukkaShiharai)
                                                                            ).FirstOrDefault()
                                                      );
            // 登録
            this.shukeiResultDetailList.Add(uiRecord);


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 相殺しない場合の金額算出
        /// </summary>
        /// <param name="uiRecordType">表示行</param>
        /// <param name="konkaiDetailList">今回明細レコード</param>
        /// <param name="zenkaiDetailList">前回明細レコード</param>
        /// <param name="dainoInfo">代納基本情報</param>
        /// <returns>表示行の集計結果</returns>
        private ShukeiResultDetail MakeShukeiRecordNotSousai(DisplayDataRowType uiRecordType,
                                                            List<ResultDainoDetailKonkaiDto> konkaiDetailList,
                                                            List<ResultDainoDetailZenkaiDto> zenkaiDetailList,
                                                            ResultDainoEntryDto dainoInfo)
        {
            LogUtility.DebugMethodStart(uiRecordType, konkaiDetailList, zenkaiDetailList, dainoInfo);

            // 集計レコード生成、初期化
            ShukeiResultDetail shukeiResultDetail = new ShukeiResultDetail();
            shukeiResultDetail.DisplayRecordType = uiRecordType;
            shukeiResultDetail.hasEnableData = false;

            // 前回残高
            shukeiResultDetail.ZenkaiZandakaSoto = this.GetValueZenkaiZandaka(ConstClass.ZeiKbnCd.SotoZei, zenkaiDetailList, dainoInfo);
            shukeiResultDetail.ZenkaiZandakaUchi = this.GetValueZenkaiZandaka(ConstClass.ZeiKbnCd.UchiZei, zenkaiDetailList, dainoInfo);
            shukeiResultDetail.ZenkaiZandakaHikazei = this.GetValueZenkaiZandaka(ConstClass.ZeiKbnCd.HikaZei, zenkaiDetailList, dainoInfo);

            // 今回金額
            shukeiResultDetail.KonkaiKingakuSoto = this.GetValueKonkaiKingaku(konkaiDetailList);
            shukeiResultDetail.KonkaiKingakuUchi = this.GetValueKonkaiKingaku(konkaiDetailList);
            shukeiResultDetail.KonkaiKingakuHikazei = this.GetValueKonkaiKingaku(konkaiDetailList);

            // 今回税額
            shukeiResultDetail.KonkaiZeigakuSoto = this.GetValueKonkaiZeigaku(ConstClass.ZeiKbnCd.SotoZei, konkaiDetailList);
            shukeiResultDetail.KonkaiZeigakuUchi = this.GetValueKonkaiZeigaku(ConstClass.ZeiKbnCd.UchiZei, konkaiDetailList);
            shukeiResultDetail.KonkaiZeigakuHikazei = this.GetValueKonkaiZeigaku(ConstClass.ZeiKbnCd.HikaZei, konkaiDetailList);

            // 今回取引( 今回金額 ＋ 今回税額　)
            shukeiResultDetail.KonkaiTorihikiSoto = shukeiResultDetail.KonkaiKingakuSoto + shukeiResultDetail.KonkaiZeigakuSoto;
            shukeiResultDetail.KonkaiTorihikiUchi = shukeiResultDetail.KonkaiKingakuUchi + shukeiResultDetail.KonkaiZeigakuUchi;
            shukeiResultDetail.KonkaiTorihikiHikazei = shukeiResultDetail.KonkaiKingakuHikazei + shukeiResultDetail.KonkaiZeigakuHikazei;

            // 相殺金額
            shukeiResultDetail.SousaiSoto = 0;
            shukeiResultDetail.SousaiUchi = 0;
            shukeiResultDetail.SousaiHikazei = 0;

            // 差引残高( 前回残高 ＋ 今回取引　)
            shukeiResultDetail.SasihikiZandakaSoto = shukeiResultDetail.ZenkaiZandakaSoto + shukeiResultDetail.KonkaiTorihikiSoto;
            shukeiResultDetail.SasihikiZandakaUchi = shukeiResultDetail.ZenkaiZandakaUchi + shukeiResultDetail.KonkaiTorihikiUchi;
            shukeiResultDetail.SasihikiZandakaHikazei = shukeiResultDetail.ZenkaiZandakaHikazei + shukeiResultDetail.KonkaiTorihikiHikazei;

            // データの有無
            if ((konkaiDetailList != null && konkaiDetailList.Count > 0)
                   || (zenkaiDetailList != null && zenkaiDetailList.Count > 0))
            {
                // 有り
                shukeiResultDetail.hasEnableData = true;
            }

            LogUtility.DebugMethodEnd(shukeiResultDetail);
            return shukeiResultDetail;
        }

        #endregion

        #region 前回残高、今回金額、今回税額

        /// <summary>
        /// 前回残高取得
        /// </summary>
        /// <param name="zeiKbnCd">税区分</param>
        /// <param name="zenkaiDetailList">前回明細レコード</param>
        /// <param name="dainoInfo">代納基本情報</param>
        /// <returns>前回残高</returns>
        private decimal GetValueZenkaiZandaka(ConstClass.ZeiKbnCd zeiKbnCd, 
                                                List<ResultDainoDetailZenkaiDto> zenkaiDetailList,
                                                ResultDainoEntryDto dainoInfo)
        {
            LogUtility.DebugMethodStart(zeiKbnCd, zenkaiDetailList, dainoInfo);

            // 締め済み前回残高
            decimal shimezumiZenkaiZandaka = 0;

            // 締め済み前回残高の取得
            if (dainoInfo != null)
            {
                shimezumiZenkaiZandaka = dainoInfo.KAISHI_ZANDAKA;
                if (dainoInfo.KONKAI_GAKU.HasValue)
                {
                    shimezumiZenkaiZandaka = dainoInfo.KONKAI_GAKU.Value;
                }
            }

            // 金額( 金額 ＋ 品名別金額 )
            decimal zenkaiKingaku = 0;
            // 消費税( 消費税 ＋ 品名消費税 )
            decimal zenkaiZei = 0;

            // 金額、消費税の取得
            if (zenkaiDetailList != null && zenkaiDetailList.Count > 0)
            {
                // 金額( 金額 ＋ 品名別金額 )
                decimal kingaku = zenkaiDetailList.Sum(t => t.KINGAKU);
                decimal himeiKingaku = zenkaiDetailList.Sum(t => t.HINMEI_KINGAKU);
                zenkaiKingaku = kingaku + himeiKingaku;

                // 消費税( 消費税 ＋ 品名消費税 )
                if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.SotoZei))
                {
                    decimal taxSoto = zenkaiDetailList.Sum(t => t.TAX_SOTO);
                    decimal himeiTaxSoto = zenkaiDetailList.Sum(t => t.HINMEI_TAX_SOTO);
                    zenkaiZei = taxSoto + himeiTaxSoto;
                }
                else if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.UchiZei))
                {
                    decimal taxUchi = zenkaiDetailList.Sum(t => t.TAX_UCHI);
                    decimal himeiTaxUchi = zenkaiDetailList.Sum(t => t.HINMEI_TAX_UCHI);
                    zenkaiZei = taxUchi + himeiTaxUchi;
                }
            }

            // 前回残高( 今回ご請求(支払)額 or 開始買掛(売掛)残高 ＋ 金額合計 ＋ 消費税合計 )
            decimal zenkaiZandaka = shimezumiZenkaiZandaka + zenkaiKingaku + zenkaiZei;

            LogUtility.DebugMethodEnd(zenkaiZandaka);
            return zenkaiZandaka;
        }

        /// <summary>
        /// 今回金額取得
        /// </summary>
        /// <param name="konkaiDetailList"></param>
        /// <returns></returns>
        private decimal GetValueKonkaiKingaku(List<ResultDainoDetailKonkaiDto> konkaiDetailList)
        {
            LogUtility.DebugMethodStart(konkaiDetailList);

            // 金額( 金額 ＋ 品名別金額 )
            decimal konkaiKingaku = 0;
            if (konkaiDetailList != null && konkaiDetailList.Count > 0)
            {
                decimal kingaku = konkaiDetailList.Sum(t => t.KINGAKU);
                decimal himeiKingaku = konkaiDetailList.Sum(t => t.HINMEI_KINGAKU);
                konkaiKingaku = kingaku + himeiKingaku;
            }
            LogUtility.DebugMethodEnd(konkaiKingaku);
            return konkaiKingaku;
        }

        /// <summary>
        /// 今回税額取得
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <param name="konkaiDetailList"></param>
        /// <returns></returns>
        private decimal GetValueKonkaiZeigaku(ConstClass.ZeiKbnCd zeiKbnCd, List<ResultDainoDetailKonkaiDto> konkaiDetailList)
        {
            LogUtility.DebugMethodStart(zeiKbnCd, konkaiDetailList);

            // 今回税額( 消費税 ＋ 品名消費税 )
            decimal konkaiZeigaku = 0;
            
            if (konkaiDetailList != null && konkaiDetailList.Count > 0)
            {
                if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.SotoZei))
                {
                    decimal taxSoto = konkaiDetailList.Sum(t => t.TAX_SOTO);
                    decimal himeiTaxSoto = konkaiDetailList.Sum(t => t.HINMEI_TAX_SOTO);
                    konkaiZeigaku = taxSoto + himeiTaxSoto;
                }
                else if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.UchiZei))
                {
                    decimal taxUchi = konkaiDetailList.Sum(t => t.TAX_UCHI);
                    decimal himeiTaxUchi = konkaiDetailList.Sum(t => t.HINMEI_TAX_UCHI);
                    konkaiZeigaku = taxUchi + himeiTaxUchi;
                }
            }

            LogUtility.DebugMethodEnd(konkaiZeigaku);
            return konkaiZeigaku;
        }

        #endregion

        #region [画面IF] 金額レコード返却

        /// <summary>
        /// [画面IF]
        /// 指定した表示タイプの金額データを返却
        /// UIラジオ設定に応じた金額を返却
        /// </summary>
        /// <param name="form"></param>
        /// <param name="displayType"></param>
        /// <returns></returns>
        internal DisplayDataRow GetDisplayData(UIForm form, DisplayDataRowType displayType)
        {
            LogUtility.DebugMethodStart(form, displayType);

            // ------------------------------------------------------
            // UI設定を内部プロパティへ反映

            // 税区分の設定
            this.UkeireSeikyuZeiKbnCd = ConstClass.GetZeiKbnCd(form.numtxt_UkeireSeikyuZeiKbn.Text);
            this.UkeireShiharaiZeiKbnCd = ConstClass.GetZeiKbnCd(form.numtxt_UkeireShiharaiZeiKbn.Text);
            this.ShukkaSeikyuZeiKbnCd = ConstClass.GetZeiKbnCd(form.numtxt_ShukkaSeikyuZeiKbn.Text);
            this.ShukkaShiharaiZeiKbnCd = ConstClass.GetZeiKbnCd(form.numtxt_ShukkaShiharaiZeiKbn.Text);

            // 発行区分の設定
            this.UkeireHakkouKbnCd = ConstClass.GetHakkouKbnCd(form.numtxt_UkeireHakkoKbn.Text);
            this.ShukkaHakkouKbnCd = ConstClass.GetHakkouKbnCd(form.numtxt_ShukkaHakkoKbn.Text);

            // 金額レコードを返却(データ数に依存せず取得)
            DisplayDataRow res = this.GetDisplayData(displayType, false);

            LogUtility.DebugMethodEnd(res);
            return res;

        }
        #endregion 

        #region [帳票IF] 金額レコード返却

        /// <summary>
        /// [帳票IF]
        /// 指定した表示タイプの金額データを返却
        /// 本メソッド実行前にプロパティ設定を行うこと
        /// </summary>
        /// <param name="displayType">取得タイプ</param>
        /// <param name="checkDataCount">true:データがない場合はALL0、false：データがない場合も前回残高は算出</param>
        /// <returns></returns>
        internal DisplayDataRow GetDisplayData(DisplayDataRowType displayType, bool checkDataCount =true)
        {
            LogUtility.DebugMethodStart(displayType, checkDataCount);

            DisplayDataRow res = new DisplayDataRow();

            if (!DisplayDataRowType.None.Equals(displayType))
            {
                // ------------------------------------------------------
                // 税区分に従ったデータを取得（相殺しない場合のデータ）
                DisplayDataRow ukeireSeikyu = this.GetKingakuRecord(DisplayDataRowType.UkeireSeikyu, this.UkeireSeikyuZeiKbnCd, checkDataCount);
                DisplayDataRow ukeireShiharai = this.GetKingakuRecord(DisplayDataRowType.UkeireShiharai, this.UkeireShiharaiZeiKbnCd, checkDataCount);
                DisplayDataRow shukkaSeikyu = this.GetKingakuRecord(DisplayDataRowType.ShukkaSeikyu, this.ShukkaSeikyuZeiKbnCd, checkDataCount);
                DisplayDataRow shukkaShiharai = this.GetKingakuRecord(DisplayDataRowType.ShukkaShiharai, this.ShukkaShiharaiZeiKbnCd, checkDataCount);


                // ------------------------------------------------------
                // 相殺する場合

                // 受入が相殺の場合
                if (this.UkeireHakkouKbnCd.Equals(ConstClass.HakkouKbnCd.Sousai))
                {
                    // 相殺レコードを作成
                    DisplayDataRow ukeireSeikyuSousai;
                    DisplayDataRow ukeireShiharaiSousai;
                    this.GetSousaiRecord(ukeireSeikyu,
                                         ukeireShiharai,
                                         out ukeireSeikyuSousai,
                                         out ukeireShiharaiSousai);

                    // 受入請求、受入支払を相殺レコードで上書き
                    ukeireSeikyu = ukeireSeikyuSousai;
                    ukeireShiharai = ukeireShiharaiSousai;

                }

                // 出荷が相殺の場合
                if (this.ShukkaHakkouKbnCd.Equals(ConstClass.HakkouKbnCd.Sousai))
                {
                    // 相殺レコードを作成
                    DisplayDataRow shukkaSeikyuSousai;
                    DisplayDataRow shukkaShiharaiSousai;
                    this.GetSousaiRecord(shukkaSeikyu,
                                         shukkaShiharai,
                                         out shukkaSeikyuSousai,
                                         out shukkaShiharaiSousai);
                    // 出荷請求、出荷支払を相殺レコードで上書き
                    shukkaSeikyu = shukkaSeikyuSousai;
                    shukkaShiharai = shukkaShiharaiSousai;
                }


                // ------------------------------------------------------
                // 合計値を取得
                DisplayDataRow total = new DisplayDataRow();
                total.ZenkaiZandaka = this.GetTotalKingaku(ukeireSeikyu.ZenkaiZandaka,
                                                           ukeireShiharai.ZenkaiZandaka,
                                                           shukkaSeikyu.ZenkaiZandaka,
                                                           shukkaShiharai.ZenkaiZandaka);
                total.KonkaiKingaku = this.GetTotalKingaku(ukeireSeikyu.KonkaiKingaku,
                                                           ukeireShiharai.KonkaiKingaku,
                                                           shukkaSeikyu.KonkaiKingaku,
                                                           shukkaShiharai.KonkaiKingaku);
                total.KonkaiZeigaku = this.GetTotalKingaku(ukeireSeikyu.KonkaiZeigaku,
                                                           ukeireShiharai.KonkaiZeigaku,
                                                           shukkaSeikyu.KonkaiZeigaku,
                                                           shukkaShiharai.KonkaiZeigaku);
                total.KonkaiTorihiki = this.GetTotalKingaku(ukeireSeikyu.KonkaiTorihiki,
                                                            ukeireShiharai.KonkaiTorihiki,
                                                            shukkaSeikyu.KonkaiTorihiki,
                                                            shukkaShiharai.KonkaiTorihiki);
                total.SasihikiZandaka = this.GetTotalKingaku(ukeireSeikyu.SasihikiZandaka,
                                                             ukeireShiharai.SasihikiZandaka,
                                                             shukkaSeikyu.SasihikiZandaka,
                                                             shukkaShiharai.SasihikiZandaka);
                total.Sousai = this.GetTotalKingaku(ukeireSeikyu.Sousai,
                                                    ukeireShiharai.Sousai,
                                                    shukkaSeikyu.Sousai,
                                                    shukkaShiharai.Sousai);


                // ------------------------------------------------------
                // 返却
                if (DisplayDataRowType.UkeireSeikyu.Equals(displayType))
                {
                    res = ukeireSeikyu;
                }
                else if (DisplayDataRowType.UkeireShiharai.Equals(displayType))
                {
                    res = ukeireShiharai;
                }
                else if (DisplayDataRowType.ShukkaSeikyu.Equals(displayType))
                {
                    res = shukkaSeikyu;
                }
                else if (DisplayDataRowType.ShukkaShiharai.Equals(displayType))
                {
                    res = shukkaShiharai;
                }
                else if (DisplayDataRowType.TotalValue.Equals(displayType))
                {
                    res = total;
                }

            }


            LogUtility.DebugMethodEnd(res);
            return res;
        }
        
        #endregion 

        #region [帳票用] 指定した検索結果の有無

        /// <summary>
        /// 指定するレコードデータの有無確認
        /// </summary>
        /// <param name="displayType"></param>
        /// <returns></returns>
        public bool HasRecord(DisplayDataRowType displayType)
        {
            bool res = false;

            if (this.shukeiResultDetailList != null && this.shukeiResultDetailList.Count > 0)
            {
                // 該当レコード取得
                var record  = this.shukeiResultDetailList
                                    .Where(t => t.DisplayRecordType.Equals(displayType))
                                    .FirstOrDefault();

                // データ有無取得
                res = record.hasEnableData;
            }

            return res;
        }
        
        #endregion 

        #region 表示用金額レコード取得(内部金額レコードから表示用金額レコードへ変換

        /// <summary>
        /// 表示用金額レコード取得
        /// </summary>
        /// <param name="displayType"></param>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        private DisplayDataRow GetKingakuRecord(DisplayDataRowType displayType, 
                                                ConstClass.ZeiKbnCd zeiKbnCd,
                                                bool checkDataCount)
        {
            LogUtility.DebugMethodStart(displayType, zeiKbnCd, checkDataCount);

            // 表示用レコード
            DisplayDataRow displayDataRow = new DisplayDataRow();

            // 金額データを取得
            ShukeiResultDetail shukeiRecord = this.shukeiResultDetailList
                                                    .Where(t => t.DisplayRecordType.Equals(displayType))
                                                    .FirstOrDefault();

            // 外税の場合
            if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.SotoZei))
            {
                displayDataRow.ZenkaiZandaka = shukeiRecord.ZenkaiZandakaSoto;
                displayDataRow.KonkaiKingaku = shukeiRecord.KonkaiKingakuSoto;
                displayDataRow.KonkaiZeigaku = shukeiRecord.KonkaiZeigakuSoto;
                displayDataRow.KonkaiTorihiki = shukeiRecord.KonkaiTorihikiSoto;
                displayDataRow.SasihikiZandaka = shukeiRecord.SasihikiZandakaSoto;
                displayDataRow.Sousai = shukeiRecord.SousaiSoto;
            }

            // 内税の場合
            else if (zeiKbnCd.Equals(ConstClass.ZeiKbnCd.UchiZei))
            {
                displayDataRow.ZenkaiZandaka = shukeiRecord.ZenkaiZandakaUchi;
                displayDataRow.KonkaiKingaku = shukeiRecord.KonkaiKingakuUchi;
                displayDataRow.KonkaiZeigaku = shukeiRecord.KonkaiZeigakuUchi;
                displayDataRow.KonkaiTorihiki = shukeiRecord.KonkaiTorihikiUchi;
                displayDataRow.SasihikiZandaka = shukeiRecord.SasihikiZandakaUchi;
                displayDataRow.Sousai = shukeiRecord.SousaiUchi;
            }

            // 非課税、未選択の場合
            else
            {
                displayDataRow.ZenkaiZandaka = shukeiRecord.ZenkaiZandakaHikazei;
                displayDataRow.KonkaiKingaku = shukeiRecord.KonkaiKingakuHikazei;
                displayDataRow.KonkaiZeigaku = shukeiRecord.KonkaiZeigakuHikazei;
                displayDataRow.KonkaiTorihiki = shukeiRecord.KonkaiTorihikiHikazei;
                displayDataRow.SasihikiZandaka = shukeiRecord.SasihikiZandakaHikazei;
                displayDataRow.Sousai = shukeiRecord.SousaiHikazei;
            }

            // データ有無確認(データ数を確認、データ件数が0件の場合は0初期化）
            if (checkDataCount && !shukeiRecord.hasEnableData)
            {
                displayDataRow = new DisplayDataRow();
            }

            LogUtility.DebugMethodEnd(displayDataRow);
            return displayDataRow;
        }


        /// <summary>
        /// 相殺時の金額データ取得
        /// </summary>
        /// <param name="seikyu"></param>
        /// <param name="shiharai"></param>
        /// <param name="seikyuSousai"></param>
        /// <param name="shiharaiSousai"></param>
        private void GetSousaiRecord(DisplayDataRow seikyu,
                                            DisplayDataRow shiharai,
                                            out DisplayDataRow seikyuSousai,
                                            out DisplayDataRow shiharaiSousai)
        {

            // 初期化
            seikyuSousai = new DisplayDataRow();
            shiharaiSousai = new DisplayDataRow();

            LogUtility.DebugMethodStart(seikyu, shiharai, seikyuSousai, shiharaiSousai);

            // 値コピー
            seikyu.ToCopy(seikyuSousai);
            shiharai.ToCopy(shiharaiSousai);

            // 相殺金額取得（請求と支払で小さい方）
            decimal sousaiKingaku = Math.Min((decimal)seikyuSousai.SasihikiZandaka, (decimal)shiharaiSousai.SasihikiZandaka);

            // 相殺金額設定
            seikyuSousai.Sousai = (decimal)sousaiKingaku;
            shiharaiSousai.Sousai = (decimal)sousaiKingaku;

            // 差引残高（相殺金額を引く）
            seikyuSousai.SasihikiZandaka = seikyuSousai.SasihikiZandaka - seikyuSousai.Sousai;
            shiharaiSousai.SasihikiZandaka = shiharaiSousai.SasihikiZandaka - shiharaiSousai.Sousai;

            LogUtility.DebugMethodEnd(seikyuSousai, shiharaiSousai);
        }


        /// <summary>
        /// 金額計算
        /// </summary>
        /// <param name="ukeireSeikyu"></param>
        /// <param name="ukeireShiharai"></param>
        /// <param name="shukkaSeikyu"></param>
        /// <param name="shukkaShiharai"></param>
        private decimal GetTotalKingaku(decimal ukeireSeikyu,
                                        decimal ukeireShiharai,
                                        decimal shukkaSeikyu,
                                        decimal shukkaShiharai)
        {
            LogUtility.DebugMethodStart(ukeireSeikyu, ukeireShiharai, shukkaSeikyu, shukkaShiharai);

            // 0初期化
            decimal res = 0;

            // 請求はプラス
            res += ukeireSeikyu;
            res += shukkaSeikyu;

            // 支払はマイナス
            res -= ukeireShiharai;
            res -= shukkaShiharai;

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region Equals/GetHashCode/ToString
        public bool Equals(LogicKingakuData other)
        {
            return this.Equals(other);
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

    }
}
