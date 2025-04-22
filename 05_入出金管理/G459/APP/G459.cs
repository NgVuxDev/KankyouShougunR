using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
{
    /// <summary>
    /// G459 入金入力クラス
    /// </summary>
    class G459 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォームを作成します
        /// </summary>
        /// <param name="args">パラメータ</param>
        /// <returns>フォームインスタンス</returns>
        public Form CreateForm(params object[] args)
        {
            var headerForm = new UIHeader();
            var callForm = new UIForm(headerForm, WINDOW_TYPE.NEW_WINDOW_FLAG);

            var windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] :入金番号
            var nyuukinNumber = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                nyuukinNumber = (String)args[1];
            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG != windowType)
            {
                callForm = new UIForm(headerForm, windowType, nyuukinNumber);
            }

            //if (null == callForm)
            //{
                //データ移動モード判定
                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                    args.Length > 4 &&
                    args[1] == null
                    )
                {
                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm(headerForm, windowType, (string)args[2], (string)args[3], (string)args[4]);
                }
                else
                {
                    // 引数 arg[5] :SEQ
                    string seq = string.Empty;
                    if (args.Length > 5 && args[5] != null)
                    {
                        seq = (string)args[5];
                    }

                    callForm = new Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm(headerForm, windowType, nyuukinNumber, seq);
                }
            //}

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
                String Nyuukin_CD = (String)args[1];
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2.UIForm;
                if (uiForm.NYUUKIN_NUMBER.Text == Nyuukin_CD)
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
