using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表 条件DTO
    /// </summary>
    public class ManifestMeisaihyouDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ManifestMeisaihyouDto()
        {
            this.KyotenCd = 99;
            this.Kyoten = "全社";
            this.DateFrom = null;
            this.DateTo = null;
            this.IsKamiMani = true;
            this.IsDenMani = true;
            this.IchijiKbn = 1;
            this.NijiHimozuke = 1;
            this.KofuDateFrom = null;
            this.KofuDateTo = null;
            this.UnpanEndDateFrom = null;
            this.UnpanEndDateTo = null;
            this.ShobunEndDateFrom = null;
            this.ShobunEndDateTo = null;
            this.LastShobunEndDateFrom = null;
            this.LastShobunEndDateTo = null;
            this.HaishutsuJigyoushaCdFrom = null;
            this.HaishutsuJigyoushaFrom = null;
            this.HaishutsuJigyoushaCdTo = null;
            this.HaishutsuJigyoushaTo = null;
            this.HaishutsuJigyoujouCdFrom = null;
            this.HaishutsuJigyoujouFrom = null;
            this.HaishutsuJigyoujouCdTo = null;
            this.HaishutsuJigyoujouTo = null;
            this.UnpanJutakushaCdFrom = null;
            this.UnpanJutakushaFrom = null;
            this.UnpanJutakushaCdTo = null;
            this.UnpanJutakushaTo = null;
            this.ShobunJigyoushaCdFrom = null;
            this.ShobunJigyoushaFrom = null;
            this.ShobunJigyoushaCdTo = null;
            this.ShobunJigyoushaTo = null;
            this.ShobunJigyoujouCdFrom = null;
            this.ShobunJigyoujouFrom = null;
            this.ShobunJigyoujouCdTo = null;
            this.ShobunJigyoujouTo = null;
            this.LastShobunJigyoushaCdFrom = null;
            this.LastShobunJigyoushaFrom = null;
            this.LastShobunJigyoushaCdTo = null;
            this.LastShobunJigyoushaTo = null;
            this.LastShobunJigyoujouCdFrom = null;
            this.LastShobunJigyoujouFrom = null;
            this.LastShobunJigyoujouCdTo = null;
            this.LastShobunJigyoujouTo = null;
            this.HoukokushoBunruiCdFrom = null;
            this.HoukokushoBunruiFrom = null;
            this.HoukokushoBunruiCdTo = null;
            this.HoukokushoBunruiTo = null;
            this.HaikibutsuMeishouCdFrom = null;
            this.HaikibutsuMeishouFrom = null;
            this.HaikibutsuMeishouCdTo = null;
            this.HaikibutsuMeishouTo = null;
            this.NisugataCdFrom = null;
            this.NisugataFrom = null;
            this.NisugataCdTo = null;
            this.NisugataTo = null;
            this.ShobunHouhouCdFrom = null;
            this.ShobunHouhouFrom = null;
            this.ShobunHouhouCdTo = null;
            this.ShobunHouhouTo = null;
            this.TorihikisakiCdFrom = null;
            this.TorihikisakiFrom = null;
            this.TorihikisakiCdTo = null;
            this.TorihikisakiTo = null;
            this.Sort = ConstClass.SORT_KOFU_DATE;
            this.IsGroupDate = true;
            this.IsGroupHaishutsuJigyousha = false;
            this.IsGroupHaishutsuJigyoujou = false;
            this.IsGroupUnpanJutakusha1 = false;
            this.IsGroupUnpanJutakusha2 = false;
            this.IsGroupShobunJigyoujou = false;
            this.IsGroupHoukokushoBunrui = false;
            this.IsGroupLastShobunGenba = false;
            this.IsGroupShobunHouhou = false;
        }

        #region     プロパティ

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点を取得・設定します
        /// </summary>
        public string Kyoten { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 出力区分 「紙マニフェスト」を取得・設定します
        /// </summary>
        public bool IsKamiMani { get; set; }

        /// <summary>
        /// 出力区分 「電子マニフェスト」を取得・設定します
        /// </summary>
        public bool IsDenMani { get; set; }

        /// <summary>
        /// 一次二次区分 「一次」を取得・設定します
        /// </summary>
        public int IchijiKbn { get; set; }

        /// <summary>
        /// 一次二次区分 「二次」を取得・設定します
        /// </summary>
        public int NijiHimozuke { get; set; }

        /// <summary>
        /// 交付年月日Fromを取得・設定します
        /// </summary>
        public string KofuDateFrom { get; set; }

        /// <summary>
        /// 交付年月日Toを取得・設定します
        /// </summary>
        public string KofuDateTo { get; set; }

        /// <summary>
        /// 運搬終了日Fromを取得・設定します
        /// </summary>
        public string UnpanEndDateFrom { get; set; }

        /// <summary>
        /// 運搬終了日Toを取得・設定します
        /// </summary>
        public string UnpanEndDateTo { get; set; }

        /// <summary>
        /// 処分終了日Fromを取得・設定します
        /// </summary>
        public string ShobunEndDateFrom { get; set; }

        /// <summary>
        /// 処分終了日Toを取得・設定します
        /// </summary>
        public string ShobunEndDateTo { get; set; }

        /// <summary>
        /// 最終処分日Fromを取得・設定します
        /// </summary>
        public string LastShobunEndDateFrom { get; set; }

        /// <summary>
        /// 最終処分日Toを取得・設定します
        /// </summary>
        public string LastShobunEndDateTo { get; set; }

        /// <summary>
        /// 排出事業者CDFromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaCdFrom { get; set; }

        /// <summary>
        /// 排出事業者名Fromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaFrom { get; set; }

        /// <summary>
        /// 排出事業者CDToを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaCdTo { get; set; }

        /// <summary>
        /// 排出事業者名Toを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaTo { get; set; }

        /// <summary>
        /// 排出事業場CDFromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 排出事業場名Fromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouFrom { get; set; }

        /// <summary>
        /// 排出事業場CDToを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouCdTo { get; set; }

        /// <summary>
        /// 排出事業場名Toを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouTo { get; set; }

        /// <summary>
        /// 運搬受託者CDFromを取得・設定します
        /// </summary>
        public string UnpanJutakushaCdFrom { get; set; }

        /// <summary>
        /// 運搬受託者名Fromを取得・設定します
        /// </summary>
        public string UnpanJutakushaFrom { get; set; }

        /// <summary>
        /// 運搬受託者CDToを取得・設定します
        /// </summary>
        public string UnpanJutakushaCdTo { get; set; }

        /// <summary>
        /// 運搬受託者名Toを取得・設定します
        /// </summary>
        public string UnpanJutakushaTo { get; set; }

        /// <summary>
        /// 処分事業者CDFromを取得・設定します
        /// </summary>
        public string ShobunJigyoushaCdFrom { get; set; }

        /// <summary>
        /// 処分事業者名Fromを取得・設定します
        /// </summary>
        public string ShobunJigyoushaFrom { get; set; }

        /// <summary>
        /// 処分事業者CDToを取得・設定します
        /// </summary>
        public string ShobunJigyoushaCdTo { get; set; }

        /// <summary>
        /// 処分事業者名Toを取得・設定します
        /// </summary>
        public string ShobunJigyoushaTo { get; set; }

        /// <summary>
        /// 処分事業場CDFromを取得・設定します
        /// </summary>
        public string ShobunJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 処分事業場名Fromを取得・設定します
        /// </summary>
        public string ShobunJigyoujouFrom { get; set; }

        /// <summary>
        /// 処分事業場CDToを取得・設定します
        /// </summary>
        public string ShobunJigyoujouCdTo { get; set; }

        /// <summary>
        /// 処分事業場名Toを取得・設定します
        /// </summary>
        public string ShobunJigyoujouTo { get; set; }

        /// <summary>
        /// 最終処分業者CDFromを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaCdFrom { get; set; }

        /// <summary>
        /// 最終処分業者名Fromを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaFrom { get; set; }

        /// <summary>
        /// 最終処分事業者CDToを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaCdTo { get; set; }

        /// <summary>
        /// 最終処分事業者名Toを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaTo { get; set; }

        /// <summary>
        /// 最終処分事業場CDFromを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 最終処分事業場名Fromを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouFrom { get; set; }

        /// <summary>
        /// 最終処分事業場CDToを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouCdTo { get; set; }

        /// <summary>
        /// 最終処分事業場名Toを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouTo { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)CDFromを取得・設定します
        /// </summary>
        public string HoukokushoBunruiCdFrom { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)名Fromを取得・設定します
        /// </summary>
        public string HoukokushoBunruiFrom { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)CDToを取得・設定します
        /// </summary>
        public string HoukokushoBunruiCdTo { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)名Toを取得・設定します
        /// </summary>
        public string HoukokushoBunruiTo { get; set; }

        /// <summary>
        /// 廃棄物名称CDFromを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouCdFrom { get; set; }

        /// <summary>
        /// 廃棄物名称Fromを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouFrom { get; set; }

        /// <summary>
        /// 廃棄物名称CDToを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouCdTo { get; set; }

        /// <summary>
        /// 廃棄物名称Toを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouTo { get; set; }

        /// <summary>
        /// 荷姿CDFromを取得・設定します
        /// </summary>
        public string NisugataCdFrom { get; set; }

        /// <summary>
        /// 荷姿名Fromを取得・設定します
        /// </summary>
        public string NisugataFrom { get; set; }

        /// <summary>
        /// 荷姿CDToを取得・設定します
        /// </summary>
        public string NisugataCdTo { get; set; }

        /// <summary>
        /// 荷姿名Toを取得・設定します
        /// </summary>
        public string NisugataTo { get; set; }

        /// <summary>
        /// 処分方法CDFromを取得・設定します
        /// </summary>
        public string ShobunHouhouCdFrom { get; set; }

        /// <summary>
        /// 処分方法名Fromを取得・設定します
        /// </summary>
        public string ShobunHouhouFrom { get; set; }

        /// <summary>
        /// 処分方法CDToを取得・設定します
        /// </summary>
        public string ShobunHouhouCdTo { get; set; }

        /// <summary>
        /// 処分方法名Toを取得・設定します
        /// </summary>
        public string ShobunHouhouTo { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先名Fromを取得・設定します
        /// </summary>
        public string TorihikisakiFrom { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先名Toを取得・設定します
        /// </summary>
        public string TorihikisakiTo { get; set; }

        /// <summary>
        /// 並び順を取得・設定します
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 集計「日付」を取得・設定します
        /// </summary>
        public bool IsGroupDate { get; set; }

        /// <summary>
        /// 集計「排出事業者」を取得・設定します
        /// </summary>
        public bool IsGroupHaishutsuJigyousha { get; set; }

        /// <summary>
        /// 集計「排出事業場」を取得・設定します
        /// </summary>
        public bool IsGroupHaishutsuJigyoujou { get; set; }

        /// <summary>
        /// 集計「運搬受託者１」を取得・設定します
        /// </summary>
        public bool IsGroupUnpanJutakusha1 { get; set; }

        /// <summary>
        /// 集計「運搬受託者２」を取得・設定します
        /// </summary>
        public bool IsGroupUnpanJutakusha2 { get; set; }

        /// <summary>
        /// 集計「処分事業場」を取得・設定します
        /// </summary>
        public bool IsGroupShobunJigyoujou { get; set; }

        /// <summary>
        /// 集計「廃棄物種類(報告書分類)」を取得・設定します
        /// </summary>
        public bool IsGroupHoukokushoBunrui { get; set; }

        /// <summary>
        /// 集計「最終処分場」を取得・設定します
        /// </summary>
        public bool IsGroupLastShobunGenba { get; set; }

        /// <summary>
        /// 集計「処分方法」を取得・設定します
        /// </summary>
        public bool IsGroupShobunHouhou { get; set; }

        #endregion  プロパティ
    }
}
