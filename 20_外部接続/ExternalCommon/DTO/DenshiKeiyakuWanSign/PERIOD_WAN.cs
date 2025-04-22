using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 期間
    /// </summary>
    [DataContract]
    public class PERIOD_WAN 
    {
        /// <summary>期間</summary>
        [DataMember(Name = "period")]
        public string Period { get; set; }

        /// <summary>単位</summary>
        [DataMember(Name = "period_unit")]
        public string Period_Unit { get; set; }
    }
}
