
using r_framework.CustomControl;
using r_framework.MasterAccess;

namespace r_framework.Setting
{
    /// <summary>
    /// マスタのデータを取得し、コンボボックスに設定を行うクラス
    /// </summary>
    public class MasterDataSetting
    {
        /// <summary>
        /// 車種マスタより、値を取得し、コンボボックスへの設定を行う
        /// </summary>
        /// <param name="settingBox">設定を行うコンボボックス</param>
        /// <returns>設定後のコンボボックス</returns>
        public CustomComboBox SettingShashuMasterData(CustomComboBox settingBox)
        {
            var shashuAccess = new ShashuMasterAccess();

            var entityList = shashuAccess.GetMasterData();

            foreach (var entity in entityList)
            {
                settingBox.ValueMember += "," + entity.SHASHU_CD;
                settingBox.Items.Add(entity.SHASHU_NAME);
            }

            return settingBox;
        }

        /// <summary>
        /// 配車状況マスタより、値を取得し、コンボボックスへの設定を行う
        /// </summary>
        /// <param name="settingBox">設定を行うコンボボックス</param>
        /// <returns>設定後のコンボボックス</returns>
        public CustomComboBox SettingHaishaJokyoMasterData(CustomComboBox settingBox)
        {
            var haishaJokyoAccess = new HaishaJokyoMasterAccess();

            var entityList = haishaJokyoAccess.GetMasterData();

            settingBox.Items.Add("");

            foreach (var entity in entityList)
            {
                settingBox.ValueMember += "," + entity.HAISHA_JOKYO_CD;
                settingBox.Items.Add(entity.HAISHA_JOKYO_NAME);
            }

            return settingBox;
        }
    }
}
