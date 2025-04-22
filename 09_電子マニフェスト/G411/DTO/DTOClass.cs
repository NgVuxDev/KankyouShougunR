using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Dto
{
    /// <summary>
    /// 通知情報照会検索条件DTO
    /// </summary>
    public class TsuuchiJyouhouDTOCls
    {
        /// <summary>
        /// 検索条件 :通知日From
        /// </summary>
        public String tuuchiBiFrom { get; set; }
        /// <summary>
        /// 検索条件 :通知日To
        /// </summary>
        public String tuuchiBiTo { get; set; }
        /// <summary>
        /// 検索条件 :確認
        /// </summary>
        public String readFlag { get; set; }

    }

    /// <summary>
    /// 通知明細取得検索条件DTO
    /// </summary>
    public class MeisaiInfoDTOCls
    {
        /// <summary>
        /// 検索条件 :通知日From
        /// </summary>
        public String tuuchiBiFrom { get; set; }
        /// <summary>
        /// 検索条件 :通知日To
        /// </summary>
        public String tuuchiBiTo { get; set; }
        /// <summary>
        /// 検索条件 :確認
        /// </summary>
        public String readFlag { get; set; }

        /// <summary>
        /// 検索条件 :通知コード
        /// </summary>
        public String tuuchiCd { get; set; }

        /// <summary>
        /// 修正／取消SEQモードフラグ
        /// </summary>
        public String ApprovalFlg { get; set; }
    }

    /// <summary>
    /// 最大seq取得
    /// </summary>
    public class GetMaxSeqDtoCls
    {
        /// <summary>
        /// 検索条件 :管理番号
        /// </summary>
        public String  kanriId { get; set; }

    }
}
