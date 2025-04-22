
using System.ComponentModel;
using r_framework.Setting;
namespace r_framework.Converter
{
    /// <summary>
    /// チェックメソッド名をプルダウンにて表示するための
    /// コンバートクラス
    /// </summary>
    internal class RunCheckMethodConverter : CollectionConverter
    {
        private static StandardValuesCollection RunCheckDefaultRelations;

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

            RunCheckMethodSetting cms = new RunCheckMethodSetting();

            RunCheckDefaultRelations = new StandardValuesCollection(cms.GetKeyCollection());
            return RunCheckDefaultRelations;
        }
    }
}
