using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.Common.Kakepopup.Accessor
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
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DBAccessor()
		{
			// フィールドの初期化
			this.TorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
		}
		#endregion

		#region DBアクセッサ
		/// <summary>
		/// 取引先CDに紐付けられた略名を返却する
		/// </summary>
		/// <param name="TorihikisakiCD">略名を取得したい取引先のCD</param>
		/// <returns name="string">取引先略名</returns>
		internal string GetName(string TorihikisakiCD)
		{
			// 取得したentityより略名を取得
			M_TORIHIKISAKI entity = this.TorihikisakiDao.GetDataByCd(TorihikisakiCD);
			return entity.TORIHIKISAKI_NAME_RYAKU;
		}
		#endregion
	}
}
