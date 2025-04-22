using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// 書籍情報のレスポンス
    /// </summary>
    [DataContract]
    public class ATTRIBUTE_MODEL : IApiDto
    {
        /// <summary>書類ID</summary>
        [DataMember(Name = "document_id")]
        public string Document_id { get; set; }
        /// <summary>管理用タイトル</summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
        /// <summary>契約相手の名称</summary>
        [DataMember(Name = "counterparty")]
        public string Counterparty { get; set; }
        /// <summary>契約締結日　nullを許容する</summary>
        [DataMember(Name = "contract_at")]
        public string Contract_at { get; set; }
        /// <summary>契約開始日　nullを許容する</summary>
        [DataMember(Name = "validity_start_at")]
        public string Validity_start_at { get; set; }
        /// <summary>契約終了日　nullを許容する</summary>
        [DataMember(Name = "validity_end_at")]
        public string Validity_end_at { get; set; }
        /// <summary>解約通知期限　nullを許容する</summary>
        [DataMember(Name = "validity_end_notice_at")]
        public string Validity_end_notice_at { get; set; }
        /// <summary>自動更新の有無　0:指定なし　1:あり　2:なし</summary>
        [DataMember(Name = "auto_update")]
        public int Auto_update { get; set; }
        /// <summary>管理番号</summary>
        [DataMember(Name = "local_id")]
        public string Local_id { get; set; }
        /// <summary>取引金額　nullを許容する</summary>
        [DataMember(Name = "amount")]
        public string Amount { get; set; }
        /// <summary>作成日時</summary>
        [DataMember(Name = "created_at")]
        public string Created_at { get; set; }
        /// <summary>更新日時</summary>
        [DataMember(Name = "updated_at")]
        public string Updated_at { get; set; }
        /// <summary>ユーザー定義の項目</summary>
        [DataMember(Name = "options")]
        public List<ATTRIBUTE_OPTIONS_MODEL> Options { get; set; }
    }

    /// <summary>
    /// ユーザー定義の項目
    /// </summary>
    [DataContract]
    public class ATTRIBUTE_OPTIONS_MODEL : IApiDto
    {
        /// <summary>ユーザー定義の項目の番号</summary>
        [DataMember(Name = "order")]
        public int Order { get; set; }

        /// <summary>ユーザー定義の項目の値</summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}
