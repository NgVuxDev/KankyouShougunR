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

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        #region ヘッダ画面Form

        public HeaderForm()
        {
            try
            {
                // 初期化
                InitializeComponent();

                // Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderForm", ex);
                throw;
            }
        }

        #endregion

        #region 画面コントロールイベント

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                // 画面Load
                base.OnLoad(e);

                // 初期値設定
                this.ReadDataNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
        }

        private void KYOTEN_CD_Leave(object sender, EventArgs e)
        {
            try
            {
                // 変数定義
                int i;

                // 拠点整数以外場合
                if (!int.TryParse(this.KYOTEN_CD.Text, out i))
                {
                    this.KYOTEN_CD.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KYOTEN_CD_Leave", ex);
                throw;
            }
        }

        private void alertNumber_Validated(object sender, EventArgs e)
        {
            try
            {
                this.alertNumber.Text = SetComma(this.alertNumber.Text);
            }
            catch (Exception ex)
            {
                LogUtility.Error("alertNumber_Validated", ex);
                throw;
            }
        }

        /// <summary>
        /// カンマ編集
        /// </summary>
        /// <returns></returns>
        private string SetComma(string value)
        {
            try
            {
                value = value.Replace(",", "");
                if (string.IsNullOrEmpty(value) == true)
                {
                    return "0";
                }
                else
                {
                    return string.Format("{0:#,0}", Convert.ToDecimal(value));
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetComma", ex);
                throw;
            }
        }

        #endregion
    }
}