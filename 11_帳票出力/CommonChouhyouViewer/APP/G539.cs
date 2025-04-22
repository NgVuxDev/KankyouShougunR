using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;
using CommonChouhyouPopup.App;

namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
{
    /// <summary>
    /// G539 計量集計表/一覧
    /// </summary>
    class G539 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] :
            //if (args.Length > 0)
            //{
            //    windowType = (WINDOW_TYPE)args[0];
            //}

            //// 引数 arg[1] : 受付番号
            //string uketsukeNumber = string.Empty;
            //if (args.Length > 1)
            //{
            //    uketsukeNumber = (string)args[1];
            //}
            var hearfrom = new UIHeaderForm();
            var callForm = new UIFormG539(hearfrom, WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU);
            callForm.ReportInfo = (ReportInfoBase)args[0];
            //return new BusinessBaseForm(callForm, hearfrom);
            return new ReportBaseForm(callForm, hearfrom);
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
            //if (args.Length > 1)
            //{
            //    return true;
            //}
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
