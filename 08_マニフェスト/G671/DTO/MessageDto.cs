
namespace Shougun.Core.PaperManifest.ManifestImport
{
    /// <summary>メッセージ定義</summary>
    public class MessageDto
    {
        /// <summary>メッセージID</summary>
        public string Id { get; private set; }

        /// <summary>メッセージテキスト</summary>
        public string Text { get; private set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public MessageDto(string id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}
