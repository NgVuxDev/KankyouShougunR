using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku.APP
{
     ///<summary>
     ///G602 定期実績CSV出力
     ///</summary>
    class G602 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var callForm = new Shougun.Core.Allocation.TeikiJissekiHoukoku.UIForm();
            var callHeader = new Shougun.Core.Allocation.TeikiJissekiHoukoku.UIHeader();
            return new BusinessBaseForm(callForm, callHeader);
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
