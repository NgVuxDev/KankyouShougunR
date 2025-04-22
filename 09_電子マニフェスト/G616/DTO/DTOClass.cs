using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    internal class DTOClass
    {
    }

    /// <summary>
    /// 混合種類マスタ、混合廃棄物検索
    /// </summary>
    public class GetKongouNameDtoCls
    {
        /// <summary>検索条件  :混合種類CD</summary>
        public string KONGOU_SHURUI_CD { get; set; }
    }
}
