using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    [DataContract]
    public class WIDGET_MODEL : IApiDto
    {
        /// <summary>入力項目のID</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>入力項目の種類（押印は 0 / フリーテキストは 1）</summary>
        [DataMember(Name = "widget_type")]
        public int Widget_Type { get; set; }

        /// <summary>入力が割り当てられている宛先の ID</summary>
        [DataMember(Name = "participant_id")]
        public string Participant_Id { get; set; }

        /// <summary>入力対象のファイルの ID</summary>
        [DataMember(Name = "file_id")]
        public string File_Id { get; set; }

        /// <summary>入力項目が設定されている対象ファイルのページ番号</summary>
        [DataMember(Name = "page")]
        public long Page { get; set; }

        /// <summary>入力項目左上の対象ファイル・対象ページにおける設置位置の X 座標</summary>
        [DataMember(Name = "x")]
        public long X { get; set; }

        /// <summary>入力項目左上の対象ファイル・対象ページにおける設置位置の Y 座標</summary>
        [DataMember(Name = "y")]
        public long Y { get; set; }

        /// <summary>入力項目の幅</summary>
        [DataMember(Name = "w")]
        public long W { get; set; }

        /// <summary>入力項目の高さ</summary>
        [DataMember(Name = "h")]
        public long H { get; set; }

        /// <summary>入力項目に入力されたテキスト</summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>入力項目の状態</summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>入力項目に入力されたラベル</summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }
    }
}
