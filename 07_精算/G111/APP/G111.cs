using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Adjustment.Shiharaimeisaishokakunin
{
    /// <summary>
    /// G111 支払明細書確認
    /// </summary>
    class G111 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : string 精算番号
            string sNumber = string.Empty;
            if (args.Length > 0)
            {
                sNumber = args[0].ToString();
            }

            // 引数 arg[1] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 1)
            {
                windowType = (WINDOW_TYPE)args[1];
            }

            var HeaderForm = new Shougun.Core.Adjustment.Shiharaimeisaishokakunin.UIHeader();
            var callForm = new Shougun.Core.Adjustment.Shiharaimeisaishokakunin.UIForm(sNumber, windowType);
            //精算伝票が削除済みでないかをチェック
            var isExistSeisanData = callForm.IsExistSeisanData();
            if (!isExistSeisanData)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                callForm.Dispose();
                return null;
            }

            return new BusinessBaseForm(callForm, HeaderForm) { IsInFormResizable = true }; ;
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            bool ret = false;

            // 同じ精算番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 0)
            {
                string seisanNumber = args[0].ToString();
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;

                ret = ((seisanNumber == "-1" || seisanNumber != uiForm.SeisanNumber) ? false : true);

            }
            return ret;
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
