
using System.IO;
using System.Reflection;

namespace r_framework.Setting
{
    /// <summary>
    /// マッピング情報をXMLから取得するクラス
    /// </summary>
    public class MappingFileSetting
    {
        public string TableName { get; set; }

        public string LogicalName { get; set; }

        public string PhysicalName { get; set; }

        public MappingFileSetting[] LoadMappingFileSetting(Assembly assembly, string xmlPath)
        {
            var serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(MappingFileSetting[]));

            using (var resourceStream = assembly.GetManifestResourceStream(xmlPath))
            {
                using (var resourceReader = new StreamReader(resourceStream))
                {
                    MappingFileSetting[] loadClasses = (MappingFileSetting[])serializer.Deserialize(resourceReader);
                    return loadClasses;
                }
            }
        }
    }
}
