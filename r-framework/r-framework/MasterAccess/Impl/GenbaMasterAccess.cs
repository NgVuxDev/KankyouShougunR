using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Impl
{
    class GenbaMasterAccess : Base.AbstractMasterAcess
        <Dao.IM_GYOUSHADao, Entity.M_GYOUSHA,
         Dao.IM_GENBADao, Entity.M_GENBA>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GenbaMasterAccess(CustomControl.ICustomControl control, object[] obj, object[] sendParam)
            : base(control, obj, sendParam,
                   "GYOUSHA_CD", "業者CD", "GENBA_CD", "現場CD", "GYOUSHA_NAME")
        {
        }

        /// <summary>
        /// CDのMax桁数
        /// </summary>
        public readonly int CdMaxLength = 6;

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="gyoushaCd">絞込みを行う業者CD</param>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        public bool IsOverCDLimit(string gyoushaCd, out int maxPlusKeyValue)
        {
            var maxPlusKey = this.dao2.GetMaxPlusKeyByGyoushaCd(gyoushaCd);
            var allKeyDate = this.dao2.GetDataByShokuchiKbn1(gyoushaCd);

            foreach (Entity.M_GENBA genbaaEntity in allKeyDate)
            {
                var genbaCd = int.Parse(genbaaEntity.GENBA_CD);
                if (genbaCd == maxPlusKey)
                {
                    maxPlusKey = genbaCd + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.CdMaxLength < maxPlusKey.ToString().Length)
            {
                return true;
            }

            maxPlusKeyValue = maxPlusKey;
            return false;
        }


    }

}
