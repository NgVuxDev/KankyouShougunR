using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.PayByProxy.DainoMeisaihyo
{
    public partial class UIHeader : HeaderBaseForm
    {
        internal DainoMeisaihyoLogic logic { get; set; }
        public UIHeader()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void UIHeader_Load(object sender, EventArgs e)
        {

        }
    }
}
