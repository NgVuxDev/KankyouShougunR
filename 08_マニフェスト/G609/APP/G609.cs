using System.Windows.Forms;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.JissekiHokokuCsv
{
    /// <summary>
    /// G609 実績報告書（運搬実績）CSV出力
    /// </summary>
    class G609 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : システムID
            T_JISSEKI_HOUKOKU_ENTRY ent = null;
            bool isSyukei = false;
            if (args.Length > 0 && args[0] != null)
            {
                ent = (T_JISSEKI_HOUKOKU_ENTRY)args[0];
            }
            if (args.Length > 1 && args[1] != null)
            {
                isSyukei = (bool)args[1];
            }
            UIForm callForm = new UIForm(ent, isSyukei);
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
            return false;
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
