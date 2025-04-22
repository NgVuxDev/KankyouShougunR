using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Shougun.Core.PaperManifest.ManifestImport
{
    /// <summary>メッセージユーティリティ</summary>
    internal class MessageLogic
    {
        /// <summary>
        /// メッセージ定義リスト
        /// </summary>
        private List<MessageDto> messageList = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MessageLogic()
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            using (Stream resourceStream = thisAssembly.GetManifestResourceStream("Shougun.Core.PaperManifest.ManifestImport.MessageData.xml"))
            using (StreamReader resourceReader = new StreamReader(resourceStream))
            {
                var xDoc = XDocument.Load(resourceReader);

                this.messageList = xDoc.Element("root").Elements("Message")
                                                                 .Select(s => new MessageDto
                                                                     (
                                                                         s.Attribute("id").Value,
                                                                         s.Value.Trim().Replace(@"\n", Environment.NewLine)
                                                                     )
                                                                 ).ToList();
            }
        }

        /// <summary>IDからメッセージ定義クラスを取得</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public MessageDto GetMessage(string messageId)
        {
            return this.messageList.FirstOrDefault(s => s.Id == messageId);
        }

        /// <summary>IDからメッセージ文字列を取得</summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public string GetMessageString(string messageId)
        {
            return this.messageList.FirstOrDefault(s => s.Id == messageId).Text;
        }
    }
}
