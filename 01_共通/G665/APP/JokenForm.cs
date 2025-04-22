using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Logic;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP
{
    /// <summary>
    /// 範囲条件指定ポップアップ画面
    /// </summary>
    public partial class JokenForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 範囲条件
        /// </summary>
        internal JokenDto Joken { get; private set; }

        ///// <summary>
        ///// エラー発生状態(True:エラー発生)
        ///// </summary>
        //internal bool IsError { get; set; }

        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private JokenLogicClass logic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="joken">出力条件</param>
        public JokenForm(JokenDto joken)
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            this.Joken = joken;
            this.logic = new JokenLogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region イベント

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);

            try
            {
                if (!this.logic.WindowInit())
                    return;

                if (!this.logic.JokenInit())
                    return;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.JokenSave())
                    return;

                this.DialogResult = DialogResult.Yes;
                this.logic.FormClose();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.DialogResult = DialogResult.No;
            this.logic.FormClose();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputTo_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.logic.InputToCopy(sender as TextBox);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHaniKbn_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.HaniJokenChanged(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDateSpecify2_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.DateSpecify2Changed(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGyoushaCd_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.GyoushaCdChanged(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBankCd_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.BankCdChanged(false);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtGenbaCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.GenbaCheckAndSetting(sender as CustomTextBox))
                e.Cancel = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBankShitenCd_Validating(object sender, CancelEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.BankShitenCdCheckAndSetting(sender as CustomTextBox))
                e.Cancel = true;

            LogUtility.DebugMethodEnd();
        }

        #endregion
    }
}