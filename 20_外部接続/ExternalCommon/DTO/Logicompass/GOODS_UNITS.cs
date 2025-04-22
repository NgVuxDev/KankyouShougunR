using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 品名単位API(取得用)
    /// </summary>
    [DataContract]
    public class GOODS_UNITS
    {
        /// <summary>品名単位情報</summary>
        [DataMember(Name = "goods_unit_records")]
        public List<GOODS_UNITS_RECORDS> Goods_Unit_Records;
    }

    /// <summary>
    /// 品名単位API(取得用：明細)
    /// </summary>
    [DataContract]
    public class GOODS_UNITS_RECORDS
    {
        /// <summary>品名単位ID</summary>
        [DataMember(Name = "goods_unit_id")]
        public string Goods_Unit_Id { get; set; }

        /// <summary>品名単位名</summary>
        [DataMember(Name = "goods_unit_name")]
        public string Goods_Unit_Name { get; set; }
    }

    /// <summary>
    /// 品名単位API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_GOODS_UNITS : GOODS_UNITS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
