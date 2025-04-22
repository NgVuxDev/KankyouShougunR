using System.IO;
using System.Reflection;

namespace r_framework.Setting
{
    public class ButtonSetting
    {
        /// <summary>
        /// 対象のボタン名
        /// </summary>
        public string ButtonName { get; set; }
        /// <summary>
        /// デフォルトにて使用されるヒントテキスト
        /// 各画面タイプのヒントテキストが存在しない場合は
        /// このヒントテキストが設定される
        /// </summary>
        public string DefaultHintText { get; set; }
        /// <summary>
        /// 新規画面タイプ時の表示名
        /// </summary>
        public string NewButtonName { get; set; }
        /// <summary>
        /// 新規画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string NewButtonHintText { get; set; }
        /// <summary>
        /// 参照画面タイプ時の表示名
        /// </summary>
        public string ReferButtonName { get; set; }
        /// <summary>
        /// 参照画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string ReferButtonHintText { get; set; }
        /// <summary>
        /// 更新画面タイプ時の表示名
        /// </summary>
        public string UpdateButtonName { get; set; }
        /// <summary>
        /// 更新画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string UpdateButtonHintText { get; set; }
        /// <summary>
        /// 削除画面タイプ時の表示名
        /// </summary>
        public string DeleteButtonName { get; set; }
        /// <summary>
        /// 削除画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string DeleteButtonHintText { get; set; }
        /// <summary>
        /// 一覧画面タイプ時の表示名
        /// </summary>
        public string IchiranButtonName { get; set; }
        /// <summary>
        /// 一覧画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string IchiranButtonHintText { get; set; }
        /// <summary>
        /// ポップアップ画面タイプ時の表示名
        /// </summary>
        public string PopupButtonName { get; set; }
        /// <summary>
        /// ポップアップ画面タイプ時の表示ヒントテキスト
        /// </summary>
        public string PopupButtonHintText { get; set; }
        /// <summary>
        /// ボタン名称情報ロード処理
        /// </summary>
        /// <param name="assembly">設定ファイルを読み込むアセンブリ</param>
        /// <param name="xmlPath">読み込み対象のXML格納パス</param>　
        public ButtonSetting[] LoadButtonSetting(Assembly assembly, string xmlPath)
        {
            //var serializer =
            //    new System.Xml.Serialization.XmlSerializer(typeof(ButtonSetting[]));

            using (var resourceStream = assembly.GetManifestResourceStream(xmlPath))
            {
                using (var resourceReader = new StreamReader(resourceStream))
                {
                    ButtonSetting[] loadClasses = (ButtonSetting[])_s.Deserialize(resourceReader);
                    return loadClasses;
                }
            }
        }

        //シリアライザをキャッシュ
        static private System.Xml.Serialization.XmlSerializer _s = new System.Xml.Serialization.XmlSerializer(typeof(ButtonSetting[]));

    }
}
