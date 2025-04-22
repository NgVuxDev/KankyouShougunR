// $Id: G157.cs 20980 2014-05-20 05:58:12Z j-kikuchi $
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
{
    class G157 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
			// 引数 arg[0] : 検収入力DTO
			var dto = new KenshuNyuuryokuDTOClass();
			if(args.Length > 0)
			{
				dto = (KenshuNyuuryokuDTOClass)args[0];
			}

			// Call Form
			var callHeader = new Shougun.Core.Inspection.KenshuMeisaiNyuryoku.UIHeader();
            var callForm = new Shougun.Core.Inspection.KenshuMeisaiNyuryoku.KenshuMeisaiNyuryokuForm(dto);
            
            return new BusinessBaseForm(callForm, callHeader);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 1)
            {
                string renkei_denshu_kbn_cd = (string)args[1];
                var baseForm = form as BusinessBaseForm;
                var uiForm = baseForm.inForm as KenshuMeisaiNyuryokuForm;
                return true;
            }
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
