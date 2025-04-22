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

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.DAO
{
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface  DAOClass_header : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_NYUUKIN_ENTRY data);

        /// <summary>
        /// 明細のheader部分の情報を取得する
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetHeaderSql.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface DAOClass_Kaishi : IS2Dao
    {
        /// <summary>
        /// 開始残高値を取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetKaishiZantaka.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_SHAIN))]
    public interface DAOClass_PopupEikyouTantou : IS2Dao
    {
        /// <summary>
        /// 営業担当者POPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetPopupEikyouTantou.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_TORIHIKISAKI))]
    public interface DAOClass_PopupTorihikisaki : IS2Dao
    {
        /// <summary>
        /// 取引先コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetPopupTorihikisaki.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_KYOTEN))]
    public interface DAOClass_PopupKyoten : IS2Dao
    {
        /// <summary>
        /// 拠点コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetPopupKyoten.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_NYUUSHUKKIN_KBN))]
    public interface DAOClass_PopupNyushuukkin_Kbn : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードPOPUP画面のデータソースを取得
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetPopupNyuushukkin_kbn.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface DAOClass_Torihiki : IS2Dao
    {
        /// <summary>
        /// 取引先の情報を取得
        /// </summary>
        /// <param name="TORIHIKISAKI_CD"></param>
        /// <returns>M_TORIHIKISAKI_SEIKYUU</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetTorihikisaki.sql")]
        DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Insert(T_NYUUKIN_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_DETAIL data);
    }

    [Bean(typeof(T_NYUUKIN_DETAIL))]
    public interface DAOClass_meisai : IS2Dao
    {
        /// <summary>
        /// 明細一覧の情報を取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// <returns>T_NYUUKIN_DETAIL</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetMeisai.sql")]
        DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_DETAIL data);
    }

    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface DAOClass_Shimeshori : IS2Dao
    {
        /// <summary>
        /// 締処理中のチェックをするためのSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetShimeshori.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface DAOClass_Shimezumi : IS2Dao
    {
        /// <summary>
        /// 締済みチェックをするためのSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetShimezumi.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_KYOTEN))]
    public interface DAOClass_CheckKyoten : IS2Dao
    {
        /// <summary>
        /// 拠点コードをチェックするSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.CheckKyoten.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_TORIHIKISAKI))]
    public interface DAOClass_CheckTorihikisaki : IS2Dao
    {
        /// <summary>
        /// 取引先コードをチェックするSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.CheckTorihikisaki.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_SHAIN))]
    public interface DAOClass_CheckShain : IS2Dao
    {
        /// <summary>
        /// 社員コードをチェックするSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.CheckShain.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(M_NYUUSHUKKIN_KBN))]
    public interface DAOClass_CheckNyuushukinKbn : IS2Dao
    {
        /// <summary>
        /// 入出金区分コードをチェックするSQL文
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.CheckNyuushukinKbn.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface DAOClass_Kesikomi : IS2Dao
    {
        /// <summary>
        /// 消込一覧のデータを取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetKesikomi.sql")]
        DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_KESHIKOMI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("SEIKYUU_NUMBER", "NYUUKIN_NUMBER", "KESHIKOMI_GAKU", "KESHIKOMI_BIKOU", "NYUUKIN_SEQ", "TIME_STAMP")]
        int Update(T_NYUUKIN_KESHIKOMI data);
    }
    
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface DAOClass_Zenkai : IS2Dao
    {
        /// <summary>
        /// 前回請求額を取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetZenkaiseikyuu.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface Dao_MaxKesikomiSeq : IS2Dao
    {
        /// <summary>
        /// 前回請求額を取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetMaxKesikomiSeq.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }

    [Bean(typeof(T_NYUUKIN_KESHIKOMI))]
    public interface Dao_Seikyuu_Number : IS2Dao
    {
        /// <summary>
        /// 前回請求額を取得する。
        /// </summary>
        /// <param name="NYUKIN_NO"></param>
        /// /// <param name="NYUKIN_NO"></param>
        /// <returns>DAOClass</returns>
        [SqlFile("Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku.Sql.GetSeikyuuNumber.sql")]
        DataTable GetDataForEntity(DTOClass data);
    }
}
