
using System.ComponentModel;
using r_framework.Setting;
namespace r_framework.Converter
{
    /// <summary>
    /// ポップアップ画面名をプルダウンにて表示するための
    /// コンバートクラス
    /// </summary>
    internal class PopupWindowConverter : StringConverter
    {

        private static StandardValuesCollection defaultRelations;

        public override bool GetStandardValuesSupported(
                       ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(
                       ITypeDescriptorContext context)
        {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(
                      ITypeDescriptorContext context)
        {

            PopupSetting ps = new PopupSetting();

            var kakunou = new string[ps.popupSettingList.Count];

            int i = 0;
            foreach (var pop in ps.popupSettingList)
            {
                kakunou[i] = pop.Key;
                i++;
            }

            System.Array.Sort(kakunou);//ソートしないとデザイナで見づらい！(なぜか反映されない？)

            defaultRelations =
             new StandardValuesCollection(kakunou);



            return defaultRelations;
        }
    }
}
