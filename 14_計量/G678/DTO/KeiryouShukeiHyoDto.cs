using ChouhyouPatternPopup;
using System;

namespace Shougun.Core.Scale.KeiryouShukeiHyo
{
    /// <summary>
    /// 計量集計表DTOクラス
    /// </summary>
    public class KeiryouShukeiHyoDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public KeiryouShukeiHyoDto()
        {
        }

        /// <summary>
        /// パターンを取得・設定します
        /// </summary>
        public PatternDto Pattern { get; set; }

        /// <summary>
        /// 基本計量を取得・設定します
        /// </summary>
        public int KihonKeiryou { get; set; }

        /// <summary>
        /// 伝票区分を取得・設定します
        /// </summary>
        public int DenpyouKbn { get; set; }

        /// <summary>
        /// 伝票区分名を取得・設定します
        /// </summary>
        public String DenpyouKbnName { get; set; }

        /// <summary>
        /// 日付種類を取得・設定します
        /// </summary>
        public int DateShurui { get; set; }

        /// <summary>
        /// 日付種類名を取得・設定します
        /// </summary>
        public String DateShuruiName { get; set; }

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
        public String DenpyouShuruiName { get; set; }

        /// <summary>
        /// 請求取引区分を取得・設定します
        /// </summary>
        public int UriageTorihikiKbn { get; set; }

        /// <summary>
        /// 請求取引区分名を取得・設定します
        /// </summary>
        public String UriageTorihikiKbnName { get; set; }

        /// <summary>
        /// 支払取引区分を取得・設定します
        /// </summary>
        public int ShiharaiTorihikiKbn { get; set; }

