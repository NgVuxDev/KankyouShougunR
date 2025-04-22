using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
{
    /// <summary>
    /// G611 入金消込
    /// </summary>
    class G611 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            WindowInitDataDTO windowInit = new WindowInitDataDTO();

            if (args.Length > 0
                && args[0] != null)
            {
                windowInit = args[0] as WindowInitDataDTO;
            }

            var HeaderForm = new HeaderBaseForm();
            var callForm = new UIForm(windowInit);
            var popupForm = new BasePopForm(callForm, HeaderForm);

            return popupForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        public void UpdateForm(Form form)
        {
        }
    }
}
