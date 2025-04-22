using System;
using System.Linq;

namespace r_framework.Const
{
    /// <summary>
    /// 画面ID
    /// </summary>
    /// <remarks>
    /// 新規に画面IDを追加する場合は末尾に追加してください
    /// ※IDに固定値を振っているものがあり、途中にIDを追加することにより
    /// ※enum値にズレが発生するため
    /// </remarks>
    public enum WINDOW_ID : int
    {
        /// <summary>何もなし</summary>
        NONE = -1,

        /// <summary>G000 メインメニュー</summary>
        MAIN_MENU,
        /// <summary>全メニュー表示</summary>
        MENU_LIST,
        /// <summary>受付（収集）</summary>
        UKETSUKE_SHUSHU,
        /// <summary>顧客一覧</summary>
        KOKYAKU_ITIRAN,
        /// <summary>受付（出荷）</summary>
        UKETSUKE_SHUKKA,
        /// <summary>受付一覧</summary>
        UKETSUKE_ICHIRAN,
        /// <summary>G451 ログイン</summary>
        [WindowTitle("ログイン情報")]
        LOGIN,
        /// <summary>郵便辞書一覧画面</summary>
        S_ZIP_CODE,

        #region 共通
        /// <summary>G176 検索結果一覧</summary>
        [WindowTitle("検索結果一覧")]
        C_KENSAKUKEKKA_ICHIRAN,
        /// <summary>G177 検索条件設定</summary>
        [WindowTitle("検索条件設定")]
        C_KENSAKUJYOUKEN_SETTEI,
        /// <summary>G186 パターン一覧</summary>
        [WindowTitle("パターン一覧")]
        C_PATTERN_ICHIRAN,
        /// <summary>G187 一覧出力項目選択</summary>
        [WindowTitle("一覧出力項目選択")]
        C_ICHIRANSYUTSURYOKU_KOUMOKU,
        /// <summary>G184 コンテナ指定</summary>
        [WindowTitle("コンテナ指定")]
        C_CONTENA_SHITEI,
        /// <summary>トラックスケール通信設定</summary>
        [WindowTitle("トラックスケール通信設定")]
        C_TRUCKSCALE_TSUUSHINSETTEI,
        /// <summary>トラックスケール重量値読込み</summary>
        [WindowTitle("トラックスケール重量値読込み")]
        C_TRUCKSCALE_WEIGHT,
        /// <summary>G487 印刷設定画面</summary>
        [WindowTitle("印刷設定画面")]
        C_REPORT_PRINTER_SETTING,
        /// <summary>G449 伝票紐付一覧</summary>
        [WindowTitle("伝票紐付一覧")]
        C_DENPYOU_HIMODUKE_ICHIRAN,
        /// <summary>G328 回収品名詳細</summary>
        [WindowTitle("回収品名詳細")]
        C_KAISYUU_HINMEI_SHOUSAI,
        /// <summary>G475 委託契約情報検索</summary>
        [WindowTitle("委託契約情報検索")]
        C_ITAKU_KEIYAKU_JOUHOU_KENSAKU,
        /// <summary>G480 一覧出力項目選択（伝票紐付一覧用）</summary>
        [WindowTitle("一覧出力項目選択（伝票紐付一覧用）")]
        C_ICHIRANSYUTSURYOKU_KOUMOKU_DENPYOU,
        /// <summary>G554 伝票紐付一覧用パターン一覧</summary>
        [WindowTitle("伝票紐付一覧用パターン一覧")]
        C_DENPYOUHIMOZUKE_PATTERN_ICHIRAN,

