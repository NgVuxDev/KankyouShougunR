using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Common.KensakuKekkaIchiran
{
    class G176 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : 検索文字列
            string searchString = string.Empty;

            if (args.Length > 0)
            {
                searchString = args[0].ToString();
            }

            var callForm = new Shougun.Core.Common.KensakuKekkaIchiran.UIForm(DENSHU_KBN.KENSAKU_KEKKA, searchString);
            var HeaderForm = new Shougun.Core.Common.KensakuKekkaIchiran.HeaderForm();
            var businessForm = new BusinessBaseForm(callForm, HeaderForm);

            return businessForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 引数 arg[0] : 検索文字列
            string searchString = string.Empty;

            if (args.Length > 0)
            {
                searchString = args[0].ToString();
            }

            var baseForm = (BusinessBaseForm)form;
            var uiForm = (UIForm)baseForm.inForm;
            return uiForm.SearchString[0] == searchString && uiForm.SearchString[1] == searchString;
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