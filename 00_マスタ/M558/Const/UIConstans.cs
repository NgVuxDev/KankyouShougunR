// $Id: UIConstans.cs 27854 2014-08-18 10:00:06Z j-kikuchi $
using r_framework.Entity;

namespace Shougun.Core.Master.DenshiShinseiRoute.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
        /// <summary>
        /// Button設定用XMLファイルパス
        /// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.Master.DenshiShinseiRoute.Setting.ButtonSetting.xml";

        /// <summary>
        /// 更新用情報構造体
        /// </summary>
        internal struct ST_DATA_LIST
        {
            public bool insertFlag;     // TRUE:新規登録を行う, FALSE:行わない
            public bool existFlag;      // TRUE:DBに存在する(＝論理削除を行う), FALSE:存在しない(＝行の削除を行う)
            public M_DENSHI_SHINSEI_ROUTE entity;
        }
	}
}
