using System.Windows.Forms;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// G606 実績報告書（運搬実績）
    /// </summary>
    class G606 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : 画面区分
            WINDOW_TYPE window_type = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                window_type = (WINDOW_TYPE)args[0];
            }
            UIForm callForm = new UIForm(window_type);
            var callHeader = new UIHeader();
            return new BasePopForm(callForm, callHeader);
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
