using System;
using r_framework.Dao;
using r_framework.Entity;

namespace r_framework.Utility
{
    /// <summary>
    /// メッセージ読み込みクラス
    /// </summary>
    [Obsolete("Shougun.Core.Message.MessageUtilityを使用してください")]
    public class MessageUtility
    {
        /// <summary>
        /// マスタからメッセージを取得する処理
        /// </summary>
        public M_ERROR_MESSAGE GetMessage(string messageId)
        {
            var msg = Shougun.Core.Message.MessageUtility.GetMessage(messageId);
            if (msg == null)
            {
                throw new Exception(String.Format("messageId：{0}が見つかりません。", messageId));
            }

            var message = new M_ERROR_MESSAGE()
            {
                MESSAGE_ID = msg.Id,
                MESSAGE_KUBUN = (System.Data.SqlTypes.SqlInt16)((int)msg.Kubun),
                MESSAGE = msg.Text
            };
            return message;
        }

    }
}