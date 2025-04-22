using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.CustomControl;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo.APP
{
    public class G133DateTime : CustomDateTimePicker
    {
        internal CustomTextBox TextBoxFrom;
        internal CustomTextBox TextBoxTo;
        internal LogicClass logic;
        
        protected override void OnEnter(EventArgs e)
        {
            if (this.logic != null)
            {
                if (this.logic.TextBoxFromToChk(this, TextBoxFrom, TextBoxTo))
                {
                    return;
                }
            }
            base.OnEnter(e);
        }
    }
}
