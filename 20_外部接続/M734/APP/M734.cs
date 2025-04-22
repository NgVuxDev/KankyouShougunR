using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.FormManager;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    /// <summary>
    /// M734 電子契約最新照会
    /// </summary>
    class M734 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var headerForm = new UIHeader();
            var callForm = new UIForm();
            var businessForm = new BusinessBaseForm(callForm, headerForm) { IsInFormResizable = true };
            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form"></param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
