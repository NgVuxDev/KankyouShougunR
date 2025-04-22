using System.Linq;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.OriginalException;
using r_framework.Utility;

namespace r_framework.Const
{
    /// <summary>
    /// 処理区分
    /// </summary>
    public enum DENSHU_KBN : int
    {
        /// <summary>
        /// 該当なし
        /// </summary>
        NONE = 0,

        /// <summary>受入</summary>
        UKEIRE = 1,

        /// <summary>出荷</summary>
        SHUKKA = 2,

        /// <summary>売上支払</summary>
        URIAGE_SHIHARAI = 3,

        /// <summary>共通</summary>
        KYOUTSUU = 9,

        /// <summary>入金</summary>
        NYUUKIN = 10,

        /// <summary>出金</summary>
        SHUKKIN = 20,

        /// <summary>請求</summary>
        SEIKYUU = 30,

        /// <summary>請求締</summary>
        SEIKYUU_SHIME = 40,

        /// <summary>支払</summary>
        SHIHARAI = 50,

        /// <summary>支払締</summary>
        SHIHARAI_SHIME = 60,

        /// <summary>紙マニフェスト</summary>
        KAMI_MANIFEST = 80,

        /// <summary>電子マニフェスト</summary>
        DENSHI_MANIFEST = 90,

        /// <summary>受付</summary>
        UKETSUKE = 100,

        /// <summary>配車</summary>
        HAISHA = 110,

        /// <summary>定期配車</summary>
        TEIKI_HAISHA = 120,

        /// <summary>定期実績</summary>
        TEIKI_JISSEKI = 130,

        /// <summary>モバイル連携</summary>
        MOBILE_RENKEI = 125,

        /// <summary>計量</summary>
        KEIRYOU = 140,

        /// <summary>在庫</summary>
        ZAIKO = 150,

        /// <summary>在庫締</summary>
        ZAIKO_SHIME = 151,

        /// <summary>物販在庫調整一覧</sumary>
        [PatternOutputKbn(1)]
        BUPPAN_ZAIKO_TYOUSEI = 155,

        /// <summary>物販在庫締処理</summary>
        BUPPAN_ZAIKO_SHIME_SYORI = 156,

        /// <summary>運賃</summary>
        UNCHIN = 160,

        /// <summary>代納</summary>
        DAINOU = 170,

        /// <summary>見積</summary>
        MITSUMORI = 180,

        /// <summary>受注目標</summary>
        JYUCYUU_MOKUHYOU = 181,

        /// <summary>営業予算</summary>
        EIGYOU_YOSAN = 182,

        /// <summary>委託契約書</summary>
        [PatternOutputKbn(1)]
        ITAKU_KEIYAKUSHO = 190,

        /// <summary>覚書一括</summary>
        OBOEGAKI_IKKATSU = 191,

        /// <summary>取引先</summary>
        [PatternOutputKbn(1)]
        TORIHIKISAKI = 200,

        /// <summary>業者</summary>
        [PatternOutputKbn(1)]
        GYOUSHA = 210,

        /// <summary>現場</summary>
        GENBA = 220,

        /// <summary>電マニ事業者</summary>
        [PatternOutputKbn(1)]
        DENNSHI_MANIFEST_JIGYOUSHA = 230,

        /// <summary>電マニ事業場</summary>
        [PatternOutputKbn(1)]
        DENNSHI_MANIFEST_JIGYOUJO = 240,

        /// <summary>入金先</summary>
        NYUUKINSAKI = 250,

        /// <summary>出金先</summary>
        SHUKKINSAKI = 260,

        /// <summary>コース</summary>
        COURSE = 270,

        /// <summary>汎用CSV出力</summary>
        HANYO_CSV_SHUTSURYOKU = 280,

        /// <summary>実績報告書/// </summary>
        JISSEKI_HOUKOKUSHO = 400,

        /// <summary>電子申請</summary>
        DENSHI_SHINSEI = 500,

        /// <summary>検索結果一覧</summary>
        [PatternOutputKbn(1)]
        KENSAKU_KEKKA = 900,

        /// <summary>入出金一覧</summary>
        NYUUSHUKKIN_ICHIRAN = 901,

