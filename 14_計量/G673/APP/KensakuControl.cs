using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Utility;

namespace Shougun.Core.Scale.KeiryouIchiran.APP
{
    internal partial class KensakuControl : UserControl
    {
        internal KensakuControl(LogicClass logicclass)
        {
            InitializeComponent();
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = logicclass;
        }

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 業者CD Enter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.BEFORE_GYOUSHA_CD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者CD 検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckGyousha();
        }

        /// <summary>
        /// 現場CD 検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckGenba();
        }

        /// <summary>
        /// 運搬業者CD 検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPNA_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUnpanGyousha();
        }

        /// <summary>
        /// 車輌CD 検証処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.CheckSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CD　Enter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 車輌CD　Enter処理
            this.logic.BEFORE_SHARYOU_CD = this.SHARYOU_CD.Text;
        }

    }
}
