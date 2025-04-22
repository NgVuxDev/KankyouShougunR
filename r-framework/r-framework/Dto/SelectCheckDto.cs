
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using r_framework.Converter;
using r_framework.Editor;
namespace r_framework.Dto
{
    /// <summary>
    /// プロパティにてプルダウン表示を行うためのDto
    /// </summary>
    [Serializable]
    public class SelectCheckDto
    {

        /// <summary>Initializes a new instance of the <see cref="SelectCheckDto"/> class.</summary>
        public SelectCheckDto()
        {
            if (this.RunCheckMethod == null)
            {
                this.RunCheckMethod = new Collection<SelectRunCheckDto>();
            }
        }

        /// <summary>Initializes a new instance of the <see cref="SelectCheckDto"/> class.</summary>
        /// <param name="checkMethodName">チェック対象のメソッド名を表す文字列</param>
        public SelectCheckDto(string checkMethodName)
        {
            if (this.RunCheckMethod == null)
            {
                this.RunCheckMethod = new Collection<SelectRunCheckDto>();
            }

            this.CheckMethodName = checkMethodName;
        }

        /// <summary>
        /// チェック対象の名称（日本語）
        /// </summary>
        [Category("チェック実施メソッド指定")]
        //[DisplayName("チェック")]
        [TypeConverter(typeof(CheckMethodConverter))]
        public string CheckMethodName { get; set; }

        /// <summary>
        /// チェッククラスへ送信するコントロール
        /// </summary>
        [Category("送信対象コントロール指定")]
        //[DisplayName("同時チェックコントロール")]
        public string[] SendParams { get; set; }

        /// <summary>
        /// 新規画面区分にてチェックを行うか
        /// </summary>
        [Category("起動画面区分設定")]
        //[DisplayName("新規画面起動不可フラグ")]
        public bool NewUnChecked { get; set; }

        /// <summary>
        /// 更新画面区分にてチェックを行うか
        /// </summary>
        [Category("起動画面区分設定")]
        //[DisplayName("更新画面起動不可フラグ")]
        public bool UpdateUnChecked { get; set; }

        /// <summary>
        /// 削除画面区分にてチェックを行うか
        /// </summary>
        [Category("起動画面区分設定")]
        //[DisplayName("削除画面起動不可フラグ")]
        public bool DeleteUnChecked { get; set; }

        /// <summary>
        /// 一覧画面区分にてチェックを行うか
        /// </summary>
        [Category("起動画面区分設定")]
        //[DisplayName("一覧画面起動不可フラグ")]
        public bool IchiranUnChecked { get; set; }

        /// <summary>
        /// 新規処理区分にてチェックを行うか
        /// </summary>
        [Category("起動処理区分設定")]
        //[DisplayName("新規画面起動不可フラグ")]
        public bool NewProcessUnChecked { get; set; }

        /// <summary>
        /// 更新処理区分にてチェックを行うか
        /// </summary>
        [Category("起動処理区分設定")]
        //[DisplayName("更新画面起動不可フラグ")]
        public bool UpProcessUnChecked { get; set; }

        /// <summary>
        /// 削除処理区分にてチェックを行うか
        /// </summary>
        [Category("起動処理区分設定")]
        //[DisplayName("削除画面起動不可フラグ")]
        public bool DelProcessUnChecked { get; set; }

        /// <summary>
        /// 一覧処理区分にてチェックを行うか
        /// </summary>
        [Category("起動処理区分設定")]
        //[DisplayName("一覧画面起動不可フラグ")]
        public bool IchiProcessUnChecked { get; set; }

        /// <summary>
        /// チェック起動前処理
        /// 該当チェックの起動可否をチェックする
        /// </summary>
        [Category("チェック起動前処理")]
        [System.ComponentModel.Editor(typeof(RunCheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Collection<SelectRunCheckDto> RunCheckMethod { get; set; }

        /// <summary>
        /// チェック処理実行時に表示するエラーメッセージ指定
        /// </summary>
        [Category("エラー時表示メッセージ")]
        public string DisplayMessage { get; set; }
    }
}
