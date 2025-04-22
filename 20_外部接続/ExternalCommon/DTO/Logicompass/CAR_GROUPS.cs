using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 車両グループAPI(取得用)
    /// </summary>
    [DataContract]
    public class CAR_GROUPS
    {
        /// <summary>車両グループ情報</summary>
        [DataMember(Name = "car_group_records")]
        public List<CAR_GROUPS_RECORDS> Car_Group_Records;
    }

    /// <summary>
    /// 車両グループAPI(取得用：明細)
    /// </summary>
    [DataContract]
    public class CAR_GROUPS_RECORDS
    {
        /// <summary>車両グループID</summary>
        [DataMember(Name = "car_group_id")]
        public string Car_Group_Id { get; set; }

        /// <summary>車両グループ名</summary>
        [DataMember(Name = "car_group_name")]
        public string Car_Group_Name { get; set; }
    }

    /// <summary>
    /// 車両グループAPI(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_CAR_GROUPS : CAR_GROUPS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
