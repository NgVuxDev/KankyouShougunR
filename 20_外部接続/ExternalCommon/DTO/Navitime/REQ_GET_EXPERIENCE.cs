using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// POSTに使用するパラメータ
    /// </summary>
    [DataContract]
    public class REQ_GET_EXPERIENCE : IApiDto
    {
        [DataMember(Name = "matterCode")]
        public string matterCode;

        [DataMember(Name = "targetDate")]
        public string targetDate;

        [DataMember(Name = "userCode")]
        public string userCode;
    }

}
