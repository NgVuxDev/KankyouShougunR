// $Id: ConstClass.cs 7927 2013-11-22 06:57:47Z sys_dev_22 $

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstClass
    {

        /// <summary>
        /// ボタン設定ファイル
        /// </summary>
        public const string BUTTON_SETTING_XML = "Shougun.Core.Reception.UketsukeKuremuNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
