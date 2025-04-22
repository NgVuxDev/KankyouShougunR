// $Id: G021.cs 15378 2014-01-29 00:25:09Z sys_dev_22 $using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
{
    /// <summary>
    /// G742 現場メモ一覧
    /// </summary>
    class G742 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : T_GENBAMEMO_ENTRY
            T_GENBAMEMO_ENTRY entry = null;
            if (0 < args.Length && args[0] != null)
            {
                entry = args[0] as T_GENBAMEMO_ENTRY;
            }

            // 引数 arg[1] : Window_Id
            string winId = string.Empty;
            if (1 < args.Length && args[1] != null)
            {
                winId = args[1] as string;
            }

            var callForm = new Shougun.Core.ExternalConnection.GenbamemoIchiran.UIForm(entry, winId);
            return new BusinessBaseForm(callForm, new UIHeaderForm());
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
