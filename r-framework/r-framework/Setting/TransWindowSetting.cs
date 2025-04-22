using System.IO;
using System.Text;

namespace r_framework.Setting
{
    /// <summary>
    /// ウインドウ遷移定義
    /// </summary>
    public class TransWindowSetting
    {
        /// <summary>
        /// 対象の画面名
        /// </summary>
        public string WindowName { get; set; }
        /// <summary>
        /// 対象のアセンブリー名
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 対象のフォーム名s
        /// </summary>
        public string FormName { get; set; }
        /// <summary>
        /// 遷移画面情報ロード処理
        /// </summary>
        /// <param name="xmlPath">読み込み対象のXML格納パス</param>　
        public static TransWindowSetting[] LoadTransWindowSetting(string xmlPath)
        {
            var serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(TransWindowSetting[]));

            using (var resourceStream = new FileStream(xmlPath, FileMode.Open))
            {
                TransWindowSetting[] loadClasses = (TransWindowSetting[])serializer.Deserialize(resourceStream);
                return loadClasses;
            }
        }
    }
}
