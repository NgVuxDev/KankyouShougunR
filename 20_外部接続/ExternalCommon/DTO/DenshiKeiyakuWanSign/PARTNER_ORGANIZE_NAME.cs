using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 相手方レスポンス
    /// </summary>
    [DataContract]
    public class PARTNER_ORGANIZE_NAME
    {
        /// <summary>相手方</summary>
        [DataMember(Name = "partner_organize_nm")]
        public string Partner_Organize_Name { get; set; }
    }
}
