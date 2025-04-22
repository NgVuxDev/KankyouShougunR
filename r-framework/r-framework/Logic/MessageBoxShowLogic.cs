using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Message;

namespace r_framework.Logic
{
    /// <summary>
    /// 画面上に表示するメッセージボックスを
    /// メッセージIDから検索し表示する処理
    /// </summary>
    public class MessageBoxShowLogic
    {
        /// <summary>
        /// エラーメッセージボックス表示
        /// </summary>
        /// <param name="messageId">表示するメッセージのID</param>
        /// <param name="str">置き換えを行う表示用の文言リスト</param>
        public DialogResult MessageBoxShow(string messageId, params string[] str)
        {
            return MessageBoxUtility.MessageBoxShow(messageId, str);
        }

        /// <summary>
        /// エラーメッセージボックス表示
        /// </summary>
        /// <param name="messageId">表示するメッセージのID</param>
        /// <param name="defaultBtn">デフォルトボタン</param>
        /// <param name="str">置き換えを行う表示用の文言リスト</param>
        public DialogResult MessageBoxShow(string messageId, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1, params string[] str)
        {
            return MessageBoxUtility.MessageBoxShow(messageId, defaultBtn, str);
        }

        /// <summary>確認用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowConfirm(string msgStr, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1)
        {
            return MessageBoxUtility.MessageBoxShowConfirm(msgStr, defaultBtn);
        }

        /// <summary>警告用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowWarn(string msgStr)
        {
            return MessageBoxUtility.MessageBoxShowWarn(msgStr);
        }

        /// <summary>エラー(アラート)用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowError(string msgStr)
        {
            return MessageBoxUtility.MessageBoxShowError(msgStr);
        }

        /// <summary>情報用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowInformation(string msgStr)
        {
            return MessageBoxUtility.MessageBoxShowInformation(msgStr);
        }
    }
}
