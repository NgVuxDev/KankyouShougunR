using System;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            try
            {
                LogUtility.DebugMethodStart();

                InitializeComponent();

                //Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region override

        /// <summary>
        /// 画面初期表示処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region アラート件数プロパティ

        /// <summary>
        /// アラート件数
        /// </summary>
        public int AlertCount
        {
            get
            {
                int res = 0;
                decimal tmp = 0m;
                if (decimal.TryParse(this.alertNumber.Text, System.Globalization.NumberStyles.AllowThousands, null, out tmp))
                {
                    res = int.Parse(tmp.ToString());
                }
                return res;
            }
        }

        #endregion

        #region アラート件数入力有効性チェック

        /// <summary>
        /// アラート件数入力有効性チェック
        /// </summary>
        private void alertNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if ((this.alertNumber.Text.Length != 0 &&
                    Int32.Parse(this.alertNumber.Text.Replace(",", "")) == 0) ||
                    this.alertNumber.Text.Length == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E002", "アラート件数", "1～99999");
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
    }
}
