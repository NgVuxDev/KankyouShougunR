using System;
using System.Drawing;

namespace Shougun.Core.Common.BusinessCommon.Const
{
    /// <summary>
    /// 環境将軍Rで使用する共通的な定数を定義
    /// </summary>
    public static class CommonConst
    {
		/// <summary>拠点CD：99</summary>
		public static readonly int KYOTEN_CD_ZENSHA = 99;

		/// <summary>税計算区分</summary>
		public static readonly Int16 ZEI_KEISAN_KBN_DENPYOU = 1;	// 伝票毎
		public static readonly Int16 ZEI_KEISAN_KBN_SEIKYUU = 2;	// 請求毎
		public static readonly Int16 ZEI_KEISAN_KBN_MEISAI = 3;	// 明細毎

		/// <summary>税区分</summary>
		public static readonly Int16 ZEI_KBN_SOTO = 1;		// 外税
		public static readonly Int16 ZEI_KBN_UCHI = 2;		// 内税
		public static readonly Int16 ZEI_KBN_EXEMPTION = 3;	// 非課税

        /// <summary>伝票区分</summary>
        public static readonly Int16 DENPYOU_KBN_URIAGE = 1;		// 売上
        public static readonly Int16 DENPYOU_KBN_SHIHARAI = 2;	// 支払
        public static readonly Int16 DENPYOU_KBN_KYOUTSUU = 9;	// 共通

        /// <summary>確定区分</summary>
        public static readonly Int16 KAKUTEI_KBN_KAKUTEI = 1;		// 確定
        public static readonly Int16 KAKUTEI_KBN_MIKAKUTEI = 2;	// 未確定

        /// <summary>確定利用区分</summary>
        public static readonly Int16 KAKUTEI_USE_KBN_USE = 1;		// 確定登録を利用する
        public static readonly Int16 KAKUTEI_USE_KBN_UNUSED = 2;	// 利用しない

        /// <summary>伝種区分</summary>
        public static readonly Int16 DENSHU_KBN_UKEIRE = 1;             // 受入
        public static readonly Int16 DENSHU_KBN_SHUKKA = 2;             // 出荷
        public static readonly Int16 DENSHU_KBN_UR_SH = 3;              // 売上支払
        public static readonly Int16 DENSHU_KBN_KYOUTSUU = 9;           // 共通
        public static readonly Int16 DENSHU_KBN_NYUUKIN = 10;           // 入金
        public static readonly Int16 DENSHU_KBN_SHUKKINN = 20;          // 出金
        public static readonly Int16 DENSHU_KBN_SEIKYUU = 30;           // 請求
        public static readonly Int16 DENSHU_KBN_SEIKYUU_SHIME = 40;     // 請求締
        public static readonly Int16 DENSHU_KBN_SHIHARAI = 50;          // 支払
        public static readonly Int16 DENSHU_KBN_SHIHARAI_SHIME = 60;    // 支払締
        public static readonly Int16 DENSHU_KBN_MANIFEST = 80;          // 紙マニフェスト
        public static readonly Int16 DENSHU_KBN_D_MANIFEST = 90;        // 電子マニフェスト
        public static readonly Int16 DENSHU_KBN_UKETSUKE = 100;         // 受付
        public static readonly Int16 DENSHU_KBN_HAISHA = 110;           // 配車
        public static readonly Int16 DENSHU_KBN_TEIKI_HAISHA = 120;     // 定期配車
        public static readonly Int16 DENSHU_KBN_TEIKI_JISSEKI = 130;    // 定期実績
        public static readonly Int16 DENSHU_KBN_KEIRYOU = 140;          // 計量
        public static readonly Int16 DENSHU_KBN_ZAIKO = 150;            // 在庫
        public static readonly Int16 DENSHU_KBN_UNCHIN = 160;           // 運賃
        public static readonly Int16 DENSHU_KBN_DAINOU = 170;           // 代納
        public static readonly Int16 DENSHU_KBN_MITSUMORI = 180;        // 見積
    
        /// <summary>消費税端数CD</summary>
        public enum TAX_HASUU_CD : short
        {
            CEILING = 1,    // 切り上げ
            FLOOR,          // 切り捨て
            ROUND,          // 四捨五入
        }

        /// <summary>設置引揚区分</summary>
        public static readonly Int16 CONTENA_SET_KBN_SECCHI = 1;
        public static readonly Int16 CONTENA_SET_KBN_HIKIAGE = 2;

        /// <summary>DT_MF_TOC.STATUS_FLAG</summary>
        public static readonly string DT_MF_TOC_STATUS_FLAG_REFISTERED = "4";

        /// <summary>DT_MF_TOC.STATUS_DETAIL(0:通常、1:修正中、2:承認待ち、3：承認中)</summary>
        public static readonly string DT_MF_TOC_STATUS_DETAIL_MODIFYING = "1";

