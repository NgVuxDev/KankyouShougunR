using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using System.Data;

namespace Report
{
    public class ReportInfoR490 : ReportInfoBase
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();

        // ヘッダー部分の列定義（1-1）
        // バーコード
        private string barcode = string.Empty;
        // マニフェスト番号
        private string manifestno = string.Empty;
        // 登録の状態
        private string tourokujoutai = string.Empty;
        // 引渡し日
        private string hikiwatasiday = string.Empty;
        // 引渡し担当者
        private string hikiwatasitantousha = string.Empty;
        // 連絡番号１
        private string renrakutel1 = string.Empty;
        // 連絡番号２
        private string renrakutel2 = string.Empty;
        // 連絡番号３
        private string renrakutel3 = string.Empty;
        // ヘッダー部分の列定義（1-2）
        // 排出事業者名
        private string haishutujigyoshaname = string.Empty;
        // 排出事業者郵便番号
        private string haishutujigyoshapost = string.Empty;
        // 排出事業者住所
        private string haishutujigyoushaadress = string.Empty;
        // 排出事業者電話番号
        private string haishutujigyoshatel = string.Empty;
        // 排出事業者加入者番号
        private string haishutujigyoshakanyuno = string.Empty;
        // 排出事業場名称
        private string haishutujigyoujonameisho = string.Empty;
        // 排出事業場郵便番号
        private string haishutujigyoujopost = string.Empty;
        // 排出事業場所在地
        private string haishutujigyoujoaddress = string.Empty;
        // 排出事業場電話番号
        private string haishutujigyoujotel = string.Empty;

        // ヘッダー部分の列定義（1-3）※課題のため変更の可能性あり

