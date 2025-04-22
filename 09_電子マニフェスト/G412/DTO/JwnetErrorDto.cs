using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
{
    /// <summary>
    /// JWNET通信エラー表示用DTO
    /// </summary>
    public class JwnetErrorDto
    {
        /// <summary>マニフェスト番号</summary>
        public string manifestId { get; set; }

        /// <summary>登録日</summary>
        public string createDate { get; set; }

        /// <summary>KANRI_ID</summary>
        public string kanriId { get; set; }

        /// <summary>QUE_SEQ</summary>
        public string queSeq { get; set; }
    }
}
