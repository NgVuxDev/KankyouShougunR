using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;
using System.Data;

namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
{
    internal class TorikomiDataStruct
    {
        public int Position { get; set; }
        public int Length { get; set; }
        public TorikomiDataStruct(int position, int length)
        {
            this.Position = position;
            this.Length = length;

        }

    }
    public class DTOClass
    {
        public SqlDateTime YonyuuDateFrom { get; set; }
        public SqlDateTime YonyuuDateTo { get; set; }
        public DTOClass()
        {
            YonyuuDateFrom = SqlDateTime.Null;
            YonyuuDateTo = SqlDateTime.Null;
        }
    }

    public class DuplicateNyuukinDTOClass
    {
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlDecimal KINGAKU { get; set; }
    }
}
