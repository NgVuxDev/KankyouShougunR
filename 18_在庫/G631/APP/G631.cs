using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.Stock.ZaikoIdou
{
    /// <summary>
    /// G631 入金入力（取引先）クラス
    /// </summary>
    class G631 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォームを作成します
        /// </summary>
        /// <param name="args">パラメータ</param>
        /// <returns>フォームインスタンス</returns>
        public Form CreateForm(params object[] args)
        {
            var headerForm = new UIHeader();

            var windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] :移動番号
            var idouNumber = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                idouNumber = (String)args[1];
            }
            
            if (WINDOW_TYPE.NEW_WINDOW_FLAG != windowType)
            {
                windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
            }

            var callForm = new UIForm(headerForm, windowType, idouNumber);
            var baseForm = new BusinessBaseForm(callForm, headerForm);

            return baseForm;
        }

        /// <summary>
        /// 同じフォームが開かれていないかをチェックします
        /// </summary>
        /// <param name="form">開いているフォーム</param>
        /// <param name="args">パラメータ</param>
        /// <returns>問い合わせ結果</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            if (args.Length > 1)
            {
                String IDOU_NUMBER = (String)args[1];
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as Shougun.Core.Stock.ZaikoIdou.UIForm;
                if (uiForm.IDOU_NUMBER.Text == IDOU_NUMBER)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateForm(Form form)
        {
        }
    }
}
