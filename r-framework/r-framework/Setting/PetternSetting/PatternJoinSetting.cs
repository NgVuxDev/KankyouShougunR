
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using r_framework.Dto.PetternSettingDto;
namespace r_framework.Setting.PetternSetting
{
    public class PatternJoinSetting
    {
        private Dictionary<string, JoinSettingDto> joinSettingDtoList;

        /// <summary>
        /// XMLよりフォーマット情報を取得する
        /// </summary>
        public PatternJoinSetting()
        {

        }

        public void LoadPatternJoinSetting(Assembly assembly, string xmlPath)
        {
            this.joinSettingDtoList = new Dictionary<string, JoinSettingDto>();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(JoinSettingDto[]));

            using (var resourceStream = assembly.GetManifestResourceStream(xmlPath))
            {
                using (var resourceReader = new StreamReader(resourceStream))
                {
                    JoinSettingDto[] loadClasses = (JoinSettingDto[])serializer.Deserialize(resourceReader);
                    foreach (var setting in loadClasses)
                    {
                        this.joinSettingDtoList.Add(setting.TableName, setting);
                    }
                }
            }
        }

        /// <summary>
        /// フォーマット対象を取得する
        /// </summary>
        public JoinSettingDto GetSetting(string SettingId)
        {
            return this.joinSettingDtoList[SettingId];
        }
    }
}
