using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムデータグリッドビューに配置するコントロールインタフェース
    /// </summary>
    public interface ICustomDataGridControl : ICustomControl
    {
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// <para>値設定先コントロール。未指定(null)の場合、ICustomControl.PopupSetFormFieldが使用される。</para>
        /// <para>PopupGetMasterFieldと同じ順序で格納。</para>
        /// </summary>
        CustomControl.ICustomDataGridControl[] ReturnControls { get; set; }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void KeyPress(KeyPressEventArgs e);

        /// <summary>
        /// フォーカスアウトイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void Leave(EventArgs e);

        /// <summary>
        /// フォーカスインイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void Enter(EventArgs e);

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void KeyDown(KeyEventArgs e);

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void PreviewKeyDown(PreviewKeyDownEventArgs e);

        /// <summary>
        /// テキスト変更イベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        void TextChanged(EventArgs e);

        /// <summary>
        /// キーアップイベント
        /// </summary>
        /// <param name="sender">押下キー情報</param>
        /// <param name="e">イベントハンドラ</param>
        void KeyUp(object sender, KeyEventArgs e);

        /// <summary>
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>PopupDataSourceを指定して、PopupDataHeaderTitleを指定しない場合、PopupDataSourceに設定されている列名が表示される</para>
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Browsable(false)]
        string[] PopupDataHeaderTitle { get; set; }

        //カラムのみでOK セルは利用しないので不要
        ///// <summary>
        ///// マスタ検索項目ポップアップに出すかどうかを設定します。Trueは表示/Falseは非表示
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("検索条件ポップアップに表示するか否かを選択してください")]
        //[DefaultValue(true)]
        //bool ViewSearchItem { get; set; }


        /// <summary>
        /// 列からEDISONプロパティをコピーする
        /// </summary>
       void CloneChiled();

    }

    public interface ICustomeDataGridViewColmun
    {
        /// <summary>
        /// マスタ検索項目ポップアップに出すかどうかを設定します。Trueは表示/Falseは非表示
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索条件ポップアップに表示するか否かを選択してください")]
        [DefaultValue(true)]
        bool ViewSearchItem { get; set; }
    }
}
