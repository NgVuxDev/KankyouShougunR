using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Utility;

namespace r_framework.Components
{
    /// <summary>
    /// IME入力中かどうかを取得するクラス
    /// </summary>
    [DefaultEvent("ConversionChanged")]
    public class ImeStatus : System.ComponentModel.Component, IMessageFilter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImeStatus()
        {
            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public ImeStatus(IContainer container)
            : this()
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        private static readonly object EventConversionChanged = new object();
        private bool isConversion = false;
        /// <summary>
        /// IME入力中かどうかを取得します。
        /// </summary>
        [Browsable(false)]
        public bool IsConversion
        {
            get { return this.isConversion; }
        }

        /// <summary>
        /// IME入力中変更イベント
        /// </summary>
        public event EventHandler ConversionChanged
        {
            add
            {
                this.Events.AddHandler(EventConversionChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventConversionChanged, value);
            }
        }

        /// <summary>
        /// 後処理
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Application.RemoveMessageFilter(this);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// メッセージがディスパッチされる前に、フィルタで排除します。 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            const int WmStartComposition = 0x10D;
            const int WmEndComposition = 0x10E;
            switch (m.Msg)
            {
                case WmStartComposition:
                    this.isConversion = true;
                    OnConversionChanged(EventArgs.Empty);
                    break;
                case WmEndComposition:
                    this.isConversion = false;
                    OnConversionChanged(EventArgs.Empty);
                    break;
            }
            return false;
        }

        /// <summary>
        /// IME入力中変更時処理
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnConversionChanged(EventArgs e)
        {
            EventHandler handler = this.Events[EventConversionChanged] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void ReleaseImeMode(IntPtr handle)
        {
            ImeUtility.ReleaseImeMode(handle);
            this.isConversion = false;
        }
        public void ChengeIsConversion(bool b)
        {
            this.isConversion = b;
        }
    }

}
