using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity
{
    /// <summary>
    /// グリッドデータ
    /// </summary>
    [Serializable()]
    class GRID_ROW_VALUE
    {
        public int TEIKI_FLG { get; set; }
        public int TANKA_FLG { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME_RYAKU { get; set; }
        public string TORIHIKISAKI_FURIGANA { get; set; }
        public List<T_SELECT_RESULT> ResultList { get; set; }
    }
}
