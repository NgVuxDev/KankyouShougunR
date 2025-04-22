using System;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace Shougun.Core.Billing.InxsSeikyuushoHakkou
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
        /// 検索条件 :HikaeInsatsuKbn
        /// </summary>
        public SqlBoolean HikaeInsatsuKbn { get; set; }

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

    public class SeikyuuUserSettingsDto
    {
        public SeikyuuUserSettingsDto()
        {
            KagamiUserList = new List<KagamiUserListDto>();
        }
        public string SeikyuuNumber { get; set; }
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
        public long SEIKYUU_NUMBER { get; set; }
        public byte[] TIME_STAMP { get; set; }
    }
}
