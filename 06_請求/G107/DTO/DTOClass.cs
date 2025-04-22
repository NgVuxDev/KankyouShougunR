using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :DenpyoHizukeFrom
        /// </summary>
        public String DenpyoHizukeFrom { get; set; }

        /// <summary>
        /// 検索条件  :DenpyoHizukeTo
        /// </summary>
        public String DenpyoHizukeTo { get; set; }

        /// <summary>
        /// 検索条件  :HakkouKyotenCD
        /// </summary>
        public String HakkouKyotenCD { get; set; }

        /// <summary>
        /// 検索条件  :Simebi
        /// </summary>
        public String Simebi { get; set; }

        /// <summary>
        /// 検索条件  :PrintOrder
        /// </summary>
        public int PrintOrder { get; set; }

        /// <summary>
        /// 検索条件  :SeikyuPaper
        /// </summary>
        public int SeikyuPaper { get; set; }

        /// <summary>
        /// 検索条件  :TorihikisakiCD
        /// </summary>
        public String TorihikisakiCD { get; set; }

        /// <summary>
        /// 検索条件  :HakkoKbn
        /// </summary>
        public SqlBoolean HakkoKbn { get; set; }

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        /// <summary>
        /// 検索条件  :OutputKbn
        /// </summary>
        public int OutputKbn { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 start
        /// <summary>
        /// 将軍-INXS 請求書アップロード Option
        /// </summary>
        public bool UseInxsSeikyuuKbn { get; set; }
        // 20210222 【マージ】INXS請求書アップロード機能を環境将軍R V2.22にマージ依頼 #147338 end

        /// <summary>
        /// 検索条件：抽出データ
        /// </summary>
        public int FilteringData { get; set; }

        /// <summary>
        /// 画面初期表示：拠点CD
        /// </summary>
        public string InitKyotenCd { get; set; }

        /// <summary>
        /// 画面初期表示：締日
        /// </summary>
        public string InitShimebi { get; set; }

        /// <summary>
        /// 画面初期表示：取引先CD
        /// </summary>
        public string InitTorihiksiakiCd { get; set; }

        /// <summary>
        /// 画面初期表示：伝票日付
        /// </summary>
        public string InitDenpyouHiduke { get; set; }

        /// <summary>
        /// 検索条件  :0円の請求書を抽出対象外
        /// </summary>
        public bool ZeroKingakuTaishogai { get; set; }//VAN 20201125 #136235
    }
    /// <summary>
    /// 
    /// </summary>
    public class GinkouDtoClass
    {
        public string ginkouMei { get; set; }               // 銀行名
        public string shitenMei { get; set; }               // 支店名
        public string kouzaShurui { get; set; }             // 口座種類
        public string kouzaBangou { get; set; }             // 口座番号
        public string kouzaMeigi { get; set; }              // 口座名義
    }
}
