using System.Data.SqlTypes;
using System;
namespace r_framework.Entity
{
public class T_UKETSUKE_KONTENA_SLIP:SuperEntity{
public SqlInt32 UKETSUKE_NO { get; set; }

public SqlInt16 SECCHI_HIKIAGE_FLG { get; set; }

public SqlInt16 ROW_NO { get; set; }

public string KONTENA_SHURUI_CD { get; set; }

public string KONTENA_CD { get; set; }


}
}