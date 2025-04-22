using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Dto;
using r_framework.Dao;
using r_framework.Entity;

namespace r_framework.Utility
{
    /// <summary>
    /// 自社情報マスタ用のUtility
    /// </summary>
    public class CorpInfoUtility
    {

        private M_CORP_INFO corpInfo { get; set; }
        private M_CORP_INFO[] corpInfoList { get; set; }

        /// <summary>
        /// DAO
        /// </summary>
        private IM_CORP_INFODao dao;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="corpInfo"></param>
        public CorpInfoUtility(M_CORP_INFO corpInfo)
        {
            this.corpInfo = corpInfo;
        }

        public CorpInfoUtility()
        {
            this.dao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();

            try
            {
                M_CORP_INFO corpEntity = new M_CORP_INFO();
                corpEntity.SYS_ID = 0;
                this.corpInfoList = this.dao.GetAllValidData(corpEntity);
                this.corpInfo = this.corpInfoList[0];
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 現在の年度を取得
        /// 引数のみの値で計算する
        /// 自オブジェクトのM_CORP_INFO.KISHU_MONTHを使用するので注意してください
        /// </summary>
        /// <param name="denpyouHiduke">年度を算出したい対象の日付</param>
        /// <returns></returns>
        public int GetCurrentYear(DateTime denpyouHiduke)
        {
            int returnInt = -1;

            if (denpyouHiduke == null)
            {
                return returnInt;
            }

            // 伝票日付 < 伝票日付のYYYY + 期首月
            // だったら伝票日付のYYYY - 1
            // 一致しなかったら伝票日付のYYYY
            DateTime compareDate = new DateTime(denpyouHiduke.Year, (short)corpInfo.KISHU_MONTH, 1);
            switch (denpyouHiduke.CompareTo(compareDate))
            {
                case -1:
                    returnInt = denpyouHiduke.Year - 1;
                    break;

                default:
                    returnInt = denpyouHiduke.Year;
                    break;

            }

            return returnInt;
        }

        /// <summary>
        /// 現在の年度を取得
        /// 引数のみの値で計算する
        /// </summary>
        /// <param name="denpyouHiduke">年度を算出したい対象の日付</param>
        /// <param name="kishuMonth">自社情報マスタ.期首月</param>
        /// <returns></returns>
        public static int GetCurrentYear(DateTime denpyouHiduke, short kishuMonth)
        {
            int returnInt = -1;

            if (denpyouHiduke == null
                || !(0 < kishuMonth && kishuMonth < 13))
            {
                return returnInt;
            }

            // 伝票日付 < 伝票日付のYYYY + 期首月
            // だったら伝票日付のYYYY - 1
            // 一致しなかったら伝票日付のYYYY
            DateTime compareDate = new DateTime(denpyouHiduke.Year, kishuMonth, 1);
            switch (denpyouHiduke.CompareTo(compareDate))
            {
                case -1:
                    returnInt = denpyouHiduke.Year - 1;
                    break;

                default:
                    returnInt = denpyouHiduke.Year;
                    break;

            }

            return returnInt;
        }
    }
}
