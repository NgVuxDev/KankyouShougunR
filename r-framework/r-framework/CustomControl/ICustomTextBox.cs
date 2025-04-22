using System.ComponentModel;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムテキストボックスコントロールのインタフェース
    /// </summary>
    public interface ICustomTextBox
    {
        [Category("EDISONプロパティ")]
        bool ZeroPaddengFlag { get; set; }

        [Category("EDISONプロパティ")]
        string FuriganaAutoSetControl { get; set; }

        [Category("EDISONプロパティ")]
        string CopyAutoSetControl { get; set; }

        [Category("EDISONプロパティ")]
        string FormatSetting { get; set; }

        [Category("EDISONプロパティ")]
        string CustomFormatSetting { get; set; }

        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>PopupDataSourceを指定して、PopupDataHeaderTitleを指定しない場合、PopupDataSourceに設定されている列名が表示される</para>
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Browsable(false)]
        string[] PopupDataHeaderTitle { get; set; }

        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        bool IsInputErrorOccured { get; set; }
    }
}
