using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using r_framework.Dto;

namespace r_framework.Setting
{
    /// <summary>
    /// プロパティに表示する選択用のフォーマット情報を生成するクラス
    /// </summary>
    public class FormatSetting
    {
        /// <summary>
        /// フォーマットの情報を格納するフィールド
        /// </summary>
        public Dictionary<string, FormatSettingDto> formatSettingList;

        private static Dictionary<string, FormatSettingDto> _formatSettingList = new Dictionary<string, FormatSettingDto>(); //キャッシュ

        /// <summary>
        /// XMLよりフォーマット情報を取得する
        /// </summary>
        public FormatSetting()
        {
            //lock (_formatSettingList)
            {
                if (_formatSettingList.Count == 0)
                {
                    //this.formatSettingList = new Dictionary<string, FormatSettingDto>();
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(FormatSettingDto[]));
                    var thisAssembly = Assembly.GetExecutingAssembly();

                    using (var resourceStream = thisAssembly.GetManifestResourceStream("r_framework.Setting.FormatSetting.xml"))
                    {
                        using (var resourceReader = new StreamReader(resourceStream))
                        {
                            FormatSettingDto[] loadClasses = (FormatSettingDto[])serializer.Deserialize(resourceReader);
                            lock (_formatSettingList)
                            {
                                if (_formatSettingList.Count == 0)
                                {
                                    foreach (var setting in loadClasses)
                                    {
                                        _formatSettingList.Add(setting.FormatName, setting);
                                    }
                                }
                            }
                        }
                    }
                }
                this.formatSettingList = _formatSettingList;
            }
        }

        /// <summary>
        /// フォーマット対象を取得する
        /// </summary>
        public FormatSettingDto GetSetting(string name)
        {
            return this.formatSettingList[name];
        }

        public FormatSettingDto[] GetSettings(FORMAT_TYPE type)
        {
            return this.formatSettingList.Values.Where(x => x.FormatType == type).ToArray();
        }

        /// <summary>
        /// 表示用にフォーマットの日本語名を返却する
        /// </summary>
        public Dictionary<string, FormatSettingDto>.KeyCollection GetKeyCollection()
        {
            return this.formatSettingList.Keys;
        }
    }
}