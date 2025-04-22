using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using r_framework.Const;

namespace r_framework.Dto
{
    /// <summary>
    /// 共通検索ポップアップに絞り込み条件を渡すためのDto
    /// </summary>
    [Serializable]
    public class PopupSearchSendParamDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PopupSearchSendParamDto()
        {
            if (SubCondition == null)
            {
                SubCondition = new Collection<PopupSearchSendParamDto>();
            }
        }

        /// <summary>
        /// AndかOrかを指定する。
        /// </summary>
        [Category("And_Or指定")]
        [Description("Andで接続するかOrで接続するか指定します。先頭配列の場合は必ずAndで接続するので指定はいりません。")]
        public CONDITION_OPERATOR And_Or { get; set; }

        /// <summary>
        /// 検索ポップアップ画面表示時に絞込みを行うキー名を指定する。
        /// </summary>
        [Category("絞込対象")]
        [Description("絞込み対象のキー名を指定してください。(アプリケーション作成手順書にキー指定が必要な画面が記載されているはずなので参照してください。)")]
        public string KeyName { get; set; }

        /// <summary>
        /// キー名に対応するコントロールを指定する。
        /// </summary>
        [Category("絞込対象")]
        [Description("絞込み対象の値が設定されているコントロールを指定してください。")]
        public string Control { get; set; }

        /// <summary>
        /// キー名に対応する値を指定する。
        /// </summary>
        [Category("絞込対象")]
        [Description("絞込み対象の値を直接指定してください。Controlが指定されていない場合にこの値を使います。")]
        public string Value { get; set; }

        /// <summary>
        /// 括弧付きの絞込を指定する場合に設定する。
        /// </summary>
        [Category("絞込対象")]
        [Description("括弧付きの絞込を行いたい場合に指定してください。")]
        [TypeConverter(typeof(PopupSearchSendParamDto))]
        public Collection<PopupSearchSendParamDto> SubCondition { get; set; }
    }
}
