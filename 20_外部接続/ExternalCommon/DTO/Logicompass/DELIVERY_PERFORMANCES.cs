using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 配送実績用API
    /// </summary>
    [DataContract]
    public class DELIVERY_PERFORMANCES
    {
        /// <summary>配送実績ヘッダ情報</summary>
        [DataMember(Name = "delivery_header")]
        public DELIVERY_HEADER Delivery_Header { get; set; }
    }

    /// <summary>
    /// 配送実績ヘッダ情報
    /// </summary>
    [DataContract]
    public class DELIVERY_HEADER
    {
        /// <summary>車両ID</summary>
        [DataMember(Name = "car_id")]
        public string Car_Id { get; set; }

        /// <summary>配送開始日</summary>
        [DataMember(Name = "delivery_date")]
        public string Delivery_Date { get; set; }

        /// <summary>配送NO</summary>
        [DataMember(Name = "delivery_no")]
        public int Delivery_No { get; set; }

        /// <summary>配送名</summary>
        [DataMember(Name = "delivery_name")]
        public string Delivery_Name { get; set; }

        /// <summary>配送実績明細情報</summary>
        [DataMember(Name = "delivery_detail")]
        public List<DELIVERY_DETAIL_PERFORMANCES> Delivery_Detail { get; set; }
    }

    /// <summary>
    /// 配送実績明細情報
    /// </summary>
    [DataContract]
    public class DELIVERY_DETAIL_PERFORMANCES
    {
        /// <summary>配送明細NO</summary>
        [DataMember(Name = "delivery_detail_no")]
        public short Delivery_Detail_No { get; set; }

        /// <summary>運転手ID</summary>
        [DataMember(Name = "driver_id")]
        public string Driver_Id { get; set; }

        /// <summary>地点ID</summary>
        [DataMember(Name = "point_id")]
        public string Point_Id { get; set; }

        /// <summary>配送パスフラグ</summary>
        [DataMember(Name = "delivery_pass_flag")]
        public string Delivery_Pass_Flag { get; set; }

        /// <summary>配送実績積卸明細情報</summary>
        [DataMember(Name = "unloading_detail")]
        public List<UNLOADING_DETAIL> Unloading_Detail { get; set; }
    }

    /// <summary>
    /// 配送実績積卸明細情報
    /// </summary>
    [DataContract]
    public class UNLOADING_DETAIL
    {
        /// <summary>積卸連番</summary>
        [DataMember(Name = "unloading_detail_no")]
        public short Unloading_Detail_No { get; set; }

        /// <summary>積卸区分</summary>
        [DataMember(Name = "unloading_type")]
        public string Unloading_Type { get; set; }

        /// <summary>品名ID</summary>
        [DataMember(Name = "goods_id")]
        public string Goods_Id { get; set; }

        /// <summary>品名単位ID</summary>
        [DataMember(Name = "goods_unit_id")]
        public string Goods_Unit_Id { get; set; }

        /// <summary>数量</summary>
        [DataMember(Name = "goods_count")]
        public decimal Goods_Count { get; set; }
    }
}
