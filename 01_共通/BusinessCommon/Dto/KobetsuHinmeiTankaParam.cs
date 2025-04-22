using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// Dtoクラス・コントロール
    /// </summary>
    public class KobetsuHinmeiTankaParam
    {
        /// <summary>
        /// 品名コード
        /// </summary>
        public string HINMEI_CD;

        /// <summary>
        /// 単位コード
        /// </summary>
        public short UNIT_CD;

        /// <summary>
        /// 荷卸現場コード
        /// </summary>
        public string NIOROSHI_GENBA_CD;

        /// <summary>
        /// 荷卸業者コード
        /// </summary>
        public string NIOROSHI_GYOUSHA_CD;

        /// <summary>
        /// 運搬業者コード
        /// </summary>
        public string UNPAN_GYOUSHA_CD;

        /// <summary>
        /// 現場コード
        /// </summary>
        public string GENBA_CD;

        /// <summary>
        /// 業者コード
        /// </summary>
        public string GYOUSHA_CD;

        /// <summary>
        /// 取引先コード
        /// </summary>
        public string TORIHIKISAKI_CD;

        /// <summary>
        /// 伝票区分
        /// </summary>
        public short DENPYOU_KBN_CD;

        /// <summary>
        /// 伝種区分
        /// </summary>
        public short DENSHU_KBN_CD;
    }
}