        private string sanpaihaikibutsukindcd = string.Empty;                   // 産業廃棄物種類CD
        private string sanpaihaikibutsukind = string.Empty;                     // 産業廃棄物種類
        private string sanpaihaikibutsudaibunrui = string.Empty;                // 産業廃棄物大分類名称
        private string sanpaihaikibutsuyuugaibushutucd1 = string.Empty;         // 産業廃棄物有害物質名称CD1
        private string sanpaihaikibutsuyuugaibushutuname1 = string.Empty;       // 産業廃棄物有害物質名称1
        private string sanpaihaikibutsuyuugaibushutucd2 = string.Empty;         // 産業廃棄物有害物質名称CD2
        private string sanpaihaikibutsuyuugaibushutuname2 = string.Empty;       // 産業廃棄物有害物質名称2
        private string sanpaihaikibutsuyuugaibushutucd3 = string.Empty;         // 産業廃棄物有害物質名称CD3
        private string sanpaihaikibutsuyuugaibushutuname3 = string.Empty;       // 産業廃棄物有害物質名称3
        private string sanpaihaikibutsuyuugaibushutukensuu = string.Empty;      // 産業廃棄物有害物質名称他件数
        private string sanpaihaikibutsunoname = string.Empty;                   // 廃棄物の名称
        private string sanpaisuuryou = string.Empty;                            // 数量
        private string sanpaisuuryoutani = string.Empty;                        // 数量単位
        private string sanpainisugatasuuryou = string.Empty;                    // 荷姿
        private string sanpainisugatasuuryoutani = string.Empty;                // 荷姿単位
        private string sanpaikakuteisuuryou = string.Empty;                     // 確定数量
        private string sanpaikakuteisuuryoutani = string.Empty;                 // 確定数量単位
        private string sanpaisuuryoukakuteisha = string.Empty;                  // 数量の確定者
        // ヘッダー部分の列定義（1-4）
        private string chuukansanpaihaikijidounyuryokukbn1 = string.Empty;      // 中間処理産業廃棄物・電子/紙
        private string chuukansanpaihaikijidounyuryokukbn2 = string.Empty;      // 中間処理産業廃棄物・マニフェスト番号/交付番号
        // ヘッダー部分の列定義（1-9）
        private string chuukansanpaihaikijidounyuryokukbn3 = string.Empty;      // 中間処理産業廃棄物・電子/紙（２行目）
        private string chuukansanpaihaikijidounyuryokukbn4 = string.Empty;      // 中間処理産業廃棄物・マニフェスト番号/交付番号（２行目）
        // ヘッダー部分の列定義（1-5）
        private string saishuushobunbashoyoteiaddress = string.Empty;           // 最終処分場所（予定）所在地・名称
        // ヘッダー部分の列定義（1-6）
        private string shuushuuupngyoushakukan1 = string.Empty;                 // 収集運搬業者区間１～３可変項目
        private string shuushuuupngyoushakukan1name = string.Empty;             // 収集運搬業者区間１名称
        private string shuushuuupngyoushakukan1post = string.Empty;             // 収集運搬業者区間１郵便番号
        private string shuushuuupngyoushakukan1address = string.Empty;          // 収集運搬業者区間１住所
        private string shuushuuupngyoushakukan1tel = string.Empty;              // 収集運搬業者区間１電話番号
        private string shuushuuupngyoushakukan1kanyuno = string.Empty;          // 収集運搬業者区間１加入者番号
        private string shuushuuupngyoushakukan1kyokano = string.Empty;          // 収集運搬業者区間１許可番号
        private string shuushuuupngyoushakukan1bikou = string.Empty;            // 収集運搬業者区間１備考
        private string upnsakijigyojoname = string.Empty;                       // 運搬先の事業場名称
        private string upnsakijigyojopost = string.Empty;                       // 運搬先の事業場所郵便番号
        private string upnsakijigyojoaddress = string.Empty;                    // 運搬先の事業場所在地
        private string upnsakijigyojotel = string.Empty;                        // 運搬先の事業場電話番号
        private string upnsakijigyojohouhou = string.Empty;                     // 運搬方法
        private string upnsakijigyojosharyono = string.Empty;                   // 車両番号（排出）
        private string upnsakijigyojoupnryo = string.Empty;                     // 運搬量
        private string upnsakijigyojoupnryotani = string.Empty;                 // 運搬量単位
        private string upnsakijigyojoyuukabutsuryo = string.Empty;              // 有価物拾集量
        private string upnsakijigyojoyuukabutsuryotani = string.Empty;          // 運搬量単位
        private string upnsakijigyojoupnryoupntantosha = string.Empty;          // 運搬担当者
        private string upnsakijigyojoupnryoupndate = string.Empty;              // 運搬終了日
        // ヘッダー部分の列定義（1-7）
        private string shobungyoshaname = string.Empty;                         // 処分業者名称
        private string shobungyoshapost = string.Empty;                         // 処分業者郵便番号
        private string shobungyoshaaddress = string.Empty;                      // 処分業者住所
        private string shobungyoshatel = string.Empty;                          // 処分業者電話番号
        private string shobungyoshakanyuno = string.Empty;                      // 処分業者加入番号
        private string shobungyoshakyokano = string.Empty;                      // 処分業者許可番号

        private string shobungyoshabikou = string.Empty;                        // 処分業者備考
        private string shobunjigyojoname = string.Empty;                        // 処分事業場名称
        private string shobunjigyojopost = string.Empty;                        // 処分事業場郵便番号
        private string shobunjigyojoaddress = string.Empty;                     // 処分事業場所在地
        private string shobunjigyojotel = string.Empty;                         // 処分事業場電話番号
        private string shobunjigyojohouhou = string.Empty;                      // 処分方法

