using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Function.ShougunCSCommon.Dto
{
    /// <summary>
    /// 将軍の共通で使用するデータを保持するクラス
    /// TODO: RibbonMainMenuのCommonInformationに設定されるようになっているので
    /// そちらに乗せかえること
    /// </summary>
    public static class CommonShogunData
    {
        public static M_SYS_INFO SYS_INFO { get; private set; }

        public static M_CORP_INFO CORP_INFO { get; private set; }

        public static M_SHAIN LOGIN_USER_INFO { get; private set; }

        public static M_KYOTEN KYOTEN_INFO { get; private set; }

        /// <summary>
        /// データを取得
        /// 実行すべき箇所は以下になります。
        /// ユーザログイン時、保持しているテーブル情報が更新された時
        /// </summary>
        /// <param name="shainCd">ログインユーザの社員CD</param>
        public static void Create(string shainCd)
        {
            CommonShogunData.SYS_INFO = new M_SYS_INFO();
            CommonShogunData.CORP_INFO = new M_CORP_INFO();
            CommonShogunData.LOGIN_USER_INFO = new M_SHAIN();
            CommonShogunData.KYOTEN_INFO = new M_KYOTEN();

            var sysInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SYS_INFODao>();
            var corpInfoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_CORP_INFODao>();
            var shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();
            var kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
    
            var sysInfos = sysInfoDao.GetAllData();
            var corpInfos = corpInfoDao.GetAllData();
            var loginUserInfo = shainDao.GetDataByCd(shainCd);
            var kyotenInfo = kyotenDao.GetDataByCd("0");

            if (sysInfos != null && 0 < sysInfos.Length)
            {
                CommonShogunData.SYS_INFO = sysInfos[0];
            }

            if (corpInfos != null && 0 < corpInfos.Length)
            {
                CommonShogunData.CORP_INFO = corpInfos[0];
            }

            CommonShogunData.LOGIN_USER_INFO = loginUserInfo;

            CommonShogunData.KYOTEN_INFO = kyotenInfo;

        }
    }
}
