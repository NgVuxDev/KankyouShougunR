using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using r_framework.Dto;

namespace r_framework.Setting
{
    public class RunCheckMethodSetting
    {
        /// <summary>
        /// メソッド情報の格納クラス
        /// </summary>
        private Dictionary<string, CheckMethodSettingDto> methodSettingList;
        private static Dictionary<string, CheckMethodSettingDto> _methodSettingList = new Dictionary<string, CheckMethodSettingDto>();//キャッシュ

        /// <summary>
        /// Keyからメソッド情報を取得する
        /// </summary>
        public CheckMethodSettingDto this[string settingId]
        {
            get { return this.methodSettingList[settingId]; }
        }

        /// <summary>
        /// プロパティ表示用のデータを生成する
        /// </summary>
        public IEnumerator<KeyValuePair<string, CheckMethodSettingDto>> GetEnumerator()
        {
            foreach (var dto in this.methodSettingList)
            {
                yield return dto;
            }
        }
        /// <summary>
        /// XMLファイルより情報を取得して返却する
        /// </summary>
        public RunCheckMethodSetting()
        {
            lock (_methodSettingList)
            {
                if (_methodSettingList.Count == 0)
                {

                    //this.methodSettingList = new Dictionary<string, CheckMethodSettingDto>();

                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CheckMethodSettingDto[]));
                    var thisAssembly = Assembly.GetExecutingAssembly();

                    using (var resourceStream = thisAssembly.GetManifestResourceStream("r_framework.Setting.RunCheckMethodSetting.xml"))
                    {
                        using (var resourceReader = new StreamReader(resourceStream))
                        {
                            CheckMethodSettingDto[] loadClasses = (CheckMethodSettingDto[])serializer.Deserialize(resourceReader);

                            _methodSettingList = loadClasses.ToDictionary(s => s.CheckMethodName);
                        }
                    }
                }

                this.methodSettingList = _methodSettingList;
            }

        }

        /// <summary>
        /// チェック対象を取得する
        /// </summary>
        public CheckMethodSettingDto GetSetting(string SettingId)
        {
            return this.methodSettingList[SettingId];
        }

        /// <summary>
        /// 選択用にkey項目の一覧を返却する
        /// </summary>
        public Dictionary<string, CheckMethodSettingDto>.KeyCollection GetKeyCollection()
        {
            return this.methodSettingList.Keys;
        }
    }
}
