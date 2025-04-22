using System.Runtime.Serialization;
using System.Collections.Generic;
namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// エラー
    /// </summary>
    [DataContract]
    public class ERROR_DELIVERY_PLANS
    {
        /// <summary>エラーメッセージ</summary>
        [DataMember(Name = "message")]
        public string message { get; set; }

        /// <summary>検証エラー情報</summary>
        [DataMember(Name = "errors")]
        public List<ERROR_DELIVERY_PLANS_DETAIL> Errors { get; set; }
    }
}
