using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using r_framework.Logic;

namespace r_framework.Setting
{
    [Serializable()]
    public class SettingFile
    {
        public string SettingFileName { get; set; }

        public List<GridSetting> GridSettingList { get; set; }

        public SettingFile(string fileName)
        {
            this.SettingFileName = fileName;
            this.GridSettingList = new List<GridSetting>();
        }

        public void LoadSetting()
        {
            var m_bank = new Entity.M_BANK();
            var hoge = new DataBinderLogic<Entity.M_BANK>(m_bank);

            SettingFile settingRead = null;

            using (var fs = new FileStream(this.SettingFileName, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SettingFile));
                settingRead = (SettingFile)serializer.Deserialize(fs);
            }

            this.GridSettingList = settingRead.GridSettingList;
        }

        public void SaveSetting()
        {
            this.SaveSetting(this);
        }

        public void SaveSetting(SettingFile settingSave)
        {
            using (var fs = new FileStream(this.SettingFileName, FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(SettingFile));
                serializer.Serialize(fs, settingSave);
            }
        }

        public bool Equals(SettingFile other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
