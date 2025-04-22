using System;
using System.ComponentModel;
using System.Linq;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Allocation.HaishaMeisai
{

    /// <summary>
    /// 配車明細表
    /// </summary>
    public partial class UIForm : SuperForm
    {

        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private HaishaMeisaiLogicClass logic;

        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region 初期処理

        #region コンストラクタ
        public UIForm()
            : base(WINDOW_ID.T_HAISHA_MEISAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new HaishaMeisaiLogicClass(this);

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

        #region 運転者CD_FROM LostFocus処理
        /// <summary>
        /// 運転者CD_FROM LostFocus処理
        /// </summary>
        /// <param name="e"></param>
        private void UNTENSHA_CD_FROM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 未入力時は何もしない
                if (this.UNTENSHA_CD_FROM.Text.Trim().Length == 0)
                {
                    this.UNTENSHA_NAME_FROM.Text = string.Empty;
                    return;
                }
                var shortName = this.logic.untenshaAll.Where(s => s.SHAIN_CD == this.UNTENSHA_CD_FROM.Text.Trim()).Select(s => s.SHAIN_NAME_RYAKU).FirstOrDefault();
                if (shortName == null)
                {
                    this.UNTENSHA_CD_FROM.IsInputErrorOccured = true;
                    e.Cancel = true;

                    //レコードが存在しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.UNTENSHA_CD_FROM.SelectAll();
                    this.UNTENSHA_NAME_FROM.Clear();
                }
                else
                {
                    this.UNTENSHA_NAME_FROM.Text = shortName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CD_FROM_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        
        #region 運転者CD_TO LostFocus処理
        /// <summary>
        /// 運転者CD_TO LostFocus処理
        /// </summary>
        /// <param name="e"></param>
        private void UNTENSHA_CD_TO_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 未入力時は何もしない
                if (this.UNTENSHA_CD_TO.Text.Trim().Length == 0)
                {
                    this.UNTENSHA_NAME_TO.Text = string.Empty;
                    return;
                }
                var shortName = this.logic.untenshaAll.Where(s => s.SHAIN_CD == this.UNTENSHA_CD_TO.Text.Trim()).Select(s => s.SHAIN_NAME_RYAKU).FirstOrDefault();
                if (shortName == null)
                {
                    this.UNTENSHA_CD_TO.IsInputErrorOccured = true;
                    e.Cancel = true;

                    //レコードが存在しない
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.UNTENSHA_CD_TO.SelectAll();
                    this.UNTENSHA_NAME_TO.Clear();
                }
                else
                {
                    this.UNTENSHA_NAME_TO.Text = shortName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CD_TO_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
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
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end
    }
}
