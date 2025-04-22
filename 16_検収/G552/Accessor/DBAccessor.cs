using r_framework.Dao;
using r_framework.Utility;
using r_framework.Entity;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Accessor
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
		/// 取引先のDao
		/// </summary>
		private IM_TORIHIKISAKIDao TorihikisakiDao;
        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao GyoushaDao;
        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao GenbaDao;
        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao HinmeiDao;
        /// <summary>
        /// 拠点のDao
        /// </summary>
        private IM_KYOTENDao KyotenDao;
        #endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBAccessor()
		{
			// フィールドの初期化
			this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.GyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.GenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.KyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
        }
		#endregion

		#region DBアクセッサ
		/// <summary>
		/// 取引先CDに紐付けられた略名を返却する
		/// </summary>
		/// <param name="TorihikisakiCD">略名を取得したい取引先のCD</param>
		/// <returns name="string">取引先略名</returns>
		internal string GetNameT(string TorihikisakiCD)
		{
			// 取得したentityより略名を取得
			M_TORIHIKISAKI entity = this.TorihikisakiDao.GetDataByCd(TorihikisakiCD);
			return entity.TORIHIKISAKI_NAME_RYAKU;
		}
        /// <summary>
        /// 業者CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="GyoushaCD"></param>
        /// <returns></returns>
        internal string GetNameGY(string GyoushaCD)
        {
            // 取得したentityより略名を取得
            M_GYOUSHA entity = this.GyoushaDao.GetDataByCd(GyoushaCD);
            return entity.GYOUSHA_NAME_RYAKU;
        }
        /// <summary>
        /// 業者/現場CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal string GetNameGE(M_GENBA data)
        {
            // 取得したentityより略名を取得
            M_GENBA entity = this.GenbaDao.GetDataByCd(data);
            return entity.GENBA_NAME_RYAKU;
        }
        /// <summary>
        /// 品名CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="HinmeiCD"></param>
        /// <returns></returns>
        internal string GetNameH(string HinmeiCD)
        {
            // 取得したentityより略名を取得
            M_HINMEI entity = this.HinmeiDao.GetDataByCd(HinmeiCD);
            return entity.HINMEI_NAME_RYAKU;
        }
        /// <summary>
        ///  拠点CDに紐付けられた略名を返却する
        /// </summary>
        /// <param name="KyotenCD"></param>
        /// <returns></returns>
        internal string GetNameK(string KyotenCD)
        {
            // 取得したentityより略名を取得
            M_KYOTEN entity = this.KyotenDao.GetDataByCd(KyotenCD);
            return entity.KYOTEN_NAME_RYAKU;
        }

		#endregion
	}
}
