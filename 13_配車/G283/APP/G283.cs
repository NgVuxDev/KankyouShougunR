using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.APP
{
    /// <summary>
    /// G283 モバイル将軍データ取込
    /// </summary>
    class G283 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            //// 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            //WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            //if (args.Length > 0)
            //{
            //    windowType = (WINDOW_TYPE)args[0];
            //}
            var callForm = new UIForm();
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
