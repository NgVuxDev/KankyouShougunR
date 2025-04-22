using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 検証エラー情報
    /// </summary>
    [DataContract]
    public class INFO_ERROR
    {
        /// <summary>管理番号</summary>
        [DataMember(Name = "sequence_no")]
        public int Sequence_No { get; set; }

        /// <summary>ID1</summary>
        [DataMember(Name = "id1")]
        public string Id1 { get; set; }

        /// <summary>ID2</summary>
        [DataMember(Name = "id2")]
        public string Id2 { get; set; }

        /// <summary>ID3</summary>
        [DataMember(Name = "id3")]
        public string Id3 { get; set; }

        /// <summary>ID4</summary>
        [DataMember(Name = "id4")]
        public string Id4 { get; set; }

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
