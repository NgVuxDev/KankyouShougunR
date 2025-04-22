using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Master.EigyoTantoushaIkkatsu
{
    class ConstClass
    {
        /// <summary>ボタン設定ファイル</summary>
        public static readonly string BUTTON_SETTING_XML = "Shougun.Core.Master.EigyoTantoushaIkkatsu.Setting.ButtonSetting.xml";

        public static readonly string[] TARGET_NAMES = new string[] { "[1]業者", "[1]現場", "[1]取引先" };

        public const int TORIHIKISAKI = 0;
        public const int GYOUSHA = 1;
        public const int GENBA = 2;
        public const int TARGET_COUNT = 3;
    }
}
