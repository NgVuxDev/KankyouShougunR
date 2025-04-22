using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;

namespace Shougun.Core.PayByProxy.DainoNyuryuku
{
    /// <summary>
    /// G161 代納入力
    /// </summary>
    class G161 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0]: WINDOW_TYPE モード 新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1]: 代納番号
            long dainouNumber = 0;
            if (args.Length > 1)
            {
                long.TryParse(args[1].ToString(), out dainouNumber);
            }

            // 新規/修正/削除モード/複写
            // 引数:
            var callForm = new Shougun.Core.PayByProxy.DainoNyuryuku.G161Form(windowType, dainouNumber);
            var headerForm = new Shougun.Core.PayByProxy.DainoNyuryuku.G161HeaderForm();
            if (windowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 代納番号が使えるかチェック
                bool catchErr = true;
                var isExistUkeireData = callForm.IsExistDainoData(dainouNumber, out catchErr);
                if (!catchErr)
                {
                    return null;
                }
                if (!isExistUkeireData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    callForm.Dispose();
                    return null;
                }
            }
            return new BusinessBaseForm(callForm, headerForm);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true: 同じ | false: 異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ代納番号のフォームなら前面表示、そうでなければ新規画面作成
            // ----20150603 顧客カルテなど画面から遷移する時、伝票番号は文字列型の対応(代納不具合一覧52) Start
            // 引数 arg[1]: 代納番号
            long dainouNumber = 0;
            if (args.Length > 1)
            {
                long.TryParse(args[1].ToString(), out dainouNumber);
                // 20150603 顧客カルテなど画面から遷移する時、伝票番号は文字列型の対応(代納不具合一覧52) End

                var baseForm = form as BusinessBaseForm;
                var callForm = baseForm.inForm as Shougun.Core.PayByProxy.DainoNyuryuku.G161Form;
                if (dainouNumber == 0 || dainouNumber.ToString() == callForm.DAINOU_NUMBER.Text.Trim())
                {
                    return true;
                }
            }

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
