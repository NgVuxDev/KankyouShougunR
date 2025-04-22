using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    /// <summary>
    /// データグリッドビュー用カスタムテキストボックスセル
    /// </summary>
    public partial class DgvCustomTextBoxCell : DataGridViewTextBoxCell, ICustomDataGridControl, ICustomTextBox ,ICustomAutoChangeBackColor
    {
        #region ICustomDataGridControl プロパティ
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// <para>値設定先コントロール。未指定(null)の場合、ICustomControl.PopupSetFormFieldが使用される。</para>
        /// <para>PopupGetMasterFieldと同じ順序で格納。</para>
        /// </summary>
        public CustomControl.ICustomDataGridControl[] ReturnControls { get; set; }

        private bool isInputErrorOccured = false;

        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        public bool IsInputErrorOccured
        {
            get
            {
                return isInputErrorOccured;
            }
            set
            {
                isInputErrorOccured = value;
                this.UpdateBackColor();
            }
        }

        #endregion

        private bool autoChangeBackColorEnabled = true;

        /// <summary>
        /// 自動背景色変更モード
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("エラーやフォーカス時の色を独自に設定する場合はfalseに変更してください")]
        [DefaultValue(true)]
        public bool AutoChangeBackColorEnabled
        {
            get
            {
                return autoChangeBackColorEnabled;
            }
            set
            {
                autoChangeBackColorEnabled = value;
                this.UpdateBackColor();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomTextBoxCell()
        {
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }
        }

        /// <summary>
        /// 対象のセルの同一コピーを作成します。
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomTextBoxCell myTextBoxCell = base.Clone() as DgvCustomTextBoxCell;
            myTextBoxCell.IsInputErrorOccured = this.IsInputErrorOccured;
            myTextBoxCell.AutoChangeBackColorEnabled = this.AutoChangeBackColorEnabled;

            return myTextBoxCell;
        }

        /// <summary>
        /// カラムから情報をコピー
        /// </summary>
        public virtual void CloneChiled()
        {
            var column = this.OwningColumn as DgvCustomTextBoxColumn;

            if (column != null)
            {
                this.DBFieldsName = column.DBFieldsName;
                this.ItemDefinedTypes = column.ItemDefinedTypes;
                this.DisplayItemName = column.DisplayItemName;
                this.ShortItemName = column.ShortItemName;
                this.RegistCheckMethod = column.RegistCheckMethod;
                this.FocusOutCheckMethod = column.FocusOutCheckMethod;
                this.PopupWindowName = column.PopupWindowName;
                this.GetCodeMasterField = column.GetCodeMasterField;

                this.PopupGetMasterField = this.GetCodeMasterField; //グリッドの場合は この設定ができない（ポップアップがずっと動かなかったはず。みなどうしてた？）

                this.SetFormField = column.SetFormField;
                this.ErrorMessage = column.ErrorMessage;
                this.DefaultBackColor = column.DefaultBackColor;
                this.CharactersNumber = column.CharactersNumber;
                this.ZeroPaddengFlag = column.ZeroPaddengFlag;
                this.PopupWindowId = column.PopupWindowId;
                this.PopupMultiSelect = column.PopupMultiSelect;
                this.PopupDataSource = column.PopupDataSource;
                this.PopupSendParams = column.PopupSendParams;
                this.popupWindowSetting = column.popupWindowSetting;
                this.CopyAutoSetControl = column.CopyAutoSetControl;
                this.CopyAutoSetWithSpace = column.CopyAutoSetWithSpace;
                this.FormatSetting = column.FormatSetting;
                this.CustomFormatSetting = column.CustomFormatSetting;
                this.FuriganaAutoSetControl = column.FuriganaAutoSetControl;
                this.Name = column.Name;
                this.SearchDisplayFlag = column.SearchDisplayFlag;
                this.PopupSearchSendParams = column.PopupSearchSendParams;
                this.PopupAfterExecuteMethod = column.PopupAfterExecuteMethod;
                this.PopupBeforeExecuteMethod = column.PopupBeforeExecuteMethod;

                if (RegistCheckMethod == null)
                {
                    RegistCheckMethod = new Collection<SelectCheckDto>();
                }
                if (FocusOutCheckMethod == null)
                {
                    FocusOutCheckMethod = new Collection<SelectCheckDto>();
                }
                if (popupWindowSetting == null)
                {
                    popupWindowSetting = new Collection<JoinMethodDto>();
                }
                if (PopupSearchSendParams == null)
                {
                    PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();
                }
                this.Name = column.Name;

                this.ReadOnlyPopUp = column.ReadOnlyPopUp;
                this.PopupTitleLabel = column.PopupTitleLabel;

                this.ClearFormField = column.ClearFormField;
                this.PopupClearFormField = column.PopupClearFormField;
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            this.CloneChiled();
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string Name { get; set; }
        private bool ShouldSerializeName()
        {
            return this.Name != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string DBFieldsName { get; set; }
        private bool ShouldSerializeDBFieldsName()
        {
            return this.DBFieldsName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        public string ItemDefinedTypes { get; set; }
        private bool ShouldSerializeItemDefinedTypes()
        {
            return this.ItemDefinedTypes != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        private bool ShouldSerializeShortItemName()
        {
            return this.ShortItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        public int SearchDisplayFlag { get; set; }
        private bool ShouldSerializeSearchDisplayFlag()
        {
            return this.SearchDisplayFlag != 0;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [TypeConverter(typeof(PopupWindowConverter))]
        [Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        public string PopupWindowName { get; set; }
        private bool ShouldSerializePopupWindowName()
        {
            return this.PopupWindowName != null;
        }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public string ErrorMessage { get; set; }
        private bool ShouldSerializeErrorMessage()
        {
            return this.ErrorMessage != null;
        }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public Color DefaultBackColor { get; set; }
        private bool ShouldSerializeDefaultBackColor()
        {
            return this.DefaultBackColor != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        public string GetCodeMasterField { get; set; }
        private bool ShouldSerializeGetCodeMasterField()
        {
            return this.GetCodeMasterField != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        private bool ShouldSerializeSetFormField()
        {
            return this.SetFormField != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        public Decimal CharactersNumber { get; set; }
        private bool ShouldSerializeCharactersNumber()
        {
            return this.CharactersNumber != 0;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        public bool ZeroPaddengFlag { get; set; }
        private bool ShouldSerializeZeroPaddengFlag()
        {
            return this.ZeroPaddengFlag != false;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        public WINDOW_ID PopupWindowId { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップでマルチセレクトをする場合に指定してください。")]
        public bool PopupMultiSelect { get; set; }
        private bool ShouldSerializePopupMultiSelect()
        {
            return this.PopupMultiSelect != false;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データを設定する場合に指定してください。")]
        public DataTable PopupDataSource { get; set; }
        private bool ShouldSerializePopupDataSource()
        {
            return this.PopupDataSource != null;
        }

        /// <summary>
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>PopupDataSourceを指定して、PopupDataHeaderTitleを指定しない場合、PopupDataSourceに設定されている列名が表示される</para>
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データ列名を設定する場合に指定してください。")]
        [Browsable(false)]
        public string[] PopupDataHeaderTitle { get; set; }
        private bool ShouldSerializePopupDataHeaderTitle()
        {
            return this.PopupDataHeaderTitle != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("フリガナの自動設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string FuriganaAutoSetControl { get; set; }
        private bool ShouldSerializeFuriganaAutoSetControl()
        {
            return this.FuriganaAutoSetControl != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("自動複写設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string CopyAutoSetControl { get; set; }
        private bool ShouldSerializeCopyAutoSetControl()
        {
            return this.CopyAutoSetControl != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("自動複写時にスペースも含める場合はTrueで設定してください。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool CopyAutoSetWithSpace { get; set; }
        private bool ShouldSerializeFuriganaAutoSetWithSpace()
        {
            return this.CopyAutoSetWithSpace != false;
        }
        internal void ResetFuriganaAutoSetWithSpace()
        {
            this.CopyAutoSetWithSpace = false;
        }

        /// <summary>
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        public string[] PopupSendParams { get; set; }
        private bool ShouldSerializePopupSendParams()
        {
            return this.PopupSendParams != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの表示条件を選んでください。")]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        /// <summary>
        /// 検索ポップアップへ送信する値
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索ポップアップで絞込みを行うため、テーブルのキー名と値を指定")]
        public Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }
        private bool ShouldSerializePopupSearchSendParams()
        {
            return this.PopupSearchSendParams != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("フォーカスアウト時に行うフォーマットを選んでください。")]
        [TypeConverter(typeof(FormatConverter))]
        public string FormatSetting { get; set; }
        private bool ShouldSerializeFormatSetting()
        {
            return this.FormatSetting != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("FormatSettingでカスタムを指定した場合にフォーマット（「#」「,」「0」「.」の組合せのみ）を設定してください。")]
        public string CustomFormatSetting { get; set; }
        private bool ShouldSerializeCustomFormatSetting()
        {
            return this.CustomFormatSetting != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        public string PopupGetMasterField { get; set; }
        private bool ShouldSerializePopupGetMasterField()
        {
            return this.PopupGetMasterField != null;
        }


        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string PopupSetFormField { get; set; }
        private bool ShouldSerializePopupSetFormField()
        {
            return this.PopupSetFormField != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップから戻ってきた後に実行させたいメソッド名を指定してください。")]
        public string PopupAfterExecuteMethod { get; set; }
        private bool ShouldSerializePopupAfterExecuteMethod()
        {
            return this.PopupAfterExecuteMethod != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップが開く前に実行させたいメソッド名を指定してください。")]
        public string PopupBeforeExecuteMethod { get; set; }
        private bool ShouldSerializePopupBeforeExecuteMethod()
        {
            return this.PopupBeforeExecuteMethod != null;
        }

        [Category("EDISONプロパティ_テキストボックス編集判定")]
        [Description("テキストボックスが編集済みかどうかの判定を返す")]
        public bool TextBoxChanged { get; set; }
        private bool ShouldSerializeTextBoxChanged()
        {
            return this.TextBoxChanged != false;
        }

        [Category("EDISONプロパティ_チェック設定")]
        public string ClearFormField { get; set; }
        private bool ShouldSerializeClearFormField()
        {
            return this.ClearFormField != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        public string PopupClearFormField { get; set; }
        private bool ShouldSerializePopupClearFormField()
        {
            return this.PopupClearFormField != null;
        }
        #endregion

        /// <summary>
        /// 名称取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        ///// <summary>
        ///// ヒントテキスト設定処理
        ///// </summary>
        //public virtual void CreateHintText()
        //{
        //    this.Tag = ControlUtility.CreateHintText(this);
        //}

        /// <summary>
        /// 値の取得処理
        /// </summary>
        public string GetResultText()
        {
            return Convert.ToString(this.Value);
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        public void SetResultText(string value)
        {
            if (Constans.NULL_STRING == value)
            {
                value = string.Empty;
            }
            else if (this.ItemDefinedTypes == DB_TYPE.MONEY.ToTypeString())
            {
                value = string.Format("{0:#,0}", value);
            }

            this.Value = value;
        }

        private bool _ReadOnlyPopUp = false;
        /// <summary>
        /// ポップアップを読み取り専用でも出すかどうかを設定します
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("読み取り専用でもポップアップを起動する場合はTrueにしてください")]
        [DefaultValue(false)]
        public bool ReadOnlyPopUp
        {
            get
            {
                return _ReadOnlyPopUp;
            }
            set
            {
                _ReadOnlyPopUp = value;
            }
        }


        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public virtual void KeyPress(KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// フォーカスアウト処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public virtual void Leave(EventArgs e)
        {
            ((ICustomAutoChangeBackColor)this).UpdateBackColor(false);
        }

        /// <summary>
        /// フォーカスイン処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public virtual void Enter(EventArgs e)
        {
            ((ICustomAutoChangeBackColor)this).UpdateBackColor(true);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public virtual void KeyDown(KeyEventArgs e)
        {

        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public virtual void PreviewKeyDown(PreviewKeyDownEventArgs e)
        {

        }

        /// <summary>
        /// テキスト変更処理
        /// </summary>
        /// <param name="e"></param>
        public virtual void TextChanged(EventArgs e)
        {
            //効率化
            if (!string.IsNullOrEmpty(this.FuriganaAutoSetControl) || !string.IsNullOrEmpty(CopyAutoSetControl))
            {
                this.IsInputErrorOccured = false;
                //this.UpdateBackColor();

                var ctrlUtil = new ControlUtility();
                var textLogic = new CustomTextBoxLogic(this);
                DataGridViewRow row = this.DataGridView.CurrentRow;
                object[] fields = new object[row.Cells.Count];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    fields[i] = row.Cells[i];
                }

                // フリガナ設定処理
                // 1次マスタの仕様に合わせる為コメントアウト
                //if (!string.IsNullOrEmpty(this.FuriganaAutoSetControl))
                //{
                //    textLogic.SettingFurigana(this, fields);
                //}

                // 1次マスタのフレームワークより
                if (this.Value == null || string.IsNullOrWhiteSpace(this.Value.ToString()))
                {
                    textLogic.SettingFuriganaPhase1(this, this.FuriganaAutoSetControl, fields, string.Empty);
                }

                // 自動複写処理
                if (!string.IsNullOrEmpty(CopyAutoSetControl))
                {
                    textLogic.SettingCopyValue(this, fields);
                }
            }
        }

        /// <summary>
        /// キー押下時の処理
        /// </summary>
        public virtual void KeyUp(object sender, KeyEventArgs e)
        {
            // CustomDataGridView.ProcessDataGridViewKeyにてポップアップ表示を行うため
            // ここでは何もしない
        }
        /// <summary>
        /// ポップアップのタイトルを強制的に上書きする場合は設定してください
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップのタイトルを強制的に上書きする場合は設定してください")]
        public string PopupTitleLabel { get; set; }
        private bool ShouldSerializePopupTitleLabel()
        {
            return !string.IsNullOrEmpty(this.PopupTitleLabel);
        }


        #region ICustomAutoChangeBackColor メンバー

        /// <summary>
        /// DGVにはEnabledは無い
        /// </summary>
        public bool Enabled
        {
            get
            {
                return true;
            }
            set
            {
                //無視;
            }
        }

        /// <summary>
        /// フォーカスがあるかどうか
        /// </summary>
        public bool Focused
        {
            get
            {
                return (this.DataGridView != null && this.DataGridView.CurrentCell != null && this.DataGridView.Focused
                        && this.DataGridView.CurrentCell.RowIndex == this.RowIndex
                        && this.DataGridView.CurrentCell.ColumnIndex == this.ColumnIndex);
            }
        }


        #endregion


        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
                this.UpdateBackColor();//値が変わる場合は色を再設定
            }
        }

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

        /// <summary>
        /// 入力用コントロールの型情報を取得する。
        /// DataGridViewがTextBoxセルの編集開始直前に呼び出す。
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(DvgCustomTextBoxEditingControl);
            }
        }
    }
}
