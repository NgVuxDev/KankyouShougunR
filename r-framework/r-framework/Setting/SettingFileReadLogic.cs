
using r_framework.Entity;

namespace r_framework.Setting
{
    /// <summary>
    /// 設定ファイル読み込みクラス
    /// </summary>
    public static class SettingFileReadLogic
    {

        public static string SettingFilePath { get; set; }

        /// <summary>
        /// ログイン情報のファイル読み込み(仮)
        /// </summary>
        public static M_LOGIN_USER_INFO LoadUserSettingFile()
        {
            //XmlSerializerオブジェクトを作成
            var serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(M_LOGIN_USER_INFO));

            M_LOGIN_USER_INFO userInfo;

            //読み込むファイルを開く
            using (var fs = new System.IO.FileStream(SettingFilePath, System.IO.FileMode.Open))
            {
                //XMLファイルから読み込み、逆シリアル化する
                userInfo = (M_LOGIN_USER_INFO)serializer.Deserialize(fs);
                //ファイルを閉じる
            }

            return userInfo;
        }

        /// <summary>
        /// メニュー表示情報の読込
        /// </summary>
        public static void LoadMenuSettingFile()
        {

        }

        /// <summary>
        /// アプリケーション設定の読込
        /// </summary>
        public static void LoadApplicatinSettingFile()
        {

        }

        public static void LoadMessage()
        {
        }
    }
}
