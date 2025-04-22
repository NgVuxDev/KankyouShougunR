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

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビューのカスタムチェックボックスセル
    /// </summary>
    public partial class DgvCustomCheckBoxCell : DataGridViewCheckBoxCell, ICustomDataGridControl, ICustomAutoChangeBackColor
    {
        #region ICustomDataGridControlプロパティ
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// 値設定先コントロール。未指定(null)の場合、ICustomControl.PopupSetFormFieldが使用される。
        /// </summary>
        public CustomControl.ICustomDataGridControl[] ReturnControls { get; set; }

        /// <summary>
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>PopupDataSourceを指定して、PopupDataHeaderTitleを指定しない場合、PopupDataSourceに設定されている列名が表示される</para>
        /// </summary>
        public string[] PopupDataHeaderTitle { get; set; }
        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomCheckBoxCell()
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
        /// カラムから各種パラメータをクローンしてくる処理
        /// </summary>
        public virtual void CloneChiled()
        {
            var column = this.OwningColumn as DgvCustomCheckBoxColumn;

            if (column != null)
            {
                this.DBFieldsName = column.DBFieldsName;
                this.ItemDefinedTypes = column.ItemDefinedTypes;
                this.DisplayItemName = column.DisplayItemName;
                this.ShortItemName = column.ShortItemName;
                this.RegistCheckMethod = column.RegistCheckMethod;
                this.FocusOutCheckMethod = column.FocusOutCheckMethod;
                this.GetCodeMasterField = column.GetCodeMasterField;
                this.SetFormField = column.SetFormField;
                this.ErrorMessage = column.ErrorMessage;
                this.SearchDisplayFlag = column.SearchDisplayFlag;
                this.Name = column.Name;

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

                this.ReadOnlyPopUp = column.ReadOnlyPopUp;
                this.PopupTitleLabel = column.PopupTitleLabel;

                this.AutoChangeBackColorEnabled = column.AutoChangeBackColorEnabled;

                this.ClearFormField = column.ClearFormField;
                this.PopupClearFormField = column.PopupClearFormField;
            }
        }

        /// <summary>
        /// ペイントイベント
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

        /// <summary>
        /// 名称属性
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string Name { get; set; }
        private bool ShouldSerializeName()
        {
            return this.Name != null;
        }

        #region Property
        /// <summary>
        /// 対象のDBカラム名を設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string DBFieldsName { get; set; }
        private bool ShouldSerializeDBFieldsName()
        {
            return this.DBFieldsName != null;
        }

        /// <summary>
        /// DBカラムの型指定を行いプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        public string ItemDefinedTypes { get; set; }
        private bool ShouldSerializeItemDefinedTypes()
        {
            return this.ItemDefinedTypes != null;
        }

        /// <summary>
        /// 画面に表示する日本語名を設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

        /// <summary>
        /// 画面に表示する日本語短縮名を設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        private bool ShouldSerializeShortItemName()
        {
            return this.ShortItemName != null;
        }

        /// <summary>
        /// 汎用検索画面へ表示を行うかのフラグプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        public int SearchDisplayFlag { get; set; }
        private bool ShouldSerializeSearchDisplayFlag()
        {
            return this.SearchDisplayFlag != 0;
        }

        /// <summary>
        /// 登録時のチェック情報を格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        /// <summary>
        /// フォーカスアウト時のチェック情報を格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        /// <summary>
        /// 表示するポップアップの情報を格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [TypeConverter(typeof(PopupWindowConverter))]
        [Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        public string PopupWindowName { get; set; }
        private bool ShouldSerializePopupWindowName()
        {
            return this.PopupWindowName != null;
        }

        /// <summary>
        /// Errorメッセージの設定プロパティ
        /// </summary>
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

        /// <summary>
        /// マスタから取得してくるデータを指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        public string GetCodeMasterField { get; set; }
        private bool ShouldSerializeGetCodeMasterField()
        {
            return this.GetCodeMasterField != null;
        }

        /// <summary>
        /// マスタから取得してきたデータを設定するコントロールを指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        private bool ShouldSerializeSetFormField()
        {
            return this.SetFormField != null;
        }

        /// <summary>
        /// 入力可能桁数をバイトにて指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        public Decimal CharactersNumber { get; set; }
        private bool ShouldSerializeCharactersNumber()
        {
            return this.CharactersNumber != 0;
        }

        /// <summary>
        /// 0埋めを行うか否かの設定を行うプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        public bool ZeroPaddengFlag { get; set; }
        private bool ShouldSerializeZeroPaddengFlag()
        {
            return this.ZeroPaddengFlag != false;
        }

        /// <summary>
        /// ポップアップに表示する画面区分の設定を行うプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        public WINDOW_ID PopupWindowId { get; set; }

        /// <summary>
        /// ポップアップで複数選択の設定を行うプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップでマルチセレクトをする場合に指定してください。")]
        public bool PopupMultiSelect { get; set; }
        private bool ShouldSerializePopupMultiSelect()
        {
            return this.PopupMultiSelect != false;
        }

        /// <summary>
        /// ポップアップで取得済データを設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データを設定する場合に指定してください。")]
        public DataTable PopupDataSource { get; set; }
        private bool ShouldSerializePopupDataSource()
        {
            return this.PopupDataSource != null;
        }

        /// <summary>
        /// フリガナ自動設定を行うコントロールを指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("フリガナの自動設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string FuriganaAutoSetControl { get; set; }
        private bool ShouldSerializeFuriganaAutoSetControl()
        {
            return this.FuriganaAutoSetControl != null;
        }

        /// <summary>
        /// 自動複写を行う先を設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("自動複写設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string CopyAutoSetControl { get; set; }
        private bool ShouldSerializeCopyAutoSetControl()
        {
            return this.CopyAutoSetControl != null;
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

        /// <summary>
        /// ポップアップの表示条件を設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの表示条件を選んでください。")]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        /// <summary>
        /// ポップアップ表示時にどのデータを取得するかの設定を行うプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップ時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        public string PopupGetMasterField { get; set; }
        private bool ShouldSerializePopupGetMasterField()
        {
            return this.PopupGetMasterField != null;
        }

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


        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
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
            this.Value = Constans.NULL_STRING == value ? "" : value;
        }

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
        //public void CreateHintText()
        //{
        //    this.Tag = ControlUtility.CreateHintText(this);
        //}

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
        public void KeyPress(KeyPressEventArgs e)
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
        /// キーダウン時のイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public void KeyDown(KeyEventArgs e)
        {
        }

        /// <summary>
        /// キーダウン時のイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public void PreviewKeyDown(PreviewKeyDownEventArgs e)
        {
        }

        /// <summary>
        /// 入力されたデータが変更されたときのイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        public void TextChanged(EventArgs e)
        {
            this.IsInputErrorOccured = false;
        }

        /// <summary>
        /// テキストボックスにてスペースキー押下時の処理
        /// ポップアップ画面が設定されている場合は、表示を行う
        /// </summary>
        public virtual void KeyUp(object sender, KeyEventArgs e)
        {
            // スペースの場合、ポップアップ表示
            if (e.KeyCode == Keys.Space)
            {
                var cell = this as ICustomDataGridControl;
                if (cell == null)
                {
                    return;
                }
                DataGridViewRow row = this.DataGridView.CurrentRow;
                var ctrlUtil = new ControlUtility();

                object[] sendParamArray = null;

                if (cell.PopupSendParams != null)
                {
                    sendParamArray = new Control[cell.PopupSendParams.Length];
                    for (int i = 0; i < cell.PopupSendParams.Length; i++)
                    {
                        var sendParam = cell.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(this.DataGridView.FindForm(), sendParam);
                    }
                }

                object[] fields = new object[row.Cells.Count];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    fields[i] = row.Cells[i];
                }

                // ポップアップウィンドウ表示処理
                //var logic = new CustomControlLogic(cell);
                var logic = new CustomControlLogic(cell, this.DisplayPopUp, this.ReturnControls);
                logic.ShowPopupWindow(cell, fields, sender, sendParamArray);
            }
        }

        /// <summary>
        /// コピー処理、DataGridViewの内部処理でいろいろ呼び出される。
        /// </summary>
        /// <returns>新しい行のセル</returns>
        public override object Clone()
        {
            DgvCustomCheckBoxCell c = (DgvCustomCheckBoxCell)base.Clone();
            //ControlUtility.SetBackColorForDgvCell(c, Color.Empty);//背景色までコピーされるのでリセット

            c.AutoChangeBackColorEnabled = this.AutoChangeBackColorEnabled;

            return c;
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

    }
}