        #endregion
        #region 業務
        #region 受付
        /// <summary>G015 収集受付入力画面</summary>
        [WindowTitle("収集受付入力")]
        T_UKETSUKE_SHUSHU,
        /// <summary>G016 出荷受付入力画面</summary>
        [WindowTitle("出荷受付入力")]
        T_UKETSUKE_SHUKKA,
        /// <summary>G018 持込受付入力</summary>
        [WindowTitle("持込受付入力")]
        T_UKETSUKE_MOCHIKOMI,
        /// <summary>G019 物販受付入力</summary>
        [WindowTitle("物販受付入力")]
        T_UKETSUKE_BUPPAN,
        /// <summary>G020 クレーム受付入力画面</summary>
        [WindowTitle("クレーム受付入力")]
        T_UKETSUKE_COMPLAIN,
        /// <summary>G021 受付一覧画面</summary>
        [WindowTitle("受付一覧")]
        T_UKETSUKE_ICHIRAN,
        #endregion
        #region 売上／支払処理
        /// <summary>G051 受入入力</summary>
        [WindowTitle("受入入力")]
        T_UKEIRE,
        /// <summary>G053 出荷入力</summary>
        [WindowTitle("出荷入力")]
        T_SHUKKA,
        /// <summary>G054 売上／支払入力</summary>
        [WindowTitle("売上／支払入力")]
        T_URIAGE_SHIHARAI,
        /// <summary>G055 伝票一覧</summary>
        [WindowTitle("伝票一覧")]
        T_DENPYO_ICHIRAN,
        /// <summary>G292 搬入予定一覧</summary>
        [WindowTitle("搬入予定一覧")]
        T_HANNYU_YOTEI_ICHIRAN,
        /// <summary>G303 滞留一覧</summary>
        [WindowTitle("滞留一覧")]
        T_TAIRYU_ICHIRAN,
        /// <summary>G304 車輌選択</summary>
        [WindowTitle("車輌選択")]
        T_SYARYOU_SENTAKU,
        /// <summary>G331 月極売上伝票作成</summary>
        [WindowTitle("月極売上伝票作成")]
        T_TSUKIGIME_URIAGE_DENPYOU,
        /// <summary>G335 伝票発行</summary>
        [WindowTitle("伝票発行")]
        T_DENPYOU_HAKKOU,
        /// <summary>G466 伝票一覧</summary>
        [WindowTitle("伝票一覧")]
        T_DENPYOU_ICHIRAN,
        /// <summary>G482 月極売上伝票作成(定期配車用)</summary>
        [WindowTitle("月極売上伝票作成(定期配車用)")]
        T_TSUKIGIME_URIAGE_DENPYOU_TEIKI_HAISHA,
        /// <summary>G568 売上明細表出力（固定帳票）</summary>
        [WindowTitle("売上明細表 出力画面")]
        T_URIAGE_MEISAIHYOU_KOTEI,
        /// <summary>G569 受入明細表出力（固定帳票）</summary>
        [WindowTitle("受入明細表 出力画面")]
        T_UKEIRE_MEISAIHYOU_KOTEI,
        /// <summary>G572 支払明細表出力（固定帳票）</summary>
        [WindowTitle("支払明細表 出力画面")]
        T_SHIHARAI_MEISAIHYOU_KOTEI,
        /// <summary>G574 出荷明細表出力（固定帳票）</summary>
        [WindowTitle("出荷明細表 出力画面")]
        T_SHUKKA_MEISAIHYOU_KOTEI,
        /// <summary>G576 伝票確定入力</summary>
        [WindowTitle("伝票確定")]
        T_DENPYOU_KAKUTEI,
        /// <summary>G577 売上集計表</summary>
        [WindowTitle("売上集計表")]
        T_URIAGE_SHUUKEIHYOU = 182,
        /// <summary>G579 支払集計表</summary>
        [WindowTitle("支払集計表")]
        T_SHIHARAI_SHUUKEIHYOU = 183,
        #endregion
        #region 売上管理
        /// <summary>G059 売上確定入力</summary>
        [WindowTitle("売上確定")]
        T_URIAGE_KAKUTEI,
        /// <summary>G064 売上元帳</summary>
        [WindowTitle("売上元帳")]
        T_URIAGE_MOTOCHO,
        /// <summary>G065 売掛金一覧表</summary>
        [WindowTitle("売掛金一覧表")]
        T_URIGAKEKIN_ICHIRAN,
        #endregion
        #region 運賃
        /// <summary>G153 運賃入力</summary>
        [WindowTitle("運賃入力")]
        T_UNCHIN_NYUURYOKU,
        /// <summary>G155 運賃一覧表</summary>
        [WindowTitle("運賃一覧表")]
        T_UNCHIN_ICHIRANHYOU,
        /// <summary>G530 運賃集計表条件指定ポップアップ</summary>
        [WindowTitle("運賃集計表条件指定")]
        T_UNCHIN_SYUUKEI_JYOUKEN,
        /// <summary>G642 運賃明細表</summary>
        [WindowTitle("運賃明細表　出力画面")]
        T_UNCHIN_MEISAIHYOU,
        /// <summary>G646 運賃集計表</summary>
        [WindowTitle("運賃集計表")]
        T_UNCHIN_SHUUKEIHYOU = 191,
        /// <summary>G650 運賃台帳</summary>
        [WindowTitle("運賃台帳")]
        T_UNCHIN_DAICHOU,
        #endregion
        #region 営業管理
        /// <summary>G273 予算入力</summary>
        [WindowTitle("予算入力")]
        T_YOSAN_NYUURYOKU,
        /// <summary>G274 予算一覧</summary>
        [WindowTitle("予算一覧")]
        T_YOSAN_ICHIRAN,
        /// <summary>G275 予実管理表</summary>
        [WindowTitle("予実管理表")]
        T_YOJITSU_KANRIHYOU,
        /// <summary>G276 見積入力</summary>
        [WindowTitle("見積入力")]
        T_MITSUMORI_NYUURYOKU,
        /// <summary>G277 見積一覧</summary>
        [WindowTitle("見積一覧")]
        T_MITSUMORI_ICHRAN,
        /// <summary>G278 受注予実管理表</summary>
        [WindowTitle("受注予実管理表")]
        T_JYUCYUU_YOJITSU_KANNRIHYOU,
        /// <summary>
        /// G280 申請一覧
        /// </summary>
        [WindowTitle("申請一覧")]
        T_DENSHI_SHINSEI_ICHIRAN,
        /// <summary>G334 取引履歴一覧</summary>
        [WindowTitle("取引履歴一覧")]
        T_TORIHIKI_RIREKI_ICHIRAN,
        /// <summary>G464 受注目標件数入力</summary>
        [WindowTitle("受注目標件数入力")]
        T_JYUCYUU_MOKUHYUO_KENSUU_NYUURYOKU,
        /// <summary>G279 申請入力</summary>
        [WindowTitle("申請入力")]
        T_DENSHI_SHINSEI_NYUURYOKU,
        /// <summary>G560 申請内容選択入力</summary>
        [WindowTitle("申請内容選択入力")]
        T_DENSHI_SHINSEI_NAIYOU_SENTAKU_NYUURYOKU,
        /// <summary>G613 申請内容確認（業者）</summary>
        [WindowTitle("業者")]
        M_GYOUSHA_KAKUNIN,
        /// <summary>G561 承認済申請一覧</summary>
        [WindowTitle("承認済申請一覧")]
        T_SHOUNINZUMI_DENSHI_SHINSEI_ICHIRAN,
        #endregion
        #region 検収
        /// <summary>G157 検収明細入力</summary>
        [WindowTitle("検収入力")]
        T_KENSYUU_MEISAI_NYUURYOKU,
        /// <summary>G159 検収一覧表</summary>
        [WindowTitle("検収一覧表")]
        T_KENSYUU_ICHIRAN,
        /// <summary>G159-2 検収一覧表 条件指定ポップアップ</summary>
        [WindowTitle("検収一覧表-範囲条件指定")]
        T_KENSYUU_ICHIRAN_JYOUKEN,
        #endregion
        #region 計量
        /// <summary>G045 計量入力</summary>
        [WindowTitle("計量入力")]
        T_KEIRYO,
        /// <summary>G047 計量明細表</summary>
        [WindowTitle("計量明細表")]
        T_KEIRYO_MEISAI,
        /// <summary>G048 計量集計表</summary>
        [WindowTitle("計量集計表")]
        T_KEIRYO_SHUKEI,
        /// <summary>G050 計量票</summary>
        [WindowTitle("計量票")]
        T_KEIRYOHYO,
        #endregion
        #region 在庫
        /// <summary>G165 在庫伝票入力</summary>
        [WindowTitle("在庫伝票入力")]
        T_ZAIKO_DENPYOU_NYUURYOKU,
        /// <summary>G167 在庫調整入力</summary>
        [WindowTitle("在庫調整入力")]
        T_ZAIKO_TYOUSEI_NYUURYOKU,
        /// <summary>G168 在庫調整一覧</summary>
        [WindowTitle("在庫調整一覧")]
        T_ZAIKO_TYOUSEI_ICHIRAN,
        /// <summary>G169 在庫管理表</summary>
        [WindowTitle("在庫管理表")]
        T_ZAIKO_KANRIHYOU,
        /// <summary>G170 在庫締処理</summary>
        [WindowTitle("在庫締処理")]
        T_ZAIKO_SIMESYORI,
        /// <summary>G171 在庫拠点間移動入力</summary>
        [WindowTitle("在庫拠点間移動入力")]
        T_ZAIKO_KYOTENKAN_IDOU_NYUURYOKU,
        /// <summary>G293 物販在庫調整入力</summary>
        [WindowTitle("物販在庫調整入力")]
        T_BUPPAN_ZAIKO_TYOUSEI_NYUURYOKU,
        /// <summary>G294 物販在庫品名状況</summary>
        [WindowTitle("物販在庫品名状況")]
        T_BUPPAN_ZAIKO_SYOUHIN_JYOUKYOU,
        /// <summary>G458 物販在庫一覧</summary>
        [WindowTitle("物販在庫一覧")]
        T_BUPPAN_ZAIKO_ICHIRAN,
        /// <summary>G497 物販在庫締処理</summary>
        [WindowTitle("物販在庫締処理")]
        T_BUPPAN_ZAIKO_SIMESYORI,
        /// <summary>G631 在庫移動入力</summary>
        [WindowTitle("在庫移動入力")]
        T_ZAIKO_IDOU_NYUURYOKU,
        /// <summary>G632 在庫移動一覧</summary>
        [WindowTitle("在庫移動一覧")]
        T_ZAIKO_IDOU_ICHIRAN,
        /// <summary>G633 在庫品名振分</summary>
        [WindowTitle("在庫品名振分")]
        T_ZAIKO_HINMEI_HURIWAKE,
        //Start Sontt #10156 20150425
        /// <summary>G652 開始在庫情報入力</summary>
        [WindowTitle("開始在庫情報入力")]
        M_KAISHI_ZAIKO_INFO,
        //End Sontt #10156 20150425
        #endregion
        #region 支払管理
        /// <summary>G068 支払確定入力</summary>
        [WindowTitle("支払確定入力")]
        T_SHIHARAI_KAKUTEI,
        /// <summary>G073 支払元帳</summary>
        [WindowTitle("支払元帳")]
        T_SHIHARAI_MOTOCHO,
        /// <summary>G074 買掛金一覧表</summary>
        [WindowTitle("買掛金一覧表")]
        T_KAIKAKEKIN_ICHIRANHYOU,
        #endregion
        #region 請求
        /// <summary>G101 請求締処理</summary>
        [WindowTitle("請求締処理")]
        T_SEIKYU_SHIME,
        /// <summary>G102 請求書確認</summary>
        [WindowTitle("請求書確認")]
        T_SEIKYUSHO_KAKUNIN,
        /// <summary>G103 請求一覧</summary>
        [WindowTitle("請求一覧")]
        T_SEIKYU_ICHIRAN,
        /// <summary>G107 請求書発行</summary>
        [WindowTitle("請求書発行")]
        T_SEIKYUSHO_HAKKO,
        /// <summary>G108 請求チェック表</summary>
        [WindowTitle("請求チェック表")]
        T_SEIKYU_CHECK,
        /// <summary>G332 請求締処理ｴﾗｰ一覧</summary>
        [WindowTitle("請求締処理エラー一覧")]
        T_SEIKYU_SHIME_ERROR,
        //Start Sontt #10030 20150415
        /// <summary>G635 請求明細表</summary>
        [WindowTitle("請求明細表 出力画面")]
        T_SEIKYU_MEISAIHYOU_SHUTSURYOKU,
        //End Sontt #10030 20150415
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>G745 INXS請求書発行</summary>
        [WindowTitle("INXS請求書発行")]
        T_INXS_SEIKYUSHO_HAKKO,
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>G747 INXS支払明細書発行</summary>
        [WindowTitle("INXS支払明細書発行")]
        T_INXS_SHIHARAI_MESAISHO_HAKKO,
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
        #endregion
        #region 精算
        /// <summary>G110 支払締処理</summary>
        [WindowTitle("支払締処理")]
        T_SHIHARAI_SHIME,
        /// <summary>G111 支払明細書確認</summary>
        [WindowTitle("支払明細書確認")]
        T_SHIHARAI_MEISAISHO_KAKUNIN,
        /// <summary>G112 支払明細一覧</summary>
        [WindowTitle("支払明細一覧")]
        T_SHIHARAI_ICHIRAN,
        /// <summary>G116 支払明細書発行</summary>
        [WindowTitle("支払明細書発行")]
        T_SHIHARAI_MEISAISHO_HAKKO,
        /// <summary>G117 支払チェック表</summary>
        [WindowTitle("支払チェック表")]
        T_SHIHARAI_CHECK,
        /// <summary>G118 宛名ラベル</summary>
        [WindowTitle("宛名ラベル")]
        T_ATENA_RABERU,
        /// <summary>G333 支払締処理ｴﾗｰ一覧</summary>
        [WindowTitle("支払締処理エラー一覧")]
        T_SHIHARAI_SHIME_ERROR,
        #endregion
        #region 月次
        /// <summary>G617 月次処理</summary>
        [WindowTitle("月次処理")]
        T_GETSUJI,
        /// <summary>G618 月次消費税調整入力（売上）</summary>
        [WindowTitle("月次消費税調整入力（売上）")]
        T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_UR,
        /// <summary>G618 月次消費税調整入力（支払）</summary>
        [WindowTitle("月次消費税調整入力（支払）")]
        T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH,
        #endregion
        #region 代納
        /// <summary>G161 代納入力</summary>
        [WindowTitle("代納入力")]
        T_DAINO,
        /// <summary>G507 代納伝票発行</summary>
        [WindowTitle("伝票発行（代納）")]
        T_DAINO_DENPYO_HAKKOU,
        /// <summary>G531 代納明細表／集計表条件指定ポップアップ</summary>
        [WindowTitle("代納明細表／集計表条件指定ポップアップ")]
        T_DAINO_MEISAI_SYUUKEI_JYOUKEN,
        /// <summary>G541 代納明細表/一覧</summary>
        [WindowTitle("代納明細表/一覧")]
        T_DAINO_MEISAIHYOU,
        /// <summary>G542 代納集計表/一覧</summary>
        [WindowTitle("代納集計表/一覧")]
        T_DAINO_SYUUKEIHYOU,
        /// <summary>G638 代納明細表 出力画面</summary>
        [WindowTitle("代納明細表 出力画面")]
        T_DAINO_MEISAIHYOU_OUTPUT,
        #endregion
        #region 電子マニフェスト
        /// <summary>G141 電子マニフェスト入力</summary>
        [WindowTitle("電子マニフェスト入力")]
        T_DENSHI_MANIFEST,
        /// <summary>G142 送信保留登録情報</summary>
        [WindowTitle("送信保留登録情報")]
        T_SOUSHIN_HORYU_JYOHO,
        /// <summary>G144 運搬終了報告</summary>
        [WindowTitle("運搬終了報告")]
        T_UNPAN_SHURYO,
        /// <summary>G145 処分終了報告</summary>
        [WindowTitle("処分終了報告")]
        T_SYOBUN_SHURYO,
        /// <summary>G146 送信保留最終処分報告</summary>
        [WindowTitle("送信保留最終処分報告")]
        T_SOUSHIN_HORYU_SAISHU_SYOBUN,
        /// <summary>G147 電子マニフェスト補助データ作成</summary>
        [WindowTitle("電子マニフェスト補助データ作成")]
        T_MIHIMODUKE_ICHIRAN,
        /// <summary>G148 ※TOPへの情報公開</summary>
        [WindowTitle("情報公開")]
        T_TOP_JYOUHOU,
        /// <summary>G149 電子マニフェスト(二次)入力</summary>
        [WindowTitle("電子マニフェスト(二次)入力")]
        T_DENSHI_MANIFEST_NIZI,
        /// <summary>G150 電子マニフェスト二次紐付け(廃止</summary>
        [WindowTitle("電子マニフェスト二次紐付け")]
        T_DENSHI_MANIFEST_NIZI_HIMODUKE,
        /// <summary>G151 電子CSV出力(廃止</summary>
        [WindowTitle("電子CSV出力")]
        T_DENSHI_MANIFEST_CSV_OUTPUT,
        /// <summary>G152 電子マニフェストCSV取込</summary>
        [WindowTitle("電子マニフェストCSV取込")]
        T_DENSHI_MANIFEST_CSV_INPUT,
        /// <summary>G306 運搬終了報告情報　一括入力</summary>
        [WindowTitle("運搬終了報告情報　一括入力")]
        T_UNPAN_SHURYO_IKKATSU,
        /// <summary>G307 処分終了報告情報　一括入力</summary>
        [WindowTitle("処分終了報告情報　一括入力")]
        T_SYOBUN_SHURYO_IKKATSU,
        /// <summary>G411 通知履歴照会</summary>
        [WindowTitle("通知履歴照会")]
        T_TUCHI_RIREKI_SHOKAI,
        /// <summary>G412 通信履歴照会</summary>
        [WindowTitle("通信履歴照会")]
        T_TUSHIN_RIREKI_SHOKAI,
        /// <summary>G413 最新情報照会</summary>
        [WindowTitle("最新情報照会")]
        T_SAISHIN_JOHO_SHOKAI,
        /// <summary>G615 混合廃棄物状況一覧</summary>
        [WindowTitle("混合廃棄物状況一覧")]
        T_KONGOU_HAIKIBUTSU_JOUKYOU_ICHIRAN,
        /// <summary>G616 混合廃棄物振分</summary>
        [WindowTitle("混合廃棄物振分")]
        T_KONGOU_HAIKIBUTSU_FURIWAKE,
        #endregion
        #region 入出金管理
        /// <summary>G619 入金入力（取引先）</summary>
        [WindowTitle("入金入力（取引先）")]
        T_NYUKIN_TORIHIKISAKI,
        /// <summary>G077/G459 入金入力(入金先)</summary>
        [WindowTitle("入金入力(入金先)")]
        T_NYUKIN,
        /// <summary>G611 入金消込</summary>
        [WindowTitle("入金消込")]
        T_NYUKIN_KESHIKOMI,
        /// <summary>G620 入金消込修正入力</summary>
        [WindowTitle("入金消込修正入力")]
        T_NYUKIN_KESHIKOMI_SHUSEI,
        /// <summary>G077-2 入金一括入力画面(廃止</summary>
        [WindowTitle("入金一括")]
        T_NYUKIN_IKKATSU,
        /// <summary>G078 入出金一覧</summary>
        [WindowTitle("入出金一覧")]
        T_NYUSHUTSUKIN_ICHIRAN,
        /// <summary>G084 入金消込一覧</summary>
        [WindowTitle("入金消込一覧")]
        T_NYUKIN_KESHIKOMI_RIREKI,
        /// <summary>G085 未入金一覧表</summary>
        [WindowTitle("未入金一覧表")]
        T_MINYUKIN_ICHIRAN,
        /// <summary>G086 入金予定一覧表</summary>
        [WindowTitle("入金予定一覧表")]
        T_NYUKIN_YOTEI_ICHIRAN,
        /// <summary>G089 受取手形入力</summary>
        [WindowTitle("受取手形入力")]
        T_UKETORI_TEGATA,
        /// <summary>G090/G460 出金入力画面</summary>
        [WindowTitle("出金入力")]
        T_SHUKKIN,
        /// <summary>G096 出金消込履歴一覧画面</summary>
        [WindowTitle("出金消込履歴一覧")]
        T_SYUKKIN_KESHIKOMI_RIREKI,
        /// <summary>G097 未払金一覧表画面</summary>
        [WindowTitle("未払金一覧表")]
        T_MISHUKKIN,
        /// <summary>G098 出金予定一覧表画面</summary>
        [WindowTitle("出金予定一覧表")]
        T_SYUKKIN_YOTEI_ICHIRAN,
        /// <summary>G100 支払手形入力画面</summary>
        [WindowTitle("支払手形入力")]
        T_SHIHARAI_TEGATA,
        /// <summary>G082 入金明細消込入力(廃止</summary>
        [WindowTitle("入金明細消込入力")]
        T_NYUUKIN_MEISAI_KESHIKOMI,
        /// <summary>G088 滞留債権年齢表</summary>
        [WindowTitle("滞留債権年齢表")]
        T_TAIRYUU_SAIKEN_NENREIHYOU,
        /// <summary>G094 出金明細消込入力(廃止</summary>
        [WindowTitle("出金明細消込入力")]
        T_SYUKKIN_MEISAI_KESHIKOMI,
        /// <summary>G587 入金明細表</summary>
        [WindowTitle("入金明細表 出力画面")]
        T_NYUUKIN_MEISAIHYOU,
        /// <summary>G591 出金明細表</summary>
        [WindowTitle("出金明細表 出力画面")]
        T_SHUKKIN_MEISAIHYOU,
        /// <summary>G597 未入金一覧</summary>
        [WindowTitle("未入金一覧表 出力画面")]
        T_MINYUUKIN_ICHIRAN,
        /// <summary>G598 入金予定一覧</summary>
        [WindowTitle("入金予定一覧表 出力画面")]
        T_NYUUKIN_YOTEI_ICHIRAN,
        #endregion
        #region 配車
        /// <summary>G026 配車割当（一日）入力</summary>
        [WindowTitle("配車割当（一日）入力")]
        T_HAISHA_WARIATE_DAY,
        /// <summary>G027 作業日変更</summary>
        [WindowTitle("作業日変更")]
        T_SAGYOBI_HENKO,
        /// <summary>G029 配車割当（案件）入力</summary>
        [WindowTitle("配車割当（案件）入力")]
        T_HAISHA_WARIATE_ANKEN,
        /// <summary>G030 定期配車入力</summary>
        [WindowTitle("定期配車入力")]
        T_TEIKI_HAISHA,
        /// <summary>G031 コース配車依頼入力</summary>
        [WindowTitle("コース配車依頼入力")]
        T_COURSE_HAISHA_IRAI,
        /// <summary>G032 定期配車一覧画面</summary>
        [WindowTitle("定期配車一覧")]
        T_TEIKI_HAISHA_ICHIRAN,
        /// <summary>G033 配車明細表</summary>
        [WindowTitle("配車明細表")]
        T_HAISHA_MEISAI,
        /// <summary>G037 作業指示書(廃止</summary>
        [WindowTitle("作業指示書")]
        T_SAGYO_SIZISHO,
        /// <summary>G041 設置コンテナ一覧</summary>
        [WindowTitle("設置コンテナ一覧")]
        T_CONTENA_ICHIRAN,
        /// <summary>G042 コンテナ履歴(廃止</summary>
        [WindowTitle("コンテナ履歴")]
        T_CONTENA_RIREKI,
        /// <summary>G044 定期配車一括作成画面</summary>
        [WindowTitle("定期配車一括作成")]
        T_TEIKI_HAISHA_IKKATSU,
        /// <summary>G202 運転者休動入力</summary>
        [WindowTitle("運転者休動入力")]
        T_UNTENSYA_KYUUDOU_NYUURYOKU,
        /// <summary>G203 荷降先休動入力</summary>
        [WindowTitle("荷降先休動入力")]
        T_HANNYUUSAKI_KYUUDOU_NYUURYOKU,
        /// <summary>G208 車輌休動入力</summary>
        [WindowTitle("車輌休動入力")]
        T_SYARYOU_KYUUDOU_NYUURYOKU,
        /// <summary>G289 定期配車実績入力画面</summary>
        [WindowTitle("定期配車実績入力")]
        T_TEIKIHAISHA_ZISSEKI,
        /// <summary>G290 定期配車実績一覧画面</summary>
        [WindowTitle("定期配車実績一覧")]
        T_TEIKIHAISHA_ZISSEKI_ICHIRAN,
        /// <summary>G300 定期配車実績表</summary>
        [WindowTitle("定期配車実績表")]
        T_TEIKIHAISHA_ZISSEKI_HYOU,
        /// <summary>G300 定期配車実績表(月報)</summary>
        [WindowTitle("定期配車実績表(月報)")]
        T_TEIKIHAISHA_ZISSEKI_HYOU_GEPPOU,
        /// <summary>G301 定期配車実績表(年報)</summary>
        [WindowTitle("定期配車実績表(年報)")]
        T_TEIKIHAISHA_ZISSEKI_HYOU_NENPOU,
        /// <summary>G330 実績売上支払確定画面</summary>
        [WindowTitle("実績売上支払確定")]
        T_ZISSEKI_URIAGE_SHIHARAI_KAKUTEI,
        /// <summary>G593 待機コンテナ一覧表画面</summary>
        [WindowTitle("待機コンテナ一覧表")]
        T_KARA_CONTENA_ICHIRAN_HYOU,
        /// <summary>G595 コンテナ履歴一覧表画面</summary>
        [WindowTitle("コンテナ履歴一覧表")]
        T_CONTENA_RIREKI_ICHIRAN_HYOU,
        /// <summary>G601 モバイル通信設定画面</summary>
        [WindowTitle("モバイル通信設定")]
        T_MOBILE_TSUUSHINSETTEI,
        /// <summary>G602 定期実績CSV出力画面</summary>
        [WindowTitle("定期実績CSV出力")]
        T_TEIKI_JISSEKI_HOUKOKU,
        #endregion
        #region マニフェスト
        /// <summary>G119 産廃(直行)マニフェスト入力画面</summary>
        [WindowTitle("産廃マニフェスト(直行)入力")]
        T_SANPAI_MANIFEST,
        /// <summary>G120 産廃(積替)マニフェスト入力画面</summary>
        [WindowTitle("産廃マニフェスト(積替)入力")]
        T_TUMIKAE_MANIFEST,
        /// <summary>G121 建廃マニフェスト入力画面</summary>
        [WindowTitle("建廃マニフェスト入力")]
        T_KENPAI_MANIFEST,
        /// <summary>G122 マニフェスト入力（一括入力）画面</summary>
        [WindowTitle("マニフェスト入力（一括入力）")]
        T_MANIFEST_IKKATU,
        /// <summary>G123 マニフェスト紐付画面</summary>
        [WindowTitle("マニフェスト紐付")]
        T_MANIFEST_HIMODUKE,
        /// <summary>G124 マニフェストチェック表画面</summary>
        [WindowTitle("マニフェストチェック表")]
        T_MANIFEST_CHECK,
        /// <summary>G126 マニフェスト一覧画面</summary>
        [WindowTitle("マニフェスト一覧")]
        T_MANIFEST_ICHIRAN,
        /// <summary>G127 マニフェスト明細表画面</summary>
        [WindowTitle("マニフェスト明細表")]
        T_MANIFEST_MEISAI,
        /// <summary>G128 マニフェスト推移表</summary>
        [WindowTitle("マニフェスト推移表")]
        T_MANIFEST_SUII,
        /// <summary>G131 交付等状況報告書画面</summary>
        [WindowTitle("交付等状況報告書")]
        T_KOUHUJYOKYO_HOKOKUSHO,
        /// <summary>G133 廃棄物帳簿画面</summary>
        [WindowTitle("廃棄物帳簿")]
        T_HAIKIBUTU_CHOBO,
        /// <summary>G137 返却日入力／一覧画面</summary>
        [WindowTitle("返却日入力／一覧")]
        T_HENKYAKUBI,
        /// <summary>G138 宛名ラベル(マニフェスト)</summary>
        [WindowTitle("宛名ラベル(マニフェスト)")]
        T_MANIFEST_ATENA,
        /// <summary>G299 マニパターン一覧画面</summary>
        // 20140529 syunrei No.730 マニフェストパターン一覧 start
        //[WindowTitle("マニフェストパターン一覧")]
        [WindowTitle("パターン一覧")]
        // 20140529 syunrei No.730 マニフェストパターン一覧 end
        T_PATTERN_ICHIRAN,
        /// <summary>G324 返送案内書画面</summary>
        [WindowTitle("返送案内書")]
        T_HENSO_ANNAI,
        /// <summary>G465 マニフェスト換算値再計算</summary>
        [WindowTitle("マニフェスト換算値再計算")]
        T_MANIFEST_KANSANTI_SAIKEISAN,
        /// <summary>G589 マニフェスト紐付一覧画面</summary>
        [WindowTitle("マニフェスト紐付一覧")]
        T_MANIFEST_HIMODUKEICHIRAN,
        /// <summary>G511 交付等状況報告書画面</summary>
        [WindowTitle("交付等状況報告書一覧")]
        T_KOUHUJYOKYO_ICHIRAN,
        /// <summary>G134 実績報告書（処分実績）</summary>
        [WindowTitle("実績報告書（処分実績）")]
        T_JISSEKIHOKOKU,
        /// <summary>G135 実績報告書一覧</summary>
        [WindowTitle("実績報告書一覧")]
        T_JISSEKIHOKOKU_ICHIRAN,
        /// <summary>G603 実績報告書（処理施設）</summary>
        [WindowTitle("実績報告書（処理施設）")]
        T_JISSEKIHOKOKU_SISETSU,
        /// <summary>G606 実績報告書（運搬実績）</summary>
        [WindowTitle("実績報告書（運搬実績）")]
        T_JISSEKIHOKOKU_UNPAN,
        /// <summary>G609 CSV出力項目選択（処分実績）</summary>
        [WindowTitle("CSV出力項目選択（処分実績）")]
        T_JISSEKIHOKOKU_CSV,
        /// <summary>G610 CSV出力項目選択（運搬実績）</summary>
        [WindowTitle("CSV出力項目選択（運搬実績）")]
        T_JISSEKIHOKOKU_UNPAN_CSV,
        /// <summary>G136 実績報告書修正</summary>
        [WindowTitle("実績報告書修正（処分実績）")]
        T_JISSEKIHOKOKU_SYUSEI_1,
        /// <summary>G136 実績報告書修正</summary>
        [WindowTitle("実績報告書修正（処理施設実績）")]
        T_JISSEKIHOKOKU_SYUSEI_2,
        /// <summary>G136 実績報告書修正</summary>
        [WindowTitle("実績報告書修正（運搬実績）")]
        T_JISSEKIHOKOKU_SYUSEI_3,
        #endregion
        #region オプション
        /// <summary>G281 オンラインバンク連携（入金データ取込）</summary>
        [WindowTitle("オンラインバンク連携（取込）")]
        T_ONLINE_BANK_RENKEI_TORIKOMI,
        /// <summary>G481 オンラインバンク連携（入金データ消込）</summary>
        [WindowTitle("オンラインバンク連携（消込）")]
        T_ONLINE_BANK_RENKEI_KESHIKOMI,
        /// <summary>G282 財務連携</summary>
        [WindowTitle("財務連携")]
        T_ZAIMU_RENKEI,
        /// <summary>G283 モバイル将軍取込</summary>
        [WindowTitle("モバイル将軍用データ取込")]
        T_MOBILE_SHOUGUN_TORIKOMI,
        /// <summary>G284 モバイル将軍出力</summary>
        [WindowTitle("モバイル将軍用データ出力")]
        T_MOBILE_SHOUGUN_SHUTSURYOKU,
        /// <summary>G286 デジタコ連携</summary>
        [WindowTitle("デジタコ連携")]
        T_DEJITAKO_RENKEI,
        /// <summary>G287 モバイル将軍取込（定期）</summary>
        [WindowTitle("モバイル将軍用データ取込（定期）")]
        T_MOBILE_SHOUGUN_TORIKOMI_TEIKI,
        /// <summary>G287 モバイル将軍取込（スポット）</summary>
        [WindowTitle("モバイル将軍用データ取込（スポット）")]
        T_MOBILE_SHOUGUN_TORIKOMI_SPOT,
        #endregion

