using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 品名API(登録・更新用)
    /// </summary>
    [DataContract]
    public class GOODS
    {
        /// <summary>品名情報</summary>
        [DataMember(Name = "goods_records")]
        public List<GOODS_RECORDS> Goods_Records;
    }

    /// <summary>
    /// 品名API(取得用：明細)
    /// </summary>
    [DataContract]
    public class GOODS_RECORDS
    {
        /// <summary>品名ID</summary>
        [DataMember(Name = "goods_id")]
        public string Goods_Id { get; set; }

        /// <summary>品名</summary>
        [DataMember(Name = "goods_name")]
        public string Goods_Name { get; set; }

        /// <summary>品名カナ</summary>
        [DataMember(Name = "goods_kana_name")]
        public string Goods_Kana_Name { get; set; }

        /// <summary>品名単位ID</summary>
        [DataMember(Name = "goods_unit_id")]
        public string Goods_Unit_Id { get; set; }

        /// <summary>品名分類ID</summary>
        [DataMember(Name = "goods_kind_id")]
        public string Goods_Kind_Id { get; set; }
    }

    /// <summary>
    /// 品名API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_GOODS : GOODS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