        #region 入出金一覧でのみ使用
        /// <summary>入金一覧</summary>
        NYUUKIN_ICHIRAN = 9011,

        /// <summary>出金一覧</summary>
        SHUKKIN_ICHIRAN = 9012,
        #endregion

        /// <summary>入金消込一覧</summary>
        [PatternOutputKbn(2)]
        NYUUKIN_KESHIKOMI_RIEKI_ICHIRAN = 902,

        /// <summary>請求一覧</summary>
        [PatternOutputKbn(1)]
        SEIKYUU_ICHIRAN = 903,

        /// <summary>支払明細一覧</summary>
        [PatternOutputKbn(1)]
        SHIHARAI_ICHIRAN = 904,

        /// <summary>マニフェスト一覧</summary>
        MANIFEST_ICHIRAN = 905,

        /// <summary>マニフェスト紐付一覧</summary>
        MANIFEST_HIMODUKE_ICHIRAN = 1006,

        #region マニフェスト一覧でのみ使用

        /// <summary>マニ一覧(直行)</summary>
        MANI_ICHIRAN_CHOKKOU = 9051,

        /// <summary>マニ一覧(積替)</summary>
        MANI_ICHIRAN_TSUMIKAE = 9052,

        /// <summary>マニ一覧(建廃)</summary>
        MANI_ICHIRAN_KENPAI = 9053,

        /// <summary>マニ一覧(電子)</summary>
        MANI_ICHIRAN_DENSHI = 9054,

        /// <summary>マニ一覧(全て)</summary>
        MANI_ICHIRAN_ALL = 9055,

        #endregion

        /// <summary>返却日一覧</summary>
        [PatternOutputKbn(1)]
        HENKYAKUBI_ICHIRAN = 906,

        /// <summary>マニパターン一覧</summary>
        MANI_PATTERN_ICHIRAN = 907,

        /// <summary>売上確定入力</summary>
        URIAGE_KAKUTEI_NYUURYOKU = 908,

        /// <summary>支払確定入力</summary>
        SHIHARAI_KAKUTEI_NYUURYOKU = 909,

        /// <summary>伝票一覧</summary>
        DENPYOU_ICHIRAN = 910,

        #region 伝票一覧でのみ使用

        /// <summary>受入一覧</summary>
        UKEIRE_ICHIRAN = 9101,

        /// <summary>出荷一覧</summary>
        SHUKKA_ICHIRAN = 9102,

        /// <summary>売上/支払一覧</summary>
        URIAGE_SHIHARAI_ICHIRAN = 9103,

        /// <summary>計量一覧</summary>
        KEIRYOU_ICHIRAN = 9104,

        /// <summary>運賃一覧</summary>
        UNCHIN_ICHIRAN = 9105,

        /// <summary>代納一覧</summary>
        DAINOU_ICHIRAN = 9106,

        //PhuocLoc 2021/05/05 #148576 -Start
        /// <summary>伝票一覧</summary>
        UKEIRE_SHUKKA_URSH_ICHIRAN = 9107,
        //PhuocLoc 2021/05/05 #148576 -End

        #endregion

        /// <summary>滞留一覧</summary>
        TAIRYU_ICHIRAN = 911,

        /// <summary>汎用帳票</summary>
        HANYOU_CHOUHYOU = 912,

        /// <summary>引合業者</summary>
        [PatternOutputKbn(1)]
        HIKIAI_GYOUSHA = 913,

        /// <summary>月極売上伝票一覧</summary>
        TSUKIGIME_URIAGE_DENPYOU = 914,

        /// <summary>送信保留登録情報</summary>
        [PatternOutputKbn(1)]
        SOUSHIN_HORYUU_TOUROKU = 915,

        /// <summary>運搬終了報告</summary>
        [PatternOutputKbn(1)]
        UNPAN_SYUURYOU_HOUKOKU = 916,

        /// <summary>処分終了報告</summary>
        [PatternOutputKbn(1)]
        SHOBUN_SYUURYOU_HOUKOKU = 917,

        /// <summary>送信保留最終処分報告</summary>
        [PatternOutputKbn(1)]
        SOUSHIN_HORYUU_SAISYUU_HOUKOKU = 918,