        #region マスタ
        /// <summary>部署保守画面</summary>
        M_BUSHO,
        /// <summary>単位保守画面</summary>
        M_UNIT,
        /// <summary>銀行保守画面</summary>
        M_BANK,
        /// <summary>業種保守画面</summary>
        M_GYOUSHU,
        /// <summary>コンテナ種類保守画面</summary>
        M_CONTENA_SHURUI,
        /// <summary>拠点保守画面</summary>
        M_KYOTEN,
        /// <summary>入出金保守画面</summary>
        M_NYUUSHUKKIN_KBN,
        /// <summary>都道府県保守画面</summary>
        M_TODOUFUKEN,
        /// <summary>車種保守画面</summary>
        M_SHASHU,
        /// <summary>分類保守画面</summary>
        M_BUNRUI,
        /// <summary>地域保守画面</summary>
        M_CHIIKI,
        /// <summary>処分方法保守画面</summary>
        M_SHOBUN_HOUHOU,
        /// <summary>処分目的保守画面</summary>
        M_SHOBUN_MOKUTEKI,
        /// <summary>運搬方法保守画面</summary>
        M_UNPAN_HOUHOU,
        /// <summary>容器保守画面</summary>
        M_YOUKI,
        /// <summary>形態区分保守画面</summary>
        M_KEITAI_KBN,
        /// <summary>社員保守画面</summary>
        M_SHAIN,
        /// <summary>営業担当者保守画面</summary>
        M_EIGYOU_TANTOUSHA,
        /// <summary>運転者保守画面</summary>
        M_UNTENSHA,
        /// <summary>処分担当者保守画面</summary>
        M_SHOBUN_TANTOUSHA,
        /// <summary>手形保管者保守画面</summary>
        M_TEGATA_HOKANSHA,
        /// <summary>入力担当者保守画面</summary>
        M_NYUURYOKU_TANTOUSHA,
        /// <summary>INXS担当者保守画面</summary>
        M_INXS_TANTOUSHA,
        /// <summary>伝票区分保守画面</summary>
        M_DENPYOU_KBN,
        /// <summary>マニフェスト種類保守画面</summary>
        M_MANIFEST_SHURUI,
        /// <summary>マニフェスト手配保守画面</summary>
        M_MANIFEST_TEHAI,
        /// <summary>現着時間保守画面</summary>
        M_GENCHAKU_TIME,
        /// <summary>コンテナ状況保守画面</summary>
        M_CONTENA_JOUKYOU,
        /// <summary>コンテナ操作保守画面</summary>
        M_CONTENA_SOUSA,
        /// <summary>形状保守画面</summary>
        M_KEIJOU,
        /// <summary>荷姿保守画面</summary>
        M_NISUGATA,
        /// <summary>報告書分類保守画面</summary>
        M_HOUKOKUSHO_BUNRUI,
        /// <summary>報告書分類保守運搬画面</summary>
        M_HOUKOKUSHO_BUNRUI_UNPAN,
        /// <summary>廃棄物名称保守画面</summary>
        M_HAIKI_NAME,
        /// <summary>廃棄物区分保守画面</summary>
        M_HAIKI_KBN,
        /// <summary>種類保守画面</summary>
        M_SHURUI,
        /// <summary>市区町村保守画面</summary>
        M_SHIKUCHOUSON,
        /// <summary>有害物質保守画面</summary>
        M_YUUGAI_BUSSHITSU,
        /// <summary>取引区分保守画面</summary>
        M_TORIHIKI_KBN,
        /// <summary>部門保守画面</summary>
        M_BUMON,
        /// <summary>銀行支店</summary>
        M_BANK_SHITEN,
        /// <summary>集計項目保守画面</summary>
        M_SHUUKEI_KOUMOKU,
        /// <summary>減溶率保守画面</summary>
        M_GENNYOURITSU,
        /// <summary>品名保守画面</summary>
        M_HINMEI,
        /// <summary>申請経路名入力画面</summary>
        M_DENSHI_SHINSEI_KEIRO_NAME,
        /// <summary>伝種区分保守画面</summary>
        M_DENSHU_KBN,
        /// <summary>車輌保守画面</summary>
        M_SHARYOU,
        /// <summary>消費税保守画面</summary>
        M_SHOUHIZEI,
        /// <summary>基本品名単価保守画面</summary>
        M_KIHON_HINMEI_TANKA,
        /// <summary>個別品名単価保守画面</summary>
        M_KOBETSU_HINMEI_TANKA,
        /// <summary>コース名保守画面</summary>
        M_COURSE_NAME,
        /// <summary>計量調整保守画面</summary>
        M_KEIRYOU_CHOUSEI,
        /// <summary>換算値保守画面</summary>
        M_KANSAN,
        /// <summary>マニフェスト換算値保守画面</summary>
        M_MANIFEST_KANSAN,
        /// <summary>混合種類保守画面</summary>
        M_KONGOU_SHURUI,
        /// <summary>混合廃棄物保守画面</summary>
        M_KONGOU_HAIKIBUTSU,
        /// <summary>廃棄物種類保守画面</summary>
        M_HAIKI_SHURUI,
        /// <summary>運転者休動保守画面</summary>
        M_WORK_CLOSED_UNTENSHA,
        /// <summary>車輛休動保守画面</summary>
        M_WORK_CLOSED_SHARYOU,
        /// <summary>荷降先休動保守画面</summary>
        M_WORK_CLOSED_HANNYUUSAKI,
        /// <summary>メニュー権限入力（メニュー毎）保守画面</summary>
        M_MENU_AUTH_EACH_MENU,
        /// <summary>メニュー権限入力（社員毎）保守画面</summary>
        M_MENU_AUTH_EACH_SHAIN,
        /// <summary>メニュー権パターン一覧画面</summary>
        M_MENU_AUTH_PATTERN_ICHIRAN,
        /// <summary>自社情報入力保守画面</summary>
        M_CORP_INFO,
        /// <summary>出金先入力保守画面</summary>
        M_SYUKKINSAKI,
        /// <summary>取引先入力保守画面</summary>
        M_TORIHIKISAKI,
        /// <summary>業者一覧画面</summary>
        M_GYOUSHA_ICHIRAN,
        /// <summary>現場入力保守画面</summary>
        M_GENBA,
        /// <summary>システム設定入力保守画面</summary>
        M_SYS_INFO,
        /// <summary>取引先一覧画面</summary>
        M_TORIHIKISAKI_ICHIRAN,
        /// <summary>業者入力保守画面</summary>
        M_GYOUSHA,
        /// <summary>現場一覧画面</summary>
        M_GENBA_ICHIRAN,
        /// <summary>入金先入力保守画面</summary>
        M_NYUUKINSAKI,
        /// <summary>コンテナ保守画面</summary>
        M_CONTENA,
        /// <summary>委託契約画面</summary>
        M_ITAKU_KEIYAKU,
        /// <summary>委託契約書許可証期限管理画面</summary>
        M_ITAKU_KEIYAKU_KIGEN,
        /// <summary>委託契約書一覧画面</summary>
        M_ITAKU_KEIYAKU_ICHIRAN,
        /// <summary>覚書一括入力画面</summary>
        M_OBOE_IKKATSU,
        /// <summary>会社休日保守画面</summary>
        M_KAISHA_KYUJITSU,
        /// <summary>月極単価保守画面</summary>
        M_TSUKIGIME_TANKA,
        /// <summary>現場定期保守画面</summary>
        M_GENBA_TEIKI,
        /// <summary>現着時間入力画面</summary>
        M_GENBACHAKU_TIME,
        /// <summary>コース保守画面</summary>
        M_COURSE,
        /// <summary>地域別許可番号入力画面</summary>
        M_CHIIKIBETSU_KYOKA,
        /// <summary>地域別許可番号入力（運搬）画面</summary>
        M_CHIIKIBETSU_KYOKA_UPN,
        /// <summary>地域別許可番号入力（処分）画面</summary>
        M_CHIIKIBETSU_KYOKA_SBN,
        /// <summary>地域別許可番号入力（最終処分）画面</summary>
        M_CHIIKIBETSU_KYOKA_LAST_SBN,
        /// <summary>地域別業種保守画面</summary>
        M_CHIIKIBETSU_GYOUSHU,
        /// <summary>地域別施設保守画面</summary>
        M_CHIIKIBETSU_SHISETSU,
        /// <summary>地域別住所保守画面</summary>
        M_CHIIKIBETSU_JUSHO,
        /// <summary>地域別処分保守画面</summary>
        M_CHIIKIBETSU_SHOBUN,
        /// <summary>地域別分類保守画面</summary>
        M_CHIIKIBETSU_BUNRUI,
        /// <summary>在庫商品保守画面</summary>
        M_ZAIKO_SHOHIN,
        /// <summary>M244 在庫品名入力</summary>
        [WindowTitle("在庫品名入力")]
        M_ZAIKO_HINMEI,
        /// <summary>在庫比率保守画面</summary>
        M_ZAIKO_HIRITSU,
        /// <summary>郵便辞書メンテナンス画面</summary>
        M_YUBIN_MAINTENANCE,
        /// <summary>利用履歴管理画面</summary>
        M_RIYOU_RIREKI,
        /// <summary>物販在庫持出場所保守画面</summary>
        M_BUPPAN_ZAIKO_BASHO,
        /// <summary>物販在庫商品保守画面</summary>
        M_BUPPAN_ZAIKO_SHOHIN,
        /// <summary>M296 物販在庫品名入力</summary>
        [WindowTitle("物販在庫品名入力")]
        M_BUPPAN_ZAIKO_HINMEI,
        /// <summary>営業担当者一括更新画面</summary>
        M_EIGYOU_TANTOUSHA_IKKATSU,
        /// <summary>銀行一括更新画面</summary>
        M_BANK_IKKATSU,
        /// <summary>加入者保守画面</summary>
        M_KANYUSHA,
        /// <summary>事業者保守画面</summary>
        M_ZIGYOSHA,
        /// <summary>事業者一覧画面</summary>
        M_ZIGYOSHA_ICHIRAN,
        /// <summary>事業者未紐付一覧画面</summary>
        M_ZIGYOSHA_MIHIMOZUKE_ICHIRAN,
        /// <summary>事業場保守画面</summary>
        M_ZIGYOBA,
        /// <summary>事業場一覧画面</summary>
        M_ZIGYOBA_ICHIRAN,
        /// <summary>事業場未紐付一覧画面</summary>
        M_ZIGYOBA_MIHIMOZUKE_ICHIRAN,
        /// <summary>担当者保守画面</summary>
        M_TANTOUSHA,
        /// <summary>換算値保守画面</summary>
        M_DM_KANSAN,
        /// <summary>減容率保守画面</summary>
        M_DM_GENNYOURITSU,
        /// <summary>業者台帳画面</summary>
        M_GYOUSHA_DAICHO,
        /// <summary>現場台帳画面</summary>
        M_GENBA_DAICHO,
        /// <summary>取引先台帳画面</summary>
        M_TORIHIKISAKI_DAICHO,
        /// <summary>最終処分場所パターン一覧画面</summary>
        M_SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN,
        /// <summary>覚書一括入力一覧画面</summary>
        M_OBOE_IKKATSU_ICHIRAN,
        /// <summary>単価一括変更画面</summary>
        M_TANKA_IKKATSU,
        /// <summary>(電子)事業者入力画面</summary>
        M_DENSHI_JIGYOUSHA,
        /// <summary>(電子)事業場入力画面</summary>
        M_DENSHI_JIGYOUJOU,
        /// <summary>(電子)担当者入力画面</summary>
        M_DENSHI_TANTOUSHA,
        /// <summary>(電子)有害物質保守画面</summary>
        M_DENSHI_YUUGAI_BUSSHITSU,
        /// <summary>(電子)廃棄物種類保守画面</summary>
        M_DENSHI_HAIKI_SHURUI,
        /// <summary>(電子)廃棄物種類細分類保守画面</summary>
        M_DENSHI_HAIKI_SHURUI_SAIBUNRUI,
        /// <summary>(電子)廃棄物名称保守画面</summary>
        M_DENSHI_HAIKI_NAME,
        /// <summary>(電子)事業者未紐付一覧画面</summary>
        M_DENSHI_JIGYOUSHA_MIHIMODUKE_ICHIRAN,
        /// <summary>(電子)事業場未紐付一覧画面</summary>
        M_DENSHI_JIGYOUJOU_MIHIMODUKE_ICHIRAN,
        /// <summary>G461 引合取引先入力保守画面</summary>
        [WindowTitle("引合取引先入力")]
        M_HIKIAI_TORIHIKISAKI_NYUURYOKU,
        /// <summary>G612 申請内容確認（取引先）</summary>
        [WindowTitle("申請内容確認（取引先）")]
        M_TORIHIKISAKI_KAKUNIN,
        /// <summary>G462 引合業者入力保守画面</summary>
        [WindowTitle("引合業者入力")]
        M_HIKIAI_GYOUSHA_NYUURYOKU,
        /// <summary>G463 引合現場入力保守画面</summary>
        [WindowTitle("引合現場入力")]
        M_HIKIAI_GENBA_NYUURYOKU,
        //G614 start
        /// <summary>G614  申請内容確認（現場）画面</summary>
        [WindowTitle("申請内容確認（現場）")]
        M_GENBA_KAKUNIN,
        //G614 end
        /// <summary>物販在庫保管場所入力</summary>
        [WindowTitle("物販在庫保管場所入力")]
        M_BUPPAN_ZAIKO_HOKANBASHO,
        /// <summary>G423 個別品名単価一括変更</summary>
        [WindowTitle("個別品名単価一括変更")]
        M_KOBETSU_HINMEI_TANKA_IKKATSU,
        /// <summary>G428 伝票単価一括変更</summary>
        [WindowTitle("伝票単価一括変更")]
        M_DENPYOU_TANKA_IKKATSU,
        /// <summary>G498 物販在庫換算値入力</summary>
        [WindowTitle("物販在庫換算値入力")]
        M_BUPPAN_ZAIKO_KANSAN,
        //558
        /// <summary>M557 申請経路名入力</summary>
        [WindowTitle("申請経路名入力")]
        M_DENSHI_SHINSEI_ROUTE_NAME,
        /// <summary>M558 申請経路入力</summary>
        [WindowTitle("申請経路入力")]
        M_DENSHI_SHINSEI_ROUTE,
        //558
        /// <summary>G590 入力項目設定</summary>
        [WindowTitle("入力項目設定")]
        M_TAB_ORDER,
        /// <summary>G559 重要度入力</summary>
        [WindowTitle("重要度入力")]
        M_DENSHI_SHINSEI_JYUYOUDO,
        /// <summary>電子申請内容名</summary>
        M_DENSHI_SHINSEI_NAIYOU,
        /// <summary>運転者休動</summary>
        M_SHAIN_CLOSED,
        /// <summary>車輌休動</summary>
        M_SHARYOU_CLOSED,
        /// <summary>荷降先休動</summary>
        M_GENBA_CLOSED,

