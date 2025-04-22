using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Const;

namespace Shougun.Core.BusinessManagement.Const.Common
{
    /// <summary>
    /// 画面表示パラメータ
    /// </summary>
    public class FormShowParamDao
    {
        /// <summary>
        /// 見積書タイプ
        /// </summary>
        public int cyohyoType;

        /// <summary>
        /// 画面起動タイプ
        /// </summary>
        public WINDOW_TYPE windowType;

        /// <summary>
        /// システムID
        /// </summary>
        public string systemID;

        /// <summary>
        /// 連番
        /// </summary>
        public string detailSystemID;

        /// <summary>
        /// 見積番号
        /// </summary>
        public string mitsumoriNumber;

    }

}
