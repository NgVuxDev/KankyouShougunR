using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// 書類のレスポンス
    /// </summary>
    [DataContract]
    public class DOCUMENT_MODEL : IApiDto
    {
        /// <summary>書類ID</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>作成者ID</summary>
        [DataMember(Name = "user_id")]
        public string User_Id { get; set; }

        /// <summary>書類のタイトル</summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>送信者用のメモ</summary>
        [DataMember(Name = "note")]
        public string Note { get; set; }

        /// <summary>受信者に対するメッセージ</summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>書類の状態</summary>
        [DataMember(Name = "status")]
        public long Status { get; set; }

        /// <summary>転送許可設定</summary>
        [DataMember(Name = "can_transfer")]
        public bool Can_Transfer { get; set; }

        /// <summary>作成日時（RFC3339準拠）</summary>
        [DataMember(Name = "created_at")]
        public string Created_At { get; set; }

        /// <summary>更新日時（RFC3339準拠）</summary>
        [DataMember(Name = "updated_at")]
        public string Updated_At { get; set; }

        /// <summary>参加者の一覧（作成者を含む）</summary>
        [DataMember(Name = "participants")]
        public List<PARTICIPANT_MODEL> Participants { get; set; }

        /// <summary>ファイルの一覧</summary>
        [DataMember(Name = "files")]
        public List<FILE_MODEL> Files { get; set; }
    }
}
