// $Id: UIConstans.cs 23324 2014-06-17 13:10:46Z nagata $

namespace Shougun.Core.SalesManagement.ShiharaiMotocho.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class UIConstans
    {
		/// <summary>
		/// 税計算区分
		/// </summary>
		internal static readonly string ZEI_KEISAN_KBN_DENPYOU = "1";	// 伝票毎
		internal static readonly string ZEI_KEISAN_KBN_SEIKYUU = "2";	// 請求毎
		internal static readonly string ZEI_KEISAN_KBN_MEISAI = "3";	// 明細毎

		/// <summary>
		/// 税区分
		/// </summary>
		internal static readonly string ZEI_KBN_SOTO = "1";		// 外税
		internal static readonly string ZEI_KBN_UCHI = "2";		// 内税
		internal static readonly string ZEI_KBN_EXEMPTION = "3";	// 非課税

        /// <summary>
        /// 最大桁数
        /// </summary>
        internal static int MAX_LENGTH = 32767;

		/// <summary>消費税端数CD</summary>
		public enum TAX_HASUU_CD : short
		{
			CEILING = 1,    // 切り上げ
			FLOOR,          // 切り捨て
			ROUND,          // 四捨五入
		}

		/// <summary>
		/// Button設定用XMLファイルパス
		/// </summary>
		internal static readonly string ButtonInfoXmlPath = "Shougun.Core.SalesManagement.ShiharaiMotocho.Setting.ButtonSetting.xml";
		/// <summary>
		/// Report設定用XMLファイルパス
		/// </summary>
		internal static readonly string OutputFormFullPathName = "./Template/R416-Form.xml";
    }
}
