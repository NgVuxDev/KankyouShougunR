using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    internal class G277 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数がそのまま渡される</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            int iDensyuKbn = 0;
            string sSyainid = "";
            string searchString = null;
            if (args.Length >= 2)
            {
                iDensyuKbn = (int)args[0];
                sSyainid = (string)args[1];

                if (args.Length >= 3)
                {
                    searchString = (string)args[2];
                }
            }

            // 社員コード、伝種区分を共通画面に渡す
            var HeaderForm = new Shougun.Core.BusinessManagement.MitumoriIchiran.HeaderForm();
            var callForm = new Shougun.Core.BusinessManagement.MitumoriIchiran.MitumoriIchiranForm((DENSHU_KBN)iDensyuKbn, searchString, HeaderForm, sSyainid);
            var businessForm = new BusinessBaseForm(callForm, HeaderForm);

            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="Form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
            // もし伝種区分ごとに一覧画面を別に開きたい場合はG055同メソッドのコメントを参照のこと
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