        /// <summary>
        /// 最終処分終了報告、最終処分終了取消のFUNCTION_ID(QUE_INFO)
        /// </summary>
        public static readonly string FUNCTION_ID_2000 = "2000";
        public static readonly string FUNCTION_ID_2100 = "2100";
    
        /// <summary>配車種類CD</summary>
        public static readonly Int16 HAISHA_SHURUI_TSUUJOU = 1;     // 通常
        public static readonly Int16 HAISHA_SHURUI_KARI = 2;        // 仮押
        public static readonly Int16 HAISHA_SHURUI_KAKUTEI = 3;     // 確定

        /// <summary>コンテナ管理方法(1：数量管理、2：個体管理)</summary>
        public static readonly Int16 CONTENA_KANRI_HOUHOU_SUURYOU = 1;
        public static readonly Int16 CONTENA_KANRI_HOUHOU_KOTAI = 2;

        /// <summary>部署CD：999(全部署)</summary>
        public static readonly string BUSHO_CD_ZENBUSHO = "999";

        /// <summary>契約区分</summary>
        public static readonly Int16 KEIYAKU_KBN_TEIKI = 1;		// 定期
        public static readonly Int16 KEIYAKU_KBN_TANKA = 2;	    // 単価
        public static readonly Int16 KEIYAKU_KBN_NOT = 3;	        // なし

        /// <summary>計上(月極)区分</summary>
        public static readonly Int16 KEIJYOU_KBN_DENPYOU = 1;		// 伝票
        public static readonly Int16 KEIJYOU_KBN_TOTAL = 2;		// 合算

        /// <summary>電子事業者区分</summary>
        public static readonly Int16 DENSHI_JIGYOUSHA_KBN_HST = 1;  // 排出
        public static readonly Int16 DENSHI_JIGYOUSHA_KBN_UPN = 2;  // 運搬
        public static readonly Int16 DENSHI_JIGYOUSHA_KBN_SBN = 3;  // 処分

        /// <summary>電子事業場区分</summary>
        public static readonly Int16 DENSHI_JIGYOUJOU_KBN_HST = 1;  // 排出
        public static readonly Int16 DENSHI_JIGYOUJOU_KBN_UPN = 2;  // 運搬
        public static readonly Int16 DENSHI_JIGYOUJOU_KBN_SBN = 3;  // 処分

        /// <summary>T_MANIFET_RELATIONのHAIKI_KBN_CD</summary>
        public static readonly Int16 RELATIION_HAIKI_KBN_CD_DENSHI = 4; // 電子

        /// <summary>DT_MF_TOCのKIND</summary>
        public static readonly Int16 MF_TOC_KIND_NOT_EDI = 5;           // Not EDI(手動)

        /// <summary>キャッシャ入出金区分</summary>
        public static readonly Int16 CASHER_MONEY_KBN_NYUUKIN = 1;  // 入金
        public static readonly Int16 CASHER_MONEY_KBN_SHUKKIN = 2;  // 出金

        /// <summary>取引区分</summary>
        public static readonly Int16 TORIHIKI_KBN_GENKIN = 1;	// 現金
        public static readonly Int16 TORIHIKI_KBN_KAKE = 2;	// 掛金

        /// <summary>キャッシャ連動区分</summary>
        public static readonly string CASHER_LINK_KBN_USE = "1";	    // 連動する
        public static readonly string CASHER_LINK_KBN_UNUSED = "2";	// しない

        /// <summary>入出金区分</summary>
        public static readonly Int16 NYUUSHUKKIN_KBN_GENKIN = 1;	    // 現金
        public static readonly Int16 NYUUSHUKKIN_KBN_FURIKOMI = 2;	    // 振込
        public static readonly Int16 NYUUSHUKKIN_KBN_TEGATA = 5;	    // 手形
        public static readonly Int16 NYUUSHUKKIN_KBN_TESUURYO = 21;	    // 手数料
        public static readonly Int16 NYUUSHUKKIN_KBN_SOUSAI = 22;	    // 相殺
        public static readonly Int16 NYUUSHUKKIN_KBN_KARIUKEKIN = 51;	// 仮受金

        public static readonly string SYS_ID = "0";

