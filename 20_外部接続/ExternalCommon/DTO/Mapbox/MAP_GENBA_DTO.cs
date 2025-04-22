using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// 業者または現場の情報抽出時に使う汎用DTO
    /// </summary>
    public class MAP_GENBA_DTO
    {
        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>業者名</summary>
        public string GYOUSHA_NAME { get; set; }
        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary>現場名</summary>
        public string GENBA_NAME { get; set; }
        /// <summary>郵便番号</summary>
        public string POST { get; set; }
        /// <summary>住所</summary>
        public string ADDRESS { get; set; }
        /// <summary>電話番号</summary>
        public string TEL { get; set; }
        /// <summary>備考1</summary>
        public string BIKOU1 { get; set; }
        /// <summary>備考2</summary>
        public string BIKOU2 { get; set; }
        /// <summary>緯度</summary>
        public string LATITUDE { get; set; }
        /// <summary>経度</summary>
        public string LONGITUDE { get; set; }
    }
}
