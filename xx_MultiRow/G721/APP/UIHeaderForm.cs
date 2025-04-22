using System.Linq;
using System.Windows.Forms;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku2
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        public LogicClass logic;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        public UIHeaderForm(UIForm targetForm)
        {
            this.form = targetForm;
            InitializeComponent();
        }

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (this.logic != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                    if (!forward)
                    {
                        if ((this.form.beforbeforControlName == "KYOTEN_CD" && this.KYOTEN_CD.TabStop)
                            || (this.form.beforbeforControlName == "UKETSUKE_NUMBER" && this.UKETSUKE_NUMBER.TabStop && !this.KYOTEN_CD.TabStop)
                            || (this.form.beforbeforControlName == "KEIRYOU_NUMBER" && this.KEIRYOU_NUMBER.TabStop && !this.UKETSUKE_NUMBER.TabStop && !this.KYOTEN_CD.TabStop))
                        {
                            this.form.EMPTY_KEIRYOU_TIME.Focus();
                            return;
                        }

                    }

                    if (this.form.beforbeforControlName == "gcMultiRow1" || this.form.beforbeforControlName == "gcMultiRow2")
                    {
                        this.form.beforbeforControlName = "UKETSUKE_NUMBER";
                    }

                    this.ActiveControl = this.allControl.Where(c => c.Name == this.form.beforbeforControlName).FirstOrDefault();

                    //PhuocLoc 2020/12/01 #136219 -Start
                    //if (this.ActiveControl == null)
                    //    this.ActiveControl = this.form.allControl.Where(c => c.Name == this.form.beforbeforControlName).FirstOrDefault();
                    //PhuocLoc 2020/12/01 #136219 -End

                    this.logic.GotoNextControl(forward);
                }
            }
        }
    }
}
