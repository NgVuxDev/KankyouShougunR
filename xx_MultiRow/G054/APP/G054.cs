using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    /// <summary>
    /// G054 売上/支払入力
    /// </summary>
    class G054 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数がそのまま渡される</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0 && args[0] != null)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : long 売上/出荷番号 -1:なし 0以上:有効な出荷番号
            long urShNumber = -1L;
            if (args.Length > 1 && args[1] != null)
            {
                urShNumber = long.TryParse(args[1].ToString(), out urShNumber) ? urShNumber : -1;
            }

            // 引数 arg[2] : 実行メソッド delegate
            Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.UIForm.LastRunMethod lastRunMethod = null;
            if (args.Length > 2 && args[2] != null)
            {
                lastRunMethod = (Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.UIForm.LastRunMethod)args[2];
            }

            // 引数 arg[3] : long 受付番号 -1:なし 0以上:有効な受付番号
            long uketsukeNumber = -1L;
            if (args.Length > 3 && args[3] != null)
            {
                uketsukeNumber = long.TryParse(args[3].ToString(), out uketsukeNumber) ? uketsukeNumber : -1;
            }

            // 引数 arg[4] :取引先CD
            // 引数 arg[5] :業者CD
            // 引数 arg[6] :現場CD
            UIForm callForm = null;
            //データ移動モード判定
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 6 &&
                args[1] == null &&
                args[2] == null &&
                args[3] == null)
            {
                callForm = new UIForm(WINDOW_ID.T_URIAGE_SHIHARAI, windowType, (string)args[4], (string)args[5], (string)args[6]);
            }
            else
            {
                // 引数 arg[7] :SQE
                string SEQ = string.Empty;
                if (args.Length > 7 && args[7] != null)
                {
                    SEQ = (string)args[7];
                }

                callForm = new UIForm(WINDOW_ID.T_URIAGE_SHIHARAI, windowType, urShNumber, lastRunMethod, uketsukeNumber, -1, SEQ);
                // 受入番号が使えるかチェック
                var isExistUkeireData = callForm.IsExistUrShData();
                if (!isExistUkeireData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    return null;
                }
            }
            return new BusinessBaseForm(callForm, new UIHeaderForm());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="Form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ売上/出荷番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1 && args[1] != null)
            {
                long urShNumber = long.Parse(args[1].ToString());
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                return ((urShNumber == -1 || urShNumber.ToString() != uiForm.ENTRY_NUMBER.Text.Trim()) ? false : true);
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
