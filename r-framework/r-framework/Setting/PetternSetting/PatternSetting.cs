using System.Collections.Generic;
using System.IO;
using System.Reflection;
using r_framework.Dto.PetternSettingDto;

namespace r_framework.Setting.PetternSetting
{
    public class PatternSetting
    {
        private Dictionary<string, PatternSettingDto> patternSettingDto;

        /// <summary>
        /// XMLよりフォーマット情報を取得する
        /// </summary>
        public PatternSetting()
        {
        }

        public void LoadPatternSetting(Assembly assembly, string xmlPath)
        {
            this.patternSettingDto = new Dictionary<string, PatternSettingDto>();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(PatternSettingDto[]));

            using (var resourceStream = assembly.GetManifestResourceStream(xmlPath))
            {
                using (var resourceReader = new StreamReader(resourceStream))
                {
                    PatternSettingDto[] loadClasses = (PatternSettingDto[])serializer.Deserialize(resourceReader);
                    foreach (var setting in loadClasses)
                    {
                        this.patternSettingDto.Add(setting.DisplayColumnName, setting);
                    }
                }
            }
        }

        /// <summary>
        /// フォーマット対象を取得する
        /// </summary>
        public PatternSettingDto GetSetting(string columnName)
        {
            return patternSettingDto[columnName];
        }
    }
}
