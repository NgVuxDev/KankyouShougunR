using System;
using System.Windows.Forms;
using System.ComponentModel;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.APP.Base;
using System.Globalization;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビュー用のカスタムデイトタイムエディターコントロール
    /// </summary>
    class DgvCustomDataTimeEditingControl : CustomDateTimePicker, IDataGridViewEditingControl
    {


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomDataTimeEditingControl()
        {
        }

        /// <summary>
        /// 貼り付け防止処理
        /// </summary>
        const int WM_PASTE = 0x302;

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="m"></param>
        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            // CustomDateTimePickerがやるので、ここでは処理不要

            base.WndProc(ref m);
        }

        /// <summary>
        /// フォーマットプロパティ
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {

                return this.Value;
            }
            set
            {
                this.Value = value;
                return;
            }
        }

        /// <summary>
        /// フォーマット取得処理
        /// </summary>
        /// <param name="context">エラーコンテキスト</param>
        /// <returns>フォーマット情報</returns>
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return this.Text;

            //if (context.HasFlag(DataGridViewDataErrorContexts.Formatting))
            //{
            //    //表示用文字列
            //    return this.Text;
            //}
            //else
            //{
            //    //値
            //    return null;
            //}
        }

        /// <summary>
        /// セルのスタイルを設定
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        /// <summary>
        /// エディットコントロールのRow番号指定
        /// </summary>
        public int EditingControlRowIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                    if (this.SelectionLength == 0 && this.SelectionStart == 0)
                    {
                        //左端はグリッドの左移動させる
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                ////case Keys.Up:
                ////case Keys.Down:
                case Keys.Right:
                    //case Keys.Home:
                    //case Keys.End:
                    ////case Keys.PageDown:
                    ////case Keys.PageUp:
                    if (this.SelectionLength == 0 && this.SelectionStart == this.TextLength)
                    {
                        //右端はグリッドの右移動させる
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                //case Keys.Back:
                //    return true;
                default:
                    return false;
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            this.EditingControlValueChanged = false; //初期化
            this.MaxLength = 10; //入力制限
            this.TextAlign = HorizontalAlignment.Left; //左寄せ
            return;

        }

        //編集可能になる前はtrue なったらfalse
        private bool _isInit = true;
        internal bool IsInit
        {
            get { return this._isInit; }
            set { this._isInit = value; }
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView { get; set; }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged { get; set; }

        /// <summary>
        /// EditingPanelCursorメソッド
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        /// <summary>
        /// 入力値が変更された場合
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnTextChanged(EventArgs eventargs)
        {

            // Notify the DataGridView that the contents of the cell

            //初期化中の値セットでも、変更と判断してしまっていたのでその対応。
            if (!(this.IsInit || this._entering || this._leaving))
            {
                this.EditingControlValueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
            base.OnTextChanged(eventargs);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //継承元コントロールが制御するので、ここでは実装不要

            ////0-9 バックスペース、スラッシュを許可
            //if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b' || e.KeyChar == '/')
            //{
            //    //数値は入力可
            //}
            //else
            //{
            //    e.Handled = true; //何もさせない
            //}

            base.OnKeyPress(e);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
        }
    }
}
