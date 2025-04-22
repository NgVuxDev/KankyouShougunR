using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku2
{
    /// <summary>
    /// G721 受入入力
    /// </summary>
    class G721 : r_framework.FormManager.IShougunForm
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
            if (args.Length > 0 && args[0] != null)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : long 受入番号 -1:なし 0以上:有効な受入番号　
            long ukeireNumber = -1L;
            if (args.Length > 1 && args[1] != null)
            {
                ukeireNumber = long.TryParse(args[1].ToString(), out ukeireNumber) ? ukeireNumber : -1;
            }

            // 引数 arg[2] : 実行メソッド delegate
            Shougun.Core.SalesPayment.UkeireNyuuryoku2.UIForm.LastRunMethod lastRunMethod = null;
            if (args.Length > 2 && args[2] != null)
            {
                lastRunMethod = (Shougun.Core.SalesPayment.UkeireNyuuryoku2.UIForm.LastRunMethod)args[2];
            }

            // 引数 arg[3] : long 受付番号 -1:なし 0以上:有効な受付番号
            long uketsukeNumber = -1L;
            if (args.Length > 3 && args[3] != null)
            {
                uketsukeNumber = long.TryParse(args[3].ToString(), out uketsukeNumber) ? uketsukeNumber : -1;
            }

            // 引数 arg[4] : long 計量番号 -1:なし 0以上:有効な計量番号
            long keiryouNumber = -1L;
            if (args.Length > 4 && args[4] != null)
            {
                keiryouNumber = long.TryParse(args[4].ToString(), out keiryouNumber) ? keiryouNumber : -1;
            }

            // 引数 arg[5] : bool 継続計量 true:する false:しない
            bool keizokukeiryouFlg = false;
            if (args.Length > 5 && args[5] != null)
            {
                bool.TryParse(args[5].ToString(), out keizokukeiryouFlg);
            }

            // No.2334-->
            // 引数 arg[6] : bool  true:する false:しない
            bool newChangeFlg = false;
            if (args.Length > 6 && args[6] != null)
            {
                bool.TryParse(args[6].ToString(), out newChangeFlg);
            }
            // No.2334<--

            // 引数 arg[7] :取引先CD
            // 引数 arg[8] :業者CD
            // 引数 arg[9] :現場CD
            UIForm callForm = null;
            //データ移動モード判定
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 9 &&
                args[1] == null &&
                args[2] == null &&
                args[3] == null &&
                args[4] == null &&
                args[5] == null &&
                args[6] == null)
            {
                callForm = new UIForm(WINDOW_ID.T_UKEIRE, windowType, (string)args[7], (string)args[8], (string)args[9]);
            }
            else
            {
                // 引数 arg[10] :SQE
                string SEQ = string.Empty;
                if (args.Length > 10 && args[10] != null)
                {
                    SEQ = (string)args[10];
                }

                callForm = new UIForm(WINDOW_ID.T_UKEIRE, windowType, ukeireNumber, lastRunMethod, uketsukeNumber, keiryouNumber, keizokukeiryouFlg, newChangeFlg, SEQ);// No.2334
                // 受入番号が使えるかチェック
                var isExistUkeireData = callForm.IsExistUkeireData();
                if (!isExistUkeireData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    callForm.Dispose();
                    return null;
                }

                // 滞留登録された受入伝票用権限チェック
                var hasAuthority = callForm.HasAuthorityTairyuu();
                if (!hasAuthority)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E158", "新規");
                    callForm.Dispose();
                    return null;
                }
            }

            return new BusinessBaseForm(callForm, new UIHeaderForm(callForm));
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ受入番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1 && args[1] != null)
            {
                long ukeireNumber = long.Parse(args[1].ToString());
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                // No.2334-->
                bool newChangeFlg = false;
                if (args.Length > 6 && args[6] != null)
                {
                    newChangeFlg = bool.Parse(args[6].ToString());
                }
                if (args[0].Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && newChangeFlg)
                {   // 滞留一覧から受入番号選択の場合で新規画面だったら本画面の受入番号を設定し使用する
                    if (ukeireNumber == -1)
                    {
                        return false;
                    }
                    if (ukeireNumber == uiForm.UkeireNumber)
                    {
                        return true;
                    }
                    else if (uiForm.UkeireNumber == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    return ((ukeireNumber == -1 || ukeireNumber.ToString() != uiForm.ENTRY_NUMBER.Text.Trim()) ? false : true);
                }
                // No.2334<--
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
