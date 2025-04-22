// $Id: CustomAddressSearchButton.cs 18532 2014-04-04 00:23:26Z tnakatsu@oec-o.co.jp $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 住所検索ボタン
    /// </summary>
    public partial class CustomAddressSearchButton : Button, ICustomControl
    {
        #region フィールド

        private static readonly string AssemblyName = "JushoKensakuPopup2";

        private static readonly string CalassNameSpace = "APP.JushoKensakuPopupForm2";

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomAddressSearchButton()
        {
            InitializeComponent();

            this.Text = "住所検索";

            // FormFieldのコピー
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
        /// 描画処理
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            this.CreateHintText();
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
        }

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (string.IsNullOrEmpty(this.PostalCodeControl))
            {
                return;
            }

            var cstmLogic = new CustomControlLogic(this);
            var ctrlUtil = new ControlUtility();
            ctrlUtil.ControlCollection = this.Parent.Controls;
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));

            var field = fields.Where(n => n.Name.Equals(this.PostalCodeControl)).FirstOrDefault();
            if (field == null)
            {
                return;
            }

            var post7 = field.Text.Replace("-", string.Empty);
            if (string.IsNullOrEmpty(post7) || post7.Length < 3)
            {
                return;
            }

            post7 = post7.Insert(3, "-");

            JushoSearchLogic logic = new JushoSearchLogic();
            S_ZIP_CODE[] zipCodeArray = logic.GetDataByPost7LikeSearch(post7);

            if (zipCodeArray.Length == 0)
            {
                return;
            }

            string todofuken = string.Empty;
            string jusho = string.Empty;

            if (zipCodeArray.Length == 1)
            {
                S_ZIP_CODE entity = zipCodeArray.First();

                post7 = entity.POST7;
                todofuken = entity.TODOUFUKEN;
                jusho = entity.SIKUCHOUSON + entity.OTHER1;

                var result = this.AddTodoufukenToAddressFlg ? todofuken + jusho : jusho;

                if (this.AddressControl.IndexOf(",") < 0)
                {
                    var jushoControl = (CustomTextBox)ctrlUtil.FindControl(this.Parent, this.AddressControl);
                    PropertyUtility.SetValue(jushoControl, "Text", result);
                }
                else
                {
                    bool firstAddressControlFlg = true;
                    string[] addressControlList = this.AddressControl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string addressControl in addressControlList)
                    {
                        var jushoControl = (CustomTextBox)ctrlUtil.FindControl(this.Parent, addressControl);
                        if (firstAddressControlFlg)
                        {
                            PropertyUtility.SetValue(jushoControl, "Text", result);
                        }
                        else
                        {
                            PropertyUtility.SetValue(jushoControl, "Text", string.Empty);
                        }
                        firstAddressControlFlg = false;
                    }
                }
            }
            else if (1 < zipCodeArray.Length)
            {
                // 住所検索ポップアップ表示
                var assembltyName = AssemblyName + ".dll";

                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, AssemblyName + "." + CalassNameSpace);
                var classinfo = objectHandler.Unwrap() as SuperPopupForm;

                if (classinfo != null)
                {
                    // 検索結果を設定
                    classinfo.Params = new object[1] { zipCodeArray };

                    classinfo.ShowDialog();

                    if (classinfo.ReturnParams != null)
                    {
                        for (int i = 0; i < classinfo.ReturnParams.Count; i++)
                        {
                            List<PopupReturnParam> returnParamList = classinfo.ReturnParams[i];

                            post7 = returnParamList[0].Value.ToString();
                            todofuken = returnParamList[1].Value.ToString();
                            jusho = returnParamList[2].Value.ToString()
                                            + returnParamList[3].Value.ToString();
                        }

                        var result = this.AddTodoufukenToAddressFlg ? todofuken + jusho : jusho;

                        if (this.AddressControl.IndexOf(",") < 0)
                        {
                            var jushoControl = (CustomTextBox)ctrlUtil.FindControl(this.Parent, this.AddressControl);
                            PropertyUtility.SetValue(jushoControl, "Text", result);
                        }
                        else
                        {
                            bool firstAddressControlFlg = true;
                            string[] addressControlList = this.AddressControl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string addressControl in addressControlList)
                            {
                                var jushoControl = (CustomTextBox)ctrlUtil.FindControl(this.Parent, addressControl);
                                if (firstAddressControlFlg)
                                {
                                    PropertyUtility.SetValue(jushoControl, "Text", result);
                                }
                                else
                                {
                                    PropertyUtility.SetValue(jushoControl, "Text", string.Empty);
                                }
                                firstAddressControlFlg = false;
                            }
                        }
                    }
                    classinfo.Dispose();
                }
            }

            if (!string.IsNullOrEmpty(post7))
            {
                var post7Control = (CustomTextBox)ctrlUtil.FindControl(this.Parent, this.PostalCodeControl);
                PropertyUtility.SetValue(post7Control, "Text", post7);
            }

            if (!string.IsNullOrEmpty(todofuken))
            {
                M_TODOUFUKEN[] todofukenEntitys = logic.GetDataByTdfkName(todofuken);
                if (!string.IsNullOrEmpty(this.TodoufukenControl) && todofukenEntitys.Length == 1)
                {
                    var tdfkControl = (CustomTextBox)ctrlUtil.FindControl(this.Parent, this.TodoufukenControl);
                    PropertyUtility.SetValue(tdfkControl, "Text", todofukenEntitys.First().TODOUFUKEN_CD.ToString());
                    SuperForm superForm;
                    if (ControlUtility.TryGetSuperForm(tdfkControl, out superForm))
                    {
                        tdfkControl.Focus();
                        superForm.SetPreviousValue(String.Empty, this);
                        this.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// PreviewKeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 親フォームでキーイベントをハンドリング出来るようにするため
                e.IsInputKey = true;
            }

            base.OnPreviewKeyDown(e);
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("検索対象の郵便番号コントロール名を設定してください。")]
        public string PostalCodeControl { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「AddressControl」に都道府県を追加して表示します。")]
        public bool AddTodoufukenToAddressFlg { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        [Description("住所コントロール名を設定してください。")]
        public string AddressControl { get; set; }

        [Category("EDISONプロパティ_画面設定")]
        [Description("都道府県コントロール名を設定してください。")]
        public string TodoufukenControl { get; set; }



        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        [Browsable(false)]
        public string DBFieldsName { get; set; }
        private bool ShouldSerializeDBFieldsName()
        {
            return this.DBFieldsName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        [Browsable(false)]
        public string ItemDefinedTypes { get; set; }
        private bool ShouldSerializeItemDefinedTypes()
        {
            return this.ItemDefinedTypes != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        [Browsable(false)]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        [Browsable(false)]
        public string ShortItemName { get; set; }
        private bool ShouldSerializeShortItemName()
        {
            return this.ShortItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        [Browsable(false)]
        public int SearchDisplayFlag { get; set; }
        private bool ShouldSerializeSearchDisplayFlag()
        {
            return this.SearchDisplayFlag != 0;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        [Browsable(false)]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        [Browsable(false)]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [TypeConverter(typeof(PopupWindowConverter))]
        [Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        [Browsable(false)]
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
        [Browsable(false)]
        public string GetCodeMasterField { get; set; }
        private bool ShouldSerializeGetCodeMasterField()
        {
            return this.GetCodeMasterField != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        [Browsable(false)]
        public string SetFormField { get; set; }
        private bool ShouldSerializeSetFormField()
        {
            return this.SetFormField != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        [Browsable(false)]
        public Decimal CharactersNumber { get; set; }
        private bool ShouldSerializeCharactersNumber()
        {
            return this.CharactersNumber != 0;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        [Browsable(false)]
        public bool ZeroPaddengFlag { get; set; }
        private bool ShouldSerializeZeroPaddengFlag()
        {
            return this.ZeroPaddengFlag != false;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        [Browsable(false)]
        public WINDOW_ID PopupWindowId { get; set; }
        private bool ShouldSerializePopupWindowId()
        {
            return this.PopupWindowId != null;
        }

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
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Browsable(false)]
        public string[] PopupSendParams { get; set; }
        private bool ShouldSerializePopupSendParams()
        {
            return this.PopupSendParams != null;
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
        [Description("ポップアップの表示条件を選んでください。")]
        [Browsable(false)]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Browsable(false)]
        public string PopupGetMasterField { get; set; }
        private bool ShouldSerializePopupGetMasterField()
        {
            return this.PopupGetMasterField != null;
        }


        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        [Browsable(false)]
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
        /// 名称取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// 設定値取得取得
        /// </summary>
        /// <returns></returns>
        public string GetResultText()
        {
            return Text;
        }

        /// <summary>
        /// 値設定処理
        /// </summary>
        /// <param name="value"></param>
        public void SetResultText(string value)
        {
            this.Text = value;
        }

        /// <summary>
        /// ヒントテキスト設定処理
        /// </summary>
        public void CreateHintText()
        {
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
        /// ポップアップのタイトルを強制的に上書きする場合は設定してください
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップのタイトルを強制的に上書きする場合は設定してください")]
        public string PopupTitleLabel { get; set; }
        private bool ShouldSerializePopupTitleLabel()
        {
            return !string.IsNullOrEmpty(this.PopupTitleLabel);
        }

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

    }
}
