using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書詳細情報リクエスト
    /// </summary>
    [DataContract]
    public class RequestKeiyakuInfo : RequestControlNumber
    {
        /// <summary>トランザクションID</summary>
        [DataMember(Name = "xid")]
        public string Xid { get; set; }

        /// <summary>関連コード</summary>
        [DataMember(Name = "control_number")]
        public string Control_Number { get; set; }

        /// <summary>文書名</summary>
        [DataMember(Name = "document_name")]
        public string Document_Name { get; set; }

        /// <summary>契約日</summary>
        [DataMember(Name = "contract_date")]
        public string Contract_Date { get; set; }

        /// <summary>契約満了日</summary>
        [DataMember(Name = "contract_expiration_date")]
        public string Contract_Expiration_Date { get; set; }

        /// <summary>自動更新</summary>
        [DataMember(Name = "is_auto_updating")]
        public string Is_Auto_Updating { get; set; }

        /// <summary>更新期間</summary>
        [DataMember(Name = "renewal_period")]
        public string Renewal_Period { get; set; }

        /// <summary>更新期間単位</summary>
        [DataMember(Name = "renewal_period_unit")]
        public string Renewal_Period_Unit { get; set; }

        /// <summary>解約通知期限</summary>
        [DataMember(Name = "cancel_period")]
        public string Cancel_Period { get; set; }

        /// <summary>解約通知期限単位</summary>
        [DataMember(Name = "cancel_period_unit")]
        public string Cancel_Period_Unit { get; set; }

        /// <summary>リマインド通知</summary>
        [DataMember(Name = "is_reminder")]
        public string Is_Reminder { get; set; }

        /// <summary>リマインダー期限</summary>
        [DataMember(Name = "reminder_period")]
        public string Reminder_Period { get; set; }

        /// <summary>リマインダー期限単位</summary>
        [DataMember(Name = "reminder_period_unit")]
        public string Reminder_Period_Unit { get; set; }

        /// <summary>所属</summary>
        [DataMember(Name = "post_nm")]
        public string Post_nm { get; set; }

        /// <summary>担当者</summary>
        [DataMember(Name = "name_nm")]
        public string Name_nm { get; set; }

        /// <summary>契約金額</summary>
        [DataMember(Name = "contract_decimal")]
        public string Contract_Decimal { get; set; }

        /// <summary>保管場所</summary>
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

        /// <summary>フィールド１</summary>
        [DataMember(Name = "field_1")]
        public string Field_1 { get; set; }

        /// <summary>フィールド２</summary>
        [DataMember(Name = "field_2")]
        public string Field_2 { get; set; }

        /// <summary>フィールド３</summary>
        [DataMember(Name = "field_3")]
        public string Field_3 { get; set; }

        /// <summary>フィールド４</summary>
        [DataMember(Name = "field_4")]
        public string Field_4 { get; set; }

        /// <summary>フィールド５</summary>
        [DataMember(Name = "field_5")]
        public string Field_5 { get; set; }

        /// <summary>有効/無効</summary>
        [DataMember(Name = "is_valid")]
        public string Is_Valid { get; set; }

        /// <summary>管理番号</summary>
        [DataMember(Name = "original_control_number")]
        public string Original_Control_Number { get; set; }

        /// <summary>相手方</summary>
        [DataMember(Name = "partner")]
        public List<PARTNER_ORGANIZE_NAME> Partner { get; set; }
    }
}
