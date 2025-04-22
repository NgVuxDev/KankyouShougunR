using System;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済電子申請一覧DTO
    /// </summary>
    public class ShouninzumiDenshiShinseiIchiranDto
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ShouninzumiDenshiShinseiIchiranDto()
        {
            this.KyotenCd = String.Empty;
            this.KyotenName = String.Empty;
            this.ShainCd = String.Empty;
            this.ShainName = String.Empty;
            this.ShinseiDateFrom = SqlDateTime.Null;
            this.ShinseiDateTo = SqlDateTime.Null;
            this.ShinseiStatus = (int)DenshiShinseiUtility.DENSHI_SHINSEI_STATUS.APPROVAL;
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
        /// 申請日付Fromを取得・設定します
        /// </summary>
        public SqlDateTime ShinseiDateFrom { get; set; }

        /// <summary>
        /// 申請日付Toを取得・設定します
        /// </summary>
        public SqlDateTime ShinseiDateTo { get; set; }

        /// <summary>
        /// 申請ステータスを取得・設定します
        /// </summary>
        public int ShinseiStatus { get; set; }
    }
}
