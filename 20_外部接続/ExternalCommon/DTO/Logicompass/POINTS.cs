using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 地点API(取得用)
    /// </summary>
    [DataContract]
    public class POINTS
    {
        /// <summary>地点情報</summary>
        [DataMember(Name = "point_records")]
        public List<POINTS_RECORDS> Point_Records;
    }

    /// <summary>
    /// 地点API(取得用：明細)
    /// </summary>
    [DataContract]
    public class POINTS_RECORDS
    {
        /// <summary>地点ID</summary>
        [DataMember(Name = "point_id")]
        public string Point_Id { get; set; }

        /// <summary>地点名</summary>
        [DataMember(Name = "point_name")]
        public string Point_Name { get; set; }

        /// <summary>地点カナ名</summary>
        [DataMember(Name = "point_kana_name")]
        public string Point_Kana_Name { get; set; }

        /// <summary>地図表示名</summary>
        [DataMember(Name = "map_name")]
        public string Map_Name { get; set; }

        /// <summary>郵便番号</summary>
        [DataMember(Name = "post_code")]
        public string Post_Code { get; set; }

        /// <summary>都道府県</summary>
        [DataMember(Name = "prefectures")]
        public string Prefectures { get; set; }

        /// <summary>住所</summary>
        [DataMember(Name = "address1")]
        public string Address1 { get; set; }

        /// <summary>住所その他</summary>
        [DataMember(Name = "address2")]
        public string Address2 { get; set; }

        /// <summary>TEL番号</summary>
        [DataMember(Name = "tel_no")]
        public string Tel_No { get; set; }

        /// <summary>FAX番号</summary>
        [DataMember(Name = "fax_no")]
        public string Fax_No { get; set; }

        /// <summary>担当者名</summary>
        [DataMember(Name = "contact_name")]
        public string Contact_Name { get; set; }

        /// <summary>メールアドレス</summary>
        [DataMember(Name = "mail_address")]
        public string Mail_Address { get; set; }

        /// <summary>緯度</summary>
        [DataMember(Name = "latitude")]
        public string Latitude { get; set; }

        /// <summary>経度</summary>
        [DataMember(Name = "longitude")]
        public string Longitude { get; set; }

        /// <summary>地点範囲半径</summary>
        [DataMember(Name = "range_radius")]
        public int Range_Radius { get; set; }

        /// <summary>備考</summary>
        [DataMember(Name = "remarks")]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 地点API(登録・更新用)
    /// </summary>
    [DataContract]
    public class INFO_POINT : POINTS_RECORDS, ISystecRegistDto
    {
        /// <summary>会社ID</summary>
        [DataMember(Name = "company_id")]
        public string Company_Id { get; set; }
    }
}
