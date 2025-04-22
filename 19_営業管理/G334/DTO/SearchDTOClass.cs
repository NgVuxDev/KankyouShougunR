using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.APP;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Dto
{
    /// <summary>
    /// 取引履歴一覧検索条件クラス
    /// </summary>
    internal class SearchDTOClass
    {
        /// <summary>
        /// 検索条件  :拠点CD
        /// </summary>
        public Nullable<int> KYOTEN_CD { get; set; }

        /// <summary>
        /// 検索条件  :伝票日付(FROM)
        /// </summary>
        public Nullable<DateTime> DENPYOU_DATE_FROM { get; set; }

        /// <summary>
        /// 検索条件  :伝票日付(TO)
        /// </summary>
        public Nullable<DateTime> DENPYOU_DATE_TO { get; set; }

        /// <summary>
        /// 検索条件  :営業担当者CD
        /// </summary>
        public string EIGYOU_TANTOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 検索条件  :業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            this.KYOTEN_CD = null;
            this.DENPYOU_DATE_FROM = null;
            this.DENPYOU_DATE_TO = null;
            this.EIGYOU_TANTOUSHA_CD = null;
            this.TORIHIKISAKI_CD = null;
            this.GYOUSHA_CD = null;
            this.GENBA_CD = null;
        }

        /// <summary>
        /// 検索条件取り込み
        /// </summary>
        /// <param name="form"></param>
        public void Import(UIForm form)
        {
            // 初期化
            this.Init();

            // 伝票日付
            this.DENPYOU_DATE_FROM = (Nullable<DateTime>)form.HeaderForm.HIDUKE_FROM.Value;
            this.DENPYOU_DATE_TO = (Nullable<DateTime>)form.HeaderForm.HIDUKE_TO.Value;

            // 拠点CD
            int kyotenCd = 0;
            if (int.TryParse(form.HeaderForm.txtBox_KyotenCd.Text.Trim(), out kyotenCd))
            {
                if (kyotenCd != 99)
                {
                    this.KYOTEN_CD = kyotenCd;
                }
            }


            string tmp;

            // 営業担当者CD
            tmp = form.numTxtBox_EigyoTantosyaCD.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tmp))
            {
                this.EIGYOU_TANTOUSHA_CD = tmp;
            }

            // 取引先CD
            tmp = form.numTxtbox_TrhkskCD.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tmp))
            {
                this.TORIHIKISAKI_CD = tmp;
            }

            // 業者CD
            tmp = form.numTxtBox_GyousyaCD.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tmp))
            {
                this.GYOUSHA_CD = tmp;
            }

            // 現場CD
            tmp = form.numTxtBox_GbCD.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tmp))
            {
                this.GENBA_CD = tmp;
            }
        }
    }
}