        /// <summary>最新情報照会</summary>
        [PatternOutputKbn(1)]
        SAISHIN_JOUHOU_SHOUKAI = 919,

        /// <summary>一覧出力項目選択</summary>
        ICHIRANSYUTSURYOKU_KOUMOKU = 920,

        /// <summary>伝票紐付一覧</summary>
        DENPYOU_HIMODUKE_ICHIRAN = 921,

        /// <summary>一覧出力項目選択(伝票紐付一覧用)</summary>
        ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU = 922,

        /// <summary>電子マニパターン一覧</summary>
        DENSHI_MANI_PATTERN_ICHIRAN = 923,

        /// <summary>最終処分場所パターン一覧</summary>
        [PatternOutputKbn(1)]
        SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN = 924,

        /// <summary>中間処分場所パターン一覧</summary>
        [PatternOutputKbn(1)]
        CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN = 925,

        /// <summary>マニフェスト入力(一括入力)</summary>
        MANIFEST_IKKATU = 926,

        /// <summary>引合取引先</summary>
        [PatternOutputKbn(1)]
        HIKIAI_TORIHIKISAKI = 927,

        /// <summary>引合現場</summary>
        [PatternOutputKbn(1)]
        HIKIAI_GENBA = 928,

        #region 受付一覧用

        /// <summary>収集受付一覧</summary>
        UKETSUKE_SS_ICHIRAN = 1001,

        /// <summary>出荷受付一覧</summary>
        UKETSUKE_SK_ICHIRAN = 1002,

        /// <summary>持込受付一覧</summary>
        UKETUSKE_MK_ICHIRAN = 1003,

        /// <summary>物販受付一覧</summary>
        UKETUSKE_BP_ICHIRAN = 1004,

        /// <summary>クレーム受付一覧</summary>
        [PatternOutputKbn(1)]
        UKETSUKE_CM_ICHIRAN = 1005,

        /// <summary>収集＋出荷受付一覧</summary>
        UKETUSKE_SS_SK_ICHIRAN = 1007,

        /// <summary>収集＋持込受付一覧</summary>
        UKETUSKE_SS_MK_ICHIRAN = 1008,

        #endregion

        #region 伝票紐付一覧でのみ使用

        /// <summary>受入紐付一覧</summary>
        UKEIRE_HIMO = 9001,

        /// <summary>出荷紐付一覧</summary>
        SHUKKA_HIMO = 9002,

        /// <summary>売上／支払紐付一覧</summary>
        URIAGE_SHIHARAI_HIMO = 9003,

        /// <summary>マニ1次紐付一覧</summary>
        MANI_ICHIJI_HIMO = 9080,

        /// <summary>マニ2次紐付一覧</summary>
        MANI_NIJI_HIMO = 9081,

        /// <summary>電マニ紐付一覧</summary>
        DENMANI_HIMO = 9090,

        /// <summary>受付紐付一覧</summary>
        UKETSUKE_HIMO = 9100,

        /// <summary>計量紐付一覧</summary>
        KEIRYOU_HIMO = 9140,

        /// <summary>運賃紐付一覧</summary>
        UNCHIN_HIMO = 9160,

        /// <summary>代納紐付一覧</summary>
        DAINOU_HIMO = 9170,

        #endregion

        #region 処分場所パターン一覧用

        /// <summary>最終処分場所パターン一覧(産廃)</summary>
        [PatternOutputKbn(1)]
        SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI = 9241,

        /// <summary>最終処分場所パターン一覧(建廃)</summary>
        [PatternOutputKbn(1)]
        SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI = 9242,

        /// <summary>中間処分場所パターン一覧(産廃)</summary>
        [PatternOutputKbn(1)]
        CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI = 9251,

        /// <summary>中間処分場所パターン一覧(建廃)</summary>
        [PatternOutputKbn(1)]
        CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI = 9252,

        #endregion

        /// <summary>
        /// マニフェスト終了日警告一覧
        /// </summary>
        T_MANIFEST_SHUURYOUBI_KEIKOKU_ICHIRAN = 9056,

        /// <summary>
        /// 換算値再計算
        /// </summary>
        MANIFEST_KANSAN_SAIKEISAN_ICHIRAN = 9301,

        /// <summary>
        /// 伝票確定入力
        /// </summary>
        DENPYOU_KAKUTEI_NYUURYOKU = 929,


