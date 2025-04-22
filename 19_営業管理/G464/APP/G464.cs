using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;
using Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku;
namespace Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku
{
    /// <summary>
    /// G464 見積入力
    /// </summary>
    class G464 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            var headerForm = new Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku.HeaderForm();
            var callForm = new Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku.JuchuuMokuhyouForm(headerForm);

            return new BusinessBaseForm(callForm, headerForm);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ見積番号のフォームなら前面表示、そうでなければ新規画面作成
            //if (args.Length > 1)
            //{
            //    long mitsumoriNumber = (long)args[1];
            //    var footerForm = form as BusinessBaseForm;
            //    var uiForm = footerForm.inForm as JuchuuMokuhyouForm;
            //    return (uiForm.mitsumoriNumber == mitsumoriNumber);
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
