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

namespace Shougun.Core.PaperManifest.Manifestsuiihyo
{
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

    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface GET_DATA_DaoCls : IS2Dao
    {
        // 1.排出(紙)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetKamiManiHstGyosha.sql")]
        DataTable GetKamiHaishutuData(SerchCheckManiDtoCls data);

        // 1.排出(電子)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetDenManiHstGyosha.sql")]
        DataTable GetDenHaishutuData(SerchCheckManiDtoCls data);

        // 2.運搬(紙)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetKamiManiUpnGyosha.sql")]
        DataTable GetKamiUnpanData(SerchCheckManiDtoCls data);

        // 2.運搬(電子)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetDenManiUpnGyosha.sql")]
        DataTable GetDenUnpanData(SerchCheckManiDtoCls data);

        // 3.処分(紙)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetKamiManiSyobunGyosha.sql")]
        DataTable GetKamiShobunData(SerchCheckManiDtoCls data);

        // 3.処分(電子)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetDenManiSyobunGyosha.sql")]
        DataTable GetDenShobunData(SerchCheckManiDtoCls data);

        // 4.最終(紙)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetKamiManiLastGenba.sql")]
        DataTable GetKamiSaishuuData(SerchCheckManiDtoCls data);

        // 4.最終(電子)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetDenManiLastGenba.sql")]
        DataTable GetDenSaishuuData(SerchCheckManiDtoCls data);

        // 5.廃棄(紙)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetKamiManiHaiki.sql")]
        DataTable GetKamiHaikiData(SerchCheckManiDtoCls data);

        // 5.廃棄(電子)
        [SqlFile("Shougun.Core.PaperManifest.Manifestsuiihyo.Sql.GetDenManiHaiki.sql")]
        DataTable GetDenHaikiData(SerchCheckManiDtoCls data);
    }

    /// <summary>
    /// 会社名
    /// </summary>
    [Bean(typeof(M_CORP_INFO))]
    public interface IM_CORP_NAMEDaoCls : IS2Dao
    {
        [Sql("SELECT TOP 1 CORP_NAME FROM M_CORP_INFO")]
        string GetCorpName();
    }

    /// <summary>
    /// 単位
    /// </summary>
    [Bean(typeof(M_UNIT))]
    public interface GET_UNIT_DaoCls : IS2Dao
    {
        [Sql("SELECT TOP 1 M_UNIT.UNIT_NAME FROM M_SYS_INFO INNER JOIN M_UNIT ON M_SYS_INFO.KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD WHERE M_SYS_INFO.SYS_ID = 0")]
        string GetUnit();
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface GET_GYOUSHA_DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetGyoushay(string sql);
    }

    [Bean(typeof(M_GENBA))]
    public interface GET_GENBA_DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetGenba(string sql);
    }

    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface GET_HAIKI_SHURUI_DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetHaikiShurui(string sql);
    }
}
