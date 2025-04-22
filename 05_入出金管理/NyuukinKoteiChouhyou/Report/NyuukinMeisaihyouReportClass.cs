using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyuukinKoteiChouhyou
{
    /// <summary>
    /// 入金明細表帳票作成クラス
    /// </summary>
    internal class NyuukinMeisaihyouReportClass
    {
        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO mSysInfo;

        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        /// <summary>
        /// 帳票テンプレートのパス
        /// </summary>
        private static readonly string OutputFormFullPathName = "./Template/R588-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal NyuukinMeisaihyouReportClass()
        {
            LogUtility.DebugMethodStart();

            var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, NyuukinMeisaihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            // レポート定義ファイルをLoad
            var chouhyouDataTable = this.EditChouhyouDataTable(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);
            reportInfo.Create(OutputFormFullPathName, LAYOUT, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("NYUUSHUKKIN_KBN", false, dto.IsGroupNyuushukkinKbn);
            reportInfo.SetGroupVisible("DENPYOU_NUMBER", false, dto.IsGroupDenpyouNumber);
            reportInfo.SetGroupVisible("TORIHIKISAKI", false, dto.IsGroupTorihikisaki);
            reportInfo.SetGroupVisible("NYUUKINSAKI", false, dto.IsGroupNyuukinsaki);

            // ヘッダの表示制御
            if (String.IsNullOrEmpty(dto.TorihikisakiCdFrom) && String.IsNullOrEmpty(dto.TorihikisakiCdTo))
            {
                reportInfo.SetFieldVisible("FH_TORIHIKISAKI_FLB", false);
            }
            if (dto.BankCdFrom != dto.BankCdTo && String.IsNullOrEmpty(dto.BankShitenCdFrom) && String.IsNullOrEmpty(dto.BankShitenCdTo))
            {
                reportInfo.SetFieldVisible("FH_BANK_SHITEN_FLB", false);
                reportInfo.SetFieldVisible("FH_BANK_SHITEN_VLB", false);
            }

            // カラムの幅と位置と表示制御を設定
            if (dto.Sort1 == ConstClass.SORT_1_NYUUKINSAKI)
            {
                if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                {
                    reportInfo.SetGroupGroupBy("SUB", "NYUUKINSAKI_CD");

                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 4610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 4610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 4610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_LINE", 3300);
                    reportInfo.SetFieldLeft("D_LINE", 3300);
                    reportInfo.SetFieldWidth("D_LINE", 12930);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE || dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        reportInfo.SetGroupGroupBy("SUB", "DENPYOU_DATE");
                    }
                    else
                    {
                        reportInfo.SetGroupGroupBy("SUB", "NYUUKIN_NUMBER");
                    }

                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 9220);

                    if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        reportInfo.SetFieldWidth("GH_LINE", 1300);
                        reportInfo.SetFieldLeft("D_LINE", 1300);
                        reportInfo.SetFieldWidth("D_LINE", 14930);
                    }
                    else
                    {
                        reportInfo.SetFieldWidth("GH_LINE", 9220);
                        reportInfo.SetFieldLeft("D_LINE", 9220);
                        reportInfo.SetFieldWidth("D_LINE", 7010);
                    }
                }
                else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                {
                    reportInfo.SetGroupGroupBy("SUB", "NYUUSHUKKIN_KBN_CD");

                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("GH_LINE", 1300);
                    reportInfo.SetFieldLeft("D_LINE", 1300);
                    reportInfo.SetFieldWidth("D_LINE", 14930);
                }
            }
            else if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
            {
                if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                {
                    reportInfo.SetGroupGroupBy("SUB", "TORIHIKISAKI_CD");

                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 6610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 7915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 6610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 7915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 3305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 6610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 7915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_LINE", 6600);
                    reportInfo.SetFieldLeft("D_LINE", 6600);
                    reportInfo.SetFieldWidth("D_LINE", 9630);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE || dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        reportInfo.SetGroupGroupBy("SUB", "DENPYOU_DATE");
                    }
                    else
                    {
                        reportInfo.SetGroupGroupBy("SUB", "NYUUKIN_NUMBER");
                    }
                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 9220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 1300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 5915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 9220);

                    if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        reportInfo.SetFieldWidth("GH_LINE", 1300);
                        reportInfo.SetFieldLeft("D_LINE", 1300);
                        reportInfo.SetFieldWidth("D_LINE", 14930);
                    }
                    else
                    {
                        reportInfo.SetFieldWidth("GH_LINE", 2600);
                        reportInfo.SetFieldLeft("D_LINE", 2600);
                        reportInfo.SetFieldWidth("D_LINE", 13630);
                    }
                }
                else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                {
                    reportInfo.SetGroupGroupBy("SUB", "NYUUSHUKKIN_KBN_CD");

                    reportInfo.SetFieldWidth("PH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("PH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("PH_COLUMN_5_VLB", 3300);

                    reportInfo.SetFieldLeft("PH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("PH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("PH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("PH_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("PH_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("GH_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("GH_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("GH_COLUMN_5_VLB", 3300);

                    reportInfo.SetFieldLeft("GH_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("GH_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("GH_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("GH_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("GH_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("D_COLUMN_1_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_2_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_3_VLB", 1300);
                    reportInfo.SetFieldWidth("D_COLUMN_4_VLB", 3300);
                    reportInfo.SetFieldWidth("D_COLUMN_5_VLB", 3300);

                    reportInfo.SetFieldLeft("D_COLUMN_1_VLB", 0);
                    reportInfo.SetFieldLeft("D_COLUMN_2_VLB", 1305);
                    reportInfo.SetFieldLeft("D_COLUMN_3_VLB", 2610);
                    reportInfo.SetFieldLeft("D_COLUMN_4_VLB", 3915);
                    reportInfo.SetFieldLeft("D_COLUMN_5_VLB", 7220);

                    reportInfo.SetFieldWidth("GH_LINE", 1300);
                    reportInfo.SetFieldLeft("D_LINE", 1300);
                    reportInfo.SetFieldWidth("D_LINE", 14930);
                }
            }

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;

            //レポートタイトルの設定
            if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
            {
                reportPopup.ReportCaption = ConstClass.NYUUKIN_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_TORIHIKISAKI_SUB_TITLE;
            }
            else
            {
                reportPopup.ReportCaption = ConstClass.NYUUKIN_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_NYUUKINSAKI_SUB_TITLE;
            }
            if (dto.Sort2 == ConstClass.SORT_2_CD)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_CD_SUB_TITLE + "順）";
            }
            else if (dto.Sort2 == ConstClass.SORT_2_FURIGANA)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_FURIGANA_SUB_TITLE + "順）";
            }
            else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_DENPYOU_DATE_SUB_TITLE + "順）";
            }
            else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_DENPYOU_NO_SUB_TITLE + "順）";
            }
            else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_NYUUSHUKKIN_KBN_SUB_TITLE + "順）";
            }
            
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票に渡すデータを加工します
        /// </summary>
        /// <param name="dt">元となるデータ</param>
        /// <param name="dto">画面入力データ</param>
        /// <returns>帳票用に作成したデータ</returns>
        private DataTable EditChouhyouDataTable(DataTable dt, NyuukinMeisaihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 日付
            dt.Columns.Add("FH_DATE_VLB");
            // 取引先
            dt.Columns.Add("FH_TORIHIKISAKI_VLB");
            // 入金先
            dt.Columns.Add("FH_NYUUKINSAKI_VLB");
            // 銀行
            dt.Columns.Add("FH_BANK_VLB");
            // 銀行支店
            dt.Columns.Add("FH_BANK_SHITEN_VLB");
            // カラム１
            dt.Columns.Add("PH_COLUMN_1_VLB");
            // カラム２
            dt.Columns.Add("PH_COLUMN_2_VLB");
            // カラム３
            dt.Columns.Add("PH_COLUMN_3_VLB");
            // カラム４
            dt.Columns.Add("PH_COLUMN_4_VLB");
            // カラム５
            dt.Columns.Add("PH_COLUMN_5_VLB");
            // カラム１
            dt.Columns.Add("GH_COLUMN_1_VLB");
            // カラム２
            dt.Columns.Add("GH_COLUMN_2_VLB");
            // カラム３
            dt.Columns.Add("GH_COLUMN_3_VLB");
            // カラム４
            dt.Columns.Add("GH_COLUMN_4_VLB");
            // カラム５
            dt.Columns.Add("GH_COLUMN_5_VLB");
            // カラム１
            dt.Columns.Add("D_COLUMN_1_VLB");
            // カラム２
            dt.Columns.Add("D_COLUMN_2_VLB");
            // カラム３
            dt.Columns.Add("D_COLUMN_3_VLB");
            // カラム４
            dt.Columns.Add("D_COLUMN_4_VLB");
            // カラム５
            dt.Columns.Add("D_COLUMN_5_VLB");
            // 入金区分
            dt.Columns.Add("D_NYUUKIN_KBN_VLB");
            // 金額
            dt.Columns.Add("D_KINGAKU_VLB");
            // 入出金区分合計
            dt.Columns.Add("GF_NYUUSHUKKIN_KBN_KINGAKU_VLB");
            // 伝票合計
            dt.Columns.Add("GF_DENPYOU_NUMBER_KINGAKU_VLB");
            // 取引先合計
            dt.Columns.Add("GF_TORIHIKISAKI_KINGAKU_VLB");
            // 入金先合計
            dt.Columns.Add("GF_NYUUKINSAKI_KINGAKU_VLB");
            // 総合計（金額）
            dt.Columns.Add("GF_ALL_KINGAKU_VLB");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // データ加工

            decimal nyuushukkinKbnKingaku = 0;
            decimal denpyouNumberKingaku = 0;
            decimal torihikisakiKingaku = 0;
            decimal nyuukinsakiKingaku = 0;
            decimal allKingaku = 0;

            string nyuushukkinKbnCd = ret.Rows[0]["NYUUSHUKKIN_KBN_CD"].ToString();
            string denpyouNumber = ret.Rows[0]["NYUUKIN_NUMBER"].ToString();
            string torihikisakiCd = ret.Rows[0]["TORIHIKISAKI_CD"].ToString();
            string nyuukinsakiCd = ret.Rows[0]["NYUUKINSAKI_CD"].ToString();

            foreach (DataRow row in ret.Rows)
            {
                row["FH_TITLE_VLB"] = ConstClass.NYUUKIN_MEISAIHYOU_TITLE;

                // 並び順の指定でタイトル設定
                StringBuilder subTitleStringBuilder = new StringBuilder();
                subTitleStringBuilder.Append("（");
                if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_TORIHIKISAKI_SUB_TITLE);
                }
                else
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_NYUUKINSAKI_SUB_TITLE);
                }
                if (dto.Sort2 == ConstClass.SORT_2_CD)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_CD_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_FURIGANA_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_DENPYOU_DATE_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_DENPYOU_NO_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_NYUUSHUKKIN_KBN_SUB_TITLE);
                }
                subTitleStringBuilder.Append("順）");
                row["FH_TITLE_VLB"] += subTitleStringBuilder.ToString();

                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 日付の文字列作成
                if (1 == dto.DateShuruiCd)
                {
                    row["FH_DATE_VLB"] = "[" + ConstClass.DATE_SHURUI_DENPYOU + "] " + dto.DateFrom + " ～ " + dto.DateTo;
                }
                else if (2 == dto.DateShuruiCd)
                {
                    row["FH_DATE_VLB"] = "[" + ConstClass.DATE_SHURUI_INPUT + "] " + dto.DateFrom + " ～ " + dto.DateTo;
                }

                // 入金先の文字列作成
                if (!String.IsNullOrEmpty(dto.NyuukinsakiFrom) || !String.IsNullOrEmpty(dto.NyuukinsakiTo))
                {
                    row["FH_NYUUKINSAKI_VLB"] = dto.NyuukinsakiCdFrom + " " + dto.NyuukinsakiFrom + " ～ " + "\n" + dto.NyuukinsakiCdTo + " " + dto.NyuukinsakiTo;
                }

                // 取引先の文字列作成
                if (!String.IsNullOrEmpty(dto.TorihikisakiFrom) || !String.IsNullOrEmpty(dto.TorihikisakiTo))
                {
                    row["FH_TORIHIKISAKI_VLB"] = dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom + " ～ " + "\n" + dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo;
                }

                // 銀行の文字列作成
                row["FH_BANK_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.BankFrom) || !String.IsNullOrEmpty(dto.BankTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.BankFrom))
                    {
                        from = dto.BankCdFrom + " " + dto.BankFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.BankTo))
                    {
                        to = dto.BankCdTo + " " + dto.BankTo;
                    }

                    row["FH_BANK_VLB"] = from + " ～ " + to;
                }

                // 銀行支店の文字列作成
                row["FH_BANK_SHITEN_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.BankShitenFrom) || !String.IsNullOrEmpty(dto.BankShitenTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.BankShitenFrom))
                    {
                        from = dto.BankShitenCdFrom + " " + dto.BankShitenFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.BankShitenTo))
                    {
                        to = dto.BankShitenCdTo + " " + dto.BankShitenTo;
                    }

                    row["FH_BANK_SHITEN_VLB"] = from + " ～ " + to;
                }

                // 明細ヘッダの設定
                if (dto.Sort1 == ConstClass.SORT_1_NYUUKINSAKI)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_4_VLB"] = String.Empty;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_4_VLB"] = String.Empty;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_4_VLB"] = String.Empty;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_4_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_5_VLB"] = String.Empty;
                    }
                }
                else if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_4_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;
                        row["PH_COLUMN_4_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;
                        row["PH_COLUMN_4_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                    {
                        row["PH_COLUMN_1_VLB"] = ConstClass.COLUMN_NYUUSHUKKIN_KBN;
                        row["PH_COLUMN_2_VLB"] = ConstClass.COLUMN_DENPYOU_DATE;
                        row["PH_COLUMN_3_VLB"] = ConstClass.COLUMN_NYUUKIN_NUMBER;
                        row["PH_COLUMN_4_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;
                        row["PH_COLUMN_5_VLB"] = ConstClass.COLUMN_NYUUKINSAKI;
                    }
                }

                // 明細の設定
                var denpyouDate = DateTime.Parse(row["DENPYOU_DATE"].ToString()).ToString("yyyy/MM/dd");
                var nyuukinNumber = row["NYUUKIN_NUMBER"].ToString();
                var nyuukinsaki = row["NYUUKINSAKI_CD"].ToString() + " " + row["NYUUKINSAKI_NAME_RYAKU"].ToString();
                int nyuukinsakiLength = row["NYUUKINSAKI_NAME_RYAKU"].ToString().Length;
                int nyuukinsakiGetByteCount = Encoding.Default.GetByteCount(row["NYUUKINSAKI_NAME_RYAKU"].ToString());
                if (nyuukinsakiLength != nyuukinsakiGetByteCount)
                {
                    if (nyuukinsakiLength > 14)
                    {
                        nyuukinsaki = row["NYUUKINSAKI_CD"].ToString()
                                      + " "
                                      + row["NYUUKINSAKI_NAME_RYAKU"].ToString().Substring(0, 14)
                                      + "　　　 "
                                      + row["NYUUKINSAKI_NAME_RYAKU"].ToString().Substring(14, nyuukinsakiLength - 14);
                    }
                }
                else
                {
                    if (nyuukinsakiLength > 28)
                    {
                        nyuukinsaki = row["NYUUKINSAKI_CD"].ToString()
                                      + " "
                                      + row["NYUUKINSAKI_NAME_RYAKU"].ToString().Substring(0, 28)
                                      + "　　　 "
                                      + row["NYUUKINSAKI_NAME_RYAKU"].ToString().Substring(28, nyuukinsakiLength - 28);
                    }

                }
                var torihikisaki = row["TORIHIKISAKI_CD"].ToString() + " " + row["TORIHIKISAKI_NAME_RYAKU"].ToString();
                int torihikisakiLength = row["TORIHIKISAKI_NAME_RYAKU"].ToString().Length;
                int torihikisakiGetByteCount = Encoding.Default.GetByteCount(row["TORIHIKISAKI_NAME_RYAKU"].ToString());
                if (torihikisakiLength != torihikisakiGetByteCount)
                {
                    if (torihikisakiLength > 14)
                    {
                        torihikisaki = row["TORIHIKISAKI_CD"].ToString()
                                      + " "
                                      + row["TORIHIKISAKI_NAME_RYAKU"].ToString().Substring(0, 14)
                                      + "　　　 "
                                      + row["TORIHIKISAKI_NAME_RYAKU"].ToString().Substring(14, torihikisakiLength - 14);
                    }
                }
                else
                {
                    if (torihikisakiLength > 28)
                    {
                        torihikisaki = row["TORIHIKISAKI_CD"].ToString()
                                      + " "
                                      + row["TORIHIKISAKI_NAME_RYAKU"].ToString().Substring(0, 28)
                                      + "　　　 "
                                      + row["TORIHIKISAKI_NAME_RYAKU"].ToString().Substring(28, torihikisakiLength - 28);
                    }
                }
                var nyuushukkinKbn = Int32.Parse(row["NYUUSHUKKIN_KBN_CD"].ToString()).ToString("00") + " " + row["NYUUSHUKKIN_KBN_NAME_RYAKU"].ToString();
                int index = getSubIndex(nyuushukkinKbn, 13);
                if (index > 0)
                {
                    nyuushukkinKbn = Int32.Parse(row["NYUUSHUKKIN_KBN_CD"].ToString()).ToString("00") 
                                   + " " 
                                   + nyuushukkinKbn.Substring(0, index)
                                   + " 　 "
                                   + nyuushukkinKbn.Substring(index);
                }

                if (dto.Sort1 == ConstClass.SORT_1_NYUUKINSAKI)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                    {
                        row["GH_COLUMN_1_VLB"] = nyuukinsaki;
                        row["GH_COLUMN_2_VLB"] = String.Empty;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = denpyouDate;
                        row["D_COLUMN_3_VLB"] = nyuukinNumber;
                        row["D_COLUMN_4_VLB"] = String.Empty;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        row["GH_COLUMN_1_VLB"] = denpyouDate;
                        row["GH_COLUMN_2_VLB"] = String.Empty;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = nyuukinNumber;
                        row["D_COLUMN_3_VLB"] = nyuukinsaki; ;
                        row["D_COLUMN_4_VLB"] = String.Empty;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                    {
                        row["GH_COLUMN_1_VLB"] = nyuukinNumber;
                        row["GH_COLUMN_2_VLB"] = denpyouDate;
                        row["GH_COLUMN_3_VLB"] = nyuukinsaki;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = String.Empty;
                        row["D_COLUMN_3_VLB"] = String.Empty;
                        row["D_COLUMN_4_VLB"] = String.Empty;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                    {
                        row["GH_COLUMN_1_VLB"] = nyuushukkinKbn;
                        row["GH_COLUMN_2_VLB"] = String.Empty;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = denpyouDate;
                        row["D_COLUMN_3_VLB"] = nyuukinNumber;
                        row["D_COLUMN_4_VLB"] = nyuukinsaki;
                        row["D_COLUMN_5_VLB"] = String.Empty;
                    }
                }
                else if (dto.Sort1 == ConstClass.SORT_1_TORIHIKISAKI)
                {
                    if (dto.Sort2 == ConstClass.SORT_2_CD || dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                    {
                        row["GH_COLUMN_1_VLB"] = torihikisaki;
                        row["GH_COLUMN_2_VLB"] = nyuukinsaki;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = String.Empty;
                        row["D_COLUMN_3_VLB"] = denpyouDate;
                        row["D_COLUMN_4_VLB"] = nyuukinNumber;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_DATE)
                    {
                        row["GH_COLUMN_1_VLB"] = denpyouDate;
                        row["GH_COLUMN_2_VLB"] = String.Empty;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = nyuukinNumber;
                        row["D_COLUMN_3_VLB"] = torihikisaki;
                        row["D_COLUMN_4_VLB"] = nyuukinsaki;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_DENPYOU_NUMBER)
                    {
                        row["GH_COLUMN_1_VLB"] = nyuukinNumber;
                        row["GH_COLUMN_2_VLB"] = denpyouDate;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = String.Empty;
                        row["D_COLUMN_3_VLB"] = torihikisaki;
                        row["D_COLUMN_4_VLB"] = nyuukinsaki;
                        row["D_COLUMN_5_VLB"] = nyuushukkinKbn;
                    }
                    else if (dto.Sort2 == ConstClass.SORT_2_NYUUSHUKKIN_KBN)
                    {
                        row["GH_COLUMN_1_VLB"] = nyuushukkinKbn;
                        row["GH_COLUMN_2_VLB"] = String.Empty;
                        row["GH_COLUMN_3_VLB"] = String.Empty;
                        row["GH_COLUMN_4_VLB"] = String.Empty;
                        row["GH_COLUMN_5_VLB"] = String.Empty;

                        row["D_COLUMN_1_VLB"] = String.Empty;
                        row["D_COLUMN_2_VLB"] = denpyouDate;
                        row["D_COLUMN_3_VLB"] = nyuukinNumber;
                        row["D_COLUMN_4_VLB"] = torihikisaki;
                        row["D_COLUMN_5_VLB"] = nyuukinsaki;
                    }
                }

                var kingaku = Convert.ToDecimal(row["KINGAKU"]).ToString("#,##0");
                row["D_KINGAKU_VLB"] = kingaku;

                // 集計（入出金区分）
                if (nyuushukkinKbnCd != row["NYUUSHUKKIN_KBN_CD"].ToString())
                {
                    nyuushukkinKbnKingaku = 0;
                    nyuushukkinKbnCd = row["NYUUSHUKKIN_KBN_CD"].ToString();
                }
                nyuushukkinKbnKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);

                row["GF_NYUUSHUKKIN_KBN_KINGAKU_VLB"] = nyuushukkinKbnKingaku;

                // 集計（伝票番号）
                if (denpyouNumber != row["NYUUKIN_NUMBER"].ToString())
                {
                    denpyouNumberKingaku = 0;
                    denpyouNumber = row["NYUUKIN_NUMBER"].ToString();
                }
                denpyouNumberKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);

                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = denpyouNumberKingaku;

                // 集計（取引先）
                if (torihikisakiCd != row["TORIHIKISAKI_CD"].ToString())
                {
                    torihikisakiKingaku = 0;
                    torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                }
                torihikisakiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);

                row["GF_TORIHIKISAKI_KINGAKU_VLB"] = torihikisakiKingaku;

                // 集計（入金先）
                if (nyuukinsakiCd != row["NYUUKINSAKI_CD"].ToString())
                {
                    nyuukinsakiKingaku = 0;
                    nyuukinsakiCd = row["NYUUKINSAKI_CD"].ToString();
                }
                nyuukinsakiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);

                row["GF_NYUUKINSAKI_KINGAKU_VLB"] = nyuukinsakiKingaku;

                // 集計（全体）
                allKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);

                row["GF_ALL_KINGAKU_VLB"] = allKingaku;

                // 金額の列を書式設定
                row["GF_NYUUSHUKKIN_KBN_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_NYUUSHUKKIN_KBN_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_TORIHIKISAKI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_TORIHIKISAKI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_NYUUKINSAKI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_NYUUKINSAKI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_KINGAKU_VLB"]).ToString("#,##0");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        // 20160122 chenzz 帳票に 文字列中にスペースがあれば、改行したこと対応　start
        private static int getSubIndex(string str, int endIndex)
        {
            if (str == null || str.Length == 0 || endIndex < 0)
            {
                return 0;
            }

            int index = 0;
            int bytesCount = Encoding.Default.GetByteCount(str);
            if (bytesCount > endIndex)
            {
                int readyLength = 0;
                int byteLength = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    byteLength = Encoding.Default.GetByteCount(new char[] { str[i] });
                    readyLength += byteLength;
                    if (readyLength == endIndex)
                    {
                        index = i + 1;
                        break;
                    }
                    else if (readyLength == endIndex)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else if (bytesCount == endIndex)
            {
                index = -1;
            }
            return index;
        }
        // 20160122 chenzz 帳票に 文字列中にスペースがあれば、改行したこと対応　end
    }
}
