using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 訪問先マスタ一括登録確認のレスポンス
    /// </summary>
    [DataContract]
    public class RES_CHECK_UPLOAD_VISIT : IApiDto
    {
        [DataMember(Name = "results")]
        public RES_CHECK_UPLOAD_VISIT_RESULTS Results;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }

    [DataContract]
    public class RES_CHECK_UPLOAD_VISIT_RESULTS : IApiDto
    {
        [DataMember(Name = "uploadingStatus")]
        public string UploadingStatus;

        [DataMember(Name = "resultStatus")]
        public string ResultStatus;

        [DataMember(Name = "contentError")]
        public List<RES_CHECK_UPLOAD_VISIT_ERROR_CONTENT> ContentError;

        [DataMember(Name = "badAccuracyAddress")]
        public List<RES_CHECK_UPLOAD_VISIT_BAD_ACCURACY_ADDRESS> BadAccuracyAddress;
    }

    [DataContract]
    public class RES_CHECK_UPLOAD_VISIT_ERROR_CONTENT : IApiDto
    {
        [DataMember(Name = "lineNo")]
        public int LineNo;

        [DataMember(Name = "errorReason")]
        public string ErrorReason;
    }

    [DataContract]
    public class RES_CHECK_UPLOAD_VISIT_BAD_ACCURACY_ADDRESS : IApiDto
    {
        [DataMember(Name = "lineNo")]
        public int LineNo;

        [DataMember(Name = "badAccuracyAddress")]
        public string BadAccuracyAddress;

        [DataMember(Name = "accuracy")]
        public string Accuracy;
    }
}
