using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;
using Shougun.Core.BusinessManagement.Const.Common;
namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
{
    /// <summary>
    /// G276 見積入力
    /// </summary>
    class G276 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数遷移先パラメータ設定dto
            // 引数 arg[1] :Dao
            FormShowParamDao formParem = new FormShowParamDao();
            if (args.Length > 1 && args[1] != null)
            {
                formParem = (FormShowParamDao)(args[1]);
            }

            // 引数 arg[2] :取引先CD
            // 引数 arg[3] :業者CD
            // 引数 arg[4] :現場CD
            MitsumoriNyuryokuForm callForm = null;
            //データ移動モード判定
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 4 &&
                args[1] == null)
            {
                callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryokuForm(WINDOW_ID.T_MITSUMORI_NYUURYOKU, windowType, formParem, null,
                                                                                                    (string)args[2], (string)args[3], (string)args[4]);
            }
            else
            {
                callForm = new Shougun.Core.BusinessManagement.MitsumoriNyuryoku.MitsumoriNyuryokuForm(WINDOW_ID.T_MITSUMORI_NYUURYOKU, windowType, formParem);
                #region 見積番号が使えるかチェック

                // 複写の場合
                if (!string.IsNullOrEmpty(formParem.mitsumoriNumber) && windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    windowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    callForm.copyDataFlg = true;
                }

                if (!windowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 見積番号が使えるかチェック
                    var isExistkeireData = callForm.IsExistMitsumoriData();
                    if (!isExistkeireData)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                        callForm.Dispose();
                        return null;
                    }
                }
                #endregion
            }
            return new BusinessBaseForm(callForm, new HeaderForm());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ見積番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1)
            {
                // 引数遷移先パラメータ設定dto
                FormShowParamDao formParem = new FormShowParamDao();

                if (args[1] != null)
                {
                    formParem = (FormShowParamDao)(args[1]);

                    if (!string.IsNullOrEmpty(formParem.mitsumoriNumber))
                    {
                        long mitsumoriNumber = long.Parse(formParem.mitsumoriNumber);
                        var footerForm = form as BusinessBaseForm;
                        var uiForm = footerForm.inForm as MitsumoriNyuryokuForm;
                        return (uiForm.mitsumoriNumber == mitsumoriNumber);

                    }
                }



            }
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
