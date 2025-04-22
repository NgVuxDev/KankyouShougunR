using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Const;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using System.Data;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Report;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Entity;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP;
using System.Drawing.Printing;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.Logic
{
    /// <summary>
    /// 帳票クラス
    /// </summary>
    public class LogicReport
    {
        /// <summary>
        /// 帳票モード(通常：false、印刷プレビュー：true)
        /// UT完了したらこのフィールドをfalseにしてリリースすること
        /// </summary>
        private bool isReportDebugMode = false;

        #region DB定数

        /// <summary>
        /// 伝票区分CD 売上(1)
        /// </summary>
        private const int DEF_DBVAUE_DENPYOU_KBN_CD_URIAGE = 1;

        /// <summary>
        /// 伝票区分CD 支払(2)
        /// </summary>
        private const int DEF_DBVAUE_DENPYOU_KBN_CD_SHIHARAI = 2;

        #endregion

        #region 帳票定数
        /// <summary>
        /// テーブル名
        /// </summary>
        private const string DEF_REPORT_HEADER_TABLE_NAME = "Header";
        /// <summary>
        /// タイトル名
        /// </summary>
        private const string DEF_REPORT_HEADER_TITLE = "TITLE";
        /// <summary>
        /// 担当名
        /// </summary>
        private const string DEF_REPORT_HEADER_TANTOU = "TANTOU";
        /// <summary>
        /// お取引先CD
        /// </summary>
        private const string DEF_REPORT_HEADER_TORIHIKISAKICD = "TORIHIKISAKICD";
        /// <summary>
        /// お取引先名
        /// </summary>
        private const string DEF_REPORT_HEADER_TORIHIKISAKIMEI = "TORIHIKISAKIMEI";
        /// <summary>
        /// お取引先名2
        /// </summary>
        private const string DEF_REPORT_HEADER_TORIHIKISAKIMEI2 = "TORIHIKISAKIMEI2";
        /// <summary>
        /// お取引先名敬称
        /// </summary>
        private const string DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU = "TORIHIKISAKIKEISHOU";
        /// <summary>
        /// お取引先名敬称2
        /// </summary>
        private const string DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU2 = "TORIHIKISAKIKEISHOU2";
        /// <summary>
        /// 伝票No
        /// </summary>
        private const string DEF_REPORT_HEADER_DENPYOUNO = "DENPYOUNO";
        /// <summary>
        /// 乗員
        /// </summary>
        private const string DEF_REPORT_HEADER_JYOUIN = "JYOUIN";
        /// <summary>
        /// 車番
        /// </summary>
        private const string DEF_REPORT_HEADER_SHABAN = "SHABAN";
        /// 車輌CD
        /// </summary>
        private const string DEF_REPORT_HEADER_SHARYOUCD = "SHARYOUCD";     // No.3837
        /// <summary>
        /// 伝票日付
        /// </summary>
        private const string DEF_REPORT_HEADER_DENPYOUDATE = "DENPYOUDATE";
        /// <summary>
        /// 部門名
        /// </summary>
        private const string DEF_REPORT_HEADER_BUMON = "BUMON";


        /// <summary>
        /// テーブル名
        /// </summary>
        private const string DEF_REPORT_FOOTER_TABLE_NAME = "Footer";
        /// <summary>
        /// 現場名
        /// </summary>
        private const string DEF_REPORT_FOOTER_GENBA = "GENBA";
        /// <summary>
        /// 正味合計
        /// </summary>
        private const string DEF_REPORT_FOOTER_SHOUMI_GOUKEI = "SHOUMI_GOUKEI";
        /// <summary>
        /// // 合計金額
        /// </summary>
        private const string DEF_REPORT_FOOTER_GOUKEI_KINGAKU = "GOUKEI_KINGAKU";
        /// <summary>
        /// 備考
        /// </summary>
        private const string DEF_REPORT_FOOTER_BIKOU = "BIKOU";
        /// <summary>
        /// 上段の請求・支払いラベル
        /// </summary>
        private const string DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI1 = "SEIKYUU_SHIHARAI1";
        /// <summary>
        /// 上段の請求・前回残高
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_ZENKAI_ZANDAKA = "UP_ZENKAI_ZANDAKA";
        /// <summary>
        /// 上段の請求・伝票額（税抜）
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_DENPYOUGAKU = "UP_DENPYOUGAKU";
        /// <summary>
        /// 上段の請求・消費税
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_SHOUHIZEI = "UP_SHOUHIZEI";
        /// <summary>
        /// 上段の請求・合計（税込）
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_GOUKEI_ZEIKOMI = "UP_GOUKEI_ZEIKOMI";
        /// <summary>
        /// 上段の請求・御精算額
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_SEISANGAKU = "UP_SEISANGAKU";
        /// <summary>
        /// 上段の請求・差引残高
        /// </summary>
        private const string DEF_REPORT_FOOTER_UP_SASHIHIKIZANDAKA = "UP_SASHIHIKIZANDAKA";
        /// <summary>
        /// 下段の請求・支払いラベル
        /// </summary>
        private const string DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI2 = "SEIKYUU_SHIHARAI2";
        /// <summary>
        /// 下段の請求・前回残高
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_ZENKAI_ZANDAKA = "DOWN_ZENKAI_ZANDAKA";
        /// <summary>
        /// 下段の請求・伝票額（税抜）
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_DENPYOUGAKU = "DOWN_DENPYOUGAKU";
        /// <summary>
        /// 下段の請求・消費税
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_SHOUHIZEI = "DOWN_SHOUHIZEI";
        /// <summary>
        /// 下段の請求・合計（税込）
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_GOUKEI_ZEIKOMI = "DOWN_GOUKEI_ZEIKOMI";
        /// <summary>
        /// 下段の請求・御精算額
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_SEISANGAKU = "DOWN_SEISANGAKU";
        /// <summary>
        /// 下段の請求・差引残高
        /// </summary>
        private const string DEF_REPORT_FOOTER_DOWN_SASHIHIKIZANDAKA = "DOWN_SASHIHIKIZANDAKA";
        /// <summary>
        /// 計量情報計量証明項目1
        /// </summary>
        private const string DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU1 = "KEIRYOU_JYOUHOU1";
        /// <summary>
        /// 計量情報計量証明項目2
        /// </summary>
        private const string DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU2 = "KEIRYOU_JYOUHOU2";
        /// <summary>
        /// 計量情報計量証明項目3
        /// </summary>
        private const string DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU3 = "KEIRYOU_JYOUHOU3";
        /// <summary>
        /// 会社名
        /// </summary>
        private const string DEF_REPORT_FOOTER_CORP_RYAKU_NAME = "CORP_RYAKU_NAME";
        /// <summary>
        /// 拠点
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_NAME = "KYOTEN_NAME";
        /// <summary>
        /// 拠点郵便番号
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_POST = "KYOTEN_POST";     // No.3048
        /// 拠点住所1
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_ADDRESS1 = "KYOTEN_ADDRESS1";
        /// <summary>
        /// 拠点住所2
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_ADDRESS2 = "KYOTEN_ADDRESS2";
        /// <summary>
        /// 拠点電話
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_TEL = "KYOTEN_TEL";
        /// <summary>
        /// 拠点FAX
        /// </summary>
        private const string DEF_REPORT_FOOTER_KYOTEN_FAX = "KYOTEN_FAX";
        /// <summary>
        /// 相殺後金額
        /// </summary>
        private const string DEF_REPORT_FOOTER_SOUSAI_KINGAKU = "SOUSAI_KINGAKU";


        /// <summary>
        /// テーブル名
        /// </summary>
        private const string DEF_REPORT_DETAIL_TABLE_NAME = "Detail";
        /// <summary>
        /// レコードNo(インクリメント)
        /// </summary>
        private const string DEF_REPORT_DETAIL_NUMBER = "NUMBER";
        /// <summary>
        /// 総重量
        /// </summary>
        private const string DEF_REPORT_DETAIL_SOU_JYUURYOU = "SOU_JYUURYOU";
        /// <summary>
        /// 空車重量
        /// </summary>
        private const string DEF_REPORT_DETAIL_KUUSHA_JYUURYOU = "KUUSHA_JYUURYOU";
        /// <summary>
        /// 調整
        /// </summary>
        private const string DEF_REPORT_DETAIL_CHOUSEI = "CHOUSEI";
        /// <summary>
        /// 容器引
        /// </summary>
        private const string DEF_REPORT_DETAIL_YOUKIBIKI = "YOUKIBIKI";
        /// <summary>
        /// 正味
        /// </summary>
        private const string DEF_REPORT_DETAIL_SHOUMI = "SHOUMI";
        /// <summary>
        /// 数量
        /// </summary>
        private const string DEF_REPORT_DETAIL_SUURYOU = "SUURYOU";
        /// <summary>
        /// 数量単位名
        /// </summary>
        private const string DEF_REPORT_DETAIL_FHN_SUURYOU_TANI = "FHN_SUURYOU_TANI";
        /// <summary>
        /// 品名CD
        /// </summary>
        private const string DEF_REPORT_DETAIL_FHN_HINMEICD = "FHN_HINMEICD";
        /// <summary>
        /// 品名
        /// </summary>
        private const string DEF_REPORT_DETAIL_HINMEI = "HINMEI";
        /// <summary>
        /// 単価
        /// </summary>
        private const string DEF_REPORT_DETAIL_TANKA = "TANKA";
        /// <summary>
        /// 金額
        /// </summary>
        private const string DEF_REPORT_DETAIL_KINGAKU = "KINGAKU";
        #endregion

        #region 帳票ENUM

        /// <summary>
        /// 印刷種類
        /// </summary>
        public enum ReportType
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
            /// 受入相殺(3)
            /// </summary>
            UkeireSousai = 3,

            /// <summary>
            /// 出荷請求(4)
            /// </summary>
            ShukkaSeikyu = 4,

            /// <summary>
            /// 出荷支払(5)
            /// </summary>
            ShukkaShiharai = 5,

            /// <summary>
            /// 出荷相殺(6)
            /// </summary>
            ShukkaSousai = 6,
        }

        #endregion

        #region プロパティ（DB検索結果）

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO sysInfo { set; get; }

        /// <summary>
        /// 会社設定
        /// </summary>
        private M_CORP_INFO corpInfo { set; get; }

        /// <summary>
        /// 代納基本情報
        /// </summary>
        private List<ResultDainoEntryDto> resultDainoInfoList { set; get; }

        /// <summary>
        /// 代納明細情報
        /// </summary>
        private LogicKingakuData logicDainoMeisaiData { set; get; }

        #endregion

        #region プロパティ 伝票発行区分

        /// <summary>
        /// 受入請求 伝票発行区分
        /// </summary>
        private ConstClass.DenpyoHakkouKbnCd UkeireSeikyuDenpyoHakkouKbnCd { set; get; }
        /// <summary>
        /// 受入支払 伝票発行区分
        /// </summary>
        private ConstClass.DenpyoHakkouKbnCd UkeireShiharaiDenpyoHakkouKbnCd { set; get; }

        /// <summary>
        /// 出荷請求 伝票発行区分
        /// </summary>
        private ConstClass.DenpyoHakkouKbnCd ShukkaSeikyuDenpyoHakkouKbnCd { set; get; }
        /// <summary>
        /// 出荷支払 伝票発行区分
        /// </summary>
        private ConstClass.DenpyoHakkouKbnCd ShukkaShiharaiDenpyoHakkouKbnCd { set; get; }

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
        public LogicReport()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [公開] 帳票出力処理

        /// <summary>
        /// 帳票処理
        /// </summary>
        /// <param name="form"></param>
        /// <param name="dainoMeisaiLogic"></param>
        /// <param name="dainoInfoList"></param>
        /// <param name="systemInfo"></param>
        /// <param name="corpInfo"></param>
        internal bool Execute(UIForm form, 
                                    LogicKingakuData dainoMeisaiLogic,
                                    List<ResultDainoEntryDto> dainoInfoList,
                                    M_SYS_INFO systemInfo,
                                    M_CORP_INFO comInfo)
        {
            LogUtility.DebugMethodStart(form, dainoMeisaiLogic, dainoInfoList, systemInfo, comInfo);

            bool res = false;

            // 伝票発行区分を設定
            this.UkeireSeikyuDenpyoHakkouKbnCd = ConstClass.GetDenpyoHakkouKbnCd(form.numtxt_UkeireSeikyuDenpyoKbn.Text);
            this.UkeireShiharaiDenpyoHakkouKbnCd = ConstClass.GetDenpyoHakkouKbnCd(form.numtxt_UkeireShiharaiDenpyoKbn.Text);
            this.ShukkaSeikyuDenpyoHakkouKbnCd = ConstClass.GetDenpyoHakkouKbnCd(form.numtxt_ShukkaSeikyuDenpyoKbn.Text);
            this.ShukkaShiharaiDenpyoHakkouKbnCd = ConstClass.GetDenpyoHakkouKbnCd(form.numtxt_ShukkaShiharaiDenpyoKbn.Text);

            // 発行区分の設定
            this.UkeireHakkouKbnCd = ConstClass.GetHakkouKbnCd(form.numtxt_UkeireHakkoKbn.Text);
            this.ShukkaHakkouKbnCd = ConstClass.GetHakkouKbnCd(form.numtxt_ShukkaHakkoKbn.Text);

            // 金額算出ロジッククラスを取得
            this.logicDainoMeisaiData = dainoMeisaiLogic;

            // 代納情報を取得
            this.resultDainoInfoList = dainoInfoList;

            // システム設定を取得
            this.sysInfo = systemInfo;

            // 会社情報を取得
            this.corpInfo = comInfo;

            // レポート出力
            if (0 < this.Execute())
            {
                res = true;
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region [帳票出力] 全体
        /// <summary>
        /// 登録ボタン押下による帳票印刷処理
        /// </summary>
        /// <returns>印刷数</returns>
        private int Execute()
        {
            LogUtility.DebugMethodStart();

            int reportCnt = 0;

            // ---------------------------------------------
            // 受入

            // 個別
            if (ConstClass.HakkouKbnCd.Kobetu.Equals(this.UkeireHakkouKbnCd))
            {
                // 受入請求
                if (this.ReportUkeireSeikyu())
                {
                    reportCnt++;
                }
                // 受入支払
                if (this.ReportUkeireShiharai())
                {
                    reportCnt++;
                }
            }
            // 全て
            else if (ConstClass.HakkouKbnCd.Subete.Equals(this.UkeireHakkouKbnCd))
            {
                // 受入請求
                if(this.ReportUkeireSeikyu())
                {
                    reportCnt++;
                }
                // 受入支払
                if(this.ReportUkeireShiharai())
                {
                    reportCnt++;
                }
                // 受入相殺
                if (this.ReportUkeireSousai())
                {
                    reportCnt++;
                }
            }
            // 相殺
            else if (ConstClass.HakkouKbnCd.Sousai.Equals(this.UkeireHakkouKbnCd))
            {
                // 受入相殺
                if (this.ReportUkeireSousai())
                {
                    reportCnt++;
                }
            }


            // ---------------------------------------------
            // 出荷

            // 個別
            if (ConstClass.HakkouKbnCd.Kobetu.Equals(this.ShukkaHakkouKbnCd))
            {
                // 出荷請求
                if (this.ReportShukkaSeikyu())
                {
                    reportCnt++;
                }
                // 出荷支払
                if (this.ReportShukkaShiharai())
                {
                    reportCnt++;
                }
            }
            // 全て
            else if (ConstClass.HakkouKbnCd.Subete.Equals(this.ShukkaHakkouKbnCd))
            {
                // 出荷請求
                if (this.ReportShukkaSeikyu())
                {
                    reportCnt++;
                }
                // 出荷支払
                if (this.ReportShukkaShiharai())
                {
                    reportCnt++;
                }
                // 出荷相殺
                if (this.ReportShukkaSousai())
                {
                    reportCnt++;
                }
            }
            // 相殺
            else if (ConstClass.HakkouKbnCd.Sousai.Equals(this.ShukkaHakkouKbnCd))
            {
                // 出荷相殺
                if (this.ReportShukkaSousai())
                {
                    reportCnt++;
                }
            }

            LogUtility.DebugMethodEnd(reportCnt);
            return reportCnt;
        }
        #endregion

        #region [帳票出力] 受入請求
        /// <summary>
        /// [帳票] 受入請求
        /// </summary>
        private bool ReportUkeireSeikyu()
        {
            LogUtility.DebugMethodStart();

            bool res = false;

            // 伝票発行＝有り
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.UkeireSeikyuDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.UkeireSeikyu))
                {
                    // レポート出力
                    this.Report(ReportType.UkeireSeikyu);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region [帳票出力] 受入支払
        /// <summary>
        /// [帳票] 受入支払
        /// </summary>
        private bool ReportUkeireShiharai()
        {
            LogUtility.DebugMethodStart();

            bool res = false;

            // 伝票発行＝有り
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.UkeireShiharaiDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.UkeireShiharai))
                {
                    // レポート出力
                    this.Report(ReportType.UkeireShiharai);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region [帳票出力] 受入相殺
        /// <summary>
        /// [帳票] 受入相殺
        /// </summary>
        private bool ReportUkeireSousai()
        {
            LogUtility.DebugMethodStart();

            bool res = false;
            
            // 伝票発行＝有り(請求・支払どちらかに）
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.UkeireSeikyuDenpyoHakkouKbnCd) 
                || ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.UkeireShiharaiDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.UkeireSeikyu)
                    || this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.UkeireShiharai))
                {
                    // レポート出力
                    this.Report(ReportType.UkeireSousai);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region [帳票出力] 出荷請求
        /// <summary>
        /// [帳票] 出荷請求
        /// </summary>
        private bool ReportShukkaSeikyu()
        {
            LogUtility.DebugMethodStart();

            bool res = false;

            // 伝票発行＝有り
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.ShukkaSeikyuDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.ShukkaSeikyu))
                {
                    // レポート出力
                    this.Report(ReportType.ShukkaSeikyu);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        #endregion

        #region [帳票出力] 出荷支払
        /// <summary>
        /// [帳票] 出荷支払
        /// </summary>
        private bool ReportShukkaShiharai()
        {
            LogUtility.DebugMethodStart();

            bool res = false;

            // 伝票発行＝有り
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.ShukkaShiharaiDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.ShukkaShiharai))
                {
                    // レポート出力
                    this.Report(ReportType.ShukkaShiharai);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion

        #region [帳票出力] 出荷相殺
        /// <summary>
        /// 出荷相殺
        /// </summary>
        private bool ReportShukkaSousai()
        {
            LogUtility.DebugMethodStart();

            bool res = false;
            
            // 伝票発行＝有り(請求・支払どちらかに）
            if (ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.ShukkaSeikyuDenpyoHakkouKbnCd)
                || ConstClass.DenpyoHakkouKbnCd.Ari.Equals(this.ShukkaShiharaiDenpyoHakkouKbnCd))
            {
                // 表示レコード有り
                if (this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.ShukkaSeikyu)
                    || this.logicDainoMeisaiData.HasRecord(LogicKingakuData.DisplayDataRowType.ShukkaShiharai))
                {
                    // レポート出力
                    this.Report(ReportType.ShukkaSousai);
                    res = true;
                }
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        #endregion 

        #region 帳票実行処理
        /// <summary>
        /// 帳票印刷処理
        /// </summary>
        /// <param name="reportType"></param>
        private void Report(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            // 帳票クラス
            ReportInfoR338 reportInfo = new ReportInfoR338(WINDOW_ID.R_SHIKIRISYO);

            // 帳票種類によるデータ設定
            this.CreateReportData(reportType, reportInfo);

            // レイアウト設定（最後に設定すること）
            reportInfo.Create(@".\Template\R338-Form.xml", "LAYOUT1", new DataTable());
            reportInfo.Title = "仕切書";

            if (this.isReportDebugMode)
            {
                // 印刷プレビュー（デバッグ用)
                this.DebugDoReport(reportInfo);
            }
            else
            {
                // 通常印刷
                this.DoReport(reportInfo);
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// デバック印刷
        /// </summary>
        /// <param name="reportInfo"></param>
        private void DebugDoReport(ReportInfoR338 reportInfo)
        {
            LogUtility.DebugMethodStart(reportInfo);

            // 印刷プレビュー（デバッグ用）
            using (FormReport formReport = new FormReport(reportInfo, WINDOW_ID.R_SHIKIRISYO))
            {
                formReport.ShowDialog();
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 印刷
        /// </summary>
        /// <param name="reportInfo"></param>
        private void DoReport(ReportInfoR338 reportInfo)
        {
            LogUtility.DebugMethodStart(reportInfo);

            // 通常印刷
            using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, "R338", WINDOW_ID.R_SHIKIRISYO))
            {
                //popup.PrinterSettingInfo = new PrinterSettings();

                // 印刷アプリ初期動作(直印刷)
                popup.PrintInitAction = 1;

                popup.PrintXPS();
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 帳票データ設定
        /// <summary>
        /// 帳票データ設定
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="reportInfo"></param>
        private void CreateReportData(ReportType reportType, ReportInfoR338 reportInfo)
        {
            LogUtility.DebugMethodStart(reportType, reportInfo);

            // ------------------------------------------------
            // ヘッダテーブル作成
            DataTable h = this.CreateHeaderDataTable();

            // ヘッダデータ設定
            this.SetReportDataToHeader(reportType, h);

            // 帳票登録
            reportInfo.DataTableList.Add(DEF_REPORT_HEADER_TABLE_NAME, h);


            // ------------------------------------------------
            // 明細テーブル作成
            DataTable d = this.CreateDetailDataTable();

            // 明細データ設定
            this.SetReportDataToDetail(reportType, d);

            // 帳票登録
            reportInfo.DataTableList.Add(DEF_REPORT_DETAIL_TABLE_NAME, d);


            // ------------------------------------------------
            // フッターテーブル作成
            DataTable f = this.CreateFooterDataTable();

            // フッターデータ設定
            this.SetReportDataToFooter(reportType, f);

            // 帳票登録
            reportInfo.DataTableList.Add(DEF_REPORT_FOOTER_TABLE_NAME, f);


            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region [テーブル作成] ヘッダ
        /// <summary>
        /// ヘッダ用データテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateHeaderDataTable()
        {
            LogUtility.DebugMethodStart();

            DataTable tbl = new DataTable();

            // テーブル名
            tbl.TableName = DEF_REPORT_HEADER_TABLE_NAME;

            // タイトル名
            tbl.Columns.Add(DEF_REPORT_HEADER_TITLE);
            // 担当名
            tbl.Columns.Add(DEF_REPORT_HEADER_TANTOU);
            // お取引先CD
            tbl.Columns.Add(DEF_REPORT_HEADER_TORIHIKISAKICD);
            // お取引先名
            tbl.Columns.Add(DEF_REPORT_HEADER_TORIHIKISAKIMEI);
            // お取引先名2
            tbl.Columns.Add(DEF_REPORT_HEADER_TORIHIKISAKIMEI2);
            // お取引先名敬称
            tbl.Columns.Add(DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU);
            // お取引先名敬称2
            tbl.Columns.Add(DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU2);
            // 伝票No
            tbl.Columns.Add(DEF_REPORT_HEADER_DENPYOUNO);
            // 乗員
            tbl.Columns.Add(DEF_REPORT_HEADER_JYOUIN);
            // 車番
            tbl.Columns.Add(DEF_REPORT_HEADER_SHABAN);
            // 車輌CD
            tbl.Columns.Add(DEF_REPORT_HEADER_SHARYOUCD);   // No.3837
            // 伝票日付
            tbl.Columns.Add(DEF_REPORT_HEADER_DENPYOUDATE);
            // 部門名
            tbl.Columns.Add(DEF_REPORT_HEADER_BUMON);

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }
        #endregion

        #region [テーブル作成] 明細
        /// <summary>
        /// 明細用データテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDetailDataTable()
        {
            LogUtility.DebugMethodStart();

            DataTable tbl = new DataTable();

            // テーブル名
            tbl.TableName = DEF_REPORT_DETAIL_TABLE_NAME;

            // No
            tbl.Columns.Add(DEF_REPORT_DETAIL_NUMBER);
            // 総重量
            tbl.Columns.Add(DEF_REPORT_DETAIL_SOU_JYUURYOU);
            // 空車重量
            tbl.Columns.Add(DEF_REPORT_DETAIL_KUUSHA_JYUURYOU);
            // 調整
            tbl.Columns.Add(DEF_REPORT_DETAIL_CHOUSEI);
            // 容器引
            tbl.Columns.Add(DEF_REPORT_DETAIL_YOUKIBIKI);
            // 正味
            tbl.Columns.Add(DEF_REPORT_DETAIL_SHOUMI);
            // 数量
            tbl.Columns.Add(DEF_REPORT_DETAIL_SUURYOU);
            // 数量単位名
            tbl.Columns.Add(DEF_REPORT_DETAIL_FHN_SUURYOU_TANI);
            // 品名CD
            tbl.Columns.Add(DEF_REPORT_DETAIL_FHN_HINMEICD);
            // 品名
            tbl.Columns.Add(DEF_REPORT_DETAIL_HINMEI);
            // 単価
            tbl.Columns.Add(DEF_REPORT_DETAIL_TANKA);
            // 金額
            tbl.Columns.Add(DEF_REPORT_DETAIL_KINGAKU);

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }
        #endregion

        #region [テーブル作成] フッター
        /// <summary>
        /// フッター用データテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateFooterDataTable()
        {
            LogUtility.DebugMethodStart();

            DataTable tbl = new DataTable();

            // テーブル名
            tbl.TableName = DEF_REPORT_FOOTER_TABLE_NAME;

            // 現場名
            tbl.Columns.Add(DEF_REPORT_FOOTER_GENBA);
            // 正味合計
            tbl.Columns.Add(DEF_REPORT_FOOTER_SHOUMI_GOUKEI);
            // 合計金額
            tbl.Columns.Add(DEF_REPORT_FOOTER_GOUKEI_KINGAKU);
            // 備考
            tbl.Columns.Add(DEF_REPORT_FOOTER_BIKOU);
            // 上段の請求・支払いラベル
            tbl.Columns.Add(DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI1);
            // 上段の請求・前回残高
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_ZENKAI_ZANDAKA);
            // 上段の請求・伝票額（税抜）
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_DENPYOUGAKU);
            // 上段の請求・消費税
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_SHOUHIZEI);
            // 上段の請求・合計（税込）
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_GOUKEI_ZEIKOMI);
            // 上段の請求・御精算額
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_SEISANGAKU);
            // 上段の請求・差引残高
            tbl.Columns.Add(DEF_REPORT_FOOTER_UP_SASHIHIKIZANDAKA);
            // 下段の請求・支払いラベル
            tbl.Columns.Add(DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI2);
            // 下段の請求・前回残高
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_ZENKAI_ZANDAKA);
            // 下段の請求・伝票額（税抜）
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_DENPYOUGAKU);
            // 下段の請求・消費税
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_SHOUHIZEI);
            // 下段の請求・合計（税込）
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_GOUKEI_ZEIKOMI);
            // 下段の請求・御精算額
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_SEISANGAKU);
            // 下段の請求・差引残高
            tbl.Columns.Add(DEF_REPORT_FOOTER_DOWN_SASHIHIKIZANDAKA);

            // 計量情報計量証明項目1
            tbl.Columns.Add(DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU1);
            // 計量情報計量証明項目2
            tbl.Columns.Add(DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU2);
            // 計量情報計量証明項目3
            tbl.Columns.Add(DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU3);

            // 会社名
            tbl.Columns.Add(DEF_REPORT_FOOTER_CORP_RYAKU_NAME);
            // 拠点
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_NAME);
            // 拠点郵便番号
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_POST);    // No.3048
            // 拠点住所1
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_ADDRESS1);
            // 拠点住所2
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_ADDRESS2);
            // 拠点電話
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_TEL);
            // 拠点FAX
            tbl.Columns.Add(DEF_REPORT_FOOTER_KYOTEN_FAX);

            // 相殺後金額
            tbl.Columns.Add(DEF_REPORT_FOOTER_SOUSAI_KINGAKU);

            LogUtility.DebugMethodEnd(tbl);
            return tbl;
        }
        #endregion

        #region [データ設定] ヘッダ
        /// <summary>
        /// ヘッダテーブル設定
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="tbl"></param>
        private void SetReportDataToHeader(ReportType reportType, DataTable tbl)
        {
            LogUtility.DebugMethodStart(reportType, tbl);

            string tmp;

            // 帳票種類による代納情報取得
            ResultDainoEntryDto dainoInfo = this.GetDainoInfo(reportType);

            // レコード取得
            DataRow row = tbl.NewRow();

            // タイトル
            row[DEF_REPORT_HEADER_TITLE] = this.GetHeaderTitle(reportType);

            // 担当者
            row[DEF_REPORT_HEADER_TANTOU] = string.Empty;

            // 取引先CD
            tmp = dainoInfo.TORIHIKISAKI_CD;
            row[DEF_REPORT_HEADER_TORIHIKISAKICD] = tmp != null ? tmp : string.Empty;

            // 取引先名
            tmp = dainoInfo.TORIHIKISAKI_NAME1;
            row[DEF_REPORT_HEADER_TORIHIKISAKIMEI] = tmp != null ? tmp : string.Empty;

            // 取引先名2
            tmp = dainoInfo.TORIHIKISAKI_NAME2;
            row[DEF_REPORT_HEADER_TORIHIKISAKIMEI2] = tmp != null ? tmp : string.Empty;

            // 取引先名敬称
            tmp = dainoInfo.TORIHIKISAKI_KEISHOU1;
            row[DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU] = tmp != null ? tmp : string.Empty;

            // 取引先名敬称2
            tmp = dainoInfo.TORIHIKISAKI_KEISHOU2;
            row[DEF_REPORT_HEADER_TORIHIKISAKIKEISHOU2] = tmp != null ? tmp : string.Empty;

            // 伝票番号
            row[DEF_REPORT_HEADER_DENPYOUNO] = dainoInfo.DAINOU_NUMBER.ToString();

            // 乗員
            row[DEF_REPORT_HEADER_JYOUIN] = string.Empty;

            // 車番
            row[DEF_REPORT_HEADER_SHABAN] = string.Empty;

            // 車輌CD
            row[DEF_REPORT_HEADER_SHARYOUCD] = string.Empty;    // No.3837

            // 伝票日付
            tmp = dainoInfo.DENPYOU_DATE.IsNull ? string.Empty : dainoInfo.DENPYOU_DATE.Value.ToString("yyyy/MM/dd"); 
            row[DEF_REPORT_HEADER_DENPYOUDATE] = tmp;

            // 部門名
            tmp = dainoInfo.BUMON_NAME_RYAKU;
            row[DEF_REPORT_HEADER_BUMON] = tmp != null ? tmp : string.Empty;

            // 登録
            tbl.Rows.Add(row);

            LogUtility.DebugMethodEnd();
        }
        #endregion 
        
        #region [データ設定] 明細
        /// <summary>
        /// 明細テーブル設定
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="tbl"></param>
        private void SetReportDataToDetail(ReportType reportType, DataTable tbl)
        {
            LogUtility.DebugMethodStart(reportType, tbl);

            string tmp;

            // 帳票種類による代納明細レコード（請求）
            List<ResultDainoDetailKonkaiDto> detailSeikyuList = this.GetiDainoMeisaiSeikyuRecordList(reportType);

            // 帳票種類による代納明細レコード（支払）
            List<ResultDainoDetailKonkaiDto> detailShiharaiList = this.GetiDainoMeisaiShiharaiRecordList(reportType);

            // 表示明細レコード（請求＋支払）
            List<ResultDainoDetailKonkaiDto> detailList = detailSeikyuList.Union(detailShiharaiList)
                                             .OrderBy(t => t.ROW_NO)
                                             .ThenBy(t => t.DENPYOU_KBN_CD)
                                             .ToList();

            // 支払系(受入、出荷)の場合出力順を変更
            if (ReportType.UkeireShiharai.Equals(reportType)
                || ReportType.ShukkaShiharai.Equals(reportType))
            {
                detailList = detailList.OrderBy(t => t.ROW_NO)
                                        .ThenByDescending(t => t.DENPYOU_KBN_CD)
                                        .ToList();
            }

            // 明細ループ
            int recordCnt = 0;
            foreach (ResultDainoDetailKonkaiDto detailRecord in detailList)
            {
                // レコード取得
                DataRow row = tbl.NewRow();

                // Noラベル
                recordCnt++;
                tmp = recordCnt.ToString();
                row[DEF_REPORT_DETAIL_NUMBER] = tmp != null ? tmp : string.Empty;

                // 総重量
                row[DEF_REPORT_DETAIL_SOU_JYUURYOU] = string.Empty;

                // 空車重量ラベル
                row[DEF_REPORT_DETAIL_KUUSHA_JYUURYOU] = string.Empty;

                // 調整ラベル
                row[DEF_REPORT_DETAIL_CHOUSEI] = string.Empty;

                // 容器引ラベル
                row[DEF_REPORT_DETAIL_YOUKIBIKI] = string.Empty;

                // 正味ラベル
                tmp = detailRecord.NET_JYUURYOU.ToString(this.sysInfo.SYS_JYURYOU_FORMAT);
                row[DEF_REPORT_DETAIL_SHOUMI] = tmp != null ? tmp : string.Empty;

                // 数量ラベル
                tmp = detailRecord.SUURYOU.ToString(this.sysInfo.SYS_SUURYOU_FORMAT);
                row[DEF_REPORT_DETAIL_SUURYOU] = tmp != null ? tmp : string.Empty;

                // 数量単位ラベル
                tmp = detailRecord.UNIT_NAME_RYAKU;
                row[DEF_REPORT_DETAIL_FHN_SUURYOU_TANI] = tmp != null ? tmp : string.Empty;

                // 品名CDラベル
                tmp = detailRecord.HINMEI_CD;
                row[DEF_REPORT_DETAIL_FHN_HINMEICD] = tmp != null ? tmp : string.Empty;

                // 品名ラベル
                tmp = detailRecord.HINMEI_NAME;
                row[DEF_REPORT_DETAIL_HINMEI] = tmp != null ? tmp : string.Empty;

                // 単価ラベル
                tmp = this.GetDetailTanka(reportType, detailRecord);
                row[DEF_REPORT_DETAIL_TANKA] = tmp != null ? tmp : string.Empty;

                // 金額ラベル
                tmp = this.GetDetailKingaku(reportType, detailRecord);
                row[DEF_REPORT_DETAIL_KINGAKU] = tmp != null ? tmp : string.Empty;

                // 登録
                tbl.Rows.Add(row);
            }


            LogUtility.DebugMethodEnd();
        }
        #endregion 
        
        #region [データ設定] フッター
        /// <summary>
        /// フッターテーブル設定
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="tbl"></param>
        private void SetReportDataToFooter(ReportType reportType, DataTable tbl)
        {
            LogUtility.DebugMethodStart(reportType, tbl);

            string tmp;

            // 帳票種類による代納情報取得
            ResultDainoEntryDto dainoInfo = this.GetDainoInfo(reportType);

            // 帳票種類による代納明細レコード（請求）
            List<ResultDainoDetailKonkaiDto> detailSeikyuList = this.GetiDainoMeisaiSeikyuRecordList(reportType);

            // 帳票種類による代納明細レコード（支払）
            List<ResultDainoDetailKonkaiDto> detailShiharaiList = this.GetiDainoMeisaiShiharaiRecordList(reportType);

            // 代納明細レコード（請求・支払）
            List<ResultDainoDetailKonkaiDto> totalDetailList = detailSeikyuList.Union(detailShiharaiList).ToList();

            // 帳票種類による表示金額レコード（請求）
            DisplayDataRow displayRecordSeikyu = this.GetDisplayRecordSeikyu(reportType);
            
            // 帳票種類による表示金額レコード（支払）
            DisplayDataRow displayRecordShiharai = this.GetDisplayRecordShiharai(reportType);


            // レコード取得
            DataRow row = tbl.NewRow();

            // 現場名
            tmp = dainoInfo.GENBA_NAME_RYAKU;
            row[DEF_REPORT_FOOTER_GENBA] = tmp != null ? tmp : string.Empty;

            // 正味合計
            tmp = totalDetailList.Sum(t => t.NET_JYUURYOU).ToString(this.sysInfo.SYS_JYURYOU_FORMAT);
            row[DEF_REPORT_FOOTER_SHOUMI_GOUKEI] = tmp;

            // ******************** 今回金額合計 *******************************
            tmp = this.GetFooterKonkaiKingakuGokei(reportType, displayRecordSeikyu, displayRecordShiharai);
            row[DEF_REPORT_FOOTER_GOUKEI_KINGAKU] = tmp;
            // *****************************************************************

            // 備考
            row[DEF_REPORT_FOOTER_BIKOU] = string.Empty;  // 固定


            // 請求ラベル
            row[DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI1] = "請求"; // 固定

            // 請求・前回残高
            tmp = displayRecordSeikyu.ZenkaiZandaka.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_ZENKAI_ZANDAKA] = tmp;
            // 請求・伝票額（税抜き）
            tmp = displayRecordSeikyu.KonkaiKingaku.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_DENPYOUGAKU] = tmp;
            // 請求・消費税
            tmp = displayRecordSeikyu.KonkaiZeigaku.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_SHOUHIZEI] = tmp;
            // 請求・合計（税込み）
            tmp = displayRecordSeikyu.KonkaiTorihiki.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_GOUKEI_ZEIKOMI] = tmp;
            // 請求・ご清算額
            tmp = displayRecordSeikyu.Sousai.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_SEISANGAKU] = tmp;
            // 請求・差引残高
            tmp = displayRecordSeikyu.SasihikiZandaka.ToString("#,##0");
            row[DEF_REPORT_FOOTER_UP_SASHIHIKIZANDAKA] = tmp;
            

            // 支払ラベル
            row[DEF_REPORT_FOOTER_SEIKYUU_SHIHARAI2] = "支払"; // 固定
            
            // 支払・前回残高
            tmp = displayRecordShiharai.ZenkaiZandaka.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_ZENKAI_ZANDAKA] = tmp;
            // 支払・伝票額（税抜き）
            tmp = displayRecordShiharai.KonkaiKingaku.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_DENPYOUGAKU] = tmp;
            // 支払・消費税
            tmp = displayRecordShiharai.KonkaiZeigaku.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_SHOUHIZEI] = tmp;
            // 支払・合計（税込み）
            tmp = displayRecordShiharai.KonkaiTorihiki.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_GOUKEI_ZEIKOMI] = tmp;
            // 支払・ご清算額
            tmp = displayRecordShiharai.Sousai.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_SEISANGAKU] = tmp;
            // 支払・差引残高
            tmp = displayRecordShiharai.SasihikiZandaka.ToString("#,##0");
            row[DEF_REPORT_FOOTER_DOWN_SASHIHIKIZANDAKA] = tmp;
            

            // 計量情報計量証明項目1
            tmp = this.sysInfo.KEIRYOU_SHOUMEI_1;
            row[DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU1] = tmp != null ? tmp : string.Empty;

            // 計量情報計量証明項目2
            tmp = this.sysInfo.KEIRYOU_SHOUMEI_2;
            row[DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU2] = tmp != null ? tmp : string.Empty;

            // 計量情報計量証明項目3
            tmp = this.sysInfo.KEIRYOU_SHOUMEI_3;
            row[DEF_REPORT_FOOTER_KEIRYOU_JYOUHOU3] = tmp != null ? tmp : string.Empty;

            // 会社名
            tmp = corpInfo.CORP_NAME;
            row[DEF_REPORT_FOOTER_CORP_RYAKU_NAME] = tmp != null ? tmp : string.Empty;

            // 拠点
            tmp = dainoInfo.KYOTEN_NAME_RYAKU;
            row[DEF_REPORT_FOOTER_KYOTEN_NAME] = tmp != null ? tmp : string.Empty;

            // No.3048-->
            // 拠点郵便番号
            tmp = dainoInfo.KYOTEN_POST;
            row[DEF_REPORT_FOOTER_KYOTEN_POST] = tmp != null ? tmp : string.Empty;
            // No.3048<--

            // 拠点住所1
            tmp = dainoInfo.KYOTEN_ADDRESS1;
            row[DEF_REPORT_FOOTER_KYOTEN_ADDRESS1] = tmp != null ? tmp : string.Empty;

            // 拠点住所2
            tmp = dainoInfo.KYOTEN_ADDRESS2;
            row[DEF_REPORT_FOOTER_KYOTEN_ADDRESS2] = tmp != null ? tmp : string.Empty;

            // 拠点電話
            tmp = dainoInfo.KYOTEN_TEL;
            row[DEF_REPORT_FOOTER_KYOTEN_TEL] = tmp != null ? tmp : string.Empty;

            // 拠点FAX
            tmp = dainoInfo.KYOTEN_FAX;
            row[DEF_REPORT_FOOTER_KYOTEN_FAX] = tmp != null ? tmp : string.Empty;

            // ***************** 相殺後金額 ********************************
            tmp = this.GetFooterSousaiGoKingaku(reportType, displayRecordSeikyu, displayRecordShiharai);
            row[DEF_REPORT_FOOTER_SOUSAI_KINGAKU] = tmp != null ? tmp : string.Empty;
            // *****************************************************************

            // 登録
            tbl.Rows.Add(row);

            LogUtility.DebugMethodEnd();
        }
        #endregion 
        
        #region 検索結果データ取得処理
        /// <summary>
        /// 代納情報取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private ResultDainoEntryDto GetDainoInfo(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            ResultDainoEntryDto res = null;
            ConstClass.SelectRecordType selectType = ConstClass.SelectRecordType.None;


            // 受入請求時、受入相殺時
            if (ReportType.UkeireSeikyu.Equals(reportType)
                    || ReportType.UkeireSousai.Equals(reportType))
            {
                selectType = ConstClass.SelectRecordType.UkeireSeikyu;
            }

            // 受入支払時
            else if (ReportType.UkeireShiharai.Equals(reportType))
            {
                selectType = ConstClass.SelectRecordType.UkeireShiharai;
            }

            // 出荷請求時、出荷相殺時
            else if (ReportType.ShukkaSeikyu.Equals(reportType)
                        || ReportType.ShukkaSousai.Equals(reportType))
            {
                selectType = ConstClass.SelectRecordType.ShukkaSeikyu;
            }

            // 出荷支払時
            else if (ReportType.ShukkaShiharai.Equals(reportType))
            {
                selectType = ConstClass.SelectRecordType.ShukkaShiharai;
            }

            // 該当する代納情報を取得
            res = this.resultDainoInfoList.Where(t => t.DAINOUU_INOUT_TYPE.Equals((int)selectType))
                                          .FirstOrDefault();

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        /// <summary>
        /// 代納明細（請求）レコード取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private List<ResultDainoDetailKonkaiDto> GetiDainoMeisaiSeikyuRecordList(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            List<ResultDainoDetailKonkaiDto> res = new List<ResultDainoDetailKonkaiDto>();

            // 受入時(出荷、支払、相殺）
            if (ReportType.UkeireSeikyu.Equals(reportType)
                    || ReportType.UkeireShiharai.Equals(reportType)
                    || ReportType.UkeireSousai.Equals(reportType))
            {
                // 受入請求を返却
                res.AddRange(this.logicDainoMeisaiData.konkaiUkeireSeikyuList);
            }

            // 出荷時(出荷、支払、相殺）
            else if (ReportType.ShukkaSeikyu.Equals(reportType)
                    || ReportType.ShukkaShiharai.Equals(reportType)
                    || ReportType.ShukkaSousai.Equals(reportType))
            {
                // 出荷請求を返却
                res.AddRange(this.logicDainoMeisaiData.konkaiShukkaSeikyuList);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        /// <summary>
        /// 代納明細（支払）レコード取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private List<ResultDainoDetailKonkaiDto> GetiDainoMeisaiShiharaiRecordList(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            List<ResultDainoDetailKonkaiDto> res = new List<ResultDainoDetailKonkaiDto>();

            // 受入時(出荷、支払、相殺）
            if (ReportType.UkeireSeikyu.Equals(reportType) 
                    || ReportType.UkeireShiharai.Equals(reportType)
                    || ReportType.UkeireSousai.Equals(reportType))
            {
                // 受入支払を返却
                res.AddRange(this.logicDainoMeisaiData.konkaiUkeireShiharaiList);
            }

            // 出荷時(出荷、支払、相殺）
            else if (ReportType.ShukkaSeikyu.Equals(reportType)
                    || ReportType.ShukkaShiharai.Equals(reportType)
                    || ReportType.ShukkaSousai.Equals(reportType))
            {
                // 出荷支払を返却
                res.AddRange(this.logicDainoMeisaiData.konkaiShukkaShiharaiList);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }
        /// <summary>
        /// 帳票種類による表示金額（請求）レコード取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private DisplayDataRow GetDisplayRecordSeikyu(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            // 初期化
            LogicKingakuData.DisplayDataRowType displayType = LogicKingakuData.DisplayDataRowType.None;

            // 変更前の発行区分
            ConstClass.HakkouKbnCd privUkeireHakkouKbnCd = this.logicDainoMeisaiData.UkeireHakkouKbnCd;
            ConstClass.HakkouKbnCd privShukkaHakkouKbnCd = this.logicDainoMeisaiData.ShukkaHakkouKbnCd;

            // 受入請求時
            if (ReportType.UkeireSeikyu.Equals(reportType))
            {
                this.logicDainoMeisaiData.UkeireHakkouKbnCd = ConstClass.HakkouKbnCd.Kobetu;
                displayType = LogicKingakuData.DisplayDataRowType.UkeireSeikyu;
            }
            // 受入相殺時
            else if (ReportType.UkeireSousai.Equals(reportType))
            {
                this.logicDainoMeisaiData.UkeireHakkouKbnCd = ConstClass.HakkouKbnCd.Sousai;
                displayType = LogicKingakuData.DisplayDataRowType.UkeireSeikyu;
            }

            // 出荷請求時
            else if (ReportType.ShukkaSeikyu.Equals(reportType))
            {
                this.logicDainoMeisaiData.ShukkaHakkouKbnCd = ConstClass.HakkouKbnCd.Kobetu;
                displayType = LogicKingakuData.DisplayDataRowType.ShukkaSeikyu;
            }
            // 出荷請求相殺
            else if (ReportType.ShukkaSousai.Equals(reportType))
            {
                this.logicDainoMeisaiData.ShukkaHakkouKbnCd = ConstClass.HakkouKbnCd.Sousai;
                displayType = LogicKingakuData.DisplayDataRowType.ShukkaSeikyu;
            }

            // 請求金額データを取得
            // 受入支払、出荷支払時はNoneで検索する
            DisplayDataRow displaySeikyuRecord = this.logicDainoMeisaiData.GetDisplayData(displayType);

            // 発行区分を元に戻す
            this.logicDainoMeisaiData.UkeireHakkouKbnCd = privUkeireHakkouKbnCd;
            this.logicDainoMeisaiData.ShukkaHakkouKbnCd = privShukkaHakkouKbnCd;


            // 相殺の場合
            if (ReportType.UkeireSousai.Equals(reportType)
                    || ReportType.ShukkaSousai.Equals(reportType)
                )
            {
                if (displaySeikyuRecord.SasihikiZandaka == 0)
                {
                    displaySeikyuRecord.Sousai = 0;
                }
            }

            LogUtility.DebugMethodEnd(displaySeikyuRecord);
            return displaySeikyuRecord;
        }
        /// <summary>
        /// 帳票種類による表示金額（支払）レコード取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private DisplayDataRow GetDisplayRecordShiharai(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            // 初期化
            LogicKingakuData.DisplayDataRowType displayType = LogicKingakuData.DisplayDataRowType.None;

            // 変更前の発行区分
            ConstClass.HakkouKbnCd privUkeireHakkouKbnCd = this.logicDainoMeisaiData.UkeireHakkouKbnCd;
            ConstClass.HakkouKbnCd privShukkaHakkouKbnCd = this.logicDainoMeisaiData.ShukkaHakkouKbnCd;

            // 受入支払時
            if (ReportType.UkeireShiharai.Equals(reportType))
            {
                this.logicDainoMeisaiData.UkeireHakkouKbnCd = ConstClass.HakkouKbnCd.Kobetu;
                displayType = LogicKingakuData.DisplayDataRowType.UkeireShiharai;
            }

            // 受入相殺時
            else if (ReportType.UkeireSousai.Equals(reportType))
            {
                this.logicDainoMeisaiData.UkeireHakkouKbnCd = ConstClass.HakkouKbnCd.Sousai;
                displayType = LogicKingakuData.DisplayDataRowType.UkeireShiharai;
            }

            // 出荷支払時
            else if (ReportType.ShukkaShiharai.Equals(reportType))
            {
                this.logicDainoMeisaiData.ShukkaHakkouKbnCd = ConstClass.HakkouKbnCd.Kobetu;
                displayType = LogicKingakuData.DisplayDataRowType.ShukkaShiharai;
            }

            // 出荷相殺時
            else if (ReportType.ShukkaSousai.Equals(reportType))
            {
                this.logicDainoMeisaiData.ShukkaHakkouKbnCd = ConstClass.HakkouKbnCd.Sousai;
                displayType = LogicKingakuData.DisplayDataRowType.ShukkaShiharai;
            }

            // 支払金額データを取得
            // 受入請求、出荷請求時はNoneでする
            DisplayDataRow displayShiharaiRecord = this.logicDainoMeisaiData.GetDisplayData(displayType);

            // 発行区分を元に戻す
            this.logicDainoMeisaiData.UkeireHakkouKbnCd = privUkeireHakkouKbnCd;
            this.logicDainoMeisaiData.ShukkaHakkouKbnCd = privShukkaHakkouKbnCd;

            // 相殺の場合
            if (ReportType.UkeireSousai.Equals(reportType)
                    || ReportType.ShukkaSousai.Equals(reportType)
                )
            {
                if (displayShiharaiRecord.SasihikiZandaka == 0)
                {
                    displayShiharaiRecord.Sousai = 0;
                }
            }

            LogUtility.DebugMethodEnd(displayShiharaiRecord);
            return displayShiharaiRecord;
        }
        #endregion

        #region ヘッダーデータ用メソッド
        /// <summary>
        /// ヘッダータイトル文字取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        private string GetHeaderTitle(ReportType reportType)
        {
            LogUtility.DebugMethodStart(reportType);

            string titleString = string.Empty;
            string reportFormat = "{0},{1},{2}";

            // 受入請求、受入相殺時
            if (ReportType.UkeireSeikyu.Equals(reportType)
                || ReportType.UkeireSousai.Equals(reportType))
            {
                titleString = string.Format(reportFormat,
                                    this.sysInfo.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1,
                                    this.sysInfo.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2,
                                    this.sysInfo.UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3);
            }

            // 受入支払時
            else if (ReportType.UkeireShiharai.Equals(reportType))
            {
                titleString = string.Format(reportFormat,
                                    this.sysInfo.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1,
                                    this.sysInfo.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2,
                                    this.sysInfo.UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3);
            }

            // 出荷請求、出荷相殺時
            else if (ReportType.ShukkaSeikyu.Equals(reportType)
                || ReportType.ShukkaSousai.Equals(reportType))
            {
                titleString = string.Format(reportFormat,
                                    this.sysInfo.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1,
                                    this.sysInfo.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2,
                                    this.sysInfo.SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3);
            }

            // 出荷支払時
            else if (ReportType.ShukkaShiharai.Equals(reportType))
            {
                titleString = string.Format(reportFormat,
                                    this.sysInfo.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1,
                                    this.sysInfo.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2,
                                    this.sysInfo.SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3);
            }

            LogUtility.DebugMethodEnd(titleString);
            return titleString;
        }
        #endregion

        #region 明細データ用メソッド
        /// <summary>
        /// 単価取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="detailRecord"></param>
        /// <returns></returns>
        private string GetDetailTanka(ReportType reportType, ResultDainoDetailKonkaiDto detailRecord)
        {
            LogUtility.DebugMethodStart(reportType, detailRecord);
            string tankaString = detailRecord.TANKA.ToString(this.sysInfo.SYS_TANKA_FORMAT);

            // 請求時
            if (ReportType.UkeireSeikyu.Equals(reportType) ||
                        ReportType.ShukkaSeikyu.Equals(reportType))
            {
                // 伝票区分=2：支払いなら空文字
                if (DEF_DBVAUE_DENPYOU_KBN_CD_SHIHARAI.Equals(detailRecord.DENPYOU_KBN_CD))
                {
                    tankaString = string.Empty;
                }
            }

            // 支払時
            else if (ReportType.UkeireShiharai.Equals(reportType) ||
                        ReportType.ShukkaShiharai.Equals(reportType))
            {
                // 伝票区分=1：売上なら空文字
                if (DEF_DBVAUE_DENPYOU_KBN_CD_URIAGE.Equals(detailRecord.DENPYOU_KBN_CD))
                {
                    tankaString = string.Empty;
                }
            }

            LogUtility.DebugMethodEnd(tankaString);
            return tankaString;
        }
        /// <summary>
        /// 金額取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="detailRecord"></param>
        /// <returns></returns>
        private string GetDetailKingaku(ReportType reportType, ResultDainoDetailKonkaiDto detailRecord)
        {
            LogUtility.DebugMethodStart(reportType, detailRecord);
            
            // 金額 ＋ 品名金額
            decimal kingaku = detailRecord.KINGAKU + detailRecord.HINMEI_KINGAKU;
            string kingakuString = kingaku.ToString("#,##0");

            // 請求時
            if (ReportType.UkeireSeikyu.Equals(reportType) ||
                        ReportType.ShukkaSeikyu.Equals(reportType))
            {
                // 伝票区分=2：支払いなら空文字
                if (DEF_DBVAUE_DENPYOU_KBN_CD_SHIHARAI.Equals(detailRecord.DENPYOU_KBN_CD))
                {
                    kingakuString = string.Empty;
                }
            }

            // 支払時
            else if (ReportType.UkeireShiharai.Equals(reportType) ||
                        ReportType.ShukkaShiharai.Equals(reportType))
            {
                // 伝票区分=1：売上なら空文字
                if (DEF_DBVAUE_DENPYOU_KBN_CD_URIAGE.Equals(detailRecord.DENPYOU_KBN_CD))
                {
                    kingakuString = string.Empty;
                }
            }

            // 相殺の場合
            else if (ReportType.UkeireSousai.Equals(reportType) ||
                        ReportType.ShukkaSousai.Equals(reportType))
            {
                // 支払いならマイナス化
                if (DEF_DBVAUE_DENPYOU_KBN_CD_SHIHARAI.Equals(detailRecord.DENPYOU_KBN_CD))
                {
                    kingaku = kingaku * -1;
                    kingakuString = kingaku.ToString("#,##0");
                }
            }

            LogUtility.DebugMethodEnd(kingakuString);
            return kingakuString;
        }
        #endregion

        #region フッターデータ用メソッド
        /// <summary>
        /// 今回金額の合計値取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="seikyuSousai"></param>
        /// <param name="shiharaiSousai"></param>
        /// <returns></returns>
        private string GetFooterKonkaiKingakuGokei(ReportType reportType, 
                                                    DisplayDataRow seikyuSousai, 
                                                    DisplayDataRow shiharaiSousai)
        {
            LogUtility.DebugMethodStart(reportType, seikyuSousai, shiharaiSousai);

            // 金額
            decimal konkaiKingaku = seikyuSousai.KonkaiKingaku - shiharaiSousai.KonkaiKingaku;
            string konkaiKingakuGokeiString = konkaiKingaku.ToString("#,##0");

            // 相殺以外は、
            if (ReportType.UkeireSeikyu.Equals(reportType) ||
                        ReportType.ShukkaSeikyu.Equals(reportType) ||
                ReportType.UkeireShiharai.Equals(reportType) ||
                        ReportType.ShukkaShiharai.Equals(reportType))
            {
                // プラスにする
                konkaiKingakuGokeiString = Math.Abs(konkaiKingaku).ToString("#,##0");
            }

            LogUtility.DebugMethodEnd(konkaiKingakuGokeiString);
            return konkaiKingakuGokeiString;
        }

        /// <summary>
        /// 相殺後金額取得
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="seikyuSousaiRecord"></param>
        /// <param name="shiharaiSousaiRecord"></param>
        /// <returns></returns>
        private string GetFooterSousaiGoKingaku(ReportType reportType, 
                                                DisplayDataRow seikyuSousaiRecord, 
                                                DisplayDataRow shiharaiSousaiRecord)
        {
            LogUtility.DebugMethodStart(reportType, seikyuSousaiRecord, shiharaiSousaiRecord);

            // 相殺後金額( 請求：相殺金額 - 支払：相殺金額)
            decimal kingaku = seikyuSousaiRecord.Sousai - shiharaiSousaiRecord.Sousai;
            string SousaiGoKingakuString = string.Format("相殺後御清算額:{0}", kingaku.ToString("#,##0"));

            // 相殺以外は、
            if (ReportType.UkeireSeikyu.Equals(reportType) ||
                        ReportType.ShukkaSeikyu.Equals(reportType) ||
                ReportType.UkeireShiharai.Equals(reportType) ||
                        ReportType.ShukkaShiharai.Equals(reportType))
            {
                // 空文字にする
                SousaiGoKingakuString = string.Empty;
            }

            LogUtility.DebugMethodEnd(SousaiGoKingakuString);
            return SousaiGoKingakuString;
        }
        #endregion

        #region Equals/GetHashCode/ToString
        public bool Equals(LogicReport other)
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
