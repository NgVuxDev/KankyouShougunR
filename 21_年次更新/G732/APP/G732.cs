using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{
    class G732 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var callForm = new Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.UIForm();
            var callHeader = new Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.HeaderForm();
            return new BusinessBaseForm(callForm, callHeader);
        }

        public bool IsSameContentForm(Form form, params object[] args)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        public void UpdateForm(Form form)
        {
            //throw new System.NotImplementedException();
        }
    }
}
