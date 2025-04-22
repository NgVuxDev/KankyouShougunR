using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Event;
using r_framework.Utility;
using Shougun.Core.Message;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku
{
    /// <summary>
    /// 定期配車実績表
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region 初期処理

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_TEIKI_JISSEKI_HOUKOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                // 画面初期化処理
                this.logic.WindowInit();

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        
        #endregion

        #region 登録時チェック処理
        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckRegist(object sender, RegistCheckEventArgs e)
        {
            // 日付コントロールチェック
            if (sender is CustomDateTimePicker)
            {
                var item = sender as CustomDateTimePicker;

                // 期間(FROM～TO)チェック
                if (item.Name.Equals(this.KIKAN_FROM.Name))
                {
                    int iCompare = string.Compare(this.KIKAN_FROM.Text, this.KIKAN_TO.Text);
                    if (iCompare == 1)
                    {
                        e.errorMessages.Add(Shougun.Core.Message.MessageUtility.GetMessage("E043").Text);
                        this.KIKAN_FROM.BackColor = Constans.ERROR_COLOR;
                        this.KIKAN_TO.BackColor = Constans.ERROR_COLOR;
                    }
                }
            }
        }
        #endregion

        private void KIKAN_KBN_2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.KIKAN_KBN_2.Checked)
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func1.Enabled = true;
                parentForm.bt_func2.Enabled = true;
                this.KIKAN_FROM.Enabled = false;
                this.KIKAN_TO.Enabled = false;

                DateTime calendarBefor = parentForm.sysDate;
                if (!string.IsNullOrEmpty(this.KIKAN_FROM.Text))
                {
                    calendarBefor = Convert.ToDateTime(this.KIKAN_FROM.Value);
                }
                int day = calendarBefor.Day;
                calendarBefor = calendarBefor.AddDays(-(day - 1));
                this.KIKAN_FROM.Value = calendarBefor;
                this.KIKAN_TO.Value = calendarBefor.AddMonths(1).AddDays(- 1);

            }
            else
            {
                var parentForm = (BusinessBaseForm)this.Parent;
                parentForm.bt_func1.Enabled = false;
                parentForm.bt_func2.Enabled = false;
                this.KIKAN_FROM.Enabled = true;
                this.KIKAN_TO.Enabled = true;
            }
        }

        private void KIKAN_TO_DoubleClick(object sender, EventArgs e)
        {
            this.KIKAN_TO.Value = this.KIKAN_FROM.Value;
        }

    }
}
