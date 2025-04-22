using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.SmsNyuuryoku
{
    /// <summary>
    /// G767 ショートメッセージ入力
    /// </summary>
    class G767 : IShougunForm
    {
        /// <summary>
        /// フォーム作成
        /// </summary>
        /// <param name=args>SgFormManager.OpenForm()の可変引数</param>
        /// <return>作成したフォーム。失敗時はnull</return>
        public Form CreateForm(params object[] args)
        {
            // 1.ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト
            List<int> smsReceiverList = null;

            if (0 < args.Length)
            {
                smsReceiverList = (List<int>)args[0];
            }
            
            // 2.画面区分
            WINDOW_TYPE windowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (1 < args.Length)
            {
                windowType = (WINDOW_TYPE)args[1];
            }

            // 3.遷移元画面からのパラメータWindows_ID
            WINDOW_ID windowId = WINDOW_ID.NONE;
            if (2 < args.Length)
            {
                windowId = (WINDOW_ID)args[2];
            }

            // 4.呼び出し元画面の情報
            string[] paramList = null;
            if (3 < args.Length)
            {
                paramList = (string[])args[3];
            }

            // 5.システムID（ｼｮｰﾄﾒｯｾｰｼﾞ）
            string systemId = null;
            if (4 < args.Length)
            {
                systemId = (string)args[4];
            }

            UIForm callForm = null;
            var callHeader = new UIHeader();
            if (3 < args.Length)
            {
                callForm = new UIForm(smsReceiverList, windowType, windowId, paramList, systemId);
            }
            else
            {
                callForm = new UIForm();
            }
            return new BusinessBaseForm(callForm, callHeader);
        }

        /// <summary>
        /// 同内容フォーム問い合わせ
        /// </summary>
        /// <param name="form">現在表示されている画面</param>
        /// <param name="args">表示を要求されたSgFormManager.OpenForm()の可変引数</param>
        /// <returns>true：同じ false:異なる</returns>
        public bool IsSameContentForm(Form form, params object[] args)
        {
            return true;
        }

        /// <summary>
        /// フォーム更新
        /// </summary>
        /// <param name="form"></param>
        /// リスト表示や他の画面で変更される内容を表示している場合は最新の情報を表示すること。
        public void UpdateForm(Form form)
        {
        }
    }
}
