using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Message
{
    public static class MessageBoxUtility
    {
        /// <summary>エラーメッセージボックス表示</summary>
        /// <param name="messageId">表示するメッセージのID</param>
        /// <param name="str">置き換えを行う表示用の文言リスト</param>
        public static DialogResult MessageBoxShow(string messageId, params string[] str)
        {
            var msg = MessageUtility.GetMessage(messageId);
            var msgStr = MessageBoxUtility.CreateErrorMessage(msg.Text, str);

            if (msg.Kubun == MESSAGE_KUBUN.CONFIRM)
            {
                return MessageBoxUtility.MessageBoxShowConfirm(msgStr);
            }
            else if (msg.Kubun == MESSAGE_KUBUN.WARN)
            {
                return MessageBoxUtility.MessageBoxShowWarn(msgStr);
            }
            else if (msg.Kubun == Shougun.Core.Message.MESSAGE_KUBUN.ERROR)
            {
                return MessageBoxUtility.MessageBoxShowError(msgStr);
            }
            else if (msg.Kubun == Shougun.Core.Message.MESSAGE_KUBUN.INFO)
            {
                return MessageBoxUtility.MessageBoxShowInformation(msgStr);
            }

            return DialogResult.None;
        }

        /// <summary>エラーメッセージボックス表示</summary>
        /// <param name="messageId">表示するメッセージのID</param>
        /// <param name="defaultBtn">デフォルトボタン</param>
        /// <param name="str">置き換えを行う表示用の文言リスト</param>
        public static DialogResult MessageBoxShow(string messageId, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1, params string[] str)
        {
            var msg = MessageUtility.GetMessage(messageId);
            var msgStr = MessageBoxUtility.CreateErrorMessage(msg.Text, str);

            if (msg.Kubun == MESSAGE_KUBUN.CONFIRM)
            {
                return MessageBoxUtility.MessageBoxShowConfirm(msgStr, defaultBtn);
            }

            return DialogResult.None;
        }

        /// <summary>確認用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <param name="defaultBtn">既定のボタン。未指定はButton1</param>
        /// <returns></returns>
        public static DialogResult MessageBoxShowConfirm(string msgStr, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(msgStr, MESSAGE_KUBUN.CONFIRM.ToKubunString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultBtn);
        }

        /// <summary>警告用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public static DialogResult MessageBoxShowWarn(string msgStr)
        {
            return MessageBox.Show(msgStr, MESSAGE_KUBUN.WARN.ToKubunString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>エラー(アラート)用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public static DialogResult MessageBoxShowError(string msgStr)
        {
            return MessageBox.Show(msgStr, MESSAGE_KUBUN.ERROR.ToKubunString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>情報用メッセージダイアログ</summary>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public static DialogResult MessageBoxShowInformation(string msgStr)
        {
            return MessageBox.Show(msgStr, MESSAGE_KUBUN.INFO.ToKubunString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// メッセージのformat形成
        /// </summary>
        /// <param name="errorMessage">メッセージ</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private static string CreateErrorMessage(string errorMessage, params string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                errorMessage = errorMessage.Replace("{" + i + "}", str[i]);
            }

            return errorMessage;
        }
    }
}
