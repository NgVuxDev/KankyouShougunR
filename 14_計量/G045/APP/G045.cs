using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;
using r_framework.APP.Base;

namespace Shougun.Core.Scale.Keiryou
{
    /// <summary>
    /// G045 計量入力
    /// </summary>
    class G045 : r_framework.FormManager.IShougunForm
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

            // 引数 arg[1] : long 計量番号 -1:なし 0以上:有効な受入番号　
            long keiryouNumber = -1L;
            if (args.Length > 1 && args[1] != null)
            {
                keiryouNumber = long.Parse(args[1].ToString());
            }


            // 引数 arg[2] : long 受付番号 -1:なし 0以上:有効な受付番号
            long uketsukeNumber = -1L;
            if (args.Length > 2 && args[2] != null)
            {
                uketsukeNumber = long.Parse(args[2].ToString());
            }


            // 引数 arg[3] : true:滞留登録,false:滞留登録なし
            bool keizokuKeiryouFlg = false;
            if (args.Length > 3 && args[3] != null)
            {
                keizokuKeiryouFlg = bool.Parse(args[3].ToString());
            }

            // No.2334-->
            // 引数 arg[4] : bool  true:する false:しない
            bool newChangeFlg = false;
            if (args.Length > 4 && args[4] != null)
            {
                bool.TryParse(args[4].ToString(), out newChangeFlg);
            }
            // No.2334<--

            // 引数 arg[5] :取引先CD
            // 引数 arg[6] :業者CD
            // 引数 arg[7] :現場CD
            // 引数 arg[8] :入出区分
            UIForm callForm = null;
            //データ移動モード判定
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 8 &&
                args[1] == null &&
                args[2] == null &&
                args[3] == null &&
                args[4] == null)
            {
                callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, (string)args[5], (string)args[6], (string)args[7], (string)args[8]);
            }
            else
            {
                // 引数 arg[9] :SQE
                string SEQ = string.Empty;
                if (args.Length > 9 && args[9] != null)
                {
                    SEQ = (string)args[9];
                }

                callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, keiryouNumber, uketsukeNumber, keizokuKeiryouFlg, newChangeFlg, null, SEQ);// No.2334
                #region 受付番号と計量番号判定
                if (keiryouNumber != -1)
                {
                    // 計量番号が使えるかチェック
                    var isExistKeiryouData = callForm.IsExistKeiryouData();
                    if (!isExistKeiryouData)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                        callForm.Dispose();
                        return null;
                    }
                }
                else if (uketsukeNumber != -1)
                {

                    // 受入番号が使えるかチェック
                    var isExistUkeireData = callForm.IsExistUketsukeData();
                    if (!isExistUkeireData)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045");
                        callForm.Dispose();
                        return null;
                    }

                }
                #endregion
            }
            return new BusinessBaseForm(callForm, new UIHeaderForm());
        }


        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateFormKeiryou(params object[] args)
        {
            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : long 計量番号 -1:なし 0以上:有効な計量番号　
            long keirouNumber = -1L;
            if (args.Length > 1)
            {
                keirouNumber = (long)args[1];
            }

            // No.2334-->
            // 引数 arg[4] : bool  true:する false:しない
            bool newChangeFlg = false;
            if (args.Length > 4 && args[4] != null)
            {
                bool.TryParse(args[4].ToString(), out newChangeFlg);
            }

            //var callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, keirouNumber, -1);
            var callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, keirouNumber, -1L, false, newChangeFlg);
            // No.2334<--

            //// 計量番号が使えるかチェック
            //var isExistKeiryouData = callForm.IsExistKeiryouData();
            //if (!isExistKeiryouData)
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E045");
            //    callForm.Dispose();
            //    return null;
            //}

            return new BusinessBaseForm(callForm, new UIHeaderForm());

        }

        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateFormUketsuke(params object[] args)
        {

            // 引数 arg[0] : WINDOW_TYPE　モード　新規/修正/削除
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                windowType = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : long 受付番号 -1:なし 0以上:有効な受付番号　
            long uketsukeNumber = -1L;
            if (args.Length > 2)
            {
                uketsukeNumber = (long)args[2];
            }

            // No.2334-->
            // 引数 arg[4] : bool  true:する false:しない
            bool newChangeFlg = false;
            if (args.Length > 4 && args[4] != null)
            {
                bool.TryParse(args[4].ToString(), out newChangeFlg);
            }

            //var callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, -1, uketsukeNumber);
            var callForm = new UIForm(WINDOW_ID.T_KEIRYO, windowType, -1, uketsukeNumber, false, newChangeFlg);
            // No.2334<--

            // 受付番号が使えるかチェック
            var isExistUketsukeData = callForm.IsExistUketsukeData();
            if (!isExistUketsukeData)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E045");
                callForm.Dispose();
                return null;
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
                long keirouNumber = long.TryParse(args[1].ToString(), out keirouNumber) ? keirouNumber : -1;
                var footerForm = form as BusinessBaseForm;
                var uiForm = footerForm.inForm as UIForm;
                // No.2334-->
                bool newChangeFlg = false;
                if (args.Length > 4 && args[4] != null)
                {
                    newChangeFlg = bool.Parse(args[4].ToString());
                }
                if (args[0].Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && newChangeFlg)
                {   // 滞留一覧から計量番号選択の場合で新規画面だったら本画面の計量番号を設定し使用する
                    if (keirouNumber == -1)
                    {
                        return false;
                    }
                    if (keirouNumber == uiForm.KeiryouNumber)
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
                    return (keirouNumber != -1 && uiForm.KEIRYOU_NUMBER.Text.Trim() == keirouNumber.ToString());
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
