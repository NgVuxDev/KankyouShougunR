
using System;
using System.ComponentModel;
using r_framework.Converter;
namespace r_framework.Dto
{
    [Serializable]
    public class SelectRunCheckDto
    {
        /// <summary>
        /// チェック対象の名称（日本語）
        /// </summary>
        [Category("チェック実施メソッド指定")]
        [TypeConverter(typeof(RunCheckMethodConverter))]
        public string CheckMethodName { get; set; }

        /// <summary>
        /// チェッククラスへ送信するコントロール
        /// </summary>
        [Category("送信対象コントロール指定")]
        public string[] SendParams { get; set; }

        /// <summary>
        /// チェッククラスへ送信するコントロール
        /// </summary>
        [Category("判定条件")]
        public string[] Condition { get; set; }

        [Category("起動処理区分設定")]
        public bool NewWinUnChecked { get; set; }

        [Category("起動処理区分設定")]
        public bool DelWinUnChecked { get; set; }

        [Category("起動処理区分設定")]
        public bool UpdWinUnChecked { get; set; }

        [Category("起動処理区分設定")]
        public bool IchiranWinUnChecked { get; set; }

        [Category("起動画面区分設定")]
        public bool NewProcessUnChecked { get; set; }

        [Category("起動画面区分設定")]
        public bool DelProcessUnChecked { get; set; }

        [Category("起動画面区分設定")]
        public bool UpdProcessUnChecked { get; set; }

        [Category("起動画面区分設定")]
        public bool IchiranProcessUnChecked { get; set; }
    }
}
