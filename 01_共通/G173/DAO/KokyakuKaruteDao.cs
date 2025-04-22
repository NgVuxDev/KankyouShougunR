using System.Collections.Generic;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.KokyakuKarute.DAO
{
    /// <summary>
    /// 取引先マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IM_TorihikisakiDao : IS2Dao
    {
        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_TorihikisakiDao_GetTorihikisakiData.sql")]
        M_TORIHIKISAKI GetTorihikisakiData(string torihikisakiCd);

        /// <summary>
        /// 取引先コードをもとに取引先のヘッダデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_TorihikisakiDao_GetTorihikisakiHeaderData.sql")]
        DataTable GetTorihikisakiHeaderData(string torihikisakiCd);

        /// <summary>
        /// 取引先コードをもとに取引先の基本データを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_TorihikisakiDao_GetTorihikisakiKihonData.sql")]
        DataTable GetTorihikisakiKihonData(string torihikisakiCd);
    }

    /// <summary>
    /// 個別品名単価マスタDao
    /// </summary>
    [Bean(typeof(M_KOBETSU_HINMEI_TANKA))]
    public interface IM_KobetsuHinmeiTankaDao : IS2Dao
    {

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="denpyouKbnCd"></param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_KobetsuHinmeiTankaDao_GetData.sql")]
        DataTable GetDataBySqlFile(M_KOBETSU_HINMEI_TANKA data, int denpyouKbnCd);
    }

    /// <summary>
    /// 取引先請求情報マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TorihikisakiSeikyuuDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_TorihikisakiSeikyuuDao_GetTorihikisakiSeikyuuData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd);
    }

    /// <summary>
    /// 取引先支払情報マスタDao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface IM_TorihikisakiShiharaiDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_TorihikisakiShiharaiDao_GetTorihikisakiShiharaiData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd);
    }

    /// <summary>
    /// 業者マスタDao
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface IM_GyoushaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_GyoushaDao_GetGyoushaInfoData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="gyoushaCd">業者コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_GyoushaDao_GetGyoushaData.sql")]
        DataTable GetDataBySqlFile2(string gyoushaCd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetIchiranGyoushaDataSql.sql")]
        DataTable GetDataBySqlFile3(M_GYOUSHA data);

        /// <summary>
        /// 一件データ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_GyoushaDao_GetGyoushaDataForMoveData.sql")]
        DataTable GetDataBySqlFileForMoveData(string torihikisakiCd, string gyoushaCd);
    }

    /// <summary>
    /// 現場マスタDao
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface IM_GenbaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_GenbaDao_GetGenbaInfoData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_GenbaDao_GetGenbaData.sql")]
        DataTable GetDataBySqlFile2(M_GENBA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetIchiranGenbaDataSql.sql")]
        DataTable GetDataBySqlFile3(M_GENBA data);
    }

    /// <summary>
    /// 受付Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface IT_UketsukeDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UketsukeDao_GetUketsukeData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 受付(収集)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface IT_UketsukeSSDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UketsukeSSDetailDao_GetUketsukeSSDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付(出荷)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    public interface IT_UketsukeSKDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UketsukeSKDetailDao_GetUketsukeSKDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付(持込)明細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_DETAIL))]
    public interface IT_UketsukeMKDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UketsukeMKDetailDao_GetUketsukeMKDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受付クレームDao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_CM_ENTRY))]
    public interface IT_UketsukeCMDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UketsukeCMDao_GetUketsukeCMData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 計量Dao
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface IT_KeiryouDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_KeiryouDao_GetKeiryouData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd);
    }

    /// <summary>
    /// 計量明細Dao
    /// </summary>
    [Bean(typeof(T_KEIRYOU_DETAIL))]
    public interface IT_KeiryouDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_KeiryouDetailDao_GetKeiryouDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 受入Dao
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface IT_UkeireDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UkeireDao_GetUkeireData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 受入明細Dao
    /// </summary>
    [Bean(typeof(T_UKEIRE_DETAIL))]
    public interface IT_UkeireDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UkeireDetailDao_GetUkeireDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 出荷Dao
    /// </summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface IT_ShukkaDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkaDao_GetShukkaData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得(検収データ)
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkaDao_GetShukkaKenshuuEntryData.sql")]
        DataTable GetDataBySqlFileKenshuuData(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 出荷明細Dao
    /// </summary>
    [Bean(typeof(T_SHUKKA_DETAIL))]
    public interface IT_ShukkaDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkaDetailDao_GetShukkaDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkaDao_GetShukkaKenshuuData.sql")]
        DataTable GetShukkaKenshuuData(long systemId, int seq);
    }

    /// <summary>
    /// 売上/支払Dao
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface IT_UrShDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UrShDao_GetUrShData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UrShDao_GetDainouData.sql")]
        DataTable GetDainouDataBySqlFile(string torihikisakiCd, string gyoushaCd, string genbaCd, string kyotenCd, string fromDate, string toDate);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        T_UR_SH_ENTRY[] GetDataForEntity(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上/支払明細Dao
    /// </summary>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface IT_UrShDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UrShDetailDao_GetUrShDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_UrShDetailDao_GetDainouDetailData.sql")]
        DataTable GetDainouDataBySqlFile(long systemIduUkiere, int seqUkiere, long systemIdShukka, int seqShukka);
    }

    /// <summary>
    /// 請求明細Dao
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface IT_SeikyuuDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="urShNumber">伝票番号</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_SeikyuuDetailDao_GetSeikyuuDetailCount.sql")]
        int GetDataBySqlFile(long denpyouSystemId, int denpyouSeq, long detailSystemId, long denpyouNumber, int denpyouShuruiCd);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_SeikyuuDetailDao_GetSeikyuuDetailCount2.sql")]
        int GetDataBySqlFile2(long denpyouSystemId, int denpyouSeq, int denpyouShuruiCd);
    }

    /// <summary>
    /// 清算明細Dao
    /// </summary>
    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface IT_SeisanDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="denpyouSystemId">システムID</param>
        /// <param name="denpyouSeq">枝番</param>
        /// <param name="denpyouShuruiCd">伝票種類CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_SeisanDetailDao_GetSeisanDetailCount.sql")]
        int GetDataBySqlFile(long denpyouSystemId, int denpyouSeq, int denpyouShuruiCd);
    }

    /// <summary>
    /// 入金Dao
    /// </summary>
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface IT_NyuukinDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_NyuukinDao_GetNyuukinData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string kyotenCd, string fromDate, string toDate);

        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        T_NYUUKIN_ENTRY[] GetDataForEntity(T_NYUUKIN_ENTRY data);
    }

    /// <summary>
    /// 入金明細Dao
    /// </summary>
    [Bean(typeof(T_NYUUKIN_DETAIL))]
    public interface IT_NyuukinDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_NyuukinDetailDao_GetNyuukinDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 出金Dao
    /// </summary>
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    public interface IT_ShukkinDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先コード</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkinDao_GetShukkinData.sql")]
        DataTable GetDataBySqlFile(string torihikisakiCd, string kyotenCd, string fromDate, string toDate);
    }

    /// <summary>
    /// 出金明細Dao
    /// </summary>
    [Bean(typeof(T_SHUKKIN_DETAIL))]
    public interface IT_ShukkinDetailDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IT_ShukkinDetailDao_GetShukkinDetailData.sql")]
        DataTable GetDataBySqlFile(long systemId, int seq);
    }

    /// <summary>
    /// 委託契約Dao
    /// </summary>
    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface IM_ItakuKeiyakuDao : IS2Dao
    {
        /// <summary>
        /// ユーザ指定の検索条件によるデータ取得
        /// </summary>
        /// <param name="GyoushaCd">業者コード</param>
        /// <param name="GenbaCd">現場コード</param>
        /// <returns>現場コード=nullの場合、業者の委託契約；現場コード!=nullの場合、現場の委託契約</returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.IM_ItakuKeiyakuDao_GetGyoushaItakuKeiyakuData.sql")]
        DataTable GetDataBySqlFile(string gyoushaCd, string genbaCd);
    }

    /// <summary>
    /// 顧客カルテ用汎用Dao
    /// </summary>
    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface IM_KokyakuKaruteDao : IS2Dao
    {
        /// <summary>
        /// 顧客カルテ表示用定期品名情報取得
        /// </summary>
        /// <param name="gyoushaCD">業者コード</param>
        /// <param name="genbaCD">現場コード</param>
        /// <returns name ="DataTable"></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetTeikiHinmeiData.sql")]
        DataTable GetTeikiHinmeiData(string gyoushaCD, string genbaCD);

        /// <summary>
        /// 顧客カルテ表示用月極品名情報取得
        /// </summary>
        /// <param name="gyoushaCD">業者コード</param>
        /// <param name="genbaCD">現場コード</param>
        /// <returns name ="DataTable"></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetTsukiHinmeiData.sql")]
        DataTable GetTsukiHinmeiData(string gyoushaCD, string genbaCD);
    }

    [Bean(typeof(SearchResultContena))]
    public interface ContenaDao : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetIchiranDataSql.sql")]
        List<SearchResultContena> GetIchiranDataSql(ContenaDTO data);
        /// <summary>
        /// 設置コンテナ一覧画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetIchiranJissekiDataSql.sql")]
        List<SearchResultContena> GetIchiranJissekiDataSql(ContenaDTO data);
        /// <summary>
        /// 現場情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetGenbrDataSql.sql")]
        new DataTable GetGenbrDataSql(ContenaDTO data);
        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetContenaDataSql.sql")]
        new DataTable GetContenaDataSql(ContenaDTO data);
        /// <summary>
        /// コンテナ情報データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetContenaDataByCDSql.sql")]
        new DataTable GetContenaDataSqlByCD(ContenaDTO data);
    }

    [Bean(typeof(SearchResultContena))]
    public interface SuuryouKanriDAOClass : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用(数量管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KokyakuKarute.Sql.GetIchiranDataSqlForSuuryouKanri.sql")]
        List<SearchResultContena> GetIchiranDataSqlForSuuryouKanri(ContenaDTO data);
    }
}

