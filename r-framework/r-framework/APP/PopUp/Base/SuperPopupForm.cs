using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace r_framework.APP.PopUp.Base
{
    /// <summary>
    /// すべてのポップアップ画面の基となる画面
    /// </summary>
    public partial class SuperPopupForm : Form
    {
        /// <summary>
        /// システム日付
        /// </summary>
        public DateTime sysDate;

        /// <summary>
        /// 画面ID
        /// </summary>
        public WINDOW_ID WindowId { get; set; }

        /// <summary>
        /// 画面で利用するパラメータ
        /// </summary>
        public object[] Params { get; set; }

        /// <summary>
        /// 親画面のコントロール配列
        /// </summary>
        public object[] ParentControls { get; set; }

        /// <summary>
        /// 前画面から受け取るポップアップ画面表示時の追加ＳＱＬ
        /// </summary>
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMasterAccessStartUp { get; set; }

        /// <summary>
        /// Entity
        /// </summary>
        public SuperEntity Entity { get; set; }

        /// <summary>
        /// ポップアップ表示絞込み用条件
        /// </summary>
        public Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }

        /// <summary>
        /// ポップアップ表示対象のカラム名
        /// </summary>
        public string PopupGetMasterField { get; set; }

        /// <summary>
        /// 複数選択の指定
        /// </summary>
        public bool PopupMultiSelect { get; set; }

        /// <summary>
        /// 取得済みのデータ
        /// </summary>
        public DataTable table { get; set; }

        /// <summary>
        /// <para>表示データテーブルの列名。</para>
        /// <para>nullでない場合this.tableの値を設定したDataGridViewの列名に使用。</para>
        /// </summary>
        public string[] PopupDataHeaderTitle { get; set; }


        /// <summary>
        /// ポップアップのタイトルを強制的に上書きする場合は設定してください
        /// </summary>
        public string PopupTitleLabel { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SuperPopupForm(WINDOW_ID windoId)
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SuperPopupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 返却値
        /// </summary>
        [Category("EDISONプロパティ")]
        public virtual Dictionary<int, List<PopupReturnParam>> ReturnParams { get; set; }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuperForm_KeyUp(object sender, KeyEventArgs e)
        {
            //IME入力中は実行しない
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            //switch (e.KeyCode)
            //{
            //    case Keys.F1:
            //        ControlUtility.ClickButton(this, "bt_func1");
            //        break;
            //    case Keys.F2:
            //        ControlUtility.ClickButton(this, "bt_func2");
            //        break;
            //    case Keys.F3:
            //        ControlUtility.ClickButton(this, "bt_func3");
            //        break;
            //    case Keys.F4:
            //        ControlUtility.ClickButton(this, "bt_func4");
            //        break;
            //    case Keys.F5:
            //        ControlUtility.ClickButton(this, "bt_func5");
            //        break;
            //    case Keys.F6:
            //        ControlUtility.ClickButton(this, "bt_func6");
            //        break;
            //    case Keys.F7:
            //        ControlUtility.ClickButton(this, "bt_func7");
            //        break;
            //    case Keys.F8:
            //        ControlUtility.ClickButton(this, "bt_func8");
            //        break;
            //    case Keys.F9:
            //        ControlUtility.ClickButton(this, "bt_func9");
            //        break;
            //    case Keys.F10:
            //        ControlUtility.ClickButton(this, "bt_func10");
            //        break;
            //    case Keys.F11:
            //        ControlUtility.ClickButton(this, "bt_func11");
            //        break;
            //    case Keys.F12:
            //        ControlUtility.ClickButton(this, "bt_func12");
            //        break;
            //}

            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
                ControlUtility.ClickButton(this, buttonName);
                e.Handled = true;
            }

        }

        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        public void SuperForm_KeyDown(object sender, KeyEventArgs e)
        {
            //IME入力中は実行しない
            if (this.imeStatus.IsConversion)
            {
                return;
            }



            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                // DataGridViewのフォーカス移動はCustomDataGridViewで対応
                var dataGrid = this.ActiveControl as r_framework.CustomControl.CustomDataGridView;
                if (dataGrid != null)
                {
                    return;
                }

                // 複数行TextBoxなら改行入力
                var textBox = this.ActiveControl as TextBox;
                if (textBox != null && textBox.Multiline && textBox.AcceptsReturn)
                {
                    return;
                }

                // EnterキーでTabキーと同じようにフォーカス
                this.ProcessTabKey(!e.Shift);
                e.Handled = true;
            }

            ////Downは連続で発生するのでUpに変更
            //if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            //{
            //    var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
            //    ControlUtility.ClickButton(this, buttonName);
            //    e.Handled = true;
            //}
        }

        //FW_QA74:LeaveをValidatingへ移動
        private const int WM_CLOSE = 0x10;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_CLOSE = 0xF060;
        /// <summary>
        /// メッセージポンプ
        /// ・閉じる系の処理はvalidateをキャンセル
        /// </summary>
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == WM_CLOSE)
            {
                //base.WndProcの前に書き換え必要
                this.FindForm().AutoValidate = AutoValidate.Disable;
            }

            //×やALT+F4
            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_CLOSE)
            {
                //base.WndProcの前に書き換え必要
                this.FindForm().AutoValidate = AutoValidate.Disable;
            }
            base.WndProc(ref m);
        }

        protected override void OnActivated(System.EventArgs e)
        {
            base.OnActivated(e);

            // アクティブなテキストボックスのIME変換モードを設定する。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this.ActiveControl);
        }


        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (this.StartPosition == FormStartPosition.WindowsDefaultLocation && this.Modal)
            {
                Form parentForm = Form.ActiveForm;
                if (parentForm != null)
                {
                    //this.Location = new System.Drawing.Point(parentForm.Location.X + (parentForm.Width - this.Width) / 2,
                    //                                         parentForm.Location.Y + (parentForm.Height - this.Height) / 2);
                    this.Location = new System.Drawing.Point(parentForm.Location.X, parentForm.Location.Y);
                }
                else
                {
                    this.StartPosition = FormStartPosition.CenterParent;
                }
            }
        }
    }
}