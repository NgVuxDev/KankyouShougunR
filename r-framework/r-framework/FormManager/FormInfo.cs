using System;
using System.Windows.Forms;

namespace r_framework.FormManager
{
    /// <summary>
    /// フォーム情報
    /// オープン中フォームの個々の情報を格納する。
    /// フォームマネージャはこのオブジェクトのコレクションでオープン中フォームを管理する。の個々の情報を格納する。
    /// </summary>
    class FormInfo
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormInfo(string formID, Form form, IShougunForm formInterface, string caption, bool isCalledMenu)
        {
            this.FormID = formID;
            this.Form = form;
            this.FormInterface = formInterface;
            this.Caption = caption;
            this.IsCalledMenu = isCalledMenu;
        }

        /// <summary>
        /// フォーム識別子 ex)"G051"
        /// </summary>
        public string FormID;

        /// <summary>
        /// 識別子に対応したオープン中フォームへの参照
        /// </summary>
        public Form Form;

        /// <summary>
        /// フォームのインタフェース
        /// </summary>
        public IShougunForm FormInterface;

        /// <summary>
        /// フォームのキャプション
        /// </summary>
        public string Caption;

        /// <summary>
        /// リボンメニューから呼ばれたかどうか
        /// </summary>
        public bool IsCalledMenu;
    }
}
