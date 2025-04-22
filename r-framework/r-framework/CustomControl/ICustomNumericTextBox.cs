using System.ComponentModel;
using r_framework.Dto;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタム数値入力テキストボックスのインタフェース
    /// </summary>
    public interface ICustomNumericTextBox
    {
        [Browsable(false)]
        string PrevText { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        bool MinusEnableFlag { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        char[] CharacterLimitList { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        RangeSettingDto RangeSetting { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        bool RangeLimitFlag { get; set; }
    }

    /// <summary>
    /// カスタム数値入力テキストボックスのインタフェース
    /// </summary>
    public interface ICustomNumericTextBox2
    {
        [Category("EDISONプロパティ_画面設定")]
        RangeSettingDto RangeSetting { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        string PrevText { get; set; }
    }
}
