// $Id: ConstClass.cs 24687 2014-07-04 04:30:57Z j-kikuchi $

namespace Shougun.Core.Common.MobileTsuushin
{
    class ConstClass
    {
        /// <summary>モバイル設定ファイル</summary>
        public static readonly string MOBILE_SETTING_INI = "mobileXMLPath.ini";

        /// <summary>マスタファイル出力先</summary>
        public static readonly string MOBILE_OUTPUT_MASTER_PATH = "mobileOutPutMasterPath";
        
        /// <summary>配車ファイル出力先</summary>
        public static readonly string MOBILE_OUTPUT_TRANS_PATH = "mobileOutPutTransPath";
        
        /// <summary>実績データ取込先</summary>
        public static readonly string MOBILE_INPUT_PATH = "mobileInPutPath";
        
        /// <summary>バックアップ保存先</summary>
        public static readonly string MOBILE_BACKUP_PATH = "mobileBackUpPath";

        /// <summary>登録を行うパスの一覧</summary>
        public static readonly string[] REGIST_PATH_TABLE = new string[] {
                                                                    MOBILE_OUTPUT_MASTER_PATH,
                                                                    MOBILE_OUTPUT_TRANS_PATH,
                                                                    MOBILE_INPUT_PATH,
                                                                    MOBILE_BACKUP_PATH
                                                                };
    }
}
