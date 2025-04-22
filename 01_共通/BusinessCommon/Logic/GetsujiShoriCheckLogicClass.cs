using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.BusinessCommon.Logic
{
    public class GetsujiShoriCheckLogicClass
    {
        #region Field

        /// <summary>月次処理チェックロジック用DAO</summary>
        GetsujiShoriDao dao;
        /// <summary>月次処理中DAO</summary>
        T_GETSUJI_SHORI_CHUDao getsujiShoriChuDao;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetsujiShoriCheckLogicClass()
        {
            this.dao = DaoInitUtility.GetComponent<GetsujiShoriDao>();
            this.getsujiShoriChuDao = DaoInitUtility.GetComponent<T_GETSUJI_SHORI_CHUDao>();
        }

        #endregion

        #region Method

        /// <summary>
        /// 月次処理中かを調べます
        /// </summary>
        /// <returns>月次処理中：True</returns>
        public bool CheckGetsujiShoriChu()
        {
            bool val = false;

            /* 月次処理中かのチェック */
            DataTable dt = this.getsujiShoriChuDao.GetAllData();
            if (dt != null && dt.Rows.Count > 0)
            {
                val = true;
            }

            return val;
        }

        /// <summary>
        /// 指定した年月日以前で月次処理中かを調べます
        /// </summary>
        /// <param name="date">チェックする日付</param>
        /// <returns>月次処理中：True</returns>
        public bool CheckGetsujiShoriChu(DateTime date)
        {
            bool val = false;

            /* 月次処理中かのチェック */
            DataTable dt = this.getsujiShoriChuDao.GetAllData();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int year = int.Parse(row["YEAR"].ToString());
                    int month = int.Parse(row["MONTH"].ToString());

                    // 月次処理中月の末日を取得
                    DateTime lockDate = new DateTime(year, month, 1);
                    lockDate = lockDate.AddMonths(1).AddDays(-1);

                    if (date.CompareTo(lockDate) <= 0)
                    {
                        val = true;
                        break;
                    }
                }
            }

            return val;
        }

        /// <summary>
        /// 月次処理中の最新年月日(日付は末日)を取得します。
        /// 月次処理中ではない場合空文字が返却されます。
        /// </summary>
        /// <returns>月次処理中の最新日付の文字列(yyy/MM/dd)</returns>
        public string GetLatestGetsjiShoriChuDateTime()
        {
            DataTable dt = this.getsujiShoriChuDao.GetLatestData();

            if (dt != null && dt.Rows.Count > 0)
            {
                // ソート済みなので1行目取得
                DataRow dr = dt.Rows[0];
                int year = int.Parse(dr["YEAR"].ToString());
                int month = int.Parse(dr["MONTH"].ToString());

                DateTime date = new DateTime(year, month, 1);
                date = date.AddMonths(1).AddDays(-1);

                return date.ToString("yyyy/MM/dd");
            }

            return string.Empty;
        }

        /// <summary>
        /// 指定した年月が月次処理によってロック中かを調べます
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns>ロックされている：True　ロックされていない：False</returns>
        public bool CheckGetsujiShoriLock(short year, short month)
        {
            bool val = false;
            
            /* 売上・支払月次処理データの最新日付からその範囲内かのチェック */
            GetusjiShoriCheckDTOClass dto = new GetusjiShoriCheckDTOClass();
            dto.YEAR = year;
            DataTable dt = this.dao.GetGetusjiShoriLockCheckData(dto);

            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime checkDate = new DateTime(year, month, 1);
                foreach (DataRow row in dt.Rows)
                {
                    int tmpYear = int.Parse(row["YEAR"].ToString());
                    int tmpMonth = int.Parse(row["MONTH"].ToString());
                    DateTime date = new DateTime(tmpYear, tmpMonth, 1);

                    if (checkDate.CompareTo(date) <= 0)
                    {
                        val = true;
                        break;
                    }
                }
            }

            return val;
        }

        /// <summary>
        /// 締済みの最新年月を引数で指定した年月に設定します
        /// 最新締めデータが無い場合、引数の値は更新されません
        /// </summary>
        /// <param name="year">月次年月の年</param>
        /// <param name="month">月次年月の月</param>
        public void GetLatestGetsujiDate(ref int year, ref int month)
        {
            GetusjiShoriCheckDTOClass dto = new GetusjiShoriCheckDTOClass();
            DataTable dt = this.dao.GetLatestGetsujiDate(dto);
            if (dt != null && dt.Rows.Count > 0)
            {
                // 最新月次年月でソート済みのため1件目取得
                DataRow row = dt.Rows[0];
                year = int.Parse(row["YEAR"].ToString());
                month = int.Parse(row["MONTH"].ToString());
            }
        }

        #endregion
    }
}
