using System.Collections;

namespace Shougun.Core.SalesPayment.UriageSuiiChouhyou
{
    /// <summary>
    /// G581で使用するDto
    /// </summary>
    public class UriageSuiihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public UriageSuiihyouDtoClass()
        {
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            DenpyouShuruiCd = 5;
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
            DateShuruiCd = 1;
            DateFrom = null;
            DateTo = null;
            TorihikiKbnCd = 3;
            KakuteiKbnCd = 3;
            ShimeJoukyouCd = 3;

            // 拠点
            KyotenCd = 99;
            KyotenName = null;
            // 取引先
            TorihikisakiCdFrom = null;
            TorihikisakiFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiTo = null;
            // 業者
            GyoushaCdFrom = null;
            GyoushaFrom = null;
            GyoushaCdTo = null;
            GyoushaTo = null;
            // 現場
            GenbaCdFrom = null;
            GenbaFrom = null;
            GenbaCdTo = null;
            GenbaTo = null;
            // 品名
            HinmeiCdFrom = null;
            HinmeiFrom = null;
            HinmeiCdTo = null;
            HinmeiTo = null;
            // 種類
            ShuruiCdFrom = null;
            ShuruiFrom = null;
            ShuruiCdTo = null;
            ShuruiTo = null;
            // 分類
            BunruiCdFrom = null;
            BunruiFrom = null;
            BunruiCdTo = null;
            BunruiTo = null;
            // 荷降業者
            NioroshiGyoushaCdFrom = null;
            NioroshiGyoushaFrom = null;
            NioroshiGyoushaCdTo = null;
            NioroshiGyoushaTo = null;
            // 荷降現場
            NioroshiGenbaCdFrom = null;
            NioroshiGenbaFrom = null;
            NioroshiGenbaCdTo = null;
            NioroshiGenbaTo = null;
            // 荷積業者
            NizumiGyoushaCdFrom = null;
            NizumiGyoushaFrom = null;
            NizumiGyoushaCdTo = null;
            NizumiGyoushaTo = null;
            // 荷積現場
            NizumiGenbaCdFrom = null;
            NizumiGenbaFrom = null;
            NizumiGenbaCdTo = null;
            NizumiGenbaTo = null;
            // 営業担当者
            EigyouTantoushaCdForm = null;
            EigyouTantoushaForm = null;
            EigyouTantoushaCdTo = null;
            EigyouTantoushaTo = null;
            // 入力担当者
            NyuuryokuTantoushaCdFrom = null;
            NyuuryokuTantoushaFrom = null;
            NyuuryokuTantoushaCdTo = null;
            NyuuryokuTantoushaTo = null;
            // 運搬業者
            UnpanGyoushaCdFrom = null;
            UnpanGyoushaFrom = null;
            UnpanGyoushaCdTo = null;
            UnpanGyoushaTo = null;
            // 車種
            ShashuCdFrom = null;
            ShashuFrom = null;
            ShashuCdTo = null;
            ShashuTo = null;
            // 車輛
            SharyouCdFrom = null;
            SharyouFrom = null;
            SharyouCdTo = null;
            SharyouTo = null;
            // 形態区分
            KeitaiKbnCdFrom = null;
            KeitaiKbnFrom = null;
            KeitaiKbnCdTo = null;
            KeitaiKbnTo = null;
            // 台貫
            DaikanCdFrom = null;
            DaikanFrom = null;
            DaikanCdTo = null;
            DaikanTo = null;
            // 条件
            Jyouken1 = null;
            Jyouken2 = null;
            
            Select = new ArrayList();
            SelectDate = null;
            Pivot = new ArrayList();
            MonthCount = 0;

            Pattern = null;
        }

        /// <summary>
        /// 伝票種類CDを取得・設定します
        /// </summary>
        public int DenpyouShuruiCd { get; set; }

