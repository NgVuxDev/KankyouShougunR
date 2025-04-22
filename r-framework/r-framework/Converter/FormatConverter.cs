
using System.ComponentModel;
using r_framework.Setting;
namespace r_framework.Converter
{
    /// <summary>
    /// フォーマット名称をプルダウンにて表示するための
    /// コンバートクラス
    /// </summary>
    internal class FormatConverter : StringConverter
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
            FormatSetting fs = new FormatSetting();

            var kakunou = new string[fs.formatSettingList.Count];

            int i = 0;
            foreach (var format in fs.formatSettingList)
            {
                kakunou[i] = format.Key;
                i++;
            }

            defaultRelations = new StandardValuesCollection(kakunou);
            return defaultRelations;
        }
    }
}
