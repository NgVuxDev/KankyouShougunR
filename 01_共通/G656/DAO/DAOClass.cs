using System.Data;
using r_framework.Dao;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.DenpyouRenkeiIchiran
{
    [Bean(typeof(DTOClass))]
    internal interface DAOClass : IS2Dao
    {
        #region 受付
        /// <summary>
        /// 受付データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryUketsukeData.sql")]
        DataTable GetUketsukeEntryData(DTOClass data);

        /// <summary>
        /// 取集受付派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUketsukeSSData.sql")]
        DataTable GetUketsukeSSHaseiData(DTOClass data);

        /// <summary>
        /// 出荷受付派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUketsukeSKData.sql")]
        DataTable GetUketsukeSKHaseiData(DTOClass data);

        /// <summary>
        /// 持込受付派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUketsukeMKData.sql")]
        DataTable GetUketsukeMKHaseiData(DTOClass data);

        /// <summary>
        /// 物販受付派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUketsukeBPData.sql")]
        DataTable GetUketsukeBPHaseiData(DTOClass data);

        /// <summary>
        /// 受付連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiUketsukeData.sql")]
        DataTable GetUketsukeRenkeiData(DTOClass data);
        #endregion

        #region 受入
        /// <summary>
        /// 受入データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryUkeireData.sql")]
        DataTable GetUkeireEntryData(DTOClass data);

        /// <summary>
        /// 受入派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUkeireData.sql")]
        DataTable GetUkeireHaseiData(DTOClass data);

        /// <summary>
        /// 受入連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiUkeireData.sql")]
        DataTable GetUkeireRenkeiData(DTOClass data);
        #endregion

        #region 出荷
        /// <summary>
        /// 出荷データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryShukkaData.sql")]
        DataTable GetShukkaEntryData(DTOClass data);

        /// <summary>
        /// 出荷派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiShukkaData.sql")]
        DataTable GetShukkaHaseiData(DTOClass data);

        /// <summary>
        /// 出荷連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiShukkaData.sql")]
        DataTable GetShukkaRenkeiData(DTOClass data);
        #endregion

        #region 売上支払
        /// <summary>
        /// 売上支払データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryUrShData.sql")]
        DataTable GetUrShEntryData(DTOClass data);

        /// <summary>
        /// 売上支払派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUrShData.sql")]
        DataTable GetUrShHaseiData(DTOClass data);

        /// <summary>
        /// 売上支払連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiUrShData.sql")]
        DataTable GetUrShRenkeiData(DTOClass data);
        #endregion

        #region マニフェスト
        /// <summary>
        /// マニフェストデータ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryManiData.sql")]
        DataTable GetManiEntryData(DTOClass data);

        /// <summary>
        /// マニフェスト派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiManiData.sql")]
        DataTable GetManiHaseiData(DTOClass data);

        /// <summary>
        /// マニフェスト連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiManiData.sql")]
        DataTable GetManiRenkeiData(DTOClass data);
        #endregion

        #region 運賃
        /// <summary>
        /// 運賃データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryUnchinData.sql")]
        DataTable GetUnchinEntryData(DTOClass data);

        /// <summary>
        /// 運賃派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiUnchinData.sql")]
        DataTable GetUnchinHaseiData(DTOClass data);

        /// <summary>
        /// 運賃連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiUnchinData.sql")]
        DataTable GetUnchinRenkeiData(DTOClass data);
        #endregion

        #region 代納
        /// <summary>
        /// 代納データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetEntryDainouData.sql")]
        DataTable GetDainouEntryData(DTOClass data);

        /// <summary>
        /// 代納派生データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetHaseiDainouData.sql")]
        DataTable GetDainouHaseiData(DTOClass data);

        /// <summary>
        /// 代納連携データ取得
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.DenpyouRenkeiIchiran.Sql.GetRenkeiDainouData.sql")]
        DataTable GetDainouRenkeiData(DTOClass data);
        #endregion
    }
}
