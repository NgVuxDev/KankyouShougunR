using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;

namespace Shougun.Core.Common.KokyakuKarute.APP
{
    public partial class G173HeaderForm :  r_framework.APP.Base.HeaderBaseForm
    {
        public G173HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "顧客カルテ";

            var parentForm = this.Parent ?? this;

            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(base.lb_title.Text);

            if (!this.windowTypeLabel.Visible)
            {
                this.panel1.Location = new Point(this.lb_title.Location.X + this.lb_title.Width + 4, this.panel1.Location.Y);
            }
        }


    }
}
