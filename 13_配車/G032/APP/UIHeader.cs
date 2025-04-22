using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Const;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();
                InitializeComponent();

                // Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                base.OnLoad(e);
                //読込データ件数の初期値設定
                this.readDataNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 伝票日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.radbtnDenpyouHiduke.Checked)
                {
                    this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_DenPyou;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("radbtnDenpyouHiduke_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.radbtnNyuuryokuHiduke.Checked)
                {
                    this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_NyuuRyoku;
                }
                else
                {
                    this.lab_HidukeNyuuryoku.Text = ConstCls.HidukeName_DenPyou;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("radbtnNyuuryokuHiduke_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 日付選択フォーカス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_HidukeSentaku_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (string.IsNullOrEmpty(this.txtNum_HidukeSentaku.Text))
                {
                    //警告メッセージを表示して、フォーカス移動しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("W001", ConstCls.HidukeCD_DenPyou, ConstCls.HidukeCD_NyuuRyoku);
                    this.txtNum_HidukeSentaku.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("txtNum_HidukeSentaku_Leave", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end
    }
}
