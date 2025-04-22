using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 配送計画一括登録確認のレスポンス
    /// </summary>
    [DataContract]
    public class RES_CHECK_UPLOAD : IApiDto
    {
        [DataMember(Name = "results")]
        public RES_CHECK_UPLOAD_RESULTS Results;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }

    [DataContract]
    public class RES_CHECK_UPLOAD_RESULTS : IApiDto
    {
        [DataMember(Name = "uploadingStatus")]
        public string UploadingStatus;

        [DataMember(Name = "resultStatus")]
        public string ResultStatus;

        [DataMember(Name = "updateTime")]
        public string UpdateTime;

        [DataMember(Name = "contentError")]
        public List<RES_CHECK_UPLOAD_CONTENT_ERROR> ContentError;
    }

    [DataContract]
    public class RES_CHECK_UPLOAD_CONTENT_ERROR : IApiDto
    {
        [DataMember(Name = "lineNo")]
        public int LineNo;

        [DataMember(Name = "errorReason")]
        public string ErrorReason;
    }
}
