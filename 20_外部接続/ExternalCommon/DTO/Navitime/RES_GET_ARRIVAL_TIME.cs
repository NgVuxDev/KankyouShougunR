using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 案件の到着予定時刻取得のレスポンス
    /// </summary>
    [DataContract]
    public class RES_GET_ARRIVAL_TIME : IApiDto
    {
        [DataMember(Name = "results")]
        public RES_GET_ARRIVAL_TIME_RESULTS Results;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }

    [DataContract]
    public class RES_GET_ARRIVAL_TIME_RESULTS : IApiDto
    {
        [DataMember(Name = "userCode")]
        public string UserCode;

        [DataMember(Name = "targetDate")]
        public string TargetDate;

        [DataMember(Name = "matter")]
        public List<RES_GET_ARRIVAL_TIME_MATTER> Matter;

        //officeは過去使用していたパラメータで現在は無効。nullしか返ってこないので省く
        //[DataMember(Name = "office")]
        //public List<RES_GET_ARRIVAL_TIME_OFFICE> Office;
    }

    [DataContract]
    public class RES_GET_ARRIVAL_TIME_MATTER : IApiDto
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

        [DataMember(Name = "estimatedArrivalTime")]
        public string EstimatedArrivalTime;

        [DataMember(Name = "reflectedArrivalTime")]
        public string ReflectedArrivalTime;

        [DataMember(Name = "ArrivalTime")]
        public string ArrivalTime;

        [DataMember(Name = "estimatedDepartureTime")]
        public string EstimatedDepartureTime;

        [DataMember(Name = "estimatedMovingStartTime")]
        public string EstimatedMovingStartTime;

        [DataMember(Name = "movingStartTime")]
        public string MovingStartTime;
    }
}
