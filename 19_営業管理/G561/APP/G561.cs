using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済申請一覧
    /// </summary>
    class G561 : r_framework.FormManager.IShougunForm
    {
        private ShouninzumiDenshiShinseiIchiranUIForm callForm;

        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new ShouninzumiDenshiShinseiIchiranHeaderForm();
            callForm = new ShouninzumiDenshiShinseiIchiranUIForm(WINDOW_ID.T_SHOUNINZUMI_DENSHI_SHINSEI_ICHIRAN);
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
            LogUtility.DebugMethodStart(form);

            callForm.Search();

            LogUtility.DebugMethodEnd();
        }
    }
}
