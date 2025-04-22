using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
{
    #region - Class -

    /// <summary> R771(請求書)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR771 : ReportInfoBase
    {
        #region - Fields -

        // Detail部データテーブル
        private DataTable chouhyouDataTable = new DataTable();

        // Header部
        // 1-1
        private string torihikisakiCD = string.Empty;           // 取引先CD
        private string shiharaiSaki1H = string.Empty;           // 支払先1
        private string shiharaiSaki2H = string.Empty;           // 支払先2
        private string printDate = string.Empty;                // 発行日
        private string shiharaiNumber = string.Empty;           // 支払番号
        private string taitoru = "支払明細書";                  // タイトル【固定】"支払明細書"

        // Detail部-Header部
        // 1-2  
        private string shiharaiSakiPostCD = string.Empty;       // 支払先郵便番号
        private string shiharaiSakiAddress1 = string.Empty;     // 支払先住所1
        private string shiharaiSakiAddress2 = string.Empty;         // 支払先住所2
        private string shiharaiSaki1DH = string.Empty;          // 支払先1
        private string shiharaiSaki2DH = string.Empty;          // 支払先2
        private string shiharaiSakiBusho = string.Empty;        // 支払先部署
        private string shiharaiSakiTantousha = string.Empty;    // 支払先担当者

        private List<string> listCorpInfo = new List<string>(); //自社名 ～ FAX

        private string kyotenNamePrintKbn = string.Empty;       // 拠点名を印字区分 
        private string corpName = string.Empty;                 //会社名
        private string kyotenName = string.Empty;               //請求書拠点　
        private string kyotenDaihyou = string.Empty;            //拠点代表者　
        private string corpDaihyou = string.Empty;              //会社代表者　
        private string kyotenPost = string.Empty;               //郵便番号　
        private string kyotenAddress1 = string.Empty;           //住所１　
        private string kyotenAddress2 = string.Empty;           //住所２　
        private string kyotenTel = string.Empty;                //電話番号　
        private string kyotenFax = string.Empty;                //FAX番号

        private string maidoArigatou = "毎度ありがとうございます。";                         // 【固定】 "毎度ありがとうございます。"
        private string uchiwake = "内訳は下記の通りですので、御照合をお願い申し上げます。";  // 【固定】 "内訳は下記の通りですので、御照合をお願い申し上げます。"

        // Detail部-Detail部
        // 1-3
        private string konkaiSeisanLabel = "今回御精算額";        // 今回御精算額label【固定】"今回精算額"
        private string konkaiSeisan = string.Empty;             // 今回御精算額
        private string shiharaiDateLabel = "支払年月日";        // 支払年月日label【固定】"支払年月日"
        private string shiharaiDate = string.Empty;             // 支払年月日
        private string codeLabel = "コード";                    // コードlabel【固定】"コード"
        private string code = string.Empty;                     // コード
        private string zenkaiKurikoshiLabel = "前回精算額";     // 前回精算額label【固定】"前回繰越額"
        private string zenkaiKurikoshi = string.Empty;          // 前回精算額
        private string shiharaiGakuLabel = "今回出金額";            // 今回出金額label【固定】"支払額"
        private string shiharaiGaku = string.Empty;             // 今回出金額
        private string tyouseiLabel = "調整額";                 // 調整額label【固定】"調整額"
        private string tyouseiHoka = string.Empty;              // 調整額
        private string sashihikiKurikosiLabel = "繰越額";   // 繰越額label【固定】"差引繰越額"
        private string sashihikiKurikosi = string.Empty;        // 繰越額
        private string konkaiShiharaiLabel = "今回取引額(税抜)";      // 今回取引額(税抜)label【固定】"今回支払額"
        private string konkaiShiharai = string.Empty;           // 今回取引額(税抜)
        private string shouhizeiLabel = "消費税";             // 消費税label【固定】"消費税額"
        private string shouhizei = string.Empty;                // 消費税
        private string goukeiSeisanLabel = "今回取引額";        // 今回取引額label【固定】"合計精算額"
        private string goukeiSeisan = string.Empty;             // 今回取引額

        // Detail部-footer部
        // 1-4
        private string bikouLabel = "備考：";                   // 備考label【固定】"備考："
        private string bikou1 = string.Empty;                   // 備考1
        private string bikou2 = string.Empty;                   // 備考2

        private string tourokuNo = string.Empty;                   // 登録番号

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR771" /> class. </summary>
        public ReportInfoR771()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R771-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        #endregion

        #region - properties -
        /// <summary>帳票出力フルパスフォーム名を保持するフィールド</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するフィールド</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>帳票用データテーブルを保持するプロパティ</summary>
        public DataTable ChouhyouDataTable { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        public M_SYS_INFO MSysInfo { get; set; }
        #endregion

        #region - Methods -

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {

            // 発行日の編集
            string headerPrintDate = string.Empty;
            if (string.IsNullOrWhiteSpace(this.printDate))
            {
                headerPrintDate = this.printDate;
            }
            else
            {
                headerPrintDate = "発行日：" + this.printDate;
            }

            // Header部
            // 1-1
            this.SetFieldName("FH_TORIHIKISAKI_CD_1_CTL", this.torihikisakiCD);             // 取引先CD
            this.SetFieldName("FH_SHIHARAI_SOUFU_NAME1_1_CTL", this.shiharaiSaki1H);            // 支払先1
            this.SetFieldName("FH_SHIHARAI_SOUFU_NAME2_1_CTL", this.shiharaiSaki2H);            // 支払先2            
            this.SetFieldName("FH_PRINT_DATE_VLB", headerPrintDate);                        // 発行日
            this.SetFieldName("FH_SEISAN_NUMBER_CTL", "No." + this.shiharaiNumber);             // 支払番号
            this.SetFieldName("FH_TITLE_FLB", this.taitoru);                                    // タイトル【固定】"支払明細書"

            // page-Header部
            this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", this.torihikisakiCD);              // 取引先CD
            this.SetFieldName("PHY_SHIHARAI_SOUFU_NAME1_CTL", this.shiharaiSaki1H);             // 支払先1
            this.SetFieldName("PHY_SHIHARAI_SOUFU_NAME2_CTL", this.shiharaiSaki2H);             // 支払先2
            this.SetFieldName("PHY_PRINT_DATE_VLB", headerPrintDate);                       // 発行日
            this.SetFieldName("PHY_SEISAN_NUMBER_CTL", "No." + this.shiharaiNumber);            // 支払番号

            // Detail-Header部
            // 1-2
            this.SetFieldName("FH_SHIHARAI_SOUFU_POST_CTL", this.shiharaiSakiPostCD);           // 支払先郵便番号
            this.SetFieldName("FH_SHIHARAI_SOUFU_ADDRESS1_CTL", this.shiharaiSakiAddress1);     // 支払先住所1
            this.SetFieldName("FH_SHIHARAI_SOUFU_ADDRESS2_CTL", this.shiharaiSakiAddress2);         // 支払先住所2
            this.SetFieldName("FH_SHIHARAI_SOUFU_NAME1_2_CTL", this.shiharaiSaki1DH);           // 支払先1
            this.SetFieldName("FH_SHIHARAI_SOUFU_NAME2_2_CTL", this.shiharaiSaki2DH);           // 支払先2
            this.SetFieldName("FH_SHIHARAI_SOUFU_BUSHO_CTL", this.shiharaiSakiBusho);           // 支払先部署
            this.SetFieldName("FH_SHIHARAI_SOUFU_TANTOU_CTL", this.shiharaiSakiTantousha);      // 支払先担当者

            this.SetFieldName("FH_TOUROKUNO_CTL", this.tourokuNo);        // 登録番号

            //自社名 ～ 請求担当者
            for (int i = 0; i < ConstCls.MAX_ROW_CORP_INFO; i++)
            {
                this.SetFieldName(string.Format("FH_CORP_INFO{0}_CTL", i + 1),string.Empty);
            }
            for (int i = 0; i < this.listCorpInfo.Count; i++)
            {
                this.SetFieldName(string.Format("FH_CORP_INFO{0}_CTL", i+1), this.listCorpInfo[i]); 
            }
            this.SetFieldName("FH_MAIDO_FBL", this.maidoArigatou);                              // 【固定】"毎度ありがとうございます。"
            this.SetFieldName("FH_KAKINOTORI_FBL", this.uchiwake);                              // 【固定】"内訳は下記の通りですので、御照合をお願い申し上げます。"

            // 支払先郵便番号がない場合、〒マークを印字しない。
            if (String.Equals(this.shiharaiSakiPostCD, ConstCls.YUBIN))
            {
                this.SetFieldVisible("FH_SHIHARAI_SOUFU_POST_CTL", false);
            }

            // Detail-Header部
            // 1-3
            this.SetFieldName("FH_KONKAI_SEISAN_GAKU_FLB", this.konkaiSeisanLabel);             // 今回精算額label【固定】"今回精算額"
            this.SetFieldName("FH_KONKAI_SEISAN_GAKU_CTL", "\\" + this.konkaiSeisan);           // 合計精算額:仕様変更(今回請求額と合計請求額の表示箇所入替え)
            this.SetFieldName("FH_SHIHARAI_NENGATSUPI_FLB", this.shiharaiDateLabel);            // 支払年月日label【固定】"支払年月日"
            this.SetFieldName("FH_SHIHARAI_NENGATSUPI_CTL", this.shiharaiDate);                 // 支払年月日
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_FLB", this.codeLabel);                      // コードlabel【固定】"コード"
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_CTL", this.code);                           // コード
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_FLB", this.zenkaiKurikoshiLabel);        // 前回繰越額label【固定】"前回繰越額"
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_CTL", this.zenkaiKurikoshi);             // 前回繰越額
            this.SetFieldName("FH_KONKAI_SHUKKIN_GAKU_FLB", this.shiharaiGakuLabel);            // 支払額label【固定】"支払額"
            this.SetFieldName("FH_KONKAI_SHUKKIN_GAKU_CTL", this.shiharaiGaku);                 // 支払額
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_FLB", this.tyouseiLabel);                 // 調整額label【固定】"調整額"
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_CTL", this.tyouseiHoka);                  // 調整額
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_FLB", this.sashihikiKurikosiLabel);  // 差引繰越額label【固定】"差引繰越額"
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_CTL", this.sashihikiKurikosi);       // 差引繰越額
            this.SetFieldName("FH_KONKAI_SHIHARAI_GAKU_FLB", this.konkaiShiharaiLabel);         // 今回支払額label【固定】"今回支払額"
            this.SetFieldName("FH_KONKAI_SHIHARAI_GAKU_CTL", this.konkaiShiharai);              // 今回支払額
            this.SetFieldName("FH_SHOUHIZEI_GAKU_FLB", this.shouhizeiLabel);                    // 消費税額label【固定】"消費税額"
            this.SetFieldName("FH_SHOUHIZEI_GAKU_CTL", this.shouhizei);                         // 消費税額
            this.SetFieldName("FH_GOUKEI_SEISAN_GAKU_FLB", this.goukeiSeisanLabel);             // 合計精算額label【固定】"合計精算額"
            this.SetFieldName("FH_GOUKEI_SEISAN_GAKU_CTL", this.goukeiSeisan);                  // 今回精算額:仕様変更(今回請求額と合計請求額の表示箇所入替え)

            if (String.IsNullOrEmpty(this.zenkaiKurikoshi))
            {
                this.SetFieldVisible("FH_ZENKAI_KURIKOSI_GAKU_FLB", false);
                this.SetFieldVisible("FH_ZENKAI_KURIKOSI_GAKU_CTL", false);
                this.SetFieldVisible("FH_KONKAI_SHUKKIN_GAKU_FLB", false);
                this.SetFieldVisible("FH_KONKAI_SHUKKIN_GAKU_CTL", false);
                this.SetFieldVisible("FH_KONKAI_CHOUSEI_GAKU_FLB", false);
                this.SetFieldVisible("FH_KONKAI_CHOUSEI_GAKU_CTL", false);
                this.SetFieldVisible("FH_SASHIHIKI_KURIKOSHI_GAKU_FLB", false);
                this.SetFieldVisible("FH_SASHIHIKI_KURIKOSHI_GAKU_CTL", false);
            }

            // Page-Footer部
            // 1-4
            this.SetFieldName("FF_BIKOU_FLB", this.bikouLabel);     // 備考label【固定】"備考："
            this.SetFieldName("FF_BIKOU1_CTL", this.bikou1);        // 備考1
            this.SetFieldName("FF_BIKOU2_CTL", this.bikou2);        // 備考2

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

        /// <summary> 指定フォーマットに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <param name="format">指定フォーマットを表す文字列</param>
        private string SetFormat(string value, string format)
        {
            // フォーマット変換後文字列
            string ret = string.Empty;

            // 引数がブランクの場合はブランクを返して終了
            if (value.Trim() != string.Empty)
            {
                //※↓↓↓引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↓↓↓
                // ret = value;
                //※↑↑↑引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↑↑↑

                // 数値変換時の一時変数
                decimal temp = 0;

                // 引数が括弧付(内税)の場合は括弧付で返す
                if (ConstCls.KAKKO_START == value.Trim().Substring(0, 1))
                {
                    // ◆括弧付の場合

                    // 先頭と末尾の括弧を外す(例： (123456789) ⇒ 123456789 )
                    value = value.Substring(1, value.Length - 2);

                    // 引数の文字列が数値変換可能か
                    if (decimal.TryParse(value, out temp))
                    {
                        // 数値変換できた場合は指定フォーマットの文字列に変換する
                        ret = string.Format(temp.ToString(format));

                        // 括弧を付け直す(例： 123,456,789 ⇒ (123,456,789) )
                        ret = ConstCls.KAKKO_START + ret + ConstCls.KAKKO_END;
                    }
                }
                else
                {
                    // ◆括弧なしの場合

                    // 引数の文字列が数値変換可能か
                    if (decimal.TryParse(value, out temp))
                    {
                        // 数値変換できた場合は指定フォーマットの文字列に変換する
                        ret = string.Format(temp.ToString(format));
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 精算伝票ヘッダデータ設定
        /// </summary>
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <param name="headerRow">精算伝票データ</param>
        private void SetReportHearder(ShiharaiDenpyouDto dto, DataRow headerRow)
        {

            //発行日
            if (dto.ShiharaiHakkou.Equals(ConstCls.SHIHARAI_HAKKOU_PRINT_SURU))
            {
                this.printDate = dto.HakkoBi;
            }
            else
            {
                this.printDate = string.Empty;
            }
            //取引先CD
            this.torihikisakiCD = dto.TorihikisakiCd;
            //支払先1
            this.shiharaiSaki1H = headerRow["SHIHARAI_SOUFU_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SHIHARAI_SOUFU_KEISHOU1"].ToString();
            //支払先2
            this.shiharaiSaki2H = headerRow["SHIHARAI_SOUFU_NAME2"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SHIHARAI_SOUFU_KEISHOU2"].ToString();
            //支払番号
            this.shiharaiNumber = headerRow["SEISAN_NUMBER"].ToString();

            // Detail部-Header部
            // 支払先郵便番号
            this.shiharaiSakiPostCD = ConstCls.YUBIN + headerRow["SHIHARAI_SOUFU_POST"].ToString();
            this.shiharaiSakiAddress1 = headerRow["SHIHARAI_SOUFU_ADDRESS1"].ToString();    // 支払先住所1
            this.shiharaiSakiAddress2 = headerRow["SHIHARAI_SOUFU_ADDRESS2"].ToString();         // 支払先住所2
            this.shiharaiSaki1DH = headerRow["SHIHARAI_SOUFU_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SHIHARAI_SOUFU_KEISHOU1"].ToString();        // 支払先1
            this.shiharaiSaki2DH = headerRow["SHIHARAI_SOUFU_NAME2"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SHIHARAI_SOUFU_KEISHOU2"].ToString();         // 支払先2
            if (!string.IsNullOrEmpty(headerRow["SHIHARAI_SOUFU_BUSHO"].ToString()))
            {
                this.shiharaiSakiBusho = headerRow["SHIHARAI_SOUFU_BUSHO"].ToString();       // 支払先部署
                if (!string.IsNullOrEmpty(headerRow["SHIHARAI_SOUFU_TANTOU"].ToString()))
                {
                        this.shiharaiSakiTantousha = headerRow["SHIHARAI_SOUFU_TANTOU"].ToString() + ConstCls.ZENKAKU_SPACE + "様";// 支払先担当者
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(headerRow["SHIHARAI_SOUFU_TANTOU"].ToString()))
                {
                        this.shiharaiSakiBusho = headerRow["SHIHARAI_SOUFU_TANTOU"].ToString() + ConstCls.ZENKAKU_SPACE + "様";// 支払先担当者
                }
            }

            //登録番号
            this.tourokuNo = headerRow["TOUROKU_NO"].ToString();

            //拠点名を印字区分
            this.kyotenNamePrintKbn = headerRow["KYOTEN_NAME_PRINT_KBN"].ToString();

            //会社名
            this.corpName = headerRow["CORP_NAME"].ToString();
            //支払明細書拠点
            this.kyotenName = headerRow["KYOTEN_NAME"].ToString();
            //拠点代表者
            this.kyotenDaihyou = headerRow["KYOTEN_DAIHYOU"].ToString();
            //代表者
            this.corpDaihyou = headerRow["CORP_DAIHYOU"].ToString();
            //拠点郵便番号
            if (!string.IsNullOrEmpty(headerRow["KYOTEN_POST"].ToString()))
            {
                this.kyotenPost = ConstCls.YUBIN + headerRow["KYOTEN_POST"].ToString();
            }
            //拠点住所１
            this.kyotenAddress1 = headerRow["KYOTEN_ADDRESS1"].ToString();
            //拠点住所２
            this.kyotenAddress2 = headerRow["KYOTEN_ADDRESS2"].ToString();
            //拠点電話番号
            this.kyotenTel = "TEL" + ConstCls.ZENKAKU_SPACE + headerRow["KYOTEN_TEL"].ToString();
            //拠点FAX番号
            this.kyotenFax = "FAX" + ConstCls.ZENKAKU_SPACE + headerRow["KYOTEN_FAX"].ToString();

            //自社名 ～ 請求担当者 
            //10からリストの最後までのインデックス
            this.listCorpInfo = new List<string>();

            /* 自社名1・2、代表者名の印字スペースにおいて、印字される項目で下につめていく */
            //自社情報と拠点情報の表示条件に合わせて、空白を埋めるようにセット処理を変更
            // 1.自社情報入力ー会社名
            listCorpInfo.Add(corpName);
            //パターン1
            //＜T_SEISAN_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する場合＞
            if (ConstCls.PRINT_KBN_1.Equals(headerRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                // 2.取引先入力ー請求書拠点　
                listCorpInfo.Add(kyotenName);
            }
            ////パターン2
            ////＜T_SEISAN_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 2:印字しない＞
            //else
            //{
            //}
            // 3.BLANK
            listCorpInfo.Add(string.Empty);
            // 4.取引先‐支払明細書拠点に紐づく　拠点入力－郵便番号
            listCorpInfo.Add(kyotenPost);
            // 5.取引先‐支払明細書拠点に紐づく　拠点入力－住所１
            listCorpInfo.Add(kyotenAddress1);
            // 6.取引先‐支払明細書拠点に紐づく　拠点入力－住所２
            listCorpInfo.Add(kyotenAddress2);
            // 7.取引先‐支払明細書拠点に紐づく　拠点入力－　電話番号
            listCorpInfo.Add(kyotenTel);
            // 8.取引先‐支払明細書拠点に紐づく　拠点入力－　FAX番号
            listCorpInfo.Add(kyotenFax);

            // Detail部-Header部
            //支払年月日を設定
            this.shiharaiDate = string.Empty;

            if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_SIMEBI)
            {
                this.shiharaiDate = ((DateTime)headerRow["SEISAN_DATE"]).ToShortDateString();
            }
            else if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_HAKKOBI)
            {
                this.shiharaiDate = DateUtility.GetCurrentDateTime().ToShortDateString();
            }
            else if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_SITEI)
            {
                this.shiharaiDate = dto.ShiharaiDate.ToShortDateString();
            }

            // 「支払形態」の条件によって出力内容を分岐
            //   1:支払データ作成時 → T_SEISAN_DENPYOU.SHIHARAI_KEITAI_KBN を利用して発行
            //   2:単月精算         → 単月精算として発行
            //   3:繰越精算         → 繰越精算として発行
            if (dto.ShiharaiStyle == ConstCls.SHIHARAI_KEITAI_DATA_SAKUSEIJI)
            {
                if (headerRow["SHIHARAI_KEITAI_KBN"].ToString() == ConstCls.SHIHARAI_KEITAI_KBN_1)
                {
                    SetTangetsuReportHeader(headerRow);
                }
                else
                {
                    SetKurikoshiReportHeader(headerRow);
                }
            }
            else if (dto.ShiharaiStyle == ConstCls.SHIHARAI_KEITAI_TANGETU_SEIKYU)
            {
                SetTangetsuReportHeader(headerRow);
            }
            else
            {
                SetKurikoshiReportHeader(headerRow);
            }
            
            //備考1
            this.bikou1 = headerRow["BIKOU_1"].ToString();
            //備考2
            this.bikou2 = headerRow["BIKOU_2"].ToString();
            
        }

        /// <summary>
        /// 精算伝票ヘッダデータ設定
        /// </summary>
        /// <param name="dto">支払明細書発行用DTO</param>
        /// <param name="headerRow">精算伝票データ</param>
        private void SetReportHearderEmpty(ShiharaiDenpyouDto dto, DataRow headerRow)
        {
            //発行日
            if (dto.ShiharaiHakkou.Equals(ConstCls.SHIHARAI_HAKKOU_PRINT_SURU))
            {
                this.printDate = dto.HakkoBi;
            }
            else
            {
                this.printDate = string.Empty;
            }
            //取引先CD
            this.torihikisakiCD = dto.TorihikisakiCd;
            //支払番号
            this.shiharaiNumber = headerRow["SEISAN_NUMBER"].ToString();

            //拠点名を印字区分
            this.kyotenNamePrintKbn = headerRow["KYOTEN_NAME_PRINT_KBN"].ToString();

            // Detail部-Header部
            //支払年月日を設定
            this.shiharaiDate = string.Empty;

            if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_SIMEBI)
            {
                this.shiharaiDate = ((DateTime)headerRow["SEISAN_DATE"]).ToShortDateString();
            }
            else if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_HAKKOBI)
            {
                this.shiharaiDate = DateUtility.GetCurrentDateTime().ToShortDateString();
            }
            else if (dto.ShiharaiPrintDay == ConstCls.SHIHARAI_PRINT_DAY_SITEI)
            {
                this.shiharaiDate = dto.ShiharaiDate.ToShortDateString();
            }

            // 「支払形態」の条件によって出力内容を分岐
            //   1:支払データ作成時 → T_SEISAN_DENPYOU.SHIHARAI_KEITAI_KBN を利用して発行
            //   2:単月精算         → 単月精算として発行
            //   3:繰越精算         → 繰越精算として発行
            if (dto.ShiharaiStyle == ConstCls.SHIHARAI_KEITAI_DATA_SAKUSEIJI)
            {
                if (headerRow["SHIHARAI_KEITAI_KBN"].ToString() == ConstCls.SHIHARAI_KEITAI_KBN_1)
                {
                    SetTangetsuReportHeader(headerRow);
                }
                else
                {
                    SetKurikoshiReportHeader(headerRow);
                }
            }
            else if (dto.ShiharaiStyle == ConstCls.SHIHARAI_KEITAI_TANGETU_SEIKYU)
            {
                SetTangetsuReportHeader(headerRow);
            }
            else
            {
                SetKurikoshiReportHeader(headerRow);
            }

            //備考1
            this.bikou1 = headerRow["BIKOU_1"].ToString();
            //備考2
            this.bikou2 = headerRow["BIKOU_2"].ToString();

        }

        //// <summary>
        /// 支払形態が単月精算の明細ヘッダデータを作成します。
        /// </summary>
        /// <param name="startRow">精算データ先頭行</param>
        /// <returns>明細ヘッダデータ</returns>
        private void SetTangetsuReportHeader(DataRow headerRow)
        {

            var konkaiSeisanGaku = String.Empty;
            var torihikisakiCd = headerRow["TSD_TORIHIKISAKI_CD"].ToString();
            var zenkaiShiharaiGaku = String.Empty;
            var shiharaiGaku = String.Empty;
            var sosaiHoka = String.Empty;
            var kurikoshiGaku = String.Empty;
            var konkaiShiharaiGaku = headerRow["TSDK_KONKAI_SHIHARAI_GAKU"].ToString();
            var shouhizeiGaku = headerRow["SYOUHIZEIGAKU"].ToString();
            var goukeiSeisanGaku = KingakuAdd(konkaiShiharaiGaku, shouhizeiGaku);

            // 単月精算の今回精算額は、合計精算額と同じ額となる
            konkaiSeisanGaku = goukeiSeisanGaku;

            // 今回精算額
            this.konkaiSeisan = SetComma(konkaiSeisanGaku);

            // コード
            this.code = torihikisakiCd;

            // 前回繰越額
            this.zenkaiKurikoshi = string.Empty;

            // 支払額
            this.shiharaiGaku = string.Empty;

            // 調整額
            this.tyouseiHoka = string.Empty;

            // 差引繰越額
            this.sashihikiKurikosi = string.Empty;

            // 今回支払額
            this.konkaiShiharai = SetComma(konkaiShiharaiGaku);

            // 消費税額
            this.shouhizei = SetComma(shouhizeiGaku);

            // 合計精算額
            this.goukeiSeisan = SetComma(goukeiSeisanGaku);

        }

        /// <summary>
        /// 支払形態が繰越精算の明細ヘッダデータを作成します。
        /// </summary>
        /// <param name="startRow">精算データ先頭行</param>
        /// <returns>明細ヘッダデータ</returns>
        private void SetKurikoshiReportHeader(DataRow headerRow)
        {

            var konkaiSeisanGaku = String.Empty;
            var torihikisakiCd = headerRow["TSD_TORIHIKISAKI_CD"].ToString();
            var zenkaiShiharaiGaku = headerRow["ZENKAI_KURIKOSI_GAKU"].ToString();
            var shiharaiGaku = headerRow["KONKAI_SHUKKIN_GAKU"].ToString();
            var sosaiHoka = headerRow["KONKAI_CHOUSEI_GAKU"].ToString();
            var kurikoshiGaku = KingakuSubtract(KingakuSubtract(zenkaiShiharaiGaku, shiharaiGaku), sosaiHoka);
            var konkaiShiharaiGaku = headerRow["TSDK_KONKAI_SHIHARAI_GAKU"].ToString();
            var shouhizeiGaku = headerRow["SYOUHIZEIGAKU"].ToString();
            var goukeiSeisanGaku = KingakuAdd(konkaiShiharaiGaku, shouhizeiGaku);

            // 繰越精算の今回精算額は、差引繰越額と合計精算額を足した額となる
            konkaiSeisanGaku = KingakuAdd(kurikoshiGaku, goukeiSeisanGaku);

            var shoshikiKbn = headerRow["SHOSHIKI_KBN"].ToString();
            if (ConstCls.SHOSHIKI_KBN_1 != shoshikiKbn
                && (false == String.IsNullOrEmpty(headerRow["GYOUSHA_CD"].ToString()) || false == String.IsNullOrEmpty(headerRow["GENBA_CD"].ToString()))
                )
            {
                zenkaiShiharaiGaku = "0";
                shiharaiGaku = "0";
                sosaiHoka = "0";
                kurikoshiGaku = "0";
                konkaiSeisanGaku = goukeiSeisanGaku;
            }

            // 今回精算額
            this.konkaiSeisan = SetComma(konkaiSeisanGaku);

            // コード
            this.code = torihikisakiCd;

            // 前回繰越額
            this.zenkaiKurikoshi = SetComma(zenkaiShiharaiGaku);

            // 支払額
            this.shiharaiGaku = SetComma(shiharaiGaku);

            // 調整額
            this.tyouseiHoka = SetComma(sosaiHoka);

            // 差引繰越額
            this.sashihikiKurikosi = SetComma(kurikoshiGaku);

            // 今回支払額
            this.konkaiShiharai = SetComma(konkaiShiharaiGaku);

            // 消費税額
            this.shouhizei = SetComma(shouhizeiGaku);

            // 合計精算額
            this.goukeiSeisan = SetComma(goukeiSeisanGaku);

        }

        /// <summary>
        /// C1Reportの帳票データの明細の作成を実行する
        /// </summary>
        /// <param name="denpyouData"></param>
        /// <param name="shukkinData"></param>
        private void CreateDetailReportData(DataTable denpyouData, DataTable shukkinData)
        {
            string formatKingaku = "#,0";          // 金額フォーマット （0は0を返す）

            this.ChouhyouDataTable = new DataTable();

            // 明細データTABLEカラムセット

            this.ChouhyouDataTable.Columns.Add("PHY_GROUP_NAME_FLB");                     // 月日
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_DATE_FLB");                     // 月日
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_NUMBER_FLB");                   // 支払No.
            this.ChouhyouDataTable.Columns.Add("PHY_HINMEI_NAME_FLB");                      // 品名
            this.ChouhyouDataTable.Columns.Add("PHY_SUURYOU_FLB");                          // 数量
            this.ChouhyouDataTable.Columns.Add("PHY_SUURYOU_SHOUSUU_FLB");                  // 数量（小数部）
            this.ChouhyouDataTable.Columns.Add("PHY_UNIT_NAME_FLB");                        // 単位
            this.ChouhyouDataTable.Columns.Add("PHY_TANKA_FLB");                            // 単価
            this.ChouhyouDataTable.Columns.Add("PHY_TANKA_SHOUSUU_FLB");                    // 単価（小数部）
            this.ChouhyouDataTable.Columns.Add("PHY_KINGAKU_FLB");                          // 金額
            this.ChouhyouDataTable.Columns.Add("PHY_SHOUHIZEI_FLB");                          // 金額
            this.ChouhyouDataTable.Columns.Add("PHY_MEISAI_BIKOU_FLB");                     // 備考
            this.ChouhyouDataTable.Columns.Add("PHY_DATA_KBN_FLB");                     //
            foreach (DataRow row in shukkinData.Rows)
            {
                DataRow ChouhyouRow = this.ChouhyouDataTable.NewRow();
                ChouhyouRow["PHY_GROUP_NAME_FLB"] = row["GROUP_NAME"];
                ChouhyouRow["PHY_DENPYOU_DATE_FLB"] = row["DENPYOU_DATE"];
                ChouhyouRow["PHY_DENPYOU_NUMBER_FLB"] = row["DENPYOU_NUMBER"];
                ChouhyouRow["PHY_HINMEI_NAME_FLB"] = row["HINMEI_NAME"];
                ChouhyouRow["PHY_UNIT_NAME_FLB"] = row["UNIT_NAME"];
                //ChouhyouRow["PHY_SUURYOU_FLB"] = SetFormat(row["SUURYOU"].ToString(), this.MSysInfo.SYS_SUURYOU_FORMAT);
                char[] separator = new char[] { '.' };

                string[] splittedSuuryou = row["SUURYOU"].ToString().Split(separator);
                ChouhyouRow["PHY_SUURYOU_FLB"] = this.SetFormat(splittedSuuryou[0], this.MSysInfo.SYS_SUURYOU_FORMAT).ZeroTorimu();       // 数量（整数部）
                ChouhyouRow["PHY_SUURYOU_SHOUSUU_FLB"] = string.Empty;
                if (splittedSuuryou.Length == 2)
                {
                    ChouhyouRow["PHY_SUURYOU_SHOUSUU_FLB"] = ("." + splittedSuuryou[1]).ZeroTorimu();       // 数量（小数部）
                }

                //ChouhyouRow["PHY_TANKA_FLB"] = SetFormat(row["TANKA"].ToString(), this.MSysInfo.SYS_TANKA_FORMAT);
                string[] splittedTanka = row["TANKA"].ToString().Split(separator);
                ChouhyouRow["PHY_TANKA_FLB"] = this.SetFormat(splittedTanka[0], this.MSysInfo.SYS_SUURYOU_FORMAT).ZeroTorimu();       // 単価（整数部）
                ChouhyouRow["PHY_TANKA_SHOUSUU_FLB"] = string.Empty;
                if (splittedTanka.Length == 2)
                {
                    ChouhyouRow["PHY_TANKA_SHOUSUU_FLB"] = ("." + splittedTanka[1]).ZeroTorimu();       // 単価（小数部）
                }

                ChouhyouRow["PHY_KINGAKU_FLB"] = SetFormat(row["KINGAKU"].ToString(), formatKingaku);
                if (row["DATA_KBN"].ToString().Equals("ZEI"))
                {
                    ChouhyouRow["PHY_SHOUHIZEI_FLB"] = SetFormat(row["SHOUHIZEI"].ToString(), formatKingaku);
                }
                else
                {
                    ChouhyouRow["PHY_SHOUHIZEI_FLB"] = row["SHOUHIZEI"].ToString();
                }
                ChouhyouRow["PHY_MEISAI_BIKOU_FLB"] = row["MEISAI_BIKOU"];
                ChouhyouRow["PHY_DATA_KBN_FLB"] = row["DATA_KBN"];
                this.ChouhyouDataTable.Rows.Add(ChouhyouRow);
            }
            foreach (DataRow row in denpyouData.Rows)
            {
                DataRow ChouhyouRow = this.ChouhyouDataTable.NewRow();
                ChouhyouRow["PHY_GROUP_NAME_FLB"] = row["GROUP_NAME"];
                ChouhyouRow["PHY_DENPYOU_DATE_FLB"] = row["DENPYOU_DATE"];
                ChouhyouRow["PHY_DENPYOU_NUMBER_FLB"] = row["DENPYOU_NUMBER"];
                ChouhyouRow["PHY_HINMEI_NAME_FLB"] = row["HINMEI_NAME"];
                ChouhyouRow["PHY_UNIT_NAME_FLB"] = row["UNIT_NAME"];
                //ChouhyouRow["PHY_SUURYOU_FLB"] = SetFormat(row["SUURYOU"].ToString(), this.MSysInfo.SYS_SUURYOU_FORMAT);
                char[] separator = new char[] { '.' };

                string[] splittedSuuryou = row["SUURYOU"].ToString().Split(separator);
                ChouhyouRow["PHY_SUURYOU_FLB"] = this.SetFormat(splittedSuuryou[0], this.MSysInfo.SYS_SUURYOU_FORMAT).ZeroTorimu();       // 数量（整数部）
                ChouhyouRow["PHY_SUURYOU_SHOUSUU_FLB"] = string.Empty;
                if (splittedSuuryou.Length == 2)
                {
                    ChouhyouRow["PHY_SUURYOU_SHOUSUU_FLB"] = ("." + splittedSuuryou[1]).ZeroTorimu();       // 数量（小数部）
                }
                //ChouhyouRow["PHY_TANKA_FLB"] = SetFormat(row["TANKA"].ToString(), this.MSysInfo.SYS_TANKA_FORMAT);
                string[] splittedTanka = row["TANKA"].ToString().Split(separator);
                ChouhyouRow["PHY_TANKA_FLB"] = this.SetFormat(splittedTanka[0], this.MSysInfo.SYS_SUURYOU_FORMAT).ZeroTorimu();       // 単価（整数部）
                ChouhyouRow["PHY_TANKA_SHOUSUU_FLB"] = string.Empty;
                if (splittedTanka.Length == 2)
                {
                    ChouhyouRow["PHY_TANKA_SHOUSUU_FLB"] = ("." + splittedTanka[1]).ZeroTorimu();       // 単価（小数部）
                }

                ChouhyouRow["PHY_KINGAKU_FLB"] = SetFormat(row["KINGAKU"].ToString(), formatKingaku);
                if (row["DATA_KBN"].ToString().Equals("ZEI"))
                {
                    ChouhyouRow["PHY_SHOUHIZEI_FLB"] = SetFormat(row["SHOUHIZEI"].ToString(), formatKingaku);
                }
                else
                {
                    ChouhyouRow["PHY_SHOUHIZEI_FLB"] = row["SHOUHIZEI"].ToString();
                }
                ChouhyouRow["PHY_MEISAI_BIKOU_FLB"] = row["MEISAI_BIKOU"];
                ChouhyouRow["PHY_DATA_KBN_FLB"] = row["DATA_KBN"];
                this.ChouhyouDataTable.Rows.Add(ChouhyouRow);
            }
            if (this.ChouhyouDataTable.Rows.Count <= ConstCls.MAX_ROW_PAGE_ONE)
            {
                for (int i = this.ChouhyouDataTable.Rows.Count; i < ConstCls.MAX_ROW_PAGE_ONE; i++)
                {
                    DataRow ChouhyouRow = this.ChouhyouDataTable.NewRow();
                    this.ChouhyouDataTable.Rows.Add(ChouhyouRow);

                }
            }
            else
            {
                int countRow = this.ChouhyouDataTable.Rows.Count - ConstCls.MAX_ROW_PAGE_ONE;
                countRow = countRow % ConstCls.MAX_ROW_PAGE_ONE_OVER;
                if (countRow != 0)
                {
                    for (int i = countRow; i < ConstCls.MAX_ROW_PAGE_ONE_OVER; i++)
                    {
                        DataRow ChouhyouRow = this.ChouhyouDataTable.NewRow();
                        this.ChouhyouDataTable.Rows.Add(ChouhyouRow);

                    }
                }
            }
        }

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="dto"></param>
        /// <param name="headerRow"></param>
        /// <param name="denpyouData"></param>
        /// <param name="shukkinData"></param>
        public void CreateReportData(ShiharaiDenpyouDto dto, DataRow headerRow, DataTable denpyouData, DataTable shukkinData)
        {
            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.SHUKKIN_MEISAI_NASHI && string.IsNullOrEmpty(headerRow["KAGAMI_NUMBER"].ToString()))
            {
                SetReportHearderEmpty(dto, headerRow);
            }
            else
            {
                SetReportHearder(dto, headerRow);
            }
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            CreateDetailReportData(denpyouData, shukkinData);

            /* LAYOUT1 & LAYOUT2(SubReport) */
            Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            dic.Add(OutputFormLayout, this.ChouhyouDataTable);

            // 明細データTABLEをセット
            this.SetRecord(this.ChouhyouDataTable);

            // データテーブル情報から帳票情報作成処理を実行する
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dic);

        }

        /// <summary>
        /// CSVデータの作成を実行する
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="headerRow"></param>
        /// <param name="denpyouData"></param>
        /// <param name="shukkinData"></param>
        /// <returns></returns>
        internal DataTable CreateCsvData(ShiharaiDenpyouDto dto, DataRow headerRow, DataTable denpyouData, DataTable shukkinData)
        {
            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.SHUKKIN_MEISAI_NASHI && string.IsNullOrEmpty(headerRow["KAGAMI_NUMBER"].ToString()))
            {
                SetReportHearderEmpty(dto, headerRow);
            }
            else
            {
                SetReportHearder(dto, headerRow);
            }
            string formatKingaku = "#,0";          // 金額フォーマット （0は0を返す）

            string[] csvHead = { "head_1", "head_2", "head_3", "head_4", "head_5", "head_6", "head_7", "head_8", "head_9", "head_10", "head_11" };

            DataTable csvDT = new DataTable();
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }

            DataRow rowTmp;
            // 1行目 ~ 7行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSakiPostCD.Replace("〒","").Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSakiAddress1.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSakiAddress2.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSaki1DH.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSaki2DH.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSakiBusho.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiSakiTantousha.Trim();
            csvDT.Rows.Add(rowTmp);

            // 8行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = "支払年月日";
            rowTmp["head_2"] = "コード";
            rowTmp["head_3"] = "今回御精算額";

            if (String.IsNullOrEmpty(zenkaiKurikoshi))
            {
                rowTmp["head_4"] = "";
                rowTmp["head_5"] = "";
                rowTmp["head_6"] = "";
                rowTmp["head_7"] = "";
            }
            else
            {
                rowTmp["head_4"] = "前回精算額";
                rowTmp["head_5"] = "今回出金額";
                rowTmp["head_6"] = "調整額";
                rowTmp["head_7"] = "繰越額";
            }

            rowTmp["head_8"] = "今回取引額（税抜）";
            rowTmp["head_9"] = "消費税";
            rowTmp["head_10"] = "今回取引額";
            csvDT.Rows.Add(rowTmp);

            // 9行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = shiharaiDate;
            rowTmp["head_2"] = code;
            rowTmp["head_3"] = konkaiSeisan;
            rowTmp["head_4"] = zenkaiKurikoshi;
            rowTmp["head_5"] = shiharaiGaku;
            rowTmp["head_6"] = tyouseiHoka;
            rowTmp["head_7"] = sashihikiKurikosi;
            rowTmp["head_8"] = konkaiShiharai;
            rowTmp["head_9"] = shouhizei;
            rowTmp["head_10"] = goukeiSeisan;
            csvDT.Rows.Add(rowTmp);

            // 10行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = "業者名";
            rowTmp["head_2"] = "現場名";
            rowTmp["head_3"] = "月日";
            rowTmp["head_4"] = "支払No";
            rowTmp["head_5"] = "品名";
            rowTmp["head_6"] = "数量";
            rowTmp["head_7"] = "単位";
            rowTmp["head_8"] = "単価";
            rowTmp["head_9"] = "金額";
            rowTmp["head_10"] = "消費税";
            rowTmp["head_11"] = "備考";
            csvDT.Rows.Add(rowTmp);

            // 出金明細追加
            foreach (DataRow row in shukkinData.Rows)
           {
                if (row["HINMEI_NAME"].Equals("出金計"))
                {
                    row["HINMEI_NAME"] = "【" + row["HINMEI_NAME"] + "】";
                }
                rowTmp = csvDT.NewRow();
                rowTmp["head_3"] = row["DENPYOU_DATE"];
                rowTmp["head_4"] = row["DENPYOU_NUMBER"];
                rowTmp["head_5"] = row["HINMEI_NAME"];
                rowTmp["head_7"] = row["UNIT_NAME"];
                rowTmp["head_6"] = SetFormat(row["SUURYOU"].ToString(), this.MSysInfo.SYS_SUURYOU_FORMAT);
                rowTmp["head_8"] = SetFormat(row["TANKA"].ToString(), this.MSysInfo.SYS_TANKA_FORMAT);
                rowTmp["head_9"] = SetFormat(row["KINGAKU"].ToString(), formatKingaku);
                rowTmp["head_10"] = SetFormat(row["SHOUHIZEI"].ToString(), formatKingaku);
                rowTmp["head_11"] = row["MEISAI_BIKOU"];
                csvDT.Rows.Add(rowTmp);
            }

            // 伝票明細追加
            string gyoushaCd = string.Empty;                // 業者CD
            string gyoushaName = string.Empty;              // 業者名
            string genbaCd = string.Empty;                  // 現場CD
            string genbaName = string.Empty;                // 現場名

            foreach (DataRow row in denpyouData.Rows)
            {
                if(row["DATA_KBN"].ToString()=="GYOUSHA_GROUP")
                {
                    gyoushaName = row["GROUP_NAME"].ToString();
                    continue;
                }
                if (row["DATA_KBN"].ToString() == "GENBA_GROUP")
                {
                    genbaName = row["GROUP_NAME"].ToString();
                    continue;
                }
                if( row["HINMEI_NAME"].Equals("業者計")
                    || row["HINMEI_NAME"].Equals("現場計"))
                {
                    row["HINMEI_NAME"] = "【" + row["HINMEI_NAME"] + "】";
                }

                rowTmp = csvDT.NewRow();
                if (row["HINMEI_NAME"].Equals("【精算毎消費税】") 
                    || row["HINMEI_NAME"].Equals("【精算毎消費税(内)】") 
                    || row["HINMEI_NAME"].Equals("【精算毎消費税】")
                    || row["HINMEI_NAME"].Equals("【業者計】")
                    || row["HINMEI_NAME"].Equals("【現場計】")
                    || row["DATA_KBN"].Equals("ZEI_BLANK")
                    || row["DATA_KBN"].Equals("ZEI")
                    )
                {
                    rowTmp["head_1"] = string.Empty;
                    rowTmp["head_2"] = string.Empty;
                }
                else
                {
                    rowTmp["head_1"] = gyoushaName;
                    rowTmp["head_2"] = genbaName;
                }
                rowTmp["head_3"] = row["DENPYOU_DATE"];
                rowTmp["head_4"] = row["DENPYOU_NUMBER"];
                rowTmp["head_5"] = row["HINMEI_NAME"];
                rowTmp["head_7"] = row["UNIT_NAME"];
                rowTmp["head_6"] = SetFormat(row["SUURYOU"].ToString(), this.MSysInfo.SYS_SUURYOU_FORMAT);
                rowTmp["head_8"] = SetFormat(row["TANKA"].ToString(), this.MSysInfo.SYS_TANKA_FORMAT);
                rowTmp["head_9"] = SetFormat(row["KINGAKU"].ToString(), formatKingaku);
                if (row["DATA_KBN"].ToString().Equals("ZEI"))
                {
                    rowTmp["head_10"] = SetFormat(row["SHOUHIZEI"].ToString(), formatKingaku);
                }
                else
                {
                    rowTmp["head_10"] = row["SHOUHIZEI"].ToString();
                }
                rowTmp["head_11"] = row["MEISAI_BIKOU"];
                csvDT.Rows.Add(rowTmp);
            }

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = "登録番号";
            rowTmp["head_2"] = this.tourokuNo;
            csvDT.Rows.Add(rowTmp);

            return csvDT;
        }
        
        #endregion
    }

    #endregion




}
