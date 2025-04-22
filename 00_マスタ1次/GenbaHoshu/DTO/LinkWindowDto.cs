// $Id: LinkWindowDto.cs 968 2013-09-01 04:18:26Z gai $
using System.IO;
using System.Reflection;

namespace GenbaHoshu.Dto
{
    public class LinkWindowDto
    {
        /// <summary>
        /// 対象の画面名
        /// </summary>
        public string LinkWindowName { get; set; }
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
        /// <param name="assembly">設定ファイルを読み込むアセンブリ</param>
        /// <param name="xmlPath">読み込み対象のXML格納パス</param>　
        public static LinkWindowDto[] LoadLinkWindowSetting(Assembly assembly, string xmlPath)
        {
            var serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(LinkWindowDto[]));

            using (var resourceStream = assembly.GetManifestResourceStream(xmlPath))
            {
                using (var resourceReader = new StreamReader(resourceStream))
                {
                    LinkWindowDto[] loadClasses = (LinkWindowDto[])serializer.Deserialize(resourceReader);
                    return loadClasses;
                }
            }
        }
    }
}
