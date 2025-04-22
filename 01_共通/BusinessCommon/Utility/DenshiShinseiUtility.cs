using System;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using r_framework.Entity;
using System.Data;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 電子申請用のUtility
    /// </summary>
    public class DenshiShinseiUtility
    {
        #region 定数
        /// <summary>電子申請ステータスの文字列</summary>
        public static readonly string STR_APPLYING = "申請中";
        public static readonly string STR_APPROVAL = "承認";
        public static readonly string STR_DENIAL = "否認";
        public static readonly string STR_COMPLETE = "移行済";
        public static readonly string STR_DENIAL_CONF = "否認確認";

        // 電子申請内容名マスタの申請内容名CD
        /// <summary>電子申請内容名:取引先</summary>
        public static readonly string NAIYOU_CD_TORIHIKISAKI = "1";
        /// <summary>電子申請内容名:業者</summary>
        public static readonly string NAIYOU_CD_GYOUSHA = "2";
        /// <summary>電子申請内容名:現場</summary>
        public static readonly string NAIYOU_CD_GENBA = "3";
        #endregion

        #region 列挙型
        public enum DENSHI_SHINSEI_STATUS
        {
            /// <summary>申請中</summary>
            APPLYING = 1,
            /// <summary>承認</summary>
            APPROVAL = 2,
            /// <summary>否認</summary>
            DENIAL = 3,
            /// <summary>移行済</summary>
            COMPLETE = 4,
            /// <summary>否認確認</summary>
            DENIAL_CONF = 5,
        }
        #endregion

        #region メソッド

        #region ToString(DENSHI_SHINSEI_STATUS)
        /// <summary>
        /// DENSHI_SHINSEI_STATUSの値から、表示文字列を返す。
        /// </summary>
        /// <param name="status">DENSHI_SHINSEI_STATUS</param>
        /// <returns>表示文字列</returns>
        public string ToString(DENSHI_SHINSEI_STATUS status)
        {
            string returnVal = string.Empty;

            switch (status)
            {
                case DENSHI_SHINSEI_STATUS.APPLYING:
                    returnVal = DenshiShinseiUtility.STR_APPLYING;
                    break;

                case DENSHI_SHINSEI_STATUS.APPROVAL:
                    returnVal = DenshiShinseiUtility.STR_APPROVAL;
                    break;

                case DENSHI_SHINSEI_STATUS.DENIAL:
                    returnVal = DenshiShinseiUtility.STR_DENIAL;
                    break;

                case DENSHI_SHINSEI_STATUS.COMPLETE:
                    returnVal = DenshiShinseiUtility.STR_COMPLETE;
                    break;
                case DENSHI_SHINSEI_STATUS.DENIAL_CONF:
                    returnVal = DenshiShinseiUtility.STR_DENIAL_CONF;
                    break;
            }

            return returnVal;
        }
        #endregion

        #region ToString(int)
        /// <summary>
        /// SHINSEI_STATUS_CDの値から、表示文字列を返す。
        /// </summary>
        /// <param name="statusCd">SHINSEI_STATUS_CD</param>
        /// <returns>表示文字列</returns>
        public string ToString(int statusCd)
        {
            string returnVal = string.Empty;

            switch (statusCd)
            {
                case (int)DENSHI_SHINSEI_STATUS.APPLYING:
                    returnVal = DenshiShinseiUtility.STR_APPLYING;
                    break;

                case (int)DENSHI_SHINSEI_STATUS.APPROVAL:
                    returnVal = DenshiShinseiUtility.STR_APPROVAL;
                    break;

                case (int)DENSHI_SHINSEI_STATUS.DENIAL:
                    returnVal = DenshiShinseiUtility.STR_DENIAL;
                    break;

                case (int)DENSHI_SHINSEI_STATUS.COMPLETE:
                    returnVal = DenshiShinseiUtility.STR_COMPLETE;
                    break;
                case (int)DENSHI_SHINSEI_STATUS.DENIAL_CONF:
                    returnVal = DenshiShinseiUtility.STR_DENIAL_CONF;
                    break;
            }

            return returnVal;
        }
        #endregion

        #region 電子申請可能データチェック
        public enum SHINSEI_MASTER_KBN
        {
            TORIHIKISAKI = 1,
            GYOUSHA = 2,
            GENBA = 3,
            HIKIAI_TORIHIKISAKI = 4,
            HIKIAI_GYOUSHA = 5,
            HIKIAI_GENBA_AND_HIKIAI_GYOUSHA = 6,
            HIKIAI_GENBA_AND_KIZON_GYOUSHA = 7
        }

        /// <summary>
        /// 電子申請可能データかどうか判定
        /// </summary>
        /// <param name="type">マスタの種類(取引先、業者、現場、引合取引先、引合業者、引合現場)</param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns>true:電子申請可能、false:電子申請不可</returns>
        public bool IsPossibleData(SHINSEI_MASTER_KBN type, string torihikisakiCd, string gyoushaCd, string genbaCd)
        {
            bool returnVal = false;

            var conditionDao = new T_DENSHI_SHINSEI_ENTRY();

            // 関係のない電子申請データを取得しないため、
            // 対象のマスタ条件以外はnullまたは空で検索させる
            conditionDao.TORIHIKISAKI_CD = string.Empty;
            conditionDao.GYOUSHA_CD = string.Empty;
            conditionDao.GENBA_CD = string.Empty;
            conditionDao.HIKIAI_TORIHIKISAKI_CD = string.Empty;
            conditionDao.HIKIAI_GYOUSHA_CD = string.Empty;
            conditionDao.HIKIAI_GENBA_CD = string.Empty;

            #region 条件設定
            switch (type)
            {
                case SHINSEI_MASTER_KBN.TORIHIKISAKI:
                    // 取引先
                    if (string.IsNullOrEmpty(torihikisakiCd))
                    {
                        return returnVal;
                    }

                    conditionDao.TORIHIKISAKI_CD = torihikisakiCd;

                    break;

                case SHINSEI_MASTER_KBN.GYOUSHA:
                    // 業者
                    if (string.IsNullOrEmpty(gyoushaCd))
                    {
                        return returnVal;
                    }

                    // 取引先は関係なく検索したいため条件に含めない
                    conditionDao.TORIHIKISAKI_CD = null;
                    conditionDao.GYOUSHA_CD = gyoushaCd;

                    break;

                case SHINSEI_MASTER_KBN.GENBA:
                    // 現場
                    if (string.IsNullOrEmpty(gyoushaCd)
                        || string.IsNullOrEmpty(genbaCd))
                    {
                        return returnVal;
                    }

                    // 取引先は関係なく検索したいため条件に含めない
                    conditionDao.TORIHIKISAKI_CD = null;
                    conditionDao.GYOUSHA_CD = gyoushaCd;
                    conditionDao.GENBA_CD = genbaCd;

                    break;

                case SHINSEI_MASTER_KBN.HIKIAI_TORIHIKISAKI:
                    // 引合取引先
                    if (string.IsNullOrEmpty(torihikisakiCd))
                    {
                        return returnVal;
                    }

                    conditionDao.HIKIAI_TORIHIKISAKI_CD = torihikisakiCd;

                    break;

                case SHINSEI_MASTER_KBN.HIKIAI_GYOUSHA:
                    // 引合業者
                    if (string.IsNullOrEmpty(gyoushaCd))
                    {
                        return returnVal;
                    }

                    // 取引先は関係なく検索したいため条件に含めない
                    conditionDao.TORIHIKISAKI_CD = null;
                    conditionDao.HIKIAI_TORIHIKISAKI_CD = null;
                    conditionDao.HIKIAI_GYOUSHA_CD = gyoushaCd;

                    break;

                case SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_HIKIAI_GYOUSHA:
                    // 引合現場(引合業者)
                    if (string.IsNullOrEmpty(gyoushaCd)
                        || string.IsNullOrEmpty(genbaCd))
                    {
                        return returnVal;
                    }

                    // 取引先は関係なく検索したいため条件に含めない
                    conditionDao.TORIHIKISAKI_CD = null;
                    conditionDao.HIKIAI_TORIHIKISAKI_CD = null;
                    conditionDao.HIKIAI_GYOUSHA_CD = gyoushaCd;
                    conditionDao.HIKIAI_GENBA_CD = genbaCd;

                    break;

                case SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_KIZON_GYOUSHA:
                    // 引合現場(既存業者)
                    if (string.IsNullOrEmpty(gyoushaCd)
                        || string.IsNullOrEmpty(genbaCd))
                    {
                        return returnVal;
                    }

                    // 取引先は関係なく検索したいため条件に含めない
                    conditionDao.TORIHIKISAKI_CD = null;
                    conditionDao.HIKIAI_TORIHIKISAKI_CD = null;
                    conditionDao.GYOUSHA_CD = gyoushaCd;
                    conditionDao.HIKIAI_GENBA_CD = genbaCd;

                    break;
            }
            #endregion

            var dsStatusDao = DaoInitUtility.GetComponent<IT_DENSHI_SHINSEI_STATUSDao>();
            bool gyoushaCheck = true;
            
            if (type == SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_KIZON_GYOUSHA)
            {
                /*
                 * 引合現場(既存業者使用)の場合
                 * 　①既存業者が元々引合業者の時点で引合現場が申請され、その後本登録によって引合現場(既存業者)に変わった
                 * 　②最初から既存業者を使用した引合現場
                 * の2パターンがあるため、引合現場の業者CDが引合業者マスタの「移行後業者CD」に有るかを確認し、有る場合は①のパターンとして申請内容をチェックする。
                 */

                /* パターン①のチェック */
                string gyoushaCdBefore = string.Empty;
                var mHikiaiGyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_HIKIAI_GYOUSHADao>();
                string sql = "SELECT * FROM M_HIKIAI_GYOUSHA WHERE GYOUSHA_CD_AFTER = '" + conditionDao.GYOUSHA_CD + "'";
                DataTable hikiaiGyoushaTable = mHikiaiGyoushaDao.GetDateForStringSql(sql);
                if (hikiaiGyoushaTable != null && hikiaiGyoushaTable.Rows.Count > 0)
                {
                    // 移行後の業者CDは業者マスタ上PKなので1件
                    gyoushaCdBefore = hikiaiGyoushaTable.Rows[0]["GYOUSHA_CD"] == null ? string.Empty : hikiaiGyoushaTable.Rows[0]["GYOUSHA_CD"].ToString();
                }

                if (!string.IsNullOrEmpty(gyoushaCdBefore))
                {
                    var searchDao = new T_DENSHI_SHINSEI_ENTRY();
                    searchDao.HIKIAI_GYOUSHA_CD = gyoushaCdBefore;
                    searchDao.HIKIAI_GENBA_CD = genbaCd;
                    searchDao.SHINSEI_MASTER_KBN = 6;
                    DataTable resultData = dsStatusDao.GetAllValidDataMinCols(searchDao);

                    returnVal = true;
                    foreach (DataRow row in resultData.Rows)
                    {
                        int statusCd = 0;
                        if (row["SHINSEI_STATUS_CD"] != null
                            && int.TryParse(row["SHINSEI_STATUS_CD"].ToString(), out statusCd))
                        {
                            switch (statusCd)
                            {
                                case (int)DENSHI_SHINSEI_STATUS.APPLYING:
                                case (int)DENSHI_SHINSEI_STATUS.APPROVAL:
                                case (int)DENSHI_SHINSEI_STATUS.DENIAL_CONF:
                                    returnVal = false;
                                    gyoushaCheck = false;
                                    break;
                            }
                        }
                    }
                }
            }

            if (gyoushaCheck)
            {
                if (type == SHINSEI_MASTER_KBN.HIKIAI_GENBA_AND_KIZON_GYOUSHA)
                {
                    /* パターン②のチェック */
                    conditionDao.SHINSEI_MASTER_KBN = 7;
                }
                DataTable resultData = dsStatusDao.GetAllValidDataMinCols(conditionDao);

                returnVal = true;
                foreach (DataRow row in resultData.Rows)
                {
                    int statusCd = 0;
                    if (row["SHINSEI_STATUS_CD"] != null
                        && int.TryParse(row["SHINSEI_STATUS_CD"].ToString(), out statusCd))
                    {
                        switch (statusCd)
                        {
                            case (int)DENSHI_SHINSEI_STATUS.APPLYING:
                            case (int)DENSHI_SHINSEI_STATUS.APPROVAL:
                            case (int)DENSHI_SHINSEI_STATUS.DENIAL_CONF:
                                returnVal = false;
                                break;
                        }
                    }
                }
            }

            return returnVal;
        }
        #endregion

        #endregion

    }
}
