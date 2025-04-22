using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 車両関連グループAPI(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_CAR_RELATIONS : ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }

        /// <summary>車両グループID</summary>
        [DataMember(Name = "car_group_id")]
        public string Car_Group_Id { get; set; }

        /// <summary>車両ID</summary>
        [DataMember(Name = "car_id")]
        public string Car_Id { get; set; }
    }
}
