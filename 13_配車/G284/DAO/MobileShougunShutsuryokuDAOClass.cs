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

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.MobileShougunShutsuryoku
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface MobileShougunShutsuryokuDAOClass : IS2Dao
    {

        /// <summary>
        /// Entityで絞り込んで拠点マスタ値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetKyotenData.sql")]
        DataTable GetKyotenDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込んで単位マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetUnitData.sql")]
        DataTable GetUnitDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込んコンテナマスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetContainerData.sql")]
        DataTable GetContainerDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん車輌マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetCarData.sql")]
        DataTable GetCarDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん運転マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetDriverData.sql")]
        DataTable GetDriverDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん品目マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetGoodsData.sql")]
        DataTable GetGoodsDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん会社略称名を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetMyCompanyNameData.sql")]
        DataTable GetMyCompanyNameDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん自社情報マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetMyCompanyKyotenData.sql")]
        DataTable GetMyCompanyKyotenDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん現場マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん搬入現場マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetHannyuData.sql")]
        DataTable GetHannyuDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込ん定期配車を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetTeikiData.sql")]
        DataTable GetTeikiDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込んスポット配車を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetUketsukeSSData.sql")]
        DataTable GetUketsukeSSDataForEntity(MobileShougunShutsuryokuDTOClass data);

        /// <summary>
        /// Entityで絞り込んスポット配車コンテナを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunShutsuryoku.Sql.GetContainer.sql")]
        DataTable GetUketsukeSSContenaDataForEntity(MobileShougunShutsuryokuDTOClass data);
    }
}