        public class RequestStatusInxs
        {
            /// <summary>新規確定待ちのValue</summary>
            public const int WAITING_NEW_CONFIRM_VALUE = 1;
            /// <summary>新規確定待ちのText</summary>
            public static readonly string WAITING_NEW_CONFIRM_TEXT = "新規確定待ち";
            /// <summary>新規確定待ちの背景色</summary>
            public static readonly Color WAITING_NEW_CONFIRM_BACK_COLOR = Color.FromArgb(217, 51, 67);
            /// <summary>新規確定待ちの文字色</summary>
            public static readonly Color WAITING_NEW_CONFIRM_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>確定のValue</summary>
            public const int CONFIRMED_VALUE = 2;
            /// <summary>確定のText</summary>
            public static readonly string CONFIRMED_TEXT = "確定";
            /// <summary>確定の背景色</summary>
            public static readonly Color CONFIRMED_BACK_COLOR = Color.FromArgb(40, 167, 69);
            /// <summary>確定の文字色</summary>
            public static readonly Color CONFIRMED_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>変更確定待ちのValue</summary>
            public const int WAITING_CHANGE_CONFIRM_VALUE = 3;
            /// <summary>変更確定待ちのText</summary>
            public static readonly string WAITING_CHANGE_CONFIRM_TEXT = "変更確定待ち";
            /// <summary>変更確定待ちの背景色</summary>
            public static readonly Color WAITING_CHANGE_CONFIRM_BACK_COLOR = Color.FromArgb(255, 255, 0);
            /// <summary>変更確定待ちの文字色</summary>
            public static readonly Color WAITING_CHANGE_CONFIRM_FORE_COLOR = Color.FromArgb(0, 0, 0);


            /// <summary>キャンセル確定待ちのValue</summary>
            public const int WAITING_CANCE_CONFIRM_VALUE = 4;
            /// <summary>キャンセル確定待ちのText</summary>
            public static readonly string WAITING_CANCE_CONFIRM_TEXT = "ｷｬﾝｾﾙ確定待ち";
            /// <summary>キャンセル確定待ちの背景色</summary>
            public static readonly Color WAITING_CANCE_CONFIRM_BACK_COLOR = Color.FromArgb(245, 166, 35);
            /// <summary>キャンセル確定待ちの文字色</summary>
            public static readonly Color WAITING_CANCE_CONFIRM_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>調整確定待ちのValue</summary>
            public const int WAITING_ADJUST_CONFIRM_VALUE = 7;
            /// <summary>調整確定待ちのTex</summary>
            public static readonly string WAITING_ADJUST_CONFIRM_TEXT = "調整確定待ち";
            /// <summary>調整確定待ちの背景色</summary>
            public static readonly Color WAITING_ADJUST_CONFIRM_BACK_COLOR = Color.FromArgb(105, 0, 48);
            /// <summary>調整確定待ちの文字色</summary>
            public static readonly Color WAITING_ADJUST_CONFIRM_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>調整中のValue</summary>
            public const int ADJUSTING_VALUE = 6;
            /// <summary>調整中のText</summary>
            public static readonly string ADJUSTING_TEXT = "調整中";
            /// <summary>調整中の背景色</summary>
            public static readonly Color ADJUSTING_BACK_COLOR = Color.FromArgb(119, 11, 246);
            /// <summary>調整中の文字色</summary>
            public static readonly Color ADJUSTING_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>キャンセルのValue</summary>
            public const int CANCELED_VALUE = 5;
            /// <summary>キャンセルのText</summary>
            public static readonly string CANCELED_TEXT = "キャンセル";
            /// <summary>キャンセルの背景色</summary>
            public static readonly Color CANCELED_BACK_COLOR = Color.FromArgb(23, 162, 184);
            /// <summary>キャンセルの文字色</summary>
            public static readonly Color CANCELED_FORE_COLOR = Color.FromArgb(255, 255, 255);


            /// <summary>不成立のValue</summary>
            public const int CANCEL_ADJUSTMENT_VALUE = 8;
            /// <summary>不成立のText</summary>
            public static readonly string CANCEL_ADJUSTMENT_TEXT = "不成立";
            /// <summary>不成立の背景色</summary>
            public static readonly Color CANCEL_ADJUSTMENT_BACK_COLOR = Color.FromArgb(108, 117, 125);
            /// <summary>不成立の文字色</summary>
            public static readonly Color CANCEL_ADJUSTMENT_FORE_COLOR = Color.FromArgb(255, 255, 255);
            
            /// <summary>取下げのValue</summary>
            public const int DISCARD_VALUE = 9;
            /// <summary>取下げのText</summary>
            public static readonly string DISCARD_TEXT = "取下げ ";
            /// <summary>取下げの背景色</summary>
            public static readonly Color DISCARD_BACK_COLOR = Color.FromArgb(211, 100, 100);
            /// <summary>取下げの文字色</summary>
            public static readonly Color DISCARD_FORE_COLOR = Color.FromArgb(255, 255, 255);

        }

        public const string PUBLIC_USER_CONFIRM_TEXT = "要確認";

        /// <summary>廃棄区分</summary>
        public static readonly Int16 HAIKI_KBN_CHOKKOU = 1;       // 直行
        public static readonly Int16 HAIKI_KBN_KENPAI = 2;　      // 建廃
        public static readonly Int16 HAIKI_KBN_TSUMIKAE = 3;　    // 積替
        public static readonly Int16 HAIKI_KBN_DENSHI = 4;        // 電子
    }
}
