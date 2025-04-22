using System;
using System.Collections.Generic;
using r_framework.CustomControl;

namespace r_framework.Event
{
    /// <summary>
    /// 登録時チェック処理のイベント定義
    /// </summary>
    public class RegistCheckEventArgs : EventArgs
    {
        /// <summary>
        /// エラーメッセージプロパティ
        /// </summary>
        public List<string> errorMessages { get; set; }

        /// <summary>
        /// マルチロウオブジェクトプロパティ
        /// </summary>
        public GcCustomMultiRow multiRow { get; set; }

        /// <summary>
        /// データグリッドビューオブジェクトプロパティ
        /// </summary>
        public CustomDataGridView dataGridView { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RegistCheckEventArgs()
            : base()
        {
            this.errorMessages = new List<string>();
            this.multiRow = null;
            this.dataGridView = null;
        }
    }
}
