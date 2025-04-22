using System;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    public partial class mapPopupForm : SuperPopupForm
    {
        #region フィールド

        public mapPopupLogic logic;

        internal MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        public ControlUtility controlUtil = new ControlUtility();

        #endregion

        #region コンストラクタ

        public mapPopupForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 読込

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new mapPopupLogic(this);

            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            this.logic.WindowInit();

            //一覧内のチェックボックスの設定
            this.logic.HeaderCheckBoxSupport();
        }

        #endregion

        #region ファンクション

        /// <summary>
        /// 地図表示
        /// </summary>
        internal virtual void bt_func9_Click(object sender, System.EventArgs e)
        {
            this.logic.MapOpen();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal virtual void bt_func12_Click(object sender, System.EventArgs e)
        {
            base.ReturnParams = null;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region イベント

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = ActiveControl as SuperPopupForm;

            if (activ == null)
            {
                if (ActiveControl != null)
                {
                    this.lb_hint.Text = (string)ActiveControl.Tag;
                }
            }
        }

        #region タブ内

        /// <summary>
        /// 未配車表示変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_MihaishaHyouzi_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_MihaishaHyouzi_TextChanged();
        }

        /// <summary>
        /// 伝票種類変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void chk_DenpyouShurui_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.chk_DenpyouShurui_CheckedChanged();
        }

        /// <summary>
        /// 拠点指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_KyotenShitei_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_KyotenShitei_TextChanged();
        }

        /// <summary>
        /// 現着時間指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_GenchakuShitei_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_GenchakuShitei_TextChanged();
        }

        /// <summary>
        /// 車種指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_ShashuShitei_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_ShashuShitei_TextChanged();
        }

        /// <summary>
        /// 運搬業者指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_UnpanGyousha_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_UnpanGyousha_TextChanged();
        }

        /// <summary>
        /// 運転者指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_Untensha_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_Untensha_TextChanged();
        }

        /// <summary>
        /// コンテナ作業指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_ContenaSagyouShitei_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_ContenaSagyouShitei_TextChanged();
        }

        /// <summary>
        /// コンテナ種類指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_ContenaShurui_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_ContenaShurui_TextChanged();
        }

        /// <summary>
        /// 設置コンテナ表示変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_SecchiContena_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_SecchiContena_TextChanged();
        }

        /// <summary>
        /// コンテナ種類指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_ContenaShurui2_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_ContenaShurui2_TextChanged();
        }

        /// <summary>
        /// 設置期間指定変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txtNum_SecchiKikan_TextChanged(object sender, EventArgs e)
        {
            this.logic.txtNum_SecchiKikan_TextChanged();
        }

        #endregion

        #endregion

    }
}
