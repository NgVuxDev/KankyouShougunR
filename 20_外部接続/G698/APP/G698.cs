using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.FormManager;

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    /// <summary>
    /// G698 コース最適化入力
    /// </summary>
    class G698 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            long systemId = 0L;
            if (0 < args.Length)
            {
                systemId = long.Parse(args[0].ToString());
            }

            var callForm = new UIForm(systemId);
            var callHeader = new UIHeader();
            var businessBaseForm = new BusinessBaseForm(callForm, callHeader);

            return businessBaseForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同一作業者のフォームなら前面表示、そうでなければ新規画面作成
            if (0 < args.Length)
            {
                long systemId = (long)args[0];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as UIForm;
                return uiForm.IsSameSagyousha(systemId);
            }
            return false;
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
