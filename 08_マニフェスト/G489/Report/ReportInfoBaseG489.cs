using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei.Report
{
    /// <summary>
    /// マニ帳票用帳票基底クラス
    /// </summary>
    public class ReportInfoBaseG489 : ReportInfoBase
    {

        /// <summary>システム設定 フォーマットCD</summary>
        private string ManifestSuuryoFormatCD;
        /// <summary>システム設定 フォーマット書式</summary>
        private string ManifestSuuryoFormat;

        /// <summary>システム設定 フォーマット書式</summary>
        private Shougun.Core.Common.BusinessCommon.Logic.ManifestoLogic mlogic;

        public ReportInfoBaseG489()
            : base()
        {
            mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();

            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            ManifestSuuryoFormatCD = mSysInfo.MANIFEST_SUURYO_FORMAT_CD.ToString();
            ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();
        }

        /// <summary>
        /// 数量等をフォーマットして返す
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string FormatSuuryo(string su)
        {
            if (string.IsNullOrWhiteSpace(su))
            {
                return su;
            }
            else
            {
                decimal dbl;

                if (decimal.TryParse(su, out dbl))
                {
                    return this.mlogic.GetSuuryoRound(dbl, this.ManifestSuuryoFormatCD).ToString(this.ManifestSuuryoFormat);
                }
                else
                {
                    r_framework.Utility.LogUtility.Debug("数値フォーマット対象の値が想定外です：" + su);
                    return su;
                }

            }
        }


    }
}