        /// <summary>入金データ取込</summary>
        [PatternOutputKbn(1)]
        NYUUKIN_DATA_TORIKOMI_ICHIRAN = 940,


        /// <summary>コース一覧</summary>
        [PatternOutputKbn(1)]
        M_COURSE_ICHIRAN = 271,

        /// <summary>
        /// 外部連携現場一覧
        /// </summary>
        [PatternOutputKbn(1)]
        GAIBU_RENKEI_GENBA_ICHIRAN = 290,

        /// <summary>
        /// 配送計画入力
        /// </summary>
        HAISOU_KEIKAKU_NYUURYOKU = 300,

        /// <summary>
        /// 電子契約履歴一覧
        /// </summary>
        DENSHI_KEIYAKU_RIREKI_ICHIRAN = 330,

        /// <summary>マニフェスト終了日一括更新</summary>
        [PatternOutputKbn(1)]
        MANIFEST_IKKATSU_UPD_ICHIRAN = 9320,

        /// <summary>
        /// 紐付1次最終処分終了報告
        /// </summary>
        DENSHI_MANIFEST_SAISHU_SHOBUN = 9330,

        /// <summary>個別品名単価一覧</summary>
        [PatternOutputKbn(1)]
        KOBETSU_HINMEI_TANKA_ICHIRAN = 9350,

        /// <summary>マニフェスト一覧</summary>
        [PatternOutputKbn(2)]
        T_MANIFEST_JISSEKI_ICHIRAN = 9057,

        /// <summary>
        /// 配車計画(NAVITIME)
        /// </summary>
        HAISOU_KEIKAKU_TEIKI = 310,

        /// <summary>
        /// ファイルアップロード
        /// </summary>
        FILE_UPLOAD = 340,

        /// <summary>
        /// ファイルアップロード一覧
        /// </summary>
        FILE_UPLOAD_ICHIRAN = 350,

        /// <summary>
        /// 電子契約最新照会
        /// </summary>
        [PatternOutputKbn(1)]
        DENSHI_KEIYAKU_SAISHIN_SHOUKAI = 360,

        /// <summary>
        /// 受入実績
        /// </summary>
        UKEIRE_JISSEKI = 370,

        /// <summary>
        /// 現場メモ
        /// </summary>
        GENBA_MEMO = 380,

        /// <summary>
        /// 現場メモ一覧
        /// </summary>
        GENBA_MEMO_ICHIRAN = 390,

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ着信結果
        /// </summary>
        SMS_RESULT = 410,

        //CongBinh 20210714 #152813 S
        [PatternOutputKbn(2)]
        KYOKASHOU_ICHIRAN = 748,
        //CongBinh 20210714 #152813 E

        // Begin: LANDUONG - 20220211 - refs#157800
        [PatternOutputKbn(1)]
        T_RAKURAKU_MASUTA_ICHIRAN = 9401,
        // End: LANDUONG - 20220211 - refs#157800

        /// <summary>出金消込一覧</summary>
        [PatternOutputKbn(2)]
        SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN = 9013,

        //#160047 20220328 CongBinh S
        [PatternOutputKbn(1)]
        DENSHI_KEIYAKU_SAISHIN_SHOUKAI_WAN_SIGN = 758,
        //#160047 20220328 CongBinh E
    }