        M_TORIHIKISAKI_ALL,
        M_GYOUSHA_ALL,
        M_GENBA_ALL,
 

        // 20150610 マイメニュー追加 Start
        /// <summary></summary>
        [WindowTitle("マイメニュー選択")]
        M_BOOKMARK,
        // 20150610 マイメニュー追加 End
        // 20150625 一般廃用報告書分類 Start
        /// <summary></summary>
        [WindowTitle("実績分類入力")]
        M_JISSEKI_BUNRUI,
        // 20150625 一般廃用報告書分類 End

        #endregion
        #region マスタ-委託契約機能追加
        M_ITAKU_KEIYAKU_SANPAI,
        M_ITAKU_KEIYAKU_KENPAI,
        /// <summary>運賃単価入力</summary>
        M_UNCHIN_TANKA,
        /// <summary>運賃品名入力</summary>
        M_UNCHIN_HINMEI,
        #endregion

        #region Ver 1.19 機能追加
        /// <summary>実績分類入力</summary>
        M_IPPANPAIYOU_HOUKOKUSHO_BUNRUI,
        /// <summary>伝票連携状況一覧</summary>
        T_DENPYOU_RENKEI_JOUKYOU_ICHIRAN,
        /// <summary>M653 電子マニフェスト換算値入力</summary>
        [WindowTitle("電子マニフェスト換算値入力")]
        M_DENSHI_MANIFEST_KANSAN,
        /// <summary>M662 個別品名入力</summary>
        [WindowTitle("個別品名入力")]
        M_KOBETSU_HINMEI,
        #endregion

