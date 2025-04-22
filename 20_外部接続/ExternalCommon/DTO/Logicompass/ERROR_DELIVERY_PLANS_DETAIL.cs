using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// エラー
    /// </summary>
    [DataContract]
    public class ERROR_DELIVERY_PLANS_DETAIL
    {
        /// <summary>管理番号</summary>
        [DataMember(Name = "sequence_no")]
        public int Sequence_No { get; set; }

        /// <summary>車両ID</summary>
        [DataMember(Name = "car_id")]
        public string Car_Id { get; set; }

        /// <summary>配送日</summary>
        [DataMember(Name = "delivery_date")]
        public string Delivery_Date { get; set; }

        /// <summary>配送NO</summary>
        [DataMember(Name = "delivery_no")]
        public string Delivery_No { get; set; }

        /// <summary>配送明細NO</summary>
        [DataMember(Name = "delivery_detail_no")]
        public string Delivery_Detail_No { get; set; }

        //Nullを許容しないとエラーする
        /// <summary>品明細NO</summary>
        [DataMember(Name = "goods_detail_no")]
        public int? Goods_Detail_No { get; set; }

        /// <summary>異常項目名</summary>
        [DataMember(Name = "error_item_name")]
        public string Error_Item_Name { get; set; }

        /// <summary>異常項目値</summary>
        [DataMember(Name = "error_item_value")]
        public string Error_Item_Value { get; set; }

        /// <summary>異常内容</summary>
        [DataMember(Name = "error_contents")]
        public string Error_Contents { get; set; }
    }
}
