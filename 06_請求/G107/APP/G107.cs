using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.APP.Base;

namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    /// <summary>
	/// G107 請求書発行
    /// </summary>
	class G107 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            DTOClass dto = new DTOClass();
            if (args != null && args.Length > 0)
            {
                // 拠点CD
                dto.InitKyotenCd = args[0].ToString();
                // 締日
                dto.InitShimebi = args[1].ToString();
                // 取引先CD
                dto.InitTorihiksiakiCd = args[2].ToString();
                // 伝票日付
                dto.InitDenpyouHiduke = args[3].ToString();
            }
            else
            {
                dto = null;
            }

            var HeaderForm = new Shougun.Core.Billing.SeikyuushoHakkou.APP.HeaderSeikyuushoHakkou();

            if (dto != null)
            {
                var callForm = new Shougun.Core.Billing.SeikyuushoHakkou.UIForm(HeaderForm, dto);
                var bbf = new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true };
                return bbf;
            }
            else
            {
                var callForm = new Shougun.Core.Billing.SeikyuushoHakkou.UIForm(HeaderForm);
                var bbf = new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true };
                return bbf;
            }
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