        #region Ver2.0 機能追加
        /// <summary>G658 受付明細表</summary>
        [WindowTitle("受付明細表 出力画面")]
        T_UKETSUKE_MEISAIHYO,
        /// <summary>G660 支払明細明細表</summary>
        [WindowTitle("支払明細明細表 出力画面")]
        T_SHIHARAI_MEISAI_MEISAIHYO,
        /// <summary>G667 モバイル状況一覧</summary>
        [WindowTitle("モバイル状況一覧")]
        T_MOBILE_JOUKYOU_ICHIRAN,
        /// <summary>G668 モバイル状況詳細</summary>
        [WindowTitle("モバイル状況詳細")]
        T_MOBILE_JOUKYOU_SHOUSAI,
        /// <summary>G744 他社振替登録（スポット）</summary>
        [WindowTitle("他社振替登録（スポット）")]
        T_MOBILE_TASHA_SUPOTTO,
        /// <summary>G743 他社振替登録（定期）</summary>
        [WindowTitle("他社振替登録（定期）")]
        T_MOBILE_TASHA_TEIKI,
        /// <summary>G732 履歴データ削除</summary>
        [WindowTitle("履歴データ削除")]
        RIREKI_DEUTA_SAKUJO,
        #endregion

        #region Ver2.1 機能追加
        /// <summary>M663 コース一覧</summary>
        M_COURSE_ICHIRAN,
        /// <summary>G664 荷降No設定</summary>
        [WindowTitle("荷降No設定")]
        C_NIOROSHI_NO_SETTEI,

        /// <summary>G665 汎用CSV出力</summary>
        [WindowTitle("汎用CSV出力")]
        T_HANYO_CSV_SHUTSURYOKU,

        /// <summary>G665 CSV出力項目選択</summary>
        /// <remarks>子画面</remarks>
        [WindowTitle("CSV出力項目選択")]
        T_HANYO_CSV_OUTPUT_KOUMOKU,

        [WindowTitle("CTI連携設定")]
        T_CTI_RENKEI_SETTEI,
        #endregion

        #region Ver2.2 機能追加
        /// <summary>M669 コンテナQR発行</summary>
        [WindowTitle("コンテナQR発行")]
        M_CONTENA_QR_HAKKOU,
        /// <summary>G668 コンテナ状況詳細</summary>
        [WindowTitle("コンテナ状況詳細")]
        T_CONTENA_JOUKYOU_SHOUSAI,
        /// <summary>G671 マニフェストデータインポート</summary>
        [WindowTitle("マニフェストデータインポート")]
        T_MANIFEST_DATA_IMPORT,
        #endregion

        #region Ver2.6 機能追加
        /// <summary>G672 計量入力</summary>
        [WindowTitle("計量入力")]
        T_KEIRYOU_NYUURYOKU,
        /// <summary>G674 計量報告</summary>
        [WindowTitle("計量報告")]
        T_KEIRYOU_HOUKOKU,
        #region Ver2.6 機能追加
        /// <summary>システム個別設定入力保守画面</summary>
        M_SYS_KOBETSU_INFO,
        #endregion

        #endregion

        #region Ver2.8 機能追加
        /// <summary>M689 デジタコマスタ連携</summary>
        [WindowTitle("デジタコマスタ連携")]
        T_DEJITAKO_MASTER_RENKEI,
        /// <summary></summary>
        [WindowTitle("外部連携現場入力")]
        M_GENBA_DIGI,
        /// <summary>G695 配送計画入力</summary>
        [WindowTitle("配送計画入力")]
        T_HAISOU_KEIKAKU_NYUURYOKU,
        /// <summary>G696 配送計画一覧</summary>
        [WindowTitle("配送計画一覧")]
        T_HAISOU_KEIKAKU_ICHIRAN,
        #endregion

        #region Ver2.9 機能追加
        /// <summary>M690 NAVITIMEマスタ連携</summary>
        [WindowTitle("NAVITIMEマスタ連携")]
        M_NAVI_TIME_MASTER_RENKEI,
        /// <summary>G697 配車計画(NAVITIME)</summary>
        [WindowTitle("配車計画(NAVITIME)")]
        T_HAISOU_KEIKAKU_TEIKI,
        /// <summary>G698 コース最適化入力</summary>
        [WindowTitle("コース最適化入力")]
        T_COURSE_SAITEKIKA_NYUURYOKU,
        /// <summary>G715 電子契約入力</summary>
        [WindowTitle("電子契約入力")]
        T_DENSHI_KEIYAKU_NYUURYOKU,

        /// <summary>G716 電子契約履歴一覧</summary>
        [WindowTitle("電子契約履歴一覧")]
        T_DENSHI_KEIYAKU_RIREKI_ICHIRAN,

        /// <summary>M717 クライアントID入力</summary>
        [WindowTitle("クライアントID入力")]
        M_DENSHI_KEIYAKU_CLIENT_ID,

        /// <summary>M718 社内経路名入力（電子）</summary>
        [WindowTitle("社内経路名入力（電子）")]
        M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME,

        /// <summary>M719 社内経路入力（電子）</summary>
        [WindowTitle("社内経路入力（電子）")]
        M_DENSHI_KEIYAKU_SHANAI_KEIRO,

        #endregion

        // 20170831 katen #108061 伝票一括更新 start
        /// <summary>G684 伝票一括更新</summary>
        [WindowTitle("伝票一括更新")]
        T_DENPYOU_IKKATU_UPDATE,
        /// <summary>G685 伝票明細一括更新</summary>
        [WindowTitle("伝票明細一括更新")]
        T_DENPYOU_DETAIL_IKKATU_UPDATE,
        // 20170831 katen #108061 伝票一括更新 end

        /// <summary>G999 個別品名単価一括変更</summary>
        [WindowTitle("個別品名単価一括変更")]
        M_KOBETSU_HUNNMETANNKA_YIKKATSU,
        /// <summary>G686 マニフェスト終了日一括更新</summary>
        [WindowTitle("マニフェスト終了日一括更新")]
        T_MANIFEST_IKKATSUKOUSIN,
        /// <summary>G687 紐付1次最終処分終了報告</summary>
        [WindowTitle("紐付1次最終処分終了報告")]
        T_DENMANI_SAISHUSHOBUN,

        /// <summary>地域一括更新画面</summary>
        [WindowTitle("地域一括更新")]
        M_CHIIKI_IKKATSU,
        /// <summary>G713 マニフェスト実績一覧画面</summary>
        [WindowTitle("マニフェスト実績一覧")]
        T_MANIFEST_JISSEKI_ICHIRAN,

        #region Ver2.13 ものづくり機能追加
        /// <summary>品名検索画面（伝票用）</summary>
        M_HINMEI_SEARCH,
        #endregion

        #region Ver2.12 機能追加
        /// <summary>M720 画面制御入力</summary>
        [WindowTitle("画面制御入力")]
        M_SHAIN_MAX_WINDOW,
        #endregion

        #region Ver2.18 機能追加

        /// <summary>M730 ファイルアップロード</summary>
        [WindowTitle("ファイルアップロード")]
        T_FILE_UPLOAD,
        /// <summary>M731 ファイルアップロード一覧</summary>
        [WindowTitle("ファイルアップロード一覧")]
        T_FILE_UPLOAD_ICHIRAN,

        /// <summary>M734 電子契約最新照会</summary>
        [WindowTitle("電子契約最新照会")]
        T_DENSHI_KEIYAKU_SAISHIN_SHOUKAI,
        /// <summary>M737 書類情報入力</summary>
        [WindowTitle("書類情報入力")]
        M_DENSHI_KEIYAKU_SHORUI_INFO,

        #endregion

        #region Ver2.XX機能追加

        /// <summary>M738 コンテナ設置期間表示設定</summary>
        [WindowTitle("コンテナ設置期間表示設定")]
        M_CONTENA_KEIKA_DATE,

        /// <summary>M739 コンテナ設置期間表示設定</summary>
        [WindowTitle("地図連携")]
        M_MAP_RENKEI,

        /// <summary>現場メモ分類入力画面</summary>
        [WindowTitle("現場メモ分類入力")]
        M_GENBAMEMO_BUNRUI,

        /// <summary>G741 現場メモ入力</summary>
        [WindowTitle("現場メモ入力")]
        T_GENBA_MEMO_NYUURYOKU,

        /// <summary>G742 現場メモ一覧</summary>
        [WindowTitle("現場メモ一覧")]
        T_GENBA_MEMO_ICHIRAN,

        /// <summary>現場メモ表題入力画面</summary>
        [WindowTitle("現場メモ表題入力")]
        M_GENBAMEMO_HYOUDAI,

        #endregion

        #region Ver2.XX機能追加

        /// <summary>M746 共有先入力画面</summary>
        [WindowTitle("共有先入力（電子）")]
        M_KYOYUSAKI,

        /// <summary>M766 ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面</summary>
        [WindowTitle("ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力")]
        M_SMS_RECEIVER,

