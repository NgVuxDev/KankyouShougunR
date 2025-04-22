using System;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace Shougun.Core.Adjustment.InxsShiharaiMeisaishoHakko
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
        /// 検索条件  :HikaeInsatsuKbn
        /// </summary>
        public SqlBoolean HikaeInsatsuKbn { get; set; }

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
        /// 検索条件：アップロード状況
        /// </summary>
        public int? UploadStatus { get; set; }

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
        public bool ZeroKingakuTaishogai { get; set; }
    }

    public class SeisanUserSettingsDto
    {
        public SeisanUserSettingsDto()
        {
            KagamiUserList = new List<KagamiUserListDto>();
        }
        public string SeisanNumber { get; set; }
        public List<KagamiUserListDto> KagamiUserList { get; set; }
    }

    public class KagamiUserListDto
    {
        public KagamiUserListDto()
        {
            UserSettingInfos = new List<UserSettingInfoDto>();
        }
        public int KagamiNumber { get; set; }
        public List<UserSettingInfoDto> UserSettingInfos { get; set; }
    }

    public class KagamiUserDto
    {
        public int KagamiNumber { get; set; }
        public long UserSysId { get; set; }
        public long UserId { get; set; }
        public bool IsSend { get; set; }
    }

    public class UserSettingInfoDto
    {
        public long UserSysId { get; set; }
        public long UserId { get; set; }
        public bool IsSend { get; set; }
    }

    public class UploadLoadDto
    {
        public long SEISAN_NUMBER { get; set; }
        public byte[] TIME_STAMP { get; set; }
    }
}
