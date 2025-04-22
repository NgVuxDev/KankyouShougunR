using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// POST時に指定するパラメータ
    /// 他、REQ_○○系のDTOは使わないのでこれのみ使用
    /// </summary>
    [DataContract]
    public class NaviRequestDto
    {
        /// <summary>プロセスID</summary>
        [DataMember(Name = "processingId")]
        public string processingId;

        /// <summary>案件コード</summary>
        [DataMember(Name = "matterCode")]
        public string matterCode;

        /// <summary>対象日</summary>
        [DataMember(Name = "targetDate")]
        public string targetDate;

        /// <summary>ユーザーID</summary>
        [DataMember(Name = "userCode")]
        public string userCode;

        /// <summary>ファイルパス</summary>
        [DataMember(Name = "filePath")]
        public string filePath;
    }
}
