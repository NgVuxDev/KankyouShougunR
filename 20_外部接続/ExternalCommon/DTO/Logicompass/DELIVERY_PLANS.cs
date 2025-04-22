using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 配送計画API(取得用)
    /// </summary>
    [DataContract]
    public class DELIVERY_PLANS
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

        /// <summary>配送明細情報</summary>
        [DataMember(Name = "delivery_detail")]
        public List<DELIVERY_DETAIL_PLANS> Delivery_Detail { get; set; }
    }

    /// <summary>
    /// 配送明細情報(取得用)
    /// </summary>
    [DataContract]
    public class DELIVERY_DETAIL_PLANS
    {
        /// <summary>配送明細NO</summary>
        [DataMember(Name = "delivery_detail_no")]
        public int Delivery_Detail_No { get; set; }

        /// <summary>地点ID</summary>
        [DataMember(Name = "point_id")]
        public string Point_Id { get; set; }

        /// <summary>到着予定日時</summary>
        [DataMember(Name = "arrival_time")]
        public string Arrival_Time { get; set; }

        /// <summary>出発予定日時</summary>
        [DataMember(Name = "departure_time")]
        public string Departure_Time { get; set; }

        /// <summary>配送順序</summary>
        [DataMember(Name = "delivery_order")]
        public int Delivery_Order { get; set; }

        /// <summary>実績有無</summary>
        [DataMember(Name = "is_actual")]
        public bool Is_Actual { get; set; }

        /// <summary>到着実績時刻</summary>
        [DataMember(Name = "arrival_time_actual")]
        public string Arrival_Time_Actual { get; set; }

        /// <summary>出発実績時刻</summary>
        [DataMember(Name = "departure_time_actual")]
        public string Departure_Time_Actual { get; set; }

        /// <summary>配送パス実績時刻</summary>
        [DataMember(Name = "pass_time_actual")]
        public string Pass_Time_Actual { get; set; }
    }

    /// <summary>
    /// 配送計画API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_DELIVERY_PLANS : ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }

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

        /// <summary>配送明細情報</summary>
        [DataMember(Name = "delivery_detail")]
        public List<INFO_DELIVERY_DETAIL_PLANS> Delivery_Detail { get; set; }
    }

    /// <summary>
    /// 配送明細情報(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_DELIVERY_DETAIL_PLANS
    {
        /// <summary>配送明細NO</summary>
        [DataMember(Name = "delivery_detail_no")]
        public int Delivery_Detail_No { get; set; }

        /// <summary>地点ID</summary>
        [DataMember(Name = "point_id")]
        public string Point_Id { get; set; }

        /// <summary>到着予定日時</summary>
        [DataMember(Name = "arrival_time")]
        public string Arrival_Time { get; set; }

        /// <summary>出発予定日時</summary>
        [DataMember(Name = "departure_time")]
        public string Departure_Time { get; set; }

        /// <summary>発送順序</summary>
        [DataMember(Name = "delivery_order")]
        public int Delivery_Order { get; set; }

        /// <summary>配送品明細情報</summary>
        [DataMember(Name = "goods_detail")]
        public List<INFO_GOODS_DETAIL_PLANS> Goods_Detail { get; set; }
    }

    /// <summary>
    /// 配送品明細情報(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_GOODS_DETAIL_PLANS
    {
        /// <summary>品明細NO</summary>
        [DataMember(Name = "goods_detail_no")]
        public int Goods_Detail_No { get; set; }

        /// <summary>品名ID</summary>
        [DataMember(Name = "goods_id")]
        public string Goods_Id { get; set; }

        /// <summary>数量</summary>
        [DataMember(Name = "goods_count")]
        public string Goods_Count { get; set; }

        /// <summary>品名単位ID</summary>
        [DataMember(Name = "goods_unit_id")]
        public string Goods_Unit_Id { get; set; }
    }
}
