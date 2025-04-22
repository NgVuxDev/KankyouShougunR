using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.APP
{
    /// <summary>
    /// G507 代納伝票発行
    /// </summary>
    class G507 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : string 代納番号

            // パラメータ不足
            if (args.Length <= 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E027", "代納番号");
                return null;
            }
            // 型チェック
            long dainoNumber;
            if (!long.TryParse(args[0].ToString(), out dainoNumber))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E027", "代納番号");
                return null;
            }
            // 範囲チェック
            if (dainoNumber <= 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E027", "代納番号");
                return null;
            }

            // 返却
            var callForm = new UIForm(WINDOW_TYPE.ICHIRAN_WINDOW_FLAG, dainoNumber);
            return new BusinessBaseForm(callForm, new UIHeader());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // パラメータ不足
            if (args.Length <= 0)
            {
                return false;
            }
            // 型チェック
            long dainoNumber;
            if (!long.TryParse(args[0].ToString(), out dainoNumber))
            {
                return false;
            }
            // 範囲チェック
            if (dainoNumber <= 0)
            {
                return false;
            }

            var businessBaseForm = form as BusinessBaseForm;
            var uiForm = businessBaseForm.inForm as UIForm;

            // 同じ代納番号のフォームなら前面表示、そうでなければ新規画面作成
            return ((dainoNumber < 0 || dainoNumber != uiForm.DainoNo) ? false : true);
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
