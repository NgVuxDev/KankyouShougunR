using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiShobun
{
    /// <summary>
    /// G607 実績報告書修正（運搬実績）
    /// </summary>
    class G607 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // モード
            WINDOW_TYPE windowType = (WINDOW_TYPE)args[0];
            string systemid = args[1].ToString();
            var headerForm = new Shougun.Core.PaperManifest.JissekiHokokuSyuseiShobun.HeaderForm();
            // 実績報告書種類
            WINDOW_ID windowId = WINDOW_ID.T_JISSEKIHOKOKU_SYUSEI_3;

            var callForm = new Shougun.Core.PaperManifest.JissekiHokokuSyuseiShobun.JissekiHokokuSyuseiShobunForm(headerForm, windowId, windowType, systemid);

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
            // モード
            WINDOW_TYPE windowType = (WINDOW_TYPE)args[0];
            string systemid = args[1].ToString();
            var businessForm = form as BusinessBaseForm;
            var uiForm = businessForm.inForm as JissekiHokokuSyuseiShobunForm;

            // 実績報告書種類
            WINDOW_ID windowId = WINDOW_ID.T_JISSEKIHOKOKU_SYUSEI_3;

            return (uiForm.WindowId == windowId
                && uiForm.WindowType == windowType
                && uiForm.systemid == systemid);
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