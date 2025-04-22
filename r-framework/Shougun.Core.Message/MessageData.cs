using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Message
{
    /// <summary>メッセージ定義</summary>
    public class MessageData
    {
        /// <summary>メッセージID</summary>
        public string Id { get; private set; }

        /// <summary>メッセージ区分</summary>
        public MESSAGE_KUBUN Kubun { get; private set; }

        /// <summary>メッセージテキスト</summary>
        public string Text { get; private set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="id"></param>
        /// <param name="kubun"></param>
        /// <param name="text"></param>
        public MessageData(string id, int kubun, string text)
        {
            this.Id = id;
            this.Kubun = (MESSAGE_KUBUN)kubun;
            this.Text = text;
        }
    }
}
