using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    /// <summary>
    /// G597 未入金一覧
    /// </summary>
    class G597 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new HeaderBaseForm();
            var callForm = new UIForm_MinyuukinIchiran(WINDOW_ID.T_MINYUUKIN_ICHIRAN);
            var popupForm = new BusinessBaseForm(callForm, HeaderForm);

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