        /// <summary>G767 ｼｮｰﾄﾒｯｾｰｼﾞ入力画面</summary>
        [WindowTitle("ｼｮｰﾄﾒｯｾｰｼﾞ入力")]
        T_SMS_NYUURYOKU,

        /// <summary>G768 ｼｮｰﾄﾒｯｾｰｼﾞ送信一覧画面</summary>
        [WindowTitle("ｼｮｰﾄﾒｯｾｰｼﾞ送信一覧")]
        T_SMS_ICHIRAN,

        /// <summary>G769 ｼｮｰﾄﾒｯｾｰｼﾞ着信結果画面</summary>
        [WindowTitle("ｼｮｰﾄﾒｯｾｰｼﾞ着信結果")]
        T_SMS_RESULT,

        #endregion

        #endregion

        #region 帳票
        // 帳票が増えた場合、ここに追加。値は連番。
        // この値はM_LIST_PATTERN.WINDOW_IDに格納。
        /// <summary>R336 請求書</summary>
        [WindowTitle("請求書")]
        R_SEIKYUUSYO = 10001,
        /// <summary>R337 支払明細書</summary>
        [WindowTitle("支払明細書")]
        R_SHIHARAI_MEISAISYO = 10002,
        /// <summary>R338 仕切書</summary>
        [WindowTitle("仕切書")]
        R_SHIKIRISYO = 10003,
        /// <summary>R339 領収書</summary>
        [WindowTitle("領収書")]
        R_RYOUSYUUSYO = 10004,
        /// <summary>R342 受付明細表</summary>
        [WindowTitle("受付明細表")]
        R_UKETSUKE_MEISAIHYOU = 10005,
        /// <summary>R345 配車依頼書</summary>
        [WindowTitle("配車依頼書")]
        R_HAISYA_IRAISYO = 10006,
        /// <summary>R346 配車明細表</summary>
        [WindowTitle("配車明細表")]
        R_HAISYA_MEISAISYO = 10007,
        /// <summary>R349 配車割当表</summary>
        [WindowTitle("配車割当表")]
        R_HAISYA_WARIATEHYOU = 10008,
        /// <summary>R350 作業指示書</summary>
        [WindowTitle("指示書")]
        R_SAGYOU_SIJISYO = 10009,
        /// <summary>R351 計量明細表</summary>
        [WindowTitle("計量明細表")]
        R_KEIRYOU_MEISAIHYOU = 10010,
        /// <summary>R352 計量集計表</summary>
        [WindowTitle("計量集計表")]
        R_KEIRYOU_SYUUKEIHYOU = 10011,
        /// <summary>R354 計量票</summary>
        [WindowTitle("計量票")]
        R_KEIRYOU_HYOU = 10012,
        /// <summary>R355 売上／支払明細表</summary>
        [WindowTitle("売上／支払明細表")]
        R_URIAGE_SHIHARAI_MEISAIHYOU = 10013,
        /// <summary>R356 売上／支払集計表</summary>
        [WindowTitle("売上／支払集計表")]
        R_URIAGE_SHIHARAI_SYUUKEIHYOU = 10014,
        /// <summary>R358 売上明細表</summary>
        [WindowTitle("売上明細表")]
        R_URIAGE_MEISAIHYOU = 10015,
        /// <summary>R359 売上集計表</summary>
        [WindowTitle("売上集計表")]
        R_URIAGE_SYUUKEIHYOU = 10016,
        /// <summary>R361 売掛金一覧表</summary>
        [WindowTitle("売掛金一覧表")]
        R_URIKAKEKIN_ICHIRANHYOU = 10017,
        /// <summary>R362 支払明細表</summary>
        [WindowTitle("支払明細表")]
        R_SHIHARAI_MEISAIHYOU = 10018,
        /// <summary>R363 支払集計表</summary>
        [WindowTitle("支払集計表")]
        R_SHIHARAI_SYUUKEIHYOU = 10019,
        /// <summary>R365 買掛金一覧表</summary>
        [WindowTitle("買掛金一覧表")]
        R_KAIKAKEKIN_ICHIRANHYOU = 10020,
        /// <summary>R366 入金明細表</summary>
        [WindowTitle("入金明細表")]
        R_NYUUKIN_MEISAIHYOU = 10021,
        /// <summary>R367 入金集計表</summary>
        [WindowTitle("入金集計表")]
        R_NYUUKIN_SYUUKEIHYOU = 10022,
        /// <summary>R369 未入金一覧表</summary>
        [WindowTitle("未入金一覧表")]
        R_MINYUUKIN_ICHIRANHYOU = 10023,
        /// <summary>R370 入金予定一覧表</summary>
        [WindowTitle("入金予定一覧表")]
        R_NYUUKIN_YOTEI_ICHIRANHYOU = 10024,
        /// <summary>R372 滞留債権年齢表</summary>
        [WindowTitle("滞留債権年齢表")]
        R_TAIRYUU_SAIKEN_NENNREIHYOU = 10025,
        /// <summary>R373 出金明細表</summary>
        [WindowTitle("出金明細表")]
        R_SYUKKINN_MEISAIHYOU = 10026,
        /// <summary>R374 出金集計表</summary>
        [WindowTitle("出金集計表")]
        R_SYUKKINN_ICHIRANHYOU = 10027,
        /// <summary>R376 未払金一覧表</summary>
        [WindowTitle("未払金一覧表")]
        R_MISYUKKIN_ICHIRANHYOU = 10028,
        /// <summary>R377 出金予定一覧表</summary>
        [WindowTitle("出金予定一覧表")]
        R_SYUKKIN_YOTEI_ICHIRANHYOU = 10029,
        /// <summary>R379 請求明細表</summary>
        [WindowTitle("請求明細表")]
        R_SEIKYUU_MEISAIHYOU = 10030,
        /// <summary>R382 請求チェック表</summary>
        [WindowTitle("請求チェック表")]
        R_SEIKYUU_CHECKHYOU = 10031,
        /// <summary>R383 宛名ラベル</summary>
        [WindowTitle("宛名ラベル")]
        R_ATENA_LABEL = 10032,
        /// <summary>R384 支払明細明細表</summary>
        [WindowTitle("支払明細明細表")]
        R_SHIHARAIMEISAI_MEISAIHYOU = 10033,
        /// <summary>R387 支払チェック表</summary>
        [WindowTitle("支払チェック表")]
        R_SHIHARAI_CHECKHYOU = 10034,
        /// <summary>R389 マニフェストチェック表</summary>
        [WindowTitle("マニフェストチェック表")]
        R_MANIFEST_CHECKHYOU = 10035,
        /// <summary>R390 マニフェスト明細表</summary>
        [WindowTitle("マニフェスト明細表")]
        R_MANIFEST_MEISAIHYOU = 10036,
        /// <summary>R391 マニフェスト推移表</summary>
        [WindowTitle("マニフェスト推移表")]
        R_MANIFEST_SUIIHYOU = 10037,
        /// <summary>R394 交付等状況報告書</summary>
        [WindowTitle("交付等状況報告書")]
        R_KOUFUJYOUKYOU_HOUKOKUSYO = 10038,
        /// <summary>R395 廃棄物帳簿</summary>
        [WindowTitle("廃棄物帳簿")]
        R_HAIKIBUTSU_CHYOUBO = 10039,
        /// <summary>R396 実績報告書</summary>
        [WindowTitle("実績報告書")]
        R_JISSEKI_HOUKOKUSYO = 10040,
        /// <summary>R398 運賃明細表</summary>
        [WindowTitle("運賃明細表")]
        R_UNNCHIN_MEISAIHYOU = 10041,
        /// <summary>R400 検収一覧表</summary>
        [WindowTitle("検収一覧表")]
        R_KENNSYUU_ICHIRANHYOU = 10042,
        /// <summary>R403 代納明細表</summary>
        [WindowTitle("代納明細表")]
        R_DAINOU_ICHIRANHYOU = 10043,
        /// <summary>R405 在庫管理表</summary>
        [WindowTitle("在庫管理表")]
        R_ZAIKO_KANNRIHYOU = 10044,
        /// <summary>R407 返送案内書</summary>
        [WindowTitle("返送案内書")]
        R_HENSOU_ANNAISYO = 10045,
        /// <summary>R408 業者台帳</summary>
        [WindowTitle("業者台帳")]
        R_GYOUSYA_DAICHO = 10046,
        /// <summary>R409 現場台帳</summary>
        [WindowTitle("現場台帳")]
        R_GENBA_DAICHO = 10047,
        /// <summary>R410 取引先台帳</summary>
        [WindowTitle("取引先台帳")]
        R_TORIHIKISAKI_DAICHO = 10048,
        /// <summary>R415 売上元帳</summary>
        [WindowTitle("売上元帳")]
        R_URIAGE_MOTOCHO = 10049,
        /// <summary>R416 支払元帳</summary>
        [WindowTitle("支払元帳")]
        R_SHIHARAI_MOTOCHOU = 10050,
        /// <summary>R419 委託契約書</summary>
        [WindowTitle("委託契約書")]
        R_ITAKUKEIYAKUSYO = 10051,
        /// <summary>R424 単価変更対象一覧表</summary>
        [WindowTitle("単価変更対象一覧表")]
        R_TANKA_HENKOU_TAISYOU_ICHIRANHYOU = 10052,
        /// <summary>R425 見積書</summary>
        [WindowTitle("見積書")]
        R_MITSUMORISYO = 10053,
        /// <summary>R429 定期配車実績表(月報)</summary>
        [WindowTitle("定期配車実績表(月報)")]
        R_TEIKI_HAISYAHYOU_TSUKI = 10054,
        /// <summary>R430 定期配車実績表(年報)</summary>
        [WindowTitle("定期配車実績表(年報)")]
        R_TEIKI_HAISYAHYOU_NEN = 10055,
        /// <summary>R432 売上推移表</summary>
        [WindowTitle("売上推移表")]
        R_URIAGE_SUIIHYOU = 10056,
        /// <summary>R433 売上順位表</summary>
        [WindowTitle("売上順位表")]
        R_URIAGE_JYUNNIHYOU = 10057,
        /// <summary>R434 売上前年対比表</summary>
        [WindowTitle("売上前年対比表")]
        R_URIAGE_ZENNEN_TAIHIHYOU = 10058,
        /// <summary>R450 運転日報</summary>
        [WindowTitle("運転日報")]
        R_UNTEN_NIPPOU = 10059,
        /// <summary>R467 請求書(横レイアウト）</summary>
        [WindowTitle("請求書(横レイアウト）")]
        R_SEIKYUUSYO_YOKO = 10060,
        /// <summary>R468 支払明細書(横レイアウト）</summary>
        [WindowTitle("支払明細書(横レイアウト）")]
        R_SHIHARAI_MEISAISYO_YOKO = 10061,
        /// <summary>R470 在庫元帳</summary>
        [WindowTitle("在庫元帳")]
        R_ZAIKO_MOTOCHOU = 10062,
        /// <summary>R473 請求締処理エラー一覧表</summary>
        [WindowTitle("請求締処理エラー一覧表")]
        R_SEIKYUU_SIMESYORI_ERROR = 10063,
        /// <summary>R474 支払締処理エラー一覧表</summary>
        [WindowTitle("支払締処理エラー一覧表")]
        R_SHIHARAI_SIMESYORI_ERROR = 10064,
        /// <summary>R478 マニフェスト集計表</summary>
        [WindowTitle("マニフェスト集計表")]
        R_MANIFEST_SYUUKEIHYOU = 10065,
        /// <summary>R483 運賃集計表</summary>
        [WindowTitle("運賃集計表")]
        R_UNNCHIN_SYUUKEIHYOU = 10066,
        /// <summary>R488 代納集計表</summary>
        [WindowTitle("代納集計表")]
        R_DAINOU_SYUUKEIHYOU = 10067,
        /// <summary>R432 支払推移表</summary>
        [WindowTitle("支払推移表")]
        R_SHIHARAI_SUIIHYOU = 10068,
        /// <summary>R432 売上/支払推移表</summary>
        [WindowTitle("売上/支払推移表")]
        R_URIAGE_SHIHARAI_SUIIHYOU = 10069,
        /// <summary>R432 売上・支払（全て）推移表</summary>
        [WindowTitle("売上・支払（全て）推移表")]
        R_URIAGE_SHIHARAI_ALL_SUIIHYOU = 10070,
        /// <summary>R432 計量推移表</summary>
        [WindowTitle("計量推移表")]
        R_KEIRYOU_SUIIHYOU = 10071,
        /// <summary>R433 支払順位表</summary>
        [WindowTitle("支払順位表")]
        R_SHIHARAI_JYUNNIHYOU = 10072,
        /// <summary>R433 売上/支払順位表</summary>
        [WindowTitle("売上/支払順位表")]
        R_URIAGE_SHIHARAI_JYUNNIHYOU = 10073,
        /// <summary>R433 売上・支払（全て）順位表</summary>
        [WindowTitle("売上・支払（全て）順位表")]
        R_URIAGE_SHIHARAI_ALL_JYUNNIHYOU = 10074,
        /// <summary>R433 計量順位表</summary>
        [WindowTitle("計量順位表")]
        R_KEIRYOU_JYUNNIHYOU = 10075,
        /// <summary>R434 支払前年対比表</summary>
        [WindowTitle("支払前年対比表")]
        R_SHIHARAI_ZENNEN_TAIHIHYOU = 10076,
        /// <summary>R434 売上/支払前年対比表</summary>
        [WindowTitle("売上/支払前年対比表")]
        R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU = 10077,
        /// <summary>R434 売上・支払（全て）前年対比表</summary>
        [WindowTitle("売上・支払（全て）前年対比表")]
        R_URIAGE_SHIHARAI_ALL_ZENNEN_TAIHIHYOU = 10078,
        /// <summary>R434 計量前年対比表</summary>
        [WindowTitle("計量前年対比表")]
        R_KEIRYOU_ZENNEN_TAIHIHYOU = 10079,
        /// <summary>R493 マニフェスト直行用</summary>
        [WindowTitle("マニフェスト直行用")]
        R_MANIFEST_CHOKKOU = 10080,
        /// <summary>R494 マニフェスト積替用</summary>
        [WindowTitle("マニフェスト積替用")]
        R_MANIFEST_TSUMIKAE = 10081,
        /// <summary>R495 マニフェスト建廃用</summary>
        [WindowTitle("マニフェスト建廃用")]
        R_MANIFEST_KENPAI = 10082,
        /// <summary>R651 運賃台帳</summary>
        [WindowTitle("運賃台帳")]
        R_UNCHIN_DAICHOU = 10083,
        /// <summary>配車割当表</summary>
        [WindowTitle("配車割当表")]
        R_HAISHA_WARIATEHYOU = 10084,
        #endregion