        private string shobunjigyojohoukokukbn = string.Empty;                  // 報告区分
        private string shobunjigyojoshuryoubi = string.Empty;                   // 処分終了日
        private string shobunjigyojohaikijuryobi = string.Empty;                // 廃棄物受領日
        private string shobunjigyojoshobuntantou = string.Empty;                // 処分担当者
        private string shobunjigyojojunyuryo = string.Empty;                    // 受入量
        private string shobunjigyojojunyuryotani = string.Empty;                // 受入量単位
        // ヘッダー部分の列定義（1-8） 
        private string saishuushobunbashoresultsaddress = string.Empty;         // 最終処分の場所（実績）所在地
        private string saishuushobunbashoresultsshuryobi = string.Empty;        // 最終処分終了日
        private string bikou1 = string.Empty;                                   // 備考１
        private string bikou2 = string.Empty;                                   // 備考２
        private string bikou3 = string.Empty;                                   // 備考３
        private string bikou4 = string.Empty;                                   // 備考４
        private string bikou5 = string.Empty;                                   // 備考５

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR382"/> class.
        /// </summary>
        public ReportInfoR490()
        {
            // 帳票出力フルパスフォーム名
            //this.OutputFormFullPathName = TEMPLATE_PATH + "R494-Form.xml";
            //this.OutputFormFullPathName = "../../../Template/R494-Form.xml";
            this.OutputFormFullPathName = "Template/R490-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        /// <summary>
        /// 帳票出力フルパスフォームを保持するプロパティ
        /// </summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>
        /// 帳票出力フォームレイアウトを保持するプロパティ
        /// </summary>
        public string OutputFormLayout { get; set; }

        /// <summary>
        /// C1Reportの帳票データの作成ならびに明細部分の列定義を実行する
        /// </summary>
        public void R490_Report(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

            //// データテーブル作成処理のみ
            this.SetRecord(this.chouhyouDataTable);
            /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
            /// 引数１：XMLレポート定義ファイルの完全名
            /// 引数２：string reportName
            /// 引数３：画面から受け継いだdataTable
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dataTableChouhyouForm);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            // ヘッダー部分の列定義（1-1）
            // バーコード
            this.SetFieldName("FH_BARCODE_FLB", this.barcode);
            // マニフェスト番号
            this.SetFieldName("FH_MANIFEST_NO_CTL", this.manifestno);
            // 登録の状態
            this.SetFieldName("FH_TOUROKU_JOUTAI_CTL", this.tourokujoutai);
            // 引渡し日
            this.SetFieldName("FH_HIKIWATASI_DAY_CTL", this.hikiwatasiday);
            // 引渡し担当者
            this.SetFieldName("FH_HIKIWATASI_TANTOUSHA_CTL", this.hikiwatasitantousha);
            // 連絡番号１
            this.SetFieldName("FH_RENRAKU_TEL1_CTL", this.renrakutel1);
            // 連絡番号２
            this.SetFieldName("FH_RENRAKU_TEL2_CTL", this.renrakutel2);
            // 連絡番号３
            this.SetFieldName("FH_RENRAKU_TEL3_CTL", this.renrakutel3);
            // ヘッダー部分の列定義（1-2）
            // 排出事業者名
            this.SetFieldName("FH_HAISHUTU_JIGYOSHANAME_CTL", this.haishutujigyoshaname);
            // 排出事業者郵便番号
            this.SetFieldName("FH_HAISHUTU_JIGYOSHAPOST_CTL", this.haishutujigyoshapost);
            // 排出事業者住所
            this.SetFieldName("FH_HAISHUTU_JIGYOSHAADRESS_CTL", this.haishutujigyoushaadress);
            // 排出事業者電話番号
            this.SetFieldName("FH_HAISHUTU_JIGYOSHATEL_CTL", this.haishutujigyoshatel);
            // 排出事業者加入番号
            this.SetFieldName("FH_HAISHUTU_JIGYOSHAKANYUNO_CTL", this.haishutujigyoshakanyuno);
            // 排出事業場名称
            this.SetFieldName("FH_HAISHUTU_JIGYOJONAME_CTL", this.haishutujigyoujonameisho);
            // 排出事業場郵便番号
            this.SetFieldName("FH_HAISHUTU_JIGYOJOPOST_CTL", this.haishutujigyoujopost);
            // 排出事業場所在地
            this.SetFieldName("FH_HAISHUTU_JIGYOJOADRESS_CTL", this.haishutujigyoujoaddress);
            // 排出事業場電話番号
            this.SetFieldName("FH_HAISHUTU_JIGYOJOTEL_CTL", this.haishutujigyoujotel);
            // ヘッダー部分の列定義（1-3）※課題のため変更の可能性あり
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_KIND_CD_CTL", this.sanpaihaikibutsukindcd);                                             // 産業廃棄物種類CD
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_KIND_CTL", this.sanpaihaikibutsukind);                                                  // 産業廃棄物種類
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_DAIBUNRUI_CTL", this.sanpaihaikibutsudaibunrui);                                        // 産業廃棄物大分類名称
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUCD1_CTL", this.sanpaihaikibutsuyuugaibushutucd1);                          // 産業廃棄物有害物質名称CD1
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUNAME1_CTL", this.sanpaihaikibutsuyuugaibushutuname1);                      // 産業廃棄物有害物質名称1
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUCD2_CTL", this.sanpaihaikibutsuyuugaibushutucd2);                          // 産業廃棄物有害物質名称CD2
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUNAME2_CTL", this.sanpaihaikibutsuyuugaibushutuname2);                      // 産業廃棄物有害物質名称2
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUCD3_CTL", this.sanpaihaikibutsuyuugaibushutucd3);                          // 産業廃棄物有害物質名称CD3
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUNAME3_CTL", this.sanpaihaikibutsuyuugaibushutuname3);                      // 産業廃棄物有害物質名称3
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_YUUGAIBUSHUTUKENSUU_CTL", this.sanpaihaikibutsuyuugaibushutukensuu);                    // 産業廃棄物有害物質名称他件数
            this.SetFieldName("FH_SANPAI_HAIKIBUTSU_NAME_CTL", this.sanpaihaikibutsunoname);                                                // 廃棄物の名称
            this.SetFieldName("FH_SANPAI_SUURYOU_CTL", this.sanpaisuuryou);                                                                 // 数量
            this.SetFieldName("FH_SANPAI_SUURYOUTANI_CTL", this.sanpaisuuryoutani);                                                         // 数量単位
            this.SetFieldName("FH_SANPAI_NISUGATASUURYOU_CTL", this.sanpainisugatasuuryou);                                                 // 荷姿
            this.SetFieldName("FH_SANPAI_NISUGATASUURYOU_TANI_CTL", this.sanpainisugatasuuryoutani);                                        // 荷姿単位
            this.SetFieldName("FH_SANPAI_KAKUTEISUURYOU_CTL", this.sanpaikakuteisuuryou);                                                   // 確定数量
            this.SetFieldName("FH_SANPAI_KAKUTEISUURYOU_TANI_CTL", this.sanpaikakuteisuuryoutani);                                          // 確定数量単位
            this.SetFieldName("FH_SANPAI_SUURYOUKAKUTEISHA_CTL", this.sanpaisuuryoukakuteisha);                                             // 数量の確定者
            // ヘッダー部分の列定義（1-4）
            this.SetFieldName("FH_CHUUKAN_SANPAIHAIKIBUTSU_JIDOUNYURYOKU_KBN1_CTL", this.chuukansanpaihaikijidounyuryokukbn1);              // 中間処理産業廃棄物・電子/紙
            this.SetFieldName("FH_CHUUKAN_SANPAIHAIKIBUTSU_JIDOUNYURYOKU_KBN2_CTL", this.chuukansanpaihaikijidounyuryokukbn2);              // 中間処理産業廃棄物・マニフェスト番号/交付番号
            // ヘッダー部分の列定義（1-9）
            this.SetFieldName("FH_CHUUKAN_SANPAIHAIKIBUTSU_JIDOUNYURYOKU_KBN3_CTL", this.chuukansanpaihaikijidounyuryokukbn3);              // 中間処理産業廃棄物・電子/紙（２行目）
            this.SetFieldName("FH_CHUUKAN_SANPAIHAIKIBUTSU_JIDOUNYURYOKU_KBN4_CTL", this.chuukansanpaihaikijidounyuryokukbn4);              // 中間処理産業廃棄物・マニフェスト番号/交付番号（２行目）
            // ヘッダー部分の列定義（1-5）
            this.SetFieldName("FH_SAISHUUSHOBUNBASHO_YOTEI_ADDRESS_CTL", this.saishuushobunbashoyoteiaddress);                              // 最終処分場所（予定）所在地・名称
            // ヘッダー部分の列定義（1-6）
            // "収集運搬業者" + 1-6．カラム1
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_CTL", "収集運搬業者" + Environment.NewLine + this.shuushuuupngyoushakukan1);    // 収集運搬業者区間１～３可変項目
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_NAME_CTL", this.shuushuuupngyoushakukan1name);                                  // 収集運搬業者区間１名称
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_POST_CTL", this.shuushuuupngyoushakukan1post);                                  // 収集運搬業者区間１郵便番号
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_ADRESS_CTL", this.shuushuuupngyoushakukan1address);                             // 収集運搬業者区間１住所
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_TEL_CTL", this.shuushuuupngyoushakukan1tel);                                    // 収集運搬業者区間１電話番号
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_KANYUNO_CTL", this.shuushuuupngyoushakukan1kanyuno);                            // 収集運搬業者区間１加入者番号
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_KYOKANO_CTL", this.shuushuuupngyoushakukan1kyokano);                            // 収集運搬業者区間１許可番号
            this.SetFieldName("FH_SHUUSHUUUPNGYOUSHA_KUKAN1_BIKOU_CTL", this.shuushuuupngyoushakukan1bikou);                                // 収集運搬業者区間１備考
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_NAME_CTL", this.upnsakijigyojoname);                                                      // 運搬先の事業場名称
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_POST_CTL", this.upnsakijigyojopost);                                                      // 運搬先の事業場所郵便番号
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_ADDRESS_CTL", this.upnsakijigyojoaddress);                                                // 運搬先の事業場所在地
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_TEL_CTL", this.upnsakijigyojotel);                                                        // 運搬先の事業場電話番号
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_HOUHOU_CTL", this.upnsakijigyojohouhou);                                                  // 運搬方法
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_SHARYONO_CTL", this.upnsakijigyojosharyono);                                              // 車両番号（排出）
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_UPNRYO_CTL", this.upnsakijigyojoupnryo);                                                  // 運搬量
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_UPNRYO_TANI_CTL", this.upnsakijigyojoupnryotani);                                         // 運搬量単位
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_YUUKABUTSURYO_CTL", this.upnsakijigyojoyuukabutsuryo);                                    // 有価物拾集量
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_YUUKABUTSURYO_TANI_CTL", this.upnsakijigyojoyuukabutsuryotani);                           // 有価物拾集量単位
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_UPNTANTOSHA_CTL", this.upnsakijigyojoupnryoupntantosha);                                  // 運搬担当者
            this.SetFieldName("FH_UPNSAKI_JIGYOJO_UPNDATE_CTL", this.upnsakijigyojoupnryoupndate);                                          // 運搬終了日
            // ヘッダー部分の列定義（1-7）
            this.SetFieldName("FH_SHOBUN_GYOSHANAME_CTL", this.shobungyoshaname);                                                           // 処分業者名称
            this.SetFieldName("FH_SHOBUN_GYOSHAPOST_CTL", this.shobungyoshapost);                                                           // 処分業者郵便番号
            this.SetFieldName("FH_SHOBUN_GYOSHAADDRESS_CTL", this.shobungyoshaaddress);                                                     // 処分業者住所
            this.SetFieldName("FH_SHOBUN_GYOSHATEL_CTL", this.shobungyoshatel);                                                             // 処分業者電話番号
            this.SetFieldName("FH_SHOBUN_GYOSHA_KANYUNO_CTL", this.shobungyoshakanyuno);                                                    // 処分業者加入番号
            this.SetFieldName("FH_SHOBUN_GYOSHA_KYOKANO_CTL", this.shobungyoshakyokano);                                                    // 処分業者許可番号
            this.SetFieldName("FH_SHOBUN_GYOSHABIKOU_CTL", this.shobungyoshabikou);                                                         // 処分業者備考
            this.SetFieldName("FH_SHOBUN_JIGYOJONAME_CTL", this.shobunjigyojoname);                                                         // 処分事業場名称
            this.SetFieldName("FH_SHOBUN_JIGYOPOST_CTL", this.shobunjigyojopost);                                                           // 処分事業場郵便番号
            this.SetFieldName("FH_SHOBUN_JIGYOADDRESS_CTL", this.shobunjigyojoaddress);                                                     // 処分事業場所在地
            this.SetFieldName("FH_SHOBUN_JIGYOTEL_CTL", this.shobunjigyojotel);                                                             // 処分事業場電話番号
            this.SetFieldName("FH_SHOBUN_JIGYOHOUHOU_CTL", this.shobunjigyojohouhou);                                                       // 処分方法
            this.SetFieldName("FH_SHOBUN_JIGYOJO_HOUKOKU_KBN_CTL", this.shobunjigyojohoukokukbn);                                           // 報告区分
            this.SetFieldName("FH_SHOBUN_JIGYOJO_SHURYOUBI_CTL", this.shobunjigyojoshuryoubi);                                              // 処分終了日
            this.SetFieldName("FH_SHOBUN_JIGYOJO_HAIKIJURYOBI_CTL", this.shobunjigyojohaikijuryobi);                                        // 廃棄物受領日
            this.SetFieldName("FH_SHOBUN_JIGYOJO_SHOBUN_TANTOU_CTL", this.shobunjigyojoshobuntantou);                                       // 処分担当者
            this.SetFieldName("FH_SHOBUN_JIGYOJO_JUNYURYO_CTL", this.shobunjigyojojunyuryo);                                                // 受入量
            this.SetFieldName("FH_SHOBUN_JIGYOJO_JUNYURYO_TANI_CTL", this.shobunjigyojojunyuryotani);                                       // 受入量単位
            // ヘッダー部分の列定義（1-8）
            this.SetFieldName("FH_SAISHUUSHOBUNBASHO_RESULTS_ADDRESS_CTL", this.saishuushobunbashoresultsaddress);                          // 最終処分の場所（実績）所在地・名称
            this.SetFieldName("FH_SAISHUUSHOBUNBASHO_RESULTS_SHURYOBI_CTL", this.saishuushobunbashoresultsshuryobi);                        // 最終処分終了日
            this.SetFieldName("FH_BIKOU1_CTL", this.bikou1);                                                                                // 備考１
            this.SetFieldName("FH_BIKOU2_CTL", this.bikou2);                                                                                // 備考２
            this.SetFieldName("FH_BIKOU3_CTL", this.bikou3);                                                                                // 備考３
            this.SetFieldName("FH_BIKOU4_CTL", this.bikou4);                                                                                // 備考４
            this.SetFieldName("FH_BIKOU5_CTL", this.bikou5);                                                                                // 備考５  
            this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");                                                            // 発行日時
        }