        /// <summary>
        /// 支払取引区分名を取得・設定します
        /// </summary>
        public String ShiharaiTorihikiKbnName { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public String KyotenCd { get; set; }

        /// <summary>
        /// 拠点名を取得・設定します
        /// </summary>
        public String KyotenName { get; set; }

        /// <summary>
        /// 拠点を取得します
        /// </summary>
        public String Kyoten { get { return "　[拠点] " + this.KyotenCd + " " + this.KyotenName + "\r\n"; } }

        /// <summary>
        /// 取引先CD Fromを取得・設定します
        /// </summary>
        public String TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先名 Fromを取得・設定します
        /// </summary>
        public String TorihikisakiNameFrom { get; set; }

        /// <summary>
        /// 取引先 Fromを取得します
        /// </summary>
        public String TorihikisakiFrom { get { return this.TorihikisakiCdFrom + " " + this.TorihikisakiNameFrom; } }

        /// <summary>
        /// 取引先CD Toを取得・設定します
        /// </summary>
        public String TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先名 Toを取得・設定します
        /// </summary>
        public String TorihikisakiNameTo { get; set; }

        /// <summary>
        /// 取引先 Toを取得します
        /// </summary>
        public String TorihikisakiTo { get { return this.TorihikisakiCdTo + " " + this.TorihikisakiNameTo; } }

        /// <summary>
        /// 取引先を取得します
        /// </summary>
        public String Torihikisaki
        {
            get
            {
                if (String.IsNullOrEmpty(this.TorihikisakiCdFrom) && String.IsNullOrEmpty(this.TorihikisakiCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[取引先] " + this.TorihikisakiFrom + " ～ " + this.TorihikisakiTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 業者CD Fromを取得・設定します
        /// </summary>
        public String GyoushaCdFrom { get; set; }

        /// <summary>
        /// 業者名 Fromを取得・設定します
        /// </summary>
        public String GyoushaNameFrom { get; set; }

        /// <summary>
        /// 業者 Fromを取得します
        /// </summary>
        public String GyoushaFrom { get { return this.GyoushaCdFrom + " " + this.GyoushaNameFrom; } }

        /// <summary>
        /// 業者CD Toを取得・設定します
        /// </summary>
        public String GyoushaCdTo { get; set; }

        /// <summary>
        /// 業者名 Toを取得・設定します
        /// </summary>
        public String GyoushaNameTo { get; set; }

        /// <summary>
        /// 業者 Toを取得します
        /// </summary>
        public String GyoushaTo { get { return this.GyoushaCdTo + " " + this.GyoushaNameTo; } }

        /// <summary>
        /// 業者を取得します
        /// </summary>
        public String Gyousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.GyoushaCdFrom) && String.IsNullOrEmpty(this.GyoushaCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[業者] " + this.GyoushaFrom + " ～ " + this.GyoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 現場CD Fromを取得・設定します
        /// </summary>
        public String GenbaCdFrom { get; set; }

        /// <summary>
        /// 現場名 Fromを取得・設定します
        /// </summary>
        public String GenbaNameFrom { get; set; }

        /// <summary>
        /// 現場 Fromを取得します
        /// </summary>
        public String GenbaFrom { get { return this.GenbaCdFrom + " " + this.GenbaNameFrom; } }

        /// <summary>
        /// 現場CD Toを取得・設定します
        /// </summary>
        public String GenbaCdTo { get; set; }

        /// <summary>
        /// 現場名 Toを取得・設定します
        /// </summary>
        public String GenbaNameTo { get; set; }

        /// <summary>
        /// 現場 Toを取得します
        /// </summary>
        public String GenbaTo { get { return this.GenbaCdTo + " " + this.GenbaNameTo; } }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        public String Genba
        {
            get
            {
                if (String.IsNullOrEmpty(this.GenbaCdFrom) && String.IsNullOrEmpty(this.GenbaCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[現場] " + this.GenbaFrom + " ～ " + this.GenbaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 品名CD Fromを取得・設定します
        /// </summary>
        public String HinmeiCdFrom { get; set; }

        /// <summary>
        /// 品名 Fromを取得・設定します
        /// </summary>
        public String HinmeiNameFrom { get; set; }

        /// <summary>
        /// 品名 Fromを取得します
        /// </summary>
        public String HinmeiFrom { get { return this.HinmeiCdFrom + " " + this.HinmeiNameFrom; } }

        /// <summary>
        /// 品名CD Toを取得・設定します
        /// </summary>
        public String HinmeiCdTo { get; set; }

        /// <summary>
        /// 品名 Toを取得・設定します
        /// </summary>
        public String HinmeiNameTo { get; set; }

        /// <summary>
        /// 品名 Toを取得します
        /// </summary>
        public String HinmeiTo { get { return this.HinmeiCdTo + " " + this.HinmeiNameTo; } }

        /// <summary>
        /// 品名を取得します
        /// </summary>
        public String Hinmei
        {
            get
            {
                if (String.IsNullOrEmpty(this.HinmeiCdFrom) && String.IsNullOrEmpty(this.HinmeiCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[品名] " + this.HinmeiFrom + " ～ " + this.HinmeiTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 荷降業者CD Fromを取得・設定します
        /// </summary>
        public String NioroshiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷降業者名 Fromを取得・設定します
        /// </summary>
        public String NioroshiGyoushaNameFrom { get; set; }

        /// <summary>
        /// 荷降業者 Fromを取得します
        /// </summary>
        public String NioroshiGyoushaFrom { get { return this.NioroshiGyoushaCdFrom + " " + this.NioroshiGyoushaNameFrom; } }

        /// <summary>
        /// 荷降業者CD Toを取得・設定します
        /// </summary>
        public String NioroshiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷降業者名 Toを取得・設定します
        /// </summary>
        public String NioroshiGyoushaNameTo { get; set; }

        /// <summary>
        /// 荷降業者 Toを取得します
        /// </summary>
        public String NioroshiGyoushaTo { get { return this.NioroshiGyoushaCdTo + " " + this.NioroshiGyoushaNameTo; } }

        /// <summary>
        /// 荷降業者を取得します
        /// </summary>
        public String NioroshiGyousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.NioroshiGyoushaCdFrom) && String.IsNullOrEmpty(this.NioroshiGyoushaCdTo))
                {
                    return String.Empty;
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
        public String NioroshiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷降現場名 Fromを取得・設定します
        /// </summary>
        public String NioroshiGenbaNameFrom { get; set; }

        /// <summary>
        /// 荷降現場 Fromを取得します
        /// </summary>
        public String NioroshiGenbaFrom { get { return this.NioroshiGenbaCdFrom + " " + this.NioroshiGenbaNameFrom; } }

        /// <summary>
        /// 荷降現場CD Toを取得・設定します
        /// </summary>
        public String NioroshiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷降現場名 Toを取得・設定します
        /// </summary>
        public String NioroshiGenbaNameTo { get; set; }

        /// <summary>
        /// 荷降現場 Toを取得します
        /// </summary>
        public String NioroshiGenbaTo { get { return this.NioroshiGenbaCdTo + " " + this.NioroshiGenbaNameTo; } }

        /// <summary>
        /// 荷降現場を取得します
        /// </summary>
        public String NioroshiGenba
        {
            get
            {
                if (String.IsNullOrEmpty(this.NioroshiGenbaCdFrom) && String.IsNullOrEmpty(this.NioroshiGenbaCdTo))
                {
                    return String.Empty;
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
        public String NizumiGyoushaCdFrom { get; set; }

        /// <summary>
        /// 荷積業者名 Fromを取得・設定します
        /// </summary>
        public String NizumiGyoushaNameFrom { get; set; }

        /// <summary>
        /// 荷積業者 Fromを取得します
        /// </summary>
        public String NizumiGyoushaFrom { get { return this.NizumiGyoushaCdFrom + " " + this.NizumiGyoushaNameFrom; } }

        /// <summary>
        /// 荷積業者CD Toを取得・設定します
        /// </summary>
        public String NizumiGyoushaCdTo { get; set; }

        /// <summary>
        /// 荷積業者名 Toを取得・設定します
        /// </summary>
        public String NizumiGyoushaNameTo { get; set; }

        /// <summary>
        /// 荷積業者 Toを取得します
        /// </summary>
        public String NizumiGyoushaTo { get { return this.NizumiGyoushaCdTo + " " + this.NizumiGyoushaNameTo; } }

        /// <summary>
        /// 荷積業者を取得します
        /// </summary>
        public String NizumiGyousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.NizumiGyoushaCdFrom) && String.IsNullOrEmpty(this.NizumiGyoushaCdTo))
                {
                    return String.Empty;
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
        public String NizumiGenbaCdFrom { get; set; }

        /// <summary>
        /// 荷積現場名 Fromを取得・設定します
        /// </summary>
        public String NizumiGenbaNameFrom { get; set; }

        /// <summary>
        /// 荷積現場 Fromを取得します
        /// </summary>
        public String NizumiGenbaFrom { get { return this.NizumiGenbaCdFrom + " " + this.NizumiGenbaNameFrom; } }

        /// <summary>
        /// 荷積現場CD Toを取得・設定します
        /// </summary>
        public String NizumiGenbaCdTo { get; set; }

        /// <summary>
        /// 荷積現場名 Toを取得・設定します
        /// </summary>
        public String NizumiGenbaNameTo { get; set; }

        /// <summary>
        /// 荷積現場 Toを取得します
        /// </summary>
        public String NizumiGenbaTo { get { return this.NizumiGenbaCdTo + " " + this.NizumiGenbaNameTo; } }

        /// <summary>
        /// 荷積現場を取得します
        /// </summary>
        public String NizumiGenba
        {
            get
            {
                if (String.IsNullOrEmpty(this.NizumiGenbaCdFrom) && String.IsNullOrEmpty(this.NizumiGenbaCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[荷積現場] " + this.NizumiGenbaFrom + " ～ " + this.NizumiGenbaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 営業担当者CD Fromを取得・設定します
        /// </summary>
        public String EigyouTantoushaCdFrom { get; set; }

        /// <summary>
        /// 営業担当者名 Fromを取得・設定します
        /// </summary>
        public String EigyouTantoushaNameFrom { get; set; }

        /// <summary>
        /// 営業担当者 Fromを取得します
        /// </summary>
        public String EigyouTantoushaFrom { get { return this.EigyouTantoushaCdFrom + " " + this.EigyouTantoushaNameFrom; } }

        /// <summary>
        /// 営業担当者CD Toを取得・設定します
        /// </summary>
        public String EigyouTantoushaCdTo { get; set; }

        /// <summary>
        /// 営業担当者名 Toを取得・設定します
        /// </summary>
        public String EigyouTantoushaNameTo { get; set; }

        /// <summary>
        /// 営業担当者 Toを取得します
        /// </summary>
        public String EigyouTantoushaTo { get { return this.EigyouTantoushaCdTo + " " + this.EigyouTantoushaNameTo; } }

        /// <summary>
        /// 営業担当者を取得します
        /// </summary>
        public String EigyouTantousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.EigyouTantoushaCdFrom) && String.IsNullOrEmpty(this.EigyouTantoushaCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[営業担当者] " + this.EigyouTantoushaFrom + " ～ " + this.EigyouTantoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 入力担当者CD Fromを取得・設定します
        /// </summary>
        public String NyuuryokuTantoushaCdFrom { get; set; }

        /// <summary>
        /// 入力担当者名 Fromを取得・設定します
        /// </summary>
        public String NyuuryokuTantoushaNameFrom { get; set; }

        /// <summary>
        /// 入力担当者 Fromを取得します
        /// </summary>
        public String NyuuryokuTantoushaFrom { get { return this.NyuuryokuTantoushaCdFrom + " " + this.NyuuryokuTantoushaNameFrom; } }

        /// <summary>
        /// 入力担当者CD Toを取得・設定します
        /// </summary>
        public String NyuuryokuTantoushaCdTo { get; set; }

        /// <summary>
        /// 入力担当者名 Toを取得・設定します
        /// </summary>
        public String NyuuryokuTantoushaNameTo { get; set; }

        /// <summary>
        /// 入力担当者 Toを取得します
        /// </summary>
        public String NyuuryokuTantoushaTo { get { return this.NyuuryokuTantoushaCdTo + " " + this.NyuuryokuTantoushaNameTo; } }

        /// <summary>
        /// 入力担当者を取得します
        /// </summary>
        public String NyuuryokuTantousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.NyuuryokuTantoushaCdFrom) && String.IsNullOrEmpty(this.NyuuryokuTantoushaCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[入力担当者] " + this.NyuuryokuTantoushaFrom + " ～ " + this.NyuuryokuTantoushaTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 運搬業者CD Fromを取得・設定します
        /// </summary>
        public String UnpanGyoushaCdFrom { get; set; }

        /// <summary>
        /// 運搬業者名 Fromを取得・設定します
        /// </summary>
        public String UnpanGyoushaNameFrom { get; set; }

        /// <summary>
        /// 運搬業者 Fromを取得します
        /// </summary>
        public String UnpanGyoushaFrom { get { return this.UnpanGyoushaCdFrom + " " + this.UnpanGyoushaNameFrom; } }

        /// <summary>
        /// 運搬業者CD Toを取得・設定します
        /// </summary>
        public String UnpanGyoushaCdTo { get; set; }

        /// <summary>
        /// 運搬業者名 Toを取得・設定します
        /// </summary>
        public String UnpanGyoushaNameTo { get; set; }

        /// <summary>
        /// 運搬業者 Toを取得します
        /// </summary>
        public String UnpanGyoushaTo { get { return this.UnpanGyoushaCdTo + " " + this.UnpanGyoushaNameTo; } }

        /// <summary>
        /// 運搬業者を取得します
        /// </summary>
        public String UnpanGyousha
        {
            get
            {
                if (String.IsNullOrEmpty(this.UnpanGyoushaCdFrom) && String.IsNullOrEmpty(this.UnpanGyoushaCdTo))
                {
                    return String.Empty;
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
        public String ShashuCdFrom { get; set; }

        /// <summary>
        /// 車種名 Fromを取得・設定します
        /// </summary>
        public String ShashuNameFrom { get; set; }

        /// <summary>
        /// 車種 Fromを取得・設定します
        /// </summary>
        public String ShashuFrom { get { return this.ShashuCdFrom + " " + this.ShashuNameFrom; } }

        /// <summary>
        /// 車種CD Toを取得・設定します
        /// </summary>
        public String ShashuCdTo { get; set; }

        /// <summary>
        /// 車種名 Toを取得・設定します
        /// </summary>
        public String ShashuNameTo { get; set; }

        /// <summary>
        /// 車種 Toを取得します
        /// </summary>
        public String ShashuTo { get { return this.ShashuCdTo + " " + this.ShashuNameTo; } }

        /// <summary>
        /// 車種を取得します
        /// </summary>
        public String Shashu
        {
            get
            {
                if (String.IsNullOrEmpty(this.ShashuCdFrom) && String.IsNullOrEmpty(this.ShashuCdTo))
                {
                    return String.Empty;
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
        public String SharyouCdFrom { get; set; }

        /// <summary>
        /// 車輌名 Fromを取得・設定します
        /// </summary>
        public String SharyouNameFrom { get; set; }

        /// <summary>
        /// 車輌 Fromを取得します
        /// </summary>
        public String SharyouFrom { get { return this.SharyouCdFrom + " " + this.SharyouNameFrom; } }

        /// <summary>
        /// 車輌CD Toを取得・設定します
        /// </summary>
        public String SharyouCdTo { get; set; }

        /// <summary>
        /// 車輌名 Toを取得・設定します
        /// </summary>
        public String SharyouNameTo { get; set; }

        /// <summary>
        /// 車輌 Toを取得します
        /// </summary>
        public String SharyouTo { get { return this.SharyouCdTo + " " + this.SharyouNameTo; } }

        /// <summary>
        /// 車輌を取得します
        /// </summary>
        public String Sharyou
        {
            get
            {
                if (String.IsNullOrEmpty(this.SharyouCdFrom) && String.IsNullOrEmpty(this.SharyouCdTo))
                {
                    return String.Empty;
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
        public String KeitaiKbnCdFrom { get; set; }

        /// <summary>
        /// 形態区分名 Fromを取得・設定します
        /// </summary>
        public String KeitaiKbnNameFrom { get; set; }

        /// <summary>
        /// 形態区分 Fromを取得します
        /// </summary>
        public String KeitaiKbnFrom { get { return this.KeitaiKbnCdFrom + " " + this.KeitaiKbnNameFrom; } }

        /// <summary>
        /// 形態区分CD Toを取得・設定します
        /// </summary>
        public String KeitaiKbnCdTo { get; set; }

        /// <summary>
        /// 形態区分名 Toを取得・設定します
        /// </summary>
        public String KeitaiKbnNameTo { get; set; }

        /// <summary>
        /// 形態区分 Toを取得します
        /// </summary>
        public String KeitaiKbnTo { get { return this.KeitaiKbnCdTo + " " + this.KeitaiKbnNameTo; } }

        /// <summary>
        /// 形態区分を取得します
        /// </summary>
        public String KeitaiKbn
        {
            get
            {
                if (String.IsNullOrEmpty(this.KeitaiKbnCdFrom) && String.IsNullOrEmpty(this.KeitaiKbnCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[形態区分] " + this.KeitaiKbnFrom + " ～ " + this.KeitaiKbnTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 種類CD Fromを取得・設定します
        /// </summary>
        public String ShuruiCdFrom { get; set; }

        /// <summary>
        /// 種類 Fromを取得・設定します
        /// </summary>
        public String ShuruiNameFrom { get; set; }

        /// <summary>
        /// 種類 Fromを取得します
        /// </summary>
        public String ShuruiFrom { get { return this.ShuruiCdFrom + " " + this.ShuruiNameFrom; } }

        /// <summary>
        /// 種類CD Toを取得・設定します
        /// </summary>
        public String ShuruiCdTo { get; set; }

        /// <summary>
        /// 種類 Toを取得・設定します
        /// </summary>
        public String ShuruiNameTo { get; set; }

        /// <summary>
        /// 種類 Toを取得します
        /// </summary>
        public String ShuruiTo { get { return this.ShuruiCdTo + " " + this.ShuruiNameTo; } }

        /// <summary>
        /// 種類を取得します
        /// </summary>
        public String Shurui
        {
            get
            {
                if (String.IsNullOrEmpty(this.ShuruiCdFrom) && String.IsNullOrEmpty(this.ShuruiCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[種類] " + this.ShuruiFrom + " ～ " + this.ShuruiTo + "\r\n";
                }
            }
        }

        /// <summary>
        /// 分類CD Fromを取得・設定します
        /// </summary>
        public String BunruiCdFrom { get; set; }

        /// <summary>
        /// 分類 Fromを取得・設定します
        /// </summary>
        public String BunruiNameFrom { get; set; }

        /// <summary>
        /// 分類 Fromを取得します
        /// </summary>
        public String BunruiFrom { get { return this.BunruiCdFrom + " " + this.BunruiNameFrom; } }

        /// <summary>
        /// 分類CD Toを取得・設定します
        /// </summary>
        public String BunruiCdTo { get; set; }

        /// <summary>
        /// 分類 Toを取得・設定します
        /// </summary>
        public String BunruiNameTo { get; set; }

        /// <summary>
        /// 分類 Toを取得します
        /// </summary>
        public String BunruiTo { get { return this.BunruiCdTo + " " + this.BunruiNameTo; } }

        /// <summary>
        /// 分類を取得します
        /// </summary>
        public String Bunrui
        {
            get
            {
                if (String.IsNullOrEmpty(this.BunruiCdFrom) && String.IsNullOrEmpty(this.BunruiCdTo))
                {
                    return String.Empty;
                }
                else
                {
                    return "　[種類] " + this.BunruiFrom + " ～ " + this.BunruiTo + "\r\n";
                }
            }
        }
    }
}
