
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using r_framework.Dto;
namespace r_framework.Setting
{
    /// <summary>
    /// プロパティに表示する選択用のポップアップ情報を生成するクラス
    /// </summary>
    public class PopupSetting
    {
        /// <summary>
        /// ポップアップの情報を格納するフィールド
        /// </summary>
        public Dictionary<string, PopupSettingDto> popupSettingList;


        private static Dictionary<string, PopupSettingDto> _popupSettingList = new Dictionary<string, PopupSettingDto>(); //キャッシュするように変更

        /// <summary>
        /// XMLよりポップアップ情報を取得する
        /// </summary>
        public PopupSetting()
        {

            lock (_popupSettingList) //マルチスレッドはしないと思うが念のためロック
            {

                if (_popupSettingList.Count == 0 ) //空だと初期化
                {
                    //_popupSettingList = new Dictionary<string, PopupSettingDto>();
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(PopupSettingDto[]));
                    var thisAssembly = Assembly.GetExecutingAssembly();

                    using (var resourceStream = thisAssembly.GetManifestResourceStream("r_framework.Setting.PopupSetting.xml"))
                    {
                        using (var resourceReader = new StreamReader(resourceStream))
                        {
                            PopupSettingDto[] loadClasses = (PopupSettingDto[])serializer.Deserialize(resourceReader);
                            foreach (var setting in loadClasses)
                            {
                                _popupSettingList.Add(setting.CheckMethodName, setting);
                            }
                        }
                    }
                }

                //キャッシュを公開する
                this.popupSettingList = _popupSettingList;
            }
        }

        /// <summary>
        /// ポップアップ対象を取得する
        /// </summary>
        public PopupSettingDto GetSetting(string SettingId)
        {
            return this.popupSettingList[SettingId];
        }

        /// <summary>
        /// 表示用にポップアップの日本語名を返却する
        /// </summary>
        public Dictionary<string, PopupSettingDto>.KeyCollection GetKeyCollection()
        {
            return this.popupSettingList.Keys;
        }
    }
}
