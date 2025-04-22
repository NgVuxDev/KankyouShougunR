using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 運転手API(取得用)
    /// </summary>
    [DataContract]
    public class DRIVERS
    {
        /// <summary>運転手情報</summary>
        [DataMember(Name = "driver_records")]
        public List<DRIVERS_RECORDS> Driver_Records;
    }

    /// <summary>
    /// 運転手API(取得用：明細)
    /// </summary>
    [DataContract]
    public class DRIVERS_RECORDS
    {
        /// <summary>運転手ID</summary>
        [DataMember(Name = "driver_id")]
        public string Driver_Id { get; set; }

        /// <summary>パスワード</summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>運転手名</summary>
        [DataMember(Name = "driver_name")]
        public string Driver_Name { get; set; }

        /// <summary>運転手カナ名</summary>
        [DataMember(Name = "driver_kana_name")]
        public string Driver_Kana_Name { get; set; }

        /// <summary>電話番号</summary>
        [DataMember(Name = "tel_no")]
        public string Tel_No { get; set; }

        /// <summary>免許更新日</summary>
        [DataMember(Name = "license_renewal_date")]
        public string License_Renewal_Date { get; set; }

        /// <summary>備考</summary>
        [DataMember(Name = "remarks")]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 運転手API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_DRIVERS : DRIVERS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
