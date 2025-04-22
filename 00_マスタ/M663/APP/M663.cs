using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Master.CourseIchiran
{
    /// <summary>
    /// M663 コース一覧
    /// </summary>
    class M663 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            DTOClass dto = new DTOClass();
            if (args != null && args.Length > 0)
            {
                // 業者CD
                dto.GYOUSHA_CD = args[0].ToString();
                // 現場CD
                dto.GENBA_CD = args[1].ToString();
            }
            else
            {
                dto = null;
            }
            var headForm = new UIHeader();

            if (dto != null)
            {
                var callForm = new UIForm(dto);
                return new BusinessBaseForm(callForm, headForm);
            }
            else
            {
                var callForm = new UIForm();
                return new BusinessBaseForm(callForm, headForm);
            }
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
        /// <param name=form>表示を更新するフォーム</param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
