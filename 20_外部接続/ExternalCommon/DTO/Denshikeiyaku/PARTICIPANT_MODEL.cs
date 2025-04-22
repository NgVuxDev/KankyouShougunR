using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// 宛先のレスポンス
    /// </summary>
    [DataContract]
    public class PARTICIPANT_MODEL : IApiDto
    {
        /// <summary>宛先ID</summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>宛先のメールアドレス</summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>宛先の名前</summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>宛先の会社名</summary>
        [DataMember(Name = "organization")]
        public string Organization { get; set; }

        /// <summary>書類内の宛先の順序（送信者は 0）</summary>
        [DataMember(Name = "order")]
        public long Order { get; set; }

        /// <summary>宛先の状態</summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>宛先に設定されているアクセスコード。APIを使用しているユーザーが値を設定した場合のみレスポンスに含まれる。</summary>
        [DataMember(Name = "access_code")]
        public string Access_Code { get; set; }

        /// <summary>宛先の言語設定。ja（日本語）、en（英語）、zh-CHS（簡体字）、zh-CHT（繁体字）のいずれか。</summary>
        [DataMember(Name = "language_code")]
        public string Language_Code { get; set; }

        /// <summary>URL有効期限（RFC3339準拠）。statusが4の場合のみレスポンスに含まれる。</summary>
        [DataMember(Name = "access_expires_at")]
        public string Access_Expires_At { get; set; }
    }
}
