using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.APP;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.DTO
{
    /// <summary>
    /// 検索条件DTO
    /// </summary>
    internal class SearchDTOClass
    {
        /// <summary>
        /// 確定条件：確定済み
        /// </summary>
        internal static readonly string FIX_CONDITION_VALUE_FIXED = "1";

        /// <summary>
        /// 確定条件：未確定
        /// </summary>
        internal static readonly string FIX_CONDITION_VALUE_UNFIXED = "2";

        /// <summary>
        /// 取引条件：現金
        /// </summary>
        internal static readonly string TORIHIKI_KBN_VALUE_GENKIN = "1";

        /// <summary>
        /// 取引条件：掛け
        /// </summary>
        internal static readonly string TORIHIKI_KBN_VALUE_KAKE = "2";

        /// <summary>
        /// 拠点CD
        /// </summary>
        public Nullable<int> KYOTEN_CD { get; set; }

        /// <summary>
        /// 期間日付(FROM)
        /// </summary>
        public Nullable<DateTime> KIKAN_DATE_FROM { get; set; }

        /// <summary>
        /// 期間日付(TO)
        /// </summary>
        public Nullable<DateTime> KIKAN_DATE_TO { get; set; }

        /// <summary>
        /// 締日
        /// </summary>
        public Nullable<int> SHIMEBI { get; set; }

        /// <summary>
        /// 確定条件
        /// </summary>
        public string FIX_CONDITION_VALUE { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 取引先CD検索
        /// </summary>
        public string TORIHIKISAKI_CD_custom { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            this.KYOTEN_CD = null;
            this.KIKAN_DATE_FROM = null;
            this.KIKAN_DATE_TO = null;
            this.SHIMEBI = null;
            this.FIX_CONDITION_VALUE = SearchDTOClass.FIX_CONDITION_VALUE_UNFIXED;
            this.TORIHIKISAKI_CD = null;
            this.TORIHIKISAKI_CD_custom = null;
        }

        /// <summary>
        /// 検索条件取り込み
        /// </summary>
        /// <param name="form"></param>
        public void SetUIForm(UIForm form)
        {
            // 初期化
            this.Init();

            int tmp;
            //string tmp2;

            // 締日
            if (int.TryParse(form.cmb_Shimebi.Text.Trim(), out tmp))
            {
                this.SHIMEBI = tmp;
            }

            // 拠点CD
            if (int.TryParse(form.txtBox_KyotenCd.Text.Trim(), out tmp))
            {
                this.KYOTEN_CD = tmp;
            }
            
            // 伝票日付
            this.KIKAN_DATE_FROM = (Nullable<DateTime>)form.dtp_KikanFrom.Value;
            this.KIKAN_DATE_TO = (Nullable<DateTime>)form.dtp_KikanTo.Value;

            // 確定条件
            if (!string.IsNullOrEmpty(form.fixConditionValue.Text))
            {
                this.FIX_CONDITION_VALUE = form.fixConditionValue.Text;
            }

            // 取引先CD
            if (!string.IsNullOrEmpty(form.TORIHIKISAKI_CD_custom.Text))
            {
                this.TORIHIKISAKI_CD_custom = form.TORIHIKISAKI_CD_custom.Text;
            }
        }
    }
}
