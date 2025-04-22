using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Stock.ZaikoIdouIchiran
{
    class G632 : r_framework.FormManager.IShougunForm
    {
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new Shougun.Core.Stock.ZaikoIdouIchiran.UIHeader();
            var callForm = new Shougun.Core.Stock.ZaikoIdouIchiran.UIForm(HeaderForm);
            return new BusinessBaseForm(callForm, HeaderForm);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        public void UpdateForm(Form form)
        {
        }

    }
}
