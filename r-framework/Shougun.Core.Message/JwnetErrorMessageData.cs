using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Message
{
    /// <summary>JWNET通信エラーメッセージ定義</summary>
    public class JwnetErrorMessageData
    {
        /// <summary>メッセージID</summary>
        public string Id { get; private set; }

        /// <summary>メッセージテキスト</summary>
        public string Text { get; private set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="id"></param>
        /// <param name="kubun"></param>
        /// <param name="text"></param>
        public JwnetErrorMessageData(string id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