    /// <summary>
    /// 伝種区分の拡張メソッド
    /// </summary>
    public static class DENSHU_KBNExt
    {
        /// <summary>
        /// 伝種区分から伝種区分名を取得
        /// </summary>
        /// <param name="e">画面タイプ</param>
        /// <returns>画面タイプ名</returns>
        public static string ToTitleString(this DENSHU_KBN e)
        {
            switch (e)
            {
                case DENSHU_KBN.UKEIRE:
                    return "受入一覧";
                case DENSHU_KBN.SHUKKA:
                    return "出荷一覧";
                case DENSHU_KBN.URIAGE_SHIHARAI:
                    return "売上支払一覧";
                //PhuocLoc 2021/05/05 #148576 -Start
                case DENSHU_KBN.UKEIRE_SHUKKA_URSH_ICHIRAN:
                    return "伝票一覧";
                //PhuocLoc 2021/05/05 #148576 -End
                case DENSHU_KBN.NYUUKIN:
                    return "入金一覧";
                case DENSHU_KBN.SHUKKIN:
                    return "出金一覧";
                case DENSHU_KBN.SEIKYUU:
                    return "請求一覧";
                case DENSHU_KBN.SEIKYUU_SHIME:
                    return "請求締一覧";
                case DENSHU_KBN.SHIHARAI:
                    return "支払一覧";
                case DENSHU_KBN.SHIHARAI_SHIME:
                    return "支払締一覧";
                case DENSHU_KBN.KAMI_MANIFEST:
                    return "紙マニフェスト一覧";
                case DENSHU_KBN.DENSHI_MANIFEST:
                    return "電子マニフェスト一覧";
                case DENSHU_KBN.UKETSUKE:
                    return "受付一覧";
                case DENSHU_KBN.HAISHA:
                    return "配車一覧";
                case DENSHU_KBN.TEIKI_HAISHA:
                    return "定期配車一覧";
                case DENSHU_KBN.TEIKI_JISSEKI:
                    return "定期配車実績一覧";
                case DENSHU_KBN.KEIRYOU:
                    return "計量一覧";
                case DENSHU_KBN.ZAIKO:
                    return "在庫調整一覧";
                case DENSHU_KBN.UNCHIN:
                    return "運賃一覧";
                case DENSHU_KBN.DAINOU:
                    return "代納一覧";
                case DENSHU_KBN.MITSUMORI:
                    return "見積一覧";
                case DENSHU_KBN.ITAKU_KEIYAKUSHO:
                    return "委託契約書一覧";
                case DENSHU_KBN.TORIHIKISAKI:
                    return "取引先一覧";
                case DENSHU_KBN.KOBETSU_HINMEI_TANKA_ICHIRAN:
                    return "個別品名単価一覧";
                case DENSHU_KBN.GYOUSHA:
                    return "業者一覧";
                case DENSHU_KBN.GENBA:
                    return "現場一覧";
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUSHA:
                    return "電マニ事業者一覧";
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUJO:
                    return "電マニ事業場一覧";
                case DENSHU_KBN.NYUUKINSAKI:
                    return "入金先一覧";
                case DENSHU_KBN.SHUKKINSAKI:
                    return "出金先一覧";
                case DENSHU_KBN.JISSEKI_HOUKOKUSHO:
                    return "実績報告書";
                case DENSHU_KBN.KENSAKU_KEKKA:
                    return "検索結果一覧";
                case DENSHU_KBN.NYUUSHUKKIN_ICHIRAN:
                    return "入出金一覧";
                case DENSHU_KBN.NYUUKIN_KESHIKOMI_RIEKI_ICHIRAN:
                    return "入金消込一覧";
                case DENSHU_KBN.SEIKYUU_ICHIRAN:
                    return "請求一覧";
                case DENSHU_KBN.SHIHARAI_ICHIRAN:
                    return "支払明細一覧";
                case DENSHU_KBN.MANIFEST_ICHIRAN:
                    return "マニフェスト一覧";
                case DENSHU_KBN.MANI_ICHIRAN_CHOKKOU:
                    return "産廃(直行)マニフェスト一覧";
                case DENSHU_KBN.MANI_ICHIRAN_TSUMIKAE:
                    return "産廃(積替)マニフェスト一覧";
                case DENSHU_KBN.MANI_ICHIRAN_KENPAI:
                    return "建廃マニフェスト一覧";
                case DENSHU_KBN.MANI_ICHIRAN_DENSHI:
                    return "電子マニフェスト一覧";
                case DENSHU_KBN.MANI_ICHIRAN_ALL:
                    return "マニフェスト一覧";
                case DENSHU_KBN.HENKYAKUBI_ICHIRAN:
                    // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する start
                    //return "返却日一覧";
                    return "返却日入力／一覧";
                    // 20140621 katen EV004673 返却日入力の抽出条件と明細に廃棄物区分を追加する end
                case DENSHU_KBN.MANI_PATTERN_ICHIRAN:
                    return "マニパターン一覧";
                case DENSHU_KBN.URIAGE_KAKUTEI_NYUURYOKU:
                    return "売上確定入力";
                case DENSHU_KBN.SHIHARAI_KAKUTEI_NYUURYOKU:
                    return "支払確定入力";
                case DENSHU_KBN.DENPYOU_ICHIRAN:
                    return "伝票一覧";
                case DENSHU_KBN.UKEIRE_ICHIRAN:
                    return "受入一覧";
                case DENSHU_KBN.SHUKKA_ICHIRAN:
                    return "出荷一覧";
                case DENSHU_KBN.URIAGE_SHIHARAI_ICHIRAN:
                    return "売上/支払一覧";
                case DENSHU_KBN.KEIRYOU_ICHIRAN:
                    return "計量一覧";
                case DENSHU_KBN.UNCHIN_ICHIRAN:
                    return "運賃一覧";
                case DENSHU_KBN.DAINOU_ICHIRAN:
                    return "代納一覧";
                case DENSHU_KBN.TAIRYU_ICHIRAN:
                    return "滞留一覧";
                case DENSHU_KBN.ZAIKO_SHIME:
                    return "在庫締";
                case DENSHU_KBN.BUPPAN_ZAIKO_TYOUSEI:
                    return "物販在庫調整一覧";
                case DENSHU_KBN.JYUCYUU_MOKUHYOU:
                    return "受注目標";
                case DENSHU_KBN.EIGYOU_YOSAN:
                    return "営業予算";
                case DENSHU_KBN.OBOEGAKI_IKKATSU:
                    return "覚書一括";
                case DENSHU_KBN.HIKIAI_GYOUSHA:
                    return "引合業者一覧";
                case DENSHU_KBN.TSUKIGIME_URIAGE_DENPYOU:
                    return "月極売上伝票一覧";
                case DENSHU_KBN.SOUSHIN_HORYUU_TOUROKU:
                    return "送信保留登録情報";
                case DENSHU_KBN.UNPAN_SYUURYOU_HOUKOKU:
                    return "運搬終了報告";
                case DENSHU_KBN.SHOBUN_SYUURYOU_HOUKOKU:
                    return "処分終了報告";
                case DENSHU_KBN.SOUSHIN_HORYUU_SAISYUU_HOUKOKU:
                    return "送信保留最終処分報告";
                case DENSHU_KBN.SAISHIN_JOUHOU_SHOUKAI:
                    return "最新情報照会";
                case DENSHU_KBN.ICHIRANSYUTSURYOKU_KOUMOKU:
                    return "一覧出力項目選択";
                case DENSHU_KBN.UKETSUKE_SS_ICHIRAN:
                    return "収集受付一覧";
                case DENSHU_KBN.UKETSUKE_SK_ICHIRAN:
                    return "出荷受付一覧";
                case DENSHU_KBN.UKETUSKE_MK_ICHIRAN:
                    return "持込受付一覧";
                case DENSHU_KBN.UKETUSKE_BP_ICHIRAN:
                    return "物販受付一覧";
                case DENSHU_KBN.UKETSUKE_CM_ICHIRAN:
                    return "クレーム受付一覧";
                case DENSHU_KBN.UKETUSKE_SS_SK_ICHIRAN:
                    return "収集＋出荷受付一覧";
                case DENSHU_KBN.UKETUSKE_SS_MK_ICHIRAN:
                    return "収集＋持込受付一覧";
                case DENSHU_KBN.DENPYOU_HIMODUKE_ICHIRAN:
                    return "伝票紐付一覧";
                case DENSHU_KBN.DENSHI_MANI_PATTERN_ICHIRAN:
                    return "電子マニパターン一覧";
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN:
                    return "最終処分場所パターン一覧";
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                    return "最終処分場所パターン一覧(産廃)";
                case DENSHU_KBN.SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    return "最終処分場所パターン一覧(建廃)";
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN:
                    return "中間処分場所パターン一覧";
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_SANPAI:
                    return "中間処分場所パターン一覧(産廃)";
                case DENSHU_KBN.CYUUKAN_SHOBUNBASHO_PATTERN_ICHIRAN_KENPAI:
                    return "中間処分場所パターン一覧(建廃)";
                case DENSHU_KBN.MANIFEST_IKKATU:
                    return "マニフェスト入力(一括入力)";
                case DENSHU_KBN.HIKIAI_GENBA:
                    return "引合現場一覧";
                case DENSHU_KBN.HIKIAI_TORIHIKISAKI:
                    return "引合取引先一覧";
                case DENSHU_KBN.MANIFEST_HIMODUKE_ICHIRAN:
                // 20140604 ria EV004639 マニフェスト紐付状況一覧のタイトルがマニフェスト紐付一覧になっている start
                    //return "マニフェスト紐付一覧";
                    return "マニフェスト紐付状況一覧";
                // 20140604 ria EV004639 マニフェスト紐付状況一覧のタイトルがマニフェスト紐付一覧になっている end
                case DENSHU_KBN.T_MANIFEST_SHUURYOUBI_KEIKOKU_ICHIRAN:
                    return "マニフェスト終了日警告一覧";
                case DENSHU_KBN.MANIFEST_KANSAN_SAIKEISAN_ICHIRAN:
                    return "換算値再計算";
                case DENSHU_KBN.DENPYOU_KAKUTEI_NYUURYOKU:
                    return "伝票確定入力";
                case DENSHU_KBN.HANYO_CSV_SHUTSURYOKU:
                    return "汎用CSV出力";
                case DENSHU_KBN.NYUUKIN_DATA_TORIKOMI_ICHIRAN:
                    return "入金データ取込";
                case DENSHU_KBN.M_COURSE_ICHIRAN:
                    return "コース一覧";
                case DENSHU_KBN.GAIBU_RENKEI_GENBA_ICHIRAN:
                    return "外部連携現場一覧";
                case DENSHU_KBN.MANIFEST_IKKATSU_UPD_ICHIRAN:
                    return "マニフェスト終了日一括更新";
                case DENSHU_KBN.DENSHI_MANIFEST_SAISHU_SHOBUN:
                    return "紐付1次最終処分終了報告";
                case DENSHU_KBN.T_MANIFEST_JISSEKI_ICHIRAN:
                    return "マニフェスト実績一覧";
                case DENSHU_KBN.DENSHI_KEIYAKU_RIREKI_ICHIRAN:
                    return "電子契約履歴一覧";
                case DENSHU_KBN.FILE_UPLOAD:
                    return "ファイルアップロード";
                case DENSHU_KBN.FILE_UPLOAD_ICHIRAN:
                    return "ファイルアップロード一覧";
                case DENSHU_KBN.DENSHI_KEIYAKU_SAISHIN_SHOUKAI:
                    return "電子契約最新照会";
                case DENSHU_KBN.UKEIRE_JISSEKI:
                    return "受入実績";
                case DENSHU_KBN.GENBA_MEMO_ICHIRAN:
                    return "現場メモ一覧";
                case DENSHU_KBN.SMS_RESULT:
                    return "ｼｮｰﾄﾒｯｾｰｼﾞ着信結果";
                //CongBinh 20210714 #152813 S
                case DENSHU_KBN.KYOKASHOU_ICHIRAN:
                    return "許可証一覧";
                //CongBinh 20210714 #152813 E
                // QNTUAN #157800 S
                case DENSHU_KBN.T_RAKURAKU_MASUTA_ICHIRAN:
                    return "楽楽明細マスタ一覧";
                // QNTUAN #157800 E
                case DENSHU_KBN.SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN:
                    return "出金消込一覧";
                //#160047 20220328 CongBinh S
                case DENSHU_KBN.DENSHI_KEIYAKU_SAISHIN_SHOUKAI_WAN_SIGN:
                    return "電子契約最新照会（WAN-Sign）";
                //#160047 20220328 CongBinh E
                default:
                    return null;
            }
        }

