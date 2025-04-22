using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// G141 マニフェスト一覧
    /// </summary>
    class G141 : r_framework.FormManager.IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            //引数 arg[0] : 画面区分
            WINDOW_TYPE paramInMode = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (args.Length > 0)
            {
                paramInMode = (WINDOW_TYPE)args[0];
            }
            //引数 arg[1] : 管理ID
            String paramInKanriId = string.Empty;
            if (args.Length > 1 && args[1] != null)
            {
                paramInKanriId = (String)args[1];
            }
            //引数 arg[2] : SEQ
            String paramInSeq = string.Empty;
            if (args.Length > 2 && args[2] != null)
            {
                paramInSeq = (String)args[2];
            }
            UIForm callForm = null;
            //データ移動モード判定
            if (paramInMode == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                args.Length > 5 &&
                args[1] == null &&
                args[2] == null)
            {
                // 引数 arg[3] :取引先CD
                // 引数 arg[4] :業者CD
                // 引数 arg[5] :現場CD
                callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(paramInMode,
                            paramInKanriId, paramInSeq, (string)args[3], (string)args[4], (string)args[5]);
            }
            else if (paramInMode == WINDOW_TYPE.REFERENCE_WINDOW_FLAG &&
                args.Length == 7 &&
                args[1] != null &&
                args[2] != null &&
                args[3] != null &&
                args[3].ToString() == "TUUCHI_RIREKI") 
            {
                // 引数 arg[3] :APPROVAL_SEQ  承認
                // 引数 arg[4] :LATEST_SEQ    最新
                callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(paramInMode,
                            paramInKanriId, paramInSeq, (string)args[4], (string)args[5], (string)args[6], true);
            }
            else
            {
                callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(paramInMode,
                            paramInKanriId, paramInSeq);
                // 20140606 katen 不具合No.4691 start‏
                if (args.Length > 6 && args[6] != null)
                {
                    // 引数 arg[8] :マニフェスト一次二次区分
                    callForm.fromManiFirstFlag = Convert.ToInt16(args[6]);
                }
                // 20140606 katen 不具合No.4691 end‏
                if (args.Length > 7 && args[7] != null)
                {
                    //マニ一覧からの、保留/JWNETエラーのデータを開けるのにも使用
                    callForm.isOpenG142 = (bool)args[7];
                }
            }
            var callHeader = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIHeader();
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
            if (args.Length > 2)
            {
                //引数 arg[0] : 画面区分
                WINDOW_TYPE paramInMode = (WINDOW_TYPE)args[0];
                //管理ID
                String paramInKanriId = (String)args[1];
                //SEQ
                String paramInSeq = (String)args[2];

                var callForm = form as BusinessBaseForm;
                var uiForm = callForm.inForm as UIForm;

                if (uiForm.WindowType == paramInMode
                    || (uiForm.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && paramInMode == WINDOW_TYPE.DELETE_WINDOW_FLAG)
                    || (uiForm.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG && paramInMode == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    return (uiForm.paramInKanriId == paramInKanriId &&
                            uiForm.paramInSeq == paramInSeq
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
