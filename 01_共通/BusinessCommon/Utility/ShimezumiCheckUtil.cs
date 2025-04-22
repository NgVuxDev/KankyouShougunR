using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class ShimezumiCheckUtil
    {
        /// <summary>
        /// 伝種区分
        /// </summary>
        public enum DenshuKubunCD
        {
            DENSHU_KBN_CD_UKEIRE = 1,           //受入
            DENSHU_KBN_CD_SHUKKA = 2,           //出荷
            DENSHU_KBN_CD_URIAGESHIHARAI = 3,   //売上支払
            #region "CongBinh 20150714 追加"
            DENSHU_KBN_CD_NYUUKIN = 10,         //入金
            DENSHU_KBN_CD_SHUKKIN = 20           //出金
            #endregion
        }

        /// <summary>
        /// 締状況チェック処理
        /// 請求明細、精算明細、在庫明細を確認して、対象の伝票に締済のデータが存在するか確認する。
        /// </summary>
        /// <param name="prmSystemId">システムID</param>
        /// <param name="prmSEQ">枝番</param>
        /// <param name="prmTorihikisakiCd">取引先CD</param>
        /// <param name="prmDenshukbn">伝種区分</param>
        /// <param name="prmNeedCheckZaiko">在庫明細を確認する</param>
        /// <returns>Boolean</returns>
        public static bool CheckAllShimeStatus(SqlInt64 prmSystemId, SqlInt32 prmSEQ, string prmTorihikisakiCd, DenshuKubunCD prmDenshukbn, bool prmNeedCheckZaiko)
        {
            bool retval = false;

            long systemId = -1;
            int seq = -1;

            if (!prmSystemId.IsNull) systemId = (long)prmSystemId;
            if (!prmSEQ.IsNull) seq = (int)prmSEQ;
            if (systemId != -1 && seq != -1)
            {
                // 締処理状況判定用データ取得
                DataTable seikyuuData = GetSeikyuMeisaiData(systemId, seq, -1, prmTorihikisakiCd, (int)prmDenshukbn);
                DataTable seisanData = GetSeisanMeisaiData(systemId, seq, -1, prmTorihikisakiCd, (int)prmDenshukbn);

                // 締処理状況(請求明細)
                if (seikyuuData != null && 0 < seikyuuData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(精算明細)
                if (retval == false && seisanData != null && 0 < seisanData.Rows.Count)
                {
                    retval = true;
                }

                // 締処理状況(在庫明細)
                if (retval == false && prmNeedCheckZaiko == true)
                {
                    switch (prmDenshukbn)
                    {
                        //受入の場合
                        case DenshuKubunCD.DENSHU_KBN_CD_UKEIRE:
                            T_ZAIKO_UKEIRE_DETAIL zaikoUkeireData = GetZaikoUkeireData(systemId, seq, (int)DenshuKubunCD.DENSHU_KBN_CD_UKEIRE);
                            if (zaikoUkeireData != null)
                            {
                                retval = true;
                            }
                            break;
                        //出荷の場合
                        case DenshuKubunCD.DENSHU_KBN_CD_SHUKKA:
                            T_ZAIKO_SHUKKA_DETAIL zaikoShukkaData = GetZaikoShukkaData(systemId, seq, (int)DenshuKubunCD.DENSHU_KBN_CD_SHUKKA);
                            if (zaikoShukkaData != null)
                            {
                                retval = true;
                            }
                            break;
                        #region "CongBinh 20150714 追加"
                        default:
                            retval = false;
                            break;
                        #endregion
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// 請求明細取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>DataTable</returns>
        internal static DataTable GetSeikyuMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd, int denshuKbn)
        {
            T_SEIKYUU_DETAIL keyEntity = new T_SEIKYUU_DETAIL();
            IT_SEIKYUU_DETAILDao seikyuuDetail = r_framework.Utility.DaoInitUtility.GetComponent<IT_SEIKYUU_DETAILDao>();
            
            keyEntity.DENPYOU_SHURUI_CD = (SqlInt16)denshuKbn;
            keyEntity.DENPYOU_SYSTEM_ID = systemId;
            keyEntity.DENPYOU_SEQ = seq;
            if (0 <= detailSystemId)
            {
                keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
            }
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            
            return seikyuuDetail.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 清算明細取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq">枝番</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>DataTable</returns>
        internal static DataTable GetSeisanMeisaiData(long systemId, int seq, long detailSystemId, string torihikisakiCd, int denshuKbn)
        {
            T_SEISAN_DETAIL keyEntity = new T_SEISAN_DETAIL();
            IT_SEISAN_DETAILDao seisanDetail = r_framework.Utility.DaoInitUtility.GetComponent<IT_SEISAN_DETAILDao>();
            
            keyEntity.DENPYOU_SHURUI_CD = (SqlInt16)denshuKbn;
            keyEntity.DENPYOU_SYSTEM_ID = systemId;
            keyEntity.DENPYOU_SEQ = seq;
            if (0 <= detailSystemId)
            {
                keyEntity.DETAIL_SYSTEM_ID = detailSystemId;
            }
            keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
            
            return seisanDetail.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 締処理状況（在庫）取得(出荷入力用)
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns></returns>
        internal static T_ZAIKO_SHUKKA_DETAIL GetZaikoShukkaData(long systemId, int seq, int denshuKbn)
        {
            TZSDClass tzsdDao = r_framework.Utility.DaoInitUtility.GetComponent<TZSDClass>();
            T_ZAIKO_SHUKKA_DETAIL keyEntity = new T_ZAIKO_SHUKKA_DETAIL();
            
            keyEntity.DENSHU_KBN_CD = (SqlInt16)denshuKbn;
            keyEntity.SYSTEM_ID = systemId;
            keyEntity.SEQ = seq;

            return tzsdDao.GetDataForEntity(keyEntity);
        }

        /// <summary>
        /// 締処理状況（在庫）取得(受入入力用)
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="seq"></param>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns></returns>
        internal static T_ZAIKO_UKEIRE_DETAIL GetZaikoUkeireData(long systemId, int seq, int denshuKbn)
        {
            TZUDClass tzudDao = r_framework.Utility.DaoInitUtility.GetComponent<TZUDClass>();
            T_ZAIKO_UKEIRE_DETAIL keyEntity = new T_ZAIKO_UKEIRE_DETAIL();

            keyEntity.DENSHU_KBN_CD = (SqlInt16)denshuKbn;
            keyEntity.SYSTEM_ID = systemId;
            keyEntity.SEQ = seq;

            return tzudDao.GetDataForEntity(keyEntity);
        }
    }

    /// <summary>
    /// 請求明細DAO
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface IT_SEIKYUU_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.IT_SEIKYUU_DETAILDao_GetDataForEntity.sql")]
        DataTable GetDataForEntity(T_SEIKYUU_DETAIL data);
    }

    /// <summary>
    /// 精算明細DAO
    /// </summary>
    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface IT_SEISAN_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.IT_SEISAN_DETAILDao_GetDataForEntity.sql")]
        DataTable GetDataForEntity(T_SEISAN_DETAIL data);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_SHUKKA_DETAIL))]
    internal interface TZSDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_SHUKKA_DETAIL GetDataForEntity(T_ZAIKO_SHUKKA_DETAIL data);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_UKEIRE_DETAIL))]
    internal interface TZUDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_UKEIRE_DETAIL GetDataForEntity(T_ZAIKO_UKEIRE_DETAIL data);
    }
}
