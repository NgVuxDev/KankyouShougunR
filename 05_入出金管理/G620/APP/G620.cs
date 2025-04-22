using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiShusei
{
    /// <summary>
    /// G620 入金消込
    /// </summary>
    class G620 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            var windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            string TORIHIKISAKI_CD = string.Empty;
            string TORIHIKISAKI_NAME = string.Empty;
            string SEIKYUU_DATE_TO = string.Empty;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            if (args.Length > 1
                && args[1] != null)
            {
                SEIKYUU_DATE_TO = (String)args[1];
            }

            if (args.Length > 2 && args[2] != null)
            {
                TORIHIKISAKI_CD = (String)args[2];
            }

            if (args.Length > 3 && args[3] != null)
            {
                TORIHIKISAKI_NAME = (String)args[3];
            }

            var nyuukinNumber = string.Empty;
            if (args.Length > 4 && args[4] != null)
            {
                nyuukinNumber = (String)args[4];
            }

            var HeaderForm = new HeaderBaseForm();
            var callForm = new UIForm(windowType, SEIKYUU_DATE_TO, TORIHIKISAKI_CD, TORIHIKISAKI_NAME, nyuukinNumber);
            var popupForm = new BusinessBaseForm(callForm, HeaderForm);

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
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form">表示を更新するフォーム</param>
        public void UpdateForm(Form form)
        {
        }
    }
}
