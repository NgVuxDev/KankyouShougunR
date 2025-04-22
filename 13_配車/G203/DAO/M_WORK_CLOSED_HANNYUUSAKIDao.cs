using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    [Bean(typeof(M_WORK_CLOSED_HANNYUUSAKI))]
    public interface M_WORK_CLOSED_HANNYUUSAKIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WORK_CLOSED_HANNYUUSAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        [NoPersistentProps("TEKIYOU_BEGIN", "TEKIYOU_END", "DELETE_FLG", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WORK_CLOSED_HANNYUUSAKI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data">エンティティ</param>
        /// <returns>件数</returns>
        int Delete(M_WORK_CLOSED_HANNYUUSAKI data);

        /// <summary>
        /// 業者マスタ、現場マスタから搬入先情報（業者CD/業者名、現場CD/現場名）を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku.Sql.GetHannyuusakiList.sql")]
        DataTable GetHannyuusakiList(SearchDTOClass data);

        /// <summary>
        /// 搬入先（業者CD、現場CD）をもとに搬入先休動マスタのデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku.Sql.GetHannyuusakiKyuudouData.sql")]
        DataTable GetHannyuusakiKyuudouData(DTOClass data);

        /// <summary>
        /// [TableLock付] 業者マスタ、現場マスタから搬入先情報（業者CD/業者名、現場CD/現場名）を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WORK_CLOSED_HANNYUUSAKI WITH(TABLOCKX) WHERE GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/ AND CLOSED_DATE = /*data.CLOSED_DATE*/")]
        M_WORK_CLOSED_HANNYUUSAKI GetHannyuusakiKyuudouDataWithTableLock(M_WORK_CLOSED_HANNYUUSAKI data);
    }
}
