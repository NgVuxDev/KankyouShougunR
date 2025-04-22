using System.Data.SqlTypes;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    public class UnchinEntryDTOClass
    {
      public SqlInt64 SYSTEM_ID{get;set;}
      public SqlInt32 SEQ {get;set;}
      public SqlInt64 DENPYOU_NUMBER{get;set;}
      public SqlInt16 DENSHU_KBN_CD { get; set; }
      public SqlInt64 RENKEI_NUMBER { get; set; }
      public SqlInt16 DENPYOU_KBN_CD { get; set; }
      public SqlInt16 KYOTEN { get; set; }
    }
}
