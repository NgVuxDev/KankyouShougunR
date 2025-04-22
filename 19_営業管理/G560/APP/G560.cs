using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku
{
    /// <summary>
    /// G560 申請内容選択入力
    /// </summary>
    class G560 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new HeaderBaseForm();
            var callForm = new Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.UIForm();

            return new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true };
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
            // 一覧を更新する
            // ※現在、各「引合」マスタの申請後のみ呼ばれる
            BusinessBaseForm businessBaseForm = form as BusinessBaseForm;
            if (businessBaseForm != null)
            {
                Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.UIForm mainForm = businessBaseForm.inForm as Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku.UIForm;
                if (mainForm != null)
                {
                    mainForm.Search();
                }
            }
        }
    
    }
}
