using System;
using System.Windows.Forms;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.Common.IchiranSyu
{
    /// <summary>
    /// G187 一覧項目選択
    /// </summary>
    class G187 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : 伝種区分
            String DenshuKbn = "";
            if (args.Length > 0)
            {
                DenshuKbn = (String)args[0];
            }

            // 引数 arg[1] : システムID　
            String SystemID = "";
            if (args.Length > 1)
            {
                SystemID = (String)args[1];
            }

            var callForm = new Shougun.Core.Common.IchiranSyu.UIForm(DenshuKbn, SystemID);
            var callHeader = new Shougun.Core.Common.IchiranSyu.UIHeader();
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
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                String SystemID = (String)args[1];
                var footerForm = form as BasePopForm;
                var uiForm = footerForm.inForm as UIForm;
                return (uiForm.systemID == SystemID);
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
