using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 案件の実績情報取得のレスポンス
    /// </summary>
    [DataContract]
    public class RES_GET_EXPERIENCE : IApiDto
    {
        [DataMember(Name = "results")]
        public RES_GET_EXPERIENCE_RESULTS Results;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }

    [DataContract]
    public class RES_GET_EXPERIENCE_RESULTS : IApiDto
    {
        [DataMember(Name = "userCode")]
        public string UserCode;

        [DataMember(Name = "targetDate")]
        public string TargetDate;

        [DataMember(Name = "matter")]
        public List<RES_GET_EXPERIENCE_MATTER> Matter;
    }

    [DataContract]
    public class RES_GET_EXPERIENCE_MATTER : IApiDto
    {
        [DataMember(Name = "deliveryOrder")]
        public int DeliveryOrder;

        [DataMember(Name = "matterNo")]
        public int MatterNo;

        [DataMember(Name = "matterCode")]
        public string MatterCode;

        [DataMember(Name = "matterName")]
        public string MatterName;

        [DataMember(Name = "visitCode")]
        public string VisitCode;

        [DataMember(Name = "lat")]
        public int Lat;

        [DataMember(Name = "lon")]
        public int Lon;

        [DataMember(Name = "parkingLat")]
        public int ParkingLat;

        [DataMember(Name = "parkingLon")]
        public int ParkingLon;

        [DataMember(Name = "startMovingTime")]
        public string StartMovingTime;

        [DataMember(Name = "startWorkTime")]
        public string StartWorkTime;

        [DataMember(Name = "completeWorkTime")]
        public string CompleteWorkTime;

        [DataMember(Name = "matterStatusList")]
        public List<RES_GET_EXPERIENCE_MATTER_STATUS_LIST> MatterStatusList;
    }

    [DataContract]
    public class RES_GET_EXPERIENCE_MATTER_STATUS_LIST : IApiDto
    {
        [DataMember(Name = "statusName")]
        public string StatusName;

        [DataMember(Name = "statusTime")]
        public string StatusTime;

        [DataMember(Name = "registerTime")]
        public string RegisterTime;
    }
}
