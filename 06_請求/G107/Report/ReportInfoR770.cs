using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Billing.SeikyuushoHakkou.Const;
using r_framework.Dao;
using r_framework.Utility;
using Shougun.Core.FileUpload.FileUploadCommon;
using Shougun.Core.FileUpload.FileUploadCommon.Logic;
using System.IO;
using Shougun.Core.Common.BusinessCommon.Xml;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    #region - Class -

    /// <summary> R770(請求書)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR770 : ReportInfoBase
    {
        #region - Fields -

        // Detail部データテーブル
        private DataTable chouhyouDataTable = new DataTable();

        // Header部
        // 1-1
        private string torihikisakiCD = string.Empty;           // 取引先CD
        private string seikyuuSaki1H = string.Empty;            // 請求先1
        private string seikyuuSaki2H = string.Empty;            // 請求先2
        private string printDate = string.Empty;                // 発行日
        private string seikyuuNumber = string.Empty;            // 請求番号
        private string taitoru = "請　求　書";                  // タイトル【固定】

        // Detail部-Header部
        // 1-2  
        private string seikyuuSakiPostCD = string.Empty;        // 請求先郵便番号
        private string seikyuuSakiAddress1 = string.Empty;      // 請求先住所1
        private string seikyuuSakiAddress2 = string.Empty;      // 請求先住所2
        private string seikyuuSaki1DH = string.Empty;           // 請求先1
        private string seikyuuSaki2DH = string.Empty;           // 請求先2
        private string seikyuuSakiBusho = string.Empty;         // 請求先部署
        private string seikyuuSakiTantousha = string.Empty;     // 請求先担当者

        private List<string> listCorpInfo = new List<string>(); //自社名 ～ 請求担当者
        private string daihyouPrintKbn = string.Empty;          // 代表者を印字区分 
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
        private string seikyuuTantou = string.Empty;            //請求担当者　
        private string kakinotori = "下記の通りご請求申し上げます。";  // 【固定】 
        
        // Detail部-Detail部
        // 1-3
        private string konkaiSeikyuuLabel = "今回御請求額";     // 今回御請求額label【固定】
        private string konkaiSeikyuu = string.Empty;            // 今回御請求額
        private string seikyuuDateLabel = "請求年月日";         // 請求年月日label【固定】
        private string seikyuuDate = string.Empty;              // 請求年月日
        private string codeLabel = "コード";                    // コードlabel【固定】
        private string code = string.Empty;                     // コード
        private string zenkaiSeikyuuLabel = "前回請求額";     // 前回請求額label【固定】
        private string zenkaiSeikyuu = string.Empty;            // 前回請求額
        private string nyuukinGakuLabel = "今回入金額";             // 今回入金額label【固定】
        private string nyuukinGaku = string.Empty;              // 今回入金額
        private string sousaiHokaLabel = "調整額";              // 調整額label【固定】
        private string sousaiHoka = string.Empty;               // 調整額
        private string sashihikiKurikosiLabel = "繰越額";   // 繰越額label【固定】
        private string sashihikiKurikosi = string.Empty;        // 繰越額
        private string konkaiUriageLabel = "今回取引額(税抜)";        // 今回取引額(税抜)label【固定】
        private string konkaiUriage = string.Empty;             // 今回取引額(税抜)
        private string shouhizeiLabel = "消費税";             // 消費税label【固定】
        private string shouhizei = string.Empty;                // 消費税
        private string goukeiSeikyuuLabel = "今回取引額";       // 今回取引額label【固定】
        private string goukeiSeikyuu = string.Empty;            // 今回取引額

        // Detail部-footer部
        // 1-4
        private string furikomiGinkouLabel = "[振込口座]";        // 振込銀行label【固定】
        private GinkouDtoClass ginkouDto1 = new GinkouDtoClass();
        private GinkouDtoClass ginkouDto2 = new GinkouDtoClass();
        private GinkouDtoClass ginkouDto3 = new GinkouDtoClass();
        // Detail部-footer部
        // 1-5
        private string bikouLabel = "備考：";                   // 備考label【固定】
        private string bikou1 = string.Empty;                   // 備考1
        private string bikou2 = string.Empty;                   // 備考2
        private string tourokuNo = string.Empty;                   // 登録番号

        /// <summary>
        /// システム設定入力ファイル連携データ
        /// </summary>
        M_FILE_LINK_SYS_INFO fileLink;

        /// <summary>
        /// システム設定入力ファイル連携Dao
        /// </summary>
        private IM_FILE_LINK_SYS_INFODao fileLinkSysInfoDao;

        /// <summary>
        /// ファイルデータ
        /// </summary>
        T_FILE_DATA fileData;

        /// <summary>
        /// ファイルデータDao
        /// </summary>
        private FILE_DATADAO fileDataDao;

        /// <summary>
        /// ファイルアップロード処理クラス
        /// </summary>
        public FileUploadLogic uploadLogic;

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR361" /> class. </summary>
        public ReportInfoR770()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R770-Form.xml";

            // 帳票出力フルパスフォーム名(車輛名表示用)
            this.OutputFormSyaryouFullPathName = "./Template/R770_2-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            this.fileLink = new M_FILE_LINK_SYS_INFO();
            this.fileLinkSysInfoDao = DaoInitUtility.GetComponent<IM_FILE_LINK_SYS_INFODao>();
            this.uploadLogic = new FileUploadLogic();
            this.fileData = new T_FILE_DATA();
            this.fileDataDao = DaoInitUtility.GetComponent<FILE_DATADAO>();

        }

        #endregion

        #region - properties -
        /// <summary>帳票出力フルパスフォーム名を保持するフィールド</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フルパスフォーム名(車輛名表示用)を保持するフィールド</summary>
        public string OutputFormSyaryouFullPathName { get; set; }

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
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME1_1_CTL", this.seikyuuSaki1H);          // 請求先1
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME2_1_CTL", this.seikyuuSaki2H);          // 請求先2            
            this.SetFieldName("FH_PRINT_DATE_VLB", headerPrintDate);                        // 発行日
            this.SetFieldName("FH_SEIKYUU_NUMBER_CTL", "No." + this.seikyuuNumber);         // 請求番号
            this.SetFieldName("FH_TITLE_FLB", this.taitoru);                                // タイトル【固定】"請　求　書"

            // page-Header部
            this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", this.torihikisakiCD);              // 取引先CD
            this.SetFieldName("PHY_SEIKYUU_SOUFU_NAME1_CTL", this.seikyuuSaki1H);           // 請求先1
            this.SetFieldName("PHY_SEIKYUU_SOUFU_NAME2_CTL", this.seikyuuSaki2H);           // 請求先2
            this.SetFieldName("PHY_PRINT_DATE_VLB", headerPrintDate);                       // 発行日
            this.SetFieldName("PHY_SEIKYUU_NUMBER_CTL", "No." + this.seikyuuNumber);        // 請求番号

            // Detail-Header部
            // 1-2
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL", this.seikyuuSakiPostCD);         // 請求先郵便番号
            this.SetFieldName("FH_SEIKYUU_SOUFU_ADDRESS1_CTL", this.seikyuuSakiAddress1);   // 請求先住所1
            this.SetFieldName("FH_SEIKYUU_SOUFU_ADDRESS2_CTL", this.seikyuuSakiAddress2);   // 請求先住所2
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME1_2_CTL", this.seikyuuSaki1DH);         // 請求先1
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME2_2_CTL", this.seikyuuSaki2DH);         // 請求先2
            this.SetFieldName("FH_SEIKYUU_SOUFU_BUSHO_CTL", this.seikyuuSakiBusho);         // 請求先部署
            this.SetFieldName("FH_SEIKYUU_SOUFU_TANTOU_CTL", this.seikyuuSakiTantousha);    // 請求先担当者
            
            //自社名 ～ 請求担当者
            for (int i = 0; i < Const.ConstCls.MAX_ROW_CORP_INFO; i++)
            {
                this.SetFieldName(string.Format("FH_CORP_INFO{0}_CTL", i + 1),string.Empty);
            }
            for (int i = 0; i < this.listCorpInfo.Count; i++)
            {
                this.SetFieldName(string.Format("FH_CORP_INFO{0}_CTL", i+1), this.listCorpInfo[i]); 
            }
            //取引先入力ー請求書拠点　（太字フォント）
            if (ConstCls.PRINT_KBN_1.Equals(this.daihyouPrintKbn)
                && ConstCls.PRINT_KBN_1.Equals(this.kyotenNamePrintKbn))
            {
                this.SetFieldBold("FH_CORP_INFO3_CTL", true);
            }
            this.SetFieldName("FH_KAKINOTORI_FBL", this.kakinotori);                        // 【固定】"下記の通りご請求申し上げます。"

            // 請求先郵便番号がない場合、〒マークを印字しない。
            if (String.Equals(this.seikyuuSakiPostCD, Const.ConstCls.YUBIN))
            {
                this.SetFieldVisible("FH_SEIKYUU_SOUFU_POST_CTL", false);
            }

            // Detail-Header部
            // 1-3
            this.SetFieldName("FH_KONKAI_SEIKYU_GAKU_FLB", this.konkaiSeikyuuLabel);            // 今回御請求額label【固定】"今回御請求額"
            this.SetFieldName("FH_KONKAI_SEIKYU_GAKU_CTL", "\\" + this.konkaiSeikyuu);          // 合計請求額:仕様変更(今回請求額と合計請求額の表示箇所入替え)
            this.SetFieldName("FH_SEIKYU_NENGATSUPI_FLB", this.seikyuuDateLabel);               // 請求年月日label【固定】"請求年月日"
            this.SetFieldName("FH_SEIKYU_NENGATSUPI_CTL", this.seikyuuDate);                    // 請求年月日
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_FLB", this.codeLabel);                      // コードlabel【固定】"コード"
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_CTL", this.code);                           // コード
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_FLB", this.zenkaiSeikyuuLabel);          // 前回御請求額label【固定】"前回御請求額"
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_CTL", this.zenkaiSeikyuu);               // 前回御請求額
            this.SetFieldName("FH_KONKAI_NYUUKIN_GAKU_FLB", this.nyuukinGakuLabel);             // 入金額label【固定】"入金額"
            this.SetFieldName("FH_KONKAI_NYUUKIN_GAKU_CTL", this.nyuukinGaku);                  // 入金額
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_FLB", this.sousaiHokaLabel);              // 相殺他label【固定】"相殺他"
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_CTL", this.sousaiHoka);                   // 相殺他
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_FLB", this.sashihikiKurikosiLabel);  // 差引繰越額label【固定】"差引繰越額"
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_CTL", this.sashihikiKurikosi);       // 差引繰越額
            this.SetFieldName("FH_KONKAI_URIAGE_GAKU_FLB", this.konkaiUriageLabel);             // 今回売上額label【固定】"今回売上額"
            this.SetFieldName("FH_KONKAI_URIAGE_GAKU_CTL", this.konkaiUriage);                  // 今回売上額
            this.SetFieldName("FH_SHOUHIZEI_GAKU_FLB", this.shouhizeiLabel);                    // 消費税額label【固定】"消費税額"
            this.SetFieldName("FH_SHOUHIZEI_GAKU_CTL", this.shouhizei);                         // 消費税額
            this.SetFieldName("FH_GOUKEI_GOSEIKYUU_GAKU_FLB", this.goukeiSeikyuuLabel);         // 合計請求額label【固定】"合計請求額"
            this.SetFieldName("FH_GOUKEI_GOSEIKYUU_GAKU_CTL", this.goukeiSeikyuu);              // 今回御請求額:仕様変更(今回請求額と合計請求額の表示箇所入替え)

            if (String.IsNullOrEmpty(this.zenkaiSeikyuu))
            {
                this.SetFieldVisible("FH_ZENKAI_KURIKOSI_GAKU_FLB", false);
                this.SetFieldVisible("FH_ZENKAI_KURIKOSI_GAKU_CTL", false);
                this.SetFieldVisible("FH_KONKAI_NYUUKIN_GAKU_FLB", false);
                this.SetFieldVisible("FH_KONKAI_NYUUKIN_GAKU_CTL", false);
                this.SetFieldVisible("FH_KONKAI_CHOUSEI_GAKU_FLB", false);
                this.SetFieldVisible("FH_KONKAI_CHOUSEI_GAKU_CTL", false);
                this.SetFieldVisible("FH_SASHIHIKI_KURIKOSHI_GAKU_FLB", false);
                this.SetFieldVisible("FH_SASHIHIKI_KURIKOSHI_GAKU_CTL", false);
            }

            // Detail-Header部
            // 1-4
            this.UpdateGinkouField();
            // Page-Footer部
            // 1-5
            this.SetFieldName("FF_BIKOU_FLB", this.bikouLabel);     // 備考label【固定】"備考："
            this.SetFieldName("FF_BIKOU1_CTL", this.bikou1);        // 備考1
            this.SetFieldName("FF_BIKOU2_CTL", this.bikou2);        // 備考2
            this.SetFieldName("FF_TOUROKUNO_CTL", this.tourokuNo);        // 登録番号

            // 角印ファイルの配置
            // 角印ファイルパス取得
            string imagePath = "";
            // システム設定入力データからファイルID取得
            fileLink = fileLinkSysInfoDao.GetDataById("0");

            if (fileLink != null)
            {
                // ファイルIDからファイル情報を取得
                long fileId = (long)fileLink.FILE_ID;
                fileData = this.fileDataDao.GetDataByKey(fileId);
                imagePath = fileData.FILE_PATH;

                // ファイルの存在確認
                if (!(File.Exists(imagePath)))
                {
                    // 初期フォルダを取得
                    // ユーザ定義情報を取得
                    CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();

                    // ファイルアップロード参照先のフォルダを取得
                    string folderPath = this.uploadLogic.GetUserProfileValue(userProfile, "ファイルアップロード参照先");

                    // 一時ファイル作成
                    imagePath = Path.Combine(folderPath, Path.GetFileName(fileData.FILE_PATH));

                    // ファイルが存在していない場合はバイナリデータからファイルを作成
                    using (var fs = new FileStream(imagePath, FileMode.Create))
                    {
                        using (var bw = new BinaryWriter(fs))
                        {
                            bw.Write(fileData.BINARY_DATA.Value);
                        }
                    }
                }
                this.SetFieldImage("FH_KAKUIN_FLB", imagePath); 　　//画像の読み込み
            }

            // 角印ファイルの位置情報初期値設定
            int kakuinPositionTop = 1035;
            int kakuinPositionLeft = 8445;

            // 角印ファイルの位置情報取得
            if (MSysInfo.KAKUIN_POSITION_TOP > 0)
            {
                kakuinPositionTop = (int)(MSysInfo.KAKUIN_POSITION_TOP);
            }
            if (MSysInfo.KAKUIN_POSITION_LEFT > 0)
            {
                kakuinPositionLeft = (int)(MSysInfo.KAKUIN_POSITION_LEFT);
            }
            this.SetFieldTop("FH_KAKUIN_FLB", kakuinPositionTop);            //画像の縦位置
            this.SetFieldLeft("FH_KAKUIN_FLB", kakuinPositionLeft);          //画像の横位置

            // 角印ファイルの幅設定
            // 初期値　18mm(約1020Twips) × 18mm(約1020Twips)
            int kakuinSizeWidth = 1020;
            int kakuinSizeHeight = 1020;

            if (MSysInfo.KAKUIN_SIZE == 2)
            {
                // 21mm(約1190Twips) × 21mm(約1190Twips)
                kakuinSizeWidth = 1190;
                kakuinSizeHeight = 1190;
            }
            else if (MSysInfo.KAKUIN_SIZE == 3)
            {
                // 24mm(約1360Twips) × 24mm(約1360Twips)
                kakuinSizeWidth = 1360;
                kakuinSizeHeight = 1360;
            }

            this.SetFieldHeight("FH_KAKUIN_FLB", kakuinSizeHeight); // 画像の高さ
            this.SetFieldWidth("FH_KAKUIN_FLB", kakuinSizeWidth);　 // 画像の幅
        }

		 /// <summary>
        /// ３銀行表示できるようにレイアウトを変更
        /// </summary>
        private void UpdateGinkouField()
        {
            this.SetFieldName("FH_FURIKOMI_GINKO_FLB", this.furikomiGinkouLabel);   // 振込銀行label【固定】"振込銀行"
            //4列
            int MaxLineGinkouInfo = 4;
            List<string> listGinkouInfo = new List<string>();
            //請求書を印字する取引先に銀行が３銀行セットされている場合
            if (!string.IsNullOrWhiteSpace(this.ginkouDto1.ginkouMei)
                && !string.IsNullOrWhiteSpace(this.ginkouDto2.ginkouMei)
                && !string.IsNullOrWhiteSpace(this.ginkouDto3.ginkouMei))
            {
                string lineGinkouInfo1 = this.CreateGinkouInfo(this.ginkouDto1);
                listGinkouInfo.Add(lineGinkouInfo1);

                string lineGinkouInfo2 = this.CreateGinkouInfo(this.ginkouDto2);
                listGinkouInfo.Add(lineGinkouInfo2);

                string lineGinkouInfo3 = this.CreateGinkouInfo(this.ginkouDto3);
                listGinkouInfo.Add(lineGinkouInfo3);
            }
            //請求書を印字する取引先に銀行が２銀行、または1銀行がセットされている場合
            else
            {
                //1行目: 銀行名　＋　▲(space 2 byte)　＋　銀行支店名
                //2行目:▲(space 2 byte)　＋　口座種類　＋　▲(space 2 byte)　＋　口座番号　＋　▲(space 2 byte)　＋　口座名
                if (!string.IsNullOrWhiteSpace(this.ginkouDto1.ginkouMei))
                {
                    listGinkouInfo.AddRange(this.CreateSplitGinkouInfo(this.ginkouDto1));
                }
                if (!string.IsNullOrWhiteSpace(this.ginkouDto2.ginkouMei))
                {
                    listGinkouInfo.AddRange(this.CreateSplitGinkouInfo(this.ginkouDto2));
                }
                if (!string.IsNullOrWhiteSpace(this.ginkouDto3.ginkouMei))
                {
                    listGinkouInfo.AddRange(this.CreateSplitGinkouInfo(this.ginkouDto3));
                }
            }
            //Blankに設定
            for (int i = 1; i <= MaxLineGinkouInfo; i++)
            {
                this.SetFieldName(string.Format("FH_FURIKOMI_GINKO{0}_CTL", i), string.Empty);
            }
            //新しい銀行情報を設定する
            for (int i = 0; i < listGinkouInfo.Count; i++)
            {
                //37個の全角文字 (74個の半角文字)
                this.SetFieldName(string.Format("FH_FURIKOMI_GINKO{0}_CTL", i + 1), listGinkouInfo[i].SubStringByByte(74));
            }
        }

        /// <summary>
        /// 1行目: 銀行名　＋　▲(space 2 byte)　＋　銀行支店名
        /// 2行目:▲(space 2 byte)　＋　口座種類　＋　▲(space 2 byte)　＋　口座番号　＋　▲(space 2 byte)　＋　口座名
        /// </summary>
        /// <param name="ginkouMei"></param>
        /// <param name="shitenMei"></param>
        /// <param name="kouzaShurui"></param>
        /// <param name="kouzaBangou"></param>
        /// <param name="kouzaMeigi"></param>
        private string[] CreateSplitGinkouInfo(GinkouDtoClass ginkouDto)
        {
            string[] ret = new string[2];
            ginkouDto.kouzaShurui = ginkouDto.kouzaShurui == "その他" ? ginkouDto.kouzaShurui : ginkouDto.kouzaShurui.SubStringByByte(4);
            string line1 = string.Format("{0}　{1}", ginkouDto.ginkouMei, ginkouDto.shitenMei);
            ret[0] = line1;

            string line2 = string.Format("　{0}　{1}　{2}", ginkouDto.kouzaShurui, ginkouDto.kouzaBangou, ginkouDto.kouzaMeigi);
            ret[1] = line2;
            return ret;
        }

        /// <summary>
        /// 銀行名　＋　▲(全角ｽﾍﾟｰｽ)　＋　銀行支店名　＋　▲(全角ｽﾍﾟｰｽ)　＋　口座種類　
        /// ＋　▲(全角ｽﾍﾟｰｽ)　＋　口座番号　＋　▲(全角ｽﾍﾟｰｽ)　＋　口座名
        /// </summary>
        /// <param name="ginkouMei"></param>
        /// <param name="shitenMei"></param>
        /// <param name="kouzaShurui"></param>
        /// <param name="kouzaBangou"></param>
        /// <param name="kouzaMeigi"></param>
        /// <returns></returns>
        private string CreateGinkouInfo(GinkouDtoClass ginkouDto)
        {
            ginkouDto.kouzaShurui = ginkouDto.kouzaShurui == "その他" ? ginkouDto.kouzaShurui : ginkouDto.kouzaShurui.SubStringByByte(4);
            return string.Format("{0}　{1}　{2}　{3}　{4}", ginkouDto.ginkouMei, ginkouDto.shitenMei, ginkouDto.kouzaShurui, ginkouDto.kouzaBangou, ginkouDto.kouzaMeigi);
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
        /// <returns>指定フォーマットに変換後文字列</returns>
        private string SetFormat(string value, string format)
        {
            // フォーマット変換後文字列
            string ret = string.Empty;

            // 引数がブランクの場合はブランクを返す
            if (value.Trim() != string.Empty)
            {
                //※↓↓↓引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↓↓↓
                // ret = value;
                //※↑↑↑引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↑↑↑

                // 数値変換時の一時変数
                decimal temp = 0;

                // 引数が括弧付(内税)の場合は括弧付で返す
                if (Const.ConstCls.KAKKO_START == value.Trim().Substring(0, 1))
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
                        ret = Const.ConstCls.KAKKO_START + ret + Const.ConstCls.KAKKO_END;
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
        /// 請求伝票ヘッダデータ設定
        /// </summary>
        /// <param name="dto">請求書発行用DTO</param>
        /// <param name="headerRow">請求伝票データ</param>
        private void SetReportHearder(SeikyuuDenpyouDto dto, DataRow headerRow)
        {
            
            // 発行日
            if (dto.SeikyuHakkou.Equals(ConstCls.SEIKYU_HAKKOU_PRINT_SURU))
            {
                this.printDate = dto.HakkoBi;
            }
            else
            {
                this.printDate = string.Empty;
            }
            //取引先CD
            this.torihikisakiCD = dto.TorihikisakiCd;
            //請求先1
            this.seikyuuSaki1H = headerRow["SEIKYUU_SOUFU_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SEIKYUU_SOUFU_KEISHOU1"].ToString();
            //請求先2
            this.seikyuuSaki2H = headerRow["SEIKYUU_SOUFU_NAME2"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SEIKYUU_SOUFU_KEISHOU2"].ToString();
            //請求番号
            this.seikyuuNumber = headerRow["SEIKYUU_NUMBER"].ToString();

            // Detail部-Header部
            // 請求先郵便番号
            this.seikyuuSakiPostCD = ConstCls.YUBIN + headerRow["SEIKYUU_SOUFU_POST"].ToString();
            this.seikyuuSakiAddress1 = headerRow["SEIKYUU_SOUFU_ADDRESS1"].ToString();    // 請求先住所1
            this.seikyuuSakiAddress2 = headerRow["SEIKYUU_SOUFU_ADDRESS2"].ToString();         // 請求先住所2
            this.seikyuuSaki1DH = headerRow["SEIKYUU_SOUFU_NAME1"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SEIKYUU_SOUFU_KEISHOU1"].ToString();        // 請求先1
            this.seikyuuSaki2DH = headerRow["SEIKYUU_SOUFU_NAME2"].ToString() + ConstCls.ZENKAKU_SPACE + headerRow["SEIKYUU_SOUFU_KEISHOU2"].ToString();         // 請求先2
            if (!string.IsNullOrEmpty(headerRow["SEIKYUU_SOUFU_BUSHO"].ToString()))
            {
                this.seikyuuSakiBusho = headerRow["SEIKYUU_SOUFU_BUSHO"].ToString();       // 請求先部署
                if (!string.IsNullOrEmpty(headerRow["SEIKYUU_SOUFU_TANTOU"].ToString()))
                {
                        this.seikyuuSakiTantousha = headerRow["SEIKYUU_SOUFU_TANTOU"].ToString() + ConstCls.ZENKAKU_SPACE + "様";// 請求先担当者
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(headerRow["SEIKYUU_SOUFU_TANTOU"].ToString()))
                {
                        this.seikyuuSakiBusho = headerRow["SEIKYUU_SOUFU_TANTOU"].ToString() + ConstCls.ZENKAKU_SPACE + "様";// 請求先担当者
                }
            }

            //代表者を印字区分
            this.daihyouPrintKbn = headerRow["DAIHYOU_PRINT_KBN"].ToString();
            //拠点名を印字区分
            this.kyotenNamePrintKbn = headerRow["KYOTEN_NAME_PRINT_KBN"].ToString();

            //会社名
            this.corpName = headerRow["CORP_NAME"].ToString();
            //請求書拠点　
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
            //請求担当者
            if (!string.IsNullOrEmpty(headerRow["SEIKYUU_TANTOU"].ToString()))
            {
                this.seikyuuTantou = "請求担当者" + ConstCls.ZENKAKU_SPACE + headerRow["SEIKYUU_TANTOU"].ToString();
            }

            //自社名 ～ 請求担当者 
            //10からリストの最後までのインデックス
            this.listCorpInfo = new List<string>();

            /* 自社名1・2、代表者名の印字スペースにおいて、印字される項目で下につめていく */
            //自社情報と拠点情報の表示条件に合わせて、空白を埋めるようにセット処理を変更
            // 1.自社情報入力ー会社名
            listCorpInfo.Add(corpName);
            //パターン1
            //＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 1:印字する＞
            //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する＞
            if (ConstCls.PRINT_KBN_1.Equals(headerRow["DAIHYOU_PRINT_KBN"].ToString()) &&
                ConstCls.PRINT_KBN_1.Equals(headerRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                // 2.取引先入力ー請求書拠点　
                listCorpInfo.Add(kyotenName);
                // 3.拠点入力ー拠点代表者
                listCorpInfo.Add(kyotenDaihyou);
            }
            //パターン2
            //＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 1:印字する＞
            //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 2:印字しない＞
            else if (ConstCls.PRINT_KBN_1.Equals(headerRow["DAIHYOU_PRINT_KBN"].ToString()) &&
                ConstCls.PRINT_KBN_2.Equals(headerRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                // 2.自社情報入力ー代表者
                listCorpInfo.Add(corpDaihyou);
            }
            //パターン3
            //＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 2:印字しない＞
            //＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 1:印字する＞
            else if (ConstCls.PRINT_KBN_2.Equals(headerRow["DAIHYOU_PRINT_KBN"].ToString()) &&
               ConstCls.PRINT_KBN_1.Equals(headerRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            {
                // 2.取引先入力ー請求書拠点
                listCorpInfo.Add(kyotenName);
            }
            ////パターン4
            ////＜T_SEIKYUU_DENPYOU_KAGAMI.DAIHYOU_PRINT_KBN = 2:印字しない＞
            ////＜T_SEIKYUU_DENPYOU_KAGAMI.KYOTEN_NAME_PRINT_KBN = 2:印字ない＞
            //else if (ConstCls.PRINT_KBN_2.Equals(headerRow["DAIHYOU_PRINT_KBN"].ToString()) &&
            //   ConstCls.PRINT_KBN_2.Equals(headerRow["KYOTEN_NAME_PRINT_KBN"].ToString()))
            //{
            //}
            // 4.BLANK
            listCorpInfo.Add(string.Empty);
            // 5.取引先‐請求書拠点に紐づく　拠点入力－郵便番号
            listCorpInfo.Add(kyotenPost);
            // 6.取引先‐請求書拠点に紐づく　拠点入力－住所１
            listCorpInfo.Add(kyotenAddress1);
            // 7.取引先‐請求書拠点に紐づく　拠点入力－住所２
            listCorpInfo.Add(kyotenAddress2);
            // 8.取引先‐請求書拠点に紐づく　拠点入力－　電話番号
            listCorpInfo.Add(kyotenTel);
            // 9.取引先‐請求書拠点に紐づく　拠点入力－　FAX番号
            listCorpInfo.Add(kyotenFax);
            // 10.取引先入力-請求担当者
            listCorpInfo.Add(seikyuuTantou);

            // Detail部-Header部
            //請求年月日を設定
            this.seikyuuDate = string.Empty;

            if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SIMEBI)
            {
                this.seikyuuDate = ((DateTime)headerRow["SEIKYUU_DATE"]).ToShortDateString();
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_HAKKOBI)
            {
                this.seikyuuDate = DateUtility.GetCurrentDateTime().ToShortDateString();
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SITEI)
            {
                this.seikyuuDate = dto.SeikyuDate.ToShortDateString();
            }

            // 「請求形態」の条件によって出力内容を分岐
            //   1:請求書データ作成時 → T_SEIKYUU_DENPYOU.SEIKYUU_KEITAI_KBN を利用して発行
            //   2:単月請求           → 単月請求として発行
            //   3:繰越請求           → 繰越請求として発行
            if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_DATA_SAKUSEIJI)
            {
                if (headerRow["SEIKYUU_KEITAI_KBN"].ToString() == ConstCls.SEIKYUU_KEITAI_KBN_1)
                {
                    SetTangetsuReportHeader(headerRow);
                }
                else
                {
                    SetKurikoshiReportHeader(headerRow);
                }
            }
            else if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_TANGETU_SEIKYU)
            {
                SetTangetsuReportHeader(headerRow);
            }
            else
            {
                SetKurikoshiReportHeader(headerRow);
            }

            this.ginkouDto1 = new GinkouDtoClass();
            this.ginkouDto1.ginkouMei = headerRow["FURIKOMI_BANK_NAME"].ToString();              // 振込銀行1-銀行名
            this.ginkouDto1.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME"].ToString();       // 振込銀行1-支店名
            this.ginkouDto1.kouzaShurui = headerRow["KOUZA_SHURUI"].ToString();                  // 振込銀行1-口座種類
            this.ginkouDto1.kouzaBangou = headerRow["KOUZA_NO"].ToString();                      // 振込銀行1-口座番号
            this.ginkouDto1.kouzaMeigi = headerRow["KOUZA_NAME"].ToString();                     // 振込銀行1-口座名義

            this.ginkouDto2 = new GinkouDtoClass();
            this.ginkouDto2.ginkouMei = headerRow["FURIKOMI_BANK_NAME_2"].ToString();             // 振込銀行2-銀行名
            this.ginkouDto2.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME_2"].ToString();      // 振込銀行2-支店名
            this.ginkouDto2.kouzaShurui = headerRow["KOUZA_SHURUI_2"].ToString();                 // 振込銀行2-口座種類
            this.ginkouDto2.kouzaBangou = headerRow["KOUZA_NO_2"].ToString();                     // 振込銀行2-口座番号
            this.ginkouDto2.kouzaMeigi = headerRow["KOUZA_NAME_2"].ToString();                    // 振込銀行2-口座名義

            this.ginkouDto3 = new GinkouDtoClass();
            this.ginkouDto3.ginkouMei = headerRow["FURIKOMI_BANK_NAME_3"].ToString();             // 振込銀行3-銀行名
            this.ginkouDto3.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME_3"].ToString();      // 振込銀行3-支店名
            this.ginkouDto3.kouzaShurui = headerRow["KOUZA_SHURUI_3"].ToString();                 // 振込銀行3-口座種類
            this.ginkouDto3.kouzaBangou = headerRow["KOUZA_NO_3"].ToString();                     // 振込銀行3-口座番号
            this.ginkouDto3.kouzaMeigi = headerRow["KOUZA_NAME_3"].ToString();                    // 振込銀行3-口座名義

            //備考1
            this.bikou1 = headerRow["BIKOU_1"].ToString();
            //備考2
            this.bikou2 = headerRow["BIKOU_2"].ToString();

            //登録番号
            this.tourokuNo = headerRow["TOUROKU_NO"].ToString();
        }

        /// <summary>
        /// 請求伝票ヘッダデータ設定
        /// </summary>
        /// <param name="dto">請求書発行用DTO</param>
        /// <param name="headerRow">請求伝票データ</param>
        private void SetReportHearderEmpty(SeikyuuDenpyouDto dto, DataRow headerRow)
        {
            // 発行日
            if (dto.SeikyuHakkou.Equals(ConstCls.SEIKYU_HAKKOU_PRINT_SURU))
            {
                this.printDate = dto.HakkoBi;
            }
            else
            {
                this.printDate = string.Empty;
            }
            //取引先CD
            this.torihikisakiCD = dto.TorihikisakiCd;
            //請求番号
            this.seikyuuNumber = headerRow["SEIKYUU_NUMBER"].ToString();

            //代表者を印字区分
            this.daihyouPrintKbn = headerRow["DAIHYOU_PRINT_KBN"].ToString();
            //拠点名を印字区分
            this.kyotenNamePrintKbn = headerRow["KYOTEN_NAME_PRINT_KBN"].ToString();

            // Detail部-Header部
            //請求年月日を設定
            this.seikyuuDate = string.Empty;

            if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SIMEBI)
            {
                this.seikyuuDate = ((DateTime)headerRow["SEIKYUU_DATE"]).ToShortDateString();
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_HAKKOBI)
            {
                this.seikyuuDate = DateUtility.GetCurrentDateTime().ToShortDateString();
            }
            else if (dto.SeikyushoPrintDay == ConstCls.SEIKYU_PRINT_DAY_SITEI)
            {
                this.seikyuuDate = dto.SeikyuDate.ToShortDateString();
            }
            if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_DATA_SAKUSEIJI)
            {
                if (headerRow["SEIKYUU_KEITAI_KBN"].ToString() == ConstCls.SEIKYUU_KEITAI_KBN_1)
                {
                    SetTangetsuReportHeader(headerRow);
                }
                else
                {
                    SetKurikoshiReportHeader(headerRow);
                }
            }
            else if (dto.SeikyuStyle == ConstCls.SEIKYU_KEITAI_TANGETU_SEIKYU)
            {
                SetTangetsuReportHeader(headerRow);
            }
            else
            {
                SetKurikoshiReportHeader(headerRow);
            }

            this.ginkouDto1 = new GinkouDtoClass();
            this.ginkouDto1.ginkouMei = headerRow["FURIKOMI_BANK_NAME"].ToString();              // 振込銀行1-銀行名
            this.ginkouDto1.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME"].ToString();       // 振込銀行1-支店名
            this.ginkouDto1.kouzaShurui = headerRow["KOUZA_SHURUI"].ToString();                  // 振込銀行1-口座種類
            this.ginkouDto1.kouzaBangou = headerRow["KOUZA_NO"].ToString();                      // 振込銀行1-口座番号
            this.ginkouDto1.kouzaMeigi = headerRow["KOUZA_NAME"].ToString();                     // 振込銀行1-口座名義

            this.ginkouDto2 = new GinkouDtoClass();
            this.ginkouDto2.ginkouMei = headerRow["FURIKOMI_BANK_NAME_2"].ToString();             // 振込銀行2-銀行名
            this.ginkouDto2.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME_2"].ToString();      // 振込銀行2-支店名
            this.ginkouDto2.kouzaShurui = headerRow["KOUZA_SHURUI_2"].ToString();                 // 振込銀行2-口座種類
            this.ginkouDto2.kouzaBangou = headerRow["KOUZA_NO_2"].ToString();                     // 振込銀行2-口座番号
            this.ginkouDto2.kouzaMeigi = headerRow["KOUZA_NAME_2"].ToString();                    // 振込銀行2-口座名義

            this.ginkouDto3 = new GinkouDtoClass();
            this.ginkouDto3.ginkouMei = headerRow["FURIKOMI_BANK_NAME_3"].ToString();             // 振込銀行3-銀行名
            this.ginkouDto3.shitenMei = headerRow["FURIKOMI_BANK_SHITEN_NAME_3"].ToString();      // 振込銀行3-支店名
            this.ginkouDto3.kouzaShurui = headerRow["KOUZA_SHURUI_3"].ToString();                 // 振込銀行3-口座種類
            this.ginkouDto3.kouzaBangou = headerRow["KOUZA_NO_3"].ToString();                     // 振込銀行3-口座番号
            this.ginkouDto3.kouzaMeigi = headerRow["KOUZA_NAME_3"].ToString();                    // 振込銀行3-口座名義

            //備考1
            this.bikou1 = headerRow["BIKOU_1"].ToString();
            //備考2
            this.bikou2 = headerRow["BIKOU_2"].ToString();

            //登録番号
            this.tourokuNo = headerRow["TOUROKU_NO"].ToString();
        }

        /// <summary>
        /// 請求形態が単月請求の明細ヘッダデータを作成します。
        /// </summary>
        /// <param name="headerRow">請求データ先頭行</param>
        /// <returns>明細ヘッダデータ</returns>
        private void SetTangetsuReportHeader(DataRow headerRow)
        {

            var konkaiGoseikyuGaku = String.Empty;
            var torihikisakiCd = headerRow["TSD_TORIHIKISAKI_CD"].ToString();
            var zenkaiGoseikyuGaku = String.Empty;
            var nyukinGaku = String.Empty;
            var sosaiHoka = String.Empty;
            var kurikoshiGaku = String.Empty;
            var konkaiUriageGaku = headerRow["TSDK_KONKAI_URIAGE_GAKU"].ToString();
            var shouhizeiGaku = headerRow["SYOUHIZEIGAKU"].ToString();
            var goukeiGoseikyuGaku = KingakuAdd(konkaiUriageGaku, shouhizeiGaku);

            // 単月請求の今回御請求額は、合計御請求額と同じ額となる
            konkaiGoseikyuGaku = goukeiGoseikyuGaku;

            // 今回御請求額
            this.konkaiSeikyuu = SetComma(konkaiGoseikyuGaku);

            // コード
            this.code = torihikisakiCd;

            // 前回御請求額
            this.zenkaiSeikyuu = string.Empty;

            // 入金額
            this.nyuukinGaku = string.Empty;

            // 相殺他
            this.sousaiHoka = string.Empty;

            // 差引繰越額
            this.sashihikiKurikosi = string.Empty;

            // 今回売上額
            this.konkaiUriage = SetComma(konkaiUriageGaku);

            // 消費税額
            this.shouhizei = SetComma(shouhizeiGaku);

            // 合計御請求額
            this.goukeiSeikyuu = SetComma(goukeiGoseikyuGaku);

        }

        /// <summary>
        /// 請求形態が繰越請求の明細ヘッダデータを作成します。
        /// </summary>
        /// <param name="headerRow">請求データ先頭行</param>
        /// <returns>明細ヘッダデータ</returns>
        private void SetKurikoshiReportHeader(DataRow headerRow)
        {

            var konkaiGoseikyuGaku = String.Empty;
            var torihikisakiCd = headerRow["TSD_TORIHIKISAKI_CD"].ToString();
            var zenkaiGoseikyuGaku = headerRow["ZENKAI_KURIKOSI_GAKU"].ToString();
            var nyukinGaku = headerRow["KONKAI_NYUUKIN_GAKU"].ToString();
            var sosaiHoka = headerRow["KONKAI_CHOUSEI_GAKU"].ToString();
            var kurikoshiGaku = KingakuSubtract(KingakuSubtract(zenkaiGoseikyuGaku, nyukinGaku), sosaiHoka);
            var konkaiUriageGaku = headerRow["TSDK_KONKAI_URIAGE_GAKU"].ToString();
            var shouhizeiGaku = headerRow["SYOUHIZEIGAKU"].ToString();
            var goukeiGoseikyuGaku = KingakuAdd(konkaiUriageGaku, shouhizeiGaku);

            // 繰越請求の今回御請求額は、差引繰越額と合計御請求額を足した額となる
            konkaiGoseikyuGaku = KingakuAdd(kurikoshiGaku, goukeiGoseikyuGaku);

            var shoshikiKbn = headerRow["SHOSHIKI_KBN"].ToString();
            if (ConstCls.SHOSHIKI_KBN_1 != shoshikiKbn
                 && (false == String.IsNullOrEmpty(headerRow["TSDE_GYOUSHA_CD"].ToString()) || false == String.IsNullOrEmpty(headerRow["TSDE_GENBA_CD"].ToString()))
                )
            {
                zenkaiGoseikyuGaku = "0";
                nyukinGaku = "0";
                sosaiHoka = "0";
                kurikoshiGaku = "0";
                konkaiGoseikyuGaku = goukeiGoseikyuGaku;
            }

            // 今回御請求額
            this.konkaiSeikyuu = SetComma(konkaiGoseikyuGaku);

            // コード
            this.code = torihikisakiCd;

            // 前回請求額
            this.zenkaiSeikyuu = SetComma(zenkaiGoseikyuGaku);

            // 入金額
            this.nyuukinGaku = SetComma(nyukinGaku);

            // 相殺他
            this.sousaiHoka = SetComma(sosaiHoka);

            // 差引繰越額
            // NO30(前回御請求額) - NO32(入金額) - NO34(相殺他)
            this.sashihikiKurikosi = SetComma(kurikoshiGaku);

            // 今回売上額
            this.konkaiUriage = SetComma(konkaiUriageGaku);

            // 消費税額
            this.shouhizei = SetComma(shouhizeiGaku);

            // 合計御請求額
            this.goukeiSeikyuu = SetComma(goukeiGoseikyuGaku);

        }

        /// <summary>
        /// C1Reportの帳票データの明細の作成を実行する
        /// </summary>
        /// <param name="denpyouData"></param>
        /// <param name="nyuukinData"></param>
        private void CreateDetailReportData(DataTable denpyouData, DataTable nyuukinData)
        {
            string formatKingaku = "#,0";          // 金額フォーマット （0は0を返す）

            this.ChouhyouDataTable = new DataTable();

            // 明細データTABLEカラムセット

            this.ChouhyouDataTable.Columns.Add("PHY_GROUP_NAME_FLB");                     // 月日
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_DATE_FLB");                     // 月日
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_NUMBER_FLB");                   // 支払No
            this.ChouhyouDataTable.Columns.Add("PHY_SHARYOU_NAME_FLB");                      // 車輛
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
            foreach (DataRow row in nyuukinData.Rows)
            {
                DataRow ChouhyouRow = this.ChouhyouDataTable.NewRow();
                ChouhyouRow["PHY_GROUP_NAME_FLB"] = row["GROUP_NAME"];
                ChouhyouRow["PHY_DENPYOU_DATE_FLB"] = row["DENPYOU_DATE"];
                ChouhyouRow["PHY_DENPYOU_NUMBER_FLB"] = row["DENPYOU_NUMBER"];
                ChouhyouRow["PHY_SHARYOU_NAME_FLB"] = row["SHARYOU_NAME"];
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
                ChouhyouRow["PHY_SHARYOU_NAME_FLB"] = row["SHARYOU_NAME"];
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
        /// <param name="nyuukinData"></param>
        public void CreateReportData(SeikyuuDenpyouDto dto, DataRow headerRow, DataTable denpyouData, DataTable nyuukinData)
        {
            bool syaryouOutputFlg = false;

            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.NYUKIN_MEISAI_NASHI && string.IsNullOrEmpty(headerRow["KAGAMI_NUMBER"].ToString()))
            {
                SetReportHearderEmpty(dto, headerRow);
            }
            else
            {
                SetReportHearder(dto, headerRow);
            }
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            CreateDetailReportData(denpyouData, nyuukinData);

            /* LAYOUT1 & LAYOUT2(SubReport) */
            Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
            dic.Add(OutputFormLayout, this.ChouhyouDataTable);

            // 明細データTABLEをセット
            this.SetRecord(this.ChouhyouDataTable);

            if (Convert.ToString(dto.MSysInfo.SHARYOU_NAME_INGI) == "1")
            {
                syaryouOutputFlg = true;
            }

            if (syaryouOutputFlg)
            {
                // 車輛名あり
                // データテーブル情報から帳票情報作成処理を実行する
                this.Create(this.OutputFormSyaryouFullPathName, this.OutputFormLayout, dic);
            }
            else
            {
                // 車輛名なし
                // データテーブル情報から帳票情報作成処理を実行する
                this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dic);
            }
        }

        /// <summary>
        /// CSVデータの作成を実行する
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="headerRow"></param>
        /// <param name="denpyouData"></param>
        /// <param name="nyuukinData"></param>
        /// <returns></returns>
        internal DataTable CreateCsvData(SeikyuuDenpyouDto dto, DataRow headerRow, DataTable denpyouData, DataTable nyuukinData)
        {
            //「明細：なし」を選択 かつ 鑑テーブルにテータがない場合
            if (dto.Meisai == ConstCls.NYUKIN_MEISAI_NASHI && string.IsNullOrEmpty(headerRow["KAGAMI_NUMBER"].ToString()))
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
            rowTmp["head_1"] = seikyuuSakiPostCD.Replace("〒","").Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSakiAddress1.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSakiAddress2.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSaki1DH.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSaki2DH.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSakiBusho.Trim();
            csvDT.Rows.Add(rowTmp);

            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuSakiTantousha.Trim();
            csvDT.Rows.Add(rowTmp);

            // 8行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = "請求年月日";
            rowTmp["head_2"] = "コード";
            rowTmp["head_3"] = "今回御請求額";

            if (String.IsNullOrEmpty(zenkaiSeikyuu))
            {
                rowTmp["head_4"] = "";
                rowTmp["head_5"] = "";
                rowTmp["head_6"] = "";
                rowTmp["head_7"] = "";
            }
            else
            {
                rowTmp["head_4"] = "前回請求額";
                rowTmp["head_5"] = "今回入金額";
                rowTmp["head_6"] = "調整額";
                rowTmp["head_7"] = "繰越額";
            }

            rowTmp["head_8"] = "今回取引額（税抜）";
            rowTmp["head_9"] = "消費税";
            rowTmp["head_10"] = "今回取引額";
            csvDT.Rows.Add(rowTmp);

            // 9行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = seikyuuDate;
            rowTmp["head_2"] = code;
            rowTmp["head_3"] = konkaiSeikyuu;
            rowTmp["head_4"] = zenkaiSeikyuu;
            rowTmp["head_5"] = nyuukinGaku;
            rowTmp["head_6"] = sousaiHoka;
            rowTmp["head_7"] = sashihikiKurikosi;
            rowTmp["head_8"] = konkaiUriage;
            rowTmp["head_9"] = shouhizei;
            rowTmp["head_10"] = goukeiSeikyuu;
            csvDT.Rows.Add(rowTmp);

            // 10行目
            rowTmp = csvDT.NewRow();
            rowTmp["head_1"] = "業者名";
            rowTmp["head_2"] = "現場名";
            rowTmp["head_3"] = "月日";
            rowTmp["head_4"] = "売上No";
            rowTmp["head_5"] = "品名";
            rowTmp["head_6"] = "数量";
            rowTmp["head_7"] = "単位";
            rowTmp["head_8"] = "単価";
            rowTmp["head_9"] = "金額";
            rowTmp["head_10"] = "消費税";
            rowTmp["head_11"] = "備考";
            csvDT.Rows.Add(rowTmp);

            // 入金明細追加
           foreach (DataRow row in nyuukinData.Rows)
           {
                if (row["HINMEI_NAME"].Equals("入金計"))
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
                if (row["HINMEI_NAME"].Equals("【伝票毎消費税】") 
                    || row["HINMEI_NAME"].Equals("【請求毎消費税(内)】") 
                    || row["HINMEI_NAME"].Equals("【請求毎消費税】")
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
