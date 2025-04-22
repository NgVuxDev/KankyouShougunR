
using System.ComponentModel;
using r_framework.Setting;
namespace r_framework.Converter
{
    /// <summary>
    /// チェックメソッド名をプルダウンにて表示するための
    /// コンバートクラス
    /// </summary>
    internal class CheckMethodConverter : CollectionConverter
    {
        private static StandardValuesCollection defaultRelations;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {

            CheckMethodSetting cms = new CheckMethodSetting();

            defaultRelations = new StandardValuesCollection(cms.GetKeyCollection());
            return defaultRelations;
        }
    }
}
