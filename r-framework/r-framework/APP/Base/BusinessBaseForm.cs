using System;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.APP.Base
{
    #region - Class -

    /// <summary>業務画面にて使用するベースクラス</summary>
    public partial class BusinessBaseForm : BaseBaseForm
    {
        #region - Fields -
        #endregion - Fields

        #region - Constructors -
        /// <summary>コンストラクタ
        /// 引数で渡されたFormを埋め込み画面を表示する</summary>
        /// <param name="form">埋め込むForm</param>
        /// <param name="windowType">画面タイプ</param>
        public BusinessBaseForm(Form form, WINDOW_TYPE windowType, RibbonMainMenu ribbon = null)
        {
            this.InitializeComponent();

            if (ribbon == null)
            {
                this.ribbonForm = new RibbonMainMenu(FormManager.FormManager.UserRibbonMenu.MenuConfigXML, (Dto.CommonInformation)FormManager.FormManager.UserRibbonMenu.GlobalCommonInformation.Clone());
            }
            else
            {
                this.ribbonForm = new RibbonMainMenu(ribbon.MenuConfigXML, ribbon.GlobalCommonInformation);
            }

            this.inForm = form;

            switch (windowType)
            {
                case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                    this.headerForm = new ListHeaderForm();
                    break;
                default:
                    this.headerForm = new DetailedHeaderForm();
                    break;
            }

            //コンストラクタで追加必須
            this.ribbonForm.TopLevel = false; //フォームを追加するには必須の設定
            this.headerForm.TopLevel = false;
            this.inForm.TopLevel = false;
            this.Controls.Add(this.ribbonForm);
            this.Controls.Add(this.headerForm);
            this.Controls.Add(this.inForm);
        }

        /// <summary>明細部にform、ヘッダー部にheaderForm</summary>
        /// <param name="form">明細フォーム</param>
        /// <param name="headerForm">ヘッダーフォーム</param>
        public BusinessBaseForm(Form form, HeaderBaseForm headerForm, RibbonMainMenu ribbon = null)
        {
            this.InitializeComponent();

            if (ribbon == null)
            {
                this.ribbonForm = new RibbonMainMenu(FormManager.FormManager.UserRibbonMenu.MenuConfigXML, (Dto.CommonInformation)FormManager.FormManager.UserRibbonMenu.GlobalCommonInformation.Clone());
            }
            else
            {
                this.ribbonForm = new RibbonMainMenu(ribbon.MenuConfigXML, ribbon.GlobalCommonInformation);
            }

            this.inForm = form;
            this.headerForm = headerForm;

            //コンストラクタで追加必須
            this.ribbonForm.TopLevel = false; //フォームを追加するには必須の設定
            this.headerForm.TopLevel = false;
            this.inForm.TopLevel = false;
            this.Controls.Add(this.ribbonForm);
            this.Controls.Add(this.headerForm);
            this.Controls.Add(this.inForm);
        }

        #endregion - Constructors -

        #region - Properties -
        #endregion - Properties -

        #region - Methods -
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion - Methods -

        // 20150723 障害#11443一時対応 Start
        [Obsolete("画面起動最大数一時対応用、FormManagerを利用されてない画面が表示する時、自動的に本メソッドを呼び出す。今後、FormManagerを利用するよに修正は必要。")]
        public new void Show()
        {
            FormManager.FormManager.OpenNoneIdForm(this);
        }

        [Obsolete("画面起動最大数一時対応用、FormManagerを利用されてない画面が表示する時、自動的に本メソッドを呼び出す。今後、FormManagerを利用するよに修正は必要。")]
        public new DialogResult ShowDialog()
        {
            return FormManager.FormManager.OpenNoneIdFormModal(this);
        }
        // 20150723 障害#11443一時対応 End
    }

    #endregion - Class -
}
