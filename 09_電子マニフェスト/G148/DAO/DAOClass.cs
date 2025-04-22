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
using Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.DAO
{
    [Bean(typeof(DT_R24))]
    public interface JTR24DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Sql.SelectJuuyouTuutiKensuu.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(DT_R24))]
    public interface OTR24DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Sql.SelectOshiraseTuutiKensuu.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(QUE_INFO))]
    public interface QIDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.TopHeNoJouhouKoukai.Sql.SelectTuushinRirekiKensuu.sql")]
        new DataTable GetDataForEntity(DTOClass data);
    }
}
