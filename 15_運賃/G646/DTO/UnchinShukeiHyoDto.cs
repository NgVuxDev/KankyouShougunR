using System;
using ChouhyouPatternPopup;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表DTOクラス
    /// </summary>
    public class UnchinShukeiHyoDto
    {
        /// <summary>
        /// パターンを取得・設定します
        /// </summary>
        public PatternDto Pattern { get; set; }

        /// <summary>
        /// 日付種類を取得・設定します
        /// </summary>
        public int DateShurui { get; set; }

        /// <summary>
        /// 日付種類名を取得・設定します
        /// </summary>
        public string DateShuruiName { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// 伝票種類を取得・設定します
        /// </summary>
        public int DenpyouShurui { get; set; }

        /// <summary>
        /// 伝票種類名を取得・設定します
        /// </summary>
        public string DenpyouShuruiName { get; set; }

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
        public string Kyoten { get { return "　[拠点] " + this.KyotenCd + " " + this.KyotenName + "\r\n"; } }

        /// <summary>
        /// 荷降業者CD Fromを取得・設定します
        /// </summary>
        public string NioroshiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷降業者名 Fromを取得・設定します
        /// </summary>
        public string NioroshiGyoushaNameFrom { get; set; }

        /// <summary>
        /// 荷降業者 Fromを取得します
        /// </summary>
        public string NioroshiGyoushaFrom { get { return this.NioroshiGyoushaCdFrom + " " + this.NioroshiGyoushaNameFrom; } }

        /// <summary>
        /// 荷降業者CD Toを取得・設定します
        /// </summary>
        public string NioroshiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷降業者名 Toを取得・設定します
        /// </summary>
        public string NioroshiGyoushaNameTo { get; set; }

        /// <summary>
        /// 荷降業者 Toを取得します
        /// </summary>
        public string NioroshiGyoushaTo { get { return this.NioroshiGyoushaCdTo + " " + this.NioroshiGyoushaNameTo; } }

        /// <summary>
        /// 荷降業者を取得します
        /// </summary>
        public string NioroshiGyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.NioroshiGyoushaCdFrom) && string.IsNullOrEmpty(this.NioroshiGyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[荷降業者] " + this.NioroshiGyoushaFrom + " ～ " + this.NioroshiGyoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 荷降現場CD Fromを取得・設定します
        /// </summary>
        public string NioroshiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷降現場名 Fromを取得・設定します
        /// </summary>
        public string NioroshiGenbaNameFrom { get; set; }

        /// <summary>
        /// 荷降現場 Fromを取得します
        /// </summary>
        public string NioroshiGenbaFrom { get { return this.NioroshiGenbaCdFrom + " " + this.NioroshiGenbaNameFrom; } }

        /// <summary>
        /// 荷降現場CD Toを取得・設定します
        /// </summary>
        public string NioroshiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷降現場名 Toを取得・設定します
        /// </summary>
        public string NioroshiGenbaNameTo { get; set; }

        /// <summary>
        /// 荷降現場 Toを取得します
        /// </summary>
        public string NioroshiGenbaTo { get { return this.NioroshiGenbaCdTo + " " + this.NioroshiGenbaNameTo; } }

        /// <summary>
        /// 荷降現場を取得します
        /// </summary>
        public string NioroshiGenba
        {
            get
            {
                if (string.IsNullOrEmpty(this.NioroshiGenbaCdFrom) && string.IsNullOrEmpty(this.NioroshiGenbaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[荷降現場] " + this.NioroshiGenbaFrom + " ～ " + this.NioroshiGenbaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 荷積業者CD Fromを取得・設定します
        /// </summary>
        public string NizumiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷積業者名 Fromを取得・設定します
        /// </summary>
        public string NizumiGyoushaNameFrom { get; set; }

        /// <summary>
        /// 荷積業者 Fromを取得します
        /// </summary>
        public string NizumiGyoushaFrom { get { return this.NizumiGyoushaCdFrom + " " + this.NizumiGyoushaNameFrom; } }

        /// <summary>
        /// 荷積業者CD Toを取得・設定します
        /// </summary>
        public string NizumiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷積業者名 Toを取得・設定します
        /// </summary>
        public string NizumiGyoushaNameTo { get; set; }

        /// <summary>
        /// 荷積業者 Toを取得します
        /// </summary>
        public string NizumiGyoushaTo { get { return this.NizumiGyoushaCdTo + " " + this.NizumiGyoushaNameTo; } }

        /// <summary>
        /// 荷積業者を取得します
        /// </summary>
        public string NizumiGyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.NizumiGyoushaCdFrom) && string.IsNullOrEmpty(this.NizumiGyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[荷積業者] " + this.NizumiGyoushaFrom + " ～ " + this.NizumiGyoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 荷積現場CD Fromを取得・設定します
        /// </summary>
        public string NizumiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷積現場名 Fromを取得・設定します
        /// </summary>
        public string NizumiGenbaNameFrom { get; set; }

        /// <summary>
        /// 荷積現場 Fromを取得します
        /// </summary>
        public string NizumiGenbaFrom { get { return this.NizumiGenbaCdFrom + " " + this.NizumiGenbaNameFrom; } }

        /// <summary>
        /// 荷積現場CD Toを取得・設定します
        /// </summary>
        public string NizumiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷積現場名 Toを取得・設定します
        /// </summary>
        public string NizumiGenbaNameTo { get; set; }

        /// <summary>
        /// 荷積現場 Toを取得します
        /// </summary>
        public string NizumiGenbaTo { get { return this.NizumiGenbaCdTo + " " + this.NizumiGenbaNameTo; } }

        /// <summary>
        /// 荷積現場を取得します
        /// </summary>
        public string NizumiGenba
        {
            get
            {
                if (string.IsNullOrEmpty(this.NizumiGenbaCdFrom) && string.IsNullOrEmpty(this.NizumiGenbaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[荷積現場] " + this.NizumiGenbaFrom + " ～ " + this.NizumiGenbaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 運搬業者CD Fromを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdFrom { get; set; }

        /// <summary>
        /// 運搬業者名 Fromを取得・設定します
        /// </summary>
        public string UnpanGyoushaNameFrom { get; set; }

        /// <summary>
        /// 運搬業者 Fromを取得します
        /// </summary>
        public string UnpanGyoushaFrom { get { return this.UnpanGyoushaCdFrom + " " + this.UnpanGyoushaNameFrom; } }

        /// <summary>
        /// 運搬業者CD Toを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdTo { get; set; }

        /// <summary>
        /// 運搬業者名 Toを取得・設定します
        /// </summary>
        public string UnpanGyoushaNameTo { get; set; }

        /// <summary>
        /// 運搬業者 Toを取得します
        /// </summary>
        public string UnpanGyoushaTo { get { return this.UnpanGyoushaCdTo + " " + this.UnpanGyoushaNameTo; } }

        /// <summary>
        /// 運搬業者を取得します
        /// </summary>
        public string UnpanGyousha
        {
            get
            {
                if (string.IsNullOrEmpty(this.UnpanGyoushaCdFrom) && string.IsNullOrEmpty(this.UnpanGyoushaCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[運搬業者] " + this.UnpanGyoushaFrom + " ～ " + this.UnpanGyoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 車種CD From取得・設定します
        /// </summary>
        public string ShashuCdFrom { get; set; }

        /// <summary>
        /// 車種名 Fromを取得・設定します
        /// </summary>
        public string ShashuNameFrom { get; set; }

        /// <summary>
        /// 車種 Fromを取得・設定します
        /// </summary>
        public string ShashuFrom { get { return this.ShashuCdFrom + " " + this.ShashuNameFrom; } }

        /// <summary>
        /// 車種CD Toを取得・設定します
        /// </summary>
        public string ShashuCdTo { get; set; }

        /// <summary>
        /// 車種名 Toを取得・設定します
        /// </summary>
        public string ShashuNameTo { get; set; }

        /// <summary>
        /// 車種 Toを取得します
        /// </summary>
        public string ShashuTo { get { return this.ShashuCdTo + " " + this.ShashuNameTo; } }

        /// <summary>
        /// 車種を取得します
        /// </summary>
        public string Shashu
        {
            get
            {
                if (string.IsNullOrEmpty(this.ShashuCdFrom) && string.IsNullOrEmpty(this.ShashuCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[車種] " + this.ShashuFrom + " ～ " + this.ShashuTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 車輌CD Fromを取得・設定します
        /// </summary>
        public string SharyouCdFrom { get; set; }

        /// <summary>
        /// 車輌名 Fromを取得・設定します
        /// </summary>
        public string SharyouNameFrom { get; set; }

        /// <summary>
        /// 車輌 Fromを取得します
        /// </summary>
        public string SharyouFrom { get { return this.SharyouCdFrom + " " + this.SharyouNameFrom; } }

        /// <summary>
        /// 車輌CD Toを取得・設定します
        /// </summary>
        public string SharyouCdTo { get; set; }

        /// <summary>
        /// 車輌名 Toを取得・設定します
        /// </summary>
        public string SharyouNameTo { get; set; }

        /// <summary>
        /// 車輌 Toを取得します
        /// </summary>
        public string SharyouTo { get { return this.SharyouCdTo + " " + this.SharyouNameTo; } }

        /// <summary>
        /// 車輌を取得します
        /// </summary>
        public string Sharyou
        {
            get
            {
                if (string.IsNullOrEmpty(this.SharyouCdFrom) && string.IsNullOrEmpty(this.SharyouCdTo))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[車輌] " + this.SharyouFrom + " ～ " + this.SharyouTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 形態区分CD Fromを取得・設定します
        /// </summary>
        public string KeitaiKbnCdFrom { get; set; }

        /// <summary>
        /// 形態区分名 Fromを取得・設定します
        /// </summary>
        public string KeitaiKbnNameFrom { get; set; }

        /// <summary>
        /// 形態区分 Fromを取得します
        /// </summary>
        public string KeitaiKbnFrom { get { return this.KeitaiKbnCdFrom + " " + this.KeitaiKbnNameFrom + "\r\n"; } }

        /// <summary>
        /// 形態区分を取得します
        /// </summary>
        public string KeitaiKbn
        {
            get
            {
                if (string.IsNullOrEmpty(this.KeitaiKbnCdFrom))
                {
                    return string.Empty;
                }
                else
                {
                    return "　[形態区分] " + this.KeitaiKbnFrom;
                }
            }
        }

    }
}
