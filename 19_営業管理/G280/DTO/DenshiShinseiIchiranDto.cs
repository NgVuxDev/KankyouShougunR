using System;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Utility;
namespace Shougun.Core.BusinessManagement.DenshiShinseiIchiran
{
    /// <summary>
    /// G280 申請一覧DTO
    /// </summary>
    public class DenshiShinseiIchiranDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DenshiShinseiIchiranDto()
        {
            this.KyotenCd = String.Empty;
            this.KyotenName = String.Empty;
            this.ShainCd = String.Empty;
            this.ShainName = String.Empty;
            this.ShinseiKbn1Cd = 1;
            this.ShinseiKbn2Cd = 1;
            this.ShinseiKbn3Cd = 0;
            this.ShinseiDateFrom = SqlDateTime.Null;
            this.ShinseiDateTo = SqlDateTime.Null;
        }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public string KyotenCd { get; set; }

        /// <summary>
        /// 拠点名を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 社員CDを取得・設定します
        /// </summary>
        public string ShainCd { get; set; }

        /// <summary>
        /// 社員名を取得・設定します
        /// </summary>
        public string ShainName { get; set; }

        /// <summary>
        /// 申請区分１を取得・設定します
        /// </summary>
        public int ShinseiKbn1Cd { get; set; }

        /// <summary>
        /// 申請区分２を取得・設定します
        /// </summary>
        public int ShinseiKbn2Cd { get; set; }

        /// <summary>
        /// 申請区分３を取得・設定します
        /// </summary>
        public int ShinseiKbn3Cd { get; set; }

        /// <summary>
        /// 申請ステータスを取得します
        /// </summary>
        public int ShinseiStatus
        {
            get
            {
                var ret = DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING;
                switch (this.ShinseiKbn3Cd)
                {
                    case 1:
                        ret = DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPLYING;
                        break;
                    case 2:
                        ret = DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL;
                        break;
                    case 3:
                        ret = DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.DENIAL;
                        break;
                    case 4:
                        ret = DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.COMPLETE;
                        break;
                    default:
                        break;
                }
                return (int)ret;
            }
        }

        /// <summary>
        /// 申請日付Fromを取得・設定します
        /// </summary>
        public SqlDateTime ShinseiDateFrom { get; set; }

        /// <summary>
        /// 申請日付Toを取得・設定します
        /// </summary>
        public SqlDateTime ShinseiDateTo { get; set; }
    }
}
