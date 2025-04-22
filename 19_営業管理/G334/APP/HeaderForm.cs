using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.APP
{
    /// <summary>
    /// G334：取引履歴一覧 ヘッダクラス
    /// </summary>
    public partial class HeaderForm : HeaderBaseForm
    {
        #region 内部変数
        #endregion

        #region プロパティ

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

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderForm()
        {
            LogUtility.DebugMethodStart();

            // 初期化
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント

        /// <summary>
        /// 入力内容のチェック(数値以外は空文字)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBox_Leave(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                Control con = sender as Control;

                if (null != con)
                {
                    int i;
                    if (!int.TryParse(con.Text, out i))
                    {
                        con.Text = string.Empty;
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion


    }
}