        #region 汎用帳票
        /// <summary>G627 入金集計表</summary>
        [WindowTitle("入金集計表 出力画面")]
        T_NYUUKIN_SHUUKEIHYOU = 11001,
        /// <summary>G629 出金集計表</summary>
        [WindowTitle("出金集計表 出力画面")]
        T_SHUKKIN_SHUUKEIHYOU = 11002,
        /// <summary>G479 マニフェスト集計表</summary>
        [WindowTitle("マニフェスト集計表")]
        T_MANIFEST_SYUUKEIHYOU = 11003,
        /// <summary>G581 売上推移表</summary>
        [WindowTitle("売上推移表")]
        T_URIAGE_SUIIHYOU = 11004,
        /// <summary>G584 支払推移表</summary>
        [WindowTitle("支払推移表")]
        T_SHIHARAI_SUIIHYOU = 11005,
        /// <summary>G623 売上順位表</summary>
        [WindowTitle("売上順位表 出力画面")]
        T_URIAGE_JUNIHYO = 11006,
        /// <summary>G625 支払順位表</summary>
        [WindowTitle("支払順位表 出力画面")]
        T_SHIHARAI_JUNIHYO = 11007,
        /// <summary>G678 計量集計表</summary>
        [WindowTitle("計量集計表")]
        T_KEIRYOU_SHUUKEIHYOU = 11008,
        /// <summary>G723 マニフェスト推移表</summary>
        [WindowTitle("マニフェスト推移表 出力画面")]
        T_MANIFEST_SUIIHYOU = 11009,
        #endregion

        /// <summary>R434 売上前年対比表</summary>
        [WindowTitle("売上前年対比表")]
        T_URIAGE_ZENNEN_TAIHIHYOU = 10085,
        [WindowTitle("支払前年対比表")]
        T_SHIHARAI_ZENNEN_TAIHIHYOU = 10086,
        /// <summary>
        /// G725 単価履歴
        /// </summary>
        [WindowTitle("単価履歴")]
        T_TANKA_RIREKI_ICHIRAN,
        //CongBinh 20210713 #152806 S
        [WindowTitle("作業日入力")]
        SAGYOUBI_NYUURYOKU,
        //CongBinh 20210713 #152806 E
        //CongBinh 20210714 #152813 S
        [WindowTitle("許可証一覧")]
        KYOKASHOU_ICHIRAN = 748,
        //CongBinh 20210714 #152813 E

        // Begin: LANDUONG - 20220214 - refs#
        /// <summary>楽楽明細マスタ一覧</summary>        
        RAKURAKU_MASUTA_ICHIRAN,
        // End: LANDUONG - 20220214 - refs#

        [WindowTitle("伝票履歴")]
        T_DENPYOU_RIREKI_ICHIRAN,

        /// <summary>G751 出金消込修正入力</summary>
        [WindowTitle("出金消込修正入力")]
        T_SYUKKIN_KESHIKOMI_SHUSEI,
        /// <summary>G752 出金消込一覧</summary>
        [WindowTitle("出金消込一覧")]
        T_SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN,
        /// <summary>G753 未出金一覧表</summary>
        [WindowTitle("未出金一覧表 出力画面")]
        T_MISHUKKIN_ICHIRAN,
        /// <summary>R369 未入金一覧表</summary>
        [WindowTitle("未出金一覧表")]
        R_MISHUKKIN_ICHIRANHYOU,
        /// <summary>G754 出金予定一覧表</summary>
        [WindowTitle("出金予定一覧表 出力画面")]
        T_SHUKKIN_YOTEI_ICHIRAN,
        /// <summary>R756（帳票）出金予定一覧表</summary>
        [WindowTitle("出金予定一覧表")]
        R_SHUKKIN_YOTEI_ICHIRANHYOU,
        /// <summary>
        /// G762 出金データ出力
        /// </summary>
        [WindowTitle("出金データ出力")]
        T_SHUKKIN_DATA_SHUTSURYOKU,

        /// <summary>電子文書詳細入力画面</summary>
        [WindowTitle("電子文書詳細入力")]
        M_DENSHI_BUNSHO_INFO,

        ///20250317
        [WindowTitle("グループ入力(売上)")]
        M_GURUPU_NYURYOKU,

        ///20250403
        [WindowTitle("備考パターン入力")]
        M_BIKO_PATAN_NYURYOKU,

        ///20250404
        [WindowTitle("備考選択肢入力")]
        M_BIKO_SENTAKUSHI_NYURYOKU,

        ///20250405
        [WindowTitle("備考内訳入力")]
        M_BIKO_UCHIWAKE_NYURYOKU,
    }

