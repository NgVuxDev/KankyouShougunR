using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.FormManager;

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku
{
    class G145 : r_framework.FormManager.IShougunForm 
    {

        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.UIHeader();
            var callForm = new Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku.UIForm();
            return new r_framework.APP.Base.BusinessBaseForm(callForm, HeaderForm);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 常に前面表示
            return true;
        }

        public void UpdateForm(Form form)
        {
        }
    }
}
