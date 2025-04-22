using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Drawing;

namespace ShukkinDataShutsuryoku
{
    internal class ConstClass
    {
        public const string GAMEN_ID = "G762";
        /// <summary>ボタン設定ファイル</summary>
        public static readonly string BUTTON_SETTING_XML = "ShukkinDataShutsuryoku.Setting.ButtonSetting.xml";
        /// <summary>出力状況 1.未出力</summary>
        public const string SHUTSURYOKU_JOUKYOU_MI = "1";
        /// <summary>出力状況 2.出力済</summary>
        public const string SHUTSURYOKU_JOUKYOU_ZUMI = "2";
        /// <summary>出力状況 3.全て</summary>
        public const string SHUTSURYOKU_JOUKYOU_SUBETE = "3";
    }
}