        /// <summary>
        /// 伝種区分から一覧表示用のXMLファイルパスを取得するメソッド
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetXmlFileName(this DENSHU_KBN e)
        {
            switch (e)
            {
                case DENSHU_KBN.TORIHIKISAKI:
                    return "TorihikisakiSetting.xml";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 伝種区分から利用する各種Daoを取得
        /// </summary>
        /// <param name="e">画面タイプ</param>
        /// <returns>Dao</returns>
        public static IS2Dao GetDao(this DENSHU_KBN e)
        {
            switch (e)
            {
                case DENSHU_KBN.UKEIRE:
                    return DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
                //case DENSHU_KBN.SHUKKA:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.URIAGE_SHIHARAI:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.NYUUKIN:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.SHUKKIN:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.SEIKYUU:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.SEIKYUU_SHIME:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.SHIHARAI:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.SHIHARAI_SHIME:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.KAMI_MANIFEST:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.DENSHI_MANIFEST:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.UKETSUKE:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.HAISHA:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.TEIKI_HAISHA:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.TEIKI_JISSEKI:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.KEIRYOU:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.ZAIKO:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.UNCHIN:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.DAINOU:
                //    return DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.MITSUMORI:
                //    return DaoInitUtility.GetComponent<>();
                case DENSHU_KBN.ITAKU_KEIYAKUSHO:
                    return DaoInitUtility.GetComponent<IM_ITAKU_KEIYAKU_KIHONDao>();
                case DENSHU_KBN.TORIHIKISAKI:
                    return DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                case DENSHU_KBN.GYOUSHA:
                    return DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                case DENSHU_KBN.GENBA:
                    return DaoInitUtility.GetComponent<IM_GENBADao>();
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUSHA:
                    return DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUJO:
                    return DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                case DENSHU_KBN.KENSAKU_KEKKA:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 伝種区分から利用するEntityを取得
        /// </summary>
        /// <param name="e">画面タイプ</param>
        /// <returns>Entity</returns>
        public static SuperEntity GetEntity(this DENSHU_KBN e)
        {
            switch (e)
            {
                case DENSHU_KBN.UKEIRE:
                    return new T_UKEIRE_ENTRY();
                //case DENSHU_KBN.SHUKKA:
                //    return DaoInitUtility.GetComponent<IT_UKEIRE_ENTRYDao>();
                //case DENSHU_KBN.URIAGE_SHIHARAI:
                //    return  DaoInitUtility.GetComponent<>();
                //case DENSHU_KBN.NYUUKIN:
                //    return "入金";
                //case DENSHU_KBN.SHUKKIN:
                //    return "出金";
                //case DENSHU_KBN.SEIKYUU:
                //    return "請求";
                //case DENSHU_KBN.SEIKYUU_SHIME:
                //    return "請求締";
                //case DENSHU_KBN.SHIHARAI:
                //    return "支払";
                //case DENSHU_KBN.SHIHARAI_SHIME:
                //    return "支払締";
                //case DENSHU_KBN.KAMI_MANIFEST:
                //    return "紙マニフェスト";
                //case DENSHU_KBN.DENSHI_MANIFEST:
                //    return "電子マニフェスト";
                //case DENSHU_KBN.UKETSUKE:
                //    return "受付";
                //case DENSHU_KBN.HAISHA:
                //    return "配車";
                //case DENSHU_KBN.TEIKI_HAISHA:
                //    return "定期配車";
                //case DENSHU_KBN.TEIKI_JISSEKI:
                //    return "定期実績";
                //case DENSHU_KBN.KEIRYOU:
                //    return "計量";
                //case DENSHU_KBN.ZAIKO:
                //    return "在庫";
                //case DENSHU_KBN.UNCHIN:
                //    return "運賃";
                //case DENSHU_KBN.DAINOU:
                //    return "代納";
                //case DENSHU_KBN.MITSUMORI:
                //    return "見積";
                case DENSHU_KBN.ITAKU_KEIYAKUSHO:
                    return new M_ITAKU_KEIYAKU_KIHON();
                case DENSHU_KBN.TORIHIKISAKI:
                    return new M_TORIHIKISAKI();
                //case DENSHU_KBN.GYOUSHA:
                //    return DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                //case DENSHU_KBN.GENBA:
                //    return DaoInitUtility.GetComponent<IM_GENBADao>();
                //case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUSHA:
                //    return DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
                //case DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUBA:
                //    return DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
                case DENSHU_KBN.KENSAKU_KEKKA:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// パターンの出力区分を取得します
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int GetPatternOutputKbn(this DENSHU_KBN e)
        {
            var t = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(PatternOutputKbn), false)
               .OfType<PatternOutputKbn>().FirstOrDefault();
            return t == null ? 0 : t.OutputKbn;
        }
    }
}
