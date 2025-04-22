using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Shougun.Core.Message
{
    /// <summary>メッセージユーティリティ</summary>
    public static class MessageUtility
    {
        /// <summary>
        /// メッセージ定義リスト
        /// </summary>
        private static List<MessageData> messageList = null;

        /// <summary>
        /// JWNET通信エラーメッセージ定義リスト
        /// </summary>
        private static List<JwnetErrorMessageData> jwnetErrorMessageList = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static MessageUtility()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            using (Stream resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Core.Message.MessageData.xml"))
            using (StreamReader resourceReader = new StreamReader(resourceStream))
            {
                var xDoc = XDocument.Load(resourceReader);

                MessageUtility.messageList = xDoc.Element("root").Elements("Category").Elements("Message")
                                                                 .Select(s => new MessageData
                                                                     (
                                                                         s.Attribute("id").Value,
                                                                         Convert.ToInt16(s.Attribute("kubun").Value),
                                                                         s.Value.Trim().Replace(@"\n", Environment.NewLine)
                                                                     )
                                                                 ).ToList();
            }

            if (MessageUtility.messageList == null || MessageUtility.messageList.Count == 0)
            {
                MessageBoxUtility.MessageBoxShowError("メッセージ定義が正しくありません。");
            }


            using (Stream jwnetResourceStream = thisAssembly.GetManifestResourceStream("Shougun.Core.Message.JwnetErrorMessageData.xml"))
            using (StreamReader jwnetResourceReader = new StreamReader(jwnetResourceStream))
            {
                var xDocJwnetMessage = XDocument.Load(jwnetResourceReader);

                MessageUtility.jwnetErrorMessageList = xDocJwnetMessage.Element("root").Elements("Category").Elements("Message")
                                                                 .Select(s => new JwnetErrorMessageData
                                                                     (
                                                                         s.Attribute("id").Value,
                                                                         s.Value.Trim().Replace(@"\n", Environment.NewLine)
                                                                     )
                                                                 ).ToList();
            }

            if (MessageUtility.jwnetErrorMessageList == null || MessageUtility.jwnetErrorMessageList.Count == 0)
            {
                MessageBoxUtility.MessageBoxShowError("JWNETエラーメッセージ定義が正しくありません。");
            }
        }

        /// <summary>IDからメッセージ定義クラスを取得</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static MessageData GetMessage(string messageId)
        {
            return MessageUtility.messageList.FirstOrDefault(s => s.Id == messageId);
        }

        /// <summary>IDからメッセージ文字列を取得</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static string GetMessageString(string messageId)
        {
            return MessageUtility.messageList.FirstOrDefault(s => s.Id == messageId).Text;
        }

        /// <summary>IDからメッセージ定義クラスを取得(JWNETエラーメッセージ)</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static JwnetErrorMessageData GetJwnetErrorMessage(string messageId)
        {
            return MessageUtility.jwnetErrorMessageList.FirstOrDefault(s => s.Id == messageId);
        }

        /// <summary>IDからメッセージ文字列を取得(JWNETエラーメッセージ)</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static string GetJwnetErrorMessageString(string messageId)
        {
            string returnVal = string.Empty;

            var jwnetErrorData = MessageUtility.jwnetErrorMessageList.FirstOrDefault(s => s.Id == messageId);
            if (jwnetErrorData != null
                && !string.IsNullOrEmpty(jwnetErrorData.Id))
            {
                returnVal = jwnetErrorData.Text;
            }
            return returnVal;
        }
    }
}
