// $Id: LogicClass.cs 24789 2014-07-07 01:25:23Z j-kikuchi $
using System;
using System.Data;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Allocation.MobileShougunTorikomi.Logic;
using System.Windows.Forms;


namespace Shougun.Core.Allocation.MobileShougunTorikomi.APP
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private MobileShougunTorikomiLogic MobileShougunTorikomiLogic;

        // コース名称DataTable
        private DataTable courseNameDt = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public UIForm()
            //コンストラクタ
            : base(WINDOW_ID.T_MOBILE_SHOUGUN_TORIKOMI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MobileShougunTorikomiLogic = new MobileShougunTorikomiLogic(this);

            //// コース名称マスタ
            //this.GetCourseName();
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            this.MobileShougunTorikomiLogic.WindowInit();

			// Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        private void ntxt_CourseCd_Validated(object sender, EventArgs e)
        {
            MobileShougunTorikomiLogic.CheckCourse();
        }

        private void set_ntxt_Control_Color(object sender, EventArgs e)
        {
            this.ntxt_ContenaMitouroku.ForeColor = System.Drawing.Color.Red;
            this.ntxt_SpotMitouroku.ForeColor = System.Drawing.Color.Red;
            this.ntxt_TeikiMitouroku.ForeColor = System.Drawing.Color.Red;
        }


        #region 車輌更新後処理
        /// <summary>
        /// 車輌更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.MobileShougunTorikomiLogic.CheckSharyouCd())
                {
                    // フォーカス設定
                    this.ntxt_SharyouCd.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validated", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　start
        private void dtp_SagyouDateFrom_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtp_SagyouDateTo.Text))
            {
                this.dtp_SagyouDateTo.IsInputErrorOccured = false;
                this.dtp_SagyouDateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void dtp_SagyouDateTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtp_SagyouDateFrom.Text))
            {
                this.dtp_SagyouDateFrom.IsInputErrorOccured = false;
                this.dtp_SagyouDateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141021 Houkakou 「モバイル将軍取込」の日付チェックを追加する　end

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
    }
}