    /// <summary>
    /// 画面タイトル拡張クラス
    /// </summary>
    public static class WINDOW_TITLEExt
    {
        /// <summary>
        /// タイトルの日本語名取得
        /// </summary>
        /// <param name="id">画面ID</param>
        /// <returns></returns>
        public static string ToTitleString(this WINDOW_ID id)
        {
            switch (id)
            {
                case WINDOW_ID.MENU_LIST:
                    return "全メニュー表示";
                case WINDOW_ID.M_SYS_INFO:
                    return "システム設定入力";
                case WINDOW_ID.M_DENSHI_SHINSEI_ROUTE:
                    return "申請経路入力";
                case WINDOW_ID.M_TAB_ORDER:
                    return "入力項目設定";
                case WINDOW_ID.M_KEITAI_KBN:
                    return "形態区分入力";
                case WINDOW_ID.M_DENPYOU_KBN:
                    return "伝票区分入力";
                case WINDOW_ID.M_TORIHIKI_KBN:
                    return "取引区分入力";
                case WINDOW_ID.M_DENSHU_KBN:
                    return "伝種区分入力";
                case WINDOW_ID.M_UNIT:
                    return "単位入力";
                case WINDOW_ID.M_SHOUHIZEI:
                    return "消費税入力";
                case WINDOW_ID.M_NYUUSHUKKIN_KBN:
                    return "入出金区分入力";
                case WINDOW_ID.M_TODOUFUKEN:
                    return "都道府県入力";
                case WINDOW_ID.M_SHIKUCHOUSON:
                    return "市区町村入力";
                case WINDOW_ID.M_CHIIKI:
                    return "地域入力";
                case WINDOW_ID.M_MANIFEST_SHURUI:
                    return "マニフェスト種類入力";
                case WINDOW_ID.M_MANIFEST_TEHAI:
                    return "マニフェスト手配入力";
                case WINDOW_ID.M_HAIKI_KBN:
                    return "廃棄物区分入力";
                case WINDOW_ID.M_KEIJOU:
                    return "形状入力";
                case WINDOW_ID.M_NISUGATA:
                    return "荷姿入力";
                case WINDOW_ID.M_UNPAN_HOUHOU:
                    return "運搬方法入力";
                case WINDOW_ID.M_SHOBUN_HOUHOU:
                    return "処分方法入力";
                case WINDOW_ID.M_YUUGAI_BUSSHITSU:
                    return "有害物質入力";
                case WINDOW_ID.M_CONTENA_JOUKYOU:
                    return "コンテナ状況入力";
                case WINDOW_ID.M_YUBIN_MAINTENANCE:
                    return "郵便辞書メンテナンス";
                case WINDOW_ID.S_ZIP_CODE:
                    return "郵便辞書メンテナンス";
                case WINDOW_ID.M_RIYOU_RIREKI:
                    return "利用履歴管理";
                case WINDOW_ID.M_CORP_INFO:
                    return "自社情報入力";
                case WINDOW_ID.M_KYOTEN:
                    return "拠点入力";
                case WINDOW_ID.M_BUMON:
                    return "部門入力";
                case WINDOW_ID.M_BUPPAN_ZAIKO_BASHO:
                    return "物販在庫持出場所入力";
                case WINDOW_ID.M_KAISHA_KYUJITSU:
                    return "会社休日入力";
                case WINDOW_ID.M_BUSHO:
                    return "部署入力";
                case WINDOW_ID.M_SHAIN:
                    return "社員入力";
                case WINDOW_ID.M_MENU_AUTH_EACH_SHAIN:
                    return "メニュー権限";
                case WINDOW_ID.M_MENU_AUTH_EACH_MENU:
                    return "メニュー権限入力（メニュー毎）";
                case WINDOW_ID.M_MENU_AUTH_PATTERN_ICHIRAN:
                    return "メニュー権限パターン一覧";
                case WINDOW_ID.M_NYUURYOKU_TANTOUSHA:
                    return "入力担当者情報入力";
                case WINDOW_ID.M_INXS_TANTOUSHA:
                    return "INXS担当者情報入力";
                case WINDOW_ID.M_EIGYOU_TANTOUSHA:
                    return "営業担当者情報入力";
                case WINDOW_ID.M_EIGYOU_TANTOUSHA_IKKATSU:
                    return "営業担当者一括変更";
                case WINDOW_ID.M_UNTENSHA:
                    return "運転者情報入力";
                case WINDOW_ID.M_SHOBUN_TANTOUSHA:
                    return "処分担当者情報入力";
                case WINDOW_ID.M_TEGATA_HOKANSHA:
                    return "手形保管者情報入力";
                case WINDOW_ID.M_BANK:
                    return "銀行入力";
                case WINDOW_ID.M_BANK_SHITEN:
                    return "銀行支店入力";
                case WINDOW_ID.M_BANK_IKKATSU:
                    return "銀行一括変更";
                case WINDOW_ID.M_TORIHIKISAKI:
                    return "取引先入力";
                case WINDOW_ID.M_TORIHIKISAKI_KAKUNIN:
                    return "申請内容確認（取引先）";
                case WINDOW_ID.M_TORIHIKISAKI_ICHIRAN:
                    return "取引先一覧";
                case WINDOW_ID.M_GYOUSHA:
                    return "業者入力";
                case WINDOW_ID.M_GYOUSHA_ICHIRAN:
                    return "業者一覧";
                case WINDOW_ID.M_GENBA:
                    return "現場入力";
                case WINDOW_ID.M_GENBA_ICHIRAN:
                    return "現場一覧";
                case WINDOW_ID.M_NYUUKINSAKI:
                    return "入金先入力";
                case WINDOW_ID.M_SYUKKINSAKI:
                    return "出金先入力";
                case WINDOW_ID.M_KIHON_HINMEI_TANKA:
                    return "基本品名単価入力";
                case WINDOW_ID.M_KOBETSU_HINMEI_TANKA:
                    return "個別品名単価入力";
                case WINDOW_ID.M_ITAKU_KEIYAKU:
                    return "委託契約書入力";
                case WINDOW_ID.M_ITAKU_KEIYAKU_SANPAI:
                    return "委託契約書入力(産廃)";
                case WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI:
                    return "委託契約書入力(建廃)";
                case WINDOW_ID.M_ITAKU_KEIYAKU_KIGEN:
                    return "委託契約書許可証期限管理";
                case WINDOW_ID.M_ITAKU_KEIYAKU_ICHIRAN:
                    return "委託契約書一覧";
                case WINDOW_ID.M_OBOE_IKKATSU:
                    return "覚書一括入力";
                case WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN:
                    return "覚書一括入力一覧";
                case WINDOW_ID.M_SAISHU_SHOBUNBASHO_PATTERN_ICHIRAN:
                    return "最終処分場所パターン一覧";
                case WINDOW_ID.M_CHIIKIBETSU_KYOKA:
                    return "地域別許可番号入力";
                case WINDOW_ID.M_CHIIKIBETSU_KYOKA_UPN:
                    return "地域別許可番号入力（運搬）";
                case WINDOW_ID.M_CHIIKIBETSU_KYOKA_SBN:
                    return "地域別許可番号入力（処分）";
                case WINDOW_ID.M_CHIIKIBETSU_KYOKA_LAST_SBN:
                    return "地域別許可番号入力（最終処分）";
                case WINDOW_ID.M_SHARYOU:
                    return "車輌入力";
                case WINDOW_ID.M_SHASHU:
                    return "車種入力";
                case WINDOW_ID.M_CONTENA_SHURUI:
                    return "コンテナ種類入力";
                case WINDOW_ID.M_CONTENA:
                    return "コンテナ入力";
                case WINDOW_ID.M_HINMEI:
                    return "品名入力";
                case WINDOW_ID.M_DENSHI_SHINSEI_KEIRO_NAME:
                    return "申請経路名入力";
                case WINDOW_ID.M_SHURUI:
                    return "種類入力";
                case WINDOW_ID.M_BUNRUI:
                    return "分類入力";
                case WINDOW_ID.M_SHUUKEI_KOUMOKU:
                    return "集計項目入力";
                case WINDOW_ID.M_KEIRYOU_CHOUSEI:
                    return "計量調整入力";
                case WINDOW_ID.M_YOUKI:
                    return "容器入力";
                case WINDOW_ID.M_KANSAN:
                    return "換算値入力";
                case WINDOW_ID.M_CONTENA_SOUSA:
                    return "コンテナ操作入力";
                case WINDOW_ID.M_ZAIKO_SHOHIN:
                    return "在庫商品入力";
                case WINDOW_ID.M_ZAIKO_HIRITSU:
                    return "在庫比率入力";
                case WINDOW_ID.M_BUPPAN_ZAIKO_SHOHIN:
                    return "物販在庫商品入力";
                case WINDOW_ID.M_GENBACHAKU_TIME:
                    return "現着時間入力";
                case WINDOW_ID.M_GENCHAKU_TIME:
                    return "現着時間入力";
                case WINDOW_ID.M_COURSE_NAME:
                    return "コース名入力";
                case WINDOW_ID.M_COURSE:
                    return "コース入力";
                case WINDOW_ID.M_HAIKI_SHURUI:
                    return "廃棄物種類入力";
                case WINDOW_ID.M_HAIKI_NAME:
                    return "廃棄物名称入力";
                case WINDOW_ID.M_KONGOU_SHURUI:
                    return "混合種類入力";
                case WINDOW_ID.M_KONGOU_HAIKIBUTSU:
                    return "混合品名入力";
                case WINDOW_ID.M_MANIFEST_KANSAN:
                    return "マニフェスト換算値入力";
                case WINDOW_ID.M_GENNYOURITSU:
                    return "減容率入力";
                case WINDOW_ID.M_HOUKOKUSHO_BUNRUI:
                    return "報告書分類入力";
                case WINDOW_ID.M_SHOBUN_MOKUTEKI:
                    return "処分目的入力";
                case WINDOW_ID.M_GYOUSHU:
                    return "業種入力";
                case WINDOW_ID.M_CHIIKIBETSU_GYOUSHU:
                    return "地域別業種入力";
                case WINDOW_ID.M_CHIIKIBETSU_SHISETSU:
                    return "地域別施設入力";
                case WINDOW_ID.M_CHIIKIBETSU_JUSHO:
                    return "地域別住所入力";
                case WINDOW_ID.M_CHIIKIBETSU_SHOBUN:
                    return "地域別処分入力";
                case WINDOW_ID.M_CHIIKIBETSU_BUNRUI:
                    return "地域別分類入力";
                case WINDOW_ID.M_ZIGYOSHA:
                    return "事業者入力";
                case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                    return "事業者入力";
                case WINDOW_ID.M_ZIGYOSHA_ICHIRAN:
                    return "事業者一覧";
                case WINDOW_ID.M_ZIGYOSHA_MIHIMOZUKE_ICHIRAN:
                    return "事業者未紐付一覧";
                case WINDOW_ID.M_DENSHI_JIGYOUSHA_MIHIMODUKE_ICHIRAN:
                    return "事業者未紐付一覧";
                case WINDOW_ID.M_ZIGYOBA:
                    return "事業場入力";
                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    return "事業場入力";
                case WINDOW_ID.M_ZIGYOBA_ICHIRAN:
                    return "事業場一覧";
                case WINDOW_ID.M_ZIGYOBA_MIHIMOZUKE_ICHIRAN:
                    return "事業場未紐付一覧";
                case WINDOW_ID.M_DENSHI_JIGYOUJOU_MIHIMODUKE_ICHIRAN:
                    return "事業場未紐付一覧";
                case WINDOW_ID.M_TANTOUSHA:
                    return "担当者入力";
                case WINDOW_ID.M_DENSHI_TANTOUSHA:
                    return "担当者入力";
                case WINDOW_ID.M_DENSHI_YUUGAI_BUSSHITSU:
                    return "有害物質入力";
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI:
                    return "電子廃棄物種類入力";
                case WINDOW_ID.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI:
                    return "廃棄物種類細分類入力";
                case WINDOW_ID.M_DENSHI_HAIKI_NAME:
                    return "電子廃棄物名称入力";
                case WINDOW_ID.UKETSUKE_SHUSHU:
                    return "収集受付入力";
                case WINDOW_ID.KOKYAKU_ITIRAN:
                    return "顧客一覧";
                case WINDOW_ID.UKETSUKE_SHUKKA:
                    return "出荷受付入力";
                case WINDOW_ID.UKETSUKE_ICHIRAN:
                    return "受付一覧";
                case WINDOW_ID.M_WORK_CLOSED_UNTENSHA:
                    return "運転者休動入力";
                case WINDOW_ID.M_WORK_CLOSED_SHARYOU:
                    return "車輛休動入力";
                case WINDOW_ID.M_WORK_CLOSED_HANNYUUSAKI:
                    return "荷降先休動入力";
                case WINDOW_ID.M_TSUKIGIME_TANKA:
                    return "月極単価入力";
                case WINDOW_ID.M_GENBA_TEIKI:
                    return "現場定期入力";
                case WINDOW_ID.M_KANYUSHA:
                    return "加入者情報入力";
                case WINDOW_ID.M_DM_KANSAN:
                    return "換算値入力";
                case WINDOW_ID.M_DM_GENNYOURITSU:
                    return "減容率入力";
                case WINDOW_ID.M_GYOUSHA_DAICHO:
                    return "業者台帳";
                case WINDOW_ID.M_GENBA_DAICHO:
                    return "現場台帳";
                case WINDOW_ID.M_TORIHIKISAKI_DAICHO:
                    return "取引先台帳";
                case WINDOW_ID.M_TANKA_IKKATSU:
                    return "単価一括変更";
                // 20150421 teikyou #4935 start
                case WINDOW_ID.M_UNCHIN_TANKA:
                    return "運賃単価入力";
                // 20150421 teikyou #4935 end
                case WINDOW_ID.M_UNCHIN_HINMEI:
                    return "運賃品名入力";
                case WINDOW_ID.M_DENSHI_SHINSEI_ROUTE_NAME:
                    return "申請経路名入力";
                case WINDOW_ID.M_DENSHI_SHINSEI_JYUYOUDO:
                    return "重要度入力";
                case WINDOW_ID.M_GYOUSHA_KAKUNIN:
                    return "申請内容確認（業者）";
                case WINDOW_ID.T_UNCHIN_MEISAIHYOU:
                    return "運賃明細表　出力画面";
                // 20150611 マイメニュー選択追加 Start
                case WINDOW_ID.M_BOOKMARK:
                    return "マイメニュー選択";
                // 20150611 マイメニュー選択追加 End
                case WINDOW_ID.M_IPPANPAIYOU_HOUKOKUSHO_BUNRUI:
                    return "実績分類入力";
                case WINDOW_ID.T_DENPYOU_RENKEI_JOUKYOU_ICHIRAN:
                    return "伝票連携状況一覧";
                case WINDOW_ID.T_UKETSUKE_MEISAIHYO:
                    return "受付明細表 出力画面";
                case WINDOW_ID.T_SHIHARAI_MEISAI_MEISAIHYO:
                    return "支払明細明細表 出力画面";
                case WINDOW_ID.M_KOBETSU_HINMEI:
                    return "個別品名入力";
                case WINDOW_ID.M_COURSE_ICHIRAN:
                    return "コース一覧";
                case WINDOW_ID.T_HANYO_CSV_SHUTSURYOKU:
                    return "汎用CSV出力";
                case WINDOW_ID.T_HANYO_CSV_OUTPUT_KOUMOKU:
                    return "CSV出力項目選択";
                case WINDOW_ID.M_KOBETSU_HUNNMETANNKA_YIKKATSU:
                    return "個別品名単価一括変更";
                case WINDOW_ID.M_SYS_KOBETSU_INFO:
                    return "システム個別設定入力";
                case WINDOW_ID.T_MANIFEST_IKKATSUKOUSIN:
                    return "マニフェスト終了日一括更新";
                case WINDOW_ID.T_DENMANI_SAISHUSHOBUN:
                    return "紐付1次最終処分終了報告";
                case WINDOW_ID.M_CHIIKI_IKKATSU:
                    return "地域一括変更";
                case WINDOW_ID.T_TANKA_RIREKI_ICHIRAN:
                    return "単価履歴";
                case WINDOW_ID.T_SHIHARAI_ZENNEN_TAIHIHYOU:
                    return "支払前年対比表";
                //CongBinh 20210714 #152813 S
                case WINDOW_ID.KYOKASHOU_ICHIRAN:
                    return "許可証一覧";
                //CongBinh 20210714 #152813 E

                case WINDOW_ID.RAKURAKU_MASUTA_ICHIRAN:
                    return "楽楽明細マスタ一覧";

                case WINDOW_ID.T_DENPYOU_RIREKI_ICHIRAN:
                    return "伝票履歴";

                case WINDOW_ID.T_SHUKKIN_DATA_SHUTSURYOKU:
                    return "出金データ出力";
                case WINDOW_ID.M_DENSHI_BUNSHO_INFO:
                    return "電子文書詳細入力";

                //20250317
                case WINDOW_ID.M_GURUPU_NYURYOKU:
                    return "グループ入力(売上)";
                
                //20250403
                case WINDOW_ID.M_BIKO_PATAN_NYURYOKU:
                    return "備考パターン入力";

                //20250404
                case WINDOW_ID.M_BIKO_SENTAKUSHI_NYURYOKU:
                    return "備考選択肢入力";

                //20250405
                case WINDOW_ID.M_BIKO_UCHIWAKE_NYURYOKU:
                    return "備考内訳入力";
                default:
                    var t = id.GetType().GetField(id.ToString()).GetCustomAttributes(typeof(WindowTitle), false)
                                   .OfType<WindowTitle>().FirstOrDefault();
                    return t == null ? String.Empty : t.Title;
            }
        }
    }
}
