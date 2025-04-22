using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku
{
    /// <summary>
    /// G121 建廃マニフェスト
    /// </summary>
    class G121 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 引数 arg[0] : 画面区分
            //WINDOW_TYPE window_type = WINDOW_TYPE.NONE;
            WINDOW_TYPE window_type = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                window_type = (WINDOW_TYPE)args[0];
            }

            // 引数 arg[1] : 連携伝達区分
            String RDentatuKbn = "";
            if (args.Length > 1 && args[1] != null)
            {
                RDentatuKbn = (String)args[1];
            }

            // 引数 arg[2] : システムID
            String SystemId = "";
            if (args.Length > 2 && args[2] != null)
            {
                SystemId = (String)args[2];
            }

            // 引数 arg[3] : 連携明細システムID
            String RMeisaiId = "";
            if (args.Length > 3 && args[3] != null)
            {
                RMeisaiId = (String)args[3];
            }

            // 引数 arg[4] : 処理モード
            //int iMode = 1;
            int iMode = (int)WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 4 && args[4] != null)
            {
                iMode = (int)args[4];
            }

            // 引数 arg[5] :取引先CD
            // 引数 arg[6] :業者CD
            // 引数 arg[7] :現場CD
            KenpaiManifestoNyuryoku callForm = null;
            //データ移動モード判定
            if (window_type == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 7 &&
                args[1] == null &&
                args[2] == null &&
                args[3] == null &&
                args[4] == null)
            {
                callForm = new Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku(window_type, RDentatuKbn, SystemId, RMeisaiId, iMode,
                                                                                                    (string)args[5], (string)args[6], (string)args[7]);
            }
            else
            {
                callForm = new Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryoku(window_type, RDentatuKbn, SystemId, RMeisaiId, iMode);
                // 20140606 katen 不具合No.4691 start‏
                if (args.Length > 8 && args[8] != null)
                {
                    // 引数 arg[8] :マニフェスト一次二次区分
                    callForm.fromManiFirstFlag = Convert.ToInt16(args[8]);
                }
                // 20140606 katen 不具合No.4691 end‏
            }
            var callHeader = new Shougun.Core.PaperManifest.KenpaiManifestoNyuryoku.KenpaiManifestoNyuryokuHeader();
            // 20140609 katen No.730 規定値機能の追加について start‏
            callHeader.form = callForm;
            // 20140609 katen No.730 規定値機能の追加について end‏
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
            // 同じ引数のフォームなら前面表示、そうでなければ新規画面作成
            if (args.Length > 4)
            {
                WINDOW_TYPE window_type = (WINDOW_TYPE)args[0];
                String RDentatuKbn = (String)args[1];
                String SystemId = (String)args[2];
                String RMeisaiId = (String)args[3];
                int iMode = (int)args[4];

                var callForm = form as BusinessBaseForm;
                var uiForm = callForm.inForm as KenpaiManifestoNyuryoku;

                if (uiForm.WindowType == window_type
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && window_type == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && window_type == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.RDentatuKbn == RDentatuKbn &&
                            uiForm.SystemId == SystemId &&
                            uiForm.RMeisaiId == RMeisaiId
                            );
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
