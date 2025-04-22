using System;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dao;
using Shougun.Core.Allocation.TeikiJissekiHoukoku.Dto;
using Shougun.Core.Allocation.TeikiJissekiHoukoku.Dao;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku.Accessor
{
    /// <summary>
    /// DBAccessするためのクラス
    /// </summary>
    public class DBAccessor
    {
        #region フィールド

        /// <summary>
        /// 定期実績CSV出力DAO
        /// </summary>
        ITeikiJissekiHoukokuDao teikiJissekiHoukokuDao;

        /// <summary>
        /// 拠点DAO
        /// </summary>
        IM_KYOTENDao kyotenDao;

        /// <summary>
        /// 市区町村DAO
        /// </summary>
        IM_SHIKUCHOUSONDao shikuchousonDao;

        #region 保存用領域

        /// <summary>
        /// 拠点エンティティ配列
        /// </summary>
        M_KYOTEN[] kyotenEntities;

        /// <summary>
        /// 市区町村エンティティ配列
        /// </summary>
        M_SHIKUCHOUSON[] shikuchousonEntities;

        #endregion

        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DBAccessor()
        {
            // スタートアッププロジェクトのDiconに情報が設定されていることを必ず確認
            this.teikiJissekiHoukokuDao = DaoInitUtility.GetComponent<ITeikiJissekiHoukokuDao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.shikuchousonDao = DaoInitUtility.GetComponent<IM_SHIKUCHOUSONDao>();

            // よく使用するテーブルをメモリに保存しておく
            this.kyotenEntities = this.kyotenDao.GetAllValidData(new M_KYOTEN());
            this.shikuchousonEntities = this.shikuchousonDao.GetAllValidData(new M_SHIKUCHOUSON());
		}
        #endregion

        #region DBアクセッサ

        /// <summary>
        /// 拠点取得
        /// </summary>
        /// <param name="kyotenCd"></param>
        /// <returns></returns>
        internal M_KYOTEN GetKyoten(short kyotenCd)
        {
            return this.kyotenEntities.Where(
                x => !x.KYOTEN_CD.IsNull && x.KYOTEN_CD.Value == kyotenCd)
                .FirstOrDefault();
        }

        /// <summary>
        /// 実績報告データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal TeikiJissekiHoukokuDto[] GetJissekiHoukoku_1(TeikiJissekiHoukokuDto data)
        {
            return this.teikiJissekiHoukokuDao.GetReportDetailData_1(data);
        }

        /// <summary>
        /// 実績報告データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal TeikiJissekiHoukokuDto[] GetJissekiHoukoku_2(TeikiJissekiHoukokuDto data)
        {
            return this.teikiJissekiHoukokuDao.GetReportDetailData_2(data);
        }

        /// <summary>
        /// 実績報告データ取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal TeikiJissekiHoukokuDto[] GetJissekiHoukoku_3(TeikiJissekiHoukokuDto data)
        {
            return this.teikiJissekiHoukokuDao.GetReportDetailData_3(data);
        }

		#endregion

    }
}