        /// <summary>
        /// 日付種類CDを取得・設定します
        /// </summary>
        public int DateShuruiCd { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 取引区分CDを取得・設定します
        /// </summary>
        public int TorihikiKbnCd { get; set; }

        /// <summary>
        /// 確定区分CDを取得・設定します
        /// </summary>
        public int KakuteiKbnCd { get; set; }

        /// <summary>
        /// 締め状況CDを取得・設定します
        /// </summary>
        public int ShimeJoukyouCd { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名称を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先Fromを取得・設定します
        /// </summary>
        public string TorihikisakiFrom { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先Toを取得・設定します
        /// </summary>
        public string TorihikisakiTo { get; set; }

        /// <summary>
        /// 業者CDFromを取得・設定します
        /// </summary>
        public string GyoushaCdFrom { get; set; }

        /// <summary>
        /// 業者Fromを取得・設定します
        /// </summary>
        public string GyoushaFrom { get; set; }

        /// <summary>
        /// 業者CDToを取得・設定します
        /// </summary>
        public string GyoushaCdTo { get; set; }

        /// <summary>
        /// 業者Toを取得・設定します
        /// </summary>
        public string GyoushaTo { get; set; }

        /// <summary>
        /// 現場CDFromを取得・設定します
        /// </summary>
        public string GenbaCdFrom { get; set; }

        /// <summary>
        /// 現場Fromを取得・設定します
        /// </summary>
        public string GenbaFrom { get; set; }

        /// <summary>
        /// 現場CDToを取得・設定します
        /// </summary>
        public string GenbaCdTo { get; set; }

        /// <summary>
        /// 現場Toを取得・設定します
        /// </summary>
        public string GenbaTo { get; set; }

        /// <summary>
        /// 品名CDFromを取得・設定します
        /// </summary>
        public string HinmeiCdFrom { get; set; }

        /// <summary>
        /// 品名Fromを取得・設定します
        /// </summary>
        public string HinmeiFrom { get; set; }

        /// <summary>
        /// 品名CDToを取得・設定します
        /// </summary>
        public string HinmeiCdTo { get; set; }

        /// <summary>
        /// 品名Toを取得・設定します
        /// </summary>
        public string HinmeiTo { get; set; }

        /// <summary>
        /// 種類CDFromを取得・設定します
        /// </summary>
        public string ShuruiCdFrom { get; set; }

        /// <summary>
        /// 種類Fromを取得・設定します
        /// </summary>
        public string ShuruiFrom { get; set; }

        /// <summary>
        /// 種類CDToを取得・設定します
        /// </summary>
        public string ShuruiCdTo { get; set; }

        /// <summary>
        /// 種類Toを取得・設定します
        /// </summary>
        public string ShuruiTo { get; set; }

        /// <summary>
        /// 分類CDFromを取得・設定します
        /// </summary>
        public string BunruiCdFrom { get; set; }

        /// <summary>
        /// 分類Fromを取得・設定します
        /// </summary>
        public string BunruiFrom { get; set; }

        /// <summary>
        /// 分類CDToを取得・設定します
        /// </summary>
        public string BunruiCdTo { get; set; }

        /// <summary>
        /// 分類Toを取得・設定します
        /// </summary>
        public string BunruiTo { get; set; }
                
        /// <summary>
        /// 荷降業者CDFromを取得・設定します
        /// </summary>
        public string NioroshiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷降業者Fromを取得・設定します
        /// </summary>
        public string NioroshiGyoushaFrom { get; set; }

        /// <summary>
        /// 荷降業者CDToを取得・設定します
        /// </summary>
        public string NioroshiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷降業者Toを取得・設定します
        /// </summary>
        public string NioroshiGyoushaTo { get; set; }

        /// <summary>
        /// 荷降現場CDFromを取得・設定します
        /// </summary>
        public string NioroshiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷降現場Fromを取得・設定します
        /// </summary>
        public string NioroshiGenbaFrom { get; set; }

        /// <summary>
        /// 荷降現場CDToを取得・設定します
        /// </summary>
        public string NioroshiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷降現場Toを取得・設定します
        /// </summary>
        public string NioroshiGenbaTo { get; set; }

        /// <summary>
        /// 荷積業者CDFromを取得・設定します
        /// </summary>
        public string NizumiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷積業者Fromを取得・設定します
        /// </summary>
        public string NizumiGyoushaFrom { get; set; }

        /// <summary>
        /// 荷積業者CDToを取得・設定します
        /// </summary>
        public string NizumiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷積業者Toを取得・設定します
        /// </summary>
        public string NizumiGyoushaTo { get; set; }

        /// <summary>
        /// 荷積現場CDFromを取得・設定します
        /// </summary>
        public string NizumiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷積現場Fromを取得・設定します
        /// </summary>
        public string NizumiGenbaFrom { get; set; }

        /// <summary>
        /// 荷積現場CDToを取得・設定します
        /// </summary>
        public string NizumiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷積現場Toを取得・設定します
        /// </summary>
        public string NizumiGenbaTo { get; set; }

        /// <summary>
        /// 営業担当者CDFromを取得・設定します
        /// </summary>
        public string EigyouTantoushaCdForm { get; set; }

        /// <summary>
        /// 営業担当者Fromを取得・設定します
        /// </summary>
        public string EigyouTantoushaForm { get; set; }

        /// <summary>
        /// 営業担当者CDToを取得・設定します
        /// </summary>
        public string EigyouTantoushaCdTo { get; set; }

        /// <summary>
        /// 営業担当者Toを取得・設定します
        /// </summary>
        public string EigyouTantoushaTo { get; set; }

        /// <summary>
        /// 入力担当者CDFromを取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaCdFrom { get; set; }

        /// <summary>
        /// 入力担当者Fromを取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaFrom { get; set; }

        /// <summary>
        /// 入力担当者CDToを取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaCdTo { get; set; }

        /// <summary>
        /// 入力担当者Fromを取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaTo { get; set; }

        /// <summary>
        /// 運搬業者CDFromを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdFrom { get; set; }

        /// <summary>
        /// 運搬業者Fromを取得・設定します
        /// </summary>
        public string UnpanGyoushaFrom { get; set; }

        /// <summary>
        /// 運搬業者CDToを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdTo { get; set; }

        /// <summary>
        /// 運搬業者Toを取得・設定します
        /// </summary>
        public string UnpanGyoushaTo { get; set; }

        /// <summary>
        /// 車種CDFromを取得・設定します
        /// </summary>
        public string ShashuCdFrom { get; set; }

        /// <summary>
        /// 車種Fromを取得・設定します
        /// </summary>
        public string ShashuFrom { get; set; }

        /// <summary>
        /// 車種CDToを取得・設定します
        /// </summary>
        public string ShashuCdTo { get; set; }

        /// <summary>
        /// 車種Toを取得・設定します
        /// </summary>
        public string ShashuTo { get; set; }

        /// <summary>
        /// 車輛CDFromを取得・設定します
        /// </summary>
        public string SharyouCdFrom { get; set; }

        /// <summary>
        /// 車輛Fromを取得・設定します
        /// </summary>
        public string SharyouFrom { get; set; }

        /// <summary>
        /// 車輛CDToを取得・設定します
        /// </summary>
        public string SharyouCdTo { get; set; }

        /// <summary>
        /// 車輛Toを取得・設定します
        /// </summary>
        public string SharyouTo { get; set; }
        
        /// <summary>
        /// 形態区分CDFromを取得・設定します
        /// </summary>
        public string KeitaiKbnCdFrom { get; set; }

        /// <summary>
        /// 形態区分Fromを取得・設定します
        /// </summary>
        public string KeitaiKbnFrom { get; set; }

        /// <summary>
        /// 形態区分CDToを取得・設定します
        /// </summary>
        public string KeitaiKbnCdTo { get; set; }

        /// <summary>
        /// 形態区分Toを取得・設定します
        /// </summary>
        public string KeitaiKbnTo { get; set; }

        /// <summary>
        /// 台貫CDFromを取得・設定します
        /// </summary>
        public string DaikanCdFrom { get; set; }

        /// <summary>
        /// 台貫Fromを取得・設定します
        /// </summary>
        public string DaikanFrom { get; set; }

        /// <summary>
        /// 台貫CDToを取得・設定します
        /// </summary>
        public string DaikanCdTo { get; set; }

        /// <summary>
        /// 台貫Toを取得・設定します
        /// </summary>
        public string DaikanTo { get; set; }

        //PhuocLoc 2020/12/07 #136224 -Start
        /// <summary>
        /// 集計項目CDFromを取得・設定します
        /// </summary>
        public string ShuukeiKoumokuCdFrom { get; set; }

        /// <summary>
        /// 集計項目Fromを取得・設定します
        /// </summary>
        public string ShuukeiKoumokuFrom { get; set; }

        /// <summary>
        /// 集計項目CDToを取得・設定します
        /// </summary>
        public string ShuukeiKoumokuCdTo { get; set; }

        /// <summary>
        /// 集計項目Toを取得・設定します
        /// </summary>
        public string ShuukeiKoumokuTo { get; set; }
        //PhuocLoc 2020/12/07 #136224 -End

        /// <summary>
        /// 条件1を取得・設定します
        /// </summary>
        public string Jyouken1 { get; set; }

        /// <summary>
        /// 条件2を取得・設定します
        /// </summary>
        public string Jyouken2 { get; set; }

        /// <summary>
        /// SQLのSELECT句を取得・設定します
        /// </summary>
        public ArrayList Select { get; set; }

        /// <summary>
        /// SQLの日付SELECT句を取得・設定します
        /// </summary>
        public string SelectDate { get; set; }

        /// <summary>
        /// SQLのPIVOTに指定する日付を取得・設定します。
        /// </summary>
        public ArrayList Pivot { get; set; }

        /// <summary>
        /// 選択した月数を取得・設定します
        /// </summary>
        public int MonthCount { get; set; }

        /// <summary>
        /// パターン
        /// </summary>
        public ChouhyouPatternPopup.PatternDto Pattern { get; set; }
    }
}
