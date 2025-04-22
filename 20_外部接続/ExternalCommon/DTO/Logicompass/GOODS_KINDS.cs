using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 品名分類API(取得用)
    /// </summary>
    [DataContract]
    public class GOODS_KINDS
    {
        /// <summary>品名分類情報</summary>
        [DataMember(Name = "goods_kind_records")]
        public List<GOODS_KINDS_RECORDS> Goods_Kind_Records;
    }

    /// <summary>
    /// 品名分類API(取得用：明細)
    /// </summary>
    [DataContract]
    public class GOODS_KINDS_RECORDS
    {
        /// <summary>品名分類ID</summary>
        [DataMember(Name = "goods_kind_id")]
        public string Goods_Kind_Id { get; set; }

        /// <summary>品名分類名</summary>
        [DataMember(Name = "goods_kind_name")]
        public string Goods_Kind_Name { get; set; }
    }

    /// <summary>
    /// 品名分類API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_GOODS_KINDS : GOODS_KINDS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
