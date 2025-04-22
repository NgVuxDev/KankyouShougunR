using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using r_framework.Const;
using r_framework.Dto;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムコントロールのインタフェース
    /// </summary>
    public interface ICustomControl
    {
        [Category("EDISONプロパティ")]
        string DBFieldsName { get; set; }

        [Category("EDISONプロパティ")]
        string ItemDefinedTypes { get; set; }

        [Category("EDISONプロパティ")]
        string DisplayItemName { get; set; }

        [Category("EDISONプロパティ")]
        string ShortItemName { get; set; }

        [Category("EDISONプロパティ")]
        int SearchDisplayFlag { get; set; }

        [Category("EDISONプロパティ")]
        Collection<SelectCheckDto> RegistCheckMethod { get; set; }

        [Category("EDISONプロパティ")]
        Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }

        [Category("EDISONプロパティ")]
        string PopupWindowName { get; set; }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        string ErrorMessage { get; set; }

        [Category("EDISONプロパティ")]
        string GetCodeMasterField { get; set; }

        [Category("EDISONプロパティ")]
        string SetFormField { get; set; }

        [Category("EDISONプロパティ")]
        WINDOW_ID PopupWindowId { get; set; }

        [Category("EDISONプロパティ")]
        string[] PopupSendParams { get; set; }

        [Category("EDISONプロパティ")]
        Color DefaultBackColor { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        Collection<JoinMethodDto> popupWindowSetting { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        string PopupSetFormField { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        string PopupGetMasterField { get; set; }

        [Category("EDISONプロパティ")]
        string ClearFormField { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        string PopupClearFormField { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        string PopupAfterExecuteMethod { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        string PopupBeforeExecuteMethod { get; set; }

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        Action<ICustomControl> PopupBeforeExecute {get;set;}

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        bool PopupMultiSelect { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        DataTable PopupDataSource { get; set; }

        Decimal CharactersNumber { get; set; }

        /// <summary>
        /// 結果取得処理処
        /// </summary>
        /// <returns>結果</returns>
        string GetResultText();

        /// <summary>
        /// 値の設定処理
        /// </summary>
        /// <param name="value">設定する値</param>
        void SetResultText(string value);

        ///// <summary>
        ///// ヒントテキスト作成処理
        ///// </summary>
        //void CreateHintText();

        /// <summary>
        /// 要素名取得処理
        /// </summary>
        /// <returns>要素名</returns>
        string GetName();

        /// <summary>
        /// ポップアップを読み取り専用でも出すかどうかを設定します
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("読み取り専用でもポップアップを起動する場合はTrueにしてください")]
        [DefaultValue(false)]
        bool ReadOnlyPopUp { get; set; }

        /// <summary>
        /// ポップアップのタイトルを強制的に上書きする場合は設定してください
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップのタイトルを強制的に上書きする場合は設定してください")]
        string PopupTitleLabel { get; set; }


 

    }

    /// <summary>
    /// 色変更に対応する場合実装してください
    /// </summary>
    public interface ICustomAutoChangeBackColor
    {
        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        [Browsable(false)]
        bool IsInputErrorOccured { get; set; }

        /// <summary>
        /// 自動背景色変更モード
        /// </summary>
        [Category("EDISONプロパティ")]
        [Description("エラーやフォーカス時の色を独自に設定する場合はfalseに変更してください")]
        [DefaultValue(true)]
        bool AutoChangeBackColorEnabled { get; set; }


        //継承元のコントロールで持ってるはず
        bool Enabled { get; set; }
        bool ReadOnly { get; set; }
        bool Focused { get; }
    }



}
