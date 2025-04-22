// $Id: DBAccessor.cs 43947 2015-03-06 08:16:13Z huangxy@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Carriage.UnchinDaichou.Const;
using Shougun.Function.ShougunCSCommon.Utility;
using Shougun.Function.ShougunCSCommon.Dao;

namespace Shougun.Core.Carriage.UnchinDaichou.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// 
    /// FW側と業務側とでDaoが点在するため、
    /// 本クラスで呼び出すDaoをコントロールする
    /// </summary>
    internal class DBAccessor
    {
        #region フィールド
        /// <summary>
        /// IM_SYS_INFODao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;  // No.3688

        /// <summary>
        /// TUNCHINDao
        /// </summary>
        private TUNCHINDao unchinDao;

        /// <summary>
        ///  明細用テーブル
        /// </summary>
        private DataTable meisaiTable;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // フィールドの初期化
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.unchinDao = DaoInitUtility.GetComponent<TUNCHINDao>();
        }
        #endregion

        #region DBアクセッサ

        /// <summary>
        /// SYS_INFOを取得する
        /// </summary>
        /// <returns></returns>
        public M_SYS_INFO GetSysInfo()
        {
            M_SYS_INFO[] returnEntity = sysInfoDao.GetAllData();
            return returnEntity[0];
        }

        /// <summary>
        /// 明細用一覧データの取得
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        /// <param name="sysInfo"></param>
        /// <param name="hadSearched"></param>
        /// <returns></returns>
        internal DataTable GetIchiranData(UnchinDaichouHaniJokenPopUp.Const.UIConstans.ConditionInfo param, M_SYS_INFO sysInfo, ref bool hadSearched)
        {
            DTOClass dto = new DTOClass();
            dto.DENPYOU_DATE_FROM = param.StartDay;
            dto.DENPYOU_DATE_TO = param.EndDay;
            if (!string.IsNullOrEmpty(param.StartUnpanGyoushaCD))
            {
                dto.UNPAN_GYOUSHA_CD_FROM = param.StartUnpanGyoushaCD;
            }
            if (!string.IsNullOrEmpty(param.EndUnpanGyoushaCD))
            {
                dto.UNPAN_GYOUSHA_CD_TO = param.EndUnpanGyoushaCD;
            }
            this.meisaiTable = new DataTable();
            this.meisaiTable = this.unchinDao.GetUnchinDaichou(dto);
            // 数量と単位を結合、明細備考編集
            this.meisaiTable = this.EditDetail(this.meisaiTable, sysInfo);
            hadSearched = true;

            return this.meisaiTable;
        }

        /// <summary>
        /// 明細編集
        /// </summary>
        /// <param name="table">格納対象データテーブル</param>
        /// <param name="sysInfo"></param>
        /// <returns name="DataTable">格納後のデータテーブル</returns>
        private DataTable EditDetail(DataTable table, M_SYS_INFO sysInfo)
        {
            DataTable workTable = table.Copy();
            workTable.Columns["SUURYOU_UNIT"].ReadOnly = false;
            workTable.Columns["SUURYOU_UNIT"].MaxLength = UIConstans.MAX_LENGTH;
            foreach (DataRow row in workTable.Rows)
            {
                // 数量端数処理
                string formatStr = "#,##0.###";
                string suuryou = FormatUtility.ToAmountValue(row["SUURYOU"].ToString(), formatStr);

                // 単位の結合及び代入
                if (false == string.IsNullOrEmpty(suuryou))
                {
                    row["SUURYOU_UNIT"] = string.Format("{0}{1}", suuryou.ToString(), row["UNIT_NAME_RYAKU"].ToString());
                }
            }
            return workTable;
        }

        #endregion
    }
}
