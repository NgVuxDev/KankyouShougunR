using System;
using System.Text;

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    public class ManifestShukeihyoDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ManifestShukeihyoDto()
        {
        }

        /// <summary>
        /// パターンを取得・設定します
        /// </summary>
        public PatternDto Pattern { get; set; }

        /// <summary>
        /// グループ化の対象カラムを取得・設定します
        /// </summary>
        public string GroupColumn { get; set; }

        /// <summary>
        /// パターンで運搬受託者項目の選択有無を取得・設定します
        /// </summary>
        public bool SelectedColumnUnpanJutakushaCd { get; set; }

        /// <summary>
        /// パターンで処分事業場項目の選択有無を取得・設定します
        /// </summary>
        public bool SelectedColumnShobunJigyoujouCd { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 拠点を取得します
        /// </summary>
        public string Kyoten { get { return "　[拠点] " + this.KyotenCd + " " + this.KyotenName + Environment.NewLine; } }

        /// <summary>
        /// 出力区分 「紙マニフェスト」を取得・設定します
        /// </summary>
        public bool IsKamiMani { get; set; }

        /// <summary>
        /// 出力区分 「電子マニフェスト」を取得・設定します
        /// </summary>
        public bool IsDenMani { get; set; }

        /// <summary>
        /// 出力区分を取得します
        /// </summary>
        public string OutputKbn
        {
            get
            {
                if (!IsKamiMani && !IsDenMani)
                {
                    return string.Empty;
                }

                StringBuilder sb = new StringBuilder();
                if (IsKamiMani)
                {
                    sb.Append("紙マニフェスト");
                }

                if (IsDenMani)
                {
                    if (0 < sb.Length)
                    {
                        sb.Append("、");
                    }

                    sb.Append("電子マニフェスト");
                }

                return "　[出力区分] " + sb.ToString() + Environment.NewLine;
            }
        }

        /// <summary>
        /// 一次二次区分 を取得・設定します
        /// </summary>
        public int IchijiNijiKbn { get; set; }

        /// <summary>
        /// 一次二次区分名を取得します
        /// </summary>
        public string IchijiNijiKbnName
        {
            get
            {
                var name = string.Empty;
                switch (IchijiNijiKbn)
                {
                    case 1:
                        name = "一次";
                        break;
                    case 2:
                        name = "二次";
                        break;
                    case 3:
                        name = "全て";
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(name))
                {
                    return name;
                }
                else
                {
                    return "　[一次二次区分] " + name + Environment.NewLine;
                }
            }
        }

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
        public string HaishutsuJigyoushaNameFrom { get; set; }

        /// <summary>
        /// 排出事業者Fromを取得します
        /// </summary>
        public string HaishutsuJigyoushaFrom { get { return this.HaishutsuJigyoushaCdFrom + " " + this.HaishutsuJigyoushaNameFrom; } }

        /// <summary>
        /// 排出事業者CDToを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaCdTo { get; set; }

        /// <summary>
        /// 排出事業者名Toを取得・設定します
        /// </summary>
        public string HaishutsuJigyoushaNameTo { get; set; }

        /// <summary>
        /// 排出事業者Toを取得します
        /// </summary>
        public string HaishutsuJigyoushaTo { get { return this.HaishutsuJigyoushaCdTo + " " + this.HaishutsuJigyoushaNameTo; } }

        /// <summary>
        /// 排出事業者を取得します
        /// </summary>
        public string HaishutsuJigyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.HaishutsuJigyoushaCdFrom) && string.IsNullOrEmpty(this.HaishutsuJigyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[排出事業者] " + this.HaishutsuJigyoushaFrom + " ～ " + this.HaishutsuJigyoushaTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 排出事業場CDFromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 排出事業場名Fromを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouNameFrom { get; set; }

        /// <summary>
        /// 排出事業場Fromを取得します
        /// </summary>
        public string HaishutsuJigyoujouFrom { get { return this.HaishutsuJigyoujouCdFrom + " " + this.HaishutsuJigyoujouNameFrom; } }

        /// <summary>
        /// 排出事業場CDToを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouCdTo { get; set; }

        /// <summary>
        /// 排出事業場名Toを取得・設定します
        /// </summary>
        public string HaishutsuJigyoujouNameTo { get; set; }

        /// <summary>
        /// 排出事業場Toを取得します
        /// </summary>
        public string HaishutsuJigyoujouTo { get { return this.HaishutsuJigyoujouCdTo + " " + this.HaishutsuJigyoujouNameTo; } }

        /// <summary>
        /// 排出事業場を取得します
        /// </summary>
        public string HaishutsuJigyoujou
        {
            get
            {
                if (string.IsNullOrEmpty(this.HaishutsuJigyoujouCdFrom) && string.IsNullOrEmpty(this.HaishutsuJigyoujouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[排出事業場] " + this.HaishutsuJigyoujouFrom + " ～ " + this.HaishutsuJigyoujouTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 運搬受託者CDFromを取得・設定します
        /// </summary>
        public string UnpanJutakushaCdFrom { get; set; }

        /// <summary>
        /// 運搬受託者名Fromを取得・設定します
        /// </summary>
        public string UnpanJutakushaNameFrom { get; set; }

        /// <summary>
        /// 運搬受託者Fromを取得します
        /// </summary>
        public string UnpanJutakushaFrom { get { return this.UnpanJutakushaCdFrom + " " + this.UnpanJutakushaNameFrom; } }

        /// <summary>
        /// 運搬受託者CDToを取得・設定します
        /// </summary>
        public string UnpanJutakushaCdTo { get; set; }

        /// <summary>
        /// 運搬受託者名Toを取得・設定します
        /// </summary>
        public string UnpanJutakushaNameTo { get; set; }

        /// <summary>
        /// 運搬受託者Toを取得します
        /// </summary>
        public string UnpanJutakushaTo { get { return this.UnpanJutakushaCdTo + " " + this.UnpanJutakushaNameTo; } }

        /// <summary>
        /// 運搬受託者を取得します
        /// </summary>
        public string UnpanJutakusha
        {
            get
            {
                if (string.IsNullOrEmpty(this.UnpanJutakushaCdFrom) && string.IsNullOrEmpty(this.UnpanJutakushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[運搬受託者] " + this.UnpanJutakushaFrom + " ～ " + this.UnpanJutakushaTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 処分事業者CDFromを取得・設定します
        /// </summary>
        public string ShobunJigyoushaCdFrom { get; set; }

        /// <summary>
        /// 処分事業者名Fromを取得・設定します
        /// </summary>
        public string ShobunJigyoushaNameFrom { get; set; }

        /// <summary>
        /// 処分事業者Fromを取得します
        /// </summary>
        public string ShobunJigyoushaFrom { get { return this.ShobunJigyoushaCdFrom + " " + this.ShobunJigyoushaNameFrom; } }

        /// <summary>
        /// 処分事業者CDToを取得・設定します
        /// </summary>
        public string ShobunJigyoushaCdTo { get; set; }

        /// <summary>
        /// 処分事業者名Toを取得・設定します
        /// </summary>
        public string ShobunJigyoushaNameTo { get; set; }

        /// <summary>
        /// 処分事業者Toを取得します
        /// </summary>
        public string ShobunJigyoushaTo { get { return this.ShobunJigyoushaCdTo + " " + this.ShobunJigyoushaNameTo; } }

        /// <summary>
        /// 処分事業者を取得します
        /// </summary>
        public string ShobunJigyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.ShobunJigyoushaCdFrom) && string.IsNullOrEmpty(this.ShobunJigyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[処分受託者] " + this.ShobunJigyoushaFrom + " ～ " + this.ShobunJigyoushaTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 処分事業場CDFromを取得・設定します
        /// </summary>
        public string ShobunJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 処分事業場名Fromを取得・設定します
        /// </summary>
        public string ShobunJigyoujouNameFrom { get; set; }

        /// <summary>
        /// 処分事業場Fromを取得します
        /// </summary>
        public string ShobunJigyoujouFrom { get { return this.ShobunJigyoujouCdFrom + " " + this.ShobunJigyoujouNameFrom; } }

        /// <summary>
        /// 処分事業場CDToを取得・設定します
        /// </summary>
        public string ShobunJigyoujouCdTo { get; set; }

        /// <summary>
        /// 処分事業場名Toを取得・設定します
        /// </summary>
        public string ShobunJigyoujouNameTo { get; set; }

        /// <summary>
        /// 処分事業場Toを取得します
        /// </summary>
        public string ShobunJigyoujouTo { get { return this.ShobunJigyoujouCdTo + " " + this.ShobunJigyoujouNameTo; } }

        /// <summary>
        /// 処分事業場を取得します
        /// </summary>
        public string ShobunJigyoujou
        {
            get
            {
                if (string.IsNullOrEmpty(this.ShobunJigyoujouCdFrom) && string.IsNullOrEmpty(this.ShobunJigyoujouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[処分事業場] " + this.ShobunJigyoujouFrom + " ～ " + this.ShobunJigyoujouTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 最終処分業者CDFromを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaCdFrom { get; set; }

        /// <summary>
        /// 最終処分業者名Fromを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaNameFrom { get; set; }

        /// <summary>
        /// 最終処分業者Fromを取得します
        /// </summary>
        public string LastShobunJigyoushaFrom { get { return this.LastShobunJigyoushaCdFrom + " " + this.LastShobunJigyoushaNameFrom; } }

        /// <summary>
        /// 最終処分事業者CDToを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaCdTo { get; set; }

        /// <summary>
        /// 最終処分事業者名Toを取得・設定します
        /// </summary>
        public string LastShobunJigyoushaNameTo { get; set; }

        /// <summary>
        /// 最終処分事業者Toを取得します
        /// </summary>
        public string LastShobunJigyoushaTo { get { return this.LastShobunJigyoushaCdTo + " " + this.LastShobunJigyoushaNameTo; } }

        /// <summary>
        /// 最終処分事業者を取得します
        /// </summary>
        public string LastShobunJigyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.LastShobunJigyoushaCdFrom) && string.IsNullOrEmpty(this.LastShobunJigyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[最終処分業者] " + this.LastShobunJigyoushaFrom + " ～ " + this.LastShobunJigyoushaTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 最終処分事業場CDFromを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouCdFrom { get; set; }

        /// <summary>
        /// 最終処分事業場名Fromを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouNameFrom { get; set; }

        /// <summary>
        /// 最終処分事業場Fromを取得します
        /// </summary>
        public string LastShobunJigyoujouFrom { get { return this.LastShobunJigyoujouCdFrom + " " + this.LastShobunJigyoujouNameFrom; } }

        /// <summary>
        /// 最終処分事業場CDToを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouCdTo { get; set; }

        /// <summary>
        /// 最終処分事業場名Toを取得・設定します
        /// </summary>
        public string LastShobunJigyoujouNameTo { get; set; }

        /// <summary>
        /// 最終処分事業場Toを取得します
        /// </summary>
        public string LastShobunJigyoujouTo { get { return this.LastShobunJigyoujouCdTo + " " + this.LastShobunJigyoujouNameTo; } }

        /// <summary>
        /// 最終処分事業場を取得します
        /// </summary>
        public string LastShobunJigyoujou
        {
            get
            {
                if (string.IsNullOrEmpty(this.LastShobunJigyoujouCdFrom) && string.IsNullOrEmpty(this.LastShobunJigyoujouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[最終処分場所] " + this.LastShobunJigyoujouFrom + " ～ " + this.LastShobunJigyoujouTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 廃棄物種類(報告書分類)CDFromを取得・設定します
        /// </summary>
        public string HoukokushoBunruiCdFrom { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)名Fromを取得・設定します
        /// </summary>
        public string HoukokushoBunruiNameFrom { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)Fromを取得します
        /// </summary>
        public string HoukokushoBunruiFrom { get { return this.HoukokushoBunruiCdFrom + " " + this.HoukokushoBunruiNameFrom; } }

        /// <summary>
        /// 廃棄物種類(報告書分類)CDToを取得・設定します
        /// </summary>
        public string HoukokushoBunruiCdTo { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)名Toを取得・設定します
        /// </summary>
        public string HoukokushoBunruiNameTo { get; set; }

        /// <summary>
        /// 廃棄物種類(報告書分類)Toを取得します
        /// </summary>
        public string HoukokushoBunruiTo { get { return this.HoukokushoBunruiCdTo + " " + this.HoukokushoBunruiNameTo; } }

        /// <summary>
        /// 廃棄物種類(報告書分類)を取得します
        /// </summary>
        public string HoukokushoBunrui
        {
            get
            {
                if (string.IsNullOrEmpty(this.HoukokushoBunruiCdFrom) && string.IsNullOrEmpty(this.HoukokushoBunruiCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[廃棄物種類(報告書分類)] " + this.HoukokushoBunruiFrom + " ～ " + this.HoukokushoBunruiTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 廃棄物名称CDFromを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouCdFrom { get; set; }

        /// <summary>
        /// 廃棄物名称Fromを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouNameFrom { get; set; }

        /// <summary>
        /// 廃棄物Fromを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouFrom { get { return this.HaikibutsuMeishouCdFrom + " " + this.HaikibutsuMeishouNameFrom; } }

        /// <summary>
        /// 廃棄物名称CDToを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouCdTo { get; set; }

        /// <summary>
        /// 廃棄物名称Toを取得・設定します
        /// </summary>
        public string HaikibutsuMeishouNameTo { get; set; }

        /// <summary>
        /// 廃棄物Toを取得します
        /// </summary>
        public string HaikibutsuMeishouTo { get { return this.HaikibutsuMeishouCdTo + " " + this.HaikibutsuMeishouNameTo; } }

        /// <summary>
        /// 廃棄物を取得します
        /// </summary>
        public string HaikibutsuMeishou
        {
            get
            {
                if (string.IsNullOrEmpty(this.HaikibutsuMeishouCdFrom) && string.IsNullOrEmpty(this.HaikibutsuMeishouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[廃棄物名称] " + this.HaikibutsuMeishouFrom + " ～ " + this.HaikibutsuMeishouTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 処分方法CDFromを取得・設定します
        /// </summary>
        public string ShobunHouhouCdFrom { get; set; }

        /// <summary>
        /// 処分方法名Fromを取得・設定します
        /// </summary>
        public string ShobunHouhouNameFrom { get; set; }

        /// <summary>
        /// 処分方法Fromを取得します
        /// </summary>
        public string ShobunHouhouFrom { get { return this.ShobunHouhouCdFrom + " " + this.ShobunHouhouNameFrom; } }

        /// <summary>
        /// 処分方法CDToを取得・設定します
        /// </summary>
        public string ShobunHouhouCdTo { get; set; }

        /// <summary>
        /// 処分方法名Toを取得・設定します
        /// </summary>
        public string ShobunHouhouNameTo { get; set; }

        /// <summary>
        /// 処分方法Toを取得します
        /// </summary>
        public string ShobunHouhouTo { get { return this.ShobunHouhouCdTo + " " + this.ShobunHouhouNameTo; } }

        /// <summary>
        /// 処分方法を取得します
        /// </summary>
        public string ShobunHouhou
        {
            get
            {
                if (string.IsNullOrEmpty(this.ShobunHouhouCdFrom) && string.IsNullOrEmpty(this.ShobunHouhouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[処分方法] " + this.ShobunHouhouFrom + " ～ " + this.ShobunHouhouTo + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先名Fromを取得・設定します
        /// </summary>
        public string TorihikisakiNameFrom { get; set; }

        /// <summary>
        /// 取引先Fromを取得します
        /// </summary>
        public string TorihikisakiFrom { get { return this.TorihikisakiCdFrom + " " + this.TorihikisakiNameFrom; } }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先名Toを取得・設定します
        /// </summary>
        public string TorihikisakiNameTo { get; set; }

        /// <summary>
        /// 取引先Toを取得します
        /// </summary>
        public string TorihikisakiTo { get { return this.TorihikisakiCdTo + " " + this.TorihikisakiNameTo; } }

        /// <summary>
        /// 取引先を取得します
        /// </summary>
        public string Torihikisaki
        {
            get
            {
                if (string.IsNullOrEmpty(this.TorihikisakiCdFrom) && string.IsNullOrEmpty(this.TorihikisakiCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[取引先] " + this.TorihikisakiFrom + "  " + this.TorihikisakiTo + Environment.NewLine;
                }
            }
        }

    }
}
