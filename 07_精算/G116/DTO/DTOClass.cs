using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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
        /// 検索条件  :ShiharaiPaper
        /// </summary>
        public int ShiharaiPaper { get; set; }

        /// <summary>
        /// 検索条件  :TorihikisakiCD
        /// </summary>
        public String TorihikisakiCD { get; set; }

        /// <summary>
        /// 検索条件  :HakkoKbn
        /// </summary>
        public SqlBoolean HakkoKbn { get; set; }

        /// <summary>
        /// 検索条件 : REJECT_FLG(排他フラグ)
        /// </summary>
        //private Boolean rEJECT_FLG = false;
        //public Boolean REJECT_FLG
        //{
        //    get
        //    {
        //        return rEJECT_FLG;
        //    }
        //    set
        //    {
        //        rEJECT_FLG = value;
        //    }
        //}

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

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// 将軍-INXS 支払明細書アップロード Option
        /// </summary>
        public bool UseInxsShiharaiKbn { get; set; }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
    }

    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
    public class KagamiFileExportDto
    {
        public int KagamiNumber { get; set; }
        public string FileExport { get; set; }
    }
    // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
}
