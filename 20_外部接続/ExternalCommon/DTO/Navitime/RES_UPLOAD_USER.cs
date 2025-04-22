using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// ユーザーマスタ一括登録のレスポンス
    /// </summary>
    [DataContract]
    public class RES_UPLOAD_USER : IApiDto
    {
        [DataMember(Name = "results")]
        public RES_UPLOAD_USER_RESULT_STATUS Results;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }

    [DataContract]
    public class RES_UPLOAD_USER_RESULT_STATUS : IApiDto
    {
        [DataMember(Name = "resultStatus")]
        public string ResultStatus;

        [DataMember(Name = "contentError")]
        public List<RES_UPLOAD_USER_CONTENT_ERROR> ContentError;
    }

    [DataContract]
    public class RES_UPLOAD_USER_CONTENT_ERROR : IApiDto
    {
        [DataMember(Name = "lineNo")]
        public int LineNo;

        [DataMember(Name = "errorReason")]
        public string ErrorReason;
    }
}
