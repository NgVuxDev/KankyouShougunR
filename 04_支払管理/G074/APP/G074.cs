using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.APP.Base;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo
{
    /// <summary>
	/// G074 買掛金一覧表
    /// </summary>
	class G074 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var HeaderForm = new Shougun.Core.PaymentManagement.KaikakekinItiranHyo.UIHeader();
            var callForm = new Shougun.Core.PaymentManagement.KaikakekinItiranHyo.UIForm(HeaderForm);
            return new BusinessBaseForm(callForm, HeaderForm);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
			// 一覧画面のためtrueを返却
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
