using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Scale.KeiryouNyuuryoku
{
    /// <summary>
    /// G672 計量入力
    /// </summary>
    class G672 : r_framework.FormManager.IShougunForm
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

            // 引数 arg[1] : long 計量番号 -1:なし 0以上:有効な計量番号　
            long keiryouNumber = -1L;
            if (args.Length > 1 && args[1] != null)
            {
                keiryouNumber = long.TryParse(args[1].ToString(), out keiryouNumber) ? keiryouNumber : -1;
            }

            // 引数 arg[2] : 実行メソッド delegate
            Shougun.Core.Scale.KeiryouNyuuryoku.UIForm.LastRunMethod lastRunMethod = null;
            if (args.Length > 2 && args[2] != null)
            {
                lastRunMethod = (Shougun.Core.Scale.KeiryouNyuuryoku.UIForm.LastRunMethod)args[2];
            }

            // 引数 arg[3] : bool 継続計量 true:する false:しない
            bool keizokukeiryouFlg = false;
            if (args.Length > 5 && args[3] != null)
            {
                bool.TryParse(args[5].ToString(), out keizokukeiryouFlg);
            }

            // 引数 arg[4] : bool  true:する false:しない
            bool newChangeFlg = false;
            if (args.Length > 6 && args[4] != null)
            {
                bool.TryParse(args[4].ToString(), out newChangeFlg);
            }

            // 引数 arg[5] :取引先CD
            // 引数 arg[6] :業者CD
            // 引数 arg[7] :現場CD
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
                callForm = new UIForm(WINDOW_ID.T_KEIRYOU_NYUURYOKU, windowType, (string)args[7], (string)args[8], (string)args[9]);
            }
            else
            {
                // 引数 arg[10] :SQE
                string SEQ = string.Empty;
                if (args.Length > 10 && args[10] != null)
                {
                    SEQ = (string)args[10];
                }

                callForm = new UIForm(WINDOW_ID.T_KEIRYOU_NYUURYOKU, windowType, keiryouNumber, lastRunMethod, keizokukeiryouFlg, newChangeFlg, SEQ);// No.2334
                // 計量番号が使えるかチェック
                var isExistKeiryouData = callForm.IsExistKeiryouData();
                if (!isExistKeiryouData)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    callForm.Dispose();
                    return null;
                }

                //// 滞留登録された計量票用権限チェック
                //var hasAuthority = callForm.HasAuthorityTairyuu();
                //if (!hasAuthority)
                //{
                //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //    msgLogic.MessageBoxShow("E158", "新規");
                //    callForm.Dispose();
                //    return null;
                //}
            }

            return new BusinessBaseForm(callForm, new UIHeaderForm());
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <return>true：同じ false:異なる</return>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            // 同じ計量番号のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 1 && args[1] != null)
            {
                long keiryouNumber = long.Parse(args[1].ToString());
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                bool newChangeFlg = false;
                if (args.Length > 6 && args[6] != null)
                {
                    newChangeFlg = bool.Parse(args[6].ToString());
                }
                if (args[0].Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && newChangeFlg)
                {   // 滞留一覧から計量番号選択の場合で新規画面だったら本画面の計量番号を設定し使用する
                    if (keiryouNumber == -1)
                    {
                        return false;
                    }
                    if (keiryouNumber == uiForm.KeiryouNumber)
                    {
                        return true;
                    }
                    else if (uiForm.KeiryouNumber == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    return ((keiryouNumber == -1 || keiryouNumber.ToString() != uiForm.ENTRY_NUMBER.Text.Trim()) ? false : true);
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
