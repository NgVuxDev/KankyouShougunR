using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書詳細レスポンス
    /// </summary>
    [DataContract]
    public class KEIYAKU_INFO_WAN_SIGN
    {
        /// <summary>文書名</summary>
        [DataMember(Name = "document_name")]
        public string Document_Name { get; set; }

        /// <summary>関連コード</summary>
        [DataMember(Name = "control_number")]
        public string Control_Number { get; set; }

        /// <summary>相手方</summary>
        [DataMember(Name = "partner")]
        public List<PARTNER_ORGANIZE_NAME> Partner { get; set; }

        /// <summary>親契約</summary>
        [DataMember(Name = "parent_document")]
        public KEIYAKU_DOCUMENTS_WAN Parent_Document { get; set; }

        /// <summary>子契約</summary>
        [DataMember(Name = "child_document")]
        public List<KEIYAKU_DOCUMENTS_WAN> Child_Document { get; set; }

        /// <summary>契約日</summary>
        [DataMember(Name = "contract_date")]
        public string Contract_Date { get; set; }

        /// <summary>契約満了日</summary>
        [DataMember(Name = "contract_expiration_date")]
        public string Contract_Expiration_Date { get; set; }

        /// <summary>自動更新有無</summary>
        [DataMember(Name = "is_auto_updating")]
        public string Is_Auto_Updating { get; set; }

        /// <summary>更新期間</summary>
        [DataMember(Name = "renewal")]
        public PERIOD_WAN Renewal { get; set; }

        /// <summary>解約通知</summary>
        [DataMember(Name = "cancel")]
        public PERIOD_WAN Cancel { get; set; }

        /// <summary>リマインド通知有無</summary>
        [DataMember(Name = "is_reminder")]
        public string Is_Reminder { get; set; }

        /// <summary>リマインド通知</summary>
        [DataMember(Name = "reminder")]
        public PERIOD_WAN Reminder { get; set; }

        /// <summary>所属</summary>
        [DataMember(Name = "post_nm")]
        public string Post_Name { get; set; }

        /// <summary>送信者</summary>
        [DataMember(Name = "name_nm")]
        public string Soushin_Name { get; set; }

        /// <summary>保管場所</summary>
        [DataMember(Name = "contract_decimal")]
        public string Contract_Decimal { get; set; }

        /// <summary>契約金額</summary>
        [DataMember(Name = "storage_location")]
        public string Storage_Location { get; set; }

        /// <summary>備考1</summary>
        [DataMember(Name = "comment_1")]
        public string Comment_1 { get; set; }

        /// <summary>備考2</summary>
        [DataMember(Name = "comment_2")]
        public string Comment_2 { get; set; }

        /// <summary>備考3</summary>
        [DataMember(Name = "comment_3")]
        public string Comment_3 { get; set; }

        /// <summary>フィールド1</summary>
        [DataMember(Name = "field_1")]
        public string Field_1 { get; set; }

        /// <summary>フィールド2</summary>
        [DataMember(Name = "field_2")]
        public string Field_2 { get; set; }

        /// <summary>フィールド3</summary>
        [DataMember(Name = "field_3")]
        public string Field_3 { get; set; }

        /// <summary>フィールド4</summary>
        [DataMember(Name = "field_4")]
        public string Field_4 { get; set; }

        /// <summary>フィールド5</summary>
        [DataMember(Name = "field_5")]
        public string Field_5 { get; set; }

        /// <summary>有効無効</summary>
        [DataMember(Name = "is_valid")]
        public string Is_Valid { get; set; }

        /// <summary>箱番号</summary>
        [DataMember(Name = "box_number")]
        public string Box_Number { get; set; }

        /// <summary>文書管理ラベル表示済</summary>
        [DataMember(Name = "is_view_doc_control_label")]
        public string Is_View_Doc_Control_Label { get; set; }

        /// <summary>DocumentID</summary>
        [DataMember(Name = "document_id")]
        public string Document_Id { get; set; }

        /// <summary>登録者名</summary>
        [DataMember(Name = "registered_user_name")]
        public string Registered_User_Name { get; set; }

        /// <summary>管理番号</summary>
        [DataMember(Name = "original_control_number")]
        public string Original_Control_Number { get; set; }
    }
}