        /// <summary>
        /// 帳票データより、C1Reportに渡すデータを作成する
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        private void InputDataToMem(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();
                //// ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                //    //string[] splt = { "\",\"" };
                string[] list = this.ReportSplit(tmp);

                // ヘッダ部・データ種類(list[0])により分岐   
                if (list[0] == "1-1")
                {
                    // バーコード
                    this.barcode = list[1];
                    // マニフェスト番号
                    this.manifestno = list[1];
                    // 登録の状態
                    this.tourokujoutai = list[2];
                    // 引渡し日
                    this.hikiwatasiday = list[3];
                    // 引渡し担当者
                    this.hikiwatasitantousha = list[4];
                    // 連絡番号１
                    this.renrakutel1 = list[5];
                    // 連絡番号２
                    this.renrakutel2 = list[6];
                    // 連絡番号３
                    this.renrakutel3 = list[7];
                }
                else if (list[0] == "1-2")
                {
                    // 排出事業者名
                    this.haishutujigyoshaname = list[1];
                    // 排出事業者郵便番号
                    this.haishutujigyoshapost = list[2];
                    // 排出事業者住所
                    this.haishutujigyoushaadress = list[3];
                    // 排出事業者電話番号
                    this.haishutujigyoshatel = list[4];
                    // 排出事業者加入番号
                    this.haishutujigyoshakanyuno = list[5];
                    // 排出事業場名称
                    this.haishutujigyoujonameisho = list[6];
                    // 排出事業場郵便番号
                    this.haishutujigyoujopost = list[7];
                    // 排出事業場所在地
                    this.haishutujigyoujoaddress = list[8];
                    // 排出事業場電話番号
                    this.haishutujigyoujotel = list[9];

                    // 明細部
                    //this.detail.Add(new Detail(list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11]));
                }
                else if (list[0] == "1-3")
                {
                    this.sanpaihaikibutsukindcd = list[1];                  // 産業廃棄物種類CD
                    this.sanpaihaikibutsukind = list[2];                    // 産業廃棄物種類
                    this.sanpaihaikibutsudaibunrui = list[3];               // 産業廃棄物大分類名称
                    this.sanpaihaikibutsuyuugaibushutucd1 = list[4];        // 産業廃棄物有害物質名称CD1
                    this.sanpaihaikibutsuyuugaibushutuname1 = list[5];      // 産業廃棄物有害物質名称1
                    this.sanpaihaikibutsuyuugaibushutucd2 = list[6];        // 産業廃棄物有害物質名称CD2
                    this.sanpaihaikibutsuyuugaibushutuname2 = list[7];      // 産業廃棄物有害物質名称2
                    this.sanpaihaikibutsuyuugaibushutucd3 = list[8];        // 産業廃棄物有害物質名称CD3
                    this.sanpaihaikibutsuyuugaibushutuname3 = list[9];      // 産業廃棄物有害物質名称3
                    this.sanpaihaikibutsuyuugaibushutukensuu = list[10];    // 産業廃棄物有害物質名称他件数
                    this.sanpaihaikibutsunoname = list[11];                 // 廃棄物の名称
                    this.sanpaisuuryou = list[12];                          // 数量
                    this.sanpaisuuryoutani = list[13];                      // 数量単位
                    this.sanpainisugatasuuryou = list[14];                  // 荷姿
                    this.sanpainisugatasuuryoutani = list[15];              // 荷姿単位
                    this.sanpaikakuteisuuryou = list[16];                   // 確定数量
                    this.sanpaikakuteisuuryoutani = list[17];               // 確定数量単位
                    this.sanpaisuuryoukakuteisha = list[18];                // 数量の確定者
                }
                else if (list[0] == "1-4")
                {
                    this.chuukansanpaihaikijidounyuryokukbn1 = list[1];     // 中間処理産業廃棄物・電子/紙
                    this.chuukansanpaihaikijidounyuryokukbn2 = list[2];     // 中間処理産業廃棄物・マニフェスト番号/交付番号
                }
                else if (list[0] == "1-9")
                {
                    this.chuukansanpaihaikijidounyuryokukbn3 = list[1];     // 中間処理産業廃棄物・電子/紙（２行目）
                    this.chuukansanpaihaikijidounyuryokukbn4 = list[2];     // 中間処理産業廃棄物・マニフェスト番号/交付番号（２行目）
                }
                else if (list[0] == "1-5")
                {
                    this.saishuushobunbashoyoteiaddress = list[1];          // 最終処分場所（予定）所在地
                }
                else if (list[0] == "1-6")
                {              
                    this.shuushuuupngyoushakukan1 = list[1];                // 収集運搬業者区間１可変項目
                    this.shuushuuupngyoushakukan1name = list[2];            // 収集運搬業者区間１名称
                    this.shuushuuupngyoushakukan1post = list[3];            // 収集運搬業者区間１郵便番号
                    this.shuushuuupngyoushakukan1address = list[4];         // 収集運搬業者区間１住所
                    this.shuushuuupngyoushakukan1tel = list[5];             // 収集運搬業者区間１電話番号
                    this.shuushuuupngyoushakukan1kanyuno = list[6];         // 収集運搬業者区間１加入者番号
                    this.shuushuuupngyoushakukan1kyokano = list[7];         // 収集運搬業者区間１許可番号
                    this.shuushuuupngyoushakukan1bikou = list[8];           // 収集運搬業者区間１備考
                    this.upnsakijigyojoname = list[9];                      // 運搬先の事業場名称
                    this.upnsakijigyojopost = list[10];                     // 運搬先の事業場所郵便番号
                    this.upnsakijigyojoaddress = list[11];                  // 運搬先の事業場所在地
                    this.upnsakijigyojotel = list[12];                      // 運搬先の事業場電話番号
                    this.upnsakijigyojohouhou = list[13];                   // 運搬方法
                    this.upnsakijigyojosharyono = list[14];                 // 車両番号（排出）
                    this.upnsakijigyojoupnryo = list[15];                   // 運搬量
                    this.upnsakijigyojoupnryotani = list[16];               // 運搬量単位
                    this.upnsakijigyojoyuukabutsuryo = list[17];            // 有価物拾集量
                    this.upnsakijigyojoyuukabutsuryotani = list[18];        // 運搬量単位
                    this.upnsakijigyojoupnryoupntantosha = list[19];        // 運搬担当者
                    this.upnsakijigyojoupnryoupndate = list[20];            // 運搬終了日  
                }
                else if (list[0] == "1-7")
                {
                    this.shobungyoshaname = list[1];                        // 処分業者名称
                    this.shobungyoshapost = list[2];                        // 処分業者郵便番号
                    this.shobungyoshaaddress = list[3];                     // 処分業者住所
                    this.shobungyoshatel = list[4];                         // 処分業者電話番号
                    this.shobungyoshakanyuno = list[5];                     // 処分業者加入番号
                    this.shobungyoshakyokano = list[6];                     // 処分業者許可番号
                    this.shobungyoshabikou = list[7];                       // 処分業者備考
                    this.shobunjigyojoname = list[8];                       // 処分事業場名称
                    this.shobunjigyojopost = list[9];                       // 処分事業場郵便番号
                    this.shobunjigyojoaddress = list[10];                   // 処分事業場所在地
                    this.shobunjigyojotel = list[11];                       // 処分事業場電話番号
                    this.shobunjigyojohouhou = list[12];                    // 処分方法

                    this.shobunjigyojohoukokukbn = list[13];                // 報告区分
                    this.shobunjigyojoshuryoubi = list[14];                 // 処分終了日
                    this.shobunjigyojohaikijuryobi = list[15];              // 廃棄物受領日
                    this.shobunjigyojoshobuntantou = list[16];              // 処分担当者
                    this.shobunjigyojojunyuryo = list[17];                  // 受入量
                    this.shobunjigyojojunyuryotani = list[18];              // 受入量単位
                }
                else if (list[0] == "1-8")
                {
                    this.saishuushobunbashoresultsaddress = list[1];        // 最終処分の場所（実績）所在地
                    this.saishuushobunbashoresultsshuryobi = list[2];       // 最終処分終了日
                    this.bikou1 = list[3];                                  // 備考１
                    this.bikou2 = list[4];                                  // 備考２
                    this.bikou3 = list[5];                                  // 備考３
                    this.bikou4 = list[6];                                  // 備考４
                    this.bikou5 = list[7];                                  // 備考５
                }
            }
        }
        /// <summary> 文字列の帳票データを文字列配列データへの変換を実行する </summary>
        /// <param name="tmp">帳票データを表す文字列</param>
        /// <returns>文字列配列の帳票データ</returns>
        private string[] ReportSplit(string tmp)
        {
            // 値が空の項目を半角スペースに置き換える(""⇒" ")
            tmp = tmp.Replace("\"\"", "\" \"");

            // 先頭と末尾の"(ダブルコーテーション)を削除する
            // 先頭と末尾の空白を削除
            tmp = tmp.Trim();

            // 先頭と末尾以外を抽出(先頭と、末尾は"(ダブルコーテーション))
            tmp = tmp.Substring(1, tmp.Length - 2);

            // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
            string[] splt = { "\",\"" };
            string[] ret = tmp.Split(splt, StringSplitOptions.RemoveEmptyEntries);

            return ret;
        }
    }
}
