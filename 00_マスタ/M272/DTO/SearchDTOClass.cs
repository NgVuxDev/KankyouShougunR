using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Shougun.Core.Master.RiyouRirekiKanri.APP;

namespace Shougun.Core.Master.RiyouRirekiKanri.Dto
{
    /// <summary>
    /// 利用履歴管理検索条件クラス
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
        public  string GENBA_CD { get; set; }

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
                this.KYOTEN_CD = kyotenCd;
            }
        }
    }
}
