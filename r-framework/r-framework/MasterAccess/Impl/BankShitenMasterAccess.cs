using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Impl
{
    [Obsolete("キーが４つテーブルなので対象外",true)]
    class BankShitenMasterAccess : Base.AbstractMasterAcess
        <Dao.IM_BANKDao, Entity.M_BANK,
         Dao.IM_BANK_SHITENDao, Entity.M_BANK_SHITEN>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BankShitenMasterAccess(CustomControl.ICustomControl control, object[] obj, object[] sendParam)
            : base(control, obj, sendParam,
                   "BANK_CD", "銀行CD", "BANK_SHITEN_CD", "銀行支店CD", "BANK_NAME")
        {
        }

    }

}
