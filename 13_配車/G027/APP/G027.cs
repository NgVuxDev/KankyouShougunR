using System;
using System.Data;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using r_framework.Entity;

namespace Shougun.Core.Allocation.SagyoubiHenkou
{
    /// <summary>
    /// G027 作業日変更
    /// </summary>
    public class G027 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var callForm = new UIForm((string)args[0], (string)args[1], (string)args[2], (string)args[3], (DataRow)args[4], (string)args[5], (bool)args[6], (M_SYS_INFO)args[7], (string)args[8], (string)args[9]);
            var callHeadForm = new HeaderForm();

            BasePopForm popupForm = new BasePopForm(callForm, callHeadForm);
            popupForm.StartPosition = FormStartPosition.CenterScreen;
            return popupForm;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
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
