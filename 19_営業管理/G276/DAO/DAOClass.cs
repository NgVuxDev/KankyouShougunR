// $Id: DAOClass.cs 4063 2013-10-18 04:23:00Z sys_dev_24 $
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
using System.Data;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    [Bean(typeof(M_SHAIN))]
    public interface MitsumoriTantousyaDao : IS2Dao
    {
        /// <summary>
        /// 社員マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetShainData.sql")]
        DataTable GetShainDataForEntity(M_SHAIN data);

    }

    [Bean(typeof(M_KYOTEN))]
    public interface MitsumoriKyotenDao : IS2Dao
    {
        /// <summary>
        /// 拠点マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetKyotenData.sql")]
        DataTable GetKyotenDataForEntity(M_KYOTEN data);
    }

    [Bean(typeof(M_TORIHIKISAKI))]
    public interface MitsumoriTorihikisakiDao : IS2Dao
    {
        /// <summary>
        /// 取引先マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiDataForEntity(M_TORIHIKISAKI data);
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface MitsumoriGyoushaDao : IS2Dao
    {
        /// <summary>
        /// 業者マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetGyoushaData.sql")]
        DataTable GetGyousyaDataForEntity(M_GYOUSHA data);
    }

    [Bean(typeof(M_GENBA))]
    public interface MitsumoriGenbaDao : IS2Dao
    {
        /// <summary>
        /// 現場マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(M_GENBA data);
    }


    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface MitsumoriToriikisakiSeikyuDao : IS2Dao
    {
        /// <summary>
        /// 請求マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetSeikyuuData.sql")]
        DataTable GetSeikyuuDataForEntity(M_TORIHIKISAKI_SEIKYUU data);
    }

    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface MitsumoriToriikisakiShiharaiDao : IS2Dao
    {
        /// <summary>
        /// 支払マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetShiharaiData.sql")]
        DataTable GetShiharaiDataForEntity(M_TORIHIKISAKI_SHIHARAI data);
    }

    [Bean(typeof(M_SHOUHIZEI))]
    public interface MitsumoriShouhizeiDao : IS2Dao
    {
        /// <summary>
        /// 消費税マスタのデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetShouhizeiData.sql")]
        DataTable GetShouhizeiDataForEntity(DateTime  data);
    }

    [Bean(typeof(M_HIKIAI_TORIHIKISAKI))]
    public interface HikiaiTorihikisakiDao : IS2Dao
    {
        /// <summary>
        /// 引合取引先を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.CheckHikiaiTorihikisakiData.sql")]
        DataTable CheckHikiaiTorihikisakiDataForEntity(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 引合取引先を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetHikiaiTorihikisakiData.sql.sql")]
        DataTable GetHikiaiTorihikisakiDataForEntity(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コード最大値を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxTorihikisakiCode.sql")]
        string GetMaxTorihikisakiCode(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Insert(M_HIKIAI_TORIHIKISAKI data);
    }


    [Bean(typeof(M_HIKIAI_GYOUSHA))]
    public interface HikiaiGyoushaDao : IS2Dao
    {
        /// <summary>
        /// 引合業者データ件数を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.CheckGyoushaData.sql")]
        DataTable CheckHikiaiGyoushaDataForEntity(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 業者コード最大値を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxGyoushaCode.sql")]
        string GetMaxGyoushaCode(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// 引合業者データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetHikiaiGyoushaData.sql")]
        DataTable GetHikiaiGyoushaDataForEntity(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Insert(M_HIKIAI_GYOUSHA data);
    }

    [Bean(typeof(M_HIKIAI_GENBA))]
    public interface HikiaiGenbaDao : IS2Dao
    {
        /// <summary>
        /// 引合現場データ件数を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.CheckHikiaiGenbaData.sql")]
        DataTable CheckHikiaiGenbaDataForEntity(M_HIKIAI_GENBA data);

        /// <summary>
        /// 現場コード最大値を取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxGenbaCode.sql")]
        string GetMaxGenbaCode(M_HIKIAI_GENBA data);

        /// <summary>
        /// 引合現場データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetHikiaiGenbaData.sql")]
        DataTable GetHikiaiGenbaDataForEntity(M_HIKIAI_GENBA data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA data);
    }

    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface MitsumoriEntryDao : IS2Dao
    {
        /// <summary>
        /// 見積入力データ取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMitsumoriEntryData.sql")]
        DataTable GetMitsumoriEntryDataForEntity(T_MITSUMORI_ENTRY data);

    }

    [Bean(typeof(T_MITSUMORI_DETAIL))]
    public interface MitsumoriDetailEntryDao : IS2Dao
    {
        /// <summary>
        /// 見積明細データ取得
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMitsumoriDetailEntryData.sql")]
        DataTable GetMitsumoriDetailEntryDataForEntity(T_MITSUMORI_DETAIL data);

    }


    internal class DAOClass : IS2Dao
    {
        public int Insert(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Update(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Delete(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        {
            throw new NotImplementedException();
        }

        public SuperEntity GetDataForEntity(SuperEntity date)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDateForStringSql(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
